<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormInputTimbangan
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.btnRefreshConnection = New System.Windows.Forms.Button()
        Me.lblComPort = New System.Windows.Forms.Label()
        Me.lblStatusTimbangan = New System.Windows.Forms.Label()
        Me.PanelTombol = New System.Windows.Forms.Panel()
        Me.btnTutup = New System.Windows.Forms.Button()
        Me.btnBatal = New System.Windows.Forms.Button()
        Me.btnCetak = New System.Windows.Forms.Button()
        Me.btnSimpan = New System.Windows.Forms.Button()
        Me.PanelKiri = New System.Windows.Forms.Panel()
        Me.lblNoDO = New System.Windows.Forms.Label()
        Me.chkIncludeKeterangan = New System.Windows.Forms.CheckBox()
        Me.txtKeterangan = New System.Windows.Forms.TextBox()
        Me.lblKeterangan = New System.Windows.Forms.Label()
        Me.txtPotonganKg = New System.Windows.Forms.TextBox()
        Me.lblPotonganKg = New System.Windows.Forms.Label()
        Me.txtPotonganPersen = New System.Windows.Forms.TextBox()
        Me.lblPotonganPersen = New System.Windows.Forms.Label()
        Me.lblSeparator2 = New System.Windows.Forms.Label()
        Me.txtDirt = New System.Windows.Forms.TextBox()
        Me.lblDirt = New System.Windows.Forms.Label()
        Me.txtMoisture = New System.Windows.Forms.TextBox()
        Me.lblMoisture = New System.Windows.Forms.Label()
        Me.txtFFA = New System.Windows.Forms.TextBox()
        Me.lblFFA = New System.Windows.Forms.Label()
        Me.txtSuhuMinyak = New System.Windows.Forms.TextBox()
        Me.lblSuhuMinyak = New System.Windows.Forms.Label()
        Me.chkIncludeFFA = New System.Windows.Forms.CheckBox()
        Me.grpSegel = New System.Windows.Forms.GroupBox()
        Me.txtSegelBawah = New System.Windows.Forms.TextBox()
        Me.lblSegelBawah = New System.Windows.Forms.Label()
        Me.txtSegelAtas = New System.Windows.Forms.TextBox()
        Me.lblSegelAtas = New System.Windows.Forms.Label()
        Me.cmbTransporter = New System.Windows.Forms.ComboBox()
        Me.lblTransporter = New System.Windows.Forms.Label()
        Me.txtNoSIM = New System.Windows.Forms.TextBox()
        Me.lblNoSIM = New System.Windows.Forms.Label()
        Me.txtNamaSupir = New System.Windows.Forms.TextBox()
        Me.lblNamaSupir = New System.Windows.Forms.Label()
        Me.txtNoPolisi = New System.Windows.Forms.TextBox()
        Me.lblNoPolisi = New System.Windows.Forms.Label()
        Me.txtAlamat = New System.Windows.Forms.TextBox()
        Me.lblAlamat = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cmbTransType = New System.Windows.Forms.ComboBox()
        Me.lblTransType = New System.Windows.Forms.Label()
        Me.cmbProduct = New System.Windows.Forms.ComboBox()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.txtNoDO = New System.Windows.Forms.TextBox()
        Me.txtNoKontrak = New System.Windows.Forms.TextBox()
        Me.lblNoKontrak = New System.Windows.Forms.Label()
        Me.txtNoTiket = New System.Windows.Forms.TextBox()
        Me.lblNoTiket = New System.Windows.Forms.Label()
        Me.cmbAntrian = New System.Windows.Forms.ComboBox()
        Me.lblPilihAntrian = New System.Windows.Forms.Label()
        Me.lblHeaderKiri = New System.Windows.Forms.Label()
        Me.PanelKanan = New System.Windows.Forms.Panel()
        Me.PanelBeratBersih = New System.Windows.Forms.Panel()
        Me.lblSatuanBeratBersih = New System.Windows.Forms.Label()
        Me.lblBeratBersih = New System.Windows.Forms.Label()
        Me.lblTitleBeratBersih = New System.Windows.Forms.Label()
        Me.lblSeparator5 = New System.Windows.Forms.Label()
        Me.lblTotalPotongan = New System.Windows.Forms.Label()
        Me.lblTitlePotongan = New System.Windows.Forms.Label()
        Me.lblBeratNetto = New System.Windows.Forms.Label()
        Me.lblTitleNetto = New System.Windows.Forms.Label()
        Me.lblSeparator4 = New System.Windows.Forms.Label()
        Me.lblBeratKeluar = New System.Windows.Forms.Label()
        Me.lblTitleBeratKeluar = New System.Windows.Forms.Label()
        Me.lblWaktuKeluar = New System.Windows.Forms.Label()
        Me.lblTitleWaktuKeluar = New System.Windows.Forms.Label()
        Me.lblBeratMasuk = New System.Windows.Forms.Label()
        Me.lblTitleBeratMasuk = New System.Windows.Forms.Label()
        Me.lblWaktuMasuk = New System.Windows.Forms.Label()
        Me.lblTitleWaktuMasuk = New System.Windows.Forms.Label()
        Me.lblSeparator3 = New System.Windows.Forms.Label()
        Me.btnAmbilBerat = New System.Windows.Forms.Button()
        Me.PanelDisplayBerat = New System.Windows.Forms.Panel()
        Me.lblBeratRealtime = New System.Windows.Forms.Label()
        Me.lblHeaderKanan = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TimerRealtime = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PanelHeader.SuspendLayout()
        Me.PanelTombol.SuspendLayout()
        Me.PanelKiri.SuspendLayout()
        Me.grpSegel.SuspendLayout()
        Me.PanelKanan.SuspendLayout()
        Me.PanelBeratBersih.SuspendLayout()
        Me.PanelDisplayBerat.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.PanelHeader.Controls.Add(Me.btnRefreshConnection)
        Me.PanelHeader.Controls.Add(Me.lblComPort)
        Me.PanelHeader.Controls.Add(Me.lblStatusTimbangan)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(1350, 60)
        Me.PanelHeader.TabIndex = 0
        '
        'btnRefreshConnection
        '
        Me.btnRefreshConnection.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.btnRefreshConnection.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshConnection.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.btnRefreshConnection.FlatAppearance.BorderSize = 0
        Me.btnRefreshConnection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnRefreshConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshConnection.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshConnection.ForeColor = System.Drawing.Color.White
        Me.btnRefreshConnection.Location = New System.Drawing.Point(520, 12)
        Me.btnRefreshConnection.Name = "btnRefreshConnection"
        Me.btnRefreshConnection.Size = New System.Drawing.Size(140, 36)
        Me.btnRefreshConnection.TabIndex = 2
        Me.btnRefreshConnection.Text = "⟳  Refresh"
        Me.btnRefreshConnection.UseVisualStyleBackColor = False
        '
        'lblComPort
        '
        Me.lblComPort.AutoSize = True
        Me.lblComPort.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComPort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblComPort.Location = New System.Drawing.Point(370, 18)
        Me.lblComPort.Name = "lblComPort"
        Me.lblComPort.Size = New System.Drawing.Size(104, 23)
        Me.lblComPort.TabIndex = 1
        Me.lblComPort.Text = "Port : COM3"
        '
        'lblStatusTimbangan
        '
        Me.lblStatusTimbangan.AutoSize = True
        Me.lblStatusTimbangan.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusTimbangan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(107, Byte), Integer))
        Me.lblStatusTimbangan.Location = New System.Drawing.Point(20, 18)
        Me.lblStatusTimbangan.Name = "lblStatusTimbangan"
        Me.lblStatusTimbangan.Size = New System.Drawing.Size(235, 23)
        Me.lblStatusTimbangan.TabIndex = 0
        Me.lblStatusTimbangan.Text = "⚖ Status : Belum Terhubung"
        '
        'PanelTombol
        '
        Me.PanelTombol.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.PanelTombol.Controls.Add(Me.btnTutup)
        Me.PanelTombol.Controls.Add(Me.btnBatal)
        Me.PanelTombol.Controls.Add(Me.btnCetak)
        Me.PanelTombol.Controls.Add(Me.btnSimpan)
        Me.PanelTombol.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelTombol.Location = New System.Drawing.Point(1210, 60)
        Me.PanelTombol.Name = "PanelTombol"
        Me.PanelTombol.Size = New System.Drawing.Size(140, 890)
        Me.PanelTombol.TabIndex = 1
        '
        '
        '
        'btnTutup
        '
        Me.btnTutup.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnTutup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTutup.FlatAppearance.BorderSize = 0
        Me.btnTutup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.btnTutup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTutup.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnTutup.ForeColor = System.Drawing.Color.White
        Me.btnTutup.Location = New System.Drawing.Point(12, 285)
        Me.btnTutup.Name = "btnTutup"
        Me.btnTutup.Size = New System.Drawing.Size(116, 55)
        Me.btnTutup.TabIndex = 3
        Me.btnTutup.Text = "✕  TUTUP"
        Me.btnTutup.UseVisualStyleBackColor = False
        '
        'btnBatal
        '
        Me.btnBatal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnBatal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBatal.FlatAppearance.BorderSize = 0
        Me.btnBatal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBatal.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnBatal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.btnBatal.Location = New System.Drawing.Point(12, 210)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(116, 55)
        Me.btnBatal.TabIndex = 2
        Me.btnBatal.Text = "✖  BATAL"
        Me.btnBatal.UseVisualStyleBackColor = False
        '
        'btnCetak
        '
        Me.btnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCetak.FlatAppearance.BorderSize = 0
        Me.btnCetak.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.btnCetak.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCetak.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCetak.ForeColor = System.Drawing.Color.White
        Me.btnCetak.Location = New System.Drawing.Point(12, 135)
        Me.btnCetak.Name = "btnCetak"
        Me.btnCetak.Size = New System.Drawing.Size(116, 55)
        Me.btnCetak.TabIndex = 1
        Me.btnCetak.Text = "🖨  CETAK"
        Me.btnCetak.UseVisualStyleBackColor = False
        '
        'btnSimpan
        '
        Me.btnSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnSimpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSimpan.FlatAppearance.BorderSize = 0
        Me.btnSimpan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSimpan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSimpan.ForeColor = System.Drawing.Color.White
        Me.btnSimpan.Location = New System.Drawing.Point(12, 60)
        Me.btnSimpan.Name = "btnSimpan"
        Me.btnSimpan.Size = New System.Drawing.Size(116, 55)
        Me.btnSimpan.TabIndex = 0
        Me.btnSimpan.Text = "💾  SIMPAN"
        Me.btnSimpan.UseVisualStyleBackColor = False
        '
        'PanelKiri
        '
        Me.PanelKiri.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PanelKiri.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PanelKiri.Controls.Add(Me.lblNoDO)
        Me.PanelKiri.Controls.Add(Me.chkIncludeKeterangan)
        Me.PanelKiri.Controls.Add(Me.txtKeterangan)
        Me.PanelKiri.Controls.Add(Me.lblKeterangan)
        Me.PanelKiri.Controls.Add(Me.txtPotonganKg)
        Me.PanelKiri.Controls.Add(Me.lblPotonganKg)
        Me.PanelKiri.Controls.Add(Me.txtPotonganPersen)
        Me.PanelKiri.Controls.Add(Me.lblPotonganPersen)
        Me.PanelKiri.Controls.Add(Me.lblSeparator2)
        Me.PanelKiri.Controls.Add(Me.txtDirt)
        Me.PanelKiri.Controls.Add(Me.lblDirt)
        Me.PanelKiri.Controls.Add(Me.txtMoisture)
        Me.PanelKiri.Controls.Add(Me.lblMoisture)
        Me.PanelKiri.Controls.Add(Me.txtFFA)
        Me.PanelKiri.Controls.Add(Me.lblFFA)
        Me.PanelKiri.Controls.Add(Me.txtSuhuMinyak)
        Me.PanelKiri.Controls.Add(Me.lblSuhuMinyak)
        Me.PanelKiri.Controls.Add(Me.chkIncludeFFA)
        Me.PanelKiri.Controls.Add(Me.grpSegel)
        Me.PanelKiri.Controls.Add(Me.cmbTransporter)
        Me.PanelKiri.Controls.Add(Me.lblTransporter)
        Me.PanelKiri.Controls.Add(Me.txtNoSIM)
        Me.PanelKiri.Controls.Add(Me.lblNoSIM)
        Me.PanelKiri.Controls.Add(Me.txtNamaSupir)
        Me.PanelKiri.Controls.Add(Me.lblNamaSupir)
        Me.PanelKiri.Controls.Add(Me.txtNoPolisi)
        Me.PanelKiri.Controls.Add(Me.lblNoPolisi)
        Me.PanelKiri.Controls.Add(Me.txtAlamat)
        Me.PanelKiri.Controls.Add(Me.lblAlamat)
        Me.PanelKiri.Controls.Add(Me.cmbCustomer)
        Me.PanelKiri.Controls.Add(Me.lblCustomer)
        Me.PanelKiri.Controls.Add(Me.cmbTransType)
        Me.PanelKiri.Controls.Add(Me.lblTransType)
        Me.PanelKiri.Controls.Add(Me.cmbProduct)
        Me.PanelKiri.Controls.Add(Me.lblProduct)
        Me.PanelKiri.Controls.Add(Me.txtNoDO)
        Me.PanelKiri.Controls.Add(Me.txtNoKontrak)
        Me.PanelKiri.Controls.Add(Me.lblNoKontrak)
        Me.PanelKiri.Controls.Add(Me.txtNoTiket)
        Me.PanelKiri.Controls.Add(Me.lblNoTiket)
        Me.PanelKiri.Controls.Add(Me.cmbAntrian)
        Me.PanelKiri.Controls.Add(Me.lblPilihAntrian)
        Me.PanelKiri.Controls.Add(Me.lblHeaderKiri)
        Me.PanelKiri.Location = New System.Drawing.Point(12, 72)
        Me.PanelKiri.Name = "PanelKiri"
        Me.PanelKiri.Padding = New System.Windows.Forms.Padding(8)
        Me.PanelKiri.Size = New System.Drawing.Size(620, 865)
        Me.PanelKiri.TabIndex = 2
        '
        'lblNoDO
        '
        Me.lblNoDO.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblNoDO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblNoDO.Location = New System.Drawing.Point(12, 161)
        Me.lblNoDO.Name = "lblNoDO"
        Me.lblNoDO.Size = New System.Drawing.Size(130, 25)
        Me.lblNoDO.TabIndex = 39
        Me.lblNoDO.Text = "No. DO :"
        Me.lblNoDO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkIncludeKeterangan
        '
        Me.chkIncludeKeterangan.AutoSize = True
        Me.chkIncludeKeterangan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkIncludeKeterangan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.chkIncludeKeterangan.Location = New System.Drawing.Point(15, 675)
        Me.chkIncludeKeterangan.Name = "chkIncludeKeterangan"
        Me.chkIncludeKeterangan.Size = New System.Drawing.Size(164, 24)
        Me.chkIncludeKeterangan.TabIndex = 38
        Me.chkIncludeKeterangan.Text = "Include Keterangan"
        Me.chkIncludeKeterangan.UseVisualStyleBackColor = True
        '
        'txtKeterangan
        '
        Me.txtKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKeterangan.Enabled = False
        Me.txtKeterangan.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtKeterangan.Location = New System.Drawing.Point(148, 707)
        Me.txtKeterangan.Name = "txtKeterangan"
        Me.txtKeterangan.Size = New System.Drawing.Size(455, 30)
        Me.txtKeterangan.TabIndex = 37
        '
        'lblKeterangan
        '
        Me.lblKeterangan.AutoSize = True
        Me.lblKeterangan.Enabled = False
        Me.lblKeterangan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblKeterangan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblKeterangan.Location = New System.Drawing.Point(40, 710)
        Me.lblKeterangan.Name = "lblKeterangan"
        Me.lblKeterangan.Size = New System.Drawing.Size(92, 20)
        Me.lblKeterangan.TabIndex = 36
        Me.lblKeterangan.Text = "Keterangan :"
        '
        'txtPotonganKg
        '
        Me.txtPotonganKg.BackColor = System.Drawing.Color.White
        Me.txtPotonganKg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPotonganKg.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPotonganKg.Location = New System.Drawing.Point(420, 635)
        Me.txtPotonganKg.Name = "txtPotonganKg"
        Me.txtPotonganKg.Size = New System.Drawing.Size(100, 30)
        Me.txtPotonganKg.TabIndex = 35
        Me.txtPotonganKg.Text = "0"
        Me.txtPotonganKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPotonganKg
        '
        Me.lblPotonganKg.AutoSize = True
        Me.lblPotonganKg.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPotonganKg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblPotonganKg.Location = New System.Drawing.Point(300, 638)
        Me.lblPotonganKg.Name = "lblPotonganKg"
        Me.lblPotonganKg.Size = New System.Drawing.Size(111, 20)
        Me.lblPotonganKg.TabIndex = 34
        Me.lblPotonganKg.Text = "Potongan (Kg) :"
        '
        'txtPotonganPersen
        '
        Me.txtPotonganPersen.BackColor = System.Drawing.Color.White
        Me.txtPotonganPersen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPotonganPersen.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPotonganPersen.Location = New System.Drawing.Point(148, 635)
        Me.txtPotonganPersen.Name = "txtPotonganPersen"
        Me.txtPotonganPersen.Size = New System.Drawing.Size(100, 30)
        Me.txtPotonganPersen.TabIndex = 33
        Me.txtPotonganPersen.Text = "0"
        Me.txtPotonganPersen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPotonganPersen
        '
        Me.lblPotonganPersen.AutoSize = True
        Me.lblPotonganPersen.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPotonganPersen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblPotonganPersen.Location = New System.Drawing.Point(28, 638)
        Me.lblPotonganPersen.Name = "lblPotonganPersen"
        Me.lblPotonganPersen.Size = New System.Drawing.Size(105, 20)
        Me.lblPotonganPersen.TabIndex = 32
        Me.lblPotonganPersen.Text = "Potongan (%) :"
        '
        'lblSeparator2
        '
        Me.lblSeparator2.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.lblSeparator2.Location = New System.Drawing.Point(15, 623)
        Me.lblSeparator2.Name = "lblSeparator2"
        Me.lblSeparator2.Size = New System.Drawing.Size(588, 1)
        Me.lblSeparator2.TabIndex = 31
        '
        'txtDirt
        '
        Me.txtDirt.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtDirt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDirt.Enabled = False
        Me.txtDirt.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDirt.Location = New System.Drawing.Point(240, 578)
        Me.txtDirt.Name = "txtDirt"
        Me.txtDirt.Size = New System.Drawing.Size(145, 30)
        Me.txtDirt.TabIndex = 30
        Me.txtDirt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDirt
        '
        Me.lblDirt.AutoSize = True
        Me.lblDirt.Enabled = False
        Me.lblDirt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblDirt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblDirt.Location = New System.Drawing.Point(262, 555)
        Me.lblDirt.Name = "lblDirt"
        Me.lblDirt.Size = New System.Drawing.Size(101, 20)
        Me.lblDirt.TabIndex = 29
        Me.lblDirt.Text = "Dirt (Kotoran)"
        '
        'txtMoisture
        '
        Me.txtMoisture.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtMoisture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMoisture.Enabled = False
        Me.txtMoisture.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtMoisture.Location = New System.Drawing.Point(425, 578)
        Me.txtMoisture.Name = "txtMoisture"
        Me.txtMoisture.Size = New System.Drawing.Size(141, 30)
        Me.txtMoisture.TabIndex = 28
        Me.txtMoisture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblMoisture
        '
        Me.lblMoisture.AutoSize = True
        Me.lblMoisture.Enabled = False
        Me.lblMoisture.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblMoisture.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblMoisture.Location = New System.Drawing.Point(449, 555)
        Me.lblMoisture.Name = "lblMoisture"
        Me.lblMoisture.Size = New System.Drawing.Size(100, 20)
        Me.lblMoisture.TabIndex = 27
        Me.lblMoisture.Text = "Moisture (Air)"
        '
        'txtFFA
        '
        Me.txtFFA.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtFFA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFFA.Enabled = False
        Me.txtFFA.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtFFA.Location = New System.Drawing.Point(61, 578)
        Me.txtFFA.Name = "txtFFA"
        Me.txtFFA.Size = New System.Drawing.Size(144, 30)
        Me.txtFFA.TabIndex = 26
        Me.txtFFA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblFFA
        '
        Me.lblFFA.AutoSize = True
        Me.lblFFA.Enabled = False
        Me.lblFFA.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblFFA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblFFA.Location = New System.Drawing.Point(95, 555)
        Me.lblFFA.Name = "lblFFA"
        Me.lblFFA.Size = New System.Drawing.Size(83, 20)
        Me.lblFFA.TabIndex = 25
        Me.lblFFA.Text = "FFA (Asam)"
        '
        'txtSuhuMinyak
        '
        Me.txtSuhuMinyak.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtSuhuMinyak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuhuMinyak.Enabled = False
        Me.txtSuhuMinyak.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtSuhuMinyak.Location = New System.Drawing.Point(468, 513)
        Me.txtSuhuMinyak.Name = "txtSuhuMinyak"
        Me.txtSuhuMinyak.Size = New System.Drawing.Size(98, 30)
        Me.txtSuhuMinyak.TabIndex = 24
        Me.txtSuhuMinyak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSuhuMinyak
        '
        Me.lblSuhuMinyak.AutoSize = True
        Me.lblSuhuMinyak.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblSuhuMinyak.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblSuhuMinyak.Location = New System.Drawing.Point(358, 517)
        Me.lblSuhuMinyak.Name = "lblSuhuMinyak"
        Me.lblSuhuMinyak.Size = New System.Drawing.Size(107, 20)
        Me.lblSuhuMinyak.TabIndex = 23
        Me.lblSuhuMinyak.Text = "Suhu Minyak :"
        '
        'chkIncludeFFA
        '
        Me.chkIncludeFFA.AutoSize = True
        Me.chkIncludeFFA.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkIncludeFFA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.chkIncludeFFA.Location = New System.Drawing.Point(15, 516)
        Me.chkIncludeFFA.Name = "chkIncludeFFA"
        Me.chkIncludeFFA.Size = New System.Drawing.Size(304, 24)
        Me.chkIncludeFFA.TabIndex = 22
        Me.chkIncludeFFA.Text = "Include Analisa Lab (FFA, Moisture, Dirt)"
        Me.chkIncludeFFA.UseVisualStyleBackColor = True
        '
        'grpSegel
        '
        Me.grpSegel.Controls.Add(Me.txtSegelBawah)
        Me.grpSegel.Controls.Add(Me.lblSegelBawah)
        Me.grpSegel.Controls.Add(Me.txtSegelAtas)
        Me.grpSegel.Controls.Add(Me.lblSegelAtas)
        Me.grpSegel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpSegel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.grpSegel.Location = New System.Drawing.Point(15, 443)
        Me.grpSegel.Name = "grpSegel"
        Me.grpSegel.Size = New System.Drawing.Size(551, 65)
        Me.grpSegel.TabIndex = 21
        Me.grpSegel.TabStop = False
        Me.grpSegel.Text = "Nomor Segel (Seal Security)"
        '
        'txtSegelBawah
        '
        Me.txtSegelBawah.BackColor = System.Drawing.Color.White
        Me.txtSegelBawah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSegelBawah.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSegelBawah.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtSegelBawah.Location = New System.Drawing.Point(350, 28)
        Me.txtSegelBawah.Name = "txtSegelBawah"
        Me.txtSegelBawah.Size = New System.Drawing.Size(180, 29)
        Me.txtSegelBawah.TabIndex = 3
        '
        'lblSegelBawah
        '
        Me.lblSegelBawah.AutoSize = True
        Me.lblSegelBawah.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSegelBawah.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblSegelBawah.Location = New System.Drawing.Point(283, 31)
        Me.lblSegelBawah.Name = "lblSegelBawah"
        Me.lblSegelBawah.Size = New System.Drawing.Size(60, 20)
        Me.lblSegelBawah.TabIndex = 2
        Me.lblSegelBawah.Text = "Bawah :"
        '
        'txtSegelAtas
        '
        Me.txtSegelAtas.BackColor = System.Drawing.Color.White
        Me.txtSegelAtas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSegelAtas.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSegelAtas.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtSegelAtas.Location = New System.Drawing.Point(70, 28)
        Me.txtSegelAtas.Name = "txtSegelAtas"
        Me.txtSegelAtas.Size = New System.Drawing.Size(180, 29)
        Me.txtSegelAtas.TabIndex = 1
        '
        'lblSegelAtas
        '
        Me.lblSegelAtas.AutoSize = True
        Me.lblSegelAtas.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSegelAtas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblSegelAtas.Location = New System.Drawing.Point(16, 31)
        Me.lblSegelAtas.Name = "lblSegelAtas"
        Me.lblSegelAtas.Size = New System.Drawing.Size(45, 20)
        Me.lblSegelAtas.TabIndex = 0
        Me.lblSegelAtas.Text = "Atas :"
        '
        'cmbTransporter
        '
        Me.cmbTransporter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbTransporter.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbTransporter.FormattingEnabled = True
        Me.cmbTransporter.Location = New System.Drawing.Point(148, 406)
        Me.cmbTransporter.Name = "cmbTransporter"
        Me.cmbTransporter.Size = New System.Drawing.Size(455, 31)
        Me.cmbTransporter.TabIndex = 20
        '
        'lblTransporter
        '
        Me.lblTransporter.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTransporter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblTransporter.Location = New System.Drawing.Point(12, 406)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(130, 25)
        Me.lblTransporter.TabIndex = 19
        Me.lblTransporter.Text = "Transporter :"
        Me.lblTransporter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNoSIM
        '
        Me.txtNoSIM.BackColor = System.Drawing.Color.White
        Me.txtNoSIM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoSIM.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNoSIM.Location = New System.Drawing.Point(148, 371)
        Me.txtNoSIM.Name = "txtNoSIM"
        Me.txtNoSIM.Size = New System.Drawing.Size(455, 30)
        Me.txtNoSIM.TabIndex = 18
        '
        'lblNoSIM
        '
        Me.lblNoSIM.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblNoSIM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblNoSIM.Location = New System.Drawing.Point(12, 371)
        Me.lblNoSIM.Name = "lblNoSIM"
        Me.lblNoSIM.Size = New System.Drawing.Size(130, 25)
        Me.lblNoSIM.TabIndex = 17
        Me.lblNoSIM.Text = "No. SIM :"
        Me.lblNoSIM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNamaSupir
        '
        Me.txtNamaSupir.BackColor = System.Drawing.Color.White
        Me.txtNamaSupir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNamaSupir.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNamaSupir.Location = New System.Drawing.Point(148, 336)
        Me.txtNamaSupir.Name = "txtNamaSupir"
        Me.txtNamaSupir.Size = New System.Drawing.Size(455, 30)
        Me.txtNamaSupir.TabIndex = 16
        '
        'lblNamaSupir
        '
        Me.lblNamaSupir.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblNamaSupir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblNamaSupir.Location = New System.Drawing.Point(12, 336)
        Me.lblNamaSupir.Name = "lblNamaSupir"
        Me.lblNamaSupir.Size = New System.Drawing.Size(130, 25)
        Me.lblNamaSupir.TabIndex = 15
        Me.lblNamaSupir.Text = "Supir :"
        Me.lblNamaSupir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNoPolisi
        '
        Me.txtNoPolisi.BackColor = System.Drawing.Color.White
        Me.txtNoPolisi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoPolisi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNoPolisi.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNoPolisi.Location = New System.Drawing.Point(148, 301)
        Me.txtNoPolisi.Name = "txtNoPolisi"
        Me.txtNoPolisi.Size = New System.Drawing.Size(455, 30)
        Me.txtNoPolisi.TabIndex = 14
        '
        'lblNoPolisi
        '
        Me.lblNoPolisi.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblNoPolisi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblNoPolisi.Location = New System.Drawing.Point(12, 301)
        Me.lblNoPolisi.Name = "lblNoPolisi"
        Me.lblNoPolisi.Size = New System.Drawing.Size(130, 25)
        Me.lblNoPolisi.TabIndex = 13
        Me.lblNoPolisi.Text = "No. Polisi :"
        Me.lblNoPolisi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAlamat
        '
        Me.txtAlamat.BackColor = System.Drawing.Color.White
        Me.txtAlamat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlamat.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtAlamat.Location = New System.Drawing.Point(148, 266)
        Me.txtAlamat.Name = "txtAlamat"
        Me.txtAlamat.Size = New System.Drawing.Size(455, 30)
        Me.txtAlamat.TabIndex = 12
        '
        'lblAlamat
        '
        Me.lblAlamat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAlamat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblAlamat.Location = New System.Drawing.Point(12, 266)
        Me.lblAlamat.Name = "lblAlamat"
        Me.lblAlamat.Size = New System.Drawing.Size(130, 25)
        Me.lblAlamat.TabIndex = 11
        Me.lblAlamat.Text = "Alamat :"
        Me.lblAlamat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCustomer
        '
        Me.cmbCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCustomer.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbCustomer.FormattingEnabled = True
        Me.cmbCustomer.Location = New System.Drawing.Point(148, 231)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(455, 31)
        Me.cmbCustomer.TabIndex = 10
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblCustomer.Location = New System.Drawing.Point(12, 231)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(130, 25)
        Me.lblCustomer.TabIndex = 9
        Me.lblCustomer.Text = "Customer :"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbTransType
        '
        Me.cmbTransType.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTransType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbTransType.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbTransType.FormattingEnabled = True
        Me.cmbTransType.Items.AddRange(New Object() {"JUAL", "BELI"})
        Me.cmbTransType.Location = New System.Drawing.Point(505, 196)
        Me.cmbTransType.Name = "cmbTransType"
        Me.cmbTransType.Size = New System.Drawing.Size(98, 31)
        Me.cmbTransType.TabIndex = 6
        '
        'lblTransType
        '
        Me.lblTransType.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTransType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblTransType.Location = New System.Drawing.Point(445, 196)
        Me.lblTransType.Name = "lblTransType"
        Me.lblTransType.Size = New System.Drawing.Size(54, 25)
        Me.lblTransType.TabIndex = 0
        Me.lblTransType.Text = "Type :"
        Me.lblTransType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbProduct
        '
        Me.cmbProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbProduct.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbProduct.FormattingEnabled = True
        Me.cmbProduct.Location = New System.Drawing.Point(148, 196)
        Me.cmbProduct.Name = "cmbProduct"
        Me.cmbProduct.Size = New System.Drawing.Size(280, 31)
        Me.cmbProduct.TabIndex = 5
        '
        'lblProduct
        '
        Me.lblProduct.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblProduct.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblProduct.Location = New System.Drawing.Point(12, 196)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(130, 25)
        Me.lblProduct.TabIndex = 0
        Me.lblProduct.Text = "Product :"
        Me.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNoDO
        '
        Me.txtNoDO.BackColor = System.Drawing.Color.White
        Me.txtNoDO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoDO.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNoDO.Location = New System.Drawing.Point(148, 161)
        Me.txtNoDO.Name = "txtNoDO"
        Me.txtNoDO.Size = New System.Drawing.Size(455, 30)
        Me.txtNoDO.TabIndex = 4
        '
        'txtNoKontrak
        '
        Me.txtNoKontrak.BackColor = System.Drawing.Color.White
        Me.txtNoKontrak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoKontrak.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNoKontrak.Location = New System.Drawing.Point(148, 126)
        Me.txtNoKontrak.Name = "txtNoKontrak"
        Me.txtNoKontrak.Size = New System.Drawing.Size(455, 30)
        Me.txtNoKontrak.TabIndex = 3
        '
        'lblNoKontrak
        '
        Me.lblNoKontrak.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblNoKontrak.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblNoKontrak.Location = New System.Drawing.Point(12, 126)
        Me.lblNoKontrak.Name = "lblNoKontrak"
        Me.lblNoKontrak.Size = New System.Drawing.Size(130, 25)
        Me.lblNoKontrak.TabIndex = 0
        Me.lblNoKontrak.Text = "No. Kontrak :"
        Me.lblNoKontrak.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNoTiket
        '
        Me.txtNoTiket.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtNoTiket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoTiket.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtNoTiket.Location = New System.Drawing.Point(148, 91)
        Me.txtNoTiket.Name = "txtNoTiket"
        Me.txtNoTiket.ReadOnly = True
        Me.txtNoTiket.Size = New System.Drawing.Size(455, 30)
        Me.txtNoTiket.TabIndex = 2
        '
        'lblNoTiket
        '
        Me.lblNoTiket.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblNoTiket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblNoTiket.Location = New System.Drawing.Point(12, 91)
        Me.lblNoTiket.Name = "lblNoTiket"
        Me.lblNoTiket.Size = New System.Drawing.Size(130, 25)
        Me.lblNoTiket.TabIndex = 0
        Me.lblNoTiket.Text = "No. Tiket :"
        Me.lblNoTiket.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAntrian
        '
        Me.cmbAntrian.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbAntrian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAntrian.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbAntrian.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbAntrian.FormattingEnabled = True
        Me.cmbAntrian.Location = New System.Drawing.Point(148, 55)
        Me.cmbAntrian.Name = "cmbAntrian"
        Me.cmbAntrian.Size = New System.Drawing.Size(455, 31)
        Me.cmbAntrian.TabIndex = 1
        '
        'lblPilihAntrian
        '
        Me.lblPilihAntrian.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPilihAntrian.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblPilihAntrian.Location = New System.Drawing.Point(12, 55)
        Me.lblPilihAntrian.Name = "lblPilihAntrian"
        Me.lblPilihAntrian.Size = New System.Drawing.Size(130, 25)
        Me.lblPilihAntrian.TabIndex = 0
        Me.lblPilihAntrian.Text = "Antrian (Keluar) :"
        Me.lblPilihAntrian.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHeaderKiri
        '
        Me.lblHeaderKiri.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.lblHeaderKiri.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeaderKiri.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeaderKiri.ForeColor = System.Drawing.Color.White
        Me.lblHeaderKiri.Location = New System.Drawing.Point(8, 8)
        Me.lblHeaderKiri.Name = "lblHeaderKiri"
        Me.lblHeaderKiri.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.lblHeaderKiri.Size = New System.Drawing.Size(604, 40)
        Me.lblHeaderKiri.TabIndex = 0
        Me.lblHeaderKiri.Text = "📋  DATA KENDARAAN"
        Me.lblHeaderKiri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelKanan
        '
        Me.PanelKanan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PanelKanan.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.PanelKanan.Controls.Add(Me.PanelBeratBersih)
        Me.PanelKanan.Controls.Add(Me.lblSeparator5)
        Me.PanelKanan.Controls.Add(Me.lblTotalPotongan)
        Me.PanelKanan.Controls.Add(Me.lblTitlePotongan)
        Me.PanelKanan.Controls.Add(Me.lblBeratNetto)
        Me.PanelKanan.Controls.Add(Me.lblTitleNetto)
        Me.PanelKanan.Controls.Add(Me.lblSeparator4)
        Me.PanelKanan.Controls.Add(Me.lblBeratKeluar)
        Me.PanelKanan.Controls.Add(Me.lblTitleBeratKeluar)
        Me.PanelKanan.Controls.Add(Me.lblWaktuKeluar)
        Me.PanelKanan.Controls.Add(Me.lblTitleWaktuKeluar)
        Me.PanelKanan.Controls.Add(Me.lblBeratMasuk)
        Me.PanelKanan.Controls.Add(Me.lblTitleBeratMasuk)
        Me.PanelKanan.Controls.Add(Me.lblWaktuMasuk)
        Me.PanelKanan.Controls.Add(Me.lblTitleWaktuMasuk)
        Me.PanelKanan.Controls.Add(Me.lblSeparator3)
        Me.PanelKanan.Controls.Add(Me.btnAmbilBerat)
        Me.PanelKanan.Controls.Add(Me.PanelDisplayBerat)
        Me.PanelKanan.Controls.Add(Me.lblHeaderKanan)
        Me.PanelKanan.Location = New System.Drawing.Point(642, 72)
        Me.PanelKanan.Name = "PanelKanan"
        Me.PanelKanan.Padding = New System.Windows.Forms.Padding(8)
        Me.PanelKanan.Size = New System.Drawing.Size(558, 865)
        Me.PanelKanan.TabIndex = 3
        '
        'PanelBeratBersih
        '
        Me.PanelBeratBersih.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelBeratBersih.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.PanelBeratBersih.Controls.Add(Me.lblSatuanBeratBersih)
        Me.PanelBeratBersih.Controls.Add(Me.lblBeratBersih)
        Me.PanelBeratBersih.Controls.Add(Me.lblTitleBeratBersih)
        Me.PanelBeratBersih.Location = New System.Drawing.Point(25, 545)
        Me.PanelBeratBersih.Name = "PanelBeratBersih"
        Me.PanelBeratBersih.Size = New System.Drawing.Size(508, 130)
        Me.PanelBeratBersih.TabIndex = 17
        '
        'lblSatuanBeratBersih
        '
        Me.lblSatuanBeratBersih.AutoSize = True
        Me.lblSatuanBeratBersih.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblSatuanBeratBersih.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblSatuanBeratBersih.Location = New System.Drawing.Point(430, 72)
        Me.lblSatuanBeratBersih.Name = "lblSatuanBeratBersih"
        Me.lblSatuanBeratBersih.Size = New System.Drawing.Size(53, 37)
        Me.lblSatuanBeratBersih.TabIndex = 2
        Me.lblSatuanBeratBersih.Text = "KG"
        '
        'lblBeratBersih
        '
        Me.lblBeratBersih.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblBeratBersih.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.lblBeratBersih.Location = New System.Drawing.Point(15, 55)
        Me.lblBeratBersih.Name = "lblBeratBersih"
        Me.lblBeratBersih.Size = New System.Drawing.Size(410, 65)
        Me.lblBeratBersih.TabIndex = 1
        Me.lblBeratBersih.Text = "0"
        Me.lblBeratBersih.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitleBeratBersih
        '
        Me.lblTitleBeratBersih.AutoSize = True
        Me.lblTitleBeratBersih.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitleBeratBersih.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblTitleBeratBersih.Location = New System.Drawing.Point(15, 15)
        Me.lblTitleBeratBersih.Name = "lblTitleBeratBersih"
        Me.lblTitleBeratBersih.Size = New System.Drawing.Size(258, 25)
        Me.lblTitleBeratBersih.TabIndex = 0
        Me.lblTitleBeratBersih.Text = "BERAT BERSIH (NETTO PAID)"
        '
        'lblSeparator5
        '
        Me.lblSeparator5.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.lblSeparator5.Location = New System.Drawing.Point(25, 530)
        Me.lblSeparator5.Name = "lblSeparator5"
        Me.lblSeparator5.Size = New System.Drawing.Size(508, 3)
        Me.lblSeparator5.TabIndex = 16
        '
        'lblTotalPotongan
        '
        Me.lblTotalPotongan.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalPotongan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblTotalPotongan.Location = New System.Drawing.Point(325, 486)
        Me.lblTotalPotongan.Name = "lblTotalPotongan"
        Me.lblTotalPotongan.Size = New System.Drawing.Size(200, 30)
        Me.lblTotalPotongan.TabIndex = 15
        Me.lblTotalPotongan.Text = "0 KG"
        Me.lblTotalPotongan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitlePotongan
        '
        Me.lblTitlePotongan.AutoSize = True
        Me.lblTitlePotongan.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitlePotongan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblTitlePotongan.Location = New System.Drawing.Point(25, 488)
        Me.lblTitlePotongan.Name = "lblTitlePotongan"
        Me.lblTitlePotongan.Size = New System.Drawing.Size(175, 25)
        Me.lblTitlePotongan.TabIndex = 14
        Me.lblTitlePotongan.Text = "Total Potongan      :"
        '
        'lblBeratNetto
        '
        Me.lblBeratNetto.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblBeratNetto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.lblBeratNetto.Location = New System.Drawing.Point(325, 448)
        Me.lblBeratNetto.Name = "lblBeratNetto"
        Me.lblBeratNetto.Size = New System.Drawing.Size(200, 30)
        Me.lblBeratNetto.TabIndex = 13
        Me.lblBeratNetto.Text = "0 KG"
        Me.lblBeratNetto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitleNetto
        '
        Me.lblTitleNetto.AutoSize = True
        Me.lblTitleNetto.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleNetto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblTitleNetto.Location = New System.Drawing.Point(25, 450)
        Me.lblTitleNetto.Name = "lblTitleNetto"
        Me.lblTitleNetto.Size = New System.Drawing.Size(177, 25)
        Me.lblTitleNetto.TabIndex = 12
        Me.lblTitleNetto.Text = "Berat Netto            :"
        '
        'lblSeparator4
        '
        Me.lblSeparator4.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.lblSeparator4.Location = New System.Drawing.Point(25, 435)
        Me.lblSeparator4.Name = "lblSeparator4"
        Me.lblSeparator4.Size = New System.Drawing.Size(508, 1)
        Me.lblSeparator4.TabIndex = 11
        '
        'lblBeratKeluar
        '
        Me.lblBeratKeluar.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblBeratKeluar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblBeratKeluar.Location = New System.Drawing.Point(325, 390)
        Me.lblBeratKeluar.Name = "lblBeratKeluar"
        Me.lblBeratKeluar.Size = New System.Drawing.Size(200, 30)
        Me.lblBeratKeluar.TabIndex = 10
        Me.lblBeratKeluar.Text = "0 KG"
        Me.lblBeratKeluar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitleBeratKeluar
        '
        Me.lblTitleBeratKeluar.Font = New System.Drawing.Font("Segoe UI", 10.2!)
        Me.lblTitleBeratKeluar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblTitleBeratKeluar.Location = New System.Drawing.Point(25, 392)
        Me.lblTitleBeratKeluar.Name = "lblTitleBeratKeluar"
        Me.lblTitleBeratKeluar.Size = New System.Drawing.Size(250, 25)
        Me.lblTitleBeratKeluar.TabIndex = 9
        Me.lblTitleBeratKeluar.Text = "Berat Keluar (Tarra)  :"
        '
        'lblWaktuKeluar
        '
        Me.lblWaktuKeluar.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.lblWaktuKeluar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblWaktuKeluar.Location = New System.Drawing.Point(104, 360)
        Me.lblWaktuKeluar.Name = "lblWaktuKeluar"
        Me.lblWaktuKeluar.Size = New System.Drawing.Size(200, 25)
        Me.lblWaktuKeluar.TabIndex = 8
        Me.lblWaktuKeluar.Text = "-"
        '
        'lblTitleWaktuKeluar
        '
        Me.lblTitleWaktuKeluar.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleWaktuKeluar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblTitleWaktuKeluar.Location = New System.Drawing.Point(25, 360)
        Me.lblTitleWaktuKeluar.Name = "lblTitleWaktuKeluar"
        Me.lblTitleWaktuKeluar.Size = New System.Drawing.Size(73, 25)
        Me.lblTitleWaktuKeluar.TabIndex = 7
        Me.lblTitleWaktuKeluar.Text = "OUT :"
        '
        'lblBeratMasuk
        '
        Me.lblBeratMasuk.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblBeratMasuk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblBeratMasuk.Location = New System.Drawing.Point(325, 318)
        Me.lblBeratMasuk.Name = "lblBeratMasuk"
        Me.lblBeratMasuk.Size = New System.Drawing.Size(200, 30)
        Me.lblBeratMasuk.TabIndex = 6
        Me.lblBeratMasuk.Text = "0 KG"
        Me.lblBeratMasuk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitleBeratMasuk
        '
        Me.lblTitleBeratMasuk.Font = New System.Drawing.Font("Segoe UI", 10.2!)
        Me.lblTitleBeratMasuk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblTitleBeratMasuk.Location = New System.Drawing.Point(25, 320)
        Me.lblTitleBeratMasuk.Name = "lblTitleBeratMasuk"
        Me.lblTitleBeratMasuk.Size = New System.Drawing.Size(250, 25)
        Me.lblTitleBeratMasuk.TabIndex = 5
        Me.lblTitleBeratMasuk.Text = "Berat Masuk (Bruto) :"
        '
        'lblWaktuMasuk
        '
        Me.lblWaktuMasuk.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.lblWaktuMasuk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblWaktuMasuk.Location = New System.Drawing.Point(104, 288)
        Me.lblWaktuMasuk.Name = "lblWaktuMasuk"
        Me.lblWaktuMasuk.Size = New System.Drawing.Size(200, 25)
        Me.lblWaktuMasuk.TabIndex = 4
        Me.lblWaktuMasuk.Text = "-"
        '
        'lblTitleWaktuMasuk
        '
        Me.lblTitleWaktuMasuk.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleWaktuMasuk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblTitleWaktuMasuk.Location = New System.Drawing.Point(26, 288)
        Me.lblTitleWaktuMasuk.Name = "lblTitleWaktuMasuk"
        Me.lblTitleWaktuMasuk.Size = New System.Drawing.Size(72, 25)
        Me.lblTitleWaktuMasuk.TabIndex = 3
        Me.lblTitleWaktuMasuk.Text = "IN    :"
        '
        'lblSeparator3
        '
        Me.lblSeparator3.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.lblSeparator3.Location = New System.Drawing.Point(25, 270)
        Me.lblSeparator3.Name = "lblSeparator3"
        Me.lblSeparator3.Size = New System.Drawing.Size(508, 1)
        Me.lblSeparator3.TabIndex = 2
        '
        'btnAmbilBerat
        '
        Me.btnAmbilBerat.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.btnAmbilBerat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAmbilBerat.FlatAppearance.BorderSize = 0
        Me.btnAmbilBerat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAmbilBerat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAmbilBerat.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAmbilBerat.ForeColor = System.Drawing.Color.White
        Me.btnAmbilBerat.Location = New System.Drawing.Point(370, 218)
        Me.btnAmbilBerat.Name = "btnAmbilBerat"
        Me.btnAmbilBerat.Size = New System.Drawing.Size(163, 40)
        Me.btnAmbilBerat.TabIndex = 1
        Me.btnAmbilBerat.Text = "📥  AMBIL BERAT"
        Me.btnAmbilBerat.UseVisualStyleBackColor = False
        '
        'PanelDisplayBerat
        '
        Me.PanelDisplayBerat.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.PanelDisplayBerat.Controls.Add(Me.lblBeratRealtime)
        Me.PanelDisplayBerat.Location = New System.Drawing.Point(25, 55)
        Me.PanelDisplayBerat.Name = "PanelDisplayBerat"
        Me.PanelDisplayBerat.Size = New System.Drawing.Size(508, 150)
        Me.PanelDisplayBerat.TabIndex = 0
        '
        'lblBeratRealtime
        '
        Me.lblBeratRealtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBeratRealtime.Font = New System.Drawing.Font("Consolas", 72.0!, System.Drawing.FontStyle.Bold)
        Me.lblBeratRealtime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.lblBeratRealtime.Location = New System.Drawing.Point(0, 0)
        Me.lblBeratRealtime.Name = "lblBeratRealtime"
        Me.lblBeratRealtime.Size = New System.Drawing.Size(508, 150)
        Me.lblBeratRealtime.TabIndex = 0
        Me.lblBeratRealtime.Text = "0"
        Me.lblBeratRealtime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHeaderKanan
        '
        Me.lblHeaderKanan.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.lblHeaderKanan.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeaderKanan.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeaderKanan.ForeColor = System.Drawing.Color.White
        Me.lblHeaderKanan.Location = New System.Drawing.Point(8, 8)
        Me.lblHeaderKanan.Name = "lblHeaderKanan"
        Me.lblHeaderKanan.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.lblHeaderKanan.Size = New System.Drawing.Size(542, 40)
        Me.lblHeaderKanan.TabIndex = 0
        Me.lblHeaderKanan.Text = "⚖  DISPLAY TIMBANGAN"
        Me.lblHeaderKanan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TimerRealtime
        '
        Me.TimerRealtime.Interval = 1000
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'FormInputTimbangan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1350, 950)
        Me.Controls.Add(Me.PanelTombol)
        Me.Controls.Add(Me.PanelKanan)
        Me.Controls.Add(Me.PanelKiri)
        Me.Controls.Add(Me.PanelHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FormInputTimbangan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "⚖ Input Timbangan - System Administrator"
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        Me.PanelTombol.ResumeLayout(False)
        Me.PanelTombol.PerformLayout()
        Me.PanelKiri.ResumeLayout(False)
        Me.PanelKiri.PerformLayout()
        Me.grpSegel.ResumeLayout(False)
        Me.grpSegel.PerformLayout()
        Me.PanelKanan.ResumeLayout(False)
        Me.PanelKanan.PerformLayout()
        Me.PanelBeratBersih.ResumeLayout(False)
        Me.PanelBeratBersih.PerformLayout()
        Me.PanelDisplayBerat.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelHeader As System.Windows.Forms.Panel
    Friend WithEvents lblStatusTimbangan As System.Windows.Forms.Label
    Friend WithEvents btnRefreshConnection As System.Windows.Forms.Button
    Friend WithEvents lblComPort As System.Windows.Forms.Label
    Friend WithEvents PanelTombol As System.Windows.Forms.Panel
    Friend WithEvents btnSimpan As System.Windows.Forms.Button
    Friend WithEvents btnTutup As System.Windows.Forms.Button
    Friend WithEvents btnBatal As System.Windows.Forms.Button
    Friend WithEvents btnCetak As System.Windows.Forms.Button
    Friend WithEvents PanelKiri As System.Windows.Forms.Panel
    Friend WithEvents lblHeaderKiri As System.Windows.Forms.Label
    Friend WithEvents lblPilihAntrian As System.Windows.Forms.Label
    Friend WithEvents cmbAntrian As System.Windows.Forms.ComboBox
    Friend WithEvents lblNoTiket As System.Windows.Forms.Label
    Friend WithEvents txtNoTiket As System.Windows.Forms.TextBox
    Friend WithEvents lblNoKontrak As System.Windows.Forms.Label
    Friend WithEvents txtNoKontrak As System.Windows.Forms.TextBox
    Friend WithEvents txtNoDO As System.Windows.Forms.TextBox
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents cmbProduct As System.Windows.Forms.ComboBox
    Friend WithEvents lblTransType As System.Windows.Forms.Label
    Friend WithEvents cmbTransType As System.Windows.Forms.ComboBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents lblAlamat As System.Windows.Forms.Label
    Friend WithEvents txtAlamat As System.Windows.Forms.TextBox
    Friend WithEvents lblNoPolisi As System.Windows.Forms.Label
    Friend WithEvents txtNoPolisi As System.Windows.Forms.TextBox
    Friend WithEvents lblNamaSupir As System.Windows.Forms.Label
    Friend WithEvents txtNamaSupir As System.Windows.Forms.TextBox
    Friend WithEvents lblNoSIM As System.Windows.Forms.Label
    Friend WithEvents txtNoSIM As System.Windows.Forms.TextBox
    Friend WithEvents lblTransporter As System.Windows.Forms.Label
    Friend WithEvents cmbTransporter As System.Windows.Forms.ComboBox
    Friend WithEvents grpSegel As System.Windows.Forms.GroupBox
    Friend WithEvents lblSegelAtas As System.Windows.Forms.Label
    Friend WithEvents txtSegelAtas As System.Windows.Forms.TextBox
    Friend WithEvents lblSegelBawah As System.Windows.Forms.Label
    Friend WithEvents txtSegelBawah As System.Windows.Forms.TextBox
    Friend WithEvents chkIncludeFFA As System.Windows.Forms.CheckBox
    Friend WithEvents lblSuhuMinyak As System.Windows.Forms.Label
    Friend WithEvents txtSuhuMinyak As System.Windows.Forms.TextBox
    Friend WithEvents lblFFA As System.Windows.Forms.Label
    Friend WithEvents txtFFA As System.Windows.Forms.TextBox
    Friend WithEvents lblMoisture As System.Windows.Forms.Label
    Friend WithEvents txtMoisture As System.Windows.Forms.TextBox
    Friend WithEvents lblDirt As System.Windows.Forms.Label
    Friend WithEvents txtDirt As System.Windows.Forms.TextBox
    Friend WithEvents lblSeparator2 As System.Windows.Forms.Label
    Friend WithEvents lblPotonganPersen As System.Windows.Forms.Label
    Friend WithEvents txtPotonganPersen As System.Windows.Forms.TextBox
    Friend WithEvents lblPotonganKg As System.Windows.Forms.Label
    Friend WithEvents txtPotonganKg As System.Windows.Forms.TextBox
    Friend WithEvents chkIncludeKeterangan As System.Windows.Forms.CheckBox
    Friend WithEvents lblKeterangan As System.Windows.Forms.Label
    Friend WithEvents txtKeterangan As System.Windows.Forms.TextBox
    Friend WithEvents PanelKanan As System.Windows.Forms.Panel
    Friend WithEvents lblHeaderKanan As System.Windows.Forms.Label
    Friend WithEvents PanelDisplayBerat As System.Windows.Forms.Panel
    Friend WithEvents lblBeratRealtime As System.Windows.Forms.Label
    Friend WithEvents btnAmbilBerat As System.Windows.Forms.Button
    Friend WithEvents lblSeparator3 As System.Windows.Forms.Label
    Friend WithEvents lblTitleWaktuMasuk As System.Windows.Forms.Label
    Friend WithEvents lblWaktuMasuk As System.Windows.Forms.Label
    Friend WithEvents lblTitleBeratMasuk As System.Windows.Forms.Label
    Friend WithEvents lblBeratMasuk As System.Windows.Forms.Label
    Friend WithEvents lblTitleWaktuKeluar As System.Windows.Forms.Label
    Friend WithEvents lblWaktuKeluar As System.Windows.Forms.Label
    Friend WithEvents lblTitleBeratKeluar As System.Windows.Forms.Label
    Friend WithEvents lblBeratKeluar As System.Windows.Forms.Label
    Friend WithEvents lblSeparator4 As System.Windows.Forms.Label
    Friend WithEvents lblTitleNetto As System.Windows.Forms.Label
    Friend WithEvents lblBeratNetto As System.Windows.Forms.Label
    Friend WithEvents lblTitlePotongan As System.Windows.Forms.Label
    Friend WithEvents lblTotalPotongan As System.Windows.Forms.Label
    Friend WithEvents lblSeparator5 As System.Windows.Forms.Label
    Friend WithEvents PanelBeratBersih As System.Windows.Forms.Panel
    Friend WithEvents lblTitleBeratBersih As System.Windows.Forms.Label
    Friend WithEvents lblBeratBersih As System.Windows.Forms.Label
    Friend WithEvents lblSatuanBeratBersih As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TimerRealtime As System.Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents lblNoDO As Label
End Class