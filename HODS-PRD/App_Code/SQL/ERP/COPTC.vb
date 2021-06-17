Imports System.Data

Public Class COPTC
    'Manufacturing->shop floor control->MO Operation
    '## TABLE COPTC

    Public Const table As String = "COPTC"
    Dim conn_sql As New ConnSQL

    Public Function getData(whr As String, Optional fld As String = "", Optional orderBy As String = "TC001,TC002") As DataTable
        If fld.Trim = "" Then
            fld = "*"
        End If
        Dim SQL As String = "select " & fld & " from " & table & " where 1=1 " & whr
        If orderBy <> "" Then
            SQL &= " order by " & orderBy
        End If
        Return conn_sql.Get_DataReader(SQL, conn_sql.ERP_ConnectionString)
    End Function

    Public Function getCustCode(soType As String, soNumber As String) As String
        If soType = "" And soNumber = "" Then
            Return ""
        End If

        Dim whr As String = ""
        whr = conn_sql.Where("TC001", soType, , False)
        whr &= conn_sql.Where("TC002", soNumber, , False)

        Dim dt As DataTable = getData(whr, "TC004")
        Dim valReturn As String = ""
        If dt.Rows.Count > 0 Then
            valReturn = dt.Rows(0).Item(0).ToString.Trim
        End If
        Return valReturn
    End Function


End Class