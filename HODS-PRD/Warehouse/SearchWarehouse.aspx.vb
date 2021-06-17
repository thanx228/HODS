Public Class SearchWarehouse
    Inherits System.Web.UI.Page
     Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MC001,MC001+' : '+MC002 as MC002 from CMSMC order by MC001 "
            ControlForm.showCheckboxList(cblWh, SQL, "MC002", "MC001", 4, Conn_SQL.ERP_ConnectionString)
            ucHead.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            btExport.Visible = False

        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String,
            WHR As String

        WHR = Conn_SQL.Where("MC001", tbItem)
        WHR &= Conn_SQL.Where("MB002", tbName)
        WHR &= Conn_SQL.Where("MB003", tbSpec)
        WHR &= Conn_SQL.Where("MC002", cblWh)

        Dim colName() As String = {"Item:MC001", "Desc:MB002", "Spec:MB003",
                                   "WH:MC002", "Stock Qty:MC007:2", "Unit:MB004",
                                   "Shelf:UDF01", "Last Receipt:MC012", "Last Issue:MC013"}
        ControlForm.GridviewColWithLinkFirst(gvShow, colName)
        SQL = " select MC001,MB002,MB003,MC002,MC007,INVMB.UDF01,MB004,MC012,MC013 from INVMC left join INVMB on MB001=MC001 where MB109 = 'Y' and MC007>0 " & WHR & " order by MC001,MC007"

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("Warehouse" & Session("UserName"), gvShow)
    End Sub
End Class