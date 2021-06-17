Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class WorkLoading
    Inherits System.Web.UI.Page
    Dim listNotWC As String = "'W08','W19','W20','W21','W24','W18','W26','W22','W31','W51','W52','W53','W54','W55','W56','W57','W58','W59','W60'"
    Dim listTye As String = "'5102','5104','5106','5108','5109','5192','5194','5198','5196','5199'"

    Dim cblCont As New CheckBoxListControl
    Dim dbConn As New DataConnectControl
    Dim gvCont As New GridviewControl
    Dim dateCont As New DateControl


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            Else
                Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
                cblCont.showCheckboxList(cblWc, SQL, VarIni.ERP, "MD002", "MD001", 4)

                SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52')  order by MQ002"
                cblCont.showCheckboxList(cblType, SQL, VarIni.ERP, "MQ002", "MQ001", 5)
                btExport.Visible = False
            End If
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = ""

        WHR = dbConn.WHERE_IN("SFC.TA006", cblWc,, True)
        WHR &= dbConn.WHERE_IN("SFC.TA001", cblType,, True)
        WHR &= dbConn.WHERE_LIKE("TC004", tbCust)
        WHR &= dbConn.WHERE_BETWEEN("SFC.TA009", UcDateFrom.Text, UcDateTo.Text)

        Dim con As String = ddlCon.Text.Trim
        If con = "2" Then 'on process
            WHR &= " and SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058>0 "
        ElseIf con = "3" Then 'on pending
            WHR &= " and SFC.TA010+SFC.TA013+SFC.TA016-SFC.TA011-SFC.TA012-SFC.TA014-SFC.TA015-SFC.TA048-SFC.TA056-SFC.TA058=0 "
        End If

        SQL = " select SFC.TA006 F01,MAX(MD002) F02,COUNT(distinct MOC.TA006) F03,COUNT( distinct MOC.TA001+MOC.TA002) F04, " &
              " SUM(MOC.TA015) F05,SUM(SFC.TA022)/60 F06,SUM(SFC.TA035)/60 F07,MAX(mancapacity) F08,MAX(capacity) F09, " &
              " case when MAX(capacity)=0 then 0 else SUM(SFC.TA022)/MAX(mancapacity*60) end F10, " &
              " case when MAX(mancapacity)=0 then 0 else SUM(SFC.TA035)/MAX(capacity*60) end F11, MAX('" & UcDateTo.Text & "') F12 " &
              " from SFCTA SFC left join MOCTA MOC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
              " left join CMSMD on MD001=SFC.TA006 left join COPTC on TC001=MOC.TA026 and TC002=MOC.TA027 " &
              " left join " & VarIni.DBMIS & ".dbo.MachineCapacity on wc=SFC.TA006 " &
              " where  SFC.TA032 = 'N' and (MOC.TA011 not in ('y','Y')) and SFC.TA005 = '1' " & WHR &
              "group by SFC.TA006 order by SFC.TA006"
        gvCont.ShowGridView(gvShow, SQL, VarIni.ERP)
        'lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplLink"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("F01")) Then
                    Dim link As String = ""
                    Dim wc As String = .DataItem("F01")
                    link = link & "&wc= " & wc
                    link = link & "&fdate= " & UcDateFrom.Text
                    link = link & "&tdate= " & UcDateTo.Text.Trim
                    link = link & "&cust= " & tbCust.Text.Trim
                    link = link & "&mcon= " & ddlCon.Text.Trim

                    Dim typeSel As String = ""
                    For Each boxItem As ListItem In cblType.Items
                        If boxItem.Selected Then
                            typeSel = typeSel & "" & CStr(boxItem.Value.Trim) & ","
                        End If
                    Next
                    link = link & "&motype= " & typeSel

                    hplDetail.NavigateUrl = "WorkLoadingPop.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", .DataItem("F02").ToString.Trim)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("workLoading" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class