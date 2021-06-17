<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SetMoldOrder.aspx.vb" Inherits="MIS_HTI.SetMoldOrder" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            Form<uc1:Date ID="DateFrom" runat="server" />
            TO<uc1:Date ID="DateTo" runat="server" />
            


            DocNo<asp:TextBox ID="TBDocNo" runat="server"></asp:TextBox>
            


            <asp:Button ID="Button1" runat="server" Text="Search" />
            <br />
                        <asp:Button ID="BtnNewSetupOrder" runat="server" Text="New" />
            <asp:Button ID="BtnDeleteSetupOrder" runat="server" Text="delete" Visible="False" />

             <asp:ModalPopupExtender ID="modEx2" runat="server" TargetControlID="BtnNewDetail0"  PopupControlID="PanNewSetMoldDetail"
                     OkControlID="" CancelControlID="btnCancel1" DropShadow="true" BackgroundCssClass="modalBackground" >           
     </asp:ModalPopupExtender>
            <asp:Panel ID="PanNewSetMoldOrder" runat="server">
                Doc Date<uc1:Date ID="DateDocDate" runat="server" /> <br />
                Doc Time<asp:textbox runat="server"  ID="TBDocTime" /> <br />
                Remark<asp:textbox runat="server"  ID="TBRemark" /><br />
                <asp:Button runat="server" ID="btnSave" Text="Save" OnClientClick  ="btnSave_Click"  /><asp:Button runat="server" ID="btnCancel" Text="Cancel" />
            </asp:Panel>
            <asp:Panel ID="PanNewSetMoldDetail" runat="server">
                MO<asp:DropDownList runat="server" ID="DDLAllCanSelectMO" AutoPostBack="True"></asp:DropDownList>
                <br />
                Power<asp:Label runat="server" ID="LBPower"></asp:Label><br />
                MacNo<asp:DropDownList ID="DDLAllMachineNo" runat="server" AutoPostBack="True">
                </asp:DropDownList><br />
                Qty<asp:TextBox runat="server" ID="TBQty" AutoPostBack="true" ></asp:TextBox>
                <br />
                ItemNo<asp:Label runat="server" ID="LBItemNo"></asp:Label><br />
                Item Spec<asp:Label runat="server" ID="LBItemSpec"></asp:Label><br />
                Mat Code<asp:textbox runat="server" ID="TBMatCode" Width="65px"></asp:textbox><br />
                MOLD Name<asp:TextBox ID="TBMOLDName" runat="server" Width="32px"></asp:TextBox>
                <br />
                Process : <asp:Label runat="server" ID="LBProcess"></asp:Label><br />
                Std Pcs(Man) : <asp:Label runat="server" ID="LBStdPcs"></asp:Label>
                <br />
                Std Pcs(Machine) :
                <asp:Label ID="LBStdPcs0" runat="server"></asp:Label>
                <br />
                Hours : <asp:Label runat="server" ID="LBHours"></asp:Label>
                <br />
                QI:<asp:Label ID="LBQI" runat="server" ></asp:Label>
                <br />
                <asp:Button runat="server" ID="BtnSave1" Text="Save" OnClientClick  ="btnSave1_Click"  /><asp:Button runat="server" ID="btnCancel1" Text="Cancel" />
                <asp:Button runat="server" ID="BtnDelete" Text="Delete" OnClick="btnDelete_Click" />
            </asp:Panel>
            <br />

            <asp:ModalPopupExtender ID="modEx1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" DropShadow="true" OkControlID="" PopupControlID="PanNewSetMoldOrder" TargetControlID="BtnNewSetupOrder">
            </asp:ModalPopupExtender>
            
            <br />
        <asp:Panel ID="PanSetMoldOrder" runat="server" ScrollBars="Both" Height="300" Width="600">
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AllowSorting="True" DataKeyNames="DocNo" DataSourceID="SqlDataSourceSetMoldOrder">
                <Columns>
                    <asp:BoundField DataField="DocNo" HeaderText="DocNo" ReadOnly="True" SortExpression="DocNo" />
                    <asp:BoundField DataField="DocDate" HeaderText="DocDate" SortExpression="DocDate" />
                    <asp:BoundField DataField="DocTime" HeaderText="DocTime" SortExpression="DocTime" />
                    <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Print">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplPrint" runat="server" ImageUrl="~/Images/printer-icon-small.gif"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceSetMoldOrder" runat="server" ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" SelectCommand="" DeleteCommand="">
                </asp:SqlDataSource>
        </asp:Panel>
             <asp:Button ID="BtnNewDetail" runat="server" text="New" />
            <asp:Panel ID="PanSetMoldDetail" runat="server" ScrollBars="Auto" Height="300" Width="1000px">
                <asp:Label ID="LBDocNo" runat="server"  ></asp:Label>
                 <asp:Label ID="LBSeq" runat="server"  Visible="false" ></asp:Label>
                <asp:Label ID="LBDocDate" runat="server"  Visible="false" ></asp:Label>
               
                

                <asp:Button ID="BtnNewDetail0" runat="server" text="New" Height="0px" Width="0px" />

                

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DocNo,Seq" DataSourceID="SqlDataSourceSetMoldDetail">
                <Columns>
                    <asp:BoundField DataField="DocNo" HeaderText="DocNo" ReadOnly="True" SortExpression="DocNo" />
                    <asp:BoundField DataField="Seq" HeaderText="Seq" ReadOnly="True" SortExpression="Seq" />
                    <asp:BoundField DataField="MoNo" HeaderText="MoNo" SortExpression="MoNo" />
                    <asp:BoundField DataField="MoSeq" HeaderText="MoSeq" SortExpression="MoSeq" />
                    <asp:BoundField DataField="MacNo" HeaderText="MacNo" SortExpression="MacNo" />
                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                    <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" SortExpression="ItemNo" />
                    <asp:BoundField DataField="ItemSpec" HeaderText="ItemSpec" SortExpression="ItemSpec" />
                    <asp:BoundField DataField="MatCode" HeaderText="MatCode" SortExpression="MatCode" />
                    <asp:BoundField DataField="MoldName" HeaderText="MoldName" SortExpression="MoldName" />
                    <asp:BoundField DataField="Process" HeaderText="Process" SortExpression="Process" />
                    <asp:BoundField DataField="StdPcs" HeaderText="StdPcs" SortExpression="StdPcs" />
                    <asp:BoundField DataField="UseHours" HeaderText="UseHours" SortExpression="UseHours" />
                    <asp:BoundField DataField="QI" HeaderText="QI" SortExpression="QI" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceSetMoldDetail" runat="server" ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" SelectCommand=""></asp:SqlDataSource>
           
            </asp:Panel>


        </ContentTemplate>
        <Triggers >
            <asp:AsyncPostBackTrigger ControlID="BtnNewSetupOrder" />
            <asp:AsyncPostBackTrigger ControlID="DDLAllCanSelectMO"  />
            <asp:PostBackTrigger ControlID="gvshow" />
           
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>
