Public Class MessageAlert
    Inherits System.Web.UI.Page
    Public Shared JavaScript As String
    Public Shared cstype As Type
    Public Shared cs As ClientScriptManager

    Public Shared Sub Show(Page As System.Web.UI.Page, ByVal Message As String)
        Page.ClientScript.RegisterStartupScript(Page.[GetType](), "MessageBox", "<script language='javascript'>alert('" + Message + "');</script>")
    End Sub
    Public Shared Function javaMsg(ByVal message As String) As String
        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")
        Return sb.ToString()
    End Function
    Public Shared Function ShowBox(page As Page, msg As String)
        Dim sb As New System.Text.StringBuilder()
        sb.Append("<script type = 'text/javascript'>")
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(msg)
        sb.Append("')};")
        sb.Append("</script>")
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", sb.ToString())
        Return True
    End Function

    Public Shared Sub ShowAlertOkCancel(page As Page, message As String)
        'message = "Do you want to Submit?"
        Dim sb As New System.Text.StringBuilder()
        sb.Append("return confirm('")
        sb.Append(message)
        sb.Append("');")
        page.ClientScript.RegisterOnSubmitStatement(page.GetType(), "alert", sb.ToString())
    End Sub

    Public Shared Sub ShowAlertOK(page As Page, message As String)
        'message = "Order Placed Successfully."
        Dim sb As New System.Text.StringBuilder()
        sb.Append("<script type = 'text/javascript'>")
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")
        sb.Append("</script>")
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", sb.ToString())
    End Sub

    Public Shared Sub ShwMsgConfrim(page As Page, msg As String)
        'msg = "Are you sure?"
        Dim CSM As ClientScriptManager = page.ClientScript
        If Not ReturnValue() Then
            Dim strconfirm As String = "<script>if(!window.confirm('" & msg & "')){window.location.href='#'}</script>"
            CSM.RegisterClientScriptBlock(page.[GetType](), "Confirm", strconfirm, False)
        End If
    End Sub
    Private Shared Function ReturnValue() As Boolean
        Return False
    End Function

End Class
