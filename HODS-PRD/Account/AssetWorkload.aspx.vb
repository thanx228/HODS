Public Class AssetWorkload
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim createTableSale As New createTableSale
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            'btReset_Click(sender, e)

            ucDate.dateVal = Date.Now.ToString("dd/MM/yyyy")
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)

        End If
    End Sub

    Protected Sub btCal_Click(sender As Object, e As EventArgs) Handles btCal.Click
        Dim ymSel As String = ucDate.dateVal.Substring(0, 6),
            ymNow As String = Date.Now.ToString("yyyyMM"),
            msg As String = "Not Allow",
            strSQL As String = "",
            SQL As String = "",
            dateBegin As DateTime = ConfigDate.strToDateTime(ucDate.dateVal, "yyyyMMdd")

        If ucDate.dateVal <> "" And ymSel >= ymNow Then
            'delete ASTTR,ASTTQ

            strSQL &= "delete from ASTTR where TR001='" & ymSel & "'; "
            strSQL &= "delete from ASTTQ where TQ001='" & ymSel & "'; "
            Conn_SQL.Exec_Sql(strSQL, Conn_SQL.ERP_ConnectionString)

            Dim dayInMonth As String = DateTime.DaysInMonth(CInt(ymSel.Substring(0, 4)), CInt(ymSel.Substring(4, 2))).ToString
            Dim endDateMonth As DateTime = ConfigDate.strToDateTime(ymSel & dayInMonth, "yyyyMMdd")

            Dim fld As Hashtable,
                whr As Hashtable


            'MB001 as ASSET_ID,MB016 as ACQ_DATE,MB053 as USE_WORKLOAD,(MB052-MB053)as BAL_WORKLOAD 
            SQL = " select MB001,MB016,MB053,MB052-MB053 MB0523  from ASTMB " &
                  " where MB052-MB053>0 and (CASE WHEN MB021=0 THEN MB020 ELSE 0 END)+MB021-MB029-MB056 > 0 " &
                  " And MB016 <='" & ymSel & dayInMonth & "' and MB024='3'; "
            Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    Dim asset As String = .Item("MB001")
                    Dim useWorkLoad As String = .Item("MB053")

                    Dim dateAcq As DateTime = ConfigDate.strToDateTime(.Item("MB016").ToString.Trim, "yyyyMMdd")
                    Dim dateVal As DateTime = dateBegin
                    If DateTime.Compare(dateBegin, dateAcq) < 0 Then
                        dateVal = dateAcq
                    End If

                    Dim balWorkload As Short = CShort(.Item("MB0523"))
                    Dim cntDayBal As Short = CShort((DateAndTime.DateDiff(DateInterval.Day, dateVal, endDateMonth, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) + 1L))

                    If balWorkload < cntDayBal Then
                        cntDayBal = balWorkload
                    End If

                    strSQL = ""

                    Dim dept As String = "******",
                        sumWorkload As Integer = 0
                    SQL = "select MC002 from ASTMC where MC001='" & asset & "';"
                    Dim dtSub As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                    If dtSub.Rows.Count = 0 Then
                        fld = New Hashtable
                        whr = New Hashtable
                        whr.Add("TR001", ymSel)
                        whr.Add("TR002", asset)
                        fld.Add("TR003", dept)
                        fld.Add("TR004", CInt(cntDayBal))

                        fld.Add("COMPANY", Conn_SQL.DBMain)
                        fld.Add("CREATOR", "H" & Session("UserName"))
                        fld.Add("USR_GROUP", "HODS")
                        fld.Add("CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss"))

                        strSQL &= Conn_SQL.GetSQL("ASTTR", fld, whr, "I")
                        sumWorkload += CInt(cntDayBal)
                    Else
                        For j As Integer = 0 To dtSub.Rows.Count - 1
                            With dtSub.Rows(j)
                                fld = New Hashtable
                                whr = New Hashtable

                                whr.Add("TR001", ymSel)
                                whr.Add("TR002", asset)
                                fld.Add("TR003", .Item("MC002"))
                                fld.Add("TR004", CInt(cntDayBal))
                                fld.Add("COMPANY", Conn_SQL.DBMain)
                                fld.Add("CREATOR", "H" & Session("UserName"))
                                fld.Add("USR_GROUP", "HODS")
                                fld.Add("CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss"))

                                strSQL &= Conn_SQL.GetSQL("ASTTR", fld, whr, "I")
                                sumWorkload += CInt(cntDayBal)

                            End With
                        Next
                    End If
                    fld = New Hashtable
                    whr = New Hashtable

                    whr.Add("TQ001", ymSel)
                    whr.Add("TQ002", asset)
                    fld.Add("TQ003", useWorkLoad)
                    fld.Add("TQ004", sumWorkload)

                    fld.Add("COMPANY", Conn_SQL.DBMain)
                    fld.Add("CREATOR", "H" & Session("UserName"))
                    fld.Add("USR_GROUP", "HODS")
                    fld.Add("CREATE_DATE", DateTime.Now.ToString("yyyyMMddHHmmss"))

                    strSQL &= Conn_SQL.GetSQL("ASTTQ", fld, whr, "I")
                    Conn_SQL.Exec_Sql(strSQL, Conn_SQL.ERP_ConnectionString)

                End With
            Next

            msg = "Complete"
        End If
        show_message.ShowMessage(Page, msg, UpdatePanel1)

    End Sub
End Class