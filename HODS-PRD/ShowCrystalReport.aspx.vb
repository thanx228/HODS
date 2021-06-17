Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web
Imports System.IO

Public Class ShowCrystalReport
    Inherits System.Web.UI.Page
    'Planning/WorkNotActualDate.aspx for prototype
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        Dim chrConnect As String = Chr(8)
        Dim dbName As String = Request.QueryString("dbName").ToUpper
        Dim paraName As String = Request.QueryString("paraName")
        Dim fileName As String = Request.QueryString("ReportName")
        Dim encode As String = Request.QueryString("encode")
        Dim chrC As String = ","
        If Not IsNothing(Request.QueryString("chrConn")) Then
            chrC = Request.QueryString("chrConn")
        End If

        If encode = "1" Then
            paraName = Server.UrlDecode(paraName)
            chrC = Server.UrlDecode(chrC)
        End If
        If File.Exists(Server.MapPath("~/App_Code/Print/" & fileName)) Then

        End If

        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/" & fileName)
        Select Case dbName
            Case "MIS" 'main
                ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
            Case "ERP" 'report
                ConfigCrystalReport.SubERP_CRCrystalLogon(CrystalReportSource1.ReportDocument)
                'Case "JPP"
                '    ConfigCrystalReport.SubJPP_CRCrystalLogon(CrystalReportSource1.ReportDocument)
            Case Else
                Exit Sub
        End Select
        'CrystalReportSource1.ReportDocument.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape

        With CrystalReportSource1.ReportDocument
            Dim para1() As String = paraName.Split(chrC)
            Dim allVal As String = ""
            For Each allVal In para1
                If allVal <> "" Then
                    Dim para2() As String = allVal.Split(":")
                    Dim val As String = para2(1)

                    If para2.Count = 3 Then
                        If para2(2) = "1" Then
                            val = "'" & val & "'"
                        End If
                    End If

                    .SetParameterValue(para2(0), val)
                End If
            Next
        End With
    End Sub
End Class