Public Class InvenStatusdetail
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            btExport.Visible = False

        End If


    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim tempTable As String = "tempInvenStatusDetail" & Session("UserName")
        CreateTempTable.createTempInvenStatusDetail(tempTable)


        'If txtItem.Text.Trim <> "" Then

        '    SearchBy(tempTable)

        'End If

        SearchBy(tempTable)

        Dim SQL As String = "",
            WHR As String = ""

        SQL = " select T.item as IN01, INVMB.MB003 as IN02, T.SOUndel as IN03, " & _
           " T.WH2101 as IN04,INVMC.MC015 as IN05, " & _
           " T.WH2600 as IN06,T.WH2301 as IN07, " & _
           " T.CusCode as IN08" & _
           " from " & tempTable & " T " & _
           " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001 = T.item" & _
           " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001 = T.item and INVMC.MC002 = INVMB.MB017" & _
           " left join JINPAO80.dbo.COPMG COPMG on COPMG.MG002 = T.item and COPMG.MG001 = T.CusCode" & _
           " where INVMC.MC007 > '0'  and INVMC.MC002 like '2101'" & _
           " order by T.item,T.CusCode "


        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)

        lbCount.Text = ControlForm.rowGridview(gvShow)

        btExport.Visible = True

        System.Threading.Thread.Sleep(1000)

    End Sub
    Protected Sub SearchBy(tempTable As String)

        Dim SQL As String = "",
            WHR As String = "",
            USQL As String = ""

        Dim Program As New DataTable
        Dim item As String = "",
            Qty As Integer = 0

        If txtDate.Text <> "" Then

            WHR = WHR & Conn_SQL.Where("COPTD.TD013 = ", configDate.dateFormat2(txtDate.Text.Trim))

        End If

        WHR = WHR & Conn_SQL.Where("COPTC.TC004", txtCusCode, False)
       
        WHR = WHR & Conn_SQL.Where("INVMB.MB001", txtItem)
        
        WHR = WHR & Conn_SQL.Where("INVMB.MB003", txtSpec)

        'SO Undelv.

        SQL = " select INVMB.MB001 as item,sum(COPTD.TD008-COPTD.TD009) as SOUndel from INVMB " & _
                   " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017" & _
                   " left join COPMG on COPMG.MG002 = INVMB.MB001 and COPMG.MG004 = INVMB.MB002" & _
                   " left join COPMA on COPMA.MA001 = COPMG.MG001 " & _
                   " left join COPTD on COPTD.TD004 = INVMB.MB001 and COPTD.TD005 = INVMB.MB002 and COPTD.TD006=INVMB.MB003" & _
                   " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 and COPTC.TC004=COPMG.MG001" & _
                   " where COPTC.TC027 = 'Y' and COPTD.TD016 not in ('y','Y')" & WHR & _
                   " group by INVMB.MB001 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("SOUndel")

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "'  ) " & _
                   " update " & tempTable & " set SOUndel='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,SOUndel)values ('" & item & "','" & Qty & "')"

            ' USQL = " insert into " & tempTable & "(item,SOUndel)values ('" & item & "','" & Qty & "')"

            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next

        'WH2101

        SQL = " select INVMB.MB001 as item, sum(isnull(INVMC.MC007,0)) as WH2101 from INVMB " & _
                   " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017" & _
                   " left join COPMG on COPMG.MG002 = INVMB.MB001 and COPMG.MG004 = INVMB.MB002" & _
                   " left join COPMA on COPMA.MA001 = COPMG.MG001 " & _
                   " left join COPTD on COPTD.TD004 = INVMB.MB001 and COPTD.TD005 = INVMB.MB002 and COPTD.TD006=INVMB.MB003" & _
                   " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 and COPTC.TC004=COPMG.MG001" & _
                   " where INVMC.MC007 > '0' and INVMC.MC002 like '2101'" & _
                   " and COPTC.TC027 = 'Y' and COPTD.TD016 not in ('y','Y')" & WHR & _
                   " group by INVMB.MB001 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("WH2101")

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "'  ) " & _
                   " update " & tempTable & " set WH2101='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,WH2101)values ('" & item & "','" & Qty & "')"

            'USQL = " insert into " & tempTable & "(item,WH2101)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next

        'WH2600

        SQL = " select INVMB.MB001 as item, sum(isnull(INVMC.MC007,0)) as WH2600 from INVMB " & _
            " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017" & _
            " left join COPMG on COPMG.MG002 = INVMB.MB001 and COPMG.MG004 = INVMB.MB002" & _
            " left join COPMA on COPMA.MA001 = COPMG.MG001 " & _
            " left join COPTD on COPTD.TD004 = INVMB.MB001 and COPTD.TD005 = INVMB.MB002 and COPTD.TD006=INVMB.MB003" & _
            " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 and COPTC.TC004=COPMG.MG001" & _
            " where INVMC.MC007 > '0' and INVMC.MC002 like '2600'" & _
            " and COPTC.TC027 = 'Y' and COPTD.TD016 not in ('y','Y')" & WHR & _
            " group by INVMB.MB001 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("WH2600")

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "'  ) " & _
                   " update " & tempTable & " set WH2600='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,WH2600)values ('" & item & "','" & Qty & "')"

            ' USQL = " insert into " & tempTable & "(item,WH2600)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next

        'WH2301

        SQL = " select INVMB.MB001 as item,sum(isnull(INVMC.MC007,0)) as WH2301 from INVMB " & _
           " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017" & _
           " left join COPMG on COPMG.MG002 = INVMB.MB001 and COPMG.MG004 = INVMB.MB002" & _
           " left join COPMA on COPMA.MA001 = COPMG.MG001 " & _
           " left join COPTD on COPTD.TD004 = INVMB.MB001 and COPTD.TD005 = INVMB.MB002 and COPTD.TD006=INVMB.MB003" & _
           " left join COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 and COPTC.TC004=COPMG.MG001" & _
           " where INVMC.MC007 > '0' and INVMC.MC002 like '2301'" & _
           " and COPTC.TC027 = 'Y' and COPTD.TD016 not in ('y','Y')" & WHR & _
           " group by INVMB.MB001 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("WH2301")

            USQL = " if exists(select * from " & tempTable & " where item='" & item & "'  ) " & _
                   " update " & tempTable & " set WH2301='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,WH2301)values ('" & item & "','" & Qty & "')"

            ' USQL = " insert into " & tempTable & "(item,WH2301)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)

        Next

        Dim txt As String = "",
            lstCode As String = ""

        SQL = " select INVMB.MB001,COPTC.TC004,SUM(isnull(COPTD.TD008,0)-isnull(COPTD.TD009,0)) from JINPAO80.dbo.INVMB INVMB  " & _
     " left join JINPAO80.dbo.COPMG COPMG on COPMG.MG002 = INVMB.MB001 and COPMG.MG004 = INVMB.MB002 " & _
     " left join JINPAO80.dbo.COPTD COPTD on COPTD.TD004 = INVMB.MB001 and COPTD.TD005 = INVMB.MB002 and COPTD.TD006=INVMB.MB003" & _
     " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001 = COPTD.TD001 and COPTC.TC002 = COPTD.TD002 and COPTC.TC004=COPMG.MG001" & _
     " where COPTC.TC027 = 'Y' and COPTD.TD016 not in ('y','Y')" & WHR & _
     " group by INVMB.MB001,COPTC.TC004 order by INVMB.MB001,COPTC.TC004"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("MB001")
            Dim cust As String = Program.Rows(i).Item("TC004")
            If lstCode <> item And lstCode <> "" Then
                USQL = " update " & tempTable & " set CusCode='" & txt.Substring(0, txt.Length - 1) & "' where item='" & lstCode & "' "
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                txt = ""
            End If
            txt = txt & cust & ","
            lstCode = item
        Next
        If txt <> "" Then
            USQL = " update " & tempTable & " set CusCode='" & txt.Substring(0, txt.Length - 1) & "' where item='" & lstCode & "' "
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        End If

    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("IN01")) Then
                    Dim link As String = ""
                    'Dim EndDate As String = configDate.dateFormat2(tbEndDate.Text)
                    Dim jpPart As String = .DataItem("IN01")
                    link = link & "&JPPart= " & jpPart
                    link = link & "&JPSpec= " & .DataItem("IN02")
                    link = link & "&SOUndel= " & .DataItem("IN03")
                    link = link & "&WH2101= " & .DataItem("IN04")
                    link = link & "&Bin= " & .DataItem("IN05")
                    link = link & "&WH2600= " & .DataItem("IN06")
                    link = link & "&WH2301= " & .DataItem("IN07")
                    link = link & "&CusCode= " & .DataItem("IN08")
  
                    hplDetail.NavigateUrl = "InventoryStatusDetailPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)

                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("InventoryStatusDetail" & Session("UserName"), gvShow)
    End Sub

    Protected Sub gvShow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvShow.SelectedIndexChanged

    End Sub
End Class