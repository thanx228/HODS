Imports MIS_HTI.FormControl
Imports MIS_HTI.DataControl

Public Class DropDownListUserControl
    Inherits System.Web.UI.UserControl

    Dim ddlCont As New DropDownListControl
    Dim dtCont As New DataTableControl
    Dim dbConn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public ReadOnly Property getObject() As DropDownList
        Get
            Try
                Return ddl
            Catch ex As Exception
                'Response.Write(ex.Message.ToString)
                Return Nothing
            End Try
            Return Nothing
        End Get
    End Property

    Public Property Text() As String
        Get
            Try
                Return ddl.Text.Trim
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(value As String)
            Try
                ddl.Text = value
            Catch ex As Exception
                'ddl.Text = 0
                'Response.Write(ex.Message.ToString)
            End Try
        End Set
    End Property

    Function Text2(Optional ByVal headVal As String = "0") As String
        Dim val As String = ddl.Text.Trim
        Return If(val = headVal, "", val)
    End Function

    Sub show(SQL As String, strConn As String,
            fldText As String, fldValue As String,
            Optional ByVal showAll As Boolean = False,
            Optional ByVal headVal As String = "0")
        ddlCont.showDDL(ddl, SQL, strConn, fldText, fldValue, showAll, headVal)
    End Sub

    Sub show(dt As DataTable, fldText As String, fldValue As String,
             Optional ByVal showAll As Boolean = False,
             Optional ByVal headVal As String = "0")
        ddlCont.showDDL(ddl, dt, fldText, fldValue, showAll, headVal)
    End Sub

    Sub showDocType(CodeGroup As String, Optional ByVal showAll As Boolean = False, Optional whr As String = "")
        Dim SQL As String = "select MQ001,MQ001 +'-' + MQ002 MQ002 from CMSMQ where 1=1 " & dbConn.WHERE_IN("MQ003", CodeGroup,, True) & whr & " order by MQ001"
        show(SQL, VarIni.ERP, "MQ002", "MQ001", showAll)
    End Sub

    Public WriteOnly Property yyyy() As String
        Set(endYear As String)
            Try
                Dim outCont As New OutputControl
                Dim colName As New ArrayList
                colName.Add("" & VarIni.char8 & "YEAR")
                Dim dtShow As DataTable = dtCont.setColDatatable(colName, VarIni.char8)
                Dim yearEnd As Integer
                If outCont.checkNumberic(endYear) = 0 Then
                    yearEnd = CInt(Date.Now.ToString("yyyy")) + 1
                Else
                    yearEnd = CInt(endYear)
                End If
                For i As Integer = VarIni.YearBegin To yearEnd
                    Dim fldData As New ArrayList
                    fldData.Add("YEAR" & VarIni.char8 & i)
                    dtCont.addDataRow(dtShow, fldData, VarIni.char8)
                Next
                show(dtShow, "YEAR", "YEAR")
                ddl.Text = Date.Now.ToString("yyyy")
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Set
    End Property

    Public WriteOnly Property yyyymm() As String
        Set(endYear As String)
            Try
                Dim outCont As New OutputControl
                Dim colName As New ArrayList
                colName.Add("" & VarIni.char8 & "M_CODE")
                colName.Add("" & VarIni.char8 & "M_NAME")
                Dim dtShow As DataTable = dtCont.setColDatatable(colName, VarIni.char8)
                Dim yearEnd As Integer
                If outCont.checkNumberic(endYear) = 0 Then
                    yearEnd = CInt(Date.Now.ToString("yyyy")) + 1
                Else
                    yearEnd = CInt(endYear)
                End If

                Dim monthList As New ArrayList From {
                    "01" & VarIni.char8 & "Jan : มกราคม",
                    "02" & VarIni.char8 & "Feb : กุมภาพันธ์",
                    "03" & VarIni.char8 & "Mar : มีนาคม",
                    "04" & VarIni.char8 & "Apr : เมษายน",
                    "05" & VarIni.char8 & "May : พฤกษาคม",
                    "06" & VarIni.char8 & "Jun : มิถุนายน",
                    "07" & VarIni.char8 & "Jul : กรกฏาคม",
                    "08" & VarIni.char8 & "Aug : สิงหาคม",
                    "09" & VarIni.char8 & "Sep : กันยายน",
                    "10" & VarIni.char8 & "Oct : ตุลาคม",
                    "11" & VarIni.char8 & "Nov : พฤศจิกายน",
                    "12" & VarIni.char8 & "Dec : ธันวาคม"
                }
                For i As Integer = VarIni.YearBegin To yearEnd
                    For Each str As String In monthList
                        Dim aa() As String = str.Split(VarIni.char8)
                        Dim fldData As New ArrayList
                        fldData.Add("M_CODE" & VarIni.char8 & i & aa(0))
                        fldData.Add("M_NAME" & VarIni.char8 & i & "/" & aa(1))
                        dtCont.addDataRow(dtShow, fldData, VarIni.char8)
                    Next
                Next
                show(dtShow, "M_NAME", "M_CODE")
                ddl.Text = Date.Now.ToString("yyyyMM")
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Set
    End Property

    Public WriteOnly Property mm() As String
        Set(value As String)
            Try
                Dim monthList As New ArrayList From {
                    "01" & VarIni.char8 & "Jan : มกราคม",
                    "02" & VarIni.char8 & "Feb : กุมภาพันธ์",
                    "03" & VarIni.char8 & "Mar : มีนาคม",
                    "04" & VarIni.char8 & "Apr : เมษายน",
                    "05" & VarIni.char8 & "May : พฤกษาคม",
                    "06" & VarIni.char8 & "Jun : มิถุนายน",
                    "07" & VarIni.char8 & "Jul : กรกฏาคม",
                    "08" & VarIni.char8 & "Aug : สิงหาคม",
                    "09" & VarIni.char8 & "Sep : กันยายน",
                    "10" & VarIni.char8 & "Oct : ตุลาคม",
                    "11" & VarIni.char8 & "Nov : พฤศจิกายน",
                    "12" & VarIni.char8 & "Dec : ธันวาคม"
                }
                manualShow(monthList)
                ddl.Text = Date.Now.ToString("MM")
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Set
    End Property

    Public WriteOnly Property transfer() As String
        Set(value As String)
            Try
                Dim dataList As New ArrayList From {
                    "OUT" & VarIni.char8 & "OUT : Transfer From",
                    "IN" & VarIni.char8 & "IN : Transfer To"
                }
                manualShow(dataList)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Set
    End Property

    Public WriteOnly Property Symbol() As String
        Set(value As String)
            Try
                Dim dataList As New ArrayList From {
                    "=" & VarIni.char8 & "Equal(=)",
                    "<" & VarIni.char8 & "Less(<)",
                    "<=" & VarIni.char8 & "Less or Equal(<=)",
                    ">" & VarIni.char8 & "More(>)",
                    ">=" & VarIni.char8 & "More and Equal(>=)"
                }
                manualShow(dataList)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Set
    End Property

    Public Sub manualShow(dataList As ArrayList, Optional strSplit As String = VarIni.char8, Optional showAll As Boolean = False)
        If dataList.Count = 0 Then
            Exit Sub
        End If
        Dim colName As New ArrayList From {
            "" & strSplit & "M_CODE",
            "" & strSplit & "M_NAME"
        }
        Dim dtShow As DataTable = dtCont.setColDatatable(colName, strSplit)
        For Each str As String In dataList
            Dim aa() As String = str.Split(strSplit)
            Dim fldData As New ArrayList From {
                "M_CODE" & strSplit & aa(0),
                "M_NAME" & strSplit & aa(1)
            }
            dtCont.addDataRow(dtShow, fldData, strSplit)
        Next
        show(dtShow, "M_NAME", "M_CODE", showAll)
    End Sub


    'add by noi on 2021/06/11
    Public Function getAll(Optional ignoreVal As String = "0") As List(Of String)
        Dim allValue As New List(Of String)

        For Each item As ListItem In ddl.Items
            If item.Value = ignoreVal Then
                Continue For
            End If
            allValue.Add(item.Value)
        Next
        Return allValue
    End Function

    Sub setAutoPostback(Optional AutoPostBack As Boolean = True)
        ddl.AutoPostBack = AutoPostBack
    End Sub

    Public Event SelectedIndexChanged(sender As Object, e As EventArgs)
    Protected Sub ddl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged(sender, e)
    End Sub

End Class