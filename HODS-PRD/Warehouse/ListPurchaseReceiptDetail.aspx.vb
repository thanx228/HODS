Public Class ListPurchaseReceiptDetail
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

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        Dim whr As String = "",
            SQL As String = ""

        whr = whr & Conn_SQL.Where("PURTG.TG013", cblApp)
        whr = whr & Conn_SQL.Where("PURTG.TG005", tbVendor, False)
        whr = whr & Conn_SQL.Where("PURTH.TH004", tbItem)
        whr = whr & Conn_SQL.Where("PURTH.TH006", tbSpec)
        whr = whr & configDate.DateWhere("PURTG.TG003", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))

        SQL = " select substring (PURTG.TG003 ,7,2)+'-'+substring (PURTG.TG003,5,2)+'-'+ substring (PURTG.TG003,1,4) as 'Date', " & _
            " PURTH.TH001 +'-'+PURTH.TH002 +'-'+PURTH.TH003  as 'Purchase Receipt', " & _
            " case when len(PURTH.TH004)=16 then (SUBSTRING(PURTH.TH004,1,14)+'-'+SUBSTRING(PURTH.TH004,15,2)) else PURTH.TH004 end as 'Item', " & _
            " PURTH.TH005 as 'Item Desc', PURTH.TH006 as 'Spec', CAST(PURTH.TH007 as decimal (18,2)) as 'Receipt Qty', " & _
            " cast (PURTH.TH015 as decimal(18,2)) as 'Accept Qty', cast(PURTH.TH017 as decimal(18,2)) as 'Insp. Return Qty', " & _
            " PURTH.TH008 as 'Unit', PURTH.TH011 as 'PO Type', PURTH.TH012 as 'PO No.', PURTH.TH013 as 'PO Seq', " & _
            " RTRIM(PURTG.TG005) +'-'+ (PURMA.MA002 ) as 'Vendor', PURTG.TG011 as 'Invoice No.',PURTG.TG013 as 'App Status' from PURTG " & _
            " left join PURTH on PURTH.TH001 = PURTG.TG001 and PURTH.TH002 = PURTG.TG002 " & _
            " left join PURMA on PURMA.MA001 = PURTG.TG005 where 1=1 " & whr & _
            " order by PURTG.TG003,PURTH.TH001,PURTH.TH002,PURTH.TH003 "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        System.Threading.Thread.Sleep(1000)

    End Sub
    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("ListPurReceiptDetail" & Session("UserName"), gvShow)
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