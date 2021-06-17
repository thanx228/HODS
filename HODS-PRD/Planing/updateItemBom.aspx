<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="updateItemBom.aspx.vb" Inherits="MIS_HTI.updateItemBom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="btUpdate" runat="server" Text="Update Item &amp; Bom Head" />
            <br />
            <asp:Label ID="lbShow" runat="server" ForeColor="Blue"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
