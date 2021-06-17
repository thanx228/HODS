Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web
Public Class POPricesNoInquiry1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TempTable As String = "TempPoNotInquiry" & Session("Username")
        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/POPricesNoInquiry_Rpt.rpt")
        ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        With CrystalReportSource1.ReportDocument
            .SetParameterValue("Stable", TempTable)
            .SetParameterValue("Today", Request("Tdate"))
            .SetParameterValue("Fromday", Request("Fdate"))
        End With
    End Sub

End Class