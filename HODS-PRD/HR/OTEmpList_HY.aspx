<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTEmpList_HY.aspx.vb" Inherits="MIS_HTI.OTEmpList_HY" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 86px;
        }
        .auto-style4 {
            width: 133px;
        }
        .auto-style8 {
            width: 240px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:80%;">
                <tr>
                    <td class="auto-style3" bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="รหัสพนักงาน"></asp:Label>
                    </td>
                    <td class="auto-style8" bgcolor="White">
                        <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4" bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="เพศ"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlGender" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="ชื่อ ภาษาไทย"></asp:Label>
                    </td>
                    <td class="auto-style8" bgcolor="White">
                        <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4" bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="นามสกุล ภาาษาไทย"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSurName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="แผนก"></asp:Label>
                    </td>
                    <td class="auto-style8" bgcolor="White">
                        <asp:DropDownList ID="ddlDept" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style4" bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="ตำแหน่ง"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlPos" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" bgcolor="White">
                        <asp:Label ID="Label12" runat="server" Text="Shift Day"></asp:Label>
                    </td>
                    <td class="auto-style8" bgcolor="White">
                        <asp:DropDownList ID="ddlShiftDay" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style4" bgcolor="White">
                        <asp:Label ID="Label11" runat="server" Text="Shift"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlShift" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" bgcolor="White">
                        <asp:Label ID="Label9" runat="server" Text="สถานะ"></asp:Label>
                    </td>
                    <td class="auto-style8" bgcolor="White">
                        <asp:DropDownList ID="ddlStus" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style4" bgcolor="White">
                        <asp:Label ID="Label10" runat="server" Text="สายรถ"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlPickUpLocation" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
    <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Report" />
                        &nbsp;<asp:Button ID="btNew" runat="server" Text="New" />
                        &nbsp;<asp:Button ID="btSave" runat="server" Text="Save" />
                         &nbsp;<asp:Button ID="btCancel" runat="server" Text="Cancel" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export excel" />
                        <br />
                    </td>
                </tr>
            </table>
              <uc2:CountRow ID="CntRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns >
                                <asp:ButtonField ButtonType="Image" HeaderText="Edit" 
                                    ImageUrl="~/Images/edit.gif" Text="Button" CommandName="onEdit" />
                       <asp:BoundField DataField="A" HeaderText="Emp No" />
                                <asp:BoundField DataField="B" HeaderText="Emp Name" />
                                <asp:BoundField DataField="C" HeaderText="Gender" />
                                <asp:BoundField DataField="D" HeaderText="Dept" />
                    <asp:BoundField DataField="E" HeaderText="Position" />
                    <asp:BoundField DataField="F" HeaderText="Pick Up Location" />
                    <asp:BoundField DataField="G" HeaderText="Status" />
                     <asp:BoundField DataField="H" HeaderText="Shift" />
                     <asp:BoundField DataField="I" HeaderText="Shift Day" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Wrap="False" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
           <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
