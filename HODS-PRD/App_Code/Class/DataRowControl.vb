Imports System.Data
'Imports MIS_T100.DataControl
'meta data
'Name Class : DataRowControl
'objective : manage datatable row object
'create by : WANLOP THONGCOMDEE
'Create Date : 2019/07/18
Namespace DataControl
    Public Class DataRowControl
        Private drReciece As DataRow = Nothing
        Private StringWhenValueIsEmpty As String = String.Empty
        Private NumberWhenValueIsEmpty As Decimal = Decimal.Zero

        Sub New(ByRef PI_DATAROW As DataRow, Optional StringIsNull As String = "", Optional NuberIsNull As Decimal = 0)
            DR = PI_DATAROW
            SrtingNullorEmpty = StringIsNull
            NumberNullorZero = NuberIsNull
        End Sub

        Sub New(strSQL As String, DBType As String, Optional whoCall As String = "",
                Optional StringIsNull As String = "", Optional NuberIsNull As Decimal = 0)
            Dim dbconn As New DataConnectControl
            DR = dbconn.QueryDataRow(strSQL, DBType, whoCall & dbconn.WhoCalledMe)
            SrtingNullorEmpty = StringIsNull
            NumberNullorZero = NuberIsNull
        End Sub

        Private Property DR As DataRow
            Get
                Return drReciece
            End Get
            Set(value As DataRow)
                drReciece = value
            End Set
        End Property

        Private Property SrtingNullorEmpty As String
            Get
                Return StringWhenValueIsEmpty
            End Get
            Set(value As String)
                StringWhenValueIsEmpty = value
            End Set
        End Property

        Private Property NumberNullorZero As Decimal
            Get
                Return NumberWhenValueIsEmpty
            End Get
            Set(value As Decimal)
                NumberWhenValueIsEmpty = value
            End Set
        End Property


        Function Text(fldName As String) As String
            If DR Is Nothing Then
                Return SrtingNullorEmpty
            End If
            Try
                Return If(IsDBNull(DR(fldName)), SrtingNullorEmpty, Trim(DR(fldName)))
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Function Text(fldName As String, whenValueIsEmpty As String) As String
            If DR Is Nothing Then
                Return whenValueIsEmpty
            End If
            Try
                Return If(IsDBNull(DR(fldName)), whenValueIsEmpty, Trim(DR(fldName)))
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Function Text(fldName As String, fldNameElse As String, whenValueIsEmpty As String) As String
            If DR Is Nothing Then
                Return whenValueIsEmpty
            End If
            Try
                Return If(IsDBNull(DR(fldName)), Text(fldNameElse, whenValueIsEmpty), Trim(DR(fldName)))
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Function Text(seq As Integer) As String
            If DR Is Nothing Then
                Return SrtingNullorEmpty
            End If
            Try
                Return If(IsDBNull(DR(seq)), SrtingNullorEmpty, Trim(DR(seq)))
            Catch ex As Exception
                Return SrtingNullorEmpty
            End Try
        End Function

        Function Text(seq As Integer, whenValueIsEmpty As String) As String
            If DR Is Nothing Then
                Return whenValueIsEmpty
            End If
            Try
                Return If(IsDBNull(DR(seq)), whenValueIsEmpty, Trim(DR(seq)))
            Catch ex As Exception
                Return SrtingNullorEmpty
            End Try
        End Function

        Function Number(fldName As String) As Decimal
            If DR Is Nothing Then
                Return NumberNullorZero
            End If
            Try
                Dim OutputControl As New OutputControl
                Dim getVal As String = If(IsDBNull(DR(fldName)), NumberNullorZero, Trim(DR(fldName)))
                Return OutputControl.checkNumberic(getVal)
            Catch ex As Exception
                Return NumberNullorZero
            End Try
        End Function

        Function Number(fldName As String, whenValueIsEmpty As Decimal) As Decimal
            If DR Is Nothing Then
                Return whenValueIsEmpty
            End If
            Try
                Dim OutputControl As New OutputControl
                Dim getVal As String = If(IsDBNull(DR(fldName)), whenValueIsEmpty, Trim(DR(fldName)))
                Return OutputControl.checkNumberic(getVal)
            Catch ex As Exception
                Return whenValueIsEmpty
            End Try
        End Function

        Function Number(fldName As String, fldNameElse As String, whenValueIsEmpty As Decimal) As Decimal
            If DR Is Nothing Then
                Return whenValueIsEmpty
            End If
            Try
                Dim OutputControl As New OutputControl
                Dim getVal As String = If(IsDBNull(DR(fldName)), Number(fldNameElse, whenValueIsEmpty), Trim(DR(fldName)))
                Return OutputControl.checkNumberic(getVal)
            Catch ex As Exception
                Return whenValueIsEmpty
            End Try
        End Function

        Function Number(seq As Integer) As Decimal
            If DR Is Nothing Or seq < 0 Then
                Return NumberNullorZero
            End If
            Try
                Dim OutputControl As New OutputControl
                Dim getVal As String = If(IsDBNull(DR(seq)), NumberNullorZero, Trim(DR(seq)))
                Return OutputControl.checkNumberic(getVal)
            Catch ex As Exception
                Return NumberNullorZero
            End Try
        End Function

        Function Number(seq As Integer, whenValueIsEmpty As Decimal) As Decimal
            If DR Is Nothing Or seq < 0 Then
                Return whenValueIsEmpty
            End If
            Try
                Dim OutputControl As New OutputControl
                Dim getVal As String = If(IsDBNull(DR(seq)), whenValueIsEmpty, Trim(DR(seq)))
                Return OutputControl.checkNumberic(getVal)
            Catch ex As Exception
                Return whenValueIsEmpty
            End Try
        End Function
        'add datarow to datatable (hashtable)
        Sub AddFromHash(ByRef dt As DataTable, dataHash As Hashtable)
            Try
                Dim dr As DataRow
                dr = dt.NewRow()
                For Each fName As String In dataHash.Keys
                    dr(fName) = dataHash.Item(fName)
                Next
                dt.Rows.Add(dr)
            Catch ex As Exception
                Dim aa As String = ex.Message
                Dim aad As String = ""
            End Try
        End Sub
        'add datarow to datatable
        Sub Add(ByRef dt As DataTable, fldList As ArrayList, Optional strSplit As String = VarIni.char8)
            'fldList Format FieldCOde:Decimal Checker(1 or 0/empty):Value Manual
            Dim dataHash As New Hashtable
            Dim OutputControl As New OutputControl
            Dim hashCont As New HashtableControl
            Try
                For Each str As String In fldList
                    If str IsNot String.Empty Then
                        Dim temp() As String = str.Split(strSplit)
                        Dim countTemp As Integer = temp.Length
                        Dim isDecimal As Boolean = False
                        Dim key As String = temp(0)
                        If countTemp = 3 Then
                            dataHash.Add(key, temp(2))
                        Else
                            Dim showDec As Boolean = False
                            If countTemp = 2 Then
                                showDec = True
                            End If
                            With New DataRowControl(DR)
                                If showDec Then 'for decimal
                                    hashCont.addDataHash(dataHash, key, .Number(key))
                                Else 'for string
                                    hashCont.addDataHash(dataHash, key, .Text(key))
                                End If
                            End With

                        End If
                    End If
                Next
                If dataHash.Count > Decimal.Zero Then
                    AddFromHash(dt, dataHash)
                End If
            Catch ex As Exception
                Dim aa As String = ex.Message
                Dim aad As String = ""
            End Try
        End Sub

        Sub AddDatarow(ByRef dt As DataTable,
                       Optional fldNumber As ArrayList = Nothing,
                       Optional fldList As ArrayList = Nothing,
                       Optional strSplit As String = VarIni.C8)
            'fldList Format FieldCode:Value
            Dim dataHash As New Hashtable
            Dim OutCont As New OutputControl
            Dim hashCont As New HashtableControl
            Try
                For Each col As DataColumn In dt.Columns
                    Dim val As String = ""
                    Dim colName As String = col.ColumnName
                    With New DataRowControl(DR)
                        If col.DataType = Type.GetType("System.Double") Then
                            val = .Number(colName)
                        Else
                            val = .Text(colName)
                        End If
                        hashCont.addDataHash(dataHash, colName, val)
                    End With
                Next
                'option to manual
                If fldList IsNot Nothing Then
                    For Each str As String In fldList
                        If str IsNot String.Empty Then
                            '0=column name
                            '1=value
                            Dim temp() As String = str.Split(strSplit)
                            If temp.Length > 1 Then
                                Dim colName As String = temp(0)
                                Dim val As String = temp(1)
                                If fldNumber.Contains(colName) Then
                                    val = OutCont.checkNumberic(val)
                                End If
                                hashCont.UpdateDataHash(dataHash, colName, val)
                            End If
                        End If
                    Next
                End If
                If dataHash.Count > Decimal.Zero Then
                    AddFromHash(dt, dataHash)
                End If
            Catch ex As Exception
                Dim aa As String = ex.Message
                Dim aad As String = ""
            End Try
        End Sub

        Sub AddDatarow(ByRef dt As DataTable,
                       Optional fldNumber As List(Of String) = Nothing,
                       Optional fldList As List(Of String) = Nothing,
                       Optional strSplit As String = VarIni.C8)
            'fldList Format FieldCode:Value
            Dim dataHash As New Hashtable
            Dim OutCont As New OutputControl
            Dim hashCont As New HashtableControl
            Try
                For Each col As DataColumn In dt.Columns
                    Dim val As String = ""
                    Dim colName As String = col.ColumnName
                    With New DataRowControl(DR)
                        If col.DataType = Type.GetType("System.Double") Then
                            val = .Number(colName)
                        Else
                            val = .Text(colName, "")
                        End If
                        hashCont.addDataHash(dataHash, colName, val)
                    End With
                Next
                'option to manual
                If fldList IsNot Nothing Then
                    For Each str As String In fldList
                        If str IsNot String.Empty Then
                            '0=column name
                            '1=value
                            Dim temp() As String = str.Split(strSplit)
                            If temp.Length > 1 Then
                                Dim colName As String = temp(0)
                                Dim val As String = temp(1)
                                If fldNumber.Contains(colName) Then
                                    val = OutCont.checkNumberic(val)
                                End If
                                hashCont.UpdateDataHash(dataHash, colName, val)
                            End If
                        End If
                    Next
                End If
                If dataHash.Count > Decimal.Zero Then
                    AddFromHash(dt, dataHash)
                End If
            Catch ex As Exception
                Dim aa As String = ex.Message
                Dim aad As String = ""
            End Try
        End Sub

        Sub AddManual(ByRef dt As DataTable, fldList As ArrayList, Optional strSplit As String = VarIni.char8)
            'fldList Format FieldCOde:Decimal Checker(1 or 0/empty):Value Manual
            Dim dataHash As New Hashtable
            'Dim OutputControl As New OutputControl
            Dim hashCont As New HashtableControl
            Try
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
                        If hashCont.existDataHash(dataHash, key) And (Not String.IsNullOrEmpty(val) Or val <> "0") Then
                            hashCont.UpdateDataHash(dataHash, key, val)
                        End If
                    End If
                Next
                If dataHash.Count > Decimal.Zero Then
                    AddFromHash(dt, dataHash)
                End If
            Catch ex As Exception
                Dim aa As String = ex.Message
                Dim aad As String = ""
            End Try
        End Sub
        'add datarow to datatable

    End Class
End Namespace