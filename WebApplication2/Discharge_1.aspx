<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Discharge.aspx.vb" Inherits="WebIsomedik.Discharge" Title="Transaction | Discharge"%>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        R<asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
                       
            <div id="DivBody">
            <asp:MultiView ID="_mv1" runat="server">
                <asp:View ID="_v_discharge_list" runat="server">
                    <div class="row">                
                    <div class="col-sm-8">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                                <asp:TextBox ID="_tb_search" runat="server" class="form-control" placeholder="Search Code or Description..." name="Search Code or Description..." type="Search Code or Description..." data-toggle="tooltip" data-placement="top" title="Input Key" ></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Search Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                            </div> 
                        </div>
                    </div>                
                    </div>
                    <div class="row">
                    <div class="col-sm-12">

                        <div class="box box-info">
                            <h3 class="box-title">Discharge <small>List</small></h3>

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
                                                    <asp:BoundField HeaderText="Transaction ID" DataField="TRANSACTIONID"  HeaderStyle-Width="7%" ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="Provider" DataField="PROVIDER"  HeaderStyle-Width="6%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="Card No" DataField="CARDNO" HeaderStyle-Width="10%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="Name" DataField="MEMBERNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="Payor" DataField="PAYORNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="Client" DataField="CLIENTNM" HeaderStyle-Width="15%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="Benefit" DataField="SERVICENM" HeaderStyle-Width="8%"  ItemStyle-Font-Size="Small" />                                                    
                                                    <asp:BoundField HeaderText="Admission Date" DataField="ADMISSIONDT" HeaderStyle-Width="10%"  ItemStyle-Font-Size="Small" />
                                                    <asp:BoundField HeaderText="Status" DataField="STATUSNM" HeaderStyle-Width="3%"  ItemStyle-Font-Size="Small" />
                                                
                                                    <asp:TemplateField HeaderText="" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>                                                            
                                                            <asp:Button ID="_btn_process" runat="server" CssClass="btn-success" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectProcess' Text="Process" /><br />
                                                            <asp:Button ID="_btn_fim" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectFIM' Text="FIM" /><br />
                                                            <asp:Button ID="_btn_sp" runat="server" CssClass="btn-warning" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>' CommandName='SelectSP' Text="SP" /><br />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                            
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
                                        <asp:TemplateField HeaderText="TRANSACTION ID" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TRANSACTIONID")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CARD NO" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CARDNO")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NAME" HeaderStyle-Width="20%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MEMBERNM")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="COMPANY" HeaderStyle-Width="25%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CLIENTNM")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAYOR" HeaderStyle-Width="20%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PAYORNM")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ADMISSION DATE" HeaderStyle-Width="15%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ADMISSIONDT")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                            
                                    </Columns>
                                </asp:GridView>                                
                                <table class="table table-bordered table-striped" >
                                    <thead>
                                        <tr>
                                            <th colspan="6">
                                                 INFORMATION
                                            </th>
                                        </tr>
                                        <tr>
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
                                                Is Referring
                                            </th>
                                            <th>
                                                Refer to Provider
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td style="width:15%">
                                                <asp:TextBox ID="_tb_doctor" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="_req_1" runat="server" ControlToValidate="_tb_doctor" Text="*Dokter tidak boleh kosong" ForeColor="Red" ValidationGroup="next"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:20%">
                                                <asp:TextBox ID="_tb_keluhan" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td style="width:20%">
                                                <asp:DropDownList ID="_ddl_diagnose_1" runat="server" class="form-control" >
                                                    <asp:ListItem Text="A00.0 - Respiratory" Value="A00.0"></asp:ListItem>
                                                    <asp:ListItem Text="A00.1 - CHOLERA DUE TO VIBRIO CHOLERAE 01" Value="A00.0"></asp:ListItem>
                                                    <asp:ListItem Text="A00.9 - CHOLERA" Value="A00.0"></asp:ListItem>
                                                    <asp:ListItem Text="A01.0 - TYPHOID FEVER (DEMAM TYPHOID)" Value="A00.0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_ddl_diagnose_1" Text="*Minimal 1 diagnosa" ForeColor="Red" ValidationGroup="next"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:20%">
                                                <asp:DropDownList ID="_ddl_diagnose_2" runat="server" class="form-control" >
                                                    <asp:ListItem Text="A00.0 - Respiratory" Value="A00.0"></asp:ListItem>
                                                    <asp:ListItem Text="A00.1 - CHOLERA DUE TO VIBRIO CHOLERAE 01" Value="A00.0"></asp:ListItem>
                                                    <asp:ListItem Text="A00.9 - CHOLERA" Value="A00.0"></asp:ListItem>
                                                    <asp:ListItem Text="A01.0 - TYPHOID FEVER (DEMAM TYPHOID)" Value="A00.0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width:5%">
                                                <asp:CheckBox ID="_cb_isrujukan" runat="server" CssClass="form-control" />
                                            </td>
                                            <td style="width:20%">
                                                <asp:DropDownList ID="_ddl_provider_rujukan" runat="server" class="form-control" >
                                                    <asp:ListItem Text="JAKARTA RS" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="MEDIKA PERMATA HIJAU RS" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="KOPINDOSAT KLINIK" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="ANNA RS" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-right">
                                                Remark
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="_tb_remark" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <asp:Panel ID="_pnl_referral_remark" runat="server" Visible="true">
                                            <td class="text-right">
                                                Referral Remark
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="_tb_referral_remark" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            </asp:Panel>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-bordered table-striped" >
                                    <thead>
                                        <tr>
                                            <th colspan="8">
                                                 DETAIL BENEFIT
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                Coverage
                                            </td>
                                            <td colspan="7">
                                                <asp:DropDownList ID="_ddl_coverage" runat="server" CssClass="form-control" DataTextField="LONGDESC" DataValueField="SUBPRODID" Width="30%" AutoPostBack="true"> 
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th >
                                                No
                                            </th>
                                            <th>
                                                Benefit Name
                                            </th>
                                            <th>
                                                Remaining Benefit
                                            </th>
                                            <th>
                                                Incurred Amount
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
                                        </tr>                                        
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="_rpt_item">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:3%">
                                                        <%#  Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="width:37%">
                                                        <%# DataBinder.Eval(Container.DataItem, "BENEFITNM2").ToString()%>
                                                        <asp:HiddenField ID="_hf_subprod_id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SUBPRODID").ToString()%>' />
                                                        <asp:HiddenField ID="_hf_benefit_id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "BENEFITID").ToString()%>' />
                                                    </td>
                                                    <td style="text-align:right;width:10%">                                                        
                                                        <asp:Label ID="_lbl_eligible_amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BENLIMAMT").ToString()%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align:right;width:10%">
                                                        <asp:TextBox ID="_tb_incurred_amount" runat="server" CssClass="form-control" onClick="this.select();" Text="0"></asp:TextBox>                                                        
                                                    </td>
                                                    <td style="text-align:right;width:10%">
                                                        <asp:Label ID="_lbl_orig_accept_amount" runat="server" Text="0"></asp:Label>                                                        
                                                    </td>
                                                    <td style="text-align:right;width:10%">
                                                        <asp:Label ID="_lbl_orig_excess_amount" runat="server" Text="0"></asp:Label>                                                        
                                                    </td>
                                                    <td style="text-align:right;width:10%">
                                                        <asp:TextBox ID="_tb_accept_amount" runat="server" CssClass="form-control" onClick="this.select();" Text="0"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align:right;width:10%">
                                                        <asp:TextBox ID="_tb_excess_amount" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>                                            
                                    </tbody>
                                        <tr style="text-align:center">
                                            <td colspan="8">
                                                <div class="col-sm-12" style="text-align:center">                                                    
                                                    <asp:Button ID="_btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"/>                                                    
                                                </div>
                                            </td>
                                        </tr>
                                </table>
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
                                        <asp:TemplateField HeaderText="NO" HeaderStyle-Width="5%" >
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BENEFIT NAME" HeaderStyle-Width="35%" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "BENEFITNM2")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REMAINING BENEFIT" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "ELIGIBLEAMT"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INCURRED AMOUNT" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "BILLEDAMT"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="_lbl_total_incurred_amount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACCEPT AMOUNT" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# FormatMoney(DataBinder.Eval(Container.DataItem, "ACCEPTAMT"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="_lbl_total_accept_amount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EXCESS AMOUNT" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
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
                                    <asp:Button ID="_btn_slip" CssClass="btn btn-success" runat="server" Text="Slip" Width="10%" />
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
        
</asp:Content>
