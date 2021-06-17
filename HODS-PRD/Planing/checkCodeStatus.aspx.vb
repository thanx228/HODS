Public Class checkCodeStatus
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='51' order by MQ002"
            'ControlForm.showDDL(ddlTypeCode, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            btExport.Visible = False
        End If
    End Sub
    Private Function substrSQL(fld As String, val As String) As String
        Return " and SUBSTRING(" & fld & ",3,1) ='" & val & "' "
    End Function
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempCodeStatus" & Session("UserName")
        Dim TypeCode As String = ddlTypeCode.SelectedValue.ToString
        CreateTempTable.createTempCheckCodeStatus(tempTable, TypeCode)
        Dim whr As String = "", SQL As String = "", USQL As String = "", ISQL As String = "", whr2 As String = ""
        Dim code As String = tbCode.Text, spec As String = tbSpec.Text
        Dim item As String = "", Qty As Integer = 0
        Dim Program As New DataTable

        'Sale order
        whr = substrSQL("COPTD.TD004", TypeCode)
        If code <> "" Then
            whr = whr & " and COPTD.TD004 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and COPTD.TD006 like '%" & spec & "%' "
        End If
        SQL = " select COPTD.TD004,SUM(COPTD.TD008-COPTD.TD009) from JINPAO80.dbo.COPTD COPTD " & _
            " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
            " where COPTD.TD016='N' " & whr & _
            " group by COPTD.TD004  order by COPTD.TD004 "
        ISQL = "insert into " & tempTable & "(item,delQty) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        'MO Plan Mat Issue
        whr = substrSQL("MOCTB.TB003", TypeCode)
        If code <> "" Then
            whr = whr & " and MOCTB.TB003 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and MOCTB.TB013 like '%" & spec & "%' "
        End If

        SQL = " select TB003 as item,SUM(TB004-TB005) as issue from  MOCTB " & _
            " left join MOCTA on TA001=TB001 and TA002=TB002 " & _
            " where TB004-TB005>0 and  TA011 not in('y','Y') and TA013='Y' " & whr & " group by TB003 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("issue")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set issueQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,issueQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'MO 
        whr = substrSQL("A.TA006", TypeCode)
        If code <> "" Then
            whr = whr & " and A.TA006 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and A.TA035 like '%" & spec & "%' "
        End If

        SQL = "select A.TA006 as item, SUM(isnull(A.TA015,0)-isnull(A.TA017,0)-isnull(A.TA018,0)-isnull(A.TA060,0)) as mo from MOCTA A where A.TA011 not in('y','Y') and A.TA013 ='Y'  " & whr & " group by TA006  order by A.TA006"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("mo")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'PO
        whr = substrSQL("D.TD004", TypeCode)
        If code <> "" Then
            whr = whr & " and D.TD004 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and (D.TD005 like '%" & spec & "%' or D.TD006 like '%" & spec & "%' ) "
        End If
        SQL = " select D.TD004 as item,SUM(isnull(D.TD008,0)-isnull(D.TD015,0)) as po from PURTD D where D.TD016='N' " & whr & " group by D.TD004  order by D.TD004 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set poQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,poQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'PR
        whr = substrSQL("B.TB004", TypeCode)
        If code <> "" Then
            whr = whr & " and B.TB004 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and (B.TB005 like '%" & spec & "%' or B.TB006 like '" & spec & "')  "
        End If
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from PURTR R left join PURTB B on B.TB001=R.TR001 and B.TB002=R.TR002 and TB003=TR003 where R.TR019='' and B.TB039='N' " & whr & " group by B.TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'wh = 2400 
        whr = substrSQL("C.MC001", TypeCode)
        If code <> "" Then
            whr = whr & " and C.MC001 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and (B.MB002 like '%" & spec & "%' or B.MB003 like '" & spec & "')  "
        End If
        
        SQL = " select B.MB001 as item,C.MC007 as Qty from INVMC C left join INVMB B on B.MB001=C.MC001 " & _
              " where C.MC002 in ('2400') " & whr & " order by B.MB001 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("Qty")

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set supportQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,supportQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'select WH
        whr = substrSQL("C.MC001", TypeCode)
        If code <> "" Then
            whr = whr & " and C.MC001 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and (B.MB002 like '%" & spec & "%' or B.MB003 like '" & spec & "')  "
        End If
        Dim fld As String = "B.MB001 as item",
            grp As String = "",
            wh As String = "",
            fldName As String = ""
        Select Case TypeCode
            Case "2"
                wh = "'2101'"
                fld = fld & ",C.MC007 as Qty,C.MC002 as wh"
            Case "3"
                grp = " group by B.MB001 "
                wh = "'3100','3200','3300','3400','3500','3600','3700','3800'"
                fld = fld & ",sum(C.MC007) as Qty"
            Case Else
                fld = fld & ",C.MC007 as Qty,C.MC002 as wh"
                wh = "'2201','2202','2204','2205','2206','2900','2901','3333'"
        End Select

        SQL = " select " & fld & " from INVMC C left join INVMB B on B.MB001=C.MC001 " & _
              " where C.MC002 in (" & wh & ") and C.MC007>0 " & whr & grp & " order by B.MB001 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                item = .Item("item")
                Qty = .Item("Qty")

                If TypeCode = "3" Then
                    fldName = "whQty"
                Else
                    Select Case .Item("wh").ToString.Trim
                        Case "2101" 'FG
                            fldName = "wh2101"
                            'Case "2201" 'mat & sp
                        Case "3333" 'mat & sp
                            fldName = "wh3333"
                        Case Else 'mat & sp
                            fldName = "wh2201"
                    End Select
                End If
                USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                       " update " & tempTable & " set " & fldName & "='" & Qty & "' where item='" & item & "' else " & _
                       " insert into " & tempTable & "(item," & fldName & ")values ('" & item & "','" & Qty & "')"
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            End With
        Next

        'Show Data
        Dim fldStock As String = ""
        fld = ""
        Select Case TypeCode
            Case "2"
                fld = ",T.wh2101 as 'WH 2101' "
                fldStock = "T.wh2101"
            Case "3"
                fld = ",T.whQty as 'Stock' "
                fldStock = "T.whQty"
            Case Else
                fld = " ,T.wh2201 as 'WH 2201', T.wh3333 as 'WH 3333'"
                fldStock = "T.wh2201+T.wh3333"
        End Select

        Dim strDemand As String = " T.issueQty+T.delQty  "
        Dim strSupply As String = fldStock & "+T.moQty+T.poQty+T.prQty+T.supportQty "
        Dim valConsel As String = ddlCondition.SelectedValue.ToString
        whr = ""
        If valConsel <> "0" Then
            whr = " where "
            If valConsel = "1" Then   'Supply >= Demand ,Demand>0
                whr = whr & strSupply & ">=" & strDemand & " and " & strDemand & ">0  "
            ElseIf valConsel = "2" Then 'Demand>=Supply , Supply>0
                whr = whr & strDemand & ">=" & strSupply & " and " & strSupply & ">0 "
            ElseIf valConsel = "3" Then  ' Demand=0,Supply>0
                whr = whr & strDemand & "=0 and " & strSupply & ">0 "
            ElseIf valConsel = "4" Then  'Supply=0,Demand>0
                whr = whr & strSupply & "=0 and " & strDemand & ">0 "
            ElseIf valConsel = "5" Then  'Enough Stock
                whr = whr & fldStock & " >= " & strDemand & " and " & strDemand & ">0 "
            ElseIf valConsel = "6" Then  'Demand > Supply
                whr = whr & strDemand & ">" & strSupply
            ElseIf valConsel = "7" Then  'Stock>0
                whr = whr & fldStock & " >0 "
            ElseIf valConsel = "8" Then  'Stock>0
                whr = whr & "T.supportQty >0 "
            End If
        End If
        'SQL = " select T.item as JP_Part,INVMB.MB002+'-'+ INVMB.MB003 as JP_SPEC," & _
        '      " T.delQty as SO,T.issueQty as MatIssue,cast(INVMB.MB064 as decimal(20,3)) as Stock, " & _
        '      " T.moQty as MO,T.poQty as PO,T.prQty as PR,INVML.ML005 " & _
        '      " from " & tempTable & " T " & _
        '      " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & _
        '      " left join JINPAO80.dbo.INVML INVML on INVML.ML001=INVMB.MB001 " & whr & _
        '      " order by T.item "
        SQL = " select T.item as 'Item',INVMB.MB002 as 'Desc.',INVMB.MB003 as 'Spec'" & fld & "," & _
              " T.delQty as SO,T.issueQty as 'Mat Issue', " & _
              " T.moQty as MO,T.poQty as PO,T.prQty as PR,supportQty as Support    " & _
              " from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & whr & _
              " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Item")) Then
                    Dim link As String = ""
                    Dim jpPart As String = .DataItem("Item")
                    link = link & "&JPPart= " & jpPart
                    link = link & "&JPSpec= " & .DataItem("Spec")
                    link = link & "&delQty= " & .DataItem("SO")
                    link = link & "&issueQty= " & .DataItem("Mat Issue")
                    'Dim TypeCode As String = ddlTypeCode.SelectedValue.ToString
                    Select Case ddlTypeCode.SelectedValue.ToString
                        Case "2"
                            link = link & "&stock= " & .DataItem("WH 2101")
                        Case "3"
                            link = link & "&stock= " & .DataItem("Stock")
                        Case Else
                            Dim whQty As Decimal = CDec(.DataItem("WH 2201")) + CDec(.DataItem("WH 3333"))
                            link = link & "&stock= " & whQty
                    End Select
                    link = link & "&poQty= " & .DataItem("PO")
                    link = link & "&prQty= " & .DataItem("PR")
                    link = link & "&moQty= " & .DataItem("MO")
                    link = link & "&supportQty= " & .DataItem("Support")
                    hplDetail.NavigateUrl = "checkCodeStatusPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                '.BackColor = Drawing.Color.Violet
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("checkCodeStatus" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class