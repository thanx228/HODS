Imports System.IO

Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class SaleUndeliveryStatus
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate

    Dim dbconn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempPlanDelivery" & Session("UserName")
        CreateTempTable.createTempPlanDelivery(tempTable)
        Dim SQL As String = "", ISQL As String = "", USQL As String = ""
        Dim having As String = "", whr As String = "", whr2 As String = ""
        Dim Program As New DataTable
        Dim item As String = "", qty As Integer = 0

        whr = ""
        whr &= dbconn.WHERE_IN("COPTD.TD001", cblSaleType,, True)
        whr &= dbconn.WHERE_LIKE("COPTD.TD002", tbSO)
        whr &= dbconn.WHERE_LIKE("COPTD.TD003", tbSoSeq)
        whr &= dbconn.WHERE_LIKE("COPTC.TC004", tbCustId)
        whr &= dbconn.WHERE_LIKE("COPTD.TD004 ", tbCode)
        whr &= dbconn.WHERE_LIKE("COPTD.TD006", tbSpec)
        whr &= dbconn.WHERE_DATE("COPTD.TD013", UcDateFrom.Text, UcDateTo.Text)
        'so
        Dim sqlSO As String
        sqlSO = " select COPTD.TD004,SUM(COPTD.TD008+COPTD.TD024-COPTD.TD009-COPTD.TD025) SO from " & Conn_SQL.DBMain & "..COPTD " &
             " left join COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " &
             " where COPTD.TD016='N' and COPTC.TC027='Y' " & whr & " group by COPTD.TD004 "
        sqlSO = "(" & sqlSO & ") COPTD "

        'Stock
        Dim sqlStock As String
        sqlStock = "select MC001,sum(MC007) STOCK from INVMC  where MC002  in('2101') group by MC001 "
        sqlStock = "(" & sqlStock & ") INVMC "

        'MO 
        Dim sqlMO As String
        sqlMO = " select TA006, SUM(isnull(TA015,0)-isnull(TA017,0)-isnull(TA018,0)) MO from MOCTA  where TA011 not in('y','Y') and TA013 ='Y' group by TA006 "
        sqlMO = "(" & sqlMO & ") MOCTA "
        'PO
        Dim sqlPO As String
        sqlPO = " select TD004,SUM(isnull(TD008,0)-isnull(TD015,0)) PO from PURTD where TD016='N'  group by TD004 "
        sqlPO = "(" & sqlPO & ") PURTD "

        'PR
        Dim sqlPR As String
        sqlPR = " select TB004,sum(TR006) PR from PURTB left join PURTR on TR001=TB001 and TR002=TB002 and TR003=TB003 where TR019='' and TB039='N' group by TB004 "
        sqlPR = "(" & sqlPR & ") PURTB "

        Dim al As New ArrayList
        Dim fldName As ArrayList
        Dim colName As ArrayList

        With New ArrayListControl(al)
            .TAL("COPTD.TD004" & VarIni.C8 & "TD004", "Item")
            .TAL("MB002", "desc")
            .TAL("MB003", "spec")
            .TAL("SO", "Undelivery Qty", "0")
            .TAL("STOCK", "Stock Qty", "0")
            .TAL("MO", "MO Qty", "0")
            .TAL("PO", "PO Qty", "0")
            .TAL("PR", "PR Qty", "0")
            .TAL("SO-isnull(STOCK,0)" & VarIni.C8 & "SHIP_BAL", "Ship Balance Qty", "0")
            .TAL("SO-isnull(STOCK,0)-isnull(MO,0)-isnull(PO,0)-isnull(PR,0)" & VarIni.C8 & "SO_BAL", "SO Balance Qty", "0")
            fldName = .ChangeFormat()
            colName = .ChangeFormat(True)
        End With

        Dim strsql As New SQLString(sqlSO, fldName)

        strsql.setLeftjoin(" left join " & sqlStock & " on INVMC.MC001=COPTD.TD004 ")
        strsql.setLeftjoin(" left join " & sqlMO & " on MOCTA.TA006=COPTD.TD004 ")
        strsql.setLeftjoin(" left join " & sqlPO & " on PURTD.TD004=COPTD.TD004 ")
        strsql.setLeftjoin(" left join " & sqlPR & " on PURTB.TB004=COPTD.TD004 ")
        strsql.setLeftjoin(" left join INVMB on INVMB.MB001=COPTD.TD004 ")

        whr = ""
        Dim condition As String = ddlCondition.Text
        If condition <> "0" Then
            whr = " and "
            Dim stock As String = "isnull(STOCK,0)"
            Dim supply As String = stock & "+snull(MO,0)+isnull(PO,0)+isnull(PR,0)"
            Dim undel As String = "SO"
            Select Case condition
                Case "1" ' stock >= undelivery
                    whr &= stock & "<" & undel
                Case "2" ' supply >= undelivery
                    whr &= supply & ">=" & undel
                Case "3" ' supply < undelivery
                    whr &= supply & "<" & undel
                Case "4" ' supply < undelivery
                    whr &= stock & ">=" & undel
            End Select
        End If
        strsql.SetWhere(whr, True)
        strsql.SetOrderBy("COPTD.TD004")
        UcGv.ShowGridviewHyperLink(strsql.GetSQLString, VarIni.ERP, colName, True)

    End Sub

    Private Sub UcGv_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles UcGv.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                With New GridviewRowControl(e.Row)
                    Dim hplDetail As HyperLink = CType(.FindControl("hplDetail"), HyperLink)
                    If Not IsNothing(hplDetail) And Not IsDBNull(.Text("TD004")) Then
                        Dim code As String = .Text("TD004").ToString.Replace("Null", "")
                        Dim spec As String = .Text("MB003").ToString.Replace("Null", "")

                        Dim link As String = "",
                            ordType As String = "",
                            cnt As Integer = 0

                        For Each boxItem As ListItem In cblSaleType.Items
                            If boxItem.Selected = True Then
                                ordType = ordType & "'" & CStr(boxItem.Value) & "',"
                                cnt += 1
                            End If
                        Next
                        If cnt > 0 Then
                            ordType = ordType.Substring(0, ordType.Length - 1)
                        End If
                        link = link & "&saleType=" & ordType
                        link = link & "&saleNo=" & tbSO.Text
                        link = link & "&saleSeq=" & tbSoSeq.Text
                        link = link & "&custID=" & tbCustId.Text
                        link = link & "&endDate=" & UcDateTo.Text
                        link = link & "&JPPart=" & code
                        link = link & "&JPSpec=" & spec
                        link = link & "&stock=" & .Number("STOCK")
                        link = link & "&delQty=" & .Number("SO")
                        link = link & "&poQty=" & .Number("PO")
                        link = link & "&moQty=" & .Number("MO")
                        link = link & "&prQty=" & .Number("PR")
                        hplDetail.NavigateUrl = "../Sales/SaleUndeliveryStatusPopup.aspx?height=150&width=350" & link
                        hplDetail.Attributes.Add("title", code & " - " & spec)
                    End If
                End With
            End If
        End With
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SaleUndeliveryStatus" & Session("UserName"), UcGv.getGridview)
    End Sub

End Class