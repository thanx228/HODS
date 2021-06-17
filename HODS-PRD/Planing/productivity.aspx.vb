Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Globalization

Public Class productivity
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "Select MD001,MD001+':'+MD002 as MD002 from CMSMD order by MD001"
            ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            'Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            'pnSearch.Visible = False
            'pnDetail.Visible = False
        End If
    End Sub

    'Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
    '    pnSearch.Visible = True
    '    generateDataDetail()

    '    Dim date1 As String = tbDateFrom.Text, date2 As String = tbDateTo.Text
    '    Dim strDate As String = "", endDate As String = "",SQL As String = ""
    '    'Begin date
    '    If date1 <> "" Then
    '        strDate = configDate.dateFormat(date1)
    '    Else
    '        strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
    '    End If
    '    'End date
    '    If date2 <> "" Then
    '        endDate = configDate.dateFormat(date2)
    '    Else
    '        endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
    '    End If

    '    'Dim tempTableS As String = "tempProductivitySummary" & Session("UserName")
    '    'Dim tempTableD As String = "tempProductivityDetail" & Session("UserName")

    '    Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
    '    Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
    '    Dim dateWork As Short = DateDiff(DateInterval.Day, beginDate, lastDate)

    '    generateProductivitySummary(beginDate, dateWork)
    '    Dim tempTableS As String = "tempProductivitySummary" & Session("UserName")
    '    Dim fldSel As String = "", fldName As String = "", fldShow As String = ""
    '    For i As Integer = 0 To dateWork
    '        Dim addDate1 As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
    '        Dim addDate2 As String = beginDate.AddDays(i).ToString("ddMM", System.Globalization.CultureInfo.InvariantCulture)
    '        'MCH
    '        fldName = "T.mch" & addDate1
    '        fldShow = "MCH" & addDate2
    '        fldSel = fldSel & returnFld(fldName, fldShow)
    '        'MAN
    '        fldName = "T.man" & addDate1
    '        fldShow = "MAN" & addDate2
    '        fldSel = fldSel & returnFld(fldName, fldShow)
    '    Next
    '    SQL = " select T.wc" & fldSel & " from " & tempTableS & " T order by T.wc "
    '    ControlForm.ShowGridView(GridView1, SQL)
    '    lbShowSearch.Text = GridView1.Rows.Count
    'End Sub
    Function returnFld(fldName As String, fldCall As String) As String
        'Return ",(convert(varchar,floor(" & fldName & "/3600)))+':'+(right('0'+convert(varchar,floor((round(" & fldName & "/3600,2)-floor(" & fldName & "/3600))*60)),2)) +((case when " & fldCap & "=0 then '' else(('(')+ convert(varchar,cast(ISNULL(round((round(" & fldName & "/60,4)/" & fldCap & ")*100,2),0) as decimal(9,2))) )+('%)')end)) as " & fldCall
        'Return ",(convert(varchar,floor(" & fldName & "/3600)))+':'+(right('0'+convert(varchar,floor((round(" & fldName & "/3600,2)-floor(" & fldName & "/3600))*60)),2)) as " & fldCall

        Return ",CONVERT(varchar, floor(" & fldName & "/3600)) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),floor((" & fldName & " % 3600) / 60))), 2) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),(" & fldName & " % 60))), 2) as " & fldCall
    End Function
    'Protected Sub btDetail_Click(sender As Object, e As EventArgs) Handles btDetail.Click
    '    pnDetail.Visible = True
    '    generateDataDetail()
    '    Dim tempTableD As String = "tempProductivityDetail" & Session("UserName")
    '    Dim SQL As String, WHR As String
    '    WHR = returnFld("manTimeUsage", "manTime")
    '    WHR = WHR & returnFld("mchTimeUsage", "mchTime")

    '    SQL = " select wc as WC,docDetail,(SUBSTRING(docDate,7,2))+'-'+(SUBSTRING(docDate,5,2))+'-'+(SUBSTRING(docDate,1,4)) as DeliveryDate," & _
    '          " woDetail,partNo,partSpec,cast(qty as decimal(9,0)) as Qty" & WHR & _
    '          " from " & tempTableD & " order by wc,docDate"
    '    ControlForm.ShowGridView(GridView2, SQL)
    '    lbShowDetail.Text = GridView2.Rows.Count
    'End Sub

    Protected Sub generateDataDetail()
        Dim tempTable As String = "tempProductivityDetail" & Session("UserName")
        CreateTempTable.createTempProductivityDetail(tempTable)
        Dim SQL As String = ""
        Dim ISQL As String = ""
        Dim WHR As String = ""
        Dim WC As String = ddlWC.Text
        If WC <> "" And WC <> "ALL" Then
            WHR = WHR & " and SFCTB.TB005='" & WC & "'"
        Else
            WHR = WHR & " and SFCTB.TB005 in (select CMSMD.MD001 from JINPAO80.dbo.CMSMD CMSMD )"
        End If
        Dim date1 As String = tbDateFrom.Text
        Dim date2 As String = tbDateTo.Text

        Dim strDate As String = ""
        Dim endDate As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat2(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat2(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        SQL = " select distinct SFCTB.TB001+'-'+SFCTB.TB002+'-'+SFCTC.TC003,SFCTB.TB003,SFCTB.TB005,SFCTC.TC006," & _
              "        SFCTC.TC004+'-'+SFCTC.TC005+'-'+SFCTC.TC006,SFCTC.TC047,SFCTC.TC049,SFCTC.TC036, " & _
              "        case when MOCTA.TA015=0 then 0 else floor(SFCTA.TA022/MOCTA.TA015) end,case when MOCTA.TA015=0 then 0 else floor(SFCTA.TA035/MOCTA.TA015) end " & _
              " from JINPAO80.dbo.SFCTC SFCTC " & _
              " left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
              " left join JINPAO80.dbo.SFCTA SFCTA on SFCTA.TA001=SFCTC.TC004 and SFCTA.TA002=SFCTC.TC005  and SFCTA.TA003=SFCTC.TC006 and SFCTA.TA006=SFCTB.TB005 " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " & _
              " where SFCTB.TB013='Y' " & WHR & " and SFCTB.TB003 BETWEEN '" & strDate & "' and '" & endDate & "' " & _
              " and SFCTB.TB001 in (select CMSMQ.MQ001 from JINPAO80.dbo.CMSMQ CMSMQ where CMSMQ.MQ003 not like 'D4%' and CMSMQ.MQ003 not like 'D1%' and CMSMQ.MQ003 like 'D%') "
        ISQL = "insert into " & tempTable & "(docDetail,docDate,wc,itemSeq,woDetail,partNo,partSpec,qty,manStd,mchStd) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

    End Sub
    Protected Sub generateDataDetail2()
        Dim tempTable As String = "tempProductivityDetail" & Session("UserName")
        CreateTempTable.createTempProductivityDetail(tempTable)
        Dim SQL As String = ""
        Dim ISQL As String = ""
        Dim WHR As String = ""
        Dim WC As String = ddlWC.Text
        If WC <> "" And WC <> "ALL" Then
            WHR = WHR & " and SFCTB.TB005='" & WC & "'"
        Else
            WHR = WHR & " and SFCTB.TB005 in (select CMSMD.MD001 from JINPAO80.dbo.CMSMD CMSMD )"
        End If
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
        'SQL = " select distinct SFCTB.TB001+'-'+SFCTB.TB002+'-'+SFCTC.TC003,SFCTB.TB003,SFCTB.TB005," & _
        '      "        SFCTC.TC004+'-'+SFCTC.TC005+'-'+SFCTC.TC006,SFCTC.TC047,SFCTC.TC049,SFCTC.TC036," & _
        '      "        BOMMF.MF009+(SFCTC.TC036*BOMMF.MF010),BOMMF.MF024+(SFCTC.TC036*BOMMF.MF025)  " & _
        '      " from JINPAO80.dbo.SFCTC SFCTC " & _
        '      " left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
        '      " left join JINPAO80.dbo.BOMMF BOMMF on BOMMF.MF001=SFCTC.TC047 and BOMMF.MF003=SFCTC.TC006 and BOMMF.MF006=SFCTB.TB005 " & _
        '      " where SFCTB.TB013='Y' " & WHR & " and SFCTB.TB003 BETWEEN '" & strDate & "' and '" & endDate & "' " & _
        '      " and BOMMF.MF005='1' and SFCTB.TB001 in (select CMSMQ.MQ001 from JINPAO80.dbo.CMSMQ CMSMQ where CMSMQ.MQ003 not like 'D4%' and CMSMQ.MQ003 not like 'D1%' and CMSMQ.MQ003 like 'D%')"

        SQL = " select distinct SFCTB.TB001+'-'+SFCTB.TB002+'-'+SFCTC.TC003,SFCTB.TB003,SFCTB.TB005,SFCTC.TC006," & _
              "        SFCTC.TC004+'-'+SFCTC.TC005+'-'+SFCTC.TC006,SFCTC.TC047,SFCTC.TC049,SFCTC.TC036 " & _
              "        case when MOCTA.TA015=0 then 0 else floor(SFCTA.TA022/MOCTA.TA015) end,case when MOCTA.TA015=0 then 0 else floor(SFCTA.TA035/MOCTA.TA015) end " & _
              " from JINPAO80.dbo.SFCTC SFCTC " & _
              " left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
              " left join JINPAO80.dbo.SFCTA SFCTA on SFCTA.TA001=SFCTC.TC004 and SFCTA.TA002=SFCTC.TC005  and SFCTA.TA003=SFCTC.TC006 " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " & _
              " where SFCTB.TB013='Y' " & WHR & " and SFCTB.TB003 BETWEEN '" & strDate & "' and '" & endDate & "' " & _
              " and SFCTB.TB001 in (select CMSMQ.MQ001 from JINPAO80.dbo.CMSMQ CMSMQ where CMSMQ.MQ003 not like 'D4%' and CMSMQ.MQ003 not like 'D1%' and CMSMQ.MQ003 like 'D%') "
        ISQL = "insert into " & tempTable & "(docDetail,docDate,wc,itemSeq,woDetail,partNo,partSpec,qty,manStd,mchStd) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        ',manTimeUsage,mchTimeUsage
        'BOMMF.MF009+(SFCTC.TC036*BOMMF.MF010),BOMMF.MF024+(SFCTC.TC036*BOMMF.MF025) 
        'left join JINPAO80.dbo.BOMMF BOMMF on BOMMF.MF001=SFCTC.TC047 and BOMMF.MF003=SFCTC.TC006 and BOMMF.MF006=SFCTB.TB005
        'SQL = " select docDetail,itemSeq,partNo,qty from " & tempTable & " order by partNo "
        'Dim SQL1 As String = "", USQL As String = ""
        'Dim lastPartNo As String = "", PartNo As String = ""
        'Dim lastSeq As String = "", seq As String = ""
        'Dim Program As New DataTable,Program1 As New DataTable
        'Dim manSet As Integer = 0, manWork As Integer = 0
        'Dim mchSet As Integer = 0, mchWork As Integer = 0
        'Dim docDeteail As String = "", qty As Integer = 0
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        'For i As Integer = 0 To Program.Rows.Count - 1
        '    With Program.Rows(i)
        '        PartNo = .Item("partNo")
        '        seq = .Item("itemSeq")
        '        docDeteail = .Item("docDetail")
        '        qty = .Item("qty")
        '        If lastPartNo <> PartNo Or lastSeq <> seq Then
        '            manSet = 0
        '            manWork = 0
        '            mchSet = 0
        '            mchWork = 0
        '            SQL1 = "select MF009 as manSet,MF010 as manWork,MF024 as mchSet,MF025 as mchWork from BOMMF where MF001='" & PartNo & "' and MF003='" & seq & "' and MF005='1'  "
        '            Program1 = Conn_SQL.Get_DataReader(SQL1, Conn_SQL.ERP_ConnectionString)
        '            If Program1.Rows.Count > 0 Then
        '                With Program1.Rows(0)
        '                    manSet = .Item("manSet")
        '                    manWork = .Item("manWork")
        '                    mchSet = .Item("mchSet")
        '                    mchWork = .Item("mchWork")
        '                End With
        '            End If
        '        End If
        '        If manSet <> 0 Or manWork <> 0 Or mchSet <> 0 Or mchWork <> 0 Then
        '            Dim manTimeUsage As Integer = manSet + (qty * manWork)
        '            Dim mchTimeUsage As Integer = mchSet + (qty * mchWork)
        '            USQL = "update " & tempTable & " set manTimeUsage='" & manTimeUsage & "',mchTimeUsage='" & mchTimeUsage & "' where docDetail='" & docDeteail & "' "
        '            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        '        End If
        '        lastPartNo = PartNo
        '        lastSeq = seq
        '    End With
        'Next

    End Sub
    Private Sub generateProductivitySummary(beginDate As Date, dateWork As Integer)
        Dim SQL As String = ""
        Dim tempTableS As String = "tempProductivitySummary" & Session("UserName")
        Dim tempTableD As String = "tempProductivityDetail" & Session("UserName")

        CreateTempTable.createTempProductivitySummary(tempTableS, beginDate, dateWork)
        SQL = "select docDate,wc,sum(qty*manStd)as sman,sum(qty*mchStd) as smch from " & tempTableD & " group by wc,docDate order by wc,docDate"
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Dim SQLA As String = "", SQLB As String = "", lstWC As String = "", SQLALL As String = ""

        For i As Integer = 0 To Program.Rows.Count - 1
            If lstWC <> Program.Rows(i).Item("wc") Then
                If lstWC <> "" Then
                    SQLALL = SQLA & ") " & SQLB & ")"
                    Conn_SQL.Exec_Sql(SQLALL, Conn_SQL.MIS_ConnectionString)
                End If
                SQLA = " Insert into " & tempTableS & "(wc"
                SQLB = " VALUES('" & Program.Rows(i).Item("wc") & "'"
            End If
            SQLA = SQLA & "," & "man" & Program.Rows(i).Item("docDate") & ",mch" & Program.Rows(i).Item("docDate")
            SQLB = SQLB & ",'" & Program.Rows(i).Item("sman") & "','" & Program.Rows(i).Item("smch") & "'"
            lstWC = Program.Rows(i).Item("wc")
        Next
        If SQLA <> "" Then
            SQLALL = SQLA & ") " & SQLB & ")"
            Conn_SQL.Exec_Sql(SQLALL, Conn_SQL.MIS_ConnectionString)
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        
        generateDataDetail()
        Dim tempTableS As String = "tempProductivitySummary" & Session("UserName")
        Dim tempTableD As String = "tempProductivityDetail" & Session("UserName")
        Dim SQL As String = "", WHR As String
       
        Dim showType As String = ddlShowType.SelectedValue.ToString
        Select Case showType
            Case "0" 'detail
                WHR = returnFld("(qty*manStd)", "'Labor Time Usage'")
                WHR = WHR & returnFld("(qty*mchStd)", "'Machine Time Usage'")

                SQL = " select wc as WC,docDetail,(SUBSTRING(docDate,7,2))+'-'+(SUBSTRING(docDate,5,2))+'-'+(SUBSTRING(docDate,1,4)) as DeliveryDate," & _
                      " woDetail,partNo,partSpec,cast(qty as decimal(9,0)) as Qty" & WHR & _
                      " from " & tempTableD & " order by wc,docDate"
            Case "1" 'Summary
                Dim date1 As String = tbDateFrom.Text, date2 As String = tbDateTo.Text
                Dim strDate As String = "", endDate As String = ""
                'Begin date
                If date1 <> "" Then
                    strDate = configDate.dateFormat2(date1)
                Else
                    strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
                End If
                'End date
                If date2 <> "" Then
                    endDate = configDate.dateFormat2(date2)
                Else
                    endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
                End If
                Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                Dim dateWork As Short = DateDiff(DateInterval.Day, beginDate, lastDate)

                generateProductivitySummary(beginDate, dateWork)

                Dim fldSel As String = "", fldName As String = "", fldShow As String = ""
                For i As Integer = 0 To dateWork
                    Dim addDate1 As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                    Dim addDate2 As String = beginDate.AddDays(i).ToString("ddMM", System.Globalization.CultureInfo.InvariantCulture)
                    'MCH
                    fldName = "T.mch" & addDate1
                    fldShow = "MCH" & addDate2
                    fldSel = fldSel & returnFld(fldName, fldShow)
                    'MAN
                    fldName = "T.man" & addDate1
                    fldShow = "MAN" & addDate2
                    fldSel = fldSel & returnFld(fldName, fldShow)
                Next
                SQL = " select T.wc" & fldSel & " from " & tempTableS & " T order by T.wc "
        End Select
        ControlForm.ShowGridView(gvShow, SQL)
        lbShow.Text = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class