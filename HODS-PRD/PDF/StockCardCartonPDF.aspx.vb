Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MIS_HTI.DataControl
Imports System.IO
Public Class StockCardCartonPDF
    Inherits System.Web.UI.Page
    Dim dbConn As New DataConnectControl
    Dim pdfCont As New ControlPDF
    Dim dateCont As New DateControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Item As String = Request.QueryString("Item")
        Dim Size As String = Request.QueryString("Size")
        Dim Spec As String = Request.QueryString("Spec")
        Dim Month As String = Request.QueryString("Month")
        Dim nameFile As String = "Stock Card Carton " & Month & ".pdf"

        Dim dt As DataTable = SQLsub(Item, Size, Spec, Month)
        Dim Count As Integer = dt.Rows.Count
        Dim LimitRows As Integer = 15

        Dim document As Document = New Document(New Rectangle(841.8898F, 595.2756F), 0.0F, 0.0F, 20.0F, 20.0F)

        Using MemoryStream As New System.IO.MemoryStream()

            Dim writer As PdfWriter = PdfWriter.GetInstance(document, MemoryStream)
            document.Open()

            Dim cell As PdfPCell = Nothing
            Dim table As PdfPTable = Nothing

            document.NewPage()
            document.Add(TopHead())
            document.Add(TableHead)

            Dim No As Integer = 0
            Dim Num As Integer = 0

            For Each dr As DataRow In dt.Rows
                Try
                    dr = dt.Rows(No)
                Catch ex As Exception
                    Exit For
                End Try
                Dim drC As New DataRowControl(dr)
                With New DataRowControl(dr)
                    Num += 1
                    If Num <= LimitRows Then
                        No += 1

                        table = New PdfPTable(17)
                        table.TotalWidth = 785.19F
                        table.LockedWidth = True
                        table.SetWidths(New Single() {0.3F, 0.9F, 1.1F, 2.16F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.5F})
                        cell = pdfCont.CellPadding(No, PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(12, SarabunBold:=False),,, 2, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(.Text("LA001"), PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(13, SarabunBold:=False),,, 5, 3, 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(.Text("MB002"), PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(13, SarabunBold:=False),,, 5, 3, 0.1, BaseColor.BLACK)
                        cell.FixedHeight = 29
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(.Text("MB003"), PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(13, SarabunBold:=False),,, 5, 3, 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("be_2402"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("be_2501"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("be_sum"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("in_2402"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("in_2501"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("in_sum"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("out_2402"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("out_2501"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("out_sum"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("end_new"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("end_old"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding(Format(.Number("end_sum"), "##,###"), PdfPCell.ALIGN_RIGHT, pdfCont.FontSarabun(12, SarabunBold:=False),, 3, 5, , 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        cell = pdfCont.CellPadding("", PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(14, SarabunBold:=False),,, 5, 3, 0.1, BaseColor.BLACK)
                        table.AddCell(cell)
                        document.Add(table)

                        If Num = LimitRows Then
                            Num = 0
                            document.NewPage()
                            document.Add(TopHead())
                            document.Add(TableHead)
                        End If

                    Else
                        Exit For
                    End If

                End With
            Next

            pdfCont.ShowTable(writer, Footer(), 50, 180)

            document.Close()
            Dim bytes As Byte() = MemoryStream.ToArray()
            MemoryStream.Close()
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("content-disposition", "inline;filename=" & nameFile)
            Response.ContentType = "application/pdf"
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.Clear()

        End Using
    End Sub


    Private Function SQLsub(Item As String, Size As String, Spec As String, showMonth As String) As DataTable
        Dim dateInput As String = showMonth
        Dim ShowDate As Date = dateCont.strToDateTime(dateInput & "01", "yyyyMMdd")
        Dim DateBegin As String = ShowDate.AddMonths(-1).ToString("yyyyMM")
        Dim DateStart As String = ShowDate.ToString("yyyyMMdd")
        Dim DateEnd As String = ShowDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd")
        Dim DateBeginEnd As String = ShowDate.AddDays(-1).ToString("yyyyMMdd")

        Dim WHR As String = String.Empty
        WHR = dbConn.WHERE_LIKE("LA001", Item) & vbCrLf
        WHR &= dbConn.WHERE_LIKE("MB002", Size) & vbCrLf
        WHR &= dbConn.WHERE_LIKE("MB003", Spec) & vbCrLf
        WHR &= dbConn.WHERE_BETWEEN("LA004", DateStart, DateEnd) & vbCrLf


        Dim SQL As String = String.Empty
        SQL = " select  LA001 , MB002 , MB003 , 
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
        order by LA001"

        Return dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe())
    End Function


    Private Function TopHead() As PdfPTable
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing

        Dim Month As String = Request.QueryString("Month")
        Dim dateInput As String = Month
        Dim ShowDate As Date = dateCont.strToDateTime(dateInput & "01", "yyyyMMdd")
        Dim DateStart As String = ShowDate.ToString("yyyyMMdd")
        Dim DateEnd As String = ShowDate.AddMonths(1).AddDays(-1).ToString("dd-MM-yyyy")

        table = New PdfPTable(1)
        table.TotalWidth = 785.19F
        table.LockedWidth = True
        table.SetWidths(New Single() {10.0F})

        cell = pdfCont.CellPadding("STOCK CARD " & DateEnd, PdfPCell.ALIGN_LEFT, pdfCont.FontCreate(16),,, 5,, 0.0)
        table.AddCell(cell)

        Return table
    End Function


    Private Function TableHead() As PdfPTable
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing

        Dim Month As String = Request.QueryString("Month")
        Dim ShowDate As Date = dateCont.strToDateTime(Month & "01", "yyyyMMdd")
        Dim DateBeginEnd As String = ShowDate.AddDays(-1).ToString("yyyyMMdd")

        table = New PdfPTable(17)
        table.TotalWidth = 785.19F
        table.LockedWidth = True
        table.SetWidths(New Single() {0.3F, 0.9F, 1.1F, 2.16F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.42F, 0.5F})
        cell = pdfCont.CellPadding("No.", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 2,, 0.1, BaseColor.BLACK,, 2)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Item", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 2,, 0.1, BaseColor.BLACK,, 2)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Size", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 2,, 0.1, BaseColor.BLACK,, 2)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Specification", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 2,, 0.1, BaseColor.BLACK,, 2)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Begin Balance " & vbCrLf & " ( " & DateBeginEnd & " ) ", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 2,, 0.1, BaseColor.BLACK, 3)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("IN PUT", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 2,, 0.1, BaseColor.BLACK, 3)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("OUT PUT", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 2,, 0.1, BaseColor.BLACK, 3)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("End Balance", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 2,, 0.1, BaseColor.BLACK, 3)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("หมายเหตุ", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(12),,, 2,, 0.1, BaseColor.BLACK,, 2)
        table.AddCell(cell)


        cell = pdfCont.CellPadding("New", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Old", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Sum", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("New", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Old", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Sum", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("New", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Old", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Sum", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("New", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Old", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("Sum", PdfPCell.ALIGN_CENTER, pdfCont.FontSarabun(14),,, 3,, 0.1, BaseColor.BLACK)
        table.AddCell(cell)

        Return table
    End Function


    Private Function Footer() As PdfPTable
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing

        Dim dateUnder As String = "วันที่............................................."
        Dim Under As String = "................................"

        table = New PdfPTable(3)
        table.TotalWidth = 785.19F
        table.LockedWidth = True
        table.SetWidths(New Single() {3.3F, 3.3F, 3.3F})

        cell = pdfCont.CellPadding("(MR)ตรวจสอบ" & Under, PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(14), 50,, 5, 50, 0.0, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("หัวหน้าแผนกตรวจสอบ" & Under, PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(14), 50,, 5, 50, 0.0, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding("ผู้จัดทำ" & Under, PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(14), 50,, 5, 50, 0.0, BaseColor.BLACK)
        table.AddCell(cell)


        cell = pdfCont.CellPadding(dateUnder, PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(14), 20,, 5, 50, 0.0, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding(dateUnder, PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(14), 20,, 5, 50, 0.0, BaseColor.BLACK)
        table.AddCell(cell)
        cell = pdfCont.CellPadding(dateUnder, PdfPCell.ALIGN_LEFT, pdfCont.FontSarabun(14), 20,, 5, 50, 0.0, BaseColor.BLACK)
        table.AddCell(cell)

        Return table
    End Function
End Class