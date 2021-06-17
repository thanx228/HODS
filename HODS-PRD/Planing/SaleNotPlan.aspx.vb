Imports System.IO
Imports System.Globalization

Public Class SaleNotPlan
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       If Not IsPostBack Then

            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            'Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            lblShowText.Text = ""
            lblList.Text = 0

        End If
    End Sub

    Protected Sub CreateTempSale(tableName As String)

        Dim date1 As String = txtDateFrom.Text
        Dim date2 As String = txtDateTo.Text
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = ""
        Dim endDate As String = ""

        'Begin date
        If date1 <> "" Then
             strDate = ConfigDate.dateFormat(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = ConfigDate.dateFormat(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        'insert sal order all 
        Dim WH As String = "  "
        If ddlCodeType.Text = "0" Then
            WH = WH & " and  TD004 not like '822%' and TD004 not like '823%' and TD004 not like '833%'"
        Else
            WH = WH & " and  (TD004 like '822%' or TD004 like '823%')"
        End If

        'If ddlSaleType.Text <> "ALL" Then
        '    WH = WH & " and   TD001='" & ddlSaleType.Text & "' "
        'End If

        WH = WH & Conn_SQL.Where("TD001", cblSaleType)

        If txtSaleNo.Text = "" Then
            If txtCustCode.Text <> "" Then
                WH = WH & " and TC004='" & txtCustCode.Text & "' "
            End If
            If txtPartNo.Text <> "" Then
                WH = WH & " and TD004 like '%" & txtPartNo.Text & "%' "
            End If
            Dim fldDate As String = "TD013"
            If ddlDateType.Text = 0 Then
                fldDate = "TC003"
            End If
            WH = WH & " and  " & fldDate & " between '" & strDate & "' and '" & endDate & "' "
        Else
            WH = WH & " and TC002='" & txtSaleNo.Text & "' "
        End If

        Dim SQL = " insert into " & tableName & "(SaleType,SaleNo,SaleSeq,PartNo,PartSpec,CustID,SaleDate,SaleDelDate,SaleQty,DeliveryQty,statusOrder,IndustryType,SaleForcast,SaleRemark,LeadTime) "
        SQL = SQL & " select TC001,TC002,TD003,TD004,TD006,TC004,TC003,TD013,TD008,TD009,JINPAO80.dbo.COPTC.UDF01,TC012,case when TD015='' then '' else rtrim(TD015)+'-'+TD028 end ,COPTD.TD020,MB036 from JINPAO80.dbo.COPTD " & _
              " LEFT JOIN JINPAO80.dbo.COPTC ON TC001=TD001 and TC002=TD002 " & _
              " LEFT JOIN JINPAO80.dbo.LRPLB ON TD001=LB002 AND TD002=LB003 AND (TD003=LB004 OR LB004='') " & _
              " LEFT JOIN JINPAO80.dbo.PURTB ON TB029=TD001 and TB030=TD002 and TB031=TD003 " & _
              " LEFT JOIN JINPAO80.dbo.INVMB ON MB001=TD004  " & _
              " Where  TD016='N' and TD021='Y' " & WH & " AND LB001 IS NULL  AND TB001 IS NULL "
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Protected Sub createTempSaleNotPlan(TempTable As String)
        CreateTempTable.CreateTempSaleNotPlan(TempTable)
        CreateTempSale(TempTable)
    End Sub

    Protected Sub CreateTempSale1(tableName As String)

        Dim date1 As String = txtDateFrom.Text,date2 As String = txtDateTo.Text
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = "", endDate As String = ""

        'Begin date
        If date1 <> "" Then
            strDate = ConfigDate.dateFormat(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = ConfigDate.dateFormat(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        'insert sal order all 
        Dim WH As String = "  "

        If ddlCodeType.Text = "0" Then
            WH = WH & " and  D.TD004 not like '822%' and D.TD004 not like '823%' and D.TD004 not like '833%'"
        Else
            WH = WH & " and  (D.TD004 like '822%' or D.TD004 like '823%')"
        End If

        'If ddlSaleType.Text <> "ALL" Then
        '    WH = WH & " and   D.TD001='" & ddlSaleType.Text & "' "
        'End If
        WH = WH & Conn_SQL.Where("D.TD001", cblSaleType)

        If txtSaleNo.Text = "" Then
            If txtCustCode.Text <> "" Then
                WH = WH & " and C.TC004='" & txtCustCode.Text & "' "
            End If
            If txtPartNo.Text <> "" Then
                WH = WH & " and D.TD004 like '%" & txtPartNo.Text & "%' "
            End If
            Dim fldDate As String = "D.TD013"
            If ddlDateType.Text = 0 Then
                fldDate = "C.TC003"
            End If
            WH = WH & " and  " & fldDate & " between '" & strDate & "' and '" & endDate & "' "
        Else
            WH = WH & " and C.TC002='" & txtSaleNo.Text & "' "
        End If

        Dim SQL = " insert into " & tableName & "(SaleType,SaleNo,SaleSeq,PartNo,PartSpec,CustID,SaleDate,SaleDelDate,SaleQty,DeliveryQty) " & _
                  " select C.TC001,C.TC002,D.TD003,D.TD004,D.TD006,C.TC004,C.TC003,D.TD013,D.TD008,D.TD009 " & _
                  " from JINPAO80.dbo.COPTD D left join JINPAO80.dbo.COPTC C on C.TC001=D.TD001 and C.TC002=D.TD002 " & _
                  " where D.TD016='N' " & WH
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)

    End Sub

    Protected Sub createTempSaleOverDue(Temptable As String)
        CreateTempTable.CreateTempSaleOverDue(Temptable)
        CreateTempSale1(Temptable)

        Dim SQL As String = "",SSQL As String = "",ASQL As String = ""
        Dim SaleType As String = "", SaleNo As String = "",SaleSeq As String = ""
        Dim MOFinishDate As Date,MOFinishDate2 As String = ""
        Dim SaleDueDate As Date,SaleDueDate2 As String = ""
        Dim overDate As Integer = 0
        'Dim overDate As Integer = 0
        Dim Program As New DataTable, SProgram As New DataTable,SWHR As String = ""
        SQL = "select SaleType,SaleNo,SaleSeq,SaleDelDate from " & Temptable & " "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            SaleType = Program.Rows(i).Item("SaleType")
            SaleNo = Program.Rows(i).Item("SaleNo")
            SaleSeq = Program.Rows(i).Item("SaleSeq")
            SaleDueDate2 = Program.Rows(i).Item("SaleDelDate").ToString.Trim()
            If SaleDueDate2 <> "" Then
                SaleDueDate = DateTime.ParseExact(SaleDueDate2, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            End If
            MOFinishDate2 = ""
            SSQL = " select isnull(MAX(S.TA009),0)as MaxDate from SFCTA S " & _
                   " left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
                   " where M.TA026='" & SaleType & "' " & _
                   "   and M.TA027='" & SaleNo & "' " & _
                   "   and M.TA028='" & SaleSeq & "' " & _
                   " group by M.TA026 order by MaxDate "
            SProgram = Conn_SQL.Get_DataReader(SSQL, Conn_SQL.ERP_ConnectionString)

            For j As Integer = 0 To SProgram.Rows.Count - 1
                MOFinishDate2 = SProgram.Rows(j).Item("MaxDate")
                If MOFinishDate2 <> "" Then
                    MOFinishDate = DateTime.ParseExact(MOFinishDate2, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                End If
            Next
            overDate = 0
            If SaleDueDate2 <> "" And MOFinishDate2 <> "" Then
                overDate = DateDiff(DateInterval.Day, SaleDueDate, MOFinishDate)
            End If
            SWHR = " where SaleType='" & Program.Rows(i).Item("SaleType") & "' " & _
                   "   and SaleNo='" & Program.Rows(i).Item("SaleNo") & "' " & _
                   "   and SaleSeq='" & Program.Rows(i).Item("SaleSeq") & "' "
            If overDate <= 0 Then
                ASQL = " delete from " & Temptable & " "
            Else
                ASQL = " update " & Temptable & " set MOFinishDate='" & MOFinishDate2 & "',OverDate='" & overDate & "' "
            End If
            Conn_SQL.Exec_Sql(ASQL & SWHR, Conn_SQL.MIS_ConnectionString)
        Next
    End Sub

    Private Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowCreated
        Dim val As Integer = ddlShowType.Text
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.CssClass = "header"
            'If e.Row.RowType = ListItemType.Header Then
            Select Case val
                Case 0 'Sale not Plan
                    e.Row.Cells(0).Text = "CUST"
                    e.Row.Cells(1).Text = "Sale Date"
                    e.Row.Cells(1).Width = 100
                    e.Row.Cells(2).Text = "Sale Order"
                    e.Row.Cells(2).Width = 150
                    'e.Row.Cells(2).Width = 10

                    e.Row.Cells(3).Text = "ITEM"
                    e.Row.Cells(3).Width = 150
                    e.Row.Cells(4).Text = "SPEC"
                    e.Row.Cells(4).Width = 200

                    e.Row.Cells(5).Text = "Delivery Date"
                    e.Row.Cells(5).Width = 100
                    e.Row.Cells(6).Text = "Sale QTY"
                    e.Row.Cells(7).Text = "Delivery QTY"
                    e.Row.Cells(8).Text = "Balance QTY"

                    e.Row.Cells(9).Text = "STATUS"
                Case 1 'Over Due Date
                    e.Row.Cells(0).Text = "CUST"
                    e.Row.Cells(1).Text = "Sale Date"
                    e.Row.Cells(1).Width = 100
                    e.Row.Cells(2).Text = "Sale Order"
                    e.Row.Cells(2).Width = 150
                    'e.Row.Cells(2).Width = 10

                    e.Row.Cells(3).Text = "ITEM"
                    e.Row.Cells(3).Width = 150
                    e.Row.Cells(4).Text = "SPEC"
                    e.Row.Cells(4).Width = 200

                    e.Row.Cells(5).Text = "Delivery Date"
                    e.Row.Cells(5).Width = 100
                    e.Row.Cells(6).Text = "Sale QTY"
                    e.Row.Cells(7).Text = "Delivery QTY"
                    e.Row.Cells(8).Text = "Balance QTY"
                    e.Row.Cells(9).Text = "LOT Finish Date"
                    e.Row.Cells(9).Width = 100
                    e.Row.Cells(10).Text = "Over Date"
                Case Else
                    e.Row.Cells(0).Text = "CUST"
                    e.Row.Cells(0).Width = 40

                    e.Row.Cells(1).Text = "LOT"
                    e.Row.Cells(1).Width = 120

                    e.Row.Cells(2).Text = "LOT Date"
                    e.Row.Cells(2).Width = 80
                    'e.Row.Cells(2).Width = 10
                    e.Row.Cells(3).Text = "Plan Finish Date"
                    e.Row.Cells(4).Text = "Sale Order"
                    e.Row.Cells(4).Width = 150
                    e.Row.Cells(5).Text = "Sale Order Date"
                    e.Row.Cells(6).Text = "Sale Order Due Date"

                    e.Row.Cells(7).Text = "ITEM"
                    e.Row.Cells(7).Width = 150

                    e.Row.Cells(8).Text = "SPEC"
                    e.Row.Cells(8).Width = 200

                    e.Row.Cells(9).Text = "Printed Time"
                    'e.Row.Cells(10).Text = "Inputer"
            End Select
            Dim i As Integer
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center
            Next
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "style.backgroundColor='#FFFFC0'")
            If e.Row.RowIndex Mod 2 = 0 Then
                e.Row.Attributes.Add("onmouseout", "style.backgroundColor='#F7F6F3'")
                'e.Row.CssClass = "alternate"
            Else
                e.Row.Attributes.Add("onmouseout", "style.backgroundColor='White'")
                'e.Row.CssClass = "normal"
            End If
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim val As Integer = ddlShowType.Text
        Dim TempTable As String,
            SQL As String = "",
            msg As String = "",
            connectSting As String = Conn_SQL.MIS_ConnectionString
        Select Case val
            Case 0 'Sale not Plan
                msg = "Sale Not Plan"
                TempTable = "TempSaleNotPlan" & Session("UserName")
                createTempSaleNotPlan(TempTable)
                SQL = " select CustID as CUSTOMER,(SUBSTRING(SaleDate,7,2))+'-'+(SUBSTRING(SaleDate,5,2))+'-'+(SUBSTRING(SaleDate,1,4))as SaleDate," & _
                      " SaleType+'-'+SaleNo+'-'+SaleSeq as SaleOrder,PartNo,PartSpec as DESCRIPTION," & _
                      " (SUBSTRING(SaleDelDate,7,2))+'-'+(SUBSTRING(SaleDelDate,5,2))+'-'+(SUBSTRING(SaleDelDate,1,4)) as DeliveryDate," & _
                      " cast(SaleQty as decimal(9,0)) as Qty,cast(DeliveryQty as decimal(9,0)) as DeliveryQty,cast(SaleQty-DeliveryQty as decimal(9,0)) as BalanceQty, " & _
                      " statusOrder as STATUS,IndustryType as 'Industry Type',SaleForcast as 'Sale Forcast',SaleRemark as Remark,cast(LeadTime as decimal(9,0)) as 'Lead Time' from " & TempTable & " order by SaleType,SaleNo,SaleSeq "
            Case 1 'Over Due Date
                msg = "Sale Over Due Date"
                TempTable = "TempSaleOverDue" & Session("UserName")
                createTempSaleOverDue(TempTable)
                SQL = " select CustID as CUST,(SUBSTRING(SaleDate,7,2))+'-'+(SUBSTRING(SaleDate,5,2))+'-'+(SUBSTRING(SaleDate,1,4))as SaleDate," & _
                      " SaleType+'-'+SaleNo+'-'+SaleSeq as SaleOrder,PartNo,PartSpec as DESCRIPTION," & _
                      " (SUBSTRING(SaleDelDate,7,2))+'-'+(SUBSTRING(SaleDelDate,5,2))+'-'+(SUBSTRING(SaleDelDate,1,4)) as DeliveryDate," & _
                      " cast(SaleQty as decimal(9,0)) as Qty,cast(DeliveryQty as decimal(9,0)) as DeliveryQty, " & _
                      " cast(SaleQty-DeliveryQty as decimal(9,0)) as BalanceQty ," & _
                      " case when MOFinishDate='' then '' else (SUBSTRING(MOFinishDate,7,2))+'-'+(SUBSTRING(MOFinishDate,5,2))+'-'+(SUBSTRING(MOFinishDate,1,4)) end as MOFinishDate, " & _
                      " OverDate from " & TempTable & " order by SaleType,SaleNo,SaleSeq "
            Case 2 'Checked Generate LOT
                msg = "MO Not Approve"
                connectSting = Conn_SQL.ERP_ConnectionString
                SQL = LOT(" and TA013='N' ")
            Case 3 'Checked Printed LOT
                msg = "Not Printed LOT"
                connectSting = Conn_SQL.ERP_ConnectionString
                SQL = LOT(" and TA031='0' ")
        End Select
        lblShowText.Text = msg
        ControlForm.ShowGridView(gvShow, SQL, connectSting)
        lblList.Text = gvShow.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        Dim val As Integer = ddlShowType.Text
        Dim TempTable As String, paraName As String
        Select Case val
            Case 0 'Sale not Plan
                TempTable = ("TempSaleNotPlan" & Session("UserName"))
                createTempSaleNotPlan(TempTable)
                paraName = "tableName:" & TempTable
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=SaleNotPlan.rpt&paraName=" & paraName & "');", True)

            Case 1 'Over Due Date
                TempTable = "TempSaleOverDue" & Session("UserName")
                createTempSaleOverDue(TempTable)
                paraName = "tableName:" & TempTable
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=SaleOverDue.rpt&paraName=" & paraName & "');", True)
            Case 2 'Sale not Generate LOT

            Case 3 'not Printed LOT

        End Select
    End Sub
    Protected Function LOT(ByVal whrAdd As String) As String

        Dim SQL As String
        Dim date1 As String = txtDateFrom.Text
        Dim date2 As String = txtDateTo.Text
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = ""
        Dim endDate As String = ""

        Dim xd As String = ""
        Dim xm As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = ConfigDate.dateFormat(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = ConfigDate.dateFormat(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        Dim WH As String = " "
        If ddlCodeType.Text = "0" Then
            WH = WH & " and TA006 not like '822%' and TA006 not like '823%' and TA006 not like '833%' "
        Else
            WH = WH & " and (TA006 like '822%' or TA006 like '823%') "
        End If

        'If ddlSaleType.Text <> "ALL" Then
        '    WH = WH & " and TA026='" & ddlSaleType.Text & "' "
        'End If
        WH = WH & Conn_SQL.Where("TA026", cblSaleType)

        If txtSaleNo.Text = "" Then
            If txtCustCode.Text <> "" Then
                WH = WH & " and TC004='" & txtCustCode.Text & "' "
            End If
            If txtPartNo.Text <> "" Then
                WH = WH & " and TA006 like '%" & txtPartNo.Text & "%' "
            End If
            WH = WH & " and TA003  between '" & strDate & "' and '" & endDate & "' "

        Else
            WH = WH & " and TA027='" & txtSaleNo.Text & "' "
        End If


        SQL = " select TC004 as cust,TA001+'-'+TA002 as LOT, " & _
              " (SUBSTRING(TA003,7,2))+'-'+(SUBSTRING(TA003,5,2))+'-'+(SUBSTRING(TA003,1,4)) as LOT_Date, " & _
              " (SUBSTRING(TA010,7,2))+'-'+(SUBSTRING(TA010,5,2))+'-'+(SUBSTRING(TA010,1,4)) as Finish_Date, " & _
              " TA026+'-'+TA027+'-'+TA028 as SO, " & _
              " (SUBSTRING(TC003,7,2))+'-'+(SUBSTRING(TC003,5,2))+'-'+(SUBSTRING(TC003,1,4)) as SO_Date," & _
              " (SUBSTRING(TD013,7,2))+'-'+(SUBSTRING(TD013,5,2))+'-'+(SUBSTRING(TD013,1,4)) as Due_Date," & _
              " (SUBSTRING(TA006,1,14)+'-'+SUBSTRING(TA006,15,2)) as ITEM,TA035 as SPEC, " & _
              " TA031 as Printed " & _
              " from MOCTA " & _
              " left join COPTC on TC001=TA026 and TC002=TA027 " & _
              " left join COPTD on TD001=TA026 and TD002=TA027 and TD003=TA028 " & _
              " where TA011 not in ('Y','y') " & WH & whrAdd & _
              " order by TA003,TC001,TC002 "
        Return SQL
    End Function

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

End Class