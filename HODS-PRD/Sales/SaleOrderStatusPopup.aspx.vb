Public Class SaleOrderStatusPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim whr As String = ""

        lbSO.Text = Request.QueryString("so").ToString.Trim
        lbItem.Text = Request.QueryString("item").ToString.Trim
        'lbDesc.Text = Request.QueryString("desc").ToString.Trim
        lbSpec.Text = Request.QueryString("spec").ToString.Trim

        If lbSO.Text <> "" Then
            whr = whr & " and COPTD.TD001 ='" & lbSO.Text.Substring(0, 4) & "' "
            whr = whr & " and COPTD.TD002 ='" & lbSO.Text.Substring(5, 11) & "' "
            whr = whr & " and COPTD.TD003 ='" & lbSO.Text.Substring(17, 4) & "' "
        End If
      

        'PR
        Dim SQL As String = " select PURTB.TB001+'-'+PURTB .TB002+'-'+PURTB.TB003 AS 'PR Type-No-Seq',PURTB.TB004 as 'Item', " & _
        " PURTB.TB005 as 'Desc.',PURTB.TB006 as 'Spec',CAST( PURTB.TB009 as decimal(16,3) )as 'PR Qty.',PURTA.TA007 as 'App. Status', " & _
        " (SUBSTRING(PURTA.TA013,7,2)+'-'+SUBSTRING(PURTA.TA013,5,2)+'-'+SUBSTRING(PURTA.TA013,1,4)) as 'Doc. Date'," & _
        " (SUBSTRING(PURTA.TA003,7,2)+'-'+SUBSTRING(PURTA.TA003,5,2)+'-'+SUBSTRING(PURTA.TA003,1,4)) as 'PR Date' FROM PURTB " & _
        " left join PURTA on PURTA.TA001 = PURTB.TB001 and PURTA.TA002 = PURTB.TB002" & _
        " left join COPTD on COPTD.TD001 = PURTB.TB029 and COPTD.TD002 = PURTB.TB030 and COPTD.TD003 = PURTB.TB031 where 1=1 " & whr & _
        " order by PURTB.TB001,PURTB.TB002,PURTB.TB003 "

        ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountPR.Text = ControlForm.rowGridview(gvPR)


        'PO
        SQL = " select PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 AS 'PO Type-No-Seq',PURTD.TD004 AS 'Item', " & _
            " PURTD.TD005 AS 'Desc.',PURTD.TD006 AS 'Spec',cast (PURTD.TD008 as decimal(16,3)) AS 'PO Qty.',PURTC.TC014 AS 'App. Status'," & _
            " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) AS 'Plan Date' FROM PURTD" & _
            " left join PURTC on PURTC.TC001 = PURTD.TD001 and PURTC.TC002 = PURTD.TD002" & _
            " left join COPTD on COPTD.TD001 = PURTD.TD013 and COPTD.TD002 = PURTD.TD021 and COPTD.TD003 = PURTD.TD023 where 1=1 " & whr & _
            " order by PURTD.TD001,PURTD.TD002,PURTD.TD003"

        ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountPO.Text = ControlForm.rowGridview(gvPO)


        'MO
        SQL = " select MOCTA.TA001+'-'+MOCTA.TA002 AS 'MO Type-No',MOCTA.TA006 AS 'Item'," & _
            " MOCTA.TA034 AS 'Desc.',MOCTA.TA035 AS 'Spec',CAST ( MOCTA.TA015 as decimal(18,3)) AS 'Plan Capacity'," & _
            " cast (MOCTA.TA017 as decimal(18,3)) AS 'MO Qty.',MOCTA.TA013 AS 'App. Status'," & _
            " (SUBSTRING(MOCTA.TA003,7,2)+'-'+SUBSTRING(MOCTA.TA003,5,2)+'-'+SUBSTRING(MOCTA.TA003,1,4)) AS 'Order Date'," & _
            " (SUBSTRING(MOCTA.TA009,7,2)+'-'+SUBSTRING(MOCTA.TA009,5,2)+'-'+SUBSTRING(MOCTA.TA009,1,4)) AS 'Start Date'," & _
            " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) AS 'Finish Date' FROM MOCTA " & _
            " left join COPTD on COPTD.TD001 = MOCTA.TA026 and COPTD.TD002 = MOCTA.TA027 and COPTD.TD003 = MOCTA.TA028 where 1=1 " & whr & _
            " order by MOCTA.TA001,MOCTA.TA002"

        ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountMO.Text = ControlForm.rowGridview(gvMO)


    End Sub

End Class