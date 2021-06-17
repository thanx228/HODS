Public Class CheckBOMPopupSub
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim whr As String = "", SQL As String = ""

            Dim item As String = Request.QueryString("item").ToString.Trim
            Dim tempTable As String = Request.QueryString("tempTable").ToString.Trim
            lbPart.Text = item

            SQL = "select * from " & tempTable & " where item='" & item & "'"
            Dim dt As DataTable
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            With dt.Rows(0)
                lbIssue.Text = Convert.ToDecimal(.Item("issueQty")).ToString("#,##0.00")
                lbDel.Text = Convert.ToDecimal(.Item("soQty")).ToString("#,##0.00")
                lbStock.Text = Convert.ToDecimal(.Item("stockQty")).ToString("#,##0.00")
                lbMO.Text = Convert.ToDecimal(.Item("moQty")).ToString("#,##0.00")
                lbPO.Text = Convert.ToDecimal(.Item("poQty")).ToString("#,##0.00")
                lbPR.Text = Convert.ToDecimal(.Item("prQty")).ToString("#,##0.00")
            End With
            lbSpec.Text = Request.QueryString("spec").ToString.Trim

            'Dim endDate As String = Request.QueryString("endDate").ToString.Trim
            'Sale order Detail
            SQL = " select COPTC.TC004 as CUST,COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO Detail', " & _
                  " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'Doc Date'," & _
                  " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Plan Del Date', " & _
                  " cast(COPTD.TD008 as decimal(8,0)) as 'SO Qty',cast(COPTD.TD009 as decimal(8,0)) as 'Delivery Qty'," & _
                  " cast(COPTD.TD008-COPTD.TD009 as decimal(8,0)) as 'Bal Qty',COPTD.TD010 as UNIT from COPTD  " & _
                  " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                  "  where COPTD.TD016='N' and COPTD.TD004 = '" & item & "' order by COPTD.TD001,COPTD.TD002,COPTD.TD003 "
            ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountSO.Text = ControlForm.rowGridview(gvSO)

            'Materials Request Issue
            SQL = " select COPTC.TC004 as 'Customer Code', MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO Detail',TA001+'-'+TA002 as MO," & _
                  " (SUBSTRING(TA009,7,2)+'-'+SUBSTRING(TA009,5,2)+'-'+SUBSTRING(TA009,1,4)) as 'MO Plan Start Date', " & _
                  " cast(isnull(MOCTA.TA015,0) as decimal(10,3)) as 'Plan Qty',  " & _
                  " (SUBSTRING(MOCTB.TB015,7,2)+'-'+SUBSTRING(MOCTB.TB015,5,2)+'-'+SUBSTRING(MOCTB.TB015,1,4)) as 'Plan Mat Issue Date', " & _
                  " cast(isnull(TB004,0) as decimal(10,3)) as 'Mat Req Qty',cast(isnull(TB005,0) as decimal(10,3)) as 'Issue Qty', " & _
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(10,3)) as 'issue Bal'," & _
                  " TA006 as 'JP PART',TA035 as 'JP SPEC' " & _
                  " from MOCTB  left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
                  " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " & _
                  " where TB003='" & item & "' " & _
                  " and TB004-TB005>0 and TA011 not in('y','Y')  and TA013='Y' " & _
                  " order by TA001,TA002 "
            ControlForm.ShowGridView(gvIssue, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountIssue.Text = ControlForm.rowGridview(gvIssue)

            'MO Detail
            SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO Detail'," & _
                  " MOCTA.TA001+'-'+MOCTA.TA002 as 'MO No'," & _
                  " (SUBSTRING(MOCTA.TA040,7,2)+'-'+SUBSTRING(MOCTA.TA040,5,2)+'-'+SUBSTRING(MOCTA.TA040,1,4)) as 'MO Date'," & _
                  " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) as 'Plan Complete Date'," & _
                  " cast(MOCTA.TA015 as decimal(10,3)) as 'MO Qty',cast(MOCTA.TA017 as decimal(10,3)) as 'Prd Qty',cast(MOCTA.TA015-MOCTA.TA017 as decimal(10,3)) as 'MO Bal' " & _
                  " from MOCTA where  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' and MOCTA.TA006='" & item & "' " & _
                  " order by MOCTA.TA001,MOCTA.TA002 "

            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = ControlForm.rowGridview(gvMO)
            'PR Detail
            SQL = " select TB001+'-'+TB002+'-'+TB003 as PR, " & _
                  " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'PR Date', " & _
                  " (SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4)) as 'PR Req Date', " & _
                  " cast(TR006 as decimal(8,2)) as 'PR Qty' ,TA005 as 'Plan Batch'," & _
                  " case when PURTA.TA007='Y' then 'Approved' else 'Not Approve' end as 'Status of PR' " & _
                  " from PURTR " & _
                  " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
                  " left join PURTA on TA001=TB001 and TA002=TB002  " & _
                  " where TR019='' and TB039='N' and TB004='" & item & "' " & _
                  " order by TB001,TB002,TB003 " 'and TA007 = 'Y'
            ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPR.Text = ControlForm.rowGridview(gvPR)

            'PO Detail
            SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as 'SO Detail'," & _
                 " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as 'PO Detail'," & _
                 " substring(PURTD.TD012,7,2)+'-'+substring(PURTD.TD012,5,2)+'-'+substring(PURTD.TD012,1,4) as 'Plan Delivery Date', " & _
                 " cast(PURTD.TD008 as decimal(16,2)) as 'PO Qty',cast(PURTD.TD015 as decimal(16,2)) as 'PO Del Qty', " & _
                 " cast(PURTD.TD008-PURTD.TD015 as decimal(16,2)) as 'PO Bal',rtrim(TC004)+'-'+MA002 as 'Supplier' , " & _
                 " PURTD.TD014 as 'Confirm Delivery Date' from PURTD  " & _
                 " left join PURTC on TC001=TD001 and TC002=TD002 " & _
                 " left join PURMA on MA001=TC004 " & _
                 " where  PURTD.TD016='N' and PURTD.TD004='" & item & "' " & _
                 " order by PURTD.TD001,PURTD.TD002,PURTD.TD003"
            ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPO.Text = ControlForm.rowGridview(gvPO)
        End If
    End Sub
End Class