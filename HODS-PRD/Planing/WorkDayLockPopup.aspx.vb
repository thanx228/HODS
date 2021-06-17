Public Class WorkDayLockPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim wc As String = Request.QueryString("wc").ToString.Trim,
                lock As Integer = CInt(Request.QueryString("lock").ToString.Trim),
                fDate As String = Request.QueryString("fDate").ToString.Trim,
                tDate As String = Request.QueryString("tDate").ToString.Trim,
                SQL As String = "",
                whr As String = ""

            lbWc.Text = wc
            lbLock.Text = lock
            lbDateFrom.Text = ConfigDate.dateShow2(fDate)
            lbDateTo.Text = ConfigDate.dateShow2(tDate)

            If fDate <> "" And tDate <> "" Then
                whr = " and SFC.TA008 between '" & fDate & "' and '" & tDate & "' "
            Else
                If fDate <> "" Then
                    whr = " and SFC.TA008 < '" & fDate & "' "
                Else
                    whr = " and SFC.TA008 = '" & tDate & "' "
                End If
            End If
            If lock = 4 Then
                whr = whr & " and MOC.TA050 not in('1','2','3') "
            Else
                whr = whr & " and MOC.TA050 ='" & lock & "' "
            End If


            SQL = " select (SUBSTRING(SFC.TA008,7,2)+'-'+SUBSTRING(SFC.TA008,5,2)+'-'+SUBSTRING(SFC.TA008,1,4)) AS 'Plan Date'," & _
                  " MOC.TA026+'-'+MOC.TA027+'-'+MOC.TA028 as 'SO Detail'," & _
                  "COP.TD004 as 'SO Item',COP.TD006 as 'SO Spec'," & _
                  " (SUBSTRING(COP.TD013,7,2)+'-'+SUBSTRING(COP.TD013,5,2)+'-'+SUBSTRING(COP.TD013,1,4)) AS 'SO Due Date'," & _
                  " SFC.TA001+'-'+SFC.TA002+'-'+SFC.TA003 as 'MO Detail'," & _
                  "  (SUBSTRING(MOC.TA010,7,2)+'-'+SUBSTRING(MOC.TA010,5,2)+'-'+SUBSTRING(MOC.TA010,1,4)) AS 'WO Plan finish', " & _
                  " MOC.TA006 as 'MO Item',MOC.TA035 as 'MO Spec',cast(MOC.TA015 as decimal(10,3)) as 'MO Qty' " & _
                  " from SFCTA SFC " & _
                  " left join MOCTA MOC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " & _
                  " left join COPTD COP on COP.TD001=MOC.TA026 and COP.TD002=MOC.TA027 and COP.TD003=MOC.TA028" & _
                  " where  SFC.TA006='" & wc & "' and SFC.TA005='1' " & _
                  " and SFC.TA008<>'' and MOC.TA011 not in ('Y','y') and SFC.TA032='N' " & whr & _
                  " order by COP.TD013,SFC.TA008,SFC.TA001,SFC.TA002,SFC.TA003 "
            'select MOC.TA026+'-'+MOC.TA027+'-'+MOC.TA028 as 'SO Detail',
            'COP.TD013 AS 'SO Due Date',
            'SFC.TA001+'-'+SFC.TA002+'-'+SFC.TA003 as 'WO Detail',
            'MOC.TA010 AS 'WO Plan finish',
            'MOC.TA006 as 'Item',MOC.TA035 as 'Spec',MOC.TA015 as 'Qty' 
            'from SFCTA SFC 
            'left join MOCTA MOC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 
            'left join COPTD COP on COP.TD001=MOC.TA026 and COP.TD002=MOC.TA027 and COP.TD003=MOC.TA028 
            'where SFC.TA008='20130117' and SFC.TA006='W01' and MOC.TA044='N' and SFC.TA005='1' and SFC.TA008<>'' and MOC.TA011 not in ('Y','y') and SFC.TA032='N'
            ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
            lbCount.Text = ControlForm.rowGridview(gvShow)
        End If
    End Sub
    'Function returnFld(ByVal fldName As String, ByVal fldCall As String) As String
    '    Return ",CONVERT(varchar, floor(" & fldName & "/3600)) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),floor((" & fldName & " % 3600) / 60))), 2) + ':' + RIGHT('0' + CONVERT(varchar,CONVERT(decimal(2,0),(" & fldName & " % 60))), 2) as " & fldCall
    'End Function
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        Dim wc As String = Request.QueryString("wc").ToString.Trim
        ControlForm.ExportGridViewToExcel("OperationReport" & wc & Session("UserName"), gvShow)
    End Sub
End Class