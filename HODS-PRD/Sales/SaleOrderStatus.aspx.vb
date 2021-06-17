Imports MIS_HTI.DataControl
Public Class SaleOrderStatus
	Inherits System.Web.UI.Page
	Dim ControlForm As New ControlDataForm
	Dim Conn_SQL As New ConnSQL
	Dim configDate As New ConfigDate

	Dim dbconn As New DataConnectControl
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			If Session("UserName") = "" Then
				Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
			End If
			Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
			ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

			btExport.Visible = False
		End If
	End Sub

	Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
		'Save To Excel File
	End Sub

	Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
		ControlForm.ExportGridViewToExcel("SaleOrderStatus" & Session("UserName"), gvShow)
	End Sub

	Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
		Dim SQL As String = "",
			WHR As String = "",
			saleType As String = cblSaleType.Text,
			saleNo As String = tbSaleOrderNo.Text.Trim,
			saleSeq As String = tbSaleOrderSeq.Text.Trim,
			typeCon As String = cblStatus.Text

		WHR = dbconn.WHERE_BETWEEN("TD013", configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))
		WHR &= dbconn.WHERE_IN("TD001", cblSaleType,, True)
		WHR &= dbconn.WHERE_LIKE("TD002", tbSaleOrderNo)
		WHR &= dbconn.WHERE_LIKE("TD003", tbSaleOrderSeq)
		WHR &= dbconn.WHERE_IN("TD016", cblStatus,, True)
		WHR &= dbconn.WHERE_IN("TC027", cblApp,, True)
		WHR &= dbconn.WHERE_LIKE("TD004", tbItem)
		WHR &= dbconn.WHERE_LIKE("TD006", tbSpec)
		WHR &= dbconn.WHERE_LIKE("TC004", tbCust)
		WHR &= dbconn.WHERE_LIKE("COPTC.UDF01", TbPlant)
		WHR &= dbconn.WHERE_LIKE("COPTD.UDF02", TbCustWo)
		WHR &= dbconn.WHERE_LIKE("COPTD.UDF03", TbCustLine)
		Dim ordBy As String = ""
		If CbOverDue.Checked Then
			WHR &= dbconn.WHERE_EQUAL("datediff(day,CONVERT(DATE,TD013,112),CONVERT(date, getdate()))", "0", ">")
			ordBy = "TD013_OVER Desc,"

		End If

		Dim SSQL1 As String = "(select count(*) from MOCTA where TA026=TD001 and  TA027=TD002 and TA028=TD003) "
		Dim SSQL2 As String = "(select count(*) from PURTB where TB029=TD001 and  TB030=TD002 and TB031=TD003) "
		Dim SSQL3 As String = "(select count(*) from PURTD D where D.TD014=TD001 and  D.TD021=TD002 and D.TD023=TD003) "
		Dim SSQL4 As String = "(SELECT count(*) FROM BOMMF WHERE MF001 = TD004	AND MF002 = '01' and MF005 = '2') "

		'Dim al As New ArrayList
		'Dim colName As ArrayList
		'Dim fldName As ArrayList
		'With New ArrayListControl(al)
		'    .TAL("(SUBSTRING(TC003,7,2)+'-'+SUBSTRING(TC003,5,2)+'-'+SUBSTRING(TC003,1,4))" & VarIni.C8 & "TC003", "Order Date")
		'    .TAL("TD001+'-'+TD002+'-'+TD003 as TD001" & VarIni.C8 & "TD001", "S/O")
		'    .TAL("TD004", "")
		'    .TAL("case TD015 when '' then '' else rtrim(TD015)+'-'+TD028 end" & VarIni.C8 & "TD015", "")
		'    .TAL("PLANT", "Plant")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'    .TAL("TD004", "")
		'End With
		Dim SQL1 As String = "(select * from (select TA001,TA006,TA023,TA024,TA025,TA051,ROW_NUMBER() OVER (PARTITION BY TA023,TA024,TA025 ORDER BY TA001) row_num from LRPTA  left join LRPLA on LA001=TA001  and LA012=TA050 where TA029='1' and LA005='1' and LA013 = '1' and TA006>0) AA where row_num=1) LRPTA "
		Dim sqlProcess As String = "(STUFF((SELECT ' |' + MW002 FROM BOMMF left join CMSMW on MW001=BOMMF.MF004 WHERE MF001 = TD004	AND MF002 = '01' order by MF003 FOR XML PATH('') ), 1, 2, '') ) "
		Dim sqlMat As String = "(select MD001, MD003,MB002+' '+MB003 MAT_SPEC,isnull(MC007,0) MC007,isnull(TA0157,0) TA0157,isnull(MC007,0)-isnull(TA0157,0) MAT_BAL from 
				(select * from (select MD001,MD003,ROW_NUMBER() OVER (PARTITION BY MD001 ORDER BY MD002) row_num from BOMMD where (MD003 like '511%' or MD003 like '503%')) AA where row_num =1 ) BB
				left join (select TA006,sum(TA015-TA017) TA0157 from MOCTA where TA011 not in('y','Y') and TA013 ='Y' and TA015-TA017>0 group by TA006) MOCTA on TA006=MD003
				left join (select MC001,sum(MC007) MC007  from INVMC group by MC001 having sum(MC007)>0) INVMC on MC001=MD003
				left join INVMB on MB001=MD003) MAT_DATA"

		SQL = " select TC003, " &
			  " TD001+'-'+TD002+'-'+TD003 as TD001,TD004," &
			  " case TD015 when '' then '' else rtrim(TD015)+'-'+TD028 end as TD015," &
			  " TD006,cast(TD008 as decimal(15,2)) as TD008,cast(TD009 as decimal(15,2)) as TD009," &
			  " cast(TD008-TD009 as decimal(15,2)) as TD0089,cast(INVMC.MC007 as decimal(15,2)) as MC007," &
			  " TD013," &
			  " TC004,MA002,TC012,COPTD.TD027 as custPO ," &
			  " case MB025 when 'P' then 'Purchase' when 'M' then 'Manufacture' when 'S' then 'Subcontract' when 'Y' then 'Phantom' when 'C' then 'Configuration' end as MB025  ," &
			  " case TD016 when 'N' then 'Not Closed' when 'Y' then 'Auto Closed' when 'y' then 'Manual Closed' end as TD016," &
			  " case TC027 when 'N' then 'Not Approved' when 'Y' then 'Approved' else TC027 end as TC027 ," &
			  " case when " & SSQL1 & "=0 then '' else 'Yes' end as MO, " &
			  " case when " & SSQL2 & "=0 then '' else 'Yes' end as PR, " &
			  " case when " & SSQL3 & "=0 then '' else 'Yes' end as PO ," &
			  " COPTC.UDF01 PLANT,COPTD.TD011 PRICE ,COPTD.UDF02 CWO,COPTD.UDF03 CLINE," &
			  " TA001,TA006,TA051," & sqlProcess & " PROCESS_LIST," &
			  " case when " & SSQL4 & "=0 then '' else 'Yes' end as PROCESS_OUTS, " &
			  " MAT_SPEC,MAT_BAL," &
			  " datediff(day,CONVERT(DATE,TD013,112),CONVERT(date, getdate())) TD013_OVER" &
			  " from COPTD left join COPTC on TC001=TD001 and TC002=TD002 " &
			  " left join INVMB on INVMB.MB001=TD004 " &
			  " left join INVMC on MC001=INVMB.MB001 and MC002=MB017 " &
			  " left join COPMA on MA001=TC004 " &
			  " left join " & SQL1 & " on TA023=TD001 and TA024=TD002 and TA025=TD003 " &
			  " left join " & sqlMat & " on  MD001=TD004 " &
			  " where 1=1 " & WHR &
			  " order by " & ordBy & "TD013,TD001,TD002,TD003 "

		ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
		ucCountRow.RowCount = gvShow.Rows.Count
		Select Case Trim(Session("Dept"))
			Case "A01", "A07"
			Case Else
				gvShow.Columns(10).Visible = False
		End Select

		btExport.Visible = True
	End Sub

	Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

		With e.Row
			If .RowType = DataControlRowType.DataRow Then
				Dim hplDetail As HyperLink = CType(.FindControl("hlShow"), HyperLink)

				If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("TD001")) Then
					Dim link As String = "&so= " & .DataItem("TD001").ToString.Trim
					link = link & "&item=" & .DataItem("TD004").ToString.Trim
					'link = link & "&desc=" & .DataItem("TD005").ToString.Trim
					link = link & "&spec=" & .DataItem("TD006").ToString.Trim
					hplDetail.NavigateUrl = "SaleOrderStatusPopup.aspx?height=150&width=350" & link
					hplDetail.Attributes.Add("title", .DataItem("TD006"))
				End If
				.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
				.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
			End If
		End With

	End Sub

End Class