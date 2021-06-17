Imports OfficeOpenXml
Imports System.IO

Public Class CustPOImportForcast
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim createTableSale As New createTableSale
    Dim ConfigDate As New ConfigDate

    Const tableHead As String = "COPME"
    Const tableBody As String = "COPMF"

    Const table As String = "CodeInfo"
    Const codeHead As String = "PLANT"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            reset()
            ddlCust_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Protected Sub reset()

        lbDate.Text = ConfigDate.dateShow2(Date.Today.ToString("yyyyMMdd"), "/")
        btSave.Visible = False
        tbCustPO.Text = ""
        tbRemark.Text = ""
        Dim SQL As String = "select rtrim(MA001) MA001,rtrim(MA001)+':'+MA002 MA002 from COPMA where MA097 = '1' and substring(COPMA.UDF01,1,1)='Y' order by MA001 "
        ControlForm.showDDL(ddlCust, SQL, "MA002", "MA001", False, Conn_SQL.ERP_ConnectionString)

        gvCustShow.DataSource = ""
        gvCustShow.DataBind()

        lbError.Text = ""
        
    End Sub

    Protected Sub btUpload_Click(sender As Object, e As EventArgs) Handles btUpload.Click
        If tbCustPO.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Cust PO เท่ากับค่าว่าง, กรุณากรอกเลขที่ Cust PO.", UpdatePanel1)
            tbCustPO.Focus()
            Exit Sub
        End If

        Dim fileExten As String = Path.GetExtension(fuUpload.FileName).Replace(".", "")
        If (fuUpload.HasFile AndAlso (fileExten = "xlsx" Or fileExten = "xls")) Then
            Using excel = New ExcelPackage(fuUpload.PostedFile.InputStream)
                Dim tbl = New DataTable()
                'Dim ws As ExcelWorksheet = excel.Workbook.Worksheets.First
                Dim ws As ExcelWorksheet = excel.Workbook.Worksheets.First
                Dim hasHeader = True ' change it if required '
                ' create DataColumns '
                For Each firstRowCell In ws.Cells(1, 1, 1, ws.Dimension.End.Column)
                    tbl.Columns.Add(If(hasHeader, firstRowCell.Text, String.Format("Column {0}", firstRowCell.Start.Column)))
                Next
                ' add rows to DataTable '
                Dim startRow = If(hasHeader, 2, 1)
                For rowNum = startRow To ws.Dimension.End.Row
                    Dim wsRow = ws.Cells(rowNum, 1, rowNum, ws.Dimension.End.Column)
                    Dim row = tbl.NewRow()
                    For Each cell In wsRow
                        row(cell.Start.Column - 1) = cell.Text
                    Next
                    tbl.Rows.Add(row)
                Next

                'spec A
                'Cust PO Qty B

                Dim dt = New DataTable
                'Dim aa As DataColumn = New DataColumn(
                dt.Columns.Add("Cust PO") '0
                dt.Columns.Add("Seq") '1
                dt.Columns.Add("Item") '2
                dt.Columns.Add("Desc") '3
                dt.Columns.Add("Spec") '4
                dt.Columns.Add("Qty") '5
                dt.Columns.Add("Unit") '6
                dt.Columns.Add("WH") '7
                'dt.Columns.Add("Price") '7
                'dt.Columns.Add("Amout") '8

                Dim line As Integer = 1
                Dim SQL As String,
                    dtSub As DataTable,
                     dateToday As String = Date.Now.ToString("yyyyMMdd")
                With tbl
                    For i As Integer = 0 To .Rows.Count - 1
                        With .Rows(i)

                            Dim row = dt.NewRow
                            row(1) = CStr(line) 'seq
                            Dim item As String = "",
                                desc As String = "",
                                spec As String = Trim(.Item(0)),
                                price As Decimal = 0,
                                unit As String = "",
                                wh As String = ""

                            'get basic info for spec
                            SQL = " select INV.MB001,INV.MB002,INV.MB003,INV.MB004,INV.MB017,isnull(COP.MB008,0) MB008 from INVMB INV " & _
                                  " left join COPMB COP on COP.MB002=INV.MB001 and COP.MB001='" & ddlCust.Text.Trim & "' and COP.MB017<='" & dateToday & "' " & _
                                  " where INV.MB003='" & spec & "' order by COP.MB017 desc "
                            dtSub = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                            If dtSub.Rows.Count > 0 Then
                                With dtSub.Rows(0)
                                    item = Trim(.Item("MB001"))
                                    desc = Trim(.Item("MB002"))
                                    unit = .Item("MB004")
                                    price = .Item("MB008")
                                    wh = .Item("MB017")
                                End With
                            End If
                            row(0) = tbCustPO.Text.Trim 'Cust PO
                            row(2) = item 'Item
                            row(3) = desc 'Desc
                            row(4) = spec 'Spec
                            row(5) = Trim(.Item(1)) 'Qty
                            row(6) = unit 'unit
                            row(7) = wh 'main warehouse
                            'row(7) = price 'price
                            'row(8) = Math.Round(If(IsNumeric(Trim(.Item(1))), CDec(Trim(.Item(1))), 0) * price, 2) 'amout
                            dt.Rows.Add(row)
                            line += 1
                        End With
                    Next
                End With

                gvCustShow.DataSource = dt
                gvCustShow.DataBind()
                'Dim msg = String.Format("DataTable successfully created from excel-file Colum-count:{0} Row-count:{1}", tbl.Columns.Count, tbl.Rows.Count)
                'lbShow.Text = msg
            End Using
        Else
            'UploadStatusLabel.Text = "You did not specify an excel-file to upload."
        End If
        If gvCustShow.Rows.Count > 0 Then 'And gvCustShow.Columns.Count > 3
            btSave.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollList", "gridviewScrollList();", True)
        End If

    End Sub

    Protected Sub ddlCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCust.SelectedIndexChanged
        ddlPlant.Items.Clear()
        Dim SQL As String = "select rtrim(Code) MD001,Code+':'+Name MD002 from " & table & " where CodeType='" & codeHead & "' and WC like '%" & ddlCust.Text.Trim & "%' order by Code "
        ControlForm.showDDL(ddlPlant, SQL, "MD002", "MD001", False, Conn_SQL.MIS_ConnectionString)
    End Sub


    Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        reset()
        ddlCust_SelectedIndexChanged(sender, e)
    End Sub

    'Private Function getDocNo() As String
    '    Dim DocNo As String = ""
    '    Dim DateDoc As String = Date.Today.ToString("yyyyMMdd")
    '    'Dim ymd As String = configDate.dateFormat2(tbDate.Text.Trim)
    '    Dim SQL As String = "select substring(docNo,1,8),max(docNo) as DocNo  from " & tablePO_Head & " where docNo like '" & DateDoc & "%' group by substring(docNo,1,8)"
    '    Dim Program As New Data.DataTable
    '    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
    '    If Program.Rows.Count > 0 Then
    '        DocNo = CDec(Program.Rows(0).Item("docNo")) + 1
    '    Else
    '        DocNo = DateDoc & "001"
    '    End If
    '    Return DocNo
    'End Function

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Dim docNo As String = "",
            strSQL As String = "",
            docDate As String = Date.Today.ToString("yyyyMMdd"),
            cBy As String = "H" & Session("UserName"),
            cDay As String = DateTime.Now.ToString("yyyyMMddHHmmss"),
            custCode As String = ddlCust.Text.Trim,
            custPlant As String = ddlPlant.Text.Trim,
            SQL As String = "",
            chkExist As New ArrayList,
            dt As DataTable = New DataTable,
            txtError As String = ""

        Dim custPO As String = tbCustPO.Text.Trim

        'check cust po code
        SQL = "select ME001 from " & tableHead & "  where ME001='" & custPO & "' "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        If dt.Rows.Count > 0 Then
            show_message.ShowMessage(Page, "Cust PO =" & custPO & " is repeat( PO ซ้ำกัน)", UpdatePanel1)
            Exit Sub
        End If

        For i As Integer = 0 To gvCustShow.Rows.Count - 1
            With gvCustShow.Rows(i)
                Dim item As String = .Cells(2).Text.Trim.Replace("&nbsp;", "")
                'Dim custPO As String = .Cells(1).Text.Trim
                If chkExist.Count > 0 And chkExist.Contains(item) Then
                    show_message.ShowMessage(Page, "item=" & item & " is repeat(มีรายการซ้ำกัน)", UpdatePanel1)
                    Exit Sub
                End If
                chkExist.Add(item)
                If item = "" Then
                    show_message.ShowMessage(Page, "spec=" & .Cells(4).Text.Trim.Replace("&nbsp;", "") & " is empty on line " & i + 1 & "(ไม่มีรหัสสินค้านี้ รายการที่ " & i + 1 & " กรุณาตรวจสอบอีกครั้ง)", UpdatePanel1)
                    Exit Sub
                End If

                'SQL = " select H.custCode, H.plant,B.item,B.custPO from " & tableBody & " B left join " & tableHead & " H on H.docNo=B.docNo " & _
                '      " where H.custCode='" & custCode & "' and H.plant='" & custPlant & "' and B.item='" & item & "' and B.custPO='" & custPO & "' "
                'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                'If dt.Rows.Count > 0 Then
                '    txtError = item & " and " & custPO & ",<br>"
                'End If

            End With
        Next
        'If txtError <> "" Then
        '    lbError.Text = txtError
        '    Exit Sub
        'End If

        Dim fld As Hashtable
        Dim whr As Hashtable

        'Head
        fld = New Hashtable
        whr = New Hashtable

        whr.Add("ME001", custPO)

        fld.Add("COMPANY", Conn_SQL.DBMain)
        fld.Add("CREATOR", cBy)
        fld.Add("USR_GROUP", "HODS")
        fld.Add("CREATE_DATE", cDay)
        fld.Add("FLAG", "3")

        fld.Add("ME002", custCode)
        fld.Add("ME006", "A01")
        fld.Add("ME008", "N")
        fld.Add("ME009", tbRemark.Text.Trim)
        fld.Add("ME013", "HT")
        fld.Add("UDF01", ddlPlant.Text.Trim)
        Dim dNow As String = Date.Now.ToString("yyyyMMdd")
        fld.Add("UDF02", dNow) 'date input
        strSQL &= Conn_SQL.GetSQL(tableHead, fld, whr, "I")
        'body
        For i As Integer = 0 To gvCustShow.Rows.Count - 1
            With gvCustShow.Rows(i)
                fld = New Hashtable
                whr = New Hashtable
                Dim xseq As String = .Cells(1).Text.Trim.Replace("&nbsp;", "")

                whr.Add("MF001", .Cells(0).Text.Trim)
                whr.Add("MF002", returnLine(xseq))

                fld.Add("COMPANY", Conn_SQL.DBMain)
                fld.Add("CREATOR", cBy)
                fld.Add("USR_GROUP", "HODS")
                fld.Add("CREATE_DATE", cDay)
                fld.Add("FLAG", "3")

                'fld.Add("UDF01", custCode)
                'fld.Add("UDF02 ", custPlant)
                'fld.Add("custPO", .Cells(0).Text.Trim)

                fld.Add("MF003", .Cells(2).Text.Trim) 'item
                fld.Add("MF004", .Cells(3).Text.Trim) 'desc
                fld.Add("MF005", .Cells(4).Text.Trim) 'spec
                fld.Add("MF006", dNow) 'date

                fld.Add("MF008", .Cells(5).Text.Trim) 'qty
                fld.Add("MF010", .Cells(6).Text.Trim) 'unit
                fld.Add("MF007", .Cells(7).Text.Trim) 'wh
                fld.Add("MF011", "THB") 'currency

                strSQL &= Conn_SQL.GetSQL(tableBody, fld, whr, "I")
 
            End With
        Next
        If gvCustShow.Rows.Count > 0 Then
            Conn_SQL.Exec_Sql(strSQL, Conn_SQL.ERP_ConnectionString)
            show_message.ShowMessage(Page, "บันทึกเรียบร้อย(Record Complete)", UpdatePanel1)
            reset()
        End If
    End Sub

    Function returnLine(line As String) As String
        line = "000" & line
        Return Right(line, 4)
    End Function

End Class