Public Class shiftD
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public ReadOnly Property getValue() As String
        Get
            Return ddlShift.Text.Trim
        End Get
    End Property

End Class