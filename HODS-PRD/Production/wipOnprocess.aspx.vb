Public Class wipOnprocess
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            ControlForm.showCheckboxList(cblWc, SQL, "MD002", "MD001", 4, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False

        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = "",
            sSQL As String = "",
            sSQL1 As String = "",
            sSQL2 As String = ""
        sSQL = "(select top 1 B1.TB003 from SFCTC C1 left join SFCTB B1 on B1.TB001=C1.TC001 and B1.TB002=C1.TC002 where C1.TC004=S.TA001 and C1.TC005=S.TA002 and C1.TC008=S.TA003 order by B1.TB003 desc )"

        WHR = WHR & Conn_SQL.Where("S.TA006", cblWc)
        WHR = WHR & Conn_SQL.Where("C.TC004", tbCust)
        WHR = WHR & Conn_SQL.Where("M.TA006", tbItem)
        WHR = WHR & Conn_SQL.Where("M.TA035", tbSpec)
        WHR = WHR & Conn_SQL.Where(sSQL, configDate.dateFormat2(tbDateFM.Text.Trim), configDate.dateFormat2(tbDateTO.Text.Trim))

        sSQL2 = "(select top 1 B2.TB003 from SFCTC C2 left join SFCTB B2 on B2.TB001=C2.TC001 and B2.TB002=C2.TC002 where C2.TC004=S.TA001 and C2.TC005=S.TA002 and C2.TC008=S.TA003 order by B2.TB003 desc )"
        sSQL1 = "(select top 1 PlanDate from DBMIS..PlanSchedule P where P.TA001=S.TA001 and P.TA002=S.TA002 and P.TA003=S.TA003 order by  PlanDate desc)"
        SQL = " select MW002,MA002,case when M.TA026='' then '' else M.TA026+'-'+M.TA027+'-'+M.TA028 end TA026, " & _
              " S.TA001+'-'+S.TA002 TA001,M.TA035 ,M.TA015,S.TA010," & _
              " S.TA010+S.TA013+S.TA016-S.TA011-S.TA012-S.TA014-S.TA015-S.TA048-S.TA056-S.TA058 wip," & _
              " S.TA017,S.TA008 ," & sSQL2 & " F01, " & sSQL1 & " F02 ,datediff(day,GETDATE(),cast( " & sSQL & " as date)) F03," & _
              "  convert(varchar(10), getdate(),121) F04,S.TA034 " & _
              " from SFCTA S left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
              " left join COPTC C on C.TC001=M.TA026 and C.TC002=M.TA027 " & _
              " left join CMSMW on MW001=S.TA004 " & _
              " left join COPMA on MA001=C.TC004 " & _
              " where M.TA011 not in ('Y','y') and S.TA010+S.TA013+S.TA016-S.TA011-S.TA012-S.TA014-S.TA015-S.TA048-S.TA056-S.TA058 > 0 " & WHR & _
              " order by F03,TA001"
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("workLoading" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class