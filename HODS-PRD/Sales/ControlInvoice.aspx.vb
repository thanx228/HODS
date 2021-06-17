Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop

Public Class CK
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim CreateTable As New CreateTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            CreateTable.CreateControlSales()
        End If


        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("dd/MMM/yyyy")
        txtdate.Text = resultdate

    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click
        RefreshData()
    End Sub

    Private Sub RefreshData()
        If txtfrom.Text <> "" And txtto.Text <> "" And txtcid.Text = "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[SalesReInDate],[StoreDO],[StoreDoDate],[SalesReDoDate],Sa.SalesBy,Sa.Status,[RemarkSales]" & _
                " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
            " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
            " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
            " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
            " left join [DBMIS].[dbo].[ControlSales] Sa on (A.TA001 = Sa.TA001) and (A.TA002 = Sa.TA002)" & _
            " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
            " where A.TA001 not like '%EX%'" & _
            " and A.TA038 between'" & txtfrom.Text & "' and '" & txtto.Text & "'"
            Me.GridView1.DataBind()
        ElseIf txtfrom.Text <> "" And txtto.Text <> "" And txtcid.Text <> "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[SalesReInDate],[StoreDO],[StoreDoDate],[SalesReDoDate],Sa.SalesBy,Sa.Status,[RemarkSales]" & _
                 " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
             " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
             " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
             " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
             " left join [DBMIS].[dbo].[ControlSales] Sa on (A.TA001 = Sa.TA001) and (A.TA002 = Sa.TA002)" & _
             " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
             " where A.TA001 not like '%EX%'" & _
             " and A.TA038 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and A.TA004 ='" & txtcid.Text & "'"
            Me.GridView1.DataBind()
        ElseIf txttype.Text <> "" And txtno.Text <> "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[SalesReInDate],[StoreDO],[StoreDoDate],[SalesReDoDate],Sa.SalesBy,Sa.Status,[RemarkSales]" & _
               " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
           " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
           " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
           " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
           " left join [DBMIS].[dbo].[ControlSales] Sa on (A.TA001 = Sa.TA001) and (A.TA002 = Sa.TA002)" & _
           " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
           " where A.TA001 not like '%EX%'" & _
           " and A.TA001 like '%" & txttype.Text & "%' and A.TA002 like '%" & txtno.Text & "%'"
            Me.GridView1.DataBind()
        ElseIf txttype.Text = "" And txtno.Text <> "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[SalesReInDate],[StoreDO],[StoreDoDate],[SalesReDoDate],Sa.SalesBy,Sa.Status,[RemarkSales]" & _
             " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
         " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
         " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
         " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
         " left join [DBMIS].[dbo].[ControlSales] Sa on (A.TA001 = Sa.TA001) and (A.TA002 = Sa.TA002)" & _
         " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
         " where A.TA001 not like '%EX%'" & _
         " and A.TA002 like '%" & txtno.Text & "%'"
            Me.GridView1.DataBind()
        ElseIf txtcid.Text <> "" And txtno.Text <> "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[SalesReInDate],[StoreDO],[StoreDoDate],[SalesReDoDate],Sa.SalesBy,Sa.Status,[RemarkSales]" & _
            " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
        " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
        " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
        " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
        " left join [DBMIS].[dbo].[ControlSales] Sa on (A.TA001 = Sa.TA001) and (A.TA002 = Sa.TA002)" & _
        " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
        " where A.TA001 not like '%EX%'" & _
        " and A.TA002 like '%" & txtno.Text & "%' and A.TA004 ='" & txtcid.Text & "'"
            Me.GridView1.DataBind()
        ElseIf txtfrom.Text = "" And txtto.Text = "" And txtcid.Text = "" And txttype.Text = "" And txtno.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Data For Search", UpdatePanel1)
            Exit Sub

        End If

        ClearData()
    End Sub

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        RefreshData()
    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            CCode.Text = GridView1.Rows(i).Cells(1).Text.Replace("&nbsp;", "").Replace(" ", "")
            CName.Text = GridView1.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            InDate.Text = GridView1.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
            LType.Text = GridView1.Rows(i).Cells(4).Text.Replace("&nbsp;", "")
            LNo.Text = GridView1.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            LBillNo.Text = GridView1.Rows(i).Cells(10).Text.Replace("&nbsp;", "")
            StoreInvoice.Text = GridView1.Rows(i).Cells(11).Text.Replace("&nbsp;", "").Replace(" ", "")
            StoreInDate.Text = GridView1.Rows(i).Cells(12).Text.Replace("&nbsp;", "").Replace(" ", "")
            StoreDo.Text = GridView1.Rows(i).Cells(14).Text.Replace("&nbsp;", "").Replace(" ", "")
            StoreDoDate.Text = GridView1.Rows(i).Cells(15).Text.Replace("&nbsp;", "").Replace(" ", "")
            txtremark.Text = GridView1.Rows(i).Cells(18).Text.Replace("&nbsp;", "")

        End If

    End Sub

    Private Sub ClearData()
        CCode.Text = ""
        CName.Text = ""
        InDate.Text = ""
        LType.Text = ""
        LNo.Text = ""
        LBillNo.Text = ""
        txtremark.Text = ""
        StoreInvoice.Text = ""
        StoreInDate.Text = ""
        StoreDo.Text = ""
        StoreDoDate.Text = ""
    End Sub

    Protected Sub BuSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSave.Click

        If LType.Text = "" And LNo.Text = "" Then
            show_message.ShowMessage(Page, "Please Select Data", UpdatePanel1)
            Exit Sub
        ElseIf LType.Text <> "" And LNo.Text <> "" Then

            If StoreInvoice.Text = "" And StoreDo.Text = "" Then
                show_message.ShowMessage(Page, "Store No Sent Invoice & DO", UpdatePanel1)
                Exit Sub
            End If

            If StoreInvoice.Text = "Yes" And StoreInDate.Text <> "" And StoreDo.Text = "No" And StoreDoDate.Text = "" And CCode.Text <> "D005" Or CCode.Text <> "D003" Then
                Dim InSQL As String = "Insert into ControlSales([TA001],[TA002],[TA004],[TA038],[BillNo],[SalesReInDate],[SalesReDoDate],[SalesBy],[SalesDate],[Status],[RemarkSales]) "
                InSQL = InSQL & " Values('" & LType.Text & "','" & LNo.Text & "',"
                InSQL = InSQL & "'" & CCode.Text & "','" & InDate.Text & "',"
                InSQL = InSQL & "'" & LBillNo.Text & "','" & txtdate.Text & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','" & Session("UserName") & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','Close',"
                InSQL = InSQL & "'" & txtremark.Text & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)

                Dim UpStore As String = "Update ControlStore set Status='Close' where TA001='" & LType.Text & "' and TA002='" & LNo.Text & "'"
                Conn_sql.Exec_Sql(UpStore, Conn_sql.MIS_ConnectionString)

            ElseIf StoreDo.Text = "Yes" And StoreDoDate.Text <> "" And StoreInvoice.Text = "Yes" And StoreInDate.Text <> "" And CCode.Text = "D003" Then
                Dim InSQL As String = "Insert into ControlSales([TA001],[TA002],[TA004],[TA038],[BillNo],[SalesReInDate],[SalesReDoDate],[SalesBy],[SalesDate],[Status],[RemarkSales]) "
                InSQL = InSQL & " Values('" & LType.Text & "','" & LNo.Text & "',"
                InSQL = InSQL & "'" & CCode.Text & "','" & InDate.Text & "',"
                InSQL = InSQL & "'" & LBillNo.Text & "','" & txtdate.Text & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','" & Session("UserName") & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','Closed',"
                InSQL = InSQL & "'" & txtremark.Text & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)

                Dim UpStore As String = "Update ControlStore set Status='Close' where TA001='" & LType.Text & "' and TA002='" & LNo.Text & "'"
                Conn_sql.Exec_Sql(UpStore, Conn_sql.MIS_ConnectionString)

            ElseIf StoreDo.Text = "Yes" And StoreInvoice.Text = "No" And CCode.Text = "D005" Then
                Dim InSQL As String = "Insert into ControlSales([TA001],[TA002],[TA004],[TA038],[BillNo],[SalesReInDate],[SalesReDoDate],[SalesBy],[SalesDate],[Status],[RemarkSales]) "
                InSQL = InSQL & " Values('" & LType.Text & "','" & LNo.Text & "',"
                InSQL = InSQL & "'" & CCode.Text & "','" & InDate.Text & "',"
                InSQL = InSQL & "'" & LBillNo.Text & "','" & txtdate.Text & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','" & Session("UserName") & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','Closed',"
                InSQL = InSQL & "'" & txtremark.Text & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)

                Dim UpStore As String = "Update ControlStore set Status='Close' where TA001='" & LType.Text & "' and TA002='" & LNo.Text & "'"
                Conn_sql.Exec_Sql(UpStore, Conn_sql.MIS_ConnectionString)

            ElseIf StoreDo.Text = "Yes" And StoreDoDate.Text <> "" And StoreInvoice.Text = "No" And StoreInDate.Text = "" Then
                Dim InSQL As String = "Insert into ControlSales([TA001],[TA002],[TA004],[TA038],[BillNo],[SalesReInDate],[SalesReDoDate],[SalesBy],[SalesDate],[Status],[RemarkSales]) "

                InSQL = InSQL & " Values('" & LType.Text & "','" & LNo.Text & "',"
                InSQL = InSQL & "'" & CCode.Text & "','" & InDate.Text & "',"
                InSQL = InSQL & "'" & LBillNo.Text & "','" & txtdate.Text & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','" & Session("UserName") & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','Open',"
                InSQL = InSQL & "'" & txtremark.Text & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)

            ElseIf StoreInvoice.Text = "Yes" And StoreInDate.Text <> "" And StoreDo.Text = "Yes" And StoreDoDate.Text <> "" Then
                Dim InSQL As String = "Insert into ControlSales([TA001],[TA002],[TA004],[TA038],[BillNo],[SalesReInDate],[SalesReDoDate],[SalesBy],[SalesDate],[Status],[RemarkSales]) "
                InSQL = InSQL & " Values('" & LType.Text & "','" & LNo.Text & "',"
                InSQL = InSQL & "'" & CCode.Text & "','" & InDate.Text & "',"
                InSQL = InSQL & "'" & LBillNo.Text & "','" & txtdate.Text & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','" & Session("UserName") & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','Closed',"
                InSQL = InSQL & "'" & txtremark.Text & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)

                Dim UpStore As String = "Update ControlStore set Status='Close' where TA001='" & LType.Text & "' and TA002='" & LNo.Text & "'"
                Conn_sql.Exec_Sql(UpStore, Conn_sql.MIS_ConnectionString)

            End If

            ClearData()
            Me.GridView1.DataBind()
        End If
    End Sub

    Protected Sub BuPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuPrint.Click

        If DDLStatus.SelectedValue = "Open" Then

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ControlInvoice1.aspx?Status=" + DDLStatus.SelectedValue + "');", True)
        ElseIf DDLStatus.SelectedValue = "Close" Then

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ControlInvoice2.aspx?Status=" + DDLStatus.SelectedValue + "');", True)
        ElseIf DDLStatus.SelectedValue = "All" Then

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ControlInvoice3.aspx?Status=" + DDLStatus.SelectedValue + "');", True)
        End If

    End Sub

    Protected Sub BuShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuShow.Click

        Dim Sql As String
        Dim Whr As String
        Dim cust As String
        If DDLStatus.SelectedValue = "All" Then
            Whr = ""
        ElseIf DDLStatus.SelectedValue = "Open" Then
            Whr = " and S.Status = 'Open'"
        ElseIf DDLStatus.SelectedValue = "Close" Then
            Whr = " and S.Status = 'Close'"
        End If

        If txtrecust.Text = "" Then
            cust = ""
        ElseIf txtrecust.Text <> "" Then
            cust = " and TA004='" & txtrecust.Text & "'"
        End If

        Sql = "select TA001 as Type,TA002 as No,TA004 as Customer,TA038 as Date,BillNo,StoreInvoice as Invoice,StoreInDate as InvoiceDate,StoreDO as DO,StoreDoDate as DoDate,Status,RemarkStore as Remark" & _
            " from [DBMIS].[dbo].[ControlStore] S" & _
            " WHERE EXISTS(select * from [JINPAO80].[dbo].[ACRTA] I" & _
            " where S.TA001 = I.TA001 and S.TA002 = I.TA002) " & _
            "  " & Whr & " " & cust & ""
        ControlForm.ShowGridView(GridView3, Sql, Conn_sql.MIS_ConnectionString)
        lbCount.Text = "Number Of " & GridView3.Rows.Count & " Item"

    End Sub

        'ControlForm.ExportGridViewToExcel("Invoice", GridView3)
        ' ExportData("application/vnd.xls", "Delay.xls")

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Sub ExportData(ByVal _contentType As String, ByVal fileName As String)

        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = _contentType
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        frm.Attributes("runat") = "server"
        frm.Controls.Add(GridView3)
        GridView3.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Status As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Status")).Replace(" ", "")
            If Status = "Close" Then
                e.Row.BackColor = System.Drawing.Color.DarkSeaGreen
                'ElseIf Status = "U" Then
                '    e.Row.BackColor = Drawing.Color.Red
                'ElseIf Status = " " Or Status = "NULL" Then
                '    e.Row.BackColor = Drawing.Color.White
            End If
        End If

    End Sub

End Class