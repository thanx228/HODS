<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTEmpList.aspx.vb" Inherits="MIS_HTI.OTEmpList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style4 {
            width: 73px;
        }
        .auto-style5 {
            width: 264px;
        }
        .auto-style6 {
            width: 115px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 80%;">
                <tr>
                    <td class="auto-style4" style="background-color: #FFFFFF">
                        Dept.</td>
                    <td class="style10" colspan="3" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblDept" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
               <tr>
                    <td class="auto-style4" style="background-color: #FFFFFF">
                        <asp:Label ID="lbEmpNo" runat="server" Text="Emp. No."></asp:Label>
                    </td>
                    <td class="auto-style5" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbEmpNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style6" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btShow" runat="server" Text="Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                        <br />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvEdit" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>

                    <asp:BoundField DataField="Dept." HeaderText="Dept." />

                    <asp:BoundField DataField="EmpNo" HeaderText="Emp. No." />
                    <asp:BoundField DataField="Name" HeaderText="Name" />

                    <asp:BoundField DataField="ShiftDay" HeaderText="ShiftDay Default" />


                        <asp:TemplateField HeaderText="Position">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlPosition" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>


                        <asp:TemplateField HeaderText="Bus Line">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlBusLine" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Line">
                         <ItemTemplate>
                            <asp:DropDownList ID="ddlLine" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Shift">
                         <ItemTemplate>
                            <asp:DropDownList ID="ddlShift" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Shift Day">
                         <ItemTemplate>
                            <asp:DropDownList ID="ddlShiftDay" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Resign(ลาออก)">
                        <ItemTemplate>
                        <asp:CheckBox ID = "cbResign" runat = "server" /></CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Work Center">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlWorkCenter" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

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

            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
