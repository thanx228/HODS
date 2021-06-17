Public Class LockToMO
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLock.Click
        Dim srcType As String = tbSrcType.Text.Trim,
            srcNo As String = tbSrcNo.Text.Trim,
            srcSeq As String = tbSrcSeq.Text.Trim


        If srcType = "" Then
            show_message.ShowMessage(Page, "Src Type is Null", UpdatePanel1)
            tbSrcType.Focus()
            Exit Sub
        End If
        If srcNo = "" Then
            show_message.ShowMessage(Page, "Src No is null!!", UpdatePanel1)
            tbSrcNo.Focus()
            Exit Sub
        End If
        If srcSeq = "" Then
            show_message.ShowMessage(Page, "Src Seq is null!!", UpdatePanel1)
            tbSrcSeq.Focus()
            Exit Sub
        End If
        Dim WHR As String = " where TA023='" & srcType & "' and TA024='" & srcNo & "' and TA025='" & srcSeq & "' and TA009='N' "
        Dim SQL As String = "select * from LRPTA " & WHR
        Dim dt As DataTable
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        If dt.Rows.Count = 0 Then
            show_message.ShowMessage(Page, "Data not found, Please check data agian !!", UpdatePanel1)
            tbSrcType.Focus()
            Exit Sub
        End If

        Dim USQL As String = " update LRPTA set TA009='Y' " & WHR
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
        show_message.ShowMessage(Page, "Update Data Complete Please check again in ERP System", UpdatePanel1)
    End Sub
End Class