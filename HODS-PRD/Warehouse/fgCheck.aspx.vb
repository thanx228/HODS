Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class fgCheck
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim dbconn As New DataConnectControl
    Dim gvcont As New GridviewControl
    Dim datecont As New DateControl
    Dim outcont As New OutputControl


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Function getWhr(Optional fldIn As Boolean = True, Optional nextDate As Boolean = False) As String 'true=mo reciept , false= sale delivery
        Dim whr As String = "",
            fldName As String = "",
            dateFrom As String = "",
            dateTo As String = ""

        'check date 
        dateFrom = ucDateFrom.dateVal
        dateTo = ucDateTo.dateVal
        If ucDateFrom.dateVal = "" Or ucDateTo.dateVal = "" Then
            Dim dateToday As String = Date.Now.ToString("yyyyMMdd")
            If ucDateFrom.dateVal = "" And ucDateTo.dateVal = "" Then
                dateFrom = dateToday
                dateTo = dateToday
            Else
                If ucDateFrom.dateVal <> "" Then
                    dateTo = ucDateFrom.dateVal
                Else
                    dateFrom = ucDateTo.dateVal
                End If
            End If
        End If

        whr = dbconn.WHERE_LIKE(If(fldIn, "TC047", "TH004"), tbItem)
        whr &= dbconn.Where_like(If(fldIn, "TC049", "TH006"), tbSpec)
        If nextDate Then
            Dim dd As Date = datecont.strToDateTime(dateTo, "yyyyMMdd")
            Dim tomorrow As String = dd.AddDays(1).ToString("yyyyMMdd")
            dateFrom = tomorrow
            dateTo = tomorrow
        End If
        whr &= dbconn.WHERE_BETWEEN(If(fldIn, "TB003", "TG003"), dateFrom, dateTo)
        Return whr
    End Function

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        'new format
        Dim sqlDelSelect As String = ""
        Dim sqlDelNextDay As String = ""
        Dim sqlMoReceipt As String = ""
        Dim sqlStock As String = ""
        Dim sqlSeftyStock As String = ""
        Dim sqlItem As String = ""
        Dim whr As String = ""

        'sale delivery
        sqlDelSelect = " (select TH004,sum(TH008) OUT_SELECT_QTY from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023='Y' " & getWhr(False) & " group by TH004) T1 "

        'sale delivery next days
        sqlDelNextDay = " (select TH004,sum(TH008) OUT_NEXT_QTY from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023='Y' " & getWhr(False, True) & " group by TH004) T2 "

        'mo reciept
        sqlMoReceipt = " (select TC047,sum(TC014) TC014 from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 where TB013='Y' and TB001='D301' " & getWhr() & " group by TC047) MO"

        'stock
        sqlStock = " (select MC001,sum(MC007) STOCK_QTY from INVMC where MC002 in ('2101','2104')  group by MC001) M1 "

        'safty stock
        sqlSeftyStock = " (select MC001,sum(MC004) SAFTY_STOCK_QTY from INVMC where MC004>0 group by MC001) M2 "

        'item
        sqlItem = " (select MB001,MB003,UDF01 POS,UDF51 QTY_PER_BOX,UDF03 CARTON_CODE,UDF04 CARTON_NAME from INVMB) I "

        Dim al As New ArrayList
        Dim fldName As ArrayList
        Dim colName As ArrayList
        With New ArrayListControl(al)
            .TAL("T1.TH004" & VarIni.C8 & "TH004", "Item")
            .TAL("MB003", "Spec")
            .TAL("TC014", "In Qty", "0")
            .TAL("OUT_SELECT_QTY", "Out Qty", "0")
            .TAL("OUT_NEXT_QTY", "Out Qty(Tomorrow)", "0")
            .TAL("STOCK_QTY", "Stock Qty", "0")
            .TAL("SAFTY_STOCK_QTY", "Safety Stock Qty", "0")
            .TAL("POS", "Pos")
            .TAL("QTY_PER_BOX", "Qty/Box", "0")
            .TAL("CARTON_CODE", "Carton Code")
            .TAL("CARTON_NAME", "Carton Name")
            fldName = .ChangeFormat()
            colName = .ChangeFormat(True)
        End With
        Dim strSQL = New SQLString(sqlDelSelect, fldName)
        With strSQL
            .setLeftjoin(sqlDelNextDay, New List(Of String) From {"T2.TH004" & VarIni.C8 & "T1.TH004"})
            .setLeftjoin(sqlMoReceipt, New List(Of String) From {"MO.TC047" & VarIni.C8 & "T1.TH004"})
            .setLeftjoin(sqlStock, New List(Of String) From {"M1.MC001" & VarIni.C8 & "T1.TH004"})
            .setLeftjoin(sqlSeftyStock, New List(Of String) From {"M2.MC001" & VarIni.C8 & "T1.TH004"})
            .setLeftjoin(sqlItem, New List(Of String) From {"I.MB001" & VarIni.C8 & "T1.TH004"})
            .SetOrderBy("T1.TH004")
        End With

        With gvcont
            .GridviewColWithLinkFirst(gvShow, colName, True, "Detail", VarIni.C8)
            .ShowGridView(gvShow, strSQL.GetSQLString, VarIni.ERP)
            ucCountRow.RowCount = .rowGridview(gvShow)
        End With

        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShow", "gridviewScrollgvShow();", True)

    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplDetail"), HyperLink) 'Detail
                If Not IsNothing(hplDetail) Then
                    If Not IsDBNull(.DataItem("TH004")) Then
                        Dim dateFrom As String = "", dateTo As String = ""

                        'check date 
                        dateFrom = ucDateFrom.dateVal
                        dateTo = ucDateTo.dateVal
                        If ucDateFrom.dateVal = "" Or ucDateTo.dateVal = "" Then
                            Dim dateToday As String = Date.Now.ToString("yyyyMMdd")
                            If ucDateFrom.dateVal = "" And ucDateTo.dateVal = "" Then
                                dateFrom = dateToday
                                dateTo = dateToday
                            Else
                                If ucDateFrom.dateVal <> "" Then
                                    dateTo = ucDateFrom.dateVal
                                Else
                                    dateFrom = ucDateTo.dateVal
                                End If
                            End If
                        End If

                        hplDetail.NavigateUrl = "fgCheckSub.aspx?height=150&width=350&item=" & outcont.EncodeTo64UTF8(.DataItem("TH004").ToString.Trim) & "&dateF=" & outcont.EncodeTo64UTF8(dateFrom) & "&dateT=" & outcont.EncodeTo64UTF8(dateTo)
                        hplDetail.Attributes.Add("title", .DataItem("MB003"))
                    End If
                End If
            End If
        End With
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("fgCheck" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class