<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MachineTableWC.aspx.vb" Inherits="MIS_HTI.MachineTableWC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
        .auto-style3 {
            height: 21px;
        }
        .auto-style4 {
            width: 155px;
            height: 21px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" >
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('http://localhost:54341/Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Machine Table"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBIlde" runat="server" BackColor="White" BorderColor="Black" BorderWidth="1px" Text="IDLE" Width="60px" text-align="center"></asp:Label>
                        &nbsp;<asp:Label ID="LBIlde0" runat="server" BackColor="Pink" BorderColor="Black" BorderWidth="1px" Text="Wait Mold" Width="70px"></asp:Label>
                        &nbsp;<asp:Label ID="LBIlde1" runat="server" BackColor="Yellow" BorderColor="Black" BorderWidth="1px" Text="Setup Mold" Width="70px"></asp:Label>
                        &nbsp;<asp:Label ID="LBIlde5" runat="server" BackColor="Firebrick" BorderColor="Black" BorderWidth="1px" Text="Wait Running" Width="87px"></asp:Label>
                        <asp:Label ID="LBIlde2" runat="server" BackColor="GreenYellow" BorderColor="Black" BorderWidth="1px" Text="Running" Width="70px"></asp:Label>
                        &nbsp;<asp:Label ID="LBIlde3" runat="server" BackColor="DarkGreen" BorderColor="Black" BorderWidth="1px" Text="&lt;60" Width="70px"></asp:Label>
&nbsp;<asp:Label ID="LBIlde4" runat="server" BackColor="Red" BorderColor="Black" BorderWidth="1px" Text="Break" Width="70px"></asp:Label>
                        <asp:Timer ID="Timer1" runat="server" Interval="60000">
                        </asp:Timer>
                    </td>
                </tr>
            </table>
            
            <table style="width:75%;" border="1">
                <tr>
                    <td></td>
                    <td>01</td>
                    <td>02</td>
                    <td>03</td>
                    <td>04</td>
                    <td>05</td>
                    <td>06</td>
                    <td>07</td>
                    <td>08</td>
                    <td>09</td>
                    <td>10</td>
                    <td>11</td>
                    <td>12</td>
                    <td>13</td>
                    <td>14</td>
                    <td>15</td>
                    <td>16</td>
                </tr>
                <tr>
                    <td>A</td>
                    <td>
                        <asp:Button ID="MachineA01" runat="server" Height="30px" Width="60px" Font-Size="Smaller" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA02" runat="server" Font-Size="Smaller" Height="30px" Text="160T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA03" runat="server" Font-Size="Smaller" Height="30px" Text="160T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA04" runat="server" Font-Size="Smaller" Height="30px" Text="160T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA05" runat="server" Font-Size="Smaller" Height="30px" Text="110T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA06" runat="server" Font-Size="Smaller" Height="30px" Text="110T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA07" runat="server" Font-Size="Smaller" Height="30px" Text="110T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA08" runat="server" Font-Size="Smaller" Height="30px" Text="80T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA09" runat="server" Font-Size="Smaller" Height="30px" Text="80T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA10" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA11" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA12" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA13" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA14" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA15" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineA16" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T" Width="60px" />
                    </td>
                </tr>
                <tr><td height="20"></td></tr>
                <tr>
                    <td>B</td>
                    <td>
                        <asp:Button ID="MachineB01" runat="server" Font-Size="Smaller" Height="30px" Text="200T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB02" runat="server" Font-Size="Smaller" Height="30px" Text="160T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB03" runat="server" Font-Size="Smaller" Height="30px" Text="160T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB04" runat="server" Font-Size="Smaller" Height="30px" Text="200T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB05" runat="server" Font-Size="Smaller" Height="30px" Text="200T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB06" runat="server" Font-Size="Smaller" Height="30px" Text="160T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB07" runat="server" Font-Size="Smaller" Height="30px" Text="200T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB08" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB09" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB10" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB11" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB12" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB13" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB14" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB15" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineB16" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Width="60px" />
                    </td>
                </tr>
                <tr><td height="20"></td></tr>
                <tr>
                    <td>C</td>
                    <td>
                        <asp:Button ID="MachineC01" runat="server" Font-Size="Smaller" Height="30px" Text="45T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC02" runat="server" Font-Size="Smaller" Height="30px" Text="45T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC03" runat="server" Font-Size="Smaller" Height="30px" Text="45T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC04" runat="server" Font-Size="Smaller" Height="30px" Text="45T" Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC05" runat="server" Font-Size="Smaller" Height="30px" Text="3T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC06" runat="server" Font-Size="Smaller" Height="30px" Text="3T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC07" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC08" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC09" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC10" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC11" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC12" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T " Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC13" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T " Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC14" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T " Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC15" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T " Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC16" runat="server" Font-Size="Smaller" Height="30px" onclick="MachineSelect" Text="80T " Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td>C</td>
                    <td>
                        <asp:Button ID="MachineC17" runat="server" Font-Size="Smaller" Height="30px" Text="160T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC18" runat="server" Font-Size="Smaller" Height="30px" Text="160T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC19" runat="server" Font-Size="Smaller" Height="30px" Text="160T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC20" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC21" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC22" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC23" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC24" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC25" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC26" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC27" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC28" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineC29" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                </tr>
                <tr><td height="20"></td></tr>
                <tr>
                    <td>D</td>
                    <td>
                        <asp:Button ID="MachineD01" runat="server" Font-Size="Smaller" Height="30px" Text="160T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD02" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD03" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD04" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD05" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD06" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD07" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD08" runat="server" Font-Size="Smaller" Height="30px" Text="110T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD09" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD10" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD11" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD12" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD13" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD14" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD15" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD16" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                </tr>
                <tr>
                    <td>D</td>
                    <td>
                        <asp:Button ID="MachineD17" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD18" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD19" runat="server" Font-Size="Smaller" Height="30px" Text="80T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD20" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD21" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD22" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD23" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                    <td>
                        <asp:Button ID="MachineD24" runat="server" Font-Size="Smaller" Height="30px" Text="60T " Width="60px" onclick="MachineSelect" />
                    </td>
                   
                </tr>
               
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Button runat="server" ID="BtnExport"  Text="Export Machine Table"/>
                        <asp:Label ID="LBNowSelectMachine" runat="server" Visible="false" ></asp:Label>
                        <asp:Label ID="LBNowSelectMachineStatus" runat="server" Visible="false" ></asp:Label>
                        <asp:Label ID="LBNowSelectMONo" runat="server" Visible="false" ></asp:Label>
                        <asp:TextBox ID="txtContactsSearch" runat="server" Visible="False"></asp:TextBox>
        <asp:AutoCompleteExtender ServiceMethod="HelloWorld" MinimumPrefixLength="2" ServicePath="~/WebService/WebService2.asmx"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtContactsSearch"
            ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" >
        </asp:AutoCompleteExtender>
                        <br />
                        &nbsp;
                        <br />


                        
                        <asp:Button ID="BtnTemp" runat="server" Text="Break/Idle" Height="1px" Width="1px" />
                        <asp:Button ID="BtnTemp1" runat="server" Text="Break/Idle" Height="1px" Width="1px" />
                        

                        
                    </td>
                    <td bgcolor="White">
                        </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
            </table>
             <asp:ModalPopupExtender ID="modEx2" runat="server" TargetControlID="BtnTemp"  PopupControlID="Panel1"
                     OkControlID="" CancelControlID="btnCancel" DropShadow="true" BackgroundCssClass="modalBackground" >
             </asp:ModalPopupExtender>
<asp:ModalPopupExtender ID="modEx1" runat="server" TargetControlID="BtnTemp1"  PopupControlID="Panel2"
                     OkControlID="" CancelControlID="" DropShadow="true" BackgroundCssClass="modalBackground" >

</asp:ModalPopupExtender>

    <asp:Panel ID="Panel1" runat="server" BorderWidth="1" BorderColor="Black" BackColor="White" >
        <table>
            <tr>
                <td colspan="4">Machine ID:<asp:Label ID="LBMachineID" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td colspan="4" bgcolor="Black"><asp:Label ID="LBPresent" runat="server" Text="Mo Detail" BackColor="Black" Font-Size="Large" ForeColor="White"></asp:Label>
                    <asp:Label ID="LBWorkCenter" runat="server" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Status:</td><td>
                <asp:DropDownList ID="DDLMachineAllStatus" runat="server">
                </asp:DropDownList>
                <asp:Label ID="LBRID" runat="server" Visible="False"></asp:Label>
                </td><td>&nbsp;</td><td class="style8">&nbsp;</td>
            </tr>
            <tr>
                <td>WO-Step:</td><td>
                <asp:Label ID="LBMONO" runat="server"></asp:Label>
                <asp:DropDownList ID="DDLCommonMO" runat="server">
                </asp:DropDownList>
                </td><td>Part No:</td><td class="style8">
                <asp:Label ID="LBPartNumber" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td>Desc:</td><td>
                 <asp:Label ID="LBPartDesc" runat="server"></asp:Label>
                 </td><td>Spec:</td><td class="style8">
                 <asp:Label ID="LBPartSpec" runat="server"></asp:Label>
                 </td>
            </tr>
             <tr>
                <td>MO Qty:</td><td>
                 <asp:Label ID="LBMOQty" runat="server"></asp:Label>
                 </td><td>Finish Qty /Process Qty:</td><td class="style8">
                 <asp:Label ID="LBProcFinishQty" runat="server"></asp:Label>
                 /<asp:Label ID="LBProcessQty" runat="server"></asp:Label>
                 </td>
            </tr>
             <tr>
                <td>Start Time:</td><td>
                 <asp:Label ID="LBStartTime" runat="server"></asp:Label>
                 </td><td>Std Pcs::</td><td class="style8">
                 <asp:Label ID="LBStdPcs" runat="server"></asp:Label>
                 </td>
            </tr>
            <tr>
                <td class="auto-style3">Plan Finish time:</td><td class="auto-style3">
                             <asp:Label ID="LBFinishTime" runat="server"></asp:Label>
                             </td><td class="auto-style3">Total Time:</td><td class="auto-style4">
                <asp:Label ID="LBTotalTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
             <tr>
                <td>User:</td><td colspan="3">
                 <asp:ComboBox ID="CBBEmployeee" runat="server" AutoCompleteMode="Append">
                 </asp:ComboBox>
                 <asp:ComboBox ID="CBBEmployeee1" runat="server" AutoCompleteMode="Append">
                 </asp:ComboBox>
                 <asp:ComboBox ID="CBBEmployeee2" runat="server" AutoCompleteMode="Append">
                 </asp:ComboBox>
                 </td>
            </tr>
                         <tr>
                <td>Qty:</td><td><asp:TextBox runat="server" ID="TBFinishQty" Width="79px"></asp:TextBox></td><td>Note:</td><td class="style8">
                             <asp:TextBox ID="TBNote" runat="server"></asp:TextBox>
                             </td>
            </tr>
             <tr>
                <td colspan="4" bgcolor="Black"><asp:Label ID="Label1" runat="server" Text="Setup Mold" BackColor="Black" Font-Size="Large" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Set Mold No:</td><td></td><td>User:</td><td class="style8">
                <asp:ComboBox ID="CBBSetupEmployee" runat="server" AutoCompleteMode="Append">
                </asp:ComboBox>
                </td>
            </tr>
            <tr>
                <td>StartTime:</td><td>
                <asp:Label ID="LBStartTime0" runat="server"></asp:Label>
                </td><td>Finish Time:</td><td class="style8">
                <asp:Label ID="LBFinishTime0" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="Black"><asp:Label ID="Label2" runat="server" Text="History" BackColor="Black" Font-Size="Large" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">MO-Step:</td><td class="auto-style3">
                <asp:Label ID="LBHisMONO" runat="server"></asp:Label>
                </td><td class="auto-style3">Part No:</td><td class="auto-style4">
                <asp:Label ID="LBHisPartNumber" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Desc:</td><td class="auto-style3">
                <asp:Label ID="LBHisDesc" runat="server"></asp:Label>
                </td><td class="auto-style3">Spec:</td><td class="auto-style4">
                <asp:Label ID="LBHisSpec" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">MO Qty / Plan Qty:</td><td class="auto-style3">
                <asp:Label ID="LBHisMOQty" runat="server"></asp:Label>
                /<asp:Label ID="LBHisPlanQty" runat="server"></asp:Label>
                </td><td class="auto-style3">Finish Qty / Std Pcs:</td><td class="auto-style4">
                <asp:Label ID="LBHisFinishQty" runat="server"></asp:Label>
                /<asp:Label ID="LBHisStdPcs" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Start Time / Finish Time:</td><td class="auto-style3">
                <asp:Label ID="LBHisStartTime" runat="server"></asp:Label>
                /<asp:Label ID="LBHisFinishTime" runat="server"></asp:Label>
                </td><td class="auto-style3">Total Time:</td><td class="auto-style4">
                <asp:Label ID="LBHisTotalTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button runat="server" ID="btnSave" Text="Save" OnClientClick  ="btnSave_Click"  /></td><td colspan="2"><asp:Button runat="server" ID="btnCancel" Text="Cancel" /></td>
            </tr>
        </table>
    </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" BorderWidth="1" BorderColor="Black" BackColor="White">
                 <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                            <Columns>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
                                <asp:TemplateField></asp:TemplateField>
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
                </asp:Panel>
        </ContentTemplate>
        <Triggers >
           
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>
