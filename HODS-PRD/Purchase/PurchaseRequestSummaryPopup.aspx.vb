Public Class PurchaseRequestSummaryPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim code As String = Request.QueryString("JPPart").Trim
            Dim prType As String = Request.QueryString("prtype").Trim
            Dim endDate As String = Request.QueryString("endDate").TrimEnd
            lbCode.Text = code
            lbSpec.Text = Request.QueryString("JPSpec").TrimEnd
            lbPR.Text = FormatNumber(Request.QueryString("prQty"), 2, TriState.True)
            Dim SQL As String, whr As String = ""

            If prType <> "ALL" Then
                whr = whr & " and B.TB001='" & prType & "' "
            End If

            If endDate <> "" Then
                whr = " and A.TA003<='" & endDate & "'  "
            End If
            'B.TB004 as 'JP PART',B.TB005+'-'+B.TB006 as 'JP SPEC',cast(sum(R.TR006) as decimal(20,3)) as 'PR Qty'
            SQL = " select B.TB001+'-'+B.TB002+'-'+B.TB003 as 'PR Detail',B.TB004 as 'JP Part'," & _
                  " B.TB005+'-'+B.TB006 as SPEC,B.TB007 as 'unit' ,cast(B.TB009 as decimal(15,3)) as 'PR qty'," & _
                  " B.TB010 as 'Supplier'," & _
                  " (SUBSTRING(B.TB011,7,2)+'/'+SUBSTRING(B.TB011,5,2)+'/'+SUBSTRING(B.TB011,1,4)) as 'Request Date'," & _
                  "  (SUBSTRING(A.TA013,7,2)+'/'+SUBSTRING(A.TA013,5,2)+'/'+SUBSTRING(A.TA013,1,4)) as 'Aproved Date' from PURTR R " & _
                  " join PURTB B on B.TB001=R.TR001 and B.TB002=R.TR002 and B.TB003=R.TR003 " & _
                  " left join PURTA A on A.TA001=B.TB001 and A.TA002=B.TB002 " & _
                  " where R.TR019='' and A.TA007 = 'Y' and B.TB039='N' and B.TB004='" & code & "' " & whr & "  order by B.TB004 "
            ControlForm.ShowGridView(gvPr, SQL, Conn_SQL.ERP_ConnectionString)
            lbCount.Text = gvPr.Rows.Count
        End If
    End Sub
End Class