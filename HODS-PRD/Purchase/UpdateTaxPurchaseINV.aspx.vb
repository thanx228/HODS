Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class UpdateTaxPurchaseINV
    Inherits System.Web.UI.Page
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl
    Dim dateCont As New DateControl


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            btPlusBC.Visible = False
            btMinusBC.Visible = False
            btUpdate.Visible = False
        End If

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        'เดือนปัจจุบันที่ User ค้นหา
        Dim DateNow As String = Date.Now.ToString("yyyyMM") '202105

        'Format เดือนปัจจุบันที่ User ค้นหา
        Dim endMonth As Date = dateCont.strToDateTime(DateNow & "01", "yyyyMMdd").AddMonths(1) '20210601  วันที่ 1 ของเดือนถัดไปจากวันที่ User ค้นหา         
        Dim endMonth1 As Date = endMonth.AddDays(-1)
        Dim formatendMonth As String = endMonth.AddDays(-1).ToString("yyyyMMdd") '20210531 Format วันสุดท้ายของเดือนที่ User ค้นหา     

        '1 เดือนก่อนหน้า ที่ User ค้นหา
        Dim startMonth As Date = dateCont.strToDateTime(DateNow & "01", "yyyyMMdd").AddMonths(-1) '20210401 วันที่ 1 ของเดือนก่อนหน้าจากเดือนที่ User ค้นหา  
        Dim endMonthDay1 As Date = startMonth.AddDays(-1) '20210331 
        Dim formatStartMonth As String = startMonth.ToString("yyyyMMdd") '20210401  format วันที่ของ 1 เดือนก่อนหน้าจากเดือนที่ User ค้นหา 



        Dim c8 As String = VarIni.C8
        Dim flaName As New ArrayList From {
               "TA001",
               "TA002",
               "TA004",
               "TA028",
               "TA029" & c8 & "VAT",
               "TA028 + TA029" & c8 & "TotalOC",
               "TA037",
               "TA038" & c8 & "VAT_BC",
               "TA037 + TA038" & c8 & "TotalBC",
               "TA034"
            }

        Dim sqlStr As New SQLString("ACPTA", flaName, strSplit:=c8)
        With sqlStr

            .SetWhere(.WHERE_EQUAL("TA024", "Y"), True)
            .SetWhere(.WHERE_EQUAL("TA001", txtInvType))
            .SetWhere(.WHERE_EQUAL("TA002", txtInvNo))

            '.SetWhere(.WHERE_BETWEEN("TA034", formatStartMonth, formatendMonth))


        End With

        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString, VarIni.ERP, dbConn.WhoCalledMe())

        With dt.Rows(0)
            Dim dateFld As String = .Item("TA034")
            Dim dateFldT As Date = dateCont.strToDateTime(dateFld, "yyyyMMdd")

            Dim dateCnt As Integer = DateDiff(DateInterval.Month, startMonth, endMonth1)
            Dim dateCnt1 As Integer = DateDiff(DateInterval.Month, dateFldT, startMonth)


            If txtInvType.Text = String.Empty And txtInvNo.Text = String.Empty Then
                show_message.ShowMessage(Page, "Please Purchase Invoice Type/No.   ", UpdatePanel1)
                gvshow.Visible = False
                gvshow.DataBind()
            ElseIf dateCnt1 > dateCnt Then
                show_message.ShowMessage(Page, "There is a scope to search for historical data for 1 month.   ", UpdatePanel1)
            Else
                blInvType.Text = .Item("TA001")
                blInvNo.Text = .Item("TA002")
                blSup.Text = .Item("TA004")
                blAmountOC.Text = .Item("TA028")
                blTaxOC.Text = .Item("VAT")
                blTotalOC.Text = .Item("TotalOC")
                blAmountBC.Text = .Item("TA037")
                blTaxBC.Text = .Item("VAT_BC")
                blTotalBC.Text = .Item("TotalBC")
                btPlusBC.Visible = True
                btMinusBC.Visible = True
                btUpdate.Visible = True
            End If
        End With

    End Sub

    Protected Sub btMinusBC_Click(sender As Object, e As EventArgs) Handles btMinusBC.Click
        Dim taxMinusBC As Decimal = blTaxBC.Text
        taxMinusBC -= 0.01
        blTaxBC.Text = taxMinusBC
        blTaxOC.Text = blTaxBC.Text
    End Sub

    Protected Sub btPlusBC_Click(sender As Object, e As EventArgs) Handles btPlusBC.Click
        Dim taxPlusBC As Decimal = blTaxBC.Text
        taxPlusBC += 0.01
        blTaxBC.Text = taxPlusBC
        blTaxOC.Text = blTaxBC.Text
    End Sub

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click


        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("th-TH")
        Dim sdate As String = Date.Now.ToString("")
        Dim newdate As String = Date.Parse(sdate, format)
        Dim resultdate As String = Date.Now.ToString("yyyyMMdd HH:mm:ss")

        Dim VAT As String = String.Empty
        Dim VatBC As String = String.Empty

        Dim c8 As String = VarIni.C8
        Dim flaName As New ArrayList From {
               "TA001",
               "TA002",
               "TA004",
               "TA028",
               "TA029",
               "TA028 + TA029" & c8 & "TotalOC",
               "TA037",
               "TA038",
               "TA037 + TA038" & c8 & "TotalBC"
            }


        Dim sqlStr As New SQLString("ACPTA", flaName, strSplit:=c8)
        With sqlStr

            .SetWhere(.WHERE_EQUAL("TA024", "Y"), True)
            .SetWhere(.WHERE_EQUAL("TA001", txtInvType))
            .SetWhere(.WHERE_EQUAL("TA002", txtInvNo))


        End With

        Dim dtupdate As DataTable = dbConn.Query(sqlStr.GetSQLString, VarIni.ERP, dbConn.WhoCalledMe())

        For Each dr As DataRow In dtupdate.Rows
            Dim drC As New DataRowControl(dr)
            With drC

                VAT = .Text("TA029")
                VatBC = .Text("TA038")

                'Insert 
                Dim BeforeUpdate As New ArrayList From {
                    dbConn.getInsertSql("UpdateTax", New Hashtable From {
                           {"InvoiceType", blInvType.Text},
                           {"InvoiceNo", blInvNo.Text},
                           {"Supplier", blSup.Text.Trim},
                           {"Amount_OC", blAmountOC.Text},
                           {"Tax_OC", .Text("TA029")},
                           {"TotalAmount_OC", blTotalOC.Text},
                           {"Amount_BC", blAmountBC.Text},
                           {"Tax_BC", .Text("TA038")},
                           {"TotalAmount_BC", blTotalBC.Text},
                           {"Change_By", Session("UserID")},
                           {"ChangeDate", resultdate}
                   }, New Hashtable)
                       }

                'Update
                Dim AfterUpdate As New Hashtable From {
                           {"TA029", blTaxOC.Text},
                           {"TA038", blTaxBC.Text}
                        }
                Dim WhrUpdate As New Hashtable From {
                           {"TA001", .Text("TA001")},
                           {"TA002", .Text("TA002")},
                           {"TA004", .Text("TA004")},
                           {"TA028", .Text("TA028")},
                           {"TA037", .Text("TA037")}
                        }
                If VAT <> blTaxOC.Text Or VatBC <> blTaxBC.Text Then
                    dbConn.TransactionSQL(BeforeUpdate, VarIni.DBMIS, dbConn.WhoCalledMe)
                    Dim UpdateERP As String = dbConn.getUpdateSql("ACPTA", AfterUpdate, WhrUpdate)
                    dbConn.TransactionSQL(UpdateERP, VarIni.ERP, dbConn.WhoCalledMe)

                    DetailUpdate()
                End If
            End With

        Next
    End Sub



    Private Sub DetailUpdate()
        Dim Al As New ArrayList
        Dim FldName As New ArrayList
        Dim ColName As New ArrayList
        Dim FldNumber As New ArrayList
        Dim c8 As String = VarIni.C8

        With New ArrayListControl(Al)
            .TAL("TA001", "Invoice Type")
            .TAL("TA002", "Invoice No")
            .TAL("TA004", "SUPPLIER")
            .TAL("TA028", "AMOUNT(O/C)", "2")
            .TAL("T2.Tax_OC" & c8 & "TaxOC", "TAX(O/C)", "2")
            .TAL("TA028 + TA029" & c8 & "TotalOC", "Total Amount(O/C)", "2")
            .TAL("TA037", "AMOUNT(B/C)", "2")
            .TAL("T2.Tax_BC" & c8 & "TaxBC", "TAX(B/C)", "2")
            .TAL("TA037 + TA038" & c8 & "TotalBC", "Total Amount(B/C)", "2")
            .TAL("T2.Change_By" & c8 & "Change_By", "Change_By")
            .TAL("T2.ChangeDate" & c8 & "ChangeDate", "ChangeDate")

            FldName = .ChangeFormat(False)
            ColName = .ChangeFormat(True)
            FldNumber = .ChangeFormat()
        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(ColName, c8)

        Dim sqlStr As New SQLString("ACPTA", FldName, c8)
        With sqlStr

            .setLeftjoin("HOOTHAI_REPORT.dbo.UpdateTax T2", New List(Of String) From {
                       "TA001" & c8 & "T2.InvoiceType",
                       "TA002" & c8 & "T2.InvoiceNo"
                       }
             )

            .SetWhere(.WHERE_EQUAL("TA024", "Y"), True)
            .SetWhere(.WHERE_EQUAL("TA001", txtInvType))
            .SetWhere(.WHERE_EQUAL("TA002", txtInvNo))

            .SetOrderBy("T2.ChangeDate DESC")
        End With


        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString, VarIni.ERP, dbConn.WhoCalledMe())


        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, FldName, fldManual)
            End With
        Next

        gvCont.GridviewInitial(gvshow, ColName, strSplit:=c8)
        gvCont.ShowGridView(gvshow, dtShow)
        CountRow.RowCount = dtShow.Rows.Count
    End Sub


    Protected Sub btcancel_Click(sender As Object, e As EventArgs) Handles btcancel.Click
        txtInvType.Text = String.Empty
        txtInvNo.Text = String.Empty
        blInvType.Text = String.Empty
        blInvNo.Text = String.Empty
        blSup.Text = String.Empty
        blAmountOC.Text = String.Empty
        blTaxOC.Text = String.Empty
        blTotalOC.Text = String.Empty
        blAmountBC.Text = String.Empty
        blTaxBC.Text = String.Empty
        blTotalBC.Text = String.Empty
        btPlusBC.Visible = False
        btMinusBC.Visible = False
        btUpdate.Visible = False
        gvshow.DataSource = Nothing
        gvshow.DataBind()
        CountRow.RowCount = Nothing

    End Sub


