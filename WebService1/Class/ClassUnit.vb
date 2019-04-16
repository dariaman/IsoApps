Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClassUnit

#Region "property"

    Private _UnitCode As String
    Private _Description As String
    Private _Unittype As String
    Private _IsActive As String
    Private _CRE_BY As String
    Private _CRE_IP As String

    Public Property UnitCode() As String
        Get
            Return _UnitCode
        End Get
        Set(ByVal value As String)
            _UnitCode = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Public Property Unittype() As String
        Get
            Return _Unittype
        End Get
        Set(ByVal value As String)
            _Unittype = value
        End Set
    End Property

    Public Property CRE_BY() As String
        Get
            Return _CRE_BY
        End Get
        Set(ByVal value As String)
            _CRE_BY = value
        End Set
    End Property

    Public Property CRE_IP() As String
        Get
            Return _CRE_IP
        End Get
        Set(ByVal value As String)
            _CRE_IP = value
        End Set
    End Property

    Public Property IsActive() As String
        Get
            Return _IsActive
        End Get
        Set(ByVal value As String)
            _IsActive = value
        End Set
    End Property

#End Region

    Public Function InsertUnit() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_I_U_MSUnit"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@UnitCode", SqlDbType.VarChar).Value = UnitCode
                com.Parameters.Add("@Description ", SqlDbType.VarChar).Value = Description
                com.Parameters.Add("@Unittype", SqlDbType.VarChar).Value = Unittype
                com.Parameters.Add("@CRE_BY ", SqlDbType.VarChar).Value = CRE_BY
                com.Parameters.Add("@IsActive ", SqlDbType.VarChar).Value = IsActive
                Dim rowSuccess As Integer = com.ExecuteNonQuery
                If rowSuccess = 0 Then
                    Return False
                    Throw New Exception("Error -" & sql)
                Else
                    Return True
                End If
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function
    Public Function bindData(Unit As String, type As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String = ""
                If Type = "Kode" Then
                    txtsql = " SELECT [UnitCode],[Unittype],[Description],IsActive FROM [dbo].[MSUnit] with (nolock) where [UnitCode] like '" & Unit & "%'  ORDER BY [UnitCode]"
                Else
                    txtsql = " SELECT [UnitCode],[Unittype],[Description],IsActive FROM [dbo].[MSUnit] with (nolock) where [UnitType] like '" & Unit & "%'  ORDER BY [UnitType]"
                End If
                cmd.CommandText = txtsql
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Return dt
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function bindisiData(ID As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "SELECT [UnitCode],[Unittype],[Description],IsActive FROM [dbo].[MSUnit] with (nolock) where [UnitCode]= '" & ID & "'  ORDER BY UnitCode"
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Return dt
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class
