Public Class PrintLabelDelta
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim createTableWarehouse As New createTableWarehouse
    Dim tableName As String = "LabelDeltaRecord"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            createTableWarehouse.CreateLabelRecord()
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            reset()
        End If
    End Sub

    Protected Sub reset()
        'tbCustId.Text = ""
        Dim SQL As String = "select rtrim(Code) Code,rtrim(Code)+':'+rtrim(Name) Name from CodeInfo where CodeType='PLANT' order by Code "
        ControlForm.showDDL(ddlPlant, SQL, "Name", "Code", True, Conn_SQL.MIS_ConnectionString)

        SQL = "select rtrim(MA001) MA001,rtrim(MA001)+':'+rtrim(MA002) MA002 from COPMA where COPMA.UDF01='Yes' order by MA001"
        ControlForm.showDDL(ddlCust, SQL, "MA002", "MA001", False, Conn_SQL.ERP_ConnectionString)

        ucInvType.setObjectFull = "HT"

        ucDate.dateVal = ""
        tbInvNo.Text = ""
        tbTime.Text = ""

        btExport.Visible = False
        btGenBar.Visible = False
        btPrint.Visible = False
    End Sub

    Function getWhr() As String
        Dim WHR As String

        WHR = Conn_SQL.Where("ACRTA.TA004", ddlCust)
        WHR &= Conn_SQL.Where("ACRTA.UDF06", ddlPlant)
        WHR &= Conn_SQL.Where("ACRTA.TA001", ucInvType.getObject)
        WHR &= Conn_SQL.Where("ACRTA.TA002", tbInvNo)
        WHR &= Conn_SQL.Where("ACRTA.TA003", If(ucDate.dateVal = "", Date.Now.ToString("yyyyMMdd"), ucDate.dateVal), True, False)
        WHR &= Conn_SQL.Where("ACRTA.UDF08", tbTime)
        WHR &= Conn_SQL.Where("substring(isnull(ACRTB.UDF01,''),1,1)", ddlGenBar)

        Return WHR
    End Function

    Protected Function getSelSQL() As String
        Dim SQL As String,
            WHR As String = getWhr(),
            fld As String = ""

        'WHR = Conn_SQL.Where("ACRTA.TA004", ddlCust)
        'WHR &= Conn_SQL.Where("ACRTA.UDF06", ddlPlant)
        'WHR &= Conn_SQL.Where("ACRTA.TA001", ucInvType.getObject)
        'WHR &= Conn_SQL.Where("ACRTA.TA002", tbInvNo)
        'WHR &= Conn_SQL.Where("ACRTA.TA003", If(ucDate.dateVal = "", Date.Now.ToString("yyyyMMdd"), ucDate.dateVal), True, False)
        'WHR &= Conn_SQL.Where("ACRTA.UDF08", tbTime)
        'WHR &= Conn_SQL.Where("substring(isnull(ACRTB.UDF01,''),1,1)", ddlGenBar)

        fld &= "rtrim(ACRTA.TA004)+':'+COPMA.MA002 L,"
        fld &= "ACRTA.TA003 C,"
        fld &= "ACRTA.TA001 A,"
        fld &= "ACRTA.TA002 B,"
        fld &= "ACRTB.TB003 D,"
        fld &= "ACRTB.TB039 E,"
        fld &= "ACRTB.TB041 F,"
        fld &= "ACRTB.TB048 G,"
        fld &= "ACRTB.TB022 H,"
        fld &= "isnull(INVMB.UDF51,0) I,"
        fld &= "case when isnull(INVMB.UDF51,0)=0 then 0 else floor(ACRTB.TB022/INVMB.UDF51) end J,"
        fld &= "case when isnull(INVMB.UDF51,0)=0 then 0 else ACRTB.TB022 % INVMB.UDF51 end K,"
        fld &= "ACRTB.TB042 Q,"
        fld &= "ACRTA.UDF06 M,"
        fld &= "ACRTA.UDF08 P,"
        fld &= "isnull(COPTH.UDF01,'') N,"
        fld &= "isnull(COPTH.UDF02,'') O,"
        fld &= "case when isnull(ACRTB.UDF01,'')='' then 'No' else ACRTB.UDF01 end R,"
        fld &= "isnull(INVMB.UDF02,'') S,"
        fld &= "isnull(ACRTB.UDF02,'') T,"
        'INVMB.UDF02

        SQL = " select " & fld.Substring(0, fld.Length - 1) & " from ACRTB " & _
              " left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002 " & _
              " left join COPTH on TH001=ACRTB.TB005 and TH002=ACRTB.TB006 and TH003=ACRTB.TB007 " & _
              " left join COPTD on COPTD.TD001=COPTH.TH014 and COPTD.TD002=COPTH.TH015 and COPTD.TD003=COPTH.TH016 " & _
              " left join COPMA on COPMA.MA001=ACRTA.TA004 " & _
              " left join INVMB on INVMB.MB001=ACRTB.TB039 " & _
              " where 1=1 " & WHR & " order by ACRTA.UDF06,ACRTA.UDF08,ACRTA.TA001,ACRTA.TA002,ACRTA.TA003"
        Return SQL
    End Function

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim colName As ArrayList = New ArrayList,
            fld As String = ""
        colName.Add("Cust:L")
        'fld &= "rtrim(ACRTA.TA004)+':'+COPMA.MA002 L,"
        colName.Add("Inv Date:C")
        'fld &= "ACRTA.TA003 C,"
        colName.Add("Inv Type:A")
        'fld &= "ACRTA.TA001 A,"
        colName.Add("Inv No:B")
        'fld &= "ACRTA.TA002 B,"
        colName.Add("Inv Seq:D")
        'fld &= "ACRTB.TB003 D,"
        colName.Add("Item:E")
        'fld &= "ACRTB.TB039 E,"
        colName.Add("Spec:F")
        'fld &= "ACRTB.TB041 F,"
        colName.Add("Cust PO:G")
        'fld &= "ACRTB.TB048 G,"
        colName.Add("Qty:H:0")
        'fld &= "ACRTB.TB022 H,"
        colName.Add("Pcs/Box:I:0")
        'fld &= "isnull(INVMB.UDF51,0) I,"
        colName.Add("Full Box:J:0")
        'fld &= "case when isnull(INVMB.UDF51,0)=0 then 0 else floor(ACRTB.TB022/INVMB.UDF51) end J,"
        colName.Add("Last Box:K:0")
        'fld &= "case when isnull(INVMB.UDF51,0)=0 then 0 else ACRTB.TB022 % INVMB.UDF51 end K,"
        colName.Add("Unit:Q")
        'fld &= "ACRTB.TB042 Q,"
        colName.Add("Plant:M")
        'fld &= "ACRTA.UDF06 M,"
        colName.Add("Time:P")
        'fld &= "ACRTA.UDF08 P,"
        colName.Add("Cust WO:N")
        'fld &= "isnull(COPTH.UDF01,'') N,"
        colName.Add("Cust Line:O")
        'fld &= "isnull(COPTH.UDF02,'') O,"
        colName.Add("Model:S")
        'fld &= "isnull(INVMB.UDF02,'') S,"
        colName.Add("Gen Barcode:R")
        'fld &= "case when isnull(ACRTB.UDF01,'')='' then 'No' else ACRTB.UDF01 end R,"
        colName.Add("Serail No(First):T")
        'fld &= "case when isnull(ACRTB.UDF01,'')='' then 'No' else ACRTB.UDF01 end R,"

        ControlForm.GridviewColWithLinkFirst(gvShow, colName, True, "Print INV", True, "Print")
        'SQL = " select " & fld.Substring(0, fld.Length - 1) & " from ACRTB " & _
        '      " left join ACRTA on ACRTA.TA001=ACRTB.TB001 and ACRTA.TA002=ACRTB.TB002 " & _
        '      " left join COPTH on TH001=ACRTB.TB005 and TH002=ACRTB.TB006 and TH003=ACRTB.TB007 " & _
        '      " left join COPTD on COPTD.TD001=COPTH.TH014 and COPTD.TD002=COPTH.TH015 and COPTD.TD003=COPTH.TH016 " & _
        '      " left join COPMA on COPMA.MA001=ACRTA.TA004 " & _
        '      " left join INVMB on INVMB.MB001=ACRTB.TB039 " & _
        '      " where 1=1 " & WHR & " order by ACRTA.UDF06,ACRTA.UDF08,ACRTA.TA001,ACRTA.TA002,ACRTA.TA003"

        ControlForm.ShowGridView(gvShow, getSelSQL(), Conn_SQL.ERP_ConnectionString)
        Dim rowGet As Decimal = ControlForm.rowGridview(gvShow)
        ucCount.RowCount = rowGet
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShow", "gridviewScrollgvShow();", True)

        Dim showExport As Boolean = False,
            showGenBar As Boolean = False,
            showPrint As Boolean = False

        If rowGet > 0 Then
            showExport = True
            If ddlGenBar.Text = "N" And tbTime.Text.Trim <> "" Then
                showGenBar = True
            End If
            If ddlGenBar.Text = "Y" And tbTime.Text.Trim <> "" Then 'And ucDate.dateVal <> ""
                showPrint = True
            End If
        End If

        btExport.Visible = showExport
        btGenBar.Visible = showGenBar
        btPrint.Visible = showPrint

    End Sub

    Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        reset()
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Print INV = Type(iType)+No(iNo)
                'Print     = Type(iType)+No(iNo)+Seq(iSeq)
                Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplDetail"), HyperLink) 'Print INV
                Dim whr As String = ""
                If Not IsNothing(hplDetail) Then
                    If Not IsDBNull(.DataItem("A")) Then
                        whr = Conn_SQL.Where("invType", .DataItem("A").ToString.Trim, True, False)
                        whr &= Conn_SQL.Where("invNo", .DataItem("B").ToString.Trim, True, False)
                        hplDetail.NavigateUrl = "~/PDF/labelDelta.aspx?height=150&width=350&whr=" & Conn_SQL.EncodeTo64UTF8(whr)
                        hplDetail.Attributes.Add("title", .DataItem("F"))
                    End If
                End If
                Dim hplDetail2 As HyperLink = CType(e.Row.FindControl("hplDetail2"), HyperLink) 'Print
                If Not IsNothing(hplDetail2) Then
                    If Not IsDBNull(.DataItem("A")) Then
                        whr = Conn_SQL.Where("invType", .DataItem("A").ToString.Trim, True, False)
                        whr &= Conn_SQL.Where("invNo", .DataItem("B").ToString.Trim, True, False)
                        whr &= If(.DataItem("D").ToString.Trim <> "", Conn_SQL.Where("invSeq", .DataItem("D").ToString.Trim, True, False), "")

                        hplDetail2.NavigateUrl = "~/PDF/labelDelta.aspx?height=150&width=350&whr=" & Conn_SQL.EncodeTo64UTF8(whr)
                        hplDetail2.Attributes.Add("title", .DataItem("F"))
                    End If
                End If
            End If
        End With
    End Sub

    Protected Sub btGenBar_Click(sender As Object, e As EventArgs) Handles btGenBar.Click
        'get serail no
        Dim dateCode As String = gvShow.Rows(0).Cells(3).Text.Trim,
            custCode As String = gvShow.Rows(0).Cells(2).Text.Trim
        Dim cc() As String = custCode.Split(":")

        Dim SQL As String = "select COPMA.UDF02 vcode,COPMA.UDF03 tcode from COPMA where MA001='" & cc(0) & "' "
        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim vCode As String = ""
        Dim tCode As String = ""
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                vCode = Trim(.Item(0))
                tCode = Trim(.Item(1))
            End With
        End If
        Dim serialNo As Decimal = getSerail(dateCode)
        Dim fSerialNo As Decimal
        dt = Conn_SQL.Get_DataReader(getSelSQL(), Conn_SQL.ERP_ConnectionString)

        With dt
            Dim strSQL As String = "",
                strSQL2 As String = ""
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    'strSQL = ""
                    Dim fullBox As Integer = .Item("J")
                    fSerialNo = serialNo
                    For j As Integer = 1 To fullBox
                        strSQL &= getStrSql(dt.Rows(i), serialNo, "F", .Item("I"), vCode, tCode)
                        serialNo += 1
                    Next

                    Dim lastBox As Decimal = .Item("K")
                    If lastBox > 0 Then
                        strSQL &= getStrSql(dt.Rows(i), serialNo, "L", lastBox, vCode, tCode)
                        serialNo += 1
                    End If

                    Dim fld As Hashtable = New Hashtable,
                        whr As Hashtable = New Hashtable

                    whr.Add("TB001", Trim(.Item("A")))
                    whr.Add("TB002", Trim(.Item("B")))
                    whr.Add("TB003", Trim(.Item("D")))
                    fld.Add("UDF01", "Yes")
                    fld.Add("UDF02", CStr(fSerialNo))
                    strSQL2 &= Conn_SQL.GetSQL("ACRTB", fld, whr, "U")

                    'Conn_SQL.Exec_Sql(strSQL, Conn_SQL.MIS_ConnectionString)

                End With
            Next
            If strSQL <> "" Then
                Conn_SQL.Exec_Sql(strSQL, Conn_SQL.MIS_ConnectionString)
            End If
            If strSQL2 <> "" Then
                Conn_SQL.Exec_Sql(strSQL2, Conn_SQL.ERP_ConnectionString)
            End If
            If strSQL <> "" And strSQL2 <> "" Then
                show_message.ShowMessage(Page, "Generate Barcode complete?(รันลาเบลเสร็จแล้ว)", UpdatePanel1)
                ddlGenBar.Text = "Y"
                btShow_Click(sender, e)
            End If
        End With
        'For i As Integer = 0 To 20
        '    serialNo += 1
        'Next
        'Dim ff As String = "00011"
    End Sub

    Protected Function getStrSql(dr As DataRow, serialNo As String, packType As String, qty As Decimal, vcode As String, tCode As String) As String
        Dim fld As Hashtable = New Hashtable,
            whr As Hashtable = New Hashtable
        With dr

            whr.Add("docNo", serialNo)
            fld.Add("invType", Trim(.Item("A")))
            fld.Add("invNo", Trim(.Item("B")))
            fld.Add("invSeq", Trim(.Item("D")))
            fld.Add("plant", Trim(.Item("M")))
            fld.Add("item", Trim(.Item("E")))
            Dim pp() As String = Trim(.Item("F")).Split("REV")
            fld.Add("spec", pp(0))
            fld.Add("vender", vcode)
            fld.Add("dateCode", Trim(.Item("C")))
            fld.Add("tradeCode", tCode)
            fld.Add("custPO", Trim(.Item("G")))
            fld.Add("unit", "PCE")
            fld.Add("qty", .Item("H"))
            fld.Add("packType", packType)
            fld.Add("qty_pack", qty)
            fld.Add("timeSend", Trim(.Item("P")))
            fld.Add("custWO", Trim(.Item("N")))
            fld.Add("custLine", Trim(.Item("O")))
            fld.Add("custModel", Trim(.Item("S")))
            fld.Add("CreateBy", Session("UserName"))
            fld.Add("CreateDate", DateTime.Now.ToString("yyyyMMdd hhmmss"))

        End With

        Return Conn_SQL.GetSQL(tableName, fld, whr, "I")
    End Function

    Protected Function getSerail(dateCode As String) As Decimal
        Dim SQL As String = "select top 1 docNo from " & tableName & " where dateCode='" & dateCode & "' order by docNo desc"
        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Dim docNo As Decimal = CDec(dateCode & "000001")
        If dt.Rows.Count = 1 Then
            docNo = dt.Rows(0).Item(0) + 1
        End If
        Return docNo
    End Function

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        Response.Redirect("~/PDF/labelDelta.aspx?height=150&width=350&whr=" & Conn_SQL.EncodeTo64UTF8(getWhr()))
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("LabelDelta" & Session("UserName"), gvShow)
    End Sub
End Class