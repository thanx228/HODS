Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class MOReceiptReport
    Inherits System.Web.UI.Page
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl
    Dim ChkCont As New CheckBoxListControl
    Dim expCont As New ExportImportControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            ChkRecType.setObject = "D2,D3,58"

            Dim SqlWH As String = String.Empty
            SqlWH = "select MC001,MC001+' : '+MC002 as MC002 from CMSMC  order by MC002"
            ChkCont.showCheckboxList(ChkWH, SqlWH, VarIni.ERP, "MC002", "MC001", 4)

        End If
    End Sub


    Protected Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        ShowReport(True)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btReport_Click(sender As Object, e As EventArgs) Handles btReport.Click
        ShowReport(False)
    End Sub

    Function ShowReport(Optional ByVal excel As Boolean = True) As DataTable
        Dim Al As New ArrayList
        Dim fldName As New ArrayList
        Dim colName As New ArrayList
        Dim fldNumber As New ArrayList
        Dim c8 As String = VarIni.C8

        With New ArrayListControl(Al)
            .TAL("(SUBSTRING(TF012,7,2)+'/'+SUBSTRING(TF012,5,2)+'/'+SUBSTRING(TF012,1,4))" & c8 & "Doc_Date", "Doc. Date")
            .TAL("TG014+'-'+TG015" & c8 & "MO_TYPR_NO", "MO Type-No")
            .TAL("TA006", "Item")
            .TAL("TA035", "Spec")
            .TAL("QTY" & c8 & "QTY", "Receipt Qty", "2")
            .TAL("RTrim(TG010) +'-'+MC002" & c8 & "ISSUE_WH", "Issue to Warehouse")
            .TAL("TA015 - TA017 - TA018" & c8 & "BALANCE_WIP", "Balance WIP", "2")

            fldName = .ChangeFormat(False)
            colName = .ChangeFormat(True)
            fldNumber = .ChangeFormat()
        End With


        'From Sub
        Dim FromSub As String = String.Empty
        Dim fldFrom As New ArrayList From {
            "TF012",
            "TG014",
            "TG015",
            "SUM(TG011)" & c8 & "QTY",
            "min(TG010)" & c8 & "TG010"
            }
        Dim sqlStrFrom As New SQLString("MOCTF", fldFrom, strSplit:=c8)
        With sqlStrFrom
            .setLeftjoin("MOCTG", New List(Of String) From {
                "TF001" & c8 & "TG001",
                "TF002" & c8 & "TG002"
                }
            )

            .SetWhere(.WHERE_IN("TG010", ChkWH.SelectedValue.Trim), True)
            .SetWhere(.WHERE_IN("TF001", ChkRecType.getObject))
            .SetWhere(.WHERE_EQUAL("TF002", txtRecNo.Text))
            .SetWhere(.WHERE_EQUAL("TG014", txtMoType.Text))
            .SetWhere(.WHERE_EQUAL("TG015", txtMoNo.Text))
            .SetWhere(.WHERE_BETWEEN("TF012", DateF.Text, DateT.Text))

            .SetGroupBy("TF012, TG014, TG015")

            FromSub = sqlStrFrom.GetSQLString()

        End With


        Dim sqlStr As New SQLString(dbConn.addBracket(FromSub) & " TRN", fldName, strSplit:=c8)
        With sqlStr

            .setLeftjoin("CMSMC", New List(Of String) From {
                       "MC001" & c8 & "TG010"
                       }
             )

            .setLeftjoin("MOCTA", New List(Of String) From {
                      "TA001" & c8 & "TG014",
                      "TA002" & c8 & "TG015"
                      }
            )

            .SetWhere(.WHERE_EQUAL("TA006", txtItem.Text), True)
            .SetWhere(.WHERE_EQUAL("TA035", txtSpec.Text))

            .SetOrderBy("TG014, TG015, TF012")
        End With

        Dim dtShow As DataTable = dtCont.setColDatatable(colName, c8)
        Dim dt As DataTable = dbConn.Query(sqlStr.GetSQLString(), VarIni.ERP, dbConn.WhoCalledMe)

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim fldManual As New ArrayList
                .AddDatarow(dtShow, fldNumber, fldManual)
            End With
        Next

        Dim Count As Integer = dt.Rows.Count

        If excel = True Then
            expCont.Export("MO Receipt Report" & Session("UserID"), dtShow)
        Else
            If Count > VarIni.LimitGridView Then
                show_message.ShowMessage(Page, "จำนวนข้อมูลมากกว่า 500 แถว (" & Count & " Rows) \nโปรดเลือก Export Excel. ", UpdatePanel)
            Else
                gvCont.GridviewInitial(gvShow, colName, strSplit:=c8)
                gvCont.ShowGridView(gvShow, dtShow)
                CountRow.RowCount = dtShow.Rows.Count
            End If
        End If
        Return dtShow
    End Function

End Class

'NOTE SQL
'Select Case
'(SUBSTRING(TF012,7,2)+'/'+SUBSTRING(TF012,5,2)+'/'+SUBSTRING(TF012,1,4))  Doc_Date,
'TG014+'-'+TG015 MO_TYPR_NO,
'TA006, --TG004,
'TA035, --TG006,
'QTY,
'Rtrim(TG010) +'-'+MC002 ISSUE_WH,
'TA015 - TA017 - TA018 BALANCE_WIP
'from(select TF012, TG014, TG015, SUM(TG011) QTY, min(TG010)  TG010 from MOCTF left join MOCTG on  TF001=TG001 And TF002=TG002 
'--where include here
' WHERE 1 = 1
'--And TF001  IN ('D301')    
'--And TF002 = '20210507003'   
'--And TG014 = '5103'   
'--And TG015 = '20210206019'     
'--And TF012 BETWEEN '20210507' AND '20210507'   

' group by TF012,TG014,TG015 ) TRN 
' left join CMSMC On MC001=TG010
' left join MOCTA On TA001=TG014 And TA002=TG015
'WHERE 1 = 1
'--And TA006 = '502234300003'   
'--And TA035 = '3466680205MREV.07'   

' order by TG014,TG015,TF012;