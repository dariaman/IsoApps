Imports System.Data.SqlClient

Public Class MasterProvider
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
        Else
            Try
                Dim totalrow As Integer = 0
                provider_gv.DataSource = GetDataPaged()
                provider_gv.DataBind()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End If
    End Sub

    Public Function GetDataPaged() As DataTable
        Dim con As New SqlConnection(config.MSSQLConnection)
        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM dbo.MSPROVIDERMASTER"

        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            con.Close()
        End Try

        Return dt
    End Function

    Private Sub provider_gv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles provider_gv.PageIndexChanging
        Dim halaman As Integer = e.NewPageIndex + 1
        provider_gv.PageIndex = e.NewPageIndex
        Dim totalrow As Integer = 0
        Try
            provider_gv.DataSource = GetDataPaged()
            provider_gv.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub provider_gv_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles provider_gv.RowCommand

        Dim providerid As String = e.CommandArgument
        If e.CommandName = "UpdateData" Then

            Response.Redirect("TambahProvider.aspx?providerid=" + providerid)
        ElseIf e.CommandName = "UpdateStatus" Then

            Dim con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            Dim dt As New DataTable

            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "UPDATE MSPROVIDERMASTER SET ISACTIVE=CASE WHEN ISACTIVE=1 THEN 0 ELSE 1 END WHERE PROVIDERID=@PROVIDERID"
            cmd.Parameters.Clear()
            cmd.Parameters.Add("@PROVIDERID", SqlDbType.Int).Value = providerid

            Try
                con.Open()
                cmd.ExecuteNonQuery()
                provider_gv.DataSource = GetDataPaged()
                provider_gv.DataBind()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try

        End If
    End Sub

    Public Function CheckNullBoolean(myval As Object) As String
        If (IsDBNull(myval)) Then
            Return "False"
        Else
            myval.ToString()
        End If
    End Function

    Private Sub btn_add_Provider_Click(sender As Object, e As EventArgs) Handles btn_add_Provider.Click
        Response.Redirect("TambahProvider.aspx")
    End Sub

    Private Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Dim con As New SqlConnection(config.MSSQLConnection)
        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        Dim kolom_search As String = "PROVIDERID"
        If ddlSearch.SelectedValue = 1 Then
            kolom_search = "PROVIDERID"
        ElseIf ddlSearch.SelectedValue = 2 Then
            kolom_search = "PROVIDERNAME"
        ElseIf ddlSearch.SelectedValue = 3 Then
            kolom_search = "EMAIL"
        End If

        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        If txtnamaprovider.Text = "" Then
            cmd.CommandText = "SELECT * FROM dbo.MSPROVIDERMASTER "
        Else
            cmd.CommandText = "SELECT * FROM dbo.MSPROVIDERMASTER Where " + kolom_search + " like @SearchText"
        End If
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = txtnamaprovider.Text

        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            provider_gv.DataSource = dt
            provider_gv.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            con.Close()
        End Try

    End Sub
End Class