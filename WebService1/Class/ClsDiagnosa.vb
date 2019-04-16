Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsDiagnosa
#Region "property"

    Private _DIAGCODE As String
    Private _DIAGDESC As String
    

    Public Property DIAGCODE() As String
        Get
            Return _DIAGCODE
        End Get
        Set(ByVal value As String)
            _DIAGCODE = value
        End Set
    End Property

    Public Property DIAGDESC() As String
        Get
            Return _DIAGDESC
        End Get
        Set(ByVal value As String)
            _DIAGDESC = value
        End Set
    End Property



#End Region

    Public Function bindData(paramkey As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String = " SELECT * FROM  FC_TBL_DIAGNOSE('" & paramkey & "')  order by DIAGCODE"

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

    Public Function bindisiData(DIAGCODE As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = " SELECT * FROM  FC_TBL_DIAGNOSE('') where DIAGCODE='" & DIAGCODE & "'  order by DIAGCODE"
                Try

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


End Class

