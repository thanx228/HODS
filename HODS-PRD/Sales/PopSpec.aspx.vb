Public Class PopSpec
    Inherits System.Web.UI.Page

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        If txtsearch.Text = "" Then
            ' show_message.ShowMessage(Page, "Please Insert Data", UpdatePanel1)
            Me.GridSpec.DataBind()
        ElseIf txtsearch.Text <> "" Then
            If DDLSearch.SelectedIndex = 0 Then
                SqlDataSource1.SelectCommand = "select MB001,MB002,MB003 from INVMB where MB109 = 'Y' and MB001 like '" & txtsearch.Text & "%'"

                Me.GridSpec.DataBind()
            ElseIf DDLSearch.SelectedIndex = 1 Then
                SqlDataSource1.SelectCommand = "select MB001,MB002,MB003 from INVMB where MB109 = 'Y' and MB002 like '%" & txtsearch.Text & "%'"

                Me.GridSpec.DataBind()
            ElseIf DDLSearch.SelectedIndex = 2 Then
                SqlDataSource1.SelectCommand = "select MB001,MB002,MB003 from INVMB where MB109 = 'Y' and MB003 like '%" & txtsearch.Text & "%'"

                Me.GridSpec.DataBind()
            End If

        End If
    End Sub
   
    Private Sub GridSpec_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridSpec.RowDataBound

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            DirectCast(e.Row.FindControl("Button1"), Button).Attributes.Add("onclick", "javascript:GetRowValue('" & e.Row.Cells(1).Text & "','" & e.Row.Cells(2).Text & "','" & e.Row.Cells(3).Text & "')")

            'DirectCast(e.Row.FindControl("Button"), ImageButton).Attributes.Add("onclick", "javascript:GetRowValue('" & e.Row.Cells(1).Text & "')")

            'DirectCast(e.Row.FindControl("Image"), ImageButton).Attributes.Add("onclick", "javascript:GetRowValue('" & e.Row.Cells(1).Text & "')")

            ','" & e.Row.Cells(2).Text & "','" & e.Row.Cells(3).Text & "'
        End If

    End Sub
End Class