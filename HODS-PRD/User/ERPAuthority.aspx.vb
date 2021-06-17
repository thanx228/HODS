Public Class ERPAuthority
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select ME001,rtrim(ME001)+'-'+ME002 as ME002 from ADMME order by ME001"
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            ControlForm.showDDL(ddlGroup, SQL, "ME002", "ME001", True, Conn_SQL.ERP_ConnectionString)
            SQL = "select ME001,rtrim(ME001)+'-'+ME002 as ME002 from CMSME order by ME001"
            ControlForm.showDDL(ddlDept, SQL, "ME002", "ME001", True, Conn_SQL.ERP_ConnectionString)
            'btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHR As String = ""

        WHR = WHR & Conn_SQL.Where("MF004", ddlGroup)
        WHR = WHR & Conn_SQL.Where("MF007", ddlDept)
        WHR = WHR & Conn_SQL.Where("MG002", tbProcedure)
        WHR = WHR & Conn_SQL.Where("SUBSTRING(MG006,9,1)", cbNew)
        WHR = WHR & Conn_SQL.Where("SUBSTRING(MG006,1,1)", cbQuery)
        WHR = WHR & Conn_SQL.Where("SUBSTRING(MG006,2,1)", cbAlter)
        WHR = WHR & Conn_SQL.Where("SUBSTRING(MG006,3,1)", cbDelete)
        WHR = WHR & Conn_SQL.Where("SUBSTRING(MG006,4,1)", cbApprove)
        WHR = WHR & Conn_SQL.Where("SUBSTRING(MG006,5,1)", cbDeApp)
        WHR = WHR & Conn_SQL.Where("SUBSTRING(MG006,6,1)", cbExport)

        SQL = " select MF004,isnull(ADM.ME002,'')as GNAME,MF001,MF002,MF007,MG002,MB011,isnull(CMS.ME002,'') as DNAME,isnull(MA005,'') as MA005," & _
              " SUBSTRING(MG006,9,1) as 'A', SUBSTRING(MG006,1,1) as 'B',SUBSTRING(MG006,2,1) as 'C'," & _
              " SUBSTRING(MG006,3,1) as 'D',SUBSTRING(MG006,4,1) as 'E'," & _
              " SUBSTRING(MG006,5,1) as 'F' ,SUBSTRING(MG006,6,1) as 'G' from ADMMG " & _
              " left join ADMMF on MF001=MG001 and MF004=MG009 " & _
              " left join DSCSYS80.dbo.DSCMA on MA001=MF001 " & _
              " left join CMSME CMS on CMS.ME001=MF007 " & _
              " left join ADMME ADM on ADM.ME001=MF004 " & _
              " left join DSCSYS80.dbo.ADMMB on MB001=MG002 " & _
              " where 1=1 " & WHR & _
              " order by MF007,MF004,MF001 " 'MF001<>'**********' 
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        lbCount.Text = gvShow.Rows.Count
    End Sub

    'Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
    '    With e.Row
    '        If .RowType = DataControlRowType.DataRow Then

    '            Dim hplDetail As HyperLink = CType(.FindControl("hlShow"), HyperLink)
    '            Dim code As String = .DataItem("MF004").ToString.Replace("Null", "")
    '            Dim spec As String = .DataItem("GNAME").ToString.Replace("Null", "")
    '            Dim user As String = .DataItem("MF001").ToString.Replace("Null", "")

    '            If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("MF004")) And Not IsDBNull(.DataItem("MF001")) Then

    '                Dim link As String = "&grpCode=" & code
    '                link = link & "&User=" & user
    '                hplDetail.NavigateUrl = "ERPUserPopup.aspx?height=150&width=350" & link
    '                hplDetail.Attributes.Add("title", user & "-" & code & " - " & spec)
    '            End If
    '        End If
    '    End With
    'End Sub
End Class