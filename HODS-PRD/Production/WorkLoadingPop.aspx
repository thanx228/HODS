<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkLoadingPop.aspx.vb" Inherits="MIS_HTI.WorkLoadingPop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Check Code Status</title>
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:58%;">
            <tr>
                <td align="center" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                    class="style1">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Productoin Line Loading Detail"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:61%;">
            </table>
        <table style="width: 58%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label26" runat="server" 
                        Text="จำนวนรายการ  Purchase Request            "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPR" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label27" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Height="16px" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="F01" HeaderText="WC" />
                <asp:BoundField DataField="F011" HeaderText="Process Name" />
                <asp:BoundField DataField="F02" HeaderText="Cust Name" />
                <asp:BoundField DataField="F021" HeaderText="SO" />
                <asp:BoundField DataField="F03" HeaderText="Work Order" />
                <asp:BoundField DataField="F04" HeaderText="Spec" />
                <asp:BoundField DataField="F05" DataFormatString="{0:N0}" 
                    HeaderText="Plan Qty" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="F06" DataFormatString="{0:N0}" 
                    HeaderText="WIP Qty" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="F061" DataFormatString="{0:N0}" 
                    HeaderText="Input Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="F062" DataFormatString="{0:N0}" 
                    HeaderText="Finish Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="F063" DataFormatString="{0:N0}" 
                    HeaderText="Scrap Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="F07" DataFormatString="{0:N0}" 
                    HeaderText="Time//Pcs(Sec)" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="F08" HeaderText="Plan Finish Date" />
                <asp:BoundField DataField="F09" HeaderText="Actual Plan Date" />
                <asp:BoundField DataField="F10" HeaderText="MO Status" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
