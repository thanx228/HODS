Imports System.Data

Public Class CMSME
    'system control->Commond Data->Staff
    '## TABLE CMSME
    '## ME001 EMDEPARTMENT CODE
    '## ME002 EMDEPARTMENT NAME

    Public Const table As String = "CMSME"
    Dim conn_sql As New ConnSQL

    Public Function getData(whr As String, Optional fld As String = "ME002", Optional orderBy As String = "ME001") As DataTable
        If fld.Trim = "" Then
            fld = "*"
        End If
        Dim SQL As String = "select " & fld & " from " & table & " where len(ME001)=3 " & whr
        If orderBy <> "" Then
            SQL &= " order by " & orderBy
        End If
        Return conn_sql.Get_DataReader(SQL, conn_sql.ERP_ConnectionString)
    End Function

    Public Function getValName(deptCode As String) As String
        Dim whr As String = conn_sql.Where("ME001", deptCode, , False)
        Dim dt As DataTable = getData(whr)
        Dim valReturn As String = ""
        If dt.Rows.Count > 0 Then
            valReturn = dt.Rows(0).Item(0).ToString.Trim
        End If

        Return valReturn
    End Function

End Class