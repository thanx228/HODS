Imports System
Imports System.IO
Imports System.Configuration
Imports System.Web
Imports System.Web.UI
Imports System.Data
Imports System.Web.Security
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Drawing
'Namespace Export
Public Class ExportsUtility

    ''' <summary>
    ''' '####### Export to Excel Routing 1 ##########################
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <param name="UsingGridview"></param>
    Public Shared Sub ExportGridviewToMsExcel(ByVal strFileName As String, UsingGridview As GridView, Optional isText As Boolean = True)


        Dim sfileName As String = strFileName & "-" & DateTime.Now.ToString("yyyyMMdd:hh:mm:ss") & ".xls"
        Dim sAttachfiels As String = "attachment;filename=" & sfileName
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.AddHeader("content-disposition", sAttachfiels)

        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            UsingGridview.AllowPaging = False
            UsingGridview.HeaderRow.BackColor = Color.White
            'Dim typeHash As New Hashtable
            Dim col As Integer = 0
            'For Each cell As TableCell In UsingGridview.HeaderRow.Cells
            '    cell.BackColor = UsingGridview.HeaderStyle.BackColor
            '    cell.BorderStyle = BorderStyle.Inset
            '    cell.Wrap = False
            '    'typeHash.Add(col, UsingGridview.Columns(col). )
            '    col += 1
            'Next
            For Each cell As TableCell In UsingGridview.HeaderRow.Cells
                cell.BackColor = UsingGridview.HeaderStyle.BackColor
                cell.BorderStyle = BorderStyle.Inset
                cell.Wrap = False
                Dim controls As New List(Of Control)()

                'Add controls to be removed to Generic List
                For Each control As Control In cell.Controls
                    controls.Add(control)
                Next

                'Loop through the controls to be removed and replace then with Literal
                For Each control As Control In controls
                    Select Case control.GetType().Name
                        Case "HyperLink"
                            cell.Controls.Add(New Literal() With {
                                 .Text = TryCast(control, HyperLink).Text.ToString()
                                })
                            Exit Select
                        Case "Label"
                            'cell.Controls.Add(New Literal() With {
                            '     .Text = TryCast(control, Label).Text.ToString()
                            '    })
                            Exit Select
                        Case "TextBox"
                            cell.Controls.Add(New Literal() With {
                                 .Text = TryCast(control, TextBox).Text.ToString()
                                })
                            Exit Select
                        Case "LinkButton"
                            cell.Controls.Add(New Literal() With {
                                 .Text = TryCast(control, LinkButton).Text.ToString()
                                })
                            Exit Select
                        Case "CheckBox"
                            cell.Controls.Add(New Literal() With {
                                 .Text = TryCast(control, CheckBox).Text.ToString()
                                })
                            Exit Select
                        Case "RadioButton"
                            cell.Controls.Add(New Literal() With {
                                 .Text = TryCast(control, RadioButton).Text.ToString()
                                })
                            Exit Select
                    End Select
                    cell.Controls.Remove(control)
                Next
            Next


            For Each row As GridViewRow In UsingGridview.Rows
                'row.BackColor = Color.White
                col = 0
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = UsingGridview.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = UsingGridview.RowStyle.BackColor
                    End If

                    If TypeOf (cell.Text) Is String Then
                        cell.CssClass = "textmode"
                    End If

                    cell.BorderStyle = BorderStyle.Inset
                    cell.Wrap = False
                    Dim controls As New List(Of Control)()

                    'Add controls to be removed to Generic List
                    For Each control As Control In cell.Controls
                        controls.Add(control)
                    Next

                    'Loop through the controls to be removed and replace then with Literal
                    For Each control As Control In controls
                        Select Case control.GetType().Name
                            Case "HyperLink"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, HyperLink).Text.ToString()
                                    })
                                Exit Select
                            Case "Label"
                                'cell.Controls.Add(New Literal() With {
                                '     .Text = TryCast(control, Label).Text.ToString()
                                '    })
                                Exit Select
                            Case "TextBox"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, TextBox).Text.ToString()
                                    })
                                Exit Select
                            Case "LinkButton"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, LinkButton).Text.ToString()
                                    })
                                Exit Select
                            Case "CheckBox"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, CheckBox).Text.ToString()
                                    })
                                Exit Select
                            Case "RadioButton"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, RadioButton).Text.ToString()
                                    })
                                Exit Select
                        End Select
                        cell.Controls.Remove(control)
                        col += 1
                    Next
                Next
            Next
            UsingGridview.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode {" & If(isText, "mso-number-format:\@;", "") & "} </style>"
            'Dim style As String = "<style> .textmode { } </style>"
            HttpContext.Current.Response.Write(style)
            HttpContext.Current.Response.Output.Write(sw.ToString())
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.End()
        End Using
    End Sub

    ''' <summary>
    '''     '#####Export to Excel Routing 2 ####################
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <param name="gv"></param>
    Public Shared Sub ExportToExcel(ByVal strFileName As String, ByVal gv As GridView)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        HttpContext.Current.Response.Charset = ""
        Dim oStringWriter As New System.IO.StringWriter
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
        gv.RenderControl(oHtmlTextWriter)
        HttpContext.Current.Response.Write(oStringWriter.ToString())
        HttpContext.Current.Response.[End]()
    End Sub

    ''' <summary>
    '''    '###### Export to Excel Routing 3 #######################
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <param name="gv"></param>
    Public Shared Sub Export(fileName As String, gv As GridView)
            HttpContext.Current.Response.Clear()
            Dim sfileName As String = fileName & "-" & DateTime.Now.ToString("yyyyMMdd:hh:mm:ss") & ".xls"
            Dim sAttachfiels As String = "attachment;filename=" & sfileName
            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.AddHeader("content-disposition", sAttachfiels)
            HttpContext.Current.Response.Charset = ""
            HttpContext.Current.Response.ContentType = "application/ms-excel"
            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    '  Create a form to contain the grid
                    Dim table As New Table()

                    ''  add the header row to the table
                    If gv.HeaderRow IsNot Nothing Then
                        ' gv.HeaderRow.BorderStyle = BorderStyle.Inset
                        ExportsUtility.PrepareControlForExport(gv.HeaderRow)
                        table.Rows.Add(gv.HeaderRow)
                    End If

                    For Each Hcell As TableCell In gv.HeaderRow.Cells
                        'row.BorderStyle = BorderStyle.Inset
                        ExportsUtility.PrepareControlForExport(Hcell)
                        ' table.Rows.Add(Hcell)
                    Next

                    '  add each of the data rows to the table
                    For Each row As GridViewRow In gv.Rows
                        'row.BorderStyle = BorderStyle.Inset
                        ExportsUtility.PrepareControlForExport(row)
                        'table.Rows.Add(row)
                    Next

                    '  add the footer row to the table
                    If gv.FooterRow IsNot Nothing Then
                        ExportsUtility.PrepareControlForExport(gv.FooterRow)
                        ' table.Rows.Add(gv.FooterRow)
                    End If

                    '  render the table into the htmlwriter
                    gv.RenderControl(htw)

                    '  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()
                End Using
            End Using
        End Sub
    ''' <summary>
    ''' ##### Replace any of the contained controls with literals
    ''' </summary>
    ''' <param name="control"></param>
    Private Shared Sub PrepareControlForExport(control As Control)
            Dim i As Integer = 0
            While i < control.Controls.Count
                Dim current As Control = control.Controls(i)
                If TypeOf current Is LinkButton Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl((TryCast(current, LinkButton)).Text))
                ElseIf TypeOf current Is ImageButton Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl((TryCast(current, ImageButton)).AlternateText))
                ElseIf TypeOf current Is HyperLink Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl((TryCast(current, HyperLink)).Text))
                ElseIf TypeOf current Is DropDownList Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl((TryCast(current, DropDownList)).SelectedItem.Text))
                ElseIf TypeOf current Is CheckBox Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl(If((TryCast(current, CheckBox)).Checked, "True", "False")))
                End If

                If current.HasControls() Then
                    ExportsUtility.PrepareControlForExport(current)
                End If
                i += 1
            End While
        End Sub
    ''' <summary>
    ''' ########### to CVS Routing 1 ###################################
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <param name="gv"></param>
    Public Shared Sub ExportCVS(fileName As String, gv As GridView)
        Dim sfileName As String = fileName & "-" & DateTime.Now.ToString("yyyyMMdd:hh:mm:ss") & ".csv"
        Dim sAttachfiels As String = "attachment;filename=" & sfileName
        'Build the CSV file data as a Comma separated string.
        Dim csv As String = String.Empty
        For Each column As DataColumn In gv.Columns
            'Add the Header row for CSV file.
            csv += column.ColumnName + ","c
        Next
        'Add new line.
        csv += vbCr & vbLf
        For Each row As DataRow In gv.Rows
            For Each column As DataColumn In gv.Columns
                'Add the Data rows.
                csv += row(column.ColumnName).ToString().Replace(",", ";") + ","c
            Next
            'Add new line.
            csv += vbCr & vbLf
        Next
        'Download the CSV file.
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.AddHeader("content-disposition", sAttachfiels)
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.ContentType = "application/text"
        HttpContext.Current.Response.Output.Write(csv)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Sub
End Class
'End Namespace
