<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PolBenefit.aspx.vb" Inherits="WebIsomedik.PolBenefit" Title="Transaction | Policy Benefit"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div id="DivBody" style="overflow-x:scroll;">
                    <div class="row">                   
                    <div class="col-sm-8">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                                <asp:TextBox ID="_tb_search" runat="server" onkeypress="return isKey(event)"  class="form-control" placeholder="Isi Nomor Polis..." name="Isi Nomor Polis..." type="Isi Nomor Polis..." data-toggle="tooltip" data-placement="top" title="Isi Nomor Polis" ></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Cari Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                            </div> 
                        </div>
                    </div>    
                    <div class="col-sm-4">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                                <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By" AutoPostBack="True" >
                                    <asp:ListItem Value="Select">PILIH TIPE MAPPING..</asp:ListItem>
                                <asp:ListItem Value="COB">COB</asp:ListItem>
                                <asp:ListItem Value="MUTASI">MUTASI</asp:ListItem>
                                <asp:ListItem Value="RAWATINAP">RAWAT INAP</asp:ListItem>
                            </asp:DropDownList>
                            </div> 
                        </div>
                    </div>              
                    </div>
                <div class="row">
                     <asp:GridView ID="gridMenu" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                         EmptyDataRowStyle-CssClass="empty_data" 
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SELECT" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHCK" runat="server" Checked='<%# IIf(DataBinder.Eval(Container.DataItem, "CHCK") = "True", "True", "False")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="POLICYNO" HeaderText=" POLICY NO" HeaderStyle-Width="15%"/>
                                            <asp:BoundField DataField="PLANID" HeaderText=" PLAN ID" HeaderStyle-Width="10%"/>
                                            <asp:BoundField DataField="SUBPRODID" HeaderText=" PRODUCT ID" HeaderStyle-Width="10%"/>
                                            <asp:BoundField DataField="SUBPRODNM2" HeaderText=" PRODUCT NAME" />
                                        </Columns>
                                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        
<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary  btn-block btn-flat" Text="Submit"  />
</asp:Content>
