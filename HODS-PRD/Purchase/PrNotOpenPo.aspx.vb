Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class PrNotOpenPo
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    'Dim ExportUtil As New ExportUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If

            Me.lblcount.Text = Conn_SQL.Get_value("select COUNT(*)  from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 = 'Y'", Conn_SQL.ERP_ConnectionString)
        End If
    End Sub

    Protected Sub BuExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuExport.Click

        'ControlForm.ExportGridViewToExcel("PrNotOpenPo", GridView2)
        insertPrNoPo()

    End Sub

    Private Sub insertPrNoPo()
        Dim DelQuo As String = "delete from PrNoPo"
        Conn_SQL.Exec_Sql(DelQuo, Conn_SQL.MIS_ConnectionString)

        If GridView2.Rows.Count > 0 Then
            For M_count As Integer = 0 To GridView2.Rows.Count - 1

                Dim Type As String = GridView2.Rows(M_count).Cells(0).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim No As String = GridView2.Rows(M_count).Cells(1).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Item As String = GridView2.Rows(M_count).Cells(2).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Desc As String = GridView2.Rows(M_count).Cells(3).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Spec As String = GridView2.Rows(M_count).Cells(4).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Unit As String = GridView2.Rows(M_count).Cells(5).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim SoType As String = GridView2.Rows(M_count).Cells(6).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim PrQty As String = GridView2.Rows(M_count).Cells(7).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim CID As String = GridView2.Rows(M_count).Cells(8).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim RequestDate As String = GridView2.Rows(M_count).Cells(9).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim IssueDate As String = GridView2.Rows(M_count).Cells(10).Text.Replace(" ", "").Replace("&nbsp;", "")

                Dim InSQL As String = "Insert into PrNoPo(Type,No,Item,[Desc],Spec,Unit,SoType,PrQty,CID,RequestDate,IssueDate)"
                InSQL = InSQL & " Values('" & Type & "','" & No & "',"
                InSQL = InSQL & "'" & Item & "','" & Desc & "',"
                InSQL = InSQL & "'" & Spec & "','" & Unit & "',"
                InSQL = InSQL & "'" & SoType & "','" & PrQty & "',"
                InSQL = InSQL & "'" & CID & "','" & RequestDate & "',"
                InSQL = InSQL & "'" & IssueDate & "')"
                Conn_SQL.Exec_Sql(InSQL, Conn_SQL.MIS_ConnectionString)

            Next
        End If

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('PrNotOpenPo1.aspx?');", True)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
 
    Protected Sub BuView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuView.Click

        If DDLType.SelectedValue = 0 Then     'ALL
            If txtno.Text <> "" Then       'Sales Order No   TB002
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB002='" & txtno.Text & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB002='" & txtno.Text & "'", Conn_SQL.ERP_ConnectionString)
            ElseIf txtitem.Text <> "" Then    'Item   TB004
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB004 like '" & txtitem.Text & "%'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("select COUNT(*)  from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB004='" & txtitem.Text & "'", Conn_SQL.ERP_ConnectionString)
            ElseIf txtspec.Text <> "" Then    'Spec   TB006
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB006 like '" & txtspec.Text & "%'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB006='" & txtspec.Text & "'", Conn_SQL.ERP_ConnectionString)
            ElseIf txtdateissue.Text <> "" Then    'Date Issue   PURTA.TA013
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and H.TA013='" & txtdateissue.Text & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and H.TA013='" & txtdateissue.Text & "'", Conn_SQL.ERP_ConnectionString)
            ElseIf txtrequest.Text <> "" Then    'Date Required    TB011
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB011='" & txtrequest.Text & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB011='" & txtrequest.Text & "'", Conn_SQL.ERP_ConnectionString)
            ElseIf txttype.Text <> "" Then    'Sales Order Type    TB001
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB001='" & txttype.Text & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB001='" & txttype.Text & "'", Conn_SQL.ERP_ConnectionString)
            ElseIf txttype.Text <> "" And txtno.Text <> "'" Then 'Type and No
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB001='" & txttype.Text & "' and L.TB002='" & txtno.Text & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB001='" & txttype.Text & "' and L.TB002='" & txtno.Text & "'", Conn_SQL.ERP_ConnectionString)
            ElseIf txttype.Text = "" And txtno.Text = "" And txtitem.Text = "" And txtspec.Text = "" And txtdateissue.Text = "" And txtrequest.Text = "" Then   'ถ้าไม่ใส่ข้อมูล Approve = 'Y'
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf txttype.Text = "" And txtno.Text = "" And txtitem.Text = "" And txtspec.Text = "" And txtdateissue.Text = "" And txtrequest.Text = "" And DDLApp.SelectedIndex = 1 Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)
            End If

        ElseIf DDLType.SelectedValue <> 0 Then    '2201,2202,2203,2204,2205
            If DDLType.SelectedValue = "2201" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2202" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2203" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2204" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2205" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2206" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2207" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2208" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2209" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2210" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2211" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2212" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2213" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2214" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "2215" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "3112" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)

            ElseIf DDLType.SelectedValue = "3113" Then
                SqlDataSource2.SelectCommand = "select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,L.TB011,H.TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'"
                GridView2.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and H.TA007 ='" & DDLApp.SelectedValue & "' and L.TB008='" & DDLType.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)
            End If

        End If

    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLType.SelectedIndexChanged

        lblcount.Text = 0

    End Sub

End Class