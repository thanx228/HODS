Public Class PONotCloseNew
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
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 = '22' order by MQ002 "
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 = '33' order by MQ002 "
            ControlForm.showCheckboxList(cblPurType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim WHR As String = ""
        WHR &= Conn_SQL.Where("TD001", cblPurType)
        WHR &= Conn_SQL.Where("TD002", tbPoNo)
        WHR &= Conn_SQL.Where("substring(TA006,1,4)", cblSaleType)
        WHR &= Conn_SQL.Where("TC004", tbSup)
        WHR &= Conn_SQL.Where("TD004", tbItem)
        WHR &= Conn_SQL.Where("TD006", tbSpec)
        WHR &= Conn_SQL.Where("TC011", tbBuyer)

        Select Case ddlDate.SelectedValue
            Case "TD014" ' yyyy-MM-dd (dateFormat4)
                WHR &= Conn_SQL.Where(ddlDate.Text, configDate.dateFormat4(tbDateFrom.Text.Trim), configDate.dateFormat4(tbDateTo.Text.Trim))
            Case "TC003" ' yyyyMMdd (dateFormat2)
                WHR &= Conn_SQL.Where(ddlDate.Text, configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))
        End Select

        Dim SQL As String = "select L.TD001,L.TD002,L.TD003,RL.TB003,L.TD026,L.TD027,L.TD004,L.TD006,L.TD016,L.TD007,L.TD008,L.TD015,L.TD009,L.TD014,L.TD008 - L.TD015 as Balance,H.TC004,R.TH007,R.TH015,L.UDF01 as PDD,L.TD012 as CDD,L.TD007 as WH,RL.TB011 AS RDate from PURTD L left join PURTC H on(H.TC001 = L.TD001) and (H.TC002 = L.TD002) left join PURTH R on (L.TD001 = R.TH011) and (L.TD002 = R.TH012) and (L.TD004 = R.TH004) and (L.TD015 = R.TH007) left join PURTB RL ON(L.TD026 = RL.TB001) AND (L.TD027 = RL.TB002) and (L.TD004 = RL.TB004) and (L.TD003 = RL.TB003) where L.TD016 = 'N'"
        Dim SSQL As String = ""
        SSQL = "cast((select sum(TH007) from PURTH where TH011=TD001 and TH012=TD002 and TH013=TD003 and TH030='N' ) as decimal(15,2)) as 'Wait Receipt Approved',"

        SQL = " select TD001+'-'+TD002+'-'+TD003 as 'PO NO'," & _
              " (SUBSTRING(TC003,7,2)+'-'+SUBSTRING(TC003,5,2)+'-'+SUBSTRING(TC003,1,4)) as 'PO Date'," & _
              " rtrim(TC004)+'-'+MA002 as 'Supplier',TD004 as 'Item',TD005 as 'Desc'," & _
              " TD006 as 'Spec' ,case when TD026='' then '' else TD026+'-'+TD027+'-'+TD028 end as 'PR NO' ," & _
              " TA005 as 'Source Ref.',TD009 as 'Unit',cast(TD008 as decimal(15,2)) as 'Purchase Qty'," & _
              " cast(TD015 as decimal(15,2)) as 'Delivery Qty',cast(TD008-TD015 as decimal(15,2)) as 'Balance Qty'," & SSQL & _
              " TD014 as 'Confirm Del Date'," & _
              " (SUBSTRING(PURTD.TD012,7,2)+'-'+SUBSTRING(PURTD.TD012,5,2)+'-'+SUBSTRING(PURTD.TD012,1,4)) as 'Plan Delivery Date',TC011 as 'Buyer' " & _
              " from PURTD left join PURTC on TC001=TD001 and TC002 =TD002 " & _
              " left join PURTA on TA001=TD026 and TA002=TD027 " & _
              " left join PURMA on MA001=TC004 " & _
              " where TD016 = 'N' " & WHR & _
              " order by TD001,TD002,TD003 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("PurchaseOrderNotClose" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class