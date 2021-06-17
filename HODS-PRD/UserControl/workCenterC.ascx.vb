Public Class workCenterC
    Inherits System.Web.UI.UserControl
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public WriteOnly Property setObject() As String
        Set(ByVal listNotWC As String)
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD where MD001 not in ('" & listNotWC.Replace(",", "','") & "') order by MD001 "
            ControlForm.showCheckboxList(cbl, SQL, "MD002", "MD001", 4, Conn_SQL.ERP_ConnectionString)
        End Set
    End Property
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbl
        End Get
    End Property
End Class