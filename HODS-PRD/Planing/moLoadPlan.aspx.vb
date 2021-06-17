Public Class moLoadPlan
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim selWh As String = "'W03','W04','W05','W09','W13','W14','W15','W16','W17','W20'",
        selTrn As String = "'D201','D202','D204','D205'"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  where MD001 in(" & selWh & ")  order by MD001 "
            ControlForm.showDDL(ddlWorkCenter, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('D2') and MQ001 in (" & selTrn & ") order by MQ002"
            ControlForm.showDDL(ddlTransType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            'SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

    End Sub
End Class