Imports System.Globalization
Imports MIS_HTI.DataControl
Public Class inventoryMovement
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    'Dim listWh As String = "2201,2202,2301,2302,2203,2204,2205,2206,2207,2900"
    Dim listWhNot As String = "2305,2500,2501,2502,2600,2601,2700,2800"
    Dim dbConn As New DataConnectControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = String.Empty Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            Dim SQL As String = "select MC001,MC001+' : '+MC002 as MC002 from CMSMC where MC001 not in ('" & listWhNot.Replace(",", "','") & "')  order by MC001 "
            ControlForm.showCheckboxList(cblWH, SQL, "MC002", "MC001", 4, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempInventoryMove" & Session("UserName"),
            tempTable2 As String = "tempBOMMAT" & Session("UserName")

        CreateTempTable.createTempMaterialsMovement(tempTable)
        CreateTempTable.createTempBomMat(tempTable2)
        
        Dim SQL As String = "",
            WHR As String = "",
            endDate As String = configDate.dateFormat2(tbDate.Text.Trim),
            USQL As String = ""
        If String.IsNullOrEmpty(endDate) Then
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        Dim sqlStock As String
        WHR = Conn_SQL.Where("MC002", cblWH, False, , True)

        ' Dim whr1 As String = " where 1=1 "

        sqlStock = "(Select MC001 ,MC012,MC013,MC007,trn_date,date_diff,
                        case when date_diff <31 then  MC007 else 0 end stockA,
                        case when date_diff >=31 and date_diff<=90 then  MC007 else 0 end stockA1,
                        case when date_diff >=91 and date_diff<=180  then  MC007 else 0 end stockB,
                        case when date_diff >=181 and date_diff<=270  then  MC007 else 0 end stockC,
                        case when date_diff >=271 and date_diff<=360  then  MC007 else 0 end stockD,
                        case when date_diff >=361 and date_diff<=720  then  MC007 else 0 end stockE,
                        case when date_diff >=721 and date_diff<=999  then  MC007 else 0 end stockF,
                        case when date_diff >999   then  MC007 else 0 end stockG
                    from (select *,datediff(day,trn_date,GETDATE())  date_diff from (
                            select MC001,max(MC012) MC012,MAX(MC013) MC013,sum(MC007) MC007,
                                   case when isnull(MAX(MC012),0)=0 and isnull(MAX(MC013),0)=0 then GETDATE()-999  else case when  isnull(MAX(MC012),0)=0 or isnull(MAX(MC013),0)=0 then case when isnull(MAX(MC012),0)=0 then MAX(MC013) else MAX(MC012) end else case when cast(MAX(MC012) as date) > cast(MAX(MC013) as date) then cast(MAX(MC012) as date) else cast(MAX(MC013) as date) end end end trn_date
                            from INVMC where MC007>0 " & WHR & " group by MC001
                            ) AA where 1=1 " & dbConn.WHERE_EQUAL("convert(varchar, trn_date, 112)", endDate, "<=") & ") BB ) INVMC "

        Dim sqlSo As String
        sqlSo = "(select TD004,sum(TD008+TD024) TD008 from COPTD left join COPTC on TC001=TD001 and TC002=TD002 where TD008+TD024>0 and TD016 = 'N' and TC027 = 'Y' group by TD004) COPTD "

        Dim sqlMO As String
        sqlMO = "(select TA006,SUM(TA015-TA017) TA015 from  MOCTA  where TA015-TA017>0 and TA011 not in('y','Y') and TA013='Y'  group by TA006) MOCTA"


        Dim sqlIssue As String
        sqlIssue = "(select TB003,SUM(TB004-TB005) TB004 from MOCTB   left join MOCTA on TA001=TB001 and TA002=TB002  where TB004-TB005>0 and TA011 not in('y','Y') and TA013='Y'  group by TB003) MOCTB "

        Dim sqlBOMHave As String
        sqlBOMHave = " (select MD001,count(*) COUNT_HAVE from BOMMD left join BOMMC on MC001=MD001 where MC019='Y' group by MD001) BOM_HAVE "

        Dim sqlBOMUse As String
        sqlBOMUse = " (select MD003,count(*) COUNT_USE from BOMMD left join BOMMC on MC001=MD001 where MC019='Y' group by MD003) BOM_USE "

        Dim fldName1 As New ArrayList From {
            "MB001 F01",
            "MB002 F02",
            "MB003 F03",
            "INVMC.MC007 F04",
            "MB004 F05",
            "CMSMC.MC002 F06",
            "case when isnull((select INVMC.MC004  from INVMC where MC001 = MB001 And INVMC.MC004 > 0),0) <> '0' then cast((select INVMC.MC004  from INVMC where INVMC.MC001 = MB001 and INVMC.MC004 > 0) as decimal(16,2)) else '0' end F66",
            "isnull(TB004,0) F07",
            "isnull(TA015,0) F08",
            "isnull(stockA1,0) F09",
            "isnull(stockA,0) F90",
            "isnull(stockB,0) F10",
            "isnull(stockC,0) F11",
            "isnull(stockD,0) F12",
            "isnull(stockE,0) F13",
            "isnull(stockF,0) F22",
            "isnull(stockG,0) F23",
            "isnull(INVMC.MC013,'') F14",
            "isnull(INVMC.MC012,'') F15",
            "isnull(INVMC.trn_date,'') F151",
            "(select top 1 INVLB.LB010 from INVLB where INVLB.LB001 = MB001 order by  INVLB.LB002 DESC ) F16",
            "(select top 1 TD010  from PURTD left join PURTC on TC001=TD001 and TC002=TD002 where TCD01 = 'Y' and TD004=MB001 order by TC024 desc ) F18",
            "MB065 F17",
            "MB067 F19",
            "(select top 1 Rtrim(PURTG.TG005)+'-'+PURMA.MA002 from PURTG  left join PURTH on PURTH.TH001 = PURTG.TG001 and PURTH.TH002 = PURTG.TG002 left join PURMA on PURMA.MA001 = PURTG.TG005  where PURTH.TH004 = MB001 order by PURTG.TG002 DESC) F20",
            "case isnull(COUNT_HAVE,0) when 0 then 'No BOM' else '' end F21",
            "case isnull(COUNT_USE,0) when 0 then 'No Use' else '' end F211",
            " isnull(TD008,0) SO",
            "(select top 1 TD026 +'-'+ TD027  from PURTD left join PURTC on TC001=TD001 and TC002=TD002 where TC014 = 'Y' and TD004=MB001 order by TC024 desc ) F91",
            "(select top 1 TD001 +'-'+ TD002  from PURTD left join PURTC on TC001=TD001 and TC002=TD002 where TC014 = 'Y' and TD004=MB001 order by TC024 desc ) F92",
            "date_diff"
        }


        '//Update by wit // ข้อมูลเดิม PR "(select top 1  TB001 +'-'+ TB002 from PURTB left join PURTA on TA001=TB001 and TA002=TB002 where TA007 = 'Y' AND TB004=MB001 order by TA013 desc ) F91",

        Dim strSQL As New SQLString("INVMB", fldName1)
        strSQL.setLeftjoin(" left join " & sqlStock & " on INVMC.MC001 = MB001 ")
        strSQL.setLeftjoin(" left join " & sqlSo & " on TD004 = MB001 ")
        strSQL.setLeftjoin(" left join " & sqlMO & " on TA006 = MB001 ")
        strSQL.setLeftjoin(" left join " & sqlIssue & " on TB003 = MB001 ")
        strSQL.setLeftjoin(" left join " & sqlBOMHave & " on MD001 = MB001 ")
        strSQL.setLeftjoin(" left join " & sqlBOMUse & " on MD003 = MB001 ")
        strSQL.setLeftjoin(" left join CMSMC on CMSMC.MC001=MB017 ")
        WHR = Conn_SQL.Where("MB001", tbItem)
        WHR &= Conn_SQL.Where("MB002", tbDesc)
        WHR &= Conn_SQL.Where("MB003", tbSpec)
        'WHR &= Conn_SQL.Where("substring(MB001,3,1)", ddlTypeCode)
        WHR &= Conn_SQL.Where("substring(MB005,1,2)", ddlTypeCode)
        WHR &= dbConn.WHERE_EQUAL("INVMC.MC007", "0", ">", False)
        If CbObver30Days.Checked Then
            WHR &= dbConn.WHERE_EQUAL("date_diff", "30", ">", False)
        End If
        strSQL.SetWhere(WHR, True)
        strSQL.SetOrderBy("MB001")
        SQL = strSQL.GetSQLString

        'SQL = " select distinct T.item F01,MB002 F02,MB003 F03,stock F04,MB004 F05,MC002 F06," &
        '      " case when isnull((select INVMC.MC004  from INVMC INVMC where INVMC.MC001 = T.item And INVMC.MC004 > 0),0) <> '0' then cast((select INVMC.MC004  from INVMC INVMC where INVMC.MC001 = T.item and INVMC.MC004 > 0) as decimal(16,2)) else '0' end F66," &
        '      " T.issueQty F07,T.planQty F08,stockA F09,stockA1 F90, " &
        '      " stockB F10,stockC F11,stockD F12,stockE F13,stockF F22,stockG F23,lastDisDate F14,lastRcvDate F15 " &
        '      " ,(select top 1 INVLB.LB010 from INVLB where INVLB.LB001 = T.item order by  INVLB.LB002 DESC ) F16 " &
        '      " ,MB065 F17 ,T.MOSpec F18, MB067 F19 " &
        '      " ,(select top 1 Rtrim(PURTG.TG005)+'-'+PURMA.MA002 from PURTG  left join PURTH on PURTH.TH001 = PURTG.TG001 and PURTH.TH002 = PURTG.TG002  " &
        '      " left join PURMA on PURMA.MA001 = PURTG.TG005  where PURTH.TH004 = T.item order by PURTG.TG002 DESC) F20, " &
        '      " case when BOMCA.CA003 <>'' then '' else 'No BOM' end F21,T.soQty SO,T.SoSpec SoSpec,T.ForSpec Forc ," &
        '      " (select top 1  TB001 +'-'+ TB002 from PURTB left join PURTA on TA001=TB001 and TA002=TB002 where TA007 = 'Y' AND TB004=T.item order by TA013 desc ) F91," &
        '      " (select top 1 TD001 +'-'+ TD002  from PURTD left join PURTC on TC001=TD001 and TC002=TD002 where TC014 = 'Y' and TD004=T.item order by TC024 desc ) F92 " &
        '      " from INVMB on " &
        '      " from " & Conn_SQL.DBReport & ".." & tempTable & " T  on  MC001=MB001" &
        '      " left join INVMB on MB001 = T.item" &
        '      " left join " & Conn_SQL.DBReport & ".." & tempTable2 & " B on B.itemMAT = T.item " &
        '      " left join CMSMC on MC001=MB017 " &
        '      " left join BOMCB on BOMCB.CB005 = T.item " &
        '      " left join BOMCA on BOMCB.CB001 = BOMCA.CA003 " &
        '      " order by T.item  "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub

    Protected Sub CodeBOM(tempTable As String, tempTable2 As String, code As String, Optional codeParent As String = "")
        If codeParent = "" Then
            codeParent = code
        End If
        Dim SQL As String = "",
            WHR As String = ""
        WHR &= Conn_SQL.Where("MB001", tbItem)
        WHR &= Conn_SQL.Where("MB002", tbDesc)
        WHR &= Conn_SQL.Where("MB003", tbSpec)

        SQL = " select MD003,MD006,MB025,MD008 from BOMMD " &
              " left join INVMB on MB001=MD003 " &
              " where MB109='Y' and MD001='" & code & "'  " & WHR &
              " order by MD003 "
        Dim Program As New DataTable
        Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim item As String = .Item("MD003"),
                    qty As Decimal = .Item("MD006"),
                    ScrapRatio As Decimal = .Item("MD008")
                If .Item("MB025") = "M" Then
                    CodeBOM(tempTable, tempTable2, item, codeParent)
                ElseIf chkCode(item) Then
                    Dim fldInsHash As Hashtable = New Hashtable,
                        whrHash As Hashtable = New Hashtable,
                        fldUpdHash As Hashtable = New Hashtable
                    'whr of condition
                    Dim matCode As String = .Item("MD003").ToString.Trim
                    whrHash.Add("item", matCode)
                    fldInsHash.Add("issueQty", "0") ' fg item
                    fldUpdHash.Add("issueQty", "'0'") ' fg item
                    dbConn.TransactionSQL(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
                    Dim USQL As String = ""
                    'BOMMAT
                    USQL = " if not exists(select * from " & tempTable2 & " where itemParent='" & code & "' and itemMAT='" & matCode & "' ) " &
                           " insert into " & tempTable2 & "(itemParent,itemMAT,qty,scrapRatio)values ('" & code & "','" & matCode & "','" & qty & "','" & ScrapRatio & "')"
                    dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)

                End If
            End With
        Next

    End Sub

    Function chkCode(code As String) As Boolean
        Dim c1 As String = code.Substring(2, 1)
        Dim res As Boolean = False
        If ddlTypeCode.Text = "0" Then
            res = True
        Else
            Dim tm As String = ddlTypeCode.Text.Trim
            If c1 = tm Then
                res = True
            End If
        End If
        Return res
    End Function

    Sub ExeSQL(SQL As String, Code As String)
        Dim USQL As String = SQL.Substring(0, SQL.Length - 1) & " where item='" & Code & "' "
        dbConn.TransactionSQL(USQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Function TextSQL(fldName As String, Val As Decimal) As String
        Return fldName & "='" & Val & "',"
    End Function

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("InventoryMovement" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)

                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("F01")) Then
                    Dim link As String = ""
                    Dim MatPart As String = .DataItem("F01")

                    link &= "&MatPart=" & MatPart
                    link &= "&MatDesc=" & .DataItem("F02")
                    link &= "&MatSpec=" & .DataItem("F03")
                    hplDetail.NavigateUrl = "inventoryMovementPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", MatPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

End Class