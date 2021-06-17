Public Class SaleDeliveryStatusDN
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim chrConn As String = Chr(8)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            btExport.Visible = False

        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim SQL As String = "",
            WHR As String = "",
            CusCode As String = txtCusCode.Text.Trim,
            Item As String = txtItem.Text.Trim,
            Spec As String = txtSpec.Text.Trim,
            SONo As String = txtSONo.Text.Trim,
            DueDate As String = txtDate.Text.Trim

        WHR = WHR & Conn_SQL.Where("COPTD.TD001", cblSaleType)

        If SONo <> "" Then
            WHR = WHR & " and COPTH.TH015 like '" & SONo & "%' "
        End If

        If Item <> "" Then
            WHR = WHR & " and INVMB.MB001 like '" & Item & "%' "
        End If

        If CusCode <> "" Then
            WHR = WHR & " and COPMA.MA001 like '" & CusCode & "%' "
        End If

        If Spec <> "" Then
            WHR = WHR & " and INVMB.MB003 like '" & Spec & "%' "
        End If

        If txtDate.Text <> "" Then
            WHR = WHR & Conn_SQL.Where("COPTG.TG042 < ", configDate.dateFormat2(txtDate.Text))
        End If

        Dim ordType As String = "",
            cnt As Integer = 0

        For Each boxItem As ListItem In cblSaleType.Items
            If boxItem.Selected = True Then
                ordType = ordType & "'" & CStr(boxItem.Value) & "',"
                cnt = cnt + 1
            End If
        Next
        If cnt > 0 Then
            ordType = ordType.Substring(0, ordType.Length - 1)
        End If

        SQL = " select COPTH.TH014+'-'+COPTH.TH015+'-'+COPTH.TH016 as 'S01'," & _
            " (SUBSTRING(INVMB.MB001,1,14)+'-'+SUBSTRING(INVMB.MB001,15,2)) as 'S04'," & _
            " (SUBSTRING(COPTG.TG042,7,2)+'-'+SUBSTRING(COPTG.TG042,5,2)+'-'+SUBSTRING(COPTG.TG042,1,4)) as 'S03'," & _
            " INVMB.MB003 as 'S05', INVMC.MC007 as 'S06', COPTH.TH008 as 'S07'," & _
            " COPTH.TH007 as 'S08', COPTH.TH056 as 'S09',  " & _
            " COPMA.MA001+'-'+COPMA.MA002  as 'S10',(INVMC.MC007-COPTH.TH008) as 'S11'," & _
            " COPTG.TG001+'-'+COPTG.TG002 as 'S12'" & _
            " FROM COPTH " & _
            " left join INVMB on INVMB.MB001 = COPTH.TH004 and INVMB.MB003 = COPTH.TH006 " & _
            " left join COPTD on COPTD.TD001 = COPTH.TH014 and COPTD.TD002 = COPTH.TH015 and COPTD.TD003 = COPTH.TH016 " & _
            " left join COPTG on COPTG.TG001 = COPTH.TH001 and COPTG.TG002 = COPTH.TH002 " & _
            " left join COPMA on COPMA.MA001 = COPTG.TG004 " & _
            " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017 " & _
            " WHERE COPTG.TG023 = 'N' " & WHR & _
            " order by COPTH.TH014,COPTH.TH015,COPTH.TH016,INVMB.MB001"

        ControlForm.ShowGridView(gvShow, Sql, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("SaleDeliveryStatusDN" & Session("UserName"), gvShow)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim paraName As String = "",
           chrConn As String = Chr(8),
           whr As String = "",
            CusCode As String = txtCusCode.Text.Trim,
            Item As String = txtItem.Text.Trim,
            Spec As String = txtSpec.Text.Trim,
            SONo As String = txtSONo.Text.Trim,
            DueDate As String = txtDate.Text.Trim

        whr = whr & Conn_SQL.Where("COPTD.TD001", cblSaleType)

        If SONo <> "" Then
            whr = whr & " and COPTH.TH015 like '" & SONo & "%' "
        End If

        If Item <> "" Then
            whr = whr & " and INVMB.MB001 like '" & Item & "%' "
        End If

        If CusCode <> "" Then
            whr = whr & " and COPMA.MA001 like '" & CusCode & "%' "
        End If

        If Spec <> "" Then
            whr = whr & " and INVMB.MB003 like '" & Spec & "%' "
        End If

        If txtDate.Text <> "" Then
            whr = whr & Conn_SQL.Where("COPTG.TG042 < ", configDate.dateFormat2(txtDate.Text))

        End If

        Dim ordType As String = "",
            cnt As Integer = 0

        For Each boxItem As ListItem In cblSaleType.Items
            If boxItem.Selected = True Then
                ordType = ordType & "'" & CStr(boxItem.Value) & "',"
                cnt = cnt + 1
            End If
        Next
        If cnt > 0 Then
            ordType = ordType.Substring(0, ordType.Length - 1)
        End If

        paraName = "whr:" & whr
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=SaleDeliveryStatus.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & chrConn & "&encode=1');", True)

    End Sub
End Class