Public Class ProductCost
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MQ001,MQ001+':'+MQ002 as MQ002 from CMSMQ where MQ003 in('D3','D4','D5') order by MQ001 "
            ControlForm.showDDL(ddlDocType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)

            SQL = "select MA002,rtrim(MA002)+'-'+MA003 as MA003 from INVMA where MA001='1' order by MA002"
            ControlForm.showDDL(ddlItemType, SQL, "MA003", "MA002", True, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = ""

        WHR = WHR & Conn_SQL.Where("LA001", tbItem)
        WHR = WHR & Conn_SQL.Where("MB002", tbDesc)
        WHR = WHR & Conn_SQL.Where("MB003", tbSpec)
        WHR = WHR & Conn_SQL.Where("LA007", tbDocNo)
        WHR = WHR & Conn_SQL.Where("LA006", ddlDocType)
        WHR = WHR & Conn_SQL.Where("MB005", ddlItemType)
        WHR = WHR & Conn_SQL.Where("LA004", configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))

        SQL = " select (SUBSTRING(LA004,7,2)+'-'+SUBSTRING(LA004,5,2)+'-'+SUBSTRING(LA004,1,4)) as 'LA004'," & _
              " LA001,MB002,MB003,MB004,LA006+'-'+LA007+'-'+LA008 as LA006,LA011,LA012,LA017,LA018,LA019,LA020, " & _
              " LA017/LA011 as LA0171,LA018/LA011 as LA0181,LA019/LA011 as LA0191,LA020/LA011 as LA0201, " & _
              " LA024,LA009,LA010  " & _
              " from INVLA " & _
              " left join INVMB on MB001=LA001 " & _
              " left join CMSMQ on MQ001=LA006 " & _
              " where MQ003 in('D3','D4','D5')  and LA011>0 " & WHR & _
              " order by LA004,LA001,LA006,LA007,LA008"

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("ProductCost" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class