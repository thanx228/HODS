Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Globalization
Imports MIS_HTI.DataControl

Public Class WorkStatus
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    ' Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim dconn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showDDL(ddlWorkType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            lbShow.Text = "0"
            btExport.Visible = False
            
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click

        gvShow.DataSource = ""
        gvShow.DataBind()
        Dim SQL As String = genSQL(True)
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim maxWC As Integer = 1

        If Program.Rows.Count > 0 Then
            maxWC = Program.Rows(0).Item("maxProcess")
        End If

        Dim dt As Data.DataTable = New DataTable
        dt.Columns.Add(New DataColumn("Start / Finished"))
        dt.Columns.Add(New DataColumn("Spec / Cus. Name"))
        dt.Columns.Add(New DataColumn("Item"))
        dt.Columns.Add(New DataColumn("MO No."))
        dt.Columns.Add(New DataColumn("SO No."))
        dt.Columns.Add(New DataColumn("MO / SO Qt'y"))
        dt.Columns.Add(New DataColumn("Msg2"))
        dt.Columns.Add(New DataColumn("First Process"))
        dt.Columns.Add(New DataColumn("Qt'y"))

        For i As Integer = 1 To maxWC
            'Dim strWC As String = setNameWC(i)
            'Dim StrQty As String = setNameQTY(i)
            dt.Columns.Add(New DataColumn(setNameWC(i)))
            dt.Columns.Add(New DataColumn(setNameQTY(i)))
        Next

        SQL = genSQL()
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim lastWO As String = ""
        Dim dr1 As DataRow,
            dr2 As DataRow,
            dr3 As DataRow
        Dim wcSeq As Integer,
            scrapQty As Integer,
            sQty As Integer = 0
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim WO As String = Program.Rows(i).Item("WO")

            If lastWO <> WO Then
                sQty = 0
                'scrapQty = 0
                If lastWO <> "" Then
                    setGrid(dt, dr1, dr2, dr3, wcSeq, maxWC)
                End If
                'Row DOC
                dr1 = dt.NewRow()
                dr1("Start / Finished") = Program.Rows(i).Item("planDate")
                dr1("Spec / Cus. Name") = Program.Rows(i).Item("Spec")
                dr1("Item") = Program.Rows(i).Item("Item")
                dr1("MO No.") = WO
                'Dim soSeq As String = Program.Rows(i).Item("soSeq")
                'If soSeq = "    " Then
                '    soSeq = 0
                'End If
                'Dim xsoSeq As String = Convert.ToDecimal(soSeq)
                'Dim strSeq As String = ""
                'If xsoSeq <> 0 Then
                '    strSeq = "-" & FormatNumber(xsoSeq, 0, TriState.True)
                'End If
                'dr1("SO No.") = Program.Rows(i).Item("SO") & strSeq
                dr1("SO No.") = Program.Rows(i).Item("SO")

                'MO Qty
                Dim woQty As String = Program.Rows(i).Item("woQty")
                'SO Qty
                Dim soQty As String = Program.Rows(i).Item("soQty")
                Dim xsoQty As Integer
                If soQty = "    " Then
                    xsoQty = 0
                Else
                    xsoQty = Convert.ToDecimal(soQty)
                End If
                dr1("MO / SO Qt'y") = FormatNumber(woQty, 0, TriState.True)
                dr1("Msg2") = "WC"
                dr1("First Process") = ""
                dr1("Qt'y") = ""

                'Row Date
                dr2 = dt.NewRow()
                dr2("Start / Finished") = Program.Rows(i).Item("finishDate")
                dr2("Spec / Cus. Name") = Program.Rows(i).Item("CustName")
                dr2("Item") = Program.Rows(i).Item("Item")

                'Dim soSeq As String = Program.Rows(i).Item("soSeq")
                'If soSeq = "    " Then
                '    soSeq = 0
                'End If
                'Dim xsoSeq As String = Convert.ToDecimal(soSeq)
                'Dim strSeq As String = ""
                'If xsoSeq <> 0 Then
                '    strSeq = "-" & FormatNumber(xsoSeq, 0, TriState.True)
                'End If
                dr2("MO No.") = WO
                'dr2("SO No.") = Program.Rows(i).Item("SO") & strSeq
                dr2("SO No.") = Program.Rows(i).Item("SO")
                dr2("MO / SO Qt'y") = FormatNumber(xsoQty, 0, TriState.True)
                dr2("Msg2") = "Date"
                dr2("First Process") = ""
                dr2("Qt'y") = ""

                'Row ว่าง
                dr3 = dt.NewRow()
                dr3("Start / Finished") = ""
                dr3("Spec / Cus. Name") = ""
                dr3("Item") = ""
                dr3("MO No.") = ""
                dr3("SO No.") = ""
                dr3("MO / SO Qt'y") = ""
                dr3("Msg2") = ""
                dr3("First Process") = ""
                dr3("Qt'y") = ""
                wcSeq = 1

            End If
            Dim transQty As Integer = Program.Rows(i).Item("transQty")
            scrapQty = Program.Rows(i).Item("scrapQty")
            Dim balQty As Integer = Program.Rows(i).Item("balQty")
            If balQty > 0 And sQty > 0 Then
                balQty = balQty - sQty
                If balQty < 0 Then
                    balQty = Program.Rows(i).Item("balQty")
                End If
            End If

            sQty += scrapQty
            'WO1 Process....(W01,W02,W23,W27)
            Dim WC As String = Program.Rows(i).Item("WC").ToString.Trim
            If dr1("First Process") = "" And (WC = "W01" Or WC = "W02" Or WC = "W23" Or WC = "W27") Then
                dr1("First Process") = Program.Rows(i).Item("Process")
                dr1("Qt'y") = FormatNumber(balQty, 0, TriState.True) & "  (" & Program.Rows(i).Item("scrapQty") & ")"
            End If
            'sQty = Program.Rows(i).Item("scrapQty")
            Dim strWC As String = setNameWC(wcSeq)
            Dim StrQty As String = setNameQTY(wcSeq)
            dr1(strWC) = Program.Rows(i).Item("Process")
            dr1(StrQty) = FormatNumber(balQty, 0, TriState.True) & "  (" & Program.Rows(i).Item("scrapQty") & ")"
            Dim xscrapQty As String = ""
            If sQty > 0 Then
                xscrapQty = "(" & FormatNumber(sQty, 0, TriState.True) & ")"
            End If
            dr2(strWC) = Program.Rows(i).Item("planDate")
            dr2(StrQty) = ""
            'dr2(strWC) = Program.Rows(i).Item("WIP") & "  (" & Program.Rows(i).Item("scrapQty") & ")"
            dr3(strWC) = ""
            dr3(StrQty) = ""
            'dr4(strWC) = Program.Rows(i).Item("planDate")
            'dr5(strWC) = ""
            lastWO = WO
            wcSeq += 1

            'Dim strQty As String = setNameQTY(wcSeq)
            'dr1(strQty) = FormatNumber(balQty, 0, TriState.True) & "  (" & Program.Rows(i).Item("scrapQty") & ")"
            'dr2(strQty) = ""
            ''dr2(strWC) = Program.Rows(i).Item("WIP") & "  (" & Program.Rows(i).Item("scrapQty") & ")"
            'dr3(strQty) = ""
            ''dr4(strWC) = Program.Rows(i).Item("planDate")
            ''dr5(strWC) = ""
            'lastWO = WO
            'wcSeq += 1
        Next
        If Program.Rows.Count > 0 Then
            setGrid(dt, dr1, dr2, dr3, wcSeq, maxWC)
            gvShow.DataSource = dt
            gvShow.DataBind()
        End If
        lbShow.Text = gvShow.Rows.Count / 3
                btExport.Visible = True
                System.Threading.Thread.Sleep(1000)
    End Sub


    Sub setGrid(ByRef dt As DataTable, ByVal dr1 As DataRow, ByVal dr2 As DataRow, ByVal dr3 As DataRow, ByVal wcSeq As Integer, ByVal maxwc As Integer)

        For i As Integer = wcSeq To maxwc
            Dim strWC As String = setNameWC(i)
            Dim qtyWC As String = setNameQTY(i)
            dr1(strWC) = ""
            dr1(qtyWC) = ""

            dr2(strWC) = ""
            dr2(qtyWC) = ""

            dr3(strWC) = ""
            dr3(qtyWC) = ""

            'dr4(strWC) = ""
            'dr5(strWC) = ""
        Next

        dt.Rows.Add(dr1)
        dt.Rows.Add(dr2)
        dt.Rows.Add(dr3)
        'dt.Rows.Add(dr4)
        'dt.Rows.Add(dr5)
    End Sub


    Function setNameWC(ByVal wcSeq As Integer) As String
        Dim strWC As String = "W" & wcSeq
        If wcSeq.ToString.Length = 1 Then
            strWC = "W0" & wcSeq
        End If

        Return strWC

    End Function


    Function setNameQTY(ByVal wcSeq As Integer) As String
        Dim strWC As String = "Qt'y" & wcSeq
        If wcSeq.ToString.Length = 1 Then
            strWC = "Qt'y" & wcSeq
        End If

        Return strWC

    End Function

    Function genSQL(Optional ByVal maxWC As Boolean = False) As String

        Dim CustID = tbCust.Text, PartNo = tbPart.Text, PartSpec = tbSpec.Text
        Dim woType = ddlWorkType.Text, woNo = tbWorkNo.Text, statusCode = ddlStatusCode.Text
        Dim soType = ddlSaleType.Text, soNo = tbSaleNo.Text, soSeq = tbSaleSeq.Text
        Dim strDate As String = "", endDate As String = "", SQL As String = "", WHR As String = ""

        If CustID <> "" Then
            WHR = WHR & " and COPTC.TC004 ='" & CustID & "' "
        End If
        If PartNo <> "" Then
            WHR = WHR & " and MOCTA.TA006 like '%" & PartNo & "%' "
        End If
        If PartSpec <> "" Then
            WHR = WHR & " and MOCTA.TA035 like '%" & PartSpec & "%' "
        End If

        If woType <> "" And woType <> "ALL" Then
            WHR = WHR & " and SFCTA.TA001 = '" & woType & "' "
        End If
        If woNo <> "" Then
            WHR = WHR & " and SFCTA.TA002 = '" & woNo & "' "
        End If
        If soType <> "" And soType <> "ALL" Then
            WHR = WHR & " and MOCTA.TA026 = '" & soType & "' "
        End If
        If soNo <> "" Then
            WHR = WHR & " and MOCTA.TA027 = '" & soNo & "' "
        End If
        If soSeq <> "" Then
            WHR = WHR & " and MOCTA.TA028 like '%" & soSeq & "%' "
        End If
        If statusCode <> "0" Then
            Dim txt As String = " in "
            If statusCode = "2" Then
                txt = " not in "
            End If
            WHR = WHR & " and MOCTA.TA011 " & txt & " ('y','Y') "
        End If


        Dim fldDate As String = ""
        Select Case ddlDateType.Text
            Case "0"
                fldDate = "COPTD.TD013" 'delivery date
            Case "1"
                fldDate = "COPTC.TC003" 'sale date
            Case "2"
                fldDate = "MOCTA.TA003" 'MO date
            Case "3"
                fldDate = "MOCTA.TA009" 'Plan Start date
            Case "4"
                fldDate = "MOCTA.TA010" 'Plan Finish date
        End Select
        WHR &= dconn.WHERE_DATE(fldDate, UcDateFrom.Text, UcDateTo.Text, "0")

        If maxWC = False Then
            SQL = " select case when MOCTA.TA026='' then '' else MOCTA.TA026+'-'+MOCTA.TA027+'-'+MOCTA.TA028 end as SO,MOCTA.TA028 as soSeq, isnull(MOCTA.TA008,0) as soQty," &
                  " (SUBSTRING(COPTD.TD013,7,2))+'-'+(SUBSTRING(COPTD.TD013,5,2))+'-'+(SUBSTRING(COPTD.TD013,1,4))as delDate, " &
                  " (SUBSTRING(COPTC.TC003,7,2))+'-'+(SUBSTRING(COPTC.TC003,5,2))+'-'+(SUBSTRING(COPTC.TC003,1,4))as soDate, " &
                  " COPTC.TC004 as CustID ,COPMA.MA002 as CustName ,SFCTA.TA001+'-'+SFCTA.TA002 as WO," &
                  " convert(int,(SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA056-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048)) as WIP," &
                  " (SUBSTRING(MOCTA.TA003,7,2))+'-'+(SUBSTRING(MOCTA.TA003,5,2))+'-'+(SUBSTRING(MOCTA.TA003,1,4))as woDate, isnull(MOCTA.TA015,0) as woQty," &
                  " (SUBSTRING(SFCTA.TA009,7,2))+'-'+(SUBSTRING(SFCTA.TA009,5,2))+'-'+(SUBSTRING(SFCTA.TA009,1,4))as planDate,  " &
                  " (SUBSTRING(MOCTA.TA010,7,2))+'-'+(SUBSTRING(MOCTA.TA010,5,2))+'-'+(SUBSTRING(MOCTA.TA010,1,4))as finishDate,  " &
                  " MOCTA.TA006 as PartNo,MOCTA.TA035 as Spec,SFCTA.TA003 as woSeq,CMSMW.MW002 as Process,  " &
                  " SFCTA.TA010 as inputQty,SFCTA.TA011 as transQty,convert(int,SFCTA.TA012+SFCTA.TA056) as scrapQty, " &
                  " MOCTA.TA015+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048-SFCTA.TA058-SFCTA.TA012-SFCTA.TA056 as balQty," &
                  " MOCTA.TA011 as statusCode, " &
                  " SFCTA.TA034 as remark, SFCTA.TA006 as WC ,(SUBSTRING(MOCTA.TA006,1,14))+'-'+(SUBSTRING(MOCTA.TA006,15,2))  as Item " &
                  " from SFCTA " &
                  " left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " &
                  " left join COPTC on COPTC.TC001=MOCTA.TA026 and COPTC.TC002=MOCTA.TA027 " &
                  " left join COPTD on COPTD.TD001=MOCTA.TA026 and COPTD.TD002=MOCTA.TA027  and COPTD.TD003=MOCTA.TA028 " &
                  " left join CMSMW on CMSMW.MW001=SFCTA.TA004 " &
                  " left join COPMA on COPMA.MA001=COPTC.TC004 " &
                  " where " & WHR.Substring(4) &
                  " order by MOCTA.TA011,SFCTA.TA001,SFCTA.TA002,MOCTA.TA026,MOCTA.TA027,MOCTA.TA028,SFCTA.TA003 "
            '& " and MOCTA.TA011 not in('y','Y')  "  
        Else
            SQL = " select top 1 count(SFCTA.TA003) as maxProcess from SFCTA " &
                  " left join MOCTA on MOCTA.TA001=SFCTA.TA001 and MOCTA.TA002=SFCTA.TA002 " &
                  " left join COPTD on COPTD.TD001=MOCTA.TA026 and COPTD.TD002=MOCTA.TA027  and COPTD.TD003=MOCTA.TA028 " &
                  " left join COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " &
                  " where " & WHR.Substring(4) &
                  " group by MOCTA.TA026,MOCTA.TA027,SFCTA.TA001,SFCTA.TA002  order by maxProcess desc "
        End If
        Return SQL
    End Function

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("WorkStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

End Class