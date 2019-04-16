<%@ Page Title="Pengaturan | Membuat Pengguna" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GenerateProvider.aspx.vb" Inherits="WebIsomedik.GenerateProvider" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-4">
                    

                </div>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server" data-toggle="tooltip" data-placement="top" title="Cari Data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>

                </div>
                <div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <%--<span class="input-group-addon"><i class='fa fa-plus-square fa-fw'></i></span>--%>
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Generate" 
                                data-toggle="tooltip" data-placement="top" title="Click for add new Provider" />
                        </div>

                    </div>
                </div>
            </div>
            <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <asp:Button ID="btnDownload" runat="server" CssClass="btn btn-primary btn-block btn-flat" data-placement="top" data-toggle="tooltip" Text="Unduh" title="Click untuk Unduh" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box box-info">

                        <div class="box-body pad">
                            <div class="panel-body">
                                <div class="dataTable_wrapper">
                                    <br />
                                    <asp:GridView ID="gridMenu" runat="server"  AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        DataKeyNames="ProviderId" EmptyDataRowStyle-CssClass="empty_data"
                                        EmptyDataText="Tidak ada data">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="ID" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="ImgViewPROVIDERID" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MEMBID")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "MEMBID")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            <asp:BoundField DataField="ProviderId" HeaderText="Id Provider" />
                                            <asp:BoundField DataField="PROVIDERNAME" HeaderText="NamaProvider" />
                                            <asp:BoundField DataField="UserId2" HeaderText="Id Pengguna 2" />
                                            <asp:BoundField DataField="Password" HeaderText="Sandi" />
                                            <asp:BoundField DataField="UserIdLOGIN" HeaderText="Id Pengguna Login" />
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
        
    <asp:Panel ID="pnlPopup" runat="server" Visible="false"  DefaultButton="btnSave">
        <div id="DivBody1">
            <table class="table table-striped table-bordered table-hover" id="mGridPict">
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkClose" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Close Page"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Tutup</p></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkSubmit" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Submit Data"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Simpan</p></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Kode Provider<i class="fa fa-code fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtProviderCode" runat="server" MaxLength="20" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Isi Kode Provider" CssClass="form-control" placeholder="Kode Provider.." name="Kode Provider.." type="Kode Provider.."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                           Nama Provider <i class="fa fa-building fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtProvidername" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Isi Nama Provider" MaxLength="100" CssClass="form-control" placeholder="Nama Provider.." name="Nama Provider.." type="Nama Provider.."></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            REMARK
                        <i class="fa fa-pencil-square-o fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtRemark" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Remark" MaxLength="20" CssClass="form-control" placeholder="Input Remark.." name="Input Remark.." type="Input Remark.."></asp:TextBox>
                        </div>
                        <div class="panel-footer">
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Simpan" Visible="False" />
        </div>
    </asp:Panel>
</asp:Content>
