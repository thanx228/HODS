Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web

Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim Program As New DataTable
            Program = Conn_SQL.Get_DataReader("Select MD001,MD002 from CMSMD order by MD002", Conn_SQL.ERP_ConnectionString) ' Machines
            For i As Integer = 0 To Program.Rows.Count - 1
                DropDownList1.Items.Add(New ListItem(Program.Rows(i).Item("MD002"), Program.Rows(i).Item("MD001")))
            Next

        End If


    End Sub

    Private Sub DDLChange()

      
            'DropDownList2.Items.Clear()

        'Dim Program1 As New DataTable
        'Program1 = Conn_SQL.Get_DataReader("Select MX001,MX002 from CMSMX where MX002='" & DropDownList1.SelectedValue & "'", Conn_SQL.ERP_ConnectionString) ' Work Center
        'For i As Integer = 0 To Program1.Rows.Count - 1
        '    DropDownList2.Items.Add(New ListItem(Program1.Rows(i).Item("MX001"), Program1.Rows(i).Item("MX001")))
        'Next

        'Dim dataPage As ControlDataForm = New ControlDataForm
        'Dim SQL As String = "Select MX001,MX002 from CMSMX where MX002='" & DropDownList1.SelectedValue & "'"
        'Dim fldVal As String = "MX001"
        'Dim fldTxt As String = "MX001"
        'dataPage.showDDL(DropDownList2, SQL, Conn_SQL.ERP_ConnectionString, fldTxt, fldVal)

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click

        Label3.Text = DropDownList2.SelectedValue
    End Sub

End Class