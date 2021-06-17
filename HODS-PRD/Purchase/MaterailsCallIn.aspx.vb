Imports MIS_HTI.DataControl
Public Class MaterailsCallIn
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim dbConn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            Dim Sql As String = "select MC001,MC001+' '+MC002 MC002 from CMSMC where CMSMC.UDF01='Y' order by MC001"
            UcCblWh.show(Sql, VarIni.ERP, "MC002", "MC001", 5)
            btExport.Visible = False
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempMaterialsCallIn" & Session("UserName")
        CreateTempTable.createTempMaterialShortage(tempTable)
        If tbSup.Text.Trim <> "" Then
            SearchBySupplier(tempTable)
        Else
            SearchBy(tempTable)
        End If
        'Stock
        Dim CodeType As String = cblCodeType.Text,
            WHR As String = "",
            SQL As String = "",
            Program As New DataTable,
            item As String = "",
            Qty As Integer = 0,
            USQL As String = String.Empty

        'Dim whList As String = " and INVMC.MC002 in ('2101','2201','2202','2204','2205','2206','2300','2301','2400','2900','2901','3333') "
        'If CodeType = "2" Or CodeType = "3" Then
        '    whList = " and INVMC.MC002 not in ('8888','9999','2600','2700','5000','5001','5002') "
        'End If
        Dim whList As String = dbConn.WHERE_IN("INVMC.MC002", UcCblWh.getObject,, True)

        SQL = " select INVMC.MC001 as item,SUM(isnull(INVMC.MC007,0)) as stock," &
              " SUM(isnull(INVMC.MC004,0)) as saveStock from " & tempTable &
              " T left join  HOOTHAI.dbo.INVMC INVMC on INVMC.MC001=T.item " &
              " where (INVMC.MC007 <>0 or INVMC.MC004<>0  ) " & whList & " group by INVMC.MC001"
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("stock")
            Dim saveQty As Decimal = Program.Rows(i).Item("saveStock")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set stockQty='" & Qty & "',saveQty='" & saveQty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,stockQty,saveQty)values ('" & item & "','" & Qty & "','" & saveQty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'Show Data
        Dim strDemand As String = " T.issueQty+T.delQty  "
        Dim strSupply As String = " T.stockQty+T.poRcpQty " '+T.moQty+T.poQty+T.poManQty+T.poForQty+T.poMoQty+T.prQty+T.poRcpQty
        Dim strSupply2 As String = " T.stockQty+T.moQty+T.poQty+T.poManQty+T.poForQty+T.poMoQty+T.prQty+T.poRcpQty " '
        Dim valConsel As String = ddlCondition.SelectedValue.ToString
        If valConsel <> "0" Then
            WHR = " where "
            Select Case valConsel
                Case "1" 'Call In
                    WHR = WHR & strDemand & ">" & strSupply & " and " & strDemand & ">0  "
                Case "2" 'Shortage
                    WHR = WHR & strDemand & ">" & strSupply2 & " and " & strDemand & ">0  "
            End Select
        End If
        SQL = " select T.item as 'C01',INVMB.MB002 as 'C02', INVMB.MB003 as 'C22'," &
           " cast(case when (" & strSupply & ")-(" & strDemand & ")>='0' then '0' else (" & strSupply & ")-(" & strDemand & ") end as decimal(10,2)) as 'C03', " &
           " cast(case when (" & strSupply2 & ")-(" & strDemand & ")>='0' then '0' else (" & strSupply2 & ")-(" & strDemand & ") end as decimal(10,2)) as 'C04', " &
           " T.issueQty as 'C05',INVMB.MB036 as 'C06'," &
           " T.saveQty as 'C07',T.stockQty as 'C08',T.poRcpQty as 'C09'," &
           " T.poQty as 'C10',T.poManQty as 'C11',T.poForQty as 'C12',T.poMoQty as 'C13', " &
           " T.prQty as 'C14',T.prQtyNot as 'C15',T.moQty as 'C16',T.delQty as 'C17',INVMB.MB032 as 'C18', " &
           " (case when T.confirmdate='' then ' ' else (substring(T.confirmdate,7,2)+'-'+substring(T.confirmdate,5,2)+'-'+substring(T.confirmdate,1,4)) end  ) as 'C19', " &
           " INVMB.MB017 as 'C20', T.plandate as 'C21'" &
           " from " & tempTable & " T " &
           " left join HOOTHAI.dbo.INVMB INVMB on INVMB.MB001=T.item " & WHR &
           " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("C01")) Then
                    Dim link As String = ""
                    'Dim EndDate As String = configDate.dateFormat2(tbEndDate.Text)
                    Dim jpPart As String = .DataItem("C01")
                    link &= "&JPPart= " & jpPart
                    link &= "&JPSpec= " & .DataItem("C02") 'JP Spec
                    link &= "&issueQty= " & .DataItem("C05") 'MO Issue(-)
                    link &= "&stock= " & .DataItem("C08") 'Stock(+)
                    link &= "&poRcpQty= " & .DataItem("C09") 'PO Insp.(+)
                    link &= "&poQty= " & .DataItem("C10") 'PO(+)
                    link &= "&poManQty= " & .DataItem("C11") 'PO Manual(+)
                    link &= "&poForQty= " & .DataItem("C12") 'PO Forcast(+)
                    link &= "&poMoQty= " & .DataItem("C13") 'PO MO(+)
                    link &= "&prQty= " & .DataItem("C14") 'PR(+)App
                    link &= "&moQty= " & .DataItem("C16") 'MO Rcv(+)
                    link &= "&delQty= " & .DataItem("C17") 'SO(-)
                    link &= "&mainsup= " & .DataItem("C18") 'Main Supp.
                    link &= "&fixtime= " & .DataItem("C19") 'Fixed Lead Time
                    link &= "&endDate= " & configDate.dateFormat2(tbEndDate.Text)
                    hplDetail.NavigateUrl = "MaterailsCallInPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("MaterialsCallIn" & Session("UserName"), gvShow)
    End Sub

    Protected Sub SearchBy(ByVal tempTable As String)
        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = "",
            USQL As String = ""
        Dim CodeType As String = cblCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text.Trim,
            Spec As String = tbSpec.Text.Trim,
            Desc As String = tbDesc.Text.Trim,
            EndDate As String = configDate.dateFormat2(tbEndDate.Text)
        Dim Program As New DataTable
        Dim item As String = "",
            Qty As Decimal = 0,
            Mdate As String = ""

        'MO issue mat 
        WHR = Conn_SQL.Where("MOCTA.TA009", "", EndDate)
        WHR = WHR & Conn_SQL.Where("substring(MOCTB.TB003,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("MOCTB.TB003", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("MOCTB.TB012", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("MOCTB.TB013", tbSpec) 'Spec
        SQL = " select MOCTB.TB003,SUM(MOCTB.TB004-MOCTB.TB005) from  HOOTHAI.dbo.MOCTB MOCTB " &
              " left join HOOTHAI.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
              " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & WHR &
              " group by MOCTB.TB003 "
        ISQL = "insert into " & tempTable & "(item,issueQty) " & SQL
        dbConn.TransactionSQL(ISQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        'Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        'MO 
        SQL = String.Empty
        WHR = Conn_SQL.Where("substring(MOCTA.TA006,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("MOCTA.TA006", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("MOCTA.TA034", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("MOCTA.TA035", tbSpec) 'Spec
        SQL = " select MOCTA.TA006 as item, SUM(isnull(MOCTA.TA015,0)-isnull(MOCTA.TA017,0)-isnull(MOCTA.TA018,0)) as mo from HOOTHAI.dbo.MOCTA " &
              " where MOCTA.TA011 not in('y','Y') and MOCTA.TA013 ='Y'  " & WHR &
              " group by MOCTA.TA006  order by MOCTA.TA006"
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("mo")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'Sale Order
        WHR = Conn_SQL.Where("COPTD.TD013", "", EndDate)
        WHR = WHR & Conn_SQL.Where("substring(COPTD.TD004,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("COPTD.TD004", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("COPTD.TD005", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("COPTD.TD006", tbSpec) 'Spec
        SQL = " select COPTD.TD004 as item,SUM(COPTD.TD008-COPTD.TD009) as so from HOOTHAI.dbo.COPTD " &
            " left join  HOOTHAI.dbo.COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " &
            " where COPTD.TD016='N' " & WHR & " group by COPTD.TD004  order by COPTD.TD004 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("so")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set delQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,delQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next
        'purchase request Appove 
        WHR = Conn_SQL.Where("substring(PURTB.TB004,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("PURTB.TB004", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("PURTB.TB005", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("PURTB.TB006", tbSpec) 'Spec

        SQL = " select PURTB.TB004 as item,sum(PURTR.TR006) as pr from  HOOTHAI.dbo.PURTB " &
              " left join HOOTHAI.dbo.PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " &
              " left join HOOTHAI.dbo.PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " &
              " where PURTB.TB039='N' and PURTA.TA007 = 'Y' and PURTR.TR019='' " & WHR &
              " group by PURTB.TB004 order by PURTB.TB004 "
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'purchase request Not Appove 
        SQL = " select PURTB.TB004 as item,sum(PURTB.TB009) as pr from  HOOTHAI.dbo.PURTB " &
              " left join HOOTHAI.dbo.PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " &
              " where PURTB.TB039='N' and PURTA.TA007 = 'N' " & WHR &
              " group by PURTB.TB004 order by PURTB.TB004 "
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set prQtyNot='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,prQtyNot)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'purchase order
        WHR = Conn_SQL.Where("substring(PURTD.TD004,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("PURTD.TD004", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("PURTD.TD005", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("PURTD.TD006", tbSpec) 'Spec
        'WHR = WHR & Conn_SQL.Where("PURTC.TC004", tbSup) 'Supplier



        SQL = " select PURTD.TD004 as item,substring(PURTD.TD024,1,1) as poType,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from HOOTHAI.dbo. PURTD " &
              " left join HOOTHAI.dbo.PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' " & WHR & " group by PURTD.TD004,substring(PURTD.TD024,1,1)  order by PURTD.TD004,substring(PURTD.TD024,1,1) "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")
            Dim fldPO As String = "poManQty"
            Select Case Program.Rows(i).Item("poType")
                Case "2"
                    fldPO = "poQty"
                Case "4"
                    fldPO = "poForQty"
                Case "5"
                    fldPO = "poMoQty"
            End Select

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set " & fldPO & "= " & fldPO & "+'" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item," & fldPO & ")values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next




        'PO ConfirmDate 
        WHR = Conn_SQL.Where("substring(PURTD.TD004,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("PURTD.TD004", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("PURTD.TD005", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("PURTD.TD006", tbSpec) 'Spec
        SQL = " select distinct PURTD.TD004 as item, PURTD.TD012 as ConFirmDate from HOOTHAI.dbo.PURTD " &
            " left join HOOTHAI.dbo.PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
            " where PURTD.TD016='N' " & WHR
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("confirmdate")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set confirmdate='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,confirmdate)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next



        'PO PlanDate 
        WHR = Conn_SQL.Where("substring(PURTD.TD004,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("PURTD.TD004", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("PURTD.TD005", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("PURTD.TD006", tbSpec) 'Spec
        SQL = " select distinct PURTD.TD004 as item, isnull(PURTD.UDF01,'') as plandate from HOOTHAI.dbo.PURTD " &
            " left join HOOTHAI.dbo.PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
            " where PURTD.TD016='N' " & WHR
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Mdate = Program.Rows(i).Item("plandate")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set plandate='" & Mdate & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,plandate)values ('" & item & "','" & Mdate & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next



        'Purchase receipt inspection
        WHR = Conn_SQL.Where("substring(PURTH.TH004,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("PURTH.TH004", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("PURTH.TH005", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("PURTH.TH006", tbSpec) 'Spec
        'WHR = WHR & Conn_SQL.Where("PURTG.TG005", tbSup) 'Supplier

        SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH007,0)) as po_insp from HOOTHAI.dbo.PURTH " &
              " left join HOOTHAI.dbo.PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " &
              " where PURTG.TG013 = 'N' " & WHR & " group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) " &
              " order by PURTH.TH004 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next
    End Sub

    Protected Sub SearchBySupplier(ByVal tempTable As String)
        Dim SQL As String = "",
    WHR As String = "",
    ISQL As String = "",
    USQL As String = ""
        Dim CodeType As String = cblCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text.Trim,
            Spec As String = tbSpec.Text.Trim,
            Desc As String = tbDesc.Text.Trim,
            EndDate As String = configDate.dateFormat2(tbEndDate.Text)
        Dim Program As New DataTable
        Dim item As String = "",
            Qty As Integer = 0
        'purchase order
        WHR = Conn_SQL.Where("substring(PURTD.TD004,3,1)", cblCodeType, "0") 'code type
        WHR = WHR & Conn_SQL.Where("PURTD.TD004", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("PURTD.TD005", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("PURTD.TD006", tbSpec) 'Spec
        WHR = WHR & Conn_SQL.Where("PURTC.TC004", tbSup) 'Supplier

        SQL = " select PURTD.TD004 as item,substring(PURTD.TD024,1,1) as poType,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as po from HOOTHAI.dbo.PURTD " &
              " left join HOOTHAI.dbo.PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
              " where PURTD.TD016='N' " & WHR & " group by PURTD.TD004,substring(PURTD.TD024,1,1)  order by PURTD.TD004,substring(PURTD.TD024,1,1) "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")

            Dim fldPO As String = "poManQty"
            Select Case Program.Rows(i).Item("poType")
                Case "2"
                    fldPO = "poQty"
                Case "4"
                    fldPO = "poForQty"
                Case "5"
                    fldPO = "poMoQty"
            End Select

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set " & fldPO & "= " & fldPO & "+'" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item," & fldPO & ")values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'MO issue mat 
        WHR = Conn_SQL.Where("MOCTA.TA036", "", EndDate) 'MOCTB.TB015
        SQL = " select MOCTB.TB003 as item,SUM(MOCTB.TB004-MOCTB.TB005) as mo from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join HOOTHAI.dbo.MOCTB MOCTB on MOCTB.TB003=T.item" &
              " left join HOOTHAI.dbo.MOCTA MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 " &
              " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y' " & WHR &
              " group by MOCTB.TB003 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("mo")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set issueQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,issueQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next
        'MO 
        SQL = " select MOCTA.TA006 as item, SUM(isnull(MOCTA.TA015,0)-isnull(MOCTA.TA017,0)) as mo from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join HOOTHAI.dbo.MOCTA MOCTA on MOCTA.TA006=T.item" &
              " where MOCTA.TA011 not in('y','Y') and MOCTA.TA013 ='Y'  " &
              " group by MOCTA.TA006  order by MOCTA.TA006"
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("mo")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'Sale Order
        WHR = Conn_SQL.Where("COPTD.TD013", "", EndDate)
        SQL = " select COPTD.TD004 as item,SUM(COPTD.TD008-COPTD.TD009) as so from HOOTHAI_REPORT.dbo." & tempTable & " T " &
             " left join HOOTHAI.dbo.COPTD COPTD on COPTD.TD004=T.item " &
             " left join  COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " &
             " where COPTD.TD016='N' " & WHR & " group by COPTD.TD004  order by COPTD.TD004 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("so")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set delQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,delQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next
        'purchase request Appove 
        SQL = " select PURTB.TB004 as item,sum(PURTR.TR006) as pr from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join HOOTHAI.dbo.PURTB PURTB on PURTB.TB004=T.item " &
              " left join HOOTHAI.dbo.PURTR PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " &
              " left join HOOTHAI.dbo.PURTA PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " &
              " where PURTR.TR019='' and PURTB.TB039='N' and PURTA.TA007 = 'Y' " &
              " group by PURTB.TB004 order by PURTB.TB004 "
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

        'purchase request Not Appove 
        SQL = " select PURTB.TB004 as item,sum(PURTR.TR006) as pr from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join HOOTHAI.dbo.PURTB PURTB on PURTB.TB004=T.item " &
              " left join HOOTHAI.dbo.PURTR PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003 " &
              " left join HOOTHAI.dbo.PURTA PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 " &
              " where PURTR.TR019='' and PURTB.TB039='N' and PURTA.TA007 = 'N' " &
              " group by PURTB.TB004 order by PURTB.TB004 "
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set prQtyNot='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,prQtyNot)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

        'Purchase receipt inspection
        SQL = " select PURTH.TH004 as item,SUM(isnull(PURTH.TH007,0)) as po_insp from HOOTHAI_REPORT.dbo." & tempTable & " T " &
              " left join HOOTHAI.dbo.PURTH PURTH on PURTH.TH004=T.item " &
              " left join HOOTHAI.dbo.PURTG PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002 " &
              " where PURTG.TG013 = 'N' group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) " &
              " order by PURTH.TH004 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next

    End Sub

End Class