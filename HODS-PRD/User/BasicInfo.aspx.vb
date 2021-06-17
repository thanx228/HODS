Public Class BasicInfo
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Const table As String = "CodeInfo"
    Const codeHead As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            clearHead()
            clearSub()
        End If
    End Sub

    Protected Sub saveData(codeType As String, code As String, codeName As String, Optional remark As String = "-")
        Dim whrHash As Hashtable = New Hashtable,
           fldInsHash As Hashtable = New Hashtable

        whrHash.Add("CodeType", codeType) 'code type
        whrHash.Add("Code", code) 'code
        fldInsHash.Add("Name:N", codeName) 'name
        fldInsHash.Add("WC", remark) 'wc
        Dim SQL As String = Conn_SQL.GetSQL(table, fldInsHash, whrHash)
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)

    End Sub

    Protected Sub btHeadSave_Click(sender As Object, e As EventArgs) Handles btHeadSave.Click
        Dim SQL As String,
            dt As DataTable,
            code As String = tbHeadCode.Text.Trim,
            name As String = tbHeadName.Text.Trim


        If code = "" Or name = "" Then
            show_message.ShowMessage(Page, "Code or name is null,Please check it again!!!", UpdatePanel1)
            If code = "" Then
                tbHeadCode.Focus()
            Else
                tbHeadName.Focus()
            End If
            Exit Sub
        End If
        If tbHeadCode.Enabled Then
            SQL = "select Code from " & table & " where CodeType='" & codeHead & "' and Code='" & code & "' "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            If dt.Rows.Count > 0 Then
                show_message.ShowMessage(Page, "Code is Exist,Please check it again!!!", UpdatePanel1)
                tbHeadCode.Focus()
                Exit Sub
            End If
        End If

        saveData(codeHead, code, name)
        show_message.ShowMessage(Page, "Save Head Completed!!", UpdatePanel1)
        clearHead()
        clearSub()
        btHeadSearch_Click(sender, e)

    End Sub

    Protected Sub clearHead()
        tbHeadCode.Text = ""
        tbHeadCode.Enabled = True
        tbHeadName.Text = ""
        btHeadDelete.Visible = False
        Dim SQL As String = "select "
        SQL = "select rtrim(Code) MD001,Code+':'+Name MD002 from " & table & " where CodeType='" & codeHead & "' order by Code "
        ControlForm.showDDL(ddlHead, SQL, "MD002", "MD001", False, Conn_SQL.MIS_ConnectionString)

    End Sub

    Protected Sub clearSub()
        tbSubCode.Text = ""
        tbSubCode.Enabled = True
        tbSubName.Text = ""
        tbSubRemark.Text = ""
       btSubDelete.Visible = False
    End Sub

    Protected Sub btSubSave_Click(sender As Object, e As EventArgs) Handles btSubSave.Click
        Dim SQL As String,
            dt As DataTable,
            code As String = tbSubCode.Text.Trim,
            name As String = tbSubName.Text.Trim,
            remark As String = tbSubRemark.Text.Trim,
            headCode As String = ddlHead.Text.Trim


        If code = "" Or name = "" Then
            show_message.ShowMessage(Page, "Code or name is null,Please check it again!!!", UpdatePanel1)
            If code = "" Then
                tbHeadCode.Focus()
            Else
                tbHeadName.Focus()
            End If
            Exit Sub
        End If

        If remark = "" Then
            remark = "-"
        End If
        If tbSubCode.Enabled Then
            SQL = "select Code from " & table & " where CodeType='" & ddlHead.Text.Trim & "' and Code='" & code & "' "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            If dt.Rows.Count > 0 Then
                show_message.ShowMessage(Page, "Code is Exist,Please check it again!!!", UpdatePanel1)
                tbHeadCode.Focus()
                Exit Sub
            End If
        End If
        saveData(ddlHead.Text.Trim, code, name, remark)
        show_message.ShowMessage(Page, "Save Sub Completed!!", UpdatePanel1)
        clearSub()
        btSubSearch_Click(sender, e)

    End Sub

    Protected Sub btHeadSearch_Click(sender As Object, e As EventArgs) Handles btHeadSearch.Click
        Dim SQL As String,
            WHR As String

        WHR = Conn_SQL.Where("Code", tbHeadCode)
        WHR &= Conn_SQL.Where("Name", tbHeadName)

        SQL = "select Code A,Name B from " & table & " where CodeType='" & codeHead & "' " & WHR & " order by Code"
        ControlForm.ShowGridView(gvHead, SQL, Conn_SQL.MIS_ConnectionString)

    End Sub

    Protected Sub btSubSearch_Click(sender As Object, e As EventArgs) Handles btSubSearch.Click
        Dim SQL As String,
            WHR As String

        WHR = Conn_SQL.Where("Code", tbSubCode)
        WHR &= Conn_SQL.Where("Name", tbHeadName)
        WHR &= Conn_SQL.Where("CodeType", ddlHead)

        SQL = "select CodeType A,Code B,Name C,WC D from " & table & " where 1=1 " & WHR & " order by Code"
        ControlForm.ShowGridView(gvSub, SQL, Conn_SQL.MIS_ConnectionString)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollSub", "gridviewScrollSub();", True)

    End Sub

    Private Sub gvHead_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvHead.RowCommand
        Dim i As Integer = e.CommandArgument
        Select Case e.CommandName
            Case "onEdit"
                
                tbHeadCode.Text = gvHead.Rows(i).Cells(1).Text.Trim
                tbHeadCode.Enabled = False
                tbHeadName.Text = gvHead.Rows(i).Cells(2).Text.Trim
                btHeadDelete.Visible = True
        End Select
    End Sub

    Protected Sub btHeadDelete_Click(sender As Object, e As EventArgs) Handles btHeadDelete.Click
        Dim SQL As String = "delete from " & table & " where CodeType='0' and Code='" & tbHeadCode.Text.Trim & "' "
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
        show_message.ShowMessage(Page, "Delete Head Completed!!", UpdatePanel1)
    End Sub

    Protected Sub btSubDelete_Click(sender As Object, e As EventArgs) Handles btSubDelete.Click
        Dim SQL As String = "delete from " & table & " where CodeType='" & lbSubCodeType.Text.Trim & "' and Code='" & tbSubCode.Text.Trim & "' "
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
        show_message.ShowMessage(Page, "Delete sub Completed!!", UpdatePanel1)
    End Sub

    Private Sub gvSub_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSub.RowCommand
        Dim i As Integer = e.CommandArgument
        Select Case e.CommandName
            Case "onEdit"
                ddlHead.Text = gvSub.Rows(i).Cells(1).Text.Trim
                lbSubCodeType.Text = gvSub.Rows(i).Cells(1).Text.Trim
                tbSubCode.Text = gvSub.Rows(i).Cells(2).Text.Trim
                tbSubCode.Enabled = False
                tbSubName.Text = gvSub.Rows(i).Cells(3).Text.Trim
                tbSubRemark.Text = gvSub.Rows(i).Cells(4).Text.Trim
                btSubDelete.Visible = True
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollSub", "gridviewScrollSub();", True)
        End Select
    End Sub

    Protected Sub btPosReset_Click(sender As Object, e As EventArgs) Handles btPosReset.Click
        clearSub()
    End Sub

    Protected Sub btHeadReset_Click(sender As Object, e As EventArgs) Handles btHeadReset.Click
        clearHead()
    End Sub
End Class