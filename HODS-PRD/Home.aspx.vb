Public Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            If Session("UserName") = "" Then
                Response.Redirect("Login.aspx")
            End If
        End If
            
    End Sub

End Class