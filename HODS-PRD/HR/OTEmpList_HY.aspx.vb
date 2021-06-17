Imports MIS_HTI.FormControl
Imports MIS_HTI.DataControl

Public Class OTEmpList_HY
    Inherits System.Web.UI.Page
    Dim TABLE_UserOTRecord As String = "UserOTRecord"
    Dim TABLE_EMP_HY As String = "HR_EMPLOYEE_HY"
    Dim dbConn As New DataConnectControl
    Dim sqlCont As New SQLControl
    Dim gvCont As New GridviewControl
    Dim ddlCnt As New DropDownListControl
    Dim whrCont As New WhereControl
    Dim dtCont As New DataTableControl
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            getControl()
            btExport.Visible = False
        End If
    End Sub

    '============== Sub =========

    Private Sub getControl()

        Dim deptcode As String = String.Empty
        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
        Dim Grpcode As String = dtCont.IsDBNullDataRow(dr, "Grp")

        If deptcode = "" Then
            Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
        Else
            deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
            If Grpcode.Trim = "dept_head" Then
                btNew.Visible = True
                btSave.Visible = True
            Else
                btNew.Visible = False
                btSave.Visible = False
            End If
        End If
        ddlCnt.showDDL(ddlDept, getDept(deptcode.Replace(",", "','")), VarIni.DBMIS, "CodeName", "Code", 4)

        Dim SQL As String = String.Empty
        'PICK UP LOCATION
        SQL = VarIni.S & trailing_spaces(CODEINFO.Name) & " NAME" & VarIni.F & CODEINFO.TABLENAME
        SQL &= VarIni.W & VarIni.oneEqualOne
        SQL &= whrCont.Where(CODEINFO.CodeType, "PICKUP_LOCATION",, False)
        SQL &= VarIni.O & " CAST(" & CODEINFO.Code & " AS INT)"
        ddlCnt.showDDL(ddlPickUpLocation, SQL, VarIni.DBMIS, "NAME", "NAME", True, )
        'POSITION
        SQL = String.Empty
        SQL = VarIni.S & "CODE,CODE+'-'+rtrim(NAME) NAME" & VarIni.F & " V_HR_JOB "
        SQL &= VarIni.W & VarIni.oneEqualOne
        SQL &= VarIni.O & " CODE ASC"
        ddlCnt.showDDL(ddlPos, SQL, VarIni.DBMIS, "NAME", "CODE", True,)
        'STATE
        SQL = String.Empty
        SQL = VarIni.S & "Code,Code+'-'+Name Name" & VarIni.F & " V_HR_EMP_STATE "
        SQL &= VarIni.W & VarIni.oneEqualOne
        SQL &= VarIni.O & " Code ASC"
        ddlCnt.showDDL(ddlStus, SQL, VarIni.DBMIS, "Name", "Code", True, )
        'GENDER
        SQL = VarIni.S & trailing_spaces(CODEINFO.Code) & " CODE," & trailing_spaces(CODEINFO.Name) & " NAME" & VarIni.F & CODEINFO.TABLENAME
        SQL &= VarIni.W & VarIni.oneEqualOne
        SQL &= whrCont.Where(CODEINFO.CodeType, "Gender",, False)
        SQL &= VarIni.O & CODEINFO.Code
        ddlCnt.showDDL(ddlGender, SQL, VarIni.DBMIS, "NAME", "CODE", True, )
        'SHIFT
        SQL = VarIni.S & trailing_spaces(CODEINFO.Code) & " CODE," & trailing_spaces(CODEINFO.Name) & " NAME" & VarIni.F & CODEINFO.TABLENAME
        SQL &= VarIni.W & VarIni.oneEqualOne
        SQL &= whrCont.Where(CODEINFO.CodeType, "SHIFT",, False)
        SQL &= VarIni.O & CODEINFO.Code
        ddlCnt.showDDL(ddlShift, SQL, VarIni.DBMIS, "NAME", "CODE", True, )
        'SHIFTDAY
        SQL = VarIni.S & trailing_spaces(CODEINFO.Code) & " CODE," & trailing_spaces(CODEINFO.Name) & " NAME" & VarIni.F & CODEINFO.TABLENAME
        SQL &= VarIni.W & VarIni.oneEqualOne
        SQL &= whrCont.Where(CODEINFO.CodeType, "SHIFTDAY",, False)
        'SQL &= whrCont.Where(CODEINFO.Code, "Day",, False)
        SQL &= VarIni.O & CODEINFO.Code
        ddlCnt.showDDL(ddlShiftDay, SQL, VarIni.DBMIS, "NAME", "CODE", True, )
    End Sub

    Protected Sub saveData(sqlType As String)
        Dim whrHash As Hashtable = New Hashtable,
               fldHash As Hashtable = New Hashtable
        Dim SQL As String = String.Empty
        Dim sqlHash As New ArrayList
        fldHash.Add("EMP_NAME:N", tbName.Text.Trim & " " & tbSurName.Text.Trim)
        fldHash.Add("GENDER", ddlGender.SelectedValue.Trim)
        fldHash.Add("DEPT_CODE", ddlDept.SelectedValue.Trim)
        fldHash.Add("POS_CODE", ddlPos.SelectedValue.Trim)
        fldHash.Add("SHIFT", ddlShift.SelectedValue.Trim)
        fldHash.Add("SHIFTDAY", ddlShiftDay.SelectedValue.Trim)
        fldHash.Add("PICKUP_LOCATION:N", ddlPickUpLocation.SelectedValue.Trim)
        fldHash.Add("EMP_STATE", ddlStus.SelectedValue.Trim)

        If sqlType = "I" Then
            fldHash.Add("CODE", tbCode.Text.Trim)
            sqlHash.Add(sqlCont.GetSQL(TABLE_EMP_HY, fldHash, whrHash, sqlType))
        Else
            whrHash.Add("CODE", tbCode.Text.Trim)
            sqlHash.Add(sqlCont.GetSQL(TABLE_EMP_HY, fldHash, whrHash, sqlType))
        End If
        dbConn.TransactionSQL(sqlHash, VarIni.DBMIS, dbConn.WhoCalledMe)

    End Sub

    '============== Function =========

    Function trailing_spaces(str As String)
        Return "RTRIM(" & str & ")"
    End Function

    Function getUser(userid As String)
        Dim SQL As String = String.Empty
        Dim fldName As New ArrayList
        fldName.Add("Dept")
        fldName.Add("Grp")
        SQL = VarIni.S & sqlCont.getFeild(fldName) & VarIni.F & TABLE_UserOTRecord
        SQL &= VarIni.W & " 1=1 "
        SQL &= whrCont.Where("Id", userid,, False)
        Return SQL
    End Function

    Function getDept(code As String) As String

        Dim SQL As String = String.Empty
        Dim WHR As String = String.Empty
        Dim fldName As New ArrayList
        fldName.Add(HR_DEPT.CODE & ":Code")
        fldName.Add(HR_DEPT.CODE & "+'-'+" & HR_DEPT.NAME & "+'-'+ isnull(" & HR_DEPT.SHORT_NAME & ",''):CodeName")
        WHR &= VarIni.oneEqualOne
        WHR &= whrCont.Where(HR_DEPT.CODE, " in ('" & code & "')")
        SQL = VarIni.S & sqlCont.getFeild(fldName) & VarIni.F & HR_DEPT.TABLENAME & VarIni.W & WHR & sqlCont.getOrderBy(HR_DEPT.CODE)
        Return SQL
    End Function

    '============== Button =========

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Dim SQL As String,
            code As String = tbCode.Text.Trim

        If code = "" Then
            show_message.ShowMessage(Page, "CODE Is null,Please check it again!!!", UpdatePanel1)
            tbCode.Focus()
            Exit Sub
        End If

        If tbCode.Enabled Then
            SQL = VarIni.S & " CODE " & VarIni.F & TABLE_EMP_HY & VarIni.W & " CODE='" & code & "'"
            Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            If dr IsNot Nothing Then
                show_message.ShowMessage(Page, "Duplicate CODE,Please check it again!!!", UpdatePanel1)
                tbCode.Focus()
                Exit Sub
            End If
        End If
        saveData(If(tbCode.Enabled, "I", "U"))
        show_message.ShowMessage(Page, "Save Completed!!", UpdatePanel1)
    End Sub

    Protected Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        tbCode.Text = String.Empty
        tbCode.Enabled = True
        tbName.Text = String.Empty
        tbSurName.Text = String.Empty
        getControl()
        btExport.Visible = False

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim deptcode As String = String.Empty
        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")

        Dim fldName As New ArrayList
        fldName.Add("CODE:A")
        fldName.Add("EMP_NAME:B")
        fldName.Add("GENDER:C")
        fldName.Add("DEPT_CODE:D")
        fldName.Add("POS_CODE:E")
        fldName.Add("PICKUP_LOCATION:F")
        fldName.Add("EMP_STATE:G")
        fldName.Add("SHIFT:H")
        fldName.Add("SHIFTDAY:I")
        Dim sql As String = String.Empty
        sql = VarIni.S & sqlCont.getFeild(fldName) & VarIni.F & TABLE_EMP_HY
        sql &= VarIni.W & " 1=1 "
        sql &= dbConn.Where("CODE", tbCode)
        If tbName.Text.Trim <> "" Then
            sql &= dbConn.Where("EMP_NAME", " like N'%" & tbName.Text & "%'")
        End If
        If tbSurName.Text.Trim <> "" Then
            sql &= dbConn.Where("EMP_NAME", " like N'%" & tbSurName.Text & "%'")
        End If

        sql &= dbConn.Where("GENDER", ddlGender)
        sql &= dbConn.Where("DEPT_CODE", If(ddlDept.SelectedValue = "0", " IN ('" & deptcode.Replace(",", "','") & "')", ddlDept),, True)
        sql &= dbConn.Where("POS_CODE", ddlPos,, True)
        sql &= dbConn.Where("SHIFTDAY", ddlShiftDay)
        sql &= dbConn.Where("SHIFT", ddlShift)
        If ddlPickUpLocation.SelectedValue <> "0" Then
            sql &= dbConn.Where("PICKUP_LOCATION", " in (N'" & ddlPickUpLocation.SelectedValue & "') ")
        End If
        sql &= dbConn.Where("EMP_STATE", ddlStus,, True)
        Dim dt As DataTable = dbConn.Query(sql, VarIni.DBMIS, dbConn.WhoCalledMe)
        gvCont.ShowGridView(gvShow, sql, VarIni.DBMIS)
        CntRow.RowCount = gvCont.rowGridview(gvShow)
        btExport.Visible = True
    End Sub

    '============== GridView Event=========
    Private Sub gvShow_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShow.RowCommand
        Dim i As Integer = e.CommandArgument
        Select Case e.CommandName
            Case "onEdit"
                tbCode.Text = gvShow.Rows(i).Cells(1).Text.Trim
                tbName.Text = gvShow.Rows(i).Cells(2).Text.Trim.Split(" ").ToArray(0)
                tbCode.Enabled = False
                tbSurName.Text = gvShow.Rows(i).Cells(2).Text.Trim.Split(" ").ToArray(1)
                ddlGender.SelectedValue = gvShow.Rows(i).Cells(3).Text.Trim
                ddlDept.SelectedValue = gvShow.Rows(i).Cells(4).Text.Trim
                ddlPos.SelectedValue = gvShow.Rows(i).Cells(5).Text.Trim
                ddlPickUpLocation.SelectedValue = gvShow.Rows(i).Cells(6).Text.Trim
                ddlStus.SelectedValue = gvShow.Rows(i).Cells(7).Text.Trim
                ddlShift.SelectedValue = gvShow.Rows(i).Cells(8).Text.Trim
                ddlShiftDay.SelectedValue = gvShow.Rows(i).Cells(9).Text.Trim
        End Select
    End Sub

    '============== Export Excel =========

    Protected Sub btExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("OT_HONGYANG" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        tbCode.Text = String.Empty
        tbCode.Enabled = True
        tbName.Text = String.Empty
        tbSurName.Text = String.Empty
        getControl()
        btExport.Visible = False
    End Sub
End Class