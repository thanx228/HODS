<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="GridviewShow.ascx.vb" Inherits="MIS_HTI.GridviewShow" %>
<%@ Register src="CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<style type="text/css">
    .gridview  {       
        height:100%;
    }
    .gridview .head {
        height:5%;
    }
    .gridview .body {
        height:95%;
        overflow:auto;
    }
</style>
<link href="../Styles/gridView.css" rel="stylesheet" />
<div class ="gridview">
    <div class="head">
        <uc1:CountRow ID="ucRowCount" runat="server" />
    </div>
    <div class="body">
        <asp:GridView ID="gvShow" runat="server" CssClass="table table-sm table-condensed table-striped table-hover"  >
        </asp:GridView>
    </div>
     
</div>