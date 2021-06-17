Imports System.Data

Public Class CMSMW
    'system control->Commond Data->Operaion
    '## TABLE CMSMW
    '## ME001 OPERATION CODE
    '## ME002 OPERATION NAME

    Public Const table As String = "CMSMW"
    Dim conn_sql As New ConnSQL

    Public Function getData(whr As String, Optional fld As String = "MW002", Optional orderBy As String = "MW001") As DataTable
        If fld.Trim = "" Then
            fld = "*"
        End If
        Dim SQL As String = "select " & fld & " from " & table & " where 1=1 " & whr
        If orderBy <> "" Then
            SQL &= " order by " & orderBy
        End If
        Return conn_sql.Get_DataReader(SQL, conn_sql.ERP_ConnectionString)
    End Function

    Public Function getValName(code As String) As String
        Dim whr As String = conn_sql.Where("MW001", code, , False)
        Dim dt As DataTable = getData(whr)
        Dim valReturn As String = ""
        If dt.Rows.Count > 0 Then
            valReturn = dt.Rows(0).Item(0).ToString.Trim
        End If
        Return valReturn
    End Function

End Class