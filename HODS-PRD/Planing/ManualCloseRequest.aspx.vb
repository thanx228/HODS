Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class ManualCloseRequest
    Inherits System.Web.UI.Page
    'Dim ControlForm As New ControlDataForm
    'Dim Conn_SQL As New ConnSQL
    'Dim configDate As New ConfigDate
    Dim dbConn As New DataConnectControl
    Dim dateCont As New DateControl

    Dim CreateTable As New CreateTable
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim ddlCont As New DropDownListControl
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52')  order by MQ002"
            ddlCont.showDDL(ddlMoType, SQL, VarIni.ERP, "MQ002", "MQ001", False)

            btExport.Visible = False
            'HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        If tbMoNo.Text = "" Then
            show_message.ShowMessage(Page, "MO No is null!!", UpdatePanel1)
            tbMoNo.Focus()
            Exit Sub
        End If

        Dim tempTable As String = "tempManulaClose" & Session("UserName")
        genTemp(tempTable)
        Dim SQL As String = "",
            USQL As String = "",
            moType As String = ddlMoType.Text.Trim,
            moNo As String = tbMoNo.Text.Trim,
            comQty As String = "TA017+TA018+TA060"

        SQL = " select TA001,TA002,TA006,TA035,TA015,TA017,TA018," &
              " TA060,TA015-TA017-TA018-TA060 F01,TB003,TB013,TB004," &
              " isnull(T.qpaQty,0) F11,T.issueQty F02,TB004-T.issueQty F03," &
              " case when (T.issueQty-(isnull(T.qpaQty,0)*(" & comQty & ")))<0 then 0 else T.issueQty-(isnull(T.qpaQty,0)*(" & comQty & ")) end F04," &
              " T.returnQty F05, " &
              " case when (T.issueQty-(isnull(T.qpaQty,0)*(" & comQty & ")))<0 then 0 else (T.issueQty-(isnull(T.qpaQty,0)*(" & comQty & ")))-T.returnQty end F06 " &
              " from " & VarIni.DBMIS & ".." & tempTable & " T left join MOCTA on TA001=T.moType and TA002=T.moNo " &
              " left join MOCTB on TB001=T.moType and TB002 = T.moNo and TB003 = T.item  " &
              " where T.moType='" & moType & "' and T.moNo='" & moNo & "' order by TB003 "
        Dim gvCont As New GridviewControl

        gvCont.ShowGridView(gvShow, SQL, VarIni.ERP)
        ucRowCount.RowCount = gvCont.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub

    Sub genTemp(tempTable As String)
        CreateTempTable.createTempMOCheckMatReturn(tempTable)
        'generate temp
        Dim SQL As String = "",
            WHR As String = "",
            USQL As String = "",
            moType As String = ddlMoType.Text.Trim,
            moNo As String = tbMoNo.Text.Trim

        'get mo data & BOM
        SQL = "select TA001,TA002,TB003,MD006 from MOCTB left join MOCTA on TA001=TB001 and TA002=TB002 left join BOMMD on MD001=TA006 and MD003=TB003 where TB001='" & moType & "' and TB002='" & moNo & "' "
        SQL = "insert into " & VarIni.DBMIS & ".." & tempTable & "(moType,moNo,item,qpaQty)" & SQL
        dbConn.TransactionSQL(SQL, VarIni.ERP, dbConn.WhoCalledMe)

        SQL = "select * from " & tempTable
        Dim Program As New DataTable
        Program = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        If Program.Rows.Count > 0 Then
            'get data from mat issue & return
            SQL = "select TE004,sum(case when MQ003='54' then TE005 else 0 end) mat_issue,sum(case when MQ003='56' then TE005 else 0 end) mat_return from MOCTE left join CMSMQ on MQ001=TE001 where TE011='" & moType & "' and TE012='" & moNo & "' group by TE004"
            Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    USQL = " update " & tempTable & " set issueQty=" & .Item("mat_issue") & ",returnQty=" & .Item("mat_return") & " where moType='" & moType & "'and moNo='" & moNo & "' and item='" & .Item("TE004") & "'"
                    dbConn.TransactionSQL(USQL, VarIni.ERP, dbConn.WhoCalledMe)
                End With
            Next
        Else
            SQL = "select TA001,TA002 from MOCTA where TA001='" & moType & "' and TA002='" & moNo & "' "
            SQL = "insert into " & VarIni.DBMIS & ".." & tempTable & "(moType,moNo)" & SQL
            dbConn.TransactionSQL(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        End If

    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If tbMoNo.Text = "" Then
            show_message.ShowMessage(Page, "MO No is null!!", UpdatePanel1)
            tbMoNo.Focus()
            Exit Sub
        End If
        If ddlReason.Text = "3" And tbReason.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Reason for other is empty", UpdatePanel1)
            tbReason.Focus()
            Exit Sub
        End If
        Dim reason As String = ""
        Select Case ddlReason.Text.Trim
            Case "1"
                reason = "Order Cancle"
            Case "2"
                reason = "ECN Change"
            Case Else
                reason = tbReason.Text.Trim.Replace(",", "")
        End Select
        Dim tempTable As String = "tempManulaClose" & Session("UserName")
        genTemp(tempTable)

        Dim filePrint As String = "ManualCloseRequest.rpt",
           moType As String = ddlMoType.Text.Trim,
           moNo As String = tbMoNo.Text.Trim
        If ddlPage.Text = "A5" Then
            filePrint = "ManualCloseRequestHalf.rpt"
        End If
        Dim paraName As String = "table:" & tempTable & ",moType:" & moType & ",moNo:" & moNo & ",Reason:" & reason
        Dim rnd As New Random
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=" & filePrint & "&paraName=" & Server.UrlEncode(paraName) & "&encode=1&RID=" & rnd.Next(1, 100) & "');", True)

    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        Dim expCont As New ExportImportControl
        expCont.Export("ManualCloseRequest" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File 
    End Sub
End Class