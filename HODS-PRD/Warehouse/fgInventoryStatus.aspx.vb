Public Class fgInventoryStatus
    Inherits System.Web.UI.Page
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate
    Dim saleType As String = "'2201','2202','2203','2204','2205','2213'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' and MQ001 in(" & saleType & ") order by MQ002"
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            'If Session("UserName") = "" Then
            '    'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            'End If
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempFGInventoryStatus" & Session("UserName")
        CreateTempTable.createTempFGInventoryStatus(tempTable)
        Dim fldHash As Hashtable = New Hashtable
        Dim whrHash As Hashtable = New Hashtable
        Dim DueDate As String = tbDate.Text
        If DueDate <> "" Then
            DueDate = ConfigDate.dateFormat2(DueDate)
        Else
            DueDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        End If


        Dim SQL As String = "",
            WHR As String = "",
            dt As New DataTable,
            InvDate As Date = DateTime.ParseExact(DueDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

        'inventory
        WHR = Conn_SQL.Where("LA001", tbItem)
        WHR = WHR & Conn_SQL.Where("MB003", tbSpec)
        WHR = WHR & Conn_SQL.Where("LA004", "", DueDate)
        SQL = " select LA001,sum(LA005*LA011)as qty,sum(LA005*LA012)as amt from INVLA " & _
              " left join INVMB on MB001=LA001 " & _
              " where LA009='2101' and substring(LA001,3,1)='2' and LA011<>0 " & WHR & _
              " group by LA001 having sum(LA005*LA011)<>0 " & _
              " order by LA001 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        With dt
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    fldHash = New Hashtable
                    whrHash = New Hashtable

                    Dim code As String = .Item("LA001"),
                        qty As Decimal = .Item("qty"),
                        amt As Decimal = .Item("amt"),
                        sSQL As String = ""

                    whrHash.Add("item", code)
                    sSQL = "select top 1 LA004 from INVLA  where LA009='2101' and substring(LA001,3,1)='2' and LA001='" & code & "' and LA005=1  order by LA004 desc"
                    Dim sProgram As New DataTable
                    sProgram = Conn_SQL.Get_DataReader(sSQL, Conn_SQL.ERP_ConnectionString)
                    Dim LastDate As Date = DateTime.ParseExact(sProgram.Rows(0).Item("LA004").ToString.Trim, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                    Dim dateAging As Integer = DateDiff(DateInterval.Day, LastDate, InvDate)
                    Dim fldQty As String = "",
                        fldAmt As String = ""
                    If dateAging < 91 Then
                        fldQty = "qtyA"
                        fldAmt = "amtA"
                    ElseIf dateAging < 180 Then
                        fldQty = "qtyB"
                        fldAmt = "amtB"
                    ElseIf dateAging < 271 Then
                        fldQty = "qtyC"
                        fldAmt = "amtC"
                    ElseIf dateAging < 360 Then
                        fldQty = "qtyD"
                        fldAmt = "amtD"
                    Else
                        fldQty = "qtyE"
                        fldAmt = "amtE"
                    End If
                    fldHash.Add(fldQty, qty)
                    fldHash.Add(fldAmt, amt)
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                End With
            Next
        End With
        'SO
        WHR = Conn_SQL.Where("TD004", tbItem)
        WHR = WHR & Conn_SQL.Where("TD006", tbSpec)
        WHR = WHR & Conn_SQL.Where("TD013", "", DueDate)
        WHR = WHR & Conn_SQL.Where("TD001", cblSaleType, False, saleType)
        WHR = WHR & Conn_SQL.Where("TD002", tbSaleNo)
        WHR = WHR & Conn_SQL.Where("TD003", tbSaleSeq)
        WHR = WHR & Conn_SQL.Where("TC004", tbCust)

        SQL = " select TD004,sum(TD008-TD009) as qty,sum((TD008-TD009)*TD001) as amt from COPTD " & _
              " left join COPTC on TC001=TD001 and TC002=TD002 " & _
              " where TC027 ='Y' and TD016 = 'N' " & WHR & _
              " group by TD004 order by TD004 "
        'Dim dt As New DataTable
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        With dt
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    fldHash = New Hashtable
                    whrHash = New Hashtable
                    whrHash.Add("item", .Item("TD004"))
                    fldHash.Add("delQty", .Item("qty"))
                    fldHash.Add("delAmt", .Item("amt"))
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash), Conn_SQL.MIS_ConnectionString)
                End With
            Next
        End With
        '
        'SO Close
        SQL = " select TD004,sum(TD008-TD009) as qty from COPTD " & _
              " left join COPTC on TC001=TD001 and TC002=TD002 " & _
              " where TC027 ='Y' and TD016 = 'y' " & WHR & _
              " group by TD004 order by TD004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        With dt
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    fldHash = New Hashtable
                    whrHash = New Hashtable
                    whrHash.Add("item", .Item("TD004"))
                    fldHash.Add("closeQty", .Item("qty"))
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash), Conn_SQL.MIS_ConnectionString)
                End With
            Next
        End With
        'SO Change
        WHR = Conn_SQL.Where("TF005", tbItem)
        WHR = WHR & Conn_SQL.Where("TF006", tbSpec)
        WHR = WHR & Conn_SQL.Where("TF015", "", DueDate)
        WHR = WHR & Conn_SQL.Where("TF001", cblSaleType)
        WHR = WHR & Conn_SQL.Where("TF002", tbSaleNo)
        WHR = WHR & Conn_SQL.Where("TF004", tbSaleSeq)
        WHR = WHR & Conn_SQL.Where("TE007", tbCust)
        SQL = " select TF001,TF002,TF004,TF005,count(*) as cnt,sum(TF109-TF009)as qty from COPTF " & _
              " left join COPTE on TE001=TF001 and TE002=TF002  and TE003=TF003 " & _
              " where TE029 = 'Y' and TF105=TF005 and TF009<>TF109 " & WHR & _
              " group by TF001,TF002,TF004,TF005 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim item As New Dictionary(Of String, Decimal)
        With dt
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim code As String = .Item("TF005"),
                        qty As Decimal = 0
                    If .Item("cnt") = 1 Then
                        qty = .Item("qty")
                    Else
                        Dim sSQL As String = "select top 1 TF109-TF009 as qty from COPTF " & _
                            " where TF001='" & .Item("TF001") & "' and TF002='" & .Item("TF002") & "' and TF004='" & .Item("TF004") & "' " & _
                            " order by TF003 desc"
                        Dim sProgram As New DataTable
                        sProgram = Conn_SQL.Get_DataReader(sSQL, Conn_SQL.ERP_ConnectionString)
                        qty = sProgram.Rows(0).Item("qty")
                    End If
                    If item.ContainsKey(code) Then
                        item.Item(code) = item.Item(code) + qty
                    Else
                        item.Add(code, qty)
                    End If
                End With
            Next
        End With
        For Each pair In item
            fldHash = New Hashtable
            whrHash = New Hashtable
            whrHash.Add("item", pair.Key) 'item
            fldHash.Add("changeQty", pair.Value) 'qty
            Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash), Conn_SQL.MIS_ConnectionString)
        Next
        'show data
        Dim fldStockQ As String = "T.qtyA+T.qtyB+T.qtyC+T.qtyD+T.qtyE ",
            fldStockA As String = "T.amtA+T.amtB+T.amtC+T.amtD+T.amtE",
            con As String = ddlCon.Text
        WHR = ""
        If con <> 0 Then
            WHR = " where "
            Select Case con
                Case "1"
                    WHR = WHR & " T.delQty=0 "
                Case "2"
                    WHR = WHR & fldStockQ & ">= T.delQty "
            End Select
        End If
        SQL = " select T.*,MB003," & fldStockQ & " as invQty," & fldStockA & " as invAmt from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB on MB001=T.item " & WHR & _
              " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("fgInventoryStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim hplDetail As HyperLink = CType(.FindControl("hlShow"), HyperLink)
                Dim code As String = .DataItem("Item").ToString.Replace("Null", "")
                Dim spec As String = .DataItem("MB003").ToString.Replace("Null", "")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("MB003")) Then

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
                    link = link & "&saleNo=" & tbSaleNo.Text
                    link = link & "&saleSeq=" & tbSaleSeq.Text
                    link = link & "&cust=" & tbCust.Text
                    link = link & "&endDate=" & ConfigDate.dateFormat2(tbDate.Text)
                    link = link & "&item=" & code
                    link = link & "&spec=" & spec
                    hplDetail.NavigateUrl = "fgInventoryStatusPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", code & " - " & spec)

                    'change value
                    'Dim gvRow As GridViewRow = .Rows(rowIndex)
                    For cellCount As Integer = 3 To .Cells.Count - 2
                        Dim val As String = .Cells(cellCount).Text
                        If val = "0.00" Then
                            val = ""
                        End If
                        .Cells(cellCount).Text = val
                    Next

                End If
            End If
        End With
    End Sub
End Class