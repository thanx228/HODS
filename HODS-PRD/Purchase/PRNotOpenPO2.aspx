<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PRNotOpenPO2.aspx.vb" Inherits="MIS_HTI.PRNotOpenPO2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .style31
        {
            width: 155px;
            height: 30px;
        }
        .style32
        {
            width: 296px;
            height: 30px;
        }
        .style27
        {
            width: 193px;
            }
        .style21
        {
            width: 155px;
            height: 28px;
        }
        .style22
        {
            width: 296px;
            height: 28px;
        }
        .style6
        {
            height: 26px;
        }
        .style40
        {
            height: 29px;
        }
        .style42
        {
            width: 192px;
            height: 28px;
        }
        .style52
        {
            width: 192px;
            height: 30px;
        }
        .style57
        {
            width: 122px;
            height: 25px;
        }
        .style58
        {
            width: 296px;
            height: 25px;
        }
        .style59
        {
            height: 25px;
        }
        .style70
        {
            width: 187px;
            height: 100px;
        }
        .style79
        {
            height: 29px;
            width: 187px;
        }
        .style83
        {
            width: 173px;
        }
        .style84
        {
            height: 29px;
            width: 173px;
        }
        .style85
        {
            width: 173px;
            height: 30px;
        }
        .style86
        {
            width: 173px;
            height: 25px;
        }
        .style88
        {
            width: 192px;
            height: 25px;
        }
        .style93
        {
            height: 29px;
            width: 166px;
        }
        .style94
        {
            width: 166px;
        }
        .style95
        {
            width: 122px;
            height: 29px;
        }
        .style100
        {
            width: 187px;
        }
        .style101
        {
            width: 187px;
            height: 2px;
        }
        .style102
        {
            width: 62px;
            height: 25px;
        }
        .style103
        {
            height: 25px;
            width: 270px;
        }
        .style105
        {
            width: 155px;
        }
        .style106
        {
            width: 155px;
            height: 25px;
        }
        .style109
        {
            height: 60px;
        }
        .style110
        {
            height: 100px;
        }
    .style1
    {
        width: 282px;
    }
        .auto-style1 {
            height: 25px;
            width: 187px;
        }
        .auto-style2 {
            width: 187px;
            height: 5px;
        }
        .auto-style3 {
            height: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" 
                Height="388px" VerticalStripWidth="200px" Width="987px">
                
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    
                   
                    <HeaderTemplate>
                        
                       
                        PR
                   
                    </HeaderTemplate>
                    
                   
                    <ContentTemplate>
                        
                       
                        <table bgcolor="White" style="width: 99%; height: 127px;">
                            
                           
                            <tr>
                                
                               
                                <td class="auto-style1" style="border: thin groove #808000; vertical-align: top">
                                    
                                   
                                    <asp:Label ID="Label71" runat="server" Text="SO Type :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td colspan="3" style="border: thin groove #808000" class="style59">
                                    
                                   
                                    <asp:CheckBoxList ID="cblSoType" runat="server">
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style85">
                                    
                                   
                                    <asp:Label ID="Label25" runat="server" Text="Include"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style52">
                                    
                                   
                                    <asp:CheckBox ID="cbNoneSO" runat="server" Text="None SO Type" />
                                    
                                   
                                    <asp:CheckBox ID="cbAll" runat="server" Checked="True" Text="Show All " />
                                    
                               
                                </td>
                                
                               
                                <td class="style31">
                                    
                                   
                                    <asp:Label ID="Label26" runat="server" Text="Approve :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style32">
                                    
                                   
                                    <asp:CheckBoxList ID="CblApp" runat="server" RepeatDirection="Horizontal" 
                                        Width="379px">
                                        
                                       
                                        <asp:ListItem Value="Y">Y : Approved</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="N">N : Not Approved</asp:ListItem>
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style84" style="border: thin groove #808000; vertical-align: top">
                                    
                                   
                                    <asp:Label ID="Label27" runat="server" Text="PR Type :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style40" colspan="3" style="border: thin groove #808000">
                                    
                                   
                                    <asp:CheckBoxList ID="cblPrType" runat="server">
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style83">
                                    
                                   
                                    <asp:Label ID="Label28" runat="server" Text="PR No :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style100">
                                    
                                   
                                    <asp:TextBox ID="txtno" runat="server"></asp:TextBox>
                                    
                               
                                </td>
                                
                               
                                <td class="style105">
                                    
                                   
                                    <asp:Label ID="Label29" runat="server" Text="Code Type"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style27">
                                    
                                   
                                    <asp:CheckBoxList ID="cblCodeType" runat="server" RepeatDirection="Horizontal" 
                                        Width="377px">
                                        
                                       
                                        <asp:ListItem Value="1">Materails</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="2">FG</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="3">Semi FG</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="4">SP and Other</asp:ListItem>
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style86">
                                    
                                   
                                    SO No. :</td>
                                
                               
                                <td class="style88">
                                    
                                   
                                    <asp:TextBox ID="txtSONo" runat="server"></asp:TextBox>
                                    
                               
                                </td>
                                
                               
                                <td class="style106">
                                    
                                   
                                    Plan Batch No. :</td>
                                
                               
                                <td class="style58">
                                    
                                   
                                    <asp:TextBox ID="txtPlanBatch" runat="server"></asp:TextBox>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style86">
                                    
                                   
                                    <asp:Label ID="Label31" runat="server" Text="Item :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style88">
                                    
                                   
                                    <asp:TextBox ID="txtitem" runat="server"></asp:TextBox>
                                    
                               
                                </td>
                                
                               
                                <td class="style106">
                                    
                                   
                                    <asp:Label ID="Label32" runat="server" Text="Spec :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style58">
                                    
                                   
                                    <asp:TextBox ID="txtspec" runat="server"></asp:TextBox>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style83">
                                    
                                   
                                    PR Close Status :</td>
                                
                               
                                <td class="style59" colspan="3">
                                    
                                   
                                    <asp:CheckBoxList ID="cblCloseStatus" runat="server" 
                                        RepeatDirection="Horizontal">
                                        
                                       
                                        <asp:ListItem Selected="True" Value="N">N:Not Closed</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="Y">Y: Auto-Closed</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="y">y:Manual Closed</asp:ListItem>
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style83">
                                    
                                   
                                    <asp:Label ID="Label35" runat="server" Text="Date Issue From :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style88">
                                    
                                   
                                    <asp:TextBox ID="txtdateissue" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="txtdateissue_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdateissue">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                               
                                <td class="style106">
                                    
                                   
                                    <asp:Label ID="Label36" runat="server" Text="Date Issue To :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style58">
                                    
                                   
                                    <asp:TextBox ID="txtdateissueTo" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="txtdateissueTo_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdateissueTo">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style83">
                                    
                                   
                                    <asp:Label ID="Label37" runat="server" Text="Date Required From :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style42">
                                    
                                   
                                    <asp:TextBox ID="txtrequest" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="txtrequest_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtrequest">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                               
                                <td class="style21">
                                    
                                   
                                    <asp:Label ID="Label38" runat="server" Text="Date Required To :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style22">
                                    
                                   
                                    <asp:TextBox ID="txtrequestTo" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="txtrequestTo_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtrequestTo">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                       
                        </table>
                        
                       
                        <br />
                        
                       
                        <br />
                        
                   
                    </ContentTemplate>
                    
               
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    
                   
                    <HeaderTemplate>
                        
                       
                        Asset PR
                   
                    </HeaderTemplate>
                    
                   
                    <ContentTemplate>
                        
                       
                        <table bgcolor="White" style="width: 101%; height: 27px;">
                            
                           
                            <tr>
                                
                               
                                <td class="style93" style="border: thin groove #808000">
                                    
                                   
                                    Asset PR Type:</td>
                                
                               
                                <td class="style40" colspan="3" style="border: thin groove #808000">
                                    
                                   
                                    <asp:CheckBoxList ID="cblAsPrType" runat="server">
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style93">
                                    
                                   
                                    <asp:Label ID="Label56" runat="server" Text="Approve :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style40" colspan="2">
                                    
                                   
                                    <asp:CheckBoxList ID="CblAppAsset" runat="server" RepeatDirection="Horizontal" 
                                        Width="300px">
                                        
                                       
                                        <asp:ListItem Value="Y">Y : Approved</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="N">N : Not Approved</asp:ListItem>
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                               
                                <td class="style27">
                                    
                                   
                                     </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style93">
                                    
                                   
                                    Close Status</td>
                                
                               
                                <td class="style40">
                                    
                                   
                                    <asp:CheckBox ID="cbNo" runat="server" Checked="True" Text="N : Not Closed" />
                                    
                               
                                </td>
                                
                               
                                <td class="style95">
                                    
                                   
                                    &nbsp;</td>
                                
                               
                                <td class="style27">
                                    
                                   
                                    &nbsp;</td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                <td class="style93">
                                    <asp:Label ID="Label58" runat="server" Text="PR No :"></asp:Label>
                                </td>
                                <td class="style40">
                                    <asp:TextBox ID="txtnoAsset" runat="server"></asp:TextBox>
                                </td>
                                <td class="style95">
                                    <asp:Label ID="Label62" runat="server" Text="Spec :"></asp:Label>
                                </td>
                                <td class="style27">
                                    <asp:TextBox ID="txtspecAsset" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style94">
                                    
                                   
                                    <asp:Label ID="Label65" runat="server" Text="Date Issue From :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style59">
                                    
                                   
                                    <asp:TextBox ID="txtdateissueAsset" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="txtdateissueAsset_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdateissueAsset">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                               
                                <td class="style57">
                                    
                                   
                                    <asp:Label ID="Label66" runat="server" Text="Date Issue To :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style58">
                                    
                                   
                                    <asp:TextBox ID="txtdateissueToAsset" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="txtdateissueToAsset_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdateissueToAsset">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style94">
                                    
                                   
                                    <asp:Label ID="Label67" runat="server" Text="Date Required From :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style42">
                                    
                                   
                                    <asp:TextBox ID="txtrequestAsset" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="txtrequestAsset_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtrequestAsset">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                               
                                <td class="style21">
                                    
                                   
                                    <asp:Label ID="Label68" runat="server" Text="Date Required To :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style22">
                                    
                                   
                                    <asp:TextBox ID="txtrequestToAsset" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="txtrequestToAsset_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtrequestToAsset">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                       
                        </table>
                        
                   
                    </ContentTemplate>

                    

               

                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel1">
                    
                   
                    <HeaderTemplate>
                        
                       
                        Open PO
                   
                    </HeaderTemplate>
                    
                   
                    <ContentTemplate>
                        
                       
                        <table bgcolor="White" style="width: 99%; height: 98px;">
                            
                           
                            <tr>
                                
                               
                                <td class="auto-style2" style="border: thin groove #808000; vertical-align: top">
                                    
                                   
                                    <asp:Label ID="Label39" runat="server" Text="SO Type :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td colspan="3" style="border: thin groove #808000" class="auto-style3">
                                    
                                   
                                    <asp:CheckBoxList ID="cblSoTypePO" runat="server">
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style79" style="border: thin groove #808000">
                                    
                                   
                                    PO Type :</td>
                                
                               
                                <td class="style40" colspan="3" style="border: thin groove #808000">
                                    
                                   
                                    <asp:CheckBoxList ID="cblPOType" runat="server">
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style101">
                                    
                                   
                                    PO NO.</td>
                                
                               
                                <td class="style103">
                                    
                                   
                                    <asp:TextBox ID="tbPoNo" runat="server"></asp:TextBox>
                                    
                               
                                </td>
                                
                               
                                <td class="style102">
                                    
                                   
                                    <asp:Label ID="Label45" runat="server" Text="Supplier"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style58">
                                    
                                   
                                    <asp:TextBox ID="tbSup" runat="server" Width="50px"></asp:TextBox>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style101">
                                    
                                   
                                    <asp:Label ID="Label46" runat="server" Text="Item :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style103">
                                    
                                   
                                    <asp:TextBox ID="txtitemPO" runat="server"></asp:TextBox>
                                    
                               
                                </td>
                                
                               
                                <td class="style102">
                                    
                                   
                                    <asp:Label ID="Label47" runat="server" Text="Spec :"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style58">
                                    
                                   
                                    <asp:TextBox ID="txtspecPO" runat="server"></asp:TextBox>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style100">
                                    
                                   
                                    PO Close Status :</td>
                                
                               
                                <td class="style59" colspan="3">
                                    
                                   
                                    <asp:CheckBoxList ID="cblCloseStatusPO" runat="server" 
                                        RepeatDirection="Horizontal">
                                        
                                       
                                        <asp:ListItem Selected="True" Value="N">N:Not Closed</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="Y">Y: Auto-Closed</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="y">y:Manual Closed</asp:ListItem>
                                        
                                   
                                    </asp:CheckBoxList>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                           
                            <tr>
                                
                               
                                <td class="style100">
                                    
                                   
                                    PO
                                   
                                    <asp:DropDownList ID="ddlDate" runat="server">
                                        
                                       
                                        <asp:ListItem Value="TD014">Confirm Del Date</asp:ListItem>
                                        
                                       
                                        <asp:ListItem Value="TC003">PO Date</asp:ListItem>
                                        
                                   
                                    </asp:DropDownList>
                                    
                                   
                                    <asp:Label ID="Label48" runat="server" Text="From"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style103">
                                    
                                   
                                    <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                               
                                <td class="style102">
                                    
                                   
                                    <asp:Label ID="Label49" runat="server" Text="Date To"></asp:Label>
                                    
                               
                                </td>
                                
                               
                                <td class="style58">
                                    
                                   
                                    <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                                    
                                   
                                    <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                                        
                                   
                                    </asp:CalendarExtender>
                                    
                               
                                </td>
                                
                           
                            </tr>
                            
                       
                        </table>
                        
                   
                    </ContentTemplate>
                    
               
                </asp:TabPanel>

            </asp:TabContainer>

            <table style="width: 75%;">
                <tr>
                    <td align="center" class="style6" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                         &nbsp;<asp:Button ID="btExport" runat="server" AutoPostBack="true" 
                            Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
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
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
      
    </asp:UpdatePanel>
</asp:Content>
