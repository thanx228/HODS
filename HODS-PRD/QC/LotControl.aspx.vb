Imports System.Data
Imports System.Data.SqlClient

Public Class LotControl
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Print.Visible = False
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        If WNo.Text = "" And Item.Text = "" And Spec.Text = "" And MODate.Text = "" Then
            show_message.ShowMessage(Page, "Please insert data", UpdatePanel1)
            GridView1.DataBind()
            Exit Sub
        Else
            If DDLType.SelectedValue <> "" And WNo.Text = "" Then
                SqlDataSource1.SelectCommand = "select TA001,TA002,TA003,TA006,TA035,TA015 from MOCTA where TA001='" & DDLType.SelectedValue & "'"
                GridView1.DataBind()
            ElseIf WNo.Text <> "" And DDLType.SelectedValue = "" Then
                SqlDataSource1.SelectCommand = "select TA001,TA002,TA003,TA006,TA035,TA015 from MOCTA where TA002='" & WNo.Text & "'"
                GridView1.DataBind()
            ElseIf DDLType.SelectedValue <> "" And WNo.Text <> "" Then
                SqlDataSource1.SelectCommand = "select TA001,TA002,TA003,TA006,TA035,TA015 from MOCTA where TA001='" & DDLType.SelectedValue & "' and TA002='" & WNo.Text & "'"
                GridView1.DataBind()
            ElseIf Item.Text <> "" Then
                SqlDataSource1.SelectCommand = "select TA001,TA002,TA003,TA006,TA035,TA015 from MOCTA where TA006 like '%" & Item.Text & "%'"
                GridView1.DataBind()
            ElseIf Spec.Text <> "" Then
                SqlDataSource1.SelectCommand = "select TA001,TA002,TA003,TA006,TA035,TA015 from MOCTA where TA035 like '%" & Spec.Text & "%'"
                GridView1.DataBind()
            ElseIf MODate.Text <> "" Then
                SqlDataSource1.SelectCommand = "select TA001,TA002,TA003,TA006,TA035,TA015 from MOCTA where TA003 like '%" & MODate.Text & "%'"
                GridView1.DataBind()
            End If
            'Print.Visible = True
        End If

    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        Dim delhead As String = "delete from LotHead"
        Conn_sql.Exec_Sql(delhead, Conn_sql.MIS_ConnectionString)

        Dim delline As String = "delete from LotLine"
        Conn_sql.Exec_Sql(delline, Conn_sql.MIS_ConnectionString)

        If e.CommandName = "OnPrint" Then
            Dim i As Integer = e.CommandArgument
            Dim WType As String = GridView1.Rows(i).Cells(0).Text 'TA001
            Dim WNo As String = GridView1.Rows(i).Cells(1).Text   'TA002

            Dim Program As New Data.DataTable
            Dim strSql As String = "SELECT * FROM MOCTA WHERE TA001='" & WType & "' and TA002='" & WNo & "'"
            Program = Conn_sql.Get_DataReader(strSql, Conn_sql.ERP_ConnectionString)

            For L_count As Integer = 0 To Program.Rows.Count - 1
                Dim TA001 As String = Program.Rows(L_count).Item("TA001") 'Work Type
                Dim TA002 As String = Program.Rows(L_count).Item("TA002") 'Work No
                Dim TA003 As String = Program.Rows(L_count).Item("TA003") 'mo date
                Dim TA004 As String = Program.Rows(L_count).Item("TA004") 'due date
                Dim TA006 As String = Program.Rows(L_count).Item("TA006") 'item
                Dim TA015 As String = Program.Rows(L_count).Item("TA015").ToString.Replace(".000000", "") 'qty
                Dim TA020 As String = Program.Rows(L_count).Item("TA020") 'wh
                Dim TA026 As String = Program.Rows(L_count).Item("TA026") 'so type
                Dim TA027 As String = Program.Rows(L_count).Item("TA027") 'so no
                Dim TA028 As String = Program.Rows(L_count).Item("TA028") 'seq
                Dim TA034 As String = Program.Rows(L_count).Item("TA034") 'product name 
                Dim TA035 As String = Program.Rows(L_count).Item("TA035") 'spec
                Dim Y03 As String = TA003.Substring(0, 4)
                Dim M03 As String = TA003.Substring(4, 2)
                Dim D03 As String = TA003.Substring(6, 2)
                Dim Modate As String = Y03 & "-" & M03 & "-" & D03
                Dim Y04 As String = TA004.Substring(0, 4)
                Dim M04 As String = TA004.Substring(4, 2)
                Dim D04 As String = TA004.Substring(6, 2)
                Dim Duedate As String = Y04 & "-" & M04 & "-" & D04
                Dim sqlwh As String = "select MC002 from CMSMC where MC001='" & TA020 & "'"
                Dim WhName As String = Conn_sql.Get_value(sqlwh, Conn_sql.ERP_ConnectionString)
                Dim sqlpart As String = "select MB028 from INVMB where MB001 = '" & TA006 & "'"
                Dim PartNo As String = Conn_sql.Get_value(sqlpart, Conn_sql.ERP_ConnectionString)
                Dim sqlcid As String = "select TC004 from COPTC where TC001 = '" & TA026 & "' and TC002 = '" & TA027 & "'"
                Dim CID As String = Conn_sql.Get_value(sqlcid, Conn_sql.ERP_ConnectionString)
                Dim Cname As String = Conn_sql.Get_value("select MA002 from COPMA where MA001 = '" & CID & "'", Conn_sql.ERP_ConnectionString)
                Dim balmanu As String = "*" & TA001 & "-" & TA002 & "*"
                Dim balspec As String = "*" & TA035.Replace(" ", "") & "*"
                Dim TypeNo As String = TA001 & "-" & TA002
                Dim SoNo As String = TA026 & "-" & TA027 & "-" & TA028
                Dim Sqlstr As String = ""
                Sqlstr = "insert into LotHead(TA001,TA002,TA003,TA004,TA006,TA015,TA020,WhName,TA026,TA027,TA028,TA034,TA035,CID,CName,PartNo,balmanu,balspec,WNo,SoNo) " & _
                "values('" & TA001 & "','" & TA002 & "','" & Modate & "','" & Duedate & "','" & TA006 & "','" & TA015 & "','" & TA020 & "','" & WhName & "','" & TA026 & "','" & TA027 & "','" & TA028 & "','" & TA034 & "','" & TA035 & "','" & CID & "','" & Cname & "','" & PartNo & "','" & balmanu & "','" & balspec & "','" & TypeNo & "','" & SoNo & "')"
                Conn_sql.Exec_Sql(Sqlstr, Conn_sql.MIS_ConnectionString)
            Next

            Dim Line As Data.DataTable
            Dim Sqlline As String = "SELECT * FROM SFCTA WHERE TA001='" & WType & "' and TA002='" & WNo & "'"
            Line = Conn_sql.Get_DataReader(Sqlline, Conn_sql.ERP_ConnectionString)

            For N_count As Integer = 0 To Line.Rows.Count - 1
                Dim DetailType As String = Line.Rows(N_count).Item("TA001")
                Dim DetailNo As String = Line.Rows(N_count).Item("TA002")
                Dim DetailSeq As String = Line.Rows(N_count).Item("TA003")
                Dim DetailProcess As String = "*" & DetailType & "-" & DetailNo & "-" & DetailSeq & "*"
                Dim OpID As String = Line.Rows(N_count).Item("TA004")
                Dim OpName As String = Conn_sql.Get_value("select MW002 from CMSMW where MW001='" & OpID & "'", Conn_sql.ERP_ConnectionString)
                Dim Wid As String = Line.Rows(N_count).Item("TA006")
                Dim Wname As String = Line.Rows(N_count).Item("TA007")
                Dim PDate As String = Line.Rows(N_count).Item("TA008")
                Dim Ypdate As String = PDate.Substring(0, 4)
                Dim Mpdate As String = PDate.Substring(4, 2)
                Dim Dpdate As String = PDate.Substring(6, 2)
                Dim FinDate As String = Ypdate & "-" & Mpdate & "-" & Dpdate
                Dim ddd As String = Line.Rows(N_count).Item("TA033")

                Dim Remark As String = ""
                If ddd = "0" Then
                    Remark = "No Inspection"
                ElseIf ddd = "1" Then
                    Remark = "Sampling(Minor)"
                ElseIf ddd = "2" Then
                    Remark = "Sampling(Normal)"
                ElseIf ddd = "3" Then
                    Remark = "Sampling(Serious)"
                ElseIf ddd = "4" Then
                    Remark = "Full Inspection"
                End If

                Dim Sqlstr As String = ""
                Sqlstr = "insert into LotLine(baldetail,OpID,OpName,Wid,Wname,TA008,TA033,TA001,TA002) " & _
                "values('" & DetailProcess & "','" & OpID & "','" & OpName & "','" & Wid & "','" & Wname & "','" & FinDate & "','" & Remark & "','" & DetailType & "','" & DetailNo & "')"
                Conn_sql.Exec_Sql(Sqlstr, Conn_sql.MIS_ConnectionString)
            Next

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('LotControl1.aspx?Type=" + WType + "');", True)
        End If

    End Sub
End Class