Public Class SaleInvoiceCheckAmt
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            'Reset()
            ucInvType.setObjectFull = "IV"
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String,
            WHR As String
        Dim colName() As String = {"Invoice Type:TA001",
                                   "Invoice No:TA002",
                                   "Cust ID:TA004",
                                   "Cust Name:MA002",
                                   "Order Date:TA038",
                                   "Amount:TA041:2",
                                   "Tax:TA042:3",
                                   "Total Amt:TA0412:2",
                                   "Cal Tax Cust:TA0411:3",
                                   "Diff:TA04110:3"}

        WHR = Conn_SQL.Where("TA004", tbCust)
        WHR &= Conn_SQL.Where("TA001", ucInvType.getObject)
        WHR &= Conn_SQL.Where("TA002", tbInvNo)
        WHR &= Conn_SQL.Where("TA038", ucDateFrom.dateVal, ucDateTo.dateVal)

        SQL = "select TA001,TA002,TA004,MA002,TA038,TA041,TA042,TA041+TA042 TA0412,TA041*0.07 TA0411, TA042-(TA041*0.07) TA04110 from ACRTA left join COPMA on MA001=TA004 where TA025 in('Y','N') " & WHR & " order by TA016,TA001,TA002 "
        ControlForm.GridviewColWithLinkFirst(gvShow, colName)
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click

        ControlForm.ExportGridViewToExcel("SaleInvoiceCheckAmt" & Session("UserName"), gvShow)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class