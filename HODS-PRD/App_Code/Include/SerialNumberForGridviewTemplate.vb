Imports Microsoft.VisualBasic

Public Class SerialNumberForGridviewTemplate
    Implements System.Web.UI.ITemplate

    Dim templateItemID As String
    Dim tempItemType As Integer
    Dim tempImageUrl As String

    Sub New(ByVal ItemID As String, Optional ByVal ItemType As Integer = 0, Optional ByVal ImageUrl As String = "")
        templateItemID = ItemID
        tempItemType = ItemType
        tempImageUrl = ImageUrl
    End Sub

    Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) _
      Implements System.Web.UI.ITemplate.InstantiateIn

        Dim ph As New PlaceHolder
        Select Case tempItemType
            Case 0
                Dim item1 As New Label
                item1.ID = templateItemID
                ph.Controls.Add(item1)
            Case 1
                Dim item1 As New CheckBox
                item1.ID = templateItemID
                ph.Controls.Add(item1)
            Case 2
                Dim item1 As New ImageButton
                item1.ID = templateItemID
                item1.ImageUrl = tempImageUrl
                item1.CommandName = "MyDelete"
                item1.OnClientClick = "return confirm('Are you sure delete it');"
                'item1.CommandArgument = "<%#Container.DataItem('BomID')%>"
                ph.Controls.Add(item1)
            Case 3
                Dim item1 As New ImageButton
                item1.ID = templateItemID
                'item1.CommandArgument = "<%#Container.DataItem('BomID')%>"
                item1.ImageUrl = tempImageUrl
                item1.CommandName = "MyEdit"
                ph.Controls.Add(item1)
        End Select
       


        'Select Case (templateType)
        '    Case ListItemType.Header
        '        ph.Controls.Add(New LiteralControl("<table border=""1"">" & _
        '            "<tr><td><b>Category ID</b></td>" & _
        '            "<td><b>Category Name</b></td></tr>"))
        '    Case ListItemType.Item
        '        ph.Controls.Add(New LiteralControl("<tr><td>"))
        '        ph.Controls.Add(item1)
        '        ph.Controls.Add(New LiteralControl("</td><td>"))
        '        ph.Controls.Add(item2)
        '        ph.Controls.Add(New LiteralControl("</td></tr>"))
        '        AddHandler ph.DataBinding, New EventHandler(AddressOf Item_DataBinding)
        '    Case ListItemType.AlternatingItem
        '        ph.Controls.Add(New LiteralControl("<tr bgcolor=""lightblue""><td>"))
        '        ph.Controls.Add(item1)
        '        ph.Controls.Add(New LiteralControl("</td><td>"))
        '        ph.Controls.Add(item2)
        '        ph.Controls.Add(New LiteralControl("</td></tr>"))
        '        AddHandler ph.DataBinding, New EventHandler(AddressOf Item_DataBinding)
        '    Case ListItemType.Footer
        '        ph.Controls.Add(New LiteralControl("</table>"))
        'End Select
        container.Controls.Add(ph)
    End Sub

End Class
