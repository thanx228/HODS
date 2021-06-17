Public Class OTEmpListDetail
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Dim CreateTempTable As New CreateTempTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SQL As String = ""

        Dim EmpNo As String = Request.QueryString("EmpNo").Trim()


        SQL = "select * from ChangeEmpInfo where EmpNo = '" & EmpNo & "'"

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub

End Class