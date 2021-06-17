Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop

Public Class PoNotClose
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim TempTable As String

    Private QSql As String = "select L.TD001,L.TD002,L.TD003,RL.TB003,L.TD026,L.TD027,L.TD004,L.TD006,L.TD016,L.TD007,L.TD008,L.TD015,L.TD009,L.TD012,L.TD008 - L.TD015 as Balance,H.TC004,R.TH007,R.TH015,L.UDF01 as PDD,L.TD012 as CDD,L.TD007 as WH,RL.TB011 AS RDate from PURTD L left join PURTC H on(H.TC001 = L.TD001) and (H.TC002 = L.TD002) left join PURTH R on (L.TD001 = R.TH011) and (L.TD002 = R.TH012) and (L.TD004 = R.TH004) and (L.TD015 = R.TH007) left join PURTB RL ON(L.TD026 = RL.TB001) AND (L.TD027 = RL.TB002) and (L.TD004 = RL.TB004) and (L.TD003 = RL.TB003) where L.TD016 = 'N'"
    Private CSql As String = "select count(*) from PURTD L left join PURTC H on(H.TC001 = L.TD001) and (H.TC002 = L.TD002) left join PURTH R on (L.TD001 = R.TH011) and (L.TD002 = R.TH012) and (L.TD004 = R.TH004) and (L.TD015 = R.TH007) left join PURTB RL ON(L.TD026 = RL.TB001) AND (L.TD027 = RL.TB002) and (L.TD004 = RL.TB004) and (L.TD003 = RL.TB003) where L.TD016 = 'N'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
        End If

        lblcount.Text = GridView1.Rows.Count
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click

        If txtfrom.Text = "" And txtto.Text = "" Then  'ถ้าไม่ใส่วันที่
            If txtpono.Text = "" And txtitem.Text = "" And txtspec.Text = "" And txtsup.Text = "" Then
                SqlDataSource1.SelectCommand = QSql & "and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count

                If DDLSType.SelectedIndex = 1 Then  '2201
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 2 Then   '2202
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 3 Then   '2203
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 4 Then  '2204
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 5 Then   '2205
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 6 Then   '2206
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 7 Then    '2207
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 8 Then    '2208
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 9 Then   '2209
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 10 Then    '2210
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 11 Then    '2211
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 12 Then    '2212
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 13 Then    '2213
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 14 Then    '2214
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf DDLSType.SelectedIndex = 15 Then    '2215
                    SqlDataSource1.SelectCommand = QSql & " and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                End If

            ElseIf txtpono.Text <> "" And txtitem.Text = "" And txtspec.Text = "" And txtsup.Text = "" Then
                'PO No TD002
                SqlDataSource1.SelectCommand = QSql & " and L.TD002 ='" & txtpono.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count

            ElseIf txtpono.Text = "" And txtitem.Text <> "" And txtspec.Text = "" And txtsup.Text = "" Then
                'Item TD004
                SqlDataSource1.SelectCommand = QSql & " and L.TD004 ='" & txtitem.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count

            ElseIf txtpono.Text = "" And txtitem.Text = "" And txtspec.Text <> "" And txtsup.Text = "" Then
                'Spec TD006
                SqlDataSource1.SelectCommand = QSql & " and L.TD006 like '%" & txtspec.Text & "%' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count

            ElseIf txtpono.Text = "" And txtitem.Text = "" And txtspec.Text = "" And txtsup.Text <> "" Then
                'Supp ID H.TC004
                SqlDataSource1.SelectCommand = QSql & " and H.TC004 ='" & txtsup.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count

            ElseIf txtpono.Text = "" And txtitem.Text = "" And txtspec.Text = "" And txtsup.Text = "" Then
                'PO Type L.TD001
                SqlDataSource1.SelectCommand = QSql & " and L.TD001 ='" & DDLPoType.SelectedValue & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count

            ElseIf txtpono.Text <> "" And txtitem.Text = "" And txtspec.Text = "" And txtsup.Text = "" Then
                'PO Type L.TD001 and PO No TD002
                SqlDataSource1.SelectCommand = QSql & " and L.TD001 ='" & DDLPoType.SelectedValue & "' and L.TD002 = '" & txtpono.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count

            ElseIf txtpono.Text = "" And txtitem.Text <> "" And txtspec.Text = "" And txtsup.Text <> "" Then
                'Item TD004  and Spec TD006 
                SqlDataSource1.SelectCommand = QSql & " and L.TD004 ='" & txtitem.Text & "' and L.TD006 like '%" & txtspec.Text & "%' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count

            End If

        ElseIf txtfrom.Text <> "" And txtto.Text <> "" Then    'ถ้าใส่วันที่
            If DDLSType.SelectedIndex = 0 Then        'ALL

                If txtpono.Text <> "" Then  'PO No TD002
                    SqlDataSource1.SelectCommand = QSql & "  and L.TD002 ='" & txtpono.Text & "' and L.TD012 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf txtitem.Text <> "" Then    'Item   TD004
                    SqlDataSource1.SelectCommand = QSql & "  and L.TD004 ='" & txtitem.Text & "' and L.TD012 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf txtspec.Text <> "" Then    'Spec   TD006
                    SqlDataSource1.SelectCommand = QSql & " and L.TD006 ='" & txtspec.Text & "' and L.TD012 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
                ElseIf txtsup.Text <> "" Then    'Supp ID H.TC004
                    SqlDataSource1.SelectCommand = QSql & " and H.TC004 ='" & txtsup.Text & "' and L.TD012 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()
                    lblcount.Text = GridView1.Rows.Count
              
                End If

            ElseIf DDLSType.SelectedIndex <> 0 Then    '2201,2202,2203,2204,2205
                If txtpono.Text <> "" Then       'Sales Order No   TD002
                    SqlDataSource1.SelectCommand = QSql & " and L.TD002 ='" & txtpono.Text & "' and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD012 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()

                    lblcount.Text = GridView1.Rows.Count
                ElseIf txtitem.Text <> "" Then    'Item   TD004
                    SqlDataSource1.SelectCommand = QSql & " and L.TD004 ='" & txtitem.Text & "' and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD012 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()

                    lblcount.Text = GridView1.Rows.Count
                ElseIf txtspec.Text <> "" Then    'Spec   TD006
                    SqlDataSource1.SelectCommand = QSql & " and L.TD006 like '%" & txtspec.Text & "%' and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD012 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()

                    lblcount.Text = GridView1.Rows.Count
                ElseIf txtsup.Text <> "" Then    'Supp ID H.TC004
                    SqlDataSource1.SelectCommand = QSql & " and H.TC004 ='" & txtsup.Text & "' and L.TD007 ='" & DDLSType.SelectedValue & "' and L.TD012 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                    GridView1.DataBind()

                    lblcount.Text = GridView1.Rows.Count
                End If
            End If
        ElseIf txtfrom.Text = "" And txtto.Text <> "" Then    'txtto

            SqlDataSource1.SelectCommand = QSql & " and L.TD012 < '" & txtto.Text & "'"
            GridView1.DataBind()
            lblcount.Text = GridView1.Rows.Count

            If txtpono.Text <> "" Then
                SqlDataSource1.SelectCommand = QSql & " and L.TD002 ='" & txtpono.Text & "' and L.TD012 < '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count
            ElseIf txtitem.Text <> "" Then
                SqlDataSource1.SelectCommand = QSql & " and L.TD004 ='" & txtitem.Text & "' and L.TD012 < '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count
            ElseIf txtspec.Text <> "" Then
                SqlDataSource1.SelectCommand = QSql & " and L.TD006 like '%" & txtspec.Text & "%' and L.TD012 < '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count
            ElseIf txtsup.Text <> "" Then
                SqlDataSource1.SelectCommand = QSql & " and H.TC004 ='" & txtsup.Text & "' and L.TD012 < '" & txtto.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count
            End If

        ElseIf txtfrom.Text <> "" And txtto.Text = "" Then
            If txtpono.Text <> "" Then
                SqlDataSource1.SelectCommand = QSql & " and L.TD002 ='" & txtpono.Text & "' and L.TD012 > '" & txtfrom.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count
            ElseIf txtitem.Text <> "" Then
                SqlDataSource1.SelectCommand = QSql & " and L.TD004 ='" & txtitem.Text & "' and L.TD012 > '" & txtfrom.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count
            ElseIf txtspec.Text <> "" Then
                SqlDataSource1.SelectCommand = QSql & " and L.TD006 like '%" & txtspec.Text & "%' and L.TD012 > '" & txtfrom.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count
            ElseIf txtsup.Text <> "" Then
                SqlDataSource1.SelectCommand = QSql & " and H.TC004 ='" & txtsup.Text & "' and L.TD012 > '" & txtfrom.Text & "' and L.TD001 ='" & DDLPoType.SelectedValue & "'"
                GridView1.DataBind()
                lblcount.Text = GridView1.Rows.Count
            End If
        End If

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click

        'ExportData("application/vnd.xls", "PoNotClose.xls")
        InsertPoNotClose()

    End Sub

    Private Sub InsertPoNotClose()

        Dim DelQuo As String = "delete from PoNotClose"
        Conn_SQL.Exec_Sql(DelQuo, Conn_SQL.MIS_ConnectionString)

        If GridView1.Rows.Count > 0 Then
            For M_count As Integer = 0 To GridView1.Rows.Count - 1

                Dim PType As String = GridView1.Rows(M_count).Cells(0).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim PNo As String = GridView1.Rows(M_count).Cells(1).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Item As String = GridView1.Rows(M_count).Cells(2).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Spec As String = GridView1.Rows(M_count).Cells(3).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim SoType As String = GridView1.Rows(M_count).Cells(4).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Pqty As String = GridView1.Rows(M_count).Cells(5).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim DQty As String = GridView1.Rows(M_count).Cells(6).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Unit As String = GridView1.Rows(M_count).Cells(7).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim CDelDate As String = GridView1.Rows(M_count).Cells(8).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim BQty As String = GridView1.Rows(M_count).Cells(9).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim SuppID As String = GridView1.Rows(M_count).Cells(10).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim RQty As String = GridView1.Rows(M_count).Cells(11).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim AQty As String = GridView1.Rows(M_count).Cells(12).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim PDelDate As String = GridView1.Rows(M_count).Cells(13).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim RequestDate As String = GridView1.Rows(M_count).Cells(14).Text.Replace(" ", "").Replace("&nbsp;", "")

                Dim InSQL As String = "Insert into PoNotClose(Type,No,Item,Spec,SoType,PurQty,DelQty,Unit,ConDelDate,BalQty,Supp,ReceiptQty,Accqty,PlanDelDate,RequestQty)"
                InSQL = InSQL & " Values('" & PType & "','" & PNo & "',"
                InSQL = InSQL & "'" & Item & "','" & Spec & "',"
                InSQL = InSQL & "'" & SoType & "','" & Pqty & "',"
                InSQL = InSQL & "'" & DQty & "','" & Unit & "',"
                InSQL = InSQL & "'" & CDelDate & "','" & BQty & "',"
                InSQL = InSQL & "'" & SuppID & "','" & RQty & "',"
                InSQL = InSQL & "'" & AQty & "','" & PDelDate & "',"
                InSQL = InSQL & "'" & RequestDate & "')"

                Conn_SQL.Exec_Sql(InSQL, Conn_SQL.MIS_ConnectionString)
            Next
        End If

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('PoNotClose1.aspx?');", True)

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Sub ExportData(ByVal _contentType As String, ByVal fileName As String)

        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = _contentType
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        frm.Attributes("runat") = "server"
        frm.Controls.Add(GridView1)
        GridView1.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub

End Class