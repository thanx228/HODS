Public Class SaleDelSelectPO
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim createTableSale As New createTableSale
    Dim ConfigDate As New ConfigDate
    Const strDate As String = "20160701"
    Const table As String = "CodeInfo"
    'Const codeHead As String = "PLANT"
    Const codeTime As String = "TIME_SHIP"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            reset()
            updatePO()
        End If

    End Sub

    Sub updatePO()
        Dim SQL As String = " select TA004,ACRTA.UDF06,TB048,TB039,sum(TB022) from ACRTB left join ACRTA on TA001=TB001 and TA002=TB002" & _
            " where TA003>= '" & strDate & "' and ACRTA.UDF06<>'' and TB048 <> '' and TA025 = 'Y' and TA004 in ('D004','D005') " & _
            "   and TA003 between '" & Date.Now.AddDays(-7).ToString("yyyyMMdd") & "' and '" & Date.Now.ToString("yyyyMMdd") & "' " & _
            " group by TB048,ACRTA.UDF06,TB039,TA004 having sum(TB022)>0 "

        Dim fld As Hashtable,
            whr As Hashtable,
            strSQL As String = "",
            cnt As Integer = 0

        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                If cnt = 50 Then
                    Conn_SQL.Exec_Sql(strSQL, Conn_SQL.ERP_ConnectionString)
                    cnt = 0
                    strSQL = ""
                End If

                fld = New Hashtable
                whr = New Hashtable

                Dim cust As String = .Item("TA004"),
                    plant As String = .Item("UDF06"),
                    poCode As String = .Item("TB048"),
                    item As String = .Item("TB039")

                'whr.Add("custCodeB", cust)
                'whr.Add("plantB", plant)
                whr.Add("MF001", poCode)
                whr.Add("MF003", item)

                SQL = " select sum(TB022) from ACRTB left join ACRTA on TA001=TB001 and TA002=TB002 " & _
                      " where TA003>= '" & strDate & "' and TA004='" & cust & "' and ACRTA.UDF06 ='" & plant & "' " & _
                      "   and TB048 = '" & poCode & "' and TB039='" & item & "' and TB048 <> '' and TA025 = 'Y' "
                Dim invQty As Decimal = Conn_SQL.Get_value(SQL, Conn_SQL.ERP_ConnectionString)
                fld.Add("UDF51", invQty)

                strSQL &= Conn_SQL.GetSQL("COPMF", fld, whr, "UU")
                cnt += 1

            End With
        Next
        If strSQL <> "" Then
            Conn_SQL.Exec_Sql(strSQL, Conn_SQL.ERP_ConnectionString)
        End If
    End Sub

    Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        reset()
    End Sub

    Sub reset()

        Dim SQL As String = "select rtrim(MQ001) MQ001,rtrim(MQ001)+':'+rtrim(MQ002) MQ002 from CMSMQ where UDF01='Yes'  order by MQ001 "
        ControlForm.showDDL(ddlType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)

        SQL = "select rtrim(Code) MD001,Code+':'+Name MD002 from " & Table & " where CodeType='" & codeTime & "' order by Code "
        ControlForm.showDDL(ddlShipTime, SQL, "MD002", "MD001", False, Conn_SQL.MIS_ConnectionString)

        tbNumber.Text = ""

        pnShow.Visible = False
        lbDate.Text = ""
        lbSO.Text = ""
        lbCust.Text = ""
        lbPlant.Text = ""
        lbTime.Text = ""

        MultiView1.Visible = False

        gvShow.DataSource = ""
        gvShow.DataBind()

        gvSel.DataSource = ""
        gvSel.DataBind()

        ucCount.RowCount = 0

        ucSaleDelType.setObject = "23"

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        If tbNumber.Text.Trim = "" Then
            show_message.ShowMessage(Page, "กรุณาใส่หมายเลข Sale Order ", UpdatePanel1)
            tbNumber.Focus()
            Exit Sub
        End If

        Dim SQL As String = "select TD003,TD004,TD005,TD006,TD007,TD008,TD009,TD008-TD009 TD0089,TD011,TD010,COPTD.UDF02,COPTD.UDF03,COPTD.UDF04,MC007,TD013 from COPTD " & _
                " left join COPTC on TC001=TD001 and TC002=TD002 " & _
                " left join INVMC on MC001=TD004 and MC002=TD007 " & _
                " where TD001='" & ddlType.Text.Trim & "' and TD002='" & tbNumber.Text.Trim & "' and  TC027 = 'Y' and TD016 = 'N' order by TD001 "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        ucCount.RowCount = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)

        If gvShow.Rows.Count > 0 Then
            pnShow.Visible = True
            MultiView1.Visible = True
            MultiView1.SetActiveView(ViewSearch)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

            SQL = "select TC003,TC004,UDF01,UDF02 from COPTC where TC001='" & ddlType.Text.Trim & "' and TC002='" & tbNumber.Text.Trim & "' "
            Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    lbDate.Text = Date.Now.ToString("dd/MM/yyyy")
                    ucShipDate.dateVal = lbDate.Text
                    lbSO.Text = ddlType.Text.Trim & "-" & tbNumber.Text.Trim
                    lbCust.Text = .Item("TC004").ToString.Trim
                    lbPlant.Text = .Item("UDF01").ToString.Trim
                    lbTime.Text = .Item("UDF02").ToString.Trim
                End With
            End If
        Else
            show_message.ShowMessage(Page, "หมายเลข Sale Order นี้ ไม่พบรายการ ", UpdatePanel1)
            tbNumber.Focus()
            Exit Sub
        End If

    End Sub

    Sub showPO(Optional stock As Boolean = False)
        Dim dt = New DataTable
        dt.Columns.Add("Seq") '0
        'dt.Columns.Add("SO") '1
        dt.Columns.Add("SO Seq") '2
        dt.Columns.Add("Item") '3
        dt.Columns.Add("Desc") '4
        dt.Columns.Add("Spec") '5
        dt.Columns.Add("WH") '6
        dt.Columns.Add("Bal Qty") '7
        dt.Columns.Add("Cust PO") '8
        dt.Columns.Add("Qty") '9
        dt.Columns.Add("Stock Qty") '10
        dt.Columns.Add("Price") '11
        dt.Columns.Add("Amout") '12
        dt.Columns.Add("Unit") '13
        dt.Columns.Add("Cust WO") '14
        dt.Columns.Add("Cust Line") '15
        dt.Columns.Add("Model") '16
        With gvShow
            Dim cntChk As Integer = 0,
                line As Integer = 1
            Dim poUse As New Dictionary(Of String, Decimal),
                stockOld As New Dictionary(Of String, Decimal),
                sumAmt As Decimal = 0,
                sumQty As Decimal = 0
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim cbsel As CheckBox = .FindControl("cbSel"),
                        tbQty As TextBox = .FindControl("tbQty"),
                        balQty As Decimal = CDec(.Cells(8).Text),
                        invQty As Decimal = CDec(.Cells(10).Text),
                        prc As Decimal = CDec(.Cells(11).Text)
                    Dim valQty As String = tbQty.Text.Trim
                    Dim inputQty As Decimal = If(valQty <> "" And IsNumeric(valQty), CDec(valQty), 0)
                    Dim item As String = .Cells(2).Text.Trim,
                        invOld As Decimal = 0

                    If stock Then
                        stockOld.TryGetValue(item, invOld)
                        invQty -= invOld
                    End If
                    If cbsel.Checked Or inputQty > 0 Or stock Then
                        Dim selQty As Decimal = 0
                        If stock Then
                            selQty = balQty
                            If invQty < balQty Then
                                selQty = invQty
                            End If
                            If stockOld.TryGetValue(item, invOld) Then
                                stockOld(item) = invOld + selQty
                            Else
                                stockOld.Add(item, selQty)
                            End If
                        Else
                            If inputQty > 0 Then
                                selQty = inputQty
                            Else
                                selQty = balQty
                                If invQty < balQty Then
                                    selQty = invQty
                                End If
                            End If
                        End If

                        If selQty > 0 Then
                            'select cust po
                            Dim SQL As String
                            '= "select B.custPO,poQty-voidQty-useQty  from CustPOBody B " & _
                            '  " left join CustPOHead H on H.docNo=B.docNo " & _
                            '  " where H.custCode='" & lbCust.Text.Trim & "' and H.plant='" & lbPlant.Text.Trim & "' and B.item='" & item & "' and B.status<3 and poQty-voidQty-useQty>0 " & _
                            '  " order by B.custPO"

                            SQL = " select ME001,COPMF.MF008-COPMF.UDF51-COPMF.UDF52 from COPMF  " & _
                                  " left join COPME on ME001=MF001 where  ME002='" & lbCust.Text.Trim & "' and " & _
                                  " COPME.UDF01='" & lbPlant.Text.Trim & "' and MF003='" & item & "'  and " & _
                                  " ME008 = 'N' and COPMF.MF008-COPMF.UDF51-COPMF.UDF52 >0 order by MF001 "

                            Dim dtSub As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                            If dtSub.Rows.Count > 0 Then
                                For j As Integer = 0 To dtSub.Rows.Count - 1
                                    Dim poBalQty As Decimal = dtSub.Rows(j).Item(1),
                                        custPO As String = Trim(dtSub.Rows(j).Item(0))
                                    Dim codePO As String = item & "-" & custPO
                                    Dim poUseOld As Decimal = 0
                                    poUse.TryGetValue(codePO, poUseOld)
                                    poBalQty -= poUseOld

                                    Do While selQty > 0 And poBalQty > 0
                                        Dim poQty As Decimal = selQty
                                        If poBalQty < selQty Then
                                            poQty = poBalQty
                                        End If
                                        addRowDatatable(gvShow.Rows(i), line, custPO, poQty, selQty, dt)
                                        sumAmt += (poQty * prc)
                                        sumQty += poQty
                                        line += 1
                                        poBalQty -= poQty
                                        selQty -= poQty
                                        'po
                                        If poUse.TryGetValue(codePO, poUseOld) Then
                                            poUse(codePO) = poUseOld + poQty
                                        Else
                                            poUse.Add(codePO, poQty)
                                        End If
                                    Loop
                                    If selQty = 0 Then
                                        Exit For
                                    End If
                                Next
                            Else
                                addRowDatatable(gvShow.Rows(i), line, "", selQty, selQty, dt)
                                sumAmt += (selQty * prc)
                                sumQty += selQty
                                line += 1
                            End If
                        End If
                        'cntChk += 1
                    End If
                End With
            Next
            If dt.Rows.Count = 0 Then
                show_message.ShowMessage(Page, If(stock, "ไม่มีรายการที่ต้องการ กรุณาตรวจสอบ", "ไม่ได้เลือกรายการ กรุณาเลือกรายการ "), UpdatePanel1)
                Exit Sub
            Else
                gvSel.DataSource = dt
                gvSel.DataBind()
                ucCount.RowCount = ControlForm.rowGridview(gvSel)
                pnShow.Visible = True
                MultiView1.Visible = True
                MultiView1.SetActiveView(ViewPO)

                lbAllAmt.Text = FormatNumber(sumAmt, 2, TriState.True)
                lbAllQty.Text = FormatNumber(sumQty, 0)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShowSel", "gridviewScrollShowSel();", True)
            End If
        End With
    End Sub

    Protected Sub btSelect_Click(sender As Object, e As EventArgs) Handles btSelect.Click
        showPO()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShowSel", "gridviewScrollShowSel();", True)
    End Sub

    Sub addRowDatatable(ds As GridViewRow, line As Integer, custPO As String, poQty As Decimal, selQty As Decimal, ByRef dt As DataTable)

        Dim valHash As Hashtable = New Hashtable
        With ds
            Dim i As Integer = 0
            valHash.Add(i, line) 'Seq
            'i += 1
            'valHash.Add(i, lbSO.Text.Trim) 'SO
            i += 1
            valHash.Add(i, .Cells(1).Text.Trim) 'SO Seq
            i += 1
            valHash.Add(i, .Cells(2).Text.Trim) 'Item
            i += 1
            valHash.Add(i, .Cells(3).Text.Trim) 'Desc
            i += 1
            valHash.Add(i, .Cells(4).Text.Trim) 'Spec
            i += 1
            valHash.Add(i, .Cells(5).Text.Trim) 'WH
            i += 1
            valHash.Add(i, FormatNumber(selQty, 0)) ' sel qty
            i += 1
            valHash.Add(i, custPO) 'Cust PO
            i += 1
            valHash.Add(i, FormatNumber(poQty, 0)) 'po Qty
            i += 1
            valHash.Add(i, FormatNumber(CDec(.Cells(10).Text.Trim), 0)) 'Stock Qty
            i += 1
            valHash.Add(i, FormatNumber(CDec(.Cells(11).Text.Trim), 3)) 'Price
            i += 1
            valHash.Add(i, FormatNumber(poQty * CDec(.Cells(11).Text.Trim), 2)) 'Amt
            i += 1
            valHash.Add(i, .Cells(12).Text.Trim) 'Unit
            i += 1
            valHash.Add(i, .Cells(14).Text.Trim.Replace("&nbsp;", "")) 'Cust WO
            i += 1
            valHash.Add(i, .Cells(15).Text.Trim.Replace("&nbsp;", "")) 'Cust Line
            i += 1
            valHash.Add(i, .Cells(16).Text.Trim.Replace("&nbsp;", "")) 'Model

        End With

        Dim row = dt.NewRow
        For Each seq As Integer In valHash.Keys
            row(seq) = valHash.Item(seq) 'seq
        Next
        dt.Rows.Add(row)
    End Sub
    
    Protected Sub btSelInven_Click(sender As Object, e As EventArgs) Handles btSelInven.Click
        showPO(True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShowSel", "gridviewScrollShowSel();", True)
    End Sub

    Protected Sub btBack_Click(sender As Object, e As EventArgs) Handles btBack.Click
        MultiView1.Visible = True
        MultiView1.SetActiveView(ViewSearch)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Function getDocNo() As String
        Dim SQL As String,
            dt As DataTable = New DataTable
        Dim dateVal As String = Date.Now.ToString("yyyyMMdd")
        SQL = "select isnull(cast(max(TG002) as Decimal(20,0))+1,'" & dateVal & "001') TG002 from COPTG where TG001='" & Trim(ucSaleDelType.getValue) & "' and TG042 ='" & dateVal & "' "
        Dim maxNo As String = Conn_SQL.Get_value(SQL, Conn_SQL.ERP_ConnectionString)
        Return maxNo
    End Function

    Function checkStrLimit(txt As String) As Boolean
        Dim charactersAllowed As String = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
        Dim letter As String

        For i As Integer = 0 To txt.Length - 1
            letter = txt.Substring(i, 1)
            If Not charactersAllowed.Contains(letter) Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        'check remark
        If tbRemark.Text.Trim <> "" And checkStrLimit(tbRemark.Text.Trim) Then
            show_message.ShowMessage(Page, "Remark for english and number only(ช่อง Remark ใส่ได้แค่ตัวอักษรอังกฤษกับตัวเลขเท่านั้น)!!", UpdatePanel1)
            tbRemark.Focus()
            Exit Sub
        End If

        'save Head
        headRecord()
        'save body
        BodyRecord()
        show_message.ShowMessage(Page, "SAVE DATA COMPLETED Sale Delivery Number=" & Trim(ucSaleDelType.getValue) & "-" & lbSelDocNo.Text.Trim & "!!", UpdatePanel1)
        btReset_Click(sender, e)

    End Sub

    Sub headRecord()
        lbSelDocNo.Text = getDocNo()

        Dim fld As Hashtable = New Hashtable,
            whr As Hashtable = New Hashtable

        Dim cust As String = lbCust.Text.Trim
        Dim docDate As String = Date.Now.ToString("yyyyMMdd")
        Dim SQL As String = "select MA083,MA014,MA038,MA025,MA026 from COPMA where MA001='" & cust & "' "

        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        If dt.Rows.Count > 0 Then
            whr.Add("TG001", ucSaleDelType.getValue)
            whr.Add("TG002", lbSelDocNo.Text.Trim)

            fld.Add("COMPANY", Conn_SQL.DBMain)
            fld.Add("CREATOR", "HODS-" & Session("UserName"))
            fld.Add("USR_GROUP", "HODS")
            fld.Add("CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss"))
            fld.Add("FLAG", "3")

            fld.Add("TG003", docDate) 'Delivery Date
            fld.Add("TG004", cust) 'Customer
            fld.Add("TG010", "HT") 'Plant
            fld.Add("TG012", "1") 'exchange rate

            With dt.Rows(0)
                fld.Add("TG011", Trim(.Item("MA014"))) 'Currency
                fld.Add("TG008", Trim(.Item("MA025"))) 'addres1
                fld.Add("TG009", Trim(.Item("MA026"))) 'addres2
                fld.Add("TG047", Trim(.Item("MA083"))) 'payment term
                fld.Add("TG017", Trim(.Item("MA038"))) 'Tax Type
            End With
            Dim amt As String = lbAllAmt.Text.Trim.Replace(",", "")

            fld.Add("TG013", amt) 'Delivery Amount(O/C)
            fld.Add("TG016", "A") 'Invoice Kind
            fld.Add("TG023", "N") 'Approval Indicator
            fld.Add("TG024", "N") 'Update Indicator
            fld.Add("TG031", "N") 'Cigarette & Liquor Remark
            fld.Add("TG033", lbAllQty.Text.Replace(",", "")) 'Total Quantity
            fld.Add("TG036", "N") 'Journalized(Revenue)
            fld.Add("TG037", "N") 'Journalized(Cost)
            fld.Add("TG042", docDate) 'Document Date
            fld.Add("TG045", amt) 'E-Approval Status
            fld.Add("TG055", "N") 'Journalized(Revenue)
            fld.Add("TG059", "N") 'Post Status
            fld.Add("TG068", "N") 'EBCExport Indicator
            fld.Add("TG074", "N") 'Unlimited release
            fld.Add("UDF01", ConfigDate.dateShow2(If(ucShipDate.dateVal = "", docDate, ucShipDate.dateVal), "/")) 'ship date
            fld.Add("UDF02", lbPlant.Text) 'Plant
            fld.Add("UDF03", Trim(ddlShipTime.Text)) 'ship time

            Dim rowEffect As Integer = Conn_SQL.exeSQL(Conn_SQL.GetSQL("COPTG", fld, whr, "II"), Conn_SQL.ERP_ConnectionString)
            If rowEffect = 0 Then
                headRecord()
            End If

        End If

    End Sub

    Sub BodyRecord()
        Dim whr As Hashtable,
            fld As Hashtable,
            strSQL As String = ""

        With gvSel

            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    whr = New Hashtable
                    whr.Add("TH001", Trim(ucSaleDelType.getValue)) 'type
                    whr.Add("TH002", lbSelDocNo.Text.Trim) 'no
                    whr.Add("TH003", returnLine(.Cells(0).Text.Trim)) 'seq

                    fld = New Hashtable
                    fld.Add("COMPANY", Conn_SQL.DBMain)
                    fld.Add("CREATOR", "HODS-" & Session("UserName"))
                    fld.Add("USR_GROUP", "HODS")
                    fld.Add("CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss"))
                    fld.Add("FLAG", "13")

                    Dim so() As String = lbSO.Text.Trim.Split("-")
                    fld.Add("TH014", If(so.Length = 1, so(0), "")) 'SO Type
                    fld.Add("TH015", If(so.Length = 2, so(1), "")) 'SO
                    fld.Add("TH016", .Cells(1).Text.Trim) 'SO Seq
                    fld.Add("TH004", .Cells(2).Text.Trim) 'Item
                    fld.Add("TH005", .Cells(3).Text.Trim) 'Desc
                    fld.Add("TH006", .Cells(4).Text.Trim) 'Spec
                    fld.Add("TH007", .Cells(5).Text.Trim) 'WH
                    fld.Add("TH030", .Cells(7).Text.Trim) 'Cust PO
                    fld.Add("TH008", .Cells(8).Text.Trim) 'Qty
                    fld.Add("TH012", .Cells(10).Text.Trim) 'Price
                    Dim amt As Decimal = CDec(.Cells(11).Text.Trim.Replace(",", ""))
                    fld.Add("TH013", amt) 'amt
                    fld.Add("TH035", amt) 'amt
                    fld.Add("TH037", amt) 'amt
                    fld.Add("TH009", .Cells(12).Text.Trim) 'Unit
                    fld.Add("UDF01", .Cells(13).Text.Trim) 'Cust WO
                    fld.Add("UDF02", .Cells(14).Text.Trim) 'Cust Line
                    fld.Add("UDF03", .Cells(15).Text.Trim) 'Model
                    fld.Add("TH017", "********************") 'Lot
                    fld.Add("TH020", "N") 'Approval Indicator
                    fld.Add("TH021", "N") 'Update Indicator
                    fld.Add("TH025", "1") 'Discount Rate
                    fld.Add("TH026", "N") 'Code bill
                    fld.Add("TH031", "1") 'Type
                    fld.Add("TH056", "##########") 'Bin
                    fld.Add("TH064", "N") '
                    fld.Add("THH01", "N") '
                    strSQL &= Conn_SQL.GetSQL("COPTH", fld, whr, "I")

                End With
            Next
            If strSQL <> "" Then
                Conn_SQL.Exec_Sql(strSQL, Conn_SQL.ERP_ConnectionString)
            End If
        End With

    End Sub

    Function returnLine(line As String) As String
        line = "000" & line
        Return Right(line, 4)
    End Function

End Class