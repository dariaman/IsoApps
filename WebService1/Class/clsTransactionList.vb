Imports System.Data.SqlClient
Public Class clsTransactionList

    Public Function bindTransactionList(status As String, search As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONHDR_SELECT '" & status & "','" & search & "'"
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

    Public Function getTransactionHdr(trxid As String, opt As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONHDR_GET '" & trxid & "'," & opt
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

    Public Function getTransactionInfo(trxid As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONINFO_GET '" & trxid & "'"
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
    Public Function getTransactionBenefit(trxid As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "USP_MSTRANSACTIONBENEFIT_GET '" & trxid & "'"
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
