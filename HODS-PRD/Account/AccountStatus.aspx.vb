Imports System.Globalization

Public Class AccountStatus
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click

        Dim typePrint As String = ddlType.Text,
            sendTo As String = ddlSendTo.Text,
            AccType As String = ddlAccType.Text,
            filePrint As String = "",
            paraName As String = "",
            whrDate As String = "",
            dbName As String = "MIS"

        Dim accCode As String = "",
            contType As String = "",
            isNumberShow As String = ""
        'Dim xx As Match = Regex.Match(typePrint, "", RegexOptions.IgnoreCase).Success
        If Regex.Match(typePrint, "0|1|2|6|7").Success Then 'customer
            Select Case AccType
                Case "1" 'Local
                    accCode = "1103-0101"
                    contType = "(ในประเทศ)"
                Case "2" 'Forign
                    accCode = "1103-0102"
                    contType = "(ต่างประเทศ)"
                Case "3" 'Other
                    accCode = "1103-0103"
                    contType = "(อื่นๆ)"
            End Select
            isNumberShow = "9"
        Else 'suppier
            Select Case AccType
                Case "1" 'Local
                    accCode = "2111-1000"
                    contType = "(ในประเทศ)"
                    isNumberShow = "10"
                Case "2" 'Forign
                    accCode = "2111-3000"
                    contType = "(ต่างประเทศ)"
                    isNumberShow = "11"
                Case "3" 'Other
                    accCode = "1"
                    contType = "(อื่นๆ)"
            End Select
        End If
        Dim dateFrom As String = configDate.dateFormat2(tbDateFrom.Text)
        Dim dateTo As String = configDate.dateFormat2(tbDateTo.Text)
        Dim whr2 As String = ""
        If tbCode.Text <> "" Then
            whr2 = " and TA004='" & tbCode.Text.Trim & "' "
        End If
        Dim tempTable As String = "tempAccountStatus" & Session("UserName")

        Select Case typePrint
            Case "0" ' รายงานภาษีขาย
                If tbDateFrom.Text = "" Then
                    show_message.ShowMessage(Page, "Date From is null!!", UpdatePanel1)
                    tbDateFrom.Focus()
                    Exit Sub
                End If
                If tbDateTo.Text = "" Then
                    show_message.ShowMessage(Page, "Date To is null!!", UpdatePanel1)
                    tbDateTo.Focus()
                    Exit Sub
                End If
                filePrint = "TaxInv.rpt"
                If sendTo = "1" Then
                    filePrint = "TaxInv1.rpt"
                End If
                Dim whr As String = "where TA025 not in ('N') " & configDate.DateWhere("TA038", dateFrom, dateTo)
                If accCode <> "" Then
                    whr = whr & " and MA047='" & accCode & "' "
                End If
                whr = whr & whr2
                Dim dateF As String = configDate.dateFormat2(tbDateFrom.Text)
                Dim txtMY As String = dateF.Substring(4, 2) & "/" & dateF.Substring(2, 2)
                dbName = "ERP"
                paraName = "whr:" & whr & ",TaxMonthYear:" & txtMY & ",DateFrom:" & tbDateFrom.Text & ",DateTo:" & tbDateTo.Text & ",NameType:" & contType
            Case "1" ' ลูกหนี้คงค้างแบบละเอียด
                filePrint = "AccountReceiveBalanceDetail.rpt"
                If sendTo = "1" Then
                    filePrint = "AccountReceiveBalanceDetail1.rpt"
                End If
                genAccRcvBalanceDetail(tempTable, accCode)
                paraName = "tempTable:" & tempTable & ",DateFrom:" & tbDateFrom.Text & ",DateTo:" & tbDateTo.Text & ",NameType:" & contType & ",showNumber:" & isNumberShow
            Case "2" ' ลูกหนี้คงค้างแบบสรุป
                filePrint = "AccountReceiveBalanceSummary.rpt"
                If sendTo = "1" Then
                    filePrint = "AccountReceiveBalanceSummary1.rpt"
                End If
                genAccRcvBalanceDetail(tempTable, accCode)
                paraName = "tempTable:" & tempTable & ",DateFrom:" & tbDateFrom.Text & ",DateTo:" & tbDateTo.Text & ",NameType:" & contType & ",showNumber:" & isNumberShow
            Case "6"  'วิเคราะห์อายุลูกหนี้
                tempTable = "tempAccountInvoiceOverDue" & Session("UserName")
                genInvoiceOverDue(tempTable, accCode)
                filePrint = "AROverDueNew.rpt"
                paraName = "tempTable:" & tempTable & ",DateFrom:" & tbDateFrom.Text & ",DateTo:" & tbDateTo.Text & ",NameHead:วิเคราะห์อายุลูกหนี้,NameType:" & contType & ",showNumber:" & isNumberShow
            Case "7" ' ลูกหนี้คงค้างเกินกำหนด
                tempTable = "tempAccountInvoiceOverDueAll" & Session("UserName")
                genInvoiceOverDue(tempTable, accCode, False)
                filePrint = "AROverDueNew.rpt"
                paraName = "tempTable:" & tempTable & ",DateFrom:,DateTo:,NameHead:ลูกหนี้คงค้างเกินกำหนด,NameType:" & contType & ",showNumber:" & isNumberShow
            Case "8" ' สรุปยอดขายประจำงวด
                ' Dim dd As String = configDate.dateFormat2(tbDateTo.Text.Trim, "/")
                If tbDateTo.Text = "" Then
                    show_message.ShowMessage(Page, "Date To is null!!", UpdatePanel1)
                    tbDateTo.Focus()
                    Exit Sub
                End If
                filePrint = "SaleAmount.rpt"
                paraName = "whr:" & If(AccType = "0", "", " and C.MA047='" & accCode & "'") & ",txtMonth:" & tbDateTo.Text.Trim & ",monthlyCode:" & configDate.dateFormat2(tbDateTo.Text.Trim, "/").Substring(0, 6)
                dbName = "ERP"
            Case "3" ' รายงานภาษีซื้อ
                If tbDateFrom.Text = "" Then
                    show_message.ShowMessage(Page, "Date From is null!!", UpdatePanel1)
                    tbDateFrom.Focus()
                    Exit Sub
                End If
                If tbDateTo.Text = "" Then
                    show_message.ShowMessage(Page, "Date To is null!!", UpdatePanel1)
                    tbDateTo.Focus()
                    Exit Sub
                End If
                tempTable = "tempAccountTaxPuchase" & Session("UserName")
                genPurchaseData(tempTable, accCode)
                filePrint = "TaxPur.rpt"
                Dim dateF As String = configDate.dateFormat2(tbDateFrom.Text)
                Dim txtMY As String = dateF.Substring(4, 2) & "/" & dateF.Substring(2, 2)
                'dbName = "MIS"
                paraName = "tempTable:" & tempTable & ",TaxMonthYear:" & txtMY & ",DateFrom:" & tbDateFrom.Text & ",DateTo:" & tbDateTo.Text & ",NameType:" & contType
                'paraName = "tempTable:" & tempTable & ",DateFrom:" & tbDateFrom.Text & ",DateTo:" & tbDateTo.Text
            Case "4" ' เจ้าหนี้คงค้างแบบละเอียด
                filePrint = "AccountPayBalanceDetail.rpt"
                If sendTo = "1" Then
                    filePrint = "AccountPayBalanceDetail1.rpt"
                End If
                genAccPayBalanceDetail(tempTable, accCode)
                paraName = "tempTable:" & tempTable & ",DateFrom:" & tbDateFrom.Text & ",DateTo:" & tbDateTo.Text & ",NameType:" & contType & ",showNumber:" & isNumberShow
            Case "5"  ' เจ้าหนี้คงค้างแบบสรุป
                filePrint = "AccountPayBalanceSummary.rpt"
                If sendTo = "1" Then
                    filePrint = "AccountPayBalanceSummary1.rpt"
                End If
                genAccPayBalanceDetail(tempTable, accCode)
                paraName = "tempTable:" & tempTable & ",DateFrom:" & tbDateFrom.Text & ",DateTo:" & tbDateTo.Text & ",NameType:" & contType & ",showNumber:" & isNumberShow
        End Select
        'Dim paraName As String = "DateFrom:" & strDate & ",DateTo:" & endDate
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=" & dbName & "&ReportName=" & filePrint & "&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "NewWindow('../ShowCrystalReport.aspx?dbName=ERP&ReportName=" & filePrint & "&paraName=" & paraName & "','Cheque','900','500');", True)
    End Sub
    Sub genAccRcvBalanceDetail(tempTable As String, accCode As String)
        CreateTempTable.createTempAccountStatus(tempTable)
        Dim dateTo As String = configDate.dateFormat2(tbDateTo.Text)
        Dim dateFrom As String = configDate.dateFormat2(tbDateFrom.Text)
        Dim Program As New DataTable
        Dim whr As String = "",
            ISQL As String = "",
            USQL As String = "",
            SQL As String = ""
        If tbCode.Text <> "" Then
            whr = " and TA004='" & tbCode.Text.Trim & "' "
        End If
        If accCode <> "" Then
            whr = whr & " and MA047='" & accCode & "' "
        End If
        whr = whr & configDate.DateWhere("TA038", dateFrom, dateTo)

        SQL = " select TA001+'-'+TA002,TA003,TA004,MA002,MA017,TA056," & _
              " case when TA010>1 then case TA079 when '2' then -1*(TA029+TA030) else TA029+TA030 end else 0 end as amtExp," & _
              " case when TA010>1 then case TA079 when '2' then -1*(TA099) else TA099 end else 0 end as rcvExp," & _
              " case when TA010>1 then case TA079 when '2' then -1*(TA029+TA030-TA099) else TA029+TA030-TA099 end else 0 end as balExp," & _
              " case TA079 when '2' then -1*(TA041+TA042) else TA041+TA042 end as amtLoc," & _
              " case TA079 when '2' then -1*(TA098) else TA098 end as rcvLoc, " & _
              " case TA079 when '2' then -1*(TA041+TA042-TA098) else TA041+TA042-TA098 end as balLoc" & _
              " from HOOTHAI.dbo.ACRTA left join HOOTHAI.dbo.COPMA on MA001=TA004 " & _
              " where  TA100 in ('1','2') and TA025 ='Y' " & whr '
        ISQL = "insert into " & tempTable & "(docDetail,docDate,contactId,contactName,section,docBy,exportAmount,exportAction,exportBalance,localAmount,localAction,localBalance) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        If dateTo <> "" Then
            whr = whr & " and TK003 > '" & dateTo & "' "
            'receipt
            SQL = " select TA001+'-'+TA002 as ordDetail,TA003,TA004,MA002,MA017,TA056, " & _
                  " case when TA010>1 then case TA079 when '2' then -1*(TA029+TA030) else TA029+TA030 end else 0 end as amtExp," & _
                  " case when TA010>1 then case TA079 when '2' then -1*(TA031) else TA031 end else 0 end as rcvExp," & _
                  " case when TA010>1 then case TA079 when '2' then -1*(TA029+TA030-TA031) else TA029+TA030-TA031 end else 0 end as balExp," & _
                  " case TA079 when '2' then -1*(TA041+TA042) else TA041+TA042 end as amtLoc," & _
                  " case TA079 when '2' then -1*(TA098) else TA098 end as rcvLoc, " & _
                  " case TA079 when '2' then -1*(TA041+TA042-TA098) else TA041+TA042-TA098 end as balLoc," & _
                  "  case when TA010>1 then TL019 else 0 end as rcvExpCol, TL020 as rcvLocCol " & _
                  " from ACRTL left join ACRTK on TK001=TL001 and TK002=TL002 " & _
                  " left join ACRTA on TA001=TL005 and TA002=TL006 " & _
                  " left join COPMA on  MA001=TA004 " & _
                  " where substring(TK001,1,2)<>'6D' and TK020='Y' and TA025 ='Y' " & whr & _
                  " order by TA001,TA002"
            'case when TA010>1 then TL017 else 0 end as rcvExpCol
            Dim fldInsHash As Hashtable = New Hashtable
            Dim fldUpdHash As Hashtable = New Hashtable
            Dim whrHash As Hashtable = New Hashtable
            Dim lstDetail As String = ""
            Dim rcvLocCol As Decimal = 0
            Dim rcvLoc As Decimal = 0
            Dim balLoc As Decimal = 0
            Dim rcvExpCol As Decimal = 0
            Dim rcvExp As Decimal = 0
            Dim balExp As Decimal = 0

            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)

                    If lstDetail <> .Item("ordDetail") Then
                        If lstDetail <> "" Then
                            Dim locRcv As Decimal = rcvLoc - rcvLocCol
                            Dim locBal As Decimal = balLoc + rcvLocCol
                            Dim ExpRcv As Decimal = rcvExp - rcvExpCol
                            Dim ExpBal As Decimal = balExp + rcvExpCol
                            fldInsHash.Add("exportAction", ExpRcv) 'Export Recv
                            fldInsHash.Add("exportBalance", ExpBal) 'Export Balance
                            fldInsHash.Add("localAction", locRcv) 'Local Rcv
                            fldInsHash.Add("localBalance", locBal) 'Local Balance
                            'Update Zone
                            fldUpdHash.Add("exportAction", "'" & ExpRcv & "'") 'Local Rcv
                            fldUpdHash.Add("exportBalance", "'" & ExpBal & "'") 'Local Balance
                            fldUpdHash.Add("localAction", "'" & locRcv & "'") 'Local Rcv
                            fldUpdHash.Add("localBalance", "'" & locBal & "'") 'Local Balance
                            Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                        End If

                        fldInsHash = New Hashtable
                        fldUpdHash = New Hashtable
                        whrHash = New Hashtable
                        whrHash.Add("docDetail", .Item("ordDetail")) ' order detail " doc type- doc no"
                        'Insert Zone
                        fldInsHash.Add("docDate", .Item("TA003")) 'order date
                        fldInsHash.Add("contactId", .Item("TA004")) 'customer ID
                        fldInsHash.Add("contactName", .Item("MA002")) 'Customer Name
                        fldInsHash.Add("section", .Item("MA017")) 'Invoice By
                        fldInsHash.Add("docBy", .Item("TA056")) 'Invoice By
                        fldInsHash.Add("exportAmount", .Item("amtExp")) 'Export Amount
                        fldInsHash.Add("localAmount", .Item("amtLoc")) 'Local Amount
                        rcvLoc = .Item("rcvLoc")
                        balLoc = .Item("balLoc")
                        rcvLocCol = 0
                        rcvExp = .Item("rcvExp")
                        balExp = .Item("balExp")
                        rcvExpCol = 0
                    End If
                    rcvLocCol = rcvLocCol + .Item("rcvLocCol")
                    rcvExpCol = rcvExpCol + .Item("rcvExpCol")
                    lstDetail = .Item("ordDetail")

                    'whrHash.Add("docDetail", .Item("ordDetail")) ' order detail " doc type- doc no"
                    ''Insert Zone
                    'fldInsHash.Add("docDate", .Item("TA003")) 'order date
                    'fldInsHash.Add("contactId", .Item("TA004")) 'customer ID
                    'fldInsHash.Add("contactName", .Item("MA002")) 'Customer Name
                    'fldInsHash.Add("section", .Item("MA017")) 'Invoice By
                    'fldInsHash.Add("docBy", .Item("TA056")) 'Invoice By
                    'fldInsHash.Add("exportAmount", .Item("amtExp")) 'Export Amount
                    'fldInsHash.Add("localAmount", .Item("amtLoc")) 'Local Amount
                    ''Dim locRcv As Decimal = .Item("rcvLoc") - .Item("rcvLocCol")
                    ''Dim locBal As Decimal = .Item("balLoc") + .Item("rcvLocCol")
                    ''Dim ExpRcv As Decimal = .Item("rcvExp") - .Item("rcvExpCol")
                    ''Dim ExpBal As Decimal = .Item("balExp") + .Item("rcvExpCol")

                    ''fldInsHash.Add("exportAction", .Item("rcvExp")) 'Export Recv
                    ''fldInsHash.Add("exportBalance", .Item("balExp")) 'Export Balance
                    ''fldInsHash.Add("exportAction", ExpRcv) 'Export Recv
                    ''fldInsHash.Add("exportBalance", ExpBal) 'Export Balance
                    ''fldInsHash.Add("localAction", locRcv) 'Local Rcv
                    ''fldInsHash.Add("localBalance", locBal) 'Local Balance
                    ' ''Update Zone
                    ''fldUpdHash.Add("exportAction", "'" & ExpRcv & "'") 'Local Rcv
                    ''fldUpdHash.Add("exportBalance", "'" & ExpBal & "'") 'Local Balance
                    ''fldUpdHash.Add("localAction", "'" & locRcv & "'") 'Local Rcv
                    ''fldUpdHash.Add("localBalance", "'" & locBal & "'") 'Local Balance
                    'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                End With
            Next
            If lstDetail <> "" Then
                Dim locRcv As Decimal = rcvLoc - rcvLocCol
                Dim locBal As Decimal = balLoc + rcvLocCol
                Dim ExpRcv As Decimal = rcvExp - rcvExpCol
                Dim ExpBal As Decimal = balExp + rcvExpCol
                fldInsHash.Add("exportAction", ExpRcv) 'Export Recv
                fldInsHash.Add("exportBalance", ExpBal) 'Export Balance
                fldInsHash.Add("localAction", locRcv) 'Local Rcv
                fldInsHash.Add("localBalance", locBal) 'Local Balance
                'Update Zone
                fldUpdHash.Add("exportAction", "'" & ExpRcv & "'") 'Local Rcv
                fldUpdHash.Add("exportBalance", "'" & ExpBal & "'") 'Local Balance
                fldUpdHash.Add("localAction", "'" & locRcv & "'") 'Local Rcv
                fldUpdHash.Add("localBalance", "'" & locBal & "'") 'Local Balance
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
            End If

            'refund
            SQL = " select TA001+'-'+TA002 as ordDetail,TA003,TA004,MA002,MA017,TA056, " & _
                  " case when TA010>1 then case TA079 when '2' then -1*(TA029+TA030) else TA029+TA030 end else 0 end as amtExp," & _
                  " case when TA010>1 then case TA079 when '2' then -1*(TA031) else TA031 end else 0 end as rcvExp," & _
                  " case when TA010>1 then case TA079 when '2' then -1*(TA029+TA030-TA031) else TA029+TA030-TA031 end else 0 end as balExp," & _
                  " case TA079 when '2' then -1*(TA041+TA042) else TA041+TA042 end as amtLoc," & _
                  " case TA079 when '2' then -1*(TA098) else TA098 end as rcvLoc, " & _
                  " case TA079 when '2' then -1*(TA041+TA042-TA098) else TA041+TA042-TA098 end as balLoc," & _
                  "  case when TA010>1 then TL019 else 0 end as rcvExpCol, TL020 as rcvLocCol " & _
                  " from ACRTL left join ACRTK on TK001=TL001 and TK002=TL002 " & _
                  " left join ACRTA on TA001=TL005 and TA002=TL006 " & _
                  " left join COPMA on  MA001=TA004 " & _
                  " where substring(TK001,1,2)='6D' and TK020='Y' and TA025 ='Y' " & whr
            'case when TA010>1 then TL017 else 0 end as rcvExpCol
            'Dim fldInsHash As Hashtable = New Hashtable
            'Dim fldUpdHash As Hashtable = New Hashtable
            'Dim whrHash As Hashtable = New Hashtable
            lstDetail = ""
            rcvLocCol = 0
            rcvLoc = 0
            balLoc = 0
            rcvExpCol = 0
            rcvExp = 0
            balExp = 0

            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)

                    If lstDetail <> .Item("ordDetail") Then

                        If lstDetail <> "" Then
                            Dim locRcv As Decimal = rcvLoc + rcvLocCol
                            Dim locBal As Decimal = balLoc - rcvLocCol
                            Dim ExpRcv As Decimal = rcvExp + rcvExpCol
                            Dim ExpBal As Decimal = balExp - rcvExpCol
                            fldInsHash.Add("exportAction", ExpRcv) 'Export Recv
                            fldInsHash.Add("exportBalance", ExpBal) 'Export Balance
                            fldInsHash.Add("localAction", locRcv) 'Local Rcv
                            fldInsHash.Add("localBalance", locBal) 'Local Balance
                            'Update Zone
                            fldUpdHash.Add("exportAction", "'" & ExpRcv & "'") 'Local Rcv
                            fldUpdHash.Add("exportBalance", "'" & ExpBal & "'") 'Local Balance
                            fldUpdHash.Add("localAction", "'" & locRcv & "'") 'Local Rcv
                            fldUpdHash.Add("localBalance", "'" & locBal & "'") 'Local Balance

                            Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                        End If

                        fldInsHash = New Hashtable
                        fldUpdHash = New Hashtable
                        whrHash = New Hashtable
                        whrHash.Add("docDetail", .Item("ordDetail")) ' order detail " doc type- doc no"
                        'Insert Zone
                        fldInsHash.Add("docDate", .Item("TA003")) 'order date
                        fldInsHash.Add("contactId", .Item("TA004")) 'customer ID
                        fldInsHash.Add("contactName", .Item("MA002")) 'Customer Name
                        fldInsHash.Add("section", .Item("MA017")) 'Invoice By
                        fldInsHash.Add("docBy", .Item("TA056")) 'Invoice By
                        fldInsHash.Add("exportAmount", .Item("amtExp")) 'Export Amount
                        fldInsHash.Add("localAmount", .Item("amtLoc")) 'Local Amount
                        rcvLoc = .Item("rcvLoc")
                        balLoc = .Item("balLoc")
                        rcvExp = .Item("rcvExp")
                        balExp = .Item("balExp")
                        rcvExpCol = 0
                        rcvLocCol = 0
                    End If
                    rcvLocCol = rcvLocCol + .Item("rcvLocCol")
                    rcvExpCol = rcvExpCol + .Item("rcvExpCol")
                    lstDetail = .Item("ordDetail")
                End With
            Next
            If lstDetail <> "" Then
                Dim locRcv As Decimal = rcvLoc + rcvLocCol
                Dim locBal As Decimal = balLoc - rcvLocCol
                Dim ExpRcv As Decimal = rcvExp + rcvExpCol
                Dim ExpBal As Decimal = balExp - rcvExpCol
                fldInsHash.Add("exportAction", ExpRcv) 'Export Recv
                fldInsHash.Add("exportBalance", ExpBal) 'Export Balance
                fldInsHash.Add("localAction", locRcv) 'Local Rcv
                fldInsHash.Add("localBalance", locBal) 'Local Balance
                'Update Zone
                fldUpdHash.Add("exportAction", "'" & ExpRcv & "'") 'Local Rcv
                fldUpdHash.Add("exportBalance", "'" & ExpBal & "'") 'Local Balance
                fldUpdHash.Add("localAction", "'" & locRcv & "'") 'Local Rcv
                fldUpdHash.Add("localBalance", "'" & locBal & "'") 'Local Balance

                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
            End If

        End If
    End Sub
    Sub genAccPayBalanceDetail(ByVal tempTable As String, ByVal accCode As String)
        CreateTempTable.createTempAccountStatus(tempTable)
        Dim dateTo As String = configDate.dateFormat2(tbDateTo.Text)
        Dim dateFrom As String = configDate.dateFormat2(tbDateFrom.Text)
        Dim whr As String = "",
            whr2 As String = "",
            ISQL As String = "",
            USQL As String = "",
            SQL As String = "",
            supCode As String = tbCode.Text.Trim
        'purchase invoice
        If supCode <> "" Then
            whr = " and TA004='" & supCode & "' "
        End If
        'If accCode <> "" Then
        '    whr = whr & " and MA041='" & accCode & "' "
        'End If
        If accCode <> "" Then
            If accCode = "1" Then
                whr = whr & " and MA041 not in('2111-1000','2111-3000') "
            Else
                whr = whr & " and MA041='" & accCode & "' "
            End If
        End If
        whr = whr & configDate.DateWhere("TA034", dateFrom, dateTo)
        SQL = " select TA001+'-'+TA002,TA034,TA014,TA004,MA003," & _
              " case when TA009>1 then case TA079 when '2' then -1*(TA028+TA029) else TA028+TA029 end else 0 end as amtExp," & _
              " case when TA009>1 then case TA079 when '2' then -1*TA030 else TA030 end else 0 end as payExp," & _
              " case when TA009>1 then case TA079 when '2' then -1*(TA028+TA029-TA030 ) else TA028+TA029-TA030  end else 0 end as balExp," & _
              " case TA079 when '2' then -1*(TA037+TA038) else TA037+TA038 end as amt," & _
              " case TA079 when '2' then -1*TA085 else TA085 end as pay," & _
              " case TA079 when '2' then -1*(TA037+TA038-TA085) else TA037+TA038-TA085 end as bal " & _
              " from HOOTHAI.dbo.ACPTA left join HOOTHAI.dbo.PURMA on MA001=TA004 " & _
              " where TA024='Y' and TA087 in('1','2') " & whr
        ISQL = "insert into " & tempTable & "(docDetail,docDate,docRefer,contactId,contactName,exportAmount,exportAction,exportBalance,localAmount,localAction,localBalance) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        'Other Paybles
        If tbCode.Text <> "" Then
            whr2 = " and TI004='" & tbCode.Text.Trim & "' "
        End If
        'If accCode <> "" Then
        '    whr2 = whr2 & " and MA041='" & accCode & "' "
        'End If
        If accCode <> "" Then
            If accCode = "1" Then
                whr2 = whr2 & " and MA041 not in('2111-1000','2111-3000') "
            Else
                whr2 = whr2 & " and MA041='" & accCode & "' "
            End If
        End If
        whr2 = whr2 & configDate.DateWhere("TI019", dateFrom, dateTo)

        SQL = " select TI001+'-'+TI002,TI019,TI012,TI004,TI036," & _
              " case when TI008>1 then case TI031 when '2' then -1*(TI015) else TI015 end else 0 end as amtExp," & _
              " case when TI008>1 then case TI031 when '2' then -1*(TI017) else TI017 end else 0 end as payExp," & _
              " case when TI008>1 then case TI031 when '2' then -1*(TI015-TI017) else TI015-TI017 end else 0 end as balExp," & _
              " case TI031 when '2' then -1*(TI016) else TI016 end as amt," & _
              " case TI031 when '2' then -1*(TI018) else TI018 end as pay," & _
              " case TI031 when '2' then -1*(TI016-TI018) else TI016-TI018 end as bal" & _
              " from HOOTHAI.dbo.ACPTI left join HOOTHAI.dbo.PURMA on MA001=TI004 " & _
              " where TI013='Y' and TI029 in ('1','2') " & whr2
        ISQL = "insert into " & tempTable & "(docDetail,docDate,docRefer,contactId,contactName,exportAmount,exportAction,exportBalance,localAmount,localAction,localBalance) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        If dateTo <> "" Then
            Dim Program As New DataTable
            'purchase invoice
            whr = whr & " and TK003 > '" & dateTo & "' "
            SQL = " select TA001+'-'+TA002 as docNo,TA034,TA014,TA004,MA002," & _
              " case when TA009>1 then case TA079 when '2' then -1*(TA028+TA029) else TA028+TA029 end else 0 end as amtExp," & _
              " case when TA009>1 then case TA079 when '2' then -1*TA030 else TA030 end else 0 end as payExp," & _
              " case when TA009>1 then case TA079 when '2' then -1*(TA028+TA029-TA030 ) else TA028+TA029-TA030  end else 0 end as balExp," & _
              " case TA079 when '2' then -1*(TA037+TA038) else TA037+TA038 end as amt," & _
              " case TA079 when '2' then -1*TA085 else TA085 end as pay," & _
              " case TA079 when '2' then -1*(TA037+TA038-TA085) else TA037+TA038-TA085 end as bal, " & _
              " TL019 as payExp2,TL020 as pay2 " & _
              " from ACPTL " &
              " left join ACPTK on TK001=TL001 and TK002=TL002 " & _
              " left join ACPTA on TA001=TL005 and TA002=TL006 " & _
              " left join PURMA on MA001=TA004 " & _
              " where TA024='Y' and TK020='Y' and TL004='1' " & whr & _
              " order by TA001,TA002 "
            Dim lastDocNo As String = "",
                payAmt As Decimal = 0,
                balAmt As Decimal = 0
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    If lastDocNo <> .Item("docNo") Then
                        payAmt = .Item("pay")
                        balAmt = .Item("bal")
                    End If

                    Dim fldInsHash As Hashtable = New Hashtable
                    Dim fldUpdHash As Hashtable = New Hashtable
                    Dim whrHash As Hashtable = New Hashtable

                    whrHash.Add("docDetail", .Item("docNo"))
                    'insert
                    fldInsHash.Add("docDate", .Item("TA034")) 'order date
                    fldInsHash.Add("docRefer", .Item("TA014")) 'doc refer
                    fldInsHash.Add("contactId", .Item("TA004")) 'contactId
                    fldInsHash.Add("contactName", .Item("MA002")) 'contactName
                    fldInsHash.Add("exportAmount", .Item("amtExp")) 'Export Amount
                    fldInsHash.Add("localAmount", .Item("amt")) 'Local Amount
                    fldInsHash.Add("exportAction", .Item("payExp")) 'Export Recv
                    fldInsHash.Add("exportBalance", .Item("balExp")) 'Export Balance
                    'Dim locRcv As Decimal = .Item("pay") - .Item("pay2")
                    'Dim locBal As Decimal = .Item("bal") + .Item("pay2")
                    payAmt = payAmt - .Item("pay2")
                    balAmt = balAmt + .Item("pay2")
                    fldInsHash.Add("localAction", payAmt) 'Local Rcv
                    fldInsHash.Add("localBalance", balAmt) 'Local Balance

                    'Update Zone
                    fldUpdHash.Add("localAction", payAmt) 'Local Rcv
                    fldUpdHash.Add("localBalance", balAmt) 'Local Balance
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                    lastDocNo = .Item("docNo")
                End With
            Next

            'other paybles
            whr2 = whr2 & " and TK003 > '" & dateTo & "' "
            SQL = " select TI001+'-'+TI002 as docNo,TI019,TI012,TI004,TI036," & _
                  " case when TI008>1 then case TI031 when '2' then -1*(TI015) else TI015 end else 0 end as amtExp," & _
                  " case when TI008>1 then case TI031 when '2' then -1*(TI017) else TI017 end else 0 end as payExp," & _
                  " case when TI008>1 then case TI031 when '2' then -1*(TI015-TI017) else TI015-TI017 end else 0 end as balExp," & _
                  " case TI031 when '2' then -1*(TI016) else TI016 end as amt," & _
                  " case TI031 when '2' then -1*(TI018) else TI018 end as pay," & _
                  " case TI031 when '2' then -1*(TI016-TI018) else TI016-TI018 end as bal," & _
                  " TL019 as payExp2,TL020 as pay2 " & _
                  " from ACPTL " &
                  " left join ACPTK on TK001=TL001 and TK002=TL002 " & _
                  " left join ACPTI on TI001=TL005 and TI002=TL006 " & _
                  " left join HOOTHAI.dbo.PURMA on MA001=TI004 " & _
                  " where TI013='Y' and TK020='Y' and TL004='2' " & whr2
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            lastDocNo = ""
            payAmt = 0
            balAmt = 0
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    If lastDocNo <> .Item("docNo") Then
                        payAmt = .Item("pay")
                        balAmt = .Item("bal")
                    End If
                    Dim fldInsHash As Hashtable = New Hashtable
                    Dim fldUpdHash As Hashtable = New Hashtable
                    Dim whrHash As Hashtable = New Hashtable
                    whrHash.Add("docDetail", .Item("docNo"))
                    'Insert Zone
                    fldInsHash.Add("docDate", .Item("TI019")) 'order date
                    fldInsHash.Add("docRefer", .Item("TI012")) 'doc refer
                    fldInsHash.Add("contactId", .Item("TI004")) 'contactId
                    fldInsHash.Add("contactName", .Item("TI036")) 'contactName
                    fldInsHash.Add("exportAmount", .Item("amtExp")) 'Export Amount
                    fldInsHash.Add("localAmount", .Item("amt")) 'Local Amount
                    fldInsHash.Add("exportAction", .Item("payExp")) 'Export Recv
                    fldInsHash.Add("exportBalance", .Item("balExp")) 'Export Balance
                    'Dim locRcv As Decimal = .Item("pay") - .Item("pay2")
                    'Dim locBal As Decimal = .Item("bal") + .Item("pay2")
                    payAmt = payAmt - .Item("pay2")
                    balAmt = balAmt + .Item("pay2")
                    fldInsHash.Add("localAction", payAmt) 'Local Rcv
                    fldInsHash.Add("localBalance", balAmt) 'Local Balance
                    'Update Zone
                    fldUpdHash.Add("localAction", payAmt) 'Local Rcv
                    fldUpdHash.Add("localBalance", balAmt) 'Local Balance
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                    lastDocNo = .Item("docNo")
                End With
            Next
        End If
    End Sub

    Sub genPurchaseData(ByVal tempTable As String, ByVal accCode As String)
        CreateTempTable.createTempAccountTaxPurchase(tempTable)
        Dim dateTo As String = configDate.dateFormat2(tbDateTo.Text)
        Dim dateFrom As String = configDate.dateFormat2(tbDateFrom.Text)
        Dim whr As String = "",
            whr2 As String = "",
            ISQL As String = "",
            USQL As String = "",
            SQL As String = ""
        'purchase invoice
        If tbCode.Text <> "" Then
            whr = " and TI004='" & tbCode.Text.Trim & "' "
        End If
        If accCode <> "" Then
            If accCode = "1" Then
                whr = whr & " and MA041 not in('2111-1000','2111-3000') "
            Else
                whr = whr & " and MA041='" & accCode & "' "
            End If
        End If
        whr = whr & configDate.DateWhere("TA034", dateFrom, dateTo)
        SQL = " select TA001+'-'+TA002,TA034,TA014,MA003," & _
              " case TA079 when '2' then -1*TA037 else TA037 end as amt," & _
              " case TA079 when '2' then -1*TA038 else TA038 end as vat, " & _
              " case when (select PURMA.MA062 from HOOTHAI.dbo.PURMA where PURMA.MA001 = TA004 ) <> '' then " & _
              " (select PURMA.MA062 from HOOTHAI.dbo.PURMA where PURMA.MA001 = TA004) else " & _
              " (select COPMA.MA010 from HOOTHAI.dbo.COPMA where MA001=TA004 ) end as tax " & _
              " from HOOTHAI.dbo.ACPTA left join HOOTHAI.dbo.PURMA on MA001=TA004 " & _
              " where TA024='Y' " & whr
        ISQL = "insert into " & tempTable & "(docDetail,docDate,docRefer,contactDetail,localAmount,localVat,tax) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        'Other Paybles
        If tbCode.Text <> "" Then
            whr2 = " and TI004='" & tbCode.Text.Trim & "' "
        End If
        If accCode <> "" Then
            If accCode = "1" Then
                whr2 = whr2 & " and MA041 not in('2111-1000','2111-3000') "
            Else
                whr2 = whr2 & " and MA041='" & accCode & "' "
            End If
        End If
        whr2 = whr2 & configDate.DateWhere("TI019", dateFrom, dateTo)
        SQL = " select TI001+'-'+TI002,TI019,TI012,TI036," & _
              " case TI031 when '2' then -1*(TI016-(TI016*0.07)) else TI016-(TI016*0.07) end as amt," & _
              " case TI031 when '2' then -1*(TI016*0.07) else TI016*0.07 end as vat, " & _
              " case when (select PURMA.MA062 from HOOTHAI.dbo.PURMA where PURMA.MA001 = TI004 ) <> '' then " & _
              " (select PURMA.MA062 from HOOTHAI.dbo.PURMA where PURMA.MA001 = TI004) else " & _
              " (select COPMA.MA010 from HOOTHAI.dbo.COPMA where MA001=TI004 ) end as tax " & _
              " from HOOTHAI.dbo.ACPTI " & _
              " where TI013='Y' " & whr2
        '" from HOOTHAI.dbo.ACPTI left join HOOTHAI.dbo.COPMA on  MA001=TI004 " & _
        ISQL = "insert into " & tempTable & "(docDetail,docDate,docRefer,contactDetail,localAmount,localVat,tax) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub genInvoiceOverDue(ByVal tempTable As String, ByVal accCode As String, Optional ByVal noPresent As Boolean = True)
        CreateTempTable.createTempAccountOverDue(tempTable)
        Dim dateTo As String = configDate.dateFormat2(tbDateTo.Text)
        Dim dateFrom As String = configDate.dateFormat2(tbDateFrom.Text)
        Dim Program As New DataTable
        Dim whr As String = "",
            ISQL As String = "",
            USQL As String = "",
            SQL As String = ""
        'Dim dateToday As String = DateTime.Today.ToString("yyyyMMdd")
        Dim dateToday As String = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))

        If tbCode.Text <> "" Then
            whr = " and TA004='" & tbCode.Text.Trim & "' "
        End If
        If accCode <> "" Then
            whr = " and MA047='" & accCode & "' "
        End If
        If noPresent Then
            whr = whr & configDate.DateWhere("TA038", dateFrom, dateTo)
        End If
        SQL = " select TA001+'-'+TA002,TA003,TA020,TA043,TA004,MA002," & _
              " case when TA010>1 then case TA079 when '2' then -1*(TA029+TA030-TA099) else TA029+TA030-TA099 end else 0 end as balExp," & _
              " case TA079 when '2' then -1*(TA041+TA042-TA098) else TA041+TA042-TA098 end as balLoc" & _
              " from HOOTHAI.dbo.ACRTA left join HOOTHAI.dbo.COPMA on MA001=TA004 " & _
              " where TA100 in ('1','2') and TA025 ='Y' and TA020<'" & dateToday & "' " & whr
        'Label8.Text = SQL
        ISQL = "insert into " & tempTable & "(docDetail,docDate,dueDate,creditTerm,contactId,contactName,exportBalance,localBalance) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        If noPresent And dateTo <> "" Then
            whr = whr & " and TK003 > '" & dateTo & "' "
            SQL = " select TA001+'-'+TA002 as ordDetail,TA003,TA020,TA043,TA004,MA002, " & _
                  " case when TA010>1 then case TA079 when '2' then -1*(TA029+TA030-TA099) else TA029+TA030-TA099 end else 0 end as balExp," & _
                  " case TA079 when '2' then -1*(TA041+TA042-TA098) else TA041+TA042-TA098 end as balLoc," & _
                  " case when TA010>1 then TL017 else 0 end as rcvExpCol, TL020 as rcvLocCol" & _
                  " from ACRTL left join ACRTK on TK001=TL001 and TK002=TL002 " & _
                  " left join ACRTA on TA001=TL005 and TA002=TL006 " & _
                  " left join COPMA on  MA001=TA004 " & _
                  " where TK020='Y' and TA025 ='Y' and TA020<'" & dateToday & "' " & whr

            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    Dim fldInsHash As Hashtable = New Hashtable
                    Dim fldUpdHash As Hashtable = New Hashtable
                    Dim whrHash As Hashtable = New Hashtable
                    whrHash.Add("docDetail", .Item("ordDetail")) ' order detail " doc type- doc no"
                    'Insert Zone
                    fldInsHash.Add("docDate", .Item("TA003")) 'order date
                    fldInsHash.Add("dueDate", .Item("TA020")) 'order date
                    fldInsHash.Add("contactId", .Item("TA004")) 'customer ID
                    fldInsHash.Add("contactName", .Item("MA002")) 'Customer Name
                    fldInsHash.Add("creditTerm", .Item("TA043")) 'Invoice By
                    Dim locBal As Decimal = .Item("balLoc") + .Item("rcvLocCol")
                    fldInsHash.Add("exportBalance", .Item("balExp")) 'Export Balance
                    fldInsHash.Add("localBalance", locBal) 'Local Balance
                    'Update Zone
                    fldUpdHash.Add("localBalance", locBal) 'Local Balance
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
                End With
            Next
        End If

    End Sub
End Class