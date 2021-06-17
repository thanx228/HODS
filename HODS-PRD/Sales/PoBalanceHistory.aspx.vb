Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class PoBalanceHistory
    Inherits System.Web.UI.Page

    Dim Conn_sql As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        GridBalance.Visible = False
    End Sub

    Private Sub SumPo()

        'Dim delSumPo As String = "delete from SumPo"
        'Conn_sql.Exec_Sql(delSumPo, Conn_sql.MIS_ConnectionString)

        'Dim StrSql As String
        'For M_count As Integer = 0 To GridView3.Rows.Count - 1

        '    Dim Item As String = GridView3.Rows(M_count).Cells(0).Text                   'Item
        '    Dim IQty As String = GridView3.Rows(M_count).Cells(1).Text.Replace(" ", "")  'Invoice Qty

        '    Dim GetSoQty As String = "select sum(TD008) as SQty from [JINPAO80].[dbo].[COPTD] where TD027 = '" & DropDownList1.SelectedValue & "'  and TD004='" & Item & "' group by TD004"
        '    Dim SoQty As String = Conn_sql.Get_value(GetSoQty, Conn_sql.ERP_ConnectionString)

        '    Dim GetPoBalanceQty As String = "select sum(Qty) as PoQty from [DBMIS].[dbo].[PoBalance] where Po ='" & DropDownList1.SelectedValue & "' and Item ='" & Item & "'"
        '    Dim PoQty As String = Conn_sql.Get_value(GetPoBalanceQty, Conn_sql.ERP_ConnectionString)

        '    Dim BalQty As String = SoQty - IQty - PoQty    'Sum BalanceQty
        '    StrSql = "insert into SumPo(Item,SQty,IQty,BalQty,PoQty) values('" & Item & "','" & SoQty & "','" & IQty & "','" & BalQty & "','" & PoQty & "')"

        '    Conn_sql.Exec_Sql(StrSql, Conn_sql.MIS_ConnectionString)
        'Next

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click

        GridBalance.Visible = True

        Dim delSumPo As String = "delete from SumPo"
        Conn_sql.Exec_Sql(delSumPo, Conn_sql.MIS_ConnectionString)

        Dim StrSql As String
        For M_count As Integer = 0 To Invoice.Rows.Count - 1

            Dim Item As String = Invoice.Rows(M_count).Cells(3).Text

            Dim SqlIQty As String = "select SUM(TB022) as IQty from  [JINPAO80].[dbo].[ACRTB] where TB048 = '" & DropDownList1.SelectedValue & "' and TB039='" & Item & "'"
            Dim IQty As Double = Conn_sql.Get_value(SqlIQty, Conn_sql.ERP_ConnectionString)

            Dim SqlSQty As String = "select SUM(TD008) as SQty from [JINPAO80].[dbo].[COPTD] where TD027 = '" & DropDownList1.SelectedValue & "' and TD004='" & Item & "'"
            Dim SQty As Double = Conn_sql.Get_value(SqlSQty, Conn_sql.ERP_ConnectionString)

            Dim SqlPQty As String = "select Qty from [DBMIS].[dbo].[PoBalance] where Po='" & DropDownList1.SelectedValue & "' and Item ='" & Item & "'"
            Dim PoQty As Double = Conn_sql.Get_value(SqlPQty, Conn_sql.ERP_ConnectionString)

            Dim BalQty As Double = SQty - IQty - PoQty

            Dim Sqlitem As String = "select Item from [DBMIS].[dbo].[SumPo] where Item ='" & Item & "'"
            Dim Citem As String = Conn_sql.Get_value(Sqlitem, Conn_sql.MIS_ConnectionString)
            If Citem <> "" Then

            ElseIf Citem = "" Then
                StrSql = "insert into SumPo(Item,IQty,SQty,PoQty,BalQty) values('" & Item & "','" & IQty & "','" & SQty & "','" & PoQty & "','" & BalQty & "')"
                Conn_sql.Exec_Sql(StrSql, Conn_sql.MIS_ConnectionString)
            End If
           
        Next

        GridBalance.DataBind()
    End Sub
End Class