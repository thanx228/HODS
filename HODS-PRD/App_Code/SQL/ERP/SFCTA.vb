Imports System.Data

Public Class SFCTA
    'Manufacturing->shop floor control->MO Operation
    '## TABLE SFCTA

    Public Const table As String = "SFCTA"




    Dim conn_sql As New ConnSQL

    Public Function getData(whr As String, Optional fld As String = "", Optional orderBy As String = "TA001,TA002,TA003") As DataTable
        If fld.Trim = "" Then
            fld = "*"
        End If
        Dim SQL As String = "select " & fld & " from " & table & " where 1=1 " & whr
        If orderBy <> "" Then
            SQL &= " order by " & orderBy
        End If
        Return conn_sql.Get_DataReader(SQL, conn_sql.ERP_ConnectionString)
    End Function

    Public Function getOperationCode(moType As String, moNumber As String, moSeq As String) As String
        Dim whr As String = conn_sql.Where("TA001", moType, , False)
        whr &= conn_sql.Where("TA002", moNumber, , False)
        whr &= conn_sql.Where("TA003", moSeq, , False)
        Dim dt As DataTable = getData(whr, "TA004")
        Dim valReturn As String = ""
        If dt.Rows.Count > 0 Then
            valReturn = dt.Rows(0).Item(0).ToString.Trim
        End If
        Return valReturn
    End Function

End Class