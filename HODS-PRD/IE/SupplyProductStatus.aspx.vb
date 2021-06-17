Public Class SupplyProductStatus
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim prTypeAll As String = "'3112','3113'"
    Dim poTypeAll As String = "'3312','3313'"
    Dim codeType As String = "'1401','1402','1201'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MA002,MA002+' : '+MA003 as MA003 from INVMA where MA001='1' and MA002 in(" & codeType & ") order by MA002"
            ControlForm.showDDL(ddlCodeType, SQL, "MA003", "MA002", True, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            btExport.Visible = False
         End If
    End Sub

    Protected Sub btShowReport_Click(sender As Object, e As EventArgs) Handles btShowReport.Click
        Dim SQL As String = "",
            WHR As String = "",
            codeType As String = ddlCodeType.Text.Trim,
            docNo As String = tbDocNo.Text.Trim,
            supCode As String = tbSup.Text.Trim,
            item As String = tbItem.Text.Trim,
            spec As String = tbSpec.Text.Trim,
            desc As String = tbDesc.Text.Trim,
            dueDate As String = configDate.dateFormat2(tbDueDate.Text),
            overDate As Integer = CInt(ddlOverDue.Text.Trim)

        Select Case ddlShowType.Text
            Case "1" ' PR NOT OPEN PO
                WHR = ""
                If codeType <> "ALL" Then
                    WHR = WHR & " and MB005 ='" & codeType & "' "
                    If codeType = "1201" Then
                        WHR = WHR & " and MB017 ='2207' "
                    End If
                Else
                    WHR = WHR & " and (MB005  in ('1401','1402') or (MB005 ='1201' and MB017 ='2207') )"
                End If
                WHR = WHR & Conn_SQL.Where("TB002", tbDocNo)
                WHR = WHR & Conn_SQL.Where("TB004", tbItem)
                WHR = WHR & Conn_SQL.Where("TB005", tbDesc)
                WHR = WHR & Conn_SQL.Where("TB006", tbSpec)
                WHR = WHR & Conn_SQL.Where("TB011", "", dueDate)
                If dueDate = "" Then
                    dueDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
                End If
                If overDate > 0 Then
                    WHR = WHR & " and  DATEDIFF(DD,CONVERT(datetime,TB011),'" & dueDate & "')  "
                    Select Case overDate
                        Case 5
                            WHR = WHR & " between '1' and '5' "
                        Case 10
                            WHR = WHR & " between '6' and '10' "
                        Case 15
                            WHR = WHR & " between '11' and '15' "
                        Case Else
                            WHR = WHR & ">'15' "
                    End Select
                End If
                SQL = " select TB001+'-'+TB002+'-'+TB003 as 'PR Details', " & _
                      " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'Doc Date', " & _
                      " (SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4)) as 'Request Date', " & _
                      " TB004 as 'Item', TB005 as 'Desc', TB006 as 'Spec', " & _
                      " cast(TR006 as decimal(15,2)) as 'PR Qty',TB007 as 'Unit', " & _
                      " cast(DATEDIFF(DD,CONVERT(datetime,TB011),'" & dueDate & "') as integer) as 'Over Due' " & _
                      " from PURTR " & _
                      " left join PURTB on TB001=TR001 and TB002=TR002 and TB003=TR003 " & _
                      " left join PURTA on TA001=TB001 and TA002=TB002  " & _
                      " left join INVMB on MB001=TB004 " & _
                      " where TR019='' and TB039='N' " & WHR & _
                      " order by TB004 "
            Case "2" ' PO NOT CLOSE
                WHR = ""
                If codeType <> "ALL" Then
                    WHR = WHR & " and MB005 ='" & codeType & "' "
                    If codeType = "1201" Then
                        WHR = WHR & " and MB017 ='2207' "
                    End If
                Else
                    WHR = WHR & " and (MB005  in ('1401','1402') or (MB005 ='1201' and MB017 ='2207') )"
                End If
                WHR = WHR & Conn_SQL.Where("TD002", tbDocNo)
                WHR = WHR & Conn_SQL.Where("TD004", tbItem)
                WHR = WHR & Conn_SQL.Where("TD005", tbDesc)
                WHR = WHR & Conn_SQL.Where("TD006", tbSpec)
                WHR = WHR & Conn_SQL.Where("TC004", tbSup)
                WHR = WHR & Conn_SQL.Where("TB011", "", dueDate)

                If dueDate = "" Then
                    dueDate = Date.Today.ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
                End If
                If overDate > 0 Then
                    WHR = WHR & " and  DATEDIFF(DD,CONVERT(datetime,TD012),'" & dueDate & "')  "
                    Select Case overDate
                        Case 5
                            WHR = WHR & " between '1' and '5' "
                        Case 10
                            WHR = WHR & " between '6' and '10' "
                        Case 15
                            WHR = WHR & " between '11' and '15' "
                        Case Else
                            WHR = WHR & ">'15' "
                    End Select
                End If

                SQL = " select  TD001+'-'+TD002+'-'+TD003 as 'PO Detail'," & _
                      " case when TD026='' then '' else  TD026+'-'+TD027 end as 'PR Detail'," & _
                      " TD004 as 'Item', TD005 as 'Desc', TD006 as 'Spec', " & _
                      " case when TD012='' then '' else  (SUBSTRING(TD012,7,2)+'-'+SUBSTRING(TD012,5,2)+'-'+SUBSTRING(TD012,1,4)) end as 'Delivery Date'," & _
                      " cast(TD008 as decimal(15,2)) as 'PO QTY'," & _
                      " cast(TD015 as decimal(15,2)) as 'Receipt QTY', " & _
                      " cast(TD008-TD015 as decimal(15,2)) as 'Balance QTY'," & _
                      " TD009 as Unit,TC004 as 'Supplier ID'," & _
                      " cast(DATEDIFF(DD,CONVERT(datetime,TD012),'" & dueDate & "') as integer) as 'Over Due' " & _
                      " from PURTD " & _
                      " left join PURTC on TC001 = TD001 and TC002 = TD002 " & _
                      " left join INVMB on MB001=TD004 " & _
                      " where  TD016='N' " & WHR & " order by TD001,TD002,TD003 "
        End Select
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SupplyProductStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class