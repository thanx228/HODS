Public Class InventoryPriceList
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            btExport.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "TempInventoryListAmount" & Session("UserName")
        genItemPrice(tempTable)
        Dim SQL As String = " select * from JINPAO80.dbo.INVML INVML  "
        Dim rateUSD As Decimal = CDec(tbUSD.Text.Trim)
        Dim rateEUR As Decimal = CDec(tbEUR.Text.Trim)
        Dim wh As String = ddlWh.SelectedValue.ToString.Trim
        Dim whr As String = ""
        If wh <> "0" Then
            'Dim whList As String = "'2101','2102','2103'"
            'whList = "'" & wh & "'"
            whr = " and INVML.ML002 in ('" & wh & "') "
        End If
        SQL = " select T.Item as Item,INVMB.MB002 as 'Description',INVMB.MB003 as 'Specification', " & _
              " INVML.ML002 as 'Warehouse',CMSMC.MC002 as 'Warehouse Name' ,INVMB.MB004 as Unit," & _
              " convert(decimal(15,0),INVML.ML005) as 'Inventory Quantity'," & _
              " case T.Currency when 'USD' then convert(decimal(15,5),T.price*" & rateUSD & ") when 'EUR' then convert(decimal(15,5),T.price*" & rateEUR & ") else convert(decimal(15,2),T.price) end as 'Unit Price' ," & _
              " case T.Currency when 'USD' then convert(decimal(15,2),INVML.ML005*T.price*" & rateUSD & ") when 'EUR' then convert(decimal(15,2),INVML.ML005*T.price*" & rateEUR & ") else convert(decimal(15,2),INVML.ML005*T.price) end as 'Inventory Amount' " & _
              " from DBMIS.dbo." & tempTable & " T " & _
              " left join JINPAO80.dbo.INVML INVML on INVML.ML001=T.Item " & _
              " left join JINPAO80.dbo.INVMB INVMB on  INVMB.MB001=INVML.ML001 " & _
              " left join JINPAO80.dbo.CMSMC CMSMC on CMSMC.MC001=INVML.ML002 " & _
              " where  INVML.ML005>0  and INVMB.MB005 in ('1101','1102','1103','1104') " & whr & _
              " order by INVML.ML001,INVML.ML002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
    End Sub
    Protected Sub genItemPrice(tempTable As String)
        CreateTempTable.createTempItemPrice(tempTable)
        Dim item As String = "",
            currency As String = "",
            price As Decimal = 0,
            effDate As String = "",
            USQL As String = "",
            SQL1 As String = "",
            SQL2 As String = ""
        'Dim whList As String = "'2101','2102','2103'"

        'If wh <> "0" Then
        '    whList = "'" & wh & "'"
        'End If
        Dim whr As String = ""
        If tbCode.Text <> "" Then
            whr = whr & " and MB001 like '%" & tbCode.Text.Trim & "%'"
        End If
        If tbSpec.Text <> "" Then
            whr = whr & " and MB003 like '%" & tbSpec.Text.Trim & "%'"
        End If
        Dim wh As String = ddlWh.SelectedValue.ToString.Trim
        If wh <> "0" Then
            whr = whr & " and ML002 in ('" & wh & "') "
        End If

        Dim SQL As String = " select ML001 from INVML left join INVMB on MB001=ML001" & _
                            " where ML005>0 and MB005 in ('1101','1102','1103','1104') " & whr & " " & _
                            " group by ML001 order by ML001 "
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim Program1 As New DataTable
            item = Program.Rows(i).Item("ML001")
            SQL1 = " select TB001,TB002,TB003,TA004,TA007,TB004,TB007," & _
                   " case TB007 when 0 then TB009 else CONVERT(DECIMAL(15,5),TB009/TB007) end as price," & _
                   " TB016 from  COPTB left join COPTA on TA001=TB001 and TA002 = TB002 " & _
                   " where TA019='Y' and TB004='" & item & "' " & _
                   " order by TB016 desc "
            Program1 = Conn_SQL.Get_DataReader(SQL1, Conn_SQL.ERP_ConnectionString)
            If Program1.Rows.Count > 0 Then
                currency = Program1.Rows(0).Item("TA007")
                price = Program1.Rows(0).Item("price")
                If Program1.Rows(0).Item("TB007") = 0 Then
                    Dim qType As String = Program1.Rows(0).Item("TB001")
                    Dim qNo As String = Program1.Rows(0).Item("TB002")
                    Dim qSeq As String = Program1.Rows(0).Item("TB003")
                    Dim Program2 As New DataTable
                    SQL2 = "select TK006 from COPTK where TK001='" & qType & "' and TK002='" & qNo & "' and TK003='" & qSeq & "' and  TK004>0 order by TK004"
                    Program2 = Conn_SQL.Get_DataReader(SQL2, Conn_SQL.ERP_ConnectionString)
                    If Program2.Rows.Count > 0 Then
                        price = Program2.Rows(0).Item("TK006")
                    End If
                End If
                effDate = Program1.Rows(0).Item("TB016")
                USQL = " insert into " & tempTable & "(Item,Currency,price,effDate)values ('" & item & "','" & currency & "','" & price & "','" & effDate & "')"
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            End If
        Next

        'COPTK
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("InventoryPriceList" & Session("UserName"), gvShow)





        'Dim whr As String = " and (INVML.ML002='2101'or INVML.ML002='2102' or INVML.ML002= '2103')"
        'Dim wh As String = ddlWh.SelectedValue.ToString.Trim
        'If wh <> "0" Then
        '    whr = " and INVML.ML002='" & wh & "'"
        'End If
        'Dim tempTable As String = "TempInventoryListAmount" & Session("UserName")
        'genItemPrice(tempTable)
        'Dim rateUSD As Decimal = 0
        'Dim rateEUR As Decimal = 0
        'If tbUSD.Text = "" Then
        '    rateUSD = CDec(tbUSD.Text.Trim)
        'End If
        'If tbEUR.Text = "" Then
        '    rateEUR = CDec(tbEUR.Text.Trim)
        'End If
        'Dim filePrint As String = "InventoryPriceList.rpt",
        '    paraName As String = "rateUSD:" & rateUSD & ",rateEUR:" & rateEUR & ",tempTable:" & tempTable & ",whr:" & whr
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=" & filePrint & "&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub
End Class