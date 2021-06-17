Namespace DataControl
    Public Class VarIni 'variable initial
        'DB Type(data base type)
        'main system
        'cryp
        Public Const EncryptionKey As String = "1yA2TOgbswOOZ4ZZ" '

        'session
        Public Const UserId As String = "UserId"
        Public Const UserName As String = "UserName"
        Public Const UserGroup As String = "UserGroup"

        'char special
        Public Const char8 As String = "◘"
        Public Const C8 As String = char8
        Public Const char9 As String = "○"
        Public Const char10 As String = "◙"
        Public Const char11 As String = "♂"
        Public Const char12 As String = "♀"
        Public Const char21 As String = "§"

        'oracle
        'Public Const T100 As String = "T100" 'oracle
        'Public Const JODS As String = "JODS" 'oracle
        'Public Const JSIMS As String = "KIOSK" 'oracle
        'Public Const ORACLE As String = T100 & "|" & JODS & "|" & JSIMS  'oracle
        'Public Const T100DS As String = "DS" 'oracle

        'sqlserver
        Public Const ERP As String = "HOOTHAI" 'sqlserver
        Public Const DBMIS As String = "HOOTHAI_REPORT" 'sqlserver
        Public Const HRMHT As String = "HRMHT" 'sqlserver
        'Public Const DBMIST100 As String = "DBMIST100" 'sqlserver
        'Public Const DBMIST100TST As String = "DBMIST100_TST" 'sqlserver
        'Public Const SQLSERVER As String = ERP & "|" & DBMIS & "|" & DBMIST100 & "|" & DBMIST100TST 'sqlserver

        'mysql 
        'Public Const KIOSK As String = "MY_JSIMS" 'mysql
        'Public Const ERMIS As String = "ERMIS" 'mysql

        'area
        Public Const DEV As String = "T100dev"
        Public Const QAS As String = "T100qas"
        Public Const PRD As String = "topprd"

        'var for sql
        Public Const A As String = " AND "
        Public Const A2 As String = " * "
        Public Const B As String = " BETWEEN " 'Between
        Public Const B1 As String = "(" 'left bracket
        Public Const B2 As String = ")" 'right bracket
        Public Const C As String = "," 'comma
        Public Const D As String = "DELETE "
        Public Const DESC As String = "  DESC  "
        Public Const E As String = " = " 'equal
        Public Const F As String = " FROM "
        Public Const I As String = "INSERT INTO "
        Public Const I2 As String = " IN "
        Public Const H As String = " HAVING "
        Public Const G As String = " GROUP BY "
        Public Const L As String = " LEFT JOIN "
        Public Const L2 As String = " LIKE "
        Public Const O As String = " ORDER BY "
        Public Const O2 As String = " ON "
        Public Const P As String = "%"
        Public Const N As String = " NOT "
        Public Const NSP As String = "" 'none space
        Public Const RL As String = " REGEXP_LIKE" 'regexp_like
        Public Const S As String = "SELECT "
        Public Const S2 As String = " SET "
        Public Const SP As String = " " 'space
        Public Const SQ As String = "'" 'single qout
        Public Const U As String = "UPDATE "
        Public Const W As String = " WHERE "
        Public Const dot As String = "."
        Public Const oneEqualOne As String = " 1=1 "
        Public Const DISTINCT As String = " DISTINCT "

        'case when
        Public Const C2 As String = " CASE "
        Public Const T As String = " THEN "
        Public Const W2 As String = " WHEN "
        Public Const E2 As String = " ELSE "
        Public Const E3 As String = " END "

        'val
        'Public Const EntV As String = "3"
        'Public Const SiteV As String = "JINPAO"
        'Public Const PrefixDocno As String = "JP"
        'Public Const enUS_V As String = "en_US"
        'Public Const APSVersion_V As String = "JINPAO"

        'format decimal in datatrow
        Public Const NumberFormatCur As String = "C2"
        Public Const NumberFormat2Dec As String = "N2"
        Public Const numberFormatPercent As String = "P"
        Public Const numberFormatAccount As String = "$#,##0.00;($#,##0.00)"

        'format Date in string format
        Public Const dateFormatMMddyyyy As String = "0:MM/dd/yyyy"
        Public Const dateFormatddMMyyyy As String = "0:dd/MM/yyyy"
        Public Const dateFormatyyyyMMdd As String = "0:yyyy-MM-dd"
        Public Const dateFormatFull As String = "yyyy-MM-dd hh:mm:ss:tt"
        Public Const timeFormarhhmmss As String = "0:hh:mm:ss"
        Public Const dateFormatFull2 As String = "yyyyMMdd HH:mm:ss"

        Public Const YYYYMM As String = "YYYYMM"
        Public Const YYYYMMDD As String = "YYYYMMDD"
        Public Const YYYYMMDD2 As String = "YYYY/MM/DD"
        Public Const DDMMYYYY As String = "DD/MM/YYYY"
        Public Const MONTH As String = "MONTH"
        Public Const YEAR As String = "YEAR"
        Public Const WEEK As String = "W" 'start at monday
        Public Const WEEK2 As String = "IW" 'start at sunday
        Public Const YYYYMMDDHH24MI As String = "YYYYMMDD HH24:MI:SS"
        Public Const YYYYMMDDHH24MI2 As String = "YYYYMMDD HH24:MI"
        Public Const addAllDay As String = "+0.99999 "
        'table List
        Public Const TABLE_OT As String = "OVER_TIME_RECORD"

        'special value
        Public Const wcLabor As String = "WC06|WC13|WC19|WC51|WC21|WC67|WC71|WC63|WC32"

        Public Const YearBegin As Integer = 2017

        Public Const PageLogin As String = "~/Login.aspx"

        Public Const LimitGridView As Integer = 500 'Add by wit 10-11-2020
    End Class
End Namespace