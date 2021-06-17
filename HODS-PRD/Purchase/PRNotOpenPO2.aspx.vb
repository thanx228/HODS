Imports System.Globalization

Public Class PRNotOpenPO2
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    'Dim SOType As String = "('3112','3113')"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 = '22' order by MQ002 "
            ControlForm.showCheckboxList(cblSoType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 = '22' order by MQ002 "
            ControlForm.showCheckboxList(cblSoTypePO, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 = '31' order by MQ002 "
            ControlForm.showCheckboxList(cblPrType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 like 'CA%' order by MQ002 "
            ControlForm.showCheckboxList(cblAsPrType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 = '33' order by MQ002 "
            ControlForm.showCheckboxList(cblPOType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False

            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)

        End If

    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHRAll As String = "",
            WHR As String = "",
            cnt As Decimal = 0,
            notIn As Boolean = False,
            selAll As Boolean = True

        Dim PricePO As String = "",
            PricePR As String = ""

        If Session("UserGroup").ToString.Substring(0, 3).Trim = "PUR" Or Session("UserGroup").ToString.Trim = "SYS" Then
            PricePO = " , cast(TD010 as decimal(16,2)) as 'Price' "
            PricePR = " , cast(L.TB017 as decimal(16,2)) as 'Price' "

        End If

        If TabContainer1.ActiveTabIndex = 0 Then

            For Each boxItem As ListItem In cblSoType.Items
                Dim boxVal As String = CStr(boxItem.Value.Trim)
                If boxItem.Selected Then
                    cnt = cnt + 1
                End If
            Next

            If cnt = 0 And cbNoneSO.Checked Then
                notIn = True
            ElseIf cnt = 0 And cbAll.Checked And cbNoneSO.Checked = False Then
                selAll = False
                'notIn = True
            End If

            WHR = WHR & Conn_SQL.Where("H.TA007", CblApp) 'code type
            WHR = WHR & Conn_SQL.Where("substring(H.TA006,1,4) ", cblSoType, notIn, "", selAll) 'Sale Order type
            WHR = WHR & Conn_SQL.Where("substring(L.TB004,3,1)", cblCodeType) 'Code Type
            WHR = WHR & Conn_SQL.Where("L.TB004", txtitem) 'item
            WHR = WHR & Conn_SQL.Where("L.TB006", txtspec) 'spec
            WHR = WHR & Conn_SQL.Where("L.TB001", cblPrType) 'pr type
            WHR = WHR & Conn_SQL.Where("L.TB002", txtno) 'pr NO

            WHR = WHR & Conn_SQL.Where("L.TB039", cblCloseStatus) 'pr type
            'WHR = WHR & Conn_SQL.Where("(select top 1 LRPTC.TC027 from LRPLA left join LRPTC on LRPTC.TC001=LRPLA.LA001 and LRPTC.TC024=LRPLA.LA005 where LRPTC.TC026 =L.TB029 and LRPTC.TC027 =L.TB030 and LRPTC.TC028 =L.TB031 )", txtSONo)
            'WHR = WHR & Conn_SQL.Where("(select top 1 LRPLA.LA001 from LRPLA left join LRPTC on LRPTC.TC001=LRPLA.LA001 and LRPTC.TC024=LRPLA.LA005 where LRPTC.TC026 =L.TB029 and LRPTC.TC027 =L.TB030 and LRPTC.TC028 =L.TB031 )", txtPlanBatch)

            WHR = WHR & Conn_SQL.Where("L.TB030", txtSONo)
            WHR = WHR & Conn_SQL.Where("L.TB057", txtPlanBatch)

            WHR = WHR & Conn_SQL.Where("H.TA013", configDate.dateFormat2(txtdateissue.Text.Trim), configDate.dateFormat2(txtdateissueTo.Text.Trim))
            WHR = WHR & Conn_SQL.Where("L.TB011", configDate.dateFormat2(txtrequest.Text.Trim), configDate.dateFormat2(txtrequestTo.Text.Trim))

            SQL = " select distinct L.TB001 as 'PR Type',L.TB002 as 'PR No.',L.TB003 as 'PR Seq.', " &
                " case when len(L.TB004)=16 then (SUBSTRING(L.TB004,1,14)+'-'+SUBSTRING(L.TB004,15,2)) else L.TB004 end as 'Item', " &
                " L.TB005 as 'Description',L.TB006 as 'Spec',L.TB007 as 'Unit', " &
                " case when L.TB030 = '' then '' else  Rtrim(L.TB029)+'-'+L.TB030+'-'+L.TB031 end as 'Sale Type-No-Seq', " &
                " case when L.TB059 = '' then Rtrim(L.TB057)+'-'+L.TB058 else Rtrim(L.TB057)+'-'+L.TB058+'-'+L.TB059 end as 'Plan Batch No.',  " &
                " CAST(L.TB009 AS DECIMAL(16,3)) as 'PR Quantity',L.TB010 as 'Cust ID', " &
                " (SUBSTRING(L.TB011,7,2)+'-'+SUBSTRING(L.TB011,5,2)+'-'+SUBSTRING(L.TB011,1,4)) as 'Required Date', " &
                " (SUBSTRING(H.TA013,7,2)+'-'+SUBSTRING(H.TA013,5,2)+'-'+SUBSTRING(H.TA013,1,4)) as 'Issue Date' , " &
                " Rtrim(H.TA004)+'-'+N.ME002 as 'P/R Dept.', H.TA007 as 'Approved Status',  " &
                " R.TR019 as 'PO No.',cast(D.TD008 as decimal(15,2)) as 'PO Qty', " &
                " rtrim(PURTC.TC004)+'-'+MA002 as 'Supplier',D.TD014 as 'Confirm Del Date',  S.UDF06 as 'PUR Confirm Date',S.UDF07 as 'PC Confirm Date', " &
                " S.UDF05 as 'Sale Confirm'  , cast(L.TB017 as decimal(16,2)) as 'Price'  , PURTA.TA006 as 'Remark',  " &
                " (SUBSTRING(H.TA003,1,4)+'-'+SUBSTRING(H.TA003,5,2)+'-'+SUBSTRING(H.TA003,7,2)) as 'P/R Date', " &
                " case when LRPTC.TC007 = '' then '' else (SUBSTRING(LRPTC.TC007,1,4)+'-'+SUBSTRING(LRPTC.TC007,5,2)+'-'+SUBSTRING(LRPTC.TC007,7,2)) end as 'PUR DATE' " &
                " from PURTB L  " &
                " left join PURTR R on R.TR001 = L.TB001 and R.TR002 = L.TB002 and R.TR003 = L.TB003   " &
                " left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002)  " &
                " left join CMSME N on(N.ME001=H.TA004)  " &
                " left join PURTD D on D.TD026=L.TB001 and D.TD027=L.TB002 and D.TD028=L.TB003  " &
                " left join PURTC on TC001=TD001 and TC002 =TD002  " &
                " left join PURMA on MA001=TC004  " &
                " left join PURTA on PURTA.TA001=TB001 and PURTA.TA002 = TB002  " &
                " left join LRPTC on LRPTC.TC026 =L.TB029 and LRPTC.TC027 =L.TB030 and LRPTC.TC028 =L.TB031 and " &
                " LRPTC.TC002 = L.TB004 and LRPTC.TC001 =L.TB057 and LRPTC.TC046 =L.TB058 and LRPTC.TC025 =L.TB059 " &
                " left join COPTD S on S.TD001 = LRPTC.TC026 and S.TD002 = LRPTC.TC027 and S.TD003 = LRPTC.TC028  " &
                " where 1=1 " & WHR

            'SQL = "select distinct L.TB001 as 'PR Type',L.TB002 as 'PR No.',L.TB003 as 'PR Seq.',case when len(L.TB004)=16 then (SUBSTRING(L.TB004,1,14)+'-'+SUBSTRING(L.TB004,15,2)) else L.TB004 end as 'Item'," & _
            '    " L.TB005 as 'Description',L.TB006 as 'Spec',L.TB007 as 'Unit'," & _
            '    " (select top 1 case when LRPTC.TC026 = '' then '' else LRPTC.TC026+'-'+LRPTC.TC027+'-'+LRPTC.TC028 end from LRPLA left join LRPTC on LRPTC.TC001=LRPLA.LA001 and LRPTC.TC024=LRPLA.LA005 where LRPTC.TC026 =L.TB029 and LRPTC.TC027 =L.TB030 and LRPTC.TC028 =L.TB031 ) as 'Sale Type-No-Seq'," & _
            '    " (select top 1 Rtrim(LRPLA.LA001)+'-'+LRPLA.LA012 from LRPLA left join LRPTC on LRPTC.TC001=LRPLA.LA001 and LRPTC.TC024=LRPLA.LA005 where LRPTC.TC026 =L.TB029 and LRPTC.TC027 =L.TB030 and LRPTC.TC028 =L.TB031  and LRPTC.TC002 = L.TB004 ) as 'Plan Batch No.'," & _
            '    " CAST(L.TB009 AS DECIMAL(16,3)) as 'PR Quantity',L.TB010 as 'Cust ID'," & _
            '    " (SUBSTRING(L.TB011,7,2)+'-'+SUBSTRING(L.TB011,5,2)+'-'+SUBSTRING(L.TB011,1,4)) as 'Required Date'," & _
            '    " (SUBSTRING(H.TA013,7,2)+'-'+SUBSTRING(H.TA013,5,2)+'-'+SUBSTRING(H.TA013,1,4)) as 'Issue Date' ," & _
            '    " H.TA004+'-'+N.ME002 as 'P/R Dept.', H.TA007 as 'Approved Status', " & _
            '    " R.TR019 as 'PO No.',cast(D.TD008 as decimal(15,2)) as 'PO Qty', " & _
            '    " rtrim(PURTC.TC004)+'-'+MA002 as 'Supplier',D.TD014 as 'Confirm Del Date', " & _
            '    " S.UDF06 as 'PUR Confirm Date',S.UDF07 as 'PC Confirm Date',S.UDF05 as 'Sale Confirm' " & PricePR & " , PURTA.TA006 as 'Remark', " & _
            '    " (SUBSTRING(H.TA003,1,4)+'-'+SUBSTRING(H.TA003,5,2)+'-'+SUBSTRING(H.TA003,7,2)) as 'P/R Date'," & _
            '    " (select top 1 (SUBSTRING(LRPTC.TC007,1,4)+'-'+SUBSTRING(LRPTC.TC007,5,2)+'-'+SUBSTRING(LRPTC.TC007,7,2)) from LRPLA left join LRPTC on LRPTC.TC001=LRPLA.LA001 and LRPTC.TC024=LRPLA.LA005 where LRPTC.TC026 =L.TB029 and LRPTC.TC027 =L.TB030 and LRPTC.TC028 =L.TB031 and LRPTC.TC002 = L.TB004) as 'PUR DATE'" & _
            '    " from PURTB L " & _
            '    " left join PURTR R on R.TR001 = L.TB001 and R.TR002 = L.TB002 and R.TR003 = L.TB003 " & _
            '    " left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) " & _
            '    " left join CMSME N on(N.ME001=H.TA004) " & _
            '    " left join PURTD D on D.TD026=L.TB001 and D.TD027=L.TB002 and D.TD028=L.TB003 " & _
            '    " left join PURTC on TC001=TD001 and TC002 =TD002 " & _
            '    " left join PURMA on MA001=TC004 " & _
            '    " left join PURTA on PURTA.TA001=TB001 and PURTA.TA002 = TB002 " & _
            '    " left join LRPTC on LRPTC.TC026 =L.TB029 and LRPTC.TC027 =L.TB030 and LRPTC.TC028 =L.TB031 " & _
            '    " left join COPTD S on S.TD001 = LRPTC.TC026 and S.TD002 = LRPTC.TC027 and S.TD003 = LRPTC.TC028 " & _
            '    " where 1=1" & WHR
            ''" order by L.TB011,L.TB001,L.TB002,L.TB003 " 

        ElseIf TabContainer1.ActiveTabIndex = 1 Then

            WHR = WHR & Conn_SQL.Where("ASTTI.TI001", cblAsPrType)
            WHR = WHR & Conn_SQL.Where("ASTTI.TI002", txtnoAsset)
            WHR = WHR & Conn_SQL.Where("ASTTJ.TJ005", txtspecAsset)
            WHR = WHR & Conn_SQL.Where("ASTTI.TI010", CblAppAsset)
            WHR = WHR & Conn_SQL.Where("ASTTI.TI004", configDate.dateFormat2(txtdateissueAsset.Text.Trim), configDate.dateFormat2(txtdateissueToAsset.Text.Trim))
            WHR = WHR & Conn_SQL.Where("ASTTJ.TJ009", configDate.dateFormat2(txtrequestAsset.Text.Trim), configDate.dateFormat2(txtrequestToAsset.Text.Trim))

            If cbNo.Checked = True Then
                WHR = WHR & " and ASTTJ.TJ021 = 'N' "
            End If

            SQL = " select ASTTI.TI001 as 'PR Type',ASTTI.TI002 as 'PR No.',ASTTJ.TJ003 as 'PR Seq.','-' as 'Item',   " & _
                " ASTTJ.TJ004 as 'Description',ASTTJ.TJ005 as 'Spec',ASTTJ.TJ006 as 'Unit',   " & _
                " '-' as 'Sale Type-No-Seq', '-' as 'Plan Batch No.' , " & _
                " CAST(ASTTJ.TJ007 AS DECIMAL(16,3)) as 'PR Quantity','-' as 'Cust ID',   " & _
                " case when ASTTJ.TJ009 = '' then '' else (SUBSTRING(ASTTJ.TJ009,7,2)+'-'+SUBSTRING(ASTTJ.TJ009,5,2)+'-'+SUBSTRING(ASTTJ.TJ009,1,4)) end as 'Required Date',   " & _
                " case when ASTTI.TI004 = '' then '' else (SUBSTRING(ASTTI.TI004,7,2)+'-'+SUBSTRING(ASTTI.TI004,5,2)+'-'+SUBSTRING(ASTTI.TI004,1,4)) end as 'Issue Date' ,   " & _
                " ASTTI.TI006+'-'+CMSME.ME002 as 'P/R Dept.',ASTTI.TI010 as 'Approved Status',    " & _
                " ASTTN.TN001+'-'+ASTTN.TN002+'-'+ASTTN.TN003 as 'Asset P/O',(SUBSTRING(ASTTI.TI003,7,2)+'-'+SUBSTRING(ASTTI.TI003,5,2)+'-'+SUBSTRING(ASTTI.TI003,1,4)) as 'P/R Date', " & _
                " ASTTJ.TJ021 as 'Close Status' " & _
                " from ASTTI left join ASTTJ on(ASTTJ.TJ001 = ASTTI.TI001) and (ASTTJ.TJ002 = ASTTI.TI002) " & _
                " left join ASTTN on ASTTN.TN016 = ASTTI.TI001 and ASTTN.TN017 = ASTTI.TI002 and ASTTN.TN018 = ASTTJ.TJ003 " & _
                " left join CMSME on(CMSME.ME001 = ASTTI.TI006) " & _
                " where 1=1 " & WHR & _
                " order by ASTTI.TI001,ASTTI.TI002,ASTTJ.TJ003 "

        Else

            For Each boxItem As ListItem In cblSoTypePO.Items
                Dim boxVal As String = CStr(boxItem.Value.Trim)
                If boxItem.Selected Then
                    cnt = cnt + 1
                End If
            Next

            WHR = WHR & Conn_SQL.Where("TD001", cblPOType)
            WHR = WHR & Conn_SQL.Where("TD002", tbPoNo)
            WHR = WHR & Conn_SQL.Where("substring(TA006,1,4)", cblSoTypePO)
            WHR = WHR & Conn_SQL.Where("TC004", tbSup)
            WHR = WHR & Conn_SQL.Where("TD004", txtitemPO)
            WHR = WHR & Conn_SQL.Where("TD006", txtspecPO)
            WHR = WHR & Conn_SQL.Where("TD016", cblCloseStatusPO)

            Select Case ddlDate.SelectedValue
                Case "TD014" ' yyyy-MM-dd (dateFormat4)
                    WHR = WHR & Conn_SQL.Where(ddlDate.Text, configDate.dateFormat4(tbDateFrom.Text.Trim,,), configDate.dateFormat4(tbDateTo.Text.Trim,, "-"))
                Case "TC003" ' yyyyMMdd (dateFormat2)
                    WHR = WHR & Conn_SQL.Where(ddlDate.Text, configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))
            End Select

            SQL = "select L.TD001,L.TD002,L.TD003,RL.TB003,L.TD026,L.TD027,L.TD004,L.TD006,L.TD016,L.TD007,L.TD008,L.TD015,L.TD009,L.TD014,L.TD008 - L.TD015 as Balance,H.TC004,R.TH007,R.TH015,L.UDF01 as PDD,L.TD012 as CDD,L.TD007 as WH,RL.TB011 AS RDate from PURTD L left join PURTC H on(H.TC001 = L.TD001) and (H.TC002 = L.TD002) left join PURTH R on (L.TD001 = R.TH011) and (L.TD002 = R.TH012) and (L.TD004 = R.TH004) and (L.TD015 = R.TH007) left join PURTB RL ON(L.TD026 = RL.TB001) AND (L.TD027 = RL.TB002) and (L.TD004 = RL.TB004) and (L.TD003 = RL.TB003) where L.TD016 = 'N'"
            Dim SSQL As String = ""
            SSQL = "cast((select sum(TH007) from PURTH where TH011=TD001 and TH012=TD002 and TH013=TD003 and TH030='N' ) as decimal(15,2)) as 'Wait Receipt Approved',"

            SQL = " select TD001+'-'+TD002+'-'+TD003 as 'PO NO'," & _
                  " (SUBSTRING(TC003,7,2)+'-'+SUBSTRING(TC003,5,2)+'-'+SUBSTRING(TC003,1,4)) as 'PO Date'," & _
                  " rtrim(TC004)+'-'+MA002 as 'Supplier',case when len(TD004)=16 then (SUBSTRING(TD004,1,14)+'-'+SUBSTRING(TD004,15,2)) else TD004 end as 'Item',TD005 as 'Desc'," & _
                  " TD006 as 'Spec' ,case when TD026='' then '' else TD026+'-'+TD027+'-'+TD028 end as 'PR NO' ," & _
                  " TA005 as 'Source Ref.',TD009 as 'Unit',cast(TD008 as decimal(15,2)) as 'Purchase Qty'," & _
                  " cast(TD015 as decimal(15,2)) as 'Delivery Qty',cast(TD008-TD015 as decimal(15,2)) as 'Balance Qty'," & SSQL & _
                  " TD014 as 'Confirm Del Date'," & _
                  " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as 'Plan Delivery Date' " & PricePO & _
                  " from PURTD left join PURTC on TC001=TD001 and TC002 =TD002 " & _
                  " left join PURTA on TA001=TD026 and TA002=TD027 " & _
                  " left join PURMA on MA001=TC004 " & _
                  " where 1=1 " & WHR & _
                  " order by TD001,TD002,TD003 "

            '
        End If

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click

        If TabContainer1.ActiveTabIndex = 0 Then
            ControlForm.ExportGridViewToExcel("PRStatus" & Session("UserName"), gvShow)
        ElseIf TabContainer1.ActiveTabIndex = 1 Then
            ControlForm.ExportGridViewToExcel("AssetPR" & Session("UserName"), gvShow)
        Else
            ControlForm.ExportGridViewToExcel("OpenPO" & Session("UserName"), gvShow)
        End If

    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

End Class