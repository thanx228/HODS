Public Class FQCForm
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String =  "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showDDL(ddlType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String,
            WHR As String

        WHR = Conn_SQL.Where("TA001", ddlType)
        WHR = WHR & Conn_SQL.Where("TA002", tbNumber)
        WHR = WHR & Conn_SQL.Where("TA006", tbItem)
        WHR = WHR & Conn_SQL.Where("TA035", tbSpec)
        WHR = WHR & Conn_SQL.Where("TA009", configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))

        SQL = " select TA001,TA002,MF008,MW002, (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) TA003, " & _
              " TA006,TA035,TA015,TA020,TA009,TA026,TA027,TA028," & _
              " case isnull(TC004,'') when '' then '' else  rtrim(TC004)+'-'+MA002 end TC004 from QMSMF " & _
              " left join MOCTA on TA006 = MF002 " & _
              " left join CMSMW on MW001=MF008 " & _
              " left join COPTC on TC001=TA026 and TC002=TA027 " & _
              " left join COPMA on MA001=TC004 " & _
              " where 1=1 " & WHR & _
              " order by TA001,TA002,MF008 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub


    Private Sub gvShow_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShow.RowCommand
        If e.CommandName = "onPrint" Then

            Dim paraName As String = "",
                chrConn As String = ","

            gvShow.Visible = True
            With gvShow.Rows(e.CommandArgument)
                paraName = "MOType:" & .Cells(1).Text.Trim & ",MONo:" & .Cells(2).Text.Trim & ",Operation:" & .Cells(3).Text.Trim
            End With
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=InspectionNew.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & chrConn & "&encode=1');", True)
        End If
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("FQCForm" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class