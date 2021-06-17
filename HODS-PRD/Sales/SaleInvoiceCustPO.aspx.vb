Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class SaleInvoiceCustPO
	Inherits System.Web.UI.Page

	Dim dbConn As New DataConnectControl
	Dim datecont As New DateControl
	Dim dtCont As New DataTableControl
	Dim ddlCont As New DropDownListControl
	Dim hashCont As New HashtableControl
	Dim fileCont As New FileControl
	Dim gvCont As New GridviewControl
	Const table As String = "CodeInfo"
	Const codeHead As String = "PLANT"
	Dim qtyHash As New Hashtable

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			UcDdlCust.setAutoPostback()
			UcDdlPlant.setAutoPostback()
			'UcDdlSaleDel.setAutoPostback()
			UcDdlSaleInvoice.setAutoPostback()
			BtReset_Click(sender, e)
			'set to hide
			Dim show As Boolean = False
			LbSaleInvoiceType.Visible = show
			LbSaleInvoiceNo.Visible = show
			show = False
			If Session(VarIni.UserName) = "500026" Then
				show = True
			End If
			BtACRLB.Visible = show
		End If
	End Sub

	Private Sub UcDdlCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UcDdlCust.SelectedIndexChanged
		Dim SQL As String = "select distinct rtrim(Code) Code,rtrim(Code)+':'+rtrim(Name) Name from " & table & " where CodeType='" & codeHead & "' " & dbConn.WHERE_LIKE("WC", UcDdlCust.Text) & " order by Code "
		UcDdlPlant.show(SQL, VarIni.DBMIS, "Name", "Code", True)
		UcDdlPlant_SelectedIndexChanged(sender, e)
	End Sub

	Private Sub UcDdlPlant_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UcDdlPlant.SelectedIndexChanged
		Dim SQL As String = ""
		Dim DateS As String = UcShipDate.textEmptyToday
		Dim cust As String = UcDdlCust.Text
		Dim plant As String = UcDdlPlant.Text
		Dim dtDel As DataTable = Nothing
		Dim dtInvoice As DataTable = Nothing

		If cust <> "0" And plant <> "0" Then
			'show sale delivery and TA025 = 'Y' 
			'actual
			SQL = "select distinct TH001+'-'+TH002 CODE,TH001+'-'+TH002+ case when COPTG.UDF03='' then '' else  '('+COPTG.UDF03+')' end NAME from COPTH 
				left join COPTG on TG001=TH001 and TG002=TH002
				left join (select TA004,TB005,TB006,TB007,sum(TB022) TB022 from ACRTB 
							left join ACRTA on TA001=TB001 and TA002=TB002
							where TB004 = '1' 
							group by TA004,TB005,TB006,TB007
						  ) B on TB005=TH001 and TB006=TH002 and TB007=TH003
				where  COPTG.TG023='Y' and TH008>isnull(TB022,0)  and TG003='" & DateS & "' 
				  and  TG004='" & cust & "' and COPTG.UDF02='" & plant & "'
				order by CODE"
			''test
			'SQL = "select distinct TH001+'-'+TH002 CODE,TH001+'-'+TH002+ case when COPTG.UDF03='' then '' else  '('+COPTG.UDF03+')' end NAME from COPTH 
			'	left join COPTG on TG001=TH001 and TG002=TH002
			'	left join (select TA004,TB005,TB006,TB007,sum(TB022) TB022 from ACRTB 
			'				left join ACRTA on TA001=TB001 and TA002=TB002
			'				where TB004 = '1' and TB006 not in ('20201117008')
			'				group by TA004,TB005,TB006,TB007
			'			  ) B on TB005=TH001 and TB006=TH002 and TB007=TH003
			'	where  COPTG.TG023='Y' and TH008>isnull(TB022,0)  and TG003='" & DateS & "' 
			'	  and  TG004='" & cust & "' and COPTG.UDF02='" & plant & "'
			'	order by CODE"

			dtDel = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)

			'show sale invoice
			SQL = "select ACRTA.TA001+'-'+ ACRTA.TA002 CODE from ACRTA 
				 left join (select distinct ACRTB.TB001,ACRTB.TB002 from ACRTB) V on V.TB001=ACRTA.TA001 and V.TB002=ACRTA.TA002
				where ACRTA.TA025 = 'N' and V.TB002 is null and ACRTA.TA038='" & DateS & "'  and ACRTA.TA004='" & cust & "' and
					  ACRTA.UDF06='" & plant & "' 
			 order by TA038,TA002"
			dtInvoice = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)

		End If

		'UcDdlSaleDel.show(dtDel, "NAME", "CODE", True)
		UcCblSaleDel.show(dtDel, "NAME", "CODE", 1)
		Dim showBut As Boolean = False

		If dtDel IsNot Nothing Then
			showBut = If(dtDel.Rows.Count > 0, True, False)
		End If
		BtSaleDel.Visible = showBut

		UcDdlSaleInvoice.show(dtInvoice, "CODE", "CODE", True)

		Dim dt As DataTable = Nothing
		gvCont.ShowGridView(GvLeft, dt)
		gvCont.ShowGridView(GvRight, dt)

	End Sub

	Private Sub BtSaleDel_Click(sender As Object, e As EventArgs) Handles BtSaleDel.Click
		'If String.IsNullOrEmpty(UcCblSaleDel.getValue) Then
		'	show_message.ShowMessage(Page, "กรุณาเลือกเลขที่สำหรับออก Invoice???", UpdatePanel1)
		'	Exit Sub
		'End If
		Dim whr As String = dbConn.WHERE_IN("TH001+'-'+TH002", UcCblSaleDel.getObject)

		Dim SQL As String = ""
		SQL = "select TH001,TH002,TH003,TH006,TH008-isnull(TB022,0) TH008,TH009,TH012,TH007,TH004,TH005,isnull(PO_BAL,0) PO_BAL,MA005 from COPTH 
				left join COPTG on TG001=TH001 and TG002=TH002
				left join (select TA004,TB005,TB006,TB007,sum(TB022) TB022 from ACRTB 
							left join ACRTA on TA001=TB001 and TA002=TB002
							where TB004 = '1' and TA025 in( 'N','Y') 
							group by TA004,TB005,TB006,TB007
							) B on TB005=TH001 and TB006=TH002 and TB007=TH003
				left join (
							select Item,sum(PO_BAL) PO_BAL from (
							select Cust,CustPOD,Item,Plant,Qty-QtyVoid-isnull(TB022,0) PO_BAL from HOOTHAI_REPORT..CustPODetail
							left join   HOOTHAI_REPORT..CustPOInfo on CustPO=CustPOD
							left join
							(
								select ACRTA.TA004,ACRTB.TB039,ACRTB.TB048, ACRTA.UDF06,
								sum(ACRTB.TB022) TB022,sum(case ACRTA.TA025 when 'Y' then ACRTB.TB022 else 0 end) TB022Y,sum(case ACRTA.TA025 when 'N' then ACRTB.TB022 else 0 end) TB022N
								from ACRTB left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002
								where ACRTA.TA025 in ('N','Y') and ACRTB.TB048 is not null and rtrim(ACRTB.TB048)<>'' and ACRTA.TA005 not in ('20201117008') 
								group by ACRTB.TB048, ACRTB.TB039,ACRTA.TA004,ACRTA.UDF06
							) A on A.TA004=Cust and A.TB039=Item and A.TB048=CustPOD and A.UDF06=Plant
							where 1=1 and Cust='" & UcDdlCust.Text & "' and Plant='" & UcDdlPlant.Text & "'
							)B group by Item
						) PO on  PO.Item=TH004
				left join INVMB on MB001=TH004
				left join INVMA on INVMA.MA001='1' and INVMA.MA002=MB005 and INVMA.MA011 = 'Y' 
				where COPTG.TG023='Y' and TH008>isnull(TB022,0) " & whr & " 
				order by TH004,TH001,TH002,TH003"

		''test
		'SQL = "select TH001,TH002,TH003,TH006,TH008-isnull(TB022,0) TH008,TH009,TH012,TH007,TH004,TH005,isnull(PO_BAL,0) PO_BAL,MA005 from COPTH 
		'		left join COPTG on TG001=TH001 and TG002=TH002
		'		left join (select TA004,TB005,TB006,TB007,sum(TB022) TB022 from ACRTB 
		'					left join ACRTA on TA001=TB001 and TA002=TB002
		'					where TB004 = '1' and TA025 in( 'N','Y') and TB006 not in ('20201117008')
		'					group by TA004,TB005,TB006,TB007
		'					) B on TB005=TH001 and TB006=TH002 and TB007=TH003
		'		left join (
		'					select Item,sum(PO_BAL) PO_BAL from (
		'					select Cust,CustPOD,Item,Plant,Qty-QtyVoid-isnull(TB022,0) PO_BAL from HOOTHAI_REPORT..CustPODetail
		'					left join   HOOTHAI_REPORT..CustPOInfo on CustPO=CustPOD
		'					left join
		'					(
		'						select ACRTA.TA004,ACRTB.TB039,ACRTB.TB048, ACRTA.UDF06,
		'						sum(ACRTB.TB022) TB022,sum(case ACRTA.TA025 when 'Y' then ACRTB.TB022 else 0 end) TB022Y,sum(case ACRTA.TA025 when 'N' then ACRTB.TB022 else 0 end) TB022N
		'						from ACRTB left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002
		'						where ACRTA.TA025 in ('N','Y') and ACRTB.TB048 is not null and rtrim(ACRTB.TB048)<>'' and ACRTA.TA005 not in ('20201117008') 
		'						group by ACRTB.TB048, ACRTB.TB039,ACRTA.TA004,ACRTA.UDF06
		'					) A on A.TA004=Cust and A.TB039=Item and A.TB048=CustPOD and A.UDF06=Plant
		'					where 1=1 and Cust='" & UcDdlCust.Text & "' and Plant='" & UcDdlPlant.Text & "'
		'					)B group by Item
		'				) PO on  PO.Item=TH004
		'		left join INVMB on MB001=TH004
		'		left join INVMA on INVMA.MA001='1' and INVMA.MA002=MB005 and INVMA.MA011 = 'Y' 
		'		where COPTG.TG023='Y' and TH008>isnull(TB022,0) " & whr & " 
		'		order by TH004,TH001,TH002,TH003"

		Dim dt As DataTable = Nothing
		dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
		gvCont.ShowGridView(GvLeft, dt)
		dt = Nothing
		gvCont.ShowGridView(GvRight, dt)
		UcDdlSaleInvoice_SelectedIndexChanged(sender, e)
		Dim showButton As Boolean = False
		If GvLeft.Rows.Count > 0 Then
			showButton = True
		End If
		BtSelect.Visible = showButton
	End Sub

	Protected Sub BtReset_Click(sender As Object, e As EventArgs) Handles BtReset.Click
		'LbSaleDelType.Text = ""
		'LbSaleDelNo.Text = ""
		LbSaleInvoiceType.Text = ""
		LbSaleInvoiceNo.Text = ""

		UcShipDate.Text = DateTime.Now.ToString("yyyyMMdd")

		'cust
		Dim SQL As String = "select rtrim(MA001) MA001,rtrim(MA001)+':'+MA002 MA002 from COPMA where MA097 = '1' and substring(COPMA.UDF01,1,1)='Y' order by MA001 "
		UcDdlCust.show(SQL, VarIni.ERP, "MA002", "MA001", True)

		UcDdlCust_SelectedIndexChanged(sender, e)
		UcDdlPlant_SelectedIndexChanged(sender, e)
		'UcDdlSaleDel_SelectedIndexChanged(sender, e)
		'UcDdlSaleInvoice_SelectedIndexChanged(sender, e)
		BtSave.Visible = False
		BtSelect.Visible = False
		BtSaleDel.Visible = False

	End Sub

	Private Sub GvLeft_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvLeft.RowDataBound
		With e.Row
			If .RowType = DataControlRowType.DataRow Then
				With New GridviewRowControl(e.Row)
					Dim item As String = .Text(10)
					hashCont.CountItemDataHash(qtyHash, item, .Number(5))
					If .Number(9) < hashCont.getDataHashDecimal(qtyHash, item) Then 'po bal<qty  
						Dim cbSelect As CheckBox = .FindControl("cbSelect")
						cbSelect.Checked = False
						cbSelect.Enabled = False
						e.Row.BackColor = Drawing.Color.Red
						e.Row.ForeColor = Drawing.Color.White
						e.Row.BorderColor = Drawing.Color.LightYellow
					End If
				End With
			End If
		End With
	End Sub

	Private Sub UcShipDate_ChangeEvent(sender As Object, e As EventArgs) Handles UcShipDate.ChangeEvent
		UcDdlPlant_SelectedIndexChanged(sender, e)
	End Sub

	Protected Sub BtSelect_Click(sender As Object, e As EventArgs) Handles BtSelect.Click
		'set head for gridview
		Dim SQL As String = ""
		Dim al As New ArrayList
		Dim colName As ArrayList
		With New ArrayListControl(al)
			.TAL("SEQ", "Inv Seq")
			.TAL("TB005", "Del Type")
			.TAL("TB006", "Del No")
			.TAL("TB007", "Del Seq")
			.TAL("TB041", "Spec")
			.TAL("TB048", "Cust PO")
			.TAL("TB023", "Price", "3")
			.TAL("TB022", "Qty", "2")
			.TAL("TB042", "Unit")
			.TAL("TB039", "ITEM")
			.TAL("TB040", "Desc")
			.TAL("TB013", "A/C Code")
			colName = .ChangeFormat(True)
		End With
		Dim dtShow As DataTable = dtCont.setColDatatable(colName, VarIni.C8)
		Dim custCode As String = UcDdlCust.Text
		Dim qtyOldHash As New Hashtable
		Dim dtHash As New Hashtable
		Dim qtyOldVal As Decimal '= hashCont.getDataHashDecimal(qtyOldHash, item)

		Dim seq As Integer = 1
		Const C88 As String = VarIni.C8 & VarIni.C8
		For Each gr As GridViewRow In GvLeft.Rows
			With New GridviewRowControl(gr)
				Dim cbSelect As CheckBox = .FindControl("cbSelect")
				If cbSelect.Checked Then
					Dim TypeDel As String = .Text(1)
					Dim NumberDel As String = .Text(2)
					Dim SeqDel As String = .Text(3)
					Dim spec As String = .Text(4)
					Dim qtyDelivery As Decimal = .Number(5)
					Dim unit As String = .Text(6)
					Dim price As Decimal = .Number(7)
					Dim item As String = .Text(10)
					Dim desc As String = .Text(11)
					Dim acCode As String = .Text(12)
					'hashCont.getHash(qtyOldHash, item, qtyOldVal)
					If Not hashCont.existDataHash(dtHash, item) Then
						'get polist 
						SQL = "select Cust,Item,CustPOD,PO_BAL from (
								select Cust,CustPOD,Item,Plant,Qty-QtyVoid-isnull(TB022,0) PO_BAL from HOOTHAI_REPORT..CustPODetail
								left join HOOTHAI_REPORT..CustPOInfo on CustPO=CustPOD
								left join
								(
									select ACRTA.TA004,ACRTB.TB039,ACRTB.TB048,ACRTA.UDF06, 
									sum(ACRTB.TB022) TB022,sum(case ACRTA.TA025 when 'Y' then ACRTB.TB022 else 0 end) TB022Y,sum(case ACRTA.TA025 when 'N' then ACRTB.TB022 else 0 end) TB022N
									from ACRTB left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002
									where ACRTA.TA025 in ('N','Y') and ACRTB.TB048 is not null and rtrim(ACRTB.TB048)<>'' 
									group by ACRTB.TB048, ACRTB.TB039,ACRTA.TA004,ACRTA.UDF06
								) A on A.TA004=Cust and A.TB039=Item and A.TB048=CustPOD and A.UDF06 = Plant
								) B where Item='" & item & "' and Cust='" & custCode & "' and Plant='" & UcDdlPlant.Text & "' and PO_BAL>0
								order by CustPOD "
						hashCont.AddHash(dtHash, item, dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe))
					End If

					Dim dtsub As DataTable = hashCont.getHashDatatable(dtHash, item)
					qtyOldVal = hashCont.getDataHashDecimal(qtyOldHash, item)
					For Each dr As DataRow In dtsub.Rows
						With New DataRowControl(dr)
							Dim poQty As Decimal = .Number("PO_BAL")
							Dim custPO As String = .Text("CustPOD")
							If qtyOldVal - poQty >= 0 Then
								qtyOldVal -= poQty
							Else
								If qtyOldVal > 0 And qtyOldVal - poQty < 0 Then
									poQty = Math.Abs(qtyOldVal - poQty)
									qtyOldVal = 0
								End If

								If poQty > 0 And qtyDelivery > 0 Then
									Dim cQty As Decimal = qtyDelivery
									If poQty < qtyDelivery Then
										cQty = poQty
									End If
									qtyDelivery -= cQty
									Dim dataFld As New ArrayList From {
										"SEQ" & C88 & seq.ToString("0000"),'seq to sale invoice
										"TB005" & C88 & TypeDel,'type of delivery
										"TB006" & C88 & NumberDel,'number of delivery
										"TB007" & C88 & SeqDel,'seq of delivery
										"TB041" & C88 & spec,'spec
										"TB048" & C88 & custPO,'cust po
										"TB023" & C88 & price,'price
										"TB022" & C88 & cQty,'po qty
										"TB042" & C88 & unit,'unit
										"TB039" & C88 & item,'item
										"TB040" & C88 & desc,'desc
										"TB013" & C88 & acCode'account code
									}
									dtCont.addDataRow(dtShow, dr, dataFld, VarIni.char8)
									seq += 1
									hashCont.CountItemDataHash(qtyOldHash, item, cQty)
								Else
									Exit For
								End If
							End If
						End With
					Next

				End If
			End With
		Next
		gvCont.GridviewInitial(GvRight, colName, strSplit:=VarIni.C8)
		gvCont.ShowGridView(GvRight, dtShow)

		'set to hide column
		Dim colHide As New List(Of Integer) From {8, 9}
		gvCont.hideColumn(GvRight, colHide, "displayNone")
	End Sub

	Private Sub UcDdlSaleInvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UcDdlSaleInvoice.SelectedIndexChanged
		Dim si As String = UcDdlSaleInvoice.Text
		Dim siType As String = ""
		Dim siNo As String = ""
		Dim showButton As Boolean = False
		If si <> "0" Then
			Dim ddd() As String = si.Split("-")
			siType = ddd(0)
			siNo = ddd(1)

			'show button to save
			If GvLeft.Rows.Count > 0 Then
				showButton = True
			End If
		End If
		LbSaleInvoiceType.Text = siType
		LbSaleInvoiceNo.Text = siNo
		BtSave.Visible = showButton
	End Sub

	Protected Sub BtSave_Click(sender As Object, e As EventArgs) Handles BtSave.Click
		Dim SQL As String = ""
		'check data before save
		'get data for sale invoice
		Dim company As String = ""
		Dim userGroup As String = ""
		Dim flag As String = ""
		Dim dept As String = ""
		Dim taxRate As Decimal = 0
		Dim taxType As String = ""
		Dim exchangeRate As Decimal = 0
		Dim docType As String = LbSaleInvoiceType.Text
		Dim docNumber As String = LbSaleInvoiceNo.Text
		Dim itemHash As New Hashtable
		SQL = "select COMPANY, USR_GROUP,FLAG,TA070,TA040,TA012,TA010 from ACRTA where TA001='" & docType & "' and TA002='" & docNumber & "'"
		With New DataRowControl(SQL, VarIni.ERP, dbConn.WhoCalledMe)
			company = .Text("COMPANY")
			userGroup = .Text("USR_GROUP")
			flag = .Text("FLAG")
			dept = .Text("TA070")
			taxRate = .Number("TA040")
			taxType = .Text("TA012")
			exchangeRate = .Number("TA010")
		End With
		Dim sumAmt As Decimal = 0
		Dim sumTax As Decimal = 0
		Dim fld As New Hashtable
		Dim whr As New Hashtable
		'sss
		Dim SaleDelHash As New Hashtable
		'record sale invoice
		Dim sqlList As New ArrayList
		For Each gr As GridViewRow In GvRight.Rows
			With New GridviewRowControl(gr)
				Dim qty As Decimal = .Number(7) '"TB022"
				Dim price As Decimal = .Number(6) '"TB023"
				Dim amt As Decimal = Math.Round(qty * price, 2)
				Dim tax As Decimal = 0
				'calculate on local only
				Select Case taxType
					Case 2 'tax un-include
						tax = Math.Round(amt * taxRate, 2)
						'Case 4 'tax free
				End Select
				'Dim saleDelNumber As String = LbSaleDelType.Text & "-" & LbSaleDelNo.Text & "-" & .Text(1)
				fld = New Hashtable From {
					{"COMPANY", company},
					{"CREATOR", Session(VarIni.UserName)},
					{"USR_GROUP", userGroup},
					{"CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss")},
					{"FLAG", flag},
					{"TB001", docType},
					{"TB002", docNumber},
					{"TB003", .Text(0)},'seq
					{"TB004", "1"},
					{"TB005", .Text(1)},'sale delivery type
					{"TB006", .Text(2)},'sale delivery number
					{"TB007", .Text(3)},'TB007
					{"TB008", UcShipDate.textEmptyToday},
					{"TB011", ""},'remark
					{"TB012", "Y"},
					{"TB013", .Text(11)},'"TB013"
					{"TB017", amt},
					{"TB018", tax},
					{"TB019", Math.Round(amt * exchangeRate, 2)},
					{"TB020", Math.Round(tax * exchangeRate, 2)},
					{"TB021", dept},
					{"TB022", qty},
					{"TB023", price},
					{"TB024", "1"},
					{"TB039", .Text(9)},'"TB039"
					{"TB040", .Text(10)},'"TB040"
					{"TB041", .Text(4)},'"TB041"
					{"TB042", .Text(8)},'"TB042"
					{"TB048", .Text(5)},'"TB048"
					{"TB051", amt},
					{"TB052", amt},
					{"UDF01", "No"}'generate barcode
				}
				sumAmt += amt
				sumTax += tax
				sqlList.Add(dbConn.getInsertSql("ACRTB", fld, New Hashtable))

			End With
		Next

		'update to main record
		If sumAmt > 0 Or sumTax > 0 Then
			fld = New Hashtable From {
				{"TA029", sumAmt},
				{"TA030", sumTax},
				{"TA041", Math.Round(sumAmt * exchangeRate, 2)},
				{"TA042", Math.Round(sumTax * exchangeRate, 2)}
			}
			whr = New Hashtable From {
				{"TA001", docType},
				{"TA002", docNumber}
			}
			sqlList.Add(dbConn.getUpdateSql("ACRTA", fld, whr))
			BtACRLB_Click(sender, e)
		End If
		Dim rowEffect As Integer = dbConn.TransactionSQL(sqlList, VarIni.ERP, dbConn.WhoCalledMe)

		show_message.ShowMessage(Page, "บันทึกสำเร็จ" & rowEffect & " รายการ \nตรวจสอบกรายการที่บันทึกหน้า Sale Invoice (เลขที่ " & UcDdlSaleInvoice.Text & " )", UpdatePanel1)
		BtReset_Click(sender, e)
	End Sub

	Protected Sub BtACRLB_Click(sender As Object, e As EventArgs) Handles BtACRLB.Click
		Dim dbconn As New DataConnectControl
		'Dim sqlList As New List(Of String)
		Dim sqlList As New ArrayList
		Dim SQL As String = "select ACRTA.* from ACRTA left join ACRLB on LB002=TA001 and LB003=TA002 where TA004 like 'D%' and TA001='HT' and TA025 = 'Y' and TA029+TA030>0 and TA041+TA042>0 and LB001 is null order by TA003 "
		Dim getRow As DataTable = dbconn.Query(SQL, VarIni.ERP, dbconn.WhoCalledMe)
		For Each dr As DataRow In getRow.Rows
			With New DataRowControl(dr)
				Dim invoiceType As String = .Text("TA001")
				Dim invoiceNo As String = .Text("TA002")
				Dim orderDate As String = .Text("TA038")
				Dim whr As Hashtable
				whr = New Hashtable From {
					{"LB001", "1"},
					{"LB002", invoiceType},
					{"LB003", invoiceNo},
					{"LB004", orderDate},
					{"LB009", invoiceType},'Affected Order Type
					{"LB010", invoiceNo}'Affected Order No.
				}
				Dim fld As Hashtable
				fld = New Hashtable From {
						{"COMPANY", .Text("COMPANY")},
						{"CREATOR", .Text("CREATOR")},
						{"USR_GROUP", .Text("USR_GROUP")},
						{"CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss")},
						{"FLAG", "1"},
						{"LB005", .Text("TA004")},'customer code
						{"LB006", orderDate},'order date
						{"LB007", .Text("TA009")},'currency
						{"LB008", .Number("TA010")},'exchange rate
						{"LB011", "1"},'debit(1)/credit(-1)
						{"LB012", .Text("TA015")},'invoice number
						{"LB013", .Number("TA029") + .Number("TA030")},'Amount(O/C)
						{"LB014", .Number("TA041") + .Number("TA042")},'Amount(B/C)
						{"LB015", "0"},
						{"LB016", "0"},
						{"LB017", "0"},
						{"LB018", "0"},
						{"LB019", "0"},
						{"LB020", .Text("TA003")},'approve date
						{"LB021", "1"},'write off status
						{"LB022", "N"},
						{"LB023", "N"},
						{"LB024", .Text("TA022")},'remark
						{"LB025", .Text("TA005")},'sale code
						{"LB026", .Text("TA070")}'dept code
					}
				sqlList.Add(dbconn.getInsertSql("ACRLB", fld, whr))
			End With
		Next

		Dim aa As ArrayList = sqlList
		dbconn.TransactionSQL(sqlList, VarIni.ERP, dbconn.WhoCalledMe)
	End Sub

End Class