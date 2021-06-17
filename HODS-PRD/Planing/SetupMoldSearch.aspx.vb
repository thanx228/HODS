Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class SetupMoldSearch
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim dbConn As New DataConnectControl
    Dim SelectDate As String = ""
    Dim WorkCenter As String = ""
    Dim NowSelectMachine As String
    Dim MachineCode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Session("UserName") = "500026"
            'Session("UserId") = "500026"
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

        End If

    End Sub

    Private Sub ShowGridview()
        Dim dt = New DataTable,
        colName As New ArrayList

        dt.Columns.Add("A") '0
        colName.Add("DocDate:A")

        dt.Columns.Add("B") '1
        colName.Add("DocTime:B")

        dt.Columns.Add("C") '1
        colName.Add("DocNo:C")

        dt.Columns.Add("D") '1
        colName.Add("Seq:D")

        'dt.Columns.Add("Emp Name") '3
        dt.Columns.Add("E") '3
        colName.Add("MO Number:E")

        dt.Columns.Add("F") '4
        colName.Add("MO Seq:F")

        dt.Columns.Add("G") '5
        colName.Add("MacNo:G")

        dt.Columns.Add("H") '5
        colName.Add("Qty:H")

        dt.Columns.Add("I") '5
        colName.Add("ItemNo:I")

        dt.Columns.Add("J") '5
        colName.Add("ItemSpec:J")

        dt.Columns.Add("K") '5
        colName.Add("MatCode:K")

        dt.Columns.Add("L") '5
        colName.Add("MoldName:L")

        dt.Columns.Add("M") '5
        colName.Add("Process:M")

        dt.Columns.Add("N") '5
        colName.Add("StdPcs:N")

        dt.Columns.Add("O") '5
        colName.Add("UseHours:O")

        'set format
        ControlForm.GridviewColWithLinkFirst(gvShow, colName)


        'SelectDate = Year(Now()) & Month(Now()).ToString.PadLeft(2, "0") & Day(Now()).ToString.PadLeft(2, "0")
        Dim strSql As String = "select * from SetMoldOrder A left join SetMoldDetail B on B.DocNo=A.DocNo where A.DocDate='" & Date1.Text & "' order by DocTime"
        Dim dt1 As DataTable = dbConn.Query(strSql, VarIni.DBMIS, dbConn.WhoCalledMe)


        For L_count As Integer = 0 To dt1.Rows().Count - 1
            'gvShow.Rows
            Dim row = dt.NewRow
            Dim colSeq As Integer = 0

            row(colSeq) = Trim(dt1.Rows(L_count).Item("DocDate"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("DocTime"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("DocNo"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("Seq"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("MONo"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("MOSeq"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("MacNo"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("Qty"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("ItemNo"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("ItemSpec"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("MatCode"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("MoldName"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("Process"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("StdPcs"))

            colSeq += 1
            row(colSeq) = Trim(dt1.Rows(L_count).Item("UseHours"))
            dt.Rows.Add(row)

            'SerialNo += 1
        Next




        gvShow.DataSource = dt
        gvShow.DataBind()

        'For i = 0 To gvShow.Rows.Count - 1
        '    Dim selectdata As New Button
        '    selectdata.ID = "alex" & i
        '    selectdata.Text = "select"
        '    'selectdata.Attributes.Add("onclick", "javascript:CloseWindows()") '.Add(("onclick", "javascript:CloseWindows()")
        '    selectdata.CommandArgument = i 'gvShow.Rows(i).Cells(1).Text
        '    gvShow.Rows(i).Cells(8).Controls.Add(selectdata)
        'Next
        gvShow.Visible = True
    End Sub

    Protected Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        ShowGridview()
    End Sub
End Class