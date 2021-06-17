<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="POPricesNoInquiry.aspx.vb" Inherits="MIS_HTI.POPricesNoInquiry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            height: 58px;
        }
        .style4
        {
            height: 77px;
        }
        .style5
        {
            height: 58px;
            width: 212px;
        }
        .style6
        {
            height: 77px;
            width: 212px;
        }
        .style8
        {
            width: 212px;
        }
        .style12
        {
            height: 58px;
            width: 213px;
        }
        .style13
        {
            height: 77px;
            width: 213px;
        }
        .style14
        {
            width: 213px;
        }
        .style15
        {
            height: 58px;
            width: 251px;
        }
        .style16
        {
            height: 77px;
            width: 251px;
        }
        .style17
        {
            width: 251px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    <ContentTemplate> 
    <table class="TSHOW">
        <tr>
               <td  background="/Images/btt.jpg"  height="25px"  colspan="4">
                   PO Prices &gt; Inquiry Prices and Not have Quotation</td>
       
        </tr>
        <tr>
            <td class="style3">
                From Date:</td>
            <td class="style12">
               <asp:TextBox ID="Fromdate" runat="server" Height="20px" Width="92px"></asp:TextBox>
                <cc1:CalendarExtender ID="Fromdate_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="Fromdate" Format="MM/dd/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td class="style15">
                To:<asp:TextBox ID="Todate" runat="server" Width="83px"></asp:TextBox>
                <cc1:CalendarExtender ID="Todate_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="Todate" Format="MM/dd/yyyy">
                </cc1:CalendarExtender>
                </td>
                 <td class="style3">
                     &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                </td>
            <td class="style13">
                <asp:Button ID="btPreview" runat="server" Text="  Preview  " />
            </td>
            <td class="style16">
            </td>
                 <td class="style4">
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style14">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
                 <td>
                &nbsp;</td>
        </tr>
    </table>
    </ContentTemplate> 
      </asp:UpdatePanel>
       <script language="javascript">

           function chkdata() {
               if (document.getElementById("ctl00_MainContent_Todate").value == '' || document.getElementById("ctl00_MainContent_Fromdate").value == '') {
                   alert("Please Select Date");
                   return false;
               }
           }
</script>
</asp:Content>
