Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class labelMatReceive
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate
    Dim controlPDF As New ControlPDF
    Dim tableTrace As New createTableTrace

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'printLabel()
        tableTrace.createLabelLog()
        Dim SQL As String,
            dt As DataTable,
            WHR As String
        Dim docName As String = "labelMatRecieve" & Session("UserName") & ".pdf"
        Dim document As Document = New Document(New Rectangle(288.0F, 216.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*3
        'Dim document As Document = New Document(New Rectangle(288.0F, 180.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*2.5
        Using memoryStream As New System.IO.MemoryStream()
            ' memoryStream.ReadTimeout = 5000
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, memoryStream)
            document.Open()

            Dim iType As String = Request.QueryString("iType").ToString.Trim,
                iNo As String = Request.QueryString("iNo").ToString.Trim,
                iSeq As String = Request.QueryString("iSeq").ToString.Trim

            If iType = "" And iNo = "" And iSeq = "" Then
                printLabel(document)
            Else
                WHR = Conn_SQL.Where("TH001", iType, True, False)
                WHR &= Conn_SQL.Where("TH002", iNo, True, False)
                WHR &= If(iSeq <> "", Conn_SQL.Where("TH003", iSeq, True, False), "")

                'SQL = "select TE001,TE002,TE003,rtrim(TE001)+'-'+rtrim(TE002)+'-'+rtrim(TE003) TE00123, TC003,rtrim(TE011)+'-'+rtrim(TE012) TE011,TE004,TE017,TE018,TE010,TE005,MOCTE.UDF52,TE006 from MOCTE left join MOCTC on TC001=TE001 and TC002=TE002 where 1=1 " & WHR & " order by TE003 "
                SQL = " select rtrim(TH001)+'-'+rtrim(TH002)+'-'+rtrim(TH003) TH0011,TH001,TH002,TH003,TG014,rtrim(TH011)+'-'+rtrim(TH012)+'-'+rtrim(TH013) TH011,TH014,TH057," &
                      " rtrim(TG005)+'-'+rtrim(MA002) TG005,TH004,TH005,TH006,TH009,replace(TH072,'#','') TH072,case when replace(TH010,'*','')='' then TG016 else replace(TH010,'*','') end TH010,TH007,TH008,isnull(PURTH.UDF52,0) UDF52,TH036,isnull(PURTH.UDF53,0) UDF53,TG027,isnull(PURTH.UDF54,0) UDF54  " &
                      " from PURTH left join PURTG on TG001=TH001 and TG002=TH002 left join PURMA on MA001=TG005 where 1=1 " & WHR &
                      " order by TH001,TH002,TH003"

                dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        With dt.Rows(i)
                            Dim packStd As Decimal = .Item("UDF52"),
                                issueQty As Decimal = .Item("TH007")

                            Dim packStdSheet As Decimal = .Item("UDF54"),
                                issueQtySheet As Decimal = .Item("UDF53")

                            Dim fullBox As Integer = If(packStd = 0, 1, Math.Floor(issueQty / packStd)),
                                lastqty As Integer = If(packStd = 0, 0, issueQty Mod packStd),
                                receiveDate As String = If(Trim(.Item("TH014")) = "", "", ConfigDate.strToDateTime(.Item("TH014"), "yyyyMMdd").ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                                productDate As String = If(Trim(.Item("TH057")) = "", "", ConfigDate.strToDateTime(.Item("TH057"), "yyyyMMdd").ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                                expireDate As String = If(Trim(.Item("TH036")) = "", "", ConfigDate.strToDateTime(.Item("TH036"), "yyyyMMdd").ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture))
                            Dim fullBoxSheet As Integer = If(packStdSheet = 0, 1, Math.Floor(issueQtySheet / packStdSheet)),
                                lastqtySheet As Integer = If(packStd = 0, 0, issueQtySheet Mod packStdSheet)

                            Dim allBox As Integer = fullBox + If(lastqty > 0, 1, 0)
                            'allBox = 1
                            For j As Integer = 1 To allBox
                                If j > fullBox Then 'last box
                                    issueQty = lastqty
                                    issueQtySheet = lastqtySheet
                                Else
                                    issueQty = If(packStd > 0, packStd, issueQty)
                                    issueQtySheet = If(packStd > 0, packStdSheet, issueQtySheet)
                                End If
                                printLabel(document, dt.Rows(i), issueQty, receiveDate, productDate, expireDate, issueQtySheet, j & "/" & allBox)
                            Next
                            'record when open for print 
                            Dim fld As Hashtable = New Hashtable,
                                whrhash As Hashtable = New Hashtable
                            'Dim moRcv() As String = Trim(dt.Rows(0).Item("N")).Split("-")
                            Dim mType As String = Trim(dt.Rows(0).Item("TH001")),
                                mNo As String = Trim(dt.Rows(0).Item("TH002")),
                                mSeq As String = Trim(dt.Rows(0).Item("TH003"))
                            fld.Add("docType", mType)
                            fld.Add("docNo", mNo)
                            fld.Add("docSeq", mSeq)
                            fld.Add("fullBox", fullBox)
                            fld.Add("qtyBox", If(packStd > 0, packStd, issueQty))
                            fld.Add("qtyLast", lastqty)
                            fld.Add("CreateBy", Session("UserName"))
                            fld.Add("CreateDate", DateTime.Today.ToString("yyyyMMdd hhmmss"))
                            Conn_SQL.Exec_Sql(Conn_SQL.GetSQL("LabelLog", fld, whrhash, "I"), Conn_SQL.MIS_ConnectionString)

                            SQL = "select count(*) from LabelLog where docType='" & mType & "' and docNo='" & mNo & "' and docSeq='" & mSeq & "' "
                            Dim printTime As String = Conn_SQL.Get_value(SQL, Conn_SQL.MIS_ConnectionString)
                            fld = New Hashtable
                            whrhash = New Hashtable
                            fld.Add("UDF51", printTime)
                            whrhash.Add("TH001", mType)
                            whrhash.Add("TH002", mNo)
                            whrhash.Add("TH003", mSeq)
                            Conn_SQL.Exec_Sql(Conn_SQL.GetSQL("PURTH", fld, whrhash, "U"), Conn_SQL.ERP_ConnectionString)
                        End With
                    Next
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

    Sub printLabel(ByRef document As Document, dr As DataRow,
                   qty As Decimal, receiveDate As String, productDate As String,
                   expireDate As String, qtySheet As Decimal, strPage As String)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing
        Dim color__1 As BaseColor = Nothing
        Dim paddingButton As Single = 2.5F
        Dim tableWidth As Single = 278.0F
        'Dim font As Font = Nothing
        document.NewPage()

        table = New PdfPTable(2)
        table.TotalWidth = 280.0F
        table.LockedWidth = True
        table.SetWidths(New Single() {0.7F, 1.0F})

        Dim fontHead As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)
        Dim fontNormal As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
        Dim fontNormal2 As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)
        Dim fontBold As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)

        'Company Logo
        cell = controlPDF.ImageCell("~/Images/logoHTI.jpg", 20.0F, PdfPCell.ALIGN_CENTER)
        table.AddCell(cell)

        'font = FontFactory.GetFont("Arial", 16, font.BOLD, BaseColor.BLACK)
        phrase = New Phrase()
        'phrase.Add(New Chunk("MATERIALS RECEIVE LABEL", fontHead))
        phrase.Add(New Chunk("INCOMING LABEL", fontHead))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

        table.AddCell(cell)
        document.Add(table)
        'body
        Dim colData As ArrayList = New ArrayList

        colData.Add("Item : " & dr("TH004"))
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontBold, False, paddingButton))
        colData = New ArrayList
        'colData.Add("Desc : 00-003752-BLACK  CODE :213-0513.96-00128 DIRAK(ANODIZED BLAC")
        'colData.Add("Spec : 00-003752-BLACK  CODE :213-0513.96-00128 DIRAK(ANODIZED BLAC")
        colData.Add(controlPDF.limitText("Desc: " & dr("TH005"), 43))
        colData.Add(controlPDF.limitText("Spec: " & dr("TH006"), 43))
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("Lot Mo/MFG No: " & dr("TG027"))
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        Dim strSheet As String = ""
        If qtySheet > 0 Then
            strSheet = "(" & CStr(qtySheet.ToString("#,##0")) & " PCS)"
        End If
        colData.Add("Q'ty: " & CStr(qty.ToString("#,##0.000")) & strSheet)
        colData.Add("Unit: " & dr("TH008"))
        document.Add(controlPDF.rowPrintData(2, tableWidth, New Single() {7.0F, 3.0F}, colData, fontBold, False, paddingButton))
        colData = New ArrayList
        colData.Add("Receive Date: " & receiveDate)
        colData.Add("Bin: " & dr("TH072"))
        document.Add(controlPDF.rowPrintData(2, tableWidth, New Single() {5.5F, 4.5F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("Prod Date: " & productDate)
        colData.Add("Exp Date:" & expireDate)
        document.Add(controlPDF.rowPrintData(2, tableWidth, New Single() {5.5F, 4.5F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("PO No.: " & dr("TH011"))
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("Receive No.: " & dr("TH0011"))
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        'colData = New ArrayList
        'document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add(controlPDF.limitText("Vendor.: " & dr("TG005"), 50))
        colData.Add("../Images/RoHS.png◘1◘I◘20.0F")
        document.Add(controlPDF.rowPrintData(2, 276.0F, New Single() {6.5F, 3.5F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("(" & strPage & ")")
        colData.Add("FM-WH-09-B")
        document.Add(controlPDF.rowPrintData(2, 276.0F, New Single() {5.5F, 5.5F}, colData, fontNormal, False, paddingButton, "R"))

    End Sub

    Sub printLabel(ByRef document As Document)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing
        Dim color__1 As BaseColor = Nothing
        Dim paddingButton As Single = 2.5F
        Dim tableWidth As Single = 278.0F
        'Dim font As Font = Nothing
        document.NewPage()

        table = New PdfPTable(2)
        table.TotalWidth = 280.0F
        table.LockedWidth = True
        table.SetWidths(New Single() {0.7F, 1.0F})

        Dim fontHead As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)
        Dim fontNormal As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
        Dim fontNormal2 As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)
        Dim fontBold As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)

        'Company Logo
        cell = controlPDF.ImageCell("~/Images/logoHTI.jpg", 20.0F, PdfPCell.ALIGN_CENTER)
        table.AddCell(cell)

        'font = FontFactory.GetFont("Arial", 16, font.BOLD, BaseColor.BLACK)
        phrase = New Phrase()
        'phrase.Add(New Chunk("MATERIALS RECEIVE LABEL", fontHead))
        phrase.Add(New Chunk("INCOMING LABEL", fontHead))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

        table.AddCell(cell)
        document.Add(table)
        'body
        Dim colData As ArrayList = New ArrayList

        colData.Add("Item : .............................")
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontBold, False, paddingButton))
        colData = New ArrayList
        'colData.Add("Desc : 00-003752-BLACK  CODE :213-0513.96-00128 DIRAK(ANODIZED BLAC")
        'colData.Add("Spec : 00-003752-BLACK  CODE :213-0513.96-00128 DIRAK(ANODIZED BLAC")
        colData.Add("Desc: ..........................")
        colData.Add("Spec: ..........................")
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("Lot Mo/MFG No: ..............................")
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        Dim strSheet As String = ""

        colData.Add("Q'ty: ....................")
        colData.Add("Unit: ..........")
        document.Add(controlPDF.rowPrintData(2, tableWidth, New Single() {7.0F, 3.0F}, colData, fontBold, False, paddingButton))
        colData = New ArrayList
        colData.Add("Receive Date: ....................")
        colData.Add("Bin: ...............")
        document.Add(controlPDF.rowPrintData(2, tableWidth, New Single() {5.5F, 4.5F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("Prod Date:.............")
        colData.Add("Exp Date:..............")
        document.Add(controlPDF.rowPrintData(2, tableWidth, New Single() {5.5F, 4.5F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("PO No.: ....................")
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("Receive No.: ......................")
        document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        'colData = New ArrayList
        'document.Add(controlPDF.rowPrintData(1, tableWidth, New Single() {1.0F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("Vendor.: ...............")
        colData.Add("../Images/RoHS.png◘1◘I◘20.0F")
        document.Add(controlPDF.rowPrintData(2, 276.0F, New Single() {6.5F, 3.5F}, colData, fontNormal, False, paddingButton))
        colData = New ArrayList
        colData.Add("(......./........)")
        colData.Add("FM-WH-09-B")
        document.Add(controlPDF.rowPrintData(2, 276.0F, New Single() {5.5F, 5.5F}, colData, fontNormal, False, paddingButton, "R"))

    End Sub

End Class