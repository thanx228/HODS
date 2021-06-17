Public Class fgInventoryStatusPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim item As String = Request.QueryString("item").ToString.Trim
        Dim spec As String = Request.QueryString("spec").ToString.Trim
        Dim cust As String = Request.QueryString("cust").ToString.Trim
        Dim soType As String = Request.QueryString("saleType").ToString.Trim
        Dim soNo As String = Request.QueryString("saleNo").ToString.Trim
        Dim soSeq As String = Request.QueryString("saleSeq").ToString.Trim
        Dim endDate As String = Request.QueryString("endDate").ToString.Trim
        Dim whr As String = ""

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
        Dim SQL As String = " select COPMA.MA102 as 'CUST NAME',COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO Detail', " & _
                            " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'SO Approved Date'," & _
                            " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Delivery Date', " & _
                            " cast(COPTD.TD008 as decimal(8,0)) as 'Order Qty(+)',cast(COPTD.TD024 as decimal(8,0)) as 'Large Qty(+)'," & _
                            " cast(COPTD.TD009 as decimal(8,0)) as 'Delivery Qty(-)',cast(COPTD.TD025 as decimal(8,0)) as 'Largess Delivery Qty(-)'," & _
                            " cast(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025 as decimal(8,0)) as 'Balance Qty',COPTD.TD010 as Unit," & _
                            " COPTD.TD027 as 'Customer P/O' from COPTD " & _
                            " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                            " left join COPMA on MA001=TC004 " & _
                            " where COPTD.TD016='N' and COPTC.TC027='Y' and COPTD.TD004 = '" & item & "' " & whr & _
                            " order by COPTD.TD013 "
        ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountSO.Text = ControlForm.rowGridview(gvSO)

        'SO Close Detail
        SQL = " select COPMA.MA102 as 'CUST NAME',COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO Detail', " & _
              " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'SO Approved Date'," & _
              " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Delivery Date', " & _
              " cast(COPTD.TD008 as decimal(8,0)) as 'Order Qty',cast(COPTD.TD009 as decimal(8,0)) as 'Delivery Qty'," & _
              " cast(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025 as decimal(8,0)) as 'Close Qty',COPTD.TD010 as Unit," & _
              "(SUBSTRING(COPTE.TE038,7,2)+'-'+SUBSTRING(COPTE.TE038,5,2)+'-'+SUBSTRING(COPTE.TE038,1,4)) as 'Manual Close Date' " & _
              " from COPTD " & _
              " left join COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
              " left join COPTF on COPTF.TF001=COPTD.TD001 and COPTF.TF002=COPTD.TD002 and COPTF.TF104=COPTD.TD003 and COPTF.TF017 = 'Y' " & _
              " left join COPTE on COPTE.TE001=COPTF.TF001 and COPTE.TE002=COPTF.TF002 and COPTE.TE003=COPTF.TF003 " & _
              " left join COPMA on MA001=TC004 " & _
              " where COPTC.TC027='Y' and TD016 = 'y'  and TD004 = '" & item & "' " & whr & _
              " order by COPTD.TD013 "

        ControlForm.ShowGridView(gvClose, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountClose.Text = ControlForm.rowGridview(gvClose)

        'SOChange Detail
        'whr = Conn_SQL.Where("TF005", tbItem)
        'whr = whr & Conn_SQL.Where("TF006", tbSpec)
        'whr = whr & Conn_SQL.Where("TF015", "", DueDate)
        'whr = whr & Conn_SQL.Where("TF001", cblSaleType)
        'whr = whr & Conn_SQL.Where("TF002", tbSaleNo)
        'whr = whr & Conn_SQL.Where("TF004", tbSaleSeq)
        'whr = whr & Conn_SQL.Where("TE007", tbCust)
        whr = ""
        If soType <> "" And soType <> "" Then
            whr = whr & " and COPTF.TF001 in(" & soType & ") "
        End If
        If soNo <> "" Then
            whr = whr & " and COPTF.TF002='" & soNo & "' "
        End If
        If soSeq <> "" Then
            whr = whr & " and COPTF.TF004='" & soSeq & "' "
        End If

        If cust <> "" Then
            whr = whr & " and COPTF.TE007='" & cust & "' "
        End If

        If endDate <> "" Then
            whr = whr & " and COPTF.TF015<='" & endDate & "' "
        End If

        SQL = " select COPMA.MA102 as 'CUST NAME',TF001+'-'+TF002+'-'+TF004 as 'SO Detail',TF003 as 'SO Chg Ver.'," & _
              " (SUBSTRING(TC003,7,2)+'-'+SUBSTRING(TC003,5,2)+'-'+SUBSTRING(TC003,1,4)) as 'SO Approved Date'," & _
              " cast(TF109 as decimal(8,0)) as 'SO Qty Change FM',cast(TF009 as decimal(8,0)) as 'SO Qty Change TO'," & _
              " cast(TD009 as decimal(8,0)) as 'Delivery Qty(-)',cast(TD008-TD009 as decimal(8,0)) as 'Undelivery Qty'," & _
              " (SUBSTRING(TE004,7,2)+'-'+SUBSTRING(TE004,5,2)+'-'+SUBSTRING(TE004,1,4)) as 'SO Change Approved Date' " & _
              " from COPTF left join COPTE on TE001=TF001 and TE002=TF002 and TE003=TF003 " & _
              " left join COPMA on MA001=TE007 " & _
              " left join COPTD on TD001=TF001 and TD002=TF002 and TD003=TF004 " & _
              " left join COPTC on TC001=TD001 and TC002=TD002 " & _
              " where TE029 = 'Y' and TF105=TF005 and TF005='" & item & "' and TF009<>TF109 " & whr & _
              " order by TF001,TF002,TF003,TF004 "

        'SQL = " select  substring(PURTD.TD024,1,4)+'-'+PURTD.TD021+'-'+PURTD.TD023 as 'SO Detail'," & _
        '     " PURTD.TD001+'-'+PURTD.TD002+'-'+PURTD.TD003 as 'PO Detail'," & _
        '     " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as 'Delivery Date'," & _
        '     " cast(PURTD.TD008 as decimal(8,0)) as 'PO Qty',cast(PURTD.TD015 as decimal(8,0)) as 'Reciepted Qty', " & _
        '     " cast(PURTD.TD008-PURTD.TD015 as decimal(8,0)) as 'Bal Qty'  from PURTD " & _
        '     " where  PURTD.TD016='N' and PURTD.TD004='" & item & "' order by PURTD.TD001,PURTD.TD002"

        ControlForm.ShowGridView(gvChange, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountChange.Text = ControlForm.rowGridview(gvChange)

        
    End Sub
End Class