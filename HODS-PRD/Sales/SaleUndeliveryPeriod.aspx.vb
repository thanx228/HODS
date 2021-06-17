Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Imports System.Globalization

Public Class SaleUndeliveryPeriod
    Inherits System.Web.UI.Page
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl
    Dim hashCont As New HashtableControl
    Dim exCont As New ExportImportControl

    Dim Hash As New Hashtable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            ChkSOType.setObject = "22"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        If ddlReport.SelectedValue = "0" Then
            ShowReport(True)
        Else
            showReportDetail(True)
        End If
    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        If ddlReport.SelectedValue = "0" Then
            ShowReport(False)
        Else
            showReportDetail(False)
        End If
    End Sub


    Function strToDate(ByVal strDate As String, Optional ByVal dateFormat As String = "yyyyMMdd") As Date
        Return DateTime.ParseExact(strDate, dateFormat, New CultureInfo("en-US"))
    End Function

    Function getFirstWeek(ByVal selDate As Date, Optional daystart As Decimal = 0) As Date 'o=sunday,1=monday...6=Saturday
        Return selDate.AddDays(-(selDate.DayOfWeek - daystart))
    End Function

    Function getLastWeek(ByVal selDate As Date, Optional dayend As Decimal = 6) As Date 'o=sunday,1=monday...6=Saturday
        Return selDate.AddDays(-(selDate.DayOfWeek - dayend))
    End Function

    Function getWeek(ByVal selDate As Date, Optional dateStart As Microsoft.VisualBasic.FirstDayOfWeek = 1) As Integer
        Return DatePart("ww", selDate, dateStart)
    End Function


    Function getWeekNumber(yearChk As Integer, yearPeriod As Integer, dateVal As Date, Optional strYear As Boolean = True) As Integer
        Dim dateReturn As New Date

        If yearChk = yearPeriod Then
            dateReturn = dateVal
        Else
            Dim dm As String = "1231"
            If strYear Then
                dm = "0101"
            End If
            dateReturn = strToDate(yearChk & dm)
        End If
        Return getWeek(dateReturn, 1)
    End Function

    Private Function GetWeekStartDate(year As Integer, weekNumber As Integer) As Date
        Dim startDate As New DateTime(year, 1, 1)
        Dim weekDate As DateTime = DateAdd(DateInterval.WeekOfYear, weekNumber - 1, startDate)
        Return DateAdd(DateInterval.Day, (-weekDate.DayOfWeek), weekDate)
    End Function

    Function setColName(dateInput As Date, i As Integer, value As String) As String
        Dim ReturnDate As String = String.Empty
        If value = "Day" Then
            ReturnDate = "D" & dateInput.AddDays(i).ToString("yyMMdd", CultureInfo.InvariantCulture)
        ElseIf value = "Week" Then
            ReturnDate = "W" & dateInput.AddDays(i).ToString("yyMMdd", CultureInfo.InvariantCulture)
        ElseIf value = "Month" Then
            ReturnDate = "M" & dateInput.AddMonths(i).ToString("yyMM", CultureInfo.InvariantCulture)
        End If
        Return ReturnDate
    End Function

    Private Function valueChk(InputDdl As DropDownList, beginDate As Date, lastDate As Date, showValueDdl As Boolean) As String
        Dim ddlType As String = ddlSummary.SelectedValue
        Dim val As Integer = 0
        Dim valDdl As String = String.Empty
        If ddlType = "0" Then
            val = DateDiff(DateInterval.Day, beginDate, lastDate)
            valDdl = "Day"
        ElseIf ddlType = "1" Then
            val = DateDiff(DateInterval.Weekday, beginDate, lastDate)
            valDdl = "Week"
        ElseIf ddlType = "2" Then
            val = DateDiff(DateInterval.Month, beginDate, lastDate)
            valDdl = "Month"
        End If
        If showValueDdl = True Then
            Return valDdl
        Else
            Return val
        End If
    End Function

    'เงื่อนไข ddlReport = Default
    Private Sub ShowReport(excel As Boolean)

        If DateF.Text = String.Empty Or DateT.Text = String.Empty Then
            show_message.ShowMessage(Page, "กรุณาเลือกวันที่ !", UpdatePanel1)
            Exit Sub
        End If

        Dim Condition As String = ddlCondition.SelectedValue

        Dim beginDate As Date = DateTime.ParseExact(DateF.Text, "yyyyMMdd", CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(DateT.Text, "yyyyMMdd", CultureInfo.InvariantCulture)

        Dim CbeginDate As String = beginDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
        Dim ClastDate As String = lastDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)

        Dim strYear As String = beginDate.ToString("yyyy", New CultureInfo("en-US"))
        Dim endYear As String = lastDate.ToString("yyyy", New CultureInfo("en-US"))

        Dim listValueName As String = valueChk(ddlSummary, beginDate, lastDate, True)

        Dim listValueNum As Integer = 0
        If listValueName = "Week" Then
            listValueNum = (getWeek(lastDate) - getWeek(beginDate))
        Else
            listValueNum = valueChk(ddlSummary, beginDate, lastDate, False)
        End If


        Dim colName As New ArrayList
        colName.Add("item:Item")
        colName.Add("Item Desc:Item Desc")
        colName.Add("Spec:Spec")
        colName.Add("Back log:Back log:2")

        If listValueName = "Week" Then

            For i As Integer = strYear To endYear
                Dim firstWeek As Integer = getWeekNumber(i, strYear, beginDate)
                Dim lastWeek As Integer = getWeekNumber(i, endYear, lastDate, False)
                For y As Integer = firstWeek To lastWeek
                    Dim val As String = GetWeekStartDate(i, y).ToString("dd/MMM")
                    colName.Add(val & ":" & val & ":2")
                Next
            Next

        Else

            For i As Integer = 0 To listValueNum
                Dim val As String = setColName(beginDate, i, listValueName)
                colName.Add(val & ":" & val & ":2")
            Next

        End If

        colName.Add("Stock Qty:Stock Qty:2")
        colName.Add("Undelivery Qty:Undelivery Qty:2")
        colName.Add("MO Qty:MO Qty:2")
        colName.Add("PO Qty:PO Qty:2")
        colName.Add("PR Qty:PR Qty:2")
        Dim dtShow As DataTable = dtCont.setColDatatable(colName)

        Dim c8 As String = VarIni.C8
        Dim FldFrom As New ArrayList From {
               "COPTD.TD004"
            }
        Dim sqlStrFrom As New SQLString("COPTD", FldFrom, strSplit:=c8)
        With sqlStrFrom
            .setLeftjoin("COPTC", New List(Of String) From {
                "COPTC.TC001" & c8 & "COPTD.TD001",
                "COPTC.TC002" & c8 & "COPTD.TD002"
               }
            )

            .SetWhere(.WHERE_EQUAL("COPTD.TD016", "N"), True)
            .SetWhere(.WHERE_EQUAL("COPTC.TC027", "Y"))

            .SetWhere(.WHERE_IN("COPTC.TC001", ChkSOType.getObject,, True))
            .SetWhere(.WHERE_EQUAL("COPTC.TC002", txtSONo.Text))
            .SetWhere(.WHERE_EQUAL("COPTD.TD004", txtItemNo.Text))
            .SetWhere(.WHERE_EQUAL("COPTD.TD004", txtItemNo.Text))
            .SetWhere(.WHERE_BETWEEN("COPTD.TD013", DateF.Text, DateT.Text))
            .SetWhere(.WHERE_EQUAL("COPTC.TC004", txtCustID.Text.Trim))
            .SetWhere(.WHERE_EQUAL("COPTD.TD003", txtSeq.Text))

            .SetGroupBy("COPTD.TD004")

        End With

        Dim flaName As New ArrayList From {
               "COPTD.TD004" & c8 & "TD004",
               "MB002",
               "MB003",
               "STOCK",
               "MO",
               "PO",
               "PR"
            }
        Dim sqlStr As New SQLString(dbConn.addBracket(sqlStrFrom.GetSQLString) & " COPTD", flaName, strSplit:=c8)
        With sqlStr

            'Stock
            Dim JoinStock As New ArrayList From {
                "MC001",
                "sum(MC007)" & c8 & "STOCK"
                }
            Dim sqlJoinStock As New SQLString("INVMC", JoinStock, strSplit:=c8)
            With sqlJoinStock

                .SetGroupBy("MC001")
            End With

            .setLeftjoin(dbConn.addBracket(sqlJoinStock.GetSQLString) & " INVMC", New List(Of String) From {
                "INVMC.MC001" & c8 & "COPTD.TD004"
               }
             )

            'MO
            Dim JoinMO As New ArrayList From {
                "TA006",
                "SUM(isnull(TA015,0)-isnull(TA017,0)-isnull(TA018,0))" & c8 & "MO"
                }
            Dim sqlJoinMO As New SQLString("MOCTA", JoinMO, strSplit:=c8)
            With sqlJoinMO

                .SetWhere(.WHERE_IN("TA011", "y,Y", True, True), True)
                .SetWhere(.WHERE_EQUAL("TA013", "Y"))
                .SetGroupBy("TA006")
            End With

            .setLeftjoin(dbConn.addBracket(sqlJoinMO.GetSQLString) & " MOCTA", New List(Of String) From {
                "MOCTA.TA006" & c8 & "COPTD.TD004"
               }
             )

            'PO
            Dim JoinPO As New ArrayList From {
                "TD004",
                "SUM(isnull(TD008,0)-isnull(TD015,0))" & c8 & "PO"
                }
            Dim sqlJoinPO As New SQLString("PURTD", JoinPO, strSplit:=c8)
            With sqlJoinPO

                .SetWhere(.WHERE_EQUAL("TD016", "N"), True)
                .SetGroupBy("TD004")
            End With

            .setLeftjoin(dbConn.addBracket(sqlJoinPO.GetSQLString) & " PURTD", New List(Of String) From {
                "PURTD.TD004" & c8 & "COPTD.TD004"
               }
             )

            'PR
            Dim JoinPR As New ArrayList From {
                "TB004",
                "sum(TR006)" & c8 & "PR"
                }
            Dim sqlJoinPR As New SQLString("PURTB", JoinPR, strSplit:=c8)
            With sqlJoinPR

                .setLeftjoin("PURTR", New List(Of String) From {
                    "TR001" & c8 & "TB001",
                    "TR002" & c8 & "TB002",
                    "TR003" & c8 & "TB003"
                    }
                )

                .SetWhere(.WHERE_EQUAL("TB039", "N"), True)
                .SetGroupBy("TB004")
            End With

            .setLeftjoin(dbConn.addBracket(sqlJoinPR.GetSQLString) & " PURTB", New List(Of String) From {
                "PURTB.TB004" & c8 & "COPTD.TD004"
               }
             )

            .setLeftjoin("INVMB", New List(Of String) From {
                "INVMB.MB001" & c8 & "COPTD.TD004"
               }
             )

            .SetWhere(.WHERE_EQUAL("MB003", txtSpec.Text), True)

            .SetOrderBy("COPTD.TD004")
        End With

        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString, VarIni.ERP, dbConn.WhoCalledMe())

        Dim SumUndel As Hashtable = hashUnDelivery(CbeginDate, ClastDate, listValueName, False)
        Dim SumUndelBack As Hashtable = hashUnDelivery(CbeginDate, ClastDate, listValueName, True)

        For Each dr As DataRow In dt.Rows
            Dim drC As New DataRowControl(dr)
            Dim dataFld1 As New ArrayList
            With drC
                Dim ShearchItemNo As String = .Text("TD004"),
                    Item As String = .Text("TD004"),
                    ItemDec As String = .Text("MB002").Replace("'", " "),
                    Spec As String = .Text("MB003").Replace("'", " "),
                    stockQty As String = .Number("STOCK"),
                    MoQty As String = .Number("MO"),
                    poQty As String = .Number("PO"),
                    prQty As String = .Number("PR"),
                    UndelQty As Decimal = 0.0,
                    UndelQtyBack As Decimal = 0.0

                '--Sum Back Log
                If hashCont.existDataHash(SumUndelBack, ShearchItemNo) Then
                    Dim Qty As Decimal = SumUndelBack.Item(ShearchItemNo)
                    UndelQtyBack = Qty
                End If

                Dim QtyListArl(listValueNum) As Decimal

                '--Sum UndelQty
                Dim sumQty As Decimal = 0
                Dim sQty As Decimal = 0
                If listValueName = "Week" Then

                    For i As Integer = strYear To endYear
                        Dim firstWeek As Integer = getWeekNumber(i, strYear, beginDate)
                        Dim lastWeek As Integer = getWeekNumber(i, endYear, lastDate, False)
                        Dim ArZero As Integer = 0
                        For y As Integer = firstWeek To lastWeek

                            Dim setDate As Integer = GetWeekStartDate(i, y).ToString("yyyyMMdd")
                            Dim PlusDate As Date = DateTime.ParseExact(setDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                            For x As Integer = 0 To 6
                                Dim formatDate As String = "W" & PlusDate.ToString("yyMMdd", New CultureInfo("en-US"))
                                If hashCont.existDataHash(SumUndel, ShearchItemNo & "•" & formatDate) Then
                                    Dim Qty As Decimal = SumUndel.Item(ShearchItemNo & "•" & formatDate)
                                    sumQty += Qty
                                    QtyListArl(ArZero) += Qty
                                End If
                                PlusDate = PlusDate.AddDays(1)
                                'setDate += 1

                            Next
                            ArZero += 1
                        Next

                    Next


                Else
                    For i As Integer = 0 To listValueNum
                        Dim formatDate As String = setColName(beginDate, i, listValueName)
                        If hashCont.existDataHash(SumUndel, ShearchItemNo & "•" & formatDate) Then
                            Dim Qty As Decimal = SumUndel.Item(ShearchItemNo & "•" & formatDate)
                            sumQty += Qty
                            QtyListArl(i) = Qty
                        End If
                    Next

                End If

                UndelQty = sumQty


                dataFld1.Add("Item::" & Item)
                dataFld1.Add("Item Desc::" & ItemDec)
                dataFld1.Add("Spec::" & Spec)
                dataFld1.Add("Back log::" & UndelQtyBack)

                If listValueName = "Week" Then

                    For i As Integer = strYear To endYear
                        Dim firstWeek As Integer = getWeekNumber(i, strYear, beginDate)
                        Dim lastWeek As Integer = getWeekNumber(i, endYear, lastDate, False)
                        Dim ArZero As Integer = 0
                        For y As Integer = firstWeek To lastWeek

                            Dim val As String = GetWeekStartDate(i, y).ToString("dd/MMM")
                            dataFld1.Add(val & "::" & QtyListArl(ArZero))
                            ArZero += 1

                        Next
                    Next

                Else
                    For i As Integer = 0 To listValueNum
                        dataFld1.Add(setColName(beginDate, i, listValueName) & "::" & QtyListArl(i))
                    Next
                End If

                dataFld1.Add("Stock Qty::" & stockQty)
                dataFld1.Add("Undelivery Qty::" & UndelQty)
                dataFld1.Add("MO Qty::" & MoQty)
                dataFld1.Add("PO Qty::" & poQty)
                dataFld1.Add("PR Qty::" & prQty)

                Dim supply As String = .Number("STOCK") + (.Number("MO") + .Number("PO") + .Number("PR"))
                Dim showData As Boolean = False
                Select Case Condition
                    Case "0"
                        'All
                        showData = True
                    Case "1"
                        ' stockQty < UndelQty
                        If stockQty < UndelQty Then
                            showData = True
                        End If
                    Case "2"
                        ' supply >= undelivery
                        If supply >= UndelQty Then
                            showData = True
                        End If
                    Case "3"
                        ' supply < undelivery
                        If supply < UndelQty Then
                            showData = True
                        End If
                    Case "4"
                        ' stockQty > undelivery
                        If stockQty >= UndelQty Then
                            showData = True
                        End If
                End Select
                If showData = True Then
                    dtCont.addDataRow(dtShow, dr, dataFld1)
                End If

            End With

        Next
        If excel Then
            exCont.Export("SO Undelivery Period", dtShow)
        Else
            gvCont.ShowGridView(GvUndelPeriod, dtShow)
            CountRow1.RowCount = gvCont.rowGridview(GvUndelPeriod)
        End If

    End Sub

    'เงื่อนไข ddlReport = Detail
    Private Sub showReportDetail(excel As Boolean)
        If DateF.Text = String.Empty Or DateT.Text = String.Empty Then
            show_message.ShowMessage(Page, "กรุณาเลือกวันที่ !", UpdatePanel1)
            Exit Sub
        End If

        Dim Al As New ArrayList
        Dim fldName As New ArrayList
        Dim colName As New ArrayList
        Dim fldNumber As New ArrayList
        Dim c8 As String = VarIni.C8
        With New ArrayListControl(Al)
            .TAL("DateDiff(Day, Convert(Date, TG042), Convert(Date, TG003))" & c8 & "Day", "Days")
            .TAL("MA014" & c8 & "Currency", "Currency")
            .TAL("TG001+'-'+TG002" & c8 & "Deli_Doc", "Delivery No")
            .TAL("TH003", "Delivery Seq")
            .TAL("(SUBSTRING(TG003,7,2)+'/'+SUBSTRING(TG003,5,2)+'/'+SUBSTRING(TG003,1,4))" & c8 & "Deli_Date", "Delivery Date")
            .TAL("(SUBSTRING(TG042,7,2)+'/'+SUBSTRING(TG042,5,2)+'/'+SUBSTRING(TG042,1,4))" & c8 & "Order_Date", "Order Date")
            .TAL("TG023", "Status")
            .TAL("TG006", "Sale Person")
            .TAL("TH014+'-'+TH015" & c8 & "SO_No", "SO No")
            .TAL("TH016", "SO Seq")
            .TAL("RTRIM(TG004)+'-'+MA003" & c8 & "Customer", "Customer")
            .TAL("TH030", "Customer PO")
            .TAL("TH004", "Item")
            .TAL("MB002", "Item Name")
            .TAL("MB003", "Spec")
            .TAL("TH009", "Unit")
            .TAL("TH008", "Delivery Qty", "2")
            .TAL("RTRIM(TH007)+'-'+MC002" & c8 & "WH", "Warehouse")
            .TAL("STOCK", "Stock Qty", "2")
            .TAL("MO", "MO Qty", "2")
            .TAL("PO", "PO Qty", "2")

            fldName = .ChangeFormat(False)
            colName = .ChangeFormat(True)
            fldNumber = .ChangeFormat()
        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, c8)

        Dim sqlStr As New SQLString("COPTG", fldName, strSplit:=c8)
        With sqlStr
            .setLeftjoin("COPTH", New List(Of String) From {
                "TH001" & c8 & "TG001",
                "TH002" & c8 & "TG002"
                }
            )

            .setLeftjoin("COPMA", New List(Of String) From {
                "TG004" & c8 & "MA001"
                }
            )

            .setLeftjoin("INVMB", New List(Of String) From {
                "MB001" & c8 & "TH004"
               }
            )

            .setLeftjoin("CMSMC", New List(Of String) From {
                "TH007" & c8 & "MC001"
                }
            )

            'Stock
            Dim FldStock As New ArrayList From {
                "MC001",
                "SUM(isnull(MC007, 0))" & c8 & "STOCK"
                }
            Dim sqlStrStock As New SQLString("INVMC", FldStock, c8)
            With sqlStrStock

                .SetGroupBy("MC001")
            End With

            .setLeftjoin(dbConn.addBracket(sqlStrStock.GetSQLString) & " INVMC", New List(Of String) From {
                "INVMC.MC001" & c8 & "TH004"
               }
            )

            'MO
            Dim FldMO As New ArrayList From {
                "TA006",
                "SUM(isnull(TA015, 0) - isnull(TA017, 0) - isnull(TA018, 0))" & c8 & "MO"
                }
            Dim sqlStrMO As New SQLString("MOCTA", FldMO, c8)
            With sqlStrMO

                .SetWhere(.WHERE_EQUAL("TA013", "Y"), True)
                .SetWhere(.WHERE_IN("TA011", "y,Y", True, True))
                .SetGroupBy("TA006")
            End With

            .setLeftjoin(dbConn.addBracket(sqlStrMO.GetSQLString) & " MOCTA", New List(Of String) From {
                "MOCTA.TA006" & c8 & "TH004"
               }
            )

            'PO
            Dim FldPO As New ArrayList From {
                "TD004",
                "SUM(isnull(TD008, 0) - isnull(TD015, 0))" & c8 & "PO"
                }
            Dim sqlStrPO As New SQLString("PURTD", FldPO, c8)
            With sqlStrPO

                .SetWhere(.WHERE_EQUAL("TD016", "N"), True)
                .SetGroupBy("TD004")
            End With

            .setLeftjoin(dbConn.addBracket(sqlStrPO.GetSQLString) & " PURTD", New List(Of String) From {
                "PURTD.TD004" & c8 & "TH004"
               }
            )

            .SetWhere(.WHERE_IN("TH014", ChkSOType.getObject,, True), True)
            .SetWhere(.WHERE_EQUAL("TH015", txtSONo.Text))
            .SetWhere(.WHERE_EQUAL("TH004", txtItemNo.Text))
            .SetWhere(.WHERE_EQUAL("MB003", txtSpec.Text))
            .SetWhere(.WHERE_BETWEEN("TG042", DateF.Text, DateT.Text))
            .SetWhere(.WHERE_EQUAL("TG004", txtCustID.Text.Trim))
            .SetWhere(.WHERE_EQUAL("TH016", txtSeq.Text))

            .SetOrderBy("TG001, TG002,TH003")
        End With


        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        Dim Count As Integer = dt.Rows.Count
        If excel = False Then
            If Count >= VarIni.LimitGridView Then
                show_message.ShowMessage(Page, "จำนวนข้อมูลมากกว่า 500 Rows (" & Count & ")\nกรุณา Export Excel.", UpdatePanel1)
                Exit Sub
            End If
        End If

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, fldNumber, fldManual)
            End With
        Next


        If excel Then
            ExportsUtility.ExportGridviewToMsExcel("SO Undelivery Period Detail", GvUndelPeriod)
        Else
            gvCont.GridviewInitial(GvUndelPeriod, colName, strSplit:=c8)
            gvCont.ShowGridView(GvUndelPeriod, dtShow)
            CountRow1.RowCount = dtShow.Rows.Count
        End If

    End Sub


    Private Function hashUnDelivery(DateFrom As String, DateTo As String, FormatSummary As String, UnDeliveryBack As Boolean) As Hashtable
        Dim c8 As String = VarIni.C8
        Dim Whr As String = String.Empty
        Dim Format As String = String.Empty
        Dim CodeFormat As String = String.Empty
        Dim fldSumMonth As String = String.Empty
        Dim Hash As New Hashtable
        Dim dt As New DataTable

        If FormatSummary = "Month" Then
            fldSumMonth = "SUBSTRING(TD013,3,2)+''+SUBSTRING(TD013,5,2)"
            CodeFormat = "'M'"
        Else
            fldSumMonth = "SUBSTRING(TD013,3,2)+''+SUBSTRING(TD013,5,2)+''+SUBSTRING(TD013,7,2)"
            If FormatSummary = "Day" Then
                CodeFormat = "'D'"
            ElseIf FormatSummary = "Week" Then
                CodeFormat = "'W'"
            End If
        End If

        Dim fldUndelQty As New ArrayList From {
            CodeFormat & "+''+" & fldSumMonth & c8 & "SUMMONTH",
            "TD004",
            "SUM(TD008 + TD024 - TD009 - TD025)" & c8 & "undelQty"
        }

        Dim SqlUndelQty As New SQLString("COPTD", fldUndelQty, strSplit:=c8)
        With SqlUndelQty

            .setLeftjoin(vbCrLf & "COPTC", New List(Of String) From {
                "TC001" & c8 & "TD001",
                "TC002" & c8 & "TD002"
            }
        )

            .SetWhere(.WHERE_EQUAL("TD016", "N"), True)
            .SetWhere(.WHERE_EQUAL("TC027", "Y"))

            If UnDeliveryBack = True Then
                .SetWhere(.WHERE_EQUAL("TD013", DateF.Text, "<", False))
                fldUndelQty.RemoveAt(0)

            Else
                .SetWhere(.WHERE_BETWEEN("TD013", DateF.Text, DateT.Text))
            End If


            .SetGroupBy(If(ddlSummary.SelectedValue = "2", "SUBSTRING(TD013,3,2), SUBSTRING(TD013,5,2), TD004", "TD013, TD004"))


        End With
        dt = dbConn.Query(SqlUndelQty.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe())

        For Each dr As DataRow In dt.Rows
            Dim DrC As New DataRowControl(dr)
            With DrC
                If UnDeliveryBack = True Then
                    hashCont.addDataHash(Hash, .Text("TD004"), .Text("undelQty"))
                Else
                    hashCont.addDataHash(Hash, .Text("TD004") & "•" & .Text("SUMMONTH"), .Text("undelQty"))
                End If
            End With
        Next

        Return Hash
    End Function

    Protected Sub GvUndelPeriod_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvUndelPeriod.RowDataBound
        With e.Row

            If .RowType = DataControlRowType.DataRow Then

                If ddlReport.SelectedValue = "0" Then

                    Dim hplDetail As HyperLink = CType(.FindControl("ShowDetail"), HyperLink)
                    Dim item As String = .DataItem("item").ToString.Replace("Null", "").ToString.Replace("-", "")
                    If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("item")) Then
                        Dim link As String = ""
                        link = link & "&Item=" & item
                        link = link & "&ItemName=" & .DataItem("Item")
                        link = link & "&Spec=" & .DataItem("Item Desc")
                        link = link & "&UndelQty=" & .DataItem("Undelivery Qty")
                        link = link & "&moQty=" & .DataItem("Mo Qty")
                        link = link & "&poQty=" & .DataItem("PO Qty")
                        link = link & "&prQty=" & .DataItem("PR Qty")
                        link = link & "&stockQty=" & .DataItem("Stock Qty")
                        link = link & "&DateFrom=" & DateF.Text
                        link = link & "&DateTo=" & DateT.Text
                        hplDetail.NavigateUrl = "../Sales/SaleUndeliveryPeriodPopup.aspx?height=150&width=350" & link
                        hplDetail.Attributes.Add("title", item)
                        hplDetail.Target = "_blank"
                    End If

                Else

                    Dim Currency As String = .DataItem("Currency")
                    Dim Days As Integer = .DataItem("Day")

                    If Currency = "THB" Then
                        'จ่ายเป็นเงิน THB
                        If Days < 7 Then
                            .Cells(0).Attributes.Add("style", "color: red;font-weight:900;")
                        End If
                    Else
                        'จ่ายเป็นเงิน ตปท
                        If Days < 14 Then
                            .Cells(0).Attributes.Add("style", "color: red;font-weight:900;")
                        End If
                    End If

                End If
            End If
        End With
    End Sub


    'การแสดงผลเมื่อมีการเลือกรูปแบบ Report
    Protected Sub ddlReport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReport.SelectedIndexChanged
        If ddlReport.SelectedValue = "0" Then
            ddlCondition.Visible = True
            ddlSummary.Visible = True
            lbRemark.Visible = True
            lbReport.Visible = False
        Else
            ddlCondition.Visible = False
            ddlSummary.Visible = False
            lbRemark.Visible = False
            lbReport.Visible = True
        End If

        GvUndelPeriod.DataSource = Nothing
        GvUndelPeriod.DataBind()
        CountRow1.RowCount = Nothing
    End Sub

End Class
