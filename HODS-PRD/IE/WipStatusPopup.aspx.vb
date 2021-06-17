Public Class WipStatusPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim whList As String = "'3000','3100','3200','3300','3400','3600','3800'"
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then

            Dim jpPart As String = Request.QueryString("item").ToString.Trim,
                temptable As String = Request.QueryString("tempTable").ToString.Trim,
                wh As String = Request.QueryString("wh").ToString.Trim,
                whr As String = "",
                SQL As String = ""

            If wh <> "ALL" Then
                whr = whr & " and INVMC.MC002 = '" & wh & "' "
            Else
                whr = whr & " and INVMC.MC002 in (" & whList & ") "
            End If
            SQL = " select INVMC.MC002 as 'Main wh' ,INVMB.MB001 as Item," & _
                  " MB003 as Spec,cast(INVMC.MC007 as decimal(15,2)) as 'Inv Qty','' as 'MO Qty', " & _
                  " '' as 'Req MO Qty','' as 'Bal Qty' " & _
                  " from " & temptable & " T " & _
                  " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item and INVMC.MC007>0 " & _
                  " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=INVMC.MC001 " & _
                  " where T.item = '" & jpPart & "' " & whr & _
                  " order by INVMC.MC002 "
            ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
            'lbCountSO.Text = gvSO.Rows.Count

            'Materilas Request Issue
            SQL = " select case when TA026='' then '' else TA026+'-'+TA027+'-'+TA028 end as 'SO Detail'," & _
                  " TA001+'-'+TA002 as 'MO Detail',TB003 as 'Child Item',TB013 as 'Spec'," & _
                  " (SUBSTRING(TA040,7,2)+'-'+SUBSTRING(TA040,5,2)+'-'+SUBSTRING(TA040,1,4)) as 'MO Date', " & _
                  " (SUBSTRING(TA010,7,2)+'-'+SUBSTRING(TA010,5,2)+'-'+SUBSTRING(TA010,1,4)) as 'Plan Complete Date', " & _
                  " cast(isnull(TB004,0) as decimal(15,2)) as 'Mat Req',cast(isnull(TB005,0) as decimal(15,2)) as 'Mat Issue', " & _
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(15,2)) as 'issue Bal'," & _
                  " TA006 as 'Item',TA035 as 'Customer Spec' " & _
                  " from MOCTB " & _
                  " left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
                  " where TB003='" & jpPart & "' and TB004-TB005>0 and TA011 not in('y','Y')  and TA013='Y' " & _
                  " order by TA001,TA002 "
            ControlForm.ShowGridView(gvIssue, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountIssue.Text = gvIssue.Rows.Count

            'MO Detail
            SQL = " select case when TA026='' then '' else TA026+'-'+TA027+'-'+TA028 end as 'SO Detail'," & _
                  " TA001+'-'+TA002 as 'MO Detail', TA006 as 'Item',TA035 as 'Spec'," & _
                  " (SUBSTRING(TA040,7,2)+'-'+SUBSTRING(TA040,5,2)+'-'+SUBSTRING(TA040,1,4)) as 'Lot Date'," & _
                  " (SUBSTRING(TA010,7,2)+'-'+SUBSTRING(TA010,5,2)+'-'+SUBSTRING(TA010,1,4)) as 'Plan Complete Date'," & _
                  " cast(TA015 as decimal(15,2)) as 'MO Qty'," & _
                  " cast(TA017 as decimal(15,2)) as 'Finish Qty'," & _
                  " cast(TA018 as decimal(15,2)) as 'Scrap Qty'," & _
                  " cast(TA060 as decimal(15,2)) as 'Destoy Qty'," & _
                  " cast(TA015-TA017-TA018-TA060 as decimal(10,3)) as 'Bal Qty' " & _
                  " from MOCTA where  TA011 not in('y','Y') and TA013='Y' and TA006='" & jpPart & "' " & _
                  " order by TA001,TA002 "
            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = gvMO.Rows.Count

        End If
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'set row display
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            'e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(2).Text = "Summary"
            e.Row.Cells(3).Text = String.Format("{0:f2}", CDec(Request.QueryString("stockQty").ToString.Trim)) '
            e.Row.Cells(4).Text = String.Format("{0:f2}", CDec(Request.QueryString("moQty").ToString.Trim))
            e.Row.Cells(5).Text = String.Format("{0:f2}", CDec(Request.QueryString("issueQty").ToString.Trim))
            e.Row.Cells(6).Text = String.Format("{0:f2}", CDec(Request.QueryString("balQty").ToString.Trim))
        End If
    End Sub

    Private Sub gvIssue_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIssue.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'set row display
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            'e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(5).Text = "Sum Total"
            e.Row.Cells(8).Text = String.Format("{0:f2}", CDec(Request.QueryString("issueQty").ToString.Trim))


        End If
    End Sub

    Private Sub gvMO_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMO.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'set row display
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            'e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(5).Text = "Sum Total"
            e.Row.Cells(10).Text = String.Format("{0:f2}", CDec(Request.QueryString("moQty").ToString.Trim))
        End If
    End Sub

End Class