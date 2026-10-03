' =============================================
' FormEditTimbangan.Designer.vb
' Designer File untuk Form Edit Timbangan
' Menampilkan SEMUA data yang sudah diinput
' =============================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormEditTimbangan
    Inherits System.Windows.Forms.Form

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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblEditInfo = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.grpSegel = New System.Windows.Forms.GroupBox()
        Me.txtSegelBawah = New System.Windows.Forms.TextBox()
        Me.txtSegelAtas = New System.Windows.Forms.TextBox()
        Me.lblSegelBawah = New System.Windows.Forms.Label()
        Me.lblSegelAtas = New System.Windows.Forms.Label()
        Me.grpInfoTiket = New System.Windows.Forms.GroupBox()
        Me.lblNoTiketLabel = New System.Windows.Forms.Label()
        Me.txtNoTiket = New System.Windows.Forms.TextBox()
        Me.lblStatusLabel = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblTglInputLabel = New System.Windows.Forms.Label()
        Me.txtTglInput = New System.Windows.Forms.TextBox()
        Me.lblOperatorLabel = New System.Windows.Forms.Label()
        Me.txtOperator = New System.Windows.Forms.TextBox()
        Me.grpDataKendaraan = New System.Windows.Forms.GroupBox()
        Me.txtNoKontrak = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNoPolisi = New System.Windows.Forms.Label()
        Me.txtNoPolisi = New System.Windows.Forms.TextBox()
        Me.lblNamaSupir = New System.Windows.Forms.Label()
        Me.txtNamaSupir = New System.Windows.Forms.TextBox()
        Me.lblNoDO = New System.Windows.Forms.Label()
        Me.txtNoDO = New System.Windows.Forms.TextBox()
        Me.lblTransporter = New System.Windows.Forms.Label()
        Me.cmbTransporter = New System.Windows.Forms.ComboBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.cmbProduct = New System.Windows.Forms.ComboBox()
        Me.grpFFA = New System.Windows.Forms.GroupBox()
        Me.chkIncludeFFA = New System.Windows.Forms.CheckBox()
        Me.lblFFA = New System.Windows.Forms.Label()
        Me.txtFFA = New System.Windows.Forms.TextBox()
        Me.lblMoisture = New System.Windows.Forms.Label()
        Me.txtMoisture = New System.Windows.Forms.TextBox()
        Me.lblDirt = New System.Windows.Forms.Label()
        Me.txtDirt = New System.Windows.Forms.TextBox()
        Me.lblSuhuMinyak = New System.Windows.Forms.Label()
        Me.txtSuhuMinyak = New System.Windows.Forms.TextBox()
        Me.grpKeterangan = New System.Windows.Forms.GroupBox()
        Me.lblKeteranganLabel = New System.Windows.Forms.Label()
        Me.txtKeterangan = New System.Windows.Forms.TextBox()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.grpDataBerat = New System.Windows.Forms.GroupBox()
        Me.lblBeratBrutoLabel = New System.Windows.Forms.Label()
        Me.txtBeratBruto = New System.Windows.Forms.TextBox()
        Me.lblKgBruto = New System.Windows.Forms.Label()
        Me.lblWaktuBruto = New System.Windows.Forms.Label()
        Me.lblBeratTaraLabel = New System.Windows.Forms.Label()
        Me.txtBeratTara = New System.Windows.Forms.TextBox()
        Me.lblKgTara = New System.Windows.Forms.Label()
        Me.lblWaktuTara = New System.Windows.Forms.Label()
        Me.pnlSeparator1 = New System.Windows.Forms.Panel()
        Me.lblBeratNettoLabel = New System.Windows.Forms.Label()
        Me.txtBeratNetto = New System.Windows.Forms.TextBox()
        Me.lblKgNetto = New System.Windows.Forms.Label()
        Me.grpPotongan = New System.Windows.Forms.GroupBox()
        Me.lblPotonganPersenLabel = New System.Windows.Forms.Label()
        Me.txtPotonganPersen = New System.Windows.Forms.TextBox()
        Me.lblPersen = New System.Windows.Forms.Label()
        Me.lblPotonganKgLabel = New System.Windows.Forms.Label()
        Me.txtPotonganKg = New System.Windows.Forms.TextBox()
        Me.lblKgPot = New System.Windows.Forms.Label()
        Me.pnlSeparator2 = New System.Windows.Forms.Panel()
        Me.lblTotalPotonganLabel = New System.Windows.Forms.Label()
        Me.txtTotalPotongan = New System.Windows.Forms.TextBox()
        Me.lblKgTotalPot = New System.Windows.Forms.Label()
        Me.pnlBeratBersih = New System.Windows.Forms.Panel()
        Me.lblBeratBersihTitle = New System.Windows.Forms.Label()
        Me.txtBeratBersih = New System.Windows.Forms.TextBox()
        Me.lblKgBersih = New System.Windows.Forms.Label()
        Me.grpAlasanEdit = New System.Windows.Forms.GroupBox()
        Me.lblAlasanEditLabel = New System.Windows.Forms.Label()
        Me.txtAlasanEdit = New System.Windows.Forms.TextBox()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnSimpan = New System.Windows.Forms.Button()
        Me.btnCetak = New System.Windows.Forms.Button()
        Me.btnBatal = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.grpSegel.SuspendLayout()
        Me.grpInfoTiket.SuspendLayout()
        Me.grpDataKendaraan.SuspendLayout()
        Me.grpFFA.SuspendLayout()
        Me.grpKeterangan.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.grpDataBerat.SuspendLayout()
        Me.grpPotongan.SuspendLayout()
        Me.pnlBeratBersih.SuspendLayout()
        Me.grpAlasanEdit.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblEditInfo)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(990, 49)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(333, 32)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "✏️ EDIT DATA TIMBANGAN"
        '
        'lblEditInfo
        '
        Me.lblEditInfo.AutoSize = True
        Me.lblEditInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEditInfo.ForeColor = System.Drawing.Color.White
        Me.lblEditInfo.Location = New System.Drawing.Point(506, 21)
        Me.lblEditInfo.Name = "lblEditInfo"
        Me.lblEditInfo.Size = New System.Drawing.Size(164, 20)
        Me.lblEditInfo.TabIndex = 1
        Me.lblEditInfo.Text = "👤 Diedit oleh : - 📅 -"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.White
        Me.pnlLeft.Controls.Add(Me.grpSegel)
        Me.pnlLeft.Controls.Add(Me.grpInfoTiket)
        Me.pnlLeft.Controls.Add(Me.grpDataKendaraan)
        Me.pnlLeft.Controls.Add(Me.grpFFA)
        Me.pnlLeft.Controls.Add(Me.grpKeterangan)
        Me.pnlLeft.Location = New System.Drawing.Point(4, 55)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(490, 660)
        Me.pnlLeft.TabIndex = 1
        '
        'grpSegel
        '
        Me.grpSegel.Controls.Add(Me.txtSegelBawah)
        Me.grpSegel.Controls.Add(Me.txtSegelAtas)
        Me.grpSegel.Controls.Add(Me.lblSegelBawah)
        Me.grpSegel.Controls.Add(Me.lblSegelAtas)
        Me.grpSegel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSegel.Location = New System.Drawing.Point(10, 367)
        Me.grpSegel.Name = "grpSegel"
        Me.grpSegel.Size = New System.Drawing.Size(470, 46)
        Me.grpSegel.TabIndex = 4
        Me.grpSegel.TabStop = False
        Me.grpSegel.Text = "Nomor Segel (Seal Security)"
        '
        'txtSegelBawah
        '
        Me.txtSegelBawah.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSegelBawah.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSegelBawah.Location = New System.Drawing.Point(292, 18)
        Me.txtSegelBawah.MaxLength = 20
        Me.txtSegelBawah.Name = "txtSegelBawah"
        Me.txtSegelBawah.Size = New System.Drawing.Size(142, 27)
        Me.txtSegelBawah.TabIndex = 3
        Me.txtSegelBawah.TabStop = False
        Me.txtSegelBawah.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSegelAtas
        '
        Me.txtSegelAtas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSegelAtas.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSegelAtas.Location = New System.Drawing.Point(75, 18)
        Me.txtSegelAtas.MaxLength = 20
        Me.txtSegelAtas.Name = "txtSegelAtas"
        Me.txtSegelAtas.Size = New System.Drawing.Size(142, 27)
        Me.txtSegelAtas.TabIndex = 2
        Me.txtSegelAtas.TabStop = False
        '
        'lblSegelBawah
        '
        Me.lblSegelBawah.AutoSize = True
        Me.lblSegelBawah.ForeColor = System.Drawing.Color.DarkRed
        Me.lblSegelBawah.Location = New System.Drawing.Point(229, 21)
        Me.lblSegelBawah.Name = "lblSegelBawah"
        Me.lblSegelBawah.Size = New System.Drawing.Size(68, 20)
        Me.lblSegelBawah.TabIndex = 1
        Me.lblSegelBawah.Text = "Bawah : "
        '
        'lblSegelAtas
        '
        Me.lblSegelAtas.AutoSize = True
        Me.lblSegelAtas.ForeColor = System.Drawing.Color.DarkRed
        Me.lblSegelAtas.Location = New System.Drawing.Point(29, 21)
        Me.lblSegelAtas.Name = "lblSegelAtas"
        Me.lblSegelAtas.Size = New System.Drawing.Size(49, 20)
        Me.lblSegelAtas.TabIndex = 0
        Me.lblSegelAtas.Text = "Atas :"
        '
        'grpInfoTiket
        '
        Me.grpInfoTiket.Controls.Add(Me.lblNoTiketLabel)
        Me.grpInfoTiket.Controls.Add(Me.txtNoTiket)
        Me.grpInfoTiket.Controls.Add(Me.lblStatusLabel)
        Me.grpInfoTiket.Controls.Add(Me.lblStatus)
        Me.grpInfoTiket.Controls.Add(Me.lblTglInputLabel)
        Me.grpInfoTiket.Controls.Add(Me.txtTglInput)
        Me.grpInfoTiket.Controls.Add(Me.lblOperatorLabel)
        Me.grpInfoTiket.Controls.Add(Me.txtOperator)
        Me.grpInfoTiket.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpInfoTiket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.grpInfoTiket.Location = New System.Drawing.Point(10, 5)
        Me.grpInfoTiket.Name = "grpInfoTiket"
        Me.grpInfoTiket.Size = New System.Drawing.Size(470, 112)
        Me.grpInfoTiket.TabIndex = 0
        Me.grpInfoTiket.TabStop = False
        Me.grpInfoTiket.Text = "📋 INFO TIKET (Read Only)"
        '
        'lblNoTiketLabel
        '
        Me.lblNoTiketLabel.AutoSize = True
        Me.lblNoTiketLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoTiketLabel.ForeColor = System.Drawing.Color.Black
        Me.lblNoTiketLabel.Location = New System.Drawing.Point(17, 27)
        Me.lblNoTiketLabel.Name = "lblNoTiketLabel"
        Me.lblNoTiketLabel.Size = New System.Drawing.Size(91, 20)
        Me.lblNoTiketLabel.TabIndex = 0
        Me.lblNoTiketLabel.Text = "No. Tiket     :"
        '
        'txtNoTiket
        '
        Me.txtNoTiket.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtNoTiket.Font = New System.Drawing.Font("Consolas", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtNoTiket.ForeColor = System.Drawing.Color.DarkBlue
        Me.txtNoTiket.Location = New System.Drawing.Point(128, 25)
        Me.txtNoTiket.Name = "txtNoTiket"
        Me.txtNoTiket.ReadOnly = True
        Me.txtNoTiket.Size = New System.Drawing.Size(150, 27)
        Me.txtNoTiket.TabIndex = 1
        Me.txtNoTiket.TabStop = False
        '
        'lblStatusLabel
        '
        Me.lblStatusLabel.AutoSize = True
        Me.lblStatusLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusLabel.ForeColor = System.Drawing.Color.Black
        Me.lblStatusLabel.Location = New System.Drawing.Point(288, 27)
        Me.lblStatusLabel.Name = "lblStatusLabel"
        Me.lblStatusLabel.Size = New System.Drawing.Size(56, 20)
        Me.lblStatusLabel.TabIndex = 2
        Me.lblStatusLabel.Text = "Status :"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.LightGray
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.ForeColor = System.Drawing.Color.Green
        Me.lblStatus.Location = New System.Drawing.Point(350, 25)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Padding = New System.Windows.Forms.Padding(8, 2, 8, 2)
        Me.lblStatus.Size = New System.Drawing.Size(100, 27)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "SELESAI"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTglInputLabel
        '
        Me.lblTglInputLabel.AutoSize = True
        Me.lblTglInputLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTglInputLabel.ForeColor = System.Drawing.Color.Black
        Me.lblTglInputLabel.Location = New System.Drawing.Point(17, 53)
        Me.lblTglInputLabel.Name = "lblTglInputLabel"
        Me.lblTglInputLabel.Size = New System.Drawing.Size(90, 20)
        Me.lblTglInputLabel.TabIndex = 4
        Me.lblTglInputLabel.Text = "Tgl Input     :"
        '
        'txtTglInput
        '
        Me.txtTglInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtTglInput.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtTglInput.Location = New System.Drawing.Point(128, 53)
        Me.txtTglInput.Name = "txtTglInput"
        Me.txtTglInput.ReadOnly = True
        Me.txtTglInput.Size = New System.Drawing.Size(150, 27)
        Me.txtTglInput.TabIndex = 5
        Me.txtTglInput.TabStop = False
        '
        'lblOperatorLabel
        '
        Me.lblOperatorLabel.AutoSize = True
        Me.lblOperatorLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperatorLabel.ForeColor = System.Drawing.Color.Black
        Me.lblOperatorLabel.Location = New System.Drawing.Point(15, 84)
        Me.lblOperatorLabel.Name = "lblOperatorLabel"
        Me.lblOperatorLabel.Size = New System.Drawing.Size(92, 20)
        Me.lblOperatorLabel.TabIndex = 6
        Me.lblOperatorLabel.Text = "Operator     :"
        '
        'txtOperator
        '
        Me.txtOperator.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtOperator.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtOperator.Location = New System.Drawing.Point(128, 81)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.ReadOnly = True
        Me.txtOperator.Size = New System.Drawing.Size(150, 27)
        Me.txtOperator.TabIndex = 7
        Me.txtOperator.TabStop = False
        '
        'grpDataKendaraan
        '
        Me.grpDataKendaraan.Controls.Add(Me.txtNoKontrak)
        Me.grpDataKendaraan.Controls.Add(Me.Label1)
        Me.grpDataKendaraan.Controls.Add(Me.lblNoPolisi)
        Me.grpDataKendaraan.Controls.Add(Me.txtNoPolisi)
        Me.grpDataKendaraan.Controls.Add(Me.lblNamaSupir)
        Me.grpDataKendaraan.Controls.Add(Me.txtNamaSupir)
        Me.grpDataKendaraan.Controls.Add(Me.lblNoDO)
        Me.grpDataKendaraan.Controls.Add(Me.txtNoDO)
        Me.grpDataKendaraan.Controls.Add(Me.lblTransporter)
        Me.grpDataKendaraan.Controls.Add(Me.cmbTransporter)
        Me.grpDataKendaraan.Controls.Add(Me.lblCustomer)
        Me.grpDataKendaraan.Controls.Add(Me.cmbCustomer)
        Me.grpDataKendaraan.Controls.Add(Me.lblProduct)
        Me.grpDataKendaraan.Controls.Add(Me.cmbProduct)
        Me.grpDataKendaraan.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpDataKendaraan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.grpDataKendaraan.Location = New System.Drawing.Point(10, 115)
        Me.grpDataKendaraan.Name = "grpDataKendaraan"
        Me.grpDataKendaraan.Size = New System.Drawing.Size(470, 246)
        Me.grpDataKendaraan.TabIndex = 1
        Me.grpDataKendaraan.TabStop = False
        Me.grpDataKendaraan.Text = "🚛 DATA KENDARAAN"
        '
        'txtNoKontrak
        '
        Me.txtNoKontrak.Font = New System.Drawing.Font("Segoe UI", 10.2!)
        Me.txtNoKontrak.Location = New System.Drawing.Point(128, 209)
        Me.txtNoKontrak.Name = "txtNoKontrak"
        Me.txtNoKontrak.Size = New System.Drawing.Size(327, 30)
        Me.txtNoKontrak.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(15, 214)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 20)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "No Kontrak  :"
        '
        'lblNoPolisi
        '
        Me.lblNoPolisi.AutoSize = True
        Me.lblNoPolisi.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoPolisi.ForeColor = System.Drawing.Color.Black
        Me.lblNoPolisi.Location = New System.Drawing.Point(15, 28)
        Me.lblNoPolisi.Name = "lblNoPolisi"
        Me.lblNoPolisi.Size = New System.Drawing.Size(93, 20)
        Me.lblNoPolisi.TabIndex = 0
        Me.lblNoPolisi.Text = "No. Polisi     :"
        '
        'txtNoPolisi
        '
        Me.txtNoPolisi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNoPolisi.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtNoPolisi.Location = New System.Drawing.Point(128, 23)
        Me.txtNoPolisi.MaxLength = 15
        Me.txtNoPolisi.Name = "txtNoPolisi"
        Me.txtNoPolisi.Size = New System.Drawing.Size(327, 30)
        Me.txtNoPolisi.TabIndex = 1
        '
        'lblNamaSupir
        '
        Me.lblNamaSupir.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNamaSupir.ForeColor = System.Drawing.Color.Black
        Me.lblNamaSupir.Location = New System.Drawing.Point(15, 58)
        Me.lblNamaSupir.Name = "lblNamaSupir"
        Me.lblNamaSupir.Size = New System.Drawing.Size(103, 20)
        Me.lblNamaSupir.TabIndex = 2
        Me.lblNamaSupir.Text = "Nama Supir :"
        '
        'txtNamaSupir
        '
        Me.txtNamaSupir.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtNamaSupir.Location = New System.Drawing.Point(128, 55)
        Me.txtNamaSupir.MaxLength = 100
        Me.txtNamaSupir.Name = "txtNamaSupir"
        Me.txtNamaSupir.Size = New System.Drawing.Size(327, 27)
        Me.txtNamaSupir.TabIndex = 3
        '
        'lblNoDO
        '
        Me.lblNoDO.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoDO.ForeColor = System.Drawing.Color.Black
        Me.lblNoDO.Location = New System.Drawing.Point(15, 88)
        Me.lblNoDO.Name = "lblNoDO"
        Me.lblNoDO.Size = New System.Drawing.Size(107, 20)
        Me.lblNoDO.TabIndex = 4
        Me.lblNoDO.Text = "No. DO         :"
        '
        'txtNoDO
        '
        Me.txtNoDO.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtNoDO.Location = New System.Drawing.Point(128, 85)
        Me.txtNoDO.MaxLength = 50
        Me.txtNoDO.Name = "txtNoDO"
        Me.txtNoDO.Size = New System.Drawing.Size(327, 27)
        Me.txtNoDO.TabIndex = 5
        '
        'lblTransporter
        '
        Me.lblTransporter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporter.ForeColor = System.Drawing.Color.Black
        Me.lblTransporter.Location = New System.Drawing.Point(15, 118)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(107, 20)
        Me.lblTransporter.TabIndex = 6
        Me.lblTransporter.Text = "Transporter  :"
        '
        'cmbTransporter
        '
        Me.cmbTransporter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTransporter.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbTransporter.FormattingEnabled = True
        Me.cmbTransporter.Location = New System.Drawing.Point(128, 115)
        Me.cmbTransporter.Name = "cmbTransporter"
        Me.cmbTransporter.Size = New System.Drawing.Size(327, 28)
        Me.cmbTransporter.TabIndex = 7
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.ForeColor = System.Drawing.Color.Black
        Me.lblCustomer.Location = New System.Drawing.Point(15, 148)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(107, 20)
        Me.lblCustomer.TabIndex = 8
        Me.lblCustomer.Text = "Customer     :"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCustomer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbCustomer.FormattingEnabled = True
        Me.cmbCustomer.Location = New System.Drawing.Point(128, 145)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(327, 28)
        Me.cmbCustomer.TabIndex = 9
        '
        'lblProduct
        '
        Me.lblProduct.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProduct.ForeColor = System.Drawing.Color.Black
        Me.lblProduct.Location = New System.Drawing.Point(15, 178)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(103, 20)
        Me.lblProduct.TabIndex = 10
        Me.lblProduct.Text = "Product        :"
        '
        'cmbProduct
        '
        Me.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProduct.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbProduct.FormattingEnabled = True
        Me.cmbProduct.Location = New System.Drawing.Point(128, 175)
        Me.cmbProduct.Name = "cmbProduct"
        Me.cmbProduct.Size = New System.Drawing.Size(327, 28)
        Me.cmbProduct.TabIndex = 11
        '
        'grpFFA
        '
        Me.grpFFA.Controls.Add(Me.chkIncludeFFA)
        Me.grpFFA.Controls.Add(Me.lblFFA)
        Me.grpFFA.Controls.Add(Me.txtFFA)
        Me.grpFFA.Controls.Add(Me.lblMoisture)
        Me.grpFFA.Controls.Add(Me.txtMoisture)
        Me.grpFFA.Controls.Add(Me.lblDirt)
        Me.grpFFA.Controls.Add(Me.txtDirt)
        Me.grpFFA.Controls.Add(Me.lblSuhuMinyak)
        Me.grpFFA.Controls.Add(Me.txtSuhuMinyak)
        Me.grpFFA.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpFFA.ForeColor = System.Drawing.Color.Purple
        Me.grpFFA.Location = New System.Drawing.Point(10, 418)
        Me.grpFFA.Name = "grpFFA"
        Me.grpFFA.Size = New System.Drawing.Size(470, 155)
        Me.grpFFA.TabIndex = 2
        Me.grpFFA.TabStop = False
        Me.grpFFA.Text = "🔬 ANALISA LAB"
        '
        'chkIncludeFFA
        '
        Me.chkIncludeFFA.AutoSize = True
        Me.chkIncludeFFA.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeFFA.ForeColor = System.Drawing.Color.Black
        Me.chkIncludeFFA.Location = New System.Drawing.Point(15, 22)
        Me.chkIncludeFFA.Name = "chkIncludeFFA"
        Me.chkIncludeFFA.Size = New System.Drawing.Size(347, 24)
        Me.chkIncludeFFA.TabIndex = 0
        Me.chkIncludeFFA.Text = "Include Analisa Lab (FFA, Moisture, Dirt, Suhu)"
        Me.chkIncludeFFA.UseVisualStyleBackColor = True
        '
        'lblFFA
        '
        Me.lblFFA.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblFFA.ForeColor = System.Drawing.Color.Black
        Me.lblFFA.Location = New System.Drawing.Point(10, 50)
        Me.lblFFA.Name = "lblFFA"
        Me.lblFFA.Size = New System.Drawing.Size(136, 18)
        Me.lblFFA.TabIndex = 1
        Me.lblFFA.Text = "FFA (Asam)"
        Me.lblFFA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtFFA
        '
        Me.txtFFA.BackColor = System.Drawing.Color.Gainsboro
        Me.txtFFA.Enabled = False
        Me.txtFFA.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtFFA.Location = New System.Drawing.Point(10, 70)
        Me.txtFFA.Name = "txtFFA"
        Me.txtFFA.Size = New System.Drawing.Size(136, 27)
        Me.txtFFA.TabIndex = 2
        Me.txtFFA.Text = "0"
        Me.txtFFA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblMoisture
        '
        Me.lblMoisture.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblMoisture.ForeColor = System.Drawing.Color.Black
        Me.lblMoisture.Location = New System.Drawing.Point(162, 50)
        Me.lblMoisture.Name = "lblMoisture"
        Me.lblMoisture.Size = New System.Drawing.Size(136, 18)
        Me.lblMoisture.TabIndex = 3
        Me.lblMoisture.Text = "Moisture (Air)"
        Me.lblMoisture.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMoisture
        '
        Me.txtMoisture.BackColor = System.Drawing.Color.Gainsboro
        Me.txtMoisture.Enabled = False
        Me.txtMoisture.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtMoisture.Location = New System.Drawing.Point(162, 70)
        Me.txtMoisture.Name = "txtMoisture"
        Me.txtMoisture.Size = New System.Drawing.Size(136, 27)
        Me.txtMoisture.TabIndex = 4
        Me.txtMoisture.Text = "0"
        Me.txtMoisture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDirt
        '
        Me.lblDirt.AutoSize = True
        Me.lblDirt.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblDirt.ForeColor = System.Drawing.Color.Black
        Me.lblDirt.Location = New System.Drawing.Point(314, 50)
        Me.lblDirt.Name = "lblDirt"
        Me.lblDirt.Size = New System.Drawing.Size(101, 20)
        Me.lblDirt.TabIndex = 5
        Me.lblDirt.Text = "Dirt (Kotoran)"
        Me.lblDirt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDirt
        '
        Me.txtDirt.BackColor = System.Drawing.Color.Gainsboro
        Me.txtDirt.Enabled = False
        Me.txtDirt.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtDirt.Location = New System.Drawing.Point(314, 70)
        Me.txtDirt.Name = "txtDirt"
        Me.txtDirt.Size = New System.Drawing.Size(136, 27)
        Me.txtDirt.TabIndex = 6
        Me.txtDirt.Text = "0"
        Me.txtDirt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblSuhuMinyak
        '
        Me.lblSuhuMinyak.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblSuhuMinyak.ForeColor = System.Drawing.Color.Black
        Me.lblSuhuMinyak.Location = New System.Drawing.Point(10, 103)
        Me.lblSuhuMinyak.Name = "lblSuhuMinyak"
        Me.lblSuhuMinyak.Size = New System.Drawing.Size(136, 18)
        Me.lblSuhuMinyak.TabIndex = 7
        Me.lblSuhuMinyak.Text = "Suhu Minyak (°C)"
        Me.lblSuhuMinyak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSuhuMinyak
        '
        Me.txtSuhuMinyak.BackColor = System.Drawing.Color.Gainsboro
        Me.txtSuhuMinyak.Enabled = False
        Me.txtSuhuMinyak.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSuhuMinyak.Location = New System.Drawing.Point(10, 123)
        Me.txtSuhuMinyak.Name = "txtSuhuMinyak"
        Me.txtSuhuMinyak.Size = New System.Drawing.Size(136, 27)
        Me.txtSuhuMinyak.TabIndex = 8
        Me.txtSuhuMinyak.Text = "0"
        Me.txtSuhuMinyak.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'grpKeterangan
        '
        Me.grpKeterangan.Controls.Add(Me.lblKeteranganLabel)
        Me.grpKeterangan.Controls.Add(Me.txtKeterangan)
        Me.grpKeterangan.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpKeterangan.ForeColor = System.Drawing.Color.DarkGreen
        Me.grpKeterangan.Location = New System.Drawing.Point(10, 571)
        Me.grpKeterangan.Name = "grpKeterangan"
        Me.grpKeterangan.Size = New System.Drawing.Size(470, 81)
        Me.grpKeterangan.TabIndex = 3
        Me.grpKeterangan.TabStop = False
        Me.grpKeterangan.Text = "📝 KETERANGAN"
        '
        'lblKeteranganLabel
        '
        Me.lblKeteranganLabel.AutoSize = True
        Me.lblKeteranganLabel.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!)
        Me.lblKeteranganLabel.ForeColor = System.Drawing.Color.Black
        Me.lblKeteranganLabel.Location = New System.Drawing.Point(14, 17)
        Me.lblKeteranganLabel.Name = "lblKeteranganLabel"
        Me.lblKeteranganLabel.Size = New System.Drawing.Size(170, 20)
        Me.lblKeteranganLabel.TabIndex = 0
        Me.lblKeteranganLabel.Text = "Keterangan / Catatan :"
        '
        'txtKeterangan
        '
        Me.txtKeterangan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtKeterangan.Location = New System.Drawing.Point(15, 40)
        Me.txtKeterangan.MaxLength = 500
        Me.txtKeterangan.Multiline = True
        Me.txtKeterangan.Name = "txtKeterangan"
        Me.txtKeterangan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtKeterangan.Size = New System.Drawing.Size(440, 35)
        Me.txtKeterangan.TabIndex = 1
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.White
        Me.pnlRight.Controls.Add(Me.grpDataBerat)
        Me.pnlRight.Controls.Add(Me.grpPotongan)
        Me.pnlRight.Controls.Add(Me.pnlBeratBersih)
        Me.pnlRight.Controls.Add(Me.grpAlasanEdit)
        Me.pnlRight.Location = New System.Drawing.Point(500, 55)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(490, 607)
        Me.pnlRight.TabIndex = 2
        '
        'grpDataBerat
        '
        Me.grpDataBerat.Controls.Add(Me.lblBeratBrutoLabel)
        Me.grpDataBerat.Controls.Add(Me.txtBeratBruto)
        Me.grpDataBerat.Controls.Add(Me.lblKgBruto)
        Me.grpDataBerat.Controls.Add(Me.lblWaktuBruto)
        Me.grpDataBerat.Controls.Add(Me.lblBeratTaraLabel)
        Me.grpDataBerat.Controls.Add(Me.txtBeratTara)
        Me.grpDataBerat.Controls.Add(Me.lblKgTara)
        Me.grpDataBerat.Controls.Add(Me.lblWaktuTara)
        Me.grpDataBerat.Controls.Add(Me.pnlSeparator1)
        Me.grpDataBerat.Controls.Add(Me.lblBeratNettoLabel)
        Me.grpDataBerat.Controls.Add(Me.txtBeratNetto)
        Me.grpDataBerat.Controls.Add(Me.lblKgNetto)
        Me.grpDataBerat.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpDataBerat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.grpDataBerat.Location = New System.Drawing.Point(10, 5)
        Me.grpDataBerat.Name = "grpDataBerat"
        Me.grpDataBerat.Size = New System.Drawing.Size(470, 155)
        Me.grpDataBerat.TabIndex = 0
        Me.grpDataBerat.TabStop = False
        Me.grpDataBerat.Text = "⚖️ DATA BERAT"
        '
        'lblBeratBrutoLabel
        '
        Me.lblBeratBrutoLabel.AutoSize = True
        Me.lblBeratBrutoLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBeratBrutoLabel.ForeColor = System.Drawing.Color.Black
        Me.lblBeratBrutoLabel.Location = New System.Drawing.Point(15, 31)
        Me.lblBeratBrutoLabel.Name = "lblBeratBrutoLabel"
        Me.lblBeratBrutoLabel.Size = New System.Drawing.Size(174, 23)
        Me.lblBeratBrutoLabel.TabIndex = 0
        Me.lblBeratBrutoLabel.Text = "Berat Masuk (Bruto) :"
        '
        'txtBeratBruto
        '
        Me.txtBeratBruto.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtBeratBruto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.txtBeratBruto.Location = New System.Drawing.Point(280, 23)
        Me.txtBeratBruto.Name = "txtBeratBruto"
        Me.txtBeratBruto.Size = New System.Drawing.Size(120, 34)
        Me.txtBeratBruto.TabIndex = 1
        Me.txtBeratBruto.Text = "0"
        Me.txtBeratBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblKgBruto
        '
        Me.lblKgBruto.AutoSize = True
        Me.lblKgBruto.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblKgBruto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.lblKgBruto.Location = New System.Drawing.Point(406, 28)
        Me.lblKgBruto.Name = "lblKgBruto"
        Me.lblKgBruto.Size = New System.Drawing.Size(37, 25)
        Me.lblKgBruto.TabIndex = 2
        Me.lblKgBruto.Text = "KG"
        '
        'lblWaktuBruto
        '
        Me.lblWaktuBruto.AutoSize = True
        Me.lblWaktuBruto.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblWaktuBruto.ForeColor = System.Drawing.Color.Gray
        Me.lblWaktuBruto.Location = New System.Drawing.Point(164, 34)
        Me.lblWaktuBruto.Name = "lblWaktuBruto"
        Me.lblWaktuBruto.Size = New System.Drawing.Size(15, 19)
        Me.lblWaktuBruto.TabIndex = 3
        Me.lblWaktuBruto.Text = "-"
        '
        'lblBeratTaraLabel
        '
        Me.lblBeratTaraLabel.AutoSize = True
        Me.lblBeratTaraLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBeratTaraLabel.ForeColor = System.Drawing.Color.Blue
        Me.lblBeratTaraLabel.Location = New System.Drawing.Point(15, 65)
        Me.lblBeratTaraLabel.Name = "lblBeratTaraLabel"
        Me.lblBeratTaraLabel.Size = New System.Drawing.Size(175, 23)
        Me.lblBeratTaraLabel.TabIndex = 4
        Me.lblBeratTaraLabel.Text = "Berat Keluar (Tara)    :"
        '
        'txtBeratTara
        '
        Me.txtBeratTara.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtBeratTara.ForeColor = System.Drawing.Color.Blue
        Me.txtBeratTara.Location = New System.Drawing.Point(280, 58)
        Me.txtBeratTara.Name = "txtBeratTara"
        Me.txtBeratTara.Size = New System.Drawing.Size(120, 34)
        Me.txtBeratTara.TabIndex = 5
        Me.txtBeratTara.Text = "0"
        Me.txtBeratTara.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblKgTara
        '
        Me.lblKgTara.AutoSize = True
        Me.lblKgTara.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblKgTara.ForeColor = System.Drawing.Color.Blue
        Me.lblKgTara.Location = New System.Drawing.Point(406, 63)
        Me.lblKgTara.Name = "lblKgTara"
        Me.lblKgTara.Size = New System.Drawing.Size(37, 25)
        Me.lblKgTara.TabIndex = 6
        Me.lblKgTara.Text = "KG"
        '
        'lblWaktuTara
        '
        Me.lblWaktuTara.AutoSize = True
        Me.lblWaktuTara.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblWaktuTara.ForeColor = System.Drawing.Color.Gray
        Me.lblWaktuTara.Location = New System.Drawing.Point(164, 69)
        Me.lblWaktuTara.Name = "lblWaktuTara"
        Me.lblWaktuTara.Size = New System.Drawing.Size(15, 19)
        Me.lblWaktuTara.TabIndex = 7
        Me.lblWaktuTara.Text = "-"
        '
        'pnlSeparator1
        '
        Me.pnlSeparator1.BackColor = System.Drawing.Color.DarkGray
        Me.pnlSeparator1.Location = New System.Drawing.Point(15, 98)
        Me.pnlSeparator1.Name = "pnlSeparator1"
        Me.pnlSeparator1.Size = New System.Drawing.Size(440, 2)
        Me.pnlSeparator1.TabIndex = 8
        '
        'lblBeratNettoLabel
        '
        Me.lblBeratNettoLabel.AutoSize = True
        Me.lblBeratNettoLabel.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblBeratNettoLabel.ForeColor = System.Drawing.Color.Black
        Me.lblBeratNettoLabel.Location = New System.Drawing.Point(15, 112)
        Me.lblBeratNettoLabel.Name = "lblBeratNettoLabel"
        Me.lblBeratNettoLabel.Size = New System.Drawing.Size(180, 25)
        Me.lblBeratNettoLabel.TabIndex = 9
        Me.lblBeratNettoLabel.Text = "Berat Netto            :"
        '
        'txtBeratNetto
        '
        Me.txtBeratNetto.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtBeratNetto.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtBeratNetto.ForeColor = System.Drawing.Color.Black
        Me.txtBeratNetto.Location = New System.Drawing.Point(280, 110)
        Me.txtBeratNetto.Name = "txtBeratNetto"
        Me.txtBeratNetto.ReadOnly = True
        Me.txtBeratNetto.Size = New System.Drawing.Size(120, 34)
        Me.txtBeratNetto.TabIndex = 10
        Me.txtBeratNetto.TabStop = False
        Me.txtBeratNetto.Text = "0"
        Me.txtBeratNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblKgNetto
        '
        Me.lblKgNetto.AutoSize = True
        Me.lblKgNetto.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblKgNetto.ForeColor = System.Drawing.Color.Black
        Me.lblKgNetto.Location = New System.Drawing.Point(406, 116)
        Me.lblKgNetto.Name = "lblKgNetto"
        Me.lblKgNetto.Size = New System.Drawing.Size(37, 25)
        Me.lblKgNetto.TabIndex = 11
        Me.lblKgNetto.Text = "KG"
        '
        'grpPotongan
        '
        Me.grpPotongan.Controls.Add(Me.lblPotonganPersenLabel)
        Me.grpPotongan.Controls.Add(Me.txtPotonganPersen)
        Me.grpPotongan.Controls.Add(Me.lblPersen)
        Me.grpPotongan.Controls.Add(Me.lblPotonganKgLabel)
        Me.grpPotongan.Controls.Add(Me.txtPotonganKg)
        Me.grpPotongan.Controls.Add(Me.lblKgPot)
        Me.grpPotongan.Controls.Add(Me.pnlSeparator2)
        Me.grpPotongan.Controls.Add(Me.lblTotalPotonganLabel)
        Me.grpPotongan.Controls.Add(Me.txtTotalPotongan)
        Me.grpPotongan.Controls.Add(Me.lblKgTotalPot)
        Me.grpPotongan.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpPotongan.ForeColor = System.Drawing.Color.DarkOrange
        Me.grpPotongan.Location = New System.Drawing.Point(10, 165)
        Me.grpPotongan.Name = "grpPotongan"
        Me.grpPotongan.Size = New System.Drawing.Size(470, 120)
        Me.grpPotongan.TabIndex = 1
        Me.grpPotongan.TabStop = False
        Me.grpPotongan.Text = "✂️ POTONGAN"
        '
        'lblPotonganPersenLabel
        '
        Me.lblPotonganPersenLabel.AutoSize = True
        Me.lblPotonganPersenLabel.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!)
        Me.lblPotonganPersenLabel.ForeColor = System.Drawing.Color.Black
        Me.lblPotonganPersenLabel.Location = New System.Drawing.Point(15, 28)
        Me.lblPotonganPersenLabel.Name = "lblPotonganPersenLabel"
        Me.lblPotonganPersenLabel.Size = New System.Drawing.Size(88, 20)
        Me.lblPotonganPersenLabel.TabIndex = 0
        Me.lblPotonganPersenLabel.Text = "Potongan :"
        '
        'txtPotonganPersen
        '
        Me.txtPotonganPersen.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPotonganPersen.Location = New System.Drawing.Point(100, 24)
        Me.txtPotonganPersen.Name = "txtPotonganPersen"
        Me.txtPotonganPersen.Size = New System.Drawing.Size(70, 30)
        Me.txtPotonganPersen.TabIndex = 1
        Me.txtPotonganPersen.Text = "0"
        Me.txtPotonganPersen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPersen
        '
        Me.lblPersen.AutoSize = True
        Me.lblPersen.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPersen.ForeColor = System.Drawing.Color.Black
        Me.lblPersen.Location = New System.Drawing.Point(176, 27)
        Me.lblPersen.Name = "lblPersen"
        Me.lblPersen.Size = New System.Drawing.Size(25, 23)
        Me.lblPersen.TabIndex = 2
        Me.lblPersen.Text = "%"
        '
        'lblPotonganKgLabel
        '
        Me.lblPotonganKgLabel.AutoSize = True
        Me.lblPotonganKgLabel.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!)
        Me.lblPotonganKgLabel.ForeColor = System.Drawing.Color.Black
        Me.lblPotonganKgLabel.Location = New System.Drawing.Point(207, 30)
        Me.lblPotonganKgLabel.Name = "lblPotonganKgLabel"
        Me.lblPotonganKgLabel.Size = New System.Drawing.Size(112, 20)
        Me.lblPotonganKgLabel.TabIndex = 3
        Me.lblPotonganKgLabel.Text = "Potongan Kg :"
        '
        'txtPotonganKg
        '
        Me.txtPotonganKg.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPotonganKg.Location = New System.Drawing.Point(320, 24)
        Me.txtPotonganKg.Name = "txtPotonganKg"
        Me.txtPotonganKg.Size = New System.Drawing.Size(80, 30)
        Me.txtPotonganKg.TabIndex = 4
        Me.txtPotonganKg.Text = "0"
        Me.txtPotonganKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblKgPot
        '
        Me.lblKgPot.AutoSize = True
        Me.lblKgPot.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblKgPot.ForeColor = System.Drawing.Color.Black
        Me.lblKgPot.Location = New System.Drawing.Point(405, 26)
        Me.lblKgPot.Name = "lblKgPot"
        Me.lblKgPot.Size = New System.Drawing.Size(37, 25)
        Me.lblKgPot.TabIndex = 5
        Me.lblKgPot.Text = "KG"
        '
        'pnlSeparator2
        '
        Me.pnlSeparator2.BackColor = System.Drawing.Color.DarkOrange
        Me.pnlSeparator2.Location = New System.Drawing.Point(15, 60)
        Me.pnlSeparator2.Name = "pnlSeparator2"
        Me.pnlSeparator2.Size = New System.Drawing.Size(440, 2)
        Me.pnlSeparator2.TabIndex = 6
        '
        'lblTotalPotonganLabel
        '
        Me.lblTotalPotonganLabel.AutoSize = True
        Me.lblTotalPotonganLabel.Font = New System.Drawing.Font("Microsoft YaHei", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalPotonganLabel.ForeColor = System.Drawing.Color.Black
        Me.lblTotalPotonganLabel.Location = New System.Drawing.Point(16, 77)
        Me.lblTotalPotonganLabel.Name = "lblTotalPotonganLabel"
        Me.lblTotalPotonganLabel.Size = New System.Drawing.Size(177, 24)
        Me.lblTotalPotonganLabel.TabIndex = 7
        Me.lblTotalPotonganLabel.Text = "Total Potongan      :"
        '
        'txtTotalPotongan
        '
        Me.txtTotalPotongan.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtTotalPotongan.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotalPotongan.ForeColor = System.Drawing.Color.DarkOrange
        Me.txtTotalPotongan.Location = New System.Drawing.Point(280, 72)
        Me.txtTotalPotongan.Name = "txtTotalPotongan"
        Me.txtTotalPotongan.ReadOnly = True
        Me.txtTotalPotongan.Size = New System.Drawing.Size(120, 32)
        Me.txtTotalPotongan.TabIndex = 8
        Me.txtTotalPotongan.TabStop = False
        Me.txtTotalPotongan.Text = "0"
        Me.txtTotalPotongan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblKgTotalPot
        '
        Me.lblKgTotalPot.AutoSize = True
        Me.lblKgTotalPot.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblKgTotalPot.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblKgTotalPot.Location = New System.Drawing.Point(405, 75)
        Me.lblKgTotalPot.Name = "lblKgTotalPot"
        Me.lblKgTotalPot.Size = New System.Drawing.Size(37, 25)
        Me.lblKgTotalPot.TabIndex = 9
        Me.lblKgTotalPot.Text = "KG"
        '
        'pnlBeratBersih
        '
        Me.pnlBeratBersih.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlBeratBersih.Controls.Add(Me.lblBeratBersihTitle)
        Me.pnlBeratBersih.Controls.Add(Me.txtBeratBersih)
        Me.pnlBeratBersih.Controls.Add(Me.lblKgBersih)
        Me.pnlBeratBersih.Location = New System.Drawing.Point(10, 290)
        Me.pnlBeratBersih.Name = "pnlBeratBersih"
        Me.pnlBeratBersih.Size = New System.Drawing.Size(470, 70)
        Me.pnlBeratBersih.TabIndex = 2
        '
        'lblBeratBersihTitle
        '
        Me.lblBeratBersihTitle.AutoSize = True
        Me.lblBeratBersihTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblBeratBersihTitle.ForeColor = System.Drawing.Color.White
        Me.lblBeratBersihTitle.Location = New System.Drawing.Point(15, 10)
        Me.lblBeratBersihTitle.Name = "lblBeratBersihTitle"
        Me.lblBeratBersihTitle.Size = New System.Drawing.Size(268, 25)
        Me.lblBeratBersihTitle.TabIndex = 0
        Me.lblBeratBersihTitle.Text = "BERAT BERSIH (NETTO PAID)"
        '
        'txtBeratBersih
        '
        Me.txtBeratBersih.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txtBeratBersih.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBeratBersih.Font = New System.Drawing.Font("Segoe UI", 19.3!, System.Drawing.FontStyle.Bold)
        Me.txtBeratBersih.ForeColor = System.Drawing.Color.White
        Me.txtBeratBersih.Location = New System.Drawing.Point(19, 27)
        Me.txtBeratBersih.Name = "txtBeratBersih"
        Me.txtBeratBersih.ReadOnly = True
        Me.txtBeratBersih.Size = New System.Drawing.Size(200, 43)
        Me.txtBeratBersih.TabIndex = 1
        Me.txtBeratBersih.TabStop = False
        Me.txtBeratBersih.Text = "0"
        '
        'lblKgBersih
        '
        Me.lblKgBersih.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblKgBersih.ForeColor = System.Drawing.Color.White
        Me.lblKgBersih.Location = New System.Drawing.Point(410, 29)
        Me.lblKgBersih.Name = "lblKgBersih"
        Me.lblKgBersih.Size = New System.Drawing.Size(57, 41)
        Me.lblKgBersih.TabIndex = 2
        Me.lblKgBersih.Text = "KG"
        '
        'grpAlasanEdit
        '
        Me.grpAlasanEdit.Controls.Add(Me.lblAlasanEditLabel)
        Me.grpAlasanEdit.Controls.Add(Me.txtAlasanEdit)
        Me.grpAlasanEdit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpAlasanEdit.ForeColor = System.Drawing.Color.Red
        Me.grpAlasanEdit.Location = New System.Drawing.Point(10, 367)
        Me.grpAlasanEdit.Name = "grpAlasanEdit"
        Me.grpAlasanEdit.Size = New System.Drawing.Size(470, 178)
        Me.grpAlasanEdit.TabIndex = 3
        Me.grpAlasanEdit.TabStop = False
        Me.grpAlasanEdit.Text = "⚠️ ALASAN EDIT (wajib di isi!)"
        '
        'lblAlasanEditLabel
        '
        Me.lblAlasanEditLabel.AutoSize = True
        Me.lblAlasanEditLabel.Font = New System.Drawing.Font("Microsoft New Tai Lue", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlasanEditLabel.ForeColor = System.Drawing.Color.Black
        Me.lblAlasanEditLabel.Location = New System.Drawing.Point(15, 29)
        Me.lblAlasanEditLabel.Name = "lblAlasanEditLabel"
        Me.lblAlasanEditLabel.Size = New System.Drawing.Size(392, 20)
        Me.lblAlasanEditLabel.TabIndex = 0
        Me.lblAlasanEditLabel.Text = "Jelaskan mengapa data ini perlu di edit (min 10 karakter) :"
        '
        'txtAlasanEdit
        '
        Me.txtAlasanEdit.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtAlasanEdit.Location = New System.Drawing.Point(6, 41)
        Me.txtAlasanEdit.MaxLength = 500
        Me.txtAlasanEdit.Multiline = True
        Me.txtAlasanEdit.Name = "txtAlasanEdit"
        Me.txtAlasanEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAlasanEdit.Size = New System.Drawing.Size(440, 113)
        Me.txtAlasanEdit.TabIndex = 1
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlBottom.Controls.Add(Me.btnSimpan)
        Me.pnlBottom.Controls.Add(Me.btnCetak)
        Me.pnlBottom.Controls.Add(Me.btnBatal)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 715)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(990, 65)
        Me.pnlBottom.TabIndex = 3
        '
        'btnSimpan
        '
        Me.btnSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSimpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSimpan.FlatAppearance.BorderSize = 0
        Me.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSimpan.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnSimpan.ForeColor = System.Drawing.Color.White
        Me.btnSimpan.Location = New System.Drawing.Point(600, 12)
        Me.btnSimpan.Name = "btnSimpan"
        Me.btnSimpan.Size = New System.Drawing.Size(130, 42)
        Me.btnSimpan.TabIndex = 0
        Me.btnSimpan.Text = "💾 SIMPAN"
        Me.ToolTip1.SetToolTip(Me.btnSimpan, "Simpan perubahan data")
        Me.btnSimpan.UseVisualStyleBackColor = False
        '
        'btnCetak
        '
        Me.btnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.btnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCetak.FlatAppearance.BorderSize = 0
        Me.btnCetak.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCetak.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCetak.ForeColor = System.Drawing.Color.White
        Me.btnCetak.Location = New System.Drawing.Point(740, 12)
        Me.btnCetak.Name = "btnCetak"
        Me.btnCetak.Size = New System.Drawing.Size(120, 42)
        Me.btnCetak.TabIndex = 1
        Me.btnCetak.Text = "🖨️ CETAK"
        Me.ToolTip1.SetToolTip(Me.btnCetak, "Cetak tiket timbangan")
        Me.btnCetak.UseVisualStyleBackColor = False
        '
        'btnBatal
        '
        Me.btnBatal.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnBatal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBatal.FlatAppearance.BorderSize = 0
        Me.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBatal.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnBatal.ForeColor = System.Drawing.Color.White
        Me.btnBatal.Location = New System.Drawing.Point(870, 12)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(120, 42)
        Me.btnBatal.TabIndex = 2
        Me.btnBatal.Text = "❌ BATAL"
        Me.ToolTip1.SetToolTip(Me.btnBatal, "Batalkan perubahan")
        Me.btnBatal.UseVisualStyleBackColor = False
        '
        'FormEditTimbangan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1000, 733)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormEditTimbangan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "✏️ Edit Data Timbangan"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.grpSegel.ResumeLayout(False)
        Me.grpSegel.PerformLayout()
        Me.grpInfoTiket.ResumeLayout(False)
        Me.grpInfoTiket.PerformLayout()
        Me.grpDataKendaraan.ResumeLayout(False)
        Me.grpDataKendaraan.PerformLayout()
        Me.grpFFA.ResumeLayout(False)
        Me.grpFFA.PerformLayout()
        Me.grpKeterangan.ResumeLayout(False)
        Me.grpKeterangan.PerformLayout()
        Me.pnlRight.ResumeLayout(False)
        Me.grpDataBerat.ResumeLayout(False)
        Me.grpDataBerat.PerformLayout()
        Me.grpPotongan.ResumeLayout(False)
        Me.grpPotongan.PerformLayout()
        Me.pnlBeratBersih.ResumeLayout(False)
        Me.pnlBeratBersih.PerformLayout()
        Me.grpAlasanEdit.ResumeLayout(False)
        Me.grpAlasanEdit.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    ' =============================================
    ' CONTROL DECLARATIONS
    ' =============================================

    ' Panels
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlSeparator1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSeparator2 As System.Windows.Forms.Panel
    Friend WithEvents pnlBeratBersih As System.Windows.Forms.Panel

    ' Header
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblEditInfo As System.Windows.Forms.Label

    ' GroupBoxes
    Friend WithEvents grpInfoTiket As System.Windows.Forms.GroupBox
    Friend WithEvents grpDataKendaraan As System.Windows.Forms.GroupBox
    Friend WithEvents grpDataBerat As System.Windows.Forms.GroupBox
    Friend WithEvents grpPotongan As System.Windows.Forms.GroupBox
    Friend WithEvents grpFFA As System.Windows.Forms.GroupBox
    Friend WithEvents grpKeterangan As System.Windows.Forms.GroupBox
    Friend WithEvents grpAlasanEdit As System.Windows.Forms.GroupBox

    ' Info Tiket (Read Only)
    Friend WithEvents lblNoTiketLabel As System.Windows.Forms.Label
    Friend WithEvents txtNoTiket As System.Windows.Forms.TextBox
    Friend WithEvents lblStatusLabel As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblTglInputLabel As System.Windows.Forms.Label
    Friend WithEvents txtTglInput As System.Windows.Forms.TextBox
    Friend WithEvents lblOperatorLabel As System.Windows.Forms.Label
    Friend WithEvents txtOperator As System.Windows.Forms.TextBox

    ' Data Kendaraan
    Friend WithEvents lblNoPolisi As System.Windows.Forms.Label
    Friend WithEvents txtNoPolisi As System.Windows.Forms.TextBox
    Friend WithEvents lblNamaSupir As System.Windows.Forms.Label
    Friend WithEvents txtNamaSupir As System.Windows.Forms.TextBox
    Friend WithEvents lblNoDO As System.Windows.Forms.Label
    Friend WithEvents txtNoDO As System.Windows.Forms.TextBox
    Friend WithEvents lblTransporter As System.Windows.Forms.Label
    Friend WithEvents cmbTransporter As System.Windows.Forms.ComboBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents cmbProduct As System.Windows.Forms.ComboBox

    ' Data Berat
    Friend WithEvents lblBeratBrutoLabel As System.Windows.Forms.Label
    Friend WithEvents txtBeratBruto As System.Windows.Forms.TextBox
    Friend WithEvents lblKgBruto As System.Windows.Forms.Label
    Friend WithEvents lblWaktuBruto As System.Windows.Forms.Label
    Friend WithEvents lblBeratTaraLabel As System.Windows.Forms.Label
    Friend WithEvents txtBeratTara As System.Windows.Forms.TextBox
    Friend WithEvents lblKgTara As System.Windows.Forms.Label
    Friend WithEvents lblWaktuTara As System.Windows.Forms.Label
    Friend WithEvents lblBeratNettoLabel As System.Windows.Forms.Label
    Friend WithEvents txtBeratNetto As System.Windows.Forms.TextBox
    Friend WithEvents lblKgNetto As System.Windows.Forms.Label

    ' Potongan
    Friend WithEvents lblPotonganPersenLabel As System.Windows.Forms.Label
    Friend WithEvents txtPotonganPersen As System.Windows.Forms.TextBox
    Friend WithEvents lblPersen As System.Windows.Forms.Label
    Friend WithEvents lblPotonganKgLabel As System.Windows.Forms.Label
    Friend WithEvents txtPotonganKg As System.Windows.Forms.TextBox
    Friend WithEvents lblKgPot As System.Windows.Forms.Label
    Friend WithEvents lblTotalPotonganLabel As System.Windows.Forms.Label
    Friend WithEvents txtTotalPotongan As System.Windows.Forms.TextBox
    Friend WithEvents lblKgTotalPot As System.Windows.Forms.Label

    ' Berat Bersih
    Friend WithEvents lblBeratBersihTitle As System.Windows.Forms.Label
    Friend WithEvents txtBeratBersih As System.Windows.Forms.TextBox
    Friend WithEvents lblKgBersih As System.Windows.Forms.Label

    ' FFA
    Friend WithEvents chkIncludeFFA As System.Windows.Forms.CheckBox
    Friend WithEvents lblFFA As System.Windows.Forms.Label
    Friend WithEvents txtFFA As System.Windows.Forms.TextBox
    Friend WithEvents lblMoisture As System.Windows.Forms.Label
    Friend WithEvents txtMoisture As System.Windows.Forms.TextBox
    Friend WithEvents lblDirt As System.Windows.Forms.Label
    Friend WithEvents txtDirt As System.Windows.Forms.TextBox
    Friend WithEvents lblSuhuMinyak As System.Windows.Forms.Label
    Friend WithEvents txtSuhuMinyak As System.Windows.Forms.TextBox

    ' Keterangan
    Friend WithEvents lblKeteranganLabel As System.Windows.Forms.Label
    Friend WithEvents txtKeterangan As System.Windows.Forms.TextBox

    ' Alasan Edit
    Friend WithEvents lblAlasanEditLabel As System.Windows.Forms.Label
    Friend WithEvents txtAlasanEdit As System.Windows.Forms.TextBox

    ' Buttons
    Friend WithEvents btnSimpan As System.Windows.Forms.Button
    Friend WithEvents btnCetak As System.Windows.Forms.Button
    Friend WithEvents btnBatal As System.Windows.Forms.Button

    ' Tooltip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents grpSegel As GroupBox
    Friend WithEvents txtSegelBawah As TextBox
    Friend WithEvents txtSegelAtas As TextBox
    Friend WithEvents lblSegelBawah As Label
    Friend WithEvents lblSegelAtas As Label
    Friend WithEvents txtNoKontrak As TextBox
    Friend WithEvents Label1 As Label
End Class
