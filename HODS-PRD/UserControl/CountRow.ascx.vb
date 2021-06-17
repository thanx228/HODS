Public Class CountRow
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public WriteOnly Property RowCount() As String
        Set(value As String)
            lbCount.Text = value
        End Set
    End Property

    Public WriteOnly Property SetName() As String
        Set(value As String)
            lbName.Text = value
        End Set
    End Property

    Public ReadOnly Property returnRow() As Integer
        Get
            Dim valTemp As String = lbCount.Text.Trim.Replace(",", "")
            Return If(IsNumeric(valTemp), CInt(valTemp), 0)
        End Get
    End Property

    Public ReadOnly Property value() As Integer
        Get
            Dim valTemp As String = lbCount.Text.Trim.Replace(",", "")
            Return If(IsNumeric(valTemp), CInt(valTemp), 0)
        End Get
    End Property
End Class