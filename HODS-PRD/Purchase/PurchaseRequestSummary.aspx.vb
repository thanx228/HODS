Public Class PurchaseRequestSummary
    Inherits System.Web.UI.Page
    Dim configDate As New ConfigDate
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='31' order by MQ002"
            ControlForm.showDDL(ddlPrType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Private Function substrSQL(fld As String, val As String) As String
        Return " and SUBSTRING(" & fld & ",3,1) ='" & val & "' "
    End Function
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String, whr As String = ""
        Dim TypeCode As String = ddlType.Text
        Dim prType As String = ddlPrType.Text
        Dim endDate As String = configDate.dateFormat2(tbDate.Text)
        If TypeCode <> "0" Then
            If TypeCode < 5 Then
                whr = whr & substrSQL("B.TB004", TypeCode)
            End If
            Select Case TypeCode
                Case "5" '4100
                    whr = whr & " and INVMB.MB005 = '1401' "
                Case "6" '4200
                    whr = whr & " and INVMB.MB005 = '1402' "
                Case Else
                    whr = whr & " and INVMB.MB005 not in ('1401','1402') "
            End Select
        End If

        If prType <> "ALL" Then
            whr = whr & " and B.TB001='" & prType & "' "
        End If

        If endDate <> "" Then
            whr = whr & " and A.TA013<='" & endDate & "'"
        End If
        SQL = " select B.TB004 as 'JP PART',B.TB005+'-'+B.TB006 as 'JP SPEC',cast(sum(R.TR006) as decimal(20,3)) as 'PR Qty' from PURTR R " & _
              " left join PURTB B on B.TB001=R.TR001 and B.TB002=R.TR002 and TB003=TR003 " & _
              " left join PURTA A on A.TA001=B.TB001 and A.TA002=B.TB002 " & _
              " left join INVMB INVMB on INVMB.MB001=B.TB004 " & _
              " where R.TR019='' and B.TB039='N' and A.TA007 = 'Y' " & whr & _
              " group by B.TB004,B.TB005,B.TB006 order by B.TB004 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)

                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("JP Part")) Then
                    Dim link As String = ""
                    Dim jpPart As String = .DataItem("JP PART")

                    link = link & "&JPPart=" & jpPart
                    link = link & "&JPSpec=" & .DataItem("JP SPEC")
                    link = link & "&prQty=" & .DataItem("PR Qty")
                    link = link & "&prtype=" & ddlPrType.Text
                    link = link & "&endDate=" & configDate.dateFormat2(tbDate.Text)
                    hplDetail.NavigateUrl = "PurchaseRequestSummaryPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("PurchaseRequestSummary" & Session("UserName"), gvShow)
    End Sub
End Class