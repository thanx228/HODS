Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class ItemCycle
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Session("UserName") = "" Then
        '    Response.Redirect("Login.aspx")
        'End If

        GridView1.Visible = False
        LCount.Text = 0
    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click
        GridView1.Visible = True
        GridView1.AllowPaging = False
        SqlDataSource1.SelectCommand = "select MB001,MB002,MB003,MB004,MC007,MC002 from INVMC C left join INVMB B on(B.MB001 =C.MC001) where MC002='" & DDLWh.SelectedValue & "' and MC007 > 0"
        GridView1.DataBind()
        LCount.Text = GridView1.Rows.Count
    End Sub

    Protected Sub Busave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busave.Click

        InsertItemCycle()
    End Sub

    Private Sub InsertItemCycle()
        Dim CreateDate As Date
        CreateDate = Date.Now.Date.ToString("dd-MMM-yyyy")
        Dim PNo As String
        Dim PrintNo As New Data.DataTable
        PrintNo = Conn_sql.Get_DataReader("  select isnull(MAX(PrintNo),0) from ItemCycle e", Conn_sql.MIS_ConnectionString)
        If PrintNo.Rows(0).Item(0) = 0 Then
            PNo = 1
        Else
            PNo = CInt(PrintNo.Rows(0).Item(0)) + 1
        End If
        If GridView1.Rows.Count > 0 Then
            For M_count As Integer = 0 To GridView1.Rows.Count - 1
              
                Dim Item As String = GridView1.Rows(M_count).Cells(0).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Desc As String = GridView1.Rows(M_count).Cells(1).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Spec As String = GridView1.Rows(M_count).Cells(2).Text.Replace(" ", "").Replace("&nbsp;", "").Replace("&#39;", "").Replace("&quot;", "")
                Dim Qty As String = GridView1.Rows(M_count).Cells(3).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Unit As String = GridView1.Rows(M_count).Cells(4).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Wh As String = GridView1.Rows(M_count).Cells(5).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim WHName As String = Conn_sql.Get_value("select MC002 from [JINPAO80].[dbo].[CMSMC] where MC001='" & Wh & "'", Conn_sql.ERP_ConnectionString)

                Dim program As New Data.DataTable
                Dim RNo As Integer
                Dim NoShow As String
                Dim docstr As String = Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                program = Conn_sql.Get_DataReader("SELECT isnull(max(RunNo),0) FROM ItemCycle where RunNo like '%" & docstr & "%' ", Conn_sql.MIS_ConnectionString)
                If program.Rows(0).Item(0) = 0 Then
                    RNo = docstr & "0001"
                    NoShow = DDLWh.SelectedValue.Replace(" ", "") & "-" & RNo
                Else
                    RNo = CInt(program.Rows(0).Item(0)) + 1
                    NoShow = DDLWh.SelectedValue.Replace(" ", "") & "-" & RNo
                End If

                Dim InSQL As String = "Insert into ItemCycle(Item,[Desc],Spec,Wid,WName,Qty,Unit,UserID,RunNo,CreateDate,PrintNo,NoShow)"
                InSQL = InSQL & " Values('" & Item & "','" & Desc & "',"
                InSQL = InSQL & "'" & Spec & "','" & Wh & "',"
                InSQL = InSQL & "'" & WHName & "','" & Qty & "',"
                InSQL = InSQL & "'" & Unit & "',"
                InSQL = InSQL & "'" & Session("UserName") & "','" & RNo & "','" & CreateDate & "','" & PNo & "','" & NoShow & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
            Next
        End If

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ItemCycle1.aspx?PNo=" + PNo + "');", True)
    End Sub

    Protected Sub BuExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuExcel.Click

        ItemList()
    End Sub

    Private Sub ItemList()

        Dim DelItemList As String = "delete from ItemList"
        Conn_sql.Exec_Sql(DelItemList, Conn_sql.MIS_ConnectionString)

        If GridView1.Rows.Count > 0 Then
            For M_count As Integer = 0 To GridView1.Rows.Count - 1

                Dim Item As String = GridView1.Rows(M_count).Cells(0).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Desc As String = GridView1.Rows(M_count).Cells(1).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Spec As String = GridView1.Rows(M_count).Cells(2).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Qty As String = GridView1.Rows(M_count).Cells(3).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Unit As String = GridView1.Rows(M_count).Cells(4).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim Wh As String = GridView1.Rows(M_count).Cells(5).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim CountQty As String = GridView1.Rows(M_count).Cells(6).Text.Replace(" ", "").Replace("&nbsp;", "")
                Dim CountBy As String = GridView1.Rows(M_count).Cells(7).Text.Replace(" ", "").Replace("&nbsp;", "")
               
                Dim InSQL As String = "Insert into ItemList(Item,[Desc],Spec,Qty,Unit,Wh,CountQty,CountBy)"
                InSQL = InSQL & " Values('" & Item & "','" & Desc & "',"
                InSQL = InSQL & "'" & Spec & "','" & Qty & "',"
                InSQL = InSQL & "'" & Unit & "','" & Wh & "',"
                InSQL = InSQL & "'" & CountQty & "','" & CountBy & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
            Next
        End If

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ItemCycle2.aspx?');", True)

    End Sub
End Class