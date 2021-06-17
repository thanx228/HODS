Public Class MaterialsList
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showDDL(ddlWorkType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
            tbOrdQty.Text = 1
            
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        'Dim SearchBy As String = ddlSearchBy.Text
        Dim SQL As String = "",
            WHR As String = "",
            docNo As String = "",
            Program As New DataTable,
            orderQty As Decimal = CDec(tbOrdQty.Text.Trim),
            item As String = "",
            Qty As Integer = 0

        Select Case ddlSearchBy.Text
            Case "1" 'Item
                item = tbItem.Text.Trim
                Dim spec As String = tbSpec.Text.Trim
                If item = "" And spec = "" Then
                    show_message.ShowMessage(Page, "Item and Spec is null !!!", UpdatePanel1)
                    tbItem.Focus()
                    Exit Sub
                End If
                If item <> "" Then
                    WHR = " and MB001 like '%" & item & "%' "
                End If
                If spec <> "" Then
                    WHR = WHR & " and MB003 like '%" & spec & "%' "
                End If
                If orderQty = 0 Then
                    orderQty = 1
                End If
                SQL = " select MB001 as parentItem," & orderQty & " as orderQty from INVMB where MB109 = 'Y' " & WHR
                'SQL = "select * from COPMG where "
            Case "2" 'Sale Order
                Dim saleType As String = ddlSaleType.Text.Trim,
                    saleNo As String = tbSaleNo.Text.Trim,
                    saleSeq As String = tbSaleSeq.Text.Trim
                If saleNo = "" Then
                    show_message.ShowMessage(Page, "Sale No is null !!!", UpdatePanel1)
                    tbSaleNo.Focus()
                    Exit Sub
                End If
                docNo = saleType & "-" & saleNo
                WHR = Conn_SQL.Where("TD002", tbSaleNo)
                WHR = WHR & Conn_SQL.Where("TD003", tbSaleSeq)
                WHR = WHR & Conn_SQL.Where("TC004", tbCust)

                'If saleSeq <> "" Then
                '    WHR = " and TD003 like'%" & tbSaleSeq & "' "
                'End If
                SQL = "select TD004 as parentItem,sum(TD008) as orderQty from COPTD left join COPTC on TC001=TD001 and TC002=TD002 where TD001='" & saleType & "' " & WHR & " group by TD004"
            Case "3" 'Manufactor Order
                Dim workType As String = ddlWorkType.Text.Trim,
                    workNo As String = tbWorkNo.Text.Trim
                If workNo = "" Then
                    show_message.ShowMessage(Page, "Manufactor No is null !!!", UpdatePanel1)
                    tbWorkNo.Focus()
                    Exit Sub
                End If
                docNo = workType & "-" & workNo
                SQL = "select TA006 as parentItem,TA015 as orderQty from MOCTA left join MOCTC on TC001=TA026 and TC002=TA027 where TA001='" & workType & "' and TA002='" & workNo & "' "
        End Select
        Dim tempTable As String = "tempBomMaterialsList" & Session("UserName"),
            code As String = ""
        CreateTempTable.createTempBOMMaterialsList(tempTable)
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                code = .Item("parentItem").ToString.Trim
                orderQty = .Item("orderQty").ToString.Trim
                Dim BomItem = docNo & ":" & code
                Dim fldInsHash As Hashtable = New Hashtable,
                whrHash As Hashtable = New Hashtable
                whrHash.Add("BomItem", BomItem) ' parent item
                'insert Zone
                fldInsHash.Add("DocNo", docNo) ' doc no
                fldInsHash.Add("ParentItem", code) ' parent item
                fldInsHash.Add("MatItem", code) ' sub item
                fldInsHash.Add("Unit", "PC") 'Unit
                fldInsHash.Add("orderQty", orderQty) 'order qty
                fldInsHash.Add("QtyPerPcs", 1) 'qty per pcs
                fldInsHash.Add("Property", "M") 'property
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
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
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        'MO Plan Mat Issue
        SQL = " select MOCTB.TB003 as item,SUM(MOCTB.TB004-MOCTB.TB005) as issueQty from DBMIS.dbo." & tempTable2 & " T " & _
            " left join JINPAO80.dbo.MOCTB MOCTB on MOCTB.TB003=T.item " & _
            " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " & _
            " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & _
            " group by MOCTB.TB003 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("issueQty")
            USQL = " if exists(select * from " & tempTable2 & " where item='" & item & "' ) " & _
                   " update " & tempTable2 & " set issueQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable2 & "(item,issueQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'PO
        SQL = " select D.TD004 as item,SUM(isnull(D.TD008,0)-isnull(D.TD015,0)) as po from  DBMIS.dbo." & tempTable2 & " T " & _
             " left join JINPAO80.dbo.PURTD D on D.TD004=T.item where D.TD016='N' and SUBSTRING(D.TD004,3,1) not in ('2')   " & _
             " group by D.TD004  order by D.TD004 "
 
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable2 & " where item='" & item & "' ) " & _
                   " update " & tempTable2 & " set poQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable2 & "(item,poQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'PR
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from DBMIS.dbo." & tempTable2 & " T " & _
               " left join JINPAO80.dbo.PURTB B on B.TB004=T.item " & _
               " left join JINPAO80.dbo.PURTR R on R.TR001=B.TB001 and R.TR002=B.TB002 and R.TR003=B.TB003 " & _
              " where R.TR019='' and B.TB039='N'  and SUBSTRING(B.TB004,3,1) not in ('2')  group by B.TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable2 & " where item='" & item & "' ) " & _
                   " update " & tempTable2 & " set prQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable2 & "(item,prQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'stock        
        SQL = " select INVMC.MC001 as item,sum(INVMC.MC007) as supportQty from DBMIS.dbo." & tempTable2 & " T " & _
              " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
              " left join JINPAO80.dbo.INVMB INVMB on  INVMB.MB001=INVMC.MC001 " & _
              " where INVMC.MC002 in ('2201','2202','2204','2205','2206','2400','2900','2901','3000','3333') group by INVMC.MC001 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("supportQty")
            USQL = " if exists(select * from " & tempTable2 & " where  item='" & item & "' ) " & _
                   " update " & tempTable2 & " set supportQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable2 & "(item,supportQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        SQL = "select T1.DocNo as 'Doc No',T1.ParentItem as 'Parent Item', " & _
            " case when T1.ParentItem=T1.MatItem then '' else T1.MatItem end as 'Sub Item'," & _
            " INVMB.MB003 as 'Item Spec',INVMB.MB002 as 'Item Desc', " & _
            " T1.orderQty as 'Order Qty' ,T1.QtyPerPcs as 'Qty Per', " & _
            " INVMB.MB004 as 'Unit'," & _
            " case T1.Property when 'P' then 'Purchase' " & _
            "                  when 'M' then 'Manufacture' " & _
            "                  when 'S' then 'Subcontract' " & _
            "                  when 'Y' then 'Phantom' " & _
            "                  else 'Configuration' end as Property," & _
            " T2.supportQty as 'Stock',cast(T1.orderQty*T1.QtyPerPcs as decimal(20,3) ) as 'Req Qty'," & _
            " T2.issueQty as 'Plan Issue',T2.prQty as 'PR',T2.poQty as 'PO' " & _
            " from " & tempTable & " T1 left join " & tempTable2 & " T2 on T2.item=T1.MatItem " & _
            " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T2.item " & _
            " order by T1.BomItem,T1.MatItem "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    'Protected Sub generateBOM(tempTable As String, BomItem As String, docNo As String, codePrd As String, orderQty As Decimal)
    '    Dim fldInsHash As Hashtable = New Hashtable,
    '        whrHash As Hashtable = New Hashtable
    '    whrHash.Add("BomItem", BomItem) ' parent item
    '    'insert Zone
    '    fldInsHash.Add("DocNo", docNo) ' doc no
    '    fldInsHash.Add("ParentItem", codePrd) ' parent item
    '    fldInsHash.Add("SubItem", codePrd) ' sub item
    '    fldInsHash.Add("Unit", "PC") 'Unit
    '    fldInsHash.Add("orderQty", orderQty) 'order qty
    '    fldInsHash.Add("QtyPerPcs", 1) 'qty per pcs
    '    fldInsHash.Add("Property", "M") 'property
    '    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
    '    CodeBOM(tempTable, docNo, codePrd, BomItem, 1, orderQty)
    'End Sub
    Protected Sub CodeBOM(tempTable As String, docNo As String, parentItem As String, code As String, qpa As Decimal, orderQty As Decimal)
        Dim SQL As String = "",
            USQL As String = "",
            BomItem As String = docNo & ":" & parentItem

        SQL = " select MD001,MD003,MD006,MD004,MB025 from BOMMD " & _
              " left join INVMB on MB001=MD003 " & _
              " where MD001='" & code & "' "
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                If .Item("MB025") = "M" Then
                    CodeBOM(tempTable, docNo, parentItem, .Item("MD003"), qpa * .Item("MD006"), orderQty)
                Else
                    Dim fldInsHash As Hashtable = New Hashtable,
                        whrHash As Hashtable = New Hashtable,
                        fldUpdHash As Hashtable = New Hashtable,
                        sumQPA As Decimal = .Item("MD006") * qpa
                    'whr of condition
                    whrHash.Add("BomItem", BomItem & ":" & .Item("MD003").ToString.Trim) ' parent item
                    'insert Zone
                    fldInsHash.Add("DocNo", docNo) ' doc no
                    fldInsHash.Add("ParentItem", parentItem) ' parent item
                    fldInsHash.Add("MatItem", .Item("MD003")) ' purchase item
                    fldInsHash.Add("Unit", .Item("MD004")) 'Unit
                    fldInsHash.Add("orderQty", orderQty) 'order qty
                    fldInsHash.Add("QtyPerPcs", sumQPA) 'qty per pcs
                    fldInsHash.Add("Property", .Item("MB025")) 'property
                    'Update Zone
                    fldUpdHash.Add("QtyPerPcs", "QtyPerPcs+" & sumQPA) '
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                End If
            End With
        Next
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
            If Not IsNothing(hplDetail) And Not IsDBNull(e.Row.DataItem("Sub Item")) Then
                Dim link As String = ""
                link = link & "&JPPart= " & e.Row.DataItem("Sub Item")
                link = link & "&JPSpec= " & e.Row.DataItem("Item Spec")
                link = link & "&stock= " & e.Row.DataItem("Stock")
                link = link & "&issueQty= " & e.Row.DataItem("Plan Issue")
                link = link & "&poQty= " & e.Row.DataItem("PO")
                link = link & "&prQty= " & e.Row.DataItem("PR")
                hplDetail.NavigateUrl = "MaterialsListPopup.aspx?height=150&width=350" & link
                hplDetail.Attributes.Add("title", e.Row.DataItem("Sub Item"))
            End If
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("BomMaterialsList" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class