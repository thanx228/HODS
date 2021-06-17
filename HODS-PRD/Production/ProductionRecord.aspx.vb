Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class ProductionRecord
    Inherits System.Web.UI.Page
    'Dim ControlForm As New ControlDataForm
    'Dim Conn_SQL As New ConnSQL
    'Dim configDate As New ConfigDate

    Dim CreateTable As New CreateTable

    'Dim CreateTempTable As New CreateTempTable
    Dim table As String = "ProductionProcessRecord"
    Dim tableSum As String = "ProductionProcessSum"
    Dim tableOper As String = "ProductionProcessOperator"
    Dim tableBOM As String = "ProductionProcessBOM"
    Dim usageTime As Integer = 0

    Dim dbConn As New DataConnectControl
    Dim ddlCont As New DropDownListControl
    Dim gvCont As New GridviewControl
    Dim dtCont As New DataTableControl
    Dim dateCont As New DateControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("UserName") = "" Then
            '    Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            'End If
            If Session(VarIni.UserName) <> "" Then
                Dim setShowBut As Boolean = False
                If Session(VarIni.UserGroup).ToString.Trim = "SYS" Then
                    setShowBut = True
                End If

                Button1.Visible = setShowBut
                'ControlForm.GridviewFormat(gvListMO)
                'ControlForm.GridviewFormat(gvListQty)
                CreateTable.CreateProductionProcessRecord()
                CreateTable.CreateProductionProcessSum()
                CreateTable.CreateProductionProcessOperator()
                CreateTable.CreateProductionProcessBOM()
                Dim headCont As New HeaderControl
                Dim nameHead As String = headCont.nameHeader(Request.CurrentExecutionFilePath.ToString)

                Label47.Text = nameHead & "(Key In)"
                Label48.Text = nameHead & "(Report)"

                ClearKey()
                ClearCheck()
                ddlWc_SelectedIndexChanged(sender, e)
                'Dim script As String = "$(document).ready(function () { $('[id*=btReCal]').click(); });"
                'ClientScript.RegisterStartupScript(Me.GetType, "load", script, True)
            End If
        Else
            Dim wcICausedPostBack As WebControl = CType(GetControlThatCausedPostBack(TryCast(sender, Page)), WebControl)
            Dim indx As Integer = wcICausedPostBack.TabIndex
            Dim ctrl =
             From control In wcICausedPostBack.Parent.Controls.OfType(Of WebControl)()
             Where control.TabIndex > indx
             Select control
            ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus()
        End If
        'MyBase.LockScreenAfterClick(btReCal, "Processing Your Request...")
    End Sub

    Protected Function GetControlThatCausedPostBack(ByVal page As Page) As Control
        Dim control As Control = Nothing

        Dim ctrlname As String = page.Request.Params.Get("__EVENTTARGET")
        If ctrlname IsNot Nothing AndAlso ctrlname <> String.Empty Then
            control = page.FindControl(ctrlname)
        Else
            For Each ctl As String In page.Request.Form
                Dim c As Control = page.FindControl(ctl)
                If TypeOf c Is Button OrElse TypeOf c Is ImageButton Then
                    control = c
                    Exit For
                End If
            Next ctl
        End If
        Return control

    End Function

    Sub checkStatusElement(ByRef isMultiMO As Boolean, ByRef isMachine As Boolean, ByRef isBOM As Boolean, ByRef isPatial As Boolean)
        Dim SQL As String,
           dt As DataTable

        SQL = " select case when CMSMD.UDF01='' then CMSMX.UDF01 else CMSMD.UDF01 end UDF01,CMSMD.UDF02," &
              " CMSMD.UDF03,case when CMSMD.UDF04='' then CMSMX.UDF04 else CMSMD.UDF04 end UDF04 from CMSMX " &
              " left join CMSMD on MD001=MX002 where CMSMD.MD001='" & lbWC.Text & "' and CMSMX.MX001='" & ddlMC.Text.Trim & "' "
        'SQL = "select CMSMD.UDF01,CMSMD.UDF02,CMSMD.UDF03,CMSMD.UDF04 from CMSMD where CMSMD.MD001='" & lbWC.Text & "' "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        With dt
            If .Rows.Count > 0 Then
                With .Rows(0)
                    If .Item("UDF01").ToString.Trim = "M" Then
                        isMachine = False
                    End If
                    If .Item("UDF02").ToString.Trim = "M" Then
                        isMultiMO = True
                    End If
                    Select Case .Item("UDF04").ToString.Trim
                        Case "A" 'Patial process
                            isPatial = True
                        Case "B" 'BOM
                            isBOM = True
                    End Select
                End With
            End If
        End With
    End Sub

    Sub gridviewColVisible(ByRef gv As GridView, Optional ByVal col As String = "", Optional ByVal colName As Boolean = False)

        With gv
            Dim colLen As Integer = .Columns.Count - 1
            If col = "" Then
                For i As Integer = 0 To colLen
                    .Columns(i).Visible = True
                Next
            Else
                Dim cc() As String = col.Split(",")
                For i As Integer = 0 To cc.Length - 1
                    Dim colIndex As Integer = 0
                    Dim setV As Boolean = False
                    If colName Then
                        colIndex = gvCont.getColIndexByName(gv, cc(i).Trim)
                    Else
                        Dim dd() As String = cc(i).Trim.Split(":")
                        colIndex = CInt(dd(0).Trim)
                        If dd(1).Trim = "1" Then
                            setV = True
                        End If
                    End If

                    If colIndex <= colLen And colIndex >= 0 Then
                        .Columns(colIndex).Visible = setV
                    End If
                Next
            End If
        End With

    End Sub

    Protected Sub btChk_Click(sender As Object, e As EventArgs) Handles btChk.Click
        gridviewColVisible(gvListMO)
        gridviewColVisible(gvListQty)
        Dim SQL As String,
            dt As DataTable
        'check for Multi MO for one machine
        Dim isMultiMO As Boolean = False,
            isMachine As Boolean = True,
            isBOM As Boolean = False,
            isPatial As Boolean = False
        checkStatusElement(isMultiMO, isMachine, isBOM, isPatial)
        'check machine for mo
        Dim wc As String = ddlWc.Text.Trim,
            mc As String = ddlMC.Text.Trim
        If Not isMachine Then
            If tbMO.Text.Trim.Replace("-", "").Replace("_", "") = "" Then
                show_message.ShowMessage(Page, "MO is null!!!", UpdatePanel1)
                tbMO.Focus()
                Exit Sub
            End If
        End If
        lbMo.Text = tbMO.Text.Trim
        Dim mo() As String = If(lbMo.Text.Trim.Replace("-", "").Replace("_", "") = "", "", lbMo.Text.Trim).Split("-") 'moType=S.TA001 and moNo=S.TA002 and moSeq=S.TA003

        'check mch for mo
        Dim whr As String = ""
        If isMachine Then 'machine:MO = 1:1
            If mo.Count > 1 Then
                If isMultiMO Then 'multi MO per action  wc = "W01" Or wc = "W02" Or wc = "W23" Or wc = "W27" or "W52"   and TB001='D210' and TB002='20150509139'
                    SQL = "select TB001,TB002,TC003 from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 where TC004='" & mo(0) & "' and TC005='" & mo(1) & "' and TC008='" & mo(2) & "' and SFCTB.TB005 in ('W19','W51') order by TB015 desc "
                    'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                    dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            With dt.Rows(i)
                                whr &= " and tranNo not like '" & .Item("TB001").ToString.Trim & "-" & .Item("TB002").ToString.Trim & "%' "
                            End With
                        Next
                    End If
                Else
                    whr = " and rtrim(S.moType)+'-'+rtrim(S.moNo)+'-'+rtrim(S.moSeq) <>'" & mo(0).Trim & "-" & mo(1).Trim & "-" & mo(2) & "' "
                End If
            End If

            SQL = " select case when D.tranNo='' then rtrim(S.moType)+'-'+rtrim(S.moNo)+'-'+rtrim(S.moSeq) else D.tranNo end docNo " &
                  " from " & tableSum & " S " &
                  " left join " & table & " D on D.docNo=S.docStart" &
                  " where D.wc='" & wc & "' and S.mc='" & ddlMC.Text.Trim & "' and S.docEnd=0 " & whr
            'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            dt = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            If dt.Rows.Count > 0 And mo.Count > 1 Then
                show_message.ShowMessage(Page, dt.Rows(0).Item("docNo").ToString.Trim & " is running for Machine(" & ddlMC.Text.Trim & ")!!,Please check it again.", UpdatePanel1)
                lbMo.Text = ""
                gvListMO.DataSource = ""
                gvListMO.DataBind()
                gvListQty.DataSource = ""
                gvListQty.DataBind()
                ddlMC.Focus()
                Exit Sub
            End If
            If dt.Rows.Count = 0 And mo.Count = 1 Then
                show_message.ShowMessage(Page, "MO is null,Please check it again.", UpdatePanel1)
                lbMo.Text = ""
                gvListMO.DataSource = ""
                gvListMO.DataBind()
                gvListQty.DataSource = ""
                gvListQty.DataBind()
                tbMO.Focus()
                Exit Sub
            End If
        End If

        'If isPatial And tbSub.Text.Trim = "" Then
        '    show_message.ShowMessage(Page, "Sub Process is null please check it!!", UpdatePanel1)
        '    tbSub.Focus()
        '    Exit Sub
        'End If

        'If cbInd.Enabled And cbInd.Checked And tbOperInd.Text.Trim = "" Then
        '    show_message.ShowMessage(Page, "Individual(opt code) is null please check it!!", UpdatePanel1)
        '    cbInd.Focus()
        '    Exit Sub
        'End If

        Dim isShowQty As Boolean = False 'isnull(rtrim(R.dateCode)+' '+rtrim(R.timeCode),'') E
        Dim showFld As String = ""
        Dim pType As String = "",
            pCode As String = ""
        whr = ""
        'check for one man process
        If cbInd.Enabled Then 'And tbOperInd.Text.Trim <> ""
            If cbInd.Checked Then
                pType = ""
                If isBOM Then
                    pType = "B"
                End If
                pCode = tbOperInd.Text.Trim
                If pCode <> "" Then
                    isShowQty = True
                    whr = " and R.processType='" & pType & "' and R.processCode='" & pCode & "' "
                End If
            Else
                If isBOM Then
                    pType = "B"
                End If
                whr = " and R.processType='" & pType & "' and R.processCode='' "
                If mo.Count > 0 Then
                    whr &= " and S.TA001='" & mo(0) & "' and S.TA002='" & mo(1) & "' and S.TA003 ='" & mo(2) & "' "
                End If
                SQL = " select isnull(SS.docStart,0) E " &
                      " from SFCTA S left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
                      " left join  " & VarIni.DBMIS & ".." & tableSum & " SS on SS.moType=S.TA001 and SS.moNo=S.TA002 and SS.moSeq=S.TA003 and SS.mc='" & ddlMC.Text.Trim & "' and SS.docEnd=0 " &
                      " left join " & VarIni.DBMIS & ".." & table & " R on R.docNo=SS.docStart " &
                      " where S.TA006 = '" & wc & "' and R.mc='" & mc & "' " & whr
                ' dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                If dt.Rows.Count > 0 Then
                    With dt.Rows(0)
                        If CInt(.Item("E").ToString.Trim) > 0 Then
                            isShowQty = True
                        End If
                    End With
                End If
            End If
        Else
            'SQL = ""
            Dim txt As String = ""
            If isMultiMO Then
                'check mo for machine running
                If mo.Count > 1 Then
                    SQL = "select TB001,TB002,count(*) cnt from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 where TC004='" & mo(0) & "' and TC005='" & mo(1) & "' and TC008='" & mo(2) & "' and SFCTB.TB005 in ('W19','W51') group by TB001,TB002 order by TB001,TB002  "
                    'Dim dt2 As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                    Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                    With dt2
                        If .Rows.Count > 0 Then
                            For j As Integer = 0 To .Rows.Count - 1
                                With .Rows(j)
                                    SQL = "select TC004,TC005,TC008 from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 where TB001='" & .Item("TB001").ToString.Trim & "' and TB002='" & .Item("TB002").ToString.Trim & "' order by TB015 desc "
                                    'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                                    dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                                    With dt
                                        For i As Integer = 0 To dt.Rows.Count - 1
                                            With .Rows(i)
                                                txt &= .Item("TC004").ToString.Trim & "-" & .Item("TC005").ToString.Trim & "-" & .Item("TC008").ToString.Trim & ","
                                            End With
                                        Next
                                    End With
                                End With
                            Next
                            txt = txt.Substring(0, txt.Length - 1)
                        End If
                    End With
                End If
            Else
                If isPatial Then
                    'pType = "A"
                    'pCode = tbSub.Text.Trim
                    'whr = " and R.processType='" & pType & "' and R.processCode='" & pCode & "' "
                    whr = " and R.processType='A' "
                End If
                If isBOM Then
                    'pType = "B"
                    pCode = ""
                    whr = " and R.processType='B' and R.processCode='" & pCode & "' "
                End If
                If mo.Count > 1 Then
                    txt = mo(0).Trim & "-" & mo(1).Trim & "-" & mo(2).Trim
                End If
            End If
            If txt <> "" Then
                whr &= " and rtrim(S.TA001)+'-'+rtrim(S.TA002)+'-'+rtrim(S.TA003) in ('" & txt.Replace(",", "','") & "') "
            End If
            SQL = " select top 1 isnull(SS.docStart,0) E " &
                  " from SFCTA S left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
                  " left join  " & VarIni.DBMIS & ".." & tableSum & " SS on SS.moType=S.TA001 and SS.moNo=S.TA002 and SS.moSeq=S.TA003 and SS.mc='" & ddlMC.Text.Trim & "' and SS.docEnd=0 " &
                  " left join " & VarIni.DBMIS & ".." & table & " R on R.docNo=SS.docStart " &
                  " where  S.TA006 = '" & wc & "' and R.mc='" & mc & "'  " & whr &
                  " order by isnull(SS.docStart,0) desc"

            ' dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)

            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    If CInt(.Item("E").ToString.Trim) > 0 Then
                        isShowQty = True
                    End If
                End With
            End If
        End If

        Dim col As String = ""
        Dim colName As String = ""
        If isMultiMO Then 'multi MO per action wc = "W01" Or wc = "W02" Or wc = "W23" Or wc = "W27"
            Dim dt2 As DataTable
            Dim txtTrn As String = ""

            If isShowQty Then 'end work input qty
                SQL = "select substring(isnull(R.tranNo,''),1,16) A,count(*) B from " & tableSum & " S left join " & table & " R on R.docNo=S.docStart  where R.wc = '" & wc & "' and S.mc='" & mc & "' and S.docEnd=0 group by substring(isnull(R.tranNo,''),1,16) "
                'dt2 = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                dt2 = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

                If dt2.Rows.Count > 0 Then
                    For i As Integer = 0 To dt2.Rows.Count - 1
                        With dt2.Rows(i)
                            txtTrn &= .Item("A").ToString & ","
                        End With
                    Next
                    txtTrn = txtTrn.Substring(0, txtTrn.Length - 1)
                End If
                If txtTrn <> "" Then
                    whr &= " and rtrim(TC001)+'-'+rtrim(TC002) in ('" & txtTrn.Replace(",", "','") & "') "
                End If
                showFld = " rtrim(S.TA001)+'-'+rtrim(S.TA002)+'-'+S.TA003 A,'' B,M.TA035 C, M.TA015 D," &
                          " isnull(rtrim(R.isSetTime),'N') E,isnull(rtrim(R.dateCode)+' '+rtrim(R.timeCode),'') F, " &
                          " TC001+'-'+TC002+'-'+TC003 G,isnull(rtrim(SS.docNo),'') H,isnull(R.shift,'') I,'' J, " &
                          " M.TA034 K,S.TA011 L,S.TA012 M,MW002 O,isnull(R.point,0) P," &
                          " case M.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' when 'Y' then 'Completed' else 'Manual Completed' end N "
                'col = "1:0,13:0"
                colName = "Process Type,Point Name/Opt,Point"
                SQL = " select " & showFld &
                      " from SFCTC  " &
                      " left join SFCTA S on S.TA001=TC004 and S.TA002=TC005 and S.TA003=TC008 " &
                      " left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
                      " left join CMSMW on MW001=S.TA004 " &
                      " left join  " & VarIni.DBMIS & ".." & tableSum & " SS on SS.moType=S.TA001 and SS.moNo=S.TA002 and SS.moSeq=S.TA003 " &
                      " left join " & VarIni.DBMIS & ".." & table & " R on R.docNo=SS.docStart " &
                      " where S.TA006 = '" & wc & "' and SS.mc='" & mc & "' and SS.docEnd=0   " & whr &
                      " order by TC003 "
            Else 'begin work
                SQL = " select TB001 A,TB002 B,count(*) C from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 " &
                      " where TC004='" & mo(0) & "' and TC005='" & mo(1) & "' and TC008='" & mo(2) & "' group by TB001,TB002"
                ' dt2 = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                dt2 = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                If dt2.Rows.Count > 0 Then
                    For i As Integer = 0 To dt2.Rows.Count - 1
                        With dt2.Rows(i)
                            txtTrn &= .Item("A").ToString.Trim & "-" & .Item("B").ToString.Trim & ","
                        End With
                    Next

                    If txtTrn <> "" Then
                        whr &= " and rtrim(TC001)+'-'+rtrim(TC002) in ('" & txtTrn.Replace(",", "','") & "') "
                    End If
                    showFld = " rtrim(S.TA001)+'-'+rtrim(S.TA002)+'-'+S.TA003 A,M.TA035 B, M.TA015 C,TC001+'-'+TC002+'-'+TC003 D,M.TA006 E," &
                                " '' F,'' G ,'' H,M.TA034 I,S.TA011 J,S.TA012 K,MW002 M," &
                                " case M.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' when 'Y' then 'Completed' else 'Manual Completed' end  L "
                    'col = "5:0,6:0,7:0,10:0"
                    colName = "Item,Assy Desc,Assy Part,Assy Item,Point Name,Point"
                    SQL = " select " & showFld &
                            " from SFCTC  " &
                            " left join SFCTA S on S.TA001=TC004 and S.TA002=TC005 and S.TA003=TC008 " &
                            " left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
                            " left join CMSMW on MW001=S.TA004 " &
                            " left join  " & VarIni.DBMIS & ".." & tableSum & " SS on SS.moType=S.TA001 and SS.moNo=S.TA002 and SS.moSeq=S.TA003 and SS.mc='" & mc & "' and SS.docEnd=0 " &
                            " left join " & VarIni.DBMIS & ".." & table & " R on R.docNo=SS.docStart " &
                            " where S.TA006 = '" & wc & "' " & whr &
                            " order by TC001,TC002,TC003 "
                End If
            End If
            'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        Else 'single MO per action
            If isBOM Then 'souse from BOM
                If isShowQty Then 'end work
                    showFld = " rtrim(S.TA001)+'-'+rtrim(S.TA002)+'-'+S.TA003 A,isnull(R.processCode,'') B,M.TA035 C, M.TA015 D," &
                              " isnull(rtrim(R.isSetTime),'N') E,isnull(rtrim(R.dateCode)+' '+rtrim(R.timeCode),'') F, " &
                              " '' G,isnull(rtrim(SS.docNo),'') H,isnull(R.shift,'') I ,isnull(R.processType,'') J ," &
                              " M.TA034 K,S.TA011 L,S.TA012 M,MW002 O,isnull(R.point,0) P," &
                              " case M.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' when 'Y' then 'Completed' else 'Manual Completed' end N "
                    'col = "10:0" ',1:0
                    colName = "Transfer No,Process Type,Point Name/Opt,Point"
                    If mo.Count > 1 Then
                        whr &= " and S.TA001='" & mo(0) & "' and S.TA002='" & mo(1) & "' and S.TA003 ='" & mo(2) & "'"
                    End If

                    SQL = " select " & showFld &
                          " from SFCTA S left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 left join CMSMW on MW001=S.TA004 " &
                          " left join  " & VarIni.DBMIS & ".." & tableSum & " SS on SS.moType=S.TA001 and SS.moNo=S.TA002 and SS.moSeq=S.TA003  " &
                          " left join " & VarIni.DBMIS & ".." & table & " R on R.docNo=SS.docStart " &
                          " where  S.TA006 = '" & wc & "' and SS.mc='" & mc & "' and SS.docEnd=0 " & whr
                Else 'begin work
                    showFld = " rtrim(S.TA001)+'-'+rtrim(S.TA002)+'-'+S.TA003 A,M.TA035 B, M.TA015 C,'' D,M.TA006 E,MB.TB013 F,MB.TB003 G,MB.TB012 H," &
                              " M.TA034 I,S.TA011 J,S.TA012 K,MW002 M," &
                              " case M.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' when 'Y' then 'Completed' else 'Manual Completed' end  L "
                    'col = "4:0,5:0,9:0"
                    colName = "Transfer No,Item,Point Name,Point"
                    If cbInd.Enabled And cbInd.Checked And tbOperInd.Text.Trim = "" Then
                        whr = ""
                        SQL = "select " & showFld &
                              " from MOCTB MB " &
                              " left join MOCTA M on M.TA001=MB.TB001 and M.TA002=MB.TB002 " &
                              " left join SFCTA S on S.TA001=MB.TB001 and S.TA002=MB.TB002 " &
                              " left join CMSMW on MW001=S.TA004 " &
                              " where S.TA001='" & mo(0) & "' and S.TA002='" & mo(1) & "' and S.TA003 ='" & mo(2) & "' and S.TA006 = '" & wc & "' "
                    Else
                        SQL = " select " & showFld &
                              " from MOCTB MB " &
                              " left join MOCTA M on M.TA001=MB.TB001 and M.TA002=MB.TB002 " &
                              " left join SFCTA S on S.TA001=MB.TB001 and S.TA002=MB.TB002 " &
                              " left join CMSMW on MW001=S.TA004 " &
                              " where S.TA001='" & mo(0) & "' and S.TA002='" & mo(1) & "' and S.TA003 ='" & mo(2) & "' and S.TA006 = '" & wc & "' "
                    End If
                End If
                'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            Else 'is normal case
                If isShowQty Then 'end work
                    If isPatial Then
                        showFld = " rtrim(S.TA001)+'-'+rtrim(S.TA002)+'-'+S.TA003 A,R.processCode B,M.TA035 C, M.TA015 D," &
                                  " isnull(rtrim(R.isSetTime),'N') E,isnull(rtrim(R.dateCode)+' '+rtrim(R.timeCode),'') F,'' G, " &
                                  " isnull(rtrim(SS.docNo),'') H,isnull(R.shift,'') I,isnull(R.processType,'') J ," &
                                  " M.TA034 K,S.TA011 L,S.TA012 M,MW002 O,isnull(R.point,0) P," &
                                  " case M.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' when 'Y' then 'Completed' else 'Manual Completed' end N "
                        'col = ""
                        colName = "Process Type,Transfer No"
                    Else
                        showFld = " rtrim(S.TA001)+'-'+rtrim(S.TA002)+'-'+S.TA003 A,'' B,M.TA035 C, M.TA015 D," &
                                  " isnull(rtrim(R.isSetTime),'N') E,isnull(rtrim(R.dateCode)+' '+rtrim(R.timeCode),'') F,'' G, " &
                                  " isnull(rtrim(SS.docNo),'') H,isnull(R.shift,'') I,'' J, " &
                                  " M.TA034 K,S.TA011 L,S.TA012 M,MW002 O,isnull(R.point,0) P," &
                                  " case M.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' when 'Y' then 'Completed' else 'Manual Completed' end N "

                        'col = "1:0,13:0"
                        colName = "Transfer No,Process Type,Point Name/Opt,Point"
                    End If
                    If mo.Count > 1 Then
                        whr &= " and S.TA001='" & mo(0) & "' and S.TA002='" & mo(1) & "' and S.TA003 ='" & mo(2) & "'"
                    End If
                    SQL = " select " & showFld &
                         " from SFCTA S left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
                         " left join CMSMW on MW001=S.TA004 " &
                         " left join  " & VarIni.DBMIS & ".." & tableSum & " SS on SS.moType=S.TA001 and SS.moNo=S.TA002 and SS.moSeq=S.TA003  " &
                         " left join " & VarIni.DBMIS & ".." & table & " R on R.docNo=SS.docStart " &
                         " where  S.TA006 = '" & wc & "' and SS.mc='" & mc & "' and SS.docEnd=0 " & whr
                Else 'begin work
                    If Not cbInd.Checked Then
                        whr = ""
                    End If
                    showFld = " rtrim(S.TA001)+'-'+rtrim(S.TA002)+'-'+S.TA003 A,M.TA035 B,M.TA015 C,'' D,M.TA006 E,'' F,'' G,'' H," &
                              " M.TA034 I,S.TA011 J,S.TA012 K,MW002 M," &
                              " case M.TA011 when '1' then 'Not Produced' when '2' then 'Issued' when '3' then 'Producing' when 'Y' then 'Completed' else 'Manual Completed' end  L "

                    'col = "0:0,4:0,5:0,6:0,7:0,8:0"
                    If isPatial Then
                        'col = "0:0,4:0,5:0,6:0,7:0,8:0"
                        colName = "Sel,Transfer No,Item,Assy Desc,Assy Part,Assy Item"
                    Else
                        'col = "0:0,4:0,5:0,6:0,7:0,8:0,9:0"
                        colName = "Sel,Transfer No,Item,Assy Desc,Assy Part,Assy Item,Point Name,Point"
                    End If
                    SQL = " select " & showFld &
                          " from SFCTA S left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
                          " left join CMSMW on MW001=S.TA004 " &
                          " where S.TA001='" & mo(0) & "' and S.TA002='" & mo(1) & "' and S.TA003 ='" & mo(2) & "' and S.TA006 = '" & wc & "' " & whr
                End If

                'SQL = " select " & showFld & _
                '          " from SFCTA S left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
                '          " left join  " & Conn_SQL.DBReport & ".." & tableSum & " SS on SS.moType=S.TA001 and SS.moNo=S.TA002 and SS.moSeq=S.TA003 and SS.mc='" & ddlMC.Text.Trim & "' and SS.docEnd=0 " & _
                '          " left join " & Conn_SQL.DBReport & ".." & table & " R on R.docNo=SS.docStart " & _
                '          " where S.TA001='" & mo(0) & "' and S.TA002='" & mo(1) & "' and S.TA003 ='" & mo(2) & "' and S.TA006 = '" & ddlWc.Text.Trim & "' " & whr
                'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                dt = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            End If
        End If

        If dt.Rows.Count = 0 Then
            show_message.ShowMessage(Page, "MO " & lbMo.Text.Trim & " is not found !!,Please check it again.", UpdatePanel1)
            tbMO.Focus()
            lbMo.Text = ""
            Exit Sub
        Else
            MultiView2.Visible = True
            Dim jsStr As String = "",
                jsStr2 As String = ""
            If isShowQty Then 'input qty=>End
                gvListQty.DataSource = dt
                gvListQty.DataBind()
                MultiView2.SetActiveView(View4)
                With gvListQty
                    Dim setLine As Boolean,
                        setChk As Boolean,
                        nightShift As Boolean,
                        setCol As Boolean
                    For i As Integer = 0 To .Rows.Count - 1
                        With .Rows(i)

                            setLine = False
                            setChk = True
                            nightShift = True
                            setCol = False

                            Dim setting As String = .Cells(gvCont.getColIndexByName(gvListQty, "Setting")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim.Substring(0, 1)
                            Dim shift As String = .Cells(gvCont.getColIndexByName(gvListQty, "Shift")).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "")
                            Dim lastID As String = .Cells(gvCont.getColIndexByName(gvListQty, "Last ID")).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "")

                            Dim tbAcceptQty As TextBox = .FindControl("tbAcceptQty")
                            Dim tbDefQty As TextBox = .FindControl("tbDefQty")
                            Dim tbScrapQty As TextBox = .FindControl("tbScrapQty")
                            Dim ddlScrapCode As DropDownList = .FindControl("ddlScrapCode")

                            If lastID <> "" Then
                                If setting = "N" Then
                                    setLine = True
                                    SQL = "select rtrim(Code) Code,rtrim(Code)+':'+Name Name from CodeInfo where CodeType='SCRAP' and WC like'%" & lbWC.Text.Trim & "%' order by Code"
                                    ddlCont.showDDL(ddlScrapCode, SQL, VarIni.DBMIS, "Name", "Code", True)
                                    setChk = False
                                Else
                                    If Not setCol Then
                                        Dim colSet As String = "Transfer No,Point/Opt,Process Type"
                                        If isMultiMO Then
                                            colSet = "Point/Opt,Process Type"
                                        End If
                                        If isPatial Then
                                            colSet = "Transfer No,Process Type,"
                                        End If
                                        If colName = "" Then
                                            colName = colSet & ",Accept Qty,Return Qty,Scrap Qty,Scrap Code"
                                        Else
                                            colName = colName & "," & colSet & ",Accept Qty,Return Qty,Scrap Qty,Scrap Code"
                                        End If
                                        setCol = True
                                    End If
                                End If
                            Else
                                setChk = False
                            End If
                            If shift = "D" Or shift = "" Then
                                nightShift = False
                            End If
                            tbAcceptQty.Enabled = setLine
                            tbDefQty.Enabled = setLine
                            tbScrapQty.Enabled = setLine
                        End With
                    Next
                    cbSet.Checked = setChk
                    cbSet.Enabled = False
                    cbShift.Checked = nightShift
                    cbShift.Enabled = False
                    .EnableViewState = True
                End With
                lbChk.Text = "E"
                jsStr = "gridviewScrollQty"
                jsStr2 = "gridviewScrollQty();"
                gridviewColVisible(gvListQty, colName, True)
            Else 'show=>Begin
                gvListMO.DataSource = dt
                gvListMO.DataBind()
                MultiView2.SetActiveView(View3)

                'pnSpot.Visible = chkMchSpotWeld() 'show spot welding
                lbChk.Text = "B"
                jsStr = "gridviewScrollList"
                jsStr2 = "gridviewScrollList();"
                'gvListMO.EnableViewState = True
                gridviewColVisible(gvListMO, colName, True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), jsStr, jsStr2, True)
            btSave.Visible = True
        End If

    End Sub

    Function chkMchSpotWeld() As Boolean
        Dim spotWeld As Boolean = False
        If cbSet.Checked = False Then
            Dim SQL As String = "select rtrim(MX001) MX001 from CMSMX where UDF03='Yes' and MX006<>'CANCEL' and MX001='" & ddlMC.Text.Trim & "' order by MX001"
            'Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            Dim dt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            If dt.Rows.Count > 0 Then
                spotWeld = True
            End If
        End If
        Return spotWeld
    End Function

    Protected Sub btReport_Click(sender As Object, e As EventArgs) Handles btReport.Click
        If MultiView1.ActiveViewIndex = 0 Then
            MultiView1.SetActiveView(View2)
            ClearCheck()
            machineChk.setObjectWithAll = ddlWcChk.Text.Trim
        End If
    End Sub

    Protected Sub btKey_Click(sender As Object, e As EventArgs) Handles btKey.Click
        If MultiView1.ActiveViewIndex <> 0 Then
            MultiView1.SetActiveView(View1)
            ClearKey()
        End If
    End Sub

    Public Shared Function FindControlRecursive(ByVal root As Control, ByVal id As String) As Control
        If root.ID = id Then
            Return root
        End If
        Return root.Controls.Cast(Of Control)().[Select](Function(c) FindControlRecursive(c, id)).FirstOrDefault(Function(c) c IsNot Nothing)
    End Function

    Protected Function returnDocRecord(mo() As String, Optional processCode As String = "") As Integer
        Dim SQL As String,
            WHR As String = ""
        If processCode <> "" Then
            WHR = " and processCode='" & processCode & "' "
        End If
        SQL = "select top 1 docNo from " & table & " where moType='" & mo(0) & "' and moNo='" & mo(1) & "' and moSeq='" & mo(2) & "' and mc='" & ddlMC.Text.Trim & "' " & WHR & " order by dateCode desc,timeCode desc"
        Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Return dtCont.IsDBNullDataRowDecimal(dr, "docNo")
    End Function

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        btSave.Enabled = False
        'btChk_Click(sender, e)

        Dim whrHash As Hashtable = New Hashtable,
           fldInsHash As Hashtable = New Hashtable,
           SQL As String, dt As DataTable
        Dim dateCode As String = Date.Now.ToString("yyyyMMdd"),
            timeCode As String = Date.Now.ToString("HH:mm"),
            mchCode As String = ddlMC.Text.Trim,
            wcCode As String = lbWC.Text.Trim,
            dateWork As String = ""

        Dim isSetTime As String = "N"
        If cbSet.Checked Then
            isSetTime = "Y"
        End If
        isSetTime &= lbChk.Text.Trim

        Dim isShift As String = "D"
        dateWork = dateCode
        If cbShift.Checked Then
            isShift = "N"
            Dim hh As Integer = CInt(Date.Now.ToString("HH"))
            If CInt(Date.Now.ToString("HH")) < 7 Then
                dateWork = Date.Now.AddDays(-1).ToString("yyyyMMdd")
            End If
        End If

        'check for Multi MO for one machine
        Dim isMultiMO As Boolean = False,
            isMachine As Boolean = True,
            isBOM As Boolean = False,
            isPatial As Boolean = False
        checkStatusElement(isMultiMO, isMachine, isBOM, isPatial)

        If lbChk.Text.Trim = "B" Then 'begin
            'check MO
            Const empNum As Integer = 24
            If gvListMO.Rows.Count = 0 Then
                show_message.ShowMessage(Page, "MO " & lbMo.Text.Trim & " is not found!!,Please check it again.", UpdatePanel1)
                btSave.Enabled = True
                tbMO.Focus()
                lbMo.Text = ""
                Exit Sub
            End If
            If isMachine Then 'machine:MO = 1:1
                SQL = "select case when D.tranNo='' then rtrim(S.moType)+'-'+rtrim(S.moNo)+'-'+rtrim(S.moSeq) else D.tranNo end docNo " &
                    " from   " & tableSum & " S " &
                    " left join " & table & " D on D.docNo=S.docStart" &
                    " where S.mc='" & ddlMC.Text.Trim & "' and S.docEnd=0 "
                'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                dt = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                If dt.Rows.Count > 0 Then
                    show_message.ShowMessage(Page, dt.Rows(0).Item("docNo").ToString.Trim & " is running for Machine(" & ddlMC.Text.Trim & ")!!,Please check it again.", UpdatePanel1)
                    btSave.Enabled = True
                    ddlMC.Focus()
                    lbMo.Text = ""
                    gvListMO.DataSource = ""
                    gvListMO.DataBind()
                    Exit Sub
                End If
            End If

            If isPatial Then
                Dim tbSubProcess As TextBox
                Dim tbPoint As TextBox
                With gvListMO
                    For i As Integer = 0 To .Rows.Count - 1
                        With .Rows(i)
                            tbSubProcess = .FindControl("tbSubProcess")
                            tbPoint = .FindControl("tbPoint")
                            If tbSubProcess.Text.Trim = "" Or tbPoint.Text.Trim = "" Or (tbPoint.Text.Trim <> "" And Not IsNumeric(tbPoint.Text.Trim)) Then
                                show_message.ShowMessage(Page, "Please input point name/point for your work", UpdatePanel1)
                                If tbSubProcess.Text.Trim = "" Then
                                    tbSubProcess.Focus()
                                Else
                                    tbPoint.Focus()
                                End If
                                btSave.Enabled = True
                                Exit Sub
                            End If
                        End With
                    Next
                End With
            End If

            'check exist operator
            Dim val As Hashtable = New Hashtable
            Dim NotHaveOper As Boolean = True
            For i As Integer = 1 To empNum
                Dim fld1 As String = "tbOper" & i
                Dim tbGet As TextBox
                tbGet = TryCast(FindControlRecursive(Page, fld1), TextBox)
                Dim oper As String = tbGet.Text.Trim
                If oper <> "" Then
                    If val.ContainsKey(oper) Then
                        show_message.ShowMessage(Page, "Operator Code > one time..", UpdatePanel1)
                        btSave.Enabled = True
                        tbGet.Focus()
                        Exit Sub
                    End If
                    'SQL = "select MV001,case when MV047='' then MV002 else MV047 end MV002 from CMSMV where MV001 = '" & tbGet.Text.Trim & "' order by MV001 "
                    SQL = " SELECT CODE,EMP_NAME FROM V_HR_EMPLOYEE WHERE EMP_STATE not like '3%' AND DEPT_CODE  IN (SELECT rtrim(Code) collate Chinese_PRC_BIN from CodeInfo where CodeType='DEPT_PRD') AND CODE='" & tbGet.Text.Trim & "' "

                    ' dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                    dt = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                    If dt.Rows.Count = 0 Then
                        show_message.ShowMessage(Page, "Operator " & tbGet.Text.Trim & " is not exist!!,Please check it again.", UpdatePanel1)
                        btSave.Enabled = True
                        tbGet.Focus()
                        Exit Sub
                    End If
                    val.Add(oper, oper)
                    NotHaveOper = False
                End If
            Next
            If NotHaveOper Then
                show_message.ShowMessage(Page, "Operator Code is Null", UpdatePanel1)
                btSave.Enabled = True
                tbOper1.Focus()
                Exit Sub
            End If
            Dim isMulti As String = "N"
            Dim rowLen As Integer = 0,
                getDoc As String = ""

            With gvListMO
                If cbInd.Checked Then 'one man per work(>1 man)
                    If .Rows.Count > 1 And Not isBOM Then
                        isMulti = "Y"
                    End If
                    If Not isBOM Then
                        rowLen = .Rows.Count - 1
                    End If
                    For k As Integer = 1 To empNum
                        Dim fld1 As String = "tbOper" & k
                        Dim tbGet As TextBox
                        tbGet = TryCast(FindControlRecursive(Page, fld1), TextBox)
                        Dim oper As String = tbGet.Text.Trim
                        If oper <> "" Then
                            For i As Integer = 0 To rowLen
                                With .Rows(i)
                                    'save record
                                    Dim mo() As String = .Cells(gvCont.getColIndexByName(gvListMO, "MO")).Text.Trim.Split("-")
                                    fldInsHash = New Hashtable From {
                                        {"moType", mo(0)}, 'mo type
                                        {"moNo", mo(1)}, 'mo no
                                        {"moSeq", mo(2)}, 'mo seq
                                        {"wc", wcCode}, 'WC
                                        {"mc", mchCode}, 'MC
                                        {"dateCode", dateCode}, 'date input auto
                                        {"timeCode", timeCode}, 'time input auto
                                        {"isSetTime", isSetTime}, 'Set Time
                                        {"isMulti", isMulti}, 'Muti
                                        {"tranNo", .Cells(gvCont.getColIndexByName(gvListMO, "Transfer No")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim}, 'Transfer Number
                                        {"shift", isShift} 'Is shift night
                                        }
                                    'fldInsHash.Add("isTeam", "N") 'Is Team=Group
                                    Dim pType As String = ""
                                    Dim pCode As String = ""
                                    If isPatial Then
                                        pType = "A"
                                        'pCode = .Cells(ControlForm.getColIndexByName(gvListMO, "Transfer No")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim
                                    End If
                                    If isBOM Then
                                        pType = "B"
                                    End If
                                    fldInsHash.Add("processType", pType) '
                                    fldInsHash.Add("processCode", oper) '
                                    fldInsHash.Add("dateWork", dateWork) 'datework
                                    fldInsHash.Add("createBy", Session("UserName")) 'user save
                                    dbConn.TransactionSQL(dbConn.getInsertSql(table, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                                    'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(table, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)

                                    Dim docNo As Integer
                                    docNo = returnDocRecord(mo, oper)
                                    getDoc = docNo

                                    'save operator
                                    whrHash = New Hashtable
                                    fldInsHash = New Hashtable From {
                                        {"moType", mo(0)}, 'mo type
                                        {"moNo", mo(1)}, 'mo no
                                        {"moSeq", mo(2)}, 'mo seq
                                        {"wc", wcCode}, 'WC
                                        {"mc", mchCode}, 'MC
                                        {"dateCode", dateCode}, 'date input auto
                                        {"timeCode", timeCode}, 'time input auto
                                        {"docStart", docNo}, 'doc Start
                                        {"opCode", oper} 'operator code
                                        }
                                    dbConn.TransactionSQL(dbConn.getInsertSql(tableOper, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                                    'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableOper, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)

                                    'save sumary
                                    whrHash = New Hashtable
                                    fldInsHash = New Hashtable From {
                                        {"moType", mo(0)}, 'mo type
                                        {"moNo", mo(1)}, 'mo no
                                        {"moSeq", mo(2)}, 'mo seq
                                        {"mc", mchCode}, 'MC
                                        {"docStart", docNo}, 'doc start
                                        {"manPower", "1"} 'Man Power
                                        }
                                    dbConn.TransactionSQL(dbConn.getInsertSql(tableSum, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                                    'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableSum, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                                End With
                            Next
                            'save bom
                            If isBOM Then
                                For i As Integer = 0 To .Rows.Count - 1
                                    With .Rows(i)
                                        'save BOM
                                        fldInsHash = New Hashtable
                                        whrHash = New Hashtable
                                        Dim cbSel As CheckBox
                                        cbSel = .FindControl("cbSel")
                                        If cbSel.Checked Then
                                            fldInsHash.Add("docStart", getDoc) 'doc start
                                            fldInsHash.Add("bomItem", .Cells(gvCont.getColIndexByName(gvListQty, "Assy Item")).Text.Trim) 'item assy
                                            'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableBOM, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                                            dbConn.TransactionSQL(dbConn.getInsertSql(tableBOM, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                                        End If
                                    End With
                                Next
                            End If

                        End If
                    Next
                Else 'normal case
                    If .Rows.Count > 1 And Not isBOM Then
                        isMulti = "Y"
                    End If
                    If Not isBOM Then
                        rowLen = .Rows.Count - 1
                    End If
                    'chek for multi mo
                    If isMultiMO Then
                        Dim cnt As Integer = 0
                        For i As Integer = 0 To rowLen
                            Dim cbSel As CheckBox '= CType(gvListMO.Rows(i).Cells(0).FindControl(0), CheckBox)
                            cbSel = .Rows(i).FindControl("cbSel")
                            If cbSel.Checked Then
                                ' If cbSel IsNot Nothing And cbSel.Checked ThencbSel IsNot Nothing And
                                cnt += 1
                            End If
                        Next
                        If cnt = 0 Then
                            show_message.ShowMessage(Page, "Pease select MO!!!", UpdatePanel1)
                            btSave.Enabled = True
                            Exit Sub
                        End If
                    End If
                    'save data for beginning
                    For i As Integer = 0 To rowLen
                        With .Rows(i)
                            If isMultiMO Then
                                Dim cbSel As CheckBox = .FindControl("cbSel")
                                If cbSel IsNot Nothing And Not cbSel.Checked Then
                                    Continue For
                                End If
                            End If
                            'Dim mo() As String = .Cells(ControlForm.getColIndexByName(gvListQty, "MO")).Text.Trim.Split("-")
                            Dim mo() As String = .Cells(getColIndexByNameMO("MO")).Text.Trim.Split("-")
                            fldInsHash = New Hashtable From {
                                {"moType", mo(0)}, 'mo type
                                {"moNo", mo(1)}, 'mo no
                                {"moSeq", mo(2)}, 'mo seq
                                {"wc", wcCode}, 'WC
                                {"mc", mchCode}, 'MC
                                {"dateCode", dateCode}, 'date input auto
                                {"timeCode", timeCode}, 'time input auto
                                {"isSetTime", isSetTime}, 'Set Time
                                {"isMulti", isMulti}, 'Muti
                                {"tranNo", .Cells(gvCont.getColIndexByName(gvListMO, "Transfer No")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim}, 'Transfer Number
                                {"shift", isShift} 'Is shift night
                                }
                            'fldInsHash.Add("isTeam", "Y") 'Is Team=Group
                            Dim pType As String = ""
                            Dim pCode As String = ""
                            Dim point As Integer = 0
                            If isPatial Then
                                pType = "A"
                                Dim tbPoint As TextBox
                                Dim tbSubProcess As TextBox
                                tbSubProcess = .FindControl("tbSubProcess")
                                tbPoint = .FindControl("tbPoint")

                                pCode = tbSubProcess.Text.Trim
                                point = CInt(tbPoint.Text.Trim)

                                'pCode = .Cells(ControlForm.getColIndexByName(gvListMO, "Transfer No")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim
                            End If
                            If isBOM Then
                                pType = "B"
                            End If
                            fldInsHash.Add("processType", pType) '
                            fldInsHash.Add("processCode", pCode) '
                            fldInsHash.Add("point", point) '
                            fldInsHash.Add("dateWork", dateWork) 'datework
                            fldInsHash.Add("createBy", Session("UserName")) 'user save
                            'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(table, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                            dbConn.TransactionSQL(dbConn.getInsertSql(table, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)

                            Dim docNo As Integer
                            docNo = returnDocRecord(mo)
                            getDoc = docNo
                            'save operator
                            Dim mPower As Integer = 0
                            For k As Integer = 1 To empNum
                                Dim fld1 As String = "tbOper" & k
                                Dim tbGet As TextBox
                                tbGet = TryCast(FindControlRecursive(Page, fld1), TextBox)
                                Dim oper As String = tbGet.Text.Trim
                                If oper <> "" Then
                                    whrHash = New Hashtable
                                    fldInsHash = New Hashtable From {
                                        {"moType", mo(0)}, 'mo type
                                        {"moNo", mo(1)}, 'mo no
                                        {"moSeq", mo(2)}, 'mo seq
                                        {"wc", wcCode}, 'WC
                                        {"mc", mchCode}, 'MC
                                        {"dateCode", dateCode}, 'date input auto
                                        {"timeCode", timeCode}, 'time input auto
                                        {"docStart", docNo}, 'doc Start
                                        {"opCode", oper} 'operator code
                                        }
                                    'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableOper, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                                    dbConn.TransactionSQL(dbConn.getInsertSql(tableOper, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                                    mPower += 1
                                End If
                            Next
                            'Save summary
                            whrHash = New Hashtable
                            fldInsHash = New Hashtable From {
                                {"moType", mo(0)}, 'mo type
                                {"moNo", mo(1)}, 'mo no
                                {"moSeq", mo(2)}, 'mo seq
                                {"mc", mchCode}, 'MC
                                {"docStart", docNo}, 'doc start
                                {"manPower", mPower} 'Man Power
                                }
                            'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableSum, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                            dbConn.TransactionSQL(dbConn.getInsertSql(tableSum, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                        End With
                    Next
                    'save BOM
                    If isBOM Then
                        For i As Integer = 0 To .Rows.Count - 1
                            With .Rows(i)
                                'save BOM
                                fldInsHash = New Hashtable
                                whrHash = New Hashtable
                                Dim cbSel As CheckBox
                                cbSel = .FindControl("cbSel")
                                If cbSel.Checked Then
                                    fldInsHash.Add("docStart", getDoc) 'doc start
                                    fldInsHash.Add("bomItem", .Cells(gvCont.getColIndexByName(gvListMO, "Assy Item")).Text.Trim) 'item assy
                                    'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableBOM, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)
                                    dbConn.TransactionSQL(dbConn.getInsertSql(tableBOM, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                                End If
                            End With
                        Next
                    End If

                End If
            End With
        Else 'ending
            'check MO
            If gvListQty.Rows.Count = 0 Then
                show_message.ShowMessage(Page, "MO " & lbMo.Text.Trim & " is not found!!,Please check it again.", UpdatePanel1)
                btSave.Enabled = True
                tbMO.Focus()
                lbMo.Text = ""
                Exit Sub
            End If
            'check for cancle
            If cbCancle.Checked And tbCanReason.Text.Trim = "" Then
                show_message.ShowMessage(Page, "Reason Cancle is empty!!!", UpdatePanel1)
                btSave.Enabled = True
                tbCanReason.Focus()
                Exit Sub
            End If
            With gvListQty
                Dim isMulti As String = "N"
                If .Rows.Count > 1 Then
                    isMulti = "Y"
                End If
                'check Qty
                Dim tbAcceptQty As TextBox,
                    tbDefQty As TextBox,
                    tbScrapQty As TextBox,
                    ddlScrap As DropDownList,
                    lastTime As String,
                    moShow As String,
                    lastID As String
                Dim acceptQty As Integer,
                    defectQty As Integer,
                    scrapQty As Integer,
                    scrapCode As String = ""

                If Not cbSet.Checked And Not cbCancle.Checked Then 'running time end
                    For i As Integer = 0 To .Rows.Count - 1
                        With .Rows(i)
                            tbAcceptQty = .FindControl("tbAcceptQty")
                            tbDefQty = .FindControl("tbDefQty")
                            tbScrapQty = .FindControl("tbScrapQty")
                            ddlScrap = .FindControl("ddlScrapCode")
                            lastTime = .Cells(gvCont.getColIndexByName(gvListQty, "Last Record")).Text.Trim
                            lastID = .Cells(gvCont.getColIndexByName(gvListQty, "Last ID")).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "")
                            moShow = .Cells(gvCont.getColIndexByName(gvListQty, "MO")).Text.Trim

                            If lastID <> "" Then
                                acceptQty = checkNumberic(tbAcceptQty)
                                defectQty = checkNumberic(tbDefQty)
                                scrapQty = checkNumberic(tbScrapQty)
                                If acceptQty = 0 And defectQty = 0 And scrapQty = 0 Then
                                    show_message.ShowMessage(Page, "MO " & moShow & "(" & lastTime & ") had qty(Accept,Defect or Scrap)!!,Please check it again.", UpdatePanel1)
                                    btSave.Enabled = True
                                    tbAcceptQty.Focus()
                                    Exit Sub
                                End If
                                'check select scrap code when scrap qty>0
                                If scrapQty > 0 And ddlScrap.Text = "Select" Then
                                    show_message.ShowMessage(Page, "scrap qty >0 and defect Code in null,Please select it again.", UpdatePanel1)
                                    btSave.Enabled = True
                                    ddlScrap.Focus()
                                    Exit Sub
                                End If

                                If scrapQty = 0 And ddlScrap.Text <> "0" Then
                                    show_message.ShowMessage(Page, "scrap qty =0 and defect Code in not null,Please select it again.", UpdatePanel1)
                                    btSave.Enabled = True
                                    ddlScrap.Focus()
                                    Exit Sub
                                End If
                            End If

                        End With
                    Next
                End If
                Dim rCancle As String = tbCanReason.Text.Trim
                Dim rStatus As String = "1"
                If cbCancle.Checked Then
                    rStatus = "0"
                End If

                Dim sumQty As Decimal = 0,
                    sumDef As Decimal = 0,
                    sumScp As Decimal = 0

                For i As Integer = 0 To .Rows.Count - 1
                    With .Rows(i)
                        'save data for ending
                        Dim mo() As String = .Cells(gvCont.getColIndexByName(gvListQty, "MO")).Text.Trim.Split("-")
                        tbAcceptQty = .FindControl("tbAcceptQty")
                        tbDefQty = .FindControl("tbDefQty")
                        tbScrapQty = .FindControl("tbScrapQty")
                        ddlScrap = .FindControl("ddlScrapCode")
                        lastTime = .Cells(gvCont.getColIndexByName(gvListQty, "Last Record")).Text.Trim

                        acceptQty = checkNumberic(tbAcceptQty)
                        defectQty = checkNumberic(tbDefQty)
                        scrapQty = checkNumberic(tbScrapQty)

                        If Not cbCancle.Checked Then
                            scrapCode = ddlScrap.Text.Replace("Select", "")
                        End If

                        Dim docNo As String = .Cells(gvCont.getColIndexByName(gvListQty, "Last ID")).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "")
                        If docNo = "" Then
                            Continue For
                        End If

                        whrHash = New Hashtable
                        fldInsHash = New Hashtable From {
                            {"moType", mo(0)}, 'mo type
                            {"moNo", mo(1)}, 'mo no
                            {"moSeq", mo(2)}, 'mo seq
                            {"wc", wcCode}, 'WC
                            {"mc", mchCode}, 'MC
                            {"acceptQty", acceptQty}, 'accept qty
                            {"defectQty", defectQty}, 'defect qty ====>> Return Qty
                            {"scrapQty", scrapQty}, 'scrap qty
                            {"scrapCode", scrapCode}, 'scrap code
                            {"dateCode", dateCode}, 'date input auto
                            {"timeCode", timeCode} 'time input auto
                            }
                        Dim isBreakTime As String = "N"
                        If cbBreakTime.Checked Then
                            isBreakTime = "Y"
                        End If
                        fldInsHash.Add("breakTime", isBreakTime) 'break Time
                        fldInsHash.Add("isSetTime", isSetTime) 'set Time
                        fldInsHash.Add("isMulti", isMulti) 'Muti

                        fldInsHash.Add("tranNo", .Cells(gvCont.getColIndexByName(gvListQty, "Transfer No")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim) 'Transfer Number
                        fldInsHash.Add("shift", isShift) 'Is shift night
                        'If isPatial Then
                        '    pType = "A"
                        'End If
                        'If isBOM Then
                        '    pType = "B"
                        'End If
                        fldInsHash.Add("processType", .Cells(gvCont.getColIndexByName(gvListQty, "Process Type")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim) '
                        fldInsHash.Add("processCode", .Cells(gvCont.getColIndexByName(gvListQty, "Point Name/Opt")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim) '
                        fldInsHash.Add("point", .Cells(gvCont.getColIndexByName(gvListQty, "Point")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Replace(",", "").Trim) '
                        fldInsHash.Add("dateWork", dateWork) 'datework
                        fldInsHash.Add("createBy", Session("UserName")) 'user save
                        dbConn.TransactionSQL(dbConn.getInsertSql(table, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                        'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(table, fldInsHash, whrHash, "I"), Conn_SQL.MIS_ConnectionString)

                        'summary
                        fldInsHash = New Hashtable
                        whrHash = New Hashtable
                        Dim docNoEnd As Integer = returnDocRecord(mo)
                        Dim dateStart As Date = dateCont.strToDateTime(lastTime)
                        Dim dateEnd As Date = dateCont.strToDateTime(dateCode & " " & timeCode)
                        Dim workTime As Decimal = dateCont.getTime(dateStart, dateEnd)
                        If Not cbBreakTime.Checked Then
                            Dim shift As String = .Cells(gvCont.getColIndexByName(gvListQty, "Shift")).Text.Replace("&amp;", "").Replace("&nbsp;", "").Trim
                            workTime -= dateCont.getBreakTime(dateStart, dateEnd, shift)
                        End If
                        Dim fldTime As String = "workTime"
                        If cbSet.Checked Then
                            fldTime = "setTime"
                        End If
                        whrHash.Add("docNo", docNo) 'doc no
                        fldInsHash.Add("docEnd", docNoEnd) 'doc end
                        fldInsHash.Add(fldTime, getTimeUsage(i, workTime)) 'work time record
                        fldInsHash.Add("recStatus", rStatus) 'Record Status
                        fldInsHash.Add("reasonCancel::N", rCancle) 'Reason for cancle
                        'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableSum, fldInsHash, whrHash, "U"), Conn_SQL.MIS_ConnectionString)
                        dbConn.TransactionSQL(dbConn.getUpdateSql(tableSum, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                        'update qty to operator
                        SQL = "select S.docStart,count(*) cnt from " & tableSum & " S left join " & tableOper & " O on O.docStart = S.docStart where S.docNo='" & docNo & "' group by S.docStart  "
                        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                        dt = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                        If dt.Rows.Count > 0 Then
                            Dim cntOper As Decimal = dt.Rows(0).Item("cnt")
                            Dim avgQty As Decimal = If(acceptQty > 0, If(cntOper = 1, acceptQty, Math.Round(acceptQty / cntOper, 2)), 0)
                            Dim avgDef As Decimal = If(defectQty > 0, If(cntOper = 1, defectQty, Math.Round(defectQty / cntOper, 2)), 0)
                            Dim avgScp As Decimal = If(scrapQty > 0, If(cntOper = 1, scrapQty, Math.Round(scrapQty / cntOper, 2)), 0)
                            whrHash = New Hashtable From {
                                {"docStart", dt.Rows(0).Item("docStart")} 'doc start
                                }
                            fldInsHash = New Hashtable From {
                                {"acceptQty", avgQty},
                                {"defectQty", avgDef},
                                {"scrapQty", avgScp}
                            }
                            dbConn.TransactionSQL(dbConn.getUpdateSql(tableOper, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                            'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableOper, fldInsHash, whrHash, "U"), Conn_SQL.MIS_ConnectionString)

                        End If
                    End With
                Next
            End With
        End If
        show_message.ShowMessage(Page, "Save Complete!!", UpdatePanel1)
        ClearKey()
        ddlWc_SelectedIndexChanged(sender, e)
        btSave.Enabled = True
    End Sub

    Private Function getTimeUsage(line As Integer, workTime As Integer) As Integer
        Dim tbAcceptQty As TextBox,
            tbDefQty As TextBox,
            tbScrapQty As TextBox,
            tempTime As Integer

        With gvListQty
            If .Rows.Count > 1 Then
                If cbSet.Checked Or cbCancle.Checked Then
                    tempTime = Math.Floor(workTime / .Rows.Count)
                Else
                    If usageTime = 0 Then
                        Dim qtySum As Integer = 0
                        For i As Integer = 0 To .Rows.Count - 1
                            With .Rows(i)
                                tbAcceptQty = .Cells(6).FindControl("tbAcceptQty")
                                tbDefQty = .Cells(7).FindControl("tbDefQty")
                                tbScrapQty = .Cells(8).FindControl("tbScrapQty")

                                Dim aQty As Integer = 0,
                                    dQty As Integer = 0,
                                    sQty As Integer = 0
                                If tbAcceptQty.Text <> "" And IsNumeric(tbAcceptQty.Text.Trim) Then
                                    aQty = CInt(tbAcceptQty.Text.Trim)
                                End If

                                If tbDefQty.Text <> "" And IsNumeric(tbDefQty.Text.Trim) Then
                                    dQty = CInt(tbDefQty.Text.Trim)
                                End If

                                If tbScrapQty.Text <> "" And IsNumeric(tbScrapQty.Text.Trim) Then
                                    sQty = CInt(tbScrapQty.Text.Trim)
                                End If

                                qtySum += aQty
                                qtySum += dQty
                                qtySum += sQty

                            End With
                        Next
                        If qtySum > 0 Then
                            usageTime = Math.Floor(workTime / qtySum)
                        End If
                    End If
                    Dim qty As Integer = 0
                    With .Rows(line)
                        tbAcceptQty = .Cells(5).FindControl("tbAcceptQty")
                        tbDefQty = .Cells(6).FindControl("tbDefQty")
                        tbScrapQty = .Cells(8).FindControl("tbScrapQty")

                        Dim aQty As Integer = 0,
                            dQty As Integer = 0,
                            sQty As Integer = 0

                        If tbAcceptQty.Text <> "" And IsNumeric(tbAcceptQty.Text.Trim) Then
                            aQty = CInt(tbAcceptQty.Text.Trim)
                        End If

                        If tbDefQty.Text <> "" And IsNumeric(tbDefQty.Text.Trim) Then
                            dQty = CInt(tbDefQty.Text.Trim)
                        End If

                        If tbScrapQty.Text <> "" And IsNumeric(tbScrapQty.Text.Trim) Then
                            sQty = CInt(tbScrapQty.Text.Trim)
                        End If

                        qty += aQty
                        qty += dQty
                        qty += sQty
                    End With
                    tempTime = qty * usageTime
                End If
            Else '=1
                tempTime = workTime
            End If
        End With
        Return tempTime

    End Function

    Private Function checkNumberic(tb As TextBox) As Integer
        Dim valReturn As Decimal = 0
        If Not cbCancle.Checked Then
            Dim val As String = tb.Text.Trim
            If val <> "" And IsNumeric(val) Then
                valReturn = CInt(val)
            End If
        End If
        Return valReturn
    End Function

    Sub ClearKey()
        Dim SQL As String = "select WC from UserDailyAuthority where Id='" & Session("UserId") & "' "
        Dim dt As DataTable
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        With dt
            If .Rows.Count > 0 Then
                With .Rows(0)
                    Dim WC As String = .Item("WC").ToString.Trim
                    SQL = "select rtrim(MD001) MD001,MD001+':'+MD002 as MD002 from CMSMD where MD001 in ('" & WC.Replace(",", "','") & "') order by MD001 "
                    Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                    ddlCont.showDDL(ddlWc, dt2, "MD002", "MD001")
                    ddlCont.showDDL(ddlWcChk, dt2, "MD002", "MD001")
                End With
            End If
        End With

        tbMO.Text = ""
        lbMo.Text = ""
        lbWC.Text = ""
        lbTypeWC.Text = ""
        lbChk.Text = ""

        'tbSub.Text = ""
        'tbSub.Enabled = False

        tbOperInd.Text = ""
        tbOperInd.Enabled = False

        'machine.setObject = ""
        ddlMC.Items.Clear()
        For i As Integer = 1 To 12
            Dim fld1 As String = "tbOper" & i
            Dim tbGet As TextBox
            tbGet = TryCast(FindControlRecursive(Page, fld1), TextBox)
            tbGet.Text = ""
        Next

        cbCancle.Checked = False
        tbCanReason.Text = ""

        gvListMO.DataSource = ""
        gvListMO.DataBind()

        gvListQty.DataSource = ""
        gvListQty.DataBind()

        'ddlScrapCode.Enabled = True
        aboutWC()
        'MultiView2.SetActiveView(View3)
        MultiView2.Visible = False

        cbSet.Enabled = False
        cbSet.Checked = False

        cbShift.Enabled = False
        cbShift.Checked = False

        cbInd.Enabled = False
        cbInd.Checked = False

        btSave.Visible = False

    End Sub

    Sub ClearCheck()
        tbMOChk.Text = ""
        'tbOperChk.Text = ""
        gvShow.DataSource = ""
        gvShow.DataBind()
        CountRow1.RowCount = gvCont.rowGridview(gvShow)
        Date1.dateVal = ""
    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click

        If tbMOChk.Text.Trim.Replace("-", "").Replace("_", "") = "" And Date1.dateVal = "" Then
            show_message.ShowMessage(Page, "Data for search is null.", UpdatePanel1)
            'tbOperChk.Focus()
            Exit Sub
        End If
        Dim WHR As String = ""
        If tbMOChk.Text.Trim.Replace("-", "").Replace("_", "") <> "" Then
            Dim mo() As String = tbMOChk.Text.Trim.Split("-")
            WHR &= dbConn.Where("P.moType", mo(0), True, False)
            WHR &= dbConn.Where("P.moNo", mo(1), True, False)
            WHR &= dbConn.Where("P.moSeq", mo(2), True, False)
        End If

        'If tbOperChk.Text.Trim <> "" Then
        '    'WHR = WHR & Conn_SQL.Where("R1.opCode", tbOperChk)
        'End If
        WHR &= dbConn.Where("R1.wc", ddlWcChk)
        WHR &= dbConn.Where("P.mc", machineChk.getObject)

        If Date1.dateVal <> "" Then
            WHR &= dbConn.Where("R1.dateCode", Date1.dateVal, True, False)
            'WHR = WHR & " and "
        End If
        Dim SQL As String
        '= " select docNo A,moType B,moNo C,moSeq D,opCode E,acceptQty F,defectQty G,defectCode H," & _
        '  " dateCode I,timeCode J,createBy K, M.TA035 L,wc M,mc N " & _
        '  " from ProductionProcessRecord " & _
        '  " left join " & Conn_SQL.DBMain & "..SFCTA S on S.TA001=moType and S.TA002=moNo and S.TA003=moSeq  " & _
        '  " left join " & Conn_SQL.DBMain & "..MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
        '  " where 1=1 " & WHR & " order by moType,moNo,moSeq,dateCode,timeCode "
        'isnull(R2.dateCode,'') J,
        SQL = " select P.docNo DN ,rtrim(P.moType)+'-'+rtrim(P.moNo)+'-'+rtrim(P.moSeq) A,case when substring(R1.isSetTime,1,1)='Y' then 'Setting' else 'Running' end B," &
              " R1.tranNo C ,R1.wc D,P.mc E, " &
              " case when substring(R1.isSetTime,1,1)='Y' then case when P.setTime=0 then 0 else floor(P.setTime/60) end " &
              " else case when P.workTime=0 then 0 else floor(P.workTime/60) end end F," &
              " floor(isnull(P.lossTime,0)/60) F1,R1.dateCode G,R1.timeCode H, " &
              " P.manPower I,isnull(R2.timeCode,'') K, " &
              " isnull(R2.acceptQty,0) L,isnull(R2.defectQty,0) M,M.TA035 O,C.MW002 P," &
              " case when R1.isMulti='Y' then 'Multi' else 'Single' end S,isnull(R2.scrapQty,0) T,isnull(R2.scrapCode,'') U ," &
              " case when isnull(P.recStatus,'1')='1' then 'NO' else 'YES' end Y,isnull(P.reasonCancel,'') Z,R1.processCode PT,R1.point PC " &
              " from ProductionProcessSum P " &
              " left join ProductionProcessRecord R1 on R1.docNo=P.docStart " &
              " left join ProductionProcessRecord R2 on R2.docNo=P.docEnd " &
              " left join " & VarIni.ERP & "..SFCTA S on S.TA001=P.moType and S.TA002=P.moNo and S.TA003=P.moSeq  " &
              " left join " & VarIni.ERP & "..MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " &
              " left join " & VarIni.ERP & "..CMSMW C on C.MW001=S.TA004 " &
              " where 1=1 " & WHR &
              " order by R1.dateCode,R1.timeCode,P.moType,P.moNo,P.moSeq "
        gvCont.ShowGridView(gvShow, SQL, VarIni.DBMIS)
        CountRow1.RowCount = gvCont.rowGridview(gvShow)
        Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Protected Sub btResetReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btResetReport.Click
        ClearCheck()
    End Sub

    Protected Sub btResetKey_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btResetKey.Click
        ClearKey()
        ddlWc_SelectedIndexChanged(sender, e)
    End Sub

    'Protected Sub btUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btUpdate.Click
    '    If tbMOChk.Text.Trim.Replace("-", "").Replace("_", "") = "" And Date1.dateVal = "" Then
    '        show_message.ShowMessage(Page, "Data for search is null.", UpdatePanel1)
    '        'tbOperChk.Focus()
    '        Exit Sub
    '    End If
    '    Dim WHR As String = ""
    '    If tbMOChk.Text.Trim.Replace("-", "").Replace("_", "") <> "" Then
    '        Dim mo() As String = tbMOChk.Text.Trim.Split("-")
    '        WHR = WHR & Conn_SQL.Where("S.moType", mo(0), True, False)
    '        WHR = WHR & Conn_SQL.Where("S.moNo", mo(1), True, False)
    '        WHR = WHR & Conn_SQL.Where("S.moSeq", mo(2), True, False)
    '    End If
    '    'WHR = WHR & Conn_SQL.Where("R1.opCode", tbOperChk)
    '    If Date1.dateVal <> "" Then
    '        WHR = WHR & Conn_SQL.Where("R.dateCode", Date1.dateVal, True, False)
    '    End If
    '    Dim SQL As String = " select docNo A,moType B,moNo C,moSeq D,opCode E,acceptQty F,defectQty G,defectCode H," & _
    '                       " dateCode I,timeCode J,createBy K, M.TA035 L,wc M,mc N " & _
    '                       " from ProductionProcessRecord " & _
    '                       " left join " & Conn_SQL.DBMain & "..SFCTA S on S.TA001=moType and S.TA002=moNo and S.TA003=moSeq  " & _
    '                       " left join " & Conn_SQL.DBMain & "..MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
    '                       " where 1=1 " & WHR & " order by moType,moNo,moSeq,dateCode,timeCode "

    '    SQL = " select S.docNo,S.moType,S.moNo,S.moSeq,R.dateCode,R.timeCode,R.docNo d2n from ProductionProcessSum S left join ProductionProcessRecord R on R.docNo=S.docStart " & _
    '          " where  1=1 " & WHR
    '    'S.docEnd=0
    '    Dim dt As DataTable
    '    dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        With dt.Rows(i)
    '            Dim whrHash As Hashtable = New Hashtable,
    '                fldInsHash As Hashtable = New Hashtable,
    '                dd As New DataTable,
    '                docNo As String = .Item("d2n").ToString.Trim

    '            SQL = "select top 1 docNo,dateCode,timeCode,breakTime from " & table & " where moType='" & .Item("moType").ToString.Trim & "' and moNo='" & .Item("moNo").ToString.Trim & "' and moSeq='" & .Item("moSeq").ToString.Trim & "' and docNo>'" & docNo & "'  order by dateCode,timeCode"
    '            dd = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
    '            'docNo = Conn_SQL.Get_value(SQL, Conn_SQL.MIS_ConnectionString)
    '            'Dim dCode As String = ""
    '            If dd.Rows.Count > 0 Then
    '                Dim dateStart As Date = configDate.strToDateTime(.Item("dateCode").ToString.Trim & " " & .Item("timeCode").ToString.Trim)
    '                Dim dateEnd As Date = configDate.strToDateTime(dd.Rows(0).Item("dateCode").ToString.Trim & " " & dd.Rows(0).Item("timeCode").ToString.Trim)
    '                Dim workTime As Decimal = configDate.getTime(dateStart, dateEnd)
    '                If dd.Rows(0).Item("dateCode").ToString.Trim = "N" Then
    '                    workTime = workTime - configDate.getBreakTime(dateStart, dateEnd)
    '                End If

    '                'SQL = "select top 1 docNo from " & table & " where moType='" & mo(0) & "' and moNo='" & mo(1) & "' and moSeq='" & mo(2) & "' and mc='" & mchCode & "' order by dateCode desc,timeCode desc"
    '                'docNo = Conn_SQL.Get_value(SQL, Conn_SQL.MIS_ConnectionString)

    '                whrHash.Add("docNo", .Item("docNo").ToString.Trim) 'doc no
    '                fldInsHash.Add("docEnd", dd.Rows(0).Item("docNo").ToString.Trim) 'doc end
    '                fldInsHash.Add("workTime", workTime) 'time record
    '                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tableSum, fldInsHash, whrHash, "U"), Conn_SQL.MIS_ConnectionString)
    '            End If
    '        End With
    '    Next
    'End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim SQL As String = ""
        Dim dateToday As String = Date.Now.Date.ToString("yyyyMMdd")
        SQL = " select S.docNo,S.docStart from ProductionProcessSum S left join ProductionProcessRecord R on R.docNo=S.docStart " &
              " where  S.docEnd=0 and R.dateCode<'" & dateToday & "' "
        Dim dt As DataTable
        ' dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        dt = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim USQL As String
            USQL = "update ProductionProcessSum set docEnd=" & dt.Rows(i).Item("docStart").ToString.Trim & " where docNo='" & dt.Rows(i).Item("docNo").ToString.Trim & "'"
            'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
            dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Next
    End Sub

    Sub aboutWC()
        lbWC.Text = ddlWc.Text
        'mc
        Dim wc As String = ddlWc.Text.Trim,
            SQL As String = ""

        If wc = "" Then
            ddlMC.Items.Clear()
        Else
            If wc = "W07" Or wc = "W25" Then
                wc = "W07,W25"
            End If
            SQL = "select rtrim(MX001) MX001,rtrim(MX001)+' : '+rtrim(MX003) MX003 from CMSMX where MX002 in ('" & wc.Replace(",", "','") & "') and MX006<>'CANCEL' order by MX001"
            ddlCont.showDDL(ddlMC, SQL, VarIni.ERP, "MX003", "MX001")
        End If

        ' machine.setObject = lbWC.Text.Trim
    End Sub

    'Sub setEnableText(ByRef tb As TextBox, ByVal setEnable As Boolean)
    '    tb.Enabled = setEnable
    '    If setEnable Then
    '        tb.CssClass = ""
    '    Else
    '        tb.CssClass = "textEnableFalse"
    '    End If
    'End Sub

    Sub clearGridview()
        MultiView2.Visible = False
        gvListMO.DataSource = ""
        gvListMO.DataBind()
        gvListQty.DataSource = ""
        gvListQty.DataBind()
    End Sub

    Protected Sub ddlWc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlWc.SelectedIndexChanged
        clearGridview()
        aboutWC()
        tbOperInd.Enabled = False
        'tbSub.Text = ""
        tbOperInd.Text = ""
        tbMO.Text = ""
        ddlMC_SelectedIndexChanged(sender, e)
        'Dim wc As String = ddlWc.Text.Trim,
        '    SQL As String = "",
        '    dt As DataTable

        'SQL = "select CMSMD.UDF01,CMSMD.UDF02,CMSMD.UDF03 from CMSMD where CMSMD.MD001='" & lbWC.Text & "' "
        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'With dt
        '    If .Rows.Count > 0 Then
        '        Dim isShift As Boolean = True,
        '            isMch As Boolean = True,
        '            isIndi As Boolean = False,
        '            isSub As Boolean = False
        '        With .Rows(0)
        '            Dim UDF01 As String = .Item("UDF01").ToString.Trim
        '            If lbWC.Text = "W05" Then
        '                UDF01 = "A"
        '            End If
        '            If UDF01 = "A" Then 'check for sub process
        '                isSub = True
        '            End If
        '            If UDF01 = "M" Then 'check mch for work
        '                isMch = False
        '                isIndi = True
        '            End If

        '            tbSub.Text = ""

        '            tbSub.Enabled = isSub
        '            cbInd.Checked = False
        '            cbInd.Enabled = isIndi
        '            tbOperInd.Text = ""
        '            'tbOperInd.Enabled = isIndi
        '            cbSet.Checked = False
        '            cbSet.Enabled = isMch

        '            If .Item("UDF03").ToString.Trim = "N" Then 'check work center have shift(N=Day only,Y or '' = Day and night shift)
        '                isShift = False
        '            End If
        '            cbShift.Checked = False
        '            cbShift.Enabled = isShift

        '        End With
        '    End If
        'End With
        'Dim wcType As String = Conn_SQL.Get_value(SQL, Conn_SQL.ERP_ConnectionString)
        'lbTypeWC.Text = wcType.Trim
    End Sub

    Protected Sub ddlWcChk_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlWcChk.SelectedIndexChanged
        machineChk.setObjectWithAll = ddlWcChk.Text.Trim
        gvShow.DataSource = ""
        gvShow.DataBind()
        CountRow1.RowCount = gvCont.rowGridview(gvShow)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    'Protected Sub btReCal_Click(sender As Object, e As EventArgs) Handles btReCal.Click
    '    System.Threading.Thread.Sleep(5000)

    '    If tbMOChk.Text.Trim.Replace("-", "").Replace("_", "") = "" And Date1.dateVal = "" Then
    '        show_message.ShowMessage(Page, "Data for search is null.", UpdatePanel1)
    '        'tbOperChk.Focus()
    '        Exit Sub
    '    End If
    '    Dim WHR As String = ""
    '    If tbMOChk.Text.Trim.Replace("-", "").Replace("_", "") <> "" Then
    '        Dim mo() As String = tbMOChk.Text.Trim.Split("-")
    '        WHR = WHR & Conn_SQL.Where("P.moType", mo(0), True, False)
    '        WHR = WHR & Conn_SQL.Where("P.moNo", mo(1), True, False)
    '        WHR = WHR & Conn_SQL.Where("P.moSeq", mo(2), True, False)
    '    End If

    '    'If tbOperChk.Text.Trim <> "" Then
    '    '    'WHR = WHR & Conn_SQL.Where("R1.opCode", tbOperChk)
    '    'End If
    '    WHR = WHR & Conn_SQL.Where("R1.wc", ddlWcChk)
    '    WHR = WHR & Conn_SQL.Where("P.mc", machineChk.getObject)

    '    If Date1.dateVal <> "" Then
    '        WHR = WHR & Conn_SQL.Where("R1.dateCode", Date1.dateVal, True, False)
    '        'WHR = WHR & " and "
    '    End If
    '    Dim SQL As String

    '    SQL = " select P.docNo,R1.dateCode D1 ,R1.timeCode T1 ,R2.dateCode D2 ,R2.timeCode T2,R1.breakTime " & _
    '          " from ProductionProcessSum P " & _
    '          " left join ProductionProcessRecord R1 on R1.docNo=P.docStart " & _
    '          " left join ProductionProcessRecord R2 on R2.docNo=P.docEnd " & _
    '          " where 1=1 " & WHR & _
    '          " order by R1.dateCode,R1.timeCode,P.moType,P.moNo,P.moSeq "

    '    Dim dt As DataTable
    '    dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)

    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        With dt.Rows(i)
    '            Dim dateStart As Date = configDate.strToDateTime(.Item("D1").ToString.Trim & " " & .Item("T1").ToString.Trim)
    '            Dim dateEnd As Date = configDate.strToDateTime(.Item("D2").ToString.Trim & " " & .Item("T2").ToString.Trim)
    '            Dim workTime As Decimal = configDate.getTime(dateStart, dateEnd)
    '            If .Item("breakTime").ToString.Trim.Substring(0, 1) = "N" Then
    '                workTime = workTime - configDate.getBreakTime(dateStart, dateEnd)
    '            End If
    '            Dim USQL As String = "update ProductionProcessSum set workTime='" & workTime & "' where docNo='" & .Item("docNo").ToString.Trim & "' "
    '            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
    '        End With
    '    Next
    '    btSearch_Click(sender, e)
    'End Sub

    Protected Sub cbInd_CheckedChanged(sender As Object, e As EventArgs) Handles cbInd.CheckedChanged
        tbOperInd.Enabled = cbInd.Checked
        clearGridview()
    End Sub

    Protected Sub ddlMC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMC.SelectedIndexChanged
        Dim SQL As String = " select UDF01,UDF02,UDF03,UDF04 from CMSMX where MX001='" & ddlMC.Text.Trim & "' and MX002='" & lbWC.Text & "' "
        Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        If dr IsNot Nothing Then
            Dim isShift As Boolean = True,
                isMch As Boolean = false,
                isIndi As Boolean = False,
                isSub As Boolean = False
            Dim UDF01 As String = dtCont.IsDBNullDataRow(dr, "UDF01")
            Dim UDF03 As String = dtCont.IsDBNullDataRow(dr, "UDF03")
            Dim UDF04 As String = dtCont.IsDBNullDataRow(dr, "UDF04")
            'If lbWC.Text = "W05" Then
            '    UDF04 = "A"
            'End If
            'If UDF04 = "A" Then 'check for sub process
            '    isSub = True
            'End If
            If UDF01 = "W" Then 'check mch for work
                isMch = False
                isIndi = True
            End If
            'tbSub.Text = ""
            'tbSub.Enabled = isSub
            cbInd.Checked = False
            cbInd.Enabled = isIndi
            tbOperInd.Text = ""
            'tbOperInd.Enabled = isIndi
            cbSet.Checked = False
            cbSet.Enabled = isMch

            If UDF03 = "N" Then 'check work center have shift(N=Day only,Y or '0' or '' = Day and night shift)
                isShift = False
            End If
            cbShift.Checked = False
            cbShift.Enabled = isShift
        End If
    End Sub

    Public Function getColIndexByNameMO(ByVal name As String) As Integer
        For i As Integer = 0 To gvListMO.Columns.Count - 1
            If gvListMO.Columns(i).HeaderText.ToLower.Trim = name.ToLower.Trim Then
                Return i
            End If
        Next
        Return -1
    End Function

    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row

            If .RowType = DataControlRowType.DataRow Then
                Dim hplLoss As HyperLink = CType(.FindControl("hplLoss"), HyperLink)
                If Not IsNothing(hplLoss) And Not IsDBNull(.DataItem("DN")) And .DataItem("F") > 0 Then
                    Dim link As String = ""
                    link &= "&docno= " & .DataItem("DN").ToString.Trim
                    link &= "&endtime=Y"
                    'link &= "&jobtype= " & .DataItem("S").ToString.Trim
                    ' &= "&setting= " & .DataItem("B").ToString.Trim
                    hplLoss.NavigateUrl = "ProductionRecordPopup2.aspx?height=150&width=350" & link
                    .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                    .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
                Else
                    hplLoss.Text = ""
                End If
                hplLoss.Attributes.Add("title", .DataItem("A"))

            End If
        End With
    End Sub

    Private Sub gvListQty_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvListQty.RowDataBound
        With e.Row

            If .RowType = DataControlRowType.DataRow Then
                Dim hplLoss As HyperLink = CType(.FindControl("hplLoss"), HyperLink)
                If Not IsNothing(hplLoss) And Not IsDBNull(.DataItem("H")) Then
                    Dim link As String = ""
                    link &= "&docno= " & .DataItem("H").ToString.Trim
                    link &= "&endtime=N"

                    hplLoss.NavigateUrl = "ProductionRecordPopup2.aspx?height=150&width=350" & link
                    .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                    .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
                Else
                    hplLoss.Text = ""
                End If
                hplLoss.Attributes.Add("title", .DataItem("A"))

            End If
        End With
    End Sub

End Class