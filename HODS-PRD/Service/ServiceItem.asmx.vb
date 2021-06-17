Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports MIS_HTI.DataControl


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class ServiceItem
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function SearchSpec(ByVal prefixText As String, ByVal count As Integer) As List(Of String)

        Dim returnList As New List(Of String)
        If Not String.IsNullOrEmpty(prefixText) Then
            Dim dbconn As New DataConnectControl
            Dim fldName As New ArrayList From {"MB003"}
            Dim strSQL As New SQLString("INVMB", fldName,, count)
            With strSQL
                Dim whr As String = ""
                whr &= .WHERE_EQUAL("INVMB.MB109", "Y")
                whr &= .WHERE_EQUAL("INVMB.MB025", "M")
                whr &= .WHERE_LIKE("INVMB.MB003", prefixText)
                .SetWhere(whr, True)
                .SetOrderBy("INVMB.MB003")
            End With
            returnList = dbconn.QueryList(strSQL.GetSQLString, VarIni.ERP,, dbconn.WhoCalledMe)
        End If
        Return returnList
    End Function

End Class