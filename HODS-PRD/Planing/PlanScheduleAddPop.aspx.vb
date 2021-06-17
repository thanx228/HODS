Public Class PlanScheduleAddPop
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim whr As String = "",
                SQL As String = "",
                sSQL1 As String,
                sSQL2 As String,
                item As String = Request.QueryString("item").Trim
            Dim mo() As String = Request.QueryString("mo").Trim.Split("-")
            Dim moType As String = "",
                moNo As String = ""

            If mo.Length > 1 Then
                moType = mo(0)
                moNo = mo(1)
                lbMO.Text = moType & "-" & moNo
                SQL = " select TA015,TA035,rtrim(TC004)+'-'+MA002 MA002  from MOCTA left join COPTC on TC001 = TA026 and TC002 = TA027 " & _
                      " left join COPMA on MA001 = TC004 where TA001='" & moType & "' and TA002='" & moNo & "' "
                Dim Program As New Data.DataTable
                Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                If Program.Rows.Count > 0 Then
                    With Program.Rows(0)
                        lbSpec.Text = .Item("TA035").ToString.Trim
                        lbMoQty.Text = CInt(.Item("TA015")).ToString("###0")
                        lbCust.Text = Trim(.Item("MA002"))
                    End With
                End If
                sSQL1 = "select sum(MC007) from INVMC where MC001=M.TB003 and MC002 in('2201','2204','2205','2206','2207','2300','2400','2900','2901','3000','3100','3200','3300','3333','3400','3500','3600','3700','3800','3900')"
                sSQL2 = "select sum(TB004-TB005) from MOCTB M1 left join MOCTA M2 on M2.TA001=M1.TB001 and M2.TA002=M1.TB002 where M1.TB003=M.TB003 and M2.TA013 = 'Y' and M2.TA011 not in ('y','Y') and TB004-TB005 >0 "
                SQL = "select M.TB003,M.TB012,M.TB013,M.TB004,M.TB005,M.TB004-M.TB005 TB0045,M.TB007,M.TB009,(" & sSQL1 & ") STOCK,(" & sSQL2 & ") ISSUE " & _
                      " from MOCTB M where M.TB001='" & moType & "' and M.TB002='" & moNo & "' order by M.TB003 "
                ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
                'lbCount.Text = gvMO.Rows.Count
                ucCountRow1.RowCount = ControlForm.rowGridview(gvMO)
            Else
                SQL = "select MB003 from INVMB where MB001='" & item & "' "
                Dim Program As New Data.DataTable
                Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                If Program.Rows.Count > 0 Then
                    With Program.Rows(0)
                        lbSpec.Text = .Item("MB003").ToString.Trim
                    End With
                End If
            End If

            Dim nameHead As String = ControlForm.nameHeader("/Planing/PlanScheduleList.aspx") & "(Pop Up)"
            'ucHeaderForm.HeaderLable = nameHead
            'Page.Title = "ERP REPORT->" & nameHead

            'whr = Conn_SQL.Where("", "")
            Dim colName As ArrayList = New ArrayList
            colName.Add("Seq:TA003")
            colName.Add("Opt Desc:MW002")
            colName.Add("Work Center:TA007")
            colName.Add("Input Qty:TA010:0")
            colName.Add("Complete Qty:TA011:0")
            colName.Add("Scrap Qty:TA012:0")
            colName.Add("WIP Qty:wipQty:0")
            colName.Add("Wait Trans Qty:TA017:0")
            colName.Add("Re-Mo Issue:TA013:0")
            colName.Add("Re-MO Complete:TA014:0")
            colName.Add("Destroy Qty:TA048:0")
            colName.Add("Plan Comp Date:TA009")

            If mo.Length > 1 Then
                Dim tableTemp As String = "tempMOPLAN" & Session("UserName")
                Dim dateList As ArrayList = genTemp(tableTemp, moType, moNo)
                Dim fld As String = ""
                Dim fldAll As String = "0"
                colName.Add("BAL PLAN:BP:0")
                For Each str As String In dateList
                    Dim dd As String = str.Substring(6, 2),
                        mm As String = str.Substring(4, 2),
                        yy As String = str.Substring(0, 4),
                        fldName As String = "Plan (" & dd & "-" & mm & If(Date.Today.Year.ToString() = yy, "", "-" & yy) & ")",
                        fldData As String = "plan" & str

                    fld &= ",isnull(T." & fldData & ",0) " & fldData
                    fldAll &= "+isnull(T." & fldData & ",0) "
                    colName.Add(fldName & ":" & fldData & ":0")
                    'fldName = "Bal (" & dd & "-" & mm & If(Date.Today.Year.ToString() = yy, "", "-" & yy) & ")"
                    'fldData = "bal" & str
                    'fld &= ",T." & fldData
                    'colName.Add(fldName & ":" & fldData & ":0")
                Next
                fld = "," & CDec(lbMoQty.Text.Trim) & "+TA013-(" & fldAll & ") BP" & fld
                SQL = " select TA003,MW002,TA007,TA010,TA011,TA012+TA056 TA012,TA017,TA013,TA014,TA048," & _
                      " TA010+TA013+TA016-TA011-TA012-TA014-TA015-TA048-TA056-TA058 wipQty," & _
                      " substring(TA009,7,2)+'-'+substring(TA009,5,2)+'-'+substring(TA009,1,4) TA009 " & fld & _
                      " from SFCTA left join CMSMW on MW001=TA004 " & _
                      " left join " & Conn_SQL.DBReport & ".." & tableTemp & " T on T.moType=TA001  and T.moNo=TA002 and T.moSeq=TA003 " & _
                      " where TA001='" & moType & "' and TA002='" & moNo & "' order by TA001,TA002,TA003 "
            Else
                SQL = " select SFC.TA001+'-'+SFC.TA002+'-'+SFC.TA003 TA003,MW002,SFC.TA007,SFC.TA010,SFC.TA011,SFC.TA012+SFC.TA056 TA012,SFC.TA017,SFC.TA013,SFC.TA014,SFC.TA048," & _
                      " SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058 wipQty," & _
                      " substring(SFC.TA009,7,2)+'-'+substring(SFC.TA009,5,2)+'-'+substring(SFC.TA009,1,4) TA009 " & _
                      " from SFCTA SFC left join MOCTA MOC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002  left join CMSMW on MW001=SFC.TA004 " & _
                      " where MOC.TA006='" & item & "' and MOC.TA011 not in ('Y','y') order by SFC.TA001,SFC.TA002,SFC.TA003 "
            End If
            ControlForm.GridviewColWithLinkFirst(gvOperation, colName, False)
            ControlForm.ShowGridView(gvOperation, SQL, Conn_SQL.ERP_ConnectionString)
            ucCountRow2.RowCount = ControlForm.rowGridview(gvOperation)

            'SQL = " select TA003,MW002,TA007,TA010,TA011,TA012+TA056 TA012,TA017,TA013,TA014,TA048," & _
            '      " TA010+TA013+TA016-TA011-TA012-TA014-TA015-TA048-TA056-TA058 wipQty " & _
            '      " from SFCTA left join CMSMW on MW001=TA004 where TA001='" & moType & "' and TA002='" & moNo & "' order by TA001,TA002,TA003 "
            'ControlForm.ShowGridView(gvOperation, SQL, Conn_SQL.ERP_ConnectionString)
            ''lbCount0.Text = gvOperation.Rows.Count
            'ucCountRow2.RowCount = ControlForm.rowGridview(gvOperation)

            Dim visible As Boolean = False
            If mo.Length = 3 Then
                'showDataNew(mo(0).Trim, mo(1).Trim, mo(2).Trim)

                'showData(" and P.moType='" & mo(0).Trim & "' and P.moNo='" & mo(1).Trim & "' and P.moSeq='" & mo(2).Trim & "' ")
                visible = True
                nameHead = "MO Check Status Plan TV(Pop Up)"
            End If
            ucHeaderForm.HeaderLable = nameHead
            Page.Title = "ERP REPORT->" & nameHead
            ucCountRow3.Visible = visible

        End If
    End Sub

    Private Function genTemp(tableTemp As String, moType As String, moNo As String) As ArrayList
        Dim SQL As String,
            dt As DataTable,
            colCount As Integer = 0,
            dateList As ArrayList = New ArrayList
        'SQL = "select top 1 TA001,TA002,TA003,COUNT(DISTINCT  PlanDate) from PlanSchedule where TA001='" & moType & "' and TA002='" & moNo & "' and Cancled=0 group by TA001,TA002,TA003 order by COUNT(*) desc"
        SQL = "select DISTINCT PlanDate from PlanSchedule where TA001='" & moType & "' and TA002='" & moNo & "' and Cancled=0 order by PlanDate "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If dt.Rows.Count > 0 Then
            With dt
                colCount = .Rows.Count
                For i As Integer = 0 To .Rows.Count - 1
                    With .Rows(i)
                        dateList.Add(.Item(0))
                    End With
                Next
            End With
        End If
        If colCount > 0 Then
            CreateTempTable.createTempMOPLAN(tableTemp, dateList)
            Dim Qty As Decimal = 0,
                bal As Decimal = 0
            SQL = "select TA015 from MOCTA where TA001='" & moType & "' and TA002='" & moNo & "' "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            If dt.Rows.Count > 0 Then
                Qty = CDec(dt.Rows(0).Item(0))
            End If

            SQL = "select TA001,TA002,TA003,PlanDate,sum(PlanQty) from PlanSchedule where TA001='" & moType & "' and TA002='" & moNo & "' and Cancled=0 group by TA001,TA002,TA003,PlanDate order by TA001,TA002,TA003,PlanDate"
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            Dim lastSeq As String = "",
                    fldHash As Hashtable,
                    whrHash As Hashtable,
                    strSQL As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    Dim seq As String = Trim(.Item(2))
                    If seq <> lastSeq Then
                        If lastSeq <> "" Then
                            strSQL &= Conn_SQL.GetSQL(tableTemp, fldHash, whrHash, "I")
                        End If
                        'bal = Qty
                        fldHash = New Hashtable
                        whrHash = New Hashtable
                        whrHash.Add("moType", moType)
                        whrHash.Add("moNo", moNo)
                        whrHash.Add("moSeq", seq)
                    End If
                    Dim pd As String = Trim(.Item(3)),
                        pq As Decimal = CDec(.Item(4))
                    fldHash.Add("plan" & pd, pq)
                    'bal -= pq
                    'fldHash.Add("bal" & pd, bal)
                    lastSeq = seq
                End With
            Next
            If fldHash.Count > 0 And whrHash.Count > 0 Then
                strSQL &= Conn_SQL.GetSQL(tableTemp, fldHash, whrHash, "I")
            End If
            Conn_SQL.Exec_Sql(strSQL, Conn_SQL.MIS_ConnectionString)
        End If
        Return dateList
    End Function

    'Private Sub showDataNew(moType As String, moNumber As String, moSeq As String)
    '    Dim PU As New process_updates,
    '        SFCTA As New SFCTA,
    '        CMSMW As New CMSMW,
    '        MCH As New machines

    '    Dim fld As String = "",
    '        col As New ArrayList

    '    col.Add("MO Seq:moSeq")
    '    col.Add("MO Process:moProcess")
    '    col.Add("Machine:Mch")
    '    col.Add("Work Type:wType")
    '    col.Add("Start Time:sTime")
    '    col.Add("End Time:eTime")
    '    col.Add("Usage Time:uTime:0")
    '    col.Add("Accept Qty:aQty:0")
    '    col.Add("Return Qty:rQty:0")
    '    col.Add("Scrap Qty:sQty:0")
    '    col.Add("Man Power:manPower:0")
    '    Dim df As String = "%Y-%m-%d %H:%i"
    '    fld = " case when ifnull(setup_start_time,'')='' then '' else  date_format(setup_start_time,'" & df & "') end setup_start_time," &
    '          " case when ifnull(setup_end_time,'')='' then '' else  date_format(setup_end_time,'" & df & "') end setup_end_time," &
    '          " case when ifnull(start_time,'')='' then '' else  date_format(start_time,'" & df & "') end start_time," &
    '          " case when ifnull(end_time,'')='' then '' else  date_format(end_time,'" & df & "') end end_time," &
    '          " process_workcenter,machine,ifnull(setup_time,0)/60 setup_time,ifnull(update_time,0)/60 update_time," &
    '          " processed_qty,ifnull(return_qty,0) return_qty,ifnull(scrap_qty,0) scrap_qty," &
    '          " ifnull(setup_operator,'') setup_operator,ifnull(operator_id,'') operator_id "

    '    ControlForm.GridviewInitial(gvStatus, col, False)

    '    Dim whr As String = " And mo_type='" & moType & "' and mo_number='" & moNumber & "' and seq='" & moSeq & "' "
    '    Dim showProcess As String = CMSMW.getValName(SFCTA.getOperationCode(moType, moNumber, moSeq))
    '    Dim dt As DataTable = PU.getData(whr, fld)
    '    Dim dtShow As DataTable = ControlForm.setColDatatable(col)
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        With dt.Rows(i)
    '            'main table select 
    '            Dim mchName As String = MCH.getMchName(.Item("machine"))
    '            Dim setup_start_time As String = Trim(.Item("setup_start_time"))
    '            Dim setup_end_time As String = Trim(.Item("setup_end_time"))
    '            Dim start_time As String = Trim(.Item("start_time"))
    '            Dim end_time As String = Trim(.Item("end_time"))
    '            If setup_start_time <> "" Or setup_end_time <> "" Then
    '                Dim setupCount As Decimal = Trim(.Item("setup_operator")).Split("  ").Count
    '                Dim setup_time As Decimal = .Item("setup_time")

    '                dtShow.Rows.Add(New Object() {moSeq,
    '                                              showProcess,
    '                                              mchName,
    '                                              "Set Up",
    '                                              setup_start_time,
    '                                              setup_end_time,
    '                                              setup_time,
    '                                              0,
    '                                              0,
    '                                              0,
    '                                              setupCount})
    '            End If
    '            If start_time <> "" Or end_time <> "" Then
    '                Dim update_time As Decimal = .Item("update_time")
    '                Dim aQty As Decimal = .Item("processed_qty")
    '                Dim rQty As Decimal = .Item("return_qty")
    '                Dim sQty As Decimal = .Item("scrap_qty")
    '                Dim runCount As Decimal = Trim(.Item("operator_id")).Split("  ").Count

    '                dtShow.Rows.Add(New Object() {moSeq,
    '                                              showProcess,
    '                                              mchName,
    '                                              "Run",
    '                                              start_time,
    '                                              end_time,
    '                                              update_time,
    '                                              aQty,
    '                                              rQty,
    '                                              sQty,
    '                                              runCount})
    '            End If
    '        End With
    '    Next

    '    Dim dv As New DataView(dtShow)
    '    dv.Sort = "sTime ASC"
    '    ControlForm.ShowGridView(gvStatus, dv)
    '    ucCountRow3.RowCount = ControlForm.rowGridview(gvStatus)
    'End Sub


    Private Sub showData(WHR As String)
        Dim colName() As String = {"Doc No:Z", _
                                   "Shift:A",
                                   "Operator:OPER",
                                   "Opt Name:OPERNAME",
                                   "Work Date:B", _
                                   "Job Type:C", _
                                   "MO:D", _
                                   "Process:E", _
                                   "Spec:F", _
                                   "Accept Qty:G:0", _
                                   "Return Qty:H:0", _
                                   "Scrap Qty:I1:0", _
                                   "Scrap Code:I", _
                                   "Work Type:J", _
                                   "Start Time:K", _
                                   "End Time:L", _
                                   "Work Time(Min):M:0", _
                                   "Man Power:N:0", _
                                   "STD Time Man(Min/PC):O:2", _
                                   "STD Time Mch(Min/PC):P:2", _
                                   "W/C:Q", _
                                   "Mach/Line:R", _
                                   "Part Item:S", _
                                   "Process Type:T", _
                                   "Point/Opt:U"}
        Dim SQL As String

        SQL = " select isnull(R1.shift,'') A,R1.dateCode B,case when isnull(R1.isMulti,'')='' then '' else case when isnull(R1.isMulti,'')='N' then 'Single' else 'Multi' end end C," & _
              " rtrim(P.moType)+'-'+rtrim(P.moNo)+'-'+rtrim(P.moSeq) D," & _
              " C.MW002 E,M.TA035 F,isnull(R2.acceptQty,0) G,isnull(R2.defectQty,0) H,isnull(R2.scrapQty,0) I1,isnull(R2.scrapCode,'') I," & _
              " case when substring(R1.isSetTime,1,1)='Y' then 'Setting' else 'Run' end J," & _
              " R1.timeCode K,case when R1.dateCode=isnull(R2.dateCode,'') then isnull(R2.timeCode,'') else rtrim(rtrim(isnull(R2.dateCode,''))+' '+isnull(R2.timeCode,'')) end L, " & _
              " case when substring(isnull(R1.isSetTime,''),1,1)='' or substring(isnull(R1.isSetTime,''),1,1)='Y' then " & _
              " case when P.setTime=0 then 0 else floor(P.setTime/60) end else " & _
              " case when P.workTime=0 then 0 else floor(P.workTime/60) end end M ," & _
              " P.manPower N,R1.wc Q,P.mc R, M.TA006 S," & _
              " case when substring(isnull(R1.isSetTime,''),1,1)='' or substring(isnull(R1.isSetTime,''),1,1)='Y' then " & _
              " case when F.MF009=0 then 0 else floor(F.MF009/60) end else " & _
              " case when F.MF010=0 then 0 else round(F.MF010/60,2) end end O ," & _
              " case when substring(isnull(R1.isSetTime,''),1,1)='' or substring(isnull(R1.isSetTime,''),1,1)='Y' then " & _
              " case when F.MF024=0 then 0 else floor(F.MF024/60) end else " & _
              " case when F.MF025=0 then 0 else round(F.MF025/60,2) end end P ," & _
              " P.docNo Z ,R1.processType T,R1.processCode U ,O.opCode OPER,case when V.MV047='' then V.MV002 else V.MV047 end OPERNAME " & _
              " from ProductionProcessSum P " & _
              " left join ProductionProcessOperator O on O.docStart=P.docStart " & _
              " left join ProductionProcessRecord R1 on R1.docNo=P.docStart  " & _
              " left join ProductionProcessRecord R2 on R2.docNo=P.docEnd " & _
              " left join " & Conn_SQL.DBMain & "..SFCTA S on S.TA001=P.moType and S.TA002=P.moNo and S.TA003=P.moSeq  " & _
              " left join " & Conn_SQL.DBMain & "..MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
              " left join " & Conn_SQL.DBMain & "..CMSMW C on C.MW001=S.TA004 " & _
              " left join " & Conn_SQL.DBMain & "..BOMMF F on F.MF001=M.TA006 and F.MF002='01' and F.MF003=P.moSeq " & _
              " left join " & Conn_SQL.DBMain & "..CMSMV V on V.MV001=O.opCode " & _
              " where 1=1 " & WHR & _
              " order by R1.dateCode,R1.timeCode,P.moType,P.moNo,P.moSeq "

        ControlForm.GridviewColWithLinkFirst(gvStatus, colName, False)
        ControlForm.ShowGridView(gvStatus, SQL, Conn_SQL.MIS_ConnectionString)
        ucCountRow3.RowCount = ControlForm.rowGridview(gvStatus)
        'System.Threading.Thread.Sleep(1000)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Private Sub gvMO_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMO.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("TB003")) Then 'And Not IsDBNull(.DataItem("TA002"))
                    Dim link As String = ""
                    link = link & "&item= " & .DataItem("TB003").ToString.Trim
                    hplDetail.NavigateUrl = "PlanScheduleAddPop.aspx?height=150&width=350&mo=" & link
                    hplDetail.Attributes.Add("title", .DataItem("TB013"))
                End If
            End If
        End With
    End Sub
End Class