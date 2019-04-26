<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddMappingProvider.aspx.vb" Inherits="WebIsomedik.AddMappingProvider" 
    Title="Proses | Mapping Provider" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
<h3 class="box-title"> 
<asp:Label ID="_lbl_header" Text="Mapping Provider" runat="server"></asp:Label>
</h3>
<div class="panel-body">
    <div class="table-responsive">
        <table class="table table-bordered table-striped">
        <tbody>
            <tr>                                            
                <td class="col-sm-4"  style="text-align:right">Provider Internal (IsoApps)</td>
                <td ><asp:DropDownList ID="ddlinternal" runat="server" class="form-control" placeholder="Provider  Internal"></asp:DropDownList></td>
            </tr>                                        
            <tr>
                <td  style="text-align:right">Provider Eksternal (REL Isomedik)</td>
                <td ><asp:DropDownList ID="ddleksternal" runat="server" class="form-control" placeholder="Provider Eksternal"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="table5" style="text-align:right">                                            
                </td>
                <td class="auto-table31">
                    <div class="panel-body" style="text-align:left">                                                
                    <asp:Button ID="_btnsimpan" CssClass="btn btn-success" runat="server" Text="Simpan Mapping" />
                        <asp:Button ID="btn_cancel" CssClass="btn btn-primary" runat="server" Text="Batal" />                                                    
                    </div>
                </td>
            </tr>                                    
        </tbody>
        </table>
    </div>
</div>

</asp:Content>