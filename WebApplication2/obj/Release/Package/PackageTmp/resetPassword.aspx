<%@ Page Title="Pengaturan | Reset Sandi" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="resetPassword.aspx.vb" Inherits="WebIsomedik.resetPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Cari dengan </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control tip-bottom" placeholder="Cari dengan" name="Cari dengan" type="Cari dengan"   >
                                <asp:ListItem>Id Pengguna</asp:ListItem>
                                <asp:ListItem Value="User Name">Nama Pengguna</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>

                </div>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="txtSearch" onkeypress="return isKey(event)" runat="server" class="form-control tip-bottom" placeholder="Cari.." name="Cari.." type="Cari.."    data-original-title="Cari Data"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-success btn-block btn-flat tip-bottom" runat="server"    data-original-title="Cari data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
    <div id="DivBody">
            <div class="row">
            <div class="col-sm-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Reset <small>Sandi</small></h3>
                        </div>
                                <div class="panel-body">
                                    <div class="dataTable_wrapper">
                                        <br />
                                        <asp:GridView ID="gridMenu" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="UserId" EmptyDataText="No data Found" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id Pengguna" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserId")%>' CommandName="SelectLink"> <i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "UserId")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Username" HeaderText="Nama Pengguna" />
                                                <asp:BoundField DataField="Password" HeaderText="Sandi" />
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                    </div>
                                </div>
                    </div>
                </div>
            </div>
    </div>
        </asp:Panel>

    <asp:LinkButton ID="LinkMpe" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="LinkMpeModalPopupExtender" PopupControlID="pnlPopup" runat="server"
        TargetControlID="LinkMpe" PopupDragHandleControlID="mGridPict" BackgroundCssClass="Overlay">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;" >
        <div class="modalPopup-dialog" role="document">
            <div class="modalPopup-content">
                <div class="modalPopup-header" id="mGridPict">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>

                            <td style="vertical-align: central; text-align: start;">
                                <asp:LinkButton ID="LinkReset" runat="server" CssClass="btn btn-info btn-sm tip-bottom" Text=""  data-original-title="Reset password"><i class="fa fa-rotate-right">&nbsp;</i>Reset</asp:LinkButton>
                            </td>
                            <td style="vertical-align: central; text-align: end;">
                                <asp:LinkButton ID="LinkClose" runat="server" CssClass="btn btn-info btn-sm tip-bottom"  data-original-title="Close Page"><i class="fa fa-times" ></i></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modalPopup-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        Sandi Saat ini
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td>Id Pengguna
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUserId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nama Pengguna
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Sandi
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                        <div class="col-sm-2">
                                            <asp:Button ID="btnReset" CssClass="btn btn-success btn-block btn-flat" runat="server" Text="Reset" Visible="False" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
