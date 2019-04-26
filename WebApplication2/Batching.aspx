<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Batching.aspx.vb" Inherits="WebIsomedik.Batching" 
    Title="Proses | Batching" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">

<h3 class="box-title"> 
    <asp:Label ID="_lbl_header" Text="Proses Batching Transaksi" runat="server"></asp:Label>
</h3>
        <asp:GridView ID="batching_gv"  
            runat="server" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover"
            DataKeyNames="TRANSACTIONID" 
            EmptyDataRowStyle-CssClass="empty_data"             
            AllowSorting="true"
            GridLines="Both"
            EmptyDataText="No data Found">
            <Columns>
                
<asp:TemplateField HeaderStyle-Width="2%" ItemStyle-Font-Size="Small" >
    <%--<HeaderTemplate>
      <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="selectall"  />
   </HeaderTemplate>--%>

    <ItemTemplate><%# Container.DataItemIndex + 1%></ItemTemplate>
    <ItemTemplate><asp:CheckBox ID="chkSelected" runat="server" style="margin-right: 2px" CssClass="selectrow" /></ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="NO." HeaderStyle-Width="2%" ItemStyle-Font-Size="Small" ><ItemTemplate><%# Container.DataItemIndex + 1%></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="NO.TRANSAKSI" HeaderStyle-Width="8%" >
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>
                        <asp:HiddenField ID="TRANSACTIONID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' />
                        <asp:HiddenField ID="PROVIDERID_PARTNER" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PROVIDERID_PARTNER")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField HeaderText="TRANSACTIONID" DataField="TRANSACTIONID" HeaderStyle-Width="50px" ItemStyle-Font-Size="Small" />--%>
                <asp:BoundField HeaderText="NO KARTU PESERTA" DataField="CARDNO"  HeaderStyle-Width="50px" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="NAMA PROVIDER" DataField="PROVIDERNAME" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="NAMA PROVIDER EKSTERNAL" DataField="PROVIDER_PARTNER_NAME" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="NAMA MEMBER" DataField="MEMBERNM" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="NAMA CLIENT" DataField="CLIENTNM" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="TIPE LAYANAN" DataField="SERVICENM" HeaderStyle-Width="140px" ItemStyle-Font-Size="Small" />
                <asp:BoundField HeaderText="TGL. REGISTRASI" DataField="ADMISSIONDT"  ItemStyle-Font-Size="Small" />
            </Columns>
        </asp:GridView>


    <asp:Button id="btn_save_batch" Text="SIMPAN" class="btn btn-success" runat="server"/>


</asp:Content>

