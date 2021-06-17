Public Class MaterailsShortagePopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

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
            lbUnit.Text = Request.QueryString("Unit").ToString.Trim
            Dim whr As String = "", SQL As String = ""
            Dim endDate As String = Request.QueryString("endDate").ToString.Trim
            'Sale order Detail
            SQL = " select COPTC.TC004 as CUST,COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO Detail', " & _
                  " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'Doc Date'," & _
                  " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Plan Del Date', " & _
                  " cast(COPTD.TD008 as decimal(8,0)) as 'SO Qty',cast(COPTD.TD009 as decimal(8,0)) as 'Delivery Qty'," & _
                  " cast(COPTD.TD008-COPTD.TD009 as decimal(8,0)) as 'Bal Qty',COPTD.TD010 as UNIT from COPTD  " & _
                  " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                  "  where COPTD.TD016='N' and COPTD.TD004 = '" & jpPart & "' order by COPTD.TD001,COPTD.TD002,COPTD.TD003 "
            ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountSO.Text = ControlForm.rowGridview(gvSO)

            'Materials Request Issue
            SQL = " select TA001+'-'+TA002 as MO,(SUBSTRING(TA040,7,2)+'-'+SUBSTRING(TA040,5,2)+'-'+SUBSTRING(TA040,1,4)) as 'MO Date', " & _
                  " (SUBSTRING(TB015,7,2)+'-'+SUBSTRING(TB015,5,2)+'-'+SUBSTRING(TB015,1,4)) as 'Plan Issue Date', " & _
                  " cast(isnull(TB004,0) as decimal(10,3)) as 'Mat Req Qty',cast(isnull(TB005,0) as decimal(10,3)) as 'Issue Qty', " & _
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(10,3)) as 'issue Bal'," & _
                  " TA006 as 'JP PART',TA035 as 'JP SPEC' " & _
                  " from MOCTB  left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
                  " where TB003='" & jpPart & "' " & configDate.DateWhere("TA003", "", endDate) & _
                  " and TB004-TB005>0 and TA011 not in('y','Y')  and TA013='Y' " & _
                  " order by TB015,TA001,TA002 "
            ControlForm.ShowGridView(gvIssue, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountIssue.Text = ControlForm.rowGridview(gvIssue)

            'MO Detail
            SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO Detail'," & _
                  " MOCTA.TA001+'-'+MOCTA.TA002 as 'MO No'," & _
                  " (SUBSTRING(MOCTA.TA040,7,2)+'-'+SUBSTRING(MOCTA.TA040,5,2)+'-'+SUBSTRING(MOCTA.TA040,1,4)) as 'MO Date'," & _
                  " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) as 'Plan Complete Date'," & _
                  " cast(MOCTA.TA015 as decimal(10,3)) as 'MO Qty',cast(MOCTA.TA017 as decimal(10,3)) as 'Prd Qty',cast(MOCTA.TA015-MOCTA.TA017 as decimal(10,3)) as 'MO Bal' " & _
                  " from MOCTA where  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' and MOCTA.TA006='" & jpPart & "' " & _
                  " " & configDate.DateWhere("TA010", "", endDate) & "order by MOCTA.TA010,MOCTA.TA001,MOCTA.TA002 "

            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = ControlForm.rowGridview(gvMO)
            'PR Detail
            SQL = " select TB001+'-'+TB002+'-'+TB003 as PR, " & _
                  " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'PR Date', " & _
                  " (SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4)) as 'PR Req Date', " & _
                  " cast(TR006 as decimal(8,2)) as 'PR Qty' ,TA005 as 'Plan Batch'" & _
                  " from PURTR " & _
                  " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
                  " left join PURTA on TA001=TB001 and TA002=TB002  " & _
                  " where TR019='' and TB039='N' and TA007 = 'Y' and TB004='" & jpPart & "' " & _
                  " order by TB001,TB002,TB003 "
            ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPR.Text = ControlForm.rowGridview(gvPR)

            'PO Detail
            SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as 'SO Detail'," & _
                 " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as 'PO Detail'," & _
                 " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as 'Plan Del Date'," & _
                 " cast(PURTD.TD008 as decimal(8,0)) as 'PO Qty',cast(PURTD.TD015 as decimal(8,0)) as 'PO Del Qty', " & _
                 " cast(PURTD.TD008-PURTD.TD015 as decimal(8,0)) as 'PO Bal',TC004 as 'Supplier',MA002 as 'Supplier Name'  from PURTD " & _
                 " left join PURTC on TC001=TD001 and TC002=TD002 " & _
                 " left join PURMA on MA001=TC004 " & _
                 " where PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' " & _
                 " order by PURTD.TD012,PURTD.TD001,PURTD.TD002,PURTD.TD003"
            ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPO.Text = ControlForm.rowGridview(gvPO)

            'Purchase Receipt not aprove
            SQL = " select PURTH.TH011+'-'+PURTH.TH012+'-'+PURTH.TH013 as 'PO Detail', " & _
                  " PURTH.TH001+'-'+PURTH.TH002+'-'+PURTH.TH003 as 'PO Receipt',PURTG.TG005 as 'Supplier', " & _
                  " (SUBSTRING(PURTG.TG003,7,2)+'-'+SUBSTRING(PURTG.TG003,5,2)+'-'+SUBSTRING(PURTG.TG003,1,4)) as 'Receipt Date'," & _
                  " (SUBSTRING(PURTH.TH014,7,2)+'-'+SUBSTRING(PURTH.TH014,5,2)+'-'+SUBSTRING(PURTH.TH014,1,4)) as 'Inspection Date'," & _
                  " cast(PURTH.TH015 as decimal(8,0)) as 'PO Receipt Qty' " & _
                  " from PURTH " & _
                  " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " & _
                  " where PURTG.TG013 = 'N' and PURTH.TH015>0 and  PURTH.TH004='" & jpPart & "' " & configDate.DateWhere("PURTG.TG014", "", endDate) & _
                  " order by PURTH.TH004 "
            ControlForm.ShowGridView(gvPoInsp, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPoInsp.Text = ControlForm.rowGridview(gvPoInsp)
        End If
    End Sub
End Class