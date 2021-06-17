Imports MIS_HTI.DataControl
Imports System.Data
Namespace FormControl
    Public Class DropDownListControl
        Dim dbConn As New DataConnectControl

        Public Sub showDDL(ByRef ControlDDL As DropDownList,
                           ByVal SQL As String,
                           ByVal dbType As String,
                           ByVal fldText As String,
                           ByVal fldValue As String,
                           Optional ByVal setAll As Boolean = False,
                           Optional ByVal headVal As String = "0")

            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            Dim whoCall As String = dbConn.WhoCalledMe
            If SQL = String.Empty Or dbType = String.Empty Or
                fldText = String.Empty Or fldValue = String.Empty Then
                dbConn.returnPageError(PathFile, "Data input Is Empty", whoCall, SQL)
            End If

            showDDL(ControlDDL, dbConn.Query(SQL, dbType, whoCall), fldText, fldValue, setAll, headVal)
        End Sub

        Public Sub showDDL(ByRef ControlDDL As DropDownList,
                           dt As DataTable, fldText As String, fldValue As String,
                           Optional ByVal setAll As Boolean = False,
                           Optional ByVal headVal As String = "0")

            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            Dim whoCall As String = dbConn.WhoCalledMe

            With ControlDDL
                .Items.Clear()
                Try
                    .DataSource = dt
                    .DataTextField = fldText
                    .DataValueField = fldValue
                    .DataBind()
                    If setAll Then
                        .Items.Insert(0, New ListItem("===Select===", headVal))
                    End If
                Catch ex As Exception
                    dbConn.returnPageError(PathFile, ex.Message, whoCall, "DATATABLE")
                End Try
            End With
        End Sub

        Sub setValue(ByRef ddl As DropDownList, val As String, Optional checkValue As Boolean = True)

            If checkValue Then
                If ddl.Items.FindByValue(val) IsNot Nothing Then
                    ddl.Items.FindByValue(val).Selected = True
                Else
                    ddl.SelectedIndex = 0
                End If
            Else
                If ddl.Items.FindByText(val) IsNot Nothing Then
                    ddl.Items.FindByText(val).Selected = True
                Else
                    ddl.SelectedIndex = 0
                End If
            End If
        End Sub

        Function checkExistVal(ddl As DropDownList, Optional valCheck As String = "0") As Boolean
            Return If(ddl.Items.Count = 0 Or ddl.Items.FindByValue(valCheck) Is Nothing, False, True)
        End Function

    End Class
End Namespace