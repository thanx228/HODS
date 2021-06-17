Public Class ERPUserPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim grpCode As String = Request.QueryString("grpCode").ToString.Trim
        Dim User As String = Request.QueryString("User").ToString.Trim
        lbGrp.Text = grpCode
        Dim SQL As String

        SQL = " select MG002 as 'Procedure',MB011 as 'Procedure Name',SUBSTRING(MG006,9,1) as 'New',SUBSTRING(MG006,1,1) as 'Query',SUBSTRING(MG006,2,1) as 'Alter', " & _
              " SUBSTRING(MG006,3,1) as 'Delete',SUBSTRING(MG006,4,1) as 'Approve',SUBSTRING(MG006,5,1) as 'De-App' ," & _
              " SUBSTRING(MG006,6,1) as 'Export' from ADMMG " & _
              " left join DSCSYS80.dbo.ADMMB on MB001=MG002 " & _
              " where MG001='**********' and MG009='" & grpCode & "' order by MG002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountSO.Text = ControlForm.rowGridview(gvShow)


        SQL = " select MG002 as 'Procedure',MB011 as 'Procedure Name',SUBSTRING(MG006,9,1) as 'New',SUBSTRING(MG006,1,1) as 'Query',SUBSTRING(MG006,2,1) as 'Alter', " & _
             " SUBSTRING(MG006,3,1) as 'Delete',SUBSTRING(MG006,4,1) as 'Approve',SUBSTRING(MG006,5,1) as 'De-App' ," & _
             " SUBSTRING(MG006,6,1) as 'Export' from ADMMG " & _
             " left join DSCSYS80.dbo.ADMMB on MB001=MG002 " & _
             " where MG001<>'**********' and MG001='" & User & "' order by MG002 "
        ControlForm.ShowGridView(gvUser, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountUser.Text = ControlForm.rowGridview(gvUser)




    End Sub
End Class