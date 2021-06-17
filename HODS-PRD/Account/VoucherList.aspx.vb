Imports System.Globalization

Public Class VoucherList
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim chrConn As String = Chr(8)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' - '+MQ002 as MQ002 from CMSMQ where MQ003='91' order by MQ002 "
            ControlForm.showDDL(ddlType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub
    Private Function getWhr(Optional ByVal likeCon As Boolean = True) As String
        Dim WHR As String = ""
        WHR = WHR & Conn_SQL.Where("TA001", ddlType)
        If tbAcCode.Text <> "" Then
            WHR = WHR & Conn_SQL.Where("TB005", tbAcCode, likeCon)
        End If
        Return WHR
    End Function
    Private Function getAccDesc(ByVal accCode As String) As String
        Dim SQL As String = "select MQ002 from CMSMQ where MQ001='" & accCode & "' "
        Dim dt As DataTable = Conn_SQL.Get_DataReader(Sql, Conn_SQL.ERP_ConnectionString)
        Dim typeName As String = ""
        If dt.Rows.Count > 0 Then
            typeName = "-" & dt.Rows(0).Item("MQ002")
        End If
        Return typeName
    End Function
    Protected Sub btPrintDet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrintDet.Click
        Dim WHR As String = getWhr(False)
        Dim date1 As String = tbDateFrom.Text,
          date2 As String = tbDateTo.Text,
          strDate As String = "",
          endDate As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat2(date1)
        Else
            strDate = Date.Today.ToString("yyyyMM", New CultureInfo("en-US"))
            strDate = strDate & "01"
        End If

        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat2(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        WHR = WHR & Conn_SQL.Where("TA003", strDate, endDate)
        Dim paraName As String = "whr:" & WHR & chrConn & "DateFrom:" & configDate.dateShow2(strDate, "/") & chrConn & "DateTo:" & configDate.dateShow2(endDate, "/") & chrConn & "Type2:" & ddlType.Text & getAccDesc(ddlType.Text)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=VoucherListDet.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & chrConn & "&encode=1');", True)

    End Sub

    Protected Sub btPrintSum_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrintSum.Click
        Dim tempTable As String = "tempVoucheListSum" & Session("UserName")
        CreateTempTable.createTempVoucherListSum(tempTable)
        Dim SQL As String = "",
            WHR As String = getWhr(False),
            ISQL As String = ""

       Dim date1 As String = tbDateFrom.Text,
          date2 As String = tbDateTo.Text,
          strDate As String = "",
          endDate As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat2(date1)
        Else
            strDate = Date.Today.ToString("yyyyMM", New CultureInfo("en-US"))
            strDate = strDate & "01"
        End If

        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat2(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        WHR = WHR & Conn_SQL.Where("TA003", strDate, endDate)
        SQL = " select TB005,case when TA001='9102' then substring(TA009,1,2) else substring(TA009,1,4) end type2, " & _
              " sum(case when TB004='1' then TB015 else 0 end) as debit, " & _
              " sum(case when TB004='-1' then TB015 else 0 end) as credit from JINPAO80.dbo.ACTTB " & _
              " left join JINPAO80.dbo.ACTTA on TA001=TB001 and TA002=TB002 where TA010 = 'Y' " & WHR & _
              " group by TB005,case when TA001='9102' then substring(TA009,1,2) else substring(TA009,1,4) end "
        ISQL = "insert into " & tempTable & "(accCode,typeCode,debitAmt,creditAmt) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        Dim paraName As String = "temptable:" & tempTable & chrConn & "DateFrom:" & configDate.dateShow2(strDate, "/") & chrConn & "DateTo:" & configDate.dateShow2(endDate, "/") & chrConn & "Type2:" & ddlType.Text & getAccDesc(ddlType.Text)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=VoucherListSum.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & chrConn & "&encode=1');", True)
    End Sub
End Class