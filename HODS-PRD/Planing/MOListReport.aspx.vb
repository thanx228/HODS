'เป็นส่วนของการประกาศตัวแปรเพื่อใช้ในการติดต่อฐานข้อมูล
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class MOListReport
    Inherits System.Web.UI.Page
    Dim dbConn As New DataConnectControl
    'ประกาศตัวแปร dbConn เพื่อแทนค่าการติดต่อกับฐานข้อมูล
    Dim dtCont As New DataTableControl
    'ประกาศตัวแปร dtCont เพื่อแทนค่าการติดต่อในการใช้งาน DataTable
    Dim gvCont As New GridviewControl
    'ประกาศตัวแปร gvCont เพื่อแทนค่าการติดต่อ Gridview ที่ใช้ในการแสดงข้อมูลในตาราง
    Dim expCont As New ExportImportControl
    'ประกาศตัวแปร expCont เพื่อแทนค่าการติดต่อในการใช้งาน Export Import Excel
    Dim cblCont As New CheckBoxListControl
    'ประกาศตัวแปร cblCont เพื่อแทนค่าการติดต่อในการใช้งาน CheckBoxList
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            'ประกาศ SqlMoType เป็นค่าว่างเพื่อใช้เก็บค่าของ Query ที่ต้องการใช้มีการแสดงข้อมูลในรูปแบบของ CheckBoxlist
            'เป็นส่วนของชื่อ MO Type 
            Dim SqlMoType As String = String.Empty
            SqlMoType = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            cblCont.showCheckboxList(cblMoType, SqlMoType, VarIni.ERP, "MQ002", "MQ001", 4)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    'ส่วนของปุ่ม show report 
    'ถ้ามีการคลิกที่ปุ่มให้เข้าไปแสดงข้อมูลตามคำสั่งใน fuction showdata แสดงออกในรูปแบบของ gridview
    Protected Sub btshow_Click(sender As Object, e As EventArgs) Handles btshow.Click
        showdata(False)
    End Sub

    'ส่วนของปุ่ม Export Excel
    'ถ้ามีการคลิกที่ปุ่มให้เข้าไปแสดงข้อมูลตามคำสั่งใน fuction showdata แสดงออกในรูปแบบของ Excel
    Protected Sub btexcel_Click(sender As Object, e As EventArgs) Handles btexcel.Click
        showdata(True)
    End Sub

    Function showdata(Optional ByVal excel As Boolean = True) As DataTable
        Dim dtshow As New DataTable 'ประกาศตัวแปร dtshow เป็นค่า DAtaTable
        'HeadColcumn มีการประกาศเพื่อเก็บค่าของ Colcumn ในรูปแบบของ ArrayList 
        Dim HeadColcumn As New ArrayList From {
            (":MO Type"),
            (":MO No"),
            (":MO DATE"),
            (":ITEM"),
            (":Desc"),
            (":Spec"),
            (":Unit"),
            (":Plan Start Date"),
            (":Plan completed Date"),
            (":Actual Start Date"),
            (":Actual completed Date"),
            (":Delivery Date"),
            (":Plan Qty:2"),
            (":Completed Qty:2"),
            (":Scarp Qty:2"),
            (":MO Status"),
            (":App Status"),
            (":SO Type"),
            (":SO Number"),
            (":SO Seq"),
            (":SO Qty:2"),
            (":Cust Name"),
            (":Cust WO"),
            (":Cust Line"),
            (":Model")
        }
        dtshow = dtCont.setColDatatable(HeadColcumn)
        'dtshow ให้มีการแสดงค้าของชื่อ Colcumn ในรูปแบบของ datatable


        Dim dt As New DataTable
        Dim sql As String = String.Empty 'ประกาศตัวแปร sql เป็นค่าว่างเพื่อเก็บค่าของ Query ของชุดข้อมูลที่จะใช้ในการแสดง
        Dim whr As String = String.Empty 'ประกาศค่าตัวแปร whr เป็นค่าว่างเพื่อเก็บค่าของเงื่อนไขที่มีการเลือกข้อมูลที่ต้องให้แสดงผลตามเงื่อนไขหน้า UI

        whr = dbConn.WHERE_LIKE("MOCTA.TA006", txtitem) 'การระบุ เพื่อส่งค่าของ Item ผ่าน UI มาเป็นเงื่อนไขในการแสดงข้อมูลทีต้องการ
        whr &= dbConn.WHERE_LIKE("MOCTA.TA035", txtspec) 'การระบุ เพื่อส่งค่าของ spec ผ่าน UI มาเป็นเงื่อนไขในการแสดงข้อมูลทีต้องการ
        whr &= dbConn.WHERE_IN("TA001", cblMoType) 'การระบุ เพื่อส่งค่าของ Mo Type ผ่าน UI มาเป็นเงื่อนไขในการแสดงข้อมูลทีต้องการ

        'การระบุ เพื่อส่งค่าของ Mo status และ App status ผ่าน UI มาเป็นเงื่อนไขในการแสดงข้อมูลทีต้องการ

        whr &= dbConn.WHERE_EQUAL("MOCTA.TA013", ddlapptus)

        If ddlmotus.SelectedValue = "Y" Then
            whr &= dbConn.WHERE_IN("MOCTA.TA011", "Y,y",, True)
        ElseIf ddlmotus.SelectedValue = "N" Then
            whr &= dbConn.WHERE_IN("MOCTA.TA011", "'Y','y'", notIn:=True)
        Else
            whr &= dbConn.WHERE_EQUAL("MOCTA.TA011", ddlmotus)
        End If

        'If ddlmotus.SelectedValue = "Y" Then
        '    whr &= dbConn.WHERE_IN("MOCTA.TA011", "Y,y",, True)
        'ElseIf ddlmotus.SelectedValue = "N" Then
        '    whr &= dbConn.WHERE_IN("MOCTA.TA011", "'Y','y'", notIn:=True)
        'ElseIf ddlmotus.SelectedValue = "A" Then
        'Else
        '    whr &= dbConn.WHERE_IN("MOCTA.TA011", ddlmotus)
        'End If

        'การระบุ เพื่อส่งค่าของ date ผ่าน UI มาเป็นเงื่อนไขในการแสดงข้อมูลทีต้องการ
        whr &= dbConn.WHERE_DATE("MOCTA.TA003", sdate.Text, edate.Text)


        sql = "Select TA001 ,TA002 , TA003,TA006 , TA034 ,TA035 ,TA007 ,TA009,TA010,TA012,TA014,TA015 ,TA017 ,TA018 ,
            Case TA011 When '1' THEN  '1:Not Produced' 
            WHEN '2' THEN  '2:Issued' 
            WHEN '3' THEN  '3:Producing'  
            WHEN 'Y' THEN  'Y:Completed' 
            WHEN 'y' THEN 'y:Manual Completed'  ELSE  ''  END  TA011,
            Case TA013 When 'Y' THEN  'Y:Approved' 
            WHEN 'N' THEN  'N:Not Approved' 
            WHEN 'V' THEN 'Cancal'
            WHEN 'U' THEN 'U:Approve failed' ELSE '' END  TA013 
            ,TD013,TC001,TC002,TD003,TD008,MA002,
            COPTD.UDF02,COPTD.UDF03,COPTD.UDF04
            From MOCTA 
            LEFT JOIN COPTC ON TC001 = TA026 And TC002 = TA027 
            LEFT JOIN COPTD ON TA028 = TD003 And TD002 = TA027  And TD004 = TA006 
            LEFT JOIN COPMA ON MA001 = TC004 
            where 1=1 " & whr & "
            order by TA003, TA002"


        'Select  TA001 ,TA002 , TA003,TA006 , TA034 ,TA035 ,TA007 ,TA009,TA010,TA012,TA014,TA015 ,TA017 ,TA018 ,
        'Case TA011 When '1' THEN  '1:Not Produced' 
        'WHEN '2' THEN  '2:Issued' 
        'WHEN '3' THEN  '3:Producing'  
        'WHEN 'Y' THEN  'Y:Completed' 
        'WHEN 'y' THEN 'y:Manual Completed'  ELSE  ''  END  TA011,
        'Case TA013 When 'Y' THEN  'Y:Approved' 
        'WHEN 'N' THEN  'N:Not Approved' 
        'WHEN 'V' THEN 'Cancal'
        'WHEN 'U' THEN 'U:Approve failed' ELSE '' END  TA013 
        ',TD013,TC001,TC002,TD003,TC031,
        'MA002,COPTD.UDF02,COPTD.UDF03,COPTD.UDF04
        'From MOCTA 
        'Left Join COPTC ON TC001 = TA026 And TC002 = TA027 
        'Left Join COPTD ON TA028 = TD003 And TD002 = TA027  And TD004 = TA006 
        'Left Join COPMA ON MA001 = TC004 
        'WHERE TA003 = '20210127' 
        '--And TA001 = '5102'
        'order by TA003, TA002


        dt = dbConn.Query(sql, VarIni.ERP, dbConn.WhoCalledMe())
        ' dt เป็นคำสั่งที่ใช้ดูข้อมูลที่ต้องการให้แสดงข้อมูล (Datatable)
        Dim count As Integer = dt.Rows.Count
        'ประกาศตัวแปร count เป็นค่าของตัวเลข ใช้ในการนับแถวของข้อมูลที่ต้องการแสดง
        'มีการเขียนเงื่อนไขเพื่อกำหนดจำนวนในการแสดงข้อมูลว่าถ้าข้อมูลมีจำนวนมากกว่า 500 แถว ให้ show message ว่าให้เลือกเป็น export excel แทน
        If excel = False Then
            If count > 500 Then
                show_message.ShowMessage(Page, "จำนวนข้อมูลมากกว่า 500 แถว (" & count & " rows) \nโปรดเลือก export excel. ", UpdatePanel1)
                Return dtshow
                Exit Function
            End If
        End If


        'เป็นส่วนของการประกาศค่าของข้อมูลที่ต้องการให้แสดงในแต่ละแถวว่าต้องการให้มีการแสดงข้อมูลใดบ้างในแต่ละ Colcumn
        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim Y As New Hashtable From {
                    {"MO Type", .Number("TA001")},
                    {"MO No", .Number("TA002")},
                    {"MO DATE", .Text("TA003")},
                    {"ITEM", .Text("TA006")},
                    {"Desc", .Text("TA034")},
                    {"Spec", .Text("TA035")},
                    {"Unit", .Text("TA007")},
                    {"Plan Start Date", .Text("TA009")},
                    {"Plan completed Date", .Text("TA010")},
                    {"Actual Start Date", .Text("TA012")},
                    {"Actual completed Date", .Text("TA014")},
                    {"Delivery Date", .Text("TD013")}, 'Delivery Date BY SALE ORDER
                    {"Plan Qty", .Number("TA015")},
                    {"Completed Qty", .Number("TA017")},
                    {"Scarp Qty", .Number("TA018")},
                    {"MO Status", .Text("TA011")},
                    {"App Status", .Text("TA013")},
                    {"SO Type", .Text("TC001")},
                    {"SO Number", .Text("TC002")},
                    {"SO Seq", .Text("TD003")},
                    {"SO Qty", .Number("TD008")},
                    {"Cust Name", .Text("MA002")},
                    {"Cust WO", .Text("UDF02")},
                    {"Cust Line", .Text("UDF03")},
                    {"Model", .Text("UDF04")}
                }
                dtCont.addDataRow(dtshow, Y)
            End With
        Next

        If excel = True Then
            expCont.Export("MO List Report", dtshow)
        Else
            gvCont.GridviewInitial(gvshow, HeadColcumn)
            gvCont.ShowGridView(gvshow, dtshow)
            CountRow.RowCount = count
        End If
        'ส่วนของเงื่อนไข ถ้ามีต้องการ exprot excel ให้มีการแสดงชื่อ excel เป็น MO List Report 
        'ถ้าไม่มีการ exprot excel ให้แสดงข้อมูลในรูปแบบของ gridview
        Return dtshow
    End Function

    'ส่วนของปุ่ม Clear เป็นส่วนของปุ่มที่เมื่อมีการคลิกจะมีการ clear ข้อมูลในส่วนของหน้า UI ที่มีการระบุข้อมูลที่ต้องการค้น 
    Protected Sub clear_Click(sender As Object, e As EventArgs) Handles clear.Click
        txtitem.Text = String.Empty
        txtspec.Text = String.Empty
        sdate.Text = Nothing
        edate.Text = Nothing
        ddlapptus.ClearSelection()
        ddlmotus.ClearSelection()
        gvshow.DataSource = Nothing
        gvshow.DataBind()
        CountRow.RowCount = Nothing

    End Sub

End Class