Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class PlanMatIssue
    Inherits System.Web.UI.Page
    'Dim ControlForm As New ControlDataForm
    'Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    'Dim configDate As New ConfigDate
    Dim dbConn As New DataConnectControl
    Dim dateCont As New DateControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim ddlCont As New DropDownListControl
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ddlCont.showDDL(ddlSaleType, SQL, VarIni.ERP, "MQ002", "MQ001", True)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ddlCont.showDDL(ddlWorkType, SQL, VarIni.ERP, "MQ002", "MQ001", True)
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempMaterailIssueStatus" & Session("UserName")
        CreateTempTable.createTempPlanIssue(tempTable)
        Dim SQL As String = "",
            WHR As String = "",
            WHR1 As String = "",
            USQL As String = "",
            ISQL As String = "",
            code As String = tbItem.Text.Trim,
            spec As String = tbSpec.Text.Trim,
            saleType As String = ddlSaleType.Text.Trim,
            saleNo As String = tbSaleNo.Text.Trim,
            saleSeq As String = tbSaleSeq.Text.Trim,
            workType As String = ddlWorkType.Text,
            workNoFrom As String = tbWorkNoFrom.Text.Trim,
            workNoTo As String = tbWorkNoTo.Text.Trim,
            workStatus As String = ddlWorkStatus.Text.Trim,
            cust As String = tbCust.Text.Trim,
            delDate As String = dateCont.dateFormat2(tbDelDate.Text.Trim),
            WorkDateFrom As String = dateCont.dateFormat2(tbWorkDateFrom.Text.Trim),
            WorkDateTo As String = dateCont.dateFormat2(tbWorkDateTo.Text.Trim),
            item As String = "",
            Qty As Integer = 0,
            Program As New DataTable



        WHR &= dbConn.WHERE_EQUAL("MOCTA.TA026", ddlSaleType) 'so type
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA027", tbSaleNo) 'so no
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA028", tbSaleSeq) 'so seq
        WHR &= dbConn.WHERE_EQUAL("MOCTA.TA001", ddlWorkType) 'work type
        WHR &= dbConn.WHERE_BETWEEN("MOCTA.TA002", workNoFrom, workNoTo) 'work no from and to
        WHR &= dbConn.Where("MOCTA.TA003", WorkDateFrom, WorkDateTo) 'mo date 
        WHR &= dbConn.Where("COPTD.TD013", "", delDate) 'delivery date

        WHR &= dbConn.WHERE_LIKE("COPTC.TC004", tbCust) 'cust
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA006", tbItem) 'code
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA035", tbSpec) 'spec
        If workStatus <> "0" Then
            WHR &= " and MOCTA.TA011 "
            Dim WHR2 As String = ""
            If workStatus = "1" Then
                WHR &= " not "
                WHR2 = " and MOCTB.TB004-MOCTB.TB005 > 0 "
            End If
            WHR &= " in ('y','Y')" & WHR2
        End If

        SQL = " select MOCTB.TB003 from " & VarIni.ERP & "..MOCTB MOCTB " &
            " left join " & VarIni.ERP & "..MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
            " left join " & VarIni.ERP & "..COPTD COPTD on COPTD.TD001=MOCTA.TA026 and COPTD.TD002=MOCTA.TA027 and COPTD.TD003=MOCTA.TA028 " &
            " left join " & VarIni.ERP & "..COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " &
            " where MOCTA.TA013 = 'Y' " & WHR &
            " group by MOCTB.TB003 "
        ISQL = "insert into " & tempTable & "(item) " & SQL
        dbConn.TransactionSQL(ISQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        WHR1 = WHR
        'PO
        WHR = ""
        SQL = " select D.TD004 as item,SUM(isnull(D.TD008,0)-isnull(D.TD015,0)) as po from  " & VarIni.DBMIS & ".." & tempTable & " T " &
              " left join PURTD D on D.TD004=T.item where D.TD016='N' and SUBSTRING(D.TD004,3,1) not in ('2')   " & WHR &
              " group by D.TD004  order by D.TD004 "
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next
        'PR
        WHR = ""
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from " & VarIni.DBMIS & ".." & tempTable & " T " &
               " left join PURTB B on B.TB004=T.item " &
               " left join PURTR R on R.TR001=B.TB001 and R.TR002=B.TB002 and R.TR003=B.TB003 " &
              " where R.TR019='' and B.TB039='N'  and SUBSTRING(B.TB004,3,1) not in ('2') " & WHR & " group by B.TB004 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'inventory stock 
        SQL = " select INVMC.MC001 as item,sum(INVMC.MC007) as stockQty from " & VarIni.DBMIS & ".." & tempTable & " T " &
              " left join INVMC INVMC on INVMC.MC001=T.item " &
              " left join INVMB INVMB on INVMB.MB001=INVMC.MC001 " &
              " where INVMC.MC002 in ('2101','2201','2202','2204','2205','2206','2300','2301','2400','2900','2901','3333') " & WHR &
              " group by INVMC.MC001 "

        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("stockQty")
            USQL = " if exists(select * from " & tempTable & " where  item='" & item & "' ) " &
                   " update " & tempTable & " set stockQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,stockQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'show data
        WHR = WHR1 'cast( case when INVMC.MC007 is NULL then 0 else INVMC.MC007 end as decimal(20,3))
        '" left join INVMC on INVMC.MC001=T.item and INVMC.MC002=MOCTB.TB009" & _

        SQL = " select case when MOCTA.TA026='' then '' else MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 end as 'Sale Order'," &
            " MOCTA.TA001+'-'+MOCTA.TA002 as 'Manf. Order',MOCTA.TA006 as 'MO Item',MOCTA.TA035 as 'MO Spec'," &
            " MOCTB.TB003 as 'RM Item',TB012 as 'RM Desc',TB013 as 'RM Spec', MOCTB.TB007 as 'Unit',MOCTB.TB009 as 'Main WH'," &
            " cast(MOCTB.TB004 as decimal(20,3)) as 'Required Qty',cast(MOCTB.TB005 as decimal(20,3)) as 'QPA'," &
            " cast(MOCTB.TB004-MOCTB.TB005 as decimal(20,3)) as 'Qty not Issue', " &
            "  T.stockQty as Stock,T.poQty as PO,T.prQty as PR " &
            " from MOCTB MOCTB " &
            " left join MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
            " left join COPTD on COPTD.TD001=MOCTA.TA026 and COPTD.TD002=MOCTA.TA027 and COPTD.TD003=MOCTA.TA028 " &
            " left join COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " &
            " left join " & VarIni.DBMIS & ".." & tempTable & " T on T.item = MOCTB.TB003 " &
            " where MOCTA.TA013 = 'Y' " & WHR &
            " order by MOCTA.TA001,MOCTA.TA002,MOCTB.TB003 "
        Dim gvCont As New GridviewControl


        gvCont.ShowGridView(gvShow, SQL, VarIni.ERP)
        lbCount.Text = gvCont.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        Dim expCont As New ExportImportControl

        expCont.Export("MaterialsIssueStatus" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class