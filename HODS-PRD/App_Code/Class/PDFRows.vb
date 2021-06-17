Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports MIS_HTI.DataControl
Imports System.Data

Namespace PDF
    Public Class PDFRows

        Private _format As ArrayList 'field name or value & varini.char8 & align value(L,C,R) & varini.char8 & font style(B,N)
        Private _table As PdfPTable
        Private _strSplit As String
        Private _size As Single = 12.0F
        Private _color As BaseColor = BaseColor.BLACK
        Private _line As Integer


        Sub New(ByRef PI_table As PdfPTable, PI_format As ArrayList,
                Optional PI_Size As Single = 10.0F,
                Optional PI_strSplit As String = VarIni.char8)
            Table = PI_table
            Format = PI_format
            SplitStr = PI_strSplit
            Size = PI_Size
            Seq = 0
        End Sub

        Public Property SplitStr As String
            Get
                Return _strSplit
            End Get
            Set
                _strSplit = SplitStr
            End Set
        End Property

        Public Property Seq As Integer
            Get
                Return _line
            End Get
            Set
                _line = Seq
            End Set
        End Property

        Public Property Size As Single
            Get
                Return _size
            End Get
            Set
                _size = Size
            End Set
        End Property

        Public Property Format As ArrayList
            Get
                Return _format
            End Get
            Set
                _format = Format
            End Set
        End Property

        Public Property Table As PdfPTable
            Get
                Return _table
            End Get
            Set
                _table = Table
            End Set
        End Property


        Public Property Color As BaseColor
            Get
                Return _color
            End Get
            Set
                _color = Color
            End Set
        End Property


        'datatable
        Sub Rows(dt As DataTable)
            Try
                For Each dr As DataRow In dt.Rows
                    Rows(dr)
                Next
            Catch ex As Exception

            End Try
        End Sub
        'datarow
        Sub Rows(dr As DataRow)
            Try
                With New PDFControl
                    Dim drCont As New DataRowControl(dr)
                    If Seq > 0 Then
                        Table.AddCell(.Cell(Seq, Size, PdfPCell.ALIGN_CENTER, Color))
                    End If
                    For Each str As String In Format
                        Dim fldName As String = str.Split(SplitStr).ToArray(0)
                        Dim align As Integer = getAlign(str.Split(SplitStr).ToArray(1)) 'L,C,R
                        Dim style As Integer = getStyle(str.Split(SplitStr).ToArray(2))
                        Table.AddCell(.Cell(drCont.Text(fldName), Size, style, Color))
                    Next
                End With
            Catch ex As Exception

            End Try

        End Sub

        'arraylist
        Sub Rows(al As ArrayList)

        End Sub

        Function getAlign(txtAlign As String) As Integer
            Dim valAlign As Integer = PdfPCell.ALIGN_LEFT
            Try
                If Not String.IsNullOrEmpty(txtAlign) Then
                    Select Case txtAlign
                        Case "C"
                            valAlign = PdfPCell.ALIGN_CENTER
                        Case "R"
                            valAlign = PdfPCell.ALIGN_RIGHT
                    End Select
                End If
            Catch ex As Exception

            End Try
            Return valAlign
        End Function

        Function getStyle(txtStyle As String)
            Dim valStyle As Integer = Font.NORMAL
            Try
                Select Case txtStyle
                    Case "B"
                        valStyle = Font.BOLD
                    Case "I"
                        valStyle = Font.ITALIC
                    Case "U"
                        valStyle = Font.UNDERLINE
                End Select
            Catch ex As Exception

            End Try
            Return valStyle
        End Function

    End Class
End Namespace
