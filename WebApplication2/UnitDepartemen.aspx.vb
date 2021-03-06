﻿Imports SPGeneral
Public Class UnitDepartemen
    Inherits System.Web.UI.Page
    Dim _ClsUNITDEPARTEMEN As New WebService.ClassUnitDepartemen
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
                        Session("DashBoard") = "Unit Departemen List<i class='fa fa-bookmark fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
                        Dim msg As String = String.Format("{0} - UNITDEPARTEMEN - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=UNITDEPARTEMEN.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=UNITDEPARTEMEN.aspx", False)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try

            If validasi() = True Then
                _ClsUNITDEPARTEMEN.UNITDEPARTEMENCode = txtunitdepartemencode.Text
                _ClsUNITDEPARTEMEN.UNITDEPARTEMENtype = txtunitdepartementype.Text
                _ClsUNITDEPARTEMEN.Description = txtDESCRIPTION.Text
                _ClsUNITDEPARTEMEN.IsActive = chkAktiv.Checked
                _ClsUNITDEPARTEMEN.CRE_BY = UserLogin.UserId

                If _ClsUNITDEPARTEMEN.InsertUNITDEPARTEMEN() = True Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                    LinkSubmit.Enabled = False
                    bindData(txtKeyWord1.Text)
                End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - UNITDEPARTEMEN - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If txtunitdepartementype.Text = "" Or txtunitdepartemencode.Text = "" Or txtDESCRIPTION.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Sub doreset()
        ddlSearch.SelectedIndex = 0
        txtunitdepartemencode.Text = ""
        txtunitdepartementype.Text = ""
        txtDESCRIPTION.Text = ""
        chkAktiv.Checked = True
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try

            System.Threading.Thread.Sleep(500)
            bindData(txtKeyWord1.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - UNITDEPARTEMEN - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub bindData(UNITDEPARTEMEN As String)
        Try
            gridMenu.DataSource = _ClsUNITDEPARTEMEN.bindData(UNITDEPARTEMEN, ddlSearch.SelectedValue)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindisiData(ID As String)
        Try
            Dim dt As DataTable = _ClsUNITDEPARTEMEN.bindisiData(ID)
            If dt.Rows.Count > 0 Then
                PnlMain.Visible = False
                pnlPopup.Visible = True
                txtunitdepartemencode.Text = dt.Rows(0)(0).ToString
                txtunitdepartementype.Text = dt.Rows(0)(1).ToString
                txtDESCRIPTION.Text = dt.Rows(0)(2).ToString
                chkAktiv.Checked = dt.Rows(0)(3).ToString
            Else
                doreset()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gridMenu_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles gridMenu.PageIndexChanged
        Try
            gridMenu.PageIndex = e.NewPageIndex
            bindData(txtKeyWord1.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - UNITDEPARTEMEN - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        doreset()
        PnlMain.Visible = False
        pnlPopup.Visible = True
        txtunitdepartemencode.ReadOnly = False
    End Sub

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        PnlMain.Visible = True
        pnlPopup.Visible = False
        LinkSubmit.Enabled = True
    End Sub

    Private Sub DGuser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        Try
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
                Dim KEY As String = e.CommandArgument
                bindisiData(KEY)
                txtunitdepartemencode.ReadOnly = True
            End If
             If e.CommandName = "UpdateLink" Then
                System.Threading.Thread.Sleep(500)
                Dim KEY As String = e.CommandArgument

                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value
                '_ClsInventarisItem.NO = KEY
                '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
                '_ClsInventarisItem.updateStatus()
                _sama.UpdateActive("MSUnitDepartemen", "ISACTIVE", IIf(sts = "True", "False", "True"), "UnitDepartemenCode", KEY)
                bindData(txtKeyWord1.Text)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - UNITDEPARTEMEN - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub gridMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMenu.SelectedIndexChanged

    End Sub
End Class