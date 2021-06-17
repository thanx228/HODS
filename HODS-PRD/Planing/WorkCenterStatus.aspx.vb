Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class WorkCenterStatus
    Inherits System.Web.UI.Page

    Dim dateCont As New DateControl
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl
    Dim ddlCont As New DropDownListControl


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            ddlCont.showDDL(ddlWC, SQL, VarIni.ERP, "MD002", "MD001", True, "ALL")

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ddlCont.showDDL(ddlMoType, SQL, VarIni.ERP, "MQ002", "MQ001", True, "ALL")

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ddlCont.showDDL(ddlSaleType, SQL, VarIni.ERP, "MQ002", "MQ001", True, "ALL")

            'btExportExcel.Visible = False
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim arlAll As New ArrayList
        Dim arlFeild As New ArrayList
        Dim arlColumn As New ArrayList
        Dim arlNumber As New ArrayList
        Dim c8 As String = VarIni.C8
        With New ArrayListControl(arlAll)

            .TAL("RTrim(SFC.TA006)+'-'+SFC.TA007" & c8 & "WC", "WC")
            .TAL("CMSMW.MW002" & c8 & "MW002", "Process Name")
            .TAL("RTrim(COPTC.TC004)" & c8 & "Contractor", "Contractor")
            .TAL("COPMA.MA002" & c8 & "MA002", "Customer Name")
            .TAL("MOC.TA026+'-'+MOC.TA027+'-'+MOC.TA028" & c8 & "so", "SO")
            .TAL("SFC.TA001+'-'+SFC.TA002" & c8 & "mo", "MO")
            .TAL("SFC.TA003" & c8 & "TA003", "MO Seq")
            .TAL("MOC.TA035" & c8 & "TA035", "Item Spec")
            .TAL("(SUBSTRING(SFC.TA008,7,2)+'/'+SUBSTRING(SFC.TA008,5,2)+'/'+SUBSTRING(SFC.TA008,1,4))" & c8 & "startDate", "Plan Start Date")
            .TAL("MOC.TA015 " & c8 & "TA015", "Plan Qty", 2)
            .TAL("SFC.TA010 + SFC.TA013 + SFC.TA016 - SFC.TA011 - SFC.TA012 - SFC.TA014 - SFC.TA015 - SFC.TA048" & c8 & "wip", "WIP Qty", 2)
            .TAL("MOC.TA015 + SFC.TA013 + SFC.TA016 - SFC.TA011  - SFC.TA012 - SFC.TA014 - SFC.TA015 - SFC.TA048" & c8 & "Pending", "Pending Qty", 2)
            .TAL("SFC.TA010 " & c8 & "TA010", "Input Qty(+)", 2)
            .TAL("SFC.TA011 " & c8 & "TA011", "Finish Qty(-)", 2)
            .TAL("SFC.TA013 " & c8 & "TA013", "Return Qty(-)", 2)
            .TAL("SFC.TA014 " & c8 & "TA014", "Finish Return Qty(-)", 2)
            .TAL("SFC.TA012 + SFC.TA056" & c8 & "scrap", "Scrap Qty(-)", 2)
            .TAL("Case MOC.TA011 When '1' then 'Have not Manufactured' when '2' then 'Materials Not Issue'  when '3' then 'Manufacturing' Else 'Finished' end" & c8 & "status", "Status")
            .TAL("(SUBSTRING(MOC.TA006,1,14)+'-'+SUBSTRING(MOC.TA006,15,2))" & c8 & "item", "Item Code")

            arlFeild = .ChangeFormat(False)
            arlColumn = .ChangeFormat(True)
            arlNumber = .ColumnNumber()
        End With

        Dim sqlStr As New SQLString("SFCTA SFC", arlFeild, strSplit:=c8)
        With sqlStr
            .setLeftjoin("MOCTA MOC", New List(Of String) From {
                       "MOC.TA001" & c8 & "SFC.TA001",
                       "MOC.TA002" & c8 & "SFC.TA002"
                       }
             )
            .setLeftjoin("COPTC", New List(Of String) From {
                     "COPTC.TC001" & c8 & "MOC.TA026",
                     "COPTC.TC002" & c8 & "MOC.TA027"
                     }
            )
            .setLeftjoin("COPMA", New List(Of String) From {
                     "COPMA.MA001" & c8 & "COPTC.TC004"
                     }
            )
            .setLeftjoin("CMSMW", New List(Of String) From {
                     "CMSMW.MW001" & c8 & "SFC.TA004"
                     }
            )

            .SetWhere(.WHERE_EQUAL("MOC.TA013", "Y"), True)
            .SetWhere(.WHERE_EQUAL("SFC.TA005", ddlProperty.Text))
            .SetWhere(.WHERE_LIKE("MOC.TA006", tbCode.Text))
            .SetWhere(.WHERE_LIKE("MOC.TA035", tbSpec.Text))
            .SetWhere(.WHERE_LIKE("MOC.TA027", tbSaleNo.Text.Trim))
            .SetWhere(.WHERE_LIKE("MOC.TA028", tbSaleSeq.Text.Trim))
            .SetWhere(.WHERE_EQUAL("COPTC.TC004", tbCust.Text.Trim.ToUpper()))
            .SetWhere(.WHERE_BETWEEN("SFC.TA008", UcDateFrom.Text, UcDateTo.Text))


            If ddlWC.Text <> "ALL" Then
                If ddlProperty.Text = "1" Then '1
                    .SetWhere(.WHERE_EQUAL("SFC.TA006", ddlWC.Text))
                Else '2
                    If ddlProperty.Text = "2" Then
                        ddlWC.Text = tbWC.Text
                        .SetWhere(.WHERE_LIKE("SFC.TA006", ddlWC.Text))
                    End If
                End If
            End If

            If ddlMoType.Text <> "ALL" Then
                .SetWhere(.WHERE_EQUAL("SFC.TA001", ddlMoType.Text))
            End If

            If ddlSaleType.Text <> "ALL" Then
                .SetWhere(.WHERE_EQUAL("MOC.TA026", ddlSaleType.Text))
            End If

            'Status
            Dim showStatus As Boolean = True
            If showStatus Then
                If ddlStatus.Text = "0" Then
                    .SetWhere(.WHERE_IN("MOC.TA011", "y,Y,1,2,3",, True))
                ElseIf ddlStatus.Text = "123" Then
                    .SetWhere(.WHERE_IN("MOC.TA011", "1,2,3",, True))
                ElseIf ddlStatus.Text = "Yy" Then
                    .SetWhere(.WHERE_IN("MOC.TA011", "Y,y",, True))
                Else
                    .SetWhere(.WHERE_EQUAL("MOC.TA011", ddlStatus.Text))
                End If
            End If

            'Qty Status
            Dim fldLast As String = " + SFC.TA013 + SFC.TA016 - SFC.TA011 - SFC.TA012 - SFC.TA014 -  SFC.TA015 - SFC.TA048"
            Dim fldWIP As String = "SFC.TA010 " & fldLast
            Dim fldPending As String = "MOC.TA015 " & fldLast
            If ddlWip.Text = "All" Then
                Dim allwhr As New List(Of String) From {
                    .WHERE_EQUAL(fldWIP, "0", ">", False, False),
                    .WHERE_EQUAL(fldPending, "0", ">", False, False)
                }
                .SetWhere(VarIni.A & dbConn.addBracket(String.Join(" OR ", allwhr)))
            ElseIf ddlWip.Text = "1" Then
                .SetWhere(.WHERE_EQUAL(fldWIP, "0", ">", False))
            Else 'Pending = '2'
                .SetWhere(.WHERE_EQUAL(fldPending, "0", ">", False))
            End If
            .SetOrderBy("MOC.TA006, SFC.TA001, SFC.TA002, SFC.TA003 ")

        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(arlColumn, c8)
        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, arlNumber, fldManual)
            End With
        Next


        gvCont.GridviewInitial(gvShow, arlColumn, strSplit:=c8)
        gvCont.ShowGridView(gvShow, dtShow)
        CountRow.RowCount = dtShow.Rows.Count

        ' btExportExcel.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btExportExcel_Click(sender As Object, e As EventArgs) Handles btExportExcel.Click
        gvCont.ExportGridViewToExcel("WorkCenterStatus" & Session("UserName"), gvShow)
    End Sub



    '////////////////////////////////////////////
    '/////////////ส่วนของ  Print//////////////////
    '///////////////////////////////////////////


    'Function returnFld(fldName As String, fldCall As String) As String
    '    Return ",CONVERT(varchar, floor(" & fldName & "/60)) as " & fldCall
    'End Function

    'Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
    '        e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
    '    End If
    'End Sub

    'ส่วนของ  Print
    'Protected Sub btLabel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLabel.Click
    '    Dim paraName As String = "whr:" & getWhr(False)
    '    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=ItemCycleWIP.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    'End Sub

    'Protected Sub btList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btList.Click
    '    Dim typeFor As String = ddlFor.Text.Trim,
    '        txtQty As String = "Audit Qty",
    '        txtBy As String = "Audit By"
    '    If typeFor = "1" Then
    '        txtQty = "Check Qty"
    '        txtBy = "Check By"
    '    End If
    '    Dim paraName As String = "whr:" & getWhr(False) & ",txtQty:" & txtQty & ",txtBy:" & txtBy
    '    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=ItemListWIP.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)

    'End Sub


End Class
