Imports System.Drawing
Imports ClosedXML.Excel

Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl




Public Class PlanScheduleList
    Inherits System.Web.UI.Page
    'Dim ControlForm As New ControlDataForm
    'Dim Conn_SQL As New ConnSQL
    'Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim cblCont As New CheckBoxListControl
    Dim dateCont As New DateControl
    Dim gvCont As New GridviewControl

    Dim PS As New planSchedule
    Private Shared DateBegin As Date
    'Private Shared DateEnd As Date
    Private Shared CountDate As Integer
    Private Shared sameMonth As Boolean = True


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            CreateTable.CreateMoBalanceTable()
            CreateTable.CreatePlanScheduleTable()
            Dim SQL As String = "select MD001,MD001+':'+MD002 as MD002 from CMSMD where MD001 in ('" & getWc() & "') order by MD001 "
            cblCont.showCheckboxList(cblWorkCenter, SQL, VarIni.ERP, "MD002", "MD001", 4)
            'ddlMonth.Text = Now.Month
            showGridview(True)
        End If
    End Sub

    Function getWc(Optional needSQ As Boolean = True) As String
        Dim SQL As String = ""
        SQL = "select WC from UserPlanAuthority where Id='" & Session("UserId") & "' "
        Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim val As String = dtCont.IsDBNullDataRow(dr, "WC")
        Return If(needSQ, val.Replace(",", "','"), val)
    End Function

    Private Sub gvShow_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShow.RowCommand
        Dim i As Integer = e.CommandArgument
        Dim id As String = gvShow.Rows(i).Cells(0).Text.Replace(" ", "")
        Select Case e.CommandName
            Case "onEdit"
                With gvShow.Rows(i)
                    Response.Redirect("PlanScheduleAdd.aspx?wc=" & .Cells(0).Text.Replace(" ", "") & "&planno=" & .Cells(1).Text.Replace(" ", "") & "&plandate=" & .Cells(2).Text.Replace(" ", ""))
                End With
        End Select
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        showGridview()
    End Sub

    Private Sub showGridview(Optional wcDefault As Boolean = False)
        Dim dateStartVal As String = ucPlanDateFrom.text
        Dim dateToVal As String = ucPlanDateTo.text
        Dim dateStart As String = dateStartVal
        Dim dateTo As String = dateToVal
        Dim dateCount As Integer = 1
        Dim date1 As Date, date2 As Date

        If dateStart <> String.Empty Then
            date1 = dateCont.strToDateTime(dateStart, dateCont.FormatData)
        End If
        If dateTo <> String.Empty Then
            date2 = dateCont.strToDateTime(dateTo, dateCont.FormatData)
        End If

        If dateStart = String.Empty And dateTo = String.Empty Then
            dateStart = DateTime.Now.ToString("yyyyMMdd")
            date1 = dateCont.strToDateTime(dateStart, dateCont.FormatData)
        Else
            If dateStart <> String.Empty And dateTo <> String.Empty Then
                dateCount = DateDiff(DateInterval.Day, date1, date2)
            Else
                If dateStart = String.Empty Then
                    dateStart = dateTo
                    date1 = dateCont.strToDateTime(dateStart, dateCont.FormatData)
                End If
            End If
        End If

        DateBegin = date1
        CountDate = dateCount
        If dateStartVal = String.Empty And dateToVal = String.Empty Then
            DateBegin = date1.AddDays(-1)
            CountDate = 1
        End If
        If CountDate > 1 Then
            Dim cntMonth As Integer = DateDiff(DateInterval.Month, DateBegin, DateBegin.AddDays(CountDate))
            If cntMonth > 0 Then
                sameMonth = False
            End If
        End If

        Dim ddFrom As String = DateBegin.ToString(dateCont.FormatData)
        Dim ddTo As String = DateBegin.AddDays(CountDate).ToString(dateCont.FormatData)
        Dim WHR As String = String.Empty
        WHR = dbConn.WHERE_IN("MD001", cblWorkCenter, False, True)
        Dim SQL As String = "select MD001,MD002,isnull(STD_TIME,0) STD_TIME from CMSMD left join (select MX002,ROUND(SUM((CMSMX.UDF51+UDF52)*CMSMX.UDF53),0) STD_TIME from CMSMX GROUP BY MX002) CMSMX on MX002=MD001 where 1=1 " & WHR & " order by MD001 "
        Dim grpBy As String = ""
        Dim ordBy As String = ""
        Dim colName As New ArrayList
        colName.Add("W/C" & VarIni.char8 & "MD001")
        colName.Add("W/C Name" & VarIni.char8 & "MD002")
        colName.Add("Capacity(HH:MM)" & VarIni.char8 & "STD_TIME")
        For i = 0 To dateCount
            Dim d As Date = DateBegin.AddDays(i)
            Dim dd As String = d.ToString(dateCont.FormatData)
            colName.Add(dd & VarIni.char8 & dd)
        Next
        Dim dtShow As DataTable = dtCont.setColDatatable(colName, VarIni.char8)
        Dim dt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For Each dr As DataRow In dt.Rows
            Dim wc As String = dtCont.IsDBNullDataRow(dr, "MD001")
            Dim dataFld As New ArrayList From {
                "MD001" & VarIni.char8 & VarIni.char8 & wc,
                "MD002" & VarIni.char8 & VarIni.char8 & dtCont.IsDBNullDataRow(dr, "MD002"),
                "STD_TIME" & VarIni.char8 & VarIni.char8 & dtCont.IsDBNullDataRow(dr, "STD_TIME")
            }
            Dim fldNameSub As New ArrayList From {
                PLANSCHEDULE_T.PlanDate,
                dbConn.SUM(PLANSCHEDULE_T.PlanTimeStd & "*" & PLANSCHEDULE_T.PlanQty, VarIni.char8, True, PLANSCHEDULE_T.PlanTime)
            }
            Dim strSQL As New SQLString(PlanSchedule_t.table_full, fldNameSub)
            WHR = ""
            WHR &= dbConn.WHERE_EQUAL(PlanSchedule_t.WorkCenter, wc)
            WHR &= dbConn.WHERE_BETWEEN(PlanSchedule_t.PlanDate, ddFrom, ddTo)
            WHR &= dbConn.WHERE_EQUAL(PlanSchedule_t.PlanStatus, "P")
            strSQL.SetWhere(WHR, True)
            strSQL.SetGroupBy(PlanSchedule_t.PlanDate)
            strSQL.SetOrderBy(PlanSchedule_t.PlanDate)

            Dim dtSub As DataTable = dbConn.Query(strSQL.GetSQLString, VarIni.ERP, dbConn.WhoCalledMe)
            For Each drSub As DataRow In dtSub.Rows
                Dim pDate As String = dtCont.IsDBNullDataRow(drSub, PLANSCHEDULE_T.PlanDate)
                If pDate >= ddFrom And pDate <= ddTo Then
                    dataFld.Add(pDate & VarIni.char8 & VarIni.char8 & dtCont.IsDBNullDataRowDecimal(drSub, PLANSCHEDULE_T.PlanTime))
                End If
            Next
            dtCont.addDataRow(dtShow, dr, dataFld, VarIni.char8)
        Next
        gvCont.GridviewInitial(gvShow, colName, strSplit:=VarIni.char8)
        gvCont.ShowGridView(gvShow, dtShow)
        CreateHeadGeridShow()
    End Sub

    Private Sub CreateHeadGeridShow()
        Dim planCont As New DataControl.PlanControl
        Dim outCont As New OutputControl
        Dim hashCont As New HashtableControl
        Dim timeHash As New Hashtable

        If gvShow.Rows.Count > 0 Then
            Dim i As Integer = 3
            Dim SQL As String = String.Empty
            While i < gvShow.HeaderRow.Cells.Count
                Dim dateHead As String = gvShow.HeaderRow.Cells(i).Text.Trim
                Dim PlanDate As Date = dateCont.strToDateTime(dateHead, dateCont.FormatData)
                Dim DayOfWeek As String = PlanDate.ToString("ddd")
                Dim dateToday As String = DateTime.Now.ToString(dateCont.FormatData)

                Dim styleCode As String = "White"
                If PlanDate.Date = DateTime.Now.Date Then
                    styleCode = "Green"
                Else
                    If Regex.IsMatch(DayOfWeek, "Sun|Sat") Then
                        styleCode = "Red"
                    Else
                        styleCode = "White"
                    End If
                End If
                gvShow.HeaderRow.Cells(i).Text = "<span style='color:" & styleCode & ";'>" & PlanDate.ToString(If(sameMonth, "dd", "dd-MMM")) & "(" & DayOfWeek & ")</span>"
                Dim WC As String = String.Empty
                Dim mach As String = String.Empty
                Dim machType As String = String.Empty
                Dim shift As String = String.Empty
                Dim Loading As Decimal = 0
                'Dim wcHash As New Hashtable
                For Each row As GridViewRow In gvShow.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        With row
                            WC = .Cells(0).Text
                            If Not hashCont.existDataHash(timeHash, WC) Then
                                Dim val As Decimal = CDec(.Cells(2).Text)
                                hashCont.addDataHash(timeHash, WC, val)
                                .Cells(2).Text = dateCont.ConvertHHMM(val)
                                .Cells(2).HorizontalAlign = HorizontalAlign.Center
                            End If
                            Loading = hashCont.getDataHashDecimal(timeHash, WC)
                            With .Cells(i)
                                Dim txtTooltip As String = String.Empty
                                Dim foreColor As Color = Color.Black
                                Dim actualLoading As Decimal = outCont.checkNumberic(.Text)
                                If actualLoading > 0 Then
                                    If Loading = 0 Then
                                        foreColor = Color.Yellow
                                        txtTooltip = "Work center capacity not set!!!"
                                    Else
                                        Dim perLoad As Decimal = (actualLoading / Loading) * 100
                                        foreColor = planCont.getColorPlan(perLoad)
                                        txtTooltip = "Plan% " & planCont.getTextPlan(perLoad) & " , Plan%=" & perLoad.ToString("N2")
                                    End If
                                    .Text = dateCont.ConvertHHMM(actualLoading)
                                    .HorizontalAlign = HorizontalAlign.Center
                                Else
                                    .Text = ""
                                End If
                                .ForeColor = foreColor
                                .ToolTip = txtTooltip
                                If PlanDate.Date >= DateTime.Now.Date Then
                                    .BackColor = Color.LightGray
                                End If
                                .Attributes.Add("style", "cursor:pointer;")
                                .Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFCCFF'")
                                .Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                                Dim sendPara As String = "plandate=" & outCont.EncodeTo64UTF8(dateHead)
                                sendPara &= "&wc=" & outCont.EncodeTo64UTF8(WC)
                                'sendPara &= "&shift=" & outCont.EncodeTo64UTF8(shift)
                                'sendPara &= "&mtype=" & outCont.EncodeTo64UTF8(machType.Substring(0, 1))
                                'sendPara &= "&plan_method=" & outCont.EncodeTo64UTF8(ddlPlanMethod.Text)
                                'sendPara &= "&add_ot=" & outCont.EncodeTo64UTF8(If(cbAddOT.Checked, "1", "0"))
                                '.Attributes.Add("onclick", "location='PlanScheduleMch.aspx?" & sendPara & "'")
                                .Attributes.Add("onclick", "NewWindow('PlanScheduleAdd.aspx?" & sendPara & "','PlanScheduleMach',1000,750,'yes')")
                            End With
                        End With
                    End If
                Next
                i += 1
            End While
        End If
    End Sub

    Private Sub gvShow_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim indexStart As Integer = 3
            Dim HeaderRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell2 As New TableCell()
            HeaderCell2.Text = "Information"
            HeaderCell2.Font.Bold = True
            HeaderCell2.ForeColor = ColorTranslator.FromHtml("#DF7401")
            HeaderCell2.ColumnSpan = indexStart
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell2)
            gvShow.Controls(0).Controls.AddAt(0, HeaderRow)

            Dim HeaderCell As New TableCell()
            HeaderCell.Text = "Date Between " & "<span style='color:#DF7401;'>" & "  " & DateBegin.ToString("dd-MMM-yyyy") & " ~ " & DateBegin.AddDays(CountDate).ToString("dd-MMM-yyyy") & "</span>"
            HeaderCell.ColumnSpan = (e.Row.Cells.Count - indexStart)
            HeaderCell.Font.Bold = True
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell)
            gvShow.Controls(0).Controls.AddAt(1, HeaderRow)
        End If
    End Sub

    Protected Sub BtPlanExcel_Click(sender As Object, e As EventArgs) Handles BtPlanExcel.Click
        showPlan(True)
    End Sub


    Sub showPlan(Optional excel As Boolean = False, Optional OT As Boolean = False)
        Dim whr As String = ""
        whr &= dbConn.WHERE_BETWEEN("PlanDate", ucPlanDateFrom.text, ucPlanDateTo.text)
        whr &= dbConn.WHERE_IN("WorkCenter", cblWorkCenter,, True)
        If OT Then
            whr &= dbConn.WHERE_EQUAL("Urgent", "Y")
        End If
        Dim dtShow As DataTable = Nothing
        If Not String.IsNullOrEmpty(whr) Then
            'get max process
            Dim SQL As String = "SELECT top 1 PlanDate,MoType,MoNo,count(*) cntProcess  FROM PlanSchedule where 1=1 " & whr & " group by PlanDate,MoType,MoNo order by cntProcess desc "
            Dim maxProcess As Integer = 0
            With New DataRowControl(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                maxProcess = .Number("cntProcess")
            End With

            Dim al As New ArrayList
            Dim colName As ArrayList
            'Dim colNum As Hashtable
            With New ArrayListControl(al)
                .TAL("SEQ", "Seq")
                .TAL("DOC_NO", "MO")
                .TAL("SPEC", "Spec")
                .TAL("QTY", "Qty", "0")
                .TAL("PLAN_DATE", "Plan Date")
                .TAL("COMP_DATE", "Plan Complete Date")
                For i As Integer = 1 To maxProcess
                    .TAL("P" & i, "Process " & i)
                    .TAL("Q" & i, "QTY " & i, "0")
                Next
                colName = .ChangeFormat(True)
                'colNum = .ColumnNumberHash()
            End With
            'get process
            SQL = "SELECT MoType+'-'+rtrim(MoNo) DOC_NO,rtrim(PlanDate)+'*'+MoType+'-'+rtrim(MoNo) DOC_NO_CHECK,
                          MoSeq MO_SEQ,MOCTA.TA035 SPEC , MOCTA.TA010 COMP_DATE,MOCTA.TA015 QTY,PlanDate PLAN_DATE ,
                          MW002,PlanQty
	               FROM [HOOTHAI_REPORT]..[PlanSchedule]
                   LEFT JOIN MOCTA ON MOCTA.TA001=MoType and MOCTA.TA002=MoNo 
                   LEFT JOIN CMSMW ON CMSMW.MW001=ProcessCode
                   WHERE PlanQty>0 " & whr & "  ORDER BY DOC_NO_CHECK,MoSeq"

            Dim dt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            dtShow = dtCont.setColDatatable(colName, VarIni.C8)
            Dim valPresent As String = ""
            Dim valLast As String = ""
            Dim fldData As New ArrayList
            Dim line As Integer = 1
            Dim col As Integer = 1
            For Each dr As DataRow In dt.Rows
                With New DataRowControl(dr)
                    Dim docNo As String = .Text("DOC_NO")
                    valPresent = .Text("DOC_NO_CHECK")
                    If valPresent <> valLast Then
                        If Not String.IsNullOrEmpty(valLast) Then
                            .AddManual(dtShow, fldData)
                            line += 1
                            col = 1
                        End If
                        fldData = New ArrayList From {
                            "SEQ" & VarIni.C8 & line,
                            "DOC_NO" & VarIni.C8 & .Text("DOC_NO"),
                            "SPEC" & VarIni.C8 & .Text("SPEC"),
                            "QTY" & VarIni.C8 & .Text("QTY"),
                            "PLAN_DATE" & VarIni.C8 & .Text("PLAN_DATE"),
                            "COMP_DATE" & VarIni.C8 & .Text("COMP_DATE")
                        }
                    End If
                    fldData.Add("P" & col & VarIni.C8 & .Text("MW002"))
                    fldData.Add("Q" & col & VarIni.C8 & .Number("PlanQty"))
                    col += 1
                    valLast = valPresent
                End With
            Next
            If Not String.IsNullOrEmpty(valLast) Then
                Dim drr As DataRow = Nothing
                With New DataRowControl(drr)
                    .AddManual(dtShow, fldData)
                End With
            End If


            If excel Then
                Dim expCont As New ExportImportControl
                'Dim wb As New XLWorkbook()
                'expCont.ExportDatatable(wb, NameOf(PlanScheduleList), dtShow, colName)
                'expCont.downloadFile(wb, NameOf(PlanScheduleList))
                ' expCont.ExportDatatable(NameOf(PlanScheduleList), dtShow, colName)
                expCont.Export(NameOf(PlanScheduleList), dtShow)
            Else
                'Dim gvCont As New GridviewControl
                'gvCont.ShowGridView(gvShow, dtShow)
            End If

        End If

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub BtPlanOTExcel_Click(sender As Object, e As EventArgs) Handles BtPlanOTExcel.Click
        showPlan(True, True)
    End Sub
End Class