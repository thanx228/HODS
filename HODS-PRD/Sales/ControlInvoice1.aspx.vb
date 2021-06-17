Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web

Public Class ControlInvoice1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'TextBox1.Text = Request.QueryString("CID")
        'TextBox2.Text = Request.QueryString("fr")
        'TextBox3.Text = Request.QueryString("to")

        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/InvoiceOpen.rpt")
        ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        With CrystalReportSource1.ReportDocument
            '.SetParameterValue("CID", Request("CID"))
            '.SetParameterValue("fr", Request("fr"))
            '.SetParameterValue("to", Request("to"))
        End With
    End Sub

End Class