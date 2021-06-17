Public Class SaleOrderNoGenLot
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("Login.aspx")
            End If
        End If
    End Sub

    Protected Sub BtPreview_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtPreview.Click
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('SaleOrderNoGenLot1.aspx?');", True)
    End Sub
End Class