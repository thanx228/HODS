Public Class CheckBOM
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String,
            WHR As String

        WHR = Conn_SQL.Where("TD001", cblSaleType)
        WHR = WHR & Conn_SQL.Where("TC004", tbCust)
        WHR = WHR & Conn_SQL.Where("TD002", tbSaleNo)
        WHR = WHR & Conn_SQL.Where("TD003", tbSOSeq)
        WHR = WHR & Conn_SQL.Where("TD004", tbItem)
        WHR = WHR & Conn_SQL.Where("TD006", tbSpec)
        WHR = WHR & Conn_SQL.Where("TD013", configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))
        WHR = WHR & Conn_SQL.Where("MB025", cblProperty)

        Dim aa As String = "<>"
        If cbApp.Checked Then
            aa = "="
        End If
        If cbNoBOM.Checked Then
            WHR = WHR & " and (select count(*) from BOMMD where MD001=TD004)=0 "
        End If

        SQL = " select COPTD.TD001 +'-'+ COPTD.TD002 +'-'+ COPTD.TD003 as 'Sale Order', " & _
            " (SUBSTRING(COPTD.TD004,1,14)+'-'+SUBSTRING(COPTD.TD004,15,2)) as 'Item'," & _
            "  COPTD.TD005 as 'Description', COPTD.TD006 as 'Spec' ,MB025 as 'Property'," & _
            " CONVERT (int, COPTD.TD008+COPTD.TD024) as 'Qty',CONVERT (int, COPTD.TD009+COPTD.TD025) as 'Delivery Qty'," & _
            " CONVERT (int, COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025) as 'Balacne Qty'," & _
            " COPTC.UDF01 as 'Status', COPTC.TC012 as 'Industry Type',  " & _
            " rtrim(COPTC.TC004)+'-'+COPMA.MA002 as 'Cust.'," & _
            " case when TD013='' then '' else (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) end as 'Plan Delivery Date'," & _
            " COPTD.UDF02 as 'Sale Request DueDate' ," & _
            " case when (select count(*) from BOMMD where MD001=TD004)=0 then 'N' else 'Y' end as 'BOM', " & _
            " (select top 1 case when BOMMD.MODIFIER='PDM' then (SUBSTRING(BOMMD.MODI_DATE,7,2)+'-'+SUBSTRING(BOMMD.MODI_DATE,5,2)+'-'+SUBSTRING(BOMMD.MODI_DATE,1,4)) else (SUBSTRING(BOMMD.CREATE_DATE,7,2)+'-'+SUBSTRING(BOMMD.CREATE_DATE,5,2)+'-'+SUBSTRING(BOMMD.CREATE_DATE,1,4)) end from BOMMD where MD001=TD004 order by BOMMD.MODI_DATE desc, BOMMD.CREATE_DATE desc) as 'Last BOM Update' " & _
            " from COPTD " & _
            " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 " & _
            " left join COPMA on COPMA.MA001  = COPTC.TC004 " & _
            " left join INVMB on INVMB.MB001  = COPTD.TD004 " & _
            " where TC027" & aa & "'Y'  and TD016='N' " & WHR & _
            " order by COPTD .TD001 , COPTD.TD002,COPTD.TD003 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hlBOM"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Item")) Then
                    Dim link As String = "&item= " & .DataItem("Item").ToString.Replace("-", "").Trim
                    link = link & "&spec=" & .DataItem("Spec").ToString.Trim
                    hplDetail.NavigateUrl = "CheckBOMPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", .DataItem("Spec"))
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("checkBOM" & Session("UserName"), gvShow)
    End Sub
End Class