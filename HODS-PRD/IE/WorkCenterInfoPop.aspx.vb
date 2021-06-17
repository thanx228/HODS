Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class WorkCenterInfoPop
    Inherits System.Web.UI.Page

    Dim cblCont As New CheckBoxListControl
    Dim ddlCont As New DropDownListControl
    'Dim whrCont As New WhereControl
    Dim dbConn As New DataConnectControl
    Dim gvCont As New GridviewControl
    Dim outCont As New OutputControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lbWc.Text = Request.QueryString("wc").ToString.Trim
            lbWcName.Text = Request.QueryString("wcname").ToString.Trim

            lbMach.Text = Request.QueryString("mach").ToString.Trim
            lbMachName.Text = Request.QueryString("machname").ToString.Trim
            'Dim showView As String = Request.QueryString("view").ToString.Trim
            'operation()

            'show work center
            Dim CMSMD As New CMSMD
            Dim whr As String = ""
            Dim dt As DataTable = CMSMD.getData("", "rtrim(MD001) MD001,rtrim(MD001)+'-'+MD002 MD002")
            ddlCont.showDDL(ddlWC, dt, "MD002", "MD001")
            ddlCont.setValue(ddlWC, lbWc.Text)
            'show work center
            Dim CreateTable As New CreateTable
            CreateTable.CreateProcessMach()

            ddlWC_SelectedIndexChanged(sender, e)
            btShow_Click(sender, e)

        End If
    End Sub

    'Sub cap()
    '    mvPop.SetActiveView(viewCap)
    '    'show gridview
    '    Dim fldName As New ArrayList
    '    fldName.Add(MACHINE_CAP.SHIFT)
    '    fldName.Add(MACHINE_CAP.HOLIDAY)
    '    fldName.Add(MACHINE_CAP.QTY)
    '    fldName.Add(MACHINE_CAP.NORMAL_TIME & "/60:" & MACHINE_CAP.NORMAL_TIME)
    '    fldName.Add(MACHINE_CAP.OVER_TIME & "/60:" & MACHINE_CAP.OVER_TIME)
    '    fldName.Add(MACHINE_CAP.PRODUCTIVITY & "*100:" & MACHINE_CAP.PRODUCTIVITY)
    '    fldName.Add(MACHINE_CAP.STATUS)

    '    Dim strSQL As New SQLString(MACHINE_CAP.TABLENAME, fldName, False)
    '    strSQL.SetWhere(dbConn.Where(MACHINE_CAP.WC_CODE, lbWc.Text.Trim,, False), True)
    '    strSQL.SetWhere(dbConn.Where(MACHINE_CAP.MACH_CODE, lbMach.Text.Trim,, False))
    '    strSQL.SetOrderBy(MACHINE_CAP.SHIFT)
    '    gvCont.ShowGridView(gvShowCap, strSQL.GetSQLString, VarIni.JODS)
    '    ucCountRowCap.RowCount = gvCont.rowGridview(gvShowCap)

    'End Sub
    'Sub operation()
    '    'mvPop.SetActiveView(viewOperation)
    '    'Dim cblCont As New CheckBoxListControl
    '    Dim fldName As New ArrayList
    '    fldName.Add(ECAA.WorkcenterID)
    '    fldName.Add(ECAA.WorkcenterID & "||'-'||" & ECAA.Workcenter & ":" & ECAA.Workcenter)
    '    ddlCont.showDDL(ddlWc, ECAA.GetSQL(fldName, "", ECAA.WorkcenterID), VarIni.T100, ECAA.Workcenter, ECAA.WorkcenterID)
    '    ddlCont.setValue(ddlWc, lbWc.Text)

    '    'show data to gridview
    '    fldName = New ArrayList
    '    fldName.Add(MACHINE_OPERATION.OPERATION_CODE)
    '    fldName.Add(OOCQL.Operation)

    '    Dim colName As New ArrayList
    '    colName.Add("Operation Code:" & MACHINE_OPERATION.OPERATION_CODE)
    '    colName.Add("Operation Name:" & OOCQL.Operation)

    '    Dim strSQL As New SQLString(MACHINE_OPERATION.TABLENAME, fldName, False)
    '    Dim leftList As New ArrayList
    '    leftList.Add(OOCQL.ent & ":" & VarIni.EntV & ":")
    '    leftList.Add(OOCQL.Language & ":" & VarIni.enUS_V & ":")
    '    leftList.Add(OOCQL.IssueSite & ":" & OOCQ.OperationData & ":")
    '    leftList.Add(OOCQL.OperationID & ":" & MACHINE_OPERATION.OPERATION_CODE)
    '    strSQL.setLeftjoin(strSQL.getLeftjoinManual(OOCQL.TableName, leftList))
    '    Dim whr As String = strSQL.Where(MACHINE_OPERATION.WC_CODE, lbWc.Text.Trim,, False)
    '    whr &= strSQL.Where(MACHINE_OPERATION.MACH_CODE, lbMach.Text.Trim,, False)
    '    strSQL.SetWhere(whr, True)
    '    strSQL.SetOrderBy(MACHINE_OPERATION.OPERATION_CODE)

    '    Dim SQL As String = strSQL.GetSQLString

    '    gvCont.GridviewInitial(gvShowOperation, colName)
    '    gvCont.ShowGridView(gvShowOperation, SQL, VarIni.T100)
    '    ucCountRowOperation.RowCount = gvCont.rowGridview(gvShowOperation)

    'End Sub

    'Protected Sub ddlWc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWc.SelectedIndexChanged
    '    If ddlWc.Text = "0" Then
    '        cblOper.Items.Clear()
    '    Else
    '        Dim fldName As New ArrayList
    '        'fldName.Add("rtrim(MW001) MW001")
    '        'fldName.Add(OOCQL.OperationID & "||'-'||" & OOCQL.Operation & ":" & OOCQL.Operation)
    '        'Dim dt As DataTable = OOCQ.getDataProcess(fldName, whrCont.Where(OOCQ.Workstation, ddlWc), False)


    '    End If
    'End Sub

    Protected Sub btSaveOperation_Click(sender As Object, e As EventArgs) Handles btSaveOperation.Click

        Dim sqlList As New ArrayList
        Dim getVal As String = ""
        For Each boxItem As ListItem In cblOper.Items
            If boxItem.Selected Then

                Dim whrHash As New Hashtable From {
                    {"WorkCenter", lbWc.Text},
                    {"MachLine", lbMach.Text},
                    {"ProcessCode", boxItem.Value.Trim}
                }
                Dim fldHash As New Hashtable From {
                    {"CreateBy", Session(VarIni.UserId)}
                }
                sqlList.Add(dbConn.getInsertSql("ProcessMach", fldHash, whrHash))
            End If
            getVal &= boxItem.Value.Trim & ","
        Next
        'delete before save
        Dim SQL As String = "delete from ProcessMach" & VarIni.W & " 1=1 "
        SQL &= dbConn.WHERE_EQUAL("WorkCenter", lbWc.Text)
        SQL &= dbConn.WHERE_EQUAL("MachLine", lbMach.Text)
        SQL &= dbConn.WHERE_IN("ProcessCode", getVal.Substring(0, getVal.Length - 1), addSigleQuote:=True)
        dbConn.TransactionSQL(SQL, VarIni.DBMIS, dbConn.WhoCalledMe) 'delete
        'delete before save
        dbConn.TransactionSQL(sqlList, VarIni.DBMIS, dbConn.WhoCalledMe) 'insert
        show_message.ShowMessage(Page, "Insert Completed", UpdatePanel1)
        btShow_Click(sender, e)
    End Sub

    Protected Sub ddlWC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWC.SelectedIndexChanged
        Dim SQL As String = "SELECT rtrim(MW001) MW001,rtrim(MW001)+':'+rtrim(MW002)+':'+rtrim(MW004) MW002 FROM CMSMW WHERE MW004 = '1' AND MW005='" & ddlWC.Text.Trim & "' ORDER BY MW001"
        cblCont.showCheckboxList(cblOper, SQL, VarIni.ERP, "MW002", "MW001", 4)
        'select form machine_operation
        SQL = "SELECT ProcessCode FROM ProcessMach LEFT JOIN HOOTHAI..CMSMW ON MW001=ProcessCode WHERE WorkCenter='" & lbWc.Text.Trim & "' AND MachLine='" & lbMach.Text.Trim & "' AND MW005='" & ddlWC.Text.Trim & "' ORDER BY ProcessCode"
        cblCont.setValue(cblOper, SQL, VarIni.DBMIS, "ProcessCode")

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "SELECT ProcessCode,MW002 'Process Name' FROM ProcessMach LEFT JOIN HOOTHAI..CMSMW ON MW001=ProcessCode WHERE WorkCenter='" & lbWc.Text.Trim & "' AND MachLine='" & lbMach.Text.Trim & "' ORDER BY ProcessCode"
        gvCont.ShowGridView(gvShow, SQL, VarIni.DBMIS)
        ddlWC_SelectedIndexChanged(sender, e)
    End Sub
End Class