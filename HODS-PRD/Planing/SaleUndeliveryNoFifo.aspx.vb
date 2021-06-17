Imports System.IO
Public Class SaleUndeliveryNoFifo
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempPlanDelivery" & Session("UserName")
        CreateTempTable.createTempPlanDelivery(tempTable)
        Dim SQL As String = "", ISQL As String = "", USQL As String = ""
        Dim having As String = "", whr As String = "", whr2 As String = ""
        Dim Program As New DataTable
        Dim item As String = "", qty As Integer = 0
        'get data from form input data
        'Dim saleType As String = ""
        'Dim custID As String = tbCustId.Text
        'Dim soNo As String = tbSO.Text
        'Dim soSeq As String = tbSoSeq.Text
        'Dim code As String = tbCode.Text
        'Dim spec As String = tbSpec.Text
        'Dim FromDate As String = ConfigDate.dateFormat2(txtFromDueDate.Text)
        'Dim endDate As String = ConfigDate.dateFormat2(tbDueDate.Text)


        whr = whr & Conn_SQL.Where("COPTD.TD001", cblSaleType)
        whr2 = whr2 & Conn_SQL.Where("COPTD2.TD001", cblSaleType)

        'If saleType <> "" And saleType <> "ALL" Then
        '    whr = whr & " and COPTD.TD001='" & saleType & "' "
        'End If

        whr = whr & Conn_SQL.Where("COPTD.TD002", tbSO, False)
        whr2 = whr2 & Conn_SQL.Where("COPTD2.TD002", tbSO, False)

        'If soNo <> "" Then
        '    whr = whr & " and COPTD.TD002='" & soNo & "' "
        'End If
        whr = whr & Conn_SQL.Where("COPTD.TD003", tbSoSeq, False)
        whr2 = whr2 & Conn_SQL.Where("COPTD2.TD003", tbSoSeq, False)
        'If soSeq <> "" Then
        '    whr = whr & " and COPTD.TD003='" & soSeq & "' "
        'End If
        whr = whr & Conn_SQL.Where("COPTC.TC004", tbCustId, False)
        whr2 = whr2 & Conn_SQL.Where("COPTC2.TC004", tbCustId, False)
        'If custID <> "" Then
        '    whr = whr & " and COPTC.TC004='" & custID & "' "
        'End If
        whr = whr & Conn_SQL.Where("COPTD.TD004 ", tbCode)
        whr2 = whr2 & Conn_SQL.Where("COPTD2.TD004 ", tbCode)
        'If code <> "" Then
        '    whr = whr & " and COPTD.TD004 like '%" & code & "%' "
        'End If
        whr = whr & Conn_SQL.Where("COPTD.TD006", tbSpec)
        whr2 = whr2 & Conn_SQL.Where("COPTD2.TD006", tbSpec)
        'If spec <> "" Then
        '    whr = whr & " and COPTD.TD006 like '%" & spec & "%' "
        'End If

        'If FromDate <> "" And endDate <> "" Then
        '    whr = whr & " and COPTD.TD013 between '" & FromDate & "' and '" & endDate & "' "
        'End If

        whr = whr & Conn_SQL.Where("COPTD.TD013", ConfigDate.dateFormat2(txtFromDueDate.Text), ConfigDate.dateFormat2(tbDueDate.Text))
        whr2 = whr2 & Conn_SQL.Where("COPTD2.TD013", ConfigDate.dateFormat2(txtFromDueDate.Text), ConfigDate.dateFormat2(tbDueDate.Text))
        'whr = whr & ConfigDate.DateWhere("COPTD.TD013", FromDate, endDate)
        'If endDate <> "" Then
        '    whr = whr & " and COPTD.TD013<='" & endDate & "' "
        'End If
        Dim sSQL As String = ""
        sSQL = " select min(COPTD2.TD013)  from JINPAO80.dbo.COPTD COPTD2 left join JINPAO80.dbo.COPTC COPTC2 on COPTC2.TC001=COPTD2.TD001 and COPTC2.TC002=COPTD2.TD002 " & _
                " where COPTD2.TD016='N' and COPTC2.TC027='Y' and COPTD2.TD008+COPTD2.TD024-COPTD2.TD009-COPTD2.TD025> 0  and COPTD2.TD004 = COPTD.TD004 " & whr2 & _
                " group by COPTD2.TD004 "
        'Sale order detail and COPTD.TD009+COPTD.TD025> 0 COPTD.TD016='N' and
        SQL = " select COPTD.TD004,SUM(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025),(" & sSQL & ") from JINPAO80.dbo.COPTD COPTD  " & _
             " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
             " where  COPTC.TC027='Y' and COPTD.TD009+COPTD.TD025> 0 and COPTD.TD013>=(" & sSQL & ")  " & whr & _
             " group by COPTD.TD004 having count(COPTD.TD004)>1 and SUM(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025)>0 order by COPTD.TD004"
        ' Dim TA As New DataTable  ,COPTD.TD001+'-'+COPTD.TD002 as TypeNo 
        ' TA = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        ' For i As Integer = 0 To TA.Rows.Count - 1
        'TextBox1.Text = Program.Rows(i).Item("COPTD.TD004")
        'Next
        ISQL = "insert into " & tempTable & "(item,delQty,dateStr) " & SQL ',SoTypeNo
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        'get cust ID
        Dim txt As String = "",
            lstCode As String = ""
        SQL = " select COPTD.TD004,COPTC.TC004,SUM(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025) from JINPAO80.dbo.COPTD COPTD  " & _
             " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
             " where COPTD.TD016='N' and COPTC.TC027='Y' and COPTD.TD009+COPTD.TD025> 0  " & whr & _
             " group by COPTD.TD004,COPTC.TC004 having count(COPTD.TD004)>1 order by COPTD.TD004,COPTC.TC004"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("TD004")
            Dim cust As String = Program.Rows(i).Item("TC004")
            If lstCode <> item And lstCode <> "" Then
                USQL = " update " & tempTable & " set cust='" & txt.Substring(0, txt.Length - 1) & "' where item='" & lstCode & "' "
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                txt = ""
            End If
            txt = txt & cust & ","
            lstCode = item
        Next
        If txt <> "" Then
            USQL = " update " & tempTable & " set cust='" & txt.Substring(0, txt.Length - 1) & "' where item='" & lstCode & "' "
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        End If

        'MO 
        SQL = " select A.TA006 as item, SUM(isnull(A.TA015,0)-isnull(A.TA017,0)-isnull(A.TA018,0)) as mo from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.MOCTA A on A.TA006=T.item where A.TA011 not in('y','Y') and A.TA013 ='Y'  " & _
              " group by TA006  order by TA006"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("mo")

            'Dim aaa As String = "select SUM(isnull(A.TA015,0)-isnull(A.TA017,0)-isnull(A.TA018,0)) as mo from JINPAO80.dbo.MOCTA A where A.TA011 not in('y','Y') and A.TA013 ='Y' and A.TA006 = '" & item & "'"
            'Dim MOQty As Integer = Conn_SQL.Get_value(aaa, Conn_SQL.ERP_ConnectionString)

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set moQty='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'PO
        SQL = " select D.TD004 as item,SUM(isnull(D.TD008,0)-isnull(D.TD015,0)) as po from  DBMIS.dbo." & tempTable & " T " & _
               " left join JINPAO80.dbo.PURTD D on D.TD004=T.item where D.TD016='N'  " & _
               " group by D.TD004  order by D.TD004 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set poQty='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,poQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'PR
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.PURTB B on B.TB004=T.item " & _
              " left join JINPAO80.dbo.PURTR R on R.TR001=B.TB001 and R.TR002=B.TB002 and R.TR003=B.TB003 " & _
              " where R.TR019='' and B.TB039='N' group by B.TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set prQty='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Stock
        SQL = " select T.item as 'item',sum(INVMC.MC007) as 'stock' from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
              " where MC002 not in('2600','2700','2800') " & _
              " group by T.item order by T.item "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("stock")
            'Dim aaa As String = "SELECT SUM(ML005) FROM JINPAO80.dbo.INVML WHERE ML001 ='" & item & "' AND ML002 not in('2600','2700','2800') "
            'Dim SQty As Integer = Conn_SQL.Get_value(aaa, Conn_SQL.ERP_ConnectionString)

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set stockQty='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,stockQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        whr = ""
        Dim condition As String = ddlCondition.Text
        If condition <> "0" Then
            whr = " where "
            Dim stock As String = "T.stockQty"
            Dim supply As String = stock & "+T.moQty+T.poQty+T.prQty"
            Dim undel As String = "T.delQty"
            Select Case condition
                Case "1" ' stock >= undelivery
                    whr = whr & stock & "<" & undel
                Case "2" ' supply >= undelivery
                    whr = whr & supply & ">=" & undel
                Case "3" ' supply < undelivery
                    whr = whr & supply & "<" & undel
                Case "4" ' supply < undelivery
                    whr = whr & stock & ">=" & undel
            End Select
        End If
        'SQL = " select T.SoTypeNo,T.item as 'JP Part No',INVMB.MB002 as 'JP Desc', INVMB.MB003 as 'JP SPEC', T.delQty as 'Undelivery Qty'," & _
        '      " T.stockQty as 'Stock Qty',T.moQty as 'MO Qty',T.poQty as 'PO Qty',T.prQty as 'PR Qty' from " & tempTable & " T " & _
        '      " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & whr & _
        '      " order by T.item"
        SQL = " select T.item as 'JP Part No',INVMB.MB002 as 'JP Desc', INVMB.MB003 as 'JP SPEC'," & _
              " T.delQty as 'Undelivery Qty',T.stockQty as 'Stock Qty'," & _
              " T.moQty as 'MO Qty',T.poQty as 'PO Qty',T.prQty as 'PR Qty',T.cust as 'Cust',T.dateStr as 'Check Date' from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & whr & _
              " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                Dim code As String = .DataItem("JP Part No").ToString.Replace("Null", "")
                Dim spec As String = .DataItem("JP SPEC").ToString.Replace("Null", "")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("JP Part No")) Then

                    Dim link As String = "",
                        ordType As String = "",
                        cnt As Integer = 0

                    'If ddlSaleType.Text <> "ALL" Then
                    '    ordType = ddlSaleType.Text
                    'End If
                    For Each boxItem As ListItem In cblSaleType.Items
                        If boxItem.Selected = True Then
                            ordType = ordType & "'" & CStr(boxItem.Value) & "',"
                            cnt = cnt + 1
                        End If
                    Next
                    If cnt > 0 Then
                        ordType = ordType.Substring(0, ordType.Length - 1)
                    End If
                    link = link & "&saleType=" & ordType
                    link = link & "&saleNo=" & tbSO.Text
                    link = link & "&saleSeq=" & tbSoSeq.Text
                    link = link & "&custID=" & tbCustId.Text
                    link = link & "&endDate=" & ConfigDate.dateFormat2(tbDueDate.Text)
                    link = link & "&JPPart=" & code
                    link = link & "&JPSpec=" & spec
                    link = link & "&stock=" & .DataItem("Stock Qty")
                    link = link & "&delQty=" & .DataItem("Undelivery Qty")
                    link = link & "&poQty=" & .DataItem("PO Qty")
                    link = link & "&moQty=" & .DataItem("MO Qty")
                    link = link & "&prQty=" & .DataItem("PR Qty")
                    link = link & "&strDate=" & .DataItem("Check Date")
                    hplDetail.NavigateUrl = "SaleUndeliveryNoFifoPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", code & " - " & spec)
                End If
            End If
        End With
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SaleUndeliveryStatus" & Session("UserName"), gvShow)
    End Sub
End Class