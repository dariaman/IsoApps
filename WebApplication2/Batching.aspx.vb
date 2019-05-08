Imports System.Data.SqlClient

Public Class Batching
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
            Try
                Dim totalrow As Integer = 0
                batching_gv.DataSource = GetDataPaged(1, "", totalrow)
                'batching_gv.VirtualItemCount = totalrow
                batching_gv.DataBind()
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
        cmd.CommandText = "SP_OUTSTANDING_CLAIM_BATCH_LIST"
        'cmd.Parameters.Clear()
        'cmd.Parameters.Add("@Page_no", SqlDbType.Int)
        'cmd.Parameters("@Page_no").Value = Page_no
        'cmd.Parameters.Add("@filter", SqlDbType.VarChar)
        'cmd.Parameters("@filter").Value = ""
        'cmd.Parameters.Add("@totalrow", SqlDbType.Int)
        'cmd.Parameters("@totalrow").Direction = ParameterDirection.Output

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

    Protected Sub btn_save_batch_Click(sender As Object, e As EventArgs) Handles btn_save_batch.Click
        Dim coltransid As String = ""

        For Each item As GridViewRow In batching_gv.Rows
            Dim selected As Boolean = DirectCast(item.FindControl("chkSelected"), CheckBox).Checked
            Dim transid As String = DirectCast(item.FindControl("TRANSACTIONID"), HiddenField).Value.ToString
            Dim Provider_mapping As String = DirectCast(item.FindControl("PROVIDERID_PARTNER"), HiddenField).Value.ToString

            If selected Then
                coltransid += "'" + transid + "',"
                If Provider_mapping = "" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Provider masih belum di Mapping. Silahkan di Mapping terlebih dahulu !');</script>")
                    coltransid = ""
                    Exit For
                End If
            End If
        Next
        If coltransid <> "" Then
            coltransid = coltransid.TrimEnd(",")

            Try
                Submit_Batch(coltransid)
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Proses batching berhasil ');</script>")
                batching_gv.DataSource = GetDataPaged(1, "", 0)
                batching_gv.DataBind()
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('" + ex.Message + "');</script>")
            End Try
            'Response.Redirect("Batching.aspx")
        End If
    End Sub

    Public Sub Submit_Batch(CollTransID As String)
        Dim con As New SqlConnection(config.RELIsomedikConnection)
        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "Proses_batching"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@user_batch", SqlDbType.VarChar)
        cmd.Parameters("@user_batch").Value = UserLogin.UserName
        cmd.Parameters.Add("@transid", SqlDbType.VarChar)
        cmd.Parameters("@transid").Value = CollTransID
        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Catch ex As SqlException
            Throw New Exception(ex.Message)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    'Private Sub CheckState()
    '    For Each row As GridViewRow In batching_gv.Rows
    '        Dim tombol_add As CheckBox = TryCast(row.FindControl("chkSelected"), CheckBox)
    '        'DirectCast(e.Row.FindControl("chkSelected"), CheckBox)
    '    Next

    '    'For Each GridViewRow As rw In ds
    '    'Next
    'End Sub

    'private void CheckState(bool p)
    '{
    '    foreach (GridViewRow row in GridView1.Rows)
    '   {
    '       CheckBox chkcheck = (CheckBox)row.FindControl("chkid");
    '             chkcheck.Checked = p;
    '    }
    '}
End Class