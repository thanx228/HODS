<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ManPower.aspx.vb" Inherits="MIS_HTI.ManPower" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 146%;
        }
        .style6
        {
            width: 65px;
        }
        .style10
        {
            width: 110px;
        }
        .style12
        {
            width: 119px;
        }
        .style14
        {
            width: 85px;
        }
        .style15
        {
            width: 154px;
        }
        .style16
        {
            width: 147px;
        }
        .style17
        {
            width: 92px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
             <table class="style3">
                 <tr>
                     <td>
                         <asp:Label ID="Label1" runat="server" Font-Size="X-Large" ForeColor="Blue" 
                             Text="ManPower Report"></asp:Label>
                     </td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
             </table>
             <table class="style3">
                 <tr>
                     <td class="style14">
                         <asp:Label ID="Label5" runat="server" Text="Date :"></asp:Label>
                     </td>
                     <td class="style15">
                         <asp:TextBox ID="txtdate" runat="server" Width="167px"></asp:TextBox>
                         <asp:TextBoxWatermarkExtender ID="txtdate_TextBoxWatermarkExtender" 
                             runat="server" Enabled="True" TargetControlID="txtdate" 
                             WatermarkText="Date Format YYYYMMDD">
                         </asp:TextBoxWatermarkExtender>
                     </td>
                     <td class="style16">
                         <asp:Label ID="Label6" runat="server" Text="Work Time Date :"></asp:Label>
                     </td>
                     <td class="style17">
                         <asp:TextBox ID="txttime" runat="server"></asp:TextBox>
                     </td>
                     <td class="style12">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style14">
                         <asp:Label ID="Label4" runat="server" Text="Work Center :"></asp:Label>
                     </td>
                     <td class="style15">
                         <asp:DropDownList ID="DDLWorkCenter" runat="server" AutoPostBack="True">
                         </asp:DropDownList>
                     </td>
                     <td class="style16">
                         <asp:Label ID="Label7" runat="server" Text="Machine Code No :"></asp:Label>
                     </td>
                     <td class="style17">
                         <asp:DropDownList ID="DDLMachine" runat="server" AutoPostBack="True">
                         </asp:DropDownList>
                     </td>
                     <td class="style12">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style14">
                         &nbsp;</td>
                     <td class="style15">

                         &nbsp;</td>
                     <td class="style16">
                         <asp:Button ID="Buview" runat="server" Text="Man" />
                     </td>
                     <td class="style17">
                         <asp:Button ID="BtEffMan" runat="server" Text="Eff Man" />
                     </td>
                     <td class="style12">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style14">
                         &nbsp;</td>
                     <td class="style15">
                         <asp:DropDownList ID="DDLDate" runat="server" AutoPostBack="True" 
                             Visible="False">
                             <asp:ListItem>01</asp:ListItem>
                             <asp:ListItem>02</asp:ListItem>
                             <asp:ListItem>03</asp:ListItem>
                             <asp:ListItem>04</asp:ListItem>
                             <asp:ListItem>05</asp:ListItem>
                             <asp:ListItem>06</asp:ListItem>
                             <asp:ListItem>07</asp:ListItem>
                             <asp:ListItem>08</asp:ListItem>
                             <asp:ListItem>09</asp:ListItem>
                             <asp:ListItem>10</asp:ListItem>
                             <asp:ListItem>11</asp:ListItem>
                             <asp:ListItem>12</asp:ListItem>
                             <asp:ListItem>13</asp:ListItem>
                             <asp:ListItem>14</asp:ListItem>
                             <asp:ListItem>15</asp:ListItem>
                             <asp:ListItem>16</asp:ListItem>
                             <asp:ListItem>17</asp:ListItem>
                             <asp:ListItem>18</asp:ListItem>
                             <asp:ListItem>19</asp:ListItem>
                             <asp:ListItem>20</asp:ListItem>
                             <asp:ListItem>21</asp:ListItem>
                             <asp:ListItem>22</asp:ListItem>
                             <asp:ListItem>23</asp:ListItem>
                             <asp:ListItem>24</asp:ListItem>
                             <asp:ListItem>25</asp:ListItem>
                             <asp:ListItem>26</asp:ListItem>
                             <asp:ListItem>27</asp:ListItem>
                             <asp:ListItem>28</asp:ListItem>
                             <asp:ListItem>29</asp:ListItem>
                             <asp:ListItem>30</asp:ListItem>
                             <asp:ListItem>31</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                     <td class="style16">
                         <asp:DropDownList ID="DDLYear" runat="server" AutoPostBack="True" 
                             Visible="False">
                             <asp:ListItem>2011</asp:ListItem>
                             <asp:ListItem>2012</asp:ListItem>
                             <asp:ListItem>2013</asp:ListItem>
                             <asp:ListItem>2014</asp:ListItem>
                             <asp:ListItem>2015</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                     <td class="style17">
                         <asp:DropDownList ID="DDLmonth" runat="server" AutoPostBack="True" 
                             Visible="False">
                         </asp:DropDownList>
                     </td>
                     <td class="style12">
                         <asp:Label ID="Label3" runat="server" Text="Month :" Visible="False"></asp:Label>
                     </td>
                     <td>
                         <asp:Label ID="Label2" runat="server" Text="Year :" Visible="False"></asp:Label>
                     </td>
                     <td>
                         &nbsp;</td>
                 </tr>
             </table>
             <br />
             <br />
         </ContentTemplate>
     </asp:UpdatePanel>
     <br />
    <br />
    <br />
</asp:Content>
