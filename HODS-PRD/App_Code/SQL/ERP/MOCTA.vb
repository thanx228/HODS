Imports System.Data

Public Class MOCTA
    'Manufacturing->shop floor control->MO Operation
    '## TABLE MOCTA

    Public Const table As String = "MOCTA"
    Dim conn_sql As New ConnSQL

    Public Function getData(whr As String, Optional fld As String = "", Optional orderBy As String = "TA001,TA002,TA003", Optional mostatus As String = "1,2,3") As DataTable
        If fld.Trim = "" Then
            fld = "*"
        End If
        If mostatus.Trim <> "" Then
            whr &= "and TA011 in ('" & mostatus.Replace(",", "','") & "')"
        End If
        Dim SQL As String = "select " & fld & " from " & table & " where 1=1 " & whr
        If orderBy <> "" Then
            SQL &= " order by " & orderBy
        End If
        Return conn_sql.Get_DataReader(SQL, conn_sql.ERP_ConnectionString)
    End Function

End Class