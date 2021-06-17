Public Class SupplyStockPop
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim jpPart As String = Request.QueryString("item").ToString.Trim
            Dim SQL As String = ""
            'PR Detail
            SQL = " select TB004 as 'Item',TB005 as 'Item Desc.',TB006 as 'Spec.', " & _
                  " cast(TR006 as decimal(8,2)) as prQty,TB001+'-'+TB002+'-'+TB003 as PR " & _
                  " from PURTR " & _
                  " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
                  " left join PURTA on TA001=TB001 and TA002=TB002  " & _
                  " where TR019='' and TB039='N'  and TB004='" & jpPart & "' order by TB001,TB002,TB003 "
            ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPR.Text = ControlForm.rowGridview(gvPR)

            'PO Detail
            SQL = " select TD004 as 'Item',TD005 as 'Item Desc.',TD006 as 'Spec.', " & _
                  " TB001+'-'+TB002+'-'+TB003 as 'PR Detail'," & _
                  " cast(TB014 as decimal(10,2)) as 'PR Qty'," & _
                  " TD001+'-'+TD002+'-'+TD003 as 'PO Detail'," & _
                  " cast(TD008 as decimal(8,0)) as 'PO Qty'," & _
                  " cast(TD015 as decimal(8,0)) as 'PO Del Qty', " & _
                  " cast(TD008-TD015 as decimal(8,0)) as 'PO Bal' , " & _
                  " (SUBSTRING(TD012,7,2)+'-'+SUBSTRING(TD012,5,2)+'-'+SUBSTRING(TD012,1,4)) as PlanDelDate," & _
                  " TC004 as 'Supplier Code' " & _
                  " from PURTD " & _
                  " left join PURTC on TC001=TD001 and TC002=TD002 " & _
                  " left join PURTB on TB001=TD026 and TB002=TD027 and TB003=TD026 " & _
                  " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' " & _
                  " order by PURTD.TD001,PURTD.TD002,PURTD.TD003 "

            ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)

            lbCountPO.Text = ControlForm.rowGridview(gvPO)

        End If
    End Sub
End Class