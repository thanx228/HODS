Imports MIS_HTI.DataControl
Public Class ITEM

    Private Function Text(SQL As String, aliasName As String) As String
        Return VarIni.B1 & SQL & VarIni.B2 & VarIni.SP & aliasName
    End Function

    'sale order
    Function SO(Optional aliasName As String = "SO") As String
        Dim SQL As String = "select COPTD.TD004 SO_ITEM,SUM(COPTD.TD008-COPTD.TD009) SO_QTY,min(TD013) SO_DUE_DATE from COPTD left join COPTC on COPTC.TC001=COPTD.TD001 and COPTC.TC002=COPTD.TD002 where COPTD.TD016='N' group by COPTD.TD004 "
        Return Text(SQL, aliasName)
    End Function
    Function CI(Optional aliasName As String = "CI") As String
        Dim SQL As String = "select ITEM CI_ITEM,sum(CALL_IN_BAL_QTY2) CI_QTY,min(SHIP_DATE) CI_SHIP_DATE from " & VarIni.DBMIS & "..V_CALL_IN_BALANCE  where CALL_IN_BAL_QTY2>0   group by ITEM "
        Return Text(SQL, aliasName)
    End Function
    'purchase request
    Function PR(Optional aliasName As String = "PR") As String
        Dim SQL As String = ""
        SQL &= " select PURTB.TB004 PR_ITEM, sum(PURTR.TR006) PR_QTY from PURTB"
        SQL &= " left join PURTR on PURTR.TR001=PURTB.TB001 and PURTR.TR002=PURTB.TB002 and PURTR.TR003=PURTB.TB003"
        SQL &= " left join PURTA on PURTA.TA001=PURTB.TB001 and PURTA.TA002=PURTB.TB002 "
        SQL &= " where PURTB.TB039='N'"
        SQL &= " group by PURTB.TB004 HAVING sum(PURTR.TR006)>0"
        Return Text(SQL, aliasName)
    End Function

    'purchase order
    Function PO(Optional aliasName As String = "PO") As String
        Dim SQL As String = ""
        SQL &= " select PURTD.TD004 PO_ITEM,SUM(isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)) as PO_QTY,min(TD012) PO_DUE_DATE from PURTD "
        SQL &= " left join PURTC on PURTC.TC001=PURTD.TD001 and PURTC.TC002=PURTD.TD002"
        SQL &= " where PURTD.TD016='N' and PURTC.TC014 in ('N','Y') and isnull(PURTD.TD008,0)-isnull(PURTD.TD015,0)>0"
        SQL &= " group by PURTD.TD004 "
        Return Text(SQL, aliasName)
    End Function

    'purchase reciept and inspection
    Function PI(Optional aliasName As String = "PO_INSP") As String
        Dim SQL As String = ""
        SQL &= " select PURTH.TH004 PI_ITEM,SUM(isnull(PURTH.TH007,0)) PI_QTY from PURTH "
        SQL &= " left join PURTG on PURTG.TG001=PURTH.TH001 and PURTG.TG002=PURTH.TH002"
        SQL &= " where PURTG.TG013 = 'N' "
        SQL &= " group by PURTH.TH004 having (SUM(isnull(PURTH.TH007, 0)) > 0) "
        Return Text(SQL, aliasName)
    End Function

    'manufacture order
    Function MO(Optional aliasName As String = "MO") As String
        Dim SQL As String = ""
        SQL &= " Select MOCTA.TA006 MO_ITEM, SUM(isnull(MOCTA.TA015,0)-isnull(MOCTA.TA017,0)-isnull(MOCTA.TA018,0)) MO_QTY,MIN(MOCTA.TA010) MO_PLAN_COMP_DATE from MOCTA "
        SQL &= " where MOCTA.TA011 Not In('y','Y') and MOCTA.TA013 ='Y' "
        SQL &= " group by MOCTA.TA006 "
        Return Text(SQL, aliasName)
    End Function

    'materail issued balance
    Function MI(Optional aliasName As String = "MI") As String
        Dim SQL As String = ""
        SQL &= " select MOCTB.TB003 MI_ITEM,SUM(MOCTB.TB004-MOCTB.TB005) MI_QTY,MIN(MOCTB.TB015) MI_ISSUE_DATE from MOCTB "
        SQL &= " left join MOCTA on MOCTA.TA001=MOCTB.TB001 and MOCTA.TA002=MOCTB.TB002 "
        SQL &= " where MOCTB.TB004-MOCTB.TB005>0 and  MOCTA.TA011 not in('y','Y') and MOCTA.TA013='Y'"
        SQL &= " group by MOCTB.TB003"
        Return Text(SQL, aliasName)
    End Function

    'inventory stock
    Function STOCK(Optional aliasName As String = "STOCK") As String
        Dim SQL As String = "select INVMC.MC001 STOCK_ITEM,SUM(isnull(INVMC.MC007,0)) STOCK_QTY,SUM(isnull(INVMC.MC004,0)) as SAFTY_STOCK from INVMC where (INVMC.MC007 <>0 or INVMC.MC004<>0  )  group by INVMC.MC001"
        Return Text(SQL, aliasName)
    End Function

End Class
