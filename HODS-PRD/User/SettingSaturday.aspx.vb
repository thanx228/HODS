Public Class SettingSaturday
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click

        Dim sql As String = ""
        sql = "insert into NormalSatOT (DateSat) values('" & Date1.dateValDefault.Trim & "')"
        Conn_SQL.Exec_Sql(sql, Conn_SQL.MIS_ConnectionString)
        gvShow.DataBind()
        show_message.ShowMessage(Page, "บันทึกรายการเรียบร้อย", UpdatePanel1)
    End Sub

    Private Sub gvShow_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShow.RowCommand

        If e.CommandName = "OnDelete" Then
            Dim i As Integer = e.CommandArgument
            Dim strSqlup As String = "delete NormalSatOT where Id = '" & gvShow.Rows(i).Cells(0).Text.Trim & "'"
            Conn_SQL.Exec_Sql(strSqlup, Conn_SQL.MIS_ConnectionString)
            gvShow.DataBind()
        End If

    End Sub

    Private Function getId() As String
        Dim Id As String = ""
        Dim SQL As String = "select max(Id) as Id  from NormalSatOT "
        Dim Program As New Data.DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If Program.Rows.Count > 0 Then
            Id = CDec(Program.Rows(0).Item("Id")) + 1
        Else
            Id = "1"
        End If
        Return Id
    End Function

End Class