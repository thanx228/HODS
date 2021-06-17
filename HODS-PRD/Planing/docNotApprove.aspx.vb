Imports System.Globalization

Public Class docNotApprove
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim listData = New Hashtable
            'listData.Add("D3", " MO Recieve ")
            'listData.Add("D2", " Transfer ")
            'listData.Add("D1", " Production Input ")
            'ddlType.DataSource = listData
            'ddlType.DataValueField = "Key"
            'ddlType.DataTextField = "Value"
            'ddlType.DataBind()
            'listData.Clear()

            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click

        Dim rType As String = ddlType.Text, SQL As String = ""
        Dim date1 As String = tbEndDate.Text,endDate As String

        'End date
        If date1 <> "" Then
            endDate = configDate.dateFormat(date1)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        If rType = "D4" Then 'Materials Issue count(TC005)
            SQL = "select TC005 as LOCATION,isnull(MD002,'unknow') as 'LOCATION NAME',sum(case when TC009='N' then 1 else 0 end ) as 'Not Aprove',  " & _
                  " sum(case when TC009='U' then 1 else 0 end ) as 'Aprove Fail' from MOCTC " & _
                  " left join CMSMD on MD001=TC005 " & _
                  " where TC001 in (select MQ001 from CMSMQ where MQ003='54') and TC009 in ('N','U') " & _
                  " and TC003<='" & endDate & "' " & _
                  " group by TC005,MD002 order by TC005 "
        ElseIf rType = "D5" Then 'inventory transaction order count(TA004)
            SQL = " select TA004 as LOCATION,isnull(ME002,'unknow') as 'LOCATION NAME',sum(case when TA004='N' then 1 else 0 end) as 'Not Aprove', " & _
                  " sum(case when TA004='U' then 1 else 0 end) as 'Aprove Fail' from INVTA " & _
                  " left join CMSME on ME001=TA004 " & _
                  " where TA001='1105' AND TA006 in ('N','U') and TA003<='" & endDate & "' " & _
                  " group by  TA004,ME002 order by TA004"
        ElseIf rType = "D6" Then
            SQL = "select TA014 as LOCATION,'Not Aprove' as 'LOCATION NAME' ,sum(case when TA014='N' then 1 else 0 end)as 'Not Aprove',sum(case when TA014='U' then 1 else 0 end)as 'Aproved Fail' from QMSTA where TA014 in ('N','U') group by TA014"
        ElseIf rType = "D7" Then
            SQL = "select TG014 as LOCATION,'Not Aprove' as 'LOCATION NAME' ,sum(case when TG014='N' then 1 else 0 end)as 'Not Aprove',sum(case when TG014='U' then 1 else 0 end)as 'Aproved Fail' from QMSTG where TG014 in ('N','U') group by TG014"
        ElseIf rType = "D8" Then
            SQL = "select TD014 as LOCATION,'Not Aprove' as 'LOCATION NAME' ,sum(case when TD014='N' then 1 else 0 end)as 'Not Aprove',sum(case when TD014='U' then 1 else 0 end)as 'Aproved Fail' from QMSTD where TD014 in ('N','U') group by TD014"
        ElseIf rType = "D9" Then
            SQL = "select TJ014 as LOCATION,'Not Aprove' as 'LOCATION NAME' ,sum(case when TJ014='N' then 1 else 0 end)as 'Not Aprove',sum(case when TJ014='U' then 1 else 0 end)as 'Aproved Fail' from QMSTJ where TJ014 in ('N','U') group by TJ014"
        ElseIf rType = "D10" Then
            SQL = "select TM014 as LOCATION,'Not Aprove' as 'LOCATION NAME' ,sum(case when TM014='N' then 1 else 0 end)as 'Not Aprove',sum(case when TM014='U' then 1 else 0 end)as 'Aproved Fail' from QMSTM where TM014 in ('N','U') group by TM014"
        ElseIf rType = "D11" Then 'Sale Delivery
            SQL = "select TG004 as LOCATION,'Not Aprove' as 'LOCATION NAME',sum(case when TG023='N' then 1 else 0 end)as 'Not Aprove',sum(case when TG023='U' then 1 else 0 end)as 'Aproved Fail' from COPTG where TG023 in ('N','U') and TG042<='" & endDate & "'  group by TG004"
        ElseIf rType = "D12" Then 'Purchase Receipt
            SQL = "select TG005 as LOCATION,'Not Aprove' as 'LOCATION NAME' ,sum(case when TG013='N' then 1 else 0 end)as 'Not Aprove',sum(case when TG013='U' then 1 else 0 end)as 'Aproved Fail' from PURTG where TG013 in ('N','U') and TG014>='20130101' and TG014<='" & endDate & "' group by TG005"
        ElseIf rType = "D13" Then 'Loan/Borrow Order
            SQL = "select TF005 as LOCATION,'Not Aprove' as 'LOCATION NAME' ,sum(case when TF024='N' then 1 else 0 end)as 'Not Aprove',sum(case when TF024='U' then 1 else 0 end)as 'Aproved Fail' from INVTF where TF020 in ('N','U') and TF024<='" & endDate & "' group by TF005"
        ElseIf rType = "D14" Then 'Loan/Borrow Return
            SQL = " select TH005 as LOCATION,'Not Aprove' as 'LOCATION NAME' ,sum(case when TH020='N' then 1 else 0 end)as 'Not Aprove',sum(case when TH020='U' then 1 else 0 end)as 'Aproved Fail' from INVTH " &
                  " where TH020 in ('N','U') and TH023<='" & endDate & "' group by TH005"
        ElseIf rType = "D15" Then 'Fix Asset PR
            SQL = " select TI006 as LOCATION,ME002 as 'LOCATION NAME' ,sum(case when TI010='N' then 1 else 0 end)as 'Not Aprove',sum(case when TI010='U' then 1 else 0 end)as 'Aproved Fail' from ASTTI " & _
                  " left join CMSME on ME001=TI006" & _
                  " where TI010 in ('N','U') and TI004<='" & endDate & "'  group by TI006,ME002 order by TI006,ME002"
        Else
            SQL = " select TB008 as LOCATION,TB009 as 'LOCATION NAME' ,sum(case when TB013='N' then 1 else 0 end)as 'Not Aprove',sum(case when TB013='U' then 1 else 0 end)as 'Aproved Fail' " & _
                  " from SFCTB where TB013 in ('N','U') and TB001 like '" & rType & "%' and TB003<='" & endDate & "'" & _
                  " group by TB008,TB009 order by TB008 "
            'QMSTG()
        End If

        'If rTye = "D3" Then
        '    SQL = " select TB005 as LOCATION,TB006 as LOCATION_NAME ,COUNT(TB005) as NotAprove " & _
        '          " from SFCTB where TB013 = 'N' and TB001 like '" & rTye & "%' and TB003<='" & endDate & "'" & _
        '          "  group by TB005,TB006 order by TB005"
        'Else

        'End If
        ControlForm.ShowGridView(GridView1, SQL, Conn_SQL.ERP_ConnectionString)
        lbCnt.Text = GridView1.Rows.Count
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
            If Not IsNothing(hplDetail) Then
                Dim date1 As String = tbEndDate.Text, endDate As String
                If date1 <> "" Then
                    endDate = configDate.dateFormat(date1)
                Else
                    endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
                End If
                hplDetail.NavigateUrl = "docNotApprovePop.aspx?height=150&width=350&rcvLocation=" & e.Row.DataItem("LOCATION") & _
                                        "&rType=" & ddlType.Text & "&locationName=" & e.Row.DataItem("LOCATION NAME") & _
                                        "&endDate=" & endDate
                hplDetail.Attributes.Add("title", e.Row.DataItem("LOCATION NAME").ToString.TrimEnd)
            End If
        End If
    End Sub
End Class