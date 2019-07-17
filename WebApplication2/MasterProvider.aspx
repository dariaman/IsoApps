<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="MasterProvider.aspx.vb" Inherits="WebIsomedik.MasterProvider" 
    Title="LIST | Master Provider" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
<h3 class="box-title"> 
    <asp:Label ID="_lbl_header" Text="Daftar Provider" runat="server"></asp:Label>
</h3>
    <div class="row">
            <div class="col-sm-4">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Search Type </span>
                        <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Type" name="Type" type="Type">
                            <asp:ListItem Value="1">ProviderID</asp:ListItem>
                            <asp:ListItem Value="2">Nama Provider</asp:ListItem>
                            <asp:ListItem Value="3">Email</asp:ListItem>
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
    <asp:Button id="btn_add_Provider" Text="Tambah Provider" class="btn  btn-success pull-right" runat="server"/>
        <asp:GridView ID="provider_gv"  
            runat="server" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover"
            EmptyDataRowStyle-CssClass="empty_data" 
            PageSize="10" 
            DataKeyNames="PROVIDERID"
            AllowSorting="true"
            AllowPaging="true" 
            GridLines="Both"
            EmptyDataText="No data Found">
            <Columns>
                <asp:TemplateField HeaderText="PROVIDERID" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" 
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PROVIDERID")%>' 
                            CommandName="UpdateData"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "PROVIDERID")%></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="PROVIDERNAME" DataField="PROVIDERNAME" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="BUILDING" DataField="BUILDING" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="STREET1" DataField="STREET1" ItemStyle-Font-Size="Small" />

                <%--<asp:BoundField HeaderText="STREET2" DataField="STREET2" ItemStyle-Font-Size="Small" />--%>
                <asp:BoundField HeaderText="ZIPCODE" DataField="ZIPCODE" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="PHONE1" DataField="PHONE1" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="FAX1" DataField="FAX1" ItemStyle-Font-Size="Small" />

                <asp:BoundField HeaderText="LONGITUDE" DataField="LONGITUDE" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="LATITUDE" DataField="LATITUDE" ItemStyle-Font-Size="Small" />

                <asp:BoundField HeaderText="EMAIL" DataField="EMAIL" HeaderStyle-Width="50px" ItemStyle-Font-Size="Small" />
                <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PROVIDERID")%>' 
                            CommandName="UpdateStatus">
                            <%# IIf(IIf(DataBinder.Eval(Container.DataItem, "ISACTIVE").Equals(DBNull.Value), "False", DataBinder.Eval(Container.DataItem, "ISACTIVE")) = "True", "<i class='fa fa-check fa-fw'></i>", "<i class='fa fa-times fa-fw'></i>")%>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
              
            </Columns>
        </asp:GridView>
</asp:Content>
