<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS_SUB.Master" CodeBehind="PlanScheduleCheck.aspx.vb" Inherits="MIS_HTI.PlanScheduleCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 196px;
        }
        .style6
        {
            width: 22%;
        }
        .style8
        {
            width: 203px;
        }
        .style9
        {
            width: 213px;
        }
        .style10
        {
            height: 23px;
        }
        .style11
        {
            height: 23px;
            width: 35px;
        }
        .style12
        {
            width: 35px;
        }
        .auto-style1 {
            width: 242px;
        }
    </style>
    <script src="../Scripts/jquery-3.5.1.min.js" type="text/javascript"></script>
     <script type="text/javascript">         

         $(document).ready(function () {             
             $(window).keydown(function (event) {
                 if (event.keyCode == 13) {
                     event.preventDefault();
                     return false;
                 }
             });
             window.moveTo(0, 0);
             //window.resizeTo(screen.availWidth, screen.availHeight);
             window.resizeTo(screen.width, screen.height);

         });
     </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
            <table style="width: 95%;">
                <tr style="width: 50%;">
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Plan Date"></asp:Label>
                    </td>
                    <td class="style4" style="background-color: #FFFFFF">
                        <asp:Label ID="lbPlanDate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="lbWc" runat="server" ForeColor="Blue" Visible="False"></asp:Label>
                        <asp:Label ID="lbWcName" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td align="center" class="auto-style1" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
           
            <table style="width: 95%;">
                <tr>
                    <td align="center" bgcolor="White" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btBefore" runat="server" Text="&lt;&lt;" Width="80px" />
                        &nbsp;<asp:Button ID="BtShow" runat="server" Text="Show Again" />
                        &nbsp;<asp:Button ID="BtExport" runat="server" Text="Excel Export" />
                        &nbsp;<asp:Button ID="btAfter" runat="server" Text="&gt;&gt;" Width="80px" />
                    </td>
                </tr>
            </table>
    <table style="width:95%;">
        <tr>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label9" runat="server" Text="Plan"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label10" runat="server" Text="Actual Transfer"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label11" runat="server" Text="Actual Plan"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label12" runat="server" Text="% Actual On Plan"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label13" runat="server" Text="No Plan"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label15" runat="server" Text="% No Plan"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label20" runat="server" Text="WC Load"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label16" runat="server" Text="Man Time"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style11">
                <asp:Label ID="Label17" runat="server" Text="Mch Time"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbPlan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbActTran" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbActPlan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbPer" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbNoPlan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbPerNoPlan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbWcLoad" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbManTime" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style12">
                <asp:Label ID="lbMchTime" runat="server" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        </table>
    <asp:Label ID="Label8" runat="server" Text="Detail" Font-Size="1em" 
        ForeColor="Blue"></asp:Label>
    
            <asp:GridView ID="gvPlan" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" Width="443px">
                <Columns>
                    <asp:BoundField DataField="Urgent" HeaderText="OT" />
                    <asp:BoundField DataField="PlanDate" HeaderText="Plan Date" />
                    <asp:BoundField DataField="TA001" HeaderText="MO Type" />
                    <asp:BoundField DataField="TA002" HeaderText="MO No." />
                    <asp:BoundField DataField="TA003" HeaderText="MO Seq." />
                    <asp:BoundField DataField="MW002" HeaderText="Operation" />
                    <asp:BoundField DataField="TA035" HeaderText="Spec" />
                    <asp:BoundField DataField="TA015" DataFormatString="{0:N}" 
                        HeaderText="MO Qty" />
                    <asp:BoundField DataField="PlanQty" HeaderText="Plan Qty" 
                        DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ActualQty" HeaderText="Actual Qty" 
                        DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UnAppQty" DataFormatString="{0:N}" 
                        HeaderText="Not App Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ScrapQty" DataFormatString="{0:N}" HeaderText="Scrap Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                     </asp:BoundField>
                    <asp:BoundField DataField="ActualDate" HeaderText="Actual Date" />
                    <asp:BoundField DataField="TranAcc" HeaderText="Create Date Time" />
                    <asp:BoundField DataField="A" DataFormatString="{0:N}" 
                        HeaderText="Labor Usage(Min)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="B" DataFormatString="{0:N}" 
                        HeaderText="Machine Usage(Min)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MA002" HeaderText="Cust Name" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            
        </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="BtExport" />
         </Triggers>
    </asp:UpdatePanel>
    <asp:Timer ID="Timer1" runat="server">
    </asp:Timer>
</asp:Content>
