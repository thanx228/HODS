<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="shiftD.ascx.vb" Inherits="MIS_HTI.shiftD" %>
<style type="text/css">

select
{
    color:#000;
    background:#FFFFCC;
    margin:0px 0px 0px 0px !important;
    padding:0px 0px 0px 0px !important;
}

select option
{
    background:#FFFFCC;
}
</style>
                                    <asp:DropDownList ID="ddlShift" 
    runat="server" >
                                        <asp:ListItem Value="D">D:Day</asp:ListItem>
                                        <asp:ListItem Value="N">N:Night</asp:ListItem>
                                    </asp:DropDownList>
                                
