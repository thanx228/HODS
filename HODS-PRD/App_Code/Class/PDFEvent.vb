Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MIS_HTI.PDF
Imports MIS_HTI.DataControl

Public Class PDFEvent
    Inherits PdfPageEventHelper

    ' This is the contentbyte object of the writer
    Private cb As PdfContentByte

    ' we will put the final number of pages in a template
    Private headerTemplate As PdfTemplate

    Private footerTemplate As PdfTemplate

    ' this is the BaseFont we are going to use for the header / footer
    Private bf As BaseFont = Nothing

    ' This keeps track of the creation time
    Dim pdfCont As New PDFControl


#Region "Fields"

    Private _header As String
    Private _header2 As String
    Private _iso_doc As String
    Private _effect_date As String


    Sub New()
        Header = ""
        Header2 = ""
        DOC_ISO = ""
        effect_date = ""
    End Sub

    Sub New(txtHead As String, txtISO As String, effectiveDate As String,
            Optional strSplit As String = VarIni.char8)
        Dim tt() As String = txtHead.Split(strSplit)
        Header = tt(0)
        Dim h2 As String = ""
        If tt.Length > 1 Then
            h2 = tt(1)
        End If
        Header2 = h2
        DOC_ISO = txtISO
        effect_date = effectiveDate
    End Sub

#End Region
#Region "Properties"

    Public Property Header As String
        Get
            Return _header
        End Get
        Set
            _header = Value
        End Set
    End Property

    Public Property Header2 As String
        Get
            Return _header2
        End Get
        Set
            _header2 = Value
        End Set
    End Property

    Public Property DOC_ISO As String
        Get
            Return _iso_doc
        End Get
        Set
            _iso_doc = Value
        End Set
    End Property

    Public Property effect_date As String
        Get
            Return _effect_date
        End Get
        Set
            _effect_date = If(Value = "", Value, "Effective Date " & Value)
        End Set

    End Property
#End Region

    Public Overrides Sub OnOpenDocument(ByVal writer As PdfWriter, ByVal document As Document)
        Try
            'PrintTime = DateTime.Now
            'bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            bf = pdfCont.BaseFontSarabun()
            cb = writer.DirectContent
            headerTemplate = cb.CreateTemplate(100, 100)
            footerTemplate = cb.CreateTemplate(50, 50)
        Catch de As DocumentException

        Catch ioe As System.IO.IOException

        End Try

    End Sub

    Public Overrides Sub OnEndPage(ByVal writer As PdfWriter, ByVal document As Document)
        MyBase.OnEndPage(writer, document)

        Dim baseFontNormal As Font = pdfCont.FontSarabun(12, Font.NORMAL)
        Dim baseFontBig As Font = pdfCont.FontSarabun(12, Font.BOLD)
        'Create PdfTable object
        'We will have to create separate cells to include image logo and 2 separate strings
        'Add paging to footer
        Dim fromButtom As Single = 10
        'date time print
        pdfCont.Text(cb, "HOO THAI INDUSTRIAL CO.,LTD.", document.PageSize.GetLeft(10), document.PageSize.GetBottom(fromButtom), fontColor:=BaseColor.GRAY)
        'page number of total page
        Dim getX As Single = document.PageSize.Width / 2
        Dim text As String = "Page " & writer.PageNumber & " of "
        pdfCont.Text(cb, text, getX, document.PageSize.GetBottom(fromButtom), fontColor:=BaseColor.GRAY)
        Dim len As Single = bf.GetWidthPoint(text, 12)
        cb.AddTemplate(headerTemplate, getX + len, document.PageSize.GetBottom(fromButtom))

        'iso doc number
        len = bf.GetWidthPoint(DOC_ISO, 12)
        pdfCont.Text(cb, DOC_ISO, document.PageSize.GetRight(15) - len, document.PageSize.GetBottom(fromButtom + 13), fontColor:=BaseColor.GRAY)
        'effective date show
        Dim txtDate As String = "Print : " & Now.ToString("dd/MMM/yyyy [HH:mm]")
        len = bf.GetWidthPoint(txtDate, 12)
        'pdfCont.Text(cb, _effect_date, document.PageSize.GetRight(15) - len, document.PageSize.GetBottom(fromButtom - 8), fontColor:=BaseColor.GRAY)
        pdfCont.Text(cb, txtDate, document.PageSize.GetRight(15) - len, document.PageSize.GetBottom(fromButtom), fontColor:=BaseColor.GRAY)

        'len = bf.GetWidthPoint(DOC_ISO, 12)
        'cb.AddTemplate(footerTemplate, (document.PageSize.GetRight(10) + len), document.PageSize.GetBottom(20))

        'add header show
        Dim colWidth() As Single = New Single() {1.0F}
        'Dim pdfTab As PdfPTable = New PdfPTable(2)
        Dim pdfTab As New PdfPTable(colWidth.Length) With {
            .TotalWidth = document.PageSize.Width - 10,
            .LockedWidth = True
        }
        pdfTab.SetWidths(colWidth)
        'add all three cells into PdfTable
        pdfTab.AddCell(pdfCont.ImageCell("~/Images/logo-hti.png", 50.0F, PdfPCell.ALIGN_CENTER, 0))
        'pdfTab.AddCell(pdfCont.Cell("", 20, fixHieght:=25.0F, CellBorder:=0, CellAlignH:=PdfPCell.ALIGN_CENTER, CellAlignV:=PdfPCell.ALIGN_CENTER))
        pdfTab.AddCell(pdfCont.Cell(Header, 18, fixHieght:=25.0F, CellBorder:=0, CellAlignH:=PdfPCell.ALIGN_CENTER, CellAlignV:=PdfPCell.ALIGN_BOTTOM))
        If Not String.IsNullOrEmpty(Header2) Then
            'pdfTab.AddCell(pdfCont.Cell("", 20, fixHieght:=25.0F, CellBorder:=3, CellAlignH:=PdfPCell.ALIGN_CENTER, CellAlignV:=PdfPCell.ALIGN_CENTER))
            pdfTab.AddCell(pdfCont.Cell(Header2, 16, fixHieght:=25.0F, CellBorder:=0, CellAlignH:=PdfPCell.ALIGN_CENTER, CellAlignV:=PdfPCell.ALIGN_TOP))
        End If
        pdfTab.WidthPercentage = 95
        'pdfTab.TotalWidth = (document.PageSize.Width - 80.0!)
        'pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;    
        'call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
        'first param is start row. -1 indicates there is no end row and all the rows to be included to write
        'Third and fourth param is x and y position to start writing
        pdfTab.WriteSelectedRows(0, -1, 1, document.PageSize.GetTop(10), writer.DirectContent)
        pdfCont.Line(cb, 5, document.PageSize.GetBottom(20), document.PageSize.GetRight(5), document.PageSize.GetBottom(20), BaseColor.GRAY)
    End Sub

    Public Overrides Sub OnCloseDocument(ByVal writer As PdfWriter, ByVal document As Document)
        MyBase.OnCloseDocument(writer, document)
        headerTemplate.BeginText()
        headerTemplate.SetFontAndSize(bf, 12)
        headerTemplate.SetTextMatrix(0, 0)
        headerTemplate.ShowText(writer.PageNumber.ToString)
        headerTemplate.EndText()

        'footerTemplate.BeginText()
        'footerTemplate.SetFontAndSize(bf, 12)
        'footerTemplate.SetTextMatrix(0, 0)
        'footerTemplate.ShowText("")
        'footerTemplate.EndText()
    End Sub
End Class