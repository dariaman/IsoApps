<%@  Page Title="Pengaturan | Profil Pengguna" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserProfile.aspx.vb" Inherits="WebIsomedik.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody">

        <div class="row">
            <div class="col-sm-12">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        Profil Pengguna
                    </div>
                    <div class="panel-body">
                        <table class="table table-bordered table-striped">
                            
                            <tr>
                                <td  colspan="2">
                                    <div class="row">
                                        <div class="col-sm-12">
                                          <%--  <div class="panel panel-default">
                                                <div class="panel-body">
                                                   <center> <asp:Image ID="Image1" runat="server" Style="height: 150px; width: 150px" CssClass="img-thumbnail img-rounded" /></center>
                                                </div>
                                            </div>--%>
 
                                        </div>
                                    </div>
                                    <div class="row" style="visibility:hidden;">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <center><asp:FileUpload ID="FileUpload1" runat="server"  CssClass="btn btn-primary  btn-block btn-flat" Visible="False" />
                                                    <asp:LinkButton ID="LinkUpload" runat="server"  class="btn btn-primary  btn-block btn-flat" Visible="False"><i class='fa fa-upload fa-fw'></i> Up Load</asp:LinkButton>
                                                    </center>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td >Id Pengguna
                        <i class="fa fa-code fa-fw" ></i> 
                                </td>
                                <td >
                                    <asp:Label ID="lblUserId" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >Nama Pengguna
                        <i class="fa fa-user fa-fw" ></i> 
                                </td>
                                <td >
                                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >Email @ <i class='fa fa-envelope-o fa-fw'></i></td>
                                <td >
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >
                            <asp:LinkButton ID="LinkChangeDetail" runat="server" Text=""><p style="line-height: 0%;text-align: Left;"><i class="fa fa-user" ></i> Ubah Detail Pengguna</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td ></td>
                                <td >
                            <asp:LinkButton ID="LinkChangePass" runat="server" Text=""> <p style="line-height: 0%;text-align: Left;"><i class="fa fa-lock" ></i> Ubah Kata Sandi</p></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row" style="visibility:hidden;">
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Question <i class='fa fa-question-circle fa-fw'></i></span>
                            <asp:DropDownList ID="ddlQuestion" runat="server" class="form-control" name="Question" placeholder="Question" >
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Answer <i class='fa  fa-comments fa-fw'></i></span>
                            <asp:TextBox ID="txtAnswer" runat="server" onkeypress="return isKey(event)" data-original-title="Answer" MaxLength="30" class="form-control tip-bottom" placeholder="Answer.." name="Answer.." type="Answer.."></asp:TextBox>
                            <span class="input-group-btn"><i class='fa fa-comments fa-fw'></i></span>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="visibility:hidden;">
            <div class="col-sm-12">
                <div class="panel-body">
            <div class="col-sm-2">
                <div class="form-group input-group">
                                    <span class="input-group-addon"><i class='fa fa-check-square fa-fw'></i></span>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary  btn-block btn-flat tip-bottom" Text="Submit" data-original-title="Click for edit User" />

                </div>
                </div>
                </div>
            </div>
        </div>
        
    </div>
</asp:Content>
