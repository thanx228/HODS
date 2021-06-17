Imports System.Globalization

Public Class AccountStockCard
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim unWh As String = "'8888','9999'"
    Dim chrConn As String = Chr(8)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MC001,MC001+' : '+MC002 as MC002 from CMSMC where MC001 not in (" & unWh & ")  order by MC001 "
            ControlForm.showCheckboxList(cblWh, SQL, "MC002", "MC001", 4, Conn_SQL.ERP_ConnectionString)
            SQL = "select MA004,rtrim(MA004)+'-'+MA003 as MA003 from INVMA where MA001='1' order by MA002"
            ControlForm.showCheckboxList(cblCodeType, SQL, "MA003", "MA004", 4, Conn_SQL.ERP_ConnectionString)

        End If
    End Sub

    Protected Sub btDetail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btDetail.Click
        Dim table As String = "tempOpenBalance" & Session("UserName")
        OpenBalance(table)
        Dim WHR As String = " where LA009 <>'' "
        If tbItem.Text.Trim <> "" Then
            WHR = WHR & " and LA001 = '" & tbItem.Text.Trim & "' "
        End If
        WHR = WHR & Conn_SQL.Where("LA009", cblWh)
        WHR = WHR & Conn_SQL.Where("MA004", cblCodeType)
        Dim dateFrom As String = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-us"))
        Dim dateTo As String = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-us"))
        If tbDateFrom.Text <> "" Then
            dateFrom = configDate.dateFormat2(tbDateFrom.Text.Trim)
        End If
        If tbDateTo.Text <> "" Then
            dateTo = configDate.dateFormat2(tbDateTo.Text.Trim)
        End If
        WHR = WHR & Conn_SQL.Where("LA004", dateFrom, dateTo)
        Dim paraName As String = "tempTable:" & table & chrConn & "dateFrom:" & configDate.dateShow2(dateFrom, "/") & chrConn & "dateTo:" & configDate.dateShow2(dateTo, "/") & chrConn & "whr:" & WHR
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=stockCard.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & chrConn & "&encode=1');", True)
    End Sub

    Protected Sub btSum_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSum.Click
        Dim table As String = "tempOpenBalance" & Session("UserName")
        OpenBalance(table)
        Dim fldwh As String = "LA009"
        If ddlGrpBy.Text.Trim = "2" Then 'Account Code
            fldwh = "MA004"
        End If

        Dim WHR As String = ""
        WHR = Conn_SQL.Where("LA001", tbItem)
        WHR = WHR & Conn_SQL.Where("LA009", cblWh)
        WHR = WHR & Conn_SQL.Where("MA009", cblCodeType)
        Dim dateFrom As String = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-us"))
        Dim dateTo As String = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-us"))
        If tbDateFrom.Text <> "" Then
            dateFrom = configDate.dateFormat2(tbDateFrom.Text.Trim)
        End If
        If tbDateTo.Text <> "" Then
            dateTo = configDate.dateFormat2(tbDateTo.Text.Trim)
        End If
        WHR = WHR & Conn_SQL.Where("LA004", dateFrom, dateTo)
        Dim SQL As String = " select LA001," & fldwh & ",SUM(LA005*LA011)as qty,SUM(LA005*LA013)as amt from INVLA " & _
            " left join INVMB on MB001=LA001 " & _
            " left join INVMA on MA002=MB005 " & _
            " where LA009 not in(" & unWh & ") " & WHR & _
            " group by LA001," & fldwh & " having SUM(LA005*LA011)<>0 or SUM(LA005*LA013)<>0 " & _
            " order by LA001," & fldwh

        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim fldHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                whrHash.Add("item", .Item("LA001").ToString.Trim)
                whrHash.Add("wh", .Item(fldwh).ToString.Trim)

                fldHash.Add("qty", .Item("qty"))
                fldHash.Add("amt", .Item("amt"))
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(table, fldHash, whrHash), Conn_SQL.MIS_ConnectionString)
            End With
        Next
        Dim paraName As String = "tempTable:" & table & chrConn & "dateFrom:" & configDate.dateShow2(dateFrom, "/") & chrConn & "dateTo:" & configDate.dateShow2(dateTo, "/")
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=stockSummary.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & Server.UrlEncode(chrConn) & "&encode=1');", True)
    End Sub

    Protected Sub OpenBalance(ByVal table As String)
        CreateTempTable.createTempAccStockCard(table)
        Dim fldwh As String = "LA009"
        If ddlGrpBy.Text.Trim = "2" Then 'Account Code
            fldwh = "MA004"
        End If

        Dim WHR As String = ""
        WHR = Conn_SQL.Where("LA001", tbItem)
        WHR = WHR & Conn_SQL.Where("LA009", cblWh)
        WHR = WHR & Conn_SQL.Where("MA004", cblCodeType)
        Dim dateFrom As String = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-us"))
        If tbDateFrom.Text <> "" Then
            dateFrom = configDate.dateFormat2(tbDateFrom.Text.Trim)
        End If
        WHR = WHR & Conn_SQL.Where("LA004", "<" & dateFrom)
        Dim SQL As String = " select LA001," & fldwh & ",SUM(LA005*LA011),SUM(LA005*LA013) from JINPAO80.dbo.INVLA " & _
            " left join JINPAO80.dbo.INVMB on MB001=LA001 " & _
            " left join JINPAO80.dbo.INVMA on MA002=MB005 " & _
            " where LA009 not in(" & unWh & ") " & WHR & _
            " group by LA001," & fldwh & " having SUM(LA005*LA011)<>0 or SUM(LA005*LA013)<>0 " & _
            " order by LA001," & fldwh
        Dim ISQL As String = "insert into " & table & "(item,wh,openQty,openAmt) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
    End Sub

End Class