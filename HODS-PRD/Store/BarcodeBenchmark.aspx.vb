Public Class BarcodeBenchmark
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Busearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busearch.Click

        If txtsearch.Text <> "" Then
            If DDLSearch.SelectedIndex = 0 Then     'Invoice No
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'B004' and TA002='" & txtsearch.Text & "'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 1 Then 'PO
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'B004' and TB048 like '" & txtsearch.Text & "%'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 2 Then 'Desc
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'B004' and TB040 like '" & txtsearch.Text & "%'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 3 Then  'Spec
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'B004' and TB041 like '" & txtsearch.Text & "%'"
                GridView1.DataBind()
            End If
        Else
            GridView1.DataBind()
        End If

    End Sub

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        ClearData()
    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            Litype.Text = GridView1.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            Lino.Text = GridView1.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Lcust.Text = GridView1.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
            Litem.Text = GridView1.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            Ldesc.Text = GridView1.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
            Lspec.Text = GridView1.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
            Lpo.Text = GridView1.Rows(i).Cells(8).Text.Replace("&nbsp;", "")
            Lqty.Text = GridView1.Rows(i).Cells(9).Text.Replace(".000000", ".00")
            txtPartCode.Text = GridView1.Rows(i).Cells(7).Text.Replace("&nbsp;", "")

        End If

        'If e.CommandName = "OnPrint" Then
        '    Dim i As Integer = e.CommandArgument
        '    Dim Type As String = GridView1.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
        '    Dim No As String = GridView1.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
        '    Dim Item As String = GridView1.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
        '    Dim Desc As String = GridView1.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
        '    Dim Spec As String = GridView1.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
        '    Dim Po As String = GridView1.Rows(i).Cells(8).Text.Replace("&nbsp;", "")
        '    Dim Qty As String = GridView1.Rows(i).Cells(9).Text.Replace("&nbsp;", "")
        '    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Benchmark1.aspx?No=" + No + "&Type=" + Type + "&Item=" + Item + "');", True)
        'End If

    End Sub

    Protected Sub BuPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuPrint.Click

        If CInt(txtqbox.Text) > Lqty.Text Then
            show_message.ShowMessage(Page, "Please Check Qty", UpdatePanel1)
            Exit Sub
        ElseIf txtqbox.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Qty", UpdatePanel1)
            Exit Sub
        End If

        Dim DelLabeleBM As String = "delete from LabeleBM"
        Conn_sql.Exec_Sql(DelLabeleBM, Conn_sql.MIS_ConnectionString)

        Dim Baino As String = "*" & Lino.Text.Replace(" ", "") & "*"
        Dim Bapo As String = "*" & Lpo.Text.Replace(" ", "") & "*"
        Dim Badesc As String = "*" & Ldesc.Text.Replace(" ", "") & "*"

        Dim BaQBox As String = "*" & txtqbox.Text.Replace(" ", "") & "*"
        Dim BaLlqty As String = "*" & Llqty.Text.Replace(" ", "") & "*"

        Dim Bapartcust As String = "*" & txtPartCode.Text.Replace(" ", "") & "*"
        Dim Balot As String = "*" & txtlotno.Text.Replace(" ", "") & "*"
        Dim Badatecode As String = "*" & txtdatecode.Text.Replace(" ", "") & "*"
       
        For M_count As Integer = 1 To Lfbox.Text
            Dim InSQL As String = "Insert into LabeleBM(Type,No,CID,Po,lot,Item,[Desc],Spec,Qty,DateCode,PartCust,Baino,Bapo,Badesc,BaQty,Bapartcust,Balot,Badatecode)"
            InSQL = InSQL & " Values('" & Litype.Text & "','" & Lino.Text & "','" & Lcust.Text & "',"
            InSQL = InSQL & "'" & Lpo.Text & "','" & txtlotno.Text & "',"
            InSQL = InSQL & "'" & Litem.Text & "','" & Ldesc.Text & "',"
            InSQL = InSQL & "'" & Lspec.Text & "','" & txtqbox.Text & "','" & txtdatecode.Text & "','" & txtPartCode.Text & "',"
            InSQL = InSQL & "'" & Baino.Replace(" ", "") & "','" & Bapo.Replace(" ", "") & "','" & Badesc & "','" & BaQBox & "','" & Bapartcust & "','" & Balot & "','" & Badatecode & "')"
            Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
        Next
        For N_count As Integer = 1 To Llbox.Text
            Dim SqlLBox As String = "Insert into LabeleBM(Type,No,CID,Po,lot,Item,[Desc],Spec,Qty,DateCode,PartCust,Baino,Bapo,Badesc,BaQty,Bapartcust,Balot,Badatecode)"
            SqlLBox = SqlLBox & " Values('" & Litype.Text & "','" & Lino.Text & "','" & Lcust.Text & "',"
            SqlLBox = SqlLBox & "'" & Lpo.Text & "','" & txtlotno.Text & "',"
            SqlLBox = SqlLBox & "'" & Litem.Text & "','" & Ldesc.Text & "',"
            SqlLBox = SqlLBox & "'" & Lspec.Text & "','" & Llqty.Text & "','" & txtdatecode.Text & "','" & txtPartCode.Text & "',"
            SqlLBox = SqlLBox & "'" & Baino.Replace(" ", "") & "','" & Bapo.Replace(" ", "") & "','" & Badesc & "','" & BaLlqty & "','" & Bapartcust & "','" & Balot & "','" & Badatecode & "')"
            Conn_sql.Exec_Sql(SqlLBox, Conn_sql.MIS_ConnectionString)
        Next

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Benchmark1.aspx?No=" + Lino.Text + "&Type=" + Litype.Text + "&Item=" + Litem.Text + "');", True)
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

        Litype.Text = ""
        Lino.Text = ""
        Lcust.Text = ""
        Lpo.Text = ""
        txtlotno.Text = ""
        txtPartCode.Text = ""
        Litem.Text = ""
        Ldesc.Text = ""
        Lspec.Text = ""
        Lqty.Text = ""
        txtdatecode.Text = ""
        Lfbox.Text = ""
        Llbox.Text = ""
        Llqty.Text = ""
        txtqbox.Text = ""
    End Sub
End Class