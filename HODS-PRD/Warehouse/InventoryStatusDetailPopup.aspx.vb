Public Class InvenStatusDetailPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not Page.IsPostBack() Then

            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
            lbItem.Text = jpPart

            ''Dim CusCode As String = Request.QueryString("CusCode").ToString.Trim
            ''lbCusID.Text = CusCode

            Dim jpSpec As String = Request.QueryString("JPSpec").ToString.Trim
            lbSpec.Text = jpSpec

            lbSOUn.Text = Request.QueryString("SOUndel").ToString.Trim
            lb2101.Text = Request.QueryString("WH2101").ToString.Trim
            'lbBin.Text = Request.QueryString("Bin").ToString.Trim
            'lb2301.Text = Request.QueryString("WH2301").ToString.Trim
            'lb2600.Text = Request.QueryString("WH2600").ToString.Trim
            'lbCusID.Text = Request.QueryString("CusCode").ToString.Trim


            Dim whr As String = "",
                SQL As String = ""


            SQL = " select COPTC.TC001+'-'+COPTC.TC002 as 'S01', COPTD.TD003 as 'S02',INVMB.MB001 as IN01, INVMB.MB003 as IN02,(COPTD.TD008-COPTD.TD009) as IN03, " & _
          " INVMC.MC002 as IN05,INVMC.MC015 as IN06, " & _
          " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'IN07'," & _
          " COPTC.TC004 as IN08, COPMA.MA002 as IN09 " & _
          " from INVMB " & _
          " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017 " & _
          " left join JINPAO80.dbo.COPMG COPMG on COPMG.MG002 = INVMB.MB001 and COPMG.MG004 = INVMB.MB002 " & _
          " left join JINPAO80.dbo.COPMA COPMA on COPMA.MA001 = COPMG.MG001 " & _
          " left join JINPAO80.dbo.COPTD COPTD on COPTD.TD004 = INVMB.MB001 and COPTD.TD005 = INVMB.MB002 and COPTD.TD006=INVMB.MB003" & _
          " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 and COPTC.TC004=COPMG.MG001" & _
          " where COPTC.TC027 = 'Y' and COPTD.TD016 not in ('y','Y') " & _
          " and INVMB.MB001 = '" & jpPart & "' and INVMB.MB003 = '" & jpSpec & "' " & _
          " order by INVMB.MB001,COPTC.TC004,COPTD.TD013"

            '          " " & configDate.DateWhere("COPTD.TD013", "", endDate) & " " & _
            ' and COPMG.MG001  = '" & CusCode & "' 

            ControlForm.ShowGridView(gvIN, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountSO.Text = ControlForm.rowGridview(gvIN)


        End If

    End Sub

    Protected Sub gvShow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIN.SelectedIndexChanged

    End Sub
End Class