<%@ Page Title="Info | Batasan Benefit Peserta" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="MemberLimitBenefit.aspx.vb" Inherits="WebIsomedik.MemberLimitBenefit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">

    <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="btnSearch1">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                        <asp:TextBox ID="txtCardNo" runat="server" onkeypress="return isKey(event)"
                            MaxLength="50" class="form-control tip-bottom" placeholder="Nomor Kartu..." name="Nomor Kartu..." type="Nomor Kartu..." data-original-title="Nomor Kartu Isomedik"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="btnSearch1" CssClass="btn btn-success  btn-block btn-flat tip-bottom" runat="server" data-original-title="Search data"><i class="fa fa-search"></i></asp:LinkButton></span>
                            <asp:HiddenField ID="_hf_free_service_flag" runat="server" />
                            <asp:HiddenField ID="_hf_client_code" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="_pnl_member_detail" runat="server" Visible="false">
        <div id="Div1">
            <div class="row">
                <div class="col-sm-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">DETAIL Peserta</h3>
                        </div>
                            <div class="panel-body">
                                <div class="dataTable_wrapper">
                                    <asp:GridView ID="gridMemberDetail" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        EmptyDataRowStyle-CssClass="empty_data"
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Nomor Policy " HeaderStyle-Width="10%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "POLICYNO")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nomor Kartu" HeaderStyle-Width="10%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "ALTMEMBID")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Kode Plan" HeaderStyle-Width="9%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PLANID")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Peserta" HeaderStyle-Width="30%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "MEMBID")%> -  <%# DataBinder.Eval(Container.DataItem, "FULLNAME")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jenis Kelamin" HeaderStyle-Width="5%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "SEXNM1")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tanggal Lahir" HeaderStyle-Width="8%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BIRTHDATE")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hubungan" HeaderStyle-Width="8%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "RELSHIPNM1")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No Kaywawan" HeaderStyle-Width="10%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "EMPLOYEEID")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "STATUS")%>                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="FREE SERVICE" HeaderStyle-Width="5%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "FREE_SERVICE")%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            --%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>                        
                    </div>
                </div>
            </div>
        </div>
        </asp:Panel>
        <div id="DivBody">            
            <asp:Panel ID="_pnl_member_benefit" runat="server" Visible="false">
            <div class="row">
                <asp:Panel ID="_pnl_member_benefit_vm" runat="server">
                <div class="col-sm-12">                    
                    <div class="box box-info"> 
                        <div class="box-header">
                            <h3 class="box-title">Daftar Manfaat Tambahan </h3>
                        </div>                      
                        <%--<asp:label ID="_lbl_benefit_vm" runat="server" CssClass="form-control" ForeColor="#339966" Font-Size="Medium" Visible="false" Font-Bold="True"></asp:label>--%>
                        <div class="panel-body">
                            <div class="dataTable_wrapper">
                                <asp:GridView ID="gridExtraBenefit" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                    DataKeyNames="subprodid" EmptyDataRowStyle-CssClass="empty_data"
                                    EmptyDataText="Tidak ada data">
                                    <Columns>
                                        <asp:TemplateField HeaderText="NO." HeaderStyle-Width="5%" >
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>                                                  
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:BoundField HeaderText="KODE PLAN" DataField="altsubid"  HeaderStyle-Width="10%"  />
                                        <asp:BoundField HeaderText="COVERAGE" DataField="subprodid"  HeaderStyle-Width="10%"  />
                                        <asp:BoundField HeaderText="NAMA COVERAGE" DataField="subprodnm2" HeaderStyle-Width="50%"  />
                                        <asp:TemplateField HeaderText="LIMIT TAHUNAN" HeaderStyle-Width="15%" >
                                            <ItemTemplate>
                                                <asp:Label ID="ANNUALLIMIT" runat="server" Text='<%# IIf(Eval("sublimit") = 999999999, "Sesuai benefit", FormatNumber(Eval("sublimit"), 2))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="DETAIL" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "subprodid")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> View DETAIL</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                            
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>                    
                </div>
                </asp:Panel>
            </div>
            <div class="row">
                <asp:Panel ID="_pnl_member_benefit_ib" runat="server">
                <div class="col-sm-12">                    
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
                                        <asp:TemplateField HeaderText="LIMIT TAHUNAN" HeaderStyle-Width="15%" >
                                            <ItemTemplate>
                                                <asp:Label ID="ANNUALLIMIT" runat="server" Text='<%# IIf(Eval("sublimit") = 999999999, "Sesuai benefit", FormatNumber(Eval("sublimit"), 2))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="DETAIL" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "subprodid")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> Lihat Detail</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                            
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>                    
                </div>
                </asp:Panel>
            </div>
            <div class="row">
                <asp:Panel ID="_pnl_member_benefit_cob" runat="server">
                <div class="col-sm-12">                    
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Daftar Maanfaat COB</h3>
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
                                        <asp:TemplateField HeaderText="LIMIT TAHUNAN" HeaderStyle-Width="15%" >
                                            <ItemTemplate>
                                                <asp:Label ID="ANNUALLIMIT" runat="server" Text='<%# IIf(Eval("sublimit") = 999999999, "Sesuai benefit", FormatNumber(Eval("sublimit"), 2))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="DETAIL" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "subprodid")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> Lihat Detail</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                            
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>                    
                </div>
                </asp:Panel>
            </div>
            
            </asp:Panel>
        </div>
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
                                            <asp:TemplateField HeaderText="ID BENEFIT " HeaderStyle-Width="3%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BENEFITID")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NAMA BENEFIT " HeaderStyle-Width="15%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BENEFITNM2")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                
                                            <asp:TemplateField HeaderText="JUMLAH BENEFIT " HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="_lbl_benefit_amount" runat="server" Text='<%# IIf(Eval("BENLIMAMT") = 999999999, "Sesuai Limit Tahunan", FormatNumber(Eval("BENLIMAMT"), 2))%>'></asp:Label>--%>
                                                    <%# DataBinder.Eval(Container.DataItem, "BENLIMAMT","{0:N2}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SISA BENEFIT " HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>                                        
                                                    <b><%# GetRemainingBenefit_2(DataBinder.Eval(Container.DataItem, "ALTMEMBID"), DataBinder.Eval(Container.DataItem, "POLICYNO"), DataBinder.Eval(Container.DataItem, "SUBLIMIT"), DataBinder.Eval(Container.DataItem, "SUBGROUP"), DataBinder.Eval(Container.DataItem, "LIMITAMTID"), DataBinder.Eval(Container.DataItem, "SUBPRODID"), DataBinder.Eval(Container.DataItem, "BENEFITID"), DataBinder.Eval(Container.DataItem, "BENLIMAMT"))%></b>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BENEFIT UNIT" HeaderStyle-Width="2%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BENLIMUNIT")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DESKRIPSI" HeaderStyle-Width="13%" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "LIMITDESC")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                                                                      
                                        </Columns>
                                    </asp:GridView>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Close"><i class="fa fa-times" ></i>Close</asp:LinkButton>&nbsp;                   
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

    

</asp:Content>
