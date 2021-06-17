Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class ConfigGridview
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Public Sub ShowGridView(ByRef Gridview1 As Object, ByVal str_sqlcommand As String)
        Dim SelSQL As String = str_sqlcommand
        Dim da As New SqlDataAdapter(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim ds As New DataSet
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            Gridview1.DataSource = ds
            Gridview1.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            Gridview1.DataSource = ds
            Gridview1.DataBind()
            Dim ColumnCount As Integer = Gridview1.Rows(0).Cells.Count
            Gridview1.Rows(0).Cells.Clear()
            Gridview1.Rows(0).Cells.Add(New TableCell())
            Gridview1.Rows(0).Cells(0).ColumnSpan = ColumnCount
            Gridview1.Rows(0).Cells(0).Text = "No Record Found"
        End If
    End Sub

    
End Class
