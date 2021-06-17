Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class BillInvoice
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If

            CreateTable.CreateBillhead()
            CreateTable.CreateBillLine()

            Dim strSql As String = "SELECT MA001,rtrim(MA001)+'-'+MA002 AS MA002 FROM COPMA order by MA001"
            ControlForm.showDDL(DDLCustID, strSql, "MA002", "MA001", False, Conn_sql.ERP_ConnectionString)
            ControlForm.showDDL(DDLCustomer, strSql, "MA002", "MA001", False, Conn_sql.ERP_ConnectionString)
            ControlForm.showDDL(DropDownList1, strSql, "MA002", "MA001", False, Conn_sql.ERP_ConnectionString)

            strSql = "select MK002,rtrim(MV001)+'-'+MV002 as MV002 from CMSMK left join CMSMV on MV001=MK002 where MK001='Sales' order by MK002"
            ControlForm.showDDL(DDLBillby, strSql, "MV002", "MK002", False, Conn_sql.ERP_ConnectionString)
            ControlForm.showDDL(DDLBilledit, strSql, "MV002", "MK002", False, Conn_sql.ERP_ConnectionString)
            ControlForm.showDDL(DropDownList2, strSql, "MV002", "MK002", False, Conn_sql.ERP_ConnectionString)

        End If
       
        GridView3.Visible = False
        GridEdit.Visible = False
        Griddelete.Visible = False

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("dd/MM/yyyy")
        txtdate.Text = resultdate
        lbldate.Text = resultdate
        ddd.Text = newdate.ToString("yyyyMMdd")

    End Sub

    Protected Sub Buview_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buview.Click
        GridView3.Visible = True
        'Dim aa As String = CStr(txtfrom.Text)
        'Dim bb As String = CStr(txtto.Text)
        'Dim ake As String = "and"

        Dim SQL As String = ""
        Dim WHR As String = ""

        WHR = WHR & Conn_sql.Where("TA004", DDLCustID)
        WHR = WHR & Conn_sql.Where("TB048", txtpo)
        WHR = WHR & Conn_sql.Where("TA038", CStr(txtfrom.Text), CStr(txtto.Text))
        SQL = " select distinct TA004, TA008, TA038, TA080, TA001,TA002, TA020, TA042, TA041,TA098 " & _
              " from [HOOTHAI].[dbo].[ACRTA] A JOIN [HOOTHAI].[dbo].[ACRTB] B ON (A.TA001 = B.TB001) and (A.TA002 = B.TB002) " & _
              " WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillLine] where A.TA001 = BillLine.InvoiceH and A.TA002 = BillLine.InvoiceNo) " & WHR & _
              " ORDER BY TA038,TA001,TA002 "
        ControlForm.ShowGridView(GridView3, SQL, Conn_sql.ERP_ConnectionString)

        'If txtfrom.Text <> "" And txtto.Text <> "" Then
        '    SqlDataSource3.SelectCommand = "select distinct TA004, TA008, TA038, TA080, TA001,TA002, TA020, TA042, TA041,TA098 from [JINPAO80].[dbo].[ACRTA] A JOIN [JINPAO80].[dbo].[ACRTB] B ON (A.TA001 = B.TB001) and (A.TA002 = B.TB002) WHERE not EXISTS(select * from [DBMIS].[dbo].[BillLine] where A.TA001 = BillLine.InvoiceH and A.TA002 = BillLine.InvoiceNo) and TA038 between '" & aa & "'" & ake & "'" & bb & "'" & ake & " TA004 ='" & DDLCustID.SelectedValue & "' ORDER BY TA002"
        'ElseIf txtfrom.Text = "" And txtto.Text = "" And txtdate.Text <> "" Then
        '    SqlDataSource3.SelectCommand = "select distinct TA004, TA008, TA038, TA080, TA001,TA002, TA020, TA042, TA041,TA098 from [JINPAO80].[dbo].[ACRTA] A JOIN [JINPAO80].[dbo].[ACRTB] B ON (A.TA001 = B.TB001) and (A.TA002 = B.TB002) WHERE not EXISTS(select * from [DBMIS].[dbo].[BillLine] where A.TA001 = BillLine.InvoiceH and A.TA002 = BillLine.InvoiceNo) and TA004 ='" & DDLCustID.SelectedValue & "' and TB048 like '" & txtpo.Text & "%' ORDER BY TA002"
        'End If

        'Me.GridView3.DataBind()
        AutoGenerateID_Part()
        GetValue()
    End Sub

    Private Sub ClearData()

        txtbilling.Text = ""
        txtCustID.Text = ""
        txtcustname.Text = ""
        txtaddress1.Text = ""
        txtaddress2.Text = ""
        txtdate.Text = ""
        txtpayment.Text = ""
        txtBillNo.Text = ""
        txtfrom.Text = ""
        txtto.Text = ""

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

        sqlTmp = "SELECT TOP 1 BillNo FROM Billhead ORDER BY BillNo DESC"

        With conn
            If .State = ConnectionState.Open Then .Close()
            .ConnectionString = str
            .Open()
        End With

        Dim BI As String = "BI"

        Try
            With comTmp
                .CommandType = CommandType.Text
                .CommandText = sqlTmp
                .Connection = conn
                drTmp = .ExecuteReader()
                drTmp.Read()
                tmpidpart = CInt(drTmp.Item("BillNo"))
                tmpidpart = tmpidpart + 1

                txtBillNo.Text = tmpidpart.ToString("0000000")
                txtbilling.Text = BI & tmpidpart.ToString("0000000")
            End With
        Catch
            txtBillNo.Text = "0000001"
            txtbilling.Text = BI & "0000001"
        End Try
        drTmp.Close()
    End Sub

    Protected Sub Busave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busave.Click

        If txtbilling.Text = "" Then
            show_message.ShowMessage(Page, "Please Search Data", UpdatePanel1)
            Exit Sub
        End If

        Dim strSql As String

        strSql = "insert into Billhead (BillNo,CustID,Address1,Address2,Date,Payment,BillShow,CreateBy,CustName,BillBy,BeDate) values('" & txtBillNo.Text & "','" & txtCustID.Text & "','" & txtaddress1.Text & "','" & txtaddress2.Text & "','" & txtdate.Text & "','" & txtpayment.Text & "','" & txtbilling.Text & "','" & Session("Username") & "','" & txtcustname.Text & "','" & DDLBillby.SelectedValue & "','" & ddd.Text & "')"

        Conn_sql.Exec_Sql(strSql, Conn_sql.MIS_ConnectionString)

        Dim StrSqlLine As String
        Dim CR As Double
        For M_count As Integer = 0 To GridView3.Rows.Count - 1

            Dim cb As CheckBox = GridView3.Rows(M_count).FindControl("Ck")
            If cb IsNot Nothing AndAlso cb.Checked = False Then
            ElseIf cb.Checked = True Then
                'gridview 5 - 3
                Dim TA042 As Decimal = GridView3.Rows(M_count).Cells(5).Text
                Dim TA041 As Decimal = GridView3.Rows(M_count).Cells(6).Text
                Dim Amount As Decimal
                Amount = TA041 + TA042

                Dim Balance As Decimal
                Dim Paid As Decimal = GridView3.Rows(M_count).Cells(7).Text
                Balance = Amount - Paid

                'Dim StrDue As String = "select (SUBSTRING(TA020,1,4))+'-'+(SUBSTRING(TA020,5,2))+'-'+(SUBSTRING(TA020,7,2)) as DeliveryDate from ACRTA"
                'Dim DueDate As String = Conn_sql.Get_value(StrDue, Conn_sql.ERP_ConnectionString)
                'Dim StrOrder As String = "select (SUBSTRING(TA038,1,4))+'-'+(SUBSTRING(TA038,5,2))+'-'+(SUBSTRING(TA038,7,2)) as DeliveryDate from ACRTA"
                'Dim OrderDate As String = Conn_sql.Get_value(StrOrder, Conn_sql.ERP_ConnectionString)

                Dim OrderDate As String = GridView3.Rows(M_count).Cells(1).Text
                Dim DueDate As String = GridView3.Rows(M_count).Cells(4).Text

                Dim Type As String = GridView3.Rows(M_count).Cells(2).Text.Replace(" ", "")
                Dim ValuesType As String
                If Type = "CR1" Or Type = "CR2" Then
                    CR = Balance
                    ValuesType = "-"
                Else
                    ValuesType = ""
                End If
                Dim ShowAmount As String = ValuesType & Amount
                Dim ShowBalance As String = ValuesType & Balance

                Dim InvoiceType As String = GridView3.Rows(M_count).Cells(2).Text.Replace(" ", "")
                Dim Invoice As String = GridView3.Rows(M_count).Cells(3).Text.Replace(" ", "")
                Dim BBB As String = "select InvoiceNo from BillLine where InvoiceNo='" & Invoice & "' and InvoiceH='" & InvoiceType & "'"
                Dim CCC As String = Conn_sql.Get_value(BBB, Conn_sql.MIS_ConnectionString)                                 'ShowBalance

                Dim SPaid As String = ""
                If Paid = "0.00" Then
                    SPaid = ""
                ElseIf Paid <> "0.00" Then
                    SPaid = Paid
                End If

                If CCC = "" Then 'Save(OK)
                    StrSqlLine = "insert into BillLine(InvoiceH,InvoiceNo,DueDate,Amount,BillNo,Balance,OrderDate,ShowAmount,ShowBalance,ShowPaid,Paid,CustID) values('" & GridView3.Rows(M_count).Cells(2).Text & "','" & GridView3.Rows(M_count).Cells(3).Text & "','" & DueDate & "','" & Amount & "','" & txtBillNo.Text & "','" & Balance & "','" & OrderDate & "','" & ShowAmount & "','" & ShowBalance & "','" & SPaid & "','" & GridView3.Rows(M_count).Cells(7).Text & "','" & txtCustID.Text & "')"
                ElseIf CCC <> "" Then   'Not Save

                End If
                Conn_sql.Exec_Sql(StrSqlLine, Conn_sql.MIS_ConnectionString)

            End If
        Next
        'Sum Balance

        Dim StrSum As String = "select SUM(Balance) from BillLine where BillNo ='" & txtBillNo.Text & "' and InvoiceH <> 'CR1' and InvoiceH <> 'CR2'"
        Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        Dim StrSumCR As String = "select SUM(Balance) from BillLine where BillNo ='" & txtBillNo.Text & "' and InvoiceH = 'CR1' or InvoiceH = 'CR2'"
        Dim SumCR As Double = Conn_sql.Get_value(StrSumCR, Conn_sql.MIS_ConnectionString)
        Dim Total As Double = SumBal - SumCR

        'Dim SumCRAgin As Double = Conn_sql.Get_value(StrSumCR, Conn_sql.MIS_ConnectionString)
        'Dim SumTotal As Double = Total - SumCRAgin

        SpellNumber(Total)
        'ThaiBaht(Total)
        'Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update BillLine set AmountBalance ='" & Total & "' where BillNo ='" & txtBillNo.Text & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        'Save AmountText
        Dim AmountText As String = "Update BillLine set AmountText =N'" & txtbath.Text & "' where BillNo ='" & txtBillNo.Text & "'"
        Conn_sql.Exec_Sql(AmountText, Conn_sql.MIS_ConnectionString)

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Billing1.aspx?BillNo=" + txtBillNo.Text + "');", True)
        ClearData()

        GridView5.DataBind()
        GridView4.DataBind()
        Griddelete.DataBind()

    End Sub

    Private Sub GetValue()
        'Dim CustID As String
        'Dim CustName As String
        'Dim Address1 As String
        'Dim Address2 As String
        'Dim Payment As String
        Dim SQL As String = "Select MA001,MA003,MA025,MA026,MA031 from COPMA where MA001='" & DDLCustID.SelectedValue & "'"
        Dim dt As DataTable = Conn_sql.Get_DataReader(SQL, Conn_sql.ERP_ConnectionString)

        With dt.Rows(0)
            txtCustID.Text = .Item("MA001")
            txtcustname.Text = .Item("MA003")
            txtaddress1.Text = .Item("MA025")
            txtaddress2.Text = .Item("MA026")
            txtpayment.Text = .Item("MA031")
        End With

        'CustID = Conn_sql.Get_value("Select MA001 from COPMA where MA001='" & DDLCustID.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        'CustName = Conn_sql.Get_value("Select MA003 from COPMA where MA001='" & DDLCustID.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        'Address1 = Conn_sql.Get_value("Select MA025 from COPMA where MA001='" & DDLCustID.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        'Address2 = Conn_sql.Get_value("Select MA026 from COPMA where MA001='" & DDLCustID.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        'Payment = Conn_sql.Get_value("Select MA031 from COPMA where MA001='" & DDLCustID.SelectedValue & "'", Conn_sql.ERP_ConnectionString)

        'txtCustID.Text = CustID
        'txtcustname.Text = CustName
        'txtaddress1.Text = Address1 '.Replace(" ", "")
        'txtpayment.Text = Payment
        'txtaddress2.Text = Address2

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

    Protected Sub BuReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuReport.Click

        If txtmonth.Text = "" Then
            SqlDataSource1.SelectCommand = "select * from Billhead where CustID='" & DDLCustomer.SelectedValue & "'"
        ElseIf txtmonth.Text <> "" Then
            Dim month As String = "/" & txtmonth.Text
            SqlDataSource1.SelectCommand = "select * from Billhead where CustID='" & DDLCustomer.SelectedValue & "' and Date like '%" & month & "'"
        End If

    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        If txtmonth.Text = "" Then
            DataSourceEdit.SelectCommand = "select * from Billhead where CustID='" & DropDownList1.SelectedValue & "'"
        ElseIf txtmonth.Text <> "" Then
            Dim month As String = "/" & txtmonth.Text
            DataSourceEdit.SelectCommand = "select * from Billhead where CustID='" & DropDownList1.SelectedValue & "' and Date like '%" & month & "'"
        End If
        GridView4.DataBind()
    End Sub

    Protected Sub Buselect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buselect.Click

        GridView3.Visible = True
        For Each gvr As GridViewRow In GridView3.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = True
        Next

    End Sub

    Protected Sub BuClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuClear.Click
        GridView3.Visible = True
        For Each gvr As GridViewRow In GridView3.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = False
        Next

    End Sub

    Private Sub GridView4_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView4.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument

            Dim BillNo As String
            BillNo = Conn_sql.Get_value("select BillNo from Billhead where BillShow='" & GridView4.Rows(i).Cells(0).Text & "'", Conn_sql.MIS_ConnectionString)
            Dim ID As Integer = BillNo
            TextBox2.Text = ID

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Billing1.aspx?BillNo=" + TextBox2.Text + "');", True)
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

    Protected Sub BuEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuEdit.Click

        If lblbillno.Text = "" Then
            show_message.ShowMessage(Page, "Please Select Billing No.", UpdatePanel1)
            Exit Sub
        End If

        Dim SqlUpHead As String

        SqlUpHead = "update Billhead set EditBy='" & DDLBilledit.SelectedValue & "'"
        Conn_sql.Exec_Sql(SqlUpHead, Conn_sql.MIS_ConnectionString)

        Dim StrSqlLine As String
        Dim CR As Double
        For M_count As Integer = 0 To GridEdit.Rows.Count - 1

            Dim cb As CheckBox = GridEdit.Rows(M_count).FindControl("Ck")
            If cb IsNot Nothing AndAlso cb.Checked = False Then
            ElseIf cb.Checked = True Then
                '         CustName cell 2
                '         address cell 4
                Dim TA042 As Double = GridEdit.Rows(M_count).Cells(6).Text
                Dim TA041 As Double = GridEdit.Rows(M_count).Cells(7).Text
                Dim Amount As Double
                Amount = TA041 + TA042

                Dim Balance As Double
                Dim Paid As Double = GridEdit.Rows(M_count).Cells(8).Text
                Balance = Amount - Paid

                Dim OrderDate As String = GridEdit.Rows(M_count).Cells(2).Text
                Dim DueDate As String = GridEdit.Rows(M_count).Cells(5).Text

                Dim Type As String = GridEdit.Rows(M_count).Cells(3).Text.Replace(" ", "")
                Dim ValuesType As String
                If Type = "CR1" Or Type = "CR2" Then
                    CR = Balance
                    ValuesType = "-"
                Else
                    ValuesType = ""
                End If
                Dim ShowAmount As Double = ValuesType & Amount
                Dim ShowBalance As Double = ValuesType & Balance

                Dim InvoiceH As String = GridEdit.Rows(M_count).Cells(3).Text.Replace(" ", "")
                Dim Invoice As String = GridEdit.Rows(M_count).Cells(4).Text.Replace(" ", "")
                Dim BBB As String = "select InvoiceNo from BillLine where InvoiceNo='" & Invoice & "'and InvoiceH='" & InvoiceH & "'and CustID ='" & lblcustid.Text & "'"
                Dim CCC As String = Conn_sql.Get_value(BBB, Conn_sql.MIS_ConnectionString)                                 'ShowBalance

                Dim SPaid As String = ""
                If Paid = "0.00" Then
                    SPaid = ""
                ElseIf Paid <> "0.00" Then
                    SPaid = Paid
                End If

                If CCC = "" Then 'Save(OK)
                    StrSqlLine = "insert into BillLine(InvoiceH,InvoiceNo,DueDate,Amount,BillNo,Balance,OrderDate,ShowAmount,ShowBalance,ShowPaid,Paid,CustID) values('" & GridEdit.Rows(M_count).Cells(3).Text & "','" & GridEdit.Rows(M_count).Cells(4).Text & "','" & DueDate & "','" & Amount & "','" & BillNoEdit.Text & "','" & Balance & "','" & OrderDate & "','" & ShowAmount & "','" & ShowBalance & "','" & SPaid & "','" & GridEdit.Rows(M_count).Cells(8).Text & "','" & lblcustid.Text & "')"
                ElseIf CCC <> "" Then   'Not Save
                End If
                Conn_sql.Exec_Sql(StrSqlLine, Conn_sql.MIS_ConnectionString)
            End If
        Next
        ''Sum Balance

        Dim StrSum As String = "select SUM(Balance) from BillLine where BillNo ='" & BillNoEdit.Text & "' and InvoiceH <> 'CR1' and InvoiceH <> 'CR2'"
        Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        Dim StrSumCR As String = "select SUM(Balance) from BillLine where BillNo ='" & BillNoEdit.Text & "' and InvoiceH = 'CR1' or InvoiceH = 'CR2'"
        Dim SumCR As Double = Conn_sql.Get_value(StrSumCR, Conn_sql.MIS_ConnectionString)
        Dim Total As Double = SumBal - SumCR

        SpellNumber(Total)
        'ThaiBaht(Total)
        'Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update BillLine set AmountBalance ='" & Total & "' where BillNo ='" & BillNoEdit.Text & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        'Save AmountText
        Dim AmountText As String = "Update BillLine set AmountText =N'" & txtbathedit.Text & "' where BillNo ='" & BillNoEdit.Text & "'"
        Conn_sql.Exec_Sql(AmountText, Conn_sql.MIS_ConnectionString)

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Billing1.aspx?BillNo=" + BillNoEdit.Text + "');", True)
        ClearDataeEdit()

    End Sub

    Private Sub ClearDataeEdit()
        lblbillno.Text = ""
        lblcustid.Text = ""
        lblcustomer.Text = ""
        txtfromdate.Text = ""
        txttodate.Text = ""
    End Sub

    Protected Sub Bubill_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bubill.Click
        SqlDataSource1.SelectCommand = "select * from Billhead where BillBy='" & DDLBilledit.SelectedValue & "'"
        GridView4.DataBind()
    End Sub

    Protected Sub BuDAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuDAll.Click
        Griddelete.Visible = True
        For Each gvr As GridViewRow In Griddelete.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = True
        Next
    End Sub
    Protected Sub BuCAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuCAll.Click
        Griddelete.Visible = True
        For Each gvr As GridViewRow In Griddelete.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = False
        Next
    End Sub
    Protected Sub BuDelData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuDelData.Click

        For M_count As Integer = 0 To Griddelete.Rows.Count - 1

            Dim cb As CheckBox = Griddelete.Rows(M_count).FindControl("Ck")
            If cb IsNot Nothing AndAlso cb.Checked = False Then
            ElseIf cb.Checked = True Then

                Dim StrDel As String = "Delete from BillLine where ID ='" & Griddelete.Rows(M_count).Cells(1).Text & "'"
                Conn_sql.Exec_Sql(StrDel, Conn_sql.MIS_ConnectionString)

            End If
        Next

        'Sum Balance
        Dim ake As String = "and"
        Dim StrSum As String = "select SUM(Balance) from BillLine where BillNo ='" & txtidEdit.Text & "'" & ake & " InvoiceH <> 'CR1' " & ake & " InvoiceH <> 'CR2'"
        Dim SumBal As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)
        'ThaiBaht(SumBal)
        SpellNumber(SumBal)
        'Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update BillLine set AmountBalance ='" & SumBal & "' where BillNo ='" & txtidEdit.Text & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        'Save AmountText
        Dim AmountText As String = "Update BillLine set AmountText =N'" & txtbath.Text & "' where BillNo ='" & txtidEdit.Text & "'"
        Conn_sql.Exec_Sql(AmountText, Conn_sql.MIS_ConnectionString)

        txtidEdit.Text = ""
        txttype.Text = ""
        txtinvoiceno.Text = ""

    End Sub

    Protected Sub BuDelSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuDelSearch.Click

        If txtdelBill.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Billing No ", UpdatePanel1)
            txtdelBill.Focus()
            Exit Sub
        ElseIf txtdelBill.Text <> "" Then
            Dim BillNo As String
            BillNo = Conn_sql.Get_value("select BillNo from Billhead where BillShow='" & txtdelBill.Text & "'", Conn_sql.MIS_ConnectionString)
            txtidEdit.Text = BillNo

            Griddelete.Visible = True 'SELECT [ID], [InvoiceH], [InvoiceNo], [OrderDate], [DueDate], [Amount], [Paid] FROM [BillLine]
            DataSourceDelete.SelectCommand = "SELECT [ID], [InvoiceH], [InvoiceNo], [OrderDate], [DueDate], [Amount], [Paid] FROM [BillLine] where BillNo='" & txtidEdit.Text & "'"
            Me.Griddelete.DataBind()
            'Dim aa As Integer
            'aa = Conn_sql.Get_value("select BillNo from Billhead where BillShow ='" & txtsearchEdit.Text & "'", Conn_sql.MIS_ConnectionString)
            'TextBox4.Text = aa
        End If

    End Sub

    Protected Sub BuEditSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuEditSearch.Click

        If txtfromdate.Text <> "" And txttodate.Text <> "" Then
            GridEdit.Visible = True
            Dim aa As String = CStr(txtfromdate.Text)
            Dim bb As String = CStr(txttodate.Text)
            Dim ake As String = "and"

            'select distinct TA004, TA008, TA038, TA080, TA001,TA002, TA020, TA042, TA041,TA098 from [JINPAO80].[dbo].[ACRTA] A JOIN [JINPAO80].[dbo].[ACRTB] B ON (A.TA001 = B.TB001) and (A.TA002 = B.TB002) WHERE not EXISTS(select * from [DBMIS].[dbo].[BillLine] where A.TA001 = BillLine.InvoiceH and A.TA002 = BillLine.InvoiceNo) and TA038 between '" & aa & "'" & ake & "'" & bb & "'" & ake & " TA004 ='" & DDLCustID.SelectedValue & "' ORDER BY TA002"

            SqlDataSource4.SelectCommand = "select distinct TA004, TA038, TA001,TA002, TA020, TA042, TA041,TA098 from [HOOTHAI].[dbo].[ACRTA] A JOIN [HOOTHAI].[dbo].[ACRTB] B ON (A.TA001 = B.TB001) and (A.TA002 = B.TB002) WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillLine] where A.TA001 = BillLine.InvoiceH and A.TA002 = BillLine.InvoiceNo) and TA038 between '" & aa & "'" & ake & "'" & bb & "'" & ake & " TA004 ='" & lblcustid.Text & "' ORDER BY TA002"

        ElseIf txtfromdate.Text = "" And txttodate.Text = "" And txtPoEdit.Text <> "" Then
            SqlDataSource4.SelectCommand = "select distinct TA004, TA038, TA001,TA002, TA020, TA042, TA041,TA098 from [HOOTHAI].[dbo].[ACRTA] A JOIN [HOOTHAI].[dbo].[ACRTB] B ON (A.TA001 = B.TB001) and (A.TA002 = B.TB002) WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillLine] where A.TA001 = BillLine.InvoiceH and A.TA002 = BillLine.InvoiceNo) and TA004 ='" & lblcustid.Text & "' and TB048 like '" & txtPoEdit.Text & "%' ORDER BY TA002"

        End If
        Me.GridEdit.DataBind()
        GridEdit.Visible = True

    End Sub

    Protected Sub Bueditall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bueditall.Click
        GridEdit.Visible = True
        For Each gvr As GridViewRow In GridEdit.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = True
        Next
    End Sub

    Protected Sub Bueditcall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bueditcall.Click
        GridEdit.Visible = True
        For Each gvr As GridViewRow In GridEdit.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = False
        Next
    End Sub

    Private Sub GridView5_RowCommand1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView5.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            lblbillno.Text = GridView5.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
            lblcustid.Text = GridView5.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            lblcustomer.Text = GridView5.Rows(i).Cells(2).Text.Replace("&nbsp;", "")

            Dim BillNo As String
            BillNo = Conn_sql.Get_value("select BillNo from Billhead where BillShow='" & lblbillno.Text & "'", Conn_sql.MIS_ConnectionString)
            BillNoEdit.Text = BillNo

        End If

    End Sub
End Class