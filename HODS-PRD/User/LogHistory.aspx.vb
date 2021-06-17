Imports System.Globalization
Public Class LogHistory
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SaleUndeliveryStatus" & Session("UserName"), gvShow)
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = "",
            strDate As String = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US")),
            date1 As String = tbDate.Text.Trim
        If tbDate.Text <> "" Then
            strDate = ConfigDate.dateFormat2(date1)
        End If

        If ddlType.Text = "1" Then 'Login
            'WHR = WHR & Conn_SQL.Where("", tbMenu)
            WHR = WHR & Conn_SQL.Where("UserName", tbUser)
            WHR = WHR & Conn_SQL.Where("IpAddr", tbAdd)
            WHR = WHR & Conn_SQL.Where("ComName", tbCom)
            WHR = WHR & Conn_SQL.Where("LogInDate", " like  '" & strDate & "%' ")

            SQL = " select UserName as 'User Name',NameSurname as 'Name',Dept,UserGroup as 'User Group',ComName as 'Computer Name',IpAddr as 'IP Address',LogInDate as 'Login Datetime' " & _
                  " from LogInHistory L left join UserInfo U on U.Id=L.UserId " & _
                  " where  UserName<>'' " & WHR & _
                  " order by UserName,LogInDate,ComName"
        Else 'Menu
            WHR = WHR & Conn_SQL.Where("Name", tbMenu)
            WHR = WHR & Conn_SQL.Where("UserName", tbUser)
            WHR = WHR & Conn_SQL.Where("IpAddr", tbAdd)
            WHR = WHR & Conn_SQL.Where("ComName", tbCom)
            WHR = WHR & Conn_SQL.Where("InDateTime", " like  '" & strDate & "%' ")

            SQL = " select UserName as 'User Name',NameSurname as 'Name',Dept,UserGroup as 'User Group',ComName as 'Computer Name',IpAddr as 'IP Address',Name as 'Menu Name',InDateTime as 'In Date Time',outDateTime as 'Out Date Time' " & _
                  " from LogHistory L left join UserInfo U on U.Id=L.UserId " & _
                  " left join Menu M on M.Id=L.MenuId " & _
                  " where L.UserId<>'' " & WHR & _
                  " order by UserName,Name,InDateTime"
        End If
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
    End Sub
End Class