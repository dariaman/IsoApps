<%@  Page Title="Pengaturan | Udah Sandi" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="changePassword.aspx.vb" Inherits="WebIsomedik.changePassword" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">

        <div class="row">
            <div class="col-sm-12">
                <div class="panel-default">
                    <div class="panel-heading">
                        Ubah Sandi
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table class="table table-bordered table-striped">
                                <tbody>
                                    <tr>
                                        <td class="table5">Nama Pengguna</td>
                                        <td class="auto-table31">
                                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table5">Sandi Lama</td>
                                        <td class="auto-table31">
                                            <asp:TextBox ID="txtOldPassword" runat="server" onkeypress="return isKey(event)" MaxLength="25" class="form-control tip-bottom" TextMode="Password" placeholder="Sandi Lama.." name="Sandi Lama.." type="Sandi Lama.."   data-original-title="Sandi Lama.." ></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table5">Sandi Baru</td>
                                        <td class="auto-table31">
                                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" onselectstart="return false" onpaste="return false;" onCopy="return false" onCut="return false" onDrag="return false" onDrop="return false" autocomplete=off onkeypress="return isKey(event)" MaxLength="25" class="form-control tip-bottom" placeholder="Sandi Baru.." name="Sandi Baru.." type="Sandi Baru.."  data-original-title="New Password" ></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table5">Ulangi Sandi Baru</td>
                                        <td class="auto-table31">
                                            <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" onselectstart="return false" onpaste="return false;" onCopy="return false" onCut="return false" onDrag="return false" onDrop="return false" autocomplete=off onkeypress="return isKey(event)" MaxLength="25" class="form-control tip-bottom" placeholder="Ulangi Sandi Baru.." name="Ulangi Sandi Baru.." type="Ulangi Sandi Baru.."   data-original-title="Ulangi Sandi Baru.." ></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table5">&nbsp;</td>
                                        <td class="auto-table31">
                                            <asp:LinkButton ID="LinkUserprofile" runat="server" Text=""> <p style="line-height: 0%;text-align: Left;"><i class="fa fa-user" ></i>Profil Pengguna</p></asp:LinkButton>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-table31" colspan="2">

                                            <div class="col-sm-2">
                                                <div class="panel-body">
                                                    <div class="form-group input-group">
                                                        <span class="input-group-addon"><i class='fa fa-check-square fa-fw'></i></span>
                                                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary btn-block btn-flat" runat="server" Text="Simpan" />
                                                    </div>
                                                </div>
                                            </div>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
