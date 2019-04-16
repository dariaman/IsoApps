 <%@  Page Title="Info | Informasi Diagnosa " Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Diagnosa.aspx.vb" Inherits="WebIsomedik.Diagnosa" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                
                <div class="col-sm-8">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" onkeypress="return isKey(event)" placeholder="Cari Kode atau Deskripsi..." name="Cari Kode atau Deskripsi..." type="Cari Kode atau Deskripsi..." data-toggle="tooltip" data-placement="top" title="Input Key" ></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Cari Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div> 
                    </div>

                </div>
                <div class="col-sm-2">
                    <div class="panel-body" >
                        <div class="form-group input-group">
                            <%--<span class="input-group-addon"><i class='fa fa-plus-square fa-fw'></i></span>--%>
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Add Group" Visible="false"
                            data-toggle="tooltip" data-placement="top" title="Click for add new Provider" />
                            </div>
                    </div>
                </div>
            </div>

            <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Daftar <small>Diagnosa</small></h3>
                                                      
                        </div>

                        <div class="box-body pad">
                                <div class="panel-body">
                                    <div class="dataTable_wrapper">
                                        <br />
                                        <asp:GridView ID="gridMenu" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                            DataKeyNames="DIAGCODE" EmptyDataRowStyle-CssClass="empty_data" 
                                            EmptyDataText="Tidak ada data">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Kode Diagnosa" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="ImgViewDIAGCODE" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DIAGCODE")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "DIAGCODE")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="DIAGCODE" HeaderText="Kode Diagnosa" />
                                                <asp:BoundField DataField="DIAGDESC" HeaderText="Deskripsi Diagnosa" />
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
    </div>
        </asp:Panel>
        
</asp:Content>
