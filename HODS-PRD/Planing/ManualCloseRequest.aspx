<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ManualCloseRequest.aspx.vb" Inherits="MIS_HTI.ManualCloseRequest" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 108px;
        }
        .style8
        {
            width: 108px;
            height: 26px;
        }
        .style9
        {
            height: 26px;
        }
    .style1
    {
        width: 282px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label4" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMoType" runat="server">
                        </asp:DropDownList>
                        <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label5" runat="server" Text="MO No."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbMoNo" runat="server"></asp:TextBox>
                        <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label6" runat="server" Text="Reason"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReason" runat="server">
                            <asp:ListItem Value="1">Order Cancle</asp:ListItem>
                            <asp:ListItem Value="3">ECN Change</asp:ListItem>
                            <asp:ListItem Value="3">Other</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="tbReason" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label12" runat="server" Text="Page size"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:DropDownList ID="ddlPage" runat="server">
                            <asp:ListItem>A4</asp:ListItem>
                            <asp:ListItem Selected="True" Value="A5">A5(Half A4)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                    <td style="color: #FF0000">
                        *** MO Status Not Completed&nbsp; Only ***</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" 
                            Text="Show Report" Height="30px" />
                        &nbsp;<asp:Button ID="btPrint" runat="server" Text="Print Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="ucRowCount" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TA001" HeaderText="MO Type" />
                    <asp:BoundField DataField="TA002" HeaderText="MO No." />
                    <asp:BoundField DataField="TA006" HeaderText="Item" />
                    <asp:BoundField DataField="TA035" HeaderText="Spec" />
                    <asp:BoundField DataField="TA015" DataFormatString="{0:N2}" 
                        HeaderText="Plan Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA017" DataFormatString="{0:N2}" 
                        HeaderText="Complete Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA018" DataFormatString="{0:N2}" 
                        HeaderText="Scrap Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA060" DataFormatString="{0:N2}" 
                        HeaderText="Destroy Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F01" DataFormatString="{0:N2}" 
                        HeaderText="Balance  Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB003" HeaderText="Child Item" />
                    <asp:BoundField DataField="TB013" HeaderText="Child Spec" />
                    <asp:BoundField DataField="F11" DataFormatString="{0:N2}" HeaderText="Qty Per">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB004" DataFormatString="{0:N2}" 
                        HeaderText="Req Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F02" DataFormatString="{0:N2}" 
                        HeaderText="Issue Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F03" DataFormatString="{0:N2}" 
                        HeaderText="Not Issue Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F04" DataFormatString="{0:N2}" 
                        HeaderText="Reqest Return">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F05" DataFormatString="{0:N2}" 
                        HeaderText="Return Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F06" DataFormatString="{0:N2}" 
                        HeaderText="Bal Return">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
