Public Class DeptC
    Inherits System.Web.UI.UserControl
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public WriteOnly Property setObjectWithSession() As String
        Set(ByVal code As String)
            Dim Sql As String = "select Dept from UserOTRecord where Id='" & Session("UserId") & "' "
            Dim Dept As String = Conn_SQL.Get_value(Sql, Conn_SQL.MIS_ConnectionString).Replace(",", "','")

            Sql = "select rtrim(ME001) ME001,ME001+':'+ME002 ME002 from CMSME where ME001 in ('" & Dept & "') order by ME001 "
            ControlForm.showCheckboxList(cbl, Sql, "ME002", "ME001", 4, Conn_SQL.ERP_ConnectionString)

        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(ByVal listNotDept As String)
            Dim SQL As String = "select rtrim(ME001) ME001,rtrim(ME001)+':'+rtrim(ME002) ME002 from CMSME where len(rtrim(ME001))=3 and ME001 not in ('" & listNotDept.Replace(",", "','") & "') order by ME001 "
            ControlForm.showCheckboxList(cbl, SQL, "ME002", "ME001", 4, Conn_SQL.ERP_ConnectionString)
        End Set
    End Property

    Public WriteOnly Property setObject_1() As String
        Set(ByVal listNotDept As String)
            Dim SQL As String = "select rtrim(ME001) ME001,rtrim(ME001)+':'+rtrim(ME002) ME002 from CMSME where len(rtrim(ME001))=3 and ME001 not in ('M02','M03','" & listNotDept.Replace(",", "','") & "') order by ME001 "
            ControlForm.showCheckboxList(cbl, SQL, "ME002", "ME001", 4, Conn_SQL.ERP_ConnectionString)
        End Set
    End Property
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbl
        End Get
    End Property

    Public ReadOnly Property getChecked() As Integer
        Get
            Dim cnt As Integer = 0
            For Each boxItem As ListItem In cbl.Items
                If boxItem.Selected Then
                    cnt += 1
                End If
            Next
            Return cnt
        End Get
    End Property

    Public ReadOnly Property getValue() As String
        Get
            Dim val As String = ""
            For Each boxItem As ListItem In cbl.Items
                Dim boxVal As String = CStr(boxItem.Value.Trim)
                If boxItem.Selected Then
                    val &= boxVal & ","
                End If
            Next
            Return val.Substring(0, val.Count - 1)
        End Get
    End Property

End Class