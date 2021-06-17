<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterailsCallIn.aspx.vb" Inherits="MIS_HTI.MaterailsCallIn" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CheckBoxListUserControl.ascx" tagname="CheckBoxListUserControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style4 {
            width: 110px;
        }
        .auto-style5 {
            width: 83px;
        }
        .auto-style6 {
            width: 266px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%; ">
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style4">
                        <asp:Label ID="Label15" runat="server" Text="Warehouse"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" colspan="3">
                        <uc1:CheckBoxListUserControl ID="UcCblWh" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td class="auto-style6" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblCodeType" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" style="margin-left: 0px; margin-right: 0px" Width="250px">
                            <asp:ListItem Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="2">Finished Goods</asp:ListItem>
                            <asp:ListItem Value="3">Semi FG</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part and Another</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td class="auto-style5" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Call In</asp:ListItem>
                            <asp:ListItem Value="2">Shortage</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style4">
                        <asp:Label ID="Label7" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style6">
                        <asp:TextBox ID="tbCode" runat="server" Width="155px" BorderColor="Blue" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        <asp:Label ID="Label14" runat="server" Text="Desc"></asp:Label>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDesc" runat="server" BorderColor="Blue" BorderStyle="Solid" 
                            BorderWidth="1px" Width="149px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style4">
                        <asp:Label ID="Label12" runat="server" Text="Supplier"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style6">
                        <asp:TextBox ID="tbSup" runat="server" BorderColor="Blue" BorderStyle="Solid" 
                            BorderWidth="1px" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        <asp:Label ID="Label9" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BorderColor="Blue" BorderStyle="Solid" 
                            BorderWidth="1px" Width="149px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style4">
                        <asp:Label ID="Label8" runat="server" Text="End Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style6">
                        <asp:TextBox ID="tbEndDate" runat="server" Width="80px" BorderColor="Blue" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbEndDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbEndDate">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        &nbsp;</td>
                    <td class="style13" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" ForeColor="Red" 
                            Text="Call In=Stock+PO Insp.-Mo Issue-SO "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" ForeColor="Red" 
                            Text="Shortage=Stock+PO Insp.+ PO+PO Manual+PO Forcast+PO MO+PR-MO Rcv-SO-MO Issue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="150px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" Width="150px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF" 
                        class="style6">
                        <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF" 
                        class="style7">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF" 
                        class="style4">
                        <asp:Label ID="Label4" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Item" DataField="C01" />
                    <asp:BoundField HeaderText="Desc" DataField="C02" />
                    <asp:BoundField HeaderText="Spec" DataField="C22" />
                    <asp:BoundField HeaderText="Call In" DataField="C03" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Shortage" DataField="C04" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="MO Issue(-)" DataField="C05" 
                        DataFormatString="{0:N}" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Lead Time(Day)" DataField="C06"/>
                    <asp:BoundField HeaderText="Safety" DataField="C07" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Stock(+)" DataField="C08" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PO Insp.(+)" DataField="C09" 
                        DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PO(+)" DataField="C10" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PO Manual(+)" DataField="C11" 
                        DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PO Forcast(+)" DataField="C12" 
                        DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PO MO(+)" DataField="C13" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PR(+)App" DataField="C14" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PR(+) Not App" DataField="C15" 
                        DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="MO Rcv(+)" DataField="C16" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SO(-)" DataField="C17" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="C18" HeaderText="Main Supp." />
                    <asp:BoundField DataField="C19" HeaderText="Confirm Delivery Date" />
                    <asp:BoundField DataField="C21" HeaderText="Plan Delivery Date" />
                    <asp:BoundField DataField="C20" HeaderText="Main W/H" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" HorizontalAlign="Center" />
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
