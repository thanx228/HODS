Imports OfficeOpenXml
Imports System.IO
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class CustPORecord
    Inherits System.Web.UI.Page

    Const tablePO_Head As String = "CustPOInfo"
    'Const tablePO_Body As String = "CustPODetail"

    Const table As String = "CodeInfo"
    Const codeHead As String = "PLANT"

    Dim dbconn As New DataConnectControl
    Dim ddlcont As New DropDownListControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Session("UserName")) Then
                Dim createTableSale As New createTableSale
                createTableSale.createCustPOInfo()
                createTableSale.createCustPODetail()
                UcDdlCustID.setAutoPostback()
                reset(sender, e)
                TcMain.ActiveTabIndex = 1
                TcMain_ActiveTabChanged(sender, e)
            End If
        End If
    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click

        Dim WHR As String = ""
        WHR &= dbconn.WHERE_EQUAL("Cust", tbCust)
        WHR &= dbconn.WHERE_EQUAL("Plant", UcDdlPlantSearch.getObject)
        WHR &= dbconn.WHERE_LIKE("CustPO", tbCustPO)
        WHR &= dbconn.WHERE_BETWEEN("DocDate", ucDateFrom.dateVal, ucDateTo.dateVal)

        Dim fldName As New ArrayList

        'From {
        '    "CustPO ",
        '    "DocDate",
        '    "Cust",
        '    "Plant",
        '    "StatusInfo",
        '    "CreateBy",
        '    "CreateDate",
        '    "ChangeBy",
        '    "ChangeDate",
        '    "COUNT_ITEM",
        '    "CLOSE_ITEM"
        '}

        Dim al As New ArrayList
        Dim colName As List(Of String)
        With New ArrayListControl(al)
            .TAL("CustPO", "Cust PO")
            .TAL("DocDate", "Doc Date")
            .TAL("Cust", "Cust.")
            .TAL("Plant", "Plant")
            .TAL("SUM_QTY", "Qty", 0)
            .TAL("SUM_AMT", "Amount", 2)
            .TAL("COUNT_ITEM", "Item", 0)
            .TAL("CLOSE_ITEM", "Close Bal Item", 0)
            .TAL("StatusInfo", "Status")
            .TAL("CreateBy", "Create By")
            .TAL("CreateDate", "Create Date")
            .TAL("ChangeBy", "Change By")
            .TAL("ChangeDate", "Change Date")
            fldName = .ChangeFormat
            colName = .ChangeFormatList(True)
        End With

        Dim strSQL As New SQLString(tablePO_Head, fldName)
        With strSQL
            fldName = New ArrayList From {
                "CustPOD",
                "count(*)" & VarIni.C8 & "COUNT_ITEM",
                "sum(Qty)" & VarIni.C8 & "SUM_QTY",
                "sum(round(Qty*Price,2))" & VarIni.C8 & "SUM_AMT",
                "sum(case when Qty-QtyVoid>0 and PO_BAL_USAGE=0 then 1 else 0 end )" & VarIni.C8 & "CLOSE_ITEM"
            }
            Dim strSQL1 As New SQLString("V_CUST_PO_BALANCE", fldName)
            strSQL1.SetGroupBy("CustPOD")

            .setLeftjoin(strSQL1.GetSubQuery("CUST_PO_SUM"), New List(Of String) From {"CustPOD" & VarIni.C8 & "CustPO"})
            .SetWhere(WHR, True)
            .SetOrderBy("CustPO")
        End With

        With New GridviewControl(gvShow)
            .Format()
            .AddButtonField("Edit", ButtonType.Image)
            .AddTemplateField(,, "hlShow")
            .AddBoundField(colName)
            .ShowGridView(strSQL.GetSQLString, VarIni.DBMIS)
            ucCount.RowCount = .rowGridview(gvShow)
        End With
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub

    Function sqlPlantInfo(Optional custCode As String = "")
        Return "select distinct rtrim(Code) Code,rtrim(Code)+':'+rtrim(Name) Name from " & table & " where CodeType='" & codeHead & "' " & dbconn.WHERE_LIKE("WC", custCode) & " order by Code "
    End Function

    Protected Sub reset(sender As Object, e As EventArgs)
        tbCust.Text = ""
        tbCustPO.Text = ""
        ucDateTo.Text = ""
        ucDateFrom.Text = ""

        UcDdlPlantSearch.show(sqlPlantInfo, VarIni.DBMIS, "Name", "Code", True)
        gvShow.DataSource = ""
        gvShow.DataBind()
    End Sub

    Private Sub gvShow_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShow.RowCommand
        Dim i As Integer = e.CommandArgument
        With New GridviewRowControl(gvShow.Rows(i))
            Dim custPO As String = .Text(2).Replace(" ", "")
            Dim docDate As String = .Text(3).Replace(" ", "")
            Dim cust As String = .Text(4).Replace(" ", "")
            Dim plant As String = .Text(5).Replace(" ", "")
            Dim docStatus As String = .Text(6).Replace(" ", "")

            If e.CommandName = "OnEdit" Then
                btNew_Click(sender, e)
                lbCustPO.Visible = True
                TbCustPORecord.Visible = False

                lbCustPO.Text = custPO
                TbCustPORecord.Text = custPO
                UcDdlCustID.Text = cust
                uCDate.Text = docDate
                UcDdlPlant.Text = plant
                UcDdlStatus.Text = docStatus
                'get datafrom db
                Dim SQL As String = "select Remark from " & tablePO_Head & " where CustPO='" & custPO & "'"
                Dim remark As String = ""
                With New DataRowControl(SQL, VarIni.DBMIS, dbconn.WhoCalledMe)
                    remark = .Text("Remark")
                End With
                TbRemark.Text = remark

                TcMain_ActiveTabChanged(sender, e)

                System.Threading.Thread.Sleep(1000)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
            End If
        End With
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hlShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("CustPO")) Then
                    Dim link As String = "&custpo= " & .DataItem("CustPO").ToString.Trim
                    hplDetail.NavigateUrl = "CustPORecordPopup.aspx?height=150&width=350&custpo=" & .DataItem("CustPO").ToString.Trim
                    hplDetail.Attributes.Add("title", .DataItem("CustPO"))
                    .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                    .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
                End If
            End If
        End With
    End Sub

    Protected Sub btSaveHead_Click(sender As Object, e As EventArgs) Handles btSave.Click
        'check before save data
        'new and cust po is empty
        If String.IsNullOrEmpty(lbCustPO.Text) And String.IsNullOrEmpty(TbCustPORecord.Text) Then
            show_message.ShowMessage(Page, "Cust PO ว่าง กรุณาตรวจสอบ", UpdatePanel1)
            Exit Sub
        End If

        'cust
        If UcDdlCustID.Text = "0" Then
            show_message.ShowMessage(Page, "รหัสลูกค้า กรุณาตรวจสอบ", UpdatePanel1)
            Exit Sub
        End If
        'plant
        If UcDdlPlant.Text = "0" Then
            show_message.ShowMessage(Page, "Plant กรุณาตรวจสอบ", UpdatePanel1)
            Exit Sub
        End If
        'status
        If UcDdlStatus.Text = "0" Then
            show_message.ShowMessage(Page, "Status กรุณาตรวจสอบ", UpdatePanel1)
            Exit Sub
        End If

        Dim fldName As New ArrayList From {
            "DocDate",
            "Cust",
            "Plant",
            "Remark",
            "StatusInfo"
        }
        Dim strSQL As New SQLString(tablePO_Head, fldName)
        strSQL.SetWhere(dbconn.WHERE_EQUAL("CustPO", TbCustPORecord), True)
        Dim dr As DataRow = dbconn.QueryDataRow(strSQL.GetSQLString, VarIni.DBMIS, dbconn.WhoCalledMe)
        If dr IsNot Nothing Then
            If String.IsNullOrEmpty(lbCustPO.Text) Then 'insert
                show_message.ShowMessage(Page, "PO ซ้ำ กรุณาตรวจสอบ", UpdatePanel1)
                Exit Sub
            Else 'update
                With New DataRowControl(dr)
                    If UcDdlPlant.Text = .Text("Plant") And
                        UcDdlStatus.Text = .Text("StatusInfo") And
                        UcDdlCustID.Text = .Text("Cust") And
                        TbRemark.Text = .Text("Remark") Then
                        show_message.ShowMessage(Page, "ไม่มีการเปลี่ยนแปลง ไม่จำเป็นต้องบันทึก กรุณาตรวจสอบ", UpdatePanel1)
                        UcDdlPlant.Focus()
                        Exit Sub
                    End If
                End With
            End If
        Else

        End If
        Dim sqlList As New ArrayList,
            dtVal As String = DateTime.Now.ToString("yyyyMMddhhmmss"),
            CustPo As String = If(String.IsNullOrEmpty(lbCustPO.Text.Trim), TbCustPORecord.Text, lbCustPO.Text).Trim
        'update data head 
        Dim whr As Hashtable = New Hashtable From {
            {"CustPO", CustPo}
        }
        Dim statusVal As String = UcDdlStatus.Text

        Dim sqlType As String = "U"
        Dim fldUser As String = "ChangeBy"
        Dim fldDate As String = "ChangeDate"
        If String.IsNullOrEmpty(lbCustPO.Text) Then
            sqlType = "I"
            fldUser = "CreateBy"
            fldDate = "CreateDate"
        End If


        Dim fld As Hashtable = New Hashtable From {
            {"DocDate", uCDate.TextEmptyToday},
            {"Cust", UcDdlCustID.Text},
            {"Plant", UcDdlPlant.Text},
            {"StatusInfo", statusVal},
            {"Remark:N", TbRemark.Text},
            {fldUser, Session("UserName")},
            {fldDate, DateTime.Now.ToString("yyyyMMddhhmmss")}
        }
        sqlList.Add(dbconn.GetSQL(tablePO_Head, fld, whr, sqlType))

        Dim rowEffect As Integer = 0
        If sqlList.Count > 0 Then
            rowEffect = dbconn.TransactionSQL(sqlList, VarIni.DBMIS, dbconn.WhoCalledMe)
        End If
        show_message.ShowMessage(Page, "Transaction Completed(ปรับปรุงรายการเรียบร้อย)", UpdatePanel1)

        tbCustPO.Text = CustPo
        btNew_Click(sender, e)
        btSearch_Click(sender, e)

    End Sub

    Protected Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click

        TcMain.ActiveTabIndex = 0
        TbCustPORecord.Text = ""
        TbCustPORecord.Visible = True
        lbCustPO.Text = ""
        lbCustPO.Visible = False
        uCDate.text = ""
        TbRemark.Text = ""

        Dim SQL As String = "select rtrim(Code) Code,rtrim(Code)+':'+rtrim(Name) Name from " & table & " where CodeType='PO_STATUS'  order by Code "
        UcDdlStatus.show(SQL, VarIni.DBMIS, "Name", "Code", True)

        UcDdlStatus.Text = 1

        SQL = "select rtrim(MA001) MA001,rtrim(MA001)+':'+MA002 MA002 from COPMA where MA097 = '1' and substring(COPMA.UDF01,1,1)='Y' order by MA001 "
        UcDdlCustID.show(SQL, VarIni.ERP, "MA002", "MA001", True)
        UcDdlCustID_SelectedIndexChanged(sender, e)

    End Sub

    Private Sub UcDdlCustID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UcDdlCustID.SelectedIndexChanged
        UcDdlPlant.show(sqlPlantInfo(UcDdlCustID.Text), VarIni.DBMIS, "Name", "Code", True)
    End Sub



    Private Sub TcMain_ActiveTabChanged(sender As Object, e As EventArgs) Handles TcMain.ActiveTabChanged
        Dim tabRecord As Boolean = False
        Dim tabSearch As Boolean = True

        If TcMain.ActiveTabIndex = 0 Then 'record
            tabRecord = True
            tabSearch = False
        End If

        btSave.Visible = tabRecord
        btNew.Visible = tabRecord
        BtSearch.Visible = tabSearch
    End Sub

End Class