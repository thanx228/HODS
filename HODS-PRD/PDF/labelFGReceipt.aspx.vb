Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class labelFGReceipt
    Inherits System.Web.UI.Page
    'Dim Conn_SQL As New ConnSQL
    'Dim ConfigDate As New ConfigDate
    Dim controlPDF As New ControlPDF
    Dim tableTrace As New createTableTrace

    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim dateCont As New DateControl

    Dim Bar128 As New Barcode128
    Dim Img128 As Image
    Dim content As PdfContentByte



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'printLabel()
        tableTrace.createLabelLog()
        Dim SQL As String,
            dr As DataRow
        Dim prtFor As String = Request.QueryString("prtFor").ToString.Trim
        Dim docName As String = "labelFGLabel" & Session("UserName") & ".pdf"

        Dim document As Document = New Document(New Rectangle(288.0F, 216.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*3
        Using memoryStream As New System.IO.MemoryStream()
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, memoryStream)
            document.Open()
            Dim docNo As String = Request.QueryString("docno").ToString.Trim
            If docNo = "" Then
                dr = Nothing
                'If prtFor = "N" Then 'Hoothai
                '    printLabelHoothai(document, dr, 0, 0, 0)
                'Else 'jinpao
                '    printLabelJinpao(document, dr, 0, 0, 0)
                'End If

                Select Case prtFor
                    Case "HT" 'hoothai format print
                        printLabelHoothai(document, dr, 0, 0, 0)
                    Case "JP" 'jinpao format print
                        printLabelJinpao(document, dr, 0, 0, 0)
                    Case "FS" 'custorm of jinpao (Fisher)
                        printLabelFisher(document, dr, 0, 0, 0, writer)
                End Select
            Else
                SQL = "select Rtrim(MOCTA.TA001)+'-'+MOCTA.TA002 C, Rtrim(MOCTA.TA026)+'-'+RTRIM (MOCTA.TA027)+'-'+RTRIM(MOCTA.TA028) D," &
                  "  isnull(custPO,'') E,MOCTA.TA006 F,MOCTA.TA034 G, COPMA.MA002 L ," &
                  " case when isnull(COPTD.TD014,'')='' then SFCTC.TC049 else COPTD.TD014 end  H, " &
                  " INVMB.MB014  N,SFCTC.TC036 O,SFCTB.TB015 X," &
                  " case when F.DocNo = '' then INVMB.MB073 else F.qtyCtn end  P," &
                  " case when F.DocNo = '' then INVMB.MB075 else F.CtnWgh end  Q,  " &
                  " MOCTA.UDF03 T,F.PackBy Z, SFCTC.TC056 Y, " &
                  " SFCTC.TC001,SFCTC.TC002,SFCTC.TC003,SFCTC.UDF01,COPTD.TD020," &
                  " INVMB.UDF05,INVMB.UDF06,INVMB.UDF01 INVUDF01,MG003,MG005 from MOCTA " &
                  " left join COPTD on COPTD.TD001 = MOCTA.TA026 And COPTD.TD002 = MOCTA.TA027 And COPTD.TD003 = MOCTA.TA028 " &
                  " left join COPTC on COPTC.TC001 = MOCTA.TA026 And COPTC.TC002 = MOCTA.TA027 " &
                  " left join COPMA on COPMA.MA001 = COPTC.TC004 " &
                  " left join SFCTC on SFCTC.TC004 = MOCTA.TA001 And SFCTC.TC005 = MOCTA.TA002 " &
                  " left join SFCTB on SFCTB.TB001 = SFCTC.TC001 And SFCTB.TB002 = SFCTC.TC002 " &
                  " left join CMSMG on CMSMG.MG001 = COPTC.TC004 And CMSMG.MG002 = MOCTA.TA006 " &
                  " left join INVMB on INVMB.MB001 = MOCTA.TA006" &
                  " left join " & VarIni.DBMIS & "..FGLabel F on F.moType = SFCTB.TB001 And F.moNo = SFCTB.TB002 And F.moSeq = SFCTC.TC003 " &
                  " where  F.DocNo='" & Request.QueryString("docno").ToString.Trim & "' " &
                  " order by SFCTB.TB001,SFCTB.TB002,SFCTB.TB003 "

                'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                dr = dbConn.QueryDataRow(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                If dr IsNot Nothing Then

                    Dim qty As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "O")
                    Dim qtyCnt As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "P")
                    Dim wghCarton As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "Q")
                    Dim itemWgh As Decimal = dtCont.IsDBNullDataRowDecimal(dr, "N")
                    Dim full As Integer = 0
                    Dim notFull As Integer = qty
                    Dim notFullGross As Decimal = 0
                    If qtyCnt > 0 Then
                        full = Math.Floor(qty / qtyCnt)
                        notFull = qty Mod qtyCnt
                    End If
                    For i As Integer = 1 To full
                        Select Case prtFor
                            Case "HT" 'hoothai format print
                                printLabelHoothai(document, dr, qtyCnt, (qtyCnt * itemWgh), (qtyCnt * itemWgh) + wghCarton)
                            Case "JP" 'jinpao format print
                                printLabelJinpao(document, dr, qtyCnt, (qtyCnt * itemWgh), (qtyCnt * itemWgh) + wghCarton)
                            Case "FS" 'custorm of jinpao (Fisher)
                                printLabelFisher(document, dr, qtyCnt, (qtyCnt * itemWgh), (qtyCnt * itemWgh) + wghCarton, writer)
                        End Select
                        'If prtFor = "N" Then 'Hoothai
                        '    printLabelHoothai(document, dr, qtyCnt, (qtyCnt * itemWgh), (qtyCnt * itemWgh) + wghCarton)
                        'Else 'jinpao
                        '    printLabelJinpao(document, dr, qtyCnt, (qtyCnt * itemWgh), (qtyCnt * itemWgh) + wghCarton)
                        'End If
                    Next
                    If notFull > 0 Then
                        Select Case prtFor
                            Case "HT" 'hoothai format print
                                printLabelHoothai(document, dr, notFull, (notFull * itemWgh), (notFull * itemWgh) + wghCarton)
                            Case "JP" 'jinpao format print
                                printLabelJinpao(document, dr, notFull, (notFull * itemWgh), (notFull * itemWgh) + wghCarton)
                            Case "FS" 'custorm of jinpao (Fisher)
                                printLabelFisher(document, dr, notFull, (notFull * itemWgh), (notFull * itemWgh) + wghCarton, writer)
                        End Select

                        'If prtFor = "N" Then 'hoothai
                        '    printLabelHoothai(document, dr, notFull, (notFull * itemWgh), (notFull * itemWgh) + wghCarton)
                        'Else 'jinpao
                        '    printLabelJinpao(document, dr, notFull, (notFull * itemWgh), (notFull * itemWgh) + wghCarton)
                        'End If
                    End If

                    'record when open for print 
                    Dim fld As Hashtable = New Hashtable,
                    whr As Hashtable = New Hashtable
                    'Dim moRcv() As String = Trim(dt.Rows(0).Item("N")).Split("-")
                    fld.Add("docType", Trim(dtCont.IsDBNullDataRow(dr, "TC001")))
                    fld.Add("docNo", Trim(dtCont.IsDBNullDataRow(dr, "TC002")))
                    fld.Add("docSeq", Trim(dtCont.IsDBNullDataRow(dr, "TC003")))
                    fld.Add("fullBox", full)
                    fld.Add("qtyBox", qtyCnt)
                    fld.Add("qtyLast", notFull)
                    fld.Add("CreateBy", Session("UserName"))
                    fld.Add("CreateDate", DateTime.Today.ToString("yyyyMMdd hhmmss"))

                    dbConn.TransactionSQL(dbConn.GetSQL("LabelLog", fld, whr, "I"), VarIni.DBMIS, dbConn.WhoCalledMe)

                Else
                    document.NewPage()
                    Dim phrase As Phrase = Nothing
                    Dim cell As PdfPCell = Nothing
                    Dim table As PdfPTable = Nothing
                    Dim color__1 As BaseColor = Nothing
                    Dim font As Font = Nothing

                    table = New PdfPTable(1)
                    table.TotalWidth = 280.0F
                    table.LockedWidth = True
                    table.SetWidths(New Single() {1.0F})

                    font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("No data Print", font))
                    cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

                    table.AddCell(cell)
                    document.Add(table)
                End If
            End If
            'document.Add(table)
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

    Sub printLabelJinpao(ByRef document As Document, dr As DataRow, qty As Integer, netWgh As Decimal, grossWgh As Decimal)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing
        Dim color__1 As BaseColor = Nothing
        Dim fontHead As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)
        Dim fontNormal As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
        Dim fontBold As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)
        document.NewPage()

        table = New PdfPTable(2)
        table.TotalWidth = 280.0F
        table.LockedWidth = True
        table.SetWidths(New Single() {0.7F, 1.0F})
        Dim prtFor As String = Request.QueryString("prtFor").ToString.Trim

        'Company Logo
        cell = controlPDF.ImageCell("~/Images/logo-jp2.jpg", 20.0F, PdfPCell.ALIGN_CENTER)
        table.AddCell(cell)

        'font = FontFactory.GetFont("Arial", 14, font.BOLD, BaseColor.BLACK)
        phrase = New Phrase()
        phrase.Add(New Chunk("FINISHED GOODS LABEL", fontHead))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

        table.AddCell(cell)
        document.Add(table)
        'body
        Dim colData As ArrayList = New ArrayList
        colData.Add("Cust Name : " & dtCont.IsDBNullDataRow(dr, "TD020") & "◘2")
        colData.Add("Item:" & dtCont.IsDBNullDataRow(dr, "F") & "◘2")
        'font = FontFactory.GetFont("Arial", 10, font.NORMAL, BaseColor.BLACK)
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        colData.Add("Part No: " & dtCont.IsDBNullDataRow(dr, "H") & "◘2")
        'font = FontFactory.GetFont("Arial", 12, font.BOLD, BaseColor.BLACK)
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontBold, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        Dim txt As String = dtCont.IsDBNullDataRow(dr, "G")
        colData.Add("Part Name: " & If(txt.Length > 30, txt.Substring(0, 30), txt) & "◘2")
        'Dim batchTxt As String = ""
        'Dim label As String = "SO No:"
        'Dim val As String = "D"
        'If prtFor <> "N" Then
        '    batchTxt = " Batch No: " & dr.Item("T").ToString.Trim
        '    label = "PO :"
        '    val = "E"
        'End If
        colData.Add("MFG No:" & dtCont.IsDBNullDataRow(dr, "C") & "◘2")
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)
        'colData.Add(label & " : " & dr.Item(val).ToString.Trim & "◘2")
        colData = New ArrayList
        colData.Add("Qty/Carton: " & qty.ToString("#,###") & " Pcs◘2")
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontBold, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        colData.Add("Net Weight: " & netWgh.ToString("#,###.##") & " Kgs")
        colData.Add("Gross Weight: " & grossWgh.ToString("#,###.##") & " Kgs")
        'colData.Add("Net Weight: 100.00 Kgs")
        'colData.Add("Gross Weight: 100.50 Kgs")
        colData.Add("Packing Date:" & dateCont.dateShow2(dtCont.IsDBNullDataRow(dr, "X"), "-") & "◘2")
        colData.Add("Pack By: " & dr.Item("Z").ToString.Trim & "◘2")
        'colData.Add("Date of Delivery: ◘2")
        'colData = New ArrayList
        colData.Add("Bin: " & dtCont.IsDBNullDataRow(dr, "Y"))
        colData.Add("../Images/RoHS.png◘1◘I◘20.0F")
        table = controlPDF.rowPrintData(2, 276.0F, New Single() {5.0F, 5.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)

    End Sub

    Sub printLabelHoothai(ByRef document As Document, dr As DataRow, qty As Integer, netWgh As Decimal, grossWgh As Decimal)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing
        Dim color__1 As BaseColor = Nothing
        Dim fontHead As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)
        Dim fontNormal As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
        Dim fontBold As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)
        document.NewPage()

        table = New PdfPTable(2)
        table.TotalWidth = 280.0F
        table.LockedWidth = True
        table.SetWidths(New Single() {0.7F, 1.0F})
        Dim prtFor As String = Request.QueryString("prtFor").ToString.Trim

        'Company Logo
        cell = controlPDF.ImageCell("~/Images/logoHTI.jpg", 20.0F, PdfPCell.ALIGN_CENTER)
        table.AddCell(cell)

        'font = FontFactory.GetFont("Arial", 14, font.BOLD, BaseColor.BLACK)
        phrase = New Phrase()
        phrase.Add(New Chunk("FINISHED GOODS LABEL", fontHead))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

        table.AddCell(cell)
        document.Add(table)
        'body
        Dim colData As ArrayList = New ArrayList
        'colData.Add("Cust Name : " & dr.Item("TD020").ToString.Trim & "◘2")
        'colData.Add("Item: " & dr.Item("F").ToString.Trim & "◘2")
        'font = FontFactory.GetFont("Arial", 10, font.NORMAL, BaseColor.BLACK)
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        colData.Add("Part No: " & dtCont.IsDBNullDataRow(dr, "H", "....................."))
        colData.Add("Rev: " & dtCont.IsDBNullDataRow(dr, "UDF05", "........"))
        'font = FontFactory.GetFont("Arial", 12, font.BOLD, BaseColor.BLACK)
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {7.0F, 3.0F}, colData, fontBold, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        Dim txt As String = dtCont.IsDBNullDataRow(dr, "G", ".....................")
        colData.Add("Part Name: " & If(txt.Length > 30, txt.Substring(0, 30), txt) & "◘2")
        colData.Add("WO No:" & dtCont.IsDBNullDataRow(dr, "C", ".....................") & "◘2")
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {7.0F, 3.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)
        'colData.Add(label & " : " & dr.Item(val).ToString.Trim & "◘2")
        colData = New ArrayList
        colData.Add("Qty/Carton: " & If(qty = 0, "..........", qty.ToString("#,###")) & " Pcs◘2")
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontBold, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        colData.Add("Net Weight: " & If(netWgh = 0, "........", netWgh.ToString("#,###.##")) & " Kgs")
        colData.Add("Gross Weight: " & If(netWgh = 0, ".......", grossWgh.ToString("#,###.##")) & " Kgs")

        colData.Add("Packing Date:" & dateCont.dateShow2(dtCont.IsDBNullDataRow(dr, "X"), "-"))
        Dim outCont As New OutputControl
        colData.Add("Pos:" & outCont.checkStringValue(dtCont.IsDBNullDataRow(dr, "INVUDF01", "........")))

        colData.Add("Pack By: " & dtCont.IsDBNullDataRow(dr, "Z", ".....................") & "◘2")
        colData.Add("Lot No: " & dtCont.IsDBNullDataRow(dr, "UDF01", ".....................") & "◘2")
        table = controlPDF.rowPrintData(2, 276.0F, New Single() {5.0F, 5.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        colData.Add("Test Report No: " & dtCont.IsDBNullDataRow(dr, "UDF06", ".....................")) '516026161116048
        colData.Add("../Images/ERS.jpg◘1◘I◘20.0F")
        table = controlPDF.rowPrintData(2, 276.0F, New Single() {7.0F, 3.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        colData.Add("FM-PD-12-D◘2")
        table = controlPDF.rowPrintData(2, 276.0F, New Single() {7.0F, 3.0F}, colData, fontNormal, False, 4.0F, "R")
        document.Add(table)
    End Sub

    Sub printLabelFisher(ByRef document As Document, dr As DataRow, qty As Integer, netWgh As Decimal, grossWgh As Decimal, writer As PdfWriter)

        Dim drCont As New DataRowControl(dr)

        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing
        Dim color__1 As BaseColor = Nothing
        Dim fontHead As Font = FontFactory.GetFont("Calibri", 15, Font.BOLD, BaseColor.BLACK)
        Dim fontNormal As Font = FontFactory.GetFont("Calibri", 10, Font.NORMAL, BaseColor.BLACK)
        Dim fontBold As Font = FontFactory.GetFont("Calibri", 15, Font.BOLD, BaseColor.BLACK)
        Dim fontsmall As Font = FontFactory.GetFont("Calibri", 8, Font.NORMAL, BaseColor.BLACK)

        document.NewPage()
        table = New PdfPTable(3) With {
            .TotalWidth = 280.0F,
            .LockedWidth = True
        }
        table.SetWidths(New Single() {0.6F, 1.2F, 0.96F})
        'Dim prtFor As String = Request.QueryString("prtFor").ToString.Trim

        phrase = New Phrase From {
            New Chunk("P/N", fontBold)
        }
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)
        cell.UseVariableBorders = True
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 1.0F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 1.0F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        phrase = New Phrase()
        'Dim txtspec() As String = dr.Item(IMAAL.Specifaction).ToString.Split(".")
        'phrase.Add(New Chunk(If((txtspec(0).Length - 3) > 13, Left(txtspec(0).Substring(0, 13), 13), Left(txtspec(0), txtspec(0).Length - 3)), fontBold))


        Dim jinpaoSpec As String = drCont.Text("H")
        Dim spec As String = dtCont.IsDBNullDataRow(dr, "MG003", jinpaoSpec)
        Dim specCut() As String = spec.Split("REV")
        Dim txtspec As String = If(specCut.Count >= 1, specCut(0), "")
        Dim txtRev As String = If(specCut.Count >= 2, specCut(1), "")

        phrase.Add(New Chunk(If(txtspec.Length > 23, txtspec.Substring(0, 23), txtspec), fontBold))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 0.5F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 1.0F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        cell.Colspan = 2
        table.AddCell(cell)
        document.Add(table)

        table = New PdfPTable(2) With {
            .TotalWidth = 280.0F,
            .LockedWidth = True
        }
        table.SetWidths(New Single() {1.0F, 1.0F})

        Content = writer.DirectContent
        Bar128 = New Barcode128 With {
            .Code = txtspec,
            .Font = Nothing,
            .StartStopText = False,
            .BarHeight = 40,
            .Extended = True
        }
        Img128 = Bar128.CreateImageWithBarcode(Content, BaseColor.BLACK, BaseColor.BLACK)

        cell = ImageBarcode(Img128, 60, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 1.0F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.PaddingBottom = 10.0F
        cell.PaddingTop = 9.0F
        cell.Colspan = 2
        table.AddCell(cell)

        'content = writer.DirectContent
        'Bar128 = New Barcode128 With {
        '    .Code = txtRev,
        '    .Font = Nothing,
        '    .StartStopText = False,
        '    .BarHeight = 40,
        '    .Extended = True
        '}
        'Img128 = Bar128.CreateImageWithBarcode(content, BaseColor.BLACK, BaseColor.BLACK)

        'cell = ImageBarcode(Img128, 60, PdfPCell.ALIGN_MIDDLE)
        'cell.BorderColorLeft = BaseColor.BLACK
        'cell.BorderColorRight = BaseColor.BLACK
        'cell.BorderColorTop = BaseColor.BLACK
        'cell.BorderColorBottom = BaseColor.BLACK
        'cell.BorderWidthLeft = 0.5F
        'cell.BorderWidthRight = 1.0F
        'cell.BorderWidthTop = 0.5F
        'cell.BorderWidthBottom = 0.5F
        'cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        'cell.PaddingBottom = 10.0F
        'cell.PaddingTop = 9.0F
        'table.AddCell(cell)
        document.Add(table)


        table = New PdfPTable(3)
        table.TotalWidth = 280.0F
        table.LockedWidth = True
        table.SetWidths(New Single() {0.6F, 1.2F, 0.96F})

        phrase = New Phrase From {
            New Chunk("Desc", fontNormal)
        }
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_MIDDLE)
        cell.UseVariableBorders = True
        'cell.BorderColorLeft = BaseColor.BLACK
        'cell.BorderColorRight = BaseColor.BLACK
        'cell.BorderColorTop = BaseColor.BLACK
        'cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderColor = BaseColor.BLACK
        cell.BorderWidthLeft = 1.0F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        Dim JinpaoItemDesc As String = dtCont.IsDBNullDataRow(dr, "MG005")
        Dim itemDesc As String = dtCont.IsDBNullDataRow(dr, "G", JinpaoItemDesc)
        phrase = New Phrase From {
            New Chunk(If(itemDesc.Length > 20, itemDesc.Substring(0, 20), itemDesc), fontBold)
        }
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 0.5F
        cell.BorderWidthRight = 1.0F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        cell.Colspan = 2
        table.AddCell(cell)
        'document.Add(table)

        'table = New PdfPTable(2)
        'table.TotalWidth = 280.0F
        'table.LockedWidth = True
        'table.SetWidths(New Single() {0.15F, 1.0F})

        phrase = New Phrase From {
            New Chunk("Supplier", fontNormal)
        }
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_MIDDLE)
        cell.UseVariableBorders = True
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 1.0F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        phrase = New Phrase()
        phrase.Add(New Chunk("JINPAO PRECISION INDUSTRY CO.,LTD.", fontNormal))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 0.5F
        cell.BorderWidthRight = 1.0F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        cell.Colspan = 2
        table.AddCell(cell)
        'document.Add(table)

        'table = New PdfPTable(3)
        'table.TotalWidth = 280.0F
        'table.LockedWidth = True
        'table.SetWidths(New Single() {0.36F, 1.0F, 1.4F})

        phrase = New Phrase()
        phrase.Add(New Chunk("QTY", fontBold))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)
        cell.UseVariableBorders = True
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 1.0F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        phrase = New Phrase()
        phrase.Add(New Chunk(qty.ToString("#,##0") & " Pcs.", fontBold))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.WHITE
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 0.5F
        cell.BorderWidthRight = 0.0F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        Content = writer.DirectContent
        Bar128 = New Barcode128
        If qty.ToString("#,##0") = String.Empty Then
            Bar128.Code = "0"
            'Bar39.InkSpreading = 10
        Else
            Bar128.Code = qty.ToString("###0")
        End If
        Bar128.Font = Nothing
        Bar128.StartStopText = False
        Bar128.Extended = True
        Img128 = Bar128.CreateImageWithBarcode(Content, BaseColor.BLACK, BaseColor.BLACK)

        cell = ImageBarcode(Img128, 100.0F, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.WHITE
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 0.0F
        cell.BorderWidthRight = 1.0F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)
        'document.Add(table)

        'table = New PdfPTable(3)
        'table.TotalWidth = 280.0F
        'table.LockedWidth = True
        'table.SetWidths(New Single() {0.36F, 1.0F, 1.4F})

        phrase = New Phrase()
        phrase.Add(New Chunk("Lot No.", fontNormal))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_MIDDLE)
        cell.UseVariableBorders = True
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 1.0F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        phrase = New Phrase()
        phrase.Add(New Chunk(dr.Item("C").ToString.Trim, fontNormal))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 0.5F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.5F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        phrase = New Phrase()
        phrase.Add(New Chunk("Color FIFO", fontNormal))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.WHITE
        cell.BorderWidthLeft = 0.5F
        cell.BorderWidthRight = 1.0F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 0.0F
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)
        'document.Add(table)

        'table = New PdfPTable(3)
        'table.TotalWidth = 280.0F
        'table.LockedWidth = True
        'table.SetWidths(New Single() {0.36F, 1.0F, 1.4F})

        phrase = New Phrase()
        phrase.Add(New Chunk("MFG.Date", fontNormal))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_MIDDLE)
        cell.UseVariableBorders = True
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 1.0F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 1.0F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        phrase = New Phrase()
        phrase.Add(New Chunk(dateCont.dateShow2(dtCont.IsDBNullDataRow(dr, "X"), "-"), fontNormal))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.BLACK
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 0.5F
        cell.BorderWidthRight = 0.5F
        cell.BorderWidthTop = 0.5F
        cell.BorderWidthBottom = 1.0F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)

        phrase = New Phrase()
        phrase.Add(New Chunk("Fisher & Paykel", fontsmall))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, PdfPCell.ALIGN_BOTTOM)
        cell.BorderColorLeft = BaseColor.BLACK
        cell.BorderColorRight = BaseColor.BLACK
        cell.BorderColorTop = BaseColor.WHITE
        cell.BorderColorBottom = BaseColor.BLACK
        cell.BorderWidthLeft = 0.5F
        cell.BorderWidthRight = 1.0F
        cell.BorderWidthTop = 0.0F
        cell.BorderWidthBottom = 1.0F
        cell.PaddingBottom = 7.0F
        cell.PaddingTop = 6.0F
        table.AddCell(cell)
        document.Add(table)

    End Sub

    Public Function ImageBarcode(path As Image, scale As Single, align As Integer) As PdfPCell
        path.ScalePercent(scale)
        Dim cell As New PdfPCell(path)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 2.5F
        cell.PaddingTop = 2.5F
        Return cell
    End Function


End Class