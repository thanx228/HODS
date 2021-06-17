Public Class WorkCenterSummary
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            ControlForm.showCheckboxList(cblWC, SQL, "MD002", "MD001", 6, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showCheckboxList(cblMOType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            btExport.Visible = False

        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim SQL As String = "",
            WHR As String = "",
        CusCode As String = txtCusCode.Text.Trim,
           Item As String = txtItem.Text.Trim,
           Spec As String = txtSpec.Text.Trim,
           SONo As String = txtSONo.Text.Trim,
           SOSeq As String = txtSOSeq.Text.Trim,
           MONo As String = txtMONo.Text.Trim,
      DateFrom As String = txtDateFrom.Text.Trim,
      DateTo As String = txtDateTo.Text

        WHR = WHR & Conn_SQL.Where("MOCTA.TA026", cblSaleType)
        WHR = WHR & Conn_SQL.Where("MOCTA.TA001", cblMOType)
        WHR = WHR & Conn_SQL.Where("SFCTA.TA006", cblWC)

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

        Dim ordType As String = "",
            ordTypeSO As String = "",
            ordTypeMO As String = "",
            cnt As Integer = 0,
            cntSO As Integer = 0,
            cntMO As Integer = 0

        For Each boxItem As ListItem In cblSaleType.Items
            If boxItem.Selected = True Then
                ordTypeSO = ordTypeSO & "'" & CStr(boxItem.Value) & "',"
                cntSO = cntSO + 1
            End If
        Next
        If cntSO > 0 Then
            ordTypeSO = ordTypeSO.Substring(0, ordTypeSO.Length - 1)
        End If

        For Each boxItem As ListItem In cblMOType.Items
            If boxItem.Selected = True Then
                ordTypeMO = ordTypeMO & "'" & CStr(boxItem.Value) & "',"
                cntMO = cntMO + 1
            End If
        Next
        If cntMO > 0 Then
            ordTypeMO = ordTypeMO.Substring(0, ordTypeMO.Length - 1)
        End If

        For Each boxItem As ListItem In cblWC.Items
            If boxItem.Selected = True Then
                ordType = ordType & "'" & CStr(boxItem.Value) & "',"
                cnt = cnt + 1
            End If
        Next
        If cnt > 0 Then
            ordType = ordType.Substring(0, ordType.Length - 1)
        End If

        SQL = " select SFCTA.TA007 as 'W01', " & _
              " COUNT(SFCTA.TA007) as 'W02', " & _
              " SUM(case  when SFCTA.TA011>=MOCTA.TA015 then 1 else 0 end )as 'W03'," & _
              " SUM(case  when (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)> 0 and SFCTA.TA011<MOCTA.TA015 then 1 else 0 end )as 'W04', " & _
              " SUM(case  when (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)= 0 and SFCTA.TA011<MOCTA.TA015 then 1 else 0 end )as 'W05'," & _
              " convert(varchar,(convert(decimal(10,2),(convert(decimal(10,2),(sum(case  when MOCTA.TA015<=SFCTA.TA011 then 1 else 0 end ))*100)/ convert(decimal(10,2),COUNT( SFCTA.TA007))))))+'%' as 'W06'" & _
              " from SFCTA " & _
              " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
              " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " & _
              " where SFCTA.TA005 = '1' " & WHR & _
              " group by SFCTA.TA007 order by SFCTA.TA007 "


        ' " convert(varchar,(SUM(case  when MOCTA.TA015<=SFCTA.TA011 then 1 else 0 end )*100)/COUNT( SFCTA.TA007))+'%' as 'W06'" & _



        'SQL = " select SFCTA.TA007 as 'W01', " & _
        '     " COUNT(SFCTA.TA007) as 'W02', " & _
        '     " SUM(case  when SFCTA.TA011>=MOCTA.TA015 then 1 else 0 end )as 'W03'," & _
        '     " SUM(case  when (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)> 0 then 1 else 0 end )as 'W04', " & _
        '     " SUM(case  when (SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)= 0 then 1 else 0 end )as 'W05'," & _
        '     " (SUM(case  when MOCTA.TA015<=SFCTA.TA011 then 1 else 0 end )*100)/COUNT( SFCTA.TA007) as 'W06'" & _
        '     " from SFCTA " & _
        '     " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
        '     " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 " & _
        '     " where MOCTA.TA011 not in ('y','Y') and SFCTA.TA005 = '1' " & WHR & _
        '     " group by SFCTA.TA007 order by SFCTA.TA007 "




        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("W01")) Then
                    Dim link As String = ""
                    Dim CusCode As String = txtCusCode.Text.Trim,
                       Item As String = txtItem.Text.Trim,
                        Spec As String = txtSpec.Text.Trim,
                      SONo As String = txtSONo.Text.Trim,
                     SOSeq As String = txtSOSeq.Text.Trim,
                      MONo As String = txtMONo.Text.Trim,
                   DateFrom As String = txtDateFrom.Text.Trim,
                    DateTo As String = txtDateTo.Text,
                    MO As String = cblMOType.Text.Trim,
                    SO As String = cblSaleType.Text

                    Dim WC As String = .DataItem("W01")
                    link = link & "&WC= " & WC
                    link = link & "&ProTotal= " & .DataItem("W02")
                    link = link & "&ProFinish= " & .DataItem("W03")
                    link = link & "&OnHand= " & .DataItem("W04")
                    link = link & "&Pending= " & .DataItem("W05")
                    link = link & "&Finish= " & .DataItem("W06")
                    link = link & "&SONo= " & SONo
                    link = link & "&SOSeq= " & SOSeq
                    link = link & "&CusCode= " & CusCode
                    link = link & "&MONo= " & MONo
                    link = link & "&Item= " & Item
                    link = link & "&Spec= " & Spec
                    link = link & "&DateFrom= " & DateFrom
                    link = link & "&DateTo= " & DateTo
                    link = link & "&SO= " & SO
                    link = link & "&MO= " & MO

                    hplDetail.NavigateUrl = "WorkCenterSummaryPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", WC)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("WorkCenterSummery" & Session("UserName"), gvShow)
    End Sub

End Class