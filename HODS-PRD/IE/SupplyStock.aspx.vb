Public Class SupplyStock
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    'Dim prTypeAll As String = "'3112','3113','3114'"
    'Dim poTypeAll As String = "'3312','3313'"
    Dim allWhType As String = "'4100','4200','2207'"
    Dim allCodeType As String = "'1401','1402','1201'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MC001,MC001+' : '+MC002 as MC002 from CMSMC where MC001 in(" & allWhType & ") order by MC001"
            ControlForm.showDDL(ddlWh, SQL, "MC002", "MC001", True, Conn_SQL.ERP_ConnectionString)
            SQL = "select MA002,MA002+' : '+MA003 as MA003 from INVMA where MA001='1' and MA002 in(" & allCodeType & ") order by MA002"
            ControlForm.showDDL(ddlCodeType, SQL, "MA003", "MA002", True, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempCodeStatus" & Session("UserName")
        CreateTempTable.createTempStockSupply(tempTable)
        Dim SQL As String = "",
            WHR As String = "",
            USQL As String = "",
            dueDate As String = configDate.dateFormat2(tbDueDate.Text),
            code As String = "",
            qty As Decimal = 0,
            codeType As String = ddlCodeType.Text.Trim
        'wh As String = ddlWh.Text.Trim
        Dim Program As New DataTable

        'PR
        WHR = ""
        If codeType <> "ALL" Then
            WHR = WHR & " and MB005 ='" & codeType & "' "
            If codeType = "1201" Then
                WHR = WHR & " and MB017 ='2207' "
            End If
        Else
            WHR = WHR & " and (MB005 in ('1401','1402') or (MB005 ='1201' and MB017 ='2207')) "
        End If
        'WHR = WHR & Conn_SQL.Where("MB017", ddlWh)
        If ddlWh.Text.Trim <> "ALL" Then
            WHR = WHR & Conn_SQL.Where("MB017", ddlWh)
        Else
            WHR = WHR & " and MB017 in ('4100','4200','2207') "
        End If
        WHR = WHR & Conn_SQL.Where("TB004", tbItem)
        WHR = WHR & Conn_SQL.Where("TB005", tbDesc)
        WHR = WHR & Conn_SQL.Where("TB005", tbSpec)
        WHR = WHR & Conn_SQL.Where("TB011", "", dueDate)

        SQL = " select TB004 as code,sum(TR006) as prQty " & _
              " from PURTR " & _
              " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
              " left join PURTA on TA001=TB001 and TA002=TB002  " & _
              " left join INVMB on MB001=TB004 " & _
              " where TR019='' and TB039='N' " & WHR & _
              " group by TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            code = Program.Rows(i).Item("code")
            qty = Program.Rows(i).Item("prQty")
            USQL = " if exists(select * from " & tempTable & " where item='" & code & "' ) " & _
                   " update " & tempTable & " set prQty='" & qty & "' where item='" & code & "' else " & _
                   " insert into " & tempTable & "(item,prQty)values ('" & code & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'PO
        WHR = ""
        If codeType <> "ALL" Then
            WHR = WHR & " and MB005 ='" & codeType & "' "
            If codeType = "1201" Then
                WHR = WHR & " and MB017 ='2207' "
            End If
        Else
            WHR = WHR & " and (MB005  in ('1401','1402') or (MB005 ='1201' and MB017 ='2207') )"
        End If
        'WHR = WHR & Conn_SQL.Where("MB017", ddlWh)
        If ddlWh.Text.Trim <> "ALL" Then
            WHR = WHR & Conn_SQL.Where("MB017", ddlWh)
        Else
            WHR = WHR & " and MB017 in ('4100','4200','2207') "
        End If
        WHR = WHR & Conn_SQL.Where("TD004", tbItem)
        WHR = WHR & Conn_SQL.Where("TD005", tbDesc)
        WHR = WHR & Conn_SQL.Where("TD006", tbSpec)
        WHR = WHR & Conn_SQL.Where("TD012", "", dueDate)

        SQL = " select  TD004 as code,sum(TD008-TD015) as poQty " & _
              " from PURTD  left join PURTC on TC001 = TD001 and TC002 = TD002 " & _
              " left join INVMB on MB001=TD004 " & _
              " where  TD016='N' " & WHR & _
              " group by TD004"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            code = Program.Rows(i).Item("code")
            qty = Program.Rows(i).Item("poQty")
            USQL = " if exists(select * from " & tempTable & " where item='" & code & "' ) " & _
                   " update " & tempTable & " set poQty='" & qty & "' where item='" & code & "' else " & _
                   " insert into " & tempTable & "(item,poQty)values ('" & code & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'WH 4100,4200,2207
        WHR = ""
        If ddlWh.Text.Trim <> "ALL" Then
            WHR = WHR & Conn_SQL.Where("MB017", ddlWh)
            WHR = WHR & Conn_SQL.Where("MC002", ddlWh)
        Else
            WHR = WHR & " and MB017 in ('4100','4200','2207') and MC002 in ('4100','4200','2207') "
        End If
        WHR = WHR & Conn_SQL.Where("MB001", tbItem)
        WHR = WHR & Conn_SQL.Where("MB002", tbDesc)
        WHR = WHR & Conn_SQL.Where("MB003", tbSpec)

        Dim fldWh As String = ""
        SQL = " select MC001 as code,MC002 as wh ,sum(MC007) as qty from INVMC " & _
              " left join INVMB on MB001=MC001 where MC007>0 " & WHR & _
              " group by MC001,MC002 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            code = Program.Rows(i).Item("code")
            qty = Program.Rows(i).Item("qty")
            fldWh = "wh" & Program.Rows(i).Item("wh").ToString.Trim
            USQL = " if exists(select * from " & tempTable & " where item='" & code & "' ) " & _
                   " update " & tempTable & " set " & fldWh & "='" & qty & "' where item='" & code & "' else " & _
                   " insert into " & tempTable & "(item," & fldWh & ")values ('" & code & "','" & qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        Dim wh As String = ddlWh.Text.Trim,
            fld As String = "T.wh4100 as 'WH 4100',T.wh4200 as 'WH 4200',T.wh2207 as 'WH 2207',"
        If wh <> "ALL" Then
            fld = "T.wh" & wh & " as 'WH " & wh & "',"
        End If
        'WHR = ""
        'If overDate > 0 Then
        '    WHR = " where T.poQty>0 "
        'End If
        SQL = " select T.item as 'Item',INVMB.MB002 as 'Desc.',INVMB.MB003 as 'Spec'," & _
              " INVMB.MB004 as 'UNIT'," & fld & _
              " prQty as 'PR not open PO Qty',T.poQty as 'PO Undelivery Qty' " & _
              " from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & _
              " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SupplyStock" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplLink"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Item")) Then
                    Dim link As String = ""
                    Dim item As String = .DataItem("Item")
                    link = link & "&item= " & item
                    hplDetail.NavigateUrl = "SupplyStockPop.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", item)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub
End Class