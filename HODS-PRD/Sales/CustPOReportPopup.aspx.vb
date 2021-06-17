Imports MIS_HTI.DataControl

Public Class CustPOReportPopup
	Inherits System.Web.UI.Page
	Dim dbconn As New DataConnectControl
	Dim contform As New ControlDataForm
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			'show data

			Dim POREPORT As New CustPOReport
			Dim al As New ArrayList
			'Dim colName As ArrayList
			Dim fldName As ArrayList
			With New ArrayListControl(al)
				.TAL("Cust+' '+COPMA.MA002" & VarIni.C8 & "MA002", "Cust Name")
				.TAL("Plant", "Plant")
				'.TAL("CustPOD", "Cust PO No.")
				'.TAL("Item", "Item")
				.TAL("MB003", "Spec")
				.TAL("Qty", "Qty", "0")
				.TAL("QtyVoid", "Qty Void", "0")
				.TAL("isnull(TB022,0)" & VarIni.C8 & "TB022", "Invoice Qty", "0")
				.TAL("Qty-QtyVoid-isnull(TB022,0)" & VarIni.C8 & "PO_BAL", "PO Balance", "0")
				'colName = .ChangeFormat(True)
				fldName = .ChangeFormat()
			End With
			LbPo.Text = Request.QueryString("custpo").ToString.Trim
			LbItem.Text = Request.QueryString("item").ToString.Trim
			Dim whr As String = ""
			whr &= dbconn.WHERE_EQUAL("CustPOD", LbPo.Text)
			whr &= dbconn.WHERE_EQUAL("Item", LbItem.Text)
			With New DataRowControl(POREPORT.sqlPO(fldName, whr), VarIni.ERP)
				LbCust.Text = .Text("MA002")
				LbSpec.Text = .Text("MB003")
				LbPlant.Text = .Text("Plant")
				LbQty.Text = .Number("Qty").ToString("#,##0")
				LbQtyVoid.Text = .Number("QtyVoid").ToString("#,##0")
				LbQtyInvoice.Text = .Number("TB022").ToString("#,##0")
				LbQtyBal.Text = .Number("PO_BAL").ToString("#,##0")
			End With
			BtShow_Click(sender, e)
		End If
	End Sub

	Protected Sub BtShow_Click(sender As Object, e As EventArgs) Handles BtShow.Click

		Dim al As New ArrayList
		Dim colName As ArrayList
		Dim fldName As ArrayList
		With New ArrayListControl(al)
			'invoice date
			.TAL("TA038", "Invoice Date")
			'customer
			.TAL("TA004", "Cust Code")
			'plant
			.TAL("ACRTA.UDF06" & VarIni.C8 & "UDF06", "Plant")
			'time sent
			.TAL("ACRTA.UDF08" & VarIni.C8 & "UDF08", "Time Ship")
			'remark
			.TAL("TA022", "Invoice Remark")
			'sale invoince no-seq
			.TAL("TB001", "Invoice Type")
			.TAL("TB002", "Invoice No")
			.TAL("TB003", "Invoice Seq")
			'sale delivery no-seq
			.TAL("TB005", "Delivery Type")
			.TAL("TB006", "Delivery No")
			.TAL("TB007", "Delivery Seq")
			'item
			.TAL("TB039", "Item")
			'desc
			.TAL("TB040", "Desc")
			'spec
			.TAL("TB041", "Spec")
			'price
			.TAL("TB023", "Price", "3")
			'qty
			.TAL("TB022", "Qty", "0")
			'po
			.TAL("TB048", "Cust PO")
			'status
			.TAL("TA025", "Approve Status")
			colName = .ChangeFormat(True)
			fldName = .ChangeFormat()
		End With
		Dim strSQL As New SQLString("ACRTB", fldName)
		strSQL.setLeftjoin(" left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002 ")
		strSQL.setLeftjoin(" left join COPMA on COPMA.MA001=TA004 ")
		Dim whr As String = " AND ACRTA.TA025 in ('N','Y')"
		whr &= dbconn.WHERE_EQUAL("TB048", LbPo.Text)
		whr &= dbconn.WHERE_EQUAL("TB039", LbItem.Text)
		strSQL.SetWhere(whr, True)
		strSQL.SetOrderBy("TB001,TB002,TB003")
		UcGv.ShowGridviewHyperLink(strSQL.GetSQLString, VarIni.ERP, colName, False)
		BtExport.Visible = If(UcGv.RowValue > 0, True, False)
	End Sub

	Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
		'Save To Excel File
	End Sub

	Protected Sub BtExport_Click(sender As Object, e As EventArgs) Handles BtExport.Click
		Dim expcont As New ExportImportControl
		'expcont.Export("CUST PO INVOICE", UcGv.getGridview)
		contform.ExportGridViewToExcel("CUST PO INVOICE", UcGv.getGridview)
	End Sub
End Class