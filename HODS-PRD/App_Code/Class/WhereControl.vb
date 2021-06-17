Namespace DataControl
    Public Class WhereControl
        Function Where(ByVal fld As String, ByRef checkboxList As CheckBoxList,
                       Optional ByVal notIn As Boolean = False,
                       Optional ByVal valDefault As String = "",
                       Optional ByVal selectAll As Boolean = False) As String
            Dim type As String = "",
                cnt As Decimal = 0,
                whr As String = "",
                typeAll As String = ""
            For Each boxItem As ListItem In checkboxList.Items
                Dim boxVal As String = CStr(boxItem.Value.Trim)
                typeAll &= boxVal & ","
                If boxItem.Selected Then
                    type &= boxVal & ","
                    cnt = cnt + 1
                End If
            Next
            If cnt > 0 Then
                type = type.Substring(0, type.Count - 1).Replace(",", "','")
                Dim strIn As String = " in "
                If notIn Then
                    strIn = " not in "
                End If
                whr = " and " & fld & " " & strIn & " ('" & type & "')"
            Else
                If valDefault <> "" Then
                    whr = " and " & fld & " in (" & valDefault & ")"
                End If
                If selectAll Then
                    typeAll = typeAll.Substring(0, typeAll.Count - 1).Replace(",", "','")
                    Dim strIn As String = " in "
                    If notIn Then
                        strIn = " not in "
                    End If
                    whr = " and " & fld & strIn & " ('" & typeAll & "')"
                End If
            End If
            Return whr
        End Function

        Function Where(ByVal fld As String, ByRef dropdownList As DropDownList,
                       Optional ByVal notValue As String = "0",
                       Optional ByVal conIn As Boolean = False,
                       Optional selAllRang As Boolean = False) As String
            Dim dataGet As String = dropdownList.Text,
                whr As String = ""
            If dataGet <> notValue Then
                If conIn Then
                    whr = " and " & fld & " in ('" & dataGet.Replace(",", "','") & "') "
                Else
                    whr = " and " & fld & " = '" & dataGet & "' "
                End If
            Else
                If selAllRang And dropdownList.Items.Count > 0 Then
                    Dim val As String = String.Empty
                    For Each li As ListItem In dropdownList.Items
                        Dim gVal As String = li.Value
                        If gVal <> notValue Then
                            val &= li.Value & ","
                        End If

                    Next
                    whr = " and " & fld & " in ('" & val.Substring(0, val.Length - 1).Replace(",", "','") & "') "
                End If
            End If
            Return whr
        End Function

        Function Where(fld As String, ByRef textbox As TextBox, Optional likeCondition As Boolean = True) As String
            If fld = "" Then
                Return fld
            End If
            Dim val As String = textbox.Text.Trim,
                whr As String = ""
            If val <> "" Then
                If likeCondition Then
                    'whr = " and " & fld & " like '%" & val & "%' "
                    whr = WHERE_LIKE(fld, val, True)

                Else
                    'whr = " and " & fld & " = '" & val & "' "
                    whr = WHERE_EQUAL(fld, val)
                End If
            End If
            Return whr
        End Function

        Function Where(ByVal fldDate As String, ByVal dateFrom As String, ByVal dateTo As String,
                       Optional to_date As Boolean = False, Optional ByVal causeVal As Boolean = False,
                       Optional ByVal showAnd As Boolean = True) As String
            If fldDate = "" Or (dateFrom = "" And dateTo = "") Then
                Return ""
            End If
            Dim whr As String = " "
            If showAnd Then
                whr &= "and "
            End If
            If to_date Then
                'Dim cfd As New ConfigDate
                'If dateFrom <> "" Then
                '    dateFrom = "to_date('" & dateFrom & "','" & cfd.FormatShowOracle2 & "') "
                'End If
                'If dateTo <> "" Then
                '    dateTo = "to_date('" & dateTo & "','" & cfd.FormatShowOracle2 & "') "
                'End If
            End If
            If dateFrom <> "" And dateTo <> "" Then
                whr &= fldDate & " between " & dateFrom & " and " & dateTo & " "
            Else
                Dim symbol As String = ">="
                If causeVal Then
                    symbol = "="
                End If
                Dim dateSel As String = symbol & dateFrom & " "
                If dateFrom = "" Then
                    symbol = "<="
                    If causeVal Then
                        symbol = "="
                    End If
                    dateSel = symbol & dateTo & " "
                End If
                whr &= fldDate & dateSel
            End If
            Return whr
        End Function

        Function Where(fld As String, val As String, Optional showAnd As Boolean = True,
                       Optional manual As Boolean = True,
                       Optional equal As Boolean = True) As String
            Dim andShow As String = ""
            If showAnd Then
                andShow = " and "
            End If
            If Not manual Then
                If equal Then
                    val = " ='" & val & "' "
                End If
            End If
            Return andShow & fld & val
        End Function

        Function Where(fld As String, ByRef checkbox As CheckBox, Optional ByVal chkTrue As String = "Y") As String
            Dim val As String = ""
            If checkbox.Checked Then
                val = WHERE_EQUAL(fld, chkTrue)
            End If
            Return val
        End Function

        'causeVal = 0 = equal, 1= more or less and 2 = more and equal or less and equal
        'Function WhereDate(ByVal fldDate As String, ByVal dateFrom As String, ByVal dateTo As String,
        '                   Optional truncFormat As String = "",
        '                   Optional causeVal As String = "2",
        '                   Optional showAnd As Boolean = True,
        '                   Optional formatDate As String = "",
        '                   Optional fullDays As Boolean = True) As String
        '    If fldDate = String.Empty Or (dateFrom = String.Empty And dateTo = String.Empty) Then
        '        Return String.Empty
        '    End If
        '    'Dim cfd As New DateControl
        '    Dim sqlCont As New SQLControl
        '    Dim whr As String = String.Empty
        '    If showAnd Then
        '        whr &= VarIni.A
        '    End If
        '    If truncFormat <> String.Empty Then
        '        fldDate = sqlCont.TRUNC(fldDate, truncFormat)
        '    End If
        '    If dateFrom <> String.Empty Then
        '        If truncFormat <> String.Empty Then
        '            dateFrom = sqlCont.TRUNC(dateFrom, truncFormat, True, True)
        '        Else
        '            dateFrom = sqlCont.TO_DATE(dateFrom, formatDate, True)
        '        End If
        '    End If
        '    If dateTo <> String.Empty Then
        '        If truncFormat <> String.Empty Then
        '            dateTo = sqlCont.TRUNC(dateTo, truncFormat, True, True)
        '        Else
        '            dateTo = sqlCont.TO_DATE(dateTo, formatDate, True)
        '        End If
        '    End If
        '    If dateFrom <> String.Empty And dateTo <> String.Empty Then
        '        ' whr &= fldDate & " between " & dateFrom & VarIni.A & dateTo & If(fullDays, "+0.99999 ", "")
        '        whr &= WHERE_BETWEEN(fldDate, dateFrom, dateTo & If(fullDays, VarIni.addAllDay, ""), False, False, False)
        '    Else
        '        whr &= fldDate
        '        Select Case causeVal
        '            Case "0" '=
        '                whr &= VarIni.E
        '            Case "1" '<,>
        '                whr &= If(dateFrom = String.Empty, " > ", " < ")
        '            Case "2" '<=,>=
        '                whr &= If(dateFrom = String.Empty, " >= ", " <= ")
        '        End Select
        '        whr &= If(dateFrom = String.Empty, dateTo, dateFrom)
        '    End If
        '    Return whr
        'End Function

        'Function whereMonth(fldDate As String, yearMonth As String, Optional showAnd As Boolean = True) As String
        '        If fldDate = "" Or yearMonth = "" Then
        '            Return ""
        '        End If
        '        Dim sqlCont As New SQLControl
        '        Dim dateFm As String = sqlCont.TO_DATE(yearMonth & "01", VarIni.YYYYMMDD)
        '        Dim dateTo As String = sqlCont.LAST_DAY(dateFm)
        '        Return If(showAnd, VarIni.A, VarIni.SP) & fldDate & VarIni.B & dateFm & VarIni.A & dateTo & VarIni.SP
        '        Return WHERE_BETWEEN(fldDate, dateFm, dateTo, False, False)
        '    End Function

        '##### getWhrFirst
        '## Create by : Noi
        '## Create Date :
        '## PI 1 : tc = table code
        '## PI 2 : showSite
        '## PI 3 : A1 =alias name
        '##Description : show where when select data from T100
        '## Modify by:Noi
        '## Modify date : 2017-09-09
        '## Modify Detail : add paramitor name : showWhere for show "where"
        '## PI 4 : showWhere
        '##### getWhrFirst
        'Function getWhrFirst(tc As String,
        '                         Optional showSite As Boolean = True,
        '                         Optional A1 As String = "",
        '                         Optional showWhere As Boolean = True) As String 'tc=table code

        '        Dim sqlCont As New SQLControl
        '        Dim A1_2 As String = sqlCont.addAliasName(A1, True)
        '        Dim strWhr As String = sqlCont.addSpace(If(showWhere, VarIni.W, VarIni.SP) & A1_2 & tc & VarIni.EntF & VarIni.E & VarIni.EntV)
        '        If showSite Then
        '            strWhr &= sqlCont.addSpace(VarIni.A & A1_2 & tc & VarIni.SiteF & VarIni.E & VarIni.SQ & VarIni.SiteV & VarIni.SQ, False)
        '        End If
        '        Return strWhr
        '    End Function

        Function ItemFindByType(fldName As String, grpType As String,
                                Optional codeType As String = "0",
                                Optional caseSensitive As String = "i") As String
            'grpType = 1 =BOM Item,2 = none BOM and none asset and 3 asset code
            'codeType 0=all code bom type 1=material,2=FG ,3=Semi part and 4 = spare part and other
            Dim strReturn As String = String.Empty
            If Regex.IsMatch(grpType, "[123]") Then
                Dim pattern As String = ""
                If grpType = "1" Then 'BOM Item
                    codeType = If(codeType = "0", "1-4", codeType)
                    pattern = "^([AGM][" & codeType & "]|8.[" & codeType & "])"
                ElseIf grpType = "3" Then 'asset group type
                    pattern = "^(GAD)"
                Else '2=none BOM and None Asset
                    pattern = "^([^AGM8(GAD)])"
                End If
                strReturn &= WHERE_REGEXP_LIKE(fldName, pattern, caseSensitive)
            End If
            Return strReturn
        End Function

        Function WHERE_EQUAL(fldCheck As String, val As String,
                             Optional showSymbol As String = VarIni.E,
                             Optional addSigleQuote As Boolean = True,
                             Optional showAnd As Boolean = True,
                             Optional fullFormat As Boolean = False) As String
            Dim sqlCont As New SQLControl
            Dim txt As String = String.Empty
            If fullFormat OrElse Not String.IsNullOrEmpty(val) Then
                txt = sqlCont.addSpace(If(showAnd, VarIni.A, VarIni.NSP) & fldCheck & showSymbol & sqlCont.addSingleQuot(val, addSigleQuote))
            End If
            Return txt
        End Function

        Function WHERE_EQUAL(fldCheck As String, ddl As DropDownList,
                             Optional showSymbol As String = VarIni.E,
                             Optional addSigleQuote As Boolean = True,
                             Optional valNotFind As String = "0",
                             Optional showAnd As Boolean = True,
                             Optional fullFormat As Boolean = False) As String
            Return WHERE_EQUAL(fldCheck, If(ddl.Text.Trim = valNotFind, String.Empty, ddl.Text.Trim), showSymbol, addSigleQuote, showAnd, fullFormat)
        End Function

        Function WHERE_EQUAL(fldCheck As String, tb As TextBox,
                             Optional showSymbol As String = VarIni.E,
                             Optional addSigleQuote As Boolean = True,
                             Optional showAnd As Boolean = True,
                             Optional fullFormat As Boolean = False) As String
            Return WHERE_EQUAL(fldCheck, tb.Text.Trim, showSymbol, addSigleQuote, showAnd, fullFormat)
        End Function

        Function WHERE_EQUAL(fldCheck As String, cb As CheckBox, Optional valWhenTrue As String = "Y",
                             Optional showSymbol As String = VarIni.E,
                             Optional addSigleQuote As Boolean = True,
                             Optional showAnd As Boolean = True,
                             Optional fullFormat As Boolean = False) As String
            Return If(cb.Checked, WHERE_EQUAL(fldCheck, valWhenTrue, showSymbol, addSigleQuote, showAnd, fullFormat), String.Empty)
        End Function

        Function WHERE_IN(fldCheck As String, val As String,
                          Optional notIn As Boolean = False,
                          Optional addSigleQuote As Boolean = False,
                          Optional showAnd As Boolean = True) As String
            Dim sqlCont As New SQLControl
            Return If(val = String.Empty, String.Empty, sqlCont.addSpace(If(showAnd, VarIni.A, VarIni.NSP) & fldCheck & If(notIn, VarIni.N, VarIni.SP) & VarIni.I2 & sqlCont.addBracket(sqlCont.addSingleQuot(val.Replace(VarIni.C, sqlCont.addSingleQuot(VarIni.C, addSigleQuote)), addSigleQuote))))
        End Function

        Function WHERE_IN(fldCheck As String, tb As TextBox,
                          Optional notIn As Boolean = False,
                          Optional addSigleQuote As Boolean = False,
                          Optional showAnd As Boolean = True) As String
            Return WHERE_IN(fldCheck, tb.Text.Trim, notIn, addSigleQuote, showAnd)
        End Function

        'Function WHERE_IN(ByVal fld As String, ByRef cbl As CheckBoxList,
        '                  Optional ByVal notIn As Boolean = False,
        '                  Optional ByVal allWhenNotSelect As Boolean = False,
        '                  Optional ByVal valDefault As String = "") As String
        '    Dim type As String = "",
        '        cnt As Decimal = 0,
        '        whr As String = "",
        '        typeAll As String = ""
        '    For Each boxItem As ListItem In cbl.Items
        '        Dim boxVal As String = CStr(boxItem.Value.Trim)
        '        typeAll &= boxVal & ","
        '        If boxItem.Selected Then
        '            type &= boxVal & VarIni.C
        '            cnt += 1
        '        End If
        '    Next
        '    If cnt > 0 Then
        '        whr = WHERE_IN(fld, type.Substring(0, type.Count - 1), notIn, True)
        '    Else
        '        If allWhenNotSelect Then
        '            whr = WHERE_IN(fld, typeAll.Substring(0, typeAll.Count - 1), notIn, True)
        '        Else
        '            If valDefault <> "" Then
        '                whr = WHERE_IN(fld, valDefault, notIn, True)
        '            End If
        '        End If
        '    End If
        '    Return whr
        'End Function

        'update by noi on 2021/04/07
        Function WHERE_IN(ByVal fld As String, ByRef cbl As CheckBoxList,
                          Optional ByVal notIn As Boolean = False,
                          Optional ByVal allWhenNotSelect As Boolean = False,
                          Optional ByVal valDefault As String = "",
                          Optional showAnd As Boolean = True) As String
            Dim type As String = "",
                cnt As Decimal = 0,
                whr As String = "",
                typeAll As String = ""
            For Each boxItem As ListItem In cbl.Items
                Dim boxVal As String = CStr(boxItem.Value.Trim)
                typeAll &= boxVal & ","
                If boxItem.Selected Then
                    type &= boxVal & VarIni.C
                    cnt += 1
                End If
            Next
            If cnt > 0 Then
                whr = WHERE_IN(fld, type.Substring(0, type.Count - 1), notIn, True, showAnd) 'add showAnd on 2021/04/05
            Else
                If allWhenNotSelect Then
                    whr = WHERE_IN(fld, typeAll.Substring(0, typeAll.Count - 1), notIn, True, showAnd) 'add showAnd on 2021/04/05
                Else
                    If Not String.IsNullOrEmpty(valDefault) Then
                        whr = WHERE_IN(fld, valDefault, notIn, True, showAnd)
                    End If
                End If
            End If
            Return whr
        End Function

        'Function WHERE_IN(ByVal fld As String, ByRef dropdownList As DropDownList,
        '                  Optional ByVal notValue As String = "0",
        '                  Optional selAllRang As Boolean = False) As String
        '    Dim sqlCont As New SQLControl
        '    Dim dataGet As String = dropdownList.Text,
        '        whr As String = "",
        '        val As String = ""
        '    If dataGet <> notValue Then
        '        val = dataGet
        '    Else
        '        If selAllRang And dropdownList.Items.Count > 0 Then
        '            For Each li As ListItem In dropdownList.Items
        '                Dim gVal As String = li.Value
        '                If gVal <> notValue Then
        '                    val &= gVal & VarIni.C
        '                End If
        '            Next
        '            val = val.Substring(0, val.Length - 1)
        '        End If
        '    End If
        '    If val <> "" Then
        '        whr = WHERE_IN(fld, val,, True)
        '    End If
        '    Return whr
        'End Function

        'update by noi on 2021/04/07 ==> add optional showAnd
        Function WHERE_IN(fld As String, ByRef dropdownList As DropDownList,
                          Optional notValue As String = "0",
                          Optional selAllRang As Boolean = False,
                          Optional showAnd As Boolean = True) As String
            Dim sqlCont As New SQLControl
            Dim dataGet As String = dropdownList.Text,
                whr As String = "",
                val As String = ""
            If dataGet <> notValue Then
                val = dataGet
            Else
                If selAllRang And dropdownList.Items.Count > 1 Then
                    For Each li As ListItem In dropdownList.Items
                        Dim gVal As String = li.Value
                        If gVal <> notValue Then
                            val &= gVal & VarIni.C
                        End If
                    Next
                    val = val.Substring(0, val.Length - 1)
                End If
            End If
            If Not String.IsNullOrEmpty(val) Then
                whr = WHERE_IN(fld, val,, True, showAnd)
            End If
            Return whr
        End Function

        Function WHERE_LIKE(fldCheck As String, val As String,
                            Optional prefix As Boolean = True,
                            Optional postfix As Boolean = True,
                            Optional notLike As Boolean = False,
                            Optional showAnd As Boolean = True) As String
            Dim sqlCont As New SQLControl
            Return If(val = String.Empty, String.Empty, sqlCont.addSpace(If(showAnd, VarIni.A, VarIni.NSP) & fldCheck & If(notLike, VarIni.N, VarIni.SP) & VarIni.L2 & sqlCont.addSingleQuot(If(prefix, VarIni.P, VarIni.NSP) & val & If(postfix, VarIni.P, VarIni.NSP))))
        End Function

        'Function WHERE_LIKE(fldCheck As String, ddl As DropDownList,
        '                    Optional prefix As Boolean = True,
        '                    Optional postfix As Boolean = True,
        '                    Optional notLike As Boolean = False,
        '                    Optional showAnd As Boolean = True) As String
        '    Return WHERE_LIKE(fldCheck, ddl.Text.Trim, prefix, postfix, notLike, showAnd)
        'End Function
        Function WHERE_LIKE(fldCheck As String, ddl As DropDownList,
                            Optional prefix As Boolean = True,
                            Optional postfix As Boolean = True,
                            Optional notLike As Boolean = False,
                            Optional valAll As String = "0",
                            Optional showAnd As Boolean = True) As String
            Dim val As String = ddl.Text.Trim
            Return WHERE_LIKE(fldCheck, If(val = valAll, "", val), prefix, postfix, notLike, showAnd)
        End Function

        Function WHERE_LIKE(fldCheck As String, tb As TextBox,
                            Optional prefix As Boolean = True,
                            Optional postfix As Boolean = True,
                            Optional notLike As Boolean = False,
                            Optional showAnd As Boolean = True) As String
            Return WHERE_LIKE(fldCheck, tb.Text.Trim, prefix, postfix, notLike, showAnd)
        End Function

        Function WHERE_REGEXP_LIKE(fldCheck As String, pattern As String,
                                   Optional caseSensitive As String = "i",
                                   Optional showAnd As Boolean = True) As String
            Dim sqlCont As New SQLControl
            Return sqlCont.addSpace(If(showAnd, VarIni.A, VarIni.NSP) & VarIni.RL & sqlCont.addBracket(fldCheck & VarIni.C & sqlCont.addSingleQuot(pattern) & VarIni.C & sqlCont.addSingleQuot(caseSensitive)))
        End Function

        Function WHERE_BETWEEN(fldCheck As String, val1 As String, val2 As String,
                             Optional addSigleQuote1 As Boolean = True,
                             Optional addSigleQuote2 As Boolean = True,
                             Optional showAnd As Boolean = True) As String
            Dim sqlCont As New SQLControl
            Return If(val1 = String.Empty And val2 = String.Empty, String.Empty, sqlCont.addSpace(If(showAnd, VarIni.A, VarIni.NSP) & fldCheck & VarIni.B & sqlCont.addSingleQuot(val1, addSigleQuote1) & VarIni.A & sqlCont.addSingleQuot(val2, addSigleQuote2)))
        End Function

        Function WHERE_DATE(fldDate As String, dateFrom As String, dateTo As String,
                            Optional causeVal As String = "2",
                            Optional showAnd As Boolean = True,
                            Optional AllEmptyShowToday As Boolean = False) As String
            If Not AllEmptyShowToday And (fldDate = String.Empty Or (dateFrom = String.Empty And dateTo = String.Empty)) Then
                Return String.Empty
            End If

            Dim sqlCont As New SQLControl
            Dim whr As String = String.Empty
            If showAnd Then
                whr &= VarIni.A
            End If
            If Not String.IsNullOrEmpty(dateFrom) And Not String.IsNullOrEmpty(dateTo) Then
                ' whr &= fldDate & " between " & dateFrom & VarIni.A & dateTo & If(fullDays, "+0.99999 ", "")
                whr &= WHERE_BETWEEN(fldDate, dateFrom, dateTo, True, True, False)
            ElseIf Not String.IsNullOrEmpty(dateFrom) Or Not String.IsNullOrEmpty(dateTo) Then
                whr &= fldDate
                Select Case causeVal
                    Case "0" '=
                        whr &= VarIni.E
                    Case "1" '<,>
                        whr &= If(dateFrom = String.Empty, " > ", " < ")
                    Case "2" '<=,>=
                        whr &= If(dateFrom = String.Empty, " >= ", " <= ")
                End Select
                whr &= If(dateFrom = String.Empty, dateTo, dateFrom)
            Else
                If AllEmptyShowToday Then
                    whr &= WHERE_EQUAL(fldDate, Date.Now.ToString("yyyyMMdd"),, True, False)
                End If
            End If
            Return whr
        End Function

        Function Where(fld1 As String, fld2 As String, ByRef textbox As TextBox, Optional likeCondition As Boolean = True) As String
            Dim val As String = textbox.Text.Trim,
                whr As String = ""
            If val <> "" Then
                If likeCondition Then
                    whr = " and (" & fld1 & " like '%" & val & "%'  or " & fld2 & " like '%" & val & "%' )"
                Else
                    whr = " and (" & fld1 & " = '" & val & "'  or " & fld2 & " = '" & val & "' )"
                End If
            End If
            Return whr
        End Function

        Function WHERE_OR(fld1 As String, fld2 As String, tb As TextBox, Optional isLike As Boolean = True) As String
            Dim val As String = tb.Text.Trim,
                whr As String = ""
            If val <> "" Then
                Dim whrTemp1 As String
                Dim whrTemp2 As String
                If isLike Then
                    whrTemp1 = WHERE_LIKE(fld1, tb, showAnd:=False)
                    whrTemp2 = WHERE_LIKE(fld2, tb, showAnd:=False)
                Else
                    whrTemp1 = WHERE_EQUAL(fld1, tb, showAnd:=False)
                    whrTemp2 = WHERE_EQUAL(fld2, tb, showAnd:=False)
                End If
                whr = VarIni.A & "(" & whrTemp1 & " OR " & whrTemp2 & ")"
            End If
            Return whr
        End Function

    End Class
End Namespace