Imports System.Data
Imports MIS_HTI.DataControl
Public Class PLANSCHEDULE_T
    '## TABLE PlanSchedule
    Public Const table As String = "PlanSchedule"
    Public Const table_full As String = VarIni.DBMIS & ".." & table
    Public Const PlanDate As String = "PlanDate"
    Public Const WorkCenter As String = "WorkCenter"
    Public Const PlanSeq As String = "PlanSeq"
    Public Const PlanSeqSet As String = "PlanSeqSet"
    Public Const MoType As String = "MoType"
    Public Const MoNo As String = "MoNo"
    Public Const MoSeq As String = "MoSeq"
    Public Const ProcessCode As String = "ProcessCode"
    Public Const PlanedQty As String = "PlanedQty"
    Public Const PlanQty As String = "PlanQty"
    Public Const Urgent As String = "Urgent"
    Public Const PlanStatus As String = "PlanStatus"
    Public Const TransferNo As String = "TransferNo"
    Public Const PlanTimeStd As String = "PlanTimeStd"
    Public Const PlanTime As String = "PlanTime"
    Public Const PlanNote As String = "PlanNote"
    Public Const CreateBy As String = "CreateBy"
    Public Const CreateDate As String = "CreateDate"
    Public Const ChangeBy As String = "ChangeBy"
    Public Const ChangeDate As String = "ChangeDate"
    Public Const CancledBy As String = "CancledBy"
    Public Const CancledDate As String = "CancledDate"

    ''Dim conn_sql As New ConnSQL
    'Dim dbConn As New DataConnectControl


    'Public Function getData(whr As String, Optional fld As String = "MD002", Optional orderBy As String = "MD001") As DataTable
    '    If fld.Trim = "" Then
    '        fld = "*"
    '    End If
    '    Dim SQL As String = "select " & fld & " from " & table & " where isnull(UDF07,'')<>'No'  " & whr
    '    If orderBy <> "" Then
    '        SQL &= " order by " & orderBy
    '    End If
    '    Return dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
    'End Function

    'Public Function getName(code As String) As String
    '    Dim whr As String = dbConn.Where("MD001", code, , False)
    '    Dim dt As DataTable = getData(whr)
    '    Dim valReturn As String = ""
    '    If dt.Rows.Count > 0 Then
    '        valReturn = dt.Rows(0).Item(0).ToString.Trim
    '    End If
    '    Return valReturn
    'End Function

End Class