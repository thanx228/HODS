Imports System.Globalization
Public Class PeriodInventory
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim unWh As String = "'8888','9999'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MC001,MC001+' : '+MC002 as MC002 from CMSMC where MC001 not in (" & unWh & ")  order by MC001 "
            'ControlForm.showDDL(ddlWh, SQL, "MC002", "MC001", False, Conn_SQL.ERP_ConnectionString)
            ControlForm.showCheckboxList(cblWh, SQL, "MC002", "MC001", 4, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False

            SQL = "select MA002,rtrim(MA002)+'-'+MA003 as MA003 from INVMA where MA001='1' order by MA002"
            ControlForm.showDDL(ddlInvCat, SQL, "MA003", "MA002", True, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempPeriodInventory" & Session("UserName")

        Dim date1 As String = tbDateFrom.Text,
            date2 As String = tbDateTo.Text,
            strDate As String = "",
            endDate As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormatYM(date1)
        Else
            strDate = Date.Today.ToString("yyyyMM", New CultureInfo("en-US"))
        End If
        strDate = strDate & "01"
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormatYM(date2)
        Else
            endDate = Date.Today.ToString("yyyyMM", New CultureInfo("en-US"))
        End If
        endDate = endDate & "01"
        Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim amtMonth As Short = DateDiff(DateInterval.Month, beginDate, lastDate)
        'CreateTempTable.CreateTempPeriodInventory(tempTable, beginDate, amtMonth)

        Dim SQL As String = "",
            WHR As String = ""
        Dim Program As New DataTable

        Dim dt As Data.DataTable = New DataTable
        'Dim tt As DataColumn
        'tt = New DataColumn
        'tt.ColumnName = "text"
        dt.Columns.Add(New DataColumn("Item"))
        dt.Columns.Add(New DataColumn("Desc"))
        dt.Columns.Add(New DataColumn("Spec"))
        dt.Columns.Add(New DataColumn("Unit"))
        dt.Columns.Add(New DataColumn("Warehouse"))
        For i As Integer = 0 To amtMonth
            Dim tdate As String = beginDate.AddMonths(i).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
            Dim text As String = tdate.Substring(4, 2) & tdate.Substring(0, 4)
            Dim dc As DataColumn
            dc = New DataColumn("Qty" & text)
            dc.Caption = "test show"
            dt.Columns.Add(dc)
            dt.Columns.Add(New DataColumn("Cost" & text))
            dt.Columns.Add(New DataColumn("Amt" & text))
        Next
        'where 
        WHR = WHR & Conn_SQL.Where("MB005", ddlInvCat)
        WHR = WHR & Conn_SQL.Where("LC001", tbItem)
        WHR = WHR & Conn_SQL.Where("MB002", tbDesc)
        WHR = WHR & Conn_SQL.Where("MB003", tbSpec)
        WHR = WHR & Conn_SQL.Where("LC003", cblWh)
        WHR = WHR & Conn_SQL.Where("LC002", strDate.Substring(0, 6), endDate.Substring(0, 6))

        SQL = "select LC001 as 'item',MB002 as itemDesc,MB003 as spec,MB004 as unit,LC003 as 'wh',LC002 as 'ym',LB010 as 'unitCost'," & _
              " LC004+LC006-LC008-LC010+LC012+LC014-LC016+LC018+LC020-LC022-LC024 as 'endCost'," & _
              " LC005+LC007-LC009-LC011+LC013+LC015-LC017+LC019+LC021-LC023-LC025 as 'endQty' from INVLC " & _
              " left join INVLB on LB001=LC001 and LB002=LC002 " & _
              " left join INVMB on MB001=LC001 " & _
              " where LC001<>'' " & WHR & " order by LC001,LC003,LC002 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim dr As DataRow,
            lstItem As String = "",
            lstWh As String = ""
        gvShow.DataSource = ""
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim item As String = .Item("item").ToString
                Dim wh As String = .Item("wh").ToString
                If lstItem <> item Or lstWh <> wh Then
                    If lstItem <> "" And lstWh <> "" Then
                        dt.Rows.Add(dr)
                    End If
                    dr = dt.NewRow()
                    dr("Item") = item
                    dr("Desc") = .Item("itemDesc").ToString
                    dr("Spec") = .Item("spec").ToString
                    dr("Unit") = .Item("unit").ToString
                    dr("Warehouse") = wh
                    For j As Integer = 0 To amtMonth
                        Dim tdate As String = beginDate.AddMonths(j).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
                        Dim text As String = tdate.Substring(4, 2) & tdate.Substring(0, 4)
                        dr("Qty" & text) = ""
                        dr("Cost" & text) = ""
                        dr("Amt" & text) = ""
                    Next
                End If
                'set value to cell gridview 
                Dim ym As String = .Item("ym").ToString.Trim.Substring(4, 2) & .Item("ym").ToString.Trim.Substring(0, 4)
                dr("Qty" & ym) = String.Format("{0:n2}", CDec(.Item("endCost")))
                dr("Cost" & ym) = String.Format("{0:n2}", CDec(.Item("unitCost")))
                dr("Amt" & ym) = String.Format("{0:n2}", CDec(.Item("endQty")))
                lstItem = item
                lstWh = wh
            End With
        Next
        If Program.Rows.Count > 0 And lstItem <> "" And lstWh <> "" Then
            dt.Rows.Add(dr)
            gvShow.DataSource = dt
        End If
        gvShow.DataBind()
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    'Function getSQL(Optional ByVal sum As Boolean = False) As String

    '    Dim SQL As String = "",
    '        WHR As String = ""


    '    If sum Then 'check sum is true
    '        SQL = "select top 1 count(LC001)as maxPeriod  from INVLC " & _
    '          " left join INVLB on LB001=LC001 and LB002=LC002 " & _
    '          " left join INVMB on MB001=LC001 " & _
    '          " where " & _
    '          " group by  LC001,LC003 " & _
    '          "order by maxPeriod desc"
    '    Else 'check sum is false
    '        'SQL = "select LC001 as 'item',LC002 as 'ym',LC003 as 'wh',LB010 as 'unitCost'," & _
    '        '  " LC004+LC006-LC008-LC010+LC012+LC014-LC016+LC018+LC020-LC022-LC024 as 'endCost'," & _
    '        '  " LC005+LC007-LC009-LC011+LC013+LC015-LC017+LC019+LC021-LC023-LC025 as 'endQty' from INVLC " & _
    '        '  " left join INVLB on LB001=LC001 and LB002=LC002 " & _
    '        '  " left join INVMB on MB001=LC001 " & _
    '        '  " where " & _
    '        '  "order by LC001,LC003,LC002"
    '    End If
    '    Return SQL
    'End Function

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("PeriodInventory" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class