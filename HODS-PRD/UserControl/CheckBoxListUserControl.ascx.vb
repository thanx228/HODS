Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class CheckBoxListUserControl
    Inherits System.Web.UI.UserControl

    Dim cblCont As New CheckBoxListControl
    Dim dbConn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Sub show(SQL As String, strConn As String, fldText As String, fldValue As String, Optional showColumn As Decimal = 4)
        cblCont.showCheckboxList(cbl, SQL, strConn, fldText, fldValue, showColumn)
    End Sub

    Sub show(dt As DataTable, fldText As String, fldValue As String, Optional showColumn As Decimal = 4)
        cblCont.showCheckboxList(cbl, dt, fldText, fldValue, showColumn)
    End Sub

    '================document type=====================
    'Sub ShowDocTypeFromPageCode(val As String, Optional showColumn As Integer = 4) 'exam=apmt520,apmt540
    '    Dim WHR As String = dbConn.WHERE_IN(OOBX.DocTypePage, val, addSigleQuote:=True)
    '    cblCont.showCheckboxList(cbl, OOBX.getDocType(WHR), OOBXL.DocType, OOBX.DocTypeNo, showColumn)
    'End Sub

    'Sub ShowDocTypeFull(val As String, Optional showColumn As Integer = 4) 'exam=5102,5104
    '    Dim WHR As String = dbConn.WHERE_IN(OOBX.DocTypeNo, val, addSigleQuote:=True)
    '    cblCont.showCheckboxList(cbl, OOBX.getDocType(WHR), OOBXL.DocType, OOBX.DocTypeNo, showColumn)
    'End Sub
    '================document type=====================

    '================department=====================
    'Sub ShowDept(Optional dept As String = "", Optional showColumn As Integer = 4)
    '    Dim whr As String = dbConn.WHERE_IN(OOEFL.DeptID, dept, addSigleQuote:=True)
    '    Dim fldName As New ArrayList
    '    fldName.Add(OOEFL.DeptID)
    '    fldName.Add(OOEFL.DeptID & "||' '|| " & OOEFL.Dept & VarIni.char8 & OOEFL.Dept)
    '    Dim SQL As String = OOEFL.SQLDepartmentLang(fldName, whr, OOEFL.DeptID, strSplit:=VarIni.char8)
    '    cblCont.showCheckboxList(cbl, SQL, VarIni.T100, OOEFL.Dept, OOEFL.DeptID, showColumn)
    'End Sub

    'Sub ShowSaleDept(Optional showColumn As Integer = 4)
    '    Dim saleDept As String = "MKTS1,MKTS2,MKTS3,MKTS4,MKTS5"
    '    ShowDept(saleDept, showColumn)
    'End Sub

    '================department=====================

    '================work center=====================
    'T100
    'Function SqlWorkCenter(whr As String, Optional ord As String = ECAA.WorkcenterID) As String
    '    Dim fldName As New ArrayList From {
    '        ECAA.WorkcenterID,
    '        ECAA.WorkcenterID & "||'-'||" & ECAA.Workcenter & VarIni.char8 & ECAA.Workcenter
    '    }
    '    Return ECAA.GetSQL(fldName, whr, ECAA.WorkcenterID, strSplit:=VarIni.char8)
    'End Function

    'Sub ShowWorkCenter(Optional wc As String = "", Optional showColumn As Integer = 4)
    '    Dim SQL As String = SqlWorkCenter(dbConn.WHERE_IN(ECAA.WorkcenterID, wc, addSigleQuote:=True))
    '    cblCont.showCheckboxList(cbl, SQL, VarIni.T100, ECAA.Workcenter, ECAA.WorkcenterID, showColumn)
    'End Sub

    'Sub ShowWorkCenterFromArea(Optional area As String = "0", Optional showColumn As Integer = 4)
    '    Dim SQL As String = SqlWorkCenter(dbConn.WHERE_EQUAL(ECAA.AREA, area))
    '    cblCont.showCheckboxList(cbl, SQL, VarIni.T100, ECAA.Workcenter, ECAA.WorkcenterID, showColumn)
    'End Sub

    'Sub ShowWorkCenterFromArea(ddl As DropDownList, Optional showColumn As Integer = 4)
    '    Dim SQL As String = SqlWorkCenter(dbConn.WHERE_EQUAL(ECAA.AREA, ddl))
    '    cblCont.showCheckboxList(cbl, SQL, VarIni.T100, ECAA.Workcenter, ECAA.WorkcenterID, showColumn)
    'End Sub
    ''KIOSK
    'Sub SqlWorkCenterKIOSK(isAMP As String, Optional showColumn As Decimal = 4)
    '    Dim fldName As New ArrayList From {
    '        "NAME",
    '        "NAME||'-'||DESCRIPTION" & VarIni.char8 & "DESCRIPTION"
    '    }
    '    Dim strSQL As New SQLString("WORKCENTERS", fldName, False, VarIni.char8)
    '    Dim whrFirst As String = ""
    '    whrFirst &= strSQL.WHERE_LIKE("NAME", "w", False)
    '    whrFirst &= dbConn.WHERE_IN("ISAMP", isAMP,, True)
    '    strSQL.SetWhere(whrFirst, True)
    '    strSQL.SetOrderBy("NAME")
    '    cblCont.showCheckboxList(cbl, strSQL.GetSQLString, VarIni.JSIMS, "DESCRIPTION", "NAME", showColumn)
    'End Sub

    '================work center=====================

    '================code info =====================
    'Sub ShowCodeInfo(CODE_TYPE As String, Optional SHOW_COLUMN As Decimal = 4, Optional whr As String = "")
    '    cblCont.showCheckboxList(cbl, CODEINFO.SQLHeadCode(CODE_TYPE, whr), VarIni.JODS, CODEINFO.NAME, CODEINFO.CODE, SHOW_COLUMN)
    'End Sub

    '================code info =====================

    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbl
        End Get
    End Property

    Public ReadOnly Property getChecked() As Integer
        Get
            Dim cnt As Integer = 0
            For Each boxItem As ListItem In cbl.Items
                If boxItem.Selected Then
                    cnt += 1
                End If
            Next
            Return cnt
        End Get
    End Property

    Private ReadOnly Property getValue() As String
        Get
            With New ListControl(New List(Of String))
                For Each boxItem As ListItem In cbl.Items
                    If boxItem.Selected Then
                        .Add(CStr(boxItem.Value.Trim))
                    End If
                Next
                Return .Text(VarIni.C)
            End With
        End Get
    End Property

    Public ReadOnly Property getValueAllList() As List(Of String)
        Get
            Dim valueListAll As New List(Of String)
            Dim val As String = ""
            For Each boxItem As ListItem In cbl.Items
                valueListAll.Add(CStr(boxItem.Value.Trim))
            Next
            Return valueListAll
        End Get
    End Property

    Private ReadOnly Property getValueAll() As String
        Get
            With New ListControl(New List(Of String))
                For Each boxItem As ListItem In cbl.Items
                    .Add(CStr(boxItem.Value.Trim))
                Next
                Return .Text(VarIni.C)
            End With
        End Get
    End Property

    Public Function Text(Optional getvalwhenempty As Boolean = False) As String
        Dim val As String = String.Empty
        If getChecked() > 0 Then
            val = getValue()
        Else
            If getvalwhenempty Then
                val = getValueAll()
            End If
        End If
        Return val
    End Function

End Class