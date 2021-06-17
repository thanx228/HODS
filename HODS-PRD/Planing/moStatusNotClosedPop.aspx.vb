Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class moStatusNotClosedPop
    Inherits System.Web.UI.Page
    'Dim Conn_SQL As New ConnSQL
    'Dim ControlForm As New ControlDataForm
    'Dim ConfigDate As New ConfigDate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim dbConn As New DataConnectControl

            Dim tempTable As String = Request.QueryString("tempTable").ToString.Trim,
                motype As String = Request.QueryString("motype").ToString.Trim,
                selmonth As String = Request.QueryString("selmonth").ToString.Trim,
                SQL As String

            lbHeader.Text = "Detail for MO Type " & motype & " on monthly(" & selmonth.Substring(4, 2) & "-" & selmonth.Substring(0, 4) & ")"
            'show detail
            Dim colName As ArrayList = New ArrayList,
            fld As String = ""
            fld = "MOCTA.TA010 A,"
            colName.Add("MO Plan Complete:A")
            fld &= "SFCTA.TA009 B,"
            colName.Add("Process Plan Complete:B")
            fld &= "case MOCTA.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' else 'MO Completed' end C,"
            colName.Add("MO Status:C")
            fld &= "CMSMQ.MQ002 D1,"
            colName.Add("MO Type Name:D1")
            fld &= "SFCTA.TA001 D2,"
            colName.Add("MO Type:D2")
            fld &= "SFCTA.TA002 E,"
            colName.Add("MO:E")
            fld &= "SFCTA.TA003 F,"
            colName.Add("MO Seq:F")
            fld &= "SFCTA.TA006 G1,"
            colName.Add("W/C:G1")
            fld &= "CMSMD.MD002 G2,"
            colName.Add("W/C:G2")
            fld &= "SFCTA.TA004 H1,"
            colName.Add("Operation:H1")
            fld &= "CMSMW.MW002 H2,"
            colName.Add("OP DESC:H2")
            fld &= "case when len(MOCTA.TA006)=16 then (SUBSTRING(MOCTA.TA006,1,14)+'-'+SUBSTRING(MOCTA.TA006,15,2)) else MOCTA.TA006 end I,"
            colName.Add("Item:I")
            fld &= "MOCTA.TA034 J,"
            colName.Add("Desc:J")
            fld &= "MOCTA.TA035 K,"
            colName.Add("Desc:K")
            fld &= "MOCTA.TA015  L,"
            colName.Add("Plan Qty:L:0")
            fld &= "SFCTA.TA011  M,"
            colName.Add("Completed Qty:M:0")
            fld &= "SFCTA.TA012+SFCTA.TA056  N,"
            colName.Add("Scrap Qty:N:0")
            fld &= "MOCTA.TA007 O,"
            colName.Add("Unit:O")
            fld &= "SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA056-SFCTA.TA058 P,"
            colName.Add("WIP QTY:P:0")
            fld &= "isnull((select top 1 SFCTC.TC001+'-'+SFCTC.TC002 from " & VarIni.ERP & "..SFCTC where SFCTC.TC004=SFCTA.TA001 and SFCTC.TC005=SFCTA.TA002 and SFCTC.TC006=SFCTA.TA003 order by SFCTC.CREATE_DATE desc),'') Q,"
            colName.Add("Last Transfer:Q")
            fld &= "MOCTA.TA026 R,"
            colName.Add("SO Type:R")
            fld &= "MOCTA.TA027 S,"
            colName.Add("SO No:S")
            fld &= "MOCTA.TA028 T,"
            colName.Add("SO Seq:T")
            fld &= "COPTC.TC004 U,"
            colName.Add("Customer:U")
            fld &= "COPMA.MA002 V,"
            colName.Add("Customer Desc:V")
            fld &= "COPTC.TC012  W "
            colName.Add("Industry Type:W")

            Dim gvCont As New GridviewControl
            gvCont.GridviewColWithLinkFirst(gvShow, colName)
            SQL = " select " & fld & " from " & VarIni.DBMIS & ".." & tempTable & " T " &
                  " left join SFCTA on SFCTA.TA001=T.moType and SFCTA.TA002=T.moNo and SFCTA.TA003=T.moSeq " &
                  " left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " &
                  " left join CMSMQ on CMSMQ.MQ001=MOCTA.TA001 " &
                  " left join CMSMD on CMSMD.MD001=SFCTA.TA006 " &
                  " left join CMSMW on CMSMW.MW001=SFCTA.TA004 " &
                  " left join COPTC on COPTC.TC001=MOCTA.TA026 and COPTC.TC002=MOCTA.TA027 " &
                  " left join COPMA on COPMA.MA001=COPTC.TC004 " &
                  " where T.moType='" & motype & "' and MOCTA.TA010 like '" & selmonth & "%'" &
                  " order by MOCTA.TA010,SFCTA.TA001,SFCTA.TA002,SFCTA.TA003 "
            gvCont.ShowGridView(gvShow, SQL, VarIni.ERP)
            ucCountRow.RowCount = gvCont.rowGridview(gvShow)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        Dim expCont As New ExportImportControl
        Dim motype As String = Request.QueryString("motype").ToString.Trim,
            selmonth As String = Request.QueryString("selmonth").ToString.Trim
        expCont.Export("OperationReport_" & motype & "_" & selmonth & "_" & Session("UserName"), gvShow)
    End Sub
End Class