Imports System.Globalization

Public Class BudgetReport
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim SQL As String = "select TA004 from INVTA where INVTA.TA001 between '1105' and '1199' and TA004 <> '' group by TA004 order by TA004 "
            ControlForm.showDDL(ddlDept, SQL, "TA004", "TA004", True, Conn_SQL.ERP_ConnectionString)

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btnExcel.Visible = False
        End If

    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShow.Click

        Dim tempTable As String = "TempBudget" & Session("UserName")
        CreateTempTable.createTempBudget(tempTable)

        SearchBy(tempTable)

        Dim SQL As String = "",
            WHR As String = ""

        SQL = " select T.item as 'D01', T.Inv as 'D02', " & _
           " T.NonInv as 'D03' " & _
           " from " & tempTable & " T " & _
           " order by T.item "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btnExcel.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub
    Protected Sub SearchBy(ByVal tempTable As String)

        Dim SQL As String = "",
            WHR As String = "",
            WHRInv As String = "",
            USQL As String = "",
            Dept As String = ddlDept.Text

        Dim Program As New DataTable
        Dim item As String = "",
            Qty As Decimal = 0

        WHR = configDate.DateWhere("ASTTO.TO004", configDate.dateFormat2(txtDateFrom.Text), configDate.dateFormat2(txtDateTo.Text))

        If Dept <> "ALL" Or Dept = "" Then
            WHR = WHR & " and ASTTP.UDF01 = '" & Dept & "' "
        End If

        If txtDateFrom.Text <> "" Then
            WHR = WHR & " and ASTTO.TO004 >= '" & Date.Today.ToString("yyyyMMdd") & "' "
        Else
            txtDateFrom.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("dd/MM/yyyy", New CultureInfo("en-US"))
        End If

        If txtDateTo.Text <> "" Then
            WHR = WHR & " and ASTTO.TO004 <= '" & Date.Today.ToString("yyyyMMdd") & "' "
        Else
            txtDateTo.Text = Date.Now.ToString("dd/MM/yyyy", New CultureInfo("en-US"))
        End If

        'Total Non-Inv

        SQL = " select ASTTP.UDF01 as item ,SUM(CMSMQ.MQ010*((TP017-TP018)*TO009+TP019)) as NonInv from ASTTP " & _
                   " left join ASTTO on ASTTO.TO001 = ASTTP.TP001 and ASTTO.TO002 = ASTTP.TP002 " & _
                   " left join CMSMQ on CMSMQ.MQ001 = ASTTP.TP001 " & _
                   " where ASTTP.UDF01 <> '' " & WHR & _
                   " group by ASTTP.UDF01 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("NonInv")

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "'  ) " & _
                   " update " & tempTable & " set NonInv='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,NonInv)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next

        'INV'

        WHRInv = configDate.DateWhere("INVTA.TA014", configDate.dateFormat2(txtDateFrom.Text), configDate.dateFormat2(txtDateTo.Text))

        If Dept <> "ALL" Or Dept = "" Then
            WHRInv = WHRInv & " and INVTA.TA004 = '" & Dept & "' "
        End If

        If txtDateFrom.Text <> "" Then
            WHR = WHR & Conn_SQL.Where("INVTA.TA014 >=", configDate.dateFormat2(txtDateFrom.Text), configDate.dateFormat2(txtDateTo.Text))
        Else
            txtDateFrom.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("dd/MM/yyyy", New CultureInfo("en-US"))
        End If

        If txtDateTo.Text <> "" Then
            WHR = WHR & Conn_SQL.Where("INVTA.TA014 <=", configDate.dateFormat2(txtDateFrom.Text), configDate.dateFormat2(txtDateTo.Text))
        Else
            txtDateTo.Text = Date.Now.ToString("dd/MM/yyyy", New CultureInfo("en-US"))
        End If

        SQL = " select INVTA.TA004 as item , sum((convert(decimal(10,2),(case  when CMSMQ.MQ010=1 then -1 else 1 end )))*INVTB.TB011) as Inv from INVTA " & _
                   " left join INVTB on INVTB.TB001 = INVTA.TA001 and INVTB.TB002 = INVTA.TA002 " & _
                   " left join CMSMQ on CMSMQ.MQ001 = INVTB .TB001 " & _
                   " where INVTA.TA001 between '1105' and '1199' and INVTA.TA004 <> '' " & WHRInv & _
                   " group by INVTA.TA004 order by INVTA.TA004 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("Inv")

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "'  ) " & _
                   " update " & tempTable & " set Inv='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,Inv)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("D01")) Then
                    Dim link As String = "",
                    DateFrom As String = txtDateFrom.Text.Trim,
                    DateTo As String = txtDateTo.Text
                    Dim Dept As String = .DataItem("D01")
                    link = link & "&Dept= " & Dept
                    link = link & "&Inv= " & .DataItem("D02")
                    link = link & "&NonInv= " & .DataItem("D03")
                    link = link & "&DateFrom= " & DateFrom
                    link = link & "&DateTo= " & DateTo

                    hplDetail.NavigateUrl = "BudgetReportPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", Dept)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcel.Click
        ControlForm.ExportGridViewToExcel("BudgetingReport" & Session("UserName"), gvShow)
    End Sub
End Class