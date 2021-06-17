Public Class PrintCheqCash
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = ""
        Dim WHR As String = ""
        If tbCheq.Text <> "" Then
            WHR = WHR & " and TB010 like '" & tbCheq.Text.Trim & "%' "
        Else
            WHR = WHR & configDate.DateWhere("TA003", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text), True)
        End If

        SQL = " select TB001 as 'Vocher Type',TB002 as 'Vocher No.',TB010 as 'Cheq',ACTTA.UDF01 as PAY," & _
              " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'Vocher Date'," & _
              " cast(TB015 as decimal(15,2))  as 'Cheq Amout' " & _
              " from ACTTB left join ACTTA on TA001=TB001 and TA002=TB002 " & _
              " where TB001='9110' and TB005='2120-6000' " & WHR & _
              " order by TB002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim paraName As String = "PayNo:" & e.Row.Cells(2).Text.ToString.Trim & ",PayType:" & e.Row.Cells(1).Text.ToString.Trim
            e.Row.Cells(0).Attributes("onclick") = "javascript:NewWindow('../ShowCrystalReport.aspx?dbName=ERP&ReportName=ChequeCash.rpt&paraName=" & paraName & "','Cheque','950','500')"
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class