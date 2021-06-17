Imports MIS_HTI.DataControl
'meta data
'Name Class : GridviewRowControl
'objective : manage gridview row object

'create by : WANLOP THONGCOMDEE
'Create Date : 2019/07/06

'Update by : WANLOP THONGCOMDEE
'Update Date : 2019/07/18
'Note : add new function Text( as same function GetValString) and function Number(as same function GetValDecimal)

'Update by : WANLOP THONGCOMDEE
'Update Date : 2019/07/22
'Note : add new function Text(get from dataitem) and function Number(get from dataitem)


Namespace FormControl
    Public Class GridviewRowControl
        Public gr As GridViewRow
        Dim outCOnt As New OutputControl

        Sub New(ByRef PI_GRIDVIEWROW As GridViewRow)
            gr = PI_GRIDVIEWROW
        End Sub

        Function GetValString(columnSeq As Integer, Optional NotFindDefault As String = "") As String
            Dim cellCnt As Integer = gr.Cells.Count
            Return If(cellCnt < columnSeq Or cellCnt < 0, NotFindDefault, gr.Cells(columnSeq).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
        End Function

        Function Text(columnSeq As Integer, Optional NotFindDefault As String = "") As String
            Dim cellCnt As Integer = gr.Cells.Count
            Return If(cellCnt < columnSeq Or cellCnt < 0, NotFindDefault, gr.Cells(columnSeq).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
        End Function

        Function Text(columnName As String, Optional NotFindDefault As String = "") As String
            Return If(IsDBNull(gr.DataItem(columnName)), NotFindDefault, gr.DataItem(columnName)).ToString.Trim
        End Function

        Function GetValDecimal(columnSeq As Integer, Optional NotFindDefault As Decimal = 0) As Decimal
            Return outCOnt.checkNumberic(Text(columnSeq, NotFindDefault.ToString))
        End Function

        Function Number(columnSeq As Integer, Optional NotFindDefault As Decimal = 0) As Decimal
            Return outCOnt.checkNumberic(Text(columnSeq, NotFindDefault.ToString))
        End Function

        Function Number(columnName As String, Optional NotFindDefault As Decimal = 0) As Decimal
            Return outCOnt.checkNumberic(Text(columnName, NotFindDefault.ToString))
        End Function

        Function FindControl(controlName As String) As Control
            Return gr.FindControl(controlName)
        End Function

        Function Count() As Integer
            Return gr.Cells.Count
        End Function

        Sub SetVal(columnSeq As Integer, valSet As String)
            If columnSeq > -1 And columnSeq <= Count() Then
                gr.Cells(columnSeq).Text = valSet
            End If
        End Sub
    End Class
End Namespace
