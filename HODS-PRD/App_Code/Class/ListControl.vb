Imports System.Linq
Imports MIS_HTI.DataControl

Namespace DataControl
    Public Class ListControl

        Private lst As List(Of String)
        Private strSplit As String
        'Private strConnect As String
        Sub New()
            lst = New List(Of String)
            strSplit = VarIni.C8
        End Sub

        Sub New(ByRef PI_LIST As List(Of String), Optional PI_strSplit As String = VarIni.C8)
            lst = PI_LIST '.Where(Function(val As String) val <> String.Empty)
            strSplit = PI_strSplit
        End Sub

        Property ListS() As List(Of String)
            Get
                Return lst
            End Get
            Set(val As List(Of String))
                lst = val
            End Set
        End Property


        Public Function FromChar(str As String, Optional strSplit As String = VarIni.C) As List(Of String)
            Return (str.Trim.Split(strSplit)).Cast(Of String)().ToList
        End Function

        Public Function FromArrayList(al As ArrayList) As List(Of String)
            Return al.Cast(Of String)().ToList
        End Function

        Public Sub Add(str As String, Optional checkExist As Boolean = True)
            If Not String.IsNullOrEmpty(str) OrElse (checkExist AndAlso Not String.IsNullOrEmpty(str) AndAlso Not Exist(str)) Then
                lst.Add(str)
            End If
        End Sub

        Public Sub Add(strList As List(Of String), Optional checkExist As Boolean = True)
            For Each str As String In strList
                Add(str, checkExist)
            Next
        End Sub

        Public Function Exist(str As String) As Boolean
            Return lst.Contains(str)
        End Function

        Sub TAL(fldName As String, colName As String, Optional showDecimal As String = "")
            If Not fldName.Contains(strSplit) Then
                fldName &= strSplit
            End If
            lst.Add(fldName & strSplit & colName & If(String.IsNullOrEmpty(showDecimal), String.Empty, strSplit & showDecimal))
        End Sub

        Function ChangeFormat(Optional Gridveiwcolumn As Boolean = False) As List(Of String)
            Dim new_lst As New List(Of String)
            For Each str As String In lst
                Dim new_str() As String = str.Split(strSplit)
                'format =>> fld_name(0) : fld_show(1) : col_name(2) : show_decimal(3)
                Dim fld_name As String = String.Empty
                Dim fld_show As String = String.Empty
                Dim col_name As String = String.Empty
                Dim show_decimal As String = String.Empty

                Dim line As Integer = 0
                For Each txt As String In new_str
                    Select Case line
                        Case 0
                            fld_name = txt
                        Case 1
                            fld_show = txt
                        Case 2
                            col_name = txt
                        Case 3
                            show_decimal = txt
                    End Select
                    line += 1
                Next
                Dim new_str2 As String = String.Empty
                If Gridveiwcolumn Then
                    'format =>> fld_name(0) : fld_show(1) : col_name(2) : show_decimal(3)
                    If Not String.IsNullOrEmpty(col_name) Then
                        new_str2 = col_name & strSplit & If(String.IsNullOrEmpty(fld_show), fld_name, fld_show) & If(String.IsNullOrEmpty(show_decimal), String.Empty, strSplit & show_decimal)
                    End If
                Else
                    'format =>> fld_name(0) : fld_show(1)
                    If Not String.IsNullOrEmpty(fld_name) Then
                        new_str2 = fld_name & If(String.IsNullOrEmpty(fld_show), String.Empty, strSplit & fld_show)
                    End If
                End If
                If Not String.IsNullOrEmpty(new_str2) Then
                    new_lst.Add(new_str2)
                End If

            Next
            Return new_lst
        End Function

        'add by noi on 2021/05/25
        Function ChangeFormat2(newStrSpit As String, Optional Gridveiwcolumn As Boolean = False) As List(Of String)

            Dim oldList As List(Of String) = ChangeFormat(Gridveiwcolumn)
            If oldList.Count <= 0 OrElse strSplit = newStrSpit Then
                Return oldList
            End If
            Dim newList As New List(Of String)
            For Each txt As String In oldList
                newList.Add(txt.Replace(strSplit, newStrSpit))
            Next
            Return newList
        End Function

        'add by noi 2020/02/20
        Function ColumnNumber() As List(Of String)
            Dim new_lst As New List(Of String)
            For Each str As String In lst
                Dim new_str() As String = str.Split(strSplit)
                'format =>> fld_name(0) : fld_show(1) : col_name(2) : show_decimal(3)
                Dim fld_name As String = new_str(0)
                If new_str.Length > 3 Then
                    If Not new_lst.Contains(fld_name) Then
                        new_lst.Add(fld_name)
                    End If
                End If
            Next
            Return new_lst
        End Function

        Function Text(Optional strconnect As String = "") As String
            Return String.Join(strconnect, lst)
        End Function

        Function Count() As Integer
            Return lst.Count
        End Function


    End Class
End Namespace


