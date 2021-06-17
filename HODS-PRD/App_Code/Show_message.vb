Public Class show_message
    '*********************************************************************   
    '   
    ' CloseWindow() 函式   
    '   
    ' 關閉視窗
    '   
    '*********************************************************************  
    Shared Sub CloseWindow(ByVal Page As System.Web.UI.Page)
        Dim JavaScript As String = ""
        Dim cstype As Type = Page.GetType()
        Dim cs As ClientScriptManager = Page.ClientScript
        JavaScript = "<SCRIPT Language='JavaScript'>"
        JavaScript += "window.close();"
        JavaScript += "</SCRIPT>"
        'Dim csname1 As String = "PopupScript"
        'If (Not cs.IsStartupScriptRegistered(cstype, csname1)) Then
        '    cs.RegisterStartupScript(cstype, csname1, JavaScript, True)
        'End If
        Dim csname2 As String = "ButtonClickScript"
        If (Not cs.IsClientScriptBlockRegistered(cstype, csname2)) Then
            cs.RegisterClientScriptBlock(cstype, csname2, JavaScript, False)
        End If
    End Sub
    '*********************************************************************   
    '   
    ' ShowMessage() 函式   
    '   
    ' 傳入欲顯示之字串，以對話視窗顯示出   
    '   
    '*********************************************************************  
    Shared Sub ShowMessage(ByVal Page As System.Web.UI.Page, ByVal Message As String, ByRef UpdatePanel1 As UpdatePanel)
        Dim JavaScript As String = ""
        JavaScript = "<SCRIPT Language='JavaScript'>"
        JavaScript += "window.alert('"
        JavaScript += Message & "');"
        JavaScript += "</SCRIPT>"
        ScriptManager.RegisterStartupScript(UpdatePanel1.Page, GetType(String), "ShowMessage", JavaScript, False)
    End Sub

    Shared Sub ShowMessage_1(ByVal Page As System.Web.UI.Page, ByVal Message As String, ByVal Redirect As String)
        Dim JavaScript As String = ""
        Dim cstype As Type = Page.GetType()
        Dim cs As ClientScriptManager = Page.ClientScript
        JavaScript = "<SCRIPT Language='JavaScript'>"
        JavaScript += "window.alert('"
        JavaScript += Message & "');"
        If Redirect = "" Then
            JavaScript += "window.opener=null;"
            JavaScript += "window.close();"
        Else
            JavaScript += "document.location='" & Redirect & "';"
        End If
        JavaScript += "</SCRIPT>"
        Dim csname2 As String = "ButtonClickScript"
        If (Not cs.IsClientScriptBlockRegistered(cstype, csname2)) Then
            cs.RegisterClientScriptBlock(cstype, csname2, JavaScript, False)
        End If
    End Sub

    '*********************************************************************   
    '   
    ' ShowMessage_Redirect() 函式   
    '   
    ' 傳入欲顯示之字串，以對話視窗顯示出後轉址到所指定之位置   
    '  
    '   
    '*********************************************************************  

    Shared Sub ShowMessage_Redirect(ByVal Page As System.Web.UI.Page, ByVal Message As String, ByVal Redirect As String)
        Dim JavaScript As String = ""
        Dim cstype As Type = Page.GetType()
        Dim cs As ClientScriptManager = Page.ClientScript
        JavaScript = "<SCRIPT Language='JavaScript'>"
        JavaScript += "window.alert('"
        JavaScript += Message & "');"
        JavaScript += "document.location='" & Redirect & "';"
        JavaScript += "</SCRIPT>"
        Dim csname2 As String = "ButtonClickScript"
        If (Not cs.IsClientScriptBlockRegistered(cstype, csname2)) Then
            cs.RegisterClientScriptBlock(cstype, csname2, JavaScript, False)
        End If
    End Sub

    '*********************************************************************   
    '   
    ' ShowMessage_bace_close() 函式   
    '   
    ' 公用函式，傳入欲顯示之字串，以對話視窗顯示出後   
    '   
    ' GoPrevPage=True：回上一頁   
    ' GoPrevPage=False：關閉視窗   
    '   
    '********************************************************************* 


    Shared Sub ShowMessage_back_close(ByVal Page As System.Web.UI.Page, ByVal Message As String, ByVal GoPrevPage As Boolean)
        Dim JavaScript As String = ""
        Dim cstype As Type = Page.GetType()
        Dim cs As ClientScriptManager = Page.ClientScript
        JavaScript = "<SCRIPT Language='JavaScript'>"
        JavaScript += "window.alert('"
        JavaScript += Message & "');"
        If GoPrevPage = False Then
            JavaScript += "window.opener=null;"
            JavaScript += "window.close();"
        Else
            JavaScript += "history.go(-1);"
        End If
        JavaScript += "</SCRIPT>"
        Dim csname2 As String = "ButtonClickScript"
        If (Not cs.IsClientScriptBlockRegistered(cstype, csname2)) Then
            cs.RegisterClientScriptBlock(cstype, csname2, JavaScript, False)
        End If
    End Sub

    '*********************************************************************   
    '   
    ' sure_goon() 函式   
    '   
    ' 傳入欲顯示之字串，以對話視窗顯示出 
    ' 確定是否的繼續
    '   
    '*********************************************************************  
    Shared Sub sure_goon(ByVal Page As System.Web.UI.Page, ByVal Message As String)
        Dim JavaScript As String = ""
        Dim cstype As Type = Page.GetType()
        Dim cs As ClientScriptManager = Page.ClientScript
        JavaScript = "<SCRIPT Language='JavaScript'>"
        JavaScript += "function sure_go_on(){"
        JavaScript += "if (confirm('"
        JavaScript += Message
        JavaScript += "')){"
        JavaScript += "return true;}"
        JavaScript += "return false;}"
        JavaScript += "</SCRIPT>"
        Dim csname2 As String = "ButtonClickScript"
        If (Not cs.IsClientScriptBlockRegistered(cstype, csname2)) Then
            cs.RegisterStartupScript(cstype, csname2, JavaScript, False)
        End If
    End Sub

    '*********************************************************************   
    '   
    ' check_keyin_null() 函式   
    '   
    ' 公用函式，check 每一個form 裡面的值是否正確
    ' filed 用","分開form裡面的每一個欄位名稱
    ' Message 用","分開，錯誤顯示的字串
    ' check_type 用","分開，0為禁止空值 1為需為數字 其他為字串長度限制
    ' limit_value 用","分開，限制的數字   
    '
    ' GoPrevPage=True：回上一頁   
    ' GoPrevPage=False：關閉視窗   
    '   
    '********************************************************************* 
    Shared Sub check_keyin_null(ByVal Page As System.Web.UI.Page, ByVal filed As String, ByVal Message As String, ByVal check_type As String, ByVal limit_value As String)
        Dim JavaScript As String = ""
        Dim cstype As Type = Page.GetType()
        Dim cs As ClientScriptManager = Page.ClientScript
        Dim filedArray() As String
        Dim messageArray() As String
        Dim typeArray() As String
        Dim limitArray() As String
        filedArray = Split(filed, ",", -1, 1)
        messageArray = Split(Message, ",", -1, 1)
        typeArray = Split(check_type, ",", -1, 1)
        limitArray = Split(limit_value, ",", -1, 1)

        JavaScript = "<SCRIPT Language='JavaScript'>"
        JavaScript += "function check_keyin_null(){"
        For l_count As Integer = 0 To UBound(filedArray, 1)
            JavaScript = JavaScript & " if ((0==1) "
            For k_count As Integer = 1 To Len(typeArray(l_count))
                Select Case Mid(typeArray(l_count), k_count, 1)
                    Case 0
                        JavaScript = JavaScript & " || document.Form1." & filedArray(l_count) & ".value==''"
                    Case 1
                        JavaScript = JavaScript & " || (isNaN(document.Form1." & filedArray(l_count) & ".value))"
                    Case Else
                        JavaScript = JavaScript & " || document.Form1." & filedArray(l_count) & ".value.length>" & limitArray(l_count)
                End Select
            Next
            JavaScript += ") {"
            JavaScript = JavaScript & "alert('" & messageArray(l_count) & " error!!');"
            JavaScript = JavaScript & "document.Form1." & filedArray(l_count) & ".focus();"
            JavaScript += "return false;"
            JavaScript += "	}"
        Next
        JavaScript += "}</SCRIPT>"
        Dim csname2 As String = "ButtonClickScript"
        If (Not cs.IsClientScriptBlockRegistered(cstype, csname2)) Then
            cs.RegisterClientScriptBlock(cstype, csname2, JavaScript, False)
        End If
    End Sub

    'Shared Sub ShowMessage(ByVal Page As System.Web.UI.Page, ByVal Message As String, ByRef UpdatePanel1 As UpdatePanel)
    '    Dim JavaScript As String = ""
    '    JavaScript = "<SCRIPT Language='JavaScript'>"
    '    JavaScript += "window.alert('"
    '    JavaScript += Message & "');"
    '    JavaScript += "</SCRIPT>"
    '    ScriptManager.RegisterStartupScript(UpdatePanel1.Page, GetType(String), "ShowMessage", JavaScript, False)
    'End Sub

    'Shared Sub ConfirmMsg(ByVal Page As System.Web.UI.Page, ByVal Message As String, ByRef UpdatePanel1 As UpdatePanel)
    '    ScriptManager.RegisterStartupScript(UpdatePanel1.Page, GetType(String), "ConfirmMsg", "<SCRIPT Language='JavaScript'>window.confirm('" & Message & "');</SCRIPT>", True)
    'End Sub

End Class
