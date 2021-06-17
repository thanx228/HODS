Public Class LeaveOperator
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 in ('W01','W02','W07','W12','W19','W23','W25','W27')
            ControlForm.showDDL(ddlWc, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("LeaveOperator" & Session("UserName"), gvShow)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = ""

        WHR = WHR & Conn_SQL.Where("TE004", tbStaff)
        WHR = WHR & Conn_SQL.Where("TD004", ddlWc)
        WHR = WHR & Conn_SQL.Where("TD008", configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))
        SQL = " select TD004 as 'WC',MD002 as 'WC Name',TE004 as 'Staff Code'," & _
              " case when MV047='' then MV002 else MV047 end as 'Staff Name', " & _
              " (SUBSTRING(TD008,7,2)+'-'+SUBSTRING(TD008,5,2)+'-'+SUBSTRING(TD008,1,4)) as 'Doc Date'," & _
              " TE015 as 'Remark'" & _
              " from SFCTE left join SFCTD on TD001 = TE001 and TD002 = TE002 " & _
              " left join CMSMD on MD001=TD004 " & _
              " left join CMSMV on MV001=TE004 " & _
              " where TD005='Y' and TE012 in ('0','1') and SFCTE.TE006 = '5195' " & WHR & _
              " order by TD004,TE004,TD008"
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btSum_Click(sender As Object, e As EventArgs) Handles btSum.Click
        Dim tempTable As String = "tempLeaveOperator" & Session("UserName")
        CreateTempTable.createTempLeaveOperator(tempTable)
        Dim SQL As String = "",
            WHR As String = "",
            lstEmp As String = "",
            lstWc As String = ""

        WHR = WHR & Conn_SQL.Where("TE004", tbStaff)
        WHR = WHR & Conn_SQL.Where("TD004", ddlWc)
        WHR = WHR & Conn_SQL.Where("TD008", configDate.dateFormat2(tbDateFrom.Text.Trim), configDate.dateFormat2(tbDateTo.Text.Trim))
        SQL = " select TD004,TE004,RTRIM(LTRIM(substring(TE015,1,2))) as 'LeaveCon',count(*) as cnt" & _
              " from SFCTE left join SFCTD on TD001 = TE001 and TD002 = TE002 " & _
              " where TD005='Y' and TE012 in ('0') and SFCTE.TE006 = '5195' " & WHR & _
              " group by TD004,TE004,RTRIM(LTRIM(substring(TE015,1,2))) " & _
              " order by TD004,TE004,RTRIM(LTRIM(substring(TE015,1,2))) "
        genData(tempTable, SQL)


        SQL = " select TD004,TE004 ,RTRIM(LTRIM(substring(TE015,1,3))) as 'LeaveCon',count(*) as cnt" & _
              " from SFCTE left join SFCTD on TD001 = TE001 and TD002 = TE002 " & _
              " where TD005='Y' and TE012 in ('1') and SFCTE.TE006 = '5195' " & WHR & _
              " group by TD004,TE004 ,RTRIM(LTRIM(substring(TE015,1,3))) " & _
              " order by TE004,RTRIM(LTRIM(substring(TE015,1,3))) "
        genData(tempTable, SQL)

        Dim dataDic As New Dictionary(Of String, Integer),
            pair = New KeyValuePair(Of String, Integer),
            fld As String = "",
            fldSum As String = ""
        dataDic = setLeave()
        For Each pair In dataDic
            fld = fld & ", case when cast(T." & pair.Key & " as varchar(5))='0' then '' else cast(T." & pair.Key & " as varchar(5)) end as '" & pair.Key & "' "
            fldSum = fldSum & "T." & pair.Key & "+"
        Next
        fld = fld & "," & fldSum.Substring(0, fldSum.Length - 1) & " as 'Sum'"
        SQL = " select T.wc as 'WC',CMSMD.MD002 as 'WC Name',T.EmpId as 'Staff ID', " & _
              " case when CMSMV.MV047='' then CMSMV.MV002 else CMSMV.MV047 end as 'Staff Name' " & fld & _
              " from " & tempTable & " T " & _
              " left join JINPAO80.dbo.CMSMD CMSMD on CMSMD.MD001=T.wc " & _
              " left join JINPAO80.dbo.CMSMV CMSMV on CMSMV.MV001=T.EmpId " & _
              " order by T.wc,T.EmpId "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Sub genData(ByVal temptable As String, ByVal SQL As String)

        Dim lstEmp As String = "",
            lstWc As String = ""
        Dim Program As New DataTable,
            dataDic As New Dictionary(Of String, Integer)
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim empId As String = Program.Rows(i).Item("TE004")
            Dim wc As String = Program.Rows(i).Item("TD004")
            Dim fld As String = Program.Rows(i).Item("LeaveCon").ToString.Trim
            Dim cnt As Integer = Program.Rows(i).Item("cnt")
            If lstEmp <> empId Or lstWc <> wc Then
                If lstEmp <> "" And lstWc <> "" Then
                    saveData(tempTable, lstEmp, lstWc, dataDic)
                End If
                dataDic = New Dictionary(Of String, Integer)
                dataDic = setLeave()
            End If
            If dataDic.ContainsKey(fld) Then
                dataDic(fld) = dataDic(fld) + Program.Rows(i).Item("cnt")
            End If
            lstEmp = empId
            lstWc = wc
        Next
        If lstEmp <> "" And lstWc <> "" Then
            saveData(tempTable, lstEmp, lstWc, dataDic)
        End If
    End Sub

    Function setLeave() As Dictionary(Of String, Integer)
        Dim listLeave As New Dictionary(Of String, Integer)
        listLeave.Add("A", 0)
        listLeave.Add("B", 0)
        listLeave.Add("C1", 0)
        listLeave.Add("C2", 0)
        listLeave.Add("D", 0)
        listLeave.Add("E", 0)
        listLeave.Add("F", 0)
        listLeave.Add("G", 0)
        listLeave.Add("H", 0)
        listLeave.Add("I", 0)
        listLeave.Add("J", 0)
        listLeave.Add("K", 0)
        listLeave.Add("L", 0)
        listLeave.Add("M", 0)
        listLeave.Add("N", 0)
        listLeave.Add("O", 0)
        listLeave.Add("Q1", 0)
        listLeave.Add("Q2", 0)
        listLeave.Add("Q3", 0)
        listLeave.Add("P1", 0)
        listLeave.Add("P2", 0)
        listLeave.Add("P3", 0)
        listLeave.Add("XQ1", 0)
        listLeave.Add("XQ2", 0)
        listLeave.Add("XQ3", 0)
        listLeave.Add("XP1", 0)
        listLeave.Add("XP2", 0)
        listLeave.Add("XP3", 0)
        Return listLeave
    End Function
    Protected Sub saveData(ByVal temptable As String, ByVal empId As String, ByVal wc As String, ByRef dataDic As Dictionary(Of String, Integer))

        Dim insertHash = New Hashtable,
            whereHash = New Hashtable,
            updateHash = New Hashtable,
            pair As KeyValuePair(Of String, Integer)
        whereHash.Add("EmpId", empId)
        whereHash.Add("wc", wc)
        For Each pair In dataDic
            insertHash.Add(pair.Key, pair.Value)
            updateHash.Add(pair.Key, pair.Key & "+'" & pair.Value & "' ")
        Next
        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(temptable, insertHash, updateHash, whereHash), Conn_SQL.MIS_ConnectionString)
    End Sub

End Class