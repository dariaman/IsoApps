Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
'Imports Newtonsoft.Json
Imports System.Configuration
'Imports System.Web.Script.Services
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
 Public Class WebTest
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    '<WebMethod()> _
    'Public Sub AutocompleteDiagDesc(DiagDesc As String)
    '    Dim CS As String = ConfigurationManager.ConnectionStrings("getSoftware").ConnectionString

    '    Dim diagnosa As New List(Of WebService.ClsDiagnosa)()

    '    Using con As New SqlConnection(CS)
    '        Dim cmd As New SqlCommand("select top 20 DiagCode, DiagDesc from dbo.fc_tbl_diagnose('') where DiagDesc like '%" & DiagDesc & "%' ", con)
    '        cmd.CommandType = CommandType.Text
    '        'cmd.Parameters.AddWithValue("@UserId", UserId)
    '        con.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader()
    '        While dr.Read()
    '            Dim ClsDiagnosa As New WebService.ClsDiagnosa()
    '            ClsDiagnosa.DIAGCODE = dr("DiagCode").ToString()
    '            ClsDiagnosa.DIAGDESC = dr("DiagDesc").ToString()
    '            diagnosa.Add(ClsDiagnosa)
    '        End While
    '    End Using
    '    Dim JS As New JavaScriptSerializer()
    '    Context.Response.Write(JS.Serialize(diagnosa))
    '    'Return JsonConvert.SerializeObject(countries)
    'End Sub

    '<WebMethod()> _
    'Public Sub AutocompleteProvider(providerName As String)
    '    Dim CS As String = ConfigurationManager.ConnectionStrings("getSoftware").ConnectionString

    '    Dim PROVIDER As New List(Of WebService.ClsProvider)()

    '    Using con As New SqlConnection(CS)
    '        Dim cmd As New SqlCommand("Sp_S_PRV_PROVIDER_MASTER_ '%" & providerName & "%' ", con)
    '        cmd.CommandType = CommandType.Text
    '        'cmd.Parameters.AddWithValue("@UserId", UserId)
    '        con.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader()
    '        While dr.Read()
    '            Dim ClsProvider As New WebService.ClsProvider()
    '            ClsProvider.PROVIDERID = dr("PROVIDERID").ToString()
    '            ClsProvider.PROVIDERNAME = dr("PROVIDERNAME").ToString()
    '            PROVIDER.Add(ClsProvider)
    '        End While
    '    End Using
    '    Dim JS As New JavaScriptSerializer()
    '    Context.Response.Write(JS.Serialize(PROVIDER))
    '    'Return JsonConvert.SerializeObject(countries)
    'End Sub
End Class