Imports System.Net.Mail
Public Class alertItemFollowLot
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim Table As String = "ItemFollowLot"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SQL As String = "select * from " & Table & " where status='10' order by Id,item"
        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim item As String = .Item("item").ToString.Trim
                Dim cLot As String = .Item("LotCheck").ToString.Trim
                Dim sSQL As String = "select top " & cLot & " TA001,TA002 from MOCTA " & _
                    " where TA006='" & item & "' and TA012>='" & .Item("dateStart").ToString.Trim & "' " & _
                    " and TA011 ='Y' " & _
                    " order by TA012"
                Dim dt2 As DataTable = Conn_SQL.Get_DataReader(sSQL, Conn_SQL.ERP_ConnectionString)
                If dt2.Rows.Count = cLot Then
                    Dim txt As String = item & " Include MO <br/> "
                    For j As Integer = 0 To dt2.Rows.Count - 1
                        With dt2.Rows(j)
                            txt = txt & .Item("TA001").ToString.Trim & "-" & .Item("TA002").ToString.Trim & ",<br/>"
                        End With
                    Next
                    'send mail
                    Dim myMail As New MailMessage()
                    myMail.From = New MailAddress("mis@jinpao.co.th", "IT")
                    myMail.To.Add(New MailAddress("jpqc-center@jinpao.co.th", "QC Center JP"))
                    myMail.To.Add(New MailAddress("watcharapong@jinpao.co.th", "K.Watcharapong"))
                    myMail.To.Add(New MailAddress("jpqc@(jinpao.co.th)", "QC JP"))
                    myMail.CC.Add(New MailAddress("mis@jinpao.co.th", "IT"))

                    myMail.Subject = "Alert E-Mail Item follow Lot "
                    myMail.IsBodyHtml = True
                    myMail.Body = txt
                    Dim smtp As New SmtpClient("mail.jinpao.co.th")
                    smtp.Send(myMail)

                End If

            End With
        Next




    End Sub

End Class