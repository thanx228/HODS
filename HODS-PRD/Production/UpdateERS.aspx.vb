Public Class UpdateERS
    Inherits System.Web.UI.Page
    Dim CreateTable As New CreateTable
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate
    Dim ControlForm As New ControlDataForm
    Dim tableERS As String = CreateTable.tableERS

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            CreateTable.CreateERSCode()
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim SQL As String,
            WHR As String
        WHR = Conn_SQL.Where("MB001", tbItem)
        WHR &= Conn_SQL.Where("MB003", tbSpec)

        If WHR = "" Then
            show_message.ShowMessage(Page, "กรุณากรอกข้อมูลสำหรับค้นหาข้อมูล!!!", UpdatePanel1)
            tbItem.Focus()
            Exit Sub
        End If

        SQL = "select MB001,MB003,UDF05,UDF06 from INVMB where MB025 = 'M' and MB109 = 'Y' " & WHR & " order by MB001"
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)

        ucCntRow.RowCount = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)

    End Sub

    Private Sub gvShow_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShow.RowCommand
        If e.CommandName = "editERS" Then
            Dim i As Integer = e.CommandArgument
            With gvShow.Rows(i)
                lbItemShow.Text = Conn_SQL.checkStringValue(.Cells(1).Text)
                lbSpecShow.Text = Conn_SQL.checkStringValue(.Cells(2).Text)
                tbRevShow.Text = Conn_SQL.checkStringValue(.Cells(3).Text)
                tbErsCode.Text = Conn_SQL.checkStringValue(.Cells(4).Text)
                lbLineHiddle.Text = i
            End With
            Me.ModalPopupExtender1.Show()
        End If
    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Dim item As String = Conn_SQL.checkStringValue(lbItemShow.Text)
        Dim itemRev As String = Conn_SQL.checkStringValue(tbRevShow.Text)
        Dim ersCode As Decimal = Conn_SQL.checkNumberic(tbErsCode.Text)

        If itemRev = "" Then
            show_message.ShowMessage(Page, "Rev. is empty !!!", UpdatePanel1)
            tbRevShow.Focus()
            Me.ModalPopupExtender1.Show()
            Exit Sub
        End If
        If ersCode = 0 Then
            show_message.ShowMessage(Page, "ERS Code is empty or number only !!!", UpdatePanel1)
            tbErsCode.Focus()
            Me.ModalPopupExtender1.Show()
            Exit Sub
        End If
        Dim fld As Hashtable,
            whr As Hashtable
        'insert log ers code
        fld = New Hashtable
        whr = New Hashtable
        fld.Add("Item", item)
        fld.Add("ItemRev", itemRev)
        fld.Add("CodeERS", Conn_SQL.checkStringValue(tbErsCode.Text))
        fld.Add("CreateBy", Session("UserName"))
        fld.Add("CreateDate", DateTime.Today.ToString("yyyyMMddhhmmss"))
        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableERS, fld, whr, "I"), Conn_SQL.MIS_ConnectionString)

        'update item ERP(UDF05=rev,UDF52=ers code)
        fld = New Hashtable
        whr = New Hashtable

        whr.Add("MB001", item)
        fld.Add("UDF05", itemRev)
        fld.Add("UDF06", ersCode)
        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL("INVMB", fld, whr, "U"), Conn_SQL.ERP_ConnectionString)

        'update value to grid
        Dim lineGridview As Integer = Conn_SQL.checkNumberic(lbLineHiddle.Text)
        With gvShow.Rows(lineGridview)
            .Cells(3).Text = itemRev
            .Cells(4).Text = ersCode
        End With

    End Sub
End Class