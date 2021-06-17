Public Class MaterialsListPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then

            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
            lbPart.Text = jpPart
            lbSpec.Text = Request.QueryString("JPSpec").ToString.Trim
            lbStock.Text = Request.QueryString("stock").ToString.Trim
            lbNotDel.Text = Request.QueryString("issueQty").ToString.Trim
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
            lbCountIssue.Text = gvIssue.Rows.Count
            'PO Detail
            SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as OrdDetail," & _
                 " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as PO_Detail," & _
                 " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as PlanDelDate," & _
                 " cast(PURTD.TD008 as decimal(15,3)) as PO_Qty,cast(PURTD.TD015 as decimal(15,3)) as PODelQty, " & _
                 " cast(PURTD.TD008-PURTD.TD015 as decimal(15,3)) as PO_Bal  from PURTD " & _
                 " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' order by PURTD.TD001,PURTD.TD002"
            ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPO.Text = gvPO.Rows.Count

            'PR Detail
            SQL = " select TB001+'-'+TB002+'-'+TB003 as PR, " & _
                  " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as PrDate, " & _
                  " (SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4)) as PrReqDate, " & _
                  " cast(TR006 as decimal(15,3)) as prQty " & _
                  " from PURTR " & _
                  " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
                  " left join PURTA on TA001=TB001 and TA002=TB002  " & _
                  " where TR019='' and TB039='N'  and TB004='" & jpPart & "' order by TB004 "
            ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPR.Text = gvPO.Rows.Count

        End If
    End Sub
End Class