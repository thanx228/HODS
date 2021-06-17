<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OTEmpListDetail.aspx.vb" Inherits="MIS_HTI.OTEmpListDetail" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Bus Location Detail"></asp:Label>
                </td>
            </tr>
        </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
        <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />

<SortedAscendingCellStyle BackColor="#EDF6F6"></SortedAscendingCellStyle>

<SortedAscendingHeaderStyle BackColor="#0D4AC4"></SortedAscendingHeaderStyle>

<SortedDescendingCellStyle BackColor="#D6DFDF"></SortedDescendingCellStyle>

<SortedDescendingHeaderStyle BackColor="#002876"></SortedDescendingHeaderStyle>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
