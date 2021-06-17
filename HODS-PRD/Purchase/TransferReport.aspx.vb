Imports System.Globalization

Public Class TransferReport
    Inherits System.Web.UI.Page
    Dim configDate As New ConfigDate
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim outsTrnType As String = "'D204','D205','D206','D209'"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 in(" & outsTrnType & ") order by MQ001"
            ControlForm.showDDL(ddlTrnType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)

        End If
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click

        If tbVendor.Text = "" Then
            show_message.ShowMessage(Page, "Please check vendor code!!", UpdatePanel1)
            tbVendor.Focus()
            Exit Sub
        End If
        Dim tempTable As String = "TempTransferOutsource" & Session("UserName")
        genData(tempTable)
        Dim txtHead As String = "Outsource List"
        Select Case ddlTrnType.Text.ToString
            Case "D205"
                txtHead = "Outsource Credit List"
            Case "D209"
                txtHead = "Outsource Scrap List"
        End Select
        Dim date1 As String = tbTrnDateFrom.Text, date2 As String = tbTrnDateTo.Text
        Dim strDate As String = "", endDate As String = ""
        If date1 <> "" Then
            strDate = date1
        Else
            strDate = Date.Today.ToString("dd/MM/yyyy", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = date2
        Else
            endDate = Date.Today.ToString("dd/MM/yyyy", New CultureInfo("en-US"))
        End If
        Dim SQL As String = " select MA002 from PURMA where MA001='" & tbVendor.Text & "' "
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        Dim vendorName As String = ""
        If Program.Rows.Count > 0 Then
            vendorName = Program.Rows(0).Item("MA002").ToString.Replace(",", "")
        End If
        Dim paraName As String = "TempTable:" & tempTable & ",PageHead:" & txtHead & ",FromDate:" & strDate & ",ToDate:" & endDate & ",Supplier:" & tbVendor.Text & "-" & vendorName & ",RowPerPage:" & ddlRecordPerPage.Text.Trim
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=OutsourceList.rpt&paraName=" & paraName & "');", True)
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('UnClosedSaleOrder1.aspx?Fdate=" + Fromdate.ToString() + "&Tdate=" + Todate.ToString + "&Ndate=" + Ndate + "');", True)
    End Sub
    Sub genData(tempTable As String)
        CreateTempTable.createTempOutsourc(tempTable)
        Dim Program As New DataTable
        Dim date1 As String = tbTrnDateFrom.Text, date2 As String = tbTrnDateTo.Text
        Dim strDate As String = "", endDate As String = "", SQL As String = "", ISQL As String = "", USQL As String = ""
        Dim moType As String = ""
        Dim moNo As String = ""
        Dim qty As Integer = 0
        'Begin date
        If date1 <> "" Then
            strDate = configDate.dateFormat2(date1)
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'End date
        If date2 <> "" Then
            endDate = configDate.dateFormat2(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        Dim vendor As String = tbVendor.Text.Trim,
            trnType As String = ddlTrnType.Text

        'SQL = " select SFCTC.TC004,SFCTC.TC005,sum(SFCTC.TC036) from JINPAO80.dbo.SFCTC SFCTC " & _
        '      " left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
        '      " where SFCTB.TB001='" & ddlTrnType.Text & "' and SFCTB.TB004='2' and SFCTB.TB005='" & tbVendor.Text & "' " & _
        '      "   and SFCTB.TB003 between '" & strDate & "' and '" & endDate & "' " & _
        '      " group by SFCTC.TC004,SFCTC.TC005  order by SFCTC.TC004,SFCTC.TC005 "

        SQL = " select SFCTC.TC001,SFCTC.TC002,SFCTC.TC003,SFCTC.TC004,SFCTC.TC005,SFCTC.TC036, " & _
              " SFCTC.TC017,SFCTC.TC018,SFCTB.TB014 from JINPAO80.dbo.SFCTC SFCTC " & _
              "  left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
              " where SFCTB.TB001='" & trnType & "' and SFCTB.TB004='2' and SFCTB.TB005='" & vendor & "' " & _
              "   and SFCTB.TB003 between '" & strDate & "' and '" & endDate & "' " & _
              " order by SFCTC.TC004,SFCTC.TC005 "

        ISQL = "insert into " & tempTable & "(TrnType,TrnNo,TrnSeq,MoType,MoNo,receivingQty,price,amount,InvNo) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)



        SQL = " select SFCTC.TC004 as moType ,SFCTC.TC005 as moNo,sum(SFCTC.TC036) as qty from " & tempTable & " T " & _
              " left join JINPAO80.dbo.SFCTC SFCTC on SFCTC.TC004=T.MoType and SFCTC.TC005=T.MoNo " & _
              " left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
             " where SFCTB.TB001='" & ddlTrnType.Text & "' and SFCTB.TB004='2' and SFCTB.TB005='" & vendor & "' " & _
             "   and SFCTB.TB003 < '" & strDate & "' " & _
             " group by SFCTC.TC004,SFCTC.TC005  order by SFCTC.TC004,SFCTC.TC005 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            moType = Program.Rows(i).Item("moType")
            moNo = Program.Rows(i).Item("moNo")
            qty = Program.Rows(i).Item("qty")
            USQL = " if exists(select * from " & tempTable & " where MoType='" & moType & "' and MoNo='" & moNo & "' ) " & _
                   " update " & tempTable & " set receivedQty='" & qty & "' where MoType='" & moType & "' and MoNo='" & moNo & "' "
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next




        If trnType = "D204" Or trnType = "D206" Or trnType = "D209" Then
            Dim prvTrnType As String = "D205"
            If trnType = "D204" Then
                prvTrnType = "D203"
            End If
            'SQL = " select SFCTC.TC004 as moType ,SFCTC.TC005 as moNo,sum(SFCTC.TC036)as qty from " & tempTable & " T " & _
            '      " left join JINPAO80.dbo.SFCTC SFCTC on SFCTC.TC004=T.MoType and SFCTC.TC005=T.MoNo " & _
            '      " left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
            '      " where SFCTB.TB001='" & prvTrnType & "' and SFCTB.TB007='2' and SFCTB.TB008='" & vendor & "' " & _
            '      " group by SFCTC.TC004,SFCTC.TC005  order by SFCTC.TC004,SFCTC.TC005 "


            SQL = " select SFCTB.TB002 as trnNo,SFCTC.TC004 as moType ,SFCTC.TC005 as moNo,sum(SFCTC.TC036) as qty from " & tempTable & " T " & _
                 " left join JINPAO80.dbo.SFCTC SFCTC on SFCTC.TC004=T.MoType and SFCTC.TC005=T.MoNo " & _
                 " left join JINPAO80.dbo.SFCTB SFCTB on SFCTB.TB001=SFCTC.TC001 and SFCTB.TB002=SFCTC.TC002 " & _
                 " where SFCTB.TB001='" & prvTrnType & "' and SFCTB.TB007='2' and SFCTB.TB008='" & vendor & "' " & _
                 " group by SFCTC.TC004,SFCTC.TC005,SFCTB.TB002 " & _
                 " order by SFCTC.TC004,SFCTC.TC005,SFCTB.TB002 desc "
            Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            Dim lastMoType As String = "",
                lastMoNo As String = "",
                refTrnDetail As String = ""
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    If lastMoType <> .Item("moType") Or lastMoNo <> .Item("moNo") Then
                        If lastMoNo <> "" And lastMoNo <> "" Then
                            USQL = " if exists(select * from " & tempTable & " where MoType='" & lastMoType & "' and MoNo='" & lastMoNo & "' ) " & _
                                   " update " & tempTable & " set issueQty='" & qty & "',refTrnDetail='" & refTrnDetail.Substring(0, refTrnDetail.Length - 1) & "' where MoType='" & lastMoType & "' and MoNo='" & lastMoNo & "' "
                            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                        End If
                        qty = 0
                        refTrnDetail = prvTrnType & " "
                    End If
                    'moType = Program.Rows(i).Item("moType")
                    'moNo = Program.Rows(i).Item("moNo")
                    qty = qty + .Item("qty")
                    refTrnDetail = refTrnDetail & .Item("trnNo") & "=" & FormatNumber(.Item("qty").ToString, 0) & ","
                    lastMoType = .Item("moType")
                    lastMoNo = .Item("moNo")
                End With
            Next
            If lastMoType <> "" And lastMoNo <> "" Then
                USQL = " if exists(select * from " & tempTable & " where MoType='" & lastMoType & "' and MoNo='" & lastMoNo & "' ) " & _
                       " update " & tempTable & " set issueQty='" & qty & "',refTrnDetail='" & refTrnDetail.Substring(0, refTrnDetail.Length - 1) & "' where MoType='" & lastMoType & "' and MoNo='" & lastMoNo & "' "
                Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            End If
        End If

        'Outsource issue in case = 0 
        SQL = " select MOCTA.TA001 as moType,MOCTA.TA002 as moNo,MOCTA.TA015 as qty from " & tempTable & " T " & _
              " left join JINPAO80.dbo.MOCTA MOCTA on MOCTA.TA001=T.MoType and MOCTA.TA002=T.MoNo " & _
              " where T.issueQty=0 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            moType = Program.Rows(i).Item("moType")
            moNo = Program.Rows(i).Item("moNo")
            qty = Program.Rows(i).Item("qty")
            USQL = " if exists(select * from " & tempTable & " where MoType='" & moType & "' and MoNo='" & moNo & "' ) " & _
                   " update " & tempTable & " set issueQty='" & qty & "' where MoType='" & moType & "' and MoNo='" & moNo & "' "
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "TempTransferOutsource" & Session("UserName")
        genData(tempTable)
        'Dim SQL As String = "select * from " & tempTable & " order by TrnType,TrnNo,TrnSeq"
        Dim SQL As String = " select  PURMA.MA002 as 'Supplier Short Description', " & _
                        " case when ACPTB.TB004 = '1' then 'Receipt' " & _
                        " when ACPTB.TB004 = '2' then 'Return' " & _
                        " when ACPTB.TB004 = '3' then 'Subcontract Receipt' " & _
                        " when ACPTB.TB004 = '4' then 'Subcontract Return'" & _
                        " when ACPTB.TB004 = '5' then 'Declaration/Retirement' " & _
                        " when ACPTB.TB004 = '6' then 'Export Expenses' " & _
                        " when ACPTB.TB004 = '7' then 'Asset Acquisition' " & _
                        " when ACPTB.TB004 = '8' then 'Asset Betterment' " & _
                        " when ACPTB.TB004 = '9' then 'Other' " & _
                        " when ACPTB.TB004 = 'A' then 'Offset Proforma' " & _
                        " when ACPTB.TB004 = 'B' then 'Purchase' " & _
                        " when ACPTB.TB004 = 'C' then 'Repair' " & _
                        " when ACPTB.TB004 = 'E' then 'Asset Receipt' " & _
                        " when ACPTB.TB004 = 'G' then 'CRMRepair' end as 'Source', " & _
                        " ACPTA.TA021 as 'Invoice', " & _
                        " case when ACPTB.TB008 ='' then '' else " & _
                        " (SUBSTRING( ACPTB.TB008 , 1,4)+'-'+SUBSTRING ( ACPTB.TB008 , 5,2)+'-'+SUBSTRING ( ACPTB.TB008 , 7,2)) end as 'Order Date' , " & _
                        "  TrnType+'-'+TrnNo  as 'Source Code', " & _
                        "  ACPTB.TB039 as 'Spec', refTrnDetail as 'PO/Tranfer Qty', receivingQty as 'Invoice Receipt', " & _
                        "  price as 'Invoce Price', amount as 'Total(O/C)' " & _
                        "  from " & tempTable & _
                        "  left join JINPAO80.dbo.ACPTB on ACPTB.TB005 = TrnType and ACPTB.TB006 = TrnNo and ACPTB.TB007 = TrnSeq " & _
                        "  left join JINPAO80.dbo.ACPTA on ACPTA.TA001 = ACPTB.TB001 and ACPTA.TA002 = ACPTB.TB002  " & _
                        "  left join JINPAO80.dbo.PURMA on PURMA.MA001 = ACPTA.TA004 " & _
                        " order by TrnType,TrnNo,TrnSeq"

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class