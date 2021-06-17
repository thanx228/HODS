Namespace DataControl
    Public Class SQLControl
        Inherits WhereControl
        Dim outputControl As New OutputControl

        Function GetSQL(ByVal table As String, ByVal fldInsert As Hashtable, ByVal fldUpdate As Hashtable, ByVal whr As Hashtable) As String
            Dim ISQL As String = getInsertSql(table, fldInsert, whr)
            Dim USQL As String = getUpdateSql(table, fldUpdate, whr)
            Dim WSQL As String = ""
            For Each whrName As String In whr.Keys
                WSQL = WSQL & whrName & "='" & outputControl.replaceSingleQuote(whr.Item(whrName)) & "' and "
            Next
            Return " if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL & " else " & ISQL & ";"
        End Function

        Function GetSQL(ByVal table As String, ByVal fld As Hashtable, ByVal whr As Hashtable,
                               Optional ByVal typeSql As String = "UI",
                               Optional showSemi As Boolean = True) As String
            Dim SQL As String = ""
            Select Case typeSql.ToUpper
                Case "I" 'insert
                    SQL = getInsertSql(table, fld, whr)
                Case "U" 'update
                    SQL = getUpdateSql(table, fld, whr, True)
                Case "D" 'delete
                    SQL = getDeleteSql(table, whr)
                Case "UI" ' if exist then update else insert
                    Dim USQL As String = getUpdateSql(table, fld, whr, True)
                    Dim ISQL As String = getInsertSql(table, fld, whr)
                    Dim WSQL As String = ""
                    For Each whrName As String In whr.Keys
                        WSQL = WSQL & whrName & "='" & outputControl.replaceSingleQuote(whr.Item(whrName)) & "' and "
                    Next
                    SQL = " if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL & " else " & ISQL
                Case "II" 'if not exist then insert
                    Dim ISQL As String = getInsertSql(table, fld, whr)
                    Dim WSQL As String = ""
                    For Each whrName As String In whr.Keys
                        WSQL = WSQL & whrName & "='" & outputControl.replaceSingleQuote(whr.Item(whrName)) & "' and "
                    Next
                    SQL = " if not exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & ISQL
                Case "UU" 'if exit then update
                    Dim USQL As String = getUpdateSql(table, fld, whr, True)
                    Dim WSQL As String = ""
                    For Each whrName As String In whr.Keys
                        WSQL = WSQL & whrName & "='" & outputControl.replaceSingleQuote(whr.Item(whrName)) & "' and "
                    Next
                    SQL = " if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL
            End Select
            Return SQL & If(showSemi, ";", "")
        End Function
        'updat by noi on 2021/04/19 
        'update detail re-engineer function
        Function getInsertSql(table As String, fld As Hashtable, whr As Hashtable, Optional strSpit As String = ":") As String
            If fld.Count = 0 AndAlso whr.Count = 0 Then
                Return ""
            End If
            Dim fldInsert As New List(Of String)
            Dim valInsert As New List(Of String)
            getFldInsert(fldInsert, valInsert, whr, strSpit)
            getFldInsert(fldInsert, valInsert, fld, strSpit)
            Dim fldIns As New ListControl(fldInsert)
            Dim valIns As New ListControl(valInsert)
            Return "insert into " & table & "(" & fldIns.Text(VarIni.C) & ") values(" & valIns.Text(VarIni.C) & ")"
        End Function

        'add by noi on 2021/04/19 
        'detial re-engineer function
        Sub getFldInsert(ByRef fldInsert As List(Of String),
                         ByRef valInsert As List(Of String),
                         fld As Hashtable,
                         Optional strSpit As String = VarIni.C8)

            For Each fldName As String In fld.Keys
                Dim textSplit() As String = fldName.Split(strSpit)
                Dim wordPrefix As String = "",
                    realFldName As String = ""

                Dim line As Integer = 0
                For Each txt As String In textSplit
                    Select Case line
                        Case 0
                            realFldName = txt
                        Case 1
                            wordPrefix = txt
                    End Select
                    line += 1
                Next
                fldInsert.Add(realFldName)
                valInsert.Add(wordPrefix & addSingleQuot(outputControl.replaceSingleQuote(fld.Item(fldName))))
            Next
        End Sub

        'update by noi on 2021/04/09
        'update by noi on 2021/04/19 cannot update
        Function getUpdateSql(table As String, fld As Hashtable, whr As Hashtable,
                              Optional setQuot As Boolean = False) As String

            If fld.Count = 0 OrElse whr.Count = 0 Then
                Return ""
            End If
            ' field and value of table
            Dim fldList As New List(Of String)
            For Each fldName As String In fld.Keys
                Dim textSplit() As String = fldName.Split(":")
                Dim wordPrefix As String = "",
                    realFldName As String = ""
                Dim addSingleQuote As Boolean = True
                Dim line As Integer = 0
                For Each txt As String In textSplit
                    Select Case line
                        Case 0
                            realFldName = txt
                        Case 1
                            wordPrefix = txt
                        Case 2
                            addSingleQuote = False
                    End Select
                    line += 1
                Next
                fldList.Add(WHERE_EQUAL(realFldName, wordPrefix & addSingleQuot(outputControl.replaceSingleQuote(fld.Item(fldName)), addSingleQuote), addSigleQuote:=False, showAnd:=False))
            Next
            'where of table 
            Dim whrList As New List(Of String)
            For Each whrName As String In whr.Keys
                whrList.Add(WHERE_EQUAL(whrName, addSingleQuot(outputControl.replaceSingleQuote(whr.Item(whrName))), addSigleQuote:=False, showAnd:=False))
            Next
            Dim fldUpdate As New ListControl(fldList)
            Dim whrUpdate As New ListControl(whrList)
            Return " update " & table & " set " & fldUpdate.Text(VarIni.C) & " where " & whrUpdate.Text(VarIni.A) '& If(sqlserver, "", " COMMIT WORK ")
        End Function

        Function getDeleteSql(ByVal table As String, ByVal whr As Hashtable) As String
            Dim ASQL As String = ""     ' where of table
            For Each whrName As String In whr.Keys
                ASQL = ASQL & whrName & "='" & outputControl.replaceSingleQuote(whr.Item(whrName)) & "' and "
            Next
            Return " delete from " & table & " where " & ASQL.Substring(0, ASQL.Length - 4)
        End Function

        'Function GetSQLOracle(ByVal table As String, ByVal fld As Hashtable, ByVal whr As Hashtable,
        '                       Optional ByVal typeSql As String = "I") As String
        '    Dim SQL As String = ""
        '    Select Case typeSql.ToUpper
        '        Case "I" 'insert
        '            SQL = getSqlInsertOracle(table, fld)
        '        Case "U" 'update
        '            SQL = getSqlUpdateOracle(table, fld, whr)
        '        Case "D" 'delete
        '            SQL = getSqlDeleteOracle(table, whr)
        '            'Case "UI" ' if exist then update else insert
        '            '    Dim USQL As String = getUpdateSql(table, fld, whr, True)
        '            '    Dim ISQL As String = getInsertSql(table, fld, whr)
        '            '    Dim WSQL As String = ""
        '            '    For Each whrName As String In whr.Keys
        '            '        WSQL = WSQL & whrName & "='" & outputControl.replaceSingleQuote(whr.Item(whrName)) & "' and "
        '            '    Next
        '            '    SQL = " if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL & " else " & ISQL
        '            'Case "II" 'if not exist then insert
        '            '    Dim ISQL As String = getInsertSql(table, fld, whr)
        '            '    Dim WSQL As String = ""
        '            '    For Each whrName As String In whr.Keys
        '            '        WSQL = WSQL & whrName & "='" & outputControl.replaceSingleQuote(whr.Item(whrName)) & "' and "
        '            '    Next
        '            '    SQL = " if not exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & ISQL
        '            'Case "UU" 'if exit then update
        '            '    Dim USQL As String = getUpdateSql(table, fld, whr, True)
        '            '    Dim WSQL As String = ""
        '            '    For Each whrName As String In whr.Keys
        '            '        WSQL = WSQL & whrName & "='" & outputControl.replaceSingleQuote(whr.Item(whrName)) & "' and "
        '            '    Next
        '            '    SQL = " if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL
        '    End Select
        '    Return SQL
        'End Function

        Public Function checkSQLValType(valGet As String, Optional isWhere As Boolean = True, Optional strSplit As String = ":") As String
            Dim val As String = String.Empty
            Dim typeString As Boolean = True
            Dim nullNotShow As Boolean = False

            If valGet IsNot String.Empty And valGet IsNot Nothing Then
                Dim textSplit() As String = valGet.Split(strSplit)
                val = textSplit(0)
                If textSplit.Count > 2 And val = String.Empty Then
                    nullNotShow = True
                ElseIf textSplit.Count > 1 Then 'no : =char,have : =number/Manual/Date/fuction
                    typeString = False
                Else
                    val = outputControl.replaceSingleQuote(val)
                End If

            End If
            Return If(nullNotShow, String.Empty, addSingleQuot(val, typeString) & If(isWhere, VarIni.A, VarIni.C))
        End Function

        'Function getSqlInsertOracle(table As String, fldInsert As Hashtable, Optional strSplit As String = ":") As String
        '    Dim ASQL As String = String.Empty     ' fld of table
        '    Dim BSQL As String = String.Empty    ' value of table separate=: 0=val,1type=varchar,number,date
        '    If fldInsert.Count = 0 Then
        '        Return String.Empty
        '    End If

        '    For Each fldName As String In fldInsert.Keys
        '        ASQL &= fldName & VarIni.C 'field
        '        BSQL &= checkSQLValType(fldInsert.Item(fldName), False, strSplit) 'value
        '    Next
        '    Return VarIni.I & table & "(" & ASQL.Substring(0, ASQL.Length - 1) & ") values(" & BSQL.Substring(0, BSQL.Length - 1) & ")"
        'End Function

        'Function getSqlInsertOracle(table As String, fldInsert As ArrayList, SQL As String) As String
        '    Dim ASQL As String = String.Empty     ' fld of table
        '    Dim AB As Boolean = True
        '    If fldInsert.Count = 0 Then
        '        AB = False
        '    End If
        '    For Each fldName As String In fldInsert
        '        ASQL &= fldName & VarIni.C 'field
        '    Next
        '    Return VarIni.I & table & addBracket(ASQL.Substring(0, ASQL.Length - 1), AB) & VarIni.SP & SQL
        'End Function

        'Function getSqlUpdateOracle(ByVal table As String, ByVal fld As Hashtable, ByVal whr As Hashtable, Optional strSplit As String = ":") As String

        '    If fld.Count = 0 Then
        '        Return String.Empty
        '    End If
        '    Dim ASQL As String = ""     ' fld and value of table
        '    Dim BSQL As String = ""     ' where of table
        '    ' field and value of table
        '    For Each fldName As String In fld.Keys
        '        ASQL &= fldName & VarIni.E & checkSQLValType(fld.Item(fldName), False, strSplit)
        '    Next
        '    'where of table 
        '    For Each whrName As String In whr.Keys
        '        BSQL &= whrName & VarIni.E & checkSQLValType(whr.Item(whrName), True, strSplit)
        '    Next
        '    Return VarIni.U & table & VarIni.S2 & ASQL.Substring(0, ASQL.Length - 1) & VarIni.W & BSQL.Substring(0, BSQL.Length - 4)
        'End Function

        'Function getSqlDeleteOracle(ByVal table As String, ByVal whr As Hashtable,
        '                            Optional strSplit As String = ":") As String
        '    Dim ASQL As String = "" ' where of table
        '    For Each whrName As String In whr.Keys
        '        Dim valFind As String = checkSQLValType(whr.Item(whrName), True, strSplit)
        '        ASQL &= If(valFind = String.Empty, String.Empty, whrName & VarIni.E & valFind)
        '    Next
        '    Return VarIni.D & VarIni.F & table & VarIni.W & ASQL.Substring(0, ASQL.Length - 4)
        'End Function

        Function getFeild(al As ArrayList, Optional strSplit As String = ":") As String
            'Dim fld As String = String.Empty
            'If al.Count > 0 Then
            '    For Each str As String In al
            '        Dim temp() As String = str.Split(strSplit)
            '        fld &= If(temp.Length > 0, temp(0), "") & If(temp.Length > 1, " " & temp(1), "") & VarIni.C
            '    Next
            'Else
            '    fld = " * ,"
            'End If

            'Return fld.Substring(0, fld.Length - 1)
            Dim listcont As New ListControl
            Return getFeild(listcont.FromArrayList(al), strSplit)
        End Function

        Function GetFeild(lst As List(Of String), Optional strSplit As String = VarIni.C8) As String
            Dim fld As String = String.Empty
            With New ListControl(New List(Of String))
                If lst.Count > 0 Then
                    For Each str As String In lst
                        Dim temp() As String = str.Split(strSplit)
                        Dim fldName As String = ""
                        Dim aliasName As String = ""
                        Dim line As Integer = 0

                        For Each txt As String In temp
                            Select Case line
                                Case 0
                                    fldName = txt
                                Case 1
                                    aliasName = VarIni.SP & txt
                            End Select
                            line += 1
                        Next
                        If Not String.IsNullOrEmpty(fldName) OrElse Not String.IsNullOrEmpty(aliasName) Then
                            .Add(fldName & aliasName)
                        End If
                    Next
                Else
                    .Add("*")
                End If
                fld = .Text(VarIni.C)
            End With
            Return fld
        End Function

        'Function getFeild(al() As String, Optional strSplit As String = ":") As String
        '    Dim fld As String = ""
        '    If al.Count > 0 Then
        '        For Each str As String In al
        '            Dim temp() As String = str.Split(strSplit)
        '            fld &= If(temp.Length > 0, temp(0), "") & If(temp.Length > 1, " " & temp(1), "") & ","
        '        Next
        '    Else
        '        fld = " * ,"
        '    End If

        '    Return fld.Substring(0, fld.Length - 1)
        'End Function
        Function Field(fld As String, Optional alaisName As String = "") As String
            Dim fldText As String = String.Empty
            With New ListControl(New List(Of String))
                .Add(fld)
                .Add(alaisName)
                fldText = .Text(VarIni.SP)
            End With
            Return fldText
        End Function

        Public Function addAliasName(aliasName As String, Optional showDot As Boolean = False) As String
            Dim valReturn As String = ""
            If aliasName <> "" Then
                Dim dot As String = ""
                Dim showBack As Boolean = True
                If showDot Then
                    dot = "."
                    showBack = False
                End If
                valReturn = addSpace(aliasName & dot,, showBack)
            End If
            Return valReturn
        End Function

        Function getLeftjoinManual(TableJoin As String, StingLeftjoin As String,
                                   Optional AliasLeft As String = "",
                                   Optional AliasRight As String = "") As String 'T1=table target,T2=table refer A1=alias Name of T1 ,A2 alias Name of T2
            Dim strLeftJoin As String = addSpace(VarIni.L & TableJoin & addAliasName(AliasLeft) & VarIni.O2 & VarIni.oneEqualOne)
            strLeftJoin &= getEqualList(StingLeftjoin, AliasLeft, AliasRight)
            Return strLeftJoin
        End Function

        Function getLeftjoinManual(TableJoin As String, LeftJoinList As ArrayList,
                                   Optional AliasLeft As String = "",
                                   Optional AliasRight As String = "") As String 'T1=table target,T2=table refer A1=alias Name of T1 ,A2 alias Name of T2
            Dim strLeftJoin As String = addSpace(VarIni.L & TableJoin & addAliasName(AliasLeft) & VarIni.O2 & VarIni.oneEqualOne)
            strLeftJoin &= getEqualList(LeftJoinList, AliasLeft, AliasRight)
            Return strLeftJoin
        End Function

        Function getEqualList(LeftjoinList As ArrayList,
                              Optional AliasLeft As String = "",
                              Optional AliasRight As String = "") As String
            Dim strReturn As String = String.Empty
            If LeftjoinList.Count > 0 Then
                For Each fld1 As String In LeftjoinList
                    strReturn &= checkSingleQuote(fld1, addAliasName(AliasLeft, True), addAliasName(AliasRight, True))
                Next
            End If
            Return strReturn
        End Function

        Function getEqualList(StringLeftjoin As String,
                              Optional AliasLeft As String = "",
                              Optional AliasRight As String = "") As String
            Dim strReturn As String = String.Empty
            If StringLeftjoin IsNot String.Empty Then
                Dim sepaFld() As String = StringLeftjoin.Trim.Split(",")
                For Each fld As String In sepaFld
                    strReturn &= checkSingleQuote(fld, addAliasName(AliasLeft, True), addAliasName(AliasRight, True))
                Next
            End If
            Return strReturn
        End Function


        Function getEqualSingle(fld As String, val As String,
                                Optional singleQuot As Boolean = False,
                                Optional showAnd As Boolean = True,
                                Optional showEqual As Boolean = True) As String
            Dim andShow As String = ""
            If showAnd Then
                andShow = VarIni.A
            End If
            'add by noi date 08/03/2018 
            Dim equalShow As String = ""
            If showEqual Then
                equalShow = VarIni.E
            End If
            'add by noi date 08/03/2018 
            If singleQuot Then
                val = addSpace(VarIni.SQ & val & VarIni.SQ)
            End If
            Return addSpace(andShow & fld & equalShow & val) 'add by noi date 08/03/2018 
        End Function


        Function checkSingleQuote(fld1 As String, aliasLeft As String, aliasRight As String, Optional showAnd As Boolean = True) As String
            Dim strReturn As String = String.Empty
            If fld1.Trim <> "" Then
                Dim fld2() As String = fld1.Split(":")
                If fld2.Length > 1 Then
                    Dim showSQ As Boolean = False
                    If fld2.Length = 3 Then
                        showSQ = True
                    End If
                    'add by noi date 08/03/2018 
                    Dim showEqual As Boolean = True
                    If fld2.Length = 4 Then
                        showEqual = False
                    End If
                    Dim showAliasRight As Boolean = True
                    If fld2.Length = 5 Then
                        showAliasRight = False
                        Dim val As String = fld2(4)
                        If val <> "" Then
                            showSQ = True
                        End If
                    End If
                    strReturn &= getEqualSingle(aliasLeft & fld2(0), If(showAliasRight, aliasRight, String.Empty) & fld2(1), showSQ, showAnd, showEqual)
                    'add by noi date 08/03/2018 
                End If
            End If
            Return strReturn
        End Function

        Function AddComma(strList As List(Of String)) As String
            Return String.Join(VarIni.C, strList)
        End Function

        Function AddComma(str As String) As String
            Return str & VarIni.C
        End Function

        'add by noi on 2021/02/24
        Function GroupBy(fldListgroup As List(Of String)) As String
            Return If(fldListgroup.Count = 0, String.Empty, AddSpace(New List(Of String) From {VarIni.G, AddComma(fldListgroup)}))
        End Function

        Function getGroupBy(fldOrder As String) As String
            Return If(fldOrder = String.Empty, String.Empty, GroupBy(New List(Of String) From {fldOrder}))
        End Function

        'add by noi on 2021/02/24
        Function OrderBy(fldOrderList As List(Of String)) As String
            Return If(fldOrderList.Count = 0, String.Empty, AddSpace(New List(Of String) From {VarIni.O, AddComma(fldOrderList)}))
        End Function
        Function getOrderBy(fldOrder As String) As String
            Return If(fldOrder = String.Empty, String.Empty, OrderBy(New List(Of String) From {fldOrder}))
        End Function

        Function SQLOffset(Optional endRow As Integer = 0) As String
            Return If(endRow = 0, String.Empty, addSpace("OFFSET " & endRow & " ROWS"))
        End Function

        Function SQLFetchFirst(Optional rowcount As Integer = 1) As String
            Return If(rowcount > 0, addSpace("FETCH FIRST " & rowcount & " ROWS ONLY"), String.Empty)
        End Function

        Function SQLFetchFirstTies(Optional rowcount As Integer = 1) As String
            Return If(rowcount > 0, addSpace("FETCH FIRST " & rowcount & " ROWS WITH TIES"), String.Empty)
        End Function

        Function SQLFetchNext(Optional rowcount As Integer = 1) As String
            Return If(rowcount > 0, addSpace("FETCH NEXT " & rowcount & " ROWS ONLY"), String.Empty)
        End Function

        Function SQLFetchNextTies(Optional rowcount As Integer = 1) As String
            Return If(rowcount > 0, addSpace("FETCH NEXT " & rowcount & " ROWS WITH TIES"), String.Empty)
        End Function

        'add by noi on 2021/02/24
        Function AddSpace(strList As List(Of String), Optional front As Boolean = True, Optional back As Boolean = True, Optional conStr As String = "") As String
            Return String.Join("", New List(Of String) From {
                               If(front, VarIni.SP, VarIni.NSP),
                               String.Join(conStr, strList),
                               If(back, VarIni.SP, VarIni.NSP)
                               })
        End Function

        'update by noi on 2021/02/24
        Function addSpace(str As String, Optional front As Boolean = True, Optional back As Boolean = True) As String
            Return AddSpace(New List(Of String) From {str}, front, back)
        End Function
        Function addBracket(str As String, Optional addBacket As Boolean = True) As String
            Return If(addBacket, VarIni.B1 & str & VarIni.B2, str)
        End Function

        Function addSingleQuot(str As String, Optional addSQ As Boolean = True) As String
            Return If(addSQ, VarIni.SQ & str & VarIni.SQ, str)
        End Function

        Public Function addAlias(aliasName As String, Optional showAlias As Boolean = False) As String
            Return If(showAlias, aliasName, String.Empty)
        End Function

        Function checkField(fldName As ArrayList) As ArrayList
            If fldName.Count = 0 Then
                fldName.Add(VarIni.A2)
            End If
            Return fldName
        End Function

        Function StartWithConnectBy(fldStart As ArrayList, fldConnect As ArrayList) As String
            Return "START WITH " & getEqualList(fldStart) & "  CONNECT BY nocycle " & getEqualList(fldConnect)
        End Function

        Function getReferDb(db As String, tableName As String) As String
            Return db & VarIni.dot & tableName
        End Function

        Function SUM(fld As String, Optional strSpit As String = ":",
                     Optional showAlias As Boolean = False,
                     Optional nameAlias As String = "") As String
            If fld Is String.Empty Then
                Return String.Empty
            End If
            Dim aliasName As String = If(nameAlias = String.Empty, fld, nameAlias)
            Return "SUM" & addBracket(fld) & addAlias(strSpit & aliasName, showAlias)
        End Function
        Function ISNULL(fld As String,
                        Optional whenEmpty As String = "''",
                        Optional strSpit As String = VarIni.C8,
                        Optional showAlias As Boolean = False,
                        Optional nameAlias As String = "") As String
            If fld Is String.Empty Then
                Return String.Empty
            End If
            Dim aliasName As String = If(nameAlias = String.Empty, fld, nameAlias)
            Return "ISNULL" & addBracket(fld & VarIni.C & whenEmpty) & addAlias(strSpit & aliasName, showAlias)
        End Function


    End Class
End Namespace