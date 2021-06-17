Imports MIS_HTI.DataControl
Public Class CostBom
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Private TotalAmt As Decimal = 0
    Dim dbConn As New DataConnectControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "CostBOM" & Session("UserName")
        GenData(tempTable)

        Dim qty As Decimal = 1
        Dim fldQty As String = tbQty.Text.ToString.Trim
        If fldQty <> "" Then
            qty = CDec(fldQty)
            If qty = 0 Then
                qty = 1
            End If
        End If
        'Dim qtyForCal As String = ""
        Dim SQL As String = ""
        SQL = " select substring(BomItem,1,16)as 'Parent Item',SubItem as 'Sub Item', " &
              " case when Operation ='' then INVMB.MB002 else T.Operation end as 'Description', case when Operation ='' then INVMB.MB003 else CMSMW.MW002 end  as 'Specification', " &
              " (LEN(BomItem) - LEN(REPLACE(BomItem, ':', ''))) as 'Level Bom', " &
              " case Property when 'P' then 'Purchase' " &
              "               when 'M' then 'Manufacture' " &
              "               when 'S' then 'Subcontract' " &
              "               when 'Y' then 'Phantom' " &
              "               when 'O' then 'Outsource' " &
              "               else 'Configuration' end as Property," &
              " QtyPerPcs as 'QPA',cast(QtyPerPcs*" & qty & " as decimal(16,4)) as 'Usage'," &
              " Unit,Currency,Price as 'Price Original',ExchangRate as 'Exchang Rate'," &
              " cast(Price*ExchangRate as decimal(16,4)) as 'Price THB', " &
              " Supplier,PURMA.MA002 as 'Supplier Description'," &
              " cast(QtyPerPcs*" & qty & "*Price*ExchangRate as decimal(16,4)) as 'Amount THB' " &
              " from " & tempTable & " T " &
              " left join HOOTHAI.dbo.PURMA PURMA on PURMA.MA001= T.Supplier " &
              " left join HOOTHAI.dbo.INVMB INVMB on INVMB.MB001= T.SubItem " &
              " left join HOOTHAI.dbo.CMSMW CMSMW on CMSMW.MW001= T.Operation " &
              " order by BomItem"
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("CostBOM" & Session("UserName"), gvShow)
    End Sub

    Protected Sub GenData(tempTable As String)
        CreateTempTable.createTempCostBOM(tempTable)
        Dim fldInsHash As Hashtable = New Hashtable,
            whrHash As Hashtable = New Hashtable,
            codePrd As String = tbCode.Text.Trim
        whrHash.Add("BomItem", codePrd) ' parent item
        'insert Zone
        fldInsHash.Add("ParentItem", codePrd) ' parent item
        fldInsHash.Add("SubItem", codePrd) ' sub item
        fldInsHash.Add("Unit", "PC") 'Unit
        fldInsHash.Add("QtyPerPcs", 1) 'qty per pcs
        fldInsHash.Add("Property", "M") 'property
        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
        codeOuts(tempTable, codePrd, codePrd, codePrd)
        CodeBOM(tempTable, codePrd, codePrd)

        Dim SQL As String = "select SubItem,Property from " & tempTable & " where Property not in ('M')  group by SubItem,Property ",
            SQL1 As String = "",
            StrSQL As String = "",
            Program As New DataTable,
            sProgram As New DataTable,
            ssProgram As New DataTable,
            ExchangeRateList = New Hashtable
        'get curency export value to local value
        ExchangeRateList.Add("THB", 1)
        Dim yearMonth As String = DateTime.Today.ToString("yyyyMM")
        StrSQL = " select MG001,MG004 from CMSMG where MG001<>'THB' and MG002 like '" & yearMonth & "%' and MG002<'" & yearMonth & "06' and MG004>0 order by MG002 desc "
        'ssProgram = Conn_SQL.Get_DataReader(ssSQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(StrSQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To ssProgram.Rows.Count - 1
            With ssProgram.Rows(i)
                Dim currency As String = .Item("MG001").ToString.Trim
                If ExchangeRateList.ContainsKey(currency) = False Then
                    ExchangeRateList.Add(currency, .Item("MG004")) 'value is exchang rate for currency
                End If
            End With
        Next
        Program = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim supplier As String = "",
                    price As Decimal = 99999,
                    currency As String = "",
                    exchangRate As Decimal = 1,
                    code As String = .Item("SubItem").ToString.Trim,
                    propertyCode As String = .Item("Property").ToString.Trim

                'select price last update each supplier
                Dim priceList As Hashtable = New Hashtable,
                    currencyList As Hashtable = New Hashtable

                If propertyCode = "O" Then
                    Dim temp As String() = code.Split("-")
                    SQL1 = "select TM004 as sup,TM005 as cur,TN009 as prc  from MOCTN left join MOCTM on TM001=TN001 and TM002=TN002 where TM009='Y' and TN004='" & temp(0) & "' and TN007='" & temp(1) & "' order by TM004,TN011 DESC"
                    Dim aa As String = ""
                Else
                    SQL1 = "select MB002 as sup,MB003 as cur,MB011 as prc from PURMB where MB001='" & code & "' order by MB002,MB014 DESC"
                End If
                sProgram = dbConn.Query(SQL1, VarIni.ERP, dbConn.WhoCalledMe)
                'sProgram = Conn_SQL.Get_DataReader(sSQL, Conn_SQL.ERP_ConnectionString)
                For j As Integer = 0 To sProgram.Rows.Count - 1
                    With sProgram.Rows(j)
                        Dim supCode As String = .Item("sup") 'supplier code
                        If priceList.ContainsKey(supCode) = False Then
                            priceList.Add(supCode, .Item("prc")) 'price per unit for supplier
                            currencyList.Add(supCode, .Item("cur")) 'currency for supplier
                        End If
                    End With
                Next

                'select price is cheapest and currency
                If priceList.Count > 0 Then
                    For Each sCode As String In priceList.Keys
                        Dim temp_cur As String = currencyList.Item(sCode).ToString.Trim
                        Dim temp_prc_ori As Decimal = CDec(priceList.Item(sCode))
                        Dim temp_rate As Decimal = CDec(ExchangeRateList.Item(temp_cur))
                        Dim temp_prc As Decimal = temp_prc_ori * temp_rate
                        If temp_prc < price Then
                            supplier = sCode
                            price = temp_prc_ori
                            currency = temp_cur
                            exchangRate = temp_rate
                        End If
                    Next
                Else
                    price = 0
                    currency = ""
                    exchangRate = 1
                    supplier = ""
                End If

                fldInsHash = New Hashtable
                Dim fldUpdHash As Hashtable = New Hashtable
                whrHash = New Hashtable
                'whr of condition
                whrHash.Add("SubItem", code.Trim) ' Sub item
                'Update Zone
                fldUpdHash.Add("Supplier", "" & supplier.Trim & "") ' Supplier
                fldUpdHash.Add("Price", "" & price & "") ' Price
                fldUpdHash.Add("Currency", "" & currency & "") ' Currency
                fldUpdHash.Add("ExchangRate", "" & exchangRate & "") ' Exchange Rate
                Dim USQL = Conn_SQL.GetSQL(tempTable, fldUpdHash, whrHash, "U")
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next
    End Sub

    Protected Sub CodeBOM(tempTable As String, code As String, parentItem As String, Optional qpa As Decimal = 1)
        Dim SQL As String = "",
            USQL As String = "",
            sSQL As String = ""

        SQL = " select MD001,MD003,MD006,MD004,MB025 from BOMMD " &
              " left join INVMB on MB001=MD003 " &
              " where MD001='" & code & "' "
        Dim Program As New DataTable,
            sProgram As New DataTable
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim fldInsHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                Dim parentCode As String = .Item("MD001")
                Dim subCode As String = .Item("MD003").ToString.Trim
                'whr of condition
                whrHash.Add("BomItem", parentItem & ":" & subCode) ' parent item
                'insert Zone
                fldInsHash.Add("ParentItem", parentCode) ' parent item
                fldInsHash.Add("SubItem", subCode) ' sub item
                fldInsHash.Add("Unit", .Item("MD004")) 'Unit
                fldInsHash.Add("QtyPerPcs", .Item("MD006") * qpa) 'qty per pcs
                fldInsHash.Add("Property", .Item("MB025")) 'property
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash, "I"), VarIni.DBMIS, dbConn.WhoCalledMe)
                'select data from outs
                codeOuts(tempTable, parentItem, parentCode, subCode, qpa)
                If .Item("MB025") = "M" Then
                    CodeBOM(tempTable, subCode, parentItem & ":" & subCode, .Item("MD006"))
                End If

            End With
        Next
    End Sub
    Protected Sub codeOuts(ByVal temptable As String, ByVal parentItem As String, ByVal parentCode As String, ByVal code As String, Optional ByVal qpa As Decimal = 1)
        Dim sSQL As String = "",
            sProgram As DataTable

        'select data from outs
        sSQL = "select MF004,MF017,COUNT(MF004)as cnt from BOMMF where  MF001='" & code & "' and MF002='01' and MF005='2' group by MF001,MF002,MF004,MF017"
        'sProgram = Conn_SQL.Get_DataReader(sSQL, Conn_SQL.ERP_ConnectionString)
        sProgram = dbConn.Query(sSQL, VarIni.ERP, dbConn.WhoCalledMe)
        For j As Integer = 0 To sProgram.Rows.Count - 1
            With sProgram.Rows(j)
                Dim xOper As String = .Item("MF004")
                Dim operation As String = code & "-" & xOper
                Dim fldInsHash = New Hashtable
                Dim whrHash = New Hashtable
                'whr of condition
                whrHash.Add("BomItem", parentItem & ":" & operation) ' parent item
                'insert Zone
                fldInsHash.Add("ParentItem", parentCode) ' parent item
                fldInsHash.Add("SubItem", operation) ' sub item
                fldInsHash.Add("Unit", .Item("MF017")) 'Unit
                fldInsHash.Add("QtyPerPcs", .Item("cnt") * qpa) 'qty per pcs
                fldInsHash.Add("Property", "O") 'property
                fldInsHash.Add("Operation", xOper) 'Operation
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(temptable, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                dbConn.TransactionSQL(Conn_SQL.GetSQL(temptable, fldInsHash, whrHash, "I"), VarIni.DBMIS, dbConn.WhoCalledMe)

            End With
        Next
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' if row type is DataRow, add ProductSales value to TotalSales
            TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount THB"))
            'set row display
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(12).Text = "Qauntity"
            e.Row.Cells(13).Text = String.Format("{0:0}", CDec(tbQty.Text.ToString.Trim))
            e.Row.Cells(14).Text = "Sum Total"
            e.Row.Cells(15).Text = String.Format("{0:n}", TotalAmt)
        End If
    End Sub
End Class