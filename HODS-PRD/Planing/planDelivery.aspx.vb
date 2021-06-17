Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Globalization

Public Class planDelivery
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            ControlForm.showDDL(ddlOrdType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            'Dim listData = New Hashtable
            'listData.Add("5", " Sale OK ")
            'listData.Add("4", " Stock=0,Delivery>0 ")
            'listData.Add("3", " Delivery=0,Stock>0 ")
            'listData.Add("2", " Delivery Over Stock ")
            'listData.Add("1", " Stock Over Delivery ")
            'listData.Add("0", " All ")

            'ddlConSel.DataSource = listData
            'ddlConSel.DataValueField = "Key"
            'ddlConSel.DataTextField = "Value"
            'ddlConSel.DataBind()
            'ddlConSel.SelectedIndex = 1
            'listData.Clear()

            'ddlConSel.Items.FindByValue("Not Select").Selected = True
            'ddlConSel.Items.= "Not Select"

            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempPlanDelivery" & Session("UserName")
        CreateTempTable.createTempPlanDelivery(tempTable)
        Dim SQL As String = "", ISQL As String = ""
        Dim having As String = "", whr As String = "", whr2 As String = ""

        If ddlOrdType.Text <> "" And ddlOrdType.Text <> "ALL" Then
            whr = whr & " and COPTD.TD001='" & ddlOrdType.Text & "' "
        End If
        If tbCust.Text <> "" Then
            whr = whr & " and COPTC.TC004='" & tbCust.Text & "' "
            'whr2 = whr2 & " and COPTC.TC004='" & tbCust.Text & "' "
        End If
        If tbPart.Text <> "" Then
            whr = whr & " and COPTD.TD004 like '%" & tbPart.Text & "%' "
            whr2 = whr2 & " and T.item like '%" & tbPart.Text & "%' "
        End If
        If tbSpec.Text <> "" Then
            whr = whr & " and COPTD.TD006 like '%" & tbSpec.Text & "%' "
            whr2 = whr2 & " and COPTD.TD006 like '%" & tbSpec.Text & "%' "
        End If

        'sale order detail  ,0,0,0   ,moQty,poQty,onhandQty
        SQL = " select COPTD.TD004,SUM(COPTD.TD008-COPTD.TD009)  from JINPAO80.dbo.COPTD COPTD " & _
             " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
             " where COPTD.TD016='N' and COPTC.TC027='Y' " & whr & " group by COPTD.TD004  order by COPTD.TD004"
        ISQL = "insert into " & tempTable & "(item,delQty) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        Dim Program As New DataTable
        Dim USQL As String = "", item As String = ""
        Dim Qty As Decimal = 0

        If (ddlOrdType.Text <> "" And ddlOrdType.Text <> "ALL") Or tbCust.Text <> "" Then
            whr = ""
            If ddlOrdType.Text <> "" And ddlOrdType.Text <> "ALL" Then
                whr = whr & " and COPTD.TD001 not in ('" & ddlOrdType.Text & "') "
            End If
            If tbCust.Text <> "" Then
                whr = whr & " and COPTC.TC004 not in('" & tbCust.Text & "') "
            End If

            SQL = " select T.item as item,SUM(COPTD.TD008-COPTD.TD009) as del from " & tempTable & " T " & _
                   "left join JINPAO80.dbo.COPTD COPTD on COPTD.TD004=T.item " & _
                  " left join JINPAO80.dbo.COPTC COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
                  " where COPTD.TD016='N' " & whr2 & whr & " group by T.item  order by T.item "
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            For i As Integer = 0 To Program.Rows.Count - 1
                item = Program.Rows(i).Item("item")
                Qty = Program.Rows(i).Item("del")
                USQL = " update " & tempTable & " set delQty=delQty+'" & Qty & "' where item='" & item & "'  "
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            Next
        End If

        'check PR
        whr = ""
        If ddlOrdType.Text <> "" And ddlOrdType.Text <> "ALL" Then
            whr = whr & " and substring(B.TB029,1,4) in('" & ddlOrdType.Text & "','') "
        End If

        If tbPart.Text <> "" Then
            whr = whr & " and B.TB004 like '%" & tbPart.Text & "%' "
        End If
        If tbSpec.Text <> "" Then
            whr = whr & " and B.TB006 like '%" & tbSpec.Text & "%' "
        End If
        SQL = " select B.TB004 as item,sum(R.TR006) as pr from PURTR R left join PURTB B on B.TB001=R.TR001 and B.TB002=R.TR002 and TB003=TR003 where R.TR019='' and B.TB039='N'  " & whr & " group by B.TB004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'check PO 
        whr = ""
        If tbPart.Text <> "" Then
            whr = whr & " and TD004 like '%" & tbPart.Text & "%' "
        End If
        If tbSpec.Text <> "" Then
            whr = whr & " and TD006 like '%" & tbSpec.Text & "%' "
        End If

        SQL = " select TD004 as item,SUM(isnull(TD008,0)-isnull(TD015,0)) as po from PURTD where TD016='N' and SUBSTRING(TD004,3,1) in ('2','3')   " & whr & " group by TD004  order by TD004 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set poQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,poQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'check MO
        whr = ""
        'If ddlOrdType.Text <> "" And ddlOrdType.Text <> "ALL" Then
        '    ' whr = whr & " and TA026 = '" & ddlOrdType.Text & "' "
        '    'whr = whr & " and TA026 in('" & ddlOrdType.Text & "','') "
        'End If

        If tbPart.Text <> "" Then
            whr = whr & " and TA006 like '%" & tbPart.Text & "%' "
        End If

        If tbSpec.Text <> "" Then
            whr = whr & " and TA035 like '%" & tbSpec.Text & "%' "
        End If

        SQL = "select TA006 as item, SUM(isnull(TA015,0)-isnull(TA017,0)) as mo from MOCTA where TA011 not in('y','Y') and SUBSTRING(TA006,3,1) in ('2','3') " & whr & " group by TA006  order by TA006"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("mo")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        'issue mat only prefix=803
        If tbPart.Text <> "" Then
            whr = whr & " and TA006 like '%" & tbPart.Text & "%' "
        End If

        If tbSpec.Text <> "" Then
            whr = whr & " and TA035 like '%" & tbSpec.Text & "%' "
        End If

        'SQL = "select TB003 as item, SUM(isnull(TB004,0)-isnull(TB005,0)) as issue from MOCTB where SUBSTRING(TB003,3,1) ='3' and TB004-TB005>0 " & whr & " group by TB003  order by TB003"
        SQL = " select TB003 as item, SUM(isnull(TB004,0)-isnull(TB005,0)) as issue from MOCTB " & _
            " left join MOCTA on TA001=TB001 and  TA002=TB002 " & _
            " where SUBSTRING(TB003,3,1) ='3' and TB004-TB005>0 and TA011 not in('y','Y')  " & _
            " group by TB003  order by TB003 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("issue")
            USQL = " if exists(select * from " & tempTable & " where item='" & item & "' ) " & _
                   " update " & tempTable & " set issueQty='" & Qty & "' where item='" & item & "' else " & _
                   " insert into " & tempTable & "(item,issueQty)values ('" & item & "','" & Qty & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        Dim strBal As String = " INVMB.MB064+T.moQty+T.poQty+T.prQty "
        Dim valConsel As String = ddlConSel.SelectedValue.ToString
        whr = ""
        If valConsel <> "0" Then
            whr = " where "
            If valConsel = "1" Then  'Stock over Delivery
                whr = whr & strBal & ">T.delQty and T.delQty>0 "
            ElseIf valConsel = "2" Then 'Delivery Over Stock
                whr = whr & " T.delQty >" & strBal & " and T.delQty>0 "
            ElseIf valConsel = "3" Then  ' Delivery=0,Stock>0
                whr = whr & "T.delQty=0 and " & strBal & ">0 "
            ElseIf valConsel = "4" Then  'Stock=0,Delivery>0
                whr = whr & strBal & "=0 and T.delQty>0 "
            ElseIf valConsel = "5" Then  'Sale OK
                whr = whr & "INVMB.MB064>=T.delQty and T.delQty>0 "
            End If
        End If ',T.prQty as PR
        SQL = " select T.item as JP_Part,INVMB.MB003 as JP_SPEC,cast(T.delQty as decimal(8,0)) as Delivery,T.issueQty as MatISSUE,cast(INVMB.MB064 as decimal(8,0)) as Stock, " & _
              " T.moQty as MO,T.poQty as PO,T.prQty as PR from " & tempTable & " T left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & _
              " " & whr & " order by T.item"
        ControlForm.ShowGridView(GridView1, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = GridView1.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    'Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
    '    'If e.Row.RowType = DataControlRowType.Header Then
    '    '    e.Row.Cells(0).Text = "JP Part"
    '    '    e.Row.Cells(0).Width = 150
    '    '    e.Row.Cells(1).Text = "JP SPEC"
    '    '    e.Row.Cells(1).Width = 200
    '    '    e.Row.Cells(2).Text = "Delivery"
    '    '    e.Row.Cells(2).Width = 100
    '    '    e.Row.Cells(3).Text = "Stock"
    '    '    e.Row.Cells(3).Width = 100
    '    '    e.Row.Cells(4).Text = "MO"
    '    '    e.Row.Cells(4).Width = 100
    '    '    e.Row.Cells(5).Text = "PO"
    '    '    e.Row.Cells(5).Width = 100
    '    '    e.Row.Cells(6).Text = "PR"
    '    '    e.Row.Cells(6).Width = 100
    '    'End If
    'End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
 
            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
            If Not IsNothing(hplDetail) And Not IsDBNull(e.Row.DataItem("JP_Part")) Then
                Dim link As String = ""
                Dim ordType As String = ""
                If ddlOrdType.Text <> "ALL" Then
                    ordType = ddlOrdType.Text
                End If

                'Dim date1 As String = tbDateFrom.Text, date2 As String = tbDateTo.Text

                'Dim strDate As String = "", endDate As String
                ''Begin date
                'If date1 <> "" Then
                '    strDate = configDate.dateFormat(date1)
                'End If
                ''End date
                'If date2 <> "" Then
                '    endDate = configDate.dateFormat(date2)
                'Else
                '    endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
                'End If

                link = link & "&saleType= " & ordType
                link = link & "&custID= " & tbCust.Text
                'link = link & "&dateFrom= " & strDate
                'link = link & "&dateTo= " & endDate
                link = link & "&JPPart= " & e.Row.DataItem("JP_Part")
                link = link & "&JPSpec= " & e.Row.DataItem("JP_SPEC")
                link = link & "&stock= " & e.Row.DataItem("Stock")
                link = link & "&delQty= " & e.Row.DataItem("Delivery")
                link = link & "&poQty= " & e.Row.DataItem("PO")
                link = link & "&moQty= " & e.Row.DataItem("MO")
                hplDetail.NavigateUrl = "planDeliveryPopup.aspx?height=150&width=350" & link
                hplDetail.Attributes.Add("title", e.Row.DataItem("JP_Part"))
            End If
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub


End Class