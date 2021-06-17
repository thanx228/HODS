
Public Class show_message

    Shared Sub ShowMessage(ByVal Page As System.Web.UI.Page, ByVal Message As String, ByRef UpdatePanel1 As UpdatePanel)
        Dim JavaScript As String = ""
        JavaScript = "<SCRIPT Language='JavaScript'>"
        JavaScript += "window.alert('"
        JavaScript += Message & "');"
        JavaScript += "</SCRIPT>"
        ScriptManager.RegisterStartupScript(UpdatePanel1.Page, GetType(String), "ShowMessage", JavaScript, False)
    End Sub
    Shared Sub ConfirmMsg(ByVal Page As System.Web.UI.Page, ByVal Message As String, ByRef UpdatePanel1 As UpdatePanel)
        ScriptManager.RegisterStartupScript(UpdatePanel1.Page, GetType(String), "ConfirmMsg", "<SCRIPT Language='JavaScript'>window.confirm('" & Message & "');</SCRIPT>", True)
    End Sub

    'Shared Sub CloseWindow(ByVal Page As System.Web.UI.Page, ByRef UpdatePanel1 As UpdatePanel)
    '    ScriptManager.RegisterStartupScript(UpdatePanel1.Page, GetType(String), "CloseWin", "<SCRIPT Language='JavaScript'>window.close();</SCRIPT>", True)
    'End Sub


End Class
