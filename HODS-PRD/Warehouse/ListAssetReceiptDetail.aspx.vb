Public Class ListAssetReceiptDetail
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        Dim whr As String = "",
            SQL As String = ""
        whr = whr & Conn_SQL.Where("TO013", cblApp)
        whr = whr & Conn_SQL.Where("TO005", tbVendor, False)
        whr = whr & Conn_SQL.Where("TP005", tbItem)
        whr = whr & Conn_SQL.Where("TP006", tbSpec)
        whr = whr & configDate.DateWhere("TO003", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))

        SQL = " select substring (TO003 ,7,2)+'-'+substring (TO003,5,2)+'-'+ substring (TO003,1,4) 'Date', " & _
            " TP001 +'-'+TP002 +'-'+TP003 'Asset Receipt', " & _
            " TP005 'Spec Desc', TP006 'Spec Detail', CAST(TP007 as decimal (18,2)) 'Receipt Qty', " & _
            " cast (TP013 as decimal(18,2)) 'Accept Qty', cast(TP015 as decimal(18,2)) as 'Insp. Return Qty', " & _
            " TP008 'Unit', TP009 as 'Asset PO Type', TP010 as 'Asset PO No.', TP011 as 'Asset PO Seq', " & _
            " RTRIM(TO005) +'-'+ (MA002) as 'Vendor', TO010 as 'Invoice No.',TO013 as 'App Status' from ASTTP " & _
            " left join ASTTO on TO001 = TP001 and TO002 = TP002 " & _
            " left join PURMA on MA001 = TO005 where 1=1 " & whr & _
            " order by TO005,TP001,TP002,TP003 "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        System.Threading.Thread.Sleep(1000)

    End Sub
    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("ListAssetReceiptDetail" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class