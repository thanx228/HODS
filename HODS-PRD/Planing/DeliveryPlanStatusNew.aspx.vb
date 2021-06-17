Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class DeliveryPlanStatusNew
    Inherits System.Web.UI.Page
    Dim DataSOtype As String = "('2201','2202','2203','2204','2205','2213')"
    Dim dbConn As New DataConnectControl
    Dim cblCont As New CheckBoxListControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session(VarIni.UserName) <> "" Then
                Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' and MQ001 in " & DataSOtype & " order by MQ002 "
                cblCont.showCheckboxList(cblSaleType, SQL, VarIni.ERP, "MQ002", "MQ001", 6)
                btExport.Visible = False
                TabContainer1.ActiveTabIndex = 0
            End If
        End If
    End Sub

    Sub DeliverySum()
        Dim SQL As String = ""
        Dim WHR As String = ""
        'Dim WHRD As String = ""
        'sub select
        Dim fldSum As String = ""
        Dim colName As New ArrayList
        If CbSumDelDate.Checked Then
            fldSum = ",TG003"
            colName.Add("Del Date" & VarIni.char8 & "TG003")
        End If
        colName.Add("Cust" & VarIni.char8 & "TG004")
        colName.Add("Cust Name" & VarIni.char8 & "MA002")
        colName.Add("ITEM" & VarIni.char8 & "TH004")
        colName.Add("SPEC" & VarIni.char8 & "TH006")
        colName.Add("Cust WO" & VarIni.char8 & "UDF01")
        colName.Add("Del Qty" & VarIni.char8 & "TH008" & VarIni.char8 & "0")
        colName.Add("Carton Spec" & VarIni.char8 & "UDF04")
        colName.Add("Qty Per Box" & VarIni.char8 & "UDF51" & VarIni.char8 & "0")
        colName.Add("Full Box" & VarIni.char8 & "FULL_BOX" & VarIni.char8 & "0")
        colName.Add("Last Box" & VarIni.char8 & "LAST_BOX" & VarIni.char8 & "0")
        colName.Add("Total Box" & VarIni.char8 & "ALL_BOX" & VarIni.char8 & "0")
        colName.Add("Stock Qty" & VarIni.char8 & "MC007" & VarIni.char8 & "0")
        colName.Add("Plant" & VarIni.char8 & "PLANT" & VarIni.char8 & "0")
        Dim SQLPlant As String = ""
        WHR = dbConn.WHERE_BETWEEN("B.TG003", ucDateFrom.Text, ucDateTo.Text)

        SQLPlant = ",STUFF((SELECT '; ' + B.UDF02 from COPTH  A left join COPTG B on B.TG001=A.TH001 and B.TG002=A.TH002 
                         where B.TG023 = 'Y' and B.UDF02<>'' and B.UDF02 is not null " & WHR & " and A.TH004=DEL_DATA.TH004
                          group by B.UDF02 ORDER BY B.UDF02 FOR XML PATH('')), 1, 1, '') PLANT "

        Dim SQLShipTime As String = ""
        If CbSumDelDate.Checked Then
            colName.Add("Time Ship" & VarIni.char8 & "TIME_SHIP" & VarIni.char8 & "0")
            SQLShipTime = ",STUFF((SELECT '; ' + B.UDF03 
                          from COPTH  A
                         left join COPTG B on B.TG001=A.TH001 and B.TG002=A.TH002 
                         where B.TG023 = 'Y' and B.UDF02<>'' and B.UDF02 is not null and B.TG003=DEL_DATA.TG003 and A.TH004=DEL_DATA.TH004
                          group by B.UDF03
                          ORDER BY B.UDF03
                          FOR XML PATH('')), 1, 1, '') TIME_SHIP "
        End If

        Dim SQLStock As String = ""


        'SQL = "select TG004,TH004, TH006, COPTH.UDF01" & fldSum & ",sum(isnull(TH008,0)) TH008 from COPTH left join COPTG on TG001=TH001 and TG002=TH002 left join COPMA on MA001=TG004 " &
        '      " where TG023 = 'Y' " & WHR &
        '      " group by TH004,TH006,TG004,COPTH.UDF01" & fldSum &
        '      " order by TG004,TH004,TH006,COPTH.UDF01" & fldSum
        WHR = ""
        WHR &= dbConn.WHERE_IN("COPTH.TH014", cblSaleType)
        WHR &= dbConn.WHERE_LIKE("COPTH.TH004", tbItem)
        WHR &= dbConn.WHERE_LIKE("COPTG.TG004", tbCust)
        WHR &= dbConn.WHERE_LIKE("COPTH.TH006", tbSpec)
        WHR &= dbConn.WHERE_LIKE("COPTH.TH015", tbSaleNo)
        WHR &= dbConn.WHERE_LIKE("COPTH.TH016", tbSaleSeq)
        WHR &= dbConn.WHERE_LIKE("COPTG.TG002", tbDelNo)
        WHR &= dbConn.WHERE_LIKE("COPTH.TH030", tbCustPO)
        WHR &= dbConn.WHERE_LIKE("COPTH.UDF01", tbCustWo)
        WHR &= dbConn.WHERE_BETWEEN("COPTG.TG003", ucDateFrom.Text, ucDateTo.Text)



        SQL = "select TG004,MA002,TH004, TH006,DEL_DATA.UDF01" & fldSum & ",TH008,INVMB.UDF04,INVMB.UDF51,
                floor(case when INVMB.UDF51=0 then 0 else TH008/INVMB.UDF51 end) FULL_BOX,INVMB.MB064,MC007,
                floor(case when INVMB.UDF51=0 then TH008 else  TH008%INVMB.UDF51 end) LAST_BOX,
                floor(case when INVMB.UDF51=0 then 0 else TH008/INVMB.UDF51 end)+floor(case when INVMB.UDF51=0 then case when TH008>0 then 1 else 0 end else case when TH008%INVMB.UDF51=0 then 0 else 1 end end)   ALL_BOX
                " & SQLPlant & SQLShipTime & "
                from (
                select TG004,TH004, TH006, COPTH.UDF01" & fldSum & ",sum(isnull(TH008,0)) TH008 from COPTH
                left join COPTG on TG001=TH001 and TG002=TH002
                where TG023 = 'Y' " & WHR & "
                group by TH004,TH006,TG004,COPTH.UDF01" & fldSum & "
            ) DEL_DATA
            left join INVMB on INVMB.MB001=TH004
            left join COPMA on MA001=TG004
            left join (select MC001,sum(MC007) MC007 from INVMC where MC002='2101' group by MC001)  INVMC on MC001=TH004
            order by TG004,TH004,TH006,UDF01 " & fldSum
        ucGvShow.showGridview(SQL, VarIni.ERP, colName, strSplit:=VarIni.char8)
    End Sub

    Sub DateSum()
        Dim colName As New ArrayList
        Dim SQL As String = ""
        Dim whr As String = dbConn.WHERE_BETWEEN("COPTG.TG003", ucDateFrom.text, ucDateTo.text)
        If CbDaysum.Checked Then 'sum days
            colName = New ArrayList From {
               "Deleviry Date" & VarIni.char8 & "TG003",
               "Sale Delivery" & VarIni.char8 & "TH001" & VarIni.char8 & "0",
               "Item to Ship" & VarIni.char8 & "TH004" & VarIni.char8 & "0",
               "Del Qty" & VarIni.char8 & "TH008" & VarIni.char8 & "0"
           }
            SQL = "select TG003,count(distinct TH001+TH002) TH001,count(distinct TH004 ) TH004,sum(TH008) TH008
                    from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 = 'Y' " & whr & "
                    group by TG003"

        Else 'sum doc
            colName = New ArrayList From {
                "Deleviry Date" & VarIni.char8 & "TG003",
                "Delivery Type" & VarIni.char8 & "TH001",
                "Delivery No" & VarIni.char8 & "TH002",
                "Cust" & VarIni.char8 & "TG004",
                "Cust Name" & VarIni.char8 & "MA002",
                "Item to Ship" & VarIni.char8 & "TH004" & VarIni.char8 & "0",
                "Del Qty" & VarIni.char8 & "TH008" & VarIni.char8 & "0"
            }
            SQL = "select TG003,TG004,TH001,TH002,TH004,TH008,MA002 from (
                    select TG003,TG004,TH001,TH002,count(distinct TH004 ) TH004,sum(TH008) TH008
                    from COPTH left join COPTG on TG001=TH001 and TG002=TH002 where TG023 = 'Y' " & whr & "
                    group by TG003,TG004,TH001,TH002
                    ) AA
                    left join COPMA on MA001=TG004
                    left join INVMB on INVMB.MB001=TH004
                    order by TG003,TH001,TH002,TG004"
        End If
        ucGvShow.showGridview(SQL, VarIni.ERP, colName, strSplit:=VarIni.char8)
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Select Case TabContainer1.ActiveTabIndex
            Case 0 'delivery sum
                DeliverySum()
            Case 1 'days um
                DateSum()
        End Select
        System.Threading.Thread.Sleep(1000)
        If ucGvShow.RowValue > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShow", "gridviewScrollgvShow();", True)
            btExport.Visible = True
        End If

    End Sub

    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        Dim expCont As New ExportImportControl
        expCont.Export("DeliveryPlanStatus" & Session(VarIni.UserName), ucGvShow.getGridview)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class