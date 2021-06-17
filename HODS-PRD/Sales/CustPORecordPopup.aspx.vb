Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class CustPORecordPopup
    Inherits System.Web.UI.Page

    Const tablePO_Head As String = "CustPOInfo"
    Const tablePO_Body As String = "CustPODetail"
    Const table As String = "CodeInfo"
    Const codeHead As String = "PLANT"
    Dim dbconn As New DataConnectControl
    Dim outcont As New OutputControl
    Dim gvcont As New GridviewControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btNew_Click(sender, e)
            TcMain.ActiveTabIndex = 0
            LbItemRecord.Visible = False
        End If
    End Sub

    Protected Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'head
        LbCustPO.Text = Request.QueryString("custpo").ToString.Trim
        TbRemark.Text = ""
        Dim SQL As String = "select rtrim(Code) Code,rtrim(Code)+':'+rtrim(Name) Name from " & table & " where CodeType='PO_STATUS'  order by Code "
        UcDdlStatus.show(SQL, VarIni.DBMIS, "Name", "Code", False)

        ShowCustomerPO(Nothing)

        LbDocDate.Text = ""
        LbCustCode.Text = ""
        LbCustName.Text = ""
        LbPlant.Text = ""
        LbRemarkHead.Text = ""
        TbSpec.Text = ""

        'get cust po head
        Dim fldName As New ArrayList From {
            "DocDate",
            "Cust",
            "MA002",
            "Plant",
            "StatusInfo",
            "Remark"
        }
        Dim strSQL As New SQLString(tablePO_Head, fldName)
        strSQL.setLeftjoin(VarIni.ERP & "..COPMA", New List(Of String) From {"MA001" & VarIni.C8 & "Cust"})
        strSQL.SetWhere(dbconn.WHERE_EQUAL("CustPO", LbCustPO.Text), True)
        With New DataRowControl(strSQL.GetSQLString, VarIni.DBMIS, dbconn.WhoCalledMe)
            LbDocDate.Text = .Text("DocDate")
            LbCustCode.Text = .Text("Cust")
            LbCustName.Text = .Text("MA002")
            LbPlant.Text = .Text("Plant")
            LbRemarkHead.Text = .Text("Remark")
            LbStatus.Text = .Text("StatusInfo")
        End With
        'detail
        With TbSpec
            TbSpec.Text = ""
            .Visible = True
        End With
        With LbSpec
            .Text = ""
            .Visible = False
        End With
        LbItem.Text = ""

        TbQty.Text = 0
        TbVoidQty.Text = 0
        TbVoidQty.ReadOnly = True
        LbInvoiceQty.Text = 0
        LbBalQty.Text = 0
        LbPrice.Text = 0

        Dim showButtom As Boolean = True
        If LbStatus.Text = "3" Then
            showButtom = False
        End If
        btSave.Visible = showButtom
        btNew.Visible = showButtom

        'show gridview
        Dim al As New ArrayList
        Dim colName As List(Of String)
        With New ArrayListControl(al)
            .TAL("Item", "Item")
            .TAL("MB003", "MB003")
            .TAL("Qty", "Qty", 0)
            .TAL("QtyVoid", "QtyVoid", 0)
            .TAL("TB022", "invoice qty", 0)
            .TAL("PO_BAL_USAGE" & VarIni.C8 & "BQTY", "PO Bal", 0)
            .TAL("Price", "Price", 3)
            .TAL("round(Qty*Price,2)" & VarIni.C8 & "Amount", "Amount", 2)
            .TAL("StatusDetail", "StatusDetail")
            .TAL("CreateByD", "CreateByD")
            .TAL("CreateDateD", "CreateDateD")
            .TAL("ChangeByD", "ChangeByD")
            .TAL("ChangeDateD", "ChangeDateD")
            fldName = .ChangeFormat()
            colName = .ChangeFormatList(True)
        End With

        Dim gvcont As New GridviewControl(gvShow)
        'gvcont.GridviewColWithCommand(colName, New List(Of String) From {"Edit" & VarIni.C8})
        gvcont.ShowGridView(colName, New List(Of String) From {"Edit" & VarIni.C8}, SQLCustPO(fldName, dbconn.WHERE_EQUAL("CustPOD", LbCustPO.Text.Trim)), VarIni.ERP)
        ucCountHead.RowCount = gvcont.rowGridview()

        BtUpdatePrice.Visible = gvShow.Rows.Count > 0
        gvcont.ShowGridView(GvInvoice, New DataTable)
        ShowButton()
        ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Function SQLCustPO(fldName As ArrayList, whr As String) As String
        Dim strSQL As New SQLString(VarIni.DBMIS & "..V_CUST_PO_BALANCE", fldName)
        With strSQL
            .setLeftjoin("INVMB", New List(Of String) From {"MB001" & VarIni.C8 & "Item"})
            .SetWhere(whr, True)
            .SetOrderBy("CustPOD,Item")
        End With
        Return strSQL.GetSQLString
    End Function



    'Function SQLCustPO(fldName As ArrayList, whr As String) As String
    '    Dim strSQL As New SQLString(tablePO_Body, fldName)
    '    strSQL.setLeftjoin(" left join " & VarIni.ERP & "..INVMB on MB001=Item ")
    '    Dim SQL As String = "select TH004,TH030,sum(TH008) TH008  from HOOTHAI..COPTH left join HOOTHAI..COPTG on TG001=TH001 and TG002=TH002 where TH030<>'' and TH008 >0 and TG004='" & LbCustCode.Text & "' group by TH004,TH030"

    '    SQL = "select ACRTB.TB039,ACRTB.TB048,sum(ACRTB.TB022) TB022 from HOOTHAI..ACRTB 
    '             left join HOOTHAI..ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002  
    '            where ACRTA.TA025 in ('N','Y') and ACRTB.TB048 is not null and rtrim(ACRTB.TB048)<>'' and ACRTA.TA004='" & LbCustCode.Text & "' 
    '         group by ACRTB.TB039,ACRTB.TB048"

    '    strSQL.setLeftjoin(" left join (" & SQL & ") SD on SD.TB039=Item and SD.TB048=CustPOD ")
    '    strSQL.SetWhere(whr, True)
    '    strSQL.SetOrderBy("CustPOD,Item")
    '    Return strSQL.GetSQLString
    'End Function

    'Protected Sub TbItem_TextChanged(sender As Object, e As EventArgs) Handles TbItem.TextChanged
    '    If String.IsNullOrEmpty(TbItem.Text) Then
    '        Exit Sub
    '        TbItem.Focus()
    '    End If
    '    getItem()
    'End Sub

    'Protected Sub TbSpec_TextChanged(sender As Object, e As EventArgs) Handles TbSpec.TextChanged
    '    If String.IsNullOrEmpty(TbSpec.Text) Then
    '        Exit Sub
    '        TbItem.Focus()
    '    End If
    'End Sub

    Private Sub gvShow_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShow.RowCommand
        Dim i As Integer = e.CommandArgument
        With New GridviewRowControl(gvShow.Rows(i))
            btNew_Click(sender, e)
            LbItemRecord.Text = .Text(1).Replace(" ", "")
            LbItem.Text = LbItemRecord.Text
            'LbItem.Visible = True

            TbSpec.Visible = False
            LbSpec.Visible = True

            LbSpec.Text = .Text(2).Replace(" ", "")
            TbQty.Text = .Number(3)
            TbVoidQty.Text = .Text(4)
            TbVoidQty.ReadOnly = False
            LbInvoiceQty.Text = .Text(5)
            LbBalQty.Text = .Text(6)
            LbPrice.Text = .Text(7).Replace(" ", "")
            UcDdlStatus.Text = .Text(8).Replace(" ", "")

            Dim SQL As String = "select RemarkD from " & tablePO_Body & " where  CustPOD='" & LbCustPO.Text & "' and Item='" & LbItemRecord.Text & "' "
            Dim remark As String = ""
            With New DataRowControl(SQL, VarIni.DBMIS, dbconn.WhoCalledMe)
                remark = .Text("RemarkD")
            End With
            TbRemark.Text = remark
            btSave.Visible = If(LbStatus.Text = "3", False, True)

            'show invoice
            Dim colName As ArrayList
            Dim fldName As ArrayList
            With New ArrayListControl(InvoiceInitail())
                colName = .ChangeFormat(True)
                fldName = .ChangeFormat()
            End With

            Dim whr As String = ""
            whr &= dbconn.WHERE_EQUAL("TB048", LbCustPO.Text)
            whr &= dbconn.WHERE_EQUAL("TB039", LbItemRecord.Text)
            SQL = InvoiceSQL(whr, fldName)

            gvcont.GridviewInitial(GvInvoice, colName, strSplit:=VarIni.C8)
            gvcont.ShowGridView(GvInvoice, SQL, VarIni.ERP)
            ShowButton()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End With
    End Sub
    Sub ShowButton()
        btNew.Visible = True
        'BtUpdatePrice.Visible = Not String.IsNullOrEmpty(LbItemRecord.Text.Trim)
        btSave.Visible = Not String.IsNullOrEmpty(LbItem.Text.Trim)
    End Sub

    Function InvoiceInitail() As ArrayList
        Dim al As New ArrayList
        With New ArrayListControl(al)
            .TAL("TA038", "Invoice Date") 'invoice date
            .TAL("TA004", "Cust Code") 'customer
            .TAL("ACRTA.UDF06" & VarIni.C8 & "UDF06", "Plant") 'plant
            .TAL("ACRTA.UDF08" & VarIni.C8 & "UDF08", "Time Ship") 'time sent
            .TAL("TA022", "Invoice Remark") 'remark
            .TAL("TB001", "Invoice Type") 'sale invoince no-seq
            .TAL("TB002", "Invoice No")
            .TAL("TB003", "Invoice Seq")
            .TAL("TB005", "Delivery Type") 'sale delivery no-seq
            .TAL("TB006", "Delivery No")
            .TAL("TB007", "Delivery Seq")
            .TAL("TB039", "Item") 'item
            .TAL("TB040", "Desc") 'desc
            .TAL("TB041", "Spec") 'spec
            .TAL("TB023", "Price", "3") 'price
            .TAL("TB022", "Qty", "0") 'qty
            .TAL("TB048", "Cust PO") 'po
            .TAL("TA025", "Approve Status") 'status
        End With
        Return al
    End Function

    Function InvoiceSQL(whr As String, fldName As ArrayList) As String
        Dim strSQL As New SQLString("ACRTB", fldName)
        strSQL.setLeftjoin(" left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002 ")
        strSQL.setLeftjoin(" left join COPMA on COPMA.MA001=TA004 ")
        whr &= strSQL.WHERE_IN("ACRTA.TA025", "N,Y",, True)
        strSQL.SetWhere(whr, True)
        strSQL.SetOrderBy("TB001,TB002,TB003")
        Return strSQL.GetSQLString
    End Function



    Protected Sub TbQty_TextChanged(sender As Object, e As EventArgs) Handles TbQty.TextChanged
        TbVoidQty_TextChanged(sender, e)
    End Sub

    Protected Sub TbVoidQty_TextChanged(sender As Object, e As EventArgs) Handles TbVoidQty.TextChanged
        Dim vQty As Decimal = outcont.checkNumberic(TbVoidQty)
        Dim qty As Decimal = outcont.checkNumberic(TbQty)
        Dim iQty As Decimal = outcont.checkNumberic(LbInvoiceQty.Text)
        If qty = 0 Then
            TbQty.Text = (vQty + iQty).ToString("N0")
            Exit Sub
        End If
        LbBalQty.Text = (qty - vQty - iQty).ToString("N0")
    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        'check before save data
        Dim poQty As Decimal = outcont.checkNumberic(TbQty.Text),
            voidQty As Decimal = outcont.checkNumberic(TbVoidQty.Text),
            useQty As Decimal = outcont.checkNumberic(LbInvoiceQty.Text)

        If poQty <= 0 Then
            show_message.ShowMessage(Page, "จำนวนต้องไม่เท่ากับค่าว่างหรือต้องเป็นตัวเลขเท่านั้น กรุณาตรวจสอบ", UpdatePanel1)
            TbQty.Focus()
            Exit Sub
        End If
        If voidQty < 0 Then
            show_message.ShowMessage(Page, "จำนวนต้องไม่เท่ากับค่าว่างหรือต้องเป็นตัวเลขเท่านั้น กรุณาตรวจสอบ", UpdatePanel1)
            TbVoidQty.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(LbItem.Text) Then
            show_message.ShowMessage(Page, "พาร์ทงานไม่พบในระบบ กรุณาตรวจสอบ", UpdatePanel1)
            TbSpec.Focus()
            Exit Sub
        End If

        'get data befor save
        Dim SQL As String
        If Not String.IsNullOrEmpty(LbItemRecord.Text) Then
            SQL = "select Qty,QtyVoid,RemarkD,StatusDetail from CustPODetail where CustPOD='" & LbCustPO.Text & "' and Item='" & LbItemRecord.Text & "' "
            Dim dr As DataRow = dbconn.QueryDataRow(SQL, VarIni.DBMIS, dbconn.WhoCalledMe)

            With New DataRowControl(dr)
                If poQty = .Number("Qty") And
                    voidQty = .Number("QtyVoid") And
                    UcDdlStatus.Text = .Text("StatusDetail") And
                    TbRemark.Text.Trim = .Text("RemarkD") Then
                    show_message.ShowMessage(Page, "ไม่มีการเปลี่ยนแปลง กรุณาตรวจสอบ", UpdatePanel1)
                    TbQty.Focus()
                    Exit Sub
                End If
            End With
            'Else
            '    'check exist data
            '    If dr IsNot Nothing Then
            '        show_message.ShowMessage(Page, "รหัสงานนี้มีอยู่แล้ว", UpdatePanel1)
            '        'TbItem.Focus()
            '        Exit Sub
            '    End If

        End If

        If poQty < voidQty + useQty Then
            show_message.ShowMessage(Page, "จำนวนใน PO มีน้อยกว่า จำนวนที่ถูกยกเลิกรวมกับจำนวนที่ใช้ไปแล้ว กรุณาตรวจสอบ", UpdatePanel1)
            TbQty.Focus()
        End If

        'Dim bStatus As String = UcDdlStatus.Text

        Dim sqlList As New ArrayList
        Dim item As String = LbItem.Text.Trim
        'update data head 
        Dim whr As Hashtable = New Hashtable From {
            {"CustPOD", LbCustPO.Text},
            {"Item", LbItem.Text.Trim}
        }
        Dim statusVal As String = UcDdlStatus.Text

        Dim sqlType As String = "U"
        Dim fldUser As String = "ChangeByD"
        Dim fldDate As String = "ChangeDateD"
        If String.IsNullOrEmpty(LbItemRecord.Text) Then
            sqlType = "I"
            fldUser = "CreateByD"
            fldDate = "CreateDateD"
        End If
        Dim fld As Hashtable = New Hashtable From {
            {"Price", outcont.checkNumberic(LbPrice.Text)},
            {"Qty", poQty},
            {"QtyVoid", voidQty},
            {"StatusDetail", UcDdlStatus.Text},
            {"RemarkD:N", TbRemark.Text},
            {fldUser, Session("UserName")},
            {fldDate, DateTime.Now.ToString("yyyyMMddhhmmss")}
        }
        sqlList.Add(dbconn.GetSQL(tablePO_Body, fld, whr, sqlType))

        Dim rowEffect As Integer = dbconn.TransactionSQL(sqlList, VarIni.DBMIS, dbconn.WhoCalledMe)
        Dim msg As String = "not record ไม่มีรายการปรับปรุง"
        If rowEffect > 0 Then
            msg = "Transaction Completed(ปรับปรุงรายการเรียบร้อย)"
            btNew_Click(sender, e)
        End If
        show_message.ShowMessage(Page, msg, UpdatePanel1)
    End Sub

    Protected Sub TbSpec_TextChanged(sender As Object, e As EventArgs) Handles TbSpec.TextChanged
        'get item from INVMB

        Dim strSQL As New SQLString("INVMB", New ArrayList From {"MB001"}, , 1)
        With strSQL
            Dim whr As String = ""
            whr &= .WHERE_EQUAL("INVMB.MB109", "Y")
            whr &= .WHERE_EQUAL("INVMB.MB025", "M")
            whr &= .WHERE_EQUAL("INVMB.MB003", TbSpec)
            .SetWhere(whr, True)
            .SetOrderBy("INVMB.MB001")
        End With
        'set show follow item
        ShowCustomerPO(Nothing)

        With New DataRowControl(strSQL.GetSQLString, VarIni.ERP, dbconn.WhoCalledMe)
            LbItem.Text = .Text("MB001")
            getItem(LbItem.Text)
        End With
        ShowButton()
    End Sub

    Sub ShowCustomerPO(dr As DataRow)
        With New DataRowControl(dr)
            LbItemRecord.Text = .Text("Item")
            'LbItem.Text = ""
            LbSpec.Text = .Text("MB003")
            'TbSpec.Text = .Text("MB003")
            TbQty.Text = .Number("Qty").ToString("N0")
            TbVoidQty.Text = .Number("QtyVoid").ToString("N0")
            LbInvoiceQty.Text = .Number("TB022").ToString("N0")
            LbBalQty.Text = .Number("BQTY").ToString("N0")
            LbPrice.Text = .Number("Price").ToString("N5")
            TbRemark.Text = .Text("RemarkD")
            UcDdlStatus.Text = .Text("StatusDetail")
        End With
    End Sub

    Sub getItem(item As String)

        Dim SQL As String
        Dim whr As String = ""
        'check data for exist

        Dim fldName As New ArrayList
        Dim al As New ArrayList
        With New ArrayListControl(al)
            .TAL("Item", "Item")
            .TAL("MB003", "MB003")
            .TAL("Qty", "Qty")
            .TAL("QtyVoid", "QtyVoid")
            .TAL("TB022", "invoice qty")
            .TAL("PO_BAL_USAGE" & VarIni.C8 & "BQTY", "PO Bal")
            .TAL("Price", "Price")
            .TAL("StatusDetail", "StatusDetail")
            .TAL("RemarkD", "RemarkD")
            fldName = .ChangeFormat()
        End With
        whr &= dbconn.WHERE_EQUAL("CustPOD", LbCustPO.Text)
        whr &= dbconn.WHERE_EQUAL("Item", LbItem.Text, fullFormat:=True)
        'SQL = SQLCustPO(fldName, whr)
        Dim dr As DataRow = dbconn.QueryDataRow(SQLCustPO(fldName, whr), VarIni.ERP, dbconn.WhoCalledMe)
        Dim showSave As Boolean = False
        If dr IsNot Nothing Then 'get data record
            showSave = True
            ShowCustomerPO(dr)
        Else 'find new item
            ShowCustomerPO(Nothing)
            'Dim SQL1 As String '= "select sum(TH008) TH008  from HOOTHAI..COPTH left join HOOTHAI..COPTG on TG001=TH001 and TG002=TH002 where TH030<>'' and TH008 >0 and TG004='" & LbCustCode.Text & "' and TH004='" & TbItem.Text & "' and TH030='" & LbCustPO.Text & "' "

            'SQL1 = "select sum(ACRTB.TB022) TB022 from HOOTHAI..ACRTB 
            '          left join HOOTHAI..ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002  
            '         where ACRTA.TA025 in ('N','Y') and ACRTB.TB048 is not null and rtrim(ACRTB.TB048)<>'' and 
            '               ACRTA.TA004='" & LbCustCode.Text & "' and  ACRTB.TB039='" & item & "' and ACRTB.TB048 ='" & LbCustPO.Text & "'"

            'SQL = " select INV.MB001,INV.MB002,INV.MB003,INV.MB004,INV.MB017,isnull(COP.MB008,0) MB008,isnull((" & SQL1 & "),0) TB022 from INVMB INV " &
            '      " left join COPMB COP on COP.MB002=INV.MB001 and COP.MB001='" & LbCustCode.Text.Trim & "' and COP.MB017<='" & Date.Now.ToString("yyyyMMdd") & "' " &
            '      " where INV.MB001 = '" & item & "' order by COP.MB017 desc "

            Dim spec As String = ""
            Dim price As Decimal = 0
            Dim invoiceQty As Decimal = 0

            With New DataRowControl(PriceDatarow(item))
                spec = .Text("MB003")
                price = .Number("MB008")
                invoiceQty = .Number("TB022")
                showSave = True
            End With
            LbSpec.Text = spec
            LbPrice.Text = price.ToString("N5")
            LbInvoiceQty.Text = invoiceQty.ToString("N0")
            If LbStatus.Text = "3" Then
                showSave = False
            End If

        End If
        If Not String.IsNullOrEmpty(LbItemRecord.Text) Then
            TbSpec.Visible = False
            LbSpec.Visible = True
        End If
        btSave.Visible = showSave
    End Sub

    Function PriceDatarow(item As String, Optional InvcQty As Boolean = True) As DataRow
        Dim SQL1 As String '= "select sum(TH008) TH008  from HOOTHAI..COPTH left join HOOTHAI..COPTG on TG001=TH001 and TG002=TH002 where TH030<>'' and TH008 >0 and TG004='" & LbCustCode.Text & "' and TH004='" & TbItem.Text & "' and TH030='" & LbCustPO.Text & "' "
        Dim SQL As String
        'Dim item As String = LbItemRecord.Text
        Dim fldName As New ArrayList From {
            "INVMB.MB001" & VarIni.C8 & "MB001",
            "INVMB.MB002" & VarIni.C8 & "MB002",
            "INVMB.MB003" & VarIni.C8 & "MB003",
            "INVMB.MB004" & VarIni.C8 & "MB004",
            "INVMB.MB017" & VarIni.C8 & "MB017",
            "isnull(COPMB.MB008,0)" & VarIni.C8 & "MB008"
       }
        Dim cust_code As String = LbCustCode.Text
        If InvcQty Then
            SQL1 = "select sum(ACRTB.TB022) TB022 from HOOTHAI..ACRTB 
                      left join HOOTHAI..ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002  
                     where ACRTA.TA025 in ('N','Y') and ACRTB.TB048 is not null and rtrim(ACRTB.TB048)<>'' and 
                           ACRTA.TA004='" & cust_code & "' and  ACRTB.TB039='" & item & "' and ACRTB.TB048 ='" & LbCustPO.Text & "'"
            fldName.Add("isnull((" & SQL1 & "),0)" & VarIni.C8 & "TB022")
        End If

        Dim strSQL As New SQLString("INVMB", fldName,, 1)
        With strSQL
            .setLeftjoin(" left join COPMB on COPMB.MB002=INVMB.MB001 and COPMB.MB001='" & cust_code & "' and COPMB.MB017<='" & Date.Now.ToString("yyyyMMdd") & "' ")
            .SetWhere(" and INVMB.MB001 = '" & item & "'")
            .SetOrderBy("COPMB.MB017 desc")
        End With
        'SQL = " select INV.MB001,INV.MB002,INV.MB003,INV.MB004,INV.MB017,isnull(COP.MB008,0) MB008,isnull((" & SQL1 & "),0) TB022 from INVMB INV " &
        '      " left join COPMB COP on COP.MB002=INV.MB001 and COP.MB001='" & cust_code & "' and COP.MB017<='" & Date.Now.ToString("yyyyMMdd") & "' " &
        '      " where INV.MB001 = '" & item & "' order by COP.MB017 desc "
        Return dbconn.QueryDataRow(strSQL.GetSQLString, VarIni.ERP, dbconn.WhoCalledMe)
    End Function

    Private Sub BtUpdatePrice_Click(sender As Object, e As EventArgs) Handles BtUpdatePrice.Click
        Dim al As New ArrayList
        Dim fldName As ArrayList
        With New ArrayListControl(al)
            .TAL("Item", "Item")
            .TAL("Price", "Price")
            fldName = .ChangeFormat()
        End With

        Dim dt As DataTable = dbconn.Query(SQLCustPO(fldName, dbconn.WHERE_EQUAL("CustPOD", LbCustPO.Text.Trim)), VarIni.ERP, dbconn.WhoCalledMe)
        If dt.Rows.Count = 0 Then
            show_message.ShowMessage(Page, "No Data for Update(ไม่มีข้อมูลต้องปรับปรุง)", UpdatePanel1)
            Exit Sub
        End If
        Dim sqlList As New ArrayList
        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim item As String = .Text("Item")
                Dim priceOld As Decimal = .Number("Price")
                With New DataRowControl(PriceDatarow(item, False))
                    Dim priceGet As Decimal = .Number("MB008")
                    If priceOld = priceGet Then
                        Continue For
                    Else
                        'update price
                        Dim fldHash As New Hashtable From {
                            {"Price", priceGet},
                            {"ChangeByD", Session("UserName")},
                            {"ChangeDateD", DateTime.Now.ToString("yyyyMMddhhmmss")}
                        }
                        Dim whrHash As New Hashtable From {
                            {"CustPOD", LbCustPO.Text.Trim},
                            {"Item", item}
                        }
                        sqlList.Add(dbconn.getUpdateSql(tablePO_Body, fldHash, whrHash))
                    End If
                End With
            End With
        Next
        Dim rowEffect As Integer = dbconn.TransactionSQL(sqlList, VarIni.DBMIS, dbconn.WhoCalledMe)
        Dim msg As String = "not record ไม่มีรายการปรับปรุง"
        If rowEffect > 0 Then
            msg = "Price Change " & rowEffect.ToString("N0") & " Rows Completed(ปรับปรุงราคาเรียบร้อย)"
            btNew_Click(sender, e)
        End If
        show_message.ShowMessage(Page, msg, UpdatePanel1)
    End Sub
End Class