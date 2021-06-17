Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web
Imports System.IO

Public Class PoDelivery
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BuReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuReport.Click

        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("\Purchase\ReportPo.rpt")
        Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)

        Dim FNumday As Integer = DDLDate.SelectedValue
        Dim dtNow As DateTime = DateTime.Now
        Dim Todate As String = Format(dtNow, "yyyyMMdd")


        With CrystalReportSource1.ReportDocument
            .SetParameterValue("NumDay", FNumday)
            .SetParameterValue("ToDay", Todate)
        End With

    End Sub

    Public Shared Sub Sub_CRCrystalLogon(ByRef rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)

        ''CrystalReport1' must be the name the CrystalReport
        Dim crTableLogOnInfo As TableLogOnInfo = New TableLogOnInfo
        Dim crConnectionInfo As ConnectionInfo = New ConnectionInfo

        'Crystal Report Properties
        Dim crDatabase As CrystalDecisions.CrystalReports.Engine.Database
        Dim crTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crTable As CrystalDecisions.CrystalReports.Engine.Table

        'Then, use following code sample to add the logic in the Page_Load method of your Web Form:
        crConnectionInfo.ServerName = "192.168.50.1"
        crConnectionInfo.DatabaseName = "JPDEMO80"
        crConnectionInfo.UserID = "sa"
        crConnectionInfo.Password = "Alex0717"
        crDatabase = rpt.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)
        Next

    End Sub

End Class