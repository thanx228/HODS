Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class LRPreport
    Inherits System.Web.UI.Page

    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim ddlCont As New DropDownListControl
    Dim expCont As New ExportImportControl
    Dim gvCont As New GridviewControl
    Dim C8 As String = VarIni.C8

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = String.Empty Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            ShowDdlSoType()
            ddlReport_SelectedIndexChanged(sender, e)
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvScrollShow", "GvScrollShow();", True)

    End Sub

    Private Sub ShowDdlSoType()
        Dim sql As String = "select MQ001 , MQ001 +' : '+ MQ002 MQ002 from CMSMQ where COMPANY = 'HOOTAI' and MQ007 = '1' and MQ003 = '22'"
        ddlCont.showDDL(ddlSoType, sql, VarIni.ERP, "MQ002", "MQ001", True)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        ShowReport(False)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        ShowReport(True)
        'expCont.Export("LRP Report " & If(ddlReport.SelectedValue = "1", "Production", "Purchase"), gvShow)
    End Sub

    Private Sub ShowReport(excel As Boolean)

        Dim Report As String = ddlReport.SelectedValue
        Dim NameReport As String = String.Empty
        Dim dtShow As New DataTable
        Dim dt As New DataTable

        Dim arlMain As New ArrayList
        Dim arlField As New ArrayList
        Dim arlColumn As New ArrayList
        Dim arlNumber As New ArrayList

        If Report = "1" Then
            'Production
            NameReport = "Production"

            With New ArrayListControl(arlMain)

                .TAL(vbCrLf & "LA001" & C8 & "LA001", "Batch No")
                .TAL("LA012" & C8 & "LA012", "Version")
                .TAL("(CASE LA013 WHEN '1' THEN '1 : Valid' WHEN '2' THEN '2 : Simulative' WHEN '3' THEN '3 : Invalid' ELSE '' END)" & C8 & "LA013", "Charater")
                .TAL("TA002" & C8 & "TA002", "Item")
                .TAL("MB002" & C8 & "MB002", "Item Desc")
                .TAL("MB003" & C8 & "MB003", "Item Spec")
                .TAL("TA003" & C8 & "TA003", "Complete Date")
                .TAL("RTRIM(TA005) +' : '+ MC002" & C8 & "TA005", "WH Desc")
                .TAL("cast(TA006 as decimal(15,3))" & C8 & "TA006", "Prod.Qty", 3)
                .TAL("MB004" & C8 & "MB004", "Unit")
                .TAL("TA007" & C8 & "TA007", "Strat Date")
                .TAL("TA008" & C8 & "TA008", "BOM Date")
                .TAL("TA010 +' : '+ MQ002" & C8 & "TA010", "Mo Desc")
                .TAL("RTRIM(TA004) +' : '+ TA027" & C8 & "TA004", "WC Desc")
                .TAL("cast(TA034 as decimal(15,3))" & C8 & "TA034", "Subc.Qty", 3)
                .TAL("TA033" & C8 & "TA033", "Subc.Unit")
                .TAL("TA031" & C8 & "TA031", "Currency")
                .TAL("cast(TA032 as decimal(15,3))" & C8 & "TA032", "Subc.Price", 3)
                .TAL("TA009" & C8 & "TA009", "Lock")
                .TAL("(CASE TA029 WHEN '1' THEN '1 : Order' WHEN '2' THEN '2 : Manufacture Order' WHEN '3' THEN '3 : Plan' WHEN '4' THEN '4 : MPSProduction Plan' WHEN '5' THEN '5 : Sales Forecast' WHEN '6' THEN '6 : Split' ELSE '' END)" & C8 & "TA029", "Basis")
                .TAL("TA023 +'-'+ TA024" & C8 & "TA023", "SO")
                .TAL("TA025" & C8 & "TA025", "So Seq")
                .TAL("cast(TA012 as decimal(15,3))" & C8 & "TA012", "Inventory Qty", 3)
                .TAL("cast(TA043 as decimal(15,3))" & C8 & "TA043", "Safe Stock", 3)
                .TAL("cast(TA013 as decimal(15,3))" & C8 & "TA013", "Purchase Receipt Plan", 3)
                .TAL("cast(TA030 as decimal(15,3))" & C8 & "TA030", "P/R Plan", 3)
                .TAL("cast(TA014 as decimal(15,3))" & C8 & "TA014", "MO Plan", 3)
                .TAL("cast(TA015 as decimal(15,3))" & C8 & "TA015", "SO Plan", 3)
                .TAL("cast(TA016 as decimal(15,3))" & C8 & "TA016", "MO Issue Plan", 3)
                .TAL("cast(TA017 as decimal(15,3))" & C8 & "TA017", "Plan Po", 3)
                .TAL("cast(TA018 as decimal(15,3))" & C8 & "TA018", "Plan Mo", 3)
                .TAL("cast(TA019 as decimal(15,3))" & C8 & "TA019", "Sale Forecast", 3)
                .TAL("cast(TA020 as decimal(15,3))" & C8 & "TA020", "Plan Mo Issue", 3)
                .TAL("cast(TA041 as decimal(15,3))" & C8 & "TA041", "Replaced", 3)
                .TAL("cast(TA042 as decimal(15,3))" & C8 & "TA042", "Replace Other Item", 3)
                .TAL("cast(TA011 as decimal(15,3))" & C8 & "TA011", "Gross Reqmt.", 3)
                .TAL("cast(TA021 as decimal(15,3))" & C8 & "TA021", "Released Qty", 3)
                .TAL("TA051" & C8 & "TA051", "Released Indicator")
                .TAL("TA022" & C8 & "TA022", "Remark")

                .TAL("mo_type +'-'+ mo" & C8 & "mo", "Mo")
                .TAL("mo_status" & C8 & "mo_status", "Mo Status")
                .TAL("mo_bom_date" & C8 & "mo_bom_date", "Bom Date")
                .TAL("plan_qty" & C8 & "plan_qty", "Plan Qty", 3)
                .TAL("issued_kit_qty" & C8 & "issued_kit_qty", "Issued Kit Qty", 3)
                .TAL("completed_qty" & C8 & "completed_qty", "Completed Qty", 3)
                .TAL("scrap_qty" & C8 & "scrap_qty", "Scrap Qty", 3)
                .TAL("destroyed_qty" & C8 & "destroyed_qty", "Destroyed Qty", 3)

                .TAL("so_date", "SO Order Date")
                .TAL("so_paln_de_date", "SO Plan Delivery Date")

                arlField = .ChangeFormat(False)
                arlColumn = .ChangeFormat(True)
                arlNumber = .ColumnNumber()

            End With

            Dim sqlStr As New SQLString("LRPTA TA", arlField)
            With sqlStr
                .setLeftjoin("LRPLA LA", New List(Of String) From {
                    "LA.COMPANY" & C8 & "TA.COMPANY",
                    "LA.LA001" & C8 & "TA.TA001",
                    "LA.LA012" & C8 & "TA.TA050" & vbCrLf
                    }
                )
                .setLeftjoin("INVMB MB", New List(Of String) From {
                    "MB.COMPANY" & C8 & "TA.COMPANY",
                    "MB.MB001" & C8 & "TA.TA002" & vbCrLf
                    }
                )
                .setLeftjoin("CMSMC MC", New List(Of String) From {
                    "MC.MC001" & C8 & "TA.TA005",
                    "MC.COMPANY" & C8 & "HOOTAI" & C8
                    }
                )
                .setLeftjoin("CMSMQ MQ", New List(Of String) From {
                   "MQ.MQ001" & C8 & "TA.TA010",
                   "MQ.COMPANY" & C8 & "HOOTAI" & C8
                   }
                 )

                Dim subSql As String = String.Empty

                '///////////// Sql sub MO \\\\\\\\\\\\
                Dim arlSubFieldMo As New ArrayList From {
                 "TA001" & C8 & "mo_type",
                 "TA002" & C8 & "mo",
                 "(CASE TA011 WHEN '1' THEN '1:Not Produced' WHEN '2' THEN '2:Issued' WHEN '3' THEN '3:Producing' WHEN 'Y' THEN 'Y:Completed' WHEN 'y' THEN 'y:Manual Completed' ELSE '' END)" & C8 & "mo_status",
                 "TA004" & C8 & "mo_bom_date",
                 "TA033" & C8 & "mo_batch",
                 "TA026" & C8 & "mo_so_type",
                 "TA027" & C8 & "mo_so",
                 "TA028" & C8 & "mo_so_seq",
                 "TA006" & C8 & "mo_item",
                 "cast(TA015 as decimal(15,3))" & C8 & "plan_qty" & C8,
                 "cast(TA016 as decimal(15,3))" & C8 & "issued_kit_qty" & C8,
                 "cast(TA017 as decimal(15,3))" & C8 & "completed_qty" & C8,
                 "cast(TA018 as decimal(15,3))" & C8 & "scrap_qty",
                 "cast(TA060 as decimal(15,3))" & C8 & "destroyed_qty"
                }
                Dim sqlStrSubMo As New SQLString("MOCTA TA", arlSubFieldMo)
                With sqlStrSubMo
                    .SetWhere(.WHERE_EQUAL("TA.COMPANY", "HOOTHAI"), True)
                    subSql = sqlStrSubMo.GetSQLString()
                End With

                .setLeftjoin(vbCrLf & .addBracket(subSql) & " mo " & vbCrLf, New List(Of String) From {
                    "mo_batch" & C8 & "LA001",
                    "mo_so_type" & C8 & "TA023",
                    "mo_so" & C8 & "TA024",
                    "mo_so_seq" & C8 & "TA025",
                    "mo_item" & C8 & "TA002"
                    }
                )
                '///////////// END Sql sub MO \\\\\\\\\\\\


                '///////////// Sql sub SO \\\\\\\\\\\\
                Dim arlSubFieldSo As New ArrayList From {
                 "TD001" & C8 & "so_type",
                 "TD002" & C8 & "so_no",
                 "TD003" & C8 & "so_seq",
                 "TC039" & C8 & "so_date",
                 "TD013" & C8 & "so_paln_de_date"
                }
                Dim sqlStrSubSo As New SQLString("COPTD TD", arlSubFieldSo)
                With sqlStrSubSo
                    .setLeftjoin("COPTC TC", New List(Of String) From {
                       "TC.COMPANY" & C8 & "TD.COMPANY",
                       "TC.TC001" & C8 & "TD.TD001",
                       "TC.TC002" & C8 & "TD.TD002 "
                       }
                     )
                    .SetWhere(.WHERE_EQUAL("TD.COMPANY", "HOOTHAI"), True)
                    subSql = sqlStrSubSo.GetSQLString()
                End With

                .setLeftjoin(vbCrLf & .addBracket(subSql) & " so " & vbCrLf, New List(Of String) From {
                    "so_type" & C8 & "TA023",
                    "so_no" & C8 & "TA024",
                    "so_seq" & C8 & "TA025"
                    }
                )
                '///////////// END Sql sub SO \\\\\\\\\\\\


                .SetWhere(.WHERE_EQUAL("LA.COMPANY", "HOOTHAI") & vbCrLf, True)
                .SetWhere(.WHERE_EQUAL("LA005", "1") & vbCrLf)
                .SetWhere(.WHERE_LIKE("LA001", tbBatchNo) & vbCrLf)
                .SetWhere(.WHERE_LIKE("LA012", tbVersion) & vbCrLf)
                .SetWhere(.WHERE_EQUAL("LA013", ddlType) & vbCrLf)
                .SetWhere(.WHERE_EQUAL("TA023", If(ddlSoType.SelectedValue = "0", String.Empty, ddlSoType)) & vbCrLf)
                .SetWhere(.WHERE_LIKE("TA024", tbSo) & vbCrLf)
                .SetWhere(.WHERE_LIKE("TA025", tbSoSeq) & vbCrLf)
                .SetWhere(.WHERE_LIKE("TA002", tbItem) & vbCrLf)
                .SetWhere(.WHERE_LIKE("MB003", tbSpec) & vbCrLf)
                .SetWhere(.WHERE_EQUAL("TA051", If(cbReleased.Checked = True, "Y", String.Empty)))
                .SetWhere(.WHERE_DATE(If(ddlDateType.SelectedValue = "0", "so_date", "so_paln_de_date"), ucDateFrom.Text, ucDateTo.Text) & vbCrLf)
                .SetOrderBy("LA012,TA002,TA023,TA024,TA025,TA003")


            End With

            dtShow = dtCont.setColDatatable(arlColumn, C8)
            dt = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        Else
            'Purchase
            NameReport = "Purchase"

            With New ArrayListControl(arlMain)
                .TAL("LA001" & C8 & "LA001", "Batch No")
                .TAL("LA012" & C8 & "LA012", "Version")
                .TAL("(CASE LA013 WHEN '1' THEN '1 : Effective' WHEN '2' THEN '2 : Simulation' WHEN '3' THEN '3 : Invalid' ELSE '' END)" & C8 & "LA013", "Nature")
                .TAL("TC002" & C8 & "TC002", "Item")
                .TAL("MB002" & C8 & "MB002", "Item Desc")
                .TAL("MB003" & C8 & "MB003", "Item Spec")
                .TAL("TC003" & C8 & "TC003", "Delivery Date")
                .TAL("RTRIM(TC004) +' : '+ MA002" & C8 & "TC004", "Supplier Desc")
                .TAL("TC005 +' : '+ MC002" & C8 & "TC005", "WH Desc")
                .TAL("cast(TC035 as decimal(15,3))" & C8 & "TC035", "Net Reqmt", 3)
                .TAL("cast(TC006 as decimal(15,3))" & C8 & "TC006", "Purchase Qty", 3)
                .TAL("TC011" & C8 & "TC011", "Purchase Unit")
                .TAL("TC007" & C8 & "TC007", "Purchase Date")
                .TAL("TC009" & C8 & "TC009", "Currency")
                .TAL("cast(TC010 as decimal(15,4))" & C8 & "TC010", "Price")
                .TAL("TC008" & C8 & "TC008", "Lock")
                .TAL("(CASE TC042 WHEN '1' THEN '1 : Order' WHEN '2' THEN '2 : Manufacture Order' WHEN '3' THEN '3 : Plan' WHEN '4' THEN '4 : MPSProduction Plan' WHEN '5' THEN '5 : Sales Forecast' ELSE '' END)" & C8 & "TC042", "Basis")
                .TAL("TC026 +'-'+ TC027" & C8 & "TC026", "SO")
                .TAL("TC028" & C8 & "TC028", "SO Seq")
                .TAL("cast(TC013 as decimal(15,3))" & C8 & "TC013", "Inventory Qty", 3)
                .TAL("cast(TC043 as decimal(15,3))" & C8 & "TC043", "Safe Stock", 3)
                .TAL("cast(TC014 as decimal(15,3))" & C8 & "TC014", "Plan Receipt", 3)
                .TAL("cast(TC029 as decimal(15,3))" & C8 & "TC029", "Plan P/R", 3)
                .TAL("cast(TC015 as decimal(15,3))" & C8 & "TC015", "Plan Production", 3)
                .TAL("cast(TC016 as decimal(15,3))" & C8 & "TC016", "Plan Sale", 3)
                .TAL("cast(TC017 as decimal(15,3))" & C8 & "TC017", "Plan Issue", 3)
                .TAL("cast(TC018 as decimal(15,3))" & C8 & "TC018", "Planned Pur", 3)
                .TAL("cast(TC019 as decimal(15,3))" & C8 & "TC019", "Planned Prod", 3)
                .TAL("cast(TC020 as decimal(15,3))" & C8 & "TC020", "Sales Forecast", 3)
                .TAL("cast(TC021 as decimal(15,3))" & C8 & "TC021", "Planned Issue", 3)
                .TAL("cast(TC044 as decimal(15,3))" & C8 & "TC044", "Replaced", 3)
                .TAL("cast(TC045 as decimal(15,3))" & C8 & "TC045", "Replace Other Item", 3)
                .TAL("cast(TC012 as decimal(15,3))" & C8 & "TC012", "Gross Reqmt", 3)
                .TAL("cast(TC022 as decimal(15,3))" & C8 & "TC022", "Released Qty", 3)
                .TAL("TC023" & C8 & "TC023", "Remark")
                .TAL("TC047" & C8 & "TC047", "Released PR")

                .TAL("pr_no" & C8 & "pr_no", "PR No")
                .TAL("pr_seq" & C8 & "pr_seq", "PR Seq")
                .TAL("pr_doc_date" & C8 & "pr_doc_date", "PR Doc Date")
                .TAL("(CASE pr_source WHEN '1' THEN '1:MRP' WHEN '2' THEN '2:LRP' WHEN '3' THEN '3:Reorder Advice Report' WHEN '4' THEN '4:BOM Auto-generating Purchase Request' WHEN '5' THEN '5.SO Generate Purchase Request' WHEN '9' THEN '9:Other' ELSE '' END)" & C8 & "pr_source", "Source")
                .TAL("(CASE pr_approved WHEN 'Y' THEN 'Y:Approved' WHEN 'N' THEN 'N:Not Approved' WHEN 'U' THEN 'U:Approve failed' WHEN 'V' THEN 'V:Cancel' ELSE '' END)" & C8 & "pr_approved", "Approval Indicator")
                .TAL("cast(qty_request as decimal(15,3))" & C8 & "qty_request", "Qty Request", 3)
                .TAL("cast(po_bal as decimal(15,3))" & C8 & "po_bal", "PO Balance", 3)
                .TAL("cast(stock_bal as decimal(15,3))" & C8 & "stock_bal", "Stock Balance", 3)
                .TAL("cast(qty_pur as decimal(15,3))" & C8 & "qty_pur", "Qty Purchase", 3)
                .TAL("cast(pur_amt_bc as decimal(15,3))" & C8 & "pur_amt_bc", "Purchase Amount (B/C)", 3)

                .TAL("so_date", "SO Order Date")
                .TAL("so_paln_de_date", "SO Plan Delivery Date")

                arlField = .ChangeFormat(False)
                arlColumn = .ChangeFormat(True)
                arlNumber = .ColumnNumber()

            End With

            Dim sqlStr As New SQLString("LRPTC TC", arlField)
            With sqlStr
                .setLeftjoin("LRPLA LA", New List(Of String) From {
                      "LA.COMPANY" & C8 & "TC.COMPANY",
                      "LA.LA001" & C8 & "TC.TC001",
                      "LA.LA012" & C8 & "TC.TC046" & vbCrLf
                    }
                )
                .setLeftjoin("INVMB MB", New List(Of String) From {
                     "MB.COMPANY" & C8 & "TC.COMPANY",
                     "MB.MB001" & C8 & "TC.TC002" & vbCrLf
                    }
                )
                .setLeftjoin("CMSMC MC", New List(Of String) From {
                     "MC.MC001" & C8 & "TC.TC005",
                     "MC.COMPANY" & C8 & "HOOTAI" & C8
                    }
                 )
                .setLeftjoin("PURMA MA", New List(Of String) From {
                     "MA.MA001" & C8 & "TC.TC004",
                     "MA.COMPANY" & C8 & "HOOTAI" & C8
                    }
                 )

                Dim subSql As String = String.Empty

                '///////////// Sql sub PR \\\\\\\\\\\\
                Dim arlSubField As New ArrayList From {
                 "TA001 +'-'+ TA002" & C8 & "pr_no",
                 "TB003" & C8 & "pr_seq",
                 "TA003" & C8 & "pr_doc_date",
                 "TA009" & C8 & "pr_source",
                 "TA007" & C8 & "pr_approved",
                 "TB.UDF53" & C8 & "qty_request",
                 "TB.UDF55" & C8 & "po_bal",
                 "TB.UDF54" & C8 & "stock_bal",
                 "TB009" & C8 & "qty_pur",
                 "TB056" & C8 & "pur_amt_bc",
                 "TB004" & C8 & "pr_item",
                 "TB057" & C8 & "pr_batch",
                 "TB058" & C8 & "pr_version",
                 "TB029" & C8 & "pr_so_type",
                 "TB030" & C8 & "pr_so",
                 "TB031" & C8 & "pr_so_seq"
                }
                Dim sqlStrSub As New SQLString("PURTB TB", arlSubField)
                With sqlStrSub
                    .setLeftjoin("PURTA TA", New List(Of String) From {
                        "TA.COMPANY" & C8 & "TB.COMPANY",
                        "TA001" & C8 & "TB001",
                        "TA002" & C8 & "TB002"
                        }
                    )
                    .SetWhere(.WHERE_EQUAL("TB.COMPANY", "HOOTHAI"), True)
                    subSql = sqlStrSub.GetSQLString()
                End With

                .setLeftjoin(vbCrLf & .addBracket(subSql) & " pur " & vbCrLf, New List(Of String) From {
                    "pr_batch" & C8 & "LA001",
                    "pr_version" & C8 & "LA012",
                    "pr_item" & C8 & "TC002",
                    "pr_so_type" & C8 & "TC026",
                    "pr_so" & C8 & "TC027",
                    "pr_so_seq" & C8 & "TC028"
                    }
                )
                '///////////// END Sql sub PR \\\\\\\\\\\\

                '///////////// Sql sub SO \\\\\\\\\\\\
                Dim arlSubFieldSo As New ArrayList From {
                 "TD001" & C8 & "so_type",
                 "TD002" & C8 & "so_no",
                 "TD003" & C8 & "so_seq",
                 "TC039" & C8 & "so_date",
                 "TD013" & C8 & "so_paln_de_date"
                }
                Dim sqlStrSubSo As New SQLString("COPTD TD", arlSubFieldSo)
                With sqlStrSubSo
                    .setLeftjoin("COPTC TC", New List(Of String) From {
                       "TC.COMPANY" & C8 & "TD.COMPANY",
                       "TC.TC001" & C8 & "TD.TD001",
                       "TC.TC002" & C8 & "TD.TD002 "
                       }
                     )
                    .SetWhere(.WHERE_EQUAL("TD.COMPANY", "HOOTHAI"), True)
                    subSql = sqlStrSubSo.GetSQLString()
                End With

                .setLeftjoin(vbCrLf & .addBracket(subSql) & " so " & vbCrLf, New List(Of String) From {
                    "so_type" & C8 & "TC026",
                    "so_no" & C8 & "TC027",
                    "so_seq" & C8 & "TC028"
                    }
                )
                '///////////// END Sql sub SO \\\\\\\\\\\\


                .SetWhere(.WHERE_EQUAL("LA.COMPANY", "HOOTHAI") & vbCrLf, True)
                .SetWhere(.WHERE_EQUAL("LA005", "2") & vbCrLf)
                .SetWhere(.WHERE_LIKE("LA001", tbBatchNo) & vbCrLf)
                .SetWhere(.WHERE_LIKE("LA012", tbVersion) & vbCrLf)
                .SetWhere(.WHERE_EQUAL("LA013", ddlType) & vbCrLf)
                .SetWhere(.WHERE_EQUAL("TC026", If(ddlSoType.SelectedValue = "0", String.Empty, ddlSoType)) & vbCrLf)
                .SetWhere(.WHERE_LIKE("TC027", tbSo) & vbCrLf)
                .SetWhere(.WHERE_LIKE("TC028", tbSoSeq) & vbCrLf)
                .SetWhere(.WHERE_LIKE("TC002", tbItem) & vbCrLf)
                .SetWhere(.WHERE_LIKE("MB003", tbSpec) & vbCrLf)
                .SetWhere(.WHERE_EQUAL("TC047", If(cbReleased.Checked = True, "Y", String.Empty)))
                .SetWhere(.WHERE_DATE(If(ddlDateType.SelectedValue = "0", "so_date", "so_paln_de_date"), ucDateFrom.Text, ucDateTo.Text) & vbCrLf)
                .SetOrderBy("LA001,LA012,TC002,TC026,TC027,TC028,TC003")

            End With

            dtShow = dtCont.setColDatatable(arlColumn, C8)
            dt = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        End If

        Dim DtCount As Integer = dt.Rows.Count
        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, arlNumber, fldManual)
            End With
        Next
        'If DtCount > VarIni.LimitGridView Then
        '    If Not excel Then
        '        show_message.ShowMessage(Page, "ข้อมูลมากกว่า 500 Rows. ( " & DtCount & " )\nกรุณาเลือก Excel.", UpdatePanel1)
        '        Exit Sub
        '    End If
        'End If
        If excel Then
            expCont.ExportDatatable("LRP Report " & NameReport, dtShow, arlColumn)
            ' expCont.ExportGridviewSet("LRP Report " & NameReport, gvShow)
        Else
            gvCont.GridviewInitial(gvShow, arlColumn, strSplit:=C8)
            gvCont.ShowGridView(gvShow, dtShow)
            ucCountRow.RowCount = DtCount
        End If

    End Sub

    Protected Sub ddlReport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReport.SelectedIndexChanged
        Dim Report As String = ddlReport.SelectedValue
        Dim dt As New DataTable
        dt.Columns.Add("VALUE")
        dt.Columns.Add("TEXT")
        dt.Rows.Add("", "== All ==")
        If Report = "1" Then
            lbType.Text = "Charater :"
            dt.Rows.Add("1", "1 : Valid")
            dt.Rows.Add("2", "2 : Simulative")
            dt.Rows.Add("3", "3 : Invalid")
        Else
            lbType.Text = "Nature :"
            dt.Rows.Add("1", "1 : Effective")
            dt.Rows.Add("2", "2 : Simulation")
            dt.Rows.Add("3", "3 : Invalid")
        End If
        ddlCont.showDDL(ddlType, dt, "TEXT", "VALUE")
        ddlType.SelectedValue = "1"
    End Sub


End Class