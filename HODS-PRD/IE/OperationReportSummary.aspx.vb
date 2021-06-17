Public Class OperationReportSummary
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            ControlForm.showCheckboxList(cblWC, SQL, "MD002", "MD001", 6, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001 as MQ001 from CMSMQ where MQ001 in ('D401','D402','D403','D404','D407','D408') order by MQ001"
            ControlForm.showCheckboxList(cblREType, SQL, "MQ001", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False

        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim SQL As String = "",
          WHR As String = "",
          DateFrom As String = txtDateFrom.Text.Trim,
          DateTo As String = txtDateTo.Text.Trim

        WHR = WHR & Conn_SQL.Where("SFCTD.TD004", cblWC)
        WHR = WHR & Conn_SQL.Where("SFCTE.TE001", cblREType)

        If DateFrom <> "" Then
            WHR = WHR & Conn_SQL.Where("SFCTD.TD008", configDate.dateFormat2(DateFrom), configDate.dateFormat2(DateTo))
        End If

        If DateTo <> "" Then
            WHR = WHR & Conn_SQL.Where("SFCTD.TD008", configDate.dateFormat2(DateFrom), configDate.dateFormat2(DateTo))
        End If

        Dim tWHR As String = "",
            ttWHR As String = "",
            lWHR As String = "",
            fWHR As String = "",
        cntSQL As Integer = 0

        Dim ordType As String = "",
            cnt As Integer = 0,
        typeAll As String = "",
        Type As String = ""

        For Each boxItem As ListItem In CheckSum.Items
            Dim boxVal As String = CStr(boxItem.Value.Trim)
            typeAll = typeAll & "'" & boxVal & "',"
            If boxItem.Selected Then
                Type = Type & "'" & boxVal & "',"
                cntSQL = cntSQL + 1
                Select Case boxItem.Value.Trim
                    Case "0"
                        tWHR = tWHR & " CMSMD.MD002  as 'WC Name', "
                        lWHR = lWHR & " CMSMD.MD002, "
                    Case "1"
                        tWHR = tWHR & " SFCTD.TD001  as 'Report Type', "
                        lWHR = lWHR & " SFCTD.TD001, "
                        fWHR = fWHR & " SFCTD.TD001, "
                    Case "2"
                        tWHR = tWHR & " (SUBSTRING(SFCTD.TD008,7,2)+'-'+SUBSTRING(SFCTD.TD008,5,2)+'-'+SUBSTRING(SFCTD.TD008,1,4)) as 'Date', "
                        lWHR = lWHR & " SFCTD.TD008, "
                        'fWHR = fWHR & " SFCTD.TD008, "
                    Case "3"
                        tWHR = tWHR & " SFCTE.TE007  as 'MO No.', "
                        lWHR = lWHR & " SFCTE.TE007, "
                        fWHR = fWHR & " SFCTE.TE007,"
                    Case "4"
                        tWHR = tWHR & " SFCTE.TE017  as 'Item', "
                        lWHR = lWHR & " SFCTE.TE017, "
                        fWHR = fWHR & " SFCTE.TE017,"
                    Case "5"
                        ttWHR = ttWHR & " SFCTE.UDF01  as 'OP ID', "
                        lWHR = lWHR & " SFCTE.UDF01, "
                    Case "6"
                        ttWHR = ttWHR & " SFCTE.TE005  as 'Code No.', "
                        lWHR = lWHR & " SFCTE.TE005, "
                End Select

            End If
        Next
        If cntSQL > 0 Then
            Type = Type.Substring(0, Type.Length - 1)
            typeAll = typeAll.Substring(0, typeAll.Length - 1)
        End If


        For Each boxItem As ListItem In cblWC.Items
            If boxItem.Selected = True Then
                ordType = ordType & "'" & CStr(boxItem.Value) & "',"
                cnt = cnt + 1
                End If
        Next
        If cnt > 0 Then
            ordType = ordType.Substring(0, ordType.Length - 1)
        End If


        For Each boxItem As ListItem In cblREType.Items
            If boxItem.Selected = True Then
                ordType = ordType & "'" & CStr(boxItem.Value) & "',"
                cnt = cnt + 1
                End If
        Next
        If cnt > 0 Then
            ordType = ordType.Substring(0, ordType.Length - 1)
        End If


        SQL = " select " & tWHR & _
                  " SFCTE.TE019 as 'Spec', " & _
                  " convert(money,SUM(SFCTE.UDF51),0) as 'MO Qty', " & _
                  " convert(money,SUM(SFCTE.TE011),0) as 'Def. OP Qty', " & _
                  " convert(money,SUM(SFCTE.UDF52),0) as 'Scrap Qty'," & ttWHR & _
                  " SFCTE.TE012 as 'Working Hr.'" & _
                  " from SFCTE" & _
                  " left join SFCTD on SFCTD.TD001 = SFCTE.TE001 and SFCTD.TD002 = SFCTE.TE002" & _
                  " left join SFCTA on SFCTA.TA004 = SFCTD.TD001 and SFCTA.TA005 = SFCTD.TD002 and SFCTA.TA006 = SFCTD.TD004" & _
                  " left join CMSMD on CMSMD.MD001 = SFCTD.TD004" & _
                  " where SFCTD.TD005 = 'Y'" & WHR & _
                  " group by " & lWHR & " SFCTE.TE019,SFCTE.TE012" & _
                  " order by " & fWHR & " SFCTE.TE019"

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("OperationReportSummery" & Session("UserName"), gvShow)
    End Sub
End Class
