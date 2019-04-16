Imports System.Data.Sql
Imports System.Data.SqlClient
Imports SPGeneral
Imports System.Globalization
Public Class ViewDischarge
    Inherits System.Web.UI.Page
    Dim _ClsDischarge As New WebService.ClsDischarge
    Dim _sama As New WebService.sama
    Dim totalIncurredAmount As Decimal = 0
    Dim totalAcceptAmount As Decimal = 0
    Dim totalExcessAmount As Decimal = 0

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
                        Try
                            Dim trxid As String
                            Dim dt As DataTable
                            Dim isreferral As String

                            Session("DashBoard") = "Discharge List <i class='fa fa-building fa-fw'></i>"
                            Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                            If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                                Response.Redirect("Home.aspx", False)
                            End If
                            If UserLogin.ProviderId <> "" Then

                                If ((Request.QueryString("act").ToString() = "process")) Then
                                    trxid = Request.QueryString("trx").ToString()
                                    dt = _ClsDischarge.getTransaction(trxid, 1, UserLogin.ProviderId)
                                    _gv_member_detail.DataSource = dt
                                    _gv_member_detail.DataBind()
                                    '_Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)

                                    Dim policyremarks As String = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                    If policyremarks <> "" Then
                                        _pnl_policy_remark.Visible = True
                                        _Lbl_TPAREMARKS.Text = policyremarks
                                    End If


                                    _mv1.ActiveViewIndex = 1
                                    _tb_doctor.Focus()
                                    Dim dt1 As DataTable = _ClsDischarge.bindMSTRANSACTIONINFO(trxid)
                                    If (dt1.Rows.Count > 0) Then
                                        _tb_doctor.Text = dt1(0)("DOCTORNM")
                                        _tb_keluhan.Text = dt1(0)("SYMPTOM")
                                        DiagName.Text = dt1(0)("DIAGDESC1")
                                        _tb_Remark.Text = dt1(0)("REMARK")
                                        _cb_isrujukan.Checked = dt1(0)("ISREFERRAL").ToString
                                        providerName.Text = dt1(0)("PROVIDERNAME")
                                        DiagName1.Text = dt1(0)("DIAGDESC2")
                                        DiagName2.Text = dt1(0)("DIAGDESC3")
                                        _tb_ReferralRemark.Text = dt1(0)("REFERRALREMARK")
                                        _tb_tindakan.Text = dt1(0)("tindakan")
                                    End If

                                    Dim dt2 As DataTable = _ClsDischarge.bindMSTRANSACTIONBENEFIT(trxid)
                                    If (dt2.Rows.Count > 0) Then
                                        _ddl_coverage.SelectedValue = dt2(0)("SUBPRODID")

                                    End If
                                    GridView1.DataSource = dt2
                                    GridView1.DataBind()
                                    bindCoverage(trxid)
                                    
                                ElseIf ((Request.QueryString("act").ToString() = "summary")) Then
                                    trxid = Request.QueryString("trx").ToString()
                                    dt = _ClsDischarge.getTransaction(trxid, 2, UserLogin.providerid)
                                    _Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                    If (dt.Rows.Count > 0) Then
                                        _lbl_s_trxid.Text = dt(0)("TRANSACTIONID")
                                        _lbl_s_companynm.Text = dt(0)("CLIENTNM")
                                        _lbl_s_cardno.Text = dt(0)("CARDNO")
                                        _lbl_s_payor.Text = dt(0)("PAYORNM")
                                        _lbl_s_membernm.Text = dt(0)("MEMBERNM")
                                        _lbl_s_admissiondt.Text = dt(0)("ADMISSIONDT")
                                        _lbl_s_referredprv.Text = dt(0)("PROVIDERNAME")
                                        _lbl_s_referralcode.Text = dt(0)("REFERRALCODE")
                                        isreferral = dt(0)("isreferral").ToString.ToLower

                                        dt = _ClsDischarge.getTransaction(trxid, 3, UserLogin.ProviderId)
                                        If (dt.Rows.Count > 0) Then
                                            _gv_summary_detail_benefit.DataSource = dt
                                            _gv_summary_detail_benefit.DataBind()
                                        End If

                                        If isreferral = "true" Then
                                            _btn_surat_dokter.Visible = False
                                            _btn_surat_rujukan.Visible = True
                                        Else
                                            _btn_surat_dokter.Visible = True
                                            _btn_surat_rujukan.Visible = False
                                        End If

                                    End If

                                    _mv1.ActiveViewIndex = 2
                                End If
                            Else
                                trxid = Request.QueryString("trx").ToString()
                                dt = _ClsDischarge.getTransaction(trxid, 1, UserLogin.ProviderId)
                                _gv_member_detail.DataSource = dt
                                _gv_member_detail.DataBind()
                                _Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                _mv1.ActiveViewIndex = 1
                                bindCoverage(trxid)
                                If ((Request.QueryString("act").ToString() = "process")) Then
                                    trxid = Request.QueryString("trx").ToString()
                                    dt = _ClsDischarge.getTransaction(trxid, 1, UserLogin.ProviderId)
                                    _gv_member_detail.DataSource = dt
                                    _gv_member_detail.DataBind()
                                    _Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                    _mv1.ActiveViewIndex = 1
                                    _tb_doctor.Focus()
                                    Dim dt1 As DataTable = _ClsDischarge.bindMSTRANSACTIONINFO(trxid)
                                    If (dt1.Rows.Count > 0) Then
                                        _tb_doctor.Text = dt1(0)("DOCTORNM")
                                        _tb_keluhan.Text = dt1(0)("SYMPTOM")
                                        DiagName.Text = dt1(0)("DIAGDESC1")
                                        _tb_Remark.Text = dt1(0)("REMARK")
                                        _cb_isrujukan.Checked = dt1(0)("ISREFERRAL").ToString
                                        providerName.Text = dt1(0)("PROVIDERNAME")
                                        DiagName1.Text = dt1(0)("DIAGDESC2")
                                        DiagName2.Text = dt1(0)("DIAGDESC3")
                                        _tb_ReferralRemark.Text = dt1(0)("REFERRALREMARK")
                                        _tb_tindakan.Text = dt1(0)("TINDAKAN")
                                    End If
                                    Dim dt2 As DataTable = _ClsDischarge.bindMSTRANSACTIONBENEFIT(trxid)
                                    If (dt2.Rows.Count > 0) Then
                                        _ddl_coverage.SelectedValue = dt2(0)("SUBPRODID")

                                    End If
                                    GridView1.DataSource = dt2
                                    GridView1.DataBind()
                                    bindCoverage(trxid)
                                End If

                                End If
                        Catch ex As Exception
                            _mv1.ActiveViewIndex = 0
                            bindData("1", _tb_search.Text.Trim(), 5)
                        End Try
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - Discharge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=ViewDischarge.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=ViewDischarge.aspx", False)
        End If
    End Sub

    Protected Sub bindData(status As String, searchtxt As String, statushdr As String)
        Try
            _gv_discharge.DataSource = _ClsDischarge.bindDischarge(status, searchtxt, UserLogin.ProviderId, statushdr)
            _gv_discharge.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GvDischarge_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles _gv_discharge.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("SelectProcess") Then
            Dim trxId As String = e.CommandArgument
            Response.Redirect("~/ViewDischarge.aspx?act=process&trx=" + trxId, False)
            

            'bindMemberDetailBenefit(CardNo, SubProdId)
            '_lbl_popup_header.Text = "IB Benefit Detail ( " + SubProdId + " )"
            'txtBranchCode.ReadOnly = True
        End If
    End Sub

    Protected Sub GvDischarge_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _btn_fim As Button = TryCast(e.Row.FindControl("_btn_fim"), Button)
            Dim _btn_sp As Button = TryCast(e.Row.FindControl("_btn_sp"), Button)
            If e.Row.Cells(7).Text = "RAWAT INAP" Then
                _btn_fim.Visible = True
                _btn_sp.Visible = True
            Else
                _btn_fim.Visible = False
                _btn_sp.Visible = False
            End If

        End If
    End Sub

    Private Sub gvDischarge_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles _gv_discharge.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            _gv_discharge.PageIndex = e.NewPageIndex
            bindData("1", _tb_search.Text.Trim(), 5)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - ViewDischarge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub


    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles _btnSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            Dim usridNew As String = Session("UserId")
            If _ClsDischarge.transactionHdrUpdateStatus(Request.QueryString("trx").ToString(), "2", usridNew) = True Then
                'ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Status Updated to close');</script>")
                ClientScript.RegisterStartupScript(Me.GetType, "Success", "<script type='text/javascript'>alert('Status berhasil diubah menjadi DISETUJUI');window.location='TransactionList.aspx';</script>'")
            Else

                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Status tidak berhasil diubah');</script>")

            End If


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - ViewDischarge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As EventArgs) Handles _btnReject.Click
        Try
            System.Threading.Thread.Sleep(500)

            PnlMain.Visible = False
            _pnl_reject.Visible = True

            _ddl_reject_reason.DataSource = _ClsDischarge.reasonList("TRX")
            _ddl_reject_reason.DataBind()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - ViewDischarge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnReinput_Click(sender As Object, e As EventArgs) Handles _btnReinput.Click
        Try
            System.Threading.Thread.Sleep(500)
            Dim usridNew As String = Session("UserId")
            Dim dt As DataTable
            Dim trxId As String
            Dim subProdId As String = _ddl_coverage.SelectedValue

            _pnl_summary_transaction_benefit.Visible = False
            _pnl_reinput_transaction_benefit.Visible = True

            _pnl_action_close_discharge.Visible = False
            _pnl_action_reinput_discharge.Visible = True

            trxId = Request.QueryString("trx").ToString()

            dt = _ClsDischarge.bindMemberBenefitDetail(trxId, subProdId)
            If dt.Rows.Count > 0 Then
                _rpt_item.DataSource = dt
                _rpt_item.DataBind()
            Else
                _rpt_item.DataSource = Nothing
                _rpt_item.DataBind()
            End If


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - ViewDischarge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnSubmitReject_Click(sender As Object, e As EventArgs) Handles _btn_submit_reject.Click

        Dim usridNew As String = Session("UserId")
        Dim reasonId As String = _ddl_reject_reason.SelectedValue

        If _ClsDischarge.transactionRejectInfoAction(Request.QueryString("trx").ToString(), 6, reasonId, usridNew) = True Then

            ClientScript.RegisterStartupScript(Me.GetType, "Success", "<script type='text/javascript'>alert('Status Updated to Rejected');window.location='TransactionList.aspx';</script>'")
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Not Updated');</script>")
        End If


    End Sub

    Protected Sub bindCoverage(trxId As String)
        Dim dt As DataTable
        dt = _ClsDischarge.bindCoverage(trxId)

        If dt.Rows.Count > 0 Then
            _ddl_coverage.DataSource = dt
            _ddl_coverage.DataBind()

            _ddl_coverage.Items.Insert(0, New ListItem("Pilih Layanan", ""))
        Else
            _ddl_coverage.DataSource = Nothing
            _ddl_coverage.DataBind()
        End If
    End Sub

    Protected Sub _ddl_coverage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _ddl_coverage.SelectedIndexChanged
        Dim dt As DataTable
        Dim trxId As String

        Dim subProdId = _ddl_coverage.SelectedValue
        trxId = Request.QueryString("trx").ToString()

        dt = _ClsDischarge.bindMemberBenefitDetail(trxId, subProdId)
        'If dt.Rows.Count > 0 Then
        '    _rpt_item.DataSource = dt
        '    _rpt_item.DataBind()
        'Else
        '    _rpt_item.DataSource = Nothing
        '    _rpt_item.DataBind()
        'End If


    End Sub

    Protected Function FormatMoney(money As String)
        Dim CultureInfo As CultureInfo = New CultureInfo("en-US")
        Dim des As Decimal = Convert.ToDecimal(money)
        Dim price As String = String.Format(CultureInfo, "{0:N}", des)

        Return price
    End Function

    Protected Sub _gv_summary_detail_benefit_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowTotalIncurred As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BILLEDAMT"))
            Dim rowTotalAccept As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ACCEPTAMT"))
            Dim rowTotalExcess As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXCESSAMT"))
            totalIncurredAmount = totalIncurredAmount + rowTotalIncurred
            totalAcceptAmount = totalAcceptAmount + rowTotalAccept
            totalExcessAmount = totalExcessAmount + rowTotalExcess
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim _lbl_total_incurred_amount As Label = DirectCast(e.Row.FindControl("_lbl_total_incurred_amount"), Label)
            Dim _lbl_total_accept_amount As Label = DirectCast(e.Row.FindControl("_lbl_total_accept_amount"), Label)
            Dim _lbl_total_excess_amount As Label = DirectCast(e.Row.FindControl("_lbl_total_excess_amount"), Label)
            _lbl_total_incurred_amount.Text = FormatMoney(totalIncurredAmount)
            _lbl_total_accept_amount.Text = FormatMoney(totalAcceptAmount)
            _lbl_total_excess_amount.Text = FormatMoney(totalExcessAmount)
        End If
    End Sub

    'Private Sub _rpt_item_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles _rpt_item.ItemDataBound
    '    Dim a_lbl_eligible_amount As Label = DirectCast(e.Item.FindControl("_lbl_eligible_amount"), Label) ' CType(e.Item.FindControl("hidrating"), HiddenField).Value 'e.Item.FindControl("_hf_eligible_amount")
    '    Dim a_tb_incurred_amount As TextBox = e.Item.FindControl("_tb_incurred_amount")
    '    Dim a_lbl_orig_accept_amount As Label = DirectCast(e.Item.FindControl("_lbl_orig_accept_amount"), Label)
    '    Dim a_lbl_orig_excess_amount As Label = DirectCast(e.Item.FindControl("_lbl_orig_excess_amount"), Label)
    '    Dim a_tb_accept_amount As TextBox = e.Item.FindControl("_tb_accept_amount") 'DirectCast(e.Item.FindControl("_tb_accept_amount"), Label)
    '    Dim a_tb_excess_amount As TextBox = e.Item.FindControl("_tb_excess_amount") ' e.Item.FindControl("_lbl_orig_excess_amount")
    '    Dim a_tb_lenght_of_stay As TextBox = e.Item.FindControl("_tb_lenght_of_stay") 'DirectCast(e.Item.FindControl("_tb_lenght_of_stay"), TextBox)
    '    'Dim a_Lbl_LIMITAMTID As HiddenField = DirectCast(e.Item.FindControl("_Hf_LIMITAMTID"), HiddenField)
    '    Dim a_Lbl_LIMITAMTID As Label = DirectCast(e.Item.FindControl("_Lbl_LIMITAMTID"), Label)
    '    'Const prefix As String = "FrameContent__rpt_item_"
    '    'sender, sender1, target
    '    a_tb_lenght_of_stay.Attributes.Add("onblur", "Javascript:CalculateValues('" & a_lbl_eligible_amount.ClientID & "','" & a_tb_incurred_amount.ClientID & "','" & a_lbl_orig_accept_amount.ClientID & "','" & a_lbl_orig_excess_amount.ClientID & "','" & a_tb_accept_amount.ClientID & "','" & a_tb_excess_amount.ClientID & "','" & a_tb_lenght_of_stay.ClientID & "','" & a_Lbl_LIMITAMTID.ClientID & "');")
    '    a_tb_incurred_amount.Attributes.Add("onblur", "Javascript:CalculateValues('" & a_lbl_eligible_amount.ClientID & "','" & a_tb_incurred_amount.ClientID & "','" & a_lbl_orig_accept_amount.ClientID & "','" & a_lbl_orig_excess_amount.ClientID & "','" & a_tb_accept_amount.ClientID & "','" & a_tb_excess_amount.ClientID & "','" & a_tb_lenght_of_stay.ClientID & "','" & a_Lbl_LIMITAMTID.ClientID & "');")
    'End Sub

    Protected Sub _cb_isrujukan_CheckedChanged(sender As Object, e As EventArgs) Handles _cb_isrujukan.CheckedChanged
        'If _cb_isrujukan.Checked = True Then
        providerName.ReadOnly = Not (_cb_isrujukan.Checked)
        _tb_ReferralRemark.ReadOnly = Not (_cb_isrujukan.Checked)
        'Else

        'End If
    End Sub

    'Protected Sub _btn_slip_Click(sender As Object, e As EventArgs) Handles _btn_slip.Click
    '    viewrpt("WebViewer.aspx", "2", _lbl_s_trxid.Text, "SLIP")
    'End Sub

    'Sub viewrpt(strform As String, No As String, key1 As String, judul As String) ', key2 As String, key3 As String, key4 As String)
    '    Try
    '        'If Session("Syariah") = False Then
    '        '    Session("No") = "2"
    '        'Else
    '        Session("No") = No
    '        'End If
    '        Session("key1") = key1
    '        'Session("key2") = key2
    '        'Session("key3") = key3
    '        'Session("key4") = key4
    '        Session("Param1") = Session("Username")
    '        Session("Param2") = " " '"Date : " '& Format(CDate(Left(reservation.Text, 10)), "dd-MMM-yyyy") & " S/d " & Format(CDate(Right(reservation.Text, 10)), "dd-MMM-yyyy")
    '        Session("JudulXls") = judul
    '        Response.Redirect(strform, False)
    '        'ClientScript.RegisterStartupScript(Me.GetType, "onClick", "window.open('" & strform & "','_newtab');", True)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Protected Sub _btn_surat_rujukan_Click(sender As Object, e As EventArgs) Handles _btn_surat_rujukan.Click
    '    viewrpt("WebViewer.aspx", 5, _lbl_s_trxid.Text, "SURAT RUJUKAN")
    'End Sub

    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            bindData("1", _tb_search.Text.Trim(), 5)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - ViewDischarge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles _btnCalculate.Click
        Try
            System.Threading.Thread.Sleep(500)

            Dim usridNew As String = Session("UserId")
            Dim trxId = Request.QueryString("trx").ToString()
            Dim dt As DataTable
            Dim eligibleamt As Decimal
            Dim providerId As String = UserLogin.providerid
            Dim subProdId As String = _ddl_coverage.SelectedValue

            Dim _hf_cardno As HiddenField = DirectCast(_gv_member_detail.Rows(0).FindControl("_hf_cardno"), HiddenField)
            Dim _hf_policyno As HiddenField = DirectCast(_gv_member_detail.Rows(0).FindControl("_hf_policyno"), HiddenField)

            dt = _ClsDischarge.tmpTransactionBenefitCalculateDelete(trxId)


            For i = 0 To _rpt_item.Items.Count - 1
                Dim _hf_subprod_id As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_subprod_id"), HiddenField)
                Dim _hf_benefit_id As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_benefit_id"), HiddenField)
                Dim _hf_sublimit As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_sublimit"), HiddenField)
                Dim _hf_subgroup As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_subgroup"), HiddenField)
                Dim _hf_limitamtid As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_limitamtid"), HiddenField)
                Dim _hf_benlimamt As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_benlimamt"), HiddenField)
                Dim _tb_lenght_of_stay As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_lenght_of_stay"), TextBox)
                Dim _tb_incurred_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_incurred_amount"), TextBox)
                Dim _lbl_eligible_amount As Label = DirectCast(_rpt_item.Items(i).FindControl("_lbl_eligible_amount"), Label)
                Dim _tb_remarks As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_remarks"), TextBox)

                eligibleamt = CDec(_lbl_eligible_amount.Text)

                dt = _ClsDischarge.tmpTransactionBenefitCalculateInsert(trxId, _hf_subprod_id.Value, _hf_benefit_id.Value, _hf_sublimit.Value, _hf_subgroup.Value, _hf_limitamtid.Value, _tb_lenght_of_stay.Text, _hf_benlimamt.Value, eligibleamt, _tb_incurred_amount.Text, _hf_cardno.Value, _hf_policyno.Value, _tb_remarks.Text.Trim())
            Next
            'start proses calculate
            dt = _ClsDischarge.tmpTransactionBenefitCalculateProcess(trxId)
            'end proses calculate

            'start bind ulang detail
            dt = _ClsDischarge.bindMemberBenefitDetail(trxId, subProdId)
            _rpt_item.DataSource = dt
            _rpt_item.DataBind()
            'end bind ulang detail

            setEnableAfterCalculate()

            '_pnl_summary_transaction_benefit.Visible = True
            '_pnl_reinput_transaction_benefit.Visible = False

            '_pnl_action_close_discharge.Visible = True
            '_pnl_action_reinput_discharge.Visible = False

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles _btnReset.Click
        Try
            System.Threading.Thread.Sleep(500)

            setEnableAfterReset()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnSubmitReinput_Click(sender As Object, e As EventArgs) Handles _btnSubmitReinput.Click
        Try
            System.Threading.Thread.Sleep(500)
            Dim usridNew As String = Session("UserId")
            Dim trxId = Request.QueryString("trx").ToString()
            Dim dt As DataTable

            'set inactive semua row yg sebelumnya
            If (_ClsDischarge.transactionBenefitUpdateStatus(trxId, "I", usridNew) = True) Then

                For i As Integer = 0 To _rpt_item.Items.Count - 1
                    Dim _hf_subprod_id As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_subprod_id"), HiddenField)
                    Dim _hf_benefit_id As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_benefit_id"), HiddenField)
                    Dim _tb_lenght_of_stay As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_lenght_of_stay"), TextBox)
                    Dim _lbl_eligible_amount As Label = DirectCast(_rpt_item.Items(i).FindControl("_lbl_eligible_amount"), Label)
                    Dim _tb_incurred_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_incurred_amount"), TextBox)
                    Dim _lbl_orig_accept_amount As Label = DirectCast(_rpt_item.Items(i).FindControl("_lbl_orig_accept_amount"), Label)
                    Dim _lbl_orig_excess_amount As Label = DirectCast(_rpt_item.Items(i).FindControl("_lbl_orig_excess_amount"), Label)
                    Dim _tb_accept_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_accept_amount"), TextBox)
                    Dim _tb_excess_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_excess_amount"), TextBox)
                    Dim _tb_remarks As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_remarks"), TextBox)

                    Dim subProdId As String = _hf_subprod_id.Value
                    Dim benefitId As String = _hf_benefit_id.Value
                    Dim lenghtofstay As Integer = CInt(_tb_lenght_of_stay.Text)
                    Dim eligibleAmount As Decimal = CDec(_lbl_eligible_amount.Text)
                    Dim incurredAmount As Decimal = CDec(_tb_incurred_amount.Text)
                    Dim origAcceptAmount As Decimal = CDec(Replace(_lbl_orig_accept_amount.Text, ",", ""))
                    Dim origExcessAmount As Decimal = CDec(Replace(_lbl_orig_excess_amount.Text, ",", ""))
                    Dim acceptAmount As Decimal = CDec(Replace(_tb_accept_amount.Text, ",", ""))
                    Dim excessAmount As Decimal = CDec(Replace(_tb_excess_amount.Text, ",", ""))
                    Dim remark As String = _tb_remarks.Text

                    If incurredAmount <> 0 Then
                        dt = _ClsDischarge.transactionBenefitReinputCheckInsert(trxId, subProdId, benefitId, lenghtofstay, eligibleAmount, incurredAmount, origAcceptAmount, origExcessAmount, acceptAmount, excessAmount, remark, "A", usridNew)

                    End If

                Next

                'start insert transactionBenefitlog
                _ClsDischarge.transactionBenefitLogInsert(trxId, "UPDATE", usridNew)
                'end insert transactionBenefitlog

                _pnl_summary_transaction_benefit.Visible = True
                _pnl_reinput_transaction_benefit.Visible = False

                _pnl_action_close_discharge.Visible = True
                _pnl_action_reinput_discharge.Visible = False

            End If


            Dim dt2 As DataTable = _ClsDischarge.bindMSTRANSACTIONBENEFIT(trxId)
            GridView1.DataSource = dt2
            GridView1.DataBind()


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub setEnableAfterCalculate()
        _btnSubmitReinput.Visible = True
        _btnReset.Visible = True
        _btnCalculate.Visible = False

        For i = 0 To _rpt_item.Items.Count - 1
            Dim _tb_lenght_of_stay As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_lenght_of_stay"), TextBox)
            Dim _tb_incurred_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_incurred_amount"), TextBox)
            Dim _tb_accept_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_accept_amount"), TextBox)
            Dim _tb_excess_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_excess_amount"), TextBox)
            Dim _tb_remarks As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_remarks"), TextBox)

            _tb_lenght_of_stay.Enabled = False
            _tb_incurred_amount.Enabled = False
            _tb_accept_amount.Enabled = True
            _tb_excess_amount.Enabled = True

            _tb_remarks.Enabled = False
        Next

    End Sub

    Protected Sub setEnableAfterReset()
        _btnSubmitReinput.Visible = False
        _btnReset.Visible = False
        _btnCalculate.Visible = True

        For i = 0 To _rpt_item.Items.Count - 1
            Dim _tb_lenght_of_stay As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_lenght_of_stay"), TextBox)
            Dim _tb_incurred_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_incurred_amount"), TextBox)
            Dim _tb_accept_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_accept_amount"), TextBox)
            Dim _tb_excess_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_excess_amount"), TextBox)
            Dim _tb_remarks As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_remarks"), TextBox)

            _tb_lenght_of_stay.Enabled = True
            _tb_incurred_amount.Enabled = True
            _tb_accept_amount.Enabled = False
            _tb_excess_amount.Enabled = False

            _tb_remarks.Enabled = True
        Next

    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        _pnl_reject.Visible = False
        PnlMain.Visible = True
    End Sub
End Class