Imports System.Data
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MIS_HTI.DataControl

Namespace PDF
    Public Class PDFControl
        'Dim dbConn As New clsDBConnect
        Dim outCont As New OutputControl

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

        Public Function ImageCell(path As String, scale As Single, align As Integer, Optional bordelsize As Integer = 0) As PdfPCell
            Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
            image.ScalePercent(scale)
            Dim cell As New PdfPCell(image)
            With cell
                .BorderColor = BaseColor.WHITE
                .VerticalAlignment = PdfPCell.ALIGN_CENTER
                .HorizontalAlignment = align
                .PaddingBottom = 0.0F
                .PaddingTop = 0.0F
                .Border = bordelsize
            End With
            Return cell

        End Function

        Function rowPrintData(colCnt As Integer, tableWid As Single, colWid() As Single, dataShow As ArrayList, Optional fontSize As Integer = 10, Optional borColorBlack As Boolean = False, Optional PaddingBottom As Single = 7.0F) As PdfPTable
            'text/path◘col◘I=image,''=text◘scal for image
            Dim font As Font = FontFactory.GetFont("Arial", fontSize, Font.NORMAL, BaseColor.BLACK)
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
                If prt.Count > 2 Then
                    If prt(2) = "I" Then
                        showImage = True
                    End If

                End If
                If prt.Count > 1 Then
                    If prt(1) <> "" And IsNumeric(prt(1)) Then
                        col = CInt(prt(1))
                    End If

                End If
                If prt.Count > 3 Then
                    If prt(3) <> "" And IsNumeric(prt(3)) Then
                        col = CInt(prt(3))
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

        Function rowPrintData(colCnt As Integer, tableWid As Single, colWid() As Single, dataShow As ArrayList, font As Font, Optional borColorBlack As Boolean = False, Optional PaddingBottom As Single = 7.0F) As PdfPTable
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
                If prt.Count > 2 Then
                    If prt(2) = "I" Then
                        showImage = True
                    End If

                End If
                If prt.Count > 1 Then
                    If prt(1) <> "" And IsNumeric(prt(1)) Then
                        col = CInt(prt(1))
                    End If

                End If
                If prt.Count > 3 Then
                    If prt(3) <> "" And IsNumeric(prt(3)) Then
                        col = CInt(prt(3))
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

        Function limitText(txt As String, limitLen As Integer) As String
            Return If(txt.Length > limitLen, txt.Substring(0, limitLen), txt)
        End Function

        Sub borderPage(ByRef document As Document, ByRef writer As PdfWriter,
                       Optional mLeft As Single = 0.0F, Optional mRight As Single = 0.0F,
                       Optional mTop As Single = 0.0F, Optional mBottom As Single = 0.0F)

            Dim content As PdfContentByte = writer.DirectContent
            Dim rectangle As Rectangle = New Rectangle(document.PageSize)
            rectangle.Left += document.LeftMargin - mLeft
            rectangle.Right -= document.RightMargin - mRight
            rectangle.Top -= document.TopMargin - mTop
            rectangle.Bottom += document.BottomMargin + mBottom
            content.SetColorStroke(BaseColor.BLACK)
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height)
            content.Stroke()
        End Sub

        'create by wanlop
        'create detail : get new font
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

        Public Function BaseFontSarabun(Optional SarabunBold As Boolean = True) As BaseFont
            Return BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Font/Sarabun/THSarabun" & If(SarabunBold, "Bold", "")) & ".ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        End Function

        'Update by wit 29-01-2020
        Public Function FontSarabun(FontSize As Integer,
                                    Optional FontStyle As Integer = Font.NORMAL,
                                    Optional fontColor As BaseColor = Nothing,
                                    Optional SarabunBold As Boolean = True) As Font
            Return New Font(BaseFontSarabun(SarabunBold), FontSize, FontStyle, If(fontColor Is Nothing, BaseColor.BLACK, fontColor))
        End Function

        Public Sub Line(writerPDF As PdfWriter, x1 As Single, y1 As Single,
                        x2 As Single, y2 As Single, Optional Color As BaseColor = Nothing)
            Dim cb As PdfContentByte = writerPDF.DirectContent
            Line(cb, x1, y1, x2, y2, Color)
        End Sub

        Public Sub Line(cb As PdfContentByte, x1 As Single, y1 As Single,
                        x2 As Single, y2 As Single, Optional Color As BaseColor = Nothing)
            If Color IsNot Nothing Then
                cb.SetColorStroke(Color)
            End If
            cb.MoveTo(x1, y1)
            cb.LineTo(x2, y2)
            cb.Stroke()
        End Sub

        Public Sub Text(writerPDF As PdfWriter, textShow As String, setX As Single, setY As Single,
                        Optional fontSize As Single = 12,
                        Optional fontColor As BaseColor = Nothing)
            Dim cb As PdfContentByte = writerPDF.DirectContent
            Text(cb, textShow, setX, setY, fontSize, fontColor)
        End Sub

        Public Sub Text(cb As PdfContentByte, textShow As String, setX As Single, setY As Single,
                        Optional fontSize As Single = 12,
                        Optional fontColor As BaseColor = Nothing)
            cb.BeginText()
            cb.SetFontAndSize(BaseFontSarabun(), fontSize)
            If fontColor IsNot Nothing Then
                cb.SetColorFill(BaseColor.GRAY)
            End If
            cb.SetTextMatrix(setX, setY)
            cb.ShowText(textShow)
            cb.EndText()
        End Sub

        Public Sub images(cb As PdfContentByte, path As String, scale As Single, x As Single, y As Single)
            'path ../Images/arrow.jpg
            Dim img As Image = Image.GetInstance(HttpContext.Current.Server.MapPath(path))
            img.ScalePercent(scale)
            'img.ScaleToFit(20, 25)
            img.SetAbsolutePosition(x, y)
            cb.SetTextMatrix(x, y)
            cb.AddImage(img)
        End Sub



        Public Sub ShowTable(ByRef writer As PdfWriter, table As PdfPTable, xPos As Single, yPos As Single)
            If table IsNot Nothing Then
                table.WriteSelectedRows(0, -1, xPos, yPos, writer.DirectContent)
            End If
        End Sub
        'create date : 2019/09/10
        'create by : wanlop
        'objective : show text cell in pdf format

        'update by : wanlop
        'update date : 2019/09/17
        'update detail : add optional parameter CellColSpan and CellRowSpan
        Public Function Cell(text As String,
                            Optional FontSize As Integer = 10,
                            Optional FontStyle As Integer = Font.NORMAL,
                            Optional FontColor As BaseColor = Nothing,
                            Optional CellAlignH As Integer = PdfPCell.ALIGN_LEFT,
                            Optional CellAlignV As Integer = PdfPCell.ALIGN_MIDDLE,
                            Optional CellBorder As Integer = PdfPCell.BOX,
                            Optional CellBorderColours As BaseColor = Nothing,
                            Optional fixHieght As Single = 15.0F,
                            Optional CellColSpan As Integer = 1,
                            Optional CellRowSpan As Integer = 1) As PdfPCell
            Dim font As Font = FontSarabun(FontSize, FontStyle, FontColor)
            Dim bordercolor As BaseColor = If(CellBorderColours Is Nothing, BaseColor.WHITE, CellBorderColours)
            Dim cellData As New PdfPCell(New Phrase(New Chunk(text, font))) With {
                .Border = CellBorder,
                .BorderColorLeft = bordercolor,
                .BorderColorRight = bordercolor,
                .BorderColorBottom = bordercolor,
                .VerticalAlignment = CellAlignV,
                .HorizontalAlignment = CellAlignH,
                .PaddingBottom = 0.0F,
                .PaddingTop = 0.0F,
                .PaddingLeft = 1.5F,
                .PaddingRight = 1.5F,
                .Rowspan = CellRowSpan,
                .Colspan = CellColSpan,
                .FixedHeight = fixHieght * CellRowSpan
            }
            Return cellData
        End Function

        Public Function Cell(txt As String, font As Font,
                            Optional alignH As Integer = PdfPCell.ALIGN_LEFT,
                            Optional alignV As Integer = PdfPCell.ALIGN_TOP,
                            Optional padButton As Single = 2.0F,
                            Optional padTop As Single = 0.0F,
                            Optional fixHight As Single = 0.0F,
                            Optional border As Integer = Rectangle.NO_BORDER,
                            Optional borderColours As BaseColor = Nothing) As PdfPCell
            Dim cellData As New PdfPCell(New Phrase(New Chunk(txt, font))) With {
                .BorderColor = If(borderColours Is Nothing, BaseColor.WHITE, borderColours),
                .Border = border,
                .VerticalAlignment = alignV,
                .HorizontalAlignment = alignH,
                .PaddingBottom = padButton,
                .PaddingTop = padTop
            }
            If fixHight > 0.0F Then
                cellData.FixedHeight = fixHight
            End If
            Return cellData
        End Function

        Sub Rows(ByRef table As PdfPTable, dr As DataRow, fldList As ArrayList, Optional strSpit As String = VarIni.char8)
            With New DataRowControl(dr)
                For Each str As String In fldList
                    Dim fldName As String = str.Split(strSpit).ToArray(0)
                    Dim align As String = str.Split(strSpit).ToArray(1) 'L,C,R
                    Dim valAlign As Integer = PdfPCell.ALIGN_LEFT
                    If Not String.IsNullOrEmpty(align) Then
                        Select Case align
                            Case "C"
                                valAlign = PdfPCell.ALIGN_CENTER
                            Case "R"
                                valAlign = PdfPCell.ALIGN_RIGHT
                        End Select
                    End If
                Next
            End With
        End Sub

        Sub Rows(ByRef table As PdfPTable,
                 fldList As ArrayList,
                 Optional fontSize As Integer = 11,
                 Optional strSpit As String = VarIni.char8,
                 Optional cellBorder As Integer = 15,
                 Optional fixHieght As Single = 15.0F)
            For Each str As String In fldList
                Dim tt As String() = str.Split(strSpit)
                Dim pText As String = String.Empty
                Dim pColSpan As Integer = 1
                Dim pRowSpan As Integer = 1
                Dim align As String = "L"
                Dim line As Integer = 0
                For Each txt As String In tt
                    Select Case line
                        Case 0
                            pText = txt
                        Case 1
                            pColSpan = outCont.checkNumberic(txt, 1)
                        Case 2
                            pRowSpan = outCont.checkNumberic(txt, 1)
                        Case 3
                            align = txt
                    End Select
                    line += 1
                Next
                Dim cellTemp As PdfPCell = Cell(pText, fontSize, CellAlignH:=getAlign(align), CellColSpan:=pColSpan, CellRowSpan:=pRowSpan, CellBorder:=cellBorder, fixHieght:=fixHieght)
                table.AddCell(cellTemp)
            Next
        End Sub


        Sub Rows(ByRef table As PdfPTable, dr As DataRow, fldList As ArrayList,
                 Optional fontSize As Integer = 11, Optional strSpit As String = VarIni.char8)

            With New DataRowControl(dr)
                Dim newFldList As New ArrayList
                For Each str As String In fldList
                    Dim tt As String() = str.Split(strSpit)
                    Dim pText As String = String.Empty
                    Dim pColSpan As Integer = 1
                    Dim pRowSpan As Integer = 1
                    Dim align As String = "L"
                    Dim isNumber As Boolean = 0
                    Dim line As Integer = 0
                    For Each txt As String In tt
                        Select Case line
                            Case 0
                                pText = txt
                            Case 1
                                pColSpan = outCont.checkNumberic(txt, 1)
                            Case 2
                                pRowSpan = outCont.checkNumberic(txt, 1)
                            Case 3
                                align = txt
                            Case 4
                                isNumber = txt
                        End Select
                        line += 1
                    Next
                    Dim txt01 As String = ""
                    If isNumber Then
                        txt01 = .Number(pText).ToString("N2")
                    Else
                        txt01 = .Text(pText)
                    End If
                    newFldList.Add(String.Join(strSpit, New List(Of String) From {
                                               txt01,
                                               pColSpan,
                                               pRowSpan,
                                               align
                                               }))
                Next
                Rows(table, newFldList, fontSize, strSpit)
            End With


        End Sub


        'create date : 2019/09/10
        'create by : wanlop
        'objective : set format for pdf table
        Function TableFormat(colWidth() As Single, totalWidth As Single) As PdfPTable
            Dim table As New PdfPTable(colWidth.Length) With {
                .TotalWidth = totalWidth,
                .LockedWidth = True
            }
            table.SetWidths(colWidth)
            Return table
        End Function

        Function getAlign(txtAlign As String) As Integer
            Dim valAlign As Integer = PdfPCell.ALIGN_LEFT
            Try
                If Not String.IsNullOrEmpty(txtAlign) Then
                    Select Case txtAlign
                        Case "C"
                            valAlign = PdfPCell.ALIGN_CENTER
                        Case "R"
                            valAlign = PdfPCell.ALIGN_RIGHT
                    End Select
                End If
            Catch ex As Exception

            End Try
            Return valAlign
        End Function

        Function GetStyle(txtStyle As String) As Integer
            Dim valStyle As Integer = Font.NORMAL
            Try
                Select Case txtStyle
                    Case "B"
                        valStyle = Font.BOLD
                    Case "I"
                        valStyle = Font.ITALIC
                    Case "U"
                        valStyle = Font.UNDERLINE
                End Select
            Catch ex As Exception

            End Try
            Return valStyle
        End Function

        'Add by wit 29-01-2020
        'Update 16-06-2020
        'Update by wit Add ColSpan,RowSpan 30-09-2020
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

        'Add by wit 29-01-2020
        'ฟอนต์สัญญาลักษณ์
        Function fontWingdings2(size As Integer) As Font
            Dim Wingdings As BaseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Font/Wingdings/WINGDNG2.TTF"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim font As Font = New Font(Wingdings, size, Font.NORMAL, BaseColor.BLACK)
            Return font
        End Function

        'Add by wit 29-01-2020
        Public Function ImagePadding(path As String, Width As Single, Height As Single, align As Integer,
                                        Optional pdTop As Single = 0.0F,
                                        Optional pdRight As Single = 0.0F,
                                        Optional pdBottom As Single = 0.0F,
                                        Optional pdLeft As Single = 0.0F,
                                        Optional BorderWidth As Single = 0.0F,
                                        Optional Border_BaseColor As BaseColor = Nothing) As PdfPCell

            Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
            image.ScalePercent(Width, Height)
            Dim cell As New PdfPCell(image) With {
                .HorizontalAlignment = align,
                .PaddingTop = pdTop,
                .PaddingRight = pdRight,
                .PaddingBottom = pdBottom,
                .PaddingLeft = pdLeft,
                .BorderWidth = BorderWidth,
                .BorderColor = Border_BaseColor
            }
            Return cell
        End Function

        'Add by wit 07-02-2020
        '//ควบคุม  Function CellPadding เพื่อแสดง Border
        Public Function CellBorder(cell As PdfPCell,
                                        Optional borderTop As Single = 0.0F,
                                        Optional borderRight As Single = 0.0F,
                                        Optional borderBottom As Single = 0.0F,
                                        Optional borderLeft As Single = 0.0F,
                                        Optional BorderBaseColor As BaseColor = Nothing
                                        ) As PdfPCell

            cell.BorderWidthTop = borderTop
            cell.BorderWidthRight = borderRight
            cell.BorderWidthBottom = borderBottom
            cell.BorderWidthLeft = borderLeft
            cell.BorderColor = BorderBaseColor

            Return cell
        End Function

        'Add by wit 10-06-2020
        'ฟอนต์ AngsanaNew
        Function FontAngsanaNew(size As Integer, Optional FontStyle As Integer = Font.NORMAL, Optional fontColor As BaseColor = Nothing) As Font
            Dim fontName As String = String.Empty

            If FontStyle = 1 Then
                fontName = "angsab.ttf"
            ElseIf FontStyle = 2 Then
                fontName = "angsai.ttf"
            ElseIf FontStyle = 3 Then
                fontName = "angsaz.ttf"
            Else
                fontName = "angsa.ttf"
            End If
            FontStyle = 0

            Dim Angsana As BaseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Font/AngsanaNew/" & fontName), BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Return New Font(Angsana, size, FontStyle, If(fontColor Is Nothing, BaseColor.BLACK, fontColor))
        End Function

        'Add by wit 12-06-2020
        'ฟอนต์ TimsRoman
        'แนะนำใช้คู่กับ
        'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE // จะทำให้ Text อยู่กลาง Cell
        'cell.FixedHeight = 20 // กำหนดความสูงด้วยจะดีมาก
        Function FontTimsRoman(size As Integer, Optional FontStyle As Integer = Font.NORMAL, Optional fontColor As BaseColor = Nothing) As Font
            Dim bfTimes As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Return New Font(bfTimes, size, FontStyle, If(fontColor Is Nothing, BaseColor.BLACK, fontColor))
        End Function

        'Add by wit 10-06-2020
        'Position Absolute font Thaisarabun
        Public Sub TextAbsoluteSarabun(writerPDF As PdfWriter, textShow As String, setX As Single, setY As Single,
                   Optional FontStyle As Integer = Font.NORMAL,
                   Optional fontSize As Single = 12,
                   Optional fontColor As BaseColor = Nothing)

            Dim fontName As String = String.Empty
            If FontStyle = 1 Then
                fontName = "THSarabunBold.ttf"
            ElseIf FontStyle = 2 Then
                fontName = "THSarabunItalic.ttf"
            ElseIf FontStyle = 3 Then
                fontName = "THSarabunBoldItalic.ttf"
            Else
                fontName = "THSarabun.ttf"
            End If
            FontStyle = 0

            Dim BaseSarabun As BaseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Font/Sarabun/" & fontName), BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim cb As PdfContentByte = writerPDF.DirectContent
            cb.BeginText()
            cb.SetFontAndSize(BaseSarabun, fontSize)
            If fontColor IsNot Nothing Then
                cb.SetColorFill(BaseColor.GRAY)
            End If
            cb.SetTextMatrix(setX, setY)
            cb.ShowText(textShow)
            cb.EndText()
        End Sub

        'Add by wit 10-06-2020
        'Position Absolute font Arial
        Public Sub TextAbsoluteArial(writerPDF As PdfWriter, textShow As String, setX As Single, setY As Single,
                   Optional FontStyle As Integer = Font.NORMAL,
                   Optional fontSize As Single = 12,
                   Optional fontColor As BaseColor = Nothing)

            Dim fontName As String = String.Empty
            If FontStyle = 1 Then
                fontName = "arialbd.ttf"
            ElseIf FontStyle = 2 Then
                fontName = "arialbi.ttf"
            ElseIf FontStyle = 3 Then
                fontName = "ariali.ttf"
            Else
                fontName = "arial.ttf"
            End If
            FontStyle = 0

            Dim BaseArial As BaseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Font/Arial/" & fontName), BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim cb As PdfContentByte = writerPDF.DirectContent
            cb.BeginText()
            cb.SetFontAndSize(BaseArial, fontSize)
            If fontColor IsNot Nothing Then
                cb.SetColorFill(BaseColor.GRAY)
            End If
            cb.SetTextMatrix(setX, setY)
            cb.ShowText(textShow)
            cb.EndText()
        End Sub


    End Class
End Namespace