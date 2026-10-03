<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormDashboard
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
        Me.components = New System.ComponentModel.Container()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.lblTanggal = New System.Windows.Forms.Label()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.btnInputTimbangan = New System.Windows.Forms.Button()
        Me.btnDaftarTimbangan = New System.Windows.Forms.Button()
        Me.btnLaporan = New System.Windows.Forms.Button()
        Me.btnMasterData = New System.Windows.Forms.Button()
        Me.btnManajemenUser = New System.Windows.Forms.Button()
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.btnAuditLog = New System.Windows.Forms.Button()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.PanelDateContainer = New System.Windows.Forms.Panel()
        Me.lblJam = New System.Windows.Forms.Label()
        Me.PanelContent = New System.Windows.Forms.Panel()
        Me.PanelStats = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelStat3 = New System.Windows.Forms.Panel()
        Me.lblStat3Icon = New System.Windows.Forms.Label()
        Me.lblStat3Value = New System.Windows.Forms.Label()
        Me.lblStat3Title = New System.Windows.Forms.Label()
        Me.PanelStat2 = New System.Windows.Forms.Panel()
        Me.lblStat2Icon = New System.Windows.Forms.Label()
        Me.lblStat2Value = New System.Windows.Forms.Label()
        Me.lblStat2Title = New System.Windows.Forms.Label()
        Me.PanelStat1 = New System.Windows.Forms.Panel()
        Me.lblStat1Icon = New System.Windows.Forms.Label()
        Me.lblStat1Value = New System.Windows.Forms.Label()
        Me.lblStat1Title = New System.Windows.Forms.Label()
        Me.PanelTitle = New System.Windows.Forms.Panel()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.TimerJam = New System.Windows.Forms.Timer(Me.components)
        Me.PanelLogo = New System.Windows.Forms.Panel()
        Me.lblAppName = New System.Windows.Forms.Label()
        Me.PanelSidebar = New System.Windows.Forms.Panel()
        Me.PanelMenuContainer = New System.Windows.Forms.Panel()
        Me.lblMenuTitle = New System.Windows.Forms.Label()
        Me.PanelHeader.SuspendLayout()
        Me.PanelDateContainer.SuspendLayout()
        Me.PanelContent.SuspendLayout()
        Me.PanelStats.SuspendLayout()
        Me.PanelStat3.SuspendLayout()
        Me.PanelStat2.SuspendLayout()
        Me.PanelStat1.SuspendLayout()
        Me.PanelTitle.SuspendLayout()
        Me.PanelLogo.SuspendLayout()
        Me.PanelSidebar.SuspendLayout()
        Me.PanelMenuContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblUser.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblUser.Location = New System.Drawing.Point(30, 20)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Padding = New System.Windows.Forms.Padding(0, 20, 0, 0)
        Me.lblUser.Size = New System.Drawing.Size(525, 48)
        Me.lblUser.TabIndex = 0
        Me.lblUser.Text = "Selamat Datang, System Administrator (Programmer)!"
        Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTanggal
        '
        Me.lblTanggal.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblTanggal.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblTanggal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblTanggal.Location = New System.Drawing.Point(0, 50)
        Me.lblTanggal.Name = "lblTanggal"
        Me.lblTanggal.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.lblTanggal.Size = New System.Drawing.Size(350, 30)
        Me.lblTanggal.TabIndex = 1
        Me.lblTanggal.Text = "Jumat, 30 Januari 2026"
        Me.lblTanggal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnLogout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnLogout.Location = New System.Drawing.Point(0, 680)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.btnLogout.Size = New System.Drawing.Size(260, 60)
        Me.btnLogout.TabIndex = 3
        Me.btnLogout.Text = "⏻   LOGOUT"
        Me.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'btnInputTimbangan
        '
        Me.btnInputTimbangan.BackColor = System.Drawing.Color.Transparent
        Me.btnInputTimbangan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnInputTimbangan.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnInputTimbangan.FlatAppearance.BorderSize = 0
        Me.btnInputTimbangan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnInputTimbangan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnInputTimbangan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInputTimbangan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnInputTimbangan.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnInputTimbangan.Location = New System.Drawing.Point(0, 40)
        Me.btnInputTimbangan.Name = "btnInputTimbangan"
        Me.btnInputTimbangan.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
        Me.btnInputTimbangan.Size = New System.Drawing.Size(260, 55)
        Me.btnInputTimbangan.TabIndex = 3
        Me.btnInputTimbangan.Text = "⚖   Input Timbangan"
        Me.btnInputTimbangan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInputTimbangan.UseVisualStyleBackColor = False
        '
        'btnDaftarTimbangan
        '
        Me.btnDaftarTimbangan.BackColor = System.Drawing.Color.Transparent
        Me.btnDaftarTimbangan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDaftarTimbangan.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnDaftarTimbangan.FlatAppearance.BorderSize = 0
        Me.btnDaftarTimbangan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnDaftarTimbangan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnDaftarTimbangan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDaftarTimbangan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnDaftarTimbangan.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnDaftarTimbangan.Location = New System.Drawing.Point(0, 95)
        Me.btnDaftarTimbangan.Name = "btnDaftarTimbangan"
        Me.btnDaftarTimbangan.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
        Me.btnDaftarTimbangan.Size = New System.Drawing.Size(260, 55)
        Me.btnDaftarTimbangan.TabIndex = 3
        Me.btnDaftarTimbangan.Text = "📋   Daftar Timbangan"
        Me.btnDaftarTimbangan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDaftarTimbangan.UseVisualStyleBackColor = False
        '
        'btnLaporan
        '
        Me.btnLaporan.BackColor = System.Drawing.Color.Transparent
        Me.btnLaporan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLaporan.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnLaporan.FlatAppearance.BorderSize = 0
        Me.btnLaporan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnLaporan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnLaporan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLaporan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnLaporan.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnLaporan.Location = New System.Drawing.Point(0, 150)
        Me.btnLaporan.Name = "btnLaporan"
        Me.btnLaporan.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
        Me.btnLaporan.Size = New System.Drawing.Size(260, 55)
        Me.btnLaporan.TabIndex = 3
        Me.btnLaporan.Text = "📊   Laporan"
        Me.btnLaporan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLaporan.UseVisualStyleBackColor = False
        '
        'btnMasterData
        '
        Me.btnMasterData.BackColor = System.Drawing.Color.Transparent
        Me.btnMasterData.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMasterData.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMasterData.FlatAppearance.BorderSize = 0
        Me.btnMasterData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnMasterData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnMasterData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMasterData.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnMasterData.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnMasterData.Location = New System.Drawing.Point(0, 315)
        Me.btnMasterData.Name = "btnMasterData"
        Me.btnMasterData.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
        Me.btnMasterData.Size = New System.Drawing.Size(260, 55)
        Me.btnMasterData.TabIndex = 3
        Me.btnMasterData.Text = "📁   Master Data"
        Me.btnMasterData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMasterData.UseVisualStyleBackColor = False
        '
        'btnManajemenUser
        '
        Me.btnManajemenUser.BackColor = System.Drawing.Color.Transparent
        Me.btnManajemenUser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnManajemenUser.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnManajemenUser.FlatAppearance.BorderSize = 0
        Me.btnManajemenUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnManajemenUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnManajemenUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnManajemenUser.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnManajemenUser.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnManajemenUser.Location = New System.Drawing.Point(0, 205)
        Me.btnManajemenUser.Name = "btnManajemenUser"
        Me.btnManajemenUser.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
        Me.btnManajemenUser.Size = New System.Drawing.Size(260, 55)
        Me.btnManajemenUser.TabIndex = 3
        Me.btnManajemenUser.Text = "👥   Manajemen User"
        Me.btnManajemenUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnManajemenUser.UseVisualStyleBackColor = False
        '
        'btnSettings
        '
        Me.btnSettings.BackColor = System.Drawing.Color.Transparent
        Me.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnSettings.FlatAppearance.BorderSize = 0
        Me.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSettings.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnSettings.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnSettings.Location = New System.Drawing.Point(0, 260)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
        Me.btnSettings.Size = New System.Drawing.Size(260, 55)
        Me.btnSettings.TabIndex = 3
        Me.btnSettings.Text = "⚙   Pengaturan"
        Me.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSettings.UseVisualStyleBackColor = False
        '
        'btnAuditLog
        '
        Me.btnAuditLog.BackColor = System.Drawing.Color.Transparent
        Me.btnAuditLog.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAuditLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnAuditLog.FlatAppearance.BorderSize = 0
        Me.btnAuditLog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnAuditLog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnAuditLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAuditLog.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnAuditLog.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnAuditLog.Location = New System.Drawing.Point(0, 370)
        Me.btnAuditLog.Name = "btnAuditLog"
        Me.btnAuditLog.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
        Me.btnAuditLog.Size = New System.Drawing.Size(260, 55)
        Me.btnAuditLog.TabIndex = 0
        Me.btnAuditLog.Text = "📝   Audit Log"
        Me.btnAuditLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAuditLog.UseVisualStyleBackColor = False
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.PanelHeader.Controls.Add(Me.PanelDateContainer)
        Me.PanelHeader.Controls.Add(Me.lblUser)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(260, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Padding = New System.Windows.Forms.Padding(30, 20, 30, 20)
        Me.PanelHeader.Size = New System.Drawing.Size(922, 120)
        Me.PanelHeader.TabIndex = 3
        '
        'PanelDateContainer
        '
        Me.PanelDateContainer.Controls.Add(Me.lblJam)
        Me.PanelDateContainer.Controls.Add(Me.lblTanggal)
        Me.PanelDateContainer.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelDateContainer.Location = New System.Drawing.Point(542, 20)
        Me.PanelDateContainer.Name = "PanelDateContainer"
        Me.PanelDateContainer.Size = New System.Drawing.Size(350, 80)
        Me.PanelDateContainer.TabIndex = 3
        '
        'lblJam
        '
        Me.lblJam.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblJam.Font = New System.Drawing.Font("Consolas", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblJam.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblJam.Location = New System.Drawing.Point(0, 0)
        Me.lblJam.Name = "lblJam"
        Me.lblJam.Size = New System.Drawing.Size(350, 50)
        Me.lblJam.TabIndex = 2
        Me.lblJam.Text = "19:18:06"
        Me.lblJam.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'PanelContent
        '
        Me.PanelContent.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.PanelContent.Controls.Add(Me.PanelStats)
        Me.PanelContent.Controls.Add(Me.PanelTitle)
        Me.PanelContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelContent.Location = New System.Drawing.Point(260, 120)
        Me.PanelContent.Name = "PanelContent"
        Me.PanelContent.Padding = New System.Windows.Forms.Padding(50)
        Me.PanelContent.Size = New System.Drawing.Size(922, 620)
        Me.PanelContent.TabIndex = 4
        '
        'PanelStats
        '
        Me.PanelStats.ColumnCount = 3
        Me.PanelStats.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.PanelStats.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.PanelStats.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.PanelStats.Controls.Add(Me.PanelStat3, 2, 0)
        Me.PanelStats.Controls.Add(Me.PanelStat2, 1, 0)
        Me.PanelStats.Controls.Add(Me.PanelStat1, 0, 0)
        Me.PanelStats.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelStats.Location = New System.Drawing.Point(50, 236)
        Me.PanelStats.Name = "PanelStats"
        Me.PanelStats.RowCount = 1
        Me.PanelStats.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.PanelStats.Size = New System.Drawing.Size(822, 220)
        Me.PanelStats.TabIndex = 1
        '
        'PanelStat3
        '
        Me.PanelStat3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.PanelStat3.Controls.Add(Me.lblStat3Icon)
        Me.PanelStat3.Controls.Add(Me.lblStat3Value)
        Me.PanelStat3.Controls.Add(Me.lblStat3Title)
        Me.PanelStat3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelStat3.Location = New System.Drawing.Point(568, 20)
        Me.PanelStat3.Margin = New System.Windows.Forms.Padding(20)
        Me.PanelStat3.Name = "PanelStat3"
        Me.PanelStat3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
        Me.PanelStat3.Size = New System.Drawing.Size(234, 180)
        Me.PanelStat3.TabIndex = 2
        '
        'lblStat3Icon
        '
        Me.lblStat3Icon.AutoSize = True
        Me.lblStat3Icon.Font = New System.Drawing.Font("Segoe UI Emoji", 28.0!)
        Me.lblStat3Icon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStat3Icon.Location = New System.Drawing.Point(15, 15)
        Me.lblStat3Icon.Name = "lblStat3Icon"
        Me.lblStat3Icon.Size = New System.Drawing.Size(92, 63)
        Me.lblStat3Icon.TabIndex = 6
        Me.lblStat3Icon.Text = "🚛"
        '
        'lblStat3Value
        '
        Me.lblStat3Value.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStat3Value.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblStat3Value.ForeColor = System.Drawing.Color.White
        Me.lblStat3Value.Location = New System.Drawing.Point(0, 67)
        Me.lblStat3Value.Name = "lblStat3Value"
        Me.lblStat3Value.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.lblStat3Value.Size = New System.Drawing.Size(234, 71)
        Me.lblStat3Value.TabIndex = 3
        Me.lblStat3Value.Text = "0"
        Me.lblStat3Value.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lblStat3Title
        '
        Me.lblStat3Title.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.lblStat3Title.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStat3Title.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStat3Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStat3Title.Location = New System.Drawing.Point(0, 138)
        Me.lblStat3Title.Name = "lblStat3Title"
        Me.lblStat3Title.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.lblStat3Title.Size = New System.Drawing.Size(234, 36)
        Me.lblStat3Title.TabIndex = 0
        Me.lblStat3Title.Text = "ANTRIAN (BELUM KELUAR)"
        Me.lblStat3Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelStat2
        '
        Me.PanelStat2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.PanelStat2.Controls.Add(Me.lblStat2Icon)
        Me.PanelStat2.Controls.Add(Me.lblStat2Value)
        Me.PanelStat2.Controls.Add(Me.lblStat2Title)
        Me.PanelStat2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelStat2.Location = New System.Drawing.Point(294, 20)
        Me.PanelStat2.Margin = New System.Windows.Forms.Padding(20)
        Me.PanelStat2.Name = "PanelStat2"
        Me.PanelStat2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
        Me.PanelStat2.Size = New System.Drawing.Size(234, 180)
        Me.PanelStat2.TabIndex = 1
        '
        'lblStat2Icon
        '
        Me.lblStat2Icon.AutoSize = True
        Me.lblStat2Icon.Font = New System.Drawing.Font("Segoe UI Emoji", 28.0!)
        Me.lblStat2Icon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblStat2Icon.Location = New System.Drawing.Point(15, 15)
        Me.lblStat2Icon.Name = "lblStat2Icon"
        Me.lblStat2Icon.Size = New System.Drawing.Size(92, 63)
        Me.lblStat2Icon.TabIndex = 5
        Me.lblStat2Icon.Text = "⚖"
        '
        'lblStat2Value
        '
        Me.lblStat2Value.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStat2Value.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblStat2Value.ForeColor = System.Drawing.Color.White
        Me.lblStat2Value.Location = New System.Drawing.Point(0, 67)
        Me.lblStat2Value.Name = "lblStat2Value"
        Me.lblStat2Value.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.lblStat2Value.Size = New System.Drawing.Size(234, 71)
        Me.lblStat2Value.TabIndex = 2
        Me.lblStat2Value.Text = "0 KG"
        Me.lblStat2Value.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lblStat2Title
        '
        Me.lblStat2Title.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.lblStat2Title.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStat2Title.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStat2Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblStat2Title.Location = New System.Drawing.Point(0, 138)
        Me.lblStat2Title.Name = "lblStat2Title"
        Me.lblStat2Title.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.lblStat2Title.Size = New System.Drawing.Size(234, 36)
        Me.lblStat2Title.TabIndex = 0
        Me.lblStat2Title.Text = "TOTAL NETTO HARI INI"
        Me.lblStat2Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelStat1
        '
        Me.PanelStat1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.PanelStat1.Controls.Add(Me.lblStat1Icon)
        Me.PanelStat1.Controls.Add(Me.lblStat1Value)
        Me.PanelStat1.Controls.Add(Me.lblStat1Title)
        Me.PanelStat1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelStat1.Location = New System.Drawing.Point(20, 20)
        Me.PanelStat1.Margin = New System.Windows.Forms.Padding(20)
        Me.PanelStat1.Name = "PanelStat1"
        Me.PanelStat1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
        Me.PanelStat1.Size = New System.Drawing.Size(234, 180)
        Me.PanelStat1.TabIndex = 0
        '
        'lblStat1Icon
        '
        Me.lblStat1Icon.AutoSize = True
        Me.lblStat1Icon.Font = New System.Drawing.Font("Segoe UI Emoji", 28.0!)
        Me.lblStat1Icon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblStat1Icon.Location = New System.Drawing.Point(15, 15)
        Me.lblStat1Icon.Name = "lblStat1Icon"
        Me.lblStat1Icon.Size = New System.Drawing.Size(92, 63)
        Me.lblStat1Icon.TabIndex = 4
        Me.lblStat1Icon.Text = "📦"
        '
        'lblStat1Value
        '
        Me.lblStat1Value.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStat1Value.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblStat1Value.ForeColor = System.Drawing.Color.White
        Me.lblStat1Value.Location = New System.Drawing.Point(0, 67)
        Me.lblStat1Value.Name = "lblStat1Value"
        Me.lblStat1Value.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.lblStat1Value.Size = New System.Drawing.Size(234, 71)
        Me.lblStat1Value.TabIndex = 1
        Me.lblStat1Value.Text = "0"
        Me.lblStat1Value.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lblStat1Title
        '
        Me.lblStat1Title.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.lblStat1Title.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStat1Title.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStat1Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblStat1Title.Location = New System.Drawing.Point(0, 138)
        Me.lblStat1Title.Name = "lblStat1Title"
        Me.lblStat1Title.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.lblStat1Title.Size = New System.Drawing.Size(234, 36)
        Me.lblStat1Title.TabIndex = 0
        Me.lblStat1Title.Text = "TOTAL TRANSAKSI HARI INI"
        Me.lblStat1Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelTitle
        '
        Me.PanelTitle.Controls.Add(Me.lblSubtitle)
        Me.PanelTitle.Controls.Add(Me.lblWelcome)
        Me.PanelTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTitle.Location = New System.Drawing.Point(50, 50)
        Me.PanelTitle.Name = "PanelTitle"
        Me.PanelTitle.Size = New System.Drawing.Size(822, 186)
        Me.PanelTitle.TabIndex = 3
        '
        'lblSubtitle
        '
        Me.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(0, 110)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(822, 35)
        Me.lblSubtitle.TabIndex = 2
        Me.lblSubtitle.Text = "SISTEM MANAJEMEN TIMBANGAN DIGITAL"
        Me.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWelcome
        '
        Me.lblWelcome.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblWelcome.Font = New System.Drawing.Font("Segoe UI Light", 56.0!)
        Me.lblWelcome.ForeColor = System.Drawing.Color.White
        Me.lblWelcome.Location = New System.Drawing.Point(0, 0)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(822, 110)
        Me.lblWelcome.TabIndex = 0
        Me.lblWelcome.Text = "WEIGHBRIDGE"
        Me.lblWelcome.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'TimerJam
        '
        Me.TimerJam.Enabled = True
        Me.TimerJam.Interval = 1000
        '
        'PanelLogo
        '
        Me.PanelLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.PanelLogo.Controls.Add(Me.lblAppName)
        Me.PanelLogo.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelLogo.Location = New System.Drawing.Point(0, 0)
        Me.PanelLogo.Name = "PanelLogo"
        Me.PanelLogo.Size = New System.Drawing.Size(260, 110)
        Me.PanelLogo.TabIndex = 0
        '
        'lblAppName
        '
        Me.lblAppName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAppName.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblAppName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblAppName.Location = New System.Drawing.Point(0, 0)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Size = New System.Drawing.Size(260, 110)
        Me.lblAppName.TabIndex = 0
        Me.lblAppName.Text = "Ilham Al Amin Saragih"
        Me.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PanelSidebar
        '
        Me.PanelSidebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.PanelSidebar.Controls.Add(Me.btnLogout)
        Me.PanelSidebar.Controls.Add(Me.PanelMenuContainer)
        Me.PanelSidebar.Controls.Add(Me.PanelLogo)
        Me.PanelSidebar.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelSidebar.Location = New System.Drawing.Point(0, 0)
        Me.PanelSidebar.Name = "PanelSidebar"
        Me.PanelSidebar.Size = New System.Drawing.Size(260, 740)
        Me.PanelSidebar.TabIndex = 2
        '
        'PanelMenuContainer
        '
        Me.PanelMenuContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.PanelMenuContainer.Controls.Add(Me.btnAuditLog)
        Me.PanelMenuContainer.Controls.Add(Me.btnMasterData)
        Me.PanelMenuContainer.Controls.Add(Me.btnSettings)
        Me.PanelMenuContainer.Controls.Add(Me.btnManajemenUser)
        Me.PanelMenuContainer.Controls.Add(Me.btnLaporan)
        Me.PanelMenuContainer.Controls.Add(Me.btnDaftarTimbangan)
        Me.PanelMenuContainer.Controls.Add(Me.btnInputTimbangan)
        Me.PanelMenuContainer.Controls.Add(Me.lblMenuTitle)
        Me.PanelMenuContainer.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelMenuContainer.Location = New System.Drawing.Point(0, 110)
        Me.PanelMenuContainer.Name = "PanelMenuContainer"
        Me.PanelMenuContainer.Size = New System.Drawing.Size(260, 506)
        Me.PanelMenuContainer.TabIndex = 3
        '
        'lblMenuTitle
        '
        Me.lblMenuTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMenuTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblMenuTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblMenuTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblMenuTitle.Name = "lblMenuTitle"
        Me.lblMenuTitle.Padding = New System.Windows.Forms.Padding(20, 10, 0, 0)
        Me.lblMenuTitle.Size = New System.Drawing.Size(260, 40)
        Me.lblMenuTitle.TabIndex = 4
        Me.lblMenuTitle.Text = "MENU UTAMA"
        '
        'FormDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1182, 740)
        Me.Controls.Add(Me.PanelContent)
        Me.Controls.Add(Me.PanelHeader)
        Me.Controls.Add(Me.PanelSidebar)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "FormDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sistem Timbangan PKS - Dashboard"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        Me.PanelDateContainer.ResumeLayout(False)
        Me.PanelContent.ResumeLayout(False)
        Me.PanelStats.ResumeLayout(False)
        Me.PanelStat3.ResumeLayout(False)
        Me.PanelStat3.PerformLayout()
        Me.PanelStat2.ResumeLayout(False)
        Me.PanelStat2.PerformLayout()
        Me.PanelStat1.ResumeLayout(False)
        Me.PanelStat1.PerformLayout()
        Me.PanelTitle.ResumeLayout(False)
        Me.PanelLogo.ResumeLayout(False)
        Me.PanelSidebar.ResumeLayout(False)
        Me.PanelMenuContainer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblUser As Label
    Friend WithEvents lblTanggal As Label
    Friend WithEvents btnAuditLog As Button
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnManajemenUser As Button
    Friend WithEvents btnLaporan As Button
    Friend WithEvents btnMasterData As Button
    Friend WithEvents btnDaftarTimbangan As Button
    Friend WithEvents btnInputTimbangan As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents PanelHeader As Panel
    Friend WithEvents lblJam As Label
    Friend WithEvents PanelContent As Panel
    Friend WithEvents lblWelcome As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents TimerJam As Timer
    Friend WithEvents PanelStats As TableLayoutPanel
    Friend WithEvents PanelStat3 As Panel
    Friend WithEvents lblStat3Title As Label
    Friend WithEvents PanelStat2 As Panel
    Friend WithEvents lblStat2Title As Label
    Friend WithEvents PanelStat1 As Panel
    Friend WithEvents lblStat1Title As Label
    Friend WithEvents PanelLogo As Panel
    Friend WithEvents lblAppName As Label
    Friend WithEvents PanelSidebar As Panel
    Friend WithEvents PanelMenuContainer As Panel
    Friend WithEvents lblStat1Value As Label
    Friend WithEvents lblStat3Value As Label
    Friend WithEvents lblStat2Value As Label
    Friend WithEvents lblStat1Icon As Label
    Friend WithEvents lblStat2Icon As Label
    Friend WithEvents lblStat3Icon As Label
    Friend WithEvents lblMenuTitle As Label
    Friend WithEvents PanelDateContainer As Panel
    Friend WithEvents PanelTitle As Panel
End Class