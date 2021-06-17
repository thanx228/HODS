<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DateTextChange.ascx.vb" Inherits="MIS_HTI._DateTextChange" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:TextBox ID="tbDate" runat="server" Width="100px" placeholder="__/__/____" AutoPostBack="True"   ></asp:TextBox>
<asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server"
    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
</asp:CalendarExtender>