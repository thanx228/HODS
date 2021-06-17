Public Class ReceiveableReport
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If

            Dim SQL As String = "select ACTMA.MA001,ACTMA.MA001+'-'+ACTMA.MA003 as MA003 from COPMA left join ACTMA on ACTMA.MA001=COPMA.MA047 where MA047<>'' group by ACTMA.MA001,ACTMA.MA003 order by ACTMA.MA001,ACTMA.MA003"
            ControlForm.showCheckboxList(cblType, SQL, "MA003", "MA001", 3, Conn_SQL.ERP_ConnectionString)
            'ControlForm.showDDL(ddlType, SQL, "MA041", "MA041", False, Conn_SQL.ERP_ConnectionString)

        End If
    End Sub

    Protected Sub btReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btReport.Click
        Dim tempTable As String = "",
           dbName As String = "ERP",
           filePrint As String = "",
           paraName As String = "",
           chrConn As String = Chr(8),
           whr As String = ""
        Dim BeginDate As String = tbDateFrom.Text
        If BeginDate <> "" Then
            BeginDate = configDate.dateFormat2(BeginDate)
        Else
            BeginDate = Date.Today.ToString("yyyyMM", New System.Globalization.CultureInfo("en-US"))
            BeginDate = BeginDate & "01"
        End If
        Dim endDate As String = tbDateTo.Text
        If endDate <> "" Then
            endDate = configDate.dateFormat2(endDate)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        End If
        If ddlType.Text <> "1" Then
            whr = Conn_SQL.Where("TA004", tbCust, False)
            whr = whr & Conn_SQL.Where("MA047", cblType)
            whr = whr & Conn_SQL.Where("TA038", BeginDate, endDate)
        End If
        BeginDate = configDate.dateShow2(BeginDate, "/")
        endDate = configDate.dateShow2(endDate, "/")

        Select Case ddlType.Text
            Case "1" 'status
                'dbName = "MIS"
                tempTable = "tempReceiveableStatus" & Session("UserName")
                ReceiveableStatus(tempTable)
                filePrint = "ReceiveableStatusReport.rpt"
                filePrint = "ReceiveableStatusReport.rpt"
                paraName = "tempTable:" & tempTable & chrConn & "DateFrom:" & BeginDate & chrConn & "DateTo:" & endDate & chrConn & "whr:" & whr
            Case "2" 'detail
                filePrint = "ReceiveableBodyReport.rpt"
                paraName = "DateFrom:" & BeginDate & chrConn & "DateTo:" & endDate & chrConn & "whr:" & whr
            Case "3" 'summary
                filePrint = "ReceiveableHeadReport.rpt"
                paraName = "DateFrom:" & BeginDate & chrConn & "DateTo:" & endDate & chrConn & "whr:" & whr
        End Select
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=" & dbName & "&ReportName=" & filePrint & "&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & chrConn & "&encode=1');", True)

    End Sub
    Sub ReceiveableStatus(ByVal tempTable As String)
        CreateTempTable.createTempReceiveableStatus(tempTable)
        Dim SQL As String = "",
            WHR As String = "",
            dt As DataTable
        Dim supOpen As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)
        Dim BeginDate As String = tbDateFrom.Text
        If BeginDate <> "" Then
            BeginDate = configDate.dateFormat2(BeginDate)
        Else
            BeginDate = Date.Today.ToString("yyyyMM", New System.Globalization.CultureInfo("en-US"))
            BeginDate = BeginDate & "01"
        End If

        Dim endDate As String = tbDateTo.Text
        If endDate <> "" Then
            endDate = configDate.dateFormat2(endDate)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        End If
        'open sale invoice
        WHR = Conn_SQL.Where("TA004", tbCust)
        WHR = WHR & Conn_SQL.Where("MA047", cblType)

        SQL = " select TA004,sum(case when TA079='1' then TA041+TA042 else -1*(TA041+TA042) end) amt from ACRTA " & _
              " left join COPMA on MA001=TA004 " & _
              " where TA025='Y' and TA038<'" & BeginDate & "' " & WHR & _
              " group by TA004 order by TA004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                addValDic(supOpen, .Item("TA004").ToString.Trim, .Item("amt"))
            End With
        Next
        'open collectoin and refund
        WHR = Conn_SQL.Where("TK004", tbCust)
        WHR = WHR & Conn_SQL.Where("MA047", cblType, False, "", True)
        SQL = " select TK004,substring(TK001,0,2) as docType,SUM(TK036) as amt from ACRTK " & _
              " left join COPMA on MA001=TK004 " & _
              " where TK020='Y' and TK003<'" & BeginDate & "' " & WHR & _
              " group by TK004,substring(TK001,0,2) order by TK004,substring(TK001,0,2) "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim cust As String = .Item("TK004").ToString.Trim
                Dim amt As Decimal = .Item("amt")
                If .Item("docType").ToString = "63" Then
                    amt = -1 * amt
                End If
                If supOpen.ContainsKey(cust) Then
                    supOpen.Item(cust) = supOpen.Item(cust) + amt
                Else
                    supOpen.Add(cust, amt)
                End If
            End With
        Next
        Dim pair As KeyValuePair(Of String, Decimal)
        For Each pair In supOpen
            ' Display Key and Value.
            If pair.Value <> 0 Then
                Dim fldHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                whrHash.Add("customer", pair.Key)
                fldHash.Add("openSale", pair.Value)
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
            End If
        Next
        Dim Sale As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)
        Dim Credit As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)
        Dim Receive As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)
        Dim Customer As Dictionary(Of String, String) = New Dictionary(Of String, String)

        'sale
        WHR = Conn_SQL.Where("TK004", tbCust)
        WHR = WHR & Conn_SQL.Where("MA047", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TA038", BeginDate, endDate)
        SQL = " select TA004,SUM(case when TA079='1' then TA041+TA042 else 0 end) as sale," & _
              " SUM(case when TA079='2' then -1*(TA041+TA042) else 0 end) as credit from ACRTA " & _
              " left join COPMA on MA001=TA004 " & _
              " where TA025='Y' " & WHR & _
              " group by TA004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim cust As String = .Item("TA004").ToString.Trim
                addValDic(Sale, cust, .Item("sale"))
                addValDic(Credit, cust, .Item("credit"))
                If Customer.ContainsKey(cust) = False Then
                    Customer.Add(cust, cust)
                End If
            End With
        Next
        'receive
        WHR = Conn_SQL.Where("TK004", tbCust)
        WHR = WHR & Conn_SQL.Where("MA047", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TK003", BeginDate, endDate)
        SQL = " select TK004,SUM(case when substring(TK001,1,2)='6D' then -1*TK033 else TK033 end) as amt from ACRTK " & _
              " left join COPMA on MA001=TK004 " & _
              " where TK020='Y' " & WHR & _
              " group by TK004 order by TK004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim cust As String = .Item("TK004").ToString.Trim
                addValDic(Receive, cust, .Item("amt"))
                If Customer.ContainsKey(cust) = False Then
                    Customer.Add(cust, cust)
                End If
            End With
        Next
        Dim pair2 As KeyValuePair(Of String, String)
        For Each pair2 In Customer
            Dim fldHash As Hashtable = New Hashtable
            Dim whrHash As Hashtable = New Hashtable
            whrHash.Add("customer", pair2.Key)
            fldHash.Add("sale", chkVal(Sale, pair2.Key))
            fldHash.Add("credit", chkVal(Credit, pair2.Key))
            fldHash.Add("receive", chkVal(Receive, pair2.Key))
            Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash), Conn_SQL.MIS_ConnectionString)
        Next
        'Dim aa As String = "select * from " & tempTable
        'Dim bb As String = ""
    End Sub

    Sub addValDic(ByRef dict As Dictionary(Of String, Decimal), ByVal item As String, ByVal val As Decimal)
        If dict.ContainsKey(item) Then
            dict.Item(item) = dict.Item(item) + val
        Else
            dict.Add(item, val)
        End If
    End Sub
    Function chkVal(ByVal pair As Dictionary(Of String, Decimal), ByVal keyData As String) As Decimal
        Dim val As Decimal = 0
        If pair.ContainsKey(keyData) Then
            val = pair.Item(keyData)
        End If
        Return val
    End Function
End Class