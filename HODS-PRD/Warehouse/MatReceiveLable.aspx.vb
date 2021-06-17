Public Class MatReceiveLable
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
            reset()
            hlEmptyLable.NavigateUrl = "~/PDF/labelMatReceive.aspx?height=150&width=350&iType=&iNo=&iSeq="
        End If
    End Sub

    Protected Sub reset() Handles btReset.Click
        ucReceiptType.setObjectWithAll = "34"
        tbReceiptNo.Text = ""
        ucReceiptDate.dateVal = ""
        tbLot.Text = ""
        gvShow.DataSource = ""
        gvShow.DataBind()

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String,
            WHR As String
        Dim colName() As String = {"Receive Type:TH001",
                                   "Receive No:TH002",
                                   "Receive Seq:TH003",
                                   "Receive Date:TG014",
                                   "PO:TH011",
                                   "Vender:TG005",
                                   "Item:TH004",
                                   "Desc:TH005",
                                   "Spec:TH006",
                                   "WH:TH009",
                                   "Bin:TH072",
                                   "Lot:TG027",
                                   "Receive Qty:TH007:2",
                                   "Receipt Qty(Sheet):UDF53:0",
                                   "Unit:TH008",
                                   "Pack Std:UDF52:2",
                                   "Pack Std(Sheet):UDF54:0",
                                   "Print Time:UDF51:0"}

        WHR = Conn_SQL.Where("TG001", ucReceiptType.getObject)
        WHR &= Conn_SQL.Where("TG002", tbReceiptNo)
        WHR &= Conn_SQL.Where("TG014", ucReceiptDate.dateVal, ucReceiptDate.dateVal)
        WHR &= Conn_SQL.Where("TH010", tbLot)
        WHR &= Conn_SQL.Where("TG013", ddlApp)

        SQL = " select TH001,TH002,TH003,TG014,rtrim(TH011)+'-'+rtrim(TH012)+'-'+rtrim(TH013) TH011," &
              " rtrim(TG005)+'-'+rtrim(MA002) TG005,TH004,TH005,TH006,TH009,replace(TH072,'#','') TH072,replace(TH010,'*','') TH010,TH007,TH008,PURTH.UDF51," &
              " case when isnull(PURTH.UDF52,0)=0 then TH007 else PURTH.UDF52 end UDF52,isnull(PURTH.UDF53,0) UDF53,TG027,isnull(PURTH.UDF54,0) UDF54 " &
              " from PURTH " &
              " left join PURTG on TG001=TH001 and TG002=TH002 " &
              " left join PURMA on MA001=TG005 " &
              " where 1=1 " & WHR &
              " order by TH001,TH002,TH003"

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
                    If Not IsDBNull(.DataItem("TH001")) Then
                        hplDetail.NavigateUrl = "~/PDF/labelMatReceive.aspx?height=150&width=350&iType=" & .DataItem("TH001").ToString.Trim & "&iNo=" & .DataItem("TH002").ToString.Trim & "&iSeq=" & .DataItem("TH003").ToString.Trim
                        hplDetail.Attributes.Add("title", .DataItem("TG005"))
                    End If
                End If
            End If
        End With
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If ucReceiptType.getValue = "ALL" Then
            ucReceiptType.Focus()
            Exit Sub
        End If
        If tbReceiptNo.Text = "" Then
            tbReceiptNo.Focus()
            Exit Sub
        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/labelMatIssue.aspx?iType=" & ucReceiptType.getValue.Trim & "&iNo=" & tbReceiptNo.Text.Trim & "&iSeq=');", True)
    End Sub
End Class