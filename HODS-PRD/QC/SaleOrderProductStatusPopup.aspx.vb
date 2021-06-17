Public Class SaleOrderProductStatusPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jpPart As String = Request.QueryString("item").ToString.Trim
        Dim cust As String = Request.QueryString("custID").ToString.Trim
        Dim soType As String = Request.QueryString("saleType").ToString.Trim
        Dim strDate As String = Request.QueryString("strDate").ToString.Trim
        Dim endDate As String = Request.QueryString("endDate").ToString.Trim
        Dim whr As String = ""

        lbCode.Text = jpPart
        lbSpec.Text = Request.QueryString("spec").ToString.Trim
        lbUndelivery.Text = Request.QueryString("undelQty").ToString.Trim
        lbStock.Text = Request.QueryString("stockQty").ToString.Trim
        lbMO.Text = Request.QueryString("moQty").ToString.Trim
        
        'whr = whr & Conn_SQL.Where("COPTD.TD001", soType, True, False)
        If soType <> "" And soType <> "" Then
            whr = whr & " and COPTD.TD001 in(" & soType & ") "
        End If
        If cust <> "" Then
            whr = whr & " and COPTC.TC004='" & cust & "' "
        End If
        whr = whr & Conn_SQL.Where("COPTD.TD013", strDate, endDate)

        'Sale order Detail
        Dim SQL As String = " select COPTC.TC004 as 'CUST ID',COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO Detail', " & _
                            " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'SO Approved Date'," & _
                            " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Delivery Date', " & _
                            " cast(COPTD.TD008 as decimal(8,0)) as 'Order Qty(+)',cast(COPTD.TD024 as decimal(8,0)) as 'Large Qty(+)'," & _
                            " cast(COPTD.TD009 as decimal(8,0)) as 'Delivery Qty(-)',cast(COPTD.TD025 as decimal(8,0)) as 'Largess Delivery Qty(-)'," & _
                            " cast(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025 as decimal(8,0)) as 'Balance Qty',COPTD.TD010 as Unit," & _
                            " COPTD.TD027 as 'Customer P/O' from COPTD " & _
                            " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                            " where COPTD.TD016='N' and COPTC.TC027='Y' and COPTD.TD004 = '" & jpPart & "' " & whr & _
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
    End Sub
End Class