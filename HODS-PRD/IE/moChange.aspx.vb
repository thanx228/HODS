Public Class moChange
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = "",
            status As String = ""
        If ddlStatus.Text <> "0" Then
            Dim conStr As String = ""
            If ddlStatus.Text = "2" Then 'close status
                conStr = "not"
            End If
            WHR = " and TA011 " & conStr & " in ('Y','y') "
        End If
        WHR = WHR & configDate.DateWhere("TO004", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
        If ddlChangeCode.Text = "0" Then 'body
            SQL = " select TP001+'-'+TP002+'-'+TP003 as MO,TP104 as 'Original Materials'," & _
                  " case when TP104<>TP004 then TP004 else '' end as 'New Materials'," & _
                  " cast(TP105 as decimal(10,2)) as 'Original Mat Req'," & _
                  " case when TP105<>TP005 then cast(TP005 as decimal(10,2)) else cast(0 as decimal(10,2)) end as 'New Mat Req'," & _
                  " TO044 as 'Change By'," & _
                  "(SUBSTRING(TO004,7,2)+'-'+SUBSTRING(TO004,5,2)+'-'+SUBSTRING(TO004,1,4)) as 'Change Date' " & _
                  " from MOCTP " & _
                  " left join MOCTO on TO001=TP001 and TO002=TP002 and TO003=TP003 " & _
                  " left join MOCTA on TA001=TP001 and TA002=TP002 " & _
                  " where (TP005<>TP105 or TP004<>TP104) and TO041='Y' " & WHR & _
                  " order by TO041,TO001,TO002,TO003 "
        Else 'head
            SQL = " select TO001+'-'+TO002+'-'+TO003 as MO," & _
                  " cast(TO117 as decimal(10,2)) as 'Original Plan Cap'," & _
                  " case when TO117<>TO017 then cast(TO017 as decimal(10,2)) else '' end as 'New Plan Cap'," & _
                  " TO044 as 'Change By'," & _
                  " (SUBSTRING(TO004,7,2)+'-'+SUBSTRING(TO004,5,2)+'-'+SUBSTRING(TO004,1,4)) as 'Change Date' " & _
                  " from MOCTO " & _
                  " left join MOCTA on TA001=TO001 and TA002=TO002 " & _
                  " where TO117<>TO017 and TO041='Y' " & WHR & _
                  " order by TO041,TO001,TO002,TO003 "

        End If
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("moChange" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class