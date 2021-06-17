Public Class WipStatus
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim whList As String = "'3000','3100','3200','3300','3400','3600','3800'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MC001,MC001+' : '+MC002 as MC002 from CMSMC where MC001 in(" & whList & ") order by MC001 "
            ControlForm.showDDL(ddlWh, SQL, "MC002", "MC001", True, Conn_SQL.ERP_ConnectionString)

            SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            ControlForm.showDDL(ddlWorkType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("WIPStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Item")) Then
                    Dim link As String = ""
                    Dim jpPart As String = .DataItem("Item")
                    link = link & "&item= " & jpPart
                    link = link & "&tempTable= " & "wipStatus" & Session("UserName")
                    link = link & "&wh= " & ddlWh.Text.Trim
                    link = link & "&stockQty= " & .DataItem("Inv Qty")
                    link = link & "&moQty= " & .DataItem("MO Qty")
                    link = link & "&issueQty= " & .DataItem("Req MO Qty")
                    link = link & "&balQty= " & .DataItem("Bal Qty")
                    hplDetail.NavigateUrl = "WipStatusPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub
    Private Function substrSQL(fld As String, val As String) As String
        Return " and SUBSTRING(" & fld & ",3,1) ='" & val & "' "
    End Function
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "wipStatus" & Session("UserName")
        CreateTempTable.createTempWIPStatus(tempTable)

        Dim workType As String = ddlWorkType.Text,
            workNo As String = tbWorkNo.Text.Trim,
            Item As String = tbItem.Text.Trim,
            Spec As String = tbSpec.Text.Trim,
            wh As String = ddlWh.Text.Trim,
            SQL As String = "",
            Program As New DataTable,
            WHR As String = substrSQL("TA006", "3"),
            ASQL As String = "",
            code As String = "",
            qty As Decimal = 0,
            condition As String = ddlCondition.Text.Trim

        'MO  MOCTA
        If workType <> "ALL" Or workNo <> "" Then
            'Dim whr1 As String = substrSQL("MOCTA2.TA006", "3")
            Dim whr1 As String = ""
            If workType <> "ALL" Then
                whr1 = whr1 & " and MOCTA2.TA001='" & workType & "' "
            End If
            If workNo <> "" Then
                whr1 = whr1 & " and MOCTA2.TA002 like '" & workNo & "%' "
            End If
            WHR = WHR & " and MOCTA.TA006 in (select  MOCTA2.TA006 from JINPAO80.dbo.MOCTA MOCTA2 where MOCTA2.TA013='Y' and MOCTA2.TA011 not in ('y','Y')  " & whr1 & " ) "
        End If
        If Item <> "" Then
            WHR = WHR & " and MOCTA.TA006 like '%" & Item & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and MOCTA.TA035 like '%" & Spec & "%' "
        End If
        If wh <> "ALL" Then
            WHR = WHR & " and INVMB.MB017 ='" & wh & "' "
        Else
            WHR = WHR & " and INVMB.MB017 in (" & whList & ") "
        End If
        SQL = " select MOCTA.TA006,SUM(isnull(MOCTA.TA015,0)-isnull(MOCTA.TA017,0)-isnull(MOCTA.TA018,0)-isnull(MOCTA.TA060,0)) from JINPAO80.dbo.MOCTA " & _
              " left join JINPAO80.dbo.INVMB on INVMB.MB001=MOCTA.TA006 " & _
              " where MOCTA.TA013='Y' and MOCTA.TA011 not in ('y','Y') " & WHR & " group by MOCTA.TA006"
        ASQL = "insert into " & tempTable & "(item,moQty) " & SQL
        Conn_SQL.Exec_Sql(ASQL, Conn_SQL.MIS_ConnectionString)
        'Plan Issue Mat  MOCTA,MOCTB
        WHR = substrSQL("TB003", "3")
        If workType <> "ALL" Or workNo <> "" Then
            'Dim whr1 As String = substrSQL("TB003", "3")
            Dim whr1 As String = ""
            If workType <> "ALL" Then
                whr1 = whr1 & " and MOCTA2.TA001='" & workType & "' "
            End If
            If workNo <> "" Then
                whr1 = whr1 & " and MOCTA2.TA002 like '" & workNo & "%' "
            End If
            WHR = WHR & " and TB003 in (select  MOCTA2.TA006 from MOCTA MOCTA2 where MOCTA2.TA013='Y' and MOCTA2.TA011 not in ('y','Y')  " & whr1 & " ) "
        End If
        If Item <> "" Then
            WHR = WHR & " and TB003 like '%" & Item & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and TB013 like '%" & Spec & "%' "
        End If
        If wh <> "ALL" Then
            WHR = WHR & " and MB017 ='" & wh & "' "
        Else
            WHR = WHR & " and MB017 in (" & whList & ") "
        End If
        SQL = " select TB003 as item,SUM(TB004-TB005) as issue from  MOCTB " & _
              " left join MOCTA on TA001=TB001 and TA002=TB002 " & _
              " left join INVMB on MB001=TB003 " & _
              " where TB004-TB005>0 and  TA011 not in('y','Y') and TA013='Y' " & WHR & " group by TB003 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            code = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("issue")
            ASQL = " if exists(select * from " & tempTable & " where item='" & code & "' ) " & _
                   " update " & tempTable & " set issueQty='" & qty & "' where item='" & code & "' else " & _
                   " insert into " & tempTable & "(item,issueQty)values ('" & code & "','" & qty & "')"
            Conn_SQL.Exec_Sql(ASQL, Conn_SQL.MIS_ConnectionString)
        Next
        ''Inventory INVMC,INVMB
        'WHR = substrSQL("MB001", "3")
        'If workType <> "ALL" Or workNo <> "" Then
        '    Dim whr1 As String = substrSQL("MOCTA2.TA006", "3")
        '    If workType <> "ALL" Then
        '        whr1 = whr1 & " and MOCTA2.TA001='" & workType & "' "
        '    End If
        '    If workNo <> "" Then
        '        whr1 = whr1 & " and MOCTA2.TA002 like '" & workNo & "%' "
        '    End If
        '    WHR = WHR & " and MB001 in (select  MOCTA2.TA006 from MOCTA MOCTA2 where MOCTA2.TA013='Y' and MOCTA2.TA011 not in ('y','Y')  " & whr1 & " group by MOCTA2.TA006 )  "
        'End If
        'If Item <> "" Then
        '    WHR = WHR & " and MB001 like '%" & Item & "%' "
        'End If
        'If Spec <> "" Then
        '    WHR = WHR & " and MB003 like '%" & Spec & "%' "
        'End If
        'If wh <> "ALL" Then
        '    WHR = WHR & " and MC002 = '" & wh & "' "
        'Else
        '    WHR = WHR & " and MC002 in (" & whList & ") "
        'End If

        'SQL = " select MB001 as item,sum(MC007) as Qty from INVMC left join INVMB on MB001=MC001 " & _
        '      " where MC007>0 " & WHR & " group by MB001 order by MB001 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'For i As Integer = 0 To Program.Rows.Count - 1
        '    code = Program.Rows(i).Item("item")
        '    qty = Program.Rows(i).Item("Qty")
        '    ASQL = " if exists(select * from " & tempTable & " where item='" & code & "' ) " & _
        '           " update " & tempTable & " set stockQty='" & qty & "' where item='" & code & "' else " & _
        '           " insert into " & tempTable & "(item,stockQty)values ('" & code & "','" & qty & "')"
        '    Conn_SQL.Exec_Sql(ASQL, Conn_SQL.MIS_ConnectionString)
        'Next

        'Inventory INVMC,INVMB,INVML
        'WHR = substrSQL("ML001", "3")
        WHR = ""
        If workType <> "ALL" Or workNo <> "" Then
            'Dim whr1 As String = substrSQL("MOCTA2.TA006", "3")
            Dim whr1 As String = ""
            If workType <> "ALL" Then
                whr1 = whr1 & " and MOCTA2.TA001='" & workType & "' "
            End If
            If workNo <> "" Then
                whr1 = whr1 & " and MOCTA2.TA002 like '" & workNo & "%' "
            End If
            WHR = WHR & " and MB001 in (select  MOCTA2.TA006 from MOCTA MOCTA2 where MOCTA2.TA013='Y' and MOCTA2.TA011 not in ('y','Y')  " & whr1 & " group by MOCTA2.TA006 )  "
        End If
        If Item <> "" Then
            WHR = WHR & " and MB001 like '%" & Item & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and MB003 like '%" & Spec & "%' "
        End If
        If wh <> "ALL" Then
            WHR = WHR & " and ML002 = '" & wh & "' "
        Else
            WHR = WHR & " and ML002 in (" & whList & ") "
        End If

        SQL = " select ML001 as item,sum(ML005) as Qty," & _
              " sum(case when ML003=MC015 then ML005 else 0 end) as MainBin," & _
              " sum(case when ML003<>MC015 then ML005 else 0 end) as NoMainBin" & _
              " from INVML left join INVMB on MB001=ML001 " & _
              " left join INVMC on MC001=ML001 and MC002= ML002 " & _
              " where ML005<>0 " & WHR & " group by ML001 order by ML001 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            code = Program.Rows(i).Item("item")
            qty = Program.Rows(i).Item("Qty")
            Dim MainBin As Decimal = Program.Rows(i).Item("MainBin")
            Dim NoMainBin As Decimal = Program.Rows(i).Item("NoMainBin")
            ASQL = " if exists(select * from " & tempTable & " where item='" & code & "' ) " & _
                   " update " & tempTable & " set stockQty='" & qty & "',MainBinQty='" & MainBin & "',NoBinQty='" & NoMainBin & "' where item='" & code & "' else " & _
                   " insert into " & tempTable & "(item,stockQty,MainBinQty,NoBinQty)values ('" & code & "','" & qty & "','" & MainBin & "','" & NoMainBin & "')"
            Conn_SQL.Exec_Sql(ASQL, Conn_SQL.MIS_ConnectionString)
        Next

        WHR = ""
        If condition <> "0" Then
            Dim symbol As String = ">"
            If condition = "1" Then
                symbol = "<"
            ElseIf condition = "3" Then
                symbol = "="
            End If
            WHR = " where T.stockQty+T.moQty " & symbol & " T.issueQty "
        End If
        SQL = " select INVMB.MB017 as 'Main WH',INVMC.MC015 as 'Main Bin',T.item as 'Item',INVMB.MB003 as 'Spec', " & _
              " cast(T.stockQty as decimal(16,2))  as 'Inv Qty',cast(T.moQty as decimal(16,2)) as 'MO Qty'," & _
              " cast(T.issueQty as decimal(16,2)) as 'Req MO Qty', " & _
              " cast(T.stockQty+T.moQty-T.issueQty as decimal(16,2)) as 'Bal Qty','' as 'Actual Count'," & _
              "cast(T.MainBinQty as decimal(16,2)) as 'Main Bin Qty'," & _
              "cast(T.NoBinQty as decimal(16,2)) as 'No Main Bin Qty' " & _
              " from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.item " & WHR & _
              " left join JINPAO80.dbo.INVMC INVMC on INVMC.MC001=T.item and INVMC.MC002=INVMB.MB017 " & WHR & _
              " order by T.item "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
End Class