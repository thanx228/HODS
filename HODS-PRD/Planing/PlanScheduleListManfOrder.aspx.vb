Imports System.Drawing
Imports System.Globalization

Imports MIS_HTI.UserControl
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class PlanScheduleListManfOrder
    Inherits System.Web.UI.Page

    Dim dateCont As New DateControl
    Dim whrCont As New WhereControl
    Dim dbConn As New DataConnectControl
    Dim sqlCont As New SQLControl
    Dim outCont As New OutputControl
    Dim dtCont As New DataTableControl
    Dim hashCont As New HashtableControl
    Dim wcAuth As New UserAuthorization
    Dim gvCont As New GridviewControl

    Private Shared DateBegin As Date
    'Private Shared DateEnd As Date
    Private Shared CountDate As Integer
    Private Shared sameMonth As Boolean = True

    Dim STD_MCH As String = NameOf(STD_MCH)
    Dim STD_MAN As String = NameOf(STD_MAN)

    Dim rowCheck As Decimal = 100
    Dim colStart As Integer = 10
    Dim PlanShceduleHash As New Hashtable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session(VarIni.UserId) <> String.Empty Then
                '= dbConn.Where(ECAA.WorkcenterID, wcAuth.getAuthorize(Session(VarIni.UserId), True))
                Dim SQL As String = "select rtrim(MD001) as MD001,rtrim(MD001)+':'+rtrim(MD002) as MD002 from CMSMD where MD001 in ('" & getWc() & "') order by MD001 "
                'UcDdlWc.show(SQL, VarIni.ERP, "MD002", "MD001", True)
                UcCblWc.show(SQL, VarIni.ERP, "MD002", "MD001")

                UcDdlMoType.showDocType("51,52", True)
                UcDdlSoType.showDocType("22", True)

                LbNote.Text = "แสดงรายการจำนวน " & rowCheck & " แถวเท่านั้น"
                LbNote.ForeColor = Drawing.Color.Red

            End If
        End If
    End Sub

    Function getWc(Optional needSQ As Boolean = True) As String
        Dim SQL As String = ""
        SQL = "select WC from UserPlanAuthority where Id='" & Session("UserId") & "' "
        Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim val As String = dtCont.IsDBNullDataRow(dr, "WC")
        Return If(needSQ, val.Replace(",", "','"), val)
    End Function



    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        'getdataMO_Header()
        Dim getRow As Decimal = showGridview()
        If getRow > rowCheck Then
            show_message.ShowMessage(Page, "Result Row over " & rowCheck.ToString("N0") & " rows(" & getRow.ToString("N0") & " rows) , Please check conditon to search again\n(แสดงผลได้ 50 แถว กรุณาตรวจสอบคำค้นอีกครั้ง)", UpdatePanel1)
            Exit Sub
        End If
    End Sub

    Private Function showGridview() As Decimal
        Dim dateStartVal As String = ucDateFrom.Text
        Dim dateToVal As String = ucDateTo.Text
        Dim dateStart As String = dateStartVal
        Dim dateTo As String = dateToVal
        Dim dateCount As Integer = 7
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
            CountDate = 7
        End If
        If CountDate > 1 Then
            Dim cntMonth As Integer = DateDiff(DateInterval.Month, DateBegin, DateBegin.AddDays(CountDate))
            If cntMonth > 0 Then
                sameMonth = False
            End If
        End If

        Dim WHR As String = String.Empty
        '========== select to show
        Dim fldwip As String = "SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058"
        Dim fldmobal As String = "MOCTA.TA015+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058"

        Dim stdLabor As String = "round(SFCTA.TA022/MOCTA.TA015,0)"
        Dim stdMachine As String = "round(SFCTA.TA035/MOCTA.TA015,0)"

        Dim fldStandardTime As String = ""
        fldStandardTime &= " case CMSMD.UDF02 when 'Machine' then " & stdMachine & " "
        fldStandardTime &= "                  when 'Machine-AP100' then 0 "
        fldStandardTime &= "                  else " & stdLabor & " end "



        Dim fldName As New ArrayList From {
            "rtrim(SFCTA.TA001)+'*'+rtrim(SFCTA.TA002)+'*'+rtrim(SFCTA.TA003)" & VarIni.char8 & "DOC_NO", 'docno
            "rtrim(SFCTA.TA006)+':'+SFCTA.TA007" & VarIni.char8 & "WC_SHOW", 'work center
            "SFCTA.TA004+':'+CMSMW.MW002" & VarIni.char8 & "PROCESS_SHOW", 'process
            "MOCTA.TA015" & VarIni.char8 & "MO_QTY", 'plan qty
            fldwip & VarIni.char8 & "WIP_QTY", 'wip
            fldmobal & VarIni.char8 & "MO_BAL_QTY", 'mo bal
            "MOCTA.TA035" & VarIni.char8 & "SPEC",
            "SFCTA.TA001", 'type
            "SFCTA.TA002", 'no
            "SFCTA.TA003", 'seq
            "SFCTA.TA004", 'process
            "SFCTA.TA006", 'wc
            "SFCTA.TA007", 'wc name
            "MOCTA.TA006" & VarIni.char8 & "ITEM", 'item
            fldStandardTime & VarIni.char8 & "TIME_STD"'time standard          
            }


        Dim colName As New ArrayList From {
            "MO Type*Number*Seq" & VarIni.char8 & "DOC_NO",
            "W/C" & VarIni.char8 & "WC_SHOW",
            "Process" & VarIni.char8 & "PROCESS_SHOW",
            "Spec" & VarIni.char8 & "SPEC",
            "MO Qty" & VarIni.char8 & "MO_QTY" & VarIni.char8 & "0",
            "WIP Qty" & VarIni.char8 & "WIP_QTY" & VarIni.char8 & "0",
            "MO Bal Qty" & VarIni.char8 & "MO_BAL_QTY" & VarIni.char8 & "0",
            "Plan Qty" & VarIni.char8 & "PLAN_QTY",
            "Transfers Qty" & VarIni.char8 & "TRANS_QTY",
            "Time Std(s)" & VarIni.char8 & "TIME_STD" & VarIni.char8 & "0"
        }
        For i = 0 To dateCount
            Dim d As Date = DateBegin.AddDays(i)
            Dim dd As String = d.ToString(dateCont.FormatData)
            colName.Add(dd & VarIni.char8 & dd)
        Next

        Dim strsql As New SQLString("SFCTA", fldName)
        strsql.setLeftjoin("MOCTA", New List(Of String) From {
                           "MOCTA.TA001" & VarIni.C8 & "SFCTA.TA001",
                           "MOCTA.TA002" & VarIni.C8 & "SFCTA.TA002"
                           })
        strsql.setLeftjoin("COPTC", New List(Of String) From {
                           "COPTC.TC001" & VarIni.C8 & "MOCTA.TA026",
                           "COPTC.TC002" & VarIni.C8 & "MOCTA.TA027"
                           })
        strsql.setLeftjoin("COPMA", New List(Of String) From {"COPMA.MA001" & VarIni.C8 & "COPTC.TC004"})
        strsql.setLeftjoin("CMSMW", New List(Of String) From {"CMSMW.MW001" & VarIni.C8 & "SFCTA.TA004"})
        strsql.setLeftjoin("CMSMD", New List(Of String) From {"CMSMD.MD001" & VarIni.C8 & "SFCTA.TA006"})

        WHR &= dbConn.WHERE_IN("MOCTA.TA011", "Y,y", True, True)
        WHR &= dbConn.WHERE_IN("SFCTA.TA006", UcCblWc.getObject)
        WHR &= dbConn.WHERE_IN("SFCTA.TA001", UcDdlMoType.getObject)
        WHR &= dbConn.WHERE_LIKE("SFCTA.TA002", tbMO)
        WHR &= dbConn.WHERE_EQUAL("SFCTA.TA003", tbManfSeq)
        WHR &= dbConn.WHERE_IN("MOCTA.TA026", UcDdlSoType.getObject)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA027", tbSaleOrder)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA028", tbSaleSeq)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA006", tbItem)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA035", tbSpec)
        WHR &= dbConn.WHERE_LIKE("COPMA.MA002", tbCustName)

        If cbPlan.Checked Then
            WHR &= dbConn.WHERE_EQUAL(fldmobal, "0", ">")
        End If
        If cbWip.Checked Then
            WHR &= dbConn.WHERE_EQUAL(fldwip, "0", ">")
        End If

        strsql.SetWhere(WHR, True)
        strsql.SetOrderBy("SFCTA.TA001,SFCTA.TA002,SFCTA.TA003")
        Dim dt As DataTable = dbConn.Query(strsql.GetSQLString, VarIni.ERP, dbConn.WhoCalledMe)

        If dt.Rows.Count > rowCheck Then
            Return dt.Rows.Count
        End If
        Dim dtShow As DataTable = dtCont.setColDatatable(colName, VarIni.char8)
        Dim dateEnd As Date = DateBegin.AddDays(dateCount)
        Dim VAR_PLANQTY As String = NameOf(VAR_PLANQTY)
        Dim VAR_CANCELQTY As String = NameOf(VAR_CANCELQTY)
        Dim VAR_TRANS_QTY As String = NameOf(VAR_TRANS_QTY)
        Dim VAR_PLAN_DATE As String = NameOf(VAR_PLAN_DATE)
        'Dim itemHash As New Hashtable
        PlanShceduleHash = GetPlanData()

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim item As String = .Text("ITEM")
                Dim dtFld As New ArrayList From {
                    "DOC_NO",
                    "WC_SHOW", 'wc 
                    "PROCESS_SHOW", 'process
                    "SPEC",
                    "MO_QTY" & VarIni.char8,
                    "WIP_QTY" & VarIni.char8,
                    "MO_BAL_QTY" & VarIni.char8,
                    "TIME_STD" & VarIni.char8
                }

                Dim wc As String = .Text("TA006")
                Dim manfType As String = .Text("TA001")
                Dim manfOrder As String = .Text("TA002")
                Dim lineNo As String = .Text("TA003")

                Dim mo As String = .Text("DOC_NO")


                'get datafrom plan schedule
                'fldName = New ArrayList From {
                '    "PlanDate",
                '    "isnull(sum(case when PlanStatus='P' then isnull(PlanQty,0) else 0 end),0)" & VarIni.char8 & VAR_PLANQTY,
                '    "isnull(sum(case when PlanStatus='C' then isnull(PlanQty,0) else 0 end),0)" & VarIni.char8 & VAR_CANCELQTY
                '}
                'strsql = New SQLString("PlanSchedule", fldName, VarIni.char8)
                'WHR = strsql.WHERE_EQUAL("MoType", .Text("TA001"))
                'WHR &= strsql.WHERE_EQUAL("MoNo", .Text("TA002"))
                'WHR &= strsql.WHERE_EQUAL("MoSeq", .Text("TA003"))
                'WHR &= strsql.WHERE_EQUAL("WorkCenter", .Text("TA006"))
                'WHR &= strsql.WHERE_BETWEEN("PlanDate", DateBegin.ToString("yyyyMMdd"), dateEnd.ToString("yyyyMMdd"))
                'strsql.SetWhere(WHR, True)
                'strsql.SetGroupBy("PlanDate")

                'Dim drHash As Hashtable = dbConn.QueryHashDataRow(strsql.GetSQLString, VarIni.DBMIS, VAR_PLAN_DATE, dbConn.WhoCalledMe)

                Dim sumPlanQty As Decimal = 0
                Dim sumCancelQty As Decimal = 0
                Dim sumTransferQty As Decimal = 0
                Dim sumTransferQtyNot As Decimal = 0
                For i = 0 To dateCount
                    Dim dd As String = DateBegin.AddDays(i).ToString(dateCont.FormatData)
                    Dim txtShow As String = ""
                    With New DataRowControl(CType(hashCont.getDataHashDatarow(PlanShceduleHash, mo & dd), DataRow))
                        Dim pQty As Decimal = .Number("PLAN_QTY")
                        Dim cQty As Decimal = .Number("CANCLE_QTY")
                        Dim tQty As Decimal = .Number("TRANSFER_QTY")
                        Dim tQtyNot As Decimal = .Number("TRANSFER_QTY_NOT")
                        txtShow = pQty.ToString & VarIni.char9 & cQty.ToString & VarIni.char9 & tQty.ToString & VarIni.char9 & tQtyNot.ToString
                        sumPlanQty += pQty
                        sumCancelQty += cQty

                        sumTransferQty += tQty
                        sumTransferQtyNot += tQtyNot

                    End With
                    dtFld.Add(dd & VarIni.C8 & VarIni.C8 & txtShow)
                Next
                Dim txtSumShow As String = ""
                'plan qty
                If sumPlanQty > 0 Then
                    txtSumShow &= sumPlanQty.ToString("N0")
                End If
                If sumCancelQty > 0 Then
                    txtSumShow &= "(" & sumCancelQty.ToString("N0") & ")"
                End If
                dtFld.Add("PLAN_QTY" & VarIni.C8 & VarIni.C8 & txtSumShow)
                'transfer qty
                txtSumShow = ""
                If sumTransferQty > 0 Then
                    txtSumShow &= sumTransferQty.ToString("N0")
                End If
                If sumTransferQtyNot > 0 Then
                    txtSumShow &= "(" & sumTransferQtyNot.ToString("N0") & ")"
                End If
                dtFld.Add("TRANS_QTY" & VarIni.C8 & VarIni.C8 & txtSumShow)
                dtCont.addDataRow(dtShow, dr, dtFld, VarIni.C8)
            End With

        Next
        gvCont.GridviewInitial(gvShow, colName,,,,, VarIni.C8)
        gvCont.ShowGridView(gvShow, dtShow)
        CreateHeadGeridShow()
        Return dt.Rows.Count
    End Function

    Private Function GetPlanData() As Hashtable
        Dim C8 As String = VarIni.char8
        Dim fldName As New ArrayList From {
            "rtrim(P.MoType)+'*'+rtrim(P.MoNo)+'*'+rtrim(P.MoSeq)+rtrim(PlanDate) KEY_MAP",
            "P.MoType MO_TYPE",
            "rtrim(P.MoNo)MO_NO",
            "rtrim(P.MoSeq) MO_SEQ",
            "PlanDate PLAN_DATE",
            "SUM(case P.PlanStatus when 'P' then  PlanQty else 0 end)" & C8 & "PLAN_QTY",
            "SUM(case P.PlanStatus when 'C' then  PlanQty else 0 end)" & C8 & "CANCLE_QTY",
            "sum(V.TC014+V.TC016) " & C8 & "TRANSFER_QTY",
            "sum(V.TC014_NOT+V.TC016_NOT) " & C8 & "TRANSFER_QTY_NOT"
        }
        Dim strSQL As New SQLString(PLANSCHEDULE_T.table & " P", fldName)
        Dim sqlMO As String = "select A.TA001,A.TA002,A.TA003,A.TA004,A.TA006,B.TA015,B.TA011,B.TA026,B.TA027,B.TA035,A.TA010+A.TA013+A.TA016-A.TA011-A.TA012-A.TA014-A.TA015-A.TA048-A.TA056-A.TA058 WIP_QTY, isnull(cast(case B.TA015 when 0 then 0 else A.TA035/B.TA015 end as decimal(10,0)),0) as STD_MACH, isnull(cast(case B.TA015 when 0 then 0 else A.TA022/B.TA015 end as decimal(10,0)),0) as STD_MAN from " & VarIni.ERP & "..SFCTA A left join " & VarIni.ERP & "..MOCTA B on  B.TA001=A.TA001 and B.TA002=A.TA002 "
        'strSQL.setLeftjoin(" left join (" & sqlMO & ") M on M.TA001=P.MoType and M.TA002=P.MoNo AND M.TA003=P.MoSeq ")
        strSQL.setLeftjoin(dbConn.addBracket(sqlMO) & " M ", New List(Of String) From {
                           "M.TA001" & C8 & "P.MoType",
                           "M.TA002" & C8 & "P.MoNo",
                           "M.TA003" & C8 & "P.MoSeq"
                           })

        'strSQL.setLeftjoin(" left join " & VarIni.ERP & "..COPTC C on C.TC001=M.TA026 And C.TC002=M.TA027 ")
        strSQL.setLeftjoin(VarIni.ERP & "..COPTC C ", New List(Of String) From {
                           "C.TC001" & C8 & "M.TA026",
                           "C.TC002" & C8 & "M.TA027"
                           })
        ' strSQL.setLeftjoin(" left join " & VarIni.ERP & "..COPMA A on A.MA001=C.TC004 ")
        strSQL.setLeftjoin(VarIni.ERP & "..COPMA A ", New List(Of String) From {"A.MA001" & C8 & "C.TC004"})
        strSQL.setLeftjoin(VarIni.DBMIS & "..V_TRANSFER_SUM_DATE V", New List(Of String) From {
                          "V.TC004" & VarIni.C8 & "P.MoType",
                          "V.TC005" & VarIni.C8 & "P.MoNo",
                          "V.TC006" & VarIni.C8 & "P.MoSeq",
                          "V.TRANSFER_DATE" & VarIni.C8 & "PlanDate"
                           })
        Dim whr As String = ""
        whr = dbConn.WHERE_IN("M.TA006", UcCblWc.getObject,, True)
        whr &= dbConn.WHERE_IN("M.TA001", UcDdlMoType.getObject,, True)
        whr &= dbConn.WHERE_LIKE("M.TA002", tbMO)
        'whr &= dbConn.Where(SFCB.RunCard, tbRuncard, False)
        whr &= dbConn.WHERE_LIKE("M.TA003", tbManfSeq)
        whr &= dbConn.WHERE_IN("M.TA026", UcDdlSoType.getObject)
        whr &= dbConn.Where("M.TA027", tbSaleOrder)
        whr &= dbConn.Where("M.TA028", tbSaleSeq)
        'whr &= dbConn.Where(SFAA.OldRefereanceDocLineNo, tbSaleSeq)
        'whr &= dbConn.Where(SFAA.BatchNumber, tbBatch)
        whr &= dbConn.WHERE_IN("M.TA011", "y,Y", True, True)
        whr &= dbConn.WHERE_LIKE("M.ITEM", tbItem)
        whr &= dbConn.WHERE_LIKE("M.TA035", tbSpec)
        whr &= dbConn.Where("C.TC004", tbCustCode)
        whr &= dbConn.Where("A.MA002", tbCustName)
        strSQL.SetWhere(whr, True)
        ' strSQL.SetGroupBy("WorkCenter,PlanDate")
        strSQL.SetGroupBy("P.MoType,P.MoNo,P.MoSeq,PlanDate")
        Return dbConn.QueryHashDataRow(strSQL.GetSQLString, VarIni.DBMIS, "KEY_MAP", dbConn.WhoCalledMe)
    End Function

    Private Function GetWhereDatePlan(fldDate As String) As String
        Dim a1 As String = ucDateFrom.TextEmptyToday
        Dim a2 As String = ucDateTo.TextEmptyToday
        Dim dateStart As Date = dateCont.strToDateTime(ucDateFrom.TextEmptyToday, dateCont.FormatData)
        Dim dateEnd As Date = dateCont.strToDateTime(ucDateTo.TextEmptyToday, dateCont.FormatData)
        Dim getDateCount As Integer = DateDiff(DateInterval.Day, dateStart, dateEnd)

        If getDateCount = 0 And dateStart.ToString(dateCont.FormatData) = Date.Now.ToString(dateCont.FormatData) Then
            dateStart = dateStart.AddDays(-1)
            dateEnd = dateStart.AddDays(7)
        ElseIf getDateCount < 0 Then
            dateStart = Date.Now.AddDays(-1)
            dateEnd = dateStart.AddDays(7)
        End If
        Return dbConn.WHERE_BETWEEN(fldDate, dateStart.ToString(dateCont.FormatData), dateEnd.ToString(dateCont.FormatData))
    End Function

    Private Function GetCapWorkcenterUse() As Hashtable
        Dim whr As String = dbConn.WHERE_IN("WorkCenter", UcCblWc.getObject,, True)
        whr &= GetWhereDatePlan("PlanDate")
        Dim sqlDailyPlan As String = "select  rtrim(WorkCenter)+rtrim(PlanDate) KeyMap, WorkCenter WORK_CENTER,PlanDate PLAN_DATE,sum(PlanTime) PLAN_TIME,count(*) PLAN_COUNT from " & PLANSCHEDULE_T.table & " where PlanStatus='P'  " & whr & " group by WorkCenter,PlanDate "
        Return dbConn.QueryHashDataRow(sqlDailyPlan, VarIni.DBMIS, "KeyMap", dbConn.WhoCalledMe)
    End Function



    Private Function getCapWorkcenter() As Hashtable
        Dim sqlBody As String = "(select rtrim(MX002) MX002,SUM((CMSMX.UDF51+UDF52)*CMSMX.UDF53) STD_TIME,COUNT(*) MCH_COUNT from CMSMX GROUP BY MX002) CMSMX"
        Dim fldName As New ArrayList From {
            "MD001",
            "MD002",
            "CMSMD.UDF02",
            "isnull(STD_TIME,0) STD_TIME",
            "MCH_COUNT"
        }
        Dim strSQL As New SQLString("CMSMD", fldName)
        strSQL.setLeftjoin(" left join " & sqlBody & " on MX002=MD001 ")
        Return dbConn.QueryHashDataRow(strSQL.GetSQLString, VarIni.ERP, "MD001", dbConn.WhoCalledMe)
    End Function

    Private Sub CreateHeadGeridShow()
        If gvShow.Rows.Count > 0 Then
            Dim i As Integer = colStart
            Dim SQL As String = String.Empty
            Dim dToday As String = DateTime.Now.ToString("yyyyMMdd")
            Dim capHash As Hashtable = getCapWorkcenter()           'key map => MD001(Workcenter)
            Dim wcPlanDateHash As Hashtable = GetCapWorkcenterUse() 'keymap=> Workcenter+Plandate
            Dim wcAuthority As List(Of String) = UcCblWc.getValueAllList()
            While i < gvShow.HeaderRow.Cells.Count
                Dim dateHead As String = gvShow.HeaderRow.Cells(i).Text.Trim
                Dim PlanDate As Date = dateCont.strToDateTime(dateHead, dateCont.FormatData)
                Dim DayOfWeek As String = PlanDate.ToString("ddd")

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
                Dim moType As String = String.Empty
                Dim moNumber As String = String.Empty
                Dim moSeq As String = String.Empty

                For Each row As GridViewRow In gvShow.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        With row
                            Dim timeStd As Integer = outCont.checkNumberic(.Cells(9).Text)
                            Dim moData As String = .Cells(0).Text.Trim
                            Dim mo() As String = moData.Split("*")
                            moType = mo(0)
                            moNumber = mo(1)
                            moSeq = mo(2)
                            Dim wc() As String = .Cells(1).Text.Trim.Split(":")
                            Dim txtWc As String = wc(0)

                            Dim drCap As New DataRowControl(hashCont.getDataHashDatarow(capHash, txtWc))
                            Dim drCapUse As New DataRowControl(hashCont.getDataHashDatarow(wcPlanDateHash, txtWc & PlanDate.ToString("yyyyMMdd")))

                            Dim planUsageTime As Decimal = drCapUse.Number("PLAN_TIME")
                            Dim capWC As Decimal = drCap.Number("STD_TIME")
                            Dim mchCnt As Integer = drCap.Number("MCH_COUNT")

                            Dim wipQty As Decimal = outCont.checkNumberic(.Cells(5).Text)
                            Dim moBalQty As Decimal = outCont.checkNumberic(.Cells(6).Text)

                            With .Cells(i)
                                Dim planQty As Decimal = 0
                                Dim cancleQty As Decimal = 0
                                Dim transferQty As Decimal = 0
                                Dim transferQtyNot As Decimal = 0

                                Dim txts As String() = .Text.Trim.Split(VarIni.char9)
                                Dim rowNumber As Integer = 0
                                For Each txt As String In txts
                                    Dim valTxt As Decimal = outCont.checkNumberic(txt)
                                    Select Case rowNumber
                                        Case 0
                                            planQty = valTxt
                                        Case 1
                                            cancleQty = valTxt
                                        Case 2
                                            transferQty = valTxt
                                        Case 3
                                            transferQtyNot = valTxt
                                    End Select
                                    rowNumber += 1
                                Next

                                Dim txtShow As String = String.Empty
                                Dim txtTooltip As String = String.Empty
                                Dim backColor As Color = Color.LightSlateGray
                                Dim foreColor As Color = .ForeColor
                                If timeStd <= 0 Then
                                    txtTooltip &= "No Standard time(PE),"
                                End If
                                If planQty > 0 OrElse cancleQty > 0 OrElse transferQty > 0 OrElse transferQtyNot > 0 Then
                                    If planQty > 0 Then
                                        Dim pQtyShow As String = planQty.ToString("N0", New CultureInfo("en-us"))
                                        txtTooltip &= "Plan Qty=" & pQtyShow & ","
                                        txtShow &= pQtyShow
                                    End If
                                    If cancleQty > 0 Then
                                        txtShow &= "(" & cancleQty.ToString("N0", New CultureInfo("en-us")) & ")"
                                    End If
                                    If transferQty > 0 Then
                                        txtTooltip &= "Transfer Qty=" & transferQty.ToString("N0", New CultureInfo("en-us")) & ","
                                        Dim balanceQty As Decimal = planQty - transferQty
                                        If balanceQty > 0 And transferQty > 0 Then 'transver qty partial of plan qty
                                            foreColor = Color.Green
                                        ElseIf balanceQty <= 0 Then 'transfer qty to over plan qty
                                            txtTooltip &= ",Status = ON PLAN "
                                        End If
                                    Else
                                        foreColor = Color.Yellow
                                    End If
                                    If transferQtyNot > 0 Then
                                        txtTooltip &= "Transfer Qty Not Approve=" & transferQtyNot.ToString("N0", New CultureInfo("en-us")) & ","
                                    End If
                                End If

                                If mchCnt > 0 Then
                                    txtTooltip &= "Mach/Line(Day)=" & mchCnt.ToString("N0", New CultureInfo("en-us")) & ","
                                Else
                                    txtTooltip &= "Not Have Mach/Line for Day,"
                                End If

                                If capWC = 0 Then
                                    backColor = Color.LightYellow
                                    txtTooltip &= "Not have capacity,"
                                Else
                                    If planUsageTime > capWC Then
                                        backColor = Color.Red
                                        txtTooltip &= "Capacity Over Load,"
                                    Else
                                        Dim pCap As Decimal = Math.Round((planUsageTime / capWC) * 100)
                                        If pCap >= 90 Then
                                            backColor = Color.Orange
                                        End If
                                        txtTooltip &= "Capacity Balance=" & dateCont.TimeFormat(capWC - planUsageTime) & ","
                                    End If
                                End If
                                .ForeColor = foreColor
                                .ToolTip = txtTooltip.Substring(0, txtTooltip.Length - 1)
                                .Text = txtShow
                                .HorizontalAlign = HorizontalAlign.Center

                                If wcAuthority.Contains(txtWc) AndAlso timeStd > 0 AndAlso PlanDate.Date >= DateTime.Now.Date AndAlso (wipQty > 0 OrElse moBalQty > 0) Then
                                    .BackColor = backColor
                                    .Attributes.Add("style", "cursor:pointer;")
                                    .Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFCCFF'")
                                    .Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                                    .Attributes.Add("onclick", "NewWindow('PlanScheduleManfOrder.aspx?plandate=" & outCont.EncodeTo64UTF8(dateHead) & "&motype=" & outCont.EncodeTo64UTF8(moType) & "&mono=" & outCont.EncodeTo64UTF8(moNumber) & "&moseq=" & outCont.EncodeTo64UTF8(moSeq) & "','PlanScheduleManfOrderPop',1000,750,'yes')")
                                End If
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
            Dim indexStart As Integer = colStart
            Dim HeaderRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell2 As New TableCell With {
                .Text = "Information"
            }
            HeaderCell2.Font.Bold = True
            HeaderCell2.ForeColor = ColorTranslator.FromHtml("#DF7401")
            HeaderCell2.ColumnSpan = indexStart
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell2)
            gvShow.Controls(0).Controls.AddAt(0, HeaderRow)

            Dim HeaderCell As New TableCell With {
                .Text = "Date Between " & "<span style='color:#DF7401;'>" & "  " & DateBegin.ToString("dd-MMM-yyyy") & " ~ " & DateBegin.AddDays(CountDate).ToString("dd-MMM-yyyy") & "</span>",
                .ColumnSpan = (e.Row.Cells.Count - indexStart)
            }
            HeaderCell.Font.Bold = True
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell)
            gvShow.Controls(0).Controls.AddAt(1, HeaderRow)
        End If
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "MouseEventsSearch(this, event)")
            e.Row.Attributes.Add("onmouseout", "MouseEventsSearch(this, event)")
        End If
    End Sub
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If gvShow.Rows.Count <= 0 Then
            MessageAlert.Show(Me, "Not data found")
        Else
            ExportsUtility.ExportGridviewToMsExcel("PlanSheduleList", gvShow)
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub


End Class