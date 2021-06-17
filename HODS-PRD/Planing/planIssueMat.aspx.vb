Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class planIssueMat
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    'Dim ControlForm As New ControlDataForm
    'Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Dim dbConn As New DataConnectControl
    Dim ddlCont As New DropDownListControl
    Dim cblcont As New CheckBoxListControl
    Dim gvCont As New GridviewControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("UserName") = "" Then
            '    Response.Redirect("../Login.aspx")
            'End If
            If Session(VarIni.UserName) <> "" Then
                Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
                ddlCont.showDDL(ddlSaleType, SQL, VarIni.ERP, "MQ002", "MQ001", True)

                SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='51' order by MQ002"
                ddlCont.showDDL(ddlTypeMO, SQL, VarIni.ERP, "MQ002", "MQ001", True)

                SQL = "select MC001,MC001+' '+MC002 MC002 from CMSMC where CMSMC.UDF01='Y' order by MC001"
                cblcont.showCheckboxList(CblWh, SQL, VarIni.ERP, "MC002", "MC001", 5)
                btExportGrid.Visible = False
            End If
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempPlanIssue" & Session("UserName")
        CreateTempTable.createTempPlanIssue(tempTable)
        Dim whr As String = "",
            SQL As String = "",
            USQL As String = "",
            ISQL As String = "",
            whr2 As String = "",
            code As String = tbCode.Text,
            spec As String = tbSpec.Text,
            TypeMO As String = ddlTypeMO.Text,
            MoFrom As String = tbMoFrom.Text,
            MoTo As String = tbMoTo.Text,
            TypeSale As String = ddlSaleType.Text,
            SaleNumber As String = tbSoNo.Text,
            SaleSeq As String = tbSoSeq.Text,
            dateFrom As String = ucDateFrom.text,
            dateTo As String = ucDateTo.text,
            cust As String = tbCust.Text.Trim,
            item As String = "",
            Qty As Integer = 0,
            Program As New DataTable

        'MO Plan Mat Issue
        whr &= dbConn.WHERE_EQUAL("MOCTA.TA026", ddlSaleType)
        'If TypeSale <> "" And TypeSale <> "ALL" Then
        '    whr &= " and MOCTA.TA026='" & TypeSale & "' "
        'End If
        whr &= dbConn.WHERE_LIKE("MOCTA.TA027", tbSoNo)
        'If SaleNumber <> "" Then
        '    whr &= Conn_SQL.SetConditionWhr("MOCTA.TA027", SaleNumber, "")
        'End If
        whr &= dbConn.WHERE_LIKE("MOCTA.TA028", tbSoSeq)
        'If SaleSeq <> "" Then
        '    whr &= Conn_SQL.Where("MOCTA.TA028", tbSoSeq)
        'End If
        whr &= dbConn.WHERE_EQUAL("MOCTB.TB001", ddlTypeMO)

        'If TypeMO <> "" And TypeMO <> "ALL" Then
        '    whr &= " and MOCTB.TB001='" & TypeMO & "' "
        'End If
        whr &= dbConn.WHERE_BETWEEN("MOCTB.TB002", MoFrom, MoTo)

        'If MoFrom <> "" Or MoTo <> "" Then
        '    'whr = whr & " and MOCTB.TB002='" & MoFrom & "' "
        '    whr &= Conn_SQL.SetConditionWhr("MOCTB.TB002", MoFrom, MoTo)
        'End If

        whr &= dbConn.WHERE_LIKE("MOCTB.TB003", tbCode)
        whr &= dbConn.WHERE_OR("MOCTB.TB013", "MOCTB.TB012", tbSpec)
        whr &= dbConn.WHERE_LIKE("COPTC.TC004", tbCust)
        'whr &= Conn_SQL.Where("substring(MOCTB.TB003,3,1)", cblCodeType, "0") 'code type
        whr &= dbConn.WHERE_IN("substring(MOCTB.TB003,3,1)", cblCodeType, "0") 'code type

        'Dim fldDate As String = "MOCTB.TB015" 'mo issue date=2
        'If ddlDate.Text.Trim = "1" Then 'MO Date=1
        '    fldDate = "MOCTA.TA003"
        'End If
        Dim fldDate As String = ""
        Select Case ddlDate.Text.Trim
            Case "1" 'MO Date
                fldDate = "MOCTA.TA003"
            Case "2" 'mo Plan issue date
                fldDate = "MOCTB.TB015"
            Case "3" 'MO Plan start date
                fldDate = "MOCTA.TA009"
        End Select

        whr &= dbConn.WHERE_BETWEEN(fldDate, dateFrom, dateTo)

        SQL = " select MOCTB.TB003,SUM(MOCTB.TB004-MOCTB.TB005) from  " & VarIni.ERP & "..MOCTB MOCTB " &
            " left join " & VarIni.ERP & "..MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
            " left join " & VarIni.ERP & "..COPTC COPTC on COPTC.TC001= MOCTA.TA026 and COPTC.TC002= MOCTA.TA027 " &
            " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & whr &
            " group by MOCTB.TB003 "

        'SQL = " select COPTD.TD004,SUM(COPTD.TD008-COPTD.TD009)  from " & VarIni.ERP & "..COPTD COPTD " & _
        '     " left join " & VarIni.ERP & "..COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
        '     " where COPTD.TD016='N' " & whr & " group by COPTD.TD004  order by COPTD.TD004"
        ISQL = "insert into " & tempTable & "(item,issueQty) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        'MO Plan Mat Issue when select mo type
        If (TypeMO <> "" And TypeMO <> "0") Or MoFrom <> "" Or MoTo <> "" Or
            (TypeSale <> "" And TypeSale <> "0") Or SaleNumber <> "" Or SaleSeq <> "" Or cust <> "" Then
            whr = ""
            'MO
            If (TypeMO <> "" And TypeMO <> "0") And MoFrom <> "" And MoTo <> "" Then
                whr = whr & " and (TB001 not in ('" & TypeMO & "') or TB002 not between'" & MoFrom & "' and '" & MoTo & "' )"
            ElseIf (TypeMO <> "" And TypeMO <> "0") And MoFrom = "" And MoTo = "" Then
                whr = whr & " and TB001 not in ('" & TypeMO & "') "
            ElseIf MoFrom = "" Or MoTo = "" Then
                Dim valx As String = MoFrom
                If MoFrom = "" Then
                    valx = MoTo
                End If
                whr = whr & " and B.TB002 not in ('" & valx & "') "
            End If

            'sale order 
            If (TypeSale <> "" And TypeSale <> "0") Then
                whr = whr & " and A.TA026 <> '" & TypeSale & "' "
            End If

            If SaleNumber <> "" Then
                whr = whr & " and A.TA027 <>'" & SaleNumber & "' "
            End If

            If SaleSeq <> "" Then
                whr = whr & " and A.TA028 <> ('" & SaleSeq & "') "
            End If

            'If (TypeSale <> "" And TypeSale <> "ALL") And SaleNumber <> "" And SaleSeq <> "" Then
            '    whr = whr & " and (A.TA026 not in ('" & TypeSale & "') or A.TA027 <>'" & SaleNumber & "' or  A.TA028<>'" & SaleSeq & "' )"
            'ElseIf (TypeSale <> "" And TypeSale <> "ALL") And SaleNumber = "" And SaleSeq = "" Then
            '    whr = whr & " and A.TA026 not in ('" & TypeSale & "') "
            'Else
            '    'Dim valx As String = SaleFrom
            '    'If SaleFrom = "" Then
            '    '    valx = SaleTo
            '    'End If
            '    whr = whr & " and A.TA028 not in ('" & SaleSeq & "') "
            'End If

            'customer
            If cust <> "" Then
                whr &= " and C.TC004 not like '%" & cust & "%' "
            End If

            'item
            whr &= Conn_SQL.Where("B.TB003", tbCode)
            'spec
            whr &= Conn_SQL.Where("B.TB013", tbSpec)
            'date
            fldDate = "B.TB013" 'mat issue date=2
            If ddlDate.Text.Trim = "1" Then 'MO Date=1
                fldDate = "A.TA003"
            End If
            whr &= Conn_SQL.Where(fldDate, dateFrom, dateTo, True)
            whr &= Conn_SQL.Where("substring(B.TB003,3,1)", cblCodeType, "0") 'code type
            'If dateFrom <> "" Or dateTo <> "" Then
            '    Dim fldHead As String = "A.TA003"
            '    If ddlDate.Text.Trim = "2" Then
            '        fldHead = "A.TA009"
            '    End If
            '    whr = whr & Conn_SQL.Where(fldHead, dateFrom, dateTo, True)
            'End If
            SQL = " select B.TB003 as item,SUM(B.TB004-B.TB005) as issueQty from " & VarIni.DBMIS & ".." & tempTable & " T  " &
                  " left join " & VarIni.ERP & "..MOCTB B on B.TB003=T.item " &
                  " left join " & VarIni.ERP & "..MOCTA A on A.TA001=B.TB001 and A.TA002=B.TB002 " &
                  " left join " & VarIni.ERP & "..COPTC C on C.TC001= A.TA026 and C.TC002= A.TA027" &
                  " where B.TB004-B.TB005>0 and A.TA011 not in('y','Y') and A.TA013='Y' " & whr &
                  " group by B.TB003 "

            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                item = Program.Rows(i).Item("item")
                Qty = Program.Rows(i).Item("issueQty")
                USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                       " update " & tempTable & " set issueQty=issueQty+'" & Qty & "' where item='" & item & "' else " &
                       " insert into " & tempTable & "(item,issueQty)values ('" & item & "','" & Qty & "')"
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            Next
        End If
        'MO
        whr = Conn_SQL.Where("A.TA006", tbCode)
        whr = whr & Conn_SQL.Where("A.TA035", tbSpec)

        'If (TypeMO <> "" And TypeMO <> "ALL") Or MO <> "" Then
        'and SUBSTRING(A.TA006,3,1) = '3'
        SQL = " select A.TA006 as item, SUM(isnull(A.TA015,0)-isnull(A.TA017,0)) as mo from " & VarIni.DBMIS & ".." & tempTable & " T " &
              " left join " & VarIni.ERP & "..MOCTA A on A.TA006=T.item where A.TA011 not in('y','Y') and A.TA013 ='Y'  " & whr &
              " group by TA006  order by TA006"
        'Else
        '    SQL = "select A.TA006 as item, SUM(isnull(A.TA015,0)-isnull(A.TA017,0)) as mo from MOCTA A where A.TA011 not in('y','Y') and A.TA013 ='Y' and SUBSTRING(A.TA006,3,1) = '3' " & whr & " group by TA006  order by A.TA006"
        'End If
        'SQL = "select TA006 as item, SUM(isnull(TA015,0)-isnull(TA017,0)) as mo from MOCTA where TA011 not in('y','Y') and SUBSTRING(TA006,3,1) = '3' " & whr & " group by TA006  order by TA006"
        ' Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("mo")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'PO
        whr = Conn_SQL.Where("D.TD004", tbCode)
        whr = whr & Conn_SQL.Where("D.TD005", "D.TD006", tbSpec)

        'If (TypeMO <> "" And TypeMO <> "ALL") Or MO <> "" Then
        SQL = " select D.TD004 as item,SUM(isnull(D.TD008,0)-isnull(D.TD015,0)) as po from  " & VarIni.DBMIS & ".." & tempTable & " T " &
              " left join " & VarIni.ERP & "..PURTD D on D.TD004=T.item where D.TD016='N' and SUBSTRING(D.TD004,3,1) not in ('2')   " & whr &
              " group by D.TD004  order by D.TD004 "
        'Else
        'SQL = " select D.TD004 as item,SUM(isnull(D.TD008,0)-isnull(D.TD015,0)) as po from PURTD D where D.TD016='N' and SUBSTRING(D.TD004,3,1) not in ('2')   " & whr & " group by D.TD004  order by D.TD004 "
        'End If

        ' Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Purchase receipt inspection
        'whr = Conn_SQL.Where("substring(PURTH.TH004,3,1)", ddlCodeType, "0") 'code type
        whr = Conn_SQL.Where("PURTH.TH004", tbCode) 'Item
        'whr = whr & Conn_SQL.Where("PURTH.TH005", tbDesc) 'Desc
        whr = whr & Conn_SQL.Where("PURTH.TH005", "PURTH.TH006", tbSpec) 'Spec
        'whr = whr & Conn_SQL.Where("PURTG.TG005", tbSup) 'Supplier

        SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH007,0)) as po_insp from " & VarIni.DBMIS & ".." & tempTable & " T " &
              " left join " & VarIni.ERP & "..PURTH on PURTH.TH004=T.item " &
              " left join " & VarIni.ERP & "..PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " &
              " where PURTG.TG013 = 'N' " & whr & " group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) " &
              " order by PURTH.TH004 "

        'SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH007,0)) as po_insp from PURTH " & _
        '      " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " & _
        '      " where PURTG.TG013 = 'N' " & whr & " group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) " & _
        '      " order by PURTH.TH004 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'PR
        whr = Conn_SQL.Where("B.TB004", tbCode)
        whr = whr & Conn_SQL.Where("B.TB005", "B.TB006", tbSpec)

        'If (TypeMO <> "" And TypeMO <> "ALL") Or MO <> "" Then
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from " & VarIni.DBMIS & ".." & tempTable & " T " &
               " left join " & VarIni.ERP & "..PURTB B on B.TB004=T.item " &
               " left join " & VarIni.ERP & "..PURTR R on R.TR001=B.TB001 and R.TR002=B.TB002 and R.TR003=B.TB003 " &
              " where R.TR019='' and B.TB039='N'  and SUBSTRING(B.TB004,3,1) not in ('2') " & whr & " group by B.TB004 "
        'Else
        'SQL = " select B.TB004 as item,sum(R.TR006) as pr from PURTR R left join PURTB B on B.TB001=R.TR001 and B.TB002=R.TR002 and TB003=TR003 where R.TR019='' and B.TB039='N'  and SUBSTRING(B.TB004,3,1) not in ('2') " & whr & " group by B.TB004 "
        'End If
        ' Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'inventory stock 
        whr = ""
        whr &= dbConn.WHERE_LIKE("INVMC.MC001", tbCode)
        whr &= Conn_SQL.Where("INVMB.MB002", "INVMB.MB003", tbSpec)
        whr &= dbConn.WHERE_IN("INVMC.MC002", CblWh,, True)

        SQL = " select INVMC.MC001 as item,sum(INVMC.MC007) as stockQty from " & VarIni.DBMIS & ".." & tempTable & " T " &
              " left join " & VarIni.ERP & "..INVMC INVMC on INVMC.MC001=T.item " &
              " left join " & VarIni.ERP & "..INVMB INVMB on INVMB.MB001=INVMC.MC001 " &
              " where 1=1 " & whr & " group by INVMC.MC001 "

        'SQL = " select B.MB001 as item,L.ML005 as supportQty from INVML L left join INVMB B on B.MB001=L.ML001 " & _
        '      " where L.ML002='2400' " & whr & " order by B.MB001 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("stockQty")
            USQL = " if exists(select * from " & tempTable & " where  item='" & item & "' ) " &
                   " update " & tempTable & " set stockQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,stockQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Show Data
        'Dim strBal As String = " case when INVMC.MC002 is null then 0 else INVMC.MC007 end " '+T.moQty+T.poQty+T.prQty+T.supportQty
        Dim strBal As String = " T.stockQty+T.supportQty " '+T.moQty+T.poQty+T.prQty
        Dim strReq As String = " T.issueQty " '+T.moQty+T.poQty+T.prQty+T.supportQty
        Dim valConsel As String = ddlCondition.SelectedValue.ToString
        whr = ""
        If valConsel <> "0" Then
            whr = " where "
            If valConsel = "1" Then  'Planning OK == Stock >= issue request and issue request>0
                whr = whr & strBal & ">=" & strReq & " and " & strReq & ">0 "
            ElseIf valConsel = "2" Then 'issue request Over Stock and issue request>0
                whr = whr & strReq & " >" & strBal & " and " & strReq & ">0 "
            ElseIf valConsel = "3" Then  ' issue request=0,Stock>0
                whr = whr & strReq & "=0 and " & strBal & ">0 "
            ElseIf valConsel = "4" Then  'Stock=0,issue request>0
                whr = whr & strBal & "=0 and " & strReq & ">0 "
            ElseIf valConsel = "5" Then  ' Stock < issue request and issue request>0
                whr = whr & strBal & "<" & strReq & " and " & strReq & ">0 "
            End If
        End If ',T.prQty as PR


        SQL = "(select TA002,sum(TA006) TA006 from  " & VarIni.ERP & "..LRPTA  left join  " & VarIni.ERP & "..LRPLA on LA001=TA001  and LA012=TA050 where TA029='1' and TA051 = 'N' and LA005='1' and LA013 = '1' and TA006>0 group by TA002) LRPTA"

        Dim al As New ArrayList
        Dim colName As ArrayList
        Dim fldName As ArrayList
        With New ArrayListControl(al)
            .TAL("case when len(item)=16 then (SUBSTRING(item,1,14)+'-'+SUBSTRING(item,15,2)) else item end" & VarIni.C8 & "item", "Item")
            .TAL("INVMB.MB002+'-'+ INVMB.MB003" & VarIni.C8 & "MB0023", "SPEC")
            .TAL("issueQty", "Mat Request", "3")
            .TAL("stockQty", "Stock", "3")
            .TAL("moQty", "MO", "0")
            .TAL("TA006", "PLAN MO", "0")
            .TAL("poQty", "Purchase Order", "3")
            .TAL("poRcpQty", "Purchar Receipt Inspection", "3")
            .TAL("prQty", "Purchar Request", "3")
            colName = .ChangeFormat(True)
            fldName = .ChangeFormat()
        End With

        Dim strSQL As New SQLString(tempTable, fldName)
        strSQL.setLeftjoin(" left join " & VarIni.ERP & "..INVMB on INVMB.MB001=item ")
        strSQL.setLeftjoin(" left join " & SQL & " on TA002=item ")
        strSQL.SetOrderBy("item")

        gvCont.GridviewColWithLinkFirst(gvShow, colName, True, strSplit:=VarIni.C8)
        gvCont.ShowGridView(gvShow, strSQL.GetSQLString, VarIni.DBMIS)
        ucCountRow.RowCount = gvShow.Rows.Count
        btExportGrid.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplDetail"), HyperLink)
            If Not IsNothing(hplDetail) And Not IsDBNull(e.Row.DataItem("item")) Then
                Dim link As String = ""
                link = link & "&JPPart= " & e.Row.DataItem("item").ToString.Replace("-", "")
                link = link & "&JPSpec= " & Server.UrlEncode(e.Row.DataItem("MB0023"))
                link = link & "&stock= " & e.Row.DataItem("stockQty")
                link = link & "&issueQty= " & e.Row.DataItem("issueQty")
                link = link & "&poQty= " & e.Row.DataItem("poQty")
                link = link & "&prQty= " & e.Row.DataItem("poRcpQty")
                link = link & "&moQty= " & e.Row.DataItem("moQty")
                hplDetail.NavigateUrl = "planIssueMatPopup.aspx?height=150&width=350" & link
                hplDetail.Attributes.Add("title", e.Row.DataItem("item"))
            End If
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
    Protected Sub btExportGrid_Click(sender As Object, e As EventArgs) Handles btExportGrid.Click
        Dim expCont As New ExportImportControl
        expCont.Export("DailyMchPlan" & Session("UserName"), gvShow)
    End Sub


End Class