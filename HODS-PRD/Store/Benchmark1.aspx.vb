Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web

Public Class Benchmark1
    Inherits System.Web.UI.Page

    Dim strUsr As String = ""
    Dim strPwd As String = ""
    Dim strServer As String = ""
    Dim strDB As String = ""

    Dim Type As String
    Dim No As String
    Dim Item As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Code/Print/LabelBenchmark.rpt")
        ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        With CrystalReportSource1.ReportDocument

        End With

        'If True Then
        '    strUsr = ""
        '    strPwd = ""
        '    strServer = "192.168.50.1"
        '    strDB = "JINPAO80"

        '    If Me.Session("ReportSource") IsNot Nothing Then
        '        CrystalReportViewer1.ReportSource = Me.Session("ReportSource")
        '    End If
        'End If

        'Type = Request.QueryString("Type")
        'No = Request.QueryString("No")
        'Item = Request.QueryString("Item")

        'showReport()
    End Sub

    Private Sub showReport()

        Dim strConn As String = "Data Source=192.168.50.1;Initial Catalog=JINPAO80;User ID=sa;Password=Alex0717"
        Using Conn As New System.Data.SqlClient.SqlConnection(strConn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim tmpTable As New DataTable()
            'where TA002 ='" & No & "'"
            Dim sql As String = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,REPLACE(TB022,'000000','00') as qty,replace('*'+TA002+'*',' ','') as BarTA002,replace('*'+TB040+'*',' ','') AS BarTB040,replace('*'+TB048+'*',' ','') as BarTB048 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA002='" & No & "' and TA001='" & Type & "' and TB039='" & Item & "'"
            Dim com As New System.Data.SqlClient.SqlCommand(sql, Conn)
            Dim dr As System.Data.SqlClient.SqlDataReader = com.ExecuteReader()
            If dr.HasRows Then
                tmpTable.Load(dr)
            End If
            dr.Close()

            If tmpTable.Rows.Count > 0 Then
                Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
                rpt.Load(Server.MapPath("~/App_Code/Print/LabelBenchmark.rpt"))
                rpt.SetDatabaseLogon(strUsr, strPwd, strServer, strDB)
                rpt.SetDataSource(tmpTable)
                Me.Session("ReportSource") = rpt
                CrystalReportViewer1.ReportSource = Me.Session("ReportSource")

            End If
        End Using
    End Sub

End Class