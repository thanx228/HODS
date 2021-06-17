<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ClearMOForMachine.aspx.vb" Inherits="MIS_HTI.ClearMOForMachine" %>
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
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <table><tr><td>
                &nbsp;</td>
                <td>
                    <uc1:Date ID="Date1" runat="server" />
                    </td>
            </tr></table>
            <br />
            <br />
            <asp:Panel runat="server" ID="Panel1">

            </asp:Panel>
            <br />


        </ContentTemplate>
        <Triggers >
          
        </Triggers>

    </asp:UpdatePanel>
  
</asp:Content>
