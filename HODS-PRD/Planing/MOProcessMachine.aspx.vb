Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class MOProcessMachine
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim dbConn As New DataConnectControl
    Dim SelectDate As String = ""
    Dim WorkCenter As String = ""
    Dim NowSelectMachine As String
    Dim MachineCode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            'InitDDLWorkCenter()
        End If
        'If Date1.Text = "" Then
        '    Date1.Text = Year(Now) & Month(Now).ToString.PadLeft(2, "0") & Day(Now).ToString.PadLeft(2, "0")
        'End If
        'If TBSpec.Text <> "" Or (Date1.Text <> "" And DDLWorkCenter.SelectedValue <> "0") Then
        '    SearchButton()
        'End If
    End Sub

    'Private Sub InitDDLWorkCenter()
    '    Dim SQL As String = "select * from CMSMD"
    '    Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
    '    For i = 0 To dt2.Rows.Count - 1
    '        Dim WorkCenterItem As New ListItem
    '        WorkCenterItem.Value = Trim(dt2.Rows(i).Item("MD001"))
    '        WorkCenterItem.Text = dt2.Rows(i).Item("MD001") & "-" & dt2.Rows(i).Item("MD002")
    '        DDLWorkCenter.Items.Add(WorkCenterItem)
    '    Next
    'End Sub
    Protected Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        'ShowPanel.
        SearchButton()
    End Sub

    Sub SearchButton()
        Dim SqlMO As String = ""
        SqlMO = "select MOC.TA001, MOC.TA002,MB001,MB002,MB003,MOC.TA015" &
                " From MOCTA MOC " &
                " Left Join INVMB on MOC.TA006=MB001 " &
                " where MOC.TA013 ='Y' and MOC.TA011<>'Y' and MOC.TA011<>'y'" &
                " And MB003 like '%" & TBSpec.Text & "%'" &
                "order by MOC.TA001, MOC.TA002"
        Dim dtMO As DataTable = dbConn.Query(SqlMO, VarIni.ERP, dbConn.WhoCalledMe)     'Find MO




        For i = 0 To dtMO.Rows.Count - 1
            Dim MOType As String = dtMO.Rows(i).Item("TA001")
            Dim MONo As String = dtMO.Rows(i).Item("TA002")
            Dim NowShowMO As String = MOType & "-" & MONo


            Dim MOPanel As New Panel                    'MO Main Panel
            'MOPanel.CssClass = "inlineBlock"
            MOPanel.CssClass = "MainPanel"
            MOPanel.BorderWidth = 1
            MOPanel.ID = NowShowMO

            Dim MODetailPanel As New Panel              'MO Detail Data Panel
            MODetailPanel.CssClass = "inlineBlock"
            MODetailPanel.Width = 220

            Dim MOName As New Literal                   'MO Detail Data1 MO Number
            MOName.Text = " MO:" & NowShowMO
            MODetailPanel.Controls.Add(MOName)

            Dim BRchangeLine As New HtmlAnchor          'Change Line
            BRchangeLine.InnerHtml = "<br>"
            MODetailPanel.Controls.Add(BRchangeLine)

            Dim ItemNo As New Literal                   'MO Detail Data2 Item Number
            ItemNo.Text = " Item:" & dtMO.Rows(i).Item("MB001") '& "-" & dtMO.Rows(i).Item("MB002") & "-" & dtMO.Rows(i).Item("MB003")
            MODetailPanel.Controls.Add(ItemNo)

            Dim BRchangeLineM1 As New HtmlAnchor        'Change Line
            BRchangeLineM1.InnerHtml = "<br>"
            MODetailPanel.Controls.Add(BRchangeLineM1)

            Dim ItemDesc As New Literal                 'MO Detail Data3 Item Descript
            ItemDesc.Text = " Desc:" & dtMO.Rows(i).Item("MB002") '& "-" & dtMO.Rows(i).Item("MB003")
            MODetailPanel.Controls.Add(ItemDesc)

            Dim BRchangeLineM2 As New HtmlAnchor        'Change Line
            BRchangeLineM2.InnerHtml = "<br>"
            MODetailPanel.Controls.Add(BRchangeLineM2)

            Dim ItemSpec As New Literal                 'MO Detail Data3 Item Spec
            ItemSpec.Text = " Spec:" & "-" & dtMO.Rows(i).Item("MB003")
            MODetailPanel.Controls.Add(ItemSpec)

            Dim BRchangeLineM3 As New HtmlAnchor        'Change Line
            BRchangeLineM3.InnerHtml = "<br>"
            MODetailPanel.Controls.Add(BRchangeLineM3)

            Dim MOQty As New Literal                 'MO Detail Data3 Item Spec
            MOQty.Text = " MOQty:" & "-" & Math.Round(dtMO.Rows(i).Item("TA015"), 0)
            MODetailPanel.Controls.Add(MOQty)

            MOPanel.Controls.Add(MODetailPanel)


            Dim SqlProcess As String = "Select SFC.TA001, SFC.TA002,SFC.TA003,SFC.TA004,SFC.TA006,SFC.TA007,SFC.TA010,SFC.TA011,MOC.TA015,MB001,MB002,MB003" &
                                 " From SFCTA SFC " &
                                 " Left Join MOCTA MOC On MOC.TA001=SFC.TA001 And MOC.TA002=SFC.TA002 " &
                                 " Left Join INVMB On MOC.TA006=MB001 " &
                                 " where MOC.TA013 ='Y' and MOC.TA011<>'Y' and MOC.TA011<>'y' " &
                                 " and MOC.TA001='" & MOType & "' and MOC.TA002='" & MONo & "'" &
                                 " order by SFC.TA001, SFC.TA002, SFC.TA003"
            Dim dtProcess As DataTable = dbConn.Query(SqlProcess, VarIni.ERP, dbConn.WhoCalledMe)



            For L_count = 0 To dtProcess.Rows.Count - 1
                Dim CompleteFlag As Integer = 0
                Dim ProcessPanel As New Panel
                ProcessPanel.BorderWidth = 1
                ProcessPanel.Width = 220
                ProcessPanel.ID = dtProcess.Rows(L_count).Item("TA003") & "-" & dtProcess.Rows(L_count).Item("TA004")
                ProcessPanel.CssClass = "inlineBlock"

                Dim ProcessCode As New HyperLink            '1
                ProcessCode.Enabled = False
                ProcessCode.CssClass = "ButtonCss"
                ProcessCode.BorderStyle = BorderStyle.None
                ProcessCode.BorderWidth = 0
                Dim ShowProcessCode As String = dtProcess.Rows(L_count).Item("TA003") & "-" & dtProcess.Rows(L_count).Item("TA004")
                ShowProcessCode = ShowProcessCode & "(" & Math.Round(dtProcess.Rows(L_count).Item("TA011"), 0) & ")"
                'If dtProcess.Rows(L_count).Item("TA011") >= dtProcess.Rows(L_count).Item("TA015") Then
                '    ShowProcessCode = ShowProcessCode & "(Complete)"
                '    ProcessCode.ForeColor = Drawing.Color.Aqua
                '    CompleteFlag = 1
                'Else
                '    ShowProcessCode = ShowProcessCode & "(UnComplete)"
                '    ProcessCode.ForeColor = Drawing.Color.Brown
                'End If
                ProcessCode.Text = ShowProcessCode
                ProcessPanel.Controls.Add(ProcessCode)

                Dim BRchangeLine1 As New HtmlAnchor         'change Line
                BRchangeLine1.InnerHtml = "<br>"
                ProcessPanel.Controls.Add(BRchangeLine1)

                Dim ProcessName As New Literal              '2
                ProcessName.Text = dtProcess.Rows(L_count).Item("TA006") & "-" & dtProcess.Rows(L_count).Item("TA007")
                ProcessPanel.Controls.Add(ProcessName)

                Dim BRchangeLine2 As New HtmlAnchor         'change Line
                BRchangeLine2.InnerHtml = "<br>"
                ProcessPanel.Controls.Add(BRchangeLine2)

                Dim SqlPlanSchedule As String = "select NowDate,MachineID,sum(Qty) from MOWorkRecord where MONo='" & MOType & "-" & MONo & "-" & dtProcess.Rows(L_count).Item("TA003") & "'" &
                " and EndTime is not null Group by NowDate,MachineID" '"and PlanDate >=getdate()-1"
                Dim dtPlanSchedule As DataTable = dbConn.Query(SqlPlanSchedule, VarIni.DBMIS, dbConn.WhoCalledMe)

                For J_count As Integer = 0 To dtPlanSchedule.Rows.Count - 1         'Group 3
                    Dim Planned As New HyperLink
                    Planned.CssClass = "HyperLinkCss"
                    Planned.Text = dtPlanSchedule.Rows(J_count).Item("MachineID") & "-" & dtPlanSchedule.Rows(J_count).Item("NowDate") & "-" & dtPlanSchedule.Rows(J_count).Item(2)
                    'Dim SqlMachinePlan As String = "select MachineCode,PlanStartTime from DailyMachinePlan where WorkDate='" & dtPlanSchedule.Rows(J_count).Item("PlanDate") & "' and MONo='" & NowShowMO & "-" & dtProcess.Rows(L_count).Item("TA003") & "'"
                    'Dim dtMachinePlan As DataTable = dbConn.Query(SqlMachinePlan, VarIni.DBMIS, dbConn.WhoCalledMe)
                    'If dtMachinePlan.Rows.Count > 0 Then
                    '    Planned.ForeColor = Drawing.Color.Blue
                    '    Planned.Attributes.Add("onclick", "window.open('MachineDailyPlan.aspx?SelectDate=" & dtPlanSchedule.Rows(J_count).Item("PlanDate") & "&WorkCenter=" & Trim(dtProcess.Rows(L_count).Item("TA006")) & "&MONo=" & NowShowMO & "-" & dtProcess.Rows(L_count).Item("TA003") & "');")
                    '    Planned.Text = dtPlanSchedule.Rows(J_count).Item("PlanDate") & "(" & dtMachinePlan.Rows(0).Item("MachineCode") & "-" & dtMachinePlan.Rows(0).Item("PlanStartTime") & ")" & "   "
                    'Else
                    '    Planned.Text = "[" & dtPlanSchedule.Rows(J_count).Item("PlanDate") & "]   "
                    '    Planned.ForeColor = Drawing.Color.Crimson
                    '    Planned.Attributes.Add("onclick", "window.open('MachineDailyPlan.aspx?SelectDate=" & dtPlanSchedule.Rows(J_count).Item("PlanDate") & "&WorkCenter=" & Trim(dtProcess.Rows(L_count).Item("TA006")) & "&MONo=" & NowShowMO & "-" & dtProcess.Rows(L_count).Item("TA003") & "');")
                    'End If
                    ProcessPanel.Controls.Add(Planned)

                    Dim BRchangeLine3 As New HtmlAnchor         'change Line
                    BRchangeLine3.InnerHtml = "<br>"
                    ProcessPanel.Controls.Add(BRchangeLine3)
                Next



                MOPanel.Controls.Add(ProcessPanel)
            Next
            ShowPanel.Controls.Add(MOPanel)
        Next
        'Else
        '    show_message.ShowMessage(Page, "please input the Spec", UpdatePanel1)
        'End If
    End Sub

    Protected Sub buttonforMachine(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('SelectMo.aspx');", True)
    End Sub
End Class