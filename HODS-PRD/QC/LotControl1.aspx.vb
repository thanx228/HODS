Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class LotControl1
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

        'txttype.Text = Request.QueryString("Type")
        'txtno.Text = Request.QueryString("No")
        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/LotControlSheet.rpt")
        ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        With CrystalReportSource1.ReportDocument
            '.SetParameterValue("Type", Request("Type"))
        End With

    End Sub

    Private Sub showReport()    'Create Barcode 2D

        Dim strConn As String = "Data Source=192.168.50.1;Initial Catalog=DBMIS;User ID=sa;Password=Alex0717"
        Using Conn As New System.Data.SqlClient.SqlConnection(strConn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim ds As New DataSet1
            Dim RD As DataSet1TableAdapters.LotTableAdapter = New DataSet1TableAdapters.LotTableAdapter
            RD.Fill(ds.Lot)
            ds.Tables(0).Columns.Add(New DataColumn("Barcode5", GetType(Byte())))

            Dim barcode As KeepAutomation.Barcode.Crystal.BarCode = New KeepAutomation.Barcode.Crystal.BarCode()
            barcode.Symbology = KeepAutomation.Barcode.Symbology.QRCode

            For Each dr As DataRow In ds.Tables(0).Rows
                '    Dim Type As String = dr.Item(0).ToString.Replace(" ", "")
                '    Dim No As String = dr.Item(1).ToString.Replace(" ", "")
                '    'ds.Tables(0).Rows(0).ItemArray(7).ToString().Replace(" ", "")
                '    Dim Bdesc As String = dr.Item(4).ToString.Replace(" ", "")
                '    Dim Bqty As String = " {" & dr.Item(5).ToString.Replace(" ", "")
                '    Dim Bdate As String = " {" & dr.Item(10).ToString.Replace(" ", "")
                '    Dim Bpo As String = " {" & dr.Item(12).ToString.Replace(" ", "")
                '    Dim BSerial As String = " {" & dr.Item(15).ToString.Replace(" ", "")
                Dim Spec As String = dr.Item(12).ToString.Replace(" ", "")

                barcode.CodeToEncode = Spec
                Dim imageData As Byte() = barcode.generateBarcodeToByteArray()
                dr("Barcode") = imageData
            Next

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            rpt.Load(Server.MapPath("~/App_Code/Print/Lot.rpt"))
            rpt.SetDatabaseLogon(strUsr, strPwd, strServer, strDB)
            rpt.SetDataSource(ds)
            Me.Session("ReportSource") = rpt
            CrystalReportViewer1.ReportSource = Me.Session("ReportSource")

        End Using

    End Sub
End Class