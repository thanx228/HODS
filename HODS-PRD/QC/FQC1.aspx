<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FQC1.aspx.vb" Inherits="MIS_HTI.FQC1" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" ToolPanelView="None" />

     <asp:TextBox ID="txttype" runat="server" Visible="False"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtno" runat="server" Visible="False"></asp:TextBox>
        <br />

    </div>
    </form>
</body>
</html>
