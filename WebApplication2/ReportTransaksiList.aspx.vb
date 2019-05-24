Public Class ReportTransaksiList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim _provider As New WebService.ClsProvider
            _provider.DDLlistProvider(ddlProvider)
            _provider.DDLlistClient(ddlClient)
            _provider.DDLlistStatus(ddlStatus)

        End If
    End Sub

    Private Sub _btnsubmit_Click(sender As Object, e As EventArgs) Handles _btnsubmit.Click
        Dim dateFrom As String = txtFrom.Text
        Dim dateTo As String = txtTo.Text

        Dim provider As String = ddlProvider.SelectedValue
        Dim client As String = ddlClient.SelectedValue
        Dim status As String = ddlStatus.SelectedValue

        viewrpt("WebViewerData.aspx", "7", dateFrom, dateTo, provider, client, status, dateFrom, dateTo, "Daftar Transaksi")
    End Sub

    Sub viewrpt(strform As String, No As String, key1 As String, key2 As String, key3 As String, key4 As String, key5 As String, Param1 As String, Param2 As String, judul As String) ', key2 As String, key3 As String, key4 As String)
        Try
            Session("No") = No
            Session("key1") = key1
            Session("key2") = key2
            Session("key3") = key3
            Session("key4") = key4
            Session("key5") = key5
            Session("Param1") = Param1
            Session("Param2") = Param2
            Session("JudulXls") = judul
            Response.Redirect(strform, False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class