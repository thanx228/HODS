Public Class PayableReport
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim SQL As String = "select MA041 from PURMA where MA041<>'' group by MA041 order by MA041"
            ControlForm.showCheckboxList(cblType, SQL, "MA041", "MA041", 3, Conn_SQL.ERP_ConnectionString)
            'ControlForm.showDDL(ddlType, SQL, "MA041", "MA041", False, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
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
        BeginDate = configDate.dateShow2(BeginDate, "/")
        Dim endDate As String = tbDateTo.Text
        If endDate <> "" Then
            endDate = configDate.dateFormat2(endDate)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        End If
        endDate = configDate.dateShow2(endDate, "/")
        Select Case ddlReport.Text
            Case "1" 'status
                tempTable = "tempPayableStatus" & Session("UserName")
                PayableStatus(tempTable)
                Dim sql As String = "select * from " & tempTable
                dbName = "ERP"
                filePrint = "PayableStatusReport.rpt"
                'whr = whr & "and M1.MA041='" & cblType.Text.Trim & "'"
                whr = whr & Conn_SQL.Where("M1.MA041", cblType, False, "", True)
                If tbSup.Text <> "" Then
                    whr = " and M1.MA00 like '" & tbSup.Text.Trim & "%' "
                End If
                paraName = "tempTable:" & tempTable & chrConn & "DateFrom:" & BeginDate & chrConn & "DateTo:" & endDate & chrConn & "whr:" & whr
            Case "2" 'detail
                dbName = "MIS"
                filePrint = "PayableBodyReport.rpt"
                tempTable = "tempPayableHead" & Session("UserName")
                payableHead(tempTable)
                Dim tempTableB As String = "tempPayableBody" & Session("UserName")
                payableBody(tempTableB)
                paraName = "tempTable:" & tempTable & chrConn & "tempTableB:" & tempTableB & chrConn & "DateFrom:" & BeginDate & chrConn & "DateTo:" & endDate
            Case "3" 'Head
                dbName = "MIS"
                filePrint = "PayableHeadReport.rpt"
                tempTable = "tempPayableHead" & Session("UserName")
                payableHead(tempTable)
                paraName = "tempTable:" & tempTable & chrConn & "DateFrom:" & BeginDate & chrConn & "DateTo:" & endDate
        End Select
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "NewWindow('../ShowCrystalReport.aspx?dbName=ERP&ReportName=" & filePrint & "&paraName=" & paraName & "','Cheque','900','500');", True)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=" & dbName & "&ReportName=" & filePrint & "&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & chrConn & "&encode=1');", True)

    End Sub

    Sub PayableStatus(ByVal tempTable As String)
        CreateTempTable.createTempPayableStatus(tempTable)
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

        'open from purchase invoice
        WHR = Conn_SQL.Where("TA004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)

        SQL = " select TA004,sum(case when TA079='1' then TA037+TA038 else -1*(TA037+TA038) end) amt " & _
              " from ACPTA " & _
              " left join PURMA on MA001=TA004 " & _
              " where ACPTA.TA024 = 'Y' and TA015<'" & BeginDate & "' " & WHR & _
              " group by TA004  order by TA004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                addValDic(supOpen, .Item("TA004").ToString.Trim, .Item("amt"))
            End With
        Next

        'open form other Payable
        WHR = Conn_SQL.Where("TI004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType)

        SQL = " select TI004,sum(case when TI031='1' then TI018 else -1*(TI018) end) amt from ACPTI " & _
              " left join PURMA on MA001=TI004 " & _
              " where TI013='Y' and TI019<'" & BeginDate & "' " & WHR & _
              " group by TI004 order by TI004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                addValDic(supOpen, .Item("TI004").ToString.Trim, .Item("amt"))
            End With
        Next
        'open Payment  
        WHR = Conn_SQL.Where("TK004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)
        SQL = " select TK004,SUM(TK038) as amt from ACPTK " & _
              " left join PURMA on MA001=TK004 " & _
              " where TK020='Y' and TK003<'" & BeginDate & "' " & WHR & _
              " group by TK004 order by TK004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim sup As String = .Item("TK004").ToString.Trim
                Dim amt As Decimal = .Item("amt")
                If supOpen.ContainsKey(sup) Then
                    supOpen.Item(sup) = supOpen.Item(sup) - amt
                Else
                    supOpen.Add(sup, -1 * amt)
                End If
            End With
        Next
        Dim pair As KeyValuePair(Of String, Decimal)
        For Each pair In supOpen
            ' Display Key and Value.
            If pair.Value <> 0 Then
                Dim fldHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                whrHash.Add("supplier", pair.Key)
                fldHash.Add("openPur", pair.Value)
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
            End If
        Next
        Dim supCurrent As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)
        Dim supCredit As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)
        'purchase & credit from purchase invoice
        WHR = Conn_SQL.Where("TA004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TA015", BeginDate, endDate)

        SQL = " select TA004,sum(case when TA079='1' then TA037+TA038 else -1*(TA037+TA038) end) amt " & _
              " from ACPTA " & _
              " left join PURMA on MA001=TA004 " & _
              " where ACPTA.TA024 = 'Y' " & WHR & _
              " group by TA004  order by TA004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                addValDic(supCurrent, .Item("TA004").ToString.Trim, .Item("amt"))
            End With
        Next
        'purchase & credit from other payable
        WHR = Conn_SQL.Where("TI004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TI019", BeginDate, endDate)
        SQL = " select TI004,sum(case when TI031='1' then TI018 else -1*(TI018) end) amt from ACPTI " & _
              " left join PURMA on MA001=TI004 " & _
              " where TI013 = 'Y' " & WHR & _
              " group by TI004 order by TI004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                addValDic(supCurrent, .Item("TI004").ToString.Trim, .Item("amt"))
            End With
        Next
        'purchase
        For Each pair In supCurrent
            ' Display Key and Value.
            If pair.Value <> 0 Then
                Dim fldHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                whrHash.Add("supplier", pair.Key)
                fldHash.Add("purchase", pair.Value)
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash), Conn_SQL.MIS_ConnectionString)
            End If
        Next
        'payment
        WHR = Conn_SQL.Where("TK004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TK003", BeginDate, endDate)
        SQL = " select TK004,SUM(TK036) amt from ACPTK " & _
              " left join PURMA on MA001=TK004 " & _
              " where TK020='Y' " & WHR & _
              " group by TK004 order by TK004 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim fldHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                whrHash.Add("supplier", .Item("TK004").ToString.Trim)
                fldHash.Add("payment", .Item("amt"))
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldHash, whrHash), Conn_SQL.MIS_ConnectionString)
            End With
        Next

    End Sub

    Sub addValDic(ByRef dict As Dictionary(Of String, Decimal), ByVal item As String, ByVal val As Decimal)
        If dict.ContainsKey(item) Then
            dict.Item(item) = dict.Item(item) + val
        Else
            dict.Add(item, val)
        End If
    End Sub

    Sub payableHead(ByVal tempTable As String)
        CreateTempTable.createTempPayableHead(tempTable)
        Dim SQL As String = "",
            WHR As String = "",
            USQL As String = ""

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
        'Dim dt As New DataTable
        'purchase invoice

        WHR = Conn_SQL.Where("TA004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TA015", BeginDate, endDate)

        Dim fld As String = "TA001,TA002,TA004,TA014,TA019,TA034,TA037,TA038,TA087"
        SQL = " select " & fld & " from JINPAO80.dbo.ACPTA left join JINPAO80.dbo.PURMA on MA001=TA004  where TA024 = 'Y' " & WHR
        USQL = "insert into " & tempTable & "(" & fld & ")" & SQL
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        'other payable
        WHR = Conn_SQL.Where("TI004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TI019", BeginDate, endDate)
        fld = "TA001,TA002,TA004,TA014,TA034,TA037,TA087"
        SQL = "select TI001,TI002,TI004,TI012,TI019,TI016,TI029 from JINPAO80.dbo.ACPTI left join JINPAO80.dbo.PURMA on MA001=TI004 where TI013='Y' " & WHR
        USQL = "insert into " & tempTable & "(" & fld & ")" & SQL
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

    End Sub
    Sub payableBody(ByVal tempTable As String)
        CreateTempTable.createTempPayableBody(tempTable)
        Dim SQL As String = "",
            WHR As String = "",
            FLD As String = "",
            USQL As String = ""
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
        'Purchase invoice
        WHR = Conn_SQL.Where("TA004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TA015", BeginDate, endDate)

        FLD = " TB001, TB002, TB003, TB005, TB006, TB007, TB017, TB018, TB019, TB020, TB037, TB038, TB039,TB040"
        SQL = " select " & FLD & " from JINPAO80.dbo.ACPTB " & _
              " left join JINPAO80.dbo.ACPTA on TA001=TB001 and TA002=TB002 " & _
              " left join JINPAO80.dbo.PURMA on MA001=TA004 where TA024='Y' " & WHR
        USQL = "insert into " & tempTable & "(" & FLD & ")" & SQL
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        'other payable
        WHR = Conn_SQL.Where("TI004", tbSup)
        WHR = WHR & Conn_SQL.Where("MA041", cblType, False, "", True)
        WHR = WHR & Conn_SQL.Where("TI019", BeginDate, endDate)
        FLD = " TB001, TB002, TB003, TB017,TB019,TB020,TB037,TB038"
        SQL = " select TJ001,TJ002,TJ003,TJ010,'1',TJ010,TJ006,TJ005 from JINPAO80.dbo.ACPTJ " & _
              " left join JINPAO80.dbo.ACPTI on TI001=TJ001 and TI002=TJ002 " & _
              " left join JINPAO80.dbo.PURMA on MA001=TI004 where TI013='Y' " & WHR
        USQL = "insert into " & tempTable & "(" & FLD & ")" & SQL
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

    End Sub
End Class