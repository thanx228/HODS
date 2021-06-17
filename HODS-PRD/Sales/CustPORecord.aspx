<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="CustPORecord.aspx.vb" Inherits="MIS_HTI.CustPORecord" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc3" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize:10
              });
   }
    
    
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
            height: 21px;
            width: 792px;
        }
        .auto-style3 {
            height: 30px;
        }



        </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }
     </script>
    <style type="text/css">
        .auto-style3 {
            width: 95%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TcMain" runat="server" ActiveTabIndex="0" Width="95%" AutoPostBack="True">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    
                   
                    <HeaderTemplate>
                        
                        
                                                Record
                    
                   
                    </HeaderTemplate>
                    
                    
                   
                    <ContentTemplate>
                        
                        
                                               
                       
                        <table style="width: 95%;">
                            
                            
                                                       
                           
                            <tr>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label27" runat="server" Text="Cust PO"></asp:Label>
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:TextBox ID="TbCustPORecord" runat="server"></asp:TextBox>
                                    
                                    

                                                                       
                                   
                                    <asp:Label ID="lbCustPO" runat="server" ForeColor="Blue"></asp:Label>
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label29" runat="server" Text="Cust ID"></asp:Label>
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <uc4:DropDownListUserControl ID="UcDdlCustID" runat="server" />
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                           
                           
                            </tr>
                            
                            
                                                       
                           
                            <tr>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label33" runat="server" Text="Date"></asp:Label>
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <uc2:Date ID="uCDate" runat="server" />
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label30" runat="server" Text="Plant"></asp:Label>
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <uc4:DropDownListUserControl ID="UcDdlPlant" runat="server" />
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                           
                           
                            </tr>
                            
                            
                                                       
                           
                            <tr>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label42" runat="server" Text="Remark"></asp:Label>
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White" colspan="3">
                                    
                                    
                                                                       
                                   
                                    <asp:TextBox ID="TbRemark" runat="server" Width="300px"></asp:TextBox>
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                           
                           
                            </tr>
                            
                            
                                                       
                           
                            <tr>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label37" runat="server" Text="Status"></asp:Label>
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <uc4:DropDownListUserControl ID="UcDdlStatus" runat="server" />
                                    
                                    

                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White"> </td>
                                
                                
                                                               
                               
                                <td bgcolor="White"> </td>
                                
                                
                                                           
                           
                            </tr>
                            
                            
                                                   
                       
                        </table>
                        
                        
                                            
                   
                    </ContentTemplate>
                    
                    
               
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    
                   
                    <HeaderTemplate>
                        
                        
                                                Search
                    
                   
                    </HeaderTemplate>
                    
                    
                   
                    <ContentTemplate>
                        
                        
                                               
                       
                        <table style="width: 95%;">
                            
                            
                                                       
                           
                            <tr>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label6" runat="server" Text="Cust PO"></asp:Label>
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:TextBox ID="tbCustPO" runat="server"></asp:TextBox>
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White"> </td>
                                
                                
                                                               
                               
                                <td bgcolor="White"> </td>
                                
                                
                                                           
                           
                            </tr>
                            
                            
                                                       
                           
                            <tr>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label3" runat="server" Text="Cust ID"></asp:Label>
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:TextBox ID="tbCust" runat="server"></asp:TextBox>
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label41" runat="server" Text="Plant"></asp:Label>
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <uc4:DropDownListUserControl ID="UcDdlPlantSearch" runat="server" />
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                           
                           
                            </tr>
                            
                            
                                                       
                           
                            <tr>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label5" runat="server" Text="Date From"></asp:Label>
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <uc2:Date ID="ucDateFrom" runat="server" />
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <asp:Label ID="Label40" runat="server" Text="Date To"></asp:Label>
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                               
                               
                                <td bgcolor="White">
                                    
                                    
                                                                       
                                   
                                    <uc2:Date ID="ucDateTo" runat="server" />
                                    
                                    
                                                                   
                               
                                </td>
                                
                                
                                                           
                           
                            </tr>
                            
                            
                                                   
                       
                        </table>
                        
                        
                                            
                   
                    </ContentTemplate>
                    
                    
               
                </asp:TabPanel>
            </asp:TabContainer>
             <table style="width: 95%;">
                    <tr>
                        <td align="center" 
                            style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                            <asp:Button ID="btSave" runat="server" Text="Save" />
                             &nbsp;<asp:Button ID="btNew" runat="server" Text="New" />
                             &nbsp;<asp:Button ID="BtSearch" runat="server" Text="Search" />
                        </td>
                    </tr>
            </table>
            <uc3:CountRow ID="ucCount" runat="server" />
           
                <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:ButtonField ButtonType="Image" CommandName="onEdit" HeaderText="Head" 
                        ImageUrl="~/Images/edit.gif" Text="Button" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                    <asp:TemplateField HeaderText="Body">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CustPO" HeaderText="Cust PO" />
                    <asp:BoundField DataField="DocDate" HeaderText="Doc Date" />
                    <asp:BoundField DataField="Cust" HeaderText="Cust." />
                    <asp:BoundField DataField="Plant" HeaderText="Plant" />                    
                    <asp:BoundField DataField="StatusInfo" HeaderText="Status" />
                    <asp:BoundField DataField="CreateBy" HeaderText="Create By" />
                    <asp:BoundField DataField="CreateDate" HeaderText="Create Date" />
                    <asp:BoundField DataField="ChangeBy" HeaderText="Change By" />
                    <asp:BoundField DataField="ChangeDate" HeaderText="Change Date" />
                     <asp:BoundField DataField="COUNT_ITEM" HeaderText="Item" /> 
                     <asp:BoundField DataField="CLOSE_ITEM" HeaderText="Close Bal Item" />  
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
                       
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
