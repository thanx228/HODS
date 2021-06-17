<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="FixAsset.aspx.vb" Inherits="MIS_HTI.FixAsset" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 145px;
        }
        .auto-style4 {
            width: 225px;
        }
        .auto-style5 {
            width: 96px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td bgcolor="White" style="vertical-align: top" class="auto-style3">
                        <asp:Label ID="Label11" runat="server" Text="Asset Po Type :"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style3">
                        <asp:Label ID="Label2" runat="server" Text="Asset Po No :"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style4">
                        <asp:TextBox ID="Po" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="auto-style5">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style3">
                        <asp:Label ID="Label3" runat="server" Text="Supp ID :"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style4">
                        <asp:TextBox ID="tbSup" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="auto-style5">
                        <asp:Label ID="Label12" runat="server" Text="Condition :"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="DDLCondition" runat="server" AutoPostBack="True">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem Selected="True">Incomplete</asp:ListItem>
                            <asp:ListItem>Complete</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style3">
                        <asp:Label ID="Label4" runat="server" Text="Asset :"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style4">
                        <asp:TextBox ID="Asset" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="auto-style5">
                        <asp:Label ID="Label5" runat="server" Text="Asset Spec :"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="Spec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style3">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="TM003">Doc Date</asp:ListItem>
                            <asp:ListItem Value="TN015">Due Date</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="Label17" runat="server" Text="From"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style4">
                        <asp:TextBox ID="FDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="FDate_CalendarExtender" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" TargetControlID="FDate">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White" class="auto-style5">
                        <asp:Label ID="Label14" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="TDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="TDate_CalendarExtender" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" TargetControlID="TDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:70%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                          <asp:Button ID="BuSearch" runat="server" Text="Show Report"  />
                        <asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <table style="width:60%;">
                <tr>
                    <td align="center" bgcolor="White" class="style7">
                        <asp:Label ID="Label15" runat="server" Text="Amount of Row"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label16" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>

            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TM001" HeaderText="PO Asset" />
                    <asp:BoundField DataField="TM003" HeaderText="Doc Date" />
                    <asp:BoundField DataField="TN015" HeaderText="Plan Del Date" />
                    <asp:BoundField DataField="TN0151" HeaderText="Days" />
                    <asp:BoundField DataField="TM005" HeaderText="Supplier" />
                    <asp:BoundField DataField="TM006" HeaderText="Currency" />
                    <asp:BoundField DataField="TN004" HeaderText="Asset Name" />
                    <asp:BoundField DataField="TN005" HeaderText="Asset Spec" />
                    <asp:BoundField DataField="TN007" HeaderText="Unit" />
                    <asp:BoundField DataField="TN006" DataFormatString="{0:N}" HeaderText="Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TN011" DataFormatString="{0:N}" 
                        HeaderText="Del Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BAL" DataFormatString="{0:N}" HeaderText="Del Bal">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TN012" HeaderText="Status" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

        </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger  ControlID="btExport"  />
        </Triggers>
    </asp:UpdatePanel>
 
</asp:Content>
