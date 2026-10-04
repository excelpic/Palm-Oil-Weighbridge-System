' =============================================
' FormLogin.vb (Dark Industrial Theme)
' =============================================

Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices ' Untuk fungsi Drag

Public Class FormLogin

    ' =============================================
    ' DRAG FORM LOGIC (Untuk Form Borderless)
    ' =============================================
    <DllImport("user32.dll")>
    Private Shared Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = 2

    Private Sub DragForm(sender As Object, e As MouseEventArgs) Handles pnlMain.MouseDown, lblAppTitle.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        End If
    End Sub

    ' =============================================
    ' VISUAL EFFECTS: Input Focus Animation
    ' =============================================
    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Login - Sistem Timbangan PKS"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.None

        ' Setup Initial UI State
        txtUsername.Focus()
        lineUser.BackColor = Color.FromArgb(0, 200, 83) ' Neon Green

        ' Cek koneksi dan setup database
        CheckDatabaseConnection()
    End Sub

    ' Ubah warna garis bawah saat Textbox Username dipilih
    Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
        lineUser.BackColor = Color.FromArgb(0, 200, 83) ' Hijau Neon
        lineUser.Height = 2
    End Sub

    Private Sub txtUsername_Leave(sender As Object, e As EventArgs) Handles txtUsername.Leave
        lineUser.BackColor = Color.DimGray
        lineUser.Height = 1
    End Sub

    ' Ubah warna garis bawah saat Textbox Password dipilih
    Private Sub txtPassword_Enter(sender As Object, e As EventArgs) Handles txtPassword.Enter
        linePass.BackColor = Color.FromArgb(0, 200, 83) ' Hijau Neon
        linePass.Height = 2
    End Sub

    Private Sub txtPassword_Leave(sender As Object, e As EventArgs) Handles txtPassword.Leave
        linePass.BackColor = Color.DimGray
        linePass.Height = 1
    End Sub

    ' =============================================
    ' LOGIKA BISNIS (TIDAK BERUBAH)
    ' =============================================

    ' CEK KONEKSI DATABASE
    Private Sub CheckDatabaseConnection()
        lblStatus.Text = "Status: Memeriksa koneksi..."
        lblStatus.ForeColor = Color.Orange
        Application.DoEvents()

        Dim errorMsg As String = ""
        Dim connectionFailure As DatabaseHelper.DatabaseConnectionFailureType = DatabaseHelper.DatabaseConnectionFailureType.Unknown

        If DatabaseHelper.TestConnection(errorMsg, connectionFailure) Then
            lblStatus.Text = "✓ SYSTEM ONLINE"
            lblStatus.ForeColor = Color.FromArgb(0, 200, 83)
            btnLogin.Enabled = True
            CheckAndSetupDatabase()
        Else
            lblStatus.Text = "⚠ CONNECTION FAILED"
            lblStatus.ForeColor = Color.Crimson
            btnLogin.Enabled = False

            If connectionFailure = DatabaseHelper.DatabaseConnectionFailureType.DatabaseNotFound Then
                Dim result = MessageBox.Show(
                    "Database tidak ditemukan atau belum dibuat." & vbCrLf & vbCrLf &
                    "Apakah ini instalasi pertama kali?" & vbCrLf &
                    "Klik YES untuk setup database secara otomatis." & vbCrLf &
                    "Klik NO untuk mengatur koneksi manual.",
                    "Database Tidak Ditemukan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    SetupDatabaseAuto()
                Else
                    MessageBox.Show("Klik tombol ⚙ untuk mengatur server database.",
                                  "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Tidak dapat terhubung ke database." & vbCrLf & vbCrLf &
                              errorMsg & vbCrLf & vbCrLf &
                              "Klik tombol ⚙ untuk mengatur server database.",
                              "Koneksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    ' CEK DAN SETUP DATABASE JIKA BELUM ADA TABEL
    Private Sub CheckAndSetupDatabase()
        Try
            If Not DatabaseSetup.IsDatabaseSetup() Then
                lblStatus.Text = "⚠ INITIALIZING DATABASE..."
                lblStatus.ForeColor = Color.Orange
                Application.DoEvents()

                Dim result = MessageBox.Show(
                "Database terhubung tapi tabel belum dibuat." & vbCrLf & vbCrLf &
                "Apakah ini instalasi pertama kali di komputer ini?" & vbCrLf &
                "Klik YES untuk membuat tabel secara otomatis.",
                "Setup Database",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    SetupDatabaseAuto()
                Else
                    lblStatus.Text = "⚠ SETUP REQUIRED"
                    lblStatus.ForeColor = Color.Orange
                    btnLogin.Enabled = False
                End If
            Else
                ' =============================================
                ' JALANKAN MIGRATION OTOMATIS SETIAP KALI
                ' DATABASE SUDAH SIAP (TABEL SUDAH ADA)
                ' =============================================
                DatabaseMigration.RunMigrations()

                If Not DatabaseSetup.HasUsers() Then
                    PrepareInitialAdminSetup()
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine("CheckAndSetupDatabase error: " & ex.ToString())
            _initialSetupRequired = False
            btnLogin.Enabled = False
            lblStatus.Text = "⚠ DATABASE CHECK FAILED"
            lblStatus.ForeColor = Color.Crimson
        End Try
    End Sub

    ' SETUP DATABASE OTOMATIS
    Private Sub SetupDatabaseAuto()
        Try
            Me.Cursor = Cursors.WaitCursor
            btnLogin.Enabled = False
            btnSetting.Enabled = False
            lblStatus.Text = "INSTALLING..."
            lblStatus.ForeColor = Color.Cyan
            Application.DoEvents()

            Dim serverName As String = ""
            Dim dbName As String = ""
            Dim useWindowsAuth As Boolean = True
            Dim sqlUsername As String = ""
            Dim sqlPassword As String = ""

            DatabaseHelper.GetSavedConnectionSettings(
                serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword)

            Dim errorMsg As String = ""
            If DatabaseSetup.SetupDatabase(serverName, dbName, useWindowsAuth,
                                           sqlUsername, sqlPassword, errorMsg) Then
                lblStatus.Text = "✓ SYSTEM READY"
                lblStatus.ForeColor = Color.FromArgb(0, 200, 83)
                btnLogin.Enabled = True

                MessageBox.Show(
                    "Database setup completed successfully." & vbCrLf & vbCrLf &
                    "No default credentials are created by the application." & vbCrLf &
                    "Create the initial administrator account using the login form.",
                    "Setup Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)

                PrepareInitialAdminSetup()
            Else
                lblStatus.Text = "✗ SETUP FAILED"
                lblStatus.ForeColor = Color.Crimson
                btnLogin.Enabled = False

                MessageBox.Show(
                    "❌ Gagal setup database:" & vbCrLf & vbCrLf & errorMsg,
                    "Setup Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            lblStatus.Text = "✗ ERROR"
            lblStatus.ForeColor = Color.Crimson
            Debug.WriteLine("[FormLogin.SetupDatabase] Error: " & ex.ToString())
            MessageBox.Show("Terjadi kesalahan saat menyiapkan database. Silakan periksa pengaturan koneksi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            btnSetting.Enabled = True
        End Try
    End Sub

    ' PREPARE FIRST-RUN ADMIN ACCOUNT SETUP
    Private Sub PrepareInitialAdminSetup()
        _initialSetupRequired = True
        lblStatus.Text = "⚠ INITIAL ADMIN SETUP REQUIRED"
        lblStatus.ForeColor = Color.Orange
        btnLogin.Enabled = True
        btnLogin.Text = "CREATE ADMIN"
        txtUsername.Clear()
        txtPassword.Clear()
        txtUsername.Focus()
    End Sub

    ' CREATE THE FIRST ADMINISTRATOR ACCOUNT WITHOUT EMBEDDED CREDENTIALS
    Private Function CreateInitialAdminAccount(username As String, password As String) As Boolean
        If username.Length < 3 OrElse username.Length > 50 Then
            MessageBox.Show("Username must contain between 3 and 50 characters.",
                            "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Return False
        End If

        Dim passwordValidationMessage As String = String.Empty
        If Not PasswordHasher.ValidatePassword(password, passwordValidationMessage) Then
            MessageBox.Show(passwordValidationMessage,
                            "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Return False
        End If

        Try
            Dim query As String = "INSERT INTO Users (Username, PasswordHash, NamaLengkap, Role, IsActive, AllowedTransType) " &
                                  "VALUES (@Username, @Password, @NamaLengkap, 'Direktur', 1, 'SEMUA')"

            Dim params As SqlParameter() = {
                New SqlParameter("@Username", username),
                New SqlParameter("@Password", PasswordHasher.HashPassword(password)),
                New SqlParameter("@NamaLengkap", username)
            }

            Dim rowsAffected As Integer = DatabaseHelper.ExecuteNonQuery(query, params)
            If rowsAffected <= 0 Then
                Debug.WriteLine("CreateInitialAdminAccount: no rows were inserted.")
                MessageBox.Show("Unable to create the administrator account. Please verify the database configuration and try again.",
                                "Account Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            _initialSetupRequired = False
            btnLogin.Text = "LOGIN"
            lblStatus.Text = "✓ ADMIN ACCOUNT READY"
            lblStatus.ForeColor = Color.FromArgb(0, 200, 83)
            txtPassword.Clear()

            MessageBox.Show(
                "Administrator account created successfully." & vbCrLf & vbCrLf &
                "Use the credentials you just created to sign in.",
                "Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtPassword.Focus()
            Return True

        Catch ex As SqlException
            If ex.Number = 2601 OrElse ex.Number = 2627 Then
                MessageBox.Show("That username is already in use. Please choose another username.",
                                "Username Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Debug.WriteLine("CreateInitialAdminAccount SQL error: " & ex.ToString())
                MessageBox.Show("Unable to create the administrator account. Please verify the database configuration and try again.",
                                "Account Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        Catch ex As Exception
            MessageBox.Show("Unable to create the administrator account.",
                            "Account Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine("CreateInitialAdminAccount error: " & ex.Message)
            Return False
        End Try
    End Function

    ' TOMBOL LOGIN
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text

        If String.IsNullOrEmpty(username) Then
            lineUser.BackColor = Color.Red
            txtUsername.Focus()
            Return
        End If

        If String.IsNullOrEmpty(password) Then
            linePass.BackColor = Color.Red
            txtPassword.Focus()
            Return
        End If

        If _initialSetupRequired Then
            btnLogin.Enabled = False
            btnLogin.Text = "CREATING..."
            Application.DoEvents()

            Try
                CreateInitialAdminAccount(username, password)
            Finally
                If _initialSetupRequired Then
                    btnLogin.Enabled = True
                    btnLogin.Text = "CREATE ADMIN"
                Else
                    btnLogin.Enabled = True
                End If
            End Try
            Return
        End If

        btnLogin.Enabled = False
        btnLogin.Text = "WAIT..."
        Application.DoEvents()

        Try
            Dim query As String = "SELECT UserID, Username, NamaLengkap, Role, AllowedTransType, PasswordHash FROM Users " &
                                  "WHERE Username = @Username AND IsActive = 1"

            Dim params As SqlParameter() = {
                New SqlParameter("@Username", username)
            }

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, params)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim storedHash As String = If(IsDBNull(row("PasswordHash")), String.Empty, row("PasswordHash").ToString())
                Dim needsRehash As Boolean = False

                If PasswordHasher.VerifyPassword(storedHash, password, needsRehash) Then
                    UserSession.UserID = Convert.ToInt32(row("UserID"))
                    UserSession.Username = row("Username").ToString()
                    UserSession.Role = row("Role").ToString()
                    UserSession.NamaLengkap = row("NamaLengkap").ToString()

                    If Not IsDBNull(row("AllowedTransType")) AndAlso Not String.IsNullOrEmpty(row("AllowedTransType").ToString()) Then
                        UserSession.AllowedTransType = row("AllowedTransType").ToString()
                    Else
                        UserSession.AllowedTransType = "SEMUA"
                    End If

                    ' Transparently upgrade legacy/older password hashes after successful authentication.
                    If needsRehash OrElse PasswordHasher.IsLegacySha256(storedHash) Then
                        Try
                            Dim upgradeQuery As String = "UPDATE Users SET PasswordHash = @Password, UpdatedAt = GETDATE() WHERE UserID = @ID"
                            DatabaseHelper.ExecuteNonQuery(upgradeQuery, {
                                New SqlParameter("@Password", PasswordHasher.HashPassword(password)),
                                New SqlParameter("@ID", UserSession.UserID)
                            })
                        Catch exUpgrade As Exception
                            ' A failed upgrade must not block a valid login.
                            Debug.WriteLine("Password hash upgrade failed: " & exUpgrade.Message)
                        End Try
                    End If

                    ' Audit Log - Login Berhasil
                    Try
                        DatabaseHelper.InsertAuditLog(
                            UserSession.UserID,
                            "LOGIN",
                            Nothing,
                            "Users",
                            UserSession.UserID,
                            Nothing,
                            Nothing,
                            $"User {UserSession.Username} Login Success"
                        )
                    Catch exAudit As Exception
                        Debug.WriteLine("Error Audit Log: " & exAudit.Message)
                    End Try

                    Dim frmDashboard As New FormDashboard()
                    frmDashboard.Show()
                    Me.Hide()
                Else
                    ' Audit Log - Login Gagal
                    Try
                        DatabaseHelper.InsertAuditLog(
                            0,
                            "LOGIN_FAILED",
                            Nothing,
                            "Users",
                            Nothing,
                            Nothing,
                            Nothing,
                            "Login failed: " & username
                        )
                    Catch exAudit As Exception
                        Debug.WriteLine("Error Audit Log: " & exAudit.Message)
                    End Try

                    MessageBox.Show("Access Denied: Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtPassword.Clear()
                    txtPassword.Focus()
                End If
            Else
                ' Do not reveal whether the username exists.
                Try
                    DatabaseHelper.InsertAuditLog(
                        0,
                        "LOGIN_FAILED",
                        Nothing,
                        "Users",
                        Nothing,
                        Nothing,
                        Nothing,
                        "Login failed: " & username
                    )
                Catch exAudit As Exception
                    Debug.WriteLine("Error Audit Log: " & exAudit.Message)
                End Try

                MessageBox.Show("Access Denied: Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Clear()
                txtPassword.Focus()
            End If

        Catch ex As Exception
            Debug.WriteLine("Login error: " & ex.ToString())
            MessageBox.Show("Unable to complete the sign-in request. Please verify the application configuration and try again.", "Sign-In Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnLogin.Enabled = True
            btnLogin.Text = "LOGIN"
        End Try
    End Sub

    ' TOMBOL KELUAR
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MessageBox.Show("Shut down system?", "Confirm Exit",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    ' TOMBOL SETTING
    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        Dim currentServer As String = DatabaseHelper.ServerName

        Dim newServer As String = InputBox("SERVER CONFIGURATION:" & vbCrLf & vbCrLf &
                                          "Format Examples:" & vbCrLf &
                                          "• .\SQLEXPRESS" & vbCrLf &
                                          "• SERVERNAME\INSTANCE",
                                          "Database Config", currentServer)

        If Not String.IsNullOrEmpty(newServer) Then
            DatabaseHelper.SaveConnectionConfig(newServer, "sistemTimbanganPKS")
            CheckDatabaseConnection()
        End If
    End Sub

    ' KEY PRESS
    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            txtPassword.Focus()
            e.Handled = True
        End If
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            btnLogin.PerformClick()
            e.Handled = True
        End If
    End Sub


End Class