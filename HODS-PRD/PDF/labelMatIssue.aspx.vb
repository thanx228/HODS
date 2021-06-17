Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class labelMatIssue
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
        Dim docName As String = "labelMatIssue" & Session("UserName") & ".pdf"
        Dim document As Document = New Document(New Rectangle(288.0F, 216.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*3
        'Dim document As Document = New Document(New Rectangle(288.0F, 180.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*2.5
        Using memoryStream As New System.IO.MemoryStream()
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, memoryStream)
            document.Open()

            Dim iType As String = Request.QueryString("iType").ToString.Trim,
                iNo As String = Request.QueryString("iNo").ToString.Trim,
                iSeq As String = Request.QueryString("iSeq").ToString.Trim
            WHR = Conn_SQL.Where("TE001", iType, True, False)
            WHR &= Conn_SQL.Where("TE002", iNo, True, False)
            WHR &= If(iSeq <> "", Conn_SQL.Where("TE003", iSeq, True, False), "")

            SQL = " select TE001,TE002,TE003,rtrim(TE001)+'-'+rtrim(TE002)+'-'+rtrim(TE003) TE00123, TC003,rtrim(TE011)+'-'+rtrim(TE012) TE011," &
                  " TE004,TE017,TE018,MOCTE.UDF01,TE005,isnull(MOCTE.UDF52,0) UDF52,TE006,MOCTA.TA035,replace(MOCTA.TA057,'*','') TA057,MB032," &
                  " isnull(MOCTE.UDF53,0) UDF53,isnull(MOCTE.UDF54,0) UDF54,isnull(MOCTE.UDF54,0) UDF55 " &
                  " from MOCTE " &
                  " left join MOCTC on TC001=TE001 And TC002=TE002 " &
                  " left join MOCTA on TA001=TE011 And TA002=TE012 " &
                  " left join INVMB on MB001=TE004 where 1=1 " & WHR &
                  " order by TE003 "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        Dim packStd As Decimal = .Item("UDF54"),
                            issueQty As Decimal = .Item("TE005")

                        Dim fullBox As Integer = If(packStd = 0, 1, Math.Floor(issueQty / packStd)),
                            lastqty As Integer = If(packStd = 0, 0, issueQty Mod packStd),
                            issueDate As String = ConfigDate.strToDateTime(.Item("TC003"), "yyyyMMdd").ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture)

                        Dim packStdSheet As Decimal = .Item("UDF55"),
                            issueQtySheet As Decimal = .Item("UDF53")

                        Dim fullBoxSheet As Integer = If(packStd = 0, 1, Math.Floor(issueQtySheet / packStdSheet)),
                            lastqtySheet As Integer = If(packStd = 0, 0, issueQtySheet Mod packStdSheet)
                        Dim allBox As Integer = fullBox + If(lastqty > 0, 1, 0)
                        For j As Integer = 1 To allBox
                            If j > fullBox Then 'last box
                                issueQty = lastqty
                                issueQtySheet = lastqtySheet
                            Else
                                issueQty = If(packStd > 0, packStd, issueQty)
                                issueQtySheet = If(packStd > 0, packStdSheet, issueQtySheet)
                            End If
                            printLabel(document, dt.Rows(i), issueQty, issueDate, issueQtySheet, j & "/" & allBox)
                        Next
                        'record when open for print 
                        Dim fld As Hashtable = New Hashtable,
                            whrhash As Hashtable = New Hashtable
                        'Dim moRcv() As String = Trim(dt.Rows(0).Item("N")).Split("-")
                        Dim mType As String = Trim(dt.Rows(0).Item("TE001")),
                            mNo As String = Trim(dt.Rows(0).Item("TE002")),
                            mSeq As String = Trim(dt.Rows(0).Item("TE003"))
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
                        whrhash.Add("TE001", mType)
                        whrhash.Add("TE002", mNo)
                        whrhash.Add("TE003", mSeq)
                        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL("MOCTE", fld, whrhash, "U"), Conn_SQL.ERP_ConnectionString)
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

                font = FontFactory.GetFont("Arial", 16, font.BOLD, BaseColor.BLACK)
                phrase = New Phrase()
                phrase.Add(New Chunk("No data Print", font))
                cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

                table.AddCell(cell)
                document.Add(table)
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

    Sub printLabel(ByRef document As Document, dr As DataRow, qty As Decimal, issueDate As String,
                   qtySheet As Decimal, strPage As String)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing
        Dim color__1 As BaseColor = Nothing
        Dim font As Font = Nothing
        document.NewPage()

        table = New PdfPTable(2)
        table.TotalWidth = 280.0F
        table.LockedWidth = True
        table.SetWidths(New Single() {0.7F, 1.0F})

        'Company Logo
        cell = controlPDF.ImageCell("~/Images/logoHTI.jpg", 20.0F, PdfPCell.ALIGN_CENTER)
        table.AddCell(cell)

        font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
        phrase = New Phrase()
        phrase.Add(New Chunk("MATERIALS ISSUE", font))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

        table.AddCell(cell)
        document.Add(table)
        'body
        Dim colData As ArrayList = New ArrayList

        colData.Add("MFG NO:")
        colData.Add(dr("TE011"))
        colData.Add("Supp:")
        colData.Add(dr("MB032"))
        colData.Add("Part Spec:")
        colData.Add(dr("TA035") & "◘3")
        colData.Add("Issue Date:")
        colData.Add(issueDate & "◘3")
        colData.Add("ITEM:")
        colData.Add(dr("TE004") & "◘3")
        colData.Add("Desc:")
        colData.Add(dr("TE017") & "◘3")
        colData.Add("Spec:")
        colData.Add(dr("TE018") & "◘3")
        colData.Add("Mat'l Lot:")
        colData.Add(dr("UDF01") & "◘3")
        colData.Add("Issue Q'ty:")
        Dim strSheet As String = ""
        'Dim sheet As Decimal = dr("UDF53")
        If qtySheet > 0 Then
            strSheet = "(" & CStr(qtySheet.ToString("#,##0")) & " PCS)"
        End If
        colData.Add(CStr(qty.ToString("#,##0.000")) & strSheet)
        colData.Add("Unit:")
        colData.Add(dr("TE006"))
        colData.Add("Issue No:")
        colData.Add(dr("TE00123") & "◘3")
        document.Add(controlPDF.rowPrintData(4, 274.0F, New Single() {2.3F, 5.0F, 1.4F, 1.3F}, colData, 10))

        colData = New ArrayList
        colData.Add("")
        colData.Add("(" & strPage & ")")
        ''colData.Add("../Images/RoHS.png◘1◘I◘20.0F")
        document.Add(controlPDF.rowPrintData(2, 274.0F, New Single() {3.5F, 6.5F}, colData, 10))


        'table = controlPDF.rowPrintData(4, 280.0F, New Single() {2.5F, 3.5F, 1.5F, 2.5F}, colData, 10, False, 4.0F)

    End Sub
End Class