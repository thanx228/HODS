Public Class TestPage
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    'Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
    '    Dim SQL As String = "select TD001,TD002,TD003,UDF01 from COPTD where UDF01<>'' "
    '    Dim Program As New DataTable
    '    Dim USQL As String = ""
    '    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
    '    'For i As Integer = 0 To Program.Rows.Count - 1
    '    '    Dim orderType As String = Program.Rows(i).Item("TD001").ToString
    '    '    Dim orderNo As String = Program.Rows(i).Item("TD002").ToString
    '    '    Dim orderSeq As String = Program.Rows(i).Item("TD003").ToString
    '    '    Dim custPO As String = Program.Rows(i).Item("UDF01").ToString
    '    '    USQL = "update COPTD set TD027='" & custPO & "' where TD001='" & orderType & "' and TD002='" & orderNo & "' and TD003='" & orderSeq & "' "
    '    '    Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
    '    'Next
    '    'USQL = "update COPTD set TD027='' where TD027<>'' "
    '    'Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
    '    Label1.Text = "Update Complete!"
    'End Sub
    'style="display: none"

    Protected Sub btnPopup_Click(sender As Object, e As EventArgs) Handles btnPopup.Click
        SqlDataSource1.SelectCommand = "SELECT * FROM [Department] where Dept='IT'"
        SqlDataSource1.DataBind()
    End Sub
End Class