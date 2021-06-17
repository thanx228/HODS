Public Class PaymentBalSum
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            'Response.Redirect("../Login.aspx")
        End If
    End Sub

    Function getWhr() As String
        Dim BeginDate As String = tbDateFrom.Text
        If BeginDate <> "" Then
            BeginDate = configDate.dateFormat2(BeginDate)
        Else
            BeginDate = Date.Today.ToString("yyyyMM", New System.Globalization.CultureInfo("en-US"))
            BeginDate = BeginDate & "01"
        End If
        'BeginDate = configDate.dateShow2(BeginDate, "-")
        Dim endDate As String = tbDateTo.Text
        If endDate <> "" Then
            endDate = configDate.dateFormat2(endDate)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        End If
        'Dim whr As String = Conn_SQL.Where("TA034", BeginDate, endDate)
        Return Conn_SQL.Where("TA034", BeginDate, endDate)
    End Function


    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        
        Dim SQL As String = " select rtrim(TA004)+':'+MA002 as TA004,COUNT(*) as cnt," & _
                            " SUM(TA037+TA038-TA085) as balance from ACPTA " & _
                            " left join PURMA on MA001=TA004 " & _
                            " where TA087 <> '3' " & getWhr() & _
                            " group by TA004,MA002  order by TA004,MA002"
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click

        Dim BeginDate As String = tbDateFrom.Text
        If BeginDate = "" Then
            '    BeginDate = configDate.dateFormat2(BeginDate)
            'Else
            BeginDate = Date.Today.ToString("MM/yyyy", New System.Globalization.CultureInfo("en-US"))
            BeginDate = "01/" & BeginDate
        End If
        'BeginDate = configDate.dateShow2(BeginDate, "-")
        Dim endDate As String = tbDateTo.Text
        If endDate <> "" Then
            '    endDate = configDate.dateFormat2(endDate)
            'Else
            endDate = Date.Today.ToString("dd/MM/yyyy", New System.Globalization.CultureInfo("en-US"))
        End If
        Dim chrConn As String = Chr(8)
        Dim paraName As String = "whr:" & getWhr() & chrConn & "DateFrom:" & BeginDate & chrConn & "DateTo:" & endDate
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=SupBalSum.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & chrConn & "&encode=1');", True)
    End Sub
End Class