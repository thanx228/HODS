Imports OfficeOpenXml
Imports System.IO
Imports MIS_HTI.DataControl
Imports ClosedXML.Excel
Imports MIS_HTI.FormControl

Public Class CustPOImport
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim createTableSale As New createTableSale
    Dim ConfigDate As New ConfigDate

    Const tablePO_Head As String = "CustPOInfo"
    Const tablePO_Body As String = "CustPODetail"

    Const table As String = "CodeInfo"
    Const codeHead As String = "PLANT"

    Dim dbconn As New DataConnectControl
    Dim hashcont As New HashtableControl
    Dim filecont As New FileControl
    Dim gvcont As New GridviewControl
    Dim ddlCont As New DropDownListControl


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            createTableSale.createCustPOInfo()
            createTableSale.createCustPODetail()

            reset()
            ddlCust_SelectedIndexChanged(sender, e)
            'ucHeaderForm.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub

    Protected Sub reset()
        'lbDate.Text = ConfigDate.dateShow2(Date.Today.ToString("yyyyMMdd"), "/")
        btSave.Visible = False
        tbRemark.Text = ""
        Dim SQL As String = "select rtrim(MA001) MA001,rtrim(MA001)+':'+MA002 MA002 from COPMA where MA097 = '1' and substring(COPMA.UDF01,1,1)='Y' order by MA001 "
        ddlCont.showDDL(ddlCust, SQL, VarIni.ERP, "MA002", "MA001", False)
        gvCustShow.DataSource = ""
        gvCustShow.DataBind()
        lbError.Text = ""
    End Sub

    Protected Sub btUpload_Click(sender As Object, e As EventArgs) Handles btUpload.Click
        'check CUST PO EXIST
        Dim CustPoHash As New Hashtable
        If String.IsNullOrEmpty(TbCustPO.Text) Then
            show_message.ShowMessage(Page, "Cust PO in empty", UpdatePanel1)
            Exit Sub
        Else
            Dim fldName As New ArrayList From {
                "Item"
            }
            Dim strsql As New SQLString(tablePO_Body, fldName)
            strsql.SetWhere(dbconn.WHERE_EQUAL("CustPOD", TbCustPO), True)
            Dim dt As DataTable = dbconn.Query(strsql.GetSQLString, VarIni.DBMIS, dbconn.WhoCalledMe)
            For Each dr As DataRow In dt.Rows
                With New DataRowControl(dr)
                    hashcont.addDataHash(CustPoHash, .Text("Item"), .Text("Item"))
                End With
            Next
        End If

        'check CUST PO EXIST
        Dim dtExcleRead As DataTable = filecont.ReadFileExcel(fuUpload)
        If dtExcleRead.Rows.Count > 0 Then
            'spec A
            'Cust PO Qty B
            Dim dt = New DataTable
            dt.Columns.Add("Seq") '0
            dt.Columns.Add("Item") '1
            dt.Columns.Add("Desc") '2
            dt.Columns.Add("Spec") '3
            dt.Columns.Add("Cust PO") '4
            dt.Columns.Add("Qty") '5
            dt.Columns.Add("Unit") '6
            dt.Columns.Add("Price") '7
            dt.Columns.Add("Amout") '8
            Dim line As Integer = 1
            Dim SQL As String,
                dateToday As String = Date.Now.ToString("yyyyMMdd")

            For Each dr As DataRow In dtExcleRead.Rows
                With New DataRowControl(dr)
                    Dim spec As String = .Text(0)
                    If Not String.IsNullOrEmpty(spec) Then
                        'get basic info for spec
                        SQL = " select TOP 1 INV.MB001,INV.MB002,INV.MB003,INV.MB004,INV.MB017,isnull(COP.MB008,0) MB008 from INVMB INV " &
                              " left join COPMB COP on COP.MB002=INV.MB001 and COP.MB001='" & ddlCust.Text.Trim & "' and COP.MB017<='" & dateToday & "' " &
                              " where 1=1  " & dbconn.WHERE_LIKE("INV.MB003", spec & "REV", False) & " order by COP.MB017 desc "
                        Dim qty As Decimal = .Number(1)
                        With New DataRowControl(SQL, VarIni.ERP, dbconn.WhoCalledMe)
                            Dim item As String = .Text("MB001")
                            If Not hashcont.existDataHash(CustPoHash, item) Then
                                Dim row = dt.NewRow
                                row(0) = CStr(line) 'seq
                                row(1) = .Text("MB001") 'Item
                                row(2) = .Text("MB002") 'Desc
                                row(3) = .Text("MB003") 'Spec
                                row(4) = Trim(TbCustPO.Text.Trim) 'Cust PO
                                row(5) = qty 'Qty
                                row(6) = .Text("MB004") 'unit
                                Dim price As Decimal = .Number("MB008")
                                row(7) = price 'price
                                row(8) = Math.Round(qty * price, 2) 'amount
                                dt.Rows.Add(row)
                                line += 1
                            End If

                        End With
                    End If
                End With
            Next
            gvcont.ShowGridView(gvCustShow, dt)
            btSave.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollList", "gridviewScrollList();", True)
        End If
    End Sub

    Protected Sub ddlCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCust.SelectedIndexChanged
        ddlPlant.Items.Clear()
        Dim SQL As String = "select rtrim(Code) MD001,Code+':'+Name MD002 from " & table & " where CodeType='" & codeHead & "' and WC like '%" & ddlCust.Text.Trim & "%' order by Code "
        ddlCont.showDDL(ddlPlant, SQL, VarIni.DBMIS, "MD002", "MD001", False)
    End Sub

    
    Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        reset()
        ddlCust_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Dim strSQL As String = "",
            docDate As String = UcDate.textEmptyToday,
            cBy As String = Session("UserName"),
            cDay As String = DateTime.Today.ToString("yyyyMMdd hhmmss"),
            custCode As String = ddlCust.Text.Trim,
            custPlant As String = ddlPlant.Text.Trim,
            SQL As String = "",
            chkExist As New ArrayList,
            dt As DataTable = New DataTable,
            txtError As String = ""
        Dim custPO As String = TbCustPO.Text.Trim
        SQL = " select Item from " & tablePO_Body & " where CustPOD='" & custPO & "' "
        Dim ItemList As ArrayList = dbconn.QueryA(SQL, VarIni.DBMIS, 0, dbconn.WhoCalledMe)
        For i As Integer = 0 To gvCustShow.Rows.Count - 1
            With gvCustShow.Rows(i)
                Dim item As String = .Cells(1).Text.Trim
                If chkExist.Count > 0 And chkExist.Contains(item) Then
                    show_message.ShowMessage(Page, "Item=" & item & " is repeat(มีรายการซ้ำกัน)", UpdatePanel1)
                    .ForeColor = System.Drawing.Color.Red
                    Exit Sub
                End If
                chkExist.Add(item)
                If ItemList.Count > 0 And ItemList.Contains(item) Then
                    txtError = item & ",<br>"
                End If
            End With
        Next

        If Not String.IsNullOrEmpty(txtError) Then
            lbError.Text = txtError
            Exit Sub
        End If

        Dim SQLList As New ArrayList
        Dim fld As Hashtable
        Dim whr As Hashtable
        'Head
        whr = New Hashtable From {
            {"CustPO", custPO}
        }
        fld = New Hashtable From {
            {"DocDate", docDate},
            {"Cust", custCode},
            {"Plant", custPlant},
            {"Remark:N", tbRemark.Text.Trim},
            {"CreateBy", cBy},
            {"CreateDate", cDay}
        }
        SQLList.Add(dbconn.GetSQL(tablePO_Head, fld, whr, "I"))
        'body
        For i As Integer = 0 To gvCustShow.Rows.Count - 1
            With gvCustShow.Rows(i)
                whr = New Hashtable From {
                    {"CustPOD", custPO},
                    {"Item", .Cells(1).Text.Trim}
                }
                fld = New Hashtable From {
                    {"Price", .Cells(7).Text.Trim},
                    {"Qty", .Cells(5).Text.Trim},
                    {"RemarkD:N", tbRemark.Text.Trim},
                    {"CreateByD", cBy},
                    {"CreateDateD", cDay}
                }
                SQLList.Add(dbconn.GetSQL(tablePO_Body, fld, whr, "I"))
            End With
        Next

        If SQLList.Count > 0 Then
            Dim rowEffect As Integer = dbconn.TransactionSQL(SQLList, VarIni.DBMIS, dbconn.WhoCalledMe)
            Dim txt As String = "no record please check again"
            If rowEffect > 0 Then
                txt = "บันทึกเรียบร้อยจำนวน " & rowEffect & " รายการ (Record Complete)"
                reset()
            End If
            show_message.ShowMessage(Page, txt, UpdatePanel1)
        End If


    End Sub
End Class