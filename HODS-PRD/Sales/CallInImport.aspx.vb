Imports OfficeOpenXml
Imports System.IO
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class CallInImport
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim createTableSale As New createTableSale
    Dim ConfigDate As New ConfigDate


    Dim ddlcont As New DropDownListControl
    Dim fileCont As New FileControl
    Dim dbconn As New DataConnectControl
    Dim dtcont As New DataTableControl


    Const tableHead As String = "COPME"
    Const tableBody As String = "COPMF"

    Const table As String = "CodeInfo"
    Const codeHead As String = "PLANT"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            reset()
            ddlCust_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Protected Sub reset()
        ucDocDate.Text = Date.Today.ToString("yyyyMMdd")
        btSave.Visible = False
        'tbCustPO.Text = ""
        tbRemark.Text = ""
        Dim SQL As String = "select rtrim(MA001) MA001,rtrim(MA001)+':'+MA002 MA002 from COPMA where MA097 = '1' and substring(COPMA.UDF01,1,1)='Y' order by MA001 "
        ddlcont.showDDL(ddlCust, SQL, VarIni.ERP, "MA002", "MA001", False)

        SQL = "select Name from CodeInfo where CodeType='TIME_SHIP' order by Code "
        UcDdlTimeShip.show(SQL, VarIni.DBMIS, "Name", "Name", True)

        gvCustShow.DataSource = ""
        gvCustShow.DataBind()

        lbError.Text = ""

    End Sub

    Function getDocNo() As String
        Dim SQL As String,
            dt As DataTable = New DataTable
        Dim dateVal As String = ucDocDate.TextEmptyToday
        SQL = "select top 1 isnull(cast(max(ME001) as Decimal(20,0))+1,'" & dateVal & "001') ME001 from " & tableHead & " where  ME001 like'" & dateVal & "%' order by ME001 desc"
        Dim dr As DataRow = dbconn.QueryDataRow(SQL, VarIni.ERP, dbconn.WhoCalledMe)
        Dim drcont As New DataRowControl(dr)
        Return drcont.Text("ME001", dateVal & "001")
    End Function


    Protected Sub btUpload_Click(sender As Object, e As EventArgs) Handles btUpload.Click

        Dim dtExcleRead As DataTable = fileCont.ReadFileExcel(fuUpload)
        Dim dt = New DataTable
        dt.Columns.Add("Seq") '0
        dt.Columns.Add("Item") '1
        dt.Columns.Add("Desc") '2
        dt.Columns.Add("Spec") '3
        dt.Columns.Add("Qty", Type.GetType("System.Double")) '4
        dt.Columns.Add("Unit") '5
        dt.Columns.Add("Cust W/O") '6
        dt.Columns.Add("Cust Lint") '7
        dt.Columns.Add("Model") '8

        Dim line As Integer = 1
        Dim SQL As String
        For Each dr As DataRow In dtExcleRead.Rows
            With New DataRowControl(dr)
                Dim row = dt.NewRow
                row(1) = CStr(line) 'seq
                Dim item As String = "",
                    desc As String = "",
                    spec As String = .Text(0),
                    unit As String = "",
                    qty As Decimal = .Text(1)
                If qty <= 0 Then
                    Continue For
                End If
                If Not String.IsNullOrEmpty(spec) Then
                    'get basic info for spec
                    SQL = " select MB001,MB002,MB003,MB004 from INVMB " &
                          " where INVMB.MB005 <> '1501' and INVMB.MB109 = 'Y' " & dbconn.WHERE_LIKE("MB003", spec & "REV", False) & " order by MB001 desc "
                    With New DataRowControl(SQL, VarIni.ERP, dbconn.WhoCalledMe)
                        item = Trim(.Text("MB001"))
                        desc = Trim(.Text("MB002"))
                        unit = .Text("MB004")
                        spec = .Text("MB003")
                    End With
                End If

                row(0) = line.ToString("0000")
                row(1) = item 'Item
                row(2) = desc 'Desc
                row(3) = spec 'Spec
                row(4) = .Number(1) 'Qty
                row(5) = unit 'unit
                row(6) = .Text(2) 'wo
                row(7) = .Text(3) 'line
                row(8) = .Text(4) 'model
                dt.Rows.Add(row)
                line += 1
            End With
        Next

        getDocNo()
        gvCustShow.DataSource = dt
        gvCustShow.DataBind()

        If gvCustShow.Rows.Count > 0 Then 'And gvCustShow.Columns.Count > 3
            btSave.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollList", "gridviewScrollList();", True)
        End If

    End Sub

    Protected Sub ddlCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCust.SelectedIndexChanged
        ddlPlant.Items.Clear()
        Dim SQL As String = "select rtrim(Code) MD001,Code+':'+Name MD002 from " & table & " where CodeType='" & codeHead & "' and WC like '%" & ddlCust.Text.Trim & "%' order by Code "
        ControlForm.showDDL(ddlPlant, SQL, "MD002", "MD001", False, Conn_SQL.MIS_ConnectionString)
    End Sub


    Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        reset()
        ddlCust_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If gvCustShow.Rows.Count <= 0 Then
            show_message.ShowMessage(Page, "ไม่มีรายการบันทึก กรุณาตรวจสอบ(Not have data to record)", UpdatePanel1)
            Exit Sub
        End If


        Dim docNo As String = "",
            strSQL As String = "",
            docDate As String = Date.Today.ToString("yyyyMMdd"),
            cBy As String = "H" & Session("UserName"),
            cDay As String = DateTime.Now.ToString("yyyyMMddHHmmss"),
            custCode As String = ddlCust.Text.Trim,
            custPlant As String = ddlPlant.Text.Trim,
            SQL As String = "",
            chkExist As New ArrayList,
            dt As DataTable = New DataTable,
            txtError As String = ""

        Dim callInNo As String = getDocNo() ' tbCustPO.Text.Trim

        For i As Integer = 0 To gvCustShow.Rows.Count - 1
            With gvCustShow.Rows(i)
                Dim item As String = .Cells(1).Text.Trim.Replace("&nbsp;", "")
                Dim custwo As String = .Cells(6).Text.Trim.Replace("&nbsp;", "")
                'Dim custPO As String = .Cells(1).Text.Trim
                Dim valCheck As String = item & custwo
                If chkExist.Count > 0 And chkExist.Contains(valCheck) Then
                    show_message.ShowMessage(Page, "item=" & item & " is repeat(มีรายการซ้ำกัน)", UpdatePanel1)
                    Exit Sub
                End If
                chkExist.Add(valCheck)
                If valCheck = "" Then
                    show_message.ShowMessage(Page, "spec=" & .Cells(3).Text.Trim.Replace("&nbsp;", "") & " and w/o=" & custwo & " is empty on line " & i + 1 & "(ไม่มีรหัสสินค้านี้ รายการที่ " & i + 1 & " กรุณาตรวจสอบอีกครั้ง)", UpdatePanel1)
                    Exit Sub
                End If

            End With
        Next

        Dim fld As Hashtable
        Dim whr As Hashtable

        ''Head
        HeadRecord()


        'Dim dNow As String = Date.Now.ToString("yyyyMMdd")
        Dim sqlList As New ArrayList
        'body
        For Each gr As GridViewRow In gvCustShow.Rows
            With New GridviewRowControl(gr)
                whr = New Hashtable From {
                    {"MF001", LbCallinNo.Text.Trim},
                    {"MF002", .Text(0).Trim.Replace("&nbsp;", "")}
                }

                fld = New Hashtable From {
                    {"COMPANY", Conn_SQL.DBMain},
                    {"CREATOR", cBy},
                    {"USR_GROUP", "HODS"},
                    {"CREATE_DATE", cDay},
                    {"FLAG", "3"},
                    {"MF003", .Text(1)}, 'item
                    {"MF004", .Text(2)}, 'desc
                    {"MF005", .Text(3)}, 'spec
                    {"MF006", ucDocDate.TextEmptyToday}, 'date
                    {"MF007", "2101"}, 'warehouse code
                    {"MF008", .Text(4)}, 'qty
                    {"MF010", .Text(5)}, 'unit
                    {"MF011", "THB"},'currency
                    {"UDF01", .Text(6)},
                    {"UDF02", .Text(7)},
                    {"UDF03", .Text(8)}
                }
                sqlList.Add(dbconn.getInsertSql(tableBody, fld, whr, "I"))
            End With
        Next

        If sqlList.Count > 0 Then
            If dbconn.TransactionSQL(sqlList, VarIni.ERP, dbconn.WhoCalledMe) > 0 Then
                show_message.ShowMessage(Page, "บันทึกเรียบร้อย เลขที่ " & LbCallinNo.Text & "(Record Complete)", UpdatePanel1)
                reset()
            End If
        End If
    End Sub

    Sub HeadRecord()
        LbCallinNo.Text = getDocNo()

        Dim docDate As String = Date.Today.ToString("yyyyMMdd"),
            cBy As String = "H" & Session("UserName"),
            cDay As String = DateTime.Now.ToString("yyyyMMddHHmmss"),
            custCode As String = ddlCust.Text.Trim,
            custPlant As String = ddlPlant.Text.Trim,
            chkExist As New ArrayList,
            dt As DataTable = New DataTable

        Dim fld As Hashtable
        Dim whr As Hashtable

        'Head
        whr = New Hashtable From {
            {"ME001", LbCallinNo.Text}
        }

        fld = New Hashtable From {
            {"COMPANY", Conn_SQL.DBMain},
            {"CREATOR", cBy},
            {"USR_GROUP", "HODS"},
            {"CREATE_DATE", cDay},
            {"FLAG", "3"},
            {"ME002", custCode},
            {"ME006", "A01"},
            {"ME008", "N"},
            {"ME009", tbRemark.Text.Trim},
            {"ME013", "HT"},
            {"UDF01", ddlPlant.Text.Trim},
            {"UDF02", UcDdlTimeShip.Text2}
        }
        Dim ISQL As String = dbconn.GetSQL(tableHead, fld, whr, "II")
        If dbconn.TransactionSQL(ISQL, VarIni.ERP, dbconn.WhoCalledMe) = 0 Then
            HeadRecord()
        End If
    End Sub

End Class