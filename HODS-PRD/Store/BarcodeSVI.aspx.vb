Public Class Barcode
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Busearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busearch.Click
       
        If txtsearch.Text <> "" Then
            If DDLSearch.SelectedIndex = 0 Then     'Invoice No
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA002='" & txtsearch.Text & "'"
                GridView2.DataBind()
            ElseIf DDLSearch.SelectedIndex = 1 Then 'PO
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TB048 like '" & txtsearch.Text & "%'"
                GridView2.DataBind()
            ElseIf DDLSearch.SelectedIndex = 2 Then 'Desc
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TB040 like '" & txtsearch.Text & "%'"
                GridView2.DataBind()
            ElseIf DDLSearch.SelectedIndex = 3 Then  'Spec
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TB041 like '" & txtsearch.Text & "%'"
                GridView2.DataBind()
            ElseIf DDLSearch.SelectedIndex = 4 Then  'Customer
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004='" & txtsearch.Text & "'"
                GridView2.DataBind()
            End If
        End If

    End Sub

    Private Sub GridView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        ClearData()
    End Sub

    Private Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            Ltype.Text = GridView2.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            Linvoice.Text = GridView2.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Lsup.Text = GridView2.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
            Litem.Text = GridView2.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            Ldesc.Text = GridView2.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
            Lspec.Text = GridView2.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
            Lpo.Text = GridView2.Rows(i).Cells(8).Text.Replace("&nbsp;", "")
            Lqty.Text = GridView2.Rows(i).Cells(9).Text.Replace(".000000", ".00")
        End If
        If e.CommandName = "OnPrint" Then
            Dim i As Integer = e.CommandArgument
            Dim Type As String = GridView2.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            Dim No As String = GridView2.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Dim Item As String = GridView2.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            Dim Desc As String = GridView2.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
            Dim Spec As String = GridView2.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
            Dim Po As String = GridView2.Rows(i).Cells(8).Text.Replace("&nbsp;", "")
            Dim Qty As String = GridView2.Rows(i).Cells(9).Text.Replace("&nbsp;", "")
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('SVI1.aspx?No=" + No + "&Type=" + Type + "&Item=" + Item + "');", True)

        End If

    End Sub

    Protected Sub BuCal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuCal.Click
        Dim IQty As Double = CDbl(Lqty.Text)
        Dim QBox As Double = CInt(txtqbox.Text)

        Lfbox.Text = CInt(IQty \ QBox)
        Llqty.Text = IQty - (QBox * Lfbox.Text)

        If Llqty.Text = 0 Then
            Llbox.Text = "0"
        Else
            Llbox.Text = "1"
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

        Dim DelLabeleSVI As String = "delete from LabelSVI"
        Conn_sql.Exec_Sql(DelLabeleSVI, Conn_sql.MIS_ConnectionString)

        Dim qtyprint As Integer = Lfbox.Text + Llbox.Text

        Dim Baino As String = "*" & Linvoice.Text.Replace(" ", "") & "*"
        Dim Bapo As String = "*" & Lpo.Text.Replace(" ", "") & "*"
        Dim Baspec As String = "*" & Lspec.Text.Replace(" ", "") & "*"
        Dim BaQty As String = "*" & Lqty.Text.Replace(" ", "") & "*"
       
        For M_count As Integer = 1 To Lfbox.Text
            Dim InSQL As String = "Insert into LabelSVI(Type,No,CID,Po,Item,[Desc],Spec,Qty,Baino,Bapo,Badesc,BaQty)" ',Baino,Bapo,Badesc,BaQty,Bapartcust,Balot,Badatecode
            InSQL = InSQL & " Values('" & Ltype.Text.Replace(" ", "") & "','" & Linvoice.Text.Replace(" ", "") & "','" & Lsup.Text.Replace(" ", "") & "',"
            InSQL = InSQL & "'" & Lpo.Text.Replace(" ", "") & "','" & Litem.Text.Replace(" ", "") & "',"
            InSQL = InSQL & "'" & Ldesc.Text & "','" & Lspec.Text & "',"
            InSQL = InSQL & "'" & txtqbox.Text & "',"
            InSQL = InSQL & "'" & Baino & "','" & Bapo & "','" & Baspec & "','" & BaQty & "')"
            Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
        Next
        For N_count As Integer = 1 To Llbox.Text
            Dim SqlLBox As String = "Insert into LabelSVI(Type,No,CID,Po,Item,[Desc],Spec,Qty,Baino,Bapo,Badesc,BaQty)" ',Baino,Bapo,Badesc,BaQty,Bapartcust,Balot,Badatecode
            SqlLBox = SqlLBox & " Values('" & Ltype.Text.Replace(" ", "") & "','" & Linvoice.Text.Replace(" ", "") & "','" & Lsup.Text.Replace(" ", "") & "',"
            SqlLBox = SqlLBox & "'" & Lpo.Text.Replace(" ", "") & "','" & Litem.Text.Replace(" ", "") & "',"
            SqlLBox = SqlLBox & "'" & Ldesc.Text & "','" & Lspec.Text & "',"
            SqlLBox = SqlLBox & "'" & Llqty.Text & "',"
            SqlLBox = SqlLBox & "'" & Baino & "','" & Bapo & "','" & Baspec & "','" & BaQty & "')"
            Conn_sql.Exec_Sql(SqlLBox, Conn_sql.MIS_ConnectionString)
        Next
       
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('SVI1.aspx?No=" + Linvoice.Text.Replace(" ", "") + "&Type=" + Ltype.Text.Replace(" ", "") + "&Item=" + Litem.Text + "');", True)
        ClearData()
    End Sub

    Private Sub txtqbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtqbox.TextChanged

        Dim IQty As Double = CDbl(Lqty.Text)
        Dim QBox As Double = CInt(txtqbox.Text)

        Lfbox.Text = CInt(IQty \ QBox)
        Llqty.Text = IQty - (QBox * Lfbox.Text)

        If Llqty.Text = 0 Then
            Llbox.Text = "0"
        Else
            Llbox.Text = "1"
        End If
    End Sub

    Private Sub ClearData()
        Ltype.Text = ""
        Linvoice.Text = ""
        Lpo.Text = ""
        Lsup.Text = ""
        txtqbox.Text = ""
        Litem.Text = ""
        Ldesc.Text = ""
        Lspec.Text = ""
        Lqty.Text = ""
        Lfbox.Text = ""
        Llbox.Text = ""

    End Sub
End Class