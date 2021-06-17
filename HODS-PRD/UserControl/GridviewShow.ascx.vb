Imports MIS_HTI.FormControl
Imports MIS_HTI.DataControl

Public Class GridviewShow
    Inherits System.Web.UI.UserControl

    Dim gvCont As New GridviewControl()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            gvCont = New GridviewControl(gvShow)
        End If
    End Sub

    Public WriteOnly Property SetRowName() As String
        Set(value As String)
            ucRowCount.SetName = value
        End Set
    End Property

    Public ReadOnly Property RowValue() As Integer
        Get
            Return ucRowCount.value
        End Get
    End Property

    Public ReadOnly Property getGridview() As GridView
        Get
            Return gvShow
        End Get
    End Property

    Public ReadOnly Property gridviewCountRow() As Integer
        Get
            Return gvShow.Rows.Count
        End Get
    End Property

    Sub setRowCount()
        ucRowCount.RowCount = gvCont.rowGridview(gvShow)
    End Sub

    Sub showGridview(dt As DataTable)
        gvCont.ShowGridView(gvShow, dt)
        setRowCount()
    End Sub

    Sub showGridview(dt As DataTable, col() As String,
                    Optional gridviewClear As Boolean = True, Optional genRow As Boolean = False,
                    Optional showFooter As Boolean = False, Optional autoDatafield As Boolean = False,
                    Optional strSplit As String = ":")

        GridviewInitial(col, gridviewClear, genRow, showFooter, autoDatafield, strSplit)
        showGridview(dt)
    End Sub

    Sub showGridview(dt As DataTable, col As ArrayList,
                    Optional gridviewClear As Boolean = True, Optional genRow As Boolean = False,
                    Optional showFooter As Boolean = False, Optional autoDatafield As Boolean = False,
                    Optional strSplit As String = ":")

        GridviewInitial(col, gridviewClear, genRow, showFooter, autoDatafield, strSplit)
        showGridview(dt)
    End Sub

    Sub showGridview(SQL As String, dbType As String)
        gvCont.ShowGridView(gvShow, SQL, dbType)
        setRowCount()
    End Sub

    Sub showGridview(SQL As String, dbType As String, col() As String,
                    Optional gridviewClear As Boolean = True, Optional genRow As Boolean = False,
                    Optional showFooter As Boolean = False, Optional autoDatafield As Boolean = False,
                    Optional strSplit As String = ":")
        GridviewInitial(col, gridviewClear, genRow, showFooter, autoDatafield, strSplit)
        showGridview(SQL, dbType)
    End Sub

    Sub showGridview(SQL As String, dbType As String, col As ArrayList,
                    Optional gridviewClear As Boolean = True, Optional genRow As Boolean = False,
                    Optional showFooter As Boolean = False, Optional autoDatafield As Boolean = False,
                    Optional strSplit As String = ":")
        GridviewInitial(col, gridviewClear, genRow, showFooter, autoDatafield, strSplit)
        showGridview(SQL, dbType)
    End Sub

    Sub GridviewInitial(col() As String,
                    Optional gridviewClear As Boolean = True, Optional genRow As Boolean = False,
                    Optional showFooter As Boolean = False, Optional autoDatafield As Boolean = False,
                    Optional strSplit As String = ":")
        gvCont.GridviewInitial(gvShow, col, gridviewClear, genRow, showFooter, autoDatafield, strSplit)
    End Sub

    Sub GridviewInitial(col As ArrayList,
                    Optional gridviewClear As Boolean = True, Optional genRow As Boolean = False,
                    Optional showFooter As Boolean = False, Optional autoDatafield As Boolean = False,
                    Optional strSplit As String = ":")
        gvCont.GridviewInitial(gvShow, col, gridviewClear, genRow, showFooter, autoDatafield, strSplit)
    End Sub

    Sub ShowGridviewHyperLink(dt As DataTable, col As ArrayList,
                              Optional isHyperlink As Boolean = False,
                              Optional textName As String = "Detail",
                              Optional strSplit As String = VarIni.char8)
        gvCont.GridviewColWithLinkFirst(gvShow, col, isHyperlink, textName, strSplit)
        showGridview(dt)
    End Sub

    Sub ShowGridviewHyperLink(SQL As String, dbType As String, col As ArrayList,
                              Optional isHyperlink As Boolean = False,
                              Optional textName As String = "Detail",
                              Optional strSplit As String = VarIni.char8)
        gvCont.GridviewColWithLinkFirst(gvShow, col, isHyperlink, textName, strSplit)
        showGridview(SQL, dbType)
    End Sub
    Public Event RowDataBound(sender As Object, e As GridViewRowEventArgs)

    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        RaiseEvent RowDataBound(sender, e)
    End Sub

    Sub ShowWithCommand(ColList As List(Of String),
                        CommandList As List(Of String),
                        Optional ByVal addFirst As Boolean = True,
                        Optional strSplit As String = VarIni.C8,
                        Optional clear As Boolean = True,
                        Optional autoGenRow As Boolean = False,
                        Optional showFooter As Boolean = False)
        gvCont.GridviewColWithCommand(ColList, CommandList, addFirst, strSplit, clear, autoGenRow, showFooter)
    End Sub
    Sub ShowWithCommand(dt As DataTable,
                        ColList As ArrayList,
                        CommandList As List(Of String),
                        Optional addFirst As Boolean = True,
                        Optional strSplit As String = VarIni.C8,
                        Optional clear As Boolean = True,
                        Optional autoGenRow As Boolean = False,
                        Optional showFooter As Boolean = False)
        gvCont = New GridviewControl(gvShow)
        gvCont.GridviewColWithCommand(ColList, CommandList, addFirst, strSplit, clear, autoGenRow, showFooter)
        showGridview(dt)
    End Sub

    Sub ShowWithCommand(SQL As String,
                        dbType As String,
                        ColList As ArrayList,
                        CommandList As List(Of String),
                        Optional addFirst As Boolean = True,
                        Optional strSplit As String = VarIni.C8,
                        Optional clear As Boolean = True,
                        Optional autoGenRow As Boolean = False,
                        Optional showFooter As Boolean = False)
        gvCont = New GridviewControl(gvShow)
        gvCont.GridviewColWithCommand(ColList, CommandList, addFirst, strSplit, clear, autoGenRow, showFooter)
        showGridview(SQL, dbType)
    End Sub

    Sub ShowWithCommand(SQL As String,
                        dbType As String,
                        ColList As List(Of String),
                        CommandList As List(Of String),
                        Optional addFirst As Boolean = True,
                        Optional strSplit As String = VarIni.C8,
                        Optional clear As Boolean = True,
                        Optional autoGenRow As Boolean = False,
                        Optional showFooter As Boolean = False)
        gvCont = New GridviewControl(gvShow)
        gvCont.GridviewColWithCommand(ColList, CommandList, addFirst, strSplit, clear, autoGenRow, showFooter)
        showGridview(SQL, dbType)
    End Sub

    Public Event RowCommand(sender As Object, e As GridViewCommandEventArgs)
    Private Sub gvShow_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShow.RowCommand
        RaiseEvent RowCommand(sender, e)
    End Sub
End Class