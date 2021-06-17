Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web
Imports System.Data
Imports System.Data.SqlClient

Imports KeepAutomation

Public Class Delta1
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL

    Dim strUsr As String = ""
    Dim strPwd As String = ""
    Dim strServer As String = ""
    Dim strDB As String = ""

    Dim Type As String
    Dim SNo As String
    Dim Item As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        'CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        'CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/LabelDelta.rpt")
        'ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        'With CrystalReportSource1.ReportDocument
        '    .SetParameterValue("No", Request("No"))
        '    .SetParameterValue("Qty", Request("Qty"))
        '    .SetParameterValue("Item", Request("Item"))
        'End With

        'Type = Request.QueryString("Type").Replace(" ", "")
        SNo = Request.QueryString("SNo").Replace(" ", "")
        Item = Request.QueryString("Item").Replace(" ", "")

        showReport()
    End Sub

    Private Sub showReport()
        Dim strConn As String = "Data Source=192.168.50.1;Initial Catalog=DBMIS;User ID=sa;Password=Alex0717"
        Using Conn As New System.Data.SqlClient.SqlConnection(strConn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim ds As New DataSet1
            Dim RD As DataSet1TableAdapters.LabelDELTATableAdapter = New DataSet1TableAdapters.LabelDELTATableAdapter
            RD.Fill(ds.LabelDELTA, SNo)
            ds.Tables(0).Columns.Add(New DataColumn("Barcode5", GetType(Byte())))

            Dim barcode As KeepAutomation.Barcode.Crystal.BarCode = New KeepAutomation.Barcode.Crystal.BarCode()
            barcode.Symbology = KeepAutomation.Barcode.Symbology.QRCode

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim Type As String = dr.Item(0).ToString.Replace(" ", "")
                Dim No As String = dr.Item(1).ToString.Replace(" ", "")
                'ds.Tables(0).Rows(0).ItemArray(7).ToString().Replace(" ", "")
                Dim Bdesc As String = dr.Item(4).ToString.Replace(" ", "")
                Dim Bqty As String = " {" & dr.Item(5).ToString.Replace(" ", "")
                Dim Bdate As String = " {" & dr.Item(10).ToString.Replace(" ", "")
                Dim Bpo As String = " {" & dr.Item(12).ToString.Replace(" ", "")
                Dim BSerial As String = " {" & dr.Item(15).ToString.Replace(" ", "")
                Dim BUnit As String = " {pce"
                Dim BSup As String = " {556400"
                Dim BTradcode As String = " {02"
                Dim BInvoiceNo As String = " {" & Type & No

                barcode.CodeToEncode = Bdesc & Bqty & BUnit & BSup & Bdate & BTradcode & Bpo & BInvoiceNo & BSerial
                Dim imageData As Byte() = barcode.generateBarcodeToByteArray()
                dr("Barcode") = imageData
            Next

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            rpt.Load(Server.MapPath("~/App_Code/Print/LabelDelta.rpt"))
            rpt.SetDatabaseLogon(strUsr, strPwd, strServer, strDB)
            rpt.SetDataSource(ds)
            Me.Session("ReportSource") = rpt
            CrystalReportViewer1.ReportSource = Me.Session("ReportSource")

        End Using
    End Sub
End Class