Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class ProductionRecordAlter
    Inherits Page
    'Dim ControlForm As New ControlDataForm
    'Dim Conn_SQL As New ConnSQL
    'Dim configDate As New ConfigDate
    'Dim CreateTempTable As New CreateTempTable
    Dim CreateTable As New CreateTable
    Dim tableRecord As String = "ProductionProcessRecord"
    Dim tableSum As String = "ProductionProcessSum"
    Dim tableOper As String = "ProductionProcessOperator"
    Dim tableBOM As String = "ProductionProcessBOM"
    Dim tableLoss As String = "ProductionProcessLoss"
    Dim tableLog As String = "ProductionProcessLog"
    Dim usageTime As Integer = 0

    Dim dbConn As New DataConnectControl
    Dim ddlCont As New DropDownListControl
    Dim gvCont As New GridviewControl
    'Dim dtCont As New DataTableControl
    Dim dateCont As New DateControl
    Dim outCont As New OutputControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            CreateTable.CreateProductionProcessLog()
            'ucHeaderForm.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            clear()
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        tbTimeE.Text = ""
        tbTimeS.Text = ""
        ucDateS.dateVal = ""
        ucDateE.dateVal = ""
        Panel1.Visible = True
        Dim SQL As String = "", dt As DataTable
        SQL = " select rtrim(P.moType)+'-'+rtrim(P.moNo)+'-'+rtrim(P.moSeq) A,case when substring(R1.isSetTime,1,1)='Y' then 'Setting' else 'Running' end B," &
              " R1.tranNo C ,R1.wc D,P.mc E, " &
              " case when substring(R1.isSetTime,1,1)='Y' then case when P.setTime=0 then 0 else floor(P.setTime/60) end " &
              " else case when P.workTime=0 then 0 else floor(P.workTime/60) end end F," &
              " R1.timeCode H, isnull(R2.timeCode,'') K, " &
              " isnull(R2.acceptQty,0) L,isnull(R2.defectQty,0) M,M.TA035 O,isnull(R2.scrapQty,0) T," &
              " isnull(R2.scrapCode,'') U ,R1.docNo D1,R2.docNo D2,R1.dateCode G1,R2.dateCode G2,rtrim(R1.shift) shift," &
              " R1.tranNo,R2.breakTime,substring(R1.isSetTime,1,1) ist,R2.breakTime,P.recStatus,substring(R1.isSetTime,1,1) isSetTime " &
              " from " & tableSum & " P " &
              " left join " & tableRecord & " R1 on R1.docNo=P.docStart " &
              " left join " & tableRecord & " R2 on R2.docNo=P.docEnd " &
              " left join " & VarIni.ERP & "..SFCTA S on S.TA001=P.moType and S.TA002=P.moNo and S.TA003=P.moSeq  " &
              " left join " & VarIni.ERP & "..MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
              " where 1=1 and P.docNo='" & tbDocNo.Text.Trim & "'" &
              " order by R1.dateCode,R1.timeCode,P.moType,P.moNo,P.moSeq "
        '  dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        With dt
            If dt.Rows.Count > 0 Then
                Dim showDoc As Boolean = False
                If dt.Rows.Count = 1 Then
                    showDoc = True
                End If
                With .Rows(0)
                    tbMO.Enabled = showDoc
                    lbId.Text = tbDocNo.Text.Trim
                    tbMO.Text = Trim(.Item("A"))
                    ddlShift.Text = Trim(.Item("shift"))
                    lbSet.Text = Trim(.Item("isSetTime"))
                    lbStatus.Text = Trim(.Item("recStatus"))
                    lbBreak.Text = Trim(.Item("breakTime"))
                    lbWC.Text = .Item("D").ToString.Trim
                    lbMchOld.Text = Trim(.Item("E"))

                    Dim wc As String = .Item("D").ToString.Trim
                    If wc = "W07" Or wc = "W25" Then
                        wc = "W07,W25"
                    End If

                    Dim xSQL As String = "select rtrim(MX001) MX001 from CMSMX where MX002 in ('" & wc.Replace(",", "','") & "') and MX006<>'CANCEL' order by MX001"
                    ddlCont.showDDL(ddlMch, xSQL, VarIni.ERP, "MX001", "MX001")
                    ddlMch.Text = Trim(.Item("E"))
                    Dim setControl As Boolean = True
                    If lbSet.Text.Trim = "N" Then
                        tbAcceptQty.Text = .Item("L").ToString.Trim
                        tbDefQty.Text = .Item("M").ToString.Trim
                        tbScrapQty.Text = .Item("T").ToString.Trim

                        SQL = "select rtrim(Code) Code,rtrim(Code)+':'+Name Name from CodeInfo where CodeType='SCRAP' and WC like'%" & .Item("D").ToString.Trim & "%' order by Code"
                        ddlCont.showDDL(ddlScrapCode, SQL, VarIni.DBMIS, "Name", "Code", True)
                        If .Item("U").ToString.Trim <> "" Then
                            ddlScrapCode.Text = .Item("U").ToString.Trim
                        End If
                    Else
                        setControl = False
                    End If
                    tbAcceptQty.Enabled = setControl
                    tbDefQty.Enabled = setControl
                    tbScrapQty.Enabled = setControl
                    ddlScrapCode.Enabled = setControl
                    Dim WHR As String = ""
                    If .Item("tranNo").ToString.Trim = "" Then
                        WHR = " and P.docNo='" & tbDocNo.Text.Trim & "' "
                    Else
                        Dim tn() As String = .Item("tranNo").ToString.Trim.Split("-")
                        WHR = " and R1.tranNo like '" & tn(0) & "-" & tn(1) & "%' "
                        WHR &= " and R1.dateCode='" & Trim(.Item("G1")) & "' "
                        WHR &= " and R1.timeCode='" & Trim(.Item("H")) & "' "
                        WHR &= " and R1.isSetTime like '" & Trim(.Item("isSetTime")) & "%' "
                        WHR &= " and R2.dateCode='" & Trim(.Item("G2")) & "' "
                        WHR &= " and R2.timeCode='" & Trim(.Item("K")) & "' "
                        WHR &= " and R1.wc='" & Trim(.Item("D")) & "' "
                        WHR &= " and P.mc='" & Trim(.Item("E")) & "' "
                    End If
                    showData(WHR)
                End With
                btDelete.Visible = True
                btUpdate.Visible = True
            Else
                clearAll()
            End If
        End With

    End Sub

    Sub clearAll()

        tbDocNo.Text = ""
        lbId.Text = ""
        tbAcceptQty.Text = ""
        tbDefQty.Text = ""
        tbScrapQty.Text = ""
        ddlScrapCode.Items.Clear()

        tbAcceptQty.Enabled = True
        tbDefQty.Enabled = True
        tbScrapQty.Enabled = True
        ddlScrapCode.Enabled = True

        lbSet.Text = ""
        lbStatus.Text = ""
        lbBreak.Text = ""
        lbWC.Text = ""

        tbMO.Text = ""
        ddlSet.Text = "Sel"
        tbTimeS.Text = ""
        tbTimeE.Text = ""
        ucDateS.dateVal = ""
        ucDateE.dateVal = ""

        gvShow.DataSource = ""
        gvShow.DataBind()
        gvOper.DataSource = ""
        gvOper.DataBind()

    End Sub

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click

        Dim ISQL As String = "",
            USQL As String = "",
            dateS As String,
            dateE As String,
            timeS As String,
            timeE As String,
            shiftOld As String,
            dateOldS As String,
            dateOldE As String,
            timeOldS As String,
            timeOldE As String,
            dateTimeS As Date,
            dateTimeE As Date,
            workTime As Decimal,
            editCode As String = "1"

        Dim whrHash As Hashtable = New Hashtable,
            fldHash As Hashtable = New Hashtable,
            whrLogHash As Hashtable = New Hashtable,
            fldLogHash As Hashtable = New Hashtable

        With gvShow
            With .Rows(0)
                'check mo+wc
                If .Cells(gvCont.getColIndexByName(gvShow, "Start Date")).Text.Trim <> tbMO.Text.Trim Then
                    Dim moSpit() As String = tbMO.Text.Trim.Split("-")
                    Dim XSQL As String
                    XSQL = "select TA001,TA002,TA003,TA006 from SFCTA where TA006='" & lbWC.Text.Trim & "' " & If(moSpit.Length > 2, "and TA001='" & moSpit(0) & "' and TA002='" & moSpit(1) & "' and TA003='" & moSpit(2) & "' ", "")
                    Dim xdt As DataTable
                    ' xdt = Conn_SQL.Get_DataReader(XSQL, Conn_SQL.ERP_ConnectionString)
                    xdt = dbConn.Query(XSQL, VarIni.ERP, dbConn.WhoCalledMe)
                    With xdt
                        If .Rows.Count = 0 Then
                            show_message.ShowMessage(Page, "this W/C not found for mo =" & tbMO.Text.Trim, UpdatePanel1)
                            tbMO.Focus()
                            Exit Sub
                        End If
                    End With
                End If

                dateOldS = .Cells(gvCont.getColIndexByName(gvShow, "Start Date")).Text.Trim
                timeOldS = .Cells(gvCont.getColIndexByName(gvShow, "Start Time")).Text.Trim
                dateOldE = .Cells(gvCont.getColIndexByName(gvShow, "End Date")).Text.Trim
                timeOldE = .Cells(gvCont.getColIndexByName(gvShow, "End Time")).Text.Trim
                shiftOld = .Cells(gvCont.getColIndexByName(gvShow, "Shift")).Text.Trim

                dateS = If(ucDateS.dateVal = "", dateOldS, ucDateS.dateVal)
                timeS = If(tbTimeS.Text.Trim.Replace("__:__", "") = "", timeOldS, tbTimeS.Text.Trim)
                dateE = If(ucDateE.dateVal = "", dateOldE, ucDateE.dateVal)
                timeE = If(tbTimeE.Text.Trim.Replace("__:__", "") = "", timeOldE, tbTimeE.Text.Trim)

                dateTimeS = dateCont.strToDateTime(dateS & " " & timeS)
                dateTimeE = dateCont.strToDateTime(dateE & " " & timeE)
                workTime = dateCont.getTime(dateTimeS, dateTimeE)
                If lbBreak.Text.Trim = "N" Then
                    workTime -= dateCont.getBreakTime(dateTimeS, dateTimeE, ddlShift.Text.Trim)
                End If
            End With
            Dim sqlList As New ArrayList
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim shift As String = ddlShift.Text.Trim
                    Dim docStart As String = .Cells(gvCont.getColIndexByName(gvShow, "Doc Start")).Text.Trim,
                        mo As String = .Cells(gvCont.getColIndexByName(gvShow, "MO")).Text.Trim.Replace("&nbsp;", ""),
                        isSet As String = lbSet.Text.Trim,
                        docSum As String = .Cells(gvCont.getColIndexByName(gvShow, "Doc No")).Text.Trim

                    'start date time 'table ProductionProcessRecord
                    whrHash = New Hashtable
                    fldHash = New Hashtable

                    'collect log
                    whrLogHash = New Hashtable
                    fldLogHash = New Hashtable
                    whrLogHash.Add("docStart", docStart) 'doc start
                    'old
                    fldLogHash.Add("editCode", editCode)
                    fldLogHash.Add("dateStartOld", dateOldS)
                    fldLogHash.Add("timeStartOld", timeOldS)
                    fldLogHash.Add("dateEndOld", dateOldE)
                    fldLogHash.Add("timeEndOld", timeOldE)
                    fldLogHash.Add("shiftOld", shiftOld)
                    fldLogHash.Add("moOld", mo)
                    fldLogHash.Add("isSetOld", isSet)
                    fldLogHash.Add("mcOld", lbMchOld.Text.Trim)
                    If docSum = lbId.Text.Trim Then
                        mo = tbMO.Text.Trim
                        isSet = If(ddlSet.Text.Trim = "Sel", "", ddlSet.Text.Trim)
                    End If
                    Dim xmo() As String = mo.Split("-")
                    'new
                    fldLogHash.Add("dateStart", dateS)
                    fldLogHash.Add("timeStart", timeS)
                    fldLogHash.Add("dateEnd", dateE)
                    fldLogHash.Add("timeEnd", timeE)
                    fldLogHash.Add("shift", shift)
                    fldLogHash.Add("mo", mo)
                    fldLogHash.Add("isSet", isSet)
                    fldLogHash.Add("mc", ddlMch.Text.Trim)
                    whrHash.Add("docNo", docStart)
                    fldHash.Add("shift", shift)
                    fldHash.Add("dateCode", dateS)
                    fldHash.Add("timeCode", timeS)
                    If xmo.Length > 2 Then
                        fldHash.Add("moType", xmo(0))
                        fldHash.Add("moNo", xmo(1))
                        fldHash.Add("moSeq", xmo(2))
                    End If
                    If isSet <> "" Then
                        fldHash.Add("isSetTime", isSet & "B")
                    End If
                    If ddlMch.Text.Trim <> lbMchOld.Text.Trim Then
                        fldHash.Add("mc", ddlMch.Text.Trim)
                    End If
                    'USQL = Conn_SQL.GetSQL(tableRecord, fldHash, whrHash, "U")
                    sqlList.Add(dbConn.getUpdateSql(tableRecord, fldHash, whrHash))
                    'end date time 
                    whrHash = New Hashtable
                    fldHash = New Hashtable
                    whrHash.Add("docNo", .Cells(gvCont.getColIndexByName(gvShow, "Doc End")).Text.Trim)
                    fldHash.Add("shift", shift)
                    fldHash.Add("dateCode", dateE)
                    fldHash.Add("timeCode", timeE)
                    Dim acceptQty As Integer = CInt(.Cells(gvCont.getColIndexByName(gvShow, "Accept Qty")).Text.Trim.Replace("&nbsp;", "0")),
                        defectQty As Integer = CInt(.Cells(gvCont.getColIndexByName(gvShow, "Return Qty")).Text.Trim.Replace("&nbsp;", "0")),
                        scrapQty As Integer = CInt(.Cells(gvCont.getColIndexByName(gvShow, "Scrap Qty")).Text.Trim.Replace("&nbsp;", "0")),
                        scrapCode As String = .Cells(gvCont.getColIndexByName(gvShow, "Scrap Code")).Text.Trim.Replace("&nbsp;", "")
                    'old
                    fldLogHash.Add("acceptQtyOld", acceptQty)
                    fldLogHash.Add("defectQtyOld", defectQty)
                    fldLogHash.Add("scrapQtyOld", scrapQty)
                    fldLogHash.Add("scrapCodeOld", scrapCode)

                    If docSum = lbId.Text.Trim Then
                        acceptQty = outCont.checkNumberic(tbAcceptQty)
                        defectQty = outCont.checkNumberic(tbDefQty)
                        scrapQty = outCont.checkNumberic(tbScrapQty)
                        scrapCode = ddlScrapCode.Text.Trim.Replace("Select", "")
                        mo = tbMO.Text.Trim
                        isSet = If(ddlSet.Text.Trim = "Sel", "", ddlSet.Text.Trim)
                    End If
                    fldHash.Add("acceptQty", acceptQty)
                    fldHash.Add("defectQty", defectQty)
                    fldHash.Add("scrapQty", scrapQty)
                    fldHash.Add("scrapCode", scrapCode)
                    If xmo.Length > 2 Then
                        fldHash.Add("moType", xmo(0))
                        fldHash.Add("moNo", xmo(1))
                        fldHash.Add("moSeq", xmo(2))
                    End If
                    If isSet <> "" Then
                        fldHash.Add("isSetTime", isSet & "E")
                    End If
                    If ddlMch.Text.Trim <> lbMchOld.Text.Trim Then
                        fldHash.Add("mc", ddlMch.Text.Trim)
                    End If
                    'USQL &= Conn_SQL.GetSQL(tableRecord, fldHash, whrHash, "U")
                    sqlList.Add(dbConn.getUpdateSql(tableRecord, fldHash, whrHash))

                    'new
                    fldLogHash.Add("acceptQty", acceptQty)
                    fldLogHash.Add("defectQty", defectQty)
                    fldLogHash.Add("scrapQty", scrapQty)
                    fldLogHash.Add("scrapCode", scrapCode)

                    fldLogHash.Add("CreateBy", Session("UserName"))
                    fldLogHash.Add("CreateDate", DateTime.Now.ToString("yyyyMMdd HHmmss"))

                    'insert new for log
                    'USQL &= Conn_SQL.GetSQL(tableLog, fldLogHash, whrLogHash, "I")
                    sqlList.Add(dbConn.getInsertSql(tableLog, fldHash, whrHash))
                    'sum
                    whrHash = New Hashtable
                    fldHash = New Hashtable
                    whrHash.Add("docNo", docSum)
                    Dim fld As String = "workTime"
                    If lbSet.Text.Trim = "Y" Then
                        fld = "setTime"
                    End If
                    If ddlMch.Text.Trim <> lbMchOld.Text.Trim Then
                        fldHash.Add("mc", ddlMch.Text.Trim)
                    End If
                    Dim useTime As Integer = getTimeUsage(i, workTime)
                    fldHash.Add(fld, useTime)
                    If xmo.Length > 2 Then
                        fldHash.Add("moType", xmo(0))
                        fldHash.Add("moNo", xmo(1))
                        fldHash.Add("moSeq", xmo(2))
                    End If
                    'USQL &= Conn_SQL.GetSQL(tableSum, fldHash, whrHash, "U")
                    sqlList.Add(dbConn.getUpdateSql(tableSum, fldHash, whrHash))
                    'operator
                    With gvOper
                        Dim cntOper As Decimal = .Rows.Count
                        Dim avgQty As Decimal = If(acceptQty > 0, If(cntOper = 1, acceptQty, Math.Round(acceptQty / cntOper, 2)), 0)
                        Dim avgDef As Decimal = If(defectQty > 0, If(cntOper = 1, defectQty, Math.Round(defectQty / cntOper, 2)), 0)
                        Dim avgScp As Decimal = If(scrapQty > 0, If(cntOper = 1, scrapQty, Math.Round(scrapQty / cntOper, 2)), 0)
                        fldHash = New Hashtable
                        whrHash = New Hashtable
                        whrHash.Add("docStart", docStart) 'doc start
                        fldHash.Add("acceptQty", avgQty)
                        fldHash.Add("defectQty", avgDef)
                        fldHash.Add("scrapQty", avgScp)
                        If ddlMch.Text.Trim <> lbMchOld.Text.Trim Then
                            fldHash.Add("mc", ddlMch.Text.Trim)
                        End If
                        If xmo.Length > 2 Then
                            fldHash.Add("moType", xmo(0))
                            fldHash.Add("moNo", xmo(1))
                            fldHash.Add("moSeq", xmo(2))
                        End If
                        ' USQL &= Conn_SQL.GetSQL(tableOper, fldHash, whrHash, "U")
                        sqlList.Add(dbConn.getUpdateSql(tableOper, fldHash, whrHash))

                    End With
                End With
            Next
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(sqlList, VarIni.DBMIS, dbConn.WhoCalledMe)
            ' Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        End With
        btShow_Click(sender, e)
    End Sub

    Protected Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        Dim SQL As String = ""
        Dim sumID As String
        Dim startID As String
        Dim endID As String,
            editCode As String = "0"
        Dim whrLogHash As Hashtable = New Hashtable,
            fldLogHash As Hashtable = New Hashtable
        With gvShow
            Dim sqlList As New ArrayList
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    whrLogHash = New Hashtable
                    fldLogHash = New Hashtable
                    fldLogHash.Add("editCode", editCode)
                    fldLogHash.Add("dateStartOld", .Cells(gvCont.getColIndexByName(gvShow, "Start Date")).Text.Trim)
                    fldLogHash.Add("timeStartOld", .Cells(gvCont.getColIndexByName(gvShow, "Start Time")).Text.Trim)
                    fldLogHash.Add("dateEndOld", .Cells(gvCont.getColIndexByName(gvShow, "End Date")).Text.Trim)
                    fldLogHash.Add("timeEndOld", .Cells(gvCont.getColIndexByName(gvShow, "End Time")).Text.Trim)
                    fldLogHash.Add("shiftOld", .Cells(gvCont.getColIndexByName(gvShow, "Shift")).Text.Trim)
                    fldLogHash.Add("moOld", .Cells(gvCont.getColIndexByName(gvShow, "MO")).Text.Trim)
                    fldLogHash.Add("isSetOld", lbSet.Text)
                    Dim acceptQty As Integer = CInt(.Cells(gvCont.getColIndexByName(gvShow, "Accept Qty")).Text.Trim.Replace("&nbsp;", "0")),
                        defectQty As Integer = CInt(.Cells(gvCont.getColIndexByName(gvShow, "Return Qty")).Text.Trim.Replace("&nbsp;", "0")),
                        scrapQty As Integer = CInt(.Cells(gvCont.getColIndexByName(gvShow, "Scrap Qty")).Text.Trim.Replace("&nbsp;", "0")),
                        scrapCode As String = .Cells(gvCont.getColIndexByName(gvShow, "Scrap Code")).Text.Trim.Replace("&nbsp;", "")
                    fldLogHash.Add("acceptQtyOld", acceptQty)
                    fldLogHash.Add("defectQtyOld", defectQty)
                    fldLogHash.Add("scrapQtyOld", scrapQty)
                    fldLogHash.Add("scrapCodeOld", scrapCode)
                    'SQL &= Conn_SQL.GetSQL(tableLog, fldLogHash, whrLogHash, "I")
                    sqlList.Add(dbConn.getInsertSql(tableLog, fldLogHash, whrLogHash))
                    sumID = .Cells(gvCont.getColIndexByName(gvShow, "Doc No")).Text.Trim
                    startID = .Cells(gvCont.getColIndexByName(gvShow, "Doc Start")).Text.Trim
                    endID = .Cells(gvCont.getColIndexByName(gvShow, "Doc End")).Text.Trim
                    'record
                    'start-end
                    'SQL = " delete from " & tableRecord & " where docNo in ('" & startID & "','" & endID & "'); "
                    sqlList.Add("delete from " & tableRecord & " where docNo in ('" & startID & "','" & endID & "'); ")
                    'operator
                    sqlList.Add(" delete from " & tableOper & " where docStart='" & startID & "'; ")
                    'BOM
                    sqlList.Add(" delete from " & tableBOM & " where docStart='" & startID & "'; ")
                    'sum
                    sqlList.Add(" delete from " & tableSum & " where docNo='" & sumID & "'; ")
                    'loss time
                    SQL &= " delete from " & tableLoss & " where docStart='" & startID & "'; "
                    sqlList.Add(" delete from " & tableLoss & " where docStart='" & startID & "'; ")
                    'Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
                    dbConn.TransactionSQL(sqlList, VarIni.DBMIS, dbConn.WhoCalledMe)
                End With
            Next
        End With
        btReset_Click(sender, e)
    End Sub

    Private Function checkNumberic(tb As TextBox) As Integer
        Dim valReturn As Decimal = 0
        Dim val As String = tb.Text.Trim
        If val <> "" And IsNumeric(val) Then
            valReturn = CInt(val)
        End If
        Return valReturn
    End Function

    Private Sub showData(WHR As String)
        Dim colName() As String = {"Doc No:Z",
                                    "Job Type:C",
                                    "MO:D",
                                    "Batch No:D0",
                                    "Process:E",
                                    "Spec:F",
                                    "Accept Qty:G:0",
                                    "Return Qty:H:0",
                                    "Scrap Qty:I1:0",
                                    "Scrap Code:I",
                                    "Start Date:B",
                                    "Start Time:K",
                                    "End Date:B1",
                                    "End Time:L",
                                    "Work Time(Min/Man):M:0",
                                    "Man Power:N:0",
                                    "Over All Time(Min):MN:0",
                                    "STD Time Man(Min/PC):O:2",
                                    "STD Time Mch(Min/PC):P:2",
                                    "W/C:Q",
                                    "Mach/Line:R",
                                    "Part Item:S",
                                    "Shift:A",
                                    "Point/Opt:U",
                                    "Point:T:0",
                                    "Doc Start:DS",
                                    "Doc End:DE"}
        Dim SQL As String
        SQL = " select isnull(R1.shift,'') A,R1.dateCode B,R2.dateCode B1,case when isnull(R1.isMulti,'')='' then '' else case when isnull(R1.isMulti,'')='N' then 'Single' else 'Multi' end end C," &
              " rtrim(P.moType)+'-'+rtrim(P.moNo)+'-'+rtrim(P.moSeq) D,M.UDF01 D0," &
              " C.MW002 E,M.TA035 F,isnull(R2.acceptQty,0) G,isnull(R2.defectQty,0) H,isnull(R2.scrapQty,0) I1,isnull(R2.scrapCode,'') I," &
              " substring(R1.isSetTime,1,1) J,R1.timeCode K,R2.timeCode L, " &
              " case when substring(isnull(R1.isSetTime,''),1,1)='' or substring(isnull(R1.isSetTime,''),1,1)='Y' then " &
              " case when P.setTime=0 then 0 else floor(P.setTime/60) end else " &
              " case when P.workTime=0 then 0 else floor(P.workTime/60) end end M ," &
              " case when substring(isnull(R1.isSetTime,''),1,1)='' or substring(isnull(R1.isSetTime,''),1,1)='Y' then case when P.setTime=0 then 0 else floor(P.setTime/60) end else  case when P.workTime=0 then 0 else floor(P.workTime/60) end end * P.manPower MN ," &
              " P.manPower N,R1.wc Q,P.mc R, M.TA006 S," &
              " case when substring(isnull(R1.isSetTime,''),1,1)='' or substring(isnull(R1.isSetTime,''),1,1)='Y' then " &
              " case when F.MF009=0 then 0 else floor(F.MF009/60) end else " &
              " case when F.MF010=0 then 0 else round(F.MF010/60,2) end end O ," &
              " case when substring(isnull(R1.isSetTime,''),1,1)='' or substring(isnull(R1.isSetTime,''),1,1)='Y' then " &
              " case when F.MF024=0 then 0 else floor(F.MF024/60) end else " &
              " case when F.MF025=0 then 0 else round(F.MF025/60,2) end end P ," &
              " P.docNo Z ,R1.point T,R1.processCode U , P.docStart DS,P.docEnd DE,P.recStatus rec " &
              " from ProductionProcessSum P " &
              " left join ProductionProcessRecord R1 on R1.docNo=P.docStart  " &
              " left join ProductionProcessRecord R2 on R2.docNo=P.docEnd " &
              " left join " & VarIni.ERP & "..SFCTA S on S.TA001=P.moType and S.TA002=P.moNo and S.TA003=P.moSeq  " &
              " left join " & VarIni.ERP & "..MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
              " left join " & VarIni.ERP & "..CMSMW C on C.MW001=S.TA004 " &
              " left join " & VarIni.ERP & "..BOMMF F on F.MF001=M.TA006 and F.MF002='01' and F.MF003=P.moSeq " &
              " where 1=1 " & WHR &
              " order by R1.dateCode,R1.timeCode,P.moType,P.moNo,P.moSeq "

        gvCont.GridviewColWithLinkFirst(gvShow, colName, False)
        'ControlForm.gridviewColVisible(gvShow, colHide, True)
        gvCont.ShowGridView(gvShow, SQL, VarIni.DBMIS)

        With gvShow
            If .Rows.Count > 0 Then
                Dim txtDocS As String = ""
                For i As Integer = 0 To .Rows.Count - 1
                    txtDocS &= .Rows(i).Cells(gvCont.getColIndexByName(gvShow, "Doc Start")).Text.Trim & ","
                Next
                Dim colName1() As String = {"OP Code:A", "OP Name:B", "Loss Time:C:0"}
                gvCont.GridviewColWithLinkFirst(gvOper, colName1, False)

                WHR = " and O.docStart in('" & txtDocS.Substring(0, txtDocS.Length - 1).Replace(",", "','") & "')"
                SQL = " select O.opCode A,V.MV047 B,sum(floor(isnull(L.lossTime,0)/60)) C from ProductionProcessOperator O " &
                      " left join " & VarIni.ERP & "..CMSMV V on V.MV001 =O.opCode " &
                      " left join ProductionProcessLoss L on L.opCode=O.opCode" &
                      " where 1=1 " & WHR & " group by O.opCode,V.MV047 order by O.opCode"
                gvCont.ShowGridView(gvOper, SQL, VarIni.DBMIS)
            End If
        End With
        'ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Private Function getTimeUsage(line As Integer, workTime As Integer) As Integer
        Dim tempTime As Integer
        With gvShow
            If .Rows.Count > 1 Then
                If lbSet.Text.Trim = "Y" Or lbStatus.Text.Trim = "0" Then
                    tempTime = Math.Floor(workTime / CInt(.Rows.Count))
                Else
                    If usageTime = 0 Then
                        Dim qtySum As Integer = 0
                        For i As Integer = 0 To .Rows.Count - 1
                            With .Rows(i)
                                Dim docSum As String = .Cells(gvCont.getColIndexByName(gvShow, "Doc No")).Text.Trim
                                If docSum = lbId.Text.Trim Then
                                    qtySum += outCont.checkNumberic(tbAcceptQty)
                                    qtySum += outCont.checkNumberic(tbDefQty)
                                    qtySum += outCont.checkNumberic(tbScrapQty)
                                Else
                                    qtySum += CInt(.Cells(gvCont.getColIndexByName(gvShow, "Accept Qty")).Text.Trim)
                                    qtySum += CInt(.Cells(gvCont.getColIndexByName(gvShow, "Return Qty")).Text.Trim)
                                    qtySum += CInt(.Cells(gvCont.getColIndexByName(gvShow, "Scrap Qty")).Text.Trim)
                                End If
                            End With
                        Next
                        If qtySum > 0 Then
                            usageTime = Math.Floor(workTime / qtySum)
                        End If
                    End If
                    Dim qty As Integer = 0
                    With .Rows(line)
                        Dim docSum As String = .Cells(gvCont.getColIndexByName(gvShow, "Doc No")).Text.Trim
                        If docSum = lbId.Text.Trim Then
                            qty += outCont.checkNumberic(tbAcceptQty)
                            qty += outCont.checkNumberic(tbDefQty)
                            qty += outCont.checkNumberic(tbScrapQty)
                        Else
                            qty += CInt(.Cells(gvCont.getColIndexByName(gvShow, "Accept Qty")).Text.Trim)
                            qty += CInt(.Cells(gvCont.getColIndexByName(gvShow, "Return Qty")).Text.Trim)
                            qty += CInt(.Cells(gvCont.getColIndexByName(gvShow, "Scrap Qty")).Text.Trim)
                        End If

                    End With
                    tempTime = qty * usageTime
                End If
            Else '=1
                tempTime = workTime
            End If
        End With
        Return tempTime

    End Function


    Protected Sub ddlSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSet.SelectedIndexChanged
        Dim showBT As Boolean = False
        If ddlSet.Text.Trim = "N" Then
            showBT = True
        ElseIf ddlSet.Text.Trim = "Sel" Then
            If lbSet.Text.Trim = "N" Then
                showBT = True
            End If
        End If
        'tbTimeS.Enabled = showBT
        'tbTimeE.Enabled = showBT
        tbAcceptQty.Enabled = showBT
        tbDefQty.Enabled = showBT
        tbScrapQty.Enabled = showBT
        ddlScrapCode.Enabled = showBT
        If showBT Then
            Dim SQL As String
            SQL = "select rtrim(Code) Code,rtrim(Code)+':'+Name Name from CodeInfo where CodeType='SCRAP' and WC like'%" & lbWC.Text.Trim & "%' order by Code"
            ddlCont.showDDL(ddlScrapCode, SQL, VarIni.DBMIS, "Name", "Code", True)
        Else
            ddlScrapCode.Items.Clear()
        End If

    End Sub
    Sub clear()
        btDelete.Visible = False
        btUpdate.Visible = False
        Panel1.Visible = False
        clearAll()
    End Sub

    Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        clear()
    End Sub
End Class