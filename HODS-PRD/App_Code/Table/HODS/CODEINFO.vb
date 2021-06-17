Imports MIS_HTI.DataControl
Imports System.Data

Public Class CODEINFO
    Public Const TABLENAME As String = "CodeInfo"
    Public Const CodeType As String = "CodeType"
    Public Const Code As String = "Code"
    Public Const Name As String = "Name"
    Public Const WC As String = "WC"
    Shared ReadOnly dbConn As New DataConnectControl

    Shared Function SQL(docType As String,
                        Optional whrAdd As String = "",
                        Optional nameSelect As String = "rtrim(" & Code & ") +' '+rtrim(" & Name & ")",
                        Optional ordBy As String = Code) As String
        Dim strSQL As New SQLString(TABLENAME)
        Dim fldName As New ArrayList From {
            "rtrim(" & Code & ") " & Code,
            nameSelect & " " & Name
        }
        strSQL.SetSelect(fldName)
        Dim whr As String = strSQL.WHERE_EQUAL(CodeType, docType)
        If Not String.IsNullOrEmpty(whrAdd) Then
            whr &= whrAdd
        End If
        strSQL.SetWhere(whr, True)
        strSQL.SetOrderBy(ordBy)
        Return strSQL.GetSQLString
    End Function

    Shared Function GetDatatable(docType As String, Optional code As String = "") As DataTable
        Return dbConn.Query(SQL(docType, code), VarIni.DBMIS, dbConn.WhoCalledMe)
    End Function

    Shared Function GetDataRow(docType As String, Optional code As String = "") As DataRow
        Return dbConn.QueryDataRow(SQL(docType, code), VarIni.DBMIS, dbConn.WhoCalledMe)
    End Function

End Class
