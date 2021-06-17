Public Class inventoryMovementPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim code As String = Request.QueryString("MatPart").Trim
            lbCode.Text = code
            lbDesc.Text = Request.QueryString("MatDesc").Trim
            lbSpec.Text = Request.QueryString("MatSpec").Trim
            Dim SQL As String, whr As String = ""
            SQL = " select BOMCA.CA001 as 'BOM Type', BOMCA.CA002 as 'BOM No', BOMCA.CA003 as 'Item',INVMB.MB002 as 'Desc',INVMB.MB003 as 'Spec', " &
                  " cast(BOMCB.CB008 as decimal(16,3)) as 'QPA'  " &
                  " from BOMCA  left join BOMCB on BOMCB.CB001 = BOMCA.CA003 " &
                  " left join HOOTHAI_REPORT..tempInventoryMove T  on T.item = BOMCB.CB005 " &
                  " left join INVMB on INVMB.MB001 = BOMCA.CA003 " &
                  " where BOMCB.CB005 ='" & code & "' "
            ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
            CountRow1.RowCount = ControlForm.rowGridview(gvShow)

        End If
    End Sub
End Class