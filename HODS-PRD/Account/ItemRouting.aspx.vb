Public Class ItemRouting
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = ""

        WHR = WHR & Conn_SQL.Where("MB001", tbItem)
        WHR = WHR & Conn_SQL.Where("MB002", tbDesc)
        WHR = WHR & Conn_SQL.Where("MB003", tbSpec)
        WHR = WHR & Conn_SQL.Where("MB011", tbRout)

        Dim routing As String = ddlRouting.Text.Trim
        If routing <> "0" Then
            Dim val As String = ""
            If routing = "1" Then
                val = "not"
            End If
            WHR = WHR & " and ME002 is " & val & " null "
        End If
        If cbEmpty.Checked Then
            WHR = WHR & " and MB011='' "
        End If
        SQL = " select ''''+MB001 MB001 ,MB002,MB003,MB004,MB011,case when ME002 is null then 'No' else 'Yes' end ME002 " & _
              " from INVMB left join BOMME on ME001=MB010 and ME002=MB011 " & _
              " where MB025='M' and MB109 = 'Y' " & WHR & " order by MB001"
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("ItemRouting" & Session("UserName"), gvShow)
    End Sub

    'Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        For i As Integer = 0 To e.Row.Cells.Count - 1
    '            e.Row.Cells(i).Attributes.Add("class", "text")
    '        Next
    '        'e.Row.Attributes.Add("class", "text")
    '    End If




    'End Sub
End Class