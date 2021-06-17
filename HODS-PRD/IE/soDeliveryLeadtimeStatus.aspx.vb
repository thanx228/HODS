Public Class soDeliveryLeadtimeStatus
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
           WHR As String = "",
           saleType As String = ddlSaleType.Text,
           saleNo As String = tbSaleNo.Text.Trim,
           saleSeq As String = tbSaleSeq.Text.Trim,
           typeCon As String = ddlLeadtime.Text

        WHR = configDate.DateWhere("TD013", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))

        If saleType <> "ALL" Or saleType = "" Then
            WHR = WHR & " and TD001='" & saleType & "' "
        End If
        If saleNo <> "" Then
            WHR = WHR & " and TD002 like '" & saleNo & "%' "
        End If
        If saleSeq <> "" Then
            WHR = WHR & " and TD003 like '%" & saleSeq & "' "
        End If

        Select Case ddlSaleStatus.Text
            Case "1" ' close Status
                WHR = WHR & " and TD016 in ('Y','y') "
            Case "2" ' not Close status
                WHR = WHR & " and TD016 in ('N') "
        End Select


        'If typeCon <> "0" Then
        '    WHR = WHR & " and MC001 is null "
        'End If
        Dim fldDateDiff As String = " DATEDIFF(day,convert(datetime, TC003),convert(datetime,TD013)) "
        WHR = WHR & " and " & fldDateDiff & " > '0' "
        Select Case ddlLeadtime.Text
            Case "1" 'Lead time 0-3 Days
                WHR = WHR & " and " & fldDateDiff & " <= '3' "
            Case "2" 'Lead time 4-5 Days
                WHR = WHR & " and " & fldDateDiff & " between '4' and '5' "
            Case "3" 'Lead time 6-7 Days
                WHR = WHR & " and " & fldDateDiff & " between '6' and '7' "
            Case "4" 'Lead time 8-9 Days
                WHR = WHR & " and " & fldDateDiff & " between '8' and '9' "
            Case "5" 'Lead time > 9 Days
                WHR = WHR & " and " & fldDateDiff & " > '9' "
        End Select
        SQL = " select TD001+'-'+TD002+'-'+TD003 as SO,TD004 as Item,TD006 as Spec," & _
                      " cast(TD008 as decimal(15,2)) as Qty," & _
                      " (SUBSTRING(TD013,7,2)+'-'+SUBSTRING(TD013,5,2)+'-'+SUBSTRING(TD013,1,4)) as 'Delivery Date'," & _
                      " (SUBSTRING(TC003,7,2)+'-'+SUBSTRING(TC003,5,2)+'-'+SUBSTRING(TC003,1,4)) as 'SO Approve Date'," & _
                      " " & fldDateDiff & " as 'Lead Time Days' " & _
                      " from COPTD  " & _
                      " left join COPTC on TC001=TD001 and TC002=TD002 " & _
                      " where TC027 ='Y' " & WHR & _
                      " order by TD013,TD001,TD002,TD003 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Function DateDiff(dateFrom As String, dateTo As String, fldShow As String) As String
        Return " DATEDIFF(day,convert(datetime, " & dateFrom & "),convert(datetime, " & dateTo & ")) as " & fldShow
    End Function
    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SaleOrderLeadtimeStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class