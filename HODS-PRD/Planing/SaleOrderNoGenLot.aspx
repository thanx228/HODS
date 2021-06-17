<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleOrderNoGenLot.aspx.vb" Inherits="MIS_HTI.SaleOrderNoGenLot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 88px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    <ContentTemplate> 
    <table class="TSHOW">
        <tr>
            <td background="/Images/btt.jpg"  height="25px"  colspan="3">
                Sale Order No Generate Lot Requiement Plan</td>
        </tr>
        <tr>
            <td class="style4">
                </td>
            <td class="style4">
                <asp:Button ID="BtPreview" runat="server" Text="Preview" Width="90px" />
            </td>
            <td class="style4">
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </ContentTemplate> 
      </asp:UpdatePanel>
</asp:Content>
