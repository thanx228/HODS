Public Class DailyMachinePlanNew
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim listType As String = "'5102','5104','5106','5108','5201','5202','5301','5109','5203','5194','5199'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 in ('W01','W02','W07','W12','W19','W23','W25','W27')
            ControlForm.showCheckboxList(cblWC, SQL, "MD002", "MD001", 5, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 in (" & listType & ") order by MQ002"
            ControlForm.showCheckboxList(cblType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False

        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = ""

        WHR = WHR & Conn_SQL.Where("SFC.TA006", cblWC)
        WHR = WHR & Conn_SQL.Where("SFC.TA001", cblType, False, listType)
        WHR = WHR & Conn_SQL.Where("MOC.TA006", tbItem)
        WHR = WHR & Conn_SQL.Where("MOC.TA035", tbSpec)
        WHR = WHR & Conn_SQL.Where("TB003", tbMatItem)
        WHR = WHR & Conn_SQL.Where("TB013", tbMatSpec)
        WHR = WHR & Conn_SQL.Where("SFC.TA008", configDate.dateFormat2(tbDateFM.Text.Trim), configDate.dateFormat2(tbDateTO.Text.Trim))
        If ddlCon.Text = "1" Then 'on process
            WHR = WHR & " and SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058>0 "
        ElseIf ddlCon.Text = "" Then 'pending
            WHR = WHR & " and SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058=0 "
        End If
        SQL = " select MA002 F01,MD002 F02,SFC.TA001+'-'+SFC.TA002 F03,MOC.TA006 F04,MOC.TA035 F05,TB003 F06,TB012 F07,TB013 F08, " & _
              " (SUBSTRING(SFC.TA008,7,2)+'/'+SUBSTRING(SFC.TA008,5,2)+'/'+SUBSTRING(SFC.TA008,1,4)) F09," & _
              " TB004 F10,TB004-TB005 F11,TB007 F12,MOC.TA015 F13,SFC.TA011 F16," & _
              " SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058 F14," & _
              " case when SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058=0 then 'Pending' else 'On Process' end  F15 " & _
              " from SFCTA SFC left join MOCTA MOC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " & _
              " left join MOCTB on TB001=MOC.TA001 and TB002=MOC.TA002 " & _
              " left join COPTC on TC001=MOC.TA026 and TC002=MOC.TA027 " & _
              " left join COPMA on MA001=TC004 " & _
              " left join CMSMD on MD001= SFC.TA006 " & _
              " where MOC.TA013='Y' and MOC.TA011 not in('y','Y') " & _
              " and MOC.TA015+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058 >0 " & WHR & _
              " order by SFC.TA008,SFC.TA001,SFC.TA002"
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub
    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("DailyMchPlan" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

End Class