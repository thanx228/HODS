Public Class testUserControl
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Label3.Text = HeaderForm1.HeaderLable()


        HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePathExtension.ToString)
        'HeaderForm1.HeaderLable = System.IO.Path.GetFileName(HttpContext.Current.Request.PhysicalPath)

        'HeaderForm1.HeaderLable = Request.CurrentExecutionFilePath.ToString
        'docTypeC1.setObject = "51,52"
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Label3.Text = WorkOrder1.moTypeVal
        'Label4.Text = WorkOrder1.moNoVal

    End Sub
End Class