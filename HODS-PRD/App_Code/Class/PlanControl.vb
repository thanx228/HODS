Imports System.Drawing
Namespace DataControl
    Public Class PlanControl

        Function getColorPlan(perLoad As Decimal) As Color
            Dim foreColor As Color
            If perLoad < 70 Then
                foreColor = Color.Black
            ElseIf perLoad >= 70 And perLoad <= 100 Then '
                foreColor = Color.Green
            Else
                foreColor = Color.Red
            End If
            Return foreColor
        End Function

        Function getTextPlan(perLoad As Decimal) As String
            Dim NumberOver As String = ""
            If perLoad < 70 Then
                NumberOver = "<70%"
            ElseIf perLoad >= 70 And perLoad <= 100 Then
                NumberOver = "70-100%"
            Else
                NumberOver = ">100%"
            End If
            Return NumberOver
        End Function

        'Function checkWorkcenterSharePart(wc As String) As Boolean
        '    Dim valReturn As Boolean = False
        '    If wc = "WC01" Or
        '       wc = "WC02" Or
        '       wc = "WC23" Or
        '       wc = "WC27" Or
        '       wc = "WC52" Then
        '        valReturn = True
        '    End If
        '    Return valReturn
        'End Function

        Function calUsageTime(Qty As Decimal, fix As Decimal, std As Decimal, Optional showMinite As Boolean = True) As Decimal
            If Qty = 0 Then
                Return Qty
            End If
            Dim cal As Decimal = fix + (std * Qty)
            Return If(showMinite, Math.Ceiling(cal / 60), cal)
        End Function

    End Class
End Namespace