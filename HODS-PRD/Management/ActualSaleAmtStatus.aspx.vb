
Public Class ActualSaleAmtStatus
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim saleType As String = "'2201','2202','2203','2204','2205','2213'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' and MQ001 in(" & saleType & ") order by MQ002"
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            'If Session("UserName") = "" Then
            '    Response.Redirect("../Login.aspx")
            'End If
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempActualSaleAmount" & Session("UserName")
        genData(tempTable)
    End Sub
    Protected Sub genData(tempTable As String)
        CreateTempTable.createTempActualSaleAmout(tempTable)
        Dim dateTo As String = configDate.dateFormat2(tbDateTo.Text),
            dateFrom As String = configDate.dateFormat2(tbDateFrom.Text),
            ISQL As String = "",
            SQL As String = "",
            WHR As String = "",
            subSQL As String = ""
        'Sale delivery -- sale amt
        WHR = configDate.DateWhere("TG003", dateFrom, dateTo)
        If ddlSaleType.Text = "ALL" Then
            WHR = WHR & " and TH014 in (" & saleType & ") "
        Else
            WHR = WHR & " and TH014 ='" & ddlSaleType.Text & "' "
        End If

        SQL = " select TH014,TH004,SUM(TH008) from JINPAO80.dbo.COPTH left join JINPAO80.dbo.COPTG on TG001=TH001 and TG002=TH002  " & _
              " where TG023 ='Y' and TH037>0 " & WHR & " group by TH014,TH004 "
        ISQL = "insert into " & tempTable & "(SaleType,item,SaleQty) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        'mo receive--Product amt
        WHR = configDate.DateWhere("TB003", dateFrom, dateTo)
        If ddlSaleType.Text = "ALL" Then
            WHR = WHR & " and TA026 in (" & saleType & ") "
        Else
            WHR = WHR & " and TA026 ='" & ddlSaleType.Text & "' "
        End If
        Dim Program As New DataTable, subProgram As New DataTable,
            item As String = "",
            qty As Decimal = 0,
            subSaleType As String = ""

        'SQL = " select TA026 as saleType,TC047 as item,sum(TC014) as qty from SFCTC " & _
        '      " left join MOCTA on TA001=TC004 and TA002=TC005 " & _
        '      " left join SFCTB on TB001=TA001 and TB002=TA002 " & _
        '      " where TC022='Y' " & WHR & " group by  TA026,TC047 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'For i As Integer = 0 To Program.Rows.Count - 1
        '    With Program.Rows(i)
        '        subSaleType = .Item("saleType")
        '        item = .Item("item")
        '        qty = .Item("qty")
        '        'sale invoice
        '        subSQL = " select top 1 TB023 from ACRTB " & _
        '                 " left join ACRTA on TA001 =TB001 and TA002=TB002 " & _
        '                 " left join COPTH on TH001=TB005 and TH002=TB006 and TH003=TB007 " & _
        '                 " where TA079='1' and TA001 in('LC','EX') and TA025='Y' and TB023>0 and TH004='" & item & "' and TH014='" & subSaleType & "' " & _
        '                 " order by TA003 desc "
        '        subProgram = Conn_SQL.Get_DataReader(subSQL, Conn_SQL.ERP_ConnectionString)
        '        If subProgram.Rows.Count = 0 Then
        '            subSQL = " select * from COPTH left join COPTG on TG001=TH001 and TG0012TH002 " & _
        '                     " where  "
        '        Else

        '        End If
        '    End With
        'Next
        ''inventory amt
    End Sub
End Class