Public Class WorkCenterSummaryPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack() Then

            Dim WC As String = Request.QueryString("WC").ToString.Trim
            lbWC.Text = WC
            lbProTotal.Text = Request.QueryString("ProTotal").ToString.Trim
            lbProFinish.Text = Request.QueryString("ProFinish").ToString.Trim
            lbOnHand.Text = Request.QueryString("OnHand").ToString.Trim
            lbPending.Text = Request.QueryString("Pending").ToString.Trim
            lbFinish.Text = Request.QueryString("Finish").ToString.Trim

            Dim WHR As String = ""
            Dim CusCode As String = Request.QueryString("CusCode").ToString.Trim
            Dim SONo As String = Request.QueryString("SONo").ToString.Trim
            Dim SOSeq As String = Request.QueryString("SOSeq").ToString.Trim
            Dim Item As String = Request.QueryString("Item").ToString.Trim
            Dim Spec As String = Request.QueryString("Spec").ToString.Trim
            Dim MONo As String = Request.QueryString("MONo").ToString.Trim
            Dim DateFrom As String = Request.QueryString("DateFrom").ToString.Trim
            Dim DateTo As String = Request.QueryString("DateTo").ToString.Trim
            Dim SO As String = Request.QueryString("SO").ToString.Trim
            Dim MO As String = Request.QueryString("MO").ToString.Trim

            If SONo <> "" Then
                WHR = WHR & " and MOCTA.TA027 like '" & SONo & "%' "
            End If

            If SOSeq <> "" Then
                WHR = WHR & " and MOCTA.TA028 like '" & SOSeq & "%' "
            End If

            If Item <> "" Then
                WHR = WHR & " and MOCTA.TA006 like '" & Item & "%' "
            End If

            If CusCode <> "" Then
                WHR = WHR & " and COPTC.TC004 like '" & CusCode & "%' "
            End If

            If Spec <> "" Then
                WHR = WHR & " and MOCTA.TA035 like '" & Spec & "%' "
            End If

            If MONo <> "" Then
                WHR = WHR & " and MOCTA.TA002 like '" & MONo & "%' "
            End If

            If DateFrom <> "" Then
                WHR = WHR & Conn_SQL.Where("SFCTA.TA008", configDate.dateFormat2(DateFrom), configDate.dateFormat2(DateTo))
            End If

            If DateTo <> "" Then
                WHR = WHR & Conn_SQL.Where("SFCTA.TA008", configDate.dateFormat2(DateFrom), configDate.dateFormat2(DateTo))
            End If

            If MO <> "" Then
                WHR = WHR & " and MOCTA.TA001 in ('" & MO & "') "
            End If

            If SO <> "" Then
                WHR = WHR & " and MOCTA.TA026 in ('" & SO & "') "
            End If

            Dim SQL As String = ""

            SQL = "select COPTC.TC004+'-'+COPMA.MA002 as 'M01', SFCTA.TA007 as 'M02', " & _
                " MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'M03'," & _
                " MOCTA.TA001+'-'+MOCTA.TA002 as 'M04', MOCTA.TA006 as 'M05',MOCTA.TA035 as 'M06'," & _
                " MOCTA.TA015 as 'M07', SFCTA.TA011 as 'M11'," & _
                 " (SUBSTRING(SFCTA.TA008,7,2)+'-'+SUBSTRING(SFCTA.TA008,5,2)+'-'+SUBSTRING(SFCTA.TA008,1,4)) as 'M10'," & _
                " (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048) as 'M08'," & _
                " (case  when (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)>0 and SFCTA.TA011<MOCTA.TA015 then 'On Hand' end )as 'M09'" & _
                " from SFCTA " & _
                " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
                " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " & _
                " left join COPMA on COPMA.MA001 = COPTC.TC004 " & _
                " where SFCTA.TA005 = '1' and SFCTA.TA007 = '" & WC & "'  " & _
                " and (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)>0 and SFCTA.TA011<MOCTA.TA015 " & WHR & _
                " order by COPTC.TC004,MOCTA.TA026,MOCTA.TA027,MOCTA.TA028 "
            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = ControlForm.rowGridview(gvMO)

            SQL = "select COPTC.TC004+'-'+COPMA.MA002 as 'M01', SFCTA.TA007 as 'M02', " & _
                " MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'M03'," & _
                " MOCTA.TA001+'-'+MOCTA.TA002 as 'M04', MOCTA.TA006 as 'M05',MOCTA.TA035 as 'M06'," & _
                " MOCTA.TA015 as 'M07', SFCTA.TA011 as 'M11'," & _
                 " (SUBSTRING(SFCTA.TA008,7,2)+'-'+SUBSTRING(SFCTA.TA008,5,2)+'-'+SUBSTRING(SFCTA.TA008,1,4)) as 'M10'," & _
                " (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048) as 'M08'," & _
                " (case  when (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)=0 and SFCTA.TA011<MOCTA.TA015 then 'Pending' end )as 'M09'" & _
                " from SFCTA " & _
                " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
                " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " & _
                " left join COPMA on COPMA.MA001 = COPTC.TC004 " & _
                " where SFCTA.TA005 = '1' and SFCTA.TA007 = '" & WC & "'  " & _
                " and (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)=0 and SFCTA.TA011<MOCTA.TA015 " & WHR & _
                " order by COPTC.TC004,MOCTA.TA026,MOCTA.TA027,MOCTA.TA028 "
            ControlForm.ShowGridView(gvMOp, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMOp.Text = ControlForm.rowGridview(gvMOp)

        End If
    End Sub

End Class