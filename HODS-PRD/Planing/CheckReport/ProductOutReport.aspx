<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ProductOutReport.aspx.vb" Inherits="MIS_HTI.ProductOutReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
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
    <br />
    From<uc1:Date ID="DateFrom" runat="server" />
    To<uc1:Date ID="DateEnd" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <table><tr><td>
            <asp:RadioButtonList runat="server" ID="ReportType" AutoPostBack="true"  >
                <asp:ListItem Value="All" Text="All" Selected="True" ></asp:ListItem>
                <asp:ListItem Value="WC" Text="Work Center"></asp:ListItem>
                <asp:ListItem Value="Employee" Text="Employee"></asp:ListItem>
                <asp:ListItem Value="Machine" Text="Machine"></asp:ListItem>
            </asp:RadioButtonList>
            </td>
                <td>
            <asp:CheckBoxList ID="CBLSecCate" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"   Width="680px">
            </asp:CheckBoxList>
                    </td>
            </tr></table>
            <br />
            <asp:Button ID="BtnShow" Text="Show Report" runat="server" />
            <asp:Button runat="server" ID="BtnExportExcel" Text="Export Excel" />
            <asp:GridView ID="gvShow" runat="server">
            </asp:GridView>
            <br />
            <asp:Button runat="server" ID="BtnExportDetail" Text="Export Excel" />
            <asp:GridView ID="gvShow1" runat="server">
            </asp:GridView>
            <asp:Panel runat="server" ID="Panel1">

            </asp:Panel>
            <br />


        </ContentTemplate>
        <Triggers >
          
        </Triggers>

    </asp:UpdatePanel>
  
</asp:Content>
