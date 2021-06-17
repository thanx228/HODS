Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class ItemPopup
    Inherits System.Web.UI.Page

    Dim dbconn As New DataConnectControl
    Dim gvcont As New GridviewControl

    Sub showGrid(SQL As String, gv As GridView, col As ArrayList, rowcount As CountRow)
        With New GridviewControl(gv, col)
            .ShowGridView(SQL, VarIni.ERP, dbconn.WhoCalledMe)
            rowcount.RowCount = .rowGridview()
        End With
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            LbItem.Text = Request.QueryString("item").ToString.Trim
            With New SQLTEXT("INVMB")
                .SL(New List(Of String) From {
                    .Field("MB002"),
                    .Field("MB003"),
                    .Field("MB004")
                    })
                .WE("MB001", LbItem.Text)
                With New DataRowControl(.TEXT, VarIni.ERP)
                    LbDesc.Text = .Text("MB002")
                    LbSpec.Text = .Text("MB003")
                    LbUnit.Text = .Text("MB004")
                End With
            End With
            BtShow_Click(sender, e)
            TabContainer1.ActiveTabIndex = 0
        End If
    End Sub

    Protected Sub BtShow_Click(sender As Object, e As EventArgs) Handles BtShow.Click
        Dim itemNo As String = LbItem.Text
        'summary

        'stock
        Dim selFld As List(Of String)
        Dim colFld As List(Of String)
        'Dim selFldNum As List(Of String)

        With New ListControl(New List(Of String))
            .TAL("MC001", "Item")
            .TAL("MC002", "W/H")
            .TAL("MC007", "Stock Qty", "2")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With
        With New SQLTEXT("INVMC")
            .SL(selFld)
            .WE("MC007", "0", ">", False)
            .WE("MC001", itemNo)
            '.GB(New List(Of String) From {"MC001", "MC002"})
            .OB("MC002")
            showGrid(.TEXT(), GvStock, .ToArrayList(colFld), UcCountRowStock)
        End With

        'PR
        With New ListControl(New List(Of String))
            .TAL("TB004", "Item")
            .TAL("TB001", "PR Type")
            .TAL("TB002", "PR Number")
            .TAL("TB003", "PR Seq")
            .TAL("TA013 ", "PR Date")
            .TAL("TB009", "PR Qty", "2")
            .TAL("TB009", "Require Date")
            .TAL("TB029", "Ref Type")
            .TAL("TB030", "Ref Number")
            .TAL("TB031", "Ref Seq")
            .TAL("TA012", "PR Staff")
            .TAL("case TA006 when '' then '' else  'Head: '+TA006 end + case TB012 when '' then '' else ' Body: '+TB012 end" & VarIni.C8 & "TA006", "Remark")
            .TAL("TB057", "Plan Batch No.")
            .TAL("TB058", "Plan Version")
            .TAL("TB059", "Plan Batch Seq")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With

        With New SQLTEXT("PURTB")
            .SL(selFld)
            .LJ("PURTR", New List(Of String) From {
                .LE("TR001", "TB001"),
                .LE("TR002", "TB002"),
                .LE("TR003", "TB003")
                })
            .LJ("PURTA", New List(Of String) From {
                .LE("TA001", "TB001"),
                .LE("TA002", "TB002")
                })
            .WE("TB004", itemNo)
            .WE("TB039", "N")
            .WE("TA007", "Y")
            .WE("TR019", "''",, False)
            .WE("TR006", "0", ">", False)
            .OB(New List(Of String) From {"TB001", "TB002", "TB003"})
            showGrid(.TEXT(), GvPr, .ToArrayList(colFld), UcCountRowPr)
        End With

        'PO
        With New ListControl(New List(Of String))
            .TAL("TD004", "Item")
            .TAL("TD001", "PO Type")
            .TAL("TD002", "PO Number")
            .TAL("TD003", "PO Seq")
            .TAL("TC004+' '+MA002" & VarIni.C8 & "TC004", "Supplier")
            .TAL("TC024", "PO Date")
            .TAL(dbconn.ISNULL("TD008", "0",, True), "PO Qty", "2")
            .TAL(dbconn.ISNULL("TD015", "0",, True), "Delivered Qty", "2")
            .TAL(dbconn.ISNULL("TD008", "0") & "-" & dbconn.ISNULL("TD015", "0") & VarIni.C8 & "PO_BAL_QTY", "PO Bal Qty", "2")
            .TAL("TD012", "PO Delivery Date")
            .TAL("TD014", "Remark")
            .TAL("case TD013 when '' then '' else TD013+'-'+TD021+'-'+TD023 end" & VarIni.C8 & "REF_DOC", "Ref Doc")
            .TAL("case TD024 when '' then '' else TD024+'-'+TD050+'-'+TD051 end" & VarIni.C8 & "SRC_ORDER", "Src Order")
            .TAL("case TD026 when '' then '' else TD026+'-'+TD027+'-'+TD028 end" & VarIni.C8 & "REF_PR", "Ref PR ")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With
        With New SQLTEXT("PURTD")
            .SL(selFld)
            .LJ("PURTC", New List(Of String) From {
                    .LE("TC001", "TD001"),
                    .LE("TC002", "TD002")
                    })
            .LJ("PURMA", New List(Of String) From { .LE("MA001", "TC004")})

            .WE("TD004", itemNo)
            .WE("TC014", "Y")
            .WE("TD016", "N")
            .WE(.ISNULL("TD008", "0") & "-" & .ISNULL("TD015", "0"), "0", ">", False)
            .OB(New List(Of String) From {"TD012", "TD001", "TD002", "TD003"})
            showGrid(.TEXT(), GvPo, .ToArrayList(colFld), UcCountRowPo)
        End With

        'qc inspection
        With New ListControl(New List(Of String))
            .TAL("TH004", "Item")
            .TAL("TH001", "PO Receipt Type")
            .TAL("TH002", "PO Receipt Number")
            .TAL("TH003", "PO Receipt Seq")
            .TAL("TG005+' '+MA002" & VarIni.C8 & "TG005", "Supplier")
            .TAL("TG014", "PO Receipt Date")
            .TAL("TH007", "QC Qty", "2")
            .TAL("TH009", "W/H")
            .TAL("TH011", "PO Type")
            .TAL("TH012", "PO Number")
            .TAL("TH013", "PO Seq")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With
        With New SQLTEXT("PURTH")
            .SL(selFld)
            .LJ("PURTG", New List(Of String) From {
                    .LE("TG001", "TH001"),
                    .LE("TG002", "TH002")
                    })
            .LJ("PURMA", New List(Of String) From { .LE("MA001", "TG005")})

            .WE("TG013", "N")
            .WE("TH004", itemNo)
            .OB(New List(Of String) From {"TH001", "TH002", "TH003"})
            showGrid(.TEXT(), GvQc, .ToArrayList(colFld), UcCountRowQc)
        End With

        'SO
        With New ListControl(New List(Of String))
            .TAL("TD004", "Item")
            .TAL("TD001", "SO Type")
            .TAL("TD002", "SO Number")
            .TAL("TD003", "SO Seq")
            .TAL("TC004+' '+MA002" & VarIni.C8 & "TC004", "Customer")
            .TAL("TC039", "SO Date")
            .TAL("TD008", "SO Qty", "2")
            .TAL("TD009", "Delivered Qty", "2")
            .TAL("isnull(TD008,0)-isnull(TD009,0)" & VarIni.C8 & "SO_BAL", "SO Bal Qty", "2")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With
        With New SQLTEXT("COPTD")
            .SL(selFld)
            .LJ("COPTC", New List(Of String) From {
                    .LE("TC001", "TD001"),
                    .LE("TC002", "TD002")
                    })
            .LJ("COPMA", New List(Of String) From { .LE("MA001", "TC004")})

            .WE("TC027", "Y")
            .WE("isnull(TD008,0)-isnull(TD009,0)", "0", ">", False)
            .WE("TD004", itemNo)
            .OB(New List(Of String) From {"TD001", "TD002", "TD003"})
            showGrid(.TEXT(), GvSo, .ToArrayList(colFld), UcCountRowSo)
        End With

        'bom fg
        'Dim SQLBOM As String
        'With New SQLTEXT(VarIni.DBMIS & "..BOM")
        '    .SL(New List(Of String) From {
        '        .Field("*")
        '    })
        '    '.LJ("")
        'End With

        'MO
        With New ListControl(New List(Of String))
            .TAL("TA006", "Item")
            .TAL("TA001+'-'+TA002" & VarIni.C8 & "MO", "MO")
            .TAL("TA003", "MO Date")
            .TAL("TA015", "MO Qty", "2")
            .TAL("TA017", "Competed Qty", "2")
            .TAL("TA018", "Scrap Qty", "2")
            .TAL("isnull(TA015,0)-isnull(TA017,0)" & VarIni.C8 & "MO_BAL", "MO Bal Qty", "2")
            .TAL("TA009", "Plan Start Date")
            .TAL("TA010", "Plan Complete Date")
            .TAL("case TA026 when '' then '' else TA026+'-'+TA027+'-'+TA028 end" & VarIni.C8 & "SO", "SO")
            .TAL("isnull(rtrim(TC004)+' '+rtrim(MA002),'')" & VarIni.C8 & "TC004", "Customer")
            .TAL("TA011", "Status")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With
        With New SQLTEXT("MOCTA")
            .SL(selFld)
            .LJ("COPTC", New List(Of String) From {
                    .LE("TC001", "TA026"),
                    .LE("TC002", "TA027")
                    })
            .LJ("COPMA", New List(Of String) From { .LE("MA001", "TC004")})
            .WE("TA013", "Y")
            .WI("TA011", "Y,y", True, True)
            .WE("isnull(TA015,0)-isnull(TA017,0)-isnull(TA018,0)", "0", ">", False)
            .WE("TA006", itemNo)
            .OB(New List(Of String) From {"TA001", "TA002"})
            showGrid(.TEXT(), GvMo, .ToArrayList(colFld), UcCountRowMo)
        End With

        'Mat Issue
        With New ListControl(New List(Of String))
            .TAL("TB003", "Item")
            .TAL("TB001+'-'+TB002" & VarIni.C8 & "MO", "MO")
            .TAL("TA034", "MO item Desc")
            .TAL("TA035", "MO item Spec")
            .TAL("TA006", "MO Item")
            .TAL("TA003", "MO Date")
            .TAL("isnull(TA015,0)-isnull(TA017,0)" & VarIni.C8 & "MO_BAL", "MO Balance Qty", "2")
            .TAL("TB004", "Required Qty", "2")
            .TAL("TB005", "Issued Qty", "2")
            .TAL("isnull(TB004,0)-isnull(TB005,0)" & VarIni.C8 & "MO_ISSUE_BAL", "Issue Balance Qty", "2")
            .TAL("TA009", "Plan Start Date")
            .TAL("TA010", "Plan Complete Date")
            .TAL("case TA026 when '' then '' else TA026+'-'+TA027+'-'+TA028 end" & VarIni.C8 & "SO", "SO")
            .TAL("isnull(rtrim(TC004)+' '+rtrim(MA002),'')" & VarIni.C8 & "TC004", "Customer")
            .TAL("TA011", "Status")
            .TAL("case TA006 when itemFG then '' else MB002 end" & VarIni.C8 & "MB002", "FG Desc")
            .TAL("case TA006 when itemFG then '' else MB003 end" & VarIni.C8 & "MB003", "FG Spec")
            .TAL("case TA006 when itemFG then '' else itemFG end" & VarIni.C8 & "itemFG", "FG Item")
            .TAL("TA015", "MO Qty", "2")
            .TAL("TA017", "Competed Qty", "2")
            .TAL("TA018", "Scrap Qty", "2")
            .TAL("isnull(TA015,0)-isnull(TA017,0)" & VarIni.C8 & "MO_BAL", "MO Bal Qty", "2")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With
        With New SQLTEXT("MOCTB")
            .SL(selFld)
            .LJ("MOCTA", New List(Of String) From {
                    .LE("TA001", "TB001"),
                    .LE("TA002", "TB002")
                    })
            .LJ("COPTC", New List(Of String) From {
                    .LE("TC001", "TA026"),
                    .LE("TC002", "TA027")
                    })
            .LJ("COPMA", New List(Of String) From { .LE("MA001", "TC004")})
            .LJ(VarIni.DBMIS & "..BOM", New List(Of String) From {
                .LE("itemParent", "TA006"),
                .LE("itemChild", "TB003")
                })
            .LJ("INVMB", New List(Of String) From { .LE("MB001", "itemFG")})
            .WE("TA013", "Y")
            .WI("TA011", "Y,y", True, True)
            .WE("isnull(TA015,0)-isnull(TA017,0)-isnull(TA018,0)", "0", ">", False)
            .WE("isnull(TB004,0)-isnull(TB005,0)", "0", ">", False)
            .WE("TB003", itemNo)
            .OB(New List(Of String) From {"TA009", "TB001", "TB002", "TB003"})
            showGrid(.TEXT(), GvIssue, .ToArrayList(colFld), UcCountRowIssue)
        End With

        'BOM
        With New ListControl(New List(Of String))
            .TAL("itemFG", "FG Item")
            .TAL("case itemParent when itemFG then '' else itemParent end" & VarIni.C8 & "itemParent", "Parent Item")
            .TAL("itemChild", "Child Item")
            .TAL("MB002", "Child Desc")
            .TAL("MB003", "Child Spec")
            .TAL("qpa", "QPA", "2")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With
        With New SQLTEXT("HOOTHAI_REPORT..BOM")
            .SL(selFld)
            .LJ("INVMB", New List(Of String) From { .LE("MB001", "itemChild")})
            .WI(dbconn.addSingleQuot(itemNo), "itemFG,itemParent")
            .OB("item")
            showGrid(.TEXT(), GvBom, .ToArrayList(colFld), UcCountRowBom)
        End With

        'BOM Child
        With New ListControl(New List(Of String))
            .TAL("itemFG", "FG Item")
            .TAL("MB002", "FG Desc")
            .TAL("MB003", "FG Spec")
            .TAL("itemChild", "Item")
            .TAL("qpa", "QPA", "2")
            selFld = .ChangeFormat2(VarIni.SP)
            colFld = .ChangeFormat(True)
            'selFldNum = .ColumnNumber
        End With
        With New SQLTEXT("HOOTHAI_REPORT..BOM")
            .SL(selFld)
            .LJ("INVMB", New List(Of String) From { .LE("MB001", "itemFG")})
            .WE("itemChild", itemNo)
            .OB("item")
            showGrid(.TEXT(), GvBomChild, .ToArrayList(colFld), UcCountRowBomChild)
        End With

    End Sub

End Class