Imports System.IO

Public Class OpenFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fileName As String = Request.QueryString("fileName")
        'Get the physical Path of the file
        Dim filePath As String = Server.MapPath(Request.QueryString("filePath") & fileName)
        'Create New instance of FileInfo class to get the properties of the file being downloaded
        Dim file As FileInfo = New FileInfo(filePath)
        Dim showLb As Boolean = True
        If file.Exists Then
            'Clear the content of the response
            Response.ClearContent()
            'Add the file name and attachment, which will force the open/cance/save dialog to show, to the header
            'Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name)
            'Add the file size into the response header
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Set the ContentType
            Response.ContentType = ReturnExtension(file.Extension.ToLower())
            'Write the file into the response
            Response.TransmitFile(file.FullName)
            'End the response
            Response.End()
            showLb = False

        End If
        'Dim WebClient As client = New WebClient()
        lbNotFound.Visible = showLb
    End Sub
    Protected Function ReturnExtension(fileExtension As String) As String
        Dim txt As String = "application/octet-stream"
        Select Case fileExtension
            Case ".htm"
            Case ".html"
            Case ".log"
                txt = "text/HTML"
            Case ".txt"
                txt = "text/plain"
            Case ".doc"
                txt = "application/ms-word"
            Case ".tiff"
            Case ".tif"
                txt = "image/tiff"
            Case ".asf"
                txt = "video/x-ms-asf"
            Case ".avi"
                txt = "video/avi"
            Case ".xls"
            Case ".csv"
                txt = "application/vnd.ms-excel"
            Case ".gif"
                txt = "image/gif"
            Case ".jpg"
            Case "jpeg"
                txt = "image/jpeg"
            Case ".bmp"
                txt = "image/bmp"
            Case ".wav"
                txt = "audio/wav"
            Case ".mp3"
                txt = "audio/mpeg3"
            Case ".mpg"
            Case "mpeg"
                txt = "video/mpeg"
            Case ".rtf"
                txt = "application/rtf"
            Case ".asp"
                txt = "text/asp"
            Case ".pdf"
                txt = "application/pdf"
            Case ".fdf"
                txt = "application/vnd.fdf"
            Case ".ppt"
                txt = "application/mspowerpoint"
            Case ".dwg"
                txt = "image/vnd.dwg"
            Case ".msg"
                txt = "application/msoutlook"
            Case ".xml"
            Case ".sdxl"
                txt = "application/xml"
            Case ".xdp"
                txt = "application/vnd.adobe.xdp+xml"
        End Select
        Return txt
    End Function
End Class