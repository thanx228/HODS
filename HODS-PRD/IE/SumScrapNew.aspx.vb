Public Class SumScrapNew
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            ControlForm.showCheckboxList(cblWC, SQL, "MD002", "MD001", 4, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False
        End If

        If ddlProp.Text = "1" Then
            txtSupp.Enabled = False
            cblWC.Enabled = True
        Else
            txtSupp.Enabled = True
            cblWC.Enabled = False
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        Dim WHR As String = "",
           SQL As String = "",
           Supplier As String = txtSupp.Text.Trim

        If ddlProp.Text = "1" Then
            WHR = WHR & Conn_SQL.Where(" TC023 ", cblWC)
        Else
            If Supplier <> "" Then
                WHR = WHR & " and TB005 like '" & Supplier & "%' "
            End If
        End If

        Dim DateFrom As String = txtfrom.Text.Trim
        Dim DateTo As String = txtto.Text.Trim

        If DateFrom <> "" Or DateTo <> "" Then
            WHR = WHR & Conn_SQL.Where(" TB015 ", configDate.dateFormat(DateFrom), configDate.dateFormat(DateTo))
        End If

        SQL = " select TM009 as 'Transfer Type' ,TM010 as 'Transfer No' ,  TC023+ ':' +TB006 as 'Work Center', " & _
              " rtrim(TB023)+'-'+ME002 'Dept.',TA001 as 'MO Type',TA002 as 'MO No.'," & _
              " case when len(TA006)=16 then (SUBSTRING(TA006,1,14)+'-'+SUBSTRING(TA006,15,2)) else TA006 end as 'Item',TA035 as 'Spec', " & _
              " CAST(TA015 as decimal(16,3)) as 'MO Qty', " & _
              " CAST(TM007 as decimal(16,3)) as 'Scrap Qty', " & _
              " CAST((TM007/TA015)*100 as decimal(16,2))  as 'Scrap%', " & _
              " TB014 as 'Remark' " & _
              " from INVTM " & _
              " left join INVTL on TL001 = TM001 and TL002 = TM002 " & _
              " left join SFCTC on TC001 = TM009 AND TC002 = TM010 and TC003 = TM011 " & _
              " left JOIN SFCTB on TB001 = TC001 and TB002 = TC002 " & _
              " left JOIN MOCTA on TA001 = TC004 AND TA002 = TC005 " & _
              " left join CMSME on ME001 = TB023 " & _
              " where 1=1 " & WHR & _
              " group by TM009,TM010,TC023,TA002,TA006,TA035,TC014,TM007,TM001,TM002,TM003,TA015,TB005,TA001,TB014,TB006,TB023,ME002 " & _
              " order by TM009,TM010,TC023 "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SummaryScrap" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class
