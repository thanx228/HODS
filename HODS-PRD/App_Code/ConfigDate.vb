Imports System.Globalization

Public Class ConfigDate

    Public Const FormatData As String = "yyyyMMdd"
    Public Const FormatShow As String = "dd/MM/yyyy"
    Public Const formatYear As String = "yyyy"
    Public Const formatMonth As String = "MM"
    Public Const formatDate As String = "dd"

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

    Function dateFormat2(ByVal dateForChange As String, Optional ByVal separate As String = "/", Optional today As Boolean = False) As String 'format dd/MM/yyyy
        If dateForChange = "" Then
            Return ""
        End If
        'If today And dateForChange = "" Then
        '    dateForChange = Date.Now.ToString("dd" & separate & "MM" & separate & "yyyy")
        '    'Else
        '    '    Return ""
        'End If
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

    'Function dateFormat4(ByVal dateForChange As String, Optional ByVal separate As String = "/") As String 'format dd/MM/yyyy
    '    If dateForChange = "" Then
    '        Return ""
    '    End If

    '    Dim tempDate() As String = dateForChange.Split(separate)
    '    Return tempDate(2) & "-" & tempDate(1) & "-" & tempDate(0) 'yyyy-MM-dd
    'End Function

    Function dateFormat4(ByVal dateForChange As String, Optional ByVal separate As String = "/", Optional showSeparate As String = "") As String 'format dd/MM/yyyy
        If dateForChange = "" Then
            Return ""
        End If

        Dim tempDate() As String = dateForChange.Split(separate)
        Return tempDate(2) & showSeparate & tempDate(1) & showSeparate & tempDate(0) 'yyyy-MM-dd
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

    Function DateWhere(ByVal fldDate As String, ByVal dateFrom As String, ByVal dateTo As String, Optional ByVal causeVal As Boolean = False) As String
        If fldDate = "" Or (dateFrom = "" And dateTo = "") Then
            Return ""
        End If
        Dim whr As String = " and "
        If dateFrom <> "" And dateTo <> "" Then
            whr = whr & fldDate & " between '" & dateFrom & "' and '" & dateTo & "' "
        Else
            Dim symbol As String = ">='"
            If causeVal Then
                symbol = "='"
            End If
            Dim dateSel As String = symbol & dateFrom & "' "
            If dateFrom = "" Then
                symbol = "<='"
                If causeVal Then
                    symbol = "='"
                End If
                dateSel = symbol & dateTo & "' "
            End If
            whr = whr & fldDate & dateSel
        End If
        Return whr
    End Function

    'for calculate time
    Function strToDateTime(ByVal strDate As String, Optional ByVal dateFormat As String = "yyyyMMdd HH:mm") As Date ':ss
        Return DateTime.ParseExact(strDate, dateFormat, New CultureInfo("en-US"))
    End Function

    Function strToDateTimeCheck(ByVal strDate As String, ByRef dateVal As DateTime, Optional ByVal dateFormat As String = "yyyyMMdd HH:mm") As Boolean
        Dim dateCheck As Boolean = DateTime.TryParseExact(strDate, dateFormat, New CultureInfo("en-US"), DateTimeStyles.None, dateVal)
        Return dateCheck
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

    'ByVal timeStr As String, ByVal timeEnd As String,
    'Function getBreakTime(ByVal DateStr As String, ByVal DateEnd As String, ByVal dFrom As DateTime, ByVal dEnd As DateTime) As Decimal
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

    Function setDate(ByVal val As String) As String
        Dim dateVal() As String = val.Split("/")
        Dim xval As String = ""
        If dateVal.Length = 3 Then
            xval = dateVal(2) & dateVal(0) & dateVal(1)
        End If
        Return xval
    End Function

End Class
