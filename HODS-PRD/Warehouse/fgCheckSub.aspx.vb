Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class fgCheckSub
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim gvCont As New GridviewControl
        Dim dbConn As New DataConnectControl
        Dim outCont As New OutputControl

        Dim item As String = outCont.DecodeFrom64(Request.QueryString("item").ToString.Trim),
            dateFrom As String = outCont.DecodeFrom64(Request.QueryString("dateF").ToString.Trim),
            dateTo As String = outCont.DecodeFrom64(Request.QueryString("dateT").ToString.Trim)
        Dim whr As String = "",
            SQL As String = ""

        whr = dbConn.WHERE_EQUAL("TC047", item)
        whr &= dbConn.WHERE_BETWEEN("TB003", dateFrom, dateTo)

        'Mo receipt
        Dim colName As ArrayList = New ArrayList,
            fldShow As String = ""

        colName.Add("Date:TB003")
        fldShow &= "TB003,"
        colName.Add("MO Receipt:TC00123")
        fldShow &= "rtrim(TC001)+'-'+rtrim(TC002)+'-'+TC003 TC00123,"
        colName.Add("Item:TC047")
        fldShow &= "TC047,"
        colName.Add("Spec:TC049")
        fldShow &= "TC049,"
        colName.Add("Qty:TC014:0")
        fldShow &= "TC014,"
        colName.Add("Unit:TC010")
        fldShow &= "TC010,"
        colName.Add("WH:TB008")
        fldShow &= "TB008,"
        colName.Add("App by:TB016")
        fldShow &= "TB016"

        ucHeadIn.HeaderLable = "MO Receipt"
        gvCont.GridviewColWithLinkFirst(gvShowIn, colName)
        SQL = "select " & fldShow & " from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 where TB013='Y' and TB001='D301' " & whr & " order by TB003,TB001,TB002 "
        gvCont.ShowGridView(gvShowIn, SQL, VarIni.ERP)
        ucCountRowIn.RowCount = gvCont.rowGridview(gvShowIn)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShowIn", "gridviewScrollgvShowIn();", True)


        'sale delivery
        whr = dbConn.WHERE_EQUAL("TH004", item)
        whr &= dbConn.WHERE_BETWEEN("TG003", dateFrom, dateTo)
        colName = New ArrayList
        fldShow = ""

        colName.Add("Date:TG003")
        fldShow &= "TG003,"
        colName.Add("Sale Delivery:TH00123")
        fldShow &= "rtrim(TH001)+'-'+rtrim(TH002)+'-'+TH003 TH00123,"
        colName.Add("Item:TH004")
        fldShow &= "TH004,"
        colName.Add("Spec:TH006")
        fldShow &= "TH006,"
        colName.Add("Qty:TB022:0")
        fldShow &= "TB022,"
        colName.Add("Unit:TH009")
        fldShow &= "TH009,"
        colName.Add("WH:TH007")
        fldShow &= "TH007,"
        colName.Add("Sale Inv:TB0012")
        fldShow &= "rtrim(TB001)+'-'+rtrim(TB002)+'-'+rtrim(TB003) TB0012,"
        colName.Add("Cust PO:TB048")
        fldShow &= "TB048,"
        colName.Add("Cust WO:UDF01")
        fldShow &= "COPTH.UDF01,"
        colName.Add("Cust Line:UDF02")
        fldShow &= "COPTH.UDF02,"
        colName.Add("App by:TG043")
        fldShow &= "TG043"

        ucHeadOut.HeaderLable = "Sale Delivery"
        gvCont.GridviewColWithLinkFirst(gvShowOut, colName)
        SQL = "select " & fldShow & " from COPTH left join COPTG on TG001=TH001 and TG002=TH002 left join ACRTB on TB005=TH001 and TB006=TH002 and TB007=TH003  where TG023 = 'Y' " & whr & " order by TG003,TH001,TH002,TH003"
        gvCont.ShowGridView(gvShowOut, SQL, VarIni.ERP)
        ucCountRowOut.RowCount = gvCont.rowGridview(gvShowOut)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShowOut", "gridviewScrollgvShowOut();", True)

        'sale Invoice summary daily
        whr = dbConn.WHERE_EQUAL("TB039", item)
        whr &= dbConn.WHERE_BETWEEN("TA003", dateFrom, dateTo)
        colName = New ArrayList
        fldShow = ""
        colName.Add("Inv Date:TA003")
        fldShow &= "TA003,"
        colName.Add("Item:TB039")
        fldShow &= "TB039,"
        colName.Add("Spec:TB041")
        fldShow &= "TB041,"
        colName.Add("Inv Qty Sum:TB022:2")
        fldShow &= "sum(TB022) TB022 "

        ucHeadSum.HeaderLable = "Sale Invoice Summary"
        gvCont.GridviewColWithLinkFirst(gvShowSum, colName)
        ' SQL = "select " & fldShow & " from COPTH left join COPTG on TG001=TH001 and TG002=TH002 left join ACRTB on TB005=TH001 and TB006=TH002 and TB007=TH003  where TG023 = 'Y' " & whr & " order by TG003,TH001,TH002,TH003"
        SQL = "select " & fldShow & "  from ACRTB left join ACRTA on TA001=TB001 and TA002=TB002 where TA025 = 'Y' " & whr & " group by TA003,TB039,TB041 order by TA003 "

        gvCont.ShowGridView(gvShowSum, SQL, VarIni.ERP)
        ucCountRowSum.RowCount = gvCont.rowGridview(gvShowSum)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShowSum", "gridviewScrollgvShowSum();", True)

    End Sub

    Protected Sub btExcel1_Click(sender As Object, e As EventArgs) Handles btExcel1.Click
        Dim expCont As New ExportImportControl
        expCont.Export("MOReceipt" & Session(VarIni.UserName), gvShowIn)
    End Sub

    Protected Sub btExcel2_Click(sender As Object, e As EventArgs) Handles btExcel2.Click
        Dim expCont As New ExportImportControl
        expCont.Export("SaleDelivery" & Session(VarIni.UserName), gvShowOut)
    End Sub

    Protected Sub btExcel3_Click(sender As Object, e As EventArgs) Handles btExcel3.Click
        Dim expCont As New ExportImportControl
        expCont.Export("SaleInvoice" & Session(VarIni.UserName), gvShowSum)
    End Sub
End Class