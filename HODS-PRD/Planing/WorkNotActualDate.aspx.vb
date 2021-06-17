Public Class WorkNotActualDate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim date1 As String = txtFrom.Text
        Dim date2 As String = txtTo.Text
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = ""
        Dim endDate As String = ""

        Dim xd As String = ""
        Dim xm As String = ""
        'Begin date
        If date1 <> "" Then
            Dim temp1() As String = date1.Split("/")
            xd = temp1(1)
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = temp1(0)
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            strDate = temp1(2) & xm & xd
        Else
            xd = dateToday.Day
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = dateToday.Month
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            strDate = dateToday.Year & xm & xd
        End If
        'End date
        If date2 <> "" Then
            Dim temp1() As String = date2.Split("/")
            xd = temp1(1)
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = temp1(0)
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            endDate = temp1(2) & xm & xd
        Else
            xd = dateToday.Day
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = dateToday.Month
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            endDate = dateToday.Year & xm & xd
        End If

        Dim paraName As String = "DateFrom:" & strDate & ",DateTo:" & endDate
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=WorkNotActualDate.rpt&paraName=" & paraName & "');", True)

    End Sub
End Class