Public Class createTableTrace
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

End Class
