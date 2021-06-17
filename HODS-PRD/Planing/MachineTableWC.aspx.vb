Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Imports System.Data.SqlClient
Imports System.Collections.Generic
Public Class MachineTableWC
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim dbConn As New DataConnectControl
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    'Dim NowSelectMachine As String = ""
    Dim ThisMachineStatus As String
    Dim SelectDate As String
    'Dim WorkCenter As String
    'Dim MachineCode As String
    Dim Machine1ATime() As String = {"MachineA01", "MachineA02", "MachineA03", "MachineA04", "MachineA05", "MachineA06", "MachineA07", "MachineA08", "MachineA09", "MachineA10", "MachineA11", "MachineA12", "MachineA13", "MachineA14", "MachineA15", "MachineA16", "MachineA17", "MachineA18", "MachineA19"}
    Dim Machine1BTime() As String = {"MachineB01", "MachineB02", "MachineB03", "MachineB04", "MachineB05", "MachineB06", "MachineB07", "MachineB08", "MachineB09", "MachineB10", "MachineB11", "MachineB12", "MachineB13", "MachineB14", "MachineB15", "MachineB16", "MachineB17", "MachineB18", "MachineB19"}
    Dim Machine1CTime() As String = {"MachineC01", "MachineC02", "MachineC03", "MachineC04", "MachineC05", "MachineC06", "MachineC07", "MachineC08", "MachineC09", "MachineC10", "MachineC11", "MachineC12", "MachineC13", "MachineC14", "MachineC15", "MachineC16", "MachineC17", "MachineC18", "MachineC19", "MachineC20", "MachineC21", "MachineC22", "MachineC23", "MachineC24", "MachineC25", "MachineC26", "MachineC27", "MachineC28", "MachineC29"}
    Dim Machine1DTime() As String = {"MachineD01", "MachineD02", "MachineD03", "MachineD04", "MachineD05", "MachineD06", "MachineD07", "MachineD08", "MachineD09", "MachineD10", "MachineD11", "MachineD12", "MachineD13", "MachineD14", "MachineD15", "MachineD16", "MachineD17", "MachineD18", "MachineD19", "MachineD20", "MachineD21", "MachineD22", "MachineD23", "MachineD24"}
    'Dim Machine1ETime() As String = {"MachineE01", "MachineE02", "MachineE03", "MachineE04", "MachineE05", "MachineE06", "MachineE07", "MachineE08", "MachineE09", "MachineE10", "MachineE11", "MachineE12", "MachineE13", "MachineE14", "MachineE15", "MachineE16", "MachineE17", "MachineE18", "MachineE19"}
    'Dim Machine1FTime() As String = {"MachineF01", "MachineF02", "MachineF03", "MachineF04", "MachineF05", "MachineF06", "MachineF07", "MachineF08", "MachineF09", "MachineF10", "MachineF11", "MachineF12", "MachineF13", "MachineF14", "MachineF15", "MachineF16", "MachineF17", "MachineF18", "MachineF19"}
    'Dim Machine1GTime() As String = {"MachineG01", "MachineG02", "MachineG03", "MachineG04", "MachineG05", "MachineG06", "MachineG07", "MachineG08", "MachineG09", "MachineG10", "MachineG11", "MachineG12", "MachineG13", "MachineG14", "MachineG15", "MachineG16", "MachineG17", "MachineG18", "MachineG19"}
    'Dim Machine1HTime() As String = {"MachineH01", "MachineH02", "MachineH03", "MachineH04", "MachineH05", "MachineH06", "MachineH07", "MachineH08", "MachineH09", "MachineH10", "MachineH11", "MachineH12", "MachineH13", "MachineH14", "MachineH15", "MachineH16", "MachineH17", "MachineH18", "MachineH19"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SelectDate = Year(Now) & Month(Now).ToString.PadLeft(2, "0") & Day(Now).ToString.PadLeft(2, "0")

        If Not IsPostBack Then
            'Session("UserName") = "500026"
            'Session("UserId") = "500026"
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            InitNowMachineTable()
            InitAllMAchine()
            'initDDLMachineAllStatus()
        Else
            InitAllMAchine()
            'initDDLMachineAllStatus()
        End If
        'ShowGridview()
    End Sub

    Dim MachineAllStatus() As String
    Dim MachineAllStatus1() As String = {"", "Idle", "Wait Setup Mold", "", "", ""}
    Dim MachineAllStatus2() As String = {"", "Idle", "Wait Setup Mold", "Setting Mold", "", ""}
    Dim MachineAllStatus3() As String = {"", "Idle", "", "Setting Mold", "Wait Running", ""}
    Dim MachineAllStatus4() As String = {"", "Idle", "", "", "Wait Running", "Running"}
    Dim MachineAllStatus5() As String = {"", "Idle", "", "", "Wait Running", """Running"}
    Dim MachineAllStatus0() As String = {"", "Idle", "", "", "", ""}
    Private Sub initDDLMachineAllStatus()
        DDLMachineAllStatus.Items.Clear()
        'Dim MachineStatus As New ListItem
        'MachineStatus.Value = "0"
        'MachineStatus.Text = "Select"
        'DDLMachineAllStatus.Items.Add(MachineStatus)

        Select Case LBNowSelectMachineStatus.Text
            Case 1
                MachineAllStatus = MachineAllStatus1
            Case 2
                MachineAllStatus = MachineAllStatus2
            Case 3
                MachineAllStatus = MachineAllStatus3
            Case 4
                MachineAllStatus = MachineAllStatus4
            Case 5
                MachineAllStatus = MachineAllStatus5
            Case Else
                MachineAllStatus = MachineAllStatus0
        End Select


        For i = 1 To MachineAllStatus.Length - 1
            Dim MachineStatus1 As New ListItem
            MachineStatus1.Value = i
            MachineStatus1.Text = MachineAllStatus(i)
            If MachineAllStatus(i) <> "" Then
                DDLMachineAllStatus.Items.Add(MachineStatus1)
            End If
        Next
        If LBNowSelectMachineStatus.Text = 1 Or LBNowSelectMachineStatus.Text = 99 Then
            Dim MachineStatus2 As New ListItem
            MachineStatus2.Value = "99"
            MachineStatus2.Text = "Break"
            DDLMachineAllStatus.Items.Add(MachineStatus2)
        End If

        DDLMachineAllStatus.SelectedValue = LBNowSelectMachineStatus.Text
    End Sub


    Private Sub InitNowMachineTable()
        Dim SQL As String = "select * from NowMachine where NowDate='" & SelectDate & "'"
        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        If dt2.Rows.Count = 0 Then
            'For i = 1 To 8
            '    Dim ThisMachine() As String
            '    Select Case i
            '        Case "1"
            '            ThisMachine = Machine1ATime
            '        Case "2"
            '            ThisMachine = Machine1BTime
            '        Case "3"
            '            ThisMachine = Machine1CTime
            '        Case "4"
            '            ThisMachine = Machine1DTime
            '        Case "5"
            '            ThisMachine = Machine1ETime
            '        Case "6"
            '            ThisMachine = Machine1FTime
            '        Case "7"
            '            ThisMachine = Machine1GTime
            '        Case "8"
            '            ThisMachine = Machine1HTime
            '        Case Else
            '            ThisMachine = Machine1ATime
            '    End Select
            '    For j = 0 To 12
            '        Dim Yesterday As Date = DateAdd("d", -1, Now)
            '        Dim YeaterdayDate As String = Year(Yesterday) & Month(Yesterday).ToString.PadLeft(2, "0") & Day(Yesterday).ToString.PadLeft(2, "0")
            '        Dim strSql As String = ""
            '        Dim MachineID As String = Right(ThisMachine(j), 3)
            '        strSql = "insert into NowMachine (NowDate,MachineID,NowStatus) values('" &
            '            SelectDate & "','" & MachineID & "','1')"
            '        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
            '    Next
            'Next
            'Dim Yesterday As Date = DateAdd("d", -1, Now)
            'Dim YeaterdayDate As String = Year(Yesterday) & Month(Yesterday).ToString.PadLeft(2, "0") & Day(Yesterday).ToString.PadLeft(2, "0")
            Dim strSql As String = ""

            strSql = "insert into NowMachine " &
            "Select '" & SelectDate & "',MachineID,NowStatus,MONo,PlanFinishTime,EmpCode from NowMachine  where NowDate in " &
            "(select top 1 NowDate from NowMachine order by NowDate desc)"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
            strSql = "insert into MOWorkRecord " &
                "Select RID,'" & SelectDate & "',MONo,MachineID,StartTime,EndTime,Qty,EmpCode,WorkType,StartDate,EndDate,Note from MOWorkRecord  where EndTime is Null and NowDate in " &
            "(select top 1 NowDate from MOWorkRecord order by NowDate desc)"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        End If

    End Sub
    Private Sub InitAllMAchine()
        For i = 1 To 4
            Dim ThisMachine() As String
            Select Case i
                Case "1"
                    ThisMachine = Machine1ATime
                Case "2"
                    ThisMachine = Machine1BTime
                Case "3"
                    ThisMachine = Machine1CTime
                Case "4"
                    ThisMachine = Machine1DTime
                    'Case "5"
                    '    ThisMachine = Machine1ETime
                    'Case "6"
                    '    ThisMachine = Machine1FTime
                    'Case "7"
                    '    ThisMachine = Machine1GTime
                    'Case "8"
                    '    ThisMachine = Machine1HTime
                Case Else
                    ThisMachine = Machine1ATime
            End Select
            For j = 0 To 28
                If j > ThisMachine.Length - 1 Then
                    Exit For
                End If
                Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(j)) ')
                If Not NowShowTimeButton Is Nothing Then

                    Dim strSql1 As String = "select MX003,MX006,MX001 from CMSMX where MX006='" & Right(ThisMachine(j), 3) & "'"
                    Dim dt5 As DataTable = dbConn.Query(strSql1, VarIni.ERP, dbConn.WhoCalledMe)
                    Dim strThisMachine As String = dt5.Rows(0).Item("MX001")
                    NowShowTimeButton.Width = 60
                    If dt5.Rows.Count > 0 Then
                        NowShowTimeButton.Text = Trim(dt5.Rows(0).Item(1)) & "-" & Trim(dt5.Rows(0).Item(0))
                    Else
                        NowShowTimeButton.Text = "--"
                        NowShowTimeButton.Enabled = False
                    End If
                    'Select Case Trim(NowShowTimeButton.Text)
                    '    Case "200T"
                    '        NowShowTimeButton.ForeColor = Drawing.Color.Red
                    '    Case "160T"
                    '        NowShowTimeButton.ForeColor = Drawing.Color.Green
                    '    Case "110T"
                    '        NowShowTimeButton.ForeColor = Drawing.Color.Yellow
                    '    Case "80T"
                    '        NowShowTimeButton.ForeColor = Drawing.Color.Blue
                    '    Case "60T"
                    '        NowShowTimeButton.ForeColor = Drawing.Color.Black
                    '    Case "45T"
                    '        NowShowTimeButton.ForeColor = Drawing.Color.Pink
                    'End Select
                    NowShowTimeButton.ForeColor = Drawing.Color.Black
                    Dim SQL As String = "select NowStatus,PlanFinishTime,MONo from NowMachine where NowDate='" & SelectDate & "' and MachineID='" & strThisMachine & "'"
                    Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                    Dim MachineStstus As String = ""
                    If dt2.Rows.Count = 0 Then
                        MachineStstus = "1"
                        NowShowTimeButton.CommandArgument = "1"
                    Else
                        Dim ItemSpec As String = ""
                        Dim TooltipShow As String = ""
                        If Not IsDBNull(dt2.Rows(0).Item(2)) Then
                            Dim PrePorcessName As String = ""
                            Dim NextPorcessName As String = ""

                            Dim MOSpile() As String = Split(dt2.Rows(0).Item(2), "-")
                            Dim strSQL As String = "select MB002 from INVMB left join MOCTA on TA006=MB001 where TA001='" & MOSpile(0) & "' and TA002='" & MOSpile(1) & "' "
                            Dim dt1 As DataTable = dbConn.Query(strSQL, VarIni.ERP, dbConn.WhoCalledMe)
                            ItemSpec = dt1.Rows(0).Item(0)

                            Dim strSQLPreNext As String = "select * from SFCTA where TA001='" & MOSpile(0) & "' and TA002='" & MOSpile(1) & "' order by TA003 "
                            Dim dtPreNext As DataTable = dbConn.Query(strSQLPreNext, VarIni.ERP, dbConn.WhoCalledMe)
                            Dim MOSFCTA003(dtPreNext.Rows.Count) As String
                            Dim MOSFCTA007(dtPreNext.Rows.Count) As String
                            Dim TA003Index As Integer = -1
                            For K_Count As Integer = 0 To dtPreNext.Rows.Count - 1
                                MOSFCTA003(K_Count) = dtPreNext.Rows(K_Count).Item("TA003")
                                MOSFCTA007(K_Count) = dtPreNext.Rows(K_Count).Item("TA007")
                                If dtPreNext.Rows(K_Count).Item("TA003") = MOSpile(2) Then
                                    TA003Index = K_Count
                                End If
                            Next

                            If TA003Index <> -1 Then
                                If TA003Index = 0 Then
                                    If TA003Index < dtPreNext.Rows.Count - 1 Then
                                        NextPorcessName = MOSFCTA007(TA003Index + 1)
                                    End If
                                Else
                                    If TA003Index = dtPreNext.Rows.Count - 1 Then
                                        PrePorcessName = MOSFCTA007(TA003Index - 1)
                                    Else
                                        NextPorcessName = MOSFCTA007(TA003Index + 1)
                                        PrePorcessName = MOSFCTA007(TA003Index - 1)
                                    End If
                                End If
                            End If


                            TooltipShow = dt5.Rows(0).Item(1) & "-" & strThisMachine & "&#13;" & Trim(dt2.Rows(0).Item("MONo")) & "&#13;" & ItemSpec & "&#13;Pre:" & PrePorcessName & "&#13;Next:" & NextPorcessName
                            End If
                            NowShowTimeButton.CommandArgument = dt2.Rows(0).Item(0)

                        Select Case dt2.Rows(0).Item(0)
                            Case "1"        'idle
                                NowShowTimeButton.BackColor = Drawing.Color.White
                            Case "2"        'Call Setup MOLD
                                'NowShowTimeButton.ToolTip = Trim(dt2.Rows(0).Item("MONo")) & "&#13;" & ItemSpec
                                NowShowTimeButton.Attributes.Add("title", Server.HtmlDecode(TooltipShow))
                                NowShowTimeButton.BackColor = Drawing.Color.Pink
                                'NowShowTimeButton.ToolTip = ItemSpec
                            Case "3"        'Setting Mold
                                NowShowTimeButton.BackColor = Drawing.Color.Yellow
                                'NowShowTimeButton.ToolTip = ItemSpec
                                NowShowTimeButton.Attributes.Add("title", Server.HtmlDecode(TooltipShow))
                            Case "4"        'Setting Mold
                                NowShowTimeButton.BackColor = Drawing.Color.Firebrick
                                'NowShowTimeButton.ToolTip = ItemSpec
                                NowShowTimeButton.Attributes.Add("title", Server.HtmlDecode(TooltipShow))
                            Case "5"        'Running
                                'NowShowTimeButton.ToolTip = ItemSpec
                                NowShowTimeButton.Attributes.Add("title", Server.HtmlDecode(TooltipShow))
                                Dim PlanFinishTime As String = Year(Now) & "-" & Month(Now).ToString.PadLeft(2, "0") & "-" & Day(Now).ToString.PadLeft(2, "0") & " " & Left(dt2.Rows(0).Item(1), 2) & ":" & Right(dt2.Rows(0).Item(1), 2) & ":00"
                                Dim FinishTime As DateTime = Convert.ToDateTime(PlanFinishTime) '(PlanFinishTime, "yyyyMMdd HH:mm:ss")
                                Dim AboutMinFinish As Integer = DateDiff(DateInterval.Minute, Now, FinishTime)
                                Select Case AboutMinFinish
                                    Case < 30
                                        NowShowTimeButton.BackColor = Drawing.Color.DarkGreen
                                    Case Else
                                        NowShowTimeButton.BackColor = Drawing.Color.GreenYellow
                                End Select
                            Case "99"        'Setting Mold
                                NowShowTimeButton.BackColor = Drawing.Color.Red

                        End Select
                        Dim strLastMachine As String = "select top 1 * from UserMachineLog where UserID='" & Session("UserName") & "' order by CreateDate desc"
                        Dim dtLastMachine As DataTable = dbConn.Query(strLastMachine, VarIni.DBMIS, dbConn.WhoCalledMe)
                        If dtLastMachine.Rows.Count > 0 Then
                            If Trim(dtLastMachine.Rows(0).Item("LastMachineID")) = strThisMachine Then
                                NowShowTimeButton.Font.Size = FontUnit.Large
                            Else
                                NowShowTimeButton.Font.Size = FontUnit.Smaller
                            End If
                        End If
                    End If
                End If
            Next
        Next
    End Sub


    Protected Sub MachineSelect(sender As Object, e As EventArgs) 'Handles MachineA01.Click
        Dim strSqlMX As String = "select MX003,MX006,MX001 from CMSMX where MX006='" & Right(sender.ID, 3) & "'"
        Dim dt9 As DataTable = dbConn.Query(strSqlMX, VarIni.ERP, dbConn.WhoCalledMe)
        Dim strThisMachine As String = Trim(dt9.Rows(0).Item("MX001"))
        LBNowSelectMachine.Text = strThisMachine
        'LBNowSelectMachine.Text = NowSelectMachine
        LBMachineID.Text = LBNowSelectMachine.Text
        ThisMachineStatus = sender.CommandArgument
        LBNowSelectMachineStatus.Text = ThisMachineStatus
        'Dim alexalex As String = ""
        Dim strSqlInsert As String = "insert into UserMachineLog (UserID,LastMachineID) values('" & Session("UserName") & "','" & LBNowSelectMachine.Text & "')"
        Conn_SQL.Exec_Sql(strSqlInsert, Conn_SQL.MIS_ConnectionString)

        Dim strSql1 As String = "select MX002 from CMSMX where MX001='" & LBNowSelectMachine.Text & "'"
        Dim dt5 As DataTable = dbConn.Query(strSql1, VarIni.ERP, dbConn.WhoCalledMe)
        LBWorkCenter.Text = Trim(dt5.Rows(0).Item(0))
        initDDLMachineAllStatus()

        Select Case ThisMachineStatus
            Case "1"
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('SelMOForMachine.aspx?NowSelectMachine=" & LBNowSelectMachine.Text & "&MachineCode=" & LBNowSelectMachine.Text & "&SelectDate=" & SelectDate & "&WorkCenter=" & LBWorkCenter.Text & "');", True)
                'gvShow.Visible = True
                'ShowGridview()
            Case "99"
                modEx2.Show()
            Case Else

                If ThisMachineStatus = 4 Then
                    CBBEmployeee.Enabled = True
                    CBBEmployeee1.Enabled = True
                    CBBEmployeee2.Enabled = True
                    DDLCommonMO.Enabled = True
                    DDLCommonMO.Items.Clear()
                    CBBEmployeee.Items.Clear()
                    CBBEmployeee1.Items.Clear()
                    CBBEmployeee2.Items.Clear()
                    Dim SQLHR As String = "select emp.Code,cnname +'-' + enName from employee emp left join Job JB on emp.JobId=JB.JOBID left join Department Dept on Dept.departmentid=emp.DepartmentId where emp.LastWorkDate>=getdate() and JB.Name='Operator' and Dept.ShortName ='Stamping' order by emp.code"
                    Dim dtHR As DataTable = dbConn.Query(SQLHR, VarIni.HRMHT, dbConn.WhoCalledMe)
                    Dim EmpSelect As New ListItem
                    EmpSelect.Value = "0"
                    EmpSelect.Text = "Select"
                    CBBEmployeee.Items.Add(EmpSelect)
                    CBBEmployeee1.Items.Add(EmpSelect)
                    CBBEmployeee2.Items.Add(EmpSelect)
                    For EmpCount = 0 To dtHR.Rows.Count - 1
                        Dim SQLcheck As String = "select count(*) from NowMachine where EmpCode like '%" & Trim(dtHR.Rows(EmpCount).Item(0)) & "%'"
                        Dim dtCheck As DataTable = dbConn.Query(SQLcheck, VarIni.DBMIS, dbConn.WhoCalledMe)
                        If dtCheck.Rows(0).Item(0) = 0 Then
                            Dim EmpItem As New ListItem
                            EmpItem.Value = dtHR.Rows(EmpCount).Item(0)
                            EmpItem.Text = EmpItem.Value & " - " & dtHR.Rows(EmpCount).Item(1)
                            CBBEmployeee.Items.Add(EmpItem)
                            CBBEmployeee1.Items.Add(EmpItem)
                            CBBEmployeee2.Items.Add(EmpItem)
                        End If
                    Next
                    Dim SQLCommonMO As String = "select * from PlanSchedule where PlanDate='" & SelectDate & "' and WorkCenter='" & LBWorkCenter.Text & "' and SetupMold=0 and RealMachine is null"
                    Dim dtCommonMO As DataTable = dbConn.Query(SQLCommonMO, VarIni.DBMIS, dbConn.WhoCalledMe)
                    Dim DefMOCommon As New ListItem
                    DefMOCommon.Value = "0"
                    DefMOCommon.Text = "None"
                    DDLCommonMO.Items.Add(DefMOCommon)
                    For MOCount As Int16 = 0 To dtCommonMO.Rows.Count - 1
                        Dim MOCommon As New ListItem
                        MOCommon.Value = dtCommonMO(MOCount).Item("MoType") & "-" & dtCommonMO(MOCount).Item("MoNo") & "-" & dtCommonMO(MOCount).Item("MoSeq")
                        MOCommon.Text = MOCommon.Value
                        DDLCommonMO.Items.Add(MOCommon)
                    Next

                Else
                    DDLCommonMO.Enabled = False
                    CBBEmployeee.Enabled = False
                    CBBEmployeee1.Enabled = False
                    CBBEmployeee2.Enabled = False
                End If

                If ThisMachineStatus = 2 Then
                    CBBSetupEmployee.Enabled = True

                    CBBSetupEmployee.Items.Clear()
                    Dim SQLHR As String = "select emp.Code,cnname +'-' + enName from employee emp left join Job JB on emp.JobId=JB.JOBID left join Department Dept on Dept.departmentid=emp.DepartmentId where emp.LastWorkDate>=getdate() and Dept.Code ='STSP'  order by emp.code"
                    Dim dtHR As DataTable = dbConn.Query(SQLHR, VarIni.HRMHT, dbConn.WhoCalledMe)
                    Dim EmpSelect As New ListItem
                    EmpSelect.Value = "0"
                    EmpSelect.Text = "Select"
                    CBBSetupEmployee.Items.Add(EmpSelect)

                    For EmpCount = 0 To dtHR.Rows.Count - 1
                        Dim SQLcheck As String = "select count(*) from NowMachine where EmpCode='" & Trim(dtHR.Rows(EmpCount).Item(0)) & "' and NowDate='" & SelectDate & "'"
                        Dim dtCheck As DataTable = dbConn.Query(SQLcheck, VarIni.DBMIS, dbConn.WhoCalledMe)
                        If dtCheck.Rows(0).Item(0) = 0 Then
                            Dim EmpItem As New ListItem
                            EmpItem.Value = dtHR.Rows(EmpCount).Item(0)
                            EmpItem.Text = EmpItem.Value & " - " & dtHR.Rows(EmpCount).Item(1)
                            CBBSetupEmployee.Items.Add(EmpItem)
                        End If
                    Next
                Else
                    CBBSetupEmployee.Enabled = False
                End If

                Dim sqlMOWorkRecord2 As String = "select sum(Qty) from MOWorkRecord where MONo='" & LBMONO.Text & "' and NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "' and WorkType=2"
                Dim dtSMOWorkRecord2 As DataTable = dbConn.Query(sqlMOWorkRecord2, VarIni.DBMIS, dbConn.WhoCalledMe)
                If Not IsDBNull(dtSMOWorkRecord2.Rows(0).Item(0)) Then
                    LBProcFinishQty.Text = Math.Round(dtSMOWorkRecord2.Rows(0).Item(0), 0)
                Else
                    LBProcFinishQty.Text = "0"
                End If


                Dim SQL As String = "select NowStatus,MONo,EmpCode,PlanFinishTime from NowMachine where NowDate='" & SelectDate & "' and MachineID='" & Right(LBNowSelectMachine.Text, 3) & "'"
                Dim dt1 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                LBMONO.Text = dt1.Rows(0).Item("MONo")

                Dim MOGroup() As String = LBMONO.Text.ToString.Split("-")
                Dim SqlData As String = "select MB001,MB002,MB003,MOC.TA015,SFC.TA003,SFC.TA004,MW002,SFC.TA006,MD002,BOM.MF010,BOM.MF025" &
            " From INVMB " &
    "Left join MOCTA MOC on TA006=MB001 " &
    "left join SFCTA SFC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
    "left join BOMMF BOM on MF001=MB001 and MF003=SFC.TA003 " &
    "left join CMSMD on MD001=SFC.TA006 " &
    "left join CMSMW on MW001=SFC.TA004 " &
    "where MOC.TA001='" & MOGroup(0) & "' and MOC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
                Dim dtData As DataTable = dbConn.Query(SqlData, VarIni.ERP, dbConn.WhoCalledMe)

                Dim SQLPlanSchedule As String = "select * from PlanSchedule where PlanDate='" & SelectDate &
                 "' and MoType='" & MOGroup(0) & "' and MoNo='" & MOGroup(1) & "' and MoSeq='" & MOGroup(2) & "'"
                Dim dtSQLPlanSchedule As DataTable = dbConn.Query(SQLPlanSchedule, VarIni.DBMIS, dbConn.WhoCalledMe)

                If dtSQLPlanSchedule.Rows.Count > 0 Then
                    LBProcessQty.Text = Math.Round(dtSQLPlanSchedule.Rows(0).Item("PlanQty"), 0)
                Else
                    LBProcessQty.Text = "0"
                End If
                LBPartNumber.Text = dtData.Rows(0).Item("MB001")
                LBPartDesc.Text = dtData.Rows(0).Item("MB002")
                LBPartSpec.Text = dtData.Rows(0).Item("MB003")
                LBMOQty.Text = Math.Round(dtData.Rows(0).Item("TA015"), 0)
                If IsDBNull(dtData.Rows(0).Item("MF025")) Then
                    LBStdPcs.Text = 0
                Else
                    LBStdPcs.Text = dtData.Rows(0).Item("MF025")
                End If

                If LBStdPcs.Text <> 0 Then
                    Dim HoursValue As Integer = Math.Floor(((LBProcessQty.Text - LBProcFinishQty.Text) * LBStdPcs.Text) / 3600)
                    Dim MinValue As Integer = Math.Ceiling((LBProcessQty.Text - LBProcFinishQty.Text) * LBStdPcs.Text / 60) Mod 60
                    LBTotalTime.Text = HoursValue & "H " & MinValue & "M"
                Else
                    LBTotalTime.Text = 0
                End If

                Dim sqlMOWorkRecord As String = "select top 1 * from MOWorkRecord where MONo='" & LBMONO.Text & "' and MachineID='" & LBNowSelectMachine.Text & "' and WorkType=2 order by StartDate desc,StartTime desc"
                Dim dtSMOWorkRecord As DataTable = dbConn.Query(sqlMOWorkRecord, VarIni.DBMIS, dbConn.WhoCalledMe)
                If dtSMOWorkRecord.Rows.Count > 0 Then
                    LBRID.Text = dtSMOWorkRecord.Rows(0).Item("RID")
                    If Not IsDBNull(dtSMOWorkRecord.Rows(0).Item("StartTime")) Then
                        If Not IsDBNull(dtSMOWorkRecord.Rows(0).Item("StartDate")) Then
                            LBStartTime.Text = dtSMOWorkRecord.Rows(0).Item("StartDate") & "   " & dtSMOWorkRecord.Rows(0).Item("StartTime")
                        Else
                            LBStartTime.Text = dtSMOWorkRecord.Rows(0).Item("StartTime")
                        End If

                    Else
                        LBStartTime.Text = ""
                    End If
                    If Not IsDBNull(dt1.Rows(0).Item("PlanFinishTime")) Then
                        LBFinishTime.Text = dt1.Rows(0).Item("PlanFinishTime")
                    Else
                        LBFinishTime.Text = ""
                    End If

                    If Not IsDBNull(dtSMOWorkRecord.Rows(0).Item("Note")) Then
                        TBNote.Text = dtSMOWorkRecord.Rows(0).Item("Note")
                    Else
                        TBNote.Text = ""
                    End If
                Else
                    LBStartTime.Text = ""
                    LBFinishTime.Text = ""
                End If


                Dim sqlMOWorkRecord1 As String = "select top 1 * from MOWorkRecord where MONo='" & LBMONO.Text & "' and MachineID='" & LBNowSelectMachine.Text & "' and WorkType=1 order by StartDate desc,StartTime desc"
                Dim dtSMOWorkRecord1 As DataTable = dbConn.Query(sqlMOWorkRecord1, VarIni.DBMIS, dbConn.WhoCalledMe)
                If dtSMOWorkRecord1.Rows.Count > 0 Then
                    If Not IsDBNull(dtSMOWorkRecord1.Rows(0).Item("StartTime")) Then
                        If Not IsDBNull(dtSMOWorkRecord1.Rows(0).Item("StartDate")) Then
                            LBStartTime0.Text = dtSMOWorkRecord1.Rows(0).Item("StartDate") & "   " & dtSMOWorkRecord1.Rows(0).Item("StartTime")
                        Else
                            LBStartTime0.Text = dtSMOWorkRecord1.Rows(0).Item("StartTime")
                        End If

                    Else
                        LBStartTime0.Text = ""
                    End If
                    If Not IsDBNull(dtSMOWorkRecord1.Rows(0).Item("EndTime")) Then
                        If Not IsDBNull(dtSMOWorkRecord1.Rows(0).Item("EndDate")) Then
                            LBFinishTime0.Text = dtSMOWorkRecord1.Rows(0).Item("EndDate") & "  " & dtSMOWorkRecord1.Rows(0).Item("EndTime")
                        Else
                            LBFinishTime0.Text = dtSMOWorkRecord1.Rows(0).Item("EndTime")
                        End If

                    Else
                        LBFinishTime0.Text = ""
                    End If
                    If Not IsDBNull(dtSMOWorkRecord1.Rows(0).Item("EmpCode")) Then
                        Dim SQLHR As String = "Select CnName + '-' + EnName from Employee where Code='" & dtSMOWorkRecord1.Rows(0).Item("EmpCode") & "'"
                        Dim dtHR As DataTable = dbConn.Query(SQLHR, VarIni.HRMHT, dbConn.WhoCalledMe)
                        Dim EmpCode As New ListItem
                        Dim EmpCodeText As String = ""
                        If dtHR.Rows.Count > 0 Then
                            EmpCodeText = dtHR.Rows(0).Item(0)
                        Else
                            EmpCodeText = ""
                        End If
                        EmpCode.Text = EmpCodeText
                        EmpCode.Value = EmpCodeText
                        CBBSetupEmployee.Items.Add(EmpCode)
                        CBBSetupEmployee.SelectedValue = EmpCodeText
                    Else

                    End If
                Else
                    LBStartTime0.Text = ""
                    LBFinishTime0.Text = ""
                End If





                If ThisMachineStatus = 5 Then
                    Dim SQLCommon As String = "select * from MOCommonMO where RID='" & LBRID.Text & "'"
                    Dim dtCommon As DataTable = dbConn.Query(SQLCommon, VarIni.DBMIS, dbConn.WhoCalledMe)
                    If dtCommon.Rows.Count > 0 Then
                        Dim CommonItem As New ListItem
                        CommonItem.Value = dtCommon.Rows(0).Item("MONo")
                        CommonItem.Text = dtCommon.Rows(0).Item("MONo")
                        DDLCommonMO.Items.Add(CommonItem)
                    End If

                    TBFinishQty.Enabled = True
                    CBBEmployeee.Items.Clear()
                    CBBEmployeee1.Items.Clear()
                    CBBEmployeee2.Items.Clear()

                    If Not IsDBNull(dt1.Rows(0).Item("EmpCode")) Then
                        Dim EmpGroup() As String = dt1.Rows(0).Item("EmpCode").ToString.Split(";")
                        Dim SQLHR As String = "Select CnName + '-' + EnName from Employee where Code='" & EmpGroup(0) & "'"
                        Dim dtHR As DataTable = dbConn.Query(SQLHR, VarIni.HRMHT, dbConn.WhoCalledMe)
                        Dim EmpCode As New ListItem
                        If dtHR.Rows.Count > 0 Then
                            EmpCode.Text = dtHR.Rows(0).Item(0)
                            EmpCode.Value = EmpGroup(0)
                        Else
                            EmpCode.Text = ""
                            EmpCode.Value = EmpGroup(0)
                        End If
                        CBBEmployeee.Items.Add(EmpCode)
                        CBBEmployeee.SelectedValue = EmpGroup(0)


                        Dim EmpCode1 As New ListItem
                        SQLHR = "Select CnName + '-' + EnName from Employee where Code='" & EmpGroup(1) & "'"
                        dtHR = dbConn.Query(SQLHR, VarIni.HRMHT, dbConn.WhoCalledMe)

                        If dtHR.Rows.Count > 0 Then
                            EmpCode1.Text = dtHR.Rows(0).Item(0)
                            EmpCode1.Value = EmpGroup(1)
                        Else
                            EmpCode1.Text = ""
                            EmpCode1.Value = EmpGroup(1)
                        End If
                        CBBEmployeee1.Items.Add(EmpCode1)
                        CBBEmployeee1.SelectedValue = EmpGroup(1)

                        Dim EmpCode2 As New ListItem
                        SQLHR = "Select CnName + '-' + EnName from Employee where Code='" & EmpGroup(2) & "'"
                        dtHR = dbConn.Query(SQLHR, VarIni.HRMHT, dbConn.WhoCalledMe)
                        If dtHR.Rows.Count > 0 Then
                            EmpCode2.Text = dtHR.Rows(0).Item(0)
                            EmpCode2.Value = EmpGroup(2)
                        Else
                            EmpCode2.Text = ""
                            EmpCode2.Value = EmpGroup(2)
                        End If
                        CBBEmployeee2.Items.Add(EmpCode2)
                        CBBEmployeee2.SelectedValue = EmpGroup(2)
                    End If
                Else
                    TBFinishQty.Enabled = False
                End If

                If ThisMachineStatus = 3 Then
                    CBBSetupEmployee.Items.Clear()

                    If Not IsDBNull(dt1.Rows(0).Item("EmpCode")) Then
                        Dim EmpGroup() As String = dt1.Rows(0).Item("EmpCode").ToString.Split(";")
                        Dim SQLHR As String = "Select CnName + '-' + EnName from Employee where Code='" & EmpGroup(0) & "'"
                        Dim dtHR As DataTable = dbConn.Query(SQLHR, VarIni.HRMHT, dbConn.WhoCalledMe)
                        Dim EmpCode As New ListItem
                        If dtHR.Rows.Count > 0 Then
                            EmpCode.Text = dtHR.Rows(0).Item(0)
                            EmpCode.Value = EmpGroup(0)
                            CBBSetupEmployee.Items.Add(EmpCode)
                            CBBSetupEmployee.SelectedValue = EmpGroup(0)
                        Else
                            EmpCode.Text = ""
                            EmpCode.Value = EmpGroup(0)
                            CBBSetupEmployee.Items.Add(EmpCode)
                        End If


                    End If
                Else
                    'TBFinishQty.Enabled = False
                End If

                Dim SqlHis As String = "select * from MOWorkRecord where MachineID='" & LBMachineID.Text & "' and EndTime is not null " &
"and MONo not in (select top 1 MONo from MOWorkRecord where MachineID='" & LBMachineID.Text & "' and EndTime is not null order by NowDate desc,EndTime desc)" &
"order by NowDate desc,EndTime desc"
                Dim dtHis As DataTable = dbConn.Query(SqlHis, VarIni.DBMIS, dbConn.WhoCalledMe)
                If dtHis.Rows.Count > 0 Then

                    Dim SqlHis1 As String = "select MB001,MB002,MB003,MOC.TA015,SFC.TA003,SFC.TA004,MW002,SFC.TA006,MD002,BOM.MF010,BOM.MF025" &
            " From INVMB " &
    "Left join MOCTA MOC on TA006=MB001 " &
    "left join SFCTA SFC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
    "left join BOMMF BOM on MF001=MB001 and MF003=SFC.TA003 " &
    "left join CMSMD on MD001=SFC.TA006 " &
    "left join CMSMW on MW001=SFC.TA004 " &
    "where MOC.TA001='" & MOGroup(0) & "' and MOC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
                    Dim dtHis1 As DataTable = dbConn.Query(SqlHis1, VarIni.ERP, dbConn.WhoCalledMe)
                    LBHisMONO.Text = dtHis.Rows(0).Item("MONo")
                    LBHisPartNumber.Text = dtHis1.Rows(0).Item("MB001")
                    LBHisDesc.Text = dtHis1.Rows(0).Item("MB002")
                    LBHisSpec.Text = dtHis1.Rows(0).Item("MB003")
                    LBHisMOQty.Text = Math.Round(dtHis1.Rows(0).Item("TA015"), 0)
                    LBHisPlanQty.Text = ""
                    LBHisFinishQty.Text = Math.Round(dtHis.Rows(0).Item("Qty"), 0)
                    If IsDBNull(dtHis1.Rows(0).Item("MF025")) Then
                        LBHisStdPcs.Text = ""
                    Else
                        LBHisStdPcs.Text = dtHis1.Rows(0).Item("MF025")
                    End If

                    If IsDBNull(dtHis.Rows(0).Item("StartTime")) Or IsDBNull(dtHis.Rows(0).Item("StartDate")) Then
                        LBHisStartTime.Text = ""
                    Else
                        LBHisStartTime.Text = dtHis.Rows(0).Item("StartDate") & " " & dtHis.Rows(0).Item("StartTime")
                    End If
                    If IsDBNull(dtHis.Rows(0).Item("EndDate")) Or IsDBNull(dtHis.Rows(0).Item("EndTime")) Then
                        LBHisFinishTime.Text = ""
                    Else
                        LBHisFinishTime.Text = dtHis.Rows(0).Item("EndDate") & "  " & dtHis.Rows(0).Item("EndTime")
                    End If

                    ' LBHisTotalTime.Text = LBHisFinishTime.Text - LBHisStartTime.Text
                Else
                    LBHisMONO.Text = ""
                    LBHisPartNumber.Text = ""
                    LBHisDesc.Text = ""
                    LBHisSpec.Text = ""
                    LBHisMOQty.Text = ""
                    LBHisPlanQty.Text = ""
                    LBHisFinishQty.Text = ""
                    LBHisStdPcs.Text = ""
                    LBHisStartTime.Text = ""
                    LBHisFinishTime.Text = ""
                End If
                modEx2.Show()

        End Select
    End Sub








    Public Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub



    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'NowSelectMachine = LBMachineID.Text
        Dim SQL As String = "select NowStatus,MONo from NowMachine where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
        Dim dt1 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim MoNo As String = ""
        If Not IsDBNull(dt1.Rows(0).Item("MONo")) Then
            MoNo = dt1.Rows(0).Item("MONo")
        End If
        Dim NowSelectMachineStatus As String = dt1.Rows(0).Item("NowStatus")


        'If NowSelectMachineStatus = 2 Then
        Select Case DDLMachineAllStatus.SelectedValue
            Case 1          'Idle
                Dim strSql As String = ""
                If NowSelectMachineStatus = 2 Or NowSelectMachineStatus = 3 Or NowSelectMachineStatus = 4 Then
                    strSql = "delete DailyMachineReal where WorkDate='" & SelectDate & "' and WorkCenter='" & Trim(LBWorkCenter.Text) & "' and MachineCode='" & LBNowSelectMachine.Text & "' and MONo='" & MoNo & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','Stop :" & MoNo & ",from Status" & NowSelectMachineStatus & "','" & NowSelectMachineStatus & "','1')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    strSql = "update NowMachine set NowStatus='1',MONo=null,EmpCode=Null where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    strSql = "update PlanSchedule set RealMachine=null where PlanDate='" & SelectDate & "' and WorkCenter='" & LBWorkCenter.Text & "' and MoType + '-' + RTRIM(MoNo) +'-'+RTRIM(MoSeq)='" & MoNo & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    If NowSelectMachineStatus = 3 Then
                        Dim EndTime As String = Hour(Now).ToString.PadLeft(2, "0") & Minute(Now).ToString.PadLeft(2, "0")
                        strSql = "update MOWorkRecord set EndTime='" & EndTime & "',EndDate='" & SelectDate & "',Qty='" & TBFinishQty.Text & "',Note='" & TBNote.Text & "' where MONo='" & MoNo & "' and NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "' and EndTime is null and WorkType=1"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    End If
                    Response.Redirect(Request.Url.ToString())
                End If
                If NowSelectMachineStatus = 99 Then
                    strSql = "update NowMachine set NowStatus='1',MONo=null where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    Response.Redirect(Request.Url.ToString())
                End If
                If NowSelectMachineStatus = 5 And TBFinishQty.Text <> "" Then
                    strSql = "update NowMachine set NowStatus='1',MONo=null,EmpCode=null where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','Stop :" & MoNo & ",from Status" & NowSelectMachineStatus & "','" & NowSelectMachineStatus & "','1')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    Dim EndTime As String = Hour(Now).ToString.PadLeft(2, "0") & Minute(Now).ToString.PadLeft(2, "0")
                    strSql = "update MOWorkRecord set EndTime='" & EndTime & "',EndDate='" & SelectDate & "',Qty='" & TBFinishQty.Text & "',Note='" & TBNote.Text & "' where MONo='" & MoNo & "' and NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "' and EndTime is null and WorkType=2"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    Response.Redirect(Request.Url.ToString())
                Else
                    show_message.ShowMessage(Page, "Must Input Qty", UpdatePanel1)
                    Exit Sub
                End If
                'Response.Redirect(Request.Url.ToString())
            Case 2          'Wait Setup Mold
                If NowSelectMachineStatus <> 2 And NowSelectMachineStatus <> 5 Then
                    Dim strSql As String = ""
                    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','from Status" & NowSelectMachineStatus & " to Wait Setup Mold','" & NowSelectMachineStatus & "','2')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    strSql = "update NowMachine set NowStatus='2' where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    Response.Redirect(Request.Url.ToString())
                Else
                    If NowSelectMachineStatus = "2" Then
                        Dim strSql As String
                        strSql = "update MOWorkRecord set Note='" & TBNote.Text & "' where RID='" & LBRID.Text & "'"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    End If
                End If

            Case 3
                If NowSelectMachineStatus <> 3 And NowSelectMachineStatus <> 5 Then
                    If CBBSetupEmployee.SelectedValue = "0" Then
                        show_message.ShowMessage(Page, "Must Select Employee", UpdatePanel1)
                        Exit Sub
                    End If
                    Dim strSql As String = ""
                    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','from Status" & NowSelectMachineStatus & "to Setting Mold','" & NowSelectMachineStatus & "','3')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    strSql = "update NowMachine set NowStatus='3',EmpCode='" & CBBSetupEmployee.SelectedValue & "' where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    If NowSelectMachineStatus = 2 Then
                        Dim StartTime As String = Hour(Now).ToString.PadLeft(2, "0") & Minute(Now).ToString.PadLeft(2, "0")
                        strSql = "insert into MOWorkRecord (NowDate,MachineID,MONo,StartTime,EmpCode,WorkType,StartDate,Note) values('" &
                        SelectDate & "','" & LBMachineID.Text & "','" & LBMONO.Text & "','" & StartTime & "','" & CBBSetupEmployee.SelectedValue & "','1','" & SelectDate & "','" & TBNote.Text & "')"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    End If
                    Response.Redirect(Request.Url.ToString())
                Else
                    If NowSelectMachineStatus = "3" Then
                        Dim strSql As String
                        strSql = "update MOWorkRecord set Note='" & TBNote.Text & "' where RID='" & LBRID.Text & "'"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    End If
                End If
            Case 4
                Dim strSql As String = ""
                If NowSelectMachineStatus <> 4 And NowSelectMachineStatus <> 5 Then
                    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','from Status" & NowSelectMachineStatus & "to Wait Running','" & NowSelectMachineStatus & "','4')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    strSql = "update NowMachine set NowStatus='4',EmpCode=Null where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    If NowSelectMachineStatus = 3 Then
                        Dim EndTime As String = Hour(Now).ToString.PadLeft(2, "0") & Minute(Now).ToString.PadLeft(2, "0")
                        strSql = "update MOWorkRecord set EndTime='" & EndTime & "',EndDate='" & SelectDate & "',Qty='" & TBFinishQty.Text & "',Note='" & TBNote.Text & "' where MONo='" & MoNo & "' and NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "' and EndTime is null and WorkType=1"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    End If
                    Response.Redirect(Request.Url.ToString())
                Else
                    If NowSelectMachineStatus = 4 Then
                        'Dim strSql As String
                        strSql = "update MOWorkRecord set Note='" & TBNote.Text & "' where RID='" & LBRID.Text & "'"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    End If
                End If
                If NowSelectMachineStatus = 5 And TBFinishQty.Text <> "" Then
                    'If TBFinishQty.Text <= "" Then

                    strSql = "update NowMachine set NowStatus='4',EmpCode=Null where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','from Status" & NowSelectMachineStatus & "to Wait Running','" & NowSelectMachineStatus & "','4')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    Dim EndTime As String = Hour(Now).ToString.PadLeft(2, "0") & Minute(Now).ToString.PadLeft(2, "0")
                    strSql = "update MOWorkRecord set EndTime='" & EndTime & "',EndDate='" & SelectDate & "',Qty='" & TBFinishQty.Text & "',Note='" & TBNote.Text & "' where MONo='" & MoNo & "' and NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "' and EndTime is null and WorkType=2"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    Response.Redirect(Request.Url.ToString())
                    'Else
                    'show_message.ShowMessage(Page, "", UpdatePanel1)
                    'Exit Sub
                    'End If
                Else
                    show_message.ShowMessage(Page, "Must Input Qty", UpdatePanel1)
                    Exit Sub
                End If
            Case 5
                If NowSelectMachineStatus = 4 Then
                    If CBBEmployeee.SelectedValue = "0" Then
                        show_message.ShowMessage(Page, "Must Select Employee", UpdatePanel1)
                        Exit Sub
                    End If
                    Dim MOSpile() As String = MoNo.Split("-")
                    'SQL = "select * from DailyMachineReal where WorkDate='" & SelectDate & "' and MachineCode='" & LBNowSelectMachine.Text & "' and MONo='" & MoNo & "'"
                    'Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

                    Dim PlanFinishTime As String = "0000"
                    'If dt2.Rows.Count > 0 Then
                    Dim FinishTime As Date = DateAdd("s", ((LBProcessQty.Text - LBProcFinishQty.Text) * LBStdPcs.Text), Now())
                    PlanFinishTime = Hour(FinishTime).ToString.PadLeft(2, "0") & Minute(FinishTime).ToString.PadLeft(2, "0")
                    'Else
                    'End If
                    Dim strSql As String = ""
                    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','from Status" & NowSelectMachineStatus & "to Running','" & NowSelectMachineStatus & "','5')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    strSql = "update NowMachine set NowStatus='5',PlanFinishTime='" & PlanFinishTime & "',EmpCode='" & CBBEmployeee.SelectedValue & ";" & CBBEmployeee1.SelectedValue & ";" & CBBEmployeee2.SelectedValue & "' where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    Dim StartTime As String = Hour(Now).ToString.PadLeft(2, "0") & Minute(Now).ToString.PadLeft(2, "0")
                    strSql = "insert into MOWorkRecord (NowDate,MachineID,MONo,StartTime,EmpCode,WorkType,StartDate,Note) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','" & LBMONO.Text & "','" & StartTime & "','" & CBBEmployeee.SelectedValue & ";" & CBBEmployeee1.SelectedValue & ";" & CBBEmployeee2.SelectedValue & "','2','" & SelectDate & "','" & TBNote.Text & "')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)

                    If DDLCommonMO.SelectedValue <> 0 Then
                        Dim MOGroup() As String = LBMONO.Text.Split("-")
                        strSql = "update PlanSchedule set SetupMold'1' where PlanDate='" & SelectDate & "' and MoType='" & MOGroup(0) & "' and MoNo='" & MOGroup(0) & "' and MoSeq='" & MOGroup(0) & "'"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                        strSql = "insert into MOCommonMO values('" & LBRID.Text & "','" & DDLCommonMO.SelectedValue & "')"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    End If

                    Response.Redirect(Request.Url.ToString())
                Else
                    If NowSelectMachineStatus = "5" Then
                        Dim strSql As String
                        strSql = "update MOWorkRecord set Note='" & TBNote.Text & "' where RID='" & LBRID.Text & "'"
                        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    End If
                    'show_message.ShowMessage(Page, "Must Input Qty", UpdatePanel1)
                    'Exit Sub
                End If
            Case 99
                If NowSelectMachineStatus = 1 Then
                    Dim strSql As String = ""
                    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
                    SelectDate & "','" & LBMachineID.Text & "','from Status" & NowSelectMachineStatus & "to Break','" & NowSelectMachineStatus & "','99')"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    strSql = "update NowMachine set NowStatus='99' where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
                    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
                    Response.Redirect(Request.Url.ToString())
                Else
                    show_message.ShowMessage(Page, "Machine Must be IDLE", UpdatePanel1)
                    Exit Sub
                End If

        End Select


    End Sub


    'Private Sub ShowGridview()
    '    Dim dt = New DataTable,
    '    colName As New ArrayList

    '    dt.Columns.Add("A") '5
    '    colName.Add("MachineID:A")

    '    dt.Columns.Add("B") '0
    '    colName.Add("Status:B")

    '    dt.Columns.Add("C") '1
    '    colName.Add("MO Number:C")

    '    dt.Columns.Add("D") '1
    '    colName.Add("Part No:D")

    '    dt.Columns.Add("E") '1
    '    colName.Add("Desc:E")

    '    'dt.Columns.Add("Emp Name") '3
    '    dt.Columns.Add("F") '3
    '    colName.Add("Spec:F")

    '    dt.Columns.Add("G") '4
    '    colName.Add("MO Qty:G")

    '    dt.Columns.Add("H") '5
    '    colName.Add("Process Qty:H")

    '    dt.Columns.Add("I") '5
    '    colName.Add("Finish Qty:I")

    '    dt.Columns.Add("J") '5
    '    colName.Add("Start Time:J")

    '    dt.Columns.Add("K") '5
    '    colName.Add("Std Pcs:K")

    '    'set format
    '    ControlForm.GridviewColWithLinkFirst(gvShow, colName)


    '    Dim SQL As String = "select * from NowMachine where NowDate='" & SelectDate & "' and MONo is not null"
    '    Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

    '    For L_count As Integer = 0 To dt2.Rows().Count - 1
    '        'gvShow.Rows
    '        Dim MOGroup() As String = dt2.Rows(L_count).Item("MONo").ToString.Split("-")
    '        'Dim MOGroup() As String = ""

    '        Dim SqlData As String = "select MB001,MB002,MB003,MOC.TA015,SFC.TA003,SFC.TA004,MW002,SFC.TA006,MD002,BOM.MF010,BOM.MF025" &
    '        " From INVMB " &
    '"Left join MOCTA MOC on TA006=MB001 " &
    '"left join SFCTA SFC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
    '"left join BOMMF BOM on MF001=MB001 and MF003=SFC.TA003 " &
    '"left join CMSMD on MD001=SFC.TA006 " &
    '"left join CMSMW on MW001=SFC.TA004 " &
    '"where MOC.TA001='" & MOGroup(0) & "' and MOC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
    '        Dim dtData As DataTable = dbConn.Query(SqlData, VarIni.ERP, dbConn.WhoCalledMe)

    '        Dim SQLPlanSchedule As String = "select * from PlanSchedule where PlanDate='" & SelectDate &
    '             "' and MoType='" & MOGroup(0) & "' and MoNo='" & MOGroup(1) & "' and MoSeq='" & MOGroup(2) & "'"
    '        Dim dtSQLPlanSchedule As DataTable = dbConn.Query(SQLPlanSchedule, VarIni.DBMIS, dbConn.WhoCalledMe)
    '        Dim row = dt.NewRow
    '        Dim colSeq As Integer = 0

    '        row(colSeq) = dt2.Rows(L_count).Item("MachineID")
    '        colSeq += 1
    '        row(colSeq) = dt2.Rows(L_count).Item("NowStatus")

    '        colSeq += 1
    '        row(colSeq) = dt2.Rows(L_count).Item("MONo")


    '        colSeq += 1
    '        row(colSeq) = dtData.Rows(0).Item("MB001")
    '        colSeq += 1
    '        row(colSeq) = dtData.Rows(0).Item("MB002")
    '        colSeq += 1
    '        row(colSeq) = dtData.Rows(0).Item("MB003") 'dt4.Rows(0).Item("pmao004")

    '        colSeq += 1
    '        row(colSeq) = dtData.Rows(0).Item("TA015")

    '        colSeq += 1
    '        If dtSQLPlanSchedule.Rows.Count > 0 Then
    '            row(colSeq) = dtSQLPlanSchedule.Rows(0).Item("PlanQty")
    '        Else
    '            row(colSeq) = ""
    '        End If

    '        colSeq += 1
    '        Dim sqlMOWorkRecord2 As String = "select sum(Qty) from MOWorkRecord where MONo='" & dt2.Rows(L_count).Item("MONo") & "' and NowDate='" & SelectDate & "' and MachineID='" & dt2.Rows(L_count).Item("MachineID") & "' and WorkType=2"
    '        Dim dtSMOWorkRecord2 As DataTable = dbConn.Query(sqlMOWorkRecord2, VarIni.DBMIS, dbConn.WhoCalledMe)
    '        If Not IsDBNull(dtSMOWorkRecord2.Rows(0).Item(0)) Then
    '            row(colSeq) = dtSMOWorkRecord2.Rows(0).Item(0)
    '        Else
    '            row(colSeq) = ""
    '        End If

    '        Dim sqlMOWorkRecord As String = "select top 1 * from MOWorkRecord where MONo='" & dt2.Rows(L_count).Item("MONo") & "' and MachineID='" & dt2.Rows(L_count).Item("MachineID") & "' and WorkType=2 order by StartDate desc,StartTime desc"
    '        Dim dtSMOWorkRecord As DataTable = dbConn.Query(sqlMOWorkRecord, VarIni.DBMIS, dbConn.WhoCalledMe)
    '        colSeq += 1
    '        If dtSMOWorkRecord.Rows.Count > 0 Then
    '            row(colSeq) = dtSMOWorkRecord.Rows(0).Item("StartDate") & "  " & dtSMOWorkRecord.Rows(0).Item("StartTime")
    '        Else
    '            row(colSeq) = ""
    '        End If

    '        colSeq += 1
    '        row(colSeq) = dtData.Rows(0).Item("MF025")
    '        dt.Rows.Add(row)

    '        'SerialNo += 1
    '    Next

    '    gvShow.DataSource = dt
    '    gvShow.DataBind()

    '    'For i = 0 To gvShow.Rows.Count - 1
    '    '    Dim selectdata As New Button
    '    '    selectdata.ID = "alex" & i
    '    '    selectdata.Text = "select"
    '    '    'selectdata.Attributes.Add("onclick", "javascript:CloseWindows()") '.Add(("onclick", "javascript:CloseWindows()")
    '    '    selectdata.CommandArgument = i 'gvShow.Rows(i).Cells(1).Text
    '    '    gvShow.Rows(i).Cells(8).Controls.Add(selectdata)
    '    'Next
    '    gvShow.Visible = True
    'End Sub

    Protected Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow1", "window.open('ShowMachineTableList.aspx?SelectDate=" & SelectDate & "');", True)

    End Sub

    'Protected Sub gvShow_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShow.RowCommand
    '    Dim Sindex As String = e.CommandArgument
    '    Dim Qty As Int16 = Int(sender.rows(Sindex).cells(5).text)
    '    Dim TotalTime As Int32 = Int(sender.rows(Sindex).cells(7).text)
    '    Dim MoNo As String = Trim(sender.rows(Sindex).cells(1).text)
    '    Dim PlanSeq As String = Trim(sender.rows(Sindex).cells(0).text)
    '    Dim StartTime As String = Hour(Now).ToString.PadLeft(2, "0") & Minute(Now).ToString.PadLeft(2, "0")
    '    Dim strSql As String = ""

    '    strSql = "insert into DailyMachineReal(WorkDate,WorkCenter,MachineCode,StartTime,MONo,TotalTime) "
    '    strSql = strSql & "values('" & SelectDate & "','" & Trim(LBWorkCenter.Text) & "','" & LBNowSelectMachine.Text & "','" & StartTime & "','" & MoNo & "','" & TotalTime & "')"
    '    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
    '    strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
    '    SelectDate & "','" & LBNowSelectMachine.Text & "','From Idle to Running MO" & MoNo & "','1','5')"
    '    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
    '    strSql = "update NowMachine set NowStatus='2',MONo='" & MoNo & "' where NowDate='" & SelectDate & "' and MachineID='" & LBNowSelectMachine.Text & "'"
    '    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
    '    strSql = "update PlanSchedule set RealMachine='" & LBNowSelectMachine.Text & "' where PlanDate='" & SelectDate & "' and WorkCenter='" & LBWorkCenter.Text & "' and PlanSeq='" & PlanSeq & "'"
    '    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
    '    'Response.Write("<script>window.opener.location.reload();</script>")
    '    'Response.Write("<script>window.close();</script>")
    'End Sub



End Class