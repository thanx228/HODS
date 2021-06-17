Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class MaterialListNew
    Inherits System.Web.UI.Page
    'Dim ControlForm As New ControlDataForm
    'Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    'Dim configDate As New ConfigDate

    Dim dbConn As New DataConnectControl
    Dim dateCont As New DateControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim ddlCont As New DropDownListControl
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ddlCont.showDDL(ddlSaleType, SQL, VarIni.ERP, "MQ002", "MQ001", False)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ddlCont.showDDL(ddlWorkType, SQL, VarIni.ERP, "MQ002", "MQ001", False)
            btExport.Visible = False
            tbOrdQty.Text = 1
            TabContainer1.ActiveTabIndex = 0
        End If
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Select Case TabContainer1.ActiveTabIndex
            Case 0 'item
                Item()
            Case 1 'sale order
                SaleOrder()
            Case 2 'mo
                ManufactureOrder()
            Case 3 'customer
                Customer()
        End Select
    End Sub

    Sub Item()
        Dim item As String,
            WHR As String,
            SQL As String,
            orderQty As Decimal = CDec(tbOrdQty.Text.Trim)

        item = tbItem.Text.Trim
        Dim spec As String = tbSpec.Text.Trim
        If tbItem.Text.Trim = "" And tbSpec.Text.Trim = "" And tbDesc.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Item ,Desc and Spec is null !!!", UpdatePanel1)
            tbItem.Focus()
            Exit Sub
        End If

        WHR = dbConn.WHERE_LIKE("MB001", tbItem)
        WHR &= dbConn.WHERE_LIKE("MB002", tbDesc)
        WHR &= dbConn.WHERE_LIKE("MB003", tbSpec)
        If orderQty = 0 Then
            orderQty = 1
        End If
        SQL = " select MB001 parentItem," & orderQty & " orderQty from INVMB where MB109 = 'Y' and MB025='M' " & WHR
        genBOM(SQL, "")
    End Sub

    Sub SaleOrder()
        Dim saleType As String = ddlSaleType.Text.Trim,
            saleNo As String = tbSaleNo.Text.Trim,
            WHR As String,
            SQL As String
        If ddlSaleType.Text.Trim = "ALL" And
            tbSaleNo.Text.Trim = "" And
            tbSaleCust.Text.Trim = "" And
            tbDateFm.Text.Trim = "" And
            tbDateTo.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Please input data for search!!!", UpdatePanel1)
            tbSaleNo.Focus()
            Exit Sub
        End If
        'docNo = saleType & "-" & saleNo

        WHR = dbConn.WHERE_EQUAL("TD001", ddlSaleType)
        WHR &= dbConn.WHERE_LIKE("TD002", tbSaleNo)
        WHR &= dbConn.WHERE_LIKE("TD003", tbSaleSeq)
        WHR &= dbConn.WHERE_LIKE("TC004", tbCust)
        WHR &= dbConn.WHERE_IN("TD016", ddlSaleStatus)
        WHR &= dbConn.Where("TC039", dateCont.dateFormat2(tbDateFm.Text.Trim), dateCont.dateFormat2(tbDateTo.Text.Trim))
        SQL = "select TD004 parentItem,sum(TD008) orderQty from COPTD left join COPTC on TC001=TD001 and TC002=TD002 where 1=1 " & WHR & " group by TD004"
        genBOM(SQL, saleType & "-" & saleNo)
    End Sub

    Sub ManufactureOrder()
        Dim workType As String = ddlWorkType.Text.Trim,
           workNo As String = tbWorkNo.Text.Trim,
           WHR As String,
           SQL As String
        If tbWorkCust.Text.Trim = "" And ddlWorkType.Text.Trim = "ALL" And tbWorkNo.Text.Trim = "" And tbWorkDateTo.Text = "" And tbWorkDateFM.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Please input data for search!!!", UpdatePanel1)
            tbWorkNo.Focus()
            Exit Sub
        End If
        WHR = dbConn.WHERE_EQUAL("TA001", ddlWorkType)
        WHR &= dbConn.WHERE_LIKE("TA002", tbWorkNo)
        WHR &= dbConn.WHERE_LIKE("TC004", tbWorkCust)
        WHR &= dbConn.Where("TC039", dateCont.dateFormat2(tbDateFm.Text.Trim), dateCont.dateFormat2(tbDateTo.Text.Trim))
        SQL = "select TA006 parentItem,sum(TA015) orderQty from MOCTA left join MOCTC on TC001=TA026 and TC002=TA027 where 1=1 " & WHR & " group by TA006"
        genBOM(SQL, workType & "-" & workNo)
    End Sub

    Sub Customer()
        Dim SQL As String = "",
            WHR As String = ""

        If tbCust.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Customer is empty!!!", UpdatePanel1)
            tbCust.Focus()
            Exit Sub
        End If

        If ddlSource.Text.Trim = "1" Then 'sale forcast not close
            WHR = dbConn.WHERE_LIKE("ME002", tbCust)
            SQL = " select  MF003 parentItem,sum(MF008-MF009) orderQty  from COPMF left join COPME on ME001=MF001 " &
                  " where MF008-MF009>0 " & WHR &
                  " group by MF003 "
        Else 'sale order not close
            WHR = dbConn.WHERE_LIKE("TC004", tbCust)
            SQL = " select TD004 parentItem,sum(TD008+TD024-TD009-TD025) orderQty from COPTD left join COPTC on TC001=TD001 and TC002=TD002 " &
                  " where TC027='Y' and TD016='N' " & WHR &
                  " group by TD004 "
        End If
        genBOM(SQL, tbCust.Text.Trim)
    End Sub

    Sub genBOM(SQL As String, docNo As String)
        Dim tempTable As String = "tempBomMaterialsList" & Session("UserName"),
            code As String = "",
            Program As New DataTable,
            orderQty As Decimal,
            item As String,
            qty As Decimal
        CreateTempTable.createTempBOMMaterialsList(tempTable)

        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)

        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                code = .Item("parentItem").ToString.Trim
                orderQty = .Item("orderQty").ToString.Trim
                Dim BomItem = docNo & ":" & code
                Dim whrHash As Hashtable = New Hashtable From {
                    {"BomItem", BomItem} ' parent item
                    }
                'insert Zone
                Dim fldInsHash As Hashtable = New Hashtable From {
                    {"DocNo", docNo}, ' doc no
                    {"ParentItem", code}, ' parent item
                    {"MatItem", code}, ' sub item
                    {"Unit", "PC"}, 'Unit
                    {"orderQty", orderQty}, 'order qty
                    {"QtyPerPcs", 1}, 'qty per pcs
                    {"Property", "M"} 'property
                    }
                dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, whrHash, "I"), VarIni.DBMIS, dbConn.WhoCalledMe)
                CodeBOM(tempTable, docNo, code, code, 1, orderQty)
            End With
        Next
        'process qty
        Dim USQL As String = "",
            ISQL As String = "",
            tempTable2 As String = "tempBomMaterialsList2" & Session("UserName")
        CreateTempTable.createTempPlanIssue(tempTable2)

        SQL = " select MatItem from " & tempTable & " group by MatItem"
        ISQL = "insert into " & tempTable2 & "(item) " & SQL
        dbConn.TransactionSQL(ISQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        'MO Plan Mat Issue
        SQL = " select MOCTB.TB003 as item,SUM(MOCTB.TB004-MOCTB.TB005) as issueQty from " & VarIni.DBMIS & ".." & tempTable2 & " T " &
            " left join MOCTB MOCTB on MOCTB.TB003=T.item " &
            " left join MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
            " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " &
            " group by MOCTB.TB003 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)

        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("issueQty")
            USQL = " if exists(select * from " & tempTable2 & " where item='" & item & "' ) " &
                   " update " & tempTable2 & " set issueQty='" & qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable2 & "(item,issueQty)values ('" & item & "','" & qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'PO
        SQL = " select D.TD004 as item,SUM(isnull(D.TD008,0)-isnull(D.TD015,0)) as po from  " & VarIni.DBMIS & ".." & tempTable2 & " T " &
             " left join PURTD D on D.TD004=T.item where D.TD016='N' and SUBSTRING(D.TD004,3,1) not in ('2')   " &
             " group by D.TD004  order by D.TD004 "

        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable2 & " where item='" & item & "' ) " &
                   " update " & tempTable2 & " set poQty='" & qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable2 & "(item,poQty)values ('" & item & "','" & qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next
        'PR
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from " & VarIni.DBMIS & ".." & tempTable2 & " T " &
               " left join PURTB B on B.TB004=T.item " &
               " left join PURTR R on R.TR001=B.TB001 and R.TR002=B.TB002 and R.TR003=B.TB003 " &
              " where R.TR019='' and B.TB039='N'  and SUBSTRING(B.TB004,3,1) not in ('2')  group by B.TB004 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable2 & " where item='" & item & "' ) " &
                   " update " & tempTable2 & " set prQty='" & qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable2 & "(item,prQty)values ('" & item & "','" & qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'stock        
        SQL = " select INVMC.MC001 as item,sum(INVMC.MC007) as supportQty from " & VarIni.DBMIS & ".." & tempTable2 & " T " &
              " left join INVMC INVMC on INVMC.MC001=T.item " &
              " left join INVMB INVMB on  INVMB.MB001=INVMC.MC001 " &
              " where INVMC.MC002 in ('2201','2202','2204','2205','2206','2301','2302','2306') group by INVMC.MC001 "
        ' Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("supportQty")
            USQL = " if exists(select * from " & tempTable2 & " where  item='" & item & "' ) " &
                   " update " & tempTable2 & " set supportQty='" & qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable2 & "(item,supportQty)values ('" & item & "','" & qty & "')"
            ' Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        SQL = "select T1.DocNo A,T1.ParentItem B, " &
            " case when T1.ParentItem=T1.MatItem then '' else T1.MatItem end C," &
            " INVMB.MB003 D,INVMB.MB002 E,  T1.orderQty F ,T1.QtyPerPcs G,  INVMB.MB004 H," &
            " case T1.Property when 'P' then 'Purchase' " &
            "                  when 'M' then 'Manufacture' " &
            "                  when 'S' then 'Subcontract' " &
            "                  when 'Y' then 'Phantom' " &
            "                  else 'Configuration' end I," &
            " T2.supportQty J,cast(T1.orderQty*T1.QtyPerPcs as decimal(20,3) ) K," &
            " T2.issueQty L,T2.prQty M,T2.poQty N " &
            " from " & tempTable & " T1 left join " & tempTable2 & " T2 on T2.item=T1.MatItem " &
            " left join " & VarIni.ERP & "..INVMB INVMB on INVMB.MB001=T2.item " &
            " order by T1.BomItem,T1.MatItem "

        Dim gvCont As New GridviewControl
        gvCont.ShowGridView(gvShow, SQL, VarIni.DBMIS)
        CountRow1.RowCount = gvCont.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub CodeBOM(tempTable As String, docNo As String, parentItem As String, code As String, qpa As Decimal, orderQty As Decimal)
        Dim SQL As String = "",
            USQL As String = "",
            BomItem As String = docNo & ":" & parentItem
        'SQL = " select MD001,MD003,round(MD006/MC004,3) MD006,MD004,MB025 from BOMMD " &
        '      " left join INVMB on MB001=MD003 left join BOMMC on MC001=MD001 " &
        '      " where MD001='" & code & "' "
        SQL = "select * from (
                select MD001,MD003,MD004,MB025,MC004,round(MD006/MC004,3) MD006,case when MD012='' then convert(varchar, getdate(), 112) else MD012 end MD012 from BOMMD 
                left join BOMMC on MC001=MD001
                left join INVMB on MB001=MD003
                ) BOM 
                where  MD012>=convert(varchar, getdate(), 112) and MD001='" & code & "' 
              "

        Dim Program As New DataTable
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For Each dr As DataRow In Program.Rows
            With New DataRowControl(dr)
                If .Text("MB025") = "M" Then
                    CodeBOM(tempTable, docNo, parentItem, .Text("MD003"), qpa * .Number("MD006"), orderQty)
                Else
                    Dim sumQPA As Decimal = .Text("MD006") * qpa
                    'whr of condition
                    Dim whrHash As New Hashtable From {
                        {"BomItem", BomItem & ":" & .Text("MD003")} ' parent item
                    }
                    'insert Zone
                    Dim fldInsHash As New Hashtable From {
                        {"DocNo", docNo}, ' doc no
                        {"ParentItem", parentItem}, ' parent item
                        {"MatItem", .Text("MD003")}, ' purchase item
                        {"Unit", .Text("MD004")}, 'Unit
                        {"orderQty", orderQty}, 'order qty
                        {"QtyPerPcs", sumQPA}, 'qty per pcs
                        {"Property", .Text("MB025")} 'property
                    }
                    'Update Zone
                    Dim fldUpdHash As New Hashtable From {
                        {"QtyPerPcs", "QtyPerPcs+" & sumQPA} '
                    }
                    dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS)
                End If
            End With
        Next
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        Dim expCont As New ExportImportControl
        expCont.Export("MaterialsList" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class