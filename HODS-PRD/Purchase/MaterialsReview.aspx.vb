Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class MaterialsReview
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable

    Dim dbConn As New DataConnectControl
    Dim datecont As New DateControl
    Dim hashcont As New HashtableControl()
    Dim gvcont As New GridviewControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            UcDdlReportType.manualShow(New ArrayList From {
                                       "S" & VarIni.char8 & "Mat Shortage",
                                       "R" & VarIni.char8 & "Mat Review"
                                       })

            UcDdlMatType.manualShow(New ArrayList From {
                                       "1" & VarIni.char8 & "Materials",
                                       "2" & VarIni.char8 & "Package",
                                       "4" & VarIni.char8 & "SP and Other"
                                       },, True)

            UcDdlMatType.Text = "1"

            Dim SQL As String = "select rtrim(MC001) MC001,rtrim(MC001)+'-'+MC002 MC002 from CMSMC where  MC001 like '23__' order by MC001"
            UcCblWh.show(SQL, VarIni.ERP, "MC002", "MC001")

            btExport.Visible = False
        End If
    End Sub

    Function strToDate(ByVal strDate As String, Optional ByVal dateFormat As String = "yyyyMMdd") As Date
        Return DateTime.ParseExact(strDate, dateFormat, New CultureInfo("en-US"))
    End Function

    Function getFirstWeek(ByVal selDate As Date) As Date
        Return selDate.AddDays(-(selDate.DayOfWeek - DayOfWeek.Sunday))
    End Function

    Function getLastWeek(ByVal selDate As Date) As Date
        Return selDate.AddDays(-(selDate.DayOfWeek - DayOfWeek.Saturday))
    End Function

    Function getWeek(ByVal selDate As Date) As Integer
        Return DatePart("ww", selDate, Microsoft.VisualBasic.FirstDayOfWeek.Sunday)
    End Function

    Sub addDataWeek(ByRef selWeek As Hashtable, ByVal strWeek As String, ByVal endWeek As String, ByVal strYear As String)
        For i As Integer = strWeek To endWeek
            selWeek.Add(strYear & i, strYear & "-" & i)
        Next
    End Sub

    Private Function strFormat(ByVal val As Object) As String
        Dim show As String = ""
        If CDec(val) <> 0 Then
            show = String.Format("{0:n2}", val)
        End If
        Return show
    End Function

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Select Case UcDdlReportType.Text
            Case "R"
                MaterialReview()
            Case "S"
                MaterailShortage()
        End Select
    End Sub

    Sub MaterialReview()
        Dim tempTable As String = "tempMaterialReview" & Session("UserName"),
            tempTable2 As String = "tempBOMReview" & Session("UserName")

        Dim strDate As String = UcDateFrom.TextEmptyToday,
            endDate As String = UcDateTo.TextEmptyToday

        Dim beginDate As Date = getFirstWeek(strToDate(strDate))
        UcDateFrom.Text = beginDate.ToString("yyyyMMdd", New CultureInfo("en-US"))

        Dim lastDate As Date = getLastWeek(strToDate(endDate))
        UcDateTo.Text = lastDate.ToString("yyyyMMdd", New CultureInfo("en-US"))

        'get fld week
        Dim selWeek As Hashtable = New Hashtable,
            strYear As String = beginDate.ToString("yyyy", New CultureInfo("en-US")),
            endYear As String = lastDate.ToString("yyyy", New CultureInfo("en-US")),
            strWeek As Integer = 0,
            endWeek As Integer = 0

        Dim beforeDate As Date = New Date,
            afterDate As Date = New Date
        For i As Integer = strYear To endYear
            If i = strYear Then
                beforeDate = beginDate
            Else
                beforeDate = strToDate(i & "0101")
            End If
            If i = endYear Then
                afterDate = lastDate
            Else
                afterDate = strToDate(i & "1231")
            End If
            addDataWeek(selWeek, getWeek(beforeDate), getWeek(afterDate), i)
        Next

        CreateTempTable.createTempMatReview(tempTable, selWeek)
        CreateTempTable.createtempBOMReview(tempTable2)

        If tbItem.Text <> "" Or tbSpec.Text <> "" Then
            genDataItem(tempTable, tempTable2, beginDate.ToString("yyyyMMdd", New CultureInfo("en-US")), lastDate.ToString("yyyyMMdd", New CultureInfo("en-US")))
        Else
            genDataWeek(tempTable, beginDate.ToString("yyyyMMdd", New CultureInfo("en-US")), lastDate.ToString("yyyyMMdd", New CultureInfo("en-US")))
        End If
        'show Data
        Dim dtShow As Data.DataTable = New DataTable
        dtShow.Columns.Add(New DataColumn("Msg1"))
        dtShow.Columns.Add(New DataColumn("Mat Detail"))
        dtShow.Columns.Add(New DataColumn("Msg4"))
        dtShow.Columns.Add(New DataColumn("Detail"))
        dtShow.Columns.Add(New DataColumn("Msg2"))
        dtShow.Columns.Add(New DataColumn("Qty"))
        dtShow.Columns.Add(New DataColumn("Msg3"))
        dtShow.Columns.Add(New DataColumn("Before"))
        'Dim beforeDate As String = "",
        '    afterDate As String = ""
        Dim fldSumIssue As String = "issueQty",
            fldSumPlan As String = "planQty",
            fldSumPo As String = "poQty-poRcpQty",
            fldSumPoCon As String = "poConQty"

        For i As Integer = strYear To endYear
            If i = strYear Then
                beforeDate = beginDate
            Else
                beforeDate = strToDate(i & "0101")
            End If
            If i = endYear Then
                afterDate = lastDate
            Else
                afterDate = strToDate(i & "1231")
            End If
            Dim firstWeek As Integer = getWeek(beforeDate)
            Dim lastWeek As Integer = getWeek(afterDate)
            For j As Integer = firstWeek To lastWeek
                dtShow.Columns.Add(New DataColumn("W " & j & "/" & i))
                fldSumIssue = fldSumIssue & "+issue" & i & j
                fldSumPlan = fldSumPlan & "+plan" & i & j
                fldSumPo = fldSumPo & "+po" & i & j
                fldSumPoCon = fldSumPoCon & "+poCon" & i & j
            Next
        Next

        dtShow.Columns.Add(New DataColumn("sum"))
        Dim fld As String = "," & fldSumIssue & " as sumIssue," & fldSumPlan & " as sumPlan ," & fldSumPo & " as sumPo ," & fldSumPoCon & " as sumPoCon "
        Dim dr1 As DataRow,
            dr2 As DataRow,
            dr3 As DataRow,
            dr4 As DataRow,
            dr5 As DataRow
        Dim whr As String = " "
        If cbSum.Checked Then
            Dim fld_whr As String = "stockQty+poRcpQty"
            If cbPR.Checked Then
                fld_whr = fld_whr & "+prQty"
            End If
            fld_whr = fld_whr & "-(" & fldSumIssue & ")-(" & fldSumPlan & ")"
            If cbPO.Checked Then
                fld_whr = fld_whr & "+(" & fldSumPo & ")"
            End If
            whr = " where " & fld_whr & "<0"
        End If

        Dim SQL As String = " select *" & fld & " from HOOTHAI_REPORT.dbo." & tempTable & " T " &
                            " left join INVMB on MB001=T.item " & whr &
                            " order by MB001 "
        Dim dt As New DataTable
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        With dt
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    'item
                    dr1 = dtShow.NewRow()
                    dr1("Msg1") = "Item"
                    dr1("Mat Detail") = .Item("item")
                    dr1("Msg4") = "Vendors Code"
                    dr1("Detail") = .Item("MB032")
                    dr1("Msg2") = "Stock"
                    dr1("Qty") = strFormat(.Item("stockQty"))
                    dr1("Msg3") = "Issue"
                    dr1("Before") = strFormat(.Item("issueQty"))

                    'desc
                    dr2 = dtShow.NewRow()
                    dr2("Msg1") = "Desc"
                    dr2("Mat Detail") = .Item("MB002")
                    dr2("Msg4") = "Fixed Lead Time"
                    dr2("Detail") = .Item("MB036")
                    dr2("Msg2") = "PR"
                    dr2("Qty") = strFormat(.Item("prQty"))
                    dr2("Msg3") = "Plan"
                    dr2("Before") = strFormat(.Item("planQty"))

                    'spec
                    dr3 = dtShow.NewRow()
                    dr3("Msg1") = "Spec"
                    dr3("Mat Detail") = .Item("MB003")
                    dr3("Msg4") = ""
                    dr3("Detail") = ""
                    dr3("Msg2") = "Last Price"
                    dr3("Qty") = strFormat(.Item("MB049"))
                    dr3("Msg3") = "Plan PO"
                    dr3("Before") = strFormat(.Item("poQty"))
                    Dim bal As Decimal = .Item("stockQty") + .Item("poRcpQty") - .Item("issueQty") - .Item("planQty")
                    If cbPR.Checked Then
                        bal = bal + .Item("prQty")
                    End If
                    If cbPO.Checked Then
                        bal = bal + .Item("poQty") - .Item("poRcpQty")
                    End If
                    dr4 = dtShow.NewRow()
                    dr4("Msg1") = "Unit"
                    dr4("Mat Detail") = .Item("MB004")
                    dr4("Msg4") = ""
                    dr4("Detail") = ""
                    dr4("Msg2") = ""
                    dr4("Qty") = "" 'strFormat(.Item("stockQty") + .Item("poRcpQty") + .Item("prQty"))
                    dr4("Msg3") = "Confirmed PO" '"Shortage"
                    dr4("Before") = "" 'strFormat(bal)

                    dr5 = dtShow.NewRow()
                    dr5("Msg1") = ""
                    dr5("Mat Detail") = ""
                    dr5("Msg4") = ""
                    dr5("Detail") = ""
                    dr5("Msg2") = "Bal"
                    dr5("Qty") = strFormat(.Item("stockQty") + .Item("poRcpQty") + .Item("prQty"))
                    dr5("Msg3") = "Shortage"
                    dr5("Before") = strFormat(bal)

                    For year As Integer = strYear To endYear
                        If year = strYear Then
                            beforeDate = beginDate
                        Else
                            beforeDate = strToDate(year & "0101")
                        End If
                        If year = endYear Then
                            afterDate = lastDate
                        Else
                            afterDate = strToDate(year & "1231")
                        End If
                        Dim firstWeek As Integer = getWeek(beforeDate)
                        Dim lastWeek As Integer = getWeek(afterDate)
                        For week As Integer = firstWeek To lastWeek
                            Dim colName As String = "W " & week & "/" & year
                            Dim fldName As String = year & week
                            bal = bal - .Item("issue" & fldName) - .Item("plan" & fldName)
                            If cbPO.Checked Then
                                bal = bal + .Item("po" & fldName)
                            End If
                            dr1(colName) = strFormat(.Item("issue" & fldName))
                            dr2(colName) = strFormat(.Item("plan" & fldName))
                            dr3(colName) = strFormat(.Item("po" & fldName))
                            dr4(colName) = strFormat(.Item("poCon" & fldName))
                            dr5(colName) = strFormat(bal)
                        Next
                    Next
                    dr1("Sum") = strFormat(.Item("sumIssue"))
                    dr2("Sum") = strFormat(.Item("sumPlan"))
                    dr3("Sum") = strFormat(.Item("sumPo"))
                    dr4("Sum") = strFormat(.Item("sumPoCon"))
                    dr5("Sum") = ""
                    dtShow.Rows.Add(dr1)
                    dtShow.Rows.Add(dr2)
                    dtShow.Rows.Add(dr3)
                    dtShow.Rows.Add(dr4)
                    dtShow.Rows.Add(dr5)
                    bal = 0
                End With
            Next
            gvShow.DataSource = dtShow
            gvShow.DataBind()
        End With
        'lbCount.Text = gvShow.Rows.Count / 5
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Function DataWeek() As List(Of String)
        Dim beginDate As Date = getFirstWeek(strToDate(UcDateFrom.TextEmptyToday))
        Dim lastDate As Date = getLastWeek(strToDate(UcDateTo.TextEmptyToday))

        Dim selWeek As Hashtable = New Hashtable,
            strYear As String = beginDate.ToString("yyyy", New CultureInfo("en-US")),
            endYear As String = lastDate.ToString("yyyy", New CultureInfo("en-US")),
            strWeek As Integer = 0,
            endWeek As Integer = 0

        Dim beforeDate As Date = New Date,
            afterDate As Date = New Date
        With New ListControl(New List(Of String))
            For i As Integer = strYear To endYear
                If i = strYear Then
                    beforeDate = beginDate
                Else
                    beforeDate = strToDate(i & "0101")
                End If
                If i = endYear Then
                    afterDate = lastDate
                Else
                    afterDate = strToDate(i & "1231")
                End If
                strWeek = getWeek(beforeDate)
                endWeek = getWeek(afterDate)
                For j As Integer = strWeek To endWeek
                    .Add(i & j.ToString("00"))
                Next
            Next
            Return .ListS()
        End With

    End Function

    Sub addRowGrid(ByRef dt As DataTable, dr As DataRow,
                   hashWeek As Hashtable, selectionWeek As List(Of String),
                   fldNumber As List(Of String), backlog As Decimal,
                   Optional strSplit As String = VarIni.C8)
        With New DataRowControl(dr)
            Dim bal As Decimal = backlog
            Dim fldManual As New List(Of String)
            fldManual.Add("BACK_LOG" & VarIni.C8 & If(bal > 0, 0, bal))
            For Each wk As String In selectionWeek
                Dim valAdd As Decimal = 0
                If hashcont.existDataHash(hashWeek, wk) Then
                    valAdd = hashcont.getDataHashDecimal(hashWeek, wk)
                End If
                bal -= valAdd
                fldManual.Add(wk & VarIni.C8 & If(bal > 0, 0, bal))
            Next
            .AddDatarow(dt, fldNumber, fldManual)
        End With
    End Sub

    Sub MaterailShortage()
        Dim beginDate As Date = getFirstWeek(strToDate(UcDateFrom.TextEmptyToday))
        UcDateFrom.Text = beginDate.ToString("yyyyMMdd", New CultureInfo("en-US"))

        Dim lastDate As Date = getLastWeek(strToDate(UcDateTo.TextEmptyToday))
        UcDateTo.Text = lastDate.ToString("yyyyMMdd", New CultureInfo("en-US"))

        Dim selectionWeek As List(Of String) = DataWeek()

        Dim weekFrom As String = selectionWeek(0)
        Dim weekEnd As String = selectionWeek(selectionWeek.Count - 1)

        Dim fldList As List(Of String) = GetListShortage(False, New List(Of String))

        Dim sql As String = sqlShortage(fldList, weekFrom, weekEnd)
        Dim dtcont As New DataTableControl
        Dim colList1 As List(Of String) = GetListShortage(True, selectionWeek)
        Dim collist As New ArrayList
        For Each txt As String In colList1
            collist.Add(txt)
        Next
        Dim dtShow As DataTable = dtcont.setColDatatable(collist, VarIni.C8)
        Dim dt As DataTable = dbConn.Query(sql, VarIni.ERP, dbConn.WhoCalledMe)

        Dim item As String = String.Empty
        Dim itemLast As String = String.Empty
        Dim hashWeek As New Hashtable
        Dim bal As Decimal = Decimal.Zero

        Dim fldNumber As List(Of String) = GetListShortage(True, selectionWeek, True)
        Dim lastDr As DataRow = Nothing
        'Dim beginBal As Decimal = 0
        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                item = .Text("itemChild")
                If item <> itemLast Then
                    If Not String.IsNullOrEmpty(itemLast) Then
                        addRowGrid(dtShow, lastDr, hashWeek, selectionWeek, fldNumber, bal)
                    End If
                    'end of last row
                    'before new row
                    bal = .Number("stockQty") + .Number("qcQty") - .Number("issueQty_before")
                    'beginBal = bal
                    hashWeek = New Hashtable
                End If
                'bal -= .Number("issueQty")
                hashWeek.Add(.Text("YYYYWK"), .Number("issueQty"))
                itemLast = item
                lastDr = dr
            End With
        Next
        addRowGrid(dtShow, lastDr, hashWeek, selectionWeek, fldNumber, bal)

        With New GridviewControl(gvShow)
            .ShowGridView(collist, dtShow)
            Dim rowConunt As Decimal = .rowGridview
            CountRow1.RowCount = rowConunt
            btExport.Visible = If(rowConunt > 0, True, False)
        End With
        System.Threading.Thread.Sleep(1000)
    End Sub

    Public Function FirstDateOfWeek(ByVal Year As Integer,
                                    ByVal Week As Integer,
                                    Optional FirstDayOfWeek As DayOfWeek = DayOfWeek.Sunday) As Date
        Dim dt As Date = New Date(Year, 1, 1)
        If dt.DayOfWeek > 4 Then
            dt = dt.AddDays(7 - dt.DayOfWeek)
        Else
            dt = dt.AddDays(-dt.DayOfWeek)
        End If
        dt = dt.AddDays(FirstDayOfWeek)
        Return dt.AddDays(7 * (Week - 1) - 7)
    End Function

    Function getDateFormatCol(wkVal As String) As String
        Dim outcont As New OutputControl

        Dim txtyyy As String = wkVal.Substring(0, 4)
        Dim txtwk As String = wkVal.Substring(4, 2)
        Dim yyyy As Integer = outcont.checkNumberic(txtyyy)
        Dim wk As Integer = outcont.checkNumberic(txtwk)
        Dim fDate As Date = FirstDateOfWeek(yyyy, wk)
        Return fDate.ToString("dd/MMM") & " WK " & txtwk & "/" & txtyyy
    End Function

    Function GetListShortage(gvCol As Boolean,
                             listweek As List(Of String),
                             Optional getfldNumber As Boolean = False) As List(Of String)
        With New ListControl(New List(Of String))
            .TAL("itemChild", "Item")            'item
            .TAL("MB002", "Desc")            'desc
            .TAL("MB003", "Spec")            'spec
            .TAL("plan_start_date", "MO Start")            'mo start
            If gvCol Then 'gridview
                .TAL("BACK_LOG", "Back Log", "2")            'back log
                For Each wk As String In listweek
                    .TAL(wk, getDateFormatCol(wk), "2")            'each week
                Next
            Else 'sql
                .TAL(dbConn.ISNULL("YYYYWK", "''", VarIni.C8, True), "")            'each week
                '.TAL(dbConn.ISNULL("planQty_before", "0", VarIni.C8), "")            'back log
                .TAL(dbConn.ISNULL("issueQty_before", "0", VarIni.C8, True), "")            'each week

                '.TAL(dbConn.ISNULL("planQty", "0", VarIni.C8), "")            'back log
                .TAL(dbConn.ISNULL("issueQty", "0", VarIni.C8, True), "")            'each week
            End If

            .TAL(dbConn.ISNULL("issueQty_sum", "0", VarIni.C8, True), "Issue Qty All", "2")            'po qty
            .TAL(dbConn.ISNULL("poQty", "0", VarIni.C8, True), "PO Qty", "2")            'po qty
            .TAL(dbConn.ISNULL("prQty", "0", VarIni.C8, True), "PR Qty", "2")            'pr qty
            .TAL(dbConn.ISNULL("qcQty", "0", VarIni.C8, True), "POR Inspec Qty", "2")            'por inspection
            .TAL(dbConn.ISNULL("stockQty", "0", VarIni.C8, True), "Stock Qty", "2")            'stock
            .TAL(dbConn.ISNULL("stockMrbQty", "0", VarIni.C8, True), "Stock MRB Qty", "2")            'stock mrb
            .TAL(dbConn.ISNULL("po", "''", VarIni.C8, True), "PO No.")            'po no
            .TAL(dbConn.ISNULL("plan_ship_date", "''", VarIni.C8, True), "po plan delivery date")            'po ship date
            '.TAL("", "")            'source doc no
            '.TAL("", "")            'customer
            .TAL("MB036", "lead time", "0")            'lead time
            .TAL("MB032", "main vender")            'main vender
            .TAL("MB067", "main buyer")            'main buyer
            .TAL("MB148", "unit")            'unit

            Return If(getfldNumber, .ColumnNumber(), .ChangeFormat(gvCol))
        End With
    End Function

    Function sqlShortage(fldSelect As List(Of String), weekFrom As String, weekEnd As String) As String
        Dim SQL1 As String
        With New SQLTEXT("HOOTHAI_REPORT..BOM")
            .SL(New List(Of String) From { .Field(VarIni.DISTINCT & "itemChild")})
            .LJ("INVMB", New List(Of String) From { .LE("MB001", "itemFG")})
            .WE("property", "P")
            .WL("itemFG", tbItem)
            .WL("MB003", tbSpec)
            SQL1 = .TEXT(True, "BOM")
        End With

        Dim SQL2 As String
        With New SQLTEXT("PURTB")
            .SL(New List(Of String) From {
                .Field("TB004", "item_pr"),
                .Field(.SUM("TR006"), "prQty")
                })
            .LJ("PURTR", New List(Of String) From {
                .LE("TR001", "TB001"),
                .LE("TR002", "TB002"),
                .LE("TR003", "TB003")
                })
            .LJ("PURTA", New List(Of String) From {
                .LE("TA001", "TB001"),
                .LE("TA002", "TB002")
                })
            .WE("TB039", "N")
            .WE("TA007", "Y")
            .WE("TR019", "''",, False)
            .WE("TR006", "0", ">", False)
            .GB("TB004")
            SQL2 = .TEXT(True, "PR")
        End With

        Dim SQL3 As String
        With New SQLTEXT("PURTH")
            .SL(New List(Of String) From {
                .Field("TH004", "item_qc"),
                .Field(.SUM(.ISNULL("TH007", "0")), "qcQty")
                })
            .LJ("PURTG", New List(Of String) From {
                .LE("TG001", "TH001"),
                .LE("TG002", "TH002")
                })
            .WE("TG013", "N")
            .GB("TH004")
            SQL3 = .TEXT(True, "PO_INSPEC")
        End With

        Dim SQL4 As String
        With New SQLTEXT("PURTD")
            .SL(New List(Of String) From {
                .Field("TD004", "item_po"),
                .Field(.SUM(.ISNULL("TD008", "0") & "-" & .ISNULL("TD015", "0")), "poQty")
                })
            .LJ("PURTC", New List(Of String) From {
                .LE("TC001", "TD001"),
                .LE("TC002", "TD002")
                })
            .WE(.ISNULL("TD008", "0") & "-" & .ISNULL("TD015", "0"), "0", ">", False)
            .WE("TCD01", "N")
            .WE("TC014", "Y")
            .WE("TD016", "N")
            .GB("TD004")
            SQL4 = .TEXT(True, "PO")
        End With

        Dim SQL51 As String
        With New SQLTEXT("MOCTB")
            .SL(New List(Of String) From {
                .Field("TB001+'-'+TB002", "mo"),
                .Field("TB003", "item_mo"),
                .Field("TA009", "plan_start_date"),
                .Field("row_number() over (partition by TB003 order by  MOCTA.TA009 )", "row_number")
                })
            .LJ("MOCTA", New List(Of String) From {
                .LE("TA001", "TB001"),
                .LE("TA002", "TB002")
                })
            .WE("TA013", "Y")
            .WI("TA011", "Y,y", True, True)
            .WE("TB004-TB005", "0", ">", False)
            SQL51 = .TEXT(True, "MO")
        End With
        Dim SQL5 As String
        With New SQLTEXT(SQL51)
            .SL(New List(Of String) From {
                .Field("mo"),
                .Field("item_mo"),
                .Field("plan_start_date")
                })
            .WE("row_number", "1",, False)
            SQL5 = .TEXT(True, "MO1")
        End With

        Dim SQL61 As String
        With New SQLTEXT("PURTD")
            .SL(New List(Of String) From {
                .Field("TD001+'-'+TD002", "po"),
                .Field("TD004", "item_po1"),
                .Field("TD012", "plan_ship_date"),
                .Field("row_number() over (partition by TD004 order by TD012 )", "row_no")
                })
            .LJ("PURTC", New List(Of String) From {
                .LE("TC001", "TD001"),
                .LE("TC002", "TD002")
                })
            .WE("TC014", "Y")
            .WE("TCD01", "N")
            .WE("TD008-TD015", "0", ">", False)
            SQL61 = .TEXT(True, "PO")
        End With
        Dim SQL6 As String
        With New SQLTEXT(SQL61)
            .SL(New List(Of String) From {
                .Field("po"),
                .Field("item_po1"),
                .Field("plan_ship_date")
                })
            .WE("row_no", "1",, False)
            SQL6 = .TEXT(True, "PO1")
        End With

        Dim SQL7 As String
        With New SQLTEXT("HOOTHAI_REPORT..V_WEEKLY_ITEM")
            .SL(New List(Of String) From {
                .Field("item_week", "item_week_before"),
                .Field("min(YYYYWK)", "YYYYWK_before"),
                .Field(.SUM("planQty"), "planQty_before"),
                .Field("min(planDate)", "planDate_before"),
                .Field(.SUM("issueQty"), "issueQty_before"),
                .Field("min(issueDate)", "issueDate_before")
                })
            .WE("YYYYWK", weekFrom, "<")
            .GB("item_week")
            SQL7 = .TEXT(True, "WEEK_BEFORE")
        End With

        Dim SQL8 As String
        With New SQLTEXT("HOOTHAI_REPORT..V_WEEKLY_ITEM")
            .SL(New List(Of String) From {
                .Field("item_week"),
                .Field("YYYYWK"),
                .Field(.SUM("planQty"), "planQty"),
                .Field("min(planDate)", "planDate"),
                .Field(.SUM("issueQty"), "issueQty"),
                .Field("min(issueDate)", "issueDate")
                })
            .WB("YYYYWK", weekFrom, weekEnd)
            .GB(New List(Of String) From {"item_week", "YYYYWK"})
            SQL8 = .TEXT(True, "WEEK_BETWEEN")
        End With

        Dim SQL10 As String
        With New SQLTEXT("HOOTHAI_REPORT..V_WEEKLY_ITEM")
            .SL(New List(Of String) From {
                .Field("item_week", "item_week_sum"),
                .Field("min(YYYYWK)", "YYYYWK_sum"),
                .Field(.SUM("planQty"), "planQty_sum"),
                .Field("min(planDate)", "planDate_sum"),
                .Field(.SUM("issueQty"), "issueQty_sum"),
                .Field("min(issueDate)", "issueDate_sum")
                })
            .WE("YYYYWK", weekEnd, "<=")
            .GB("item_week")
            SQL10 = .TEXT(True, "WEEK_ALL")
        End With

        Dim SQL9 As String
        With New SQLTEXT("INVMC")
            .SL(New List(Of String) From {
                .Field("MC001", "item_stock"),
                .Field(.SUM(.ISNULL("case MC002 when '2800' then  INVMC.MC007 else 0 end", "0")), "stockMrbQty"),
                .Field(.SUM(.ISNULL("case MC002 when '2800' then  0 else INVMC.MC007 end", "0")), "stockQty")
            })
            .WE("MC007", "0", ">", False)
            .WI("MC002", UcCblWh.Text(True) & VarIni.C & "2800",, True)
            .GB("MC001")
            SQL9 = .TEXT(True, "STOCK")
        End With

        Dim SQL As String
        With New SQLTEXT(SQL1)
            Dim fldS As New List(Of String)
            For Each txt As String In fldSelect
                fldS.Add(txt.Replace(VarIni.C8, VarIni.SP))
            Next

            .SL(fldS)
            Dim whr1 As New List(Of String) From {
               "item_pr",
                "item_qc",
                "item_po",
                "item_mo",
                "item_po1",
                "item_week_before",
                "item_week",
                "item_stock"
            }
            Dim whr2 As New List(Of String)
            For Each txt As String In whr1
                whr2.Add(.LE(txt, " is not null ", ""))
            Next

            .LJ(SQL2, New List(Of String) From { .LE("item_pr", "itemChild")})
            .LJ(SQL3, New List(Of String) From { .LE("item_qc", "itemChild")})
            .LJ(SQL4, New List(Of String) From { .LE("item_po", "itemChild")})
            .LJ(SQL5, New List(Of String) From { .LE("item_mo", "itemChild")})
            .LJ(SQL6, New List(Of String) From { .LE("item_po1", "itemChild")})
            .LJ(SQL7, New List(Of String) From { .LE("item_week_before", "itemChild")})
            .LJ(SQL8, New List(Of String) From { .LE("item_week", "itemChild")})
            .LJ(SQL9, New List(Of String) From { .LE("item_stock", "itemChild")})
            .LJ(SQL10, New List(Of String) From { .LE("item_week_sum", "itemChild")})
            .LJ("INVMB", New List(Of String) From { .LE("MB001", "itemChild")})

            'Dim whr As String = .addBracket(String.Join(" OR ", whr2))
            .WE("(", ")", String.Join(" OR ", whr2), False)
            .WE("itemChild", tbMatItem)
            .WE("MB002", tbMatDesc)
            .WE("MB003", tbMatSpec)

            If UcDdlMatType.Text <> "0" Then
                .WL("itemChild", "_1" & UcDdlMatType.Text, False)
            Else
                Dim typeList As List(Of String) = UcDdlMatType.getAll()
                whr1 = New List(Of String)
                Dim whrTypeAll As String = ""
                With New ListControl(whr1)
                    For Each typeTxt As String In typeList
                        .Add(dbConn.WHERE_LIKE("itemChild", "_1" & typeTxt, False, showAnd:=False))
                    Next
                    whrTypeAll = .Text(" OR ")
                End With
                .WE("(", ")", whrTypeAll, False)
            End If
            If cbSum.Checked Then   'summary <0
                Dim fldsum As String = "isnull(stockQty,0)+isnull(qcQty,0)-isnull(issueQty_sum,0)"
                .WE(fldsum, "0", "<", False)
            End If

            .OB(New List(Of String) From {"itemChild", "YYYYWK"})
            SQL = .TEXT
        End With
        Return SQL
    End Function

    Protected Sub genDataItem(ByVal tempTable As String, ByVal tempTable2 As String, ByVal strDate As String, ByVal endDate As String)
        'gent from BOM
        Dim SQL As String = "",
            WHR As String = "",
            USQL As String = ""
        If tbItem.Text.Trim <> "" Then
            WHR = WHR & " and MB001 like '%" & tbItem.Text.Trim & "%'"
        End If
        If tbSpec.Text.Trim <> "" Then
            WHR = WHR & " and MB003 like '%" & tbSpec.Text.Trim & "%'"
        End If

        SQL = " select MB001 from INVMB where MB109 = 'Y' and MB025 = 'M' " & WHR & " order by MB001 "
        Dim Program As New DataTable
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            CodeBOM(tempTable, tempTable2, Program.Rows(i).Item("MB001"))
        Next

        'PR
        SQL = " select T.item,sum(PURTR.TR006) as pr from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join PURTB on PURTB.TB004=T.item " &
              " left join PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " &
              " left join PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " &
              " where PURTB.TB039='N' and PURTA.TA007 = 'Y' and PURTR.TR019='' " &
              " group by T.item order by T.item "
        Dim dt As New DataTable,
           item As String = "",
           qty As Decimal = 0
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set prQty='" & .Item("pr") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'Stock
        SQL = " select T.item,SUM(isnull(INVMC.MC007,0)) as stock from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join INVMC on INVMC.MC001=T.item " &
              " where INVMC.MC007 >0 and ((substring(INVMC.MC001,3,1)='1' and INVMC.MC002 in ('2201','2202','2203','2204')) or " &
              " (substring(INVMC.MC001,3,1)='4' and INVMC.MC002 in ('2205','2206','2900','2901'))) group by T.item "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set stockQty='" & .Item("stock") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'Purchase receipt inspection
        SQL = " select T.item,SUM(isnull(PURTH.TH007,0)) as po_insp from HOOTHAI_REPORT.dbo." & tempTable & " T  " &
              " left join PURTH on PURTH.TH004=T.item " &
              " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " &
              " where PURTG.TG013 = 'N' group by T.item having (SUM(isnull(PURTH.TH007, 0)) > 0) " &
              " order by T.item "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("po_insp")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poRcpQty='" & qty & "'  where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'Plan Receive
        'po All and TD012 < '" & strDate & "'
        SQL = " select T.item,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join PURTD on PURTD.TD004=T.item " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' and TD012 < '" & strDate & "' " &
              " group by T.item  order by T.item "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set poQty='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
        'po between date select
        SQL = " select T.item,DATEPART(week,TD012) as weekNo,DATEPART(yyyy,TD012) as selYear,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join PURTD on PURTD.TD004=T.item " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' " & dbConn.Where("TD012", strDate, endDate) & " " &
              " group by T.item,substring(PURTD.TD012,1,6),DATEPART(week,TD012),DATEPART(yyyy,TD012) "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set po" & .Item("selYear") & .Item("weekNo") & "='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'Confirm Receive
        'po All and TD012 < '" & strDate & "'
        SQL = " select T.item,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join PURTD on PURTD.TD004=T.item " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' and TD014<>'' and replace(TD014,'-','') < '" & strDate & "' " &
              " group by T.item  order by T.item "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set poConQty='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
        'po between date select
        SQL = " select T.item,DATEPART(week,replace(TD014,'-','')) as weekNo,DATEPART(yyyy,replace(TD014,'-','')) as selYear,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join PURTD on PURTD.TD004=T.item " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' and TD014<>'' " & dbConn.Where("replace(TD014,'-','')", strDate, endDate) & " " &
              " group by T.item,substring(replace(TD014,'-',''),1,6),DATEPART(week,replace(TD014,'-','')),DATEPART(yyyy,replace(TD014,'-','')) "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set poCon" & .Item("selYear") & .Item("weekNo") & "='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'Plan Usage
        '<statr date
        SQL = " select itemMAT as item,sum((isnull(TA006,0)*qty)+CEILING(isnull(TA006,0)*qty*scrapRatio)) as planQty from HOOTHAI_REPORT.dbo." & tempTable2 & " T " &
              " left join LRPTA on  TA002=itemParent " &
              " left join LRPLA on TA001=LA001  and TA050=LA012 " &
              " where TA007<'" & strDate & "' and  TA006>0  and TA051='N' and LA005='1'  and LA013 = '1' " &
              " group by itemMAT "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set planQty='" & .Item("planQty") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
        'between start date and end date
        SQL = " select itemMAT as item,DATEPART(week,TA007) as weekNo,DATEPART(yyyy,TA007) as selYear,sum((isnull(TA006,0)*qty)+CEILING(isnull(TA006,0)*qty*scrapRatio)) as planQty from HOOTHAI_REPORT.dbo." & tempTable2 & " T " &
              " left join LRPTA on  TA002=itemParent " &
              " left join LRPLA on TA001=LA001  and TA050=LA012 " &
              " where TA006>0   and TA051='N' and LA005='1' and LA013 = '1' and TA007 between  '" & strDate & "' and '" & endDate & "'   " &
              " group by itemMAT,DATEPART(week,TA007),DATEPART(yyyy,TA007) "

        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set plan" & .Item("selYear") & .Item("weekNo") & "='" & .Item("planQty") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'plan Issue
        '< from date
        SQL = " select T.item,SUM(MOCTB.TB004-MOCTB.TB005) as issue from HOOTHAI_REPORT.dbo." & tempTable & " T " &
               " left join MOCTB on MOCTB.TB003=T.item " &
               " left join MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
               " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " &
               " and TB015<'" & strDate & "' group by T.item "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set issueQty='" & .Item("issue") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
        'between from date and to date
        SQL = " select T.item,DATEPART(week,TB015) as weekNo,DATEPART(yyyy,TB015) as selYear,SUM(MOCTB.TB004-MOCTB.TB005) as issue from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join MOCTB on MOCTB.TB003=T.item " &
              " left join MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
              " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " &
              " and TB015 between '" & strDate & "' and '" & endDate & "' group by T.item,DATEPART(week,TB015),DATEPART(yyyy,TB015) "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim fldName As String = "issue" & .Item("selYear") & .Item("weekNo")
                USQL = " update " & tempTable & " set issue" & .Item("selYear") & .Item("weekNo") & " ='" & .Item("issue") & "' where item='" & .Item("item") & "' "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
    End Sub

    Protected Sub genDataWeek(ByVal tempTable As String, ByVal strDate As String, ByVal endDate As String)

        Dim SQL As String = "",
            USQL As String = "",
            WHR As String = ""
        'PR
        If tbMatItem.Text.Trim <> "" Then
            WHR &= " and PURTB.TB004 like '%" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR &= " and PURTB.TB005 like '%" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR &= " and PURTB.TB006 like '%" & tbMatSpec.Text.Trim & "%' "
        End If
        If UcDdlMatType.Text <> "0" Then
            WHR &= " and len(PURTB.TB004)>10 and substring(PURTB.TB004,3,1) like '%" & UcDdlMatType.Text & "%' "
        Else
            WHR &= " and len(PURTB.TB004)>10 and substring(PURTB.TB004,3,1) in ('1','2','4') "
        End If

        SQL = " select PURTB.TB004,sum(PURTR.TR006) as pr from PURTB  " &
              " left join PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " &
              " left join PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " &
              " where PURTB.TB039='N' and PURTA.TA007 = 'Y' and PURTR.TR019='' " & WHR &
              " group by PURTB.TB004 order by PURTB.TB004 "
        Dim dt As New DataTable,
           item As String = "",
           qty As Decimal = 0,
           fld As String = "",
           lstItem As String = "",
           fldInsHash As Hashtable = New Hashtable,
           whrHash As Hashtable = New Hashtable,
           fldUpdHash As Hashtable = New Hashtable
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item("TB004").ToString.Trim
                qty = .Item("pr").ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("prQty", qty) ' fg item
                fldUpdHash.Add("prQty", "'" & qty & "'") ' fg item
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'Plan Receive
        'po All and TD012 < '" & strDate & "'
        WHR = ""
        If tbMatItem.Text.Trim <> "" Then
            WHR = WHR & " and PURTD.TD004 like '%" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR = WHR & " and PURTD.TD005 like '%" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR = WHR & " and PURTD.TD006 like '%" & tbMatSpec.Text.Trim & "%' "
        End If
        If UcDdlMatType.Text <> "0" Then
            WHR = WHR & " and len(PURTD.TD004)>10 and substring(PURTD.TD004,3,1) like '%" & UcDdlMatType.Text & "%' "
        Else
            WHR = WHR & " and len(PURTD.TD004)>10  and substring(PURTD.TD004,3,1) in('1','2','4') "
        End If

        SQL = " select PURTD.TD004,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from PURTD " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' and TD012 < '" & strDate & "' " & WHR &
              " group by PURTD.TD004 "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item("TD004").ToString.Trim
                qty = .Item("po").ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("poQty", qty) ' fg item
                fldUpdHash.Add("poQty", "'" & qty & "'") ' fg item
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
        'po between date select
        lstItem = ""
        SQL = " select PURTD.TD004,DATEPART(week,TD012) as weekNo,DATEPART(yyyy,TD012) as selYear,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from PURTD " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' " & dbConn.Where("TD012", strDate, endDate) & WHR &
              " group by PURTD.TD004,DATEPART(week,TD012),DATEPART(yyyy,TD012) " &
              " order by PURTD.TD004 "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item("TD004").ToString.Trim
                qty = .Item("po").ToString.Trim
                fld = "po" & .Item("selYear") & .Item("weekNo")
                'whr of condition
                If lstItem <> item Then
                    If lstItem <> "" Then
                        'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                    End If
                    fldInsHash = New Hashtable
                    whrHash = New Hashtable
                    fldUpdHash = New Hashtable
                    whrHash.Add("item", item)
                End If
                fldInsHash.Add(fld, qty) ' fg item
                fldUpdHash.Add(fld, "'" & qty & "'") ' fg item
                lstItem = item
            End With
        Next
        If lstItem <> "" Then
            'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
        End If
        lstItem = ""
        ' replace(TD014,'-','')
        'Confirm PO Date
        SQL = " select PURTD.TD004,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from PURTD " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' and TD014<>'' and replace(TD014,'-','') < '" & strDate & "' " & WHR &
              " group by PURTD.TD004 "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item("TD004").ToString.Trim
                qty = .Item("po").ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("poConQty", qty) ' fg item
                fldUpdHash.Add("poConQty", "'" & qty & "'") ' fg item
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
        'po between date select
        lstItem = ""
        SQL = " select PURTD.TD004,DATEPART(week,replace(TD014,'-','')) as weekNo,DATEPART(yyyy,replace(TD014,'-','')) as selYear,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from PURTD " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' and TD014<>'' " & dbConn.Where("replace(TD014,'-','')", strDate, endDate) & WHR &
              " group by PURTD.TD004,DATEPART(week,replace(TD014,'-','')),DATEPART(yyyy,replace(TD014,'-','')) " &
              " order by PURTD.TD004 "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item("TD004").ToString.Trim
                qty = .Item("po").ToString.Trim
                fld = "poCon" & .Item("selYear") & .Item("weekNo")
                'whr of condition
                If lstItem <> item Then
                    If lstItem <> "" Then
                        'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                    End If
                    fldInsHash = New Hashtable
                    whrHash = New Hashtable
                    fldUpdHash = New Hashtable
                    whrHash.Add("item", item)
                End If
                fldInsHash.Add(fld, qty) ' fg item
                fldUpdHash.Add(fld, "'" & qty & "'") ' fg item
                lstItem = item
            End With
        Next
        If lstItem <> "" Then
            'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
        End If
        lstItem = ""

        'Plan Usage
        WHR = ""
        If tbMatItem.Text.Trim <> "" Then
            WHR = WHR & " and MD003 like '%" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR = WHR & " and MB002 like '%" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR = WHR & " and MB003 like '%" & tbMatSpec.Text.Trim & "%' "
        End If
        If UcDdlMatType.Text <> "0" Then
            WHR = WHR & " and substring(MD003,3,1) like '%" & UcDdlMatType.Text & "%' "
        Else
            WHR = WHR & " and SUBSTRING(MD003,3,1) in('1','2','4') "
        End If

        SQL = " select MD003,sum((isnull(TA006,0)* isnull(MD006,0))+CEILING(isnull(TA006,0)* isnull(MD006,0)*MD008) ) as planQty from LRPTA " &
              " right join BOMMD on MD001=TA002  " &
              " left join INVMB on MB001=MD003 " &
              " left join LRPLA on TA001=LA001  and TA050=LA012 " &
              " where MB025='P' and  TA006>0 and TA051 = 'N'   and LA005='1' and LA013 = '1'  and TA007<'" & strDate & "' " & WHR &
              " group by MD003 having sum( isnull(TA006,0)* isnull(MD006,0) )>0 "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item("MD003").ToString.Trim
                qty = .Item("planQty").ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("planQty", qty) ' fg item
                fldUpdHash.Add("planQty", "'" & qty & "'") ' fg item
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
        'between start date and end date
        SQL = " select MD003,DATEPART(week,TA007) as weekNo,DATEPART(yyyy,TA007) as selYear,sum((isnull(TA006,0)* isnull(MD006,0))+CEILING(isnull(TA006,0)* isnull(MD006,0)*MD008) ) as planQty from LRPTA " &
              " right join BOMMD on MD001=TA002  " &
              " left join INVMB on MB001=MD003 " &
              " left join LRPLA on TA001=LA001  and TA050=LA012 " &
              " where MB025='P' and TA051 = 'N' and LA005='1' and LA013 = '1'  and  TA006>0 " & dbConn.Where("TA007", strDate, endDate) & WHR &
              " group by MD003,DATEPART(week,TA007),DATEPART(yyyy,TA007) having sum( isnull(TA006,0)* isnull(MD006,0) )>0 "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item("MD003").ToString.Trim
                qty = .Item("planQty").ToString.Trim
                fld = "plan" & .Item("selYear") & .Item("weekNo")
                'whr of condition
                If lstItem <> item Then
                    If lstItem <> "" Then
                        'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                    End If
                    fldInsHash = New Hashtable
                    whrHash = New Hashtable
                    fldUpdHash = New Hashtable
                    whrHash.Add("item", item)
                End If
                fldInsHash.Add(fld, qty) ' fg item
                fldUpdHash.Add(fld, "'" & qty & "'") ' fg item
                lstItem = item
            End With
        Next
        If lstItem <> "" Then
            'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
        End If
        lstItem = ""
        'plan Issue
        '< from date
        WHR = ""
        If tbMatItem.Text.Trim <> "" Then
            WHR = WHR & " and TB003 like '%" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR = WHR & " and TB012 like '%" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR = WHR & " and TB013 like '%" & tbMatSpec.Text.Trim & "%' "
        End If
        If UcDdlMatType.Text <> "0" Then
            WHR = WHR & " and substring(TB003,3,1) like '%" & UcDdlMatType.Text & "%' "
        Else
            WHR = WHR & " and SUBSTRING(TB003,3,1) in('1','2','4') "
        End If

        SQL = " select TB003,SUM(TB004-TB005) as issue from MOCTB " &
               " left join MOCTA on TA001=TB001 and TA002=TB002 " &
               " left join INVMB on MB001=TB003 " &
               " where MB025='P' and TB004-TB005>0 and TA011 not in('y','Y') and TA013='Y' " &
               " and TB015<'" & strDate & "' " & WHR &
               " group by TB003 "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item("TB003").ToString.Trim
                qty = .Item("issue").ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("issueQty", qty) ' fg item
                fldUpdHash.Add("issueQty", "'" & qty & "'") ' fg item
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
        'between from date and to date
        SQL = " select TB003,DATEPART(week,TB015) as weekNo,DATEPART(yyyy,TB015) as selYear,SUM(TB004-TB005) as issue from MOCTB " &
              " left join MOCTA on TA001=TB001 and TA002=TB002 " &
              " left join INVMB on MB001=TB003 " &
              " where MB025='P' and TB004-TB005>0 and TA011 not in('y','Y') " &
              " and TA013='Y'  " & dbConn.Where("TB015", strDate, endDate) & WHR &
              " group by TB003,DATEPART(week,TB015),DATEPART(yyyy,TB015) " &
              " order by TB003 "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)

                item = .Item("TB003").ToString.Trim
                qty = .Item("issue").ToString.Trim
                fld = "issue" & .Item("selYear") & .Item("weekNo")
                'whr of condition
                If lstItem <> item Then
                    If lstItem <> "" Then
                        'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                    End If
                    fldInsHash = New Hashtable
                    whrHash = New Hashtable
                    fldUpdHash = New Hashtable
                    whrHash.Add("item", item)
                End If
                fldInsHash.Add(fld, qty) 'fg item
                fldUpdHash.Add(fld, "'" & qty & "'") 'fg item
                lstItem = item
            End With
        Next
        If lstItem <> "" Then
            'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
        End If

        'Stock
        SQL = " select T.item,SUM(isnull(INVMC.MC007,0)) as stock from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join INVMC on INVMC.MC001=T.item " &
              " where INVMC.MC007 >0 and ((substring(INVMC.MC001,3,1)='1' and INVMC.MC002 in ('2201','2202','2203','2204')) or " &
              " (substring(INVMC.MC001,3,1)='4' and INVMC.MC002 in ('2205','2206','2900','2901'))) group by T.item "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item("item").ToString.Trim
                qty = .Item("stock").ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("stockQty", qty) ' fg item
                fldUpdHash.Add("stockQty", "'" & qty & "'") ' fg item
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

    End Sub

    Function chkCode(ByVal code As String) As Boolean
        Dim c1 As String = code.Substring(1, 1)
        Dim res As Boolean = False
        If UcDdlMatType.Text = "0" Then
            If c1 = "1" Or c1 = "4" Then
                res = True
            End If
        Else
            Dim tm As String = UcDdlMatType.Text
            If c1 = tm Then
                res = True
            End If
        End If
        Return res
    End Function

    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        Dim ControlForm As New ControlDataForm
        ControlForm.ExportGridViewToExcel("MaterialsReview" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")

            With e.Row.Cells(0)
                Dim Item As String = .Text.Trim
                .Font.Bold = True
                .Attributes.Add("onclick", "NewWindow('../Popup/ItemPopup.aspx?Item=" & Item & "','_blank',800,500,'yes')")
            End With
        End If
    End Sub

    Protected Sub CodeBOM(ByVal tempTable As String, ByVal tempTable2 As String, ByVal code As String, Optional ByVal codeParent As String = "")
        If codeParent = "" Then
            codeParent = code
        End If
        Dim SQL As String = "",
            WHR As String = ""
        If tbMatItem.Text.Trim <> "" Then
            WHR &= " and MB001 like '%" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR &= " and MB002 like '%" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR &= " and MB003 like '%" & tbMatSpec.Text.Trim & "%' "
        End If
        If WHR <> "" Then
            WHR = " and (MB025='M' or (MB025='P' " & WHR & ")) "
        End If
        SQL = " select MD003,MD006,MB025,MD008 from BOMMD " &
              " left join INVMB on MB001=MD003 " &
              " where MD001='" & code & "' " & WHR &
              " order by MD003 "
        Dim Program As New DataTable
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim item As String = .Item("MD003"),
                    qty As Decimal = .Item("MD006"),
                    ScrapRatio As Decimal = .Item("MD008")
                If .Item("MB025") = "M" Then
                    CodeBOM(tempTable, tempTable2, item, codeParent)
                ElseIf chkCode(item) Then
                    Dim fldInsHash As Hashtable = New Hashtable,
                        whrHash As Hashtable = New Hashtable,
                        fldUpdHash As Hashtable = New Hashtable
                    'whr of condition
                    Dim matCode As String = .Item("MD003").ToString.Trim
                    whrHash.Add("item", matCode)
                    fldInsHash.Add("poQty", "0") ' fg item
                    fldUpdHash.Add("poQty", "'0'") ' fg item
                    'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                    dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)

                    Dim USQL As String = " if not exists(select * from " & tempTable2 & " where itemFG='" & codeParent & "' and itemParent='" & code & "' and itemMAT='" & matCode & "' ) " &
                                         " insert into " & tempTable2 & "(itemFG,itemParent,itemMAT,qty,scrapRatio)values ('" & codeParent & "','" & code & "','" & matCode & "','" & qty & "','" & ScrapRatio & "')"
                    'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                    dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                End If
            End With
        Next
    End Sub

    Protected Sub BtBOM_Click(sender As Object, e As EventArgs) Handles BtBOM.Click
        Dim SQL As String
        ''special select by me use once time only
        'SQL = "select MC001 from BOMMC where MC001 like '502000100000%'"
        'Dim dtt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'For Each dr As DataRow In dtt.Rows
        '    With New DataRowControl(dr)
        '        BOM(.Text("MC001"))
        '    End With
        'Next
        'show_message.ShowMessage(Page, "update completed", UpdatePanel1)
        'Exit Sub
        'select item to get BOM
        With New SQLTEXT("INVMB")
            .SL(New List(Of String) From { .Field("MB001")})
            .WL("MB001", tbItem)
            .WL("MB003", tbSpec)
            .OB("MB001")
            SQL = .TEXT
        End With

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)

        'If dt.Rows.Count > 1000 Then
        '    show_message.ShowMessage(Page, "ข้อมูลมากเกินไป กรุณาเพิ่มคำค้นหาให้มากขึ้น", UpdatePanel1)
        '    Exit Sub
        'End If
        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                BOM(.Text("MB001"))
            End With
        Next

        show_message.ShowMessage(Page, "update completed", UpdatePanel1)

    End Sub

    Sub BOM(ItemFind As String, Optional ByVal item As String = "", Optional ByVal itemMain As String = "")
        If String.IsNullOrEmpty(itemMain) Then
            itemMain = ItemFind
        End If
        If String.IsNullOrEmpty(item) Then
            item = ItemFind
        End If
        Dim SQL As String = "",
            WHR As String = ""
        Dim fld As New List(Of String) From {
            "MD003",'child item
            "round(MD006/MD007/MC004,2) MD006",'qty
            "MB025",'property=> P and M
            "MD008"'scrap ratio
        }
        With New SQLTEXT("BOMMD", fld)
            .LJ("BOMMC", New List(Of String) From { .LE("MC001", "MD001")})
            .LJ("INVMB", New List(Of String) From { .LE("MB001", "MD003")})
            .WE("MD001", ItemFind)
            .OB("MD003")
            SQL = .TEXT
        End With

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim itemChild As String = .Text("MD003")
                If .Text("MB025") = "M" Then 'manfactore
                    BOM(itemChild, item & "|" & itemChild, itemMain)
                End If
                Dim fldHash As New Hashtable From {
                    {"itemFG", itemMain},
                    {"itemParent", ItemFind},
                    {"itemChild", itemChild},
                    {"qpa", .Number("MD006")},
                    {"scrapRatio", .Number("MD008")},
                    {"property", .Text("MB025")}
                }
                Dim whrHash As New Hashtable From {
                    {"item", item & "|" & itemChild}
                }
                Dim USQL As String = dbConn.GetSQL("BOM", fldHash, whrHash, "UI")
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
    End Sub

End Class