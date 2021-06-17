Imports System.Globalization
Public Class OrderToCash
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
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
        WHR = WHR & Conn_SQL.Where("COPTC.TC003", configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))

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

        SQL = " select COPTD.TD001,COPTD.TD002,COPTD.TD003,SUM(isnull(MOCTA.TA015,0))," & _
              " SUM(isnull(MOCTA.TA017,0)),SUM(isnull(MOCTA.TA018,0)+isnull(MOCTA.TA060,0))" & _
              " from JINPAO80.dbo.COPTD COPTD " & _
              " left join " & Conn_SQL.DBMain & "..COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
              " left join " & Conn_SQL.DBMain & "..MOCTA on MOCTA.TA026=COPTD.TD001 and MOCTA.TA027=COPTD.TD002 and MOCTA.TA028=COPTD.TD003 and MOCTA.TA006=COPTD.TD004 and MOCTA.TA013='Y' " & _
              " where COPTC.TC027='Y' " & WHR & _
              " group by COPTD.TD001,COPTD.TD002,COPTD.TD003 "
        ISQL = "insert into " & tempTable & "(saleType,saleNo,saleSeq,work,warehouse,reject) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

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
        WHR = ""
        If cbOverDue.Checked Then
            'WHR = WHR & Conn_SQL.Where("ACRTA.TA020", configDate.dateFormat2(tbDateFromAR.Text.Trim), configDate.dateFormat2(tbDateToAR.Text.Trim))
            WHR = WHR & " ACRTA.TA100<>'3' and ACRTA.TA020<>'' and ACRTA.TA020< '" & Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US")) & "' "
        Else
            WHR = WHR & Conn_SQL.Where("ACRTA.TA020", configDate.dateFormat2(tbDateFromAR.Text.Trim), configDate.dateFormat2(tbDateToAR.Text.Trim), False, False)
        End If
        If WHR <> "" Then
            WHR = " where " & WHR
        End If
        SQL = " select (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'C01'," & _
              " COPTC.TC004+'-'+COPMA.MA002 as 'C02'," & _
              " COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'C03'," & _
              " COPTD.TD004 as 'C04',COPTD.TD006 as 'C05'," & _
              " CONVERT(VARCHAR,cast(COPTD.TD008 as money),1) as 'C06'," & _
              "(SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'C07'," & _
              " CONVERT(VARCHAR,cast(T.work as money),1) as 'C08'," & _
              " CONVERT(VARCHAR,cast(T.warehouse as money),1) as 'C09'," & _
              " CONVERT(VARCHAR,cast(T.reject as money),1) as 'C10'," & _
              "(SUBSTRING(ACRTA.TA038,7,2)+'-'+SUBSTRING(ACRTA.TA038,5,2)+'-'+SUBSTRING(ACRTA.TA038,1,4)) as 'C11'," & _
              " CONVERT(VARCHAR,cast(isnull(ACRTB.TB022,0) as money),1) as 'C12', " & _
              "(SUBSTRING(ACRTA.TA020,7,2)+'-'+SUBSTRING(ACRTA.TA020,5,2)+'-'+SUBSTRING(ACRTA.TA020,1,4)) as 'C13'," & _
              " case when ACRTA.TA100='3' then 'All' when ACRTA.TA100='3' then 'Partial' else 'None' end as 'C14' " & _
              " from " & tempTable & " T " & _
              " left join " & Conn_SQL.DBMain & "..COPTH on COPTH.TH014=T.saleType and COPTH.TH015=T.saleNo and COPTH.TH016=T.saleSeq and COPTH.TH020='Y' " & _
              " left join " & Conn_SQL.DBMain & "..ACRTB on ACRTB.TB004='1' and ACRTB.TB005=COPTH.TH001 and ACRTB.TB006=COPTH.TH002 and ACRTB.TB007=COPTH.TH003 and ACRTB.TB012='Y' " & _
              " left join " & Conn_SQL.DBMain & "..ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002 " & _
              " left join " & Conn_SQL.DBMain & "..COPTD on COPTD.TD001=T.saleType and COPTD.TD002=T.saleNo and COPTD.TD003=T.saleSeq " & _
              " left join " & Conn_SQL.DBMain & "..COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
              " left join " & Conn_SQL.DBMain & "..COPMA on COPMA.MA001=COPTC.TC004 " & WHR & _
              " order by T.saleType,T.saleNo,T.saleSeq "

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
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvShow.DataBound
        ControlForm.MergeGridview(gvShow, 3)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim saleDetail As String = .DataItem("C03").ToString.Trim
                If saleDetail <> "" Then
                    Dim SO() As String = saleDetail.Split("-")
                    Dim saleType As String = SO(0)
                    Dim saleNo As String = SO(1)
                    Dim saleSeq As String = SO(2)
                    .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                    .Attributes.Add("onclick", "ChangeRowColor(this,'','');NewWindow('OrderToCashPopup.aspx?saleType=" & saleType & "&saleNo=" & saleNo & "&saleSeq=" & saleSeq & "','OrderToCash',800,500,'yes');")
                End If
            End If
        End With

    End Sub
End Class