Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web

Public Class ManPower
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim MonthText() As String = {"", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            DDLmounth()
            InitDeptID()
        End If
        
    End Sub

    Private Sub InitDeptID()
        Dim Program As New Data.DataTable
        Program = Conn_sql.Get_DataReader("Select MD001,MD002 from CMSMD order by MD002", Conn_sql.ERP_ConnectionString)
        DDLWorkCenter.DataSource = Program.DefaultView
        DDLWorkCenter.DataTextField = "MD002"
        DDLWorkCenter.DataValueField = "MD001"
        DDLWorkCenter.DataBind()
        Program.Dispose()
        InitProdCode(DDLWorkCenter.SelectedValue)
    End Sub

    Private Sub DDLmounth()
        With DDLmonth
            .Items.Add("1")
            .Items.Add("2")
            .Items.Add("3")
            .Items.Add("4")
            .Items.Add("5")
            .Items.Add("6")
            .Items.Add("7")
            .Items.Add("8")
            .Items.Add("9")
            .Items.Add("10")
            .Items.Add("11")
            .Items.Add("12")
        End With
    End Sub

    Protected Sub Buview_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buview.Click

        InsertTableMAN()
        Dim Selmonth As Integer = DDLmonth.SelectedItem.Value
        Dim Tmonth As String = MonthText(CInt(Selmonth))
        Dim Tprocess As String = DDLWorkCenter.SelectedItem.Text
        Dim Ttime As String = txttime.Text
        Dim Machine As String = DDLMachine.SelectedItem.Value
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ProcessEfficiency1.aspx?Tmonth=" + Tmonth.ToString() + "&Tprocess=" + Tprocess.ToString + "&Machine=" + Machine.ToString + "&Type=Labor&Ttime=" + Ttime + "');", True)

    End Sub

    Protected Sub BtEffMan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtEffMan.Click

        InsertTableMAN()
        Dim Selmonth As Integer = DDLmonth.SelectedItem.Value
        Dim Tmonth As String = MonthText(CInt(Selmonth))
        Dim Tprocess As String = DDLWorkCenter.SelectedItem.Text
        Dim MachineNo As String = DDLMachine.SelectedItem.Value
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ProcessEfficiency2.aspx?Tmonth=" + Tmonth.ToString() + "&Tprocess=" + Tprocess.ToString + "&MachineNo=" + MachineNo.ToString + "&Type=Labor');", True)

    End Sub

    Private Sub InsertTableMAN()
        Dim TempTable As String = "TempEfficiency" & Session("UserName")
        CreateTempTable.CreateTempEfficiency(TempTable)
        'Dim YM As String = DDLYear.SelectedItem.Value & DDLmonth.SelectedItem.Value
        'Dim YM As String = DDLYear.SelectedValue & DDLmonth.SelectedValue & DDLDate.SelectedValue
        Dim YM As String = txtdate.Text
        Dim PTarget As Integer = 0
        'If Trim(TbTarget.Text) <> "" Then
        '    PTarget = CInt(TbTarget.Text)
        'End If

        Dim WorkTime As Integer = 0
        If Trim(txttime.Text) <> "" Then
            WorkTime = CInt(txttime.Text)
        End If
        Dim SelSQL As String = "Select TD008,TE006,TE007,TE017,REPLACE(TE018,'''',' ')  as PartDesc,REPLACE(TE019,'''',' ')  as PartSpec,TE011,TE012,MF010,MW002"
        SelSQL = SelSQL & " From SFCTD TD left join SFCTE TE on(TD.TD001=TE.TE001 and TD.TD002=TE.TE002 )" 'Operation Report Headers + Line
        SelSQL = SelSQL & " left join BOMMF MF on(TE017=MF001 and TE009=MF004 and TD004=MF006)" 'Item Routing Lines
        SelSQL = SelSQL & " left join CMSMW MW on(TE009=MW001)"
        SelSQL = SelSQL & " Where TD004='" & DDLWorkCenter.SelectedItem.Value & "' and TD008 ='" & YM & "'"
        SelSQL &= "and TE005='" & DDLMachine.SelectedValue & "'"

        Dim Program As New DataTable
        Program = Conn_sql.Get_DataReader(SelSQL, Conn_sql.ERP_ConnectionString)
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
            Conn_sql.Exec_Sql(InsSQL, Conn_sql.MIS_ConnectionString)
        Next
        ' Temp Table2

        Dim Temptable2 As String = TempTable & "2"
        Dim SelSql2 As String = "SELECT DocDate,SUM(Actual) as SumAcLabor,SUM(Std*Qty) as SumStdLabor "
        SelSql2 = SelSql2 & " FROM " & TempTable & " Group by DocDate"
        Dim Program2 As New DataTable
        Program2 = Conn_sql.Get_DataReader(SelSql2, Conn_sql.MIS_ConnectionString)
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
            Conn_sql.Exec_Sql(InsSQl2, Conn_sql.MIS_ConnectionString)
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

    Sub cust_change(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLWorkCenter.SelectedIndexChanged
        InitProdCode(DDLWorkCenter.SelectedValue)
    End Sub

End Class