Imports System.Data

Public Class CMSMV
    'system control->Commond Data->Staff
    '## TABLE CMSMV

    Public Const table As String = "CMSMV"
    Dim conn_sql As New ConnSQL

    Public Function getData(whr As String, Optional fld As String = "MV001,MV004,MV047", Optional orderBy As String = "MV001") As DataTable
        If fld.Trim = "" Then
            fld = "*"
        End If
        Dim SQL As String = "select " & fld & " from " & table & " where ((CMSMV.UDF03 like '1%') or (CMSMV.UDF03 like '3%')) " & whr
        If orderBy <> "" Then
            SQL &= " order by " & orderBy
        End If
        Return conn_sql.Get_DataReader(SQL, conn_sql.ERP_ConnectionString)
    End Function

    Public Function getSum(whr As String) As Integer
        Dim SQL As String = "select count(*) cnt from " & table & " where (CMSMV.UDF03 like '1%' or CMSMV.UDF03 like '3%') " & whr
        Dim dt As DataTable = conn_sql.Get_DataReader(SQL, conn_sql.ERP_ConnectionString)
        Return dt.Rows(0).Item(0)
    End Function

End Class