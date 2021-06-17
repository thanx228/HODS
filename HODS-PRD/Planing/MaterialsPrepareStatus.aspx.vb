Public Class MaterialsPrepareStatus
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    'Dim wcList As String = "'W04','W05','W09','W12','W13','W14','W15','W16'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD where MD001 in ('W04','W05','W09','W12','W13','W14','W15','W16')  order by MD001 " '
            ControlForm.showDDL(ddlWc, SQL, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString)

           SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showCheckboxList(clWorkType, SQL, "MQ002", "MQ001", 4, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = "",
            codeType As String = ddlCodeType.Text,
            condition As String = ddlCondition.Text.Trim

        Dim txtCodeType As String = "'3','4'"
        If codeType <> "0" Then
            txtCodeType = "'" & codeType & "'"
        End If
        WHR = WHR & " and SUBSTRING(MOCTB.TB003,3,1) in (" & txtCodeType & ") " 'code Type
        WHR = WHR & Conn_SQL.Where("SFCTA.TA001", clWorkType) 'Work Type
        WHR = WHR & Conn_SQL.Where("SFCTA.TA002", tbWorkNo) 'Work No
        WHR = WHR & Conn_SQL.Where("MOCTB.TB003", tbItem) 'Item
        WHR = WHR & Conn_SQL.Where("MOCTB.TB012", "MOCTB.TB013", tbSpec) 'Spec
        WHR = WHR & Conn_SQL.Where("SFCTA.TA006", ddlWc) 'Work Center
        WHR = WHR & Conn_SQL.Where("SFCTA.TA008", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
        If condition <> "0" Then 'check condition
            Select Case condition
                Case "1" ' not issue =0
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB005", "=0")
                Case "2" ' issue < required,issue>0
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB004", "> MOCTB.TB005")
                    WHR = WHR & Conn_SQL.Where("MOCTB.TB005", ">0")
            End Select
        End If

        SQL = "select SFCTA.TA006 as 'WC',SFCTA.TA001 as 'MO Type',SFCTA.TA002 as 'MO No.',CMSMW.MW002 as 'Process'," & _
            " (SUBSTRING(SFCTA.TA008,7,2)+'/'+SUBSTRING(SFCTA.TA008,5,2)+'/'+SUBSTRING(SFCTA.TA008,1,4)) as 'Plan Start Date'," & _
            " MOCTA.TA035 as 'JP Spec.',MOCTB.TB003 as 'RM Item',MOCTB.TB012+' '+MOCTB.TB013 as 'RM Spec'," & _
            " cast(MOCTB.TB004 as decimal(20,4)) as 'Req Qty',cast(MOCTB.TB005 as decimal(20,4)) as 'Issue Qty'," & _
            " cast(MOCTB.TB004-MOCTB.TB005 as decimal(20,4)) as 'Bal Qty',MOCTB.TB007 as 'Unit' " & _
            " from SFCTA left join MOCTB on MOCTB.TB001=SFCTA.TA001 and MOCTB.TB002=SFCTA.TA002 " & _
            " left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " & _
            " left join CMSMW on CMSMW.MW001=SFCTA.TA004  " & _
            " where MOCTA.TA013='Y' and MOCTA.TA011 not in ('Y','y') and SFCTA.TA032='N' " & _
            " and SFCTA.TA005 = 1 " & WHR & _
            " order by SFCTA.TA006,SFCTA.TA008,SFCTA.TA001,SFCTA.TA002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub btExport_Click(sender As Object, e As System.EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("MaterialsPrepareStatus" & Session("UserName"), gvShow)
    End Sub
End Class