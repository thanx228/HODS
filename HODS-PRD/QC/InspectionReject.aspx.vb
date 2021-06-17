Public Class InspectionReject
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If

    End Sub

    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrint.Click
        If tbType.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Type is null", UpdatePanel1)
            tbType.Focus()
            Exit Sub
        End If
        If tbNo.Text.Trim = "" Then
            show_message.ShowMessage(Page, "No is null", UpdatePanel1)
            tbNo.Focus()
            Exit Sub
        End If
        If tbSeq.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Seq is null", UpdatePanel1)
            tbSeq.Focus()
            Exit Sub
        End If

        Dim SQL As String = "",
            WHR As String = " where ",
            fldType As String = "TH001",
            fldNo As String = "TH002",
            fldSeq As String = "TH003",
            paraName As String = "",
           chrConn As String = Chr(8),
            filePrint As String = "qcInspecReject.rpt"
        SQL = " select *  from QMSTA " & _
              " left join PURTH on TH001=TA001 and TH002=TA002 and TH003=TA003 " & _
              " left join PURTG on TG001=TH001 and TG002=TH002 "
        If ddlSourceType.Text = "2" Then
            fldType = "TJ001"
            fldNo = "TJ002"
            fldSeq = "TJ003"
            filePrint = "qcInspecRejectFG.rpt"
            SQL = "select * from QMSTJ  left join SFCTC on TC001=TJ001 and TC002=TJ002 and TC003=TJ003 "
        End If

        If tbType.Text.Trim <> "" Then
            WHR = WHR & fldType & " ='" & tbType.Text.Trim & "' and "
        End If

        If tbNo.Text.Trim <> "" Then
            WHR = WHR & fldNo & " ='" & tbNo.Text.Trim & "' and "
        End If

        If tbSeq.Text.Trim <> "" Then
            Dim txtSeq As String = tbSeq.Text.Trim,
                txtZero As String = ""

            For i As Integer = 1 To 4 - txtSeq.Length
                txtZero = txtZero & "0"
            Next
            WHR = WHR & fldSeq & " = '" & txtZero & tbSeq.Text.Trim & "' and "
        End If
        WHR = WHR.Substring(0, WHR.Length - 4)

        SQL = SQL & WHR
        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        If dt.Rows.Count <> 1 Then
            show_message.ShowMessage(Page, "Please check Type or No or Seq!!", UpdatePanel1)
            tbType.Focus()
            Exit Sub
        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=" & filePrint & "&paraName=" & Server.UrlEncode("whr:" & WHR) & "&chrConn=" & chrConn & "&encode=1');", True)
    End Sub
End Class