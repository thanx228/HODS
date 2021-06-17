Imports System.Net.Mail
Imports System
Imports System.Globalization
Imports System.Threading

Public Class AlertEmailSaleAmout
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SQL As String = "",
            dateToday As String = Date.Today.AddDays(-1).ToString("yyyyMMdd")
        'dateToday = "20161227"
        Dim beginDate As String = dateToday.Substring(0, 6) & "01"
        'beginDate = ""

        Dim WHR As String = "where TA003 between '" & beginDate & "' and '" & dateToday & "' "

        Dim msg As String = "Dear Sir,<br/> Hoothai Sale Report <br/> From Date : " & configDate.dateShow2(beginDate, "-") & " To : " & configDate.dateShow2(dateToday, "-") & " <br/>"

        Dim sumAmt As Decimal = 0

        SQL = "select MA003,sum((TA041+TA042)*case when TA079=2 then -1 else 1 end) TA0412 from ACRTA left join COPMA on MA001=TA004 " & WHR & " group by MA003 order by MA003"
        msg &= "<TABLE border=1  BORDERCOLOR=blue ><TR ALIGN=CENTER><TH>Cust Name</TH><TH>Sale Amout</TH></TR>"
        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                msg &= "<TR><TD>" & .Item("MA003").ToString.Trim & "</TD> " & _
                       "    <TD ALIGN=RIGHT>" & FormatNumber(.Item("TA0412").ToString(), 2, TriState.True, TriState.True) & "</TD></TR>"
                sumAmt += .Item("TA0412")
            End With
        Next
        msg &= "<TR BGCOLOR=#a2a2a2><TD>Total Amount</TD> " &
               "    <TD ALIGN=RIGHT>" & FormatNumber(sumAmt.ToString(), 2, TriState.True, TriState.True) & "</TD></TR>"
        msg &= "</TABLE>"

        Dim myMail As New MailMessage()
        myMail.From = New MailAddress("alerts@jinpao.co.th", "IT Alert")
        'myMail.To.Add(New MailAddress("kung@jinpao.co.th", "IT"))
        myMail.To.Add(New MailAddress("charles@jinpao.co.th", "Mr.charles"))
        myMail.CC.Add(New MailAddress("victorchung@jinpao.co.th", "Mr.Chung"))
        myMail.CC.Add(New MailAddress("alerts@jinpao.co.th", "IT"))

        myMail.Subject = "** Alert System Hoothai Sale Report to Date " & configDate.dateShow2(dateToday, "-") & "**"
        myMail.IsBodyHtml = True
        myMail.Body = msg
        Dim smtp As New SmtpClient("mail.jinpao.co.th")
        smtp.Send(myMail)
    End Sub

End Class