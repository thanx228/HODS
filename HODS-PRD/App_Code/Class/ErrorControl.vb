Namespace DataControl
    Public Class ErrorControl
        Sub GetPageError(path As String, Func As String, sql As String, strError As String)
            HttpContext.Current.Session("Path") = path
            HttpContext.Current.Session("PathVB") = path & ".vb"
            HttpContext.Current.Session("FC") = Func
            HttpContext.Current.Session("SQL") = sql
            HttpContext.Current.Session("Error") = strError
            HttpContext.Current.Response.Redirect("~/HttpErrorPage.aspx")
        End Sub
    End Class
End Namespace