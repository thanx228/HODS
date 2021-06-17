Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports MIS_HTI.DataControl
Public Class BillingReceipts
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL
    Dim aaa As New show_message
    Dim CreateTable As New CreateTable
    Dim outcont As New OutputControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            CreateTable.CreateBillPurHead()
            CreateTable.CreateBillPurLine()
            CreateTable.CreateBillPurMonth()

        End If

        GridView1.Visible = False
        GridView2.Visible = False
        GridView5.Visible = False

        If Not Page.IsPostBack Then

            'Dim strSql As String = "SELECT MA001,MA001+MA002 AS ConcatField FROM PURMA order by MA001"
            'Dim program As Data.DataTable = Conn_sql.Get_DataReader(strSql, Conn_sql.ERP_ConnectionString)
            'DDLSup.DataSource = program
            'DDLSup.DataTextField = "ConcatField"
            'DDLSup.DataValueField = "MA001"
            'DDLSup.DataBind()

            Dim strSql As String = "select BillShow from BillPurHead order by BillShow desc "
            Dim program As Data.DataTable = Conn_sql.Get_DataReader(strSql, Conn_sql.MIS_ConnectionString)
            DDLBill1.DataSource = program
            DDLBill1.DataTextField = "BillShow"
            DDLBill1.DataValueField = "BillShow"
            DDLBill1.DataBind()

            Dim strSql2 As String = "select BillShow,BillNo from BillPurHead"
            Dim program2 As Data.DataTable = Conn_sql.Get_DataReader(strSql2, Conn_sql.MIS_ConnectionString)
            DDlBill2.DataSource = program2
            DDlBill2.DataTextField = "BillNo"
            DDlBill2.DataValueField = "BillShow"
            DDlBill2.DataBind()


            UcDdlYearEdit.yyyy = ""
            UcDdlMonthEdit.mm = ""

            TabContainer1.ActiveTabIndex = 0

        End If

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("dd/MM/yyyy")
        txtdate.Text = resultdate

    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        GridView1.Visible = True
        Dim aa As String = CStr(txtfrom.Text)
        Dim bb As String = CStr(txtto.Text)
        Dim ake As String = "and"

        If txtfrom.Text <> "" And txtto.Text <> "" Then
            'SqlDataSource1.SelectCommand = "SELECT A.TA001,A.TA002,A.TA004,A.TA021,A.TA015,A.TA021,A.TA020 FROM  [JINPAO80].[dbo].[ACPTA] A LEFT JOIN [DBMIS].[dbo].[BillPurLine] B ON(B.InvoiceNo = A.TA002 and B.InvoiceH = A.TA001) where A.TA015 between '" & aa & "'" & ake & "'" & bb & "'" & ake & " A.TA004 ='" & DDLSup.SelectedValue & "' and A.TA021 not in(select RemarkInvoice from [DBMIS].[dbo].[BillPurLine]) GROUP BY A.TA001, A.TA002, A.TA004, A.TA015, A.TA021, A.TA020"
            'Dim TB015 As Double = Conn_sql.Get_value(StrAmount, Conn_sql.ERP_ConnectionString)
            'Dim TAX As Double = Conn_sql.Get_value(StrTax, Conn_sql.ERP_ConnectionString)

            'Dim StrAmt As String = "select SUM(TB015) from ACPTB where TB001 =TA001 and TB002 =TA002"
            'Dim StrTax As String = "select SUM(TB016) from ACPTB where TB001 =TA001 and TB002 =TA002"
            'Dim StrAll As String = "select SUM(TB015+TB016) from ACPTB where TB001 =TA001 and TB002 =TA002"

            SqlDataSource1.SelectCommand = "SELECT DISTINCT TA001,TA002,TA004,TA015,TA021,TA020,TA079,TA037 as AMT,TA038 as TAX,TA037+TA038 as NET FROM [HOOTHAI].[dbo].[ACPTA] WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillPurLine] where ACPTA.TA001 = BillPurLine.InvoiceH and ACPTA.TA002 = BillPurLine.InvoiceNo) and TA015 between '" & aa & "'" & ake & "'" & bb & "'" & ake & " TA004 ='" & DDLSup.SelectedValue & "'"

            'if Delete Bill
            'SqlDataSource1.SelectCommand = "SELECT DISTINCT TA001,TA002,TA004,TA015,TA021,TA020,TA079,(" & StrAmt & ") as AMT,(" & StrTax & ") as TAX,(" & StrAll & ") as NET FROM [HOOTHAI].[dbo].[ACPTA] " & _
            '    " WHERE TA015 between '" & aa & "'" & ake & "'" & bb & "'" & ake & " TA004 ='" & DDLSup.SelectedValue & "'"

        End If

        Me.GridView1.DataBind()
        AutoGenerateID_Part()

        'GetValue()

    End Sub

    Private Sub ClearData()

        txtbilling.Text = ""
        txtsupid.Text = ""
        txtname.Text = ""
        txtaddress1.Text = ""
        txtaddress2.Text = ""
        'txtdate.Text = ""
        txtpayment.Text = ""
        txtbillingno.Text = ""
        txtfrom.Text = ""
        txtto.Text = ""
        DDLSup.SelectedIndex = 0

        'Page.DataBind()


    End Sub

    Private Sub AutoGenerateID_Part()

        Dim conn As New SqlConnection
        'Dim str As String = "Data Source=192.168.50.1;Initial Catalog= DBMIS;User Id=sa;Password=Alex0717"
        Dim str As String = Conn_sql.MIS_ConnectionString

        With conn
            If .State = ConnectionState.Open Then
                .ConnectionString = str
                .Open()
            End If
        End With

        Dim sqlTmp As String = ""
        Dim comTmp As SqlCommand = New SqlCommand
        Dim drTmp As SqlDataReader
        Dim tmpidpart As Integer = 0

        sqlTmp = "SELECT TOP 1 BillNo FROM BillPurHead ORDER BY BillNo DESC"

        With conn
            If .State = ConnectionState.Open Then .Close()
            .ConnectionString = str
            .Open()
        End With

        Dim BR As String = "BR"

        Try
            With comTmp
                .CommandType = CommandType.Text
                .CommandText = sqlTmp
                .Connection = conn
                drTmp = .ExecuteReader()
                drTmp.Read()
                tmpidpart = CInt(drTmp.Item("BillNo"))
                tmpidpart = tmpidpart + 1

                txtbillingno.Text = tmpidpart.ToString("0000000")
                txtbilling.Text = BR & tmpidpart.ToString("0000000")
            End With

        Catch

            txtbillingno.Text = "0000001"
            txtbilling.Text = BR & "0000001"
        End Try
        drTmp.Close()

    End Sub

    Protected Sub Busave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busave.Click

        If txtbilling.Text = "" Then
            show_message.ShowMessage(Page, "Please Search Data", UpdatePanel1)
            Exit Sub
        End If

        Dim strSql As String
        Dim Reamrk As String = "Contract Accounting & Finance Ext. 148-149"
        strSql = "insert into BillPurHead (BillNo,SupID,SupName,Address1,Address2,Date,Payment,BillShow,CreateBy,Remark) values('" & txtbillingno.Text & "','" & txtsupid.Text & "','" & txtname.Text & "','" & txtaddress1.Text & "','" & txtaddress2.Text & "','" & txtdate.Text & "','" & txtpayment.Text & "','" & txtbilling.Text & "','" & Session("Username") & "','" & Reamrk & "')"

        Conn_sql.Exec_Sql(strSql, Conn_sql.MIS_ConnectionString)

        Dim StrSqlLine As String = ""
        For M_count As Integer = 0 To GridView1.Rows.Count - 1

            Dim cb As CheckBox = GridView1.Rows(M_count).FindControl("CheckBox2")
            If cb IsNot Nothing AndAlso cb.Checked = False Then

            ElseIf cb.Checked = True Then
                With GridView1.Rows(M_count)
                    Dim purInvType As String = .Cells(1).Text.Trim,
                        purInvNo As String = .Cells(2).Text
                    Dim Invoice As String = .Cells(5).Text
                    Dim BBB As String = "select InvoiceNo from BillPurLine where InvoiceH='" & purInvType & "' and InvoiceNo='" & purInvNo & "'"
                    Dim CCC As String = Conn_sql.Get_value(BBB, Conn_sql.MIS_ConnectionString)
                    If CCC = "" Then 'Save(OK)
                        Dim TB015 As Double = outcont.checkNumberic(.Cells(7).Text.Trim),
                            TAX As Double = outcont.checkNumberic(.Cells(8).Text.Trim),
                            Amount As Double = outcont.checkNumberic(.Cells(9).Text.Trim)

                        'Dim SQL As String = "select SUM(TB015) from ACPTB where TB001 ='" & purInvType & "' and TB002 ='" & purInvNo & "'"
                        'Dim SQL As String = "select SUM(TB015) A,SUM(TB016) B from ACPTB where TB001 ='" & purInvType & "' and TB002 ='" & purInvNo & "'"
                        'Dim rs As New DataTable
                        'rs = Conn_sql.Get_DataReader(SQL, Conn_sql.ERP_ConnectionString)
                        'If rs.Rows.Count > 0 Then
                        '    TB015 = rs.Rows(0).Item("A")
                        '    TAX = rs.Rows(0).Item("B")
                        '    Amount = TB015 + TAX
                        'End If

                        'Dim StrAmount As String = "select SUM(TB015) from ACPTB where TB001 ='" & GridView1.Rows(M_count).Cells(1).Text & "' and TB002 ='" & GridView1.Rows(M_count).Cells(2).Text & "'"
                        'Dim TB015 As Double = Conn_sql.Get_value(StrAmount, Conn_sql.ERP_ConnectionString)
                        'Dim StrTax As String = "select SUM(TB016) from ACPTB where TB001 ='" & GridView1.Rows(M_count).Cells(1).Text & "' and TB002 ='" & GridView1.Rows(M_count).Cells(2).Text & "'"
                        'Dim TAX As Double = Conn_sql.Get_value(StrTax, Conn_sql.ERP_ConnectionString)
                        Dim Type As String = .Cells(1).Text.Replace(" ", "")
                        Dim No As String = .Cells(2).Text.Replace(" ", "")
                        Dim TypeNo As String = Type & No

                        Dim SuppID As String = .Cells(3).Text
                        Dim TA015 As String = .Cells(4).Text
                        Dim TA020 As String = .Cells(6).Text
                        Dim ake As String = TA015.Substring(0, 4)
                        Dim ake1 As String = TA015.Substring(4, 2)
                        Dim ake2 As String = TA015.Substring(6, 2)
                        Dim InvoiceDate As String = ake & "-" & ake1 & "-" & ake2

                        Dim ake3 As String = TA020.Substring(0, 4)
                        Dim ake4 As String = TA020.Substring(4, 2)
                        Dim ake5 As String = TA020.Substring(6, 2)
                        Dim DueDate As String = ake3 & "-" & ake4 & "-" & ake5

                        Dim StrType As String = "select TA079 from ACPTA where TA001 ='" & Type & "' and TA002 ='" & No & "'"
                        Dim OrderType As String = Conn_sql.Get_value(StrType, Conn_sql.ERP_ConnectionString)
                        'Dim preVal As Decimal = 1

                        If OrderType = "2" Then
                            Amount = Amount * -1
                            TAX = TAX * -1
                        End If

                        StrSqlLine = "insert into BillPurLine(InvoiceH,InvoiceNo,OrderDate,DueDate,Amount,BillNo,Balance,RemarkInvoice,Tax,ShowInvoice,TypeNo,SupID,OrderType) values('" & GridView1.Rows(M_count).Cells(1).Text & "','" & GridView1.Rows(M_count).Cells(2).Text & "','" & InvoiceDate & "','" & DueDate & "','" & Amount & "','" & txtbillingno.Text & "','" & Amount & "','" & GridView1.Rows(M_count).Cells(5).Text & "','" & TAX & "','" & GridView1.Rows(M_count).Cells(5).Text.Replace("#", "") & "','" & TypeNo & "','" & SuppID & "','" & OrderType.Replace(" ", "") & "')"
                        Conn_sql.Exec_Sql(StrSqlLine, Conn_sql.MIS_ConnectionString)
                    ElseIf CCC <> "" Then   'Not Save
                    End If

                End With
            End If
        Next

        'Sum Balance Order Type = Blue
        'Dim StrSum As String = "select SUM(Balance) from BillPurLine where BillNo ='" & txtbillingno.Text & "'and OrderType = '1'"
        'Dim SumBlue As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)
        ''Sum Balance Order Type = Red
        'Dim GetTypeRead As String = "select SUM(Balance) from BillPurLine where BillNo ='" & txtbillingno.Text & "'and OrderType = '2'"
        'Dim SumRed As Double = Conn_sql.Get_value(GetTypeRead, Conn_sql.MIS_ConnectionString)

        'Dim SumBal As Double = SumBlue - SumRed
        Dim StrSum As String = "select SUM(Balance) from BillPurLine where BillNo ='" & txtbillingno.Text & "' "
        Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        'Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update BillPurLine set AmountBalance ='" & SumBal & "' where BillNo ='" & txtbillingno.Text & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        SpellNumber(SumBal)
        'Save(AmountText)
        Dim AmountText As String = "Update BillPurLine set AmountText =N'" & txtbath.Text & "' where BillNo ='" & txtbillingno.Text & "'"
        Conn_sql.Exec_Sql(AmountText, Conn_sql.MIS_ConnectionString)

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('BillingReceipts1.aspx?BillNo=" + txtbillingno.Text + "');", True)
        ClearData()

        GridView2.DataBind()
        GridView3.DataBind()
        GridView4.DataBind()
        GridView5.DataBind()
    End Sub

    Private Sub GetValue()

        Dim SupID As String
        Dim SupName As String
        Dim Address1 As String
        Dim Address2 As String
        Dim Payment As String

        SupID = Conn_sql.Get_value("Select MA001 from PURMA where MA001='" & DDLSup.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        SupName = Conn_sql.Get_value("Select MA003 from PURMA where MA001='" & DDLSup.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        Address1 = Conn_sql.Get_value("Select MA014 from PURMA where MA001='" & DDLSup.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        Address2 = Conn_sql.Get_value("Select MA015 from PURMA where MA001='" & DDLSup.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        Payment = Conn_sql.Get_value("Select MA025 from PURMA where MA001='" & DDLSup.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        txtsupid.Text = SupID
        txtname.Text = SupName
        txtaddress1.Text = Address1.Replace(" ", "")
        txtpayment.Text = Payment
        txtaddress2.Text = Address2

    End Sub

    Public Sub ThaiBaht(ByVal txt As String)

        Dim bahtTxt As String, n As String, bahtTH As String = ""
        Dim amount As Double
        Try
            amount = Convert.ToDecimal(txt)
        Catch
            amount = 0
        End Try
        bahtTxt = amount.ToString("####.00")
        Dim num As String() = {"ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", _
         "หก", "เจ็ด", "แปด", "เก้า", "สิบ"}
        Dim rank As String() = {"", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", _
         "ล้าน"}
        Dim temp As String() = bahtTxt.Split("."c)
        Dim intVal As String = temp(0)
        Dim decVal As String = temp(1)
        If Convert.ToDouble(bahtTxt) = 0 Then
            bahtTH = "ศูนย์บาทถ้วน"
            txtbath.Text = bahtTH

        Else
            For i As Integer = 0 To intVal.Length - 1
                n = intVal.Substring(i, 1)
                If n <> "0" Then
                    If (i = (intVal.Length - 1)) AndAlso (n = "1") Then
                        bahtTH += "เอ็ด"

                    ElseIf (i = (intVal.Length - 2)) AndAlso (n = "2") Then
                        bahtTH += "ยี่"
                    ElseIf (i = (intVal.Length - 2)) AndAlso (n = "1") Then
                        bahtTH += ""
                    Else
                        bahtTH += num(Convert.ToInt32(n))
                    End If
                    bahtTH += rank((intVal.Length - i) - 1)
                    txtbath.Text = bahtTH

                End If
            Next
            bahtTH += "บาท"
            If decVal = "00" Then
                bahtTH += "ถ้วน"
                txtbath.Text = bahtTH

            Else
                For i As Integer = 0 To decVal.Length - 1
                    n = decVal.Substring(i, 1)
                    If n <> "0" Then
                        If (i = decVal.Length - 1) AndAlso (n = "1") Then
                            bahtTH += "เอ็ด"
                        ElseIf (i = (decVal.Length - 2)) AndAlso (n = "2") Then
                            bahtTH += "ยี่"
                        ElseIf (i = (decVal.Length - 2)) AndAlso (n = "1") Then
                            bahtTH += ""
                        Else
                            bahtTH += num(Convert.ToInt32(n))
                        End If
                        bahtTH += rank((decVal.Length - i) - 1)


                    End If
                Next
                bahtTH += "สตางค์"
                txtbath.Text = bahtTH

            End If
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
        txtbathedit.Text = Dollars & Cents & " ONLY"
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

    Protected Sub Buselect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buselect.Click
        GridView1.Visible = True
        For Each gvr As GridViewRow In GridView1.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("CheckBox2"), CheckBox)
            cb.Checked = True
        Next
    End Sub

    Protected Sub Buclear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buclear.Click
        GridView1.Visible = True
        For Each gvr As GridViewRow In GridView1.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("CheckBox2"), CheckBox)
            cb.Checked = False
        Next
    End Sub

    Protected Sub BuEsearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuEsearch.Click

        Dim aaa As String = "/" & UcDdlMonthEdit.Text
        Dim bbb As String = "/" & UcDdlYearEdit.Text

        SqlDataSource5.SelectCommand = "SELECT [BillShow], [SupID], [SupName] FROM [BillPurHead] where SupID='" & DropDownList5.SelectedValue & "' and Date like '%" & aaa & bbb & "'"
        Me.GridView4.DataBind()

    End Sub

    Protected Sub BuDeleteAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuDeleteAll.Click

        Dim BillNo As String = Conn_sql.Get_value("select BillNo from BillPurLine where ID='" & TextBox3.Text & "'", Conn_sql.MIS_ConnectionString)
        Dim ID As Integer = BillNo

        'Delete BillNo
        Dim StrDel As String = "Delete from BillPurLine where BillNo ='" & TextBox1.Text & "'"
        Conn_sql.Exec_Sql(StrDel, Conn_sql.MIS_ConnectionString)

        'Sum Balance
        Dim ake As String = "and"
        'Dim StrSum As String = "select SUM(Balance) from BillPurLine where BillNo ='" & BillNo & "'" & ake & " InvoiceH <> 'CR1' " & ake & " InvoiceH <> 'CR2'"
        'Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)
        'ThaiBaht(SumBal)

        Dim StrSum As String = "select SUM(Balance) from BillPurLine where BillNo ='" & BillNo & "' "
        Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        SpellNumber(SumBal)
        'Save AmountBalance
        Dim UpdateSumAmount As String = "Update BillPurLine set AmountBalance ='" & SumBal & "' where BillNo ='" & BillNo & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        'Save AmountText
        Dim AmountText As String = "Update BillPurLine set AmountText =N'" & txtbath.Text & "' where BillNo ='" & BillNo & "'"
        Conn_sql.Exec_Sql(AmountText, Conn_sql.MIS_ConnectionString)

        TextBox3.Text = ""
        txttype.Text = ""
        txtinvoiceno.Text = ""
    End Sub

    Protected Sub BuDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuDelete.Click

        Dim BillNo As Integer = Conn_sql.Get_value("select BillNo from BillPurLine where ID='" & TextBox3.Text & "'", Conn_sql.MIS_ConnectionString)
        'Dim ID As Integer = BillNo

        'Delete Row
        Dim StrDel As String = "Delete from BillPurLine where ID ='" & TextBox3.Text & "'"
        Conn_sql.Exec_Sql(StrDel, Conn_sql.MIS_ConnectionString)

        'Sum Balance
        'Dim ake As String = "and"
        'Dim StrSum As String = "select SUM(Balance) from BillPurLine where BillNo ='" & BillNo & "'" & ake & " InvoiceH <> 'CR1' " & ake & " InvoiceH <> 'CR2'"
        'Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)
        'ThaiBaht(SumBal)

        Dim StrSum As String = "select SUM(Balance) from BillPurLine where BillNo ='" & BillNo & "' "
        Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        'Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update BillPurLine set AmountBalance ='" & SumBal & "' where BillNo ='" & BillNo & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        SpellNumber(SumBal)
        'Save AmountText
        Dim AmountText As String = "Update BillPurLine set AmountText =N'" & txtbath.Text & "' where BillNo ='" & BillNo & "'"
        Conn_sql.Exec_Sql(AmountText, Conn_sql.MIS_ConnectionString)

        TextBox3.Text = ""
        txttype.Text = ""
        txtinvoiceno.Text = ""
        txtinvoice.Text = ""

    End Sub

    Protected Sub Bureport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bureport.Click

        Dim aaa As String = "/" & DropDownList2.SelectedValue
        Dim bbb As String = "/" & DropDownList3.SelectedValue

        SqlDataSource4.SelectCommand = "SELECT [BillShow], [SupID], [SupName] FROM [BillPurHead] where SupID='" & DropDownList1.SelectedValue & "' and Date like '%" & aaa & bbb & "'"
        Me.GridView3.DataBind()

    End Sub

    Private Sub DDLBill1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLBill1.SelectedIndexChanged

        Dim GetSupID As String = "Select SupID from BillPurHead where BillShow='" & DDLBill1.SelectedValue & "'"
        TextBox11.Text = Conn_sql.Get_value(GetSupID, Conn_sql.MIS_ConnectionString)

        Dim GetSupName As String = "Select SupName from BillPurHead where BillShow='" & DDLBill1.SelectedValue & "'"
        TextBox12.Text = Conn_sql.Get_value(GetSupName, Conn_sql.MIS_ConnectionString)

    End Sub

    Protected Sub BuDSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuDSearch.Click

        Dim BillNo As String = Conn_sql.Get_value("select BillNo from BillPurHead where BillShow='" & DDLBill1.SelectedValue & "'", Conn_sql.MIS_ConnectionString)
        TextBox1.Text = BillNo

        GridView2.Visible = True
        SqlDataSource2.SelectCommand = "select * from BillPurLine where BillNo='" & TextBox1.Text & "'"
        Me.GridView2.DataBind()

    End Sub

    Private Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand

        If e.CommandName = "OnDelete" Then
            Dim i As Integer = e.CommandArgument
            TextBox3.Text = GridView2.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
            txttype.Text = GridView2.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            txtinvoiceno.Text = GridView2.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            txtinvoice.Text = GridView2.Rows(i).Cells(3).Text.Replace("&nbsp;", "")

            GridView2.Visible = True
        End If

    End Sub

    Private Sub GridView4_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView4.RowCommand

        If e.CommandName = "OnClick" Then

            Dim i As Integer = e.CommandArgument

            TextBox4.Text = GridView4.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
            TextBox7.Text = GridView4.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            TextBox5.Text = GridView4.Rows(i).Cells(2).Text.Replace("&nbsp;", "")

            Dim aaa As String = Conn_sql.Get_value("select Date from BillPurHead where BillShow='" & TextBox4.Text & "'", Conn_sql.MIS_ConnectionString)
            TextBox10.Text = aaa
        End If

    End Sub

    Protected Sub BuESearch2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuESearch2.Click
        GridView5.Visible = True

        Dim BillShow As String = Conn_sql.Get_value("select BillNo from BillPurHead where BillShow ='" & TextBox4.Text & "'", Conn_sql.MIS_ConnectionString)
        Dim Qmonth As String = Conn_sql.Get_value("select SUBSTRING(InvoiceNo,0,7) as Month from BillPurLine where BillNo ='" & BillShow & "'", Conn_sql.MIS_ConnectionString)

        If TextBox4.Text <> "" And TextBox7.Text <> "" And TextBox10.Text <> "" Then
            SqlDataSource7.SelectCommand = "SELECT DISTINCT TA001,TA002,TA004,TA015,TA021,TA020,TA037 AMT,TA038 TAX,TA037+TA038 NET FROM [HOOTHAI].[dbo].[ACPTA] WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillPurLine] where ACPTA.TA001 = BillPurLine.InvoiceH and ACPTA.TA002 = BillPurLine.InvoiceNo) and TA015 like '" & Qmonth & "%' and TA004 ='" & TextBox7.Text & "'"
        End If
        Me.GridView5.DataBind()

    End Sub

    Protected Sub BuEall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuEall.Click
        GridView5.Visible = True
        For Each gvr As GridViewRow In GridView5.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = True
        Next
    End Sub

    Protected Sub BuEcAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuEcAll.Click
        GridView5.Visible = True
        For Each gvr As GridViewRow In GridView5.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = False
        Next
    End Sub

    Protected Sub BuESave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuESave.Click

        If TextBox4.Text = "" Then
            show_message.ShowMessage(Page, "Please Select Billing No.", UpdatePanel1)
            TextBox4.Focus()
            Exit Sub
        End If

        Dim GetBill As String = "select BillNo from BillPurHead where BillShow='" & TextBox4.Text & "'"
        Dim BillNo As String = Conn_sql.Get_value(GetBill, Conn_sql.MIS_ConnectionString)

        Dim strSql As String
        strSql = "Update BillPurHead set EditBy ='" & Session("Username") & "' where BillNo ='" & BillNo & "'"
        Conn_sql.Exec_Sql(strSql, Conn_sql.MIS_ConnectionString)

        Dim StrSqlLine As String
        For M_count As Integer = 0 To GridView5.Rows.Count - 1

            Dim cb As CheckBox = GridView5.Rows(M_count).FindControl("Ck")
            If cb IsNot Nothing AndAlso cb.Checked = False Then
            ElseIf cb.Checked = True Then
                Dim purInvType As String = GridView5.Rows(M_count).Cells(1).Text.Trim
                Dim purInvNo As String = GridView5.Rows(M_count).Cells(2).Text.Trim
                
                Dim Invoice As String = GridView5.Rows(M_count).Cells(5).Text
                'Dim BBB As String = "select InvoiceNo from BillPurLine where RemarkInvoice='" & Invoice & "'and SupID='" & DDLSup.SelectedValue & "' and "
                Dim BBB As String = "select InvoiceNo from BillPurLine where InvoiceH='" & purInvType & "'and InvoiceNo='" & purInvNo & "' "
                Dim CCC As String = Conn_sql.Get_value(BBB, Conn_sql.MIS_ConnectionString)

                If CCC = "" Then         'Save(OK)
                    With GridView5.Rows(M_count)
                        'Dim StrAmount As String = "select SUM(TB015) from ACPTB where TB001 ='" & GridView5.Rows(M_count).Cells(1).Text & "' and TB002 ='" & GridView5.Rows(M_count).Cells(2).Text & "'"
                        'Dim StrTax As String = "select SUM(TB016) from ACPTB where TB001 ='" & GridView5.Rows(M_count).Cells(1).Text & "' and TB002 ='" & GridView5.Rows(M_count).Cells(2).Text & "'"
                        Dim TB015 As Double = outcont.checkNumberic(.Cells(7).Text.Trim),
                        TAX As Double = outcont.checkNumberic(.Cells(8).Text.Trim),
                        Amount As Double = outcont.checkNumberic(.Cells(9).Text.Trim)
                        'Dim SQL As String = "select SUM(TB015) A,SUM(TB016) B from ACPTB where TB001 ='" & purInvType & "' and TB002 ='" & purInvNo & "'"
                        'Dim rs As New DataTable
                        'rs = Conn_sql.Get_DataReader(SQL, Conn_sql.ERP_ConnectionString)
                        'If rs.Rows.Count > 0 Then
                        '    TB015 = rs.Rows(0).Item("A")
                        '    TAX = rs.Rows(0).Item("B")
                        '    Amount = TB015 + TAX
                        'End If

                        Dim Type As String = .Cells(1).Text.Replace(" ", "")
                        Dim No As String = .Cells(2).Text.Replace(" ", "")
                        Dim TypeNo As String = Type & No
                        Dim SuppID As String = .Cells(3).Text.Replace(" ", "")
                        Dim TA015 As String = .Cells(4).Text
                        Dim TA020 As String = .Cells(6).Text
                        Dim ake As String = TA015.Substring(0, 4)
                        Dim ake1 As String = TA015.Substring(4, 2)
                        Dim ake2 As String = TA015.Substring(6, 2)
                        Dim InvoiceDate As String = ake & "-" & ake1 & "-" & ake2

                        Dim ake3 As String = TA020.Substring(0, 4)
                        Dim ake4 As String = TA020.Substring(4, 2)
                        Dim ake5 As String = TA020.Substring(6, 2)
                        Dim DueDate As String = ake3 & "-" & ake4 & "-" & ake5
                        Dim StrType As String = "select TA079 from ACPTA where TA001 ='" & Type & "' and TA002 ='" & No & "'"
                        Dim OrderType As String = Conn_sql.Get_value(StrType, Conn_sql.ERP_ConnectionString)

                        If OrderType = "2" Then
                            Amount = Amount * -1
                            TAX = TAX * -1
                        End If

                        StrSqlLine = "insert into BillPurLine(InvoiceH,InvoiceNo,OrderDate,DueDate,Amount,BillNo,Balance,RemarkInvoice,Tax,ShowInvoice,TypeNo,SupID,OrderType) values('" & GridView5.Rows(M_count).Cells(1).Text & "','" & GridView5.Rows(M_count).Cells(2).Text & "','" & InvoiceDate & "','" & DueDate & "','" & Amount & "','" & BillNo & "','" & Amount & "','" & GridView5.Rows(M_count).Cells(5).Text & "','" & TAX & "','" & GridView5.Rows(M_count).Cells(5).Text.Replace("#", "") & "','" & TypeNo & "','" & SuppID & "','" & OrderType & "')"
                        Conn_sql.Exec_Sql(StrSqlLine, Conn_sql.MIS_ConnectionString)
                    End With
                ElseIf CCC <> "" Then   'Not Save
                End If

            End If
        Next

        ''Sum Balance Order Type = Blue
        'Dim StrSum As String = "select SUM(Balance) from BillPurLine where BillNo ='" & BillNo & "'and OrderType = '1'"
        'Dim SumBlue As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)
        ''Sum Balance Order Type = Red
        'Dim GetTypeRead As String = "select SUM(Balance) from BillPurLine where BillNo ='" & BillNo & "'and OrderType = '2'"
        'Dim SumRed As Double = Conn_sql.Get_value(GetTypeRead, Conn_sql.MIS_ConnectionString)
        'Dim SumBal As Double = SumBlue - SumRed

        Dim StrSum As String = "select SUM(Balance) from BillPurLine where BillNo ='" & BillNo & "' "
        Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        ''Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update BillPurLine set AmountBalance ='" & SumBal & "' where BillNo ='" & BillNo & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        SpellNumber(SumBal)
        'Save(AmountText)
        Dim AmountText As String = "Update BillPurLine set AmountText =N'" & txtbathedit.Text & "' where BillNo ='" & txtbillingno.Text & "'"
        Conn_sql.Exec_Sql(AmountText, Conn_sql.MIS_ConnectionString)

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('BillingReceipts1.aspx?BillNo=" + BillNo + "');", True)
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox7.Text = ""
        TextBox10.Text = ""

    End Sub

    Private Sub GridView3_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView3.RowCommand

        If e.CommandName = "OnClick" Then

            Dim i As Integer = e.CommandArgument
            Dim BillNo As String
            BillNo = Conn_sql.Get_value("select BillNo from BillPurHead where BillShow='" & GridView3.Rows(i).Cells(0).Text & "'", Conn_sql.MIS_ConnectionString)
            Dim ID As Integer = BillNo
            TextBox2.Text = ID
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('BillingReceipts1.aspx?BillNo=" + TextBox2.Text + "');", True)

        End If

    End Sub

    Protected Sub BuReportMonth_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuReportMonth.Click

        Dim DelLabeleBM As String = "delete from BillPurMonth"
        Conn_sql.Exec_Sql(DelLabeleBM, Conn_sql.MIS_ConnectionString)

        Dim MMyyyy As String = "/" & DropDownList2.SelectedValue & "/" & DropDownList3.SelectedValue

        Dim SelSQL As String = ""
        SelSQL = SelSQL & "select distinct H.BillShow,H.SupID,H.Payment,H.Date,L.DueDate,AmountBalance,H.CreateBy"
        SelSQL = SelSQL & " FROM [HOOTHAI_REPORT].[dbo].[BillPurHead] H left join [HOOTHAI_REPORT].[dbo].[BillPurLine] L on (H.BillNo = L.BillNo)"
        SelSQL = SelSQL & "where H.Date like '%" & MMyyyy & "' and AmountBalance is not  null"

        Dim aa As New Data.DataTable
        aa = Conn_sql.Get_DataReader(SelSQL, Conn_sql.ERP_ConnectionString)

        If aa.Rows.Count > 0 Then
            For i As Integer = 0 To aa.Rows.Count - 1
                Dim BillShow As String = aa.Rows(i).Item("BillShow").ToString.Replace(" ", "")
                Dim SupID As String = aa.Rows(i).Item("SupID").ToString.Replace(" ", "")
                Dim Payment As String = aa.Rows(i).Item("Payment").ToString.Replace(" ", "")
                Dim BDate As String = aa.Rows(i).Item("Date").ToString.Replace(" ", "")
                Dim AmountBalance As String = aa.Rows(i).Item("AmountBalance").ToString.Replace(" ", "")
                Dim CreateBy As String = aa.Rows(i).Item("CreateBy").ToString.Replace(" ", "")
                Dim DueDate As String = aa.Rows(i).Item("DueDate").ToString.Replace(" ", "")

                Dim InSQL As String = "Insert into BillPurMonth(BillShow,SupID,Payment,Date,AmountBalance,DueDate,CreateBy)"
                InSQL = InSQL & " Values('" & BillShow & "','" & SupID & "','" & Payment & "',"
                InSQL = InSQL & "'" & BDate & "','" & AmountBalance & "','" & DueDate & "',"
                InSQL = InSQL & "'" & CreateBy & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
            Next
        End If

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('BillingReceipts2.aspx?Month=" + MMyyyy + "');", True) '"&Year=" + DropDownList3.SelectedValue +

    End Sub

    Private Sub DDLSup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLSup.SelectedIndexChanged

        GetValue()
    End Sub

    Protected Sub BuHisSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuHisSearch.Click


        If txthissearch.Text <> "" Then
            If DDLHisSearch.SelectedIndex = 0 Then  'Invoice No.
                DataSourceHistory.SelectCommand = "select DISTINCT H.BillShow,H.SupID,H.SupName,H.Date FROM [HOOTHAI_REPORT].[dbo].[BillPurHead] H left join [HOOTHAI_REPORT].[dbo].[BillPurLine] L on (H.BillNo = L.BillNo) where L.ShowInvoice like '%" & txthissearch.Text & "%'"
                Me.GridHistory.DataBind()
            ElseIf DDLHisSearch.SelectedIndex = 1 Then   'Supp ID
                DataSourceHistory.SelectCommand = "select DISTINCT H.BillShow,H.SupID,H.SupName,H.Date FROM [HOOTHAI_REPORT].[dbo].[BillPurHead] H left join [HOOTHAI_REPORT].[dbo].[BillPurLine] L on (H.BillNo = L.BillNo) where H.SupID = '" & txthissearch.Text & "'"
                Me.GridHistory.DataBind()
            ElseIf DDLHisSearch.SelectedIndex = 2 Then   'Bill No
                DataSourceHistory.SelectCommand = "select DISTINCT H.BillShow,H.SupID,H.SupName,H.Date FROM [HOOTHAI_REPORT].[dbo].[BillPurHead] H left join [HOOTHAI_REPORT].[dbo].[BillPurLine] L on (H.BillNo = L.BillNo) where H.BillShow = '" & txthissearch.Text & "'"
                Me.GridHistory.DataBind()
            End If
        ElseIf txthissearch.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Data", UpdatePanel1)
            Exit Sub

        End If

    End Sub

    Private Sub GridHistory_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridHistory.RowCommand

        If e.CommandName = "OnClick" Then

            Dim i As Integer = e.CommandArgument
            Dim BillNo As String
            BillNo = Conn_sql.Get_value("select BillNo from BillPurHead where BillShow='" & GridHistory.Rows(i).Cells(0).Text & "'", Conn_sql.MIS_ConnectionString)
            Dim ID As Integer = BillNo
            TextBox2.Text = ID
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('BillingReceipts1.aspx?BillNo=" + TextBox2.Text + "');", True)

        End If
    End Sub
End Class