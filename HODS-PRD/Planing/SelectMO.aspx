<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SelectMO.aspx.vb" Inherits="MIS_HTI.SelectMO" %>

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
 
        <asp:Label ID="LBMONo" runat="server" Text="Label"></asp:Label>
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
    
    </div>
    </form>
</body>
</html>

