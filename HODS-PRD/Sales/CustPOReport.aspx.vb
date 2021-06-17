Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl


Public Class CustPOReport
	Inherits System.Web.UI.Page
	Dim dbconn As New DataConnectControl
	Dim contform As New ControlDataForm

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			Dim SQL As String = "select Code,Name from HOOTHAI_REPORT..CodeInfo where CodeType='PO_STATUS' order by Code"
			UcDdlStatus.show(SQL, VarIni.DBMIS, "Name", "Code", True)
			BtExport.Visible = False
		End If
	End Sub


	Function sqlPO(fldName As ArrayList, whr As String) As String
		'Dim sqlStr As New SQLString("HOOTHAI_REPORT..CustPODetail C", fldName) 'CustPODetail
		Dim sqlStr As New SQLString("HOOTHAI_REPORT..V_CUST_PO_BALANCE V", fldName) 'CustPODetail



		'sqlStr.setLeftjoin(" left join   HOOTHAI_REPORT..CustPOInfo on CustPO=CustPOD ")
		'Dim SQL As String = ""
		'SQL = "select ACRTA.TA004,ACRTB.TB039,ACRTB.TB048, ACRTA.UDF06,
		'		sum(ACRTB.TB022) TB022,sum(case ACRTA.TA025 when 'Y' then ACRTB.TB022 else 0 end) TB022Y,sum(case ACRTA.TA025 when 'N' then ACRTB.TB022 else 0 end) TB022N
		'		from ACRTB left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002
		'		where ACRTA.TA025 in ('N','Y') and ACRTB.TB048 is not null and rtrim(ACRTB.TB048)<>'' 
		'		group by ACRTB.TB048, ACRTB.TB039,ACRTA.TA004,ACRTA.UDF06"


		'sqlStr.setLeftjoin(" left join (" & SQL & ") A  on A.TA004=Cust and A.TB039=Item and A.TB048=CustPOD and A.UDF06=Plant ")
		With sqlStr
			'.setLeftjoin("HOOTHAI_REPORT..CustPOInfo I", New List(Of String) From {"I.CustPO" & VarIni.C8 & "C.CustPOD"})
			'.setLeftjoin("HOOTHAI_REPORT..V_CUST_PO_BALANCE V",
			'			 New List(Of String) From {
			'				"V.CustPOD" & VarIni.C8 & "C.CustPOD",
			'				"V.Item" & VarIni.C8 & "C.Item",
			'				"V.Plant" & VarIni.C8 & "I.Plant"
			'			 })
			.setLeftjoin("INVMB", New List(Of String) From {"MB001" & VarIni.C8 & "V.Item"})
			.setLeftjoin("COPMA", New List(Of String) From {"COPMA.MA001" & VarIni.C8 & "V.Cust"})
		End With

		'sqlStr.setLeftjoin(" left join INVMB on MB001=Item ")
		'sqlStr.setLeftjoin(" left join COPMA on COPMA.MA001=Cust ")
		sqlStr.SetWhere(whr, True)
		sqlStr.SetOrderBy("V.Cust,V.Plant,V.CustPOD,V.Item")
		Return sqlStr.GetSQLString
	End Function


	Protected Sub BtShow_Click(sender As Object, e As EventArgs) Handles BtShow.Click
		Dim al As New ArrayList
		Dim colName As ArrayList
		Dim fldName As ArrayList
		With New ArrayListControl(al)
			.TAL("V.Cust" & VarIni.C8 & "CUST_CODE", "Cust code")
			.TAL("COPMA.MA002" & VarIni.C8 & "MA002", "Cust Name")
			.TAL("V.Plant" & VarIni.C8 & "PLANT", "Plant")
			.TAL("V.CustPOD" & VarIni.C8 & "CUST_PO", "Cust PO No.")
			.TAL("V.Item" & VarIni.C8 & "ITEM", "Item")
			.TAL("MB002", "Desc")
			.TAL("MB003", "Spec")
			.TAL("V.Qty" & VarIni.C8 & "QTY", "Qty", "0")
			.TAL("V.QtyVoid" & VarIni.C8 & "QTY_VOID", "Qty Void", "0")
			.TAL("V.TB022" & VarIni.C8 & "INVONCE_QTY", "Invoice Qty", "0")
			.TAL("V.PO_BAL_USAGE" & VarIni.C8 & "PO_BAL", "PO Balance", "0")
			colName = .ChangeFormat(True)
			fldName = .ChangeFormat()
		End With

		Dim whr As String = ""
		whr &= dbconn.WHERE_LIKE("V.Cust", TbCustCode)
		whr &= dbconn.WHERE_LIKE("V.Plant", TbPlant)
		whr &= dbconn.WHERE_LIKE("V.Item", TbItem)
		whr &= dbconn.WHERE_LIKE("MB003", TbSpec)
		whr &= dbconn.WHERE_LIKE("V.CustPOD", TbPO)


		If CbBalPO.Checked Or CbBalPONegative.Checked Then
			Dim symbol As String = ""
			If CbBalPO.Checked And CbBalPONegative.Checked Then
				symbol = "<>"
			Else
				symbol = "<"
				If CbBalPO.Checked Then
					symbol = ">"
				End If
			End If
			whr &= dbconn.WHERE_EQUAL("V.PO_BAL_USAGE", "0", symbol, False)
		End If

		UcGv.ShowGridviewHyperLink(sqlPO(fldName, whr), VarIni.ERP, colName, True)

		BtExport.Visible = If(UcGv.RowValue > 0, True, False)
	End Sub

	Private Sub UcGv_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles UcGv.RowDataBound
		With e.Row
			If .RowType = DataControlRowType.DataRow Then
				Dim hplDetail As HyperLink = CType(.FindControl("hplDetail"), HyperLink)
				If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("CUST_PO")) And Not IsDBNull(.DataItem("ITEM")) Then
					Dim po As String = .DataItem("CUST_PO").ToString.Trim
					Dim item As String = .DataItem("ITEM").ToString.Trim
					hplDetail.NavigateUrl = "../Sales/CustPOReportPopup.aspx?height=150&width=350&custpo=" & po & "&item=" & item
					hplDetail.Attributes.Add("title", "Cust PO " & po & " Item " & item)
					.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
					.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
				End If
			End If
		End With
	End Sub

	Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
		'Save To Excel File
	End Sub

	Protected Sub BtExport_Click(sender As Object, e As EventArgs) Handles BtExport.Click
		Dim expcont As New ExportImportControl
		'expcont.Export("CUST PO BALANCE", UcGv.getGridview)
		contform.ExportGridViewToExcel("CUST PO ", UcGv.getGridview)
	End Sub
End Class