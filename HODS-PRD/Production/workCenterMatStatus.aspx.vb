Public Class workCenterMatStatus
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim creatTemTable As New CreateTempTable
    Dim wcList As String = "'W01','W02','W04','W05','W06','W07','W12','W14','W23','W25','W27'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MD001,MD001+':'+MD002 as MD002 from CMSMD where MD001 in (" & wcList & ") order by MD001 "
            ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString)

        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = ""

        Dim TempTable As String = "TempWCGetMO" & Session("UserName")
        creatTemTable.createTempWCGetMO(TempTable)

        WHR = Conn_SQL.Where("S1.TA006", ddlWC)
        WHR = WHR & Conn_SQL.Where("S1.TA008", configDate.dateFormat2(tbDateFM.Text.Trim), configDate.dateFormat2(tbDateTO.Text.Trim))
        WHR = WHR & Conn_SQL.Where("M1.TA006", tbProduceItem)
        WHR = WHR & Conn_SQL.Where("M1.TA035", tbProduceSpec)
        Dim con As String = ddlCon.Text.Trim
        If con = "2" Then 'on process
            WHR = WHR & " and S1.TA010+S1.TA013+S1.TA016-S1.TA011-S1.TA012-S1.TA014-S1.TA015-S1.TA048-S1.TA056-S1.TA058>0 "
        ElseIf con = "3" Then 'on pending
            WHR = WHR & " and S1.TA010+S1.TA013+S1.TA016-S1.TA011-S1.TA012-S1.TA014-S1.TA015-S1.TA048-S1.TA056-S1.TA058=0 "
        End If

        SQL = " select distinct S1.TA001,S1.TA002,S1.TA006,M1.TA015+S1.TA013+S1.TA016-S1.TA011-S1.TA012-S1.TA014-S1.TA015-S1.TA048-S1.TA056-S1.TA058 from SFCTA S1 left join MOCTA M1 on M1.TA001=S1.TA001 and M1.TA002=S1.TA002 where M1.TA013='Y' and M1.TA011 not in ('Y','y') and S1.TA032='N' " & WHR & " "
        SQL = "insert into DBMIS.." & TempTable & "(moType,moNo,wc,moQty) " & SQL
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.ERP_ConnectionString)

        WHR = Conn_SQL.Where("substring(TB003,3,1)", cblMatType)
        WHR = WHR & Conn_SQL.Where("TB003", tbMatItem)
        WHR = WHR & Conn_SQL.Where("TB013", tbMatSpec)

        If ddlReport.Text = "1" Then 'summary
            Dim TempTable2 As String = "TempWCMatStatusSum" & Session("UserName")
            creatTemTable.createTempWCMatStatusSum(TempTable2)
            SQL = " select TB003,MAX(wc),sum(moQty),sum(TB004),SUM(TB004-TB005) " & _
                  " from DBMIS.." & TempTable & " T  " & _
                  " left join MOCTB on TB001=T.moType and TB002=T.moNo " & _
                  " left join MOCTA on TA001=TB001 and TA002=TB002 " & _
                  " where 1=1  " & WHR & " group by TB003 "
            SQL = "insert into DBMIS.." & TempTable2 & "(item,wc,moQty,reqQty,notQty) " & SQL
            Conn_SQL.Exec_Sql(SQL, Conn_SQL.ERP_ConnectionString)

            SQL = " select MD002 'WC Name', cast(moQty as decimal(15,2)) 'MO Bal',T.item 'Mat Item',MB003 'Mat Spec',MB002 'Mat Desc',MB004 'Unit', " & _
                  " MB017 'WH',cast(MB064 as decimal(15,3)) 'Stock',cast(reqQty as decimal(15,3)) 'MO Req.',cast(notQty as decimal(15,3)) 'Not Issue'" & _
                  " from DBMIS.." & TempTable2 & " T  " & _
                  " left join INVMB on MB001=T.item " & _
                  " left join CMSMD on MD001=wc " & _
                  " order by T.item "
        Else 'detail
            SQL = " select MD002 'WC Name',MA002 'Cust Name', TB001+'-'+TB002 'MO No.',TA006 'Item',TA035 'Spec'," & _
                  "        TB003 'Mat Item',TB013 'Mat Spec',TB012 'Mat Desc',TB007 'Unit',TB009 'WH',cast(TB004 as decimal(15,3)) 'Req'," & _
                  "        cast(TB004-TB005 as decimal(15,3)) 'Not Issue',(select top 1 A.TA008 from SFCTA A left join MOCTA B on B.TA001=A.TA001 and B.TA002=A.TA002 where A.TA001=T.moType and A.TA002=T.moNo and B.TA013='Y' and B.TA011 not in ('Y','y') and A.TA011+A.TA012+A.TA056+A.TA048<B.TA015 order by A.TA003) 'Plan Start'," & _
                  "        case TA011 when '1' then 'Not Produce' when '2' then 'Issue' when '3' then 'Producing' else 'MO Complete' end 'MO Status' " & _
                  " from DBMIS.." & TempTable & " T  " & _
                  " left join MOCTB on TB001=T.moType and TB002=T.moNo " & _
                  " left join MOCTA on TA001=TB001 and TA002=TB002 " & _
                  " left join COPTC on TC001=TA026 and TC002=TA027 " & _
                  " left join COPMA on MA001=TC004 " & _
                  " left join CMSMD on MD001=wc " & _
                  " where 1=1  " & WHR & " order by TC004,TB001,TB002 "
            'Dim aa As String = ""
        End If
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("workLoading" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class