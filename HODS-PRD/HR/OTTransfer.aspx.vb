Imports System.Globalization

Public Class OTTransfer
    Inherits System.Web.UI.Page
    Dim CreateTable As New CreateTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim Table As String = "employeeTransfer"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            CreateTable.CreateEmployeeTransfer()
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            resetAll()
        End If
    End Sub
    Sub resetAll()

        Dim dToday As String = Date.Now.ToString("dd/MM/yyyy", New CultureInfo("en-US"))

        tbDate.Text = dToday
        'tbDateEdit.Text = dToday
        Date1.dateVal = dToday
        Date2.dateVal = dToday
        ucDateNew.dateVal = dToday
        ucDateEdit.dateVal = dToday
        'tbDateDept.Text = dToday

        ' Session("UserId") = "2"
        Dim SQL As String
        'Dim Sql As String = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
        'Dim Dept As String = Conn_SQL.Get_value(Sql, Conn_SQL.MIS_ConnectionString).Replace(",", "','")

        'Sql = "select rtrim(ME001) ME001,ME001+':'+ME002 as ME002 from CMSME where ME001 in ('" & Dept & "') order by ME001 "
        'ControlForm.showCheckboxList(cblDept, Sql, "ME002", "ME001", 4, Conn_SQL.ERP_ConnectionString)

        ucDept.setObjectWithSession = MyBase.Session("UserId")

        'ControlForm.showCheckboxList(cblDeptFrom, Sql, "ME002", "ME001", 4, Conn_SQL.ERP_ConnectionString)
        'ControlForm.showCheckboxList(cblDeptEdit, Sql, "ME002", "ME001", 4, Conn_SQL.ERP_ConnectionString)

        SQL = "select distinct CMSMV.MV004,CMSME.ME002,CMSMV.MV004+': '+CMSME.ME002 as ME03  from CMSMV " &
                " left join CMSME on CMSME.ME001 = CMSMV.MV004 where CMSMV.UDF03 like '%Normal%' and CMSMV.UDF02 not in ('') order by CMSMV.MV004 "
        ControlForm.showDDL(ddlDeptTo, SQL, "ME03", "MV004", True, Conn_SQL.ERP_ConnectionString)
        ControlForm.showDDL(ddlDeptToEdit, SQL, "ME03", "MV004", True, Conn_SQL.ERP_ConnectionString)

        TabContainer1.ActiveTabIndex = 0

        Dim visibleNew As Boolean = False,
                visibleEdit As Boolean = False,
                visibleShow As Boolean = False,
                visibleExport As Boolean = False

        Select Case TabContainer1.ActiveTabIndex
            Case 0 'by emp
                visibleNew = True
            Case 1 'by dept
                visibleNew = True
            Case 2 'edit
                visibleEdit = True
            Case 3 'report
                visibleShow = True
                visibleExport = True
        End Select
        btNew.Visible = visibleNew
        btEdit.Visible = visibleEdit
        btShow.Visible = visibleShow
        btExport.Visible = visibleExport

    End Sub

    Sub resetNew()
        tbEmpNo.Text = ""
        tbEmpNo0.Text = ""
        tbEmpNo1.Text = ""
        tbEmpNo2.Text = ""
        tbEmpNo3.Text = ""
        tbEmpNo4.Text = ""

    End Sub

    Sub resetEdit()
        tbEmpNoEdit.Text = ""
        tbEmpNoEdit0.Text = ""
        tbEmpNoEdit1.Text = ""
        tbEmpNoEdit2.Text = ""
        tbEmpNoEdit3.Text = ""
        tbEmpNoEdit4.Text = ""
        cbOutEdit.Checked = False
    End Sub

    Sub resetReport()
        tbEmpNoRe.Text = ""
        tbEmpNoRe0.Text = ""
        tbEmpNoRe1.Text = ""
        tbEmpNoRe2.Text = ""
        tbEmpNoRe3.Text = ""
        tbEmpNoRe4.Text = ""

        cbOut.Checked = False
    End Sub

    Private Sub gvEdit_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEdit.RowDataBound
        Dim sql As String = ""
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim ddlDept As DropDownList = .FindControl("ddlDept")
                Dim tbStartTime As TextBox = .FindControl("tbStartTime")
                Dim tbEndTime As TextBox = .FindControl("tbEndTime")
                Dim tbLocation As TextBox = .FindControl("tbLocationEdit")
                Dim tbEditDate As TextBox = .FindControl("tbEditDate")
                Dim WCFrom As DropDownList = .FindControl("ddlFromWC")
                Dim WCTo As DropDownList = .FindControl("ddlToWC")

                tbLocation.Text = .DataItem("Location").ToString.Trim

                If .DataItem("StartTime").ToString <> "" Then
                    tbStartTime.Text = .DataItem("StartTime").ToString.Substring(0, 5)
                End If

                If .DataItem("EndTime").ToString <> "" Then
                    tbEndTime.Text = .DataItem("EndTime").ToString.Substring(0, 5)
                End If
                tbEditDate.Text = .DataItem("DateofTransfer").ToString.Substring(6, 2) & "/" &
                                  .DataItem("DateofTransfer").ToString.Substring(4, 2) & "/" &
                                  .DataItem("DateofTransfer").ToString.Substring(0, 4)

                sql = "select MD001,MD001+':'+MD002 as MD002 from CMSMD where MD001 <> '" & .DataItem("WCFrom").ToString.Trim & "' order by MD001 "
                If .DataItem("WCFrom").ToString.Trim <> "" Then
                    ControlForm.showDDL(WCFrom, sql, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString, .DataItem("WCFrom").ToString.Trim)
                Else
                    ControlForm.showDDL(WCFrom, sql, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString, "Out Location")
                End If

                sql = "select MD001,MD001+':'+MD002 as MD002 from CMSMD where MD001 <> '" & .DataItem("WCTo").ToString.Trim & "' order by MD001 "
                If .DataItem("WCTo").ToString.Trim <> "" Then
                    ControlForm.showDDL(WCTo, sql, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString, .DataItem("WCTo").ToString.Trim)
                Else
                    ControlForm.showDDL(WCTo, sql, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString, "Out Location")
                End If

                sql = "select distinct CMSMV.MV004,CMSME.ME002,CMSMV.MV004+': '+CMSME.ME002 as ME03  from CMSMV " &
                " left join CMSME on CMSME.ME001 = CMSMV.MV004 where CMSMV.UDF03 like '%Normal%' and CMSMV.UDF02 not in ('') and CMSMV.MV004 <> '" & .DataItem("DeptTo").ToString.Trim & "' order by CMSMV.MV004 "

                Dim txt As String
                If .DataItem("DeptTo").ToString.Trim = "" Then
                    txt = "Out Location"
                Else
                    txt = .DataItem("DeptTo").ToString.Trim
                End If
                ControlForm.showDDL(ddlDept, sql, "ME03", "MV004", True, Conn_SQL.ERP_ConnectionString, txt)
            End If
            '.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            '.Attributes.Add("onclick", "ChangeRowColor(this,'','');") 
        End With
    End Sub

    Private Sub gvNew_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvNew.RowCommand
        If e.CommandName = "OnDelete" Then
            Dim i As Integer = e.CommandArgument
            Dim docNo As String = gvNew.Rows(i).Cells(20).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&")
            If TabContainer1.ActiveTabIndex = 1 And docNo <> "" Then
                Dim Sql As String = "delete from " & Table & " where DocNo='" & docNo & "' "
                Conn_SQL.Exec_Sql(Sql, Conn_SQL.MIS_ConnectionString)
                show_message.ShowMessage(Page, "delete Complete!!!", UpdatePanel1)
                btNew_Click(sender, e)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollNew", "gridviewScrollNew();", True)
            End If
        End If
    End Sub

    Private Sub gvNew_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNew.RowDataBound
        Dim sql As String = ""
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim ddlDept As DropDownList = .FindControl("ddlDeptNew")
                Dim WCTo As DropDownList = .FindControl("ddlToWC")
                Dim WCFrom As DropDownList = .FindControl("ddlFromWC")
                Dim tbStartTime As TextBox = .FindControl("tbStartTimeNew")
                Dim tbEndTime As TextBox = .FindControl("tbEndTimeNew")
                Dim tbLocation As TextBox = .FindControl("tbLocation")
                'Dim WCTo As DropDownList = .FindControl("ddlToWC")
                Dim ddlStartDateNew As DropDownList = .FindControl("ddlStartDateNew")
                Dim ddlEndDateNew As DropDownList = .FindControl("ddlEndDateNew")
                Dim tabIndex As Integer = TabContainer1.ActiveTabIndex

                'sql = "select MD001,MD001+':'+MD002 as MD002 from CMSMD order by MD001 "
                'ControlForm.showDDL(WCTo, sql, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString, "Out Location")

                sql = "select distinct CMSMV.MV004,CMSME.ME002,CMSMV.MV004+': '+CMSME.ME002 as ME03  from CMSMV left join CMSME on CMSME.ME001 = CMSMV.MV004 where CMSMV.UDF03 like '%Normal%' and CMSMV.UDF02 not in ('') order by CMSMV.MV004 "
                ControlForm.showDDL(ddlDept, sql, "ME03", "MV004", True, Conn_SQL.ERP_ConnectionString, "Out Location")

                Dim dateToday As String = If(ucDateNew.dateVal = "", Date.Now.ToString("yyyyMMdd"), ucDateNew.dateVal)

                If ddlShiftDays.Text.Substring(0, 1) = "D" Then
                    ddlStartDateNew.Items.Add(New ListItem(configDate.dateShow2(dateToday, "/"), dateToday))
                    ddlEndDateNew.Items.Add(New ListItem(configDate.dateShow2(dateToday, "/"), dateToday))

                Else
                    Dim dateTomorrow As String = configDate.strToDateTime(dateToday, "yyyyMMdd").AddDays(1).ToString("yyyyMMdd")

                    ddlStartDateNew.Items.Add(New ListItem(configDate.dateShow2(dateToday, "/"), dateToday))
                    ddlStartDateNew.Items.Add(New ListItem(configDate.dateShow2(dateTomorrow, "/"), dateTomorrow))

                    ddlEndDateNew.Items.Add(New ListItem(configDate.dateShow2(dateToday, "/"), dateToday))
                    ddlEndDateNew.Items.Add(New ListItem(configDate.dateShow2(dateTomorrow, "/"), dateTomorrow))
                End If


                Dim deptFrom As String = .DataItem("MV004").ToString.Trim
                sql = "select ME015 from CMSME where ME001='" & deptFrom & "'"
                Dim dt As DataTable = Conn_SQL.Get_DataReader(sql, Conn_SQL.ERP_ConnectionString)
                If dt.Rows.Count > 0 Then
                    Dim production As Boolean = If(dt.Rows(0).Item("ME015") = "Y", True, False)
                    If production Then
                        sql = "select MD001,MD001+':'+MD002 as MD002 from CMSMD where MD015='" & deptFrom & "' order by MD001 "
                        ControlForm.showDDL(WCFrom, sql, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString, "Out Location")

                        sql = "select MD001,MD001+':'+MD002 as MD002 from CMSMD  order by MD001 "
                        ControlForm.showDDL(WCTo, sql, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString, "Out Location")
                    Else
                        WCFrom.Items.Add(New ListItem(.DataItem("Dept.").ToString.Trim, deptFrom))
                    End If
                End If
                Dim tStart As String = tbStartTimeHead0.Text.Trim,
                    tEnd As String = tbEndTimeHead0.Text.Trim,
                    loc As String = tbLocationHead.Text.Trim
                If tabIndex = 1 Then 'edit

                    If .DataItem("StartDate").ToString.Trim <> "" Then
                        ddlStartDateNew.Text = .DataItem("StartDate").ToString.Trim
                    End If

                    If .DataItem("EndDate").ToString.Trim <> "" Then
                        ddlEndDateNew.Text = .DataItem("EndDate").ToString.Trim
                    End If
                    If .DataItem("MV004").ToString.Trim <> "Out" Then
                        ddlDept.Text = .DataItem("MV004").ToString.Trim
                    End If
                    If .DataItem("WCFrom").ToString.Trim <> "Out" Then 'E.WCForm,E.WCTo
                        WCFrom.Text = .DataItem("WCFrom").ToString.Trim
                    End If
                    If .DataItem("WCTo").ToString.Trim <> "Out" Then
                        WCTo.Text = .DataItem("WCTo").ToString.Trim
                    End If

                    If .DataItem("StartTime").ToString <> "" Then
                        'tbStartTime.Text = .DataItem("StartTime").ToString.Substring(0, 5)
                        tStart = .DataItem("StartTime").ToString.Substring(0, 5)
                    End If

                    If .DataItem("EndTime").ToString <> "" Then
                        'tbEndTime.Text = .DataItem("EndTime").ToString.Substring(0, 5)
                        tEnd = .DataItem("EndTime").ToString.Substring(0, 5)
                    End If

                    loc = .DataItem("Location").ToString.Trim
                End If

                tbStartTime.Text = tStart
                tbEndTime.Text = tEnd
                tbLocation.Text = loc
                ''set for confirm
                'For Each button As Button In e.Row.Cells(21).Controls.OfType(Of Button)()
                '    If button.CommandName = "OnDelete" Then
                '        Dim item As String = e.Row.Cells(20).Text
                '        button.Attributes("onclick") = "return confirm('Are you sure delete Emp. No(" & item & ").?');"
                '    End If
                'Next
            End If
            '.Style.Add(HtmlTextWriterStyle.Cursor, "pointer") 
            '.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End With
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End With
    End Sub

    Function returnVal(tb As TextBox) As String
        Return If(tb.Text.Trim = "", "", tb.Text.Trim & ",")
    End Function

    Protected Sub btNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNew.Click
        Dim WHR As String = "",
            SQL As String = ""

        If TabContainer1.ActiveTabIndex = 0 Then 'new
            If tbEmpNo.Text.Trim = "" And tbEmpNo0.Text.Trim = "" And tbEmpNo1.Text.Trim = "" And tbEmpNo2.Text.Trim = "" And tbEmpNo3.Text.Trim = "" And tbEmpNo4.Text.Trim = "" And ucDept.getChecked = 0 Then
                show_message.ShowMessage(Page, "Please Input Emp. No. and select one department(กรุณากรอกรหัสพนักงานหรือเลือกหน่วยงานที่ต้องการ)", UpdatePanel1)
                tbEmpNo.Focus()
                Exit Sub
            Else
                Dim dateWork As String = If(ucDateNew.dateVal = "", Date.Now.ToString("yyyyMMdd"), ucDateNew.dateVal)
                WHR = ""
                Dim strEmp As String = returnEmp()
                WHR &= If(strEmp = "", "", " and T.EmpNo in ('" & strEmp & "')")
                WHR &= Conn_SQL.Where("T.Dept", ucDept.getObject, False, "", True)
                WHR &= Conn_SQL.Where("T.ShiftDay", ddlShiftDays)

                SQL = " select isnull(CMSMV.UDF02,'') as 'Shift', " &
                      " CMSMV.MV001  as 'EmpNo', Rtrim(CMSMV.MV004)+'-'+CMSME.ME002  as 'Dept.', CMSMV.MV047 as 'Name', CMSMV.UDF04 as 'Line'," &
                      " isnull(T.DateofOT,'') DateofOT,isnull(T.OTEndDate,'') OTEndDate,isnull(T.OTEndTime,'') OTEndTime,isnull(T.ShiftDay,'') ShiftDay," &
                      " rtrim(isnull(T.AbsenceTime,'')) AbsenceTime,isnull(T.Holiday,'') Holiday ,isnull(T.OTStartTime,'') OTStartTime,'' DocNo, " &
                      " Rtrim(CMSMV.MV004) MV004 " &
                      " from " & Conn_SQL.DBReport & "..OTRecord T " &
                      " left join CMSMV on CMSMV.MV001 = T.EmpNo" &
                      " left join CMSME on CMSME.ME001 = CMSMV.MV004  " &
                      " where  T.DateofOT='" & configDate.dateShow2(dateWork, "/") & "' And cast(case when rtrim(AbsenceTime)='' then '0.0' else rtrim(AbsenceTime) end as decimal(5,2))<8 And CMSMV.UDF03 like '%Normal%' " & WHR &
                      " order by T.EmpNo"
            End If
        Else 'edit
            Dim strEmp As String = returnEmp()
            WHR &= If(strEmp = "", "", " and E.EmpNo in ('" & strEmp & "')")

            WHR &= Conn_SQL.Where("E.DeptFrom", ucDept.getObject)
            WHR &= Conn_SQL.Where("E.DeptTo", ddlDeptToEdit)
            WHR &= If(cbOutEdit.Checked, " and DeptTo = 'Out' ", "")

            SQL = " select isnull(E.Shift,'') as 'Shift', E.EmpNo  as 'EmpNo', Rtrim(E.DeptFrom)+'-'+CMSME.ME002  as 'Dept.', " &
                " E.EmpName as 'Name', E.Line as 'Line',isnull(T.DateofOT,'') DateofOT,isnull(T.OTEndDate,'') OTEndDate," &
                " isnull(T.OTEndTime,'') OTEndTime,isnull(T.ShiftDay,'') ShiftDay,rtrim(isnull(T.AbsenceTime,'')) AbsenceTime," &
                " isnull(T.Holiday,'') Holiday ,isnull(T.OTStartTime,'') OTStartTime,E.DocNo,Rtrim(E.DeptFrom) MV004, E.WCFrom,E.WCTo,E.Location," &
                " isnull(E.ShiftDays,'') ShiftDays,isnull(E.StartDate,'') StartDate,isnull(E.StartTime,'') StartTime," &
                " isnull(E.EndDate,'') EndDate,isnull(E.EndTime,'') EndTime " &
                " from " & Conn_SQL.DBReport & ".." & Table & " E " &
                " left join CMSMV on CMSMV.MV001 = E.EmpNo" &
                " left join CMSME on CMSME.ME001 = CMSMV.MV004  " &
                " left join " & Conn_SQL.DBReport & "..OTRecord T on T.EmpNo = E.EmpNo And right(rtrim(DateofOT),4)+SUBSTRING(DateofOT,4,2)+LEFT(DateofOT,2)=DateofTransfer  " &
                " where DateofTransfer ='" & ucDateEdit.dateVal & "' " & WHR & " order by DateofTransfer,DeptFrom,E.Line,E.EmpNo "
            'and T.ShiftDay=CMSMV.UDF06  and cast(case when rtrim(AbsenceTime)='' then '0.0' else rtrim(AbsenceTime) end as decimal(5,2))<8
        End If
        ControlForm.ShowGridView(gvNew, SQL, Conn_SQL.ERP_ConnectionString)
        Dim rc As Integer = ControlForm.rowGridview(gvNew)
        CountRow1.RowCount = rc
        If rc > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollNew", "gridviewScrollNew();", True)
            btCancel.Visible = True
            btSave.Visible = True
            btSave.Text = If(TabContainer1.ActiveTabIndex = 0, "Save", "Update")
            gvNew.Visible = True
            gvEdit.Visible = False
            gvShow.Visible = False
        End If
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        Dim sql As String = ""
        '    Dim Program As Data.DataTable 
        Dim cnt As Boolean = True,
            chk As Boolean = True,
            onRecord As Boolean = False,
            chkTimeStart As Boolean = True,
            chkTimeEnd As Boolean = True,
            chkout As Boolean = True,
            chkTime As Boolean = True

        For i As Integer = 0 To gvNew.Rows.Count - 1
            With gvNew.Rows(i)
                Dim DeptFrom As String = .Cells(4).Text.Trim.Substring(0, 3)
                Dim ddlDept As DropDownList = .FindControl("ddlDeptNew")
                Dim WCFrom As DropDownList = .FindControl("ddlFromWC")
                Dim WCTo As DropDownList = .FindControl("ddlToWC")
                Dim tbStartTime As TextBox = .FindControl("tbStartTimeNew")
                Dim tbEndTime As TextBox = .FindControl("tbEndTimeNew")
                Dim tbLocation As TextBox = .FindControl("tbLocation")

                Dim ddlStartDateNew As DropDownList = .FindControl("ddlStartDateNew")
                Dim ddlEndDateNew As DropDownList = .FindControl("ddlEndDateNew")
                Dim shiftDay As String = .Cells(13).Text.Trim.Replace("&nbsp;", "")
                Dim docNo As String = .Cells(20).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&")
                'And docNo = ""  Or docNo <> ""
                If ((tbStartTime.Text.Trim <> "__:__" Or tbEndTime.Text.Trim <> "__:__" Or tbLocation.Text.Trim <> "")) Then

                    If tbStartTime.Text.Trim = "__:__" Or tbEndTime.Text.Trim = "__:__" Then
                        cnt = 0
                        Exit For
                    Else
                        Dim ttStartOT As String,
                            ttEndOT As String

                        If .Cells(18).Text.Replace("&nbsp;", "").Trim = "" Then
                            'check start 
                            ttStartOT = If(shiftDay = "Day(7)", "07:00", If(shiftDay = "Day(8)", "08:00", "19:00"))
                            'check end 
                            ttEndOT = .Cells(17).Text.Replace("&nbsp;", "").Replace(".", ":").Trim
                            If ttEndOT = "" Then
                                ttEndOT = .Cells(16).Text.Replace("&nbsp;", "").Replace(".", ":").Trim
                            End If
                        Else 'holiday
                            'check start 
                            ttStartOT = .Cells(16).Text.Replace("&nbsp;", "").Replace(".", ":").Trim
                            'check end 
                            ttEndOT = .Cells(17).Text.Replace("&nbsp;", "").Replace(".", ":").Trim
                        End If

                        'Dim ttEndOT As String = ""
                        Dim dtStateOT As DateTime = configDate.strToDateTime(configDate.dateFormat2(.Cells(14).Text.Trim) & " " & ttStartOT),
                        dtEndOT As DateTime = configDate.strToDateTime(configDate.dateFormat2(.Cells(15).Text.Trim) & " " & ttEndOT),
                        dtStartTran As DateTime = configDate.strToDateTime(ddlStartDateNew.Text.Trim & " " & tbStartTime.Text.Trim),
                        dtEndTran As DateTime = configDate.strToDateTime(ddlEndDateNew.Text.Trim & " " & tbEndTime.Text.Trim)

                        If dtStartTran < dtStateOT Then
                            chkTimeStart = 0
                            Exit For
                        End If
                        If dtEndTran > dtEndOT Then
                            chkTimeEnd = 0
                            Exit For
                        End If

                        If DateDiff(DateInterval.Minute, dtStartTran, dtEndTran) < 0 Then
                            chkTime = False
                            Exit For
                        End If


                    End If

                    If (ddlDept.Text.Trim = "Out Location" Or WCTo.Text.Trim = "Out Location") And tbLocation.Text.Trim = "" Then
                        chkout = 0
                        Exit For
                    End If

                    If tbStartTime.Text.Trim <> "__:__" And tbEndTime.Text.Trim <> "__:__" And tbLocation.Text.Trim <> "" Then
                        onRecord = 1
                    End If

                End If

            End With
        Next

        If Not onRecord Or Not cnt Then
            show_message.ShowMessage(Page, "Please check, Start or End time or resean is null. (กรุณาตรวจสอบ ใส่เวลาเริ่มและจบให้ครบ)", UpdatePanel1)
            tbEmpNo.Focus()
            Exit Sub
        End If

        If Not chkTimeStart Then
            show_message.ShowMessage(Page, "Please check, Start is not correct. (กรุณาตรวจสอบเวลาเริ่มไม่สอดคล้องกับเวลาการทำ OT)", UpdatePanel1)
            tbEmpNo.Focus()
            Exit Sub
        End If

        If Not chkTimeEnd Then
            show_message.ShowMessage(Page, "Please check, Start is not correct. (กรุณาตรวจสอบเวลาจบไม่สอดคล้องกับเวลาการทำ OT)", UpdatePanel1)
            tbEmpNo.Focus()
            Exit Sub
        End If

        If Not chkTime Then
            show_message.ShowMessage(Page, "Please check time start and end  is not correct. (กรุณาตรวจสอบเวลาเริ่มและจบไม่ถูกต้อง)", UpdatePanel1)
            tbEmpNo.Focus()
            Exit Sub
        End If


        If Not chkout Then
            show_message.ShowMessage(Page, "When you select your Dept. please set data Reason. (กรณีเลือกทำงานในแผนก โปรดระบุข้อมูลช่องสถานที่)", UpdatePanel1)
            tbEmpNo.Focus()
            Exit Sub
        End If

        Dim cntSave As Integer = 0
        For i As Integer = 0 To gvNew.Rows.Count - 1
            With gvNew.Rows(i)

                Dim DeptFrom As String = .Cells(4).Text.Trim.Substring(0, 3)
                Dim ddlDept As DropDownList = .FindControl("ddlDeptNew")
                Dim WCFrom As DropDownList = .FindControl("ddlFromWC")
                Dim WCTo As DropDownList = .FindControl("ddlToWC")
                Dim tbStartTime As TextBox = .FindControl("tbStartTimeNew")
                Dim tbEndTime As TextBox = .FindControl("tbEndTimeNew")
                Dim tbLocation As TextBox = .FindControl("tbLocation")

                Dim ddlStartDateNew As DropDownList = .FindControl("ddlStartDateNew")
                Dim ddlEndDateNew As DropDownList = .FindControl("ddlEndDateNew")
                Dim docNo As String = .Cells(20).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&")
                If ((tbStartTime.Text.Trim <> "__:__" Or tbEndTime.Text.Trim <> "__:__" Or tbLocation.Text.Trim <> "") And docNo = "") Or (docNo <> "") Then

                    Dim fld As Hashtable = New Hashtable
                    Dim whr As Hashtable = New Hashtable
                    Dim fldUser As String = "ChangeBy",
                        fldDate As String = "ChangeDate",
                        act As String = "U"

                    If docNo = "" Then
                        docNo = getDocNo()
                        fldUser = "CreateBy"
                        fldDate = "CreateDate"
                        act = "I"
                    End If
                    whr.Add("DocNo", docNo)
                    fld.Add("EmpNo", .Cells(0).Text.Trim)
                    fld.Add("EmpName", .Cells(1).Text.Trim)
                    fld.Add("Shift", .Cells(2).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
                    fld.Add("ShiftDays", .Cells(13).Text.Trim.Replace("&nbsp;", ""))
                    fld.Add("Line", .Cells(3).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
                    fld.Add("DeptFrom", DeptFrom)
                    fld.Add("DeptTo", ddlDept.Text.ToString.Trim.Substring(0, 3))
                    fld.Add("WCFrom", WCFrom.Text.ToString.Trim.Substring(0, 3))
                    fld.Add("WCTo", If(WCTo.Items.Count = 0, "", WCTo.Text.ToString.Trim.Substring(0, 3)))
                    fld.Add("Location:N", tbLocation.Text.Trim)
                    fld.Add("StartDate", ddlStartDateNew.Text.Trim)
                    fld.Add("StartTime", tbStartTime.Text.Trim)
                    fld.Add("EndDate", ddlEndDateNew.Text.Trim)
                    fld.Add("EndTime", tbEndTime.Text.Trim)

                    'Dim StartTime As DateTime = configDate.strToDateTime(ddlStartDateNew.Text.Trim & " " & tbStartTime.Text.Trim)
                    'Dim EndTime As DateTime = configDate.strToDateTime(ddlEndDateNew.Text.Trim & " " & tbEndTime.Text.Trim)

                    fld.Add("TotalTime", DateDiff(DateInterval.Minute, configDate.strToDateTime(ddlStartDateNew.Text.Trim & " " & tbStartTime.Text.Trim), configDate.strToDateTime(ddlEndDateNew.Text.Trim & " " & tbEndTime.Text.Trim)))
                    fld.Add("DateofTransfer", configDate.dateFormat2(.Cells(14).Text.Trim.Replace("&nbsp;", "")))

                    fld.Add(fldUser, Session("UserName")) 'Create by
                    fld.Add(fldDate, Date.Now.ToString("yyyyMMdd HH:mm"))

                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(Table, fld, whr, act), Conn_SQL.MIS_ConnectionString)
                    cntSave += 1
                End If

            End With

        Next

        If cntSave = 0 Then
            show_message.ShowMessage(Page, "Please check no data to save. (กรุณาตรวจสอบ ไม่ได้กรอกข้อมูลสำหรับบันทึก)", UpdatePanel1)
            tbEmpNo.Focus()
            Exit Sub
        Else
            show_message.ShowMessage(Page, If(TabContainer1.ActiveTabIndex = 0, "Save", "Update") & " Complete " & cntSave & " rows(" & If(TabContainer1.ActiveTabIndex = 0, "บันทึกรายการใหม่", "ปรับปรุงรายการ") & " " & cntSave & " รายการ)", UpdatePanel1)

            btSave.Visible = False
            btCancel.Visible = False
            btNew.Visible = True

            gvEdit.Visible = False
            gvNew.Visible = False

            ShowData()
        End If

    End Sub

    Protected Sub btEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btEdit.Click

        'If tbEmpNoEdit.Text.Trim = "" And tbEmpNoEdit0.Text.Trim = "" And tbEmpNoEdit1.Text.Trim = "" And tbEmpNoEdit2.Text.Trim = "" And tbEmpNoEdit3.Text.Trim = "" And tbEmpNoEdit4.Text.Trim = "" Then
        '    show_message.ShowMessage(Page, "Please Input Emp. No.", UpdatePanel1)
        '    tbEmpNo.Focus()
        'Else 

        Dim SQL As String = "",
            WHR As String = ""

        If tbEmpNoEdit.Text.Trim <> "" Or tbEmpNoEdit0.Text.Trim <> "" Or tbEmpNoEdit1.Text.Trim <> "" Or tbEmpNoEdit2.Text.Trim <> "" Or tbEmpNoEdit3.Text.Trim <> "" Or tbEmpNoEdit4.Text.Trim <> "" Then
            WHR &= " and EmpNo in ('" & tbEmpNoEdit.Text.Trim & "','" & tbEmpNoEdit0.Text.Trim & "','" & tbEmpNoEdit1.Text.Trim & "','" & tbEmpNoEdit2.Text.Trim & "','" & tbEmpNoEdit3.Text.Trim & "','" & tbEmpNoEdit4.Text.Trim & "')"
        End If

        WHR &= Conn_SQL.Where("DeptFrom", ucDept.getObject)
        WHR &= Conn_SQL.Where("DeptTo", ddlDeptToEdit)

        If cbOutEdit.Checked = True Then
            WHR &= " and DeptTo = 'Out' "
        End If

        'SQL = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
        'Dim Dept As String = Conn_SQL.Get_value(Sql, Conn_SQL.MIS_ConnectionString).Replace(",", "','")

        'WHR = WHR & Conn_SQL.Where("DateofTransfer", configDate.dateFormat2(tbDateEdit.Text.Trim))

        'SqlDataSource1.SelectCommand = " select DocNo,EmpNo, EmpName , Shift , Line ,(select top 1 Rtrim(DeptFrom)+': '+CMSME.ME002 from JINPAO80.dbo.CMSME where CMSME.ME001 = DeptFrom ) as 'DeptFrom', " &
        '    " (select top 1 Rtrim(DeptTo)+': '+CMSME.ME002 from JINPAO80.dbo.CMSME where CMSME.ME001 = DeptTo ) as 'DeptTo' , (select top 1 Rtrim(WCFrom)+': '+CMSMD.MD002 from JINPAO80.dbo.CMSMD where CMSMD.MD001 = WCFrom ) as 'WCFrom', " &
        '    " (select top 1 Rtrim(WCTo)+': '+CMSMD.MD002 from JINPAO80.dbo.CMSMD where CMSMD.MD001 = WCTo ) as 'WCTo',DateofTransfer, StartTime, EndTime, Location from " & Table &
        '    " where DateofTransfer ='" & configDate.dateFormat2(tbDateEdit.Text.Trim) & "' " & WHR & " order by DateofTransfer,DeptFrom,Line,EmpNo "

        'SQL = " select DocNo,EmpNo, EmpName , Shift , Line ,(select top 1 Rtrim(DeptFrom)+': '+CMSME.ME002 from JINPAO80.dbo.CMSME where CMSME.ME001 = DeptFrom ) as 'DeptFrom', " &
        '    " (select top 1 Rtrim(DeptTo)+': '+CMSME.ME002 from JINPAO80.dbo.CMSME where CMSME.ME001 = DeptTo ) as 'DeptTo' , (select top 1 Rtrim(WCFrom)+': '+CMSMD.MD002 from JINPAO80.dbo.CMSMD where CMSMD.MD001 = WCFrom ) as 'WCFrom', " &
        '    " (select top 1 Rtrim(WCTo)+': '+CMSMD.MD002 from JINPAO80.dbo.CMSMD where CMSMD.MD001 = WCTo ) as 'WCTo',DateofTransfer, StartTime, EndTime, Location from " & Table &
        '    " where DateofTransfer ='" & configDate.dateFormat2(tbDateEdit.Text.Trim) & "' " & WHR & " order by DateofTransfer,DeptFrom,Line,EmpNo "



        '" left join JINPAO80.dbo.CMSME on CMSME.ME001 = DeptTo " & _ 
        '    ControlForm.ShowGridView(gvEdit, SQL, Conn_SQL.MIS_ConnectionString) 
        gvEdit.DataBind()


        CountRow1.RowCount = ControlForm.rowGridview(gvEdit)

        btCancel.Visible = True
        btUpdate.Visible = True

        ''   btEdit.Visible = False
        'btNew.Visible = False
        'btSave.Visible = False
        ''  btShow.Visible = False

        gvNew.Visible = False
        gvShow.Visible = False
        gvEdit.Visible = True

        btExport.Visible = False

        'End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollEdit", "gridviewScrollEdit();", True)

    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        ShowReport()

        gvShow.Visible = True
        gvEdit.Visible = False
        gvNew.Visible = False

        '   btExport.Visible = True
    End Sub

    Protected Sub btUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btUpdate.Click

        Dim sql As String = ""
        Dim Program As DataTable

        Dim cnt As Integer = 0,
            chk As Integer = 0,
            chkout As Integer = 0

        For i As Integer = 0 To gvEdit.Rows.Count - 1

            Dim DeptFrom As String = gvEdit.Rows(i).Cells(5).Text.Trim.Substring(0, 3)
            Dim ddlDept As DropDownList = gvEdit.Rows(i).Cells(6).FindControl("ddlDept")
            Dim WCFrom As DropDownList = gvEdit.Rows(i).Cells(7).FindControl("ddlFromWC")
            Dim WCTo As DropDownList = gvEdit.Rows(i).Cells(8).FindControl("ddlToWC")

            Dim tbStartTime As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbStartTime")
            Dim tbEndTime As TextBox = gvEdit.Rows(i).Cells(11).FindControl("tbEndTime")
            Dim tbLocation As TextBox = gvEdit.Rows(i).Cells(12).FindControl("tbLocationEdit")


            If tbStartTime.Text.Trim = "__:__" Or tbEndTime.Text.Trim = "__:__" Then
                cnt = cnt + 1
            End If

            If ddlDept.SelectedItem.Text.Trim = "Out Location" And tbLocation.Text.Trim = "" Then
                chk = chk + 1
            End If

            If DeptFrom.ToString.Trim = ddlDept.SelectedItem.Text.Trim.Substring(0, 3) And tbLocation.Text.Trim = "" Then
                chkout = chkout + 1
            End If

            sql = " select EmpNo from OTRecord where EmpNo = '" & gvEdit.Rows(i).Cells(1).Text.Trim & "' and SUBSTRING (DateofOT,7,4)+SUBSTRING (DateofOT,4,2)+SUBSTRING (DateofOT,1,2) = '" & Date.Now.ToString("yyyyMMdd") & "'" & _
                   " and OTEndTime <> '' and '" & tbEndTime.Text.Trim.Replace("__:__", "") & "' <= replace(CONVERT(char(10),OTEndTime,108),'.',':') " & _
                   " or  EmpNo = '" & gvEdit.Rows(i).Cells(1).Text.Trim & "' and SUBSTRING (DateofOT,7,4)+SUBSTRING (DateofOT,4,2)+SUBSTRING (DateofOT,1,2) = '" & Date.Now.ToString("yyyyMMdd") & "'" & _
                   " and OTEndTime = '' and '" & tbEndTime.Text.Trim.Replace("__:__", "") & "' <= replace(CONVERT(char(10),OTStartTime,108),'.',':') "
            Program = Conn_SQL.Get_DataReader(sql, Conn_SQL.MIS_ConnectionString)

        Next

        If cnt > 0 Or chk > 0 Or chkout > 0 Or Program.Rows.Count = 0 Then

            If cnt > 0 Then
                show_message.ShowMessage(Page, "Start or End time is null, Please check (กรุณาตรวจสอบ, ใส่เวลาเริ่มและจบให้ครบ)", UpdatePanel1)
            ElseIf chk > 0 Then
                show_message.ShowMessage(Page, "When you select Out Location please set data Reason (กรณีเลือกสถานที่อื่น โปรดระบุข้อมูลในช่องสถานที่)", UpdatePanel1)
            ElseIf chkout > 0 Then
                show_message.ShowMessage(Page, "When you select your Dept. please set data Out Reason (กรณีเลือกทำงานในแผนก โปรดระบุข้อมูลช่องสถานที่)", UpdatePanel1)
            ElseIf Program.Rows.Count = 0 Then
                show_message.ShowMessage(Page, "Data not matching, Please check data from OT Record 1.Not key OTRecord, 2.Check EndTime in Support Other greater than OTEndTime in OT Record.  (ข้อมูลไม่ตรงกัน, กรุณาตรวจสอบข้อมูลใน OT Record 1.ไม่ได้ทำการบันทึกข้อมูลในOT , 2.เวลาจบซัพพอทเลยเวลาเลิกงานปกติหรือเลยเวลาOT)", UpdatePanel1)
            End If

            btUpdate.Visible = True
            btCancel.Visible = True
            btEdit.Visible = True

        Else

            For i As Integer = 0 To gvEdit.Rows.Count - 1
                Dim DeptFrom As String = gvEdit.Rows(i).Cells(5).Text.Trim.Substring(0, 3)
                Dim ddlDept As DropDownList = gvEdit.Rows(i).Cells(6).FindControl("ddlDept")
                Dim WCFrom As DropDownList = gvEdit.Rows(i).Cells(7).FindControl("ddlFromWC")
                Dim WCTo As DropDownList = gvEdit.Rows(i).Cells(8).FindControl("ddlToWC")
                Dim tbEditDate As TextBox = gvEdit.Rows(i).Cells(9).FindControl("tbEditDate")
                Dim tbStartTime As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbStartTime")
                Dim tbEndTime As TextBox = gvEdit.Rows(i).Cells(11).FindControl("tbEndTime")
                Dim tbLocation As TextBox = gvEdit.Rows(i).Cells(12).FindControl("tbLocationEdit")


                Dim fld As Hashtable = New Hashtable
                Dim whr As Hashtable = New Hashtable
                Dim StartTime As DateTime = tbStartTime.Text.Trim
                Dim EndTime As DateTime = tbEndTime.Text.Trim

                fld.Add("ChangeBy", Session("UserName")) 'Create by
                'fld.Add("ChangeBy", "Gift") 'Create by 
                fld.Add("ChangeDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                fld.Add("DeptTo", ddlDept.SelectedItem.Text.Substring(0, 3).ToString.Trim)

                fld.Add("WCFrom", WCFrom.SelectedItem.Text.Substring(0, 3).ToString.Trim)
                fld.Add("WCTo", WCTo.SelectedItem.Text.Substring(0, 3).ToString.Trim)

                If ddlDept.SelectedItem.Text.Trim = "Out Location" Then
                    fld.Add("Location:N", tbLocation.Text.Trim)
                ElseIf DeptFrom.ToString.Trim = ddlDept.SelectedItem.Text.Substring(0, 3).ToString.Trim And tbLocation.Text.Trim <> "" Then
                    fld.Add("Location:N", tbLocation.Text.Trim)
                Else
                    fld.Add("Location:N", tbLocation.Text.Trim)
                End If

                fld.Add("StartTime", tbStartTime.Text.Trim)
                fld.Add("EndTime", tbEndTime.Text.Trim)


                Dim setDate = Server.HtmlEncode(tbEditDate.Text)
                Dim dt As DateTime = DateTime.ParseExact(setDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Dim getDate = dt.ToString("yyyyMMdd")

                If tbStartTime.Text.Trim > tbEndTime.Text.Trim Then
                    StartTime = dt & " " & StartTime
                    EndTime = DateAdd(DateInterval.Day, 1, dt) & " " & EndTime
                End If

                fld.Add("TotalTime", DateDiff(DateInterval.Minute, StartTime, EndTime))
                fld.Add("DateofTransfer", getDate)

                whr.Add("EmpNo", gvEdit.Rows(i).Cells(1).Text.Trim)
                whr.Add("DocNo", gvEdit.Rows(i).Cells(0).Text.Trim)

                Dim act As String = "U",
                    msg As String = "Update Complete!!"

                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(Table, fld, whr, act), Conn_SQL.MIS_ConnectionString)
                show_message.ShowMessage(Page, msg, UpdatePanel1)

            Next

            ShowData()

            gvShow.Visible = True

            btUpdate.Visible = False
            btCancel.Visible = False
            ''btShowRe.Visible = True 
            ''btSave.Visible = False  
            btEdit.Visible = True
            ''btNew.Visible = True

            gvEdit.Visible = False
            gvNew.Visible = False

        End If

    End Sub

    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCancel.Click
        resetAll()
        Select Case TabContainer1.ActiveTabIndex
            Case 0 'new
                resetNew()
            Case 1 'edit
                resetEdit()
            Case 2 'report
                resetReport()
        End Select

        'gvEdit.Visible = False
        'gvNew.Visible = False
        'gvShow.Visible = False

        'btCancel.Visible = False
        ''  btUpdate.Visible = False
        'btSave.Visible = False

        'btNew.Visible = True
        ''   btEdit.Visible = True
        ''   btShowRe.Visible = True 
        'ShowData()

    End Sub

    Protected Sub btExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click

        ControlForm.ExportGridViewToExcel("TransferSupport" & Session("UserName"), gvShow)

    End Sub

    Private Function getDocNo() As String
        Dim DocNo As String = ""
        Dim DateDoc As String = Date.Today.ToString("yyyyMMdd")
        Dim SQL As String = "select substring(DocNo,1,8),max(DocNo) as DocNo  from " & Table & " where DocNo like '" & DateDoc & "%' group by substring(DocNo,1,8)"
        Dim Program As New Data.DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If Program.Rows.Count > 0 Then
            DocNo = CDec(Program.Rows(0).Item("DocNo")) + 1
        Else
            DocNo = DateDoc & "0001"
        End If
        Return DocNo
    End Function

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Function returnEmp() As String
        Dim strEmp As String = ""
        Select Case TabContainer1.ActiveTabIndex
            Case 0 'new
                strEmp &= returnVal(tbEmpNo)
                strEmp &= returnVal(tbEmpNo0)
                strEmp &= returnVal(tbEmpNo1)
                strEmp &= returnVal(tbEmpNo2)
                strEmp &= returnVal(tbEmpNo3)
                strEmp &= returnVal(tbEmpNo4)
            Case 1 'edit
                strEmp &= returnVal(tbEmpNoEdit)
                strEmp &= returnVal(tbEmpNoEdit0)
                strEmp &= returnVal(tbEmpNoEdit1)
                strEmp &= returnVal(tbEmpNoEdit2)
                strEmp &= returnVal(tbEmpNoEdit3)
                strEmp &= returnVal(tbEmpNoEdit4)
            Case 2 'report
                strEmp &= returnVal(tbEmpNoRe)
                strEmp &= returnVal(tbEmpNoRe0)
                strEmp &= returnVal(tbEmpNoRe1)
                strEmp &= returnVal(tbEmpNoRe2)
                strEmp &= returnVal(tbEmpNoRe3)
                strEmp &= returnVal(tbEmpNoRe4)
        End Select
        strEmp = If(strEmp.Length = 0, "", strEmp.Substring(0, strEmp.Length - 1).Replace(",", "','"))
        Return strEmp
    End Function


    Public Sub ShowData()

        Dim SQL As String = "",
        WHR As String = Conn_SQL.Where("DeptFrom", ucDept.getObject)

        Dim strEmp As String = returnEmp()

        WHR &= If(strEmp = "", "", " and EmpNo in ('" & strEmp & "')")
        WHR &= Conn_SQL.Where("DateofTransfer", ucDateNew.dateVal, ucDateNew.dateVal)

        If TabContainer1.ActiveTabIndex = 1 Then
            WHR &= Conn_SQL.Where("DeptTo", ddlDeptToEdit)
            WHR &= If(cbOutEdit.Checked, " and DeptTo = 'Out' ", "")
        End If

        SQL = " select EmpNo, EmpName , Shift , Line ,(select top 1 Rtrim(DeptFrom)+' : '+CMSME.ME002 from JINPAO80.dbo.CMSME where CMSME.ME001 = DeptFrom ) as 'DeptFrom', " &
              " case when DeptTo='Out' then 'Out Location' else (select top 1 Rtrim(DeptTo)+' : '+CMSME.ME002 from JINPAO80.dbo.CMSME where CMSME.ME001 = DeptTo ) end as 'DeptTo' , " &
              " case when WCFrom='Out' then 'Out Location' else (select top 1 Rtrim(WCFrom)+': '+CMSMD.MD002 from JINPAO80.dbo.CMSMD where CMSMD.MD001 = WCFrom ) end as 'From WC', case when WCTo='Out' then 'Out Location' else (select top 1 Rtrim(WCTo)+': '+CMSMD.MD002 from JINPAO80.dbo.CMSMD where CMSMD.MD001 = WCTo ) end as 'To WC', Location as 'Reason', " &
              " substring(DateofTransfer,7,2)+'-'+substring(DateofTransfer,5,2)+'-'+substring(DateofTransfer,1,4) as 'DateofSupport', StartTime, EndTime , CAST(TotalTime/60.00 as decimal (10,1)) as 'Total Time(hrs.)' , ChangeBy , ChangeDate from " & Table &
              " where 1=1 " & WHR & " order by DateofTransfer,DeptFrom,Line,EmpNo  "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

        gvShow.Visible = True
        gvEdit.Visible = False
        gvNew.Visible = False
        btCancel.Visible = False
        btSave.Visible = False
        btUpdate.Visible = False

    End Sub

    Public Sub ShowReport()

        Dim SQL As String = "",
        WHR As String = ""

        WHR &= Conn_SQL.Where("DeptFrom", ucDept.getObject)
        WHR &= Conn_SQL.Where("DeptTo", ddlDeptTo)
        Dim strEmp As String = returnEmp()
        WHR &= If(strEmp = "", "", " and EmpNo in ('" & strEmp & "')")
        WHR &= If(cbOut.Checked, " and DeptTo = 'Out' ", "")
        WHR &= Conn_SQL.Where("DateofTransfer", Date1.dateVal, Date2.dateVal)

        SQL = " select EmpNo, EmpName , Shift , Line ,(select top 1 Rtrim(DeptFrom)+' : '+CMSME.ME002 from " & Conn_SQL.DBMain & "..CMSME where CMSME.ME001 = DeptFrom ) as 'DeptFrom', " &
           " case when DeptTo='Out' then 'Out Location' else (select top 1 Rtrim(DeptTo)+' : '+CMSME.ME002 from " & Conn_SQL.DBMain & "..CMSME where CMSME.ME001 = DeptTo ) end as 'DeptTo', " &
           " case when WCFrom='Out' then 'Out Location' else (select top 1 Rtrim(WCFrom)+': '+CMSMD.MD002 from " & Conn_SQL.DBMain & "..CMSMD where CMSMD.MD001 = WCFrom ) end as 'From WC', case when WCTo='Out' then 'Out Location' else (select top 1 Rtrim(WCTo)+': '+CMSMD.MD002 from JINPAO80.dbo.CMSMD where CMSMD.MD001 = WCTo ) end as 'To WC', Location as 'Reason',  " &
           " substring(DateofTransfer,7,2)+'-'+substring(DateofTransfer,5,2)+'-'+substring(DateofTransfer,1,4) as 'DateofTransfer', StartTime, EndTime , CAST(TotalTime/60.00 as decimal (10,1)) as 'Total Time(hrs.)' , ChangeBy , ChangeDate from " & Table &
           " where 1=1" & WHR & " order by DateofTransfer,DeptFrom,Line,EmpNo "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

        gvShow.Visible = True
    End Sub

    Private Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged

        If TabContainer1.ActiveTabIndex = 0 Then

            btNew.Text = "New"
            btNew.Visible = True
            btSave.Visible = False
            btCancel.Visible = False

            btEdit.Visible = False
            btUpdate.Visible = False
            btShow.Visible = False
            btExport.Visible = False

            'tbEmpNoRe.Text = ""
            'tbEmpNoEdit.Text = ""

            gvEdit.Visible = False

            'gvNew.DataSource = ""
            'gvNew.DataBind()

        ElseIf TabContainer1.ActiveTabIndex = 1 Then

            btNew.Text = "Edit"
            btNew.Visible = True
            btSave.Visible = False
            btEdit.Visible = False
            btUpdate.Visible = False
            btCancel.Visible = False

            'btNew.Visible = False
            btSave.Visible = False
            btShow.Visible = False
            btExport.Visible = False

            'tbEmpNoRe.Text = ""
            'tbEmpNo.Text = ""

            'gvNew.Visible = False
            gvEdit.Visible = False

            'gvNew.DataSource = ""
            'gvNew.DataBind()

        Else
            btShow.Visible = True
            btExport.Visible = True
            tbEmpNoRe.Text = ""
            btNew.Visible = False
            btSave.Visible = False
            btCancel.Visible = False
            btEdit.Visible = False
            btUpdate.Visible = False

            'tbEmpNoEdit.Text = ""
            'tbEmpNo.Text = ""

            gvEdit.Visible = False
            gvNew.Visible = False

            'gvShow.DataSource = ""
            'gvNew.DataBind()

        End If

    End Sub


End Class