<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="UserGroup.aspx.vb" Inherits="MIS_HTI.UserGroup" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 807px;
        }
        .style5
        {
            width: 778px;
        }
        .style8
        {
            width: 792px;
        }
        .style9
        {
            width: 100%;
        }
        .style11
        {
            width: 792px;
            height: 24px;
        }
        .style12
        {
            width: 946px;
            height: 24px;
        }
        .style15
        {
            width: 235px;
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" /> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    <ContentTemplate> 

        <table class="style9">
            <tr>
                <td align="left" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="1.2em" 
                        ForeColor="Blue" Text="Level Group"></asp:Label>
                </td>
            </tr>
        </table>

    <table style="width:473px;">
            <tr>
                <td class="style11">
                    <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Group :"></asp:Label>
                </td>
                <td class="style12">
                    <asp:DropDownList ID="UserGroupDropDownList" runat="server" AutoPostBack="True" 
                        Height="30px" Width="200px">
                    </asp:DropDownList>
                    <asp:ListSearchExtender ID="UserGroupDropDownList_ListSearchExtender" 
                        runat="server" Enabled="True" TargetControlID="UserGroupDropDownList">
                    </asp:ListSearchExtender>
                </td>
                <td class="style15">
                    <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="Dept :"></asp:Label>
                </td>
                <td class="style15">
                    <asp:DropDownList ID="MenuDropDownList" runat="server" AutoPostBack="True" 
                        Height="30px" style="margin-left: 0px" Width="127px">
                    </asp:DropDownList>
                    <asp:ListSearchExtender ID="MenuDropDownList_ListSearchExtender" runat="server" 
                        Enabled="True" TargetControlID="MenuDropDownList">
                    </asp:ListSearchExtender>
                </td>
            </tr>
            </table>
            <table>
            <tr>
                <td class="style2" colspan="4">
                  
                    <asp:GridView ID="MenuGridView"   runat="server"    Width="80%"
                    onrowdatabound="MenuGridView_RowDatabound" CellPadding="4" ForeColor="#333333" 
                        GridLines="None">
                    
                        <AlternatingRowStyle BackColor="White" />
                    
                    <Columns>
                  

                       <asp:TemplateField HeaderText="Lock Menu">
                              <HeaderTemplate>
                               <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" 
              runat="server" type="checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:CheckBox ID="chkSelect"  runat="server"  />
                            </ItemTemplate>
                              <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="False" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    
 </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:Button ID="SaveButton" runat="server" Text="Save" Width="78px" />
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </ContentTemplate> 
      </asp:UpdatePanel>

<script language="javascript">

    function SelectAllCheckboxes(spanChk) {

        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
    }
</script>
</asp:Content>

