Public Class BudgetReportPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack() Then

            Dim Dept As String = Request.QueryString("Dept").ToString.Trim
            lbDept.Text = Dept
            lbInv.Text = Request.QueryString("Inv").ToString.Trim
            lbNonInv.Text = Request.QueryString("NonInv").ToString.Trim
  
            Dim WHRInv As String = "",
                WHRNon As String = ""
            Dim DateFrom As String = Request.QueryString("DateFrom").ToString.Trim
            Dim DateTo As String = Request.QueryString("DateTo").ToString.Trim

            If DateFrom <> "" Then
                WHRInv = WHRInv & Conn_SQL.Where("INVTA.TA014 >= ", configDate.dateFormat2(DateFrom))
            End If

            If DateTo <> "" Then
                WHRInv = WHRInv & Conn_SQL.Where("INVTA.TA014 <= ", configDate.dateFormat2(DateTo))
            End If

            Dim SQL As String = ""

            'Inv

            SQL = " select INVTA.TA001 as 'Order Type', CMSMQ.MQ002 as 'Order Description', INVTA.TA002 as 'Order No.'," & _
       " INVTA.TA004 as 'Dept.', CMSME.ME002 as 'Dept Name' ," & _
       " (SUBSTRING(INVTA.TA014,7,2)+'-'+SUBSTRING(INVTA.TA014,5,2)+'-'+SUBSTRING(INVTA.TA014,1,4)) as 'Date', INVTB.TB004 as 'Item', " & _
       " INVTB.TB005 as 'Item Description'," & _
       " INVTB.TB006 as 'Specification', INVTB.TB008 as 'Unit', convert(decimal(16,2),(INVTB.TB009)) as 'Quantity'," & _
       " convert(decimal(16,6),(INVTB.TB010)) as 'Price',convert(decimal(16,2),(INVTB.TB011)) as 'Amount', " & _
       " AJSMB.MB006 as 'Account Code'" & _
       " from INVTA " & _
       " left join INVTB on INVTB.TB001 = INVTA.TA001 and INVTB.TB002 = INVTA.TA002 " & _
       " left join CMSMQ on CMSMQ.MQ001 = INVTA.TA001 " & _
       " left join AJSMB on AJSMB.MB002 = INVTA.TA001 " & _
       " left join CMSME on CMSME.ME001 = INVTA.TA004 " & _
       " where INVTA.TA001 between '1105' and '1199' and INVTA.TA004 = '" & Dept & "' " & WHRInv & _
       " order by INVTA.TA001,INVTA.TA002,INVTA.TA004"

            ControlForm.ShowGridView(gvShowInv, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountInv.Text = ControlForm.rowGridview(gvShowInv)

            If DateFrom <> "" Then
                WHRNon = WHRNon & Conn_SQL.Where("ASTTO.TO003 >= ", configDate.dateFormat2(DateFrom))
            End If

            If DateTo <> "" Then
                WHRNon = WHRNon & Conn_SQL.Where("ASTTO.TO003 <= ", configDate.dateFormat2(DateTo))
            End If

            'Non-Inv
            SQL = " select ASTTO.TO001  as 'Receipt Type', CMSMQ.MQ002 as 'Receipt Description', ASTTO.TO002 as 'Receipt No.'," & _
                " ASTTP.UDF01 as 'Dept.', isnull(CMSME.ME002,'') as 'Dept Name' , " & _
                " (SUBSTRING(ASTTO.TO003,7,2)+'-'+SUBSTRING(ASTTO.TO003,5,2)+'-'+SUBSTRING(ASTTO.TO003,1,4)) as 'Date'," & _
                " ASTTP.TP004 as 'Item',ASTTP.TP005 as 'Item Description'," & _
                " ASTTP.TP006 as 'Specification', ASTTP.TP008 as 'Unit', convert(decimal(16,2),(ASTTP.TP007)) as 'Quantity', " & _
                "convert(decimal(16,6),(ASTTP.TP016)) as 'Price',convert(decimal(16,2),(ASTTP.TP017)) as 'Amount', " & _
                " AJSMB.MB006 as 'Account Code'" & _
                " from ASTTO " & _
                " left join ASTTP on ASTTP.TP001 = ASTTO.TO001 and ASTTP.TP002 = ASTTO.TO002 " & _
                " left join CMSMQ on CMSMQ.MQ001 = ASTTO.TO001" & _
                " left join AJSMB on AJSMB.MB002 = ASTTO.TO001 " & _
                " left join CMSME on CMSME.ME001 =ASTTP.UDF01 " & _
                " where ASTTP.UDF01 = '" & Dept & "' " & WHRNon & _
                " order by ASTTO.TO001,ASTTO.TO002,ASTTP.UDF01"

                    ControlForm.ShowGridView(gvShowNon, SQL, Conn_SQL.ERP_ConnectionString)
                    lbCountNonInv.Text = ControlForm.rowGridview(gvShowNon)

                End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExportInv_Click(sender As Object, e As EventArgs) Handles btExportInv.Click
        ControlForm.ExportGridViewToExcel("BudgetingReportInv" & Session("UserName"), gvShowInv)
    End Sub

    Protected Sub btExportNon_Click(sender As Object, e As EventArgs) Handles btExportNon.Click
        ControlForm.ExportGridViewToExcel("BudgetingReportNonInv" & Session("UserName"), gvShowNon)
    End Sub
End Class