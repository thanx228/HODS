Public Class SaleOrderOverDueUndelivery
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim SQL As String = "",
            WHR As String = "",
            soType As String = ddlSaleType.Text.Trim,
            dueDate As String = tbDate.Text.Trim,
            dateToday As String = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        If soType <> "ALL" Then
            WHR = WHR & " and TD001='" & soType & "'"
        End If
        If dueDate <> "" Then
            dueDate = configDate.dateFormat2(dueDate)
        Else
            dueDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        End If

        SQL = " select TD001+'-'+TD002+'-'+TD003 as 'Sale Order',TD004 as 'Item',TD005 as 'Desc', " & _
              " TD006 as 'Spec'," & _
              " case TD013 when '' then '' else (SUBSTRING(TD013,7,2)+'-'+SUBSTRING(TD013,5,2)+'-'+SUBSTRING(TD013,1,4)) end as 'Plan Delivery Date', " & _
              " cast(TD008 as decimal(15,2)) as 'Order Qty',cast(TD009 as decimal(15,2)) as 'Delivery Qty'," & _
              " cast(TD008-TD009 as decimal(15,2)) as 'Undelivery Qty', " & _
              " DATEDIFF(DD,CONVERT(datetime,TD013),'" & dueDate & "') as 'Date Over Due', " & _
              " case MC007 when NULL then 0 else cast(MC007 as decimal(15,2)) end as 'Stock Qty'" & _
              " from COPTD " & _
              " left join COPTC on TC001=TD001 and TC002=TD002  " & _
              " left join INVMC on MC001=TD004 and MC002='2101' " & _
              " where TC027='Y' and TD021='Y' and TD016 = 'N' " & _
              " and TD013<'" & dueDate & "' " & WHR & _
              " order by TD004,TD001,TD002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True

    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SaleOrderOverDueUndelivery" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class