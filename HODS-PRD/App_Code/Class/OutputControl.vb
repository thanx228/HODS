Namespace DataControl
    Public Class OutputControl
        Function checkNumberic(str As String, Optional valForReturn As Decimal = 0, Optional valReplace As String = "") As Decimal
            Try
                Dim valReturn As Decimal = valForReturn
                str = str.Replace(",", "")
                If Not String.IsNullOrEmpty(valReplace) Then
                    str = str.Replace(valReplace, "")
                End If
                If Not String.IsNullOrEmpty(str) And IsNumeric(str) Then
                    valReturn = CDec(str)
                End If
                Return valReturn
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function checkNumberic(ByVal tb As TextBox) As Decimal
            Return checkNumberic(tb.Text)
        End Function

        Function replaceSingleQuote(val As String) As String
            If String.IsNullOrEmpty(val) Then
                Return ""
            Else
                Return val.Replace("'", "''")
            End If
        End Function

        Function numberFormat(str As String, Optional formatStr As String = "{0:n0}") As String
            Dim txtReturn As String = ""
            If str <> String.Empty Then
                Dim numberString As Double = CDbl(str)
                txtReturn = String.Format(formatStr, numberString)
            End If
            Return txtReturn
        End Function

        Function IsLaborWorkCenter(WC As String) As Boolean
            Return Regex.IsMatch(WC, VarIni.wcLabor)
        End Function

        Function trim(tb As TextBox) As String
            Return tb.Text.Trim
        End Function

        Function trim(ddl As DropDownList) As String
            Return ddl.Text.Trim
        End Function

        'add 2015-09-07 by noi
        Function EncodeTo64UTF8(ByVal valforEnCode As String) As String
            Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(valforEnCode))
        End Function

        Function DecodeFrom64(ByVal valforDeCode As String) As String
            Return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(valforDeCode))
        End Function

        '--Add By Top at 18-08-2017
        Public Function ShowItem(GetItem As String) As String
            If GetItem = String.Empty Then
                Return GetItem
            End If
            Dim Item As String = GetItem
            If GetItem.Length = 16 Then '--Length Type = 80214031013000-05
                Item = GetItem.Substring(0, 14) & "-" & GetItem.Substring(14, 2)
            End If
            Return Item
        End Function

        Function checkStringValue(str As String) As String
            Return str.Replace("&nbsp;", "").Trim
        End Function

        Public Function ISNULL(str As String) As Boolean
            Return String.IsNullOrEmpty(str)
        End Function

        Public Function JOIN(strList As List(Of String), Optional conStr As String = "") As String
            Return String.Join(conStr, strList)
        End Function
    End Class
End Namespace