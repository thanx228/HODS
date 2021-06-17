Public Class POPricesNoInquiry
    Inherits System.Web.UI.Page
    Dim CreateTempTable As New CreateTempTable
    Dim Conn_SQL As New ConnSQL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btPreview.Attributes.Add("onClick", "return chkdata();")
            Fromdate_CalendarExtender.Format = "MMM/dd/yyyy"
            Todate_CalendarExtender.Format = "MMM/dd/yyyy"
        End If

    End Sub

    Protected Sub btPreview_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPreview.Click
        InsertTable()
        Dim Fromdate As String = CDate(Me.Fromdate.Text).ToString("dd/MMM/yyyy")
        Dim Todate As String = CDate(Me.Todate.Text).ToString("dd/MMM/yyyy")
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('POPricesNoInquiry1.aspx?Fdate=" + Fromdate.ToString + "&Tdate=" + Todate.ToString + "');", True)

    End Sub
    Private Sub InsertTable()
        Dim TempTable As String = "TempPoNotInquiry" & Session("Username")
        CreateTempTable.CreateTempPoNotInquiryTable(TempTable)
        Dim Fdate As DateTime = Me.Fromdate.Text
        Dim Fromdate As String = Format(Fdate, "yyyyMMdd")
        Dim Tdate As DateTime = Me.Todate.Text
        Dim Todate As String = Format(Tdate, "yyyyMMdd")
        Dim SelSQl As String = "Select TC.COMPANY,CASE  WHEN TC003 != ''  THEN CONVERT(varchar, CONVERT(datetime, TC003),106)  END as POdate"
        SelSQl = SelSQl & ",TC001 as POTYPE,TC002 as PONO,TD003 as SOSeq,TD004 as PartItem,REPLACE(TD005,'''',' ') as PartDesc,REPLACE(TD006,'''',' ') as PartSpec,TD009 as Unit,TD008 as QTY,TD010 as PRC,TC004"
        'SelSQl = SelSQl & " ,TL001 as InqType,TL002 as InqNo,TM003 as InqSeq,TM008 as ByQty,TM010 as InqPrc"
        SelSQl = SelSQl & " from PURTC TC left join PURTD TD on(TC001=TD001 and TC002=TD002 )"
        'PURTC = PO HEAD,PURTD = PO Lines
        'SelSQl = SelSQl & " left join  PURTL TL on(TC004=TL004)"
        'SelSQl = SelSQl & " left join  PURTM TM on(TL001=TM001 and TL002=TM002 and TD004=TM004)"
        SelSQl = SelSQl & " where  TC014='Y' and TC003 between '" & Fromdate & "' and '" & Todate & "'"
        'เมื่อ TC014 = Approve Indicator = Y,TC003=Purchase Date
        Dim aa As New Data.DataTable
        Dim bb As New Data.DataTable
        Dim cc As New Data.DataTable
        
        aa = Conn_SQL.Get_DataReader(SelSQl, Conn_SQL.ERP_ConnectionString)
        If aa.Rows.Count > 0 Then
            For i As Integer = 0 To aa.Rows.Count - 1
                Dim POQty As Integer = 0
                Dim InqQty As String = ""
                Dim InqPrc As Decimal = 0.0
                Dim POPrc As Decimal = 0.0
                Dim InqNo As String = ""
                Dim InqSeq As String = ""
                Dim InqType As String = ""
                Dim Com As String = aa.Rows(i).Item("COMPANY")
                Dim PODate As String = aa.Rows(i).Item("PODate")
                Dim POType As String = aa.Rows(i).Item("POTYPE")
                Dim PONo As String = aa.Rows(i).Item("PONO")
                Dim PartItem As String = aa.Rows(i).Item("PartItem")
                Dim PartDesc As String = aa.Rows(i).Item("PartDesc")
                Dim PartSpec As String = aa.Rows(i).Item("PartSpec")
                Dim Unit As String = aa.Rows(i).Item("Unit")
                POQty = aa.Rows(i).Item("QTY")
                POPrc = aa.Rows(i).Item("PRC")
                Dim SelSQL2 As String = "Select TL001,TL002,TL004, TM.UDF01  as TMQTY,TM003,TM008,TM010 from PURTL TL  left join PURTM TM on(TL001=TM001 and TL002=TM002) where TL004='" & aa.Rows(i).Item("TC004") & "' and TM004='" & PartItem & "' "
                bb = Conn_SQL.Get_DataReader(SelSQL2, Conn_SQL.ERP_ConnectionString)
                If bb.Rows.Count > 0 Then
                    For ii As Integer = 0 To bb.Rows.Count - 1
                        InqPrc = bb.Rows(ii).Item("TM010")
                        InqType = bb.Rows(ii).Item("TL001")
                        InqNo = bb.Rows(ii).Item("TL002")
                        InqQty = bb.Rows(ii).Item("TMQTY") 'CInt(bb.Rows(ii).Item("TMQTY"))
                        If bb.Rows(ii).Item("TM008") = "Y" Then
                            Dim SelSQL3 As String = "select TN007,TN008 from PURTN where TN001='" & InqType & "' and TN002='" & InqNo & "' and TN003='" & bb.Rows(ii).Item("TM003") & "'"
                            'SelSQL3 = SelSQL3 & " where TN001='" & InqType & "' and TN002='" & InqNo & "' and TN003='" & InqSeq & "'"
                            'PURTN = Price Approval Order Line Details
                            cc = Conn_SQL.Get_DataReader(SelSQL3, Conn_SQL.ERP_ConnectionString)
                            If cc.Rows.Count > 0 Then
                                For j As Integer = 0 To cc.Rows.Count - 1
                                    If Not IsDBNull(cc.Rows(j).Item("TN008")) Then
                                        If POQty >= cc.Rows(j).Item("TN007") Then
                                            InqQty = cc.Rows(j).Item("TN007")
                                            InqPrc = cc.Rows(j).Item("TN008")
                                        End If
                                    End If
                                Next
                            End If
                        End If

                    Next
                End If
               
                
                Dim InsSQL = "Insert into " & TempTable & "(Company,PODate,POType,PONo,PartItem,PartDesc,PartSpec,Unit,InqType,InqNo,POQty,POPrc,InqQty,InqPrc)"
                InsSQL = InsSQL & " VALUES('" & Com & "','" & PODate & "','" & POType & "','" & PONo & "',"
                InsSQL = InsSQL & "'" & PartItem & "','" & PartDesc & "','" & PartSpec & "','" & Unit & "'"
                InsSQL = InsSQL & ",'" & InqType & "','" & InqNo & "',' " & POQty & "','" & POPrc & "','" & InqQty & "','" & InqPrc & "')"
                Conn_SQL.Exec_Sql(InsSQL, Conn_SQL.MIS_ConnectionString)
            Next
        End If
    End Sub
End Class