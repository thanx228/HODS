Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class SetMoldOrder
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim dbConn As New DataConnectControl
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    'Dim NowSelectMachine As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            'Session("UserName") = "500026"
            'Session("UserId") = "500026"
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        Else
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ShowOrder()
    End Sub
    Private Sub ShowOrder()
        Dim commandSQL As String = "SELECT * FROM [SetMoldOrder] where 1=1"
        If DateFrom.Text <> "" Then
            commandSQL = commandSQL & " and DocDate>='" & DateFrom.Text & "'"
        End If
        If DateTo.Text <> "" Then
            commandSQL = commandSQL & " and DocDate<='" & DateTo.Text & "'"
        End If
        If TBDocNo.Text <> "" Then
            commandSQL = commandSQL & " and DocNo like '%" & TBDocNo.Text & "%'"
        End If
        commandSQL = commandSQL & "  ORDER BY [DocDate] DESC"
        SqlDataSourceSetMoldOrder.SelectCommand = commandSQL
        'gvShow.DataBind()
        gvShow.DataSourceID = "SqlDataSourceSetMoldOrder"
        'gvShow.DataSource = SqlDataSourceSetMoldOrder
        gvShow.DataBind()
        UpdatePanel1.Update()
    End Sub



    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If TBDocTime.Text = "" Or Len(TBDocTime.Text) <> 4 Or Left(TBDocTime.Text, 2) > "23" Or Right(TBDocTime.Text, 2) > "59" Then
            show_message.ShowMessage(Page, "Doc Time Error", UpdatePanel1)
            Exit Sub
        End If
        If DateDocDate.Text = "" Or Len(DateDocDate.Text) <> 8 Or Mid(DateDocDate.Text, 5, 2) > "12" Or Right(DateDocDate.Text, 2) > "31" Then
            show_message.ShowMessage(Page, "Doc Date Error", UpdatePanel1)
            Exit Sub
        End If
        Dim SQL As String = "select max(right(DocNo,3))+1 from SetMoldOrder where DocDate='" & DateDocDate.Text & "'"
        Dim dt1 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        If IsDBNull(dt1.Rows(0).Item(0)) Then
            LBDocNo.Text = DateDocDate.Text & "001"
        Else
            LBDocNo.Text = DateDocDate.Text & dt1.Rows(0).Item(0).ToString.PadLeft(3, "0")
        End If
        SQL = "insert into SetMoldOrder (DocNo,DocDate,DocTime,Remark) values('" &
                    LBDocNo.Text & "','" & DateDocDate.Text & "','" & TBDocTime.Text & "','" & TBRemark.Text & "')"
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)

        'Response.Redirect(Request.Url.ToString())
        BtnDeleteSetupOrder.Visible = False
        ShowOrder()
    End Sub

    Protected Sub gvShow_Click(sender As Object, e As EventArgs) Handles gvShow.SelectedIndexChanged
        BtnDeleteSetupOrder.Visible = True
        ShowDetail()
    End Sub

    Private Sub ShowDetail()
        Dim alex As GridViewRow = gvShow.SelectedRow
        LBDocNo.Text = alex.Cells(0).Text
        LBDocDate.Text = alex.Cells(1).Text
        Dim commandSQL As String = "SELECT * FROM [SetMoldDetail] where 1=1"
        commandSQL = commandSQL & " and DocNo='" & alex.Cells(0).Text & "'"
        commandSQL = commandSQL & "  ORDER BY [Seq] DESC"
        SqlDataSourceSetMoldDetail.SelectCommand = commandSQL
        'gvShow.DataBind()
        GridView1.DataSourceID = "SqlDataSourceSetMoldDetail"
        'gvShow.DataSource = SqlDataSourceSetMoldOrder
        GridView1.DataBind()
        UpdatePanel1.Update()
    End Sub

    Protected Sub GridView1_Edit(sender As Object, e As EventArgs) Handles GridView1.RowCommand
        Dim commandSQL As String = "SELECT * FROM [SetMoldDetail] where 1=1"
        commandSQL = commandSQL & " and DocNo='" & LBDocNo.Text & "'"
        commandSQL = commandSQL & "  ORDER BY [Seq] DESC"
        SqlDataSourceSetMoldDetail.SelectCommand = commandSQL
    End Sub


    Dim MachineList() As String = {"A01", "A02", "A03", "A04", "A05", "A06", "A07", "A08", "A09",
        "B01", "B02", "B03", "B04", "B05", "B06", "B07",
        "C01", "C02", "C03", "C04", "C05", "C06", "C07", "C08", "C09", "C10", "C11",
        "D01", "D02", "D03", "D04", "D05", "D06", "D07", "D08", "D09", "D10", "D11",
        "E01", "E02", "E03", "E04", "E05", "E06", "E07", "E08", "E09", "E10", "E11",
        "F01", "F02", "F03", "F04", "F05", "F06", "F07", "F08", "F09", "F10", "F11", "F12",
        "G01", "G02", "G03", "G04", "G05", "G06", "G07", "G08", "G09", "G10", "G11", "G12", "G13",
        "H01", "H02", "H03", "H04", "H05", "H06", "H07", "H08", "H09", "H10", "H11", "H12"
        }
    Protected Sub BtnNewDetail_Click(sender As Object, e As EventArgs) Handles BtnNewDetail.Click
        Dim SQL As String = "select * from PlanSchedule where PlanDate='" & LBDocDate.Text & "' and PlanMachine is null"
        SQL = SQL & " and MoType + '-' + Rtrim(MoNo) + '-' + MoSeq not in (select MoNo + '-' + MoSeq from SetMoldDetail where DocNo in(select DocNo from SetMoldOrder where DocDate='" & LBDocDate.Text & "'))"
        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        TBMatCode.Text = ""
        TBMOLDName.Text = ""
        DDLAllCanSelectMO.Items.Clear()
        Dim Item0 As New ListItem
        Item0.Text = "Select"
        Item0.Value = 0
        DDLAllCanSelectMO.Items.Add(Item0)
        For i = 0 To dt2.Rows.Count - 1
            Dim SQLMOData As String = "select * from MOCTA where TA001='" & Trim(dt2.Rows(i).Item("MoType")) & "' and TA002='" & Trim(dt2.Rows(i).Item("MoNo")) & "' and TA003='" & Trim(dt2.Rows(i).Item("MoSeq")) & "'"
            Dim dtMOData As DataTable = dbConn.Query(SQLMOData, VarIni.ERP, dbConn.WhoCalledMe)

            Dim alex As New ListItem
            Dim MOInfo As String = Trim(dt2.Rows(i).Item("MoType")) & "-" & Trim(dt2.Rows(i).Item("MoNo")) & "-" & Trim(dt2.Rows(i).Item("MoSeq"))
            alex.Text = MOInfo & "(" & dtMOData.Rows(0).Item("TA034") & "-" & dtMOData.Rows(0).Item("TA035") & ")"
            alex.Value = MOInfo
            DDLAllCanSelectMO.Items.Add(alex)
        Next

        'DDLAllMachineNo.Items.Clear()
        'For i = 0 To MachineList.Length - 1
        '    Dim alex As New ListItem
        '    alex.Text = MachineList(i)
        '    alex.Value = alex.Text
        '    DDLAllMachineNo.Items.Add(alex)
        'Next
        BtnSave1.Text = "Save"
        BtnDelete.Visible = False
        modEx2.Show()
    End Sub

    Protected Sub DDLAllCanSelectMO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLAllCanSelectMO.SelectedIndexChanged
        If DDLAllCanSelectMO.SelectedValue <> "0" Then
            Dim MOGroup() As String = DDLAllCanSelectMO.SelectedValue.Split("-")
            Dim strSql As String = "select MB001,MB002,MB003,MOC.TA015,SFC.TA003,SFC.TA004,MW002,SFC.TA006,MD002,BOM.MF010,BOM.MF025" &
            " From INVMB " &
    "Left join MOCTA MOC on TA006=MB001 " &
    "left join SFCTA SFC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
    "left join BOMMF BOM on MF001=MB001 and MF003=SFC.TA003 " &
    "left join CMSMD on MD001=SFC.TA006 " &
    "left join CMSMW on MW001=SFC.TA004 " &
    "where MOC.TA001='" & MOGroup(0) & "' and MOC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
            Dim dt1 As DataTable = dbConn.Query(strSql, VarIni.ERP, dbConn.WhoCalledMe)

            LBItemNo.Text = dt1.Rows(0).Item("MB001")
            LBItemSpec.Text = dt1.Rows(0).Item("MB003")
            TBQty.Text = Int(dt1.Rows(0).Item("TA015"))
            LBProcess.Text = dt1.Rows(0).Item("MW002")
            LBStdPcs.Text = dt1.Rows(0).Item("MF010")
            LBStdPcs0.Text = dt1.Rows(0).Item("MF025")
            If LBStdPcs0.Text <> 0 Then
                Dim HoursValue As Integer = Math.Floor((TBQty.Text * LBStdPcs0.Text) / 3600)
                Dim MinValue As Integer = Math.Ceiling(TBQty.Text * LBStdPcs0.Text / 60) Mod 60
                LBHours.Text = HoursValue & "H " & MinValue & "M"
            Else
                LBHours.Text = 0
            End If

            Dim SqlMX As String = "select MX001,MX006 from CMSMX where MX002='" & Trim(dt1.Rows(0).Item("TA006")) & "' order by MX006"
            Dim dt2 As DataTable = dbConn.Query(SqlMX, VarIni.ERP, dbConn.WhoCalledMe)
            DDLAllMachineNo.Items.Clear()
            Dim Item0 As New ListItem
            Item0.Text = "Select"
            Item0.Value = 0
            DDLAllMachineNo.Items.Add(Item0)
            For i = 0 To dt2.Rows.Count - 1
                Dim SqlDetail As String = "select MacNo from SetMoldDetail where DocNo='" & LBDocNo.Text & "' and MacNo='" & Trim(dt2.Rows(i).Item("MX001")) & "'"
                Dim dt3 As DataTable = dbConn.Query(SqlDetail, VarIni.DBMIS, dbConn.WhoCalledMe)
                If dt3.Rows.Count = 0 Then
                    Dim alex As New ListItem
                    alex.Text = Trim(dt2.Rows(i).Item("MX006")) & "(" & Trim(dt2.Rows(i).Item("MX001")) & ")"
                    alex.Value = alex.Text
                    DDLAllMachineNo.Items.Add(alex)
                End If
            Next

        End If
        modEx2.Show()
    End Sub

    Protected Sub TBQty_TextChanged(sender As Object, e As EventArgs) Handles TBQty.TextChanged
        If LBStdPcs0.Text <> 0 Then
            Dim HoursValue As Integer = Math.Floor((TBQty.Text * LBStdPcs0.Text) / 3600)
            Dim MinValue As Integer = Math.Ceiling(TBQty.Text * LBStdPcs0.Text / 60) Mod 60
            LBHours.Text = HoursValue & "H " & MinValue & "M"
        Else
            LBHours.Text = 0
        End If
        modEx2.Show()
    End Sub

    Protected Sub DDLAllMachineNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLAllMachineNo.SelectedIndexChanged
        modEx2.Show()
    End Sub

    Protected Sub BtnSave1_Click(sender As Object, e As EventArgs) Handles BtnSave1.Click
        If DDLAllCanSelectMO.SelectedValue <> "0" And DDLAllMachineNo.SelectedValue <> "0" Then
            Dim strSql As String = ""

            If BtnSave1.Text = "Save" Then
                Dim MOGroup() As String = DDLAllCanSelectMO.SelectedValue.Split("-")
                strSql = "select max(Seq) from SetMoldDetail where DocNo='" & LBDocNo.Text & "'"
                Dim dtSelectMaxSeq As DataTable = dbConn.Query(strSql, VarIni.DBMIS, dbConn.WhoCalledMe)

                Dim DocSeq As String = ""
                If IsDBNull(dtSelectMaxSeq.Rows(0).Item(0)) Then
                    DocSeq = "1"
                Else
                    DocSeq = dtSelectMaxSeq.Rows(0).Item(0) + 1
                End If

                strSql = "insert into SetMoldDetail values('" & LBDocNo.Text & "'" &
                ",'" & DocSeq & "'" &
                ",'" & MOGroup(0) & "-" & MOGroup(1) & "'" &
                ",'" & MOGroup(2) & "'" &
                ",'" & DDLAllMachineNo.SelectedValue & "'" &
                ",'" & TBQty.Text & "'" &
                ",'" & Trim(LBItemNo.Text) & "'" &
                ",'" & LBItemSpec.Text & "'" &
                ",'" & TBMatCode.Text & "'" &
                ",'" & TBMOLDName.Text & "'" &
                ",'" & LBProcess.Text & "'" &
                ",'" & LBStdPcs0.Text & "'" &
                ",'" & LBHours.Text & "'" &
                ",'" & LBQI.Text & "'" &
            ")"
            Else
                Dim MOGroup() As String = DDLAllCanSelectMO.SelectedValue.Split("-")
                strSql = "update SetMoldDetail set MacNo='" & DDLAllMachineNo.SelectedValue & "',Qty='" & TBQty.Text & "',UseHours='" & LBHours.Text & "',MoldName='" & TBMOLDName.Text & "',MatCode='" & TBMatCode.Text & "' where DocNo='" & LBDocNo.Text & "' and Seq='" & LBSeq.Text & "'"
            End If
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        Else
            show_message.ShowMessage(Page, "MO Number and MacNo cant be null", UpdatePanel1)
            Exit Sub
        End If
        ShowDetail()
    End Sub

    Protected Sub BtnDeleteSetupOrder_Click(sender As Object, e As EventArgs) Handles BtnDeleteSetupOrder.Click
        Dim strSql As String
        strSql = "delete SetMoldOrder where DocNo='" & LBDocNo.Text & "'"
        'SqlDataSourceSetMoldDetail.DeleteCommand = strSql
        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        strSql = "delete SetMoldDetail where DocNo='" & LBDocNo.Text & "'"
        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        ShowOrder()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Dim strSql As String = "delete SetMoldDetail where DocNo='" & LBDocNo.Text & "' and Seq='" & LBSeq.Text & "'"
        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        ShowDetail()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim alex As GridViewRow = GridView1.SelectedRow
        LBDocNo.Text = alex.Cells(0).Text
        LBSeq.Text = alex.Cells(1).Text
        BtnSave1.Text = "Update"
        BtnDelete.Visible = True

        Dim strSql As String = "select * from SetMoldDetail where DocNo='" & LBDocNo.Text & "' and Seq='" & LBSeq.Text & "'"
        Dim dtSelectDetail As DataTable = dbConn.Query(strSql, VarIni.DBMIS, dbConn.WhoCalledMe)

        DDLAllMachineNo.SelectedValue = Trim(dtSelectDetail.Rows(0).Item("MacNo"))
        TBQty.Text = Int(dtSelectDetail.Rows(0).Item("Qty"))
        LBHours.Text = dtSelectDetail.Rows(0).Item("UseHours")
        TBMatCode.Text = dtSelectDetail.Rows(0).Item("MatCode")

        DDLAllCanSelectMO.Items.Clear()
        Dim MOItem As New ListItem
        MOItem.Value = dtSelectDetail.Rows(0).Item("MoNo") & "-" & dtSelectDetail.Rows(0).Item("MoSeq")
        MOItem.Text = MOItem.Value
        DDLAllCanSelectMO.Items.Add(MOItem)

        Dim MOGroup() As String = dtSelectDetail.Rows(0).Item("MoNo").Split("-")
        strSql = "select MB001,MB002,MB003,MOC.TA015,SFC.TA003,SFC.TA004,MW002,SFC.TA006,MD002,BOM.MF010,BOM.MF025,BOM.UDF02" &
            " From INVMB " &
    "Left join MOCTA MOC on TA006=MB001 " &
    "left join SFCTA SFC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
    "left join BOMMF BOM on MF001=MB001 and MF003=SFC.TA003 " &
    "left join CMSMD on MD001=SFC.TA006 " &
    "left join CMSMW on MW001=SFC.TA004 " &
    "where MOC.TA001='" & MOGroup(0) & "' and MOC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & dtSelectDetail.Rows(0).Item("MoSeq") & "'"
        Dim dt1 As DataTable = dbConn.Query(strSql, VarIni.ERP, dbConn.WhoCalledMe)

        LBItemNo.Text = dt1.Rows(0).Item("MB001")
        LBItemSpec.Text = dt1.Rows(0).Item("MB003")

        LBProcess.Text = dt1.Rows(0).Item("MW002")
        LBStdPcs.Text = dt1.Rows(0).Item("MF010")
        LBStdPcs0.Text = dt1.Rows(0).Item("MF025")
        If IsDBNull(dt1.Rows(0).Item("UDF02")) Then
            LBQI.Text = ""
        Else
            LBQI.Text = dt1.Rows(0).Item("UDF02")
        End If



        Dim SqlMX As String = "select MX001,MX006 from CMSMX where MX002='" & Trim(dt1.Rows(0).Item("TA006")) & "' order by MX006"
        Dim dt2 As DataTable = dbConn.Query(SqlMX, VarIni.ERP, dbConn.WhoCalledMe)
        DDLAllMachineNo.Items.Clear()

        For i = 0 To dt2.Rows.Count - 1
            Dim SqlDetail As String = "select MacNo from SetMoldDetail where DocNo='" & LBDocNo.Text & "' and MacNo='" & Trim(dt2.Rows(i).Item("MX001")) & "'"
            Dim dt3 As DataTable = dbConn.Query(SqlDetail, VarIni.DBMIS, dbConn.WhoCalledMe)
            If dt3.Rows.Count = 0 Or Trim(dt2.Rows(i).Item("MX001")) = dtSelectDetail.Rows(0).Item("MacNo") Then
                Dim AllItem As New ListItem
                AllItem.Text = Trim(dt2.Rows(i).Item("MX006")) & "(" & Trim(dt2.Rows(i).Item("MX001")) & ")"
                AllItem.Value = AllItem.Text
                DDLAllMachineNo.Items.Add(AllItem)
            End If
        Next

        modEx2.Show()
    End Sub


    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hplPrint As HyperLink = CType(e.Row.FindControl("hplPrint"), HyperLink)
            If Not IsNothing(hplPrint) And Not IsDBNull(e.Row.DataItem("DocNo")) Then
                hplPrint.NavigateUrl = "../PDF/PrintServiceMold.aspx?docno=" & e.Row.DataItem("DocNo").ToString.Trim
                hplPrint.Attributes.Add("title", e.Row.DataItem("DocNo"))
                hplPrint.Target = "_blank"
            End If
        End If
    End Sub



    'Protected Sub BtnDeleteSetupOrder_Click(sender As Object, e As EventArgs) Handles BtnDeleteSetupOrder.Click
    '    Dim strSql As String
    '    strSql = "delete SetMoldOrder where DocNo='" & LBDocNo.Text & "'"
    '    'SqlDataSourceSetMoldDetail.DeleteCommand = strSql
    '    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
    '    strSql = "delete SetMoldDetail where DocNo='" & LBDocNo.Text & "'"
    '    Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
    '    ShowOrder()
    'End Sub
End Class