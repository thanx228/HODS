Imports System.Globalization
Public Class ItemFollowLot
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Dim Table As String = "ItemFollowLot"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            CreateTable.CreateItemFollowLotTable()
            btClose.Visible = False
        End If
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        Dim item As String = tbItem.Text.Trim,
            dateS As String = configDate.dateFormat2(tbDate.Text.Trim),
            cLot As String = tbLot.Text.Trim


        If item = "" Then
            show_message.ShowMessage(Page, "Item is Null", UpdatePanel1)
            tbItem.Focus()
            Exit Sub
        End If
        Dim SQL As String = "select * from INVMB where MB001='" & item & "' "
        Dim dt As DataTable
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        If dt.Rows.Count = 0 Then
            show_message.ShowMessage(Page, "Item is not found", UpdatePanel1)
            tbItem.Focus()
            Exit Sub
        End If
        If dateS = "" Then
            show_message.ShowMessage(Page, "Date Start is Null", UpdatePanel1)
            tbDate.Focus()
            Exit Sub
        End If
        If cLot = "" Or CInt(cLot) <= 0 Then
            show_message.ShowMessage(Page, "Lot is Null or zero", UpdatePanel1)
            tbLot.Focus()
            Exit Sub
        End If
        Dim fldHash As Hashtable = New Hashtable,
            whrHash As Hashtable = New Hashtable,
            typeSql As String = "I",
            dateToday As String = DateTime.Now.ToString("yyyyMMdd HH:mm:ss", New CultureInfo("en-US")),
            user As String = Session("UserName")

        If lbID.Text = "" Then 'insert
            fldHash.Add("item", item)
            fldHash.Add("dateStart", dateS)
            fldHash.Add("LotCheck", cLot)
            fldHash.Add("CreateBy", user)
            fldHash.Add("CreateDate", dateToday)
        Else 'update
            typeSql = "U"
            whrHash.Add("Id", lbID.Text.Trim)
            fldHash.Add("item", "'" & item & "'")
            fldHash.Add("dateStart", "'" & dateS & "'")
            fldHash.Add("LotCheck", "'" & cLot & "'")
            fldHash.Add("ChangeBy", "'" & user & "'")
            fldHash.Add("ChangeDate", "'" & dateToday & "'")
        End If
        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL("ItemFollowLot", fldHash, whrHash, typeSql), Conn_SQL.MIS_ConnectionString)
        show_message.ShowMessage(Page, "Add Item Completed!!", UpdatePanel1)
        clearData()
    End Sub

    Protected Sub btClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btClear.Click
        clearData()
    End Sub
    Sub clearData()
        lbID.Text = ""
        tbItem.Text = ""
        tbDate.Text = ""
        tbLot.Text = ""
        btSave.Visible = True
        btClose.Visible = False
        SqlDataSource1.SelectCommand = "SELECT [Id], [item], [dateStart], [LotCheck], [status], [CreateBy] FROM [ItemFollowLot] where [Id]<>''  ORDER BY [Id] DESC"
        gvShow.DataBind()
    End Sub

    Private Sub gvShow_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShow.RowCommand
        Dim i As Integer = e.CommandArgument
        Dim docNo As String = gvShow.Rows(i).Cells(0).Text.Replace(" ", "")
        If e.CommandName = "onEdit" Then
            Dim SQL As String = "select * from " & Table & " where Id='" & docNo & "'"
            Dim Program As New DataTable
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            With Program
                If .Rows.Count > 0 Then
                    With .Rows(0)
                        tbItem.Text = .Item("item").ToString.Trim
                        tbDate.Text = configDate.dateShow2(.Item("dateStart").ToString.Trim, "/")
                        tbLot.Text = .Item("LotCheck")
                        lbID.Text = .Item("Id").ToString
                        Dim cSave As Boolean = True,
                            cClose As Boolean = True
                        If .Item("status").ToString = "20" Then
                            cSave = False
                            cClose = False
                        End If
                        btSave.Visible = cSave
                        btClose.Visible = cClose
                    End With
                End If
            End With
        End If
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        Dim WHR As String = ""
        WHR = Conn_SQL.Where("[item]", tbItem) 'item
        WHR = WHR & Conn_SQL.Where("[dateStart]", configDate.dateFormat2(tbDate.Text), True, False)
        WHR = WHR & Conn_SQL.Where("[LotCheck]", tbLot, False)
        SqlDataSource1.SelectCommand = "SELECT [Id], [item], [dateStart], [LotCheck], [status], [CreateBy] FROM [ItemFollowLot] where [Id]<>'' " & WHR & " ORDER BY [Id] DESC"
        gvShow.DataBind()
    End Sub

    Protected Sub btClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btClose.Click
        Dim dateToday As String = DateTime.Now.ToString("yyyyMMdd HH:mm:ss", New CultureInfo("en-US")),
            user As String = Session("UserName")
        Dim USQL As String = "update " & Table & " set status='20',ChangeBy='" & user & "',ChangeDate='" & dateToday & "' where Id='" & lbID.Text & "'"
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        show_message.ShowMessage(Page, "update status completes", UpdatePanel1)
        clearData()
    End Sub
End Class