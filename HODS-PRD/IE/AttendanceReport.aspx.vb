Imports System.Globalization

Public Class AttendanceReport
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 in ('W01','W02','W07','W12','W19','W23','W25','W27')
            'ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)
            ControlForm.showCheckboxList(cblWc, SQL, "MD002", "MD001", 4, Conn_SQL.ERP_ConnectionString)

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
        Dim tempTable As String = "tempAttendance" & Session("UserName")
        Dim showType As String = ddlShowType.Text,
            grpDate As Boolean = True
        If showType = "1" Then
            grpDate = False
        End If
        CreateTempTable.createTempAttendanceReport(tempTable, grpDate)

        Dim SQL As String = "",
            ISQL As String = "",
            WHR As String = ""
        'Dim WC As String = ddlWC.Text
        'If WC <> "" And WC <> "ALL" Then
        '    WHR = WHR & " and SFCTD.TD004='" & WC & "'"
        'End If
        WHR = WHR & Conn_SQL.Where("SFCTD.TD004", cblWc)
        Dim grpBy As String = "SFCTD.TD004",
            fldIns As String = "wc"
        If grpDate Then 'summary by wc,date
            grpBy = "SFCTD.TD004,SFCTD.TD008"
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
        WHR = WHR & Conn_SQL.Where("SFCTD.TD008", strDate, endDate)
        'WHR = WHR & " and SFCTD.TD008 between '" & strDate & "' and '" & endDate & "' "
        SQL = " select " & grpBy & ",sum(SFCTE.TE011),sum(case when SFCTE.TE012='0' then 0 else SFCTE.TE011 end) " & _
              " from JINPAO80.dbo.SFCTE SFCTE left join JINPAO80.dbo.SFCTD SFCTD on SFCTD.TD001 = SFCTE.TE001 and SFCTD.TD002 = SFCTE.TE002 " & _
              " where SFCTD.TD005='Y' and SFCTE.TE012 in ('30600','28800','0') and SFCTE.TE006 = '5195' " & WHR & _
              " group by " & grpBy & _
              " order by " & grpBy

        ISQL = "insert into DBMIS.dbo." & tempTable & "(" & fldIns & ",AllOperator,WorkOperator) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        Dim fld As String = "'" & configDate.dateShow2(strDate, "-") & "' as 'From Date','" & configDate.dateShow2(endDate, "-") & "' as 'To Date',",
            ord As String = "wc"
        If grpDate Then
            fld = "(SUBSTRING(T.docDate,7,2)+'-'+SUBSTRING(T.docDate,5,2)+'-'+SUBSTRING(T.docDate,1,4)) as 'Doc Date',"
            ord = "wc,docDate"
        End If
        SQL = " select T.wc as 'WC',MD.MD002 as 'WC Name'," & fld & " AllOperator as 'All Operator',WorkOperator as 'Working Operator'," & _
              " rtrim(cast(cast(round(case when AllOperator=0 then 0 else (WorkOperator/AllOperator)*100 end,2) as decimal(10,2)) as CHAR )) +'%' as 'Attendence%' " & _
              " from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.CMSMD MD on MD.MD001=T.wc " & _
              " order by " & ord
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
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

                    hplDetail.NavigateUrl = "AttendanceReportPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", .DataItem("WC Name"))
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub
End Class