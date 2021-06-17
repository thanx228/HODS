Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class BackLogNew
	Inherits System.Web.UI.Page
	Dim dbcon As New DataConnectControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then

		End If
	End Sub

	Protected Sub BtShow_Click(sender As Object, e As EventArgs) Handles BtShow.Click
		Dim al As New ArrayList
		Dim fldName As ArrayList
		Dim colName As List(Of String)
		Dim SQLShipTime As String = "(STUFF((SELECT ' ,' + COPME.TIME_SHIP +'('+cast(cast(COPME.MF008 as integer) as varchar)+')'  FROM (select ME002,COPME.UDF01 PLANT,COPME.UDF02 TIME_SHIP,MF006,MF003,MF008 from COPMF left join COPME on ME001=MF001  where ME008 = 'N') COPME WHERE COPME.ME002 = CIB.CUST AND COPME.PLANT=CIB.PLANT AND COPME.MF006 = CIB.SHIP_DATE AND MF003=CIB.ITEM order by COPME.TIME_SHIP FOR XML PATH('') ), 1, 2, '') )"


		Dim whr As String = ""
		'whr &= dbcon.WHERE_EQUAL("VC.CUST", .Text(0))
		'whr &= dbcon.WHERE_EQUAL("VC.PLANT", .Text(2))
		'whr &= dbcon.WHERE_EQUAL("VC.SHIP_DATE", .Text(3), "<")
		'whr &= dbcon.WHERE_EQUAL("VC.ITEM", .Text(4))
		'Dim sqlBalBefore As String = "( select * from " & VarIni.DBMIS & "..V_CALL_IN_BALANCE VC where )"

		With New ArrayListControl(al)
			.TAL("CUST", "Cust Code")
			.TAL("MA002", "Cust Name")
			.TAL("PLANT", "Plant")
			.TAL("SHIP_DATE", "Ship/Invoice Date")
			.TAL("CIB.ITEM" & VarIni.C8 & "ITEM", "Item")
			.TAL("MB003", "Spec")
			.TAL("CUST_WO", "W/O")
			.TAL("CUST_LINE", "LINE")
			.TAL("CUST_MODEL", "MODEL")
			.TAL("0" & VarIni.C8 & "BQTY", "Before Qty", "0")
			.TAL("QTY", "Call In Qty", "0")
			.TAL("DEL_QTY", "Delivery Qty", "0")
			.TAL("INVOICE_QTY", "Invoice Qty", "0")
			.TAL("STOCK_QTY", "Stock Qty", "0")
			.TAL("WIP_QTY-isnull(WIP_PACK_QTY,0)" & VarIni.C8 & "WIP_QTY", "WIP Qty", "0")
			.TAL("WIP_PACK_QTY", "Pending Stock", "0")
			.TAL("CIB.CALL_IN_BAL_QTY" & VarIni.C8 & "BAL_CALLIN_QTY", "Balance Callin Qty", "0")
			.TAL("CIB.CALL_IN_BAL_QTY-isnull(STOCK_QTY,0)" & VarIni.C8 & "BACK_LOG_QTY", "Back Log Qty", "0")
			.TAL("LRP_PRD_QTY", "LRP Prod Qty", 0)
			.TAL("PO_QTY", "PO Qty", "0")
			.TAL("PR_QTY", "PR Qty", "0")
			.TAL("SO_QTY", "S/O Undel Qty", "0")
			'.TAL("QTY-INVOICE_QTY" & VarIni.C8 & "BAL_CALLIN_QTY1", "Balance Callin Qty(invoice)", "0")
			'.TAL("QTY-INVOICE_QTY-STOCK_QTY" & VarIni.C8 & "BACK_LOG_QTY1", "Back Log Qty(invoice)", "0")
			.TAL(SQLShipTime & VarIni.C8 & "SHIP_TIME", "Ship Time")
			fldName = .ChangeFormat()
			colName = .ChangeFormatList(True)
		End With
		'store SQL
		'Dim sqlSD As String = " (select COPTG.TG004,COPTG.UDF02,COPTG.TG003,COPTH.TH004,sum(TH008) TH008 from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y') and COPTH.UDF04<>'' and COPTH.UDF05<>'' group by COPTG.TG004,COPTG.UDF02,TG003,COPTH.TH004) COPTH"

		'Dim SQLCallin As String = " (
		'		select CUST,PLANT,SHIP_DATE,ITEM,sum(isnull(QTY,0)) QTY,sum(isnull(DEL_QTY,0)) DEL_QTY,sum(isnull(INVOICE_QTY,0)) INVOICE_QTY from (
		'		select ME002 CUST,COPME.UDF01 PLANT,MF006 SHIP_DATE,MF003 ITEM,sum(MF008) QTY,0 DEL_QTY,0 INVOICE_QTY from COPMF left join COPME on ME001=MF001 where ME008 = 'N' group by ME002,MF003,MF006,COPME.UDF01
		'		union (select COPTG.TG004,COPTG.UDF02,COPTG.TG003,COPTH.TH004,0,sum(TH008),0  from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y') and COPTH.UDF04<>'' and COPTH.UDF05<>'' group by COPTG.TG004,COPTG.UDF02,TG003,COPTH.TH004)
		'		union 
		'		(SELECT TA004,ACRTA.UDF06,TA038,TB039,0,0,sum(TB022) INVOICE_QTY FROM ACRTB 
		'					left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002  
		'					left join COPMA on COPMA.MA001=TA004  
		'					WHERE ACRTA.TA025 in ('N','Y') and TA038>='20201001' and TB001='HT'
		'					GROUP BY TA038,TA004,ACRTA.UDF06,TB039
		'				)
		'		) call_in
		'		group by CUST,PLANT,SHIP_DATE,ITEM 
		'	) COPMF "

		'Dim SQLCallin As String = " (
		'		select CUST,PLANT,SHIP_DATE,ITEM,CUST_WO,CUST_LINE,CUST_MODEL,sum(isnull(QTY,0)) QTY,sum(isnull(DEL_QTY,0)) DEL_QTY from (
		'		select ME002 CUST,COPME.UDF01 PLANT,MF006 SHIP_DATE,MF003 ITEM,COPMF.UDF01 CUST_WO,COPMF.UDF02 CUST_LINE,COPMF.UDF03 CUST_MODEL,sum(MF008) QTY,0 DEL_QTY from COPMF left join COPME on ME001=MF001 where ME008 = 'N' group by ME002,MF003,MF006,COPME.UDF01,COPMF.UDF01,COPMF.UDF02,COPMF.UDF03
		'		union (select COPTG.TG004,COPTG.UDF02,COPTG.TG003,COPTH.TH004,COPTH.UDF01,COPTH.UDF02,COPTH.UDF03,0,sum(TH008)  from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y')  group by COPTG.TG004,COPTG.UDF02,TG003,COPTH.TH004,COPTH.UDF01,COPTH.UDF02,COPTH.UDF03)
		'		) call_in
		'		group by CUST,PLANT,SHIP_DATE,ITEM ,CUST_WO,CUST_LINE,CUST_MODEL
		'	) COPMF "

		Dim SQLStock As String = " (select MC001 ITEM,sum(MC007) STOCK_QTY  from INVMC where MC002='2101' group by MC001 having sum(MC007)>0) INVMC "
		Dim SQLWIP As String = " (select TA006 ITEM, SUM(isnull(TA015,0)-isnull(TA017,0)-isnull(TA018,0)) WIP_QTY from MOCTA where TA011 not in('y','Y') and TA013 ='Y' group by TA006  having SUM(isnull(TA015,0)-isnull(TA017,0)-isnull(TA018,0))>0 ) MOCTA "
		Dim SQLPR As String = " (select TB004 ITEM,sum(TR006) PR_QTY from PURTB left join PURTR on TR001=TB001 and TR002=TB002 and TR003=TB003 where TR019='' and TB039='N'   group by TB004 having sum(TR006)>0) PURTB "
		Dim SQLPO As String = " (select TD004 ITEM,SUM(isnull(TD008,0)-isnull(TD015,0)) PO_QTY from  PURTD  where TD016='N' group by TD004 having SUM(isnull(TD008,0)-isnull(TD015,0))>0) PURTD "
		Dim SQLINVOICE As String = "(SELECT TA004 ICUST,ACRTA.UDF06 IPLANT,TA038 ISHIP_DATE,TB039 IITEM,sum(TB022) INVOICE_QTY FROM ACRTB 
							left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002  
							left join COPMA on COPMA.MA001=TA004  
							WHERE ACRTA.TA025 in ('N','Y') and TA038>='20201001' and TB001='HT'
							GROUP BY TA038,TA004,ACRTA.UDF06,TB039
						) ACRTB "
		'		Dim SQLMO As String = "(select TA006 ITEM,sum(MOCTA.TA015-MOCTA.TA017-MOCTA.TA018)MO_BAL from MOCTA where MOCTA.TA013 = 'Y' and MOCTA.TA011 not in ('Y','y') group by TA006 having sum(MOCTA.TA015-MOCTA.TA017-MOCTA.TA018)>0) MOCTA "
		Dim SQLSO As String = "(select COPTD.TD004 ITEM,sum(COPTD.TD008-COPTD.TD009) SO_QTY from COPTD left join COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 where COPTC.TC027 = 'Y' and COPTD.TD016 = 'N' group by COPTD.TD004 having sum(COPTD.TD008-COPTD.TD009)>0) COPTD "
		Dim SQLLRP As String = "(select LRPTA.TA002 ITEM,sum(LRPTA.TA006)LRP_PRD_QTY from LRPTA left join LRPLA on LRPLA.LA001=LRPTA.TA001 where LRPLA.LA013 = '1'  AND LRPTA.TA051 = 'N'group by LRPTA.TA002 having sum(LRPTA.TA006)>0) LRPTA "

		'Dim sqlSD As String = " (select COPTH.UDF04 CALLIN_NO,COPTH.UDF05 CALLIN_SEQ,sum(TH008) TH008 from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y') and COPTH.UDF04<>'' and COPTH.UDF05<>'' group by COPTH.UDF04,COPTH.UDF05) COPTH"
		Dim sqlWipPack As String = "(select TA006,sum(WIP_QTY) WIP_PACK_QTY from (
				select MOCTA.TA006,SFCTA.TA001,SFCTA.TA002, SFCTA.TA003,
				SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 WIP_QTY,
				ROW_NUMBER() OVER (PARTITION BY SFCTA.TA001,SFCTA.TA002 ORDER BY SFCTA.TA003 desc) row_num 
				from SFCTA left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002
				where MOCTA.TA011 not in  ('Y','y') 
				) WIP_PACK where row_num=1 and WIP_QTY>0 group by TA006) WIP "


		Dim strSQL As New SQLString(VarIni.DBMIS & "..V_CALL_IN_BALANCE CIB", fldName)
		With strSQL
			.setLeftjoin(SQLINVOICE, New List(Of String) From {
						 "ACRTB.ICUST" & VarIni.C8 & "CIB.CUST",
						 "ACRTB.IPLANT" & VarIni.C8 & "CIB.PLANT",
						 "ACRTB.ISHIP_DATE" & VarIni.C8 & "CIB.SHIP_DATE",
						 "ACRTB.IITEM" & VarIni.C8 & "CIB.ITEM"})
			.setLeftjoin(SQLStock, New List(Of String) From {"INVMC.ITEM" & VarIni.C8 & "CIB.ITEM"})
			.setLeftjoin(SQLWIP, New List(Of String) From {"MOCTA.ITEM" & VarIni.C8 & "CIB.ITEM"})
			.setLeftjoin(SQLPR, New List(Of String) From {"PURTB.ITEM" & VarIni.C8 & "CIB.ITEM"})
			.setLeftjoin(SQLPO, New List(Of String) From {"PURTD.ITEM" & VarIni.C8 & "CIB.ITEM "})
			.setLeftjoin(sqlWipPack, New List(Of String) From {"WIP.TA006" & VarIni.C8 & "CIB.ITEM"})

			.setLeftjoin(SQLSO, New List(Of String) From {"COPTD.ITEM" & VarIni.C8 & "CIB.ITEM"})
			.setLeftjoin(SQLLRP, New List(Of String) From {"LRPTA.ITEM" & VarIni.C8 & "CIB.ITEM"})


			.setLeftjoin("INVMB", New List(Of String) From {"INVMB.MB001" & VarIni.C8 & "CIB.ITEM"})
			.setLeftjoin("COPMA", New List(Of String) From {"COPMA.MA001" & VarIni.C8 & "CIB.CUST"})

			whr = ""
			whr &= .WHERE_DATE("CIB.SHIP_DATE", UcDateFrom.Text, UcDateTo.Text, "0",, True)
			whr &= .WHERE_LIKE("CIB.CUST", TbCustCode)
			whr &= .WHERE_LIKE("CIB.MA002", TbCustName)
			whr &= .WHERE_LIKE("CIB.ITEM", TbItem)
			whr &= .WHERE_LIKE("INVMB.MB003", TbSpec)
			whr &= .WHERE_LIKE("CIB.PLANT", TbPlant)
			If CbBackLog.Checked Then
				whr &= .WHERE_EQUAL("CALL_IN_BAL_QTY2-isnull(STOCK_QTY,0)", "0", ">", False)
			End If
			If CbCallIn.Checked Then
				whr &= .WHERE_EQUAL("QTY", "0", ">", False)
			End If
			.SetWhere(whr, True)
		End With
		Dim sqlCheck As String = strSQL.GetSQLString
		'UcGv.ShowGridviewHyperLink(strSQL.GetSQLString, VarIni.ERP, colName)
		UcGv.ShowWithCommand(strSQL.GetSQLString, VarIni.ERP, colName, New List(Of String) From {"Process"})
	End Sub

	Private Sub UcGv_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles UcGv.RowCommand
		If e.CommandName = "OnProcess" Then
			'Determine the RowIndex of the Row whose Button was clicked.
			Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
			'Reference the GridView Row.
			'Dim row As GridViewRow = UcGv.getGridview.Rows(rowIndex)
			With New GridviewRowControl(UcGv.getGridview.Rows(rowIndex))
				LbItem.Text = .Text(5)
				LbSpec.Text = .Text(6)
				showGridDetail()
				mpeShow.Show()
			End With
		End If
	End Sub

	Sub showGridDetail()
		Dim al As New ArrayList
		Dim colList As ArrayList
		Dim colAdd As Integer = 1
		With New ArrayListControl(al)
			.TAL("MO", "MO(Type-No)/SO")
			.TAL("QTY", "Prod/Comp/Scrap/Bal Qty")
			.TAL("TEXT", "Text")
			Dim dr As New DataRowControl(GetSQL(True), VarIni.ERP, dbcon.WhoCalledMe)
			colAdd = dr.Number("rowMax")
			For i As Integer = 1 To colAdd
				Dim txt As String = "P" & i.ToString("00")
				.TAL(txt, txt)
			Next
			colList = .ChangeFormat(True)
		End With
		Dim dtcont As New DataTableControl

		Dim dtShow As DataTable = dtcont.setColDatatable(colList, VarIni.C8)

		Dim dt As DataTable = dbcon.Query(GetSQL(, colAdd), VarIni.ERP, dbcon.WhoCalledMe)

		Dim lastWO As String = ""
		Dim WO As String = ""

		Dim fldList As New ArrayList
		Dim colIndex As Integer = 1
		For Each dr As DataRow In dt.Rows
			With New DataRowControl(dr)
				Dim txtList As New List(Of String)
				WO = .Text("MO")
				If WO <> lastWO Then
					If Not String.IsNullOrEmpty(lastWO) Then
						.AddDatarow(dtShow, New ArrayList, fldList)
						colIndex = 1
					End If
					fldList = New ArrayList From {
						"MO" & VarIni.C8 & FormatShow(New List(Of String) From {
							WO,
							.Text("SO"),
							.Text("PLANT"),
							.Text("CUST_WO")}, False),
						"QTY" & VarIni.C8 & FormatShow(New List(Of String) From {
							.Number("MO_QTY").ToString("N0"),
							.Number("COMP_QTY").ToString("N0"),
							.Number("SCRAP_QTY").ToString("N0"),
							.Number("MO_BAL_QTY").ToString("N0")}, False),
						"TEXT" & VarIni.C8 & FormatShow(New List(Of String) From {
							"W/C",
							"WIP Qty",
							"Pending Qty",
							"Plan Qty"}, False)
					}
				End If

				fldList.Add("P" & colIndex.ToString("00") & VarIni.C8 & FormatShow(New List(Of String) From {
																				  .Text("WC_NAME"),
																				  .Text("WIP_QTY"),
																				  .Number("PENDING_QTY").ToString("N0"),
																				  .Text("PLAN_QTY")
																				   }
																				   ))
				colIndex += 1
				lastWO = WO
			End With
		Next
		If Not String.IsNullOrEmpty(lastWO) Then
			With New DataRowControl(Nothing)
				.AddDatarow(dtShow, New ArrayList, fldList)
			End With
		End If

		With New GridviewControl(GvDetail)
			.ShowGridView(colList, dtShow)
		End With

	End Sub

	Function FormatShow(txtList As List(Of String), Optional dataShow As Boolean = True) As String
		Dim txtReturn As String = "<p _REPLACE_>"
		Dim line As Integer = 1
		Dim outcont As New OutputControl
		Dim showBlue As Boolean = False
		For Each txt As String In txtList
			If line > 1 Then
				txtReturn &= "<br/>"
			End If
			If dataShow Then
				Select Case line
					Case 2, 4
						Dim num As Decimal = outcont.checkNumberic(txt)
						If num > 0 Then
							txtReturn &= "<mark>" & num.ToString("N0") & "</mark>"
							showBlue = True
						Else
							txtReturn &= num.ToString("N0")
						End If
					Case Else
						txtReturn &= txt
				End Select
			Else
				txtReturn &= txt
			End If
			line += 1
		Next
		txtReturn &= "</p>"

		Return txtReturn.Replace("_REPLACE_", If(showBlue, "style='color:blue'", ""))
	End Function



	Private Sub GvDetail_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvDetail.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			With New GridviewRowControl(e.Row)
				For i As Integer = 0 To e.Row.Cells.Count - 1
					Dim val As String = Server.HtmlDecode(.Text(i))
					With e.Row.Cells(i)
						.Text = val
					End With
				Next
			End With
		End If
	End Sub

	Function GetSQL(Optional CountRow As Boolean = False, Optional rowMax As Integer = 1) As String
		Dim al As New ArrayList
		Dim fldList As ArrayList
		With New ArrayListControl(al)
			If CountRow Then
				.TAL("count(*)" & VarIni.C8 & "rowMax", "")
			Else
				.TAL("SFCTA.TA001+'-'+SFCTA.TA002" & VarIni.C8 & "MO", "")
				.TAL("MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028" & VarIni.C8 & "SO", "")
				.TAL("COPTC.UDF01" & VarIni.C8 & "PLANT", "")
				.TAL("COPTD.UDF02" & VarIni.C8 & "CUST_WO", "")
				.TAL("SFCTA.TA007" & VarIni.C8 & "WC_NAME", "")
				.TAL("SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058" & VarIni.C8 & "WIP_QTY", "")
				.TAL("MOCTA.TA015+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058" & VarIni.C8 & "PENDING_QTY", "")
				.TAL("ISNULL(PLAN_SCHEDULE.PLAN_QTY,0)" & VarIni.C8 & "PLAN_QTY", "")
				.TAL("MOCTA.TA015" & VarIni.C8 & "MO_QTY", "")
				.TAL("MOCTA.TA017" & VarIni.C8 & "COMP_QTY", "")
				.TAL("MOCTA.TA018" & VarIni.C8 & "SCRAP_QTY", "")
				.TAL("MOCTA.TA015-MOCTA.TA017-MOCTA.TA018" & VarIni.C8 & "MO_BAL_QTY", "")
				For i As Integer = 1 To rowMax
					.TAL("''" & VarIni.C8 & "P" & i.ToString("00"), "")
				Next
			End If
			fldList = .ChangeFormat()
		End With
		Dim strSQL As New SQLString("SFCTA", fldList,, If(CountRow, 1, 0))
		With strSQL
			.setLeftjoin("MOCTA", New List(Of String) From {
						 "MOCTA.TA001" & VarIni.C8 & "SFCTA.TA001",
						 "MOCTA.TA002" & VarIni.C8 & "SFCTA.TA002"
						 })
			Dim whr As String = ""
			'daily plan
			If Not CountRow Then
				'sale order body
				.setLeftjoin("COPTD", New List(Of String) From {
							 "COPTD.TD001" & VarIni.C8 & "MOCTA.TA026",
							 "COPTD.TD002" & VarIni.C8 & "MOCTA.TA027",
							 "COPTD.TD003" & VarIni.C8 & "MOCTA.TA028"
							 })
				'sale order head
				.setLeftjoin("COPTC", New List(Of String) From {
							 "COPTC.TC001" & VarIni.C8 & "COPTD.TD001",
							 "COPTC.TC002" & VarIni.C8 & "COPTD.TD002"
							 })

				al = New ArrayList
				fldList = New ArrayList
				With New ArrayListControl(al)
					.TAL("MoType", "")
					.TAL("MoNo", "")
					.TAL("MoSeq", "")
					.TAL(strSQL.SUM("PlanQty", VarIni.C8, True, "PLAN_QTY"), "")
					fldList = .ChangeFormat()
				End With
				Dim strSQLPlan As New SQLString(VarIni.DBMIS & "..PlanSchedule", fldList)
				With strSQLPlan
					whr &= .WHERE_EQUAL("PlanStatus", "P")
					whr &= .WHERE_EQUAL("PlanDate", "convert(varchar, getdate(), 112)", ">=", False)
					.SetWhere(whr, True)
					.SetGroupBy("MoType,MoNo,MoSeq")
				End With
				.setLeftjoin(strSQLPlan.GetSubQuery("PLAN_SCHEDULE"), New List(Of String) From {
							 "PLAN_SCHEDULE.MoType" & VarIni.C8 & "SFCTA.TA001",
							 "PLAN_SCHEDULE.MoNo" & VarIni.C8 & "SFCTA.TA002",
							 "PLAN_SCHEDULE.MoSeq" & VarIni.C8 & "SFCTA.TA003"
							 })
			End If

			whr = ""

			whr &= .WHERE_EQUAL("MOCTA.TA013", "Y")
			whr &= .WHERE_IN("MOCTA.TA011", "Y,y", True, True)
			whr &= .WHERE_EQUAL("MOCTA.TA006", LbItem.Text)
			whr &= "and (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 >0 or MOCTA.TA015+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 >0)"
			.SetWhere(whr, True)
			Dim fldSort As String = "SFCTA.TA001,SFCTA.TA002,SFCTA.TA003"
			If CountRow Then
				.SetGroupBy("SFCTA.TA001,SFCTA.TA002")
				fldSort = "rowMax desc"

			End If
			.SetOrderBy(fldSort)
		End With
		Return strSQL.GetSQLString
	End Function


	Protected Sub BtExport_Click(sender As Object, e As EventArgs) Handles BtExport.Click
		'Dim expCont As New ExportImportControl
		'expCont.Export("BackLog" & Session("UserName"), UcGv.getGridview)
		Dim ControlForm As New ControlDataForm
		ControlForm.ExportGridViewToExcel("BackLog" & Session("UserName"), UcGv.getGridview)
	End Sub
	Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
		'Save To Excel File
	End Sub

	Private Sub UcGv_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles UcGv.RowDataBound
		With e.Row
			If .RowType = DataControlRowType.DataRow Then
				With New GridviewRowControl(e.Row)
					Dim whr As String = ""
					whr &= dbcon.WHERE_EQUAL("CUST", .Text(0))
					whr &= dbcon.WHERE_EQUAL("PLANT", .Text(2))
					whr &= dbcon.WHERE_EQUAL("SHIP_DATE", .Text(3), "<")
					whr &= dbcon.WHERE_EQUAL("ITEM", .Text(4))
					Dim SQLCallin As String = " 
						select sum(isnull(QTY,0)-isnull(DEL_QTY,0)) QTY from (
						select ME002 CUST,COPME.UDF01 PLANT,MF006 SHIP_DATE,MF003 ITEM,sum(MF008) QTY,0 DEL_QTY from COPMF left join COPME on ME001=MF001 where ME008 = 'N' group by ME002,MF003,MF006,COPME.UDF01
						union (select COPTG.TG004,COPTG.UDF02,COPTG.TG003,COPTH.TH004,0,sum(TH008)  from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y') and COPTH.UDF04<>'' and COPTH.UDF05<>'' group by COPTG.TG004,COPTG.UDF02,TG003,COPTH.TH004)
						) call_in where 1=1 " & whr

					SQLCallin = "select sum(CALL_IN_BAL_QTY2) QTY from V_CALL_IN_BALANCE where 1=1 " & whr

					Dim dr As New DataRowControl(SQLCallin, VarIni.DBMIS, dbcon.WhoCalledMe)
					With e.Row.Cells(9)
						.Text = dr.Number("QTY").ToString("#,###")
						.VerticalAlign = TextAlign.Right
					End With

				End With
			End If
		End With

	End Sub

	Protected Sub BtRefresh_Click(sender As Object, e As EventArgs) Handles BtRefresh.Click
		showGridDetail()
		mpeShow.Show()
	End Sub
End Class