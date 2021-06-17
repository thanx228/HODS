Imports System.Globalization

Public Class OTforAdmin
    Inherits System.Web.UI.Page
    Dim CreateTable As New CreateTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim Table As String = "OTRecord"
    Dim chrConn As String = Chr(8)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = ""
            Dim Program As Data.DataTable

            SQL = " select Id from UserOTRecord where Id = '" & Session("UserId") & "'"
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

            If Program.Rows.Count = 0 Then
                Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
            End If

            If tbOTDate.Text = "" Then
                tbOTDate.Text = Date.Now.ToString("dd/MM/yyyy", New CultureInfo("en-US"))
            End If

            SQL = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
            Dim Dept As String = Conn_SQL.Get_value(SQL, Conn_SQL.MIS_ConnectionString).Replace(",", "','")

            SQL = "select rtrim(ME001) ME001,ME001+':'+ME002 as ME002 from CMSME where ME001 in ('" & Dept & "') order by ME001 "
            ControlForm.showCheckboxList(cblDept, SQL, "ME002", "ME001", 4, Conn_SQL.ERP_ConnectionString)

            SQL = "select distinct CMSMV.UDF02 from CMSMV where CMSMV.UDF02 <> '' and CMSMV.MV004 in ('" & Dept & "') and CMSMV.UDF03 like '%Normal%' order by CMSMV.UDF02 "
            ControlForm.showCheckboxList(cblShift, SQL, "UDF02", "UDF02", 5, Conn_SQL.ERP_ConnectionString)

            CreateTable.CreateOTRecord()
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)

            btUpdate.Visible = False
            btCancel.Visible = False

        End If

    End Sub

    ' --------------------   GreidView    -------------------------

    Private Sub gvEdit_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEdit.RowDataBound
        Dim sql As String = ""
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim cbHoliday As CheckBox = .FindControl("cbHolidayEdit")
                Dim tbStartTime As TextBox = .FindControl("tbStartTimeEdit")
                Dim tbEndTime As TextBox = .FindControl("tbEndTimeEdit")
                Dim rdlEndTime As RadioButtonList = .FindControl("rdlEndTimeEdit")
                Dim ddlShiftDay As DropDownList = .FindControl("ddlShiftDayEdit")
                Dim ddlBusLine As DropDownList = .FindControl("ddlBusLineEdit")
                Dim tbOTLunch As TextBox = .FindControl("tbOTLunchEdit")
                Dim cbDinner As CheckBox = .FindControl("cbDinnerEdit")
                Dim cbOT As CheckBox = .FindControl("cbOTEdit")

                If ControlForm.rowGridview(gvEdit) <> -1 Then

                    If .DataItem("OTLunch").ToString.Trim <> "" Then
                        tbOTLunch.Text = .DataItem("OTLunch").ToString.Trim
                    End If

                    If .DataItem("Dinner").ToString.Trim = "1" Then
                        cbDinner.Checked = True
                    End If

                    If .DataItem("OT Type").ToString = "Holiday" Then
                        cbHoliday.Checked = True
                    End If

                    Dim values As ArrayList = New ArrayList()
                    If .DataItem("ShiftDay").ToString <> "" Then
                        values.Add("Day(7)")
                        values.Add("Day(8)")
                        values.Add("Night")
                    End If
                    ddlShiftDay.DataSource = values
                    ddlShiftDay.DataBind()

                    If .DataItem("ShiftDay").ToString = "Day(7)" Then
                        ddlShiftDay.Items.FindByValue("Day(7)").Selected = True
                    ElseIf .DataItem("ShiftDay").ToString = "Day(8)" Then
                        ddlShiftDay.Items.FindByValue("Day(8)").Selected = True
                    ElseIf .DataItem("ShiftDay").ToString = "Night" Then
                        ddlShiftDay.Items.FindByValue("Night").Selected = True
                    End If

                    If .DataItem("OTEndTime").ToString <> "" Then
                        tbEndTime.Text = .DataItem("OTEndTime").ToString.Trim
                    End If

                    If .DataItem("Start Time").ToString <> "" Then
                        tbStartTime.Text = .DataItem("Start Time").ToString.Trim
                    End If

                    If .DataItem("OT Over Date").ToString.Trim = "1" Then
                        cbOT.Checked = True
                    End If

                    If Not IsDBNull(.DataItem("BusLine")) Then
                        'If .DataItem("BusLine") <> "" Then
                        sql = "select Code Code,Rtrim(Code)+'-'+Name Name from CodeInfo where CodeType='BusLine' and Code <> " & .DataItem("BusLine").ToString.Substring(0, 2) & " order by Code"
                        ControlForm.showDDL(ddlBusLine, sql, "Name", "Code", True, Conn_SQL.MIS_ConnectionString, .DataItem("BusLine"))
                        'End If
                    Else
                        sql = "select Code Code,Rtrim(Code)+'-'+Name Name from CodeInfo where CodeType='BusLine' order by Code"
                        ControlForm.showDDL(ddlBusLine, sql, "Name", "Code", True, Conn_SQL.MIS_ConnectionString, "select")
                    End If
                End If
            End If
            '.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            '.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End With
    End Sub

    Private Sub gvEdit_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvEdit.RowCommand

        If e.CommandName = "OnDelete" Then
            Dim i As Integer = e.CommandArgument
            Dim strSqlup As String = "delete " & Table & " where DocNo = '" & gvEdit.Rows(i).Cells(1).Text.Trim & "'"
            Conn_SQL.Exec_Sql(strSqlup, Conn_SQL.MIS_ConnectionString)
            ShowReport()
            gvShowRe.Visible = True
            gvEdit.Visible = False
            btCancel.Visible = False
            btUpdate.Visible = False
            show_message.ShowMessage(Page, "ลบข้อมูลเรียบร้อยแล้ว", UpdatePanel1)
        End If

    End Sub

    ' ---------------------------   Button   ------------------------------------------

    Protected Sub btUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btUpdate.Click

        Dim OTStart As DateTime = Date.Now
        Dim OTEnd As DateTime = Date.Now
        Dim Diff As Decimal = 0
        Dim Total As Decimal = 0
        Dim EndTime As Integer = 0
        Dim Holiday As Integer = 0
        Dim AbsenHol As Integer = 0
        Dim Absence As Integer = 0
        Dim chkAbsTime As Integer = 0
        Dim Check As Integer = 0
        Dim setDate = Server.HtmlEncode(tbOTDate.Text)
        Dim dt As DateTime = DateTime.ParseExact(setDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)

        For i As Integer = 0 To gvEdit.Rows.Count - 1
            Dim tbStartTime As TextBox = gvEdit.Rows(i).Cells(12).FindControl("tbStartTimeEdit")
            Dim tbEndTime As TextBox = gvEdit.Rows(i).Cells(14).FindControl("tbEndTimeEdit")
           
            If tbEndTime.Text.Trim = "__.__" Then
                tbEndTime.Text = ""
            End If

            If tbStartTime.Text.Trim = "__.__" Then
                tbStartTime.Text = ""
            End If

        Next

        Dim SQL As String = ""
        Dim Program As New Data.DataTable

        For i As Integer = 0 To gvEdit.Rows.Count - 1

            'Dim cbAbsence As CheckBox = gvEdit.Rows(i).Cells(0).FindControl("cbAbsenceEdit")
            'Dim tbAbsenceTime As TextBox = gvEdit.Rows(i).Cells(1).FindControl("tbAbsenceTimeEdit")
            Dim cbHoliday As CheckBox = gvEdit.Rows(i).Cells(7).FindControl("cbHolidayEdit")
            Dim ddlShiftDay As DropDownList = gvEdit.Rows(i).Cells(9).FindControl("ddlShiftDayEdit")
            Dim tbOTLunch As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbOTLunchEdit")
            Dim tbStartTime As TextBox = gvEdit.Rows(i).Cells(12).FindControl("tbStartTimeEdit")
            Dim tbEndTime As TextBox = gvEdit.Rows(i).Cells(14).FindControl("tbEndTimeEdit")
            Dim ddlBusLine As DropDownList = gvEdit.Rows(i).Cells(16).FindControl("ddlBusLineEdit")
            Dim cbDinner As CheckBox = gvEdit.Rows(i).Cells(17).FindControl("cbDinnerEdit")
            Dim cbOT As CheckBox = gvEdit.Rows(i).Cells(18).FindControl("cbOTEdit")

            Dim fld As Hashtable = New Hashtable
            Dim whr As Hashtable = New Hashtable

            If tbEndTime.Text.Trim = "__.__" Then
                tbEndTime.Text = ""
            End If

            If tbStartTime.Text.Trim = "__.__" Then
                tbStartTime.Text = ""
            End If

            fld.Add("ChangeBy", Session("UserName")) 'Create by
            fld.Add("ShiftDay", ddlShiftDay.SelectedItem.Text.Trim)

            Dim AbsenceTime As Decimal = 0

            'If tbAbsenceTime.Text.Trim = "" Then
            '    AbsenceTime = 0
            'Else
            '    AbsenceTime = CDec(tbAbsenceTime.Text.Trim)
            'End If

            If cbOT.Checked = True Then
                fld.Add("OTOver", "1")
            Else
                fld.Add("OTOver", "")
            End If

            If AbsenceTime >= 8 Then
                fld.Add("OTEndTime", "")
                fld.Add("BusLine", "-")
                fld.Add("OTLunch", "")
                fld.Add("Dinner", "")
            Else
                fld.Add("OTEndTime", tbEndTime.Text.Trim.Replace(":", ".").Replace("24", "00"))
                fld.Add("OTStartTime", tbStartTime.Text.Trim.Replace(":", ".").Replace("24", "00"))
                fld.Add("BusLine", ddlBusLine.SelectedItem.Text.Trim.Substring(0, 2).Replace("se", "-"))
                fld.Add("OTLunch", tbOTLunch.Text.Trim)

                If cbDinner.Checked = True Then
                    fld.Add("Dinner", "1")
                Else
                    fld.Add("Dinner", "")
                End If

            End If

            'fld.Add("AbsenceTime", tbAbsenceTime.Text.Trim)
            fld.Add("ChangeDate", Date.Now.ToString("yyyyMMdd HH:mm"))

            'If cbAbsence.Checked = True Then
            '    fld.Add("Absence", "Absence")
            'Else
            '    fld.Add("Absence", "")
            'End If

            If cbHoliday.Checked = True Then
                fld.Add("Holiday", "Holiday")

                If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                    OTStart = dt & " " & tbStartTime.Text.Trim.Replace(".", ":")
                    OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                    fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                Else
                    If cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                        OTStart = dt & " " & tbStartTime.Text.Trim.Replace(".", ":")
                        OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                        fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                    Else
                        OTStart = dt & " " & tbStartTime.Text.Trim.Replace(".", ":")
                        OTEnd = dt & " " & tbEndTime.Text.Trim.Replace(".", ":")
                        fld.Add("OTEndDate", tbOTDate.Text.Trim)
                    End If

                End If

                If tbEndTime.Text.Trim.Substring(0, 2) = "12" Then
                    Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)

                    fld.Add("OTHours", Diff / 60.0)
                    Dim Lunch As Decimal = 0
                    If tbOTLunch.Text.Trim <> "" Then
                        Lunch = CDec(tbOTLunch.Text.Trim)
                    End If
                    fld.Add("OTTotal", (Diff / 60.0) + Lunch)

                ElseIf tbStartTime.Text.Trim.Substring(0, 2) = "16" Or tbStartTime.Text.Trim.Substring(0, 2) = "17" Then

                    Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)

                    fld.Add("OTHours", (Diff / 60.0))
                    Dim Lunch As Decimal = 0
                    If tbOTLunch.Text.Trim <> "" Then
                        Lunch = CDec(tbOTLunch.Text.Trim)
                    End If
                    fld.Add("OTTotal", ((Diff / 60.0)) + Lunch)

                Else
                    Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)

                    fld.Add("OTHours", (Diff / 60.0) - 1.0)
                    Dim Lunch As Decimal = 0
                    If tbOTLunch.Text.Trim <> "" Then
                        Lunch = CDec(tbOTLunch.Text.Trim)
                    End If
                    fld.Add("OTTotal", ((Diff / 60.0) - 1.0) + Lunch)
                End If

            Else

                fld.Add("Holiday", "")
                SQL = " select DateSat from NormalSatOT where DateSat = '" & tbOTDate.Text.Trim & "'"
                Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

                If Program.Rows.Count > 0 Then
                    If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                        fld.Add("OTStartTime", tbStartTime.Text.Trim)
                        OTStart = DateAdd(DateInterval.Day, 1, dt) & tbStartTime.Text.Trim
                    End If
                Else
                    If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                        fld.Add("OTStartTime", tbStartTime.Text.Trim)
                        OTStart = DateAdd(DateInterval.Day, 1, dt) & tbStartTime.Text.Trim
                    End If

                End If

                If tbEndTime.Text.Trim = "" Then
                    OTStart = dt
                    OTEnd = dt
                End If

                If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                    OTStart = dt & " " & tbStartTime.Text.Trim.Replace(".", ":")
                    OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                    fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                Else
                    If cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                        OTStart = dt & " " & tbStartTime.Text.Trim.Replace(".", ":")
                        OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                        fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                    Else
                        OTStart = dt & " " & tbStartTime.Text.Trim.Replace(".", ":")
                        OTEnd = dt & " " & tbEndTime.Text.Trim.Replace(".", ":")
                        fld.Add("OTEndDate", tbOTDate.Text.Trim)
                    End If

                End If

                Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)

                If tbEndTime.Text.Trim = "" Then
                    fld.Add("OTHours", "")
                Else
                    fld.Add("OTHours", (Diff / 60.0))
                End If

                Dim Lunch As Decimal = 0
                If tbOTLunch.Text.Trim <> "" Then
                    Lunch = CDec(tbOTLunch.Text.Trim)
                End If

                If tbEndTime.Text.Trim = "" And tbOTLunch.Text.Trim = "" Then
                    fld.Add("OTTotal", "")
                Else
                    fld.Add("OTTotal", (Diff / 60.0) + Lunch)
                End If


            End If

            Dim EmpNo = gvEdit.Rows(i).Cells(5).Text.Trim.ToString

            SQL = " select EmpNo as 'Emp No' from " & Table & " where EmpNo = '" & EmpNo & "' and DateofOT = '" & tbOTDate.Text.Trim & "'"
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

            Dim act As String = "",
                msg As String = "แก้ไขสำเร็จ , Update Complete!!"

            If Program.Rows(0).Item("Emp No").ToString.Trim = EmpNo Then
                act = "U"
                whr.Add("EmpNo", gvEdit.Rows(i).Cells(5).Text.Trim)
                whr.Add("OTStartDate", tbOTDate.Text.Trim)
            End If

            Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(Table, fld, whr, act), Conn_SQL.MIS_ConnectionString)
            show_message.ShowMessage(Page, msg, UpdatePanel1)

        Next

        ShowReport()

        gvShowRe.Visible = True
        gvEdit.Visible = False
        btUpdate.Visible = False
        btCancel.Visible = False

        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btEdit.Click


        Dim WHR As String = "",
            SQL As String = ""

        WHR = WHR & Conn_SQL.Where("Dept", cblDept)
        WHR = WHR & Conn_SQL.Where("Shift", cblShift)
        WHR = WHR & Conn_SQL.Where("OTStartDate", tbOTDate)
        WHR = WHR & " and ShiftDay like '%" & rdlShift.SelectedItem.Text & "%'"

        WHR = WHR & Conn_SQL.Where("EmpNo", tbEmpNo)

        SQL = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
        Dim Dept As String = Conn_SQL.Get_value(SQL, Conn_SQL.MIS_ConnectionString).Replace(",", "','")

        SQL = "select DocNo , Shift ,EmpNo,rtrim(Dept)+'-'+CMSME.ME002 as 'Dept' ,EmpName , " & _
              " case when Holiday = '' then 'Normal working day' else 'Holiday' end as 'OT Type' , rtrim(ShiftDay) as 'ShiftDay' , OTLunch, OTStartDate  ,  " & _
              " case when OTEndTime <> '' then OTStartTime else '' end as 'Start Time', " & _
              " case when OTEndTime <> '' then OTEndDate else '' end as 'End Date', " & _
              " OTEndTime , case when OTEndTime <> '' then DateofOT else '' end as DateofOT , Rtrim(BusLine) +' : '+ Name as 'BusLine' , Line as 'Line', Dinner as 'Dinner' ,OTOver as 'OT Over Date'" & _
              " from OTRecord" & _
              " left join CodeInfo on Code  = BusLine and CodeType = 'BusLine' " & _
              " left join JINPAO80.dbo.CMSME on CMSME.ME001 = rtrim(Dept)  " & _
              " where Dept in ('" & Dept & "') " & WHR & " order by Dept,Line,EmpNo,Shift "

        ControlForm.ShowGridView(gvEdit, SQL, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvEdit)
        System.Threading.Thread.Sleep(1000)

        gvShowRe.Visible = False
        gvEdit.Visible = True
        btUpdate.Visible = True
        btCancel.Visible = True

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollEdit", "gridviewScrollEdit();", True)

        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCancel.Click

        gvEdit.Visible = False
     
        btCancel.Visible = False
        btUpdate.Visible = False
    
        System.Threading.Thread.Sleep(1000)
    End Sub

    ' ------------------------------  Function  ----------------------------------

    Private Sub ShowReport()

        Dim sql As String = "",
            whr As String = ""

        whr = whr & Conn_SQL.Where("Dept", cblDept)
        whr = whr & Conn_SQL.Where("Shift", cblShift)
        whr = whr & Conn_SQL.Where("OTStartDate", tbOTDate)
        whr = whr & " and ShiftDay like '%" & rdlShift.SelectedItem.Text & "%'"
        whr = whr & Conn_SQL.Where("EmpNo", tbEmpNo)

        sql = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
        Dim Dept As String = Conn_SQL.Get_value(sql, Conn_SQL.MIS_ConnectionString).Replace(",", "','")

        sql = "select Shift ,rtrim(Dept)+'-'+CMSME.ME002 as 'Dept' ,Line as 'Line' ,EmpNo , EmpName , " & _
       " case when Holiday = '' then 'Normal working day' else 'Holiday' end as 'OT Type' , rtrim(ShiftDay) as 'ShiftDay' , OTStartDate  , rtrim(Absence) as 'Absence', AbsenceTime ,  " & _
       " case when OTEndTime <> '' then OTStartTime else '' end as 'OT Start Time', " & _
       " case when OTEndTime <> '' then OTEndDate else '' end as 'End Date', " & _
       " OTEndTime , case when OTEndTime <> '' then DateofOT else '' end as DateofOT  , " & _
       " OTHours as 'OT hrs.', " & _
       " OTLunch as 'OT Lunch' , " & _
       " OTTotal as 'Total OT hrs.'," & _
       " Rtrim(BusLine) +' : '+ Name as 'BusLine' ,  Dinner as 'Dinner' , Remark, ChangeBy as 'Change By' , ChangeDate as'Change Date' " & _
       " from OTRecord" & _
       " left join CodeInfo on Code  = BusLine and CodeType = 'BusLine' " & _
       " left join JINPAO80.dbo.CMSME on CMSME.ME001 = rtrim(Dept)  " & _
       " where Dept in ('" & Dept & "') " & whr & " order by Dept,Line,EmpNo,Shift "

        ControlForm.ShowGridView(gvShowRe, sql, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShowRe)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShowRe", "gridviewScrollShowRe();", True)

    End Sub

    Protected Sub setDataToTDropdownlist(ByVal ddl As DropDownList, ByVal val As String)
        ddl.SelectedValue = val
    End Sub

    Protected Sub setDataToTrdlist(ByVal rdl As RadioButtonList, ByVal val As String)
        rdl.SelectedValue = val
    End Sub

End Class