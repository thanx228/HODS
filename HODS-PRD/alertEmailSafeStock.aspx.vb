Imports System.Net.Mail
Imports System
Imports System.Globalization
Imports System.Threading

Public Class alertEmailOverSafeStock
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SQL As String = ""

        'inventory for safty stock
        Dim tempTable As String = "tempSaftyStock" & Session("UserName")
        CreateTempTable.createTempAlertSaftyStock(tempTable)
        'po
        SQL = " select MC001,SUM(isnull(TD008,0)-isnull(TD015,0))  from INVMC " & _
              " left join PURTD on TD004=MC001 " & _
              " left join PURTC on TC001=TD001 and TC002=TD002 " & _
              " where MC004>0 and TD016='N' and substring(MC001,3,1) in ('1','4') " & _
              " group by MC001 order by MC001 "
        SQL = " insert into DBMIS.dbo." & tempTable & "(item,poQty) " & SQL
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.ERP_ConnectionString)
        'pr
        SQL = " select MC001,sum(TR006) as pr from INVMC " & _
              " left join PURTB on TB004=MC001 " & _
              " left join PURTR on TR001=TB001 and TR002=TB002 and TR003=TB003 " & _
              " left join PURTA on TA001=TB001 and TA002=TB002 " & _
              " where MC004>0 and TB039='N' and TA007 = 'Y' and substring(MC001,3,1) in ('1','4') " & _
              " group by MC001 order by MC001 "
        Dim dt As New DataTable,
            USQL As String
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " if not exists(select * from " & tempTable & " where item='" & .Item("MC001") & "' ) " & _
                       " insert into " & tempTable & "(item,prQty)values ('" & .Item("MC001") & "','" & .Item("pr") & "') else " & _
                       " update " & tempTable & " set prQty='" & .Item("pr") & "' where item='" & .Item("MC001") & "' "
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            End With
        Next

        'Purchase reciept
        SQL = " select MC001,SUM(isnull(TH007,0)) as pi from INVMC " & _
              " left join PURTH on MC001=TH004 " & _
              " left join PURTG on TG001=TH001 and TG002=TH002 " & _
              " where MC004>0 and PURTG.TG013 = 'N' and substring(MC001,3,1) in ('1','4') " & _
              " group by MC001 having (SUM(isnull(TH007, 0)) > 0) " & _
              " order by MC001 "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                USQL = " if not exists(select * from " & tempTable & " where item='" & .Item("MC001") & "' ) " & _
                       " insert into " & tempTable & "(item,piQty)values ('" & .Item("MC001") & "','" & .Item("pi") & "') else " & _
                       " update " & tempTable & " set piQty='" & .Item("pi") & "' where item='" & .Item("MC001") & "' "
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            End With
        Next

        sendMail("1", tempTable) 'Materials
        sendMail("4", tempTable) 'SP and Other

    End Sub
    Sub sendMail(CodeType As String, tempTable As String)
        Dim SQL As String = "",
            HeadMail As String = " Alert E-Mail Control Safety Stock ",
            sendMat As Boolean

        If CodeType = "1" Then
            HeadMail = HeadMail & "Materials"
            sendMat = True
        ElseIf CodeType = "4" Then
            HeadMail = HeadMail & "Spare Part and Other"
            sendMat = False
        End If
        'HeadMail = HeadMail & " test by IT "

        SQL = " select MB001,MB002+'-'+MB003 as MB003,MC004,MB064,isnull(prQty,0) prQty,isnull(poQty,0) poQty,isnull(piQty,0) piQty from INVMC " & _
              " left join INVMB on MB001=MC001  " & _
              " left join DBMIS.dbo." & tempTable & " T on T.item=MC001 " & _
              " where MC004>0 and MC004>MB064 and substring(MB001,3,1) in ('" & CodeType & "') order by MB001 "

        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        If Program.Rows.Count > 0 Then
            Dim msg As String = ""
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-us")
            msg = "<TABLE border=1  BORDERCOLOR=blue><TR><TD>Code</TD><TD>Spec</TD><TD>Safe stock</TD><TD>Stock</TD><TD>PR</TD><TD>PO</TD><TD>Inspec</TD></TR>"
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    Dim safeStock As Double = .Item("MC004")
                    Dim stock As Double = .Item("MB064")
                    Dim code As String = Program.Rows(i).Item("MB001")
                    Dim prQty As Double = .Item("prQty")
                    Dim poQty As Double = .Item("poQty")
                    Dim piQty As Double = .Item("piQty")

                    msg = msg & "<TR><TD>" & .Item("MB001").ToString.Trim & "</TD> " & _
                                "    <TD>" & .Item("MB003").ToString.Trim & "</TD>" & _
                                "    <TD>" & FormatNumber(safeStock.ToString(), 2, TriState.True, TriState.True) & "</TD>" & _
                                "    <TD>" & FormatNumber(stock.ToString(), 2, TriState.True, TriState.True) & "</TD>" & _
                                "    <TD>" & FormatNumber(prQty.ToString(), 2, TriState.True, TriState.True) & "</TD>" & _
                                "    <TD>" & FormatNumber(poQty.ToString(), 2, TriState.True, TriState.True) & "</TD>" & _
                                "    <TD>" & FormatNumber(piQty.ToString(), 2, TriState.True, TriState.True) & "</TD></TR>"
                End With
            Next
            msg = msg & "</TABLE>"
            Dim myMail As New MailMessage()
            myMail.From = New MailAddress("mis@jinpao.co.th", "IT")
            myMail.To.Add(New MailAddress("kloijai@jinpao.co.th", "K.Kloijai"))
            myMail.To.Add(New MailAddress("saypin@jinpao.co.th", "K.Saypin"))
            myMail.To.Add(New MailAddress("jppur@jinpao.co.th", "JPPUR"))
            myMail.To.Add(New MailAddress("jppur_mc@jinpao.co.th", "JPPUR_MC"))
            If sendMat Then
                myMail.To.Add(New MailAddress("jppur_mat@jinpao.co.th", "JPPUR_MAT"))
            Else
                myMail.To.Add(New MailAddress("jppur_eq@jinpao.co.th", "JPPUR_EQ"))
                myMail.To.Add(New MailAddress("jppur_oversea@jinpao.co.th", "JPPUR_OVERSEA"))
            End If
            'myMail.To.Add(New MailAddress("jpnct@jinpao.co.th", "Planning 1"))
            'myMail.To.Add(New MailAddress("jpcnc@jinpao.co.th", "Planning 2"))
            myMail.CC.Add(New MailAddress("mis@jinpao.co.th", "IT"))

            myMail.Subject = HeadMail
            myMail.IsBodyHtml = True
            myMail.Body = msg
            Dim smtp As New SmtpClient("mail.jinpao.co.th")
            smtp.Send(myMail)
            lbShow.Text = "Send complete"
            'Else
            '    lbShow.Text = "not Send"
        End If
    End Sub
End Class