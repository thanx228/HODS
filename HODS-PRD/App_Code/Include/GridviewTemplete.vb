

Public Class GridviewTemplete
    Implements System.Web.UI.ITemplate

    Private templateType As DataControlRowType
    Private columnName As String
    Private objectType As String
    Private objectID As String
    Private ImgageURL As String = ""
    Private CommandName As String = ""
    Private onClintClick As String = ""
    Private cssClass As String = ""

    Sub New(ByVal type As DataControlRowType, ByVal colname As String, Optional ByVal objID As String = "", _
            Optional ByVal objType As String = "Lable", Optional ByVal css As String = "", _
            Optional ByVal ImgURL As String = "", Optional ByVal ComName As String = "", _
            Optional ByVal onClick As String = "")

        templateType = type
        columnName = colname
        objectID = objID
        objectType = objType
        cssClass = css
        ImgageURL = ImgURL
        CommandName = ComName
        onClintClick = onClick

    End Sub

    Sub InstantiateIn(ByVal container As System.Web.UI.Control) _
      Implements ITemplate.InstantiateIn

        ' Create the content for the different row types.
        Select Case templateType
            Case DataControlRowType.Header
                ' Create the controls to put in the header
                ' section and set their properties.
                Dim lc As New Literal
                lc.Text = "<b>" & columnName & "</b>"
                ' Add the controls to the Controls collection
                ' of the container.
                container.Controls.Add(lc)
            Case DataControlRowType.DataRow
                'Dim ph As New PlaceHolder
                'Dim objTest As Object = New Object
                Select Case objectType
                    Case "Button"
                        Dim obj As Button = New Button
                        obj.ID = objectID
                        'obj = CType(obj, Button)
                        obj.Text = columnName
                        obj.CommandName = CommandName
                        obj.OnClientClick = onClintClick
                        container.Controls.Add(obj)
                    Case "ImageButton"
                        Dim obj As ImageButton = New ImageButton
                        obj.ID = objectID
                        obj.ImageUrl = ImgageURL
                        obj.CommandName = CommandName
                        obj.OnClientClick = ""
                        container.Controls.Add(obj)
                    Case "HyperLink"
                        Dim obj As HyperLink = New HyperLink
                        obj.ID = objectID
                        obj.Text = columnName
                        obj.Target = "_blank"
                        container.Controls.Add(obj)
                    Case "CheckBox"
                        Dim obj As CheckBox = New CheckBox
                        obj.ID = objectID
                        container.Controls.Add(obj)
                    Case "TextBox"
                        Dim obj As TextBox = New TextBox
                        obj.ID = objectID
                        obj.Width = 80
                        obj.CssClass = cssClass
                        container.Controls.Add(obj)
                    Case "Image"
                        Dim obj As Image = New Image
                        obj.ID = objectID
                        container.Controls.Add(obj)
                    Case "Lable"
                        Dim obj As Label = New Label
                        obj.ID = objectID
                        container.Controls.Add(obj)
                    Case "DropDownList"
                        Dim obj As DropDownList = New DropDownList
                        obj.ID = objectID
                        container.Controls.Add(obj)
                End Select


                ' Create the controls to put in a data row
                ' section and set their properties.
                'Dim firstName As New Label
                'Dim lastName As New Label

                'Dim spacer = New Literal
                'spacer.Text = " "

                ' To support data binding, register the event-handling methods
                ' to perform the data binding. Each control needs its own event
                ' handler.
                'AddHandler firstName.DataBinding, AddressOf FirstName_DataBinding
                'AddHandler lastName.DataBinding, AddressOf LastName_DataBinding

                ' Add the controls to the Controls collection
                ' of the container.
                'container.Controls.Add(firstName)
                'container.Controls.Add(spacer)
                'container.Controls.Add(lastName)

                ' Insert cases to create the content for the other 
                ' row types, if desired.

            Case Else

                ' Insert code to handle unexpected values. 

        End Select

    End Sub
    'Private Sub FirstName_DataBinding(ByVal sender As Object, ByVal e As EventArgs)

    '    ' Get the Label control to bind the value. The Label control
    '    ' is contained in the object that raised the DataBinding 
    '    ' event (the sender parameter).
    '    Dim l As Label = CType(sender, Label)

    '    ' Get the GridViewRow object that contains the Label control. 
    '    Dim row As GridViewRow = CType(l.NamingContainer, GridViewRow)

    '    ' Get the field value from the GridViewRow object and 
    '    ' assign it to the Text property of the Label control.
    '    l.Text = DataBinder.Eval(row.DataItem, "au_fname").ToString()

    'End Sub

    'Private Sub LastName_DataBinding(ByVal sender As Object, ByVal e As EventArgs)

    '    ' Get the Label control to bind the value. The Label control
    '    ' is contained in the object that raised the DataBinding 
    '    ' event (the sender parameter).
    '    Dim l As Label = CType(sender, Label)

    '    ' Get the GridViewRow object that contains the Label control.
    '    Dim row As GridViewRow = CType(l.NamingContainer, GridViewRow)

    '    ' Get the field value from the GridViewRow object and 
    '    ' assign it to the Text property of the Label control.
    '    l.Text = DataBinder.Eval(row.DataItem, "au_lname").ToString()

    'End Sub
End Class
