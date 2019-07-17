Imports System.Data.SqlClient
Public Class ClsDischarge

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
                Dim Sql As String = "select top 20 DiagCode, DiagDesc from dbo.fc_tbl_diagnose('') where LTRIM(left(DiagCode+'          ',11)+ DiagDesc) ='" & prefixText.Trim & "'  "
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
                Dim Sql As String = "select * from dbo.FC_PRV_PROVIDER_MASTER('') where LTRIM(left(cast(PROVIDERID as varchar(10))+'          ',11)+ PROVIDERNAME) ='" & prefixText.Trim & "'"
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

    Public Function bindDischarge2(TransactionId As String, status As String, ProviderID As String) As DataTable
        If ProviderID = "" Then
            ProviderID = "%"
        End If
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONHDR_SELECT2"
                    cmd.Parameters.Clear()
                    cmd.Parameters.Add("@TRANSACTIONID", SqlDbType.VarChar).Value = TransactionId
                    cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = status
                    cmd.Parameters.Add("@PROVIDERID", SqlDbType.VarChar).Value = ProviderID

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

    Public Function getTransaction(trxid As String, opt As String, providerId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONHDR_GET '" & trxid & "'," & opt & ",'" & providerId & "'"
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

    Public Function transactionInfoInsert(trxid As String, doctor_name As String, sympton As String, diag1 As String, diag2 As String, diag3 As String, isreferral As String, referred_provider As String, remark As String, createby As String, referred_code As String, REFERRALREMARK As String, tindakan As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONINFO_CHECK_INSERT_UPDATE '" & trxid & "','" & doctor_name & "','" & sympton & "','" & diag1 & "','" & diag2 & "','" & diag3 & "','" & isreferral & "','" & referred_provider & "','" & remark & "','" & createby & "','" & referred_code & "','" & REFERRALREMARK & "','" & tindakan & "'"
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


    Public Function transactionHdrUpdateStatus(trxid As String, status As String, userId As String) As Boolean
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "USP_MSTRANSACTIONHDR_UPDATE_STATUS '" & trxid & "','" & status & "'," & userId
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
    Public Function transactionHdrSubmit(trxid As String, status As String, userId As String) As Boolean
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "USP_MSTRANSACTIONHDR_SUBMIT '" & trxid & "','" & status & "','" & userId & "'"
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
    Public Function tmpTransactionBenefitCalculateInsert(trxId As String, subProdId As String, benefitId As String, subLimit As String, subGroup As String, limitAmtId As String, lengthOfStay As String, benLimAmt As String, eligibleamt As String, billedAmt As String, cardNo As String, policyNo As String, referralRemark As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "USP_TMPTRANSACTIONBENEFITCALCULATE_INSERT '" & trxId & "','" & subProdId & "','" & benefitId & "'," & subLimit & ",'" & subGroup & "','" & limitAmtId & "','" & lengthOfStay & "'," & benLimAmt & "," & eligibleamt & "," & billedAmt & ",'" & cardNo & "','" & policyNo & "','" & referralRemark & "'"
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

    Public Function tmpTransactionBenefitCalculateDelete(trxId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "USP_TMPTRANSACTIONBENEFITCALCULATE_DELETE '" & trxId & "'"
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

    Public Function tmpTransactionBenefitCalculateProcess(trxId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "USP_TMPTRANSACTIONBENEFITCALCULATE_PROCESS '" & trxId & "'"
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

    Public Function transactionBenefitUpdateStatus(trxId As String, status As String, userId As String) As Boolean
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "USP_MSTRANSACTIONBENEFIT_SET_STATUS '" & trxId & "','" & status & "'," & userId
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

    Public Function transactionBenefitReinputCheckInsert(trxid As String, subprod As String, benefitid As String, los As Integer, eligibleamt As Decimal, billedamt As Decimal, origacceptamt As Decimal, origexcessamt As Decimal, acceptamt As Decimal, excessamt As Decimal, remark As String, status As String, createby As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_MSTRANSACTIONBENEFIT_REINPUT_CHECK_INSERT '" & trxid & "','" & subprod & "','" & benefitid & "'," & los & "," & CDec(eligibleamt) & "," & billedamt & "," & origacceptamt & "," & origexcessamt & "," & acceptamt & "," & excessamt & ",'" & remark & "','" & status & "','" & createby & "'"
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

    Public Function transactionBenefitLogInsert(trxId As String, logAction As String, userId As String) As Boolean
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "USP_MSTRANSACTIONBENEFIT_LOG_INSERT '" & trxId & "','" & logAction & "','" & userId & "'"
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

    Public Function reasonList(type As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "USP_MSREASON_LIST '" & type & "'"
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

    Public Function transactionRejectInfoAction(trxid As String, statusId As String, reasonId As String, userId As String) As Boolean
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "USP_MSTRANSACTIONREJECTINFO_ACTION '" & trxid & "','" & statusId & "','" & reasonId & "'," & userId
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
