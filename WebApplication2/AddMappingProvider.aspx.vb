Imports System.Data.SqlClient

Public Class AddMappingProvider
    Inherits System.Web.UI.Page
    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
        Else
            bindProviderInternal()
            bindProviderEksternal()
        End If
    End Sub

    Private Sub bindProviderInternal()
        Dim con As New SqlConnection(config.MSSQLConnection)
        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT PROVIDERID,PROVIDERNAME FROM dbo.MSPROVIDERMASTER mp " & _
                            "LEFT JOIN dbo.PROVIDER_MAPPING pmp ON pmp.PROVIDERID_ISOMEDIK=mp.PROVIDERID AND pmp.ISACTIVE=1 " & _
                            "WHERE pmp.ID Is NULL " & _
                            "ORDER BY mp.PROVIDERNAME"

        Try
            con.Open()
            Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim ds As DataSet = New DataSet
            sda.Fill(ds)
            ddlinternal.DataSource = ds
            ddlinternal.DataTextField = "PROVIDERNAME"
            ddlinternal.DataValueField = "PROVIDERID"
            ddlinternal.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub bindProviderEksternal()
        Dim con As New SqlConnection(config.MSSQLConnection)
        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT PROVIDERID,PROVIDERNAME FROM rel.dbo.PRV_PROVIDER_MASTER mp " & _
                            "LEFT JOIN dbo.PROVIDER_MAPPING pmp ON pmp.PROVIDERID_ISOMEDIK=mp.PROVIDERID AND pmp.ISACTIVE=1 " & _
                            "WHERE pmp.ID Is NULL " & _
                            "ORDER BY mp.PROVIDERNAME"
        Try
            con.Open()
            Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim ds As DataSet = New DataSet
            sda.Fill(ds)
            ddleksternal.DataSource = ds
            ddleksternal.DataTextField = "PROVIDERNAME"
            ddleksternal.DataValueField = "PROVIDERID"
            ddleksternal.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub _btnsimpan_Click(sender As Object, e As EventArgs) Handles _btnsimpan.Click
        Dim con As New SqlConnection(config.MSSQLConnection)
        Dim cmd As New SqlCommand

        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "INSERT INTO dbo.PROVIDER_MAPPING(PROVIDERID_ISOMEDIK,PROVIDER_ISOMEDIK_NAME,PROVIDERID_PARTNER,PROVIDER_PARTNER_NAME,PARTNERID,ISACTIVE,CREATEBY,CREATEDT) " & _
                            "SELECT @providerID_internal,@providerName_internal,@providerID_eksternal,@providerName_eksternal,1,1,@user_crt,getdate()"
        cmd.Parameters.Add("@providerID_internal", SqlDbType.Int).Value = ddlinternal.SelectedValue
        cmd.Parameters.Add("@providerName_internal", SqlDbType.VarChar).Value = ddlinternal.SelectedItem.Text
        cmd.Parameters.Add("@providerID_eksternal", SqlDbType.Int).Value = ddleksternal.SelectedValue
        cmd.Parameters.Add("@providerName_eksternal", SqlDbType.VarChar).Value = ddleksternal.SelectedItem.Text
        cmd.Parameters.Add("@user_crt", SqlDbType.VarChar).Value = UserLogin.UserName
        Try
            con.Open()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return
        Finally
            con.Close()
        End Try

        Response.Redirect("ProviderMapping.aspx")
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Response.Redirect("ProviderMapping.aspx")
    End Sub
End Class