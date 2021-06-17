Public Class SONotApproved
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL


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

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShow.Click

        Dim SQL As String = ""
        SQL = " select COPTD.TD001 +'-'+ COPTD.TD002 +'-'+ COPTD.TD003 as 'SO Type / No. / Seq', " & _
            " (SUBSTRING(COPTD.TD004,1,14)+'-'+SUBSTRING(COPTD.TD004,15,2)) as 'Item'," & _
            " COPTD.TD006 as 'Spec' , COPTD.TD005 as 'Description', " & _
            " CONVERT (int, COPTD.TD008) as 'Qty'," & _
            " COPTC.UDF01 as 'Status', COPTC.TC012 as 'Industry Type',  " & _
            " COPTC.TC004+'-'+COPMA.MA002 as 'Cust.'," & _
            " case when TD013='' then '' else (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) end as TD013," & _
            " COPTD.UDF02 as SaleRequestDueDate, " & _
            " case when (select count(*) from BOMMD where MD001=TD004)=0 then 'No' else 'Yes' end as 'BOM' " & _
            " from COPTD " & _
            " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 " & _
            " left join COPMA on COPMA.MA001  = COPTC.TC004 " & _
            " where COPTC.TC027 = 'N' " & Conn_SQL.Where("COPTD.TD001", cblSaleType) & _
            " order by COPTD .TD001 , COPTD.TD002,COPTD.TD003 "

        ControlForm.ShowGridView(gvshow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvshow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SONotApproved" & Session("UserName"), gvshow)
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvshow.RowDataBound

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
End Class