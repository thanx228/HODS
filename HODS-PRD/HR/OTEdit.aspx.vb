Imports System.Globalization
Public Class OTEdit
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

            SetButton()

            SQL = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
            Dim Dept As String = Conn_SQL.Get_value(SQL, Conn_SQL.MIS_ConnectionString).Replace(",", "','")

            SQL = "select rtrim(ME001) ME001,ME001+':'+ME002 as ME002 from CMSME where ME001 in ('" & Dept & "') order by ME001 "
            ControlForm.showCheckboxList(cblDept, SQL, "ME002", "ME001", 4, Conn_SQL.ERP_ConnectionString)

            SQL = "SELECT Code,Name FROM CodeInfo WHERE CodeType  ='SHIFT'"
            ControlForm.showCheckboxList(cblShift, SQL, "Code", "Name", 5, Conn_SQL.MIS_ConnectionString)


            CreateTable.CreateOTRecord()
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)

            lbOther.Visible = False
            lbSelect.Visible = False
            btSave.Visible = False
            btExport.Visible = False
            btExcel.Visible = False
            btEdit.Visible = False
            btUpdate.Visible = False
            btCancel.Visible = False

        End If

    End Sub

    ' --------------------   GreidView    -------------------------

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        Dim sql As String = ""
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim cbHoliday As CheckBox = .FindControl("cbHoliday")
                Dim cbAbsence As CheckBox = .FindControl("cbAbsence")
                Dim tbAbsenceTime As TextBox = .FindControl("tbAbsenceTime")
                Dim tbEndTime As TextBox = .FindControl("tbEndTime")
                Dim rdlEndTime As RadioButtonList = .FindControl("rdlEndTime")
                Dim ddlShiftDay As DropDownList = .FindControl("ddlShiftDay")
                Dim ddlBusLine As DropDownList = .FindControl("ddlBusLine")
                Dim cbDinner As CheckBox = .FindControl("cbDinner")

                Dim values As ArrayList = New ArrayList()
                Dim ddlDay As ArrayList = New ArrayList()
                If rdlShift.SelectedValue = 0 Then
                    values.Add("NO OT")
                    values.Add("16.00")
                    values.Add("19.00")
                    values.Add("21.00")
                    values.Add("Other OT")
                    'ddlDay.Add("Day(7)")
                    'ddlDay.Add("Day(8)")
                Else
                    values.Add("NO OT")
                    values.Add("04.00")
                    values.Add("07.00")
                    values.Add("Other OT")
                    ddlDay.Add("Night")
                End If

                rdlEndTime.DataSource = values
                rdlEndTime.DataBind()

                If cbSetHoliday.Checked = True Then
                    cbHoliday.Checked = True
                    If rdlDefault.SelectedItem.Text = "NO OT" Then
                        rdlEndTime.Items.FindByValue("16.00").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "16:00" Then
                        rdlEndTime.Items.FindByValue("16.00").Selected = True
                    End If
                Else
                    If rdlDefault.SelectedItem.Text = "NO OT" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "16:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    End If
                End If

                If cbSetDinner.Checked = True Then
                    cbDinner.Checked = True
                End If

                If rdlShift.SelectedItem.Text = "Day" Then

                    '    If rdlDefault.SelectedItem.Text = "NO OT" Then
                    '        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    '        '    'ElseIf rdlDefault.SelectedItem.Text = "16:00" Then
                    '        '    '    rdlEndTime.Items.FindByValue("16.00").Selected = True
                    'Else
                    If rdlDefault.SelectedItem.Text = "19:00" Then
                        rdlEndTime.Items.FindByValue("19.00").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "21:00" Then
                        rdlEndTime.Items.FindByValue("21.00").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "04:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "07:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    End If

                Else
                    If rdlDefault.SelectedItem.Text = "NO OT" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "16:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "19:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "21:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "04:00" Then
                        rdlEndTime.Items.FindByValue("04.00").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "07:00" Then
                        rdlEndTime.Items.FindByValue("07.00").Selected = True
                    End If

                End If

                ddlShiftDay.DataSource = ddlDay
                ddlShiftDay.DataBind()

                If ControlForm.rowGridview(gvShow) <> -1 Then
                    If Not IsDBNull(.DataItem("ShiftDay")) Then
                        If rdlShift.SelectedIndex = 0 Then
                            If .DataItem("ShiftDay") <> "" Then
                                sql = "select distinct CMSMV.UDF06 from CMSMV where CMSMV.UDF06 <> '' and CMSMV.UDF06 <> '" & .DataItem("ShiftDay").ToString.Trim & "' " & returnWhereShiftDay("CMSMV.UDF06")
                                ControlForm.showDDL(ddlShiftDay, sql, "UDF06", "UDF06", True, Conn_SQL.ERP_ConnectionString, .DataItem("ShiftDay"))
                            End If
                        Else
                            ddlShiftDay.DataSource = ddlDay
                            ddlShiftDay.DataBind()
                        End If
                    End If
                    If Not IsDBNull(.DataItem("BusLine")) Then
                        If .DataItem("BusLine") <> "" Then
                            sql = "select Code Code,Rtrim(Code)+'-'+Name Name from CodeInfo where CodeType='BusLine' and Code <> " & .DataItem("BusLine").ToString.Substring(0, 2) & " order by Code"
                            ControlForm.showDDL(ddlBusLine, sql, "Name", "Code", True, Conn_SQL.MIS_ConnectionString, .DataItem("BusLine"))
                        Else
                            sql = "select Code Code,Rtrim(Code)+'-'+Name Name from CodeInfo where CodeType='BusLine'  order by Code"
                            ControlForm.showDDL(ddlBusLine, sql, "Name", "Code", True, Conn_SQL.MIS_ConnectionString, "select")
                        End If
                    End If
                End If
            End If
            '.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            '.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End With

    End Sub

    Private Sub gvWork_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvWork.RowDataBound
        Dim sql As String = ""
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim cbHoliday As CheckBox = .FindControl("cbHolidayWork")
                Dim cbWork As CheckBox = .FindControl("cbWork")
                Dim tbEndTime As TextBox = .FindControl("tbEndTimeWork")
                Dim rdlEndTime As RadioButtonList = .FindControl("rdlEndTimeWork")
                Dim ddlShiftDay As DropDownList = .FindControl("ddlShiftDayWork")
                Dim ddlBusLine As DropDownList = .FindControl("ddlBusLineWork")
                Dim cbDinner As CheckBox = .FindControl("cbDinnerWork")

                Dim values As ArrayList = New ArrayList()
                Dim ddlDay As ArrayList = New ArrayList()
                If rdlShift.SelectedValue = 0 Then
                    values.Add("NO OT")
                    values.Add("16.00")
                    values.Add("19.00")
                    values.Add("21.00")
                    values.Add("Other OT")
                    'ddlDay.Add("Day(7)")
                    'ddlDay.Add("Day(8)")
                Else
                    values.Add("NO OT")
                    values.Add("04.00")
                    values.Add("07.00")
                    values.Add("Other OT")
                    ddlDay.Add("Night")
                End If
                rdlEndTime.DataSource = values
                rdlEndTime.DataBind()


                If cbSetHoliday.Checked = True Then
                    'cbHoliday.Checked = True
                    If rdlDefault.SelectedItem.Text = "NO OT" Then
                        rdlEndTime.Items.FindByValue("16.00").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "16:00" Then
                        rdlEndTime.Items.FindByValue("16.00").Selected = True
                    End If
                Else
                    If rdlDefault.SelectedItem.Text = "NO OT" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "16:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    End If
                End If

                'If cbSetDinner.Checked = True Then
                '    cbDinner.Checked = True
                'End If

                If rdlShift.SelectedItem.Text = "Day" Then

                    '    If rdlDefault.SelectedItem.Text = "NO OT" Then
                    '        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    '        '    'ElseIf rdlDefault.SelectedItem.Text = "16:00" Then
                    '        '    '    rdlEndTime.Items.FindByValue("16.00").Selected = True
                    'Else
                    If rdlDefault.SelectedItem.Text = "19:00" Then
                        rdlEndTime.Items.FindByValue("19.00").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "21:00" Then
                        rdlEndTime.Items.FindByValue("21.00").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "04:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "07:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    End If

                Else
                    If rdlDefault.SelectedItem.Text = "NO OT" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "16:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "19:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "21:00" Then
                        rdlEndTime.Items.FindByValue("NO OT").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "04:00" Then
                        rdlEndTime.Items.FindByValue("04.00").Selected = True
                    ElseIf rdlDefault.SelectedItem.Text = "07:00" Then
                        rdlEndTime.Items.FindByValue("07.00").Selected = True
                    End If

                End If

                ddlShiftDay.DataSource = ddlDay
                ddlShiftDay.DataBind()

                If ControlForm.rowGridview(gvShow) <> -1 Then
                    If Not IsDBNull(.DataItem("ShiftDay")) Then
                        If rdlShift.SelectedIndex = 0 Then
                            If .DataItem("ShiftDay") <> "" Then
                                sql = "select distinct CMSMV.UDF06 from CMSMV where CMSMV.UDF06 <> '' and CMSMV.UDF06 <> '" & .DataItem("ShiftDay").ToString.Trim & "' " & returnWhereShiftDay("CMSMV.UDF06")
                                ControlForm.showDDL(ddlShiftDay, sql, "UDF06", "UDF06", True, Conn_SQL.ERP_ConnectionString, .DataItem("ShiftDay"))
                            End If
                        Else
                            ddlShiftDay.DataSource = ddlDay
                            ddlShiftDay.DataBind()
                        End If
                    End If
                    If Not IsDBNull(.DataItem("BusLine")) Then
                        If .DataItem("BusLine") <> "" Then
                            sql = "select Code Code,Rtrim(Code)+'-'+Name Name from CodeInfo where CodeType='BusLine' and Code <> " & .DataItem("BusLine").ToString.Substring(0, 2) & " order by Code"
                            ControlForm.showDDL(ddlBusLine, sql, "Name", "Code", True, Conn_SQL.MIS_ConnectionString, .DataItem("BusLine"))
                        Else
                            sql = "select Code Code,Rtrim(Code)+'-'+Name Name from CodeInfo where CodeType='BusLine'  order by Code"
                            ControlForm.showDDL(ddlBusLine, sql, "Name", "Code", True, Conn_SQL.MIS_ConnectionString, "select")
                        End If
                    End If
                End If
            End If
            '.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            '.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End With

    End Sub

    Private Sub gvEdit_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEdit.RowDataBound
        Dim sql As String = ""
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim cbHoliday As CheckBox = .FindControl("cbHolidayEdit")
                Dim cbAbsence As CheckBox = .FindControl("cbAbsenceEdit")
                Dim tbAbsenceTime As TextBox = .FindControl("tbAbsenceTimeEdit")
                Dim tbEndTime As TextBox = .FindControl("tbEndTimeEdit")
                Dim rdlEndTime As RadioButtonList = .FindControl("rdlEndTimeEdit")
                Dim ddlShiftDay As DropDownList = .FindControl("ddlShiftDayEdit")
                Dim ddlBusLine As DropDownList = .FindControl("ddlBusLineEdit")
                Dim tbOTLunch As TextBox = .FindControl("tbOTLunchEdit")
                Dim cbDinner As CheckBox = .FindControl("cbDinnerEdit")
                Dim cbOT As CheckBox = .FindControl("cbOTEdit")

                'Dim tbEndTime As TextBox = .FindControl("tbEndTimeEdit_MaskedEditExtender")

                If ControlForm.rowGridview(gvEdit) <> -1 Then

                    If .DataItem("Absence").ToString = "Absence" Then
                        cbAbsence.Checked = True
                    End If

                    If .DataItem("OTLunch").ToString.Trim <> "" Then
                        tbOTLunch.Text = .DataItem("OTLunch").ToString.Trim
                    End If

                    If .DataItem("Dinner").ToString.Trim = "1" Then
                        cbDinner.Checked = True
                    End If

                    If .DataItem("AbsenceTime").ToString <> "" Then
                        tbAbsenceTime.Text = .DataItem("AbsenceTime").ToString
                    End If

                    If .DataItem("OT Type").ToString = "Holiday" Then
                        cbHoliday.Checked = True
                    End If

                    Dim values As ArrayList = New ArrayList()
                    If .DataItem("ShiftDay").ToString <> "" Then
                        If ddlArea.Text.Trim = "1" Then
                            values.Add("Day(7)")
                            values.Add("Day(8)")
                            values.Add("Night")
                        Else
                            values.Add("Day(7-17)")
                            values.Add("Day(8-18)")
                        End If
                    End If
                    ddlShiftDay.DataSource = values
                    ddlShiftDay.DataBind()

                    Dim valShiftDay As String = .DataItem("ShiftDay").ToString
                    ddlShiftDay.Items.FindByValue(valShiftDay).Selected = True

                    'If .DataItem("ShiftDay").ToString = "Day(7)" Then
                    '    ddlShiftDay.Items.FindByValue("Day(7)").Selected = True
                    'ElseIf .DataItem("ShiftDay").ToString = "Day(7-17)" Then
                    '    ddlShiftDay.Items.FindByValue("Day(7-17)").Selected = True
                    'ElseIf .DataItem("ShiftDay").ToString = "Day(8)" Then
                    '    ddlShiftDay.Items.FindByValue("Day(8)").Selected = True
                    'ElseIf .DataItem("ShiftDay").ToString = "Day(8-18)" Then
                    '    ddlShiftDay.Items.FindByValue("Day(8-18)").Selected = True
                    'ElseIf .DataItem("ShiftDay").ToString = "Night" Then
                    '    ddlShiftDay.Items.FindByValue("Night").Selected = True
                    'End If

                    If .DataItem("OTEndTime").ToString <> "" Then
                        tbEndTime.Text = .DataItem("OTEndTime").ToString.Trim
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

    ' ---------------------------   Button   ------------------------------------------

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click

        Dim SQL As String = "",
                   USQL As String = ""
        Dim seq As Integer = 1
        Dim Program As New Data.DataTable
        Dim cnt As Decimal = 0

        Dim setDate = Server.HtmlEncode(tbOTDate.Text)
        Dim dt As DateTime = DateTime.ParseExact(setDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)

        Dim Absence As Integer = 0
        Dim Holiday As Integer = 0
        Dim EndTime As Integer = 0
        Dim OtherTime As Integer = 0
        Dim chk16 As Integer = 0

        Dim OTStart As DateTime = dt
        Dim OTEnd As DateTime = dt
        Dim Diff As Decimal = 0
        Dim Total As Decimal = 0

        For i As Integer = 0 To gvShow.Rows.Count - 1
            Dim cbAbsence As CheckBox = gvShow.Rows(i).Cells(6).FindControl("cbAbsence")
            Dim tbAbsenceTime As TextBox = gvShow.Rows(i).Cells(7).FindControl("tbAbsenceTime")
            Dim cbHoliday As CheckBox = gvShow.Rows(i).Cells(5).FindControl("cbHoliday")
            Dim rdlEndTime As RadioButtonList = gvShow.Rows(i).Cells(10).FindControl("rdlEndTime")
            Dim tbEndTime As TextBox = gvShow.Rows(i).Cells(10).FindControl("tbEndTime")

            Dim cntTxt As Integer = tbEndTime.Text.Trim.Length

            If tbEndTime.Text.Trim = "__.__" Then
                tbEndTime.Text = ""
            End If

            If rdlEndTime.SelectedItem.Text = "Other OT" And tbEndTime.Text.Trim = "" Then
                OtherTime = OtherTime + 1
            End If
            If cbHoliday.Checked = True And rdlEndTime.SelectedValue = "NO OT" Then
                Holiday = Holiday + 1
            End If
            If cbAbsence.Checked = True And tbAbsenceTime.Text.Trim <> "" Or cbAbsence.Checked = False And tbAbsenceTime.Text.Trim = "" Then
            Else
                Absence = Absence + 1
            End If

            If cbAbsence.Checked = False And tbAbsenceTime.Text.Trim = "" And cbHoliday.Checked = True Or
                cbAbsence.Checked = False And tbAbsenceTime.Text.Trim = "" And cbHoliday.Checked = False Or
                cbAbsence.Checked = True And tbAbsenceTime.Text.Trim <> "" And cbHoliday.Checked = False Then
            Else
                Holiday = Holiday + 1
            End If

            If tbEndTime.Text.Trim <> "" And cntTxt <> 5 Then
                EndTime = EndTime + 1
            End If

            If cbHoliday.Checked = True Then
                cnt = cnt + 1
            End If
        Next
        For i As Integer = 0 To gvShow.Rows.Count - 1
            Dim cbHoliday As CheckBox = gvShow.Rows(i).Cells(5).FindControl("cbHoliday")
            Dim rdlEndTime As RadioButtonList = gvShow.Rows(i).Cells(10).FindControl("rdlEndTime")
            If cnt <= 0 Then
                If cbHoliday.Checked = False And rdlEndTime.SelectedItem.Text = "16.00" Then
                    chk16 = chk16 + 1
                End If
            End If
        Next

        If Absence > 0 Or Holiday > 0 Or EndTime > 0 Or OtherTime > 0 Or chk16 > 0 Then
            If Absence > 0 Then
                show_message.ShowMessage(Page, "กรณีลางานเลือกช่อง Absence โปรดระบุช่อง AbsenceTime, กรณีลางานไม่สามาถระบุ OT Other", UpdatePanel1)
            ElseIf Holiday > 0 Then
                show_message.ShowMessage(Page, "กรณีเลือกช่อง Holiday ไม่สามารถเลือกช่อง Absence หรือไม่สามารถตั้งค่าเป็น NO OT ", UpdatePanel1)
            ElseIf EndTime > 0 Then
                show_message.ShowMessage(Page, "กรณีเลือก OT Other ให้ตั้งค่าเวลาเป็นรูปแบบ HH.mm ให้ถูกต้อง ", UpdatePanel1)
            ElseIf OtherTime > 0 Then
                show_message.ShowMessage(Page, "กรณีเลือก OT Other โปรดระบุเวลาที่ช่องว่างเป็นรูปแบบเวลา HH.mm ให้ถูกต้อง ", UpdatePanel1)
            ElseIf chk16 > 0 Then
                show_message.ShowMessage(Page, "กรณีทำงานปกติไม่สามารถเลือกเวลา OT End Time เป็น 16.00 ", UpdatePanel1)
            End If
        ElseIf Absence = 0 And Holiday = 0 And EndTime = 0 And OtherTime = 0 Then
            For i As Integer = 0 To gvShow.Rows.Count - 1
                Dim cbHoliday As CheckBox = gvShow.Rows(i).Cells(5).FindControl("cbHoliday")
                If cbHoliday.Checked = True Then
                    cnt = cnt + 1
                End If
            Next

            If cnt > 0 Then
                ' Holiday
                For i As Integer = 0 To gvShow.Rows.Count - 1

                    Dim fld As Hashtable = New Hashtable
                    Dim whr As Hashtable = New Hashtable


                    fld.Add("Shift", gvShow.Rows(i).Cells(0).Text.Trim.Replace("&nbsp;", " ").Replace("&amp;", "&"))
                    fld.Add("Dept", gvShow.Rows(i).Cells(1).Text.Trim.Substring(0, 3))
                    fld.Add("Line", gvShow.Rows(i).Cells(2).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
                    fld.Add("EmpNo", gvShow.Rows(i).Cells(3).Text.Trim)
                    fld.Add("EmpName", gvShow.Rows(i).Cells(4).Text.Trim)

                    Dim cbHoliday As CheckBox = gvShow.Rows(i).Cells(5).FindControl("cbHoliday")
                    Dim cbAbsence As CheckBox = gvShow.Rows(i).Cells(6).FindControl("cbAbsence")
                    Dim tbAbsenceTime As TextBox = gvShow.Rows(i).Cells(7).FindControl("tbAbsenceTime")
                    Dim ddlShiftDay As DropDownList = gvShow.Rows(i).Cells(8).FindControl("ddlShiftDay")
                    Dim tbOTLunch As TextBox = gvShow.Rows(i).Cells(9).FindControl("tbOTLunch")
                    Dim tbEndTime As TextBox = gvShow.Rows(i).Cells(10).FindControl("tbEndTime")
                    Dim rdlEndTime As RadioButtonList = gvShow.Rows(i).Cells(10).FindControl("rdlEndTime")
                    Dim ddlBusLine As DropDownList = gvShow.Rows(i).Cells(11).FindControl("ddlBusLine")
                    Dim cbDinner As CheckBox = gvShow.Rows(i).Cells(12).FindControl("cbDinner")
                    Dim cbOT As CheckBox = gvShow.Rows(i).Cells(13).FindControl("cbOT")

                    If tbEndTime.Text.Trim = "__.__" Then
                        tbEndTime.Text = ""
                    End If

                    Dim AbsenceTime As Decimal = 0

                    If tbAbsenceTime.Text = "" Then
                        AbsenceTime = 0
                    Else
                        AbsenceTime = CDec(tbAbsenceTime.Text.Trim)
                    End If

                    If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Or
                        ddlShiftDay.SelectedItem.Text.Trim = "Day(7-17)" Then
                        fld.Add("OTStartTime", "07.00")
                        OTStart = dt & " 07:00"
                    ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Or
                        ddlShiftDay.SelectedItem.Text.Trim = "Day(8-18)" Then
                        fld.Add("OTStartTime", "08.00")
                        OTStart = dt & " 08:00"
                    ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                        fld.Add("OTStartTime", "19.00")
                        OTStart = dt & " 19:00"
                    End If

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
                        If rdlEndTime.SelectedItem.Text = "NO OT" Then
                            fld.Add("OTEndTime", "")
                        ElseIf rdlEndTime.SelectedItem.Text = "Other OT" Then
                            fld.Add("OTEndTime", tbEndTime.Text.Trim.Replace(":", ".").Replace("24", "00"))
                            If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                            Else
                                If cbOT.Checked = True Then
                                    OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                Else
                                    OTEnd = dt & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                End If
                            End If
                        Else
                            fld.Add("OTEndTime", rdlEndTime.SelectedItem.Text.Trim)
                            If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & rdlEndTime.SelectedItem.Text.Trim.Replace(".", ":")
                            Else
                                OTEnd = dt & " " & rdlEndTime.SelectedItem.Text.Trim.Replace(".", ":")
                            End If
                        End If
                        fld.Add("BusLine", ddlBusLine.SelectedItem.Text.Trim.Substring(0, 2))
                        fld.Add("OTLunch", tbOTLunch.Text.Trim)

                        If cbDinner.Checked = True Then
                            fld.Add("Dinner", "1")
                        Else
                            fld.Add("Dinner", "")
                        End If

                        If tbEndTime.Text.Trim <> "" Then
                            If tbEndTime.Text.Trim.Substring(0, 2) = "12" Then
                                Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)
                                fld.Add("OTHours", Diff / 60.0)
                                Dim Lunch As Decimal = 0
                                If tbOTLunch.Text.Trim <> "" Then
                                    Lunch = CDec(tbOTLunch.Text.Trim)
                                End If
                                fld.Add("OTTotal", (Diff / 60.0) + Lunch)

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
                            Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)
                            fld.Add("OTHours", (Diff / 60.0) - 1.0)
                            Dim Lunch As Decimal = 0
                            If tbOTLunch.Text.Trim <> "" Then
                                Lunch = CDec(tbOTLunch.Text.Trim)
                            End If
                            fld.Add("OTTotal", ((Diff / 60.0) - 1.0) + Lunch)
                        End If

                    End If

                    fld.Add("AbsenceTime", tbAbsenceTime.Text.Trim)
                    fld.Add("ShiftDay", ddlShiftDay.SelectedItem.Text.Trim)
                    fld.Add("OTStartDate", tbOTDate.Text.Trim)
                    fld.Add("DateofOT", tbOTDate.Text.Trim)


                    If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                        fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                    ElseIf cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                        fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                    Else
                        fld.Add("OTEndDate", tbOTDate.Text.Trim)
                    End If

                    If cbAbsence.Checked = True Then
                        fld.Add("Absence", "Absence")
                    Else
                        fld.Add("Absence", "")
                    End If

                    If cbHoliday.Checked = True Then
                        fld.Add("Holiday", "Holiday")

                        Dim EmpNo = gvShow.Rows(i).Cells(3).Text.ToString

                        SQL = " select EmpNo as 'EmpNo' from " & Table & " where EmpNo = '" & EmpNo & "' and DateofOT = '" & tbOTDate.Text.Trim & "'"
                        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

                        Dim act As String = "",
                            msg As String = "บันทึกสำเร็จ , Record Complete!!"

                        If Program.Rows.Count > 0 Then
                            If Program.Rows(0).Item("EmpNo") = EmpNo Then
                                act = "U"
                                whr.Add("EmpNo", gvShow.Rows(i).Cells(3).Text.Trim)
                                whr.Add("OTStartDate", tbOTDate.Text.Trim)
                                fld.Add("ChangeBy", Session("UserName")) 'Create by
                                fld.Add("ChangeDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                            Else

                            End If
                        Else
                            act = "I"
                            fld.Add("DocNo", getDocNo())
                            fld.Add("CreateBy", Session("UserName")) 'Create by
                            fld.Add("CreateDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                        End If

                        SQL = " select isnull(CMSMV.UDF02,'') as 'Shift', " &
                              " CMSMV.MV001  as 'Emp. No.', Rtrim(CMSMV.MV004)+'-'+CMSME.ME002  as 'Dept.', CMSMV.MV047 as 'Name', " &
                              " isnull((Rtrim(C.Code)+'-'+C.Name),'')  as 'BusLine', CMSMV.UDF04 as 'Line' " &
                              " from CMSMV " &
                              " left join CMSME on CMSME.ME001 = CMSMV.MV004  " &
                              " left join HOOTHAI_REPORT.dbo.CodeInfo C on C.Code = SUBSTRING (CMSMV.UDF01,1,2) " &
                              " where CMSMV.UDF03 like '%Normal%' and CMSMV.MV001 ='" & EmpNo & "'" &
                              " order by CMSMV.MV004,CMSMV.UDF04,CMSMV.MV001,CMSMV.UDF02 "
                        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

                        'For j As Integer = 0 To Program.Rows.Count - 1

                        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(Table, fld, whr, act), Conn_SQL.MIS_ConnectionString)
                        show_message.ShowMessage(Page, msg, UpdatePanel1)
                        seq += 1
                        'Next

                    Else
                        fld.Add("Holiday", "")
                    End If

                Next
            End If

            If cnt <= 0 Then
                ' Normal
                If gvShow.Rows(0).Cells(0).Text = "No Data Found" Then

                Else

                    For i As Integer = 0 To gvShow.Rows.Count - 1

                        Dim fld As Hashtable = New Hashtable
                        Dim whr As Hashtable = New Hashtable

                        fld.Add("Shift", gvShow.Rows(i).Cells(0).Text.Trim.Replace("&nbsp;", " ").Replace("&amp;", "&"))
                        fld.Add("Dept", gvShow.Rows(i).Cells(1).Text.Trim.Substring(0, 3))
                        fld.Add("Line", gvShow.Rows(i).Cells(2).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
                        fld.Add("EmpNo", gvShow.Rows(i).Cells(3).Text.Trim)
                        fld.Add("EmpName", gvShow.Rows(i).Cells(4).Text.Trim)

                        Dim cbHoliday As CheckBox = gvShow.Rows(i).Cells(5).FindControl("cbHoliday")
                        Dim cbAbsence As CheckBox = gvShow.Rows(i).Cells(6).FindControl("cbAbsence")
                        Dim tbAbsenceTime As TextBox = gvShow.Rows(i).Cells(7).FindControl("tbAbsenceTime")
                        Dim ddlShiftDay As DropDownList = gvShow.Rows(i).Cells(8).FindControl("ddlShiftDay")
                        Dim tbOTLunch As TextBox = gvShow.Rows(i).Cells(9).FindControl("tbOTLunch")
                        Dim tbEndTime As TextBox = gvShow.Rows(i).Cells(10).FindControl("tbEndTime")
                        Dim rdlEndTime As RadioButtonList = gvShow.Rows(i).Cells(10).FindControl("rdlEndTime")
                        Dim ddlBusLine As DropDownList = gvShow.Rows(i).Cells(11).FindControl("ddlBusLine")
                        Dim cbDinner As CheckBox = gvShow.Rows(i).Cells(12).FindControl("cbDinner")
                        Dim cbOT As CheckBox = gvShow.Rows(i).Cells(13).FindControl("cbOT")

                        Dim AbsenceTime As Decimal = 0

                        If tbEndTime.Text.Trim = "__.__" Then
                            tbEndTime.Text = ""
                        End If

                        If tbAbsenceTime.Text = "" Then
                            AbsenceTime = 0
                        Else
                            AbsenceTime = CDec(tbAbsenceTime.Text.Trim)
                        End If

                        SQL = " select DateSat from NormalSatOT where DateSat = '" & tbOTDate.Text.Trim & "'"
                        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

                        If Program.Rows.Count > 0 Then
                            If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Then
                                fld.Add("OTStartTime", "16.00")
                                OTStart = dt & " 16:00"
                            ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Then
                                fld.Add("OTStartTime", "17.00")
                                OTStart = dt & " 17:00"
                            ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                fld.Add("OTStartTime", "04.00")
                                OTStart = DateAdd(DateInterval.Day, 1, dt) & " 04:00"
                            End If
                        Else '

                            ''Specail
                            'If tbOTDate.Text.Trim = "15/01/2016" Then

                            '    If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Then
                            '        fld.Add("OTStartTime", "16.00")
                            '        OTStart = dt & " 16:00"
                            '    ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Then
                            '        fld.Add("OTStartTime", "17.00")
                            '        OTStart = dt & " 17:00"
                            '    ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            '        fld.Add("OTStartTime", "00.00")
                            '        OTStart = DateAdd(DateInterval.Day, 1, dt) & " 00:00"
                            '    End If

                            'Else

                            'Nomal
                            If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Then
                                fld.Add("OTStartTime", "16.30")
                                OTStart = dt & " 16:30"
                            ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(7-17)" Then
                                fld.Add("OTStartTime", "17.00")
                                OTStart = dt & " 17:00"
                            ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Then
                                fld.Add("OTStartTime", "17.30")
                                OTStart = dt & " 17:30"
                            ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8-18)" Then
                                fld.Add("OTStartTime", "18.00")
                                OTStart = dt & " 18:00"
                            ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                fld.Add("OTStartTime", "04.30")
                                OTStart = DateAdd(DateInterval.Day, 1, dt) & " 04:30"
                            End If


                            'End If

                        End If

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
                            fld.Add("OTHours", "")
                            fld.Add("OTTotal", "")
                        Else

                            If rdlEndTime.SelectedItem.Text = "NO OT" Then
                                fld.Add("OTEndTime", "")
                            ElseIf rdlEndTime.SelectedItem.Text = "Other OT" Then
                                fld.Add("OTEndTime", tbEndTime.Text.Trim.Replace(":", ".").Replace("24", "00"))
                                If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                    OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                Else
                                    If cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                                        OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                    Else
                                        OTEnd = dt & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                    End If
                                End If
                            Else
                                fld.Add("OTEndTime", rdlEndTime.SelectedItem.Text.Trim)
                                If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                    OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & rdlEndTime.SelectedItem.Text.Trim.Replace(".", ":")
                                ElseIf cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                                    OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & rdlEndTime.SelectedItem.Text.Trim.Replace(".", ":")
                                Else
                                    OTEnd = dt & " " & rdlEndTime.SelectedItem.Text.Trim.Replace(".", ":")
                                End If
                            End If

                            fld.Add("BusLine", ddlBusLine.SelectedItem.Text.Trim.Substring(0, 2))
                            fld.Add("OTLunch", tbOTLunch.Text.Trim)

                            If cbDinner.Checked = True Then
                                fld.Add("Dinner", "1")
                            Else
                                fld.Add("Dinner", "")
                            End If

                            If rdlEndTime.SelectedItem.Text = "NO OT" Then
                                fld.Add("OTHours", "")
                                If tbOTLunch.Text.Trim <> "" Then
                                    fld.Add("OTTotal", tbOTLunch.Text.Trim)
                                Else
                                    fld.Add("OTTotal", "")
                                End If
                            Else
                                Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)

                                fld.Add("OTHours", Diff / 60.0)
                                Dim Lunch As Decimal = 0
                                If tbOTLunch.Text.Trim <> "" Then
                                    Lunch = CDec(tbOTLunch.Text.Trim)
                                End If
                                fld.Add("OTTotal", (Diff / 60.0) + Lunch)
                            End If

                        End If

                        fld.Add("AbsenceTime", tbAbsenceTime.Text.Trim)
                        fld.Add("ShiftDay", ddlShiftDay.SelectedItem.Text.Trim)
                        fld.Add("OTStartDate", tbOTDate.Text.Trim)
                        fld.Add("DateofOT", tbOTDate.Text.Trim)

                        If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                        ElseIf cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                            fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                        Else
                            fld.Add("OTEndDate", tbOTDate.Text.Trim)
                        End If

                        If cbAbsence.Checked = True Then
                            fld.Add("Absence", "Absence")
                        Else
                            fld.Add("Absence", "")
                        End If

                        If cbHoliday.Checked = True Then
                            fld.Add("Holiday", "Holiday")
                        Else
                            fld.Add("Holiday", "")
                        End If

                        Dim EmpNo = gvShow.Rows(i).Cells(3).Text.Trim.ToString

                        SQL = " select EmpNo as 'Emp No' from " & Table & " where EmpNo = '" & EmpNo & "' and DateofOT = '" & tbOTDate.Text.Trim & "'"
                        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

                        Dim act As String = "",
                            msg As String = "บันทึกสำเร็จ , Record Complete!!"

                        If Program.Rows.Count > 0 Then
                            If Program.Rows(0).Item("Emp No").ToString.Trim = EmpNo Then
                                act = "U"
                                whr.Add("EmpNo", gvShow.Rows(i).Cells(3).Text.Trim)
                                whr.Add("OTStartDate", tbOTDate.Text.Trim)
                                'If Session("UserGroup").ToString.Trim <> "SYS" Then
                                '    whr.Add("CreateBy", Session("UserName"))
                                'End If
                                fld.Add("ChangeBy", Session("UserName")) 'Create by
                                fld.Add("ChangeDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                            Else
                                act = "I"
                                fld.Add("DocNo", getDocNo())
                                fld.Add("CreateBy", Session("UserName")) 'Create by
                                fld.Add("CreateDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                            End If
                        Else
                            act = "I"
                            fld.Add("DocNo", getDocNo())
                            fld.Add("CreateBy", Session("UserName")) 'Create by
                            fld.Add("CreateDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                        End If

                        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(Table, fld, whr, act), Conn_SQL.MIS_ConnectionString)
                        show_message.ShowMessage(Page, msg, UpdatePanel1)
                    Next
                End If
            End If

            ' Work
            For i As Integer = 0 To gvWork.Rows.Count - 1

                Dim cbHoliday As CheckBox = gvWork.Rows(i).Cells(5).FindControl("cbHolidayWork")
                Dim cbWork As CheckBox = gvWork.Rows(i).Cells(6).FindControl("cbWork")
                Dim ddlShiftDay As DropDownList = gvWork.Rows(i).Cells(7).FindControl("ddlShiftDayWork")
                Dim tbOTLunch As TextBox = gvWork.Rows(i).Cells(8).FindControl("tbOTLunchWork")
                Dim tbEndTime As TextBox = gvWork.Rows(i).Cells(9).FindControl("tbEndTimeWork")
                Dim rdlEndTime As RadioButtonList = gvWork.Rows(i).Cells(9).FindControl("rdlEndTimeWork")
                Dim ddlBusLine As DropDownList = gvWork.Rows(i).Cells(10).FindControl("ddlBusLineWork")
                Dim cbDinner As CheckBox = gvWork.Rows(i).Cells(11).FindControl("cbDinnerWork")
                Dim cbOT As CheckBox = gvWork.Rows(i).Cells(12).FindControl("cbOTWork")

                Dim fld As Hashtable = New Hashtable
                Dim whr As Hashtable = New Hashtable

                If tbEndTime.Text.Trim = "__.__" Then
                    tbEndTime.Text = ""
                End If

                If cbWork.Checked = True Or cbHoliday.Checked = True Then

                    fld.Add("Shift", gvWork.Rows(i).Cells(0).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
                    fld.Add("Dept", gvWork.Rows(i).Cells(1).Text.Trim.Substring(0, 3))
                    fld.Add("Line", gvWork.Rows(i).Cells(2).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
                    fld.Add("EmpNo", gvWork.Rows(i).Cells(3).Text.Trim)
                    fld.Add("EmpName", gvWork.Rows(i).Cells(4).Text.Trim)

                    If cbHoliday.Checked = True Then
                        fld.Add("Holiday", "Holiday")
                        If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Or
                            ddlShiftDay.SelectedItem.Text.Trim = "Day(7-17)" Then
                            fld.Add("OTStartTime", "07.00")
                            OTStart = dt & " 07:00"
                        ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Or
                            ddlShiftDay.SelectedItem.Text.Trim = "Day(8-18)" Then
                            fld.Add("OTStartTime", "08.00")
                            OTStart = dt & " 08:00"
                        ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            fld.Add("OTStartTime", "19.00")
                            OTStart = dt & " 19:00"
                        End If
                    Else
                        fld.Add("Holiday", "")
                    End If

                    If cbOT.Checked = True Then
                        fld.Add("OTOver", "1")
                    Else
                        fld.Add("OTOver", "")
                    End If

                    If cbWork.Checked = True Then
                        fld.Add("Work", "Work")

                        SQL = " select DateSat from NormalSatOT where DateSat = '" & tbOTDate.Text.Trim & "'"
                        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

                        If Program.Rows.Count > 0 Then
                            If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Then
                                fld.Add("OTStartTime", "16.00")
                                OTStart = dt & " 16:00"
                            ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Then
                                fld.Add("OTStartTime", "17.00")
                                OTStart = dt & " 17:00"
                            ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                fld.Add("OTStartTime", "04.00")
                                OTStart = DateAdd(DateInterval.Day, 1, dt) & " 04:00"
                            End If
                        Else

                            'Specail
                            If tbOTDate.Text.Trim = "15/01/2016" Then

                                If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Then
                                    fld.Add("OTStartTime", "16.00")
                                    OTStart = dt & " 16:00"
                                ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Then
                                    fld.Add("OTStartTime", "17.00")
                                    OTStart = dt & " 17:00"
                                ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                    fld.Add("OTStartTime", "00.00")
                                    OTStart = DateAdd(DateInterval.Day, 1, dt) & " 00:00"
                                End If

                            Else
                                'Nomal
                                If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Then
                                    fld.Add("OTStartTime", "16.30")
                                    OTStart = dt & " 16:30"
                                ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Then
                                    fld.Add("OTStartTime", "17.30")
                                    OTStart = dt & " 17:30"
                                ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                                    fld.Add("OTStartTime", "04.30")
                                    OTStart = DateAdd(DateInterval.Day, 1, dt) & " 04:30"
                                End If

                            End If


                        End If

                    Else
                        fld.Add("Work", "")
                    End If

                    fld.Add("OTLunch", tbOTLunch.Text.Trim)

                    If cbDinner.Checked = True Then
                        fld.Add("Dinner", "1")
                    Else
                        fld.Add("Dinner", "")
                    End If

                    fld.Add("ShiftDay", ddlShiftDay.SelectedItem.Text.Trim)

                    If rdlEndTime.SelectedItem.Text = "NO OT" Then
                        fld.Add("OTEndTime", "")
                    ElseIf rdlEndTime.SelectedItem.Text = "Other OT" Then
                        fld.Add("OTEndTime", tbEndTime.Text.Trim.Replace(":", ".").Replace("24", "00"))
                        If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                        Else
                            If cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                                OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                            Else
                                OTEnd = dt & " " & tbEndTime.Text.Trim.Replace(".", ":")
                            End If
                        End If
                    Else
                        fld.Add("OTEndTime", rdlEndTime.SelectedItem.Text.Trim)
                        If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & rdlEndTime.SelectedItem.Text.Trim.Replace(".", ":")
                        Else
                            OTEnd = dt & " " & rdlEndTime.SelectedItem.Text.Trim.Replace(".", ":")
                        End If
                    End If

                    If cbHoliday.Checked = True Then

                        If tbEndTime.Text.Trim <> "" Then
                            If tbEndTime.Text.Trim.Substring(0, 2) = "12" Then
                                Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)
                                fld.Add("OTHours", Diff / 60.0)
                                Dim Lunch As Decimal = 0
                                If tbOTLunch.Text.Trim <> "" Then
                                    Lunch = CDec(tbOTLunch.Text.Trim)
                                End If
                                fld.Add("OTTotal", (Diff / 60.0) + Lunch)

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
                            Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)
                            fld.Add("OTHours", (Diff / 60.0) - 1.0)
                            Dim Lunch As Decimal = 0
                            If tbOTLunch.Text.Trim <> "" Then
                                Lunch = CDec(tbOTLunch.Text.Trim)
                            End If
                            fld.Add("OTTotal", ((Diff / 60.0) - 1.0) + Lunch)
                        End If

                    ElseIf cbWork.Checked = True Then

                        If rdlEndTime.SelectedItem.Text = "NO OT" Then
                            fld.Add("OTHours", "")
                            If tbOTLunch.Text.Trim <> "" Then
                                fld.Add("OTTotal", tbOTLunch.Text.Trim)
                            Else
                                fld.Add("OTTotal", "")
                            End If
                        Else
                            Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)

                            fld.Add("OTHours", Diff / 60.0)
                            Dim Lunch As Decimal = 0
                            If tbOTLunch.Text.Trim <> "" Then
                                Lunch = CDec(tbOTLunch.Text.Trim)
                            End If
                            fld.Add("OTTotal", (Diff / 60.0) + Lunch)
                        End If

                    End If

                    If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                        fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                    ElseIf cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                        fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                    Else
                        fld.Add("OTEndDate", tbOTDate.Text.Trim)
                    End If

                    fld.Add("BusLine", ddlBusLine.SelectedItem.Text.Trim.Substring(0, 2))
                    fld.Add("OTStartDate", tbOTDate.Text.Trim)
                    fld.Add("DateofOT", tbOTDate.Text.Trim)

                    Dim EmpNo = gvWork.Rows(i).Cells(3).Text.ToString
                    SQL = " select EmpNo as 'Emp No' from " & Table & " where EmpNo = '" & EmpNo & "' and DateofOT = '" & tbOTDate.Text.Trim & "'"
                    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

                    Dim act As String = "",
                        msg As String = "บันทึกสำเร็จ , Record Complete!!"

                    If Program.Rows.Count > 0 Then
                        If Program.Rows(0).Item("Emp No").ToString.Trim = EmpNo Then
                            act = "U"
                            whr.Add("EmpNo", gvWork.Rows(i).Cells(3).Text.Trim)
                            whr.Add("OTStartDate", tbOTDate.Text.Trim)
                            fld.Add("ChangeBy", Session("UserName")) 'Create by
                            fld.Add("ChangeDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                        End If
                    Else
                        act = "I"
                        fld.Add("DocNo", getDocNo())
                        fld.Add("CreateBy", Session("UserName")) 'Create by
                        fld.Add("CreateDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                    End If

                    SQL = " select isnull(CMSMV.UDF02,'') as 'Shift', " &
                " CMSMV.MV001  as 'Emp. No.', Rtrim(CMSMV.MV004)+'-'+CMSME.ME002  as 'Dept.', CMSMV.MV047 as 'Name', " &
                " isnull((Rtrim(C.Code)+'-'+C.Name),'')  as 'BusLine' , CMSMV.UDF04 as 'Line'  " &
                " from CMSMV " &
                " left join CMSME on CMSME.ME001 = CMSMV.MV004  " &
                " left join HOOTHAI_REPORT.dbo.CodeInfo C on C.Code = SUBSTRING (CMSMV.UDF01,1,2) " &
                " where CMSMV.UDF03 like '%Normal%' and CMSMV.MV001 ='" & EmpNo & "'" &
                " order by CMSMV.MV004,CMSMV.UDF04,CMSMV.MV001,CMSMV.UDF02 "
                    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                    For j As Integer = 0 To Program.Rows.Count - 1
                        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(Table, fld, whr, act), Conn_SQL.MIS_ConnectionString)
                        show_message.ShowMessage(Page, msg, UpdatePanel1)
                        seq += 1
                    Next
                End If
            Next

            'save data to kiosk
            'updateToKiosk()
            ShowReport()

            gvShow.Visible = False
            lbSelect.Visible = False
            gvEdit.Visible = False
            gvWork.Visible = False
            lbOther.Visible = False
            btExport.Visible = True
            btExcel.Visible = True
            btSave.Visible = False
            gvShowRe.Visible = True
            btCancel.Visible = False
            'btEdit.Visible = True
            btUpdate.Visible = False
            btShowRe.Visible = True

        End If
        System.Threading.Thread.Sleep(1000)
    End Sub

    'Sub updateToKiosk()
    '    Dim whr As String = "",
    '        sql As String

    '    sql = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
    '    Dim Dept As String = Conn_SQL.Get_value(sql, Conn_SQL.MIS_ConnectionString).Replace(",", "','")
    '    whr &= " ShiftDay like '%" & rdlShift.SelectedItem.Text & "%' "
    '    whr &= Conn_SQL.Where("Dept", cblDept)
    '    whr &= Conn_SQL.Where("Shift", cblShift)
    '    whr &= Conn_SQL.Where("OTStartDate", tbOTDate, False)
    '    whr &= Conn_SQL.Where("EmpNo", tbEmpNo)

    '    Dim UpdateOTRecord As New UpdateOTRecord
    '    UpdateOTRecord.moveToKiosk(whr)

    'End Sub

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

        For i As Integer = 0 To gvEdit.Rows.Count - 1
            Dim tbEndTime As TextBox = gvEdit.Rows(i).Cells(14).FindControl("tbEndTimeEdit")
            Dim cbHoliday As CheckBox = gvEdit.Rows(i).Cells(7).FindControl("cbHolidayEdit")
            Dim cbAbsence As CheckBox = gvEdit.Rows(i).Cells(0).FindControl("cbAbsenceEdit")
            Dim tbAbsenceTime As TextBox = gvEdit.Rows(i).Cells(1).FindControl("tbAbsenceTimeEdit")
            Dim cntTxt As Integer = tbEndTime.Text.Trim.Length

            If tbEndTime.Text.Trim = "__.__" Then
                tbEndTime.Text = ""
            End If

            If tbEndTime.Text.Trim <> "" And cntTxt <> 5 Then
                EndTime = EndTime + 1
            End If
            If cbHoliday.Checked = True And tbEndTime.Text.Trim = "" Then
                Holiday = Holiday + 1
            End If
            If cbHoliday.Checked = True And cbAbsence.Checked = True Or cbHoliday.Checked = True And tbAbsenceTime.Text.Trim <> "" Then
                AbsenHol = AbsenHol + 1
            End If

            If cbAbsence.Checked = True And tbAbsenceTime.Text.Trim <> "" Or cbAbsence.Checked = False And tbAbsenceTime.Text.Trim = "" Then
            Else
                Absence = Absence + 1
            End If

            If tbAbsenceTime.Text.Trim <> "" Then
                If cbAbsence.Checked = True And tbAbsenceTime.Text.Trim.Substring(0, 1) = "8" And tbEndTime.Text.Trim <> "" Then
                    chkAbsTime = chkAbsTime + 1
                End If
            End If

            Dim EmpNo = gvEdit.Rows(i).Cells(5).Text.Trim.ToString

            Dim Sql As String = " select rtrim(OTStartTime) as 'OTStart' from " & Table & " where EmpNo = '" & EmpNo & "' and DateofOT = '" & tbOTDate.Text.Trim & "'"
            Dim Program As Data.DataTable = Conn_SQL.Get_DataReader(Sql, Conn_SQL.MIS_ConnectionString)

            If Program.Rows(0).Item("OTStart") = tbEndTime.Text.Trim Then
                Check = Check + 1
            End If

        Next

        If EndTime > 0 Or Holiday > 0 Or AbsenHol > 0 Or Check > 0 Or Absence > 0 Or chkAbsTime > 0 Then
            If EndTime > 0 Then
                show_message.ShowMessage(Page, "ใส่รูปแบบเวลา OT End Time ให้ถูกต้อง (รูปแบบเวลา HH.mm)", UpdatePanel1)
            ElseIf AbsenHol > 0 Then
                show_message.ShowMessage(Page, "กรณี Holiday ไม่สามารถเลือก Absence หรือระบุ Absence Time ได้ ถ้าต้องการยกเลิกให้เอาเครื่องหมายถูกที่ช่อง Holiday ออก", UpdatePanel1)
            ElseIf Holiday > 0 Then
                show_message.ShowMessage(Page, "กรณี Holiday ระบุเวลาออก OT End Time ให้ถูกต้อง รูปแบบเวลา HH.mm ถ้าต้องการยกเลิกให้เอาเครื่องหมายถูกที่ช่อง Holiday ออก", UpdatePanel1)
            ElseIf Check > 0 Then
                show_message.ShowMessage(Page, "กรณีแก้ไขไม่ทำ OT ลบข้อมูลช่อง OT End Time ให้ว่าง", UpdatePanel1)
            ElseIf chkAbsTime > 0 Then
                show_message.ShowMessage(Page, "กรณีลางานไม่สามารถระบุช่อง OT End Time ", UpdatePanel1)
            ElseIf Absence > 0 Then
                show_message.ShowMessage(Page, "กรณีลางานเลือกช่อง Absence โปรดระบุช่อง AbsenceTime ", UpdatePanel1)
            End If
        Else

            Dim SQL As String = ""
            Dim Program As New Data.DataTable

            Dim setDate = Server.HtmlEncode(tbOTDate.Text)
            Dim dt As DateTime = DateTime.ParseExact(setDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            Dim getDate = dt.ToString("yyyyMMdd")

            tbDateFrom.Text = getDate & " " & Date.Now.ToString("HH:mm").ToString
            tbDateTo.Text = Date.Now.ToString("yyyyMMdd").ToString & " 14:00"

            Dim cnt As Decimal = 0

            For i As Integer = 0 To gvEdit.Rows.Count - 1
                Dim cbHoliday As CheckBox = gvEdit.Rows(i).Cells(7).FindControl("cbHolidayEdit")
                If cbHoliday.Checked = True Then
                    cnt = cnt + 1
                End If
            Next

            If cnt > 0 Or cnt = 0 And gvEdit.Rows(0).Cells(8).Text = "Holiday" Then

                For i As Integer = 0 To gvEdit.Rows.Count - 1

                    Dim cbAbsence As CheckBox = gvEdit.Rows(i).Cells(0).FindControl("cbAbsenceEdit")
                    Dim tbAbsenceTime As TextBox = gvEdit.Rows(i).Cells(1).FindControl("tbAbsenceTimeEdit")
                    Dim cbHoliday As CheckBox = gvEdit.Rows(i).Cells(7).FindControl("cbHolidayEdit")
                    Dim ddlShiftDay As DropDownList = gvEdit.Rows(i).Cells(9).FindControl("ddlShiftDayEdit")
                    Dim tbOTLunch As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbOTLunchEdit")
                    Dim tbEndTime As TextBox = gvEdit.Rows(i).Cells(14).FindControl("tbEndTimeEdit")
                    Dim ddlBusLine As DropDownList = gvEdit.Rows(i).Cells(16).FindControl("ddlBusLineEdit")
                    Dim cbDinner As CheckBox = gvEdit.Rows(i).Cells(17).FindControl("cbDinnerEdit")
                    Dim cbOT As CheckBox = gvEdit.Rows(i).Cells(18).FindControl("cbOTEdit")

                    Dim fld As Hashtable = New Hashtable
                    Dim whr As Hashtable = New Hashtable

                    If tbEndTime.Text.Trim = "__.__" Then
                        tbEndTime.Text = ""
                    End If

                    fld.Add("ChangeBy", Session("UserName")) 'Create by
                    fld.Add("ShiftDay", ddlShiftDay.SelectedItem.Text.Trim)

                    Dim AbsenceTime As Decimal = 0

                    If tbAbsenceTime.Text.Trim = "" Then
                        AbsenceTime = 0
                    Else
                        AbsenceTime = CDec(tbAbsenceTime.Text.Trim)
                    End If

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
                        fld.Add("BusLine", ddlBusLine.SelectedItem.Text.Trim.Substring(0, 2).Replace("se", "-"))
                        fld.Add("OTLunch", tbOTLunch.Text.Trim)

                        If cbDinner.Checked = True Then
                            fld.Add("Dinner", "1")
                        Else
                            fld.Add("Dinner", "")
                        End If

                    End If

                    fld.Add("AbsenceTime", tbAbsenceTime.Text.Trim)
                    fld.Add("ChangeDate", Date.Now.ToString("yyyyMMdd HH:mm"))

                    If cbAbsence.Checked = True Then
                        fld.Add("Absence", "Absence")
                    Else
                        fld.Add("Absence", "")
                    End If


                    Dim EmpNo = gvEdit.Rows(i).Cells(5).Text.Trim.ToString

                    SQL = " select EmpNo as 'Emp No' from " & Table & " where EmpNo = '" & EmpNo & "' and DateofOT = '" & tbOTDate.Text.Trim & "'"
                    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

                    Dim msg As String = "แก้ไขสำเร็จ , Update Complete!!"

                    If Program.Rows(0).Item("Emp No").ToString.Trim = EmpNo Then
                        whr.Add("EmpNo", gvEdit.Rows(i).Cells(5).Text.Trim)
                        whr.Add("OTStartDate", tbOTDate.Text.Trim)
                    End If

                    If cbHoliday.Checked = True And cbHoliday.Checked = True And tbEndTime.Text <> "" And cbAbsence.Checked = False And tbAbsenceTime.Text.Trim = "" Then
                        fld.Add("Holiday", "Holiday")

                        If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Or
                            ddlShiftDay.SelectedItem.Text.Trim = "Day(7-17)" Then
                            fld.Add("OTStartTime", "07.00")
                            OTStart = dt & " 07:00"
                        ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Or
                            ddlShiftDay.SelectedItem.Text.Trim = "Day(8-18)" Then
                            fld.Add("OTStartTime", "08.00")
                            OTStart = dt & " 08:00"
                        ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            fld.Add("OTStartTime", "19.00")
                            OTStart = dt & " 19:00"
                        End If

                        If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                            fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                        Else
                            If cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                                OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                            Else
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

                        Else
                            Diff = DateDiff(DateInterval.Minute, OTStart, OTEnd)

                            fld.Add("OTHours", (Diff / 60.0) - 1.0)
                            Dim Lunch As Decimal = 0
                            If tbOTLunch.Text.Trim <> "" Then
                                Lunch = CDec(tbOTLunch.Text.Trim)
                            End If
                            fld.Add("OTTotal", ((Diff / 60.0) - 1.0) + Lunch)
                        End If


                        Dim act As String = "U"
                        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(Table, fld, whr, act), Conn_SQL.MIS_ConnectionString)
                        show_message.ShowMessage(Page, msg, UpdatePanel1)

                    Else

                        Dim act As String = "D"
                        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(Table, fld, whr, act), Conn_SQL.MIS_ConnectionString)
                        show_message.ShowMessage(Page, msg, UpdatePanel1)
                    End If
                    gvShowRe.Visible = True
                Next

            Else

                For i As Integer = 0 To gvEdit.Rows.Count - 1

                    Dim cbAbsence As CheckBox = gvEdit.Rows(i).Cells(0).FindControl("cbAbsenceEdit")
                    Dim tbAbsenceTime As TextBox = gvEdit.Rows(i).Cells(1).FindControl("tbAbsenceTimeEdit")
                    Dim cbHoliday As CheckBox = gvEdit.Rows(i).Cells(7).FindControl("cbHolidayEdit")
                    Dim ddlShiftDay As DropDownList = gvEdit.Rows(i).Cells(9).FindControl("ddlShiftDayEdit")
                    Dim tbOTLunch As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbOTLunchEdit")
                    Dim tbEndTime As TextBox = gvEdit.Rows(i).Cells(14).FindControl("tbEndTimeEdit")
                    Dim ddlBusLine As DropDownList = gvEdit.Rows(i).Cells(16).FindControl("ddlBusLineEdit")
                    Dim cbDinner As CheckBox = gvEdit.Rows(i).Cells(17).FindControl("cbDinnerEdit")
                    Dim cbOT As CheckBox = gvEdit.Rows(i).Cells(18).FindControl("cbOTEdit")

                    Dim fld As Hashtable = New Hashtable
                    Dim whr As Hashtable = New Hashtable

                    If tbEndTime.Text.Trim = "__.__" Then
                        tbEndTime.Text = ""
                    End If

                    fld.Add("ChangeBy", Session("UserName")) 'Create by
                    fld.Add("ShiftDay", ddlShiftDay.SelectedItem.Text.Trim)

                    Dim AbsenceTime As Decimal = 0

                    If tbAbsenceTime.Text.Trim = "" Then
                        AbsenceTime = 0
                    Else
                        AbsenceTime = CDec(tbAbsenceTime.Text.Trim)
                    End If

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
                        fld.Add("BusLine", ddlBusLine.SelectedItem.Text.Trim.Substring(0, 2).Replace("se", "-"))
                        fld.Add("OTLunch", tbOTLunch.Text.Trim)

                        If cbDinner.Checked = True Then
                            fld.Add("Dinner", "1")
                        Else
                            fld.Add("Dinner", "")
                        End If

                    End If

                    fld.Add("AbsenceTime", tbAbsenceTime.Text.Trim)
                    fld.Add("ChangeDate", Date.Now.ToString("yyyyMMdd HH:mm"))

                    If cbAbsence.Checked = True Then
                        fld.Add("Absence", "Absence")
                    Else
                        fld.Add("Absence", "")
                    End If

                    If cbHoliday.Checked Then
                        fld.Add("Holiday", "Holiday")
                        Dim shiftDayVal As String = ddlShiftDay.SelectedItem.Text.Trim
                        If shiftDayVal = "Day(7)" Or shiftDayVal = "Day(7-17)" Then
                            fld.Add("OTStartTime", "07.00")
                            OTStart = dt & " 07:00"
                        ElseIf shiftDayVal = "Day(8)" Or shiftDayVal = "Day(8-18)" Then
                            fld.Add("OTStartTime", "08.00")
                            OTStart = dt & " 08:00"
                        ElseIf shiftDayVal = "Night" Then
                            fld.Add("OTStartTime", "19.00")
                            OTStart = dt & " 19:00"
                        End If

                        If shiftDayVal = "Night" Then
                            OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                            fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                        Else
                            If cbOT.Checked And shiftDayVal <> "Night" Then
                                OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                            Else
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
                        Dim shiftDayVal As String = ddlShiftDay.SelectedItem.Text.Trim
                        If Program.Rows.Count > 0 Then
                            If shiftDayVal = "Day(7)" Or shiftDayVal = "Day(7-17)" Then
                                fld.Add("OTStartTime", "16.00")
                                OTStart = dt & " 16:00"
                            ElseIf shiftDayVal = "Day(8)" Or shiftDayVal = "Day(8-18)" Then
                                fld.Add("OTStartTime", "17.00")
                                OTStart = dt & " 17:00"
                            ElseIf shiftDayVal = "Night" Then
                                fld.Add("OTStartTime", "04.00")
                                OTStart = DateAdd(DateInterval.Day, 1, dt) & " 04:00"
                            End If
                        Else

                            'Specail
                            'If tbOTDate.Text.Trim = "15/01/2016" Then

                            '    If ddlShiftDay.SelectedItem.Text.Trim = "Day(7)" Then
                            '        fld.Add("OTStartTime", "16.00")
                            '        OTStart = dt & " 16:00"
                            '    ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Day(8)" Then
                            '        fld.Add("OTStartTime", "17.00")
                            '        OTStart = dt & " 17:00"
                            '    ElseIf ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            '        fld.Add("OTStartTime", "00.00")
                            '        OTStart = DateAdd(DateInterval.Day, 1, dt) & " 00:00"
                            '    End If

                            'Else
                            'Nomal

                            If shiftDayVal = "Day(7)" Then
                                fld.Add("OTStartTime", "16.30")
                                OTStart = dt & " 16:30"
                            ElseIf shiftDayVal = "Day(7-17)" Then
                                fld.Add("OTStartTime", "17.00")
                                OTStart = dt & " 17:00"
                            ElseIf shiftDayVal = "Day(8)" Then
                                fld.Add("OTStartTime", "17.30")
                                OTStart = dt & " 17:30"
                            ElseIf shiftDayVal = "Day(8-18)" Then
                                fld.Add("OTStartTime", "18.00")
                                OTStart = dt & " 18:00"
                            ElseIf shiftDayVal = "Night" Then
                                fld.Add("OTStartTime", "04.30")
                                OTStart = DateAdd(DateInterval.Day, 1, dt) & " 04:30"
                            End If
                            'End If
                        End If

                        If tbEndTime.Text.Trim = "" Then
                            OTStart = dt
                            OTEnd = dt
                        End If

                        If ddlShiftDay.SelectedItem.Text.Trim = "Night" Then
                            OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                            fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                        Else
                            '  If tbEndTime.Text <> "" Then

                            If cbOT.Checked = True And ddlShiftDay.SelectedItem.Text.Trim <> "Night" Then
                                OTEnd = DateAdd(DateInterval.Day, 1, dt) & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                fld.Add("OTEndDate", DateAdd(DateInterval.Day, 1, dt).ToString("dd/MM/yyyy"))
                            Else
                                OTEnd = dt & " " & tbEndTime.Text.Trim.Replace(".", ":")
                                fld.Add("OTEndDate", tbOTDate.Text.Trim)
                            End If

                        End If

                        ' End If

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

                    gvShowRe.Visible = True
                Next

            End If

            'save data to kiosk
            'updateToKiosk()
            ShowReport()

            btSave.Visible = False
            btExport.Visible = False
            btExcel.Visible = False
            btShowRe.Visible = True
            btRecord.Visible = True
            gvEdit.Visible = False
            gvEdit.Visible = False
            btUpdate.Visible = False
            btCancel.Visible = False
            btEdit.Visible = False
            btRecord.Visible = True

            ChangeDate()
            SetButton()

        End If
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btRecord_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRecord.Click

        Dim WHR As String = "",
           SQL As String = "",
       type As String = "",
       cnt As Decimal = 0,
       typeAll As String = ""

        Dim aSQL As String = " select Rtrim(Dept) from UserOTRecord where Id='" & Session("UserId") & "' "
        Dim aDept As String = Conn_SQL.Get_value(aSQL, Conn_SQL.MIS_ConnectionString)
        '.Replace(",", "','")

        WHR &= Conn_SQL.Where("isnull(CMSMV.MV004,'')", cblDept, False, aDept)

        Dim shift As String = ""

        WHR &= Conn_SQL.Where("CMSMV.UDF02", cblShift)
        WHR &= Conn_SQL.Where("CMSMV.MV001", tbEmpNo)
        WHR &= returnWhereShiftDay("CMSMV.UDF06")

        SQL = " select isnull(CMSMV.UDF02,'') as 'Shift', " &
             " CMSMV.MV001  as 'EmpNo', Rtrim(CMSMV.MV004)+'-'+CMSME.ME002  as 'Dept.', CMSMV.MV047 as 'Name', " &
             " isnull((Rtrim(C.Code)+'-'+C.Name),'')  as 'BusLine', CMSMV.UDF04 as 'Line',CMSMV.UDF06 as 'ShiftDay' " &
             " from CMSMV " &
             " left join CMSME on CMSME.ME001 = CMSMV.MV004  " &
             " left join HOOTHAI_REPORT.dbo.CodeInfo C on C.Code = SUBSTRING (CMSMV.UDF01,1,2) and CodeType = 'BusLine' " &
             " where CMSMV.UDF03 like '%Normal%' " & WHR &
             " order by CMSMV.MV004,CMSMV.UDF04,CMSMV.MV001,CMSMV.UDF02 "

        Dim SQLselect As String = SQL.Replace("and CMSMV.UDF02  in", "and CMSMV.UDF02 not in")
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        ControlForm.ShowGridView(gvWork, SQLselect, Conn_SQL.ERP_ConnectionString)

        For Each boxItem As ListItem In cblShift.Items
            Dim boxVal As String = CStr(boxItem.Value.Trim)
            If boxItem.Selected Then
                cnt = cnt + 1
            End If
        Next

        If cnt = 0 Then
            gvWork.Visible = False
            lbOther.Visible = False
        Else
            gvWork.Visible = True
            lbOther.Visible = True
        End If

        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)

        btSave.Visible = True
        gvShowRe.Visible = False
        gvShow.Visible = True
        lbSelect.Visible = True
        gvEdit.Visible = False
        btExport.Visible = False
        btExcel.Visible = False
        btCancel.Visible = True
        btEdit.Visible = False
        btUpdate.Visible = False
        btShowRe.Visible = False

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollWork", "gridviewScrollWork();", True)

        ChangeDate()
        System.Threading.Thread.Sleep(1000)
    End Sub

    Function returnWhereShiftDay(fldName As String) As String
        Dim areaVal As String = ddlArea.Text.Trim
        Dim valShiftDay As String = "Day(7-17),Day(8-18)"
        Dim notText As String = ""
        If areaVal = "1" Then 'production
            notText = " not "
        End If
        Return Conn_SQL.Where(fldName, notText & " in ('" & valShiftDay.Replace(",", "','") & "')")
    End Function

    Protected Sub btEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btEdit.Click


        Dim WHR As String = "",
            SQL As String = ""

        WHR &= Conn_SQL.Where("Dept", cblDept)
        WHR &= Conn_SQL.Where("Shift", cblShift)
        WHR &= Conn_SQL.Where("OTStartDate", tbOTDate)
        WHR &= " and ShiftDay like '%" & rdlShift.SelectedItem.Text & "%'"

        WHR &= Conn_SQL.Where("EmpNo", tbEmpNo)
        WHR &= returnWhereShiftDay("ShiftDay")

        SQL = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
        Dim Dept As String = Conn_SQL.Get_value(SQL, Conn_SQL.MIS_ConnectionString).Replace(",", "','")

        SQL = "select rtrim(Absence) as 'Absence', AbsenceTime , Shift ,EmpNo,rtrim(Dept)+'-'+CMSME.ME002 as 'Dept' ,EmpName , " &
              " case when Holiday = '' then 'Normal working day' else 'Holiday' end as 'OT Type' , rtrim(ShiftDay) as 'ShiftDay' , OTLunch, OTStartDate  ,  " &
              " case when OTEndTime <> '' then OTStartTime else '' end as 'Start Time', " &
              " case when OTEndTime <> '' then OTEndDate else '' end as 'End Date', " &
              " OTEndTime , case when OTEndTime <> '' then DateofOT else '' end as DateofOT , Rtrim(BusLine) +' : '+ Name as 'BusLine' , Line as 'Line', Dinner as 'Dinner' ,OTOver as 'OT Over Date'" &
              " from OTRecord" &
              " left join CodeInfo on Code  = BusLine and CodeType = 'BusLine' " &
              " left join HOOTHAI.dbo.CMSME on CMSME.ME001 = rtrim(Dept)  " &
              " where Dept in ('" & Dept & "') " & WHR & " order by Dept,Line,EmpNo,Shift "

        ControlForm.ShowGridView(gvEdit, SQL, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvEdit)
        System.Threading.Thread.Sleep(1000)

        btSave.Visible = False
        gvWork.Visible = False
        lbOther.Visible = False
        gvShow.Visible = False
        lbSelect.Visible = False
        gvShowRe.Visible = False
        btExport.Visible = False
        btExcel.Visible = False
        btShowRe.Visible = False
        btRecord.Visible = False
        gvEdit.Visible = True
        btUpdate.Visible = True
        btCancel.Visible = True
        btEdit.Visible = False

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollEdit", "gridviewScrollEdit();", True)

        ChangeDate()
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btShowRe_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShowRe.Click

        ShowReport()
        gvShowRe.Visible = True
        gvShow.Visible = False
        lbSelect.Visible = False
        gvWork.Visible = False
        lbOther.Visible = False
        gvEdit.Visible = False
        btExport.Visible = True
        btExcel.Visible = True
        btSave.Visible = False
        btCancel.Visible = False
        btEdit.Visible = True
        btUpdate.Visible = False
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCancel.Click

        btSave.Visible = False
        gvWork.Visible = False
        lbOther.Visible = False
        gvShow.Visible = False
        lbSelect.Visible = False
        gvEdit.Visible = False
        btExport.Visible = False
        btExcel.Visible = False
        btShowRe.Visible = True
        btCancel.Visible = False
        btEdit.Visible = False
        btUpdate.Visible = False
        btRecord.Visible = True
        ChangeDate()
        SetButton()
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click

        Dim sql As String = ""
        Dim CodeDept As String = ""
        Dim whr As String = ""
        Dim dt As DataTable

        sql = "select rtrim(Dept) from UserOTRecord where Id='" & Session("UserId") & "' "
        Dim Dept As String = Conn_SQL.Get_value(sql, Conn_SQL.MIS_ConnectionString)
        '.Replace(",", "','")

        CodeDept = Conn_SQL.Where("Dept", cblDept, False, Dept)
        whr = Conn_SQL.Where("CMSME.ME001", cblDept, False, Dept)
        Dim Shift As String = Conn_SQL.Where("Shift", cblShift)
        Dim DateOT As String = " and OTStartDate = '" & tbOTDate.Text.Trim & "'"

        sql = " select CMSME.ME002 A from CMSME where 1=1 " & whr
        dt = Conn_SQL.Get_DataReader(sql, Conn_SQL.ERP_ConnectionString)
        Dim StrDept As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            StrDept &= "  " & dt.Rows(i).Item("A").ToString.Trim
        Next

        Dim paraName As String = ""
        paraName = "Dept:" & CodeDept & chrConn & "OTDate:" & DateOT & chrConn & "ShiftDay:" & rdlShift.SelectedItem.Text & chrConn & "DeptName:" & StrDept & chrConn & "Shift:" & Shift
        Randomize()
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/WebForm2.aspx?height=150&width=350&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & Server.UrlEncode(chrConn) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=OTRecord.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & Server.UrlEncode(chrConn) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)

        ChangeDate()

    End Sub

    Protected Sub btExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcel.Click

        ControlForm.ExportGridViewToExcel("OTRecord" & Session("UserName"), gvShowRe)

        ChangeDate()

    End Sub

    ' ------------------------------  Function  ----------------------------------

    Private Sub SetButton()

        'Dim setDate = Server.HtmlEncode(tbOTDate.Text)
        'Dim dt As DateTime = DateTime.ParseExact(setDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
        'Dim getDate = dt.ToString("yyyyMMdd")

        'tbDateFrom.Text = getDate & " " & Date.Now.ToString("HH:mm").ToString
        'tbDateTo.Text = Date.Now.ToString("yyyyMMdd").ToString & " 14:00"

        'Dim setTimeShow As Boolean = False

        'If getDate >= Date.Now.ToString("yyyyMMdd").ToString Then
        '    If tbDateFrom.Text < tbDateTo.Text Then
        '        setTimeShow = True
        '    ElseIf getDate > Date.Now.ToString("yyyyMMdd").ToString Then
        '        setTimeShow = True
        '    End If
        'Else
        '    setTimeShow = False
        'End If

        'btRecord.Visible = setTimeShow

    End Sub

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

        'sql = "select Shift ,rtrim(Dept)+'-'+CMSME.ME002 as 'Dept' ,Line as 'Line' ,EmpNo , EmpName , " & _
        ' " case when Holiday = '' then 'Normal working day' else 'Holiday' end as 'OT Type' , rtrim(ShiftDay) as 'ShiftDay' , OTStartDate  , rtrim(Absence) as 'Absence', AbsenceTime ,  " & _
        ' " case when OTEndTime <> '' then OTStartTime else '' end as 'OT Start Time', " & _
        ' " case when OTEndTime <> '' and ShiftDay = 'Night' then CONVERT(VARCHAR(11),DATEADD(day, 1, SUBSTRING(OTStartDate,7,4)+'/'+SUBSTRING(OTStartDate,4,2)+'/'+SUBSTRING(OTStartDate,1,2)),103) when OTEndTime <> '' and ShiftDay like '%Day%' then OTStartDate else '' end as 'End Date', " & _
        ' " OTEndTime , case when OTEndTime <> '' then DateofOT else '' end as DateofOT  , " & _
        ' " case when OTEndTime <> '' and Holiday = '' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)),'-','')  " & _
        ' " when OTEndTime <> '12.00' and Holiday = 'Holiday' and ShiftDay like '%Day%'  then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)-1.00),'-','') " & _
        ' " when OTEndTime = '12.00' and Holiday = 'Holiday' and ShiftDay like '%Day%' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)),'-','') " & _
        ' " when OTEndTime = '07.00' and Holiday = 'Holiday' and ShiftDay = 'Night' then '11' " & _
        ' " when OTEndTime = '04.00' and Holiday = 'Holiday' and ShiftDay = 'Night' then '8' end as 'OT Hour', " & _
        ' " OTLunch as 'OT Lunch' , " & _
        ' " case when OTEndTime <> '' and OTLunch <> '' and Holiday = '' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)+OTLunch),'-','') " & _
        ' " when OTEndTime <> '' and OTLunch <> ''  and Holiday = 'Holiday' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)-1.00)+(OTLunch),'-','') " & _
        ' " when OTEndTime <> '' and Holiday = '' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)),'-','') " & _
        ' " when OTEndTime <> '12.00' and Holiday = 'Holiday'  and ShiftDay like '%Day%' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)-1),'-','') " & _
        ' " when OTEndTime = '12.00' and Holiday = 'Holiday'  and ShiftDay like '%Day%' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)),'-','') " & _
        ' " when OTLunch <> ''   then OTLunch  " & _
        ' " when OTEndTime = '07.00' and Holiday = 'Holiday' and ShiftDay = 'Night' then '11' " & _
        ' " when OTEndTime = '04.00' and Holiday = 'Holiday' and ShiftDay = 'Night' then '8' " & _
        ' " when OTEndTime = '07.00' and Holiday = 'Holiday' and ShiftDay = 'Night' and OTLunch <> '' then CONVERT(DECIMAL(5,1),(11+OTLunch)) " & _
        ' " when OTEndTime = '04.00' and Holiday = 'Holiday' and ShiftDay = 'Night' and OTLunch <> '' then CONVERT(DECIMAL(5,1),(8+OTLunch)) end as 'Total OT hours'," & _
        ' " Rtrim(BusLine) +' : '+ Name as 'BusLine' ,  Dinner as 'Dinner' , Remark, ChangeBy as 'Change By' , ChangeDate as'Change Date' " & _
        ' " from OTRecord" & _
        ' " left join CodeInfo on Code  = BusLine and CodeType = 'BusLine' " & _
        ' " left join JINPAO80.dbo.CMSME on CMSME.ME001 = rtrim(Dept)  " & _
        ' " where Dept in ('" & Dept & "') " & whr & " order by Dept,Line,EmpNo,Shift "


        sql = "select Shift ,rtrim(Dept)+'-'+CMSME.ME002 as 'Dept' ,Line as 'Line' ,EmpNo , EmpName , " &
       " case when Holiday = '' then 'Normal working day' else 'Holiday' end as 'OT Type' , rtrim(ShiftDay) as 'ShiftDay' , OTStartDate  , rtrim(Absence) as 'Absence', AbsenceTime ,  " &
       " case when OTEndTime <> '' then OTStartTime else '' end as 'OT Start Time', " &
       " case when OTEndTime <> '' then OTEndDate else '' end as 'End Date', " &
       " OTEndTime , case when OTEndTime <> '' then DateofOT else '' end as DateofOT  , " &
       " OTHours as 'OT hrs.', " &
       " OTLunch as 'OT Lunch' , " &
       " OTTotal as 'Total OT hrs.'," &
       " Rtrim(BusLine) +' : '+ Name as 'BusLine' ,  Dinner as 'Dinner' , Remark, ChangeBy as 'Change By' , ChangeDate as'Change Date' " &
       " from OTRecord" &
       " left join CodeInfo on Code  = BusLine and CodeType = 'BusLine' " &
       " left join HOOTHAI.dbo.CMSME on CMSME.ME001 = rtrim(Dept)  " &
       " where Dept in ('" & Dept & "') " & whr & " order by Dept,Line,EmpNo,Shift "




        ControlForm.ShowGridView(gvShowRe, sql, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShowRe)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShowRe", "gridviewScrollShowRe();", True)

    End Sub

    Private Sub ChangeDate()

        'Dim setDate = Server.HtmlEncode(tbOTDate.Text)
        'Dim dt As DateTime = DateTime.ParseExact(setDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
        'Dim getDate = dt.ToString("yyyyMMdd")

        'tbDateFrom.Text = getDate & " " & Date.Now.ToString("HH:mm").ToString
        'tbDateTo.Text = Date.Now.ToString("yyyyMMdd").ToString & " 14:00"

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

    Protected Sub tbOTDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tbOTDate.TextChanged

        'Dim setDate = Server.HtmlEncode(tbOTDate.Text)
        'Dim dt As DateTime = DateTime.ParseExact(setDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
        'Dim getDate = dt.ToString("yyyyMMdd")

        'tbDateFrom.Text = getDate & " " & Date.Now.ToString("HH:mm").ToString
        'tbDateTo.Text = Date.Now.ToString("yyyyMMdd").ToString & " 14:00"

        'Dim setTimeShow As Boolean = False

        'If getDate >= Date.Now.ToString("yyyyMMdd").ToString Then
        '    If tbDateFrom.Text < tbDateTo.Text Or getDate > Date.Now.ToString("yyyyMMdd").ToString Then
        '        setTimeShow = True
        '    Else
        '        btEdit.Visible = False
        '        btSave.Visible = False
        '    End If
        'Else
        '    btEdit.Visible = False
        '    btSave.Visible = False
        '    setTimeShow = False
        'End If
        'btRecord.Visible = setTimeShow

        'If getDate = Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString And rdlShift.SelectedIndex = 1 Then
        '    If tbDateFrom.Text < Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 10:00" And rdlShift.SelectedIndex = 1 Then
        '        btEdit.Visible = True
        '    ElseIf tbDateFrom.Text > Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 10:00" And rdlShift.SelectedIndex = 0 Then
        '        btEdit.Visible = False
        '    Else
        '        btEdit.Visible = False
        '    End If
        'Else
        '    btEdit.Visible = False
        'End If


    End Sub

    Protected Sub setDataToTDropdownlist(ByVal ddl As DropDownList, ByVal val As String)
        ddl.SelectedValue = val
    End Sub

    Protected Sub setDataToTrdlist(ByVal rdl As RadioButtonList, ByVal val As String)
        rdl.SelectedValue = val
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub rdlShift_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdlShift.SelectedIndexChanged


        'Dim setDate = Server.HtmlEncode(tbOTDate.Text)
        'Dim dt As DateTime = DateTime.ParseExact(setDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
        'Dim getDate = dt.ToString("yyyyMMdd")

        'tbDateFrom.Text = getDate & " " & Date.Now.ToString("HH:mm").ToString
        'tbDateTo.Text = Date.Now.ToString("yyyyMMdd").ToString & " 14:00"

        'If getDate = Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString And rdlShift.SelectedIndex = 1 Then
        '    If tbDateFrom.Text < Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 10:00" And rdlShift.SelectedIndex = 1 Then
        '        btEdit.Visible = True
        '    ElseIf tbDateFrom.Text > Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 10:00" And rdlShift.SelectedIndex = 0 Then
        '        btEdit.Visible = False
        '    Else
        '        btEdit.Visible = False
        '    End If
        'Else
        '    btEdit.Visible = False
        'End If

    End Sub

End Class