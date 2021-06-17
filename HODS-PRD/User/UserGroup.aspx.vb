Public Class UserGroup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim CreateTable As New CreateTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CreateTable.CreateUserGroupTable()
            Dim Program As New Data.DataTable
            'ADMME = Group Table in ERP
            Program = Conn_SQL.Get_DataReader("select ME001,(ME001 + ' ' + ME002) as FullName from ADMME Where ME002 <>'' order by ME001 ", Conn_SQL.ERP_ConnectionString)

            For i As Integer = 0 To Program.Rows.Count - 1
                UserGroupDropDownList.Items.Add(New ListItem(Program.Rows(i).Item("FullName"), Program.Rows(i).Item("ME001")))
            Next


            Dim Program2 As New Data.DataTable
            'Menu = Table in DBMIS
            Program2 = Conn_SQL.Get_DataReader("Select Id,ParentId,Name from Menu where ParentId=0 or isParent='Y' order by ParentId,Line ", Conn_SQL.MIS_ConnectionString)
            For i As Integer = 0 To Program2.Rows.Count - 1
                MenuDropDownList.Items.Add(New ListItem(Program2.Rows(i).Item("Name"), Program2.Rows(i).Item("Id")))
            Next
            ShowGrid(MenuDropDownList.SelectedValue)
        End If
    End Sub
    Private Sub ShowGrid(ByVal MainMenu As String)
        Dim UserGroup As String = UserGroupDropDownList.SelectedValue
        Dim Program As New Data.DataTable
        Program = Conn_SQL.Get_DataReader("select * from Menu where ParentId > '0'  and ParentId='" & MainMenu & "'", Conn_SQL.MIS_ConnectionString)
        MenuGridView.DataSource = Program.DefaultView
        MenuGridView.DataBind()
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim Program2 As New Data.DataTable
            Dim IdMenu As String = Program.Rows(i).Item("Id")
            Program2 = Conn_SQL.Get_DataReader("Select * from UserGroup where UserGroup='" & UserGroup & "' and IdMenu='" & IdMenu & "'", Conn_SQL.MIS_ConnectionString)
            If Program2.Rows.Count <> 0 Then
                Dim ChkBox As CheckBox = CType(MenuGridView.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
                ChkBox.Checked = True
            End If
        Next
    End Sub
    Protected Sub MenuGridView_RowDatabound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles MenuGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Style.Add("Width", "30px")
            e.Row.Cells(1).Style.Add("Width", "30px")
            e.Row.Cells(2).Style.Add("Width", "50px")
            e.Row.Cells(3).Style.Add("Width", "50px")
            e.Row.Cells(4).Style.Add("Width", "100px")
            e.Row.Cells(5).Style.Add("Width", "150px")
        End If
    End Sub

    Protected Sub UserGroupDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles UserGroupDropDownList.SelectedIndexChanged
        If UserGroupDropDownList.Items.Count() = 0 Then
            UserGroupDropDownList.Items.Clear()
        Else
            ShowGrid(MenuDropDownList.SelectedValue)
        End If
    End Sub
    Protected Sub MenuDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MenuDropDownList.SelectedIndexChanged
        If MenuDropDownList.Items.Count() = 0 Then
            MenuDropDownList.Items.Clear()
        Else
            ShowGrid(MenuDropDownList.SelectedValue)
        End If
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveButton.Click
        Dim UserGroup As String = UserGroupDropDownList.SelectedValue
        Dim MenuGroup As String = MenuDropDownList.SelectedValue

        For i As Integer = 0 To MenuGridView.Rows.Count - 1
            Dim ChkBox As CheckBox = CType(MenuGridView.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            Dim IdMenu As String = MenuGridView.Rows(i).Cells(1).Text

            If ChkBox.Checked = True Then
                Dim UpInSQL As String = "Update UserGroup set UserGroup='" & UserGroup & "',IdMenu='" & IdMenu & "' where UserGroup='" & UserGroup & "' and IdMenu='" & IdMenu & "' IF @@ROWCOUNT = 0 Insert into UserGroup(UserGroup,IdMenu) Values ('" & UserGroup & "','" & IdMenu & "')"
                Conn_SQL.Exec_Sql(UpInSQL, Conn_SQL.MIS_ConnectionString)
            Else
                Dim DeSQL As String = " Delete from UserGroup where UserGroup='" & UserGroup & "' and IdMenu='" & IdMenu & "'"
                Conn_SQL.Exec_Sql(DeSQL, Conn_SQL.MIS_ConnectionString)
            End If
        Next
    End Sub

End Class