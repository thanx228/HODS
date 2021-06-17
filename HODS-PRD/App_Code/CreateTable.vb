Imports MIS_HTI.DataControl
Public Class CreateTable
    Dim Conn_SQL As New ConnSQL
    Dim dbConn As New DataConnectControl
    Sub CreateMenuTable()
        'and xtype='U'
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='Menu' )"
        StrSQL &= "CREATE TABLE Menu ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "ParentId  Integer NOT NULL  ,"
        StrSQL &= "Line  Integer NOT NULL  ,"
        StrSQL &= "Name  Char (50)  NULL ,"
        StrSQL &= "Prog  Char (100)  NULL,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateUserGroupTable()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='UserGroup' )"
        StrSQL &= "CREATE TABLE UserGroup ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "UserGroup Char(20) NOT NULL ,"
        StrSQL &= "IdMenu  Char (10)  NOT NULL ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub
    Sub CreateUserInfoTable()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='UserInfo' )"
        StrSQL &= "CREATE TABLE UserInfo ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "UserName Char(10) NOT NULL ,"
        StrSQL &= "UserPassWord  Char (20) NOT NULL ,"
        StrSQL &= "UserGroup Char(20) NOT NULL ,"
        StrSQL &= "NameSurname  Char (50)  NULL ,"
        StrSQL &= "Dept  Char (50)  NULL ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateMachineCapacityTable()
        Dim tableName As String = "MachineCapacity"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL &= "CREATE TABLE " & tableName & " ("
        StrSQL &= "wc Char(10) NOT NULL,"
        StrSQL &= "capacity Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL &= "mancapacity Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL &= "PRIMARY KEY(wc)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub


    Sub CreateProcessCostTable()
        Dim tableName As String = "ProcessCost"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL &= "CREATE TABLE " & tableName & " ("
        StrSQL &= "wc Char(10) NOT NULL,"
        StrSQL &= "wcName Char(50) NULL DEFAULT '',"
        StrSQL &= "DLCost Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL &= "MachineCost Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL &= "PRIMARY KEY(wc)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateMoUrgentTable()
        Dim tableName As String = "MoUrgent"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL &= "CREATE TABLE " & tableName & " ("
        StrSQL &= "TA001 Char(4) NOT NULL," 'MO TYPE
        StrSQL &= "TA002 Char(20) NOT NULL," 'MO NO
        StrSQL &= "PRIMARY KEY(TA001,TA002)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreatePlanScheduleTable()
        Dim tableName As String = "PlanSchedule"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL &= "CREATE TABLE " & tableName & " ("
        StrSQL &= "PlanDate Char(8) NOT NULL,"
        StrSQL &= "WorkCenter Char(10) NOT NULL," 'Work Center
        StrSQL &= "PlanSeq Integer NOT NULL," 'plan seq
        StrSQL &= "PlanSeqSet Char(4) NULL DEFAULT '',"
        StrSQL &= "MoType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "MoNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "MoSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "ProcessCode Char(4) NULL DEFAULT ''," 'Operation
        StrSQL &= "PlanedQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "PlanQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "Urgent Char(1) NULL DEFAULT '0'," 'Urgent
        StrSQL &= "PlanStatus Char(5) NULL DEFAULT '0'," 'P=Plam and CXX=cause to cancle
        StrSQL &= "TransferNo Char(40) DEFAULT ''," 'Transfer No
        StrSQL &= "PlanTimeStd Integer DEFAULT(0) ," 'ap 100 time or time to produce 'second
        StrSQL &= "PlanTime Integer DEFAULT(0) ," 'ap 100 time or time to produce 'second
        StrSQL &= "PlanNote Char(40) DEFAULT ''," 'Transfer No
        StrSQL &= "CreateBy  Char (10)  NULL  DEFAULT '' ,"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy  Char (10)  NULL  DEFAULT '' ,"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "CancledBy  Char (10)  NULL  DEFAULT '' ,"
        StrSQL &= "CancledDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(PlanDate,WorkCenter,PlanSeq)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateMoBalanceTable()
        Dim tableName As String = "MoBalance"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL &= "CREATE TABLE " & tableName & " ("
        StrSQL &= "TA001 Char(4) NOT NULL," 'MO type
        StrSQL &= "TA002 Char(20) NOT NULL," 'MO No
        StrSQL &= "TA003 Char(4) NOT NULL," 'MO seq
        StrSQL &= "TA004 Char(4) NOT NULL," 'Operation
        StrSQL &= "TA006 Char(10) NOT NULL," 'Work Center
        StrSQL &= "PlanedQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "PRIMARY KEY(TA001,TA002,TA003,TA004,TA006)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateLogHistoryTable()
        Dim table As String = "LogHistory"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "UserId Char(20) NOT NULL ,"
        StrSQL &= "MenuId  Char (10)  NOT NULL ,"
        StrSQL &= "ComName Char(30) DEFAULT '' ,"
        StrSQL &= "IpAddr Char(20) DEFAULT '' ,"
        StrSQL &= "InDateTime Char(25) DEFAULT '',"
        StrSQL &= "outDateTime Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        ' Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub
    Sub CreateLogInHistoryTable()
        Dim table As String = "LogInHistory"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "UserId Char(20) DEFAULT '' ,"
        StrSQL &= "ComName Char(30) DEFAULT '' ,"
        StrSQL &= "IpAddr Char(20) DEFAULT '' ,"
        StrSQL &= "LogInDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateItemFollowLotTable()
        Dim table As String = "ItemFollowLot"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "item Char(20) DEFAULT '' ,"
        StrSQL &= "dateStart Char(30) DEFAULT '' ,"
        StrSQL &= "LotCheck Integer DEFAULT 0   ,"
        StrSQL &= "status Char(2) DEFAULT '10' ,"
        StrSQL &= "CreateBy Char(25) DEFAULT '',"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy Char(25) DEFAULT '',"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'Sub CreateItemTable()
    '    Dim table As String = "Item"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &= "CREATE TABLE " & table & " ("
    '    StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
    '    StrSQL &= "item Char(20) DEFAULT '' ,"
    '    StrSQL &= "Wgh Decimal(16,5) NULL DEFAULT(0) ,"
    '    StrSQL &= "CreateBy Char(25) DEFAULT '',"
    '    StrSQL &= "CreateDate Char(25) DEFAULT '',"
    '    StrSQL &= "ChangeBy Char(25) DEFAULT '',"
    '    StrSQL &= "ChangeDate Char(25) DEFAULT '',"
    '    StrSQL &= "PRIMARY KEY(Id)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    Sub CreateBoxTable()
        Dim table As String = "Box"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "boxDesc Char(250) DEFAULT '',"
        StrSQL &= "boxSpec Char(25) DEFAULT '',"
        StrSQL &= "boxWgh Decimal(16,3) NULL DEFAULT(0) ,"
        StrSQL &= "CreateBy Char(25) DEFAULT '',"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy Char(25) DEFAULT '',"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateItemBoxTable()
        Dim table As String = "ItemBox"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "IdItem Integer NOT NULL  ,"
        StrSQL &= "IdBox Integer NOT NULL  ,"
        StrSQL &= "QPB Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "selItem Char(1) DEFAULT 'N'," 'say yes='Y',no='N'
        StrSQL &= "CreateBy Char(25) DEFAULT '',"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy Char(25) DEFAULT '',"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(IdItem,IdBox)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateInvHead()
        Dim table As String = "InvHead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "InvType Char(4) NOT NULL,"
        StrSQL &= "InvNumber Char(20) NOT NULL,"
        StrSQL &= "InvDate Char(8) DEFAULT '',"
        StrSQL &= "InvCust Char(15) DEFAULT '',"
        StrSQL &= "InvCustName1 Char(250) DEFAULT '',"
        StrSQL &= "InvCustName2 Char(250) DEFAULT '',"
        StrSQL &= "InvCustAdd1 text DEFAULT '',"
        StrSQL &= "InvCustAdd2 text DEFAULT '',"
        StrSQL &= "InvCurrency Char(5) DEFAULT '',"
        StrSQL &= "InvContent Char(150) DEFAULT '',"
        StrSQL &= "InvShiperPer Char(150) DEFAULT '',"
        StrSQL &= "InvAirWayBill Char(150) DEFAULT '',"
        StrSQL &= "InvFrom Char(150) DEFAULT '',"
        StrSQL &= "InvSailingOn Char(150) DEFAULT '',"
        StrSQL &= "InvAttn Char(150) DEFAULT '',"
        StrSQL &= "InvTel Char(150) DEFAULT '',"
        StrSQL &= "InvType2 Char(150) DEFAULT '',"
        StrSQL &= "InvType3 Char(150) DEFAULT '',"
        StrSQL &= "InvQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "InvPrc Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "InvAmt Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "InvNetWgh Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "InvGrsWgh Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "InvCarton int NULL DEFAULT(0) ,"
        StrSQL &= "CreateBy Char(25) DEFAULT '',"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy Char(25) DEFAULT '',"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "FileNamePic Char(50) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(InvType,InvNumber)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateInvBody()
        Dim table As String = "InvBody"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "InvType Char(4) DEFAULT '',"
        StrSQL &= "InvNumber Char(20) DEFAULT '',"
        StrSQL &= "InvOrType Char(4) NOT NULL,"
        StrSQL &= "InvOrNumber Char(20) NOT NULL,"
        StrSQL &= "InvOrSeq Char(4) NOT NULL,"
        StrSQL &= "ShipType Char(4) DEFAULT '',"
        StrSQL &= "ShipNumber Char(20) DEFAULT '',"
        StrSQL &= "ShipSeq Char(4) DEFAULT '',"
        StrSQL &= "InvItemId Char(20) DEFAULT '',"
        StrSQL &= "InvItemDesc Char(150) DEFAULT '',"
        StrSQL &= "InvCustPO Char(50) DEFAULT '',"
        StrSQL &= "InvQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "InvUnit Char(10) DEFAULT '',"
        StrSQL &= "InvPrc Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "InvAmt Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "InvBoxId Integer DEFAULT '0' ,"
        StrSQL &= "CreateBy Char(25) DEFAULT '',"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy Char(25) DEFAULT '',"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(InvOrType,InvOrNumber,InvOrSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateInvPackingList()
        Dim table As String = "InvPackingList"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "InvType Char(4) NOT NULL,"
        StrSQL &= "InvNumber Char(20) NOT NULL,"
        StrSQL &= "InvSeq Integer NOT NULL  ,"
        StrSQL &= "CartonType Char(1) NOT NULL,"
        StrSQL &= "PalNumber Char(10) DEFAULT '',"
        StrSQL &= "InvQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "CartonCnt Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL &= "CreateBy Char(25) DEFAULT '',"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(InvType,InvNumber,InvSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateNCTSpecial()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='NCTSpecial' )"
        StrSQL &= "CREATE TABLE NCTSpecial ("
        StrSQL &= "Item varchar (10) NOT NULL ,"
        StrSQL &= "AP_ID varchar (50)  DEFAULT '' ,"
        StrSQL &= "Dim1 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Dim2 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Dim3 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Dim4 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Area Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Area_Acc Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "FileNamePic  Char (50)  DEFAULT '' ,"
        StrSQL &= "FileNameCAD  Char (50)  DEFAULT '' ,"
        StrSQL &= "CreateBy  Char (10)  DEFAULT '' ,"
        StrSQL &= "ChangeBy  Char (10)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(Item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateNCTSpecialNew()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='NCTSpecial' )"
        StrSQL &= "CREATE TABLE NCTSpecial ("
        StrSQL &= "Item varchar (10) NOT NULL ,"
        StrSQL &= "AP_ID varchar (50)  DEFAULT '' ,"
        StrSQL &= "Type  Char (50)  DEFAULT '' ,"
        StrSQL &= "SPType  Char (50)  DEFAULT '' ,"
        StrSQL &= "Dim1 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Dim2 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Dim3 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Dim4 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Area Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "Area_Acc Decimal (15,3)  DEFAULT 0 ,"
        StrSQL &= "FileNamePic  Char (50)  DEFAULT '' ,"
        StrSQL &= "FileNameCAD  Char (50)  DEFAULT '' ,"
        StrSQL &= "CreateBy  Char (10)  DEFAULT '' ,"
        StrSQL &= "ChangeBy  Char (10)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(Item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBillPurHead()
        'BillNo,SupID,SupName,Address1,Address2,Date,Payment,BillShow,CreateBy,Remark,EditBy
        Dim table As String = "BillPurHead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "BillNo varchar (10) NOT NULL ,"
        StrSQL &= "SupID varchar (10)  DEFAULT '' ,"
        StrSQL &= "SupName varchar (250)  DEFAULT '' ,"
        StrSQL &= "Address1 varchar (250)  DEFAULT '' ,"
        StrSQL &= "Address2 varchar (250)  DEFAULT '' ,"
        StrSQL &= "Date varchar (20)  DEFAULT '' ,"
        StrSQL &= "Payment varchar (20)  DEFAULT '' ,"
        StrSQL &= "BillShow varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "Remark varchar (250)  DEFAULT '' ,"
        StrSQL &= "EditBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(BillNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBillPurLine()
        Dim table As String = "BillPurLine"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "BillNo varchar (10) NOT NULL ,"
        StrSQL &= "InvoiceH varchar (5)  NOT NULL ,"
        StrSQL &= "InvoiceNo varchar (20)  NOT NULL ,"
        StrSQL &= "SupID varchar (10)  DEFAULT '' ,"
        StrSQL &= "OrderDate varchar (20)  DEFAULT '' ,"
        StrSQL &= "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL &= "Amount Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "Tax Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "Balance Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "AmountBalance Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "Paid Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "ShowInvoice varchar (150)  DEFAULT '' ,"
        StrSQL &= "AmountText nvarchar (250)  DEFAULT '' ,"
        StrSQL &= "RemarkInvoice varchar (250)  DEFAULT '' ,"
        StrSQL &= "TypeNo varchar (50)  DEFAULT '' ,"
        StrSQL &= "OrderType varchar (10)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(ID)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    'BillPurMonth(BillShow,SupID,Payment,Date,AmountBalance,DueDate,CreateBy)
    Sub CreateBillPurMonth()
        Dim table As String = "BillPurMonth"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "BillShow varchar (20)  NOT NULL ,"
        StrSQL &= "SupID varchar (10)  DEFAULT '' ,"
        StrSQL &= "Payment varchar (20)  DEFAULT '' ,"
        StrSQL &= "Date varchar (20)  DEFAULT '' ,"
        StrSQL &= "AmountBalance Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(BillShow)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateItemCycle()
        Dim table As String = "ItemCycle"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "RunNo varchar (20)  NOT NULL ,"
        StrSQL &= "WName varchar (50)  NOT NULL ,"
        StrSQL &= "Item varchar (20)  DEFAULT '' ,"
        StrSQL &= "[Desc] varchar (250)  DEFAULT '' ,"
        StrSQL &= "Spec varchar (250)  DEFAULT '' ,"
        StrSQL &= "Wid varchar (20)  DEFAULT '' ,"
        StrSQL &= "Qty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "Unit varchar (20)  DEFAULT '' ,"
        StrSQL &= "NoShow varchar (50)  DEFAULT '' ,"
        StrSQL &= "UserID varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar (20)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(RunNo,WName)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateReportCheq()
        Dim table As String = "ReportCheq"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "PayType varchar (10)  NOT NULL ,"
        StrSQL &= "PayNo varchar (20)  NOT NULL ,"
        StrSQL &= "SuppNo varchar (20)  DEFAULT '' ,"
        StrSQL &= "SupName varchar (250)  DEFAULT '' ,"
        StrSQL &= "CheqNo varchar (50)  DEFAULT '' ,"
        StrSQL &= "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL &= "CheqAmout Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "PRIMARY KEY(PayType,PayNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBillTotal()
        Dim table As String = "BillTotal"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "BillNo varchar (10)  NOT NULL ,"
        StrSQL &= "Date varchar (20)  DEFAULT '' ,"
        StrSQL &= "Cid varchar (20)  DEFAULT '' ,"
        StrSQL &= "CName varchar (250)  DEFAULT '' ,"
        StrSQL &= "Address varchar (250)  DEFAULT '' ,"
        StrSQL &= "Invoice varchar (150)  DEFAULT '' ,"
        StrSQL &= "Amount Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "Payment Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "Balance Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "AmountText nvarchar (250)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(BillNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'select * from Billhead H left join BillLine L on (H.BillNo = L.BillNo) where H.BillNo = {?BillNo} order by InvoiceH desc,InvoiceNo 

    Sub CreateBillhead()
        ' BillNo,CustID,CustName,Address1,Address2,Date,Payment,BillShow,CreateBy,BillBy,BeDate
        Dim table As String = "Billhead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "BillNo varchar (10) NOT NULL ,"
        StrSQL &= "CustID varchar (10)  DEFAULT '' ,"
        StrSQL &= "CustName varchar (250)  DEFAULT '' ,"
        StrSQL &= "Address1 varchar (250)  DEFAULT '' ,"
        StrSQL &= "Address2 varchar (250)  DEFAULT '' ,"
        StrSQL &= "Date varchar (20)  DEFAULT '' ,"
        StrSQL &= "Payment varchar (50)  DEFAULT '' ,"
        StrSQL &= "BillShow varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "BillBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "EditBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "BeDate varchar (20)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(BillNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBillLine()
        Dim table As String = "BillLine"
        'BillNo-1,InvoiceH-1,InvoiceNo-1,CustID-1,OrderDate-1,DueDate-1,Amount-1,Balance*-1,ShowAmount,ShowBalance,ShowPaid,Paid
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "BillNo varchar (10) NOT NULL ,"
        StrSQL &= "InvoiceH varchar (5)  NOT NULL ,"
        StrSQL &= "InvoiceNo varchar (20)  NOT NULL ,"
        StrSQL &= "CustID varchar (10)  DEFAULT '' ,"
        StrSQL &= "OrderDate varchar (20)  DEFAULT '' ,"
        StrSQL &= "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL &= "Amount Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "ShowAmount nvarchar (250)  DEFAULT '' ,"
        StrSQL &= "Balance Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "ShowBalance nvarchar (250)  DEFAULT '' ,"
        StrSQL &= "Paid Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "ShowPaid varchar (150)  DEFAULT '' ,"
        StrSQL &= "AmountBalance Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "AmountText nvarchar (250)  DEFAULT '' ,"
        StrSQL &= "CustPO varchar (50)  DEFAULT '' ,"
        StrSQL &= "Remark varchar (250)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(ID)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateControlStore()
        Dim table As String = "ControlStore"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "TA001 char(5)  NOT NULL ,"
        StrSQL &= "TA002 char(20) NOT NULL ,"
        StrSQL &= "TA004 char(10)  DEFAULT '' ,"
        StrSQL &= "TA038 char(150)  DEFAULT '' ,"
        StrSQL &= "BillNo char(10) DEFAULT '',"
        StrSQL &= "StoreInvoice char(5) DEFAULT '',"
        StrSQL &= "StoreInDate char (20)  DEFAULT '' ,"
        StrSQL &= "StoreDO char(5) DEFAULT '',"
        StrSQL &= "StoreDoDate char(20)  DEFAULT '' ,"
        StrSQL &= "StoreBy char(20)  DEFAULT '' ,"
        StrSQL &= "StoreDate char(20)  DEFAULT '' ,"
        StrSQL &= "Status char(10) DEFAULT '',"
        StrSQL &= "RemarkStore varchar (250)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(TA001,TA002)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateControlSales()
        Dim table As String = "ControlSales"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "TA001 char(5)  NOT NULL ,"
        StrSQL &= "TA002 char(20) NOT NULL ,"
        StrSQL &= "TA004 char(10)  DEFAULT '' ,"
        StrSQL &= "TA038 char(150)  DEFAULT '' ,"
        StrSQL &= "BillNo char(10) DEFAULT '',"
        StrSQL &= "SalesReInDate char (20)  DEFAULT '' ,"
        StrSQL &= "SalesReDoDate char(20)  DEFAULT '' ,"
        StrSQL &= "SalesBy char(20)  DEFAULT '' ,"
        StrSQL &= "SalesDate char(20)  DEFAULT '' ,"
        StrSQL &= "Status char(10) DEFAULT '',"
        StrSQL &= "RemarkSales varchar (250)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(TA001,TA002)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateUserPlanAuthority()
        Dim table As String = "UserPlanAuthority"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Id Integer NOT NULL  ,"
        StrSQL &= "WC  Char (250)  NULL ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateSalesInvoice()
        Dim table As String = "SalesInvoice"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Invoice Char (100) NOT NULL ,"
        StrSQL &= "TA038  Char (250)  DEFAULT '' ,"
        StrSQL &= "TA004 Char (250) DEFAULT ''  ,"
        StrSQL &= "MA002  Char (250)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(Invoice)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateWorkRecordHead()
        Dim table As String = "WorkRecordHead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo char(20)  NOT NULL ,"
        StrSQL &= "docDate char (20)  DEFAULT '' ,"
        StrSQL &= "wc char(10) DEFAULT '',"
        StrSQL &= "mchCode char(30) DEFAULT '',"
        StrSQL &= "shift char(1) DEFAULT 'D',"
        StrSQL &= "jobType char(1) DEFAULT '',"
        StrSQL &= "LeadCode char(10) DEFAULT '',"
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "manPower integer DEFAULT 0,"
        StrSQL &= "sDateStart char(8) DEFAULT '',"
        StrSQL &= "sTimeStart char(5) DEFAULT '',"
        StrSQL &= "sDateEnd char(8) DEFAULT '',"
        StrSQL &= "sTimeEnd char(5) DEFAULT '',"
        StrSQL &= "rDateStart char(8) DEFAULT '',"
        StrSQL &= "rTimeStart char(5) DEFAULT '',"
        StrSQL &= "rDateEnd char(8) DEFAULT '',"
        StrSQL &= "rTimeEnd char(5) DEFAULT '',"
        StrSQL &= "setTime integer DEFAULT 0,"
        StrSQL &= "workTime integer DEFAULT 0,"
        StrSQL &= "LossTime integer DEFAULT 0,"
        StrSQL &= "remark nvarchar (250)  DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateWorkRecord()
        Dim table As String = "WorkRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo char(20)  NOT NULL ,"
        StrSQL &= "docSeq integer  NOT NULL ,"
        StrSQL &= "moType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "moNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "moSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "inputQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "scrapQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "returnQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "scrapCode char(10) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo,docSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateManfOrderRecord()
        Dim table As String = "ManfOrderRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "moType Char(4) NOT NULL," 'MO type
        StrSQL &= "moNo Char(20) NOT NULL," 'MO No
        StrSQL &= "moSeq Char(4) NOT NULL," 'MO seq
        StrSQL &= "inputQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "scrapQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "returnQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "PRIMARY KEY(moType,moNo,moSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateWorkRecordLoss()
        Dim table As String = "WorkRecordLoss"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo char(20)  NOT NULL ,"
        StrSQL &= "docSeq integer  NOT NULL ,"
        StrSQL &= "lossCode char(10) DEFAULT '',"
        StrSQL &= "lDateStart char(8) DEFAULT '',"
        StrSQL &= "lTimeStart char(5) DEFAULT '',"
        StrSQL &= "lDateEnd char(8) DEFAULT '',"
        StrSQL &= "lTimeEnd char(5) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo,docSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateCodeInfo()
        Dim table As String = "CodeInfo"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "CodeType char(20)  NOT NULL ,"
        StrSQL &= "Code char(20)  NOT NULL ,"
        StrSQL &= "Name nvarchar (250)  DEFAULT '' ,"
        StrSQL &= "WC char (30)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(CodeType,Code)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateUserDailyAuthority()
        Dim table As String = "UserDailyAuthority"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Id Integer NOT NULL  ,"
        StrSQL &= "WC  Char (250)  NULL ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateWorkRecordOper()
        Dim table As String = "WorkRecordOper"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo char(20)  NOT NULL ,"
        StrSQL &= "docSeq integer  NOT NULL ,"
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo,docSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateFGLabel()
        Dim table As String = "FGLabel"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "DocNo char(20) NOT NULL,"
        StrSQL &= "moType Char(4) NOT NULL," 'MO Receipt type
        StrSQL &= "moNo Char(20) NOT NULL," 'MO Receipt No
        StrSQL &= "moSeq Char(4) NOT NULL," 'MO Receipt seq
        StrSQL &= "qty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "qtyCtn Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "CtnNo Char(250) DEFAULT '' ,"
        StrSQL &= "CtnSpec Char(250) DEFAULT '' ,"
        StrSQL &= "CtnWgh Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "PackBy varchar (200)  DEFAULT '' ,"
        StrSQL &= "custPO Char(150) DEFAULT '' ,"
        StrSQL &= "remark Char(250) DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(DocNo,moType,moNo,moSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateSOConfirmDate()
        Dim table As String = "SOConfirmDate"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "DocNo char(20) NOT NULL,"
        StrSQL &= "DocType Char(4) NOT NULL,"
        StrSQL &= "soType Char(4) NOT NULL,"
        StrSQL &= "soNo Char(20) NOT NULL,"
        StrSQL &= "soSeq Char(4) NOT NULL,"
        StrSQL &= "Item varchar (20)  DEFAULT '' ,"
        StrSQL &= "Spec varchar (250)  DEFAULT '' ,"
        StrSQL &= "qty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "PlanDelDate Char(25) DEFAULT '',"
        StrSQL &= "SOReqDate Char(25) DEFAULT '',"
        StrSQL &= "PURRemark varchar(MAX) DEFAULT '',"
        StrSQL &= "PURConf Char(25) DEFAULT '',"
        StrSQL &= "PCConf Char(25) DEFAULT '',"
        StrSQL &= "SaleConf Char(25) DEFAULT '',"
        StrSQL &= "PURConf1 Char(25) DEFAULT '',"
        StrSQL &= "PCConf1 Char(25) DEFAULT '',"
        StrSQL &= "SaleConf1 Char(25) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(DocNo,soType,soNo,soSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateProductionProcessRecord()
        Dim table As String = "ProductionProcessRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "moType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "moNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "moSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "wc char(10) DEFAULT '',"
        StrSQL &= "mc char(30) DEFAULT '',"
        StrSQL &= "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectCode char(10) DEFAULT '',"
        StrSQL &= "scrapQty Decimal(16,2) DEFAULT(0),"
        StrSQL &= "scrapCode char(10) DEFAULT '',"
        StrSQL &= "dateCode Char (10)  DEFAULT '' ,"
        StrSQL &= "timeCode Char (10)  DEFAULT '' ,"
        StrSQL &= "breakTime char(1) DEFAULT 'N',"
        StrSQL &= "isSetTime char(2) DEFAULT '',"
        StrSQL &= "isMulti char(1) DEFAULT 'N',"
        StrSQL &= "tranNo char(40) DEFAULT '',"
        StrSQL &= "shift char(40) DEFAULT 'D',"
        StrSQL &= "isTeam char(1) DEFAULT '',"
        StrSQL &= "processType char(1) DEFAULT '',"
        StrSQL &= "processCode char(20) DEFAULT '',"
        StrSQL &= "point Integer DEFAULT(0),"
        StrSQL &= "dateWork Char (10)  DEFAULT '' ,"
        StrSQL &= "createBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateProductionProcessSum()
        Dim table As String = "ProductionProcessSum"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "moType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "moNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "moSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "mc char(30) DEFAULT '',"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "docEnd Integer DEFAULT 0,"
        StrSQL &= "workTime integer DEFAULT 0,"
        StrSQL &= "manPower Integer DEFAULT 0,"
        StrSQL &= "setTime integer DEFAULT 0,"
        StrSQL &= "lossTime Integer DEFAULT 0,"
        StrSQL &= "reasonCancel ntext DEFAULT '',"
        StrSQL &= "recStatus char(1) DEFAULT 'N',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateProductionProcessOperator()
        Dim table As String = "ProductionProcessOperator"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "moType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "moNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "moSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "wc char(10) DEFAULT '',"
        StrSQL &= "mc char(30) DEFAULT '',"
        StrSQL &= "dateCode Char (20)  DEFAULT '' ,"
        StrSQL &= "timeCode Char (20)  DEFAULT '' ,"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "scrapQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "lossTime Integer DEFAULT 0,"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateProductionProcessBOM()
        Dim table As String = "ProductionProcessBOM"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "bomItem char(20) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateCOCRecord()
        Dim table As String = "cocRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo varchar(20) NOT NULL ,"
        StrSQL &= "docRev varchar(5) DEFAULT '',"
        StrSQL &= "shipType varchar(5) DEFAULT '',"
        StrSQL &= "shipNo varchar(20) DEFAULT '',"
        StrSQL &= "shipDate varchar(10) DEFAULT '',"
        StrSQL &= "item varchar(20) DEFAULT '',"
        StrSQL &= "custPo varchar(40) DEFAULT '',"
        StrSQL &= "custPoShow varchar(100) DEFAULT '',"
        StrSQL &= "dateCode varchar(100) DEFAULT '',"
        StrSQL &= "qty varchar(100) DEFAULT '',"
        StrSQL &= "drawNo varchar(40) DEFAULT '',"
        StrSQL &= "drawRev varchar(10) DEFAULT '',"
        StrSQL &= "partName varchar(100) DEFAULT '',"
        StrSQL &= "partNo varchar(100) DEFAULT '',"
        StrSQL &= "partRev varchar(10) DEFAULT '',"
        StrSQL &= "rawDesc text DEFAULT '',"
        StrSQL &= "rawManu text DEFAULT '',"
        StrSQL &= "finished text DEFAULT '',"
        StrSQL &= "serialNo varchar(100) DEFAULT '',"
        StrSQL &= "reportRef varchar(40) DEFAULT '',"
        StrSQL &= "waiver varchar(40) DEFAULT '',"
        StrSQL &= "docShow varchar(20) DEFAULT '',"
        StrSQL &= "expdate varchar(40) DEFAULT '',"
        StrSQL &= "ulno varchar(100) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"

        StrSQL &= "PRIMARY KEY(docNo)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateOTRecord()
        Dim table As String = "OTRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "DocNo char(20) NOT NULL,"
        StrSQL &= "EmpNo char(10) NOT NULL,"
        StrSQL &= "EmpName char(250) NOT NULL,"
        StrSQL &= "Dept  Char (10)  NOT NULL,"
        StrSQL &= "Line char(50) DEFAULT '',"
        StrSQL &= "Work char(10) DEFAULT '',"
        StrSQL &= "Holiday char(10) DEFAULT '',"
        StrSQL &= "Absence char(10) DEFAULT '',"
        StrSQL &= "AbsenceTime Char (10) DEFAULT '',"
        StrSQL &= "Shift char(10) DEFAULT '',"
        StrSQL &= "ShiftDay char(10) DEFAULT '',"
        StrSQL &= "OTStartDate char(20) DEFAULT '',"
        StrSQL &= "OTStartTime char(10) DEFAULT '',"
        StrSQL &= "OTEndDate char(10) DEFAULT '',"
        StrSQL &= "OTEndTime char(10) DEFAULT '',"
        StrSQL &= "DateofOT char(20) DEFAULT '',"
        StrSQL &= "OTHours char(10)  DEFAULT '',"
        StrSQL &= "OTLunch char(10)  DEFAULT '',"
        StrSQL &= "OTTotal char(10)  DEFAULT '',"
        StrSQL &= "BusLine char(250) DEFAULT '',"
        StrSQL &= "Dinner char(10)  DEFAULT '',"
        StrSQL &= "OTOver char(10)  DEFAULT '',"
        StrSQL &= "Remark char(10)  DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(DocNo)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateNormalSatOT()
        Dim table As String = "NormalSatOT"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Id Integer NOT NULL  ,"
        StrSQL &= "DateSat  char(10)  NULL ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateUserOTRecord()
        Dim table As String = "UserOTRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "Id Integer NOT NULL  ,"
        StrSQL &= "Dept  Char (250)  NULL ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateEmployeeInfo()
        Dim table As String = "employeeInfo"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "empCode varchar(10) NOT NULL ,"
        StrSQL &= "empName nvarchar(250) DEFAULT '',"
        StrSQL &= "empNameEng varchar(250) DEFAULT '',"
        StrSQL &= "startDate varchar(10) DEFAULT '',"
        StrSQL &= "probDate varchar(10) DEFAULT '',"
        StrSQL &= "plant varchar(10) DEFAULT '',"
        StrSQL &= "department varchar(10) DEFAULT '',"
        StrSQL &= "position varchar(10) DEFAULT '',"
        StrSQL &= "groupWork varchar(10) DEFAULT '',"
        StrSQL &= "posLevel varchar(10) DEFAULT ''," 'for bonus
        StrSQL &= "statusEmp char(1) DEFAULT 'Y'," '(Y,N)
        StrSQL &= "empPic char(20) DEFAULT '',"
        StrSQL &= "salary Decimal(16,2) DEFAULT(0),"
        StrSQL &= "salPos Decimal(16,2) DEFAULT(0),"
        StrSQL &= "salSpe Decimal(16,2) DEFAULT(0),"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(empCode)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Sub CreateEmployeeAttendence()
        Dim table As String = "employeeAttendence"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "year varchar(4) NOT NULL ,"
        StrSQL &= "evalType varchar(5) NOT NULL ,"
        StrSQL &= "empCode varchar(10) NOT NULL ,"
        StrSQL &= "endDate varchar(10) DEFAULT '',"
        StrSQL &= "C1 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "C2 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "B Decimal(16,2) DEFAULT(0),"
        StrSQL &= "K Decimal(16,2) DEFAULT(0),"
        StrSQL &= "E Decimal(16,2) DEFAULT(0),"
        StrSQL &= "P1 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "P2 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "P3 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "A1 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "A2 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "A3 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "arriveLate Decimal(16,2) DEFAULT(0)," 'add by noi 17-06-2015
        StrSQL &= "priestHood Decimal(16,2) DEFAULT(0)," 'add by noi 07-07-2015
        StrSQL &= "dayAttend Decimal(16,2) DEFAULT(0),"
        StrSQL &= "ageWork Decimal(16,2) DEFAULT(0),"
        StrSQL &= "dayBonus Decimal(16,2) DEFAULT(0),"
        StrSQL &= "wagMin Decimal(16,2) DEFAULT(0),"
        StrSQL &= "bonusAmt Decimal(16,2) DEFAULT(0),"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(year,evalType,empCode)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'Sub CreateEmployeeEval()
    '    Dim table As String = "employeeEval"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &= "CREATE TABLE " & table & " ("
    '    StrSQL &= "year varchar(4) NOT NULL ,"
    '    StrSQL &= "evalType varchar(5) NOT NULL ,"
    '    StrSQL &= "empCode varchar(10) NOT NULL ,"
    '    StrSQL &= "endDate varchar(10) DEFAULT '',"
    '    StrSQL &= "gradeType char(5) DEFAULT '',"
    '    StrSQL &= "E1 Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "E2 Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "E3 Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "S Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "T Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "scoreEval Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "ageWork Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "groupBonus Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "bonusAmt Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "wagMin Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "CreateDate varchar(25) DEFAULT '',"
    '    StrSQL &= "PRIMARY KEY(year,evalType,empCode)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    'Sub CreateEmployeeSpecial()
    '    Dim table As String = "employeeSpecial"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &= "CREATE TABLE " & table & " ("
    '    StrSQL &= "year varchar(4) NOT NULL ,"
    '    StrSQL &= "evalType varchar(5) NOT NULL ,"
    '    StrSQL &= "empCode varchar(10) NOT NULL ,"
    '    StrSQL &= "endDate varchar(10) DEFAULT '',"
    '    StrSQL &= "byDay Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "byMoney Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "wagMin Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "bonusAmt Decimal(16,2) DEFAULT(0),"
    '    StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "CreateDate varchar(25) DEFAULT '',"
    '    StrSQL &= "PRIMARY KEY(year,evalType,empCode)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    'Sub CreateEmployeeWeightScore()
    '    Dim table As String = "employeeWeightScore"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &= "CREATE TABLE " & table & " ("
    '    StrSQL &= "plant varchar(10) NOT NULL,"
    '    StrSQL &= "department varchar(10) NOT NULL,"
    '    StrSQL &= "position varchar(10) NOT NULL,"
    '    StrSQL &= "groupWork varchar(10) NOT NULL,"
    '    StrSQL &= "wghScore Decimal(5,2) DEFAULT(0),"
    '    StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "CreateDate varchar(25) DEFAULT '',"
    '    StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
    '    StrSQL &= "PRIMARY KEY(plant,department,position,groupWork)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    Sub CreateEmployeeTransfer()
        Dim table As String = "employeeTransfer"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "DocNo char(20) NOT NULL,"
        StrSQL &= "EmpNo char(10) NOT NULL,"
        StrSQL &= "EmpName char(250) NOT NULL,"
        StrSQL &= "Line char(50) DEFAULT '',"
        StrSQL &= "Shift char(10) DEFAULT '',"
        StrSQL &= "DeptFrom  Char (10)  NOT NULL,"
        StrSQL &= "DeptTo  Char (10)  NOT NULL,"
        StrSQL &= "StartTime time(0) DEFAULT '',"
        StrSQL &= "EndTime time(0) DEFAULT '',"
        StrSQL &= "TotalTime integer DEFAULT '',"
        StrSQL &= "DateofTransfer char(10) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(DocNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


    'add new by noi --->>>>>------2015-06-29---->>>>----record loss time 
    Sub CreateProductionProcessLoss()
        Dim table As String = "ProductionProcessLoss"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "lossCode char(10) DEFAULT '',"
        StrSQL &= "dateStart Char (20)  DEFAULT '' ,"
        StrSQL &= "timeStart Char (20)  DEFAULT '' ,"
        StrSQL &= "dateEnd Char (20)  DEFAULT '' ,"
        StrSQL &= "timeEnd Char (20)  DEFAULT '' ,"
        StrSQL &= "lossTime integer DEFAULT 0,"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'add new by Gift --->>>>>------2015-07-27---->>>>----OTEmpList 
    'Sub CreateEmpInfo()
    '    Dim table As String = "ChangeEmpInfo"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &=  "CREATE TABLE " & table & " ("
    '    StrSQL &=  "DocNo char(20) NOT NULL,"
    '    StrSQL &=  "Dept char(10) NOT NULL,"
    '    StrSQL &=  "EmpNo char(10) NOT NULL,"
    '    StrSQL &=  "EmpName char(250) NOT NULL,"
    '    StrSQL &=  "Line char(50) DEFAULT '',"
    '    StrSQL &=  "LineNew char(50) DEFAULT '',"
    '    StrSQL &=  "Shift char(10) DEFAULT '',"
    '    StrSQL &=  "ShiftNew char(10) DEFAULT '',"
    '    StrSQL &=  "BusLine char(250) DEFAULT '',"
    '    StrSQL &=  "BusLineNew char(250) DEFAULT '',"
    '    StrSQL &=  "Position char(250) DEFAULT '',"
    '    StrSQL &=  "PositionNew char(250) DEFAULT '',"
    '    StrSQL &=  "ShiftDay char(10) DEFAULT '',"
    '    StrSQL &=  "ShiftDayNew char(10) DEFAULT '',"
    '    StrSQL &=  "StatusNew char(25) DEFAULT '',"
    '    StrSQL &=  "ChangeBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &=  "ChangeDate varchar(25) DEFAULT '',"
    '    StrSQL &=  "PRIMARY KEY(DocNo)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    'add new by noi --->>>>>------2015-07-18---->>>>----record log 
    Sub CreateProductionProcessLog()
        Dim table As String = "ProductionProcessLog"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "editCode char(1) DEFAULT ''," '1=update,0=Del
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "opCodeOld char(10) DEFAULT '',"
        StrSQL &= "dateStartOld Char (20)  DEFAULT '' ,"
        StrSQL &= "timeStartOld Char (20)  DEFAULT '' ,"
        StrSQL &= "dateEndOld Char (20)  DEFAULT '' ,"
        StrSQL &= "timeEndOld Char (20)  DEFAULT '' ,"
        StrSQL &= "shiftOld char(40) DEFAULT 'D',"
        StrSQL &= "acceptQtyOld Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectQtyOld Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "scrapQtyOld Decimal(16,2) DEFAULT(0),"
        StrSQL &= "scrapCodeOld char(10) DEFAULT '',"
        StrSQL &= "isSetOld char(1) DEFAULT '',"
        StrSQL &= "moOld Char (30)  DEFAULT '' ,"
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "dateStart Char (20)  DEFAULT '' ,"
        StrSQL &= "timeStart Char (20)  DEFAULT '' ,"
        StrSQL &= "dateEnd Char (20)  DEFAULT '' ,"
        StrSQL &= "timeEnd Char (20)  DEFAULT '' ,"
        StrSQL &= "shift char(40) DEFAULT 'D',"
        StrSQL &= "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "scrapQty Decimal(16,2) DEFAULT(0),"
        StrSQL &= "scrapCode char(10) DEFAULT '',"
        StrSQL &= "isSet char(1) DEFAULT '',"
        StrSQL &= "mo Char (30)  DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        'Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    'add new by Gift --->>>>>------2015-09-04---->>>>----QtyMngSys
    'Sub CreateQtyMngSys()
    '    Dim table As String = "QtyMngSys"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &= "CREATE TABLE " & table & " ("
    '    StrSQL &= "DocNo char(20) NOT NULL,"
    '    StrSQL &= "Supplier char(10) NOT NULL,"
    '    StrSQL &= "Certificate char(250) NOT NULL,"
    '    StrSQL &= "Buyer char (10)  DEFAULT '' ,"
    '    StrSQL &= "IssueDate char (10)  DEFAULT '' ,"
    '    StrSQL &= "ExpDate char (10)  DEFAULT '' ,"
    '    StrSQL &= "RecDate char (10)  DEFAULT '' ,"
    '    StrSQL &= "RevDate char (10)  DEFAULT '' ,"
    '    StrSQL &= "Status char (2)  DEFAULT '' ,"
    '    StrSQL &= "CreateBy char (20)  DEFAULT '' ,"
    '    StrSQL &= "CreateDate char(25) DEFAULT '',"
    '    StrSQL &= "ChangeBy char (20)  DEFAULT '' ,"
    '    StrSQL &= "ChangeDate char(25) DEFAULT '',"
    '    StrSQL &= "PRIMARY KEY(DocNo)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    'add 2015-09-16 by noi
    'Sub CreateRequirePrToPoLog()
    '    Dim table As String = "RequirePrToPoLog"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &= "CREATE TABLE " & table & " ("
    '    StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
    '    StrSQL &= "prNo varchar (30)  DEFAULT '' ,"
    '    StrSQL &= "poNo varchar (30)  DEFAULT '' ,"
    '    StrSQL &= "require1 varchar (255)  DEFAULT '' ,"
    '    StrSQL &= "require2 varchar (255)  DEFAULT '' ,"
    '    StrSQL &= "require3 varchar (255)  DEFAULT '' ,"
    '    StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "CreateDate varchar(25) DEFAULT '',"
    '    StrSQL &= "PRIMARY KEY(docNo)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    'record ers code add by noi on 2017-03-18
    Public tableERS As String = "ERSCode"
    Sub CreateERSCode()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableERS & "' )"
        StrSQL &= "CREATE TABLE " & tableERS & " ("
        StrSQL &= "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Item Char(20) DEFAULT '',"
        StrSQL &= "ItemRev  Char (2)  DEFAULT '' ,"
        StrSQL &= "CodeERS  Char (20)  DEFAULT '',"
        StrSQL &= "CreateBy char (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(ID)) ;"
        ' Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Public tableLogWorkcenterInfo As String = "LogWorkcenterInfo"
    Sub CreateLogWorkcenterInfo()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableLogWorkcenterInfo & "' )"
        StrSQL &= "CREATE TABLE " & tableLogWorkcenterInfo & " ("
        StrSQL &= "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "MX001 Char(20) DEFAULT ''," 'mach or line
        StrSQL &= "MX002  Char (20)  DEFAULT '' ," 'work center
        StrSQL &= "UDF01  Char (20)  DEFAULT ''," 'work type
        StrSQL &= "UDF02  Char (20)  DEFAULT ''," 'multi mo
        StrSQL &= "UDF03  Char (20)  DEFAULT ''," 'shift
        StrSQL &= "UDF04  Char (20)  DEFAULT ''," 'process type
        StrSQL &= "UDF51  Integer  DEFAULT '0'," 'normal time
        StrSQL &= "UDF52  Integer  DEFAULT '0'," 'over time
        StrSQL &= "UDF53  Integer  DEFAULT '0'," 'count
        StrSQL &= "CreateBy char (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(ID)) ;"
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

    Public tableProcessMach As String = "ProcessMach"
    Sub CreateProcessMach()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableProcessMach & "' )"
        StrSQL &= "CREATE TABLE " & tableProcessMach & " ("
        StrSQL &= "WorkCenter char(10) NOT NULL,"
        StrSQL &= "MachLine char(10) NOT NULL,"
        StrSQL &= "ProcessCode char(10) NOT NULL,"
        StrSQL &= "CreateBy char (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(WorkCenter,MachLine,ProcessCode)) ;"
        dbConn.TransactionSQL(StrSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
    End Sub

End Class
