<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PoDelivery.aspx.vb" Inherits="MIS_HTI.PoDelivery" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 423px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style1">
                <tr>
                    <td class="style2">
                        <asp:Label ID="Label1" runat="server" Text="Po Delivery After"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLDate" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Day"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="BuReport" runat="server" Text="View Report" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" />
<br />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
            <br />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <p>
    <br />
        <br />

        

    <p>
        &nbsp;</p>
</asp:Content>
