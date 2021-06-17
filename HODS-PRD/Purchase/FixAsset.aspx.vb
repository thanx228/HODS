Public Class FixAsset
    Inherits System.Web.UI.Page

    Dim show_message As New show_message
    Dim Conn_sql As New ConnSQL
    Dim ControlForm As New ControlDataForm
    'Dim ExportUtil As New ExportUtil
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('CB') order by MQ002"
            ControlForm.showCheckboxList(cblType, SQL, "MQ002", "MQ001", 6, Conn_sql.ERP_ConnectionString)
        End If

    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        Dim SQL As String = "",
            WHR As String = ""

        If DDLCondition.SelectedValue = "Incomplete" Then
            WHR = WHR & " and TN012='N' "
        ElseIf DDLCondition.SelectedValue = "Complete" Then
            WHR = WHR & " and TN012 in ('Y','y') "
        End If
        '"TM003"
        WHR = WHR & Conn_sql.Where(ddlDate.Text, configDate.dateFormat2(FDate.Text.Trim), configDate.dateFormat2(TDate.Text.Trim))
        WHR &= Conn_sql.Where("TM002", Po)
        WHR = WHR & Conn_sql.Where("TN005", Spec)
        WHR = WHR & Conn_sql.Where("TN004", Asset)
        WHR = WHR & Conn_sql.Where("TM005", tbSup)
        WHR = WHR & Conn_sql.Where("TM001", cblType)

        SQL = " select TM001+'-'+rtrim(TM002)+'-'+TN003 as TM001," & _
              " rtrim(TM005)+'-'+MA002 as TM005,TM006,TN004," & _
              " TN005,TN007,TN006,TN011,TN006 - TN011 as BAL," & _
              " (SUBSTRING(TM003,7,2)+'-'+SUBSTRING(TM003,5,2)+'-'+SUBSTRING(TM003,1,4)) as TM003, " & _
              " (SUBSTRING(TN015,7,2)+'-'+SUBSTRING(TN015,5,2)+'-'+SUBSTRING(TN015,1,4)) as TN015, " & _
              "DATEDIFF(day, GETDATE(),TN015) AS TN015," & _
              " case TN012 when 'N' then 'N:Not Closed' when 'y' then 'y:Manual Closed' when 'Y' then 'Y:Auto Closed' else 'No Match' end as TN012 " & _
              " from ASTTM H left join ASTTN L on H.TM001 = L.TN001 and H.TM002 = L.TN002 " & _
              " left join PURMA on MA001=H.TM005 " & _
              " where 1=1 " & WHR & _
              " order by TM001,TM002,TM003 "

        ControlForm.ShowGridView(gvShow, SQL, Conn_sql.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("PO-Asset" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class