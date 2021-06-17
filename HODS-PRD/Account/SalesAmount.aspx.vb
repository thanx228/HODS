Public Class SalesAmount
    Inherits System.Web.UI.Page

    Dim show_message As New show_message
    Dim Conn_sql As New ConnSQL
    Dim ControlForm As New ControlDataForm
    'Dim ExportUtil As New ExportUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        IBuExcel.Visible = False

    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        ShowSearch()
        IBuExcel.Visible = True

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Protected Sub IBuExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IBuExcel.Click

        ExportGridView("InvoiceAmount", GridView2)

    End Sub

    Public Shared Sub ExportGridView(ByVal FileName As String, ByVal gv As GridView)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>")
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + _
        HttpContext.Current.Server.UrlEncode(FileName) & ".xls")
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        Dim StringWriter As New System.IO.StringWriter
        Dim HtmlTextWriter As New HtmlTextWriter(StringWriter)
        gv.AllowSorting = False
        gv.AllowPaging = False
        gv.EnableViewState = False
        gv.AutoGenerateColumns = False
        gv.RenderControl(HtmlTextWriter)
        HttpContext.Current.Response.Write(StringWriter.ToString())
        HttpContext.Current.Response.End()
    End Sub

    Private Sub ShowSearch()

        Dim whr As String
        Dim SqlStr As String
      
        If DDLGroup.SelectedValue <> 100 Then
            whr = whr & " and G.id = '" & DDLGroup.SelectedValue & "'"
        End If
        If DDLArea.SelectedIndex <> 0 Then
            whr = whr & " and MA018='" & DDLArea.SelectedValue & "'"
        End If
        If DDLCredit.SelectedIndex <> 0 Then
            whr = whr & " and MA031='" & DDLCredit.SelectedValue & "'"
        End If

        SqlStr = "SELECT distinct TA001 + '-' + TA002 as TypeNo,TA038 as OrderDate,TA004 as CID,MA002 as CName,MA031 as Payment,R.name as Area" & _
    " ,SUM(TB019)as Amount,SUM(TB020) as Tax,SUM(TB019) + SUM(TB020) as Balance " & _
    " from ACRTA H left join COPMA A on(H.TA004 = A.MA001) left join [DBMIS].[dbo].[Area] R on (A.MA018 = R.id) " & _
    " left join [DBMIS].[dbo].[GroupProduct] G on(A.MA076 = G.id) left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002)" & _
    "  where TA038 between '" & txtfrom.Text & "' and '" & txtto.Text & "'" & whr & _
    " group by TA001,TA002,TA038,TA004,MA002,MA031,R.name"

        ControlForm.ShowGridView(GridView2, SqlStr, Conn_sql.ERP_ConnectionString)
        lbCount.Text = "Number of items. " & GridView2.Rows.Count & " item"

    End Sub

End Class