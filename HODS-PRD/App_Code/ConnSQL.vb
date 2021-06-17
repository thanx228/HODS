
Imports Microsoft.VisualBasic

Public Class ConnSQL
    'Public MIS_ConnectionString As String = "Data Source=192.168.50.1;Initial Catalog= DBMIS;User Id=mis;Password=Mis2012;Max Pool Size=100"
    Public DBMain As String = "HOOTHAI"
    Public DBReport As String = "HOOTHAI_REPORT"
    Public MIS_ConnectionString As String = "Data Source=192.168.50.6;Initial Catalog= " & DBReport & ";User Id=sa;Password=Jinpa0;Max Pool Size=100"
    Public ERP_ConnectionString As String = "Data Source=192.168.50.6;Initial Catalog= " & DBMain & ";User Id=sa;Password=Jinpa0;Max Pool Size=100"

    Public ERMIS_HTI As String = "hoothai"
    Public ERMIS_ConnectionString As String = "Data Source=192.168.1.17;port=3306;Initial Catalog=" & ERMIS_HTI & ";User Id=uthai;password=tang"

    'Public PDM_ConnectionString As String = ""

    'Public Shared PDMDBServerName As String = "jpdm"
    'Public Shared PDMDBUserName As String = "Alex"
    'Public Shared PDMDBPassWord As String = "Alex0717"
    'Public Shared PDMDBName As String = "df3_0"
    'Public Shared strPDMConnectionString As String = "Data Source=" & PDMDBServerName & ";Persist Security Info=True;User Id=" & PDMDBUserName & ";Password=" & PDMDBPassWord & ";"

    Public Sub Exec_Sql(ByVal str_sqlcommand As String, ByVal Connection_str As String)
        If str_sqlcommand <> "" Then
            Dim sqlConnection1 As New System.Data.SqlClient.SqlConnection(Connection_str)
            Dim cmd As New System.Data.SqlClient.SqlCommand
            Dim returnValue As Object
            cmd.CommandText = str_sqlcommand
            cmd.CommandType = Data.CommandType.Text
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            returnValue = cmd.ExecuteScalar()
            sqlConnection1.Close()
        End If
    End Sub

    Public Function exeSQL(ByVal str_sqlcommand As String, ByVal Connection_str As String) As Integer
        Dim valReturn As Integer
        If str_sqlcommand <> "" Then
            Dim sqlConnection1 As New System.Data.SqlClient.SqlConnection(Connection_str)
            Dim cmd As New System.Data.SqlClient.SqlCommand
            'Dim returnValue As Object
            cmd.CommandText = str_sqlcommand
            cmd.CommandType = Data.CommandType.Text
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            Try
                valReturn = cmd.ExecuteNonQuery()
            Catch ex As Exception
                valReturn = -1
            End Try
            sqlConnection1.Close()
            cmd = Nothing
            sqlConnection1 = Nothing
        End If

        Return valReturn
    End Function

    'Public Sub exeTranSQL(ByVal str_sqlcommand As String, ByVal Connection_str As String)

    '    If str_sqlcommand <> "" Then
    '        ' Dim sqlConnection1 As New System.Data.SqlClient.SqlConnection(Connection_str)
    '        Dim cmd As New System.Data.SqlClient.SqlCommand

    '        Dim objConn As System.Data.OleDb.OleDbConnection
    '        Dim objCmd As System.Data.OleDb.OleDbCommand

    '        objConn = New System.Data.OleDb.OleDbConnection(Connection_str)
    '        objConn.Open()

    '        Dim Trans As OleDbTransaction
    '        Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted)

    '        Try
    '            objCmd = New System.Data.OleDb.OleDbCommand()
    '            With objCmd
    '                .Connection = objConn
    '                .Transaction = Trans
    '                .CommandType = CommandType.Text
    '                .CommandText = Connection_str
    '            End With
    '            objCmd.ExecuteNonQuery()
    '            Trans.Commit()
    '        Catch ex As Exception
    '            Trans.Rollback()
    '        End Try

    '        objCmd = Nothing
    '        objConn.Close()
    '        objConn = Nothing

    '    End If

    'End Sub


    ' Get_DataReader ?RECORDSET??
    '********************************************************************* 
    Public Function Get_DataReader(ByVal str_sqlcommand As String, ByVal Connection_str As String) As Data.DataTable
        Get_DataReader = New Data.DataTable
        If str_sqlcommand <> "" Then
            Dim aa As New Data.DataTable
            Using connection As New System.Data.SqlClient.SqlConnection(Connection_str)
                Dim adapter As New System.Data.SqlClient.SqlDataAdapter()
                adapter.SelectCommand = New System.Data.SqlClient.SqlCommand(str_sqlcommand, connection)
                adapter.Fill(aa)
                Return aa
            End Using
        End If
    End Function

    Public Function Get_DataReader_ERMIS(ByVal str_sqlcommand As String, ByVal Connection_str As String) As Data.DataTable
        Get_DataReader_ERMIS = New Data.DataTable
        If str_sqlcommand <> "" Then
            Dim dt As New Data.DataTable
            Using con As New MySql.Data.MySqlClient.MySqlConnection(Connection_str)
                Using cmd As New MySql.Data.MySqlClient.MySqlCommand(str_sqlcommand)
                    Using sda As New MySql.Data.MySqlClient.MySqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        sda.Fill(dt)
                    End Using
                End Using
            End Using
        End If
    End Function

    '*********************************************************************   
    '   
    ' Get_value() ??   
    '   
    ' ????,??SQL?? (SELECT)
    '
    ' str_sqlcommand:sql??
    ' Connection_str :???????
    '
    '
    ' Get_value ??????
    '********************************************************************* 
    Public Function Get_value(ByVal str_sqlcommand As String, ByVal Connection_str As String) As String
        Get_value = ""
        If str_sqlcommand <> "" Then

            Dim sqlConnection1 As New System.Data.SqlClient.SqlConnection(Connection_str)
            Dim cmd As New System.Data.SqlClient.SqlCommand
            Dim returnValue As String = ""

            cmd.CommandText = str_sqlcommand
            cmd.CommandType = Data.CommandType.Text
            cmd.Connection = sqlConnection1

            sqlConnection1.Open()

            If IsDBNull(cmd.ExecuteScalar()) = True Then
                returnValue = "0"
            Else
                returnValue = cmd.ExecuteScalar()
            End If

            sqlConnection1.Close()
            Get_value = returnValue
            Return Get_value
        End If
    End Function

    Public Function SetConditionWhr(fld As String, valFrom As String, valTo As String, Optional includeAnd As Boolean = True) As String
        Dim whrReturn As String = ""
        If includeAnd = True Then
            whrReturn = " and "
        End If
        If valFrom <> "" Or valTo <> "" Then
            If valFrom <> "" And valTo <> "" Then
                whrReturn = whrReturn & " " & fld & " between '" & valFrom & "' and '" & valTo & "' "
            Else
                Dim val1 As String = valFrom
                If valFrom = "" Then
                    val1 = valTo
                End If
                whrReturn = whrReturn & " " & fld & " ='" & val1 & "' "
            End If
        End If
        Return whrReturn
    End Function

    Public Function GetSQL(ByVal table As String, ByVal fldInsert As Hashtable, ByVal fldUpdate As Hashtable, ByVal whr As Hashtable) As String
        Dim ISQL As String = getInsertSql(table, fldInsert, whr)
        Dim USQL As String = getUpdateSql(table, fldUpdate, whr)
        Dim WSQL As String = ""
        For Each whrName As String In whr.Keys
            WSQL = WSQL & whrName & "='" & replaceSingleQuote(whr.Item(whrName)) & "' and "
        Next
        Return " if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL & " else " & ISQL & ";"
    End Function

    Public Function GetSQL(ByVal table As String, ByVal fld As Hashtable, ByVal whr As Hashtable, Optional ByVal typeSql As String = "UI") As String
        Dim SQL As String = ""
        typeSql = typeSql.ToUpper
        Select Case typeSql
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
                    WSQL = WSQL & whrName & "='" & replaceSingleQuote(whr.Item(whrName)) & "' and "
                Next
                SQL = " if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL & " else " & ISQL
            Case "II" 'if not exist then insert
                Dim ISQL As String = getInsertSql(table, fld, whr)
                Dim WSQL As String = ""
                For Each whrName As String In whr.Keys
                    WSQL = WSQL & whrName & "='" & replaceSingleQuote(whr.Item(whrName)) & "' and "
                Next
                SQL = " if not exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & ISQL
            Case "UU" 'if exit then update
                Dim USQL As String = getUpdateSql(table, fld, whr, True)
                Dim WSQL As String = ""
                For Each whrName As String In whr.Keys
                    WSQL = WSQL & whrName & "='" & replaceSingleQuote(whr.Item(whrName)) & "' and "
                Next
                SQL = " if exists(select * from " & table & " where " & WSQL.Substring(0, WSQL.Length - 4) & " ) " & USQL
        End Select
        Return SQL & ";"
    End Function

    Protected Function getInsertSql(table As String, fld As Hashtable, whr As Hashtable) As String
        Dim ASQL As String = ""     ' fld of table
        Dim BSQL As String = ""     ' value of table
        'Dim SQL As String = ""      ' merge ASQL and BSQL
        'Dim moData() As String = lbMO.Text.Split("-")
        For Each fldName As String In whr.Keys
            Dim textSplit() As String = fldName.Split(":")
            Dim wordPrefix As String = "",
                realFldName As String = textSplit(0)
            If textSplit.Count > 1 Then
                wordPrefix = textSplit(1)
            End If
            ASQL = ASQL & realFldName & ","
            BSQL = BSQL & "'" & replaceSingleQuote(whr.Item(fldName)) & "',"
        Next
        For Each fldName As String In fld.Keys
            Dim textSplit() As String = fldName.Split(":")
            Dim wordPrefix As String = "",
                realFldName As String = textSplit(0)
            If textSplit.Count > 1 Then
                wordPrefix = textSplit(1)
            End If
            ASQL = ASQL & realFldName & ","
            BSQL = BSQL & wordPrefix & "'" & replaceSingleQuote(fld.Item(fldName)) & "',"
        Next
        Return "insert into " & table & "(" & ASQL.Substring(0, ASQL.Length - 1) & ") values(" & BSQL.Substring(0, BSQL.Length - 1) & ")"
    End Function

    Protected Function getUpdateSql(ByVal table As String, ByVal fld As Hashtable, ByVal whr As Hashtable, Optional ByVal setQuot As Boolean = False) As String

        If fld.Count = 0 Then
            Return ""
        End If

        Dim chrQuot As String = ""
        If setQuot Then
            chrQuot = "'"
        End If

        Dim ASQL As String = ""     ' fld and value of table
        Dim BSQL As String = ""     ' where of table
        ' field and value of table
        For Each fldName As String In fld.Keys
            Dim textSplit() As String = fldName.Split(":")
            Dim wordPrefix As String = "",
                realFldName As String = textSplit(0)
            If textSplit.Count > 1 Then
                wordPrefix = textSplit(1)
            End If
            ASQL = ASQL & realFldName & "= " & wordPrefix & chrQuot & fld.Item(fldName) & chrQuot & ","
        Next
        'where of table 
        For Each whrName As String In whr.Keys
            BSQL = BSQL & whrName & "='" & replaceSingleQuote(whr.Item(whrName)) & "' and "
        Next
        Return " update " & table & " set " & ASQL.Substring(0, ASQL.Length - 1) & " where " & BSQL.Substring(0, BSQL.Length - 4)
    End Function


    Function getDeleteSql(ByVal table As String, ByVal whr As Hashtable) As String
        Dim ASQL As String = ""     ' where of table
        For Each whrName As String In whr.Keys
            ASQL = ASQL & whrName & "='" & replaceSingleQuote(whr.Item(whrName)) & "' and "
        Next
        Return " delete from " & table & " where " & ASQL.Substring(0, ASQL.Length - 4)
    End Function

    Protected Function replaceSingleQuote(val As String) As String
        If String.IsNullOrEmpty(val) Then
            Return ""
        Else
            Return val.Replace("'", "''")
        End If
    End Function

    Function Where(ByVal fld As String, ByRef checkboxList As CheckBoxList, Optional ByVal notIn As Boolean = False, Optional ByVal valDefault As String = "", Optional ByVal selectAll As Boolean = False) As String
        Dim type As String = "",
            cnt As Decimal = 0,
            whr As String = "",
            typeAll As String = ""
        For Each boxItem As ListItem In checkboxList.Items
            Dim boxVal As String = CStr(boxItem.Value.Trim)
            typeAll = typeAll & "'" & boxVal & "',"
            If boxItem.Selected Then
                type = type & "'" & boxVal & "',"
                cnt = cnt + 1
            End If
        Next
        If cnt > 0 Then
            Dim strIn As String = " in "
            If notIn Then
                strIn = " not in "
            End If
            whr = " and " & fld & " " & strIn & " (" & type.Substring(0, type.Count - 1) & ")"
        Else
            If valDefault <> "" Then
                whr = " and " & fld & " in (" & valDefault & ")"
            End If
            If selectAll Then
                Dim strIn As String = " in "
                If notIn Then
                    strIn = " not in "
                End If
                whr = " and " & fld & strIn & " (" & typeAll.Substring(0, typeAll.Count - 1) & ")"
            End If
        End If
        Return whr
    End Function

    Function Where(fld As String, ByRef dropdownList As DropDownList, Optional notValue As String = "ALL") As String
        Dim dataGet As String = dropdownList.Text,
            whr As String = ""
        If dataGet <> notValue Then
            whr = " and " & fld & "='" & dataGet & "' "
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
                whr = " and " & fld & " like '%" & val & "%' "
            Else
                whr = " and " & fld & " = '" & val & "' "
            End If
        End If
        Return whr
    End Function

    Function Where(ByVal fldDate As String, ByVal dateFrom As String, ByVal dateTo As String, Optional ByVal causeVal As Boolean = False, Optional ByVal showAnd As Boolean = True) As String
        If fldDate = "" Or (dateFrom = "" And dateTo = "") Then
            Return ""
        End If
        Dim whr As String = " "
        If showAnd Then
            whr = " and "
        End If
        If dateFrom <> "" And dateTo <> "" Then
            whr = whr & fldDate & " between '" & dateFrom & "' and '" & dateTo & "' "
        Else
            Dim symbol As String = ">='"
            If causeVal Then
                symbol = "='"
            End If
            Dim dateSel As String = symbol & dateFrom & "' "
            If dateFrom = "" Then
                symbol = "<='"
                If causeVal Then
                    symbol = "='"
                End If
                dateSel = symbol & dateTo & "' "
            End If
            whr = whr & fldDate & dateSel
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

    Function Where(fld As String, val As String, Optional showAnd As Boolean = True, Optional manual As Boolean = True) As String
        Dim andShow As String = ""
        If showAnd Then
            andShow = " and "
        End If
        If Not manual Then
            val = " ='" & val & "' "
        End If
        Return andShow & fld & val
    End Function

    Function Where(fld As String, ByRef checkbox As CheckBox, Optional ByVal chkTrue As String = "Y")

        Dim val As String = ""
        If checkbox.Checked Then
            val = " and " & fld & "='" & chkTrue & "'"
        End If
        Return val
    End Function

    'add 2015-07-16 by noi
    Function checkNumberic(ByVal tb As TextBox) As Decimal
        Dim valReturn As Decimal = 0
        Dim val As String = tb.Text.Trim
        If val <> "" And IsNumeric(val) Then
            valReturn = CInt(val)
        End If
        Return valReturn
    End Function

    Function checkNumberic(ByVal str As String) As Decimal
        Dim valReturn As Decimal = 0
        If str <> "" And IsNumeric(str) Then
            valReturn = CDec(str)
        End If
        Return valReturn
    End Function

    'add 2015-09-07 by noi
    Function EncodeTo64UTF8(ByVal valforEnCode As String) As String
        Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(valforEnCode))
    End Function

    Function DecodeFrom64(ByVal valforDeCode As String) As String
        Return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(valforDeCode))
    End Function

    Function checkStringValue(str As String) As String
        Return str.Replace("&nbsp;", "").Trim
    End Function

End Class