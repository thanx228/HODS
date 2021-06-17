Public Class MaterailsShortageMoList
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim whr As String = "",
                SQL As String = ""
            Dim item As String = Request.QueryString("item").ToString.Trim
            Dim fDate As String = Request.QueryString("fDate").ToString.Trim
            Dim tDate As String = Request.QueryString("tDate").ToString.Trim
            Dim conDate As Boolean = False
            Dim addDate As Integer = Request.QueryString("addDate").ToString.Trim
            Dim tempDate As Date
            If tDate = "" Then
                tempDate = DateTime.ParseExact(fDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                fDate = tempDate.AddDays(addDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                conDate = True
            ElseIf fDate = "" Then
                tempDate = DateTime.ParseExact(tDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                tDate = tempDate.AddDays(addDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            End If

            'Materials Request Issue
            SQL = " select TA001+'-'+TA002 as MO,(SUBSTRING(TA040,7,2)+'-'+SUBSTRING(TA040,5,2)+'-'+SUBSTRING(TA040,1,4)) as 'MO Date', " & _
                  " (SUBSTRING(TB015,7,2)+'-'+SUBSTRING(TB015,5,2)+'-'+SUBSTRING(TB015,1,4)) as 'Plan Issue Date', " & _
                  " cast(isnull(TB004,0) as decimal(10,3)) as 'Mat Req Qty',cast(isnull(TB005,0) as decimal(10,3)) as 'Issue Qty', " & _
                  " cast(isnull(TB004,0)-isnull(TB005,0) as decimal(10,3)) as 'issue Bal'," & _
                  " TA006 as 'JP PART',TA035 as 'JP SPEC' " & _
                  " from MOCTB  left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
                  " where TB003='" & item & "' and TB004-TB005>0 and TA011 not in('y','Y')  and TA013='Y' " & _
                  configDate.DateWhere("TB015", fDate, tDate, conDate) & _
                  " order by TB015,TA001,TA002 "
            ControlForm.ShowGridView(gvIssue, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountIssue.Text = ControlForm.rowGridview(gvIssue)

            
        End If
    End Sub
End Class