﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TransactionList.aspx.vb" Inherits="WebIsomedik.TransactionList" Title="View | Transaction List"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
                       
            <div id="DivBody" style="overflow-x:scroll;">
            <asp:MultiView ID="_mv1" runat="server">
                <asp:View ID="_v_discharge_list" runat="server">
                    <div class="row">       
                    <div class="col-sm-8">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                               <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By">
                                </asp:DropDownList>
                            </div> 
                        </div>
                    </div>              
                    <div class="col-sm-8">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                                <asp:TextBox ID="_tb_search" runat="server" onkeypress="return isKey(event)"  class="form-control" placeholder="No Transaksi..." name="No Transaksi..." type="No Transaksi..." data-toggle="tooltip" data-placement="top" title="No Transaksi" ></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Search Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                            </div> 
                        </div>
                    </div>              
                    </div>
                    <div class="row">
                    <div class="col-sm-12">
                        <div class="box box-info">
                            <h3 class="box-title">Daftar Transaksi <small></small></h3>

                            <div class="box-body pad">
                                    <div class="panel-body">
                                        <div class="dataTable_wrapper">
                                            <asp:GridView ID="_gv_discharge" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="TRANSACTIONID" AllowPaging="true" EmptyDataRowStyle-CssClass="empty_data"
                                                EmptyDataText="No data Found" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO." HeaderStyle-Width="2%" ItemStyle-Font-Size="Small" >
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>                                                  
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:BoundField HeaderText="NO.TRANSAKSI" DataField="TRANSACTIONID"  HeaderStyle-Width="7%" ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="PROVIDER" DataField="PROVIDER"  HeaderStyle-Width="6%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="NO.KARTU PESERTA" DataField="CARDNO" HeaderStyle-Width="10%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="NAMA PESERTA" DataField="MEMBERNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="ASURANSI" DataField="PAYORNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="PERUSAHAAN" DataField="CLIENTNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="TIPE LAYANAN" DataField="SERVICENM" HeaderStyle-Width="8%"  ItemStyle-Font-Size="Small" />                                                    
                                                    <asp:BoundField HeaderText="TGL.REGISTRASI" DataField="ADMISSIONDT" HeaderStyle-Width="10%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="STATUS" DataField="STATUSNM" HeaderStyle-Width="3%"  ItemStyle-Font-Size="Small" ItemStyle-ForeColor="Red" />
                                                    <asp:BoundField HeaderText="REMARKS" DataField="remarks" HeaderStyle-Width="3%"  ItemStyle-Font-Size="Small" />
                                                
                                                    <asp:TemplateField HeaderText="" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate> 
                                                            <%--<asp:Panel runat="server" Visible='<%# IIf(IsNothing(UserLogin.ProviderId), "True", "False")%>' >--%>                                                      
                                                            <asp:Button ID="_btn_process" runat="server" CssClass="btn-success" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectProcess' Text="DETAIL" Width="70px"  />
                                                            <asp:Panel ID="_pnl_btn_reject" runat="server" ScrollBars="Auto" Visible='<%# getVisible(DataBinder.Eval(Container.DataItem, "STATUS"))%>'>
                                                                <asp:Button ID="_btn_reject" runat="server" CssClass="btn-danger" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectReject' Text="TOLAK" Width="70px"   />
                                                            </asp:Panel>
                                                            <asp:Panel ID="_pnl_btn_slip" runat="server" ScrollBars="Auto" Visible='<%# getVisibleSlip(DataBinder.Eval(Container.DataItem, "STATUS"))%>'>
                                                                <asp:Button ID="_btn_slip" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectSLIP' Text="SLIP" Width="60px" /><br />
                                                            </asp:Panel>
                                                            <%--<asp:Button ID="_btn_fim" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectFIM' Text="FIM" Width="60px"/><br />
                                                            <asp:Button ID="_btn_sp" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectSP' Text="SP" Width="60px" /><br />--%>
                                                            <%--</asp:Panel>--%>   
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
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NO.KARTU PESERTA" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CARDNO")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NO.POLIS" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "POLICYNO")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NAMA PESERTA" HeaderStyle-Width="20%" >
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
                                        <asp:TemplateField HeaderText="TGL.REGISTRASI" HeaderStyle-Width="10%" >
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
                                                <asp:TextBox ID="_tb_doctor" runat="server" class="form-control tip-bottom" placeholder="Nama Dokter..." name="Nama Dokter..." type="Nama Dokter..."  data-original-title="Nama Dokter" ReadOnly="True" Font-Size="Small"></asp:TextBox>
                                             </td>
                                            <td style="width:100px">
                                                <asp:TextBox ID="_tb_keluhan" TextMode="MultiLine" runat="server" class="form-control tip-bottom" placeholder="Keluhan..." name="Keluhan..." type="Keluhan..."  data-original-title="Keluhan" ReadOnly="True" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            <td style="width:120px">
                                                                <asp:TextBox ID="DiagName"  runat="server" TextMode="MultiLine" class="form-control tip-bottom" placeholder="Nama Diagnosa 1..." name="Nama Diagnosa 1..." type="Nama Diagnosa 1..."  data-original-title="Nama Diagnosa 1" ReadOnly="True" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            <td style="width:120px">
                                                <asp:TextBox ID="_tb_Remark" runat="server" class="form-control tip-bottom" data-original-title="Obat-Obatan" name="Obat-Obatan..."  placeholder="Obat-Obatan..." TextMode="MultiLine" type="Obat-Obatan..." ReadOnly="True" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            <td style="width:1%">
                                                <asp:CheckBox ID="_cb_isrujukan" runat="server" CssClass="form-control"  Text="Dirujuk ke" ReadOnly="True" Font-Size="Small"/>
                                            </td>
                                            <td style="width:120px">
                                                <asp:TextBox ID="providerName" runat="server" class="form-control tip-bottom" data-original-title="Nama Provider" name="Nama Provider..." placeholder="Nama Provider..." TextMode="MultiLine" type="Nama Provider..." ReadOnly="True" Font-Size="Small"></asp:TextBox>

                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td> <asp:TextBox ID="DiagName1"  TextMode="MultiLine" runat="server"  class="form-control tip-bottom" placeholder="Nama Diagnosa 2..." name="Nama Diagnosa 2..." type="Nama Diagnosa 2..."  data-original-title="Nama Diagnosa 2" ReadOnly="True" Font-Size="Small"></asp:TextBox>
                                                                
                                            
                                            <td><asp:TextBox ID="_tb_tindakan"  TextMode="MultiLine" runat="server"  class="form-control tip-bottom" placeholder="Tindakan Medis..." name="Tindakan Medis..." type="Tindakan Medis..."  data-original-title="Tindakan Medis" ReadOnly="true" Font-Size="Small" ></asp:TextBox></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="_tb_ReferralRemark" runat="server" class="form-control tip-bottom" data-original-title="Dirujuk ke Nama Dokter" name="Dirujuk ke Nama Dokter..." placeholder="Dirujuk ke Nama Dokter..." TextMode="MultiLine" type="Dirujuk ke Nama Dokter..." ReadOnly="True" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                                <td>&nbsp;</td>
                                                <td></td>
                                                <td> <asp:TextBox ID="DiagName2"  TextMode="MultiLine" runat="server"  class="form-control tip-bottom" placeholder="Nama Diagnosa 3..." name="Nama Diagnosa 3..." type="Nama Diagnosa 3..."  data-original-title="Nama Diagnosa 3" ReadOnly="True" Font-Size="Small"></asp:TextBox>
                                                                
                                            
                                                <td></td>
                                                <td></td>
                                                <td>
                                                  
                                                </td>
                                            
                                            </tr>
                                </table>
                                </div>  
