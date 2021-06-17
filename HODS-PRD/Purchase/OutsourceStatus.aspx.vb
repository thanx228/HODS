Public Class OutsourceStatus
    Inherits System.Web.UI.Page
    Dim configDate As New ConfigDate
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim outsTrnType As String = "'D203','D204','D205','D206','D209'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 in(" & outsTrnType & ") order by MQ001"
            ControlForm.showCheckboxList(cblTrnType, SQL, "MQ002", "MQ001", 3, Conn_SQL.ERP_ConnectionString)
            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showCheckboxList(cblmoType, SQL, "MQ002", "MQ001", 7, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShow", "gridviewScrollgvShow();", True)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = ""
        Dim WHR As String = ""
        Dim FLD As String = ""

        WHR = Conn_SQL.Where("TC.TC004", cblmoType)
        WHR = WHR & Conn_SQL.Where("TC.TC005", tbMoNo)
        WHR = WHR & Conn_SQL.Where("TA.TA006", tbItem)
        WHR = WHR & Conn_SQL.Where("TA.TA035", tbSpec)
        WHR = WHR & Conn_SQL.Where("TC.TC001", cblTrnType)

        Dim sup As String = tbSup.Text.ToString
        If sup <> "" Then
            WHR = WHR & " and (TB.TB005='" & sup & "' or TB.TB008='" & sup & "') "
        Else
            WHR = WHR & " and (TB.TB004='2' or TB.TB007='2') "
        End If
        Dim status As String = ddlStatus.Text.ToString
        If status <> "A" Then
            WHR = WHR & " and TB.TB013='" & status & "'  "
        End If

        WHR = WHR & configDate.DateWhere("TB.TB003", configDate.dateFormat2(tbDateFrom.Text.ToString), configDate.dateFormat2(tbDateTo.Text.ToString))
        FLD = FLD & "case LEN(TA.TA006) when '16' then Substring (TA.TA006,1,14)+'-'+ SUBSTRING (TA.TA006,15,2) else TA.TA006  end as 'Item'," 'Part
        FLD = FLD & "TA.TA035 as 'Spec'," 'Spec
        FLD = FLD & "TA.TA034 as 'Desc'," 'Desc
        FLD = FLD & "TC.UDF01 as 'Desc/Spec'," 'Desc/Spec
        FLD = FLD & "TA.TA001+'-'+TA.TA002 as 'MO Detail.'," 'MO Detail
        FLD = FLD & "cast(TA.TA015 as decimal(15,2)) as 'MO Qty'," 'MO Qty
        FLD = FLD & "TC.TC001+'-'+TC.TC002+'-'+TC.TC003 as 'Transfer Detail'," 'Transfer Detail
        FLD = FLD & "(SUBSTRING(TB.TB003,7,2)+'-'+SUBSTRING(TB.TB003,5,2)+'-'+SUBSTRING(TB.TB003,1,4)) as 'Transfer Date'," 'Transfer Date
        FLD = FLD & "cast(TC.TC036 as decimal(15,2)) as 'Transfer Qty'," 'Transfer Qty
        '-----FLD = FLD & "" 'WIP
        'FLD = FLD & "TC.TC009+'-'+MT.MW002 as 'Transfer To'," 'Transfer to
        FLD = FLD & "TC.TC009 as 'Operation To'," 'Outs to Location
        FLD = FLD & "TB.TB008 as 'Transfer To'," 'Outs to Location

        'FLD = FLD & "TC.TC007+'-'+MF.MW002 as 'Transfer From'," 'Transfer From
        FLD = FLD & "TC.TC007 as 'Operation From'," 'Outs to Location
        FLD = FLD & "TB.TB005 as 'Transfer From'," 'Outs to Location
        '-----FLD = FLD & "" 'Transfer From Descreiption
        '-----FLD = FLD & "" 'Transfer to Descreiption
        FLD = FLD & "TB.TB013 as 'Transfer Status' " 'Transfer Status
        SQL = " select " & FLD & " from SFCTC TC " &
              " left join SFCTB TB on TB.TB001=TC.TC001 and TB.TB002=TC.TC002 " &
              " left join MOCTA TA on TA.TA001=TC.TC004 and TA.TA002=TC.TC005 " &
              " left join CMSMW MT on MT.MW001=TC.TC009 " &
              " left join CMSMW MF on MF.MW001=TC.TC007 " &
              " where TC.TC001 in (" & outsTrnType & ") " & WHR &
              " order by TC.TC001,TC.TC002,TC.TC003 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)

        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("OutsourceStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub


End Class