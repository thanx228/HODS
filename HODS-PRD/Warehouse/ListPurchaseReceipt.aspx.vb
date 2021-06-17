Imports System.Globalization

Public Class ListPurchaseReceipt
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
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 = '33' order by MQ002 "
            ControlForm.showCheckboxList(cblPOType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)
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

        whr = whr & Conn_SQL.Where("TG014", whrData, whrDataTo, False, False)
        whr = whr & Conn_SQL.Where("TG005", tbVendor, False)
        whr = whr & Conn_SQL.Where("(select top 1 TH011 from PURTH where TH001=TG001 and TH002=TG002 order by TH003)", cblPOType)
        Return whr
    End Function

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        Dim WHR As String = getWhr()
        WHR = WHR & Conn_SQL.Where("TG013", cblApp)
        Dim SQL As String = " select TG001+'-'+TG002 as TG001," & _
                            " (SUBSTRING(TG014,7,2)+'-'+SUBSTRING(TG014,5,2)+'-'+SUBSTRING(TG014,1,4)) as TG014," & _
                            " rtrim(TG005)+'-'+MA002 as TG005,TG016,(select top 1 TH011+'-'+TH012+'-'+TH013 from PURTH where TH001=TG001 and TH002=TG002 order by TH003) as TH011," & _
                            " TG013  from PURTG " & _
                            " left join PURMA on MA001=TG005 " & _
                            " where " & WHR & " order by TG001,TG002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrint.Click
        'ListPurchaseReceipt.rpt
        Dim paraName As String = "whr:" & getWhr()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=ListPurchaseReceipt.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)

    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim hplDetail As HyperLink = CType(.FindControl("hlShow"), HyperLink)
                Dim code As String = .DataItem("TG001").ToString.Replace("Null", "")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("TG001")) Then

                    Dim link As String = "&docno=" & code
                    link = link & "&asset=N"
                    hplDetail.NavigateUrl = "ListPurchaseReceiptPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", code)
                End If
            End If
        End With
    End Sub
End Class