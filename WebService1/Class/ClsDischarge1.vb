Imports System.Data.SqlClient
Public Class ClsDischarge1

    Public Function TPARemarks(ByVal policyno As String) As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim Sql As String = "select tparemarks FROM REL..POL_POLICY_TPAREMARKS  where policyno='" & policyno.Trim & "'  "
                Dim com As SqlCommand = New SqlCommand(Sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.Text
                con.Open()
                Dim rowSuccess As String = com.ExecuteScalar
                If rowSuccess = "" Then
                    Return rowSuccess
                    Throw New Exception("Error -" & Sql)
                Else
                    Return rowSuccess
                End If

            Catch ex As Exception
                Return ""
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function DiagDescCd(ByVal prefixText As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim Sql As String = "select top 20 DiagCode, DiagDesc from dbo.fc_tbl_diagnose('') where left(DiagCode+'          ',11)+ DiagDesc ='" & prefixText.Trim & "'  "
                Dim com As SqlCommand = New SqlCommand(Sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.Text
                com.Parameters.AddWithValue("@SearchText", prefixText)
                con.Open()
                Dim rowSuccess As String = com.ExecuteScalar
                If rowSuccess = "" Then
                    Return False
                    Throw New Exception("Error -" & Sql)
                Else
                    Return True
                End If

            Catch ex As Exception
                Return False
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function PROVIDERCd(ByVal prefixText As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim Sql As String = "select * from dbo.FC_PRV_PROVIDER_MASTER('') where left(cast(PROVIDERID as varchar(10))+'          ',11)+ PROVIDERNAME ='" & prefixText.Trim & "'"
                Dim com As SqlCommand = New SqlCommand(Sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.Text
                com.Parameters.AddWithValue("@SearchText", prefixText)
                con.Open()
                Dim rowSuccess As String = com.ExecuteScalar
                If rowSuccess = "" Then
                    Return False
                    Throw New Exception("Error -" & Sql)
                Else
                    Return True
                End If

            Catch ex As Exception
                Return False
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function bindMSTRANSACTIONBENEFIT(search As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_S_MSTRANSACTIONBENEFIT  '" & search & "'"
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

    Public Function bindMSTRANSACTIONINFO(search As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_S_MSTRANSACTIONINFO  '" & search & "'"
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

    Public Function bindDischarge(status As String, search As String, ProviderId As String, statushdr As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONHDR_SELECT '" & status & "','" & search & "','" & ProviderId & "','" & statushdr & "'"
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

    Public Function getTransaction(trxid As String, opt As String, providerid As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONHDR_GET '" & trxid & "'," & opt & ",'" & providerid & "'"
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

    Public Function transactionInfoInsert(trxid As String, doctor_name As String, sympton As String, diag1 As String, diag2 As String, isreferral As String, referred_provider As String, remark As String, createby As String, referred_code As String, REFERRALREMARK As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONINFO_CHECK_INSERT_UPDATE '" & trxid & "','" & doctor_name & "','" & sympton & "','" & diag1 & "','" & diag2 & "','" & isreferral & "','" & referred_provider & "','" & remark & "','" & createby & "','" & referred_code & "','" & REFERRALREMARK & "'"
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

    Public Function bindCoverage(trxId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_S_POL_PLAN_CLASS_GET_BY_TRXID '" & trxId & "'"
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

    Public Function bindMemberBenefitDetail(trxId As String, SubProdId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_S_POL_POLICY_MEMBER_PLAN_BENEFIT_DETAIL_BY_TRXID '" & trxId & "','" & SubProdId & "'"
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

    Public Function transactionBenefitInsert(trxid As String, subprod As String, benefitid As String, los As Integer, eligibleamt As Decimal, billedamt As Decimal, origacceptamt As Decimal, origexcessamt As Decimal, acceptamt As Decimal, excessamt As Decimal, remark As String, status As String, createby As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONBENEFIT_INSERT '" & trxid & "','" & subprod & "','" & benefitid & "'," & los & "," & CDec(eligibleamt) & "," & billedamt & "," & origacceptamt & "," & origexcessamt & "," & acceptamt & "," & excessamt & ",'" & remark & "','" & status & "','" & createby & "'"
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

    Public Function getTransactionReferralCode() As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "USP_MSTRANSACTIONINFO_GENERATEREFERRALCODE "
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

    Public Function transactionHdrUpdateStatus(trxid As String, status As String) As Boolean
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "USP_MSTRANSACTIONHDR_UPDATE_STATUS '" & trxid & "','" & status & "'"
                Try
                    con.Open()

                    Dim rowSuccess As Integer = cmd.ExecuteNonQuery
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

            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
