Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class MenuNew
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim CreateTable As New CreateTable
    Dim ControlForm As New ControlDataForm
    Dim table As String = "Menu"
    Dim dbConn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CreateTable.CreateMenuTable()
            Dim ddlCont As New DropDownListControl
            Dim SQL As String = "select Id,cast(Id as varchar(5))+':'+cast(Line as varchar(5))+':'+Name as Name from " & table & " where isParent='Y' order by ParentId,Line,Id"
            ddlCont.showDDL(ddlParent, SQL, VarIni.DBMIS, "Name", "Id", 5)
        End If
    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click

        If tbLine.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Line is Null!!!", UpdatePanel1)
            tbLine.Focus()
            Exit Sub
        End If
        If tbName.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Name is Null!!!", UpdatePanel1)
            tbName.Focus()
            Exit Sub
        End If
        Dim SQL As String
        If lbIDSub.Text = "" Then 'insert
            SQL = "insert into " & table & "(ParentId,Line,Name,isParent)values(0," & CInt(tbLine.Text.Trim) & ",'" & tbName.Text.Trim & "','Y') "
        Else 'update
            SQL = "update " & table & " set Line=" & CInt(tbLine.Text.Trim) & ",Name='" & tbName.Text.Trim & "' where Id=" & CInt(lbID.Text.Trim) & ""
        End If
        'Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        gvMenu.DataBind()

    End Sub

    Private Sub gvMenu_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMenu.RowCommand
        If e.CommandName = "onEdit" Then
            Dim i As Integer = e.CommandArgument
            With gvMenu.Rows(i)
                lbID.Text = .Cells(0).Text.Replace(" ", "")
                tbLine.Text = .Cells(1).Text.Replace(" ", "")
                tbName.Text = .Cells(2).Text.Replace(" ", "")
            End With
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String,
          WHR As String
        Dim gvCont As New GridviewControl
        WHR = dbConn.Where("ParentId", ddlParent)
        SQL = "select * from " & table & " where ParentId<>0  " & WHR & " order by ParentId,Line,Id"
        gvCont.ShowGridView(gvMenuSub, SQL, VarIni.DBMIS)

    End Sub

    Protected Sub btSubSave_Click(sender As Object, e As EventArgs) Handles btSubSave.Click
        If ddlParent.Text.Trim = "ALL" Then
            show_message.ShowMessage(Page, "Parent is Null!!!", UpdatePanel1)
            ddlParent.Focus()
            Exit Sub
        End If
        If tbLineSub.Text.Trim = "" Or Not IsNumeric(tbLineSub.Text.Trim) Then
            show_message.ShowMessage(Page, "Line is Null or line is number only!!!", UpdatePanel1)
            tbLineSub.Focus()
            Exit Sub
        End If
        If tbNameSub.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Name is Null !!!", UpdatePanel1)
            tbLineSub.Focus()
            Exit Sub
        End If
        If ddlIsParent.Text = "N" And tbProg.Text = "" Then
            show_message.ShowMessage(Page, "Path link is Null !!!", UpdatePanel1)
            tbLineSub.Focus()
            Exit Sub
        End If
        Dim SQL As String
        If lbIDSub.Text.Trim = "" Then 'insert
            SQL = "insert into " & table & "(ParentId,Line,Name,Prog,isParent,MenuStatus)values(" & CInt(ddlParent.Text.Trim) & "," & CInt(tbLineSub.Text.Trim) & ",'" & tbNameSub.Text.Trim & "','" & tbProg.Text.Trim & "','" & ddlIsParent.Text.Trim & "','" & ddlStatus.Text.Trim & "')"
        Else 'update
            SQL = "update " & table & " set ParentId=" & CInt(ddlParent.Text.Trim) & ",Line=" & CInt(tbLineSub.Text.Trim) & ",Name='" & tbNameSub.Text.Trim & "',Prog='" & tbProg.Text.Trim & "',isParent='" & ddlIsParent.Text.Trim & "',MenuStatus='" & ddlStatus.Text.Trim & "' where Id=" & CInt(lbIDSub.Text.Trim) & " "
        End If

        dbConn.TransactionSQL(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        btShow_Click(sender, e)

        tbLineSub.Text = ""
        tbNameSub.Text = ""
        tbProg.Text = ""
        ddlIsParent.Text = ""
        ddlStatus.Text = ""

    End Sub

    Private Sub gvMenuSub_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMenuSub.RowCommand
        Dim i As Integer = e.CommandArgument
        If e.CommandName = "onEditSub" Then
            With gvMenuSub.Rows(i)
                lbIDSub.Text = .Cells(0).Text.Replace(" ", "")
                ddlParent.Text = .Cells(1).Text.Replace(" ", "")
                tbLineSub.Text = .Cells(2).Text.Replace(" ", "")
                tbNameSub.Text = .Cells(3).Text.Trim
                tbProg.Text = .Cells(4).Text.Replace(" ", "")
                ddlIsParent.Text = .Cells(5).Text.Replace(" ", "")
                ddlStatus.Text = .Cells(6).Text.Replace(" ", "")
            End With
        End If
        If e.CommandName = "onDeleteSub" Then
            With gvMenuSub.Rows(i)
                Dim ID As Integer = CInt(.Cells(0).Text.Replace(" ", ""))
                Dim DSQL As String = " delete from " & table & " where Id=" & ID
                Conn_SQL.Exec_Sql(DSQL, Conn_SQL.MIS_ConnectionString)
            End With
        End If
    End Sub
End Class