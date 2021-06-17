<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="Scrap.aspx.vb" Inherits="MIS_HTI.Scrap" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 148%;
        }
        .style8
        {
            width: 100%;
        }
        .style10
        {
            width: 129px;
        }
        .style11
        {
            width: 135px;
        }
        .style12
        {
            width: 112px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table class="style8">
                <tr>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" BorderStyle="Solid" Font-Bold="True" 
                            Font-Size="Large" ForeColor="Blue" Text="Rework or Scrap Record"></asp:Label>
                    </td>
                </tr>
            </table>

        <fieldset>
        <legend>Search Data</legend> 

            <table style="width:75%; background-color: #FFFFFF;">
                <tr>
                    <td class="style12">
                        <asp:Label ID="Label16" runat="server" Text="So No."></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="SoNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label13" runat="server" Text="Orde Date :"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="ucDateSaleOrder" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        <asp:Label ID="Label11" runat="server" Text="Work Order Type :"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:DropDownList ID="ddlMOType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label12" runat="server" Text="Work Order No :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="wno" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        <asp:Label ID="Label14" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="item" runat="server"></asp:TextBox>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label17" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="spec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        <asp:Label ID="Label18" runat="server" Text="From Date :"></asp:Label>
                    </td>
                    <td class="style10">
                        <uc1:Date ID="ucDateFrom" runat="server" />
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label19" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="ucDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style12">
                        Work Center :
                    </td>
                    <td class="style10">
                        <asp:DropDownList ID="ddlWC" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        Approval Indicator</td>
                    <td>
                        <asp:CheckBoxList ID="cblApp" runat="server" RepeatColumns="2" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="Y">Y : Approved</asp:ListItem>
                            <asp:ListItem Value="N">N : Not Approved</asp:ListItem>
                            <asp:ListItem Value="U">U : Approved Failed</asp:ListItem>
                            <asp:ListItem Value="V">V : Canceled</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="Busearch" runat="server" Text="Search" Width="100px" />
                        &nbsp;<asp:Button ID="BuExcel" runat="server" Text="Excel" Width="100px" />
                    </td>
                </tr>
            </table>
        </fieldset>
            <br />

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />

            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" 
                DataKeyNames="TA001,TA002" DataSourceID="SqlDataSource1" ForeColor="#333333" 
                GridLines="None" Width="236px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                 <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="TL014" HeaderText="App. Status" />
                    <asp:BoundField DataField="TL001" HeaderText="Tran Type" 
                        SortExpression="TL001" >
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TL002" HeaderText="Tran No" SortExpression="TL002" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
              
                      <asp:BoundField DataField="TM003" HeaderText="Tran Seq" />

                    <asp:BoundField DataField="TA001" HeaderText="W Type" ReadOnly="True" 
                        SortExpression="TA001" >
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA002" HeaderText="W No" ReadOnly="True" 
                        SortExpression="TA002" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TL003" HeaderText="Scrap Date" 
                        SortExpression="TL003" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                   <asp:BoundField DataField="TA006" HeaderText="Item" SortExpression="TA006" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                   <asp:BoundField DataField="TA026" HeaderText="So Type" SortExpression="TA026" >
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                   <asp:BoundField DataField="TA027" HeaderText="SO No" SortExpression="TA027" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                   <asp:BoundField DataField="TA035" HeaderText="Spec" SortExpression="TA035" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA015" HeaderText="So Qty" SortExpression="TA015" DataFormatString ={0:F3} >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Dept" HeaderText="Dept" SortExpression="Dept" >
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SQTY" HeaderText="Scrap Qty" SortExpression="SQTY" DataFormatString = {0:F3} >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
              
                      <asp:BoundField DataField="TB005" HeaderText="TFrom" SortExpression="TB005" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>

                      <asp:BoundField DataField="TB006" HeaderText="TFName" SortExpression="TB006" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>

                      <asp:BoundField DataField="TB014" HeaderText="Remark" SortExpression="TB014" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>

                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" 
                    Wrap="False" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="False" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                SelectCommand="select TA001,TA002,TL003,TA006,TA024,TA025,TA026,TA027,TA035,TA015,TL006 as Dept,TM007 AS SQTY,TL001,TL002,TB005,TB006,TB014  from INVTL SH inner join INVTM SL on(SH.TL001 = SL.TM001) and (SH.TL002 = SL.TM002) inner join SFCTC TL on(SL.TM009 = TL.TC001) AND (SL.TM010 = TL.TC002) inner JOIN MOCTA MO ON(TL.TC004 = MO.TA001) AND (TL.TC005 = MO.TA002) inner JOIN SFCTB TH on(SL.TM009 = TH.TB001) and (SL.TM010 = TH.TB002)"></asp:SqlDataSource>
            <asp:Button ID="btPrint" runat="server" Text="Print Report" />
            <br />
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="BuExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
