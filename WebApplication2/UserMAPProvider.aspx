<%@ Page Title="Pengaturan | Pemetaan Provider" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserMAPProvider.aspx.vb" Inherits="WebIsomedik.UserMAPProvider" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Kode Cabang <i class="fa fa-building fa-fw"></i></span>
                            <asp:DropDownList ID="ddlBranchCode" runat="server" class="form-control" name="Kode Cabang.." placeholder="Kode Cabang.." type="Kode Cabang..">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="txtKeyword" runat="server" onkeypress="return isKey(event)" class="form-control" placeholder="Id Pengguna atau Nama" name="Id Pengguna atau Nama" type="Id Pengguna atau Nama"  data-original-title="Masukan Keyword"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary  btn-block btn-flat tip-bottom" runat="server"  data-original-title="Search Data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
                <%--<div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa  fa-plus-square  fa-fw'></i></span>--%>
                            <asp:Button ID="btnAdd" CssClass="btn btn-primary  btn-block btn-flat tip-bottom" runat="server" Text="Tambah Baru"  data-original-title="Click untuk Tambah Baru" Visible="False" />
                        <%--</div>
                    </div>
                </div>--%>
                <br />
                <div class="row">
                </div>
                <div id="DivBody">
                <div class="col-sm-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Daftar <small>Pengguna</small></h3>


                        </div>

                            <div class="panel-body">
                                <div class="dataTable_wrapper">
                                    <asp:GridView ID="DGuser" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        DataKeyNames="UserId" EmptyDataRowStyle-CssClass="empty_data"
                                        EmptyDataText="Tidak ada data">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id Pengguna" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserId")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "UserId")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Username" HeaderText="Nama Pengguna" />
                                            <asp:BoundField DataField="rolecode" HeaderText="Role code" />
                                            <asp:BoundField DataField="roledesc" HeaderText="Role desc" />
                                            <asp:BoundField DataField="LvlAdmin" HeaderText="Level Admin" />
                                            <%--<asp:TemplateField>
                                                <HeaderTemplate>
                                                    IsActive
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CBIsActive" runat="server" Checked='<%# Eval("IsActive")%>' Enabled="false" CssClass="flat-red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                    </div>
                </div>
            </div>
    </div>
        </asp:Panel>
        

    <asp:Panel ID="pnlPopup" runat="server" Visible="false" DefaultButton="LinkSubmit">
        <%--<asp:Panel ID="pnlPopup2" runat="server" Visible="false"  DefaultButton="LinkSubmit">--%>


        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td style="vertical-align: central; text-align: center;">
                    <asp:LinkButton ID="LinkClose" runat="server" Text=""  data-original-title="Tutup Halaman" CssClass=" tip-bottom"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Tutup</p></asp:LinkButton>
                </td>
                <td style="vertical-align: central; text-align: center;">
                    <asp:LinkButton ID="LinkSubmit" runat="server" Text=""  data-original-title="Simpan Data" CssClass=" tip-bottom"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Simpan</p></asp:LinkButton>
                </td>
            </tr>
        </table>
        <div class="row">
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-heading">
                        Nama Pengguna
                        <i class="fa fa-user fa-fw"></i>
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtName" runat="server" onkeypress="return isKey(event)"  data-original-title="Masukan Nama Pengguna" MaxLength="100" class="form-control tip-bottom" placeholder="Nama Pengguna.." name="Nama Pengguna.." type="Nama Pengguna.." ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-heading">
                        ID Pengguna
                        <i class="fa fa-code fa-fw"></i>
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtUid" runat="server" onkeypress="return isKey(event)" MaxLength="10"  data-original-title="Autogenerate User id" class="form-control tip-bottom" placeholder="ID Penguna.." name="ID Penguna.." type="ID Penguna.." ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <asp:Panel runat="server" ID="PnlPIC" Visible="false">
        <div class="col-sm-6">
            <div class="panel">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Nomor Policy <i class="fa fa-files-o fa-fw"></i></span>
                        <asp:TextBox ID="TxtPolicyNo" runat="server"  data-original-title="Masukan Nomor Policy.." MaxLength="20" CssClass="form-control tip-bottom" placeholder="Nomor Policy.." name="Nomor Policy.." type="Nomor Policy.." onkeypress="return isKey(event)"></asp:TextBox>
                    </div>
                </div>
            </div>
            </div>
                </asp:Panel>
            
            <asp:Panel runat="server" ID="PnlProvider" Visible="false">
                
        <div class="col-sm-6">
            <div class="panel">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Id Provider  <i class="fa fa-hospital-o fa-fw"></i></span>
                        <asp:TextBox ID="txtProvider" runat="server"  data-original-title="Masukan Id Provider.." MaxLength="20" CssClass="form-control tip-bottom" placeholder="Id Provider.." name="Id Provider.." type="Id Provider.." onkeypress="return isKey(event)"></asp:TextBox>
                    </div>
                </div>
            </div>
            </div>
            </asp:Panel>

            
            <div class="col-sm-6">
                <div class="panel-body">
                    <div style="overflow-y: scroll; height: 100px; padding-left: 5px;">
                        <asp:DataGrid ID="DGDetailLog" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table table-striped table-bordered table-hover" ItemStyle-HorizontalAlign="Left" Width="98%">
                            <ItemStyle HorizontalAlign="Left" />
                            <PagerStyle CssClass="pagination" HorizontalAlign="Left" Mode="NumericPages" NextPageText="Next" PageButtonCount="5" Position="Bottom" PrevPageText="Prev" />
                            <Columns>
                                
                                <asp:TemplateColumn HeaderText="Hapus">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserId")%>' CommandName="SelectLink"><i class='fa  fa-times fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "UserId")%></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="POLICYNO" HeaderText="Nomor Policy"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MEMBID" HeaderText="ID Peserta"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROVIDERID" HeaderText="ID Provider "></asp:BoundColumn>
                                

                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
        

        <div class="row">
            <div class="col-sm-4">
                <div class="panel-body">
                    <div style="padding-left: 5px;">
                        <asp:DataGrid ID="gridMenu1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table table-striped table-bordered table-hover" ItemStyle-HorizontalAlign="Left" Width="98%">
                            <PagerStyle CssClass="pagination" HorizontalAlign="Left" Mode="NumericPages" NextPageText="Next" PageButtonCount="5" Position="Bottom" PrevPageText="Prev" />
                            <Columns>
                                <asp:BoundColumn DataField="TypeProduk" HeaderText="Type Produk">
                                    <HeaderStyle />
                                </asp:BoundColumn>
                                <asp:TemplateColumn ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <input type="checkbox" id="chk1" checked='<%# Eval("Chk")%>' runat="server" class="flat-red" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        Select
                                    </HeaderTemplate>
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Deskripsi">
                                    <HeaderStyle />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>

            <div class="col-sm-4">
                <div class="panel-body">
                    <div style="padding-left: 5px;">
                        <asp:DataGrid ID="gridMenu2" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table table-striped table-bordered table-hover" ItemStyle-HorizontalAlign="Left" Width="98%" Visible="False">
                            <PagerStyle CssClass="pagination" HorizontalAlign="Left" Mode="NumericPages" NextPageText="Next" PageButtonCount="5" Position="Bottom" PrevPageText="Prev" />
                            <Columns>
                                <asp:BoundColumn DataField="BranchCode" HeaderText="Kode Cabang ">
                                    <HeaderStyle />
                                </asp:BoundColumn>
                                <asp:TemplateColumn ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <input type="checkbox" id="chk" checked='<%# Eval("Chk")%>' runat="server" class="flat-red" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        Select
                                    </HeaderTemplate>
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="BranchName" HeaderText="Nama Cabang ">
                                    <HeaderStyle />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>

            <div class="col-sm-4">
                <div class="panel-body">
                    <div style="padding-left: 5px;">
                        <asp:DataGrid ID="gridMenu3" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table table-striped table-bordered table-hover" ItemStyle-HorizontalAlign="Left" Width="98%" Visible="False">
                            <PagerStyle CssClass="pagination" HorizontalAlign="Left" Mode="NumericPages" NextPageText="Next" PageButtonCount="5" Position="Bottom" PrevPageText="Prev" />

                            <Columns>
                                <asp:BoundColumn DataField="GroupCode" HeaderText="Kode Grup ">
                                    <HeaderStyle />
                                </asp:BoundColumn>
                                <asp:TemplateColumn ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <input type="checkbox" id="chk" checked='<%# Eval("Chk")%>' runat="server" class="flat-red" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        Select 
                                    </HeaderTemplate>
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GroupDesc" HeaderText="Grup Deskripsi">
                                    <HeaderStyle />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>
    <asp:ModalPopupExtender ID="LinkMpeModalPopupExtender2" PopupControlID="pnlPopup2" runat="server"
        TargetControlID="LinkMpe2" PopupDragHandleControlID="mGridPict" BackgroundCssClass="overlay">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="LinkMpe2" runat="server"></asp:LinkButton>
    <asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display: none;" >
        <div id="row">
            <center>
                <table class="table table-striped table-bordered table-hover" id="mGridPict">
                    <tr>
                        <td >
                            <asp:LinkButton ID="LinkClose2" runat="server" Font-Strikeout="FALSE"  Font-Bold="false" Text=""><p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i>Batal</p></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </center>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel-heading">
                        Marketing
                    </div>

                    <div class="col-sm-6">
                        <div class="panel-body">
                            <asp:TextBox ID="KeyMarketing" runat="server" Style="text-transform: uppercase"  data-original-title="Input Marketing" onkeypress="return isKey(event)" class="form-control tip-bottom" placeholder="Key.." name="Key.." type="Key.."></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="panel-body">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <div class="row" style="visibility: hidden;">

        <div class="col-sm-6">
            <div class="panel">
                <div class="panel-body">
                    <asp:LinkButton ID="LinkMarketing" runat="server">Marketing</asp:LinkButton>
                    <asp:ImageButton ID="btnMarketing" runat="server" ImageUrl="~/Images/zoom.png"  data-original-title="Search Marketing" CssClass=" tip-bottom" />
                    &nbsp;<asp:Label ID="LblMarketing" runat="server" BackColor="#DEF1F4"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="visibility: hidden;">
        <div class="col-sm-12">
            <div class="panel-body">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary  btn-block btn-flat" Text="Submit" Visible="False" />
            </div>
        </div>
    </div>

</asp:Content>
