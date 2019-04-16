Imports SPGeneral
Imports System.Globalization
Public Class Discharge
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
                        Dim trxid As String
                        Dim dt As DataTable
                        Dim isreferral As String

                        Session("DashBoard") = "Discharge List <i class='fa fa-building fa-fw'></i>"
                        Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If

                        Try
                            If ((Request.QueryString("act").ToString() = "process")) Then
                                trxid = Request.QueryString("trx").ToString()
                                _gv_member_detail.DataSource = _ClsDischarge.getTransaction(trxid, 1)
                                _gv_member_detail.DataBind()

                                _mv1.ActiveViewIndex = 1
                                _tb_doctor.Focus()

                                bindCoverage(trxid)

                            ElseIf ((Request.QueryString("act").ToString() = "summary")) Then
                                trxid = Request.QueryString("trx").ToString()
                                dt = _ClsDischarge.getTransaction(trxid, 2)

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

                                    dt = _ClsDischarge.getTransaction(trxid, 3)
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
                        Catch ex As Exception
                            _mv1.ActiveViewIndex = 0
                            bindData("1", _tb_search.Text.Trim())
                        End Try




                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - Discharge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=Discharge.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=DischargeRptItem_ItemDataBound.aspx", False)
        End If
    End Sub

    Protected Sub bindData(status As String, searchtxt As String)
        Try
            _gv_discharge.DataSource = _ClsDischarge.bindDischarge(status, searchtxt)
            _gv_discharge.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GvDischarge_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles _gv_discharge.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("SelectProcess") Then
            Dim trxId As String = e.CommandArgument

            Response.Redirect("~/Discharge.aspx?act=process&trx=" + trxId, False)

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
            bindData("1", _tb_search.Text.Trim())
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub bindDiagnose()

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles _btnSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)


            Dim usridNew As String = Session("UserId")
            Dim dt As DataTable
            Dim trxId, doctor, keluhan, diag1, diag2, isrujukan, remarks, providerRujukan, referralCode As String            

            trxId = Request.QueryString("trx").ToString()
            doctor = _tb_doctor.Text.Trim
            keluhan = _tb_keluhan.Text.Trim
            diag1 = _ddl_diagnose_1.SelectedValue
            diag2 = _ddl_diagnose_2.SelectedValue
            isrujukan = "0"
            remarks = ""
            referralCode = ""
            providerRujukan = "0"

            If (_cb_isrujukan.Checked) Then
                isrujukan = "1"
                providerRujukan = _ddl_provider_rujukan.SelectedValue

                dt = _ClsDischarge.getTransactionReferralCode()
                referralCode = dt(0)("referralcode")

            End If


            dt = _ClsDischarge.transactionInfoInsert(trxId, doctor, keluhan, diag1, diag2, isrujukan, providerRujukan, remarks, usridNew, referralCode)
            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To _rpt_item.Items.Count - 1
                    Dim _hf_subprod_id As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_subprod_id"), HiddenField)
                    Dim _hf_benefit_id As HiddenField = DirectCast(_rpt_item.Items(i).FindControl("_hf_benefit_id"), HiddenField)
                    Dim _lbl_eligible_amount As Label = DirectCast(_rpt_item.Items(i).FindControl("_lbl_eligible_amount"), Label)
                    Dim _tb_incurred_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_incurred_amount"), TextBox)
                    Dim _lbl_orig_accept_amount As Label = DirectCast(_rpt_item.Items(i).FindControl("_lbl_orig_accept_amount"), Label)
                    Dim _lbl_orig_excess_amount As Label = DirectCast(_rpt_item.Items(i).FindControl("_lbl_orig_excess_amount"), Label)
                    Dim _tb_accept_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_accept_amount"), TextBox)
                    Dim _tb_excess_amount As TextBox = DirectCast(_rpt_item.Items(i).FindControl("_tb_excess_amount"), TextBox)

                    Dim subProdId As String = _hf_subprod_id.Value
                    Dim benefitId As String = _hf_benefit_id.Value
                    Dim eligibleAmount As Decimal = CDec(_lbl_eligible_amount.Text)
                    Dim incurredAmount As Decimal = CDec(_tb_incurred_amount.Text)
                    Dim origAcceptAmount As Decimal = CDec(_lbl_orig_accept_amount.Text)
                    Dim origExcessAmount As Decimal = CDec(_lbl_orig_excess_amount.Text)
                    Dim acceptAmount As Decimal = CDec(_tb_accept_amount.Text)
                    Dim excessAmount As Decimal = CDec(_tb_excess_amount.Text)
                    Dim remark As String = ""

                    If incurredAmount <> 0 Then
                        dt = _ClsDischarge.transactionBenefitInsert(trxId, subProdId, benefitId, eligibleAmount, incurredAmount, origAcceptAmount, origExcessAmount, acceptAmount, excessAmount, remark, "A", usridNew)
                        'start insert log dsini
                    End If

                Next
                _btnSubmit.Enabled = True

                dt = _ClsDischarge.transactionHdrUpdateStatus(trxId, "2")

                Response.Redirect("~/Discharge.aspx?act=summary&trx=" + trxId, False)
            Else
                Response.Redirect("~/Discharge.aspx?act=error&trx=" + trxId, False)
            End If


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
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
        If dt.Rows.Count > 0 Then
            _rpt_item.DataSource = dt
            _rpt_item.DataBind()
        Else
            _rpt_item.DataSource = Nothing
            _rpt_item.DataBind()
        End If


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

End Class