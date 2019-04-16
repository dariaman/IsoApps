Imports System.Data.Sql
Imports System.Data.SqlClient
Imports SPGeneral
Imports System.Globalization
Public Class PolBenefit
    Inherits System.Web.UI.Page
    Dim _ClsPolBenefit As New WebService.ClsPolBenefit
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
                        Try

                            System.Threading.Thread.Sleep(500)
                            Session("DashBoard") = "Policy Benefit <i class='fa fa-building fa-fw'></i>"
                            Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                            If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                                Response.Redirect("Home.aspx", False)
                            End If
                            ddlSearch.SelectedIndex = 0
                        Catch ex As Exception

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

    Protected Sub bindData(policyno As String)
        Try
            gridMenu.DataSource = _ClsPolBenefit.bindData(policyno)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GvDischarge_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("SelectProcess") Then

        End If
    End Sub


    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            bindData(_tb_search.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - TransactionList - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        System.Threading.Thread.Sleep(500)
        If ddlSearch.SelectedIndex = 0 Then
            ddlSearch.Focus()
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please select one ');</script>")
            Exit Sub
        End If
        If _tb_search.Text = "" Then
            _tb_search.Focus()
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Policy No dont have value');</script>")
            Exit Sub
        End If
        Dim dt As DataTable = _ClsPolBenefit.bindData(_tb_search.Text)
        If dt.Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Check again Policy No');</script>")
            Exit Sub
        End If
            _ClsPolBenefit.POLICYNO = _tb_search.Text
            _ClsPolBenefit.TYPE = ddlSearch.SelectedValue
            _ClsPolBenefit.CREATEBY = UserLogin.UserId
            If _ClsPolBenefit.DeletePolBenefit() Then

            End If
        For Each row As GridViewRow In gridMenu.Rows

            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CHCK"), CheckBox)
                If chkRow.Checked Then

                    _ClsPolBenefit.PLANID = row.Cells(2).Text
                    _ClsPolBenefit.SUBPRODID = row.Cells(3).Text
                    If _ClsPolBenefit.InsertPolBenefit() Then

                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub ddlSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearch.SelectedIndexChanged
        System.Threading.Thread.Sleep(500)
        bindData(_tb_search.Text)
    End Sub
End Class