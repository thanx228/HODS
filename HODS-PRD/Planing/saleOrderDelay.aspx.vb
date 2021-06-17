Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Globalization


Public Class saleOrderDelay
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Protected Sub btDelayMO_Click(sender As Object, e As EventArgs) Handles btDelayMO.Click
        Dim date1 As String = tbDateFrom.Text
        Dim date2 As String = tbDateTo.Text

        Dim strDate As String = ""
        Dim endDate As String = ""

        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        Dim D01 As Integer = 0,D08 As Integer = 0
        Dim D15 As Integer = 0,D21 As Integer = 0
        'and TA001='5102' and TA002='20111212071' 
        Dim SQL As String = ""
        SQL = " select TA001,TA002,TD013 from COPTD left join MOCTA on TA026=TD001 and TA027=TD002 and TA028=TD003 " & _
              " where TA001 is not null and TD013 between '" & strDate & "' and '" & endDate & "' order by TD013 "
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim MType As String = Program.Rows(i).Item("TA001")
            Dim MNo As String = Program.Rows(i).Item("TA002")
            Dim DeliveryDate As Date = DateTime.ParseExact(Program.Rows(i).Item("TD013"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim Program1 As New DataTable
            Dim SQL1 As String = ""
            SQL1 = "select TOP 1 TA009 from SFCTA where TA001='" & MType & "' and TA002='" & MNo & "' and TA009!='' order by TA003 desc"
            Program1 = Conn_SQL.Get_DataReader(SQL1, Conn_SQL.ERP_ConnectionString)
            If Program1.Rows.Count > 0 Then
                Dim PlanDate As Date = DateTime.ParseExact(Program1.Rows(0).Item("TA009"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

                Dim DateCompare As Integer = Date.Compare(DeliveryDate, PlanDate)
                If DateCompare < 1 Then
                    Dim dateDelay As Integer = DateDiff(DateInterval.Day, DeliveryDate, PlanDate)
                    If dateDelay > 0 Then
                        If dateDelay < 8 Then
                            D01 += 1
                        ElseIf dateDelay < 15 Then
                            D08 += 1
                        ElseIf dateDelay < 21 Then
                            D15 += 1
                        Else
                            D21 += 1
                        End If
                    End If
                End If
            End If
        Next
        Dim Percent As Double = 0
        Dim dt As Data.DataTable = New DataTable
        dt.Columns.Add(New DataColumn("Condition"))
        dt.Columns.Add(New DataColumn("Count"))
        dt.Columns.Add(New DataColumn("Percent"))
        Dim AllMo As Integer = Program.Rows.Count

        Dim dr As DataRow = dt.NewRow()
        dr("Condition") = "ALL MO"
        dr("Count") = AllMo
        dr("Percent") = ""
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Condition") = "Delay 1 - 7 "
        dr("Count") = D01
        dr("Percent") = CalPercent(D01, AllMo)
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Condition") = "Delay 8 - 14 "
        dr("Count") = D08
        dr("Percent") = CalPercent(D08, AllMo)
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Condition") = "Delay 15 - 21 "
        dr("Count") = D15
        dr("Percent") = CalPercent(D15, AllMo)
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Condition") = "Delay Over 21 "
        dr("Count") = D21
        dr("Percent") = CalPercent(D21, AllMo)
        dt.Rows.Add(dr)

        Dim allDelay As Integer = D01 + D08 + D15 + D21
        dr = dt.NewRow()
        dr("Condition") = "Delay All "
        dr("Count") = allDelay
        dr("Percent") = CalPercent(allDelay, AllMo)
        dt.Rows.Add(dr)

        GridView1.DataSource = dt
        GridView1.DataBind()
        lbShowText.Text = "Delay MO"

        Dim listData = New Hashtable
        listData.Add("0", " All Delay ")
        listData.Add("1", " Delay 1 - 7 ")
        listData.Add("2", " Delay 8 - 14 ")
        listData.Add("3", " Delay 15 - 21 ")
        listData.Add("4", " Delay Over 21 ")
        ddlCondition.DataSource = listData
        ddlCondition.DataValueField = "Key"
        ddlCondition.DataTextField = "Value"
        ddlCondition.DataBind()
        listData.Clear()

        Panel1.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False
        GridView2.Visible = False
    End Sub
    Function CalPercent(Delay As Integer, DelayAll As Integer) As Double
        Dim Percent As Double = 0
        If Delay > 0 Then
            Percent = (Delay / DelayAll) * 100
        End If
        Return Math.Round(Percent, 2)
    End Function

    Protected Sub btNotIssueSO_Click(sender As Object, e As EventArgs) Handles btNotIssueSO.Click

        Dim date1 As String = tbDateFrom.Text
        Dim date2 As String = tbDateTo.Text
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = ""
        Dim endDate As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat(date2)
        Else
            Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            endDate = DateAdd(DateInterval.Day, 14, beginDate).ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        Dim SQL As String = ""
        SQL = " select TD013 from COPTD where  TD013 between '" & strDate & "' and '" & endDate & "' order by TD013 "

        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim allSO As Integer = Program.Rows.Count
        'SQL = " select TA001,TA002,TD013 from COPTD left join MOCTA on TA026=TD001 and TA027=TD002 and TA028=TD003 " & _
        '      " where TA001 is null and TD013 between '" & strDate & "' and '" & endDate & "' order by TD013 "

        SQL = " select TC001,TC002,TD003,TD004,TD006,TC004,TC003,TD013,TD008,TD009 from JINPAO80.dbo.COPTD " & _
        " LEFT JOIN JINPAO80.dbo.COPTC ON TC001=TD001 and TC002=TD002 " & _
        " LEFT JOIN JINPAO80.dbo.LRPLB ON TD001=LB002 AND TD002=LB003 AND (TD003=LB004 OR LB004='') " & _
        " LEFT JOIN JINPAO80.dbo.PURTB ON TB029=TD001 and TB030=TD002 and TB031=TD003 " & _
        " Where  TD016='N' AND TD021='Y' AND LB001 IS NULL  AND TB001 IS NULL " & _
        " AND TD013 between '" & strDate & "' AND '" & endDate & "' "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim saleNotMO As Integer = Program.Rows.Count

        Dim dt As Data.DataTable = New DataTable
        dt.Columns.Add(New DataColumn("Condition"))
        dt.Columns.Add(New DataColumn("Count"))
        dt.Columns.Add(New DataColumn("Percent"))
 
        Dim dr As DataRow = dt.NewRow()
        dr("Condition") = "ALL SO"
        dr("Count") = allSO
        dr("Percent") = ""
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Condition") = "SO Not Issue MO"
        dr("Count") = saleNotMO
        dr("Percent") = CalPercent(saleNotMO, allSO)
        dt.Rows.Add(dr)

        GridView1.DataSource = dt
        GridView1.DataBind()
        lbShowText.Text = "Not Issue MO"
        Panel1.Visible = False
        Panel2.Visible = True
        GridView2.Visible = False
        Panel3.Visible = False
    End Sub
    Protected Sub btNotIssueDetail_Click(sender As Object, e As EventArgs) Handles btNotIssueDetail.Click
        GridView2.Visible = True
        Panel3.Visible = True

        Dim date1 As String = tbDateFrom.Text
        Dim date2 As String = tbDateTo.Text
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = ""
        Dim endDate As String = ""
 
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat(date2)
        Else
            Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            endDate = DateAdd(DateInterval.Day, 14, beginDate).ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        Dim SQL As String = ""
        'SQL = " select TD001 as OrderType,TD002 as OrderNo,cast(TD003 as decimal(9,0)) as OrderSeq,TD004 as PartNo,TD006 as Spec," & _
        '      " cast(TD008 as decimal(9,0)) as OrderQty,(SUBSTRING(TD013,7,2))+'-'+(SUBSTRING(TD013,5,2))+'-'+(SUBSTRING(TD013,1,4))as DeliveryDate from COPTD left join MOCTA on TA026=TD001 and TA027=TD002 and TA028=TD003 " & _
        '      " where TA001 is null and TD013 between '" & strDate & "' and '" & endDate & "' order by TD013 "

        SQL = " select TD001 as OrderType,TD002 as OrderNo,cast(TD003 as decimal(9,0)) as OrderSeq,TD004 as PartNo,TD006 as Spec," & _
             " cast(TD008 as decimal(9,0)) as OrderQty,(SUBSTRING(TD013,7,2))+'-'+(SUBSTRING(TD013,5,2))+'-'+(SUBSTRING(TD013,1,4))as DeliveryDate from JINPAO80.dbo.COPTD  " & _
             " LEFT JOIN JINPAO80.dbo.COPTC ON TC001=TD001 and TC002=TD002 " & _
             " LEFT JOIN JINPAO80.dbo.LRPLB ON TD001=LB002 AND TD002=LB003 AND (TD003=LB004 OR LB004='') " & _
             " LEFT JOIN JINPAO80.dbo.PURTB ON TB029=TD001 and TB030=TD002 and TB031=TD003 " & _
             " Where  TD016='N' AND TD021='Y' AND LB001 IS NULL  AND TB001 IS NULL " & _
             " AND TD013 between '" & strDate & "' AND '" & endDate & "' order by TD013,TD001,TD002"


        ControlForm.ShowGridView(GridView2, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountRow.Text = "    " & GridView2.Rows.Count & "   "
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btDetailDelay_Click(sender As Object, e As EventArgs) Handles btDetailDelay.Click
        GridView2.Visible = True
        Panel3.Visible = True

        Dim date1 As String = tbDateFrom.Text,date2 As String = tbDateTo.Text

        Dim strDate As String = "", endDate As String = ""

        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        Dim D01 As Integer = 0, D08 As Integer = 0,D15 As Integer = 0,D21 As Integer = 0,SQL As String = ""

        Dim dt As Data.DataTable = New DataTable
        dt.Columns.Add(New DataColumn("SaleType"))
        dt.Columns.Add(New DataColumn("SaleNo"))
        dt.Columns.Add(New DataColumn("WorkType"))
        dt.Columns.Add(New DataColumn("WorkNo"))
        dt.Columns.Add(New DataColumn("PartNo"))
        dt.Columns.Add(New DataColumn("Spec"))
        dt.Columns.Add(New DataColumn("WorkQty"))
        dt.Columns.Add(New DataColumn("DeliveryDate"))
        dt.Columns.Add(New DataColumn("WorkFinishDate"))
        dt.Columns.Add(New DataColumn("DelayDate"))

        Dim condition As String = ddlCondition.Text

        SQL = " select TD001,TD002,TD004,TD006,TA001,TA002,TA015,TD013,(SUBSTRING(TD013,7,2))+'-'+(SUBSTRING(TD013,5,2))+'-'+(SUBSTRING(TD013,1,4))as DelDate from COPTD left join MOCTA on TA026=TD001 and TA027=TD002 and TA028=TD003 " & _
              " where TA001 is not null and TD013 between '" & strDate & "' and '" & endDate & "' order by TD013 "
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim MType As String = Program.Rows(i).Item("TA001")
            Dim MNo As String = Program.Rows(i).Item("TA002")
            Dim DeliveryDate As Date = DateTime.ParseExact(Program.Rows(i).Item("TD013"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim Program1 As New DataTable
            Dim SQL1 As String = ""
            SQL1 = "select TOP 1 TA009,(SUBSTRING(TA009,7,2))+'-'+(SUBSTRING(TA009,5,2))+'-'+(SUBSTRING(TA009,1,4))as FinishDate from SFCTA where TA001='" & MType & "' and TA002='" & MNo & "' and TA009!='' order by TA003 desc"
            Program1 = Conn_SQL.Get_DataReader(SQL1, Conn_SQL.ERP_ConnectionString)
            If Program1.Rows.Count > 0 Then
                Dim PlanDate As Date = DateTime.ParseExact(Program1.Rows(0).Item("TA009"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                Dim DateCompare As Integer = Date.Compare(DeliveryDate, PlanDate)
                If DateCompare < 1 Then
                    Dim SelectCondition As String = 0
                    Dim dateDelay As Integer = DateDiff(DateInterval.Day, DeliveryDate, PlanDate)
                    If dateDelay > 0 Then
                        If dateDelay < 8 Then
                            SelectCondition = 1
                        ElseIf dateDelay < 15 Then
                            SelectCondition = 2
                        ElseIf dateDelay < 21 Then
                            SelectCondition = 3
                        Else
                            SelectCondition = 4
                        End If
                    End If
                    Dim showGrid As Boolean = False
                    If condition = 0 Then
                        If dateDelay > 0 Then
                            showGrid = True
                        End If
                    Else
                        If condition = SelectCondition Then
                            showGrid = True
                        End If
                    End If
                    If showGrid = True Then
                        Dim dr As DataRow = dt.NewRow()
                        dr("SaleType") = Program.Rows(i).Item("TD001")
                        dr("SaleNo") = Program.Rows(i).Item("TD002")
                        dr("WorkType") = Program.Rows(i).Item("TA001")
                        dr("WorkNo") = Program.Rows(i).Item("TA002")
                        dr("PartNo") = Program.Rows(i).Item("TD004")
                        dr("Spec") = Program.Rows(i).Item("TD006")
                        dr("WorkQty") = CDbl(Program.Rows(i).Item("TA015")).ToString("N")
                        dr("DeliveryDate") = Program.Rows(i).Item("DelDate")
                        dr("WorkFinishDate") = Program1.Rows(0).Item("FinishDate")
                        dr("DelayDate") = dateDelay
                        dt.Rows.Add(dr)
                    End If
                End If
            End If
        Next
        GridView2.DataSource = dt
        GridView2.DataBind()
        lbCountRow.Text = GridView2.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        ExportGridView("Detail_Export", GridView2)
    End Sub

    Public Shared Sub ExportGridView(ByVal FileName As String, ByVal gv As GridView)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>")
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + _
        HttpContext.Current.Server.UrlEncode(FileName) & ".xls")
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        Dim StringWriter As New System.IO.StringWriter
        Dim HtmlTextWriter As New HtmlTextWriter(StringWriter)
        gv.AllowSorting = False
        gv.AllowPaging = False
        gv.EnableViewState = False
        gv.AutoGenerateColumns = False
        gv.RenderControl(HtmlTextWriter)
        HttpContext.Current.Response.Write(StringWriter.ToString())
        HttpContext.Current.Response.End()
    End Sub
End Class