Imports System.Data.SqlClient
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class LockProductionPlan
	Inherits System.Web.UI.Page
	Dim Conn_SQL As New ConnSQL
	Dim ControlForm As New ControlDataForm
	Dim configDate As New ConfigDate
	Dim CreateTempTable As New CreateTempTable

	Dim gvcont As New GridviewControl
	Dim dbconn As New DataConnectControl


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
			If Session("UserName") = "" Then
				Response.Redirect("../Login.aspx")
			End If
			clearData()
		End If
	End Sub

	Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
		Dim SQL As String = "",
			WHR As String = ""
		WHR &= dbconn.WHERE_LIKE("TA001", tbBatchNo)
		WHR &= dbconn.WHERE_EQUAL("TA023", tbSaleType)
		WHR &= dbconn.WHERE_EQUAL("TA024", tbSaleNo)
		WHR &= dbconn.WHERE_EQUAL("TA025", tbSaleSeq)
		WHR &= dbconn.WHERE_LIKE("TA002", tbItem)
		WHR &= dbconn.WHERE_LIKE("MB003", tbSpec)
		WHR &= dbconn.WHERE_LIKE("MF006", TbWC)
		WHR &= dbconn.WHERE_LIKE("TD004", tbItemSO)
		WHR &= dbconn.WHERE_LIKE("TD006", tbSpecSO)

		Dim SSQL4 As String = "(SELECT count(*) FROM BOMMF WHERE MF001 = TA002	AND MF002 = '01' and MF005 = '2') "

		Dim sqlProcess As String = "(STUFF((SELECT ' |' + MW002 FROM BOMMF left join CMSMW on MW001=BOMMF.MF004 WHERE MF001 = TD004	AND MF002 = '01' order by MF003 FOR XML PATH('') ), 1, 2, '') ) "
		Dim sqlMat As String = "(select MD001, MD003,MB002+' '+MB003 MAT_SPEC,isnull(MC007,0) MC007,isnull(TA0157,0) TA0157,isnull(MC007,0)-isnull(TA0157,0) MAT_BAL from 
				(select * from (select MD001,MD003,ROW_NUMBER() OVER (PARTITION BY MD001 ORDER BY MD002) row_num from BOMMD where (MD003 like '511%' or MD003 like '503%')) AA where row_num =1 ) BB
				left join (select TA006,sum(TA015-TA017) TA0157 from MOCTA where TA011 not in('y','Y') and TA013 ='Y' and TA015-TA017>0 group by TA006) MOCTA on TA006=MD003
				left join (select MC001,sum(MC007) MC007  from INVMC group by MC001 having sum(MC007)>0) INVMC on MC001=MD003
				left join INVMB on MB001=MD003) MAT_DATA"

		SQL = "(select MF001,MF006,MF007 from ( select ROW_NUMBER() OVER (PARTITION BY MF001 ORDER BY MF003) row_num,MF006,MF001,MF007 from BOMMF where MF002='01') BOMMF where row_num=1) BOMMF"
		SQL = " select TA001 as 'Batch No.',TA050 'Versoin',TA023+'-'+TA024+'-'+TA025 as 'Sale Order'," &
			  " TA002 item,MB003 as Spec,cast(TA006 as decimal(10,2)) as 'Prod.Qty'," &
			  " (SUBSTRING(TA007,7,2)+'-'+SUBSTRING(TA007,5,2)+'-'+SUBSTRING(TA007,1,4)) as 'Plan Start Date'," &
			  " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'Plan Complete Date'," &
			  " TA010 as 'MO Type',TA004 as 'WC',TA005 as 'WH',TA009 as 'Lock',TC012 'Industry Type',MF006 'W/C First',MF007 'W/C First NAME'," &
			  "case  TD004 when TA002 then '' else TD004 end  'SO Item'," &
			  " case  TD004 when TA002 then '' else TD006 end   'SO Spec'," &
			  " case when " & SSQL4 & "=0 then '' else 'Yes' end as 'Outsource', " & sqlProcess & " 'Process List',MAT_SPEC 'Materails',MAT_BAL 'Mat Available'" &
			  " from LRPTA  left join LRPLA on LA001=TA001  and LA012=TA050 " &
			  " left join INVMB on MB001=TA002 " &
			  " LEFT JOIN COPTD ON TD001=TA023 and TD002=TA024 AND TD003=TA025 " &
			  " LEFT JOIN COPTC ON TC001=TD001 and TC002=TD002 " &
			  " LEFT JOIN " & SQL & " ON MF001=TA002 " &
			  " left join " & sqlMat & " on  MD001=TA002 " &
			  " where TA029='1' and TA051 = 'N' and LA005='1' and LA013 = '1' and TA006>0 " & WHR &
			  " order by TA001,TA023,TA024,TA025,TA002 "
		'ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
		gvcont.ShowGridView(gvShow, SQL, VarIni.ERP)

		Dim row As Decimal = ControlForm.rowGridview(gvShow)
		lbCount.Text = ControlForm.rowGridview(gvShow)
		If row > 0 Then
			btLock.Visible = True
		End If

		'btExport.Visible = True
		System.Threading.Thread.Sleep(1000)
	End Sub

	Protected Sub btLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLock.Click
		Dim sqlList As New ArrayList
		With gvShow
			For i As Integer = 0 To .Rows.Count - 1
				With .Rows(i)
					Dim cbSelect As CheckBox = .FindControl("cbSelect")
					If cbSelect.Checked Then
						If .Cells(11).Text <> "Y" Then
							Dim USQL As String = " update LRPTA set TA009='Y' where TA001='" & .Cells(1).Text.Trim & "' and TA050='" & .Cells(2).Text.Trim & "' and TA023+'-'+TA024+'-'+TA025='" & .Cells(3).Text.Trim & "' and TA002='" & .Cells(4).Text.Trim.Replace("-", "") & "' "
							'Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
							sqlList.Add(USQL)
						End If
					End If
				End With
			Next
		End With
		Dim msg As String = "no update data"
		If sqlList.Count > 0 Then
			dbconn.TransactionSQL(sqlList, VarIni.ERP, dbconn.WhoCalledMe)
			msg = "update completed"
		End If
		show_message.ShowMessage(Page, msg, UpdatePanel1)
	End Sub
	Sub clearData()
		tbBatchNo.Text = ""
		tbSaleType.Text = ""
		tbSaleNo.Text = ""
		tbSaleSeq.Text = ""
		tbItem.Text = ""
		tbSpec.Text = ""
		btLock.Visible = False
		gvShow.DataSource = ""
		gvShow.DataBind()

		'Dim da As New SqlDataAdapter()
		'Dim ds As New DataSet
		'da.Fill(ds)
		'ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
		'gvShow.DataSource = ds
		'gvShow.DataBind()
		'Dim ColumnCount As Integer = gvShow.Rows(0).Cells.Count
		'gvShow.Rows(0).Cells.Clear()
		'gvShow.Rows(0).Cells.Add(New TableCell())
		'gvShow.Rows(0).Cells(0).ColumnSpan = ColumnCount
		'gvShow.Rows(0).Cells(0).Text = "No Data Found"

	End Sub

	Protected Sub btClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btClear.Click
		clearData()
	End Sub
End Class