Imports MIS_HTI.DataControl
Imports System.Data

Public Class PURTB
    Public Const TABLE As String = "PURTB"
    Public Const DOC_TYPE As String = "TB001"
    Public Const DOC_NO As String = "TB002"
    Public Const DOC_SEQ As String = "TB003"
    Public Const ITEM As String = "TB004"
    Public Const ITEM_DESC As String = "TB005"
    Public Const ITEM_SPEC As String = "TB006"
    Public Const WAREHOUSE As String = "TB008"
    Public Const QTY As String = "TB009"
    Public Const STOCK_BAL As String = "UDF054"
    Public Const PR_SUM_LAST_MONTH As String = "UDF056"


    Shared Function getSQL(fldName As ArrayList, whr As String,
                           Optional ordBy As String = "", Optional grpBy As String = "",
                           Optional fetchFirst As Boolean = False) As String


        Return ""
    End Function

    Shared Function getDataTable() As DataTable

    End Function

    Shared Function getDataRow() As DataRow


    End Function

End Class
