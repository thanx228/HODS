Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class OTEmpList
    Inherits System.Web.UI.Page

    Dim dbConn As New DataConnectControl
    Dim cblCont As New CheckBoxListControl
    Dim dtCont As New DataTableControl
    Dim sqlCont As New SQLControl
    Dim whrCont As New WhereControl
    Dim gvCont As New GridviewControl
    Dim UserOTRecord As String = "UserOTRecord"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim deptcode As String = String.Empty
            Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
            deptcode = dtCont.IsDBNullDataRow(dr, "Dept")

            If deptcode = "" Then
                Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
            Else
                deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
            End If

            cblCont.showCheckboxList(cblDept, getDept(deptcode.Replace(",", "','")), VarIni.DBMIS, "CodeName", "Code", 4)
            btExport.Visible = False
        End If

    End Sub

    '=============== Function ===================

    Function getUser(userid As String)
        Dim SQL As String = String.Empty
        Dim fldName As New ArrayList
        fldName.Add("Dept")
        fldName.Add("Grp")
        SQL = VarIni.S & sqlCont.getFeild(fldName) & VarIni.F & UserOTRecord
        SQL &= VarIni.W & " 1=1 "
        SQL &= whrCont.Where("Id", userid,, False)
        Return SQL
    End Function

    Function getDept(code As String)

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

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        gvCont.ExportGridViewToExcel("EmployeeList" & Session(VarIni.UserId), gvShow)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        If Session("userid") = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If
        Dim deptcode As String = String.Empty
        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
        If deptcode = "" Then
            Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
        End If

        Dim fldName As New ArrayList
        Dim SQL As String = String.Empty
        Dim Whr As String = String.Empty
        Const Code As String = "Code"
        Const Name As String = "EmpName"
        Const Dept As String = "Dept"
        Const Position As String = "Pos"
        Const Pickup_Location As String = "PickUp"
        Const EmpState As String = "State"

        '--------------------------------------- GridView Show
        fldName.Add("A.CODE" & VarIni.char8 & Code)
        fldName.Add("A.EMP_NAME" & VarIni.char8 & Name)
        fldName.Add("A.DEPT_CODE +'-'+ISNULL(D.ShortName,'')" & VarIni.char8 & Dept)
        fldName.Add("A.POS_CODE+'-'+ISNULL(J.NAME ,'')" & VarIni.char8 & Position)
        fldName.Add("A.PICKUP_LOCATION" & VarIni.char8 & Pickup_Location)
        fldName.Add("A.EMP_STATE+'-'+S.Name" & VarIni.char8 & EmpState)

        Whr &= whrCont.Where("A.DEPT_CODE", cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        Whr &= whrCont.Where("A.CODE", tbEmpNo) 'EMP CODE
        Whr &= whrCont.Where("A.EMP_STATE", " NOT IN ('3001','3002','3003') ")

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & HR_EMPLOYEE.TABLENAME & " A"
        SQL &= VarIni.L & HR_DEPT.TABLENAME & " D" & VarIni.O2 & " D.Code = A.DEPT_CODE "
        SQL &= VarIni.L & " V_HR_EMP_STATE S ON S.Code=EMP_STATE"
        SQL &= VarIni.L & HR_JOB.TABLENAME & " J" & VarIni.O2 & " J.CODE=A.POS_CODE  "
        SQL &= VarIni.W & " 1=1 " & Whr & VarIni.O & " A.DEPT_CODE,A.CODE ASC"
        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        gvCont.ShowGridView(gvShow, dt)
        CountRow1.RowCount = gvCont.rowGridview(gvShow)
        btExport.Visible = True
    End Sub

End Class