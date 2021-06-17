Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class FGLabel
    Inherits System.Web.UI.Page
    '
    'Dim Conn_SQL As New ConnSQL
    'Dim ConfigDate As New ConfigDate
    'Dim Pfunction As New Pfunction
    'Dim ControlForm As New ControlDataForm

    Dim dbConn As New DataConnectControl
    Dim dateCont As New DateControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim CreateTable As New CreateTable
            CreateTable.CreateFGLabel()
            Dim SQL As String = ""
            Dim ddlCont As New DropDownListControl
            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ddlCont.showDDL(ddlMoType, SQL, VarIni.ERP, "MQ002", "MQ001", True)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('D3') order by MQ002"
            ddlCont.showDDL(ddlMoRType, SQL, VarIni.ERP, "MQ002", "MQ001", True)
            'btExportExcel.Visible = False
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Protected Sub gvShow_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShow.RowCommand

        If e.CommandName = "OnView" Then
            Dim i As Integer = e.CommandArgument
            If MultiView1.ActiveViewIndex = 0 Then
                MultiView1.SetActiveView(View2)
            End If
            With gvShow.Rows(i)
                lbDateRec.Text = .Cells(1).Text.Replace(" ", "").Replace("&nbsp;", "")
                lbMoR.Text = .Cells(2).Text.Replace(" ", "")
                lbMo.Text = .Cells(3).Text.Replace(" ", "")
                lbItem.Text = .Cells(4).Text.Replace(" ", "")
                lbItemRev.Text = .Cells(5).Text.Replace("&nbsp;", "")
                lbSpec.Text = .Cells(6).Text.Replace("&nbsp;", "")
                lbItemWgh.Text = .Cells(12).Text.Replace(" ", "")
                lbQty.Text = .Cells(13).Text.Replace(" ", "")
                tbQtyCtn.Text = .Cells(14).Text.Replace(" ", "")
                tbCtnWgh.Text = .Cells(15).Text.Replace(" ", "")
                tbCtnNo.Text = .Cells(16).Text.Replace("&nbsp;", "")
                lbCtnSpec.Text = .Cells(17).Text.Replace(" ", "").Replace("&nbsp;", "")
                tbPack.Text = .Cells(18).Text.Replace("&nbsp;", "")
                lbSo.Text = .Cells(19).Text.Replace(" ", "")
                lbCustofCust.Text = .Cells(21).Text.Replace(" ", "").Replace("&nbsp;", "")
                lbLotNo.Text = .Cells(22).Text.Replace(" ", "").Replace("&nbsp;", "")
                lbTestNo.Text = .Cells(23).Text.Replace(" ", "").Replace("&nbsp;", "")
            End With
            calCarton()
            btCancel.Visible = True
            showElement()
        End If

    End Sub

    Sub showElement()
        'Dim lotCon As String = Conn_SQL.Get_value("select MB022 from INVMB where MB001='" & lbItem.Text.Trim.Replace("-", "") & "' ", Conn_SQL.ERP_ConnectionString)
        'Dim showLable As Boolean = False
        'If lotCon = "N" Or lotCon = Nothing Or lotCon = "" Then
        '    showLable = True
        'End If
        'lbPO.Visible = showLable
        'tbPO.Visible = Not showLable
        'tbSerialNo.Enabled = Not showLable
        btPrint.Visible = True
        If lbDateRec.Text.Trim = "" Then
            btPrint.Visible = False
        End If
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click

        Dim SQL As String = "",
            WHR As String = ""
        WHR &= dbConn.WHERE_EQUAL("SFCTB.TB001", ddlMoRType)
        WHR &= dbConn.WHERE_LIKE("SFCTB.TB002", tbMoRNo)
        WHR &= dbConn.WHERE_LIKE("SFCTC.TC003", tbMoRSeq)
        WHR &= dbConn.WHERE_EQUAL("SFCTB.TB013", ddlStatus)

        WHR &= dbConn.WHERE_EQUAL("MOCTA.TA001", ddlMoType)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA002", tbMoNo)

        'WHR = WHR & Conn_SQL.Where("MOCTA.TA026", tbSoType)
        'WHR = WHR & Conn_SQL.Where("MOCTA.TA027", tbSoNo)
        'WHR = WHR & Conn_SQL.Where("MOCTA.TA028", tbSoSeq)

        WHR &= dbConn.WHERE_LIKE("MOCTA.TA006", tbItem)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA034", tbDesc)
        'WHR = WHR & Conn_SQL.Where("COPTD.TD014", tbCustItem)
        If tbDate.Text.Trim <> "" Then
            WHR &= dbConn.Where("SFCTB.TB003", dateCont.dateFormat2(tbDate.Text.Trim), True, False)
        End If

        SQL = "select F.DocNo A,rtrim(SFCTB.TB001)+'-'+SFCTB.TB002+'-'+SFCTC.TC003 B," &
            " Rtrim(MOCTA.TA001)+'-'+MOCTA.TA002 C, Rtrim(MOCTA.TA026)+'-'+RTRIM (MOCTA.TA027)+'-'+RTRIM(MOCTA.TA028) D," &
            " case when isnull(F.DocNo,'')='' then COPTD.TD027 else custPO end E," &
            " case when len(MOCTA.TA006)=16 then (SUBSTRING(MOCTA.TA006,1,14)+'-'+SUBSTRING(MOCTA.TA006,15,2)) else MOCTA.TA006 end F,	" &
            " MOCTA.TA034 G, " &
            " case when isnull(COPTD.TD014,'')='' then SFCTC.TC049 else COPTD.TD014 end + case when rtrim(INVMB.UDF05)='' then '' else '-Rev.'+INVMB.UDF05 end  H, " &
            " MOCTA.TA015 I, MOCTA.TA017 J, MOCTA.TA018 K,Rtrim(COPTC.TC004)+'-'+COPMA.MA002 L ," &
            " case when MOCTA.TA011 = 'Y' then 'Completed' " &
            " when MOCTA.TA011 = 'y' then 'Manual Completed' " &
            " when MOCTA.TA011 = '1' then 'Not Produced' " &
            " when MOCTA.TA011 = '2' then 'Issued'" &
            " when MOCTA.TA011 = '3' then 'Producing' end M," &
            " INVMB.MB014  N,SFCTC.TC036 O, F.PackBy Z," &
            " case when F.DocNo <> '' then F.qtyCtn else INVMB.UDF51 end  P," &
            " case when F.DocNo <> '' then F.CtnWgh else INVMB.MB075 end  Q, " &
            " case when F.DocNo <> '' then F.CtnNo  else INVMB.UDF03 end  R, " &
            " case when F.DocNo <> '' then F.CtnSpec else INVMB.UDF04 end  S ," &
            " COPTD.TD020,SFCTC.UDF01,INVMB.UDF06,INVMB.UDF01 INVUDF01 " &
            " from SFCTC " &
            " left join MOCTA on MOCTA.TA001 = SFCTC.TC004 And MOCTA.TA002 = SFCTC.TC005 " &
            " left join COPTD on COPTD.TD001 = MOCTA.TA026 And COPTD.TD002 = MOCTA.TA027 And COPTD.TD003 = MOCTA.TA028 " &
            " left join COPTC on COPTC.TC001 = MOCTA.TA026 And COPTC.TC002 = MOCTA.TA027" &
            " left join COPMA on COPMA.MA001 = COPTC.TC004 " &
            " left join SFCTB on SFCTB.TB001 = SFCTC.TC001 And SFCTB.TB002 = SFCTC.TC002" &
            " left join " & VarIni.DBMIS & "..FGLabel F on F.moType = SFCTB.TB001 And F.moNo = SFCTB.TB002 And F.moSeq = SFCTC.TC003 " &
            " left join INVMB on INVMB.MB001 = MOCTA.TA006 where SFCTB.TB001 Like 'D3%' " & WHR &
            " order by SFCTB.TB001,SFCTB.TB002,SFCTB.TB003 "
        'SFCTB.TB001 = 'D301'
        Dim gvCont As New GridviewControl
        gvCont.ShowGridView(gvShow, SQL, VarIni.ERP)
        lbCount.Text = gvShow.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        SQLPrintSave()
    End Sub
    Private Sub SQLPrintSave()
        Dim dtCont As New DataTableControl
        Dim MO As String = lbMoR.Text.Trim,
            MoType As String = MO.Substring(0, 4),
            MoNo As String = MO.Substring(5, 11),
            MoSeq As String = MO.Substring(17, 4),
            Qty As String = lbQty.Text.Replace(",", ""),
            QtyCtn As String = tbQtyCtn.Text.Trim.Replace(",", ""),
            CtnNo As String = tbCtnNo.Text.Trim,
            CtnSpec As String = "",
            CtnWgh As String = tbCtnWgh.Text.Trim,
            CrBy As String = Session("UserName"),
            CrDate As String = Date.Today.ToString("yyyyMMdd hhmmss"),
            PackBy As String = tbPack.Text.Trim,
            remark As String = lbCustofCust.Text.Trim

        'Dim custPO As String = tbPO.Text, serialNo As String = tbSerialNo.Text
        Dim SQL As String = "select MB003 from INVMB where MB001='" & CtnNo & "' "
        Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        If dr IsNot Nothing Then
            CtnSpec = dtCont.IsDBNullDataRow(dr, "MB003")
        End If

        'CtnSpec = Conn_SQL.Get_value("select MB003 from INVMB where MB001='" & CtnNo & "' ", Conn_SQL.ERP_ConnectionString)
        Dim newDoc As Boolean = False
        If lbDateRec.Text.Trim = "" Then
            lbDateRec.Text = getDocNo()
            newDoc = True
        End If
        Dim DocNo As String = lbDateRec.Text.Trim

        If lbMo.Text <> "" Then
            Dim fld As Hashtable = New Hashtable
            Dim whr As Hashtable = New Hashtable

            whr.Add("DocNo", DocNo)
            whr.Add("moType", MoType)
            whr.Add("moNo", MoNo)
            whr.Add("moSeq", MoSeq)

            fld.Add("qty", Qty)
            fld.Add("qtyCtn", QtyCtn)
            fld.Add("CtnNo", CtnNo)
            fld.Add("CtnSpec", CtnSpec)
            fld.Add("CtnWgh", CtnWgh)
            fld.Add("PackBy", PackBy)
            fld.Add("remark", remark)

            'fld.Add("custPO", custPO)
            'fld.Add("serialNo", serialNo)
            fld.Add(If(newDoc, "CreateBy", "ChangeBy"), CrBy)
            fld.Add(If(newDoc, "CreateDate", "ChangeDate"), CrDate)
            SQL = dbConn.GetSQL("FGLabel", fld, whr, If(newDoc, "I", "U"))
            dbConn.TransactionSQL(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        End If
        print()
        showElement()
    End Sub

    Private Function getDocNo() As String
        Dim DocNo As String = ""
        Dim DateDoc As String = Date.Today.ToString("yyyyMMdd")
        'Dim ymd As String = configDate.dateFormat2(tbDate.Text.Trim)
        Dim SQL As String = "select substring(DocNo,1,8),max(DocNo) as DocNo  from FGLabel where DocNo like '" & DateDoc & "%' group by substring(DocNo,1,8)"
        Dim Program As New Data.DataTable
        Program = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        If Program.Rows.Count > 0 Then
            DocNo = CDec(Program.Rows(0).Item("DocNo")) + 1
        Else
            DocNo = DateDoc & "001"
        End If
        Return DocNo
    End Function

    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCancel.Click
        Clear()
        If MultiView1.ActiveViewIndex <> 0 Then
            MultiView1.SetActiveView(View1)
        End If
    End Sub

    Private Sub Clear()
        'lbBatch.Text = ""
        lbCount.Text = ""
        lbDateRec.Text = ""
        lbItemRev.Text = ""
        lbFull.Text = ""
        lbFullG.Text = ""
        lbFullN.Text = ""
        lbItem.Text = ""
        lbItemWgh.Text = ""
        lbMo.Text = ""
        lbMoR.Text = ""
        lbNotFull.Text = ""
        lbNotFullG.Text = ""
        lbNotFullN.Text = ""
        lbQty.Text = ""
        lbSo.Text = ""
        lbSpec.Text = ""

        lbLotNo.Text = ""
        lbTestNo.Text = ""

        tbCtnNo.Text = ""
        lbCtnSpec.Text = ""
        tbCtnWgh.Text = ""
        tbQtyCtn.Text = ""
        lbError.Text = ""
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        print()
    End Sub
    Sub print()
        'Dim SQL As String = " select MB022 from INVMB where MB001='" & lbItem.Text.Trim.Replace("-", "") & "'"
        Dim print As Boolean = True
        If print Then
            lbError.Text = ""
            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/labelCNC.aspx?docno=" & lbDateRec.Text.Trim & "&prtfor=" & codeControl & "');", True)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/labelFGReceipt.aspx?docno=" & lbDateRec.Text.Trim & "&prtfor=" & ddlFormatPrint.Text & "');", True)
        Else
            lbError.Text = "can't print cause mat issue less assembly record please check.!!!"
        End If
    End Sub

    Protected Sub btCal_Click(sender As Object, e As EventArgs) Handles btCal.Click
        calCarton()
    End Sub

    Sub calCarton()
        Dim qtyCnt As Decimal = CDec(tbQtyCtn.Text.Trim)
        Dim qty = CDec(lbQty.Text.Trim)
        Dim full As Integer = 0,
            notfull As Integer = qty

        If qtyCnt > 0 Then
            full = Math.Floor(qty / qtyCnt)
            notfull = qty Mod qtyCnt
        End If

        lbFull.Text = full
        lbNotFull.Text = notfull
        Dim itemWgh As Decimal = CDec(lbItemWgh.Text.Trim)
        Dim fullNest As Decimal = qtyCnt * itemWgh
        Dim notFullNest As Decimal = notfull * itemWgh
        'Dim fullGross As Decimal = fullNest + CDec(tbCtnWgh.Text)
        Dim notFullGross As Decimal = 0
        lbFullN.Text = fullNest
        lbNotFullN.Text = notFullNest

        lbFullG.Text = fullNest + CDec(tbCtnWgh.Text)
        If notfull > 0 Then
            notFullGross = notFullNest + CDec(tbCtnWgh.Text)
        End If
        lbNotFullG.Text = notFullGross

    End Sub

    Protected Sub btPrintEmpty_Click(sender As Object, e As EventArgs) Handles btPrintEmpty.Click
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/labelFGReceipt.aspx?docno=&prtfor=" & ddlFormatPrint.Text & "');", True)
    End Sub
End Class
