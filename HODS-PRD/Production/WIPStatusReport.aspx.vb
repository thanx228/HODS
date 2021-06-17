Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class WIPStatusReport
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim ChkCont As New CheckBoxListControl
    Dim gvCont As New GridviewControl
    Dim whrCont As New WhereControl
    Dim dtCont As New DataTableControl
    Dim dbConn As New DataConnectControl
    Dim hashCont As New HashtableControl
    Dim expCont As New ExportImportControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'Work Center
            Dim SQL As String = "select MD001,rtrim(MD001)+' : '+MD002 as MD002 from CMSMD where isnull(CMSMD.UDF07,'') <>'No' order by MD001 "
            ChkCont.showCheckboxList(CheckWorkC, SQL, VarIni.ERP, "MD002", "MD001", 5)

            btExport.Visible = False
            btExportDetail.Visible = False
            'ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim SQL As String = String.Empty
        Dim WHR As String = String.Empty
        Dim headTable, dataFld As New ArrayList
        Dim wc As String = String.Empty

        WHR = dbConn.WHERE_EQUAL("DATE_RECIECE_WIP", UcDate.TextEmptyToday, "<=")

        SQL = "select * from (
                select TC001,TC002,TC004,TC005,TC008,TC009,DATE_RECIECE_WIP,ROW_NUMBER() OVER( PARTITION BY TC004,TC005,TC008 ORDER BY DATE_RECIECE_WIP desc )  rowLine
                from 
                    (
                        SELECT SFCTC.TC001,SFCTC.TC002,SFCTC.TC004,SFCTC.TC005,SFCTC.TC008,SFCTC.TC009, substring(case when MODI_DATE='' or MODI_DATE is null then CREATE_DATE else MODI_DATE end,1,8) DATE_RECIECE_WIP   FROM SFCTC 
                        WHERE TC001  not like 'D3__'
                    ) AA ) BB where rowLine=1 " & WHR

        WHR = dbConn.WHERE_EQUAL("(C.TA010+C.TA013+C.TA016-C.TA011-C.TA012-C.TA014-C.TA015-C.TA048-C.TA056-C.TA058)", "0", ">")
        WHR &= dbConn.WHERE_IN("B.TA011", "Y,y", True, True)
        WHR &= dbConn.WHERE_IN("C.TA006", CheckWorkC, allWhenNotSelect:=True)

        SQL = "select C.TA006 ,ABS(DATEDIFF(day,getdate(),convert(date,DATE_RECIECE_WIP))) date_diff
                from  SFCTA C 
                left join MOCTA B On C.TA001 = B.TA001 And C.TA002 = B.TA002
                left join (" & SQL & ") D ON  D.TC004 = C.TA001 And D.TC005 = C.TA002 And D.TC008 = C.TA003 And D.TC009 = C.TA004
                where C.COMPANY='HOOTHAI'  " & WHR

        SQL = "Select TA006,
                sum(case when date_diff<=3 then 1 else 0 end) wip3,
                sum(case when date_diff>3 And date_diff<=7 then 1 else 0 end) wip7,
                sum(case when date_diff>7 And date_diff<=15 then 1 else 0 end) wip15,
                sum(case when date_diff>15 And date_diff<=30 then 1 else 0 end) wip30,
                sum(case when date_diff>31 And date_diff<=60 then 1 else 0 end) wip60,
                sum(case when date_diff>60 then 1 else 0 end) wip61 from 
                (" & SQL & ") t1
                group by TA006"

        SQL = "select TA006,MD002,wip3,wip7,wip15,wip30,wip60,wip61,wip3+wip7+wip15+wip30+wip60+wip61 sum_all from (" & SQL & ") t2 left join CMSMD on MD001=TA006 order by TA006"

        Dim colName As New ArrayList From {
            "Work Center" & VarIni.char8 & "TA006",
            "WorkCenterName" & VarIni.char8 & "MD002",
            "0~3 Days" & VarIni.char8 & "wip3" & VarIni.char8 & "0",
            "4~7 Days" & VarIni.char8 & "wip7" & VarIni.char8 & "0",
            "8~15 Days" & VarIni.char8 & "wip15" & VarIni.char8 & "0",
            "16~30 Days" & VarIni.char8 & "wip30" & VarIni.char8 & "0",
            "31~60 Days" & VarIni.char8 & "wip60" & VarIni.char8 & "0",
            ">60 Days" & VarIni.char8 & "wip61" & VarIni.char8 & "0",
            "SUM ALL" & VarIni.char8 & "sum_all" & VarIni.char8 & "0"
        }

        gvCont.GridviewColWithLinkFirst(gvShow, colName, isHyperlink:=True, strSplit:=VarIni.char8)
        gvCont.ShowGridView(gvShow, SQL, VarIni.ERP)
        Dim rCount As Decimal = gvCont.rowGridview(gvShow)
        CountRow1.RowCount = rCount
        Dim showButt As Boolean = False
        If rCount > 0 Then
            showButt = True
        End If
        btExport.Visible = showButt
        btExportDetail.Visible = showButt
        System.Threading.Thread.Sleep(1000)

    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        CheckWorkC.SelectedValue = Nothing
        CheckAll.Checked = False
        btExport.Visible = False
        btExportDetail.Visible = False
        CountRow1.RowCount = ""
        gvShow.DataSource = Nothing
        gvShow.DataBind()
    End Sub

    'ทำ Link 
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplDetail"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("TA006")) Then
                    hplDetail.NavigateUrl = "WIPStatusReportPopup.aspx?wc=" & .DataItem("TA006").ToString.Trim & "&enddate=" & UcDate.TextEmptyToday
                    hplDetail.Attributes.Add("title", .DataItem("MD002").ToString.Trim)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With

    End Sub

    'เลือกทั้งหมดใน CheckBox CheckWorkC  
    Protected Sub CheckAll_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAll.CheckedChanged
        For Each i As ListItem In CheckWorkC.Items
            i.Selected = CheckAll.Checked
        Next
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        For Each row As GridViewRow In gvShow.Rows
            For Each cell As TableCell In row.Cells
                cell.Height = 20
            Next
        Next
        ExportsUtility.ExportGridviewToMsExcel("WIPStatusReport", gvShow, False)
    End Sub

    Protected Sub btExportDetail_Click(sender As Object, e As EventArgs) Handles btExportDetail.Click
        Dim WHR As String = ""
        Dim wipStatusPopup As New WIPStatusReportPopup
        WHR &= whrCont.Where("CMS.MD001", CheckWorkC, False, "", True)
        Dim dt As DataTable = wipStatusPopup.wipStatus("", WHR, UcDate.TextEmptyToday)
        expCont.Export("WIPStatusReportDetail ", dt)
        'expCont.Export("WIPStatusReportDetail ", wipStatusPopup.getColName)
    End Sub

    Function Checklist(checkboxList As CheckBoxList) As String

        Dim type As String = "",
                cnt As Decimal = 0,
                typeAll As String = ""

        For Each boxItem As ListItem In checkboxList.Items
            Dim boxVal As String = CStr(boxItem.Value.Trim)
            typeAll &= boxVal & ","
            If boxItem.Selected Then
                Type &= boxVal & ","
                cnt = cnt + 1
            End If
        Next

        Return type

    End Function

    'Save File
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub



End Class
