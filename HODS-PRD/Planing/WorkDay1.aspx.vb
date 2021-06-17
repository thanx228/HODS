Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Public Class WorkDay1
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim com As SqlCommand
    Dim conn As New SqlConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strConnString As String
        strConnString = "Data Source=192.168.50.1;Initial Catalog= DBMIS;User Id=mis;Password=Mis2012;Max Pool Size=100"
        conn = New SqlConnection(strConnString)
        conn.Open()

        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Sub BindData()
        Dim strSQL As String
        strSQL = "SELECT * FROM TempWorkDay"

        Dim dtReader As SqlDataReader
        com = New SqlCommand(strSQL, conn)
        dtReader = com.ExecuteReader()

        GridView1.DataSource = dtReader
        GridView1.DataBind()
    End Sub

End Class