Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class Scrap
    Inherits System.Web.UI.Page
    Dim dbConn As New DataConnectControl
    Dim ddlCont As New DropDownListControl
    Dim CreateTempTable As New CreateTempTable
    Dim dtCont As New DataTableControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session(VarIni.UserName) <> "" Then
                Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
                ddlCont.showDDL(ddlMOType, SQL, VarIni.ERP, "MQ002", "MQ001", True)

                SQL = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
                ddlCont.showDDL(ddlWC, SQL, VarIni.ERP, "MD002", "MD001", True)

                CreateTempTable.createScrap("TempScrap" & Session(VarIni.UserName))
                GridView1.Visible = False
                BuExcel.Visible = False
                btPrint.Visible = False
            End If
        End If
    End Sub

    Private Sub Busearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Busearch.Click

        Dim SQL As String = "",
            WHR As String = ""
        WHR &= dbConn.WHERE_EQUAL("TA001", ddlMOType)
        WHR &= dbConn.WHERE_LIKE("TA002", wno)
        WHR &= dbConn.WHERE_EQUAL("TL003", ucDateSaleOrder.text)
        WHR &= dbConn.Where("TA006", item)
        WHR &= dbConn.Where("TA035", spec)
        WHR &= dbConn.Where("TA027", SoNo)
        WHR &= dbConn.WHERE_BETWEEN("TB003", ucDateFrom.text, ucDateTo.text)
        WHR &= dbConn.WHERE_EQUAL("TB005", ddlWC)
        WHR &= dbConn.WHERE_IN("TL014", cblApp)

        If WHR = "" Then
            GridView1.Visible = False
        Else
            SQL = " select TA001,TA002,TL003,TL014,TA006,TA024,TA025,TA026,TA027,TA035,TA015,TL006 as Dept,TM007 AS SQTY,TL001,TL002,TM003,TB005,TB006,TB014  from INVTM " &
                  " left join INVTL on TL001 = TM001 and TL002 = TM002 " &
                  " left join SFCTC on TC001 = TM009 AND TC002 = TM010 and TC003 = TM011 " &
                  " left JOIN SFCTB on TB001 = TC001 and TB002 = TC002 " &
                  " left JOIN MOCTA on TA001 = TC004 AND TA002 = TC005 " &
                  " where 1=1 " & WHR &
                  " order by TM001,TM002,TM003 "
            GridView1.Visible = True
            SqlDataSource1.SelectCommand = SQL
            GridView1.DataBind()
            BuExcel.Visible = True
            btPrint.Visible = True
        End If

    End Sub

    Protected Sub BuExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuExcel.Click

        'ControlForm.ExportGridViewToExcel("Scrap" & Session("UserName"), GridView1)
        GridView1.Visible = True
        PrintAllSearch()

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub PrintAllSearch()
        If GridView1.Rows.Count < 1 Then
            show_message.ShowMessage(Page, "No Data", UpdatePanel1)
            Exit Sub
        End If
        Dim DelLabeleBM As String = "delete from ScrapAll"
        dbConn.TransactionSQL(DelLabeleBM, VarIni.DBMIS, dbConn.WhoCalledMe)
        'Conn_sql.Exec_Sql(DelLabeleBM, Conn_sql.MIS_ConnectionString)

        If GridView1.Rows.Count > 0 Then
            For i As Integer = 0 To GridView1.Rows.Count - 1
                With GridView1.Rows(i)
                    Dim WType As String = .Cells(1).Text
                    Dim WNo As String = .Cells(2).Text
                    Dim ScrapDate As String = .Cells(3).Text
                    Dim Item As String = .Cells(4).Text
                    Dim SoType As String = .Cells(5).Text
                    Dim SoNo As String = .Cells(6).Text
                    Dim Spec As String = .Cells(7).Text
                    Dim SoQty As String = .Cells(8).Text
                    Dim Dept As String = .Cells(9).Text.Replace("&nbsp;", "").Replace(" ", "")
                    Dim SQty As String = .Cells(10).Text
                    Dim SType As String = .Cells(11).Text
                    Dim SNo As String = .Cells(12).Text
                    Dim TFrom As String = .Cells(13).Text.Replace("&nbsp;", "")
                    Dim Tname As String = .Cells(14).Text.Replace("&nbsp;", "")
                    Dim Remark As String = .Cells(15).Text.Replace("&nbsp;", "").Replace(" ", "")

                    Dim InSQL As String = "Insert into ScrapAll(WType,WNo,ScrapDate,Item,SoType,SoNo,Spec,SoQty,Dept,SQty,SType,SNo,TFrom,Tname,Remark)"
                    InSQL &= " Values('" & WType & "','" & WNo & "','" & ScrapDate & "','" & Item & "',"
                    InSQL &= "'" & SoType & "','" & SoNo & "','" & Spec & "','" & SoQty & "',"
                    InSQL &= "'" & Dept & "','" & SQty & "','" & SType & "','" & SNo & "',"
                    InSQL &= "'" & TFrom & "','" & Tname & "','" & Remark & "')"
                    dbConn.TransactionSQL(InSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                End With
            Next

        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ScrapAll.aspx?WNo=');", True) '"&Year=" + DropDownList3.SelectedValue +
    End Sub

    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrint.Click

        Dim tableName As String = "TempReworkOrScrap" & Session("UserName")
        CreateTempTable.createTempReworkOrScrap(tableName)
        Dim SQL As String = ""
        Dim Program As New DataTable
        Dim type As String = ""

        For i As Integer = 0 To GridView1.Rows.Count - 1
            With GridView1.Rows(i)
                Dim chkSelect As CheckBox = .FindControl("cbSelect")
                If chkSelect.Checked Then
                    Dim WHR As String = ""
                    WHR &= dbConn.WHERE_EQUAL("TL001", .Cells(2).Text.ToString)
                    WHR &= dbConn.WHERE_EQUAL("TL002", .Cells(3).Text.ToString)
                    WHR &= dbConn.WHERE_EQUAL("TM003", .Cells(4).Text.ToString)
                    SQL = "select Rtrim(TB005) as 'WC',Rtrim(TB006) as 'WCName',Rtrim(TL001)+'-' as 'Doc',Rtrim(TL002) as 'DocNo',Rtrim(TM003) as 'Docseq', " &
                        " (SUBSTRING (TL003,7,2)+'-'+SUBSTRING (TL003,5,2)+'-'+SUBSTRING (TL003,1,4)) AS 'DocDate', " &
                        " Rtrim(TA001) +'-' as 'MO',TA002 as 'MONo', " &
                        " TA006 as 'item',TA035 as 'spec',convert(int,TA015) as moQty,convert(int,TM007) AS scrapQty,TB014 as remark, " &
                        " MA001 as custId,MA002 as custName, TL006 as Dept, " &
                        " ME002 as deptName ,TA026 as saleType,TA027 as saleNo " &
                        " from INVTM " &
                        " left join INVTL on TL001 = TM001 and TL002 = TM002 " &
                        " left join SFCTC on TC001 = TM009 AND TC002 = TM010 and TC003 = TM011 " &
                        " left JOIN SFCTB on TB001 = TC001 and TB002 = TC002 " &
                        " left JOIN MOCTA on TA001 = TC004 AND TA002 = TC005 " &
                        " left join COPTC COP on COP.TC001=TA026 and COP.TC002 = TA027 " &
                        " left join COPMA on MA001 = COP.TC004 " &
                        " left join CMSME on ME001 = TL006 " &
                        " where 1=1 " &
                        " order by TB005,TL001,TL002,TL003 "
                    Program = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)

                    For Each dr As DataRow In Program.Rows
                        With dtCont
                            Dim ISQL = " insert into " & tableName & "(wc,wcname,doc,docno,docseq,docdate,mo,mono,item,spec,moQty,scrapQty) values " &
                                       " ('" & .IsDBNullDataRow(dr, "WC") & "','" & .IsDBNullDataRow(dr, "WCName").Replace("'", "") & "','" & .IsDBNullDataRow(dr, "Doc") & "'," &
                                       "'" & .IsDBNullDataRow(dr, "DocNo") & "','" & .IsDBNullDataRow(dr, "Docseq") & "','" & .IsDBNullDataRow(dr, "DocDate") & "','" & .IsDBNullDataRow(dr, "MO") & "'," &
                                       "'" & .IsDBNullDataRow(dr, "MONo") & "','" & .IsDBNullDataRow(dr, "item") & "',N'" & .IsDBNullDataRow(dr, "spec") & "'," &
                                       "'" & .IsDBNullDataRowDecimal(dr, "moQty") & "','" & .IsDBNullDataRowDecimal(dr, "scrapQty") & " ')"
                            dbConn.TransactionSQL(ISQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                        End With

                    Next
                End If
            End With
        Next

        'Dim paraName As String = ""
        'paraName = "table:" & tableName
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=ReworkOrScrap.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)

        'BuExcel.Visible = True
        Dim paraName As String = ""
        'Dim tableName As String = "TempReworkOrScrap" & Session("UserName")
        paraName = "table:" & tableName
        Dim rnd As New Random
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=ReworkOrScrap.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1&RID=" & rnd.Next(1, 100) & "');", True)
        BuExcel.Visible = If(GridView1.Rows.Count > 0, True, False)
    End Sub
End Class