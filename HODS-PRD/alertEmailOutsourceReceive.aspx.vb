Imports System.Net.Mail
Imports System
Imports System.Globalization
Imports System.Threading
Public Class alertEmailOutsourceReceive
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Program As New DataTable
        Dim SQL As String = ""

        Dim dateToday As String = Date.Now.ToString("yyyyMMdd")
        SQL = " select TB.TB002 as trnNO,TC.TC003 as trnSeq,TC.TC004 as moType,TC.TC005 as moNo," & _
              " TB.TB005 as vendor,TA.TA006 as partNo,TA.TA035 as partSpec,TC.TC036 as trnQty from SFCTC TC " & _
              " left join SFCTB TB on(TB.TB001=TC.TC001 and TB.TB002=TC.TC002) " & _
              " left join MOCTA TA on(TA.TA001=TC.TC004 and TA.TA002=TC.TC005 ) " & _
              " where TC.TC001='D204' and TC.TC002 like '" & dateToday & "%' " & _
              " order by  TB.TB002,TC.TC003 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        If Program.Rows.Count > 0 Then
            Dim msg As String = ""
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-us")
            msg = "<TABLE border=1  BORDERCOLOR=blue>" & _
                  "<TR align='center'><TD>Transfer No</TD>" & _
                  "    <TD>Transfer Seq.</TD>" & _
                  "    <TD>MO Type.</TD>" & _
                  "    <TD>MO No.</TD>" & _
                  "    <TD>Vendor</TD>" & _
                  "    <TD>Part</TD>" & _
                  "    <TD>Spec</TD>" & _
                  "    <TD>Qty</TD></TR>"
            For i As Integer = 0 To Program.Rows.Count - 1
               
                msg = msg & "<TR><TD>" & Program.Rows(i).Item("trnNO") & "</TD> " & _
                            "    <TD>" & Program.Rows(i).Item("trnSeq") & "</TD>" & _
                            "    <TD>" & Program.Rows(i).Item("moType") & "</TD>" & _
                            "    <TD>" & Program.Rows(i).Item("moNo") & "</TD>" & _
                            "    <TD>" & Program.Rows(i).Item("vendor") & "</TD>" & _
                            "    <TD>" & Program.Rows(i).Item("partNo") & "</TD>" & _
                            "    <TD>" & Program.Rows(i).Item("partSpec") & "</TD>" & _
                            "    <TD align='right'>" & FormatNumber(Program.Rows(i).Item("trnQty").ToString(), 2, TriState.True, TriState.True) & "</TD></TR>"
            Next
            msg = msg & "</TABLE>"
            Dim myMail As New MailMessage()
            myMail.From = New MailAddress("mis@jinpao.co.th", "IT")
            myMail.To.Add(New MailAddress("kloijai@jinpao.co.th", "K.Kloijai"))
            myMail.To.Add(New MailAddress("htpurchase@jinpao.co.th", "K.Saipin"))
            'myMail.To.Add(New MailAddress("jppur@jinpao.co.th", "JP PUR"))
            'myMail.To.Add(New MailAddress("jppur_aero@jinpao.co.th", "Jppur aero"))
            'myMail.To.Add(New MailAddress("mis@jinpao.co.th", "IT"))
            myMail.CC.Add(New MailAddress("mis@jinpao.co.th", "IT"))

            myMail.Subject = "Alert E-Mail Outsource Recieve for Type D204 on " & Date.Now.ToString("dd-MM-yyyy")
            myMail.IsBodyHtml = True
            myMail.Body = msg
            Dim smtp As New SmtpClient("mail.jinpao.co.th")
            smtp.Send(myMail)
            '    lbShow.Text = "Send complete"
            'Else
            '    lbShow.Text = "not Send"
        End If
    End Sub
End Class