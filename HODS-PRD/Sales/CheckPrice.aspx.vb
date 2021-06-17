Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class CheckPrice
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub BtShow_Click(sender As Object, e As EventArgs) Handles BtShow.Click
        Dim al As New ArrayList
        Dim fldName As ArrayList
        Dim colName As ArrayList
        With New ArrayListControl(al)
            .TAL("A.MB001" & VarIni.C8 & "CUST_CODE", "Cust Code")
            .TAL("MA002", "Cust Name")
            .TAL("A.MB002" & VarIni.C8 & "ITEM", "Item")
            .TAL("B.MB003" & VarIni.C8 & "SPEC", "Spec")
            .TAL("A.MB017" & VarIni.C8 & "EFFECT_DATE", "Effect Date")
            .TAL("A.MB018" & VarIni.C8 & "EXPIRE_DATE", "Expire")
            .TAL("A.MB008" & VarIni.C8 & "PRICE", "Price", "3")
            fldName = .ChangeFormat()
            colName = .ChangeFormat(True)
        End With

        Dim strSQL As New SQLString("COPMB A", fldName)
        strSQL.setLeftjoin(" Left Join INVMB B on B.MB001=A.MB002 ")
        strSQL.setLeftjoin(" Left Join COPMA on MA001=A.MB001 ")
        Dim whr As String = ""
        whr &= strSQL.WHERE_LIKE("A.MB001", TbCustCode)
        whr &= strSQL.WHERE_LIKE("MA002", TbCustName)
        whr &= strSQL.WHERE_LIKE("A.MB002", TbItem)
        whr &= strSQL.WHERE_LIKE("B.MB003", TbSpec)
        whr &= strSQL.WHERE_DATE("A.MB017", UcDateEffFrom.Text, UcDateEffTo.Text)
        whr &= strSQL.WHERE_DATE("A.MB018", UcDateExpireFrom.Text, UcDateExpireTo.Text)


        If CbNotExpire.Checked Then
            whr &= " and (A.MB018>=convert(varchar, getdate(), 112) or A.MB018 is null) and A.MB017<=convert(varchar, getdate(), 112) "
        End If
        strSQL.SetWhere(whr, True)
        strSQL.SetOrderBy("A.MB001,A.MB002")
        UcGv.ShowGridviewHyperLink(strSQL.GetSQLString, VarIni.ERP, colName)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles BtExport.Click
        ControlForm.ExportGridViewToExcel("CheckPrice" & Session("UserName"), UcGv.getGridview)
    End Sub


End Class