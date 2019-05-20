<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" Title="Transaction | Eligibility"
    CodeBehind="Eligibility.aspx.vb" Inherits="WebIsomedik.Eligibility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <%--<div id="DivBody">--%>
        <asp:Panel ID="Panel2" runat="server" Visible="true" >
            <asp:MultiView ID="_mv1" runat="server">
                <asp:View ID="_v_eligibility" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                        <div class="panel-default" id="_div_hdr" runat="server">                   
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <table class="table table-bordered table-striped">
                                    <tbody>
                                        <tr>
                                            <asp:Panel ID="_pnl_provider" runat="server" Visible="false">
                                            <td class="col-sm-4"  style="text-align:right">                                                
                                               Nama Provider                                           
                                            </td>                                            
                                            <td >
                                                <asp:TextBox ID="providerName" runat="server" onkeypress="return isKey(event)" class="form-control tip-bottom" data-original-title="provider Name" name="Nama Provider..." placeholder="Isi Nama Provider ( ketik minimal 3 huruf untuk mulai pencarian nama provider )..." type="Nama Provider..." Onblur="mylenProviderFunction()" Font-Size="Small"></asp:TextBox>
                                                                <%--<asp:AutoCompleteExtender ID="providerName_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="providerName">
                                                </asp:AutoCompleteExtender>--%>
                                                                <asp:AutoCompleteExtender ID="providerName_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" UseContextKey="True" TargetControlID="providerName" MinimumPrefixLength="3" EnableCaching="true" ServiceMethod="AutocompleteProviderEx" CompletionInterval="100" FirstRowSelected = "false"
                                                                  CompletionListElementID="AutoCompleteElement" CompletionListCssClass="completionList" CompletionListItemCssClass="completionListItem" CompletionListHighlightedItemCssClass="completionListHighlightedItem">
                                                                </asp:AutoCompleteExtender>
                                                <asp:HiddenField ID="_hf_provider_id" runat="server" />
                                            </td>
                                            </asp:Panel>
                                        </tr>
                                        <tr>                                            
                                            <td class="col-sm-4"  style="text-align:right">                                                
                                               Nomor Kartu Peserta                                            
                                            </td>
                                            
                                            <td >
                                                <asp:TextBox ID="_tbCardNo" runat="server" onkeypress="return isKey(event)"
                                                    MaxLength="50" class="form-control tip-bottom" placeholder="Isi Nomor Kartu Peserta..." name="Isi Nomor Kartu Peserta..." type="Isi Nomor Kartu Peserta..." data-original-title="Isi Nomor Kartu Peserta" Font-Size="Small"></asp:TextBox>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td  style="text-align:right">
                                               No.Surat Rujukan ( Jika Ada )
                                            </td>
                                            <td >
                                               <asp:TextBox ID="_tbreferral" runat="server" onkeypress="return isKey(event)" MaxLength="15" class="form-control tip-bottom" placeholder="Nomor Surat Rujukan" name="Nomor Surat Rujukan" type="Nomor Surat Rujukan"   data-original-title="Nomor Surat Rujukan" Font-Size="Small"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="table5" style="text-align:right">                                            
                                            </td>
                                            <td class="auto-table31">
                                                <div class="panel-body" style="text-align:center">                                                
                                                <asp:Button ID="_btnSearch" CssClass="btn btn-primary btn-block btn-flat " runat="server" Text="Cari Data" />                                                    
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
                </asp:View>
                <asp:View ID="_v_detail_member" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body">
                            <div class="table-responsive">
                                <h3 class="box-title"> 
                                    <asp:Label ID="_lbl_header" runat="server"></asp:Label>
                                </h3>
                                <asp:Panel ID="_pnl_member_detail" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="gridMemberDetail" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                    EmptyDataRowStyle-CssClass="empty_data"
                                    EmptyDataText="No data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="NO.KARTU PESERTA" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ALTMEMBID")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NAMA PESERTA" HeaderStyle-Width="15%">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "FULLNAME")%>                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>                                    
                                        <asp:TemplateField HeaderText="ASURANSI" HeaderStyle-Width="15%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PAYORNM")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PERUSAHAAN" HeaderStyle-Width="18%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CLIENTNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="7%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "STATUSNM")%>
                                                <asp:HiddenField ID="_hf_status" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "STATUS")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TGL.EFEKTIF" HeaderStyle-Width="7%" >
                                                <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "EFFDT","{0:yyyy-MM-dd}")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TGL.BERAKHIR" HeaderStyle-Width="7%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "EXPDT", "{0:yyyy-MM-dd}")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NO.SURAT RUJUKAN" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "REFERRAL")%>
                                                <asp:HiddenField ID="_hf_referral" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "REFERRAL")%>' />
                                                <asp:HiddenField ID="_hf_free_service_flag" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "FREE_SERVICE")%>'/>
                                                <asp:HiddenField ID="_hf_client_code" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CLIENTCODE")%>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No.BPJS - FKTP" HeaderStyle-Width="20%" >
                                            <ItemTemplate>
                                                No. BPJS :<%# DataBinder.Eval(Container.DataItem, "USERFIELD9")%><br />FTKP     :<%# DataBinder.Eval(Container.DataItem, "USERFIELD10")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </asp:Panel>
                                <asp:Panel ID="_pnl_member_benefit_vm" runat="server">
                                    <div class="box box-info">                        
                                        <div class="box-header">
                                            <h3 class="box-title">Daftar Manfaat Tambahan</h3>
                                        </div>                      
                                        <%--<asp:label ID="_lbl_benefit_vm" runat="server" CssClass="form-control" ForeColor="#339966" Font-Size="Medium" Visible="false" Font-Bold="True"></asp:label>--%>
                                        <div class="panel-body">
                                            <div class="dataTable_wrapper">
                                                <asp:GridView ID="gridExtraBenefit" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                    DataKeyNames="subprodid" EmptyDataRowStyle-CssClass="empty_data"
                                                    EmptyDataText="No data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="NO." HeaderStyle-Width="5%" >
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1%>                                                  
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                        
                                                        <asp:BoundField HeaderText="KODE PLAN" DataField="altsubid"  HeaderStyle-Width="10%"  />
                                                        <asp:BoundField HeaderText="COVERAGE" DataField="subprodid"  HeaderStyle-Width="10%"  />
                                                        <asp:BoundField HeaderText="NAMA COVERAGE" DataField="subprodnm2" HeaderStyle-Width="50%"  />
                                                        <asp:TemplateField HeaderText="BATASAN TAHUNAN" HeaderStyle-Width="15%" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="ANNUALLIMIT" runat="server" Text='<%# IIf(Eval("sublimit") = 999999999, "Sesuai benefit", FormatNumber(Eval("sublimit"), 2))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                        
                                                        <asp:TemplateField HeaderText="DETAIL" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" >
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "subprodid")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i>Lihat Detail</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                            
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="_pnl_member_benefit_ib" runat="server">
                                <div class="box box-info">
                                    <div class="box-header">
                                        <h3 class="box-title">Daftar Manfaat Utama</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="dataTable_wrapper">
                                            <asp:GridView ID="gridLimitBenefit" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="subprodid" EmptyDataRowStyle-CssClass="empty_data"
                                                EmptyDataText="No data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO." HeaderStyle-Width="5%" >
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>                                                  
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:BoundField HeaderText="KODE PLAN" DataField="altsubid"  HeaderStyle-Width="10%"  />
                                                    <asp:BoundField HeaderText="COVERAGE" DataField="subprodid"  HeaderStyle-Width="10%"  />
                                                    <asp:BoundField HeaderText="NAMA COVERAGE" DataField="subprodnm2" HeaderStyle-Width="50%"  />
                                                    <asp:TemplateField HeaderText="BATASAN TAHUNAN" HeaderStyle-Width="15%" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="ANNUALLIMIT" runat="server" Text='<%# IIf(Eval("sublimit") = 999999999, "Sesuai benefit", FormatNumber(Eval("sublimit"), 2))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:TemplateField HeaderText="DETAIL" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "subprodid")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i>Lihat Detail</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                            
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                </asp:Panel>
                                <asp:Panel ID="_pnl_member_benefit_cob" runat="server">
                                <div class="box box-info">
                                    <div class="box-header">
                                        <h3 class="box-title">Daftar Manfaat COB</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="dataTable_wrapper">
                                            <asp:GridView ID="gridLimitBenefitCOB" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="subprodid" EmptyDataRowStyle-CssClass="empty_data"
                                                EmptyDataText="No data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO." HeaderStyle-Width="5%" >
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>                                                  
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:BoundField HeaderText="KODE PLAN" DataField="altsubid"  HeaderStyle-Width="10%"  />
                                                    <asp:BoundField HeaderText="COVERAGE" DataField="subprodid"  HeaderStyle-Width="10%"  />
                                                    <asp:BoundField HeaderText="NAMA COVERAGE" DataField="subprodnm2" HeaderStyle-Width="50%"  />
                                                    <asp:TemplateField HeaderText="BATASAN TAHUNAN" HeaderStyle-Width="15%" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="ANNUALLIMIT" runat="server" Text='<%# IIf(Eval("sublimit") = 999999999, "Sesuai benefit", FormatNumber(Eval("sublimit"), 2))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:TemplateField HeaderText="DETAIL" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "subprodid")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i>Lihat Detail</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                            
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                </asp:Panel>                                                      
						    </div>
                                </div>
                        </div>
                        <div>
                            <div class="col-sm-12">
                            <div class="panel-body">
                            <asp:DropDownList ID="_ddl_service" runat="server" class="form-control" DataTextField="SERVICENM" DataValueField="SERVICETYPE">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="_req_1" runat="server" ControlToValidate="_ddl_service" Text="*Layanan Harus Dipilih" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div style="text-align:center">
                            
                            <asp:Button ID="_btn_registrasi" CssClass="btn btn-primary" runat="server" Text="Proses Registrasi" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="_v_success" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <h3 class="box-title"> 
                                        <asp:Label ID="_lbl_header_success" runat="server"></asp:Label>
                                    </h3>
                                    <h4 class="box-title"> 
                                        <asp:Label ID="_lbl_result_text" runat="server" Text="Pendaftaran Berhasil Dilakukan"></asp:Label>
                                    </h4>
                                    <table class="table table-bordered table-striped" id="_tbl_result" runat="server">                                
                                        <tbody>
                                            <tr>
                                                <td colspan="2" style="text-align:center; background-color: #0099FF;">
                                                    <asp:Label ID="_lbl_success_hdr" runat="server" Text="Mohon perhatikan Detail Pendaftaran untuk mengetahui Manfaat Kesehatan yang dimiliki Pasien" ForeColor="White"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    Kode Transaksi
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Label ID="_lbl_suc_transaction_id" runat="server" CssClass="form-control"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    Nomor Kartu Peserta
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Label ID="_lbl_suc_card_no" runat="server" CssClass="form-control"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    Nama Peserta
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Label ID="_lbl_suc_name" runat="server" CssClass="form-control"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    Asuransi
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Label ID="_lbl_suc_payor" runat="server" CssClass="form-control"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    Perusahaan
                                                </td>
                                                <td class="auto-table31">
                                                   <asp:Label ID="_lbl_suc_client" runat="server" CssClass="form-control"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    Jenis Layanan
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Label ID="_lbl_suc_benefit" runat="server" CssClass="form-control"></asp:Label>                                            
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    Tgl.Registrasi
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Label ID="_lbl_admisson_date" runat="server" CssClass="form-control"></asp:Label>                                            
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    Tgl.Proses
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Label ID="_lbl_process_date" runat="server" CssClass="form-control"></asp:Label>                                            
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                                    No.Surat Rujukan
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Label ID="_lbl_referral_code" runat="server" CssClass="form-control"></asp:Label>                                            
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table5" style="text-align:right">
                                            
                                                </td>
                                                <td class="auto-table31">
                                                    <asp:Panel ID="_pnl_btn_registration" runat="server">
                                                        <div class="col-sm-2">                                                    
                                                                <asp:Button ID="_btn_registration" CssClass="btn-success" runat="server" Text="Detail Registrasi" />    
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="_pnl_btn_fkc" runat="server">
                                                        <div class="col-sm-2">    
                                                            <asp:Button ID="_btn_fkc" CssClass="btn-warning" runat="server" Text="Rekam Medis Isomedik" />                                                        
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="_pnl_btn_fim" runat="server">
                                                        <div class="col-sm-3">                                                    
                                                                <asp:Button ID="_btn_fim" CssClass="btn-warning" runat="server" Text="Formulir Informasi Medis" />                                                    
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="_pnl_btn_surat_pernyataan" runat="server">
                                                        <div class="col-sm-2">                                                    
                                                                <asp:Button ID="_btn_surat_pernyataan" CssClass="btn-warning" runat="server" Text="Surat Pernyataan" />                                                    
                                                        </div>                                            
                                                    </asp:Panel>
                                                </td>
                                            </tr>                                                                     
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="v_error" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body" style="text-align:center; background-color: #FF0000;">
                                <asp:Label ID="_lbl_error" runat="server" ForeColor="White" Font-Size="Medium"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </asp:Panel>

        <asp:Panel ID="Panel1" runat="server" Visible="false" >
            <%--<div class="modalPopup-dialog" role="grid">
                        <div class="modalPopup-content">
                            <div class="modalPopup-header" id="mGridPict">--%>
                                <table class="table table-striped table-bordered table-hover">
                                    <tr>
                                        <td style="vertical-align: central; text-align: start;">
                                            <center><h4><asp:Label ID="_lbl_popup_header" runat="server"></asp:Label></h4></center>
                                        </td>                            
                                    </tr>
                                </table>
                            <%--</div>
                            <div class="modalPopup-body" style="overflow-y: scroll;">--%>
                                <div class="row">          

                                    <div class="table-responsive" style="overflow-y: scroll; height: 400px; width: 100%;padding-left:45px;">                                                              
                                        <asp:GridView ID="gv_benefit_detail" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        EmptyDataRowStyle-CssClass="empty_data" EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="NO." HeaderStyle-Width="3%" >
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="KODE MANFAAT" HeaderStyle-Width="3%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BENEFITID")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NAMA MANFAAT" HeaderStyle-Width="15%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BENEFITNM2")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                
                                            <asp:TemplateField HeaderText="NILAI MANFAAT" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="_lbl_benefit_amount" runat="server" Text='<%# IIf(Eval("BENLIMAMT") = 999999999, "Sesuai Limit Tahunan", FormatNumber(Eval("BENLIMAMT"), 2))%>'></asp:Label>--%>
                                                    <%# DataBinder.Eval(Container.DataItem, "BENLIMAMT","{0:N2}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SISA MANFAAT" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>                                        
                                                    <b><%# GetRemainingBenefit_2(DataBinder.Eval(Container.DataItem, "ALTMEMBID"), DataBinder.Eval(Container.DataItem, "POLICYNO"), DataBinder.Eval(Container.DataItem, "SUBLIMIT"), DataBinder.Eval(Container.DataItem, "SUBGROUP"), DataBinder.Eval(Container.DataItem, "LIMITAMTID"), DataBinder.Eval(Container.DataItem, "SUBPRODID"), DataBinder.Eval(Container.DataItem, "BENEFITID"), DataBinder.Eval(Container.DataItem, "BENLIMAMT"))%></b>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="BENEFIT UNIT" HeaderStyle-Width="2%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BENLIMUNIT")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="KETERANGAN" HeaderStyle-Width="13%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "LIMITDESC")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                                                                      
                                        </Columns>
                                    </asp:GridView>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Close"><i class="fa fa-times" ></i>Tutup</asp:LinkButton>&nbsp;                   
                                    </div>                                                           
                                </div>
                            <%--</div>
                            <div class="modalPopup-footer">--%>
                                <table class="table-bordered">
                                    <tr>                                                
                                        <td>
                                            
                                        </td>
                                    </tr>
                               </table>
                            <%--</div>
                        </div>            
                    </div>--%>
        </asp:Panel>
    </div>
</asp:Content>
