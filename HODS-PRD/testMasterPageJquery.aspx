<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS2.Master" CodeBehind="testMasterPageJquery.aspx.vb" Inherits="MIS_HTI.testMasterPageJquery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.9.1.min.js" language="javascript" type="text/javascript"></script>
<script src="Scripts/textboxclone.js" language="javascript" type="text/javascript">
    $(document).ready(function () {
        $("#btnAnimate").click(function () {
            $("#Panel1").animate(
            {
                width: "350px",
                opacity: 0.5,
                fontSize: "16px"
            }, 1800);
        });
    });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        <input id="btnAnimate" type="button" value="Animate" />
        <asp:Panel ID="Panel1" runat="server">
        Some sample text in this panel       
        </asp:Panel>
    </p>
</asp:Content>
