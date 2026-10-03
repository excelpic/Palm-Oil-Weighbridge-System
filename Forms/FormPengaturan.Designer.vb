<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPengaturan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnResetDefault = New System.Windows.Forms.Button()
        Me.btnSimpan = New System.Windows.Forms.Button()
        Me.btnBatal = New System.Windows.Forms.Button()
        Me.btnTutup = New System.Windows.Forms.Button()
        Me.tabUmum = New System.Windows.Forms.TabPage()
        Me.gbInfoAplikasi = New System.Windows.Forms.GroupBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.lblDeveloper = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblVersi = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.lblNamaAplikasi = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.gbValidasi = New System.Windows.Forms.GroupBox()
        Me.chkBackupOtomatis = New System.Windows.Forms.CheckBox()
        Me.chkKonfirmasiHapus = New System.Windows.Forms.CheckBox()
        Me.chkWajibTransporter = New System.Windows.Forms.CheckBox()
        Me.chkWajibProduk = New System.Windows.Forms.CheckBox()
        Me.chkWajibSupplier = New System.Windows.Forms.CheckBox()
        Me.gbPotongan = New System.Windows.Forms.GroupBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.nudPotonganDefault = New System.Windows.Forms.NumericUpDown()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.tabPrinter = New System.Windows.Forms.TabPage()
        Me.gbPengaturanPrinter = New System.Windows.Forms.GroupBox()
        Me.chkAutoPrint = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbPaperSizeTkt = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.CmbPrinterTkt = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.nudCopies = New System.Windows.Forms.NumericUpDown()
        Me.lblJumlahCopy = New System.Windows.Forms.Label()
        Me.cmbPaperSize = New System.Windows.Forms.ComboBox()
        Me.lblUkuranKertas = New System.Windows.Forms.Label()
        Me.cmbPrinter = New System.Windows.Forms.ComboBox()
        Me.lblNamaPrinter = New System.Windows.Forms.Label()
        Me.tabTimbangan = New System.Windows.Forms.TabPage()
        Me.gbSpesifikasi = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDivisi = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtKapasitas = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.gbKoneksiSerial = New System.Windows.Forms.GroupBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.cmbTipeIndikator = New System.Windows.Forms.ComboBox()
        Me.lblStatusKoneksi = New System.Windows.Forms.Label()
        Me.btnTestKoneksi = New System.Windows.Forms.Button()
        Me.cmbProtocol = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbStopBits = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbParity = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbDataBits = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbComPort = New System.Windows.Forms.ComboBox()
        Me.cmbMerekIndikator = New System.Windows.Forms.ComboBox()
        Me.lblMerekIndikator = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tabPerusahaan = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.btnHapusLogo = New System.Windows.Forms.Button()
        Me.btnBrowseLogo = New System.Windows.Forms.Button()
        Me.txtLogoPath = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.gbDataPerusahaan = New System.Windows.Forms.GroupBox()
        Me.txtEmailPerusahaan = New System.Windows.Forms.TextBox()
        Me.txtFaxPerusahaan = New System.Windows.Forms.TextBox()
        Me.txtTeleponPerusahaan = New System.Windows.Forms.TextBox()
        Me.txtKotaPerusahaan = New System.Windows.Forms.TextBox()
        Me.txtAlamatPerusahaan = New System.Windows.Forms.TextBox()
        Me.txtNamaPerusahaan = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tabSettings = New System.Windows.Forms.TabControl()
        Me.tabDatabase = New System.Windows.Forms.TabPage()
        Me.grpInfoKoneksi = New System.Windows.Forms.GroupBox()
        Me.lblCurrentAuth = New System.Windows.Forms.Label()
        Me.lblCurrentDB = New System.Windows.Forms.Label()
        Me.lblCurrentServer = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.grpKoneksiDB = New System.Windows.Forms.GroupBox()
        Me.pnlStatusDB = New System.Windows.Forms.Panel()
        Me.lblStatusDB = New System.Windows.Forms.Label()
        Me.btnSimpanDB = New System.Windows.Forms.Button()
        Me.btnTestDB = New System.Windows.Forms.Button()
        Me.grpAuthentication = New System.Windows.Forms.GroupBox()
        Me.txtSQLPassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtSQLUsername = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.rbSQLAuth = New System.Windows.Forms.RadioButton()
        Me.rbWindowsAuth = New System.Windows.Forms.RadioButton()
        Me.txtDatabaseName = New System.Windows.Forms.TextBox()
        Me.lblDatabaseName = New System.Windows.Forms.Label()
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.lblServerName = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.nudDigit = New System.Windows.Forms.NumericUpDown()
        Me.chkResetHarian = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.lblPreviewFormat = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblTanggalCounter = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblCounterValue = New System.Windows.Forms.Label()
        Me.btnResetCounter = New System.Windows.Forms.Button()
        Me.lblWarningCounter = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeaderSubtitle = New System.Windows.Forms.Label()
        Me.lblHeaderTitle = New System.Windows.Forms.Label()
        Me.pnlSidebar = New System.Windows.Forms.Panel()
        Me.tabUmum.SuspendLayout()
        Me.gbInfoAplikasi.SuspendLayout()
        Me.gbValidasi.SuspendLayout()
        Me.gbPotongan.SuspendLayout()
        CType(Me.nudPotonganDefault, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPrinter.SuspendLayout()
        Me.gbPengaturanPrinter.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCopies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabTimbangan.SuspendLayout()
        Me.gbSpesifikasi.SuspendLayout()
        Me.gbKoneksiSerial.SuspendLayout()
        Me.tabPerusahaan.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDataPerusahaan.SuspendLayout()
        Me.tabSettings.SuspendLayout()
        Me.tabDatabase.SuspendLayout()
        Me.grpInfoKoneksi.SuspendLayout()
        Me.grpKoneksiDB.SuspendLayout()
        Me.pnlStatusDB.SuspendLayout()
        Me.grpAuthentication.SuspendLayout()
        CType(Me.nudDigit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.pnlSidebar.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnResetDefault
        '
        Me.btnResetDefault.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnResetDefault.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnResetDefault.FlatAppearance.BorderSize = 0
        Me.btnResetDefault.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(124, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnResetDefault.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnResetDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetDefault.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnResetDefault.ForeColor = System.Drawing.Color.White
        Me.btnResetDefault.Location = New System.Drawing.Point(20, 30)
        Me.btnResetDefault.Name = "btnResetDefault"
        Me.btnResetDefault.Size = New System.Drawing.Size(160, 45)
        Me.btnResetDefault.TabIndex = 0
        Me.btnResetDefault.Text = "🔄 Reset"
        Me.btnResetDefault.UseVisualStyleBackColor = False
        '
        'btnSimpan
        '
        Me.btnSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.btnSimpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSimpan.FlatAppearance.BorderSize = 0
        Me.btnSimpan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.btnSimpan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSimpan.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSimpan.ForeColor = System.Drawing.Color.White
        Me.btnSimpan.Location = New System.Drawing.Point(20, 95)
        Me.btnSimpan.Name = "btnSimpan"
        Me.btnSimpan.Size = New System.Drawing.Size(160, 50)
        Me.btnSimpan.TabIndex = 1
        Me.btnSimpan.Text = "💾 Simpan"
        Me.btnSimpan.UseVisualStyleBackColor = False
        '
        'btnBatal
        '
        Me.btnBatal.BackColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.btnBatal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBatal.FlatAppearance.BorderSize = 0
        Me.btnBatal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer))
        Me.btnBatal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBatal.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnBatal.ForeColor = System.Drawing.Color.White
        Me.btnBatal.Location = New System.Drawing.Point(20, 160)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(160, 45)
        Me.btnBatal.TabIndex = 2
        Me.btnBatal.Text = "↩ Batal"
        Me.btnBatal.UseVisualStyleBackColor = False
        '
        'btnTutup
        '
        Me.btnTutup.BackColor = System.Drawing.Color.FromArgb(CType(CType(183, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnTutup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTutup.FlatAppearance.BorderSize = 0
        Me.btnTutup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTutup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.btnTutup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTutup.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnTutup.ForeColor = System.Drawing.Color.White
        Me.btnTutup.Location = New System.Drawing.Point(20, 220)
        Me.btnTutup.Name = "btnTutup"
        Me.btnTutup.Size = New System.Drawing.Size(160, 45)
        Me.btnTutup.TabIndex = 3
        Me.btnTutup.Text = "✖ Tutup"
        Me.btnTutup.UseVisualStyleBackColor = False
        '
        'tabUmum
        '
        Me.tabUmum.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.tabUmum.Controls.Add(Me.gbInfoAplikasi)
        Me.tabUmum.Controls.Add(Me.gbValidasi)
        Me.tabUmum.Controls.Add(Me.gbPotongan)
        Me.tabUmum.Location = New System.Drawing.Point(4, 32)
        Me.tabUmum.Name = "tabUmum"
        Me.tabUmum.Padding = New System.Windows.Forms.Padding(15)
        Me.tabUmum.Size = New System.Drawing.Size(936, 664)
        Me.tabUmum.TabIndex = 4
        Me.tabUmum.Text = "⚙ Umum"
        '
        'gbInfoAplikasi
        '
        Me.gbInfoAplikasi.BackColor = System.Drawing.Color.White
        Me.gbInfoAplikasi.Controls.Add(Me.Label35)
        Me.gbInfoAplikasi.Controls.Add(Me.lblDeveloper)
        Me.gbInfoAplikasi.Controls.Add(Me.Label34)
        Me.gbInfoAplikasi.Controls.Add(Me.lblVersi)
        Me.gbInfoAplikasi.Controls.Add(Me.Label33)
        Me.gbInfoAplikasi.Controls.Add(Me.lblNamaAplikasi)
        Me.gbInfoAplikasi.Controls.Add(Me.Label32)
        Me.gbInfoAplikasi.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbInfoAplikasi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.gbInfoAplikasi.Location = New System.Drawing.Point(18, 380)
        Me.gbInfoAplikasi.Name = "gbInfoAplikasi"
        Me.gbInfoAplikasi.Size = New System.Drawing.Size(700, 150)
        Me.gbInfoAplikasi.TabIndex = 2
        Me.gbInfoAplikasi.TabStop = False
        Me.gbInfoAplikasi.Text = "ℹ Informasi Aplikasi"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.Label35.Location = New System.Drawing.Point(25, 115)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(199, 20)
        Me.Label35.TabIndex = 6
        Me.Label35.Text = "© 2026 - All Rights Reserved"
        '
        'lblDeveloper
        '
        Me.lblDeveloper.AutoSize = True
        Me.lblDeveloper.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDeveloper.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.lblDeveloper.Location = New System.Drawing.Point(150, 85)
        Me.lblDeveloper.Name = "lblDeveloper"
        Me.lblDeveloper.Size = New System.Drawing.Size(126, 20)
        Me.lblDeveloper.TabIndex = 5
        Me.lblDeveloper.Text = "i-NURA DIGITAL"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(25, 85)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(78, 20)
        Me.Label34.TabIndex = 4
        Me.Label34.Text = "Developer"
        '
        'lblVersi
        '
        Me.lblVersi.AutoSize = True
        Me.lblVersi.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblVersi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.lblVersi.Location = New System.Drawing.Point(150, 60)
        Me.lblVersi.Name = "lblVersi"
        Me.lblVersi.Size = New System.Drawing.Size(44, 20)
        Me.lblVersi.TabIndex = 3
        Me.lblVersi.Text = "1.0.0"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer))
        Me.Label33.Location = New System.Drawing.Point(25, 60)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(40, 20)
        Me.Label33.TabIndex = 2
        Me.Label33.Text = "Versi"
        '
        'lblNamaAplikasi
        '
        Me.lblNamaAplikasi.AutoSize = True
        Me.lblNamaAplikasi.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblNamaAplikasi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.lblNamaAplikasi.Location = New System.Drawing.Point(150, 35)
        Me.lblNamaAplikasi.Name = "lblNamaAplikasi"
        Me.lblNamaAplikasi.Size = New System.Drawing.Size(196, 23)
        Me.lblNamaAplikasi.TabIndex = 1
        Me.lblNamaAplikasi.Text = "Sistem Timbangan PKS"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer))
        Me.Label32.Location = New System.Drawing.Point(25, 35)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(105, 20)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "Nama Aplikasi"
        '
        'gbValidasi
        '
        Me.gbValidasi.BackColor = System.Drawing.Color.White
        Me.gbValidasi.Controls.Add(Me.chkBackupOtomatis)
        Me.gbValidasi.Controls.Add(Me.chkKonfirmasiHapus)
        Me.gbValidasi.Controls.Add(Me.chkWajibTransporter)
        Me.gbValidasi.Controls.Add(Me.chkWajibProduk)
        Me.gbValidasi.Controls.Add(Me.chkWajibSupplier)
        Me.gbValidasi.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbValidasi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.gbValidasi.Location = New System.Drawing.Point(18, 150)
        Me.gbValidasi.Name = "gbValidasi"
        Me.gbValidasi.Size = New System.Drawing.Size(700, 210)
        Me.gbValidasi.TabIndex = 1
        Me.gbValidasi.TabStop = False
        Me.gbValidasi.Text = "✓ Validasi Input"
        '
        'chkBackupOtomatis
        '
        Me.chkBackupOtomatis.AutoSize = True
        Me.chkBackupOtomatis.Checked = True
        Me.chkBackupOtomatis.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBackupOtomatis.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkBackupOtomatis.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkBackupOtomatis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.chkBackupOtomatis.Location = New System.Drawing.Point(30, 160)
        Me.chkBackupOtomatis.Name = "chkBackupOtomatis"
        Me.chkBackupOtomatis.Size = New System.Drawing.Size(404, 27)
        Me.chkBackupOtomatis.TabIndex = 4
        Me.chkBackupOtomatis.Text = "💾 Backup database otomatis saat tutup aplikasi"
        Me.chkBackupOtomatis.UseVisualStyleBackColor = True
        '
        'chkKonfirmasiHapus
        '
        Me.chkKonfirmasiHapus.AutoSize = True
        Me.chkKonfirmasiHapus.Checked = True
        Me.chkKonfirmasiHapus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkKonfirmasiHapus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkKonfirmasiHapus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkKonfirmasiHapus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.chkKonfirmasiHapus.Location = New System.Drawing.Point(30, 125)
        Me.chkKonfirmasiHapus.Name = "chkKonfirmasiHapus"
        Me.chkKonfirmasiHapus.Size = New System.Drawing.Size(422, 27)
        Me.chkKonfirmasiHapus.TabIndex = 3
        Me.chkKonfirmasiHapus.Text = "⚠️ Tampilkan konfirmasi sebelum menghapus data"
        Me.chkKonfirmasiHapus.UseVisualStyleBackColor = True
        '
        'chkWajibTransporter
        '
        Me.chkWajibTransporter.AutoSize = True
        Me.chkWajibTransporter.Checked = True
        Me.chkWajibTransporter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWajibTransporter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkWajibTransporter.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkWajibTransporter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.chkWajibTransporter.Location = New System.Drawing.Point(30, 90)
        Me.chkWajibTransporter.Name = "chkWajibTransporter"
        Me.chkWajibTransporter.Size = New System.Drawing.Size(214, 27)
        Me.chkWajibTransporter.TabIndex = 2
        Me.chkWajibTransporter.Text = "🚛 Wajib isi Transporter"
        Me.chkWajibTransporter.UseVisualStyleBackColor = True
        '
        'chkWajibProduk
        '
        Me.chkWajibProduk.AutoSize = True
        Me.chkWajibProduk.Checked = True
        Me.chkWajibProduk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWajibProduk.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkWajibProduk.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkWajibProduk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.chkWajibProduk.Location = New System.Drawing.Point(30, 60)
        Me.chkWajibProduk.Name = "chkWajibProduk"
        Me.chkWajibProduk.Size = New System.Drawing.Size(181, 27)
        Me.chkWajibProduk.TabIndex = 1
        Me.chkWajibProduk.Text = "📦 Wajib isi Produk"
        Me.chkWajibProduk.UseVisualStyleBackColor = True
        '
        'chkWajibSupplier
        '
        Me.chkWajibSupplier.AutoSize = True
        Me.chkWajibSupplier.Checked = True
        Me.chkWajibSupplier.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWajibSupplier.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkWajibSupplier.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkWajibSupplier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.chkWajibSupplier.Location = New System.Drawing.Point(30, 30)
        Me.chkWajibSupplier.Name = "chkWajibSupplier"
        Me.chkWajibSupplier.Size = New System.Drawing.Size(270, 27)
        Me.chkWajibSupplier.TabIndex = 0
        Me.chkWajibSupplier.Text = "🏭 Wajib isi Supplier/Customer"
        Me.chkWajibSupplier.UseVisualStyleBackColor = True
        '
        'gbPotongan
        '
        Me.gbPotongan.BackColor = System.Drawing.Color.White
        Me.gbPotongan.Controls.Add(Me.Label31)
        Me.gbPotongan.Controls.Add(Me.Label30)
        Me.gbPotongan.Controls.Add(Me.nudPotonganDefault)
        Me.gbPotongan.Controls.Add(Me.Label29)
        Me.gbPotongan.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbPotongan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.gbPotongan.Location = New System.Drawing.Point(18, 20)
        Me.gbPotongan.Name = "gbPotongan"
        Me.gbPotongan.Size = New System.Drawing.Size(700, 110)
        Me.gbPotongan.TabIndex = 0
        Me.gbPotongan.TabStop = False
        Me.gbPotongan.Text = "⚖ Pengaturan Potongan"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.Label31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.Label31.Location = New System.Drawing.Point(25, 75)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(417, 20)
        Me.Label31.TabIndex = 3
        Me.Label31.Text = "💡 Potongan default akan otomatis terisi saat input timbangan"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label30.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label30.Location = New System.Drawing.Point(280, 40)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(33, 23)
        Me.Label30.TabIndex = 2
        Me.Label30.Text = "KG"
        '
        'nudPotonganDefault
        '
        Me.nudPotonganDefault.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.nudPotonganDefault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudPotonganDefault.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.nudPotonganDefault.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudPotonganDefault.Location = New System.Drawing.Point(175, 38)
        Me.nudPotonganDefault.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudPotonganDefault.Name = "nudPotonganDefault"
        Me.nudPotonganDefault.Size = New System.Drawing.Size(90, 30)
        Me.nudPotonganDefault.TabIndex = 1
        Me.nudPotonganDefault.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(25, 40)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(144, 23)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Potongan Default"
        '
        'tabPrinter
        '
        Me.tabPrinter.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.tabPrinter.Controls.Add(Me.gbPengaturanPrinter)
        Me.tabPrinter.Location = New System.Drawing.Point(4, 32)
        Me.tabPrinter.Name = "tabPrinter"
        Me.tabPrinter.Padding = New System.Windows.Forms.Padding(15)
        Me.tabPrinter.Size = New System.Drawing.Size(936, 664)
        Me.tabPrinter.TabIndex = 2
        Me.tabPrinter.Text = "🖨 Printer"
        '
        'gbPengaturanPrinter
        '
        Me.gbPengaturanPrinter.BackColor = System.Drawing.Color.White
        Me.gbPengaturanPrinter.Controls.Add(Me.chkAutoPrint)
        Me.gbPengaturanPrinter.Controls.Add(Me.NumericUpDown1)
        Me.gbPengaturanPrinter.Controls.Add(Me.Label20)
        Me.gbPengaturanPrinter.Controls.Add(Me.cmbPaperSizeTkt)
        Me.gbPengaturanPrinter.Controls.Add(Me.Label21)
        Me.gbPengaturanPrinter.Controls.Add(Me.CmbPrinterTkt)
        Me.gbPengaturanPrinter.Controls.Add(Me.Label22)
        Me.gbPengaturanPrinter.Controls.Add(Me.nudCopies)
        Me.gbPengaturanPrinter.Controls.Add(Me.lblJumlahCopy)
        Me.gbPengaturanPrinter.Controls.Add(Me.cmbPaperSize)
        Me.gbPengaturanPrinter.Controls.Add(Me.lblUkuranKertas)
        Me.gbPengaturanPrinter.Controls.Add(Me.cmbPrinter)
        Me.gbPengaturanPrinter.Controls.Add(Me.lblNamaPrinter)
        Me.gbPengaturanPrinter.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbPengaturanPrinter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.gbPengaturanPrinter.Location = New System.Drawing.Point(18, 20)
        Me.gbPengaturanPrinter.Name = "gbPengaturanPrinter"
        Me.gbPengaturanPrinter.Size = New System.Drawing.Size(880, 350)
        Me.gbPengaturanPrinter.TabIndex = 0
        Me.gbPengaturanPrinter.TabStop = False
        Me.gbPengaturanPrinter.Text = "🖨 Pengaturan Printer"
        '
        'chkAutoPrint
        '
        Me.chkAutoPrint.AutoSize = True
        Me.chkAutoPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkAutoPrint.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkAutoPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.chkAutoPrint.Location = New System.Drawing.Point(25, 130)
        Me.chkAutoPrint.Name = "chkAutoPrint"
        Me.chkAutoPrint.Size = New System.Drawing.Size(382, 27)
        Me.chkAutoPrint.TabIndex = 9
        Me.chkAutoPrint.Text = "🖨️ Cetak otomatis setelah simpan timbangan"
        Me.chkAutoPrint.UseVisualStyleBackColor = True
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.NumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericUpDown1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.NumericUpDown1.Location = New System.Drawing.Point(592, 236)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(80, 30)
        Me.NumericUpDown1.TabIndex = 17
        Me.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDown1.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(450, 238)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(136, 23)
        Me.Label20.TabIndex = 15
        Me.Label20.Text = "Jumlah Copy 📝"
        '
        'cmbPaperSizeTkt
        '
        Me.cmbPaperSizeTkt.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbPaperSizeTkt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbPaperSizeTkt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPaperSizeTkt.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbPaperSizeTkt.FormattingEnabled = True
        Me.cmbPaperSizeTkt.Items.AddRange(New Object() {"A4", "A5", "Letter", "Custom"})
        Me.cmbPaperSizeTkt.Location = New System.Drawing.Point(175, 235)
        Me.cmbPaperSizeTkt.Name = "cmbPaperSizeTkt"
        Me.cmbPaperSizeTkt.Size = New System.Drawing.Size(200, 31)
        Me.cmbPaperSizeTkt.TabIndex = 14
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(25, 238)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(145, 23)
        Me.Label21.TabIndex = 13
        Me.Label21.Text = "Ukuran Kertas 📄"
        '
        'CmbPrinterTkt
        '
        Me.CmbPrinterTkt.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.CmbPrinterTkt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbPrinterTkt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPrinterTkt.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.CmbPrinterTkt.FormattingEnabled = True
        Me.CmbPrinterTkt.Location = New System.Drawing.Point(175, 195)
        Me.CmbPrinterTkt.Name = "CmbPrinterTkt"
        Me.CmbPrinterTkt.Size = New System.Drawing.Size(260, 31)
        Me.CmbPrinterTkt.TabIndex = 11
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(25, 198)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(130, 23)
        Me.Label22.TabIndex = 10
        Me.Label22.Text = "Printer Tiket 🎫"
        '
        'nudCopies
        '
        Me.nudCopies.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.nudCopies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudCopies.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.nudCopies.Location = New System.Drawing.Point(592, 45)
        Me.nudCopies.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudCopies.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudCopies.Name = "nudCopies"
        Me.nudCopies.Size = New System.Drawing.Size(80, 30)
        Me.nudCopies.TabIndex = 8
        Me.nudCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudCopies.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'lblJumlahCopy
        '
        Me.lblJumlahCopy.AutoSize = True
        Me.lblJumlahCopy.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblJumlahCopy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblJumlahCopy.Location = New System.Drawing.Point(450, 47)
        Me.lblJumlahCopy.Name = "lblJumlahCopy"
        Me.lblJumlahCopy.Size = New System.Drawing.Size(136, 23)
        Me.lblJumlahCopy.TabIndex = 5
        Me.lblJumlahCopy.Text = "Jumlah Copy 📝"
        '
        'cmbPaperSize
        '
        Me.cmbPaperSize.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbPaperSize.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPaperSize.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbPaperSize.FormattingEnabled = True
        Me.cmbPaperSize.Items.AddRange(New Object() {"A4", "A5", "Letter", "Custom"})
        Me.cmbPaperSize.Location = New System.Drawing.Point(175, 85)
        Me.cmbPaperSize.Name = "cmbPaperSize"
        Me.cmbPaperSize.Size = New System.Drawing.Size(200, 31)
        Me.cmbPaperSize.TabIndex = 4
        '
        'lblUkuranKertas
        '
        Me.lblUkuranKertas.AutoSize = True
        Me.lblUkuranKertas.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblUkuranKertas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblUkuranKertas.Location = New System.Drawing.Point(25, 88)
        Me.lblUkuranKertas.Name = "lblUkuranKertas"
        Me.lblUkuranKertas.Size = New System.Drawing.Size(145, 23)
        Me.lblUkuranKertas.TabIndex = 3
        Me.lblUkuranKertas.Text = "Ukuran Kertas 📄"
        '
        'cmbPrinter
        '
        Me.cmbPrinter.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbPrinter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrinter.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbPrinter.FormattingEnabled = True
        Me.cmbPrinter.Location = New System.Drawing.Point(175, 45)
        Me.cmbPrinter.Name = "cmbPrinter"
        Me.cmbPrinter.Size = New System.Drawing.Size(260, 31)
        Me.cmbPrinter.TabIndex = 1
        '
        'lblNamaPrinter
        '
        Me.lblNamaPrinter.AutoSize = True
        Me.lblNamaPrinter.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblNamaPrinter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblNamaPrinter.Location = New System.Drawing.Point(25, 48)
        Me.lblNamaPrinter.Name = "lblNamaPrinter"
        Me.lblNamaPrinter.Size = New System.Drawing.Size(132, 23)
        Me.lblNamaPrinter.TabIndex = 0
        Me.lblNamaPrinter.Text = "Printer Struk 🖨️"
        '
        'tabTimbangan
        '
        Me.tabTimbangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.tabTimbangan.Controls.Add(Me.gbSpesifikasi)
        Me.tabTimbangan.Controls.Add(Me.gbKoneksiSerial)
        Me.tabTimbangan.Location = New System.Drawing.Point(4, 32)
        Me.tabTimbangan.Name = "tabTimbangan"
        Me.tabTimbangan.Padding = New System.Windows.Forms.Padding(15)
        Me.tabTimbangan.Size = New System.Drawing.Size(936, 664)
        Me.tabTimbangan.TabIndex = 1
        Me.tabTimbangan.Text = "⚖ Timbangan"
        '
        'gbSpesifikasi
        '
        Me.gbSpesifikasi.BackColor = System.Drawing.Color.White
        Me.gbSpesifikasi.Controls.Add(Me.Label19)
        Me.gbSpesifikasi.Controls.Add(Me.Label18)
        Me.gbSpesifikasi.Controls.Add(Me.txtDivisi)
        Me.gbSpesifikasi.Controls.Add(Me.Label17)
        Me.gbSpesifikasi.Controls.Add(Me.Label16)
        Me.gbSpesifikasi.Controls.Add(Me.txtKapasitas)
        Me.gbSpesifikasi.Controls.Add(Me.Label15)
        Me.gbSpesifikasi.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbSpesifikasi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.gbSpesifikasi.Location = New System.Drawing.Point(18, 280)
        Me.gbSpesifikasi.Name = "gbSpesifikasi"
        Me.gbSpesifikasi.Size = New System.Drawing.Size(700, 140)
        Me.gbSpesifikasi.TabIndex = 1
        Me.gbSpesifikasi.TabStop = False
        Me.gbSpesifikasi.Text = "📊 Spesifikasi Timbangan"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(25, 105)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(429, 20)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "💡 Divisi adalah ketelitian pembacaan timbangan (misal: 10 KG)"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(470, 45)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(33, 23)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "KG"
        '
        'txtDivisi
        '
        Me.txtDivisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtDivisi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivisi.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDivisi.Location = New System.Drawing.Point(370, 42)
        Me.txtDivisi.Name = "txtDivisi"
        Me.txtDivisi.Size = New System.Drawing.Size(90, 30)
        Me.txtDivisi.TabIndex = 4
        Me.txtDivisi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(300, 45)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(49, 23)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Divisi"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(235, 45)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(33, 23)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "KG"
        '
        'txtKapasitas
        '
        Me.txtKapasitas.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtKapasitas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKapasitas.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtKapasitas.Location = New System.Drawing.Point(125, 42)
        Me.txtKapasitas.Name = "txtKapasitas"
        Me.txtKapasitas.Size = New System.Drawing.Size(100, 30)
        Me.txtKapasitas.TabIndex = 1
        Me.txtKapasitas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(25, 45)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(81, 23)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Kapasitas"
        '
        'gbKoneksiSerial
        '
        Me.gbKoneksiSerial.BackColor = System.Drawing.Color.White
        Me.gbKoneksiSerial.Controls.Add(Me.Label36)
        Me.gbKoneksiSerial.Controls.Add(Me.cmbTipeIndikator)
        Me.gbKoneksiSerial.Controls.Add(Me.lblStatusKoneksi)
        Me.gbKoneksiSerial.Controls.Add(Me.btnTestKoneksi)
        Me.gbKoneksiSerial.Controls.Add(Me.cmbProtocol)
        Me.gbKoneksiSerial.Controls.Add(Me.Label14)
        Me.gbKoneksiSerial.Controls.Add(Me.cmbStopBits)
        Me.gbKoneksiSerial.Controls.Add(Me.Label13)
        Me.gbKoneksiSerial.Controls.Add(Me.cmbParity)
        Me.gbKoneksiSerial.Controls.Add(Me.Label12)
        Me.gbKoneksiSerial.Controls.Add(Me.cmbDataBits)
        Me.gbKoneksiSerial.Controls.Add(Me.Label11)
        Me.gbKoneksiSerial.Controls.Add(Me.cmbBaudRate)
        Me.gbKoneksiSerial.Controls.Add(Me.Label10)
        Me.gbKoneksiSerial.Controls.Add(Me.cmbComPort)
        Me.gbKoneksiSerial.Controls.Add(Me.cmbMerekIndikator)
        Me.gbKoneksiSerial.Controls.Add(Me.lblMerekIndikator)
        Me.gbKoneksiSerial.Controls.Add(Me.Label9)
        Me.gbKoneksiSerial.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbKoneksiSerial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.gbKoneksiSerial.Location = New System.Drawing.Point(18, 18)
        Me.gbKoneksiSerial.Name = "gbKoneksiSerial"
        Me.gbKoneksiSerial.Size = New System.Drawing.Size(790, 280)
        Me.gbKoneksiSerial.TabIndex = 0
        Me.gbKoneksiSerial.TabStop = False
        Me.gbKoneksiSerial.Text = "🔌 Koneksi Serial Port"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(357, 187)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(42, 23)
        Me.Label36.TabIndex = 18
        Me.Label36.Text = "Tipe"
        '
        'cmbTipeIndikator
        '
        Me.cmbTipeIndikator.FormattingEnabled = True
        Me.cmbTipeIndikator.Location = New System.Drawing.Point(446, 187)
        Me.cmbTipeIndikator.Name = "cmbTipeIndikator"
        Me.cmbTipeIndikator.Size = New System.Drawing.Size(121, 31)
        Me.cmbTipeIndikator.TabIndex = 17
        '
        'lblStatusKoneksi
        '
        Me.lblStatusKoneksi.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Italic)
        Me.lblStatusKoneksi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.lblStatusKoneksi.Location = New System.Drawing.Point(25, 227)
        Me.lblStatusKoneksi.Name = "lblStatusKoneksi"
        Me.lblStatusKoneksi.Size = New System.Drawing.Size(650, 30)
        Me.lblStatusKoneksi.TabIndex = 14
        Me.lblStatusKoneksi.Text = "● Status: Belum terkoneksi"
        '
        'btnTestKoneksi
        '
        Me.btnTestKoneksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnTestKoneksi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTestKoneksi.FlatAppearance.BorderSize = 0
        Me.btnTestKoneksi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnTestKoneksi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnTestKoneksi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTestKoneksi.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnTestKoneksi.ForeColor = System.Drawing.Color.White
        Me.btnTestKoneksi.Location = New System.Drawing.Point(577, 34)
        Me.btnTestKoneksi.Name = "btnTestKoneksi"
        Me.btnTestKoneksi.Size = New System.Drawing.Size(175, 50)
        Me.btnTestKoneksi.TabIndex = 13
        Me.btnTestKoneksi.Text = "🔌 Test Koneksi"
        Me.btnTestKoneksi.UseVisualStyleBackColor = False
        '
        'cmbProtocol
        '
        Me.cmbProtocol.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProtocol.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbProtocol.FormattingEnabled = True
        Me.cmbProtocol.Items.AddRange(New Object() {"STANDARD", "A9", "YH-T7", "AND", "OHAUS", "JADEVER"})
        Me.cmbProtocol.Location = New System.Drawing.Point(447, 134)
        Me.cmbProtocol.Name = "cmbProtocol"
        Me.cmbProtocol.Size = New System.Drawing.Size(120, 31)
        Me.cmbProtocol.TabIndex = 12
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(357, 137)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 23)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "Protocol"
        '
        'cmbStopBits
        '
        Me.cmbStopBits.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStopBits.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbStopBits.FormattingEnabled = True
        Me.cmbStopBits.Items.AddRange(New Object() {"1", "1.5", "2"})
        Me.cmbStopBits.Location = New System.Drawing.Point(168, 137)
        Me.cmbStopBits.Name = "cmbStopBits"
        Me.cmbStopBits.Size = New System.Drawing.Size(100, 31)
        Me.cmbStopBits.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(25, 148)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 23)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "Stop Bits"
        '
        'cmbParity
        '
        Me.cmbParity.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParity.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbParity.FormattingEnabled = True
        Me.cmbParity.Items.AddRange(New Object() {"None", "Odd", "Even", "Mark", "Space"})
        Me.cmbParity.Location = New System.Drawing.Point(447, 84)
        Me.cmbParity.Name = "cmbParity"
        Me.cmbParity.Size = New System.Drawing.Size(100, 31)
        Me.cmbParity.TabIndex = 8
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(357, 87)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 23)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "Parity"
        '
        'cmbDataBits
        '
        Me.cmbDataBits.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataBits.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbDataBits.FormattingEnabled = True
        Me.cmbDataBits.Items.AddRange(New Object() {"7", "8"})
        Me.cmbDataBits.Location = New System.Drawing.Point(168, 88)
        Me.cmbDataBits.Name = "cmbDataBits"
        Me.cmbDataBits.Size = New System.Drawing.Size(100, 31)
        Me.cmbDataBits.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(25, 98)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 23)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Data Bits"
        '
        'cmbBaudRate
        '
        Me.cmbBaudRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBaudRate.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbBaudRate.FormattingEnabled = True
        Me.cmbBaudRate.Items.AddRange(New Object() {"9600", "19200", "38400", "57600", "115200"})
        Me.cmbBaudRate.Location = New System.Drawing.Point(447, 34)
        Me.cmbBaudRate.Name = "cmbBaudRate"
        Me.cmbBaudRate.Size = New System.Drawing.Size(100, 31)
        Me.cmbBaudRate.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(357, 37)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 23)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Baud Rate"
        '
        'cmbComPort
        '
        Me.cmbComPort.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbComPort.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbComPort.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbComPort.FormattingEnabled = True
        Me.cmbComPort.Location = New System.Drawing.Point(168, 42)
        Me.cmbComPort.Name = "cmbComPort"
        Me.cmbComPort.Size = New System.Drawing.Size(100, 31)
        Me.cmbComPort.TabIndex = 1
        '
        'cmbMerekIndikator
        '
        Me.cmbMerekIndikator.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbMerekIndikator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMerekIndikator.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbMerekIndikator.FormattingEnabled = True
        Me.cmbMerekIndikator.Items.AddRange(New Object() {"GSC", "SONIC", "YAOHUA"})
        Me.cmbMerekIndikator.Location = New System.Drawing.Point(168, 187)
        Me.cmbMerekIndikator.Name = "cmbMerekIndikator"
        Me.cmbMerekIndikator.Size = New System.Drawing.Size(120, 31)
        Me.cmbMerekIndikator.TabIndex = 16
        '
        'lblMerekIndikator
        '
        Me.lblMerekIndikator.AutoSize = True
        Me.lblMerekIndikator.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblMerekIndikator.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblMerekIndikator.Location = New System.Drawing.Point(25, 198)
        Me.lblMerekIndikator.Name = "lblMerekIndikator"
        Me.lblMerekIndikator.Size = New System.Drawing.Size(130, 23)
        Me.lblMerekIndikator.TabIndex = 15
        Me.lblMerekIndikator.Text = "Merek Indikator"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(25, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 23)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "COM Port"
        '
        'tabPerusahaan
        '
        Me.tabPerusahaan.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.tabPerusahaan.Controls.Add(Me.GroupBox1)
        Me.tabPerusahaan.Controls.Add(Me.gbDataPerusahaan)
        Me.tabPerusahaan.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.tabPerusahaan.Location = New System.Drawing.Point(4, 32)
        Me.tabPerusahaan.Name = "tabPerusahaan"
        Me.tabPerusahaan.Padding = New System.Windows.Forms.Padding(15)
        Me.tabPerusahaan.Size = New System.Drawing.Size(936, 664)
        Me.tabPerusahaan.TabIndex = 0
        Me.tabPerusahaan.Text = "🏢 Perusahaan"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.picLogo)
        Me.GroupBox1.Controls.Add(Me.btnHapusLogo)
        Me.GroupBox1.Controls.Add(Me.btnBrowseLogo)
        Me.GroupBox1.Controls.Add(Me.txtLogoPath)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(18, 311)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(700, 220)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "🖼️ Logo Perusahaan"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(200, 130)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(214, 20)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Preview logo akan tampil di sini"
        '
        'picLogo
        '
        Me.picLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLogo.Location = New System.Drawing.Point(25, 80)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(160, 120)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 4
        Me.picLogo.TabStop = False
        '
        'btnHapusLogo
        '
        Me.btnHapusLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnHapusLogo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHapusLogo.FlatAppearance.BorderSize = 0
        Me.btnHapusLogo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnHapusLogo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.btnHapusLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapusLogo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnHapusLogo.ForeColor = System.Drawing.Color.White
        Me.btnHapusLogo.Location = New System.Drawing.Point(580, 35)
        Me.btnHapusLogo.Name = "btnHapusLogo"
        Me.btnHapusLogo.Size = New System.Drawing.Size(100, 35)
        Me.btnHapusLogo.TabIndex = 3
        Me.btnHapusLogo.Text = "🗑 Hapus"
        Me.btnHapusLogo.UseVisualStyleBackColor = False
        '
        'btnBrowseLogo
        '
        Me.btnBrowseLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnBrowseLogo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowseLogo.FlatAppearance.BorderSize = 0
        Me.btnBrowseLogo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnBrowseLogo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnBrowseLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseLogo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnBrowseLogo.ForeColor = System.Drawing.Color.White
        Me.btnBrowseLogo.Location = New System.Drawing.Point(470, 35)
        Me.btnBrowseLogo.Name = "btnBrowseLogo"
        Me.btnBrowseLogo.Size = New System.Drawing.Size(100, 35)
        Me.btnBrowseLogo.TabIndex = 2
        Me.btnBrowseLogo.Text = "📁 Browse"
        Me.btnBrowseLogo.UseVisualStyleBackColor = False
        '
        'txtLogoPath
        '
        Me.txtLogoPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtLogoPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLogoPath.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtLogoPath.Location = New System.Drawing.Point(110, 38)
        Me.txtLogoPath.Name = "txtLogoPath"
        Me.txtLogoPath.ReadOnly = True
        Me.txtLogoPath.Size = New System.Drawing.Size(350, 27)
        Me.txtLogoPath.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(25, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 23)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Path File"
        '
        'gbDataPerusahaan
        '
        Me.gbDataPerusahaan.BackColor = System.Drawing.Color.White
        Me.gbDataPerusahaan.Controls.Add(Me.txtEmailPerusahaan)
        Me.gbDataPerusahaan.Controls.Add(Me.txtFaxPerusahaan)
        Me.gbDataPerusahaan.Controls.Add(Me.txtTeleponPerusahaan)
        Me.gbDataPerusahaan.Controls.Add(Me.txtKotaPerusahaan)
        Me.gbDataPerusahaan.Controls.Add(Me.txtAlamatPerusahaan)
        Me.gbDataPerusahaan.Controls.Add(Me.txtNamaPerusahaan)
        Me.gbDataPerusahaan.Controls.Add(Me.Label6)
        Me.gbDataPerusahaan.Controls.Add(Me.Label5)
        Me.gbDataPerusahaan.Controls.Add(Me.Label4)
        Me.gbDataPerusahaan.Controls.Add(Me.Label3)
        Me.gbDataPerusahaan.Controls.Add(Me.Label2)
        Me.gbDataPerusahaan.Controls.Add(Me.Label1)
        Me.gbDataPerusahaan.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.gbDataPerusahaan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.gbDataPerusahaan.Location = New System.Drawing.Point(18, 18)
        Me.gbDataPerusahaan.Name = "gbDataPerusahaan"
        Me.gbDataPerusahaan.Size = New System.Drawing.Size(700, 287)
        Me.gbDataPerusahaan.TabIndex = 3
        Me.gbDataPerusahaan.TabStop = False
        Me.gbDataPerusahaan.Text = "🏢 Data Perusahaan"
        '
        'txtEmailPerusahaan
        '
        Me.txtEmailPerusahaan.BackColor = System.Drawing.Color.White
        Me.txtEmailPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmailPerusahaan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtEmailPerusahaan.Location = New System.Drawing.Point(173, 190)
        Me.txtEmailPerusahaan.Name = "txtEmailPerusahaan"
        Me.txtEmailPerusahaan.Size = New System.Drawing.Size(241, 30)
        Me.txtEmailPerusahaan.TabIndex = 11
        '
        'txtFaxPerusahaan
        '
        Me.txtFaxPerusahaan.BackColor = System.Drawing.Color.White
        Me.txtFaxPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFaxPerusahaan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtFaxPerusahaan.Location = New System.Drawing.Point(173, 230)
        Me.txtFaxPerusahaan.Name = "txtFaxPerusahaan"
        Me.txtFaxPerusahaan.Size = New System.Drawing.Size(241, 30)
        Me.txtFaxPerusahaan.TabIndex = 10
        '
        'txtTeleponPerusahaan
        '
        Me.txtTeleponPerusahaan.BackColor = System.Drawing.Color.White
        Me.txtTeleponPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTeleponPerusahaan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtTeleponPerusahaan.Location = New System.Drawing.Point(173, 152)
        Me.txtTeleponPerusahaan.Name = "txtTeleponPerusahaan"
        Me.txtTeleponPerusahaan.Size = New System.Drawing.Size(241, 30)
        Me.txtTeleponPerusahaan.TabIndex = 6
        '
        'txtKotaPerusahaan
        '
        Me.txtKotaPerusahaan.BackColor = System.Drawing.Color.White
        Me.txtKotaPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKotaPerusahaan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtKotaPerusahaan.Location = New System.Drawing.Point(173, 112)
        Me.txtKotaPerusahaan.Name = "txtKotaPerusahaan"
        Me.txtKotaPerusahaan.Size = New System.Drawing.Size(416, 30)
        Me.txtKotaPerusahaan.TabIndex = 5
        '
        'txtAlamatPerusahaan
        '
        Me.txtAlamatPerusahaan.BackColor = System.Drawing.Color.White
        Me.txtAlamatPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlamatPerusahaan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtAlamatPerusahaan.Location = New System.Drawing.Point(173, 72)
        Me.txtAlamatPerusahaan.Name = "txtAlamatPerusahaan"
        Me.txtAlamatPerusahaan.Size = New System.Drawing.Size(416, 30)
        Me.txtAlamatPerusahaan.TabIndex = 3
        '
        'txtNamaPerusahaan
        '
        Me.txtNamaPerusahaan.BackColor = System.Drawing.Color.White
        Me.txtNamaPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNamaPerusahaan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNamaPerusahaan.Location = New System.Drawing.Point(173, 31)
        Me.txtNamaPerusahaan.Name = "txtNamaPerusahaan"
        Me.txtNamaPerusahaan.Size = New System.Drawing.Size(416, 30)
        Me.txtNamaPerusahaan.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(15, 193)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 20)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "📧 Email"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(15, 234)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 20)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "📠 Kode Pos"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(15, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "📞 Telepon"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(15, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "🏙️Kabupaten"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(15, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "🏠 Alamat"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(15, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "🏢 Nama Perusahaan"
        '
        'tabSettings
        '
        Me.tabSettings.Controls.Add(Me.tabPerusahaan)
        Me.tabSettings.Controls.Add(Me.tabTimbangan)
        Me.tabSettings.Controls.Add(Me.tabPrinter)
        Me.tabSettings.Controls.Add(Me.tabUmum)
        Me.tabSettings.Controls.Add(Me.tabDatabase)
        Me.tabSettings.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabSettings.Location = New System.Drawing.Point(20, 100)
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.SelectedIndex = 0
        Me.tabSettings.Size = New System.Drawing.Size(944, 700)
        Me.tabSettings.TabIndex = 0
        '
        'tabDatabase
        '
        Me.tabDatabase.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.tabDatabase.Controls.Add(Me.grpInfoKoneksi)
        Me.tabDatabase.Controls.Add(Me.Button1)
        Me.tabDatabase.Controls.Add(Me.grpKoneksiDB)
        Me.tabDatabase.Location = New System.Drawing.Point(4, 32)
        Me.tabDatabase.Name = "tabDatabase"
        Me.tabDatabase.Padding = New System.Windows.Forms.Padding(15)
        Me.tabDatabase.Size = New System.Drawing.Size(936, 664)
        Me.tabDatabase.TabIndex = 5
        Me.tabDatabase.Text = "🗄️ Database"
        '
        'grpInfoKoneksi
        '
        Me.grpInfoKoneksi.BackColor = System.Drawing.Color.White
        Me.grpInfoKoneksi.Controls.Add(Me.lblCurrentAuth)
        Me.grpInfoKoneksi.Controls.Add(Me.lblCurrentDB)
        Me.grpInfoKoneksi.Controls.Add(Me.lblCurrentServer)
        Me.grpInfoKoneksi.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grpInfoKoneksi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.grpInfoKoneksi.Location = New System.Drawing.Point(18, 380)
        Me.grpInfoKoneksi.Name = "grpInfoKoneksi"
        Me.grpInfoKoneksi.Size = New System.Drawing.Size(700, 140)
        Me.grpInfoKoneksi.TabIndex = 2
        Me.grpInfoKoneksi.TabStop = False
        Me.grpInfoKoneksi.Text = "ℹ Informasi Koneksi Aktif"
        '
        'lblCurrentAuth
        '
        Me.lblCurrentAuth.AutoSize = True
        Me.lblCurrentAuth.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblCurrentAuth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblCurrentAuth.Location = New System.Drawing.Point(25, 100)
        Me.lblCurrentAuth.Name = "lblCurrentAuth"
        Me.lblCurrentAuth.Size = New System.Drawing.Size(115, 23)
        Me.lblCurrentAuth.TabIndex = 2
        Me.lblCurrentAuth.Text = "Autentikasi : -"
        '
        'lblCurrentDB
        '
        Me.lblCurrentDB.AutoSize = True
        Me.lblCurrentDB.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblCurrentDB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblCurrentDB.Location = New System.Drawing.Point(25, 70)
        Me.lblCurrentDB.Name = "lblCurrentDB"
        Me.lblCurrentDB.Size = New System.Drawing.Size(117, 23)
        Me.lblCurrentDB.TabIndex = 1
        Me.lblCurrentDB.Text = "Database    : -"
        '
        'lblCurrentServer
        '
        Me.lblCurrentServer.AutoSize = True
        Me.lblCurrentServer.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblCurrentServer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblCurrentServer.Location = New System.Drawing.Point(25, 40)
        Me.lblCurrentServer.Name = "lblCurrentServer"
        Me.lblCurrentServer.Size = New System.Drawing.Size(118, 23)
        Me.lblCurrentServer.TabIndex = 0
        Me.lblCurrentServer.Text = "Server         : -"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(111, 383)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(8, 8)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'grpKoneksiDB
        '
        Me.grpKoneksiDB.BackColor = System.Drawing.Color.White
        Me.grpKoneksiDB.Controls.Add(Me.pnlStatusDB)
        Me.grpKoneksiDB.Controls.Add(Me.btnSimpanDB)
        Me.grpKoneksiDB.Controls.Add(Me.btnTestDB)
        Me.grpKoneksiDB.Controls.Add(Me.grpAuthentication)
        Me.grpKoneksiDB.Controls.Add(Me.txtDatabaseName)
        Me.grpKoneksiDB.Controls.Add(Me.lblDatabaseName)
        Me.grpKoneksiDB.Controls.Add(Me.txtServerName)
        Me.grpKoneksiDB.Controls.Add(Me.lblServerName)
        Me.grpKoneksiDB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grpKoneksiDB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.grpKoneksiDB.Location = New System.Drawing.Point(18, 20)
        Me.grpKoneksiDB.Name = "grpKoneksiDB"
        Me.grpKoneksiDB.Size = New System.Drawing.Size(880, 340)
        Me.grpKoneksiDB.TabIndex = 0
        Me.grpKoneksiDB.TabStop = False
        Me.grpKoneksiDB.Text = "⚙ Pengaturan Koneksi Database"
        '
        'pnlStatusDB
        '
        Me.pnlStatusDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnlStatusDB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatusDB.Controls.Add(Me.lblStatusDB)
        Me.pnlStatusDB.Location = New System.Drawing.Point(450, 200)
        Me.pnlStatusDB.Name = "pnlStatusDB"
        Me.pnlStatusDB.Size = New System.Drawing.Size(400, 60)
        Me.pnlStatusDB.TabIndex = 7
        '
        'lblStatusDB
        '
        Me.lblStatusDB.AutoSize = True
        Me.lblStatusDB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusDB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStatusDB.Location = New System.Drawing.Point(15, 18)
        Me.lblStatusDB.Name = "lblStatusDB"
        Me.lblStatusDB.Size = New System.Drawing.Size(206, 23)
        Me.lblStatusDB.TabIndex = 8
        Me.lblStatusDB.Text = "Status : Belum ditest ⚠️"
        '
        'btnSimpanDB
        '
        Me.btnSimpanDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.btnSimpanDB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSimpanDB.FlatAppearance.BorderSize = 0
        Me.btnSimpanDB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.btnSimpanDB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnSimpanDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSimpanDB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSimpanDB.ForeColor = System.Drawing.Color.White
        Me.btnSimpanDB.Location = New System.Drawing.Point(450, 100)
        Me.btnSimpanDB.Name = "btnSimpanDB"
        Me.btnSimpanDB.Size = New System.Drawing.Size(180, 50)
        Me.btnSimpanDB.TabIndex = 6
        Me.btnSimpanDB.Text = "💾 Simpan && Terapkan"
        Me.btnSimpanDB.UseVisualStyleBackColor = False
        '
        'btnTestDB
        '
        Me.btnTestDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnTestDB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTestDB.FlatAppearance.BorderSize = 0
        Me.btnTestDB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnTestDB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnTestDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTestDB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTestDB.ForeColor = System.Drawing.Color.White
        Me.btnTestDB.Location = New System.Drawing.Point(450, 40)
        Me.btnTestDB.Name = "btnTestDB"
        Me.btnTestDB.Size = New System.Drawing.Size(180, 50)
        Me.btnTestDB.TabIndex = 5
        Me.btnTestDB.Text = "🔌 Test Koneksi"
        Me.btnTestDB.UseVisualStyleBackColor = False
        '
        'grpAuthentication
        '
        Me.grpAuthentication.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grpAuthentication.Controls.Add(Me.txtSQLPassword)
        Me.grpAuthentication.Controls.Add(Me.lblPassword)
        Me.grpAuthentication.Controls.Add(Me.txtSQLUsername)
        Me.grpAuthentication.Controls.Add(Me.lblUsername)
        Me.grpAuthentication.Controls.Add(Me.rbSQLAuth)
        Me.grpAuthentication.Controls.Add(Me.rbWindowsAuth)
        Me.grpAuthentication.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpAuthentication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer))
        Me.grpAuthentication.Location = New System.Drawing.Point(25, 130)
        Me.grpAuthentication.Name = "grpAuthentication"
        Me.grpAuthentication.Size = New System.Drawing.Size(400, 180)
        Me.grpAuthentication.TabIndex = 4
        Me.grpAuthentication.TabStop = False
        Me.grpAuthentication.Text = "🔐 Metode Autentikasi"
        '
        'txtSQLPassword
        '
        Me.txtSQLPassword.BackColor = System.Drawing.Color.White
        Me.txtSQLPassword.Enabled = False
        Me.txtSQLPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtSQLPassword.Location = New System.Drawing.Point(140, 130)
        Me.txtSQLPassword.Name = "txtSQLPassword"
        Me.txtSQLPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSQLPassword.Size = New System.Drawing.Size(240, 30)
        Me.txtSQLPassword.TabIndex = 5
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Enabled = False
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblPassword.Location = New System.Drawing.Point(30, 133)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(94, 23)
        Me.lblPassword.TabIndex = 4
        Me.lblPassword.Text = "Password  :"
        '
        'txtSQLUsername
        '
        Me.txtSQLUsername.BackColor = System.Drawing.Color.White
        Me.txtSQLUsername.Enabled = False
        Me.txtSQLUsername.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtSQLUsername.Location = New System.Drawing.Point(140, 95)
        Me.txtSQLUsername.Name = "txtSQLUsername"
        Me.txtSQLUsername.Size = New System.Drawing.Size(240, 30)
        Me.txtSQLUsername.TabIndex = 3
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Enabled = False
        Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblUsername.Location = New System.Drawing.Point(30, 98)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(96, 23)
        Me.lblUsername.TabIndex = 2
        Me.lblUsername.Text = "Username :"
        '
        'rbSQLAuth
        '
        Me.rbSQLAuth.AutoSize = True
        Me.rbSQLAuth.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.rbSQLAuth.Location = New System.Drawing.Point(15, 65)
        Me.rbSQLAuth.Name = "rbSQLAuth"
        Me.rbSQLAuth.Size = New System.Drawing.Size(231, 27)
        Me.rbSQLAuth.TabIndex = 1
        Me.rbSQLAuth.TabStop = True
        Me.rbSQLAuth.Text = "SQL Server Authentication"
        Me.rbSQLAuth.UseVisualStyleBackColor = True
        '
        'rbWindowsAuth
        '
        Me.rbWindowsAuth.AutoSize = True
        Me.rbWindowsAuth.Checked = True
        Me.rbWindowsAuth.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.rbWindowsAuth.Location = New System.Drawing.Point(15, 35)
        Me.rbWindowsAuth.Name = "rbWindowsAuth"
        Me.rbWindowsAuth.Size = New System.Drawing.Size(348, 27)
        Me.rbWindowsAuth.TabIndex = 0
        Me.rbWindowsAuth.TabStop = True
        Me.rbWindowsAuth.Text = "Windows Authentication (Recommended)"
        Me.rbWindowsAuth.UseVisualStyleBackColor = True
        '
        'txtDatabaseName
        '
        Me.txtDatabaseName.BackColor = System.Drawing.Color.White
        Me.txtDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDatabaseName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDatabaseName.Location = New System.Drawing.Point(175, 80)
        Me.txtDatabaseName.Name = "txtDatabaseName"
        Me.txtDatabaseName.Size = New System.Drawing.Size(250, 30)
        Me.txtDatabaseName.TabIndex = 3
        Me.txtDatabaseName.Text = "Sistem Timbangan PKS"
        '
        'lblDatabaseName
        '
        Me.lblDatabaseName.AutoSize = True
        Me.lblDatabaseName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblDatabaseName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblDatabaseName.Location = New System.Drawing.Point(25, 83)
        Me.lblDatabaseName.Name = "lblDatabaseName"
        Me.lblDatabaseName.Padding = New System.Windows.Forms.Padding(1, 0, 0, 1)
        Me.lblDatabaseName.Size = New System.Drawing.Size(142, 24)
        Me.lblDatabaseName.TabIndex = 2
        Me.lblDatabaseName.Text = "Database Name :"
        '
        'txtServerName
        '
        Me.txtServerName.BackColor = System.Drawing.Color.White
        Me.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServerName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtServerName.Location = New System.Drawing.Point(175, 40)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(250, 30)
        Me.txtServerName.TabIndex = 1
        Me.txtServerName.Text = ".\SQLEXPRESS"
        '
        'lblServerName
        '
        Me.lblServerName.AutoSize = True
        Me.lblServerName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblServerName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lblServerName.Location = New System.Drawing.Point(25, 43)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Padding = New System.Windows.Forms.Padding(1, 0, 0, 1)
        Me.lblServerName.Size = New System.Drawing.Size(143, 24)
        Me.lblServerName.TabIndex = 0
        Me.lblServerName.Text = "Server Name      :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(20, 35)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(46, 20)
        Me.Label23.TabIndex = 0
        '
        'txtPrefix
        '
        Me.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrefix.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrefix.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrefix.Location = New System.Drawing.Point(72, 33)
        Me.txtPrefix.MaxLength = 5
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(80, 27)
        Me.txtPrefix.TabIndex = 1
        Me.txtPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(200, 35)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(92, 20)
        Me.Label24.TabIndex = 2
        '
        'nudDigit
        '
        Me.nudDigit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.nudDigit.Location = New System.Drawing.Point(298, 32)
        Me.nudDigit.Maximum = New Decimal(New Integer() {6, 0, 0, 0})
        Me.nudDigit.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.nudDigit.Name = "nudDigit"
        Me.nudDigit.Size = New System.Drawing.Size(60, 27)
        Me.nudDigit.TabIndex = 3
        Me.nudDigit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudDigit.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'chkResetHarian
        '
        Me.chkResetHarian.AutoSize = True
        Me.chkResetHarian.Checked = True
        Me.chkResetHarian.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkResetHarian.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.chkResetHarian.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkResetHarian.Location = New System.Drawing.Point(20, 70)
        Me.chkResetHarian.Name = "chkResetHarian"
        Me.chkResetHarian.Size = New System.Drawing.Size(335, 24)
        Me.chkResetHarian.TabIndex = 4
        Me.chkResetHarian.Text = "Reset nomor urut setiap hari (mulai dari 0001)"
        Me.chkResetHarian.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(20, 110)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(118, 20)
        Me.Label25.TabIndex = 5
        '
        'lblPreviewFormat
        '
        Me.lblPreviewFormat.AutoSize = True
        Me.lblPreviewFormat.Font = New System.Drawing.Font("Consolas", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblPreviewFormat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblPreviewFormat.Location = New System.Drawing.Point(144, 105)
        Me.lblPreviewFormat.Name = "lblPreviewFormat"
        Me.lblPreviewFormat.Size = New System.Drawing.Size(116, 28)
        Me.lblPreviewFormat.TabIndex = 6
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Italic)
        Me.Label26.Location = New System.Drawing.Point(257, 110)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(226, 19)
        Me.Label26.TabIndex = 7
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(20, 35)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(61, 20)
        Me.Label27.TabIndex = 0
        '
        'lblTanggalCounter
        '
        Me.lblTanggalCounter.AutoSize = True
        Me.lblTanggalCounter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTanggalCounter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(114, Byte), Integer))
        Me.lblTanggalCounter.Location = New System.Drawing.Point(100, 35)
        Me.lblTanggalCounter.Name = "lblTanggalCounter"
        Me.lblTanggalCounter.Size = New System.Drawing.Size(122, 20)
        Me.lblTanggalCounter.TabIndex = 1
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(20, 70)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(151, 20)
        Me.Label28.TabIndex = 2
        '
        'lblCounterValue
        '
        Me.lblCounterValue.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblCounterValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCounterValue.Location = New System.Drawing.Point(165, 60)
        Me.lblCounterValue.Name = "lblCounterValue"
        Me.lblCounterValue.Size = New System.Drawing.Size(31, 42)
        Me.lblCounterValue.TabIndex = 3
        '
        'btnResetCounter
        '
        Me.btnResetCounter.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnResetCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetCounter.ForeColor = System.Drawing.Color.Black
        Me.btnResetCounter.Location = New System.Drawing.Point(250, 63)
        Me.btnResetCounter.Name = "btnResetCounter"
        Me.btnResetCounter.Size = New System.Drawing.Size(145, 32)
        Me.btnResetCounter.TabIndex = 0
        Me.btnResetCounter.Text = "🔄 Reset Counter"
        Me.btnResetCounter.UseVisualStyleBackColor = False
        '
        'lblWarningCounter
        '
        Me.lblWarningCounter.AutoSize = True
        Me.lblWarningCounter.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarningCounter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblWarningCounter.Location = New System.Drawing.Point(20, 100)
        Me.lblWarningCounter.Name = "lblWarningCounter"
        Me.lblWarningCounter.Size = New System.Drawing.Size(483, 17)
        Me.lblWarningCounter.TabIndex = 4
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeaderSubtitle)
        Me.pnlHeader.Controls.Add(Me.lblHeaderTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1164, 80)
        Me.pnlHeader.TabIndex = 4
        '
        'lblHeaderSubtitle
        '
        Me.lblHeaderSubtitle.AutoSize = True
        Me.lblHeaderSubtitle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblHeaderSubtitle.Location = New System.Drawing.Point(25, 50)
        Me.lblHeaderSubtitle.Name = "lblHeaderSubtitle"
        Me.lblHeaderSubtitle.Size = New System.Drawing.Size(329, 23)
        Me.lblHeaderSubtitle.TabIndex = 1
        Me.lblHeaderSubtitle.Text = "Atur konfigurasi aplikasi sesuai kebutuhan"
        '
        'lblHeaderTitle
        '
        Me.lblHeaderTitle.AutoSize = True
        Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeaderTitle.ForeColor = System.Drawing.Color.White
        Me.lblHeaderTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblHeaderTitle.Name = "lblHeaderTitle"
        Me.lblHeaderTitle.Size = New System.Drawing.Size(303, 37)
        Me.lblHeaderTitle.TabIndex = 0
        Me.lblHeaderTitle.Text = "⚙️ Pengaturan Sistem"
        '
        'pnlSidebar
        '
        Me.pnlSidebar.BackColor = System.Drawing.Color.White
        Me.pnlSidebar.Controls.Add(Me.btnResetDefault)
        Me.pnlSidebar.Controls.Add(Me.btnTutup)
        Me.pnlSidebar.Controls.Add(Me.btnSimpan)
        Me.pnlSidebar.Controls.Add(Me.btnBatal)
        Me.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSidebar.Location = New System.Drawing.Point(964, 80)
        Me.pnlSidebar.Name = "pnlSidebar"
        Me.pnlSidebar.Size = New System.Drawing.Size(200, 720)
        Me.pnlSidebar.TabIndex = 5
        '
        'FormPengaturan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1184, 700)
        Me.Controls.Add(Me.pnlSidebar)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.tabSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormPengaturan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Pengaturan Sistem"
        Me.tabUmum.ResumeLayout(False)
        Me.gbInfoAplikasi.ResumeLayout(False)
        Me.gbInfoAplikasi.PerformLayout()
        Me.gbValidasi.ResumeLayout(False)
        Me.gbValidasi.PerformLayout()
        Me.gbPotongan.ResumeLayout(False)
        Me.gbPotongan.PerformLayout()
        CType(Me.nudPotonganDefault, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPrinter.ResumeLayout(False)
        Me.gbPengaturanPrinter.ResumeLayout(False)
        Me.gbPengaturanPrinter.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCopies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabTimbangan.ResumeLayout(False)
        Me.gbSpesifikasi.ResumeLayout(False)
        Me.gbSpesifikasi.PerformLayout()
        Me.gbKoneksiSerial.ResumeLayout(False)
        Me.gbKoneksiSerial.PerformLayout()
        Me.tabPerusahaan.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDataPerusahaan.ResumeLayout(False)
        Me.gbDataPerusahaan.PerformLayout()
        Me.tabSettings.ResumeLayout(False)
        Me.tabDatabase.ResumeLayout(False)
        Me.grpInfoKoneksi.ResumeLayout(False)
        Me.grpInfoKoneksi.PerformLayout()
        Me.grpKoneksiDB.ResumeLayout(False)
        Me.grpKoneksiDB.PerformLayout()
        Me.pnlStatusDB.ResumeLayout(False)
        Me.pnlStatusDB.PerformLayout()
        Me.grpAuthentication.ResumeLayout(False)
        Me.grpAuthentication.PerformLayout()
        CType(Me.nudDigit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlSidebar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnResetDefault As Button
    Friend WithEvents btnSimpan As Button
    Friend WithEvents btnBatal As Button
    Friend WithEvents btnTutup As Button
    Friend WithEvents tabUmum As TabPage
    Friend WithEvents tabPrinter As TabPage
    Friend WithEvents gbPengaturanPrinter As GroupBox
    Friend WithEvents lblJumlahCopy As Label
    Friend WithEvents cmbPaperSize As ComboBox
    Friend WithEvents lblUkuranKertas As Label
    Friend WithEvents cmbPrinter As ComboBox
    Friend WithEvents lblNamaPrinter As Label
    Friend WithEvents tabTimbangan As TabPage
    Friend WithEvents gbSpesifikasi As GroupBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents txtDivisi As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents txtKapasitas As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents gbKoneksiSerial As GroupBox
    Friend WithEvents lblStatusKoneksi As Label
    Friend WithEvents btnTestKoneksi As Button
    Friend WithEvents cmbProtocol As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents cmbStopBits As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cmbParity As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbDataBits As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents cmbBaudRate As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents cmbComPort As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents tabPerusahaan As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents picLogo As PictureBox
    Friend WithEvents btnHapusLogo As Button
    Friend WithEvents btnBrowseLogo As Button
    Friend WithEvents txtLogoPath As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtEmailPerusahaan As TextBox
    Friend WithEvents txtFaxPerusahaan As TextBox
    Friend WithEvents txtTeleponPerusahaan As TextBox
    Friend WithEvents txtKotaPerusahaan As TextBox
    Friend WithEvents txtAlamatPerusahaan As TextBox
    Friend WithEvents txtNamaPerusahaan As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents tabSettings As TabControl
    Friend WithEvents gbPotongan As GroupBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents nudPotonganDefault As NumericUpDown
    Friend WithEvents gbValidasi As GroupBox
    Friend WithEvents chkKonfirmasiHapus As CheckBox
    Friend WithEvents chkWajibTransporter As CheckBox
    Friend WithEvents chkWajibProduk As CheckBox
    Friend WithEvents chkWajibSupplier As CheckBox
    Friend WithEvents gbInfoAplikasi As GroupBox
    Friend WithEvents lblNamaAplikasi As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents chkBackupOtomatis As CheckBox
    Friend WithEvents lblDeveloper As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents lblVersi As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents nudCopies As NumericUpDown
    Friend WithEvents tabDatabase As TabPage
    Friend WithEvents grpKoneksiDB As GroupBox
    Friend WithEvents lblServerName As Label
    Friend WithEvents grpAuthentication As GroupBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents rbSQLAuth As RadioButton
    Friend WithEvents rbWindowsAuth As RadioButton
    Friend WithEvents txtDatabaseName As TextBox
    Friend WithEvents lblDatabaseName As Label
    Friend WithEvents txtServerName As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents btnSimpanDB As Button
    Friend WithEvents btnTestDB As Button
    Friend WithEvents txtSQLPassword As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtSQLUsername As TextBox
    Friend WithEvents grpInfoKoneksi As GroupBox
    Friend WithEvents lblCurrentDB As Label
    Friend WithEvents lblCurrentServer As Label
    Friend WithEvents pnlStatusDB As Panel
    Friend WithEvents lblStatusDB As Label
    Friend WithEvents lblCurrentAuth As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label20 As Label
    Friend WithEvents cmbPaperSizeTkt As ComboBox
    Friend WithEvents Label21 As Label
    Friend WithEvents CmbPrinterTkt As ComboBox
    Friend WithEvents Label22 As Label
    Friend WithEvents chkAutoPrint As CheckBox
    Friend WithEvents Label23 As Label
    Friend WithEvents txtPrefix As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents nudDigit As NumericUpDown
    Friend WithEvents chkResetHarian As CheckBox
    Friend WithEvents Label25 As Label
    Friend WithEvents lblPreviewFormat As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents lblTanggalCounter As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents lblCounterValue As Label
    Friend WithEvents btnResetCounter As Button
    Friend WithEvents lblWarningCounter As Label
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblHeaderTitle As Label
    Friend WithEvents lblHeaderSubtitle As Label
    Friend WithEvents pnlSidebar As Panel
    Friend WithEvents gbDataPerusahaan As GroupBox
    Friend WithEvents lblMerekIndikator As Label
    Friend WithEvents cmbMerekIndikator As ComboBox
    Friend WithEvents cmbTipeIndikator As ComboBox
    Friend WithEvents Label36 As Label
End Class