<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BackLogNew.aspx.vb" Inherits="MIS_HTI.BackLogNew" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/ModalPopup.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table bgcolor="White" style="width: 95%;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Ship Date From"></asp:Label>
                    </td>
                    <td>
                        <uc2:Date ID="UcDateFrom" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Ship Date  To"></asp:Label>
                    </td>
                    <td>
                        <uc2:Date ID="UcDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbItem" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbCustCode" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Cust Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbCustName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Plant"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbPlant" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="CbCallIn" runat="server" Checked="True" Text="Call In" />
                        <asp:CheckBox ID="CbBackLog" runat="server" Text="Back log" />
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg')">
                        <asp:Button ID="BtShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="BtExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc1:GridviewShow ID="UcGv" runat="server" />
            <br />
            <asp:Button ID="BtnTemp" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender 
                ID="mpeShow" 
                runat="server" 
                CancelControlID="LbClose" 
                PopupControlID="pnShowGV" 
                TargetControlID="BtnTemp"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnShowGV" runat="server" CssClass="modalPopup">
                <div class="header" >
                    <div class="header1" >
                        <h2>M/O Process Detail</h2>
                    </div>
                    <div class="header2" >
                        <asp:LinkButton ID="LbClose" runat="server" >X</asp:LinkButton>
                    </div>
                </div>
                <div class="body"> 
                    <div class="container container-fluid">
                       
                        <div class="row">
                          <div class="col">
                             <asp:Label ID="Label13" runat="server" Text="Item"></asp:Label>
                          </div>
                          <div class="col">
                              <asp:Label ID="LbItem" runat="server" ForeColor="Blue"></asp:Label>
                          </div>
                          <div class="col">
                              <asp:Label ID="Label14" runat="server" Text="Spec"></asp:Label>
                          </div>
                          <div class="col">
                              <asp:Label ID="LbSpec" runat="server" ForeColor="Blue"></asp:Label>
                          </div>
                        </div> 
                    </div>
                    <div class="overflow overflow-auto">                       
                       <asp:GridView 
                            ID="GvDetail" 
                            runat="server"
                           CssClass="table table-sm table-condensed table-striped table-hover"
                          >
                        </asp:GridView>
                    </div> 
                </div> 
                <div class="footer">
                    <asp:Button ID="BtRefresh" runat="server" Text="Show Again" />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
