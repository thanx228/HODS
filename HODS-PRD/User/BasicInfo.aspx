<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BasicInfo.aspx.vb" Inherits="MIS_HTI.BasicInfo" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollSub() {
            gridView1 = $('#<%= gvSub.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 450,
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
            <uc1:HeaderForm ID="ucHeaderForm" runat="server" />
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                Height="600px" Width="800px">
                
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Head Code&nbsp;
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbHeadCode" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbHeadName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td align="center" 
                                    style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                                    <asp:Button ID="btHeadDelete" runat="server" 
                                        onclientclick="return confirm('Are you delete it');" Text="Delete" />
                                    &nbsp;<asp:Button ID="btHeadSave" runat="server" Text="Save" 
                                        onclientclick="return confirm('Are you save it');" />
                                    &nbsp;<asp:Button ID="btHeadSearch" runat="server" Text="Search" />
                                    &nbsp;<asp:Button ID="btHeadReset" runat="server" Text="Reset" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvHead" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4" style="margin-right: 0px">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="onEdit" HeaderText="Edit" 
                                    ImageUrl="~/Images/edit.gif" Text="Button" />
                                <asp:BoundField DataField="A" HeaderText="Head Code" />
                                <asp:BoundField DataField="B" HeaderText="Head Name" />
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
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Sub Code
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Code Type"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHead" runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbSubCodeType" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSubCode" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSubName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Remark"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSubRemark" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                                    align="center">
                                    <asp:Button ID="btSubDelete" runat="server" 
                                        onclientclick="return confirm('Are you want delete it?');" Text="Delete" />
                                    &nbsp;<asp:Button ID="btSubSave" runat="server" Text="Save" />
                                    &nbsp;<asp:Button ID="btSubSearch" runat="server" Text="Search" />
                                    &nbsp;<asp:Button ID="btPosReset" runat="server" Text="Reset" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvSub" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" HeaderText="Edit" 
                                    ImageUrl="~/Images/edit.gif" Text="Button" CommandName="onEdit" />
                                <asp:BoundField DataField="A" HeaderText="Code Type" />
                                <asp:BoundField DataField="B" HeaderText="Code" />
                                <asp:BoundField DataField="C" HeaderText="Name" />
                                <asp:BoundField DataField="D" HeaderText="Remark" />
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
                </asp:TabPanel>
                
            </asp:TabContainer>
<br />
<br />
<br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
