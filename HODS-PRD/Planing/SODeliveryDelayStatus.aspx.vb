Public Class SODeliveryDelayStatus
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim SOType As String = "('2201','2202','2203','2204','2205','2213')"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 like '22' and MQ001 in " & (SOType) & " order by MQ002 "
            ControlForm.showDDL(ddlType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
            'If Session("UserName") = "" Then
            '    Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            'End If
        End If

    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShow.Click


        Dim tempTable As String = "tempSODeliveryDelay" & Session("UserName")
        CreateTempTable.createTempSODeliveryDelay(tempTable)


        SearchBy(tempTable)


        Dim SQL As String = "",
            SQLSum As String = "",
         WHR As String = "",
         day As String = "",
         CusCode As String = tbCustId.Text.Trim,
         Item As String = tbItem.Text.Trim,
         Spec As String = tbSpec.Text.Trim,
         SONo As String = tbSO.Text.Trim,
         DateFrom As String = tbFromDate.Text.Trim

        WHR = WHR & Conn_SQL.Where("COPTC.TC001", ddlType)

        If ddlType.SelectedValue = "ALL" Then
            WHR = WHR & " and COPTC.TC001 in " & SOType & ""
        End If

        If SONo <> "" Then
            WHR = WHR & " and COPTC.TC002 like '" & SONo & "%' "
        End If

        If Item <> "" Then
            WHR = WHR & " and COPTD.TD004 like '" & Item & "%' "
        End If

        If CusCode <> "" Then
            WHR = WHR & " and COPTC.TC004 like '" & CusCode & "%' "
        End If

        If Spec <> "" Then
            WHR = WHR & " and COPTD.TD006 like '" & Spec & "%' "
        End If

        If DateFrom <> "" Then
            day = " DATEDIFF(DAY,COPTD.TD013," & "'" & configDate.dateFormat2(DateFrom) & "')"

        Else
            tbFromDate.Text = Date.Today.ToString("dd/MM/yyyy")
            day = " DATEDIFF(DAY,COPTD.TD013," & "'" & Date.Today.ToString("yyyyMMdd") & "')"
        End If


        If ddlCon.SelectedValue = "FG < SO Bal." Then
            WHR = WHR & " and INVMC.MC007 < (COPTD.TD008-COPTD.TD009)"
        Else
            WHR = WHR & " and INVMC.MC007 > (COPTD.TD008-COPTD.TD009)"
        End If


        SQLSum = " select T.CusName as 'Cus. Name', T.TotalItem as 'Total Item', " & _
           " T.to6 as '1 - 6 Days'," & _
           " T.to14 as '7 - 14 Days',T.to21 as '15 - 21 Days', " & _
           " T.more21 as '> 21 Days'" & _
           " from " & tempTable & " T " & _
           " order by T.CusName "

        SQL = " select COPMA.MA002 as 'Cus. Name', COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as 'SO No', " & _
            " (SUBSTRING(COPTD.TD004,1,14)+'-'+SUBSTRING(COPTD.TD004,15,2)) as 'Item',COPTD.TD006 as 'Spec', " & _
            " COPTD.TD008 as 'SO Qty'," & _
            " (COPTD.TD008-COPTD.TD009) as 'SO Bal.'," & _
            " INVMC.MC007 as 'FG'," & _
            " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as 'Deliver Date'," & day & _
            " AS 'Delay Days'" & _
            " FROM COPTD " & _
            " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001 = COPTD.TD004 and INVMB.MB003 = COPTD.TD006  " & _
            " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 " & _
            " left join JINPAO80.dbo.COPMA COPMA on COPMA.MA001 = COPTC.TC004  " & _
            " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017   " & _
            " WHERE " & day & " > 0 and COPTC.TC027 = 'Y' and COPTD.TD016 = 'N' " & WHR & _
            " order by COPMA.MA002,COPTD.TD001,COPTD.TD002,COPTD.TD003,COPTD.TD004 "

        If ddlShow.SelectedValue = "Detail" Then

            ControlForm.ShowGridView(gvShowSum, SQLSum, Conn_SQL.MIS_ConnectionString)
            lbCountSum.Text = ControlForm.rowGridview(gvShowSum)

            ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
            lbCount.Text = ControlForm.rowGridview(gvShow)

            gvShow.Visible = True
            lbCount.Visible = True

        Else

            ControlForm.ShowGridView(gvShowSum, SQLSum, Conn_SQL.MIS_ConnectionString)
            lbCountSum.Text = ControlForm.rowGridview(gvShowSum)

            lbCount.Visible = False
            gvShow.Visible = False

        End If


        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)


    End Sub

    Protected Sub SearchBy(ByVal tempTable As String)

        Dim SQL As String = "",
            WHR As String = "",
            USQL As String = ""

        Dim Program As New DataTable
        Dim Code As String = "",
            Name As String = "",
            Qty As Integer = 0

        Dim day As String = "",
         CusCode As String = tbCustId.Text.Trim,
         ItemCode As String = tbItem.Text.Trim,
         Spec As String = tbSpec.Text.Trim,
         SONo As String = tbSO.Text.Trim,
         DateFrom As String = tbFromDate.Text.Trim

        WHR = WHR & Conn_SQL.Where("COPTC.TC001", ddlType)

        If ddlType.SelectedValue = "ALL" Then
            WHR = WHR & " and COPTC.TC001 in " & SOType & ""
        End If

        If SONo <> "" Then
            WHR = WHR & " and COPTC.TC002 like '" & SONo & "%' "
        End If

        If ItemCode <> "" Then
            WHR = WHR & " and COPTD.TD004 like '" & ItemCode & "%' "
        End If

        If CusCode <> "" Then
            WHR = WHR & " and COPTC.TC004 like '" & CusCode & "%' "
        End If

        If Spec <> "" Then
            WHR = WHR & " and COPTD.TD006 like '" & Spec & "%' "
        End If

        If DateFrom <> "" Then
            day = " DATEDIFF(DAY,COPTD.TD013," & "'" & configDate.dateFormat2(DateFrom) & "')"

        Else
            tbFromDate.Text = Date.Today.ToString("dd/MM/yyyy")
            day = " DATEDIFF(DAY,COPTD.TD013," & "'" & Date.Today.ToString("yyyyMMdd") & "')"
        End If


        If ddlCon.SelectedValue = "FG < SO Bal." Then
            WHR = WHR & " and INVMC.MC007 < (COPTD.TD008-COPTD.TD009)"
        Else
            WHR = WHR & " and INVMC.MC007 > (COPTD.TD008-COPTD.TD009)"
        End If


        'TotalItem
        SQL = " select COPMA.MA001 AS 'CusCode', COPMA.MA002 AS 'CusName' ,count(COPTD.TD004) AS 'TotalItem' " & _
                 " FROM COPTD " & _
                 " left join INVMB on INVMB.MB001 = COPTD.TD004 and INVMB.MB003 = COPTD.TD006 " & _
                 " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 " & _
                 " left join COPMA on COPMA.MA001 = COPTC.TC004 " & _
                 " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017   " & _
                 " WHERE " & day & " > 0 and COPTC.TC027 = 'Y' and COPTD.TD016 = 'N' " & WHR & _
                 " group by COPMA.MA001, COPMA.MA002"

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Code = Program.Rows(i).Item("CusCode")
            Name = Program.Rows(i).Item("CusName")
            Qty = Program.Rows(i).Item("TotalItem")

            USQL = " if exists(select * from " & tempTable & " where CusCode='" & Code & "'  ) " & _
                   " update " & tempTable & " set TotalItem='" & Qty & "', CusName='" & Name & "' where CusCode='" & Code & "' else " & _
                   " insert into " & tempTable & "(CusCode,TotalItem,CusName)values ('" & Code & "','" & Qty & "','" & Name & "')"

            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next



        '1-6 Days
        SQL = " select COPMA.MA001 AS 'CusCode', COPMA.MA002 AS 'CusName' ,count(COPTD.TD004) AS 'to6' " & _
         " FROM COPTD " & _
         " left join INVMB on INVMB.MB001 = COPTD.TD004 and INVMB.MB003 = COPTD.TD006 " & _
         " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 " & _
         " left join COPMA on COPMA.MA001 = COPTC.TC004 " & _
         " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017   " & _
         " WHERE " & day & " between 1 and 6 and COPTC.TC027 = 'Y' and COPTD.TD016 = 'N' " & WHR & _
         " group by COPMA.MA001, COPMA.MA002"

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Code = Program.Rows(i).Item("CusCode")
            Name = Program.Rows(i).Item("CusName")
            Qty = Program.Rows(i).Item("to6")

            USQL = " if exists(select * from " & tempTable & " where CusCode='" & Code & "'  ) " & _
                   " update " & tempTable & " set to6='" & Qty & "', CusName='" & Name & "' where CusCode='" & Code & "' else " & _
                   " insert into " & tempTable & "(CusCode,to6,CusName)values ('" & Code & "','" & Qty & "','" & Name & "')"

            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next
       

        '7-14 Days
        SQL = " select COPMA.MA001 AS 'CusCode', COPMA.MA002 AS 'CusName' ,count(COPTD.TD004) AS 'to14' " & _
        " FROM COPTD " & _
        " left join INVMB on INVMB.MB001 = COPTD.TD004 and INVMB.MB003 = COPTD.TD006 " & _
        " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 " & _
        " left join COPMA on COPMA.MA001 = COPTC.TC004 " & _
        " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017   " & _
        " WHERE " & day & " between 7 and 14 and COPTC.TC027 = 'Y' and COPTD.TD016 = 'N' " & WHR & _
        " group by COPMA.MA001, COPMA.MA002"

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Code = Program.Rows(i).Item("CusCode")
            Name = Program.Rows(i).Item("CusName")
            Qty = Program.Rows(i).Item("to14")

            USQL = " if exists(select * from " & tempTable & " where CusCode='" & Code & "'  ) " & _
                   " update " & tempTable & " set to14='" & Qty & "', CusName='" & Name & "' where CusCode='" & Code & "' else " & _
                   " insert into " & tempTable & "(CusCode,to14,CusName)values ('" & Code & "','" & Qty & "','" & Name & "')"

            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next


        '15-21  Days
        SQL = " select COPMA.MA001 AS 'CusCode', COPMA.MA002 AS 'CusName' ,count(COPTD.TD004) AS 'to21' " & _
        " FROM COPTD " & _
        " left join INVMB on INVMB.MB001 = COPTD.TD004 and INVMB.MB003 = COPTD.TD006 " & _
        " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 " & _
        " left join COPMA on COPMA.MA001 = COPTC.TC004 " & _
        " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017   " & _
        " WHERE " & day & " between 15 and 21 and COPTC.TC027 = 'Y' and COPTD.TD016 = 'N' " & WHR & _
        " group by COPMA.MA001, COPMA.MA002"

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Code = Program.Rows(i).Item("CusCode")
            Name = Program.Rows(i).Item("CusName")
            Qty = Program.Rows(i).Item("to21")

            USQL = " if exists(select * from " & tempTable & " where CusCode='" & Code & "'  ) " & _
                   " update " & tempTable & " set to21='" & Qty & "', CusName='" & Name & "' where CusCode='" & Code & "' else " & _
                   " insert into " & tempTable & "(CusCode,to21,CusName)values ('" & Code & "','" & Qty & "','" & Name & "')"

            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next


        ' > 21 Days
        SQL = " select COPMA.MA001 AS 'CusCode', COPMA.MA002 AS 'CusName' ,count(COPTD.TD004) AS 'more21' " & _
        " FROM COPTD " & _
        " left join INVMB on INVMB.MB001 = COPTD.TD004 and INVMB.MB003 = COPTD.TD006 " & _
        " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 " & _
        " left join COPMA on COPMA.MA001 = COPTC.TC004 " & _
        " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017   " & _
        " WHERE " & day & " > 21 and COPTC.TC027 = 'Y' and COPTD.TD016 = 'N' " & WHR & _
        " group by COPMA.MA001, COPMA.MA002"

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Code = Program.Rows(i).Item("CusCode")
            Name = Program.Rows(i).Item("CusName")
            Qty = Program.Rows(i).Item("more21")

            USQL = " if exists(select * from " & tempTable & " where CusCode='" & Code & "'  ) " & _
                   " update " & tempTable & " set more21='" & Qty & "', CusName='" & Name & "' where CusCode='" & Code & "' else " & _
                   " insert into " & tempTable & "(CusCode,more21,CusName)values ('" & Code & "','" & Qty & "','" & Name & "')"

            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next


    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click

        If ddlShow.SelectedValue = "Detail" Then
            ControlForm.ExportGridViewToExcel("SODeliveryDelayDetail" & Session("UserName"), gvShow)
        Else
            ControlForm.ExportGridViewToExcel("SODeliveryDelaySum" & Session("UserName"), gvShowSum)
        End If

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class