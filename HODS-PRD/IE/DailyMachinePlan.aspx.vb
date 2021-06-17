Imports System.IO

Public Class DailyMachinePlan
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 in ('W01','W02','W07','W12','W19','W23','W25','W27')
            ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 in ('5102','5104','5106','5108','5201','5202','5301','5109','5203','5194','5199') order by MQ002"
            ControlForm.showCheckboxList(cblMoType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            btExportGrid.Visible = False
            
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = ""
        Dim selProperty As String = ddlProperty.Text
        Dim wc As String = ddlWC.Text
        If selProperty = "2" Then
            wc = tbWC.Text
        End If
        Dim code As String = tbCode.Text
        Dim spec As String = tbSpec.Text
        Dim whr As String = ""

        'If wc <> "ALL" Then
        If selProperty = "1" Then '1
            whr = whr & " and SFCTA.TA006='" & wc & "' "
        Else '2
            whr = whr & " and SFCTA.TA006 like '%" & wc & "%' "
        End If
        'End If
        'If (selProperty = "1" And wc <> "ALL") Or (selProperty = "2" And wc <> "") Then
        '    whr = whr & " and SFC.TA006='" & wc & "' "
        'End If

        If code <> "" Then
            whr = whr & " and MOCTA.TA006 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and MOCTA.TA035 like '%" & spec & "%' "
        End If
        whr = whr & Conn_SQL.Where("SFCTA.TA008", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
        whr = whr & Conn_SQL.Where("SFCTA.TA001", cblMoType)
        whr = whr & Conn_SQL.Where("MOCTA.TA026", ddlSaleType)
        Dim SQL1 As String = "",
            SQL11 As String = "",
            SQL2 As String = ""

        SQL1 = " (select top 1 SFCTC.TC001+'-'+SFCTC.TC002 from SFCTC " & _
               " left join SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
               " where SFCTC.TC004=SFCTA.TA001 and SFCTC.TC005=SFCTA.TA002 and SFCTC.TC008=SFCTA.TA003) as 'Transfer No.' "

        SQL11 = " (select top 1 (SUBSTRING(SFCTB.TB003,7,2)+'/'+SUBSTRING(SFCTB.TB003,5,2)+'/'+SUBSTRING(SFCTB.TB003,1,4)) from SFCTC " & _
               " left join SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
               " where SFCTC.TC004=SFCTA.TA001 and SFCTC.TC005=SFCTA.TA002 and SFCTC.TC008=SFCTA.TA003) as 'Transfer Date.' "

        SQL2 = " (select top 1 SFCTB.TB014 from SFCTC " & _
               " left join SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
               " where SFCTC.TC004=SFCTA.TA001 and SFCTC.TC005=SFCTA.TA002 and SFCTC.TC008=SFCTA.TA003) as 'AP100 Time.' "

        SQL = " select MOCTA.TA026 as 'Sale Type',SFCTA.TA006 as 'WC',CMSMW.MW002 as 'Process',SFCTA.TA001+'-'+SFCTA.TA002+'-'+SFCTA.TA003 as MO," & _
              " MOCTA.TA006 as 'JP Part',MOCTA.TA035 as 'JP SPEC', " & _
              " MOCTB.TB003 as 'Material Item'," & _
              " MOCTB.TB012 as 'Description'," & _
              " MOCTB.TB013 as 'Material Spec', " & _
              " (SUBSTRING(SFCTA.TA008,7,2)+'/'+SUBSTRING(SFCTA.TA008,5,2)+'/'+SUBSTRING(SFCTA.TA008,1,4)) as 'Plan Start Date', " & _
              SQL1 & "," & SQL11 & "," & _
              " cast(SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA017 as decimal(15,2)) as 'WIP Qty', " & _
              " cast(SFCTA.TA010 as decimal(15,2)) as 'Input Qty(+)' " & _
              " " & returnFld("(BOMMF.MF024+BOMMF.MF025*(SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA017))", "'Machine Min.'") & _
              " ," & SQL2 & ",'' as 'Actual Time.','' as 'Seq.','' as 'Date Plan','' as 'Plan Time','' as 'Machine No.','' as 'Remark' " & _
              " from SFCTA " & _
              " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
              " left join MOCTB on MOCTB.TB001 = MOCTA.TA001 and MOCTB.TB002 = MOCTA.TA002 and (SUBSTRING(MOCTB.TB003,3,1) ='1' or MOCTB.TB003 is null)  " & _
              " left join BOMMF on BOMMF.MF001 = MOCTA.TA006 and BOMMF.MF002 ='01' and BOMMF.MF003 = SFCTA.TA003 and BOMMF.MF004 = SFCTA.TA004 and BOMMF.MF006=SFCTA.TA006 " & _
              " left join CMSMW on CMSMW.MW001 = SFCTA.TA004 " & _
              " left join DBMIS.dbo.MoUrgent MU on MU.TA001 = SFCTA.TA001 and  MU.TA002 = SFCTA.TA002 " & _
              " where MOCTA.TA013='Y' and MOCTA.TA011 not in('y','Y') and SFCTA.TA005='" & selProperty & "' " & _
              " and SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA017>0 " & whr & _
              " order by MU.TA001 DESC,MU.TA002 DESC,MOCTB.TB012,MOCTB.TB013,SFCTA.TA008,MOCTA.TA006,SFCTA.TA001,SFCTA.TA002,SFCTA.TA003 "
        'SQL = ""
        'MOCTA.TA006,
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExportGrid.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Function returnFld(fldName As String, fldCall As String) As String
        Return ",CONVERT(varchar, floor(" & fldName & "/60)) as " & fldCall
    End Function
    Protected Sub btExportGrid_Click(sender As Object, e As EventArgs) Handles btExportGrid.Click
        ControlForm.ExportGridViewToExcel("DailyMchPlan" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With

        
    End Sub
End Class