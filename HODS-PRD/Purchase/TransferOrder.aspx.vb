Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class TransferOrder
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL
    Dim CreateDate As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If

            Dim strSql As String = "SELECT MQ001,MQ001+' : '+MQ002 AS ConcatField from CMSMQ where MQ003 = 'D2' order by MQ001"
            Dim program As Data.DataTable = Conn_sql.Get_DataReader(strSql, Conn_sql.ERP_ConnectionString)
            Dtype1.DataSource = program
            Dtype1.DataTextField = "ConcatField"
            Dtype1.DataValueField = "MQ001"
            Dtype1.DataBind()

            DType2.DataSource = program
            DType2.DataTextField = "ConcatField"
            DType2.DataValueField = "MQ001"
            DType2.DataBind()

            DType3.DataSource = program
            DType3.DataTextField = "ConcatField"
            DType3.DataValueField = "MQ001"
            DType3.DataBind()

            Dim SqlDelete As String = "SELECT OSNo from OutSourceH order by OSNo"
            Dim program2 As Data.DataTable = Conn_sql.Get_DataReader(SqlDelete, Conn_sql.MIS_ConnectionString)
            DDLDelete.DataSource = program2
            DDLDelete.DataTextField = "OSNo"
            DDLDelete.DataValueField = "OSNo"
            DDLDelete.DataBind()

            DDLRosno.DataSource = program2
            DDLRosno.DataTextField = "OSNo"
            DDLRosno.DataValueField = "OSNo"
            DDLRosno.DataBind()

        End If

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("dd/MMM/yyyy")
        CreateDate = resultdate
        txtdate.Text = resultdate

        GridViewAdd.Visible = False
        GridViewAdd.AllowPaging = True
        Griddelete.Visible = False
        Gridedit.Visible = False

    End Sub

    Protected Sub Busearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busearch.Click

        

        If txtfrom.Text = "" And txtto.Text = "" Then
            show_message.ShowMessage(Page, "Please Select Date", UpdatePanel1)
        ElseIf txtfrom.Text <> "" And txtto.Text <> "" Then
            SqlDataSource1.SelectCommand = "select TB001,TB002,TB003,TC018 from [JINPAO80].[dbo].[SFCTB] H JOIN [JINPAO80].[dbo].[SFCTC] L ON(H.TB001 = L.TC001) AND (H.TB002 = L.TC002) WHERE not EXISTS(select * from [DBMIS].[dbo].[OutSourceL] where H.TB001  = OutSourceL.Ttype  and H.TB002 = OutSourceL.Tno ) and TB003 between '" & txtfrom.Text & "' and '" & txtto.Text & "'and TB001='" & Dtype1.SelectedValue & "'and TB001='" & DType2.SelectedValue & "'and TB001='" & DType3.SelectedValue & "'"

        End If

        Dim program2 As Data.DataTable
        Dim docstr As String = Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        program2 = Conn_sql.Get_DataReader("SELECT isnull(max(OSNo),0) FROM OutSourceH where OSNo like '%" & docstr & "%' ", Conn_sql.MIS_ConnectionString)
        If program2.Rows(0).Item(0) = 0 Then
            txttid.Text = docstr & "0001"
        Else
            txttid.Text = CInt(program2.Rows(0).Item(0)) + 1
        End If

        GridViewAdd.Visible = True
        GridViewAdd.AllowPaging = False
        Me.GridViewAdd.DataBind()

    End Sub

    Private Sub ClearData()
        txtfrom.Text = ""
        txtto.Text = ""
        txttid.Text = ""
        Dtype1.SelectedIndex = 0
        DType2.SelectedIndex = 0
        DType3.SelectedIndex = 0
    End Sub

    Protected Sub Buaddall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buaddall.Click
        GridViewAdd.Visible = True
        For Each gvr As GridViewRow In GridViewAdd.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = True
        Next
    End Sub

    Protected Sub Buclearall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buclearall.Click
        GridViewAdd.Visible = True
        For Each gvr As GridViewRow In GridViewAdd.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = False
        Next
    End Sub

    Protected Sub Busave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busave.Click

        If txttid.Text = "" Then
            show_message.ShowMessage(Page, "Please Search Data", UpdatePanel1)
            Exit Sub
        End If

        Dim strSql As String
        strSql = "insert into OutSourceH (OSNo,Fdate,Tdate,Date,Type1,Type2,Type3,Remark,CreateBy,CreateDate) values('" & txttid.Text & "','" & txtfrom.Text & "','" & txtto.Text & "','" & txtdate.Text & "','" & Dtype1.SelectedValue & "','" & DType2.SelectedValue & "','" & DType3.SelectedValue & "','" & txtremark.Text & "','" & Session("Username") & "','" & txtdate.Text & "')"
        Conn_sql.Exec_Sql(strSql, Conn_sql.MIS_ConnectionString)

        Dim StrSqlLine As String
        For M_count As Integer = 0 To GridViewAdd.Rows.Count - 1
            Dim cb As CheckBox = GridViewAdd.Rows(M_count).FindControl("Ck")
            If cb IsNot Nothing AndAlso cb.Checked = False Then
            ElseIf cb.Checked = True Then

                Dim Type As String = GridViewAdd.Rows(M_count).Cells(1).Text
                Dim No As String = GridViewAdd.Rows(M_count).Cells(2).Text
                Dim TDate As String = GridViewAdd.Rows(M_count).Cells(3).Text
                Dim Amount As Decimal = GridViewAdd.Rows(M_count).Cells(4).Text
                ',SumAmount
                StrSqlLine = "insert into OutSourceL(Ttype,Tno,Date,Amount,OSNo) values('" & Type & "','" & No & "','" & TDate & "','" & Amount & "','" & txttid.Text & "')"

                Conn_sql.Exec_Sql(StrSqlLine, Conn_sql.MIS_ConnectionString)
            End If

        Next
        'Sum Amount
        Dim StrSum As String = "select SUM(Amount) from OutSourceL where OSNo ='" & txttid.Text & "'"
        Dim SumAmount As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        'Save SumAmount
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update OutSourceL set SumAmount ='" & SumAmount & "'where OSNo ='" & txttid.Text & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('TransferOrder1.aspx?OSNo=" + txttid.Text + "');", True)
        ClearData()

        Griddelete.DataBind()
        GridView2.DataBind()
        Gridreport.DataBind()

    End Sub

    Protected Sub Budsearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Budsearch.Click

        Griddelete.Visible = True
        SqlDataSource2.SelectCommand = "SELECT * FROM OutSourceL where OSNo='" & DDLDelete.SelectedValue & "'"
        Me.Griddelete.DataBind()

    End Sub

    Protected Sub Budall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Budall.Click
        Griddelete.Visible = True
        For Each gvr As GridViewRow In Griddelete.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = True
        Next
    End Sub

    Protected Sub Bucall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bucall.Click
        Griddelete.Visible = True
        For Each gvr As GridViewRow In Griddelete.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = False
        Next
    End Sub

    Protected Sub Buddelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buddelete.Click

        For M_count As Integer = 0 To Griddelete.Rows.Count - 1

            Dim cb As CheckBox = Griddelete.Rows(M_count).FindControl("Ck")
            If cb IsNot Nothing AndAlso cb.Checked = False Then
            ElseIf cb.Checked = True Then

                Dim StrDel As String = "Delete from OutSourceL where id ='" & Griddelete.Rows(M_count).Cells(1).Text & "'"
                Conn_sql.Exec_Sql(StrDel, Conn_sql.MIS_ConnectionString)

            End If
        Next

        'Sum Amount
        Dim StrSum As String = "select SUM(Amount) from OutSourceL where OSNo ='" & DDLDelete.SelectedValue & "'"
        Dim SumAmount As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)
        'ThaiBaht(SumBal)
        'SpellNumber(SumBal)
        'Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update OutSourceL set SumAmount ='" & SumAmount & "' where OSNo ='" & DDLDelete.SelectedValue & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        DDLDelete.SelectedIndex = 0
     
    End Sub

    Protected Sub Buesearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buesearch.Click

        SqlDataSource4.SelectCommand = "SELECT OSNo, Date, Type1, Type2, Type3 FROM OutSourceH where OSNo='" & DDLTID.SelectedValue & "'"
        GridView2.DataBind()

    End Sub

    Private Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand

        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            lbltid.Text = GridView2.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
            lbldate.Text = GridView2.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            lbltype1.Text = GridView2.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            lbltype2.Text = GridView2.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
            lbltype3.Text = GridView2.Rows(i).Cells(4).Text.Replace("&nbsp;", "")

        End If

    End Sub

    Protected Sub Bueselectall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bueselectall.Click
        Gridedit.Visible = True
        For Each gvr As GridViewRow In Gridedit.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = True
        Next

    End Sub

    Protected Sub Bueclearall_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bueclearall.Click
        Gridedit.Visible = True
        For Each gvr As GridViewRow In Gridedit.Rows
            Dim cb As CheckBox = CType(gvr.FindControl("Ck"), CheckBox)
            cb.Checked = False
        Next

    End Sub

    Protected Sub Buesave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buesave.Click

        If lbltid.Text = "" Then
            show_message.ShowMessage(Page, "Please Select OSNo.", UpdatePanel1)
            Exit Sub
        End If
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("dd/MMM/yyyy")

        Dim SqlUpHead As String
        SqlUpHead = "update OutSourceH set EditBy='" & Session("Username") & "',EditDate='" & resultdate & "'where OSNo='" & lbltid.Text & "'"
        Conn_sql.Exec_Sql(SqlUpHead, Conn_sql.MIS_ConnectionString)

        Dim StrSqlLine As String
        Dim CR As Double
        For M_count As Integer = 0 To Gridedit.Rows.Count - 1

            Dim cb As CheckBox = Gridedit.Rows(M_count).FindControl("Ck")
            If cb IsNot Nothing AndAlso cb.Checked = False Then
            ElseIf cb.Checked = True Then

                Dim Type As String = Gridedit.Rows(M_count).Cells(1).Text
                Dim No As String = Gridedit.Rows(M_count).Cells(2).Text
                Dim TDate As String = Gridedit.Rows(M_count).Cells(3).Text
                Dim Amount As Decimal = Gridedit.Rows(M_count).Cells(4).Text

                StrSqlLine = "insert into OutSourceL (Ttype,Tno,DateL,Amount,OSNo) values('" & Type & "','" & No & "','" & TDate & "','" & Amount & "','" & lbltid.Text & "')"
                Conn_sql.Exec_Sql(StrSqlLine, Conn_sql.MIS_ConnectionString)

            End If
        Next
        'Sum Amount
        Dim StrSum As String = "select SUM(Amount) from OutSourceL where OSNo ='" & lbltid.Text & "'"
        Dim SumAmount As Double = Conn_sql.Get_value(StrSum, Conn_sql.MIS_ConnectionString)

        'Save SumAmount
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update OutSourceL set SumAmount ='" & SumAmount & "'where OSNo ='" & lbltid.Text & "'"
        Conn_sql.Exec_Sql(UpdateSumAmount, Conn_sql.MIS_ConnectionString)

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('TransferOrder1.aspx?OSNo=" + lbltid.Text + "');", True)
        lbltid.Text = ""
        lbldate.Text = ""
        lbltype1.Text = ""
        lbltype2.Text = ""
        lbltype3.Text = ""
        txtefrom.Text = ""
        txteto.Text = ""

        Gridedit.Visible = False
        Griddelete.DataBind()
        GridView2.DataBind()
        Gridreport.DataBind()

    End Sub

    Private Sub Gridreport_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Gridreport.RowCommand

        If e.CommandName = "OnPrint" Then
            Dim i As Integer = e.CommandArgument

            txtOSNo.Text = Gridreport.Rows(i).Cells(0).Text.Replace(" ", "")

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('TransferOrder1.aspx?OSNo=" + txtOSNo.Text + "');", True)
        End If

    End Sub

    Protected Sub Bursearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bursearch.Click

        DataSourceReport.SelectCommand = "SELECT * FROM OutSourceH where OSNo='" & DDLRosno.SelectedValue & "'"
        Me.Gridreport.DataBind()

    End Sub

    Protected Sub BuESearch2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuESearch2.Click

        If txtefrom.Text <> "" And txteto.Text <> "" Then

            Dim aa As String = CStr(txtefrom.Text)
            Dim bb As String = CStr(txteto.Text)

            SqlDataSource5.SelectCommand = "select TB001,TB002,TB003,TC018 from [JINPAO80].[dbo].[SFCTB] H JOIN [JINPAO80].[dbo].[SFCTC] L ON(H.TB001 = L.TC001) AND (H.TB002 = L.TC002) WHERE not EXISTS(select * from [DBMIS].[dbo].[OutSourceL] where H.TB001  = OutSourceL.Ttype  and H.TB002 = OutSourceL.Tno ) and TB003 between '" & aa & "' and '" & bb & "'and TB001='" & lbltype1.Text & "'and TB001='" & lbltype2.Text & "'and TB001='" & lbltype3.Text & "'"

        ElseIf txtefrom.Text = "" And txteto.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Date", UpdatePanel1)
        End If
        Me.Gridedit.DataBind()
        Gridedit.Visible = True

    End Sub

    
End Class
