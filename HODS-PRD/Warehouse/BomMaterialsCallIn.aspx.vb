Public Class BomMaterialsCallIn
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("BomMaterialsCallIn" & Session("UserName"), gvShow)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempBomCallIn" & Session("UserName")
        CreateTempTable.createTempBomMaterialCallIn(tempTable)
        Dim Program As New DataTable
        Dim WHR As String = ""
        Program = Conn_SQL.Get_DataReader(getSql(), Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                CodeBOM(tempTable, .Item("Item"))
            End With
        Next
        Program = New DataTable
        Dim SQL As String = "select * from " & tempTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If Program.Rows.Count = 0 And (tbPartNo.Text.Trim <> "" Or tbSpec.Text.Trim <> "") Then
            WHR = WHR & Conn_SQL.Where("MB001", tbPartNo)
            WHR = WHR & Conn_SQL.Where("MB003", tbSpec)
            Program = New DataTable
            SQL = "select MB001 from INVMB where MB109 = 'Y' " & WHR & " group by MB001"
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    CodeBOM(tempTable, .Item("MB001"))
                End With
            Next
        End If

        Dim item As String = "",
            qty As Decimal = 0,
            USQL As String = ""
        'plan issue qty Approve
        SQL = " select MOCTB.TB003 as item,SUM(MOCTB.TB004-MOCTB.TB005) as issueQty from DBMIS.dbo." & tempTable & " T  " & _
              " left join JINPAO80.dbo.MOCTB MOCTB on MOCTB.TB003=T.item " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " & _
              " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & _
              " group by MOCTB.TB003 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("issueQty")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set issueQty='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,issueQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'plan issue qty not Approve
        SQL = " select MOCTB.TB003 as item,SUM(MOCTB.TB004-MOCTB.TB005) as issueQty from DBMIS.dbo." & tempTable & " T  " & _
              " left join JINPAO80.dbo.MOCTB MOCTB on MOCTB.TB003=T.item " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " & _
              " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='N' " & _
              " group by MOCTB.TB003 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("issueQty")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set issueQtyNot='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,issueQtyNot)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'PO Insp Qty
        SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH015,0)) as po_insp from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.PURTH PURTH on PURTH.TH004=T.item " & _
              " left join JINPAO80.dbo.PURTG PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " & _
              " where PURTG.TG013 = 'N'  group by PURTH.TH004 having (SUM(isnull(PURTH.TH015, 0)) > 0) " & _
              " order by PURTH.TH004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("po_insp")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set poRcpQty='" & qty & "',poQty=poQty-" & qty & " where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Plan Receipts Approve qty(PO,MO)
        'PO
        SQL = " select PURTD.TD004 as item,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from  DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.PURTD PURTD on PURTD.TD004=T.item " & _
              " left join JINPAO80.dbo.PURTC PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " & _
              " where PURTD.TD016='N' and TC014='Y' " & _
              " group by PURTD.TD004 order by PURTD.TD004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set planQty= '" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,planQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'MO
        SQL = " select A.TA006 as item, SUM(isnull(A.TA015,0)-isnull(A.TA017,0)) as mo from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.MOCTA A on A.TA006=T.item where A.TA011 not in('y','Y') and A.TA013 ='Y'  " & _
              " group by A.TA006  order by A.TA006 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("mo")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set planQty = planQty+'" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,planQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'Plan Receipts Not Approve qty(PO,MO)
        'PO
        SQL = " select PURTD.TD004 as item,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from  DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.PURTD PURTD on PURTD.TD004=T.item " & _
              " left join JINPAO80.dbo.PURTC PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " & _
              " where PURTD.TD016='N' and TC014='N' " & _
              " group by PURTD.TD004 order by PURTD.TD004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set planQtyNot= '" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,planQtyNot)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'MO
        SQL = " select A.TA006 as item, SUM(isnull(A.TA015,0)-isnull(A.TA017,0)) as mo from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.MOCTA A on A.TA006=T.item where A.TA011 not in('y','Y') and A.TA013 ='N'  " & _
              " group by TA006 order by TA006"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("mo")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set planQty = planQtyNot+'" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,planQtyNot)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'purchase request Appove 
        SQL = " select PURTB.TB004 as item,sum(PURTR.TR006) as pr from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.PURTB PURTB on PURTB.TB004=T.item" & _
              " left join JINPAO80.dbo.PURTR PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " & _
              " left join JINPAO80.dbo.PURTA PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " & _
              " where PURTR.TR019='' and PURTB.TB039='N' and PURTA.TA007 = 'Y' " & _
              " group by PURTB.TB004 order by PURTB.TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set prQty='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'purchase request Not Appove 
        SQL = " select PURTB.TB004 as item,sum(PURTR.TR006) as pr from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.PURTB PURTB on PURTB.TB004=T.item" & _
              " left join JINPAO80.dbo.PURTR PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " & _
              " left join JINPAO80.dbo.PURTA PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " & _
              " where PURTR.TR019='' and PURTB.TB039='N' and PURTA.TA007 = 'N' " & _
              " group by PURTB.TB004 order by PURTB.TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set prQtyNot='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,prQtyNot)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'stock
        SQL = " select INVMC.MC001 as item,sum(INVMC.MC007) as stockQty from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
              " where INVMC.MC002 in ('2101','2201','2400','2900','3333','3000','3100','3200','3300','3400','3500','3600','3700','3800') " & _
              " group by INVMC.MC001 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("stockQty")
            USQL = " if exists(select * from " & tempTable & " where  item='" & item & "' ) " & _
                   " update " & tempTable & " set stockQty='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,stockQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'show Data

        If ddlCondition.Text <> "0" Then
            WHR = " where "
            Select Case ddlCondition.Text
                Case "1" 'stock<plan issue
                    WHR = WHR & " T.stockQty < T.issueQty+T.issueQtyNot "
                Case "2" 'stock>=plan issue
                    WHR = WHR & " T.stockQty >= T.issueQty+T.issueQtyNot "
            End Select
        End If
        
        SQL = " select T.item as 'Part No',INVMB.MB002 as 'Desc.',INVMB.MB003 as 'Spec',INVMB.MB004 as Unit," & _
              " T.issueQty as 'Mat Issue',T.issueQtyNot as 'Mat Issue Not', " & _
              " T.stockQty as 'Stock',T.poInspQty as 'PO Insp.',T.planQty as 'Plan Receipts', " & _
              " T.planQtyNot as 'Plan Receipts Not App.',T.prQty as 'PR',T.prQtyNot as 'PR not App.', " & _
              " cast(T.stockQty+T.poInspQty-T.issueQty-T.issueQtyNot as decimal(16,3)) as 'Call In Qty' " & _
              " from " & tempTable & " T left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & _
              WHR & " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        btExport.Visible = True
        lbCnt.Text = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Function getSql() As String
        Dim SQL As String = "",
            WHR As String = "",
            fldDocType As String = "",
            fldDocNo As String = "",
            fldDocSeq As String = "",
            fldSelect As String = "",
            fldCust As String = "",
            fldItem As String = "",
            fldSpec As String = "",
            fldDate As String = ""

        Select Case ddlSource.Text.Trim
            Case "1" 'Sale Order
                fldDocType = "TC001"
                fldDocNo = "TC002"
                fldDocSeq = "TC003"
                fldCust = "TC004"
                fldItem = "TD004"
                fldSpec = "TD006"
                fldDate = "TC003"
                If ddlDate.Text.Trim = "2" Then
                    fldDate = "TD013"
                End If
                fldSelect = fldItem & " as Item "
            Case "2" 'Manufactor Order
                fldDocType = "TA001"
                fldDocNo = "TA002"
                fldItem = "TA006"
                fldSpec = "TA035"
                fldDate = "TA003"
                If ddlDate.Text.Trim = "2" Then
                    fldDate = "TA009"
                End If
                fldSelect = fldItem & " as Item "
        End Select

        WHR = WHR & Conn_SQL.Where(fldDocType, tbDocType, False) 'doc type
        WHR = WHR & Conn_SQL.Where(fldDocNo, tbDocNo) 'doc number
        WHR = WHR & Conn_SQL.Where(fldDocSeq, tbDocSeq) 'doc seq
        WHR = WHR & Conn_SQL.Where(fldCust, tbCust) 'Customer
        WHR = WHR & Conn_SQL.Where(fldDocSeq, tbDocSeq) 'Item
        WHR = WHR & Conn_SQL.Where(fldCust, tbCust) 'Spec
        WHR = WHR & Conn_SQL.Where(fldItem, tbPartNo) 'Item
        WHR = WHR & Conn_SQL.Where(fldSpec, tbSpec) 'Spec
        WHR = WHR & Conn_SQL.Where(fldDate, configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim)) 'Date
        Select Case ddlSource.Text.Trim
            Case "1"
                SQL = "select " & fldSelect & " from COPTD left join COPTC on TC001=TD001 and TC002=TD002 where TD016='N' and TC027 = 'Y' " & WHR & " group by " & fldItem
            Case "2"
                SQL = "select " & fldSelect & " from MOCTA where TA011 not in ('Y','y') and TA013 = 'Y' " & WHR & " group by " & fldItem
        End Select
        Return SQL
    End Function

    Sub InsertSQL(tempTable As String, code As String)
        Dim listCode As Hashtable = New Hashtable
        Dim cnt As Integer = 0
        For Each boxItem As ListItem In cblCodeType.Items
            If boxItem.Selected = True Then
                listCode.Add(boxItem.Value, boxItem.Value)
                cnt = cnt + 1
            End If
        Next
        Dim insert As Boolean = False

        If cnt > 0 Then
            For Each codeChk As String In listCode.Keys
                Dim strChp As String = code.Substring(2, CInt(codeChk.Length))
                If strChp = "4" And strChp = codeChk Then
                    Dim strChp2 As String = code.Substring(2, 2)
                    If strChp2 <> "43" And strChp2 <> "49" Then
                        insert = True
                    End If
                Else
                    If strChp = codeChk Then
                        insert = True
                    End If
                End If
                If insert Then
                    Exit For
                End If
            Next
        Else
            insert = True
        End If
        If insert Then
            Dim SQL As String = "if not exists(select * from " & tempTable & " where item='" & code & "' ) insert into " & tempTable & "(item)values('" & code & "') "
            Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
        End If
    End Sub

    Protected Sub CodeBOM(tempTable As String, code As String)
        Dim SQL As String = "",
            USQL As String = ""

        'BomItem As String = docNo & ":" & parentItem
        SQL = " select MD001,MD003,MB025 from BOMMD " & _
              " left join INVMB on MB001=MD003 " & _
              " where MD001='" & code & "' "
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim sSQL As String = ""
                '" if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL & " else " & ISQL
                If i = 0 Then
                    InsertSQL(tempTable, .Item("MD001"))
                    'sSQL = "if not exists(select * from " & tempTable & " where item='" & subItem & "' ) inser into " & tempTable & "(item)values('" & subItem & "') "
                End If
                InsertSQL(tempTable, .Item("MD003"))
                If .Item("MB025") = "M" Then
                    CodeBOM(tempTable, .Item("MD003"))
                End If
            End With
        Next
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Part No")) Then
                    Dim link As String = ""
                    Dim jpPart As String = .DataItem("Part No")
                    link = link & "&JPPart= " & jpPart
                    hplDetail.NavigateUrl = "BomMaterialsCallInPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub
End Class