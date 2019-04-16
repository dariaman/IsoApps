Imports SPGeneral
Imports System.Globalization
Public Class MemberLimitBenefit
    Inherits System.Web.UI.Page
    Dim _ClsMemberBenefit As New WebService.ClsMemberBenefit
    Dim _sama As New WebService.sama
    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not UserLogin Is Nothing Then
            If Not Page.IsPostBack Then
                If UserLogin.IsActive Then
                    Try
                        Session("DashBoard") = "Role List<i class='fa fa-gears fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFilename(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        Dim usridNew As String = Session("userSelect")
                        Dim isNew As String = Session("isNew")

                        txtCardNo.Focus()
                        '_sama.isiddlMenuParent(ddlMenuParent)
                        '_sama.isiddlRoleDesc(DdlLvlAdmin)
                        'bindDataUnion(txtRole.Text, IIf(ddlMenuParent.SelectedValue.ToString = "99", "", ddlMenuParent.SelectedValue.ToString))
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=MemberLimitBenefit.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=MemberLimitBenefit.aspx", False)
        End If
    End Sub

    Protected Sub bindMemberDetail(CardNo As String)
        Dim dt As DataTable        
        dt = _ClsMemberBenefit.bindMemberDetail(CardNo)
        _pnl_member_benefit.Visible = True
        _pnl_member_detail.Visible = True
        If dt.Rows.Count > 0 Then
            gridMemberDetail.DataSource = dt
            gridMemberDetail.DataBind()
            _hf_free_service_flag.Value = dt(0)("FREE_SERVICE").ToString()
            _hf_client_code.Value = dt(0)("CLIENTCODE").ToString()
        Else
            gridMemberDetail.DataSource = Nothing
            gridMemberDetail.DataBind()
        End If
    End Sub

    Protected Sub bindMemberBenefitIB(CardNo As String)
        Dim dt As DataTable
        '1.IB, 2.IP, 3.MUTASI VM
        dt = _ClsMemberBenefit.bindMemberBenefitPerSkema(CardNo, "1")
        _pnl_member_benefit.Visible = True
        _pnl_member_detail.Visible = True
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
        _pnl_member_benefit.Visible = True
        _pnl_member_detail.Visible = True
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

        '1.IB, 2.IP, 3.MUTASI VM
        'Jika flag mutasi VM= Y, tampilkan benefit extranya
        'Jika flag mutasi VM= N, cek apakah client termasuk client special yang punya benefit extra tanpa perlu mutasi ke vm

        free_service_flag = _hf_free_service_flag.Value.Trim()
        client_code = _hf_client_code.Value.Trim()

        If free_service_flag.ToLower() = "y" Then
            dt = _ClsMemberBenefit.bindMemberBenefitPerSkema(CardNo, "3")
            _pnl_member_benefit.Visible = True
            _pnl_member_detail.Visible = True
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
                _pnl_member_benefit.Visible = True
                _pnl_member_detail.Visible = True
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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            'gridMenu.CurrentPageIndex = 0
            bindMemberDetail(txtCardNo.Text)
            bindMemberBenefitIB(txtCardNo.Text)
            bindMemberBenefitCOB(txtCardNo.Text)
            bindMemberBenefitMutasiVM(txtCardNo.Text)

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub GV_Extra_Benefit_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridExtraBenefit.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("SelectLink") Then
            Dim SubProdId As String = e.CommandArgument
            Dim CardNo As String = txtCardNo.Text.Trim()

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
            Dim CardNo As String = txtCardNo.Text.Trim()

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
            Dim CardNo As String = txtCardNo.Text.Trim()

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
            PnlMain.Visible = False

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

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Panel1.Visible = False
        PnlMain.Visible = True
    End Sub

End Class