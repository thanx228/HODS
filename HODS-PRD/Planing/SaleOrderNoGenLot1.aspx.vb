Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web
Public Class SaleOrderNoGenLot1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/SaleOrderNoGenLot_Rpt.rpt")
        'CrystalReportSource1.Report.Parameters =
        ConfigCrystalReport.SubERP_CRCrystalLogon(CrystalReportSource1.ReportDocument)
    End Sub

End Class