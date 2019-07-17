Imports System.Data.Sql
Imports System.Data.SqlClient
Imports SPGeneral
Imports System.Globalization
Public Class TransactionList
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
                            Dim serviceType As String

                            Session("DashBoard") = "Discharge List <i class='fa fa-building fa-fw'></i>"
                            Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                            If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                                Response.Redirect("Home.aspx", False)
                            End If
                            _sama.isiddlMSStatus2(ddlSearch)
                            If UserLogin.providerid <> "" Then

                                If ((Request.QueryString("act").ToString() = "process")) Then
                                    trxid = Request.QueryString("trx").ToString()
                                    dt = _ClsDischarge.getTransaction(trxid, 1, UserLogin.providerid)
                                    _gv_member_detail.DataSource = dt
                                    _gv_member_detail.DataBind()
                                    serviceType = dt(0)("SERVICETYPE").ToString()

                                    Dim policyRemark As String = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                    If policyRemark <> "" Then
                                        _pnl_policy_remark.Visible = True
                                        _Lbl_TPAREMARKS.Text = policyRemark
                                    End If
                                    '_Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)


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
                                    Dim role_code As String = Session("RoleCode")

                                    'jika layanan = RJTP, tidak dimunculkan remaining benefit
                                    'START
                                    If serviceType = "1" Then
                                        GridView1.Columns(4).Visible = False
                                    End If
                                    'END

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

                                        dt = _ClsDischarge.getTransaction(trxid, 3, UserLogin.providerid)
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
                                dt = _ClsDischarge.getTransaction(trxid, 1, UserLogin.providerid)
                                _gv_member_detail.DataSource = dt
                                _gv_member_detail.DataBind()

                                Dim policyRemark As String = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                If policyRemark <> "" Then
                                    _pnl_policy_remark.Visible = True
                                    _Lbl_TPAREMARKS.Text = policyRemark
                                End If
                                '_Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)

                                _mv1.ActiveViewIndex = 1
                                bindCoverage(trxid)
                                If ((Request.QueryString("act").ToString() = "process")) Then
                                    trxid = Request.QueryString("trx").ToString()
                                    dt = _ClsDischarge.getTransaction(trxid, 1, UserLogin.providerid)
                                    _gv_member_detail.DataSource = dt
                                    _gv_member_detail.DataBind()

                                    'policyRemark = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                    'If policyRemark <> "" Then
                                    '    _pnl_policy_remark.Visible = True
                                    '    _Lbl_TPAREMARKS.Text = policyRemark
                                    'End If
                                    '_Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)

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
                                End If

                            End If
                        Catch ex As Exception
                            _mv1.ActiveViewIndex = 0
                            bindData(_tb_search.Text.Trim(), ddlSearch.SelectedValue, UserLogin.providerid)
                        End Try
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - Discharge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=TransactionList.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=TransactionList.aspx", False)
        End If
    End Sub

    Protected Sub bindData(status As String, transaksiID As String, ProviderID As String)
        Try
            _gv_discharge.DataSource = _ClsDischarge.bindDischarge2(status, transaksiID, ProviderID)
            _gv_discharge.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GvDischarge_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles _gv_discharge.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("SelectProcess") Then
            Dim trxId As String = e.CommandArgument
            Response.Redirect("~/TransactionList.aspx?act=process&trx=" + trxId, False)
        ElseIf e.CommandName.Equals("SelectReject") Then
            Dim trxId As String = e.CommandArgument

            _hf_transaction_id.Value = trxId

            _ddl_reject_reason.DataSource = _ClsDischarge.reasonList("TRX")
            _ddl_reject_reason.DataBind()

            PnlMain.Visible = False
            _pnl_reject.Visible = True
        ElseIf e.CommandName.Equals("SelectSLIP") Then
            Dim trxId As String = e.CommandArgument

            viewrpt("WebViewer.aspx", "2", trxId, "SLIP")
        End If
    End Sub

    Sub viewrpt(strform As String, No As String, key1 As String, judul As String) ', key2 As String, key3 As String, key4 As String)
        Try
            'If Session("Syariah") = False Then
            '    Session("No") = "2"
            'Else
            Session("No") = No
            'End If
            Session("key1") = key1
            'Session("key2") = key2
            'Session("key3") = key3
            'Session("key4") = key4
            Session("Param1") = Session("Username")
            Session("Param2") = " " '"Date : " '& Format(CDate(Left(reservation.Text, 10)), "dd-MMM-yyyy") & " S/d " & Format(CDate(Right(reservation.Text, 10)), "dd-MMM-yyyy")
            Session("JudulXls") = judul
            Response.Redirect(strform, False)
            'ClientScript.RegisterStartupScript(Me.GetType, "onClick", "window.open('" & strform & "','_newtab');", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
            bindData(_tb_search.Text.Trim(), ddlSearch.SelectedValue, UserLogin.providerid)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - TransactionList - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
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

    Protected Sub _cb_isrujukan_CheckedChanged(sender As Object, e As EventArgs) Handles _cb_isrujukan.CheckedChanged
        'If _cb_isrujukan.Checked = True Then
        providerName.ReadOnly = Not (_cb_isrujukan.Checked)
        _tb_ReferralRemark.ReadOnly = Not (_cb_isrujukan.Checked)
        'Else

        'End If
    End Sub

    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            bindData(_tb_search.Text.Trim(), ddlSearch.SelectedValue, UserLogin.providerid)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - TransactionList - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Function getVisible(status As String)
        Dim return_val As Boolean
        Dim roleCode As String = UserLogin.RoleCode

        return_val = False

        If (status = "1" Or status = "2") Then
            If roleCode <> "00004" Then
                return_val = True
            End If
        End If

        Return return_val
    End Function

    Protected Function getVisibleSlip(status As String)
        Dim return_val As Boolean
        Dim roleCode As String = UserLogin.RoleCode

        return_val = False

        If (status = "2" Or status = "5") Then
            return_val = True
        End If

        Return return_val
    End Function

    Protected Sub btnSubmitReject_Click(sender As Object, e As EventArgs) Handles _btn_submit_reject.Click

        Dim usridNew As String = Session("UserId")
        Dim reasonId As String = _ddl_reject_reason.SelectedValue
        Dim trxId As String = _hf_transaction_id.Value

        If _ClsDischarge.transactionRejectInfoAction(trxId, 3, reasonId, usridNew) = True Then

            ClientScript.RegisterStartupScript(Me.GetType, "Success", "<script type='text/javascript'>alert('Status berhasil diubah menjadi DITOLAK');window.location='TransactionList.aspx';</script>'")
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Status tidak berhasil diubah');</script>")
        End If


    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        _pnl_reject.Visible = False
        PnlMain.Visible = True
    End Sub
End Class