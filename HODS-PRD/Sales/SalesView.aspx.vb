Imports System.Globalization
Public Class SalesView
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("OrderToCash" & Session("UserName"), gvShow)
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempOrderToCash" & Session("UserName")
        CreateTempTable.createTempOrderToCash(tempTable)

        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = ""

        WHR = WHR & Conn_SQL.Where("COPTC.TC001", ddlSaleType)
        WHR = WHR & Conn_SQL.Where("COPTC.TC004", tbCust)

        Dim soNoFrom As String = tbSaleNoFrom.Text.Trim
        Dim soNoTo As String = tbSaleNoTo.Text.Trim
        If soNoFrom <> "" Or soNoTo <> "" Then
            Dim fldName As String = "COPTC.TC002"
            If soNoFrom <> "" And soNoTo <> "" Then
                WHR = WHR & Conn_SQL.Where(fldName, soNoFrom, soNoTo)
            Else
                Dim soNo As TextBox = tbSaleNoFrom
                If soNo.Text.Trim = "" Then
                    soNo = tbSaleNoTo
                End If
                WHR = WHR & Conn_SQL.Where(fldName, soNo)
            End If
        End If
        If cbOverDue.Checked Then
            WHR = WHR & " and COPTD.TD013< '" & Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US")) & "' "
        Else
            WHR = WHR & Conn_SQL.Where("COPTD.TD013", configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))
        End If

        'WHR = WHR & Conn_SQL.Where("COPTD.TD004", tbCode)
        'WHR = WHR & Conn_SQL.Where("COPTD.TD006", tbSpec)
        '      " left join JINPAO80.dbo.SFCTC SFCTC on SFCTC.TC004=MOCTA.TA001 and SFCTC.TC005=MOCTA.TA002  and SFCTC.TC001='D301'  and SFCTC.TC022='Y' " & _

        'SQL = " select COPTD.TD001,COPTD.TD002,COPTD.TD003,SUM(isnull(MOCTA.TA015,0))," & _
        '      " SUM(isnull(MOCTA.TA017,0)),SUM(isnull(MOCTA.TA018,0)+isnull(MOCTA.TA060,0))," & _
        '      " SUM(isnull(COPTH.TH008,0)),SUM(isnull(ACRTB.TB022,0))," & _
        '      " sum(case when ACRTA.TA100='3' then isnull(ACRTB.TB022,0) else 0 end) " & _
        '      " from JINPAO80.dbo.COPTD COPTD " & _
        '      " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
        '      " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA026=COPTD.TD001 and MOCTA.TA027=COPTD.TD002 and MOCTA.TA028=COPTD.TD003 and MOCTA.TA006=COPTD.TD004 and MOCTA.TA013='Y' " & _
        '      " left join JINPAO80.dbo.COPTH COPTH on COPTH.TH014=COPTD.TD001 and COPTH.TH015=COPTD.TD002 and COPTH.TH016=COPTD.TD003 and COPTH.TH020='Y' " & _
        '      " left join JINPAO80.dbo.ACRTB ACRTB on ACRTB.TB004='1' and ACRTB.TB005=COPTH.TH001 and ACRTB.TB006=COPTH.TH002 and ACRTB.TB007=COPTH.TH003 and ACRTB.TB012='Y'" & _
        '      " left join JINPAO80.dbo.ACRTA ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002 " & _
        '      " where COPTC.TC027='Y' " & WHR & _
        '      " group by COPTD.TD001,COPTD.TD002,COPTD.TD003 "
        'ISQL = "insert into " & tempTable & "(saleType,saleNo,saleSeq,work,warehouse,reject,ship,invoice,receipt) " & SQL
        'Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        'MO  MOCTA.TA018+MOCTA.TA060
        'SQL = " select COPTD.TD001,COPTD.TD002,COPTD.TD003,SUM(isnull(MOCTA.TA015,0))," & _
        '      " SUM(isnull(MOCTA.TA017,0)),SUM(isnull(MOCTA.TA018,0)+isnull(MOCTA.TA060,0))" & _
        '      " from JINPAO80.dbo.COPTD COPTD " & _
        '      " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
        '      " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA026=COPTD.TD001 and MOCTA.TA027=COPTD.TD002 and MOCTA.TA028=COPTD.TD003 and MOCTA.TA006=COPTD.TD004 and MOCTA.TA013='Y' " & _
        '      " where COPTC.TC027='Y' " & WHR & _
        '      " group by COPTD.TD001,COPTD.TD002,COPTD.TD003 "
        'ISQL = "insert into " & tempTable & "(saleType,saleNo,saleSeq,work,warehouse,reject) " & SQL
        'Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)


        ''Invoice
        'SQL = " select T.saleType,T.saleNo,T.saleSeq," & _
        '      " SUM(isnull(COPTH.TH008,0)) as delQty,SUM(isnull(ACRTB.TB022,0)) as invQty," & _
        '      " sum(case when ACRTA.TA100='3' then isnull(ACRTB.TB022,0) else 0 end) as rcpQty " & _
        '      " from " & tempTable & " T " & _
        '      " left join JINPAO80.dbo.COPTH COPTH on COPTH.TH014=T.saleType and COPTH.TH015=T.saleNo and COPTH.TH016=T.saleSeq and COPTH.TH020='Y' " & _
        '      " left join JINPAO80.dbo.ACRTB ACRTB on ACRTB.TB004='1' and ACRTB.TB005=COPTH.TH001 and ACRTB.TB006=COPTH.TH002 and ACRTB.TB007=COPTH.TH003 and ACRTB.TB012='Y' " & _
        '      " left join JINPAO80.dbo.ACRTA ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002 " & _
        '      " group by T.saleType,T.saleNo,T.saleSeq "
        'Dim Program As New DataTable
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        'For i As Integer = 0 To Program.Rows.Count - 1
        '    With Program.Rows(i)
        '        Dim updHash As Hashtable = New Hashtable,
        '            whrHash As Hashtable = New Hashtable
        '        'fld PK
        '        whrHash.Add("saleType", .Item("saleType").ToString.Trim)
        '        whrHash.Add("saleNo", .Item("saleNo").ToString.Trim)
        '        whrHash.Add("saleSeq", .Item("saleSeq").ToString.Trim)
        '        'fld update
        '        updHash.Add("ship", CDec(.Item("delQty").ToString.Trim))
        '        updHash.Add("invoice", CDec(.Item("invQty").ToString.Trim))
        '        updHash.Add("receipt", CDec(.Item("rcpQty").ToString.Trim))
        '        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, updHash, whrHash, "U"), Conn_SQL.MIS_ConnectionString)
        '    End With
        'Next

        'SQL = " select (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'Sale Order Entry Date'," & _
        '      " COPTC.TC004+'-'+COPMA.MA002 as 'Customer'," & _
        '      " COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'Sale Order Number'," & _
        '      " COPTD.TD004 as 'Item',COPTD.TD006 as 'Spec'," & _
        '      " CONVERT(VARCHAR,cast(COPTD.TD008 as money),1) as 'Quantity'," & _
        '      "(SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Request Delivery Date'," & _
        '      " CONVERT(VARCHAR,cast(T.work as money),1) as 'Work Qty'," & _
        '      " CONVERT(VARCHAR,cast(T.warehouse as money),1) as 'Warehouse Receive Qty'," & _
        '      " CONVERT(VARCHAR,cast(T.reject as money),1) as 'QC Reject Qty'," & _
        '      " CONVERT(VARCHAR,cast(T.ship as money),1) as 'Shipment Qty'," & _
        '      " CONVERT(VARCHAR,cast(T.invoice as money),1) as 'Invoice Qty', " & _
        '      " CONVERT(VARCHAR,cast(T.receipt as money),1) as 'Receipt Qty' " & _
        '      " from " & tempTable & " T " & _
        '      " left join JINPAO80.dbo.COPTD COPTD on COPTD.TD001=T.saleType and COPTD.TD002=T.saleNo and COPTD.TD003=T.saleSeq " & _
        '      " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
        '      " left join JINPAO80.dbo.COPMA COPMA on COPMA.MA001=COPTC.TC004 " & _
        '      " order by T.saleType,T.saleNo,T.saleSeq "
        Dim sSQL As String = ""
        sSQL = " select top 1 (SUBSTRING(COPTF.TF115,7,2)+'-'+SUBSTRING(COPTF.TF115,5,2)+'-'+SUBSTRING(COPTF.TF115,1,4)) " & _
               " from JINPAO80.dbo.COPTF where TF015<>TF115 and TF015<>'' and TF115<>'' and TF115<>COPTD.TD013 " & _
               "  and TF001=COPTD.TD001 and TF002=COPTD.TD002 and TF004=COPTD.TD003 order by TF003"

        SQL = " select (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'TC003'," & _
              " COPTC.TC004+'-'+COPMA.MA002 as 'TC004'," & _
              " COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SaleNo'," & _
              " COPTD.TD004 as 'Item',COPTD.TD006 as 'Spec'," & _
              " CONVERT(VARCHAR,cast(COPTD.TD008 as money),1) as 'TD008'," & _
              " isnull((" & sSQL & "),'') as 'fReqDate'," & _
              " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'TD013'," & _
              " (SUBSTRING(MOCTA.TA040,7,2)+'-'+SUBSTRING(MOCTA.TA040,5,2)+'-'+SUBSTRING(MOCTA.TA040,1,4)) as 'TA040'," & _
              " (SUBSTRING(MOCTA.TA009,7,2)+'-'+SUBSTRING(MOCTA.TA009,5,2)+'-'+SUBSTRING(MOCTA.TA009,1,4)) as 'TA009'," & _
              " case when MOCTA.TA014='' then '' else (SUBSTRING(MOCTA.TA014,7,2)+'-'+SUBSTRING(MOCTA.TA014,5,2)+'-'+SUBSTRING(MOCTA.TA014,1,4)) end as 'TA014'," & _
              " case when MOCTA.TA014='' then '' else DATEDIFF(DD,CONVERT(datetime,MOCTA.TA009),CONVERT(datetime,MOCTA.TA014)) end as 'onTime', " & _
              " case MOCTA.TA011 when '1' then 'Have not Manufactured' " & _
              "            when '2' then 'Materials Not Issue' " & _
              "            when '3' then 'Manufacturing' " & _
              "            when 'Y' then 'Finished' " & _
              "                     else 'Manual Finished' end as 'TA011' " & _
              " from JINPAO80.dbo.COPTD COPTD " & _
              " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA026=COPTD.TD001 and MOCTA.TA027=COPTD.TD002 and MOCTA.TA028=COPTD.TD003 and MOCTA.TA006=COPTD.TD004 and MOCTA.TA013='Y' " & _
              " left join JINPAO80.dbo.COPMA COPMA on COPMA.MA001=COPTC.TC004 " & _
              " where COPTC.TC027='Y' and COPTD.TD016 = 'N' " & WHR & _
              " order by COPTD.TD013,COPTD.TD001,COPTD.TD002,COPTD.TD003 "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                'Dim saleDetail As String = .DataItem("Sale Order Number").ToString.Trim
                'If saleDetail <> "" Then
                '    Dim SO() As String = saleDetail.Split("-")
                '    Dim saleType As String = SO(0)
                '    Dim saleNo As String = SO(1)
                '    Dim saleSeq As String = SO(2)
                '    .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                '    .Attributes.Add("onclick", "ChangeRowColor(this,'','');NewWindow('OrderToCashPopup.aspx?saleType=" & saleType & "&saleNo=" & saleNo & "&saleSeq=" & saleSeq & "','OrderToCash',800,500,'yes');")
                'End If
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With

    End Sub
End Class