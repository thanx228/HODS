Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Public Class ProcessEfficiency
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim MonthText() As String = {"", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("UserName") = "" Then
            '    Response.Redirect("Login.aspx")
            'End If
            If TbTarget.Text = "" Then
                TbTarget.Text = "85"
            End If
            If TbTime.Text = "" Then
                TbTime.Text = "1280"
            End If

            'Dim Program As New DataTable
            'Program = Conn_SQL.Get_DataReader("Select MD001,MD002 from CMSMD order by MD002", Conn_SQL.ERP_ConnectionString) ' Machines
            'For i As Integer = 0 To Program.Rows.Count - 1
            '    ddlProcess.Items.Add(New ListItem(Program.Rows(i).Item("MD002"), Program.Rows(i).Item("MD001")))
            'Next

            InitDeptID()
        End If

        'DDLChange()
    End Sub
    Protected Sub BtPerMC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtPerMC.Click
        InsertTableMC()
        Dim Selmonth As Integer = ddlMonth.SelectedItem.Value
        Dim Tmonth As String = MonthText(CInt(Selmonth))
        Dim Tprocess As String = ddlProcess.SelectedItem.Text
        Dim Ttime As String = Me.TbTime.Text
        'Dim Machine As String = DDLMachine.SelectedItem.Value
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ProcessEfficiency1.aspx?Tmonth=" + Tmonth.ToString() + "&Tprocess=" + Tprocess.ToString + "&Type=Machine&Ttime=" + Ttime + "');", True)
        '"&Machine=" + Machine.ToString + 

    End Sub
    Protected Sub BtPerMan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtPerMan.Click
        InsertTableMAN()
        Dim Selmonth As Integer = ddlMonth.SelectedItem.Value
        Dim Tmonth As String = MonthText(CInt(Selmonth))
        Dim Tprocess As String = ddlProcess.SelectedItem.Text
        Dim Ttime As String = Me.TbTime.Text
        'Dim Machine As String = DDLMachine.SelectedItem.Value
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ProcessEfficiency1.aspx?Tmonth=" + Tmonth.ToString() + "&Tprocess=" + Tprocess.ToString + "&Type=Labor&Ttime=" + Ttime + "');", True)

    End Sub
   
    Protected Sub BtEffMC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtEffMC.Click
        'ake
        InsertTableMC()
        Dim Selmonth As Integer = ddlMonth.SelectedItem.Value
        Dim Tmonth As String = MonthText(CInt(Selmonth))
        Dim Tprocess As String = ddlProcess.SelectedItem.Text
        Dim mcode As String = DDLMachine.SelectedItem.Text
        Dim MachineNo As String = DDLMachine.SelectedItem.Value
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ProcessEfficiency2.aspx?Tmonth=" + Tmonth.ToString() + "&Tprocess=" + Tprocess.ToString + "&MachineNo=" + MachineNo.ToString + "&Type=Machine');", True)

    End Sub
    Protected Sub BtEffMan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtEffMan.Click
        InsertTableMAN()
        Dim Selmonth As Integer = ddlMonth.SelectedItem.Value
        Dim Tmonth As String = MonthText(CInt(Selmonth))
        Dim Tprocess As String = ddlProcess.SelectedItem.Text
        Dim MachineNo As String = DDLMachine.SelectedItem.Value
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ProcessEfficiency2.aspx?Tmonth=" + Tmonth.ToString() + "&Tprocess=" + Tprocess.ToString + "&MachineNo=" + MachineNo.ToString + "&Type=Labor');", True)

    End Sub
    Private Sub InsertTableMC()
        Dim TempTable As String = "TempEfficiency" & Session("UserName")
        CreateTempTable.CreateTempEfficiency(TempTable)
        Dim YM As String = ddlYear.SelectedItem.Value & ddlMonth.SelectedItem.Value
        Dim PTarget As Integer = 0
        If Trim(TbTarget.Text) <> "" Then
            PTarget = CInt(TbTarget.Text)
        End If
        Dim WorkTime As Integer = 0
        If Trim(TbTime.Text) <> "" Then
            WorkTime = CInt(TbTime.Text)
        End If
        Dim SelSQL As String = "Select TD003,TE006,TE007,TE017,REPLACE(TE018,'''',' ')  as PartDesc,REPLACE(TE019,'''',' ')  as PartSpec,TE011,TE013,MF025,MW002"
        SelSQL = SelSQL & " From SFCTD TD left join SFCTE TE on(TD.TD001=TE.TE001 and TD.TD002=TE.TE002 )" 'Operation Report Headers + Line
        SelSQL = SelSQL & " left join BOMMF MF on(TE017=MF001 and TE009=MF004 and TD004=MF006)" 'Item Routing Lines
        SelSQL = SelSQL & " left join CMSMW MW on(TE009=MW001)"
        SelSQL = SelSQL & " Where TD004='" & ddlProcess.SelectedItem.Value & "' and TD003 like'" & YM & "%'"

        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SelSQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim Qty As Integer = Program.Rows(i).Item("TE011")
            Dim Std As Decimal = 0.0
            Dim Actual As Decimal = 0.0
            Actual = System.Math.Round(Program.Rows(i).Item("TE013") / 60, 2) ' sec to min 
            If Not IsDBNull(Program.Rows(i).Item("MF025")) Then
                Std = System.Math.Round(Program.Rows(i).Item("MF025") / 60, 2)
            Else
                Std = 0
            End If

            Dim InsSQL As String = "Insert into " & TempTable & "(DocDate,Process,WoType,WoNo,PartItem,PartDesc,PartSpec,Qty,Actual,Std,WorkTime)"
            InsSQL = InsSQL & "VALUES('" & Program.Rows(i).Item("TD003") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("MW002") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("TE006") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("TE007") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("TE017") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("PartDesc") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("PartSpec") & "',"
            InsSQL = InsSQL & "'" & Qty & "',"
            InsSQL = InsSQL & "'" & Actual & "',"
            InsSQL = InsSQL & "'" & Std & "',"
            InsSQL = InsSQL & "'" & WorkTime & "')"
            Conn_SQL.Exec_Sql(InsSQL, Conn_SQL.MIS_ConnectionString)
        Next
        ' Temp Table2       

        Dim Temptable2 As String = TempTable & "2"
        Dim SelSql2 As String = "SELECT DocDate,SUM(Actual) as SumAcMachine,SUM(Std*Qty) as SumStdMachine "
        SelSql2 = SelSql2 & " FROM " & TempTable & " Group by DocDate"
        Dim Program2 As New DataTable
        Program2 = Conn_SQL.Get_DataReader(SelSql2, Conn_SQL.MIS_ConnectionString)
        For j As Integer = 0 To Program2.Rows.Count - 1
            Dim SumAcMachine As Decimal = Program2.Rows(j).Item("SumAcMachine")
            Dim SumStdMachine As Decimal = Program2.Rows(j).Item("SumStdMachine")
            Dim PerEffMC As Decimal = 0.0
            Dim PerMC As Decimal = 0.0
            If WorkTime > 0 Then
                PerMC = System.Math.Round((SumAcMachine * 100) / WorkTime, 2)
            End If
            If SumAcMachine > 0 Then
                PerEffMC = System.Math.Round((SumStdMachine * 100) / SumAcMachine, 2)
            End If
            
            Dim InsSQl2 As String = "Insert into " & Temptable2 & "(DocDate,PerTarget,PerWork,PerEff)"
            InsSQl2 = InsSQl2 & "Values('" & Program2.Rows(j).Item("DocDate") & "',"
            InsSQl2 = InsSQl2 & "'" & PTarget & "',"
            InsSQl2 = InsSQl2 & "'" & PerMC & "',"
            InsSQl2 = InsSQl2 & "'" & PerEffMC & "')"
            Conn_SQL.Exec_Sql(InsSQl2, Conn_SQL.MIS_ConnectionString)
        Next
    End Sub

    Private Sub InsertTableMAN()
        Dim TempTable As String = "TempEfficiency" & Session("UserName")
        CreateTempTable.CreateTempEfficiency(TempTable)
        Dim YM As String = ddlYear.SelectedItem.Value & ddlMonth.SelectedItem.Value
        Dim PTarget As Integer = 0
        If Trim(TbTarget.Text) <> "" Then
            PTarget = CInt(TbTarget.Text)
        End If
        Dim WorkTime As Integer = 0
        If Trim(TbTime.Text) <> "" Then
            WorkTime = CInt(TbTime.Text)
        End If
        Dim SelSQL As String = "Select TD008,TE006,TE007,TE017,REPLACE(TE018,'''',' ')  as PartDesc,REPLACE(TE019,'''',' ')  as PartSpec,TE011,TE012,MF010,MW002"
        SelSQL = SelSQL & " From SFCTD TD left join SFCTE TE on(TD.TD001=TE.TE001 and TD.TD002=TE.TE002 )" 'Operation Report Headers + Line
        SelSQL = SelSQL & " left join BOMMF MF on(TE017=MF001 and TE009=MF004 and TD004=MF006)" 'Item Routing Lines
        SelSQL = SelSQL & " left join CMSMW MW on(TE009=MW001)"
        SelSQL = SelSQL & " Where TD004='" & ddlProcess.SelectedItem.Value & "' and TD008 like'" & YM & "%'"

        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SelSQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim Qty As Integer = Program.Rows(i).Item("TE011")
            Dim Std As Decimal = 0.0
            Dim Actual As Decimal = 0.0
            Actual = System.Math.Round(Program.Rows(i).Item("TE012") / 60, 2) ' sec to min 
                If Not IsDBNull(Program.Rows(i).Item("MF010")) Then
                Std = System.Math.Round(Program.Rows(i).Item("MF010") / 60, 2)
                Else
                    Std = 0
                End If

            Dim InsSQL As String = "Insert into " & TempTable & "(DocDate,Process,WoType,WoNo,PartItem,PartDesc,PartSpec,Qty,Actual,Std,WorkTime)"
            InsSQL = InsSQL & "VALUES('" & Program.Rows(i).Item("TD008") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("MW002") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("TE006") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("TE007") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("TE017") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("PartDesc") & "',"
            InsSQL = InsSQL & "'" & Program.Rows(i).Item("PartSpec") & "',"
            InsSQL = InsSQL & "'" & Qty & "',"
            InsSQL = InsSQL & "'" & Actual & "',"
            InsSQL = InsSQL & "'" & Std & "',"
            InsSQL = InsSQL & "'" & WorkTime & "')"
            Conn_SQL.Exec_Sql(InsSQL, Conn_SQL.MIS_ConnectionString)
        Next
        ' Temp Table2
        
        Dim Temptable2 As String = TempTable & "2"
        Dim SelSql2 As String = "SELECT DocDate,SUM(Actual) as SumAcLabor,SUM(Std*Qty) as SumStdLabor "
        SelSql2 = SelSql2 & " FROM " & TempTable & " Group by DocDate"
        Dim Program2 As New DataTable
        Program2 = Conn_SQL.Get_DataReader(SelSql2, Conn_SQL.MIS_ConnectionString)
        For j As Integer = 0 To Program2.Rows.Count - 1
            Dim SumAcLabor As Decimal = Program2.Rows(j).Item("SumAcLabor")
            Dim SumStdALabor As Decimal = Program2.Rows(j).Item("SumStdLabor")
            Dim PerLabor As Decimal = 0.0
            Dim PerEffLabor As Decimal = 0.0
            If WorkTime > 0 Then
                PerLabor = System.Math.Round((SumAcLabor * 100) / WorkTime, 2)
            End If
            If SumAcLabor > 0 Then
                PerEffLabor = System.Math.Round((SumStdALabor * 100) / SumAcLabor, 2)
            End If
            
            Dim InsSQl2 As String = "Insert into " & Temptable2 & "(DocDate,PerTarget,PerWork,PerEff)"
            InsSQl2 = InsSQl2 & "Values('" & Program2.Rows(j).Item("DocDate") & "',"
            InsSQl2 = InsSQl2 & "'" & PTarget & "',"
            InsSQl2 = InsSQl2 & "'" & PerLabor & "',"
            InsSQl2 = InsSQl2 & "'" & PerEffLabor & "')"
           Conn_SQL.Exec_Sql(InsSQl2, Conn_SQL.MIS_ConnectionString)
        Next
    End Sub

    Private Sub InitProdCode(ByVal ProID As String)
        Dim Program As New Data.DataTable
        Program = Conn_SQL.Get_DataReader("Select MX001,MX002 from CMSMX where MX002='" & ProID & "'", Conn_SQL.ERP_ConnectionString)
        DDLMachine.DataSource = Program.DefaultView
        DDLMachine.DataTextField = "MX001"
        DDLMachine.DataValueField = "MX001"
        DDLMachine.DataBind()
        Program.Dispose()

    End Sub

    Sub cust_change(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProcess.SelectedIndexChanged
        InitProdCode(ddlProcess.SelectedValue)
    End Sub

    Private Sub InitDeptID()
        Dim Program As New Data.DataTable
        Program = Conn_SQL.Get_DataReader("Select MD001,MD002 from CMSMD order by MD002", Conn_SQL.ERP_ConnectionString)
        ddlProcess.DataSource = Program.DefaultView
        ddlProcess.DataTextField = "MD002"
        ddlProcess.DataValueField = "MD001"
        ddlProcess.DataBind()
        Program.Dispose()
        InitProdCode(ddlProcess.SelectedValue)

    End Sub

End Class