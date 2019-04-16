<%@ Page Title="Pengaturan | Detail Pengguna" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="~/UserDetail1.aspx.vb" Inherits="WebIsomedik.UserDetail1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody1">
        <table class="table table-striped table-bordered table-hover" id="mGridPict">
            <tr>
                <td style="vertical-align: central; text-align: center;">
                    <asp:LinkButton ID="LinkClose" runat="server" Text=""  data-original-title="Tutup Halaman" CssClass=" tip-bottom"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Tutup</p></asp:LinkButton>
                </td>
                <td style="vertical-align: central; text-align: center;">
                    <asp:LinkButton ID="LinkSubmit" runat="server" Text=""  data-original-title="Simpan Data" CssClass=" tip-bottom"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Simpan</p></asp:LinkButton>
                </td>
            </tr>
        </table>
       <%-- <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <center><asp:Image ID="Image1" runat="server" Style="height: 150px; width: 150px" CssClass="img-thumbnail img-rounded" ImageUrl="~/pict/upload/unknown1.png" />
                                <br />
                                <br />
                                <div class="fileupload fileupload-new" data-provides="fileupload"  data-original-title="Select a picture to upload">
                                    <span class="btn btn-primary  btn-file">
                                        <span class="fileupload-new"><i class='fa fa-image fa-fw'></i> Select </span>
                                        <span class="fileupload-exists"><i class='fa  fa-file-image-o  fa-fw'></i> Change </span><asp:FileUpload ID="FileUpload1" runat="server" />
                                        <span class="fileupload-preview"></span>
                                        <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none;color:white;"><i class='fa fa-trash fa-fw'></i></></a>
                                    </span>
                                </div>
                        </center>
                    </div>
                    <div class="panel-footer">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:LinkButton ID="LinkUpload" runat="server" class="btn btn-primary  btn-block btn-group-vertical"  data-original-title="Upload a Picture"><i class='fa fa-upload fa-fw'></i> Up Load Picture<i class='fa fa-upload fa-fw'></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="row">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading">
                        Kode BioData <i class='fa fa-code fa-fw'></i>
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtBioDataCode" runat="server" MaxLength="5"  data-original-title="Isi kode BioData" CssClass="form-control tip-bottom" placeholder="Kode BioData.." name="Kode BioData.." type="Kode BioData.." onkeypress="return isKey(event)" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
            <%--<div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading">
                        BioData Name <i class='fa fa-user fa-fw'></i>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-3">
                            <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="form-control" placeholder="Salutation" name="Salutation" type="Salutation" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-9">
                            <asp:TextBox ID="txtBioDataname" runat="server"  data-original-title="Input Name Branch" MaxLength="100" CssClass="form-control tip-bottom" placeholder="BioData Name.." name="BioData Name.." type="BioData Name.." onkeypress="return isKey(event)" ReadOnly="true"></asp:TextBox>
                        </div>

                    </div>
                </div>
            </div>--%>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Email @ <i class='fa fa-envelope-o fa-fw'></i></span>
                            <asp:TextBox ID="txtBioDataEmail" runat="server"  data-original-title="Isi BioData Email" MaxLength="50" class="form-control tip-bottom" placeholder="BioData Email.." name="BioData Email.." type="BioData Email.." onkeypress="return isKey(event)" ></asp:TextBox>
                        </div>
                    </div>
                    <%--<div class="panel-footer"  style="visibility:hidden;">
                        <asp:RadioButtonList ID="RbGender" runat="server" RepeatDirection="Horizontal" CssClass="flat-red tip-bottom"  data-original-title="Select Gender" Enabled="false" >
                            <asp:ListItem Selected="True" Value="M">Male</asp:ListItem>
                            <asp:ListItem Value="F">Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>--%>
                </div>
            </div>
            <%--<div class="col-lg-6"  style="visibility:hidden;">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Birth Date <i class='fa fa-calendar fa-fw'></i></span>
                            <asp:TextBox ID="txtBioDataBirthDate" runat="server" class="form-control tip-bottom" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask name="Date.." placeholder="Date.."  data-original-title="input day birth" type="Date.." ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <label>
                            <asp:CheckBox ID="chkAktiv" CssClass="flat-red tip-bottom" runat="server" Checked="True" Text="IsActive"  data-original-title="Click for Active / Non Active BioData" Enabled="False" />
                            &nbsp;</label>
                    </div>
                </div>
            </div>--%>
        </div>
        <%--<div class="row">
            <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">User Station <i class="fa fa-building fa-fw"></i></span>
                                <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" placeholder="Branch Stay.." name="Branch Stay.." type="Branch Stay..">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading">
                        BioData Contact  <i class='fa fa-mobile-phone fa-fw'></i>
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtBioDataContact" runat="server"  data-original-title="Input BioData Contact" MaxLength="20" CssClass="form-control" placeholder="BioData Contact.." name="BioData Contact.." type="BioData Contact.." data-inputmask='"mask": "(9999) 999-99-999"' data-mask></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>--%>
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary  btn-block btn-flat" Text="Simpan" Visible="False" />

    </div>
</asp:Content>
