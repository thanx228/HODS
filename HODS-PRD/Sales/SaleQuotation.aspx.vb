Public Class SaleQuotation
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showCheckboxList(cblType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "saleNotQuon" & Session("UserName")
        CreateTempTable.createTempSaleQuontation(tempTable)
        'select sale order data
        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = ""

        WHR = WHR & Conn_SQL.Where("TD001", cblType)
        WHR = WHR & Conn_SQL.Where("TD002", tbSaleNo)
        WHR = WHR & Conn_SQL.Where("TC004", tbCust)
        WHR = WHR & Conn_SQL.Where("TD016", ddlStatus)
        WHR = WHR & Conn_SQL.Where("TD004", tbItem)
        WHR = WHR & Conn_SQL.Where("TD005", tbDesc)
        WHR = WHR & Conn_SQL.Where("TD006", tbSpec)
        WHR = WHR & Conn_SQL.Where("TC003", configDate.dateFormat2(tbFrom.Text), configDate.dateFormat2(tbEnd.Text))
        SQL = " select TD001,TD002,TD003,TC003,TC004,TC008,TD004,TD008,TD011 from COPTD left join COPTC on TC001=TD001 and TC002=TD002 " & _
              " where 1=1 " & WHR & _
              " order by TD004,TC003"
        Dim rs As New DataTable
        rs = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        With rs
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim item As String = .Item("TD004").ToString.Trim,
                        currency As String = .Item("TC008").ToString.Trim,
                        docDate As String = .Item("TC003").ToString.Trim,
                        soQty As Decimal = .Item("TD008"),
                        soPrice As Decimal = .Item("TD011"),
                        quPrice As Decimal = 0,
                        quDate As String = "",
                        soType As String = .Item("TD001").ToString.Trim,
                        soNo As String = .Item("TD002").ToString.Trim,
                        soSeq As String = .Item("TD003").ToString.Trim,
                        cust As String = .Item("TC004").ToString.Trim
                    Dim SSQL As String = ""
                    SSQL = " select top 1 TB001,TB002,TB003,TA003,TB013,TB009 from COPTB left join COPTA on TA001=TB001 and TA002=TB002 " & _
                           " where TA019 = 'Y' and TA004='" & cust & "' and TA007='" & currency & "' and TB016<='" & docDate & "' and TB004='" & item & "' order by TB016 desc "
                    Dim subRs As New DataTable
                    subRs = Conn_SQL.Get_DataReader(SSQL, Conn_SQL.ERP_ConnectionString)
                    If subRs.Rows.Count > 0 Then
                        With subRs.Rows(0)
                            quPrice = .Item("TB009")
                            quDate = .Item("TA003").ToString.Trim
                            If .Item("TB013").ToString.Trim = "Y" Then 'price of quantity is checked
                                Dim SQL2 As String = "select top 1 TK004,TK006 from COPTK where TK001='" & .Item("TB001").ToString.Trim & "' and TK002 = '" & .Item("TB002").ToString.Trim & "' and TK003 = '" & .Item("TB003").ToString.Trim & "' and TK004 <= " & soQty & " order by TK004 desc"
                                Dim subRs2 As New DataTable
                                subRs2 = Conn_SQL.Get_DataReader(SQL2, Conn_SQL.ERP_ConnectionString)
                                If subRs2.Rows.Count > 0 Then
                                    With subRs2.Rows(0)
                                        quPrice = .Item("TK006")
                                    End With
                                End If
                            End If
                        End With
                    End If
                    ISQL = "insert into " & tempTable & "(soType,soNo,soSeq,quPrice,quDate) values('" & soType & "','" & soNo & "','" & soSeq & "'," & quPrice & ",'" & quDate & "')"
                    Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
                End With
            Next
        End With
        WHR = ""
        If cbNotQuon.Checked Then
            WHR = WHR & " and quPrice=0 "
        End If
        If cbPrice.Checked Then
            WHR = WHR & " and quPrice>TD011 and quPrice>0"
        End If
        SQL = " select TD001+'-'+TD002+'-'+TD003 TD001,TC003,TC004+'-'+MA002 TC004,TD004,TD006,TD008,TD011,quPrice,quDate " & _
              " from DBMIS.dbo." & tempTable & " T left join COPTD on TD001=T.soType and TD002=T.soNo and TD003=T.soSeq " & _
              " left join COPTC on TC001=TD001 and TC002=TD002 " & _
              " left join COPMA on MA001=TC004 " & _
              " where 1=1 " & WHR & " order by TD001,TD002,TD003 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

        'Dim aa As String = ""


    End Sub
End Class