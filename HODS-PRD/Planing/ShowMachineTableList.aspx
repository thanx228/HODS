<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ShowMachineTableList.aspx.vb" Inherits="MIS_HTI.ShowMachineTableList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 82px;
        }
        .style3
        {
            width: 81px;
        }
        .style4
        {
            width: 133px;
        }
    input[type="submit"],input[type="button"]
{
    /*css coding*/
    font-size: 16px;
    font-family: Courier New;
    font-weight: bold;
    -webkit-border-radius: 8px 8px 8px 8px;
    -moz-border-radius: 8px 8px 8px 8px;
    border-radius: 8px 8px 8px 8px;
    border: 1px solid #0D47F7;
    padding:2px 6px;
    cursor: pointer;
    color: #0D47F7;
    display: inline-block;
    height: 30px;
}
[type=button],[type=reset],[type=submit],button{-webkit-appearance:button}button,input,optgroup,select,textarea{margin:0;font-family:inherit;font-size:inherit;line-height:inherit}*,::after,::before{box-sizing:border-box}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script language="javascript">
        function CloseWindows() {
            window.close();
        }
    </script>
    <div>
  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" >
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
                        <asp:Button runat="server" ID="BtnExport"  Text="Export Machine Table"/>
                        <br />
 
        <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                            <Columns>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>

        <br />
        <br />
    </ContentTemplate>

         <Triggers>
             <asp:PostBackTrigger ControlID="BtnExport" />
         </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

