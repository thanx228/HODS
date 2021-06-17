Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web

Public Class BillingReceipts2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/BillingMonth.rpt")
        ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        With CrystalReportSource1.ReportDocument
            '.SetParameterValue("Month", Request("Month"))
            '.SetParameterValue("Year", Request("Year"))
        End With

    End Sub

End Class