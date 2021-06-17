Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Imports System.Globalization
Public Class SaleUndeliveryPeriodPopup
    Inherits System.Web.UI.Page
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl

    Dim Item As String = String.Empty
    Dim DateFrom As String = String.Empty
    Dim DateTo As String = String.Empty
    Dim c8 As String = String.Empty

    Dim pathWorkStatus As String = "~/Planing/WorkStatus.aspx"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btRefresh_Click(sender, e)
        End If
    End Sub

    Protected Sub btRefresh_Click(sender As Object, e As EventArgs) Handles btRefresh.Click
        Item = Request.QueryString("item").ToString.Trim
        Dim Spec As String = Request.QueryString("Spec").ToString.Trim
        DateFrom = Request.QueryString("DateFrom").ToString.Trim
        DateTo = Request.QueryString("DateTo").ToString.Trim

        Linktem.Text = Item
        Linktem.NavigateUrl = pathWorkStatus & "?height=150&width=350&item=" & Item
        Linktem.Attributes.Add("title", "Item")
        Linktem.Target = "_blank"

        lbSpec.Text = Spec

        ShowGvStock()
        ShowGvSO()
        ShowGvMO()
        ShowGvPO()
        ShowGvPR()

    End Sub

    'Stock List
    Private Sub ShowGvStock()
        Dim al As New ArrayList
        Dim fldName As New ArrayList
        Dim colName As New ArrayList
        Dim fldNum As New ArrayList
        Dim c8 As String = VarIni.C8
        With New ArrayListControl(al)
            .TAL("INVMC.MC002" & c8 & "WH_ID", "Warehouse Code")
            .TAL("CMSMC.MC002" & c8 & "WH", "Warehouse Name")
            .TAL("isnull(sum(INVMC.MC007),0)" & c8 & "STOCK", "Stock Qty", "2")

            fldName = .ChangeFormat(False)
            colName = .ChangeFormat(True)
            fldNum = .ChangeFormat()
        End With

        Dim sqlStr As New SQLString("INVMC", fldName, strSplit:=c8)
        With sqlStr
            .setLeftjoin("CMSMC", New List(Of String) From {
                "CMSMC.MC001" & c8 & "INVMC.MC002"
                }
        )

            .SetWhere(.WHERE_EQUAL("INVMC.MC001", Item), True)
            .SetGroupBy("INVMC.MC001, INVMC.MC002, CMSMC.MC002")

        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, c8)
        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        Dim stockQtySum As Decimal = 0

        For Each dr As DataRow In dt.Rows
            Dim stockQty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "STOCK")
            Dim fldDate As New ArrayList From {
                "WH_ID",
                "WH",
                "STOCK" & c8 & stockQty
            }
            dtCont.addDataRow(dtShow, dr, fldDate, c8)
            stockQtySum += stockQty
        Next

        If dt.Rows.Count > 1 Then
            Dim fldData As New ArrayList From {
                    "WH" & c8 & "Sum",
                    "STOCK" & c8 & stockQtySum
                    }
            dtCont.addDataRow(dtShow, fldData, c8)
        End If

        lbStockQty.Text = stockQtySum.ToString("N", CultureInfo.InvariantCulture)
        GvStock.ShowGridviewHyperLink(dtShow, colName)

    End Sub

    'SO List
    Private Sub ShowGvSO()
        Dim al As New ArrayList
        Dim fldName As New ArrayList
        Dim colName As New ArrayList
        Dim fldNum As New ArrayList
        Dim c8 As String = VarIni.C8
        With New ArrayListControl(al)
            .TAL("TC004", "Cust ID")
            .TAL("MA003", "Cust Name")
            .TAL("TC001+'-'+TC002" & c8 & "SO", "SO NO")
            .TAL("TD003", "SO Seq")
            .TAL("TD024", "Large Qty", "2")
            .TAL("(SUBSTRING(TC003,7,2)+'-'+SUBSTRING(TC003,5,2)+'-'+SUBSTRING(TC003,1,4))" & c8 & "Approved_Date", "SO Approved Date")
            .TAL("(SUBSTRING(TD013,7,2)+'-'+SUBSTRING(TD013,5,2)+'-'+SUBSTRING(TD013,1,4))" & c8 & "Delivery_Date", "Delivery Date")
            .TAL("TD008", "Ordered Qty", "2")
            .TAL("TD025", "Largess Delivery Qty", "2")
            .TAL("TD009", "Delivery Qty", "2")
            .TAL("TD008 + TD024 - TD009 - TD025" & c8 & "Bal_Qty", "Balance Qty", "2")
            .TAL("TD010", "Unit")
            .TAL("TD027", "PO Number")

            fldName = .ChangeFormat(False)
            colName = .ChangeFormat(True)
            fldNum = .ChangeFormat()
        End With

        Dim sqlStr As New SQLString("COPTC", fldName, strSplit:=c8)
        With sqlStr
            .setLeftjoin("COPTD", New List(Of String) From {
                "TD001" & c8 & "TC001",
                "TD002" & c8 & "TC002"
                }
        )
            .setLeftjoin("COPMA", New List(Of String) From {
                "TC004" & c8 & "MA001"
                }
        )

            .SetWhere(.WHERE_EQUAL("TD004", Item), True)
            .SetWhere(.WHERE_EQUAL("TD016", "N"))
            .SetWhere(.WHERE_EQUAL("TC027", "Y"))
            .SetWhere(.WHERE_BETWEEN("TD013", DateFrom, DateTo))
            .SetOrderBy("TC001, TC002, TD003")

        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, c8)
        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        Dim Large_QtySum As Decimal = 0
        Dim Largess_Deli_QtySum As Decimal = 0
        Dim Order_QtySum As Decimal = 0
        Dim Delivery_QtySum As Decimal = 0

        For Each dr As DataRow In dt.Rows
            Dim Large_Qty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "TD024")
            Dim Order_Qty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "TD008")
            Dim Largess_Deli_Qty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "TD025")
            Dim Deli_Qty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "TD009")

            Dim fldDate As New ArrayList From {
                "TC004",
                "MA003",
                "SO",
                "TD003",
                "TD024",
                "Approved_Date",
                "Delivery_Date",
                "TD008",
                "TD025",
                "TD009",
                "Bal_Qty",
                "TD010",
                "TD027"
            }
            dtCont.addDataRow(dtShow, dr, fldDate, c8)
            Large_QtySum += Large_Qty
            Order_QtySum += Order_Qty
            Largess_Deli_QtySum += Largess_Deli_Qty
            Delivery_QtySum += Deli_Qty
        Next

        If dt.Rows.Count > 1 Then
            Dim fldData As New ArrayList From {
                    "MA003" & c8 & "Sum",
                    "TD024" & c8 & Large_QtySum,
                    "TD008" & c8 & Order_QtySum,
                    "TD025" & c8 & Largess_Deli_QtySum,
                    "TD009" & c8 & Delivery_QtySum,
                    "Bal_Qty" & c8 & (Order_QtySum - Large_QtySum - Delivery_QtySum - Largess_Deli_QtySum)
                    }
            dtCont.addDataRow(dtShow, fldData, c8)
        End If
        lbUndeliveryQty.Text = (Order_QtySum - Large_QtySum - Delivery_QtySum - Largess_Deli_QtySum).ToString("N", CultureInfo.InvariantCulture)
        GvSO.ShowGridviewHyperLink(dtShow, colName, True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvSOScrollbar", "GvSOScrollbar();", True)
    End Sub

    'MO List
    Private Sub ShowGvMO()
        Dim al As New ArrayList
        Dim fldName As New ArrayList
        Dim colName As New ArrayList
        Dim fldNum As New ArrayList
        Dim c8 As String = VarIni.C8
        With New ArrayListControl(al)
            .TAL("TA026 +'-'+TA027" & c8 & "SO_NO", "SO NO")
            .TAL("TA001+'-'+TA002" & c8 & "MO_NO", "MO NO")
            .TAL("(SUBSTRING(TA010,7,2)+'-'+SUBSTRING(TA010,5,2)+'-'+SUBSTRING(TA010,1,4))" & c8 & "Plan_Complete_Date", "Plan Complete Date")
            .TAL("TA015" & c8 & "MO_QTY", "MO Qty", "2")
            .TAL("TA017" & c8 & "Completed_Qty", "Completed Qty", "2")
            .TAL("TA018" & c8 & "SCARP_QTY", "Scarp Qty", "2")
            .TAL("TA015-TA017-TA018" & c8 & "Bal_Qty", "Balance Qty", "2")

            fldName = .ChangeFormat(False)
            colName = .ChangeFormat(True)
            fldNum = .ChangeFormat()
        End With

        Dim sqlStr As New SQLString("MOCTA", fldName, strSplit:=c8)
        With sqlStr

            .SetWhere(.WHERE_EQUAL("TA006", Item), True)
            .SetWhere(.WHERE_IN("TA011", "y,Y", True, True))
            .SetWhere(.WHERE_EQUAL("TA013", "Y"))

        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, c8)
        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        Dim MO_QtySum As Decimal = 0
        Dim Completed_QtySum As Decimal = 0
        Dim Scarp_QtySum As Decimal = 0

        For Each dr As DataRow In dt.Rows
            Dim MOQty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "MO_QTY")
            Dim CompletedQty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "Completed_Qty")
            Dim ScarpQty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "SCARP_QTY")
            Dim fldDate As New ArrayList From {
                "SO_NO",
                "MO_NO",
                "Plan_Complete_Date",
                "MO_QTY",
                "Completed_Qty",
                "SCARP_QTY",
                "Bal_Qty"
            }
            dtCont.addDataRow(dtShow, dr, fldDate, c8)
            MO_QtySum += MOQty
            Completed_QtySum += CompletedQty
            Scarp_QtySum += ScarpQty
        Next

        If dt.Rows.Count > 1 Then
            Dim fldData As New ArrayList From {
                    "SO_NO" & c8 & "Sum",
                    "MO_QTY" & c8 & MO_QtySum,
                    "Completed_Qty" & c8 & Completed_QtySum,
                    "SCARP_QTY" & c8 & Scarp_QtySum,
                    "Bal_Qty" & c8 & (MO_QtySum - Completed_QtySum - Scarp_QtySum)
                    }
            dtCont.addDataRow(dtShow, fldData, c8)
        End If
        lbMOQty.Text = (MO_QtySum - Completed_QtySum - Scarp_QtySum).ToString("N", CultureInfo.InvariantCulture)
        GvMO.ShowGridviewHyperLink(dtShow, colName, True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvMOScrollbar", "GvMOScrollbar();", True)
    End Sub

    'PO List
    Private Sub ShowGvPO()
        Dim al As New ArrayList
        Dim fldName As New ArrayList
        Dim colName As New ArrayList
        Dim fldNum As New ArrayList
        Dim c8 As String = VarIni.C8
        With New ArrayListControl(al)
            .TAL("TD013 +'-'+TD021" & c8 & "SO_NO", "SO No")
            .TAL("TC001+'-'+TC002" & c8 & "PO_NO", "PO No")
            .TAL("TD003", "PO Seq")
            .TAL("TD004", "Supp Code")
            .TAL("MA002", "Supp Name")
            .TAL("(SUBSTRING(TD012,7,2)+'-'+SUBSTRING(TD012,5,2)+'-'+SUBSTRING(TD012,1,4))" & c8 & "Delivery_Date", "Delivery Date")
            .TAL("TD008" & c8 & "PURCHASED_QTY", "Purchased Qty", "2")
            .TAL("TD015" & c8 & "RECEIVED_QTY", "Received Qty", "2")
            .TAL("TD008-TD015" & c8 & "PO_BALANCE_QTY", "PO Balance Qty", "2")

            fldName = .ChangeFormat(False)
            colName = .ChangeFormat(True)
            fldNum = .ChangeFormat()
        End With

        Dim sqlStr As New SQLString("PURTC", fldName, strSplit:=c8)
        With sqlStr
            .setLeftjoin("PURTD", New List(Of String) From {
                "TD001" & c8 & "TC001",
                "TD002" & c8 & "TC002"
                }
            )

            .setLeftjoin("PURMA", New List(Of String) From {
                "TC004" & c8 & "MA001"
                }
             )

            .SetWhere(.WHERE_EQUAL("TD004", Item), True)
            .SetWhere(.WHERE_EQUAL("TD016", "N"))

        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, c8)
        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        Dim PO_QtySum As Decimal = 0
        Dim Rece_QtySum As Decimal = 0

        For Each dr As DataRow In dt.Rows
            Dim POQty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "PURCHASED_QTY")
            Dim ReceQty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "RECEIVED_QTY")
            Dim fldDate As New ArrayList From {
                "SO_NO",
                "PO_NO",
                "TD003",
                "TD004",
                "MA002",
                "Delivery_Date",
                "PURCHASED_QTY",
                "RECEIVED_QTY",
                "PO_BALANCE_QTY"
            }
            dtCont.addDataRow(dtShow, dr, fldDate, c8)
            PO_QtySum += POQty
            Rece_QtySum += ReceQty
        Next

        If dt.Rows.Count > 1 Then
            Dim fldData As New ArrayList From {
                    "SO_NO" & c8 & "Sum",
                    "PURCHASED_QTY" & c8 & PO_QtySum,
                    "RECEIVED_QTY" & c8 & Rece_QtySum,
                    "PO_BALANCE_QTY" & c8 & (PO_QtySum - Rece_QtySum)
                    }
            dtCont.addDataRow(dtShow, fldData, c8)
        End If
        lbPOQty.Text = (PO_QtySum - Rece_QtySum).ToString("N", CultureInfo.InvariantCulture)
        GvPO.ShowGridviewHyperLink(dtShow, colName)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvPOScrollbar", "GvPOScrollbar();", True)
    End Sub

    Private Sub ShowGvPR()
        Dim al As New ArrayList
        Dim fldName As New ArrayList
        Dim colName As New ArrayList
        Dim fldNum As New ArrayList
        Dim c8 As String = VarIni.C8

        With New ArrayListControl(al)
            .TAL("TB029 +'-'+TB030" & c8 & "SO_NO", "SO No")
            .TAL("TB001+'-'+TB002" & c8 & "PR_NO", "PR No")
            .TAL("TB003", "PR Seq")
            .TAL("(SUBSTRING(TB011,7,2)+'-'+SUBSTRING(TB011,5,2)+'-'+SUBSTRING(TB011,1,4))" & c8 & "Require_Date", "Require Date")
            .TAL("TB009" & c8 & "PR_Qty", "PR Qty", "2")

            fldName = .ChangeFormat(False)
            colName = .ChangeFormat(True)
            fldNum = .ChangeFormat()
        End With
        Dim sqlStr As New SQLString("PURTR", fldName, strSplit:=c8)
        With sqlStr
            .setLeftjoin("PURTB", New List(Of String) From {
                "TB001" & c8 & "TR001",
                "TB002" & c8 & "TR002",
                "TB003" & c8 & "TR003"
                }
            )

            .SetWhere(.WHERE_EQUAL("TB004", Item), True)
            .SetWhere(.WHERE_EQUAL("TB039", "N"))

            .SetOrderBy("TB001, TB002, TB003")

        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, c8)
        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        Dim PR_QtySum As Decimal = 0

        For Each dr As DataRow In dt.Rows
            Dim PRQty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "PR_Qty")
            Dim fldDate As New ArrayList From {
                "SO_NO",
                "PR_NO",
                "TB003",
                "Require_Date",
                "PR_Qty"
            }
            dtCont.addDataRow(dtShow, dr, fldDate, c8)
            PR_QtySum += PRQty
        Next

        If dt.Rows.Count > 1 Then
            Dim fldData As New ArrayList From {
                    "SO_NO" & c8 & "Sum",
                    "PR_Qty" & c8 & PR_QtySum
                    }
            dtCont.addDataRow(dtShow, fldData, c8)
        End If
        If PR_QtySum = 0 Then
            lbPRQty.Text = 0.ToString("N", CultureInfo.InvariantCulture)
        Else
            lbPRQty.Text = PR_QtySum.ToString("N", CultureInfo.InvariantCulture)
        End If

        GvPR.ShowGridviewHyperLink(dtShow, colName)
    End Sub

    Private Sub GvStock_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvStock.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim pr As String = .DataItem("WH_ID").ToString.Trim
                If pr = "" Then
                    .BackColor = Drawing.Color.LightBlue
                End If
            End If
        End With
    End Sub

    Protected Sub GvSO_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvSO.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl(NameOf(hplDetail)), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("SO")) _
                    And Not IsDBNull(.DataItem("TD003")) Then

                    Dim so As String = .DataItem("SO").ToString.Trim
                    Dim soSeq As String = .DataItem("TD003").ToString.Trim

                    If so = "" And soSeq = "" Then
                        hplDetail.Visible = False
                        .BackColor = Drawing.Color.LightBlue
                    Else
                        hplDetail.Visible = True
                        hplDetail.NavigateUrl = pathWorkStatus & "?height=150&width=350&so=" & so & "&soseq=" & soSeq
                        hplDetail.Attributes.Add("title", "Sale Order")
                        hplDetail.Target = "_blank"
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub GvMO_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvMO.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl(NameOf(hplDetail)), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("MO_NO")) Then
                    Dim mo As String = .DataItem("MO_NO").ToString.Trim
                    If mo = "" Then
                        hplDetail.Visible = False
                        .BackColor = Drawing.Color.LightBlue
                    Else
                        hplDetail.NavigateUrl = pathWorkStatus & "?height=150&width=350&mo=" & mo
                        hplDetail.Attributes.Add("title", "MO")
                        hplDetail.Target = "_blank"
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub GvPO_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvPO.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim po As String = .DataItem("PO_NO").ToString.Trim
                If po = "" Then
                    .BackColor = Drawing.Color.LightBlue
                End If
            End If
        End With
    End Sub
End Class
