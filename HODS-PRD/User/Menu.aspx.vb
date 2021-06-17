Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataView
Public Class Menu
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim CreateTable As New CreateTable
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CreateTable.CreateMenuTable()
            CreateTable.CreateUserGroupTable()
            MenuGridView.PageIndex = 20
            ShowGrid()
        End If
    End Sub

    Friend Shared Function PARENT_ID() As Object
        Throw New NotImplementedException()
    End Function

    Private Sub ShowGrid()

        Dim SelSQL As String = "select * from Menu where Line>0 "
        Dim da As New SqlDataAdapter(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim ds As New DataSet
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            MenuGridView.DataSource = ds
            MenuGridView.DataBind()

        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            MenuGridView.DataSource = ds
            MenuGridView.DataBind()
            Dim ColumnCount As Integer = MenuGridView.Rows(0).Cells.Count
            MenuGridView.Rows(0).Cells.Clear()
            MenuGridView.Rows(0).Cells.Add(New TableCell())
            MenuGridView.Rows(0).Cells(0).ColumnSpan = ColumnCount
            MenuGridView.Rows(0).Cells(0).Text = "No Record Found"
        End If
    End Sub
    Private Sub ShowGrid1(ByVal scode As String, ByVal sline As String, ByVal sname As String, ByVal sprog As String)
        Dim Whr As String = "where Id > 0"
        If scode <> "" Then
            Whr += " and  ParentId='" & scode & "'"
        End If
        If sline <> "" Then
            Whr += " and Line='" & sline & "'"
        End If
         If sname <> "" Then
            Whr += " and Name like'%" & sname & "%'"
        End If
        If sprog <> "" Then
            Whr += " and Prog like'%" & sprog & "%'"
        End If
        Dim SelSQL As String = "select * from Menu " & Whr & ""
        Dim da As New SqlDataAdapter(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim ds As New DataSet
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            MenuGridView.DataSource = ds
            MenuGridView.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            MenuGridView.DataSource = ds
            MenuGridView.DataBind()
            Dim ColumnCount As Integer = MenuGridView.Rows(0).Cells.Count
            MenuGridView.Rows(0).Cells.Clear()
            MenuGridView.Rows(0).Cells.Add(New TableCell())
            MenuGridView.Rows(0).Cells(0).ColumnSpan = ColumnCount
            MenuGridView.Rows(0).Cells(0).Text = "No Record Found"
        End If
    End Sub


    Protected Sub MenuGridView_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        MenuGridView.EditIndex = e.NewEditIndex
        ShowGrid()
    End Sub
    Protected Sub MenuGridView_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)

        Dim KeyIdmenu As Integer = Convert.ToInt32(MenuGridView.DataKeys(e.RowIndex).Value.ToString())
        Dim ParentMenu As String = DirectCast(MenuGridView.Rows(e.RowIndex).FindControl("txtcode"), TextBox).Text
        Dim LineMenu As Integer = DirectCast(MenuGridView.Rows(e.RowIndex).FindControl("txtline"), TextBox).Text
        Dim NameMenu As String = DirectCast(MenuGridView.Rows(e.RowIndex).FindControl("txtname"), TextBox).Text
        Dim ProgMenu As String = DirectCast(MenuGridView.Rows(e.RowIndex).FindControl("txtprog"), TextBox).Text
        Dim UpInSQL As String = "Update Menu set ParentId='" & ParentMenu & "',Line='" & LineMenu & "',Name='" & NameMenu & "',Prog='" & ProgMenu & "' where Id='" & KeyIdmenu & "'"
        Conn_SQL.Exec_Sql(UpInSQL, Conn_SQL.MIS_ConnectionString)
        lblresult.ForeColor = Drawing.Color.Green
        lblresult.Text = NameMenu + " Details Updated successfully"
        MenuGridView.EditIndex = -1
        ShowGrid()
    End Sub

    Protected Sub MenuGridView_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        MenuGridView.EditIndex = -1
        ShowGrid()
    End Sub
    Protected Sub MenuGridView_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim KeyIdmenu As Integer = Convert.ToInt32(MenuGridView.DataKeys(e.RowIndex).Value.ToString())
        Dim DelSQL As String = "Delete Menu where Id='" & KeyIdmenu & "'"
        Conn_SQL.Exec_Sql(DelSQL, Conn_SQL.MIS_ConnectionString)
        If Err.Number = 0 Then
            lblresult.ForeColor = Drawing.Color.Red
            lblresult.Text = " Data deleted successfully"
        Else
            lblresult.ForeColor = Drawing.Color.Red
            lblresult.Text = " Data can not delete "
        End If

        ShowGrid()
    End Sub
    Protected Sub MenuGridView_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("AddNew") Then
            Dim ParentMenu As String = DirectCast(MenuGridView.FooterRow.FindControl("txtfcode"), TextBox).Text
            Dim LineMenu As Integer = DirectCast(MenuGridView.FooterRow.FindControl("txtfline"), TextBox).Text
            Dim NameMenu As String = DirectCast(MenuGridView.FooterRow.FindControl("txtfname"), TextBox).Text
            Dim ProgMenu As String = DirectCast(MenuGridView.FooterRow.FindControl("txtfprog"), TextBox).Text
            Dim UpInSQL As String = "Update Menu set ParentId='" & ParentMenu & "',Line='" & LineMenu & "' where ParentId='" & ParentMenu & "' and Line='" & LineMenu & "' and Name='" & NameMenu & "' IF @@ROWCOUNT = 0 Insert into Menu(ParentId,Line,Name,Prog) Values ('" & ParentMenu & "','" & LineMenu & "','" & NameMenu & "','" & ProgMenu & "')"
            Conn_SQL.Exec_Sql(UpInSQL, Conn_SQL.MIS_ConnectionString)
            If Err.Number = 0 Then
                ShowGrid()
                lblresult.ForeColor = Drawing.Color.Green
                lblresult.Text = NameMenu + " inserted successfully"
            Else
                lblresult.ForeColor = Drawing.Color.Red
                lblresult.Text = NameMenu + " can not inserted"
            End If
        End If
        If e.CommandName.Equals("Search") Then
            Session("Sparent") = ""
            Session("SLine") = ""
            Session("SName") = ""
            Session("SProg") = ""
            Dim parentMenu As String = DirectCast(MenuGridView.FooterRow.FindControl("txtfcode"), TextBox).Text
            Dim LineMenu As String = DirectCast(MenuGridView.FooterRow.FindControl("txtfline"), TextBox).Text
            Dim NameMenu As String = DirectCast(MenuGridView.FooterRow.FindControl("txtfname"), TextBox).Text
            Dim ProgMenu As String = DirectCast(MenuGridView.FooterRow.FindControl("txtfprog"), TextBox).Text
            Session("Sparent") = parentMenu
            Session("SLine") = LineMenu
            Session("SName") = NameMenu
            Session("SProg") = ProgMenu
            If Err.Number = 0 Then
                ShowGrid1(parentMenu, LineMenu, NameMenu, ProgMenu)
                lblresult.ForeColor = Drawing.Color.Green
                lblresult.Text = NameMenu + " search successfully"
            Else
                lblresult.ForeColor = Drawing.Color.Red
                lblresult.Text = NameMenu + " can not search"
            End If
        End If
    End Sub
    Protected Sub MenuGridView_PageIndexChanged(ByVal sender As Object, _
            ByVal e As System.EventArgs) Handles MenuGridView.PageIndexChanged
        ShowGrid1(Session("Sparent"), Session("SLine"), Session("SName"), Session("SProg"))
    End Sub
    Protected Sub MenuGridView_PageIndexChanging(ByVal sender As Object, _
           ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles MenuGridView.PageIndexChanging
        MenuGridView.PageIndex = e.NewPageIndex
    End Sub

    Protected Sub MenuGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MenuGridView.SelectedIndexChanged

    End Sub
End Class