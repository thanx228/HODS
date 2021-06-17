Namespace DataControl
    Public Class ArrayListControl
        Private al As ArrayList
        Private strSplit As String
        Private strConnect As String

        Sub New(ByRef alSet As ArrayList, Optional InstrSplit As String = VarIni.char8, Optional InstrConnect As String = VarIni.C)
            al = alSet
            strSplit = InstrSplit
            strConnect = InstrConnect
        End Sub

        Sub New()
            al = New ArrayList
            strSplit = VarIni.char8
            strConnect = VarIni.C
        End Sub

        Function ArrayListToText() As String
            Dim txtReturn As String = ""
            For Each str As String In al
                txtReturn &= str & strConnect
            Next
            Return txtReturn.Substring(0, txtReturn.Length - strConnect.Length)
        End Function

        Function ArrayListToText2() As String
            Dim strFinal As String
            Dim strarr() As String
            strarr = al.ToArray(Type.GetType("System.String"))
            strFinal = String.Join(strConnect, strarr)
            Return strFinal
        End Function

        Sub TAL(fldName As String, colName As String, Optional showDecimal As String = "", Optional needHide As Boolean = False)
            If Not fldName.Contains(strSplit) Then
                fldName &= strSplit
            End If
            al.Add(fldName & strSplit & colName & strSplit & showDecimal & If(needHide, strSplit, ""))
        End Sub

        Sub TALL(fldLeft As String, fldRight As String,
                 Optional fldRightSingleQuote As Boolean = False,
                 Optional strSpit As String = VarIni.C8) 'for left join 
            al.Add(fldLeft & strSpit & fldRight & If(fldRightSingleQuote, strSpit, String.Empty))
        End Sub

        Function ChangeFormat(Optional Gridveiwcolumn As Boolean = False) As ArrayList
            Dim new_al As New ArrayList
            For Each str As String In al
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
                    new_al.Add(new_str2)
                End If

            Next
            Return new_al
        End Function

        'add new by noi on 2021/02/10
        Function ChangeFormatList(Optional Gridveiwcolumn As Boolean = False) As List(Of String)
            Dim new_al As New List(Of String)
            For Each str As String In al
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
                    new_al.Add(new_str2)
                End If

            Next
            Return new_al
        End Function

        Function getColHide(al As ArrayList, Optional strSplit As String = VarIni.C8) As List(Of Integer)
            Dim colHide As New List(Of Integer)
            Dim line As Integer = 0
            For Each str As String In al
                Dim tt() As String = str.Split(strSplit)
                If tt.Count > 4 Then
                    colHide.Add(line)
                End If
                line += 1
            Next
            Return colHide
        End Function


        'old function no change because any module to call it
        Function arrayListToText(al As ArrayList, Optional strConnect As String = VarIni.C) As String
            Dim txtReturn As String = ""
            For Each str As String In al
                txtReturn &= str & strConnect
            Next
            Return txtReturn.Substring(0, txtReturn.Length - strConnect.Length)
        End Function

        Function arrayListToText2(al As ArrayList, Optional strConnect As String = VarIni.C) As String
            Dim strFinal As String
            Dim strarr() As String
            strarr = al.ToArray(Type.GetType("System.String"))
            strFinal = String.Join(strConnect, strarr)
            Return strFinal
        End Function

        'add by noi 2020/02/20
        Function ColumnNumber() As ArrayList
            Dim new_al As New ArrayList
            For Each str As String In al
                Dim new_str() As String = str.Split(strSplit)
                'format =>> fld_name(0) : fld_show(1) : col_name(2) : show_decimal(3) 
                Dim fld_name As String = new_str(0)
                If new_str.Length > 3 Then
                    If Not new_al.Contains(fld_name) Then
                        new_al.Add(fld_name)
                    End If
                End If
            Next
            Return new_al
        End Function

        Function ColumnNumberHash() As Hashtable
            Dim new_al As New Hashtable
            For Each str As String In al
                Dim new_str() As String = str.Split(strSplit)
                'format =>> fld_name(0) : fld_show(1) : col_name(2) : show_decimal(3) 
                Dim fld_name As String = new_str(0)
                If new_str.Length > 3 Then
                    If Not new_al.Contains(fld_name) Then
                        new_al.Add(fld_name, fld_name)
                    End If
                End If
            Next
            Return new_al
        End Function

    End Class
End Namespace