End Class


'NOTE SQL
'Select Case TA001,
'TA002,
'TA004,
'TA028,
'T2.Tax_OC TaxOC,
'TA028 +TA029 TotalOC,
'TA037,
'T2.Tax_BC TaxBC,
'TA037 +TA038 TotalBC,
'T2.Change_By Chang_By,
'T2.ChangeDate As ChangDate 
'FROM ACPTA  
'LEFT JOIN  HOOTHAI_REPORT.dbo.UpdateTax T2  On  1=1   And TA001=T2.InvoiceType   And TA002=T2.InvoiceNo   
'WHERE  1 = 1 And TA024 = 'Y'   AND TA001 = '7101'   AND TA002 = '20210401001'   
'ORDER BY T2.ChangeDate DESC; 

'----------------------------------------------------------------
'เช็คข้อมูล ERP
'Select Case TA029, TA038 FROM ACPTA
'wHERE TA001 = '7101'   AND TA002 = '20210401001';

'--ค่าจริง
'update ACPTA Set TA029= '950.60',TA038= '950.60' where TA004='J001' and TA037='13580.00' and TA002='20210401001' and TA028='13580.00' and TA001='7101' 

'---------------------------------------------------------------------
'เก็บข้อมูลเดิมก่อนการอัพเดท
'Select Case* From UpdateTax;
'DELETE FROM UpdateTax;