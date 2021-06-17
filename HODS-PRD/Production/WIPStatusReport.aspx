<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WIPStatusReport.aspx.vb" Inherits="MIS_HTI.WIPStatusReport" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>

<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table cellpadding="0" cellspacing="0" style="width:75%;background-color: #FFFFFF;">
                <tr>
                    <td style="vertical-align: top;width: 150px;">
                        <asp:Label ID="Label3" runat="server" Text="Work Center"></asp:Label>
                        <br />
                        <asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="True" Text="ClickAll" />
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="CheckWorkC" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">
                        <asp:Label ID="Label4" runat="server" Text="Report"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReport" runat="server">
                            <asp:ListItem Value="1">WIP STATUS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Last Transfer Date"></asp:Label>
                    </td>
                    <td>
                        <uc2:Date ID="UcDate" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btCancel" runat="server" Text="Cancel" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                        &nbsp;<asp:Button ID="btExportDetail" runat="server" Text="Excel Detail Export" />
                    </td>
                </tr>
            </table>


            <uc1:CountRow ID="CountRow1" runat="server" />


            <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplLink" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#003399" ForeColor="#CCCCFF" HorizontalAlign="Center" Wrap="True" />
            </asp:GridView>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExportDetail" />
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>

    <%-- ตัวอย่างงานเก่า --%>
    <%--<iframe src="http://192.168.1.13:8022/Plan/WIPStatusReport.aspx" frameborder="0" height="60%" width="80%"></iframe>--%>

</asp:Content>
