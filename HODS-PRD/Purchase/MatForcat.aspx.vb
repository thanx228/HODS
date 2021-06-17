Imports System.Globalization
Imports MIS_HTI.DataControl

Public Class MatForcat
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim dbConn As New DataConnectControl
    Const whList As String = "2800,2400,2101,3333,2600,2901,2207,2204,2203,2201,2206,2205,2900,2300,5003"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False
            tcShow.ActiveTabIndex = 0
        End If
    End Sub

    Sub showForMat()

        Dim date1 As String = tbDateFrom.Text,
           date2 As String = tbDateTo.Text,
           strDate As String = "",
           endDate As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat2(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'strDate = strDate & "01"
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat2(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        ' endDate = endDate & "31"
        Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim amtMonth As Short = DateDiff(DateInterval.Month, beginDate, lastDate)
        If beginDate = lastDate Then
            amtMonth = 1
        End If
        'Dim tempTable As String = "tempSaleUndeliveryPeriod" & Session("UserName")
        Dim tempTable As String = "tempMatForcast" & Session("UserName"),
            tempTable2 As String = "tempBOMMAT" & Session("UserName"),
            tempTable3 As String = "tempBOMShow" & Session("UserName"),
            code As String = ""

        Dim dt As New DataTable,
            item As String = "",
            qty As Decimal = 0,
            lastCode As String = ""

        CreateTempTable.createTempMatForcast(tempTable, beginDate, amtMonth)
        CreateTempTable.createTempBomMat(tempTable2)
        CreateTempTable.createTempBomShow(tempTable3)

        'check Mat from BOM
        Dim SQL As String = "",
            whr As String = "",
            USQL As String = ""

        'Sale forcast
        whr = ""
        If tbCust.Text.Trim <> "" Then
            whr = whr & " and ME002 in('" & tbCust.Text.Trim.Replace(",", "','") & "') "
        End If
        whr = whr & Conn_SQL.Where("MF003", tbItem)

        SQL = "select distinct MF003 from COPMF left join COPME on ME001=MF001 " &
              " where MF008-MF009>0 " & whr &
              " order by MF003 "
        Dim Program As New DataTable
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            CodeBOM(tempTable, tempTable2, tempTable3, Program.Rows(i).Item("MF003"))
        Next

        'Sale Order
        whr = ""
        If tbCust.Text.Trim <> "" Then
            whr = whr & " and TC004 in('" & tbCust.Text.Trim.Replace(",", "','") & "') "
        End If
        whr = whr & Conn_SQL.Where("TD004", tbItem)
        SQL = " select distinct TD004 from COPTD left join COPTC on TC001=TD001 and TC002=TD002 " &
              " where TC027='Y' and TD016='N' " & whr &
              " order by TD004 "
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            CodeBOM(tempTable, tempTable2, tempTable3, Program.Rows(i).Item("TD004"))
        Next

        'low to high  and MB025='M'
        SQL = " select distinct MD001,MD003,MD006,MD008 from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join BOMMD on MD003 = T.item " &
              " left join INVMB on MB001 = MD001 " &
              " where MB109='Y'  " &
              " and MD001+'-'+MD003 not in(select itemParent+'-'+itemMAT from " & Conn_SQL.DBReport & ".." & tempTable2 & ") "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                'BOMMAT
                USQL = " if not exists(select * from " & tempTable2 & " where itemParent='" & .Item("MD001") & "' and itemMAT='" & .Item("MD003") & "' ) " &
                       " insert into " & tempTable2 & "(itemParent,itemMAT,qty,scrapRatio)values ('" & .Item("MD001") & "','" & .Item("MD003") & "','" & .Item("MD006") & "','" & .Item("MD008") & "')"
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'Stock
        SQL = " select T.item,SUM(isnull(INVMC.MC007,0)) as stock from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
              " left join INVMC on INVMC.MC001=T.item " &
              " where INVMC.MC007 >0 and INVMC.MC002 in ('" & whList.Replace(",", "','") & "') group by T.item "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set stockQty='" & .Item("stock") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'PR
        SQL = " select T.item,sum(PURTR.TR006) as pr from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
              " left join PURTB on PURTB.TB004=T.item " &
              " left join PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " &
              " left join PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " &
              " where PURTB.TB039='N' and PURTA.TA007 = 'Y' and PURTR.TR019='' and PURTB.TB011<'" & strDate & "' " &
              " group by T.item order by T.item "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set prQty='" & .Item("pr") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        SQL = " select T.item,substring(PURTB.TB011,1,6) as ym,sum(PURTR.TR006) as pr from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
              " left join PURTB on PURTB.TB004=T.item " &
              " left join PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " &
              " left join PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " &
              " where PURTB.TB039='N' and PURTA.TA007 = 'Y' and PURTR.TR019='' " & Conn_SQL.Where("PURTB.TB011", strDate, endDate) &
              " group by T.item,substring(PURTB.TB011,1,6) order by T.item,substring(PURTB.TB011,1,6) "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        lastCode = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item("item")
                If lastCode <> item Then
                    If lastCode <> "" Then
                        ExeSQL(USQL, lastCode)
                    End If
                    USQL = " update " & tempTable & " set "
                End If
                USQL = USQL & TextSQL("pr" & .Item("ym"), .Item("pr"))
                lastCode = .Item("item")
            End With
        Next
        If lastCode <> "" Then
            ExeSQL(USQL, lastCode)
        End If

        If ddlReportType.Text.Trim = "1" Then 'internal report
            'Plan PO 'po All and TD012 < '" & strDate & "'
            SQL = " select T.item,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
                  " left join PURTD on PURTD.TD004=T.item " &
                  " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
                  " where PURTD.TD016='N' and TD012 < '" & strDate & "' " &
                  " group by T.item  order by T.item "
            dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    USQL = " update " & tempTable & " set poQty='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                    dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                End With
            Next
            'PLAN PO between date select
            SQL = " select T.item,substring(PURTD.TD012,1,6) as ym,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
                  " left join PURTD on PURTD.TD004=T.item " &
                  " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
                  " where PURTD.TD016='N' " & Conn_SQL.Where("TD012", strDate, endDate) & " " &
                  " group by T.item,substring(PURTD.TD012,1,6)  order by T.item,substring(PURTD.TD012,1,6) "
            'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            lastCode = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    item = .Item("item")
                    If lastCode <> item Then
                        If lastCode <> "" Then
                            ExeSQL(USQL, lastCode)
                        End If
                        USQL = " update " & tempTable & " set "
                    End If
                    USQL = USQL & TextSQL("po" & .Item("ym"), .Item("po"))
                    lastCode = .Item("item")
                End With
            Next
            If lastCode <> "" Then
                ExeSQL(USQL, lastCode)
            End If
        Else 'export report
            'Plan PO 'po All and TD012 < '" & strDate & "'
            SQL = " select T.item,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
                  " left join PURTD on PURTD.TD004=T.item " &
                  " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
                  " where TC014 = 'Y' and TD016='N' group by T.item  order by T.item "
            'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    USQL = " update " & tempTable & " set poQty='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                    'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                    dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                End With
            Next

        End If

        'Purchase receipt inspection
        SQL = " select T.item,SUM(isnull(PURTH.TH007,0)) as po_insp from " & Conn_SQL.DBReport & ".." & tempTable & " T  " &
              " left join PURTH on PURTH.TH004=T.item " &
              " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " &
              " where PURTG.TG013 = 'N' and TH004 < '" & strDate & "' group by T.item having (SUM(isnull(PURTH.TH007, 0)) > 0) " &
              " order by T.item "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " update " & tempTable & " set poRcpQty='" & .Item("po_insp") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        SQL = " select T.item,substring(PURTH.TH014,1,6) as ym,SUM(isnull(PURTH.TH007,0)) as po_insp from " & Conn_SQL.DBReport & ".." & tempTable & " T  " &
              " left join PURTH on PURTH.TH004=T.item " &
              " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " &
              " where PURTG.TG013 = 'N' " & Conn_SQL.Where("TH014", strDate, endDate) & " group by substring(PURTH.TH014,1,6),T.item having (SUM(isnull(PURTH.TH007, 0)) > 0) " &
              " order by T.item,substring(PURTH.TH014,1,6) "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        lastCode = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                'USQL = " update " & tempTable & " set po" & .Item("ym") & "='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                item = .Item("item")
                If lastCode <> item Then
                    If lastCode <> "" Then
                        ExeSQL(USQL, lastCode)
                    End If
                    USQL = " update " & tempTable & " set "
                End If
                USQL = USQL & TextSQL("poRcp" & .Item("ym"), .Item("po_insp"))
                lastCode = .Item("item")
            End With
        Next
        If lastCode <> "" Then
            ExeSQL(USQL, lastCode)
        End If

        'Confirm PO po All and TD012 < '" & strDate & "'
        SQL = " select T.item,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
              " left join PURTD on PURTD.TD004=T.item " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' and TD014 <>'' and  replace(TD014,'-','') < '" & strDate & "' " &
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
        'Confirm PO between date select
        SQL = " select T.item,substring(replace(TD014,'-',''),1,6) as ym,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
              " left join PURTD on PURTD.TD004=T.item " &
              " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N'  and TD014 <>'' " & Conn_SQL.Where("replace(TD014,'-','')", strDate, endDate) & " " &
              " group by T.item,substring(replace(TD014,'-',''),1,6)  order by T.item,substring(replace(TD014,'-',''),1,6) "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        lastCode = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                'USQL = " update " & tempTable & " set po" & .Item("ym") & "='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                item = .Item("item")
                If lastCode <> item Then
                    If lastCode <> "" Then
                        ExeSQL(USQL, lastCode)
                    End If
                    USQL = " update " & tempTable & " set "
                End If
                USQL = USQL & TextSQL("poCon" & .Item("ym"), .Item("po"))
                lastCode = .Item("item")
            End With
        Next
        If lastCode <> "" Then
            ExeSQL(USQL, lastCode)
        End If

        'Plan Usage
        SQL = " select itemMAT as item,sum((isnull(TA006,0)*qty)+CEILING(isnull(TA006,0)*qty*scrapRatio)) as planQty from " & Conn_SQL.DBReport & ".." & tempTable2 & " T " &
              " left join LRPTA on  TA002=itemParent " &
              " left join LRPLA on TA001=LA001  and TA050=LA012 " &
              " where TA007<'" & strDate & "'  and  TA006>0  and TA051='N' and LA005='1' and LA013 = '1'  " &
              " group by itemMAT order by itemMAT "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                'USQL = " update " & tempTable & " set planQty='" & .Item("planQty") & "' where item='" & .Item("item") & "'  "
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                ExeSQL("update " & tempTable & " set planQty='" & .Item("planQty") & "' ", .Item("item"))

            End With
        Next
        SQL = " select itemMAT as item,substring(TA007,1,6) as ym,sum((isnull(TA006,0)*qty)+CEILING(isnull(TA006,0)*qty*scrapRatio)) as planQty from " & Conn_SQL.DBReport & ".." & tempTable2 & " T " &
             " left join LRPTA on  TA002=itemParent " &
             " left join LRPLA on TA001=LA001  and TA050=LA012 " &
             " where TA006>0 and TA051='N'  and LA005='1' and LA013 = '1' and TA007 between  '" & strDate & "' and '" & endDate & "'   " &
             " group by itemMAT,substring(TA007,1,6) order by itemMAT,substring(TA007,1,6) "
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        lastCode = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item("item")
                If lastCode <> item Then
                    If lastCode <> "" Then
                        ExeSQL(USQL, lastCode)
                    End If
                    USQL = " update " & tempTable & " set "
                End If
                USQL = USQL & TextSQL("plan" & .Item("ym"), .Item("planQty"))
                lastCode = .Item("item")
            End With
        Next
        If lastCode <> "" Then
            ExeSQL(USQL, lastCode)
        End If

        'Plan Issue
        SQL = " select T.item,SUM(MOCTB.TB004-MOCTB.TB005) as issue from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
              " left join MOCTB on MOCTB.TB003=T.item " &
              " left join MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
              " where MOCTB.TB015<'" & strDate & "' and MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " &
              " group by T.item "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                ExeSQL(" update " & tempTable & " set issueQty='" & .Item("issue") & "' ", .Item("item"))
            End With
        Next
        SQL = " select T.item,substring(MOCTB.TB015,1,6) as ym,SUM(MOCTB.TB004-MOCTB.TB005) as issue from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
              " left join MOCTB on MOCTB.TB003=T.item " &
              " left join MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
              " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y')" &
              "   and MOCTA.TA013='Y' and  MOCTB.TB015 between  '" & strDate & "' and '" & endDate & "' " &
              " group by T.item,substring(MOCTB.TB015,1,6) order by T.item,substring(MOCTB.TB015,1,6) "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        lastCode = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item("item")
                If lastCode <> item Then
                    If lastCode <> "" Then
                        ExeSQL(USQL, lastCode)
                    End If
                    USQL = " update " & tempTable & " set "
                End If
                USQL = USQL & TextSQL("issue" & .Item("ym"), .Item("issue"))

                lastCode = .Item("item")
            End With
        Next
        If lastCode <> "" Then
            ExeSQL(USQL, lastCode)
        End If


        Dim aa As String = ""
        Dim dtShow As Data.DataTable = New DataTable
        Dim rowSeparate As Integer = 6

        If ddlReportType.Text.Trim = "1" Then 'internal report
            dtShow.Columns.Add(New DataColumn("Msg1"))
            dtShow.Columns.Add(New DataColumn("Mat Detail"))
            dtShow.Columns.Add(New DataColumn("Msg2"))
            dtShow.Columns.Add(New DataColumn("Parent Detail"))
            dtShow.Columns.Add(New DataColumn("Msg3"))
            dtShow.Columns.Add(New DataColumn("Sum Qty"))
            dtShow.Columns.Add(New DataColumn("Before"))
            dtShow.Columns.Add(New DataColumn("Msg4"))

            For i As Integer = 0 To amtMonth
                Dim strWC As String = setColName(beginDate, i)
                dtShow.Columns.Add(New DataColumn(strWC))
            Next

            dtShow.Columns.Add(New DataColumn("Sum"))

            Dim dr1 As DataRow,
                dr2 As DataRow,
                dr3 As DataRow,
                dr4 As DataRow,
                dr5 As DataRow,
                dr6 As DataRow

            whr = ""
            Dim fldPO As String = "poQty",
                fldPOCon As String = "poConQty",
                fldPR As String = "prQty",
                fldPL As String = "planQty",
                fldIssue As String = "issueQty",
                fldPORcp As String = "poRcpQty",
                fld As String = ""
            For j As Integer = 0 To amtMonth
                Dim tdate As String = beginDate.AddMonths(j).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
                whr = whr & " or  plan" & tdate & "<>0 or po" & tdate & "<>0 or pr" & tdate & "<>0 "
                fldPO = fldPO & "+po" & tdate
                fldPOCon = fldPOCon & "+poCon" & tdate
                fldPR = fldPR & "+pr" & tdate
                fldPL = fldPL & "+plan" & tdate
                fldIssue = fldIssue & "+issue" & tdate
                fldPORcp = fldPORcp & "+poRcp" & tdate
            Next

            Dim fldDemand As String = "0",
                  fldSupply As String = "0",
                  fldConfirm As String = "0",
                  fldMonth As String = ""

            For j As Integer = 0 To amtMonth

                Dim tdate As String = beginDate.AddMonths(j).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
                whr = whr & " or  plan" & tdate & "<>0 or po" & tdate & "<>0 or pr" & tdate & "<>0 "

                fldDemand = fldDemand & "+plan" & tdate & "+issue" & tdate
                fldSupply = fldSupply & "+po" & tdate
                fldConfirm = fldConfirm & "+poCon" & tdate

            Next

            fld = "," & fldPO & " as allPo," & fldPOCon & " as allPoCon," & fldPR & " as allPR," & fldPL & " as allPL, " & fldIssue & " as allIssue , " & fldPORcp & " as allPoRcp"
            fldMonth = "," & fldDemand & " as allDemand," & fldSupply & " as allSupply," & fldConfirm & " as allConfirm "

            whr = ""
            SQL = " select *" & fld & fldMonth & " from HOOTHAI_REPORT.dbo." & tempTable & " T " &
                  " left join INVMB on MB001 = T.item " &
                  " left join PURMA on MA001 = MB032 " &
                  " where planQty <>0 or poQty<>0 or prQty <>0 or stockQty<>0 or issueQty<>0 " & whr &
                  " order by MB001 "
            'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            With dt
                For i As Integer = 0 To .Rows.Count - 1
                    With .Rows(i)
                        'get fgCode,fdDesc
                        Dim sSQL As String = " select T.itemFG,MB002,MB003,count(*) from HOOTHAI_REPORT.dbo." & tempTable3 & " T left join INVMB on MB001=T.itemFG where T.itemMAT='" & .Item("item") & "' group by T.itemFG,MB002,MB003 order by T.itemFG,MB002,MB003"
                        Dim fgCode As String = "",
                            fgDesc As String = "",
                            fgSpec As String = ""
                        Dim dt2 As DataTable
                        '= Conn_SQL.Get_DataReader(sSQL, Conn_SQL.ERP_ConnectionString)
                        dt2 = dbConn.Query(sSQL, VarIni.ERP, dbConn.WhoCalledMe)
                        With dt2
                            For j As Integer = 0 To .Rows.Count - 1
                                With .Rows(j)
                                    fgCode = fgCode & .Item("itemFG") & ","
                                    fgDesc = fgDesc & .Item("MB002") & ","
                                    fgSpec = fgSpec & .Item("MB003") & ","
                                End With
                            Next
                        End With
                        fgCode = fgCode.Substring(0, fgCode.Length - 1)
                        fgDesc = fgDesc.Substring(0, fgDesc.Length - 1)
                        fgSpec = fgSpec.Substring(0, fgSpec.Length - 1)
                        'item

                        Dim StatusItem As String = "",
                            StatusFGItem As String = ""

                        'Status NPI

                        SQL = " select * from PURTM where PURTM.TM004 in ('" & .Item("item").Replace(",", "','") & "')"
                        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                        If dt.Rows.Count = 0 Then
                            StatusItem = "NPI"
                        End If

                        SQL = " select * from PURTM where PURTM.TM004 in ('" & fgCode.Replace(",", "','") & "')"
                        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                        If dt.Rows.Count = 0 Then
                            StatusFGItem = "NPI"
                        End If

                        dr1 = dtShow.NewRow()
                        dr1("Msg1") = "Item"
                        dr1("Mat Detail") = .Item("item")
                        dr1("Msg2") = "FG Part"
                        dr1("Parent Detail") = fgCode
                        dr1("Msg3") = "Plan"
                        dr1("Sum Qty") = strFormat(.Item("allPL"))
                        dr1("Before") = strFormat(.Item("planQty"))
                        dr1("Msg4") = "Plan (Forecast)"

                        'desc
                        dr2 = dtShow.NewRow()
                        dr2("Msg1") = "Desc"
                        dr2("Mat Detail") = .Item("MB002")
                        dr2("Msg2") = "FG Desc"
                        dr2("Parent Detail") = fgDesc
                        dr2("Msg3") = "Plan Issue"
                        dr2("Sum Qty") = strFormat(.Item("allIssue"))
                        dr2("Before") = strFormat(.Item("issueQty"))
                        dr2("Msg4") = "Plan Issue"

                        dr3 = dtShow.NewRow()
                        dr3("Msg1") = "Spec"
                        dr3("Mat Detail") = .Item("MB003")
                        dr3("Msg2") = "FG Spec"
                        dr3("Parent Detail") = fgSpec
                        dr3("Msg3") = "PO"
                        dr3("Sum Qty") = strFormat(.Item("allPO"))
                        dr3("Before") = strFormat(.Item("poQty"))
                        dr3("Msg4") = "Demand"

                        dr4 = dtShow.NewRow()
                        dr4("Msg1") = "Unit"
                        dr4("Mat Detail") = .Item("MB004")
                        dr4("Msg2") = "Stock Mat"
                        dr4("Parent Detail") = .Item("stockQty")
                        dr4("Msg3") = "PR"
                        dr4("Sum Qty") = strFormat(.Item("allPR"))
                        dr4("Before") = strFormat(.Item("prQty"))
                        dr4("Msg4") = "Plan Delivery"

                        dr5 = dtShow.NewRow()
                        dr5("Msg1") = "Main Supp."
                        dr5("Mat Detail") = .Item("MB032").ToString.Trim & "-" & .Item("MA002").ToString.Trim
                        dr5("Msg2") = "Fix Lead Time"
                        dr5("Parent Detail") = .Item("MB036")
                        dr5("Msg3") = "Inspection Qty"
                        dr5("Sum Qty") = strFormat(.Item("allPoRcp"))
                        dr5("Before") = strFormat(.Item("poRcpQty"))
                        dr5("Msg4") = "Confirm Delivery"

                        dr6 = dtShow.NewRow()
                        dr6("Msg1") = "Status"
                        dr6("Mat Detail") = StatusItem
                        dr6("Msg2") = "FG Status"
                        dr6("Parent Detail") = StatusFGItem
                        dr6("Msg3") = ""
                        dr6("Sum Qty") = ""
                        dr6("Before") = ""
                        dr6("Msg4") = "Shortage"

                        Dim bal As Decimal = .Item("stockQty") + .Item("poQty") - .Item("planQty") - .Item("issueQty")
                        For j As Integer = 0 To amtMonth
                            Dim tdate As String = beginDate.AddMonths(j).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
                            Dim colName As String = setColName(beginDate, j)
                            Dim Demand As Decimal = .Item("plan" & tdate) + .Item("issue" & tdate)
                            Dim Supply As Decimal = .Item("po" & tdate) ' + .Item("pr" & tdate)
                            bal = bal - Demand + Supply

                            dr1(colName) = strFormat(.Item("plan" & tdate))
                            dr2(colName) = strFormat(.Item("issue" & tdate))
                            dr3(colName) = strFormat(Demand)
                            dr4(colName) = strFormat(Supply)
                            dr5(colName) = strFormat(.Item("poCon" & tdate))
                            dr6(colName) = strFormat(bal)

                        Next

                        dr1("Sum") = strFormat(.Item("allPL"))
                        dr2("Sum") = strFormat(.Item("allIssue"))
                        dr3("Sum") = strFormat(.Item("allDemand"))
                        dr4("Sum") = strFormat(.Item("allSupply"))
                        dr5("Sum") = strFormat(.Item("allConfirm"))
                        dr6("Sum") = ""

                        dtShow.Rows.Add(dr1)
                        dtShow.Rows.Add(dr2)
                        dtShow.Rows.Add(dr3)
                        dtShow.Rows.Add(dr4)
                        dtShow.Rows.Add(dr5)
                        dtShow.Rows.Add(dr6)
                        bal = 0
                    End With
                Next

            End With
        Else 'external report
            rowSeparate = 2
            dtShow.Columns.Add(New DataColumn("No"))
            dtShow.Columns.Add(New DataColumn("Item"))
            dtShow.Columns.Add(New DataColumn("Desc"))
            dtShow.Columns.Add(New DataColumn("Spec"))
            dtShow.Columns.Add(New DataColumn("Stock/PO"))
            dtShow.Columns.Add(New DataColumn("Name"))
            dtShow.Columns.Add(New DataColumn("Before"))
            For i As Integer = 0 To amtMonth
                ' Dim tdate As String = beginDate.AddMonths(i).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
                Dim strWC As String = setColName(beginDate, i)
                dtShow.Columns.Add(New DataColumn(strWC))
            Next

            dtShow.Columns.Add(New DataColumn("Sum"))
            Dim dr1 As DataRow,
                dr2 As DataRow

            'Dim whr As String = ""
            whr = ""
            Dim fldPO As String = "poQty",
                fldPOCon As String = "poConQty",
                fldPR As String = "prQty",
                fldPL As String = "planQty",
                fldIssue As String = "issueQty",
                fldPORcp As String = "poRcpQty",
                fld As String = ""
            For j As Integer = 0 To amtMonth
                Dim tdate As String = beginDate.AddMonths(j).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
                whr = whr & " or  plan" & tdate & "<>0 or po" & tdate & "<>0 or pr" & tdate & "<>0 "
                fldPO = fldPO & "+po" & tdate
                fldPOCon = fldPOCon & "+poCon" & tdate
                fldPR = fldPR & "+pr" & tdate
                fldPL = fldPL & "+plan" & tdate
                fldIssue = fldIssue & "+issue" & tdate
                fldPORcp = fldPORcp & "+poRcp" & tdate
            Next

            Dim fldDemand As String = "0",
                  fldSupply As String = "0",
                  fldConfirm As String = "0",
                  fldMonth As String = ""

            For j As Integer = 0 To amtMonth

                Dim tdate As String = beginDate.AddMonths(j).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
                whr = whr & " or  plan" & tdate & "<>0 or po" & tdate & "<>0 or pr" & tdate & "<>0 "
                fldDemand = fldDemand & "+plan" & tdate & "+issue" & tdate
                fldSupply = fldSupply & "+po" & tdate
                fldConfirm = fldConfirm & "+poCon" & tdate

            Next

            fld = "," & fldPO & " as allPo," & fldPOCon & " as allPoCon," & fldPR & " as allPR," & fldPL & " as allPL, " & fldIssue & " as allIssue , " & fldPORcp & " as allPoRcp"
            fldMonth = "," & fldDemand & " as allDemand," & fldSupply & " as allSupply," & fldConfirm & " as allConfirm "

            whr = ""
            SQL = " select *" & fld & fldMonth & " from " & Conn_SQL.DBReport & ".." & tempTable & " T " &
                  " left join INVMB on MB001 = T.item " &
                  " left join PURMA on MA001 = MB032 " &
                  " where planQty <>0 or poQty<>0 or prQty <>0 or stockQty<>0 or issueQty<>0 " & whr &
                  " order by MB001 "
            'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            Dim line As Integer = 1
            With dt
                For i As Integer = 0 To .Rows.Count - 1
                    With .Rows(i)
                        Dim allDemand As Decimal = 0,
                            allBal As Decimal = 0
                        Dim bal As Decimal = .Item("stockQty") + .Item("poQty")
                        Dim Demand As Decimal = .Item("planQty") + .Item("issueQty")
                        allDemand += Demand

                        dr1 = dtShow.NewRow()
                        dr1("No") = line
                        dr1("Item") = .Item("MB001")
                        dr1("Desc") = .Item("MB002")
                        dr1("Spec") = .Item("MB003")
                        dr1("Stock/PO") = strFormat(.Item("stockQty"))
                        dr1("Name") = "Demand"
                        dr1("Before") = strFormat(If(Demand < 0, 0, Demand))

                        'desc
                        bal -= Demand
                        allBal = If(bal > 0, 0, bal)

                        dr2 = dtShow.NewRow()
                        dr2("No") = line
                        dr2("Item") = .Item("MB001")
                        dr2("Desc") = .Item("MB002")
                        dr2("Spec") = .Item("MB003")
                        dr2("Stock/PO") = strFormat(.Item("poQty"))
                        dr2("Name") = "Shortage"
                        dr2("Before") = strFormat(If(bal > 0, 0, bal))
                        bal = If(bal < 0, 0, bal)
                        For j As Integer = 0 To amtMonth
                            Dim tdate As String = beginDate.AddMonths(j).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
                            Dim colName As String = setColName(beginDate, j)
                            Demand = .Item("plan" & tdate) + .Item("issue" & tdate) - .Item("po" & tdate)
                            allDemand += Demand

                            bal -= Demand
                            allBal += If(bal > 0, 0, bal)
                            dr1(colName) = strFormat(If(Demand < 0, 0, Demand))
                            dr2(colName) = strFormat(If(bal > 0, 0, bal))
                            bal = If(bal < 0, 0, bal)
                        Next

                        dr1("Sum") = strFormat(If(allDemand < 0, 0, allDemand))
                        dr2("Sum") = strFormat(If(allBal > 0, 0, allBal))

                        dtShow.Rows.Add(dr1)
                        dtShow.Rows.Add(dr2)
                        bal = 0
                        line += 1

                    End With
                Next

            End With

        End If

        gvShow.DataSource = dtShow
        gvShow.DataBind()
        lbCount.Text = gvShow.Rows.Count / rowSeparate
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
        If ddlReportType.Text.Trim = "2" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End If
    End Sub

    'Sub showForOut()

    '    Dim date1 As String = tbDateFrom.Text,
    '       date2 As String = tbDateTo.Text,
    '       strDate As String = "",
    '       endDate As String = ""
    '    'Begin date
    '    If date1 <> "" Then
    '        strDate = configDate.dateFormat2(date1)
    '    Else
    '        strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
    '    End If
    '    'strDate = strDate & "01"
    '    'End date
    '    If date2 <> "" Then
    '        endDate = configDate.dateFormat2(date2)
    '    Else
    '        endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
    '    End If

    '    Dim tempTable As String = "tempMatForcastOut" & Session("UserName")
    '    CreateTempTable.createTempMatForcastOut(tempTable)

    '    Dim SQL As String,
    '        WHR As String = ""

    '    If tbCust.Text.Trim <> "" Then
    '        WHR &= " and TC004 in('" & tbCust.Text.Trim.Replace(",", "','") & "')"
    '    End If
    '    'WHR &= Conn_SQL.Where("",)

    '    'MO=1
    '    SQL = " select * from SFCTA " &
    '          " left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " &
    '          " left join COPTC on TC001=MOCTA.TA026 and TC002=MOCTA.TA027 " &
    '          " where MOCTA.TA011 in ('1','2','3') and SFCTA.TA005 = '2' " &
    '          " and MOCTA.TA015+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 >0 "




    '    'SO=2



    '    'Forcast=3




    'End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        If tcShow.ActiveTabIndex = 0 Then
            showForMat()
        Else
            'showForOut()
        End If
    End Sub

    Function TextSQL(fldName As String, Val As Decimal) As String
        Return fldName & "='" & Val & "',"
    End Function

    Sub ExeSQL(SQL As String, Code As String)
        Dim USQL As String = SQL.Substring(0, SQL.Length - 1) & " where item='" & Code & "' "
        dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Function setColName(fdate As Date, i As Integer) As String
        Return fdate.AddMonths(i).ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture)
    End Function

    Function chkCode(code As String) As Boolean

        Dim C1type As String = code.Trim.Substring(2, 1)
        '1 Mat
        '2 FG
        '3 Subpart
        '4 Sparpart

        Dim res As Boolean = False
        If ddlTypeMat.Text = "0" Then 'all
            If C1type <> "2" Then
                res = True
            End If
        ElseIf ddlTypeMat.Text = "1" Then 'met
            If C1type = "1" Then
                res = True
            End If
        Else
            If C1type = "4" Then 'sp & other
                res = True
            End If
        End If
        Return res
    End Function

    Protected Sub CodeBOM(tempTable As String, tempTable2 As String, tempTable3 As String, code As String, Optional codeParent As String = "")
        If codeParent = "" Then
            codeParent = code
        End If
        Dim whr As String = ""
        If tbMatDesc.Text <> "" Or tbMatItem.Text <> "" Or tbMatSpec.Text <> "" Then
            whr &= Conn_SQL.Where("MB001", tbMatItem)
            whr &= Conn_SQL.Where("MB002", tbMatDesc)
            whr &= Conn_SQL.Where("MB003", tbMatSpec)
        End If

        If whr <> "" Then
            whr &= " or (MD001='" & code & "'  " & "and  MB025='M') "
        End If

        Dim SQL As String = ""
        SQL = " select MD003,MD006,MB025,MD008 from BOMMD " &
              " left join INVMB on MB001=MD003 " &
              " where MB109='Y' and MD001='" & code & "'  " & whr &
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
                    CodeBOM(tempTable, tempTable2, tempTable3, item, codeParent)
                ElseIf chkCode(item) Then
                    Dim fldInsHash As Hashtable = New Hashtable,
                        whrHash As Hashtable = New Hashtable,
                        fldUpdHash As Hashtable = New Hashtable
                    'whr of condition
                    Dim matCode As String = .Item("MD003").ToString.Trim
                    whrHash.Add("item", matCode)
                    fldInsHash.Add("moQty", "0") ' fg item
                    fldUpdHash.Add("moQty", "'0'") ' fg item
                    'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                    dbConn.TransactionSQL(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                    Dim USQL As String = ""
                    'BOMMAT
                    USQL = " if not exists(select * from " & tempTable2 & " where itemParent='" & code & "' and itemMAT='" & matCode & "' ) " &
                                         " insert into " & tempTable2 & "(itemParent,itemMAT,qty,scrapRatio)values ('" & code & "','" & matCode & "','" & qty & "','" & ScrapRatio & "')"
                    'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                    dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                    'BOMShow
                    USQL = " if not exists(select * from " & tempTable3 & " where itemFG='" & codeParent & "' and itemParent='" & code & "' and itemMAT='" & matCode & "' ) " &
                                         " insert into " & tempTable3 & "(itemFG,itemParent,itemMAT,qty)values ('" & codeParent & "','" & code & "','" & matCode & "','0')"
                    'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                    dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)

                End If
            End With
        Next
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                If .Cells(0).Text.Trim = "Item" Then

                    Dim Item As String = .Cells(1).Text.Trim
                    For i As Decimal = 1 To 1
                        With .Cells(i)
                            '.ForeColor = Drawing.Color.Red
                            .Font.Bold = True
                            .Font.Underline = True
                            .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                            .Attributes.Add("onclick", "NewWindow('MatForcatPopup.aspx?Item=" & Item & "','_blank',800,500,'yes')")
                        End With
                    Next

                End If

                If .Cells(2).Text.Trim = "FG Part" Then
                    Dim Item As String = .Cells(3).Text.Trim
                    For i As Decimal = 3 To 3
                        With .Cells(i)
                            .Font.Bold = True
                            .Font.Underline = True
                            .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                            .Attributes.Add("onclick", "NewWindow('MatForcatPopup.aspx?Item=" & Item & "','_blank',800,500,'yes')")
                        End With
                    Next

                End If

            End If
        End With
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("MatForcast" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Function strFormat(ByVal val As Object) As String
        Dim show As String = ""
        If CDec(val) <> 0 Then
            show = String.Format("{0:n2}", val)
        End If
        Return show
    End Function

End Class