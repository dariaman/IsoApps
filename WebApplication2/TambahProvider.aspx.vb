Imports System.Data.SqlClient

Public Class TambahProvider
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
            If Not String.IsNullOrWhiteSpace(Request.QueryString("providerid")) Then

                Dim con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                Dim rd As SqlDataReader
                Dim provid As Integer


                If (IsDBNull(Request.QueryString("providerid"))) Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Provider tidak valid !');</script>")
                    _btnsimpan.Enabled = False
                ElseIf Not Integer.TryParse(Convert.ToString(Request.QueryString("providerid").ToString()), provid) Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Provider tidak valid !');</script>")
                    _btnsimpan.Enabled = False
                Else
                    _btnsimpan.Enabled = True
                    providerid.Value = provid.ToString

                    cmd.Connection = con
                    con.Open()
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = "SELECT * FROM MSPROVIDERMASTER WHERE PROVIDERID=@PROVIDERID;"
                    cmd.Parameters.Clear()
                    cmd.Parameters.Add("@PROVIDERID", SqlDbType.Int).Value = provid
                    rd = cmd.ExecuteReader
                    While rd.Read
                        txtnama.Text = rd(0).ToString
                        txtnama.ReadOnly = True

                        txtnama.Text = rd("PROVIDERNAME").ToString
                        txtAlamat1.Text = rd("BUILDING").ToString
                        txtAlamat2.Text = rd("STREET1").ToString
                        txtAlamat3.Text = rd("STREET2").ToString
                        txtZipcode.Text = rd("ZIPCODE").ToString
                        txtphone.Text = rd("PHONE1").ToString
                        txtphone2.Text = rd("PHONE2").ToString
                        txtfax.Text = rd("FAX1").ToString
                        txtfax2.Text = rd("FAX2").ToString
                        txtemail.Text = rd("EMAIL").ToString

                    End While

                End If
            End If
        End If
    End Sub

    Protected Sub _btnsimpan_Click(sender As Object, e As EventArgs) Handles _btnsimpan.Click
        Dim con As New SqlConnection(config.MSSQLConnection)
        Dim cmd As New SqlCommand

        cmd.Connection = con
        cmd.CommandType = CommandType.Text

        If providerid.Value = "" Then
            cmd.CommandText = "INSERT INTO dbo.MSPROVIDERMASTER(PROVIDERNAME,BUILDING,STREET1,STREET2,ZIPCODE,PHONE1,PHONE2,FAX1,FAX2,EMAIL,ISACTIVE,CREATEDT,CREATEBY) " & _
                            "VALUES(@PROVIDERNAME,@BUILDING,@STREET1,@STREET2,@ZIPCODE,@PHONE1,@PHONE2,@FAX1,@FAX2,@EMAIL,1,getdate(),@CREATEBY);"
        Else
            cmd.CommandText = "UPDATE MSPROVIDERMASTER" & _
                                    "SET PROVIDERNAME=@PROVIDERNAME,BUILDING=@BUILDING,STREET1=@STREET1,STREET2=@STREET2,ZIPCODE=@ZIPCODE,PHONE=@PHONE,PHONE2=@PHONE2" & _
                                    ",FAX1=@FAX1,FAX2=@FAX2,EMAIL=@EMAIL WHERE PROVIDERID=@PROVIDERID;"
        End If
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@PROVIDERNAME", SqlDbType.VarChar).Value = txtnama.Text
        cmd.Parameters.Add("@BUILDING", SqlDbType.VarChar).Value = txtAlamat1.Text
        cmd.Parameters.Add("@STREET1", SqlDbType.VarChar).Value = txtAlamat2.Text
        cmd.Parameters.Add("@STREET2", SqlDbType.VarChar).Value = txtAlamat3.Text
        cmd.Parameters.Add("@ZIPCODE", SqlDbType.VarChar).Value = txtZipcode.Text
        cmd.Parameters.Add("@PHONE1", SqlDbType.VarChar).Value = txtphone.Text
        cmd.Parameters.Add("@PHONE2", SqlDbType.VarChar).Value = txtphone2.Text
        cmd.Parameters.Add("@FAX1", SqlDbType.VarChar).Value = txtfax.Text
        cmd.Parameters.Add("@FAX2", SqlDbType.VarChar).Value = txtfax2.Text
        cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail.Text
        cmd.Parameters.Add("@CREATEBY", SqlDbType.VarChar).Value = UserLogin.UserName
        cmd.Parameters.Add("@PROVIDERID", SqlDbType.VarChar).Value = providerid.Value
        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return
        Finally
            con.Close()
        End Try
        Response.Redirect("MasterProvider.aspx")
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Response.Redirect("MasterProvider.aspx")
    End Sub
End Class