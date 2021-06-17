Public Class docTypeC
    Inherits System.Web.UI.UserControl
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                'Dim SQL As String
                'SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
                'ControlForm.showCheckboxList(cblSoType, SQL, "MQ002", "MQ001", 4, Conn_SQL.ERP_ConnectionString)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cblDocType
        End Get
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            If value = "" Then
                cblDocType.Items.Clear()
            Else
                Dim SQL As String
                SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('" & value.Replace(",", "','") & "') order by MQ002"
                ControlForm.showCheckboxList(cblDocType, SQL, "MQ002", "MQ001", 4, Conn_SQL.ERP_ConnectionString)
            End If
        End Set
    End Property

End Class