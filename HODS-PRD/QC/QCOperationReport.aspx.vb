Imports System.Globalization

Public Class QCOperationReport
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("UserName") = "" Then
            '    Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            'End If
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShow.Click
        Dim WHR As String = "",
            SQL As String = "",
            Item As String = txtItem.Text.Trim,
            Spec As String = txtSpec.Text.Trim,
            WorkCen As String = txtWork.Text.Trim
        WHR = configDate.DateWhere("TD003", configDate.dateFormat2(txtDateFrom.Text), configDate.dateFormat2(txtDateTo.Text))
        If Item <> "" Then
            WHR = WHR & " and TE017 like '" & Item & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and TE019 like '" & Spec & "%' "
        End If
        If WorkCen <> "" Then
            WHR = WHR & " and TD004 like '" & WorkCen & "%' "
        End If
        Select Case ddlReport.Text
            Case "0"
                WHR = WHR & " and TD001 = 'D401' "
            Case "1"
                WHR = WHR & " and TD001 = 'D402' "
            Case "2"
                WHR = WHR & " and TD001 = 'D403' "
            Case "3"
                WHR = WHR & " and TD001 = 'D404' "
            Case "4"
                WHR = WHR & " and TD001 = 'D405' "
            Case "5"
                WHR = WHR & " and TD001 = 'D406' "
            Case "6"
                WHR = WHR & " and TD001 = 'D407' "
            Case "7"
                WHR = WHR & " and TD001 = 'D408' "
        End Select
        Dim DateFrom As String = txtDateFrom.Text.Trim
        Dim DateTo As String = txtDateTo.Text.Trim
        If txtDateFrom.Text <> "" Then
            WHR = WHR & " and SFCTD.TD003< '" & Date.Today.ToString("yyyyMMdd") & "' "
        Else
            txtDateFrom.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("dd/MM/yyyy", New CultureInfo("en-US"))
            WHR = WHR & Conn_SQL.Where("SFCTD.TD003", configDate.dateFormat2(txtDateFrom.Text.Trim), configDate.dateFormat2(txtDateTo.Text.Trim))
        End If
        If txtDateTo.Text <> "" Then
            WHR = WHR & " and SFCTD.TD003< '" & Date.Today.ToString("yyyyMMdd") & "' "
        Else
            txtDateTo.Text = Date.Now.ToString("dd/MM/yyyy", New CultureInfo("en-US"))
            WHR = WHR & Conn_SQL.Where("SFCTD.TD003", configDate.dateFormat2(txtDateFrom.Text.Trim), configDate.dateFormat2(txtDateTo.Text.Trim))
        End If
        SQL = "select SFCTD.TD001 as 'TD001', CMSMQ.MQ002 as 'MQ002', SFCTE.TE006 as 'TE006'," & _
              " SFCTE.TE007 as 'TE007', SFCTE.TE008 as 'TE008', SFCTE.TE009 as 'TE009'," & _
              " CMSMW.MW002 as 'MW002', SFCTE.TE017 as 'TE017', SFCTE.TE019 as 'TE019', SFCTE.UDF51 as 'UDF51'," & _
              " SFCTE.TE011 as 'TE011', SFCTE.UDF52 as 'UDF52', SFCTD.TD004 as 'TD004', SFCTE.TE015 as 'TE015'" & _
              " from SFCTD " & _
              " left JOIN CMSMQ on CMSMQ.MQ001 = SFCTD.TD001" & _
              " left JOIN SFCTE on SFCTE.TE001 = SFCTD.TD001 and SFCTE.TE002 = SFCTD.TD002 " & _
              " left join CMSMW on CMSMW.MW001 = SFCTE.TE009" & _
              " where SFCTD.TD001 in ('D401','D402','D403','D404','D405','D406','D407','D408')" & WHR & _
              " order by TE006,TE007,TE017"

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub


    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("QCOperationReport" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class
