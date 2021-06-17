Public Class OTReportPopup
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SQL As String = ""

        Dim Dept As String = Request.QueryString("Dept").Trim()
        Dim DateofOT As String = Request.QueryString("DateofOT").Trim()
        Dim EmpNo As String = Request.QueryString("EmpNo").Trim()
        Dim ShiftDay As String = Request.QueryString("ShiftDay").Trim()
        Dim Shift As String = Request.QueryString("Shift").Trim()
        Dim Show As String = Request.QueryString("Show").Trim()

        If EmpNo <> "" Then
            EmpNo = " and EmpNo like '%" & EmpNo & "%'"
        End If

        If ShiftDay <> "" Then
            ShiftDay = " and ShiftDay like '%" & ShiftDay & "%'"
        End If

        If Shift <> "" Then
            Shift = Shift.ToString.Replace(",", "','").Replace("(", "('").Replace(")", "')").Trim
        End If

        If Show = 3 Then
            Show = ""
        ElseIf Show = 4 Then
            Show = " and substring(AbsenceTime,1,1) <> '8' "
        ElseIf Show = 5 Then
            Show = " and Absence <> '' "
        ElseIf Show = 6 Then
            Show = " and substring(AbsenceTime,1,1) = '8' and Absence <> '' "
        ElseIf Show = 7 Then
            Show = " and substring(AbsenceTime,1,1) <> '8' and Absence <> '' "
        ElseIf Show = 8 Then
            Show = " and OTEndTime  <> '' and OTEndTime <> OTStartTime "
        End If

        SQL = "select Shift ,rtrim(Dept)+'-'+CMSME.ME002 as 'Dept' ,Line as 'Line' ,EmpNo , EmpName , " &
            " case when Holiday = '' then 'Normal working day' else 'Holiday' end as 'OT Type' , rtrim(ShiftDay) as 'ShiftDay' , OTStartDate  , rtrim(Absence) as 'Absence', AbsenceTime ,  " &
            " case when OTEndTime <> '' then OTStartTime else '' end as 'OT Start Time', " &
            " case when OTEndTime <> '' then OTEndDate else '' end as 'End Date', " &
            " OTEndTime , case when OTEndTime <> '' then DateofOT else '' end as DateofOT  , " &
            " OTHours as 'OT hrs.', " &
            " OTLunch as 'OT Lunch' , " &
            " OTTotal as 'Total OT hrs.'," &
            " Rtrim(BusLine) +' : '+ Name as 'BusLine' ,  Dinner as 'Dinner' , Remark, ChangeBy as 'Change By' , ChangeDate as'Change Date' " &
            " from OTRecord" &
            " left join CodeInfo on Code  = BusLine and CodeType = 'BusLine' " &
            " left join HOOTHAI.dbo.CMSME on CMSME.ME001 = rtrim(Dept)  " &
            " where Dept in ('" & Dept & "') and DateofOT = '" & DateofOT & "' " & EmpNo & ShiftDay & Shift & Show & " order by Dept,Line,EmpNo,Shift,SUBSTRING(OTStartDate ,7,4)+SUBSTRING(OTStartDate ,4,2)+SUBSTRING(OTStartDate ,1,2) "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub
End Class