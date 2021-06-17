Imports MIS_HTI.DataControl
Imports System.Data

Namespace FormControl
    Public Class CheckBoxListControl
        Dim dbConn As New DataConnectControl

        Sub showCheckboxList(ByRef cbl As CheckBoxList,
                             SQL As String, dbType As String,
                             fldText As String, fldValue As String,
                             Optional showColumn As Decimal = 0)

            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            Dim whoCall As String = dbConn.WhoCalledMe
            If SQL = String.Empty Or
                dbType = String.Empty Or
                fldText = String.Empty Or
                fldValue = String.Empty Then
                dbConn.returnPageError(PathFile, "Data input Is Empty", whoCall)
            End If
            With cbl
                Try
                    .DataSource = dbConn.Query(SQL, dbType, whoCall)
                    .DataTextField = fldText
                    .DataValueField = fldValue
                    .DataBind()
                    .RepeatColumns = showColumn
                    .RepeatDirection = RepeatDirection.Horizontal
                    .RepeatLayout = RepeatLayout.Table
                Catch ex As Exception
                    dbConn.returnPageError(PathFile, ex.Message, whoCall)
                End Try
            End With
        End Sub

        Sub showCheckboxList(ByRef cbl As CheckBoxList, dt As System.Data.DataTable, fldText As String, fldValue As String,
                             Optional showColumn As Decimal = 0)

            Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            Dim whoCall As String = dbConn.WhoCalledMe
            'If dt.Rows.Count = 0 Then
            '    cbl.Items.Clear()
            'End If
            With cbl
                cbl.Items.Clear()
                Try
                    .DataSource = dt
                    .DataTextField = fldText
                    .DataValueField = fldValue
                    .DataBind()
                    .RepeatColumns = showColumn
                    .RepeatDirection = RepeatDirection.Horizontal
                    .RepeatLayout = RepeatLayout.Table
                Catch ex As Exception
                    dbConn.returnPageError(PathFile, ex.Message, whoCall)
                End Try
            End With
        End Sub

        Function GetValue(ByRef cbl As CheckBoxList,
                          Optional checkReturn As Integer = 1,
                          Optional separate As String = ",",
                          Optional valReturnEmpty As String = "0") As String
            'checked value =0(All),1=checked  and 2=not check
            Dim valReturn As String = String.Empty
            Dim AllCheck As Boolean = False
            Dim checked As Boolean = False
            Select Case checkReturn
                Case 0
                    AllCheck = True
                Case 1
                    checked = True
                Case 2
                    checked = False
            End Select
            For Each boxItem As ListItem In cbl.Items
                Dim boxVal As String = CStr(boxItem.Value.Trim)
                If AllCheck Or boxItem.Selected = checked Then
                    valReturn &= boxVal & separate
                End If
            Next
            Return If(valReturn = String.Empty, valReturnEmpty, valReturn)
        End Function

        Sub setValue(ByRef cbl As CheckBoxList, dt As System.Data.DataTable, fld As String)
            Dim dtCont As New DataTableControl
            For Each dr As DataRow In dt.Rows
                Dim val As String = dtCont.IsDBNullDataRow(dr, fld)
                If val <> String.Empty Then
                    Dim boxitem As ListItem = cbl.Items.FindByValue(val)
                    If boxitem IsNot Nothing Then
                        boxitem.Selected = True
                    End If
                End If
            Next
        End Sub

        Sub setValue(ByRef cbl As CheckBoxList, sql As String, dbType As String, fld As String)
            Dim dbConn As New DataConnectControl
            setValue(cbl, dbConn.Query(sql, dbType, dbConn.WhoCalledMe), fld)
        End Sub

    End Class
End Namespace