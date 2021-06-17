Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ControlPDF
    Dim Conn_SQL As New ConnSQL
    Public aa As String = ""

    Public Sub DrawLine(writer As PdfWriter, x1 As Single, y1 As Single, x2 As Single, y2 As Single, color As BaseColor)
        Dim contentByte As PdfContentByte = writer.DirectContent
        contentByte.SetColorStroke(color)
        contentByte.MoveTo(x1, y1)
        contentByte.LineTo(x2, y2)
        contentByte.Stroke()
    End Sub

    Public Function PhraseCell(phrase As Phrase, alignH As Integer, Optional alignV As Integer = PdfPCell.ALIGN_TOP) As PdfPCell
        Dim cell As New PdfPCell(phrase)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = alignV
        cell.HorizontalAlignment = alignH
        cell.PaddingBottom = 2.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function

    Public Function ImageCell(path As String, scale As Single, align As Integer) As PdfPCell
        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
        image.ScalePercent(scale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function

    Public Function ImageCell(image As Image, scale As Single, align As Integer) As PdfPCell
        'Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
        image.ScalePercent(scale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function

    Function rowPrintData(colCnt As Integer, tableWid As Single, colWid() As Single, dataShow As ArrayList, Optional fontSize As Integer = 10, Optional borColorBlack As Boolean = False, Optional PaddingBottom As Single = 7.0F) As PdfPTable
        'text/path◘col◘I=image,''=text◘scal for image
        Dim font As Font = FontFactory.GetFont("Arial", fontSize, font.NORMAL, BaseColor.BLACK)

        Dim table As PdfPTable = New PdfPTable(colCnt)

        table.LockedWidth = True
        table.SpacingBefore = 1.0F
        table.TotalWidth = tableWid
        table.SetWidths(colWid)

        Dim cell As PdfPCell = Nothing
        For i = 0 To dataShow.Count - 1
            Dim showImage = False,
                col As Integer = 1,
                scal As Single = 20.0F
            Dim prt() As String = dataShow(i).ToString.Split("◘")
            If prt.Count > 1 Then
                If prt(1) <> "" And IsNumeric(prt(1)) Then
                    col = CInt(prt(1))
                End If

            End If

            If prt.Count > 2 Then
                If prt(2) = "I" Then
                    showImage = True
                End If

                If prt.Count > 3 Then
                    If prt(3) <> "" And IsNumeric(CSng(prt(3))) Then
                        scal = CSng(prt(3))
                    End If
                End If
            End If

            If showImage Then
                cell = ImageCell(prt(0), scal, PdfPCell.ALIGN_CENTER)
            Else
                cell = PhraseCell(New Phrase(prt(0), font), PdfPCell.ALIGN_LEFT)
            End If
            cell.Colspan = col
            If borColorBlack Then
                cell.BorderColor = BaseColor.BLACK
            End If
            cell.PaddingBottom = PaddingBottom

            table.AddCell(cell)

        Next
        Return table

    End Function

    Function rowPrintData(colCnt As Integer, tableWid As Single, colWid() As Single, dataShow As ArrayList, font As Font,
                          Optional borColorBlack As Boolean = False,
                          Optional PaddingBottom As Single = 7.0F,
                          Optional align As String = "L") As PdfPTable
        'text/path◘col◘I=image,''=text◘scal for image
        'Dim font As Font = FontFactory.GetFont("Arial", FontSize, font.NORMAL, BaseColor.BLACK)

        Dim table As PdfPTable = New PdfPTable(colCnt)
        table.LockedWidth = True
        table.SpacingBefore = 1.0F
        table.TotalWidth = tableWid
        table.SetWidths(colWid)

        Dim cell As PdfPCell = Nothing
        For i = 0 To dataShow.Count - 1
            Dim showImage = False,
                col As Integer = 1,
                scal As Single = 20.0F
            Dim prt() As String = dataShow(i).ToString.Split("◘")

            If prt.Count > 1 Then
                If prt(1) <> "" And IsNumeric(prt(1)) Then
                    col = CInt(prt(1))
                End If

            End If

            If prt.Count > 2 Then
                If prt(2) = "I" Then
                    showImage = True
                End If
                If prt.Count > 3 Then
                    If prt(3) <> "" And IsNumeric(prt(3)) Then
                        scal = CInt(prt(3))
                    End If
                End If
            End If

            If showImage Then
                cell = ImageCell(prt(0), scal, PdfPCell.ALIGN_CENTER)
            Else
                Dim setAlign As Integer
                Select Case align
                    Case "L"
                        setAlign = PdfPCell.ALIGN_LEFT
                    Case "C"
                        setAlign = PdfPCell.ALIGN_CENTER
                    Case "R"
                        setAlign = PdfPCell.ALIGN_RIGHT
                    Case Else
                        setAlign = PdfPCell.ALIGN_LEFT
                End Select


                cell = PhraseCell(New Phrase(prt(0), font), setAlign)
            End If
            cell.Colspan = col
            If borColorBlack Then
                cell.BorderColor = BaseColor.BLACK
            End If
            cell.PaddingBottom = PaddingBottom

            table.AddCell(cell)
        Next
        Return table

    End Function

    Function limitText(txt As String, limitLen As Integer) As String
        Return If(txt.Length > limitLen, txt.Substring(0, limitLen), txt)
    End Function
    'Create by pin 26-11-2020  => CellPadding 
    Public Function CellPadding(txt As String, align As Integer, font As Font,
                                        Optional pdTop As Single = 0.0F,
                                        Optional pdRight As Single = 0.0F,
                                        Optional pdBottom As Single = 0.0F,
                                        Optional pdLeft As Single = 0.0F,
                                        Optional BorderWidth As Single = 0.0F,
                                        Optional Border_BaseColor As BaseColor = Nothing,
                                        Optional ColSpan As Integer = 1,
                                        Optional RowSpan As Integer = 1) As PdfPCell

        Dim phrase As Phrase = Nothing
        phrase = New Phrase()
        phrase.Add(New Chunk(txt, font))
        Dim cell As New PdfPCell(phrase)

        cell.HorizontalAlignment = align
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE

        cell.PaddingTop = pdTop
        cell.PaddingRight = pdRight
        cell.PaddingBottom = pdBottom
        cell.PaddingLeft = pdLeft

        cell.BorderWidth = BorderWidth
        cell.BorderColor = Border_BaseColor

        cell.Colspan = ColSpan
        cell.Rowspan = RowSpan

        Return cell
    End Function

    'Create by pin 26-11-2020 => fornt Arial
    Public Function FontCreate(FontSize As Single,
                                   Optional fontName As String = "Arial",
                                   Optional FontStyle As Integer = Font.NORMAL,
                                   Optional fontColor As BaseColor = Nothing) As Font
        Try
            Return FontFactory.GetFont(fontName, FontSize, FontStyle, If(fontColor Is Nothing, BaseColor.BLACK, fontColor))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Create by pin 30-11-2020 => forntSarabun

    Public Function BaseFontSarabun(Optional SarabunBold As Boolean = True) As BaseFont
        Return BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Font/Sarabun/THSarabun" & If(SarabunBold, "Bold", "")) & ".ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
    End Function
    Public Function FontSarabun(FontSize As Integer,
                                   Optional FontStyle As Integer = Font.NORMAL,
                                   Optional fontColor As BaseColor = Nothing,
                                   Optional SarabunBold As Boolean = True) As Font
        Return New Font(BaseFontSarabun(SarabunBold), FontSize, FontStyle, If(fontColor Is Nothing, BaseColor.BLACK, fontColor))
    End Function


    'Create by pin 01-12-2020 => ShowTable เป็นการสร้างตัวลอย
    Public Sub ShowTable(ByRef writer As PdfWriter, table As PdfPTable, xPos As Single, yPos As Single)
        If table IsNot Nothing Then
            table.WriteSelectedRows(0, -1, xPos, yPos, writer.DirectContent)
        End If
    End Sub
End Class
