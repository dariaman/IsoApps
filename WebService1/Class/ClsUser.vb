Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsUser

    Public UserId As String
    Public Pass As String
    Public UserName As String
    Public Email As String
    Public RoleCode As String
    Public IsActive As String
    Public BranchCode As String
    Public GroupCode As String
    Public TypeProduct As String
    Public Mkt_Code As String
    Public BranchCodeStay As String
    Public LvlAdmin As String
    Public Branch As String
    Public BranchName As String
    Public Judul As String
    Public UserIdInp As String
    Public UserNameInp As String
    Public EmailInp As String
    Public RoleCodeInp As String
    Public BranchCodeInp As String
    Public IsActiveInp As String
    Public RoleDesc As String
    Public Cre_Date As DateTime
    Public POLICYNO As String
    Public providerid As String
    Public POLICYNOINP As String

    Public firstOnline As String
    Public Online As String
    Public OnlineDate As DateTime
    Public OnlineIp As String
    Public OnlineDateCompare As DateTime
    Public OnlineIpCompare As String
    Public OnlineKeyQuestion As String
    Public Questiondesc As String
    Public OnlineKeyAnswer As String
    Public Verification As String
    Public BirthDate As Date
    Public BirthDateLogin As Date
    Public Gender As String
    Public GenderLogin As String
    Public Pict As String
    Public ExpirateDate As DateTime
    Public UserIdEditEmail As String
    Public UserEmailEditEmail As String
    Public PassGenUsr As String
    Public UserId2 As String

    Dim _ClsEncryption As New ClsEncryption

    Public Sub LoadData(ByVal userid As String, ByVal password As String) ', Source As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim com As New SqlCommand
                com.CommandTimeout = config.SQLtimeout
                com.Connection = con
                com.CommandType = CommandType.Text
                com.CommandText = String.Format("{0}.SP_S_MSUserPass '{1}','{2}','True'", config.mssqlOwner, userid, password) ', Source)
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    If Not IsDBNull(dr("firstOnline")) Then
                        Me.firstOnline = Convert.ToString(dr("firstOnline"))
                    End If
                    If Not IsDBNull(dr("Online")) Then
                        Me.Online = Convert.ToString(dr("Online"))
                    End If
                    If Not IsDBNull(dr("OnlineDate")) Then
                        Me.OnlineDate = Convert.ToDateTime(dr("OnlineDate"))
                    End If
                    If Not IsDBNull(dr("OnlineIp")) Then
                        Me.OnlineIp = Convert.ToString(dr("OnlineIp"))
                    End If
                    If Not IsDBNull(dr("OnlineDate")) Then
                        Me.OnlineDateCompare = Convert.ToDateTime(dr("OnlineDate"))
                    End If
                    If Not IsDBNull(dr("OnlineIp")) Then
                        Me.OnlineIpCompare = Convert.ToString(dr("OnlineIp"))
                    End If

                    If Not IsDBNull(dr("OnlineKeyQuestion")) Then
                        Me.OnlineKeyQuestion = Convert.ToString(dr("OnlineKeyQuestion"))
                    End If
                    If Not IsDBNull(dr("Questiondesc")) Then
                        Me.Questiondesc = Convert.ToString(dr("Questiondesc"))
                    End If
                    If Not IsDBNull(dr("OnlineKeyAnswer")) Then
                        Me.OnlineKeyAnswer = Convert.ToString(dr("OnlineKeyAnswer"))
                    End If
                    If Not IsDBNull(dr("UserId")) Then
                        Me.UserId = Convert.ToString(dr("UserId"))
                    End If
                    If Not IsDBNull(dr("UserName")) Then
                        Me.UserName = Convert.ToString(dr("UserName"))
                    End If
                    If Not IsDBNull(dr("Email")) Then
                        Me.Email = Convert.ToString(dr("Email"))
                    End If
                    If Not IsDBNull(dr("RoleCode")) Then
                        Me.RoleCode = Convert.ToString(dr("RoleCode"))
                    End If
                    If Not IsDBNull(dr("Password")) Then
                        Me.Pass = Convert.ToString(dr("Password"))
                    End If
                    If Not IsDBNull(dr("IsActive")) Then
                        Me.IsActive = Convert.ToString(dr("IsActive"))
                    End If
                    If Not IsDBNull(dr("BranchCode")) Then 'BRANCHCODE
                        Me.BranchCode = Convert.ToString(dr("BranchCode"))
                    End If
                    If Not IsDBNull(dr("BranchName")) Then 'BranchName
                        Me.BranchName = Convert.ToString(dr("BranchName"))
                    End If
                    If Not IsDBNull(dr("Judul")) Then 'BranchName
                        Me.Judul = Convert.ToString(dr("Judul"))
                    End If
                    If Not IsDBNull(dr("Mkt_Code")) Then 'BranchName
                        Me.Mkt_Code = Convert.ToString(dr("Mkt_Code"))
                    End If
                    If Not IsDBNull(dr("BranchCodeStay")) Then 'BranchName
                        Me.BranchCodeStay = Convert.ToString(dr("BranchCodeStay"))
                    End If
                    If Not IsDBNull(dr("LvlAdmin")) Then 'BranchName
                        Me.LvlAdmin = Convert.ToString(dr("LvlAdmin"))
                    End If
                    If Not IsDBNull(dr("Pict")) Then 'Pict
                        Me.Pict = Convert.ToString(dr("Pict"))
                    End If
                    If Not IsDBNull(dr("ROLEDESC")) Then 'Pict
                        Me.ROLEDESC = Convert.ToString(dr("ROLEDESC"))
                    End If
                    If Not IsDBNull(dr("ExpirateDate")) Then 'ExpirateDate
                        Me.ExpirateDate = Convert.ToString(dr("ExpirateDate"))
                    End If
                    If Not IsDBNull(dr("Cre_Date")) Then 'ExpirateDate
                        Me.Cre_Date = Convert.ToString(dr("Cre_Date"))
                    End If
                    If Not IsDBNull(dr("Cre_Date")) Then 'ExpirateDate
                        Me.Cre_Date = Convert.ToString(dr("Cre_Date"))
                    End If
                    If Not IsDBNull(dr("POLICYNO")) Then 'ExpirateDate
                        Me.POLICYNO = Convert.ToString(dr("POLICYNO"))
                    End If
                    If Not IsDBNull(dr("providerid")) Then 'ExpirateDate
                        Me.providerid = Convert.ToString(dr("providerid"))
                    End If
                    If Not IsDBNull(dr("Gender")) Then 'ExpirateDate
                        Me.GenderLogin = Convert.ToString(dr("Gender"))
                    End If
                    If Not IsDBNull(dr("BirthDate")) Then 'ExpirateDate
                        Me.BirthDateLogin = Convert.ToString(dr("BirthDate"))
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub LoadUserid(ByVal Userid As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim com As New SqlCommand
                com.CommandTimeout = config.SQLtimeout
                com.Connection = con
                com.CommandType = CommandType.Text
                com.CommandText = String.Format("select userid,IsActive,online,ONLINEDATE,ONLINEIP from Msuser with (nolock) where userid='" & Userid & "'")
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If Not IsDBNull(dr(0)) Then
                        Me.UserId = Convert.ToString(dr(0))
                    End If

                    If Not IsDBNull(dr(1)) Then
                        Me.IsActive = Convert.ToString(dr(1))
                    End If

                    If Not IsDBNull(dr(2)) Then
                        Me.Online = Convert.ToString(dr(2))
                    End If

                    If Not IsDBNull(dr(3)) Then
                        Me.OnlineDateCompare = Convert.ToString(dr(3))
                    End If

                    If Not IsDBNull(dr(4)) Then
                        Me.OnlineIpCompare = Convert.ToString(dr(4))
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Function LoadUseremail(ByVal usremail As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim com As New SqlCommand
                com.CommandTimeout = config.SQLtimeout
                com.Connection = con
                com.CommandType = CommandType.Text
                com.CommandText = String.Format("select userid,email from Msuser with (nolock) where email='" & usremail & "'")
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    
                    If Not IsDBNull(dr("userid")) Then 'BranchName
                        Me.UserIdEditEmail = Convert.ToString(dr("userid"))
                    End If

                    If Not IsDBNull(dr("email")) Then 'BranchName
                        Me.UserEmailEditEmail = Convert.ToString(dr("email"))
                    End If

                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function ubahpass(ByVal newPassword As String, ByVal userid As String, ByVal MOD_BY As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "UPDATE MSUser SET password='" & newPassword & "',MOD_BY='" & MOD_BY & "' where  userid ='" & userid & "'"
            Try
                con.Open()
                Dim rowSuccess As Integer = cmd.ExecuteNonQuery
                If rowSuccess = 0 Then
                    Throw New Exception("User Name tidak terdaftar.")
                    Return False
                End If
                Me.Pass = newPassword
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function SelectAllUser() As DataTable
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = " sp_s_generateNewUserProvider "
            Try

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt
                Else
                    Return dt
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function InsertUser() As DataTable
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_I_U_MSUser '" & Me.UserIdInp & "','" & Me.UserNameInp & "','" & Me.EmailInp & "','" & Me.RoleCodeInp & "','" & _ClsEncryption.Encrypt(Me.UserIdInp) & "','" & Me.UserId & "','" & Me.UserId & "','" & Me.IsActiveInp & "','" & Me.Mkt_Code & "','" & Me.Branch & "','" & Format(CDate(Me.BirthDate), "MM/dd/yyyy") & "','" & Me.Gender & "','" & Me.Pict & "','" & Format(CDate(Me.ExpirateDate), "MM/dd/yyyy") & "'"
            Try

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt
                Else
                    Return dt
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function InsertUserNew() As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Try

                Dim com As New SqlCommand
                com.CommandTimeout = config.SQLtimeout
                com.Connection = con
                com.CommandType = CommandType.Text
                com.CommandText = "SP_I_U_MSUser '" & Me.UserIdInp & "','" & Me.UserNameInp & "','" & Me.EmailInp & "','" & Me.RoleCodeInp & "','" & _ClsEncryption.Encrypt(Me.UserIdInp) & "','" & Me.UserId & "','" & Me.UserId & "','" & Me.IsActiveInp & "','" & Me.Mkt_Code & "','" & Me.Branch & "','" & Format(CDate(Me.BirthDate), "MM/dd/yyyy") & "','" & Me.Gender & "','" & Me.Pict & "','" & Format(CDate(Me.ExpirateDate), "MM/dd/yyyy") & "'"
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If Not IsDBNull(dr(0)) Then
                        Return Convert.ToString(dr(0))
                    End If
                    'Return True
                Else
                    Return ""
                    'Return False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try

        End Using
    End Function

    Public Function InsertUserNewGENUSRPROV() As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Try

                Dim com As New SqlCommand
                com.CommandTimeout = config.SQLtimeout
                com.Connection = con
                com.CommandType = CommandType.Text
                com.CommandText = "SP_I_U_MSUSER_GENPROVEE '" & Me.UserIdInp & "','" & Me.UserNameInp & "','" & Me.EmailInp & "','" & Me.RoleCodeInp & "','" & _ClsEncryption.Encrypt(Me.PassGenUsr) & "','" & Me.UserId & "','" & Me.UserId & "','" & Me.IsActiveInp & "','" & Me.Mkt_Code & "','" & Me.Branch & "','" & Me.Pict & "','" & Format(CDate(Me.ExpirateDate), "MM/dd/yyyy") & "','" & Me.providerid & "','" & Me.UserId2 & "'"
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If Not IsDBNull(dr(0)) Then
                        Return Convert.ToString(dr(0))
                    End If
                    'Return True
                Else
                    Return ""
                    'Return False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try

        End Using
    End Function

    Public Function InsertMSLOGINDETAIL(UserId As String, POLICYNO As String, MEMBID As String, PROVIDERID As String, ROLECODE As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_I_U_MSLOGINDETAIL '" & UserId & "','" & POLICYNO & "','" & MEMBID & "','" & PROVIDERID & "','" & rolecode & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try
        End Using

    End Function

    Public Function DeleteMSLOGINDETAIL(UserId As String, POLICYNO As String, PROVIDERID As String, ROLECODE As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_D_MSLOGINDETAIL '" & UserId & "','" & POLICYNO & "','" & PROVIDERID & "','" & ROLECODE & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try
        End Using

    End Function

    Public Function InsertUserSignUp() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try

                Dim com As New SqlCommand
                com.CommandTimeout = config.SQLtimeout
                com.Connection = con
                com.CommandType = CommandType.Text
                com.CommandText = "SP_I_MSUserSignUp '" & Me.UserNameInp & "','" & Me.EmailInp & "'"
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                Dim dr As SqlDataReader = com.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If Not IsDBNull(dr(0)) Then
                        Me.UserId = Convert.ToString(dr(0))
                    End If
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try

        End Using
    End Function

    Public Function UpdateUserQuestion(ext As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_u_userQuestion '" & Me.OnlineKeyQuestion & "','" & Me.OnlineKeyAnswer & "','" & Me.UserId & "','" & ext & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function UpdateUseronlineUpdate() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_u_useronlineUpdate '" & Me.UserId & "','" & Me.Online & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function ubahVerification(ByVal Userid As String, Verification As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "UPDATE MSUser SET Verification ='" & Verification & "' where userid ='" & Userid & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function DeleteBranchAccess() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from MSBranchAccess where userid= '" & UserIdInp & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function DeleteGroupAccess() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from MSGroupAccess where userid= '" & UserIdInp & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function DeleteProductAccess() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from MSProdukAccess where userid= '" & UserIdInp & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function InsertUserBranch(usrBranch As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_I_MSBranchAccess '" & usrBranch & "','" & Me.UserIdInp & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function InsertUserGroup(usrGroup As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_I_MSGroupAccess '" & usrGroup & "','" & Me.UserIdInp & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function InsertUserProduct(usrlob As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_I_MSProdukAccess '" & usrlob & "','" & Me.UserIdInp & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function SelectDataMSUserlike(type As String, SearchData As String) As DataTable
        Try

            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                Dim kolom As String
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                'cmd.CommandText = "[dbo].[SP_S_MSUserlike] '" & type & "','" & userid & "','True','" & useridss & "'"
                If type = "id" Then
                    kolom = "USERID"
                Else
                    kolom = "USERNAME"
                End If
                If SearchData = "" Then
                    cmd.CommandText = "SELECT USERID,USERNAME,PASSWORD FROM MSUSER"
                Else
                    cmd.CommandText = "SELECT USERID,USERNAME,PASSWORD FROM MSUSER WHERE " + kolom + " LIKE @search"
                    cmd.Parameters.Add(New SqlParameter("@search", SqlDbType.VarChar) With {.Value = SearchData})
                End If

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
            'ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>alert('Error!, Please contact IT Support');</script>")
            'Dim msg As String = String.Format("{0} - resetPassword - " & Session("userid") & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            'WriteFile.Write(config.SetFullFilePath, msg)
            Throw New Exception(ex.Message)
        End Try

    End Function
End Class

