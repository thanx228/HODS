<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestPage.aspx.vb" Inherits="MIS_HTI.TestPage" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $("[id*=btnPopup]").live("click", function () {
            $("#dialog").dialog({
                title: "jQuery Dialog Popup",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
            return false;
        });
    </script>
    <div id="dialog" style="display: none">
        This is a simple popup<br />

    </div>
    <asp:Button ID="btnPopup" runat="server" Text="Show Popup" />
    <script type="text/javascript">
        $("[id*=btnModalPopup]").live("click", function () {
            $("#modal_dialog").dialog({
                title: "jQuery Modal Dialog Popup",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
            return false;
        });
    </script>
    <div id="modal_dialog" style="display: none">
        This is a Modal Background popup
        &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="Dept" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Dept" HeaderText="Dept" ReadOnly="True" 
                    SortExpression="Dept" />
                <asp:BoundField DataField="DepName" HeaderText="DepName" 
                    SortExpression="DepName" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
            SelectCommand="SELECT * FROM [Department]"></asp:SqlDataSource>
    </div>
    <br />
    <asp:Button ID="btnModalPopup" runat="server" Text="Show Modal Popup" />
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="Dept" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="Dept" HeaderText="Dept" ReadOnly="True" 
                SortExpression="Dept" />
            <asp:BoundField DataField="DepName" HeaderText="DepName" 
                SortExpression="DepName" />
        </Columns>
    </asp:GridView>

    <asp:ModalPopupExtender ID="GridView2_ModalPopupExtender" runat="server" 
        DynamicServicePath="" Enabled="True" TargetControlID="GridView2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
    This is an ASP.Net AJAX ModalPopupExtender Example<br />
    <asp:Button ID="btnClose" runat="server" Text="Close" />
</asp:Panel>
    <br />
    </form>
</body>
</html>
