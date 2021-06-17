Public Class PrintCheq
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            CreateTable.CreateReportCheq()
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = ""
        Dim WHR As String = ""
        If tbCheq.Text <> "" Then
            WHR = WHR & " and TK014 like '" & tbCheq.Text.Trim & "%' "
        Else
            WHR = WHR & configDate.DateWhere("TK015", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text), True)
        End If
        If tbSup.Text <> "" Then
            WHR = WHR & " and TK004 = '" & tbSup.Text.Trim & "' "
        End If
        SQL = " select TK001 as 'Pay Type',TK002 as 'Pay No.',TK004 as 'Supplier',MA003 as 'Supplier Name',TK014 as 'Cheq No', " & _
            " (SUBSTRING(TK015,7,2)+'-'+SUBSTRING(TK015,5,2)+'-'+SUBSTRING(TK015,1,4)) as 'Due Date', " & _
            "  CONVERT(VARCHAR,cast(TK030 as money),1) as 'Cheq Amout' " & _
            " from ACPTK TK " & _
            " left join PURMA MA on MA.MA001=TK.TK004 " & _
            " where TK011!='' and TK012!='' and TK014!='' " & WHR & _
            " order by TK004,TK014 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=Cheque.rpt&paraName=" & paraName & "');", True)
            Dim fldDateCheque As String = "TK015"
            Dim dateChuqe As String = configDate.dateFormat2(tbDateCheque.Text.Trim)
            If dateChuqe <> "" Then
                fldDateCheque = dateChuqe & ":1"
            End If
            Dim paraName As String = "PayNo:" & e.Row.Cells(2).Text.ToString.Trim & ",PayType:" & e.Row.Cells(1).Text.ToString.Trim & ",fldDateCheque:" & fldDateCheque
            e.Row.Cells(0).Attributes("onclick") = "javascript:NewWindow('../ShowCrystalReport.aspx?dbName=ERP&ReportName=Cheque.rpt&paraName=" & Server.UrlDecode(paraName) & "&encode=1','Cheque','900','500')"
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

    Protected Sub BuPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuPrint.Click

        Dim BillTotal As String = "delete from ReportCheq"
        Conn_SQL.Exec_Sql(BillTotal, Conn_SQL.MIS_ConnectionString)

        Dim SQL As String = ""
        Dim WHR As String = ""
        If tbCheq.Text <> "" Then
            WHR = WHR & " and TK014 like '" & tbCheq.Text.Trim & "%' "
        Else
            WHR = WHR & configDate.DateWhere("TK015", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text), True)
        End If
        If tbSup.Text <> "" Then
            WHR = WHR & " and TK004 = '" & tbSup.Text.Trim & "' "
        End If
        SQL = " select TK001 as 'PayType',TK002 as 'PayNo',TK004 as 'SuppNo',MA003 as 'SupName',TK014 as 'CheqNo', " & _
            " (SUBSTRING(TK015,7,2)+'-'+SUBSTRING(TK015,5,2)+'-'+SUBSTRING(TK015,1,4)) as 'DueDate', " & _
            " cast(TK030 as decimal(15,2))  as 'CheqAmout'" & _
            " from ACPTK TK " & _
            " left join PURMA MA on MA.MA001=TK.TK004 " & _
            " where TK011!='' and TK012!='' and TK014!='' " & WHR & _
            " order by TK004,TK014 "

        Dim aa As New Data.DataTable
        aa = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        If aa.Rows.Count > 0 Then
            For i As Integer = 0 To aa.Rows.Count - 1
                Dim PayType As String = aa.Rows(i).Item("PayType").ToString.Replace(" ", "")
                Dim PayNo As String = aa.Rows(i).Item("PayNo").ToString.Replace(" ", "")
                Dim SuppNo As String = aa.Rows(i).Item("SuppNo").ToString.Replace(" ", "")
                Dim SupName As String = aa.Rows(i).Item("SupName").ToString.Replace(" ", "")
                Dim CheqNo As String = aa.Rows(i).Item("CheqNo").ToString.Replace(" ", "")
                Dim DueDate As String = aa.Rows(i).Item("DueDate").ToString.Replace(" ", "")
                Dim CheqAmout As String = aa.Rows(i).Item("CheqAmout").ToString.Replace(" ", "").ToString.Replace("NULL", "").Replace(" ", "")

                Dim InSQL As String = "Insert into ReportCheq (PayType,PayNo,SuppNo,SupName,CheqNo,DueDate,CheqAmout)"
                InSQL = InSQL & " Values('" & PayType & "','" & PayNo & "','" & SuppNo & "',"
                InSQL = InSQL & "'" & SupName & "','" & CheqNo & "','" & DueDate & "',"
                InSQL = InSQL & "'" & CheqAmout & "')"
                Conn_SQL.Exec_Sql(InSQL, Conn_SQL.MIS_ConnectionString)
            Next
        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('PrintCheq1.aspx');", True)

    End Sub
End Class