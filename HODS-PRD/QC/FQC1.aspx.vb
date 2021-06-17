Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web

Public Class FQC1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txttype.Text = Request.QueryString("Type")
        txtno.Text = Request.QueryString("No")

        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/Inspection.rpt")
        ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        With CrystalReportSource1.ReportDocument
            '.SetParameterValue("Type", Request("Type"))
            '.SetParameterValue("No", Request("No"))
            '.SetParameterValue("Type", txttype.Text)
            '.SetParameterValue("No", txtno.Text)

        End With

    End Sub
End Class