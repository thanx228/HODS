Imports System.Data

Namespace DataControl

    Public Class HashtableControl

        Public Function existDataHash(dataHash As Hashtable, valForCheck As String, Optional checkKey As Boolean = True) As Boolean
            Dim existHash As Boolean = False
            Try
                If dataHash IsNot Nothing Then
                    If checkKey Then
                        If dataHash.ContainsKey(valForCheck) Then
                            existHash = True
                        End If
                    Else
                        If dataHash.ContainsValue(valForCheck) Then
                            existHash = True
                        End If
                    End If
                End If
                Return existHash
            Catch ex As Exception
                Return existHash
            End Try
        End Function

        Public Function getHash(dataHash As Hashtable, KeyCheck As String, Optional useKey As Boolean = True) As String
            Dim valHash As String = String.Empty
            Try
                If useKey Then 'find by key
                    If dataHash.ContainsKey(KeyCheck) Then
                        valHash = dataHash(KeyCheck)
                    End If
                Else 'find by value
                    If dataHash.ContainsValue(KeyCheck) Then
                        For Each de As DictionaryEntry In dataHash
                            If de.Value = KeyCheck Then
                                valHash = de.Key
                                Exit For
                            End If
                        Next
                    End If
                End If
                Return valHash
            Catch ex As Exception
                Return valHash
            End Try
        End Function

        Public Function getDataHashDecimal(dataHash As Hashtable, KeyCheck As String) As Decimal
            Dim valtHash As Decimal = Decimal.Zero
            Try
                If dataHash.ContainsKey(KeyCheck) Then
                    valtHash = CDec(dataHash(KeyCheck))
                End If
                Return valtHash
            Catch ex As Exception
                Return valtHash
            End Try
        End Function

        Public Function getDataHashDatarow(dataHash As Hashtable, KeyCheck As String) As DataRow
            Dim valtHash As DataRow
            Try
                If dataHash.ContainsKey(KeyCheck) Then
                    valtHash = CType(dataHash(KeyCheck), DataRow)
                    Return valtHash
                End If
                Return Nothing
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        'add by noi on 2020-11-19
        Public Function getHashDatatable(dataHash As Hashtable, KeyCheck As String) As DataTable
            Dim valtHash As DataTable
            Try
                If dataHash.ContainsKey(KeyCheck) Then
                    valtHash = CType(dataHash(KeyCheck), DataTable)
                    Return valtHash
                End If
                Return Nothing
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        'add by noi on 2020-11-19
        Public Sub GetHash(Hash As Hashtable, key As String, ByRef valReturn As Object)
            Try
                Select Case True
                    Case TypeOf valReturn Is String
                        valReturn = getHash(Hash, key, True)
                    Case TypeOf valReturn Is Decimal
                        valReturn = getDataHashDecimal(Hash, key)
                    Case TypeOf valReturn Is DataRow
                        valReturn = getDataHashDatarow(Hash, key)
                    Case TypeOf valReturn Is DataTable
                        valReturn = getHashDatatable(Hash, key)
                    Case Else
                        valReturn = Nothing
                End Select
            Catch ex As Exception
                Select Case True
                    Case TypeOf valReturn Is String
                        valReturn = ""
                    Case TypeOf valReturn Is Decimal
                        valReturn = 0
                    Case TypeOf valReturn Is DataRow
                        valReturn = Nothing
                    Case TypeOf valReturn Is DataTable
                        valReturn = Nothing
                    Case Else
                        valReturn = Nothing
                End Select
            End Try
        End Sub

        'add 2017-07-19 by noi
        Public Sub addDataHash(ByRef dataHash As Hashtable, key As String, value As String)
            If Not existDataHash(dataHash, key) Then
                dataHash.Add(key, value)
            End If
        End Sub
        'add 2017-12-15 by noi
        Public Sub addDataHash(ByRef dataHash As Hashtable, key As String, value As DataRow)
            If Not existDataHash(dataHash, key) Then
                dataHash.Add(key, value)
            End If
        End Sub

        'add 2020-11-19 by noi
        Public Sub addDataHash(ByRef dataHash As Hashtable, key As String, value As DataTable)
            If Not existDataHash(dataHash, key) Then
                dataHash.Add(key, value)
            End If
        End Sub

        Public Sub AddHash(ByRef dataHash As Hashtable, key As String, value As Object)
            Try
                If Not existDataHash(dataHash, key) Then
                    Select Case True
                        Case TypeOf value Is String
                            dataHash.Add(key, CType(value, String))
                        Case TypeOf value Is Decimal
                            dataHash.Add(key, CType(value, Decimal))
                        Case TypeOf value Is Integer
                            dataHash.Add(key, CType(value, Integer))
                        Case TypeOf value Is DataRow
                            dataHash.Add(key, CType(value, DataRow))
                        Case TypeOf value Is DataTable
                            dataHash.Add(key, CType(value, DataTable))
                    End Select
                End If
            Catch ex As Exception

            End Try

        End Sub


        'add 2017-07-23 by noi update 2017-07-25
        Public Sub UpdateDataHash(ByRef dataHash As Hashtable, key As String, value As String, Optional addHash As Boolean = False)
            If existDataHash(dataHash, key) Then
                dataHash.Item(key) = value
            Else
                If addHash Then
                    addDataHash(dataHash, key, value)
                End If
            End If
        End Sub

        'add 2017-07-25 by noi
        Public Sub UpdateDataHash(ByRef dataHash As Hashtable, key As String, value As Integer, Optional addHash As Boolean = False)
            If existDataHash(dataHash, key) Then
                dataHash.Item(key) = value
            Else
                If addHash Then
                    addDataHash(dataHash, key, value)
                End If
            End If
        End Sub

        'add 2017-07-25 by noi
        Public Sub UpdateDataHash(ByRef dataHash As Hashtable, key As String, value As Decimal, Optional addHash As Boolean = False)
            If existDataHash(dataHash, key) Then
                dataHash.Item(key) = value
            Else
                If addHash Then
                    addDataHash(dataHash, key, value)
                End If
            End If
        End Sub

        'add 2017-011-21 by noi
        Public Sub CountItemDataHash(ByRef dataHash As Hashtable, key As String, Optional increaseVal As Decimal = 1)
            If existDataHash(dataHash, key) Then
                dataHash.Item(key) += increaseVal
            Else
                addDataHash(dataHash, key, increaseVal)
            End If
        End Sub

    End Class

End Namespace
