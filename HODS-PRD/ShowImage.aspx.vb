Public Class ShowImage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fileName As String = Request.QueryString("fileName")
        If fileName <> "" Then
            ImgShow.ImageUrl = Request.QueryString("filePath") & fileName
        Else
            ImgShow.Visible = False
        End If

    End Sub
End Class