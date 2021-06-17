Imports System.Drawing
Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Imports MIS_HTI.UserControl

Public Class PlanScheduleManfOrder
    Inherits System.Web.UI.Page

    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl
    Dim dateCont As New DateControl
    Dim outCont As New OutputControl
    Dim hashCont As New HashtableControl
    Dim wcAuth As New UserAuthorization
    Dim ddlCont As New DropDownListControl
    Dim planCont As New PlanControl
    Dim dtMach As DataTable
    Dim wcPlan As New List(Of String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'load wc plan
            getWcPlan()
            'show mo type 
            UcDdlMoType.showDocType("51,52")

            If wcPlan.Count > 0 Then
                lbPlanDate.Text = outCont.DecodeFrom64(Request.QueryString("plandate"))
                ucDatePlan.Text = lbPlanDate.Text
                lbMoType.Text = outCont.DecodeFrom64(Request.QueryString("motype"))
                UcDdlMoType.Text = lbMoType.Text
                lbMONo.Text = outCont.DecodeFrom64(Request.QueryString("mono"))
                TbMoNo.Text = lbMONo.Text
                lbMoSeq.Text = outCont.DecodeFrom64(Request.QueryString("moseq"))

                ShowMOHead() 'show data head
                showData() 'show gridview 
                'Else
                '    btSave.Visible = False
                '    tbPlan.Visible = False
            End If
            showButton()
            Dim showLable As Boolean = False
            lbPlanQty.Visible = showLable
            lbPlanSetSeq.Visible = showLable
            lbPlanStatus.Visible = showLable
            lbPlanUrgent.Visible = showLable
            lbPlanNote.Visible = showLable
        End If
    End Sub

    Sub getWcPlan()
        wcPlan = New List(Of String)
        Dim SQL As String = ""
        SQL = "select WC from UserPlanAuthority where Id='" & Session("UserId") & "' "
        With New DataRowControl(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            Dim datas() As String = .Text("WC").Split(",")
            For Each data As String In datas
                wcPlan.Add(data)
            Next
        End With

        Return
    End Sub


    Function checkCanPlanDate(dateCheck As String) As Boolean
        getWcPlan()

        Dim planDate As String = dateCont.strToDateTime(dateCheck, "yyyyMMdd").AddDays(1).ToString("yyyyMMdd") & "0800"
        Dim dateToday1 As String = DateTime.Now.ToString("yyyyMMddHHmm", New CultureInfo("en-US"))

        Dim DatePlanAvailiable As Boolean = False
        If planDate >= dateToday1 Then
            DatePlanAvailiable = True
        End If
        Return DatePlanAvailiable
    End Function


    Private Sub showButton()
        Dim DatePlanAvailiable As Boolean = checkCanPlanDate(lbPlanDate.Text) AndAlso wcPlan.Contains(lbWC.Text)
        'save
        Dim showButtonSave As Boolean = False
        Dim checkRecorded As Boolean = If(outCont.checkNumberic(lbRecordSeq.Text) > 0, True, False)
        Dim checkRecord As Boolean = checkRecorded AndAlso outCont.checkNumberic(lbTransferQty.Text) <= outCont.checkNumberic(lbPlanQty.Text)
        'Dim checkNoRecord As Boolean = String.IsNullOrEmpty(lbre)

        If DatePlanAvailiable AndAlso (checkRecord OrElse checkRecorded = False) Then
            showButtonSave = True
        End If
        btSave.Visible = showButtonSave
        'search
        btSearch.Visible = DatePlanAvailiable

    End Sub

    Sub showData()
        getWcPlan()
        'process DATA
        Dim grpBy As String = ""
        '========== << show all process >> =========
        Dim al As New ArrayList
        Dim fldName As New ArrayList
        Dim colName As New ArrayList
        Dim fldNumber As New ArrayList
        With New ArrayListControl(al)
            .TAL("rtrim(S.TA006)+'-'+rtrim(S.TA007)" & VarIni.C8 & "TA006", "WC/Supplier")
            .TAL("S.TA004+'-'+MW002" & VarIni.C8 & "TA004", "Process")
            .TAL("case S.TA005 when 1 then '' else 'Outs' end" & VarIni.C8 & "TA005", "Outs")
            .TAL("S.TA003" & VarIni.C8 & "MO_SEQ", "MO Seq")
            .TAL("S.TA010" & VarIni.C8 & "INPUT_QTY", "Input Qty", "0")
            .TAL("S.TA011" & VarIni.C8 & "COMP_QTY", "Completed Qty", "0")
            .TAL("S.TA013-S.TA014-S.TA056" & VarIni.C8 & "RE_MO_BAL_QTY", "Re-MO Qty Bal", "0")
            .TAL("S.TA012+S.TA056" & VarIni.C8 & "SCRAP_QTY", "Scrap Qty", "0")
            '.TAL("S.TA014" & VarIni.C8 & "RE_MO_COMP_QTY", "Re-MO Completed Qty", "0")
            '.TAL("S.TA056" & VarIni.C8 & "RE_MO_SCRAP_QTY", "Re-MO Scrap Qty", "0")
            .TAL("S.TA010+S.TA013+S.TA016-S.TA011-S.TA012-S.TA014-S.TA015-S.TA048-S.TA056-S.TA058" & VarIni.C8 & "WIP_QTY", "WIP Qty", "0")
            .TAL("M.TA015+S.TA013+S.TA016-S.TA011-S.TA012-S.TA014-S.TA015-S.TA048-S.TA056-S.TA058" & VarIni.C8 & "PENDING_QTY", "Pending Qty", "0")
            .TAL("isnull(P1.PlanQty,0)" & VarIni.C8 & "PLAN_QTY", "Plan Qty", "0")
            .TAL("isnull(P1.PlanTime,0)" & VarIni.C8 & "PLAN_TIME_PROCESS", "Usage Time", "0")
            .TAL("isnull(P2.PlanQty,0)" & VarIni.C8 & "PLANED_QTY", "Planed Qty", "0")
            .TAL("isnull(P2.cntPlan,0)" & VarIni.C8 & "PLANED_COUNT", "Planed Count", "0")
            .TAL("isnull(TOTAL_CAP,0)" & VarIni.C8 & "TOTAL_CAP", "W/C Cap")
            .TAL("isnull(P3.PlanTime,0)" & VarIni.C8 & "PLAN_TIME", "Usage Cap")
            .TAL("isnull(BOMMF.UDF51,-1)" & VarIni.C8 & "OPERATOR", "Operator", "0")
            .TAL("isnull(V.TC014,0)+isnull(V.TC016,0)" & VarIni.C8 & "TRANSFER_QTY", "Transfer Qty", "0")
            .TAL("isnull(V.TC014_NOT,0)+isnull(V.TC016_NOT,0)" & VarIni.C8 & "TRANSFER_QTY_NOT", "Transfer Qty(Not App)", "0")
            .TAL("isnull(P1.PlanQty,0)-(isnull(V.TC014,0)+isnull(V.TC016,0))" & VarIni.C8 & "PLAN_BAL_QTY", "Plan Bal Qty", "0")
            fldName = .ChangeFormat()
            fldNumber = .ColumnNumber()
            colName = .ChangeFormat(True)
        End With

        Dim strSQL As New SQLString("SFCTA S ", fldName)
        With strSQL
            .setLeftjoin("MOCTA M ",
                            New List(Of String) From {
                            "M.TA001" & VarIni.C8 & "S.TA001",
                            "M.TA002" & VarIni.C8 & "S.TA002"
                            }
                        )
            Dim SQLLeftJoin As String = "select MoType,MoNo,MoSeq,sum(PlanQty) PlanQty,sum(PlanTime) PlanTime,min(PlanDate) PlanDateStart,max(PlanDate) PlanDateEnd,convert(varchar, getdate(), 112) currentDate,count(*) cntPlan from HOOTHAI_REPORT.." & PLANSCHEDULE_T.table & " where PlanStatus='P' and PlanDate_REPLACE_convert(varchar, getdate(), 112) GROUP BY MoType,MoNo,MoSeq"

            .setLeftjoin(.addBracket(SQLLeftJoin.Replace("_REPLACE_", ">=")) & " P1 ",
                            New List(Of String) From {
                            "P1.MoType" & VarIni.C8 & "S.TA001",
                            "P1.MoNo" & VarIni.C8 & "S.TA002",
                            "P1.MoSeq" & VarIni.C8 & "S.TA003"
                            }
                            )
            .setLeftjoin(.addBracket(SQLLeftJoin.Replace("_REPLACE_", "<")) & " P2 ",
                            New List(Of String) From {
                            "P2.MoType" & VarIni.C8 & "S.TA001",
                            "P2.MoNo" & VarIni.C8 & "S.TA002",
                            "P2.MoSeq" & VarIni.C8 & "S.TA003"
                            }
                            )

            SQLLeftJoin = .addBracket("Select MX002,SUM((CMSMX.UDF51+UDF52)*CMSMX.UDF53) TOTAL_CAP from CMSMX GROUP BY MX002") & " CMSMX "
            .setLeftjoin(SQLLeftJoin, New List(Of String) From {"CMSMX.MX002" & VarIni.C8 & "S.TA006"})

            SQLLeftJoin = .addBracket("select WorkCenter, PlanDate ,sum(PlanTime) PlanTime from HOOTHAI_REPORT.." & PLANSCHEDULE_T.table & " where PlanStatus='P' and PlanDate='" & lbPlanDate.Text & "'  GROUP BY WorkCenter, PlanDate") & " P3 "
            .setLeftjoin(SQLLeftJoin, New List(Of String) From {"P3.WorkCenter" & VarIni.C8 & "S.TA006"})

            .setLeftjoin("CMSMW", New List(Of String) From {"MW001" & VarIni.C8 & "S.TA004"})
            .setLeftjoin("BOMMF", New List(Of String) From {
                         "BOMMF.MF001" & VarIni.C8 & "M.TA006",
                         "BOMMF.MF002" & VarIni.C8 & "01" & VarIni.C8,
                         "BOMMF.MF003" & VarIni.C8 & "S.TA003"
                         })

            .setLeftjoin(VarIni.DBMIS & "..V_TRANSFER_SUM_DATE V", New List(Of String) From {
                         "V.TC004" & VarIni.C8 & "S.TA001",
                         "V.TC005" & VarIni.C8 & "S.TA002",
                         "V.TC006" & VarIni.C8 & "S.TA003",
                         "V.TRANSFER_DATE" & VarIni.C8 & lbPlanDate.Text & VarIni.C8
                         })
            .SetWhere(.WHERE_EQUAL("S.TA001", lbMoType.Text), True)
            .SetWhere(.WHERE_EQUAL("S.TA002", lbMONo.Text))
            .SetOrderBy("S.TA003")
        End With

        'Dim SQL As String = strSQL.GetSQLString()

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, VarIni.char8)
        Dim dt As DataTable = dbConn.Query(strSQL.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, fldNumber, fldManual)
            End With
        Next
        gvCont.GridviewColWithLinkFirst(GvProcess, colName, True, "PLAN", VarIni.C8)
        gvCont.ShowGridView(GvProcess, dtShow)

        'BOM DATA
        al = New ArrayList
        fldName = New ArrayList
        colName = New ArrayList
        fldNumber = New ArrayList
        With New ArrayListControl(al)
            .TAL("ITEM", "Item")
            .TAL("MB002", "Item Desc")
            .TAL("MB003", "Item Spec")
            .TAL("QTY", "QTY", "2")
            .TAL("ISSUE_QTY", "Issued QTY", "2")
            .TAL("ISSUE_BAL", "Issued Balance", "2")
            .TAL("CASE SEQ WHEN '0' THEN 'PARENT' ELSE 'CHILD' END" & VarIni.C8 & "BOM", "BOM")
            .TAL(dbConn.ISNULL("STOCK_QTY", "0",, True), "Inventory Qty", "2")
            .TAL(dbConn.ISNULL("MO_QTY", "0",, True), "MO Qty(+)", "2")
            .TAL(dbConn.ISNULL("MO_PLAN_COMP_DATE", ,, True), "MO Plan Comp. Date")
            .TAL(dbConn.ISNULL("PR_QTY", "0",, True), "PR Qty(+)", "2")
            .TAL(dbConn.ISNULL("PO_QTY", "0",, True), "PO Qty(+)", "2")
            .TAL(dbConn.ISNULL("PO_DUE_DATE", ,, True), "PO Due Date")
            .TAL(dbConn.ISNULL("PI_QTY", "0",, True), "Pur Inspec Qty(+)", "2")
            .TAL(dbConn.ISNULL("MI_QTY", "0",, True), "Mat Issue Qty(-)", "2")
            .TAL(dbConn.ISNULL("MI_ISSUE_DATE", ,, True), "Mat Issue Date")
            .TAL(dbConn.ISNULL("CI_QTY", "0",, True), "Call In Qty(-)", "2")
            .TAL(dbConn.ISNULL("CI_SHIP_DATE", ,, True), "Call in Due Date")
            .TAL(dbConn.ISNULL("SO_QTY", "0",, True), "SO Qty(-)", "2")
            .TAL(dbConn.ISNULL("SO_DUE_DATE", ,, True), "SO Due Date")
            .TAL(dbConn.ISNULL("SAFTY_STOCK", "0",, True), "Safty Qty", "2")
            fldName = .ChangeFormat()
            fldNumber = .ColumnNumber()
            colName = .ChangeFormat(True)
        End With
        Dim sqlItem As New ITEM
        Dim sqlUnion As String = "(select  TA001 MO_TYPE,TA002 MO_NO,TA006 ITEM,TA015 QTY,TA017+TA018 ISSUE_QTY ,TA015-TA017-TA018 ISSUE_BAL,'0' SEQ from MOCTA UNION select TB001,TB002,TB003,TB004,TB005,TB004-TB005,'1' from MOCTB ) MO_ALL"
        strSQL = New SQLString(sqlUnion, fldName)
        With strSQL
            'mo head data
            .setLeftjoin2("MOCTA", New List(Of String) From {
                                       "TA001" & VarIni.C8 & "MO_TYPE",
                                       "TA002" & VarIni.C8 & "MO_NO"
                                       })
            'item
            .setLeftjoin2("INVMB", New List(Of String) From {"MB001" & VarIni.C8 & "ITEM"})
            'sale order qty balance
            .SetLeftjoin2(sqlItem.SO, New List(Of String) From {"SO_ITEM" & VarIni.C8 & "ITEM"})
            'Call In qty balance
            .SetLeftjoin2(sqlItem.CI, New List(Of String) From {"CI_ITEM" & VarIni.C8 & "ITEM"})
            'MO balance
            .setLeftjoin2(sqlItem.MO, New List(Of String) From {"MO_ITEM" & VarIni.C8 & "ITEM"})
            'MO Issued balance
            .SetLeftjoin2(sqlItem.MI, New List(Of String) From {"MI_ITEM" & VarIni.C8 & "ITEM"})
            'Purchase Request balance
            .setLeftjoin2(sqlItem.PR, New List(Of String) From {"PR_ITEM" & VarIni.C8 & "ITEM"})
            'Purchase Order balance
            .setLeftjoin2(sqlItem.PO, New List(Of String) From {"PO_ITEM" & VarIni.C8 & "ITEM"})
            'Purchase reciept inspection balance
            .setLeftjoin2(sqlItem.PI, New List(Of String) From {"PI_ITEM" & VarIni.C8 & "ITEM"})
            'Stock balance
            .setLeftjoin2(sqlItem.STOCK, New List(Of String) From {"STOCK_ITEM" & VarIni.C8 & "ITEM"})


            Dim whrList As New List(Of String)
            With New ListControl(whrList)
                .Add(dbConn.WHERE_IN("TA011", "y,Y", True, True, False))
                .Add(dbConn.WHERE_EQUAL("TA013", "Y", showAnd:=False))
                .Add(dbConn.WHERE_EQUAL("MO_TYPE", lbMoType.Text, showAnd:=False))
                .Add(dbConn.WHERE_EQUAL("MO_NO", lbMONo.Text, showAnd:=False))
            End With
            .SetWhere(whrList, True)
            .SetOrderBy(New List(Of String) From {
                        "MO_TYPE",
                        "MO_NO",
                        "SEQ",
                        "ITEM"})
        End With
        dtShow = dtCont.setColDatatable(colName, VarIni.char8)
        dt = dbConn.Query(strSQL.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)
        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, fldNumber, fldManual)
            End With
        Next
        gvCont.GridviewColWithLinkFirst(GvBOM, colName, True, "Detail", VarIni.C8)
        gvCont.ShowGridView(GvBOM, dtShow)
    End Sub
    Private Sub GvBOM_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvBOM.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim gr As New GridviewRowControl(e.Row)

            End If
        End With
    End Sub

    Private Sub GvProcess_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvProcess.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim gr As New GridviewRowControl(e.Row)
                Dim processGet As String = gr.Text("MO_SEQ")
                Dim capStd As Decimal = gr.Number("TOTAL_CAP")
                Dim capUsage As Decimal = gr.Number("PLAN_TIME")
                Dim pendingQty As Decimal = gr.Number("PENDING_QTY")
                Dim wipQty As Decimal = gr.Number("WIP_QTY")
                'Dim timeUsage As Decimal = gr.Number("PlanTimeProcess")
                'set of link
                Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplDetail"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("MO_SEQ")) Then
                    Dim showLinkPlan As Boolean = False
                    If checkCanPlanDate(lbPlanDate.Text) AndAlso wcPlan.Contains(gr.Text("TA006").Split("-").ToArray(0)) AndAlso
                        (pendingQty > 0 OrElse wipQty > 0) Then
                        hplDetail.NavigateUrl = "PlanScheduleManfOrder.aspx?height=150&width=350&motype= " & outCont.EncodeTo64UTF8(lbMoType.Text) & "&mono= " & outCont.EncodeTo64UTF8(lbMONo.Text) & "&moseq= " & outCont.EncodeTo64UTF8(processGet) & "&plandate=" & outCont.EncodeTo64UTF8(ucDatePlan.textEmptyToday)
                        hplDetail.Target = "_self"
                        hplDetail.Attributes.Add("title", processGet)
                        showLinkPlan = True
                        .BackColor = Drawing.Color.LightGray
                    End If
                    hplDetail.Visible = showLinkPlan
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                'set color of row
                Dim foreColor As Drawing.Color = .ForeColor
                If pendingQty > 0 OrElse wipQty > 0 Then
                    foreColor = Drawing.Color.Red
                    Dim percentUsageTime As Integer = If(capStd <= 0, 0, Math.Round(capUsage * 100 / capStd, 0))
                    If percentUsageTime <= 100 Then
                        foreColor = Drawing.Color.Blue
                    ElseIf percentUsageTime <= 120 Then
                        foreColor = Drawing.Color.Orange
                    End If
                End If
                .ForeColor = foreColor

                If processGet = lbMoSeq.Text Then
                    .BackColor = Drawing.Color.LightGreen
                End If
                '14 usage time ,17 total cap and 18 usage cap
                CellFormatTime(.Cells, New List(Of Integer) From {12, 15, 16})
                End If
        End With
    End Sub

    Sub CellFormatTime(ByRef cellSet As TableCellCollection, colIndex As List(Of Integer))
        For Each index As Integer In colIndex
            With cellSet(index)
                Dim timeUsage As Decimal = outCont.checkNumberic(.Text)
                Dim showTime As String = ""
                If timeUsage > 0 Then
                    showTime = dateCont.TimeFormat(timeUsage)
                End If
                .Text = showTime
                .HorizontalAlign = HorizontalAlign.Center
            End With
        Next
    End Sub


    Protected Sub btRefresh_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        'set plan date to ucDatePlan
        lbPlanDate.Text = ucDatePlan.TextEmptyToday
        lbMoType.Text = UcDdlMoType.Text
        lbMONo.Text = TbMoNo.Text.Trim
        lbMoSeq.Text = ""
        showData()
        ShowMOHead()
        showButton()
    End Sub

    Sub setMOHeadValue(Optional dr As DataRow = Nothing)
        Dim NumberFormat As String = "#,##0"
        With New DataRowControl(dr)
            lbItem.Text = .Text("ITEM")
            lbDesc.Text = .Text("ITEM_DESC")
            lbSpec.Text = .Text("ITEM_SPEC")
            lbSaleDocNo.Text = .Text("SO")
            lbCustName.Text = .Text("CUST_NAME")
            lbSoDueDate.Text = .Text("DUE_DATE")
            lbModel.Text = .Text("MODEL")
            lbCustWo.Text = .Text("CUST_WO")
            lbCustLine.Text = .Text("CUST_LINE")
            lbWC.Text = .Text("WORK_CENTER")
            lbWcName.Text = .Text("WORK_CENTER_NAME")
            lbProcess.Text = .Text("PROCESS_CODE")
            lbProcessName.Text = .Text("PROCESS_NAME")
            lbQty.Text = .Number("MO_QTY").ToString(NumberFormat)
            lbCompleteQty.Text = .Number("COMPLETED_QTY").ToString(NumberFormat)
            lblScarpQty.Text = .Number("SCRAP_QTY").ToString(NumberFormat)
            lbMoBalQty.Text = .Number("PROCESS_QTY_BAL").ToString(NumberFormat)
            lbWIPQty.Text = .Number("WIP_QTY").ToString(NumberFormat)

            lbStdTime.Text = If(.Text("WORKCENTER_TYPE") = "Machine", .Number("STD_MACH"), .Number("STD_LABOR")).ToString(NumberFormat)
            lblUnit.Text = .Text("ITEM_UNIT")
            lbOperator.Text = .Number("OPERATOR").ToString(NumberFormat)


            lbRecordSeq.Text = .Text("PLAN_SEQ")
            lbPlanMach.Text = .Text("PLAN_MACH")
            lbPlanedQty.Text = .Number("PLANED_QTY").ToString(NumberFormat)
            lbPlanQty.Text = .Number("PLAN_QTY")
            lbRealMach.Text = .Text("REAL_MACH")
            lbPlanNote.Text = .Text("PLAN_NOTE")
            lbPlanUrgent.Text = .Text("OVER_TIME")
            lbPlanStatus.Text = .Text("PLAN_STATUS")
            lbPlanSetSeq.Text = .Text("PLAN_SEQ_SET")
            lbUsageTime.Text = dateCont.TimeFormat(outCont.checkNumberic(.Text("PLAN_TIME")))
            lbTransferQty.Text = .Number("TRANSFER_APP_QTY").ToString(NumberFormat)

            'set value to input element
            Dim planSeq As Integer = .Number("PLAN_SEQ")
            TbPlanQty.Text = CInt(If(planSeq > 0, .Number("PLAN_QTY"), outCont.checkNumberic(If(.Number("WIP_QTY") > 0, lbWIPQty.Text, lbMoBalQty.Text))))
            If planSeq = 0 Then
                TbPlanQty_TextChanged(Nothing, Nothing)
            End If

            TbSetSeq.Text = lbPlanSetSeq.Text
            CbUrgent.Checked = If(lbPlanUrgent.Text = "Y", True, False)
            CbPlan.Checked = If(lbPlanStatus.Text = "P", True, False)
            TbNote.Text = lbPlanNote.Text
        End With
    End Sub


    Private Sub ShowMOHead()
        PnShowEdit.Visible = Not String.IsNullOrEmpty(lbMoSeq.Text)
        Dim dr As DataRow = Nothing
        If PnShowEdit.Visible Then
            Dim al As New ArrayList
            Dim fldName As New ArrayList
            Dim colname As New ArrayList
            Dim formatNumber As New Hashtable

            With New ArrayListControl(al)
                .TAL("MOCTA.TA006" & VarIni.C8 & "ITEM", "ITEM")
                .TAL("MOCTA.TA034" & VarIni.C8 & "ITEM_DESC", "ITEM_DESC")
                .TAL("MOCTA.TA035" & VarIni.C8 & "ITEM_SPEC", "ITEM_SPEC")
                .TAL("MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028" & VarIni.C8 & "SO", "SO")
                .TAL("COPMA.MA002" & VarIni.C8 & "CUST_NAME", "CUST_NAME")
                .TAL("MOCTA.TA007" & VarIni.C8 & "ITEM_UNIT", "ITEM_UNIT")
                .TAL("MOCTA.TA015" & VarIni.C8 & "MO_QTY", "MO_QTY")
                .TAL("COPTD.TD013" & VarIni.C8 & "DUE_DATE", "DUE_DATE")
                .TAL("COPTD.UDF04" & VarIni.C8 & "MODEL", "MODEL")
                .TAL("COPTD.UDF02" & VarIni.C8 & "CUST_WO", "CUST_WO")
                .TAL("COPTD.UDF03" & VarIni.C8 & "CUST_LINE", "CUST_LINE")
                .TAL("SFCTA.TA006" & VarIni.C8 & "WORK_CENTER", "WORK_CENTER")
                .TAL("SFCTA.TA007" & VarIni.C8 & "WORK_CENTER_NAME", "WORK_CENTER_NAME")
                .TAL("SFCTA.TA004" & VarIni.C8 & "PROCESS_CODE", "PROCESS_CODE")
                .TAL("SFCTA.TA024" & VarIni.C8 & "PROCESS_NAME", "PROCESS_NAME")
                .TAL("SFCTA.TA011" & VarIni.C8 & "COMPLETED_QTY", "COMPLETED_QTY")
                .TAL("SFCTA.TA012+SFCTA.TA056" & VarIni.C8 & "SCRAP_QTY", "SCRAP_QTY")
                .TAL("SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058" & VarIni.C8 & "WIP_QTY", "WIP_QTY")
                .TAL("MOCTA.TA015+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058+isnull(V.TC014_APP,0)+isnull(V.TC016_APP,0)" & VarIni.C8 & "PROCESS_QTY_BAL", "PROCESS_QTY_BAL")
                .TAL("case when MOCTA.TA015=0 then 0 else round(SFCTA.TA022/MOCTA.TA015,0) end" & VarIni.C8 & "STD_LABOR", "STD_LABOR")
                .TAL("case when MOCTA.TA015=0 then 0 else round(SFCTA.TA035/MOCTA.TA015,0) end" & VarIni.C8 & "STD_MACH", "STD_MACH")
                .TAL("isnull(BOMMF.UDF51,-1)" & VarIni.C8 & "OPERATOR", "OPERATOR", "0")
                .TAL("isnull(P.PlanDate,'')" & VarIni.C8 & "PLAN_DATE", "PLAN_DATE")
                .TAL("isnull(P.PlanSeq,0)" & VarIni.C8 & "PLAN_SEQ", "PLAN_SEQ")
                .TAL("isnull(P.PlanSeqSet,'')" & VarIni.C8 & "PLAN_SEQ_SET", "PLAN_SEQ_SET")
                .TAL("isnull(P.PlanedQty,0)" & VarIni.C8 & "PLANED_QTY", "PLANED_QTY")
                .TAL("isnull(P.PlanQty,0)" & VarIni.C8 & "PLAN_QTY", "PLAN_QTY")
                .TAL("isnull(P.Urgent,'N')" & VarIni.C8 & "OVER_TIME", "OVER_TIME")
                .TAL("isnull(P.PlanStatus,'P')" & VarIni.C8 & "PLAN_STATUS", "PLAN_STATUS")
                .TAL("isnull(P.PlanTime,0)" & VarIni.C8 & "PLAN_TIME", "PLAN_TIME")
                .TAL("isnull(P.PlanNote,'')" & VarIni.C8 & "PLAN_NOTE", "PLAN_NOTE")
                .TAL("isnull(P.PlanMachine,'')" & VarIni.C8 & "PLAN_MACH", "PLAN_MACH")
                .TAL("isnull(P.RealMachine,'')" & VarIni.C8 & "REAL_MACH", "REAL MACH")

                .TAL("isnull(V.TC014_APP,0)+ isnull(V.TC016_APP,0)" & VarIni.C8 & "TRANSFER_APP_QTY", "TRANSFER_APP_QTY")
                .TAL("CMSMD.UDF02" & VarIni.C8 & "WORKCENTER_TYPE", "WORKCENTER_TYPE")

                fldName = .ChangeFormat()
                colname = .ChangeFormat(True)
                'formatNumber = .ColumnNumberHash()

            End With

            Dim strSQL As New SQLString("SFCTA", fldName)
            With strSQL
                .setLeftjoin("MOCTA", New List(Of String) From {
                             "MOCTA.TA001" & VarIni.C8 & "SFCTA.TA001",
                             "MOCTA.TA002" & VarIni.C8 & "SFCTA.TA002"
                             })

                .setLeftjoin("COPTD", New List(Of String) From {
                             "COPTD.TD001" & VarIni.C8 & "MOCTA.TA026",
                             "COPTD.TD002" & VarIni.C8 & "MOCTA.TA027",
                             "COPTD.TD003" & VarIni.C8 & "MOCTA.TA028"
                             })
                .setLeftjoin("COPTC", New List(Of String) From {
                             "COPTC.TC001" & VarIni.C8 & "COPTD.TD001",
                             "COPTC.TC002" & VarIni.C8 & "COPTD.TD002"
                             })
                .setLeftjoin("COPMA", New List(Of String) From {"COPMA.MA001" & VarIni.C8 & "COPTC.TC004"})
                .setLeftjoin(PLANSCHEDULE_T.table_full & " P", New List(Of String) From {
                            "P.MoType" & VarIni.C8 & "SFCTA.TA001",
                            "P.MoNo" & VarIni.C8 & "SFCTA.TA002",
                            "P.MoSeq" & VarIni.C8 & "SFCTA.TA003",
                            "P.PlanDate" & VarIni.C8 & lbPlanDate.Text & VarIni.C8
                            })
                .setLeftjoin("BOMMF", New List(Of String) From {
                             "BOMMF.MF001" & VarIni.C8 & "MOCTA.TA006",
                             "BOMMF.MF002" & VarIni.C8 & "01" & VarIni.C8,
                             "BOMMF.MF003" & VarIni.C8 & "SFCTA.TA003"
                             })
                strSQL.setLeftjoin(VarIni.DBMIS & "..V_TRANSFER_SUM_DATE V", New List(Of String) From {
                          "V.TC004" & VarIni.C8 & "P.MoType",
                          "V.TC005" & VarIni.C8 & "P.MoNo",
                          "V.TC006" & VarIni.C8 & "P.MoSeq",
                          "V.TRANSFER_DATE" & VarIni.C8 & "P.PlanDate"
                           })
                'CMSMD.UDF02
                strSQL.setLeftjoin("CMSMD", New List(Of String) From {"CMSMD.MD001" & VarIni.C8 & "SFCTA.TA006"})
                .SetWhere(.WHERE_EQUAL("SFCTA.TA001", lbMoType.Text), True)
                .SetWhere(.WHERE_EQUAL("SFCTA.TA002", lbMONo.Text))
                .SetWhere(.WHERE_EQUAL("SFCTA.TA003", lbMoSeq.Text))

            End With
            ' Dim aa As String = strSQL.GetSQLString
            dr = dbConn.QueryDataRow(strSQL.GetSQLString, VarIni.ERP, strSQL.WhoCalledMe)
        End If
        setMOHeadValue(dr)

    End Sub

    Sub reset()
        setMOHeadValue()
        GvProcess.DataSource = ""
        GvProcess.DataBind()
        btSave.Visible = False
        btSearch.Visible = False
    End Sub

    Function CheckRecorded() As Boolean
        Return If(outCont.checkNumberic(lbRecordSeq.Text) > 0, True, False)
    End Function

    Protected Sub TbPlanQty_TextChanged(sender As Object, e As EventArgs) Handles TbPlanQty.TextChanged
        Dim maxPlanQty As Decimal = outCont.checkNumberic(lbWIPQty.Text)
        Dim minPlanQty As Decimal = 0
        Dim moBalQty As Decimal = outCont.checkNumberic(lbMoBalQty.Text)
        If moBalQty > maxPlanQty Then
            maxPlanQty = moBalQty
        End If

        Dim planQty As Decimal = outCont.checkNumberic(TbPlanQty.Text)
        Dim QtyPlan As Decimal = planQty
        Select Case CheckRecorded()
            Case False 'no record
                If planQty = 0 OrElse (planQty > 0 AndAlso planQty >= maxPlanQty) Then
                    QtyPlan = maxPlanQty
                End If
            Case True 'have record
                QtyPlan = planQty
                minPlanQty = outCont.checkNumberic(lbTransferQty.Text)
                'maxPlanQty += minPlanQty
                If planQty < minPlanQty Then
                    QtyPlan = minPlanQty
                End If
                If planQty <= 0 OrElse planQty > maxPlanQty Then
                    QtyPlan = maxPlanQty
                End If
        End Select
        TbPlanQty.Text = QtyPlan
        'cal time to usage 
        lbUsageTime.Text = dateCont.TimeFormat(QtyPlan * outCont.checkNumberic(lbStdTime.Text))

        showData()
    End Sub

    Function SetPlanRecordSeq() As Decimal
        Dim SQL As String = "select isnull(max(PlanSeq),0)+1 PLAN_SEQ from " & PLANSCHEDULE_T.table & " where PlanDate='_PLANDATE_' and WorkCenter='_WORKCENTER_' "
        SQL = SQL.Replace("_PLANDATE_", lbPlanDate.Text).Replace("_WORKCENTER_", lbWC.Text)
        Dim dr As New DataRowControl(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Return dr.Number("PLAN_SEQ")
    End Function


    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        'check before save
        Dim checkPass As String = False
        If outCont.checkNumberic(lbStdTime.Text) = 0 Then
            show_message.ShowMessage(Page, "No stardard time (ไม่มีเวลา กรุณาติดต่อ PE)", UpdatePanel1)
            Exit Sub
        End If
        If CheckRecorded() Then
            Dim valOt As String = If(CbUrgent.Checked, "Y", "N")
            Dim valPlanStatus As String = If(CbPlan.Checked, "P", "C")
            If TbPlanQty.Text <> lbPlanQty.Text OrElse TbNote.Text <> lbPlanNote.Text _
                OrElse TbSetSeq.Text <> lbPlanSetSeq.Text _
                OrElse valOt <> lbPlanUrgent.Text _
                OrElse valPlanStatus <> lbPlanStatus.Text Then
                checkPass = True
            End If
        Else
            checkPass = True
        End If
        Dim msg As String = "No Data Save(ข้อมูลไม่มีการเปลี่ยนแปลง)"
        If checkPass Then
            SavePlan()
            showData()
            msg = "Save completed.(บันทึกข้อมูลเรียบร้อย)"
        End If
        ShowMOHead() 'show data head
        showData() 'show gridview 
        showButton() 'set show button
        show_message.ShowMessage(Page, msg, UpdatePanel1)
    End Sub

    Function SavePlan() As Boolean
        Dim SQLType As String = "U"

        Dim line As Integer = outCont.checkNumberic(lbRecordSeq.Text)
        Dim valUser As String = Session(VarIni.UserName)
        Dim valDateTime As String = DateTime.Now.ToString("yyyyMMdd hhmmss")
        If Not CheckRecorded() Then
            line = SetPlanRecordSeq()
            SQLType = "I"
        End If
        Dim whrHash As New Hashtable From {
            {PLANSCHEDULE_T.PlanDate, lbPlanDate.Text}, 'Plan Date
            {PLANSCHEDULE_T.WorkCenter, lbWC.Text}, 'WC
            {PLANSCHEDULE_T.PlanSeq, line} 'PlanSeq
        }
        'insert
        Dim fldInsHash As New Hashtable From {
            {PLANSCHEDULE_T.PlanSeqSet, TbSetSeq.Text.Trim}, 'PlanSeqSet
            {PLANSCHEDULE_T.MoType, lbMoType.Text}, 'MO Type
            {PLANSCHEDULE_T.MoNo, lbMONo.Text}, 'MO No
            {PLANSCHEDULE_T.MoSeq, lbMoSeq.Text}, 'MO Seq
            {PLANSCHEDULE_T.ProcessCode, lbProcess.Text}, 'Operation
            {PLANSCHEDULE_T.PlanedQty, lbPlanedQty.Text} 'PlanedQty
        }

        Dim stdTime As Decimal = outCont.checkNumberic(lbStdTime.Text) 'stardard time(sec)
        Dim planQty As Decimal = outCont.checkNumberic(TbPlanQty.Text)  'MO Bal Qty

        fldInsHash.Add(PLANSCHEDULE_T.PlanQty, planQty) 'PlanQty
        fldInsHash.Add(PLANSCHEDULE_T.PlanTimeStd, stdTime) 'Plan Time(Sec)
        fldInsHash.Add(PLANSCHEDULE_T.PlanTime, stdTime * planQty) 'Plan Time(Sec)
        Dim valOt As String = If(CbUrgent.Checked, "Y", "N")
        Dim valPlanStatus As String = If(CbPlan.Checked, "P", "C")
        fldInsHash.Add(PLANSCHEDULE_T.Urgent, If(CbUrgent.Checked, "Y", "N")) 'urgent
        fldInsHash.Add(PLANSCHEDULE_T.PlanNote, TbNote.Text.Trim) 'mch/note
        If CbPlan.Checked Then
            fldInsHash.Add(PLANSCHEDULE_T.CancledBy, valUser) 'user record
            fldInsHash.Add(PLANSCHEDULE_T.CancledDate, valDateTime) 'date time record
        End If
        fldInsHash.Add(PLANSCHEDULE_T.PlanStatus, If(CbPlan.Checked, "P", "C")) 'plan status

        Dim fldUser As String = PLANSCHEDULE_T.CreateBy
        Dim fldDateTime As String = PLANSCHEDULE_T.CreateDate
        If SQLType = "U" Then
            fldUser = PLANSCHEDULE_T.ChangeBy
            fldDateTime = PLANSCHEDULE_T.ChangeDate
        End If
        fldInsHash.Add(fldUser, valUser)
        fldInsHash.Add(fldDateTime, valDateTime)
        Dim TSQL As String = dbConn.GetSQL(PLANSCHEDULE_T.table, fldInsHash, whrHash, SQLType)
        If dbConn.TransactionSQL(TSQL, VarIni.DBMIS, dbConn.WhoCalledMe) = 0 Then
            SavePlan()
        End If
        Return True
    End Function

    Private Sub ucDatePlan_ChangeEvent(sender As Object, e As EventArgs) Handles ucDatePlan.ChangeEvent
        btSearch.Visible = checkCanPlanDate(ucDatePlan.textEmptyToday)
        PnShowEdit.Visible = False
        setMOHeadValue()
        GvProcess.DataSource = ""
        GvProcess.DataBind()
        btSave.Visible=false
    End Sub


End Class