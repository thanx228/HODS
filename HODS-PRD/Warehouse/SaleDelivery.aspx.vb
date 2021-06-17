Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class SaleDelivery
    Inherits System.Web.UI.Page

    'Dim ControlForm As New ControlDataForm
    'Dim CreateTempTable As New CreateTempTable
    'Dim Conn_SQL As New ConnSQL
    'Dim configDate As New ConfigDate

    Dim dbconn As New DataConnectControl
    Dim ddlcont As New DropDownListControl
    Dim cblcont As New CheckBoxListControl
    Dim dateCont As New DateControl
    Dim gvcont As New GridviewControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 ='23' order by MQ002 "
            'ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)
            cblcont.showCheckboxList(cblSaleType, SQL, VarIni.ERP, "MQ002", "MQ001", 6)
            SQL = "select Code,Name from HOOTHAI_REPORT .dbo. CodeInfo where CodeType ='PLANT' order by Code "
            'ControlForm.showDDL(ddlPlant, SQL1, "Code", "Name", 6, Conn_SQL.ERP_ConnectionString)
            ddlcont.showDDL(ddlPlant, SQL, VarIni.DBMIS, "Code", "Name", True)
            'ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)

            btExport.Visible = False
        End If

    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim colName As ArrayList
        Dim fldName As ArrayList
        Dim al As New ArrayList
        With New ArrayListControl(al)
            .TAL("INVMB.MB003" & VarIni.C8 & "MB003", "Part No")
            .TAL("INVMB.UDF01" & VarIni.C8 & "UDF01", "Location")
            .TAL("INVMB.UDF51" & VarIni.C8 & "UDF51", "Pc/Box", "0")
            .TAL("INVMC.MC007" & VarIni.C8 & "MC007", "Stock Qty", "0")
            .TAL("ACRTB.TB022" & VarIni.C8 & "TB022", "Delivery Qty", "0")
            .TAL("case INVMB.UDF51 when 0 then 0 else floor(isnull(ACRTB.TB022,0) / INVMB.UDF51) end" & VarIni.C8 & "FULL_BOX", "จำนวนกล่องเต็ม", "0")
            .TAL("case INVMB.UDF51 when 0 then 0 else isnull(ACRTB.TB022,0) % INVMB.UDF51 end" & VarIni.C8 & "LAST_BOX", "จำนวนกล่องเศษ", "0")
            .TAL("case INVMB.UDF51 when 0 then 0 else round(floor(isnull(ACRTB.TB022,0) / INVMB.UDF51),0)+case isnull(ACRTB.TB022,0) % INVMB.UDF51 when 0 then 0 else 1 end end" & VarIni.C8 & "ALL_BOX", "จำนวนกล่องรวม", "0")
            .TAL("case when COPTG.UDF02 = '' then '' else COPTG.UDF02 end" & VarIni.C8 & "TGUDF02", "Plant")
            .TAL("COPTH.UDF02" & VarIni.C8 & "UDF02", "Cust Line")
            .TAL("ACRTB.TB048" & VarIni.C8 & "TB048", "Cust PO")
            .TAL("case when COPTG.UDF01='' then substring (COPTG.TG003,1,4)+'-'+substring (COPTG.TG003,5,2)+'-'+substring (COPTG.TG003,7,2) else COPTG.UDF01 end" & VarIni.C8 & "SHIP_DATE", "Ship Date")
            .TAL("case when COPTG.UDF03='' then '' else COPTG.UDF03 end" & VarIni.C8 & "SHIP_TIME", "Ship Time")
            .TAL("COPTG.TG001" & VarIni.C8 & "TG001", "Sale Del Type")
            .TAL("COPTG.TG002" & VarIni.C8 & "TG002", "Sale Del No")
            .TAL("COPTG.TG020" & VarIni.C8 & "TG020", "Remark")

            fldName = .ChangeFormat()
            colName = .ChangeFormat(True)
        End With

        Dim mainTable As String = "(select TA038,TB005,TB006,TB007,ACRTB.TB048,sum(TB022) TB022 from ACRTB left join ACRTA on TA001=TB001 and TA002=TB002 group by TA038,TB005,TB006,TB007,ACRTB.TB048) ACRTB"

        Dim strSQL As New SQLString(mainTable, fldName)
        strSQL.setLeftjoin(" left join COPTH on COPTH.TH001=ACRTB.TB005 and COPTH.TH002=ACRTB.TB006 and COPTH.TH003=ACRTB.TB007 ")
        strSQL.setLeftjoin(" left join (select MB001,MB003,isnull(INVMB.UDF01,'') UDF01,isnull(INVMB.UDF51,0) UDF51 from INVMB) INVMB on INVMB.MB001 = COPTH.TH004 ")
        strSQL.setLeftjoin(" left join (select MC001,sum(MC007) MC007 from INVMC where MC002 in ('2101','2104') group by MC001 having sum(MC007)>0) INVMC on INVMC .MC001 = INVMB .MB001 ")
        strSQL.setLeftjoin(" left join COPTG on COPTH.TH001 = COPTG.TG001  And COPTH.TH002 = COPTG.TG002 ")
        strSQL.setLeftjoin(" left join COPMA on COPMA.MA001 = COPTG.TG004 ")
        Dim WHR As String = ""
        WHR &= dbconn.WHERE_IN("COPTH.TH001", cblSaleType,, True)
        WHR &= dbconn.WHERE_LIKE("COPTH.TH002", tbNo)
        WHR &= dbconn.WHERE_EQUAL("COPTG.TG003", UcDate.Text)
        WHR &= dbconn.WHERE_LIKE("COPTG.UDF03", tbShTime)
        WHR &= dbconn.WHERE_IN("COPTG.UDF02", ddlPlant,, True)

        strSQL.SetWhere(WHR, True)
        strSQL.SetOrderBy("COPTG.TG001,COPTG.TG002,COPTH.TH003 Asc")

        gvcont.GridviewColWithLinkFirst(gvShow, colName, strSplit:=VarIni.C8)
        gvcont.ShowGridView(gvShow, strSQL.GetSQLString, VarIni.ERP)
        ucCntRow.RowCount = gvcont.rowGridview(gvShow)

        System.Threading.Thread.Sleep(1000)
        btExport.Visible = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("SaleDelivery" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub


End Class