Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl


Public Class RetrurnReworkScrap
    Inherits System.Web.UI.Page
    Dim dbConn As New DataConnectControl
    Dim dateCont As New DateControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim ddlCont As New DropDownListControl
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            ddlCont.showDDL(ddlWC, SQL, VarIni.ERP, "MD002", "MD001", True)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ddlCont.showDDL(ddlMoType, SQL, VarIni.ERP, "MQ002", "MQ001", True)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ddlCont.showDDL(ddlSaleType, SQL, VarIni.ERP, "MQ002", "MQ001", True)

            btExportExcel.Visible = False

        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            sSQL As String = "",
            WHR As String = "",
            FLD As String = ""
        WHR &= dbConn.WHERE_EQUAL("SFCTA.TA006", ddlWC)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA006", tbCode)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA035", tbSpec)
        WHR &= dbConn.WHERE_EQUAL("SFCTA.TA001", ddlMoType)
        WHR &= dbConn.WHERE_EQUAL("MOCTA.TA026", ddlSaleType)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA035", tbCust)
        WHR &= dbConn.Where("SFCTA.TA008", dateCont.dateFormat2(tbDateFrom.Text), dateCont.dateFormat2(tbDateTo.Text))
        'If ddlType.Text.Trim = "1" Then '1 Rework
        WHR = WHR & " and SFCTA.TA013-SFCTA.TA014>0 "
        FLD = "cast(SFCTA.TA013-SFCTA.TA014 as decimal(15,2)) as 'Rework',"
        sSQL = " isnull((select top 1 rtrim(SFCTA2.TA006)+'-'+SFCTA2.TA007 from SFCTA SFCTA2 " &
               " where SFCTA2.TA001=SFCTA.TA001 and SFCTA2.TA002=SFCTA.TA002 " &
               " and SFCTA2.TA003 <= reverse(substring(reverse('00'+ cast(CAST(substring(SFCTA.TA003,1,3) as integer)+1 as VARCHAR(4))),1,3))+'0' and SFCTA.TA003<SFCTA2.TA003 order by SFCTA2.TA003),MOCTA.TA020) as 'Next WC',"
        'Else '2 Scrap
        '    WHR = WHR & " and SFCTA.TA012 > 0 "
        '    FLD = "cast(SFCTA.TA012 as decimal(15,2)) as 'Scrap',"
        'End If
        SQL = " select rtrim(SFCTA.TA006)+'-'+SFCTA.TA007 as WC," &
              " rtrim(COPTC.TC004) as 'Customers',COPMA.MA002 as 'Customers Name'," &
              " MOCTA.TA026+'-'+MOCTA.TA027 as 'SO',MOCTA.TA028 as 'SO Seq'," &
              " SFCTA.TA001+'-'+SFCTA.TA002 as 'MO',SFCTA.TA003 as 'MO Seq'," &
              " MOCTA.TA035 as 'Item Spec', cast(MOCTA.TA015 as decimal(15,2)) as 'Plan Qty', " & FLD &
              " (SUBSTRING(SFCTA.TA008,7,2)+'/'+SUBSTRING(SFCTA.TA008,5,2)+'/'+SUBSTRING(SFCTA.TA008,1,4)) as 'Plan Start Date', " &
              sSQL & " MOCTA.TA006 as 'Item Code' " &
              " from SFCTA " &
              " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " &
              " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " &
              " left join COPMA on COPMA.MA001 = COPTC.TC004  " &
              " where MOCTA.TA013='Y' and SFCTA.TA005='1' and SFCTA.TA032 = 'N' " &
              " and MOCTA.TA011 not in('y','Y') " & WHR &
              " order by SFCTA.TA006,SFCTA.TA008,SFCTA.TA001,SFCTA.TA002,SFCTA.TA003 "

        Dim gvCont As New GridviewControl
        gvCont.ShowGridView(gvShow, SQL, VarIni.ERP, dbConn.WhoCalledMe)
        ucCountRow.RowCount = gvCont.rowGridview(gvShow)
        btExportExcel.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Function returnFld(ByVal fldName As String, ByVal fldCall As String) As String
        Return ",CONVERT(varchar, floor(" & fldName & "/60)) as " & fldCall
    End Function

    Protected Sub btExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExportExcel.Click
        Dim expCont As New ExportImportControl
        expCont.Export("WorkCenterStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class