
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MOProcessMachine.aspx.vb" Inherits="MIS_HTI.MOProcessMachine" %>
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
    <style type="text/css">
   .inlineBlock {
       display: inline-block; 
       font: 10pt verdana;
       border-style:double;
   }
   .MainPanel {
       border-width:3px;border-style:dashed;border-color:#FFAC55;padding:5px;
   }
   .HyperLinkCss {
       cursor: pointer;
   }

      .ButtonCss {
       cursor:none ;
   }

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            Spec<asp:TextBox ID="TBSpec" runat="server" ></asp:TextBox>
            <br />
            <br />
            <asp:Button runat="server" ID="BtnSearch" Text="Search"></asp:Button>
            <asp:Panel ID="ShowPanel" runat="server"  BorderWidth="0">
            </asp:Panel>

        </ContentTemplate>
        <Triggers >
            
           
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>

