<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="planIssueMat.aspx.vb" Inherits="MIS_HTI.planIssueMat" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register src="../UserControl/CheckBoxListUserControl.ascx" tagname="CheckBoxListUserControl" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 138px;
        }
        .style4
        {
           
        }
        .style5
        {
            width: 139px;
        }
        .style6
        {
            width: 139px;
        }
        .style7
        {
            height: 29px;
        }
        .style8
        {
            width: 40px;
        }
        .style9
        {
            height: 24px;
        }
        .style10
        {
            width: 139px;
        }
        .style18
        {
            width: 139px;
        }
        .style19
        {
           
        }
        .style20
        {
            
        }
        .auto-style3 {
            height: 29px;
            width: 968px;
        }
    </style>
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>
    <link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css">
    <script type="text/javascript">
        $(document).ready(function () {
            $('a#hplShow').live('click', function (e) {

                var page = $(this).attr("href")  //get url of link

                var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 450,
                    width: 'auto',
                    title: "Edit Employee",
                    buttons: {
                        "Close": function () { $dialog.dialog('close'); }
                    },
                    close: function (event, ui) {

                        __doPostBack('<%= btShow.ClientID %>', '');  // To refresh gridview when user close dialog
                    }
                });
                $dialog.dialog('open');
                e.preventDefault();
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%; ">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label18" runat="server" Text="Warehouse"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="CblWh" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlSaleType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlTypeMO" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label15" runat="server" Text="SO No"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSoNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="MO No. From"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbMoFrom" runat="server" Width="130px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style19">
                        <asp:Label ID="Label14" runat="server" Text="SO Seq"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style19">
                        <asp:TextBox ID="tbSoSeq" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style19">
                        <asp:Label ID="Label12" runat="server" Text="MO No To"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbMoTo" runat="server" Width="130px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style20" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td class="style20" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
                    </td>
                    <td class="style20" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label17" runat="server" Text="Customer"></asp:Label>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblCodeType" runat="server" RepeatColumns="2" 
                            RepeatDirection="Horizontal" style="margin-left: 0px; margin-right: 0px" 
                            Width="250px">
                            <asp:ListItem Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="2">Finished Goods</asp:ListItem>
                            <asp:ListItem Value="3">Semi FG</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part and Another</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Selected="True" Value="3">Plan MO Start</asp:ListItem>
                            <asp:ListItem Value="2">Plan Mat Issue Date</asp:ListItem>
                            <asp:ListItem Value="1">MO Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <uc2:Date ID="ucDateFrom" runat="server" />
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <uc2:Date ID="ucDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCondition" runat="server" Width="152px">
                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Stock &gt;= Issue Req</asp:ListItem>
                            <asp:ListItem Value="2">Issue Req Over Stock</asp:ListItem>
                            <asp:ListItem Value="3">Issue Req =0 ,Stock&gt;0</asp:ListItem>
                            <asp:ListItem Value="4">Stock=0,Issue Req&gt;0</asp:ListItem>
                            <asp:ListItem Value="5">Stock &lt; Issue Req</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" ForeColor="Red" 
                            Text="Stock=Stock+Support"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                        class="auto-style3">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;
                        <asp:Button ID="btExportGrid" runat="server" Text="Excel" />
                    </td>
                </tr>
            </table>
            <table>
            </table>
            <uc1:CountRow ID="ucCountRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                Height="16px">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
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
            <asp:PostBackTrigger ControlID="btExportGrid" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
