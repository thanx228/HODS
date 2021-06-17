Public Class docTypeD
    Inherits System.Web.UI.UserControl
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return ddlDocType
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return ddlDocType.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            ddlDocType.Text = value
        End Set
    End Property

    Public Property text() As String
        Get
            Return ddlDocType.Text.Trim
        End Get
        Set(value As String)
            ddlDocType.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                ddlDocType.Items.Clear()
            Else
                setShowType(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                ddlDocType.Items.Clear()
            Else
                setShowType(value, True)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectFull() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                ddlDocType.Items.Clear()
            Else
                setShowTypeFull(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAllFull() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                ddlDocType.Items.Clear()
            Else
                setShowTypeFull(value, True)
            End If
        End Set
    End Property

    Sub setShowType(val As String, Optional showAll As Boolean = True) 'exam=22,23,22,30
        Dim SQL As String
        SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('" & val.Replace(",", "','") & "') order by MQ001"
        ControlForm.showDDL(ddlDocType, SQL, "MQ002", "MQ001", showAll, Conn_SQL.ERP_ConnectionString)
    End Sub

    Sub setShowTypeFull(val As String, Optional showAll As Boolean = True) 'exam=5102,5104
        Dim SQL As String
        SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 in ('" & val.Replace(",", "','") & "') order by MQ001"
        ControlForm.showDDL(ddlDocType, SQL, "MQ002", "MQ001", showAll, Conn_SQL.ERP_ConnectionString)
    End Sub

End Class