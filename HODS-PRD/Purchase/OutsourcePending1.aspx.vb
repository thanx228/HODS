Public Class OutsourcePending1
    Inherits System.Web.UI.Page
    Dim configDate As New ConfigDate
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim outsTrnType As String = "'D203','D204','D205','D206','D209'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 in(" & outsTrnType & ") order by MQ001"
            ControlForm.showCheckboxList(cblTrnType, SQL, "MQ002", "MQ001", 2, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showDDL(ddlMoType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = ""
        Dim WHR As String = ""

        WHR = WHR & Conn_SQL.Where("SFCTC.TC004", ddlMoType)
        WHR = WHR & Conn_SQL.Where("SFCTC.TC005", tbMoNo)
        WHR = WHR & Conn_SQL.Where("MOCTA.TA006", tbItem)
        WHR = WHR & Conn_SQL.Where("MOCTA.TA035", tbSpec)
        WHR = WHR & Conn_SQL.Where("SFCTC.TC001", cblTrnType)

        Dim sup As String = tbSup.Text.ToString
        If sup <> "" Then
            WHR = WHR & " and (SFCTB.TB005='" & sup & "' or SFCTB.TB008='" & sup & "') "
        Else
            WHR = WHR & " and (SFCTB.TB004='2' or SFCTB.TB007='2') "
        End If

        Dim status As String = ddlStatus.Text.ToString
        If status <> "A" Then
            WHR = WHR & " and SFCTB.TB013='" & status & "'  "
        End If

        WHR = WHR & configDate.DateWhere("SFCTB.TB003", configDate.dateFormat2(tbDateFrom.Text.ToString), configDate.dateFormat2(tbDateTo.Text.ToString))

        SQL = " select case when len(MOCTA.TA006)=16 then (SUBSTRING(MOCTA.TA006,1,14)+'-'+SUBSTRING(MOCTA.TA006,15,2)) else MOCTA.TA006 end  as 'Item Code',  " &
            " MOCTA.TA035 as 'Item Spec',SFCTC.TC004+'-'+SFCTC.TC005 as 'MO', cast(MOCTA.TA015 as decimal(16,2)) as 'Plan Qty',   " &
            " SFCTC.TC001+'-'+SFCTC.TC002+'-'+SFCTC.TC003 as 'Transfer Detail',   " &
            " (SUBSTRING(SFCTB.TB003,7,2)+'-'+SUBSTRING(SFCTB.TB003,5,2)+'-'+SUBSTRING(SFCTB.TB003,1,4)) as 'Transfer Date',   " &
            " cast(SFCTC.TC036 as decimal(15,2)) as 'Transfer Qty', cast((SFCTC.TC014+SFCTC.TC016+SFCTC.TC052+SFCTC.TC037) as decimal(15,2)) as 'Receipt Qty', " &
            " cast(SFCTC.TC036-(SFCTC.TC014+SFCTC.TC016+SFCTC.TC052+SFCTC.TC037) as decimal(15,2))as 'Not Receipt Qty' , " &
            " cast(SFCTC.TC014 as decimal(15,2)) as 'Accepted Qty', " &
            " cast(SFCTC.TC016 as decimal(15,2)) as 'Scrap Qty', " &
            " cast(SFCTC.TC052 as decimal(15,2)) as 'Destroyed Qty', " &
            " cast(SFCTC.TC037 as decimal(15,2)) as 'Ins. Return Qty', " &
            " SFCTB.TB005 as 'Transfer From', (select top 1 SFCTA.TA007 from SFCTA where SFCTA.TA006=SFCTB.TB005) as 'Transfer From Desc.',  " &
            " SFCTB.TB008 as 'Transfer To',(select top 1 SFCTA.TA007 from SFCTA where SFCTA.TA006=SFCTB.TB008) as 'Transfer To Desc.',  " &
            " cast(MOCTA.TA017 as decimal(16,2)) as 'MO Comp. Qty', cast(MOCTA.TA016 as decimal(16,2)) as 'MO Scrap Qty',  " &
            " case when MOCTA.TA027= '' then '' else MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 end as 'SO',  " &
            " COPTC.TC012 as 'Cust. Order'  ,rtrim(COPTC.TC004) as 'Customer',COPMA.MA002 as 'Customer Name',    " &
            " SFCTC.TC006 as 'Issue Operation',SFCTC.TC008 as 'Receipt Operation',     " &
            " (SUBSTRING(SFCTB.TB015,7,2)+'-'+SUBSTRING(SFCTB.TB015,5,2)+'-'+SUBSTRING(SFCTB.TB015,1,4)) as 'Receipt Date',   " &
            " SFCTB.TB013 as 'Transfer Status', rtrim(SFCTA.TA006)+'-'+SFCTA.TA007 as 'WC',CMSMW.MW002 as 'Process Name',   " &
            " SFCTB.TB014 as 'Remark(INV)',cast(SFCTA.TA013 as decimal(16,2)) as 'Return Qty(+)', " &
            " cast(SFCTA.TA014 as decimal(16,2)) as 'Finish Return Qty(-)', " &
            " cast(SFCTA.TA012+SFCTA.TA056 as decimal(16,2)) as 'Scrap Qty(-)',   " &
            " case MOCTA.TA011 when '1' then 'Have not Manufactured'    " &
            " when '2' then 'Materials Not Issue'    " &
            " when '3' then 'Manufacturing' " &
            " else 'Finished' end as 'MO Status' " &
            " from SFCTC   " &
            " left join SFCTB on SFCTB.TB001 = SFCTC.TC001 and SFCTB.TB002 = SFCTC.TC002   " &
            " left join MOCTA on MOCTA.TA001 = SFCTC.TC004 and MOCTA.TA002 = SFCTC.TC005   " &
            " left join SFCTA on SFCTA.TA001 = SFCTC.TC004 and SFCTA.TA002 = SFCTC.TC005 and SFCTA.TA003=SFCTC.TC008   " &
            " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027   " &
            " left join COPMA on COPMA.MA001 = COPTC.TC004  " &
            " left join CMSMW on CMSMW.MW001 = SFCTA.TA004  " &
            " where SFCTC.TC001 in (" & outsTrnType & ") and SFCTC.TC036 <> (SFCTC.TC014+SFCTC.TC016+SFCTC.TC052+SFCTC.TC037) " & WHR &
            " order by SFCTC.TC001,SFCTC.TC002,SFCTC.TC003"

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("OutsourcePending" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

End Class