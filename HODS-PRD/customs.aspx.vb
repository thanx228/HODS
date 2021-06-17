Imports System.IO
Public Class customs
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    'Dim ExportUtil As New ExportUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='61' and MQ001 in ('EX','LC') order by MQ002"
            ControlForm.showDDL(ddlInvType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)
            'If Session("UserName") = "" Then
            '    Response.Redirect(Server.UrlPathEncode("/Login.aspx"))
            'End If
            btExcel.Visible = False
            btPrint.Visible = False
            btPrintDel.Visible = False
            btSelect.Visible = False
            btSelectTH.Visible = False
            btPrintChin.Visible = False
            btPrintFisher.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click

        Dim WHR As String = ""
        If ddlInvType.Text <> "" Then
            WHR = WHR & " and TA001 ='" & ddlInvType.Text & "'"
        End If

        If tbInvNo.Text <> "" Then
            WHR = WHR & " and TA002 ='" & tbInvNo.Text & "'"
        End If

        Dim date1 As String = tbDateFrom.Text
        Dim date2 As String = tbDateTo.Text
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = ""
        Dim endDate As String = ""

        Dim xd As String = ""
        Dim xm As String = ""
        'Begin date
        If date1 <> "" Then
            Dim temp1() As String = date1.Split("/")
            xd = temp1(1)
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = temp1(0)
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            strDate = temp1(2) & xm & xd
        End If
        'End date
        If date2 <> "" Then
            Dim temp1() As String = date2.Split("/")
            xd = temp1(1)
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = temp1(0)
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            endDate = temp1(2) & xm & xd
        End If
        If date1 <> "" Or date2 <> "" Then
            If date1 <> "" And date2 <> "" Then
                WHR = WHR & " and  TA038 between '" & strDate & "' and '" & endDate & "' "
            Else
                Dim xdate As String = strDate
                If xdate = "" Then
                    xdate = endDate
                End If
                WHR = WHR & " and  TA038 ='" & xdate & "' "
            End If
        End If
        Dim SQL As String = "select TA001 as InvType,TA002 as InvNo," & _
                            " (SUBSTRING(TA038,7,2))+'-'+(SUBSTRING(TA038,5,2))+'-'+(SUBSTRING(TA038,1,4)) as InvDate," & _
                            " TA004 as Cust,TA008 as CustName from [ACRTA] where TA004 ='" & tbCust.Text & "' " & WHR & " order by TA038 DESC " 'TA025='Y' 
        SqlDataSource1.SelectCommand = SQL
        SqlDataSource1.DataBind()
        'GridView1.DataSource = SqlDataSource1
        GridView1.DataBind()
        btSelect.Visible = True
        btSelectTH.Visible = True
    End Sub

    Protected Sub btSelect_Click(sender As Object, e As EventArgs) Handles btSelect.Click

        Dim tableName As String = "TempCustoms" & Session("UserName")
        CreateTempTable.createTempCustoms(tableName)
        Dim i As Integer = 0
        Dim data(8) As String
        Dim invType, invNo As String
        Dim SQL As String = ""
        Dim Program As New Data.DataTable
        Dim chkSelect As WebControls.CheckBox
        Dim seq As Integer = 1
        Dim type As String = ""

        Select Case ddlType.Text
            Case "Computer"
                type = type & " (ชิ้นส่วนคอมฯ)" & "'" & ")"
            Case "Appliances"
                type = type & " (ชิ้นส่วนประกอบตู้เย็น)" & "'" & ")"
        End Select
        For i = 0 To GridView1.Rows.Count - 1
            chkSelect = GridView1.Rows(i).FindControl("cbSelect")
            If chkSelect.Checked = True Then
                invType = GridView1.Rows(i).Cells(1).Text.ToString
                invNo = GridView1.Rows(i).Cells(2).Text.ToString
                SQL = "select ROUND(UDF51,0) as PACK, UDF52 as Weight,ROUND(TB022,0) as QTY,TB017+TB018 as AMT,TB048 as PO,(TB001)+(TB002) as INV,TB040 as NAME from ACRTB where TB001='" & invType & "' and TB002='" & invNo & "' order by TB003 "
                Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                For j As Integer = 0 To Program.Rows.Count - 1
                    Dim ISQL = " insert into " & tableName & "(seq,pack,wgh,qty,amt,po,inv,note) values " & _
                               " ('" & seq & "','" & Program.Rows(j).Item("PACK") & "','" & Program.Rows(j).Item("Weight") & "','" & Program.Rows(j).Item("QTY") & "','" & Program.Rows(j).Item("AMT") & "'," & _
                               "'" & Program.Rows(j).Item("PO") & "','" & Program.Rows(j).Item("INV") & "',N'" & Program.Rows(j).Item("NAME") & type
                    Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
                    seq += 1
                Next
            End If
        Next
        SQL = "select seq as ITEM,pack as PACK,wgh as Weight,qty as QTY,amt as AMT,po as PO,inv as INV,note as REMARK from " & tableName & " order by seq"
        ControlForm.ShowGridView(GridView2, SQL)

        btPrint.Visible = True
        btPrintDel.Visible = True
        btPrintChin.Visible = True
        btPrintFisher.Visible = True
        btExcel.Visible = True
    End Sub

    Protected Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        ExportGridView("Customs_Export", GridView2)
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

    Protected Sub btSelectTH_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSelectTH.Click
        Dim tableName As String = "TempCustoms" & Session("UserName")
        CreateTempTable.createTempCustoms(tableName)
        Dim i As Integer = 0
        Dim data(8) As String
        Dim invType, invNo As String
        Dim SQL As String = ""
        Dim Program As New Data.DataTable
        Dim chkSelect As WebControls.CheckBox
        Dim seq As Integer = 1
        Dim type As String = ""
        Select Case ddlType.Text
            Case "Computer"
                type = type & " (ชิ้นส่วนคอมฯ)" & "'" & ")"
            Case "Appliances"
                type = type & " (ชิ้นส่วนประกอบตู้เย็น)" & "'" & ")"
        End Select

        For i = 0 To GridView1.Rows.Count - 1
            chkSelect = GridView1.Rows(i).FindControl("cbSelect")
            If chkSelect.Checked = True Then
                invType = GridView1.Rows(i).Cells(1).Text.ToString
                invNo = GridView1.Rows(i).Cells(2).Text.ToString
                SQL = "select UDF51 as PACK, UDF52 as Weight,TB022 as QTY,TB019+TB020 as AMT,TB048 as PO,(TB001)+(TB002) as INV,TB040 as NAME from ACRTB where TB001='" & invType & "' and TB002='" & invNo & "' order by TB003 "
                Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                For j As Integer = 0 To Program.Rows.Count - 1
                    Dim ISQL = " insert into " & tableName & "(seq,pack,wgh,qty,amt,po,inv,note) values " & _
                               " ('" & seq & "','" & Program.Rows(j).Item("PACK") & "','" & Program.Rows(j).Item("Weight") & "','" & Program.Rows(j).Item("QTY") & "','" & Program.Rows(j).Item("AMT") & "'," & _
                               "'" & Program.Rows(j).Item("PO") & "','" & Program.Rows(j).Item("INV") & "',N'" & Program.Rows(j).Item("NAME") & type
                    Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
                    seq += 1
                Next
            End If
        Next
        SQL = "select seq as ITEM,pack as PACK,wgh as Weight,qty as QTY,amt as AMT,po as PO,inv as INV,note as REMARK from " & tableName & " order by seq"
        ControlForm.ShowGridView(GridView2, SQL)

        btPrint.Visible = True
        btPrintDel.Visible = True
        btPrintChin.Visible = True
        btPrintFisher.Visible = True
        btExcel.Visible = True
    End Sub

    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrint.Click

        Dim tableName As String = "TempCustoms" & Session("UserName")
        Dim paraName As String = ""
        paraName = "tableName:" & tableName
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=Customs.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)

    End Sub

    Protected Sub btPrintDel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrintDel.Click
        Dim tableName As String = "TempCustoms" & Session("UserName")
        Dim paraName As String = ""
        paraName = "tableName:" & tableName
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=CustomsDelta.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)

    End Sub

    Protected Sub btPrintChin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrintChin.Click
        Dim tableName As String = "TempCustoms" & Session("UserName")
        Dim paraName As String = ""
        paraName = "tableName:" & tableName
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=CustomsChin.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)

    End Sub

    Protected Sub btPrintFisher_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrintFisher.Click
        Dim tableName As String = "TempCustoms" & Session("UserName")
        Dim paraName As String = ""
        paraName = "tableName:" & tableName
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=CustomsFisher.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub
End Class