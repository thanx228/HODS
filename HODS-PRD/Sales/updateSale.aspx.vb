Imports MIS_HTI.DataControl

Public Class updateSale
    Inherits System.Web.UI.Page
    Dim dbconn As New DataConnectControl
    Dim outcont As New OutputControl
    Dim tableLog As String = "LogSaleUpdate"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim showLable As Boolean = False
            LbSaleType.Visible = showLable
            LbSaleOrder.Visible = showLable
            LbSaleSeq.Visible = showLable
            LbOldWo.Visible = showLable
            LbOldLine.Visible = showLable
            LbOldModel.Visible = showLable

            resetSD()
            resetSO()

        End If
    End Sub

    Protected Sub BtUpdate_Click(sender As Object, e As EventArgs) Handles BtUpdate.Click
        'check
        Select Case TabContainer1.ActiveTabIndex
            Case 0 'sale order
                If String.IsNullOrEmpty(LbItem.Text) Then
                    show_message.ShowMessage(Page, "Not Data for update", UpdatePanel1)
                    Exit Sub
                End If
                If LbOldWo.Text = TbWo.Text.Trim And LbOldLine.Text = TbLine.Text.Trim And LbOldModel.Text = TBModel.Text.Trim Then
                    show_message.ShowMessage(Page, "Not Data Change", UpdatePanel1)
                    Exit Sub
                End If
            Case 1 'sale delivery
                Dim sql As String = "select TH001,TH002 from COPTH left join (SELECT distinct TB005,TB006 FROM ACRTB left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002) ACRTB on TB005=TH001 and TB006=TH002 where TB005 is null and TH001='" & UcDdlSaleDelType.Text2 & "' and TH002='" & TbSaleDelNumber.Text & "' and TH042<0 "
                Dim dt As DataTable = dbconn.Query(sql, VarIni.ERP, dbconn.WhoCalledMe)
                If dt.Rows.Count = 0 Then
                    Exit Sub
                End If
        End Select

        Select Case TabContainer1.ActiveTabIndex
            Case 0 'sale order
                SaleOrder()
            Case 1 'sale delivery
                SaleDelivery()
        End Select
    End Sub

    Sub resetSO()
        Dim SQL As String = "select rtrim(MQ001) MQ001,rtrim(MQ001)+':'+rtrim(MQ002) MQ002 from CMSMQ where MQ003 = '22' order by MQ001 "
        UcDdlSaleOrderType.show(SQL, VarIni.ERP, "MQ002", "MQ001", True)

        TbSaleOrderNumber.Text = ""
        TbSaleOrderSeq.Text = ""

        LbItem.Text = ""
        LbSpec.Text = ""
        LbQty.Text = "0"

        LbSaleType.Text = ""
        LbSaleOrder.Text = ""
        LbSaleSeq.Text = ""
        LbOldWo.Text = ""
        LbOldLine.Text = ""
        LbOldModel.Text = ""

        TbWo.Text = ""
        TbLine.Text = ""
        TBModel.Text = ""

    End Sub

    Sub resetSD()
        Dim SQL As String = "select rtrim(MQ001) MQ001,rtrim(MQ001)+':'+rtrim(MQ002) MQ002 from CMSMQ where MQ003 = '23' order by MQ001 "
        UcDdlSaleDelType.show(SQL, VarIni.ERP, "MQ002", "MQ001", True)
        TbSaleDelNumber.Text = ""


    End Sub

    Sub SaleOrder()
        'update to sale order
        Dim whrHash As New Hashtable From {
            {"TD001", LbSaleType.Text},
            {"TD002", LbSaleOrder.Text},
            {"TD003", LbSaleSeq.Text}
        }
        Dim fldHash As New Hashtable
        If LbOldWo.Text <> TbWo.Text.Trim Then
            fldHash.Add("UDF02", TbWo.Text.Trim)
        End If

        If LbOldLine.Text <> TbLine.Text.Trim Then
            fldHash.Add("UDF03", TbLine.Text.Trim)
        End If

        If LbOldModel.Text <> TBModel.Text.Trim Then
            fldHash.Add("UDF04", TBModel.Text.Trim)
        End If
        Dim SQL As String = dbconn.getUpdateSql("COPTD", fldHash, whrHash)
        'update to log
        If dbconn.TransactionSQL(SQL, VarIni.ERP, dbconn.WhoCalledMe) > 0 Then
            fldHash = New Hashtable From {
                {"DocType", LbSaleType.Text},
                {"DocNumber", LbSaleOrder.Text},
                {"DocSeq", LbSaleSeq.Text},
                {"Val1", LbOldWo.Text.Trim},
                {"Val2", LbOldLine.Text.Trim},
                {"Val3", LbOldModel.Text.Trim},
                {"UserCreate", Session(VarIni.UserName)},
                {"DateCreate", DateTime.Now.ToString("yyyyddMMHHmmss")}
            }
            SQL = dbconn.getInsertSql(tableLog, fldHash, New Hashtable)
            If dbconn.TransactionSQL(SQL, VarIni.DBMIS, dbconn.WhoCalledMe) > 0 Then
                show_message.ShowMessage(Page, "Update completed number=" & LbSaleType.Text & "-" & LbSaleOrder.Text & "-" & LbSaleType.Text, UpdatePanel1)
                resetSO()
            End If
        End If
    End Sub

    Sub SaleDelivery()
        Dim sql As String = "update COPTH set TH042=0 where TH001='" & UcDdlSaleDelType.Text2 & "' and  TH002 = '" & TbSaleDelNumber.Text.Trim & "' "
        If dbconn.TransactionSQL(sql, VarIni.ERP, dbconn.WhoCalledMe) > 0 Then
            Dim fldHash As New Hashtable From {
                {"DocType", UcDdlSaleDelType.Text2},
                {"DocNumber", TbSaleDelNumber.Text.Trim},
                {"Val1", "TH042 is negative"},
                {"UserCreate", Session(VarIni.UserName)},
                {"DateCreate", DateTime.Now.ToString("yyyyddMMHHmmss")}
            }
            show_message.ShowMessage(Page, "UPDATE COMPLETED Sale Delivery Number=" & Trim(UcDdlSaleDelType.Text2) & "-" & TbSaleDelNumber.Text.Trim & "!!", UpdatePanel1)
            resetSD()
        End If
    End Sub

    Protected Sub BtCheck_Click(sender As Object, e As EventArgs) Handles BtCheck.Click
        'resetSO()
        Dim SQL As String = "select TD001,TD002,TD003,TD004,TD006,TD008,COPTD.UDF02 WO,COPTD.UDF03 LINE,COPTD.UDF04 MODEL from COPTD left join COPTC on TC001=TD001 and TC002=TD002 
                            where TD016 = 'N' and TC027 = 'Y' and TD001='" & UcDdlSaleOrderType.Text2 & "' and TD002='" & TbSaleOrderNumber.Text & "' and TD003='" & outcont.checkNumberic(TbSaleOrderSeq.Text).ToString("0000") & "'"

        LbItem.Text = ""
        LbSpec.Text = ""
        LbQty.Text = "0"

        LbSaleType.Text = ""
        LbSaleOrder.Text = ""
        LbSaleSeq.Text = ""
        LbOldWo.Text = ""
        LbOldLine.Text = ""
        LbOldModel.Text = ""

        With New DataRowControl(SQL, VarIni.ERP, dbconn.WhoCalledMe)
            LbSaleType.Text = .Text("TD001")
            LbSaleOrder.Text = .Text("TD002")
            LbSaleSeq.Text = .Text("TD003")
            LbItem.Text = .Text("TD004")
            LbSpec.Text = .Text("TD006")
            LbQty.Text = .Number("TD008")
            LbOldWo.Text = .Text("WO")
            LbOldLine.Text = .Text("LINE")
            LbOldModel.Text = .Text("MODEL")

            TbWo.Text = LbOldWo.Text
            TbLine.Text = LbOldLine.Text
            TBModel.Text = LbOldModel.Text

        End With
        BtHistory_Click(sender, e)

    End Sub

    Protected Sub BtHistory_Click(sender As Object, e As EventArgs) Handles BtHistory.Click
        Dim whr As String = ""
        Dim tabSeq As Integer = TabContainer1.ActiveTabIndex
        Select Case tabSeq
            Case 0 'sale order
                whr &= dbconn.WHERE_IN("DocType", UcDdlSaleOrderType.getObject,, True)
                whr &= dbconn.WHERE_LIKE("DocNumber", TbSaleOrderNumber)
                whr &= dbconn.WHERE_EQUAL("DocSeq", outcont.checkNumberic(TbSaleOrderSeq.Text.Trim).ToString("0000"))
            Case 1 'sale delivery
                whr &= dbconn.WHERE_IN("DocType", UcDdlSaleDelType.getObject,, True)
                whr &= dbconn.WHERE_LIKE("DocNumber", TbSaleDelNumber)
        End Select
        Dim al As New ArrayList
        Dim colName As ArrayList
        Dim fldName As ArrayList

        With New ArrayListControl(al)
            .TAL("DocType", "Doc Type")
            .TAL("DocNumber", " Doc Number")
            .TAL("DocSeq", "Doc Seq")
            .TAL("Val1", If(tabSeq = 0, "WO", "Message"))
            If tabSeq = 0 Then
                .TAL("Val2", "Line")
                .TAL("Val3", "Model")
            End If
            .TAL("UserCreate", "User Create")
            .TAL("DateCreate", "Date Create")
            colName = .ChangeFormat(True)
            fldName = .ChangeFormat()
        End With
        Dim strsql As New SQLString(tableLog, fldName)
        strsql.SetWhere(whr, True)
        strsql.SetOrderBy("ID Desc")
        UcGv.ShowGridviewHyperLink(strsql.GetSQLString, VarIni.DBMIS, colName)
    End Sub
End Class