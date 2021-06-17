Public Class MaterialsNotIssue
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    'Dim wcList As String = "'W04','W05','W09','W12','W13','W14','W15','W16'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD where MD001 in ('W04','W05','W09','W12','W13','W14','W15','W16')  order by MD001 " '
            'ControlForm.showDDL(ddlWc, SQL, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showCheckboxList(clWorkType, SQL, "MQ002", "MQ001", 4, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = "",
            codeType As String = ddlCodeType.Text,
            condition As String = ddlCondition.Text.Trim,
            program As DataTable,
            item As String = "",
            qty As Decimal = 0,
            USQL As String = ""


        'Sub createTempMatNotIssue(ByVal tempTable As String)
        Dim tempTable As String = "tempMaterialsNotIssue" & Session("UserName")
        CreateTempTable.createTempMatNotIssue(tempTable)

        Dim txtCodeType As String = "'1','4'"
        If codeType <> "0" Then
            txtCodeType = "'" & codeType & "'"
        End If
        
        'get code from Manufactor Order
        WHR = ""
        WHR = WHR & " and SUBSTRING(MOCTB.TB003,3,1) in (" & txtCodeType & ") " 'code Type
        WHR = WHR & Conn_SQL.Where("MOCTA.TA001", clWorkType) 'Work Type
        WHR = WHR & Conn_SQL.Where("MOCTA.TA002", tbWorkNo) 'Work No
        WHR = WHR & Conn_SQL.Where("MOCTB.TB003", tbItem) 'Item
        WHR = WHR & Conn_SQL.Where("MOCTB.TB012", "MOCTB.TB013", tbSpec) 'Spec
        WHR = WHR & Conn_SQL.Where("MOCTA.TA009", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
        If condition <> "0" Then 'check condition
            Select Case condition
                Case "1" ' not issue =0
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB005", "=0")
                Case "2" ' issue < required,issue>0
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB004", "> MOCTB.TB005")
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB005", ">0")
            End Select
        End If

        SQL = " select distinct MOCTB.TB003 from JINPAO80.dbo.MOCTB " & _
              " left join JINPAO80.dbo.MOCTA on MOCTA.TA001 =MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002" & _
              " where  MOCTA.TA013='Y' and MOCTA.TA011 not in ('Y','y') and MOCTB.TB004-MOCTB.TB005>0 " & WHR
        ISQL = "insert into " & tempTable & "(item) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        'get PR
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from DBMIS.dbo." & tempTable & " T " & _
                 " left join JINPAO80.dbo.PURTB B on B.TB004=T.item " & _
                 " left join JINPAO80.dbo.PURTR R on R.TR001=B.TB001 and R.TR002=B.TB002 and R.TR003=B.TB003 " & _
                " where R.TR019='' and B.TB039='N'   group by B.TB004 "
        program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To program.Rows.Count - 1
            item = program.Rows(i).Item("item")
            qty = program.Rows(i).Item("pr")
            USQL = " update " & tempTable & " set prQty='" & qty & "' where item='" & item & "' "
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Get PO
        SQL = " select D.TD004 as item,SUM(isnull(D.TD008,0)-isnull(D.TD015,0)) as po from  DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.PURTD D on D.TD004=T.item where D.TD016='N' and SUBSTRING(D.TD004,3,1) not in ('2')   " & _
              " group by D.TD004  order by D.TD004 "

        program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To program.Rows.Count - 1
            item = program.Rows(i).Item("item")
            qty = program.Rows(i).Item("po")
            USQL = " update " & tempTable & " set poQty='" & qty & "' where item='" & item & "' "
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Get IPQC
        SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH007,0)) as po_insp from DBMIS.dbo." & tempTable & " T " & _
           " left join JINPAO80.dbo.PURTH on PURTH.TH004=T.item " & _
           " left join JINPAO80.dbo.PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " & _
           " where PURTG.TG013 = 'N' group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) " & _
           " order by PURTH.TH004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp")
            USQL = " update " & tempTable & " set poRcpQty='" & qty & "' where item='" & item & "' "
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'get Stock
        SQL = " select INVMC.MC001 as item,sum(INVMC.MC007) as stockQty from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=INVMC.MC001 " & _
              " where INVMC.MC002 in ('2201','2202','2204','2205','2206','2900','2901','3333')" & _
              " group by INVMC.MC001 "

        'SQL = " select B.MB001 as item,L.ML005 as supportQty from INVML L left join INVMB B on B.MB001=L.ML001 " & _
        '      " where L.ML002='2400' " & whr & " order by B.MB001 "
        program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To program.Rows.Count - 1
            item = program.Rows(i).Item("item")
            qty = program.Rows(i).Item("stockQty")
            USQL = " update " & tempTable & " set stockQty='" & qty & "' where item='" & item & "' "
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'show data
        WHR = ""
        WHR = WHR & " and SUBSTRING(MOCTB.TB003,3,1) in (" & txtCodeType & ") " 'code Type
        WHR = WHR & Conn_SQL.Where("MOCTA.TA001", clWorkType) 'Work Type
        WHR = WHR & Conn_SQL.Where("MOCTA.TA002", tbWorkNo) 'Work No
        WHR = WHR & Conn_SQL.Where("MOCTB.TB003", tbItem) 'Item
        WHR = WHR & Conn_SQL.Where("MOCTB.TB012", "MOCTB.TB013", tbSpec) 'Spec
        ' WHR = WHR & Conn_SQL.Where("SFCTA.TA006", ddlWc) 'Work Center
        WHR = WHR & Conn_SQL.Where("MOCTA.TA009", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
        If condition <> "0" Then 'check condition
            Select Case condition
                Case "1" ' not issue =0
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB005", "=0")
                Case "2" ' issue < required,issue>0
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB004", "> MOCTB.TB005")
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB005", ">0")
            End Select
        End If

        SQL = "select MOCTA.TA001+'-'+MOCTA.TA002 as TA001," & _
            " (SUBSTRING(MOCTA.TA009,7,2)+'/'+SUBSTRING(MOCTA.TA009,5,2)+'/'+SUBSTRING(MOCTA.TA009,1,4)) as 'TA009'," & _
            " MOCTA.TA035,MOCTB.TB003,MOCTB.TB012+' '+MOCTB.TB013 as TB012," & _
            " cast(MOCTB.TB004 as decimal(20,4)) as TB004,cast(MOCTB.TB005 as decimal(20,4)) as TB005," & _
            " cast(MOCTB.TB004-MOCTB.TB005 as decimal(20,4)) as 'balQty',MOCTB.TB007 ," & _
            " T.prQty,T.poQty,T.poRcpQty,T.stockQty" & _
            " from  MOCTB left join MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " & _
            " left join DBMIS.dbo." & tempTable & " T on T.item = MOCTB.TB003 " & _
            " where MOCTA.TA013='Y' and MOCTA.TA011 not in ('Y','y') and MOCTB.TB004-MOCTB.TB005>0  " & WHR & _
            " order by MOCTB.TB003,MOCTA.TA001,MOCTA.TA002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("MaterialsNotIssue" & Session("UserName"), gvShow)
    End Sub
End Class