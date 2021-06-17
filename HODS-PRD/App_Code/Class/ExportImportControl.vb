Imports System.IO
Imports System.Data
Imports ClosedXML.Excel

Namespace DataControl
    Public Class ExportImportControl
        '==========>> from gridview <<==========
        Sub Export(fileName As String, gv As GridView, Optional isNumber As Hashtable = Nothing)
            Dim wb As XLWorkbook = ExportGridviewSet(fileName, gv, isNumber)
            downloadFile(wb, fileName & Date.Now.ToString("yyyyMMdd"))
        End Sub

        Function ExportGridviewSet(sheetName As String, gv As GridView, Optional isNumber As Hashtable = Nothing) As XLWorkbook
            Dim wb As New XLWorkbook()
            Using wb
                Dim ws = wb.Worksheets.Add(sheetName)
                'ws.Tables.FirstOrDefault.ShowAutoFilter = False
                Dim Line As Integer = 1
                Dim col As Integer = 1
                For Each cell As TableCell In gv.HeaderRow.Cells
                    With ws.Cell(Line, col)
                        .Style.Fill.BackgroundColor = XLColor.LightGreen
                        .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                        .Value = cell.Text
                    End With
                    col += 1
                Next
                'Loop and add rows from GridView to Excel.
                Dim hashCont As New HashtableControl
                Line += 1
                For Each row As GridViewRow In gv.Rows
                    col = 1
                    For j As Integer = 0 To row.Cells.Count - 1
                        Dim val As String = row.Cells(j).Text.Replace("&nbsp;", "")
                        With ws.Cell(Line, col)
                            .Style.Fill.BackgroundColor = If(Line Mod 2 = 0, XLColor.LightBlue, XLColor.LightGray)
                            .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                            If hashCont.existDataHash(isNumber, j) Then
                                .Value = val.Replace(",", "")
                                .Style.NumberFormat.Format = "#,###.00"
                                .DataType = XLDataType.Number
                            Else
                                .Value = "'" + val
                                '.DataType = XLDataType.Text
                            End If
                        End With
                        col += 1
                    Next
                    Line += 1
                Next
            End Using
            Return wb
        End Function

        'Function ExportMail(sheetName As String, gv As GridView, Optional isNumber As Hashtable = Nothing) As XLWorkbook
        '    Return ExportGridviewSet(sheetName, gv, isNumber)
        'End Function

        Sub ExportGridview(fileName As String, gv As GridView, Optional isNumber As Hashtable = Nothing)
            Dim wb As XLWorkbook = ExportGridviewSet(fileName, gv, isNumber)
            downloadFile(wb, fileName & Date.Now.ToString("yyyyMMdd"))
        End Sub
        '==========>> from gridview <<==========

        '==========>> from SQL Query <<==========
        Sub ExportDatatable(ByRef wb As XLWorkbook, workSheetName As String,
                            SQL As String, dbType As String,
                            colName As ArrayList, Optional strSplit As String = VarIni.char8, Optional WhoCalledMe As String = "")
            Dim dbConn As New DataConnectControl
            Dim dt As DataTable = dbConn.Query(SQL, dbType, WhoCalledMe & dbConn.WhoCalledMe)
            ExportDatatable(wb, workSheetName, dt, colName, strSplit)
        End Sub
        '==========>> from SQL Query <<==========

        '==========>> from datatable <<==========
        Sub Export(fileName As String, dt As DataTable)
            Using wb As New XLWorkbook()
                dt.TableName = fileName
                Dim ws = wb.Worksheets.Add(dt)
                ws.Tables.FirstOrDefault.ShowAutoFilter = False
                downloadFile(wb, fileName)
            End Using
        End Sub

        Function ExportDatatable(fileName As String, dt As DataTable) As XLWorkbook
            Dim wb As New XLWorkbook()
            Using wb
                dt.TableName = fileName
                Dim ws = wb.Worksheets.Add(dt)
                ws.Tables.FirstOrDefault.ShowAutoFilter = False
            End Using
            Return wb
        End Function

        'update by wanlop 2019/09/25 head change color bg and font and select color font default in body 
        'update by wit add function  LengthValue for check char length
        Sub ExportDatatable(ByRef wb As XLWorkbook, workSheetName As String, dt As DataTable, colName As ArrayList,
                            Optional strSplit As String = VarIni.char8)
            Dim dtCont As New DataTableControl
            Dim outCont As New OutputControl
            Using wb
                Dim ws = wb.Worksheets.Add(workSheetName)
                'ws.Tables.FirstOrDefault.ShowAutoFilter = False
                Dim Line As Integer = 1
                Dim col As Integer = 1
                For Each str As String In colName
                    Dim temp() As String = str.Split(strSplit)
                    With ws.Cell(Line, col)
                        '.Style.Fill.BackgroundColor = XLColor.LightGreen
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#3980c6")
                        .Style.Font.FontColor = XLColor.White
                        .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                        .Value = temp(0)
                    End With
                    ws.Columns(col).AdjustToContents()
                    col += 1
                Next
                Line += 1

                For Each dr As DataRow In dt.Rows
                    col = 1
                    For Each str As String In colName
                        Dim temp() As String = str.Split(strSplit)
                        Dim isNumber As Boolean = False
                        Dim stringFormat As String = ""
                        If temp.Count >= 3 Then
                            isNumber = True
                            stringFormat = "#,##0"
                            Dim valChekNumber As Decimal = outCont.checkNumberic(temp(2))
                            If valChekNumber > 0 Then
                                Dim valZero As String = "."
                                For i As Integer = 1 To valChekNumber
                                    valZero &= "0"
                                Next
                                stringFormat &= valZero
                            End If
                        End If
                        'Dim val As String = dtCont.IsDBNullDataRow(dr, temp(1), If(isNumber, "0", ""))
                        Dim val As String = LengthValue(dtCont.IsDBNullDataRow(dr, temp(1), If(isNumber, "0", "")))
                        With ws.Cell(Line, col)
                            '.Style.Fill.BackgroundColor = If(Line Mod 2 = 0, XLColor.FromHtml("#FFF0F8FF"), XLColor.FromHtml("#FFFAEBD7"))
                            .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                            If isNumber Then
                                .Style.NumberFormat.Format = stringFormat
                                '.DataType = XLDataType.Number
                            Else
                                val = "'" & val
                            End If
                            .Value = val

                        End With
                        col += 1
                    Next
                    Line += 1
                Next
            End Using
            '  Return wb
        End Sub

        'add By Wit 24-09-2019
        'update by wanlop 25-09-2019 adjust function
        Sub ExportDatatable(workSheetName As String, dt As DataTable, colName As ArrayList, Optional strSplit As String = VarIni.char8)
            Using wb As New XLWorkbook()
                ExportDatatable(wb, workSheetName, dt, colName, strSplit)
                downloadFile(wb, workSheetName)
            End Using
        End Sub

        Sub ExportDatatableDownload(fileName As String, dt As DataTable)
            Dim wb As XLWorkbook = ExportDatatable(fileName, dt)
            downloadFile(wb, fileName)
        End Sub

        Sub ExportDatatableDownload(fileName As String, dt As DataTable, colName As ArrayList, Optional strSplit As String = VarIni.char8)
            Dim wb As New XLWorkbook
            ExportDatatable(wb, fileName, dt, colName, strSplit)
            downloadFile(wb, fileName)
        End Sub

        Sub Export(fileName As String, dt1 As DataTable, dt2 As DataTable)
            Using wb As New XLWorkbook()
                'sheet 1
                dt1.TableName = "Sheet1"
                Dim ws1 = wb.Worksheets.Add(dt1)
                ws1.Tables.FirstOrDefault.ShowAutoFilter = False
                'sheet2
                dt2.TableName = "Sheet2"
                Dim ws2 = wb.Worksheets.Add(dt2)
                ws2.Tables.FirstOrDefault.ShowAutoFilter = False
                downloadFile(wb, fileName)
            End Using
        End Sub
        '==========>> from datatable <<==========
        '==========>> download <<==========
        Public Sub downloadFile(wb As XLWorkbook, fileName As String)
            fileName &= " " & DateTime.Now.ToString("yyyyMMdd:hh:mm:ss") & ".xlsx"
            'Export the Excel file.
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.Charset = ""
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
            Using MyMemoryStream As New MemoryStream()
                wb.Worksheet(1).Columns.AdjustToContents()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream)
                HttpContext.Current.Response.Flush()
                HttpContext.Current.Response.End()
            End Using
        End Sub
        '==========>> download <<==========

        'Check Length limit Cell
        'add by wit 
        'add for count lenght of string is over 32767
        Function LengthValue(Value As String) As String
            If Value.Length > 32767 Then
                Value = Value.Substring(0, 32767)
            End If
            Return Value
        End Function

        'read excel from file
        Function ReadFileExcel(iFileUpload As FileUpload) As DataTable
            Dim tbl As New DataTable()
            Dim fileExten As String = Path.GetExtension(iFileUpload.FileName).Replace(".", "")
            If (iFileUpload.HasFile AndAlso (fileExten = "xlsx" Or fileExten = "xls")) Then
                Using workBook As New XLWorkbook(iFileUpload.PostedFile.InputStream)
                    Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
                    Dim firstRow As Boolean = True

                    For Each row As IXLRow In workSheet.Rows()
                        'Use the first row to add columns to DataTable.
                        If firstRow Then 'first row
                            For Each cell As IXLCell In row.Cells()
                                tbl.Columns.Add(cell.Value.ToString())
                            Next
                            firstRow = False
                        Else 'Add rows to DataTable.
                            tbl.Rows.Add()
                            Dim i As Integer = 0
                            For Each cell As IXLCell In row.Cells()
                                tbl.Rows(tbl.Rows.Count - 1)(i) = cell.Value.ToString()
                                i += 1
                            Next
                        End If
                    Next
                End Using
            End If
            Return tbl
        End Function




    End Class
End Namespace