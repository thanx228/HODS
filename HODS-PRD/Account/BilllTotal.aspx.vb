Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization



Public Class BilllTotal
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL
    Dim aaa As New show_message
    Dim CreatTable As New CreateTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            CreatTable.CreateBillTotal()
            GridView1.Visible = False
        End If
    End Sub

    Function SpellNumber(ByVal MyNumber)
        Dim Dollars, Cents, Temp
        Dim DecimalPlace, Count
        Dim Place(9) As String

        Place(2) = " THOUSAND "
        Place(3) = " MILLION "
        Place(4) = " BILLION "
        Place(5) = " TRILLION "
        ' String representation of amount.
        MyNumber = Trim(Str(MyNumber))
        ' Position of decimal place 0 if none.
        DecimalPlace = InStr(MyNumber, ".")
        ' Convert cents and set MyNumber to dollar amount.
        If DecimalPlace > 0 Then
            Cents = GetTens(Left(Mid(MyNumber, DecimalPlace + 1) & _
                      "00", 2))
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If
        Count = 1
        Do While MyNumber <> ""
            Temp = GetHundreds(Right(MyNumber, 3))
            If Temp <> "" Then Dollars = Temp & Place(Count) & Dollars
            If Len(MyNumber) > 3 Then
                MyNumber = Left(MyNumber, Len(MyNumber) - 3)
            Else
                MyNumber = ""
            End If
            Count = Count + 1
        Loop
        Select Case Dollars
            Case ""
                Dollars = "NO DOLLARS"
            Case "One"
                Dollars = "ONE DOLLARS"
            Case Else
                Dollars = Dollars '& " Dollars"
        End Select
        Select Case Cents
            Case ""
                'Cents = " and No Cents"
            Case "One"
                Cents = " AND ONE CENT"
            Case Else
                Cents = " AND " & Cents & " CENTS"
        End Select
        txtbath.Text = Dollars & Cents & " ONLY"

    End Function

    ' Converts a number from 100-999 into text
    Function GetHundreds(ByVal MyNumber)
        Dim Result As String
        If Val(MyNumber) = 0 Then Exit Function
        MyNumber = Right("000" & MyNumber, 3)
        ' Convert the hundreds place.
        If Mid(MyNumber, 1, 1) <> "0" Then
            Result = GetDigit(Mid(MyNumber, 1, 1)) & " HUNDRED "
        End If
        ' Convert the tens and ones place.
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & GetTens(Mid(MyNumber, 2))
        Else
            Result = Result & GetDigit(Mid(MyNumber, 3))
        End If
        GetHundreds = Result
    End Function

    ' Converts a number from 10 to 99 into text.
    Function GetTens(ByVal TensText)
        Dim Result As String
        Result = ""           ' Null out the temporary function value.
        If Val(Left(TensText, 1)) = 1 Then   ' If value between 10-19...
            Select Case Val(TensText)
                Case 10 : Result = "TEN"
                Case 11 : Result = "ELEVEN"
                Case 12 : Result = "TWELVE"
                Case 13 : Result = "THIRTEEN"
                Case 14 : Result = "FOURTEEN"
                Case 15 : Result = "FIFTEEN"
                Case 16 : Result = "SIXTEEN"
                Case 17 : Result = "SEVENTEEN"
                Case 18 : Result = "EIGHTEEN"
                Case 19 : Result = "NINETEEN"
                Case Else
            End Select
        Else                                 ' If value between 20-99...
            Select Case Val(Left(TensText, 1))
                Case 2 : Result = "TWENTY "
                Case 3 : Result = "THIRTY "
                Case 4 : Result = "FORTY "
                Case 5 : Result = "FIFTY "
                Case 6 : Result = "SIXTY "
                Case 7 : Result = "SEVENTY "
                Case 8 : Result = "EIGHTY "
                Case 9 : Result = "NINETY "
                Case Else
            End Select
            Result = Result & GetDigit _
                (Right(TensText, 1))  ' Retrieve ones place.
        End If
        GetTens = Result
    End Function

    ' Converts a number from 1 to 9 into text.
    Function GetDigit(ByVal Digit)
        Select Case Val(Digit)
            Case 1 : GetDigit = "ONE"
            Case 2 : GetDigit = "TWO"
            Case 3 : GetDigit = "THREE"
            Case 4 : GetDigit = "FOUR"
            Case 5 : GetDigit = "FIVE"
            Case 6 : GetDigit = "SIX"
            Case 7 : GetDigit = "SEVEN"
            Case 8 : GetDigit = "EIGHT"
            Case 9 : GetDigit = "NINE"
            Case Else : GetDigit = ""
        End Select
    End Function

    Protected Sub BuSearch_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        GridView1.Visible = True
        SqlDataSource1.SelectCommand = "select * from Billhead H left join BillLine L on (H.BillNo = L.BillNo) where H.BeDate between '" & txtfrom.Text & "' and '" & txtto.Text & "'"
        GridView1.DataBind()
        LCount.Text = GridView1.Rows.Count

    End Sub

    Protected Sub BuPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuPrint.Click

        GridView1.Visible = True
        Dim BillTotal As String = "delete from BillTotal"
        Conn_sql.Exec_Sql(BillTotal, Conn_sql.MIS_ConnectionString)

        Dim SelSQL As String = ""
        SelSQL = SelSQL & "select * from Billhead H left join BillLine L on (H.BillNo = L.BillNo)"
        SelSQL = SelSQL & " where H.BeDate between '" & txtfrom.Text & "' and '" & txtto.Text & "'"

        Dim aa As New Data.DataTable
        aa = Conn_sql.Get_DataReader(SelSQL, Conn_sql.MIS_ConnectionString)

        If aa.Rows.Count > 0 Then
            For i As Integer = 0 To aa.Rows.Count - 1
                Dim BillNo As String = aa.Rows(i).Item("BillShow").ToString.Replace(" ", "")
                Dim OrderDate As String = aa.Rows(i).Item("Date").ToString.Replace(" ", "")
                Dim Cid As String = aa.Rows(i).Item("CustID").ToString.Replace(" ", "")
                Dim Cname As String = aa.Rows(i).Item("CustName").ToString.Replace(" ", "")
                Dim Address As String = aa.Rows(i).Item("Address1") & aa.Rows(i).Item("Address2")
                Dim Invoice As String = aa.Rows(i).Item("InvoiceH").ToString.Replace(" ", "") & "-" & aa.Rows(i).Item("InvoiceNo").ToString.Replace("NULL", "").Replace(" ", "")
                Dim Payment As String = aa.Rows(i).Item("Payment").ToString.Replace(" ", "").ToString.Replace("NULL", "").Replace(" ", "")
                Dim Balance As String = aa.Rows(i).Item("Balance").ToString.Replace(" ", "").ToString.Replace("NULL", "").Replace(" ", "")

                Dim InSQL As String = "Insert into BillTotal (BillNo,Date,Cid,CName,Address,Invoice,Payment,Balance)"
                InSQL = InSQL & " Values('" & BillNo & "','" & OrderDate & "','" & Cid & "',"
                InSQL = InSQL & "'" & Cname & "','" & Address & "','" & Invoice & "',"
                InSQL = InSQL & "'" & Payment & "','" & Balance & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
            Next
        End If

        Dim StrSum As String = "select SUM(Balance) from BillTotal"
        Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        Dim Amount As String = "Update BillTotal set Amount =N'" & SumBal & "'"
        Conn_sql.Exec_Sql(Amount, Conn_sql.MIS_ConnectionString)

        SpellNumber(SumBal)

        'Save AmountText
        Dim AmountText As String = "Update BillTotal set AmountText =N'" & txtbath.Text & "'"
        Conn_sql.Exec_Sql(AmountText, Conn_sql.MIS_ConnectionString)

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('BillTotal1.aspx?BillNo=');", True)


    End Sub

End Class