Imports System.Data.SqlClient

Public Class ProviderMapping
    Inherits System.Web.UI.Page

    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    'Dim page_no As Integer
    'Dim max_row As Integer = 10
    'Dim search_text As String
    'Dim row_count As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
        Else
            Try
                Dim totalrow As Integer = 0
                provider_mapping_gv.DataSource = GetDataPaged(1, "", totalrow)
                'provider_mapping_gv.VirtualItemCount = totalrow
                provider_mapping_gv.DataBind()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            
        End If
    End Sub

    Public Function GetDataPaged(Page_no As Integer, filter As String, ByRef totaldata As Integer) As DataTable
        Dim con As New SqlConnection(config.MSSQLConnection)
        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SP_PROVIDER_MAPPING_LIST"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@Page_no", SqlDbType.Int)
        cmd.Parameters("@Page_no").Value = Page_no
        cmd.Parameters.Add("@filter", SqlDbType.VarChar)
        cmd.Parameters("@filter").Value = ""
        cmd.Parameters.Add("@totalrow", SqlDbType.Int)
        cmd.Parameters("@totalrow").Direction = ParameterDirection.Output

        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            'totaldata = Convert.ToInt32(cmd.Parameters.Item("@totalrow").Value)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            con.Close()
        End Try

        Return dt
    End Function

    'Private Sub provider_mapping_gv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles provider_mapping_gv.PageIndexChanging
    '    Dim halaman As Integer = e.NewPageIndex + 1
    '    provider_mapping_gv.PageIndex = e.NewPageIndex
    '    Dim totalrow As Integer = 0
    '    Try
    '        provider_mapping_gv.DataSource = GetDataPaged(halaman, "", totalrow)
    '        provider_mapping_gv.DataBind()
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btn_add_mapping_Click(sender As Object, e As EventArgs) Handles btn_add_mapping.Click
    '    Response.Redirect("AddMappingProvider.aspx")
    'End Sub

    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        If txtnamaprovider.Text = "" Then
            Try
                Dim totalrow As Integer = 0
                provider_mapping_gv.DataSource = GetDataPaged(1, "", totalrow)
                'provider_mapping_gv.VirtualItemCount = totalrow
                provider_mapping_gv.DataBind()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Else

            Dim con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            Dim dt As New DataTable

            Dim kolomCari As String

            If ddljenisProvider.SelectedValue = 1 Then
                kolomCari = "pm.PROVIDERNAME"
            Else
                kolomCari = "pp.PROVIDERNAME"
            End If

            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT pm.PROVIDERID PROVIDERID_ISOMEDIK,pm.PROVIDERNAME PROVIDER_ISOMEDIK_NAME,COALESCE(CAST(pp.PROVIDERID AS VARCHAR(10)),'-') PROVIDERID_PARTNER," & _
                                "COALESCE(pp.PROVIDERNAME,'-') PROVIDER_PARTNER_NAME FROM dbo.MSPROVIDERMASTER pm " & _
                                " LEFT JOIN dbo.PROVIDER_MAPPING pmp ON pmp.PROVIDERID_ISOMEDIK=pm.PROVIDERID AND pmp.ISACTIVE=1" & _
                                " LEFT JOIN [192.168.30.225].rel.dbo.Prv_Provider_Master pp ON pp.PROVIDERID=pmp.PROVIDERID_PARTNER" & _
                                " WHERE " + kolomCari + " LIKE '" + txtnamaprovider.Text + "'"

            Try
                con.Open()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                provider_mapping_gv.DataSource = dt
                provider_mapping_gv.DataBind()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End If
    End Sub

    Private Sub provider_mapping_gv_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles provider_mapping_gv.RowCommand
        Dim IdProvider As String = e.CommandArgument

        If e.CommandName.Equals("addmapping") Then
            Response.Redirect("~/AddMappingProvider.aspx?idprovider=" + IdProvider, False)
        End If
    End Sub

    Private Sub provider_mapping_gv_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles provider_mapping_gv.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim tombol_add As Button = DirectCast(e.Row.FindControl("_btn_add"), Button)

            If e.Row.Cells(4).Text = "-" Then
                tombol_add.Visible = True
            Else
                tombol_add.Visible = False
            End If
        End If
    End Sub
End Class