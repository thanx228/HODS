Imports MIS_HTI.FormControl
Imports MIS_HTI.DataControl
Imports System.Drawing

Public Class StockCardCarton
    Inherits System.Web.UI.Page

    Dim dbConn As New DataConnectControl
    Dim gvCont As New GridviewControl
    Dim dtCont As New DataTableControl
    Dim exCont As New ExportImportControl
    Dim dateCont As New DateControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = String.Empty Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            ucYearMonth.yyyymm = 0
        End If

    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        showData(False)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        showData(True)
    End Sub

    Private Sub showData(excel As Boolean)

        Dim dtShow As New DataTable
        Dim dateInput As String = ucYearMonth.Text
        Dim ShowDate As Date = dateCont.strToDateTime(dateInput & "01", "yyyyMMdd")
        Dim DateBegin As String = ShowDate.AddMonths(-1).ToString("yyyyMM")
        Dim DateStart As String = ShowDate.ToString("yyyyMMdd")
        Dim DateEnd As String = ShowDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd")
        Dim DateBeginEnd As String = ShowDate.AddDays(-1).ToString("yyyyMMdd")

        Dim arlColumn As New ArrayList From {
        ":No:0",
        ":Item",
        ":Size",
        ":Specification",
        ":New Begin Bal " & DateBeginEnd & ":2",
        ":Old Begin Bal " & DateBeginEnd & ":2",
        ":Sum Begin Bal " & DateBeginEnd & ":2",
        ":New In:2",
        ":Old In:2",
        ":Sum In:2",
        ":New Out:2",
        ":Old Out:2",
        ":Sum Out:2",
        ":New End Bal:2",
        ":Old End Bal:2",
        ":Sum End Bal:2",
        ":หมายเหตุ"
        }
        dtShow = dtCont.setColDatatable(arlColumn)

        Dim WHR As String = String.Empty
        WHR = dbConn.WHERE_LIKE("LA001", tbItem.Text.ToUpper.Trim) & vbCrLf
        WHR &= dbConn.WHERE_LIKE("MB002", tbSize) & vbCrLf
        WHR &= dbConn.WHERE_LIKE("MB003", tbSpec) & vbCrLf
        WHR &= dbConn.WHERE_BETWEEN("LA004", DateStart, DateEnd) & vbCrLf

        Dim SQL As String = String.Empty
        SQL = " select LA001 , MB002 , MB003 , 
        be_2402 , be_2501 , (be_2402 + be_2501) be_sum ,
        in_2402 , in_2501 , (in_2402 + in_2501) in_sum ,
        out_2402 , out_2501 , (out_2402 + out_2501) out_sum ,
        (be_2402 + in_2402 - out_2402) end_new ,
        (be_2501 + in_2501 - out_2501) end_old ,
        (be_2402 + in_2402 - out_2402) + (be_2501 + in_2501 - out_2501) end_sum

        from (
        	select 
        	LA001,MB002,MB003,c1.LC004 be_2402,c2.LC004 be_2501,
        	sum(case LA005 when 1 then (case LA009 when  '2402' then LA011 else 0 end) end) in_2402 , 
        	sum(case LA005 when 1 then (case LA009 when  '2501' then LA011 else 0 end) end) in_2501 , 
        	sum(case LA005 when -1 then (case LA009 when  '2402' then LA011 else 0 end) end) out_2402 , 
        	sum(case LA005 when -1 then (case LA009 when  '2501' then LA011 else 0 end) end) out_2501 
        	from INVLA 
        	left join INVLC c1 on c1.COMPANY =  INVLA.COMPANY and c1.LC001 = LA001 and c1.LC003 = '2402' and c1.LC002 = '" & DateBegin & "'
        	left join INVLC c2 on c2.COMPANY =  INVLA.COMPANY and c2.LC001 = LA001 and c2.LC003 = '2501' and c2.LC002 = '" & DateBegin & "'
        	left join INVMB on INVMB.COMPANY = INVLA.COMPANY and MB001 = LA001 
        	where INVLA.COMPANY = 'HOOTHAI' and MB019 = 'Y'
            " & WHR & "
            and LA009 in ('2402','2501')
        	group by LA001,MB002,MB003,c1.LC004,c2.LC004
        ) stock
        order by LA001 "

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe())
        Dim Count As Integer = dt.Rows.Count

        Dim Num As Integer = 0
        For Each dr As DataRow In dt.Rows
            Num += 1
            With New DataRowControl(dr)
                .AddFromHash(dtShow, New Hashtable From {
                    {"No", Num},
                    {"Item", .Text("LA001")},
                    {"Size", .Text("MB002")},
                    {"Specification", .Text("MB003")},
                    {"New Begin Bal " & DateBeginEnd, .Number("be_2402")},
                    {"Old Begin Bal " & DateBeginEnd, .Number("be_2501")},
                    {"Sum Begin Bal " & DateBeginEnd, .Number("be_sum")},
                    {"New In", .Number("in_2402")},
                    {"Old In", .Number("in_2501")},
                    {"Sum In", .Number("in_sum")},
                    {"New Out", .Number("out_2402")},
                    {"Old Out", .Number("out_2501")},
                    {"Sum Out", .Number("out_sum")},
                    {"New End Bal", .Number("end_new")},
                    {"Old End Bal", .Number("end_old")},
                    {"Sum End Bal", .Number("end_sum")},
                    {"หมายเหตุ", String.Empty}
                })
            End With
        Next

        If excel = True Then
            exCont.Export("Stock Card Carton", dtShow)
            gvShow.DataSource = Nothing
            gvShow.DataBind()
            ucCountRows.RowCount = Nothing
        Else
            gvCont.ShowGridView(gvShow, dtShow)
            ucCountRows.RowCount = Count
        End If

    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        Dim Item As String = tbItem.Text
        Dim Size As String = tbSize.Text
        Dim Spec As String = tbSpec.Text
        Dim Month As String = ucYearMonth.Text

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/StockCardCartonPDF.aspx?Month=" & Month & "&Item=" & Item & "&Size=" & Size & "&Spec=" & Spec & "');", True)
    End Sub

    Protected Sub gvShow_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim gvr As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

            Dim ShowDate As Date = dateCont.strToDateTime(ucYearMonth.Text & "01", "yyyyMMdd")
            Dim DateBeginEnd As String = ShowDate.AddDays(-1).ToString("yyyyMMdd")

            TableCell(gvr, String.Empty, 4)
            TableCell(gvr, "Begin Balance ( " & DateBeginEnd & " )", 3)
            TableCell(gvr, "Input", 3)
            TableCell(gvr, "Output", 3)
            TableCell(gvr, "End Balance", 3)
            TableCell(gvr, String.Empty)

            gvr.BackColor = Drawing.Color.White
            Dim tbl As Table = TryCast(gvShow.Controls(0), Table)
            If tbl IsNot Nothing Then
                tbl.Rows.AddAt(0, gvr)
            End If

        End If
    End Sub

    Function TableCell(Gvr As GridViewRow, nameCell As String, Optional ByVal ColumnSpan As Integer = 1)
        Dim tCell As New TableCell()

        tCell = New TableCell()
        tCell.Text = nameCell
        tCell.ColumnSpan = ColumnSpan
        tCell.Font.Bold = True
        tCell.BackColor = ColorTranslator.FromHtml("#0066CC")
        tCell.HorizontalAlign = HorizontalAlign.Center
        Gvr.Cells.Add(tCell)

        Return Gvr
    End Function

    Protected Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim Name As New ArrayList From {
            "New", "Old", "Sum"
            }
            Dim x As Integer = 0
            For i = 4 To 15
                x = If(x = 3, 0, x)
                e.Row.Cells(i).Width = "70"
                e.Row.Cells(i).Text = Name(x)
                x += 1
            Next
        End If
    End Sub

End Class