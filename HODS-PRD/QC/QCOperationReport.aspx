<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="QCOperationReport.aspx.vb" Inherits="MIS_HTI.QCOperationReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            
        }
        .style12
        {
            width: 332px;
            height: 26px;
        }
        .style14
        {
            width: 92px;
        }
        .style32
        {
            width: 10px;
            height: 26px;
        }
        .style33
        {
            width: 92px;
        }
        .style34
        {
            width: 10px;
        }
        .style37
        {
            width: 332px;
        }
        .style38
        {
            width: 92px;
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table style="width: 70%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="QC Operation Report"></asp:Label>
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 70%;">
                <tr>
                    <td class="style33">
                        Report Type&nbsp; :&nbsp;
                    </td>
                    <td class="style34">
                        <asp:DropDownList ID="ddlReport" runat="server" Width="130px">
                            <asp:ListItem Value="0">D401 Operation</asp:ListItem>
                            <asp:ListItem Value="1">D402 IQC</asp:ListItem>
                            <asp:ListItem Value="2">D403 IPQC</asp:ListItem>
                            <asp:ListItem Value="3">D404 FQC</asp:ListItem>
                            <asp:ListItem Value="4">D405 FA</asp:ListItem>
                            <asp:ListItem Value="5">D406 QA</asp:ListItem>
                            <asp:ListItem Value="6">D407 Defect</asp:ListItem>
                            <asp:ListItem Value="7">D408 Machine</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style33">
                        Work Center&nbsp; :&nbsp;&nbsp; </td>
                    <td class="style37">
                        <asp:TextBox ID="txtWork" runat="server" Width="90px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style38">
                        Item&nbsp; :&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="style32">
                        <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                    </td>
                    <td class="style38">
                        Spec.&nbsp; :&nbsp;&nbsp;
                    </td>
                    <td class="style12">
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style33">
                        Date FM&nbsp; :&nbsp;
                    </td>
                    <td class="style34">
                        <asp:TextBox ID="txtDateFrom" runat="server" Width="90px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style33">
                        &nbsp;To&nbsp; :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style37">
                        <asp:TextBox ID="txtDateTo" runat="server" Width="90px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat"
                        class="style6">
                        <asp:Button ID="btnShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="center" class="style4" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Amout of Rows"></asp:Label>
                    </td>
                    <td align="center" class="style4" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style4" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="TD001" HeaderText="Report Type" />
                    <asp:BoundField DataField="MQ002" HeaderText="Type Name" />
                    <asp:BoundField DataField="TE006" HeaderText="Work Order Type" />
                    <asp:BoundField DataField="TE007" HeaderText="Work Order Number" />
                    <asp:BoundField DataField="TE008" HeaderText="Process Seq." />
                    <asp:BoundField DataField="TE009" HeaderText="Process" />
                    <asp:BoundField DataField="MW002" HeaderText="Process Name" />
                    <asp:BoundField DataField="TE017" HeaderText="Item" />
                    <asp:BoundField DataField="TE019" HeaderText="Item Spec." />
                    <asp:BoundField HeaderText="MO Qt'y" DataField="UDF51" 
                    DataFormatString="{0:#,##0.00}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TE011" HeaderText="Reject Qt'y"
                    DataFormatString="{0:#,##0.00}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Scrap Qt'y" DataField="UDF52"
                    DataFormatString="{0:#,##0.00}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD004" HeaderText="Work Center" />
                    <asp:BoundField DataField="TE015" HeaderText="Remark" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle ForeColor="#003399" HorizontalAlign="Left" BackColor="#99CCCC" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
