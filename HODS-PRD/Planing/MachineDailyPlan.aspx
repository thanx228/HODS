<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MachineDailyPlan.aspx.vb" Inherits="MIS_HTI.MachineDailyPlan" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
            width: 159px;
        }
        .style8
        {
            width: 155px;
        }
    .auto-style3 {
        width: 49px;
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
                    <td style="background-image: url('http://localhost:54341/Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Machine Plan"></asp:Label>
                        <uc1:Date ID="Date1" runat="server" />
                        <asp:DropDownList ID="DDLWorkCenter" runat="server" >
                        </asp:DropDownList>
                        <asp:Button ID="Button1" runat="server" Text="Change" />
                        <asp:Button ID="Button2" runat="server" Text="Export to Setup Mold ORder" />
                        <asp:Label ID="LBMONo" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
            
            <table style="width:75%;" border="1">
                <tr>
                    <td class="auto-style3"></td>
                    <td>
                        <asp:TextBox ID="LBMachineCode1" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode2" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode3" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode4" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode5" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode6" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode7" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode8" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode9" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode10" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode11" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode12" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LBMachineCode13" runat="server" ReadOnly="True" Width="82px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">0700</td>
                    <td>
                        <asp:Button ID="MachineA1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                        </td>
                    <td>
                        <asp:Button ID="MachineL1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM1" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">0730</td>
                    <td>
                        <asp:Button ID="MachineA2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM2" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">0800</td>
                    <td>
                        <asp:Button ID="MachineA3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM3" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">0830</td>
                    <td>
                        <asp:Button ID="MachineA4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM4" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">0900</td>
                    <td>
                        <asp:Button ID="MachineA5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM5" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">0930</td>
                    <td>
                        <asp:Button ID="MachineA6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM6" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1000</td>
                    <td>
                        <asp:Button ID="MachineA7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM7" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1030</td>
                    <td>
                        <asp:Button ID="MachineA8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM8" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1100</td>
                    <td>
                        <asp:Button ID="MachineA9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM9" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1130</td>
                    <td>
                        <asp:Button ID="MachineA10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM10" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
               
                <tr>
                    <td class="auto-style3">1200</td>
                    <td>
                        <asp:Button ID="MachineA11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" Enabled="False" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM11" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1230</td>
                    <td>
                        <asp:Button ID="MachineA12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" Enabled="False" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM12" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
               
                <tr>
                    <td class="auto-style3">1300</td>
                    <td>
                        <asp:Button ID="MachineA13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM13" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1330</td>
                    <td>
                        <asp:Button ID="MachineA14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM14" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1400</td>
                    <td>
                        <asp:Button ID="MachineA15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM15" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1430</td>
                    <td>
                        <asp:Button ID="MachineA16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM16" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1500</td>
                    <td>
                        <asp:Button ID="MachineA17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM17" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1530</td>
                    <td>
                        <asp:Button ID="MachineA18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM18" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1600</td>
                    <td>
                        <asp:Button ID="MachineA19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM19" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1630</td>
                    <td>
                        <asp:Button ID="MachineA20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM20" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1700</td>
                    <td>
                        <asp:Button ID="MachineA21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM21" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1730</td>
                    <td>
                        <asp:Button ID="MachineA22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM22" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1800</td>
                    <td>
                        <asp:Button ID="MachineA23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM23" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1830</td>
                    <td>
                        <asp:Button ID="MachineA24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM24" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">1900</td>
                    <td>
                        <asp:Button ID="MachineA25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineE25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineF25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineG25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineH25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineI25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineJ25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineK25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineL25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineM25" runat="server" Font-Italic="False" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False" Height="20px" Text="" Width="110px" />
                    </td>
                </tr>

            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
                        <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
