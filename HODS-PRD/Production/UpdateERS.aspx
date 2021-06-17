<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="UpdateERS.aspx.vb" Inherits="MIS_HTI.UpdateERS" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            color: #0066FF;
        }
        .auto-style4 {
            color: #3333FF;
        }
        .auto-style5 {
            height: 21px;
        }
        .modalBackground
        {
            background-color: gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 600px;
            height : 150px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding:0
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .footer
        {
            padding: 6px;
        }
        .modalPopup .yes, .modalPopup .no
        {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            border-radius: 4px;
        }
        .modalPopup .yes
        {
            background-color: #2FBDF1;
            border: 1px solid #0DA9D0;
        }
        .modalPopup .no
        {
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="ucCntRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:ButtonField ButtonType="Image" CommandName="editERS" ImageUrl="~/Images/edit.gif" Text="Edit" />
                    <asp:BoundField DataField="MB001" HeaderText="Item" />
                    <asp:BoundField DataField="MB003" HeaderText="Spec" />
                    <asp:BoundField DataField="UDF05" HeaderText="Rev" />
                    <asp:BoundField DataField="UDF06" HeaderText="ERS Code" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:Button ID="modelPopup" runat="server" style="display:none" />
<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                TargetControlID="modelPopup" PopupControlID="updatePanel" BackgroundCssClass="modalBackground" DropShadow="true" 
                Enabled="True" >
</asp:ModalPopupExtender>
<asp:Panel ID="updatePanel" runat="server" CssClass="modalPopup" style="display:none">
    <div class="header">
        <table style="width:100%;">
            <tr>
                <td>ERS Code </td>
                <td align="right">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cancel.png" />
                </td>
            </tr>
        </table>
    </div>
    <div class="body" >
        <table style="width:100%;">
            <tr>
                <td bgcolor="White" align="right" class="auto-style5">
                    <asp:Label ID="Label12" runat="server" Text="Item"></asp:Label>
                </td>
                <td bgcolor="White" align="left" class="auto-style5">
                    <asp:Label ID="lbItemShow" runat="server" CssClass="auto-style4"></asp:Label>
                </td>
                <td bgcolor="White" align="right" class="auto-style5">
                    <asp:Label ID="Label14" runat="server" Text="Spec"></asp:Label>
                </td>
                <td bgcolor="White" align="left" class="auto-style5">
                    <asp:Label ID="lbSpecShow" runat="server" CssClass="auto-style3"></asp:Label>
                    <asp:Label ID="lbLineHiddle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" align="right">
                    <asp:Label ID="Label13" runat="server" Text="Rev."></asp:Label>
                </td>
                <td bgcolor="White" align="left">
                    <asp:TextBox ID="tbRevShow" runat="server" MaxLength="2" Width="50px"></asp:TextBox>
                </td>
                <td bgcolor="White" align="right">
                    <asp:Label ID="Label15" runat="server" Text="ERS Code"></asp:Label>
                </td>
                <td bgcolor="White" align="left">
                    <asp:TextBox ID="tbErsCode" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="footer" align="center">
        <table style="width: 100%;">
            <tr>
                <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                    <asp:Button ID="btSave" runat="server" Text="Save" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
