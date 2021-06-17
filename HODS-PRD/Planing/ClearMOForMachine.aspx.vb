Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Imports System.Data.SqlClient
Imports System.Collections.Generic
Public Class ClearMOForMachine
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim dbConn As New DataConnectControl
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    'Dim NowSelectMachine As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            'Session("UserName") = "500026"
            'Session("UserId") = "500026"
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        Else
        End If
    End Sub



End Class