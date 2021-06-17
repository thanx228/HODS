Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Image
'Imports System.Drawing.Imaging
Imports System.Text
Imports System.Drawing.Drawing2D

Imports Spire.Barcode

Public Class labelDelta
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate
    Dim controlPDF As New ControlPDF
    Dim createTableWarehouse As New createTableWarehouse
    Dim pathFile As String = "~/Upload/Barcode/"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Spire.Barcode.BarcodeSettings.ApplyKey("CPLA7-547J1-P2QI4-N8FBO-71VHW")
        createTableWarehouse.CreateLabelRecord()
        Dim SQL As String,
            dt As DataTable,
            WHR As String = Conn_SQL.DecodeFrom64(Request.QueryString("whr").ToString.Trim)
        Dim docName As String = "labelDelta" & Session("UserName") & ".pdf"
        Dim document As Document = New Document(New iTextSharp.text.Rectangle(172.8F, 86.4F), 5.0F, 5.0F, 1.0F, 1.0F) '2.4*1.2

        'Dim iType As String = Request.QueryString("iType").ToString.Trim,
        '    iNo As String = Request.QueryString("iNo").ToString.Trim,
        '    iSeq As String = Request.QueryString("iSeq").ToString.Trim

        'WHR = Conn_SQL.DecodeFrom64(Request.QueryString("whr").ToString.Trim)
        Using memoryStream As New System.IO.MemoryStream()
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, memoryStream)
            document.Open()
            'WHR = Conn_SQL.Where("invType", iType, True, False)
            'WHR &= Conn_SQL.Where("invNo", iNo, True, False)
            'WHR &= If(iSeq <> "", Conn_SQL.Where("invSeq", iSeq, True, False), "")

            SQL = " select L.* from LabelDeltaRecord L left join " & Conn_SQL.DBMain & "..ACRTB on TB001=invType and TB002=invNo and TB003=invSeq " & _
                  " left join " & Conn_SQL.DBMain & "..ACRTA on TA001=TB001 and TA002=TB002 " & _
                  " where docNo >= ACRTB.UDF02 " & WHR & " order by invType,invNo,invSeq"
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            If dt.Rows.Count > 0 Then
                'Dim data As String
                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        printLabel(document, writer, strGenBar(dt.Rows(i)), Trim(.Item("plant")), _
                                   Trim(.Item("vender")), Trim(.Item("spec")), _
                                   CStr(Math.Floor(.Item("qty_pack"))) & " " & Trim(.Item("unit")), _
                                   Trim(.Item("dateCode")).Substring(2, 6), _
                                   Trim(.Item("custWO")) & " " & Trim(.Item("custLine")), _
                                   Trim(.Item("custModel")), CStr(Trim(.Item("docNo")))) 'error
                    End With
                Next
            Else
                document.NewPage()
                Dim phrase As Phrase = Nothing
                Dim cell As PdfPCell = Nothing
                Dim table As PdfPTable = Nothing
                Dim color__1 As BaseColor = Nothing
                'Dim font As Font = Nothing
                Dim font As iTextSharp.text.Font = Nothing

                table = New PdfPTable(1)
                table.TotalWidth = 280.0F
                table.LockedWidth = True
                table.SetWidths(New Single() {1.0F})

                font = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
                phrase = New Phrase()
                phrase.Add(New Chunk("No data Print", font))
                cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

                table.AddCell(cell)
                document.Add(table)
            End If

            document.Close()
            Dim bytes As Byte() = memoryStream.ToArray()
            memoryStream.Close()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("content-disposition", "inline;filename=" & docName) 'attachment
            Response.ContentType = "application/pdf"
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.Clear()

        End Using

    End Sub

    Sub WriteEvent_Click(Src As Object, e As EventArgs)
        Dim ev As New EventLog("Application")
        ' Event's Source name
        ev.Source = "TEST"

        EventLog.CreateEventSource(ev.Source, "Application")

        Try
            ev.WriteEntry("")
        Catch b As exception
            Response.write("WriteEntry " & b.message & "<br>")
        End Try
        ev = Nothing
    End Sub

    Protected Function strGenBar(dr As DataRow) As String
        Dim str As String = ""
        With dr
            'format=Spec{Qty{Unit{vendorCode{dateCode(yymmdd) Model{tradeCode{custPO{invoice{serialNo
            str &= Trim(.Item("spec")) & "{"
            str &= CStr(Math.Floor(.Item("qty_pack"))) & "{"
            str &= Trim(.Item("unit")) & "{"
            str &= Trim(.Item("vender")) & "{"
            str &= Trim(.Item("dateCode")).Substring(2, 6) & "{"
            str &= Trim(.Item("tradeCode")) & "{"
            str &= Trim(.Item("custPO")) & "{"
            str &= Trim(.Item("invType")) & Trim(.Item("invNo")) & "{"
            str &= Trim(.Item("docNo"))
        End With
        Return str
    End Function

    Sub printLabel(ByRef document As Document, writer As PdfWriter, data As String, _
                   plant As String, vendor As String, spec As String, qty As String, _
                   dateCode As String, lineWo As String, model As String, serialNo As String) 'dr As DataRow, qty As Decimal, issueDate As String

        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim color__1 As BaseColor = Nothing
        'Dim font As iTextSharp.text.Font = Nothing
        document.NewPage()

        Dim settings As BarcodeSettings = New BarcodeSettings()
        Dim type As String = "DataMatrix"

        settings.Data2D = data
        settings.Data = data
        'settings.Type = CType(System.Enum.Parse(GetType(BarCodeType), type), BarCodeType)
        settings.Type = BarCodeType.DataMatrix

        Dim fontSize As Short = 8
        Dim fontName As String = "SimSun"
        settings.TextFont = New System.Drawing.Font(fontName, fontSize, FontStyle.Bold)

        settings.BarHeight = 35
        settings.ShowText = False
        settings.ShowCheckSumChar = False

        settings.TopMargin = 1
        settings.RightMargin = 1
        settings.LeftMargin = 1
        settings.BottomMargin = 1
        settings.ImageHeight = 80
        'settings.ImageWidth = 80
        settings.DpiX = 300
        settings.DpiY = 300

        Dim generator As New BarCodeGenerator(settings)
        Dim barcode As System.Drawing.Image = generator.GenerateImage() 'error
        Dim fileName As String = "B2D" & serialNo & ".png"
        barcode.Save(Server.MapPath(pathFile & fileName))

        'barcode.Save(
        'System.IO.Stream()
        'Dim stream As Stream
        'barcode.FromStream(stream)
        'barcode.

        'Dim ibarcode As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(pathFile & fileName))
      
        'add label to pdf
        Dim img0 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(barcode, System.Drawing.Imaging.ImageFormat.Png)
        'Dim img1 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(pathFile & fileName))
        'Dim w As Decimal = img1.ScaledWidth
        'Dim h As Decimal = img1.ScaledHeight
        
        Dim t As PdfTemplate = writer.DirectContent.CreateTemplate(80, 80)
        't.AddImage(img1, 80, 0, 0, 80, 0, 0)
        t.AddImage(img0, 80, 0, 0, 80, 0, 0)
        Dim clipped As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(t)
        'clipped.ScalePercent(50)
        'clipped.SetDpi(300, 300)
        clipped.SetAbsolutePosition(2, 2)
        'document.Add(clipped)

        Dim paragrap0 As Paragraph = New Paragraph
        paragrap0.SpacingAfter = 10.0F
        paragrap0.Add(clipped)

        document.Add(paragrap0)
        'cell = controlPDF.ImageCell(pathFile & fileName, 5.0F, PdfPCell.ALIGN_CENTER)
        'body
        Dim row As Single = 74,
            col As Single = 80,
            rowHight As Single = 11
        fontSize = 7
        writeText(writer.DirectContent, "PLANT: " & plant, col, row, fontSize)
        row -= rowHight
        writeText(writer.DirectContent, "VENDER: " & vendor, col, row, fontSize)
        row -= rowHight
        writeText(writer.DirectContent, "P/N: " & spec, col, row, fontSize)
        row -= rowHight
        writeText(writer.DirectContent, "QTY: " & qty, col, row, fontSize)
        row -= rowHight
        writeText(writer.DirectContent, "D/C: " & dateCode, col, row, fontSize)
        row -= rowHight
        writeText(writer.DirectContent, "LINE WO: " & lineWo, col, row, fontSize - 1)
        row -= rowHight
        writeText(writer.DirectContent, "MODEL: " & model, col, row, fontSize)

        'If File.Exists(Server.MapPath(pathFile & fileName)) Then
        '    File.Delete(Server.MapPath(pathFile & fileName))
        'End If


    End Sub

    Sub writeText(cb As PdfContentByte, Text As String, X As Integer, Y As Integer, size As Integer, Optional align As Integer = 0)
        Dim xfont As BaseFont = FontFactory.GetFont(FontFactory.HELVETICA, size).BaseFont

        cb.SetFontAndSize(xfont, size)
        cb.ShowTextAligned(align, Text, X, Y, 0)

    End Sub

End Class