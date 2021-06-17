Public Class OrderToCashPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim saleType As String = Request.QueryString("saleType").ToString.Trim,
               saleNo As String = Request.QueryString("saleNo").ToString.Trim,
               saleSeq As String = Request.QueryString("saleSeq").ToString.Trim,
               SQL As String = ""

            'SO
            SQL = " select TC004+'-'+MA002 as 'Customer',TD001+'-'+TD002+'-'+TD003 as 'Sale Number', " & _
                  " TD004 as 'Item',TD006 as 'Spec', " & _
                  "(SUBSTRING(TD013,7,2)+'-'+SUBSTRING(TD013,5,2)+'-'+SUBSTRING(TD013,1,4)) as 'Request Delivery Date'," & _
                  " CONVERT(VARCHAR,cast(TD008 as money),1) as 'Quantity'," & _
                  " CONVERT(VARCHAR,cast(TD009 as money),1) as 'Delivered Quantity', " & _
                  " case TD016 when 'Y' then 'Auto Closed' when 'y' then 'Manual Closed' else 'Not Close' end as 'SO status'" & _
                  " from COPTD left join COPTC on TC001=TD001 and TC002=TD002 " & _
                  " left join COPMA on MA001=TC004 " & _
                  " where TD001='" & saleType & "' and TD002='" & saleNo & "' " & _
                  "   and TD003='" & saleSeq & "' order by TD001,TD002,TD003 "
            ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountSO.Text = ControlForm.rowGridview(gvSO)

            'MO
            SQL = " select TA001+'-'+TA002+'-'+TA003 as 'MO', TA006 as 'Item',TA035 as 'Spec', " & _
                  "(SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'Order Date'," & _
                  "(SUBSTRING(TA010,7,2)+'-'+SUBSTRING(TA010,5,2)+'-'+SUBSTRING(TA010,1,4)) as 'Plan Finish Date'," & _
                  "(SUBSTRING(TA014,7,2)+'-'+SUBSTRING(TA014,5,2)+'-'+SUBSTRING(TA014,1,4)) as 'Actual Finish Date'," & _
                  "(SUBSTRING(TA014,7,2)+'-'+SUBSTRING(TA014,5,2)+'-'+SUBSTRING(TA014,1,4)) as 'Actual Finish Date'," & _
                  " CONVERT(VARCHAR,cast(TA015 as money),1) as 'Plan Quantity'," & _
                  " CONVERT(VARCHAR,cast(TA017 as money),1) as 'Completed Quantity'," & _
                  " CONVERT(VARCHAR,cast(TA018 as money),1) as 'Scrap Quantity', " & _
                  " CONVERT(VARCHAR,cast(TA060 as money),1) as 'Destroy Quantity', " & _
                  " case TA011 when '1' then 'Have not Manufactured' " & _
                  "            when '2' then 'Materials Not Issue' " & _
                  "            when '3' then 'Manufacturing' " & _
                  "            when 'Y' then 'Finished' " & _
                  "                     else 'Manual Finished' end as 'MO Status' " & _
                  " from MOCTA where TA026='" & saleType & "' and TA027='" & saleNo & "' " & _
                  "  and TA028='" & saleSeq & "' and TA013 = 'Y' order by TA003,TA001,TA002 "
            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = ControlForm.rowGridview(gvMO)

            'Invoice
            SQL = " select TH001+'-'+TH002+'-'+TH003 as 'Sale Del No', " & _
                  "(SUBSTRING(TG042,7,2)+'-'+SUBSTRING(TG042,5,2)+'-'+SUBSTRING(TG042,1,4)) as 'Delivery Date'," & _
                  " CONVERT(VARCHAR,cast(TH008 as money),1) as 'Delivery Quantity'," & _
                  " TB001+'-'+TB002+'-'+TB003 as 'Invoice No'," & _
                  "(SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'Invoice Date'," & _
                  " CONVERT(VARCHAR,cast(TB022 as money),1) as 'Invoice Quantity'," & _
                  " case TA100 when '1' then 'Not' when '2' then 'Partial' else 'All' end as 'Write-off Status' ," & _
                  " (SUBSTRING(TA020,7,2)+'-'+SUBSTRING(TA020,5,2)+'-'+SUBSTRING(TA020,1,4)) as 'Plan Receive Date', " & _
                  " TL001+'-'+TL002+'-'+TL003 as 'Receipt No'," & _
                  " (SUBSTRING(TK003,7,2)+'-'+SUBSTRING(TK003,5,2)+'-'+SUBSTRING(TK003,1,4)) as 'Actual Receive Date' " & _
                  " from COPTH left join COPTG on TG001=TH001 and TG002=TH002 " & _
                  " left join ACRTB on TB004='1' and TB005=TH001 and TB006=TH002 and TB007=TH003 and TB012='Y' " & _
                  " left join ACRTA on TA001=TB001 and TA002=TB002 " & _
                  " left join ACRTL on TL004='1' and TL005=TB001 and TL006=TB002 " & _
                  " left join ACRTK on TK001=TL001 and TK002=TL002 " & _
                  " where TH014='" & saleType & "' and TH015='" & saleNo & "' and TH016='" & saleSeq & "' and TH020='Y' " & _
                  " order by TG042,TH001,TH002,TH003 "
            ControlForm.ShowGridView(gvInv, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountInv.Text = ControlForm.rowGridview(gvInv)
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        Dim wc As String = Request.QueryString("wc").ToString.Trim
        ControlForm.ExportGridViewToExcel("OperationReport" & wc & Session("UserName"), gvInv)
    End Sub

    Private Sub gvInv_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvInv.DataBound
        ControlForm.MergeGridview(gvInv, 8)
    End Sub
End Class