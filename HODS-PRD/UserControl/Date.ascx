<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Date.ascx.vb" Inherits="MIS_HTI._Date" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:TextBox ID="tbDate" runat="server" Width="80px" autocomplete="off"></asp:TextBox>
<asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" 
    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
</asp:CalendarExtender>

