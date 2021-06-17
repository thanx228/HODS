
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SetupMoldSearch.aspx.vb" Inherits="MIS_HTI.SetupMoldSearch" %>
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
        .modalBackground 
{ 
    height:100%; 
    background-color:#EBEBEB; 
    filter:alpha(opacity=70); 
    opacity:0.7; 
} 
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:Date ID="Date1" runat="server" />
            <asp:Button runat="server" ID="BtnSearch" Text="Search" />
        <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                            <Columns>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>
        </ContentTemplate>
        <Triggers >

           
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>

