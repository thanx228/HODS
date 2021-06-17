Namespace DataControl
    Public Class SQLString
        Inherits DataConnectControl

        Dim listcont As New ListControl
        Dim outcont As New OutputControl

        Public Table As String = String.Empty
        Public SQL As String = String.Empty
        Public fieldSelect As String = String.Empty
        Public LeftJoin As String = String.Empty
        Public WhrStr As String = String.Empty
        Public OrdBy As String = String.Empty
        Public GrpBy As String = String.Empty
        Public Offset As String = String.Empty
        Public FetchFirst As String = String.Empty

        Sub New(TableName As String)
            Table = TableName
        End Sub

        Sub New(TableName As String, fldName As ArrayList, Optional strSplit As String = VarIni.char8, Optional top As Integer = 0)
            Table = TableName
            SetField(fldName, strSplit)
            SetSelect(fldName, strSplit, top)
        End Sub

        Sub SetSelect(fldName As ArrayList, Optional strSplit As String = VarIni.char8, Optional top As Integer = 0)
            SQL = VarIni.S & If(top <= 0, "", " TOP (" & top.ToString & ") ") & getFeild(fldName, strSplit) & VarIni.F & Table
        End Sub

        Sub SetSelect(Optional top As Integer = 0)
            SQL = VarIni.S & If(top <= 0, "", " TOP (" & top.ToString & ") ") & fieldSelect & VarIni.F & Table
        End Sub

        Sub SetField(fldName As ArrayList, Optional strSplit As String = VarIni.char8)
            fieldSelect = getFeild(fldName, strSplit)
        End Sub

        Sub SetField(fldName As List(Of String), Optional strSplit As String = VarIni.char8)
            fieldSelect = GetFeild(fldName, strSplit)
        End Sub

        Sub SetWhere(whrAdd As String, Optional showWhere As Boolean = False)
            WhrStr &= If(showWhere, VarIni.W & VarIni.oneEqualOne, String.Empty) & whrAdd
        End Sub

        'add by noi on 2021/03/25
        Sub SetWhere(whrList As List(Of String), Optional showWhere As Boolean = False)
            If whrList.Count > 0 Then
                With New ListControl(whrList)
                    WhrStr &= If(showWhere, VarIni.W, String.Empty) & .Text(VarIni.A)
                End With
            End If
        End Sub


        Sub SetGroupBy(grpByVal As String)
            GrpBy = getGroupBy(grpByVal)
        End Sub

        Sub SetOrderBy(ordByAdd As String)
            OrdBy = getOrderBy(ordByAdd)
        End Sub

        Sub SetOrderBy(fldList As List(Of String), Optional isGroupBy As Boolean = False)
            With New ListControl(fldList)
                Dim txt As String = .Text(VarIni.C)
                If isGroupBy Then 'group by
                    SetGroupBy(txt)
                Else 'order by
                    SetOrderBy(txt)
                End If
            End With
        End Sub

        Sub setLeftjoin(leftJoinAdd As String)
            LeftJoin &= leftJoinAdd
        End Sub


        Sub setLeftjoin(tableName As String, fldJoin As String)
            setLeftjoin(addSpace(VarIni.L & addSpace(tableName) & VarIni.O2 & fldJoin))
        End Sub


        Sub SetLeftjoin(tableName As String, LeftJoinList As List(Of String), Optional strSpit As String = VarIni.C8)
            SetLeftjoin2(tableName, LeftJoinList, strSpit, False)
            'Dim strLeftJoin As String = ""
            'If LeftJoinList.Count > 0 OrElse String.IsNullOrEmpty(strSpit) Then
            '    For Each str As String In LeftJoinList
            '        If Not String.IsNullOrEmpty(str) Then
            '            Dim strArr() As String = str.Split(strSpit)
            '            Dim line As Integer = 0
            '            Dim fldLeft As String = ""
            '            Dim fldRight As String = ""
            '            Dim singleQuote As Boolean = False
            '            For Each txt As String In strArr
            '                Select Case line
            '                    Case 0
            '                        fldLeft = txt
            '                    Case 1
            '                        fldRight = txt
            '                    Case 2
            '                        singleQuote = True
            '                End Select
            '                line += 1
            '            Next
            '            If Not String.IsNullOrEmpty(fldLeft) AndAlso Not String.IsNullOrEmpty(fldRight) Then
            '                strLeftJoin &= WHERE_JOIN(fldLeft, fldRight, singleQuote)
            '            End If
            '        End If
            '    Next
            'End If
            'setLeftjoin(tableName, strLeftJoin)
        End Sub

        Sub SetLeftjoin2(tableName As String,
                         LeftJoinList As List(Of String),
                         Optional strSpit As String = VarIni.C8,
                         Optional formatNew As Boolean = True)
            Dim strLeftJoin As String = ""
            If LeftJoinList.Count > 0 OrElse String.IsNullOrEmpty(strSpit) Then
                strLeftJoin = LJF(LeftJoinList, strSpit, formatNew)
            End If
            setLeftjoin(tableName, strLeftJoin)
        End Sub

        'Left Join
        Private Function LJ(TableJoin As String, LeftJoinList As List(Of String)) As String
            If String.IsNullOrEmpty(TableJoin) OrElse LeftJoinList.Count <= 0 Then
                Return String.Empty
            End If
            Return addSpace(New List(Of String) From {VarIni.L, TableJoin, VarIni.O2}) & addSpace(LeftJoinList, True, True, VarIni.A)
        End Function
        'LJF=Left Join Format
        Private Function LJF(fldLeft As String, fldRight As String, Optional conStr As String = "=", Optional fldRightSigleQuote As Boolean = False) As String
            Return WHERE_EQUAL(fldLeft, fldRight, conStr, fldRightSigleQuote, False) 'fldLeft & conStr & fldRight
        End Function

        Private Function LJF(formatList As List(Of String),
                             Optional strSplit As String = VarIni.C8,
                             Optional formatNew As Boolean = True) As String
            Try
                With New ListControl(New List(Of String))
                    For Each txt As String In formatList
                        If String.IsNullOrEmpty(txt) Then
                            Continue For
                        End If
                        Dim data() As String = txt.Split(strSplit)
                        Dim line As Integer = 0
                        Dim fldLeft As String = String.Empty
                        Dim fldRigt As String = String.Empty
                        Dim singleQ As Boolean = False
                        Dim conStr As String = VarIni.E
                        For Each txtSplit As String In data
                            Select Case line
                                Case 0
                                    fldLeft = txtSplit
                                Case 1
                                    fldRigt = txtSplit
                                Case 2
                                    If formatNew Then
                                        conStr = txtSplit
                                    Else
                                        singleQ = True
                                    End If
                                Case 3
                                    If formatNew Then
                                        singleQ = True
                                    End If
                            End Select
                            line += 1
                        Next
                        If outcont.ISNULL(fldLeft) OrElse outcont.ISNULL(fldRigt) Then
                            Continue For
                        End If
                        .Add(LJF(fldLeft, fldRigt, conStr, singleQ))
                    Next
                    Return .Text(VarIni.A)
                End With
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        'Sub setLeftjoin(tableName As String, LeftJoinHash As Hashtable, Optional strSpit As String = VarIni.C8)
        '    Dim strLeftJoin As String = ""
        '    If LeftJoinHash.Count > 0 OrElse String.IsNullOrEmpty(strSpit) Then
        '        For Each data As DictionaryEntry In LeftJoinHash
        '            Dim strArr() As String = data.Value.Split(strSpit)
        '            Dim line As Integer = 0
        '            Dim fldLeft As String = data.Key
        '            Dim fldRight As String = ""
        '            Dim singleQuote As Boolean = False
        '            For Each txt As String In strArr
        '                Select Case line
        '                    Case 0
        '                        fldRight = txt
        '                    Case 1
        '                        singleQuote = True
        '                End Select
        '                line += 1
        '            Next
        '            If Not String.IsNullOrEmpty(fldRight) Then
        '                strLeftJoin &= WHERE_JOIN(fldLeft, fldRight, singleQuote)
        '            End If
        '        Next
        '    End If
        '    setLeftjoin(tableName, strLeftJoin)
        'End Sub

        Private Function WHERE_JOIN(fldLeft As String, fldRight As String, Optional fldRightSigleQuote As Boolean = False) As String
            Return WHERE_EQUAL(fldLeft, fldRight, "=", fldRightSigleQuote, True)
        End Function


        Function GetSQLString() As String
            Return SQL & LeftJoin & WhrStr & GrpBy & OrdBy & Offset & FetchFirst
        End Function

        Function GetSubQuery(Optional aliasName As String = "") As String
            Return addBracket(GetSQLString()) & If(String.IsNullOrEmpty(aliasName), "", " " & aliasName)
        End Function

    End Class
End Namespace