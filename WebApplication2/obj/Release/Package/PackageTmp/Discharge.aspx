<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Discharge.aspx.vb" Inherits="WebIsomedik.Discharge" Title="Transaction | Discharge"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch">
                       
            <div id="DivBody" style="overflow-x:scroll;">
            <asp:MultiView ID="_mv1" runat="server">
                <asp:View ID="_v_discharge_list" runat="server">
                    <div class="row">                
                    <div class="col-sm-12">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                                <asp:TextBox ID="_tb_search" runat="server" class="form-control" placeholder="Cari Nomor Transaksi..." name="Cari Nomor Transaksi..." type="Cari Nomor Transaksi..." data-toggle="tooltip" data-placement="top" title="Input Key" ></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="btnSearch" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Search Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                            </div> 
                        </div>
                    </div>                
                    </div>
                    <div class="row">
                    <div class="col-sm-12">
                        <div class="box box-info">
                            <h3 class="box-title">Daftar Discharge <small></small></h3>

                            <div class="box-body pad">
                                    <div class="panel-body">
                                        <div class="dataTable_wrapper">
                                            <asp:GridView ID="_gv_discharge" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="TRANSACTIONID" AllowPaging="true" EmptyDataRowStyle-CssClass="empty_data"
                                                EmptyDataText="No data Found" OnRowDataBound="GvDischarge_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No." HeaderStyle-Width="2%" ItemStyle-Font-Size="Small" >
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>                                                  
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:BoundField HeaderText="N0.TRANSAKSI" DataField="TRANSACTIONID"  HeaderStyle-Width="7%" ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="PROVIDER" DataField="PROVIDER"  HeaderStyle-Width="6%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="NO.KARTU" DataField="CARDNO" HeaderStyle-Width="10%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="NAMA PESERTA" DataField="MEMBERNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="ASURANSI" DataField="PAYORNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="PERUSAHAAN" DataField="CLIENTNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="LAYANAN" DataField="SERVICENM" HeaderStyle-Width="8%"  ItemStyle-Font-Size="Small" />                                                    
                                                    <asp:BoundField HeaderText="TGL.DAFTAR" DataField="ADMISSIONDT" HeaderStyle-Width="10%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="Status" DataField="STATUSNM" HeaderStyle-Width="3%"  ItemStyle-Font-Size="Small" />
                                                
                                                    <asp:TemplateField HeaderText="" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" >
                                                        <ItemTemplate>                                                            
                                                            <asp:Button ID="_btn_process" runat="server" CssClass="btn-success" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectProcess' Text="Proses" Width="80px"/><br />                                                            
                                                            <asp:Button ID="_btn_registrasi" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectRegistrasi' Text="Registrasi" Width="80px" /><br />
                                                            <asp:Panel ID="_pnl_fim" runat="server" ScrollBars="Auto">
                                                                <asp:Button ID="_btn_fim" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectFIM' Text="FIM"  Width="80px"/><br />
                                                            </asp:Panel>
                                                            <asp:Panel ID="_pnl_sp" runat="server" ScrollBars="Auto">
                                                                <asp:Button ID="_btn_sp" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectSP' Text="SP" Width="80px" /><br />
                                                            </asp:Panel>
                                                            <asp:Panel ID="_pnl_fkc" runat="server" ScrollBars="Auto">
                                                                <asp:Button ID="_btn_fkc" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectFKC' Text="FKC" Width="80px" /><br />
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                            
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                        </div>
                    </div> 
                </asp:View>
                
                <asp:View ID="_v_process" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body">
                                <h3 class="box-title"> 
                                    <asp:Label ID="_lbl_header" runat="server" Text="Discharge"></asp:Label>
                                </h3>                                
                                <asp:GridView ID="_gv_member_detail" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                    EmptyDataRowStyle-CssClass="empty_data"
                                    EmptyDataText="No data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="NO.TRANSAKSI" HeaderStyle-Width="8%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>
                                                <asp:HiddenField ID="_hf_service_type" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SERVICETYPE")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NP.KARTU PESERTA" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CARDNO")%>
                                                <asp:HiddenField ID="_hf_cardno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CARDNO")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NO.POLIS" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "POLICYNO")%>
                                                <asp:HiddenField ID="_hf_policyno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "POLICYNO")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NAME PESERTA" HeaderStyle-Width="20%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MEMBERNM")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PERUSAHAAN" HeaderStyle-Width="22%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CLIENTNM")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ASURANSI" HeaderStyle-Width="20%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PAYORNM")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TGL.REGISRTRASI" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <%# Format(DataBinder.Eval(Container.DataItem, "ADMISSIONDT"),"dd-MMM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                            
                                    </Columns>
                                </asp:GridView>  
 
                                <div style="overflow-x: scroll;">                           
                                <table class="table table-bordered table-striped" style="width:2500px;">
                                   
                                        <tr>
                                            <th colspan="6">
                                                 INFORMASI DETAIL
                                            </th>
                                        </tr>
                                        <%--<tr>
                                            <th >
                                                Doctor
                                            </th>
                                            <th>
                                                Symptoms
                                            </th>
                                            <th>
                                                Diagnose #1
                                            </th>
                                            <th>
                                                Diagnose #2
                                            </th>
                                            <th>
                                                Remark
                                            </th>
                                            <th>
                                                Is Referring
                                            </th>
                                            <th>
                                                Refer to Provider
                                            </th>
                                            <th>
                                                Referral Remark
                                            </th>
                                        </tr>--%>
                                        <tr>
                                            <td style="width:100px">
                                                <asp:TextBox ID="_tb_doctor" runat="server" onkeypress="return isKey(event)" class="form-control tip-bottom" placeholder="Nama Dokter..." name="Nama Dokter..." type="Nama Dokter..."  data-original-title="Dokter" MaxLength="150" TabIndex="1" Font-Size="Small" ></asp:TextBox>
                                                
                                            </td>
                                            <td style="width:100px">
                                                <asp:TextBox ID="_tb_keluhan"  runat="server" onkeypress="return isKey(event)"  onKeyUp="javascript:Check(this, 300);"
                                                onChange="javascript:Check(this, 300);" class="form-control tip-bottom" placeholder="Keluhan..." name="Keluhan..." type="Keluhan..."  data-original-title="Symptoms" TextMode="MultiLine" TabIndex="2" Font-Size="Small"></asp:TextBox>
                                            </td>

                                            <td style="width:120px">
                                                                <asp:TextBox ID="DiagName"  TextMode="MultiLine" onkeypress="return isKey(event)"   runat="server"  class="form-control tip-bottom" placeholder="Nama Diagnosa 1..." name="Nama Diagnosa 1..." type="Nama Diagnosa 1..."  data-original-title="Diagnosa Name 1" Onblur="mylenDiagFunction()" TabIndex="3" Font-Size="Small"></asp:TextBox>
                                                                
                                                                 <asp:AutoCompleteExtender ID="DiagName_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" UseContextKey="True" TargetControlID="DiagName" MinimumPrefixLength="3" EnableCaching="true" ServiceMethod="AutocompleteDiagDescEx" CompletionInterval="100" FirstRowSelected = "false"
                                                                  CompletionListElementID="AutoCompleteElement" CompletionListCssClass="completionList" CompletionListItemCssClass="completionListItem" CompletionListHighlightedItemCssClass="completionListHighlightedItem">
                                                                </asp:AutoCompleteExtender>
                                                                
                                                                 <%--<asp:AutoCompleteExtender ID="DiagName_AutoCompleteExtender" runat="server" ServicePath="~/WebTest.asmx" ServiceMethod="AutocompleteDiagDescEx" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"TargetControlID="DiagName" FirstRowSelected = "false">
                                                                </asp:AutoCompleteExtender>--%>
                                            </td>
                                            
                                            <td style="width:120px">
                                                <asp:TextBox ID="_tb_Remark" runat="server" onkeypress="return isKey(event)"  onKeyUp="javascript:Check(this, 300);"
                                                onChange="javascript:Check(this, 300);" class="form-control tip-bottom" data-original-title="Obat-Obatan" name="Obat-Obatan..."  placeholder="Obat-Obatan..." TextMode="MultiLine" type="Obat-Obatan..." TabIndex="6" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            <td style="width:1%">
                                                <asp:CheckBox ID="_cb_isrujukan" runat="server" CssClass="form-control" AutoPostBack="True" Text="Dirujuk ke" TabIndex="8" Font-Size="Smaller" />
                                            </td>
                                            <td style="width:120px">
                                                <asp:TextBox ID="providerName" runat="server" onkeypress="return isKey(event)" class="form-control tip-bottom" data-original-title="provider Name" name="Nama Provider..." placeholder="Nama Provider..." TextMode="MultiLine" type="Nama Provider..." Onblur="mylenProviderFunction()" ReadOnly="True" TabIndex="9" Font-Size="Small"></asp:TextBox>
                                                                <%--<asp:AutoCompleteExtender ID="providerName_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="providerName">
                                                </asp:AutoCompleteExtender>--%>
                                                                <asp:AutoCompleteExtender ID="providerName_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" UseContextKey="True" TargetControlID="providerName" MinimumPrefixLength="3" EnableCaching="true" ServiceMethod="AutocompleteProviderEx" CompletionInterval="100" FirstRowSelected = "false"
                                                                  CompletionListElementID="AutoCompleteElement" CompletionListCssClass="completionList" CompletionListItemCssClass="completionListItem" CompletionListHighlightedItemCssClass="completionListHighlightedItem">
                                                                </asp:AutoCompleteExtender>
                                            </td>                                                                                      
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td> <asp:TextBox ID="DiagName1"  TextMode="MultiLine" onkeypress="return isKey(event)" runat="server" class="form-control tip-bottom"  placeholder="Nama Diagnosa 2..." name="Nama Diagnosa 2..." type="Nama Diagnosa 2..."  data-original-title="Diagnosa Name 2" Onblur="mylenDiag1Function()" TabIndex="4" Font-Size="Small"></asp:TextBox>
                                                                <%--<asp:AutoCompleteExtender ID="DiagName1_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="DiagName1">
                                                </asp:AutoCompleteExtender>--%>
                                                                <asp:AutoCompleteExtender ID="DiagName1_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" UseContextKey="True" TargetControlID="DiagName1" MinimumPrefixLength="3" EnableCaching="true" ServiceMethod="AutocompleteDiagDescEx" CompletionInterval="100" FirstRowSelected = "false"
                                                                  CompletionListElementID="AutoCompleteElement" CompletionListCssClass="completionList" CompletionListItemCssClass="completionListItem" CompletionListHighlightedItemCssClass="completionListHighlightedItem">
                                                                </asp:AutoCompleteExtender></td>
                                            
                                            <td>
                                                <asp:TextBox ID="_tb_tindakan"  TextMode="MultiLine" onkeypress="return isKey(event)" runat="server" onKeyUp="javascript:Check(this, 300);"
                                                onChange="javascript:Check(this, 300);"   class="form-control tip-bottom" placeholder="Tindakan Medis..." name="Tindakan Medis..." type="Tindakan Medis..."  data-original-title="Tindakan Medis" TabIndex="7" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="_tb_ReferralRemark" TextMode="MultiLine" onkeypress="return isKey(event)" runat="server" onKeyUp="javascript:Check(this, 300);"
                                                onChange="javascript:Check(this, 300);"  class="form-control tip-bottom" data-original-title="Dirujuk ke Nama Dokterr" name="Dirujuk ke Nama Dokter..." placeholder="Dirujuk ke Nama Dokter..."  type="Dirujuk ke Nama Dokter..." ReadOnly="True" TabIndex="10" Font-Size="Small"></asp:TextBox>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td> <asp:TextBox ID="DiagName2"  TextMode="MultiLine" onkeypress="return isKey(event)"  runat="server" class="form-control tip-bottom" placeholder="Nama Diagnosa 3..." name="Nama Diagnosa 3..." type="Nama Diagnosa 3..."  data-original-title="Diagnosa Name 3" Onblur="mylenDiag1Function()" TabIndex="5" Font-Size="Small"></asp:TextBox>
                                                                <%--<asp:AutoCompleteExtender ID="DiagName1_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="DiagName1">
                                                </asp:AutoCompleteExtender>--%>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True" UseContextKey="True" TargetControlID="DiagName2" MinimumPrefixLength="3" EnableCaching="true" ServiceMethod="AutocompleteDiagDescEx" CompletionInterval="100" FirstRowSelected = "false"
                                                                  CompletionListElementID="AutoCompleteElement" CompletionListCssClass="completionList" CompletionListItemCssClass="completionListItem" CompletionListHighlightedItemCssClass="completionListHighlightedItem">
                                                                </asp:AutoCompleteExtender></td>
                                            
                                            <td>                                                
                                            </td>
                                            <td></td>
                                            <td>                                                
                                            </td>
                                        </tr>
                                </table>
                                </div>  
