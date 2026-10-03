' =============================================
' FormManajemenUser.vb
' Manajemen User: Tambah, Edit, Hapus, Reset Password
' DIPERBAIKI: 
' 1. Tambah AllowedTransType
' 2. Fix error hapus user (Foreign Key AuditLog)
' =============================================
Imports System.Data.SqlClient

Public Class FormManajemenUser

    Private currentUserRole As String = ""

    ' =============================================
    ' FORM LOAD
    ' =============================================
    Private Sub FormManajemenUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        currentUserRole = UserSession.Role
        SetButtonVisibility()
        LoadData()

        Try
            DatabaseHelper.InsertAuditLog(
                UserSession.UserID,
                "OPEN_FORM",
                Nothing,
                "FormManajemenUser",
                Nothing,
                Nothing,
                Nothing,
                "User membuka Form Manajemen User"
            )
        Catch ex As Exception
            Debug.WriteLine($"[AuditLog] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' ATUR VISIBILITAS TOMBOL BERDASARKAN ROLE
    ' =============================================
    Private Sub SetButtonVisibility()
        Select Case currentUserRole.ToLowerInvariant()
            Case "programmer"
                btnTambah.Visible = True
                btnEdit.Visible = True
                btnHapus.Visible = True
                btnResetPassword.Visible = True

            Case "direktur"
                btnTambah.Visible = True
                btnEdit.Visible = True
                btnHapus.Visible = True
                btnResetPassword.Visible = True

            Case "manager"
                btnTambah.Visible = True
                btnEdit.Visible = True
                btnHapus.Visible = False
                btnResetPassword.Visible = True

            Case "krani"
                btnTambah.Visible = False
                btnEdit.Visible = True
                btnEdit.Text = "✏️ Edit Profil"
                btnHapus.Visible = False
                btnResetPassword.Visible = True
                btnResetPassword.Text = "🔐 Ubah Password"

            Case Else
                btnTambah.Visible = False
                btnEdit.Visible = False
                btnHapus.Visible = False
                btnResetPassword.Visible = False
        End Select
    End Sub

    ' =============================================
    ' LOAD DATA KE DATAGRIDVIEW - DITAMBAH AllowedTransType
    ' =============================================
    Private Sub LoadData()
        Try
            Dim query As String = "SELECT UserID, Username, NamaLengkap, Role, AllowedTransType, IsActive FROM Users WHERE Username <> 'programmer'"
            Dim parameters As New List(Of SqlParameter)

            Select Case currentUserRole.ToLowerInvariant()
                Case "krani"
                    query &= " AND UserID = @CurrentUserID"
                    parameters.Add(New SqlParameter("@CurrentUserID", UserSession.UserID))
                Case "manager"
                    query &= " AND Role IN ('Krani', 'Manager')"
                Case "direktur"
                    query &= " AND Role IN ('Krani', 'Manager', 'Direktur')"
                Case "programmer"
                    ' Programmer bisa lihat semua.
                Case Else
                    query &= " AND 1=0"
            End Select

            query &= " ORDER BY Role, NamaLengkap"

            dgvUser.DataSource = DatabaseHelper.ExecuteQuery(query, parameters.ToArray())

            If dgvUser.Columns.Count > 0 Then
                dgvUser.Columns("UserID").Visible = False
                dgvUser.Columns("Username").HeaderText = "USERNAME"
                dgvUser.Columns("NamaLengkap").HeaderText = "NAMA LENGKAP"
                dgvUser.Columns("Role").HeaderText = "HAK AKSES"
                dgvUser.Columns("AllowedTransType").HeaderText = "AKSES DATA"
                dgvUser.Columns("IsActive").HeaderText = "AKTIF"

                ' Atur lebar kolom
                dgvUser.Columns("Username").Width = 120
                dgvUser.Columns("NamaLengkap").Width = 180
                dgvUser.Columns("Role").Width = 100
                dgvUser.Columns("AllowedTransType").Width = 100
                dgvUser.Columns("IsActive").Width = 60

                If currentUserRole.ToLowerInvariant() = "programmer" OrElse currentUserRole.ToLowerInvariant() = "direktur" Then
                    SetColumnsReadOnly(dgvUser, "IsActive")
                Else
                    For Each col As DataGridViewColumn In dgvUser.Columns
                        col.ReadOnly = True
                    Next
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormManajemenUser.LoadData] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat data user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' FUNGSI: KUNCI KOLOM KECUALI YANG DITENTUKAN
    ' =============================================
    Private Sub SetColumnsReadOnly(dgv As DataGridView, editableColumn As String)
        For Each col As DataGridViewColumn In dgv.Columns
            col.ReadOnly = (col.Name <> editableColumn)
        Next
    End Sub

    ' =============================================
    ' TOMBOL REFRESH
    ' =============================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtCari.Clear()
        LoadData()
    End Sub

    ' =============================================
    ' TOMBOL CARI
    ' =============================================
    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        FilterData(txtCari.Text.Trim())
    End Sub

    ' =============================================
    ' FILTER DATA BERDASARKAN PENCARIAN
    ' =============================================
    Private Sub FilterData(keyword As String)
        Try
            Dim query As String = "SELECT UserID, Username, NamaLengkap, Role, AllowedTransType, IsActive FROM Users WHERE (Username LIKE @keyword OR NamaLengkap LIKE @keyword) AND Username <> 'programmer'"

            Select Case currentUserRole.ToLowerInvariant()
                Case "krani"
                    query &= " AND Role = 'Krani'"
                Case "manager"
                    query &= " AND Role IN ('Krani', 'Manager')"
                Case "direktur"
                    query &= " AND Role IN ('Krani', 'Manager', 'Direktur')"
                Case "programmer"
                    ' Programmer bisa lihat semua
                Case Else
                    query &= " AND 1=0"
            End Select

            query &= " ORDER BY NamaLengkap"

            Dim searchParam As String = "%" & keyword & "%"
            dgvUser.DataSource = DatabaseHelper.ExecuteQuery(query, {New SqlParameter("@keyword", searchParam)})

            If dgvUser.Columns.Count > 0 Then
                dgvUser.Columns("UserID").Visible = False

                If currentUserRole.ToLowerInvariant() = "programmer" OrElse currentUserRole.ToLowerInvariant() = "direktur" Then
                    SetColumnsReadOnly(dgvUser, "IsActive")
                Else
                    For Each col As DataGridViewColumn In dgvUser.Columns
                        col.ReadOnly = True
                    Next
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormManajemenUser.Filter] Error: " & ex.ToString())
            MessageBox.Show("Gagal memfilter data user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL TAMBAH USER
    ' =============================================
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If currentUserRole.ToLowerInvariant() <> "programmer" AndAlso currentUserRole.ToLowerInvariant() <> "direktur" AndAlso currentUserRole.ToLowerInvariant() <> "manager" Then
            MessageBox.Show("Anda tidak memiliki izin untuk menambah user baru.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ShowUserForm(0, "", "", "Krani", "SEMUA")
    End Sub

    ' =============================================
    ' TOMBOL EDIT
    ' =============================================
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dgvUser.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih user yang ingin diedit.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row = dgvUser.SelectedRows(0)
        Dim selectedUserID As Integer = CInt(row.Cells("UserID").Value)
        Dim userRole As String = row.Cells("Role").Value.ToString()

        If currentUserRole.ToLowerInvariant() = "krani" Then
            If selectedUserID <> UserSession.UserID Then
                MessageBox.Show("Anda hanya bisa mengedit profil sendiri!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Else
            If Not CanManageRole(userRole) Then
                MessageBox.Show("Anda tidak memiliki izin untuk mengedit user dengan role ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        Dim id As Integer = CInt(row.Cells("UserID").Value)
        Dim username As String = row.Cells("Username").Value.ToString()
        Dim namaLengkap As String = row.Cells("NamaLengkap").Value.ToString()
        Dim role As String = row.Cells("Role").Value.ToString()
        Dim allowedTransType As String = If(row.Cells("AllowedTransType").Value IsNot Nothing AndAlso
                                            Not IsDBNull(row.Cells("AllowedTransType").Value),
                                            row.Cells("AllowedTransType").Value.ToString(), "SEMUA")

        ShowUserForm(id, username, namaLengkap, role, allowedTransType)
    End Sub

    ' =============================================
    ' TOMBOL HAPUS - DIPERBAIKI UNTUK HANDLE FOREIGN KEY
    ' =============================================
    ' =============================================
    ' TOMBOL HAPUS - CEK AUDIT LOG DULU SEBELUM HAPUS
    ' =============================================
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If dgvUser.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih user yang ingin dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row = dgvUser.SelectedRows(0)
        Dim userRole As String = row.Cells("Role").Value.ToString()

        If Not CanManageRole(userRole) Then
            MessageBox.Show("Anda tidak memiliki izin untuk menghapus user dengan role ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer = CInt(row.Cells("UserID").Value)
        Dim username As String = row.Cells("Username").Value.ToString()

        ' Cegah hapus user sistem
        If username.ToLowerInvariant() = "programmer" OrElse username.ToLowerInvariant() = "admin" Then
            MessageBox.Show("User sistem tidak dapat dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Cegah hapus diri sendiri
        If id = UserSession.UserID Then
            MessageBox.Show("Anda tidak dapat menghapus akun Anda sendiri!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' CEK DULU: Apakah user punya data di AuditLog?
        Dim checkQuery As String = "SELECT COUNT(*) FROM AuditLog WHERE UserID = @ID"
        Dim auditCount As Object = DatabaseHelper.ExecuteScalar(checkQuery, {New SqlParameter("@ID", id)})
        Dim count As Integer = 0
        If auditCount IsNot Nothing Then count = CInt(auditCount)

        If count > 0 Then
            ' PERINGATAN KERAS UNTUK FORCE DELETE
            Dim result = MessageBox.Show(
            $"User '{username}' memiliki {count} data riwayat di AuditLog." & vbCrLf & vbCrLf &
            "Jika dihapus, SEMUA RIWAYAT AKTIVITAS user ini juga akan hilang permanen." & vbCrLf & vbCrLf &
            "Yakin ingin PAKSA HAPUS user ini beserta seluruh datanya?",
            "Peringatan Hapus Permanen",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                Try
                    ' 1. HAPUS DULU DATANYA DI AUDIT LOG (Supaya tidak error Foreign Key)
                    Dim deleteLogQuery As String = "DELETE FROM AuditLog WHERE UserID = @ID"
                    DatabaseHelper.ExecuteNonQuery(deleteLogQuery, {New SqlParameter("@ID", id)})

                    ' 2. BARU HAPUS USERNYA
                    Dim deleteUserQuery As String = "DELETE FROM Users WHERE UserID = @ID"
                    Dim deleteSuccess As Integer = DatabaseHelper.ExecuteNonQuery(deleteUserQuery, {New SqlParameter("@ID", id)})

                    If deleteSuccess > 0 Then
                        MessageBox.Show($"User '{username}' dan semua riwayatnya berhasil dihapus permanen.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadData()
                    End If
                Catch ex As Exception
                    Debug.WriteLine("[FormManajemenUser.Delete] Error: " & ex.ToString())
                    MessageBox.Show("Gagal menghapus user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Else
            ' Hapus biasa (tidak ada audit log)
            Dim confirmResult = MessageBox.Show(
            $"Yakin hapus user '{username}' secara permanen?",
            "Konfirmasi Hapus",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If confirmResult = DialogResult.Yes Then
                Try
                    Dim deleteQuery As String = "DELETE FROM Users WHERE UserID = @ID"
                    Dim deleteSuccess As Integer = DatabaseHelper.ExecuteNonQuery(deleteQuery, {New SqlParameter("@ID", id)})

                    If deleteSuccess > 0 Then
                        MessageBox.Show($"User '{username}' berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadData()
                    End If
                Catch ex As Exception
                    Debug.WriteLine("[FormManajemenUser] Error: " & ex.ToString())
                    MessageBox.Show("Operasi user gagal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    ' =============================================
    ' TOMBOL RESET PASSWORD
    ' =============================================
    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click
        If dgvUser.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih user yang ingin direset password-nya.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row = dgvUser.SelectedRows(0)
        Dim selectedUserID As Integer = CInt(row.Cells("UserID").Value)
        Dim userRole As String = row.Cells("Role").Value.ToString()
        Dim username As String = row.Cells("Username").Value.ToString()

        If currentUserRole.ToLowerInvariant() = "krani" Then
            If selectedUserID <> UserSession.UserID Then
                MessageBox.Show("Anda hanya bisa mengubah password sendiri!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ShowChangeOwnPasswordForm(selectedUserID, username)
        Else
            If Not CanManageRole(userRole) Then
                MessageBox.Show("Anda tidak memiliki izin untuk mereset password user dengan role ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ShowResetPasswordForm(selectedUserID, username)
        End If
    End Sub

    ' =============================================
    ' FORM POPUP: RESET PASSWORD
    ' =============================================
    Private Sub ShowResetPasswordForm(userId As Integer, username As String)
        Dim frm As New Form()
        frm.Text = "Reset Password - " & username
        frm.Size = New Size(400, 250)
        frm.StartPosition = FormStartPosition.CenterParent
        frm.FormBorderStyle = FormBorderStyle.FixedDialog
        frm.MaximizeBox = False
        frm.MinimizeBox = False
        frm.BackColor = Color.FromArgb(245, 245, 250)
        frm.Font = New Font("Segoe UI", 10)

        Dim lblInfo As New Label() With {
            .Text = "Masukkan password baru untuk user: " & username,
            .Location = New Point(30, 20),
            .Size = New Size(340, 20),
            .Font = New Font("Segoe UI", 10)
        }

        Dim lblPasswordBaru As New Label() With {
            .Text = "Password Baru:",
            .Location = New Point(30, 60),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        Dim txtPasswordBaru As New TextBox() With {
            .Location = New Point(150, 57),
            .Size = New Size(200, 25),
            .Font = New Font("Segoe UI", 10),
            .UseSystemPasswordChar = True
        }

        Dim lblKonfirmasi As New Label() With {
            .Text = "Konfirmasi:",
            .Location = New Point(30, 100),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        Dim txtKonfirmasi As New TextBox() With {
            .Location = New Point(150, 97),
            .Size = New Size(200, 25),
            .Font = New Font("Segoe UI", 10),
            .UseSystemPasswordChar = True
        }

        Dim chkShowPassword As New CheckBox() With {
            .Text = "Tampilkan Password",
            .Location = New Point(150, 130),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 9)
        }

        Dim btnSimpan As New Button() With {
            .Text = "💾 SIMPAN",
            .Location = New Point(80, 165),
            .Size = New Size(120, 40),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(0, 122, 204),
            .ForeColor = Color.White,
            .Cursor = Cursors.Hand
        }

        Dim btnBatal As New Button() With {
            .Text = "❌ BATAL",
            .Location = New Point(210, 165),
            .Size = New Size(120, 40),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(192, 0, 0),
            .ForeColor = Color.White,
            .Cursor = Cursors.Hand
        }

        AddHandler chkShowPassword.CheckedChanged, Sub(senderChk As Object, eChk As EventArgs)
                                                       txtPasswordBaru.UseSystemPasswordChar = Not chkShowPassword.Checked
                                                       txtKonfirmasi.UseSystemPasswordChar = Not chkShowPassword.Checked
                                                   End Sub

        AddHandler btnSimpan.Click, Sub(senderBtn As Object, eBtn As EventArgs)
                                        If String.IsNullOrWhiteSpace(txtPasswordBaru.Text) Then
                                            MessageBox.Show("Password baru tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtPasswordBaru.Focus()
                                            Return
                                        End If

                                        Dim passwordValidationMessage As String = String.Empty
                                        If Not PasswordHasher.ValidatePassword(txtPasswordBaru.Text, passwordValidationMessage) Then
                                            MessageBox.Show(passwordValidationMessage, "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtPasswordBaru.Focus()
                                            Return
                                        End If

                                        If txtPasswordBaru.Text <> txtKonfirmasi.Text Then
                                            MessageBox.Show("Konfirmasi password tidak cocok!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtKonfirmasi.Focus()
                                            Return
                                        End If

                                        If MessageBox.Show($"Yakin ingin mengubah password user '{username}'?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                            Return
                                        End If

                                        Try
                                            Dim hashedPassword As String = PasswordHasher.HashPassword(txtPasswordBaru.Text)
                                            Dim query As String = "UPDATE Users SET PasswordHash = @Password, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy WHERE UserID = @ID"
                                            Dim result As Integer = DatabaseHelper.ExecuteNonQuery(query, {
                                                New SqlParameter("@Password", hashedPassword),
                                                New SqlParameter("@UpdatedBy", UserSession.UserID),
                                                New SqlParameter("@ID", userId)
                                            })

                                            If result > 0 Then
                                                MessageBox.Show($"Password user '{username}' berhasil diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                frm.Close()
                                            Else
                                                MessageBox.Show("Gagal mengubah password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If

                                        Catch ex As Exception
                                            Debug.WriteLine("[FormManajemenUser] Error: " & ex.ToString())
                    MessageBox.Show("Operasi user gagal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End Try
                                    End Sub

        AddHandler btnBatal.Click, Sub(senderBtn As Object, eBtn As EventArgs)
                                       frm.Close()
                                   End Sub

        AddHandler txtPasswordBaru.KeyDown, Sub(senderTxt As Object, eTxt As KeyEventArgs)
                                                If eTxt.KeyCode = Keys.Enter Then
                                                    txtKonfirmasi.Focus()
                                                    eTxt.SuppressKeyPress = True
                                                End If
                                            End Sub

        AddHandler txtKonfirmasi.KeyDown, Sub(senderTxt As Object, eTxt As KeyEventArgs)
                                              If eTxt.KeyCode = Keys.Enter Then
                                                  btnSimpan.PerformClick()
                                                  eTxt.SuppressKeyPress = True
                                              End If
                                          End Sub

        frm.Controls.AddRange({lblInfo, lblPasswordBaru, txtPasswordBaru, lblKonfirmasi, txtKonfirmasi, chkShowPassword, btnSimpan, btnBatal})

        AddHandler frm.Shown, Sub(senderFrm As Object, eFrm As EventArgs)
                                  txtPasswordBaru.Focus()
                              End Sub

        frm.ShowDialog()
    End Sub

    ' =============================================
    ' FUNGSI CEK APAKAH BISA MANAGE ROLE TERTENTU
    ' =============================================
    Private Function CanManageRole(targetRole As String) As Boolean
        Select Case currentUserRole.ToLowerInvariant()
            Case "programmer"
                Return True
            Case "direktur"
                Return targetRole.ToLowerInvariant() <> "programmer"
            Case "manager"
                Return targetRole.ToLowerInvariant() = "krani" OrElse targetRole.ToLowerInvariant() = "manager"
            Case "krani"
                Return False
            Case Else
                Return False
        End Select
    End Function

    ' =============================================
    ' TOMBOL TUTUP
    ' =============================================
    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Close()
    End Sub

    ' =============================================
    ' TOGGLE ISACTIVE (KLIK CHECKBOX)
    ' =============================================
    Private Sub dgvUser_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUser.CellContentClick
        If e.RowIndex < 0 Then Return
        If dgvUser.Columns(e.ColumnIndex).Name <> "IsActive" Then Return

        Dim userRole As String = dgvUser.Rows(e.RowIndex).Cells("Role").Value.ToString()
        If Not CanManageRole(userRole) Then
            MessageBox.Show("Anda tidak memiliki izin untuk mengubah status user ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim currentValue As Boolean = CBool(dgvUser.Rows(e.RowIndex).Cells("IsActive").Value)
        Dim newValue As Boolean = Not currentValue
        Dim id As Integer = CInt(dgvUser.Rows(e.RowIndex).Cells("UserID").Value)
        Dim username As String = dgvUser.Rows(e.RowIndex).Cells("Username").Value.ToString()

        If username.ToLowerInvariant() = "programmer" AndAlso newValue = False Then
            MessageBox.Show("User 'programmer' tidak dapat dinonaktifkan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim query As String = "UPDATE Users SET IsActive = @IsActive WHERE UserID = @ID"
            Dim result As Integer = DatabaseHelper.ExecuteNonQuery(query, {
                New SqlParameter("@IsActive", newValue),
                New SqlParameter("@ID", id)
            })

            If result > 0 Then
                dgvUser.Rows(e.RowIndex).Cells("IsActive").Value = newValue

                Try
                    DatabaseHelper.InsertAuditLog(
                        UserSession.UserID,
                        "TOGGLE_USER_STATUS",
                        Nothing,
                        "Users",
                        id,
                        $"IsActive: {currentValue}",
                        $"IsActive: {newValue}",
                        $"Ubah status user '{username}' menjadi {If(newValue, "Aktif", "Nonaktif")}"
                    )
                Catch exAudit As Exception
                    Debug.WriteLine($"[AuditLog] Error: {exAudit.Message}")
                End Try
            Else
                MessageBox.Show("Gagal mengubah status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormManajemenUser] Error: " & ex.ToString())
                    MessageBox.Show("Operasi user gagal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' ENTER DI TEXTBOX PENCARIAN
    ' =============================================
    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnCari.PerformClick()
            e.SuppressKeyPress = True
        End If
    End Sub

    ' =============================================
    ' FORM POPUP: TAMBAH/EDIT USER - DITAMBAH AllowedTransType
    ' =============================================
    Private Sub ShowUserForm(id As Integer, username As String, namaLengkap As String, role As String, allowedTransType As String)
        Dim frm As New Form()
        frm.Text = If(id = 0, "Tambah User Baru", "Edit User")
        frm.Size = New Size(450, If(id = 0, 400, 330))
        frm.StartPosition = FormStartPosition.CenterParent
        frm.FormBorderStyle = FormBorderStyle.FixedDialog
        frm.MaximizeBox = False
        frm.MinimizeBox = False
        frm.BackColor = Color.FromArgb(245, 245, 250)
        frm.Font = New Font("Segoe UI", 10)

        Dim yPos As Integer = 25

        ' LABEL & TEXTBOX: USERNAME
        Dim lblUsername As New Label() With {
            .Text = "Username:",
            .Location = New Point(30, yPos),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        Dim txtUsername As New TextBox() With {
            .Text = username,
            .Location = New Point(160, yPos - 3),
            .Size = New Size(240, 25),
            .Font = New Font("Segoe UI", 10),
            .Enabled = (id = 0)
        }
        yPos += 40

        ' LABEL & TEXTBOX: NAMA LENGKAP
        Dim lblNamaLengkap As New Label() With {
            .Text = "Nama Lengkap:",
            .Location = New Point(30, yPos),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        Dim txtNamaLengkap As New TextBox() With {
            .Text = namaLengkap,
            .Location = New Point(160, yPos - 3),
            .Size = New Size(240, 25),
            .Font = New Font("Segoe UI", 10)
        }
        yPos += 40

        ' LABEL & TEXTBOX: PASSWORD (Hanya untuk Tambah Baru)
        Dim lblPassword As New Label() With {
            .Text = "Password:",
            .Location = New Point(30, yPos),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Visible = (id = 0)
        }
        Dim txtPassword As New TextBox() With {
            .Location = New Point(160, yPos - 3),
            .Size = New Size(240, 25),
            .Font = New Font("Segoe UI", 10),
            .UseSystemPasswordChar = True,
            .Visible = (id = 0)
        }
        If id = 0 Then yPos += 40

        ' LABEL & COMBOBOX: ROLE
        Dim lblRole As New Label() With {
            .Text = "Hak Akses:",
            .Location = New Point(30, yPos),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        Dim cmbRole As New ComboBox() With {
            .Location = New Point(160, yPos - 3),
            .Size = New Size(240, 25),
            .Font = New Font("Segoe UI", 10),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }

        Select Case currentUserRole.ToLowerInvariant()
            Case "programmer"
                cmbRole.Items.AddRange({"Programmer", "Direktur", "Manager", "Krani"})
            Case "direktur"
                cmbRole.Items.AddRange({"Direktur", "Manager", "Krani"})
            Case "manager"
                cmbRole.Items.AddRange({"Manager", "Krani"})
            Case Else
                cmbRole.Items.Add("Krani")
        End Select

        If cmbRole.Items.Contains(role) Then
            cmbRole.SelectedItem = role
        ElseIf cmbRole.Items.Count > 0 Then
            cmbRole.SelectedIndex = cmbRole.Items.Count - 1
        End If
        yPos += 40

        ' =============================================
        ' LABEL & COMBOBOX: AKSES DATA (AllowedTransType) - BARU!
        ' =============================================
        Dim lblAksesData As New Label() With {
            .Text = "Akses Data:",
            .Location = New Point(30, yPos),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }
        Dim cmbAksesData As New ComboBox() With {
            .Location = New Point(160, yPos - 3),
            .Size = New Size(240, 25),
            .Font = New Font("Segoe UI", 10),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }
        cmbAksesData.Items.AddRange({"SEMUA", "JUAL", "BELI"})

        ' Set selected item
        If Not String.IsNullOrEmpty(allowedTransType) AndAlso cmbAksesData.Items.Contains(allowedTransType.ToUpperInvariant()) Then
            cmbAksesData.SelectedItem = allowedTransType.ToUpperInvariant()
        Else
            cmbAksesData.SelectedItem = "SEMUA"
        End If

        ' Label keterangan akses data
        Dim lblKetAksesData As New Label() With {
            .Text = "• SEMUA = Lihat semua data" & vbCrLf &
                    "• JUAL = Hanya data penjualan (CPO/Kernel)" & vbCrLf &
                    "• BELI = Hanya data pembelian (TBS)",
            .Location = New Point(160, yPos + 25),
            .Size = New Size(250, 50),
            .Font = New Font("Segoe UI", 8),
            .ForeColor = Color.Gray
        }
        yPos += 85

        ' TOMBOL SIMPAN
        Dim btnSimpan As New Button() With {
            .Text = "💾 SIMPAN",
            .Location = New Point(160, yPos),
            .Size = New Size(115, 40),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(0, 122, 204),
            .ForeColor = Color.White,
            .Cursor = Cursors.Hand
        }

        ' TOMBOL BATAL
        Dim btnBatal As New Button() With {
            .Text = "❌ BATAL",
            .Location = New Point(285, yPos),
            .Size = New Size(115, 40),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(192, 0, 0),
            .ForeColor = Color.White,
            .Cursor = Cursors.Hand
        }

        ' EVENT: TOMBOL SIMPAN
        AddHandler btnSimpan.Click, Sub(senderBtn As Object, eBtn As EventArgs)
                                        ' Validasi
                                        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
                                            MessageBox.Show("Username tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtUsername.Focus()
                                            Return
                                        End If

                                        If String.IsNullOrWhiteSpace(txtNamaLengkap.Text) Then
                                            MessageBox.Show("Nama lengkap tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtNamaLengkap.Focus()
                                            Return
                                        End If

                                        If id = 0 AndAlso String.IsNullOrWhiteSpace(txtPassword.Text) Then
                                            MessageBox.Show("Password tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtPassword.Focus()
                                            Return
                                        End If

                                        If id = 0 Then
                                            Dim passwordValidationMessage As String = String.Empty
                                            If Not PasswordHasher.ValidatePassword(txtPassword.Text, passwordValidationMessage) Then
                                                MessageBox.Show(passwordValidationMessage, "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                txtPassword.Focus()
                                                Return
                                            End If
                                        End If

                                        If cmbRole.SelectedItem Is Nothing Then
                                            MessageBox.Show("Pilih hak akses!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            cmbRole.Focus()
                                            Return
                                        End If

                                        If cmbAksesData.SelectedItem Is Nothing Then
                                            MessageBox.Show("Pilih akses data!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            cmbAksesData.Focus()
                                            Return
                                        End If

                                        Try
                                            Dim query As String
                                            Dim params As SqlParameter()
                                            Dim result As Integer = 0
                                            Dim newUserID As Integer = 0

                                            If id = 0 Then
                                                ' =============================================
                                                ' INSERT (Tambah Baru) - DENGAN AllowedTransType
                                                ' =============================================
                                                Dim checkQuery As String = "SELECT COUNT(*) FROM Users WHERE Username = @Username"
                                                Dim checkResult As Object = DatabaseHelper.ExecuteScalar(checkQuery, {
                                                    New SqlParameter("@Username", txtUsername.Text.Trim())
                                                })

                                                If checkResult IsNot Nothing AndAlso CInt(checkResult) > 0 Then
                                                    MessageBox.Show("Username '" & txtUsername.Text.Trim() & "' sudah digunakan!",
                                                        "Username Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                    txtUsername.Focus()
                                                    txtUsername.SelectAll()
                                                    Return
                                                End If

                                                query = "INSERT INTO Users (Username, PasswordHash, NamaLengkap, Role, AllowedTransType, IsActive, CreatedAt, CreatedBy) " &
                                                        "OUTPUT INSERTED.UserID " &
                                                        "VALUES (@Username, @Password, @NamaLengkap, @Role, @AllowedTransType, 1, GETDATE(), @CreatedBy)"

                                                params = {
                                                    New SqlParameter("@Username", txtUsername.Text.Trim()),
                                                    New SqlParameter("@Password", PasswordHasher.HashPassword(txtPassword.Text)),
                                                    New SqlParameter("@NamaLengkap", txtNamaLengkap.Text.Trim()),
                                                    New SqlParameter("@Role", cmbRole.SelectedItem.ToString()),
                                                    New SqlParameter("@AllowedTransType", cmbAksesData.SelectedItem.ToString()),
                                                    New SqlParameter("@CreatedBy", UserSession.UserID)
                                                }

                                                Dim insertResult As Object = DatabaseHelper.ExecuteScalar(query, params)

                                                If insertResult IsNot Nothing AndAlso Not IsDBNull(insertResult) Then
                                                    newUserID = CInt(insertResult)
                                                    result = 1
                                                Else
                                                    result = 0
                                                End If

                                            Else
                                                ' =============================================
                                                ' UPDATE (Edit) - DENGAN AllowedTransType
                                                ' =============================================
                                                query = "UPDATE Users SET NamaLengkap = @NamaLengkap, Role = @Role, AllowedTransType = @AllowedTransType, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy WHERE UserID = @ID"
                                                params = {
                                                    New SqlParameter("@NamaLengkap", txtNamaLengkap.Text.Trim()),
                                                    New SqlParameter("@Role", cmbRole.SelectedItem.ToString()),
                                                    New SqlParameter("@AllowedTransType", cmbAksesData.SelectedItem.ToString()),
                                                    New SqlParameter("@UpdatedBy", UserSession.UserID),
                                                    New SqlParameter("@ID", id)
                                                }

                                                result = DatabaseHelper.ExecuteNonQuery(query, params)
                                                newUserID = id
                                            End If

                                            If result > 0 Then
                                                Try
                                                    Dim actionType As String = If(id = 0, "INSERT_USER", "UPDATE_USER")
                                                    Dim keterangan As String = If(id = 0,
                                                        $"Tambah user - Username: {txtUsername.Text}, Role: {cmbRole.SelectedItem}, AksesData: {cmbAksesData.SelectedItem}",
                                                        $"Edit user - Username: {username}, Role: {cmbRole.SelectedItem}, AksesData: {cmbAksesData.SelectedItem}")

                                                    DatabaseHelper.InsertAuditLog(
                                                        UserSession.UserID,
                                                        actionType,
                                                        Nothing,
                                                        "Users",
                                                        newUserID,
                                                        If(id = 0, Nothing, $"NamaLengkap: {namaLengkap}, Role: {role}, AksesData: {allowedTransType}"),
                                                        $"NamaLengkap: {txtNamaLengkap.Text}, Role: {cmbRole.SelectedItem}, AksesData: {cmbAksesData.SelectedItem}",
                                                        keterangan
                                                    )
                                                Catch exAudit As Exception
                                                    Debug.WriteLine($"[AuditLog] Error: {exAudit.Message}")
                                                End Try

                                                MessageBox.Show("✅ Data user berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                LoadData()
                                                frm.Close()
                                            Else
                                                MessageBox.Show("Gagal menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If

                                        Catch ex As Exception
                                            If ex.Message.Contains("UNIQUE") OrElse ex.Message.Contains("duplicate") Then
                                                MessageBox.Show("Username '" & txtUsername.Text.Trim() & "' sudah digunakan!",
                                                    "Username Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                txtUsername.Focus()
                                                txtUsername.SelectAll()
                                            Else
                                                Debug.WriteLine("[FormManajemenUser.Database] Error: " & ex.ToString())
                                                MessageBox.Show("Operasi database user gagal.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If
                                        End Try
                                    End Sub

        ' EVENT: TOMBOL BATAL
        AddHandler btnBatal.Click, Sub(senderBtn As Object, eBtn As EventArgs)
                                       frm.Close()
                                   End Sub

        ' EVENT: ENTER DI TEXTBOX
        AddHandler txtUsername.KeyDown, Sub(senderTxt As Object, eTxt As KeyEventArgs)
                                            If eTxt.KeyCode = Keys.Enter Then
                                                txtNamaLengkap.Focus()
                                                eTxt.SuppressKeyPress = True
                                            End If
                                        End Sub

        AddHandler txtNamaLengkap.KeyDown, Sub(senderTxt As Object, eTxt As KeyEventArgs)
                                               If eTxt.KeyCode = Keys.Enter Then
                                                   If id = 0 Then
                                                       txtPassword.Focus()
                                                   Else
                                                       cmbRole.Focus()
                                                   End If
                                                   eTxt.SuppressKeyPress = True
                                               End If
                                           End Sub

        AddHandler txtPassword.KeyDown, Sub(senderTxt As Object, eTxt As KeyEventArgs)
                                            If eTxt.KeyCode = Keys.Enter Then
                                                cmbRole.Focus()
                                                eTxt.SuppressKeyPress = True
                                            End If
                                        End Sub

        AddHandler cmbRole.KeyDown, Sub(senderCmb As Object, eCmb As KeyEventArgs)
                                        If eCmb.KeyCode = Keys.Enter Then
                                            cmbAksesData.Focus()
                                            eCmb.SuppressKeyPress = True
                                        End If
                                    End Sub

        AddHandler cmbAksesData.KeyDown, Sub(senderCmb As Object, eCmb As KeyEventArgs)
                                             If eCmb.KeyCode = Keys.Enter Then
                                                 btnSimpan.PerformClick()
                                                 eCmb.SuppressKeyPress = True
                                             End If
                                         End Sub

        ' TAMBAHKAN KONTROL KE FORM
        frm.Controls.AddRange({lblUsername, txtUsername, lblNamaLengkap, txtNamaLengkap,
                              lblPassword, txtPassword, lblRole, cmbRole,
                              lblAksesData, cmbAksesData, lblKetAksesData,
                              btnSimpan, btnBatal})

        AddHandler frm.Shown, Sub(senderFrm As Object, eFrm As EventArgs)
                                  If id = 0 Then
                                      txtUsername.Focus()
                                  Else
                                      txtNamaLengkap.Focus()
                                  End If
                              End Sub

        frm.ShowDialog()
    End Sub

    ' =============================================
    ' FORM POPUP: UBAH PASSWORD SENDIRI
    ' =============================================
    Private Sub ShowChangeOwnPasswordForm(userId As Integer, username As String)
        Dim frm As New Form()
        frm.Text = "Ubah Password"
        frm.Size = New Size(400, 300)
        frm.StartPosition = FormStartPosition.CenterParent
        frm.FormBorderStyle = FormBorderStyle.FixedDialog
        frm.MaximizeBox = False
        frm.MinimizeBox = False
        frm.BackColor = Color.FromArgb(245, 245, 250)
        frm.Font = New Font("Segoe UI", 10)

        Dim lblInfo As New Label() With {
            .Text = "Ubah password untuk: " & username,
            .Location = New Point(30, 15),
            .Size = New Size(340, 20),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }

        Dim lblPasswordLama As New Label() With {
            .Text = "Password Lama:",
            .Location = New Point(30, 50),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10)
        }
        Dim txtPasswordLama As New TextBox() With {
            .Location = New Point(150, 47),
            .Size = New Size(200, 25),
            .Font = New Font("Segoe UI", 10),
            .UseSystemPasswordChar = True
        }

        Dim lblPasswordBaru As New Label() With {
            .Text = "Password Baru:",
            .Location = New Point(30, 90),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10)
        }
        Dim txtPasswordBaru As New TextBox() With {
            .Location = New Point(150, 87),
            .Size = New Size(200, 25),
            .Font = New Font("Segoe UI", 10),
            .UseSystemPasswordChar = True
        }

        Dim lblKonfirmasi As New Label() With {
            .Text = "Konfirmasi:",
            .Location = New Point(30, 130),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10)
        }
        Dim txtKonfirmasi As New TextBox() With {
            .Location = New Point(150, 127),
            .Size = New Size(200, 25),
            .Font = New Font("Segoe UI", 10),
            .UseSystemPasswordChar = True
        }

        Dim chkShowPassword As New CheckBox() With {
            .Text = "Tampilkan Password",
            .Location = New Point(150, 160),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 9)
        }

        Dim btnSimpan As New Button() With {
            .Text = "💾 SIMPAN",
            .Location = New Point(80, 200),
            .Size = New Size(120, 40),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(0, 122, 204),
            .ForeColor = Color.White,
            .Cursor = Cursors.Hand
        }

        Dim btnBatal As New Button() With {
            .Text = "❌ BATAL",
            .Location = New Point(210, 200),
            .Size = New Size(120, 40),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(192, 0, 0),
            .ForeColor = Color.White,
            .Cursor = Cursors.Hand
        }

        AddHandler chkShowPassword.CheckedChanged, Sub(senderChk As Object, eChk As EventArgs)
                                                       txtPasswordLama.UseSystemPasswordChar = Not chkShowPassword.Checked
                                                       txtPasswordBaru.UseSystemPasswordChar = Not chkShowPassword.Checked
                                                       txtKonfirmasi.UseSystemPasswordChar = Not chkShowPassword.Checked
                                                   End Sub

        AddHandler btnSimpan.Click, Sub(senderBtn As Object, eBtn As EventArgs)
                                        If String.IsNullOrWhiteSpace(txtPasswordLama.Text) Then
                                            MessageBox.Show("Password lama tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtPasswordLama.Focus()
                                            Return
                                        End If

                                        If String.IsNullOrWhiteSpace(txtPasswordBaru.Text) Then
                                            MessageBox.Show("Password baru tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtPasswordBaru.Focus()
                                            Return
                                        End If

                                        Dim passwordValidationMessage As String = String.Empty
                                        If Not PasswordHasher.ValidatePassword(txtPasswordBaru.Text, passwordValidationMessage) Then
                                            MessageBox.Show(passwordValidationMessage, "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtPasswordBaru.Focus()
                                            Return
                                        End If

                                        If txtPasswordBaru.Text <> txtKonfirmasi.Text Then
                                            MessageBox.Show("Konfirmasi password tidak cocok!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            txtKonfirmasi.Focus()
                                            Return
                                        End If

                                        Try
                                            Dim getHashQuery As String = "SELECT PasswordHash FROM Users WHERE UserID = @ID"
                                            Dim storedHash As Object = DatabaseHelper.ExecuteScalar(getHashQuery, {
                                                New SqlParameter("@ID", userId)
                                            })

                                            Dim needsRehash As Boolean = False
                                            If storedHash Is Nothing OrElse IsDBNull(storedHash) OrElse
                                               Not PasswordHasher.VerifyPassword(storedHash.ToString(), txtPasswordLama.Text, needsRehash) Then
                                                MessageBox.Show("Password lama salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                txtPasswordLama.Focus()
                                                txtPasswordLama.SelectAll()
                                                Return
                                            End If

                                            Dim hashedNewPassword As String = PasswordHasher.HashPassword(txtPasswordBaru.Text)
                                            Dim updateQuery As String = "UPDATE Users SET PasswordHash = @Password, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy WHERE UserID = @ID"
                                            Dim result As Integer = DatabaseHelper.ExecuteNonQuery(updateQuery, {
                                                New SqlParameter("@Password", hashedNewPassword),
                                                New SqlParameter("@UpdatedBy", UserSession.UserID),
                                                New SqlParameter("@ID", userId)
                                            })

                                            If result > 0 Then
                                                Try
                                                    DatabaseHelper.InsertAuditLog(
                                                        UserSession.UserID,
                                                        "CHANGE_OWN_PASSWORD",
                                                        Nothing,
                                                        "Users",
                                                        userId,
                                                        Nothing,
                                                        Nothing,
                                                        $"User mengubah password sendiri - Username: {username}"
                                                    )
                                                Catch exAudit As Exception
                                                    Debug.WriteLine($"[AuditLog] Error: {exAudit.Message}")
                                                End Try

                                                MessageBox.Show("✅ Password berhasil diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                frm.Close()
                                            Else
                                                MessageBox.Show("Gagal mengubah password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If

                                        Catch ex As Exception
                                            Debug.WriteLine("[FormManajemenUser.ChangePassword] Error: " & ex.ToString())
                                            MessageBox.Show("Gagal mengubah password user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End Try
                                    End Sub

        AddHandler btnBatal.Click, Sub(senderBtn As Object, eBtn As EventArgs)
                                       frm.Close()
                                   End Sub

        frm.Controls.AddRange({lblInfo, lblPasswordLama, txtPasswordLama, lblPasswordBaru, txtPasswordBaru,
                              lblKonfirmasi, txtKonfirmasi, chkShowPassword, btnSimpan, btnBatal})

        AddHandler frm.Shown, Sub(senderFrm As Object, eFrm As EventArgs)
                                  txtPasswordLama.Focus()
                              End Sub

        frm.ShowDialog()
    End Sub

    ' =============================================
    ' FORM CLOSING - AUDIT LOG
    ' =============================================
    Private Sub FormManajemenUser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            DatabaseHelper.InsertAuditLog(
                UserSession.UserID,
                "CLOSE_FORM",
                Nothing,
                "FormManajemenUser",
                Nothing,
                Nothing,
                Nothing,
                "User menutup Form Manajemen User"
            )
        Catch ex As Exception
            Debug.WriteLine($"[AuditLog] Error: {ex.Message}")
        End Try
    End Sub

End Class
