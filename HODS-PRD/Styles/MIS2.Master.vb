Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class MIS2
    Inherits System.Web.UI.MasterPage

    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            login.Text = Session("UserName")
            lbUserGroup.Text = Session("UserGroup")
            'PopulateRootLevel()
            createMenu()
        End If

    End Sub
    Private Sub createMenu()
        Dim userGroup As String = lbUserGroup.Text
        userGroup = "SYS"
        'Dim SelSQL As String = "select Id,ParentId,Line,Name,Prog,(select count(*) from Menu where ParentId=sc.Id)childnodecount from Menu sc where ParentId ='0' order by ParentId,Line"
        Dim SelSQL As String = ""
        SelSQL = " select Id,ParentId,Line,Name,Prog,(select count(*) from Menu where ParentId=sc.Id)childnodecount from Menu sc where  ParentId =0 and " & _
                 " Id in (select ParentId from UserGroup UG left join Menu M on M.Id=UG.IdMenu  where UG.UserGroup='" & userGroup & "') " & _
                 " order by Line "
        Dim dt As New DataTable()
        'Dim Program As New DataTable
        dt = Conn_SQL.Get_DataReader(SelSQL, Conn_SQL.MIS_ConnectionString)
        For Each drParent As DataRow In dt.Rows
            Dim parentMenu As New MenuItem(drParent("Name").ToString())
            mnMain.Items.Add(parentMenu)

            Dim SelSql1 As String = "select * from UserGroup u left join Menu m on  WHERE UserGroup='" & userGroup & "' and IdMenu='" & drParent("Id").ToString() & "'"
            SelSql1 = " select M.Name,M.Prog from Menu M " & _
                      " left join UserGroup UG on UG.IdMenu=M.Id " & _
                      " where ParentId='" & drParent("Id").ToString() & "' and UG.UserGroup='" & userGroup & "' " & _
                      " order by M.Line "
            Dim dt1 As New DataTable()
            dt1 = Conn_SQL.Get_DataReader(SelSql1, Conn_SQL.MIS_ConnectionString)
            For Each drChild As DataRow In dt1.Rows
                Dim ChildMenu As New MenuItem(drChild("Name").ToString(), "", "", "../" & drChild("Prog").ToString())
                parentMenu.ChildItems.Add(ChildMenu)
            Next
        Next
    End Sub





    'Private Sub PopulateRootLevel()

    '    Dim userGroup As String = lbUserGroup.Text
    '    'Dim SelSQL As String = "select Id,ParentId,Line,Name,Prog,(select count(*) from Menu where ParentId=sc.Id)childnodecount from Menu sc where ParentId ='0' order by ParentId,Line"
    '    Dim SelSQL As String = ""
    '    SelSQL = " select Id,ParentId,Line,Name,Prog,(select count(*) from Menu where ParentId=sc.Id)childnodecount from Menu sc where  ParentId =0 and " & _
    '             " Id in (select ParentId from UserGroup UG left join Menu M on M.Id=UG.IdMenu  where UG.UserGroup='" & userGroup & "') " & _
    '             " order by ParentId,Line "
    '    Dim da As New SqlDataAdapter(SelSQL, Conn_SQL.MIS_ConnectionString)
    '    Dim dt As New DataTable()
    '    da.Fill(dt)
    '    PopulateNodes(dt, TreeView1.Nodes)
    'End Sub

    'Private Sub PopulateNodes(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
    '    For Each dr As DataRow In dt.Rows
    '        Dim tn As New TreeNode()

    '        tn.Value = dr("Id").ToString()
    '        Dim foreColor As String = ""
    '        If dr("ParentId").ToString.Trim = 0 Then
    '            tn.SelectAction = TreeNodeSelectAction.None
    '            foreColor = "blue"
    '        Else
    '            Dim Program As New Data.DataTable
    '            Dim SelSql As String = "select * from UserGroup  WHERE UserGroup='" & Session("UserGroup") & "' and IdMenu='" & dr("Id").ToString() & "'"
    '            Program = Conn_SQL.Get_DataReader(SelSql, Conn_SQL.MIS_ConnectionString)
    '            If Program.Rows.Count <> 0 Then
    '                tn.NavigateUrl = "../" & dr("Prog").ToString
    '                TreeView1.NodeStyle.ForeColor = Drawing.Color.Black
    '                foreColor = "black"
    '            Else
    '                tn.SelectAction = TreeNodeSelectAction.None
    '                foreColor = "red"
    '            End If
    '        End If
    '        tn.Text = "<div style='color:" & foreColor & "'>" + dr("Name").ToString() + "</div>"
    '        nodes.Add(tn)
    '        tn.PopulateOnDemand = (CInt(dr("childnodecount")) > 0)
    '    Next
    'End Sub

    'Private Sub PopulateSubLevel(ByVal parentid As Integer, ByVal parentNode As TreeNode)
    '    Dim objConn As New SqlConnection("Data Source=192.168.50.1;Initial Catalog= DBMIS;User Id=mis;Password=Mis2012;Max Pool Size=100")
    '    Dim objCommand As New SqlCommand("select Id,ParentId,Line,Name,Prog,(select count(*) FROM Menu " _
    '          & "WHERE ParentId=sc.Id ) childnodecount FROM Menu sc where ParentId=@parentID order by Line", objConn)
    '    objCommand.Parameters.Add("@parentID", SqlDbType.Int).Value = parentid
    '    Dim da As New SqlDataAdapter(objCommand)
    '    Dim dt As New DataTable()
    '    da.Fill(dt)
    '    PopulateNodes(dt, parentNode.ChildNodes)
    'End Sub
    'Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodePopulate
    '    PopulateSubLevel(CInt(e.Node.Value), e.Node)
    'End Sub


End Class