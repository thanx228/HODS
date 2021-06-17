Imports System.Linq
Namespace DataControl
    Public Class SQLTEXT
        Inherits DataConnectControl

        Dim listcont As New ListControl
        Dim outcont As New OutputControl

        Public Table As String = String.Empty
        Public SQL As String = String.Empty
        Public LeftJoin As String = String.Empty
        Public WhrStr As String = String.Empty
        Public OrdBy As String = String.Empty
        Public GrpBy As String = String.Empty
        Public Offset As String = String.Empty
        Public FetchFirst As String = String.Empty
        Public SHOW_DB As Boolean = False
        Public whereList As New List(Of String)
        Public leftList As New List(Of String)
        Dim listcontWhere As New ListControl(whereList)

        Sub New(TableName As String,
                fldName As List(Of String),
                Optional PI_SHOW_DB As Boolean = False)
            Initial(TableName, PI_SHOW_DB)
            SL(fldName)
        End Sub

        Sub New(TableName As String,
                fldName As String,
                Optional PI_SHOW_DB As Boolean = False)
            Initial(TableName, PI_SHOW_DB)
            SL(fldName)
        End Sub

        'new version 2
        Sub New(TableName As String,
                Optional PI_SHOW_DB As Boolean = False)
            Initial(TableName, PI_SHOW_DB)
        End Sub

        Sub Initial(TableName As String, PI_SHOW_DB As Boolean)
            SHOW_DB = PI_SHOW_DB
            Table = TableName
            W()
        End Sub


        Sub SL(fldName As List(Of String)) 'select from list(of string)
            SQL = String.Join("", New List(Of String) From {
                VarIni.S,
                F(fldName),
                VarIni.F,
                 Table
            })
        End Sub
        Sub SL(fldName As String) 'select from string
            SQL = String.Join("", New List(Of String) From {VarIni.S, fldName, VarIni.F, Table})
        End Sub

        Function F(fld As List(Of String)) As String
            Dim listcontFld As New ListControl(fld)
            Return listcontFld.Text(VarIni.C)
        End Function

        'Function F(fld As String, Optional alaisName As String = "") As String
        '    Return Field(fld, alaisName)
        'End Function

        Private Sub W() 'where start
            'If T100 Then
            '    WE(Table & VarIni.EntF, VarIni.EntV, addSigleQuote:=False)
            '    If SITE Then
            '        WE(Table & VarIni.SiteF, VarIni.SiteV)
            '    End If
            'Else
            '    WE("1", "1", addSigleQuote:=False)
            'End If
            'WhrStr &= VarIni.W
        End Sub

        Sub W(whrList As List(Of String))
            listcontWhere.Add(whrList)
        End Sub

        Sub GB(grpByVal As String) 'group by
            GrpBy = If(String.IsNullOrEmpty(grpByVal), String.Empty, GroupBy(New List(Of String) From {grpByVal}))
        End Sub

        Sub GB(fldGroupList As List(Of String)) 'group by
            GrpBy = GroupBy(fldGroupList)
        End Sub

        Sub OB(ordByAdd As String) 'order by
            OrdBy = If(String.IsNullOrEmpty(ordByAdd), String.Empty, OrderBy(New List(Of String) From {ordByAdd}))
        End Sub
        Sub OB(fldOrderList As List(Of String)) 'order by
            OrdBy = OrderBy(fldOrderList)
        End Sub

        Sub LJ(TableJoin As String, LeftJoinList As List(Of String)) 'left join for general SQL
            'format to show ==> LEFT JOIN TABLE_JOIN ON FLD_LEFT=FLD_RIGHT
            LeftJoin &= AddSpace(New List(Of String) From {VarIni.L, TableJoin, VarIni.O2}) & AddSpace(LeftJoinList, True, True, VarIni.A)
        End Sub

        Sub LJ(TableJoin As String, LeftJoin As String)
            If Not String.IsNullOrEmpty(LeftJoin) Then
                LJ(TableJoin, New List(Of String) From {LeftJoin})
            End If
        End Sub

        'Left Join
        'left join equal(LE)
        Function LE(pi_fldLeft As String, pi_fldRight As String,
                    Optional pi_between As String = VarIni.E,
                    Optional pi_addSQ As Boolean = False) As String
            Return WHERE_EQUAL(pi_fldLeft, pi_fldRight, pi_between, pi_addSQ, False)
        End Function

        'where_equal (WE)

        Sub WE(fld As String, val As String,
               Optional showSymbol As String = VarIni.E,
               Optional addSigleQuote As Boolean = True,
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_EQUAL(fld, val, showSymbol, addSigleQuote, showAnd))
        End Sub

        Sub WE(fld As String, ddl As DropDownList,
               Optional showSymbol As String = VarIni.E,
               Optional addSigleQuote As Boolean = True,
               Optional showAnd As Boolean = False,
               Optional defaultValue As String = "0")
            listcontWhere.Add(WHERE_EQUAL(fld, ddl, showSymbol, addSigleQuote, showAnd, defaultValue))
        End Sub

        Sub WE(fld As String, tb As TextBox,
               Optional showSymbol As String = VarIni.E,
               Optional addSigleQuote As Boolean = True,
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_EQUAL(fld, tb, showSymbol, addSigleQuote, showAnd))
        End Sub

        Sub WE(fld As String, cb As CheckBox,
               Optional valWhenTrue As String = "Y",
               Optional showSymbol As String = VarIni.E,
               Optional addSigleQuote As Boolean = True,
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_EQUAL(fld, cb, showSymbol, addSigleQuote, showAnd))
        End Sub

        'where_in(WI)
        Sub WI(fld As String, val As String,
               Optional notIn As Boolean = False,
               Optional addSigleQuote As Boolean = False,
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_IN(fld, val, notIn, addSigleQuote, showAnd))
        End Sub

        Sub WI(fld As String, val As List(Of String),
               Optional notIn As Boolean = False,
               Optional addSigleQuote As Boolean = True,
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_IN(fld, String.Join(VarIni.C, val), notIn, addSigleQuote, showAnd))
        End Sub

        Sub WI(fld As String, cbl As CheckBoxList,
               Optional ByVal notIn As Boolean = False,
               Optional ByVal allWhenNotSelect As Boolean = False,
               Optional ByVal valDefault As String = "",
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_IN(fld, cbl, notIn, allWhenNotSelect, valDefault, showAnd))
        End Sub

        Sub WI(fld As String, ddl As DropDownList,
               Optional notValue As String = "0",
               Optional selAllRang As Boolean = False,
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_IN(fld, ddl, notValue, selAllRang, showAnd))
        End Sub
        Sub WI(fld As String, tb As TextBox,
              Optional notIn As Boolean = False,
              Optional addSigleQuote As Boolean = False,
              Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_IN(fld, tb, notIn, addSigleQuote, showAnd))
        End Sub

        'where_like(WL)
        Sub WL(fld As String, val As String,
               Optional prefix As Boolean = True,
               Optional postfix As Boolean = True,
               Optional notLike As Boolean = False,
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_LIKE(fld, val, prefix, postfix, notLike, showAnd))
        End Sub

        Sub WL(fld As String, ddl As DropDownList,
               Optional prefix As Boolean = True,
               Optional postfix As Boolean = True,
               Optional notLike As Boolean = False,
               Optional valAll As String = "0",
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_LIKE(fld, ddl, prefix, postfix, notLike, valAll, showAnd))
        End Sub

        Sub WL(fld As String, tb As TextBox,
               Optional prefix As Boolean = True,
               Optional postfix As Boolean = True,
               Optional notLike As Boolean = False,
               Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_LIKE(fld, tb, prefix, postfix, notLike, showAnd))
        End Sub

        'where_between(WB)
        Sub WB(fld As String, valStart As String, valEnd As String,
               Optional addSQ1 As Boolean = True,
               Optional addSQ2 As Boolean = True,
              Optional showAnd As Boolean = False)
            listcontWhere.Add(WHERE_BETWEEN(fld, valStart, valEnd, addSQ1, addSQ2, showAnd))
        End Sub

        Function TEXT(Optional subQuery As Boolean = False,
                      Optional aliasName As String = "") As String

            If listcontWhere.Count > 0 Then
                WhrStr = VarIni.W & listcontWhere.Text(VarIni.A)
            End If
            Dim SSQL As String = SQL & LeftJoin & WhrStr & GrpBy & OrdBy & Offset & FetchFirst
            If subQuery Then
                SSQL = addBracket(SSQL) & If(String.IsNullOrEmpty(aliasName), "", " " & aliasName)
            End If
            Return SSQL
        End Function

        'add 2021/05/26 by noi
        Function ToArrayList(list As List(Of String)) As ArrayList
            Dim newAl As New ArrayList
            For Each txt As String In list
                If Not String.IsNullOrEmpty(txt) Then
                    newAl.Add(txt)
                End If
            Next
            Return newAl
        End Function

        'add 2021/05/26 by noi
        Function ToList(list As ArrayList) As List(Of String)
            Dim newList As New List(Of String)
            With New ListControl(newList)
                For Each txt As String In list
                    .Add(txt)
                Next
            End With
            Return newList
        End Function

    End Class
End Namespace


