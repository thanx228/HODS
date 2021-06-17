Public Class SaleOrderPopUp
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not Page.IsPostBack() Then

            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
            lbItem.Text = jpPart


            Dim SoType As String = Request.QueryString("sodetail").ToString.Trim
            lbSOde.Text = SoType

            Dim SoNo As String = Request.QueryString("soNo").ToString.Trim
            lbsoNo.Text = SoNo

            Dim SoSeq As String = Request.QueryString("soSeq").ToString.Trim
            lbsoSeq.Text = SoSeq


            lbDesc.Text = Request.QueryString("JPDesc").ToString.Trim
            lbSpec.Text = Request.QueryString("JPSpec").ToString.Trim
            lbCus.Text = Request.QueryString("Cus").ToString.Trim
            'lbSOde.Text = Request.QueryString("SOdetail").ToString.Trim
            lbOrdate.Text = Request.QueryString("Ordate").ToString.Trim
            lbDeldate.Text = Request.QueryString("Deldate").ToString.Trim
            lbSO.Text = Request.QueryString("soQty").ToString.Trim
            lbMO.Text = Request.QueryString("moQty").ToString.Trim
            'lbsoNo.Text = Request.QueryString("soNo").ToString.Trim
            'lbsoSeq.Text = Request.QueryString("soSeq").ToString.Trim

            Dim whr As String = "",
                SQL As String = ""




            'Sale order Detail
            SQL = " select COPTC.TC004 as 'SO1',COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO2', " & _
                  " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as 'SO3'," & _
                  " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'SO4', " & _
                  " COPTD.TD008 as 'SO5',COPTD.TD009 as 'SO6'," & _
                  " COPTD.TD008-COPTD.TD009 as 'SO7',COPTD.TD010 as 'SO8' from COPTD  " & _
                  " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                  " where COPTD.TD004 = '" & jpPart & "' and COPTC.TC001 = '" & SoType & "' " & _
                  " and COPTC.TC002 = '" & SoNo & "' and COPTD.TD003 = '" & SoSeq & "' " & _
                  "order by COPTD.TD001,COPTD.TD002,COPTD.TD003 "

            ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountSO.Text = ControlForm.rowGridview(gvSO)



            'MO Detail
            SQL = " select MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'MO1'," & _
                  " MOCTA.TA001+'-'+MOCTA.TA002 as 'MO2'," & _
                  " (SUBSTRING(MOCTA.TA040,7,2)+'-'+SUBSTRING(MOCTA.TA040,5,2)+'-'+SUBSTRING(MOCTA.TA040,1,4)) as 'MO3'," & _
                  " (SUBSTRING(MOCTA.TA010,7,2)+'-'+SUBSTRING(MOCTA.TA010,5,2)+'-'+SUBSTRING(MOCTA.TA010,1,4)) as 'MO4'," & _
                  " MOCTA.TA015 as 'MO5',MOCTA.TA017 as 'MO6',MOCTA.TA015-MOCTA.TA017 as 'MO7' " & _
                  " from MOCTA where MOCTA.TA006='" & jpPart & "' and MOCTA.TA026 = '" & SoType & "' " & _
                  " and MOCTA.TA027 = '" & SoNo & "' and MOCTA.TA028 = '" & SoSeq & "' " & _
                  " order by MOCTA.TA001,MOCTA.TA002 "

            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = ControlForm.rowGridview(gvMO)



        End If

    End Sub

End Class