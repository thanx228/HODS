Imports System.Data.SqlClient
'Imports System.Data.OracleClient
Imports MySql.Data.MySqlClient
Imports System.Reflection
Imports System.Data
Imports System.Diagnostics

Namespace DataControl
    Public Class DataConnectControl
        Inherits SQLControl
        Dim strConn As New StringConnection
        Dim errCont As New ErrorControl

        'set return null
        Public Sub returnPageError(filePath As String, msgError As String, Optional whoCall As String = "", Optional addition As String = "")
            errCont.GetPageError(filePath, If(whoCall = "", WhoCalledMe(), whoCall), If(addition = "", "DATA IS EMPTY", addition), msgError)
        End Sub

        Private Function returnString(txt As String, Optional fromBack As Integer = 0)
            Return txt.Substring(0, txt.Length - fromBack)
        End Function

        'oracle
        'Public Sub OracleTransaction(ByVal SQL As String, strCon As String, Optional whoCall As String = "") 'single query
        '    Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        '    whoCall &= WhoCalledMe()
        '    If strCon = String.Empty Then
        '        returnPageError(PathFile, "connection string for oracle is empty", returnString(whoCall))
        '    End If

        '    Using connection As New OracleConnection(strCon)
        '        connection.Open()
        '        Dim command As OracleCommand = connection.CreateCommand()
        '        Dim transaction As OracleTransaction

        '        ' Start a local transaction
        '        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
        '        command.Transaction = transaction
        '        Try
        '            With command
        '                .CommandText = SQL
        '                .CommandTimeout = 0
        '                .ExecuteNonQuery()
        '            End With
        '            transaction.Commit()
        '        Catch ex As Exception
        '            Try
        '                transaction.Rollback()
        '            Catch ex2 As Exception
        '                errCont.GetPageError(PathFile, returnString(whoCall), SQL, ex2.Message)
        '            End Try
        '            errCont.GetPageError(PathFile, returnString(whoCall), SQL, ex.Message)
        '        Finally
        '            connection.Close()
        '        End Try
        '    End Using
        'End Sub

        'Public Sub OracleTransaction(ByVal SQLList As ArrayList, strCon As String, Optional whoCall As String = "") 'multi query;
        '    Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        '    whoCall &= WhoCalledMe()
        '    If strCon = String.Empty Then
        '        returnPageError(PathFile, "connection string for oracle is empty", returnString(whoCall))
        '    End If

        '    Using connection As New OracleConnection(strCon)
        '        connection.Open()
        '        Dim command As OracleCommand = connection.CreateCommand()
        '        Dim transaction As OracleTransaction

        '        ' Start a local transaction
        '        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
        '        ' Assign transaction object for a pending local transaction
        '        command.Transaction = transaction
        '        Try
        '            For Each SQL As String In SQLList
        '                With command
        '                    .CommandText = SQL
        '                    .CommandTimeout = 0
        '                    .ExecuteNonQuery()
        '                End With
        '            Next
        '            transaction.Commit()
        '        Catch ex As Exception
        '            Dim strSQL As String = ""
        '            For Each SQL As String In SQLList
        '                strSQL &= SQL & ","
        '            Next
        '            Try
        '                transaction.Rollback()
        '            Catch ex2 As Exception
        '                errCont.GetPageError(PathFile, returnString(whoCall), strSQL, ex2.Message)
        '            End Try
        '            errCont.GetPageError(PathFile, returnString(whoCall), strSQL, ex.Message)
        '        Finally
        '            connection.Close()
        '        End Try
        '    End Using
        'End Sub


        'mysql
        'Public Sub MySqlTransaction(ByVal SQL As String, strCon As String, Optional whoCall As String = "") 'single query

        '    Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        '    whoCall &= WhoCalledMe()
        '    If strCon = String.Empty Then
        '        returnPageError(PathFile, "connection string for MySQL is empty", returnString(whoCall))
        '    End If
        '    Using connection As New MySqlConnection(strCon)
        '        connection.Open()
        '        Dim command As MySqlCommand = connection.CreateCommand()
        '        Dim transaction As MySqlTransaction

        '        ' Start a local transaction
        '        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)

        '        command.Connection = connection
        '        command.Transaction = transaction

        '        Try
        '            With command
        '                .CommandText = SQL
        '                .CommandTimeout = 0
        '                .ExecuteNonQuery()
        '            End With
        '            transaction.Commit()
        '        Catch ex As Exception
        '            Try
        '                transaction.Rollback()
        '            Catch ex2 As Exception
        '                errCont.GetPageError(PathFile, returnString(whoCall), SQL, ex2.Message)
        '            End Try
        '            errCont.GetPageError(PathFile, returnString(whoCall), SQL, ex.Message)
        '        Finally
        '            connection.Close()
        '        End Try
        '    End Using
        'End Sub

        'Public Sub MySqlTransaction(ByVal SQLList As ArrayList, strCon As String, Optional whoCall As String = "") 'multi query

        '    Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        '    whoCall &= WhoCalledMe()
        '    If strCon = String.Empty Then
        '        returnPageError(PathFile, "connection string for MySQL is empty", returnString(whoCall))
        '    End If
        '    Using connection As New MySqlConnection(strCon)
        '        connection.Open()
        '        Dim command As MySqlCommand = connection.CreateCommand()
        '        Dim transaction As MySqlTransaction

        '        ' Start a local transaction
        '        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)

        '        command.Connection = connection
        '        command.Transaction = transaction
        '        Try
        '            For Each SQL As String In SQLList
        '                With command
        '                    .CommandText = SQL
        '                    .CommandTimeout = 0
        '                    .ExecuteNonQuery()
        '                End With
        '            Next
        '            transaction.Commit()
        '        Catch ex As Exception
        '            Dim strSQL As String = ""
        '            For Each SQL As String In SQLList
        '                strSQL &= SQL & ","
        '            Next
        '            Try
        '                transaction.Rollback()
        '            Catch ex2 As Exception
        '                errCont.GetPageError(PathFile, returnString(whoCall), strSQL, ex2.Message)
        '            End Try
        '            errCont.GetPageError(PathFile, returnString(whoCall), strSQL, ex.Message)
        '        Finally
        '            connection.Close()
        '        End Try
        '    End Using
        'End Sub

        'Public Function TransactionSQLReturnRowAffected(ByVal strSQL As String, ByVal DBType As String, whoCall As String) As Integer
        '    whoCall = returnString(whoCall & WhoCalledMe())
        '    If DBType = "" Then
        '        returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
        '    End If
        '    Dim rowReturn As Integer = 0
        '    With strConn
        '        If .checkDbOracle(DBType) Then 'oracle
        '            'OracleTransaction(strSQL, .getStringConnectOracle(DBType), whoCall)
        '        ElseIf strConn.checkDbSQL(DBType) Then 'sqlserver
        '            rowReturn = SqlTransactionReturnRowAffected(strSQL, .getStringConnectSQL(DBType), whoCall)
        '        Else 'my sql
        '            'MySqlTransaction(strSQL, .getStringConnectMySQL(), whoCall)
        '        End If
        '    End With
        '    Return rowReturn
        'End Function

        'Private Function SqlTransactionReturnRowAffected(ByVal SQL As String, strCon As String, whoCall As String) As Integer
        '    Dim rowReturn As Integer = 0
        '    Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        '    whoCall &= WhoCalledMe()
        '    If strCon = String.Empty Then
        '        returnPageError(PathFile, "connection string for MySQL is empty", returnString(whoCall))
        '    End If
        '    Using connection As New SqlConnection(strCon)
        '        connection.Open()
        '        Dim command As SqlCommand = connection.CreateCommand()
        '        Dim transaction As SqlTransaction

        '        ' Start a local transaction
        '        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)

        '        command.Connection = connection
        '        command.Transaction = transaction
        '        Try
        '            With command
        '                .CommandText = SQL
        '                .CommandTimeout = 0
        '                rowReturn = .ExecuteNonQuery()
        '            End With
        '            transaction.Commit()
        '        Catch ex As Exception
        '            Try
        '                transaction.Rollback()
        '            Catch ex2 As Exception
        '                errCont.GetPageError(PathFile, returnString(whoCall), SQL, ex2.Message)
        '            End Try
        '            errCont.GetPageError(PathFile, returnString(whoCall), SQL, ex.Message)
        '        Finally
        '            connection.Close()
        '        End Try
        '    End Using
        '    Return 0
        'End Function

        'sqlserver
        Private Function SqlTransaction(ByVal SQL As String, strCon As String, whoCall As String) As Integer 'single query
            Dim rowReturn As Integer = 0
            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            whoCall &= WhoCalledMe()
            If strCon = String.Empty Then
                returnPageError(PathFile, "connection string for MySQL is empty", returnString(whoCall))
            End If
            Using connection As New SqlConnection(strCon)
                connection.Open()
                Dim command As SqlCommand = connection.CreateCommand()
                Dim transaction As SqlTransaction

                ' Start a local transaction
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)

                command.Connection = connection
                command.Transaction = transaction
                Try
                    With command
                        .CommandText = SQL
                        .CommandTimeout = 0
                        rowReturn = .ExecuteNonQuery()
                    End With
                    transaction.Commit()
                Catch ex As Exception
                    Try
                        transaction.Rollback()
                    Catch ex2 As Exception
                        errCont.GetPageError(PathFile, returnString(whoCall), SQL, ex2.Message)
                    End Try
                    errCont.GetPageError(PathFile, returnString(whoCall), SQL, ex.Message)
                Finally
                    connection.Close()
                End Try
            End Using
            Return rowReturn
        End Function

        Public Function SqlTransaction(ByVal SQLList As ArrayList, strCon As String, Optional whoCall As String = "") As Integer 'multi query
            Dim rowReturn As Integer = 0
            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            whoCall &= WhoCalledMe()
            If strCon = String.Empty Then
                returnPageError(PathFile, "connection string for MySQL is empty", returnString(whoCall))
            End If
            Using connection As New SqlConnection(strCon)
                connection.Open()
                Dim command As SqlCommand = connection.CreateCommand()
                Dim transaction As SqlTransaction

                ' Start a local transaction
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
                command.Connection = connection
                command.Transaction = transaction
                Try
                    For Each SQL As String In SQLList
                        With command
                            .CommandText = SQL
                            .CommandTimeout = 0
                            rowReturn += .ExecuteNonQuery()
                        End With
                    Next
                    transaction.Commit()
                Catch ex As Exception
                    Dim strSQL As String = ""
                    For Each SQL As String In SQLList
                        strSQL &= SQL & ","
                    Next
                    Try
                        transaction.Rollback()
                    Catch ex2 As Exception
                        errCont.GetPageError(PathFile, returnString(whoCall), strSQL, ex2.Message)
                    End Try
                    errCont.GetPageError(PathFile, returnString(whoCall), strSQL, ex.Message)
                Finally
                    connection.Close()
                End Try
            End Using
            Return rowReturn
        End Function

        Public Function TransactionSQL(ByVal strSQL As String, Optional ByVal DBType As String = "", Optional whoCall As String = "") As Integer 'single query
            Dim rowReturn As Integer = 0
            whoCall = returnString(whoCall & WhoCalledMe())
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            With strConn
                If .checkDbOracle(DBType) Then 'oracle
                    'OracleTransaction(strSQL, .getStringConnectOracle(DBType), whoCall)
                ElseIf strConn.checkDbSQL(DBType) Then 'sqlserver
                    rowReturn = SqlTransaction(strSQL, .getStringConnectSQL(DBType), whoCall)
                Else 'my sql
                    'MySqlTransaction(strSQL, .getStringConnectMySQL(), whoCall)
                End If
            End With
            Return rowReturn
        End Function

        Public Function TransactionSQL(ByVal strSQLList As ArrayList, Optional ByVal DBType As String = "", Optional whoCall As String = "") As Integer 'multi query
            Dim rowReturn As Integer = 0
            whoCall = returnString(whoCall & WhoCalledMe())
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            With strConn
                If .checkDbOracle(DBType) Then 'oracle
                    'OracleTransaction(strSQLList, .getStringConnectOracle(DBType), whoCall)
                ElseIf strConn.checkDbSQL(DBType) Then 'sqlserver
                    rowReturn = SqlTransaction(strSQLList, .getStringConnectSQL(DBType), whoCall)
                Else 'my sql
                    'MySqlTransaction(strSQLList, .getStringConnectMySQL(), whoCall)
                End If
            End With
            Return rowReturn
        End Function

        '--Get Error Queary
        Function getMyName() As String
            Dim sf As StackFrame = New StackFrame()
            Dim mb As MethodBase = sf.GetMethod()
            Return mb.Name
        End Function

        '--Get Error Queary
        Function WhoCalledMe() As String
            Dim st As StackTrace = New StackTrace()
            Dim sf As StackFrame = st.GetFrame(1)
            Dim mb As MethodBase = sf.GetMethod()
            Return mb.Name & "==>"
        End Function

        'datatable
        'oracle 
        'Public Function QueryOracle(ByVal SQL As String, strCon As String, Optional whoCall As String = "") As DataTable
        '    Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        '    whoCall = returnString(whoCall & WhoCalledMe())

        '    If strCon = String.Empty Then
        '        returnPageError(PathFile, "connection string for ORACLE is empty", whoCall)
        '    End If
        '    Dim dt As New DataTable

        '    'strCon = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.50.15)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=topprd)));Persist Security Info=True;User Id=dsdemo;Password=dsdemo;"
        '    Using connection As New OracleConnection(strCon)
        '        connection.Open()
        '        Try
        '            Dim dtAdapter As New OracleDataAdapter(SQL, connection)
        '            dtAdapter.Fill(dt)
        '            Return dt
        '        Catch ex As Exception
        '            returnPageError(PathFile, ex.Message, whoCall, SQL)
        '            Return Nothing
        '        Finally
        '            connection.Close()
        '        End Try
        '    End Using
        'End Function

        'sqlserver
        Public Function QuerySQL(ByVal SQL As String, strCon As String, Optional whoCall As String = "") As DataTable
            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            whoCall = returnString(whoCall & WhoCalledMe())

            If strCon = String.Empty Then
                returnPageError(PathFile, "connection string for SQL SERVER is empty", whoCall)
            End If
            Dim dt As New DataTable

            Using connection As New SqlConnection(strCon)
                connection.Open()
                Try
                    Dim dtAdapter As New SqlDataAdapter(SQL, connection)
                    dtAdapter.Fill(dt)
                    Return dt
                Catch ex As Exception
                    errCont.GetPageError(PathFile, whoCall, SQL, ex.Message)
                    returnPageError(PathFile, ex.Message, whoCall, SQL)
                    Return Nothing
                Finally
                    connection.Close()
                End Try
            End Using
        End Function

        'mysql
        Public Function QueryMySQL(ByVal SQL As String, strCon As String, Optional whoCall As String = "") As DataTable
            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            whoCall = returnString(whoCall & WhoCalledMe())

            If strCon = String.Empty Then
                returnPageError(PathFile, "connection string for MYSQL is empty", whoCall)
            End If
            Dim dt As New DataTable

            Using connection As New MySqlConnection(strCon)
                connection.Open()
                Try
                    Dim dtAdapter As New MySqlDataAdapter(SQL, connection)
                    dtAdapter.Fill(dt)
                    Return dt
                Catch ex As Exception
                    returnPageError(PathFile, ex.Message, whoCall, SQL)
                    Return Nothing
                Finally
                    connection.Close()
                End Try
            End Using
        End Function

        Public Function Query(ByVal strSQL As String, ByVal DBType As String, Optional whoCall As String = "") As DataTable
            whoCall &= WhoCalledMe()
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            With strConn
                If strConn.checkDbSQL(DBType) Then 'sqlserver
                    Return QuerySQL(strSQL, .getStringConnectSQL(DBType), whoCall)
                Else 'my sql
                    Return QueryMySQL(strSQL, .getStringConnectMySQL(), whoCall)
                End If
            End With

        End Function

        Public Function QueryA(ByVal strSQL As String, ByVal DBType As String, Optional seqShow As Integer = 0, Optional whoCall As String = "") As ArrayList
            whoCall &= WhoCalledMe()
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            Dim dt As DataTable = Query(strSQL, DBType, whoCall)
            Dim al As New ArrayList
            For Each dr As DataRow In dt.Rows
                With New DataRowControl(dr)
                    Dim val As String = .Text(seqShow)
                    If Not al.Contains(val) Then
                        al.Add(.Text(seqShow))
                    End If
                End With
            Next
            Return al
        End Function

        Public Function QueryList(ByVal strSQL As String, ByVal DBType As String, Optional seqShow As Integer = 0, Optional whoCall As String = "") As List(Of String)
            whoCall &= WhoCalledMe()
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            Dim dt As DataTable = Query(strSQL, DBType, whoCall)
            Dim al As New List(Of String)
            For Each dr As DataRow In dt.Rows
                With New DataRowControl(dr)
                    al.Add(.Text(seqShow))
                End With
            Next
            Return al
        End Function

        Public Function QueryHash(ByVal strSQL As String, ByVal DBType As String, fldKey As String, fldVal As String, Optional whoCall As String = "") As Hashtable
            whoCall &= WhoCalledMe()
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            If String.IsNullOrEmpty(fldKey) And String.IsNullOrEmpty(fldVal) Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "field Key or Value is empty", whoCall)
            End If
            Dim dt As DataTable = Query(strSQL, DBType, whoCall)
            Dim ht As New Hashtable
            Dim hashcont As New HashtableControl
            For Each dr As DataRow In dt.Rows
                With New DataRowControl(dr)
                    hashcont.addDataHash(ht, .Text(fldKey), .Text(fldVal))
                End With
            Next
            Return ht
        End Function

        Public Function QueryHashDataRow(ByVal strSQL As String, ByVal DBType As String, fldKey As String, Optional whoCall As String = "") As Hashtable
            whoCall &= WhoCalledMe()
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            If String.IsNullOrEmpty(fldKey) Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "field Key is empty", whoCall)
            End If
            Dim dt As DataTable = Query(strSQL, DBType, whoCall)
            Dim ht As New Hashtable
            Dim hashcont As New HashtableControl
            For Each dr As DataRow In dt.Rows
                With New DataRowControl(dr)
                    hashcont.addDataHash(ht, .Text(fldKey), dr)
                End With
            Next
            Return ht
        End Function

        'data row
        'oracle 
        'Public Function QueryDataRowOracle(ByVal SQL As String, strCon As String, Optional whoCall As String = "") As DataRow
        '    Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        '    whoCall = returnString(whoCall & WhoCalledMe())
        '    If strCon = String.Empty Then
        '        returnPageError(PathFile, "connection string for ORACLE is empty", whoCall)
        '    End If
        '    Dim dt As New DataTable

        '    Using connection As New OracleConnection(strCon)
        '        connection.Open()
        '        Try
        '            Dim dtAdapter As New OracleDataAdapter(SQL, connection)
        '            dtAdapter.Fill(dt)
        '            Return If(dt.Rows.Count > 0, dt.Rows(0), Nothing)
        '        Catch ex As Exception
        '            errCont.GetPageError(PathFile, whoCall.Length, SQL, ex.Message)
        '            Return Nothing
        '        Finally
        '            connection.Close()
        '        End Try
        '    End Using
        'End Function

        'sqlserver
        Public Function QueryDataRowSQL(ByVal SQL As String, strCon As String, Optional whoCall As String = "") As DataRow
            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            whoCall = returnString(whoCall & WhoCalledMe())

            If strCon = String.Empty Then
                returnPageError(PathFile, "connection string for SQL SERVER is empty", whoCall)
            End If
            Dim dt As New DataTable

            Using connection As New SqlConnection(strCon)
                connection.Open()
                Try
                    Dim dtAdapter As New SqlDataAdapter(SQL, connection)
                    dtAdapter.Fill(dt)
                    Return If(dt.Rows.Count > 0, dt.Rows(0), Nothing)
                Catch ex As Exception
                    errCont.GetPageError(PathFile, whoCall, SQL, ex.Message)
                    Return Nothing
                Finally
                    connection.Close()
                End Try
            End Using
        End Function

        'mysql
        Public Function QueryDataRowMySQL(ByVal SQL As String, strCon As String, Optional whoCall As String = "") As DataRow
            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            whoCall = returnString(whoCall & WhoCalledMe())

            If strCon = String.Empty Then
                returnPageError(PathFile, "connection string for MYSQL is empty", whoCall)
            End If
            Dim dt As New DataTable

            Using connection As New MySqlConnection(strCon)
                connection.Open()
                Try
                    Dim dtAdapter As New MySqlDataAdapter(SQL, connection)
                    dtAdapter.Fill(dt)
                    Return If(dt.Rows.Count > 0, dt.Rows(0), Nothing)
                Catch ex As Exception
                    errCont.GetPageError(PathFile, whoCall, SQL, ex.Message)
                    Return Nothing
                Finally
                    connection.Close()
                End Try
            End Using
        End Function

        'get data one row data
        Public Function QueryDataRow(ByVal strSQL As String, ByVal DBType As String, Optional whoCall As String = "") As DataRow
            whoCall &= WhoCalledMe()
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            With strConn
                If .checkDbOracle(DBType) Then 'oracle
                    'Return QueryDataRowOracle(strSQL, .getStringConnectOracle(DBType), whoCall)
                ElseIf strConn.checkDbSQL(DBType) Then 'sqlserver
                    Return QueryDataRowSQL(strSQL, .getStringConnectSQL(DBType), whoCall)
                Else 'my sql
                    Return QueryDataRowMySQL(strSQL, .getStringConnectMySQL(), whoCall)
                End If
            End With
        End Function


        'data set 
        'oracle 
        'Public Function QueryDataSetOracle(ByVal SQLList As ArrayList, strCon As String, Optional whoCall As String = "") As DataSet
        '    Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        '    whoCall = returnString(whoCall & WhoCalledMe())

        '    If strCon = String.Empty Then
        '        returnPageError(PathFile, "connection string for ORACLE is empty", whoCall)
        '    End If
        '    Dim dt As New DataSet
        '    Dim dtAdapter As New OracleDataAdapter
        '    Dim command As OracleCommand

        '    Using connection As New OracleConnection(strCon)
        '        connection.Open()
        '        Try
        '            Dim line As Integer = 1
        '            For Each SQL As String In SQLList
        '                If line = 1 Then
        '                    command = New OracleCommand(SQL, connection)
        '                    dtAdapter.SelectCommand = command
        '                Else
        '                    dtAdapter.SelectCommand.CommandText = SQL
        '                End If
        '                dtAdapter.Fill(dt)
        '                line += 1
        '            Next
        '            dtAdapter.Fill(dt)
        '            Return dt
        '        Catch ex As Exception
        '            errCont.GetPageError(PathFile, whoCall, getAllSQL(SQLList), ex.Message)
        '            Return Nothing
        '        Finally
        '            dtAdapter.Dispose()
        '            command.Dispose()
        '            connection.Close()
        '        End Try
        '    End Using
        'End Function

        'sqlserver
        Public Function QueryDataSetSQL(ByVal SQLList As ArrayList, strCon As String, Optional whoCall As String = "") As DataSet
            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            whoCall = returnString(whoCall & WhoCalledMe())

            If strCon = String.Empty Then
                returnPageError(PathFile, "connection string for SQL SERVER is empty", whoCall)
            End If

            Dim dt As New DataSet
            Dim dtAdapter As New SqlDataAdapter
            Dim command As SqlCommand

            Using connection As New SqlConnection(strCon)
                connection.Open()
                Try
                    Dim line As Integer = 1
                    For Each SQL As String In SQLList
                        If line = 1 Then
                            command = New SqlCommand(SQL, connection)
                            dtAdapter.SelectCommand = command
                        Else
                            dtAdapter.SelectCommand.CommandText = SQL
                        End If
                        dtAdapter.Fill(dt)
                        line += 1
                    Next
                    dtAdapter.Fill(dt)
                    Return dt
                Catch ex As Exception
                    errCont.GetPageError(PathFile, whoCall, getAllSQL(SQLList), ex.Message)
                    Return Nothing
                Finally
                    dtAdapter.Dispose()
                    command.Dispose()
                    connection.Close()
                End Try
            End Using
        End Function

        'mysql
        Public Function QueryDataSetMySQL(ByVal SQLList As ArrayList, strCon As String, Optional whoCall As String = "") As DataSet
            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            whoCall = returnString(whoCall & WhoCalledMe())

            If strCon = String.Empty Then
                returnPageError(PathFile, "connection string for MYSQL is empty", whoCall)
            End If

            Dim dt As New DataSet
            Dim dtAdapter As New MySqlDataAdapter
            Dim command As MySqlCommand

            Using connection As New MySqlConnection(strCon)
                connection.Open()
                Try
                    Dim line As Integer = 1
                    For Each SQL As String In SQLList
                        If line = 1 Then
                            command = New MySqlCommand(SQL, connection)
                            dtAdapter.SelectCommand = command
                        Else
                            dtAdapter.SelectCommand.CommandText = SQL
                        End If
                        dtAdapter.Fill(dt)
                        line += 1
                    Next
                    Return dt
                Catch ex As Exception
                    errCont.GetPageError(PathFile, whoCall, getAllSQL(SQLList), ex.Message)
                    Return Nothing
                Finally
                    dtAdapter.Dispose()
                    command.Dispose()
                    connection.Close()
                End Try
            End Using
        End Function

        Function getAllSQL(sqlList As ArrayList) As String
            Dim strSQL As String = ""
            For Each SQL As String In sqlList
                strSQL &= SQL & ";"
            Next
            Return strSQL
        End Function

        Public Function QueryDataSet(ByVal strSQLList As ArrayList, ByVal DBType As String, Optional whoCall As String = "") As DataSet
            whoCall &= WhoCalledMe()
            If DBType = "" Then
                returnPageError(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, "connection string is empty", whoCall)
            End If
            With strConn
                If .checkDbOracle(DBType) Then 'oracle
                    'Return QueryDataSetOracle(strSQLList, .getStringConnectOracle(DBType), whoCall)
                ElseIf strConn.checkDbSQL(DBType) Then 'sqlserver
                    Return QueryDataSetSQL(strSQLList, .getStringConnectSQL(DBType), whoCall)
                Else 'my sql
                    Return QueryDataSetMySQL(strSQLList, .getStringConnectMySQL(), whoCall)
                End If
            End With
        End Function





    End Class
End Namespace