Public Class AttendanceReportPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim wc As String = Request.QueryString("wc").ToString.Trim
            Dim dateFrom As String = Request.QueryString("dateFrom").ToString.Trim
            Dim dateTo As String = Request.QueryString("dateTo").ToString.Trim
            Dim fld As String = returnFld("TE012", "'Hours/Day'")
            fld = fld & returnFld("TE011*TE012", "'Sum Hours/Day'")
            Dim SQL As String = " select TD004 as 'WC',MD002 as 'WC Name'," & _
                                " (SUBSTRING(TD008,7,2)+'-'+SUBSTRING(TD008,5,2)+'-'+SUBSTRING(TD008,1,4)) as 'Doc Date'," & _
                                " TE004 as 'Staff Code',TE015 as 'Remark'," & _
                                " cast(TE011 as decimal(5,0)) as 'Number of Operator'" & fld & _
                                " from SFCTE left join SFCTD on TD001=TE001 and TD002=TE002 " & _
                                " left join CMSMD on MD001=TD004" & _
                                " where TD005='Y' and TE012 in ('30600','28800','0') and TD004='" & wc & "' " & Conn_SQL.Where("TD008", dateFrom, dateTo, True) & _
                                " order by TD008 "
            ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
            lbCount.Text = gvShow.Rows.Count
        End If
    End Sub
    Function returnFld(fldName As String, fldCall As String) As String
        Return ",CONVERT(varchar, floor(" & fldName & "/3600)) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),floor((" & fldName & " % 3600) / 60))), 2) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),(" & fldName & " % 60))), 2) as " & fldCall
    End Function
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        Dim wc As String = Request.QueryString("wc").ToString.Trim
        ControlForm.ExportGridViewToExcel("OperationReport" & wc & Session("UserName"), gvShow)
    End Sub
End Class