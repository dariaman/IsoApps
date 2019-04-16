<%@ Page Title="Info | Info | Info Policy" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PolicyMemberAdmin.aspx.vb" Inherits="WebIsomedik.PolicyMemberAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">

    <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="btnSearch1">

        <div class="row">
            <asp:Panel ID="PnlSearch" runat="server" Style="background: transparent;">
                <div class="col-sm-12">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control tip-bottom" placeholder="Nomor Kartu..." name="Nomor Kartu..." type="Nomor Kartu..."  data-original-title="Input Key"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-success btn-block btn-flat tip-bottom tip-bottom" runat="server"  data-original-title="Search Data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>

                </div>
            </asp:Panel>
            <%--<div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group"><span class="input-group-addon"><i class='fa fa-plus-square fa-fw'></i></span><asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success btn-block btn-flat" Text="Add New"
                             data-original-title="Click for add new branch" />
                            </div>
                    </div>
                </div>--%>
        </div>

        <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Policy <small>List</small></h3>
                        </div>

                            <div class="panel-body">
                                <div class="dataTable_wrapper">

                                    <asp:GridView ID="gridMenu" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        DataKeyNames="POLICYNO" EmptyDataRowStyle-CssClass="empty_data"
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="NO KARTU" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ALTMEMBID")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "ALTMEMBID")%></asp:LinkButton>
                                                    <asp:HiddenField ID="GFMEMBID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MEMBID")%> ' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MEMBID" HeaderText="CARD NO " />
                                            <asp:BoundField DataField="FULLNAME" HeaderText="MEMBER NAME" />
                                            <asp:BoundField DataField="SEX" HeaderText="GENDER" />
                                            <asp:BoundField DataField="BIRTHDATE" HeaderText="BIRTH DATE" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="RELSHIPNM2" HeaderText="RELATION SHIP" />
                                            <asp:BoundField DataField="EMPLOYEEID" HeaderText="NIK" />
                                            <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                                            <asp:BoundField DataField="POLICYNO" HeaderText="POLICY NO" />

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlPopup" runat="server" Visible="false"  DefaultButton="LinkClose">
        <div id="DivBody1">
            <table class="table table-striped table-bordered table-hover" id="mGridPict" style="height:5px;">
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkClose" runat="server" Text=""  data-original-title="Close Page" CssClass=" tip-bottom"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Exit</p></asp:LinkButton>
                    </td>
                    <%-- <td>
                            <asp:LinkButton ID="LinkSubmit" runat="server" Text=""  data-original-title="Submit Data" > <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Submit</p></asp:LinkButton>
                        </td>--%>
                </tr>
            </table>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">INFORMASI <small>Member</small></h3>

                        </div>

                            <div class="row">
                                <div class="col-sm-4" >
                                        <b><i class="fa fa-user"></i>&nbsp; MEMBER &nbsp; :</b>&nbsp; 
                                        <asp:Label ID="LblPeserta" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                    
                                </div>
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-link"></i>&nbsp; RELATION &nbsp; :</b>&nbsp; 
                                        <asp:Label ID="LblRelasi" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>

                                </div>
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-file-o"></i>&nbsp; POLICY NO &nbsp; :</b>&nbsp; 
                                        <asp:Label ID="LblPOLIS" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-tags"></i>&nbsp; STATUS &nbsp;                            
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblStatus" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>

                                <div class="col-sm-4" style="color:#1c578a">
                                        <b>
                                            <i class='fa fa-male'></i><i class='fa fa-female'></i>
                                            &nbsp; GENDER :</b>&nbsp; 
                                        <asp:Label ID="LblSex" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>

                                </div>
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-credit-card"></i>&nbsp; MEMBER ID &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblMemID" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-credit-card"></i>&nbsp; SUB GROUP &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblClientNm" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-calendar"></i>&nbsp; BIRTH DATE&nbsp; :</b>&nbsp; 
                                        <asp:Label ID="LblBirthDate" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>

                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-list-alt"></i>&nbsp; NIK &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblNIK" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-book"></i>&nbsp; ACCOUNT NO&nbsp; :</b>&nbsp; 
                                        <asp:Label ID="LblAccNo" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                            </div>
                        </div>
                </div>

                <div class="col-sm-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">INFORMATION <small>Limit Remaining</small></h3>
                        </div>
                            <div class="panel-body">
                                <div class="dataTable_wrapper">
                                    <br />
                                    <asp:GridView ID="GVFC_Remain_Benefit" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        DataKeyNames="POLICYNO" EmptyDataRowStyle-CssClass="empty_data"
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:BoundField DataField="SUBPRODNM2" HeaderText="COVERAGE" />
                                            <asp:BoundField DataField="PlanID" HeaderText="PLAN" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    ANNUAL LIMIT
                                                </HeaderTemplate>
                                            <ItemTemplate >
                                            <asp:Label ID="Annual" runat="server" Text='<%# IIf(Eval("SUBLIMIT") = 999999999, "Sesuai benefit", FormatNumber(Eval("SUBLIMIT"), 2))%>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="SUBLIMIT" HeaderText="LIMIT TAHUNAN" DataFormatString="{0:N2}" />--%>
                                            <asp:BoundField DataField="USEDAMT" HeaderText="USAGE" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="USEDPCT" HeaderText="%USAGE" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="FAMILYLIMIT" HeaderText="FAMILY LIMIT" />
                                            <%--<asp:BoundField DataField="REMAININGLIMIT" HeaderText="SISA BENEFIT" DataFormatString="{0:N2}" />--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    REMAINING LIMIT
                                                </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="REMAININGLIMIT" runat="server" Text='<%# IIf(Eval("REMAININGLIMIT") = 999999999, "Sesuai benefit", FormatNumber(Eval("REMAININGLIMIT"), 2))%>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="MEMBID" HeaderText="NO PESERTA" />
                                    <asp:BoundField DataField="SEQNO" HeaderText="SEQ NO" />
                                    <asp:BoundField DataField="PRODUCTID" HeaderText="PRODUCT ID" />
                                    <asp:BoundField DataField="SUBPRODID" HeaderText="SUB PRODUCT ID" />
                                    <asp:BoundField DataField="ALTSUBID" HeaderText="ALT SUB ID" />--%>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="empty_data" />
                                    </asp:GridView>
                                </div>
                            </div>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">INFORMASI <small>Klaim Header</small></h3>
                        </div>

                            <div class="panel-body">
                                <div class="dataTable_wrapper">
                                    <br />
                                    <asp:GridView ID="GVVw_Claim_Info_Header" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        DataKeyNames="CLAIMNO" EmptyDataRowStyle-CssClass="empty_data"
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="CLAIM NO" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CLAIMNO")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "CLAIMNO")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FULLNAME" HeaderText="MEMBER NAME" />
                                            <asp:BoundField DataField="DOR" HeaderText="JOIN DATE" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="BILLEDAMT" HeaderText="BILL AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="ACCEPTAMT" HeaderText="APPROVE AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="ACCEPTAMT" HeaderText="ACCEPTED AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="UNPAIDAMT" HeaderText="UNAPPROVE AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="PAIDDT" HeaderText="PAID DATE" DataFormatString="{0:dd-MMM-yyyy}" />


                                            <%--<asp:BoundField DataField="PLAINID" HeaderText="PLAIN ID" />
                                    <asp:BoundField DataField="PRODUCTID" HeaderText="PRODUCT ID" />
                                    <asp:BoundField DataField="SUBPRODID" HeaderText="SUB PRODUCT ID" />
                                    <asp:BoundField DataField="SUBPRODNM2" HeaderText="SUB PRODUCT NAME" />
                                    <asp:BoundField DataField="ALTSUBID" HeaderText="ALT SUB ID" />
                                    <asp:BoundField DataField="SUBLIMIT" HeaderText="SUB LIMIT" />
                                    <asp:BoundField DataField="USEDAMT" HeaderText="USED AMOUNT" />
                                    <asp:BoundField DataField="USEDPCT" HeaderText="USED PERCENT" />
                                    <asp:BoundField DataField="FAMILYLIMIT" HeaderText="FAMILY LIMIT" />
                                    <asp:BoundField DataField="REMAININGLIMIT" HeaderText="REMAINING LIMIT" />--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                    </div>
                </div>

            </div>
    </asp:Panel>

    <asp:Panel ID="pnlPopup1" runat="server" Visible="false"  DefaultButton="LinkClose">
        <div id="Div1">
            <table class="table table-striped table-bordered table-hover" id="Table1">
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkClose1" runat="server" Text=""  data-original-title="Close Page" CssClass=" tip-bottom"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Exit</p></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">INFORMASI <small>Klaim Detail</small></h3>


                        </div>
                        <div class="box-body pad">
                            <div class="panel-body">
                                <div class="dataTable_wrapper">

                                    <br />
                                    <asp:GridView ID="GVClaim_Info_Detail" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="CLAIMNO" EmptyDataRowStyle-CssClass="empty_data" EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:BoundField DataField="SEQNO" HeaderText="NO" />
                                            <asp:BoundField DataField="BENEFITNM2" HeaderText="BENEFIT NAME" />
                                            <asp:BoundField DataField="BILLEDAMT" HeaderText="CLAIM AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="ACCEPTAMT" HeaderText="PAY AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="ACCEPTAMT" HeaderText="ACCEPTED AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="UNPAIDAMT" HeaderText="ANPAID AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="DIAGDESC" HeaderText="DIAGNOSE" />
                                            <asp:BoundField DataField="REMARK" HeaderText="REMARK" />
                                            <asp:BoundField DataField="ADMISSIONDT" HeaderText="ADMISSION DATE" DataFormatString="{0:dd-MMM-yyyy}" />
                                        </Columns>
                                    </asp:GridView>
                                    </caption>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <%-- <td>
                            <asp:LinkButton ID="LinkSubmit" runat="server" Text=""  data-original-title="Submit Data" > <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Submit</p></asp:LinkButton>
                        </td>--%>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
