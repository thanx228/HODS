Imports System.Globalization
Namespace DataControl

    Public Class DateControl
        'sql server format
        Public FormatData As String = "yyyyMMdd"
        Public FormatShow As String = "dd/MM/yyyy"
        Public FormatShow2 As String = "dd-mm-yyyy"
        Public formatYear As String = "yyyy"
        Public formatMonth As String = "MM"
        Public formatDate As String = "dd"
        'oracle format
        'FormatDataOracle
        Public FormatDataOracle As String = "yyyymmdd"
        Public FormatShowOracle As String = "dd/mm/yyyy"
        Public FormatShowOracle2 As String = "yyyy/mm/dd"
        Public formatYearOracle As String = formatYear
        Public formatMonthOracle As String = "mm"
        Public formatDateOracle As String = formatDate
        Public formatTimeStamp As String = "yyyy/mm/dd hh24:mi:ss" 'add by noi on 2018/03/13

        Public Const yyyyMMdd As String = "yyyyMMdd" 'add by noi
        Public Const yyyyMMdd2 As String = "yyyy/MM/dd" 'add by noi on 2018/03/13
        Public Const ddMMyyyy As String = "ddMMyyyy" 'add by noi on 2018/03/17
        Public Const ddMMyyyy2 As String = "dd/MM/yyyy" 'add by noi on 2018/03/17

        Function dateFormat(ByVal dateForChange As String, Optional ByVal separate As String = "/") As String 'format MM/dd/yyyy
            If dateForChange = "" Then
                Return ""
            End If
            Dim xd As String = ""
            Dim xm As String = ""
            Dim temp1() As String = dateForChange.Split(separate)
            xd = temp1(1)
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = temp1(0)
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            Return temp1(2) & xm & xd
        End Function

        Function dateFormat_yyyyMMdd(ByVal dateForChange As String, Optional ByVal separate As String = "/", Optional today As Boolean = False) As String 'format yyyy/MM/dd
            If dateForChange = "" And Not today Then
                Return ""
            End If
            Dim txt As String = ""
            If dateForChange = "" Then
                txt = DateTime.Now.ToString("yyyyMMdd")
            Else
                Dim tempDate() As String = dateForChange.Split(separate)
                txt = tempDate(0) & tempDate(1) & tempDate(2)
            End If
            Return txt 'yyyyMMdd
        End Function

        Function dateFormat2(ByVal dateForChange As String, Optional ByVal separate As String = "/", Optional today As Boolean = False) As String 'format dd/MM/yyyy
            If dateForChange = "" Then
                Return ""
            End If
            Dim tempDate() As String = dateForChange.Split(separate)
            Return tempDate(2) & tempDate(1) & tempDate(0) 'yyyyMMdd
        End Function

        Function dateFormat3(ByVal dateForChange As String, Optional ByVal separate As String = "/") As String 'format dd/MM/yyyy
            If dateForChange = "" Then
                Return ""
            End If
            Dim tempDate() As String = dateForChange.Split(separate)
            Return tempDate(1) & tempDate(0)
        End Function

        Function dateFormat4(ByVal dateForChange As String, Optional ByVal separate As String = "/") As String 'format dd/MM/yyyy
            If dateForChange = "" Then
                Return ""
            End If

            Dim tempDate() As String = dateForChange.Split(separate)
            Return tempDate(2) & "-" & tempDate(1) & "-" & tempDate(0) 'yyyy-MM-dd
        End Function

        Function dateFormatYM(ByVal dateForChange As String, Optional ByVal separate As String = "/") As String 'format dd/MM/yyyy
            If dateForChange = "" Then
                Return ""
            End If

            Dim tempDate() As String = dateForChange.Split(separate)
            Return tempDate(1) & tempDate(0)
        End Function

        Function dateShow2(ByVal dateForChange As String, Optional ByVal connStr As String = "") As String 'dd-mm-yyyy
            If dateForChange = "" Then
                Return ""
            End If
            Return dateForChange.ToString.Substring(6) & connStr & dateForChange.ToString.Substring(4, 2) & connStr & dateForChange.ToString.Substring(0, 4)
        End Function

        Function dateShow(ByVal dateForChange As String, Optional ByVal connStr As String = "") As String
            If dateForChange = "" Then
                Return ""
            End If
            Return dateForChange.ToString.Substring(7) & connStr & dateForChange.ToString.Substring(5, 2) & connStr & dateForChange.ToString.Substring(1, 4)
        End Function

        'for calculate time
        Function strToDateTime(ByVal strDate As String, Optional ByVal dateFormat As String = "yyyyMMdd HH:mm") As Date ':ss
            Try
                Return If(strDate = String.Empty, String.Empty, DateTime.ParseExact(strDate, dateFormat, New CultureInfo("en-US")))
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Function getWorkTime(ByVal dateFrom As DateTime, ByVal dateFinish As DateTime) As Decimal
            'Dim breakTime As Decimal = getBreakTime(dateFrom, dateFinish)
            Dim workTime As Decimal = DateDiff(DateInterval.Second, dateFrom, dateFinish)
            Return workTime
        End Function

        Function getSetupTime(ByVal dateFrom As DateTime, ByVal dateFinish As DateTime) As Decimal
            Return DateDiff(DateInterval.Second, dateFrom, dateFinish)
        End Function

        Function getTime(ByVal dateFrom As DateTime, ByVal dateFinish As DateTime) As Decimal
            Return DateDiff(DateInterval.Second, dateFrom, dateFinish)
        End Function

        Function setDate(ByVal val As String) As String
            Dim dateVal() As String = val.Split("/")
            Dim xval As String = ""
            If dateVal.Length = 3 Then
                xval = dateVal(2) & dateVal(0) & dateVal(1)
            End If
            Return xval
        End Function

        Function ConvertHHMM(timeVal As Integer, Optional minuteUnit As Boolean = False) As String
            Dim showHHMM As String = String.Empty
            If timeVal > 0 Then
                Dim hours As Integer
                Dim minutes As Integer
                If minuteUnit Then
                    hours = timeVal \ 60
                    minutes = timeVal Mod 60
                Else
                    hours = timeVal \ 3600
                    minutes = Math.Floor((timeVal Mod 3600) \ 60)
                End If
                showHHMM = CType(hours, String) & ":" & CType(minutes.ToString("00"), String)
            End If
            Return showHHMM
        End Function


        '--Add By Top 26-09-2017
        Function dateFormat_yyyyMM(ByVal dateForChange As String, Optional ByVal separate As String = "/", Optional today As Boolean = False) As String 'format yyyyMMdd
            If dateForChange = String.Empty And Not today Then
                Return String.Empty
            End If
            Dim txt As String = String.Empty
            If dateForChange = String.Empty Then
                txt = DateTime.Now.ToString(yyyyMMdd)
            Else
                Dim tempDate() As String = dateForChange.Split(separate)
                txt = tempDate(0) & tempDate(1)
            End If
            Return txt 'yyyyMM
        End Function

        'add by noi
        Function GetNextDays(dateVal As String, Optional isNextDays As Boolean = True, Optional formatDate As String = "yyyyMMdd") As String
            Dim dd As Date = strToDateTime(dateVal, formatDate)
            Return dd.AddDays(If(isNextDays, 1, -1)).ToString(formatDate)
        End Function

        'add by noi
        Function ChangeDateFormat(dateForChange As String, formatOld As String, formatNew As String) As String
            If dateForChange = "" Or formatOld = "" Or formatNew = "" Then
                Return ""
            End If
            'Dim dateVal As Date = strToDateTime(dateForChange, formatOld)
            Return strToDateTime(dateForChange, formatOld).ToString(formatNew)
        End Function

        Function getBreakTime(ByVal dateFrom As DateTime, ByVal dateFinish As DateTime, Optional shift As String = "D") As Decimal
            Dim tFrom As String = dateFrom.ToString("HH:mm")
            Dim tEnd As String = dateFinish.ToString("HH:mm")
            Dim bTime As Decimal = 0
            Dim strCheckTime As DateTime
            Dim endCheckTime As DateTime
            Dim dateStr As String = dateFrom.Date.ToString("yyyyMMdd")
            Dim dateEnd As String = dateFinish.Date.ToString("yyyyMMdd")

            '------------------------------------------------------------------------------------------------------------
            'Get Break Time
            If dateFrom.Date = dateFinish.Date Then 'date same
                If shift = "N" Then
                    'night ship
                    If tFrom <= "01:00" And tEnd >= "00:00" Then

                        'check start
                        If tFrom <= "00:00" Then
                            strCheckTime = strToDateTime(dateStr & " 00:00")
                        Else
                            strCheckTime = dateFrom 'strToDateTime(dateStr & " " & tFrom)
                        End If
                        'check end 
                        If tEnd >= "01:00" Then
                            endCheckTime = strToDateTime(dateEnd & " 01:00")
                        Else
                            endCheckTime = dateFinish 'strToDateTime(dateEnd & " " & tEnd)
                        End If
                        Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                        If timeForDay > 0 Then
                            bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                        End If
                    End If
                Else
                    'day shift
                    If tFrom <= "13:00" And tEnd >= "12:00" Then
                        'check start
                        If tFrom <= "12:00" Then
                            strCheckTime = strToDateTime(dateStr & " 12:00")
                        Else
                            strCheckTime = dateFrom 'strToDateTime(dateStr & " " & tFrom)
                        End If
                        'check end 
                        If tEnd >= "13:00" Then
                            endCheckTime = strToDateTime(dateEnd & " 13:00")
                        Else
                            endCheckTime = dateFinish 'strToDateTime(dateEnd & " " & tEnd)
                        End If
                        Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                        If timeForDay > 0 Then
                            bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                        End If
                    End If
                End If

                If dateFinish.DayOfWeek = 0 Or dateFinish.DayOfWeek = 6 Then 'sat and sun
                    If shift = "N" Then
                        'night shift
                        If tFrom <= "04:20" And tEnd >= "04:00" Then
                            'check start
                            If tFrom <= "04:00" Then
                                strCheckTime = strToDateTime(dateStr & " 04:00")
                            Else
                                strCheckTime = dateFrom ' strToDateTime(dateStr & " " & tFrom)
                            End If
                            'check end 
                            If tEnd >= "04:20" Then
                                endCheckTime = strToDateTime(dateEnd & " 04:20")
                            Else
                                endCheckTime = dateFinish ' strToDateTime(dateEnd & " " & tEnd)
                            End If
                            Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            If timeForDay > 0 Then
                                bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            End If
                        End If
                    Else
                        'day shift
                        If tFrom <= "16:20" And tEnd >= "16:00" Then
                            'check start
                            If tFrom <= "16:00" Then
                                strCheckTime = strToDateTime(dateStr & " 16:00")
                            Else
                                strCheckTime = dateFrom ' strToDateTime(dateStr & " " & tFrom)
                            End If
                            'check end 
                            If tEnd >= "16:20" Then
                                endCheckTime = strToDateTime(dateEnd & " 16:20")
                            Else
                                endCheckTime = dateFinish ' strToDateTime(dateEnd & " " & tEnd)
                            End If
                            Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            If timeForDay > 0 Then
                                bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            End If
                        End If
                    End If
                Else 'normal date
                    If shift = "N" Then
                        'night shift
                        If tFrom <= "04:50" And tEnd >= "04:30" Then
                            'check start
                            If tFrom <= "04:30" Then
                                strCheckTime = strToDateTime(dateStr & " 04:30")
                            Else
                                strCheckTime = dateFrom ' strToDateTime(dateStr & " " & tFrom)
                            End If
                            'check end 
                            If tEnd >= "04:50" Then
                                endCheckTime = strToDateTime(dateEnd & " 04:50")
                            Else
                                endCheckTime = dateFinish ' strToDateTime(dateEnd & " " & tEnd)
                            End If
                            Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            If timeForDay > 0 Then
                                bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            End If
                        End If
                    Else
                        'day shift
                        If tFrom <= "16:50" And tEnd >= "16:30" Then
                            'check start
                            If tFrom <= "16:30" Then
                                strCheckTime = strToDateTime(dateStr & " 16:30")
                            Else
                                strCheckTime = dateFrom 'strToDateTime(dateStr & " " & tFrom)
                            End If
                            'check end 
                            If tEnd >= "16:50" Then
                                endCheckTime = strToDateTime(dateEnd & " 16:50")
                            Else
                                endCheckTime = dateFinish ' strToDateTime(dateEnd & " " & tEnd)
                            End If
                            Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            If timeForDay > 0 Then
                                bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            End If
                        End If
                    End If
                End If
            Else 'not same date from < date to alway
                If dateFrom < dateFinish Then
                    If shift = "N" Then
                        If tEnd >= "01:00" Then
                            strCheckTime = strToDateTime(dateEnd & " 00:00")
                            'check end
                            If tEnd >= "01:00" Then
                                endCheckTime = strToDateTime(dateEnd & " 01:00")
                            Else
                                endCheckTime = dateFinish ' strToDateTime(dateEnd & " " & tEnd)
                            End If
                            Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            If timeForDay > 0 Then
                                bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                            End If
                            'bTime = bTime + 3600
                        End If
                        If dateFinish.DayOfWeek = 0 Or dateFinish.DayOfWeek = 1 Then 'sat and sun
                            If tEnd >= "04:20" Then
                                strCheckTime = strToDateTime(dateEnd & " 04:00")
                                'check end
                                If tEnd >= "04:20" Then
                                    endCheckTime = strToDateTime(dateEnd & " 04:20")
                                Else
                                    endCheckTime = dateFinish ' strToDateTime(dateEnd & " " & tEnd)
                                End If
                                Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                                If timeForDay > 0 Then
                                    bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                                End If
                            End If
                        Else
                            'normal day
                            If tEnd >= "04:50" Then
                                strCheckTime = strToDateTime(dateEnd & " 04:30")
                                'check end
                                If tEnd >= "04:50" Then
                                    endCheckTime = strToDateTime(dateEnd & " 04:50")
                                Else
                                    endCheckTime = dateFinish ' strToDateTime(dateEnd & " " & tEnd)
                                End If
                                Dim timeForDay As Decimal = DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                                If timeForDay > 0 Then
                                    bTime = bTime + DateDiff(DateInterval.Second, strCheckTime, endCheckTime)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Return bTime
        End Function

        Function TimeFormat(valTime As Integer, Optional formatHour As String = "00", Optional FormatTime As String = "H:M:S") As String 'valTime =unit time is second
            Dim negative As Boolean = False
            If valTime < 0 Then
                negative = True
            End If

            valTime = Math.Abs(valTime)
            Dim valReturn As String = FormatTime
            Dim valHour As Integer = 0
            Dim valMin As Integer = 0
            Dim valSec As Integer = 0
            'If valTime > 0 Then
            valHour = Math.Floor(valTime / 3600)
            valMin = Math.Floor((valTime Mod 3600) / 60)
            valSec = valTime Mod 60
            'End If
            Return If(negative, "-", "") & valReturn.Replace("H", valHour.ToString(formatHour)).Replace("M", valMin.ToString("00")).Replace("S", valSec.ToString("00"))
        End Function

        Function CalSecond(formatTime As String) As Integer 'HH:MM:SS
            Dim timeList As New List(Of Integer) From {3600, 60, 1}
            Dim temp() As String = formatTime.Split(":")
            Dim i As Integer = 0
            Dim sumSecond As Integer = 0
            Dim outcont As New OutputControl
            For Each str As String In temp
                sumSecond += outcont.checkNumberic(str) * timeList(i)
                i += 1
            Next
            Return sumSecond
        End Function


    End Class
End Namespace