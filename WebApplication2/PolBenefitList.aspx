<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PolBenefitList.aspx.vb" Inherits="WebIsomedik.PolBenefitList" 
    Title="List | Policy Benefit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div id="DivBody">
                    <div class="row">                   
                    <div class="col-sm-8">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                                <asp:TextBox ID="search_polis_txt" runat="server" AutoCompleteType="Disabled" onkeypress="return isKey(event)"  class="form-control" placeholder="Isi Nomor Polis..." data-toggle="tooltip" data-placement="top" title="Isi Nomor Polis" ></asp:TextBox>
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                                <asp:TextBox ID="search_membid_txt" runat="server" AutoCompleteType="Disabled" onkeypress="return isKey(event)"  class="form-control" placeholder="Isi Member ID..." data-toggle="tooltip" data-placement="top" title="Isi member ID" ></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Cari Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                            </div> 
                        </div>
                    </div>
                    </div>
                    <%--<h4 class="box-title">Benefit COB <small></small></h4>--%>
                <div class="row">
                     <asp:GridView ID="gridcob" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                            EmptyDataRowStyle-CssClass="empty_data" 
                        EmptyDataText="No data Found">
                        <Columns>
                            <asp:BoundField DataField="POLICYNO" HeaderText=" POLICY NO" />
                        <%--    <asp:BoundField DataField="MEMBID" HeaderText=" MEMBID" />
                            <asp:BoundField DataField="FULLNAME" HeaderText="Nama Member" />--%>
                            <asp:BoundField DataField="PLANID" HeaderText=" PLAN ID" />
                            <asp:BoundField DataField="SUBPRODID" HeaderText=" PRODUCT ID" />
                            <asp:BoundField DataField="SUBPRODNM2" HeaderText=" PRODUCT " />
                            <asp:BoundField DataField="MAPPING" HeaderText=" MAPPING" HeaderStyle-Width="10%"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
</asp:Content>
