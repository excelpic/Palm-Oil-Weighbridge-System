Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class DatabaseHelper

    Private Shared _connectionString As String = ""
    Private Shared _serverName As String = ".\SQLEXPRESS"
    Private Shared _dbName As String = "sistemTimbanganPKS"
    Private Shared _useWindowsAuth As Boolean = True
    Private Shared _sqlUsername As String = ""
    Private Shared _sqlPassword As String = ""

    Private Const ConfigFolderName As String = "WeighBridge"
    Private Const ConfigFileName As String = "db_config.txt"
    Private Const EncryptedCredentialsKey As String = "CREDENTIALS_DPAPI="

    Public Shared ReadOnly Property ConnectionString As String
        Get
            If String.IsNullOrEmpty(_connectionString) Then
                LoadConnectionConfig()
            End If
            Return _connectionString
        End Get
    End Property

    Public Shared ReadOnly Property ServerName As String
        Get
            EnsureConnectionConfigLoaded()
            Return _serverName
        End Get
    End Property

    Public Shared ReadOnly Property DatabaseName As String
        Get
            EnsureConnectionConfigLoaded()
            Return _dbName
        End Get
    End Property

    Public Shared ReadOnly Property UseWindowsAuthentication As Boolean
        Get
            EnsureConnectionConfigLoaded()
            Return _useWindowsAuth
        End Get
    End Property

    Public Shared ReadOnly Property SqlUsername As String
        Get
            EnsureConnectionConfigLoaded()
            Return _sqlUsername
        End Get
    End Property

    Public Shared ReadOnly Property SqlPassword As String
        Get
            EnsureConnectionConfigLoaded()
            Return _sqlPassword
        End Get
    End Property

    Private Shared Sub EnsureConnectionConfigLoaded()
        If String.IsNullOrEmpty(_connectionString) Then
            LoadConnectionConfig()
        End If
    End Sub

    Public Shared Function BuildConnectionString(serverName As String,
                                                   dbName As String,
                                                   useWindowsAuth As Boolean,
                                                   sqlUsername As String,
                                                   sqlPassword As String,
                                                   Optional timeoutSeconds As Integer = 10) As String
        If String.IsNullOrWhiteSpace(serverName) Then Throw New ArgumentException("Server name cannot be empty.", NameOf(serverName))
        If String.IsNullOrWhiteSpace(dbName) Then Throw New ArgumentException("Database name cannot be empty.", NameOf(dbName))
        If timeoutSeconds < 1 OrElse timeoutSeconds > 300 Then timeoutSeconds = 10

        Dim builder As New SqlConnectionStringBuilder() With {
            .DataSource = serverName.Trim(),
            .InitialCatalog = dbName.Trim(),
            .IntegratedSecurity = useWindowsAuth,
            .ConnectTimeout = timeoutSeconds,
            .PersistSecurityInfo = False
        }

        If Not useWindowsAuth Then
            builder.UserID = If(sqlUsername, String.Empty).Trim()
            builder.Password = If(sqlPassword, String.Empty)
        End If

        Return builder.ConnectionString
    End Function

    Public Shared Sub GetSavedConnectionSettings(ByRef serverName As String,
                                                  ByRef dbName As String,
                                                  ByRef useWindowsAuth As Boolean,
                                                  ByRef sqlUsername As String,
                                                  ByRef sqlPassword As String)
        EnsureConnectionConfigLoaded()
        serverName = _serverName
        dbName = _dbName
        useWindowsAuth = _useWindowsAuth
        sqlUsername = _sqlUsername
        sqlPassword = _sqlPassword
    End Sub

    Public Shared Sub LoadConnectionConfig()
        Dim serverName As String = ".\SQLEXPRESS"
        Dim dbName As String = "sistemTimbanganPKS"
        Dim useWindowsAuth As Boolean = True
        Dim sqlUsername As String = ""
        Dim sqlPassword As String = ""
        Dim legacyPlaintextCredentialsFound As Boolean = False

        Dim configFolder As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ConfigFolderName)
        Dim configFile As String = Path.Combine(configFolder, ConfigFileName)

        If File.Exists(configFile) Then
            Try
                Dim encryptedCredentials As String = ""

                For Each rawLine As String In File.ReadAllLines(configFile)
                    Dim line As String = If(rawLine, "").Trim()
                    If line.StartsWith("SERVER=", StringComparison.OrdinalIgnoreCase) Then
                        serverName = line.Substring(7).Trim()
                    ElseIf line.StartsWith("DATABASE=", StringComparison.OrdinalIgnoreCase) Then
                        dbName = line.Substring(9).Trim()
                    ElseIf line.StartsWith("AUTH=", StringComparison.OrdinalIgnoreCase) Then
                        useWindowsAuth = (line.Substring(5).Trim().Equals("WINDOWS", StringComparison.OrdinalIgnoreCase))
                    ElseIf line.StartsWith("USERNAME=", StringComparison.OrdinalIgnoreCase) Then
                        ' Legacy plaintext format. Read only to allow one-time migration.
                        sqlUsername = line.Substring(9)
                        legacyPlaintextCredentialsFound = True
                    ElseIf line.StartsWith("PASSWORD=", StringComparison.OrdinalIgnoreCase) Then
                        ' Legacy plaintext format. Read only to allow one-time migration.
                        sqlPassword = line.Substring(9)
                        legacyPlaintextCredentialsFound = True
                    ElseIf line.StartsWith(EncryptedCredentialsKey, StringComparison.OrdinalIgnoreCase) Then
                        encryptedCredentials = line.Substring(EncryptedCredentialsKey.Length).Trim()
                    End If
                Next

                If Not String.IsNullOrWhiteSpace(encryptedCredentials) Then
                    Dim decryptedCredentials As String = DecryptCredentials(encryptedCredentials)
                    Dim separatorIndex As Integer = decryptedCredentials.IndexOf(ChrW(9))
                    If separatorIndex >= 0 Then
                        sqlUsername = decryptedCredentials.Substring(0, separatorIndex)
                        sqlPassword = decryptedCredentials.Substring(separatorIndex + 1)
                        legacyPlaintextCredentialsFound = False
                    Else
                        sqlUsername = ""
                        sqlPassword = ""
                    End If
                ElseIf legacyPlaintextCredentialsFound Then
                    ' Replace legacy plaintext credentials with DPAPI-protected data immediately.
                    Try
                        SaveConnectionConfig(serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword)
                        legacyPlaintextCredentialsFound = False
                    Catch ex As Exception
                        Debug.WriteLine("[DatabaseHelper.LoadConnectionConfig] Legacy credential migration error: " & ex.ToString())
                        ' Keep the in-memory values for compatibility if migration cannot be written.
                    End Try
                End If
            Catch ex As Exception
                Debug.WriteLine("[DatabaseHelper.LoadConnectionConfig] Read error: " & ex.ToString())
                ' Keep secure application defaults when configuration cannot be read.
            End Try
        End If

        _serverName = serverName
        _dbName = dbName
        _useWindowsAuth = useWindowsAuth
        _sqlUsername = sqlUsername
        _sqlPassword = sqlPassword

        _connectionString = BuildConnectionString(_serverName, _dbName, _useWindowsAuth, _sqlUsername, _sqlPassword)
    End Sub

    Private Shared Function EncryptCredentials(username As String, password As String) As String
        Dim plainBytes As Byte() = Encoding.UTF8.GetBytes((If(username, "")) & ChrW(9) & (If(password, "")))
        Dim protectedBytes As Byte() = ProtectedData.Protect(plainBytes, Nothing, DataProtectionScope.CurrentUser)
        Return Convert.ToBase64String(protectedBytes)
    End Function

    Private Shared Function DecryptCredentials(encoded As String) As String
        Dim protectedBytes As Byte() = Convert.FromBase64String(encoded)
        Dim plainBytes As Byte() = ProtectedData.Unprotect(protectedBytes, Nothing, DataProtectionScope.CurrentUser)
        Return Encoding.UTF8.GetString(plainBytes)
    End Function

    Public Shared Sub SaveConnectionConfig(serverName As String, dbName As String,
                                           Optional useWindowsAuth As Boolean = True,
                                           Optional sqlUsername As String = "",
                                           Optional sqlPassword As String = "")
        If String.IsNullOrWhiteSpace(serverName) Then Throw New ArgumentException("Server name cannot be empty.", NameOf(serverName))
        If String.IsNullOrWhiteSpace(dbName) Then Throw New ArgumentException("Database name cannot be empty.", NameOf(dbName))
        If Not useWindowsAuth AndAlso String.IsNullOrWhiteSpace(sqlUsername) Then
            Throw New ArgumentException("SQL username cannot be empty when SQL Authentication is selected.", NameOf(sqlUsername))
        End If

        Dim configFolder As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ConfigFolderName)
        If Not Directory.Exists(configFolder) Then
            Directory.CreateDirectory(configFolder)
        End If

        Dim configFile As String = Path.Combine(configFolder, ConfigFileName)
        Dim lines As New List(Of String) From {
            "SERVER=" & serverName.Trim(),
            "DATABASE=" & dbName.Trim(),
            "AUTH=" & If(useWindowsAuth, "WINDOWS", "SQL")
        }

        If Not useWindowsAuth Then
            lines.Add(EncryptedCredentialsKey & EncryptCredentials(sqlUsername.Trim(), sqlPassword))
        End If

        File.WriteAllLines(configFile, lines, New UTF8Encoding(False))

        _connectionString = ""
        LoadConnectionConfig()
    End Sub

    Private Shared Function QuoteSqlIdentifier(identifier As String) As String
        If identifier Is Nothing Then Throw New ArgumentNullException(NameOf(identifier))
        Return "[" & identifier.Replace("]", "]]" ) & "]"
    End Function

    Public Enum DatabaseConnectionFailureType
        Unknown
        DatabaseNotFound
        AuthenticationFailed
        ServerUnavailable
    End Enum

    Private Shared Function ClassifyConnectionFailure(ex As Exception) As DatabaseConnectionFailureType
        Dim sqlEx As SqlException = TryCast(ex, SqlException)
        If sqlEx Is Nothing Then Return DatabaseConnectionFailureType.Unknown

        Select Case sqlEx.Number
            Case 4060, 911
                Return DatabaseConnectionFailureType.DatabaseNotFound
            Case 18456, 18452, 18453, 18488
                Return DatabaseConnectionFailureType.AuthenticationFailed
            Case 26, 53, 40, 258
                Return DatabaseConnectionFailureType.ServerUnavailable
            Case Else
                Return DatabaseConnectionFailureType.Unknown
        End Select
    End Function

    Private Shared Function GetSafeConnectionErrorMessage(failureType As DatabaseConnectionFailureType) As String
        Select Case failureType
            Case DatabaseConnectionFailureType.DatabaseNotFound
                Return "Database tidak ditemukan atau belum dibuat pada server yang dipilih."
            Case DatabaseConnectionFailureType.AuthenticationFailed
                Return "Autentikasi database gagal. Periksa metode autentikasi dan kredensial lokal."
            Case DatabaseConnectionFailureType.ServerUnavailable
                Return "Server database tidak dapat dihubungi. Periksa nama server dan layanan SQL Server."
            Case Else
                Return "Koneksi database gagal. Periksa server, database, dan pengaturan autentikasi."
        End Select
    End Function

    Public Shared Function TestConnectionCustom(serverName As String, dbName As String,
                                                useWindowsAuth As Boolean,
                                                sqlUsername As String, sqlPassword As String,
                                                ByRef errorMessage As String,
                                                Optional ByRef failureType As DatabaseConnectionFailureType = DatabaseConnectionFailureType.Unknown) As Boolean
        Try
            Dim testConnStr As String = BuildConnectionString(
                serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword, 10)

            Using conn As New SqlConnection(testConnStr)
                conn.Open()
                errorMessage = ""
                Return True
            End Using
        Catch ex As Exception
            failureType = ClassifyConnectionFailure(ex)
            Debug.WriteLine("[DatabaseHelper.TestConnectionCustom] " & failureType.ToString() & ": " & ex.ToString())
            errorMessage = GetSafeConnectionErrorMessage(failureType)
            Return False
        End Try
    End Function

    Public Shared Function EnsureDatabaseExists(serverName As String, dbName As String,
                                                useWindowsAuth As Boolean,
                                                sqlUsername As String, sqlPassword As String,
                                                ByRef errorMessage As String) As Boolean
        Try
            If String.IsNullOrWhiteSpace(serverName) Then Throw New ArgumentException("Server name cannot be empty.", NameOf(serverName))
            If String.IsNullOrWhiteSpace(dbName) Then Throw New ArgumentException("Database name cannot be empty.", NameOf(dbName))
            If dbName.Length > 128 Then Throw New ArgumentException("Database name is too long.", NameOf(dbName))

            Dim masterConnStr As String = BuildConnectionString(
                serverName, "master", useWindowsAuth, sqlUsername, sqlPassword, 10)

            Using conn As New SqlConnection(masterConnStr)
                conn.Open()
                Dim checkQuery As String = "SELECT COUNT(*) FROM sys.databases WHERE name = @DatabaseName"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.Add("@DatabaseName", SqlDbType.NVarChar, 128).Value = dbName
                    Dim count As Integer = CInt(cmd.ExecuteScalar())
                    If count = 0 Then
                        Dim createQuery As String = "CREATE DATABASE " & QuoteSqlIdentifier(dbName)
                        Using createCmd As New SqlCommand(createQuery, conn)
                            createCmd.ExecuteNonQuery()
                        End Using
                        errorMessage = "Database berhasil dibuat. Silakan jalankan script untuk membuat tabel."
                        Return True
                    Else
                        errorMessage = ""
                        Return True
                    End If
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("[DatabaseHelper.EnsureDatabaseExists] Error: " & ex.ToString())
            errorMessage = "Database tidak dapat dibuat atau diakses. Periksa hak akses dan pengaturan koneksi."
            Return False
        End Try
    End Function

    Public Shared Function GetConnection() As SqlConnection
        Return New SqlConnection(ConnectionString)
    End Function

    Public Shared Function TestConnection(ByRef errorMessage As String,
                                          Optional ByRef failureType As DatabaseConnectionFailureType = DatabaseConnectionFailureType.Unknown) As Boolean
        Try
            Using conn As SqlConnection = GetConnection()
                conn.Open()
                errorMessage = ""
                Return True
            End Using
        Catch ex As Exception
            failureType = ClassifyConnectionFailure(ex)
            Debug.WriteLine("[DatabaseHelper.TestConnection] " & failureType.ToString() & ": " & ex.ToString())
            errorMessage = GetSafeConnectionErrorMessage(failureType)
            Return False
        End Try
    End Function

    Public Shared Function ExecuteQuery(query As String, Optional parameters As SqlParameter() = Nothing) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As SqlConnection = GetConnection()
                Using cmd As New SqlCommand(query, conn)
                    cmd.CommandTimeout = 30
                    If parameters IsNot Nothing Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("[DatabaseHelper.ExecuteQuery] Error: " & ex.ToString())
            MessageBox.Show("The database query could not be completed. Check the connection and application configuration.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function

    Public Shared Function ExecuteNonQuery(query As String, Optional parameters As SqlParameter() = Nothing) As Integer
        Try
            Using conn As SqlConnection = GetConnection()
                Using cmd As New SqlCommand(query, conn)
                    cmd.CommandTimeout = 30
                    If parameters IsNot Nothing Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    conn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("[DatabaseHelper.ExecuteNonQuery] Error: " & ex.ToString())
            MessageBox.Show("The database operation could not be completed. Check the connection and application configuration.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return -1
        End Try
    End Function

    Public Shared Function ExecuteScalar(query As String, Optional parameters As SqlParameter() = Nothing) As Object
        Try
            Using conn As SqlConnection = GetConnection()
                Using cmd As New SqlCommand(query, conn)
                    cmd.CommandTimeout = 30
                    If parameters IsNot Nothing Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    conn.Open()
                    Return cmd.ExecuteScalar()
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("[DatabaseHelper.ExecuteScalar] Error: " & ex.ToString())
            MessageBox.Show("The database operation could not be completed. Check the connection and application configuration.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    ' =============================================
    ' GENERATE NO TIKET LAMA (PAKAI STORED PROCEDURE)
    ' Dipertahankan untuk backward compatibility
    ' =============================================
    Public Shared Function GenerateNoTiket(prefix As String) As String
        Try
            Using conn As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("sp_GenerateNoTiket", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 10).Value = If(prefix, "").Trim()

                    Dim outputParam As New SqlParameter("@NoTiket", SqlDbType.NVarChar, 20)
                    outputParam.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(outputParam)

                    conn.Open()
                    cmd.ExecuteNonQuery()

                    Return outputParam.Value.ToString()
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("[DatabaseHelper.GenerateNoTiket] Error: " & ex.ToString())
            MessageBox.Show("Gagal menghasilkan nomor tiket.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    ' =============================================
    ' AUDIT LOG
    ' =============================================
    Private Shared Sub AddAuditParameters(cmd As SqlCommand, userID As Integer, action As String,
                                           tableName As String, recordID As Integer?,
                                           oldValue As String, newValue As String,
                                           keterangan As String, ipAddress As String,
                                           computerName As String)
        Dim pUserID As SqlParameter = cmd.Parameters.Add("@UserID", SqlDbType.Int)
        pUserID.Value = If(userID > 0, CObj(userID), DBNull.Value)

        cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50).Value = If(action, "UNKNOWN")
        cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 50).Value = If(String.IsNullOrWhiteSpace(tableName), DBNull.Value, CObj(tableName.Trim()))

        Dim pRecordID As SqlParameter = cmd.Parameters.Add("@RecordID", SqlDbType.Int)
        pRecordID.Value = If(recordID.HasValue AndAlso recordID.Value > 0, CObj(recordID.Value), DBNull.Value)

        cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar, -1).Value = If(String.IsNullOrWhiteSpace(oldValue), DBNull.Value, CObj(oldValue))
        cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar, -1).Value = If(String.IsNullOrWhiteSpace(newValue), DBNull.Value, CObj(newValue))
        cmd.Parameters.Add("@Keterangan", SqlDbType.NVarChar, 255).Value = If(String.IsNullOrWhiteSpace(keterangan), DBNull.Value, CObj(keterangan))
        cmd.Parameters.Add("@IPAddress", SqlDbType.NVarChar, 50).Value = If(String.IsNullOrWhiteSpace(ipAddress), DBNull.Value, CObj(ipAddress))
        cmd.Parameters.Add("@ComputerName", SqlDbType.NVarChar, 100).Value = If(String.IsNullOrWhiteSpace(computerName), DBNull.Value, CObj(computerName))
    End Sub

    Public Shared Sub InsertAuditLog(userID As Integer,
                                     action As String,
                                     Optional tableName As String = Nothing,
                                     Optional recordID As Integer? = Nothing,
                                     Optional oldValue As String = Nothing,
                                     Optional newValue As String = Nothing,
                                     Optional keterangan As String = Nothing)
        Try
            If UserSession.IsProgrammer() Then Return
            If String.IsNullOrWhiteSpace(action) Then
                action = "UNKNOWN"
            End If

            Dim ipAddress As String = GetLocalIPAddress()
            Dim computerName As String = Environment.MachineName

            Dim query As String = "INSERT INTO AuditLog (UserID, Action, TableName, RecordID, " &
                                  "OldValue, NewValue, Keterangan, IPAddress, ComputerName, CreatedAt) " &
                                  "VALUES (@UserID, @Action, @TableName, @RecordID, " &
                                  "@OldValue, @NewValue, @Keterangan, @IPAddress, @ComputerName, GETDATE())"

            Using conn As SqlConnection = GetConnection()
                Using cmd As New SqlCommand(query, conn)
                    cmd.CommandTimeout = 30
                    AddAuditParameters(cmd, userID, action, tableName, recordID, oldValue, newValue,
                                       keterangan, ipAddress, computerName)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Debug.WriteLine($"[AuditLog] Inserted: {action} by UserID {userID}")
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine($"[AuditLog] Error: {ex.Message}")
        End Try
    End Sub

    Public Shared Sub InsertAuditLog(userID As Integer,
                                     action As String,
                                     timbangID As Integer?,
                                     tableName As String,
                                     recordID As Integer?,
                                     oldValue As String,
                                     newValue As String,
                                     keterangan As String)
        Try
            If UserSession.IsProgrammer() Then Return
            If String.IsNullOrWhiteSpace(action) Then
                action = "UNKNOWN"
            End If

            Dim ipAddress As String = GetLocalIPAddress()
            Dim computerName As String = Environment.MachineName

            Dim query As String = "INSERT INTO AuditLog (UserID, Action, TableName, RecordID, " &
                                  "OldValue, NewValue, Keterangan, IPAddress, ComputerName, CreatedAt) " &
                                  "VALUES (@UserID, @Action, @TableName, @RecordID, " &
                                  "@OldValue, @NewValue, @Keterangan, @IPAddress, @ComputerName, GETDATE())"

            Using conn As SqlConnection = GetConnection()
                Using cmd As New SqlCommand(query, conn)
                    cmd.CommandTimeout = 30
                    Dim finalRecordID As Integer? = Nothing
                    If recordID.HasValue AndAlso recordID.Value > 0 Then
                        finalRecordID = recordID.Value
                    ElseIf timbangID.HasValue AndAlso timbangID.Value > 0 Then
                        finalRecordID = timbangID.Value
                    End If

                    AddAuditParameters(cmd, userID, action, tableName, finalRecordID, oldValue, newValue,
                                       keterangan, ipAddress, computerName)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Debug.WriteLine($"[AuditLog] Inserted: {action} by UserID {userID}")
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine($"[AuditLog] Error: {ex.Message}")
        End Try
    End Sub

    Public Shared Sub InsertAuditLog(userID As Integer, action As String)
        InsertAuditLog(userID, action, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Public Shared Sub InsertAuditLogWithDescription(userID As Integer, action As String, keterangan As String)
        InsertAuditLog(userID, action, Nothing, Nothing, Nothing, Nothing, keterangan)
    End Sub

    Public Shared Sub InsertAuditLogTimbangan(userID As Integer, action As String, timbangID As Integer, keterangan As String)
        InsertAuditLog(userID, action, timbangID, "Timbangan", timbangID, Nothing, Nothing, keterangan)
    End Sub

    Public Shared Sub InsertAuditLogChange(userID As Integer, action As String, tableName As String,
                                           recordID As Integer, oldValue As String, newValue As String,
                                           Optional keterangan As String = Nothing)
        InsertAuditLog(userID, action, Nothing, tableName, recordID, oldValue, newValue, keterangan)
    End Sub

    Public Shared Function GetLocalIPAddress() As String
        Try
            Dim host As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            For Each ip As System.Net.IPAddress In host.AddressList
                If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
            Return "127.0.0.1"
        Catch ex As Exception
            Debug.WriteLine("[DatabaseHelper.GetLocalIPAddress] Error: " & ex.ToString())
            Return "127.0.0.1"
        End Try
    End Function

    ' =============================================
    ' GET KODE PRODUK BY PRODUCT ID
    ' =============================================
    Public Shared Function GetKodeProdukByID(productID As Integer) As String
        Try
            Dim query As String = "SELECT KodeProduk FROM Products WHERE ProductID = @ProductID"
            Dim result As Object = ExecuteScalar(query, {New SqlParameter("@ProductID", productID)})
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return result.ToString().Trim()
            End If
        Catch ex As Exception
            Debug.WriteLine($"[GetKodeProdukByID] Error: {ex.Message}")
        End Try
        Return ""
    End Function

    ' =============================================
    ' GET KODE PRODUK BY PRODUCT NAME (FALLBACK)
    ' =============================================
    Public Shared Function GetKodeProdukByName(productName As String) As String
        Try
            Dim query As String = "SELECT KodeProduk FROM Products WHERE NamaProduk = @NamaProduk"
            Dim result As Object = ExecuteScalar(query, {New SqlParameter("@NamaProduk", productName)})
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return result.ToString().Trim()
            End If
        Catch ex As Exception
            Debug.WriteLine($"[GetKodeProdukByName] Error: {ex.Message}")
        End Try
        Return ""
    End Function

    ' =============================================
    ' GENERATE NO TIKET OTOMATIS BERDASARKAN PRODUK
    ' Format: [KODE]-[YY]-[MM]-[DD]-[URUTAN 3 DIGIT]
    ' Contoh: CPO-26-02-14-001, NTTN-26-02-14-001
    ' Urutan reset per produk per hari
    ' TANPA STORED PROCEDURE - LANGSUNG QUERY
    ' =============================================
    Public Shared Function GenerateNoTiketByProduct(kodeProduk As String) As String
        Try
            If String.IsNullOrWhiteSpace(kodeProduk) Then Return ""

            Dim today As Date = Date.Today
            Dim yy As String = today.ToString("yy")
            Dim mm As String = today.ToString("MM")
            Dim dd As String = today.ToString("dd")

            ' Prefix untuk hari ini, misal: CPO-26-02-14-
            Dim prefix As String = $"{kodeProduk}-{yy}-{mm}-{dd}-"

            ' Cari nomor urut terakhir hari ini untuk produk ini
            Dim query As String = "SELECT ISNULL(MAX(CAST(RIGHT(NoTiket, 3) AS INT)), 0) " &
                                  "FROM Timbangan " &
                                  "WHERE NoTiket LIKE @Prefix + '%' " &
                                  "AND CONVERT(date, CreatedAt) = CONVERT(date, GETDATE())"

            Dim result As Object = ExecuteScalar(query, {New SqlParameter("@Prefix", prefix)})

            Dim lastNumber As Integer = 0
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                lastNumber = CInt(result)
            End If

            Dim nextNumber As Integer = lastNumber + 1
            Dim noTiket As String = $"{prefix}{nextNumber.ToString("D3")}"

            Debug.WriteLine($"[GenerateNoTiketByProduct] Kode: {kodeProduk}, Last: {lastNumber}, New: {noTiket}")

            Return noTiket

        Catch ex As Exception
            Debug.WriteLine($"[GenerateNoTiketByProduct] Error: {ex.Message}")
            Dim fallback As String = $"{kodeProduk}-{Date.Today:yy-MM-dd}-001"
            Return fallback
        End Try
    End Function

    ' =============================================
    ' VALIDASI NO DO - Return error message jika duplikat
    ' Return empty string jika valid
    ' =============================================
    Public Shared Function ValidateNoDO(noDO As String, Optional excludeTimbangID As Integer = 0) As String
        Try
            If String.IsNullOrWhiteSpace(noDO) Then Return ""

            Dim query As String = "SELECT COUNT(*) FROM Timbangan WHERE NoDO = @NoDO"

            If excludeTimbangID > 0 Then
                query &= " AND TimbangID <> @ExcludeID"
            End If

            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@NoDO", noDO.Trim()))
            If excludeTimbangID > 0 Then
                params.Add(New SqlParameter("@ExcludeID", excludeTimbangID))
            End If

            Dim result As Object = ExecuteScalar(query, params.ToArray())
            Dim count As Integer = If(result IsNot Nothing, CInt(result), 0)

            If count > 0 Then
                Return $"No. DO '{noDO}' sudah digunakan oleh tiket lain!" & vbCrLf &
                       "Silakan gunakan nomor DO yang berbeda."
            End If

        Catch ex As Exception
            Debug.WriteLine($"[ValidateNoDO] Error: {ex.Message}")
        End Try

        Return ""
    End Function

    ' =============================================
    ' CEK APAKAH NO DO SUDAH ADA (Return Boolean)
    ' =============================================
    Public Shared Function IsNoDOExists(noDO As String, Optional excludeTimbangID As Integer = 0) As Boolean
        Try
            If String.IsNullOrWhiteSpace(noDO) Then Return False

            Dim query As String = "SELECT COUNT(*) FROM Timbangan WHERE NoDO = @NoDO"

            If excludeTimbangID > 0 Then
                query &= " AND TimbangID <> @ExcludeID"
            End If

            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@NoDO", noDO.Trim()))
            If excludeTimbangID > 0 Then
                params.Add(New SqlParameter("@ExcludeID", excludeTimbangID))
            End If

            Dim result As Object = ExecuteScalar(query, params.ToArray())
            Dim count As Integer = If(result IsNot Nothing, CInt(result), 0)

            Return count > 0

        Catch ex As Exception
            Debug.WriteLine($"[IsNoDOExists] Error: {ex.Message}")
            Return False
        End Try
    End Function

End Class