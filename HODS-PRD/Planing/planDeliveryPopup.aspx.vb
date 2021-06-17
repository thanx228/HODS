Public Class planDeliveryPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then

            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
            lbPart.Text = jpPart
            lbSpec.Text = Request.QueryString("JPSpec").ToString.Trim
            lbStock.Text = Request.QueryString("stock").ToString.Trim
            lbNotDel.Text = Request.QueryString("delQty").ToString.Trim
            Dim whr As String = ""

            'Sale order Detail
            Dim SQL As String = " select COPTC.TC004 as CUST,COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as OrdDetail, " & _
                                " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as DocDate," & _
                                " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as PlanDelDate, " & _
                                " cast(COPTD.TD008 as decimal(8,0)) as OrderQty,cast(COPTD.TD009 as decimal(8,0)) as DelQty," & _
                                " cast(COPTD.TD008-COPTD.TD009 as decimal(8,0)) as BAL,COPTD.TD010 as UNIT from COPTD  " & _
                                " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                                "  where COPTD.TD016='N' and COPTC.TC027='Y' and COPTD.TD004 = '" & jpPart & "' " & _
                                " order by COPTD.TD013"
            ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountSO.Text = gvSO.Rows.Count
            'MO Detail

            SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as OrdDetail," & _
                  " MOCTA.TA001+'-'+MOCTA.TA002 as LotNo," & _
                  " (SUBSTRING(MOCTA.TA004,7,2)+'-'+SUBSTRING(MOCTA.TA004,5,2)+'-'+SUBSTRING(MOCTA.TA004,1,4)) as LotDate," & _
                  " cast(MOCTA.TA015 as decimal(8,0)) as LOT_Qty,cast(MOCTA.TA017 as decimal(8,0)) as Prd_Qty,cast(MOCTA.TA015-MOCTA.TA017 as decimal(8,0)) as LotBal" & _
                  " from MOCTA " & _
                  " where  MOCTA.TA011 not in('y','Y') and MOCTA.TA006='" & jpPart & "' " & _
                  " order by MOCTA.TA001,MOCTA.TA002 "

            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = gvMO.Rows.Count
            'PO Detail

            SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as OrdDetail," & _
                 " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as PO_Detail," & _
                 " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as PlanDelDate," & _
                 " cast(PURTD.TD008 as decimal(8,0)) as PO_Qty,cast(PURTD.TD015 as decimal(8,0)) as PODelQty, " & _
                 " cast(PURTD.TD008-PURTD.TD015 as decimal(8,0)) as PO_Bal  from PURTD " & _
                 " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' order by PURTD.TD001,PURTD.TD002"

            ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPO.Text = gvPO.Rows.Count
            'MO ISSUE
            SQL = " select TA001+'-'+TA002 as MO,cast(isnull(TB004,0) as decimal(8,0)) as MatReq,cast(isnull(TB005,0) as decimal(8,0)) as issue, " & _
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(8,0)) as issueBal from MOCTB " & _
                  " left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
                  " where TB003='" & jpPart & "' and TB004-TB005>0 and TA011 not in('y','Y')  " & _
                  " order by TB003 "
            ControlForm.ShowGridView(gvIssue, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountIssue.Text = gvIssue.Rows.Count

            'PR
            SQL = " select B.TB001+'-'+B.TB002+'-'+B.TB003 as PR," & _
                " (SUBSTRING(B.TB011,7,2)+'-'+SUBSTRING(B.TB011,5,2)+'-'+SUBSTRING(B.TB011,1,4)) as RequireDate," & _
                " B.TB029+'-'+B.TB030+'-'+B.TB031 as SO, " & _
                " cast(B.TB009 as decimal(8,0)) as prQty from PURTR R " & _
                " left join PURTB B on B.TB001=R.TR001 and B.TB002=R.TR002 and B.TB003=R.TR003 " & _
                " where R.TR019='' and B.TB039='N' and B.TB004='" & jpPart & "' " & _
                " order by B.TB001,B.TB002,B.TB003"
            ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPR.Text = gvPR.Rows.Count
        End If
    End Sub
End Class