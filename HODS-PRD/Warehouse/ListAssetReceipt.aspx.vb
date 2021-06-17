Imports System.Globalization

Public Class ListAssetReceipt
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 like '33%' order by MQ001 "
            'ControlForm.showDDL(ddlType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 = 'CB' order by MQ002 "
            ControlForm.showCheckboxList(cblPOType, SQL, "MQ002", "MQ001", 4, Conn_SQL.ERP_ConnectionString)
        End If
    End Sub

    Function getWhr() As String
        Dim whr As String = ""
        Dim date1 As String = tbDateFrom.Text.Trim
        Dim date2 As String = tbDateTo.Text.Trim
        Dim whrData As String = ""
        Dim whrDataTo As String = ""
        If date1 <> "" Then
            whrData = configDate.dateFormat2(date1)
        Else
            whrData = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        If date2 <> "" Then
            whrDataTo = configDate.dateFormat2(date2)
        Else
            whrDataTo = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        whr = whr & Conn_SQL.Where("TO003", whrData, whrDataTo, False, False)
        whr = whr & Conn_SQL.Where("TO005", tbVendor, False)
        whr = whr & Conn_SQL.Where("(select top 1 TP009 from ASTTP where TP001=TO001 and TP002=TO002 order by TP003)", cblPOType)
        Return whr
    End Function

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        Dim WHR As String = getWhr()
        WHR = WHR & Conn_SQL.Where("TO013", cblApp)
        Dim SQL As String = " select TO001+'-'+TO002 TG001," & _
                            " (SUBSTRING(TO003,7,2)+'-'+SUBSTRING(TO003,5,2)+'-'+SUBSTRING(TO003,1,4)) TG014," & _
                            " rtrim(TO005)+'-'+MA002 TG005,TO010 TG016,(select top 1 TP009+'-'+TP010+'-'+TP011 from ASTTP where TP001=TO001 and TP002=TO002 order by TP003) TH011," & _
                            " TO013 TG013  from ASTTO " & _
                            " left join PURMA on MA001=TO005 " & _
                            " where " & WHR & " order by TO001,TO002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrint.Click
        'ListPurchaseReceipt.rpt
        Dim paraName As String = "whr:" & getWhr()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=ListAssetReceipt.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)

    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim hplDetail As HyperLink = CType(.FindControl("hlShow"), HyperLink)
                Dim code As String = .DataItem("TG001").ToString.Replace("Null", "")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("TG001")) Then

                    Dim link As String = "&docno=" & code
                    link = link & "&asset=Y"
                    hplDetail.NavigateUrl = "ListPurchaseReceiptPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", code)
                End If
            End If
        End With
    End Sub
End Class