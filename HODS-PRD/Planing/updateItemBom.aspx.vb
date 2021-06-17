Public Class updateItemBom
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        Dim USQL As String = "",
            SQL As String = ""
        USQL = " update INVMB set MB010=MB001,MB011='01',MB068='W01' where MB010='' "
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)

        SQL = "select MC001 from BOMMC where MC005='' "
        Dim rs As New DataTable
        rs = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To rs.Rows.Count - 1
            USQL = "update INVMB set MB068='W01' where MB001='" & rs.Rows(i).Item("MC001").ToString.Trim & "'"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
        Next

        USQL = " update BOMMC set MC005='5102' where MC005='' "
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
        'USQL = " update BOMMD set MD008='0.07' where MD008='0' and SUBSTRING(MD003,3,1) ='3' and MD003 not in(select MB001 from INVMB where SUBSTRING(MB001,3,1) ='3'and  MB003 like '%OS2%') "
        'Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
        USQL = " update INVMB set MB040='1',MB041='1' where MB040='0' and MB041='0' and SUBSTRING(MB001,3,1)='3' "
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)

        lbShow.Text = "Update Complete"
    End Sub
End Class