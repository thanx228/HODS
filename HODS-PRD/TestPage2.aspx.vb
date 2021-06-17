Public Class TestPage2
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'Dim dt As New DataTable()
            'dt.Columns.AddRange(New DataColumn() {New DataColumn("Age", GetType(Integer)), _
            '                                       New DataColumn("Sex", GetType(String)), _
            '                                       New DataColumn("Email", GetType(String))})
            'dt.Rows.Add(1, "John Hammond", "United States")
            'dt.Rows.Add(2, "Mudassar Khan", "India")
            'dt.Rows.Add(3, "Suzanne Mathews", "France")
            'dt.Rows.Add(4, "Robert Schidner", "Russia")
            'GridView1.DataSource = dt
            'GridView1.DataBind()
        End If

    End Sub


    'Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
    '    Dim SQL As String = "select TD001,TD002,TD003,UDF01 from COPTD where UDF01<>'' "
    '    Dim Program As New DataTable
    '    Dim USQL As String = ""
    '    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
    '    'For i As Integer = 0 To Program.Rows.Count - 1
    '    '    Dim orderType As String = Program.Rows(i).Item("TD001").ToString
    '    '    Dim orderNo As String = Program.Rows(i).Item("TD002").ToString
    '    '    Dim orderSeq As String = Program.Rows(i).Item("TD003").ToString
    '    '    Dim custPO As String = Program.Rows(i).Item("UDF01").ToString
    '    '    USQL = "update COPTD set TD027='" & custPO & "' where TD001='" & orderType & "' and TD002='" & orderNo & "' and TD003='" & orderSeq & "' "
    '    '    Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
    '    'Next
    '    'USQL = "update COPTD set TD027='' where TD027<>'' "
    '    'Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
    '    Label1.Text = "Update Complete!"
    'End Sub
    'style="display: none"

    'Protected Sub btnPopup_Click(sender As Object, e As EventArgs) Handles btnPopup.Click
    '    SqlDataSource1.SelectCommand = "SELECT * FROM [Department] where Dept='IT'"
    '    SqlDataSource1.DataBind()
    'End Sub

    '    <cc1:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
    'PopupControlID="pnlAddEdit" TargetControlID = "lnkFake"
    'BackgroundCssClass="modalBackground">
    '</cc1:ModalPopupExtender>

    Private Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName.Equals("detail") Then
            Dim index = CInt(e.CommandArgument)
            Dim code As String = GridView1.Rows(index).Cells(1).ToolTip.Trim

        End If



        '41       If (e.CommandName.Equals("detail")) Then
        '42	            {
        '43	                int index = Convert.ToInt32(e.CommandArgument);
        '44	                string code = GridView1.DataKeys[index].Value.ToString();
        '45:
        '46	                    IEnumerable<DataRow> query = from i in dt.AsEnumerable()
        '47	                                      where i.Field<String>("Code").Equals(code)
        '48	                                       select i;
        '49	                    DataTable detailTable = query.CopyToDataTable<DataRow>();
        '50	                    DetailsView1.DataSource = detailTable;
        '51	                    DetailsView1.DataBind();
        '52	                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        '53	                    sb.Append(@"<script type='text/javascript'>");
        '54	                    sb.Append("$('#currentdetail').modal('show');");
        '55	                    sb.Append(@"</script>");
        '56	                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
        '57	                               "ModalScript", sb.ToString(), false);
        '58	 
        '59	            }
    End Sub
End Class