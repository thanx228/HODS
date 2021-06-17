Imports Microsoft.VisualBasic

Partial Public Class DataRecordList
    Shared Function DataTableTranf(ByVal DataTableFiledName() As String, ByVal DataTableFiledType() As String) As Data.DataTable
        Dim DT3 As New Data.DataTable
        DT3.Clear()
        For L_count As Integer = 0 To DataTableFiledName.Length - 1
            Dim myColumn As New Data.DataColumn
            myColumn.DataType = System.Type.GetType("System." & DataTableFiledType(L_count))
            myColumn.ColumnName = DataTableFiledName(L_count)
            DT3.Columns.Add(myColumn)
        Next
        Return DT3
    End Function

    Shared Function GridViewColumns(ByRef GridViewName As GridView, ByVal ShowDataFiledName() As String, ByVal HiddenDataFiledName() As String _
                                    , ByVal GridColumnsWidth() As Integer, Optional ByVal HaveSerialNumber As Boolean = False _
                                    , Optional ByVal HaveCheckBox As Boolean = False _
                                    , Optional ByVal HaveDelete As Boolean = False _
                                    , Optional ByVal HaveEdit As Boolean = False ) As Boolean
        Try
            
            If HaveSerialNumber Then
                Dim TemplateForSerialNumber As New TemplateField
                Dim tempPlaceHolder As New PlaceHolder
                TemplateForSerialNumber.ItemTemplate = New SerialNumberForGridviewTemplate("ShowSer")
                TemplateForSerialNumber.ItemTemplate.InstantiateIn(tempPlaceHolder)
                TemplateForSerialNumber.ItemStyle.Width = "40"
                GridViewName.Columns.Add(TemplateForSerialNumber)
            End If
            For L_count As Integer = 0 To ShowDataFiledName.Length - 1
                Dim alex1 As New BoundField
                alex1.DataField = ShowDataFiledName(L_count)
                alex1.ItemStyle.Width = GridColumnsWidth(L_count)
                alex1.Visible = True
                GridViewName.Columns.Add(alex1)
            Next
            For L_count As Integer = 0 To HiddenDataFiledName.Length - 1
                Dim alex1 As New BoundField
                alex1.DataField = HiddenDataFiledName(L_count)
                alex1.Visible = False
                GridViewName.Columns.Add(alex1)
            Next
            
            If HaveEdit Then
                Dim TemplateForSerialNumber As New TemplateField
                Dim tempPlaceHolder As New PlaceHolder
                TemplateForSerialNumber.ItemTemplate = New SerialNumberForGridviewTemplate("ItemEdit", 3, "/images/edit.gif")
                TemplateForSerialNumber.ItemTemplate.InstantiateIn(tempPlaceHolder)
                TemplateForSerialNumber.ItemStyle.Width = "40"
                GridViewName.Columns.Add(TemplateForSerialNumber)
            End If
            If HaveDelete Then
                Dim TemplateForSerialNumber As New TemplateField
                Dim tempPlaceHolder As New PlaceHolder
                TemplateForSerialNumber.ItemTemplate = New SerialNumberForGridviewTemplate("ItemDelete", 2, "/images/delete.gif")
                TemplateForSerialNumber.ItemTemplate.InstantiateIn(tempPlaceHolder)
                TemplateForSerialNumber.ItemStyle.Width = "40"
                GridViewName.Columns.Add(TemplateForSerialNumber)
            End If
            If HaveCheckBox Then
                Dim TemplateForSerialNumber As New TemplateField
                Dim tempPlaceHolder As New PlaceHolder
                TemplateForSerialNumber.ItemTemplate = New SerialNumberForGridviewTemplate("ItemCheckBox", 1)
                TemplateForSerialNumber.ItemTemplate.InstantiateIn(tempPlaceHolder)
                TemplateForSerialNumber.ItemStyle.Width = "40"
                GridViewName.Columns.Add(TemplateForSerialNumber)
            End If
        Catch ab As Exception
            Return (ab.Message.ToString)
        End Try
    End Function

    Shared Function GridHeadTextSet(ByRef GridViewName As GridView, ByVal GridHeadName() As String) As Boolean
        For L_count As Integer = 0 To GridHeadName.Length - 1
            GridViewName.Columns(L_count).HeaderText() = GridHeadName(L_count)
        Next
    End Function
    'Shared Function GridHeadTextSet1(ByRef GridViewName As GridView, ByVal PageName As String, ByVal GridHeadName() As String, ByVal L_id As Integer, ByVal GridColumnsWidth() As Integer) As String
    '    GridHeadTextSet1 = ""
    '    GridHeadTextSet1 += "<table width='' border='3' cellspacing='0' cellpadding='0' align='center'  bgcolor=#999999>" _
    '                        & "<tr>"
    '    GridHeadTextSet1 += "<td style='text-align: center' nowrap width='40'>"
    '    GridHeadTextSet1 += MutilLangauge.GetAlexBetaShow(PageName, "", L_id)
    '    GridHeadTextSet1 += "</td>"
    '    For L_count As Integer = 0 To GridHeadName.Length - 1
    '        GridHeadTextSet1 += "<td style='text-align: center' nowrap width='" & GridColumnsWidth(L_count) & "'>"
    '        ' GridHeadTextSet1 += "<nobr style='width:" & GridViewName.Columns(L_count).ItemStyle.Width.Value & "px;overflow:hidden;'>"
    '        GridHeadTextSet1 += MutilLangauge.GetAlexBetaShow(PageName, GridHeadName(L_count), L_id)
    '        'GridHeadTextSet1 += "</nobr>" _
    '        GridHeadTextSet1 += "</td>"
    '    Next
    '    GridHeadTextSet1 += "</tr></table>"
    'End Function

    Shared Function SetGridToCssStyle(ByRef GridViewName As GridView, ByVal ItemFontName As String) As Boolean
        GridViewName.RowStyle.Font.Size = FontSize.XXLarge
        GridViewName.RowStyle.Font.Name = ItemFontName
    End Function



End Class
