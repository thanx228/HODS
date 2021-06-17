Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class MachineDailyPlan
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim SelectDate As String
    Dim WorkCenter As String
    Dim dbConn As New DataConnectControl
    Dim Machine1ATime() As String = {"MachineA1", "MachineA2", "MachineA3", "MachineA4", "MachineA5", "MachineA6", "MachineA7", "MachineA8", "MachineA9", "MachineA10", "MachineA11", "MachineA12", "MachineA13", "MachineA14", "MachineA15", "MachineA16", "MachineA17", "MachineA18", "MachineA19", "MachineA20", "MachineA21", "MachineA22", "MachineA23", "MachineA24", "MachineA25"}
    Dim Machine1BTime() As String = {"MachineB1", "MachineB2", "MachineB3", "MachineB4", "MachineB5", "MachineB6", "MachineB7", "MachineB8", "MachineB9", "MachineB10", "MachineB11", "MachineB12", "MachineB13", "MachineB14", "MachineB15", "MachineB16", "MachineB17", "MachineB18", "MachineB19", "MachineB20", "MachineB21", "MachineB22", "MachineB23", "MachineB24", "MachineB25"}
    Dim Machine1CTime() As String = {"MachineC1", "MachineC2", "MachineC3", "MachineC4", "MachineC5", "MachineC6", "MachineC7", "MachineC8", "MachineC9", "MachineC10", "MachineC11", "MachineC12", "MachineC13", "MachineC14", "MachineC15", "MachineC16", "MachineC17", "MachineC18", "MachineC19", "MachineC20", "MachineC21", "MachineC22", "MachineC23", "MachineC24", "MachineC25"}
    Dim Machine1DTime() As String = {"MachineD1", "MachineD2", "MachineD3", "MachineD4", "MachineD5", "MachineD6", "MachineD7", "MachineD8", "MachineD9", "MachineD10", "MachineD11", "MachineD12", "MachineD13", "MachineD14", "MachineD15", "MachineD16", "MachineD17", "MachineD18", "MachineD19", "MachineD20", "MachineD21", "MachineD22", "MachineD23", "MachineD24", "MachineD25"}
    Dim Machine1ETime() As String = {"MachineE1", "MachineE2", "MachineE3", "MachineE4", "MachineE5", "MachineE6", "MachineE7", "MachineE8", "MachineE9", "MachineE10", "MachineE11", "MachineE12", "MachineE13", "MachineE14", "MachineE15", "MachineE16", "MachineE17", "MachineE18", "MachineE19", "MachineE20", "MachineE21", "MachineE22", "MachineE23", "MachineE24", "MachineE25"}
    Dim Machine1FTime() As String = {"MachineF1", "MachineF2", "MachineF3", "MachineF4", "MachineF5", "MachineF6", "MachineF7", "MachineF8", "MachineF9", "MachineF10", "MachineF11", "MachineF12", "MachineF13", "MachineF14", "MachineF15", "MachineF16", "MachineF17", "MachineF18", "MachineF19", "MachineF20", "MachineF21", "MachineF22", "MachineF23", "MachineF24", "MachineF25"}
    Dim Machine1GTime() As String = {"MachineG1", "MachineG2", "MachineG3", "MachineG4", "MachineG5", "MachineG6", "MachineG7", "MachineG8", "MachineG9", "MachineG10", "MachineG11", "MachineG12", "MachineG13", "MachineG14", "MachineG15", "MachineG16", "MachineG17", "MachineG18", "MachineG19", "MachineG20", "MachineG21", "MachineG22", "MachineG23", "MachineG24", "MachineG25"}
    Dim Machine1HTime() As String = {"MachineH1", "MachineH2", "MachineH3", "MachineH4", "MachineH5", "MachineH6", "MachineH7", "MachineH8", "MachineH9", "MachineH10", "MachineH11", "MachineH12", "MachineH13", "MachineH14", "MachineH15", "MachineH16", "MachineH17", "MachineH18", "MachineH19", "MachineH20", "MachineH21", "MachineH22", "MachineH23", "MachineH24", "MachineH25"}
    Dim Machine1ITime() As String = {"MachineI1", "MachineI2", "MachineI3", "MachineI4", "MachineI5", "MachineI6", "MachineI7", "MachineI8", "MachineI9", "MachineI10", "MachineI11", "MachineI12", "MachineI13", "MachineI14", "MachineI15", "MachineI16", "MachineI17", "MachineI18", "MachineI19", "MachineI20", "MachineI21", "MachineI22", "MachineI23", "MachineI24", "MachineI25"}
    Dim Machine1JTime() As String = {"MachineJ1", "MachineJ2", "MachineJ3", "MachineJ4", "MachineJ5", "MachineJ6", "MachineJ7", "MachineJ8", "MachineJ9", "MachineJ10", "MachineJ11", "MachineJ12", "MachineJ13", "MachineJ14", "MachineJ15", "MachineJ16", "MachineJ17", "MachineJ18", "MachineJ19", "MachineJ20", "MachineJ21", "MachineJ22", "MachineJ23", "MachineJ24", "MachineJ25"}
    Dim Machine1KTime() As String = {"MachineK1", "MachineK2", "MachineK3", "MachineK4", "MachineK5", "MachineK6", "MachineK7", "MachineK8", "MachineK9", "MachineK10", "MachineK11", "MachineK12", "MachineK13", "MachineK14", "MachineK15", "MachineK16", "MachineK17", "MachineK18", "MachineK19", "MachineK20", "MachineK21", "MachineK22", "MachineK23", "MachineK24", "MachineK25"}
    Dim Machine1LTime() As String = {"MachineL1", "MachineL2", "MachineL3", "MachineL4", "MachineL5", "MachineL6", "MachineL7", "MachineL8", "MachineL9", "MachineL10", "MachineL11", "MachineL12", "MachineL13", "MachineL14", "MachineL15", "MachineL16", "MachineL17", "MachineL18", "MachineL19", "MachineL20", "MachineL21", "MachineL22", "MachineL23", "MachineL24", "MachineL25"}
    Dim Machine1MTime() As String = {"MachineM1", "MachineM2", "MachineM3", "MachineM4", "MachineM5", "MachineM6", "MachineM7", "MachineM8", "MachineM9", "MachineM10", "MachineM11", "MachineM12", "MachineM13", "MachineM14", "MachineM15", "MachineM16", "MachineM17", "MachineM18", "MachineM19", "MachineM20", "MachineM21", "MachineM22", "MachineM23", "MachineM24", "MachineM25"}

    Dim AllLBMachineCode() As String = {"", "LBMachineCode1", "LBMachineCode2", "LBMachineCode3", "LBMachineCode4", "LBMachineCode5", "LBMachineCode6", "LBMachineCode7", "LBMachineCode8", "LBMachineCode9", "LBMachineCode10", "LBMachineCode11", "LBMachineCode12", "LBMachineCode13"}



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Date1.Text = "" Then
            Date1.Text = Year(Now) & Month(Now).ToString.PadLeft(2, "0") & Day(Now).ToString.PadLeft(2, "0")
        End If

        If Request("MONo") <> "" Then
            LBMONo.Text = Request("MONo")
        Else

        End If


        If Not IsPostBack Then
            Session("UserName") = "500026"
            Session("UserId") = "500026"
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            InitDDLWorkCenter()
            SelectDate = Date1.Text
            WorkCenter = DDLWorkCenter.SelectedValue
        Else

        End If
        If Request("SelectDate") <> "" Then
            SelectDate = Request("SelectDate")
            Date1.Text = SelectDate
        Else
            SelectDate = Date1.Text
        End If
        If Request("WorkCenter") <> "" Then
            WorkCenter = Request("WorkCenter")
            DDLWorkCenter.SelectedValue = WorkCenter
        Else
            WorkCenter = DDLWorkCenter.SelectedValue
        End If
        InitLBMachineCode()
        InitEveryButton()



    End Sub


    Dim NowMachineNameTitle(13) As String
    Private Sub InitLBMachineCode()
        Dim SqlMX As String = "select MX001,MX006 from CMSMX where MX002='" & Trim(DDLWorkCenter.SelectedValue) & "' order by MX006"
        Dim dt2 As DataTable = dbConn.Query(SqlMX, VarIni.ERP, dbConn.WhoCalledMe)
        For i = 1 To dt2.Rows.Count
            If i < 13 Then
                Dim alex As TextBox = Page.FindControl("ctl00$MainContent$" & AllLBMachineCode(i)) ')
                alex.Text = dt2.Rows(i - 1).Item(1) & "(" & dt2.Rows(i - 1).Item(0) & ")"
                NowMachineNameTitle(i) = Trim(dt2.Rows(i - 1).Item(1)) & "(" & Trim(dt2.Rows(i - 1).Item(0)) & ")" 'Trim(dt2.Rows(i - 1).Item(0))
            End If
        Next
    End Sub
    Private Sub InitDDLWorkCenter()
        Dim SQL As String = "select * from CMSMD"
        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i = 0 To dt2.Rows.Count - 1
            Dim WorkCenterItem As New ListItem
            WorkCenterItem.Value = Trim(dt2.Rows(i).Item("MD001"))
            WorkCenterItem.Text = dt2.Rows(i).Item("MD001") & "-" & dt2.Rows(i).Item("MD002")
            DDLWorkCenter.Items.Add(WorkCenterItem)
        Next
    End Sub
    Private Sub ClearAllButton()
        For i = 1 To 13
            Select Case i
                Case 1
                    ClearButtonStatus(Machine1ATime)
                Case 2
                    ClearButtonStatus(Machine1BTime)
                Case 3
                    ClearButtonStatus(Machine1CTime)
                Case 4
                    ClearButtonStatus(Machine1DTime)
                Case 5
                    ClearButtonStatus(Machine1ETime)
                Case 6
                    ClearButtonStatus(Machine1FTime)
                Case 7
                    ClearButtonStatus(Machine1GTime)
                Case 8
                    ClearButtonStatus(Machine1HTime)
                Case 9
                    ClearButtonStatus(Machine1ITime)
                Case 10
                    ClearButtonStatus(Machine1JTime)
                Case 11
                    ClearButtonStatus(Machine1KTime)
                Case 12
                    ClearButtonStatus(Machine1LTime)
                Case 13
                    ClearButtonStatus(Machine1MTime)
            End Select
        Next
    End Sub
    Private Sub ClearButtonStatus(ByVal ThisMachine() As String)
        For i = 0 To 24
            Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i)) ')
            NowShowTimeButton.Enabled = True
            NowShowTimeButton.Text = ""
            NowShowTimeButton.ForeColor = Drawing.Color.Black
            NowShowTimeButton.BackColor = Drawing.Color.White
            NowShowTimeButton.Width = 120
            If i = 10 Or i = 11 Then
                NowShowTimeButton.Enabled = False
            End If
        Next
    End Sub
    Private Sub InitEveryButton()
        ClearAllButton()
        For i = 1 To 13
            Dim SQL As String = "select * from DailyMachinePlan where WorkDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "'"
            SQL = SQL & " and MachineCode='" & NowMachineNameTitle(i) & "'"
            Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            For L_count = 0 To dt2.Rows.Count - 1
                Dim PlanStartTime As String = dt2.Rows(L_count).Item("PlanStartTime")
                Dim TotalTime As String = dt2.Rows(L_count).Item("TotalTime")
                Dim MoNo As String = dt2.Rows(L_count).Item("MONo")
                Select Case i
                    Case 1
                        ShowMachineStatus(Machine1ATime, PlanStartTime, TotalTime, MoNo)
                    Case 2
                        ShowMachineStatus(Machine1BTime, PlanStartTime, TotalTime, MoNo)
                    Case 3
                        ShowMachineStatus(Machine1CTime, PlanStartTime, TotalTime, MoNo)
                    Case 4
                        ShowMachineStatus(Machine1DTime, PlanStartTime, TotalTime, MoNo)
                    Case 5
                        ShowMachineStatus(Machine1ETime, PlanStartTime, TotalTime, MoNo)
                    Case 6
                        ShowMachineStatus(Machine1FTime, PlanStartTime, TotalTime, MoNo)
                    Case 7
                        ShowMachineStatus(Machine1GTime, PlanStartTime, TotalTime, MoNo)
                    Case 8
                        ShowMachineStatus(Machine1HTime, PlanStartTime, TotalTime, MoNo)
                    Case 9
                        ShowMachineStatus(Machine1ITime, PlanStartTime, TotalTime, MoNo)
                    Case 10
                        ShowMachineStatus(Machine1JTime, PlanStartTime, TotalTime, MoNo)
                    Case 11
                        ShowMachineStatus(Machine1KTime, PlanStartTime, TotalTime, MoNo)
                    Case 12
                        ShowMachineStatus(Machine1LTime, PlanStartTime, TotalTime, MoNo)
                    Case 13
                        ShowMachineStatus(Machine1MTime, PlanStartTime, TotalTime, MoNo)
                End Select
            Next
        Next
    End Sub

    Private Sub ShowMachineStatus(ByVal ThisMachine() As String, ByVal PlanStartTime As String, ByVal TotalTime As String, ByVal MoNo As String)
        Dim ButtonCount As Integer = Math.Ceiling(TotalTime / 1800)
        For i As Integer = 0 To ButtonCount - 1
            Select Case PlanStartTime
                Case "0700"
                    If i >= 23 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 9 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 2)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "0730"
                    If i >= 22 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 8 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 3)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 1)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "0800"
                    If i >= 21 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 7 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 4)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 2)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "0830"
                    If i >= 20 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 6 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 5)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 3)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "0900"
                    If i >= 19 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 5 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 6)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 4)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "0930"
                    If i >= 18 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 4 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 7)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 5)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1000"
                    If i >= 17 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 3 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 8)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 6)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1030"
                    If i >= 16 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 2 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 9)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 7)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1100"
                    If i >= 15 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 1 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 10)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 8)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1130"
                    If i >= 14 Then Exit For
                    Dim NowShowTimeButton As Button
                    If i > 0 Then
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 11)) ')
                    Else
                        NowShowTimeButton = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 9)) ')
                    End If
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1200"
                    If i >= 9 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 10)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1230"
                    If i >= 8 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 11)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1300"
                    If i >= 13 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 12)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1330"
                    If i >= 12 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 13)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1400"
                    If i >= 11 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 14)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1430"
                    If i >= 10 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 15)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1500"
                    If i >= 9 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 16)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)
                Case "1530"
                    If i >= 8 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 17)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)

                Case "1600"
                    If i >= 7 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 18)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)

                Case "1630"
                    If i >= 6 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 19)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)

                Case "1700"
                    If i >= 5 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 20)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)

                Case "1730"
                    If i >= 4 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 21)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)

                Case "1800"
                    If i >= 3 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 22)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)

                Case "1830"
                    If i >= 2 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 23)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)

                Case "1900"
                    If i >= 1 Then Exit For
                    Dim NowShowTimeButton As Button = Page.FindControl("ctl00$MainContent$" & ThisMachine(i + 24)) ')
                    ShowButtonStyle(NowShowTimeButton, MoNo, i, TotalTime)

            End Select

        Next
    End Sub

    Private Sub ShowButtonStyle(ByVal ButtonStyle As Button, ByVal MoNo As String, ByVal IsFirstRow As Integer, ByVal TotalTime As String)

        ButtonStyle.Width = "200"
        ButtonStyle.ForeColor = Drawing.Color.Blue
        If IsFirstRow > 0 Then
            ButtonStyle.Enabled = False
            ButtonStyle.BackColor = Drawing.Color.Red
        Else
            ButtonStyle.Enabled = True
            ButtonStyle.Text = MoNo
            If MoNo = LBMONo.Text Then
                ButtonStyle.BackColor = Drawing.Color.Blue
                ButtonStyle.ForeColor = Drawing.Color.Red
            End If
            ButtonStyle.ToolTip = "(" & (TotalTime / 3600).ToString("##.#") & ")"
        End If

    End Sub


    Private Sub Button_Click(ByVal StartTime As String, ByVal MachineCode As String, sender As Object)
        'Dim SqlMX As String = "select MX001 from CMSMX where MX002='" & Trim(DDLWorkCenter.SelectedValue) & "'"
        'Dim dt2 As DataTable = dbConn.Query(SqlMX, VarIni.ERP, dbConn.WhoCalledMe)

        Dim MachineName As String = ""
        If NowMachineNameTitle(MachineCode) <> "" Then
            MachineName = Trim(NowMachineNameTitle(MachineCode))
        Else
            MachineName = MachineCode
        End If
        If sender.text <> "" Then
            Dim MONo As String = Trim(sender.text)
            Dim strSql As String = ""
            strSql = "delete DailyMachinePlan  where WorkDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "'"
            strSql = strSql & " and MachineCode='" & MachineName & "' and PlanStartTime='" & StartTime & "'"

            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
            strSql = "update PlanSchedule set PlanMachine=null where PlanDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "' and MoType + '-' + RTRIM(MoNo) +'-'+RTRIM(MoSeq)='" & MONo & "'"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
            InitEveryButton()
        Else
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('SelectMo.aspx?StartTime=" & StartTime & "&MachineCode=" & MachineName & "&SelectDate=" & SelectDate & "&WorkCenter=" & WorkCenter & "&MONo=" & LBMONo.Text & "');", True)
        End If
        'Response.Write("<script>window.opener.location.reload();</script>")
        'Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub MachineA1_Click(sender As Object, e As EventArgs) Handles MachineA1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA2_Click(sender As Object, e As EventArgs) Handles MachineA2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA3_Click(sender As Object, e As EventArgs) Handles MachineA3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA4_Click(sender As Object, e As EventArgs) Handles MachineA4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA5_Click(sender As Object, e As EventArgs) Handles MachineA5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA6_Click(sender As Object, e As EventArgs) Handles MachineA6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA7_Click(sender As Object, e As EventArgs) Handles MachineA7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA8_Click(sender As Object, e As EventArgs) Handles MachineA8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA9_Click(sender As Object, e As EventArgs) Handles MachineA9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA10_Click(sender As Object, e As EventArgs) Handles MachineA10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA11_Click(sender As Object, e As EventArgs) Handles MachineA11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA12_Click(sender As Object, e As EventArgs) Handles MachineA12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA13_Click(sender As Object, e As EventArgs) Handles MachineA13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA14_Click(sender As Object, e As EventArgs) Handles MachineA14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA15_Click(sender As Object, e As EventArgs) Handles MachineA15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA16_Click(sender As Object, e As EventArgs) Handles MachineA16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA17_Click(sender As Object, e As EventArgs) Handles MachineA17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA18_Click(sender As Object, e As EventArgs) Handles MachineA18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineA19_Click(sender As Object, e As EventArgs) Handles MachineA19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineA20_Click(sender As Object, e As EventArgs) Handles MachineA20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineA21_Click(sender As Object, e As EventArgs) Handles MachineA21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineA22_Click(sender As Object, e As EventArgs) Handles MachineA22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineA23_Click(sender As Object, e As EventArgs) Handles MachineA23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineA24_Click(sender As Object, e As EventArgs) Handles MachineA24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineA25_Click(sender As Object, e As EventArgs) Handles MachineA25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "1"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineB1_Click(sender As Object, e As EventArgs) Handles MachineB1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB2_Click(sender As Object, e As EventArgs) Handles MachineB2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB3_Click(sender As Object, e As EventArgs) Handles MachineB3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB4_Click(sender As Object, e As EventArgs) Handles MachineB4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB5_Click(sender As Object, e As EventArgs) Handles MachineB5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB6_Click(sender As Object, e As EventArgs) Handles MachineB6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB7_Click(sender As Object, e As EventArgs) Handles MachineB7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB8_Click(sender As Object, e As EventArgs) Handles MachineB8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB9_Click(sender As Object, e As EventArgs) Handles MachineB9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB10_Click(sender As Object, e As EventArgs) Handles MachineB10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB11_Click(sender As Object, e As EventArgs) Handles MachineB11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB12_Click(sender As Object, e As EventArgs) Handles MachineB12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB13_Click(sender As Object, e As EventArgs) Handles MachineB13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB14_Click(sender As Object, e As EventArgs) Handles MachineB14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB15_Click(sender As Object, e As EventArgs) Handles MachineB15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB16_Click(sender As Object, e As EventArgs) Handles MachineB16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB17_Click(sender As Object, e As EventArgs) Handles MachineB17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB18_Click(sender As Object, e As EventArgs) Handles MachineB18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineB19_Click(sender As Object, e As EventArgs) Handles MachineB19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineB20_Click(sender As Object, e As EventArgs) Handles MachineB20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineB21_Click(sender As Object, e As EventArgs) Handles MachineB21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineB22_Click(sender As Object, e As EventArgs) Handles MachineB22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineB23_Click(sender As Object, e As EventArgs) Handles MachineB23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineB24_Click(sender As Object, e As EventArgs) Handles MachineB24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineB25_Click(sender As Object, e As EventArgs) Handles MachineB25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "2"
        Button_Click(StartTime, MachineCode, sender)
    End Sub


    Protected Sub MachineC1_Click(sender As Object, e As EventArgs) Handles MachineC1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC2_Click(sender As Object, e As EventArgs) Handles MachineC2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC3_Click(sender As Object, e As EventArgs) Handles MachineC3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC4_Click(sender As Object, e As EventArgs) Handles MachineC4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC5_Click(sender As Object, e As EventArgs) Handles MachineC5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC6_Click(sender As Object, e As EventArgs) Handles MachineC6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC7_Click(sender As Object, e As EventArgs) Handles MachineC7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC8_Click(sender As Object, e As EventArgs) Handles MachineC8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC9_Click(sender As Object, e As EventArgs) Handles MachineC9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC10_Click(sender As Object, e As EventArgs) Handles MachineC10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC11_Click(sender As Object, e As EventArgs) Handles MachineC11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC12_Click(sender As Object, e As EventArgs) Handles MachineC12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC13_Click(sender As Object, e As EventArgs) Handles MachineC13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC14_Click(sender As Object, e As EventArgs) Handles MachineC14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC15_Click(sender As Object, e As EventArgs) Handles MachineC15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC16_Click(sender As Object, e As EventArgs) Handles MachineC16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC17_Click(sender As Object, e As EventArgs) Handles MachineC17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC18_Click(sender As Object, e As EventArgs) Handles MachineC18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineC19_Click(sender As Object, e As EventArgs) Handles MachineC19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineC20_Click(sender As Object, e As EventArgs) Handles MachineC20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineC21_Click(sender As Object, e As EventArgs) Handles MachineC21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineC22_Click(sender As Object, e As EventArgs) Handles MachineC22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineC23_Click(sender As Object, e As EventArgs) Handles MachineC23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineC24_Click(sender As Object, e As EventArgs) Handles MachineC24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineC25_Click(sender As Object, e As EventArgs) Handles MachineC25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "3"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineD1_Click(sender As Object, e As EventArgs) Handles MachineD1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD2_Click(sender As Object, e As EventArgs) Handles MachineD2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD3_Click(sender As Object, e As EventArgs) Handles MachineD3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD4_Click(sender As Object, e As EventArgs) Handles MachineD4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD5_Click(sender As Object, e As EventArgs) Handles MachineD5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD6_Click(sender As Object, e As EventArgs) Handles MachineD6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD7_Click(sender As Object, e As EventArgs) Handles MachineD7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD8_Click(sender As Object, e As EventArgs) Handles MachineD8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD9_Click(sender As Object, e As EventArgs) Handles MachineD9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD10_Click(sender As Object, e As EventArgs) Handles MachineD10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD11_Click(sender As Object, e As EventArgs) Handles MachineD11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD12_Click(sender As Object, e As EventArgs) Handles MachineD12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD13_Click(sender As Object, e As EventArgs) Handles MachineD13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD14_Click(sender As Object, e As EventArgs) Handles MachineD14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD15_Click(sender As Object, e As EventArgs) Handles MachineD15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD16_Click(sender As Object, e As EventArgs) Handles MachineD16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD17_Click(sender As Object, e As EventArgs) Handles MachineD17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD18_Click(sender As Object, e As EventArgs) Handles MachineD18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineD19_Click(sender As Object, e As EventArgs) Handles MachineD19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineD20_Click(sender As Object, e As EventArgs) Handles MachineD20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineD21_Click(sender As Object, e As EventArgs) Handles MachineD21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineD22_Click(sender As Object, e As EventArgs) Handles MachineD22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineD23_Click(sender As Object, e As EventArgs) Handles MachineD23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineD24_Click(sender As Object, e As EventArgs) Handles MachineD24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineD25_Click(sender As Object, e As EventArgs) Handles MachineD25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "4"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineE1_Click(sender As Object, e As EventArgs) Handles MachineE1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE2_Click(sender As Object, e As EventArgs) Handles MachineE2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE3_Click(sender As Object, e As EventArgs) Handles MachineE3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE4_Click(sender As Object, e As EventArgs) Handles MachineE4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE5_Click(sender As Object, e As EventArgs) Handles MachineE5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE6_Click(sender As Object, e As EventArgs) Handles MachineE6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE7_Click(sender As Object, e As EventArgs) Handles MachineE7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE8_Click(sender As Object, e As EventArgs) Handles MachineE8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE9_Click(sender As Object, e As EventArgs) Handles MachineE9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE10_Click(sender As Object, e As EventArgs) Handles MachineE10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE11_Click(sender As Object, e As EventArgs) Handles MachineE11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE12_Click(sender As Object, e As EventArgs) Handles MachineE12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE13_Click(sender As Object, e As EventArgs) Handles MachineE13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE14_Click(sender As Object, e As EventArgs) Handles MachineE14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE15_Click(sender As Object, e As EventArgs) Handles MachineE15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE16_Click(sender As Object, e As EventArgs) Handles MachineE16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE17_Click(sender As Object, e As EventArgs) Handles MachineE17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE18_Click(sender As Object, e As EventArgs) Handles MachineE18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineE19_Click(sender As Object, e As EventArgs) Handles MachineE19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineE20_Click(sender As Object, e As EventArgs) Handles MachineE20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineE21_Click(sender As Object, e As EventArgs) Handles MachineE21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineE22_Click(sender As Object, e As EventArgs) Handles MachineE22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineE23_Click(sender As Object, e As EventArgs) Handles MachineE23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineE24_Click(sender As Object, e As EventArgs) Handles MachineE24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineE25_Click(sender As Object, e As EventArgs) Handles MachineE25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "5"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineF1_Click(sender As Object, e As EventArgs) Handles MachineF1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF2_Click(sender As Object, e As EventArgs) Handles MachineF2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF3_Click(sender As Object, e As EventArgs) Handles MachineF3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF4_Click(sender As Object, e As EventArgs) Handles MachineF4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF5_Click(sender As Object, e As EventArgs) Handles MachineF5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF6_Click(sender As Object, e As EventArgs) Handles MachineF6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF7_Click(sender As Object, e As EventArgs) Handles MachineF7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF8_Click(sender As Object, e As EventArgs) Handles MachineF8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF9_Click(sender As Object, e As EventArgs) Handles MachineF9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF10_Click(sender As Object, e As EventArgs) Handles MachineF10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF11_Click(sender As Object, e As EventArgs) Handles MachineF11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF12_Click(sender As Object, e As EventArgs) Handles MachineF12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF13_Click(sender As Object, e As EventArgs) Handles MachineF13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF14_Click(sender As Object, e As EventArgs) Handles MachineF14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF15_Click(sender As Object, e As EventArgs) Handles MachineF15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF16_Click(sender As Object, e As EventArgs) Handles MachineF16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF17_Click(sender As Object, e As EventArgs) Handles MachineF17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF18_Click(sender As Object, e As EventArgs) Handles MachineF18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineF19_Click(sender As Object, e As EventArgs) Handles MachineF19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineF20_Click(sender As Object, e As EventArgs) Handles MachineF20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineF21_Click(sender As Object, e As EventArgs) Handles MachineF21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineF22_Click(sender As Object, e As EventArgs) Handles MachineF22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineF23_Click(sender As Object, e As EventArgs) Handles MachineF23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineF24_Click(sender As Object, e As EventArgs) Handles MachineF24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineF25_Click(sender As Object, e As EventArgs) Handles MachineF25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "6"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineG1_Click(sender As Object, e As EventArgs) Handles MachineG1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG2_Click(sender As Object, e As EventArgs) Handles MachineG2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG3_Click(sender As Object, e As EventArgs) Handles MachineG3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG4_Click(sender As Object, e As EventArgs) Handles MachineG4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG5_Click(sender As Object, e As EventArgs) Handles MachineG5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG6_Click(sender As Object, e As EventArgs) Handles MachineG6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG7_Click(sender As Object, e As EventArgs) Handles MachineG7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG8_Click(sender As Object, e As EventArgs) Handles MachineG8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG9_Click(sender As Object, e As EventArgs) Handles MachineG9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG10_Click(sender As Object, e As EventArgs) Handles MachineG10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG11_Click(sender As Object, e As EventArgs) Handles MachineG11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG12_Click(sender As Object, e As EventArgs) Handles MachineG12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG13_Click(sender As Object, e As EventArgs) Handles MachineG13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG14_Click(sender As Object, e As EventArgs) Handles MachineG14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG15_Click(sender As Object, e As EventArgs) Handles MachineG15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG16_Click(sender As Object, e As EventArgs) Handles MachineG16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG17_Click(sender As Object, e As EventArgs) Handles MachineG17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG18_Click(sender As Object, e As EventArgs) Handles MachineG18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineG19_Click(sender As Object, e As EventArgs) Handles MachineG19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineG20_Click(sender As Object, e As EventArgs) Handles MachineG20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineG21_Click(sender As Object, e As EventArgs) Handles MachineG21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineG22_Click(sender As Object, e As EventArgs) Handles MachineG22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineG23_Click(sender As Object, e As EventArgs) Handles MachineG23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineG24_Click(sender As Object, e As EventArgs) Handles MachineG24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineG25_Click(sender As Object, e As EventArgs) Handles MachineG25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "7"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineH1_Click(sender As Object, e As EventArgs) Handles MachineH1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH2_Click(sender As Object, e As EventArgs) Handles MachineH2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH3_Click(sender As Object, e As EventArgs) Handles MachineH3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH4_Click(sender As Object, e As EventArgs) Handles MachineH4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH5_Click(sender As Object, e As EventArgs) Handles MachineH5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH6_Click(sender As Object, e As EventArgs) Handles MachineH6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH7_Click(sender As Object, e As EventArgs) Handles MachineH7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH8_Click(sender As Object, e As EventArgs) Handles MachineH8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH9_Click(sender As Object, e As EventArgs) Handles MachineH9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH10_Click(sender As Object, e As EventArgs) Handles MachineH10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH11_Click(sender As Object, e As EventArgs) Handles MachineH11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH12_Click(sender As Object, e As EventArgs) Handles MachineH12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH13_Click(sender As Object, e As EventArgs) Handles MachineH13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH14_Click(sender As Object, e As EventArgs) Handles MachineH14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH15_Click(sender As Object, e As EventArgs) Handles MachineH15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH16_Click(sender As Object, e As EventArgs) Handles MachineH16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH17_Click(sender As Object, e As EventArgs) Handles MachineH17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH18_Click(sender As Object, e As EventArgs) Handles MachineH18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineH19_Click(sender As Object, e As EventArgs) Handles MachineH19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineH20_Click(sender As Object, e As EventArgs) Handles MachineH20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineH21_Click(sender As Object, e As EventArgs) Handles MachineH21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineH22_Click(sender As Object, e As EventArgs) Handles MachineH22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineH23_Click(sender As Object, e As EventArgs) Handles MachineH23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineH24_Click(sender As Object, e As EventArgs) Handles MachineH24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineH25_Click(sender As Object, e As EventArgs) Handles MachineH25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "8"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineI1_Click(sender As Object, e As EventArgs) Handles MachineI1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI2_Click(sender As Object, e As EventArgs) Handles MachineI2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI3_Click(sender As Object, e As EventArgs) Handles MachineI3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI4_Click(sender As Object, e As EventArgs) Handles MachineI4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI5_Click(sender As Object, e As EventArgs) Handles MachineI5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI6_Click(sender As Object, e As EventArgs) Handles MachineI6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI7_Click(sender As Object, e As EventArgs) Handles MachineI7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI8_Click(sender As Object, e As EventArgs) Handles MachineI8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI9_Click(sender As Object, e As EventArgs) Handles MachineI9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI10_Click(sender As Object, e As EventArgs) Handles MachineI10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI11_Click(sender As Object, e As EventArgs) Handles MachineI11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI12_Click(sender As Object, e As EventArgs) Handles MachineI12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI13_Click(sender As Object, e As EventArgs) Handles MachineI13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI14_Click(sender As Object, e As EventArgs) Handles MachineI14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI15_Click(sender As Object, e As EventArgs) Handles MachineI15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI16_Click(sender As Object, e As EventArgs) Handles MachineI16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI17_Click(sender As Object, e As EventArgs) Handles MachineI17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI18_Click(sender As Object, e As EventArgs) Handles MachineI18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineI19_Click(sender As Object, e As EventArgs) Handles MachineI19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineI20_Click(sender As Object, e As EventArgs) Handles MachineI20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineI21_Click(sender As Object, e As EventArgs) Handles MachineI21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineI22_Click(sender As Object, e As EventArgs) Handles MachineI22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineI23_Click(sender As Object, e As EventArgs) Handles MachineI23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineI24_Click(sender As Object, e As EventArgs) Handles MachineI24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineI25_Click(sender As Object, e As EventArgs) Handles MachineI25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "9"
        Button_Click(StartTime, MachineCode, sender)
    End Sub


    Protected Sub MachineJ1_Click(sender As Object, e As EventArgs) Handles MachineJ1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ2_Click(sender As Object, e As EventArgs) Handles MachineJ2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ3_Click(sender As Object, e As EventArgs) Handles MachineJ3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ4_Click(sender As Object, e As EventArgs) Handles MachineJ4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ5_Click(sender As Object, e As EventArgs) Handles MachineJ5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ6_Click(sender As Object, e As EventArgs) Handles MachineJ6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ7_Click(sender As Object, e As EventArgs) Handles MachineJ7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ8_Click(sender As Object, e As EventArgs) Handles MachineJ8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ9_Click(sender As Object, e As EventArgs) Handles MachineJ9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ10_Click(sender As Object, e As EventArgs) Handles MachineJ10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ11_Click(sender As Object, e As EventArgs) Handles MachineJ11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ12_Click(sender As Object, e As EventArgs) Handles MachineJ12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ13_Click(sender As Object, e As EventArgs) Handles MachineJ13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ14_Click(sender As Object, e As EventArgs) Handles MachineJ14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ15_Click(sender As Object, e As EventArgs) Handles MachineJ15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ16_Click(sender As Object, e As EventArgs) Handles MachineJ16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ17_Click(sender As Object, e As EventArgs) Handles MachineJ17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ18_Click(sender As Object, e As EventArgs) Handles MachineJ18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineJ19_Click(sender As Object, e As EventArgs) Handles MachineJ19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineJ20_Click(sender As Object, e As EventArgs) Handles MachineJ20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineJ21_Click(sender As Object, e As EventArgs) Handles MachineJ21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineJ22_Click(sender As Object, e As EventArgs) Handles MachineJ22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineJ23_Click(sender As Object, e As EventArgs) Handles MachineJ23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineJ24_Click(sender As Object, e As EventArgs) Handles MachineJ24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineJ25_Click(sender As Object, e As EventArgs) Handles MachineJ25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "10"
        Button_Click(StartTime, MachineCode, sender)
    End Sub


    Protected Sub MachineK1_Click(sender As Object, e As EventArgs) Handles MachineK1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK2_Click(sender As Object, e As EventArgs) Handles MachineK2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK3_Click(sender As Object, e As EventArgs) Handles MachineK3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK4_Click(sender As Object, e As EventArgs) Handles MachineK4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK5_Click(sender As Object, e As EventArgs) Handles MachineK5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK6_Click(sender As Object, e As EventArgs) Handles MachineK6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK7_Click(sender As Object, e As EventArgs) Handles MachineK7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK8_Click(sender As Object, e As EventArgs) Handles MachineK8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK9_Click(sender As Object, e As EventArgs) Handles MachineK9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK10_Click(sender As Object, e As EventArgs) Handles MachineK10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK11_Click(sender As Object, e As EventArgs) Handles MachineK11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK12_Click(sender As Object, e As EventArgs) Handles MachineK12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK13_Click(sender As Object, e As EventArgs) Handles MachineK13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK14_Click(sender As Object, e As EventArgs) Handles MachineK14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK15_Click(sender As Object, e As EventArgs) Handles MachineK15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK16_Click(sender As Object, e As EventArgs) Handles MachineK16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK17_Click(sender As Object, e As EventArgs) Handles MachineK17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK18_Click(sender As Object, e As EventArgs) Handles MachineK18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineK19_Click(sender As Object, e As EventArgs) Handles MachineK19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineK20_Click(sender As Object, e As EventArgs) Handles MachineK20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineK21_Click(sender As Object, e As EventArgs) Handles MachineK21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineK22_Click(sender As Object, e As EventArgs) Handles MachineK22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineK23_Click(sender As Object, e As EventArgs) Handles MachineK23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineK24_Click(sender As Object, e As EventArgs) Handles MachineK24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineK25_Click(sender As Object, e As EventArgs) Handles MachineK25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "11"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineL1_Click(sender As Object, e As EventArgs) Handles MachineL1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL2_Click(sender As Object, e As EventArgs) Handles MachineL2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL3_Click(sender As Object, e As EventArgs) Handles MachineL3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL4_Click(sender As Object, e As EventArgs) Handles MachineL4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL5_Click(sender As Object, e As EventArgs) Handles MachineL5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL6_Click(sender As Object, e As EventArgs) Handles MachineL6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL7_Click(sender As Object, e As EventArgs) Handles MachineL7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL8_Click(sender As Object, e As EventArgs) Handles MachineL8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL9_Click(sender As Object, e As EventArgs) Handles MachineL9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL10_Click(sender As Object, e As EventArgs) Handles MachineL10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL11_Click(sender As Object, e As EventArgs) Handles MachineL11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL12_Click(sender As Object, e As EventArgs) Handles MachineL12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL13_Click(sender As Object, e As EventArgs) Handles MachineL13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL14_Click(sender As Object, e As EventArgs) Handles MachineL14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL15_Click(sender As Object, e As EventArgs) Handles MachineL15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL16_Click(sender As Object, e As EventArgs) Handles MachineL16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL17_Click(sender As Object, e As EventArgs) Handles MachineL17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL18_Click(sender As Object, e As EventArgs) Handles MachineL18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineL19_Click(sender As Object, e As EventArgs) Handles MachineL19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineL20_Click(sender As Object, e As EventArgs) Handles MachineL20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineL21_Click(sender As Object, e As EventArgs) Handles MachineL21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineL22_Click(sender As Object, e As EventArgs) Handles MachineL22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineL23_Click(sender As Object, e As EventArgs) Handles MachineL23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineL24_Click(sender As Object, e As EventArgs) Handles MachineL24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineL25_Click(sender As Object, e As EventArgs) Handles MachineL25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "12"
        Button_Click(StartTime, MachineCode, sender)
    End Sub


    Protected Sub MachineM1_Click(sender As Object, e As EventArgs) Handles MachineM1.Click
        Dim StartTime As String = "0700"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM2_Click(sender As Object, e As EventArgs) Handles MachineM2.Click
        Dim StartTime As String = "0730"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM3_Click(sender As Object, e As EventArgs) Handles MachineM3.Click
        Dim StartTime As String = "0800"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM4_Click(sender As Object, e As EventArgs) Handles MachineM4.Click
        Dim StartTime As String = "0830"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM5_Click(sender As Object, e As EventArgs) Handles MachineM5.Click
        Dim StartTime As String = "0900"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM6_Click(sender As Object, e As EventArgs) Handles MachineM6.Click
        Dim StartTime As String = "0930"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM7_Click(sender As Object, e As EventArgs) Handles MachineM7.Click
        Dim StartTime As String = "1000"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM8_Click(sender As Object, e As EventArgs) Handles MachineM8.Click
        Dim StartTime As String = "1030"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM9_Click(sender As Object, e As EventArgs) Handles MachineM9.Click
        Dim StartTime As String = "1100"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM10_Click(sender As Object, e As EventArgs) Handles MachineM10.Click
        Dim StartTime As String = "1130"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM11_Click(sender As Object, e As EventArgs) Handles MachineM11.Click
        Dim StartTime As String = "1200"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM12_Click(sender As Object, e As EventArgs) Handles MachineM12.Click
        Dim StartTime As String = "1230"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM13_Click(sender As Object, e As EventArgs) Handles MachineM13.Click
        Dim StartTime As String = "1300"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM14_Click(sender As Object, e As EventArgs) Handles MachineM14.Click
        Dim StartTime As String = "1330"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM15_Click(sender As Object, e As EventArgs) Handles MachineM15.Click
        Dim StartTime As String = "1400"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM16_Click(sender As Object, e As EventArgs) Handles MachineM16.Click
        Dim StartTime As String = "1430"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM17_Click(sender As Object, e As EventArgs) Handles MachineM17.Click
        Dim StartTime As String = "1500"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM18_Click(sender As Object, e As EventArgs) Handles MachineM18.Click
        Dim StartTime As String = "1530"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub
    Protected Sub MachineM19_Click(sender As Object, e As EventArgs) Handles MachineM19.Click
        Dim StartTime As String = "1600"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineM20_Click(sender As Object, e As EventArgs) Handles MachineM20.Click
        Dim StartTime As String = "1630"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineM21_Click(sender As Object, e As EventArgs) Handles MachineM21.Click
        Dim StartTime As String = "1700"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineM22_Click(sender As Object, e As EventArgs) Handles MachineM22.Click
        Dim StartTime As String = "1730"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineM23_Click(sender As Object, e As EventArgs) Handles MachineM23.Click
        Dim StartTime As String = "1800"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineM24_Click(sender As Object, e As EventArgs) Handles MachineM24.Click
        Dim StartTime As String = "1830"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub

    Protected Sub MachineM25_Click(sender As Object, e As EventArgs) Handles MachineM25.Click
        Dim StartTime As String = "1900"
        Dim MachineCode As String = "13"
        Button_Click(StartTime, MachineCode, sender)
    End Sub



    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SelectDate = Date1.Text
        WorkCenter = DDLWorkCenter.SelectedValue
        InitEveryButton()
        'Response.Redirect("MachineDailyPlan.aspx")
        'Response.Redirect("MachineDailyPlan.aspx?SelectDate=" & Date1.Text & "&WorkCenter=" & DDLWorkCenter.SelectedValue)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim StrDocDate As String = Date1.Text
        Dim StrDocNo As String = ""
        Dim StrDocTime As String = ""
        Dim StrDocNoSeq As Integer = 0
        Dim StrWorkCenter As String = DDLWorkCenter.SelectedValue



        Dim sqlSetOrder As String = "select WorkDate,PlanStartTime from DailyMachinePlan where WorkDate='" & StrDocDate & "' and WorkCenter='" & StrWorkCenter & "' group by WorkDate,PlanStartTime"
        Dim dtOrder As DataTable = dbConn.Query(sqlSetOrder, VarIni.DBMIS, dbConn.WhoCalledMe)

        Dim sqlDelete As String = ""
        For L_Count As Integer = 0 To dtOrder.Rows.Count - 1
            Dim sqlMoldOrder As String = "select DocNo from SetMoldOrder where DocDate='" & StrDocDate & "'  and Remark='" & DDLWorkCenter.SelectedValue & "'"
            Dim dtMoldOrder As DataTable = dbConn.Query(sqlMoldOrder, VarIni.DBMIS, dbConn.WhoCalledMe)
            If dtMoldOrder.Rows.Count > 0 Then
                sqlDelete = sqlDelete & "delete SetMoldOrder where DocNo='" & dtMoldOrder.Rows(0).Item(0) & "';"
                sqlDelete = sqlDelete & "delete SetMoldDetail where DocNo='" & dtMoldOrder.Rows(0).Item(0) & "';"
                Dim dtDelete As DataTable = dbConn.Query(sqlDelete, VarIni.DBMIS, dbConn.WhoCalledMe)
            End If
        Next


        Dim SQL As String = "select max(right(DocNo,3))+1 from SetMoldOrder where DocDate='" & StrDocDate & "'"
        Dim dt1 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        If IsDBNull(dt1.Rows(0).Item(0)) Then
            'StrDocNo = Date1.Text & "001"
            StrDocNoSeq = "1"
        Else
            'StrDocNo = Date1.Text & dt1.Rows(0).Item(0).ToString.PadLeft(3, "0")
            StrDocNoSeq = dt1.Rows(0).Item(0)
        End If

        For L_count As Integer = 0 To dtOrder.Rows.Count - 1
            StrDocNo = StrDocDate & StrDocNoSeq.ToString.PadLeft(3, "0")
            StrDocNoSeq = StrDocNoSeq + 1
            StrDocTime = dtOrder.Rows(L_count).Item(1)
            SQL = "insert into SetMoldOrder (DocNo,DocDate,DocTime,Remark) values('" &
                    StrDocNo & "','" & StrDocDate & "','" & StrDocTime & "','" & DDLWorkCenter.SelectedValue & "');"
            'Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
            Dim sqlMoldDetail As String = "select *  from DailyMachinePlan where WorkDate='" & StrDocDate & "' and WorkCenter='" & StrWorkCenter & "' and PlanStartTime='" & StrDocTime & "'"
            Dim dtDetail As DataTable = dbConn.Query(sqlMoldDetail, VarIni.DBMIS, dbConn.WhoCalledMe)
            For K_count As Integer = 0 To dtDetail.Rows.Count - 1
                Dim MOGroup() As String = dtDetail.Rows(K_count).Item("MONo").ToString.Split("-")
                'Dim sqlMachineNo As String = "select MX001 from CMSMX where MX002='" & DDLWorkCenter.SelectedValue & "'"
                'Dim dtMachineNo As DataTable = dbConn.Query(sqlMachineNo, VarIni.ERP, dbConn.WhoCalledMe)

                Dim SqlData As String = "select MB001,MB002,MB003,MOC.TA015,SFC.TA003,SFC.TA004,MW002,SFC.TA006,MD002,BOM.MF010,BOM.MF025" &
            " From INVMB " &
    "Left join MOCTA MOC on TA006=MB001 " &
    "left join SFCTA SFC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
    "left join BOMMF BOM on MF001=MB001 and MF003=SFC.TA003 " &
    "left join CMSMD on MD001=SFC.TA006 " &
    "left join CMSMW on MW001=SFC.TA004 " &
    "where MOC.TA001='" & MOGroup(0) & "' and MOC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
                Dim dtData As DataTable = dbConn.Query(SqlData, VarIni.ERP, dbConn.WhoCalledMe)

                Dim HoursValue As Integer = Math.Floor((dtDetail.Rows(K_count).Item("Qty") * dtData.Rows(0).Item("MF025")) / 3600)
                Dim MinValue As Integer = Math.Ceiling(dtDetail.Rows(K_count).Item("Qty") * dtData.Rows(0).Item("MF025") / 60) Mod 60



                SQL = SQL & "insert into SetMoldDetail values('" & StrDocNo & "'" &
                ",'" & K_count + 1 & "'" &
                ",'" & MOGroup(0) & "-" & MOGroup(1) & "'" &
                ",'" & MOGroup(2) & "'" &
                ",'" & Trim(dtDetail.Rows(K_count).Item("MachineCode")) & "'" &
                ",'" & dtDetail.Rows(K_count).Item("Qty") & "'" &
                ",'" & Trim(dtData.Rows(0).Item("MB001")) & "'" &
                ",'" & Trim(dtData.Rows(0).Item("MB003")) & "'" &
                ",''" &
                ",''" &
                ",'" & Trim(dtData.Rows(0).Item("MW002")) & "'" &
                ",'" & Trim(dtData.Rows(0).Item("MF025")) & "'" &
                ",'" & HoursValue & "H " & MinValue & "M" & "'" &
            ");"

            Next
            Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
        Next
        show_message.ShowMessage(Page, "Export Finished", UpdatePanel1)
    End Sub
End Class