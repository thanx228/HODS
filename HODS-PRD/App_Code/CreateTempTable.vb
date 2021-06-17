Imports MIS_HTI.DataControl

Public Class CreateTempTable
    Dim Conn_SQL As New ConnSQL
    Dim dbConn As New DataConnectControl

    Sub CreateTempSaleDeliveryTable(ByVal Temptable As String)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Company  Char (10)  NULL  ,"
        StrSQL &= "OrderType Char (04)  NULL  ,"
        StrSQL &= "OrderNo  Char (11)  NULL  ,"
        StrSQL &= "PartItem Char (20)  NULL  ,"
        StrSQL &= "PartDesc  Char (60)  NULL  ,"
        StrSQL &= "PartSpec Char (60)  NULL  ,"
        StrSQL &= "CustId Char (8)  NULL  ,"
        StrSQL &= "PlanDel  Char (12)  NULL ,"
        StrSQL &= "Qty  Integer  NULL  ,"
        StrSQL &= "DelType Char (04)  NULL  ,"
        StrSQL &= "DelNo  Char (11)  NULL  ,"
        StrSQL &= "DelDate  Char (12)  NULL ,"
        StrSQL &= "DelQty  Integer  NULL  ,"
        StrSQL &= "MOType Char (04)  NULL  ,"
        StrSQL &= "MOlNo  Char (11)  NULL  ,"
        StrSQL &= "PlFnDate  Char (12)  NULL ,"
        StrSQL &= "AcFnDate  Char (12)  NULL ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateTempUnCloseTable(ByVal Temptable As String)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Company  Char (10)  NULL  ,"
        StrSQL &= "OrderType Char (04)  NULL  ,"
        StrSQL &= "OrderNo  Char (11)  NULL  ,"
        StrSQL &= "OrderDate  Char (11)  NULL  ,"
        StrSQL &= "PartItem Char (20)  NULL  ,"
        StrSQL &= "PartDesc  Char (60)  NULL  ,"
        StrSQL &= "PartSpec Char (60)  NULL  ,"
        StrSQL &= "CustId Char (8)  NULL  ,"
        StrSQL &= "PlanDel  Char (8)  NULL ,"
        StrSQL &= "Qty  Integer  NULL  ,"
        StrSQL &= "DelQty  Integer  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempEfficiency(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "DocDate Char (12)  NULL  ,"
        StrSQL &= "Process  Char (30)  NULL  ,"
        StrSQL &= "WoNo  Char (11)  NULL  ,"
        StrSQL &= "WoType Char (04)  NULL  ,"
        StrSQL &= "PartItem Char (20)  NULL  ,"
        StrSQL &= "PartDesc  Char (60)  NULL  ,"
        StrSQL &= "PartSpec Char (60)  NULL  ,"
        StrSQL &= "Qty  Integer  NULL  ,"
        StrSQL &= "Actual  Decimal(8,2)   NULL  ,"
        StrSQL &= "Std  Decimal(8,2)   NULL  ,"
        StrSQL &= "WorkTime  Integer  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        Dim Temptable2 As String = Temptable & "2"
        Dim SelSQL2 As String = " if exists (select * from sysobjects where name='" & Temptable2 & "' )"
        SelSQL2 = SelSQL2 & "Drop Table " & Temptable2
        Conn_SQL.Exec_Sql(SelSQL2, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL2 As String = " if not exists (select * from sysobjects where name='" & Temptable2 & "' )"
        StrSQL2 = StrSQL2 & "CREATE TABLE " & Temptable2 & "("
        StrSQL2 = StrSQL2 & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL2 = StrSQL2 & "DocDate Char (08)  NULL  ,"
        StrSQL2 = StrSQL2 & "PerTarget  Integer  NULL  ,"
        StrSQL2 = StrSQL2 & "PerWork  Decimal(8,2)  NULL  ,"
        StrSQL2 = StrSQL2 & "PerEff  Decimal(8,2)  NULL  ,"
        StrSQL2 = StrSQL2 & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL2, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempInvoicePriceTable(ByVal Temptable As String)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Company  Char (10)  NULL  ,"
        StrSQL &= "InvDate  Char (11)  NULL  ,"
        StrSQL &= "InvType Char (04)  NULL  ,"
        StrSQL &= "InvNo  Char (11)  NULL  ,"
        StrSQL &= "PartItem Char (20)  NULL  ,"
        StrSQL &= "PartDesc  Char (60)  NULL  ,"
        StrSQL &= "PartSpec Char (60)  NULL  ,"
        StrSQL &= "Unit Char (10)  NULL  ,"
        StrSQL &= "QuoType Char (04)  NULL  ,"
        StrSQL &= "QuoNo  Char (11)  NULL  ,"
        StrSQL &= "InvQty  Decimal(8,2)  NULL  ,"
        StrSQL &= "InvPrc Decimal(8,2)  NULL  ,"
        StrSQL &= "QuoQty  Decimal(8,2)  NULL  ,"
        StrSQL &= "QuoPrc Decimal(8,2)  NULL  ,"
        StrSQL &= "OrdType Char (04)  NULL  ,"
        StrSQL &= "OrdNo  Char (11)  NULL  ,"
        StrSQL &= "OrdSeq  Char (08)  NULL  ,"
        StrSQL &= "OrdQty  Decimal(8,2)  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempForecast(ByVal Temptable As String)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Company  Char (10)  NULL  ,"
        StrSQL &= "ForeDate  Char (11)  NULL  ,"
        StrSQL &= "ForeNo  Char (11)  NULL  ,"
        StrSQL &= "CustId Char (08)  NULL  ,"
        StrSQL &= "Channel Char (02)  NULL  ,"
        StrSQL &= "EmpId  Char (06)  NULL  ,"
        StrSQL &= "PartItem Char (20)  NULL  ,"
        StrSQL &= "PartDesc  Char (60)  NULL  ,"
        StrSQL &= "PartSpec Char (60)  NULL  ,"
        StrSQL &= "Unit Char (10)  NULL  ,"
        StrSQL &= "Curr Char (04)  NULL  ,"
        StrSQL &= "Qty  Decimal(8,2)  NULL  ,"
        StrSQL &= "Prc Decimal(8,2)  NULL  ,"
        StrSQL &= "QtyDel Decimal(8,2)  NULL  ,"
        StrSQL &= "Remark  Char (120)  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempSaleOrderNotMO(ByVal Temptable As String)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Company  Char (10)  NULL  ,"
        StrSQL &= "OrdDate  Char (11)  NULL  ,"
        StrSQL &= "PlanDate  Char (11)  NULL  ,"
        StrSQL &= "DueDate  Char (11)  NULL  ,"
        StrSQL &= "OrdType  Char (11)  NULL  ,"
        StrSQL &= "OrdNo  Char (11)  NULL  ,"
        StrSQL &= "OrdSeq Char (08)  NULL  ,"
        StrSQL &= "PartItem Char (20)  NULL  ,"
        StrSQL &= "PartDesc  Char (60)  NULL  ,"
        StrSQL &= "PartSpec Char (60)  NULL  ,"
        StrSQL &= "Unit Char (10)  NULL  ,"
        StrSQL &= "Qty  Decimal(8,2)  NULL  ,"
        StrSQL &= "BOM Char (03)  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempSOPriceTable(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Company  Char (10)  NULL  ,"
        StrSQL &= "SODate  Char (11)  NULL  ,"
        StrSQL &= "SOType Char (04)  NULL  ,"
        StrSQL &= "SONo  Char (11)  NULL  ,"
        StrSQL &= "PartItem Char (20)  NULL  ,"
        StrSQL &= "PartDesc  Char (60)  NULL  ,"
        StrSQL &= "PartSpec Char (60)  NULL  ,"
        StrSQL &= "Unit Char (10)  NULL  ,"
        StrSQL &= "QuoType Char (04)  NULL  ,"
        StrSQL &= "QuoNo  Char (11)  NULL  ,"
        StrSQL &= "SOQty  Decimal(8,2)  NULL  ,"
        StrSQL &= "SOPrc Decimal(8,2)  NULL  ,"
        StrSQL &= "QuoQty  Decimal(8,2)  NULL  ,"
        StrSQL &= "QuoPrc Decimal(8,2)  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempPoNotInquiryTable(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Company  Char (10)  NULL  ,"
        StrSQL &= "PODate  Char (11)  NULL  ,"
        StrSQL &= "POType Char (04)  NULL  ,"
        StrSQL &= "PONo  Char (11)  NULL  ,"
        StrSQL &= "PartItem Char (20)  NULL  ,"
        StrSQL &= "PartDesc  Char (60)  NULL  ,"
        StrSQL &= "PartSpec Char (60)  NULL  ,"
        StrSQL &= "Unit Char (10)  NULL  ,"
        StrSQL &= "InqType Char (04)  NULL  ,"
        StrSQL &= "InqNo  Char (11)  NULL  ,"
        StrSQL &= "POQty  Decimal(8,2)  NULL  ,"
        StrSQL &= "POPrc Decimal(8,2)  NULL  ,"
        StrSQL &= "InqQty  Char (20)  NULL  ,"
        StrSQL &= "InqPrc Decimal(8,2)  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateTempSoNoBom(ByVal Temptable As String)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Company  Char (20)  NULL  ,"
        StrSQL &= "Ordertype  Char (8)  NULL  ,"
        StrSQL &= "Orderno  Char (20)  NULL  ,"
        StrSQL &= "Item Char (20)  NULL  ,"
        StrSQL &= "Description Char (200)  NULL  ,"
        StrSQL &= "Spec Char (80)  NULL  ,"
        StrSQL &= "Quantity Decimal(8,0)  NULL  ,"
        StrSQL &= "Unit  Char (10)  NULL  ,"
        StrSQL &= "Price Decimal(8,2)  NULL  ,"
        StrSQL &= "Remark Char (100)  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'Function CreateTempWorkDay(ByVal Temptable As String, ByVal fdate As Date, ByVal amtDate As Integer)
    '    'and xtype='U'
    '    Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
    '    SelSQL = SelSQL & "Drop Table " & Temptable
    '    Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
    '    StrSQL &= "CREATE TABLE " & Temptable & " ("
    '    StrSQL &= "wc  Char (10) NOT NULL ,"
    '    StrSQL &= "wc_name  Char (50)  NULL ,"
    '    StrSQL &= "hourBefore Char (10)  NULL  ,"

    '    For i As Integer = 0 To amtDate
    '        Dim tdate As String = fdate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
    '        StrSQL &= "hour" & tdate & " Char (10)  NULL  ,"
    '    Next
    '    StrSQL &= "hour_sum Char (10)  NULL  ,"
    '    StrSQL &= "PRIMARY KEY(wc)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Function

    Sub CreateTempWorkDay(ByVal Temptable As String, ByVal fdate As Date, ByVal amtDate As Integer)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "wc  Char (10) NOT NULL ,"
        StrSQL &= "wc_name  Char (50)  NULL ,"
        StrSQL &= "hourBefore Decimal(10,0)   DEFAULT 0   ,"

        For i As Integer = 0 To amtDate
            Dim tdate As String = fdate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            'StrSQL &= "hourLock" & tdate & " Decimal(10,0)  NULL  ,"
            StrSQL &= "hour" & tdate & " Decimal(10,0)   DEFAULT 0   ,"
        Next
        'StrSQL &= "hour_sum Decimal(10,0)  NULL  ,"
        StrSQL &= "PRIMARY KEY(wc)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateTempWorkDayLock(ByVal Temptable As String, ByVal fdate As Date, ByVal amtDate As Integer)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "wc  Char (10) NOT NULL ,"
        StrSQL &= "moStatus  Char (1) NOT NULL ," 'N= no lock,Y=lock
        StrSQL &= "wc_name  Char (50)  NULL ,"
        StrSQL &= "hourBefore Decimal(10,0)  DEFAULT 0   ,"
        For i As Integer = 0 To amtDate
            Dim tdate As String = fdate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            'StrSQL &= "hourLock" & tdate & " Decimal(10,0)  NULL  ,"
            StrSQL &= "hour" & tdate & " Decimal(10,0)   DEFAULT 0   ,"
        Next
        'StrSQL &= "hour_sum Decimal(10,0)  NULL  ,"
        StrSQL &= "PRIMARY KEY(wc,moStatus)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


    Sub CreateTempSaleNotPlan(ByVal Temptable As String)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "SaleType Char(4) NOT NULL  ,"
        StrSQL &= "SaleNo Char(11) NOT NULL  ,"
        StrSQL &= "SaleSeq  Char (4) NOT NULL  ,"
        StrSQL &= "PartNo  Char (20)  DEFAULT ''  ,"
        StrSQL &= "PartSpec  Char (60)  DEFAULT ''  ,"
        StrSQL &= "CustID  Char (10)  DEFAULT ''  ,"
        StrSQL &= "SaleDate  Char (8)  DEFAULT ''  ,"
        StrSQL &= "SaleDelDate  Char (8)  DEFAULT ''  ,"
        StrSQL &= "SaleQty Decimal(16,6)   DEFAULT 0  ,"
        StrSQL &= "DeliveryQty Decimal(16,6)   DEFAULT 0  ,"
        StrSQL &= "statusOrder Char(50)  DEFAULT ''  ,"
        StrSQL &= "IndustryType Char(50) DEFAULT ''  ,"
        StrSQL &= "SaleForcast Char(50)   DEFAULT ''  ,"
        StrSQL &= "LeadTime Decimal(16,6)   DEFAULT 0  ,"
        StrSQL &= "SaleRemark text   DEFAULT ''  ,"

        StrSQL &= "PRIMARY KEY(SaleType,SaleNo,SaleSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempSaleOverDue(ByVal Temptable As String)
        'and xtype='U'
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "SaleType Char(4) NOT NULL  ,"
        StrSQL &= "SaleNo Char(11) NOT NULL  ,"
        StrSQL &= "SaleSeq  Char (4) NOT NULL  ,"
        StrSQL &= "PartNo  Char (20)  NULL  ,"
        StrSQL &= "PartSpec  Char (60)  NULL  ,"
        StrSQL &= "CustID  Char (10)  NULL  ,"
        StrSQL &= "SaleDate  Char (8)  NULL  ,"
        StrSQL &= "SaleDelDate  Char (8)  NULL  ,"
        StrSQL &= "SaleQty Decimal(16,6)  NULL  ,"
        StrSQL &= "DeliveryQty Decimal(16,6)  NULL  ,"
        StrSQL &= "MOFinishDate  Char (8)  NULL  ,"
        StrSQL &= "OverDate Decimal(10,0)  NULL ,"
        StrSQL &= "PRIMARY KEY(SaleType,SaleNo,SaleSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempWorkStatus(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "SaleOrder Char(30) NOT NULL  ,"
        StrSQL &= "WorkOrder Char(30) NOT NULL  ,"
        StrSQL &= "WorkSeq Char(4) NOT NULL  ,"
        StrSQL &= "customer Char(10)  NULL  ,"
        StrSQL &= "PartNo  Char (20)  NULL  ,"
        StrSQL &= "PartSpec  Char (60)  NULL  ,"
        StrSQL &= "SaleQty Decimal(16,6)  NULL  ,"
        StrSQL &= "WorkQty Decimal(16,6)  NULL  ,"
        StrSQL &= "wc  Char (30) NOT NULL ,"
        StrSQL &= "process  Char (50)  NULL ,"
        StrSQL &= "transferQty Decimal(16,6)  NULL  ,"
        StrSQL &= "PRIMARY KEY(SaleOrder,WorkOrder)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempCustoms(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "seq int NOT NULL  ,"
        StrSQL &= "pack  Decimal(16,6)  NULL  ,"
        StrSQL &= "wgh  Decimal(16,6)  NULL  ,"
        StrSQL &= "qty Decimal(16,6)  NULL  ,"
        StrSQL &= "amt Decimal(16,6)  NULL  ,"
        StrSQL &= "po  Char (30) NOT NULL ,"
        StrSQL &= "inv  Char (30) NOT NULL ,"
        StrSQL &= "note nvarchar(255)  NULL  ,"
        StrSQL &= "PRIMARY KEY(seq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempProductivityDetail(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "docDetail Char (30) NOT NULL  ,"
        StrSQL &= "docDate  Char (8)  NOT NULL  ,"
        StrSQL &= "wc  Char (10) NOT NULL ,"
        StrSQL &= "itemSeq  Char (4) NOT NULL ,"
        'StrSQL &= "woType Char (30)  NULL  ,"
        'StrSQL &= "woNo Char (30)  NULL  ,"
        'StrSQL &= "woSeq Char (30)  NULL  ,"
        StrSQL &= "woDetail Char (30)  NOT NULL  ,"
        StrSQL &= "partNo  Char (20)  NULL  ,"
        StrSQL &= "partSpec  Char (60)  NULL  ,"
        StrSQL &= "qty Decimal(16,6)  NULL  ,"
        StrSQL &= "manStd Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "mchStd Decimal(16,0)  DEFAULT 0  ,"
        'StrSQL &= "manTimeUsage Decimal(16,0)  DEFAULT 0  ,"
        'StrSQL &= "mchTimeUsage Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(docDetail)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempProductivitySummary(tempTable As String, ByVal fdate As Date, ByVal amtDate As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "wc  Char (10) NOT NULL ,"
        For i As Integer = 0 To amtDate
            Dim tdate As String = fdate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "mch" & tdate & " Decimal(10,0)  DEFAULT 0  ,"
            StrSQL &= "man" & tdate & " Decimal(10,0)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(wc));"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempPlanDelivery(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "delQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "delAmt Decimal(20,2)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "stockQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "stockAmt Decimal(20,2)  DEFAULT 0  ,"
        StrSQL &= "cust Char (30)  DEFAULT ''  ,"
        StrSQL &= "dateStr Char (10)  DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    'Sub CreateTempPeriodInventory(ByVal Temptable As String, ByVal fdate As Date, ByVal amtMonth As Integer)
    '    'and xtype='U'
    '    Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
    '    SelSQL = SelSQL & "Drop Table " & Temptable
    '    Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
    '    StrSQL &= "CREATE TABLE " & Temptable & " ("
    '    StrSQL &= "item  Char (10) NOT NULL ,"
    '    StrSQL &= "wh  Char (4)  NOT NULL ,"
    '    For i As Integer = 0 To amtMonth
    '        Dim tdate As String = fdate.AddMonths(i).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
    '        StrSQL &= "Q" & tdate & " Decimal(15,3)   DEFAULT 0   ,"
    '        StrSQL &= "C" & tdate & " Decimal(15,3)   DEFAULT 0   ,"
    '        StrSQL &= "A" & tdate & " Decimal(15,3)   DEFAULT 0   ,"
    '    Next
    '    'StrSQL &= "hour_sum Decimal(10,0)  NULL  ,"
    '    StrSQL &= "PRIMARY KEY(item,wh)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub
    Sub createTempPlanDelivery2(ByVal tempTable As String, ByVal fdate As Date, ByVal amtMonth As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        For i As Integer = 0 To amtMonth
            Dim tdate As String = fdate.AddMonths(i).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "D" & tdate & " Decimal(15,2)   DEFAULT 0   ,"
        Next
        StrSQL &= "moQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "stockQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "stockAmt Decimal(20,2)  DEFAULT 0  ,"
        StrSQL &= "cust Char (30)  DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    'Sub createTempSaleUndelivery(tempTable As String)
    '    Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
    '    SelSQL = SelSQL & "Drop Table " & tempTable
    '    Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
    '    StrSQL &= "CREATE TABLE " & tempTable & " ("
    '    StrSQL &= "item Char (20) NOT NULL  ,"
    '    StrSQL &= "SoTypeNo Char (30) NOT NULL  ,"
    '    StrSQL &= "delQty Decimal(16,0)  DEFAULT 0  ,"
    '    StrSQL &= "moQty Decimal(16,0)  DEFAULT 0  ,"
    '    StrSQL &= "poQty Decimal(16,0)  DEFAULT 0  ,"
    '    StrSQL &= "prQty Decimal(16,0)  DEFAULT 0  ,"
    '    StrSQL &= "issueQty Decimal(16,0)  DEFAULT 0  ,"
    '    StrSQL &= "stockQty Decimal(16,0)  DEFAULT 0  ,"
    '    StrSQL &= "PRIMARY KEY(item,SoTypeNo)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    Sub CreateTempPoNotCloseTable(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "OrderType  Char (10)  NULL  ,"
        StrSQL &= "OrderNo  Char (20)  NULL  ,"
        StrSQL &= "Item  Char (30)  NULL  ,"
        StrSQL &= "Desciption  Char (70)  NULL  ,"
        StrSQL &= "PurQty  Char (20)  NULL  ,"
        StrSQL &= "DelQty  Char (20)  NULL  ,"
        StrSQL &= "Unit  Char (10)  NULL  ,"
        StrSQL &= "DelDate  Char (20)  NULL  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPlanIssue(tempTable As String)
        Dim dbConn As New DataConnectControl
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "supportQty Decimal(16,3)  DEFAULT 0  ," 'wh=2400 only
        StrSQL &= "stockQty Decimal(16,3)  DEFAULT 0  ," 'stock not 2400 is 2210,2900,9999
        StrSQL &= "PRIMARY KEY(item)) ;"
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub
    Sub createTempCheckCodeStatus(tempTable As String, TypeCode As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "delQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "supportQty Decimal(16,3)  DEFAULT 0  ," 'wh=2400 only
        Select Case TypeCode
            Case "2"
                StrSQL &= "wh2101 Decimal(16,3)  DEFAULT 0  ," 'wh=2101
            Case "3"
                StrSQL &= "whQty Decimal(16,3)  DEFAULT 0  ," 'wh=3000+
            Case Else
                StrSQL &= "wh2201 Decimal(16,3)  DEFAULT 0  ," 'wh=2201
                StrSQL &= "wh3333 Decimal(16,3)  DEFAULT 0  ," 'wh=3333
        End Select
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createScrap(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "TA001 Char (10)  NOT NULL  ,"
        StrSQL &= "TA002 Char (30)  NULL  ,"
        StrSQL &= "TA024 Char (30)  NULL  ,"
        StrSQL &= "TA025 Char (30)  NULL  ,"
        StrSQL &= "TA026 Char (30)  NULL  ,"
        StrSQL &= "TA027 Char (30)  NULL  ,"
        StrSQL &= "TA003 Char (30)  NULL  ,"
        StrSQL &= "TA035 Char (70)  NULL  ,"
        StrSQL &= "TA006 Char (20)  NULL  ,"
        StrSQL &= "PRIMARY KEY(TA001)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempMOQ(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "sup  Char (10)  NOT NULL ,"
        StrSQL &= "code  Char (20) NOT NULL ,"
        StrSQL &= "poYear  Char (10) NOT NULL ,"
        StrSQL &= "MoqQty Char(10)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(15,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(sup,code,poYear)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateTempMoSummary(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "MoMonth  Char (10)  NOT NULL ,"
        StrSQL &= "MoTotal  Decimal(15,3)  DEFAULT 0 ,"
        StrSQL &= "MoFinish  Decimal(15,3)  DEFAULT 0 ,"
        StrSQL &= "MoManual  Decimal(15,3)  DEFAULT 0 ,"
        StrSQL &= "MoUnclose Decimal(15,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(MoMonth)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateTempMoDelay(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "MoOver  Char (10)  NOT NULL ,"
        StrSQL &= "MoTotal  Decimal(15,3)  DEFAULT 0 ,"
        StrSQL &= "MoDelay  Decimal(15,3)  DEFAULT 0 ,"
        StrSQL &= "PRIMARY KEY(MoOver)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempMaterialShortage(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "delQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "prQtyNot Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poManQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poForQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poMoQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "stockQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "saveQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "MoqQty Char(10)  DEFAULT 0  ,"
        StrSQL &= "callIn Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "confirmdate Char(10)  DEFAULT ''  ,"
        StrSQL &= "plandate Char(50)  DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub



    Sub createTempBomMaterialCallIn(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "issueQtyNot Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "prQtyNot Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "planQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "planQtyNot Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poInspQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "stockQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempOutsourc(tempTable As String) 'D204
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "TrnType Char (10) NOT NULL  ,"
        StrSQL &= "TrnNo Char (20) NOT NULL  ,"
        StrSQL &= "TrnSeq Char (5) NOT NULL  ,"
        StrSQL &= "MoType Char (10) DEFAULT ''  ,"
        StrSQL &= "MoNo Char (20) DEFAULT ''  ,"
        StrSQL &= "InvNo Char (250) DEFAULT ''  ,"
        StrSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "receivedQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "receivingQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "price Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "amount Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "refTrnDetail text  DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(TrnType,TrnNo,TrnSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempItemPrice(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "Item Char (20) NOT NULL  ,"
        StrSQL &= "Currency Char (20) DEFAULT ''  ,"
        StrSQL &= "price Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "effDate Char (10) DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(Item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempAccountStatus(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "docDetail Char (20) NOT NULL  ,"
        StrSQL &= "docDate Char (8) DEFAULT ''  ,"
        StrSQL &= "contactId Char (10) DEFAULT ''  ,"
        StrSQL &= "contactName Char (150) DEFAULT ''  ,"
        StrSQL &= "section Char (10) DEFAULT ''  ,"
        StrSQL &= "docBy Char (30) DEFAULT ''  ,"
        StrSQL &= "docRefer Char (255) DEFAULT ''  ,"
        StrSQL &= "exportAmount Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "exportAction Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "exportBalance Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "localAmount Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "localAction Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "localBalance Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(docDetail)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'Sub createTempAccountTaxPurchase(tempTable As String)
    '    Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
    '    SelSQL = SelSQL & "Drop Table " & tempTable
    '    Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
    '    StrSQL &= "CREATE TABLE " & tempTable & " ("
    '    StrSQL &= "docDetail Char (20) NOT NULL  ,"
    '    StrSQL &= "docDate Char (8) DEFAULT ''  ,"
    '    StrSQL &= "contactDetail Char (150) DEFAULT ''  ,"
    '    StrSQL &= "docRefer Char (255) DEFAULT ''  ,"
    '    StrSQL &= "localAmount Decimal(16,6)  DEFAULT 0  ,"
    '    StrSQL &= "localVat Decimal(16,6)  DEFAULT 0  ,"
    '    StrSQL &= "PRIMARY KEY(docDetail)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    Sub createTempAccountTaxPurchase(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "docDetail Char (20) NOT NULL  ,"
        StrSQL &= "docDate Char (8) DEFAULT ''  ,"
        StrSQL &= "contactDetail Char (150) DEFAULT ''  ,"
        StrSQL &= "docRefer Char (255) DEFAULT ''  ,"
        StrSQL &= "localAmount Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "localVat Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "tax Char (150) DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(docDetail)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempAccountOverDue(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "docDetail Char (20) NOT NULL  ,"
        StrSQL &= "docDate Char (8) DEFAULT ''  ,"
        StrSQL &= "dueDate Char (8) DEFAULT ''  ,"
        StrSQL &= "creditTerm Char (8) DEFAULT ''  ,"
        StrSQL &= "contactId Char (10) DEFAULT ''  ,"
        StrSQL &= "contactName Char (150) DEFAULT ''  ,"
        StrSQL &= "exportBalance Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "localBalance Decimal(16,6)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(docDetail)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempMatOverIssue(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "MO Char (25) NOT NULL  ,"
        StrSQL &= "docDate Char (8) DEFAULT ''  ,"
        StrSQL &= "item Char (20) DEFAULT ''  ,"
        StrSQL &= "spec Char (250) DEFAULT ''  ,"
        StrSQL &= "reqQty Decimal(16,4)  DEFAULT 0  ,"
        StrSQL &= "qpaQty Decimal(16,4)  DEFAULT 0  ,"
        StrSQL &= "issueDate Char (8) DEFAULT ''  ,"
        StrSQL &= "issueDept Char (20) DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(MO)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempActualSaleAmout(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "SaleType Char (10) NOT NULL  ,"
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "Price Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "SaleQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "ProductQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "InventoryQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(SaleType,item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempCostBOM(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "BomItem Char(250) NOT NULL  ,"
        StrSQL &= "ParentItem Char(20) DEFAULT ''  ,"
        StrSQL &= "SubItem Char (25) DEFAULT ''  ,"
        StrSQL &= "Operation Char (4) DEFAULT ''  ,"
        StrSQL &= "Property Char (1) DEFAULT ''  ,"
        StrSQL &= "QtyPerPcs Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "Unit Char (10) DEFAULT ''  ,"
        StrSQL &= "Currency Char (5) DEFAULT ''  ,"
        StrSQL &= "Supplier Char (10) DEFAULT ''  ,"
        StrSQL &= "Price Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "ExchangRate Decimal(16,5)  DEFAULT 1  ,"
        StrSQL &= "PRIMARY KEY(BomItem)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempStockSupply(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "prQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "wh4100 Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "wh4200 Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "wh2207 Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempBOMMaterialsList(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "BomItem Char (100) NOT NULL  ,"
        StrSQL &= "DocNo Char (20) DEFAULT ''  ,"
        StrSQL &= "ParentItem Char (20)DEFAULT ''  ,"
        StrSQL &= "MatItem Char (20) DEFAULT ''  ,"
        StrSQL &= "Property Char (1) DEFAULT ''  ,"
        StrSQL &= "orderQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "QtyPerPcs Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "Unit Char (10) DEFAULT ''  ,"
        StrSQL &= "Currency Char (5) DEFAULT ''  ,"
        StrSQL &= "Supplier Char (10) DEFAULT ''  ,"
        StrSQL &= "Price Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "ExchangRate Decimal(16,5)  DEFAULT 1  ,"
        StrSQL &= "PRIMARY KEY(BomItem)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempProductivityReport(tempTable As String, Optional grpDate As Boolean = True)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        Dim pk As String = "wc"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "wc Char(5) NOT NULL  ,"
        If grpDate Then
            StrSQL &= "docDate Char(20) NOT NULL  ,"
            pk = "wc,docDate"
        End If
        StrSQL &= "produceTime Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "machineTime Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "operationTime Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(" & pk & ")) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempAttendanceReport(tempTable As String, Optional grpDate As Boolean = True)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        Dim pk As String = "wc"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "wc Char(5) NOT NULL  ,"
        If grpDate Then
            StrSQL &= "docDate Char(20) NOT NULL  ,"
            pk = "wc,docDate"
        End If
        StrSQL &= "WorkOperator Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "AllOperator Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(" & pk & ")) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempMatInvAgingSummary(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "codeType Char (20) NOT NULL  ,"
        StrSQL &= "txtSeq Integer NOT NULL  ,"
        StrSQL &= "A030 Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "A090 Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "A180 Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "A270 Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "A365 Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "A366 Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "pr Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "po Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(codeType,txtSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempOrderToCash(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "saleType     Char (4) NOT NULL  ,"
        StrSQL &= "saleNo       Char (11) NOT NULL  ,"
        StrSQL &= "saleSeq      Char (4) NOT NULL  ,"
        StrSQL &= "work         Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "warehouse    Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "reject       Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(saleType,saleNo,saleSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempLeaveOperator(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "EmpId Char (6) NOT NULL  ,"
        StrSQL &= "wc Char(5) NOT NULL  ,"
        StrSQL &= "A integer  DEFAULT 0  ,"
        StrSQL &= "B integer  DEFAULT 0  ,"
        StrSQL &= "C1 integer  DEFAULT 0  ,"
        StrSQL &= "C2 integer  DEFAULT 0  ,"
        StrSQL &= "D integer  DEFAULT 0  ,"
        StrSQL &= "E integer  DEFAULT 0  ,"
        StrSQL &= "F integer  DEFAULT 0  ,"
        StrSQL &= "G integer  DEFAULT 0  ,"
        StrSQL &= "H integer  DEFAULT 0  ,"
        StrSQL &= "I integer  DEFAULT 0  ,"
        StrSQL &= "J integer  DEFAULT 0  ,"
        StrSQL &= "K integer  DEFAULT 0  ,"
        StrSQL &= "L integer  DEFAULT 0  ,"
        StrSQL &= "M integer  DEFAULT 0  ,"
        StrSQL &= "N integer  DEFAULT 0  ,"
        StrSQL &= "O integer  DEFAULT 0  ,"
        StrSQL &= "Q1 integer  DEFAULT 0  ,"
        StrSQL &= "Q2 integer  DEFAULT 0  ,"
        StrSQL &= "Q3 integer  DEFAULT 0  ,"
        StrSQL &= "P1 integer  DEFAULT 0  ,"
        StrSQL &= "P2 integer  DEFAULT 0  ,"
        StrSQL &= "P3 integer  DEFAULT 0  ,"
        StrSQL &= "XQ1 integer  DEFAULT 0  ,"
        StrSQL &= "XQ2 integer  DEFAULT 0  ,"
        StrSQL &= "XQ3 integer  DEFAULT 0  ,"
        StrSQL &= "XP1 integer  DEFAULT 0  ,"
        StrSQL &= "XP2 integer  DEFAULT 0  ,"
        StrSQL &= "XP3 integer  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(EmpId,wc)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempMO(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "TC001 Char(4) NOT NULL  ,"
        StrSQL &= "TC002 Char(11)  NOT NULL  ,"
        StrSQL &= "TC003 Char(4)  NOT NULL  ,"
        StrSQL &= "TA001 Char(4) DEFAULT ''  ,"
        StrSQL &= "TA002 Char(11) DEFAULT ''  ,"
        StrSQL &= "TA003 Char(4) DEFAULT ''  ,"
        StrSQL &= "TA008 Char(8) DEFAULT ''  ,"
        StrSQL &= "TB003 Char(8) DEFAULT ''  ,"
        StrSQL &= "TB014 Char(250) DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(TC001,TC002,TC003)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempMO2(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "TA001 Char(4) NOT NULL  ,"
        StrSQL &= "TA002 Char(11) NOT NULL  ,"
        StrSQL &= "TA003 Char(4) NOT NULL  ,"
        StrSQL &= "TC001 Char(4) DEFAULT ''  ,"
        StrSQL &= "TC002 Char(11)  DEFAULT ''  ,"
        StrSQL &= "TC003 Char(4)  DEFAULT ''  ,"
        StrSQL &= "TA008 Char(8) DEFAULT ''  ,"
        StrSQL &= "TB003 Char(8) DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(TA001,TA002,TA003)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPlanCallIn(tempTable As String, ByVal fdate As Date, ByVal amtDate As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "stockQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        For i As Integer = 0 To amtDate
            Dim tdate As String = fdate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "issueQty" & tdate & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(item));"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPlanSchedule(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "TA001 Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "TA002 Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "TA003 Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "TA004 Char(4) NULL DEFAULT ''," 'Operation
        StrSQL &= "TA006 Char(10) NULL DEFAULT ''," 'Work Center
        StrSQL &= "PlanDate Char(10) NULL DEFAULT ''," 'Plan Date
        StrSQL &= "ActualDate Char(10) NULL DEFAULT ''," 'Actual Date
        StrSQL &= "PlanQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "ActualQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "ScrapQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "UnAppQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "StdMan Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL &= "StdMch Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL &= "TranAcc Char(20) NULL DEFAULT '',"
        StrSQL &= "orderBy Char(2) NULL DEFAULT '9',"
        StrSQL &= "PRIMARY KEY(Id)) ;" 'TA001,TA002,TA003,TA004,TA006
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempMatNotIssue(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "prQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "stockQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempAccStockCard(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "wh Char (10) NOT NULL  ," 'wh = Warehouse code / Account code
        StrSQL &= "openQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "openAmt Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "qty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "price Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "amt Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item,wh)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempFGInventoryStatus(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= " item Char (20) NOT NULL  ,"
        StrSQL &= " delQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= " delAmt Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= " closeQty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= " changeQty Decimal(16,0)  DEFAULT 0  ,"
        'StrSQL &= " cust Char(10) NOT DEFAULT ''  ,"
        StrSQL &= " qtyA Decimal(16,0)  DEFAULT 0  ," '1-90
        StrSQL &= " amtA Decimal(16,3)  DEFAULT 0  ," '1-90
        StrSQL &= " qtyB Decimal(16,0)  DEFAULT 0  ," '91-180
        StrSQL &= " amtB Decimal(16,3)  DEFAULT 0  ," '91-180
        StrSQL &= " qtyC Decimal(16,0)  DEFAULT 0  ," '181-270
        StrSQL &= " amtC Decimal(16,3)  DEFAULT 0  ," '181-270
        StrSQL &= " qtyD Decimal(16,0)  DEFAULT 0  ," '270-360
        StrSQL &= " amtD Decimal(16,3)  DEFAULT 0  ," '270-360
        StrSQL &= " qtyE Decimal(16,0)  DEFAULT 0  ," '>360
        StrSQL &= " amtE Decimal(16,3)  DEFAULT 0  ," '>360
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPayableStatus(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "supplier Char (5) NOT NULL  ,"
        StrSQL &= "openPur Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "purchase Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "payment Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(supplier)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPayableHead(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "TA001 Char (4) NOT NULL  ," 'doc type
        StrSQL &= "TA002 Char (11) NOT NULL  ," 'doc no
        StrSQL &= "TA034 Char (8) DEFAULT ''  ," 'date
        StrSQL &= "TA014 Char (50) DEFAULT ''  ," 'Invoice No
        StrSQL &= "TA004 Char (5) DEFAULT ''  ," 'supplier code
        StrSQL &= "TA037 Decimal(16,6)  DEFAULT 0  ," 'amount all
        StrSQL &= "TA038 Decimal(16,6)  DEFAULT 0  ," 'vat all
        StrSQL &= "TA019 Char (8) DEFAULT ''  ," 'due date
        'StrSQL &= "TA021 Char (30) DEFAULT ''  ," 'po
        StrSQL &= "TA087 Char (1) DEFAULT ''  ," 'write off status
        StrSQL &= "PRIMARY KEY(TA001,TA002)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPayableBody(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "TB001 Char (4) NOT NULL  ," 'doc type
        StrSQL &= "TB002 Char (11) NOT NULL  ," 'doc no
        StrSQL &= "TB003 Char (4) NOT NULL  ," 'doc seq
        StrSQL &= "TB005 Char (4) DEFAULT ''  ," 'src type
        StrSQL &= "TB006 Char (11) DEFAULT ''  ," 'src no
        StrSQL &= "TB007 Char (4) DEFAULT ''  ," 'src seq
        StrSQL &= "TB017 Decimal(16,6)  DEFAULT 0  ," 'amout
        StrSQL &= "TB018 Decimal(16,6)  DEFAULT 0  ," 'vat
        StrSQL &= "TB019 Decimal(16,6)  DEFAULT 0  ," 'qty
        StrSQL &= "TB020 Decimal(16,6)  DEFAULT 0  ," 'prc
        StrSQL &= "TB037 Char (20) DEFAULT ''  ," 'item
        StrSQL &= "TB038 Char (60) DEFAULT ''  ," 'desc
        StrSQL &= "TB039 Char (60) DEFAULT ''  ," 'spec
        StrSQL &= "TB040 Char (4) DEFAULT ''  ," 'unit
        StrSQL &= "PRIMARY KEY(TB001,TB002,TB003)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempMatForcast(ByVal tempTable As String, ByVal fdate As Date, ByVal amtMonth As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "stockQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "stockMRBQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poConQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "planQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "supplier char(250)  DEFAULT ''  ,"
        StrSQL &= "leadtime Decimal(16,2)  DEFAULT 0  ,"
        For i As Integer = 0 To amtMonth
            Dim tdate As String = fdate.AddMonths(i).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "plan" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "issue" & tdate & " Decimal(16,2)  DEFAULT 0  ,"
            StrSQL &= "po" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "poRcp" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "poCon" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "pr" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
        Next
        'StrSQL &= "fgItem text DEFAULT ''  ,"
        'StrSQL &= "fgDesc text DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempBomMat(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        'StrSQL &= "itemFG Char (20) NOT NULL  ,"
        StrSQL &= "itemParent Char (20) NOT NULL  ,"
        StrSQL &= "itemMAT Char (20) NOT NULL  ,"
        StrSQL &= "qty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "scrapRatio Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(itemParent,itemMAT)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub



    Sub createtempBOMReview(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "itemFG Char (20) NOT NULL  ,"
        StrSQL &= "itemParent Char (20) NOT NULL  ,"
        StrSQL &= "itemMAT Char (20) NOT NULL  ,"
        StrSQL &= "qty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "scrapRatio Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(itemFG,itemParent,itemMAT)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempBomShow(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "itemFG Char (20) NOT NULL  ,"
        StrSQL &= "itemParent Char (20) NOT NULL  ,"
        StrSQL &= "itemMAT Char (20) NOT NULL  ,"
        StrSQL &= "qty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(itemFG,itemParent,itemMAT)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempReceiveableStatus(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "customer Char (5) NOT NULL  ,"
        StrSQL &= "openSale Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "sale Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "credit Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "receive Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(customer)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempMatReview(ByVal tempTable As String, ByVal listWeek As Hashtable)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "stockQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poConQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "planQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,2)  DEFAULT 0  ,"
        For Each weekName As String In listWeek.Keys
            StrSQL &= "po" & weekName & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "poCon" & weekName & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "plan" & weekName & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "issue" & weekName & " Decimal(16,2)   DEFAULT 0   ,"
        Next
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempProductStatus(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= " cust Char (10) NOT NULL  ,"
        StrSQL &= " item Char (20) NOT NULL  ,"
        StrSQL &= " moQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= " stockQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= " delQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= " undelQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(cust,item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempVoucherListSum(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= " accCode Char (10) NOT NULL  ,"
        StrSQL &= " typeCode Char (20) NOT NULL  ,"
        StrSQL &= " debitAmt Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= " creditAmt Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(accCode,typeCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempSaleOrderTest2(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "SaleType Char (4) NOT NULL  ,"
        StrSQL &= "SaleNo Char (20) NOT NULL  ,"
        StrSQL &= "SaleSeq Char (4) NOT NULL  ,"
        StrSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        'StrSQL &= "soQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(SaleType,SaleNo,SaleSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempWIPStatus(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "stockQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "MainBinQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "NoBinQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "UnknowQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempInvenStatusDetail(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "WH2101 Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "WH2600 Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "WH2301 Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "SOUndel Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "CusCode Char (30)  DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createTempSODeliveryDelay(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "CusCode Char (20) NOT NULL  ,"
        StrSQL &= "CusName Char (50) NOT NULL  ,"
        StrSQL &= "TotalItem integer  DEFAULT 0 , "
        StrSQL &= "to6 integer  DEFAULT 0 , "
        StrSQL &= "to14 integer  DEFAULT 0 , "
        StrSQL &= "to21 integer  DEFAULT 0 , "
        StrSQL &= "more21 integer  DEFAULT 0 , "
        StrSQL &= "PRIMARY KEY(CusCode,CusName)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempBudget(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "NonInv Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "Inv Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempSaleDeliveryStatusItem(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "SumInv Decimal(16,3)  DEFAULT 0  ,"
        'StrSQL &= "AVGStore Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempDeliveryPlanStatus(ByVal tempTable As String, ByVal fdate As Date, ByVal amtDate As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= " item Char (20) DEFAULT ''  ,"
        StrSQL &= " cust Char (10) DEFAULT ''  ,"
        StrSQL &= " fgQty integer  DEFAULT 0  ,"
        StrSQL &= " fgBal integer  DEFAULT 0  ,"
        StrSQL &= " moQty integer  DEFAULT 0  ,"
        StrSQL &= " delQty integer  DEFAULT 0  ,"
        For i As Integer = 0 To amtDate
            Dim tdate As String = fdate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "delQty" & tdate & " integer  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(item));"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempBom(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Nvarchar(255) NOT NULL  ,"
        StrSQL &= "itemParent Char (20) DEFAULT ''  ,"
        StrSQL &= "itemChild Char (20) DEFAULT '' ,"
        'StrSQL &= "qty Decimal(16,2)  DEFAULT 0  ,"
        'StrSQL &= "scrapRatio Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempBOMList(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "stockQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "soQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempAlertSaftyStock(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "prQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "piQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPlanScheduleAdd(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "moType Char (5) NOT NULL  ,"
        StrSQL &= "moNo Char (20) NOT NULL  ,"
        StrSQL &= "moSeq Char (5) NOT NULL  ,"
        StrSQL &= "workCenter Char (10) NOT NULL ,"
        StrSQL &= "operation Char (4) NOT NULL ,"
        StrSQL &= "PlanStartDate Char (8) DEFAULT '' ,"
        StrSQL &= "cust Char (10) DEFAULT '' ,"
        StrSQL &= "moQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "finQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "wipQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "balQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "stdMan Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "stdMch Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "waitTrnQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "planedQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "LastPlanDate Char (8) DEFAULT '' ,"
        StrSQL &= "orderBy Char (2) DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(moType,moNo,moSeq,workCenter,operation)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempWorkLoading(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "wc Char (10) NOT NULL  ,"
        StrSQL &= "item Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "mo Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "qty Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "std Decimal(16,0)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(wc)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempReworkOrScrap(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "doc Char (5) NOT NULL  ,"
        StrSQL &= "docno Char (20) NOT NULL  ,"
        StrSQL &= "docseq Char (5) NOT NULL  ,"
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "wc Char (5) DEFAULT '' ,"
        StrSQL &= "wcname Char (250) DEFAULT '' ,"
        StrSQL &= "docdate Char (10) DEFAULT '' ,"
        StrSQL &= "mo Char (5) DEFAULT ''  ,"
        StrSQL &= "mono Char (20) DEFAULT ''  ,"
        StrSQL &= "spec Char (250) DEFAULT '' ,"
        StrSQL &= "moQty integer  DEFAULT 0  ,"
        StrSQL &= "scrapQty integer  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(doc,docno,docseq,item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub createTempPlanScheduleSum(tempTable As String, ByVal fdate As Date, ByVal amtDate As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "wc Char (10) NOT NULL  ,"
        For i As Integer = 0 To amtDate
            Dim tdate As String = fdate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "plan" & tdate & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "actTran" & tdate & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "actPlan" & tdate & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(wc));"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPlanScheduleDetail(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        'StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "docDate char(10)  NOT NULL  ,"
        StrSQL &= "moType Char (5) NOT NULL  ,"
        StrSQL &= "moNo Char (20) NOT NULL  ,"
        StrSQL &= "moSeq Char (5) NOT NULL  ,"
        StrSQL &= "wc char(10)  DEFAULT ''  ,"
        StrSQL &= "plnQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "trnQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "trnDate char(10)  DEFAULT ''  ,"
        StrSQL &= "plnDate char(10)  DEFAULT ''  ,"
        StrSQL &= "actDate char(10)  DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(docDate,moType,moNo,moSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempSaleQuontation(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "soType Char (5) NOT NULL  ,"
        StrSQL &= "soNo Char (20) NOT NULL  ,"
        StrSQL &= "soSeq Char (5) NOT NULL  ,"
        'StrSQL &= "item Char (20) DEFAULT ''  ,"
        'StrSQL &= "saleQty Decimal(16,3)  DEFAULT 0  ,"
        'StrSQL &= "salePrice Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "quPrice Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "quDate char(10)  DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(soType,soNo,soSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempPlanScheduleSum2(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "wc Char (10) NOT NULL  ,"
        StrSQL &= "planDate Char (10) NOT NULL  ,"
        StrSQL &= "planItem Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "planCan Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "actTran Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "actPlan Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(wc,planDate));"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempWCGetMO(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "moType Char (5) NOT NULL  ,"
        StrSQL &= "moNo Char (20) NOT NULL  ,"
        StrSQL &= "wc char(10)  DEFAULT ''  ,"
        StrSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(moType,moNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempWCMatStatusSum(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) DEFAULT ''  ,"
        StrSQL &= "wc char(10)  DEFAULT ''  ,"
        StrSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "reqQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "notQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


    Sub createTempInventoryMovement(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "stockQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poConQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "planQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempMOCheckMatReturn(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "moType Char (5) NOT NULL  ,"
        StrSQL &= "moNo Char (20) NOT NULL  ,"
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "qpaQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "returnQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(moType,moNo,item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempFGCheck(tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "inQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "outQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "outQtyNext Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempMaterialsMovement(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        'StrSQL &= "itemDesc char(250)  DEFAULT ''  ,"
        'StrSQL &= "Spec char(250)  DEFAULT ''  ,"
        'StrSQL &= "Vender char(250)  DEFAULT ''  ,"
        StrSQL &= "MOSpec char(250)  DEFAULT ''  ,"
        StrSQL &= "ForSpec char(250)  DEFAULT ''  ,"
        StrSQL &= "SoSpec char(250)  DEFAULT ''  ,"
        StrSQL &= "soQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "planQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,5)  DEFAULT 0  ,"
        StrSQL &= "lastRcvDate char(10)  DEFAULT ''  ,"
        StrSQL &= "lastDisDate char(10)  DEFAULT ''  ,"
        StrSQL &= "stock Decimal(16,5)  DEFAULT 0  ," 'all stock qty
        StrSQL &= "stockA Decimal(16,5)  DEFAULT 0  ," '<30
        StrSQL &= "stockA1 Decimal(16,5)  DEFAULT 0  ," '31-90
        StrSQL &= "stockB Decimal(16,5)  DEFAULT 0  ," '91-180
        StrSQL &= "stockC Decimal(16,5)  DEFAULT 0  ," '181-270
        StrSQL &= "stockD Decimal(16,5)  DEFAULT 0  ," '271-360
        StrSQL &= "stockE Decimal(16,5)  DEFAULT 0  ," '>360
        StrSQL &= "stockF Decimal(16,5)  DEFAULT 0  ," '>720
        StrSQL &= "stockG Decimal(16,5)  DEFAULT 0  ," '>999
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempBusOT(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "BusCode  Char (10) NOT NULL  ,"
        StrSQL &= "BusFrom  integer NOT NULL  ,"
        StrSQL &= "DateofOT char(20) DEFAULT '',"
        StrSQL &= "Time04   Char (10) DEFAULT ''  ,"
        StrSQL &= "Time07   Char (10) DEFAULT ''  ,"
        StrSQL &= "Time16   Char (10) DEFAULT ''  ,"
        StrSQL &= "Time17   Char (10) DEFAULT ''  ,"
        StrSQL &= "Time19   Char (10) DEFAULT ''  ,"
        StrSQL &= "Time21   Char (10) DEFAULT ''  ,"
        StrSQL &= "TimeOther Char (10) DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(BusCode,BusFrom)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createTempSumReportOT(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Dept  Char (10) DEFAULT ''  ,"
        StrSQL &= "OTStartDate   Char (10) DEFAULT ''  ,"
        StrSQL &= "Person   Char (10) DEFAULT ''  ,"
        StrSQL &= "Work   Char (10) DEFAULT ''  ,"
        StrSQL &= "AbsenceAll   Char (10) DEFAULT ''  ,"
        StrSQL &= "Absence8   Char (10) DEFAULT ''  ,"
        StrSQL &= "Absence   Char (10) DEFAULT ''  ,"
        StrSQL &= "OTAll   Char (10) DEFAULT ''  ,"
        StrSQL &= "OTTotal   Char (10) DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add 2016-03-29 by Gift
    Sub createTempPRMOQ(ByVal tempTable As String, ByVal count As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "supplier Char (10) NOT NULL  ,"
        StrSQL &= "expDate char(10)  DEFAULT ''  ,"
        StrSQL &= "sumQty Decimal(16,3)  DEFAULT 0  ,"
        For i As Integer = 0 To count
            StrSQL &= "qtyMOQ" & (i) & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "priceMOQ" & (i) & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(Id,supplier)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add 2016-03-29 by Gift
    Sub createTempPRMOQShow(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        Conn_SQL.Exec_Sql(SelSQL, Conn_SQL.MIS_ConnectionString)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "supplier Char (10) NOT NULL  ,"
        StrSQL &= "expDate char(10)  DEFAULT ''  ,"
        StrSQL &= "qtyMOQ Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "priceMOQ Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "amountMOQ Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "sumQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "price Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "amount Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add 2015-07-28 by noi
    Sub createTempMOPLAN(ByVal tempTable As String, ByVal dateList As ArrayList)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "moType Char (5) NOT NULL  ,"
        StrSQL &= "moNo Char (20) NOT NULL  ,"
        StrSQL &= "moSeq varchar(4) NOT NULL ,"
        For Each dd As String In dateList
            StrSQL &= "plan" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            'StrSQL &= "bal" & dd & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(moType,moNo,moSeq)) ;"
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'add 2015-12-02 by noi
    Sub createTempLotMat(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL &= "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "moType Char (5) NOT NULL  ,"
        StrSQL &= "moNo Char (20) NOT NULL  ,"
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "matQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(moType,moNo,item)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub


    'add 2016-01-29 by noi
    Sub createTempMOPLAN2(ByVal tempTable As String, ByVal fdate As String, ByVal tdate As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "moType Char (5) NOT NULL  ,"
        StrSQL &= "moNo Char (20) NOT NULL  ,"
        StrSQL &= "moSeq varchar(4) NOT NULL ,"
        StrSQL &= "remark varchar(250) DEFAULT ''  ,"

        Dim beginDate As Date = DateTime.ParseExact(fdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(tdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim amtDay As Short = DateDiff(DateInterval.Day, beginDate, lastDate)
        For i As Integer = 0 To amtDay
            Dim dd As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "plan" & dd & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(moType,moNo,moSeq)) ;"
        ' Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'add 2016-02-20 by noi
    Sub createTempWorkCenterPlan(ByVal tempTable As String, ByVal fdate As String, ByVal tdate As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "wc Char (5) NOT NULL  ,"

        Dim beginDate As Date = DateTime.ParseExact(fdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(tdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim amtDay As Short = DateDiff(DateInterval.Day, beginDate, lastDate)
        For i As Integer = 0 To amtDay
            Dim dd As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "plan" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "planActual" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "planTimeMan" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "planTimeMch" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "planTimeAP100" & dd & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(wc)) ;"
        ' Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'add 2017-05-08 by noi
    Sub createTempWorkCenterPlanProcess(ByVal tempTable As String, ByVal fdate As String, ByVal tdate As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "wc Char (5) NOT NULL  ,"
        StrSQL &= "operation Char (5) NOT NULL  ,"

        Dim beginDate As Date = DateTime.ParseExact(fdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(tdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim amtDay As Short = DateDiff(DateInterval.Day, beginDate, lastDate)
        For i As Integer = 0 To amtDay
            Dim dd As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "plan" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "planActual" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "planTimeMan" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "planTimeMch" & dd & " Decimal(16,3)  DEFAULT 0  ,"
            StrSQL &= "planTimeAP100" & dd & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(wc,operation)) ;"
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'add 2015-08-17 by noi
    Sub createTempWIPNOTCLOSED(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL &= "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "moType Char (5) NOT NULL  ,"
        StrSQL &= "moNo Char (20) NOT NULL  ,"
        StrSQL &= "moSeq varchar(4) NOT NULL ,"
        StrSQL &= "wip Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(moType,moNo,moSeq)) ;"
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'add 2015-08-17 by noi
    Sub createTempWIPNOTCLOSEDSUM(ByVal tempTable As String, ByVal monthList As ArrayList)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL &= "Drop Table " & tempTable
        dbConn.TransactionSQL(SelSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "moType Char (5) NOT NULL  ,"
        For Each mm As String In monthList
            StrSQL &= "C" & mm & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(moType)) ;"
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub
End Class
