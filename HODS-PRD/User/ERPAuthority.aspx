﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ERPAuthority.aspx.vb" Inherits="MIS_HTI.ERPAuthority" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
            width: 118px;
        }
        .style8
        {
            height: 21px;
            width: 118px;
        }
        .style9
        {
            width: 280px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Text="ERP Authority" Font-Size="1.1em" 
                            ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;" bgcolor="White">
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label4" runat="server" Text="Group"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGroup" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label5" runat="server" Text="Dept"></asp:Label>
                        </td>
                    <td class="style6">
                        <asp:DropDownList ID="ddlDept" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label6" runat="server" Text="Procedure"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbProcedure" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label9" runat="server" Text="Authorize"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNew" runat="server" Text="New" />
                        &nbsp;<asp:CheckBox ID="cbQuery" runat="server" Text="View" />
                        &nbsp;<asp:CheckBox ID="cbAlter" runat="server" Text="Alter" />
                        &nbsp;<asp:CheckBox ID="cbDelete" runat="server" Text="Delete" />
                        &nbsp;<asp:CheckBox ID="cbApprove" runat="server" Text="Approve" />
                        &nbsp;<asp:CheckBox ID="cbDeApp" runat="server" Text="De-Approve" />
                        &nbsp;<asp:CheckBox ID="cbExport" runat="server" Text="Export" />
                    </td>
                </tr>
            </table>
            <table style="width:75%; background-image: url('../Images/btt.jpg'); background-repeat: no-repeat;">
                <tr>
                    <td align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" bgcolor="White" class="style9">
                        <asp:Label ID="Label7" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="lbCount" runat="server"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="MF004" HeaderText="Group" />
                    <asp:BoundField DataField="GNAME" HeaderText="Group Name" />
                    <asp:BoundField DataField="MF001" HeaderText="User" />
                    <asp:BoundField DataField="MF002" HeaderText="User Name" />
                    <asp:BoundField DataField="MF007" HeaderText="Dept" />
                    <asp:BoundField DataField="DNAME" HeaderText="Dept Name" />
                    <asp:BoundField DataField="MA005" HeaderText="Status" />
                    <asp:BoundField DataField="MG002" HeaderText="Procedure" />
                    <asp:BoundField DataField="MB011" HeaderText="Procedure Name" />
                    <asp:BoundField DataField="A" HeaderText="New" />
                    <asp:BoundField DataField="B" HeaderText="Query" />
                    <asp:BoundField DataField="C" HeaderText="Alter" />
                    <asp:BoundField DataField="D" HeaderText="Delete" />
                    <asp:BoundField DataField="E" HeaderText="Approved" />
                    <asp:BoundField DataField="F" HeaderText="De-Approved" />
                    <asp:BoundField DataField="G" HeaderText="Export" />
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
    </asp:UpdatePanel>
</asp:Content>
