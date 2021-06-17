Imports MIS_HTI.FormControl

Public Class MIS_SUB
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lbFile.Text = Request.CurrentExecutionFilePath.ToString
        If Not Page.IsPostBack Then
            login.Text = Session("UserName")
            lbUserGroup.Text = Session("UserGroup")
            Page.Title = "HODS(POP UP)"
            Dim hdCont As New HeaderControl
            ucHeaderPop.HeaderLable = hdCont.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
        UpdateProgress1.DisplayAfter = 100
        UpdateProgress1.Visible = True
        UpdateProgress1.DynamicLayout = True
    End Sub

End Class