Public Class noBomItemBlockPop
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lbCode.Text = Request.QueryString("code")
            lbName.Text = Request.QueryString("name")
            viewData()
            lbCnt.Text = GridView1.Rows.Count
        End If
    End Sub
    Protected Sub viewData()
        Dim code As String = Request.QueryString("code")
        Dim SQL As String = ""
        SQL = " select TD001+'-'+TD002+'-'+TD003 as SaleOrder, " & _
               " (SUBSTRING(TD013,7,2)+'-'+SUBSTRING(TD013,5,2)+'-'+SUBSTRING(TD013,1,4)) as DueDate, " & _
               " cast(TD008-TD009 as decimal(8,0)) as balQty " & _
               " from COPTD where TD016='N' and TD004='" & code & "' "
        ControlForm.ShowGridView(GridView1, SQL, Conn_SQL.ERP_ConnectionString)
    End Sub

 
End Class