Public Class DeliveryPlanStatusPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack() Then

            Dim Item As String = Request.QueryString("Item").ToString.Trim
            lbItem.Text = Item
            lbSpec.Text = Request.QueryString("Spec").ToString.Trim
            lbCustID.Text = Request.QueryString("CusID").ToString.Trim
            lbCustName.Text = Request.QueryString("CusName").ToString.Trim
            lbDelv.Text = Request.QueryString("DelQty").ToString.Trim
            lbFG.Text = Request.QueryString("FGQty").ToString.Trim
            lbFGBal.Text = Request.QueryString("FGBal").ToString.Trim
            lbMO.Text = Request.QueryString("MO").ToString.Trim

            Dim WHRDN As String = ""
            Dim WHR As String = ""

            Dim CusCode As String = Request.QueryString("CusCode").ToString.Trim
            Dim SONo As String = Request.QueryString("SONo").ToString.Trim
            Dim SOSeq As String = Request.QueryString("SOSeq").ToString.Trim
            Dim Spec As String = Request.QueryString("Spec").ToString.Trim
            Dim DateFrom As String = Request.QueryString("DateFrom").ToString.Trim
            Dim DateTo As String = Request.QueryString("DateTo").ToString.Trim
            Dim SO As String = Request.QueryString("SO").ToString.Trim
            Dim DNNo As String = Request.QueryString("DNNo").ToString.Trim


            If SO <> "" Then
                WHR = WHR & " and MOCTA.TA026 in ('" & SO & "') "
            End If

            If SONo <> "" Then
                WHR = WHR & " and MOCTA.TA027 like '" & SONo & "%' "
            End If

            If SOSeq <> "" Then
                WHR = WHR & " and MOCTA.TA028 like '" & SOSeq & "%' "
            End If

            'If Item <> "" Then
            '    WHR = WHR & " and MOCTA.TA006 like '" & Item & "%' "
            'End If

            'If CusCode <> "" Then
            '    WHR = WHR & " and COPTC.TC004 like '" & CusCode & "%' "
            'End If

            If Spec <> "" Then
                WHR = WHR & " and MOCTA.TA035 like '" & Spec & "%' "
            End If

            'If DateFrom <> "" Then
            '    WHR = WHR & Conn_SQL.Where("SFCTA.TA008", ConfigDate.dateFormat2(DateFrom), ConfigDate.dateFormat2(DateTo))
            'End If

            'If DateTo <> "" Then
            '    WHR = WHR & Conn_SQL.Where("SFCTA.TA008", ConfigDate.dateFormat2(DateFrom), ConfigDate.dateFormat2(DateTo))
            'End If

            Dim SQL As String = ""
            Dim SQLMO As String = ""

            SQL = "select COPMA.MA002 as 'Cus. Name' , COPMA.MA001 as 'Cus ID', " & _
                " COPTG.TG001 +'-'+COPTG.TG002  as 'DN NO' , " & _
                " COPTH.TH014+'-'+COPTH.TH015+'-'+COPTH.TH016 as 'SO-Req.'," & _
                " (SUBSTRING ( COPTH.TH004,1,14)+'-'+SUBSTRING (COPTH.TH004,15,2)) as 'Item', " & _
                " COPTH.TH006 as 'Spec', convert(int,COPTH.TH008) as 'Delv. Qty',convert(int,INVMC.MC007) as 'FG'," & _
                " (SUBSTRING (COPTG.TG042,7,2)+'-'+SUBSTRING (COPTG.TG042,5,2)+'-'+SUBSTRING (COPTG.TG042,1,4)) as 'Delv Date'" & _
                " from COPTH " & _
                " left join INVMB on INVMB.MB001 = COPTH.TH004 and INVMB.MB003 = COPTH.TH006      " & _
                " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017      " & _
                " left join COPTG on COPTG.TG001 = COPTH.TH001 and COPTG.TG002 = COPTH.TH002  " & _
                " left join COPMA on COPMA.MA001 = COPTG.TG004" & _
                " where COPTG.TG023 = 'N' and COPTH.TH004 like  '" & Item & "%'"
            ControlForm.ShowGridView(gvDN, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountDN.Text = ControlForm.rowGridview(gvDN)

            gvDN.DataBind()


            SQL = " select COPMA.MA002 as 'Cus.Name' , COPMA.MA001 as 'Cus ID' , " & _
                " MOCTA.TA001 +'-'+MOCTA.TA002 as 'MO No', " & _
                " MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 as 'SO-seq'," & _
                " (SUBSTRING (MOCTA.TA006,1,14)+'-'+SUBSTRING (MOCTA.TA006,15,2)) as 'Item'," & _
                " MOCTA.TA035 as 'Spec.' ,CONVERT(int, MOCTA.TA015) as 'MO Qty' ," & _
                " CONVERT(int, MOCTA.TA017 )as 'Fins.'," & _
                " convert(int,(MOCTA.TA015 - MOCTA.TA017)) as 'Bal.', " & _
                " (SUBSTRING (MOCTA.TA009,7,2)+'-'+SUBSTRING (MOCTA.TA009,5,2)+'-'+SUBSTRING (MOCTA.TA009,1,4)) as 'Start Date'," & _
                " (SUBSTRING (MOCTA.TA010,7,2)+'-'+SUBSTRING (MOCTA.TA010,5,2)+'-'+SUBSTRING (MOCTA.TA010,1,4)) as 'Fins. Dae'" & _
                " from MOCTA " & _
                " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027" & _
                " left join COPMA on COPMA.MA001 = COPTC.TC004 " & _
                " where MOCTA.TA013 = 'Y' and MOCTA.TA011 in ('1','2','3') and MOCTA.TA006 like '" & Item & "%'" & WHR

            ControlForm.ShowGridView(gvMO, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = ControlForm.rowGridview(gvMO)

        End If
    End Sub

    'Private Sub gvDN_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)



    '    '*** CustomerID ***'
    '    Dim lblCustomerID As Label = CType(e.Row.FindControl("lblCustomerID"), Label)
    '    If Not IsNothing(lblCustomerID) Then
    '        lblCustomerID.Text = e.Row.DataItem("Cus ID")
    '    End If

    '    '*** Email ***'
    '    Dim lblCustomerName As Label = CType(e.Row.FindControl("lblCustomerName"), Label)
    '    If Not IsNothing(lblCustomerName) Then
    '        lblCustomerName.Text = e.Row.DataItem("Cus. Name")
    '    End If

    '    '*** Name ***'
    '    Dim lblItem As Label = CType(e.Row.FindControl("lblItem"), Label)
    '    If Not IsNothing(lblItem) Then
    '        lblItem.Text = e.Row.DataItem("Item")
    '    End If

    '    '*** CountryCode ***'
    '    Dim lblSpec As Label = CType(e.Row.FindControl("lblSpec"), Label)
    '    If Not IsNothing(lblSpec) Then
    '        lblSpec.Text = e.Row.DataItem("Spec")
    '    End If

    '    '*** Budget ***'
    '    Dim lblDelvQty As Label = CType(e.Row.FindControl("lblDelvQty"), Label)
    '    If Not IsNothing(lblDelvQty) Then
    '        lblDelvQty.Text = FormatNumber(e.Row.DataItem("DelQty"), 2)
    '    End If

    '    '*** Used ***'
    '    Dim lblFG As Label = CType(e.Row.FindControl("lblFG"), Label)
    '    If Not IsNothing(lblFG) Then
    '        lblFG.Text = FormatNumber(e.Row.DataItem("FGQty"), 2)
    '    End If


    '    Dim sumTotal As Integer

    '    sumTotal = sumTotal - e.Row.DataItem("FGQty")


    '    '*** Total ***'
    '    Dim lblFGBal As Label = CType(e.Row.FindControl("lblFGBal"), Label)
    '    If Not IsNothing(lblFGBal) Then
    '        lblFGBal.Text = FormatNumber(sumTotal, 2)
    '    End If


    'End Sub

End Class