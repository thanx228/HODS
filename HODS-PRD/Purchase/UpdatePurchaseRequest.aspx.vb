Imports MIS_HTI.DataControl

Public Class UpdatePurchaseRequest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ucPrType.setShowType("31", False)
        End If
    End Sub

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        Dim docType As String = ucPrType.text
        Dim dbConn As New DataConnectControl
        Dim dtCont As New DataTableControl
        Dim dateCont As New DateControl

        Dim fldName As New ArrayList
        'fldName.Add(PURTB.DOC_TYPE)
        fldName.Add(PURTB.ITEM)
        fldName.Add(dbConn.SUM(PURTB.QTY, VarIni.char8, True))
        fldName.Add("min(" & PURTA.DOC_DATE & ")" & VarIni.char8 & PURTA.DOC_DATE)

        Dim strSQL As New SQLString(PURTB.TABLE, fldName)
        strSQL.setLeftjoin(VarIni.L & PURTA.TABLE & VarIni.O2 & PURTA.DOC_TYPE & VarIni.E & PURTB.DOC_TYPE & VarIni.A & PURTA.DOC_NO & VarIni.E & PURTB.DOC_NO)
        Dim whr As String = ""
        whr &= strSQL.WHERE_EQUAL(PURTB.DOC_TYPE, ucPrType.getObject)
        whr &= strSQL.WHERE_EQUAL(PURTB.DOC_NO, tbPrNo)
        strSQL.SetWhere(whr, True)
        strSQL.SetOrderBy(PURTB.ITEM)
        strSQL.SetGroupBy(PURTB.ITEM)
        Dim SQL As String = strSQL.GetSQLString

        Dim colName As New ArrayList
        colName.Add("PR Type" & VarIni.char8 & PURTB.DOC_TYPE)
        colName.Add("PR No" & VarIni.char8 & PURTB.DOC_NO)
        colName.Add("Item" & VarIni.char8 & PURTB.ITEM)
        colName.Add("PR Qty" & VarIni.char8 & PURTB.QTY & VarIni.char8 & "3")
        colName.Add("Last Date " & VarIni.char8 & "LAST_DATE")
        colName.Add("PR Qty last Month " & VarIni.char8 & PURTB.PR_SUM_LAST_MONTH & VarIni.char8 & "3")

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, VarIni.char8)

        Dim dt As DataTable = strSQL.Query(SQL, VarIni.ERP, strSQL.WhoCalledMe)
        Dim sqlList As New ArrayList
        For Each dr As DataRow In dt.Rows
            Dim fldData As New ArrayList
            fldData.Add(PURTB.DOC_TYPE & VarIni.char8 & ucPrType.text)
            fldData.Add(PURTB.DOC_NO & VarIni.char8 & tbPrNo.Text)
            Dim item As String = dtCont.IsDBNullDataRow(dr, PURTB.ITEM)
            fldData.Add(PURTB.ITEM & VarIni.char8 & item)
            fldData.Add(PURTB.QTY & VarIni.char8 & dtCont.IsDBNullDataRowDecimal(dr, PURTB.QTY))

            'get doc date from purchase request head
            Dim purDate As String = dtCont.IsDBNullDataRow(dr, PURTA.DOC_DATE, Date.Now.ToString("yyyyMMdd")).Substring(0, 6) & "01"
            fldName = New ArrayList
            fldName.Add(PURTA.DOC_DATE)
            strSQL = New SQLString(PURTB.TABLE, fldName, top:=1)
            strSQL.setLeftjoin(VarIni.L & PURTA.TABLE & VarIni.O2 & PURTA.DOC_TYPE & VarIni.E & PURTB.DOC_TYPE & VarIni.A & PURTA.DOC_NO & VarIni.E & PURTB.DOC_NO)
            whr = ""
            whr &= strSQL.WHERE_EQUAL(PURTB.ITEM, item)
            whr &= strSQL.WHERE_EQUAL(PURTA.DOC_DATE, purDate, "<")
            strSQL.SetWhere(whr, True)
            strSQL.SetOrderBy(PURTA.DOC_DATE & VarIni.DESC)

            Dim drTA As DataRow = strSQL.QueryDataRow(strSQL.GetSQLString, VarIni.ERP, strSQL.WhoCalledMe)
            Dim prLastMonthQty As Decimal = 0
            Dim docDate As String = ""
            If drTA IsNot Nothing Then
                docDate = dtCont.IsDBNullDataRow(drTA, PURTA.DOC_DATE)
                If docDate <> "" Then
                    Dim newDocDate As String = docDate.Substring(0, 6) & "01"
                    Dim dateStart As String = newDocDate
                    Dim dateEnd As String = dateCont.strToDateTime(newDocDate, "yyyyMMdd").AddMonths(1).AddDays(-1).ToString("yyyyMMdd")

                    strSQL = New SQLString(PURTB.TABLE)
                    fldName = New ArrayList
                    fldName.Add(dbConn.SUM(PURTB.QTY, VarIni.char8, True))
                    strSQL.SetSelect(fldName)
                    strSQL.setLeftjoin(VarIni.L & PURTA.TABLE & VarIni.O2 & PURTA.DOC_TYPE & VarIni.E & PURTB.DOC_TYPE & VarIni.A & PURTA.DOC_NO & VarIni.E & PURTB.DOC_NO)
                    whr = strSQL.WHERE_EQUAL(PURTB.ITEM, item)
                    whr &= strSQL.WHERE_BETWEEN(PURTA.DOC_DATE, dateStart, dateEnd)
                    strSQL.SetWhere(whr, True)
                    Dim aas As String = strSQL.GetSQLString
                    Dim drSub As DataRow = strSQL.QueryDataRow(strSQL.GetSQLString, VarIni.ERP, strSQL.WhoCalledMe)
                    prLastMonthQty = dtCont.IsDBNullDataRowDecimal(drSub, PURTB.QTY)
                End If
            End If
            fldData.Add("LAST_DATE" & VarIni.char8 & docDate)
            fldData.Add(PURTB.PR_SUM_LAST_MONTH & VarIni.char8 & prLastMonthQty)
            dtCont.addDataRow(dtShow, fldData, VarIni.char8)

            Dim whrHash As New Hashtable
            whrHash.Add(PURTB.DOC_TYPE, ucPrType.text)
            whrHash.Add(PURTB.DOC_NO, tbPrNo.Text.Trim)
            whrHash.Add(PURTB.ITEM, item)

            Dim fldHash As New Hashtable
            fldHash.Add(PURTB.PR_SUM_LAST_MONTH, prLastMonthQty)

            sqlList.Add(dbConn.getUpdateSql(PURTB.TABLE, fldHash, whrHash, True))

        Next

        ucGv.showGridview(dtShow, colName, strSplit:=VarIni.char8)
        dbConn.TransactionSQL(sqlList, VarIni.ERP, dbConn.WhoCalledMe)

    End Sub
End Class