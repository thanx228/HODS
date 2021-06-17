Public Class checkCodeStatusPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

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
            lbPR.Text = Request.QueryString("prQty").ToString.Trim
            lbSupport.Text = Request.QueryString("supportQty").ToString.Trim
            Dim whr As String = "", SQL As String = ""

            'Sale order Detail
            SQL = " select COPTC.TC004 as CUST,COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as OrdDetail, " & _
                  " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as DocDate," & _
                  " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as PlanDelDate, " & _
                  " cast(COPTD.TD008 as decimal(8,0)) as OrderQty,cast(COPTD.TD009 as decimal(8,0)) as DelQty," & _
                  " cast(COPTD.TD008-COPTD.TD009 as decimal(8,0)) as BAL,COPTD.TD010 as UNIT from COPTD  " & _
                  " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                  "  where COPTD.TD016='N' and COPTD.TD004 = '" & jpPart & "' order by COPTD.TD001,COPTD.TD002,COPTD.TD003 "
            ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountSO.Text = gvSO.Rows.Count

            'Materilas Request Issue
            SQL = " select TA001+'-'+TA002 as MO,(SUBSTRING(TA040,7,2)+'-'+SUBSTRING(TA040,5,2)+'-'+SUBSTRING(TA040,1,4)) as LotDate, " & _
                  " (SUBSTRING(TA010,7,2)+'-'+SUBSTRING(TA010,5,2)+'-'+SUBSTRING(TA010,1,4)) as DueDate, " & _
                  " cast(isnull(TB004,0) as decimal(10,3)) as MatReq,cast(isnull(TB005,0) as decimal(10,3)) as issue, " & _
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(10,3)) as issueBal," & _
                  " TA006 as JP_PART,TA035 as JP_SPEC " & _
                  " from MOCTB " & _
                  " left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
                  " where TB003='" & jpPart & "' and TB004-TB005>0 and TA011 not in('y','Y')  and TA013='Y' " & _
                  " order by TA001,TA002 "
            ControlForm.ShowGridView(gvIssue, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountIssue.Text = gvIssue.Rows.Count

            'MO Detail
            SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as OrdDetail," & _
                  " MOCTA.TA001+'-'+MOCTA.TA002 as LotNo," & _
                  " (SUBSTRING(MOCTA.TA040,7,2)+'-'+SUBSTRING(MOCTA.TA040,5,2)+'-'+SUBSTRING(MOCTA.TA040,1,4)) as LotDate," & _
                  " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) as DueDate," & _
                  " cast(MOCTA.TA015 as decimal(10,3)) as LOT_Qty,cast(MOCTA.TA017 as decimal(10,3)) as Prd_Qty,cast(MOCTA.TA015-MOCTA.TA017 as decimal(10,3)) as LotBal" & _
                  " from MOCTA where  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' and MOCTA.TA006='" & jpPart & "' " & _
                  " order by MOCTA.TA001,MOCTA.TA002 "

            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = gvMO.Rows.Count
            'PO Detail
            SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as OrdDetail," & _
                 " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as PO_Detail," & _
                 " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as PlanDelDate," & _
                 " cast(PURTD.TD008 as decimal(8,0)) as PO_Qty,cast(PURTD.TD015 as decimal(8,0)) as PODelQty, " & _
                 " cast(PURTD.TD008-PURTD.TD015 as decimal(8,0)) as PO_Bal  from PURTD " & _
                 " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' order by PURTD.TD001,PURTD.TD002,PURTD.TD003"

            ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPO.Text = gvPO.Rows.Count

            'PR Detail
            SQL = " select TB001+'-'+TB002+'-'+TB003 as PR, " & _
                  " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as PrDate, " & _
                  " (SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4)) as PrReqDate, " & _
                  " cast(TR006 as decimal(8,2)) as prQty " & _
                  " from PURTR " & _
                  " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
                  " left join PURTA on TA001=TB001 and TA002=TB002  " & _
                  " where TR019='' and TB039='N'  and TB004='" & jpPart & "' order by TB001,TB002,TB003 "
            ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPR.Text = gvPR.Rows.Count

        End If
    End Sub
End Class