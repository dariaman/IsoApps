<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TambahProvider.aspx.vb" Inherits="WebIsomedik.TambahProvider" 
    Title="Tambah | Tambah Provider" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
<h3 class="box-title"> 
<asp:Label ID="_lbl_header" Text="Tambah Provider" runat="server"></asp:Label>
</h3>
<div class="panel-body">
    <div class="table-responsive">
        <asp:HiddenField ID="providerid" runat="server"/>

        <table class="table table-bordered table-striped">
        <tbody>
            <tr>                                            
                <td class="col-sm-4"  style="text-align:right">Nama Provider</td>
                <td ><asp:TextBox ID="txtnama" runat="server" class="form-control" placeholder="Nama Provider ... "></asp:TextBox></td>
            </tr>
            <tr>
                <td  style="text-align:right">Alamat Gedung</td>
                <td ><asp:TextBox Rows="2" TextMode="MultiLine" ID="txtAlamat1" runat="server" class="form-control" placeholder="Alamat ..."></asp:TextBox></td>
            </tr>
            <tr>
                <td  style="text-align:right">Alamat2</td>
                <td ><asp:TextBox Rows="2" TextMode="MultiLine" ID="txtAlamat2" runat="server" class="form-control" placeholder="Alamat 2 ..."></asp:TextBox></td>
            </tr>
            <tr>
                <td  style="text-align:right">Alamat3</td>
                <td ><asp:TextBox Rows="2" TextMode="MultiLine" ID="txtAlamat3" runat="server" class="form-control" placeholder="Alamat 3 ..."></asp:TextBox></td>
            </tr>

            <tr>                                            
                <td class="col-sm-4"  style="text-align:right">Kode Pos</td>
                <td ><asp:TextBox ID="txtZipcode" runat="server" class="form-control" placeholder="Kode Pos ... "></asp:TextBox></td>
            </tr>
            <tr>                                            
                <td class="col-sm-4"  style="text-align:right">Telepon </td>
                <td ><asp:TextBox ID="txtphone" runat="server" class="form-control" placeholder="Telepon ... "></asp:TextBox></td>
            </tr>
            <tr>                                            
                <td class="col-sm-4"  style="text-align:right">Telepon 2</td>
                <td ><asp:TextBox ID="txtphone2" runat="server" class="form-control" placeholder="Telepon 2 ... "></asp:TextBox></td>
            </tr>
            <tr>                                            
                <td class="col-sm-4"  style="text-align:right">Fax</td>
                <td ><asp:TextBox ID="txtfax" runat="server" class="form-control" placeholder="Fax ... "></asp:TextBox></td>
            </tr>
            <tr>                                            
                <td class="col-sm-4"  style="text-align:right">Fax2</td>
                <td ><asp:TextBox ID="txtfax2" runat="server" class="form-control" placeholder="Fax2 ... "></asp:TextBox></td>
            </tr>
            <tr>                                            
                <td class="col-sm-4"  style="text-align:right">Email</td>
                <td ><asp:TextBox ID="txtemail" runat="server" class="form-control" placeholder="Email ... "></asp:TextBox></td>
            </tr>
            <tr>
                <td class="table5" style="text-align:right">                                            
                </td>
                <td class="auto-table31">
                    <div class="panel-body" style="text-align:left">                                                
                    <asp:Button ID="_btnsimpan" CssClass="btn btn-success" runat="server" Text="Simpan" />
                        <asp:Button ID="btn_cancel" CssClass="btn btn-primary" runat="server" Text="Batal" />                                                    
                    </div>
                </td>
            </tr>                                    
        </tbody>
        </table>
    </div>
</div>

</asp:Content>