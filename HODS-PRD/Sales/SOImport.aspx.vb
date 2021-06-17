Imports OfficeOpenXml
Imports System.IO
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class SOImport
	Inherits System.Web.UI.Page

	'Dim ControlForm As New ControlDataForm
	'Dim Conn_SQL As New ConnSQL
	Dim ConfigDate As New ConfigDate
	Dim createTableSale As New createTableSale

	Dim dtCont As New DataTableControl
	Dim ddlCont As New DropDownListControl
	Dim dbConn As New DataConnectControl
	Dim hashCont As New HashtableControl
	Dim fileCont As New FileControl
	Dim gvCont As New GridviewControl

	Const table As String = "CodeInfo"
	Const codeHead As String = "PLANT"
	Const codeTime As String = "TIME_SHIP"


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			If Session("UserName") = "" Then
				'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
			End If
			LbListSo.Visible = False
			btReset_Click(sender, e)
		End If

	End Sub

	Protected Sub Reset()
		Dim SQL As String = "select rtrim(MQ001) MQ001,rtrim(MQ001)+':'+rtrim(MQ002) MQ002 from CMSMQ where UDF01='Yes'  order by MQ001 "
		ddlCont.showDDL(ddlTypeSo, SQL, VarIni.ERP, "MQ002", "MQ001", False)

		Dim dateToday As String = Date.Now.ToString("dd/MM/yyyy")
		ucDocDate.dateVal = dateToday
		'ucDueDate.dateVal = dateToday

		SQL = "select rtrim(MA001) MA001,rtrim(MA001)+':'+MA002 MA002 from COPMA where MA097 = '1' and substring(COPMA.UDF01,1,1)='Y' order by MA001 "
		ddlCont.showDDL(ddlCust, SQL, VarIni.ERP, "MA002", "MA001", False)

		'SQL = "select rtrim(Code) MD001,Code+':'+Name MD002 from " & table & " where CodeType='" & codeTime & "' order by Code "
		'ddlCont.showDDL(ddlTime, SQL, VarIni.DBMIS, "MD002", "MD001", False)

		tbRemark.Text = ""
		btSave.Visible = False

		lbSONumber.Text = ""
		lbQty.Text = ""
		lbAmt.Text = ""

		gvShow.DataSource = ""
		gvShow.DataBind()

	End Sub

	Protected Sub ddlCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCust.SelectedIndexChanged
		ddlPlant.Items.Clear()
		Dim SQL As String = "select rtrim(Code) MD001,Code+':'+Name MD002 from " & table & " where CodeType='" & codeHead & "' and WC like '%" & ddlCust.Text.Trim & "%' order by Code "
		ddlCont.showDDL(ddlPlant, SQL, VarIni.DBMIS, "MD002", "MD001", False)
	End Sub

	Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
		Reset()
		ddlCust_SelectedIndexChanged(sender, e)
	End Sub

	Protected Sub btCheck_Click(sender As Object, e As EventArgs) Handles btCheck.Click
		Dim dtExcleRead As DataTable = fileCont.ReadFileExcel(fuUpload, If(CbCut.Checked, New List(Of String) From {"3", "0"}, New List(Of String)))

		'Dim dtViewExcleRead As New DataView(dtExcleRead) With {
		'	.Sort = " " & dtExcleRead.Columns(2).ColumnName & ", " & dtExcleRead.Columns(0).ColumnName & " "
		'}

		Dim dt = New DataTable
		dt.Columns.Add("Seq") '0
		dt.Columns.Add("Item") '1
		dt.Columns.Add("Desc") '2
		dt.Columns.Add("Spec") '3
		dt.Columns.Add("Main WH") '4
		dt.Columns.Add("Qty", Type.GetType("System.Double")) '5
		dt.Columns.Add("Price", Type.GetType("System.Double")) '6
		dt.Columns.Add("Amount", Type.GetType("System.Double")) '7
		dt.Columns.Add("Due Date") '8
		dt.Columns.Add("Model") '9
		dt.Columns.Add("WO") '10
		dt.Columns.Add("Line") '11
		dt.Columns.Add("Unit") '12
		dt.Columns.Add("QPA Cust") '13
		dt.Columns.Add("Cust Order") '14
		Dim dateToday As String = If(ucDocDate.dateVal = "", Date.Now.ToString("yyyyMMdd"), ucDocDate.dateVal)

		Dim line As Integer = 1
		Dim SQL As String
		Dim sumQty As Decimal = 0,
			sumAmt As Decimal = 0
		Dim custCode As String = ddlCust.Text.Trim
		Dim priceHash As New Hashtable
		Dim custWoLast As String = ""

		For Each dr As DataRow In dtExcleRead.Rows
			With New DataRowControl(dr)
				Dim spec As String = .Text(0)
				If Not String.IsNullOrEmpty(spec) Then
					Dim qty As Decimal = .Number(1),
					dueDate As String = .Text(2)
					Dim valCheck As String = custCode & VarIni.char8 & spec
					'get basic info for spec
					If Not hashCont.existDataHash(priceHash, valCheck) Then
						'SQL = " select INV.MB001,INV.MB002,INV.MB003,INV.MB004,INV.MB017,COP.MB008,INV.UDF02 from INVMB INV " &
						'  " left join COPMB COP on COP.MB002=INV.MB001 and COP.MB001='" & ddlCust.Text.Trim & "' and COP.MB017<='" & dateToday & "' " &
						'  " where INV.MB109 = 'Y' and  INV.MB003 like '" & spec & "REV%' and INV.UDF05 <>'' and INV.UDF06 <>'' " &
						'  " order by COP.MB017 desc "
						SQL = "select INVMB.MB001,INVMB.MB002,INVMB.MB003,INVMB.MB004,INVMB.MB017,COPMB.MB008 from (
							select MB001,MB002,MB003,MB004,MB017,UDF02 from (
							select MB001,MB002,MB003,MB004,MB017,UDF02 ,ROW_NUMBER() over(partition by MB003 order by MB001 desc) rowNumber  from INVMB  where MB109 = 'Y' and MB005 <> '1501' and  UPPER(MB003) like '" & spec & "REV%'
							)INVMB where rowNumber=1
							)INVMB
							left join (
								select * from (
									select MB001,MB002,MB008,MB017,MB018,row_number() over(partition by MB001,MB002 order by MB017 desc) rNo from (select MB001,MB002,MB008,MB017, case when MB018 ='' then convert(varchar, getdate(), 112) else MB018 end MB018 from COPMB ) COPMB where  convert(varchar, getdate(), 112) between MB017 and  MB018
								)COPMB where rNo=1
							)COPMB on COPMB.MB002=INVMB.MB001
							where COPMB.MB001='" & ddlCust.Text.Trim & "'"

						hashCont.addDataHash(priceHash, valCheck, dbConn.QueryDataRow(SQL, VarIni.ERP, dbConn.WhoCalledMe))
					End If
					Dim drGet As New DataRowControl(hashCont.getDataHashDatarow(priceHash, valCheck))
					Dim item As String = drGet.Text("MB001"),
						desc As String = drGet.Text("MB002"),
						mainWh As String = drGet.Text("MB017"),
						price As Decimal = drGet.Number("MB008"),
						unit As String = drGet.Text("MB004")
					spec = drGet.Text("MB003", spec)
					Dim custwo As String = .Text(3)
					If CbCut.Checked AndAlso custwo <> custWoLast Then
						If Not String.IsNullOrEmpty(custWoLast) Then
							line = 1
						End If
					End If

					Dim row = dt.NewRow
					row(0) = line.ToString("0000") 'seq
					row(1) = item 'Item
					row(2) = desc 'Desc
					row(3) = spec 'Spec
					row(4) = mainWh 'Main WH
					row(5) = qty 'Qty
					sumQty += qty
					row(6) = price 'Price
					row(7) = qty * price 'Amount
					sumAmt += (qty * price)
					row(8) = If(dueDate = "", Date.Now.ToString("yyyyMMdd"), dueDate) 'Due Date
					row(9) = .Text(4) 'model
					row(10) = custwo 'W/O
					row(11) = .Text(5)  'Line
					row(12) = unit 'unit
					row(13) = .Text(6) 'cust order qpa
					row(14) = .Text(7) 'cust order
					dt.Rows.Add(row)
					line += 1
					custWoLast = custwo
				End If
			End With
		Next
		lbQty.Text = sumQty
		lbAmt.Text = sumAmt

		'Dim dataView As New DataView(dt) With {
		'	.Sort = " WO, Item "
		'}
		'Dim dataTable As DataTable = dataView.ToTable()

		gvCont.ShowGridView(gvShow, dt)
		If gvShow.Rows.Count > 0 Then 'And gvCustShow.Columns.Count > 3
			lbSONumber.Text = getDocNo()
			btSave.Visible = True
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollList", "gridviewScrollList();", True)
		End If


	End Sub

	Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
		'check data for save
		If ucDocDate.dateVal = "" Then
			show_message.ShowMessage(Page, "Doc date Is empty!!", UpdatePanel1)
			ucDocDate.Focus()
			Exit Sub
		End If
		'Dim docNo As String = lbSONumber.Text
		'save Head
		'HeadRecord()
		'save body
		BodyRecord()
		Dim listNo As String = LbListSo.Text
		show_message.ShowMessage(Page, "SAVE DATA COMPLETED Sale Order Number=\n" & listNo.Substring(0, listNo.Length - 1).Replace(",", "\n") & "\n !!", UpdatePanel1)
		btReset_Click(sender, e)

	End Sub

	Function getDocNo() As String
		Dim SQL As String,
			dt As DataTable = New DataTable
		Dim dateVal As String = ucDocDate.dateVal
		SQL = "select top 1 isnull(cast(max(TC002) as Decimal(20,0))+1,'" & dateVal & "001') TC002 from COPTC where TC001='" & Trim(ddlTypeSo.Text) & "' and TC039 ='" & dateVal & "' order by TC002 desc"
		Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.ERP, dbConn.WhoCalledMe)
		Return dtCont.IsDBNullDataRow(dr, "TC002", dateVal & "001")
	End Function

	Sub HeadRecord(custwo As String, amt As Decimal)
		lbSONumber.Text = getDocNo()
		Dim cust As String = Trim(ddlCust.Text)
		Dim docDate As String = ucDocDate.dateVal
		Dim SQL As String = "select MA010,MA083,MA031,MA014,MA038,MA025,MA026 from COPMA where MA001='" & cust & "' "
		'MA010 tax register
		'MA083 code Payment term
		'MA031 name payment term
		'MA014 currency
		'MA038 tax type
		'MA025 address 1
		'MA026 address 2
		'Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.ERP, dbConn.WhoCalledMe)

		With New DataRowControl(SQL, VarIni.ERP, dbConn.WhoCalledMe)
			'Dim amt As String = lbAmt.Text.Trim
			Dim whr As Hashtable = New Hashtable From {
				{"TC001", Trim(ddlTypeSo.Text)},
				{"TC002", lbSONumber.Text.Trim}
			}
			Dim fld As Hashtable = New Hashtable From {
				{"COMPANY", VarIni.ERP},
				{"CREATOR", "H" & Session("UserName")},
				{"USR_GROUP", "HODS"},
				{"CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss")},
				{"FLAG", "2"},
				{"TC003", docDate}, 'transaction Date
				{"TC004", cust}, 'Customer
				{"TC007", "HT"}, 'Plant
				{"TC009", "1"}, 'exchange rate
				{"TC008", .Text("MA014")}, 'Currency
				{"TC010", .Text("MA025")}, 'addres1
				{"TC011", .Text("MA026")}, 'addres2
				{"TC015", custwo}, 'remark add cust WO
				{"TC042", .Text("MA083")}, 'payment term
				{"TC014", .Text("MA031")}, 'payment term name
				{"TC016", .Text("MA038")}, 'Tax Type
				{"TC073", .Text("MA010")}, 'Tax Register No.
				{"TC019", "1"}, 'Delivery Term
				{"TC027", "N"}, 'app indicator
				{"TC029", amt}, 'all amount
				{"TC030", "0"}, 'all tax
				{"TC031", lbQty.Text.Trim}, 'all Qty
				{"TC039", docDate}, 'doc date
				{"TC048", "N"}, 'E-Approval Status
				{"TC050", "N"}, 'Post Status
				{"TC059", "N"}, 'EBCExport Indicator
				{"TC070", "N"}, 'Unlimited release
				{"TCI01", docDate}, 'Effect Date
				{"TCI02", docDate}, 'Install Completion Date
				{"TCI03", amt}, 'Order amout un-include tax(B/C)
				{"UDF01", Trim(ddlPlant.Text)}, 'Plant
				{"UDF02", ""} 'time
			}
			Dim ISQL As String = dbConn.GetSQL("COPTC", fld, whr, "II")
			If dbConn.TransactionSQL(ISQL, VarIni.ERP, dbConn.WhoCalledMe) = 0 Then
				HeadRecord(custwo, amt)
			End If
		End With
	End Sub

	'Function ReturnLine(line As Integer) As String
	'	Return line.ToString("0000")
	'End Function

	Sub BodyRecord()
		Dim whr As Hashtable,
			fld As Hashtable,
			strSQL As String = ""

		Dim soType As String = Trim(ddlTypeSo.Text),
			soNo As String
		Dim sqlList As New ArrayList
		Dim custWOLast As String = ""
		Dim woHash As New Hashtable

		For Each gr As GridViewRow In gvShow.Rows
			With New GridviewRowControl(gr)
				Dim custwo As String = "ALL"
				If CbCut.Checked Then
					custwo = .Text(10).Replace("&nbsp;", "")
				End If
				hashCont.CountItemDataHash(woHash, custwo, .Number(7))
			End With
		Next

		custWOLast = ""
		For Each gr As GridViewRow In gvShow.Rows
			With New GridviewRowControl(gr)
				Dim custWO As String = .Text(10).Replace("&nbsp;", "")
				If CbCut.Checked Then
					If custWO <> custWOLast Then
						If Not String.IsNullOrEmpty(custWOLast) Then
							RunSQL(sqlList, soType, soNo)
						End If
						HeadRecord(custWO, hashCont.getDataHashDecimal(woHash, custWO))
						soNo = lbSONumber.Text.Trim
						sqlList = New ArrayList
					End If
				Else
					HeadRecord(custWO, hashCont.getDataHashDecimal(woHash, "ALL"))
				End If
				whr = New Hashtable From {
					{"TD001", soType}, 'type
					{"TD002", lbSONumber.Text.Trim}, 'no
					{"TD003", .Text(0)} 'seq
				}
				fld = New Hashtable From {
					{"COMPANY", VarIni.ERP},
					{"CREATOR", "H" & Session("UserName")},
					{"USR_GROUP", "HODS"},
					{"CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss")},
					{"FLAG", "2"},
					{"TD004", .Text(1)}, 'item
					{"TD005", .Text(2)}, 'desc
					{"TD006", .Text(3)}, 'spec
					{"TD007", .Text(4)}, 'Main WH
					{"TD008", .Number(5)}, 'qty
					{"TD010", .Text(12)}, 'unit
					{"TD011", .Number(6)}, 'price
					{"TD012", .Number(7)}, 'amount
					{"TD013", .Text(8)}, 'due date
					{"TD016", "N"}, 'Close
					{"TD021", "N"}, 'app indicator
					{"TD026", "1"}, 'discount rate
					{"UDF51", .Number(13)}, 'qpa usage
					{"TD038", .Number(7)}, 'amount un-include tax
					{"TDD01", "N"}, '???
					{"TDI01", .Number(7)}, 'Amount un-include tax(B/C)
					{"TDI03", .Number(7)}, 'Amount (B/C)
					{"TD062", "N"}, '???
					{"UDF02", custWO}, 'W/O
					{"UDF03", .Text(11).Replace("&nbsp;", "")}, 'Line
					{"UDF04", .Text(9).Replace("&nbsp;", "")} 'Model
				}
				'{"TD027", ""}, 'cust po
				sqlList.Add(dbConn.GetSQL("COPTD", fld, whr, "I"))
				custWOLast = custWO
			End With
		Next
		RunSQL(sqlList, soType, soNo)

	End Sub

	Sub RunSQL(SqlList As ArrayList, soType As String, soNo As String)
		If SqlList.Count > 0 Then
			dbConn.TransactionSQL(SqlList, VarIni.ERP, dbConn.WhoCalledMe)
			LbListSo.Text &= soType & "-" & soNo & VarIni.C
		End If
	End Sub


End Class