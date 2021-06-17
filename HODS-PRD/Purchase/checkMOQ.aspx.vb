Public Class checkMOQ
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempMOQ" & Session("UserName")
        CreateTempTable.CreateTempMOQ(tempTable)
        Dim Program As New DataTable
        Dim Program1 As New DataTable
        Dim whr As String = "", SQL As String = "", ISQL As String = "", USQL As String = ""
        Dim code As String = tbCode.Text
        Dim spec As String = tbSpec.Text
        Dim sup As String = tbSup.Text
        Dim dateFrom As String = configDate.dateFormat3(tbDateFrom.Text)
        Dim dateTo As String = configDate.dateFormat3(tbDateTo.Text)
        Dim dateToday As String = DateTime.Now.ToString("yyyyMMdd")
        Dim typeCode As String = ddlTypeCode.Text

        If typeCode <> "0" Then
            whr = whr & " and SUBSTRING(D.TD004,3,1) ='" & typeCode & "' "
        End If
        If code <> "" Then
            whr = whr & " and D.TD004 like '%" & code & "%' "
        End If
        If spec <> "" Then
            whr = whr & " and (D.TD005 like '%" & spec & "%' or D.TD006 like '%" & spec & "%' ) "
        End If
        If sup <> "" Then
            whr = whr & " and C.TC004 like '" & sup & "%' "
        End If
        If dateFrom <> "" Or dateTo <> "" Then
            If dateFrom <> "" And dateTo <> "" Then
                whr = whr & " and C.TC003 between '" & dateFrom & "01' and '" & dateTo & "31' "
            Else
                Dim tt As String = dateFrom
                If tt = "" Then
                    tt = dateTo
                End If
                whr = whr & " and C.TC003 between '" & tt & "01' and '" & tt & "31' "
            End If
        End If
        SQL = " select C.TC004,D.TD004,'PO'+substring(C.TC003,1,6),SUM(D.TD008) from JINPAO80.dbo.PURTD D  " & _
              " left join JINPAO80.dbo.PURTC C on C.TC001=D.TD001 and C.TC002=D.TD002  " & _
              " where C.TC014 = 'Y' " & whr & _
              " group by C.TC004,D.TD004,substring(C.TC003,1,6) "

        ISQL = "insert into " & tempTable & "(sup,code,poYear,poQty) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        SQL = "select code,sup from " & tempTable & " group by code,sup"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim xcode As String = Program.Rows(i).Item("code")
            Dim xsup As String = Program.Rows(i).Item("sup")
            SQL = " select PURTM.UDF01 from PURTM  " & _
                  "   left join PURTL on TL001=TM001 and TL002=TM002 " & _
                  "  where TL006='Y' and TL004='" & xsup.TrimEnd & "' and TM004='" & xcode.TrimEnd & "' " & _
                  "    and (TM015='' or TM015 <='" & dateToday & "' ) order by TL010 desc"
            Program1 = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            If Program1.Rows.Count > 0 Then
                Dim moq = Program1.Rows(0).Item("UDF01")
                If moq <> "" Then
                    USQL = " update " & tempTable & " set MoqQty='" & moq & "'  where code='" & xcode & "' and sup='" & xsup & "' "
                    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                End If
            End If
        Next

        SQL = "select * from " & tempTable
        ISQL = ""
        Dim dt As Data.DataTable = New DataTable
        dt.Columns.Add(New DataColumn("Supplier"))
        dt.Columns.Add(New DataColumn("Supplier Name"))
        dt.Columns.Add(New DataColumn("Code"))
        dt.Columns.Add(New DataColumn("Spec"))
        dt.Columns.Add(New DataColumn("Unit"))
        dt.Columns.Add(New DataColumn("MOQ"))
        SQL = "select poYear from tempMOQ group by poYear order by poYear"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Dim oriPo As New Hashtable
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim poYear As String = Program.Rows(i).Item("poYear").ToString.TrimEnd
            dt.Columns.Add(New DataColumn(poYear))
            oriPo(poYear) = ""
        Next
        dt.Columns.Add(New DataColumn("PO SUM"))
        'SQL = " select * from " & tempTable & " order by sup,code,poYear "
        SQL = " select T.poYear,T.sup,T.code,T.MoqQty,cast(T.poQty as decimal(10,2))as poQty,INVMB.MB002+'-'+INVMB.MB003 as spec,INVMB.MB004 as unit,PURMA.MA002 as sup_name from " & tempTable & " T " & _
              " left join JINPAO80.dbo.INVMB INVMB on INVMB.MB001=T.code " & _
              " left join JINPAO80.dbo.PURMA PURMA on PURMA.MA001=T.sup" & _
              " order by T.sup,T.code,T.poYear "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Dim lastSup As String = "", lastCode As String = ""
        Dim dr As DataRow
        Dim sumPO As Decimal = 0
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim xsup As String = Program.Rows(i).Item("sup")
            Dim xcode As String = Program.Rows(i).Item("code")
            If lastCode <> xsup And lastSup <> xsup Then
                If lastCode <> "" And lastSup <> "" Then
                    dr("PO SUM") = FormatNumber(sumPO.ToString, 0, TriState.True)
                    dt.Rows.Add(dr)
                End If
                dr = dt.NewRow()
                dr("Supplier") = xsup
                dr("Supplier Name") = Program.Rows(i).Item("sup_name").ToString.TrimEnd
                dr("Code") = xcode
                dr("Spec") = Program.Rows(i).Item("spec").ToString.TrimEnd
                dr("Unit") = Program.Rows(i).Item("unit").ToString.TrimEnd
                dr("MOQ") = Program.Rows(i).Item("MoqQty").ToString.TrimEnd
                Dim poYear As DictionaryEntry
                For Each poYear In oriPo
                    Dim val As String = poYear.Key.ToString
                    dr(val) = ""
                Next
                sumPO = 0
            End If
            dr(Program.Rows(i).Item("poYear").ToString.TrimEnd) = FormatNumber(Program.Rows(i).Item("poQty").ToString.TrimEnd, 0, TriState.True)
            Dim poQty As Decimal = Program.Rows(i).Item("poQty")
            sumPO = sumPO + poQty  'CType(Program.Rows(i).Item("poQty"), Double)
            lastSup = xsup
            lastCode = xcode
        Next
        If Program.Rows.Count > 0 Then
            dr("PO SUM") = FormatNumber(sumPO.ToString, 0, TriState.True)
            dt.Rows.Add(dr)
            gvShow.DataSource = dt
            gvShow.DataBind()
        End If
        lbCount.Text = gvShow.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class