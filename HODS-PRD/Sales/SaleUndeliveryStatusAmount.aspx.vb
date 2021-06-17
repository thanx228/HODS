Imports System.IO
Public Class SaleUndeliveryStatusAmount
    Inherits System.Web.UI.Page
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 3, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempPlanDelivery" & Session("UserName")
        CreateTempTable.createTempPlanDelivery(tempTable)
        Dim SQL As String = "",
            ISQL As String = "",
            USQL As String = "",
            having As String = "",
            whr As String = "",
            whr2 As String = "",
            item As String = "",
            qty As Integer = 0,
            Program As New DataTable

        whr = whr & Conn_SQL.Where("COPTD.TD001", cblSaleType)
        whr = whr & Conn_SQL.Where("COPTD.TD002", tbSO, False)
        whr = whr & Conn_SQL.Where("COPTD.TD003", tbSoSeq, False)
        whr = whr & Conn_SQL.Where("COPTC.TC004", tbCustId, False)
        whr = whr & Conn_SQL.Where("COPTD.TD004 ", tbCode)
        whr = whr & Conn_SQL.Where("COPTD.TD006", tbSpec)
        whr = whr & Conn_SQL.Where("COPTD.TD013", ConfigDate.dateFormat2(txtFromDueDate.Text), ConfigDate.dateFormat2(tbDueDate.Text))
        SQL = " select COPTD.TD004,SUM(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025)," & _
              "       SUM((COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025)*COPTD.TD011*COPTC.TC009) from JINPAO80.dbo.COPTD COPTD  " & _
              " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
              " where COPTD.TD016='N' and COPTC.TC027='Y' " & whr & " group by COPTD.TD004 order by COPTD.TD004"
  
        ISQL = "insert into " & tempTable & "(item,delQty,delAmt) " & SQL ',SoTypeNo
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        'MO 
        SQL = " select A.TA006 as item, SUM(isnull(A.TA015,0)-isnull(A.TA017,0)-isnull(A.TA018,0)) as mo from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.MOCTA A on A.TA006=T.item where A.TA011 not in('y','Y') and A.TA013 ='Y'  " & _
              " group by TA006  order by TA006"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("mo")

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
        'SQL = " select T.item as 'item',sum(INVMC.MC007) as 'stock',sum(INVMC.MC007*INVMB.MB050) as 'stockAmt' from " & tempTable & " T " & _
        '      " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
        '      " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=INVMC.MC001 " & _
        '      " where MC002 not in('2600','2700','2800') " & _
        '      " group by T.item order by T.item "

        SQL = " select T.item as 'item',sum(INVLA.LA005*INVLA.LA011) as 'stock' " & _
              " from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVLA INVLA on INVLA.LA001=T.item  " & _
              " where INVLA.LA004 <='" & ConfigDate.dateFormat2(tbDueDate.Text) & "'  " & _
              " and INVLA.LA009 not in ('2600','2700','2800') " & _
              " group by T.item having sum(INVLA.LA005*INVLA.LA011)>0  " &
              " order by T.item "

        whr = Conn_SQL.Where("COPTD.TD001", cblSaleType)
        whr = whr & Conn_SQL.Where("COPTD.TD002", tbSO, False)
        whr = whr & Conn_SQL.Where("COPTD.TD003", tbSoSeq, False)
        whr = whr & Conn_SQL.Where("COPTC.TC004", tbCustId, False)
        whr = whr & Conn_SQL.Where("COPTD.TD013", "", ConfigDate.dateFormat2(tbDueDate.Text))

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Dim amt As Decimal = 0,
            prc As Decimal = 1
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("stock")

            Dim sSQL As String = " "
            sSQL = " select top 1 " & Program.Rows(i).Item("stock") & "*COPTD.TD011*COPTC.TC009 as amt from COPTD  " & _
                   " left join COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                   " where COPTC.TC027='Y' and COPTD.TD004='" & item & "' " & whr & _
                   " order by COPTC.TC003 desc "
            Dim sProgram As New DataTable
            sProgram = Conn_SQL.Get_DataReader(sSQL, Conn_SQL.ERP_ConnectionString)
            amt = Program.Rows(i).Item("stock")
            If sProgram.Rows.Count > 0 Then
                amt = sProgram.Rows(0).Item("amt")
            End If
            'amt = Program.Rows(i).Item("stock") * prc
            'Dim aaa As String = "SELECT SUM(ML005) FROM JINPAO80.dbo.INVML WHERE ML001 ='" & item & "' AND ML002 not in('2600','2700','2800') "
            'Dim SQty As Integer = Conn_SQL.Get_value(aaa, Conn_SQL.ERP_ConnectionString)
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set stockQty='" & qty & "',stockAmt='" & amt & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,stockQty,stockAmt)values ('" & item & "','" & qty & "','" & amt & "')"
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
              " T.delQty as 'Undelivery Qty',T.delAmt as 'Undelivery Amt.'," & _
              " T.stockQty as 'Stock Qty',T.stockAmt as 'Stock Amt.'," & _
              " T.moQty as 'MO Qty',T.poQty as 'PO Qty',T.prQty as 'PR Qty' from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & whr & _
              " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
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
                    hplDetail.NavigateUrl = "SaleUndeliveryStatusPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", code & " - " & spec)
                End If
            End If
        End With
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SaleUndeliveryStatus" & Session("UserName"), gvShow)
    End Sub
End Class