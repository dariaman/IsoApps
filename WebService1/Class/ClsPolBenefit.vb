Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class ClsPolBenefit

#Region "property"

    Private _POLICYNO As String
    Private _PLANID As String
    Private _SUBPRODID As String
    Private _CREATEBY As String
    Private _TYPE As String

    Public Property POLICYNO() As String
        Get
            Return _POLICYNO
        End Get
        Set(ByVal value As String)
            _POLICYNO = value
        End Set
    End Property

    Public Property PLANID() As String
        Get
            Return _PLANID
        End Get
        Set(ByVal value As String)
            _PLANID = value
        End Set
    End Property

    Public Property SUBPRODID() As String
        Get
            Return _SUBPRODID
        End Get
        Set(ByVal value As String)
            _SUBPRODID = value
        End Set
    End Property

    Public Property CREATEBY() As String
        Get
            Return _CREATEBY
        End Get
        Set(ByVal value As String)
            _CREATEBY = value
        End Set
    End Property

    Public Property TYPE() As String
        Get
            Return _TYPE
        End Get
        Set(ByVal value As String)
            _TYPE = value
        End Set
    End Property
#End Region

    Public Function bindData(POLICYNO As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim txtsql As String
                txtsql = " SP_REL_POL_BENEFIT '" & POLICYNO & "' "

                Try
                    cmd.CommandText = txtsql

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(dt)
                    Return dt
                Catch ex As Exception
                    Return Nothing
                Finally
                    con.Close()
                End Try

            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsertPolBenefit() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim sql As String = ""
                sql = "dbo.sp_i_MSPOLICYCOBBENEFIT"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure

                com.Parameters.Add("@POLICYNO", SqlDbType.VarChar).Value = POLICYNO
                com.Parameters.Add("@PLANID", SqlDbType.VarChar).Value = PLANID
                com.Parameters.Add("@SUBPRODID", SqlDbType.VarChar).Value = SUBPRODID
                com.Parameters.Add("@CREATEBY", SqlDbType.VarChar).Value = CREATEBY
                com.Parameters.Add("@Type", SqlDbType.VarChar).Value = TYPE
                com.Parameters.Add("@ISACTIVE", SqlDbType.VarChar).Value = "True"

                Try
                    con.Open()

                    Dim rowSuccess As Integer = com.ExecuteNonQuery
                    If rowSuccess = 0 Then
                        Return False
                    Else
                        Return True
                    End If
                Catch ex As Exception
                    Return False
                Finally
                    con.Close()
                End Try
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function


    Public Function DeletePolBenefit() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim sql As String = ""
                sql = "dbo.sp_D_MSPOLICYCOBBENEFIT"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure

                com.Parameters.Add("@POLICYNO", SqlDbType.VarChar).Value = POLICYNO
                com.Parameters.Add("@Type", SqlDbType.VarChar).Value = TYPE


                Try
                    con.Open()

                    Dim rowSuccess As Integer = com.ExecuteNonQuery
                    If rowSuccess = 0 Then
                        Return False
                    Else
                        Return True
                    End If
                Catch ex As Exception
                    Return False
                Finally
                    con.Close()
                End Try
                'End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function
End Class
