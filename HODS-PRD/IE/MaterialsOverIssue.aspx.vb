Public Class MaterialsOverIssue
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 in ('W01','W02','W07','W12','W19','W23','W25','W27')
            ControlForm.showDDL(ddlWc, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)

            btExcel.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Dim SQL As String = "",
            WHR As String = ""
        'mo status
        Select Case ddlMoStatus.Text
            Case "1" 'close status
                WHR = WHR & " and TA011 in ('y','Y') "
            Case "2" 'manufacturing status
                WHR = WHR & " and TA011 in ('1','2','3') "
        End Select
        'code type
        If ddlCodeType.Text <> "0" Then
            WHR = WHR & " and SUBSTRING(TB003,3,1) ='" & ddlCodeType.Text & "' "
        Else
            WHR = WHR & " and SUBSTRING(TB003,3,1) in ('1','3','4')  "
        End If
        'over issue %
        Dim fldOverIssue As String = ""
        Dim percentType As String = ddlOverIssue.Text
        If percentType <> "0" Then
            WHR = WHR & " and ((TB005/TB004)*100)-100  "
            Select Case ddlOverIssue.Text
                Case "1" '1-5%
                    WHR = WHR & " between '1' and '5' "
                Case "2" '6-10%
                    WHR = WHR & " between '6' and '10' "
                Case "3" '11-15%
                    WHR = WHR & " between '11' and '15' "
                Case "4" '16-20%
                    WHR = WHR & " between '16' and '20' "
                Case "5" '21-25%
                    WHR = WHR & " between '21' and '25' "
                Case "6" '25-30%
                    WHR = WHR & " between '25' and '30' "
                Case "7" '>30%
                    WHR = WHR & " > '30' "
            End Select
        End If

        WHR = WHR & configDate.DateWhere("TA040", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
        Dim SQL1 As String = "(select top 1 TC005 from MOCTE left join MOCTC on TC001=TE001 and TC002=TE002 where TE011=TB001 and TE012=TB002 and TE004=TB003 ) "

        Dim wc As String = ddlWc.Text.Trim
        If wc <> "ALL" Then
            WHR = WHR & " and " & SQL1 & "='" & wc & "' "
        End If

        SQL = " select " & SQL1 & " as 'WC' ,TB001 as 'MO Type',TB002 as 'MO No',(SUBSTRING(TA040,7,2)+'-'+SUBSTRING(TA040,5,2)+'-'+SUBSTRING(TA040,1,4)) as 'MO Date', " & _
              " TB003 as 'RM Item',TB012+'-'+TB013 as 'Spec',cast(TB004 as decimal(20,4)) as 'Req Qty', " & _
              " cast(TB005 as decimal(20,4)) as 'QPA Qty',cast(TB005-TB004 as decimal(20,4)) as 'Over Qty'," & _
              " TB007 as 'Unit',cast(((TB005/TB004)*100)-100 as decimal(10,2)) as 'Over %' " & _
              " from MOCTB left join MOCTA on TA001=TB001 and TA002=TB002 " & _
              " where TB005>TB004 and TB004>0 and TB018='Y' and TA013='Y' " & WHR & _
              " order by TB001,TB002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExcel.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        ControlForm.ExportGridViewToExcel("MaterialsOverIssue" & Session("UserName"), gvShow)
    End Sub
    'Private Function substrSQL(fld As String, val As String) As String
    '    Return " and SUBSTRING(" & fld & ",3,1) ='" & val & "' "
    'End Function
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)

                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("RM Item")) Then
                    Dim link As String = ""
                    Dim jpPart As String = .DataItem("RM Item")
                    link = link & "&rwCode= " & jpPart
                    link = link & "&moType= " & .DataItem("MO Type")
                    link = link & "&moNo= " & .DataItem("MO No")
                    link = link & "&issueQty= " & .DataItem("QPA Qty")
                    link = link & "&issueOver= " & .DataItem("Over Qty")
                    hplDetail.NavigateUrl = "MaterialsOverIssuePop.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub
End Class