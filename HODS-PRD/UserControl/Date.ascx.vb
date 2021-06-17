Imports MIS_HTI.DataControl

Public Class _Date
    Inherits System.Web.UI.UserControl
    Dim dateCont As New DateControl

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

    Public WriteOnly Property TextDate() As Date
        Set(value As Date) 'format date = yyyymmdd=> dd/mm/yyyy
            tbDate.Text = value.ToString("dd/MM/yyyy")
        End Set
    End Property

    'Public Function setText(val As String, OldFormatDate As String, Optional newFormatDate As String = "dd/MM/yyyy")
    '    Return dateCont.ChangeDateFormat(val, OldFormatDate, newFormatDate)
    'End Function

    Public ReadOnly Property TextEmptyToday() As String
        Get
            Return dateCont.dateFormat2(tbDate.Text.Trim,, True)
        End Get
    End Property

    Public Property DateValDefault() As String
        Get
            Return tbDate.Text.Trim
        End Get
        Set(ByVal value As String)
            tbDate.Text = value
        End Set
    End Property
End Class