<%@ Page Title="Pengaturan | Generet Pengguna Provider" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserListProvider.aspx.vb" Inherits="WebIsomedik.UserListProvider" %>

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
                            <asp:TextBox ID="txtKeyword" runat="server" onkeypress="return isKey(event)" class="form-control tip-bottom" placeholder="ID Pengguna atau Nama" name="ID Pengguna atau Nama" type="ID Pengguna atau Nama"  data-original-title="Masukan Keyword"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-success  btn-block btn-flat tip-bottom" runat="server"  data-original-title="Cari Data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa  fa-plus-square  fa-fw'></i></span>
                            <asp:Button ID="btnAdd" CssClass="btn btn-success  btn-block btn-flat tip-bottom" runat="server" Text="Tambah Baru"  data-original-title="Click untuk Tambah Baru" />
                        </div>
                    </div>
                </div>
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
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id Pengguna" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserId")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "UserId")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Username" HeaderText="Nama Pengguna" />
                                            <asp:BoundField DataField="roledesc" HeaderText="Deskripsi Role " />
                                            <asp:BoundField DataField="LvlAdmin" HeaderText="Level Admin" />
                                            <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="hfstatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsActive")%> ' />
                                                    <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                                                         data-original-title='<%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "Inactive", "Actived")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "USERID")%>' CommandName="UpdateLink"><i class='fa fa-signout fa-fw'></i>
                                            <%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "<i class='fa fa-check fa-fw'></i>", "<i class='fa fa-times fa-fw'></i>")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField>
                                                <HeaderTemplate>
                                                    IsActive
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CBIsActive" runat="server" Checked='<%# Eval("IsActive")%>' Enabled="false" CssClass="flat-red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:BoundField DataField="OnlineDate" HeaderText="Tangal Online" />
                                            <asp:BoundField DataField="OnlineIp" HeaderText="Online Ip" />
                                            <asp:TemplateField HeaderText="Online" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkKillUserId" runat="server"  data-original-title="Putus Koneksi Pengguna" CssClass=" tip-bottom" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserId")%>' CommandName="KillLink"><%# IIf(DataBinder.Eval(Container.DataItem, "Online") = "True", "<i class='fa fa-link fa-fw'></i> Kill", "")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                        <asp:TextBox ID="txtName" runat="server" onkeypress="return isKey(event)"  data-original-title="Isi Nama Pengguna" MaxLength="100" class="form-control tip-bottom" placeholder="Nama Pengguna.." name="Nama Pengguna.." type="Nama Pengguna.."></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-heading">
                        Id Pengguna
                        <i class="fa fa-code fa-fw"></i>
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtUid" runat="server" onkeypress="return isKey(event)" MaxLength="10"  data-original-title="Autogenerate id Pengguna" class="form-control tip-bottom" placeholder="Id Pengguna.." name="Id Pengguna.." type="Id Pengguna.." ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"> Role Pengguna&nbsp; <i class="fa fa-users fa-fw"></i></span>
                            <asp:DropDownList ID="ddlRoleAP" runat="server" AutoPostBack="True" class="form-control" placeholder="Role Pengguna.." name="Role Pengguna.." type="Role Pengguna..">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Cabang Pengguna Menetap <i class="fa fa-building fa-fw"></i></span>
                            <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" placeholder="Cabang Pengguna Menetap.." name="Cabang Pengguna Menetap.." type="Cabang Pengguna Menetap.." AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Email @ <i class='fa fa-envelope-o fa-fw'></i></span>
                            <asp:TextBox ID="txtEmail" runat="server" onkeypress="return isKey(event)"  data-original-title="Isi Email Pengguna" MaxLength="50" class="form-control tip-bottom" placeholder="Email Pengguna" name="Email Pengguna" type="Email Pengguna"></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <asp:RadioButtonList ID="RbGender" runat="server" RepeatDirection="Horizontal" CssClass="flat-red">
                            <asp:ListItem Selected="True" Value="M">Laki-Laki</asp:ListItem>
                            <asp:ListItem Value="F">Perempuan</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group" id="datepicker">
                            <span class="input-group-addon calender-btn">Tanggal Lahir <i class='fa fa-calendar fa-fw'></i>
                            </span>
                            <asp:TextBox ID="txtBirthDate" runat="server" 
                                 data-original-title="fill day birth" class="form-control tip-bottom " placeholder="Tanggal Lahir.." name="Tanggal Lahir.." type="Tanggal Lahir.."></asp:TextBox>
                        </div>

                    </div>
                    <div class="panel-footer">
                        <asp:CheckBox ID="chkAktiv" runat="server" Checked="True" Text="IsActive"  data-original-title="Click For Active /Non Active User" CssClass="flat-red tip-bottom" />
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group" id="datepicker1">
                            <span class="input-group-addon calender-btn">Tanggal Berakhir <i class="fa fa-calendar fa-fw"></i></span>
                            <asp:TextBox ID="txtExpirateDate" runat="server" 
                                 data-original-title="Isi Tanggal berakhir " class="form-control tip-bottom" placeholder="Tanggal berakhir.." name="Tanggal berakhir.." type="Tanggal berakhir.."></asp:TextBox>
                        </div>
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
                                <asp:BoundColumn DataField="TypeProduk" HeaderText="Type Produk ">
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
                                <asp:BoundColumn DataField="DESCRIPTION" HeaderText="DESCRIPTION">
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
                                <asp:BoundColumn DataField="BranchCode" HeaderText="Branch Code">
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
                                <asp:BoundColumn DataField="BranchName" HeaderText="Branch Name">
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
                                <asp:BoundColumn DataField="GroupCode" HeaderText="Group Code">
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
                                <asp:BoundColumn DataField="GroupDesc" HeaderText="Group Description">
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
    <asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display: none;" DefaultButton="btnCariMarketing">
        <div id="row">
            <center>
                <table class="table table-striped table-bordered table-hover" id="mGridPict">
                    <tr>
                        <td >
                            <asp:LinkButton ID="LinkClose2" runat="server" Font-Strikeout="FALSE"  Font-Bold="false" Text=""><p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i>Cancel</p></asp:LinkButton>
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
                            <asp:Button ID="btnCariMarketing" runat="server" CssClass="btn btn-success  btn-block btn-flat tip-bottom" Text="Cari"  data-original-title="Click for search data" />
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
                    <asp:ImageButton ID="btnMarketing" runat="server" ImageUrl="~/Images/zoom.png"  data-original-title="Search Marketing" CssClass=" tip-bottom"/>
                    &nbsp;<asp:Label ID="LblMarketing" runat="server" BackColor="#DEF1F4"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="visibility: hidden;">
        <div class="col-sm-12">
            <div class="panel-body">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success  btn-block btn-flat" Text="Submit" Visible="False" />
            </div>
        </div>
    </div>

</asp:Content>
