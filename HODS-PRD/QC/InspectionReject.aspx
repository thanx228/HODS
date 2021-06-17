<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="InspectionReject.aspx.vb" Inherits="MIS_HTI.InspectionReject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 626px;
        }
        .style7
        {
            height: 26px;
        }
        .style8
        {
            width: 626px;
            height: 26px;
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
                        <asp:Label ID="Label7" runat="server" Text="QC Inspection Reject Form" 
                            Font-Size="1.1em" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="Source Type"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:DropDownList ID="ddlSourceType" runat="server">
                            <asp:ListItem Value="1">Purchase Inspection</asp:ListItem>
                            <asp:ListItem Value="2">Transfer Inspection</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label4" runat="server" Text="Doc Type"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style8">
                        <asp:TextBox ID="tbType" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style7">
                        </td>
                    <td bgcolor="White" class="style7">
                        </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Doc No"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbNo" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Doc Seq"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbSeq" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btPrint" runat="server" Text="Print Form" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
