<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="AccountStatus.aspx.vb" Inherits="MIS_HTI.AccountStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 75%;
        }
        .style6
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 70%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Account Report"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 728px">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="Type  Show"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="0">รายงานภาษีขาย</asp:ListItem>
                            <asp:ListItem Value="1">ลูกหนี้คงค้างแบบละเอียด</asp:ListItem>
                            <asp:ListItem Value="2">ลูกหนี้คงค้างแบบสรุป</asp:ListItem>
                            <asp:ListItem Value="6">วิเคราะห์อายุลูกหนี้</asp:ListItem>
                            <asp:ListItem Value="7">ลูกหนี้คงค้างเกินกำหนด</asp:ListItem>
                            <asp:ListItem Value="8">สรุปยอดขายประจำงวด</asp:ListItem>
                            <asp:ListItem Value="3">รายงานภาษีซื้อ</asp:ListItem>
                            <asp:ListItem Value="4">เจ้าหนี้คงค้างแบบละเอียด</asp:ListItem>
                            <asp:ListItem Value="5">เจ้าหนี้คงค้างแบบสรุป</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Send To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlSendTo" runat="server">
                            <asp:ListItem Value="0">กรมสรรพากร</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">แอ็คมี</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo"  Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Supplier/Customer"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Supplier/Customer Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlAccType" runat="server">
                            <asp:ListItem Selected="True" Value="0">Show All:ทั้งหมด</asp:ListItem>
                            <asp:ListItem Value="1">Local : ในประเทศ</asp:ListItem>
                            <asp:ListItem Value="2">Foreign : ต่างประเทศ</asp:ListItem>
                            <asp:ListItem Value="3">Other : อื่นๆ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td align="center" class="style6" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btPrint" runat="server" style="height: 26px" 
                            Text="Print Report" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
