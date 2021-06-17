Imports System.Data
Imports System.Data.SqlClient

Public Class PR
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If

    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        If txtsearch.Text <> "" Then
            If DDLSearch.SelectedIndex = 0 Then     'Type
                DataSourcePRL.SelectCommand = "select TB001,TB002,TB004,TB005,TB006,TB014,TB015,TB029,TB030,TB032,TB012 from PURTB where TB001='" & txtsearch.Text & "'"
                GridPRL.DataBind()
            ElseIf DDLSearch.SelectedIndex = 1 Then 'No
                DataSourcePRL.SelectCommand = "select TB001,TB002,TB004,TB005,TB006,TB014,TB015,TB029,TB030,TB032,TB012 from PURTB where TB002 = '" & txtsearch.Text & "'"
                GridPRL.DataBind()
            End If
        End If

    End Sub

    Private Sub GridPRHead_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridPRHead.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            TXTHT.Text = GridPRHead.Rows(i).Cells(1).Text.Replace(" ", "")
            TXTHNO.Text = GridPRHead.Rows(i).Cells(2).Text.Replace(" ", "")
            Dept.Text = GridPRHead.Rows(i).Cells(3).Text.Replace(" ", "")
            Issue.Text = GridPRHead.Rows(i).Cells(4).Text.Replace(" ", "")
            Remark.Text = GridPRHead.Rows(i).Cells(5).Text.Replace("&nbsp;", "")

        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        If TXTHT.Text = "" Or TXTHNO.Text = "" Then
            show_message.ShowMessage(Page, "Please Select Data", UpdatePanel1)
            Exit Sub
        End If

        Dim DelLabeleBM As String = "delete from PR"
        Conn_sql.Exec_Sql(DelLabeleBM, Conn_sql.MIS_ConnectionString)

        For M_count As Integer = 0 To GridPRL.Rows.Count - 1
            Dim TB005 As String = GridPRL.Rows(M_count).Cells(3).Text().Replace(" ", "")
            Dim TB006 As String = GridPRL.Rows(M_count).Cells(4).Text().Replace(" ", "")
            Dim MB017 As String = GridPRL.Rows(M_count).Cells(12).Text().Replace(" ", "")
            Dim MB064 As String = GridPRL.Rows(M_count).Cells(13).Text().Replace(" ", "")
            Dim TB014 As String = GridPRL.Rows(M_count).Cells(5).Text().Replace(" ", "")
            Dim TB015 As String = GridPRL.Rows(M_count).Cells(6).Text().Replace(" ", "")
            Dim TB029 As String = GridPRL.Rows(M_count).Cells(7).Text().Replace(" ", "")
            Dim TB030 As String = GridPRL.Rows(M_count).Cells(8).Text().Replace(" ", "")
            Dim TB010 As String = GridPRL.Rows(M_count).Cells(9).Text().Replace(" ", "")
            Dim TB032 As String = GridPRL.Rows(M_count).Cells(10).Text().Replace(" ", "")
            Dim TB012 As String = GridPRL.Rows(M_count).Cells(11).Text().Replace("&nbsp;", "")

            Dim InSQL As String = "Insert into PR(TA001,TA002,TA004,TA013,TA006,TB005,TB006,MB017,MB064,TB014,TB015,TB029,TB030,TB010,TB032,TB012)" ',Mars,PIssue,Bal
            InSQL = InSQL & " Values('" & TXTHT.Text & "','" & TXTHNO.Text & "','" & Dept.Text & "',"
            InSQL = InSQL & "'" & Issue.Text & "','" & Remark.Text & "',"
            InSQL = InSQL & "'" & TB005 & "','" & TB006 & "',"
            InSQL = InSQL & "'" & MB017 & "','" & MB064 & "',"
            InSQL = InSQL & "'" & TB014 & "','" & TB015 & "',"
            InSQL = InSQL & "'" & TB029 & "','" & TB030 & "',"
            InSQL = InSQL & "'" & TB010 & "','" & TB032 & "','" & TB012 & "')"
            Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
        Next
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('PR1.aspx?No=" + TXTHNO.Text + "&Type=" + TXTHT.Text + "&Dept=" + Dept.Text + "');", True)

    End Sub
End Class