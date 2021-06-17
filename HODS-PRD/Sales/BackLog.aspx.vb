Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class BackLog
	Inherits System.Web.UI.Page
	Dim dbcon As New DataConnectControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then

		End If
	End Sub

	Protected Sub BtShow_Click(sender As Object, e As EventArgs) Handles BtShow.Click
		Dim al As New ArrayList
		Dim fldName As ArrayList
		Dim colName As ArrayList
		Dim SQLShipTime As String = ""
		SQLShipTime = "(STUFF((SELECT ' ,' + TIME_SHIP +'('+cast(cast(MF008 as integer) as varchar)+')'  FROM (select ME002,COPME.UDF01 PLANT,COPME.UDF02 TIME_SHIP,MF006,MF003,MF008 from COPMF left join COPME on ME001=MF001  where ME008 = 'N') COPME WHERE ME002 = CUST AND PLANT=PLANT 	AND MF006 = SHIP_DATE AND MF003=COPMF.ITEM order by TIME_SHIP FOR XML PATH('') ), 1, 2, '') )"
		With New ArrayListControl(al)
			.TAL("CUST", "Cust Code")
			.TAL("MA002", "Cust Name")
			.TAL("PLANT", "Plant")
			.TAL("SHIP_DATE", "Ship/Invoice Date")
			.TAL("COPMF.ITEM" & VarIni.C8 & "ITEM", "Item")
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
			.TAL("PR_QTY", "PR Qty", "0")
			.TAL("PO_QTY", "PO Qty", "0")
			.TAL("QTY-isnull(DEL_QTY,0)" & VarIni.C8 & "BAL_CALLIN_QTY", "Balance Callin Qty(del)", "0")
			.TAL("QTY-isnull(DEL_QTY,0)-isnull(STOCK_QTY,0)" & VarIni.C8 & "BACK_LOG_QTY", "Back Log Qty(del)", "0")
			'.TAL("QTY-INVOICE_QTY" & VarIni.C8 & "BAL_CALLIN_QTY1", "Balance Callin Qty(invoice)", "0")
			'.TAL("QTY-INVOICE_QTY-STOCK_QTY" & VarIni.C8 & "BACK_LOG_QTY1", "Back Log Qty(invoice)", "0")
			.TAL(SQLShipTime & VarIni.C8 & "SHIP_TIME", "Ship Time")
			fldName = .ChangeFormat()
			colName = .ChangeFormat(True)
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
		Dim SQLCallin As String = " (
				select CUST,PLANT,SHIP_DATE,ITEM,CUST_WO,CUST_LINE,CUST_MODEL,sum(isnull(QTY,0)) QTY,sum(isnull(DEL_QTY,0)) DEL_QTY from (
				select ME002 CUST,COPME.UDF01 PLANT,MF006 SHIP_DATE,MF003 ITEM,COPMF.UDF01 CUST_WO,COPMF.UDF02 CUST_LINE,COPMF.UDF03 CUST_MODEL,sum(MF008) QTY,0 DEL_QTY from COPMF left join COPME on ME001=MF001 where ME008 = 'N' group by ME002,MF003,MF006,COPME.UDF01,COPMF.UDF01,COPMF.UDF02,COPMF.UDF03
				union (select COPTG.TG004,COPTG.UDF02,COPTG.TG003,COPTH.TH004,COPTH.UDF01,COPTH.UDF02,COPTH.UDF03,0,sum(TH008)  from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y')  group by COPTG.TG004,COPTG.UDF02,TG003,COPTH.TH004,COPTH.UDF01,COPTH.UDF02,COPTH.UDF03)
				) call_in
				group by CUST,PLANT,SHIP_DATE,ITEM ,CUST_WO,CUST_LINE,CUST_MODEL
			) COPMF "

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


		'Dim sqlSD As String = " (select COPTH.UDF04 CALLIN_NO,COPTH.UDF05 CALLIN_SEQ,sum(TH008) TH008 from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 in ('N','Y') and COPTH.UDF04<>'' and COPTH.UDF05<>'' group by COPTH.UDF04,COPTH.UDF05) COPTH"
		Dim sqlWipPack As String = "(select TA006,sum(WIP_QTY) WIP_PACK_QTY from (
				select MOCTA.TA006,SFCTA.TA001,SFCTA.TA002, SFCTA.TA003,
				SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 WIP_QTY,
				ROW_NUMBER() OVER (PARTITION BY SFCTA.TA001,SFCTA.TA002 ORDER BY SFCTA.TA003 desc) row_num 
				from SFCTA left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002
				where MOCTA.TA011 not in  ('Y','y') 
				) WIP_PACK where row_num=1 and WIP_QTY>0 group by TA006) WIP "


		Dim strSQL As New SQLString(SQLCallin, fldName)
		strSQL.setLeftjoin(" left join " & SQLINVOICE & " on ACRTB.ICUST=COPMF.CUST and ACRTB.IPLANT=COPMF.PLANT and ACRTB.ISHIP_DATE=COPMF.SHIP_DATE and ACRTB.IITEM=COPMF.ITEM ")
		strSQL.setLeftjoin(" left join " & SQLStock & " on INVMC.ITEM=COPMF.ITEM ")
		strSQL.setLeftjoin(" left join " & SQLWIP & " on MOCTA.ITEM=COPMF.ITEM ")
		strSQL.setLeftjoin(" left join " & SQLPR & " on PURTB.ITEM=COPMF.ITEM ")
		strSQL.setLeftjoin(" left join " & SQLPO & " on PURTD.ITEM=COPMF.ITEM ")
		'strSQL.setLeftjoin(" left join " & sqlSD & " on COPTH.CALLIN_NO=COPMF.MF001 and COPTH.CALLIN_SEQ=COPMF.MF002 ")
		strSQL.setLeftjoin(" left join " & sqlWipPack & " on WIP.TA006=COPMF.ITEM ")

		strSQL.setLeftjoin(" left join INVMB on MB001=COPMF.ITEM ")
		strSQL.setLeftjoin(" left join COPMA on MA001=CUST ")

		Dim whr As String = ""
		whr &= strSQL.WHERE_DATE("SHIP_DATE", UcDateFrom.Text, UcDateTo.Text, "0",, True)
		whr &= strSQL.WHERE_LIKE("CUST", TbCustCode)
		whr &= strSQL.WHERE_LIKE("MA002", TbCustName)
		whr &= strSQL.WHERE_LIKE("COPMF.ITEM", TbItem)
		whr &= strSQL.WHERE_LIKE("MB003", TbSpec)
		whr &= strSQL.WHERE_LIKE("PLANT", TbPlant)
		If CbBackLog.Checked Then
			whr &= strSQL.WHERE_EQUAL("QTY-isnull(DEL_QTY,0)-isnull(STOCK_QTY,0)", "0", ">", False)
		End If
		If CbCallIn.Checked Then
			whr &= strSQL.WHERE_EQUAL("QTY", "0", ">", False)
		End If
		strSQL.SetWhere(whr, True)
		UcGv.ShowGridviewHyperLink(strSQL.GetSQLString, VarIni.ERP, colName)

	End Sub

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
					Dim dr As New DataRowControl(SQLCallin, VarIni.ERP, dbcon.WhoCalledMe)
					With e.Row.Cells(9)
						.Text = dr.Number("QTY").ToString("#,###")
						.VerticalAlign = TextAlign.Right
					End With

				End With
			End If
		End With

	End Sub
End Class