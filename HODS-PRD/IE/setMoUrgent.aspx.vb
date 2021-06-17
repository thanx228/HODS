Public Class setMoUrgent
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim CreateTable As New CreateTable
    Dim ControlForm As New ControlDataForm
    Dim table As String = "MoUrgent"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 in ('5102','5104','5106','5108','5201','5202','5301') order by MQ002"
            ControlForm.showDDL(ddlWorkType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            CreateTable.CreateMoUrgentTable()
        End If
    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click

        Dim moNo As String = tbWorkNo.Text.Trim,
            moType As String = ddlWorkType.Text.Trim,
            SQL As String = "",
            Program As New DataTable
        If moType = "ALL" Then
            show_message.ShowMessage(Page, "Please select MO Type  !!!", UpdatePanel1)
            ddlWorkType.Focus()
            Exit Sub
        End If

        If moNo = "" Then
            show_message.ShowMessage(Page, "MO No. Is empty !!!", UpdatePanel1)
            tbWorkNo.Focus()
            Exit Sub
        End If

        SQL = "select * from " & table & " where TA001='" & moType & "' and TA002='" & moNo & "' "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If Program.Rows.Count > 0 Then
            show_message.ShowMessage(Page, " MO Type=" & moType & " and Mo no=" & moNo & " is added !!!", UpdatePanel1)
            Exit Sub
        End If

        SQL = "select TA001,TA002 from MOCTA where TA001='" & moType & "' and TA002='" & moNo & "' and TA011 not in('y','Y') "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        If Program.Rows.Count = 0 Then
            show_message.ShowMessage(Page, "Please check MO Type=" & moType & " and Mo no =" & moNo & " is not correct !!!", UpdatePanel1)
            Exit Sub
        End If

        For i As Integer = 0 To Program.Rows.Count - 1
            Dim ISQL As String = " insert into " & table & "(TA001,TA002)values ('" & Program.Rows(i).Item("TA001") & "','" & Program.Rows(i).Item("TA002") & "')"
            Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        Next
        SqlDataSourceMO.SelectCommand = "select * from " & table & " order by TA001,TA002 desc "
        gvShow.DataBind()
        show_message.ShowMessage(Page, " MO Type=" & moType & " and Mo no=" & moNo & " is add completed !!!", UpdatePanel1)

        ddlWorkType.Text = "ALL"
        tbWorkNo.Text = ""

    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        Dim moNo As String = tbWorkNo.Text.Trim,
            moType As String = ddlWorkType.Text.Trim,
            SQL As String = " select * from " & table

        If moType <> "ALL" Or moNo <> "" Then
            If moType <> "ALL" And moNo <> "" Then
                SQL = SQL & " where TA001='" & moType & "' and TA002 like '%" & moNo & "%' "
            Else
                If moNo <> "" Then
                    SQL = SQL & " where TA002 like '%" & moNo & "%' "
                Else
                    SQL = SQL & " where TA001='" & moType & "' "
                End If
            End If
        End If
        SQL = SQL & " order by TA001,TA002 DESC"
        SqlDataSourceMO.SelectCommand = SQL
        gvShow.DataBind()
    End Sub

    Protected Sub btClear_Click(sender As Object, e As EventArgs) Handles btClear.Click
        Dim SQL As String = "",
            DSQL As String = "",
            Program As New DataTable

        SQL = " select T.TA001,T.TA002 from " & table & _
              " T left join JINPAO80.dbo.MOCTA A on A.TA001=T.TA001 and T.TA002=T.TA002 " & _
              " where A.TA011 in('y','Y')  "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            DSQL = "delete from " & table & " where TA001='" & Program.Rows(i).Item("TA001") & "' and TA002='" & Program.Rows(i).Item("TA002") & "' "
            Conn_SQL.Exec_Sql(DSQL, Conn_SQL.MIS_ConnectionString)
        Next
        SqlDataSourceMO.SelectCommand = "select * from " & table & " order by TA001,TA002 desc "
        gvShow.DataBind()
        show_message.ShowMessage(Page, " Clear MO finished is completed !!!", UpdatePanel1)

    End Sub
End Class