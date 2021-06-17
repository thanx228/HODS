Imports MIS_HTI.DataControl
Namespace UserControl
    Public Class UserAuthorization

        Dim dbConn As New DataConnectControl
        Dim dtControl As New DataTableControl

        'Function WorkCenter(userId As String, Optional replaceForIn As Boolean = False) As String
        '    If userId = "" Then
        '        Return String.Empty
        '    End If
        '    Dim dbConn As New DataConnectControl
        '    Dim dtCont As New DataTableControl
        '    Dim fldName As New ArrayList
        '    fldName.Add(USERAUTHORITY.AUTHORIRYVALUE)
        '    Dim whrSub As String = dbConn.Where(USERAUTHORITY.USERCODE, userId,, False)
        '    whrSub &= dbConn.Where(USERAUTHORITY.AUTHORITYTYPE, "W",, False)
        '    Dim sql As String = USERAUTHORITY.SQL(fldName, whrSub, "")
        '    Dim dr As DataRow = dbConn.QueryDataRow(sql, VarIni.JODS, dbConn.WhoCalledMe)
        '    Dim wc As String = dtCont.IsDBNullDataRow(dr, USERAUTHORITY.AUTHORIRYVALUE)
        '    If replaceForIn Then
        '        wc = " in ('" & wc.Replace(",", "','") & "') "
        '    End If
        '    Return wc
        'End Function

        'checktype vale =(W=work center,D=Department)
        Function WorkCenter(userId As String, wc As String, Optional checkType As String = "W") As Boolean
            'If userId = "" Then
            '    Return String.Empty
            'End If
            'Dim dbConn As New DataConnectControl
            'Dim dtCont As New DataTableControl
            'Dim fldName As New ArrayList
            'fldName.Add(USERAUTHORITY.AUTHORIRYVALUE)
            'Dim whrSub As String = dbConn.Where(USERAUTHORITY.USERCODE, userId,, False)
            'whrSub &= dbConn.Where(USERAUTHORITY.AUTHORITYTYPE, checkType,, False)
            'whrSub &= dbConn.Where(USERAUTHORITY.AUTHORIRYVALUE, " LIKE '%" & wc.Trim & "%' ")

            'Dim sql As String = USERAUTHORITY.SQL(fldName, whrSub, "")
            'Dim dr As DataRow = dbConn.QueryDataRow(sql, VarIni.JODS, dbConn.WhoCalledMe)
            'Dim valReturn As Boolean = False

            'If dr IsNot Nothing Then
            '    valReturn = True
            'End If
            'Return valReturn
        End Function

        'add 2017-12-24
        'noi change name form workCenter to getAuthorize
        Function getAuthorize(userId As String, Optional replaceForIn As Boolean = False, Optional checkType As String = "W") As String
            'If userId = "" Then
            '    Return String.Empty
            'End If
            'Dim dbConn As New DataConnectControl
            'Dim dtCont As New DataTableControl
            'Dim fldName As New ArrayList
            'fldName.Add(USERAUTHORITY.AUTHORIRYVALUE)
            'Dim whrSub As String = dbConn.Where(USERAUTHORITY.USERCODE, userId,, False)
            'whrSub &= dbConn.Where(USERAUTHORITY.AUTHORITYTYPE, checkType,, False)
            'Dim sql As String = USERAUTHORITY.SQL(fldName, whrSub, "")
            'Dim dr As DataRow = dbConn.QueryDataRow(sql, VarIni.JODS, dbConn.WhoCalledMe)
            'Dim wc As String = dtCont.IsDBNullDataRow(dr, USERAUTHORITY.AUTHORIRYVALUE)
            'If replaceForIn Then
            '    wc = " in ('" & wc.Replace(",", "','") & "') "
            'End If
            'Return wc
        End Function

        'noi change name form workCenter to checkAuthorize
        Function checkAuthorize(userId As String, Optional valCheck As String = "", Optional checkType As String = "W") As Boolean
            '    If userId = "" Then
            '        Return String.Empty
            '    End If
            '    Dim dbConn As New DataConnectControl
            '    Dim dtCont As New DataTableControl
            '    Dim fldName As New ArrayList
            '    fldName.Add(USERAUTHORITY.AUTHORIRYVALUE)
            '    Dim whrSub As String = dbConn.Where(USERAUTHORITY.USERCODE, userId,, False)
            '    whrSub &= dbConn.Where(USERAUTHORITY.AUTHORITYTYPE, checkType,, False)
            '    If valCheck <> "" Then
            '        whrSub &= dbConn.Where(USERAUTHORITY.AUTHORIRYVALUE, " LIKE '%" & valCheck.Trim & "%' ")
            '    End If
            '    Dim SQL As String = USERAUTHORITY.SQL(fldName, whrSub, "")
            '    Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.JODS, dbConn.WhoCalledMe)
            '    Dim valReturn As Boolean = False
            '    If dr IsNot Nothing Then
            '        valReturn = True
            '    End If
            '    Return valReturn
        End Function

    End Class
End Namespace