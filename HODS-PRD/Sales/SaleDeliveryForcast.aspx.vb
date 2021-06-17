Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class SaleDeliveryForcast
	Inherits System.Web.UI.Page
	Dim dbConn As New DataConnectControl
	Dim datecont As New DateControl
	Dim dtCont As New DataTableControl
	'Dim ddlCont As New DropDownListControl
	Dim hashCont As New HashtableControl
	'Dim fileCont As New FileControl
	Dim gvCont As New GridviewControl
	Const table As String = "CodeInfo"
	Const codeHead As String = "PLANT"
	'item
	Dim qtyHash As New Hashtable
	Dim stockHash As New Hashtable
	'item & WO
	Dim qtySoHash As New Hashtable
	Dim soHash As New Hashtable

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			UcDdlCust.setAutoPostback()
			UcDdlPlant.setAutoPostback()
			UcDdlSaleDelType.setAutoPostback()
			BtReset_Click(sender, e)
			'set to hide label
			Dim show As Boolean = False
			LbCust.Visible = show
			LbPlant.Visible = show
			LbDocType.Visible = show
			LbDocNo.Visible = show
			LbShipTime.Visible = show


			Dim dataList As New ArrayList From {
					"MF003,MF001,MF002" & VarIni.char8 & "Item",
					"MF001,MF002" & VarIni.char8 & "Call In"
				}
			UcDdlSort.manualShow(dataList)

		End If
	End Sub

	Private Sub BtReset_Click(sender As Object, e As EventArgs) Handles BtReset.Click
		UcDateShip.Text = DateTime.Now.ToString("yyyyMMdd")
		LbCust.Text = ""
		LbShipTime.Text = ""
		LbQtyAll.Text = "0"
		LbAmtAll.Text = "0"
		LbDocType.Text = ""
		'customer
		Dim SQL As String = "select rtrim(MA001) MA001,rtrim(MA001)+':'+MA002 MA002 from COPMA where MA097 = '1' and substring(COPMA.UDF01,1,1)='Y' order by MA001 "
		UcDdlCust.show(SQL, VarIni.ERP, "MA002", "MA001", True)

		'sale delivery type
		SQL = "select MQ001,MQ001+' '+MQ002 MQ002 from CMSMQ where MQ003 = '23' order by MQ001"
		UcDdlSaleDelType.show(SQL, VarIni.ERP, "MQ002", "MQ001", True)

		UcDdlCust_SelectedIndexChanged(sender, e)
		UcDdlPlant_SelectedIndexChanged(sender, e)

		BtForcast.Visible = False
		BtSelect.Visible = False
		BtSaleDelivery.Visible = False

	End Sub

	Private Sub UcDdlCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UcDdlCust.SelectedIndexChanged
		LbCust.Text = UcDdlCust.Text
		Dim SQL As String = "select distinct rtrim(Code) Code,rtrim(Code)+':'+rtrim(Name) Name from " & table & " where CodeType='" & codeHead & "' " & dbConn.WHERE_LIKE("WC", UcDdlCust.Text) & " order by Code "
		UcDdlPlant.show(SQL, VarIni.DBMIS, "Name", "Code", True)
		UcDdlPlant_SelectedIndexChanged(sender, e)
	End Sub

	Private Sub UcDdlPlant_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UcDdlPlant.SelectedIndexChanged
		Dim sql As String = ""
		Dim dt As DataTable = Nothing

		If Not String.IsNullOrEmpty(UcDdlCust.Text2) And Not String.IsNullOrEmpty(UcDdlPlant.Text2) Then
			sql = "select distinct COPMF.MF001 CALLIN_NO,SHIP_TIME, COPMF.MF001+ case when SHIP_TIME='' then '' else '('+SHIP_TIME+')' end MF001 from (
						select ME002,COPME.UDF01 PLANT,COPME.UDF02 SHIP_TIME, MF001,MF002,MF003,MF004,MF005,MF006,MF008,MF010,COPMF.UDF01 CUST_WO,COPMF.UDF02 CUST_LINE,COPMF.UDF03 MODEL from COPMF left join COPME on ME001=MF001 where ME008='N'
					) COPMF
					left join (
						select COPTH.UDF04 CALLIN_NO,COPTH.UDF05 CALLIN_SEQ,sum(TH008) TH008 from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y') and COPTH.UDF04<>'' and COPTH.UDF05<>'' group by COPTH.UDF04,COPTH.UDF05
					)COPTH on COPTH.CALLIN_NO=COPMF.MF001 and COPTH.CALLIN_SEQ=COPMF.MF002
					where MF008-isnull(TH008,0)>0 and ME002='" & UcDdlCust.Text & "' and PLANT='" & UcDdlPlant.Text & "' and MF006<='" & UcDateShip.textEmptyToday & "'
					order by COPMF.MF001,SHIP_TIME"
			dt = dbConn.Query(sql, VarIni.ERP, dbConn.WhoCalledMe)
		End If
		UcCblCallinNo.show(dt, "MF001", "CALLIN_NO", 1)
		Dim showBut As Boolean = False
		If dt IsNot Nothing Then
			If dt.Rows.Count > 0 Then
				showBut = True
			End If
		End If
		BtForcast.Visible = showBut
		LbPlant.Text = UcDdlPlant.Text2
		gvCont.ShowGridView(GvLeft, New DataTable)
		gvCont.ShowGridView(GvRight, New DataTable)
	End Sub

	Private Sub UcDateShip_ChangeEvent(sender As Object, e As EventArgs) Handles UcDateShip.ChangeEvent
		UcDdlPlant_SelectedIndexChanged(sender, e)
	End Sub

	Private Sub BtForcast_Click(sender As Object, e As EventArgs) Handles BtForcast.Click
		If UcCblCallinNo.getChecked = 0 Then
			'show message
			Exit Sub
		End If
		Dim al As New ArrayList
		Dim fldName As ArrayList
		With New ArrayListControl(al)
			.TAL("MF001+'-'+MF002" & VarIni.C8 & "MF001", "call no") '1
			.TAL("MF005", "Spec") '2
			.TAL("COPMF.CUST_WO" & VarIni.C8 & "CUST_WO", "") '3
			.TAL("COPMF.CUST_LINE" & VarIni.C8 & "CUST_LINE", "") '4
			.TAL("COPMF.MODEL" & VarIni.C8 & "MODEL", "") '5
			.TAL("MF008-isnull(TH008,0)" & VarIni.C8 & "MF008", "call in balance", "0") '6
			.TAL("MC007", "stock qty", "0") '7
			.TAL("SO_BAL", "", "0") '8
			.TAL("WIP_QTY" & VarIni.C8 & "TC036", "wip end process", "0") '9
			.TAL("SHIP_TIME", "ship time") '10
			.TAL("MF010", "Unit") '11
			.TAL("MF003", "item") '12
			.TAL("MF004", " desc") '13
			fldName = .ChangeFormat()
		End With

		Dim sqlCallin As String = " (select ME002,COPME.UDF01 PLANT,COPME.UDF02 SHIP_TIME, MF001,MF002,MF003,MF004,MF005,MF006,MF008,MF010,COPMF.UDF01 CUST_WO,COPMF.UDF02 CUST_LINE,COPMF.UDF03 MODEL from COPMF left join COPME on ME001=MF001 where ME008='N' ) COPMF "

		Dim sqlSO As String = " (select TC004,COPTC.UDF01 PLANT,TD004,sum(TD008-TD009) SO_BAL from COPTD left join COPTC on TC001=TD001 and TC002=TD002 where TC027 = 'Y' and TD016 = 'N' and TD008-TD009>0 group by TC004,TD004,COPTC.UDF01 )COPTD "
		Dim sqlSD As String = " (select COPTH.UDF04 CALLIN_NO,COPTH.UDF05 CALLIN_SEQ,sum(TH008) TH008 from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y') and COPTH.UDF04<>'' and COPTH.UDF05<>'' group by COPTH.UDF04,COPTH.UDF05) COPTH"
		Dim sqlStock As String = " (select MC001,sum(MC007) MC007  from INVMC where MC002 in ('2101') group by MC001 having sum(MC007)>0) INVMC "
		'Dim sqlMoRcvNotApp As String = " (select TC047,sum(TC036) TC036  from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 where TB013 = 'N' group by TC047 having sum(TC036)>0) SFCTC "
		Dim sqlWipPack As String = "(select TA006,sum(WIP_QTY) WIP_QTY from (
				select MOCTA.TA006,SFCTA.TA001,SFCTA.TA002, SFCTA.TA003,
				SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 WIP_QTY,
				ROW_NUMBER() OVER (PARTITION BY SFCTA.TA001,SFCTA.TA002 ORDER BY SFCTA.TA003 desc) row_num
				from SFCTA left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002
				where MOCTA.TA011 not in  ('Y','y')
				) WIP_PACK where row_num=1 and WIP_QTY>0 group by TA006) WIP "
		With New SQLString(sqlCallin, fldName)
			.setLeftjoin(" left join " & sqlSO & " on COPTD.TC004=COPMF.ME002 and COPTD.PLANT=COPMF.PLANT and COPTD.TD004=COPMF.MF003 ")
			.setLeftjoin(" left join " & sqlSD & " on COPTH.CALLIN_NO=COPMF.MF001 and COPTH.CALLIN_SEQ=COPMF.MF002 ")
			.setLeftjoin(" left join " & sqlStock & " on INVMC.MC001=COPMF.MF003 ")
			'strSQL.setLeftjoin(" left join " & sqlMoRcvNotApp & " on SFCTC.TC047=COPMF.MF003 ")
			.setLeftjoin(" left join " & sqlWipPack & " on WIP.TA006=COPMF.MF003 ")

			Dim whr As String = .WHERE_IN("MF001", UcCblCallinNo.getObject)
			whr &= " AND MF008-isnull(TH008,0)>0 "
			.SetWhere(whr, True)
			.SetOrderBy(UcDdlSort.Text) '"MF003,MF001,MF002"
			gvCont.ShowGridView(GvLeft, .GetSQLString, VarIni.ERP)
		End With


		Dim showBut As Boolean = False
		If GvLeft.Rows.Count > 0 Then
			showBut = True
		End If
		BtSelect.Visible = showBut

		gvCont.ShowGridView(GvRight, New DataTable)

	End Sub

	Private Sub GvLeft_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvLeft.RowDataBound
		With e.Row
			If .RowType = DataControlRowType.DataRow Then
				With New GridviewRowControl(e.Row)
					Dim item As String = .Text(12)
					'Dim wo As String = item & .Text(3)
					Dim stock As Decimal = .Number(7)
					Dim soQty As Decimal = .Number(8)
					Dim valUse As Decimal = If(stock <= soQty, stock, soQty)
					If cbStockFirst.Checked And valUse > 0 Then
						'Dim stockFirst As Decimal = If(stock <= 0, soQty, stock)
						Dim valCheck As Decimal = If(valUse > soQty, soQty, valUse)
						hashCont.addDataHash(qtySoHash, item, valCheck)
						hashCont.addDataHash(qtyHash, item, valCheck)
					Else
						Dim callinQty As Decimal = .Number(6)
						hashCont.CountItemDataHash(qtySoHash, item, callinQty)
						hashCont.CountItemDataHash(qtyHash, item, callinQty)
					End If

					'hashCont.addDataHash(stockHash, item, stock)
					'hashCont.addDataHash(soHash, wo, soQty)
					hashCont.addDataHash(stockHash, item, stock)
					hashCont.addDataHash(soHash, item, valUse)

					Dim checkSo As Boolean = hashCont.getDataHashDecimal(soHash, item) < hashCont.getDataHashDecimal(qtySoHash, item)
					Dim checkStock As Boolean = hashCont.getDataHashDecimal(stockHash, item) < hashCont.getDataHashDecimal(qtyHash, item)
					If checkStock Or checkSo Then 'stock<qty  or so<qty
						Dim cbSelect As CheckBox = .FindControl("cbSelect")
						cbSelect.Checked = False
						cbSelect.Enabled = False
						Dim backColor As Drawing.Color = Drawing.Color.Yellow
						Dim fontColor As Drawing.Color = Drawing.Color.DarkBlue
						If checkStock Then
							backColor = Drawing.Color.Red
							fontColor = Drawing.Color.White
						End If
						e.Row.BackColor = backColor
						e.Row.ForeColor = fontColor
						e.Row.BorderColor = Drawing.Color.LightYellow
					End If
				End With
			End If
		End With
	End Sub
	Private Sub UcDdlSaleDelType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UcDdlSaleDelType.SelectedIndexChanged
		Dim showBut As Boolean = False
		If Not String.IsNullOrEmpty(UcDdlSaleDelType.Text2) And GvRight.Rows.Count > 0 Then
			showBut = True
		End If
		BtSaleDelivery.Visible = showBut
		LbDocType.Text = UcDdlSaleDelType.Text2
	End Sub

	Private Sub BtSelect_Click(sender As Object, e As EventArgs) Handles BtSelect.Click
		Dim SQL As String = ""
		Dim al As New ArrayList
		Dim colName As ArrayList
		With New ArrayListControl(al)
			.TAL("TH003", "Seq") '0
			.TAL("CALL_IN", "Call In") '1 callin(no(UDF04) - seq(UDF05))
			.TAL("SO", "Sale Order") '2 so(type(TH014)-no(TH015)-seq(TH016))
			.TAL("TH006", "Spec") '3
			.TAL("TH007", "W/H") '4
			.TAL("TH008", "Qty", "0") '5
			.TAL("TH009", "Unit") '6
			.TAL("TH012", "Price", "4") '7
			.TAL("UDF01", "W/O") '8
			.TAL("UDF02", "Line") '9
			.TAL("UDF03", "Model") '10
			.TAL("SHIP_TIME", "Ship Time") '11
			.TAL("TH004", "Item",, True) '12
			.TAL("TH005", "Desc",, True) '13
			colName = .ChangeFormat(True)
		End With
		Dim dtShow As DataTable = dtCont.setColDatatable(colName, VarIni.C8)
		Dim custCode As String = UcDdlCust.Text2
		Dim plant As String = UcDdlPlant.Text2
		Dim qtyOldHash As New Hashtable
		Dim seq As Integer = 1
		Const C88 As String = VarIni.C8 & VarIni.C8

		Dim sumQty As Decimal = 0
		Dim sumAmount As Decimal = 0
		Dim callinHash As New Hashtable

		For Each gr As GridViewRow In GvLeft.Rows
			With New GridviewRowControl(gr)
				Dim cbSelect As CheckBox = .FindControl("cbSelect")
				If cbSelect.Checked Then
					Dim callin As String = .Text(1)
					Dim custwo As String = .Text(3)
					Dim custline As String = .Text(4)
					Dim custmodel As String = .Text(5)
					Dim callInbalqty As Decimal = .Number(6)
					Dim stockqty As Decimal = .Number(7)
					Dim deliveryQty As Decimal = .Number(8)
					If cbStockFirst.Checked Then
						Dim valUse As Decimal = If(stockqty <= deliveryQty, stockqty, deliveryQty)
						callInbalqty = If(callInbalqty <= valUse, callInbalqty, valUse)
					End If
					Dim shipTime As String = .Text(10)
					Dim unit As String = .Text(11)
					Dim item As String = .Text(12)
					Dim desc As String = .Text(13)
					Dim spec As String = .Text(2)
					Dim price As Decimal = 0
					'Dim qtyCallin As String = .Number(6)

					Dim qtyOldVal As Decimal = hashCont.getDataHashDecimal(qtyOldHash, item)
					hashCont.CountItemDataHash(qtyOldHash, item, callInbalqty)

					'SQL = "select TD001+'-'+TD002+'-'+TD003 SO,TD005,TD006,TD007,TD010,TD011,TD008-TD009 SO_BAL from COPTD left join COPTC on TC001=TD001 and TC002=TD002
					'		where TD016 = 'N' and TC027 = 'Y' and TD008-TD009>0
					'	      and TC004='" & custCode & "'
					'	      and COPTC.UDF01='" & plant & "'
					'	      and TD004='" & item & "'
					'		order by TD013,TC039,TD001,TD002,TD003 "
					With New SQLTEXT("COPTD")
						.SL(New List(Of String) From {
							.Field("TD001+'-'+TD002+'-'+TD003", "SO"),
							.Field("TD005"),
							.Field("TD006"),
							.Field("TD007"),
							.Field("TD010"),
							.Field("TD011"),
							.Field("TD008-TD009", "SO_BAL")
							})
						.LJ("COPTC", New List(Of String) From {
							.LE("TC001", "TD001"),
							.LE("TC002", "TD002")
							})
						.WE("TD016", "N")
						.WE("TC027", "Y")
						.WE("TD008-TD009", "0", ">", False)
						.WE("TC004", custCode)
						.WE("COPTC.UDF01", plant)
						.WE("TD004", item)
						.OB(New List(Of String) From {
							"TD013",
							"TC039",
							"TD001",
							"TD002",
							"TD003"})
						SQL = .TEXT
					End With

					Dim dt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
					For Each dr As DataRow In dt.Rows
						With New DataRowControl(dr)
							desc = .Text("TD005")
							spec = .Text("TD006")
							price = .Number("TD011")

							Dim soQty As Decimal = .Number("SO_BAL")

							If qtyOldVal - soQty >= 0 Then
								qtyOldVal -= soQty
							Else
								If qtyOldVal > 0 And qtyOldVal - soQty < 0 Then
									soQty = Math.Abs(qtyOldVal - soQty)
									qtyOldVal = 0
								End If
								If soQty > 0 And callInbalqty > 0 Then
									Dim cQty As Decimal = callInbalqty
									If soQty < callInbalqty Then
										cQty = soQty
									End If
									callInbalqty -= cQty
									Dim dataFld As New ArrayList From {
										"TH003" & C88 & seq.ToString("0000"),'seq to sale delivery
										"CALL_IN" & C88 & callin,'call in(no(UDF04) - seq(UDF05))
										"SO" & C88 & .Text("SO"),'so(type(TH014)-no(TH015)-seq(TH016))
										"TH006" & C88 & spec,'Spec
										"TH007" & C88 & .Text("TD007"),'W/H
										"TH008" & C88 & cQty,'Qty
										"TH009" & C88 & .Text("TD010"),'unit
										"TH012" & C88 & price,'Price
										"UDF01" & C88 & custwo,'W/O
										"UDF02" & C88 & custline,'Line
										"UDF03" & C88 & custmodel,'Model
										"SHIP_TIME" & C88 & shipTime,'ship time
										"TH004" & C88 & item,'Item
										"TH005" & C88 & desc'Desc
									}

									If String.IsNullOrEmpty(LbShipTime.Text) Then
										LbShipTime.Text = shipTime
									End If

									Dim ci() As String = callin.Split("-")
									hashCont.addDataHash(callinHash, ci(0), ci(0))

									sumQty += cQty
									sumAmount += (Math.Round(cQty * price, 2))
									dtCont.addDataRow(dtShow, dr, dataFld, VarIni.char8)
									seq += 1
								Else
									Exit For
								End If
							End If
						End With
					Next
				End If
			End With
		Next
		If callinHash.Count > 1 Then
			LbShipTime.Text = ""
		End If
		LbQtyAll.Text = sumQty
		LbAmtAll.Text = sumAmount

		gvCont.GridviewInitial(GvRight, colName, strSplit:=VarIni.C8)
		gvCont.ShowGridView(GvRight, dtShow)
		'set show button sale delivery
		UcDdlSaleDelType_SelectedIndexChanged(sender, e)

		'set to hide column
		gvCont.hideColumn(GvRight, al, "displayNone")
	End Sub

	Function getDocNo() As String
		Dim SQL As String,
			dt As DataTable = New DataTable
		Dim dateVal As String = Date.Now.ToString("yyyyMMdd")
		SQL = "select isnull(cast(max(TG002) as Decimal(20,0))+1,'" & dateVal & "001') TG002 from COPTG where TG001='" & Trim(UcDdlSaleDelType.Text) & "' and TG042 ='" & dateVal & "' "
		Dim dr As New DataRowControl(SQL, VarIni.ERP, dbConn.WhoCalledMe)
		Return dr.Text("TG002")
	End Function


	Sub headRecord()
		LbDocNo.Text = getDocNo()

		Dim cust As String = LbCust.Text.Trim
		Dim docDate As String = Date.Now.ToString("yyyyMMdd")
		Dim shipDate As String = UcDateShip.textEmptyToday
		Dim SQL As String = "select MA083,MA014,MA038,MA025,MA026 from COPMA where MA001='" & cust & "' "

		With New DataRowControl(SQL, VarIni.ERP, dbConn.WhoCalledMe)
			Dim whr As Hashtable = New Hashtable From {
				{"TG001", LbDocType.Text},
				{"TG002", LbDocNo.Text.Trim}
			}
			Dim amt As String = LbAmtAll.Text.Trim.Replace(",", "")

			Dim fld As Hashtable = New Hashtable From {
				{"COMPANY", VarIni.ERP},
				{"CREATOR", "H" & Session("UserName")},
				{"USR_GROUP", "HODS"},
				{"CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss")},
				{"FLAG", "3"},
				{"TG003", docDate}, 'Delivery Date
				{"TG004", cust}, 'Customer
				{"TG005", "A01"}, 'dept
				{"TG006", Session("UserName")}, 'sale employee code
				{"TG010", "HT"}, 'Plant
				{"TG012", "1"}, 'exchange rate
				{"TG011", .Text("MA014")}, 'Currency
				{"TG008", .Text("MA025")}, 'addres1
				{"TG009", .Text("MA026")}, 'addres2
				{"TG047", .Text("MA083")}, 'payment term
				{"TG017", .Text("MA038")}, 'Tax Type
				{"TG013", amt}, 'Delivery Amount(O/C)
				{"TG016", "A"}, 'Invoice Kind
				{"TG023", "N"}, 'Approval Indicator
				{"TG024", "N"}, 'Update Indicator
				{"TG031", "N"}, 'Cigarette & Liquor Remark
				{"TG033", LbQtyAll.Text.Replace(",", "")}, 'Total Quantity
				{"TG036", "N"}, 'Journalized(Revenue)
				{"TG037", "N"}, 'Journalized(Cost)
				{"TG042", docDate}, 'Document Date
				{"TG044", "0"}, 'tax rate
				{"TG045", amt}, 'E-Approval Status
				{"TG055", "N"}, 'Journalized(Revenue)
				{"TG059", "N"}, 'Post Status
				{"TG068", "N"}, 'EBCExport Indicator
				{"TG074", "N"}, 'Unlimited release
				{"UDF01", If(docDate = shipDate, "", datecont.ChangeDateFormat(shipDate, "yyyyMMdd", "dd-MM-yyyy"))}, 'ship date
				{"UDF02", LbPlant.Text}, 'Plant
				{"UDF03", LbShipTime.Text} 'ship time
			}

			Dim rowEffect As Integer = dbConn.TransactionSQL(dbConn.GetSQL("COPTG", fld, whr, "II"), VarIni.ERP, dbConn.WhoCalledMe)
			If rowEffect = 0 Then
				headRecord()
			End If

		End With
	End Sub

	Function BodyRecord() As Integer
		Dim whr As Hashtable,
			fld As Hashtable,
			strSQL As String = ""
		Dim sqlList As New ArrayList
		For Each gr As GridViewRow In GvRight.Rows
			With New GridviewRowControl(gr)
				whr = New Hashtable From {
					{"TH001", LbDocType.Text}, 'type
					{"TH002", LbDocNo.Text}, 'no
					{"TH003", .Text(0)} 'seq
				}

				Dim ci() As String = .Text(1).Split("-")
				Dim cNumber As String = ""
				Dim cSeq As String = ""
				If ci.Length <= 2 Then
					cNumber = ci(0)
					cSeq = ci(1)
				End If

				Dim so() As String = .Text(2).Split("-")
				Dim sType As String = ""
				Dim sNumber As String = ""
				Dim sSeq As String = ""
				If so.Length <= 3 Then
					sType = so(0)
					sNumber = so(1)
					sSeq = so(2)
				End If

				Dim qty As Decimal = .Number(5)
				Dim price As Decimal = .Number(7)
				Dim amt As Decimal = Math.Round(qty * price)
				fld = New Hashtable From {
					{"COMPANY", VarIni.ERP},
					{"CREATOR", "H" & Session("UserName")},
					{"USR_GROUP", "HODS"},
					{"CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss")},
					{"FLAG", "3"},
					{"TH014", sType}, 'SO Type
					{"TH015", sNumber}, 'SO number
					{"TH016", sSeq}, 'SO Seq
					{"TH004", .Text(12)}, 'Item
					{"TH005", .Text(13)}, 'Desc
					{"TH006", .Text(3)}, 'Spec
					{"TH007", .Text(4)}, 'WH
					{"TH008", qty}, 'Qty
					{"TH012", price}, 'Price
					{"TH013", amt}, 'amt
					{"TH035", amt}, 'amt
					{"TH037", amt}, 'amt
					{"TH009", .Text(6)}, 'Unit
					{"UDF01", .Text(8)}, 'Cust WO
					{"UDF02", .Text(9)}, 'Cust Line
					{"UDF03", .Text(10)}, 'Model
					{"UDF04", cNumber}, 'call in number
					{"UDF05", cSeq}, 'call in seq
					{"TH017", "********************"}, 'Lot
					{"TH020", "N"}, 'Approval Indicator
					{"TH021", "N"}, 'Update Indicator
					{"TH025", "1"}, 'Discount Rate
					{"TH026", "N"}, 'Code bill
					{"TH031", "1"}, 'Type
					{"TH056", "##########"}, 'Bin
					{"TH064", "N"}, '
					{"THH01", "N"} '
				}
				sqlList.Add(dbConn.getInsertSql("COPTH", fld, whr))
			End With
		Next
		Return dbConn.TransactionSQL(sqlList, VarIni.ERP, dbConn.WhoCalledMe)
	End Function

	Protected Sub BtSaleDelivery_Click(sender As Object, e As EventArgs) Handles BtSaleDelivery.Click

		'check select type of sale delivery
		If String.IsNullOrEmpty(LbDocType.Text) Then

			Exit Sub
		End If

		headRecord()
		If String.IsNullOrEmpty(LbDocNo.Text) Then
			Exit Sub
		End If
		Dim rowEffect As Integer = BodyRecord()
		show_message.ShowMessage(Page, "บันทึกสำเร็จ" & rowEffect & " รายการ \nตรวจสอบกรายการที่บันทึกหน้า Sale Invoice (เลขที่ " & LbDocType.Text & "-" & LbDocNo.Text & " )", UpdatePanel1)
		BtReset_Click(sender, e)
	End Sub
End Class