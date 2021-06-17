Imports MIS_HTI.FormControl
Imports MIS_HTI.DataControl

Public Class MaterailsCallInPopup
    Inherits System.Web.UI.Page
    Dim configDate As New ConfigDate
    Dim gvCont As New GridviewControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then

            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
            lbPart.Text = jpPart
            lbSpec.Text = Request.QueryString("JPSpec").ToString.Trim
            lbIssue.Text = Request.QueryString("issueQty").ToString.Trim
            lbDel.Text = Request.QueryString("delQty").ToString.Trim
            lbStock.Text = Request.QueryString("stock").ToString.Trim
            lbMO.Text = Request.QueryString("moQty").ToString.Trim
            lbPO.Text = Request.QueryString("poQty").ToString.Trim
            lbPOFor.Text = Request.QueryString("poForQty").ToString.Trim
            lbPOMan.Text = Request.QueryString("poManQty").ToString.Trim
            lbPOMO.Text = Request.QueryString("poMoQty").ToString.Trim
            lbPR.Text = Request.QueryString("prQty").ToString.Trim
            lbPoInsp.Text = Request.QueryString("poRcpQty").ToString.Trim

            Dim whr As String = "", SQL As String = ""
            Dim endDate As String = Request.QueryString("endDate").ToString.Trim
            'Sale order Detail
            SQL = " select COPTC.TC004 as CUST,COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO Detail', " &
                  " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'Doc Date'," &
                  " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Plan Del Date', " &
                  " cast(COPTD.TD008 as decimal(8,0)) as 'SO Qty',cast(COPTD.TD009 as decimal(8,0)) as 'Delivery Qty'," &
                  " cast(COPTD.TD008-COPTD.TD009 as decimal(8,0)) as 'Bal Qty',COPTD.TD010 as UNIT from COPTD  " &
                  " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " &
                  "  where COPTD.TD016='N' and COPTD.TD004 = '" & jpPart & "' order by COPTD.TD001,COPTD.TD002,COPTD.TD003 "
            gvCont.ShowGridView(gvSO, SQL, VarIni.ERP)
            lbCountSO.Text = gvCont.rowGridview(gvSO)

            'Materials Request Issue
            SQL = " select COPTC.TC004 as 'Customer Code', MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO Detail',TA001+'-'+TA002 as MO," &
                  " (SUBSTRING(TA009,7,2)+'-'+SUBSTRING(TA009,5,2)+'-'+SUBSTRING(TA009,1,4)) as 'MO Plan Start Date', " &
                  " cast(isnull(MOCTA.TA015,0) as decimal(10,3)) as 'Plan Qty',  " &
                  " (SUBSTRING(MOCTB.TB015,7,2)+'-'+SUBSTRING(MOCTB.TB015,5,2)+'-'+SUBSTRING(MOCTB.TB015,1,4)) as 'Plan Mat Issue Date', " &
                  " cast(isnull(TB004,0) as decimal(10,3)) as 'Mat Req Qty',cast(isnull(TB005,0) as decimal(10,3)) as 'Issue Qty', " &
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(10,3)) as 'issue Bal'," &
                  " TA006 as 'JP PART',TA035 as 'JP SPEC' " &
                  " from MOCTB  left join MOCTA on TA001=TB001 and  TA002=TB002 " &
                  " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " &
                  " where TB003='" & jpPart & "' " & configDate.DateWhere("TA009", "", endDate) &
                  " and TB004-TB005>0 and TA011 not in('y','Y')  and TA013='Y' " &
                  " order by TA001,TA002 "
            gvCont.ShowGridView(gvIssue, SQL, VarIni.ERP)
            lbCountIssue.Text = gvCont.rowGridview(gvIssue)

            'MO Detail
            SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO Detail'," &
                  " MOCTA.TA001+'-'+MOCTA.TA002 as 'MO No'," &
                  " (SUBSTRING(MOCTA.TA040,7,2)+'-'+SUBSTRING(MOCTA.TA040,5,2)+'-'+SUBSTRING(MOCTA.TA040,1,4)) as 'MO Date'," &
                  " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) as 'Plan Complete Date'," &
                  " cast(MOCTA.TA015 as decimal(10,3)) as 'MO Qty',cast(MOCTA.TA017 as decimal(10,3)) as 'Prd Qty', " &
                  " cast(MOCTA.TA018 as decimal(10,3)) as 'Scrap Qty'," &
                  " cast(isnull(MOCTA.TA015,0)-isnull(MOCTA.TA017,0)-isnull(MOCTA.TA018,0) as decimal(10,3)) as 'MO Bal' " &
                  " from MOCTA where  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' and MOCTA.TA006='" & jpPart & "' " &
                  " order by MOCTA.TA001,MOCTA.TA002 "
            gvCont.ShowGridView(gvMO, SQL, VarIni.ERP)
            lbCountMO.Text = gvCont.rowGridview(gvMO)
            'PR Detail
            SQL = " select TB001+'-'+TB002+'-'+TB003 as PR, " &
                  " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'PR Date', " &
                  " (SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4)) as 'PR Req Date', " &
                  " cast(TR006 as decimal(8,2)) as 'PR Qty' ,TA005 as 'Plan Batch'," &
                  " case when PURTA.TA007='Y' then 'Approved' else 'Not Approve' end as 'Status of PR' " &
                  " from PURTR " &
                  " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " &
                  " left join PURTA on TA001=TB001 and TA002=TB002  " &
                  " where TR019='' and TB039='N' and TB004='" & jpPart & "' " &
                  " order by TB001,TB002,TB003 " 'and TA007 = 'Y'
            gvCont.ShowGridView(gvPR, SQL, VarIni.ERP)
            lbCountPR.Text = gvCont.rowGridview(gvPR)

            'PO Detail
            SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as 'SO Detail'," &
                 " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as 'PO Detail'," &
                 " PURTD.UDF01 as 'Plan Del Date'," &
                 " cast(PURTD.TD008 as decimal(8,0)) as 'PO Qty',cast(PURTD.TD015 as decimal(8,0)) as 'PO Del Qty', " &
                 " cast(PURTD.TD008-PURTD.TD015 as decimal(8,0)) as 'PO Bal',TC004+'-'+MA002 as 'Supplier' , " &
                 " substring(PURTD.TD012,7,2)+'-'+substring(PURTD.TD012,5,2)+'-'+substring(PURTD.TD012,1,4) as 'Confirm Delivery Date' from PURTD " &
                 " left join PURTC on TC001=TD001 and TC002=TD002 " &
                 " left join PURMA on MA001=TC004 " &
                 " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' " &
                 " order by PURTD.TD001,PURTD.TD002,PURTD.TD003"
            gvCont.ShowGridView(gvPO, SQL, VarIni.ERP)
            lbCountPO.Text = gvCont.rowGridview(gvPO)

            'Purchase Receipt not aprove
            SQL = " select PURTH.TH011+'-'+PURTH.TH012+'-'+PURTH.TH013 as 'PO Detail', " &
                  " PURTH.TH001+'-'+PURTH.TH002+'-'+PURTH.TH003 as 'PO Receipt',PURTG.TG005 as 'Supplier', " &
                  " (SUBSTRING(PURTG.TG003,7,2)+'-'+SUBSTRING(PURTG.TG003,5,2)+'-'+SUBSTRING(PURTG.TG003,1,4)) as 'Receipt Date'," &
                  " (SUBSTRING(PURTH.TH014,7,2)+'-'+SUBSTRING(PURTH.TH014,5,2)+'-'+SUBSTRING(PURTH.TH014,1,4)) as 'Inspection Date'," &
                  " cast(PURTH.TH007 as decimal(8,0)) as 'PO Receipt Qty' " &
                  " from PURTH " &
                  " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " &
                  " where PURTG.TG013 = 'N' and PURTH.TH007>0 and  PURTH.TH004='" & jpPart & "' " &
                  " order by PURTH.TH004 "
            gvCont.ShowGridView(gvPoInsp, SQL, VarIni.ERP)
            lbCountPoInsp.Text = gvCont.rowGridview(gvPoInsp)
        End If
    End Sub
End Class