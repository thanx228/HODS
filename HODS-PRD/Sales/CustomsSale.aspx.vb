Imports System.IO

Public Class CustomsSale
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    'Dim ExportUtil As New ExportUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='61' and MQ001 in ('HT') order by MQ002"
            ControlForm.showDDL(ddlInvType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("/Login.aspx"))
            End If
            btExcel.Visible = False
            btPrintDel.Visible = False
            btSelect.Visible = False
            btSelectTH.Visible = False

        End If
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click

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

    Protected Sub btSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSelect.Click

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
                type = type & " (ชิ้นส่วนอิเลคโทรนิคส์)" & "'" & ")"
            Case "Appliances"
                type = type & " (ชิ้นส่วนประกอบตู้เย็น)" & "'" & ")"
            Case "Automative"
                type = type & " (ชิ้นส่วนประกอบยานยนต์)" & "'" & ")"
        End Select
        For i = 0 To GridView1.Rows.Count - 1
            chkSelect = GridView1.Rows(i).FindControl("cbSelect")
            If chkSelect.Checked = True Then
                invType = GridView1.Rows(i).Cells(1).Text.ToString
                invNo = GridView1.Rows(i).Cells(2).Text.ToString

                SQL = "select TB022,INVMB.UDF51,INVMB.MB001 from ACRTB left join INVMB on INVMB.MB001 = TB039 where from ACRTB left join INVMB on INVMB.MB001 = TB039 where TB001='" & invType & "' and TB002='" & invNo & "' and INVMB.MB014 = 0 or TB001='" & invType & "' and TB002='" & invNo & "' and INVMB.UDF51 = 0 order by TB003 "
                Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

                If Program.Rows.Count > 0 Then
                    show_message.ShowMessage(Page, "Please check Outer Packing Unit and Qty/Box Unequal To Zero Item: " & Program.Rows(0).Item("Item"), UpdatePanel1)
                Else
                    'SQL = "select ROUND(UDF51,0) as PACK, UDF52 as Weight,ROUND(TB022,0) as QTY,TB017+TB018 as AMT,TB048 as PO,(TB001)+(TB002) as INV,replace(TB040,'''',' ') as NAME from ACRTB where TB001='" & invType & "' and TB002='" & invNo & "' order by TB003 "
                    SQL = "select CEILING((TB022/INVMB.UDF51)) as PACK, Round((INVMB.MB014*TB022),2) as Weight,ROUND(TB022,0) as QTY,TB017+TB018 as AMT,TB048 as PO,(TB001)+(TB002) as INV,replace(TB040,'''',' ') as NAME from ACRTB left join INVMB on INVMB.MB001 = TB039 where TB001='" & invType & "' and TB002='" & invNo & "' order by TB003 "
                    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                    For j As Integer = 0 To Program.Rows.Count - 1
                        Dim ISQL = " insert into " & tableName & "(seq,pack,wgh,qty,amt,po,inv,note) values " & _
                                   " ('" & seq & "','" & Program.Rows(j).Item("PACK") & "','" & Program.Rows(j).Item("Weight") & "','" & Program.Rows(j).Item("QTY") & "','" & Program.Rows(j).Item("AMT") & "'," & _
                                   "'" & Program.Rows(j).Item("PO") & "','" & Program.Rows(j).Item("INV") & "',N'" & Program.Rows(j).Item("NAME") & type
                        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
                        seq += 1
                    Next
                End If
            End If
        Next
        SQL = "select seq as ITEM,pack as PACK,wgh as Weight,qty as QTY,amt as AMT,po as PO,inv as INV,note as REMARK from " & tableName & " order by seq"
        ControlForm.ShowGridView(GridView2, SQL)

        btPrintDel.Visible = True
        btExcel.Visible = True

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
                type = type & " (ชิ้นส่วนอิเลคโทรนิคส์)" & "'" & ")"
            Case "Appliances"
                type = type & " (ชิ้นส่วนประกอบตู้เย็น)" & "'" & ")"
            Case "Automative"
                type = type & " (ชิ้นส่วนประกอบยานยนต์)" & "'" & ")"
        End Select

        For i = 0 To GridView1.Rows.Count - 1
            chkSelect = GridView1.Rows(i).FindControl("cbSelect")
            If chkSelect.Checked = True Then
                invType = GridView1.Rows(i).Cells(1).Text.ToString
                invNo = GridView1.Rows(i).Cells(2).Text.ToString

                SQL = "select INVMB.MB001 as Item,TB022,INVMB.UDF51,(TB001)+(TB002) as INV from ACRTB left join INVMB on INVMB.MB001 = TB039 where TB001='" & invType & "' and TB002='" & invNo & "' and INVMB.MB014 = 0 or TB001='" & invType & "' and TB002='" & invNo & "' and INVMB.UDF51 = 0 order by TB003 "
                Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

                If Program.Rows.Count > 0 Then
                    show_message.ShowMessage(Page, "Please check Outer Packing Unit and Qty/Box Unequal To Zero Item: " & Program.Rows(0).Item("Item"), UpdatePanel1)
                Else
                    'SQL = "select UDF51 as PACK, UDF52 as Weight,TB022 as QTY,TB019+TB020 as AMT,TB048 as PO,(TB001)+(TB002) as INV,replace(TB040,'''',' ') as NAME from ACRTB where TB001='" & invType & "' and TB002='" & invNo & "' order by TB003 "
                    SQL = "select CEILING((TB022/INVMB.UDF51)) as PACK, Round((INVMB.MB014*TB022),2) as Weight,TB022 as QTY,TB019+TB020 as AMT,TB048 as PO,(TB001)+(TB002) as INV,replace(TB040,'''',' ') as NAME from ACRTB left join INVMB on INVMB.MB001 = TB039 where TB001='" & invType & "' and TB002='" & invNo & "' order by TB003 "
                    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

                    For j As Integer = 0 To Program.Rows.Count - 1
                        Dim ISQL = " insert into " & tableName & "(seq,pack,wgh,qty,amt,po,inv,note) values " & _
                                   " ('" & seq & "','" & Program.Rows(j).Item("PACK") & "','" & Program.Rows(j).Item("Weight") & "','" & Program.Rows(j).Item("QTY") & "','" & Program.Rows(j).Item("AMT") & "'," & _
                                   "'" & Program.Rows(j).Item("PO") & "','" & Program.Rows(j).Item("INV") & "',N'" & Program.Rows(j).Item("NAME") & type
                        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
                        seq += 1
                    Next

                End If
            End If
        Next
        SQL = "select seq as ITEM,pack as PACK,wgh as Weight,qty as QTY,amt as AMT,po as PO,inv as INV,note as REMARK from " & tableName & " order by seq"
        ControlForm.ShowGridView(GridView2, SQL)

        btPrintDel.Visible = True
        btExcel.Visible = True

    End Sub

    Protected Sub btPrintDel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrintDel.Click

        Dim tableName As String = "TempCustoms" & Session("UserName")
        Dim paraName As String = ""
        paraName = "tableName:" & tableName
        Randomize()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=CustomsDelta.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)


    End Sub

    Protected Sub btExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcel.Click
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

End Class