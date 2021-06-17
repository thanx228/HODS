Public Class SaleOrderProductStatus
    Inherits System.Web.UI.Page
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate
    Dim saleList As String = "'2201','2202','2203','2204','2205','2213'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' and MQ001 in (" & saleList & ") order by MQ002"
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempProductStatus" & Session("UserName")
        CreateTempTable.createTempProductStatus(tempTable)
        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = "",
            USQL As String = ""
        Dim Program As New DataTable
        Dim item As String = "",
            qty As Integer = 0

        'sale order 
        WHR = WHR & Conn_SQL.Where("COPTD.TD001", cblSaleType)
        WHR = WHR & Conn_SQL.Where("COPTC.TC004", tbCust, False)
        WHR = WHR & Conn_SQL.Where("COPTD.TD004 ", tbItem)
        If ddlCodeType.Text <> "0" Then
            WHR = WHR & Conn_SQL.Where("substring(COPTD.TD004,3,1)", ddlCodeType)
        End If
        WHR = WHR & Conn_SQL.Where("COPTD.TD006", tbSpec)
        WHR = WHR & Conn_SQL.Where("COPTD.TD013", ConfigDate.dateFormat2(tbDateFrom.Text), ConfigDate.dateFormat2(tbDateTo.Text))

        SQL = " select COPTC.TC004,COPTD.TD004,sum(COPTD.TD009+COPTD.TD025),SUM(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025) from JINPAO80.dbo.COPTD COPTD  " & _
             " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
             " where COPTD.TD016='N' and COPTC.TC027='Y' " & WHR & " group by  COPTC.TC004,COPTD.TD004 "
        ISQL = "insert into " & tempTable & "(cust,item,delQty,undelQty) " & SQL
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
        'stock
        SQL = " select T.item as 'item',sum(INVMC.MC007) as 'stock' from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
              " where MC002 not in('2600','2700','2800') " & _
              " group by T.item order by T.item "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("stock")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set stockQty='" & qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,stockQty)values ('" & item & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'show
        SQL = " select T.cust as 'Cust ID',T.item as 'Item',INVMB.MB003 as 'Spec',T.stockQty as 'Stock Qty'," & _
              " T.delQty as 'Delivery Qty',T.undelQty as 'Undelivery Qty',T.moQty as 'MO Qty'" & _
              "  from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & _
              " order by T.cust,T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True

    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                Dim code As String = .DataItem("Item").ToString.Replace("Null", "")
                Dim spec As String = .DataItem("Spec").ToString.Replace("Null", "")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Item")) Then

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
                    link = link & "&custID=" & .DataItem("Cust ID")
                    link = link & "&item=" & code
                    link = link & "&strDate=" & ConfigDate.dateFormat2(tbDateFrom.Text)
                    link = link & "&endDate=" & ConfigDate.dateFormat2(tbDateTo.Text)
                    link = link & "&spec=" & spec
                    link = link & "&stockQty=" & .DataItem("Stock Qty")
                    link = link & "&undelQty=" & .DataItem("Undelivery Qty")
                    link = link & "&moQty=" & .DataItem("MO Qty")
                    hplDetail.NavigateUrl = "SaleOrderProductStatusPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", code & " - " & spec)
                End If
            End If
        End With
    End Sub
End Class