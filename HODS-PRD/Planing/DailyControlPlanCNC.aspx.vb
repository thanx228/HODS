Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class DailyControlPlanCNC
    Inherits System.Web.UI.Page
    'Dim ControlForm As New ControlDataForm
    'Dim Conn_SQL As New ConnSQL
    'Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim bCapa As Integer = 0,
        line As Integer = 0,
        planStard As Date = DateTime.ParseExact(Date.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
    '        wcList As String = "'W22'"
    Dim dbConn As New DataConnectControl
    Dim dateCont As New DateControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session(VarIni.UserName) <> "" Then
                Dim ddlCont As New DropDownListControl
                Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 " 'where MD001 in (" & wcList & ") 
                ddlCont.showDDL(ddlWC, SQL, VarIni.ERP, "MD002", "MD001", False)

                Dim cblCont As New CheckBoxListControl
                SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
                cblCont.showCheckboxList(cblMoType, SQL, VarIni.ERP, "MQ002", "MQ001", 4)

                SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
                ddlCont.showDDL(ddlSaleType, SQL, VarIni.ERP, "MQ002", "MQ001", True)

                btExportExcel.Visible = False
            End If
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            sSQL As String = "",
            sSQL1 As String = "",
            sSQL2 As String = "",
            iSQL As String = "",
            WHR As String = "",
            WHR2 As String = ""

        Dim xSql As String = "select capacity from MachineCapacity where wc='" & ddlWC.Text.Trim & "' "
        Dim Program As New DataTable
        Program = dbConn.Query(xSql, VarIni.DBMIS, dbConn.WhoCalledMe)
        lbCapa.Text = Program.Rows(0).Item("capacity")

        Dim tempTable As String = "tempDailyControlPlanCNC" & Session("UserName")
        CreateTempTable.createTempMO(tempTable)
        'get search area
        WHR &= dbConn.WHERE_EQUAL("SFCTA.TA006", ddlWC)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA006", tbCode)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA035", tbSpec)
        WHR &= dbConn.WHERE_IN("SFCTA.TA001", cblMoType)
        WHR &= dbConn.WHERE_EQUAL("MOCTA.TA026", ddlSaleType)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA035", tbCust)
        Dim fldDate As String = "SFCTA.TA008" '1
        Dim cause As Boolean = False
        If ddlDate.Text.Trim = "2" Then '2
            fldDate = "SFCTB.TB003"
            cause = True
        End If
        WHR &= dbConn.Where(fldDate, dateCont.dateFormat2(tbDateFrom.Text), dateCont.dateFormat2(tbDateTo.Text), cause)

        'get transfer number
        sSQL = " (select top 1 SFCTA1.TA008 from " & VarIni.ERP & "..SFCTC SFCTC1 " &
               " left join " & VarIni.ERP & "..SFCTA SFCTA1 on SFCTA1.TA001 = SFCTC1.TC004 and SFCTA1.TA002 = SFCTC1.TC005 and SFCTA1.TA003 = SFCTC1.TC008 " &
               " where SFCTC1.TC001=SFCTC.TC001 and SFCTC1.TC002=SFCTC.TC002 order by SFCTA1.TA008) ,"
        'WIP = SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048
        SQL = "select SFCTC.TC001,SFCTC.TC002,SFCTC.TC003,SFCTA.TA001,SFCTA.TA002,SFCTA.TA003," &
               sSQL & " SFCTB.TB003,case when SFCTC.TC003='0001' then SFCTB.TB014 else '0' end " &
              " from " & VarIni.ERP & "..SFCTA " &
              " left join " & VarIni.ERP & "..MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " &
              " left join " & VarIni.ERP & "..COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " &
              " left join " & VarIni.ERP & "..SFCTC on SFCTC.TC004 = SFCTA.TA001 and SFCTC.TC005 = SFCTA.TA002 and SFCTC.TC008 = SFCTA.TA003 " &
              " left join " & VarIni.ERP & "..SFCTB on SFCTB.TB001 = SFCTC.TC001 and SFCTB.TB002 = SFCTC.TC002 " &
              " where MOCTA.TA013='Y' and SFCTA.TA005='1' and SFCTA.TA001 not in('5195') and SFCTC.TC001='D201' " &
              " and SFCTA.TA010+SFCTA.TA016-SFCTA.TA011-SFCTA.TA015-SFCTA.TA048>0 " &
              " and SFCTA.TA032 = 'N' and MOCTA.TA011 not in('y','Y') " & WHR
        iSQL = "insert into " & tempTable & "(TC001,TC002,TC003,TA001,TA002,TA003,TA008,TB003,TB014) " & SQL
        dbConn.TransactionSQL(iSQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        sSQL1 = " (select top 1 rtrim(SFCTA2.TA006)+'-'+SFCTA2.TA007 from " & VarIni.ERP & "..SFCTA SFCTA2 " &
                " where SFCTA2.TA001=SFCTA.TA001 and SFCTA2.TA002=SFCTA.TA002 and SFCTA2.TA003 <= reverse(substring(reverse('00'+ cast(CAST(substring(SFCTA.TA003,1,3) as integer)+1 as VARCHAR(4))),1,3))+'0' and SFCTA.TA003<SFCTA2.TA003 order by SFCTA2.TA003)"

        sSQL2 = " (select top 1 rtrim(SFCTA3.TA004)+'-'+CMSMW.MW002 from " & VarIni.ERP & "..SFCTA SFCTA3 " &
                " left join " & VarIni.ERP & "..CMSMW on CMSMW.MW001=SFCTA3.TA004 " &
                " where SFCTA3.TA001=SFCTA.TA001 and SFCTA3.TA002=SFCTA.TA002 " &
                " and SFCTA3.TA003 <= reverse(substring(reverse('00'+ cast(CAST(substring(SFCTA.TA003,1,3) as integer)+1 as VARCHAR(4))),1,3))+'0' and SFCTA.TA003<SFCTA3.TA003 order by SFCTA3.TA003)"

        SQL = " select rtrim(SFCTA.TA006)+'-'+SFCTA.TA007 as WC,rtrim(COPTC.TC004) as 'Customers',COPMA.MA002 as 'Customers Name'," &
              " MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO'," &
              " SFCTA.TA001+'-'+SFCTA.TA002+'-'+SFCTA.TA003 as 'MO', MOCTA.TA035 as 'Item Spec', " &
              " cast(MOCTA.TA015 as decimal(15,2)) as 'Plan Qty', " &
              " cast(SFCTA.TA010+SFCTA.TA016-SFCTA.TA011-SFCTA.TA015-SFCTA.TA048 as decimal(15,2)) as 'WIP Qty', " &
              " (SUBSTRING(SFCTA.TA008,7,2)+'/'+SUBSTRING(SFCTA.TA008,5,2)+'/'+SUBSTRING(SFCTA.TA008,1,4)) as 'Plan Start Date', " &
              " " & " T.TC001+'-'+T.TC002 as 'Transfer No.'," &
              " (SUBSTRING(T.TB003,7,2)+'/'+SUBSTRING(T.TB003,5,2)+'/'+SUBSTRING(T.TB003,1,4)) as 'Receive Date.'," &
              " T.TB014 as 'AP100(Minute)','' as 'Set Plan Date'," &
              " isnull(" & sSQL1 & ",MOCTA.TA020) as 'Next WC'," &
              " isnull(" & sSQL2 & ",MOCTA.TA020) as 'Next Process'," &
              " '' as 'Remark',(SUBSTRING(MOCTA.TA006,1,14)+'-'+SUBSTRING(MOCTA.TA006,15,2)) as 'Item Code' " &
              " from " & tempTable & " T " &
              " left join " & VarIni.ERP & "..SFCTA on SFCTA.TA001=T.TA001 and SFCTA.TA002=T.TA002 and SFCTA.TA003=T.TA003" &
              " left join " & VarIni.ERP & "..MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " &
              " left join " & VarIni.ERP & "..COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " &
              " left join " & VarIni.ERP & "..COPMA on COPMA.MA001 = COPTC.TC004  " &
              " order by T.TA008,SFCTA.TA008,T.TC002,T.TC001,T.TC003 "
        Dim gvCont As New GridviewControl

        gvCont.ShowGridView(gvShow, SQL, VarIni.DBMIS)
        ucRowCount.RowCount = gvCont.rowGridview(gvShow)
        btExportExcel.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExportExcel.Click
        Dim expCont As New ExportImportControl
        expCont.Export("DailyControlPlan" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
                Dim mchTime As Integer = 0
                If Integer.TryParse(.DataItem("AP100(Minute)").ToString.Trim, mchTime) Then
                    mchTime = mchTime
                End If
                If line = 0 Then
                    bCapa = CInt(lbCapa.Text.Trim)
                    If tbPlanDate.Text <> "" Then
                        planStard = DateTime.ParseExact(dateCont.dateFormat2(tbPlanDate.Text.Trim), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                    End If
                End If
                bCapa = bCapa - mchTime
                line = line + 1
                e.Row.Cells(14).Text = planStard.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture) 'bCapa & "," &
                If bCapa < 0 Then
                    Dim xCapa As Integer = CInt(lbCapa.Text.Trim)
                    Dim lastCapa As Integer = Math.Abs(bCapa)
                    Dim modDate As Integer = lastCapa Mod xCapa
                    Dim cntDate As Integer = (lastCapa - modDate) / xCapa
                    If modDate > 0 Then
                        cntDate = cntDate + 1
                    End If
                    planStard = planStard.AddDays(cntDate)
                    bCapa = CInt(lbCapa.Text.Trim) - modDate
                End If
            End If
        End With
    End Sub
End Class