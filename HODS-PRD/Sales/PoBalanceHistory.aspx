<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PoBalanceHistory.aspx.vb" Inherits="MIS_HTI.PoBalanceHistory" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 109px;
        }
        .style3
        {
            width: 188px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="style1">
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label1" runat="server" Text="PO :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                DataSourceID="lDataSourceSearch" DataTextField="TB048" DataValueField="TB048">
                            </asp:DropDownList>
                            <asp:ListSearchExtender ID="DropDownList1_ListSearchExtender" runat="server" 
                                Enabled="True" TargetControlID="DropDownList1">
                            </asp:ListSearchExtender>

                            <asp:SqlDataSource ID="lDataSourceSearch" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                                SelectCommand="select distinct TB048 from ACRTB where TB048 <> '' ORDER BY TB048 "></asp:SqlDataSource>
                        </td>
                        <td>
                            <asp:Button ID="BuSearch" runat="server" Text="Search" Visible="False" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Sum Balance" />
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
                    </tr>
                </table>
                <table class="style1">
                    <tr>
                        <td align="center">
                        <fieldset>
                        <legend>Sales Order</legend> 
                         <asp:GridView ID="GridSo" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="Type,NO,Seq" DataSourceID="DataSourceSO">
                                <Columns>
                                    <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" 
                                        SortExpression="Type" />
                                    <asp:BoundField DataField="NO" HeaderText="NO" ReadOnly="True" 
                                        SortExpression="NO" />
                                    <asp:BoundField DataField="Seq" HeaderText="Seq" ReadOnly="True" 
                                        SortExpression="Seq" />
                                    <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" 
                                        SortExpression="Description" />
                                    <asp:BoundField DataField="Spec" HeaderText="Spec" SortExpression="Spec" />
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                                    <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" />
                                </Columns>
                            </asp:GridView>
                             <asp:SqlDataSource ID="DataSourceSO" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" SelectCommand="select TD001 AS Type,TD002 AS NO,TD003 AS Seq,TD004 AS Item,TD005 AS Description,TD006 AS Spec,TD008 AS Qty,TD010 AS Unit 
from [JINPAO80].[dbo].[COPTD]
where TD027 = @PO">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList1" Name="PO" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </fieldset>
                           
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        <fieldset>
                        <legend>Sales Invoice</legend>
                         <asp:GridView ID="Invoice" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="Type,NO,Seq" DataSourceID="DataSourceInvoice">
                                <Columns>
                                    <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" 
                                        SortExpression="Type" />
                                    <asp:BoundField DataField="NO" HeaderText="NO" ReadOnly="True" 
                                        SortExpression="NO" />
                                    <asp:BoundField DataField="Seq" HeaderText="Seq" ReadOnly="True" 
                                        SortExpression="Seq" />
                                    <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" 
                                        SortExpression="Description" />
                                    <asp:BoundField DataField="Spec" HeaderText="Spec" SortExpression="Spec" />
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                                </Columns>
                            </asp:GridView>
                               <asp:SqlDataSource ID="DataSourceInvoice" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" SelectCommand="SELECT TB001 AS Type,TB002 AS NO,TB003 as Seq,TB039 AS Item,TB040 AS Description,TB041 AS Spec,TB022 AS Qty
FROM [JINPAO80].[dbo].[ACRTB]
where TB048 = @PO ORDER BY TB039 ">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList1" Name="PO" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </fieldset>

                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        <fieldset>
                        <legend>Po Balance From Sales Order</legend> 

                            <asp:GridView ID="GridBalance" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="SqlDataSource2">
                                <Columns>
                                    <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                    <asp:BoundField DataField="SQty" HeaderText="So Qty" SortExpression="SQty" />
                                    <asp:BoundField DataField="IQty" HeaderText="Invoice Qty" SortExpression="IQty" />

                                     <asp:BoundField DataField="PoQty" HeaderText="PoBalance Qty" SortExpression="PoQty" />
                                   
                                    <asp:BoundField DataField="BalQty" HeaderText="Balance Qty" 
                                        SortExpression="BalQty" />

                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                                SelectCommand="SELECT * FROM [SumPo]"></asp:SqlDataSource>

                        </fieldset> 
                           
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        <fieldset>
                        <legend>Po Balance From ERP Report</legend> 

                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="SqlDataSource1">
                                <Columns>
                                    <asp:BoundField DataField="SoType" HeaderText="SoType" 
                                        SortExpression="SoType" />
                                    <asp:BoundField DataField="CustID" HeaderText="CustID" 
                                        SortExpression="CustID" />
                                    <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" 
                                        SortExpression="Description" />
                                    <asp:BoundField DataField="Spec" HeaderText="Spec" SortExpression="Spec" />
                                    <asp:BoundField DataField="Po" HeaderText="Po" SortExpression="Po" />
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ReadOnly="True" 
                                        SortExpression="Balance" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                                SelectCommand="select SoType,CustID,Item,Description,Spec,Po,Qty,TB022 - Qty as Balance from [DBMIS].[dbo].[PoBalance] P left join [JINPAO80] .[dbo].[ACRTB] A on (P.Item = A.TB039) and (P.Po = A.TB048) where P.Po = @Po">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList1" Name="Po" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </fieldset>
                         
                        </td>
                    </tr>
                </table>
<br />
<br />

            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    
    </div>
    </form>
</body>
</html>
