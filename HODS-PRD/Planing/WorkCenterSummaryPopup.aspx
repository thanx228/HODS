<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkCenterSummaryPopup.aspx.vb" Inherits="MIS_HTI.WorkCenterSummaryPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Inventory Status Detail"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label19" runat="server" Text="WC Name"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label17" runat="server" Text="Process Total"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label3" runat="server" Text="Process Finished"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label8" runat="server" Text="Process On Hand"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    Process Pending</td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    Finished%</td>
            </tr>
            <tr>
                
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbWC" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbProTotal" runat="server" ForeColor="Blue" 
                        style="text-align: center"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbProFinish" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbOnHand" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbPending" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbFinish" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
            </tr>
        </table>


        <br />


        <br />
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ   MO OnHand"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label4" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMO" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="M01" HeaderText="Cus. Name" />
                    <asp:BoundField DataField="M02" HeaderText="WC Name" />
                    <asp:BoundField HeaderText="SO Detail" DataField="M03">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="MO Detail" DataField="M04">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Item" DataField="M05">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="M06" HeaderText="Spec.">
                    </asp:BoundField>
                    <asp:BoundField DataField="M10" HeaderText="Plan Start Date" />
                    <asp:BoundField DataField="M07" HeaderText="MO Qt'y"
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="M11" HeaderText="Finish Qt'y" SortExpression="M11" 
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="WIP Qt'y" DataField="M08"
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="MO Status" DataField="M09">
                    </asp:BoundField>
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






        <br />
        <br />


        <br />
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label20" runat="server" Text="จำนวนรายการ   MO Pending"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountMOp" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label21" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMOp" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="M01" HeaderText="Cus. Name" />
                    <asp:BoundField DataField="M02" HeaderText="WC Name" />
                    <asp:BoundField HeaderText="SO Detail" DataField="M03">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="MO Detail" DataField="M04">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Item" DataField="M05">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="M06" HeaderText="Spec.">
                    </asp:BoundField>
                    <asp:BoundField DataField="M10" HeaderText="Plan Start Date" />
                    <asp:BoundField DataField="M07" HeaderText="MO Qt'y"
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="M11" HeaderText="Finish Qt'y" SortExpression="M11" 
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="WIP Qt'y" DataField="M08"
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="MO Status" DataField="M09">
                    </asp:BoundField>
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






        <br />
    
    </div>
    </form>
</body>
</html>
