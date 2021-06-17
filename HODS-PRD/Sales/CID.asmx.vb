Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<ToolboxItem(False)> _
Public Class CID
    Inherits System.Web.Services.WebService

    Private ReadOnly Property ConnectionString() As String
        Get
            Return "Data Source=192.168.50.1;Initial Catalog= JINPAO80;User Id=sa;Password=Alex0717;Max Pool Size=100"
        End Get
    End Property
    Private ReadOnly Property Connection() As SqlConnection
        Get
            Dim ConnectionToFetch As New SqlConnection(ConnectionString)
            ConnectionToFetch.Open()
            Return ConnectionToFetch
        End Get
    End Property
    <WebMethod()> _
    Public Function Get_CID(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim SelectQry = "select distinct TA004 from [JINPAO80].[dbo].[ACRTA] where TA004 like '" & prefixText & "%'"

        Dim Results As New ArrayList
        Try
            Using Command As New SqlCommand(SelectQry, Connection)
                Using Reader As SqlDataReader = Command.ExecuteReader()
                    Dim Counter As Integer
                    While Reader.Read
                        If (Counter = count) Then Exit While
                        Results.Add(Reader("TA004").ToString())
                        Counter += 1
                    End While
                End Using
                Dim ResultsArray(Results.Count - 1) As String
                ResultsArray = Results.ToArray(GetType(System.String))
                Return ResultsArray
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class