Public Class MaterailsShortage
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        gvShow.Visible = True
        gvPoCallIn.Visible = False
        gvPlanCallIn.Visible = False

        Dim tempTable As String = "tempMaterialsShortage" & Session("UserName")
        CreateTempTable.createTempMaterialShortage(tempTable)

        Dim SQL As String = "", WHR As String = "", ISQL As String = "", USQL As String = ""

        Dim CodeType As String = ddlCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text,
            Spec As String = tbSpec.Text,
            EndDate As String = configDate.dateFormat2(tbDateTo.Text),
            Program As New DataTable,
            Program1 As New DataTable,
            item As String = "",
            Qty As Integer = 0

        If CodeType <> "5" And CodeType <> "6" Then
            'MO issue mat 
            WHR = configDate.DateWhere("MOCTB.TB015", "", EndDate)
            If CodeType <> "0" Then
                WHR = WHR & " and substring(MOCTB.TB003,3,1) = '" & CodeType & "' "
            End If
            If Code <> "" Then
                WHR = WHR & " and MOCTB.TB003 like '%" & Code & "%' "
            End If
            If Spec <> "" Then
                WHR = WHR & " and MOCTB.TB013 like '%" & Spec & "%' "
            End If

            SQL = " select MOCTB.TB003,SUM(MOCTB.TB004-MOCTB.TB005) from  JINPAO80.dbo.MOCTB MOCTB " & _
                  " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " & _
                  " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & WHR & _
                  " group by MOCTB.TB003 "
            ISQL = "insert into " & tempTable & "(item,issueQty) " & SQL
            Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

            'MO 
            WHR = configDate.DateWhere("MOCTA.TA010", "", EndDate)
            If CodeType <> "0" Then
                WHR = WHR & " and substring(MOCTA.TA006,3,1) = '" & CodeType & "' "
            End If
            If Code <> "" Then
                WHR = WHR & " and MOCTA.TA006 like '%" & Code & "%' "
            End If
            If Spec <> "" Then
                WHR = WHR & " and MOCTA.TA035 like '%" & Spec & "%' "
            End If

            SQL = " select MOCTA.TA006 as item, SUM(isnull(MOCTA.TA015,0)-isnull(MOCTA.TA017,0)) as mo from MOCTA " & _
                  " where MOCTA.TA011 not in('y','Y') and MOCTA.TA013 ='Y'  " & WHR & _
                  " group by MOCTA.TA006  order by MOCTA.TA006"

            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                item = Program.Rows(i).Item("item")
                Qty = Program.Rows(i).Item("mo")
                USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                       " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " & _
                       " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            Next

            'Sale Order
            WHR = configDate.DateWhere("COPTD.TD013", "", EndDate)
            If CodeType <> "0" Then
                WHR = WHR & " and substring(COPTD.TD004,3,1) = '" & CodeType & "' "
            End If
            If Code <> "" Then
                WHR = WHR & " and COPTD.TD004 like '%" & Code & "%' "
            End If
            If Spec <> "" Then
                WHR = WHR & " and (COPTD.TD006 like '%" & Spec & "%' or COPTD.TD005 like '%" & Spec & "%') "
            End If
            SQL = " select COPTD.TD004 as item,SUM(COPTD.TD008-COPTD.TD009) as so from COPTD " & _
                  " left join  COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                  " where COPTD.TD016='N' " & WHR & " group by COPTD.TD004  order by COPTD.TD004 "
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                item = Program.Rows(i).Item("item")
                Qty = Program.Rows(i).Item("so")
                USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                       " update " & tempTable & " set delQty='" & Qty & "' where item='" & item & "' else " & _
                       " insert into " & tempTable & "(item,delQty)values ('" & item & "','" & Qty & "')"
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            Next
        End If

        'purchase request
        ' WHR = configDate.DateWhere("PURTB.TB011", "", EndDate)
        'If CodeType <> "0" Then
        '    WHR = WHR & " and substring(PURTB.TB004,3,1) = '" & CodeType & "' "
        'End If
        WHR = ""
        If CodeType <> "0" Then
            If CodeType <> "5" And CodeType <> "6" Then
                WHR = WHR & " and substring(PURTB.TB004,3,1) = '" & CodeType & "' "
            End If
            Select Case CodeType
                Case "5" '1401
                    WHR = WHR & " and INVMB.MB005 = '1401' "
                Case "6" '1402
                    WHR = WHR & " and INVMB.MB005 = '1402' "
                Case Else
                    WHR = WHR & " and INVMB.MB005 not in ('1401','1402') "
            End Select
        End If
        If Code <> "" Then
            WHR = WHR & " and PURTB.TB004 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and (PURTB.TB005 like '%" & Spec & "%' or PURTB.TB006 like '%" & Spec & "%') "
        End If
        SQL = " select PURTB.TB004 as item,sum(PURTR.TR006) as pr from  PURTB " & _
              " left join PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " & _
              " left join PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " & _
              " left join INVMB on INVMB.MB001=PURTB.TB004 " & _
              " where PURTR.TR019='' and PURTB.TB039='N' and PURTA.TA007 = 'Y' " & WHR & _
              " group by PURTB.TB004 order by PURTB.TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'purchase order
        'WHR = configDate.DateWhere("PURTD.TD012", "", EndDate)
        WHR = ""
        If CodeType <> "0" Then
            If CodeType <> "5" And CodeType <> "6" Then
                WHR = WHR & " and substring(PURTD.TD004,3,1) = '" & CodeType & "' "
            End If
            Select Case CodeType
                Case "5" '4100
                    WHR = WHR & " and INVMB.MB005 = '1401' "
                Case "6" '4200
                    WHR = WHR & " and INVMB.MB005 = '1402' "
                Case Else
                    WHR = WHR & " and INVMB.MB005 not in ('1401','1402') "
            End Select
        End If

        'If CodeType <> "0" Then
        '    WHR = WHR & " and substring(PURTD.TD004,3,1) = '" & CodeType & "' "
        'End If
        If Code <> "" Then
            WHR = WHR & " and PURTD.TD004 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and (PURTD.TD005 like '%" & Spec & "%' or PURTD.TD006 like '%" & Spec & "%') "
        End If

        SQL = " select PURTD.TD004 as item,substring(PURTD.TD024,1,1) as poType,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from PURTD " & _
              " left join INVMB on INVMB.MB001=PURTD.TD004 " & _
              " where PURTD.TD016='N' " & WHR & _
              " group by PURTD.TD004,substring(PURTD.TD024,1,1) " & _
              " order by PURTD.TD004,substring(PURTD.TD024,1,1) "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")

            Dim fldPO As String = "poManQty"
            Select Case Program.Rows(i).Item("poType")
                Case "2"
                    fldPO = "poQty"
                Case "4"
                    fldPO = "poForQty"
                Case "5"
                    fldPO = "poMoQty"
            End Select

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set " & fldPO & "= " & fldPO & "+'" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item," & fldPO & ")values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'purchase receipt inspection
        WHR = configDate.DateWhere("PURTG.TG014", "", EndDate)
        'If CodeType <> "0" Then
        '    WHR = WHR & " and substring(PURTH.TH004,3,1) = '" & CodeType & "' "
        'End If
        If CodeType <> "0" Then
            If CodeType <> "5" And CodeType <> "6" Then
                WHR = WHR & " and substring(PURTH.TH004,3,1) = '" & CodeType & "' "
            End If
            Select Case CodeType
                Case "5" '1401
                    WHR = WHR & " and INVMB.MB005 = '1401' "
                Case "6" '1402
                    WHR = WHR & " and INVMB.MB005 = '1402' "
                Case Else
                    WHR = WHR & " and INVMB.MB005 not in ('1401','1402') "
            End Select
        End If
        If Code <> "" Then
            WHR = WHR & " and PURTH.TH004 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and (PURTH.TH005 like '%" & Spec & "%' or PURTH.TH006 like '%" & Spec & "%') "
        End If
        SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH007,0)) as po_insp from PURTH " & _
              " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " & _
              " left join INVMB on INVMB.MB001=PURTH.TH004 " & _
              " where PURTG.TG013 = 'N' " & WHR & _
              " group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) " & _
              " order by PURTH.TH004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp") '
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Stock
        Dim whList As String = ""
        Select Case CodeType
            Case "1"
                whList = " and INVMC.MC002 in ('2101','2201','2202','2204','2205','2206','3333','2900','2901') "
            Case "2"
                whList = " and INVMC.MC002 not in ('8888','9999','2600','2700') "
            Case "3"
                whList = " and INVMC.MC002 not in ('8888','9999','2600','2700') "
            Case "4"
                whList = " and INVMC.MC002 in ('2101','2201','2202','2204','2205','2206','2900','2901') "
            Case "5"
                whList = " and INVMC.MC002 in ('4100') "
            Case "6"
                whList = " and INVMC.MC002 in ('4200') "
        End Select

        SQL = " select INVMC.MC001 as item,SUM(isnull(INVMC.MC007,0)) as stock, SUM(isnull(INVMC.MC004,0)) as saveStock from " & tempTable & " T left join  JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
            " where INVMC.MC007 >0 " & whList & " group by INVMC.MC001"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("stock")
            Dim sQty As Decimal = Program.Rows(i).Item("saveStock")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set stockQty='" & Qty & "',saveQty='" & sQty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,stockQty,saveQty)values ('" & item & "','" & Qty & "','" & sQty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'MOQ QTY
        Dim dateToday As String = DateTime.Now.ToString("yyyyMMdd")
        SQL = "select item from " & tempTable & " group by item"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim xcode As String = Program.Rows(i).Item("item")
            'Dim xsup As String = Program.Rows(i).Item("sup")
            SQL = " select top 1 PURTM.UDF01 from PURTM  " & _
                  "   left join PURTL on TL001=TM001 and TL002=TM002 " & _
                  "  where TL006='Y' and TM004='" & xcode.TrimEnd & "' " & _
                  "    and (TM015='' or TM015 <='" & dateToday & "' ) order by TL010 desc"
            Program1 = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            If Program1.Rows.Count > 0 Then
                Dim moq = Program1.Rows(0).Item("UDF01")
                If moq <> "" Then
                    USQL = " update " & tempTable & " set MoqQty='" & moq & "'  where item='" & xcode & "' "
                    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                End If
            End If
        Next
        'Show Data
        Dim strDemand As String = " T.issueQty+T.delQty  "
        Dim strSupply As String = " T.stockQty+T.moQty+T.poQty+T.poManQty+T.poForQty+T.poMoQty+T.prQty+T.poRcpQty "
        Dim strSupply1 As String = " T.stockQty+T.poRcpQty "
        Dim valConsel As String = ddlCondition.SelectedValue.ToString
        'WHR = strDemand & ">" & strSupply1 & " and " & strDemand & ">0  "
        WHR = ""
        If valConsel <> "0" Then
            WHR = " where "
            If valConsel = "1" Then   'Shortage
                WHR = WHR & " and " & strDemand & ">" & strSupply & " and " & strDemand & ">0  "
            ElseIf valConsel = "2" Then 'call In
                WHR = WHR & strDemand & ">" & strSupply1 & " and " & strDemand & ">0  "
            End If
        End If
        SQL = " select T.item as 'JP Part',INVMB.MB002+'-'+ INVMB.MB003 as 'JP SPEC',INVMB.MB004 as 'Unit'," & _
              " cast(case when (" & strSupply1 & ")-(" & strDemand & ")>=0 then '0' else (" & strSupply1 & ")-(" & strDemand & ") end as decimal(10,3)  ) as 'Call In', " & _
              " T.stockQty as 'Stock(+)',T.poRcpQty as 'PO Insp.(+)'," & _
              " T.poQty as 'PO(+)',T.poManQty as 'PO Manual(+)',T.poForQty as 'PO Forcast(+)',T.poMoQty as 'PO MO(+)', " & _
              " T.prQty as 'PR(+)',MoqQty as 'MOQ Qty',saveQty as 'Safe Stock'," & _
              " T.moQty as 'MO Rcv(+)',T.delQty as 'SO(-)',T.issueQty as 'MO Issue(-)', " & _
              " cast((" & strSupply & ")-(" & strDemand & ") as decimal(10,3)) as Shortage " & _
              " from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & _
              WHR & " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)

                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("JP Part")) Then
                    Dim link As String = ""
                    'Dim EndDate As String = configDate.dateFormat2(tbEndDate.Text)
                    Dim jpPart As String = .DataItem("JP Part")
                    link = link & "&JPPart= " & jpPart
                    link = link & "&JPSpec= " & .DataItem("JP SPEC")
                    link = link & "&Unit= " & .DataItem("Unit")
                    link = link & "&stock= " & .DataItem("Stock(+)")
                    link = link & "&poQty= " & .DataItem("PO(+)")
                    link = link & "&poManQty= " & .DataItem("PO Manual(+)")
                    link = link & "&poForQty= " & .DataItem("PO Forcast(+)")
                    link = link & "&poMoQty= " & .DataItem("PO MO(+)")
                    link = link & "&prQty= " & .DataItem("PR(+)")
                    link = link & "&moQty= " & .DataItem("MO Rcv(+)")
                    link = link & "&delQty= " & .DataItem("SO(-)")
                    link = link & "&issueQty= " & .DataItem("MO Issue(-)")
                    link = link & "&poRcpQty= " & .DataItem("PO Insp.(+)")
                    link = link & "&endDate= " & configDate.dateFormat2(tbDateTo.Text)
                    hplDetail.NavigateUrl = "MaterailsShortagePopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click

        Dim gv As GridView = gvPoCallIn
        If gvShow.Visible Then
            gv = gvShow
        ElseIf gvPlanCallIn.Visible Then
            gv = gvPlanCallIn
        End If

        ControlForm.ExportGridViewToExcel("MaterialsShortage" & Session("UserName"), gv)
    End Sub

    Protected Sub btPO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPOCallIn.Click

        gvPoCallIn.Visible = True
        gvShow.Visible = False
        gvPlanCallIn.Visible = False

        Dim tempTable As String = "tempMaterialsShortage" & Session("UserName")
        CreateTempTable.createTempMaterialShortage(tempTable)

        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = "",
            USQL As String = ""

        Dim CodeType As String = ddlCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text,
            Spec As String = tbSpec.Text,
            EndDate As String = configDate.dateFormat2(tbDateTo.Text),
            Program As New DataTable,
            Program1 As New DataTable,
            item As String = "",
            Qty As Integer = 0

        If CodeType <> "5" And CodeType <> "6" Then
            'MO issue mat 
            WHR = configDate.DateWhere("MOCTB.TB015", "", EndDate)
            If CodeType <> "0" Then
                WHR = WHR & " and substring(MOCTB.TB003,3,1) = '" & CodeType & "' "
            End If
            If Code <> "" Then
                WHR = WHR & " and MOCTB.TB003 like '%" & Code & "%' "
            End If
            If Spec <> "" Then
                WHR = WHR & " and MOCTB.TB013 like '%" & Spec & "%' "
            End If

            SQL = " select MOCTB.TB003,SUM(MOCTB.TB004-MOCTB.TB005) from  JINPAO80.dbo.MOCTB MOCTB " & _
                  " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " & _
                  " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & WHR & _
                  " group by MOCTB.TB003 "
            ISQL = "insert into " & tempTable & "(item,issueQty) " & SQL
            Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

            'Sale Order
            WHR = configDate.DateWhere("COPTD.TD013", "", EndDate)
            If CodeType <> "0" Then
                WHR = WHR & " and substring(COPTD.TD004,3,1) = '" & CodeType & "' "
            End If
            If Code <> "" Then
                WHR = WHR & " and COPTD.TD004 like '%" & Code & "%' "
            End If
            If Spec <> "" Then
                WHR = WHR & " and (COPTD.TD006 like '%" & Spec & "%' or COPTD.TD005 like '%" & Spec & "%') "
            End If
            SQL = " select COPTD.TD004 as item,SUM(COPTD.TD008-COPTD.TD009) as so from COPTD " & _
                  " left join  COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                  " where COPTD.TD016='N' " & WHR & " group by COPTD.TD004  order by COPTD.TD004 "
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                item = Program.Rows(i).Item("item")
                Qty = Program.Rows(i).Item("so")

                USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                       " update " & tempTable & " set delQty='" & Qty & "' where item='" & item & "' else " & _
                       " insert into " & tempTable & "(item,delQty)values ('" & item & "','" & Qty & "')"
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)


            Next
        End If

        'purchase receipt inspection
        WHR = configDate.DateWhere("PURTG.TG014", "", EndDate)
        If CodeType <> "0" Then
            If CodeType <> "5" And CodeType <> "6" Then
                WHR = WHR & " and substring(PURTH.TH004,3,1) = '" & CodeType & "' "
            End If
            Select Case CodeType
                Case "5" '1401
                    WHR = WHR & " and INVMB.MB005 = '1401' "
                Case "6" '1402
                    WHR = WHR & " and INVMB.MB005 = '1402' "
                Case Else
                    WHR = WHR & " and INVMB.MB005 not in ('1401','1402') "
            End Select
        End If
        If Code <> "" Then
            WHR = WHR & " and PURTH.TH004 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and (PURTH.TH005 like '%" & Spec & "%' or PURTH.TH006 like '%" & Spec & "%') "
        End If
        SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH007,0)) as po_insp from PURTH " & _
              " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " & _
              " left join INVMB on INVMB.MB001=PURTH.TH004 " & _
              " where PURTG.TG013 = 'N' " & WHR & " group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) " & _
              " order by PURTH.TH004 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1

            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp") '
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Stock
        Dim whList As String = ""
        Select Case CodeType
            Case "1"
                whList = " and INVMC.MC002 in ('2101','2201','2202','2204','2205','2206','2900','2901','3333') "
            Case "2"
                whList = " and INVMC.MC002 not in ('8888','9999','2600','2700') "
            Case "3"
                whList = " and INVMC.MC002 not in ('8888','9999','2600','2700') "
            Case "4"
                whList = " and INVMC.MC002 in ('2101','2201','2202','2204','2205','2206','2900','2901') "
            Case "5"
                whList = " and INVMC.MC002 in ('4100') "
            Case "6"
                whList = " and INVMC.MC002 in ('4200') "
        End Select

        SQL = " select INVMC.MC001 as item,SUM(isnull(INVMC.MC007,0)) as stock, SUM(isnull(INVMC.MC004,0)) as saveStock from " & tempTable & " T left join  JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
            " where INVMC.MC007 >0 " & whList & " group by INVMC.MC001"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("stock")
            Dim sQty As Decimal = Program.Rows(i).Item("saveStock")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set stockQty='" & Qty & "',saveQty='" & sQty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,stockQty,saveQty)values ('" & item & "','" & Qty & "','" & sQty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Show Data
        Dim strDemand As String = " T.issueQty+T.delQty  "
        Dim strSupply As String = " T.stockQty+T.moQty+T.poQty+T.poManQty+T.poForQty+T.poMoQty+T.prQty+T.poRcpQty "
        Dim strSupply1 As String = " T.stockQty+T.poRcpQty "
        Dim valConsel As String = ddlCondition.SelectedValue.ToString
        WHR = ""
        If valConsel <> "0" Then
            WHR = " where "
            If valConsel = "1" Then   'Shortage
                WHR = WHR & strDemand & ">" & strSupply & " and " & strDemand & ">0  "
            ElseIf valConsel = "2" Then 'call In
                WHR = WHR & strDemand & ">" & strSupply1 & " and " & strDemand & ">0  "
            End If
        End If
        'SQL = " select T.item as 'JP Part',INVMB.MB002+'-'+ INVMB.MB003 as 'JP SPEC',INVMB.MB004 as 'Unit'," & _
        '      " cast((" & strSupply1 & ")-(" & strDemand & ") as decimal(10,3)) as 'Call In', " & _
        '      " T.stockQty as 'Stock(+)',T.poRcpQty as 'PO Insp.(+)'," & _
        '      " T.poQty as 'PO(+)',T.poManQty as 'PO Manual(+)',T.poForQty as 'PO Forcast(+)',T.poMoQty as 'PO MO(+)', " & _
        '      " T.prQty as 'PR(+)',MoqQty as 'MOQ Qty',saveQty as 'Safe Stock'," & _
        '      " T.moQty as 'MO Rcv(+)',T.delQty as 'SO(-)',T.issueQty as 'MO Issue(-)', " & _
        '      " cast((" & strSupply & ")-(" & strDemand & ") as decimal(10,3)) as Shortage " & _
        '      " from " & tempTable & " T " & _
        '      " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & WHR & _
        '      " order by T.item "

        SQL = " select T.item as 'Item',PURTD.TD005 as 'Desc',PURTD.TD006 as 'Spec', " & _
              " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as 'PO Number.'," & _
              " PURTC.TC004 as 'Supplier',PURMA.MA002 as 'Supplier Name'," & _
              " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as 'Plan Delivery Date'," & _
              " cast(PURTD.TD008-PURTD.TD015 as decimal(15,3)) as 'PO Bal', " & _
              " cast((T.stockQty+T.poRcpQty)-(T.issueQty+T.delQty) as decimal(15,3)) as 'Call In' " & _
              " from " & tempTable & " T " & _
              " left join JINPAO80.dbo.PURTD PURTD on PURTD.TD004=T.item" & _
              " left join JINPAO80.dbo.PURTC PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " & _
              " left join JINPAO80.dbo.PURMA PURMA on PURMA.MA001=PURTC.TC004 " & _
              " where PURTD.TD016='N' and T.issueQty+T.delQty>T.stockQty+T.poRcpQty and T.issueQty+T.delQty>0 " & _
              " order by PURTD.TD004,PURTD.TD001,PURTD.TD002,PURTD.TD003 "

        ControlForm.ShowGridView(gvPoCallIn, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvPoCallIn)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)


    End Sub

    Protected Sub btPlanCallIn_Click(sender As Object, e As EventArgs) Handles btPlanCallIn.Click

        gvPlanCallIn.Visible = True
        gvShow.Visible = False
        gvPoCallIn.Visible = False


        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = "",
            USQL As String = ""

        Dim CodeType As String = ddlCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text,
            Spec As String = tbSpec.Text,
            Program As New DataTable,
            Program1 As New DataTable,
            item As String = "",
            Qty As Integer = 0

        'EndDate As String = configDate.dateFormat2(tbDateTo.Text),

        Dim TempTable As String = "TempPlanCallIn" & Session("UserName")
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = returnDate(tbDateFrom.Text.Trim) 'Begin date
        Dim endDate As String = returnDate(tbDateTo.Text.Trim) 'End date

        Dim xd As String = ""
        Dim xm As String = ""

        Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim dateWork As Short = DateDiff(DateInterval.Day, beginDate, lastDate)
        CreateTempTable.createTempPlanCallIn(TempTable, beginDate, dateWork)
        'MO issue Mat
        If CodeType <> "0" Then
            WHR = WHR & " and substring(MOCTB.TB003,3,1) = '" & CodeType & "' "
        End If
        If Code <> "" Then
            WHR = WHR & " and MOCTB.TB003 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and MOCTB.TB013 like '%" & Spec & "%' "
        End If
        'before select 
        Dim whrDate As String = ""
        whrDate = " and MOCTB.TB015 <'" & strDate & "'"
        'configDate.DateWhere("MOCTB.TB015", "", endDate)

        SQL = " select MOCTB.TB003,SUM(MOCTB.TB004-MOCTB.TB005) from  JINPAO80.dbo.MOCTB MOCTB " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " & _
              " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & _
              WHR & whrDate & _
              " group by MOCTB.TB003 "
        ISQL = "insert into " & TempTable & "(item,issueQty) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        ' in range select
        whrDate = configDate.DateWhere("MOCTB.TB015", strDate, endDate)
        SQL = " select MOCTB.TB003 as item,MOCTB.TB015 as issueDate,SUM(MOCTB.TB004-MOCTB.TB005) as issueQty from  JINPAO80.dbo.MOCTB MOCTB " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " & _
              " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & _
              WHR & whrDate & _
              " group by MOCTB.TB003,MOCTB.TB015 order by MOCTB.TB003,MOCTB.TB015 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim lastItem As String = "",
                selDate As String = ""
        Dim fldInsHash As Hashtable = New Hashtable,
            whrHash As Hashtable = New Hashtable,
            fldUpdHash As Hashtable = New Hashtable

        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("issueQty")
            selDate = Program.Rows(i).Item("issueDate")
            If lastItem <> item Then
                If lastItem <> "" Then
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(TempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                End If
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                whrHash.Add("item", item)
            End If
            Dim fld As String = "issueQty" & selDate
            fldInsHash.Add(fld, Qty)
            fldUpdHash.Add(fld, fld & "+" & Qty)
            lastItem = item
        Next
        If Program.Rows.Count > 0 Then
            Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(TempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
        End If
        'purchase receipt inspection
        WHR = configDate.DateWhere("PURTG.TG014", "", endDate)
        If CodeType <> "0" Then
            If CodeType <> "5" And CodeType <> "6" Then
                WHR = WHR & " and substring(PURTH.TH004,3,1) = '" & CodeType & "' "
            End If
            Select Case CodeType
                Case "5" '1401
                    WHR = WHR & " and INVMB.MB005 = '1401' "
                Case "6" '1402
                    WHR = WHR & " and INVMB.MB005 = '1402' "
                Case Else
                    WHR = WHR & " and INVMB.MB005 not in ('1401','1402') "
            End Select
        End If
        If Code <> "" Then
            WHR = WHR & " and PURTH.TH004 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and (PURTH.TH005 like '%" & Spec & "%' or PURTH.TH006 like '%" & Spec & "%') "
        End If
        SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH007,0)) as po_insp from PURTH " & _
              " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " & _
              " left join INVMB on INVMB.MB001=PURTH.TH004 " & _
              " where PURTG.TG013 = 'N' " & WHR & " group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) " & _
              " order by PURTH.TH004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp") '
            USQL = " if exists(select * from " & TempTable & " where item='" & item & "' ) " & _
                   " update " & TempTable & " set poRcpQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & TempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Stock
        Dim whList As String = ""
        Select Case CodeType
            Case "1"
                whList = " and INVMC.MC002 in ('2101','2201','2202','2204','2205','2206','2900','2901','3333') "
            Case "2"
                whList = " and INVMC.MC002 not in ('8888','9999','2600','2700') "
            Case "3"
                whList = " and INVMC.MC002 not in ('8888','9999','2600','2700') "
            Case "4"
                whList = " and INVMC.MC002 in ('2101','2201','2202','2204','2205','2206','2900','2901') "
            Case "5"
                whList = " and INVMC.MC002 in ('4100') "
            Case "6"
                whList = " and INVMC.MC002 in ('4200') "
        End Select

        SQL = " select INVMC.MC001 as item,SUM(isnull(INVMC.MC007,0)) as stock from " & TempTable & " T left join  JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
            " where INVMC.MC007 >0 " & whList & " group by INVMC.MC001"
        Program = Conn_SQL.Get_DataReader(Sql, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("stock")
            USQL = " if exists(select * from " & TempTable & " where item='" & item & "' ) " & _
                   " update " & TempTable & " set stockQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & TempTable & "(item,stockQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Show Data
        'SQL = "select * from " & TempTable
        WHR = ""
        Dim fldSupply As String = "(T.stockQty+T.poRcpQty)"
        Dim fldIssue As String = "T.issueQty"
        Dim fldShow As String = ",T.issueQty as 'Before Issue' " 'cast(case when " & fldSupply & "- T.issueQty>=0 then '0' else " & fldSupply & "- T.issueQty end as varchar) as 'Before Call In'
        For i As Integer = 0 To dateWork
            Dim tdate As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim sdate As String = beginDate.AddDays(i).ToString(" dd/MM", System.Globalization.CultureInfo.InvariantCulture)
            Dim fld As String = "T.issueQty" & tdate
            fldIssue = fldIssue & "+" & fld
            fldShow = fldShow & "," & fld & " as 'Q" & sdate & "' ,cast(case when " & fldSupply & "- (" & fldIssue & ")>=0 then '0' else " & fldSupply & "- (" & fldIssue & ") end as varchar) as 'C" & sdate & "' "
        Next
        'WHR = WHR & fldSupply & "- (" & fldIssue & ")< 0 or "
        fldShow = fldShow & "," & fldIssue & " as 'Issue Qty Sum' ,cast(case when " & fldSupply & "- (" & fldIssue & ")>=0 then '0' else " & fldSupply & "- (" & fldIssue & ") end as varchar) as 'Call In' "

        SQL = " select T.item as 'JP Part',INVMB.MB002+'-'+ INVMB.MB003 as 'JP SPEC'," & _
              " INVMB.MB004 as 'Unit',T.stockQty as 'Stock(+)',T.poRcpQty as 'PO Insp.(+)' " & fldShow & _
              " from " & TempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & _
              " where " & fldSupply & "- (" & fldIssue & ")< 0  order by T.item "

        'SQL = " select T.item as 'JP Part',INVMB.MB002+'-'+ INVMB.MB003 as 'JP SPEC'," & _
        '      " INVMB.MB004 as 'Unit',T.stockQty as 'Stock(+)',T.poRcpQty as 'PO Insp.(+)' " & fldShow & _
        '      " from " & TempTable & " T " & _
        '      " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item  order by T.item "

        ControlForm.ShowGridView(gvPlanCallIn, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvPlanCallIn)

    End Sub

    Private Sub gvPlanCallIn_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlanCallIn.RowDataBound
        With e.Row
            Dim fDate As String = returnDate(tbDateFrom.Text.Trim) 'Begin date
            Dim tDate As String = returnDate(tbDateTo.Text.Trim) 'End date
            If .RowType = DataControlRowType.DataRow Then
                Dim item As String = .Cells(0).Text.Trim
                With .Cells(5)
                    If CDec(.Text.Trim) > 0 Then
                        .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                        .Attributes.Add("onclick", "NewWindow('MaterailsShortageMoList.aspx?item=" & item & "&fdate=&tdate=" & tDate & "&addDate=-1','moList',800,500,'yes')")
                    End If
                End With

                Dim j As Integer = 0
                For i As Decimal = 6 To gvPlanCallIn.HeaderRow.Cells.Count - 3 Step 2
                    With .Cells(i)
                        Dim qty As Decimal = CDec(.Text.Trim)
                        If qty > 0 Then
                            .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                            .Attributes.Add("onclick", "NewWindow('MaterailsShortageMoList.aspx?item=" & item & "&fdate=" & fDate & "&tdate=&addDate=" & j & "','moList',800,500,'yes')")
                            j = j + 1
                        End If
                    End With
                Next
                With .Cells(gvPlanCallIn.HeaderRow.Cells.Count - 2)
                    If CDec(.Text.Trim) > 0 Then
                        .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                        .Attributes.Add("onclick", "NewWindow('MaterailsShortageMoList.aspx?item=" & item & "&fdate=" & fDate & "&tdate=" & tDate & "&addDate=0','moList',800,500,'yes')")
                    End If
                End With
            End If
        End With

    End Sub
    Private Function returnDate(dateVal As String) As String
        Dim dateToday As Date = DateTime.Today,
            strDate As String = "",
            xd As String = "",
            xm As String = ""
        If dateVal <> "" Then
            strDate = configDate.dateFormat2(dateVal)
        Else
            xd = dateToday.Day
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = dateToday.Month
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            strDate = dateToday.Year & xm & xd
        End If
        Return strDate
    End Function
End Class