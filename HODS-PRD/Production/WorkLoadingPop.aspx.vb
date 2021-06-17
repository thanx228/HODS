Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class WorkLoadingPop
    Inherits System.Web.UI.Page

    'Dim Conn_SQL As New ConnSQL
    'Dim ControlForm As New ControlDataForm
    Dim dbConn As New DataConnectControl
    Dim gvCont As New GridviewControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim wc As String = Request.QueryString("wc").ToString.Trim,
                fdate As String = Request.QueryString("fdate").ToString.Trim,
                tdate As String = Request.QueryString("tdate").ToString.Trim,
                fdtae As String = Request.QueryString("fdate").ToString.Trim,
                cust As String = Request.QueryString("cust").ToString.Trim,
                mcon As String = Request.QueryString("mcon").ToString.Trim,
                motype As String = Request.QueryString("motype").ToString.Trim.Replace(",", "','")

            Dim SQL As String = "",
                WHR As String = ""

            If cust <> "" Then
                WHR &= dbConn.WHERE_EQUAL("TC004", cust.Trim)

            End If
            WHR &= dbConn.WHERE_BETWEEN("SFC.TA009", fdate, tdate)
            'Dim con As String = ddlCon.Text.Trim
            If mcon = "2" Then 'on process
                WHR &= " and SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058>0 "
            ElseIf mcon = "3" Then 'on pending
                WHR &= " and SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058=0 "
            End If
            If motype <> "" Then
                WHR &= " and SFC.TA001 in('" & motype & "') "
            End If

            SQL = " select MD002 F01,MW002 F011,MA002 F02, " &
                  " case when MOC.TA026=''  then '' else MOC.TA026+'-'+MOC.TA027+'-'+MOC.TA028 end F021," &
                  " SFC.TA001+'-'+SFC.TA002+'-'+SFC.TA003 F03,MOC.TA035 F04,MOC.TA015 F05, " &
                  " SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058 F06, " &
                  " SFC.TA010 F061,SFC.TA012 F062,SFC.TA012 F063," &
                  " case when MOC.TA015=0 then 0 else  ceiling(SFC.TA022/MOC.TA015) end F07, " &
                  " (SUBSTRING(SFC.TA009,7,2)+'-'+SUBSTRING(SFC.TA009,5,2)+'-'+SUBSTRING(SFC.TA009,1,4)) F08," &
                  " isnull((select top 1 (SUBSTRING(PS.PlanDate,7,2)+'-'+SUBSTRING(PS.PlanDate,5,2)+'-'+SUBSTRING(PS.PlanDate,1,4)) from " & VarIni.DBMIS & ".dbo.PlanSchedule PS where PS.MoType=SFC.TA001 and PS.MoNo=SFC.TA002 and PS.MoSeq=SFC.TA003 and PlanStatus='P' order by PS.PlanDate desc),'') F09, " &
                  " case when SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058=0 then 'Pending' else 'On Process' end F10 " &
                  " from SFCTA SFC " &
                  " left join MOCTA MOC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
                  " left join CMSMD on MD001=SFC.TA006 left join COPTC on TC001=MOC.TA026 and TC002=MOC.TA027 " &
                  " left join COPMA on MA001=TC004 " &
                  " left join CMSMW on MW001=SFC.TA004 " &
                  " where SFC.TA032 = 'N' and (MOC.TA011 not in ('y','Y')) and SFC.TA005 = '1' " &
                  " and SFC.TA006='" & wc & "' " & WHR &
                  " order by SFC.TA009,SFC.TA001,SFC.TA002,SFC.TA003 "
            gvCont.ShowGridView(gvShow, SQL, VarIni.ERP)
            lbCountPR.Text = gvCont.rowGridview(gvShow)
        End If
    End Sub
End Class