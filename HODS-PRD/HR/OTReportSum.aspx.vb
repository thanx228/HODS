Imports System.Globalization

Public Class OTReportSum
    Inherits System.Web.UI.Page
    Dim CreateTable As New CreateTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim Table As String = "OTRecord"
    Dim chrConn As String = Chr(8)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            tbUserID.Text = Session("UserName")

            If tbDateFrom.Text = "" Then
                tbDateFrom.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("dd/MM/yyyy", New CultureInfo("en-US"))
            End If

            If tbDateTo.Text = "" Then
                tbDateTo.Text = Date.Now.ToString("dd/MM/yyyy", New CultureInfo("en-US"))
            End If

            btExcel.Visible = False
        End If
    End Sub

    Protected Sub btShowRe_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShowRe.Click

        Dim sql As String = ""

        sql = "select EmpNo,rtrim(Dept)+'-'+CMSME.ME002 as 'Dept' ,EmpName , case when Holiday = '' then 'Normal working day' else 'Holiday' end as 'OT Type' , " & _
         " rtrim(ShiftDay) as 'ShiftDay' ,Shift ,rtrim(Absence) as 'Absence', AbsenceTime , OTStartDate  ,  " & _
         " case when OTEndTime <> '' then OTStartTime else '' end as 'OT Start Time', " & _
         " case when OTEndTime <> '' and ShiftDay = 'Night' then CONVERT(VARCHAR(11),DATEADD(day, 1, SUBSTRING(OTStartDate,7,4)+'/'+SUBSTRING(OTStartDate,4,2)+'/'+SUBSTRING(OTStartDate,1,2)),103) when OTEndTime <> '' and ShiftDay like '%Day%' then OTStartDate else '' end as 'End Date', " & _
         " OTEndTime , case when OTEndTime <> '' then DateofOT else '' end as DateofOT  , " & _
         " case when OTEndTime <> '' and Holiday = '' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)),'-','')  " & _
         " when OTEndTime <> '' and Holiday = 'Holiday' and ShiftDay like '%Day%'  then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)-1.00),'-','') " & _
         " when OTEndTime = '12.00' and Holiday = 'Holiday' and ShiftDay like '%Day%' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)),'-','') " & _
         " when OTEndTime = '07.00' and Holiday = 'Holiday' and ShiftDay = 'Night' then '11' " & _
         " when OTEndTime = '04.00' and Holiday = 'Holiday' and ShiftDay = 'Night' then '8' end as 'OT Hour', " & _
         " OTLunch as 'OT Lunch' , " & _
         " case when OTEndTime <> '' and OTLunch = '1' and Holiday = '' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)+1.00),'-','')  " & _
         " when OTEndTime <> '' and OTLunch = '1'  and Holiday = 'Holiday' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)),'-','') " & _
         " when OTEndTime <> '' and Holiday = '' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)),'-','') " & _
         " when OTEndTime <> '' and Holiday = 'Holiday'  and ShiftDay like '%Day%' then REPLACE (CONVERT(DECIMAL(5,1),((datediff(mi, CONVERT(Time, REPLACE(OTStartTime,'.',':')) , CONVERT(Time, REPLACE(OTEndTime,'.',':'))))/60.00)-1),'-','') " & _
         " when OTLunch = '1'  then '1'  " & _
         " when OTEndTime = '07.00' and Holiday = 'Holiday' and ShiftDay = 'Night' then '11' " & _
         " when OTEndTime = '04.00' and Holiday = 'Holiday' and ShiftDay = 'Night' then '8' " & _
         " when OTEndTime = '07.00' and Holiday = 'Holiday' and ShiftDay = 'Night' and OTLunch = '1' then '12' " & _
         " when OTEndTime = '04.00' and Holiday = 'Holiday' and ShiftDay = 'Night' and OTLunch = '1' then '9' end as 'Total OT hours'," & _
         " Rtrim(BusLine) +' : '+ Name as 'BusLine' , Line as 'Line' , Dinner as 'Dinner' , ChangeBy as 'Change By' , ChangeDate as'Change Date' " & _
         " from OTRecord " & _
         " left join CodeInfo on Code  = BusLine  " & _
         " left join JINPAO80.dbo.CMSME on CMSME.ME001 = rtrim(Dept)  " & _
         " where EmpNo = '" & Session("UserName") & "' and DateofOT between '" & tbDateFrom.Text.Trim & "' and '" & tbDateTo.Text.Trim & "'"

        ControlForm.ShowGridView(gvShow, sql, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

        btExcel.Visible = True

    End Sub

    Protected Sub btExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcel.Click
        ControlForm.ExportGridViewToExcel("OTReportSummary" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class