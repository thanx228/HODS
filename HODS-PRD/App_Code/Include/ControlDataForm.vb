Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel

Public Class ControlDataForm
    Inherits System.Web.UI.Page
    Shared Conn_SQL As New ConnSQL

    Public Sub ShowGridView(ByRef Gridview1 As Object, sqlCommand As String, Optional connectSting As String = "")
        If connectSting = "" Then
            connectSting = Conn_SQL.MIS_ConnectionString
        End If

        Dim da As New SqlDataAdapter(sqlCommand, connectSting)
        Dim ds As New DataSet
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            Gridview1.DataSource = ds
            Gridview1.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            Gridview1.DataSource = ds
            Gridview1.DataBind()
            Dim ColumnCount As Integer = Gridview1.Rows(0).Cells.Count
            Gridview1.Rows(0).Cells.Clear()
            Gridview1.Rows(0).Cells.Add(New TableCell())
            Gridview1.Rows(0).Cells(0).ColumnSpan = ColumnCount
            Gridview1.Rows(0).Cells(0).Text = "No Data Found"
        End If
    End Sub

    Public Sub ShowGridView(ByRef Gridview1 As GridView, dt As DataTable)
        Gridview1.DataSource = dt
        Gridview1.DataBind()
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
                    gvPreviousRow.Cells(cellCount).Visible = False
                End If
            Next
        Next
    End Sub



    'Public Sub showDDL(ByRef ControlDDL As DropDownList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal setAll As Boolean = False, Optional ByVal connectSting As String = "")

    '    If connectSting = "" Then
    '        connectSting = Conn_SQL.MIS_ConnectionString
    '    End If

    '    Dim da As New SqlDataAdapter(str_sqlcommand, connectSting)
    '    Dim ds As New DataSet
    '    da.Fill(ds)

    '    With ControlDDL
    '        .DataSource = ds
    '        .DataTextField = fldText
    '        .DataValueField = fldValue
    '        .DataBind()
    '        If setAll = True Then
    '            .Items.Insert(0, "ALL")
    '        End If
    '    End With
    'End Sub


    Public Sub showDDL(ByRef ControlDDL As DropDownList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal setAll As Boolean = False, Optional ByVal connectSting As String = "", Optional ByVal headVal As String = "ALL")
        If connectSting = "" Then
            connectSting = Conn_SQL.MIS_ConnectionString
        End If

        Dim da As New SqlDataAdapter(str_sqlcommand, connectSting)
        Dim ds As New DataSet
        da.Fill(ds)

        With ControlDDL
            .DataSource = ds
            .DataTextField = fldText
            .DataValueField = fldValue
            .DataBind()
            If setAll = True Then
                .Items.Insert(0, headVal)
            End If
        End With
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
    'Public Sub checkLogIn()
    '    If Session("UserName") = "" Then
    '        Response.Redirect(Server.UrlPathEncode("/Login.aspx"))
    '    End If
    'End Sub
    ' public static void MergeRows(GridView gridView)
    '{
    '    for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
    '    {
    '        GridViewRow row = gridView.Rows[rowIndex];
    '        GridViewRow previousRow = gridView.Rows[rowIndex + 1];

    '        for (int i = 0; i < row.Cells.Count; i++)
    '        {
    '            if (row.Cells[i].Text == previousRow.Cells[i].Text)
    '            {
    '                row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : 
    '                                       previousRow.Cells[i].RowSpan + 1;
    '                previousRow.Cells[i].Visible = false;
    '            }
    '        }
    '    }
    '}



    'include in class call me
    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    '    'Save To Excel File
    'End Sub
    'Public Sub ExportFromGrid(ByRef gv As Object, ByVal _contentType As String, ByVal fileName As String)
    '    Response.ClearContent()
    '    Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    Response.ContentType = _contentType
    '    Dim sw As New StringWriter()
    '    Dim htw As New HtmlTextWriter(sw)
    '    Dim frm As New HtmlForm()
    '    frm.Attributes("runat") = "server"
    '    frm.Controls.Add(gv)
    '    gv.RenderControl(htw)
    '    Response.Write(sw.ToString())
    '    Response.End()
    'End Sub

    Public Sub ExportGridViewToExcel(ByVal FileName As String, ByVal gv As GridView)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>")
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + _
        HttpContext.Current.Server.UrlEncode(FileName) & ".xls")

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

    Public Sub showCheckboxList(ByRef conCheckboxList As CheckBoxList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal showColumn As Decimal = 0, Optional ByVal connectSting As String = "")
        Try
            If connectSting = "" Then
                connectSting = Conn_SQL.MIS_ConnectionString
            End If

            Dim da As New SqlDataAdapter(str_sqlcommand, connectSting)
            Dim ds As New DataSet
            da.Fill(ds)

            With conCheckboxList
                .DataSource = ds
                .DataTextField = fldText
                .DataValueField = fldValue
                .DataBind()
                .RepeatColumns = showColumn
                .RepeatDirection = RepeatDirection.Horizontal
                .RepeatLayout = RepeatLayout.Table
            End With
        Catch ex As Exception

        End Try
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

    Public Sub UploadDataTableToExcel(ByVal dt As DataTable, ByRef filename As String)

        Dim attachment As String = "attachment; filename=" & filename
        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.AddHeader("content-disposition", attachment)
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        Dim tab As String = String.Empty
        For Each dtcol As DataColumn In dt.Columns
            HttpContext.Current.Response.Write(tab + dtcol.ColumnName)
            tab = vbTab
        Next
        HttpContext.Current.Response.Write(vbLf)
        For Each dr As DataRow In dt.Rows
            tab = ""
            For j As Integer = 0 To dt.Columns.Count - 1
                HttpContext.Current.Response.Write(tab & Convert.ToString(dr(j)))
                tab = vbTab
            Next
            HttpContext.Current.Response.Write(vbLf)
        Next
        HttpContext.Current.Response.[End]()

    End Sub

    Public Function nameHeader(ByVal progName As String) As String
        Dim strHeader As String = ""

        Dim SQL As String,
            dt As New DataTable
        SQL = "select ParentId,Name from Menu where Prog='" & progName.Substring(1, progName.Length - 1) & "' "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                strHeader = .Item("Name").ToString.Trim
                strHeader = getName(.Item("ParentId"), strHeader)
            End With
        End If
        Return strHeader
    End Function

    Private Function getName(ByVal pID As Integer, ByVal str As String) As String
        Dim SQL As String,
           dt As New DataTable
        SQL = "select ParentId,Name from Menu where Id='" & pID & "' "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                str = .Item("Name").ToString.Trim & " -> " & str
                str = getName(.Item("ParentId"), str)
            End With
        End If
        Return str
    End Function

    'Public Sub GridviewColWithLinkFirst(ByRef gv As GridView, ByVal col As ArrayList, Optional ByVal isHyperlink As Boolean = False)
    '    GridviewFormat(gv)
    '    If isHyperlink Then
    '        Dim firstCol As TemplateField = New TemplateField
    '        firstCol.HeaderText = "Detail"
    '        firstCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, "Detail", "hplDetail", "HyperLink")
    '        gv.Columns.Add(firstCol)
    '    End If
    '    'GridviewFormat(gv)
    '    gridviewSetCol(gv, col)

    'End Sub

    'Public Sub GridviewColWithLinkFirst(ByRef gv As GridView, ByVal col() As String, Optional ByVal isHyperlink As Boolean = False)
    '    GridviewFormat(gv)

    '    If isHyperlink Then
    '        Dim firstCol As TemplateField = New TemplateField
    '        firstCol.HeaderText = "Detail"
    '        firstCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, "Detail", "hplDetail", "HyperLink")
    '        gv.Columns.Add(firstCol)
    '    End If

    '    For Each str As String In col
    '        Dim temp() As String = str.Split(":")
    '        Dim bf As BoundField = New BoundField
    '        bf.HeaderText = temp(0)
    '        bf.DataField = temp(1)
    '        If temp.Length = 3 Then
    '            Dim fm As String = "{0:#,#}"
    '            Select Case CInt(temp(2))
    '                Case 1
    '                    fm = "{0:#,#.#}"
    '                Case 2
    '                    fm = "{0:#,#.##}"
    '            End Select
    '            bf.DataFormatString = fm
    '            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
    '        End If
    '        gv.Columns.Add(bf)
    '    Next

    'End Sub

    Public Sub GridviewInitial(ByRef gv As GridView, ByVal col() As String, Optional genRow As Boolean = False, Optional showFooter As Boolean = False)
        GridviewFormat(gv, genRow, showFooter)
        gridviewSetCol(gv, col)
    End Sub

    Public Sub GridviewInitial(ByRef gv As GridView, ByVal col As ArrayList, Optional genRow As Boolean = False, Optional showFooter As Boolean = False)
        GridviewFormat(gv, genRow, showFooter)
        gridviewSetCol(gv, col)
    End Sub

    Public Sub GridviewColWithLinkFirst(ByRef gv As GridView, ByVal col() As String, Optional ByVal isHyperlink As Boolean = False, Optional ByVal textName As String = "Detail")
        GridviewFormat(gv)
        If isHyperlink Then
            Dim firstCol As TemplateField = New TemplateField
            firstCol.HeaderText = textName
            firstCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, textName, "hplDetail", "HyperLink")
            gv.Columns.Add(firstCol)
        End If
        'GridviewFormat(gv)
        gridviewSetCol(gv, col)

    End Sub

    Public Sub GridviewColWithLinkFirst(ByRef gv As GridView, ByVal col As ArrayList, Optional ByVal isHyperlink As Boolean = False, Optional ByVal textName As String = "Detail", Optional ByVal isHyperlink2 As Boolean = False, Optional ByVal textName2 As String = "Detail2")
        GridviewFormat(gv)
        If isHyperlink Then
            Dim firstCol As TemplateField = New TemplateField
            'firstCol.HeaderText = textName
            firstCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, textName, "hplDetail", "HyperLink")
            gv.Columns.Add(firstCol)
        End If
        If isHyperlink2 Then
            Dim firstCol As TemplateField = New TemplateField
            'firstCol.HeaderText = textName
            firstCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, textName2, "hplDetail2", "HyperLink")
            gv.Columns.Add(firstCol)
        End If

        'GridviewFormat(gv)
        gridviewSetCol(gv, col)

    End Sub

    Public Sub GridviewFormat(ByRef gv As GridView, Optional ByVal autoGenRow As Boolean = False, Optional showFooter As Boolean = False)
        With gv
            .Columns.Clear()
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
        End With

    End Sub

    Public Sub GridviewGenCol(ByRef gv As GridView, ByVal col() As String)
        GridviewFormat(gv) 'set gridview format
        For Each str As String In col
            Dim temp() As String = str.Split(":")
            'for control   = object type:Name head:object ID:cssClass
            If temp(0).Substring(0, 1) = "_" Then 'is control
                Dim gridviewCol As TemplateField = New TemplateField
                'gridviewCol.HeaderTemplate = New GridviewTemplete(DataControlRowType.Header, temp(1).Trim)
                Dim objectType As String = "",
                    headName As String = "",
                    objectID As String = temp(2).Trim,
                    cssClass As String = ""
                If temp.Length = 4 Then
                    cssClass = temp(3).Trim
                End If
                Select Case temp(0).Replace("_", "").ToUpper
                    Case "A"
                        objectType = "Button"
                        headName = temp(1).ToString.Trim
                    Case "B"
                        objectType = "ImageButton"
                    Case "C"
                        objectType = "HyperLink"
                        headName = temp(1).ToString.Trim
                    Case "D"
                        objectType = "CheckBox"
                    Case "E"
                        objectType = "TextBox"
                    Case "F"
                        objectType = "Image"
                    Case "G"
                        objectType = "Lable"
                        headName = temp(1).ToString.Trim
                    Case "H"
                        objectType = "DropDownList"
                End Select
                'gridviewCol.ItemTemplate = New GridviewTemplete(DataControlRowType.DataRow, headName, objectID, objectType, cssClass)
                gv.Columns.Add(gridviewCol)
                ' for text show = name head:data field for databound : (optional)number show(1=1decimon and 2 =2decimon)
            Else 'is text show
                Dim bf As BoundField = New BoundField
                bf.HeaderText = temp(0)
                bf.DataField = temp(1)
                If temp.Length = 3 Then
                    Dim decFormat As String = ""
                    If temp(2).Trim <> "" And IsNumeric(temp(2).Trim) And CInt(temp(2).Trim) > 0 Then
                        decFormat = "."
                        For i As Integer = 1 To CInt(temp(2).Trim)
                            decFormat &= "#"
                        Next
                    End If
                    bf.DataFormatString = "{0:#,#" & decFormat & "}"
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                End If
                gv.Columns.Add(bf)
            End If
        Next

    End Sub

    Public Function getColIndexByName(ByRef gv As GridView, ByVal name As String) As Integer
        For i As Integer = 0 To gv.Columns.Count - 1
            If gv.Columns(i).HeaderText.ToLower.Trim = name.ToLower.Trim Then
                Return i
            End If
        Next
        Return -1
    End Function

    'Private Sub gridviewSetCol(ByRef gv As GridView, ByVal col As ArrayList)

    '    For Each str As String In col
    '        Dim temp() As String = str.Split(":")
    '        Dim bf As BoundField = New BoundField
    '        bf.HeaderText = temp(0)
    '        bf.DataField = temp(1)
    '        If temp.Length = 3 Then
    '            Dim fm As String = "{0:#,#}"
    '            Select Case CInt(temp(2))
    '                Case 1
    '                    fm = "{0:#,#.#}"
    '                Case 2
    '                    fm = "{0:#,#.##}"
    '            End Select
    '            bf.DataFormatString = fm
    '            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
    '        End If
    '        gv.Columns.Add(bf)
    '    Next
    'End Sub

   
    Private Sub gridviewSetCol(ByRef gv As GridView, ByVal col() As String)

        For Each str As String In col
            Dim temp() As String = str.Split(":")
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
                    Case 3
                        fm = "{0:#,#.###}"
                End Select
                bf.DataFormatString = fm
                bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
            End If
            'If temp.Length = 3 Then
            '    Dim fm As String = ""
            '    If IsNumeric(temp(2)) Then
            '        fm = "{0:#,#}"
            '        Select Case CInt(temp(2))
            '            Case 1
            '                fm = "{0:#,#.#}"
            '            Case 2
            '                fm = "{0:#,#.##}"
            '        End Select
            '        bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
            '    Else
            '        Select Case temp(2)
            '            Case "D"
            '                fm = "{0:dd-MM-yyyy}"
            '            Case Else
            '                fm = "{0:d}"
            '        End Select
            '    End If
            '    bf.DataFormatString = fm
            'End If
            gv.Columns.Add(bf)
        Next
    End Sub
    'new add
    Private Sub gridviewSetCol(ByRef gv As GridView, ByVal col As ArrayList)
        'Dim hdRow0 As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
        'Dim hd2 As TableCell = New TableCell
        'hd2.Text = "test Merge Column"
        'hd2.ColumnSpan = col.Count
        'hd2.HorizontalAlign = HorizontalAlign.Center
        'hdRow0.Cells.Add(hd2)
        'gv.Controls(0).Controls.AddAt(1, hdRow0)

        Dim hdRow1 As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
        For Each str As String In col
            Dim temp() As String = str.Split(":")
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
            'If temp.Length = 3 Then
            '    Dim fm As String = ""
            '    If IsNumeric(temp(2)) Then
            '        fm = "{0:#,#}"
            '        Select Case CInt(temp(2))
            '            Case 1
            '                fm = "{0:#,#.#}"
            '            Case 2
            '                fm = "{0:#,#.##}"
            '        End Select
            '        bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right
            '    Else
            '        Select Case temp(2)
            '            Case "D"
            '                fm = "{0:dd-MM-yyyy}"
            '            Case Else
            '                fm = "{0:d}"
            '        End Select
            '    End If
            '    bf.DataFormatString = fm
            'End If
            'hdRow1.
            gv.Columns.Add(bf)
        Next

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


    'add 2015-07-20 by noi format date val=yyyyMMdd
    Function getDoc(ByVal fld As String, ByVal tableName As String, ByVal whr As String, ByVal dateVal As String, Optional ByVal formatDate As String = "yyyymmdd", Optional ByVal cntSeq As Integer = 3) As String

        If whr <> "" Then
            whr = " where " & whr
        End If

        Dim SQL As String = "select isnull(max(isnull(" & fld & ",'0')),'0') docMax from " & tableName & whr
        Dim dt As DataTable
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Dim docNo As String = ""
        If dt.Rows.Count > 0 Then
            Dim valData As Decimal = CDec(dt.Rows(0).Item("docMax").ToString.Trim)
            If valData = 0 Then
                If dateVal = "" Then
                    Dim mm As String = Date.Today.Month.ToString,
                                        dd As String = Date.Today.Day.ToString
                    formatDate = formatDate.Replace("yyyy", Date.Today.Year.ToString)
                    formatDate = formatDate.Replace("mm", If(mm.ToString.Length = 1, "0" & mm, mm))
                    formatDate = formatDate.Replace("dd", If(dd.ToString.Length = 1, "0" & dd, dd))
                    docNo = formatDate
                Else
                    docNo = dateVal
                End If

                For i As Integer = 1 To cntSeq - 1
                    docNo &= "0"
                Next
                docNo &= "1"
            Else '>0
                valData += 1
                docNo = valData.ToString
            End If
        End If
        Return docNo
    End Function

End Class


