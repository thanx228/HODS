Public Class SaleOrderTest2
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            btExport.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempSaleOrderTest2" & Session("UserName")
        CreateTempTable.createTempSaleOrderTest2(tempTable)
        'If txtItem.Text.Trim <> "" Then

        'End If
        SearchBy(tempTable)
        Dim WHR As String = "",
            SQL As String = ""

        Select Case ddlCon.Text
            Case "0"
                WHR = WHR & " COPTD.TD008 <> MOCTA.TA015 "
            Case "1"
                WHR = WHR & " COPTD.TD008 < MOCTA.TA015 "
            Case "2"
                WHR = WHR & " COPTD.TD008 > MOCTA.TA015 "
        End Select

        SQL = " select TD004 as 'S03',INVMB.MB002 as 'S04',INVMB.MB003 as 'S05'," & _
            " COPTC.TC039 as 'S06',COPTD.TD013 as 'S07' ,  COPTC.TC004 +'-'+ COPMA.MA002 as 'S01', " & _
            " COPTC.TC001 as 'S02',COPTC.TC002 as 'S10',COPTD.TD003 as 'S11', " & _
            " COPTD.TD008 as 'S08',T.moQty as 'S09' " & _
            " from " & tempTable & " T " & _
            " left join JINPAO80.dbo.COPTD on TD001=T.SaleType and TD002=T.SaleNo and TD003=T.SaleSeq " & _
            " left join JINPAO80.dbo.COPTC on TC001=TD001 and TC002=TD002 " & _
            " left join JINPAO80.dbo.COPMA on COPMA.MA001 = COPTC.TC004 " & _
            " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=COPTD.TD004 " & _
            " order by COPTC.TC001,COPTC.TC002,COPTC.TC003 "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        '  ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)


    End Sub


    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("S01")) Then
                    Dim link As String = ""
                    'Dim EndDate As String = configDate.dateFormat2(tbEndDate.Text)
                    Dim jpPart As String = .DataItem("S03")
                    link = link & "&JPPart= " & jpPart
                    link = link & "&JPDesc= " & .DataItem("S04")
                    link = link & "&JPSpec= " & .DataItem("S05")
                    link = link & "&Cus= " & .DataItem("S01")
                    link = link & "&SOdetail= " & .DataItem("S02")
                    link = link & "&Ordate= " & .DataItem("S06")
                    link = link & "&Deldate= " & .DataItem("S07")
                    link = link & "&soQty= " & .DataItem("S08")
                    link = link & "&moQty= " & .DataItem("S09")
                    link = link & "&soNo= " & .DataItem("S10")
                    link = link & "&soSeq= " & .DataItem("S11")
                    'link = link & "&endDate= " & configDate.dateFormat2(txtDateTo.Text)
                    'link = link & "&fromDate= " & configDate.dateFormat2(txtDateFrom.Text)
                    hplDetail.NavigateUrl = "SaleOrderPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With


    End Sub

    Protected Sub SearchBy(tempTable As String)

        Dim SQL As String = "",
            WHR As String = "",
            WHRso As String = "",
            USQL As String = "",
            Have As String = ""

        Dim CodeItem As String = txtItem.Text.Trim,
            Desc As String = txtDesc.Text.Trim,
            Spec As String = txtSpec.Text.Trim

        Dim Program As New DataTable
        Dim item As String = "",
            Qty As Integer = 0
        'MO
        Select Case ddlCon.Text
            Case "0"
                Have = Have & " sum(COPTD.TD008) <> sum(MOCTA.TA015) "
            Case "1"
                Have = Have & " sum(COPTD.TD008) < sum(MOCTA.TA015) "
            Case "2"
                Have = Have & " sum(COPTD.TD008) > sum(MOCTA.TA015) "
        End Select

        WHR = WHR & Conn_SQL.Where("MOCTA.TA006", txtItem) 'Item
        WHR = WHR & Conn_SQL.Where("MOCTA.TA034", txtDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("MOCTA.TA035", txtSpec) 'Spec
        WHR = WHR & Conn_SQL.Where("MOCTA.TA026", txtSOtype)
        WHR = WHR & Conn_SQL.Where("MOCTA.TA027", txtSONo)
        WHR = WHR & Conn_SQL.Where(ddlFldDate.Text, configDate.dateFormat2(txtDateFrom.Text.Trim), configDate.dateFormat2(txtDateTo.Text.Trim))
        SQL = " select MOCTA.TA026,MOCTA.TA027,MOCTA.TA028, sum(MOCTA.TA015) as moQty from MOCTA " & _
              " left join COPTD on COPTD.TD001=MOCTA.TA026 and COPTD.TD002=MOCTA.TA027 and COPTD.TD003=MOCTA.TA028 and COPTD.TD004 = MOCTA.TA006 " & _
              " left join COPTC on TC001=TD001 and TC002=TD002  " & _
              " where TC027='Y'  " & WHR & _
              " group by MOCTA.TA026,MOCTA.TA027,MOCTA.TA028 having  " & Have & _
              " order by MOCTA.TA026,MOCTA.TA027,MOCTA.TA028"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString) 'and TD016='N'
        For i As Integer = 0 To Program.Rows.Count - 1
            'item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("moQty")
            USQL = " insert into " & tempTable & "(SaleType,SaleNo,SaleSeq,moQty)values ('" & Program.Rows(i).Item("TA026") & "','" & Program.Rows(i).Item("TA027") & "','" & Program.Rows(i).Item("TA028") & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next



        ''SO

        'Select Case ddlCon.Text
        '    Case "0"
        '        WHRso = WHRso & " COPTD.TD008 <> MOCTA.TA015 "
        '    Case "1"
        '        WHRso = WHRso & " COPTD.TD008 < MOCTA.TA015 "
        '    Case "2"
        '        WHRso = WHRso & " COPTD.TD008 > MOCTA.TA015 "
        'End Select

        'WHR = Conn_SQL.Where("COPTC.TC039", "", txtOrDate)
        'WHR = Conn_SQL.Where("COPTD.TD013", "", txtDelDate)
        'WHR = WHR & Conn_SQL.Where("COPTD.TD004", txtItem) 'Item
        'WHR = WHR & Conn_SQL.Where("COPTD.TD005", txtDesc) 'Desc
        'WHR = WHR & Conn_SQL.Where("COPTD.TD006", txtSpec) 'Spec
        'WHR = WHR & Conn_SQL.Where("COPTC.TC001", txtSOtype)
        'WHR = WHR & Conn_SQL.Where("COPTC.TC002", txtSONo)



        'SQL = " select COPTD.TD004 as item,COPTD.TD008 as soQty" & _
        '    " from COPTD " & _
        '    " left join COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
        '    " left join MOCTA on MOCTA.TA026 = COPTC.TC001 and MOCTA.TA027 = COPTC.TC002 and MOCTA.TA006 = COPTD.TD004 " & _
        '    " where " & WHRso & WHR & _
        '    " order by COPTD.TD004 "





        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'For i As Integer = 0 To Program.Rows.Count - 1
        '    item = Program.Rows(i).Item("item")
        '    Qty = Program.Rows(i).Item("soQty")

        '    USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
        '           " update " & tempTable & " set soQty='" & Qty & "' where item='" & item & "' else " & _
        '           " insert into " & tempTable & "(item,soQty)values ('" & item & "','" & Qty & "')"
        '    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        'Next


    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SaleOrderTest2" & Session("UserName"), gvShow)
    End Sub


End Class