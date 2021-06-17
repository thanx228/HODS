<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DeliveryPlanStatusPopup.aspx.vb" Inherits="MIS_HTI.DeliveryPlanStatusPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<script runat="server">

   

    Private Sub gvDN_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        
       
        
        '*** CustomerID ***'
        Dim lblCustomerID As Label = CType(e.Row.FindControl("lblCustomerID"), Label)
        If Not IsNothing(lblCustomerID) Then
            lblCustomerID.Text = e.Row.DataItem("Cus ID")
        End If

        '*** Email ***'
        Dim lblCustomerName As Label = CType(e.Row.FindControl("lblCustomerName"), Label)
        If Not IsNothing(lblCustomerName) Then
            lblCustomerName.Text = e.Row.DataItem("Cus. Name")
        End If

        
        '*** Email ***'
        Dim lblDNNo As Label = CType(e.Row.FindControl("lblDNNo"), Label)
        If Not IsNothing(lblDNNo) Then
            lblDNNo.Text = e.Row.DataItem("DN NO")
        End If
        
        
        '*** Email ***'
        Dim lblSOReq As Label = CType(e.Row.FindControl("lblSOReq"), Label)
        If Not IsNothing(lblSOReq) Then
            lblSOReq.Text = e.Row.DataItem("SO-Req.")
        End If
        
        
        '*** Name ***'
        Dim lblItem As Label = CType(e.Row.FindControl("lblItem"), Label)
        If Not IsNothing(lblItem) Then
            lblItem.Text = e.Row.DataItem("Item")
        End If

        '*** CountryCode ***'
        Dim lblSpec As Label = CType(e.Row.FindControl("lblSpec"), Label)
        If Not IsNothing(lblSpec) Then
            lblSpec.Text = e.Row.DataItem("Spec")
        End If

        '*** Budget ***'
        Dim lblDelvQty As Label = CType(e.Row.FindControl("lblDelvQty"), Label)
        If Not IsNothing(lblDelvQty) Then
            lblDelvQty.Text = FormatNumber(e.Row.DataItem("Delv. Qty"), 2)
        End If

        '*** Used ***'
        Dim lblFG As Label = CType(e.Row.FindControl("lblFG"), Label)
        If Not IsNothing(lblFG) Then
            lblFG.Text = FormatNumber(e.Row.DataItem("FG"), 2)
        End If
        
        Dim sumTotal As Double
        'sumTotal = sumTotal - e.Row.DataItem("FGQty")
    
        
        '*** Total ***'
        Dim lblFGBal As Label = CType(e.Row.FindControl("lblFGBal"), Label)
        If Not IsNothing(lblFGBal) Then
            lblFGBal.Text = FormatNumber(sumTotal, 2)
        End If

        Dim lblDelDate As Label = CType(e.Row.FindControl("lblDelDate"), Label)
        If Not IsNothing(lblDelDate) Then
            lblDelDate.Text = (e.Row.DataItem("Delv Date"))
        End If
        
        
    End Sub

</script>





<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 48px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        &nbsp;<table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Delivery Plan Status Detail"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label19" runat="server" Text="Cust ID"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label17" runat="server" Text="Cust, Name"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label3" runat="server" Text="Item"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    TTL. Delv. Qt&#39;y</td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    FG Qt&#39;y</td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    FG Bal.</td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    MO</td>
            </tr>
            <tr>
                
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCustID" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCustName" runat="server" ForeColor="Blue" 
                        style="text-align: center"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbItem" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbDelv" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbFG" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbFGBal" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
            </tr>
        </table>


        <br />


        <br />
        <asp:Label ID="Label22" runat="server" Font-Bold="True" ForeColor="Blue" 
            Text="DN Detail Report  :  "></asp:Label>


        <br />
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ   DN"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountDN" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label4" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>



        <asp:GridView ID="gvDN" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="5" onRowDataBound="gvDN_RowDataBound"
            AutoGenerateColumns="False">

            <Columns>


        <asp:TemplateField HeaderText="Cus. Name">
		<ItemTemplate>
			<asp:Label id="lblCustomerID" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


        <asp:TemplateField HeaderText="Cus. ID">
		<ItemTemplate>
			<asp:Label id="lblCustomerName" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


         <asp:TemplateField HeaderText="DN No">
		<ItemTemplate>
			<asp:Label id="lblDNNo" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


         <asp:TemplateField HeaderText="SO-Req.">
		<ItemTemplate>
			<asp:Label id="lblSOReq" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


        <asp:TemplateField HeaderText="Item">
		<ItemTemplate>
			<asp:Label id="lblItem" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


        <asp:TemplateField HeaderText="Spec">
		<ItemTemplate>
			<asp:Label id="lblSpec" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


        <asp:TemplateField HeaderText="Delv. Qty">
		<ItemTemplate>
			<asp:Label id="lblDelvQty" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


        <asp:TemplateField HeaderText="FG">
		<ItemTemplate>
			<asp:Label id="lblFG" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


        <asp:TemplateField HeaderText="FGBal">
		<ItemTemplate>
			<asp:Label id="lblFGBal" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>


             <asp:TemplateField HeaderText="Delv. Date">
		<ItemTemplate>
			<asp:Label id="lblDelDate" runat="server"></asp:Label>
		</ItemTemplate>
	    </asp:TemplateField>
             

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


        <asp:GridView ID="gvDN2" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4">
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


        <asp:Label ID="Label23" runat="server" Font-Bold="True" ForeColor="Blue" 
            Text="MO Detail Report  :  "></asp:Label>


        <br />
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label20" runat="server" Text="จำนวนรายการ   MO "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF" 
                    class="style1">
                    <asp:Label ID="Label21" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
        <br />






        <br />
    
    </div>
    </form>
</body>
</html>
