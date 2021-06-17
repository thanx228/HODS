Public Class BarcodeFabrinet
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        ClearData()
    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            Ltype.Text = GridView1.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            Lno.Text = GridView1.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Lcust.Text = GridView1.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
            Litem.Text = GridView1.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            Ldesc.Text = GridView1.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
            Lspec.Text = GridView1.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
            Lpo.Text = GridView1.Rows(i).Cells(8).Text.Replace("&nbsp;", "")
            Lqty.Text = GridView1.Rows(i).Cells(9).Text.Replace(".000000", ".00")
        End If

        If e.CommandName = "OnPrint" Then
            Dim i As Integer = e.CommandArgument
            Dim Type As String = GridView1.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            Dim No As String = GridView1.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Dim Item As String = GridView1.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            Dim Desc As String = GridView1.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
            Dim Spec As String = GridView1.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
            Dim Po As String = GridView1.Rows(i).Cells(8).Text.Replace("&nbsp;", "")
            Dim Qty As String = GridView1.Rows(i).Cells(9).Text.Replace("&nbsp;", "")
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Fabrinet1.aspx?No=" + No + "&Type=" + Type + "&Item=" + Item + "');", True)

        End If

    End Sub

    Protected Sub Busearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busearch.Click

        If txtsearch.Text <> "" Then
            If DDLSearch.SelectedIndex = 0 Then     'Invoice No
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'F005' and TA002='" & txtsearch.Text & "'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 1 Then 'PO
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'F005' and TB048 like '" & txtsearch.Text & "%'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 2 Then 'Desc
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'F005' and TB040 like '" & txtsearch.Text & "%'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 3 Then  'Spec
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'F005' and TB041 like '" & txtsearch.Text & "%'"
                GridView1.DataBind()
            
            End If

        End If
    End Sub

    Protected Sub Buprint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buprint.Click
        If txtqbox.Text > Lqty.Text Then
            show_message.ShowMessage(Page, "Please Check Qty", UpdatePanel1)
            Exit Sub
        ElseIf txtqbox.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Qty", UpdatePanel1)
            Exit Sub
        End If

        Dim DelFab As String = "delete from LabelFab"
        Conn_sql.Exec_Sql(DelFab, Conn_sql.MIS_ConnectionString)

        Dim qtyprint As Integer = lfbox.Text + llbox.Text

        Dim Baspec As String = "*" & Lspec.Text.Replace(" ", "") & "*"
        Dim Badesc As String = "*" & Ldesc.Text.Replace(" ", "") & "*"
        Dim BaSup As String = "*Jinpaoprecision*"   '  industry Co.,Ltd.
        Dim BaQtyBox As String = "*" & txtqbox.Text.Replace(" ", "") & "*"
        Dim BaLastQty As String = "*" & llqty.Text.Replace(" ", "") & "*"

        For M_count As Integer = 1 To lfbox.Text
            Dim InSQL As String = "Insert into LabelFab(Type,No,CID,Po,Item,[Desc],Spec,Qty,Baspec,Badesc,Basup,BaQty)"
            InSQL = InSQL & " Values('" & Ltype.Text.Replace(" ", "") & "','" & Lno.Text.Replace(" ", "") & "','" & Lcust.Text.Replace(" ", "") & "',"
            InSQL = InSQL & "'" & Lpo.Text.Replace(" ", "") & "','" & Litem.Text.Replace(" ", "") & "',"
            InSQL = InSQL & "'" & Ldesc.Text & "','" & Lspec.Text & "',"
            InSQL = InSQL & "'" & txtqbox.Text & "',"
            InSQL = InSQL & "'" & Baspec & "','" & Badesc & "','" & BaSup & "','" & BaQtyBox & "')"
            Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
        Next
        For N_count As Integer = 1 To llbox.Text
            Dim SqlLBox As String = "Insert into LabelFab(Type,No,CID,Po,Item,[Desc],Spec,Qty,Baspec,Badesc,Basup,BaQty)"
            SqlLBox = SqlLBox & " Values('" & Ltype.Text.Replace(" ", "") & "','" & Lno.Text.Replace(" ", "") & "','" & Lcust.Text.Replace(" ", "") & "',"
            SqlLBox = SqlLBox & "'" & Lpo.Text.Replace(" ", "") & "','" & Litem.Text.Replace(" ", "") & "',"
            SqlLBox = SqlLBox & "'" & Ldesc.Text & "','" & Lspec.Text & "',"
            SqlLBox = SqlLBox & "'" & llqty.Text & "',"
            SqlLBox = SqlLBox & "'" & Baspec & "','" & Badesc & "','" & BaSup & "','" & BaLastQty & "')"
            Conn_sql.Exec_Sql(SqlLBox, Conn_sql.MIS_ConnectionString)
        Next

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Fab1.aspx?No=" + Lno.Text.Replace(" ", "") + "&Type=" + Ltype.Text.Replace(" ", "") + "&Item=" + Litem.Text + "');", True)
        ClearData()
    End Sub

    Private Sub txtqbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtqbox.TextChanged

        Dim IQty As Double = CDbl(Lqty.Text)
        Dim QBox As Double = CInt(txtqbox.Text)

        lfbox.Text = CInt(IQty \ QBox)
        llqty.Text = IQty - (QBox * lfbox.Text)

        If llqty.Text = 0 Then
            llbox.Text = "0"
        Else
            llbox.Text = "1"
        End If
    End Sub

    Private Sub ClearData()
        Ltype.Text = ""
        Lno.Text = ""
        Lpo.Text = ""
        Lcust.Text = ""
        txtqbox.Text = ""
        Litem.Text = ""
        Ldesc.Text = ""
        Lspec.Text = ""
        Lqty.Text = ""
        lfbox.Text = ""
        llbox.Text = ""
        llqty.Text = ""
    End Sub
End Class