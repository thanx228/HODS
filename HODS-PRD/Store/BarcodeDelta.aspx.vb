Imports OnBarcode.Barcode
Imports System.IO
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Text
Imports System.Collections.Generic
Imports System.ComponentModel

Public Class BarcodeDelta
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("Login.aspx")
            End If
        End If

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("yyMMdd")
        txtdate.Text = resultdate

        GenAutoNumber()
    End Sub

    Private Sub GenAutoNumber()

        Dim program As Data.DataTable
        Dim docstr As String = Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        program = Conn_sql.Get_DataReader("SELECT isnull(max(SNo),0) FROM LabelDELTA where SNo like '%" & docstr & "%'", Conn_sql.MIS_ConnectionString)
        If program.Rows(0).Item(0) = 0 Then

            SNo.Text = docstr '& "01"
            RNo.Text = "000001"
            txtsnoshow.Text = SNo.Text & RNo.Text
        Else
            SNo.Text = CInt(program.Rows(0).Item(0))

            Dim GRNo As String = Conn_sql.Get_value("SELECT isnull(max(RNo),0) FROM LabelDELTA where SNo like '%" & docstr & "%'", Conn_sql.MIS_ConnectionString)
            RNo.Text = "00000" & GRNo + 1
            txtsnoshow.Text = SNo.Text & RNo.Text
        End If
    End Sub

    Protected Sub Busearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busearch.Click

        If txtsearch.Text <> "" Then
            If DDLSearch.SelectedIndex = 0 Then     'Invoice No
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'D003' and TA002='" & txtsearch.Text & "'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 1 Then 'PO
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'D003' and TB048 like '" & txtsearch.Text & "%'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 2 Then 'Desc
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'D003' and TB040 like '" & txtsearch.Text & "%'"
                GridView1.DataBind()
            ElseIf DDLSearch.SelectedIndex = 3 Then  'Spec
                DataSourceInvoice.SelectCommand = "select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'D003' and TB041 like '" & txtsearch.Text & "%'"
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
            Ltype.Text = GridView1.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            Lno.Text = GridView1.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Lvender.Text = GridView1.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
            Litem.Text = GridView1.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            Lpn.Text = GridView1.Rows(i).Cells(7).Text.Replace("&nbsp;", "").Replace("STJ-", "").Replace("CNC-", "").Substring(0, 10)
            txtpo.Text = GridView1.Rows(i).Cells(8).Text.Replace("&nbsp;", "")
            Lqty.Text = GridView1.Rows(i).Cells(9).Text.Replace(".000000", ".00")

            TypeNo.Text = Ltype.Text.Replace(" ", "") & Lno.Text.Replace(" ", "")
        End If

        GenAutoNumber()
        txtplant.Text = "PSB"
        txtplant.Focus()
    End Sub

    Private Sub ClearData()
        Ltype.Text = ""
        Lno.Text = ""
        Lvender.Text = ""
        Litem.Text = ""
        Lpn.Text = ""
        Lqty.Text = ""
        RNo.Text = ""
        txtplant.Text = ""
        txtQBox.Text = ""
        Lfbox.Text = ""
        Llbox.Text = ""
        Llqty.Text = ""

        txtsnoshow.Text = ""
        TypeNo.Text = ""
        txtpo.Text = ""
        SNo.Text = ""
    End Sub

    Protected Sub SearchDel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SearchDel.Click

        If txtdel.Text <> "" Then
            If DDLDel.SelectedIndex = 0 Then     'Po
                DataSourceDELTA.SelectCommand = "select * from LabelDELTA where Po='" & txtdel.Text & "'"
                GridDELTA.DataBind()
            ElseIf DDLDel.SelectedIndex = 1 Then 'Desc
                DataSourceDELTA.SelectCommand = "select * from LabelDELTA where [Desc] like'" & txtdel.Text & "%'"
                GridDELTA.DataBind()
            ElseIf DDLDel.SelectedIndex = 2 Then 'Invoice No
                DataSourceDELTA.SelectCommand = "select * from LabelDELTA where No like'" & txtdel.Text & "%'"
                GridDELTA.DataBind()
            ElseIf DDLDel.SelectedIndex = 3 Then  'Searil No
                DataSourceDELTA.SelectCommand = "select * from LabelDELTA where SNoShow like'" & txtdel.Text & "%'"
                GridDELTA.DataBind()
            End If
        Else

            GridDELTA.DataBind()
        End If

    End Sub

    Private Sub GridDELTA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDELTA.RowCommand

        If e.CommandName = "OnPrint" Then
            Dim i As Integer = e.CommandArgument

            Dim No As String = GridDELTA.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
            Dim Item As String = GridDELTA.Rows(i).Cells(1).Text.Replace("&nbsp;", "")

            Dim SNoShow As String = GridDELTA.Rows(i).Cells(6).Text.Replace("&nbsp;", "")

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Delta1.aspx?SNo=" + SNoShow + "&Item=" + Item + "');", True)
        End If

    End Sub
   
    Private Sub txtQBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQBox.TextChanged

        Dim IQty As Double = CDbl(Lqty.Text)
        Dim QBox As Double = CInt(txtQBox.Text)

        Lfbox.Text = CInt(IQty \ QBox)
        Llqty.Text = IQty - (QBox * Lfbox.Text)

        If Llqty.Text = 0 Then
            Llbox.Text = "0"
        Else
            Llbox.Text = "1"
        End If

    End Sub

    Protected Sub Busave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busave.Click

        If CInt(txtQBox.Text) > Lqty.Text Then
            show_message.ShowMessage(Page, "Please Check Qty", UpdatePanel1)
            Exit Sub
        ElseIf txtQBox.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Qty", UpdatePanel1)
            Exit Sub
        End If

        For M_count As Integer = 1 To Lfbox.Text
            Dim InSQL As String = "Insert into LabelDELTA(Type,No,CID,Item,[Desc],Qty,Plant,Unit,Sup,yymmdd,TradCode,Po,SNo,RNo,SNoShow,CreateBy,TotalQty)"
            InSQL = InSQL & " Values('" & Ltype.Text.Replace(" ", "") & "','" & TypeNo.Text & "','" & Lvender.Text.Replace(" ", "") & "',"
            InSQL = InSQL & "'" & Litem.Text.Replace(" ", "") & "','" & Lpn.Text & "',"
            InSQL = InSQL & "'" & txtQBox.Text & "','" & txtplant.Text & "','pce','556400','" & txtdate.Text & "',"
            InSQL = InSQL & "'02','" & txtpo.Text.Replace(" ", "") & "','" & SNo.Text & "','" & RNo.Text & "','" & txtsnoshow.Text & "','" & Session("Username") & "','" & Lqty.Text & "')"
            Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
        Next
        For N_count As Integer = 1 To Llbox.Text
            Dim SqlLBox As String = "Insert into LabelDELTA(Type,No,CID,Item,[Desc],Qty,Plant,Unit,Sup,yymmdd,TradCode,Po,SNo,RNo,SNoShow,CreateBy,TotalQty)"
            SqlLBox = SqlLBox & " Values('" & Ltype.Text.Replace(" ", "") & "','" & TypeNo.Text & "','" & Lvender.Text.Replace(" ", "") & "',"
            SqlLBox = SqlLBox & "'" & Litem.Text.Replace(" ", "") & "','" & Lpn.Text & "',"
            SqlLBox = SqlLBox & "'" & Llqty.Text & "','" & txtplant.Text & "','pce','556400','" & txtdate.Text & "',"
            SqlLBox = SqlLBox & "'02','" & txtpo.Text.Replace(" ", "") & "','" & SNo.Text & "','" & RNo.Text & "','" & txtsnoshow.Text & "','" & Session("Username") & "','" & Lqty.Text & "')"
            Conn_sql.Exec_Sql(SqlLBox, Conn_sql.MIS_ConnectionString)
        Next

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('Delta1.aspx?SNo=" + txtsnoshow.Text + "&Item=" + Litem.Text + "');", True)
        ClearData()
        GridDELTA.DataBind()

    End Sub

End Class