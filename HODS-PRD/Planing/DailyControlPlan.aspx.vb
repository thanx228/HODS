Public Class DailyControlPlan
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim bCapa As Integer = 0,
        line As Integer = 0,
        planStard As Date = DateTime.ParseExact(Date.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
        wcList As String = "'W01','W02','W23','W27'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD where MD001 not in (" & wcList & ") order by MD001 "
            ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            'ControlForm.showDDL(ddlMoType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            ControlForm.showCheckboxList(cblMoType, SQL, "MQ002", "MQ001", 4, Conn_SQL.ERP_ConnectionString)


            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            btExportExcel.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
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
            sSQL3 As String = "",
            sSQL4 As String = "",
            iSQL As String = "",
            WHR As String = "",
            WHR2 As String = "",
            conStr As String = ""
        Dim xSql As String = "select capacity from MachineCapacity where wc='" & ddlWC.Text.Trim & "' "
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(xSql, Conn_SQL.MIS_ConnectionString)
        lbCapa.Text = Program.Rows(0).Item("capacity")

        WHR = WHR & Conn_SQL.Where("MOCTA.TA006", tbCode)
        WHR = WHR & Conn_SQL.Where("MOCTA.TA035", tbSpec)
        WHR = WHR & Conn_SQL.Where("SFCTA.TA001", cblMoType)
        WHR = WHR & Conn_SQL.Where("MOCTA.TA026", ddlSaleType)
        WHR = WHR & Conn_SQL.Where("MOCTA.TA035", tbCust)

        If ddlDate.Text.Trim = "1" Then
            WHR = WHR & Conn_SQL.Where("SFCTA.TA006", ddlWC)
            WHR = WHR & Conn_SQL.Where("SFCTA.TA008", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
            'genTempMO(tempTable, WHR)

            sSQL = " (select top 1 floor((BOMMF.MF024+(BOMMF.MF025*(SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)))/60) from BOMMF " & _
                   " where BOMMF.MF001=MOCTA.TA006 and BOMMF.MF003=SFCTA.TA003 and BOMMF.MF004=SFCTA.TA004 and BOMMF.MF006=SFCTA.TA006" & _
                   " order by BOMMF.MF002)"

            sSQL1 = " (select top 1 rtrim(SFCTA2.TA006)+'-'+SFCTA2.TA007 from SFCTA SFCTA2 " & _
                    " where SFCTA2.TA001=SFCTA.TA001 and SFCTA2.TA002=SFCTA.TA002 " & _
                    " and SFCTA2.TA003 <= reverse(substring(reverse('00'+ cast(CAST(substring(SFCTA.TA003,1,3) as integer)+1 as VARCHAR(4))),1,3))+'0' and SFCTA.TA003<SFCTA2.TA003 order by SFCTA2.TA003)"

            sSQL2 = " (select top 1 rtrim(SFCTA3.TA004)+'-'+CMSMW.MW002 from SFCTA SFCTA3 " & _
                    " left join JINPAO80.dbo.CMSMW on CMSMW.MW001=SFCTA3.TA004 " & _
                    " where SFCTA3.TA001=SFCTA.TA001 and SFCTA3.TA002=SFCTA.TA002 " & _
                    " and SFCTA3.TA003 <= reverse(substring(reverse('00'+ cast(CAST(substring(SFCTA.TA003,1,3) as integer)+1 as VARCHAR(4))),1,3))+'0' and SFCTA.TA003<SFCTA3.TA003 order by SFCTA3.TA003)"

            sSQL3 = " (select top 1 (SUBSTRING(SFCTB.TB003,7,2)+'/'+SUBSTRING(SFCTB.TB003,5,2)+'/'+SUBSTRING(SFCTB.TB003,1,4)) from SFCTC " & _
                    " left join SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
                    " where SFCTC.TC004=SFCTA.TA001 and SFCTC.TC005=SFCTA.TA002 " & _
                    " and SFCTC.TC008=SFCTA.TA003 and SFCTB.TB008=SFCTA.TA006 " & _
                    " order by SFCTB.TB003 desc) "

            sSQL4 = " (select top 1 SFCTC1.TC001+'-'+SFCTC1.TC002+'-'+SFCTC1.TC003 from SFCTC SFCTC1 " & _
                    " left join SFCTB SFCTB1 on SFCTB1.TB001=SFCTC1.TC001 and SFCTB1.TB002=SFCTC1.TC002 " & _
                    " where SFCTC1.TC004=SFCTA.TA001 and SFCTC1.TC005=SFCTA.TA002 " & _
                    "   and SFCTC1.TC008=SFCTA.TA003 and SFCTB1.TB008=SFCTA.TA006 " & _
                    " order by SFCTB1.TB003 desc) "

            SQL = " select rtrim(SFCTA.TA006)+'-'+SFCTA.TA007 as WC,rtrim(COPTC.TC004) as 'Customers',COPMA.MA002 as 'Customers Name'," & _
                  " MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO'," & _
                  " SFCTA.TA001+'-'+SFCTA.TA002+'-'+SFCTA.TA003 as 'MO', MOCTA.TA035 as 'Item Spec', " & _
                  " cast(MOCTA.TA015 as decimal(15,2)) as 'Plan Qty', " & _
                  " cast(SFCTA.TA010+SFCTA.TA016-SFCTA.TA011-SFCTA.TA015-SFCTA.TA048 as decimal(15,2)) as 'WIP Qty', " & _
                  " (SUBSTRING(SFCTA.TA008,7,2)+'/'+SUBSTRING(SFCTA.TA008,5,2)+'/'+SUBSTRING(SFCTA.TA008,1,4)) as 'Plan Start Date', " & _
                  sSQL3 & " as 'Receive Date.'," & sSQL4 & " as 'Transfer Number'," & _
                  " isnull(" & sSQL & ",0) as 'Machine(Minute)', '' as 'Set Plan Date'," & _
                  " isnull(" & sSQL1 & ",MOCTA.TA020) as 'Next WC'," & _
                  " isnull(" & sSQL2 & ",MOCTA.TA020) as 'Next Process'," & _
                  " '' as 'Remark',(SUBSTRING(MOCTA.TA006,1,14)+'-'+SUBSTRING(MOCTA.TA006,15,2)) as 'Item Code' " & _
                  " from SFCTA " & _
                  " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
                  " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " & _
                  " left join COPMA on COPMA.MA001 = COPTC.TC004  " & _
                  " where MOCTA.TA013='Y' and SFCTA.TA005='1' " & _
                  " and SFCTA.TA010+SFCTA.TA016-SFCTA.TA011-SFCTA.TA015-SFCTA.TA048>0 " & _
                  " and SFCTA.TA032 = 'N' and MOCTA.TA011 not in('y','Y') " & WHR & _
                  " order by SFCTA.TA008,SFCTA.TA002,SFCTA.TA001,SFCTA.TA003 "
            conStr = Conn_SQL.ERP_ConnectionString
        Else
            Dim tempTable As String = "tempDailyControlPlan" & Session("UserName")
            CreateTempTable.createTempMO2(tempTable)
            WHR = WHR & Conn_SQL.Where("SFCTB.TB008", ddlWC)
            WHR = WHR & Conn_SQL.Where("SFCTB.TB003", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))

            SQL = " select SFCTA.TA001,SFCTA.TA002,SFCTA.TA003,SFCTC.TC001,SFCTC.TC002,SFCTC.TC003,SFCTB.TB003 " & _
                  " from SFCTC " & _
                  " left join SFCTA on TA001=TC004 and TA002=TC005 and TA003=TC008 " & _
                  " left join SFCTB on TB001=TC001 and TB002=TC002 " & _
                  " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
                  " where TB007='1' and MOCTA.TA013='Y' " & _
                  " and SFCTA.TA032 = 'N' and MOCTA.TA011 not in('y','Y') " & WHR & _
                  " and SFCTA.TA010+SFCTA.TA016-SFCTA.TA011-SFCTA.TA015-SFCTA.TA048>0 " & _
                  " order by TA001,TA002,TA003,TB003 desc "
            Dim lastData As String = ""
            Dim cnt As Integer = 1
            Program = New DataTable
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    Dim selData As String = .Item("TA001") & .Item("TA002") & .Item("TA003") & .Item("TC001") & .Item("TC003") & .Item("TC003")
                    If lastData <> selData And cnt = 1 Then
                        Dim fldInsHash As Hashtable = New Hashtable,
                            whrHash As Hashtable = New Hashtable
                        whrHash.Add("TA001", .Item("TA001")) ' Trn Type
                        whrHash.Add("TA002", .Item("TA002")) ' Trn Number
                        whrHash.Add("TA003", .Item("TA003")) ' Trn Seq
                        'insert Zone
                        fldInsHash.Add("TC001", .Item("TC001")) ' MO Type
                        fldInsHash.Add("TC002", .Item("TC002")) ' MO Number
                        fldInsHash.Add("TC003", .Item("TC003")) ' MO seq
                        fldInsHash.Add("TB003", .Item("TB003")) ' Transfer date
                        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                        cnt = 1
                    End If
                    cnt = cnt + 1
                    lastData = selData
                End With
            Next

            sSQL = "(select top 1 floor((MF024+(MF025*(SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)))/60) from JINPAO80.dbo.BOMMF" & _
                   " where MF001=MOCTA.TA006 and MF003=SFCTA.TA003 and MF004=SFCTA.TA004 and MF006=SFCTA.TA006" & _
                   " order by MF002)"

            sSQL1 = " (select top 1 rtrim(SFCTA2.TA006)+'-'+SFCTA2.TA007 from JINPAO80.dbo.SFCTA SFCTA2 " & _
                    " where SFCTA2.TA001=SFCTA.TA001 and SFCTA2.TA002=SFCTA.TA002 " & _
                    " and SFCTA2.TA003 <= reverse(substring(reverse('00'+ cast(CAST(substring(SFCTA.TA003,1,3) as integer)+1 as VARCHAR(4))),1,3))+'0' and SFCTA.TA003<SFCTA2.TA003 order by SFCTA2.TA003)"

            sSQL2 = " (select top 1 rtrim(SFCTA3.TA004)+'-'+CMSMW.MW002 from JINPAO80.dbo.SFCTA SFCTA3 " & _
                    " left join JINPAO80.dbo.CMSMW on CMSMW.MW001=SFCTA3.TA004 " & _
                    " where SFCTA3.TA001=SFCTA.TA001 and SFCTA3.TA002=SFCTA.TA002 " & _
                    " and SFCTA3.TA003 <= reverse(substring(reverse('00'+ cast(CAST(substring(SFCTA.TA003,1,3) as integer)+1 as VARCHAR(4))),1,3))+'0' and SFCTA.TA003<SFCTA3.TA003 order by SFCTA3.TA003)"

            SQL = " select rtrim(SFCTA.TA006)+'-'+SFCTA.TA007 as WC,rtrim(COPTC.TC004) as 'Customers',COPMA.MA002 as 'Customers Name'," & _
                  " MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO'," & _
                  " SFCTA.TA001+'-'+SFCTA.TA002+'-'+SFCTA.TA003 as 'MO', MOCTA.TA035 as 'Item Spec', " & _
                  " cast(MOCTA.TA015 as decimal(15,2)) as 'Plan Qty', " & _
                  " cast(SFCTA.TA010+SFCTA.TA016-SFCTA.TA011-SFCTA.TA015-SFCTA.TA048 as decimal(15,2)) as 'WIP Qty', " & _
                  " (SUBSTRING(SFCTA.TA008,7,2)+'/'+SUBSTRING(SFCTA.TA008,5,2)+'/'+SUBSTRING(SFCTA.TA008,1,4)) as 'Plan Start Date', " & _
                  " " & " T.TC001+'-'+T.TC002 as 'Transfer No.'," & _
                  " (SUBSTRING(T.TB003,7,2)+'/'+SUBSTRING(T.TB003,5,2)+'/'+SUBSTRING(T.TB003,1,4)) as 'Receive Date.'," & _
                  " isnull(" & sSQL & ",0) as 'Machine(Minute)', '' as 'Set Plan Date'," & _
                  " isnull(" & sSQL1 & ",MOCTA.TA020) as 'Next WC'," & _
                  " isnull(" & sSQL2 & ",MOCTA.TA020) as 'Next Process'," & _
                  " '' as 'Remark',(SUBSTRING(MOCTA.TA006,1,14)+'-'+SUBSTRING(MOCTA.TA006,15,2)) as 'Item Code' " & _
                  " from " & tempTable & " T " & _
                  " left join JINPAO80.dbo.SFCTA on SFCTA.TA001=T.TA001 and SFCTA.TA002=T.TA002 and SFCTA.TA003=T.TA003" & _
                  " left join JINPAO80.dbo.MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
                  " left join JINPAO80.dbo.COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " & _
                  " left join JINPAO80.dbo.COPMA on COPMA.MA001 = COPTC.TC004  " & _
                  " order by T.TA008,T.TC002,T.TC001,T.TC003 "
            conStr = Conn_SQL.MIS_ConnectionString
        End If
        ControlForm.ShowGridView(gvShow, SQL, conStr)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExportExcel.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Function returnFld(ByVal fldName As String, ByVal fldCall As String) As String
        Return "CONVERT(varchar, floor(" & fldName & "/60)) as " & fldCall & ","
    End Function

    Protected Sub btExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExportExcel.Click
        ControlForm.ExportGridViewToExcel("DailyControlPlan" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
                Dim mchTime As Integer = .DataItem("Machine(Minute)")
                If line = 0 Then
                    bCapa = CInt(lbCapa.Text.Trim)
                    If tbPlanDate.Text <> "" Then
                        planStard = DateTime.ParseExact(configDate.dateFormat2(tbPlanDate.Text.Trim), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
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

    'Protected Function getWhr(Optional ByVal showStatus As Boolean = True) As String
    '    Dim wc As String = ddlWC.Text,
    '        code As String = tbCode.Text,
    '        spec As String = tbSpec.Text,
    '        whr As String = "",
    '        moType As String = ddlMoType.Text,
    '         cust As String = tbCust.Text.Trim
    '    'If selProperty = "2" Then
    '    '    wc = tbWC.Text
    '    'End If

    '    If wc <> "ALL" Then
    '        whr = whr & " and SFC.TA006='" & wc & "' "
    '    Else
    '        whr = whr & " and SFC.TA006 not in ('" & wcList & "') "
    '    End If

    '    If code <> "" Then
    '        whr = whr & " and MOC.TA006 like '%" & code & "%' "
    '    End If
    '    If spec <> "" Then
    '        whr = whr & " and MOC.TA035 like '%" & spec & "%' "
    '    End If
    '    whr = whr & " and SFC.TA001 not in('5195') "
    '    If moType <> "ALL" Then
    '        whr = whr & " and SFC.TA001 = '" & moType & "' "
    '    End If

    '    Dim saleType As String = ddlSaleType.Text
    '    If saleType <> "ALL" Then
    '        whr = whr & " and MOC.TA026='" & saleType & "' "
    '    End If
    '    If cust <> "" Then
    '        whr = whr & " and COPTC.TC004 = '" & cust.ToUpper() & "' "
    '    End If
    '    whr = whr & configDate.DateWhere("SFC.TA008", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
    '    Return whr
    'End Function
End Class