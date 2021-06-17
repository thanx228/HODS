Public Class machineD
    Inherits System.Web.UI.UserControl
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Sub setmc(ByVal wc As String, Optional ByVal showAll As Boolean = False)
        If wc = "" Then
            ddlMC.Items.Clear()
        Else
            If wc = "W07" Or wc = "W25" Then
                wc = "W07,W25"
            End If
            Dim SQL As String = "select rtrim(MX001) MX001 from CMSMX where MX002 in ('" & wc.Replace(",", "','") & "') and MX006<>'CANCEL' order by MX001"
            ControlForm.showDDL(ddlMC, SQL, "MX001", "MX001", showAll, Conn_SQL.ERP_ConnectionString)
        End If
    End Sub

    Public WriteOnly Property setObject() As String
        Set(ByVal wc As String)
            setmc(wc, False)
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(ByVal wc As String)
            setmc(wc, True)
        End Set
    End Property

    Public WriteOnly Property setValue() As String
        Set(mc As String)
            ddlMC.Text = mc
        End Set
    End Property

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return ddlMC
        End Get
    End Property

    Public ReadOnly Property getValue() As String
        Get
            Return ddlMC.Text.Trim
        End Get
    End Property
End Class