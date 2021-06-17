Imports System.Globalization

Public Class DeliveryPlanStatus
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim DataSOtype As String = "('2201','2202','2203','2204','2205','2213')"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' and MQ001 in " & DataSOtype & " order by MQ002 "
            ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 6, Conn_SQL.ERP_ConnectionString)

            'If Session("UserName") = "" Then
            '    'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            'End If

            btExport.Visible = False

        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click

        Dim tempTable As String = "DeliveryPlanStatus" & Session("UserName")
        Dim fdate As Date
        Dim amtDate As Integer

        CreateTempTable.createTempDeliveryPlanStatus(tempTable, fdate, amtDate)

        gvShow.DataSource = ""
        gvShow.DataBind()
        Dim SQL As String = genSQL(True)

        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        Dim maxWC As Integer = 1

        If Program.Rows.Count > 0 Then
            maxWC = Program.Rows(0).Item("maxProcess")
        End If


        Dim dt As Data.DataTable = New DataTable
        dt.Columns.Add(New DataColumn("Cus. ID"))
        dt.Columns.Add(New DataColumn("Cus. Name"))
        dt.Columns.Add(New DataColumn("Item"))
        dt.Columns.Add(New DataColumn("Spec."))
        dt.Columns.Add(New DataColumn("TTL. Delv. Qt'y"))
        dt.Columns.Add(New DataColumn("FG Qt'y"))
        dt.Columns.Add(New DataColumn("FG Bal."))
        dt.Columns.Add(New DataColumn("MO"))
        dt.Columns.Add(New DataColumn("Delv. Date"))

        Dim soType = cblSaleType.Text.Trim,
        soNo = tbSaleNo.Text.Trim,
        soSeq = tbSaleSeq.Text.Trim,
        CustID = tbCust.Text.Trim,
        DNNo = tbDelNo.Text.Trim,
        Item = tbItem.Text.Trim,
        Spec = tbSpec.Text.Trim

        Dim strDate As String = "",
            endDate As String = "",
            WHR As String = "",
            WHRSum As String = ""



        If CustID <> "" Then
            WHR = WHR & " and COPMA.MA001 ='" & CustID & "' "
        End If
        If Item <> "" Then
            WHR = WHR & " and COPTH.TH004 like '%" & Item & "%' "
            WHR = WHR & " and MOCTA.TA006 like '%" & Item & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and COPTH.TH006 like '%" & Spec & "%' "
        End If
        If soType <> "" And soType <> "ALL" Then
            WHR = WHR & " and COPTH.TH014 like '%" & soType & "%' "
        Else
            WHR = WHR & " and COPTH.TH014 in " & DataSOtype & ""
        End If
        If soNo <> "" Then
            WHR = WHR & " and COPTH.TH015 like '%" & soNo & "%' "
        End If
        If soSeq <> "" Then
            WHR = WHR & " and COPTH.TH016 like '%" & soSeq & "%' "
        End If
        If DNNo <> "" Then
            WHR = WHR & " and COPTG.TG002 like '%" & DNNo & "%' "

        End If


        SQL = "select COPMA.MA001 as 'Cus ID', COPMA.MA002  as 'Cus Name'," & _
            "(SUBSTRING ( COPTH.TH004,1,14)+'-'+ SUBSTRING (COPTH.TH004,15,2)) as 'Item' ," & _
            " COPTH.TH006 as 'Spec', " & _
            " convert(int,SUM (COPTH.TH008)) as 'TTL. Delv Qty' ,convert(int,INVMC.MC007) as 'FG', " & _
            " convert(int, (INVMC.MC007-sum(COPTH.TH008)) )as 'FG Bal.' , convert(int,sum(MOCTA.TA015)) as 'MO Qty'" & _
            " from COPTH " & _
            " left join INVMB on INVMB.MB001 = COPTH.TH004 and INVMB.MB003 = COPTH.TH006      " & _
            " left join INVMC on INVMC.MC001 = INVMB.MB001 and INVMC.MC002 = INVMB.MB017      " & _
            " left join COPTG on COPTG.TG001 = COPTH.TH001 and COPTG.TG002 = COPTH.TH002 " & _
            " left join COPMA on COPMA.MA001 = COPTG.TG004" & _
            " left join MOCTA on MOCTA.TA006 = COPTH.TH004 and MOCTA.TA026 = COPTH.TH014 and MOCTA.TA027 = COPTH.TH015 and MOCTA.TA028 = COPTH.TH016 " & _
            " where COPTG.TG023 = 'N' and MOCTA.TA013 = 'Y' and MOCTA.TA011 in ('1','2','3')" & WHR & _
            " group by COPMA.MA001,COPMA.MA002,COPTH.TH004,COPTH.TH004,COPTH.TH006,INVMC.MC007,MOCTA.TA006"


        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        Dim lastWO As String = ""
        Dim dr1 As DataRow
        Dim wcSeq As Integer, scrapQty As Integer, sQty As Integer = 0
        For i As Integer = 0 To Program.Rows.Count - 1
            dr1 = dt.NewRow()
            dr1("Cus. ID") = Program.Rows(i).Item("Cus ID")
            dr1("Cus. Name") = Program.Rows(i).Item("Cus Name")
            dr1("Item") = Program.Rows(i).Item("Item")
            dr1("Spec.") = Program.Rows(i).Item("Spec")
            dr1("TTL. Delv. Qt'y") = Program.Rows(i).Item("TTL. Delv Qty")
            dr1("FG Qt'y") = Program.Rows(i).Item("FG")
            dr1("Fg Bal.") = Program.Rows(i).Item("FG Bal.")
            dr1("MO") = Program.Rows(i).Item("MO Qty")
            dr1("Delv. Date") = "Delv. (FG Bal.)"

        Next
        If Program.Rows.Count > 0 Then
            setGrid(dt, dr1, wcSeq, maxWC)
            gvShow.DataSource = dt
            gvShow.DataBind()
        End If
    End Sub


    Sub setGrid(ByRef dt As DataTable, ByVal dr1 As DataRow, ByVal wcSeq As Integer, ByVal maxwc As Integer)

        For i As Integer = wcSeq To maxwc
            
        Next

        dt.Rows.Add(dr1)
       
    End Sub


    Function genSQL(Optional ByVal maxWC As Boolean = False) As String

        'Dim CustID = tbCust.Text, PartNo = tbPart.Text, PartSpec = tbSpec.Text
        'Dim woType = ddlWorkType.Text, woNo = tbWorkNo.Text, statusCode = ddlStatusCode.Text
        Dim soType = cblSaleType.Text.Trim,
            soNo = tbSaleNo.Text.Trim,
            soSeq = tbSaleSeq.Text.Trim,
            CustID = tbCust.Text.Trim,
            DNNo = tbDelNo.Text.Trim,
            Item = tbItem.Text.Trim,
            Spec = tbSpec.Text.Trim

        Dim strDate As String = "",
            endDate As String = "",
            SQL As String = "",
            WHR As String = "",
            SQLSum As String = "",
            WHRSum As String = ""



        If CustID <> "" Then
            WHR = WHR & " and COPTC.TC004 ='" & CustID & "' "
        End If
        If Item <> "" Then
            WHR = WHR & " and COPTH.TH004 like '%" & Item & "%' "
            WHRSum = WHRSum & " and MOCTA.TA006 like '%" & Item & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and COPTH.TH006 like '%" & Spec & "%' "
        End If

        If soType <> "" And soType <> "ALL" Then
            WHR = WHR & " and COPTH.TH014 = '" & soType & "' "
        End If
        If soNo <> "" Then
            WHR = WHR & " and COPTH.TH015 = '" & soNo & "' "
        End If
        If soSeq <> "" Then
            WHR = WHR & " and COPTH.TH016 = '" & soSeq & "' "
        End If


        Dim fldDate As String = ""
    
        If strDate <> "" And endDate <> "" Then
            WHR = WHR & Conn_SQL.Where(fldDate, strDate, endDate)
        Else
            If strDate = "" Then
                WHR = WHR & " and " & fldDate & " = '" & endDate & "' "
            Else
                WHR = WHR & " and " & fldDate & " = '" & strDate & "' "
            End If
        End If


        If maxWC = False Then
    
        End If
    End Function

    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("DeliveryPlanStatus" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Item")) Then
                    Dim link As String = ""
                    Dim CusCode As String = tbCust.Text.Trim,
                       Spec As String = tbSpec.Text.Trim,
                      SONo As String = tbSaleNo.Text.Trim,
                     SOSeq As String = tbSaleSeq.Text.Trim,
                     DNNo As String = tbDelNo.Text.Trim,
                   DateFrom As String = tbDelFrom.Text.Trim,
                    DateTo As String = tbDelTo.Text,
                    SO As String = cblSaleType.Text

                    Dim Item As String = .DataItem("Item").ToString.Replace("-", "")
                    'link = link & "&Item= " & Item
                    link = link & "&Spec= " & .DataItem("Spec.")
                    link = link & "&CusID= " & .DataItem("Cus. ID")
                    link = link & "&CusName= " & .DataItem("Cus. Name")
                    link = link & "&DelQty= " & .DataItem("TTL. Delv. Qt'y")
                    link = link & "&FGQty= " & .DataItem("FG Qt'y")
                    link = link & "&FGBal= " & .DataItem("Fg Bal.")
                    link = link & "&MO= " & .DataItem("MO")
                    link = link & "&SONo= " & SONo
                    link = link & "&SOSeq= " & SOSeq
                    link = link & "&CusCode= " & CusCode
                    link = link & "&Item= " & Item
                    'link = link & "&Spec= " & Spec
                    link = link & "&DateFrom= " & DateFrom
                    link = link & "&DateTo= " & DateTo
                    link = link & "&SO= " & SO
                    link = link & "&DNNo= " & DNNo


                                hplDetail.NavigateUrl = "DeliveryPlanStatusPopup.aspx?height=150&width=350" & link
                                hplDetail.Attributes.Add("title", Item)
                            End If
                            .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                            .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
                        End If
                    End With

    End Sub
End Class