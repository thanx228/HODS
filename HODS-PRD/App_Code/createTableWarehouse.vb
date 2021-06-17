Public Class createTableWarehouse
    Dim Conn_SQL As New ConnSQL
    'add 2015-09-09 by noi for control batch no
    Sub createBatchRecord()
        Dim table As String = "BatchRecordLog"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "moType varchar(5) NOT NULL  ,"
        StrSQL &= "moNo varchar(20) NOT NULL  ,"
        StrSQL &= "barNo varchar(5) DEFAULT '',"
        StrSQL &= "poNo varchar(255) DEFAULT '',"
        StrSQL &= "soSeq varchar(255) DEFAULT '',"
        StrSQL &= "itemOld varchar(20)  DEFAULT '' ,"
        StrSQL &= "conRedo varchar(20)  DEFAULT '' ,"
        StrSQL &= "conHardTool varchar(20)  DEFAULT '' ,"
        StrSQL &= "conFA varchar(1)  DEFAULT '0' ,"
        StrSQL &= "BatchRef varchar(10) DEFAULT '',"
        StrSQL &= "CreateBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add 2015-09-21 by noi for record usage mat for production line
    Sub createProductionMatUsage()
        Dim table As String = "ProductionMatUsage"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "moType varchar(5) DEFAULT ''  ,"
        StrSQL &= "moNo varchar(20) DEFAULT ''  ,"
        StrSQL &= "item varchar(20) DEFAULT ''  ,"
        StrSQL &= "lot varchar(30) DEFAULT ''  ,"
        StrSQL &= "moQty Decimal(16,5) DEFAULT 0,"
        StrSQL &= "qtyPer Decimal(16,5) DEFAULT 0,"
        StrSQL &= "docStart Integer DEFAULT '0'  ,"
        StrSQL &= "CreateBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add 2015-09-21 by noi for record usage mat for production line
    Sub createLabelLog()
        Dim table As String = "LabelLog"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "docType varchar(5) DEFAULT ''  ,"
        StrSQL &= "docNo varchar(20) DEFAULT ''  ,"
        StrSQL &= "docSeq varchar(20) DEFAULT ''  ,"
        StrSQL &= "fullBox Decimal(16,5) DEFAULT 0,"
        StrSQL &= "qtyBox Decimal(16,5) DEFAULT 0,"
        StrSQL &= "qtyLast Decimal(16,5) DEFAULT 0,"
        StrSQL &= "CreateBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateFGLabel()
        Dim table As String = "FGLabel"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "DocNo char(20) NOT NULL,"
        StrSQL = StrSQL & "moType Char(4) NOT NULL," 'MO Receipt type
        StrSQL = StrSQL & "moNo Char(20) NOT NULL," 'MO Receipt No
        StrSQL = StrSQL & "moSeq Char(4) NOT NULL," 'MO Receipt seq
        StrSQL = StrSQL & "qty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "qtyCtn Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "CtnNo Char(250) DEFAULT '' ,"
        StrSQL = StrSQL & "CtnSpec Char(250) DEFAULT '' ,"
        StrSQL = StrSQL & "CtnWgh Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "PackBy varchar (200)  DEFAULT '' ,"
        StrSQL = StrSQL & "custPO Char(150) DEFAULT '' ,"
        StrSQL = StrSQL & "serailNo Char(150) DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(DocNo,moType,moNo,moSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateLabelRecord()
        Dim table As String = "LabelDeltaRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Char(20) NOT NULL ," 'run Serail no
        StrSQL &= "invType Char(4) DEFAULT ''," 'Invoice type
        StrSQL &= "invNo Char(20) DEFAULT ''," 'Invoice No
        StrSQL &= "invSeq Char(4) DEFAULT ''," 'Invoice seq
        StrSQL &= "dateCode Char(10) DEFAULT '',"
        StrSQL &= "vender Char(10) DEFAULT '',"
        StrSQL &= "tradeCode Char(10) DEFAULT '',"
        StrSQL &= "plant Char(10) DEFAULT '',"
        StrSQL &= "item Char(20) DEFAULT '',"
        StrSQL &= "spec Char(50) DEFAULT '',"
        StrSQL &= "custPO Char(10) DEFAULT '',"
        StrSQL &= "unit Char(10) DEFAULT 'PCE',"
        StrSQL &= "qty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "packType Char(10) DEFAULT 'F'," 'F=Full,L=Last
        StrSQL &= "qty_pack Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "timeSend Char(10) DEFAULT '',"
        StrSQL &= "custWO Char(20) DEFAULT '',"
        StrSQL &= "custLine Char(20) DEFAULT '',"
        StrSQL &= "custModel Char(20) DEFAULT '',"
        StrSQL &= "printTime integer DEFAULT '0',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate Char(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate Char(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

End Class
