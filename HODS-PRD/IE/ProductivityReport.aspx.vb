Imports System.Globalization

Public Class ProductivityReport
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                 Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 in ('W01','W02','W07','W12','W19','W23','W25','W27')
            ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
            
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("ProductivityReport" & Session("UserName"), gvShow)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempProductivity" & Session("UserName")
        Dim showType As String = ddlShowType.Text,
            grpDate As Boolean = True
        If showType = "1" Then
            grpDate = False
        End If
        CreateTempTable.createTempProductivityReport(tempTable, grpDate)

        Dim SQL As String = ""
        Dim ISQL As String = ""
        Dim WHR As String = ""
        Dim WHR2 As String = ""
        Dim WC As String = ddlWC.Text
        If WC <> "" And WC <> "ALL" Then
            WHR = WHR & " and SFCTB.TB005='" & WC & "'"
            WHR2 = WHR2 & " and TD004='" & WC & "'"
        Else
            Dim sSQL As String = "select CMSMD.MD001 from JINPAO80.dbo.CMSMD CMSMD"
            WHR = WHR & " and SFCTB.TB005 in (" & sSQL & ")"
            WHR2 = WHR2 & " and TD004 in (" & sSQL & ")"
        End If
        Dim grpBy As String = "SFCTB.TB005",
            grpBy2 As String = "TD004",
            fldIns As String = "wc"
        If grpDate Then ' summary by wc,date
            grpBy = "SFCTB.TB005,SFCTB.TB003"
            grpBy2 = "TD004,TD008"
            fldIns = "wc,docDate"
        End If
        Dim date1 As String = tbDateFrom.Text
        Dim date2 As String = tbDateTo.Text

        Dim strDate As String = "",
            endDate As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat2(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat2(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        SQL = " select " & grpBy & ",sum(case MOCTA.TA015 when 0 then 0 else SFCTC.TC036*floor(SFCTA.TA022/MOCTA.TA015) end),sum(case MOCTA.TA015 when 0 then 0 else SFCTC.TC036*floor(SFCTA.TA035/MOCTA.TA015) end) " & _
              " from JINPAO80.dbo.SFCTC SFCTC " & _
              " left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
              " left join JINPAO80.dbo.SFCTA SFCTA on SFCTA.TA001=SFCTC.TC004 and SFCTA.TA002=SFCTC.TC005  and SFCTA.TA003=SFCTC.TC006 " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " & _
              " where SFCTB.TB013='Y' " & WHR & " and SFCTB.TB003 BETWEEN '" & strDate & "' and '" & endDate & "' " & _
              " and SFCTB.TB001 in (select CMSMQ.MQ001 from JINPAO80.dbo.CMSMQ CMSMQ where CMSMQ.MQ003 not like 'D4%' and CMSMQ.MQ003 not like 'D1%' and CMSMQ.MQ003 like 'D%') " & _
              " group by " & grpBy
        ISQL = "insert into " & tempTable & "(" & fldIns & ",produceTime,machineTime) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        SQL = " select " & grpBy2 & ",sum(TE011*TE012) as sumSec from SFCTE " & _
              " left join SFCTD on TD001=TE001 and TD002=TE002 " & _
              " where TD005='Y' and SFCTE.TE006 = '5195' " & WHR2 & " and TD008 BETWEEN '" & strDate & "' and '" & endDate & "' " & _
              " group by " & grpBy2 & " order by " & grpBy2
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim fldInsHash As Hashtable = New Hashtable
                Dim fldUpdHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable

                whrHash.Add("wc", .Item("TD004")) ' WC
                If grpDate Then
                    whrHash.Add("docDate", .Item("TD008")) ' Date
                End If
                'Insert Zone
                fldInsHash.Add("operationTime", .Item("sumSec")) 'operation Time
                'Insert Zone
                fldUpdHash.Add("operationTime", "'" & .Item("sumSec") & "'") 'operation Time
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
            End With
        Next
        Dim fld As String = ",'" & configDate.dateShow2(strDate, "-") & "' as 'From Date','" & configDate.dateShow2(endDate, "-") & "' as 'To Date'",
            ord As String = "wc"

        If grpDate Then
            fld = ", (SUBSTRING(T.docDate,7,2)+'-'+SUBSTRING(T.docDate,5,2)+'-'+SUBSTRING(T.docDate,1,4)) as 'Doc Date'"
            ord = "wc,docDate"
        End If
        fld = fld & returnFld("machineTime", "'TTL M/C N-Hours'")
        fld = fld & returnFld("produceTime", "'TTL N-Hours'")
        fld = fld & returnFld("operationTime", "'TTL Input Hours'")
        SQL = " select T.wc as 'WC',MD.MD002 as 'WC Name'" & fld & _
              " ,rtrim(cast(cast(round(case when operationTime=0 then 0 else (produceTime/operationTime)*100 end,2) as decimal(10,2)) as CHAR )) +'%' as 'Productivity%' " & _
              " from DBMIS.dbo." & tempTable & " T  " & _
              " left join JINPAO80.dbo.CMSMD MD on MD.MD001=T.wc " & _
              " order by " & ord

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Function returnFld(fldName As String, fldCall As String) As String
        'Return ",(convert(varchar,floor(" & fldName & "/3600)))+':'+(right('0'+convert(varchar,floor((round(" & fldName & "/3600,2)-floor(" & fldName & "/3600))*60)),2)) +((case when " & fldCap & "=0 then '' else(('(')+ convert(varchar,cast(ISNULL(round((round(" & fldName & "/60,4)/" & fldCap & ")*100,2),0) as decimal(9,2))) )+('%)')end)) as " & fldCall
        'Return ",(convert(varchar,floor(" & fldName & "/3600)))+':'+(right('0'+convert(varchar,floor((round(" & fldName & "/3600,2)-floor(" & fldName & "/3600))*60)),2)) as " & fldCall
        Return ",CONVERT(varchar, floor(" & fldName & "/3600)) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),floor((" & fldName & " % 3600) / 60))), 2) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),(" & fldName & " % 60))), 2) as " & fldCall
    End Function
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("WC")) Then
                    Dim link As String = ""
                    link = link & "&wc= " & .DataItem("WC")
                    Dim dateFrom As String = ""
                    Dim dateTo As String = ""

                    If ddlShowType.Text = "1" Then
                        dateFrom = configDate.dateFormat2(.DataItem("From Date"), "-")
                        dateTo = configDate.dateFormat2(.DataItem("To Date"), "-")
                    Else
                        dateFrom = configDate.dateFormat2(.DataItem("Doc Date"), "-")
                    End If
                    link = link & "&dateFrom= " & dateFrom
                    link = link & "&dateTo= " & dateTo

                    hplDetail.NavigateUrl = "ProductivityReportPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", .DataItem("WC Name"))
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub
End Class