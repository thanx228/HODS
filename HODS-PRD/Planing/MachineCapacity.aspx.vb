Public Class MachineCapacity
    Inherits System.Web.UI.Page
    Dim CreateTable As New CreateTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateTable.CreateMachineCapacityTable()
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub
    Protected Sub btnUpdateMch_Click(sender As Object, e As EventArgs) Handles btnUpdateMch.Click
        Dim Program As New DataTable
        Dim SQL As String = ""
        SQL = "select MD001 from CMSMD"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            SQL = " if not exists (select * from MachineCapacity where wc='" & Program.Rows(i).Item("MD001") & "')" & _
                  " insert into  MachineCapacity(wc) values ('" & Program.Rows(i).Item("MD001") & "') "
            Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
        Next
        GridView1.DataBind()
    End Sub
End Class