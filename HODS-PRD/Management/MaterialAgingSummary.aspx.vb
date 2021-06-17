Imports System.Globalization
Public Class MaterialAgingSummary
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("UserName") = "" Then
            '    Response.Redirect("../Login.aspx")
            'End If
            btExport.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("MaterialAgingSummary" & Session("UserName"), gvShow)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempMatInvAgingSum" & Session("UserName")
        CreateTempTable.createTempMatInvAgingSummary(tempTable)

        Dim SQL As String = "",
            WHR As String = "",
            codeType As String = ddlCon.Text

        If codeType = "0" Then
            WHR = " and (substring(LA001,3,1) in ('1','4') and LA009 in ('2201','2202','2900'))"
        Else
            Select Case codeType
                Case "1" 'Materials
                    WHR = " and substring(LA001,3,1) ='1' and LA009 ='2201' "
                Case "2" 'Spare Part
                    WHR = " and substring(LA001,3,1) ='4' and substring(LA001,3,2) <>'43' and LA009 in ('2201','2202') "
                Case "3" 'Packing
                    WHR = " and substring(LA001,3,1) ='4' and substring(LA001,3,2) ='43' and LA009 ='2900' "
            End Select
        End If
        Dim DueDate As String = tbDate.Text
        If DueDate <> "" Then
            DueDate = configDate.dateFormat2(DueDate)
        Else
            DueDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        End If


        SQL = " select LA001,sum(LA005*LA011) as QTY,sum(LA005*LA013) as AMT from INVLA " & _
              " where LA004 <='" & DueDate & "' " & WHR & _
              " group by LA001 having sum(LA005*LA011)>0 or sum(LA005*LA013)>0  order by LA001 "

        Dim Program As New DataTable
        Dim InvDate As Date = DateTime.ParseExact(DueDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastPos As String = ""
        Dim item As New Dictionary(Of String, Decimal),
            qty As New Dictionary(Of String, Decimal),
            amt As New Dictionary(Of String, Decimal)

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim code As String = Program.Rows(i).Item("LA001").ToString.Trim,
                cQty As Decimal = CDec(Program.Rows(i).Item("QTY")),
                cAmt As Decimal = CDec(Program.Rows(i).Item("AMT"))
            Dim sSql As String = " select LA001,max(LA004) as eDate from INVLA where LA005=1 and LA004 <='" & DueDate & "' " & _
                                 " and LA001='" & code & "' and LA011>0  group by LA001 "
            Dim sProgram As New DataTable
            sProgram = Conn_SQL.Get_DataReader(sSql, Conn_SQL.ERP_ConnectionString)
            Dim LastDate As Date = DateTime.ParseExact(sProgram.Rows(0).Item("eDate").ToString.Trim, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim dateAging As Integer = DateDiff(DateInterval.Day, LastDate, InvDate)
            'check code 
            Dim inPos As String = chkCode(code)
            
            If lastPos <> inPos Then
                If lastPos <> "" Then
                    'item
                    saveData(tempTable, lastPos, 1, item)
                    'qty
                    saveData(tempTable, lastPos, 2, qty)
                    'amt
                    saveData(tempTable, lastPos, 3, amt)
                End If
                item = New Dictionary(Of String, Decimal)
                qty = New Dictionary(Of String, Decimal)
                amt = New Dictionary(Of String, Decimal)
            End If
            Dim fldName As String = ""
            If dateAging <= 30 Then
                fldName = "A030"
            ElseIf dateAging < 91 Then
                fldName = "A090"
            ElseIf dateAging < 180 Then
                fldName = "A180"
            ElseIf dateAging < 271 Then
                fldName = "A270"
            ElseIf dateAging < 366 Then
                fldName = "A365"
            Else
                fldName = "A366"
            End If
            If item.ContainsKey(fldName) Then
                item.Item(fldName) = item.Item(fldName) + 1
                qty.Item(fldName) = qty.Item(fldName) + cQty
                amt.Item(fldName) = amt.Item(fldName) + cAmt
            Else
                item.Add(fldName, 1)
                qty.Add(fldName, cQty)
                amt.Add(fldName, cAmt)
            End If

            lastPos = inPos
        Next
        If Program.Rows.Count - 1 > 0 Then
            'item
            saveData(tempTable, lastPos, 1, item)
            'qty
            saveData(tempTable, lastPos, 2, qty)
            'amt
            saveData(tempTable, lastPos, 3, amt)
        End If

        'PR
        item = New Dictionary(Of String, Decimal)
        qty = New Dictionary(Of String, Decimal)
        amt = New Dictionary(Of String, Decimal)
        lastPos = ""
        If codeType = "0" Then
            WHR = " and (substring(B.TB004,3,1) in ('1','4') )"
        Else
            Select Case codeType
                Case "1" 'Materials
                    WHR = " and substring(B.TB004,3,1) ='1' "
                Case "2" 'Spare Part
                    WHR = " and substring(B.TB004,3,1) ='4' and substring(B.TB004,3,2) <>'43' "
                Case "3" 'Packing
                    WHR = " and substring(B.TB004,3,1) ='4' and substring(B.TB004,3,2) ='43' "
            End Select
        End If
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from PURTR R " & _
              " left join PURTB B on B.TB001=R.TR001 and B.TB002=R.TR002 and TB003=TR003 " & _
              " where R.TR019='' and B.TB039='N' " & WHR & _
              " group by B.TB004 order by B.TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'Dim itemPrc As Decimal = 0
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim code As String = Program.Rows(i).Item("item").ToString.Trim
            Dim cQty As Decimal = Program.Rows(i).Item("pr")
            
            Dim sSql As String = "select MB046 from INVMB where MB001='" & code & "' "
            Dim sProgram As New DataTable
            sProgram = Conn_SQL.Get_DataReader(sSql, Conn_SQL.ERP_ConnectionString)
            Dim cAmt As Decimal = cQty * CDec(sProgram.Rows(0).Item("MB046"))
            'check code 
            Dim inPos As String = chkCode(code)
            If lastPos <> inPos Then
                If lastPos <> "" Then
                    'item
                    saveData(tempTable, lastPos, 1, item)
                    'qty
                    saveData(tempTable, lastPos, 2, qty)
                    'amt
                    saveData(tempTable, lastPos, 3, amt)
                End If
                item = New Dictionary(Of String, Decimal)
                qty = New Dictionary(Of String, Decimal)
                amt = New Dictionary(Of String, Decimal)
            End If
            Dim fldName As String = "pr"
            If item.ContainsKey(fldName) Then
                item.Item(fldName) = item.Item(fldName) + 1
                qty.Item(fldName) = qty.Item(fldName) + cQty
                amt.Item(fldName) = amt.Item(fldName) + cAmt
            Else
                item.Add(fldName, 1)
                qty.Add(fldName, cQty)
                amt.Add(fldName, cAmt)
            End If
            lastPos = inPos
        Next
        If Program.Rows.Count - 1 > 0 Then
            'item
            saveData(tempTable, lastPos, 1, item)
            'qty
            saveData(tempTable, lastPos, 2, qty)
            'amt
            saveData(tempTable, lastPos, 3, amt)
        End If
        'PO
        item = New Dictionary(Of String, Decimal)
        qty = New Dictionary(Of String, Decimal)
        amt = New Dictionary(Of String, Decimal)
        lastPos = ""
        If codeType = "0" Then
            WHR = " and (substring(TD004,3,1) in ('1','4') )"
        Else
            Select Case codeType
                Case "1" 'Materials
                    WHR = " and substring(TD004,3,1) ='1' "
                Case "2" 'Spare Part
                    WHR = " and substring(TD004,3,1) ='4' and substring(TD004,3,2) <>'43' "
                Case "3" 'Packing
                    WHR = " and substring(TD004,3,1) ='4' and substring(TD004,3,2) ='43' "
            End Select
        End If
        SQL = " select TD004 as item,SUM(isnull(TD008,0)-isnull(TD015,0)) as po,SUM((isnull(TD008,0)-isnull(TD015,0))*TD010)as amt from PURTD  " & _
              " where TD016='N' " & WHR & " group by TD004  order by TD004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'Dim itemPrc As Decimal = 0
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim code As String = Program.Rows(i).Item("item").ToString.Trim
            Dim cQty As Decimal = CDec(Program.Rows(i).Item("po"))
            Dim cAmt As Decimal = CDec(Program.Rows(i).Item("amt"))
            'check code 
            Dim inPos As String = chkCode(code)
            If lastPos <> inPos Then
                If lastPos <> "" Then
                    'item
                    saveData(tempTable, lastPos, 1, item)
                    'qty
                    saveData(tempTable, lastPos, 2, qty)
                    'amt
                    saveData(tempTable, lastPos, 3, amt)
                End If
                item = New Dictionary(Of String, Decimal)
                qty = New Dictionary(Of String, Decimal)
                amt = New Dictionary(Of String, Decimal)
            End If
            Dim fldName As String = "po"
            If item.ContainsKey(fldName) Then
                item.Item(fldName) = item.Item(fldName) + 1
                qty.Item(fldName) = qty.Item(fldName) + cQty
                amt.Item(fldName) = amt.Item(fldName) + cAmt
            Else
                item.Add(fldName, 1)
                qty.Add(fldName, cQty)
                amt.Add(fldName, cAmt)
            End If
            lastPos = inPos
        Next
        If Program.Rows.Count - 1 > 0 Then
            'item
            saveData(tempTable, lastPos, 1, item)
            'qty
            saveData(tempTable, lastPos, 2, qty)
            'amt
            saveData(tempTable, lastPos, 3, amt)
        End If

        Dim fld As String = ""
        If codeType = "0" Then
            'sub total
            Dim fldList As String() = {"A030", "A090", "A180", "A270", "A365", "A366", "pr", "po"}
            For Each fldName As String In fldList
                fld = fld & ",sum(" & fldName & ")as " & fldName & ""
            Next
            SQL = "select txtSeq" & fld & " from " & tempTable & " group by txtSeq "
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                item = New Dictionary(Of String, Decimal)
                For Each fldName As String In fldList
                    item.Add(fldName, Program.Rows(i).Item(fldName))
                Next
                saveData(tempTable, "99", Program.Rows(i).Item("txtSeq"), item)
            Next
        End If
        Dim fldList2 As New Dictionary(Of String, String)
        fldList2.Add("A030", "'<=30 Days'")
        fldList2.Add("A090", "'31-90 Days'")
        fldList2.Add("A180", "'91-180 Days'")
        fldList2.Add("A270", "'181-270 Days'")
        fldList2.Add("A365", "'271-365 Days'")
        fldList2.Add("A366", "'>one year'")
        fldList2.Add("pr", "'PR'")
        fldList2.Add("po", "'PO'")
        Dim pair As New KeyValuePair(Of String, String)
        fld = " case codeType when '1' then 'Materials' when '4' then 'Spare Part' when '43' then 'Packaging' when '99' then 'Sub Total' end as 'Code Type',"
        fld = fld & " case txtSeq when '1' then 'Total Item' when '2' then 'Total Qty' when '3' then '' end as 'MSG', "
        Dim fldTot As String = ""
        For Each pair In fldList2
            fld = fld & " cast(" & pair.Key & " as decimal(15,2)) as " & pair.Value & ","
            fldTot = fldTot & pair.Key & "+"
        Next
        fld = fld & " cast(" & fldTot.Substring(0, fldTot.Length - 1) & " as decimal(15,2)) as 'Total'"
        SQL = "select " & fld & " from " & tempTable
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Private Sub gvShow_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvShow.DataBound
        ControlForm.MergeGridview(gvShow, 2)
    End Sub
    Protected Sub saveData(temptable As String, CodeType As String, txtSeq As Integer, item As Dictionary(Of String, Decimal))
        'item
        Dim pair As New KeyValuePair(Of String, Decimal)
        Dim fldInsHash As Hashtable = New Hashtable
        Dim fldUpdHash As Hashtable = New Hashtable
        Dim whrHash As Hashtable = New Hashtable
        whrHash.Add("codeType", CodeType) ' order detail " doc type- doc no"
        whrHash.Add("txtSeq", txtSeq) ' order detail " doc type- doc no"
        'codeType,txtSeq
        For Each pair In item
            'add
            fldInsHash.Add(pair.Key, pair.Value)
            'update
            fldUpdHash.Add(pair.Key, pair.Key & "+'" & pair.Value & "'")
        Next
        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(temptable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
    End Sub

    Protected Function chkCode(code As String) As String
        Dim inPos As String = ""
        Dim tempPos As String = code.Trim.Substring(2, 1)
        If tempPos = "1" Then
            inPos = tempPos
        Else
            tempPos = code.Trim.Substring(2, 2)
            If tempPos = "43" Then
                inPos = tempPos
            Else
                inPos = code.Trim.Substring(2, 1)
            End If
        End If
        Return inPos
    End Function
End Class