<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkCenterStatus.aspx.vb" Inherits="MIS_HTI.WorkCenterStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/CountRow.ascx" TagPrefix="uc2" TagName="CountRow" %>

<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 104px;
        }
        .style7
        {
             width: 32px;
        }
        .style8
        {
            width: 605px;
        }
        .style10
        {
        
        }
        .style11
        {
            width: 126px;
        }
        .style12
        {
            width: 93px;
        }
    </style>
 <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
        });

        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
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
            <table style="width:95%">
                <tr>
                    <td style="background-color: #FFFFFF; width:10%">
                        <asp:Label ID="Label8" runat="server" Text="Property"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF ; width:40%">
                        <asp:DropDownList ID="ddlProperty" runat="server">
                            <asp:ListItem Selected="True" Value="1">Internal(Work Center)</asp:ListItem>
                            <asp:ListItem Value="2">Outsource(Supplier)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF; width:10%">
                        <asp:Label ID="Label9" runat="server" Text="Suplier"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF; width:40%">
                        <asp:TextBox ID="tbWC" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" MaxLength="5" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlWC" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlMoType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label14" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlSaleType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label17" runat="server" Text="Sale No"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleNo" runat="server" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px" Width="120px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label18" runat="server" Text="Sale Seq"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleSeq" runat="server" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc3:Date ID="UcDateFrom" runat="server" />
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <uc3:Date ID="UcDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Value="0">All Status</asp:ListItem>
                            <asp:ListItem Selected="True" Value="123">All Not Finished Status</asp:ListItem>
                            <asp:ListItem Value="1">Havn&#39;t Manufactured</asp:ListItem>
                            <asp:ListItem Value="2">Materials Not Issue</asp:ListItem>
                            <asp:ListItem Value="3">Manufacturing</asp:ListItem>
                            <asp:ListItem Value="Yy">All Finished</asp:ListItem>
                            <asp:ListItem Value="Y">Finished</asp:ListItem>
                            <asp:ListItem Value="y">Manual Closed</asp:ListItem>
                        </asp:DropDownList>
                    </td>


                    <td>Qty Status</td>
                    <td>

                        <asp:DropDownList ID="ddlWip" runat="server">
                            <asp:ListItem Selected="True" Value="All">ALL</asp:ListItem>
                            <asp:ListItem Value="1">WIP Qty > 0</asp:ListItem>
                            <asp:ListItem Value="2">Pending Qty > 0</asp:ListItem>
                        </asp:DropDownList>

                    </td>




                    <%-- <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label15" runat="server" Text="Print For"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlFor" runat="server">
                            <asp:ListItem Value="2">Audit</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">Check</asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                </tr>
 <%--               <tr>
                    <td>Qty Status</td>
                    <td>

                        <asp:DropDownList ID="ddlWip" runat="server">
                            <asp:ListItem Selected="True" Value="1">WIP Qty > 0</asp:ListItem>
                            <asp:ListItem Value="2">Pending Qty > 0</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>--%>
            </table>
            <table style="width:95%; background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
                <tr>
                    <td align="center" class="style10" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;
                        <asp:Button ID="btExportExcel" runat="server" Text="Export Excel" />
                        &nbsp;
<%--                        <asp:Button ID="btLabel" runat="server" Text="Print Label" Width="80px" 
                            Visible="False" />
                        &nbsp;
                        <asp:Button ID="btList" runat="server" Text="Print List" Width="80px" 
                            Visible="False" />
                        <uc1:CountRow ID="CountRow1" runat="server" />--%>
                    </td>
                </tr>
            </table>

            <uc2:CountRow runat="server" ID="CountRow" />

            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" 
                    Wrap="False" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExportExcel" />
        </Triggers>

    </asp:UpdatePanel>
    </asp:Content>