<br />                      <asp:Panel ID="_pnl_policy_remark" runat="server" Visible="false">
                            <div class=" form-group has-warning ">
                                <i class='fa fa-bell-o fa-fw'> </i>Informasi Polis
                                <asp:Label ID="_Lbl_TPAREMARKS" runat="server"  class="form-control control-label" for="inputWarning" text="123"></asp:Label>
                            </div>
                            </asp:Panel>
                        </div>  
<br /> 
                                <div style="overflow-x: scroll;">                           
                                <table class="table table-bordered table-striped" style="width:2500px" >
                                    <thead>
                                        <tr>
                                            <th colspan="10">
                                                 DETAIL BENEFIT
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                Coverage
                                            </td>
                                            <td colspan="9">
                                                <asp:DropDownList ID="_ddl_coverage" runat="server" CssClass="form-control" DataTextField="LONGDESC" DataValueField="SUBPRODID" Width="30%" AutoPostBack="true"> 
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th >
                                                No
                                            </th>
                                            <th>
                                                Nama Manfaat
                                            </th>
                                            <th>
                                                Tipe Manfaat
                                            </th>

                                            <th>
                                                Lama Rawat
                                            </th>
                                            <th id="_th_remaining_benefit" runat="server">
                                                Sisa Manfaat
                                            </th>
                                            <th>
                                                Jumlah Tagihan Per Hari
                                            </th>
                                            <th>
                                                Jumlah Tagihan Diterima (Ori)
                                            </th>
                                            <th>
                                                Jumlah Tagihan Excess (Ori)
                                            </th>
                                            <th>
                                                Jumlah Tagihan Diterima
                                            </th>
                                            <th>
                                                Jumlah Tagihan Excess
                                            </th>
                                        </tr>                                        
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="_rpt_item">
                                            <ItemTemplate >
                                                <tr>
                                                    <td style="width:1%" rowspan="2">
                                                        <asp:Label ID="_lbl_no" runat="server" Text='<%#  Container.ItemIndex + 1 %>' Font-Size="Small" ></asp:Label>
                                                    </td>
                                                    <td style="width:15%" rowspan="2">                                                        
                                                        <asp:Label ID="_lbl_benefit_name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BENEFITNM2").ToString()%>' Font-Size="Small" ></asp:Label>
                                                        <asp:HiddenField ID="_hf_subprod_id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SUBPRODID").ToString()%>' />
                                                        <asp:HiddenField ID="_hf_benefit_id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "BENEFITID").ToString()%>' />
                                                        <asp:HiddenField ID="_hf_sublimit" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SUBLIMIT").ToString()%>' />
                                                        <asp:HiddenField ID="_hf_subgroup" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SUBGROUP").ToString()%>' />
                                                        <asp:HiddenField ID="_hf_limitamtid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LIMITAMTID").ToString()%>' />
                                                        <asp:HiddenField ID="_hf_benlimamt" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "BENLIMAMT").ToString()%>' />
                                                    </td>
                                                    <td style="text-align:right;width:10%">                                                        
                                                        <asp:Label ID="_lbl_LIMITDESC2" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "LIMITDESC2").ToString()%>' Font-Size="Small"></asp:Label>
                                                            <%--<asp:HiddenField ID="_Hf_LIMITAMTID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LIMITAMTID").ToString()%>' />--%>
                                                        <asp:Label ID="_Lbl_LIMITAMTID" runat="server" ForeColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "LIMITAMTID").ToString()%>'></asp:Label>                                                        
                                                        
                                                    </td>
                                                    <td style="text-align:right;width:4%">
                                                        <asp:TextBox ID="_tb_lenght_of_stay" onkeypress="return isKeyNumber(event)" runat="server" CssClass="form-control" MaxLength="2" onClick="this.select();" Text='<%# DataBinder.Eval(Container.DataItem, "LOS").ToString()%>' Font-Size="Small"></asp:TextBox>                                                        
                                                    </td>
                                                    <td style="text-align:right;width:9%" id="_td_remaining_benefit" runat="server">                                                                                                     
                                                        <asp:Label ID="_lbl_eligible_amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ELIGIBLEAMT", "{0:N}").ToString()%>' Font-Size="Small" ></asp:Label>                                                                                                                        
                                                        <asp:HiddenField ID="_hf_load_from_tmp" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONIDTMP").ToString()%>' />
                                                    </td>
                                                    <td style="text-align:right;width:9%">
                                                        <div class="form-group has-success">
                                                            <asp:TextBox ID="_tb_incurred_amount" onkeypress="return isKeyNumber(event)" MaxLength="15" runat="server" CssClass="form-control" onClick="this.select();" Text='<%# DataBinder.Eval(Container.DataItem, "BILLEDAMT").ToString()%>' Font-Size="Small"></asp:TextBox>                                                                                                                    
                                                        </div>
                                                    </td>
                                                    <td style="text-align:right;width:9%">                                                        
                                                        <asp:Label ID="_lbl_orig_accept_amount" MaxLength="15" runat="server" for="control-group info" Text='<%# DataBinder.Eval(Container.DataItem, "ORIGACCEPTAMT", "{0:N}").ToString()%>' Font-Size="Small"></asp:Label>                                                                                                                
                                                    </td>
                                                    <td style="text-align:right;width:9%">   
                                                        <asp:Label ID="_lbl_orig_excess_amount" MaxLength="15" runat="server" for="control-group info" Text='<%# DataBinder.Eval(Container.DataItem, "ORIGEXCESSAMT", "{0:N}").ToString()%>' Font-Size="Small"></asp:Label>                                                                                                                
                                                    </td>
                                                    <td style="text-align:right;width:9%">
                                                        <div class="form-group has-success">
                                                            <asp:TextBox ID="_tb_accept_amount" MaxLength="15" onkeypress="return isKeyNumber(event)" runat="server" CssClass="form-control" onClick="this.select();" Text='<%# DataBinder.Eval(Container.DataItem, "ACCEPTAMT", "{0:N}").ToString()%>' Enabled="false" Font-Size="Small"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td style="text-align:right;width:9%">
                                                        <div class="form-group has-success">
                                                            <asp:TextBox ID="_tb_excess_amount" MaxLength="15" onkeypress="return isKeyNumber(event)"  runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "EXCESSAMT","{0:N}").ToString()%>' Enabled="false" Font-Size="Small"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <%--<td style="text-align:right;width:30%">                                                        
                                                        
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Keterangan</td>
                                                    <td colspan="6"><asp:TextBox ID="_tb_remarks" TextMode="MultiLine" onkeypress="return isKey(event)" runat="server" onKeyUp="javascript:Check(this, 300);" Font-Size="Small"
                                                onChange="javascript:Check(this, 300);" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "REFERRALREMARK").ToString()%>'></asp:TextBox></td>
                                                   <%-- <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>                                            
                                    </tbody>
                                        <tr style="text-align:center">
                                            <td colspan="10" class="auto-table31">
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-2"></div>
                                                <asp:Panel ID="_pnl_btn_calculate" runat="server">
                                                    <div class="col-sm-2" style="text-align:right">
                                                        <asp:Button ID="_btnCalculate" CssClass="btn btn-info" runat="server" Text="Hitung" ValidationGroup="next" UseSubmitBehavior="false"/>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="_pnl_btn_reset" runat="server">
                                                    <div class="col-sm-2" style="text-align:center">
                                                        <asp:Button ID="_btnReset" CssClass="btn btn-primary" runat="server" Text="Input Kembali" ValidationGroup="next" Visible="false" UseSubmitBehavior="false"/>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="_pnl_btn_submit" runat="server">
                                                    <div class="col-sm-2" style="text-align:left">
                                                        <asp:Button ID="_btnSubmit" CssClass="btn btn-success" runat="server" Text="Simpan" Visible="false"  ValidationGroup="next" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>
                                                    </div>
                                                </asp:Panel>
                                                <div class="col-sm-2"></div>
                                                <div class="col-sm-1"></div>
                                            </td>
                                        </tr>
                                </table>
                                </div>
                            </div>
                        </div> 
                         
                    </div>    
                       
                </asp:View>
                <asp:View ID="_v_summary" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body">
                                <h3 class="box-title"> 
                                    <asp:Label ID="Label1" runat="server" Text="Discharge Berhasil"></asp:Label>
                                </h3>                                                                
                                <table class="table table-bordered table-striped" >
                                    <thead>
                                        <tr>
                                            <th colspan="6">
                                                Ringkasan Transaksi
                                            </th>
                                        </tr>                                     
                                        
                                    </thead>
                                    <tbody>
                                        <tr>                                            
                                            <td style="width:15%; text-align: right;">
                                                No.Transaksi
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_trxid" runat="server" ></asp:Label>
                                            </td>
                                            <td style="width:15%">
                                                Perusahaan
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_companynm" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:15%; text-align: right;">
                                                No.Kartu Peserta
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_cardno" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                Asuransi
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_payor" runat="server"></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td style="width:15%; text-align: right;">
                                                Nama Peserta
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_membernm" runat="server"></asp:Label>
                                            </td>
                                            <td style="width:15%; text-align: left;">
                                                Tgl.Registrasi
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_admissiondt" runat="server"></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td style="width:15%; text-align: right;">
                                                Dirujuk ke Rumah Sakit
                                            </td>
                                            <td style="width:35%;">
                                                <asp:Label ID="_lbl_s_referredprv" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                Nomor Rujukan
                                            </td>
                                            <td>
                                                <asp:Label ID="_lbl_s_referralcode" runat="server"></asp:Label>
                                            </td>                                            
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:GridView ID="_gv_summary_detail_benefit" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                    EmptyDataRowStyle-CssClass="empty_data"
                                    OnRowDataBound="_gv_summary_detail_benefit_RowDataBound"
                                    EmptyDataText="No data Found"
                                    ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="NO" HeaderStyle-Width="5%" >
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NAMA MANFAAT" HeaderStyle-Width="28%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "BENEFITNM2")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LAMA RAWAT" HeaderStyle-Width="5%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LOS")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SISA MANFAAT" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "ELIGIBLEAMT"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JUMLAH TAGIHAN" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "BILLEDAMT"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="_lbl_total_incurred_amount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JUMLAH TAGIHAN DITERIMA" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "ACCEPTAMT"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="_lbl_total_accept_amount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JUMLAH TAGIHAN EXCESS" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "EXCESSAMT"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="_lbl_total_excess_amount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KETERANGAN" HeaderStyle-Width="20%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "REMARK")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            
                                    </Columns>
                                </asp:GridView>
                                <div class="panel-body" style="text-align:center">                                    
                                    <asp:Button ID="_btn_slip" CssClass="btn btn-success" runat="server" Text="Cetak Slip" Width="15%" />
                                    <asp:Button ID="_btn_surat_rujukan" CssClass="btn btn-warning" runat="server" Text="Cetak Surat Rujukan" Width="15%" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            </div>
        </asp:Panel>
        
</asp:Content>
