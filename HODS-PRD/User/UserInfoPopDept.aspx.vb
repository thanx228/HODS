Imports MIS_HTI.FormControl
Imports MIS_HTI.DataControl

Public Class UserInfoPopDept
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTable As New CreateTable
    Dim table As String = "UserOTRecord"
    Dim cblCont As New CheckBoxListControl
    Dim sqlCont As New SQLControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            CreateTable.CreateUserOTRecord()
            Dim SQL As String = String.Empty
            Dim WHR As String = String.Empty
            Dim fldName As New ArrayList
            fldName.Add(HR_DEPT.CODE & ":Code")
            fldName.Add(HR_DEPT.CODE & "+'-'+" & HR_DEPT.NAME & "+'-'+ isnull(" & HR_DEPT.SHORT_NAME & ",''):CodeName")
            WHR &= VarIni.oneEqualOne
            SQL = "SELECT " & sqlCont.getFeild(fldName) & VarIni.F & HR_DEPT.TABLENAME & VarIni.W & WHR & sqlCont.getOrderBy(HR_DEPT.CODE)
            cblCont.showCheckboxList(cblDept, SQL, VarIni.DBMIS, "CodeName", "Code", 2)

            lbId.Text = Request.QueryString("ID").ToString.Trim
            lbUser.Text = Request.QueryString("User").Trim
            lbName.Text = Request.QueryString("UserName").Trim

            SQL = " select * from " & table & " where Id='" & lbId.Text & "' "
            Dim Program As New DataTable
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            If Program.Rows.Count > 0 Then
                With Program.Rows(0)
                    Dim para1() As String = .Item("Dept").ToString.Split(",")
                    Dim para2 As String = .Item("Grp").ToString.Trim
                    rblGrp.SelectedValue = para2
                    Dim allVal As String = ""
                    For Each allVal In para1

                        For Each boxItem As ListItem In cblDept.Items
                            Dim boxVal As String = CStr(boxItem.Value.Trim)
                            If boxItem.Value.Trim = allVal.Trim Then
                                boxItem.Selected = True
                            End If
                        Next
                    Next
                End With
            End If
        End If
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        Dim USQL As String
        Dim ID As String = lbId.Text
        Dim DeptList As String = ""
        Dim cnt As Decimal = 0,
            whr As String = ""
        For Each boxItem As ListItem In cblDept.Items
            Dim boxVal As String = CStr(boxItem.Value.Trim)
            If boxItem.Selected Then
                DeptList = DeptList & boxVal & ","
                cnt = cnt + 1
            End If
        Next
        'If cnt = 0 Then
        '    show_message.ShowMessage(Page, "Please select Work center!!.", UpdatePanel1)
        '    Exit Sub
        'Else
        If cnt > 0 Then
            DeptList = DeptList.Substring(0, DeptList.Count - 1)
        End If

        USQL = " if exists(select * from " & table & " where Id='" & ID & "' ) " &
               " update " & table & " set Dept='" & DeptList & "',Grp='" & rblGrp.SelectedValue & "' where Id='" & ID & "' else " &
               " insert into " & table & "(Id,Dept,Grp)values ('" & ID & "','" & DeptList & "','" & rblGrp.SelectedValue & "')"
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        'End If
        show_message.ShowMessage(Page, "Save Complete!!.", UpdatePanel1)

    End Sub

    Protected Sub btSelAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSelAll.Click
        For Each boxItem As ListItem In cblDept.Items
            boxItem.Selected = True
        Next
    End Sub

    Protected Sub btUnSelAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btUnSelAll.Click
        For Each boxItem As ListItem In cblDept.Items
            boxItem.Selected = False
        Next
    End Sub
End Class