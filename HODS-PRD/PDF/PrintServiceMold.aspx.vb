Imports System.IO
Imports System.IO.MemoryStream
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MIS_HTI.DataControl
Imports MIS_HTI.PDF

Public Class PrintServiceMold
    Inherits System.Web.UI.Page
    Dim pdfCont As New PDFControl
    Dim dbConn As New DataConnectControl
    Dim dateCont As New DateControl
    Dim c8 As String = VarIni.char8
    Dim outCont As New OutputControl
    Dim dr As DataRow
    Dim totalWidth As Single = 600.0F
    Dim headList As New ArrayList
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        print()
    End Sub

    Sub print()
        Dim document As Document = New Document(PageSize.HALFLETTER.Rotate, 5.0F, 5.0F, 1.0F, 1.0F)
        Dim hashCont As New HashtableControl

        Dim docno As String = Request.QueryString.Item("docno").ToString
        'Dim docno As String = "20210216001"
        Using memoryStream As New MemoryStream()
            Try
                Dim writer As PdfWriter = PdfWriter.GetInstance(document, memoryStream)
                Dim text As String = "ใบสั่งงาน/ใบสั่งตั้งแม่พิมพ์"
                Dim text2 As String = "WORK ORDER /SERVICE MOLD REPORT"
                writer.PageEvent = New PDFEvent(text & VarIni.char8 & text2, "FM-PC-02-A", "")
                document.Open()
                Dim cb As PdfContentByte = writer.DirectContent
                document.AddTitle(text)
                Dim table As PdfPTable
                'Dim colWidth() As Single

                Dim xHead As Single = document.PageSize.GetLeft(5)
                Dim yHead As Single = document.PageSize.GetTop(60)

                Dim xBody As Single = xHead
                Dim yBody As Single = document.PageSize.GetTop(90)


                Dim xFoot As Single = xHead
                Dim yFoot As Single = document.PageSize.GetBottom(80)


                Dim headCol As New ArrayList From {
                    SetText("ลำดับ Item.",, 2, "C"),
                    SetText("เลขที่สั่งผลิต Work Order",, 2, "C"),
                    SetText("รหัสชิ้นงาน Part Number",, 2, "C"),
                    SetText("เครื่องจักร Machine",, 2, "C"),
                    SetText("แม่พิมพ์ Mold",, 2, "C"),
                    SetText("ขั้นตอน Step",, 2, "C"),
                    SetText("วัตถุดิบ Type",, 2, "C"),
                    SetText("จำนวนวัตถุดิบ Mat Q'ty",, 2, "C"),
                    SetText("จำนวนสั่งผลิต Order Q'ty",, 2, "C"),
                    SetText("ชั่วโมง Hour",, 2, "C"),
                    SetText("เลขที่ QI. Doc .QI",, 2, "C"),
                    SetText("หมายเหตุ Remark",, 2, "C")
                }
                Dim colHeadWidth() As Single = New Single() {
                    0.15F,'seq
                    0.6F,'work order
                    0.9F,'part number
                    0.25F,'machine
                    0.25F,'mold
                    0.35F,'step
                    0.76F,'material type
                    0.33F,'mat qty
                    0.33F,'order qty
                    0.29F,'hour
                    0.25F,'QI
                    0.5F'remark
                }

                'get data
                Dim al As New ArrayList
                Dim fldList As ArrayList

                'select sub sql
                With New ArrayListControl(al)
                    .TAL("TA001+'-'+TA002" & VarIni.C8 & "TA001", "")
                    .TAL("TB003", "")
                    .TAL("TB004-TB005" & VarIni.C8 & "ISSUE_BAL", "")
                    .TAL("ROW_NUMBER() over(PARTITION BY TA001,TA002 ORDER BY TB003)" & VarIni.C8 & "row_num", "")
                    fldList = .ChangeFormat
                End With
                Dim strsqlMO As New SQLString("MOCTB", fldList)
                With strsqlMO
                    .setLeftjoin("MOCTA", New List(Of String) From {
                                 "TA001" & VarIni.C8 & "TB001",
                                 "TA002" & VarIni.C8 & "TB002"
                                 })
                    .SetWhere(dbConn.WHERE_EQUAL("TA013", "Y"), True)
                End With

                'select sub sql
                al = New ArrayList
                With New ArrayListControl(al)
                    .TAL("Seq", "")
                    .TAL("MoNo+'-'+ MoSeq" & VarIni.C8 & "MO", "")
                    .TAL("ItemSpec", "")
                    .TAL("MacNo", "")
                    .TAL("MoldName", "")
                    .TAL("Process", "")
                    .TAL("case when MoldName like '_1' then MB003 else '-' end" & VarIni.C8 & "MAT_SHOW", "")
                    .TAL("case when MoldName like '_1' then case when ISSUE_BAL<0 then 0 else ISSUE_BAL end else 0 end" & VarIni.C8 & "ISSUE_BAL", "")
                    .TAL("Qty", "")
                    .TAL("UseHours", "")
                    .TAL("BOMME.UDF01" & VarIni.C8 & "QI_NO", "")
                    .TAL("''" & VarIni.C8 & "Remark", "")
                    fldList = .ChangeFormat
                End With
                Dim strsql As New SQLString(VarIni.DBMIS & "..SetMoldDetail A", fldList)
                With strsql
                    .setLeftjoin(VarIni.DBMIS & "..SetMoldOrder B", New List(Of String) From {"B.DocNo" & VarIni.C8 & "A.DocNo"})
                    .setLeftjoin("BOMME", New List(Of String) From {"ME001" & VarIni.C8 & "ItemNo"})
                    .setLeftjoin(strsqlMO.GetSubQuery("ISSUE"), New List(Of String) From {
                                 "ISSUE.TA001" & VarIni.C8 & "A.MoNo",
                                 "ISSUE.row_num" & VarIni.C8 & "1"
                                })
                    .setLeftjoin("INVMB", New List(Of String) From {"MB001" & VarIni.C8 & "TB003"})

                    .SetWhere(dbConn.WHERE_EQUAL("A.DocNo", docno), True)
                    .SetOrderBy("Seq")
                End With
                Dim dt As DataTable = dbConn.Query(strsql.GetSQLString, VarIni.ERP, dbConn.WhoCalledMe)
                Dim fontsize As Integer = 8
                'get data
                'draw table
                Dim line As Integer = 1
                Dim page As Integer = 0
                Dim maxLine As Integer = 12
                For Each dr As DataRow In dt.Rows
                    With New DataRowControl(dr)
                        If line Mod maxLine = 1 Then
                            If page > 1 Then
                                pdfCont.ShowTable(writer, table, xBody, yBody)
                                footer(writer, xFoot, yFoot, cb, document)
                            End If
                            document.NewPage()
                            'show head
                            Dim tableHead As PdfPTable = pdfCont.TableFormat(New Single() {0.33F, 0.33F, 0.34F}, totalWidth)
                            If headList.Count = 0 Then
                                Dim SQL As String = "select DocDate,DocTime from SetMoldOrder where DocNo='" & docno & "'"
                                With New DataRowControl(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                                    Dim headListNew As New ArrayList From {
                                        SetText(""),
                                        SetText(""),
                                        SetText("เลขที่/Doc No.: " & docno),
                                        SetText("ถึง/To : ช่างเทคนิคและซ่อมบำรุง"),
                                        SetText("วันที่/Date : " & .Text("DocDate")),
                                        SetText("เวลา/Time : " & .Text("DocTime"))
                                    }
                                    headList.AddRange(headListNew)
                                End With
                            End If
                            pdfCont.Rows(tableHead, headList, 9, cellBorder:=0)
                            pdfCont.ShowTable(writer, tableHead, xHead, yHead)

                            ''show number
                            pdfCont.Text(cb, docno.Substring(8, 3) + 0, document.PageSize.GetRight(140), document.PageSize.GetTop(45), 30)
                            ''show number

                            'show head


                            table = pdfCont.TableFormat(colHeadWidth, totalWidth)
                            pdfCont.Rows(table, headCol, 9, fixHieght:=12.0F)
                            page += 1
                        End If
                        Dim issueBal As Decimal = .Number("ISSUE_BAL")
                        Dim matShow As String = .Text("MAT_SHOW")
                        'Dim matShowAlign As String = If(matShow = "-", "C", "L")
                        Dim fldData As New ArrayList From {
                                    SetText(.Text("Seq"), align:="C"),
                                    SetText(.Text("MO")),
                                    SetText(.Text("ItemSpec")),
                                    SetText(.Text("MacNo"), align:="C"),
                                    SetText(.Text("MoldName"), align:="C"),
                                    SetText(.Text("Process")),
                                    SetText(matShow, align:=If(matShow = "-", "C", "L")),
                                    SetText(If(issueBal > 0, issueBal.ToString("N2"), "-"), align:=If(issueBal > 0, "R", "C")),
                                    SetText(.Number("Qty").ToString("N0"), align:="R"),
                                    SetText(.Text("UseHours").Replace("0H ", "").Replace(" 0M", "").Replace("H", " ชม.").Replace("M", " น."), align:="C"),
                                    SetText(.Text("QI_NO"), align:="C"),
                                    SetText(.Text("Remark"))
                                }
                        pdfCont.Rows(table, fldData, fontsize)
                        line += 1
                    End With
                Next
                'add line bank

                Dim lineBlank As Integer = (line Mod maxLine)
                Dim fldDataBlank As New ArrayList
                For i As Integer = lineBlank To maxLine
                    For Each txt As String In headCol
                        fldDataBlank.Add("")
                    Next
                Next
                pdfCont.Rows(table, fldDataBlank, fontsize)
                'add line bank
                pdfCont.ShowTable(writer, table, xBody, yBody)
                footer(writer, xFoot, yFoot, cb, document)
            Catch ex As Exception

            Finally
                document.Close()
                'draw table
                'show file to browser
                Dim bytes As Byte() = memoryStream.ToArray()
                memoryStream.Close()
                Response.ClearContent()
                Response.ClearHeaders()
                Response.AddHeader("content-disposition", "inline;filename=" & docno & ".pdf") 'attachment
                Response.ContentType = "application/pdf"
                Response.BinaryWrite(bytes)
                Response.Flush()
                Response.Clear()
            End Try
        End Using
    End Sub

    Sub footer(writer As PdfWriter, xFoot As Single, yFoot As Single, cb As PdfContentByte, document As Document)
        Dim tableFoot As PdfPTable = pdfCont.TableFormat(New Single() {0.33F, 0.33F, 0.34F}, totalWidth)
        Dim fldFoot As New ArrayList
        For i As Integer = 1 To 3
            fldFoot.Add(SetText(".................................................", align:="C"))
        Next

        Dim fldFootList As New ArrayList From {
            "แผนกช่างเทคนิคและซ่อมบำรุง",
            "แผนกผลิต",
            "ผู้เขียน",
            "Technic & Maintenance",
            "Production",
            "Writen By"
        }
        For Each txt As String In fldFootList
            fldFoot.Add(SetText(txt, align:="C"))
        Next
        pdfCont.Rows(tableFoot, fldFoot, 10, cellBorder:=0)
        pdfCont.ShowTable(writer, tableFoot, xFoot, yFoot)

        'insert arrow
        Dim path As String = "../Images/arrow.jpg"
        pdfCont.images(cb, path, 60.0F, document.PageSize.GetLeft(180), document.PageSize.GetBottom(40))
        pdfCont.images(cb, path, 60.0F, document.PageSize.GetRight(220), document.PageSize.GetBottom(40))
    End Sub

    Function SetText(text As String, Optional cSpan As Integer = 1, Optional rSpan As Integer = 1, Optional align As String = "L", Optional strSplit As String = VarIni.C8) As String
        Return String.Join(strSplit, New List(Of String) From {text, cSpan, rSpan, align})
    End Function
End Class