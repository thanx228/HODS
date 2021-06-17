Imports System.Data
Imports MIS_HTI.DataControl
Public Class CMSMD
    'system control->Commond Data->Staff
    '## TABLE CMSMD
    '## MD001 WORK CENTER CODE
    '## MD002 WORK CENTER NAME

    Public Const table As String = "CMSMD"
    Public Const MD001 As String = "MD001"
    Public Const MD002 As String = "MD002"


    'Dim conn_sql As New ConnSQL
    Dim dbConn As New DataConnectControl


    Public Function getData(whr As String, Optional fld As String = "MD002", Optional orderBy As String = "MD001") As DataTable
        If fld.Trim = "" Then
            fld = "*"
        End If
        Dim SQL As String = "select " & fld & " from " & table & " where isnull(UDF07,'')<>'No'  " & whr
        If orderBy <> "" Then
            SQL &= " order by " & orderBy
        End If
        Return dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
    End Function

    Public Function getName(code As String) As String
        Dim whr As String = dbConn.Where("MD001", code, , False)
        Dim dt As DataTable = getData(whr)
        Dim valReturn As String = ""
        If dt.Rows.Count > 0 Then
            valReturn = dt.Rows(0).Item(0).ToString.Trim
        End If
        Return valReturn
    End Function

End Class