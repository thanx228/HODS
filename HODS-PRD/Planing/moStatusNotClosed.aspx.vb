Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class moStatusNotClosed
    Inherits System.Web.UI.Page
    'Dim ControlForm As New ControlDataForm
    'Dim Conn_SQL As New ConnSQL
    'Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim dbConn As New DataConnectControl
    Dim outCont As New OutputControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            ucWc.setObject = ""
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String,
            WHR As String,
            dt As DataTable,
            tempTable As String = "tempMOnotClose" & Session("UserName"),
            tempTable2 As String = "tempMOnotCloseSum" & Session("UserName"),
            whrHash As Hashtable,
            fldHash As Hashtable,
            strSQL As String = "",
            monthList As ArrayList = New ArrayList
        CreateTempTable.createTempWIPNOTCLOSED(tempTable)
        'wip >0 
        WHR = dbConn.WHERE_EQUAL("MOCTA.TA010", ucDate.dateVal, "<=") 'head plan date
        WHR &= dbConn.WHERE_IN("SFCTA.TA006", ucWc.getObject)
        SQL = "insert into " & tempTable & "(moType,moNo,moSeq,wip) "
        SQL &= " select SFCTA.TA001,SFCTA.TA002,SFCTA.TA003,SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 from " & VarIni.ERP & "..SFCTA " &
              " left join " & VarIni.ERP & "..MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002  " &
              " where MOCTA.TA011 not in('Y','y') and SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 >0 " & WHR
        'Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        'wip=0
        WHR = dbConn.WHERE_EQUAL("MOCTA.TA010", ucDate.dateVal, "<=") 'head plan date
        WHR &= dbConn.WHERE_IN("(select top 1 TA006 from SFCTA TA3 where TA3.TA001=SFCTA.TA001 and TA3.TA002=SFCTA.TA002 order by TA003)", ucWc.getObject)
        SQL = " select SFCTA.TA001,SFCTA.TA002,(select top 1 TA003 from SFCTA TA2 where TA2.TA001=SFCTA.TA001 and TA2.TA002=SFCTA.TA002 order by TA003),SUM(SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058) from SFCTA " &
              " left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " &
              " where MOCTA.TA011 not in('Y','y') " & WHR &
              " group by SFCTA.TA001,SFCTA.TA002 having SUM(SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058)=0 "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        Dim sqlList As New ArrayList
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim XSQL As String = "",
                    Xdt As DataTable = New DataTable
                XSQL = ""
                whrHash = New Hashtable
                fldHash = New Hashtable
                whrHash.Add("moType", Trim(.Item(0)))
                whrHash.Add("moNo", Trim(.Item(1)))
                whrHash.Add("moSeq", Trim(.Item(2)))
                fldHash.Add("wip", .Item(3))
                'strSQL &= dbConn.GetSQL(tempTable, fldHash, whrHash)
                sqlList.Add(dbConn.GetSQL(tempTable, fldHash, whrHash))
            End With
        Next
        If sqlList.Count > 0 Then
            dbConn.TransactionSQL(sqlList, VarIni.DBMIS, dbConn.WhoCalledMe)
        End If
        'If strSQL <> "" Then
        '    Conn_SQL.Exec_Sql(strSQL, Conn_SQL.MIS_ConnectionString)

        'End If
        'get month list
        WHR = dbConn.WHERE_IN("SFCTA.TA006", ucWc.getObject)
        SQL = " select substring(MOCTA.TA010,1,6),count(*) " &
              " from " & VarIni.DBMIS & ".." & tempTable & " T left join SFCTA on SFCTA.TA001=T.moType and SFCTA.TA002=T.moNo and SFCTA.TA003=T.moSeq " &
              " left join MOCTA on MOCTA.TA001=SFCTA.TA001 and  MOCTA.TA002=SFCTA.TA002 " &
              " where substring(MOCTA.TA010,1,6)<>'' " &
              " group by substring(MOCTA.TA010,1,6) order by substring(MOCTA.TA010,1,6)"
        dt = dbConn.Query(SQL, VarIni.ERP)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                monthList.Add(Trim(.Item(0)))
            End With
        Next
        CreateTempTable.createTempWIPNOTCLOSEDSUM(tempTable2, monthList)
        Dim sSQL As String = "select DISTINCT rtrim(moType)+rtrim(moNo) from " & tempTable & " T "
        SQL = " select MOCTA.TA001,substring(MOCTA.TA010,1,6),count(*) " &
              " from  " & VarIni.ERP & "..MOCTA where rtrim(TA001)+rtrim(TA002) in(" & sSQL & ") and MOCTA.TA010<>'' " &
              " group by MOCTA.TA001,substring(MOCTA.TA010,1,6) order by MOCTA.TA001,substring(MOCTA.TA010,1,6)"
        dt = dbConn.Query(SQL, VarIni.DBMIS)
        Dim lastMoType As String = "",
            moType As String = ""
        strSQL = ""
        sqlList = New ArrayList
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                moType = Trim(.Item(0))
                If lastMoType <> Trim(.Item(0)) Then
                    If lastMoType <> "" Then
                        'strSQL &= Conn_SQL.GetSQL(tempTable2, fldHash, whrHash)
                        sqlList.Add(dbConn.GetSQL(tempTable2, fldHash, whrHash))
                    End If
                    whrHash = New Hashtable
                    fldHash = New Hashtable
                    whrHash.Add("moType", Trim(.Item(0)))
                End If
                fldHash.Add("C" & Trim(.Item(1)), .Item(2))
                lastMoType = Trim(.Item(0))
            End With
        Next
        If lastMoType <> "" Then
            'strSQL &= Conn_SQL.GetSQL(tempTable2, fldHash, whrHash)
            sqlList.Add(dbConn.GetSQL(tempTable2, fldHash, whrHash))
        End If

        If sqlList.Count > 0 Then
            dbConn.TransactionSQL(sqlList, VarIni.DBMIS, dbConn.WhoCalledMe)
        End If
        'show sum
        Dim colName As ArrayList = New ArrayList,
            fld As String = ""
        colName.Add("MO Type:A")
        fld &= "moType A,"
        colName.Add("MO Type Name:B")
        fld &= "MQ002 B,"
        Dim fldSum As String = "0"
        For Each mm As String In monthList
            Dim fldName As String = "C" & mm
            colName.Add(mm.Substring(4, 2) & "-" & mm.Substring(0, 4) & ":" & fldName & ":0")
            fld &= fldName & ","
            fldSum &= "+" & fldName
        Next
        colName.Add("Sum MO:SUMMO:0")
        fld &= fldSum & " SUMMO "

        Dim gvCont As New GridviewControl

        gvCont.GridviewColWithLinkFirst(gvSum, colName)
        SQL = "select " & fld & " from " & tempTable2 & " T left join " & VarIni.ERP & "..CMSMQ on MQ001=T.moType order by MQ003,MQ001"
        gvCont.ShowGridView(gvSum, SQL, VarIni.DBMIS)
        ucCountRowSum.RowCount = gvCont.rowGridview(gvSum)

        'show detail
        colName = New ArrayList
        fld = "MOCTA.TA010 A,"
        colName.Add("MO Plan Complete:A")
        fld &= "SFCTA.TA009 B,"
        colName.Add("Process Plan Complete:B")
        fld &= "case MOCTA.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' else 'MO Completed' end C,"
        colName.Add("MO Status:C")
        fld &= "CMSMQ.MQ002 D1,"
        colName.Add("MO Type Name:D1")
        fld &= "SFCTA.TA001 D2,"
        colName.Add("MO Type:D2")
        fld &= "SFCTA.TA002 E,"
        colName.Add("MO:E")
        fld &= "SFCTA.TA003 F,"
        colName.Add("MO Seq:F")
        fld &= "SFCTA.TA006 G1,"
        colName.Add("W/C:G1")
        fld &= "CMSMD.MD002 G2,"
        colName.Add("W/C Name:G2")
        fld &= "SFCTA.TA004 H1,"
        colName.Add("Operation:H1")
        fld &= "CMSMW.MW002 H2,"
        colName.Add("OP DESC:H2")
        fld &= "case when len(MOCTA.TA006)=16 then (SUBSTRING(MOCTA.TA006,1,14)+'-'+SUBSTRING(MOCTA.TA006,15,2)) else MOCTA.TA006 end I,"
        colName.Add("Item:I")
        fld &= "MOCTA.TA034 J,"
        colName.Add("Desc:J")
        fld &= "MOCTA.TA035 K,"
        colName.Add("Spec:K")
        fld &= "MOCTA.TA015  L,"
        colName.Add("Plan Qty:L:0")
        fld &= "SFCTA.TA011  M,"
        colName.Add("Completed Qty:M:0")
        fld &= "SFCTA.TA012+SFCTA.TA056  N,"
        colName.Add("Scrap Qty:N:0")
        fld &= "MOCTA.TA007 O,"
        colName.Add("Unit:O")
        fld &= "SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 P,"
        colName.Add("WIP QTY:P:0")
        fld &= "isnull((select top 1 SFCTC.TC001+'-'+SFCTC.TC002 from " & VarIni.ERP & "..SFCTC where SFCTC.TC004=SFCTA.TA001 and SFCTC.TC005=SFCTA.TA002 and SFCTC.TC006=SFCTA.TA003 order by SFCTC.CREATE_DATE desc),'') Q,"
        colName.Add("Last Transfer:Q")
        fld &= "MOCTA.TA026 R,"
        colName.Add("SO Type:R")
        fld &= "MOCTA.TA027 S,"
        colName.Add("SO No:S")
        fld &= "MOCTA.TA028 T,"
        colName.Add("SO Seq:T")
        fld &= "COPTC.TC004 U,"
        colName.Add("Customer:U")
        fld &= "COPMA.MA002 V,"
        colName.Add("Customer Desc:V")
        fld &= "COPTC.TC012  W "
        colName.Add("Industry Type:W")

        gvCont.GridviewColWithLinkFirst(gvDetail, colName)
        SQL = " select " & fld & " from " & VarIni.DBMIS & ".." & tempTable & " T " &
              " left join SFCTA on SFCTA.TA001=T.moType and SFCTA.TA002=T.moNo and SFCTA.TA003=T.moSeq " &
              " left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " &
              " left join CMSMQ on CMSMQ.MQ001=MOCTA.TA001 " &
              " left join CMSMD on CMSMD.MD001=SFCTA.TA006 " &
              " left join CMSMW on CMSMW.MW001=SFCTA.TA004 " &
              " left join COPTC on COPTC.TC001=MOCTA.TA026 and COPTC.TC002=MOCTA.TA027 " &
              " left join COPMA on COPMA.MA001=COPTC.TC004 " &
              " order by SFCTA.TA001,SFCTA.TA002,SFCTA.TA003 "
        gvCont.ShowGridView(gvDetail, SQL, VarIni.ERP)
        ucCountRowDetail.RowCount = gvCont.rowGridview(gvDetail)
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvDetail", "gridviewScrollgvDetail();", True)


    End Sub

    Private Sub gvSum_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSum.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim moType As String = .Cells(0).Text.Trim
                For i As Decimal = 2 To gvSum.HeaderRow.Cells.Count - 2
                    With .Cells(i)
                        .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                        Dim mm() As String = gvSum.HeaderRow.Cells(i).Text.Trim.Split("-")
                        .Attributes.Add("onclick", "NewWindow('moStatusNotClosedPop.aspx?tempTable=tempMOnotClose" & Session("UserName") & "&motype=" & moType & "&selmonth=" & mm(1) & mm(0) & "','_blank',800,500,'yes')")
                    End With
                Next
            End If
            If .RowType = DataControlRowType.Footer Then
                gvSum.ShowFooter = True
                .Cells(1).Text = "Sum"
                For i As Decimal = 2 To gvSum.HeaderRow.Cells.Count - 1
                    Dim val As Decimal = 0
                    For j As Decimal = 0 To gvSum.Rows.Count - 1
                        val += outCont.checkNumberic(gvSum.Rows(j).Cells(i).Text)
                    Next
                    .Cells(i).Text = val
                    .Cells(i).HorizontalAlign = HorizontalAlign.Right
                Next
            End If
        End With
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        Dim expCont As New ExportImportControl
        expCont.Export("MoStatusNotClosed" & Session("UserName"), gvDetail)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class