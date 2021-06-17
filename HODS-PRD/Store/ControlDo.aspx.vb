Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class ControlDo
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL
    Dim CreateTable As New CreateTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            CreateTable.CreateControlStore()
        End If

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("dd/MMM/yyyy")
        txtdate.Text = resultdate

    End Sub

    Protected Sub BUSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BUSearch.Click

        RefreshData()

    End Sub

    Private Sub RefreshData()
        Dim Sta As String
        If DDLStatus.SelectedValue = "All" Then
            Sta = " and Status is null "
        ElseIf DDLStatus.SelectedValue = "Open" Then
            Sta = " and Status = 'Open'"
        ElseIf DDLStatus.SelectedValue = "Close" Then
            Sta = " and Status = 'Close'"
        End If

        If txtfrom.Text <> "" And txtto.Text <> "" And txtcust.Text = "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[StoreDO],[StoreDoDate],[Status],[StoreBy],[RemarkStore]" & _
                " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L" & _
                " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
                " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
                " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
                " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
                " where A.TA001 not like '%EX%'" & _
                " and A.TA038 between '" & txtfrom.Text & "' and '" & txtto.Text & "'" & Sta
            Me.GridView1.DataBind()

        ElseIf txtfrom.Text <> "" And txtto.Text <> "" And txtcust.Text <> "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[StoreDO],[StoreDoDate],[Status],[StoreBy],[RemarkStore]" & _
                " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
                " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
                " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
                " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
                " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
                " where A.TA001 not like '%EX%'" & _
                " and A.TA038 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and A.TA004 ='" & txtcust.Text & "' & Sta & "
            Me.GridView1.DataBind()

        ElseIf txttype.Text <> "" And txtno.Text <> "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[StoreDO],[StoreDoDate],[Status],[StoreBy],[RemarkStore]" & _
            " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
            " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
            " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
            " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
            " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
            " where A.TA001 not like '%EX%'" & _
            " and A.TA001 like '%" & txttype.Text & "%' and A.TA002 like '%" & txtno.Text & "%' & Sta &"

            Me.GridView1.DataBind()
        ElseIf txttype.Text = "" And txtno.Text <> "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[StoreDO],[StoreDoDate],[Status],[StoreBy],[RemarkStore]" & _
                " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
                " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
                " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
                " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
                " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
                " where A.TA001 not like '%EX%'" & _
                " and A.TA002 like '%" & txtno.Text & "%' & Sta &"

            Me.GridView1.DataBind()
        ElseIf txtcust.Text <> "" And txtno.Text <> "" Then
            SqlDataSource1.SelectCommand = "select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[StoreDO],[StoreDoDate],[Status],[StoreBy],[RemarkStore]" & _
            " from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L " & _
            " on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)" & _
            " left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)" & _
            " left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)" & _
            " left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) " & _
            " where A.TA001 not like '%EX%'" & _
    " and A.TA002 like '%" & txtno.Text & "%' and A.TA004 ='" & txtcust.Text & "' & Sta &"

            Me.GridView1.DataBind()
        ElseIf txtfrom.Text = "" And txtto.Text = "" And txtcust.Text = "" And txttype.Text = "" And txtno.Text = "" Then
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
            CID.Text = GridView1.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            CName.Text = GridView1.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            TA038.Text = GridView1.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
            Type.Text = GridView1.Rows(i).Cells(4).Text.Replace("&nbsp;", "")
            No.Text = GridView1.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            BillNo.Text = GridView1.Rows(i).Cells(10).Text.Replace("&nbsp;", "")

            DDLInvoice.SelectedValue = GridView1.Rows(i).Cells(11).Text.Replace("&nbsp;", "")
            DateInvoice.Text = GridView1.Rows(i).Cells(12).Text.Replace("&nbsp;", "")
            DDLDO.SelectedValue = GridView1.Rows(i).Cells(13).Text.Replace("&nbsp;", "")
            DateDo.Text = GridView1.Rows(i).Cells(14).Text.Replace("&nbsp;", "")

        End If

    End Sub

    Private Sub ClearData()

        CID.Text = ""
        CName.Text = ""
        Type.Text = ""
        No.Text = ""
        DDLDO.SelectedIndex = 0
        DDLInvoice.SelectedIndex = 0
        txtremark.Text = ""
        TA038.Text = ""
        BillNo.Text = ""

    End Sub

    Protected Sub BuSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSave.Click

        If Type.Text = "" And No.Text = "" Then
            show_message.ShowMessage(Page, "กรุณาเลือกข้อมูล", UpdatePanel1)
            Exit Sub
        ElseIf Type.Text <> "" And No.Text <> "" Then

            If DDLDO.SelectedIndex = 0 And DDLInvoice.SelectedIndex = 0 Then
                show_message.ShowMessage(Page, "กรุณาเลือกเอกสาร Invoice หรือ DO", UpdatePanel1)
                Exit Sub
            End If

            'Close Open ""

            Dim aaa As String = "select Replace(Status,' ','') from [DBMIS].[dbo].[ControlStore] where TA001 = '" & Type.Text & "' and TA002 = '" & No.Text & "'"
            Dim CheckInvoice As String = Conn_sql.Get_value(aaa, Conn_sql.MIS_ConnectionString)

            If CheckInvoice = "Close" Then
                show_message.ShowMessage(Page, "Invoice No. นี้ถูกทำ Close แล้ว", UpdatePanel1)
                Exit Sub
            ElseIf CheckInvoice = "Open" Then
                Dim UpSql As String
                If DDLInvoice.SelectedValue = "No" Then
                    DateInvoice.Text = ""
                End If
                If DDLDO.SelectedValue = "No" Then
                    DateDo.Text = ""
                End If

                'UpSql = "update ControlStore set StoreInvoice='" & DDLInvoice.SelectedValue & "' ," & _
                '" StoreInDate='" & DateInvoice.Text & "', StoreDO='" & DDLDO.SelectedValue & "' ," & _
                '" StoreDoDate='" & DateDo.Text & "', EditBy='" & Session("UserName") & "' ," & _
                '" EditDate='" & txtdate.Text & "' where TA001='" & Type.Text & "' and TA002='" & No.Text & "'"

                'Conn_sql.Exec_Sql(UpSql, Conn_sql.MIS_ConnectionString)

                'ClearData()
                'GridView1.DataBind()

            ElseIf CheckInvoice = "" Then

                Dim DateDo As String
                Dim DateInvoice As String
                If DDLDO.SelectedValue = "No" Then
                    DateDo = ""
                ElseIf DDLDO.SelectedValue = "Yes" Then
                    DateDo = txtdate.Text
                End If
                If DDLInvoice.SelectedValue = "No" Then
                    DateInvoice = ""
                ElseIf DDLInvoice.SelectedValue = "Yes" Then
                    DateInvoice = txtdate.Text
                End If
                Dim InSQL As String = "Insert into ControlStore([TA001],[TA002],[TA004],[TA038],[BillNo],[StoreInvoice],[StoreInDate],[StoreDO],[StoreDoDate],[StoreBy],[StoreDate],[Status],[RemarkStore]) "
                InSQL = InSQL & " Values('" & Type.Text & "','" & No.Text & "',"
                InSQL = InSQL & "'" & CID.Text & "','" & TA038.Text & "',"
                InSQL = InSQL & "'" & BillNo.Text & "','" & DDLInvoice.SelectedValue & "',"
                InSQL = InSQL & "'" & DateInvoice & "','" & DDLDO.SelectedValue & "',"
                InSQL = InSQL & "'" & DateDo & "','" & Session("UserName") & "',"
                InSQL = InSQL & "'" & txtdate.Text & "','Open',"
                InSQL = InSQL & "'" & txtremark.Text & "')"
                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)

                ClearData()
                GridView1.DataBind()
            End If

        End If
    End Sub

    Protected Sub BuCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuCancel.Click
        ClearData()
    End Sub

    Protected Sub BuReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuReport.Click

        InsertData()
    End Sub

    Private Sub InsertData()

        Dim DelSalesInvoice As String = "delete from SalesInvoice"
        Conn_sql.Exec_Sql(DelSalesInvoice, Conn_sql.MIS_ConnectionString)
        Dim SelSQL As String = ""

        If txtfrom.Text <> "" And txtto.Text <> "" And txtcust.Text = "" Then
            SelSQL = SelSQL & "select TA038,TA004,MA002,TA001,TA002 from ACRTA A " & _
            " left join COPMA C on (A.TA004 = C.MA001)" & _
            " where TA038 between '" & txtfrom.Text & "' and '" & txtto.Text & "'"
        ElseIf txtfrom.Text <> "" And txtto.Text <> "" And txtcust.Text <> "" Then
            SelSQL = SelSQL & "select TA038,TA004,MA002,TA001,TA002 from ACRTA A " & _
           " left join COPMA C on (A.TA004 = C.MA001)" & _
           " where TA038 between '" & txtfrom.Text & "' and '" & txtto.Text & "' and TA004 = '" & txtcust.Text & "'"
        End If

        Dim aa As New Data.DataTable
        aa = Conn_sql.Get_DataReader(SelSQL, Conn_sql.ERP_ConnectionString)
        If aa.Rows.Count > 0 Then
            For i As Integer = 0 To aa.Rows.Count - 1
                Dim TA038 As String = aa.Rows(i).Item("TA038").ToString.Replace(" ", "")
                Dim TA004 As String = aa.Rows(i).Item("TA004").ToString.Replace(" ", "")
                Dim MA002 As String = aa.Rows(i).Item("MA002").ToString.Replace(" ", "")
                Dim TA001 As String = aa.Rows(i).Item("TA001").ToString.Replace(" ", "")
                Dim TA002 As String = aa.Rows(i).Item("TA002").ToString.Replace(" ", "")
                Dim Invoice As String = TA001 & "-" & TA002
                Dim InSQL As String = "Insert into SalesInvoice(TA038,TA004,MA002,Invoice) "
                InSQL = InSQL & " Values('" & TA038 & "','" & TA004 & "',"
                InSQL = InSQL & "'" & MA002 & "','" & Invoice & "')"

                Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
            Next
        End If

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ControlDo1.aspx?CID=" + txtcust.Text + "');", True)

    End Sub
End Class