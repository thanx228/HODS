Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class MIS
    Inherits System.Web.UI.MasterPage

    'Dim CreateTable As New CreateTable
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim Conn_SQL As New ConnSQL
    Dim CreateTable As New CreateTable
    'Dim MyFilePath As String = Request.CurrentExecutionFilePath.ToString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim aa As String = Request.CurrentExecutionFilePath.ToString
        lbFile.Text = Request.CurrentExecutionFilePath.ToString
        If Not Page.IsPostBack Then
            If Session("UserId") = "" Then
                Response.Redirect(Server.UrlPathEncode(VarIni.PageLogin))
            End If

            Dim hdCont As New HeaderControl
            ucHeaderMasterPage.HeaderLable = hdCont.nameHeader(Request.CurrentExecutionFilePath.ToString)

            login.Text = Session("UserName")
            lbUserGroup.Text = Session("UserGroup")
            'PopulateRootLevel()
            'createMenu()
            RootMenu()
            Session.Timeout = 60
            Dim gvCont As New GridviewControl
            Dim strConn As New StringConnection
            'lbArea.Text = strConn.getArea
            If Page.Title = "" Then
                Page.Title = "HODS(ERP)"
            End If
            ''hlLogOut.NavigateUrl = VarIni.PageLogin
            'If lbFile.Text = "/Planing/PlanScheduleListMch.aspx" Then
            '    Dim MainContent As ContentPlaceHolder = CType(Master.FindControl("MainContent"), ContentPlaceHolder)
            '    If Not MainContent Is Nothing Then
            '        Dim aa As Label = CType(MainContent.FindControl("Label11"), Label)
            '        If Not aa Is Nothing Then
            '            aa.Text = aa.ID
            '        End If
            '    End If
            'End If

        End If
        UpdateProgress1.DisplayAfter = 100
        UpdateProgress1.Visible = True
        UpdateProgress1.DynamicLayout = True
        LogHistory()

    End Sub

    Function getMenuId(ByVal pathCheck As String) As String
        Dim SQL As String = "select Id from Menu where Prog='" & pathCheck & "' "
        Dim MenuId As String = ""
        Dim dt As DataTable
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If dt.Rows.Count > 0 Then
            MenuId = dt.Rows(0).Item("Id")
        End If
        Return MenuId
    End Function

    Sub LogHistory()
        Dim MyFilePath As String = lbFile.Text
        Dim MenuId As String = getMenuId(MyFilePath.Substring(1, MyFilePath.Length - 1))
        Dim Cname As String = Session("ComName")
        Dim Cip As String = Session("ComIP")

        If Session("MenuId") <> MenuId Then
            Dim userId As String = Session("UserId")
            Dim dateToday As String = DateTime.Now.ToString("yyyyMMdd HH:mm:ss", New CultureInfo("en-US"))

            'update out page
            If Session("MenuId") <> "" Then
                'get id last in 
                Dim SQL As String = "select top 1 Id from LogHistory where UserId='" & userId & "' and MenuId='" & Session("MenuId") & "' and IpAddr='" & Cip & "' order by InDateTime desc  "
                Dim MiD As String = ""
                Dim dt As DataTable
                dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                If dt.Rows.Count > 0 Then
                    MiD = dt.Rows(0).Item("Id")
                End If
                If MiD <> "" Then
                    Dim USQL As String = "update LogHistory set outDateTime='" & dateToday & "' where Id='" & MiD & "' "
                    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                End If
            End If
            'insert in page
            If MenuId <> "" Then
                Dim ISQL As String = "insert into LogHistory(UserId,MenuId,ComName,IpAddr,InDateTime) values ('" & userId & "','" & MenuId & "','" & Cname & "','" & Cip & "','" & dateToday & "') "
                Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
                Session("MenuId") = MenuId
            End If
        End If
    End Sub

    Sub popMenu(userGroup As String, ByRef parentMenu As MenuItem, ByVal parentId As String)
        Dim SelSql1 As String = "select * from UserGroup u left join Menu m on  WHERE UserGroup='" & userGroup & "' and IdMenu='" & parentId & "'"
        Dim SQL = "select UserGroup from UserGroup UG where  UG.IdMenu = M.Id and UserGroup='" & userGroup & "' "
        SelSql1 = " select M.Id,M.Name,M.Prog,isParent,isnull((" & SQL & "),'') as UserGroup  from Menu M " &
                  " where ParentId='" & parentId & "'  " &
                  " order by M.Line "
        'and UG.UserGroup='" & userGroup & "'
        Dim dt1 As New DataTable()
        dt1 = Conn_SQL.Get_DataReader(SelSql1, Conn_SQL.MIS_ConnectionString)
        For Each drChild As DataRow In dt1.Rows
            Dim ChildMenu As New MenuItem(drChild("Name").ToString())

            If drChild("isParent").ToString() = "Y" Then
                parentMenu.ChildItems.Add(ChildMenu)
                popMenu(userGroup, ChildMenu, drChild("Id").ToString())
            Else
                If drChild("UserGroup").ToString = userGroup Then
                    ChildMenu.NavigateUrl = "../" & drChild("Prog").ToString()
                Else
                    ChildMenu.Enabled = False
                End If
                parentMenu.ChildItems.Add(ChildMenu)
            End If
        Next

    End Sub

    Sub RootMenu()
        Dim userGroup As String = lbUserGroup.Text
        Dim SelSQL As String = ""
        SelSQL = " select Id,ParentId,Line,Name,Prog,(select count(*) from Menu where ParentId=sc.Id)childnodecount from Menu sc where  ParentId =0 and isParent='Y' and " &
                 " Id in (select ParentId from UserGroup UG left join Menu M on M.Id=UG.IdMenu  where UG.UserGroup='" & userGroup & "') " &
                 " order by Line "
        Dim dt As New DataTable()
        'Dim Program As New DataTable
        dt = Conn_SQL.Get_DataReader(SelSQL, Conn_SQL.MIS_ConnectionString)
        For Each drParent As DataRow In dt.Rows
            Dim parentMenu As New MenuItem(drParent("Name").ToString())
            mnMain.Items.Add(parentMenu)
            'If drParent("Id").ToString() = "10" Then
            '    Dim aa As String = ""
            'End If
            popMenu(userGroup, parentMenu, drParent("Id").ToString())
        Next
    End Sub

End Class