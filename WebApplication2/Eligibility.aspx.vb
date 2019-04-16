Imports System.Data.Sql
Imports System.Data.SqlClient
Imports SPGeneral
Imports System.Globalization
Public Class Eligibility
    Inherits System.Web.UI.Page
    Dim _ClsEligibility As New WebService.ClsEligibility
    Dim _ClsMemberBenefit As New WebService.ClsMemberBenefit
    Dim _ClsDischarge As New WebService.ClsDischarge
    Dim _sama As New WebService.sama    

    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> _
    Public Shared Function AutocompleteProviderEx(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim CS As String = ConfigurationManager.ConnectionStrings("getSoftware").ConnectionString

        'Dim PROVIDER As New List(Of WebService.ClsProvider)()

        Using con As New SqlConnection(CS)
            Dim cmd As New SqlCommand("Sp_S_PRV_PROVIDER_MASTER_ '%" & prefixText & "%' ", con)
            cmd.CommandType = CommandType.Text
            'cmd.Parameters.AddWithValue("@SearchText", prefixText)
            cmd.Connection = con
            con.Open()
            Dim PROVIDERs As List(Of String) = New List(Of String)
            Dim sdr As SqlDataReader = cmd.ExecuteReader
            While sdr.Read
                PROVIDERs.Add(Trim(sdr("PROVIDERID").ToString))
            End While
            con.Close()
            Return PROVIDERs
        End Using
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not UserLogin Is Nothing Then
            If Not Page.IsPostBack Then
                If UserLogin.IsActive Then
                    Try
                        System.Threading.Thread.Sleep(500)
                        Session("DashBoard") = "Role List<i class='fa fa-gears fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        Dim CardNo, referral As String
                        Dim roleCode As String = UserLogin.RoleCode
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        Dim usridNew As String = Session("userSelect")
                        Dim isNew As String = Session("isNew")

                        Try
                            If ((Request.QueryString("act").ToString() = "detail") And (Request.QueryString("card").ToString() <> "")) Then
                                CardNo = Request.QueryString("card").ToString()
                                referral = Request.QueryString("referral").ToString()

                                bindMemberDetail(CardNo, referral)
                                bindMemberBenefitMutasiVM(CardNo)
                                bindMemberBenefitIB(CardNo)
                                bindMemberBenefitCOB(CardNo)

                                bindServiceType()
                                '_ddl_service.Focus()

                                _mv1.ActiveViewIndex = 1

                                _lbl_header.Text = "Registrasi"
                            ElseIf ((Request.QueryString("act").ToString() = "error") And (Request.QueryString("card").ToString() <> "")) Then
                                _mv1.ActiveViewIndex = 3
                                CardNo = Request.QueryString("card").ToString()
                                _lbl_error.Text = "Member dengan nomor kartu " + CardNo + " tidak aktif atau tidak terdaftar. Mohon Hubungin ISOMEDIK"

                            ElseIf ((Request.QueryString("act").ToString() = "errorstatus") And (Request.QueryString("card").ToString() <> "")) Then
                                _mv1.ActiveViewIndex = 3
                                CardNo = Request.QueryString("card").ToString()
                                _lbl_error.Text = "Data registrasi terakhir dari kartu " + CardNo + " tidak dalam status CLOSED, registrasi baru tidak diperbolehkan. Mohon Hubungin ISOMEDIK"

                            ElseIf ((Request.QueryString("act").ToString() = "summary") And (Request.QueryString("trx").ToString() <> "")) Then
                                bindTrx(Request.QueryString("trx").ToString())
                                _mv1.ActiveViewIndex = 2
                            
                            End If
                        Catch ex As Exception


                            _mv1.ActiveViewIndex = 0


                            If (roleCode = "00001" Or roleCode = "00002") Then
                                _pnl_provider.Visible = True
                                providerName.Focus()
                            Else
                                _tbCardNo.Focus()
                            End If

                        End Try

                        'bindServiceType()
                        '_sama.isiddlMenuParent(ddlMenuParent)
                        '_sama.isiddlRoleDesc(DdlLvlAdmin)
                        'bindDataUnion(txtRole.Text, IIf(ddlMenuParent.SelectedValue.ToString = "99", "", ddlMenuParent.SelectedValue.ToString))
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=Eligibility.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=Eligibility.aspx", False)
        End If
    End Sub

    Protected Sub bindServiceType()
        Dim dt As DataTable
        dt = _ClsEligibility.bindServiceType
        
        If dt.Rows.Count > 0 Then
            _ddl_service.DataSource = dt
            _ddl_service.DataBind()
            
            _ddl_service.Items.Insert(0, New ListItem("Pilih Layanan", ""))
        Else
            _ddl_service.DataSource = Nothing
            _ddl_service.DataBind()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles _btnSearch.Click
        Try

            System.Threading.Thread.Sleep(500)

            Dim dt As DataTable
            Dim cardNo As String
            Dim benefit As String
            Dim referral As String
            Dim roleCode As String = UserLogin.RoleCode
            Dim prvid As String
            cardNo = _tbCardNo.Text.Trim
            referral = _tbreferral.Text.Trim

            If (roleCode = "00001" Or roleCode = "00002") Then
                If providerName.Text.Trim = "" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Provider belum diisi');</script>")
                    Exit Sub
                Else
                    If _ClsDischarge.PROVIDERCd(providerName.Text) = False Then
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Nama Provider tidak ditemukan');</script>")
                        Exit Sub
                    End If
                End If
                prvid = Left(providerName.Text, 10).Trim()                
                Session.Add("EligibleProvider", prvid)
            End If

            dt = _ClsEligibility.bindMemberDetailRegister(cardNo, referral)
            If (dt.Rows.Count > 0) Then
                Response.Redirect("~/Eligibility.aspx?act=detail&card=" + cardNo + "&referral=" + referral, False)
            Else
                Response.Redirect("~/Eligibility.aspx?act=error&card=" + cardNo, False)
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.providerid, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnRegistrasi_Click(sender As Object, e As EventArgs) Handles _btn_registrasi.Click
        Try
            System.Threading.Thread.Sleep(500)
            'gridMenu.CurrentPageIndex = 0

            Dim usridNew As String = Session("UserId")
            Dim roleCode As String = UserLogin.RoleCode
            Dim providerId As String = UserLogin.providerid

            If (roleCode = "00001" Or roleCode = "00002") Then
                providerId = Session("EligibleProvider")
            End If

            If providerId = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Provider tidak ditemukan');</script>")
                Exit Sub
            End If

            Dim dt As DataTable
            Dim cardNo As String, servicetype As String
            Dim memberstatus As String, status As String, referralno As String
            Dim success_flag As String, remark As String
            Dim trxno As String
            Dim trxLastStatus As String
            
            cardNo = Request.QueryString("card").ToString()
            servicetype = _ddl_service.SelectedValue
            Dim _hf_referral As HiddenField = DirectCast(gridMemberDetail.Rows(0).FindControl("_hf_referral"), HiddenField)
            Dim _hf_status As HiddenField = DirectCast(gridMemberDetail.Rows(0).FindControl("_hf_status"), HiddenField)
            referralno = _hf_referral.Value
            memberstatus = _hf_status.Value
            status = "1" '1.open 2.close 3.reject
            'referralno = _lbl_referral.Text.Trim

            'CEK LAST TRANSACTIONSTATUS, JIKA BELUM CLOSE,TIDAK BISA INPUT TRANSAKSI BARU
            dt = _ClsEligibility.trxHdrGetLastStatus(cardNo)
            trxLastStatus = dt(0)("STATUS").ToString

            If ((trxLastStatus = "2") Or (trxLastStatus = "3") Or (trxLastStatus = "6")) Then
                '2.closed
                '3.reject
                '6.reject approval
                dt = _ClsEligibility.trxHdrInsert(providerId, cardNo, servicetype, memberstatus, usridNew, status, referralno)
                If (dt.Rows.Count > 0) Then
                    success_flag = dt(0)("FLAG")

                    If (success_flag = "1") Then
                        trxno = dt(0)("TRANSACTIONID")
                        '_div_hdr.Visible = False


                        Response.Redirect("~/Eligibility.aspx?act=summary&card=" + cardNo + "&trx=" + trxno, False)

                    Else
                        _div_hdr.Visible = False

                        _tbl_result.Visible = False

                        remark = dt(0)("REMARK")
                        _lbl_result_text.Text = "Eligibility was Unsuccessful ( " + remark + " )"

                    End If

                    _btn_registrasi.Enabled = True
                End If
            Else
                Response.Redirect("~/Eligibility.aspx?act=errorstatus&card=" + cardNo, False)
            End If



        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub bindMemberDetail(CardNo As String, Referral As String)
        Dim dt As DataTable
        dt = _ClsEligibility.bindMemberDetailRegister(CardNo, Referral)
        If dt.Rows.Count > 0 Then
            gridMemberDetail.DataSource = dt
            gridMemberDetail.DataBind()
        Else

        End If

    End Sub

    Protected Sub bindMemberBenefitIB(CardNo As String)
        Dim dt As DataTable
        '1.IB, 2.IP, 3.MUTASI VM
        dt = _ClsMemberBenefit.bindMemberBenefitPerSkema(CardNo, "1")        
        If dt.Rows.Count > 0 Then
            gridLimitBenefit.DataSource = dt
            gridLimitBenefit.DataBind()
            _pnl_member_benefit_ib.Visible = True
        Else
            gridLimitBenefit.DataSource = Nothing
            gridLimitBenefit.DataBind()
            _pnl_member_benefit_ib.Visible = False
        End If
    End Sub

    Protected Sub bindMemberBenefitCOB(CardNo As String)
        Dim dt As DataTable
        '1.IB, 2.IP, 3.MUTASI VM
        dt = _ClsMemberBenefit.bindMemberBenefitPerSkema(CardNo, "2")
        If dt.Rows.Count > 0 Then
            gridLimitBenefitCOB.DataSource = dt
            gridLimitBenefitCOB.DataBind()
            _pnl_member_benefit_cob.Visible = True
        Else
            gridLimitBenefitCOB.DataSource = Nothing
            gridLimitBenefitCOB.DataBind()
            _pnl_member_benefit_cob.Visible = False            
        End If
    End Sub

    Protected Sub bindMemberBenefitMutasiVM(CardNo As String)
        Dim dt As DataTable
        Dim free_service_flag As String
        Dim client_code As String

        Dim _hf_free_service_flag As HiddenField = DirectCast(gridMemberDetail.Rows(0).FindControl("_hf_free_service_flag"), HiddenField)
        Dim _hf_client_code As HiddenField = DirectCast(gridMemberDetail.Rows(0).FindControl("_hf_client_code"), HiddenField)

        free_service_flag = _hf_free_service_flag.Value
        client_code = _hf_client_code.Value

        '1.IB, 2.IP, 3.MUTASI VM
        'dt = _ClsMemberBenefit.bindMemberBenefitPerSkema(CardNo, "3")
        'If dt.Rows.Count > 0 Then
        '    gridExtraBenefit.DataSource = dt
        '    gridExtraBenefit.DataBind()
        '    _pnl_member_benefit_vm.Visible = True
        'Else
        '    _pnl_member_benefit_vm.Visible = False
        'End If
        If free_service_flag.ToLower() = "y" Then
            dt = _ClsMemberBenefit.bindMemberBenefitPerSkema(CardNo, "3")            
            If dt.Rows.Count > 0 Then
                gridExtraBenefit.DataSource = dt
                gridExtraBenefit.DataBind()
                _pnl_member_benefit_vm.Visible = True
            Else
                _pnl_member_benefit_vm.Visible = False
            End If
        Else
            dt = _ClsMemberBenefit.getPolicyHolderExtraBenefit(client_code)
            If (dt.Rows.Count > 0) Then
                dt = _ClsMemberBenefit.bindMemberBenefitPerSkema(CardNo, "3")                
                If dt.Rows.Count > 0 Then
                    gridExtraBenefit.DataSource = dt
                    gridExtraBenefit.DataBind()
                    _pnl_member_benefit_vm.Visible = True
                Else
                    _pnl_member_benefit_vm.Visible = False
                End If
            Else
                _pnl_member_benefit_vm.Visible = False
            End If
        End If
    End Sub
    Protected Sub bindTrx(TrxNo As String)
        Dim dt As DataTable
        Dim serviceType As String
        Dim providerId As String = UserLogin.providerid
        '1.IB, 2.IP, 3.MUTASI VM
        dt = _ClsEligibility.trxHdrGet(TrxNo, 1, providerId)
        If (dt.Rows.Count > 0) Then
            serviceType = dt(0)("SERVICETYPE")
            _lbl_header_success.Text = "Registrasi"

            _lbl_suc_transaction_id.Text = dt(0)("TRANSACTIONID")
            _lbl_suc_card_no.Text = dt(0)("CARDNO")
            _lbl_suc_name.Text = dt(0)("MEMBERNM")
            _lbl_suc_payor.Text = dt(0)("PAYORNM")
            _lbl_suc_client.Text = dt(0)("CLIENTNM")
            _lbl_suc_benefit.Text = dt(0)("SERVICENM")
            _lbl_admisson_date.Text = dt(0)("ADMISSIONDT")
            _lbl_process_date.Text = dt(0)("ADMISSIONDT")
            _lbl_referral_code.Text = dt(0)("REFERRALCODE")

            If ((serviceType = "3") Or (serviceType = "4")) Then '1.RJTP 2.RJTL 3.RI 4.IB
                _lbl_success_hdr.Text = "Mohon lengkapi dengan Formulir Informasi Medis, Surat Pernyataan, Surat Rujukan/Pengantar Rawat Inap dan dikirim ke ISOmedik dalam 24 Jam"
                '_btn_registrasi.Visible = True
                '_btn_fim.Visible = True
                '_btn_surat_pernyataan.Visible = True
                '_btn_fkc.Visible = False

                _pnl_btn_registration.Visible = True
                _pnl_btn_fim.Visible = True
                _pnl_btn_surat_pernyataan.Visible = True
                _pnl_btn_fkc.Visible = False
            Else
                _lbl_success_hdr.Text = "Mohon perhatikan Detail Pendaftaran untuk mengetahui Manfaat Kesehatan yang dimiliki Pasien"
                '_btn_registrasi.Visible = True
                '_btn_fim.Visible = False
                '_btn_surat_pernyataan.Visible = False
                '_btn_fkc.Visible = True

                _pnl_btn_registration.Visible = True
                _pnl_btn_fim.Visible = False
                _pnl_btn_surat_pernyataan.Visible = False
                _pnl_btn_fkc.Visible = True
            End If
        Else

        End If
    End Sub

    Private Sub GV_Extra_Benefit_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridExtraBenefit.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("SelectLink") Then
            Dim SubProdId As String = e.CommandArgument
            Dim CardNo As String = Request.QueryString("card").ToString()

            'bindisiData(KEY)
            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim index As Integer = gvRow.RowIndex

            bindMemberDetailBenefit(CardNo, SubProdId)
            _lbl_popup_header.Text = "Detail Manfaat Tambahan ( " + SubProdId + " )"
            'txtBranchCode.ReadOnly = True
        End If
    End Sub

    Private Sub GV_Limit_Benefit_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridLimitBenefit.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("SelectLink") Then
            Dim SubProdId As String = e.CommandArgument
            Dim CardNo As String = Request.QueryString("card").ToString()

            'bindisiData(KEY)
            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim index As Integer = gvRow.RowIndex

            bindMemberDetailBenefit(CardNo, SubProdId)
            _lbl_popup_header.Text = "Detail Manfaat Utama ( " + SubProdId + " )"
            'txtBranchCode.ReadOnly = True
        End If
    End Sub

    Private Sub GV_Limit_Benefit_COB_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridLimitBenefitCOB.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("SelectLink") Then
            Dim SubProdId As String = e.CommandArgument
            Dim CardNo As String = Request.QueryString("card").ToString()

            'bindisiData(KEY)
            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim index As Integer = gvRow.RowIndex

            bindMemberDetailBenefit(CardNo, SubProdId)
            _lbl_popup_header.Text = "Detail Manfaat COB ( " + SubProdId + " )"
            'txtBranchCode.ReadOnly = True
        End If
    End Sub

    Protected Sub bindMemberDetailBenefit(CardNo As String, SubProdId As String)
        Dim dt As DataTable
        dt = _ClsMemberBenefit.bindMemberBenefitDetail(CardNo, SubProdId)
        If dt.Rows.Count > 0 Then
            gv_benefit_detail.DataSource = dt
            gv_benefit_detail.DataBind()
            'LinkMpeModalPopupExtender.Show()
            Panel1.Visible = True
            Panel2.Visible = False
            Dim role_code As String = Session("RoleCode")
            If (role_code = "00004") Then
                gv_benefit_detail.Columns(3).Visible = False
            End If

        Else
            gv_benefit_detail.DataSource = Nothing
            gv_benefit_detail.DataBind()
        End If
    End Sub

    Protected Function GetRemainingBenefit(ByVal subgroup As String, ByVal classlimit As String, ByVal limit As String, ByVal classbenefitused As String, ByVal benefitused As String)
        Dim CultureInfo As CultureInfo = New CultureInfo("en-US")
        Dim class_limit_decimal As Decimal = Convert.ToDecimal(classlimit)
        Dim limit_decimal As Decimal = Convert.ToDecimal(limit)
        Dim class_benefit_used_decimal As Decimal = Convert.ToDecimal(classbenefitused)
        Dim benefit_used_decimal As Decimal = Convert.ToDecimal(benefitused)
        Dim price As String

        If (limit_decimal = 999999999) Then
            If (class_limit_decimal <> 999999999) Then
                price = String.Format(CultureInfo, "{0:N}", (class_limit_decimal - class_benefit_used_decimal))
            Else
                price = String.Format(CultureInfo, "{0:N}", (999999999))
            End If
        Else
            price = String.Format(CultureInfo, "{0:N}", (limit_decimal - benefit_used_decimal))
        End If



        Return price
    End Function

    Protected Function GetRemainingBenefit_2(ByVal cardno As String, ByVal policyno As String, ByVal sublimit As String, ByVal subgroup As String, ByVal limitamtid As String, ByVal subprodid As String, ByVal benefitid As String, ByVal benlimamt As String)
        Dim CultureInfo As CultureInfo = New CultureInfo("en-US")
        Dim dt As DataTable
        Dim class_limit_decimal As Decimal = Convert.ToDecimal(sublimit)
        Dim limit_decimal As Decimal = Convert.ToDecimal(benlimamt)
        Dim benefit_used_decimal As Decimal
        Dim class_benefit_used_decimal As Decimal
        Dim class_total_remaining As Decimal
        Dim class_benefit_used_per_tahun As Decimal
        Dim annual_limit_remaining As Decimal
        Dim price As String
        Dim result As Decimal

        dt = _ClsMemberBenefit.getTotalBenefitAcceptAmount(cardno, policyno, sublimit, subgroup, limitamtid, subprodid, benefitid)
        If (dt.Rows.Count > 0) Then
            benefit_used_decimal = dt(0)("TOTAL_BENEFIT_ACCEPT_AMOUNT")
        End If

        dt = _ClsMemberBenefit.getTotalClassAcceptAmount(cardno, policyno, sublimit, subgroup, limitamtid, subprodid)
        If (dt.Rows.Count > 0) Then
            class_benefit_used_decimal = dt(0)("TOTAL_CLASS_ACCEPT_AMOUNT")
        End If

        If (limit_decimal = 999999999) Then
            If (class_limit_decimal <> 999999999) Then
                If limitamtid = "3" Then
                    'cek sisa annual limit masih ada atau tidak, hitung pemakaian class per tahunnya, ambil sisa actual annual limitnya
                    dt = _ClsMemberBenefit.getTotalClassAcceptAmount(cardno, policyno, sublimit, subgroup, "4", subprodid)
                    If (dt.Rows.Count > 0) Then
                        class_benefit_used_per_tahun = dt(0)("TOTAL_CLASS_ACCEPT_AMOUNT")
                        annual_limit_remaining = class_limit_decimal - class_benefit_used_per_tahun
                    End If
                    price = String.Format(CultureInfo, "{0:N}", (annual_limit_remaining - class_benefit_used_decimal))
                Else
                    price = String.Format(CultureInfo, "{0:N}", (class_limit_decimal - class_benefit_used_decimal))
                End If
                'price = String.Format(CultureInfo, "{0:N}", (class_limit_decimal - class_benefit_used_decimal))
            Else
                price = String.Format(CultureInfo, "{0:N}", (999999999))
            End If
        Else
            If (class_limit_decimal = 999999999) Then
                price = String.Format(CultureInfo, "{0:N}", (limit_decimal - benefit_used_decimal))
            Else
                class_total_remaining = class_limit_decimal - class_benefit_used_decimal
                If (class_total_remaining > limit_decimal) Then
                    price = String.Format(CultureInfo, "{0:N}", (limit_decimal - benefit_used_decimal))
                Else
                    price = String.Format(CultureInfo, "{0:N}", (class_total_remaining - benefit_used_decimal))
                End If
            End If
        End If

        If (price < 0) Then
            price = 0
        End If

        price = String.Format(CultureInfo, "{0:N}", (price))

        Return price
    End Function

    Protected Sub _btn_registration_Click(sender As Object, e As EventArgs) Handles _btn_registration.Click
        viewrpt("WebViewer.aspx")
    End Sub

    Protected Sub _btn_fkc_Click(sender As Object, e As EventArgs) Handles _btn_fkc.Click
        viewrptFKC("WebViewer.aspx")
    End Sub

    Protected Sub _btn_surat_pernyataan_Click(sender As Object, e As EventArgs) Handles _btn_surat_pernyataan.Click
        viewrptSuratPernyataan("WebViewer.aspx")
    End Sub

    Protected Sub _btn_fim_Click(sender As Object, e As EventArgs) Handles _btn_fim.Click
        viewrptFima("WebViewer.aspx")
    End Sub

    Sub viewrpt(strform As String) ', key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            Dim dt As DataTable
            Dim trxid As String = Request.QueryString("trx").ToString()

            'If Session("Syariah") = False Then
            '    Session("No") = "2"
            'Else
            Session("No") = "1"
            'End If
            Session("key1") = trxid
            'Session("key2") = key2
            'Session("key3") = key3
            'Session("key4") = key4
            Session("Param1") = Session("Username")
            'Session("Param2") = "Date : " '& Format(CDate(Left(reservation.Text, 10)), "dd-MMM-yyyy") & " S/d " & Format(CDate(Right(reservation.Text, 10)), "dd-MMM-yyyy")
            Session("Param2") = trxid
            Session("JudulXls") = "Registration Detail"
            Response.Redirect(strform, False)
            'ClientScript.RegisterStartupScript(Me.GetType, "onClick", "window.open('" & strform & "','_newtab');", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub viewrptFKC(strform As String) ', key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            Dim dt As DataTable
            Dim trxid As String = Request.QueryString("trx").ToString()

            'If Session("Syariah") = False Then
            '    Session("No") = "2"
            'Else
            Session("No") = "3"
            'End If
            Session("key1") = trxid
            'Session("key2") = key2
            'Session("key3") = key3
            'Session("key4") = key4
            Session("Param1") = Session("Username")
            'Session("Param2") = "Date : " '& Format(CDate(Left(reservation.Text, 10)), "dd-MMM-yyyy") & " S/d " & Format(CDate(Right(reservation.Text, 10)), "dd-MMM-yyyy")
            Session("Param2") = trxid
            Session("JudulXls") = "ReportFKC"
            Response.Redirect(strform, False)
            'ClientScript.RegisterStartupScript(Me.GetType, "onClick", "window.open('" & strform & "','_newtab');", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub viewrptSuratPernyataan(strform As String) ', key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            Dim dt As DataTable
            Dim trxid As String = Request.QueryString("trx").ToString()

            'If Session("Syariah") = False Then
            '    Session("No") = "2"
            'Else
            Session("No") = "4"
            'End If
            Session("key1") = trxid
            'Session("key2") = key2
            'Session("key3") = key3
            'Session("key4") = key4
            Session("Param1") = Session("Username")
            'Session("Param2") = "Date : " '& Format(CDate(Left(reservation.Text, 10)), "dd-MMM-yyyy") & " S/d " & Format(CDate(Right(reservation.Text, 10)), "dd-MMM-yyyy")
            Session("Param2") = trxid
            Session("JudulXls") = "SuratPernyataan"
            Response.Redirect(strform, False)
            'ClientScript.RegisterStartupScript(Me.GetType, "onClick", "window.open('" & strform & "','_newtab');", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub viewrptFima(strform As String) ', key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            Dim dt As DataTable
            Dim trxid As String = Request.QueryString("trx").ToString()

            'If Session("Syariah") = False Then
            '    Session("No") = "2"
            'Else
            Session("No") = "6"
            'End If
            Session("key1") = trxid
            'Session("key2") = key2
            'Session("key3") = key3
            'Session("key4") = key4
            Session("Param1") = Session("Username")
            'Session("Param2") = "Date : " '& Format(CDate(Left(reservation.Text, 10)), "dd-MMM-yyyy") & " S/d " & Format(CDate(Right(reservation.Text, 10)), "dd-MMM-yyyy")
            Session("Param2") = trxid
            Session("JudulXls") = "SuratPernyataan"
            Response.Redirect(strform, False)
            'ClientScript.RegisterStartupScript(Me.GetType, "onClick", "window.open('" & strform & "','_newtab');", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        System.Threading.Thread.Sleep(500)
        Panel1.Visible = False
        Panel2.Visible = True
    End Sub
End Class