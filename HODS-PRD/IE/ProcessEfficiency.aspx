<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ProcessEfficiency.aspx.vb" Inherits="MIS_HTI.ProcessEfficiency" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="EffContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="style8">
                    <tr>
                        <td class="style9" align="center" colspan="8">
                            <asp:Label ID="Label6" runat="server" Text="Monthly Efficiency Process" 
                                Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style17">
                            <asp:Label ID="Label5" runat="server" Text="% Target :"></asp:Label>
                        </td>
                        <td class="style18">
                            <asp:TextBox ID="TbTarget" runat="server" Height="18px" Width="64px"></asp:TextBox>
                        </td>
                        <td class="style19">
                            <asp:Label ID="Label7" runat="server" Text="Work Time/Day :"></asp:Label>
                        </td>
                        <td class="style20">
                            <asp:TextBox ID="TbTime" runat="server" Height="18px" Width="74px"></asp:TextBox>
                        </td>
                        <td class="style13">
                            &nbsp;</td>
                        <td class="style21">
                            &nbsp;</td>
                        <td class="style14">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style17">
                            <asp:Label ID="Label1" runat="server" Text="Year :"></asp:Label>
                        </td>
                        <td class="style18">
                            <asp:DropDownList ID="ddlYear" runat="server" Height="22px" Width="72px">
                                <asp:ListItem>2011</asp:ListItem>
                                <asp:ListItem>2012</asp:ListItem>
                                <asp:ListItem>2013</asp:ListItem>
                                <asp:ListItem>2014</asp:ListItem>
                                <asp:ListItem>2015</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style19">
                            <asp:Label ID="Label2" runat="server" Text="Month :"></asp:Label>
                        </td>
                        <td class="style20">
                            <asp:DropDownList ID="ddlMonth" runat="server" Height="22px" Width="57px">
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style13">
                            <asp:Label ID="Label3" runat="server" Text="Work Center :"></asp:Label>
                        </td>
                        <td class="style21">
                            <asp:DropDownList ID="ddlProcess" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td class="style14">
                            <asp:Label ID="Label4" runat="server" Text="Machine Code No :" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLMachine" runat="server" AutoPostBack="True" 
                                Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style17">
                            &nbsp;</td>
                        <td class="style18">
                            <asp:Button ID="BtPerMC" runat="server" Text="Machine" />
                        </td>
                        <td class="style19">
                            <asp:Button ID="BtEffMC" runat="server" Height="26px" Text="Eff MC" />
                        </td>
                        <td class="style20">
                            <asp:Button ID="BtPerMan" runat="server" Text="Man" />
                        </td>
                        <td class="style13">
                            <asp:Button ID="BtEffMan" runat="server" Text="Eff Man" />
                        </td>
                        <td class="style21">
                            &nbsp;</td>
                        <td class="style14">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
     <br />
</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style8
        {
            width: 121%;
        }
        .style9
        {
        }
        .style13
        {
            width: 85px;
        }
        .style14
        {
            width: 117px;
        }
        .style17
        {
            width: 66px;
        }
        .style18
        {
            width: 78px;
        }
        .style19
        {
            width: 108px;
        }
        .style20
        {
            width: 32px;
        }
        .style21
        {
            width: 41px;
        }
    </style>
</asp:Content>



















