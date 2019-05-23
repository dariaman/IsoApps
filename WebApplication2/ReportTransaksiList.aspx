<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" 
    CodeBehind="ReportTransaksiList.aspx.vb" Inherits="WebIsomedik.ReportTransaksiList"
    Title="Report | Transaksi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" >
            <div id="DivBody">
                    <div class="col-sm-8">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                    <div class="form-group input-group " id="datepicker">
                                        <span class="input-group-addon calender-btn">Admission Date From <i class='fa fa-calendar fa-fw'></i></span>
                                        <asp:TextBox ID="txtFrom" runat="server" class="form-control tip-bottom " placeholder="Admission Date From ..."></asp:TextBox>
                                    </div>
                            </div> 
                            <div class="form-group input-group">
                                <div class="form-group input-group" id="datepicker1">
                                    <span class="input-group-addon calender-btn">Admission Date From <i class="fa fa-calendar fa-fw"></i></span>
                                    <asp:TextBox ID="txtTo" runat="server" class="form-control tip-bottom " placeholder="Admission Date To .." ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group input-group">
                                <div class="form-group input-group" >
                                    <span class="input-group-addon calender-btn">Provider <i class="fa fa-calendar fa-fw"></i></span>
                                    <asp:DropDownList ID="ddlProvider" Font-Names="ddlProvider" class="form-control tip-bottom " runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group input-group">
                                <div class="form-group input-group">
                                    <span class="input-group-addon calender-btn">Client <i class="fa fa-calendar fa-fw"></i></span>
                                    <asp:DropDownList ID="ddlClient" name="ddlClient" class="form-control tip-bottom " runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group input-group">
                                <div class="form-group input-group">
                                    <span class="input-group-addon calender-btn">Status <i class="fa fa-calendar fa-fw"></i></span>
                                    <asp:DropDownList ID="ddlStatus" name="ddlStatus" class="form-control tip-bottom " runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    <asp:Button ID="_btnsubmit" CssClass="btn btn-success" runat="server" Text="SUBMIT" />
                </div>
            </div>
        </asp:Panel>
</asp:Content>
