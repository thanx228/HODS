Public Class planIssueMatPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then

            'Dim salType As String = Request.QueryString("saleType").ToString.Trim
            'Dim cust As String = Request.QueryString("custID").ToString.Trim
            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
            'Dim dFrom As String = Request.QueryString("dateFrom").ToString.Trim
            'Dim dTo As String = Request.QueryString("dateTo").ToString.Trim


            lbPart.Text = jpPart
            lbSpec.Text = Server.UrlDecode(Request.QueryString("JPSpec").ToString.Trim)
            'lbStock.Text = Request.QueryString("stock").ToString.Trim
            'lbNotDel.Text = Request.QueryString("issueQty").ToString.Trim
            Dim whr As String = "", SQL As String = ""

            'Materilas Request Issue
            SQL = " select TA001+'-'+TA002 as MO,(SUBSTRING(TA040,7,2)+'-'+SUBSTRING(TA040,5,2)+'-'+SUBSTRING(TA040,1,4)) as LotDate, " & _
                  " (SUBSTRING(TA010,7,2)+'-'+SUBSTRING(TA010,5,2)+'-'+SUBSTRING(TA010,1,4)) as DueDate, " & _
                  " cast(isnull(TB004,0) as decimal(15,3)) as MatReq,cast(isnull(TB005,0) as decimal(15,3)) as issue, " & _
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(15,3)) as issueBal," & _
                  " TA006 as JP_PART,TA035 as JP_SPEC " & _
                  " from MOCTB " & _
                  " left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
                  " where TB003='" & jpPart & "' and TB004-TB005>0 and TA011 not in('y','Y')  and MOCTA.TA013='Y' " & _
                  " order by TB003 "
            ControlForm.ShowGridView(gvIssue, SQL, Conn_SQL.ERP_ConnectionString)

            lbCountIssue.Text = ControlForm.rowGridview(gvIssue)

            'MO Detail
            SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as OrdDetail," & _
                  " MOCTA.TA001+'-'+MOCTA.TA002 as LotNo," & _
                  " (SUBSTRING(MOCTA.TA040,7,2)+'-'+SUBSTRING(MOCTA.TA040,5,2)+'-'+SUBSTRING(MOCTA.TA040,1,4)) as LotDate," & _
                  " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) as DueDate," & _
                  " cast(MOCTA.TA015 as decimal(15,0)) as LOT_Qty,cast(MOCTA.TA017 as decimal(15,0)) as Prd_Qty,cast(MOCTA.TA015-MOCTA.TA017 as decimal(15,0)) as LotBal" & _
                  " from MOCTA where  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' and MOCTA.TA006='" & jpPart & "' " & _
                  " order by MOCTA.TA001,MOCTA.TA002 "

            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = ControlForm.rowGridview(gvMO)
            'PO Detail
            SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as OrdDetail," & _
                 " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as PO_Detail," & _
                 " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as PlanDelDate," & _
                 " cast(PURTD.TD008 as decimal(15,3)) as PO_Qty,cast(PURTD.TD015 as decimal(15,3)) as PODelQty, " & _
                 " cast(PURTD.TD008-PURTD.TD015 as decimal(15,3)) as PO_Bal,PURTC.TC004 as 'Cust Code'  from PURTD " & _
                 " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " & _
                 " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' order by PURTD.TD001,PURTD.TD002"

            ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPO.Text = ControlForm.rowGridview(gvPO)

            'PR Detail
            'SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as OrdDetail," & _
            '     " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as PO_Detail," & _
            '     " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as PlanDelDate," & _
            '     " cast(PURTD.TD008 as decimal(8,0)) as PO_Qty,cast(PURTD.TD015 as decimal(8,0)) as PODelQty, " & _
            '     " cast(PURTD.TD008-PURTD.TD015 as decimal(8,0)) as PO_Bal  from PURTD " & _
            '     " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' order by PURTD.TD001,PURTD.TD002"

            SQL = " select TB001+'-'+TB002+'-'+TB003 as PR, " & _
                  " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as PrDate, " & _
                  " (SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4)) as PrReqDate, " & _
                  " cast(TR006 as decimal(15,3)) as prQty " & _
                  " from PURTR " & _
                  " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
                  " left join PURTA on TA001=TB001 and TA002=TB002  " & _
                  " where TR019='' and TB039='N'  and TB004='" & jpPart & "' order by TB004 "
            ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPR.Text = ControlForm.rowGridview(gvPR)

        End If
    End Sub
End Class