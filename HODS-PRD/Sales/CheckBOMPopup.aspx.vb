Public Class CheckBOMPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then

            Dim tempBOM As String = "tempBOM" & Session("UserName"),
                tempBOMList As String = "tempBOMList" & Session("UserName")
            CreateTempTable.createTempBom(tempBOM)
            CreateTempTable.createTempBOMList(tempBOMList)

            Dim item As String = Request.QueryString("item").ToString.Trim
            lbItem.Text = item
            lbSpec.Text = Request.QueryString("spec").ToString.Trim

            CodeBOM(tempBOM, tempBOMList, item)
            Dim SQL As String,
                USQL As String
            Dim dt As New DataTable

            'Stock
            SQL = " select T.item,SUM(isnull(INVMC.MC007,0)) as stock from DBMIS.dbo." & tempBOMList & " T " & _
                  " left join INVMC on INVMC.MC001=T.item " & _
                  " where INVMC.MC007 >0 and ((substring(INVMC.MC001,3,1)='1' and INVMC.MC002='2204') or " & _
                  " (substring(INVMC.MC001,3,1)='4' and INVMC.MC002='2206')) group by T.item "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    USQL = " update " & tempBOMList & " set stockQty='" & .Item("stock") & "' where item='" & .Item("item") & "'  "
                    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                End With
            Next

            'PR
            SQL = " select T.item,sum(TR006) as pr from DBMIS.dbo." & tempBOMList & " T " & _
                  " left join PURTB on TB004=T.item " & _
                  " left join PURTR on TR001=TB001 and TR002=TB002 and TR003=TB003 " & _
                  " left join PURTA on TA001=TB001 and TA002=TB002 " & _
                  " where TB039='N' and TA007 = 'Y' and TR019='' " & _
                  " group by T.item order by T.item "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    USQL = " update " & tempBOMList & " set prQty='" & .Item("pr") & "' where item='" & .Item("item") & "'  "
                    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                End With
            Next

            'PO
            SQL = " select T.item,SUM(isnull(TD008,0)-isnull(TD015,0)) as po from DBMIS.dbo." & tempBOMList & " T " & _
                  " left join PURTD on TD004=T.item " & _
                  " left join PURTC on TC001=TD001 and TC002=TD002 " & _
                  " where TD016='N' " & _
                  " group by T.item  order by T.item "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    USQL = " update " & tempBOMList & " set poQty='" & .Item("po") & "' where item='" & .Item("item") & "'  "
                    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                End With
            Next

            'Plan Issue
            SQL = " select T.item,SUM(TB004-TB005) as issue from DBMIS.dbo." & tempBOMList & " T " & _
                  " left join MOCTB on TB003=T.item " & _
                  " left join MOCTA on TA001=TB001 and TA002=TB002 " & _
                  " where TB004-TB005>0 and TA011 not in('y','Y') and TA013='Y' " & _
                  " group by T.item "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    ExeSQL(" update " & tempBOMList & " set issueQty='" & .Item("issue") & "' ", .Item("item"))
                End With
            Next

            'MO
            SQL = " select TA006 as item, SUM(isnull(TA015,0)-isnull(TA017,0)-isnull(TA018,0)-isnull(TA060,0)) as mo from DBMIS.dbo." & tempBOMList & " T " & _
                  " left join MOCTA on TA006=T.item where TA011 not in('y','Y') and TA013 ='Y' group by TA006  order by TA006"
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    ExeSQL(" update " & tempBOMList & " set moQty='" & .Item("mo") & "' ", .Item("item"))
                End With
            Next

            'SO
            SQL = " select TD004 as item,SUM(TD008-TD009) as so from DBMIS.dbo." & tempBOMList & " T  " & _
                  " left join COPTD on TD004=T.item " & _
                  " left join COPTC on TC001=TD001 and TC002=TD002 " & _
                  " where TD016='N' and TC027='Y' group by TD004  order by TD004 "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    ExeSQL(" update " & tempBOMList & " set soQty='" & .Item("so") & "' ", .Item("item"))
                End With
            Next

            'Dim SQL As String = "select * from " & tempBOM
            'Dim aa As String = ""
            'Dim whr As String = ""
            ''Sale order Detail
            'Dim SQL As String = " select COPTC.TC004 as CUST,COPTD.TD001+'-'+COPTD.TD002+'-'+COPTD.TD003 as OrdDetail, " & _
            '                    " (SUBSTRING(COPTC.TC003,7,2)+'-'+SUBSTRING(COPTC.TC003,5,2)+'-'+SUBSTRING(COPTC.TC003,1,4)) as DocDate," & _
            '                    " (SUBSTRING(COPTD.TD013,7,2)+'-'+SUBSTRING(COPTD.TD013,5,2)+'-'+SUBSTRING(COPTD.TD013,1,4)) as PlanDelDate, " & _
            '                    " cast(COPTD.TD008 as decimal(8,0)) as OrderQty,cast(COPTD.TD009 as decimal(8,0)) as DelQty," & _
            '                    " cast(COPTD.TD008-COPTD.TD009 as decimal(8,0)) as BAL,COPTD.TD010 as UNIT from COPTD  " & _
            '                    " left join COPTC  on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 " & _
            '                    "  where COPTD.TD016='N' and COPTC.TC027='Y' and COPTD.TD004 = '" & jpPart & "' " & _
            '                    " order by COPTD.TD013"
            SQL = " select len(item)-len(replace(item,'" & Chr(8) & "','')) as 'Level',isnull(MD002,'') as 'Seq', " & _
                  " case len(rtrim(T.itemChild)) when 16 then (SUBSTRING(T.itemChild,1,14)+'-'+SUBSTRING(T.itemChild,15,2)) else T.itemChild end as 'Item' ," & _
                  " MB002 as 'Desc',MB003 as 'Spec',MD006 as 'Usage',MB004 as 'Unit'," & _
                  " case MB025 when 'M' then 'Manufacture' when 'P' then 'Purchase' else 'Other' end  as 'Property' " & _
                  " from DBMIS.dbo." & tempBOM & " T left join INVMB on MB001=T.itemChild " & _
                  " left join BOMMD on MD001=T.itemParent and MD003=T.itemChild " & _
                  " order by item "
            ControlForm.ShowGridView(gvBOMDetail, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountSO.Text = gvBOMDetail.Rows.Count
            ''MO Detail

            SQL = " select case len(rtrim(T.item)) when 16 then (SUBSTRING(T.item,1,14)+'-'+SUBSTRING(T.item,15,2)) else T.item end as 'Item' ," & _
                  " MB002 as 'Desc',MB003 as 'Spec',stockQty as 'Stock',issueQty as 'Issue', " & _
                  " moQty as 'MO',poQty as 'PO',prQty as 'PR',soQty as 'SO',MB036 as 'Fix Lead Time' from DBMIS.dbo." & tempBOMList & " T " & _
                  " left join INVMB on MB001 = T.item " & _
                  " order by T.item "

            ControlForm.ShowGridView(gvBOMList, SQL, Conn_SQL.ERP_ConnectionString)
            lbCountMO.Text = gvBOMList.Rows.Count
            
        End If
    End Sub

    Protected Sub CodeBOM(tempBOM As String, tempBOMList As String, code As String, Optional codeParent As String = "")

        Dim SQL As String = "",
            SSQL As String = ""

        If codeParent = "" Then
            SQL = " if not exists(select * from " & tempBOM & " where item='" & code & "' ) " & _
                                 " insert into " & tempBOM & "(item,itemParent,itemChild)values ('" & code & "','" & code & "','" & code & "')"
            Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
            SQL = " if not exists(select * from " & tempBOMList & " where item='" & code & "' ) " & _
                                 " insert into " & tempBOMList & "(item)values ('" & code & "')"
            Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
            codeParent = code
        End If
        'Dim SQL As String = "" MD006,,MD008
        SQL = " select MD003,MB025 from BOMMD " & _
              " left join INVMB on MB001=MD003 " & _
              " where MB109='Y' and MD001='" & code & "'  " & _
              " order by MD003 "

        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim item As String = .Item("MD003").ToString.Trim
                'qty As Decimal = .Item("MD006"),
                'ScrapRatio As Decimal = .Item("MD008")
                Dim tempCode As String = codeParent & Chr(8) & item ',qty,scrapRatio ,'" & qty & "','" & ScrapRatio & "'
                SQL = " if not exists(select * from " & tempBOM & " where item='" & tempCode & "' ) " & _
                      " insert into " & tempBOM & "(item,itemParent,itemChild)values ('" & tempCode & "','" & code & "','" & item & "')"
                Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)

                SQL = " if not exists(select * from " & tempBOMList & " where item='" & item & "' )  insert into " & tempBOMList & "(item) values ('" & item & "')"
                Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)

                If .Item("MB025") = "M" Then
                    CodeBOM(tempBOM, tempBOMList, item, tempCode)
                End If

                'BOMShow
                'USQL = " if not exists(select * from " & tempTable3 & " where itemFG='" & codeParent & "' and itemParent='" & code & "' and itemMAT='" & matCode & "' ) " & _
                '                     " insert into " & tempTable3 & "(itemFG,itemParent,itemMAT,qty)values ('" & codeParent & "','" & code & "','" & matCode & "','0')"
                'Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)


            End With
        Next
    End Sub
    Sub ExeSQL(SQL As String, Code As String)
        Dim USQL As String = SQL.Substring(0, SQL.Length - 1) & " where item='" & Code & "' "
        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Private Sub gvBOMList_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBOMList.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hlShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Item")) Then
                    Dim link As String = "&item= " & .DataItem("Item").ToString.Replace("-", "").Trim
                    link = link & "&spec=" & .DataItem("Spec").ToString.Trim
                    link = link & "&tempTable=tempBOMList" & Session("UserName")
                    hplDetail.NavigateUrl = "CheckBOMPopupSub.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", .DataItem("Spec"))
                End If
            End If
        End With
    End Sub
End Class