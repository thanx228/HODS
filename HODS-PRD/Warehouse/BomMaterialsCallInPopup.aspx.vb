Public Class BomMaterialsCallInPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Private stockQty As Decimal = 0
    Private matIssueQty As Decimal = 0
    Private moQty As Decimal = 0
    Private poQty As Decimal = 0
    Private prQty As Decimal = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim whr As String = "", SQL As String = ""

            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim

            SQL = "select MB002,MB003,MB017 from INVMB where MB001='" & jpPart & "' "

            Dim Program As New DataTable
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

            lbPart.Text = jpPart
            lbDesc.Text = Program.Rows(0).Item("MB002")
            lbSpec.Text = Program.Rows(0).Item("MB003")
            lbWH.Text = Program.Rows(0).Item("MB017")
            'Stock
            SQL = "select MC002 as 'WH',cast(MC007 as decimal(16,3)) as 'Stock Qty' from INVMC where MC001='" & jpPart & "' and MC007>0 order by MC002"
            ControlForm.ShowGridView(gvStock, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountStock.Text = gvStock.Rows.Count

            'Materilas Request Issue
            SQL = " select TA001+'-'+TA002 as MO,(SUBSTRING(TA040,7,2)+'-'+SUBSTRING(TA040,5,2)+'-'+SUBSTRING(TA040,1,4)) as 'Lot Date', " & _
                  " (SUBSTRING(TA010,7,2)+'-'+SUBSTRING(TA010,5,2)+'-'+SUBSTRING(TA010,1,4)) as 'Due Date', " & _
                  " cast(isnull(TB004,0) as decimal(10,3)) as 'Mat Issue Req',cast(isnull(TB005,0) as decimal(10,3)) as 'Mat issued', " & _
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(10,3)) as 'Mat Issue Bal'," & _
                  " TA006 as 'JP PART',TA035 as 'JP SPEC',TA013 as 'Status' " & _
                  " from MOCTB " & _
                  " left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
                  " where TB003='" & jpPart & "' and TB004-TB005>0 and TA011 not in('y','Y')   " & _
                  " order by TA001,TA002 " 'and TA013='Y'
            ControlForm.ShowGridView(gvIssue, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountIssue.Text = ControlForm.rowGridview(gvIssue)

            'MO Detail
            SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'Ord Detail'," & _
                  " MOCTA.TA001+'-'+MOCTA.TA002 as 'Lot No'," & _
                  " (SUBSTRING(MOCTA.TA040,7,2)+'-'+SUBSTRING(MOCTA.TA040,5,2)+'-'+SUBSTRING(MOCTA.TA040,1,4)) as 'Lot Date'," & _
                  " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) as 'Plan Due Date'," & _
                  " cast(MOCTA.TA015 as decimal(10,3)) as 'MO Qty',cast(MOCTA.TA017 as decimal(10,3)) as 'Produced Qty'," & _
                  " cast(MOCTA.TA015-MOCTA.TA017 as decimal(10,3)) as 'MO Bal',MOCTA.TA013 as 'Status' " & _
                  " from MOCTA where  MOCTA.TA011 not in('y','Y')  and MOCTA.TA006='" & jpPart & "' " & _
                  " order by MOCTA.TA001,MOCTA.TA002 " 'and MOCTA.TA013='Y'

            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = ControlForm.rowGridview(gvMO)
            'PO Detail
            SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as 'Ord Detail'," & _
                 " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as 'PO Detail'," & _
                 " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as 'Plan Del Date'," & _
                 " cast(PURTD.TD008 as decimal(16,3)) as 'PO Qty',cast(PURTD.TD015 as decimal(16,3)) as 'PO Del Qty', " & _
                 " cast(PURTD.TD008-PURTD.TD015 as decimal(16,3)) as 'PO Bal',PURTC.TC014 as 'Status' from PURTD " & _
                 " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " & _
                 " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' order by PURTD.TD001,PURTD.TD002,PURTD.TD003"

            ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPO.Text = ControlForm.rowGridview(gvPO)

            'PR Detail
            SQL = " select TB001+'-'+TB002+'-'+TB003 as 'PR Detail', " & _
                  " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'PR Date', " & _
                  " (SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4)) as 'PR Req Date ', " & _
                  " cast(TR006 as decimal(8,2)) as 'PR Qty',TA007 as 'Status' " & _
                  " from PURTR " & _
                  " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
                  " left join PURTA on TA001=TB001 and TA002=TB002  " & _
                  " where TR019='' and TB039='N'  and TB004='" & jpPart & "' order by TB001,TB002,TB003 "
            ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountPR.Text = ControlForm.rowGridview(gvPR)

        End If
    End Sub

    Private Sub gvStock_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvStock.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' if row type is DataRow, add ProductSales value to TotalSales
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "Stock Qty")) Then
                stockQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Stock Qty"))
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(0).Text = "Sum"
            e.Row.Cells(1).Text = String.Format("{0:f2}", CDec(stockQty))
        End If
    End Sub

    Private Sub gvIssue_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIssue.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' if row type is DataRow, add ProductSales value to TotalSales
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "Mat Issue Bal")) Then
                matIssueQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Mat Issue Bal"))
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(0).Text = "Sum"
            e.Row.Cells(5).Text = String.Format("{0:f2}", CDec(matIssueQty))
        End If
    End Sub

    Private Sub gvMO_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMO.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' if row type is DataRow, add ProductSales value to TotalSales
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "MO Bal")) Then
                moQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MO Bal"))
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(0).Text = "Sum"
            e.Row.Cells(6).Text = String.Format("{0:f2}", CDec(moQty))
        End If
    End Sub

    Private Sub gvPO_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPO.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' if row type is DataRow, add ProductSales value to TotalSales
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PO Bal")) Then
                poQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PO Bal"))
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(0).Text = "Sum"
            e.Row.Cells(5).Text = String.Format("{0:f2}", CDec(poQty))
        End If
    End Sub

    Private Sub gvPR_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPR.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' if row type is DataRow, add ProductSales value to TotalSales
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PR Qty")) Then
                prQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PR Qty"))
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' If row type is footer, show calculated total value
            ' Since this example uses sales in dollars, I formatted output as currency
            e.Row.Cells(0).Text = "Sum"
            e.Row.Cells(3).Text = String.Format("{0:f2}", CDec(prQty))
        End If
    End Sub
End Class