Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class PriceExpiry
    Inherits System.Web.UI.Page

    Dim chkCont As New CheckBoxListControl
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim expCont As New ExportImportControl
    Dim gvCont As New GridviewControl

    Dim C8 As String = VarIni.C8
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = String.Empty Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            ShowCheck()
        End If
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        ShowCheck()
    End Sub

    Private Sub ShowCheck()

        cbItemOut.Visible = False
        Dim sql As String = String.Empty
        If ddlType.SelectedValue = "0" Then
            sql = "select MQ001 code_name, MQ001 +' : '+ MQ002 show_name from CMSMQ where COMPANY = 'HOOTAI' and  MQ003 = '32' order by MQ001"
        ElseIf ddlType.SelectedValue = "1" Then
            sql = "select MQ001 code_name, MQ001 +' : '+ MQ002 show_name from CMSMQ where COMPANY = 'HOOTAI' and  MQ001 = '5B01'"
        ElseIf ddlType.SelectedValue = "2" Then
            sql = "select MQ001 code_name , MQ001 +' : '+ MQ002 show_name  from CMSMQ where MQ003 in ('51','52') order by MQ001"
        ElseIf ddlType.SelectedValue = "3" Then
            cbItemOut.Visible = True
            sql = "select LTRIM(RTRIM(MA002)) code_name , LTRIM(RTRIM(MA002)) +' : '+ MA003 show_name from INVMA where MA011 = 'Y' order by MA002"
        End If
        chkCont.showCheckboxList(cblDocType, sql, VarIni.ERP, "show_name", "code_name", 5)

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        If ddlType.SelectedValue = "0" Then
            PriceApprovalOrder(False)
        ElseIf ddlType.SelectedValue = "1" Then
            PriceApprovalOrderSubContract(False)
        ElseIf ddlType.SelectedValue = "2" Then
            MoOutsource(False)
        ElseIf ddlType.SelectedValue = "3" Then
            ItemExpiry(False)
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        If ddlType.SelectedValue = "0" Then
            PriceApprovalOrder(True)
        ElseIf ddlType.SelectedValue = "1" Then
            PriceApprovalOrderSubContract(True)
        ElseIf ddlType.SelectedValue = "2" Then
            MoOutsource(True)
        ElseIf ddlType.SelectedValue = "3" Then
            ItemExpiry(True)
        End If
    End Sub

    Private Sub PriceApprovalOrder(excel As Boolean)

        Dim arlAll As New ArrayList
        Dim arlFeild As New ArrayList
        Dim arlColumn As New ArrayList
        Dim arlNumber As New ArrayList
        Dim dateDiff As String = "(DATEDIFF(DAY, CONVERT(datetime, GETDATE()), CONVERT(datetime, TM015)))"
        With New ArrayListControl(arlAll)
            .TAL("TM001 +'-'+ TM002" & C8 & "docno", "Doc No")
            .TAL("TM003", "Seq")
            .TAL("TL004", "Supplier")
            .TAL("MA002", "Supplier Name")
            .TAL("TM004", "Item")
            .TAL("TM005", "Item Name")
            .TAL("TM006", "Item Spec")
            .TAL("TM009", "Price Unit")
            .TAL("TL003", "Price Approved Date")
            .TAL("TM014", "Effective Date")
            .TAL("TM015", "Expiry Date")
            .TAL("TM010", "Price", 2)
            .TAL("TL005", "Currency")
            .TAL("(case TL006 when 'Y' then 'Y : Approved' when 'N' then 'N : Not Approved' when 'U' then 'U : Approve failed ' when 'V' then 'V : Canceled' end)" & C8 & "appvored_ind", "Approval Indicator")
            .TAL(dateDiff & C8 & "date_diff", "Date Diff (Days)", 0)
            .TAL("(case  when " & dateDiff & " < 0 then 'Expire' else 'Not Expire' end)" & C8 & "status_price", "Status Price")
            arlFeild = .ChangeFormat(False)
            arlColumn = .ChangeFormat(True)
            arlNumber = .ColumnNumber()
        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(arlColumn, C8)

        Dim sqlStr As New SQLString("PURTM TM", arlFeild)
        With sqlStr
            .setLeftjoin("PURTL TL", New List(Of String) From {
                       "TL.COMPANY" & C8 & "TM.COMPANY",
                       "TL001" & C8 & "TM001",
                       "TL002" & C8 & "TM002"
                       }
             )
            .setLeftjoin("PURMA MA", New List(Of String) From {
                     "TL004" & C8 & "MA001"
                     }
            )
            .SetWhere(.WHERE_EQUAL("TM.COMPANY", "HOOTHAI"), True)
            .SetWhere(.WHERE_IN("TM001", cblDocType))
            .SetWhere(.WHERE_LIKE("TM002", tbDocNo))
            .SetWhere(.WHERE_DATE("TM015", ucDateFrom.Text, ucDateTo.Text))
            .SetWhere(.WHERE_LIKE("TM004", tbItem))
            .SetWhere(.WHERE_LIKE("TM005", tbItemDesc))
            .SetWhere(.WHERE_LIKE("TM006", tbItemSpec))
            .SetWhere(.WHERE_LIKE("TL004", tbSupCode))
            If ddlStusPrice.SelectedValue = "0" Then
                'All
            ElseIf ddlStusPrice.SelectedValue = "1" Then
                'Expire
                .SetWhere(.WHERE_EQUAL(dateDiff, 0, " < ", False))
            ElseIf ddlStusPrice.SelectedValue = "2" Then
                'Not Expire
                .SetWhere(.WHERE_EQUAL(dateDiff, 0, " >= ", False))
            End If
            .SetOrderBy("TM001,TM002,TM003")
        End With

        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)
        Dim countRow As Integer = dt.Rows.Count
        If countRow > VarIni.LimitGridView Then
            If excel = False Then
                gvShow.DataSource = Nothing
                gvShow.DataBind()
                ucCountRow.RowCount = Nothing
                show_message.ShowMessage(Page, "ข้อมูลมากกว่า 500 Rows. ( " & countRow & " )\nกรุณาเลือก Excel.", UpdatePanel1)
                Exit Sub
            End If
        End If

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, arlNumber, fldManual)
            End With
        Next

        If excel = True Then
            expCont.ExportDatatable("PriceExpiry Approval Order", dtShow, arlColumn)
        Else
            gvCont.GridviewInitial(gvShow, arlColumn,,,,, C8)
            gvCont.ShowGridView(gvShow, dtShow)
            ucCountRow.RowCount = countRow
        End If


    End Sub

    Private Sub PriceApprovalOrderSubContract(excel As Boolean)

        Dim arlAll As New ArrayList
        Dim arlFeild As New ArrayList
        Dim arlColumn As New ArrayList
        Dim arlNumber As New ArrayList
        Dim dateDiff As String = "(DATEDIFF(DAY, CONVERT(datetime, GETDATE()), CONVERT(datetime, TN012)))"
        With New ArrayListControl(arlAll)
            .TAL("TM001 +'-'+ TM002" & C8 & "docno", "Doc No")
            .TAL("TN003", "Seq")
            .TAL("TM004", "Contractor")
            .TAL("MA002", "Contractor Name")
            .TAL("TN004", "Item")
            .TAL("TN005", "Item Name")
            .TAL("TN006", "Item Spec")
            .TAL("TN008", "Unit")
            .TAL("TN009", "Subc.Price", 2)
            .TAL("TM003", "Approved Date")
            .TAL("TN011", "Effective Date")
            .TAL("TN012", "Expiry Date")
            .TAL("TM005", "Currency")
            .TAL("(case TM009 when 'Y' then 'Y : Approved' when 'N' then 'N : Not Approved' when 'U' then 'U : Approve failed ' when 'V' then 'V : Canceled' end)" & C8 & "appvored_ind", "Approval Indicator")
            .TAL(dateDiff & C8 & "date_diff", "Date Diff (Days)", 0)
            .TAL("(case  when " & dateDiff & " < 0 then 'Expire' else 'Not Expire' end)" & C8 & "status_price", "Status Price")
            arlFeild = .ChangeFormat(False)
            arlColumn = .ChangeFormat(True)
            arlNumber = .ColumnNumber()
        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(arlColumn, C8)

        Dim sqlStr As New SQLString("MOCTN TN", arlFeild)
        With sqlStr
            .setLeftjoin("MOCTM TM", New List(Of String) From {
                       "TM.COMPANY" & C8 & "TN.COMPANY",
                       "TM001" & C8 & "TN001",
                       "TM002" & C8 & "TN002"
                       }
             )
            .setLeftjoin("PURMA MA", New List(Of String) From {
                     "TM004" & C8 & "MA001"
                     }
           )
            .SetWhere(.WHERE_EQUAL("TN.COMPANY", "HOOTHAI"), True)
            .SetWhere(.WHERE_IN("TN001", cblDocType))
            .SetWhere(.WHERE_LIKE("TM002", tbDocNo))
            .SetWhere(.WHERE_DATE("TN012", ucDateFrom.Text, ucDateTo.Text))
            .SetWhere(.WHERE_LIKE("TN004", tbItem))
            .SetWhere(.WHERE_LIKE("TN005", tbItemDesc))
            .SetWhere(.WHERE_LIKE("TN006", tbItemSpec))
            .SetWhere(.WHERE_LIKE("TM004", tbSupCode))

            If ddlStusPrice.SelectedValue = "0" Then
                'All
            ElseIf ddlStusPrice.SelectedValue = "1" Then
                'Expire
                .SetWhere(.WHERE_EQUAL(dateDiff, 0, " < ", False))
            ElseIf ddlStusPrice.SelectedValue = "2" Then
                'Not Expire
                .SetWhere(.WHERE_EQUAL(dateDiff, 0, " >= ", False))
            End If
            .SetOrderBy("TM001,TM002,TN003")
        End With

        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)
        Dim countRow As Integer = dt.Rows.Count
        If countRow > VarIni.LimitGridView Then
            If excel = False Then
                gvShow.DataSource = Nothing
                gvShow.DataBind()
                ucCountRow.RowCount = Nothing
                show_message.ShowMessage(Page, "ข้อมูลมากกว่า 500 Rows. ( " & countRow & " )\nกรุณาเลือก Excel.", UpdatePanel1)
                Exit Sub
            End If
        End If

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, arlNumber, fldManual)
            End With
        Next

        If excel = True Then
            expCont.ExportDatatable("PriceExpiry Subcontract", dtShow, arlColumn)
        Else
            gvCont.GridviewInitial(gvShow, arlColumn,,,,, C8)
            gvCont.ShowGridView(gvShow, dtShow)
            ucCountRow.RowCount = countRow
        End If

    End Sub

    Private Sub MoOutsource(excel As Boolean)

        Dim arlAll As New ArrayList
        Dim arlFeild As New ArrayList
        Dim arlColumn As New ArrayList
        Dim arlNumber As New ArrayList
        With New ArrayListControl(arlAll)
            .TAL("mo_doc_no", "Mo No")
            .TAL("mo_seq", "Mo Seq")
            .TAL("mo_stus", "Mo Status")
            .TAL("appvored_ind", "Approval Indicator")
            .TAL("mo_character", "Character")
            .TAL("price_app_no", "Price App No")
            .TAL("price_app_seq", "Price Seq")
            .TAL("item_mo", "Item")
            .TAL("item_desc", "Item Desc")
            .TAL("item_spec", "Item Spec")
            .TAL("item_price", "Price", "4")
            .TAL("item_unit", "Unit")
            .TAL("wc", "Work Center / Supplier")
            .TAL("oper", "Operation")
            .TAL("app_date", "Approved Date")
            .TAL("effe_date", "Effective Date")
            .TAL("exp_date", "Expire Date")
            '.TAL("contra_name", "Contractor Name")
            .TAL("date_diff", "Date Diff (Days)", "0")
            .TAL("status_price", "Status Price")

            arlFeild = .ChangeFormat(False)
            arlColumn = .ChangeFormat(True)
            arlNumber = .ColumnNumber()
        End With
        Dim dtShow As DataTable = dtCont.setColDatatable(arlColumn, C8)



        '////////////////////////////////////////////// Table One \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        Dim subSqlTbOne As String = String.Empty
        Dim arlSubTbOne As New ArrayList From {
             "SF.TA001",
             "SF.TA002",
             "SF.TA001 +'-'+ SF.TA002 mo_doc_no",
             "SF.TA003 mo_seq",
             "(case MO.TA011 when '3' then '3:Producing' else '' end) mo_stus",
             "(case SF.TA005 when '2' then '2 : Subcontract' else '' end) mo_character",
             "MO.TA006 item_mo",
             "MO.TA034 item_desc",
             "MO.TA035 item_spec",
             "SF.TA006 +' : '+ SF.TA007 wc",
             "SF.TA004 +' : '+ MW002 oper"
            }
        Dim sqlStrSubOne As New SQLString("SFCTA SF", arlSubTbOne)
        With sqlStrSubOne
            .setLeftjoin("MOCTA MO", New List(Of String) From {
                   "MO.COMPANY" & C8 & "SF.COMPANY",
                   "MO.TA001" & C8 & "SF.TA001",
                   "MO.TA002" & C8 & "SF.TA002"
                }
            )
            .setLeftjoin("CMSMW MW", New List(Of String) From {
                   "MW001" & C8 & "SF.TA004"
                }
            )
            .SetWhere(.WHERE_EQUAL("MO.COMPANY", "HOOTHAI"), True)
            .SetWhere(.WHERE_EQUAL("MO.TA011", "3"))
            .SetWhere(.WHERE_EQUAL("SF.TA005", "2"))
            subSqlTbOne = sqlStrSubOne.GetSQLString()
        End With
        '//////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        '////////////////////////////////////////////// Table Two \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        Dim subSqlTbTwo As String = String.Empty
        Dim arlSubTbTwo As New ArrayList From {
             "TN004",
             "TM001 +'-'+ TM002" & C8 & "price_app_no",
             "TN003" & C8 & "price_app_seq",
             "TM003" & C8 & "app_date",
             "TN011" & C8 & "effe_date",
             "TN012" & C8 & "exp_date",
             "TN009" & C8 & "item_price",
             "TN008" & C8 & "item_unit",
             "TM004",
             "TM004 +' : '+ MA002" & C8 & "contra_name",
             "(case TM009 when 'Y' then 'Y : Approved' when 'N' then 'N : Not Approved' when 'U' then 'U : Approve failed ' when 'V' then 'V : Canceled' end)" & C8 & "appvored_ind",
             "(DATEDIFF(DAY, CONVERT(datetime, GETDATE()),CONVERT(datetime, (case when TN012 = '' then GETDATE() else TN012 end))))" & C8 & "date_diff",
             "(case  when (DATEDIFF(DAY, CONVERT(datetime, GETDATE()),CONVERT(datetime, (case when TN012 = '' then GETDATE() else TN012 end)))) < 0 then 'Expire' when (DATEDIFF(DAY, CONVERT(datetime, GETDATE()),CONVERT(datetime, (case when TN012 = '' then GETDATE() else TN012 end)))) >= 0 then 'Not Expire' else 'Not Price' end)" & C8 & "status_price"
            }
        Dim sqlStrSubTwo As New SQLString("MOCTN TN", arlSubTbTwo)
        With sqlStrSubTwo
            .setLeftjoin("MOCTM TM", New List(Of String) From {
                   "TM001" & C8 & "TN001",
                   "TM002" & C8 & "TN002"
                }
            )
            .setLeftjoin("PURMA MA", New List(Of String) From {
                   "MA001" & C8 & "TM004"
                }
            )

            .SetWhere(.WHERE_EQUAL("1", "1",, False), True)
            .SetWhere(.WHERE_DATE("TN012", ucDateFrom.Text, ucDateTo.Text))
            subSqlTbTwo = sqlStrSubTwo.GetSQLString()
        End With
        '//////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


        Dim sqlStr As New SQLString(vbCrLf & VarIni.B1 & vbCrLf & subSqlTbOne & vbCrLf & VarIni.B2 & " ta1 " & vbCrLf, arlFeild)
        With sqlStr
            .setLeftjoin(vbCrLf & .addBracket(subSqlTbTwo) & " ta2 " & vbCrLf, New List(Of String) From {
                   "TN004" & C8 & "item_mo"
                 }
             )

            .SetWhere(.WHERE_EQUAL("1", "1",, False), True)

            .SetWhere(.WHERE_IN("TA001", cblDocType))
            .SetWhere(.WHERE_LIKE("TA002", tbDocNo))
            .SetWhere(.WHERE_LIKE("item_mo", tbItem))
            .SetWhere(.WHERE_LIKE("item_desc", tbItemDesc))
            .SetWhere(.WHERE_LIKE("item_spec", tbItemSpec))
            .SetWhere(.WHERE_LIKE("TM004", tbSupCode))

            If ddlStusPrice.SelectedValue = "0" Then
                'All
            ElseIf ddlStusPrice.SelectedValue = "1" Then
                'Expire
                .SetWhere(.WHERE_EQUAL("date_diff", 0, " < ", False))
            ElseIf ddlStusPrice.SelectedValue = "2" Then
                'Not Expire
                .SetWhere(.WHERE_EQUAL("date_diff", 0, " >= ", False))
            End If

            .SetOrderBy("mo_doc_no , mo_seq")
        End With

        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)
        Dim countRow As Integer = dt.Rows.Count
        If countRow > VarIni.LimitGridView Then
            If excel = False Then
                gvShow.DataSource = Nothing
                gvShow.DataBind()
                ucCountRow.RowCount = Nothing
                show_message.ShowMessage(Page, "ข้อมูลมากกว่า 500 Rows. ( " & countRow & " )\nกรุณาเลือก Excel.", UpdatePanel1)
                Exit Sub
            End If
        End If

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, arlNumber, fldManual)
            End With
        Next

        If excel = True Then
            expCont.ExportDatatable("PriceExpiry MoOutsource", dtShow, arlColumn)
        Else
            gvCont.GridviewInitial(gvShow, arlColumn,,,,, C8)
            gvCont.ShowGridView(gvShow, dtShow)
            ucCountRow.RowCount = countRow
        End If


    End Sub

    Private Sub ItemExpiry(excel As Boolean)

        Dim outSource As Boolean = cbItemOut.Checked

        Dim arlAll As New ArrayList
        Dim arlFeild As New ArrayList
        Dim arlColumn As New ArrayList
        Dim arlNumber As New ArrayList
        With New ArrayListControl(arlAll)
            If outSource Then
                .TAL("TN001 +'-'+ TN002" & C8 & "price_doc", "Price Doc No")
                .TAL("TN003", "Seq")
                .TAL("acc_name", "Item Type")
                .TAL("item", "Item")
                .TAL("item_desc", "Item Desc")
                .TAL("item_spec", "Item Spec")
                .TAL("approv_ind", "Approved Indicator")
                .TAL("unit", "Unit")
                .TAL("price", "Price", "4")
                .TAL("app_date", "Approved Date")
                .TAL("effe_date", "Effective Date")
                .TAL("exp_date", "Expire Date")
                .TAL("app_in_price", "Price Approved")
                .TAL("subcontract", "Subcontract")
                .TAL("oper", "Operation")
                .TAL("date_diff", "Date Diff (Days)", "0")
                .TAL("(case  when date_diff < 0 then 'Expire' when date_diff >= 0 then 'Not Expire' else 'Not Expire' end)" & C8 & "status_price", "Status Price")
            Else
                .TAL("TL001 +'-'+ TL002" & C8 & "price_doc", "Price Doc No")
                .TAL("TM003", "Seq")
                .TAL("acc_name", "Item Type")
                .TAL("item", "Item")
                .TAL("item_desc", "Item Desc")
                .TAL("item_spec", "Item Spec")
                .TAL("unit", "Unit")
                .TAL("price", "Price", "4")
                .TAL("approv_ind", "Approved Indicator")
                .TAL("app_date", "Approved Date")
                .TAL("effe_date", "Effective Date")
                .TAL("exp_date", "Expire Date")
                .TAL("app_in_price", "Price Approved")
                .TAL("supplier", "Supplier")
                .TAL("price_by_qty", "Price Order Qty")
                .TAL("date_diff", "Date Diff (Days)", "0")
                .TAL("(case  when date_diff < 0 then 'Expire' when date_diff >= 0 then 'Not Expire' else 'Not Expire' end)" & C8 & "status_price", "Status Price")
            End If
            arlFeild = .ChangeFormat(False)
            arlColumn = .ChangeFormat(True)
            arlNumber = .ColumnNumber()
        End With
        Dim dtShow As DataTable = dtCont.setColDatatable(arlColumn, C8)
        Dim subSqlTbItem As String = String.Empty
        Dim subSqlTbPrice As String = String.Empty
        Dim dt As New DataTable

        '////////////////////////////////////////////// Table Item \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        Dim arlSubTbOne As New ArrayList From {
                 "MB001" & C8 & "item",
                 "MB002" & C8 & "item_desc",
                 "MB003" & C8 & "item_spec",
                 "(case MB109 when 'Y' then 'Y : Approved' end)" & C8 & "approv_ind",
                 "MA002",
                 "MA002 +' : '+ MA003" & C8 & "acc_name"
                }
        Dim sqlStrSubOne As New SQLString("INVMB", arlSubTbOne)
        With sqlStrSubOne
            .setLeftjoin("INVMA", New List(Of String) From {
                       "MA002" & C8 & "MB005"
                    }
                )
            .SetWhere(.WHERE_EQUAL("INVMB.COMPANY", "HOOTHAI"), True)
            .SetWhere(.WHERE_EQUAL("MB109", "Y"))
            .SetWhere(.WHERE_IN("MB005", cblDocType))
            subSqlTbItem = sqlStrSubOne.GetSQLString()
        End With
        '//////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\





        If outSource Then

            '////////////////////////////////////////////// Table Price \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            Dim arlSubTbTwo As New ArrayList From {
                 "TN001",
                 "TN002",
                 "TN003",
                 "TN004",
                 "TN008" & C8 & "unit",
                 "TN009" & C8 & "price",
                 "TM003" & C8 & "app_date",
                 "TN011" & C8 & "effe_date",
                 "TN012" & C8 & "exp_date",
                 "MA001",
                 "MA001 +' : '+ MA002" & C8 & "subcontract",
                 "TN007 +' : '+ MW002" & C8 & "oper",
                 "(case TM009 when 'Y' then 'Y : Approved' end)" & C8 & "app_in_price",
                 "(DATEDIFF(DAY, CONVERT(datetime, GETDATE()),CONVERT(datetime, (case when TN012 = '' then GETDATE() else TN012 end))))" & C8 & "date_diff",
                 "row_number() over ( partition by TN004  ORDER BY TM003 DESC )" & C8 & "rownumber_price"
                }
            Dim sqlStrSub As New SQLString("MOCTN", arlSubTbTwo)
            With sqlStrSub
                .setLeftjoin("MOCTM", New List(Of String) From {
                       "TM001" & C8 & "TN001",
                       "TM002" & C8 & "TN002"
                    }
                )
                .setLeftjoin("PURMA", New List(Of String) From {
                       "MA001" & C8 & "TM004"
                    }
                )
                .setLeftjoin("CMSMW", New List(Of String) From {
                       "MW001" & C8 & "TN007"
                    }
                )

                .SetWhere(.WHERE_EQUAL("1", "1",, False), True)
                .SetWhere(.WHERE_EQUAL("TM009", "Y"))
                subSqlTbPrice = sqlStrSub.GetSQLString()
            End With
            '//////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            Dim sqlStr As New SQLString(vbCrLf & VarIni.B1 & vbCrLf & subSqlTbItem & vbCrLf & VarIni.B2 & " ta1 " & vbCrLf, arlFeild)
            With sqlStr
                .setLeftjoin(vbCrLf & .addBracket(subSqlTbPrice) & " ta2 " & vbCrLf, New List(Of String) From {
                       "item" & C8 & "TN004"
                     }
                 )

                .SetWhere(.WHERE_EQUAL("1", "1",, False), True)
                .SetWhere(.WHERE_EQUAL("TN001", "", " IS NOT NULL "))
                .SetWhere(.WHERE_EQUAL("rownumber_price", "1"))

                .SetWhere(.WHERE_LIKE("TN001 +'-'+ TN002", tbDocNo))
                .SetWhere(.WHERE_LIKE("item", tbItem))
                .SetWhere(.WHERE_LIKE("item_desc", tbItemDesc))
                .SetWhere(.WHERE_LIKE("item_spec", tbItemSpec))
                .SetWhere(.WHERE_LIKE("MA001", tbSupCode))
                .SetWhere(.WHERE_DATE("exp_date", ucDateFrom.Text, ucDateTo.Text))

                If ddlStusPrice.SelectedValue = "0" Then
                    'All
                ElseIf ddlStusPrice.SelectedValue = "1" Then
                    'Expire
                    .SetWhere(.WHERE_EQUAL("date_diff", 0, " < ", False))
                ElseIf ddlStusPrice.SelectedValue = "2" Then
                    'Not Expire
                    .SetWhere(.WHERE_EQUAL("date_diff", 0, " >= ", False))
                End If

                .SetOrderBy("TN001,TN002,TN003,MA002")
            End With

            dt = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)


        Else

            '////////////////////////////////////////////// Table Price \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            Dim arlSubTbTwo As New ArrayList From {
                 "TL001",
                 "TL002",
                 "TM003",
                 "TM004",
                 "TM009" & C8 & "unit",
                 "TM010" & C8 & "price",
                 "TL003" & C8 & "app_date",
                 "TM014" & C8 & "effe_date",
                 "TM015" & C8 & "exp_date",
                 "(case TL006 when 'Y' then 'Y : Approved' end)" & C8 & "app_in_price",
                 "TL004",
                 "TL004 +' : '+  MA002" & C8 & "supplier",
                 "TM008" & C8 & "price_by_qty",
                 "(DATEDIFF(DAY, CONVERT(datetime, GETDATE()),CONVERT(datetime, (case when TM015 = '' then GETDATE() else TM015 end))))" & C8 & "date_diff",
                 "row_number() over ( partition by TM004  ORDER BY TL003 DESC )" & C8 & "rownumber_price"
                }
            Dim sqlStrSub As New SQLString("PURTM", arlSubTbTwo)
            With sqlStrSub
                .setLeftjoin("PURTL", New List(Of String) From {
                       "TL001" & C8 & "TM001",
                       "TL002" & C8 & "TM002"
                    }
                )
                .setLeftjoin("PURMA", New List(Of String) From {
                       "MA001" & C8 & "TL004"
                    }
                )

                .SetWhere(.WHERE_EQUAL("1", "1",, False), True)
                .SetWhere(.WHERE_EQUAL("TL006", "Y"))
                subSqlTbPrice = sqlStrSub.GetSQLString()
            End With
            '//////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            Dim sqlStr As New SQLString(vbCrLf & VarIni.B1 & vbCrLf & subSqlTbItem & vbCrLf & VarIni.B2 & " ta1 " & vbCrLf, arlFeild)
            With sqlStr
                .setLeftjoin(vbCrLf & .addBracket(subSqlTbPrice) & " ta2 " & vbCrLf, New List(Of String) From {
                       "item" & C8 & "TM004"
                     }
                 )

                .SetWhere(.WHERE_EQUAL("1", "1",, False), True)
                .SetWhere(.WHERE_EQUAL("TL001", "", " IS NOT NULL "))
                .SetWhere(.WHERE_EQUAL("rownumber_price", "1"))
                .SetWhere(.WHERE_LIKE("TL001 +'-'+ TL002", tbDocNo))
                .SetWhere(.WHERE_LIKE("item", tbItem))
                .SetWhere(.WHERE_LIKE("item_desc", tbItemDesc))
                .SetWhere(.WHERE_LIKE("item_spec", tbItemSpec))
                .SetWhere(.WHERE_LIKE("TL004", tbSupCode))
                .SetWhere(.WHERE_DATE("exp_date", ucDateFrom.Text, ucDateTo.Text))

                If ddlStusPrice.SelectedValue = "0" Then
                    'All
                ElseIf ddlStusPrice.SelectedValue = "1" Then
                    'Expire
                    .SetWhere(.WHERE_EQUAL("date_diff", 0, " < ", False))
                ElseIf ddlStusPrice.SelectedValue = "2" Then
                    'Not Expire
                    .SetWhere(.WHERE_EQUAL("date_diff", 0, " >= ", False))
                End If

                .SetOrderBy("TL001,TL002,TM003,MA002")
            End With

            dt = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        End If





        Dim countRow As Integer = dt.Rows.Count
        If countRow > VarIni.LimitGridView Then
            If excel = False Then
                gvShow.DataSource = Nothing
                gvShow.DataBind()
                ucCountRow.RowCount = Nothing
                show_message.ShowMessage(Page, "ข้อมูลมากกว่า 500 Rows. ( " & countRow & " )\nกรุณาเลือก Excel.", UpdatePanel1)
                Exit Sub
            End If
        End If

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, arlNumber, fldManual)
            End With
        Next

        If excel = True Then
            expCont.ExportDatatable(If(outSource, "PriceExpiry Item Sub", "PriceExpiry Item"), dtShow, arlColumn)
        Else
            gvCont.GridviewInitial(gvShow, arlColumn,,,,, C8)
            gvCont.ShowGridView(gvShow, dtShow)
            ucCountRow.RowCount = countRow
        End If


    End Sub

End Class