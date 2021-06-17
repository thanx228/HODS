Imports System.Globalization

Public Class noBomItemBlock
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            Dim listData = New Hashtable
            listData.Add("1", " No Bom ")
            listData.Add("2", " Item Block ")
            ddlType.DataSource = listData
            ddlType.DataValueField = "Key"
            ddlType.DataTextField = "Value"
            ddlType.DataBind()
            listData.Clear()
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click

        Dim rTye As String = ddlType.Text, SQL As String = "", saleType As String = ddlSaleType.Text, whr As String = ""

        If rTye = 1 Then 'no bom
            If saleType <> "ALL" Or saleType = "" Then
                whr = " and COPTD.TD001='" & saleType & "'"
            End If
            SQL = " select COPTD.TD004 as JP_PART,COPTD.TD006 as JP_SPEC from COPTD " & _
                  " left join BOMMD on BOMMD.MD001=COPTD.TD004 " & _
                  " where COPTD.TD016='N' and COPTD.TD021='Y' " & whr & " and substring(COPTD.TD004,1,3) in ('802','803') " & _
                  " group by COPTD.TD004,COPTD.TD006 having COUNT(BOMMD.MD001)=0  " & _
                  " order by COPTD.TD004 "
        Else 'item block
            If saleType <> "ALL" Or saleType = "" Then
                whr = " and TD001='" & saleType & "'"
            End If
            SQL = " select BOMMD.MD001 as JP_PART,I2.MB003 as JP_SPEC,BOMMD.MD003 as MAT_CODE,I1.MB003 as MAT_SPEC from BOMMD " & _
                   " left join INVMB I1 on I1.MB001=BOMMD.MD003 " & _
                   " left join INVMB I2 on I2.MB001=BOMMD.MD001 " & _
                   " where MD001 in (select TD004 from COPTD where TD016='N' and TD021='Y' " & whr & " and substring(TD004,1,3) in ('802','803') group by TD004) and I1.MB109 <>'Y'  " & _
                   " group by BOMMD.MD001,BOMMD.MD003,I1.MB003,I2.MB003  " & _
                   " order by BOMMD.MD001,BOMMD.MD003"
        End If
        ControlForm.ShowGridView(gvShowData, SQL, Conn_SQL.ERP_ConnectionString)
        lbCnt.Text = gvShowData.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShowData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShowData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
            If Not IsNothing(hplDetail) Then

                Dim name As String = "", code As String = ""
                If Not IsDBNull(e.Row.DataItem("JP_SPEC")) Then
                    name = e.Row.DataItem("JP_SPEC")
                End If
                If Not IsDBNull(e.Row.DataItem("JP_PART")) Then
                    code = e.Row.DataItem("JP_PART")
                End If
                hplDetail.NavigateUrl = "noBomItemBlockPop.aspx?height=150&width=350&code=" & code & "&name=" & name
                hplDetail.Attributes.Add("title", name)
            End If
        End If
    End Sub
End Class