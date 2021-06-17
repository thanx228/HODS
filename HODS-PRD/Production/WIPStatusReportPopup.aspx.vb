Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class WIPStatusReportPopup
	Inherits System.Web.UI.Page

	Dim gvCont As New GridviewControl
	Dim whrCont As New WhereControl
	Dim dbConn As New DataConnectControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		If Not IsPostBack Then
			btShow_Click(sender, e)
		End If

	End Sub

	Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
		Dim wc As String = Request.QueryString("wc")
		Dim endDate As String = Request.QueryString("enddate")
		wipStatusShow(wc, endDate)
	End Sub

	Function getColName() As ArrayList
		Return New ArrayList From {
		   "MO No" & VarIni.char8 & "STA001",
		   "MO Seq" & VarIni.char8 & "STA003",
		   "MO Date" & VarIni.char8 & "MTA003",
		   "Operation" & VarIni.char8 & "STA004",
		   "Desc." & VarIni.char8 & "MQ002",
		   "Item" & VarIni.char8 & "MTA006",
		   "Spec" & VarIni.char8 & "MTA035",
		   "Description" & VarIni.char8 & "MTA034",
		   "Work Center" & VarIni.char8 & "STA006",
		   "Receipt WH" & VarIni.char8 & "MTA020",
		   "Plan Start Date" & VarIni.char8 & "STA008",
		   "Plan Complete Date" & VarIni.char8 & "STA009",
		   "Actual Start Date" & VarIni.char8 & "STA030",
		   "Actual Complete Date" & VarIni.char8 & "STA031",
		   "Input Qty" & VarIni.char8 & "STA010" & VarIni.char8 & "0",
		   "Completed Qty" & VarIni.char8 & "STA011" & VarIni.char8 & "0",
		   "WIP Qty" & VarIni.char8 & "WIP_QTY" & VarIni.char8 & "0",
		   "Completed Code" & VarIni.char8 & "STA032",
		   "Status" & VarIni.char8 & "MTA011",
		   "Unit" & VarIni.char8 & "MTA007",
		   "Last Transfer Date" & VarIni.char8 & "CREATE_DATE",
		   "Diff Date" & VarIni.char8 & "date_diff" & VarIni.char8 & "0"
		}
	End Function

	Sub wipStatusShow(wc As String, lastDate As String)
		' Dim dtShow As DataTable = wipStatus(wc, "")
		Dim colName As ArrayList = getColName()
		gvCont.GridviewInitial(gvShow, colName, strSplit:=VarIni.char8)
		gvCont.ShowGridView(gvShow, wipStatus(wc, "", lastDate))
		CountRow1.RowCount = gvCont.rowGridview(gvShow)
		ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gvShowScrollbar", "gvShowScrollbar();", True)
	End Sub

	Function wipStatus(Optional wc As String = "", Optional WHRTO As String = "", Optional lastDate As String = "") As DataTable

		Dim SQL As String = String.Empty
		Dim WHR As String = String.Empty
		Dim dt As New DataTable

		WHR = dbConn.WHERE_EQUAL("CREATE_DATE", lastDate, "<=")

		SQL = "(select * from (
				select *,ROW_NUMBER() OVER (PARTITION BY TC004,TC005,TC008,TC009 ORDER BY CREATE_DATE desc) row_num from (
				SELECT  SFCTC.TC004,SFCTC.TC005,SFCTC.TC008,SFCTC.TC009, 	
				substring(case when MODI_DATE='' or MODI_DATE is null then CREATE_DATE else MODI_DATE end,1,8) CREATE_DATE	
				FROM SFCTC 
				) AA ) BB where row_num=1 " & WHR & " ) SFCTC"

		WHR = whrCont.Where("(SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058)", ">0")
		WHR &= whrCont.Where("MOC.TA011", "3",, False)
		'WHR &= whrCont.Where("CMS.MD001", wc, True, False, True)   '--------- ปกติ
		'===============================================================================================================



		'เงื่อนไข เช็ค WorkCenter ถ้ากด Export ในหน้า Popup จะเข้า IF //// ถ้ากด Export Detail ในหน้าแรกจะเข้า ELSE 
		If wc <> "" Then
			WHR &= whrCont.Where("CMS.MD001", wc, True, False, True)
		Else
			WHR &= WHRTO
		End If


		SQL = " select SFC.TA001 +'-'+ SFC.TA002 STA001,SFC.TA003 STA003," &
		" (SUBSTRING(MOC.TA003, 7, 2) +'-'+ SUBSTRING(MOC.TA003,5,2) +'-'+ SUBSTRING(MOC.TA003,1,4)) MTA003," &
		" SFC.TA004 +':'+ SMW.MW002 STA004,SMQ.MQ002,MOC.TA006 MTA006,SFC.TA006 STA006,MOC.TA020 +':'+ SMC.MC002 MTA020," &
		" (SUBSTRING(SFC.TA008, 7, 2) +'-'+ SUBSTRING(SFC.TA008,5,2) +'-'+ SUBSTRING(SFC.TA008,1,4)) STA008," &
		" (SUBSTRING(SFC.TA009,7,2) +'-'+ SUBSTRING(SFC.TA009,5,2) +'-'+ SUBSTRING(SFC.TA009,1,4)) STA009," &
		" (SUBSTRING(SFC.TA030, 7, 2) +'-'+ SUBSTRING(SFC.TA030,5,2) +'-'+ SUBSTRING(SFC.TA030,1,4)) STA030," &
		" (SUBSTRING(SFC.TA031,7,2) +'-'+ SUBSTRING(SFC.TA031,5,2) +'-'+ SUBSTRING(SFC.TA031,1,4)) STA031," &
		" SFC.TA010 STA010, SFC.TA011 STA011,(SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058) WIP_QTY ," &
		" Case SFC.TA032 when 'Y' then 'Y:Completed' when 'y' then 'y:Manual Completed' else 'N:UnCompleted' end STA032," &
		" case MOC.TA011 when 'Y' then 'Y:Completed' when 'y' then 'y:Manual Completed' else '1-3:Producing' end MTA011," &
		" MOC.TA035 MTA035,MOC.TA034 MTA034,MOC.TA007 MTA007,SFCTC.CREATE_DATE," &
		"ABS(DATEDIFF(day,getdate(),convert(date,SFCTC.CREATE_DATE))) date_diff " &
		" from CMSMD CMS" &
		" left join MOCTA MOC on MOC.TA019 = CMS.MD003" &
		" left join SFCTA SFC on SFC.COMPANY = MOC.COMPANY And SFC.TA006 = CMS.MD001 And SFC.TA007 = CMS.MD002 And SFC.TA002 = MOC.TA002" &
		" left join CMSMQ SMQ on CMS.COMPANY = SMQ.COMPANY and SMQ.MQ001 = SFC.TA001" &
		" left join CMSMW SMW on SFC.TA004 = SMW.MW001 And SFC.TA006 = SMW.MW005" &
		" left join CMSMC SMC on MOC.TA020 = SMC.MC001" &
		" left join " & SQL & " on SFCTC.TC004 = SFC.TA001   And SFCTC.TC005 = SFC.TA002  And SFCTC.TC008 = SFC.TA003  And SFCTC.TC009 = SFC.TA004 " &
		" where SFC.COMPANY='HOOTHAI'" & WHR & " ORDER BY date_diff desc, SFC.TA001,SFC.TA002"

		dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
		Return dt
	End Function

	Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
		For Each row As GridViewRow In gvShow.Rows
			For Each cell As TableCell In row.Cells
				cell.Height = 20
			Next
		Next
		ExportsUtility.ExportGridviewToMsExcel("WIPStatusReportPopup", gvShow)
	End Sub

	'Save File
	Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
		'Save To Excel File
	End Sub

End Class