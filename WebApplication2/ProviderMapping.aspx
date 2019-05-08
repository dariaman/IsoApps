<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Site.Master" Inherits="WebIsomedik.ProviderMapping" CodeBehind="ProviderMapping.aspx.vb"
    Title="View | Provider Mapping" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
<h3 class="box-title"> 
    <asp:Label ID="_lbl_header" Text="Data Mapping Provider" runat="server"></asp:Label>
</h3>
    <div class="row">
            <div class="col-sm-4">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Search Type </span>
                        <asp:DropDownList ID="ddljenisProvider" runat="server" class="form-control" placeholder="Type" name="Type" type="Type">
                            <asp:ListItem Value="1">Nama Provider Internal</asp:ListItem>
                            <asp:ListItem Value="2">Nama Provider Eksternal</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="panel-body">
                    <div class="input-group">
                        <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                        <asp:TextBox ID="txtnamaprovider" runat="server" onkeypress="return isKey(event)"
                            MaxLength="50" class="form-control tip-bottom" placeholder="Search..." name="Search..." type="Search..." data-original-title="Search role"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary  btn-block btn-flat tip-bottom" runat="server" data-original-title="Search data"><i class="fa fa-search"></i></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
        <asp:GridView ID="provider_mapping_gv"  
            runat="server" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover"
            EmptyDataRowStyle-CssClass="empty_data" 
            PageSize="10" 
            AllowSorting="true"
            AllowPaging="true" 
            GridLines="Both"
            EmptyDataText="No data Found">
            <Columns>
<asp:TemplateField HeaderText="NO." HeaderStyle-Width="2%" ItemStyle-Font-Size="Small" ><ItemTemplate><%# Container.DataItemIndex + 1%></ItemTemplate></asp:TemplateField>
                <asp:BoundField HeaderText="IDPROVIDER INTERNAL" DataField="PROVIDERID_ISOMEDIK" HeaderStyle-Width="50px" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="NAMA PROVIDER INTERNAL" DataField="PROVIDER_ISOMEDIK_NAME"  ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="IDPROVIDER EKSTERNAL" DataField="PROVIDERID_PARTNER" HeaderStyle-Width="50px" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="NAMA PROVIDER EKSTERNAL" DataField="PROVIDER_PARTNER_NAME" ItemStyle-Font-Size="Small" />
                <asp:TemplateField HeaderText="" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="20px">
                    <ItemTemplate>                                                            
                        <asp:Button ID="_btn_add" runat="server" CssClass="btn-success" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PROVIDERID_ISOMEDIK")%>' CommandName='addmapping' Text="+" /><br />
                    </ItemTemplate>
                </asp:TemplateField>    
            </Columns>
        </asp:GridView>
</asp:Content>
