Imports System.Globalization
Imports System.IO
Imports System.Net.Mail
Imports ClosedXML.Excel
Imports DocumentFormat.OpenXml.Office2010.Excel
Imports MIS_HTI.DataControl

Public Class AlertEmailManPower
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = ""
        Dim dbConn As New DataConnectControl
        Dim dtCont As New DataTableControl
        Dim dateToday As String = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-US"))
        'Get the GridView Data from database.
        Dim SQL As String = String.Empty
        SQL = "select * from (
                  select DEPT_CODE,count(*) ALL_EMPLOYEE,
                   sum(case when POS_GROUP='HEAD' and SHIFT_OF_WORK='D' then 1 else 0 end ) ALL_DAY_HEAD,
                   sum(case when POS_GROUP='STAFF' and SHIFT_OF_WORK='D' then 1 else 0 end ) ALL_DAY_STAFF,
                   sum(case when POS_GROUP='HEAD' and SHIFT_OF_WORK='D' then work_count else 0 end ) WORK_DAY_HEAD,
                   sum(case when POS_GROUP='STAFF' and SHIFT_OF_WORK='D' then work_count else 0 end ) WORK_DAY_STAFF,
                   round(sum(case when SHIFT_OF_WORK='D' then leave_hr else 0 end)/3600,0) LEAVE_DAY_HOUR,
                   sum(case when SHIFT_OF_WORK='D' then leave_count else 0 end ) LEAVE_DAY_FULL,
                   sum(case when POS_GROUP='HEAD' and SHIFT_OF_WORK='N' then 1 else 0 end ) ALL_NIGHT_HEAD,
                   sum(case when POS_GROUP='STAFF' and SHIFT_OF_WORK='N' then 1 else 0 end ) ALL_NIGHT_STAFF,
                   sum(case when POS_GROUP='HEAD' and SHIFT_OF_WORK='N' then work_count else 0 end ) WORK_NIGHT_HEAD,
                   sum(case when POS_GROUP='STAFF' and SHIFT_OF_WORK='N' then work_count else 0 end ) WORK_NIGHT_STAFF,
                   sum(case when SHIFT_OF_WORK='N' then leave_hr else 0 end ) LEAVE_NIGHT_HOUR,
                   sum(case when SHIFT_OF_WORK='N' then leave_count else 0 end ) LEAVE_NIGHT_FULL
                  from (
                  SELECT [CODE] ,[DEPT_CODE],
                  case when POS_CODE in ('FM1','SPV1','SSPV1','SOFF1','LD1','MGR1','LD1_HY') then 'HEAD' else 'STAFF' end POS_GROUP,
                  isnull(SHIFT_OF_WORK,'D') SHIFT_OF_WORK,
                  isnull(work_count,0)work_count,
                  isnull(leave_count,0) leave_count,
                  isnull(leave_hr,0) leave_hr
                  FROM V_HR_EMPLOYEE EM
                  left join ( select EMPNO,
                  case when SHIFT_DAY in  ('Sat(19)','Night(19)','Rest(Night)','Night(19-1)') then 'N' else 'D' end SHIFT_OF_WORK,
                  case when NORMAL_TIME >0 then 1 else 0 end work_count ,
                  case when LEAVE_TIME>0 and NORMAL_TIME=0 then 1 else 0 end leave_count,
                  case when LEAVE_TIME>0 and NORMAL_TIME>0 then LEAVE_TIME else 0 end leave_hr
                  from OVER_TIME_RECORD where WORK_DATE ='" & dateToday & "') OT on OT.EMPNO=EM.CODE	collate Chinese_PRC_BIN
                  where EMP_STATE not like '3___' 
                  ) AA
                    where DEPT_CODE not like '%HY%' 
                  group by DEPT_CODE
                  ) BB
                  left  join V_HR_DEPT DEPT on DEPT.Code=BB.DEPT_CODE
                  order by BB.DEPT_CODE"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        'Set DataTable Name which will be the name of Excel Sheet.
        dt.TableName = "MANPOWER_HOOTHAI"
        'Create a New Workbook.
        Using wb As New XLWorkbook()
            'Add the DataTable as Excel Worksheet.
            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
            wb.Style.Border.DiagonalBorderColor = XLColor.Black
            wb.Style.Border.OutsideBorder = XLBorderStyleValues.Thick

            Dim ws = wb.Worksheets.Add("MANPOWER" & dateToday)
            '// Merge a row
            ws.Cell("A1").Value = "HOO THAI INDUSTRIAL  CO.,LTD"
            ws.Cell("A1").Style.Font.Bold = True
            ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
            ws.Cell("A1").Style.Font.FontSize = 16
            ws.Range("A1:F1").Row(1).Merge()

            'ws.Row(1).Style.Font.Bold = False

            ws.Cell("A2").Value = "DATE : " & Date.Now.ToString("yyyy/MM/dd")
            ws.Cell("A2").Style.Font.FontSize = 14
            'ws.Cell("A2").Style.Fill.BackgroundColor = XLColor.BrightGreen
            ws.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
            ws.Range("A2:D2").Row(1).Merge()

            '// Merge a column
            ws.Cell("A4").Value = "DEPT CODE"
            ws.Cell("A4").Style.Alignment.WrapText = True
            ws.Cell("A4").Style.Fill.BackgroundColor = XLColor.Yellow
            ws.Cell("A4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Range("A4:A5").Column(1).Merge()

            ws.Cell("B4").Value = "DEPT NAME"
            ws.Cell("B4").Style.Alignment.WrapText = True
            ws.Cell("B4").Style.Fill.BackgroundColor = XLColor.Yellow
            ws.Cell("B4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Range("B4:B5").Column(1).Merge()

            ws.Cell("C4").Value = "TOTAL ALL"
            ws.Cell("C4").Style.Alignment.WrapText = True
            ws.Cell("C4").Style.Fill.BackgroundColor = XLColor.Yellow
            ws.Cell("C4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Range("C4:C5").Column(1).Merge()

            ws.Cell("P4").Value = "REMARK"
            ws.Cell("P4").Style.Alignment.WrapText = True
            ws.Cell("P4").Style.Fill.BackgroundColor = XLColor.Yellow
            ws.Cell("P4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Cell("P5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Range("P4:P5").Column(1).Merge()

            '// Merge a row (DAY)
            ws.Cell("D4").Value = "DAY SHIFT"
            ws.Cell("D4").Style.Fill.BackgroundColor = XLColor.LawnGreen
            ws.Cell("D4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Range("D4:I4").Row(1).Merge()
            '(DAY)
            ws.Cell("D5").Value = "TOTAL HEAD & LEADER"
            ws.Cell("D5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("E5").Value = "TOTAL STAFF"
            ws.Cell("E5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("F5").Value = "PRESENT HEAD & LEADER"
            ws.Cell("F5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("G5").Value = "PRESENT STAFF"
            ws.Cell("G5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("H5").Value = "TOTAL ABSENT HRS."
            ws.Cell("H5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("I5").Value = " ABSENT"
            ws.Cell("I5").Style.Fill.BackgroundColor = XLColor.Gray
            ws.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            '// Merge a row (NIGHT)
            ws.Cell("J4").Value = "NIGHT SHIFT"
            ws.Cell("J4").Style.Fill.BackgroundColor = XLColor.OrangeRed
            ws.Cell("J4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            ws.Range("J4:O4").Row(1).Merge()

            '(DAY)
            ws.Cell("J5").Value = "TOTAL HEAD & LEADER"
            ws.Cell("J5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("J4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("K5").Value = "TOTAL  STAFF"
            ws.Cell("K5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("L5").Value = "PRESENT HEAD & LEADER"
            ws.Cell("L5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("M5").Value = "PRESENT STAFF"
            ws.Cell("M5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("M5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("N5").Value = "TOTAL ABSENT HRS."
            ws.Cell("N5").Style.Fill.BackgroundColor = XLColor.Cyan
            ws.Cell("N5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin

            ws.Cell("O5").Value = " ABSENT"
            ws.Cell("O5").Style.Fill.BackgroundColor = XLColor.Gray
            ws.Cell("O5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin


            Dim Cnt As Integer = 17
            Dim line As Integer = 6
            For j = 1 To Cnt - 1
                For k As Integer = 1 To 2
                    'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.PaleGreen
                    'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    Cell(ws.Cell(line, j), XLColor.PaleGreen)
                    line += 1
                Next

                For k As Integer = 1 To 6
                    'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.Plum
                    'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    Cell(ws.Cell(line, j), XLColor.Plum)
                    line += 1
                Next
                For k As Integer = 1 To 5
                    'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.PeachPuff
                    'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    Cell(ws.Cell(line, j), XLColor.PeachPuff)
                    line += 1
                Next

                'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.PaleTurquoise
                'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                'Cell(ws.Cell(line, j), XLColor.PaleTurquoise)
                'line += 1

                'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.Lavender
                'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                Cell(ws.Cell(line, j), XLColor.Lavender)
                line += 1

                'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.LemonChiffon
                'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                Cell(ws.Cell(line, j), XLColor.LemonChiffon)
                line += 1

                'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.LightPink
                'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                Cell(ws.Cell(line, j), XLColor.LightPink)
                line += 1

                'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.SkyBlue
                'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                Cell(ws.Cell(line, j), XLColor.SkyBlue)
                line += 1
                For k As Integer = 1 To 14
                    'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.PaleTurquoise
                    'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    Cell(ws.Cell(line, j), XLColor.PaleTurquoise)
                    line += 1
                Next
                For k As Integer = 1 To 3
                    'ws.Cell(line, j).Style.Fill.BackgroundColor = XLColor.LightGray
                    'ws.Cell(line, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    Cell(ws.Cell(line, j), XLColor.LightGray)
                    line += 1
                Next
                line = 6
            Next

            Dim c8 As String = VarIni.char8
            Dim fldList As New ArrayList From {
                "Code",
                "Name",
                "ALL_EMPLOYEE" & c8,
                "ALL_DAY_HEAD" & c8,
                "ALL_DAY_STAFF" & c8,
                "WORK_DAY_HEAD" & c8,
                "WORK_DAY_STAFF" & c8,
                "LEAVE_DAY_HOUR" & c8,
                "LEAVE_DAY_FULL" & c8,
                "ALL_NIGHT_HEAD" & c8,
                "ALL_NIGHT_STAFF" & c8,
                "WORK_NIGHT_HEAD" & c8,
                "WORK_NIGHT_STAFF" & c8,
                "LEAVE_NIGHT_HOUR" & c8,
                "LEAVE_NIGHT_FULL" & c8
            }

            Dim hashcont As New HashtableControl
            Dim sumHash As New Hashtable From {
                {"ALL_EMPLOYEE", 0},
                {"ALL_DAY_STAFF", 0},
                {"WORK_DAY_HEAD", 0},
                {"WORK_DAY_STAFF", 0},
                {"LEAVE_DAY_HOUR", 0},
                {"LEAVE_DAY_FULL", 0},
                {"ALL_NIGHT_HEAD", 0},
                {"ALL_NIGHT_STAFF", 0},
                {"WORK_NIGHT_HEAD", 0},
                {"WORK_NIGHT_STAFF", 0},
                {"LEAVE_NIGHT_HOUR", 0},
                {"LEAVE_NIGHT_FULL", 0}
            }
            For Each dr As DataRow In dt.Rows
                With New DataRowControl(dr)
                    Dim k As Integer = 1
                    For Each str As String In fldList
                        Dim dd() As String = str.Split(c8)
                        Dim val As String
                        If dd.Length = 1 Then
                            val = .Text(str)
                        Else
                            Dim fldName As String = dd(0)
                            Dim valDecimal As Integer = .Number(fldName)
                            val = checkVal(valDecimal.ToString)
                            Dim dCode As String = .Text("DEPT_CODE")
                            If dCode <> "AM" And dCode <> "AM." Then
                                hashcont.CountItemDataHash(sumHash, fldName, valDecimal)
                            End If
                        End If

                            ws.Cell(line, k).Value = val
                        k += 1
                    Next
                    'get all leave this date
                    SQL = " SELECT c.Name,count(*) as cnt FROM OVER_TIME_RECORD t2
                            LEFT JOIN CodeInfo c ON c.Code = t2.LEAVE_CODE
                            WHERE t2.WORK_DATE ='" & dateToday & "' and LEAVE_TIME>0 and NORMAL_TIME=0 AND t2.DEPT = '" & .Text("Code") & "'
                            GROUP BY c.Name "
                    Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                    Dim textShow As String = ""
                    For Each dr1 As DataRow In dt2.Rows
                        With New DataRowControl(dr1)
                            textShow &= .Text("Name") & "=" & .Number("cnt").ToString("N0") & " ,"
                        End With
                    Next
                    ws.Cell(line, k).Value = If(textShow.Length = 0, "", textShow.Substring(0, textShow.Length - 1))
                End With
                line += 1
            Next

            With ws
                Dim col As Integer = 1
                For Each fld As String In fldList
                    Dim str As String = ""
                    Select Case col
                        Case 1
                            str = ""
                        Case 2
                            str = "SUM"
                        Case Else
                            str = hashcont.getDataHashDecimal(sumHash, fld.Replace(c8, "")).ToString
                    End Select
                    Cell(.Cell(line, col), XLColor.Orange, If(col > 2, checkVal(str), str))
                    col += 1
                Next
                Cell(.Cell(line, col), XLColor.Orange)
            End With
            'wb.Worksheets.Add(dt)
            'ws.Cells("B1").Calculate()
            Using memoryStream As New MemoryStream()
                'Save the Excel Workbook to MemoryStream.
                wb.SaveAs(memoryStream)

                'Convert MemoryStream to Byte array.
                Dim bytes As Byte() = memoryStream.ToArray()
                memoryStream.Close()

                'Send Email with Excel attachment.
                Using myMail As New MailMessage()

                    myMail.From = New MailAddress("alerts@jinpao.co.th", "Alert System")
                    myMail.To.Add(New MailAddress("htpers@jinpao.co.th", "PN"))
                    myMail.To.Add(New MailAddress("htfina@jinpao.co.th", "AC"))
                    myMail.To.Add(New MailAddress("htdc@jinpao.co.th", "DC"))
                    myMail.To.Add(New MailAddress("htsafety@jinpao.co.th", "ESH"))
                    myMail.To.Add(New MailAddress("chayapat@jinpao.co.th", "MK"))
                    myMail.To.Add(New MailAddress("htpdprod@jinpao.co.th", "STAMPING"))
                    myMail.To.Add(New MailAddress("htnct@jinpao.co.th", "NCT"))
                    myMail.To.Add(New MailAddress("htpe@jinpao.co.th", "PE"))
                    myMail.To.Add(New MailAddress("htpur@jinpao.co.th", "PU"))
                    myMail.To.Add(New MailAddress("htqc@jinpao.co.th", "QC"))
                    myMail.To.Add(New MailAddress("httech@jinpao.co.th", "TM"))
                    myMail.To.Add(New MailAddress("htwh@jinpao.co.th", "WH"))
                    myMail.To.Add(New MailAddress("charles@jinpao.co.th", "Mr.Sun"))

                    myMail.CC.Add(New MailAddress("victorchung@jinpao.co.th", "Mr.Chung"))
                    myMail.CC.Add(New MailAddress("huiling@jinpao.co.th", "Mrs.Kuo Hui-Ling"))
                    myMail.CC.Add(New MailAddress("developer@jinpao.co.th", "IT"))

                    myMail.Subject = "Alert for MANPOWER HOOTHAI " & DateTime.Now.ToString("yyyy/MM/dd")
                    Dim msg As String = String.Empty
                    msg = "Dear Sir, " & "<br/>"
                    msg &= "HOOTHAI INDUSTRIAL CO.,LTD " & "<br/>"
                    msg &= "MANPOWER: " & DateTime.Now.ToString("yyyMMdd") & "<br/>"
                    msg &= "Please See at Attachment" & "<br/>"
                    msg &= "Best Regards,"
                    myMail.Body = msg

                    'Add Byte array as Attachment.
                    myMail.Attachments.Add(New Attachment(New MemoryStream(bytes), "MANPOWER_HOOTHAI_" & DateTime.Now.ToString("yyyMMdd hhmmss") & ".xlsx"))
                    myMail.IsBodyHtml = True
                    Dim smtp As New SmtpClient("mail.jinpao.co.th")
                    smtp.Send(myMail)
                End Using
            End Using
        End Using
        Label1.Text = "OK"
    End Sub

    Function checkVal(val As String, Optional isValEmpty As String = "0") As String
        Return If(val = isValEmpty, "", val)
    End Function


    Sub Cell(ByRef referCell As IXLCell, bgColor As XLColor, Optional val As String = "")
        With referCell
            .Style.Fill.BackgroundColor = bgColor
            .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
            .Value = val
        End With
    End Sub


End Class