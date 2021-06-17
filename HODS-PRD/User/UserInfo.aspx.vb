
Imports System.Data
Imports System.Data.SqlClient
Public Class UserInfo
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ConForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            ClearData()
        End If
    End Sub

    Private Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "OnEdit" Then
            Dim i As Integer = e.CommandArgument
            txtid.Text = GridView2.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
            txtuser.Text = GridView2.Rows(i).Cells(4).Text.Replace(" ", "")
            txtpassword.Text = GridView2.Rows(i).Cells(5).Text.Replace(" ", "")
            txtname.Text = GridView2.Rows(i).Cells(6).Text.Replace(" ", "")
            RBSex.SelectedValue = GridView2.Rows(i).Cells(7).Text.Replace(" ", "")
            DDLGroup.Text = GridView2.Rows(i).Cells(8).Text.Trim
            DDLdept.Text = GridView2.Rows(i).Cells(9).Text.Trim
        End If
    End Sub

    Protected Sub BuSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSave.Click

        If txtuser.Text = "" Then
            show_message.ShowMessage(Page, "User ID is Null!!!", UpdatePanel1)
            txtuser.Focus()
            Exit Sub
        End If
        If txtpassword.Text = "" Then
            show_message.ShowMessage(Page, "Password is Null!!!", UpdatePanel1)
            txtpassword.Focus()
            Exit Sub
        End If
        If txtname.Text = "" Then
            show_message.ShowMessage(Page, "User Name is Null!!!", UpdatePanel1)
            txtname.Focus()
            Exit Sub
        End If

        Dim strSql As String

        If txtid.Text = "" Then
            strSql = "insert into UserInfo (UserName,UserPassWord,NameSurname,Dept,UserGroup,Sex) values('" & txtuser.Text & "','" & txtpassword.Text & "','" & txtname.Text & "','" & DDLdept.SelectedValue & "','" & DDLGroup.SelectedValue & "','" & RBSex.SelectedValue & "')"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        ElseIf txtid.Text <> "" Then
            Dim strSqlup As String = "update UserInfo set UserName='" & txtuser.Text & "', UserPassWord='" & txtpassword.Text & "', NameSurname='" & txtname.Text & "', Dept='" & DDLdept.SelectedValue & "', UserGroup='" & DDLGroup.SelectedValue & "', Sex='" & RBSex.SelectedValue & "' where Id='" & txtid.Text & "'"
            Conn_SQL.Exec_Sql(strSqlup, Conn_SQL.MIS_ConnectionString)
        End If

        GridView2.DataBind()
        ClearData()
        Busearch_Click(sender, e)
    End Sub

    Private Sub ClearData()

        txtid.Text = ""
        txtname.Text = ""
        txtpassword.Text = ""
        txtuser.Text = ""
        DDLdept.SelectedIndex = 0
        RBSex.SelectedValue = "M"

        Dim SQL As String = "select rtrim(ME001) ME001,ME001+':'+ME002 ME002 from CMSME where len(ME001)=3 order by ME001 "
        ConForm.showDDL(DDLdept, SQL, "ME002", "ME001", True, Conn_SQL.ERP_ConnectionString)

        SQL = "Select rtrim(ME001) ME001,rtrim(ME001)+':'+rtrim(ME002) ME002 from ADMME order by ME001"
        ConForm.showDDL(DDLGroup, SQL, "ME002", "ME001", False, Conn_SQL.ERP_ConnectionString)

    End Sub

    Protected Sub Busearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busearch.Click

        If txtsearch.Text <> "" Then
            SqlDataSource1.SelectCommand = "Select * from UserInfo where " & DDLSearch.Text & " like '%" & txtsearch.Text & "%'"
            GridView2.DataBind()

        ElseIf txtsearch.Text = "" Then
            txtsearch.Focus()
            GridView2.DataBind()
        End If

    End Sub

    Private Sub GridView2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
            If Not IsNothing(hplDetail) And Not IsDBNull(e.Row.DataItem("Id")) Then
                Dim link As String = ""
                link = link & "&ID= " & e.Row.DataItem("Id").ToString.Trim
                link = link & "&User= " & e.Row.DataItem("UserName").ToString.Trim
                link = link & "&UserName= " & e.Row.DataItem("NameSurname").ToString.Trim
                hplDetail.NavigateUrl = "UserInfoPop.aspx?height=150&width=350" & link
                hplDetail.Attributes.Add("title", e.Row.DataItem("NameSurname"))
            End If
            Dim hplDetail2 As HyperLink = CType(e.Row.FindControl("hplShow2"), HyperLink)
            If Not IsNothing(hplDetail2) And Not IsDBNull(e.Row.DataItem("Id")) Then
                Dim link As String = ""
                link = link & "&ID= " & e.Row.DataItem("Id").ToString.Trim
                link = link & "&User= " & e.Row.DataItem("UserName").ToString.Trim
                link = link & "&UserName= " & e.Row.DataItem("NameSurname").ToString.Trim
                hplDetail2.NavigateUrl = "UserInfoPopDaily.aspx?height=150&width=350" & link
                hplDetail2.Attributes.Add("title", e.Row.DataItem("NameSurname"))
            End If
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")

            Dim hplDetail3 As HyperLink = CType(e.Row.FindControl("hplShow3"), HyperLink)
            If Not IsNothing(hplDetail2) And Not IsDBNull(e.Row.DataItem("Id")) Then
                Dim link As String = ""
                link = link & "&ID= " & e.Row.DataItem("Id").ToString.Trim
                link = link & "&User= " & e.Row.DataItem("UserName").ToString.Trim
                link = link & "&UserName= " & e.Row.DataItem("NameSurname").ToString.Trim
                hplDetail3.NavigateUrl = "UserInfoPopDept.aspx?height=150&width=350" & link
                hplDetail3.Attributes.Add("title", e.Row.DataItem("NameSurname"))
            End If
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")

        End If
    End Sub
End Class