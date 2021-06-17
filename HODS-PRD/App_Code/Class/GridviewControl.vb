Imports System.IO
Imports System.Data
Imports System.Drawing
Imports MIS_HTI.DataControl

Namespace FormControl
    Public Class GridviewControl
        Dim dbConn As New DataConnectControl
        Public gv As GridView

        'set add gridvew object
        Sub New(ByRef PI_GRIDVIEW As GridView)
            gv = PI_GRIDVIEW
        End Sub

        'Sub New(ByRef PI_GRIDVIEW As GridView,
        '        Optional gridviewClear As Boolean = True,
        '        Optional genRow As Boolean = False,
        '        Optional showFooter As Boolean = False)
        '    gv = PI_GRIDVIEW
        '    GridviewFormat(gridviewClear, genRow, showFooter)
        'End Sub

        Sub New(ByRef PI_GRIDVIEW As GridView,
                colName As ArrayList,
                Optional gridviewClear As Boolean = True,
                Optional genRow As Boolean = False,
                Optional showFooter As Boolean = False,
                Optional autoDatafield As Boolean = False,
                Optional strSplit As String = VarIni.C8)
            gv = PI_GRIDVIEW
            GridviewFormat(gridviewClear, genRow, showFooter)
            gridviewSetCol(colName, autoDatafield, strSplit)
        End Sub

        Public Sub GridviedwInitial(colName As ArrayList,
                                   Optional gridviewClear As Boolean = True,
                                   Optional genRow As Boolean = False,
                                   Optional showFooter As Boolean = False,
                                   Optional autoDatafield As Boolean = False,
                                   Optional strSplit As String = VarIni.C8)
            GridviewFormat(gridviewClear, genRow, showFooter)
            gridviewSetCol(colName, autoDatafield, strSplit)
        End Sub

        Public Sub GridviewFormat(Optional clear As Boolean = True,
                                  Optional autoGenRow As Boolean = False,
                                  Optional showFooter As Boolean = False)
            GridviewFormat(gv, clear, autoGenRow, showFooter)
        End Sub

        Public Sub gridviewSetCol(colName As ArrayList,
                                  Optional autoDatafield As Boolean = False,
                                  Optional strSplit As String = VarIni.C8)
            gridviewSetCol(gv, colName, autoDatafield, strSplit)
        End Sub

        Public Sub gridviewSetCol(colName As List(Of String),
                                  Optional strSplit As String = VarIni.C8,
                                  Optional autoDatafield As Boolean = False)
            gridviewSetCol(gv, colName, autoDatafield, strSplit)
        End Sub

        Public Sub GridviewColWithCommand(colList As List(Of String),
                                          commandList As List(Of String),
                                          Optional ByVal addFirst As Boolean = True,
                                          Optional strSplit As String = VarIni.C8,
                                          Optional clear As Boolean = True,
                                          Optional autoGenRow As Boolean = False,
                                          Optional showFooter As Boolean = False)
            GridviewFormat(gv, clear, autoGenRow, showFooter)
            If addFirst Then
                SetButtonField(commandList, strSplit)
            End If
            gridviewSetCol(gv, colList,, strSplit)
            If Not addFirst Then
                SetButtonField(commandList, strSplit)
            End If
        End Sub

        Public Sub GridviewColWithCommand(colList As ArrayList,
                                          commandList As List(Of String),
                                          Optional addFirst As Boolean = True,
                                          Optional strSplit As String = VarIni.C8,
                                          Optional clear As Boolean = True,
                                          Optional autoGenRow As Boolean = False,
                                          Optional showFooter As Boolean = False)
            GridviewFormat(gv, clear, autoGenRow, showFooter)
            If addFirst Then
                SetButtonField(commandList, strSplit)
            End If
            gridviewSetCol(gv, colList, strSplit)
            If Not addFirst Then
                SetButtonField(commandList, strSplit)
            End If
        End Sub

        Sub SetButtonField(commandList As List(Of String),
                           Optional strSplit As String = VarIni.C8)

            For Each str As String In commandList 'format of str = text show ||strSplit||image name
                Dim data() As String = str.Split(strSplit)
                Dim text As String = "Select"
                Dim buttonTypeVal As Integer = ButtonType.Link
                Dim fileName As String = "edit.gif"
                Dim line As Integer = 0
                For Each txt As String In data
                    Select Case line
                        Case 0
                            If Not String.IsNullOrEmpty(txt) Then
                                text = txt
                            End If
                        Case 1
                            If Not String.IsNullOrEmpty(txt) Then
                                fileName = txt
                            End If
                            buttonTypeVal = ButtonType.Image
                    End Select
                    line += 1
                Next


                Dim commandName As String = "On" & text.Replace(" ", "")
                If Not String.IsNullOrEmpty(text) AndAlso Not String.IsNullOrEmpty(commandName) Then
                    Dim bf As New ButtonField
                    With bf
                        .CommandName = commandName
                        .Text = text
                        .ItemStyle.HorizontalAlign = HorizontalAlign.Center
                        .ButtonType = buttonTypeVal
                        .ImageUrl = "~/Images/" & fileName
                        .HeaderText = text.ToUpper
                        .HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    End With

                    gv.Columns.Add(bf)
                End If
            Next
        End Sub

        Sub ShowGridView(colList As ArrayList,
                         commandList As List(Of String),
                         dt As DataTable,
                         Optional colorCode As String = "#FFCCFF",
                         Optional addFirst As Boolean = True,
                         Optional strSplit As String = VarIni.C8) 'set format and show data from datatable
            GridviewColWithCommand(colList, commandList, addFirst, strSplit)
            ShowGridView(dt, colorCode)
        End Sub
        Sub ShowGridView(colList As ArrayList,
                         commandList As List(Of String),
                         SQL As String, DBType As String,
                         Optional colorCode As String = "#FFCCFF",
                         Optional addFirst As Boolean = True,
                         Optional strSplit As String = VarIni.C8) 'set format and show data from query
            Dim dt As DataTable = dbConn.Query(SQL, DBType, dbConn.WhoCalledMe)
            ShowGridView(colList, commandList, dt, colorCode, addFirst, strSplit)
        End Sub

        Sub ShowGridView(colList As List(Of String),
                         commandList As List(Of String),
                         dt As DataTable,
                         Optional colorCode As String = "#FFCCFF",
                         Optional addFirst As Boolean = True,
                         Optional strSplit As String = VarIni.C8) 'set format and show data from datatable
            GridviewColWithCommand(colList, commandList, addFirst, strSplit)
            ShowGridView(dt, colorCode)
        End Sub
        Sub ShowGridView(colList As List(Of String),
                         commandList As List(Of String),
                         SQL As String, DBType As String,
                         Optional colorCode As String = "#FFCCFF",
                         Optional addFirst As Boolean = True,
                         Optional strSplit As String = VarIni.C8) 'set format and show data from query
            Dim dt As DataTable = dbConn.Query(SQL, DBType, dbConn.WhoCalledMe)
            ShowGridView(colList, commandList, dt, colorCode, addFirst, strSplit)
        End Sub

        Public Sub ShowGridView(dt As DataTable, Optional colorCode As String = "#FFCCFF")
            gridviewBind(gv, dt, colorCode)
        End Sub

        Sub ShowGridView(ColList As ArrayList, dt As DataTable,
                         Optional colorCode As String = "#FFCCFF",
                         Optional strSplit As String = VarIni.C8)
            GridviedwInitial(ColList, True, False, False, False, strSplit)
            ShowGridView(dt, colorCode)
        End Sub

        Sub ShowGridView(ColList As ArrayList, SQL As String, DBType As String,
                         Optional colorCode As String = "#FFCCFF",
                         Optional strSplit As String = VarIni.C8)
            Dim dt As DataTable = dbConn.Query(SQL, DBType, dbConn.WhoCalledMe)
            ShowGridView(ColList, dt, colorCode, strSplit)
        End Sub

        Sub ShowGridView(dt As DataTable,
                         Optional colorCode As String = "#FFCCFF",
                         Optional strSplit As String = VarIni.C8)
            ShowGridView(dt, colorCode)
        End Sub

        Sub ShowGridView(SQL As String, DBType As String,
                         Optional colorCode As String = "#FFCCFF",
                         Optional strSplit As String = VarIni.C8)
            Dim dt As DataTable = dbConn.Query(SQL, DBType, dbConn.WhoCalledMe)
            ShowGridView(dt, colorCode, strSplit)
        End Sub

        Public Function rowGridview() As Decimal
            Dim rowGrid As Decimal = gv.Rows.Count
            If rowGrid = 0 OrElse (rowGrid = 1 AndAlso gv.Rows(0).Cells(0).Text = "No Data Found") Then
                rowGrid = 0
            End If
            Return rowGrid

        End Function


        Sub AddTemplateField(Optional ObjecctType As String = "HyperLink",
                             Optional ByVal TextName As String = "Detail",
                             Optional TextID As String = "hplDetail")
            Dim tf As New TemplateField With {
                .HeaderText = TextName,
                .ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, TextName, TextID, ObjecctType)
            }
            gv.Columns.Add(tf)
        End Sub

        Sub AddButtonField(text As String,
                          Optional ButtonType As Integer = ButtonType.Link,
                          Optional fileImageName As String = "edit.gif",
                          Optional ItemAlign As Integer = HorizontalAlign.Center,
                          Optional HeadAlign As Integer = HorizontalAlign.Center)

            Dim bf As New ButtonField With {
                .CommandName = "On" & text.Replace(" ", ""),
                .Text = text,
                .ButtonType = ButtonType,
                .ImageUrl = "~/Images/" & fileImageName,
                .HeaderText = text.ToUpper
            }
            With bf
                .ItemStyle.HorizontalAlign = ItemAlign
                .HeaderStyle.HorizontalAlign = HeadAlign
            End With
            gv.Columns.Add(bf)
        End Sub


        Sub AddBoundField(col As List(Of String),
                                  Optional dataFieldManual As Boolean = False,
                                  Optional strSplit As String = VarIni.C8)
            For Each str As String In col
                gv.Columns.Add(SetBoundField(str, dataFieldManual, strSplit))
            Next
        End Sub
        Sub AddBoundField(col As ArrayList,
                                  Optional dataFieldManual As Boolean = False,
                                  Optional strSplit As String = VarIni.C8)
            For Each str As String In col
                gv.Columns.Add(SetBoundField(str, dataFieldManual, strSplit))
            Next
        End Sub

        Public Sub Format(Optional clear As Boolean = True,
                          Optional autoGenRow As Boolean = False,
                          Optional showFooter As Boolean = False,
                          Optional cssClass As Boolean = True)
            GridviewFormat(gv, clear, autoGenRow, showFooter, cssClass)
        End Sub


        'set no add gridvew object
        Sub New()
            gv = Nothing
        End Sub

        Public Sub MergeGridview(ByRef Gridview1 As GridView)
            For rowIndex As Integer = Gridview1.Rows.Count - 2 To 0 Step -1
                Dim gvRow As GridViewRow = Gridview1.Rows(rowIndex)
                Dim gvPreviousRow As GridViewRow = Gridview1.Rows(rowIndex + 1)
                For cellCount As Integer = 0 To gvRow.Cells.Count - 1
                    If gvRow.Cells(cellCount).Text = gvPreviousRow.Cells(cellCount).Text Then
                        If gvPreviousRow.Cells(cellCount).RowSpan < 2 Then
                            gvRow.Cells(cellCount).RowSpan = 2
                        Else
                            gvRow.Cells(cellCount).RowSpan = gvPreviousRow.Cells(cellCount).RowSpan + 1
                        End If
                        gvPreviousRow.Cells(cellCount).Visible = False
                    End If
                Next
            Next
        End Sub

        Public Sub MergeGridview(ByRef Gridview1 As GridView, ByVal toCol As Integer, Optional ByVal fromCol As Integer = 0)
            For rowIndex As Integer = Gridview1.Rows.Count - 2 To 0 Step -1
                Dim gvRow As GridViewRow = Gridview1.Rows(rowIndex)
                Dim gvPreviousRow As GridViewRow = Gridview1.Rows(rowIndex + 1)
                If gvRow.Cells.Count < fromCol Then
                    Exit Sub
                End If
                If gvRow.Cells.Count < toCol Then
                    Exit Sub
                End If
                For cellCount As Integer = fromCol To toCol - 1
                    If gvRow.Cells(cellCount).Text = gvPreviousRow.Cells(cellCount).Text Then
                        If gvPreviousRow.Cells(cellCount).RowSpan < 2 Then
                            gvRow.Cells(cellCount).RowSpan = 2
                        Else
                            gvRow.Cells(cellCount).RowSpan = gvPreviousRow.Cells(cellCount).RowSpan + 1
                        End If
                        gvRow.Cells(cellCount).HorizontalAlign = HorizontalAlign.Center
                        gvPreviousRow.Cells(cellCount).Visible = False
                    End If
                Next
            Next
        End Sub

        Public Sub gridviewMergRowCell(ByRef gridView As GridView)

            If gridView.Rows.Count = 0 Then
                Exit Sub
            End If
            Dim rowIndex As Integer = gridView.Rows.Count

            For rowIndex = rowIndex To rowIndex >= 2 Step rowIndex - 1
                Dim row As GridViewRow = gridView.Rows(rowIndex)
                Dim previousRow As GridViewRow = gridView.Rows(rowIndex - 1)
                Dim colIndex As Integer = 0
                For colIndex = 0 To colIndex < row.Cells.Count
                    If row.Cells(colIndex).Text = previousRow.Cells(colIndex).Text Then
                        row.Cells(colIndex).RowSpan += 1
                    End If
                Next
            Next
        End Sub

        Public Sub ExportGridViewToExcel(ByVal FileName As String, ByVal gv As GridView)
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>")
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" +
            HttpContext.Current.Server.UrlEncode(FileName) & " " & DateTime.Now.ToString("yyyyMMdd hhmmss") & ".xls")

            HttpContext.Current.Response.Charset = ""
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
            Dim StringWriter As New System.IO.StringWriter
            Dim HtmlTextWriter As New HtmlTextWriter(StringWriter)
            gv.AllowSorting = False
            gv.AllowPaging = False
            gv.EnableViewState = False
            gv.AutoGenerateColumns = False
            gv.RenderControl(HtmlTextWriter)
            HttpContext.Current.Response.Write(StringWriter.ToString())
            HttpContext.Current.Response.End()
        End Sub

        Public Sub GridviewColWithLinkFirst(ByRef gv As GridView, ByVal col() As String,
                                            Optional ByVal isHyperlink As Boolean = False,
                                            Optional ByVal textName As String = "Detail",
                                            Optional strSplit As String = ":")
            GridviewFormat(gv)
            If isHyperlink Then
                Dim firstCol As TemplateField = New TemplateField
                firstCol.HeaderText = textName
                firstCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, textName, "hplDetail", "HyperLink")
                gv.Columns.Add(firstCol)
            End If
            'GridviewFormat(gv)
            gridviewSetCol(gv, col, strSplit)
            'GridviewOnMouseOver(gv, colorCode)
        End Sub

        Public Sub GridviewColWithLinkFirst(ByRef gv As GridView, ByVal col As ArrayList,
                                            Optional ByVal isHyperlink As Boolean = False,
                                            Optional ByVal textName As String = "Detail",
                                            Optional strSplit As String = ":")
            GridviewFormat(gv)
            If isHyperlink Then
                Dim firstCol As TemplateField = New TemplateField
                firstCol.HeaderText = textName
                firstCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, textName, "hplDetail", "HyperLink")
                gv.Columns.Add(firstCol)
            End If
            'GridviewFormat(gv)
            gridviewSetCol(gv, col,, strSplit)
        End Sub

        Public Sub GridviewColWithButton(ByRef gv As GridView, ByVal col As ArrayList, textName As String,
                                            Optional strSplit As String = ":")
            GridviewFormat(gv)

            Dim firstCol As TemplateField = New TemplateField
            firstCol.HeaderText = textName
            firstCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, textName, "btFirst", "Button", ,, "cmdFirst")
            gv.Columns.Add(firstCol)
            gridviewSetCol(gv, col,, strSplit)
        End Sub

        Public Sub GridviewInitial(ByRef gv As GridView, ByVal col() As String,
                                   Optional gridviewClear As Boolean = True, Optional genRow As Boolean = False,
                                   Optional showFooter As Boolean = False, Optional autoDatafield As Boolean = False,
                                   Optional strSplit As String = ":")
            GridviewFormat(gv, gridviewClear, genRow, showFooter)
            gridviewSetCol(gv, col, autoDatafield, strSplit)
        End Sub

        Public Sub GridviewInitial(ByRef gv As GridView, ByVal col As ArrayList,
                                   Optional gridviewClear As Boolean = True, Optional genRow As Boolean = False,
                                   Optional showFooter As Boolean = False, Optional autoDatafield As Boolean = False,
                                   Optional strSplit As String = ":")
            GridviewFormat(gv, gridviewClear, genRow, showFooter)
            gridviewSetCol(gv, col, autoDatafield, strSplit)
        End Sub

        Public Sub GridviewFormat(ByRef gv As GridView, Optional clear As Boolean = True,
                                  Optional autoGenRow As Boolean = False,
                                  Optional showFooter As Boolean = False,
                                  Optional cssClass As Boolean = False)
            With gv
                If clear Then
                    .Columns.Clear()
                End If
                .AutoGenerateColumns = autoGenRow
                .BackColor = Drawing.Color.White
                .BorderColor = Drawing.Color.Blue
                .BorderStyle = BorderStyle.None
                .BorderWidth = 1
                .CellPadding = 4
                .ShowFooter = showFooter
                With .FooterStyle
                    .BackColor = Drawing.Color.LightBlue
                    .ForeColor = Drawing.Color.DarkBlue
                End With

                With .HeaderStyle
                    .BackColor = Drawing.Color.DarkBlue
                    .Font.Bold = True
                    .ForeColor = Drawing.Color.Lavender
                    .BorderColor = Drawing.Color.White
                    .HorizontalAlign = HorizontalAlign.Center
                End With

                With .PagerStyle
                    .BackColor = Drawing.Color.LightBlue
                    .ForeColor = Drawing.Color.Lavender
                    .HorizontalAlign = HorizontalAlign.Left
                End With
                With .RowStyle
                    .BackColor = Drawing.Color.White
                    .BorderColor = Drawing.Color.Blue
                    .Wrap = False
                End With
                With .SelectedRowStyle
                    .Font.Bold = True
                    .BackColor = Drawing.Color.LightBlue
                    .ForeColor = Drawing.Color.Lavender
                End With
                If cssClass Then
                    .CssClass = "table table-sm table-condensed table-striped table-hover table-bordered border-primary"
                End If
            End With

        End Sub

        Public Function getColIndexByName(ByRef gv As GridView, ByVal name As String) As Integer
            For i As Integer = 0 To gv.Columns.Count - 1
                If gv.Columns(i).HeaderText.ToLower.Trim = name.ToLower.Trim Then
                    Return i
                End If
            Next
            Return -1
        End Function

        Private Sub gridviewSetCol(ByRef gv As GridView, ByVal col() As String, Optional dataFieldManual As Boolean = False, Optional strSplit As String = ":")
            For Each str As String In col
                gv.Columns.Add(setBoundField(str, dataFieldManual, strSplit))
            Next
        End Sub

        'new add
        Private Sub gridviewSetCol(ByRef gv As GridView, ByVal col As ArrayList, Optional dataFieldManual As Boolean = False, Optional strSplit As String = ":")
            For Each str As String In col
                gv.Columns.Add(setBoundField(str, dataFieldManual, strSplit))
            Next
        End Sub

        Private Sub gridviewSetCol(ByRef gv As GridView, ByVal col As List(Of String), Optional dataFieldManual As Boolean = False, Optional strSplit As String = ":")
            For Each str As String In col
                gv.Columns.Add(SetBoundField(str, dataFieldManual, strSplit))
            Next
        End Sub


        Private Function SetBoundField(str As String, Optional dataFieldManual As Boolean = False, Optional strSplit As String = ":") As BoundField
            Dim bf As New BoundField
            Dim temp() As String = str.Split(strSplit)
            Dim valCheck As Integer = If(dataFieldManual, 2, 3)

            If temp.Length >= valCheck - 1 Then
                bf.HeaderText = If(String.IsNullOrEmpty(temp(0)), temp(1), temp(0))
                If Not dataFieldManual Then
                    bf.DataField = temp(1)
                End If
                If temp.Length = valCheck Then
                    Dim decLen As Integer = temp(valCheck - 1)
                    Dim decTxt As String = ""
                    If decLen > 0 Then
                        decTxt = "."
                        For i As Integer = 1 To decLen
                            decTxt &= "#"
                        Next
                    End If
                    bf.DataFormatString = ("{0:#,#_DEC_TEXT_}").Replace("_DEC_TEXT_", decTxt)
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                End If
                Return bf
            Else
                Return Nothing
            End If

        End Function

        Private Sub gridviewSetCol(ByRef gv As GridView, ByVal col As ArrayList, Optional strSpit As String = ":")
            'Dim hdRow0 As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            'Dim hd2 As TableCell = New TableCell
            'hd2.Text = "test Merge Column"
            'hd2.ColumnSpan = col.Count
            'hd2.HorizontalAlign = HorizontalAlign.Center
            'hdRow0.Cells.Add(hd2)
            'gv.Controls(0).Controls.AddAt(1, hdRow0)

            Dim hdRow1 As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            For Each str As String In col
                Dim temp() As String = str.Split(strSpit)
                Dim bf As BoundField = New BoundField
                bf.HeaderText = temp(0)
                bf.DataField = temp(1)
                If temp.Length = 3 Then
                    Dim fm As String = "{0:#,#}"
                    Select Case CInt(temp(2))
                        Case 1
                            fm = "{0:#,#.#}"
                        Case 2
                            fm = "{0:#,#.##}"
                    End Select
                    bf.DataFormatString = fm
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                End If
                gv.Columns.Add(bf)
            Next

        End Sub
        'add sub head create by noi on 2016-06-21
        Public Sub gridviewAddSubHeader(ByRef gv As GridView, ByVal col As ArrayList, Optional strSpit As String = ":")

            Dim hdRow1 As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            For Each str As String In col
                Dim temp() As String = str.Split(strSpit)
                Dim cell As New TableCell()
                With cell
                    .Text = temp(0)
                    .Font.Bold = True
                    .BackColor = Drawing.Color.DarkBlue
                    .BorderColor = Drawing.Color.White
                    .ForeColor = Drawing.Color.Lavender
                    .HorizontalAlign = HorizontalAlign.Center
                    .ColumnSpan = If(temp(1) = String.Empty Or Not IsNumeric(temp(1)), 1, CInt(temp(1)))
                End With
                hdRow1.Cells.Add(cell)

            Next
            If hdRow1.Cells.Count > 0 Then
                gv.Controls(0).Controls.AddAt(0, hdRow1)
            End If
        End Sub
        'add sub head create by noi on 2017-07-23
        Public Sub gridviewAddSubHeader(ByRef gv As GridView, ByVal col() As String, Optional strSpit As String = ":")

            Dim hdRow1 As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            For Each str As String In col
                Dim temp() As String = str.Split(strSpit)

                Dim cell As New TableCell()
                With cell
                    .Text = temp(0)
                    .Font.Bold = True
                    .BackColor = Drawing.Color.DarkBlue
                    .BorderColor = Drawing.Color.White
                    .ForeColor = Drawing.Color.Lavender
                    .HorizontalAlign = HorizontalAlign.Center
                    .ColumnSpan = If(temp(1) = String.Empty Or Not IsNumeric(temp(1)), 1, CInt(temp(1)))
                End With
                hdRow1.Cells.Add(cell)
            Next
            If hdRow1.Cells.Count > 0 Then
                gv.Controls(0).Controls.AddAt(0, hdRow1)
            End If
        End Sub

        'add 2017-07-23 by noi
        Public Sub gridviewAddFooter(ByRef gv As GridView, ByVal col As ArrayList, Optional strSpit As String = ":")
            Dim hdRow1 As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal)
            For Each str As String In col
                Dim temp() As String = str.Split(strSpit)
                Dim cell As New TableCell()
                cell.Text = temp(0)
                cell.ForeColor = System.Drawing.Color.Blue
                cell.BorderColor = ColorTranslator.FromHtml("#BDBDBD")
                cell.BackColor = ColorTranslator.FromHtml("#848484")

                cell.HorizontalAlign = HorizontalAlign.Center
                cell.ColumnSpan = If(temp(1) = "" Or Not IsNumeric(temp(1)), 1, CInt(temp(1)))
                hdRow1.Cells.Add(cell)

            Next
            If hdRow1.Cells.Count > 0 Then
                gv.Controls(0).Controls.AddAt(0, hdRow1)
            End If
        End Sub

        Public Sub gridviewAddFooter(ByRef gv As GridView, ByVal col() As String, Optional strSpit As String = ":")
            Dim hdRow1 As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal)
            For Each str As String In col
                Dim temp() As String = str.Split(strSpit)
                Dim cell As New TableCell()
                cell.Text = temp(0)
                cell.ForeColor = System.Drawing.Color.Blue
                cell.BorderColor = ColorTranslator.FromHtml("#BDBDBD")
                cell.BackColor = ColorTranslator.FromHtml("#848484")

                cell.HorizontalAlign = HorizontalAlign.Center
                cell.ColumnSpan = If(temp(1) = "" Or Not IsNumeric(temp(1)), 1, CInt(temp(1)))
                hdRow1.Cells.Add(cell)

            Next
            If hdRow1.Cells.Count > 0 Then
                gv.Controls(0).Controls.AddAt(0, hdRow1)
            End If
        End Sub

        'add 2015/06/11 by noi
        Public Sub gridviewWithFirstImage(ByRef gv As GridView, ByVal col As ArrayList)
            GridviewFormat(gv) 'set gridview format
            Dim cnt As Integer = 1
            For Each str As String In col
                Dim temp() As String = str.Split(":")
                If cnt = 1 Then
                    Dim imgB As ImageField = New ImageField
                    imgB.HeaderText = temp(0)
                    imgB.DataImageUrlField = temp(1)
                    gv.Columns.Add(imgB)
                Else
                    Dim bf As BoundField = New BoundField
                    bf.HeaderText = temp(0)
                    bf.DataField = temp(1)
                    If temp.Length = 3 Then
                        Dim fm As String = "{0:#,#}"
                        Select Case CInt(temp(2))
                            Case 1
                                fm = "{0:#,#.#}"
                            Case 2
                                fm = "{0:#,#.##}"
                        End Select
                        bf.DataFormatString = fm
                        bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                    End If
                    gv.Columns.Add(bf)
                End If
                cnt += 1
            Next
        End Sub

        Public Sub ShowGridView(ByRef gv As GridView, dt As DataTable, Optional colorCode As String = "#FFCCFF")
            gridviewBind(gv, dt, colorCode)
        End Sub

        Public Sub ShowGridView(ByRef gv As GridView, ds As DataSet, Optional colorCode As String = "#FFCCFF")
            Try
                If ds.Tables(0).Rows.Count > 0 Then
                    gridviewBind(gv, ds, colorCode)
                Else
                    gridviewZero(gv, "No Data Found")
                End If
            Catch ex As Exception
                'gridviewZero(gv, "Error Data Please contract IT Programmer(173,174)")
            End Try
        End Sub

        Public Sub ShowGridView(ByRef gv As GridView, ds As Object, Optional colorCode As String = "#FFCCFF")
            If TypeOf ds Is DataSet Or TypeOf ds Is DataTable Then
                If TypeOf ds Is DataSet Then 'dataset
                    ShowGridView(gv, TryCast(ds, DataSet), colorCode)
                Else 'datatable
                    ShowGridView(gv, TryCast(ds, DataTable), colorCode)
                End If
            End If
        End Sub

        Public Sub ShowGridView(ByRef gv As GridView, SQL As String, DBType As String, Optional colorCode As String = "#FFCCFF")
            Dim ds As DataTable = dbConn.Query(SQL, DBType, dbConn.WhoCalledMe)
            gridviewBind(gv, ds, colorCode)
        End Sub

        'Sub ShowGridView(ByRef gv As GridView,
        '                 SQL As String, DBType As String,
        '                 colName As List(Of String),
        '                 Optional colorCode As String = "#FFCCFF")

        'End Sub

        Private Sub gridviewZero(ByRef gv As GridView, msg As String)
            Dim ds As New DataTable
            ds.Rows.Add(ds.NewRow())
            gridviewBind(gv, ds)
            gv.Rows(0).Cells.Clear()
            gv.Rows(0).Cells.Add(New TableCell())
            gv.Rows(0).Cells(0).ColumnSpan = gv.Rows(0).Cells.Count
            gv.Rows(0).Cells(0).Text = msg
        End Sub

        Private Sub gridviewBind(ByRef gv As GridView, ds As DataSet, Optional colorCode As String = "#FFCCFF")
            gv.DataSource = ds
            gv.DataBind()
            GridviewOnMouseOver(gv, colorCode)
        End Sub

        Private Sub gridviewBind(ByRef gv As GridView, ds As DataTable, Optional colorCode As String = "#FFCCFF")
            gv.DataSource = ds
            gv.DataBind()
            GridviewOnMouseOver(gv, colorCode)
        End Sub

        Private Sub GridviewOnMouseOver(ByRef gv As GridView, Optional htmlColor As String = "#FFCCFF")
            For Each row As GridViewRow In gv.Rows
                row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='" & htmlColor & "'")
                row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            Next
        End Sub

        Public Function rowGridview(ByRef gridview As GridView) As Decimal
            Dim rowGrid As Decimal = gridview.Rows.Count
            If rowGrid = 0 Then
                rowGrid = 0
            ElseIf rowGrid = 1 And gridview.Rows(0).Cells(0).Text = "No Data Found" Then
                rowGrid = 0
            End If
            Return rowGrid

        End Function

        'Public Function nameHeader(ByVal progName As String) As String
        '    Dim strHeader As String = ""
        '    Dim SQL As String,
        '    dt As New DataTable
        '    SQL = "select ParentId,Name from Menu where Prog='" & progName.Substring(1, progName.Length - 1) & "' "
        '    dt = dbConn.Query(SQL, VarIni.DBMIST100TST, dbConn.WhoCalledMe)
        '    If dt.Rows.Count > 0 Then
        '        With dt.Rows(0)
        '            strHeader = .Item("Name").ToString.Trim
        '            strHeader = getName(.Item("ParentId"), strHeader)
        '        End With
        '    End If
        '    Return strHeader
        'End Function
        'Private Function getName(ByVal pID As Integer, ByVal str As String) As String
        '    Dim SQL As String,
        '       dt As New DataTable
        '    SQL = "select ParentId,Name from Menu where Id='" & pID & "' "
        '    dt = dbConn.Query(SQL, VarIni.DBMIST100TST, dbConn.WhoCalledMe)
        '    If dt.Rows.Count > 0 Then
        '        With dt.Rows(0)
        '            str = .Item("Name").ToString.Trim & " -> " & str
        '            str = getName(.Item("ParentId"), str)
        '        End With
        '    End If
        '    Return str
        'End Function

        Function IndexList(gv As GridView) As Hashtable
            Dim hashCont As New HashtableControl
            Dim tempHash As New Hashtable

            For i As Integer = 0 To gv.Columns.Count - 1
                Dim dataName As String = String.Empty
                If gv.Columns(i).GetType.Name = "BoundField" Then
                    Dim dtColumn As BoundField = gv.Columns(i)
                    dataName = dtColumn.DataField
                Else
                    dataName = gv.Columns(i).HeaderText.Trim
                End If
                hashCont.addDataHash(tempHash, i, dataName)
            Next
            Return tempHash
        End Function

        Sub clearGridview(ByRef gv As GridView)
            gv.DataSource = ""
            gv.DataBind()
        End Sub

        Sub VisibleColumn(ByRef gv As GridView, column_index As Integer, Optional visible As Boolean = True)
            gv.Columns(column_index).Visible = visible
        End Sub
        Sub hideColumn(ByRef gv As GridView, colIndex As Integer, cssClass As String)
            With gv.Columns(colIndex)
                .ItemStyle.CssClass = cssClass
                .HeaderStyle.CssClass = cssClass
            End With
        End Sub

        Sub hideColumn(ByRef gv As GridView, colIndexList As List(Of Integer), cssClass As String)
            For Each i As Integer In colIndexList
                hideColumn(gv, i, cssClass)
            Next
        End Sub

        Sub hideColumn(ByRef gv As GridView, al As ArrayList, cssClass As String, Optional strSplit As String = VarIni.C8)
            Dim arcont As New ArrayListControl(al)
            hideColumn(gv, arcont.getColHide(al, strSplit), cssClass)
        End Sub
    End Class
End Namespace

