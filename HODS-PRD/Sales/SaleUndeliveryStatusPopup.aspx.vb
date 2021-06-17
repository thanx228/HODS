﻿Public Class SaleUndeliveryStatusPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
        Dim cust As String = Request.QueryString("custID").ToString.Trim
        Dim soType As String = Request.QueryString("saleType").ToString.Trim
        Dim soNo As String = Request.QueryString("saleNo").ToString.Trim
        Dim soSeq As String = Request.QueryString("saleSeq").ToString.Trim
        Dim endDate As String = Request.QueryString("endDate").ToString.Trim
        Dim whr As String = ""

        lbCode.Text = jpPart
        lbSpec.Text = Request.QueryString("JPSpec").ToString.Trim
        lbUndelivery.Text = Request.QueryString("delQty").ToString.Trim
        lbStock.Text = Request.QueryString("stock").ToString.Trim
        lbMO.Text = Request.QueryString("moQty").ToString.Trim
        lbPO.Text = Request.QueryString("poQty").ToString.Trim
        lbPR.Text = Request.QueryString("prQty").ToString.Trim

        'whr = whr & Conn_SQL.Where("COPTD.TD001", soType, True, False)
        If soType <> "" And soType <> "" Then
            whr = whr & " and COPTD.TD001 in(" & soType & ") "
        End If
        If soNo <> "" Then
            whr = whr & " and COPTD.TD002='" & soNo & "' "
        End If
        If soSeq <> "" Then
            whr = whr & " and COPTD.TD003='" & soSeq & "' "
        End If

        If cust <> "" Then
            whr = whr & " and COPTC.TC004='" & cust & "' "
        End If

        If endDate <> "" Then
            whr = whr & " and COPTD.TD013<='" & endDate & "' "
        End If

        'Sale order Detail
        Dim SQL As String = " select COPTC.TC004 as 'CUST ID',COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO Detail', " & _
                            " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'SO Approved Date'," & _
                            " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Delivery Date', " & _
                            " cast(COPTD.TD008 as decimal(8,0)) as 'Order Qty(+)',cast(COPTD.TD024 as decimal(8,0)) as 'Large Qty(+)'," & _
                            " cast(COPTD.TD009 as decimal(8,0)) as 'Delivery Qty(-)',cast(COPTD.TD025 as decimal(8,0)) as 'Largess Delivery Qty(-)'," & _
                            " cast(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025 as decimal(8,0)) as 'Balance Qty',COPTD.TD010 as Unit," & _
                            " COPTD.TD027 as 'Customer P/O' from COPTD " & _
                            " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                            "  where COPTD.TD016='N' and COPTC.TC027='Y' and COPTD.TD004 = '" & jpPart & "' " & whr & _
                            " order by COPTD.TD013 "
        ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountSO.Text = ControlForm.rowGridview(gvSO)
        'MO Detail

        SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO Detail'," & _
              " MOCTA.TA001+'-'+MOCTA.TA002 as 'MO Detail'," & _
              " (SUBSTRING(MOCTA.TA004,7,2)+'-'+SUBSTRING(MOCTA.TA004,5,2)+'-'+SUBSTRING(MOCTA.TA004,1,4)) as 'Lot Date'," & _
              " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) as 'Plan Complete Date'," & _
              " cast(MOCTA.TA015 as decimal(8,0)) as 'MO Qty'," & _
              " cast(MOCTA.TA017 as decimal(8,0)) as 'Finished Qty'," & _
              " cast(MOCTA.TA018 as decimal(8,0)) as 'Scrap Qty'," & _
              " cast(MOCTA.TA015-MOCTA.TA017-MOCTA.TA018 as decimal(8,0)) as 'Bal Qty'" & _
              " from MOCTA " & _
              " where  MOCTA.TA011 not in('y','Y') and MOCTA.TA006='" & jpPart & "' " & _
              " order by MOCTA.TA001,MOCTA.TA002 "
        ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountMO.Text = ControlForm.rowGridview(gvMO)
        'PO Detail

        SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as 'SO Detail'," & _
             " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as 'PO Detail'," & _
             " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as 'Delivery Date'," & _
             " cast(PURTD.TD008 as decimal(8,0)) as 'PO Qty',cast(PURTD.TD015 as decimal(8,0)) as 'Reciepted Qty', " & _
             " cast(PURTD.TD008-PURTD.TD015 as decimal(8,0)) as 'Bal Qty'  from PURTD " & _
             " where  PURTD.TD016='N' and PURTD.TD004='" & jpPart & "' order by PURTD.TD001,PURTD.TD002"

        ControlForm.ShowGridView(gvPO, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountPO.Text = ControlForm.rowGridview(gvPO)

        'PR
        SQL = " select  B.TB029+'-'+B.TB030+'-'+B.TB031 as 'SO Detail',B.TB001+'-'+B.TB002+'-'+B.TB003 as 'PR Detail'," & _
            " (SUBSTRING(B.TB011,7,2)+'-'+SUBSTRING(B.TB011,5,2)+'-'+SUBSTRING(B.TB011,1,4)) as 'Require Date'," & _
            " cast(B.TB009 as decimal(8,0)) as 'PR Qty' from PURTR R " & _
            " left join PURTB B on B.TB001=R.TR001 and B.TB002=R.TR002 and B.TB003=R.TR003 " & _
            " where R.TR019='' and B.TB039='N' and B.TB004='" & jpPart & "' " & _
            " order by B.TB001,B.TB002,B.TB003"
        ControlForm.ShowGridView(gvPR, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountPR.Text = ControlForm.rowGridview(gvPR)
    End Sub
End Class