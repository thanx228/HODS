Public Class SumScrap
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 in ('W01','W02','W07','W12','W19','W23','W25','W27')
            ControlForm.showDDL(DDLWC, SQL, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        If DDLStatus.SelectedIndex = 0 Then 'ALL
            If txtfrom.Text <> "" And txtto.Text <> "" Then
                DataSourceScrap.SelectCommand = "select TC001,TC002,TC023,TC014,TC016,(TC014 * TC016)/100 as Scrap,SUM(TC016) as ScrapAmt from SFCTC where TC038 between '" & txtfrom.Text & "' and '" & txtto.Text & "' group by TC023,TC014,TC016,TC001,TC002"
                GridView1.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("select COUNT(*) from SFCTC where TC038 between '" & txtfrom.Text & "' and '" & txtto.Text & "'", Conn_SQL.ERP_ConnectionString)
            End If
        ElseIf DDLStatus.SelectedIndex = 1 Then 'Approved
            If txtfrom.Text <> "" And txtto.Text <> "" Then
                DataSourceScrap.SelectCommand = "select TC001,TC002,TC023,TC014,TC016,(TC014 * TC016)/100 as Scrap,SUM(TC016) as ScrapAmt from SFCTC where TC038 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and TC022 ='" & DDLStatus.SelectedValue & "' group by TC023,TC014,TC016,TC001,TC002 "
                GridView1.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from SFCTC  where TC038 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and TC022 ='" & DDLStatus.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)
            End If
        ElseIf DDLStatus.SelectedIndex = 2 Then 'No Approved
            If txtfrom.Text <> "" And txtto.Text <> "" Then
                DataSourceScrap.SelectCommand = "select TC001,TC002,TC023,TC014,TC016,(TC014 * TC016)/100 as Scrap,SUM(TC016) as ScrapAmt from SFCTC where TC038 between '" & txtfrom.Text & "' and '" & txtto.Text & "'  and TC022 ='" & DDLStatus.SelectedValue & "' group by TC023,TC014,TC016,TC001,TC002 "
                GridView1.DataBind()
                Me.lblcount.Text = Conn_SQL.Get_value("Select COUNT (*) from SFCTC where TC038 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and TC022 ='" & DDLStatus.SelectedValue & "'", Conn_SQL.ERP_ConnectionString)
            End If
        End If
       
    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            txttype.Text = GridView1.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            txtno.Text = GridView1.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
        End If
    End Sub
End Class