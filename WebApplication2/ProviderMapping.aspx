<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Site.Master" Inherits="WebIsomedik.ProviderMapping" CodeBehind="ProviderMapping.aspx.vb"
    Title="View | Provider Mapping" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
<h3 class="box-title"> 
    <asp:Label ID="_lbl_header" Text="Data Mapping Provider" runat="server"></asp:Label>
</h3>
    <asp:Button id="btn_add_mapping" Text="Tambah Mapping" class="btn  btn-success pull-right" runat="server"/>
        <asp:GridView ID="provider_mapping_gv"  
            runat="server" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover"
            DataKeyNames="ID" 
            EmptyDataRowStyle-CssClass="empty_data" 
            PageSize="10" 
            AllowSorting="true"
            AllowPaging="true" 
            AllowCustomPaging="true"
            GridLines="Both"
            EmptyDataText="No data Found">
            <Columns>
<asp:TemplateField HeaderText="NO." HeaderStyle-Width="2%" ItemStyle-Font-Size="Small" ><ItemTemplate><%# Container.DataItemIndex + 1%></ItemTemplate></asp:TemplateField>
                <asp:BoundField HeaderText="IDPROVIDER INTERNAL" DataField="PROVIDERID_ISOMEDIK" HeaderStyle-Width="50px" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="NAMA PROVIDER INTERNAL" DataField="PROVIDER_ISOMEDIK_NAME"  ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="IDPROVIDER EKSTERNAL" DataField="PROVIDERID_PARTNER" HeaderStyle-Width="50px" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="NAMA PROVIDER EKSTERNAL" DataField="PROVIDER_PARTNER_NAME" ItemStyle-Font-Size="Small" />
                <%--<asp:BoundField HeaderText="DIBUAT OLEH" DataField="CREATEBY" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="TGL. BUAT" DataField="CREATEDT" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="DIUBAH OLEH" DataField="EDITBY" ItemStyle-Font-Size="Small" />                                                    
                <asp:BoundField HeaderText="TGL. UBAH" DataField="EDITDT"  ItemStyle-Font-Size="Small" />--%>
            </Columns>
        </asp:GridView>
</asp:Content>
