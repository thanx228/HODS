Public Class PendingMO
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showDDL(ddlMoType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            btExportExcel.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            selProperty As String = ddlProperty.Text,
            sSQL As String = ""

        'SQL1 = " (select top 1 SFCTC.TC001+'-'+SFCTC.TC002 from SFCTC " & _
        '      " left join SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
        '      " where SFCTC.TC004=SFCTA.TA001 and SFCTC.TC005=SFCTA.TA002 and SFCTC.TC008=SFCTA.TA003) as 'Transfer No.' "
        sSQL = "(select top 1 rtrim(SFCTA2.TA006)+'-'+SFCTA2.TA007 from SFCTA SFCTA2 where SFCTA2.TA001=SFC.TA001 and SFCTA2.TA002=SFC.TA002 and SFCTA2.TA003 >= reverse(substring(reverse('00'+ cast(CAST(substring(SFC.TA003,1,3) as integer)-1 as VARCHAR(4))),1,3))+'0' and SFCTA2.TA003<SFC.TA003  order by SFCTA2.TA003 desc) as 'Last WC',"
        SQL = " select rtrim(SFC.TA006)+'-'+SFC.TA007 as WC,CMSMW.MW002 as 'Process Name'," & _
              " rtrim(COPTC.TC004) as 'Customers',COPMA.MA002 as 'Customers Name'," & _
              " MOC.TA026+'-'+MOC.TA027+'-'+MOC.TA028 as 'SO'," & _
              " SFC.TA001+'-'+SFC.TA002+'-'+SFC.TA003 as 'MO'," & _
              " MOC.TA035 as 'Item Spec', " & _
              " (SUBSTRING(SFC.TA008,7,2)+'/'+SUBSTRING(SFC.TA008,5,2)+'/'+SUBSTRING(SFC.TA008,1,4)) as 'Plan Start Date', " & _
              " cast(MOC.TA015 as decimal(15,2)) as 'Plan Qty', " & _
              " cast(SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048 as decimal(15,2)) as 'WIP Qty', " & _
               sSQL & _
              " (SUBSTRING(MOC.TA006,1,14)+'-'+SUBSTRING(MOC.TA006,15,2)) as 'Item Code' " & _
              " from SFCTA SFC " & _
              " left join MOCTA MOC on MOC.TA001 = SFC.TA001 and MOC.TA002 = SFC.TA002 " & _
              " left join COPTC on COPTC.TC001 = MOC.TA026 and COPTC.TC002 = MOC.TA027 " & _
              " left join COPMA on COPMA.MA001 = COPTC.TC004  " & _
              " left join CMSMW on CMSMW.MW001 = SFC.TA004" & _
              " where MOC.TA013='Y' and SFC.TA005='" & selProperty & "' " & _
              " and SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048=0 " & _
              " and SFC.TA032 = 'N' and MOC.TA011 not in('y','Y') " & _
              getWhr() & _
              " order by SFC.TA008,SFC.TA001,SFC.TA002,SFC.TA003 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExportExcel.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    'Function returnFld(fldName As String, fldCall As String) As String
    '     Return ",CONVERT(varchar, floor(" & fldName & "/3600)) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),floor((" & fldName & " % 3600) / 60))), 2) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),(" & fldName & " % 60))), 2) as " & fldCall
    'End Function
    Function returnFld(ByVal fldName As String, ByVal fldCall As String) As String
        Return ",CONVERT(varchar, floor(" & fldName & "/60)) as " & fldCall
    End Function

    Protected Sub btExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExportExcel.Click
        ControlForm.ExportGridViewToExcel("WorkCenterStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

    Protected Function getWhr(Optional ByVal showStatus As Boolean = True) As String
        Dim selProperty As String = ddlProperty.Text,
            wc As String = ddlWC.Text,
            code As String = tbCode.Text,
            spec As String = tbSpec.Text,
            whr As String = "",
            moType As String = ddlMoType.Text,
            cust As String = tbCust.Text.Trim,
            saleType As String = ddlSaleType.Text,
            saleNo As String = tbSaleNo.Text.Trim,
            saleSeq As String = tbSaleSeq.Text.Trim


        If selProperty = "2" Then
            wc = tbWC.Text
        End If

        If wc <> "ALL" Then
            If selProperty = "1" Then '1
                whr = whr & " and SFC.TA006='" & wc & "' "
            Else '2
                whr = whr & " and SFC.TA006 like '%" & wc & "%' "
            End If
        End If

        If code <> "" Then
            whr = whr & " and MOC.TA006 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and MOC.TA035 like '%" & spec & "%' "
        End If
        If moType <> "ALL" Then
            whr = whr & " and SFC.TA001 = '" & moType & "' "
        End If


        If saleType <> "ALL" Then
            whr = whr & " and MOC.TA026='" & saleType & "' "
        End If

        If saleNo <> "" Then
            whr = whr & " and MOC.TA027 like '" & saleNo & "%' "
        End If

        If saleSeq <> "" Then
            whr = whr & " and MOC.TA028 like '%" & saleSeq & "' "
        End If

        If cust <> "" Then
            whr = whr & " and COPTC.TC004 = '" & cust.ToUpper() & "' "
        End If

        whr = whr & configDate.DateWhere("SFC.TA008", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
        Return whr
    End Function
End Class