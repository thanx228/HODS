Public Class FinishedGoodsAgingSummary
    Inherits System.Web.UI.Page
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            ''ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            'ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 3, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("FinishedGoodsAgingSummary" & Session("UserName"), gvShow)
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        '
    End Sub

    
End Class