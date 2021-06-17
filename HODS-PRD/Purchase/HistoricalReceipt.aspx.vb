Imports System.Globalization
Imports MIS_HTI.DataControl
Public Class HistoricalReceipt
    Inherits System.Web.UI.Page

    Dim configDate As New ConfigDate
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim dbConn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            btExport.Visible = False
            Dim txtDateFrom As String = String.Empty
            Dim txtDateTo As String = String.Empty

            txtDateFrom = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("dd/MM/yyyy", New CultureInfo("en-US"))
            txtDateTo = DateSerial(Year(Date.Now()), Month(Date.Now()), 0).ToString("dd/MM/yyyy", New CultureInfo("en-US"))

            FromDate.dateVal = txtDateFrom
            ToDate.dateVal = txtDateTo
        End If
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click

        Dim SQL As String = ""
        Dim WHR As String = ""

        WHR = WHR & Conn_SQL.Where("TH004", tbItem)

        Dim TypeSO As String = ""

        If ddlSO.Text.ToString = "2213" Then
            WHR = WHR & "and isnull(case when PURTD.TD013 = '' then LRPTC.TC026 else PURTD.TD013 end,'') = '2213'"
        ElseIf ddlSO.Text.ToString = "Other" Then
            WHR = WHR & "and isnull(case when PURTD.TD013 = '' then LRPTC.TC026 else PURTD.TD013 end,'') <> '2213'"
        End If

        If ddlStatus.SelectedValue = "Y" Then
            WHR = WHR & " and PURTH.TH030 = 'Y'"
        ElseIf ddlStatus.SelectedValue = "N" Then
            WHR = WHR & " and PURTH.TH030 = 'N'"
        ElseIf ddlStatus.SelectedValue = "V" Then
            WHR = WHR & " and PURTH.TH030 = 'V'"
        End If

        WHR = WHR & configDate.DateWhere("PURTG.TG014", (FromDate.dateVal), (ToDate.dateVal))

        SQL = " select distinct TG005 as 'Supplier' , TG021 as 'Supplier Short Desc.' ,COPTC.TC012 as 'Industry', " &
            " PURTH.TH011+'-'+PURTH.TH012+'-'+PURTH.TH013 as 'PO NO', " &
            " substring(PURTC.TC003,1,4)+'-'+substring(PURTC.TC003,5,2)+'-'+substring(PURTC.TC003,7,2) as 'Purchase Date', " &
            " case when len(TH004)=16 then substring(TH004,1,14)+'-'+substring(TH004,15,2) else TH004 end as 'Item', TH005 as 'Item Desc' , TH006 as 'Spec' , " &
            " PURTH.TH001+'-'+PURTH.TH002+'-'+PURTH.TH003 as 'PUR Receipt No', substring(TG003,1,4)+'-'+substring(TG003,5,2)+'-'+substring(TG003,7,2) as 'Accept/Return Date', " &
            " case when TD014 = '' then substring(PURTD.TD012,1,4)+'-'+substring(PURTD.TD012,5,2)+'-'+substring(PURTD.TD012,7,2) else TD014 end as 'Plan Confirm Delivery', " &
            " cast(TH015 as decimal(16,2)) AS 'Accept/Return Qty',PURTH.TH008 as 'Unit',cast(PURTH.TH018 as decimal(16,2)) as 'Price', " &
            " cast(PURTG.TG008 as decimal(16,2)) as 'Exchange Rate', PURTG.TG007 as 'Currency',cast(PURTH.TH045 as decimal(16,2)) as 'Receipt/Return Total(O/C)', " &
            " cast(PURTH.TH046 as decimal(16,2)) as 'Receipt/Return Tax Total(O/C)', " &
            " cast(PURTH.TH045+PURTH.TH046 as decimal(16,2)) as 'Receipt/Return Total(O/C)', CMSMC.MC002 as 'Delivery Warehouse'," &
            " isnull(case when PURTD.TD013 = '' then LRPTC.TC026+'-'+LRPTC.TC027 else PURTD.TD013+'-'+PURTD.TD021 end,'') as 'SO', " &
            " case when PURTH.TH030='Y' then 'Y:Approved' when PURTH.TH030='N' then 'N:Not Approved' else 'V:Cancel' end as 'Approved Status', " &
            " CMSMV.MV002 as 'Buyer','Inspection Receipt' as 'Action',TH033 as 'Remark' " &
            " from PURTD " &
            " left join PURTH on PURTH.TH011 = PURTD.TD001 and PURTH.TH012=PURTD.TD002 and PURTH.TH013=PURTD.TD003 " &
            " left join PURTC on PURTC.TC001 = PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
            " left join PURTG on PURTG.TG001 = PURTH.TH001 and PURTG.TG002 = PURTH.TH002 " &
            " left join CMSMC on CMSMC.MC001 = PURTH.TH009 " &
            " left join CMSMV on CMSMV.MV001 = PURTC.TC011 " &
            " left join PURTB on PURTB.TB001 = PURTD.TD026 and PURTB.TB002 = PURTD.TD027 and PURTB.TB003 = PURTD.TD028 " &
            " left join LRPTC on LRPTC.TC001 = TB057 and LRPTC.TC002 = TB004 and LRPTC.TC046 = TB058 " &
            " left join COPTC on Rtrim(PURTD.TD013) <> '' and COPTC.TC001 = PURTD.TD013 and COPTC.TC002 =PURTD.TD021 or Rtrim(PURTD.TD013) = '' and COPTC.TC001 = LRPTC.TC026 and COPTC.TC002 =LRPTC.TC027" &
            " where 1=1 " & WHR & " order by substring(TG003,1,4)+'-'+substring(TG003,5,2)+'-'+substring(TG003,7,2),PURTH.TH001+'-'+PURTH.TH002+'-'+PURTH.TH003"

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("HistoricalReceipt" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub


End Class