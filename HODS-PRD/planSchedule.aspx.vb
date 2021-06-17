Public Class planSchedule
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            ControlForm.showDDL(ddlWc, SQL, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString)

        End If

    End Sub

    Protected Sub btCheck_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCheck.Click
        Response.Redirect(Server.UrlPathEncode("Planing/PlanScheduleCheck.aspx?wc=" & ddlWc.Text.Trim & "&plandate="))
    End Sub
End Class