<br />                      <asp:Panel ID="_pnl_policy_remark" runat="server" Visible="false">
                                <div class=" form-group has-warning ">
                                    <i class='fa fa-bell-o fa-fw'></i>Informasi Polis
                                    <asp:Label ID="_Lbl_TPAREMARKS" runat="server"  class="form-control control-label" for="inputWarning" ></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>  
<br /> 
                                <div style="overflow-x: scroll;">                           
                                <table class="table table-bordered table-striped" >
                                    <thead>
                                        <tr>
                                            <th colspan="10">
                                                 DETAIL MANFAAT
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width:10%;">
                                                Coverage
                                            </td>
                                            <td colspan="9">
                                                <asp:DropDownList ID="_ddl_coverage" runat="server" CssClass="form-control" DataTextField="LONGDESC" DataValueField="SUBPRODID" Width="30%" AutoPostBack="true" Enabled="false"> 
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <th >
                                                No
                                            </th>
                                            <th>
                                                Benefit Name
                                            </th>
                                            <th>
                                                Benefit Type 
                                            </th>

                                            <th>
                                                Lenght of stay
                                            </th>
                                            <th>
                                                Remaining Benefit
                                            </th>
                                            <th>
                                                Daily Incurred Amount
                                            </th>
                                            <th>
                                                Orig Accept Amount
                                            </th>
                                            <th>
                                                Orig Excess Amount
                                            </th>
                                            <th>
                                                Accept Amount
                                            </th>
                                            <th>
                                                Excess Amount
                                            </th>
                                        </tr>    --%>                                    
                                    </thead>
                                    <tbody> <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                EmptyDataRowStyle-CssClass="empty_data"
                                                EmptyDataText="No data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="3%" ItemStyle-Font-Size="Small" >
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NAMA BENEFIT" HeaderStyle-Width="17%" ItemStyle-Font-Size="Small" >
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "BENEFITNM2")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:TemplateField HeaderText="TIPE MANFAAT" HeaderStyle-Width="10%" ItemStyle-Font-Size="Small" >
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "LIMITDESC2")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LAMA RAWAT" HeaderStyle-Width="5%" ItemStyle-Font-Size="Small">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "LOS")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SISA MANFAAT" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Size="Small" >
                                                        <ItemTemplate>
                                                            <%# FormatNumber(DataBinder.Eval(Container.DataItem, "ELIGIBLEAMT"), 2)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JUMLAH TAGIHAN PER HARI" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Size="Small">
                                                        <ItemTemplate>
                                                            <%# FormatNumber(DataBinder.Eval(Container.DataItem, "BILLEDAMT"), 2)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JUMLAH TAGIHAN DITERIMA" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Size="Small">
                                                        <ItemTemplate>
                                                            <%# FormatNumber(DataBinder.Eval(Container.DataItem, "ACCEPTAMT"), 2)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JUMLAH TAGIHAN EXCESS" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Size="Small">
                                                        <ItemTemplate>
                                                            <%# FormatNumber(DataBinder.Eval(Container.DataItem, "EXCESSAMT"), 2)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>     
                                                    <asp:TemplateField HeaderText="KETERANGAN" HeaderStyle-Width="15%" ItemStyle-Font-Size="Small">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "REMARK")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                          
                                                </Columns>
                                            </asp:GridView>

                                        </td>
                                            </tr>  
                                    </tbody>
                                        <tr style="text-align:center">
                                            <td colspan="10">
                                                <div style="text-align:center">
                                                </div>
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
                                    <asp:Label ID="Label1" runat="server" Text="Discharge Successful"></asp:Label>
                                </h3>                                                                
                                <table class="table table-bordered table-striped" >
                                    <thead>
                                        <tr>
                                            <th colspan="6">
                                                DISCHARGE SUMMARY
                                            </th>
                                        </tr>                                       
                                        
                                    </thead>
                                    <tbody>
                                        <tr>                                            
                                            <td style="width:15%; text-align: right;">
                                                Transaction ID
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_trxid" runat="server" ></asp:Label>
                                            </td>
                                            <td style="width:15%">
                                                Company
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_companynm" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:15%; text-align: right;">
                                                Card No
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_cardno" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                Payor
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_payor" runat="server"></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td style="width:15%; text-align: right;">
                                                Name
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_membernm" runat="server"></asp:Label>
                                            </td>
                                            <td style="width:15%; text-align: left;">
                                                Admission Date
                                            </td>
                                            <td style="width:35%; text-align: left;">
                                                <asp:Label ID="_lbl_s_admissiondt" runat="server"></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td style="width:15%; text-align: right;">
                                                Referred to Provider
                                            </td>
                                            <td style="width:35%;">
                                                <asp:Label ID="_lbl_s_referredprv" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                Referral Code
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
                                        <asp:TemplateField HeaderText="NO" HeaderStyle-Width="5%" ItemStyle-Font-Size="Small" >
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BENEFIT NAME" HeaderStyle-Width="35%" ItemStyle-Font-Size="Small" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "BENEFITNM2")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REMAINING BENEFIT" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "ELIGIBLEAMT"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INCURRED AMOUNT" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" ItemStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "BILLEDAMT"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="_lbl_total_incurred_amount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACCEPT AMOUNT" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" ItemStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "ACCEPTAMT"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="_lbl_total_accept_amount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EXCESS AMOUNT" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" ItemStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "EXCESSAMT"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="_lbl_total_excess_amount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                            
                                    </Columns>
                                </asp:GridView>
                                <div class="panel-body" style="text-align:center">                                                
                                    <asp:Button ID="_btn_slip" CssClass="btn btn-primary" runat="server" Text="Slip" Width="10%" />
                                    <asp:Button ID="_btn_surat_dokter" CssClass="btn btn-warning" runat="server" Text="Surat Dokter" Width="10%" Visible="false" />
                                    <asp:Button ID="_btn_surat_rujukan" CssClass="btn btn-warning" runat="server" Text="Surat Rujukan" Width="10%" Visible="false" />                                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            </div>
        </asp:Panel>
        <asp:Panel ID="_pnl_reject" runat="server" Visible="false">
            <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body">                                
                                <table class="table table-bordered table-striped" >
                                    <thead>
                                        <tr>
                                            <th colspan="6">
                                                Pilih Alasan Tolak
                                            </th>
                                        </tr>                                       
                                        
                                    </thead>
                                    <tbody>
                                        <tr>                                            
                                            <td >
                                                <asp:DropDownList ID="_ddl_reject_reason" runat="server" CssClass="form-control" DataTextField="REASONNM" DataValueField="REASONID" > 
                                                </asp:DropDownList>
                                               <asp:HiddenField ID="_hf_transaction_id" runat="server" />
                                            </td>                                            
                                        </tr>                                        
                                        <tr>                                                                                        
                                            <td >
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary" data-toggle="tooltip" data-placement="top" title="Back"><i class="fa fa-times" ></i>Tutup</asp:LinkButton>&nbsp;
                                                <asp:Button ID="_btn_submit_reject" CssClass="btn btn-danger" runat="server" Text="Tolak" onClientClick=" return confirm('Anda Yakin Ingin Menolak Discharge Ini?')" />
                                            </td>                                            
                                        </tr>                                        
                                    </tbody>
                                </table>                                                                
                            </div>
                        </div>
                    </div>
        </asp:Panel>
        
</asp:Content>
