Imports MIS_HTI.DataControl

Public Class _DateTextChange
    Inherits System.Web.UI.UserControl
    Dim dateCont As New DateControl

    Public Event ChangeEvent(sender As Object, e As EventArgs)

    Private Sub text_onChange(sender As Object, e As EventArgs) Handles tbDate.TextChanged
        RaiseEvent ChangeEvent(sender, e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    '###### Get format Date ERP เก่า ####################
    Public Property dateVal() As String
        Get
            Return dateCont.dateFormat2(tbDate.Text.Trim)
        End Get
        Set(value As String)
            tbDate.Text = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return dateCont.dateFormat2(tbDate.Text.Trim)
        End Get
        Set(value As String) 'format date = yyyymmdd=> dd/mm/yyyy
            tbDate.Text = dateCont.ChangeDateFormat(value, "yyyyMMdd", "dd/MM/yyyy")
        End Set
    End Property

    Public ReadOnly Property textEmptyToday() As String
        Get
            Return dateCont.dateFormat2(tbDate.Text.Trim,, True)
        End Get
    End Property

    Public Property dateValDefault() As String
        Get
            Return tbDate.Text.Trim
        End Get
        Set(ByVal value As String)
            tbDate.Text = value
        End Set
    End Property
End Class