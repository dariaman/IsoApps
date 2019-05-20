Imports System.Data.Sql
Imports System.Data.SqlClient
Imports SPGeneral
Imports System.Globalization
Public Class Discharge
    Inherits System.Web.UI.Page
    Dim _ClsDischarge As New WebService.ClsDischarge
    Dim _ClsMemberBenefit As New WebService.ClsMemberBenefit
    Dim _sama As New WebService.sama
    Dim totalIncurredAmount As Decimal = 0
    Dim totalAcceptAmount As Decimal = 0
    Dim totalExcessAmount As Decimal = 0
    Dim transaction_issaved As Boolean

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
    Public Shared Function AutocompleteDiagDescEx(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim CS As String = ConfigurationManager.ConnectionStrings("getSoftware").ConnectionString

        'Dim diagnosa As New List(Of WebService.ClsDiagnosa)()

        Using con As New SqlConnection(CS)
            Dim cmd As New SqlCommand("select top 20 DiagCode, DiagDesc from dbo.fc_tbl_diagnose('') where DiagDesc like  '%'+ @SearchText + '%' ", con)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@SearchText", prefixText)
            cmd.Connection = con
            con.Open()
            Dim diagnosas As List(Of String) = New List(Of String)
            Dim sdr As SqlDataReader = cmd.ExecuteReader
            While sdr.Read
                diagnosas.Add(Trim(Left(sdr("DiagCode").ToString + "          ", 11) + sdr("DiagDesc").ToString))
            End While
            con.Close()
            Return diagnosas
        End Using
    End Function

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
                        Dim trxid As String
                        Dim dt As DataTable
                        Dim isreferral As String
                        Dim providerId As String
                        Dim serviceType As String

                        providerId = UserLogin.providerid

                        Session("DashBoard") = "Discharge List <i class='fa fa-building fa-fw'></i>"
                        Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If

                        Try
                            If ((Request.QueryString("act").ToString() = "process")) Then
                                trxid = Request.QueryString("trx").ToString()
                                dt = _ClsDischarge.getTransaction(trxid, 1, providerId)
                                _gv_member_detail.DataSource = dt
                                _gv_member_detail.DataBind()

                                '_Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                Dim policyRemark As String = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                If policyRemark <> "" Then
                                    _pnl_policy_remark.Visible = True
                                    _LblTPAREMARKS.Text = policyRemark
                                End If


                                _mv1.ActiveViewIndex = 1
                                _tb_doctor.Focus()

                                bindCoverage(trxid)

                            ElseIf ((Request.QueryString("act").ToString() = "summary")) Then
                                trxid = Request.QueryString("trx").ToString()
                                dt = _ClsDischarge.getTransaction(trxid, 2, providerId)

                                '_Lbl_TPAREMARKS.Text = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                Dim policyRemark As String = _ClsDischarge.TPARemarks(dt.Rows(0)(12).ToString)
                                If policyRemark <> "" Then
                                    _pnl_policy_remark.Visible = True
                                    _LblTPAREMARKS.Text = policyRemark
                                End If

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
                                    serviceType = dt(0)("SERVICETYPE").ToString

                                    dt = _ClsDischarge.getTransaction(trxid, 3, providerId)
                                    If (dt.Rows.Count > 0) Then
                                        _gv_summary_detail_benefit.DataSource = dt
                                        _gv_summary_detail_benefit.DataBind()

                                        If (serviceType = "1") Then
                                            _gv_summary_detail_benefit.Columns(3).Visible = False
                                        End If
                                    End If

                                    If isreferral = "true" Then
                                        _btn_surat_rujukan.Visible = True
                                    Else
                                        _btn_surat_rujukan.Visible = False
                                    End If

                                End If

                                _mv1.ActiveViewIndex = 2
                            End If
                        Catch ex As Exception
                            _mv1.ActiveViewIndex = 0
                            bindData("1", _tb_search.Text.Trim(), 1)
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
            Response.Redirect("login.aspx?p=Diagnosa.aspx", False)
        End If
    End Sub

    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            System.Threading.Thread.Sleep(500)
            bindData("1", _tb_search.Text.Trim(), 1)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - ViewDischarge - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
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
        Dim trxId As String = e.CommandArgument

        If e.CommandName.Equals("SelectProcess") Then

            Response.Redirect("~/Discharge.aspx?act=process&trx=" + trxId, False)
            'bindMemberDetailBenefit(CardNo, SubProdId)
            '_lbl_popup_header.Text = "IB Benefit Detail ( " + SubProdId + " )"
            'txtBranchCode.ReadOnly = True
        ElseIf e.CommandName.Equals("SelectRegistrasi") Then
            viewrpt("WebViewer.aspx", trxId)
        ElseIf e.CommandName.Equals("SelectFIM") Then
            viewrptFima("WebViewer.aspx", trxId)
        ElseIf e.CommandName.Equals("SelectSP") Then
            viewrptSuratPernyataan("WebViewer.aspx", trxId)
        ElseIf e.CommandName.Equals("SelectFKC") Then
            viewrptFKC("WebViewer.aspx", trxId)
        End If
    End Sub

    Sub viewrpt(strform As String, trxId As String) ', key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            Dim dt As DataTable            

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

    Sub viewrptFKC(strform As String, trxId As String) ', key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            Dim dt As DataTable            

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

    Sub viewrptSuratPernyataan(strform As String, trxId As String) ', key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            Dim dt As DataTable            

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

    Sub viewrptFima(strform As String, trxId As String) ', key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            Dim dt As DataTable            

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

    Protected Sub GvDischarge_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then            
            Dim _btn_registrasi As Button = TryCast(e.Row.FindControl("_btn_registrasi"), Button)
            'Dim _btn_fim As Button = TryCast(e.Row.FindControl("_btn_fim"), Button)
            'Dim _btn_sp As Button = TryCast(e.Row.FindControl("_btn_sp"), Button)
            'Dim _btn_fkc As Button = TryCast(e.Row.FindControl("_btn_fkc"), Button)
            Dim _pnl_fim As Panel = TryCast(e.Row.FindControl("_pnl_fim"), Panel)
            Dim _pnl_sp As Panel = TryCast(e.Row.FindControl("_pnl_sp"), Panel)
            Dim _pnl_fkc As Panel = TryCast(e.Row.FindControl("_pnl_fkc"), Panel)
            If e.Row.Cells(7).Text = "RAWAT INAP" Or e.Row.Cells(7).Text = "INDEMNITY BASIC" Then
                _btn_registrasi.Visible = True
                '_btn_fim.Visible = True
                '_btn_sp.Visible = True
                '_btn_fkc.Visible = False
                _pnl_fim.Visible = True
                _pnl_sp.Visible = True
                _pnl_fkc.Visible = False
            Else
                _btn_registrasi.Visible = True
                '_btn_fim.Visible = False
                '_btn_sp.Visible = False
                '_btn_fkc.Visible = True
                _pnl_fim.Visible = False
                _pnl_sp.Visible = False
                _pnl_fkc.Visible = True
            End If

        End If
    End Sub

    Private Sub gvDischarge_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles _gv_discharge.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            _gv_discharge.PageIndex = e.NewPageIndex
            bindData("1", _tb_search.Text.Trim(), 1)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub


    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles _btnSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)

            'set variable transaction_issaved untuk mencegah proses tombol back dan disave lagi
            'If (transaction_issaved = False) Then
            '    transaction_issaved = True
            'End If

            '_btnSubmit.Enabled = False
            If _ClsDischarge.DiagDescCd(DiagName.Text) = False Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('cant find Diagnose 1');</script>")
                Exit Sub
            End If
            If DiagName1.Text.Trim <> "" Then
                If _ClsDischarge.DiagDescCd(DiagName1.Text) = False Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('cant find Diagnose 2');</script>")
                    Exit Sub
                End If
            End If
            If DiagName2.Text.Trim <> "" Then
                If _ClsDischarge.DiagDescCd(DiagName2.Text) = False Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('cant find Diagnose 3');</script>")
                    Exit Sub
                End If
            End If
            If _cb_isrujukan.Checked = True Then
                If providerName.Text.Trim = "" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Provider belum diisi');</script>")
                    Exit Sub
                Else
                    If _ClsDischarge.PROVIDERCd(providerName.Text) = False Then
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Nama Provider tidak ditemukan');</script>")
                        Exit Sub
                    End If
                End If
            End If
            If _tb_doctor.Text = "" Or _tb_keluhan.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please, fill docter and Symptoms');</script>")
                _btnSubmit.Attributes.Add("onclick", "Javascript:this.disable=true;")
                Exit Sub
            Else
                _btnSubmit.Attributes.Add("onclick", "Javascript:this.disable=fasle;")
            End If
            Dim usridNew As String = Session("UserId")
            Dim dt As DataTable
            Dim trxId As String, doctor As String, keluhan As String, diag1 As String, diag2 As String, diag3 As String, isrujukan As String, remarks As String, providerRujukan As String, referralCode As String, REFERRALREMARK As String, tindakan As String

            trxId = Request.QueryString("trx").ToString()
            doctor = _tb_doctor.Text.Trim
            keluhan = _tb_keluhan.Text.Trim
            diag1 = Left(DiagName.Text, 10).Trim
            diag2 = Left(DiagName1.Text, 10).Trim
            diag3 = Left(DiagName2.Text, 10).Trim
            isrujukan = "0"
            remarks = _tb_Remark.Text
            referralCode = ""
            providerRujukan = "0"
            REFERRALREMARK = _tb_ReferralRemark.Text
            tindakan = _tb_tindakan.Text.Trim

            If (_cb_isrujukan.Checked) Then
                isrujukan = "1"
                providerRujukan = Left(providerName.Text, 10).Trim

                dt = _ClsDischarge.getTransactionReferralCode()
                referralCode = dt(0)("referralcode")

            End If


            dt = _ClsDischarge.transactionInfoInsert(trxId, doctor, keluhan, diag1, diag2, diag3, isrujukan, providerRujukan, remarks, usridNew, referralCode, REFERRALREMARK, tindakan)
            If (dt.Rows.Count > 0) Then
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
                        dt = _ClsDischarge.transactionBenefitInsert(trxId, subProdId, benefitId, lenghtofstay, eligibleAmount, incurredAmount, origAcceptAmount, origExcessAmount, acceptAmount, excessAmount, remark, "A", usridNew)
                        'start insert log dsini
                    End If

                Next
                '_btnSubmit.Enabled = True
                If _cb_isrujukan.Checked = True Then
                    _btn_surat_rujukan.Visible = True
                Else
                    _btn_surat_rujukan.Visible = False
                End If

                If _ClsDischarge.transactionHdrSubmit(trxId, "2", usridNew) = True Then
                    'start insert benefitlog
                    _ClsDischarge.transactionBenefitLogInsert(trxId, "ADD", usridNew)
                    'end insert benefitlog

                    

                    Response.Redirect("~/Discharge.aspx?act=summary&trx=" + trxId, False)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Not Updated');</script>")
                End If


            Else
                Response.Redirect("~/Discharge.aspx?act=error&trx=" + trxId, False)
            End If


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
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
            Dim incurredamt As Decimal
            Dim totalincurredamt As Decimal
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
                incurredamt = CDec(_tb_incurred_amount.Text)
                totalincurredamt += incurredamt

                If (incurredamt > 0) Then
                    dt = _ClsDischarge.tmpTransactionBenefitCalculateInsert(trxId, _hf_subprod_id.Value, _hf_benefit_id.Value, _hf_sublimit.Value, _hf_subgroup.Value, _hf_limitamtid.Value, _tb_lenght_of_stay.Text, _hf_benlimamt.Value, eligibleamt, _tb_incurred_amount.Text, _hf_cardno.Value, _hf_policyno.Value, _tb_remarks.Text.Trim())
                End If

            Next

            'jika total incurred amount = nol, user belum input incurred amount sama sekali
            If (totalincurredamt = 0) Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Jumlah Tagihan tidak boleh kosong.');</script>")
                Exit Sub
            End If

            'start proses calculate
            dt = _ClsDischarge.tmpTransactionBenefitCalculateProcess(trxId)
            'end proses calculate

            'start bind ulang detail
            dt = _ClsDischarge.bindMemberBenefitDetail(trxId, subProdId)
            _rpt_item.DataSource = dt
            _rpt_item.DataBind()
            'end bind ulang detail

            setEnableAfterCalculate()

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

    Protected Sub setEnableAfterCalculate()
        _btnSubmit.Visible = True
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
        _btnSubmit.Visible = False
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
        System.Threading.Thread.Sleep(500)
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

        If (limit_decimal = 999999999) Then 'inner limit ascharge
            If (class_limit_decimal <> 999999999) Then 'annual limit'
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
        Else 'sesuai dengan inner limit
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

    Protected Sub _gv_summary_detail_benefit_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        System.Threading.Thread.Sleep(500)
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

    Private Sub _rpt_item_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles _rpt_item.ItemDataBound
        System.Threading.Thread.Sleep(500)
        Dim _hf_cardno As HiddenField = DirectCast(_gv_member_detail.Rows(0).FindControl("_hf_cardno"), HiddenField)
        Dim _hf_policyno As HiddenField = DirectCast(_gv_member_detail.Rows(0).FindControl("_hf_policyno"), HiddenField)
        Dim _hf_service_type As HiddenField = DirectCast(_gv_member_detail.Rows(0).FindControl("_hf_service_type"), HiddenField)

        Dim _lbl_eligible_amount As Label = DirectCast(e.Item.FindControl("_lbl_eligible_amount"), Label)
        Dim _hf_subprod_id As HiddenField = DirectCast(e.Item.FindControl("_hf_subprod_id"), HiddenField)
        Dim _hf_benefit_id As HiddenField = DirectCast(e.Item.FindControl("_hf_benefit_id"), HiddenField)
        Dim _hf_sublimit As HiddenField = DirectCast(e.Item.FindControl("_hf_sublimit"), HiddenField)
        Dim _hf_subgroup As HiddenField = DirectCast(e.Item.FindControl("_hf_subgroup"), HiddenField)
        Dim _hf_limitamtid As HiddenField = DirectCast(e.Item.FindControl("_hf_limitamtid"), HiddenField)
        Dim _hf_benlimamt As HiddenField = DirectCast(e.Item.FindControl("_hf_benlimamt"), HiddenField)

        Dim _hf_load_from_tmp As HiddenField = DirectCast(e.Item.FindControl("_hf_load_from_tmp"), HiddenField)

        Dim _tb_lenght_of_stay As TextBox = DirectCast(e.Item.FindControl("_tb_lenght_of_stay"), TextBox)

        Dim role_code As String = Session("RoleCode")

        If (_hf_limitamtid.Value = 3) Then
            _tb_lenght_of_stay.Enabled = True
        Else
            _tb_lenght_of_stay.Enabled = False
        End If

        Dim eligibleAmtDisplay As Decimal = CDec(_lbl_eligible_amount.Text)

        If _hf_load_from_tmp.Value = "" Then
            'kolom remaining benefit tidak dimunculkan jika tipe layanan=rawat jalan /RJTP
            If (_hf_service_type.Value = "1") Then
                _th_remaining_benefit.Visible = False
                DirectCast(e.Item.FindControl("_td_remaining_benefit"), HtmlTableCell).Visible = False
            Else
                _lbl_eligible_amount.Text = GetRemainingBenefit_2(_hf_cardno.Value, _hf_policyno.Value, _hf_sublimit.Value, _hf_subgroup.Value, _hf_limitamtid.Value, _hf_subprod_id.Value, _hf_benefit_id.Value, _hf_benlimamt.Value)
            End If
        Else
            If (_hf_service_type.Value = "1") Then
                _th_remaining_benefit.Visible = False
                DirectCast(e.Item.FindControl("_td_remaining_benefit"), HtmlTableCell).Visible = False            
            End If            
        End If

        'price = String.Format(CultureInfo, "{0:N}", (limit_decimal - benefit_used_decimal))
        If (_hf_service_type.Value = "1") Then
            _th_remaining_benefit.Visible = False
            DirectCast(e.Item.FindControl("_td_remaining_benefit"), HtmlTableCell).Visible = False
        End If


    End Sub

    Protected Sub _cb_isrujukan_CheckedChanged(sender As Object, e As EventArgs) Handles _cb_isrujukan.CheckedChanged
        'If _cb_isrujukan.Checked = True Then
        System.Threading.Thread.Sleep(500)
        providerName.ReadOnly = Not (_cb_isrujukan.Checked)
        _tb_ReferralRemark.ReadOnly = Not (_cb_isrujukan.Checked)
        If (_cb_isrujukan.Checked) Then
            providerName.Focus()
        End If
        'Else

        'End If
    End Sub

    Protected Sub _btn_slip_Click(sender As Object, e As EventArgs) Handles _btn_slip.Click
        System.Threading.Thread.Sleep(500)
        viewrpt("WebViewer.aspx", "2", _lbl_s_trxid.Text, "SLIP")
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

    Protected Sub _btn_surat_rujukan_Click(sender As Object, e As EventArgs) Handles _btn_surat_rujukan.Click
        System.Threading.Thread.Sleep(500)
        viewrpt("WebViewer.aspx", 5, _lbl_s_trxid.Text, "SURAT RUJUKAN")
    End Sub

    Private Sub _gv_discharge_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles _gv_discharge.RowDataBound

    End Sub
End Class