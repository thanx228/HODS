Imports MIS_HTI.DataControl
Public Class ComparePrice
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim chrConn As String = Chr(8)

    Dim dbConn As New DataConnectControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            btPrint.Visible = False
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        If tbItem.Text.Trim = "" Or tbSup.Text.Trim = "" Then
            If tbItem.Text.Trim = "" Then
                tbItem.Focus()
                show_message.ShowMessage(Page, "Please Key Item No.", UpdatePanel1)
            Else
                tbSup.Focus()
                show_message.ShowMessage(Page, "Please Key Vender Code", UpdatePanel1)
            End If
        Else
            Dim tempTableMOQ As String = "tempPRMOQ" & Session("UserName")
            Dim tempTableMOQShow As String = "tempPRMOQShow" & Session("UserName")
            Dim WHR As String = "",
                SQL As String = "",
                Program As New DataTable,
                item As String = "",
                USQL As String = ""

            WHR = ""
            WHR = WHR & Conn_SQL.Where("MC001", tbItem)
            WHR = WHR & Conn_SQL.Where("MC002", tbSup)

            SQL = "select * from PURMC where 1=1 " & WHR
            'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            'have Qty and Price
            If Program.Rows.Count > 0 Then
                SQL = " select MC001 as 'item',MC002 as 'supplier',MC005 as 'qty',MC006 as 'price' ,MB015 as 'expdate', " &
               " isnull((select sum(PURTR.TR006) FROM HOOTHAI.dbo.PURTB LEFT JOIN PURTR on PURTR.TR001 = PURTB.TB001 " &
               " and PURTR.TR002 = PURTB.TB002  and PURTR.TR003 = PURTB.TB003 WHERE PURTB.TB004 = MC001 and PURTR.TR019 ='' and PURTB.TB039 = 'N' group by PURTB.TB006),'0') as 'sumQty' " &
               " from PURMC " &
               " left join PURMB on PURMB.MB001 = PURMC.MC001 and PURMB.MB002 = PURMC.MC002 and PURMB.MB014 = PURMC.MC008  " &
               " where PURMC.MC008 = (select MAX(PURMC.MC008) from PURMC where PURMC.MC001 = PURMB.MB001 and PURMB.MB002 = PURMC.MC002)  " & WHR
            Else
                WHR = ""
                WHR = WHR & Conn_SQL.Where("MB001", tbItem)
                WHR = WHR & Conn_SQL.Where("MB002", tbSup)
                SQL = " select top 1 MB001 as 'item',MB002 as 'supplier',1 as 'qty',MB011 as 'price',MB015 as 'expdate'," &
                    " isnull((select sum(PURTR.TR006) FROM HOOTHAI.dbo.PURTB LEFT JOIN PURTR on PURTR.TR001 = PURTB.TB001  and PURTR.TR002 = PURTB.TB002  and PURTR.TR003 = PURTB.TB003 " &
                    " WHERE PURTB.TB004 = MB001 and PURTR.TR019 ='' and PURTB.TB039 = 'N'  group by PURTB.TB006),'0') as 'sumQty' " &
                    " from PURMB" &
                    " where 1=1 " & WHR &
                    " order by MB014 DESC "
            End If

            'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            If Program.Rows.Count > 0 Then

                Dim count As Integer = 0
                'For i As Integer = 0 To Program.Rows.Count - 1
                '    count = Program.Rows.Count - 1
                'Next
                CreateTempTable.createTempPRMOQ(tempTableMOQ, Program.Rows.Count - 1)
                For i As Integer = 0 To Program.Rows.Count - 1
                    With Program.Rows(i)
                        USQL = " if exists(select * from " & tempTableMOQ & " where item='" & .Item("item") & "' ) " &
                         " update " & tempTableMOQ & " set supplier='" & .Item("supplier") & "' , expDate='" & .Item("expdate") & "'" &
                         " where item='" & .Item("item") & "' else " &
                         " insert into " & tempTableMOQ & "(item,supplier,expDate)values('" & .Item("item") & "','" & .Item("supplier") & "','" & .Item("expdate") & "')"
                        'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                    End With
                Next
                For i As Integer = 0 To Program.Rows.Count - 1
                    With Program.Rows(i)
                        USQL = " update " & tempTableMOQ & " set qtyMOQ" & (i) & "" & "='" & .Item("qty") & "', " &
                             " priceMOQ" & (i) & "" & "='" & .Item("price") & "' where item='" & .Item("item") & "'  "
                        'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                    End With
                Next

                For i As Integer = 0 To Program.Rows.Count - 1
                    With Program.Rows(i)
                        USQL = " update " & tempTableMOQ & " set sumQty" & "='" & .Item("sumQty") & "' where item='" & .Item("item") & "' "
                        'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                    End With
                Next

                'tempTableMOQShow
                For i As Integer = 0 To Program.Rows.Count - 1
                    count = Program.Rows.Count - 1
                Next
                CreateTempTable.createTempPRMOQShow(tempTableMOQShow)
                For i As Integer = 0 To Program.Rows.Count - 1
                    With Program.Rows(i)
                        USQL = " if exists(select * from " & tempTableMOQShow & " where qtyMOQ='" & .Item("qty") & "' ) " &
                         " update " & tempTableMOQShow & " set supplier='" & .Item("supplier") & "' , expDate='" & .Item("expdate") & "', qtyMOQ='" & .Item("qty") & "'," &
                         " priceMOQ ='" & .Item("price") & "', amountMOQ='" & .Item("price") * .Item("qty") & "',sumQty" & "='" & .Item("sumQty") & "'" &
                         " where qtyMOQ='" & .Item("qty") & "' else " &
                         " insert into " & tempTableMOQShow & "(item,supplier,expDate,qtyMOQ,priceMOQ,amountMOQ,sumQty)values('" & .Item("item") & "','" & .Item("supplier") & "','" & .Item("expdate") & "'," &
                         " '" & .Item("qty") & "','" & .Item("price") & "','" & .Item("price") * .Item("qty") & "','" & .Item("sumQty") & "')"
                        'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                    End With
                Next

                Dim dtShow As Data.DataTable = New DataTable
                dtShow.Columns.Add(New DataColumn("No"))
                dtShow.Columns.Add(New DataColumn("Item"))
                dtShow.Columns.Add(New DataColumn("Item Desc."))
                dtShow.Columns.Add(New DataColumn("Spec"))
                dtShow.Columns.Add(New DataColumn("Exp. Date"))
                dtShow.Columns.Add(New DataColumn("VD."))
                dtShow.Columns.Add(New DataColumn("Sum of PR Qty"))

                WHR = ""
                WHR = WHR & Conn_SQL.Where("MC001", tbItem)
                WHR = WHR & Conn_SQL.Where("MC002", tbSup)

                SQL = " select *,(select MB002 from HOOTHAI.dbo.INVMB where MB001=item)  MB002," &
                    " (select MB003 from HOOTHAI.dbo.INVMB where MB001=item)  MB003 " &
                    " from  " & tempTableMOQ & ""

                Dim dt As New DataTable
                'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                dt = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                Dim dr1 As DataRow,
                    dr2 As DataRow,
                    dr3 As DataRow,
                    dr4 As DataRow

                With dt
                    For i As Integer = 0 To .Rows.Count - 1
                        With .Rows(i)
                            'item
                            dr1 = dtShow.NewRow()
                            dr1("No") = "1"
                            dr1("Item") = .Item("item")
                            dr1("Item Desc.") = .Item("MB002")
                            dr1("Spec") = .Item("MB003")
                            dr1("Exp. Date") = .Item("expDate").ToString.Substring(0, 4) & "-" & .Item("expDate").ToString.Substring(4, 2) & "-" & .Item("expDate").ToString.Substring(6, 2)
                            dr1("VD.") = .Item("supplier")

                            dr2 = dtShow.NewRow()
                            dr2("No") = ""
                            dr2("Item") = ""
                            dr2("Item Desc.") = ""
                            dr2("Spec") = ""
                            dr2("Exp. Date") = ""
                            dr2("VD.") = "Unit Price"

                            dr3 = dtShow.NewRow()
                            dr3("No") = ""
                            dr3("Item") = ""
                            dr3("Item Desc.") = ""
                            dr3("Spec") = ""
                            dr3("Exp. Date") = ""
                            dr3("VD.") = "Amount"

                            dr4 = dtShow.NewRow()
                            dr4("No") = ""
                            dr4("Item") = ""
                            dr4("Item Desc.") = ""
                            dr4("Spec") = ""
                            dr4("Exp. Date") = ""
                            dr4("VD.") = "Sales Confirm"

                            Dim price As Decimal = strFormatQty("0")

                            For J As Integer = 0 To Program.Rows.Count - 1
                                Dim colName As String = "MOQ" & (J + 1)
                                dtShow.Columns.Add(New DataColumn(colName))
                                dr1(colName) = strFormatQty(.Item("qtyMOQ" & (J)))
                                dr2(colName) = strFormat(.Item("PriceMOQ" & (J)))
                                dr3(colName) = strFormat(.Item("qtyMOQ" & (J)) * .Item("priceMOQ" & (J)))
                            Next

                            For J As Integer = 0 To Program.Rows.Count - 1

                                Dim sumQty As Integer = .Item("sumQty")
                                Dim prQty As Integer = (.Item("qtyMOQ" & (J)))
                                Dim priceQty As String = (.Item("PriceMOQ" & (J)))
                                Dim LastprQty As Integer = 0
                                Dim LastpriceQty As String = ""

                                If J <> 0 Then
                                    LastprQty = (.Item("qtyMOQ" & (J - 1)))
                                    LastpriceQty = (.Item("PriceMOQ" & (J - 1)))
                                End If

                                If J = 0 Then

                                    If Program.Rows.Count - 1 = 0 Then
                                        price = priceQty
                                    ElseIf J = 0 And J <> Program.Rows.Count - 1 Then
                                        If sumQty <= prQty Then
                                            price = priceQty
                                        End If
                                    End If

                                ElseIf J > 0 And J < Program.Rows.Count - 1 Then

                                    If sumQty > LastprQty And sumQty < prQty Then
                                        price = LastpriceQty
                                    ElseIf sumQty = prQty Then
                                        price = priceQty
                                    End If

                                ElseIf J = Program.Rows.Count - 1 And Program.Rows.Count - 1 <> 0 Then

                                    If sumQty > LastprQty And sumQty < prQty Then
                                        price = LastpriceQty
                                    ElseIf sumQty >= prQty Then
                                        price = priceQty
                                    End If
                                End If
                            Next

                            dr1("Sum of PR Qty") = strFormatQty(.Item("sumQty"))
                            dr2("Sum of PR Qty") = strFormat(price)
                            dr3("Sum of PR Qty") = strFormat(.Item("sumQty") * (price))
                            dr4("Sum of PR Qty") = ""

                            dtShow.Rows.Add(dr1)
                            dtShow.Rows.Add(dr2)
                            dtShow.Rows.Add(dr3)
                            dtShow.Rows.Add(dr4)

                            USQL = " update " & tempTableMOQShow & " set price='" & price & "',amount='" & .Item("sumQty") * price & "'  "
                            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                        End With
                    Next
                    gvShow.DataSource = dtShow
                    gvShow.DataBind()
                End With
                CountRow1.RowCount = ControlForm.rowGridview(gvShow) / 4

                WHR = ""
                WHR = WHR & Conn_SQL.Where("TB004", tbItem)

                SQL = " select distinct  TB004 as 'Item',TR001+'-'+TR002+'-'+TR003 as 'PR No', cast(TR006 as decimal (16,2)) as 'PR Qty', " &
                    " TC026+'-'+TC027+'-'+TC028 as 'SO No', case when TC042 = 1 then TD004 else MF003 end as 'SO Item', " &
                    " case when TC042 = 1 then TD005 else MF004 end as 'Item Desc', case when TC042 = 1 then TD006 else MF005 end as 'Spec' " &
                    " FROM PURTB " &
                    " left join PURTR on TR001 = TB001 and TR002 = TB002 and TR003 = TB003 " &
                    " left join LRPTC on TC001 = TB057 and TC002 = TB004 and TC046 = TB058 " &
                    " left join COPTD on TD001 = TC026 and TD002 = TC027 and TD003 = TC028 " &
                    " left join COPMF on MF001 = TC027 and MF002 = TC028 " &
                    " WHERE PURTR.TR019 = '' and PURTB.TB039 = 'N' " & WHR
                'and TC006 <> 0 
                ControlForm.ShowGridView(gvShowPR, SQL, Conn_SQL.ERP_ConnectionString)
                System.Threading.Thread.Sleep(1000)
                btPrint.Visible = True
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
                gvShow.Visible = True
                gvShowPR.Visible = True
            Else
                show_message.ShowMessage(Page, "Item : (" & tbItem.Text.Trim & ") and Vendor : (" & tbSup.Text.Trim & ")  No Data Found", UpdatePanel1)
                gvShow.Visible = False
                gvShowPR.Visible = False
            End If

        End If

    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("ComparePriceunderMOQ" & Session("UserName"), gvShow)
    End Sub

    Private Function strFormat(ByVal val As Object) As String
        Dim show As String = ""
        If CDec(val) <> 0 Then
            show = String.Format("{0:n2}", val)
        Else
            show = "0.00"
        End If
        Return show
    End Function

    Private Function strFormatQty(ByVal val As Object) As String
        Dim show As String = ""
        If CDec(val) <> 0 Then
            show = String.Format("{0:n3}", val)
        Else
            show = "0.000"
        End If
        Return show
    End Function

    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrint.Click
        Dim tempTableMOQShow As String = "tempPRMOQShow" & Session("UserName")
        Dim paraName As String = ""
        paraName = "tableMOQ:" & tempTableMOQShow & chrConn & "item:" & tbItem.Text.Trim & chrConn
        Randomize()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=ComparePrice.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & Server.UrlEncode(chrConn) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
    End Sub

End Class