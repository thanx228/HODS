Public Class createTableSale
    Dim Conn_SQL As New ConnSQL

    'Sub createCustPOHead()
    '    Dim table As String = "CustPOHead"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &= "CREATE TABLE " & table & " ("
    '    StrSQL &= "docNo char(20) NOT NULL ,"
    '    StrSQL &= "docDate varchar(20) DEFAULT '',"
    '    StrSQL &= "custCode varchar(5) DEFAULT '',"
    '    StrSQL &= "plant varchar(10) DEFAULT '',"
    '    StrSQL &= "remark nvarchar(250) DEFAULT '',"
    '    StrSQL &= "status char(2) DEFAULT '1',"
    '    StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "CreateDate varchar(25) DEFAULT '',"
    '    StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
    '    StrSQL &= "PRIMARY KEY(docNo)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    'Sub createCustPOBody()
    '    Dim table As String = "CustPOBody"
    '    Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
    '    StrSQL &= "CREATE TABLE " & table & " ("
    '    StrSQL &= "docNo char(20) NOT NULL ,"
    '    StrSQL &= "seq Integer NOT NULL ,"
    '    StrSQL &= "custCodeB varchar(5) DEFAULT '',"
    '    StrSQL &= "plantB varchar(10) DEFAULT '',"
    '    StrSQL &= "item varchar(20) NOT NULL  ,"
    '    StrSQL &= "custPO varchar(150) DEFAULT '',"
    '    StrSQL &= "price Decimal(16,5) DEFAULT(0) ,"
    '    StrSQL &= "poQty Decimal(16,5) DEFAULT(0) ,"
    '    StrSQL &= "voidQty Decimal(16,5) DEFAULT(0) ,"
    '    StrSQL &= "useQty Decimal(16,5) DEFAULT(0) ,"
    '    StrSQL &= "remark nvarchar(250) DEFAULT '',"
    '    StrSQL &= "status char(2) DEFAULT '1',"
    '    StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "CreateDate varchar(25) DEFAULT '',"
    '    StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
    '    StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
    '    StrSQL &= "PRIMARY KEY(docNo,seq)) ;"
    '    Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    Sub createCustPOInfo()
        Dim table As String = "CustPOInfo"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "CustPO varchar(50) NOT NULL,"
        StrSQL &= "DocDate varchar(20) DEFAULT '',"
        StrSQL &= "Cust varchar(5) DEFAULT '',"
        StrSQL &= "Plant varchar(10) DEFAULT '',"
        StrSQL &= "Remark nvarchar(250) DEFAULT '',"
        StrSQL &= "StatusInfo char(2) DEFAULT 'Y',"
        StrSQL &= "CreateBy varchar (10)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (10)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(CustPO)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createCustPODetail()
        Dim table As String = "CustPODetail"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "CustPOD varchar(50) NOT NULL,"
        StrSQL &= "Item varchar(20) NOT NULL  ,"
        StrSQL &= "Price Decimal(16,5) DEFAULT(0) ,"
        StrSQL &= "Qty Decimal(16,5) DEFAULT(0) ,"
        StrSQL &= "QtyVoid Decimal(16,5) DEFAULT(0) ,"
        StrSQL &= "RemarkD nvarchar(250) DEFAULT '',"
        StrSQL &= "StatusDetail char(2) DEFAULT '1',"
        StrSQL &= "CreateByD varchar (10)  DEFAULT '' ,"
        StrSQL &= "CreateDateD varchar(25) DEFAULT '',"
        StrSQL &= "ChangeByD varchar (10)  DEFAULT '' ,"
        StrSQL &= "ChangeDateD varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(CustPOD,Item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


End Class

