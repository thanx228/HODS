Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web

Public Class ProcessEfficiency1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        Dim TempTable As String = "TempEfficiency" & Session("UserName") & "2"
        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportViewer1.HasToggleGroupTreeButton = False
        CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None
        CrystalReportViewer1.HasToggleParameterPanelButton = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/EffPercentMC_Rpt.rpt")

        ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        With CrystalReportSource1.ReportDocument
            .SetParameterValue("Stable", TempTable)
            .SetParameterValue("Smonth", Request("Tmonth"))
            .SetParameterValue("Sprocess", Request("Tprocess"))
            .SetParameterValue("Stype", Request("Type"))
            .SetParameterValue("Stime", Request("Ttime"))
            '.SetParameterValue("Machine", Request("Machine"))
        End With

    End Sub
End Class