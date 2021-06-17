Imports System.Xml
Namespace DataControl
    Public Class StringConnection
        Inherits System.Web.UI.Page
        Dim filePath As String = Server.MapPath("~/App_Code/Class/Config/")

        Function getArea() As String
            Dim fileConfigOracle As New XmlDocument
            fileConfigOracle.Load(filePath & "configOracle.xml")
            Dim NodeListOracle As XmlNodeList = fileConfigOracle.SelectNodes("/config/T100")
            Dim ServiceName As String = String.Empty
            For Each NodeOracel As XmlNode In NodeListOracle
                ServiceName = NodeOracel.Item("ServiceName").InnerText
            Next
            Dim nameReturn As String = ""
            Select Case ServiceName
                Case VarIni.DEV
                    nameReturn = "Developing"
                Case VarIni.QAS
                    nameReturn = "Testing"
                Case VarIni.PRD
                    nameReturn = "Production"
            End Select
            Return nameReturn
        End Function


        ''Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.50.9)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=T100DEV)));Persist Security Info=True;User Id=dsdemo;Password=dsdemo;
        Function getStringConnectOracle(dbName As String, Optional Zone As String = "T100") As String
            If dbName = String.Empty Then
                Return String.Empty
            End If
            Dim nodeMain As String = "/config/" & Zone
            'Dim ZoneUse As String = String.Empty
            'Dim fileZone As XmlDocument = New XmlDocument
            'fileZone.Load(filePath & "SelectZone.xml")
            'Dim NodeListZone As XmlNodeList = fileZone.SelectNodes("SELECT")
            'For Each NodeZone As XmlNode In NodeListZone
            '    ZoneUse = NodeZone.Item("ZONE").InnerText
            'Next
            'If ZoneUse = String.Empty Then
            '    Return String.Empty
            'End If

            Dim fileConfigOracle As New XmlDocument
            fileConfigOracle.Load(filePath & "configOracle.xml")

            Dim Host As String = String.Empty
            Dim Port As String = String.Empty
            Dim ServiceName As String = String.Empty
            Dim UserName As String = String.Empty
            Dim userPass As String = String.Empty
            Dim NodeListOracle As XmlNodeList = fileConfigOracle.SelectNodes(nodeMain)
            If NodeListOracle.Count = 0 Then
                Return String.Empty
            End If
            For Each NodeOracel As XmlNode In NodeListOracle
                Host = NodeOracel.Item("Host").InnerText
                Port = NodeOracel.Item("Port").InnerText
                ServiceName = NodeOracel.Item("ServiceName").InnerText
            Next
            NodeListOracle = fileConfigOracle.SelectNodes(nodeMain & "/" & dbName)
            For Each NodeOracel As XmlNode In NodeListOracle
                UserName = NodeOracel.Item("UserId").InnerText
                userPass = NodeOracel.Item("Password").InnerText
            Next
            Return "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" & Host & ")(PORT=" & Port & ")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" & ServiceName & ")));Persist Security Info=True;User Id=" & UserName & ";Password=" & userPass & ";"
        End Function

        Function getStringConnectSQL(dbName As String) As String
            If dbName = String.Empty Then
                Return String.Empty
            End If
            Dim fileConfigOracle As New XmlDocument
            fileConfigOracle.Load(filePath & "configSQL.xml")
            Dim Host As String = String.Empty
            Dim UserName As String = String.Empty
            Dim userPass As String = String.Empty
            Dim NodeListSQL As XmlNodeList = fileConfigOracle.SelectNodes("/config/" & dbName)
            If NodeListSQL.Count = 0 Then
                Return String.Empty
            End If
            For Each NodeOracel As XmlNode In NodeListSQL
                Host = NodeOracel.Item("Host").InnerText
                UserName = NodeOracel.Item("UserId").InnerText
                userPass = NodeOracel.Item("Password").InnerText
            Next
            Return "Data Source=" & Host & ";Initial Catalog= " & dbName & ";User Id=" & UserName & ";Password=" & userPass & ";Max Pool Size=1000"
        End Function

        Function getStringConnectMySQL() As String
            Dim fileConfigOracle As New XmlDocument
            fileConfigOracle.Load(filePath & "configMySQL.xml")
            Dim Host As String = String.Empty
            Dim dbName As String = String.Empty
            Dim UserName As String = String.Empty
            Dim userPass As String = String.Empty
            Dim port As String = String.Empty
            'Dim NodeListMySQL As XmlNodeList = fileConfigOracle.SelectNodes("/config/JSIMS")
            Dim NodeListMySQL As XmlNodeList = fileConfigOracle.SelectNodes("/config/ERMIS")
            If NodeListMySQL.Count = 0 Then
                Return String.Empty
            End If
            For Each NodeOracel As XmlNode In NodeListMySQL
                Host = NodeOracel.Item("Host").InnerText
                dbName = NodeOracel.Item("DataBaseName").InnerText
                UserName = NodeOracel.Item("UserId").InnerText
                userPass = NodeOracel.Item("Password").InnerText
                port = NodeOracel.Item("port").InnerText

            Next
            'Return "Server=" & Host & ";Database=" & dbName & ";Uid=" & UserName & ";Pwd=" & userPass & ";"
            Return "Server=" & Host & ";Database=" & dbName & ";user=" & UserName & ";password=" & userPass & ";port=" & port & ";"
        End Function

        Function checkDbOracle(dbName As String) As Boolean
            Dim valReturn As Boolean = False
            'If dbName = VarIni.T100 Or dbName = VarIni.JODS Or dbName = VarIni.JSIMS Then
            '    valReturn = True
            'End If
            Return valReturn
            '  Return Regex.IsMatch(dbName, VarIni.ORACLE)
        End Function


        Function checkDbSQL(dbName As String) As Boolean
            Dim valReturn As Boolean = False
            If dbName = VarIni.ERP Or dbName = VarIni.DBMIS Or dbName = VarIni.HRMHT Then
                valReturn = True
            End If
            Return valReturn
            'Public Const SQLSERVER As String = "^[(" & ERP & ")|(" & DBMIS & ")|(" & DBMIST100 & ")|(" & DBMIST100TST & ")]" 'sqlserver
            'Return Regex.IsMatch(dbName, VarIni.SQLSERVER)
        End Function
    End Class
End Namespace