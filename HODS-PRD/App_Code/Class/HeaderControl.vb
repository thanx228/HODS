Imports MIS_HTI.DataControl
Imports System.Data

Namespace FormControl
    Public Class HeaderControl

        Dim dbConn As New DataConnectControl
        Dim dtCont As New DataTableControl

        Private Function MenuDatarow(whr As String) As DataRow
            Dim fldName As New ArrayList
            fldName.Add(MENU_T.PARENT_ID)
            fldName.Add(MENU_T.NAME)
            Dim strSQL As New SQLString(MENU_T.TABLE, fldName, False)
            strSQL.SetWhere(whr, True)
            Return strSQL.QueryDataRow(strSQL.GetSQLString, VarIni.DBMIS, strSQL.WhoCalledMe)
        End Function

        Public Function nameHeader(ByVal progName As String) As String
            Dim strHeader As String = ""
            Dim whr As String = dbConn.WHERE_EQUAL(MENU_T.PROG, progName.Substring(1, progName.Length - 1))
            Dim dr As DataRow = MenuDatarow(whr)
            If dr IsNot Nothing Then
                strHeader = getName(dtCont.IsDBNullDataRowDecimal(dr, MENU_T.PARENT_ID), dtCont.IsDBNullDataRow(dr, MENU_T.NAME))
            End If
            Return strHeader
        End Function

        Private Function getName(ByVal pID As Integer, ByVal str As String) As String

            Dim fldName As New ArrayList
            fldName.Add(MENU_T.PARENT_ID)
            fldName.Add(MENU_T.NAME)
            Dim strSQL As New SQLString(MENU_T.TABLE, fldName, False)
            strSQL.SetWhere(strSQL.WHERE_EQUAL(MENU_T.ID, pID), True)
            Dim whr As String = strSQL.WHERE_EQUAL(MENU_T.ID, pID)
            Dim dr As DataRow = MenuDatarow(whr)
            If dr IsNot Nothing Then
                str = getName(dtCont.IsDBNullDataRowDecimal(dr, MENU_T.PARENT_ID), dtCont.IsDBNullDataRow(dr, MENU_T.NAME) & " -> " & str)
            End If
            Return str
        End Function

    End Class
End Namespace