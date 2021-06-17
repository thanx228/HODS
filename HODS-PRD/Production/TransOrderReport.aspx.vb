Public Class TransOrderReport
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "Select ME001,rtrim(ME002) from ADMME order by ME001"
            ControlForm.showDDL(ddlGroup, SQL, "ME001", "ME001", True, Conn_SQL.ERP_ConnectionString)

            SQL = "select MC001,MC001+' : '+MC002 as MC002 from CMSMC  order by MC002"
            ControlForm.showDDL(ddlWH, SQL, "MC002", "MC001", True, Conn_SQL.ERP_ConnectionString)

            ucTranType.setObject = "D2,D3"
            ucMoType.setObjectWithAll = "51,52"
            ucWC.setObject = ""
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)

        End If

    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        Dim SQL As String = "",
            WHR As String = "",
            data1 As String = "",
            data2 As String = "",
            Type As String = ""

        WHR &= Conn_SQL.Where("TB015", DateFrom.dateVal, DateTo.dateVal)
        WHR &= Conn_SQL.Where("TB001", ucTranType.getObject)
        WHR &= Conn_SQL.Where("TC004", ucMoType.getObject)
        WHR &= Conn_SQL.Where("SFCTB.USR_GROUP", ddlGroup)
        WHR &= Conn_SQL.Where("TB002", tbTransNo)
        WHR &= Conn_SQL.Where("TC005", tbMONo)
        WHR &= Conn_SQL.Where("TC047", tbItem)
        WHR &= Conn_SQL.Where("TC049", tbSpec)
        WHR &= Conn_SQL.Where("SFCTB.CREATOR", tbInputer)
        WHR &= Conn_SQL.Where("TB008", ddlWH)
        WHR &= Conn_SQL.Where(ddWCType.Text.Trim, ucWC.getObject)

        Dim fld As String = "",
            colName As New ArrayList

        Dim fldName As ArrayList
        Dim al As ArrayList

        Select Case ddlShow.SelectedIndex

            Case "0" 'detail
                'doc date
                fld &= "TB015,"
                colName.Add("Doc. Date:TB015")

                'Trans. Type-No.
                fld &= "Rtrim(TB001)+'-'+TB002+'-'+TC003 TB00123,"
                colName.Add("Trans. Type-No.:TB00123")

                'Desc
                fld &= "CMSMQ.MQ002,"
                colName.Add("Desc.:MQ002")

                'MO Type-No
                fld &= "TC004+'-'+TC005+'-'+TC006 TC00456,"
                colName.Add("MO Type-No:TC00456")

                'Spec
                fld &= "TC049,"
                colName.Add("Spec:TC049")

                'Qty
                fld &= "TC036,"
                colName.Add("Qty:TC036:2")

                'Accepted Qty
                fld &= "TC014,"
                colName.Add("Accepted Qty:TC014:2")

                'Scrap Qty
                fld &= "TC016,"
                colName.Add("Scrap Qty:TC016:2")

                'Unit
                fld &= "TC010,"
                colName.Add("Unit:TC010")

                'plan due date
                fld &= "TC024,"
                colName.Add("Plan Due Date:TC024")

                'Issue Type
                fld &= "case when TB004='1' then '1:Work Center' else '2:Subcontractor Supllier' end TB004,"
                colName.Add("Issue Type:TB004")

                'Issue Place
                fld &= "Rtrim(TB005)+'-'+TB006 TB0056,"
                colName.Add("Issue Place:TB0056")

                'Receipt Place / WH USE
                fld &= "Rtrim(TB008)+'-'+TB009 TB0089,"
                colName.Add("Receipt Place / WH USE:TB0089")

                'Bin
                fld &= "TC056,"
                colName.Add("Bin:TC056")

                'Input Date-Time
                fld &= "SUBSTRING(SFCTB.CREATE_DATE ,7,2)+'-'+SUBSTRING(SFCTB.CREATE_DATE,5,2)+'-'+SUBSTRING(SFCTB.CREATE_DATE,1,4)+' ' +SUBSTRING(SFCTB.CREATE_DATE ,9,2)+':'+SUBSTRING(SFCTB.CREATE_DATE,11,2) CREATE_DATE,"
                colName.Add("Input Date-TimeCREATE_DATE:CREATE_DATE")

                'Inputer
                fld &= "SFCTB.CREATOR+'-'+(select MF002 from ADMMF where SFCTB.CREATOR =MF001 ) CREATOR,"
                colName.Add("Inputer:CREATOR")

                'Group No.
                fld &= "SFCTB.USR_GROUP,"
                colName.Add("Group No.:USR_GROUP")

                'Group No.
                fld &= "SUBSTRING(TB003,7,2)+'-'+SUBSTRING(TB003,5,2)+'-'+SUBSTRING(TB003,1,4) TB003,"
                colName.Add("Trans. Date:TB003")

                'Approver-Name
                fld &= "Rtrim(TB016+'-'+MF002) TB016,"
                colName.Add("Approver-Name:TB016")

                'Appr. Status
                fld &= "case TB013 when 'Y' then 'Y:Approved' when 'N' then 'N:NotApproved'  when 'V' then 'V:Cancel' when 'U' then 'V:Approve Failed' end TB013 ,"
                colName.Add("Appr. Status:TB013")

                'Item
                fld &= "TC047,"
                colName.Add("Item:TC047")

                'Issue Op.
                fld &= "TC007+'-'+MW002 TC007,"
                colName.Add("Issue Op.:TC007")

                'Receipt Op.
                fld &= "TC009+'-'+(select MW002 from CMSMW where MW001 =TC009 ) TC009,"
                colName.Add("Receipt Op.:TC009")

                'Remark (Header)
                fld &= "TB014,"
                colName.Add("Remark (Header):TB014")

                'Plan due date
                fld &= "TC024,"
                colName.Add("Plan Due Date:TC024")

                'Issue Op.
                fld &= "TC031,"
                colName.Add("Remark (Body):TC031")

                SQL = " select  " & fld.Substring(0, fld.Length - 1) & " from SFCTB " &
                      " left join SFCTC on TC001 = TB001 and TC002=TB002 " &
                      " left join CMSMQ on MQ001 = TB001 " &
                      " left join CMSMW on MW001 = TC007 " &
                      " left join ADMMF on MF001 = TB016 " &
                      " left join MOCTA on TA001 = TC004 and TA002 = TC005 " &
                      " where SUBSTRING(TB001,1,2) in ('D2','D3') " & WHR &
                      " order by  TB015,TB001,TB002,TC003 "

            Case "1" 'sum

                'Date From
                fld &= "min(TB015) TB015_min,"
                colName.Add("Date From:TB015_min")

                'Date From
                fld &= "max(TB015) TB015_max,"
                colName.Add("Date To:TB015_max")

                'Remark (Header)
                fld &= "Rtrim(TB005)+'-'+TB006 TB0056,"
                colName.Add("Issue Place:TB0056")

                'Issue Op.
                fld &= "COUNT (TB005) TB005,"
                colName.Add("Summary Time:TB005:0")

                SQL = " select" & fld.Substring(0, fld.Length - 1) & " from SFCTB " &
                      " left join SFCTC on TC001 = TB001 and TC002=TB002 " &
                      " left join CMSMQ on MQ001 = TB001 " &
                      " left join CMSMW on MW001 = TC007 " &
                      " left join ADMMF on MF001 = TB016 " &
                      " left join MOCTA on TA001 = TC004 and TA002 = TC005 " &
                      "where SUBSTRING(TB001,1,2) in ('D2','D3') " & WHR &
                      " group by TB005 , TB006  order by TB005 "
        End Select
        ControlForm.GridviewInitial(gvShow, colName)
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub

    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("Report" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End With
    End Sub
End Class