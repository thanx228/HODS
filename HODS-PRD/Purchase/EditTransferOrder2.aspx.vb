Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class EditTransferOrder2
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL
    Dim CreateDate As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("dd/MM/yyyy")
        CreateDate = resultdate
        GridView2.Visible = False

    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        If txtno.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Transfer Type/No.  ", UpdatePanel1)
            txtno.Focus()
            Exit Sub
        ElseIf txtno.Text <> "" Then
            '          (SFCTB.TB007 = '2' or SFCTB.TB004 = '2')
            SqlDataSource2.SelectCommand = "select TC001,TC002,TC003,TC004,TC005,TC047,TC048,TC049,TC014,TC017,TC018 from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 where (SFCTB.TB007 = '2' or SFCTB.TB004 = '2') and TC001='" & DDLType.SelectedValue & "' and TC002='" & txtno.Text & "' order by TC003"
        End If

        ClearData()
        GridView2.Visible = True
        Me.GridView2.DataBind()

    End Sub

    Private Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand

        If e.CommandName = "OnChange" Then
            Dim i As Integer = e.CommandArgument

            Dim dType As String = GridView2.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
            Dim dNo As String = GridView2.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            Ltype.Text = dType
            Lno.Text = dNo
            Lseq.Text = GridView2.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Litem.Text = GridView2.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            Ldesc.Text = GridView2.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
            Lspec.Text = GridView2.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
            Lqty.Text = GridView2.Rows(i).Cells(8).Text.Replace("&nbsp;", "")

            Dim sql As String = "select TH007,TH008,TH015,TH030 from MOCTH where TH001='" & dType & "' and TH002='" & dNo & "' "
            'Dim sql As String = "select TB021,TB020,TB019,TB022 from SFCTB where TB001='" & dType & "' and TB002='" & dNo & "' "

            Dim aa As New Data.DataTable
            aa = Conn_sql.Get_DataReader(sql, Conn_sql.ERP_ConnectionString)
            If aa.Rows.Count > 0 Then
                Lcurrency.Text = aa.Rows(0).Item("TH007")
                Lrate.Text = aa.Rows(0).Item("TH008")
                Ltaxtype.Text = aa.Rows(0).Item("TH015")
                Ltaxrate.Text = aa.Rows(0).Item("TH030") * 100
            End If

            GridView2.Visible = True
        End If

    End Sub

    Private Sub BuSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuSave.Click

        Dim StrSql As String
        Dim SqlSubcontact As String
        Dim qty As Double = Lqty.Text
        Dim Price As Double = txtprice.Text
        Dim Amount As Double = qty * Price
        'Dim Vat As Double = (Amount * 7) / 100


        StrSql = "update SFCTC set TC017='" & txtprice.Text & "' , TC018='" & Amount & "' where TC001='" & Ltype.Text & "' and TC002='" & Lno.Text & "' and TC003='" & Lseq.Text & "'"
        Conn_sql.Exec_Sql(StrSql, Conn_sql.ERP_ConnectionString)
        'TI024                Price()
        'TI025,TI044,TI046    Amount
        'TI045,TI047          Tax

        'get rate
        Dim rateVat As Decimal = 0,
            rate As Decimal = 0
        If Lrate.Text.Trim <> "" Then
            rate = CDec(Lrate.Text.Trim)
        End If
        If Ltaxtype.Text.Trim <> "3" And Ltaxtype.Text.Trim <> "" Then
            rateVat = CDec(Ltaxrate.Text.Trim / 100)
        End If

        SqlSubcontact = "update MOCTI set TI024='" & txtprice.Text.Replace(" ", "") & "', TI025='" & Amount & "', TI044='" & Amount & "', TI046='" & Amount * rate & "', TI045='" & Amount * rateVat & "', TI047='" & Amount * rateVat * rate & "' where TI001='" & Ltype.Text & "' and TI002='" & Lno.Text & "' and TI003='" & Lseq.Text & "'"
        Conn_sql.Exec_Sql(SqlSubcontact, Conn_sql.ERP_ConnectionString)

        Dim StrTemp As String
        StrTemp = "insert into TempTransferOrder(Type,No,Seq,Item,Dec,Spec,Qty,Price,Amount,CreateBy,CreateDate) Values('" & Ltype.Text & "','" & Lno.Text & "','" & Lseq.Text & "','" & Litem.Text & "','" & Ldesc.Text & "','" & Lspec.Text & "','" & Lqty.Text & "','" & txtprice.Text & "','" & Amount & "','" & Session("Username") & "','" & CreateDate & "')"
        Conn_sql.Exec_Sql(StrTemp, Conn_sql.MIS_ConnectionString)
        ClearData()
        GridView2.Visible = False

    End Sub

    Private Sub ClearData()
        Ltype.Text = ""
        Lno.Text = ""
        Lseq.Text = ""
        Litem.Text = ""
        Ldesc.Text = ""
        Lspec.Text = ""
        Lqty.Text = ""
        txtprice.Text = ""
        Ltaxtype.Text = ""
        Ltaxrate.Text = ""
        Lcurrency.Text = ""
        Lrate.Text = ""
        GridView2.DataBind()
    End Sub


End Class