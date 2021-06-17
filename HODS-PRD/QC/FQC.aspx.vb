Imports System.Data
Imports System.Data.SqlClient

Public Class FQC
    Inherits System.Web.UI.Page
    Dim Conn_sql As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim strSql As String = "SELECT MW001,MW001+' : '+MW002 AS ConcatField FROM CMSMW order by MW001"
            Dim program As Data.DataTable = Conn_sql.Get_DataReader(strSql, Conn_sql.ERP_ConnectionString)
            DDLoperation.DataSource = program
            DDLoperation.DataTextField = "ConcatField"
            DDLoperation.DataValueField = "MW001"
            DDLoperation.DataBind()
        End If
    End Sub

    Protected Sub BuReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuReport.Click

        If txtno.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert No", UpdatePanel1)
            Me.GridView1.DataBind()
            Exit Sub
        ElseIf txtno.Text <> "" Then
            SqlDataSource2.SelectCommand = "select TA001,TA002,TA003,TA006,TA015,TA020,TA034,TA035,TA057,MG003,MG006 from MOCTA M left join QMSMG Q on(M.TA006 = Q.MG002) where TA001 ='" & DDLType.SelectedValue & "' and TA002 = '" & txtno.Text & "' and MG008 = '" & DDLoperation.SelectedValue & "'"
        End If

        Me.GridView1.DataBind()
        GridView1.AllowPaging = False
        InsertDate()

    End Sub

    Private Sub InsertDate()
        Dim DelScrap As String = "delete from Inspection"
        Conn_sql.Exec_Sql(DelScrap, Conn_sql.MIS_ConnectionString)

        Dim SqlInsepec As String
        For M_count As Integer = 0 To GridView1.Rows.Count - 1
            Dim TA001 As String = GridView1.Rows(M_count).Cells(0).Text
            Dim TA002 As String = GridView1.Rows(M_count).Cells(1).Text
            Dim TA003 As String = GridView1.Rows(M_count).Cells(2).Text
            Dim TA006 As String = GridView1.Rows(M_count).Cells(3).Text
            Dim TA015 As String = GridView1.Rows(M_count).Cells(4).Text.Replace(".000000", ".00")
            Dim TA020 As String = GridView1.Rows(M_count).Cells(5).Text
            Dim TA034 As String = GridView1.Rows(M_count).Cells(6).Text
            Dim TA035 As String = GridView1.Rows(M_count).Cells(7).Text
            Dim TA057 As String = GridView1.Rows(M_count).Cells(8).Text
            Dim MG003 As String = GridView1.Rows(M_count).Cells(9).Text
            Dim MG006 As String = GridView1.Rows(M_count).Cells(10).Text.Replace("&nbsp;", "").Replace("&lt;", "<")
            Dim sqldia As String = "select ME002 from QMSME where ME001='" & MG003 & "'"
            Dim ME002 As String = Conn_sql.Get_value(sqldia, Conn_sql.ERP_ConnectionString)

            SqlInsepec = "insert into Inspection(TA001,TA002,TA003,TA006,TA015,TA020,TA034,TA035,TA057,MG003,MG006,ME002) values('" & TA001 & "','" & TA002 & "','" & TA003 & "','" & TA006 & "','" & TA015 & "','" & TA020 & "','" & TA034 & "','" & TA035 & "','" & TA057 & "','" & MG003 & "','" & MG006 & "','" & ME002 & "')"
            Conn_sql.Exec_Sql(SqlInsepec, Conn_sql.MIS_ConnectionString)
        Next
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('FQC1.aspx?Type=" + DDLType.SelectedValue + "&No=" + txtno.Text + "');", True)

    End Sub

End Class