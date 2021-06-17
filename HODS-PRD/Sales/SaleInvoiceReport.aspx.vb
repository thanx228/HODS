Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class SaleInvoiceReport
    Inherits System.Web.UI.Page

    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvConn As New GridviewControl
    Dim expConn As New ExportImportControl
    Dim chkCont As New CheckBoxListControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = String.Empty Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            showCblType(cblInvType, "MQ003 = '61'")
            showCblType(cblSourceType, "MQ003 in ('23','24','C4')")
        End If

    End Sub

    Private Sub showCblType(cbl As CheckBoxList, whr As String)
        Dim SQL As String = "select LTRIM(RTRIM(MQ001)) MQ001 , LTRIM(RTRIM(MQ001)) +' : '+ LTRIM(RTRIM(MQ002)) MQ002 from CMSMQ where " & whr & " order by MQ001"
        chkCont.showCheckboxList(cbl, SQL, VarIni.ERP, "MQ002", "MQ001", 6)
    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        showData(False)
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        showData(True)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub showData(excel As Boolean)

        Dim dtShow As New DataTable
        Dim ArlHead As New ArrayList From {
        ":Order Date",
        ":Order No.",
        ":Invoice Date",
        ":Source Order",
        ":Customer Code",
        ":Customer Name",
        ":Plant",
        ":Item",
        ":Spec",
        ":Invoice Quantity:2",
        ":Invoice Price:2",
        ":Amount Un-include Tax(O/C):2",
        ":Cust PO",
        ":Time Ship (Head)",
        ":Gen Barcode"
        }
        dtShow = dtCont.setColDatatable(ArlHead)


        Dim WHR As String = String.Empty
        WHR = dbConn.WHERE_IN("TA001", cblInvType)
        WHR &= dbConn.WHERE_LIKE("TA002", tbInvoice.Text.ToUpper)
        WHR &= dbConn.WHERE_IN("TB005", cblSourceType)
        WHR &= dbConn.WHERE_LIKE("TB006", tbSourceNo.Text.ToUpper)
        WHR &= dbConn.WHERE_LIKE("TA004", tbCustomer.Text.ToUpper)
        WHR &= dbConn.WHERE_LIKE("CUST_PO", tbCustPO.Text.ToUpper)
        WHR &= dbConn.WHERE_LIKE("ITEM", tbItem.Text.ToUpper)
        WHR &= dbConn.WHERE_LIKE("SPEC", tbSpec.Text.ToUpper)
        WHR &= dbConn.WHERE_BETWEEN(If(ddlSearchDate.SelectedValue = "0", "TA038", "TA003"), ucDateFrom.Text, ucDateTo.Text)
        If WHR = String.Empty Then
            show_message.ShowMessage(Page, "กรุณากรอกข้อมูล.", UpdatePanel1)
            Exit Sub
        End If

        Dim SQL As String = String.Empty
        SQL = "
        select * from 

        (select 
        TB003 SEQ , TA038 , TA038 ORDER_DATE , LTRIM(RTRIM(TA001)) TA001 , LTRIM(RTRIM(TA002)) TA002 , TA003 , TA003 INVOICE_DATE, 
        TB005 , TB006 , TB007 ,
        TA004 , MA002 CUST_NAME , ACRTA.UDF06 PLANT , TB039 ITEM, TB041 SPEC ,
        TB022 INVOICE_QTY , TB023 INVOICE_PRICE , (TB019 + TB020) UN_IN_TAX_OC , TB048 CUST_PO , 
        ACRTA.UDF08 TIME_SHIP , ACRTB.UDF01 GEN_BARCODE
        from ACRTB 
        left join ACRTA on ACRTA.COMPANY = ACRTB.COMPANY and ACRTA.TA001 = ACRTB.TB001 and ACRTA.TA002 = ACRTB.TB002
        left join COPMA on TA004 = COPMA.MA001 and COPMA.COMPANY = 'HOOTAI'
        where ACRTB.COMPANY = 'HOOTHAI'
        ) as tbmain

        where 1=1 "
        SQL &= WHR & vbCrLf
        SQL &= dbConn.getOrderBy("TA038,TA001,TA002,SEQ")


        Dim dt As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe())
        Dim Count As Integer = dt.Rows.Count

        If excel = False Then
            If Count > VarIni.LimitGridView Then
                show_message.ShowMessage(Page, "ข้อมูลมากกว่า 500 Rows (" & Count & " Rows)\nกรุณาเลือก Excel.", UpdatePanel1)
                gvShow.DataSource = Nothing
                gvShow.DataBind()
                ucCountRow.RowCount = Nothing
                Exit Sub
            End If
        End If

        For Each dr As DataRow In dt.Rows
            With New DataRowControl(dr)
                Dim Hash As New Hashtable From {
                {"Order Date", .Text("ORDER_DATE")},
                {"Order No.", .Text("TA001") & "-" & .Text("TA002")},
                {"Invoice Date", .Text("INVOICE_DATE")},
                {"Source Order", .Text("TB005") & "-" & .Text("TB006")},
                {"Customer Code", .Text("TA004")},
                {"Customer Name", .Text("CUST_NAME")},
                {"Plant", .Text("PLANT")},
                {"Item", .Text("ITEM")},
                {"Spec", .Text("SPEC")},
                {"Invoice Quantity", .Number("INVOICE_QTY")},
                {"Invoice Price", .Number("INVOICE_PRICE")},
                {"Amount Un-include Tax(O/C)", .Number("UN_IN_TAX_OC")},
                {"Cust PO", .Text("CUST_PO")},
                {"Time Ship (Head)", .Text("TIME_SHIP")},
                {"Gen Barcode", .Text("GEN_BARCODE")}
                }
                dtCont.addDataRow(dtShow, Hash)
            End With
        Next

        If excel = True Then
            expConn.Export("Sale Invoice Report", dtShow)
            gvShow.DataSource = Nothing
            gvShow.DataBind()
            ucCountRow.RowCount = Nothing
        Else
            gvConn.ShowGridView(gvShow, dtShow)
            ucCountRow.RowCount = Count
        End If


    End Sub

End Class