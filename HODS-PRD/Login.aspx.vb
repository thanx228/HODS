Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports MIS_HTI.DataControl

Public Class Login
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim sc As New Global_asax
    Dim CreateTable As New CreateTable

    Dim dbconn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            Session("UserId") = ""
            Session("UserName") = ""
            Session("PassWord") = ""
            Session("UserGroup") = ""
            Session("MenuId") = ""
            Session("ComName") = ""
            Session("ComIP") = ""
            Session("Dept") = ""
            CreateTable.CreateLogInHistoryTable()

        End If
        If UserName.Text.Trim = "" Then
            Me.UserName.Focus()
        Else
            Me.PassWord.Focus()
        End If

    End Sub
    Protected Function Login(ByVal id As String, ByVal password As String) As Boolean

        Dim Program As New Data.DataTable
        Dim SelSql As String = "select Id,UserName,UserPassWord,UserGroup,Dept from UserInfo  WHERE UserName='" & id & "' and UserPassWord='" & password & "'"
        'With New DataRowControl(SelSql, VarIni.DBMIS, dbconn.WhoCalledMe)
        '    Session("UserId") = .Text("Id")
        '    Session("UserName") = .Text("UserName")
        '    Session("PassWord") = .Text("UserPassWord")
        '    Session("UserGroup") = .Text("UserGroup")
        '    Session("Dept") = .Text("Dept")
        '    Return True
        'End With
        'Return False

        Program = Conn_SQL.Get_DataReader(SelSql, Conn_SQL.MIS_ConnectionString)
        If Program.Rows.Count <> 0 Then
            Session("UserId") = Trim(Program.Rows(0).Item("Id"))
            Session("UserName") = Trim(Program.Rows(0).Item("UserName"))
            Session("PassWord") = Program.Rows(0).Item("UserPassWord")
            Session("UserGroup") = Program.Rows(0).Item("UserGroup")
            Session("Dept") = Program.Rows(0).Item("Dept")
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub SetIPAddress()
        Dim strHostName As String
        Dim strIPAddress As String
        'If HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR") IsNot Nothing Then
        '    strHostName = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        '    strIPAddress = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        'Else
        '    strHostName = HttpContext.Current.Request.UserHostName
        '    strIPAddress = HttpContext.Current.Request.UserHostAddress
        'End If
        'strHostName = System.Net.Dns.GetHostName()'get host name
        strHostName = Request.UserHostName.ToString
        'strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString() 'get host ip address
        strIPAddress = Request.UserHostAddress.ToString
        'strIPAddress = Request.ServerVariables("REMOTE_ADDR")

        Session("ComName") = strHostName
        Session("ComIP") = strIPAddress
    End Sub
    '    protected void GetUser_IP()
    '{
    '    string VisitorsIPAddr = string.Empty;
    '    if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
    '    {
    '        VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
    '    }
    '    else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
    '    {
    '        VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
    '    }
    '    uip.Text = "Your IP is: " + VisitorsIPAddr;
    '}


    Sub LogInHistory()
        SetIPAddress()
        Dim userId As String = Session("UserId")
        Dim dateToday As String = DateTime.Now.ToString("yyyyMMdd HH:mm:ss", New CultureInfo("en-US"))
        Dim Cname As String = Session("ComName")
        Dim Cip As String = Session("ComIP")
        Dim ISQL As String = "insert into LogInHistory(UserId,ComName,IpAddr,LogInDate) values('" & userId & "','" & Cname & "','" & Cip & "','" & dateToday & "') "
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Protected Sub Btlogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btlogin.Click
        Dim userId As String = UserName.Text.Trim,
            pwd As String = PassWord.Text.Trim
        If Login(userId, pwd) Then
            LogInHistory()
            Response.Redirect("Home.aspx")
        Else
            lbError.Text = "Please Check UserName or PassWord "
        End If
    End Sub
End Class