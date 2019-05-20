Public Class PolBenefitList
    Inherits System.Web.UI.Page
    Dim _ClsPolBenefit As New WebService.ClsPolBenefit

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        bindData(search_polis_txt.Text, search_membid_txt.Text)

    End Sub

    Protected Sub bindData(policyno As String, Optional membid As String = "")
        Try
            gridcob.DataSource = _ClsPolBenefit.PolBenefitMapping(policyno, membid)
            gridcob.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class