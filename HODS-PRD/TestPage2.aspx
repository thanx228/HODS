<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestPage2.aspx.vb" Inherits="MIS_HTI.TestPage2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

 <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Modal Popup using Bootstrap</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
            <h2 style="text-align:center;">
                   Display GridView Row Details in Modal Dialog using Twitter Bootstrap</h2>
            <p style="text-align:center;">
                   Demo by Priya Darshini - Tutorial @ <a href="">Programmingfree</a>
            </p>                     
               <asp:GridView ID="GridView1" runat="server" 
                        Width="940px"  HorizontalAlign="Center"
                        AutoGenerateColumns="false"   AllowPaging="false"
                        DataKeyNames="Code" 
                        CssClass="table table-hover table-striped">
                <Columns>
                   <asp:ButtonField CommandName="detail" 
                         ControlStyle-CssClass="btn btn-info" ButtonType="Button" 
                         Text="Detail" HeaderText="Detailed View"/>
            <asp:BoundField DataField="Code" HeaderText="Code" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Continent" HeaderText="Continent" />
            <asp:BoundField DataField="Region" HeaderText="Surface Area" />
            <asp:BoundField DataField="Population" HeaderText="Population" />
            <asp:BoundField DataField="IndepYear" HeaderText="Year of Independence" />
            <asp:BoundField DataField="LocalName" HeaderText="Local Name" />
            <asp:BoundField DataField="Capital" HeaderText="Capital" />
            <asp:BoundField DataField="HeadOfState" HeaderText="Head of State" />
               </Columns>
               </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            

        <img src="" alt="Loading.. Please wait!"/>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="currentdetail" class="modal hide fade" 
               tabindex=-1 role="dialog" aria-labelledby="myModalLabel" 
               aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" 
                  aria-hidden="true">×</button>
            <h3 id="myModalLabel">Detailed View</h3>
       </div>
   <div class="modal-body">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                    <asp:DetailsView ID="DetailsView1" runat="server" 
                              CssClass="table table-bordered table-hover" 
                               BackColor="White" ForeColor="Black"
                               FieldHeaderStyle-Wrap="false" 
                               FieldHeaderStyle-Font-Bold="true"  
                               FieldHeaderStyle-BackColor="LavenderBlush" 
                               FieldHeaderStyle-ForeColor="Black"
                               BorderStyle="Groove" AutoGenerateRows="False">
                        <Fields>
                 <asp:BoundField DataField="Code" HeaderText="Code" />
                 <asp:BoundField DataField="Name" HeaderText="Name" />
                 <asp:BoundField DataField="Continent" HeaderText="Continent" />
                 <asp:BoundField DataField="Region" HeaderText="Surface Area" />
                 <asp:BoundField DataField="Population" HeaderText="Population" />
                 <asp:BoundField DataField="IndepYear" HeaderText="Year of Independence" />
                 <asp:BoundField DataField="LocalName" HeaderText="Local Name" />
                 <asp:BoundField DataField="Capital" HeaderText="Capital" />
                 <asp:BoundField DataField="HeadOfState" HeaderText="Head of State" />
                       </Fields>
                  </asp:DetailsView>
           </ContentTemplate>
           <Triggers>
               <asp:AsyncPostBackTrigger ControlID="GridView1"  EventName="RowCommand" />  
           </Triggers>
           </asp:UpdatePanel>
                <div class="modal-footer">
                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                </div>
            </div>
    </div>
    </div>
    </form>
</body>
</html>