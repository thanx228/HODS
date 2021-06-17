<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialListNew.aspx.vb" Inherits="MIS_HTI.MaterialListNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
        }
        .style8
        {
            width: 100px;
        }
        .style9
        {
            height: 21px;
            width: 303px;
        }
        .style10
        {
            width: 102px;
        }
        .style11
        {
            width: 161px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" 
                Height="25%" Width="50%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    

                    <HeaderTemplate>
                        

                        Item FG

                    </HeaderTemplate>
                    

                    <ContentTemplate>
                        

                        <table style="width: 75%;">
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label3" runat="server" Text="Item"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label4" runat="server" Text="Desc"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label5" runat="server" Text="Spec"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label26" runat="server" Text="Qty"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbOrdQty" runat="server" Width="50px">1</asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7" align="left" colspan="2">
                                    

                                    <asp:Label ID="Label24" runat="server" ForeColor="Red" 
                                        Text="* Property is manufactor only"></asp:Label>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                        </table>
                        

                    </ContentTemplate>
                    

                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    

                    <HeaderTemplate>
                        

                        Sale order<br />

                    </HeaderTemplate>
                    

                    <ContentTemplate>
                        

                        <table style="width:75%;">
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label21" runat="server" Text="Cust Code"></asp:Label>
                                    

                                </td>
                                

                                <td class="style11">
                                    

                                    <asp:TextBox ID="tbSaleCust" runat="server" Width="50px"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label6" runat="server" Text="SO Type"></asp:Label>
                                    

                                </td>
                                

                                <td class="style11">
                                    

                                    <asp:DropDownList ID="ddlSaleType" runat="server">
                                        

                                    </asp:DropDownList>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label7" runat="server" Text="SO Number"></asp:Label>
                                    

                                </td>
                                

                                <td class="style11">
                                    

                                    <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label8" runat="server" Text="SO Seq"></asp:Label>
                                    

                                </td>
                                

                                <td class="style11">
                                    

                                    <asp:TextBox ID="tbSaleSeq" runat="server"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label9" runat="server" Text="Date From"></asp:Label>
                                    

                                </td>
                                

                                <td class="style11">
                                    

                                    <asp:TextBox ID="tbDateFm" runat="server" Width="80px"></asp:TextBox>
                                    

                                    <asp:CalendarExtender ID="tbDateFm_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbDateFm" BehaviorID="_content_tbDateFm_CalendarExtender">
                                        

                                    </asp:CalendarExtender>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label10" runat="server" Text="Date To"></asp:Label>
                                    

                                </td>
                                

                                <td class="style11">
                                    

                                    <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                                    

                                    <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbDateTo" BehaviorID="_content_tbDateTo_CalendarExtender">
                                        

                                    </asp:CalendarExtender>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style7">
                                    

                                    <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                                    

                                </td>
                                

                                <td class="style11">
                                    

                                    <asp:DropDownList ID="ddlSaleStatus" runat="server">
                                        

                                        <asp:ListItem Value="N,y,Y">ALL</asp:ListItem>
                                        

                                        <asp:ListItem Value="y,Y">Closed</asp:ListItem>
                                        

                                        <asp:ListItem Value="N">Not Close</asp:ListItem>
                                        

                                    </asp:DropDownList>
                                    

                                </td>
                                

                            </tr>
                            

                        </table>
                        

                    </ContentTemplate>
                    

                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                    

                    <HeaderTemplate>
                        

                        Manf Order

                    </HeaderTemplate>
                    

                    <ContentTemplate>
                        

                        <table style="width:75%;">
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:DropDownList ID="ddlWorkType" runat="server">
                                        

                                    </asp:DropDownList>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    <asp:Label ID="Label13" runat="server" Text="MO No"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbWorkNo" runat="server"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    <asp:Label ID="Label23" runat="server" Text="Cust"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbWorkCust" runat="server"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    <asp:Label ID="Label27" runat="server" Text="Date From"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbWorkDateFM" runat="server" Width="80px"></asp:TextBox>
                                    

                                    <asp:CalendarExtender ID="tbWorkDateFM_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbWorkDateFM" BehaviorID="_content_tbWorkDateFM_CalendarExtender">
                                        

                                    </asp:CalendarExtender>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    <asp:Label ID="Label28" runat="server" Text="Date To"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbWorkDateTo" runat="server" Width="80px"></asp:TextBox>
                                    

                                    <asp:CalendarExtender ID="tbWorkDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbWorkDateTo" BehaviorID="_content_tbWorkDateTo_CalendarExtender">
                                        

                                    </asp:CalendarExtender>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style8">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                        </table>
                        

                    </ContentTemplate>
                    

                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
                    

                    <HeaderTemplate>
                        

                        Customer

                    </HeaderTemplate>
                    

                    <ContentTemplate>
                        

                        <table style="width:75%;">
                            

                            <tr>
                                

                                <td class="style10">
                                    

                                    <asp:Label ID="Label22" runat="server" Text="Cust Code"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:TextBox ID="tbCust" runat="server"></asp:TextBox>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style10">
                                    

                                    <asp:Label ID="Label30" runat="server" Text="Source By"></asp:Label>
                                    

                                </td>
                                

                                <td>
                                    

                                    <asp:DropDownList ID="ddlSource" runat="server">
                                        

                                        <asp:ListItem Value="1">Sale Forcast not Close</asp:ListItem>
                                        

                                        <asp:ListItem Value="2">Sale Order Not Close</asp:ListItem>
                                        

                                    </asp:DropDownList>
                                    

                                </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style10">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style10">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style10">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style10">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style10">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                            <tr>
                                

                                <td class="style10">
                                    

                                    </td>
                                

                                <td>
                                    

                                    </td>
                                

                            </tr>
                            

                        </table>
                        

                    </ContentTemplate>
                    

                </asp:TabPanel>
            </asp:TabContainer>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Show Report " />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="A" HeaderText="Doc No" />
                    <asp:BoundField DataField="B" HeaderText="Parent Item" />
                    <asp:BoundField DataField="C" HeaderText="Sub Item" />
                    <asp:BoundField DataField="D" HeaderText="Sub Item Spec" />
                    <asp:BoundField DataField="E" HeaderText="Sub Item Desc" />
                    <asp:BoundField DataField="F" DataFormatString="{0:N2}" HeaderText="Order Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="G" DataFormatString="{0:N3}" HeaderText="Qty Per">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="H" HeaderText="Unit" />
                    <asp:BoundField DataField="I" HeaderText="Property" />
                    <asp:BoundField DataField="J" DataFormatString="{0:N2}" HeaderText="Stock">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="K" DataFormatString="{0:N2}" HeaderText="Request">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="L" DataFormatString="{0:N2}" HeaderText="Plan Issue">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="M" DataFormatString="{0:N2}" HeaderText="PR">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="N" DataFormatString="{0:N2}" HeaderText="PO">
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
