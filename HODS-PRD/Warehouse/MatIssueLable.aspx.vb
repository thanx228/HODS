Public Class MatIssueLable
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim createTableTrace As New createTableTrace
    Const tableMat As String = "ProductionMatUsage"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            createTableTrace.createProductionMatUsage()
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            reset()
        End If
    End Sub

    Protected Sub reset() Handles btReset.Click
        ucIssueType.setObjectWithAll = "54"
        tbIssueNo.Text = ""
        ucIssueDate.dateVal = ""
        tbLot.Text = ""
        gvShow.DataSource = ""
        gvShow.DataBind()
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String,
            WHR As String
        Dim colName() As String = {"Issue Type:TE001",
                                   "Issue No:TE002",
                                   "Issue Seq:TE003",
                                   "Issue Date:TC003",
                                   "MO Type:TE011",
                                   "MO No:TE012",
                                   "Item:TE004",
                                   "Desc:TE017",
                                   "Spec:TE018",
                                   "Lot:UDF01",
                                   "WH:TE008",
                                   "Issue Qty:TE005:2",
                                   "Issue Qty(Sheet):UDF53:0",
                                   "Pack Std:UDF54:2",
                                   "Pack Std(Sheet):UDF55:0",
                                   "WC:TC005",
                                   "Print Time:UDF52:0"}
        '      (MOCTE.UDF54 = 2)
        'And (MOCTE.UDF55 = 4)

        WHR = Conn_SQL.Where("TE001", ucIssueType.getObject)
        WHR &= Conn_SQL.Where("TE002", tbIssueNo)
        WHR &= Conn_SQL.Where("TC003", ucIssueDate.dateVal, ucIssueDate.dateVal)
        WHR &= Conn_SQL.Where("MOCTE.UDF01 ", tbLot)
        WHR &= Conn_SQL.Where("TC009", ddlApp)

        SQL = "select TE001,TE002,TE003,TC003,TE011,TE012,TE004,TE017,TE018," &
              "MOCTE.UDF01,TE008,TE005,TC005,MOCTE.UDF52,isnull(MOCTE.UDF53,0) UDF53,  " &
              "isnull(MOCTE.UDF54,0) UDF54,isnull(MOCTE.UDF55,0) UDF55" &
              " from MOCTE left join MOCTC on TC001=TE001 And TC002=TE002 left join INVMB on MB001=TE004 where 1=1 " & WHR & " order by TE001,TE002,TE003"

        ControlForm.GridviewColWithLinkFirst(gvShow, colName, True, "Print")
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplDetail"), HyperLink)
                If Not IsNothing(hplDetail) Then
                    If Not IsDBNull(.DataItem("TE001")) Then
                        hplDetail.NavigateUrl = "~/PDF/labelMatIssue.aspx?height=150&width=350&iType=" & .DataItem("TE001").ToString.Trim & "&iNo=" & .DataItem("TE002").ToString.Trim & "&iSeq=" & .DataItem("TE003").ToString.Trim
                        hplDetail.Attributes.Add("title", .DataItem("TE018"))
                    End If
                End If
            End If
        End With
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If ucIssueType.getValue = "ALL" Then
            ucIssueType.Focus()
            Exit Sub
        End If
        If tbIssueNo.Text = "" Then
            tbIssueNo.Focus()
            Exit Sub
        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/labelMatIssue.aspx?iType=" & ucIssueType.getValue.Trim & "&iNo=" & tbIssueNo.Text.Trim & "&iSeq=');", True)
    End Sub
End Class