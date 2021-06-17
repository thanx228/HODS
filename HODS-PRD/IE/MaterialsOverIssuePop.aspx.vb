Public Class MaterialsOverIssuePop
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lbMoType.Text = Request.QueryString("moType").Trim
            lbMoNo.Text = Request.QueryString("moNo").Trim
            lbRwCode.Text = Request.QueryString("rwCode").Trim
            lbIssueQty.Text = Request.QueryString("issueQty").Trim
            lbIssueOver.Text = Request.QueryString("issueOver").Trim
            viewData()
            lbCnt.Text = gvShow.Rows.Count
        End If
    End Sub
    Protected Sub viewData()

        Dim moType As String = Request.QueryString("moType").Trim
        Dim moNo As String = Request.QueryString("moNo").Trim
        Dim codeMat As String = Request.QueryString("rwCode").Trim
        Dim SQL As String = ""

        SQL = " select TE001 as 'Doc Type',TE002 as 'Doc No',TC005 as 'Dept',TC021 as 'WC',TC015 as 'Aprover',convert( decimal(20,2),TE005 ) as 'Issue',TE006 as 'Unit' from MOCTE " & _
              " left join MOCTC on TC001=TE001 and TC002=TE002 " & _
              " where TE004='" & codeMat & "' and TE011='" & moType & "' " & _
              " and TE012='" & moNo & "' and TE019='Y' " & _
              " order by TE001,TE002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
    End Sub
End Class