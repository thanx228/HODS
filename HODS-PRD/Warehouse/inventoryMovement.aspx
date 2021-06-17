<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="inventoryMovement.aspx.vb" Inherits="MIS_HTI.inventoryMovement" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css"> 
        .tableMain{
            width:75%;
            background-color:white;
        }
        .textRight{
            text-align:right;
        }
    </style>

    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
       function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth,
                height: 600,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="tableMain" cellpadding="5" cellspacing="5">
                <tr>
                    <td width="15%">
                    </td>
                    <td width="30%">
                    </td>
                    <td width="15%">
                    </td>
                    <td width="40%">
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label4" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTypeCode" runat="server">
                            <asp:ListItem Value="11">FG</asp:ListItem>
                            <asp:ListItem Value="12">Semi FG</asp:ListItem>
                            <asp:ListItem Selected="True" Value="13">Mat and Spare part</asp:ListItem>
                            <asp:ListItem Value="14">Supply</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="textRight">
                        <asp:Label ID="Label5" runat="server" Text="End Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label11" runat="server" Text="Warehouse"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblWH" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label6" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbItem" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="textRight">
                        <asp:Label ID="Label8" runat="server" Text="Desc"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDesc" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label7" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="textRight">
                        <asp:Label ID="Label12" runat="server" Text="Include"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="CbObver30Days" runat="server" Text="Over 30 Days" />
                    </td>
                </tr>
            </table>
            <table class="tableMain" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
                <tr>
                    <td align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>

            <uc2:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="FG Part Common All Bom (Spec)">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">All BOM</asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="F21" HeaderText="Have BOM" /> 
                    <asp:BoundField DataField="F211" HeaderText="Use BOM" /> 
                    <asp:BoundField DataField="F01" HeaderText="Item" />
                    <asp:BoundField DataField="F02" HeaderText="Desc" />
                    <asp:BoundField DataField="F03" HeaderText="Spec" />
                    <asp:BoundField DataField="F05" HeaderText="Unit" />
                    <asp:BoundField DataField="F04" DataFormatString="{0:N2}" 
                        HeaderText="Inventory Qty" />
                    <asp:BoundField DataField="F18" HeaderText="Unit Price" />
                    <asp:BoundField DataField="F16" HeaderText="Unit Cost" />
                    <asp:BoundField DataField="F17" HeaderText="Amount" />
                    <asp:BoundField DataField="F06" HeaderText="Main WH" />
                    <asp:BoundField DataField="F66" HeaderText="Qty Safety Stock" />
                    <asp:BoundField DataField="date_diff" DataFormatString="{0:N0}" HeaderText="Date Diff" />
                    <asp:BoundField DataField="F90" DataFormatString="{0:N2}" 
                        HeaderText="&lt;=30 Days" >
                    </asp:BoundField>
                    <asp:BoundField DataField="F09" DataFormatString="{0:N2}" 
                        HeaderText="31~90 Day" >
                    </asp:BoundField>
                    <asp:BoundField DataField="F10" DataFormatString="{0:N2}" 
                        HeaderText="91~180 Day" >
                    </asp:BoundField>
                    <asp:BoundField DataField="F11" DataFormatString="{0:N2}" 
                        HeaderText="181~270 Day" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F12" DataFormatString="{0:N2}" HeaderText="271~360 Day" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F13" DataFormatString="{0:N2}" HeaderText="361~720 Day" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F22" DataFormatString="{0:N2}" 
                        HeaderText="721~999 Day" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F23" DataFormatString="{0:N2}" 
                        HeaderText="&gt;999 Day" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F151" HeaderText="Last Transaction Date" />
                    <asp:BoundField DataField="F14" HeaderText="Last Issue Date" />
                    <asp:BoundField DataField="F15" HeaderText="Last Receive Date" />
                    <asp:BoundField DataField="F07" DataFormatString="{0:N2}" HeaderText="MO" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F08" DataFormatString="{0:N2}" HeaderText="Forcast" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SO" HeaderText="SO " DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="F19" HeaderText="Buyer"/>
                    <asp:BoundField DataField="F20" HeaderText="Last Receive(Vender)" />       
                    <asp:BoundField DataField="F91" HeaderText="PR.NO." />
                    <asp:BoundField DataField="F92" HeaderText="PO.NO." />            
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
