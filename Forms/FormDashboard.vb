' =============================================
' FormDashboard.vb
' Menu Utama Aplikasi - SUDAH DIPERBAIKI
' =============================================

Public Class FormDashboard

    ' =============================================
    ' FORM LOAD
    ' =============================================
    Private Sub FormDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Tampilkan info user yang login
            lblUser.Text = "Selamat Datang, " & UserSession.NamaLengkap & " (" & UserSession.Role & ")!"
            lblTanggal.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy")
            lblJam.Text = DateTime.Now.ToString("HH:mm:ss")
            LoadNamaPerusahaan()

            ' DEBUG: Tampilkan di Output Window
            Debug.WriteLine("========================================")
            Debug.WriteLine($"[Dashboard] User Login: {UserSession.NamaLengkap}")
            Debug.WriteLine($"[Dashboard] Role: {UserSession.Role}")
            Debug.WriteLine($"[Dashboard] CanInputTimbangan: {UserSession.CanInputTimbangan()}")
            Debug.WriteLine($"[Dashboard] CanEditTimbangan: {UserSession.CanEditTimbangan()}")
            Debug.WriteLine($"[Dashboard] IsDirektur: {UserSession.IsDirektur()}")
            Debug.WriteLine($"[Dashboard] IsManager: {UserSession.IsManager()}")
            Debug.WriteLine($"[Dashboard] IsKrani: {UserSession.IsKrani()}")
            Debug.WriteLine("========================================")

            ' Atur tampilan menu berdasarkan role
            SetupMenuByRole()

            ' Load statistik dashboard
            LoadDashboardStats()

            ' Start timer untuk jam
            TimerJam.Start()
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.Load] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat dashboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ' Center dashboard content
            CenterDashboardContent()

        End Try

    End Sub

    ' =============================================
    ' LOAD NAMA PERUSAHAAN DARI DATABASE
    ' Dipanggil saat form load dan setelah tutup pengaturan
    ' =============================================
    Private Sub LoadNamaPerusahaan()
        Try
            ' Ambil nama perusahaan dari tabel Settings
            Dim result As Object = DatabaseHelper.ExecuteScalar(
            "SELECT SettingValue FROM Settings WHERE SettingKey = 'NamaPerusahaan'")

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Dim namaPerusahaan As String = result.ToString().Trim()

                ' Sesuaikan dengan nama label nama perusahaan di sidebar Anda
                ' Berdasarkan gambar, labelnya menampilkan "Example Palm Oil Mill"
                If Not String.IsNullOrWhiteSpace(namaPerusahaan) Then
                    lblAppName.Text = namaPerusahaan.ToUpperInvariant()
                End If

                Debug.WriteLine($"[Dashboard] Nama Perusahaan: {namaPerusahaan}")
            End If

        Catch ex As Exception
            ' Jika gagal ambil dari DB, biarkan nama default yang sudah ada di designer
            Debug.WriteLine($"[LoadNamaPerusahaan] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' TIMER - Update jam setiap detik
    ' =============================================
    Private Sub TimerJam_Tick(sender As Object, e As EventArgs) Handles TimerJam.Tick
        Try
            lblJam.Text = DateTime.Now.ToString("HH:mm:ss")
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.TimerJam] Error: " & ex.ToString())
        End Try
    End Sub

    ' =============================================
    ' SETUP MENU BERDASARKAN ROLE USER (VERSI LENGKAP)
    ' =============================================
    Private Sub SetupMenuByRole()
        Try
            Dim role As String = UserSession.Role.ToLowerInvariant()

            Select Case role
                Case "direktur"
                    btnInputTimbangan.Visible = False   ' 1. Input (Mati)
                    btnDaftarTimbangan.Visible = True   ' 2. Daftar (Nyala)
                    btnLaporan.Visible = True           ' 3. Laporan (Nyala)
                    btnManajemenUser.Visible = True     ' 4. ManajemenUser (Nyala)
                    btnSettings.Visible = True          ' 5. Setting (Nyala)
                    btnMasterData.Visible = True        ' 6. Master (Nyala)
                    btnAuditLog.Visible = True          ' 7. Audit (Nyala)

                Case "manager"
                    btnInputTimbangan.Visible = False    ' 1. InputData (Mati)
                    btnDaftarTimbangan.Visible = True   ' 2. Daftar (Nyala)
                    btnLaporan.Visible = True           ' 3. Laporan (Nyala)
                    btnManajemenUser.Visible = True     ' 4. ManajemenUser (Nyala)
                    btnSettings.Visible = True          ' 5. Setting (Nyala)
                    btnMasterData.Visible = True        ' 6. Master (Nyala)
                    btnAuditLog.Visible = False          ' 7. Audit (Mati)

                Case "krani"
                    btnInputTimbangan.Visible = True    ' 1. Input (Nyala)
                    btnDaftarTimbangan.Visible = True   ' 2. Daftar (Nyala)
                    btnLaporan.Visible = True           ' 3. Laporan (Nyala)
                    btnManajemenUser.Visible = True     ' 4. ManajemenUser (Nyala)
                    btnSettings.Visible = False         ' 5. Setting (Mati)
                    btnMasterData.Visible = False       ' 6. Master (Mati)
                    btnAuditLog.Visible = False         ' 7. Audit (Mati)

                Case "programmer", "admin"
                    btnInputTimbangan.Visible = True    ' 1. Input (Nyala)
                    btnDaftarTimbangan.Visible = True   ' 2. Daftar (Nyala)
                    btnLaporan.Visible = True           ' 3. Laporan (Nyala)
                    btnManajemenUser.Visible = True     ' 4. ManajemenUser (Nyala)
                    btnSettings.Visible = True          ' 5. Setting (Nyala)
                    btnMasterData.Visible = True        ' 6. Master (Nyala)
                    btnAuditLog.Visible = True          ' 7. Audit (Nyala)
            End Select

        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.SetupMenu] Error: " & ex.ToString())
            MessageBox.Show("Gagal menyiapkan menu aplikasi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' LOAD STATISTIK DASHBOARD
    ' =============================================
    Private Sub LoadDashboardStats()
        Try
            lblStat1Value.Text = "0"
            lblStat2Value.Text = "0 KG"
            lblStat3Value.Text = "0"

            Dim errMsg As String = ""
            If Not DatabaseHelper.TestConnection(errMsg) Then
                lblStat1Value.Text = "-"
                lblStat2Value.Text = "-"
                lblStat3Value.Text = "-"
                Return
            End If

            ' Statistik 1: Total transaksi hari ini
            Dim queryTotal As String = "SELECT COUNT(*) FROM Timbangan WHERE CAST(TanggalMasuk AS DATE) = CAST(GETDATE() AS DATE) AND Status = 'SELESAI'"
            Dim totalTransaksi As Object = DatabaseHelper.ExecuteScalar(queryTotal)
            If totalTransaksi IsNot Nothing AndAlso Not IsDBNull(totalTransaksi) Then
                lblStat1Value.Text = totalTransaksi.ToString()
            End If

            ' Statistik 2: Total netto hari ini
            Dim queryNetto As String = "SELECT ISNULL(SUM(BeratBersih), 0) FROM Timbangan WHERE CAST(TanggalMasuk AS DATE) = CAST(GETDATE() AS DATE) AND Status = 'SELESAI'"
            Dim totalNetto As Object = DatabaseHelper.ExecuteScalar(queryNetto)
            If totalNetto IsNot Nothing AndAlso Not IsDBNull(totalNetto) Then
                Dim netto As Decimal = Convert.ToDecimal(totalNetto)
                lblStat2Value.Text = netto.ToString("N0") & " KG"
            End If

            ' Statistik 3: Antrian (belum keluar)
            Dim queryAntrian As String = "SELECT COUNT(*) FROM Timbangan WHERE Status = 'MASUK'"
            Dim totalAntrian As Object = DatabaseHelper.ExecuteScalar(queryAntrian)
            If totalAntrian IsNot Nothing AndAlso Not IsDBNull(totalAntrian) Then
                lblStat3Value.Text = totalAntrian.ToString()
            End If

        Catch ex As Exception
            lblStat1Value.Text = "-"
            lblStat2Value.Text = "-"
            lblStat3Value.Text = "-"
        End Try
    End Sub

    ' =============================================
    ' FORM RESIZE - Responsive
    ' =============================================
    Private Sub FormDashboard_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        CenterDashboardContent()
    End Sub

    ' =============================================
    ' CENTER DASHBOARD CONTENT
    ' =============================================
    Private Sub CenterDashboardContent()
        Try
            Dim contentWidth As Integer = PanelContent.ClientSize.Width
            Dim contentHeight As Integer = PanelContent.ClientSize.Height

            ' Center PanelStats
            Dim cardWidth As Integer = 260
            Dim cardSpacing As Integer = 30
            Dim totalCardsWidth As Integer = (cardWidth * 3) + (cardSpacing * 2)

            Dim statsX As Integer = (contentWidth - totalCardsWidth) \ 2
            Dim statsY As Integer = (contentHeight - PanelStats.Height) \ 2

            PanelStats.Left = Math.Max(statsX, 20)
            PanelStats.Top = Math.Max(statsY, 140)
            PanelStats.Width = totalCardsWidth

            ' Reposition cards
            PanelStat1.Left = 0
            PanelStat2.Left = cardWidth + cardSpacing
            PanelStat3.Left = (cardWidth + cardSpacing) * 2

        Catch ex As Exception
            Debug.WriteLine($"[CenterDashboardContent] Error: {ex.Message}")
        End Try
    End Sub


    ' =============================================
    ' CENTER LABEL DALAM PANEL
    ' Memposisikan label di tengah panel (horizontal & vertical)
    ' =============================================
    Private Sub CenterLabelInPanel(lbl As Label, pnl As Panel)
        Try
            ' Hitung posisi tengah (di bawah title label yang tingginya 27px)
            Dim titleHeight As Integer = 27
            Dim availableHeight As Integer = pnl.Height - titleHeight

            lbl.Left = (pnl.Width - lbl.Width) \ 2
            lbl.Top = titleHeight + (availableHeight - lbl.Height) \ 2

        Catch ex As Exception
            Debug.WriteLine($"[CenterLabelInPanel] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' TOMBOL INPUT TIMBANGAN
    ' =============================================
    Private Sub btnInputTimbangan_Click(sender As Object, e As EventArgs) Handles btnInputTimbangan.Click
        Try
            ' Double check permission
            If Not UserSession.CanInputTimbangan() Then
                MessageBox.Show("Anda tidak memiliki akses untuk Input Timbangan!",
                    "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim frm As New FormInputTimbangan()
            frm.ShowDialog()

            LoadDashboardStats()
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.OpenInputTimbangan] Error: " & ex.ToString())
            MessageBox.Show("Gagal membuka Input Timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL DAFTAR TIMBANGAN
    ' =============================================
    Private Sub btnDaftarTimbangan_Click(sender As Object, e As EventArgs) Handles btnDaftarTimbangan.Click
        Try
            Dim frm As New FormDaftarTimbangan()
            frm.ShowDialog()

            LoadDashboardStats()
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.OpenDaftarTimbangan] Error: " & ex.ToString())
            MessageBox.Show("Gagal membuka Daftar Timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL LAPORAN
    ' =============================================
    Private Sub btnLaporan_Click(sender As Object, e As EventArgs) Handles btnLaporan.Click
        Try
            Dim frm As New FormLaporan()
            frm.ShowDialog()
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.OpenLaporan] Error: " & ex.ToString())
            MessageBox.Show("Gagal membuka Laporan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL MASTER DATA
    ' =============================================
    Private Sub btnMasterData_Click(sender As Object, e As EventArgs) Handles btnMasterData.Click
        Try
            If Not UserSession.CanManageMasterData() Then
                MessageBox.Show("Anda tidak memiliki akses ke Master Data!",
                    "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim frm As New FormMasterData()
            frm.ShowDialog()
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.OpenMasterData] Error: " & ex.ToString())
            MessageBox.Show("Gagal membuka Master Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL MANAJEMEN USER
    ' =============================================
    Private Sub btnManajemenUser_Click(sender As Object, e As EventArgs) Handles btnManajemenUser.Click
        Try
            If Not UserSession.CanAccessUserManagement() Then
                MessageBox.Show("Anda tidak memiliki akses ke Manajemen User!",
                    "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim frm As New FormManajemenUser()
            frm.ShowDialog()
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.OpenUserManagement] Error: " & ex.ToString())
            MessageBox.Show("Gagal membuka Manajemen User.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL PENGATURAN
    ' =============================================
    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Try
            If Not UserSession.CanAccessSettings() Then
                MessageBox.Show("Anda tidak memiliki akses ke Pengaturan!",
                    "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim frm As New FormPengaturan()
            frm.ShowDialog()
            LoadNamaPerusahaan()

        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.OpenSettings] Error: " & ex.ToString())
            MessageBox.Show("Gagal membuka Pengaturan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL AUDIT LOG
    ' =============================================
    Private Sub btnAuditLog_Click(sender As Object, e As EventArgs) Handles btnAuditLog.Click
        Try
            If Not UserSession.CanViewAuditLog() Then
                MessageBox.Show("Anda tidak memiliki akses ke Audit Log!",
                    "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim frm As New FormAuditLog()
            frm.ShowDialog()
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.OpenAuditLog] Error: " & ex.ToString())
            MessageBox.Show("Gagal membuka Audit Log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL LOGOUT
    ' =============================================
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Try
            If MessageBox.Show("Yakin ingin logout?", "Konfirmasi Logout",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Try
                    DatabaseHelper.InsertAuditLog(UserSession.UserID, "LOGOUT", Nothing, "Users",
                        UserSession.UserID, Nothing, Nothing,
                        $"User {UserSession.NamaLengkap} ({UserSession.Role}) logout dari sistem")
                Catch exAudit As Exception
                    Debug.WriteLine("[AuditLog] Best-effort audit write failed: " & exAudit.Message)
                End Try

                UserSession.Logout()

                Dim frmLogin As New FormLogin()
                frmLogin.Show()

                Me.Tag = "LOGOUT"
                Me.Close()
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormDashboard.Logout] Error: " & ex.ToString())
            MessageBox.Show("Gagal melakukan logout.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' FORM CLOSING
    ' =============================================
    Private Sub FormDashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Me.Tag IsNot Nothing AndAlso Me.Tag.ToString() = "LOGOUT" Then
                Return
            End If

            If Not UserSession.IsLoggedIn() Then
                Return
            End If

            If MessageBox.Show("Yakin ingin keluar dari aplikasi?", "Konfirmasi Keluar",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                e.Cancel = True
                Return
            End If

            Try
                DatabaseHelper.InsertAuditLog(UserSession.UserID, "LOGOUT", Nothing, "Users",
                    UserSession.UserID, Nothing, Nothing,
                    $"User {UserSession.NamaLengkap} keluar dari aplikasi")
            Catch exAudit As Exception
                Debug.WriteLine("[AuditLog] Best-effort audit write failed: " & exAudit.Message)
            End Try
            UserSession.Logout()
            Application.Exit()

        Catch ex As Exception
            Application.Exit()
        End Try
    End Sub

    Private Sub lblAppName_Click(sender As Object, e As EventArgs) Handles lblAppName.Click

    End Sub
End Class