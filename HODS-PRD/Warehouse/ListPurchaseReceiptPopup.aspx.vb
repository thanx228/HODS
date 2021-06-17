Public Class ListPurchaseReceiptPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ii() As String = Request.QueryString("docno").ToString.Trim.Split("-")
        Dim noiVal As String = Request.QueryString("asset").Trim
        lbType.Text = ii(0)
        lbNumber.Text = ii(1)
        Dim SQL As String
        If noiVal = "Y" Then 'asset
            SQL = " select TP001+'-'+TP002 as TH001,TP003 TH003,TP005 TH004,TP006 TH005," & _
                  " '' TH006,TP007 TH007,TP013 TH015,'' TH017, TP008 TH008,TP009 TH011, TP010 TH012,TP011 TH013,TP028 TH033," & _
                  " (SUBSTRING(TP012,7,2)+'-'+SUBSTRING(TP012,5,2)+'-'+SUBSTRING(TP012,1,4)) as TH014, " & _
                  " '' TH028 " & _
                  " from ASTTP where TP001='" & lbType.Text.Trim & "' and TP002='" & lbNumber.Text.Trim & "' order by TP003 "

        Else 'purchase

            SQL = " select TH001+'-'+TH002 as TH001,TH003,TH004,TH005,TH006,TH007,TH015,TH017,TH008,TH011,TH012,TH013,TH033," & _
                  " (SUBSTRING(TH014,7,2)+'-'+SUBSTRING(TH014,5,2)+'-'+SUBSTRING(TH014,1,4)) as TH014, " & _
                  " case TH028 when '0' then '0:No Inspection' when '1' then '1:To Inspected' when '2' then '2:Qualified' when '3' then '3:Defective' when '4' then '4:Special Receipt' else 'not Action' end as TH028 " & _
                  " from PURTH where TH001='" & lbType.Text.Trim & "' and TH002='" & lbNumber.Text.Trim & "' order by TH003 "

        End If

        ControlForm.ShowGridView(gvPur, SQL, Conn_SQL.ERP_ConnectionString)
        lbCountSO.Text = ControlForm.rowGridview(gvPur)
    End Sub
End Class