Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class WorkCenterInfo
    Inherits System.Web.UI.Page

    ReadOnly dbConn As New DataConnectControl
    ReadOnly gvCont As New GridviewControl
    ReadOnly dtCont As New DataTableControl
    ReadOnly ddlCont As New DropDownListControl
    ReadOnly cblCont As New CheckBoxListControl
    ReadOnly outCont As New OutputControl
    Dim CreateTable As New CreateTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session(VarIni.UserName) <> "" Then
                CreateTable.CreateLogWorkcenterInfo()
                Dim SQL As String = "SELECT MD001,MD001+' '+MD002 MD002 FROM CMSMD ORDER BY MD001"
                ddlCont.showDDL(ddlWC, SQL, VarIni.ERP, "MD002", "MD001")
                ddlWC_SelectedIndexChanged(sender, e)
                'btShow_Click(sender, e)
            End If
        End If
    End Sub

    Protected Sub ddlWC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWC.SelectedIndexChanged
        Dim SQL As String = "SELECT MX001,MX001+' '+MX003 MX003 FROM CMSMX WHERE 1=1 "
        SQL &= dbConn.WHERE_EQUAL("MX002", ddlWC)
        SQL &= " ORDER BY MX001 "
        'cblCont.(ddlMachLine, SQL, VarIni.ERP, "MX003", "MX001", True)
        cblCont.showCheckboxList(cblMachLine, SQL, VarIni.ERP, "MX003", "MX001", 5)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim whr As String = ""
        Dim dt As DataTable = GetDatatable()
        gvCont.ShowGridView(gvShow, dt)
        CountRow1.RowCount = gvCont.rowGridview(gvShow)
        'set value to gridview
        Dim dtProcessType As DataTable = CODEINFO.GetDatatable("WC_PROCESS_TYPE")
        Dim dtShift As DataTable = CODEINFO.GetDatatable("WC_SHIFT")
        Dim dtWorkType As DataTable = CODEINFO.GetDatatable("WC_WORK_TYPE")
        Dim line As Integer = 0
        For Each gr As GridViewRow In gvShow.Rows
            With gr
                'find control
                Dim ddlWorkType As DropDownList = .FindControl("ddlWorkType")
                Dim cbMultiMO As CheckBox = .FindControl("cbMultiMO")
                Dim ddlShift As DropDownList = .FindControl("ddlShift")
                Dim ddlProcessType As DropDownList = .FindControl("ddlProcessType")
                Dim tbNormalTime As TextBox = .FindControl("tbNormalTime")
                Dim tbOverTime As TextBox = .FindControl("tbOverTime")
                Dim tbCount As TextBox = .FindControl("tbCount")
                'set show
                ddlCont.showDDL(ddlWorkType, dtWorkType, CODEINFO.Name, CODEINFO.Code, True)
                ddlCont.showDDL(ddlShift, dtShift, CODEINFO.Name, CODEINFO.Code, True)
                ddlCont.showDDL(ddlProcessType, dtProcessType, CODEINFO.Name, CODEINFO.Code, True)
                'set val
                Dim dr As DataRow = dt.Rows(line)
                Dim workType As String = dtCont.IsDBNullDataRow(dr, "UDF01")
                Dim multiMo As String = dtCont.IsDBNullDataRow(dr, "UDF02")
                Dim shift As String = dtCont.IsDBNullDataRow(dr, "UDF03")
                Dim processType As String = dtCont.IsDBNullDataRow(dr, "UDF04")
                Dim normalTime As Integer = CInt(dtCont.IsDBNullDataRowDecimal(dr, "UDF51") / 60)
                Dim overTime As Integer = CInt(dtCont.IsDBNullDataRowDecimal(dr, "UDF52") / 60)
                Dim cnt As Integer = CInt(dtCont.IsDBNullDataRowDecimal(dr, "UDF53"))

                ddlWorkType.Text = dtCont.IsDBNullDataRow(dr, "UDF01")
                cbMultiMO.Checked = If(multiMo = "Y", True, False)
                ddlShift.Text = dtCont.IsDBNullDataRow(dr, "UDF03")
                ddlProcessType.Text = dtCont.IsDBNullDataRow(dr, "UDF04")
                tbNormalTime.Text = If(normalTime > 0, normalTime, "")
                tbOverTime.Text = If(overTime > 0, overTime, "")
                tbCount.Text = If(cnt > 0, cnt, "")
            End With
            line += 1
        Next

    End Sub

    Function GetDatatable() As DataTable
        Dim strSQL As New SQLString("CMSMX")
        Dim fldName As New ArrayList From {
            "MD001",'wc
            "MD002",'wc name
            "MX001",'mach or line code
            "MX003",'mach or line name
            "CMSMX.UDF01",'work type
            "CMSMX.UDF02",'multi mo
            "CMSMX.UDF03",'shift
            "CMSMX.UDF04",'process type
            "CMSMX.UDF51",'normal time
            "CMSMX.UDF52",'over time
            "CMSMX.UDF53"'count
        }
        strSQL.SetSelect(fldName)
        Dim leftJoinList As New ArrayList From {"MD001:MX002"}
        strSQL.setLeftjoin(strSQL.getLeftjoinManual("CMSMD", leftJoinList))
        Dim whr As String = ""
        whr &= strSQL.WHERE_EQUAL("MD001", ddlWC)
        whr &= strSQL.WHERE_IN("MX001", cblMachLine)
        strSQL.SetWhere(whr, True)
        strSQL.SetOrderBy("MD001,MX001")
        Return strSQL.Query(strSQL.GetSQLString, VarIni.ERP, strSQL.WhoCalledMe)
    End Function

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click

        Dim dt As DataTable = GetDatatable()
        Dim line As Integer = 0
        Dim sqlList As New ArrayList
        Dim sqlList2 As New ArrayList
        Dim lineUpdate As Integer = 0
        For Each gr As GridViewRow In gvShow.Rows
            With gr
                If checkUpdate(gr, dt.Rows(line)) Then
                    Dim wc As String = .Cells(0).Text.Trim
                    Dim mach As String = .Cells(2).Text.Trim
                    Dim ddlWorkType As DropDownList = .FindControl("ddlWorkType")
                    Dim cbMultiMO As CheckBox = .FindControl("cbMultiMO")
                    Dim ddlShift As DropDownList = .FindControl("ddlShift")
                    Dim ddlProcessType As DropDownList = .FindControl("ddlProcessType")
                    Dim tbNormalTime As TextBox = .FindControl("tbNormalTime")
                    Dim tbOverTime As TextBox = .FindControl("tbOverTime")
                    Dim tbCount As TextBox = .FindControl("tbCount")
                    'update to CMSMX
                    'Dim fldHash As New Hashtable
                    Dim fldHash As New Hashtable From {
                        {"UDF01", If(ddlWorkType.Text = "0", "", ddlWorkType.Text)},'work type
                        {"UDF02", If(cbMultiMO.Checked, "Y", "N")},'multi mo
                        {"UDF03", If(ddlShift.Text = "0", "", ddlShift.Text)},'shift
                        {"UDF04", If(ddlProcessType.Text = "0", "", ddlProcessType.Text)},'process type
                        {"UDF51", outCont.checkNumberic(tbNormalTime) * 60},'normal time
                        {"UDF52", outCont.checkNumberic(tbOverTime) * 60},'over time
                        {"UDF53", outCont.checkNumberic(tbCount)}'count
                    }
                    Dim whrHash As New Hashtable From {
                       {"MX001", mach},
                       {"MX002", wc}
                   }
                    sqlList.Add(dbConn.getUpdateSql("CMSMX", fldHash, whrHash))
                    'insert LogWorkcenterInfo
                    fldHash.Add("CreateBy", Session(VarIni.UserName))
                    fldHash.Add("CreateDate", Date.Now.ToString("yyyyMMdd HH:mm"))
                    sqlList2.Add(dbConn.getInsertSql(CreateTable.tableLogWorkcenterInfo, fldHash, whrHash))
                    lineUpdate += 1
                End If
            End With
            line += 1
        Next
        Dim msg As String = "No Data Update"
        If sqlList.Count > 0 Then
            dbConn.TransactionSQL(sqlList, VarIni.ERP, dbConn.WhoCalledMe)
            dbConn.TransactionSQL(sqlList2, VarIni.DBMIS, dbConn.WhoCalledMe)
            msg = "Data update " & lineUpdate & " Rows "
            btShow_Click(sender, e)
        End If
        show_message.ShowMessage(Page, msg, UpdatePanel1)
    End Sub

    Function checkUpdate(gr As GridViewRow, dr As DataRow) As Boolean
        Dim isUpdate As Boolean = False
        With gr
            Dim ddlWorkType As DropDownList = .FindControl("ddlWorkType")
            Dim cbMultiMO As CheckBox = .FindControl("cbMultiMO")
            Dim ddlShift As DropDownList = .FindControl("ddlShift")
            Dim ddlProcessType As DropDownList = .FindControl("ddlProcessType")
            Dim tbNormalTime As TextBox = .FindControl("tbNormalTime")
            Dim tbOverTime As TextBox = .FindControl("tbOverTime")
            Dim tbCount As TextBox = .FindControl("tbCount")

            Dim workType As String = dtCont.IsDBNullDataRow(dr, "UDF01")
            Dim multiMo As String = dtCont.IsDBNullDataRow(dr, "UDF02")
            Dim shift As String = dtCont.IsDBNullDataRow(dr, "UDF03")
            Dim processType As String = dtCont.IsDBNullDataRow(dr, "UDF04")
            Dim normalTime As Integer = dtCont.IsDBNullDataRowDecimal(dr, "UDF51") * 60
            Dim overTime As String = dtCont.IsDBNullDataRowDecimal(dr, "UDF52") * 60
            Dim cnt As String = dtCont.IsDBNullDataRowDecimal(dr, "UDF53")

            'work type
            Dim val As String = If(ddlWorkType.Text = "0", "", ddlWorkType.Text)
            If ddlWorkType.Text <> workType Then
                Return True
            End If
            'multi mo
            val = If(cbMultiMO.Checked, "Y", "N")
            If val <> multiMo Then
                Return True
            End If
            'shift
            val = If(ddlShift.Text = "0", "", ddlShift.Text)
            If val <> shift Then
                Return True
            End If
            'process type
            val = If(ddlProcessType.Text = "0", "", ddlProcessType.Text)
            If val <> processType Then
                Return True
            End If
            'normal time (min)
            Dim valDec As Integer = outCont.checkNumberic(tbNormalTime)
            If valDec <> normalTime Then
                Return True
            End If
            'over time(Min)
            valDec = outCont.checkNumberic(tbOverTime)
            If valDec <> overTime Then
                Return True
            End If
            'count
            valDec = outCont.checkNumberic(tbCount)
            If valDec <> cnt Then
                Return True
            End If
        End With
        Return False
    End Function

    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim wc As String = .Cells(0).Text.Trim
                Dim wcName As String = .Cells(1).Text.Trim
                Dim mach As String = .Cells(2).Text.Trim
                Dim machName As String = .Cells(3).Text.Trim
                With .Cells(11)
                    .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                    .Attributes.Add("onclick", "NewWindow('WorkCenterInfoPop.aspx?wc=" & wc & "&wcname=" & wcName & "&mach=" & mach & "&machname=" & machName & "','_blank',800,500,'yes')")
                End With
            End If
        End With
    End Sub
End Class