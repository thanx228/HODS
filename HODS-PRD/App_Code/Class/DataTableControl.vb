Imports System.Data

Namespace DataControl
    Public Class DataTableControl
        Public Function setColDatatable(col() As String, Optional strSplit As String = ":") As DataTable
            Dim dt As New DataTable
            For Each str As String In col
                Dim temp() As String = str.Split(strSplit)
                dt.Columns.Add(temp(1), getShowType(temp.Length))
            Next
            Return dt
        End Function

        Public Function setColDatatable(col As ArrayList, Optional strSplit As String = ":") As DataTable
            Dim dt As New DataTable
            For Each str As String In col
                Dim temp() As String = str.Split(strSplit)
                dt.Columns.Add(temp(1), getShowType(temp.Length))
            Next
            Return dt
        End Function

        Function getShowType(cntTemp As Integer) As Type
            Dim showType As Type
            If cntTemp >= 3 Then
                showType = Type.GetType("System.Double")
            Else
                showType = Type.GetType("System.String")
            End If
            Return showType
        End Function

        Sub addDataRow(ByRef dt As DataTable, dataHash As Hashtable)
            Dim dr As DataRow
            dr = dt.NewRow()
            For Each fName As String In dataHash.Keys
                dr(fName) = dataHash.Item(fName)
            Next
            dt.Rows.Add(dr)
        End Sub

        Sub addDataRow(ByRef dt As DataTable, dr As DataRow, fldList As ArrayList, Optional strSplit As String = ":")
            'fldList Format FieldCOde:Decimal Checker(1 or 0/empty):Value Manual
            Dim dataHash As New Hashtable
            Dim OutputControl As New OutputControl

            For Each str As String In fldList
                If str IsNot String.Empty Then
                    Dim temp() As String = str.Split(strSplit)
                    Dim countTemp As Integer = temp.Count
                    Dim isDecimal As Boolean = False
                    Dim key As String = temp(0)
                    If countTemp = 3 Then
                        dataHash.Add(key, temp(2))
                    Else
                        Dim showDec As Boolean = False
                        If countTemp = 2 Then
                            showDec = True
                            'If OutputControl.checkNumberic(temp(1)) = 0 Then

                            'End If
                        End If
                        If showDec Then 'for decimal
                            dataHash.Add(key, IsDBNullDataRowDecimal(dr, key))
                        Else 'for string
                            dataHash.Add(key, IsDBNullDataRow(dr, key))
                        End If
                    End If
                End If
            Next
            If dataHash.Count > Decimal.Zero Then
                addDataRow(dt, dataHash)
            End If
        End Sub

        Sub addDataRow(ByRef dt As DataTable, fldList As ArrayList, Optional strSplit As String = VarIni.char8)
            'fldList Format FieldCOde:Decimal Checker(1 or 0/empty):Value Manual
            Dim dataHash As New Hashtable
            'Dim OutputControl As New OutputControl
            Dim hashCont As New HashtableControl

            For Each col As DataColumn In dt.Columns
                Dim val As String = ""
                If col.DataType = Type.GetType("System.Double") Then
                    val = 0
                End If
                hashCont.addDataHash(dataHash, col.ColumnName, val)
            Next
            For Each str As String In fldList
                If str IsNot String.Empty Then
                    Dim temp() As String = str.Split(strSplit)
                    Dim key As String = temp(0)
                    Dim val As String = If(temp.Count = 2, temp(1), "")
                    If hashCont.existDataHash(dataHash, key) And (val <> "" Or val <> "0") Then
                        hashCont.UpdateDataHash(dataHash, key, val)
                    End If
                End If
            Next
            If dataHash.Count > Decimal.Zero Then
                addDataRow(dt, dataHash)
            End If
        End Sub

        Public Function IsDBNullDataRow(ByVal dr As DataRow, ByVal fldName As String, Optional strWhenEmpty As String = "") As String
            If dr Is Nothing Then
                Return strWhenEmpty
            End If
            Try
                Return If(IsDBNull(dr(fldName)), strWhenEmpty, Trim(dr(fldName)))
            Catch ex As Exception
                Return strWhenEmpty
            End Try
        End Function

        Public Function IsDBNullDataRowDecimal(ByVal dr As DataRow, ByVal fldName As String, Optional valDefault As Decimal = Decimal.Zero) As Decimal
            If dr Is Nothing Then
                Return valDefault
            End If

            Try
                Dim OutputControl As New OutputControl
                Dim getVal As String = If(IsDBNull(dr(fldName)), String.Empty, Trim(dr(fldName)))
                Return OutputControl.checkNumberic(getVal)
            Catch ex As Exception
                Return valDefault
            End Try
        End Function

        Public Function FormatDateDataRow(ByVal dr As DataRow, ByVal fldName As String, Optional formatDate As String = "0:dd/MM/yyyy") As String
            Return If(IsDBNull(dr(fldName)), String.Empty, [String].Format("{" & formatDate & "}", dr(fldName)))
        End Function

        Public Function FormatNumberDataRow(ByVal dr As DataRow, ByVal fldName As String, Optional formatNumber As String = "##,##0.00") As String
            Return If(IsDBNull(dr(fldName)), String.Empty, [String].Format("{" & formatNumber & "}", dr(fldName)))
        End Function

        Public Function DataTableTranf(ShowFiled As String) As DataTable
            Dim DT3 As New DataTable
            Dim DataTableFiledName() As String = Split(ShowFiled, ",")
            For L_count As Integer = 0 To DataTableFiledName.Length - 1
                Dim myColumn As New Data.DataColumn
                myColumn.DataType = System.Type.GetType("System.String")
                myColumn.ColumnName = DataTableFiledName(L_count)
                DT3.Columns.Add(myColumn)
            Next
            Return DT3
        End Function

        'add by top
        Public Function DataTableTranf(ByVal DataTableFiledName() As String) As DataTable
            Dim DT3 As New DataTable
            For L_count As Integer = 0 To DataTableFiledName.Length - 1
                Dim myColumn As New Data.DataColumn
                myColumn.DataType = System.Type.GetType("System.String")
                myColumn.ColumnName = DataTableFiledName(L_count)
                DT3.Columns.Add(myColumn)
            Next
            Return DT3
        End Function

        'TextArrayList
        Sub TAL(ByRef al As ArrayList, fldName As String, colName As String,
                Optional showDecimal As String = "", Optional strSplit As String = VarIni.char8)
            If Not fldName.Contains(strSplit) Then
                fldName &= strSplit
            End If
            al.Add(fldName & strSplit & colName & If(showDecimal = String.Empty, String.Empty, strSplit & showDecimal))
        End Sub

        Function newAL(al As ArrayList, Optional gv_column As Boolean = False,
                       Optional strSplit As String = VarIni.char8) As ArrayList
            Dim new_al As New ArrayList

            For Each str As String In al
                Dim new_str() As String = str.Split(strSplit)
                'format =>> fld_name(0) : fld_show(1) : col_name(2) : show_decimal(3) 
                Dim new_str2 As String = ""
                Dim fld_name As String = new_str(0)
                Dim fld_show As String = new_str(1)
                Dim col_name As String = new_str(2)
                Dim show_decimal As String = If(new_str.Count > 3, new_str(3), String.Empty)

                If gv_column Then
                    'format =>> fld_name(0) : fld_show(1) : col_name(2) : show_decimal(3) 
                    If col_name <> "" Then
                        new_str2 = col_name & strSplit & If(fld_show = String.Empty, fld_name, fld_show) & If(show_decimal = String.Empty, String.Empty, strSplit & show_decimal)
                    End If
                Else
                    'format =>> fld_name(0) : fld_show(1) 
                    If fld_name <> "" Then
                        new_str2 = fld_name & If(fld_show = String.Empty, String.Empty, strSplit & fld_show)
                    End If
                End If
                If new_str2 <> "" Then
                    new_al.Add(new_str2)
                End If

            Next
            Return new_al
        End Function

    End Class

End Namespace