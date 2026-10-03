<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormAuditLog
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlStatsContainer = New System.Windows.Forms.Panel()
        Me.pnlCardTotal = New System.Windows.Forms.Panel()
        Me.lblCardTotalChange = New System.Windows.Forms.Label()
        Me.pnlCardTotalIcon = New System.Windows.Forms.Panel()
        Me.lblCardTotalIcon = New System.Windows.Forms.Label()
        Me.pnlCardTotalBorder = New System.Windows.Forms.Panel()
        Me.lblCardTotalValue = New System.Windows.Forms.Label()
        Me.lblCardTotalTitle = New System.Windows.Forms.Label()
        Me.pnlCardLogin = New System.Windows.Forms.Panel()
        Me.lblCardLoginChange = New System.Windows.Forms.Label()
        Me.pnlCardLoginIcon = New System.Windows.Forms.Panel()
        Me.lblCardLoginIcon = New System.Windows.Forms.Label()
        Me.pnlCardLoginBorder = New System.Windows.Forms.Panel()
        Me.lblCardLoginValue = New System.Windows.Forms.Label()
        Me.lblCardLoginTitle = New System.Windows.Forms.Label()
        Me.pnlCardChanges = New System.Windows.Forms.Panel()
        Me.lblCardChangesChange = New System.Windows.Forms.Label()
        Me.pnlCardChangesIcon = New System.Windows.Forms.Panel()
        Me.lblCardChangesIcon = New System.Windows.Forms.Label()
        Me.pnlCardChangesBorder = New System.Windows.Forms.Panel()
        Me.lblCardChangesValue = New System.Windows.Forms.Label()
        Me.lblCardChangesTitle = New System.Windows.Forms.Label()
        Me.pnlCardErrors = New System.Windows.Forms.Panel()
        Me.lblCardErrorsChange = New System.Windows.Forms.Label()
        Me.pnlCardErrorsIcon = New System.Windows.Forms.Panel()
        Me.lblCardErrorsIcon = New System.Windows.Forms.Label()
        Me.pnlCardErrorsBorder = New System.Windows.Forms.Panel()
        Me.lblCardErrorsValue = New System.Windows.Forms.Label()
        Me.lblCardErrorsTitle = New System.Windows.Forms.Label()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.cmbFilterUser = New System.Windows.Forms.ComboBox()
        Me.lblFilterUser = New System.Windows.Forms.Label()
        Me.cmbJenisAktivitas = New System.Windows.Forms.ComboBox()
        Me.lblFilterTitle = New System.Windows.Forms.Label()
        Me.lblTanggalMulai = New System.Windows.Forms.Label()
        Me.dtpTanggalMulai = New System.Windows.Forms.DateTimePicker()
        Me.lblTanggalAkhir = New System.Windows.Forms.Label()
        Me.dtpTanggalAkhir = New System.Windows.Forms.DateTimePicker()
        Me.lblJenisAktivitas = New System.Windows.Forms.Label()
        Me.lblPencarian = New System.Windows.Forms.Label()
        Me.txtPencarian = New System.Windows.Forms.TextBox()
        Me.btnTerapkanFilter = New System.Windows.Forms.Button()
        Me.btnResetFilter = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.lblHeaderSubtitle = New System.Windows.Forms.Label()
        Me.pnlHeaderIcon = New System.Windows.Forms.Panel()
        Me.lblHeaderIcon = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnHapusLogLama = New System.Windows.Forms.Button()
        Me.btnExportSemua = New System.Windows.Forms.Button()
        Me.lblHeaderTitle = New System.Windows.Forms.Label()
        Me.pnlTableContainer = New System.Windows.Forms.Panel()
        Me.pnlPagination = New System.Windows.Forms.Panel()
        Me.btnNextPage = New System.Windows.Forms.Button()
        Me.btnPage5 = New System.Windows.Forms.Button()
        Me.btnPage4 = New System.Windows.Forms.Button()
        Me.btnPage3 = New System.Windows.Forms.Button()
        Me.btnPage2 = New System.Windows.Forms.Button()
        Me.btnPage1 = New System.Windows.Forms.Button()
        Me.btnPrevPage = New System.Windows.Forms.Button()
        Me.lblPaginationInfo = New System.Windows.Forms.Label()
        Me.dgvAuditLog = New System.Windows.Forms.DataGridView()
        Me.colNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWaktu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAktivitas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colModule = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDeskripsi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIPAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAksi = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.pnlTableHeader = New System.Windows.Forms.Panel()
        Me.btnExportPDF = New System.Windows.Forms.Button()
        Me.lblRecordCount = New System.Windows.Forms.Label()
        Me.lblTableTitle = New System.Windows.Forms.Label()
        Me.pnlStatsContainer.SuspendLayout()
        Me.pnlCardTotal.SuspendLayout()
        Me.pnlCardTotalIcon.SuspendLayout()
        Me.pnlCardLogin.SuspendLayout()
        Me.pnlCardLoginIcon.SuspendLayout()
        Me.pnlCardChanges.SuspendLayout()
        Me.pnlCardChangesIcon.SuspendLayout()
        Me.pnlCardErrors.SuspendLayout()
        Me.pnlCardErrorsIcon.SuspendLayout()
        Me.pnlFilter.SuspendLayout()
        Me.pnlHeaderIcon.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlTableContainer.SuspendLayout()
        Me.pnlPagination.SuspendLayout()
        CType(Me.dgvAuditLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTableHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlStatsContainer
        '
        Me.pnlStatsContainer.BackColor = System.Drawing.Color.Transparent
        Me.pnlStatsContainer.Controls.Add(Me.pnlCardTotal)
        Me.pnlStatsContainer.Controls.Add(Me.pnlCardLogin)
        Me.pnlStatsContainer.Controls.Add(Me.pnlCardChanges)
        Me.pnlStatsContainer.Controls.Add(Me.pnlCardErrors)
        Me.pnlStatsContainer.Location = New System.Drawing.Point(20, 95)
        Me.pnlStatsContainer.Name = "pnlStatsContainer"
        Me.pnlStatsContainer.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlStatsContainer.Size = New System.Drawing.Size(1195, 102)
        Me.pnlStatsContainer.TabIndex = 3
        '
        'pnlCardTotal
        '
        Me.pnlCardTotal.BackColor = System.Drawing.Color.White
        Me.pnlCardTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardTotal.Controls.Add(Me.lblCardTotalChange)
        Me.pnlCardTotal.Controls.Add(Me.pnlCardTotalIcon)
        Me.pnlCardTotal.Controls.Add(Me.pnlCardTotalBorder)
        Me.pnlCardTotal.Controls.Add(Me.lblCardTotalValue)
        Me.pnlCardTotal.Controls.Add(Me.lblCardTotalTitle)
        Me.pnlCardTotal.Location = New System.Drawing.Point(0, 0)
        Me.pnlCardTotal.Name = "pnlCardTotal"
        Me.pnlCardTotal.Size = New System.Drawing.Size(285, 95)
        Me.pnlCardTotal.TabIndex = 0
        '
        'lblCardTotalChange
        '
        Me.lblCardTotalChange.AutoSize = True
        Me.lblCardTotalChange.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblCardTotalChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.lblCardTotalChange.Location = New System.Drawing.Point(71, 74)
        Me.lblCardTotalChange.Name = "lblCardTotalChange"
        Me.lblCardTotalChange.Size = New System.Drawing.Size(127, 19)
        Me.lblCardTotalChange.TabIndex = 4
        Me.lblCardTotalChange.Text = "↑ 12% dari kemarin"
        '
        'pnlCardTotalIcon
        '
        Me.pnlCardTotalIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.pnlCardTotalIcon.Controls.Add(Me.lblCardTotalIcon)
        Me.pnlCardTotalIcon.Location = New System.Drawing.Point(15, 15)
        Me.pnlCardTotalIcon.Name = "pnlCardTotalIcon"
        Me.pnlCardTotalIcon.Size = New System.Drawing.Size(45, 45)
        Me.pnlCardTotalIcon.TabIndex = 3
        '
        'lblCardTotalIcon
        '
        Me.lblCardTotalIcon.BackColor = System.Drawing.Color.Transparent
        Me.lblCardTotalIcon.Font = New System.Drawing.Font("Segoe UI", 22.2!)
        Me.lblCardTotalIcon.Location = New System.Drawing.Point(-21, -4)
        Me.lblCardTotalIcon.Name = "lblCardTotalIcon"
        Me.lblCardTotalIcon.Size = New System.Drawing.Size(83, 51)
        Me.lblCardTotalIcon.TabIndex = 0
        Me.lblCardTotalIcon.Text = " 📊"
        Me.lblCardTotalIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlCardTotalBorder
        '
        Me.pnlCardTotalBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.pnlCardTotalBorder.Location = New System.Drawing.Point(0, 0)
        Me.pnlCardTotalBorder.Name = "pnlCardTotalBorder"
        Me.pnlCardTotalBorder.Size = New System.Drawing.Size(4, 95)
        Me.pnlCardTotalBorder.TabIndex = 2
        '
        'lblCardTotalValue
        '
        Me.lblCardTotalValue.AutoSize = True
        Me.lblCardTotalValue.Font = New System.Drawing.Font("Segoe UI", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardTotalValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblCardTotalValue.Location = New System.Drawing.Point(66, 8)
        Me.lblCardTotalValue.Name = "lblCardTotalValue"
        Me.lblCardTotalValue.Size = New System.Drawing.Size(137, 50)
        Me.lblCardTotalValue.TabIndex = 0
        Me.lblCardTotalValue.Text = "12,458"
        '
        'lblCardTotalTitle
        '
        Me.lblCardTotalTitle.AutoSize = True
        Me.lblCardTotalTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblCardTotalTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblCardTotalTitle.Location = New System.Drawing.Point(71, 55)
        Me.lblCardTotalTitle.Name = "lblCardTotalTitle"
        Me.lblCardTotalTitle.Size = New System.Drawing.Size(123, 20)
        Me.lblCardTotalTitle.TabIndex = 1
        Me.lblCardTotalTitle.Text = "Total Log Hari Ini"
        '
        'pnlCardLogin
        '
        Me.pnlCardLogin.BackColor = System.Drawing.Color.White
        Me.pnlCardLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardLogin.Controls.Add(Me.lblCardLoginChange)
        Me.pnlCardLogin.Controls.Add(Me.pnlCardLoginIcon)
        Me.pnlCardLogin.Controls.Add(Me.pnlCardLoginBorder)
        Me.pnlCardLogin.Controls.Add(Me.lblCardLoginValue)
        Me.pnlCardLogin.Controls.Add(Me.lblCardLoginTitle)
        Me.pnlCardLogin.Location = New System.Drawing.Point(300, 0)
        Me.pnlCardLogin.Name = "pnlCardLogin"
        Me.pnlCardLogin.Size = New System.Drawing.Size(285, 95)
        Me.pnlCardLogin.TabIndex = 1
        '
        'lblCardLoginChange
        '
        Me.lblCardLoginChange.AutoSize = True
        Me.lblCardLoginChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.lblCardLoginChange.Location = New System.Drawing.Point(81, 73)
        Me.lblCardLoginChange.Name = "lblCardLoginChange"
        Me.lblCardLoginChange.Size = New System.Drawing.Size(127, 20)
        Me.lblCardLoginChange.TabIndex = 4
        Me.lblCardLoginChange.Text = "↑ 5% dari kemarin"
        '
        'pnlCardLoginIcon
        '
        Me.pnlCardLoginIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.pnlCardLoginIcon.Controls.Add(Me.lblCardLoginIcon)
        Me.pnlCardLoginIcon.Location = New System.Drawing.Point(15, 15)
        Me.pnlCardLoginIcon.Name = "pnlCardLoginIcon"
        Me.pnlCardLoginIcon.Size = New System.Drawing.Size(45, 45)
        Me.pnlCardLoginIcon.TabIndex = 3
        '
        'lblCardLoginIcon
        '
        Me.lblCardLoginIcon.BackColor = System.Drawing.Color.Transparent
        Me.lblCardLoginIcon.Font = New System.Drawing.Font("Segoe UI", 22.0!)
        Me.lblCardLoginIcon.Location = New System.Drawing.Point(-17, -4)
        Me.lblCardLoginIcon.Name = "lblCardLoginIcon"
        Me.lblCardLoginIcon.Size = New System.Drawing.Size(83, 51)
        Me.lblCardLoginIcon.TabIndex = 0
        Me.lblCardLoginIcon.Text = "🔒"
        Me.lblCardLoginIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlCardLoginBorder
        '
        Me.pnlCardLoginBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.pnlCardLoginBorder.Location = New System.Drawing.Point(0, 0)
        Me.pnlCardLoginBorder.Name = "pnlCardLoginBorder"
        Me.pnlCardLoginBorder.Size = New System.Drawing.Size(4, 95)
        Me.pnlCardLoginBorder.TabIndex = 2
        '
        'lblCardLoginValue
        '
        Me.lblCardLoginValue.AutoSize = True
        Me.lblCardLoginValue.Font = New System.Drawing.Font("Segoe UI", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardLoginValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblCardLoginValue.Location = New System.Drawing.Point(76, 8)
        Me.lblCardLoginValue.Name = "lblCardLoginValue"
        Me.lblCardLoginValue.Size = New System.Drawing.Size(85, 50)
        Me.lblCardLoginValue.TabIndex = 0
        Me.lblCardLoginValue.Text = "248"
        '
        'lblCardLoginTitle
        '
        Me.lblCardLoginTitle.AutoSize = True
        Me.lblCardLoginTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblCardLoginTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblCardLoginTitle.Location = New System.Drawing.Point(81, 55)
        Me.lblCardLoginTitle.Name = "lblCardLoginTitle"
        Me.lblCardLoginTitle.Size = New System.Drawing.Size(99, 20)
        Me.lblCardLoginTitle.TabIndex = 1
        Me.lblCardLoginTitle.Text = "Login/Logout"
        '
        'pnlCardChanges
        '
        Me.pnlCardChanges.BackColor = System.Drawing.Color.White
        Me.pnlCardChanges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardChanges.Controls.Add(Me.lblCardChangesChange)
        Me.pnlCardChanges.Controls.Add(Me.pnlCardChangesIcon)
        Me.pnlCardChanges.Controls.Add(Me.pnlCardChangesBorder)
        Me.pnlCardChanges.Controls.Add(Me.lblCardChangesValue)
        Me.pnlCardChanges.Controls.Add(Me.lblCardChangesTitle)
        Me.pnlCardChanges.Location = New System.Drawing.Point(600, 0)
        Me.pnlCardChanges.Name = "pnlCardChanges"
        Me.pnlCardChanges.Size = New System.Drawing.Size(285, 96)
        Me.pnlCardChanges.TabIndex = 2
        '
        'lblCardChangesChange
        '
        Me.lblCardChangesChange.AutoSize = True
        Me.lblCardChangesChange.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblCardChangesChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblCardChangesChange.Location = New System.Drawing.Point(85, 73)
        Me.lblCardChangesChange.Name = "lblCardChangesChange"
        Me.lblCardChangesChange.Size = New System.Drawing.Size(119, 19)
        Me.lblCardChangesChange.TabIndex = 4
        Me.lblCardChangesChange.Text = "↓ 3% dari kemarin"
        '
        'pnlCardChangesIcon
        '
        Me.pnlCardChangesIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.pnlCardChangesIcon.Controls.Add(Me.lblCardChangesIcon)
        Me.pnlCardChangesIcon.Location = New System.Drawing.Point(15, 15)
        Me.pnlCardChangesIcon.Name = "pnlCardChangesIcon"
        Me.pnlCardChangesIcon.Size = New System.Drawing.Size(45, 45)
        Me.pnlCardChangesIcon.TabIndex = 3
        '
        'lblCardChangesIcon
        '
        Me.lblCardChangesIcon.BackColor = System.Drawing.Color.Transparent
        Me.lblCardChangesIcon.Font = New System.Drawing.Font("Segoe UI", 20.0!)
        Me.lblCardChangesIcon.Location = New System.Drawing.Point(-1, -2)
        Me.lblCardChangesIcon.Name = "lblCardChangesIcon"
        Me.lblCardChangesIcon.Size = New System.Drawing.Size(54, 47)
        Me.lblCardChangesIcon.TabIndex = 0
        Me.lblCardChangesIcon.Text = "✏ "
        Me.lblCardChangesIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlCardChangesBorder
        '
        Me.pnlCardChangesBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.pnlCardChangesBorder.Location = New System.Drawing.Point(0, 0)
        Me.pnlCardChangesBorder.Name = "pnlCardChangesBorder"
        Me.pnlCardChangesBorder.Size = New System.Drawing.Size(4, 95)
        Me.pnlCardChangesBorder.TabIndex = 2
        '
        'lblCardChangesValue
        '
        Me.lblCardChangesValue.AutoSize = True
        Me.lblCardChangesValue.Font = New System.Drawing.Font("Segoe UI", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardChangesValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblCardChangesValue.Location = New System.Drawing.Point(76, 8)
        Me.lblCardChangesValue.Name = "lblCardChangesValue"
        Me.lblCardChangesValue.Size = New System.Drawing.Size(116, 50)
        Me.lblCardChangesValue.TabIndex = 0
        Me.lblCardChangesValue.Text = "1,892"
        '
        'lblCardChangesTitle
        '
        Me.lblCardChangesTitle.AutoSize = True
        Me.lblCardChangesTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblCardChangesTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblCardChangesTitle.Location = New System.Drawing.Point(85, 55)
        Me.lblCardChangesTitle.Name = "lblCardChangesTitle"
        Me.lblCardChangesTitle.Size = New System.Drawing.Size(114, 20)
        Me.lblCardChangesTitle.TabIndex = 1
        Me.lblCardChangesTitle.Text = "Perubahan Data"
        '
        'pnlCardErrors
        '
        Me.pnlCardErrors.BackColor = System.Drawing.Color.White
        Me.pnlCardErrors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardErrors.Controls.Add(Me.lblCardErrorsChange)
        Me.pnlCardErrors.Controls.Add(Me.pnlCardErrorsIcon)
        Me.pnlCardErrors.Controls.Add(Me.pnlCardErrorsBorder)
        Me.pnlCardErrors.Controls.Add(Me.lblCardErrorsValue)
        Me.pnlCardErrors.Controls.Add(Me.lblCardErrorsTitle)
        Me.pnlCardErrors.Location = New System.Drawing.Point(900, 0)
        Me.pnlCardErrors.Name = "pnlCardErrors"
        Me.pnlCardErrors.Size = New System.Drawing.Size(285, 95)
        Me.pnlCardErrors.TabIndex = 3
        '
        'lblCardErrorsChange
        '
        Me.lblCardErrorsChange.AutoSize = True
        Me.lblCardErrorsChange.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblCardErrorsChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.lblCardErrorsChange.Location = New System.Drawing.Point(81, 73)
        Me.lblCardErrorsChange.Name = "lblCardErrorsChange"
        Me.lblCardErrorsChange.Size = New System.Drawing.Size(127, 19)
        Me.lblCardErrorsChange.TabIndex = 4
        Me.lblCardErrorsChange.Text = "↓ 18% dari kemarin"
        '
        'pnlCardErrorsIcon
        '
        Me.pnlCardErrorsIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.pnlCardErrorsIcon.Controls.Add(Me.lblCardErrorsIcon)
        Me.pnlCardErrorsIcon.Location = New System.Drawing.Point(15, 15)
        Me.pnlCardErrorsIcon.Name = "pnlCardErrorsIcon"
        Me.pnlCardErrorsIcon.Size = New System.Drawing.Size(45, 45)
        Me.pnlCardErrorsIcon.TabIndex = 3
        '
        'lblCardErrorsIcon
        '
        Me.lblCardErrorsIcon.BackColor = System.Drawing.Color.Transparent
        Me.lblCardErrorsIcon.Font = New System.Drawing.Font("Segoe UI", 22.0!)
        Me.lblCardErrorsIcon.Location = New System.Drawing.Point(-2, -21)
        Me.lblCardErrorsIcon.Name = "lblCardErrorsIcon"
        Me.lblCardErrorsIcon.Size = New System.Drawing.Size(54, 80)
        Me.lblCardErrorsIcon.TabIndex = 0
        Me.lblCardErrorsIcon.Text = "⚠️"
        Me.lblCardErrorsIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlCardErrorsBorder
        '
        Me.pnlCardErrorsBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.pnlCardErrorsBorder.Location = New System.Drawing.Point(0, 0)
        Me.pnlCardErrorsBorder.Name = "pnlCardErrorsBorder"
        Me.pnlCardErrorsBorder.Size = New System.Drawing.Size(4, 95)
        Me.pnlCardErrorsBorder.TabIndex = 2
        '
        'lblCardErrorsValue
        '
        Me.lblCardErrorsValue.AutoSize = True
        Me.lblCardErrorsValue.Font = New System.Drawing.Font("Segoe UI", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardErrorsValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblCardErrorsValue.Location = New System.Drawing.Point(76, 8)
        Me.lblCardErrorsValue.Name = "lblCardErrorsValue"
        Me.lblCardErrorsValue.Size = New System.Drawing.Size(64, 50)
        Me.lblCardErrorsValue.TabIndex = 0
        Me.lblCardErrorsValue.Text = "23"
        '
        'lblCardErrorsTitle
        '
        Me.lblCardErrorsTitle.AutoSize = True
        Me.lblCardErrorsTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblCardErrorsTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblCardErrorsTitle.Location = New System.Drawing.Point(81, 55)
        Me.lblCardErrorsTitle.Name = "lblCardErrorsTitle"
        Me.lblCardErrorsTitle.Size = New System.Drawing.Size(102, 20)
        Me.lblCardErrorsTitle.TabIndex = 1
        Me.lblCardErrorsTitle.Text = "Error/Warning"
        '
        'pnlFilter
        '
        Me.pnlFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter.Controls.Add(Me.cmbFilterUser)
        Me.pnlFilter.Controls.Add(Me.lblFilterUser)
        Me.pnlFilter.Controls.Add(Me.cmbJenisAktivitas)
        Me.pnlFilter.Controls.Add(Me.lblFilterTitle)
        Me.pnlFilter.Controls.Add(Me.lblTanggalMulai)
        Me.pnlFilter.Controls.Add(Me.dtpTanggalMulai)
        Me.pnlFilter.Controls.Add(Me.lblTanggalAkhir)
        Me.pnlFilter.Controls.Add(Me.dtpTanggalAkhir)
        Me.pnlFilter.Controls.Add(Me.lblJenisAktivitas)
        Me.pnlFilter.Controls.Add(Me.lblPencarian)
        Me.pnlFilter.Controls.Add(Me.txtPencarian)
        Me.pnlFilter.Controls.Add(Me.btnTerapkanFilter)
        Me.pnlFilter.Controls.Add(Me.btnResetFilter)
        Me.pnlFilter.Location = New System.Drawing.Point(20, 205)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(1195, 90)
        Me.pnlFilter.TabIndex = 2
        '
        'cmbFilterUser
        '
        Me.cmbFilterUser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbFilterUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilterUser.FormattingEnabled = True
        Me.cmbFilterUser.Items.AddRange(New Object() {"Semua User", "Admin", "Direktur", "Manager", "Krani"})
        Me.cmbFilterUser.Location = New System.Drawing.Point(494, 54)
        Me.cmbFilterUser.Name = "cmbFilterUser"
        Me.cmbFilterUser.Size = New System.Drawing.Size(166, 28)
        Me.cmbFilterUser.TabIndex = 15
        '
        'lblFilterUser
        '
        Me.lblFilterUser.AutoSize = True
        Me.lblFilterUser.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblFilterUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblFilterUser.Location = New System.Drawing.Point(499, 33)
        Me.lblFilterUser.Name = "lblFilterUser"
        Me.lblFilterUser.Size = New System.Drawing.Size(44, 19)
        Me.lblFilterUser.TabIndex = 14
        Me.lblFilterUser.Text = "User :"
        '
        'cmbJenisAktivitas
        '
        Me.cmbJenisAktivitas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbJenisAktivitas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJenisAktivitas.FormattingEnabled = True
        Me.cmbJenisAktivitas.Items.AddRange(New Object() {"Semua Aktivitas", "", "Login", "", "Logout", "", "Insert Data", "", "Update Data", "", "Delete Data", "", "View Data", "", "Print", "", "Error"})
        Me.cmbJenisAktivitas.Location = New System.Drawing.Point(329, 55)
        Me.cmbJenisAktivitas.Name = "cmbJenisAktivitas"
        Me.cmbJenisAktivitas.Size = New System.Drawing.Size(150, 28)
        Me.cmbJenisAktivitas.TabIndex = 13
        '
        'lblFilterTitle
        '
        Me.lblFilterTitle.AutoSize = True
        Me.lblFilterTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFilterTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.lblFilterTitle.Location = New System.Drawing.Point(3, 10)
        Me.lblFilterTitle.Name = "lblFilterTitle"
        Me.lblFilterTitle.Size = New System.Drawing.Size(185, 23)
        Me.lblFilterTitle.TabIndex = 0
        Me.lblFilterTitle.Text = "  FILTER - PENCARIAN"
        '
        'lblTanggalMulai
        '
        Me.lblTanggalMulai.AutoSize = True
        Me.lblTanggalMulai.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblTanggalMulai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTanggalMulai.Location = New System.Drawing.Point(15, 34)
        Me.lblTanggalMulai.Name = "lblTanggalMulai"
        Me.lblTanggalMulai.Size = New System.Drawing.Size(100, 19)
        Me.lblTanggalMulai.TabIndex = 1
        Me.lblTanggalMulai.Text = "Tanggal Mulai :"
        '
        'dtpTanggalMulai
        '
        Me.dtpTanggalMulai.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.dtpTanggalMulai.CustomFormat = "dd/MM/yyyy"
        Me.dtpTanggalMulai.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpTanggalMulai.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTanggalMulai.Location = New System.Drawing.Point(15, 56)
        Me.dtpTanggalMulai.Name = "dtpTanggalMulai"
        Me.dtpTanggalMulai.Size = New System.Drawing.Size(143, 27)
        Me.dtpTanggalMulai.TabIndex = 2
        '
        'lblTanggalAkhir
        '
        Me.lblTanggalAkhir.AutoSize = True
        Me.lblTanggalAkhir.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblTanggalAkhir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblTanggalAkhir.Location = New System.Drawing.Point(161, 34)
        Me.lblTanggalAkhir.Name = "lblTanggalAkhir"
        Me.lblTanggalAkhir.Size = New System.Drawing.Size(98, 19)
        Me.lblTanggalAkhir.TabIndex = 3
        Me.lblTanggalAkhir.Text = "Tanggal Akhir :"
        '
        'dtpTanggalAkhir
        '
        Me.dtpTanggalAkhir.CustomFormat = "dd/MM/yyyy"
        Me.dtpTanggalAkhir.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpTanggalAkhir.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTanggalAkhir.Location = New System.Drawing.Point(164, 56)
        Me.dtpTanggalAkhir.Name = "dtpTanggalAkhir"
        Me.dtpTanggalAkhir.Size = New System.Drawing.Size(143, 27)
        Me.dtpTanggalAkhir.TabIndex = 4
        '
        'lblJenisAktivitas
        '
        Me.lblJenisAktivitas.AutoSize = True
        Me.lblJenisAktivitas.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblJenisAktivitas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblJenisAktivitas.Location = New System.Drawing.Point(334, 34)
        Me.lblJenisAktivitas.Name = "lblJenisAktivitas"
        Me.lblJenisAktivitas.Size = New System.Drawing.Size(101, 19)
        Me.lblJenisAktivitas.TabIndex = 7
        Me.lblJenisAktivitas.Text = "Jenis Aktivitas :"
        '
        'lblPencarian
        '
        Me.lblPencarian.AutoSize = True
        Me.lblPencarian.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblPencarian.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblPencarian.Location = New System.Drawing.Point(694, 34)
        Me.lblPencarian.Name = "lblPencarian"
        Me.lblPencarian.Size = New System.Drawing.Size(74, 19)
        Me.lblPencarian.TabIndex = 9
        Me.lblPencarian.Text = "Pencarian :"
        '
        'txtPencarian
        '
        Me.txtPencarian.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtPencarian.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPencarian.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPencarian.ForeColor = System.Drawing.Color.White
        Me.txtPencarian.Location = New System.Drawing.Point(689, 54)
        Me.txtPencarian.Name = "txtPencarian"
        Me.txtPencarian.Size = New System.Drawing.Size(215, 27)
        Me.txtPencarian.TabIndex = 10
        Me.txtPencarian.Text = "Cari deskripsi, IP, module..."
        '
        'btnTerapkanFilter
        '
        Me.btnTerapkanFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnTerapkanFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTerapkanFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.btnTerapkanFilter.FlatAppearance.BorderSize = 0
        Me.btnTerapkanFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.btnTerapkanFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.btnTerapkanFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTerapkanFilter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnTerapkanFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.btnTerapkanFilter.Location = New System.Drawing.Point(919, 54)
        Me.btnTerapkanFilter.Name = "btnTerapkanFilter"
        Me.btnTerapkanFilter.Size = New System.Drawing.Size(130, 30)
        Me.btnTerapkanFilter.TabIndex = 11
        Me.btnTerapkanFilter.Text = "  Filter"
        Me.btnTerapkanFilter.UseVisualStyleBackColor = False
        '
        'btnResetFilter
        '
        Me.btnResetFilter.BackColor = System.Drawing.Color.White
        Me.btnResetFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnResetFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnResetFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetFilter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnResetFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.btnResetFilter.Location = New System.Drawing.Point(1054, 54)
        Me.btnResetFilter.Name = "btnResetFilter"
        Me.btnResetFilter.Size = New System.Drawing.Size(130, 30)
        Me.btnResetFilter.TabIndex = 12
        Me.btnResetFilter.Text = "↺ Reset Filter"
        Me.btnResetFilter.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(836, 7)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(107, 32)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "  Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnExportExcel
        '
        Me.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExportExcel.FlatAppearance.BorderSize = 0
        Me.btnExportExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportExcel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnExportExcel.ForeColor = System.Drawing.Color.White
        Me.btnExportExcel.Location = New System.Drawing.Point(949, 7)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(107, 32)
        Me.btnExportExcel.TabIndex = 2
        Me.btnExportExcel.Text = " Excel"
        Me.btnExportExcel.UseVisualStyleBackColor = False
        '
        'lblHeaderSubtitle
        '
        Me.lblHeaderSubtitle.AutoSize = True
        Me.lblHeaderSubtitle.BackColor = System.Drawing.Color.Transparent
        Me.lblHeaderSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblHeaderSubtitle.Location = New System.Drawing.Point(92, 55)
        Me.lblHeaderSubtitle.Name = "lblHeaderSubtitle"
        Me.lblHeaderSubtitle.Size = New System.Drawing.Size(265, 20)
        Me.lblHeaderSubtitle.TabIndex = 1
        Me.lblHeaderSubtitle.Text = "Riwayat aktivitas sistem dan pengguna"
        '
        'pnlHeaderIcon
        '
        Me.pnlHeaderIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.pnlHeaderIcon.Controls.Add(Me.lblHeaderIcon)
        Me.pnlHeaderIcon.Location = New System.Drawing.Point(25, 15)
        Me.pnlHeaderIcon.Name = "pnlHeaderIcon"
        Me.pnlHeaderIcon.Size = New System.Drawing.Size(50, 50)
        Me.pnlHeaderIcon.TabIndex = 2
        '
        'lblHeaderIcon
        '
        Me.lblHeaderIcon.BackColor = System.Drawing.Color.Transparent
        Me.lblHeaderIcon.Font = New System.Drawing.Font("Segoe UI", 27.0!)
        Me.lblHeaderIcon.Location = New System.Drawing.Point(-33, -5)
        Me.lblHeaderIcon.Name = "lblHeaderIcon"
        Me.lblHeaderIcon.Size = New System.Drawing.Size(110, 56)
        Me.lblHeaderIcon.TabIndex = 0
        Me.lblHeaderIcon.Text = " 📋"
        Me.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnHapusLogLama)
        Me.pnlHeader.Controls.Add(Me.btnExportSemua)
        Me.pnlHeader.Controls.Add(Me.lblHeaderTitle)
        Me.pnlHeader.Controls.Add(Me.pnlHeaderIcon)
        Me.pnlHeader.Controls.Add(Me.lblHeaderSubtitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1232, 79)
        Me.pnlHeader.TabIndex = 4
        '
        'btnHapusLogLama
        '
        Me.btnHapusLogLama.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.btnHapusLogLama.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHapusLogLama.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(16, Byte), Integer))
        Me.btnHapusLogLama.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.btnHapusLogLama.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(16, Byte), Integer))
        Me.btnHapusLogLama.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapusLogLama.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHapusLogLama.ForeColor = System.Drawing.Color.White
        Me.btnHapusLogLama.Location = New System.Drawing.Point(1029, 25)
        Me.btnHapusLogLama.Name = "btnHapusLogLama"
        Me.btnHapusLogLama.Size = New System.Drawing.Size(168, 35)
        Me.btnHapusLogLama.TabIndex = 5
        Me.btnHapusLogLama.Text = "   Hapus Log Lama"
        Me.btnHapusLogLama.UseVisualStyleBackColor = False
        '
        'btnExportSemua
        '
        Me.btnExportSemua.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnExportSemua.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExportSemua.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.btnExportSemua.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnExportSemua.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnExportSemua.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportSemua.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnExportSemua.ForeColor = System.Drawing.Color.White
        Me.btnExportSemua.Location = New System.Drawing.Point(903, 25)
        Me.btnExportSemua.Name = "btnExportSemua"
        Me.btnExportSemua.Size = New System.Drawing.Size(120, 35)
        Me.btnExportSemua.TabIndex = 4
        Me.btnExportSemua.Text = "  Export"
        Me.btnExportSemua.UseVisualStyleBackColor = False
        '
        'lblHeaderTitle
        '
        Me.lblHeaderTitle.AutoSize = True
        Me.lblHeaderTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblHeaderTitle.Location = New System.Drawing.Point(90, 18)
        Me.lblHeaderTitle.Name = "lblHeaderTitle"
        Me.lblHeaderTitle.Size = New System.Drawing.Size(143, 37)
        Me.lblHeaderTitle.TabIndex = 3
        Me.lblHeaderTitle.Text = "Audit Log"
        '
        'pnlTableContainer
        '
        Me.pnlTableContainer.BackColor = System.Drawing.Color.White
        Me.pnlTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTableContainer.Controls.Add(Me.pnlPagination)
        Me.pnlTableContainer.Controls.Add(Me.dgvAuditLog)
        Me.pnlTableContainer.Controls.Add(Me.pnlTableHeader)
        Me.pnlTableContainer.Location = New System.Drawing.Point(20, 305)
        Me.pnlTableContainer.Name = "pnlTableContainer"
        Me.pnlTableContainer.Size = New System.Drawing.Size(1195, 355)
        Me.pnlTableContainer.TabIndex = 5
        '
        'pnlPagination
        '
        Me.pnlPagination.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlPagination.Controls.Add(Me.btnNextPage)
        Me.pnlPagination.Controls.Add(Me.btnPage5)
        Me.pnlPagination.Controls.Add(Me.btnPage4)
        Me.pnlPagination.Controls.Add(Me.btnPage3)
        Me.pnlPagination.Controls.Add(Me.btnPage2)
        Me.pnlPagination.Controls.Add(Me.btnPage1)
        Me.pnlPagination.Controls.Add(Me.btnPrevPage)
        Me.pnlPagination.Controls.Add(Me.lblPaginationInfo)
        Me.pnlPagination.Location = New System.Drawing.Point(0, 306)
        Me.pnlPagination.Name = "pnlPagination"
        Me.pnlPagination.Size = New System.Drawing.Size(1193, 48)
        Me.pnlPagination.TabIndex = 2
        '
        'btnNextPage
        '
        Me.btnNextPage.BackColor = System.Drawing.Color.White
        Me.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNextPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.btnNextPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextPage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.btnNextPage.Location = New System.Drawing.Point(1140, 8)
        Me.btnNextPage.Name = "btnNextPage"
        Me.btnNextPage.Size = New System.Drawing.Size(35, 32)
        Me.btnNextPage.TabIndex = 7
        Me.btnNextPage.Text = "▶"
        Me.btnNextPage.UseVisualStyleBackColor = False
        '
        'btnPage5
        '
        Me.btnPage5.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPage5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPage5.Location = New System.Drawing.Point(1100, 8)
        Me.btnPage5.Name = "btnPage5"
        Me.btnPage5.Size = New System.Drawing.Size(35, 32)
        Me.btnPage5.TabIndex = 6
        Me.btnPage5.Text = "5"
        Me.btnPage5.UseVisualStyleBackColor = False
        '
        'btnPage4
        '
        Me.btnPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPage4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPage4.Location = New System.Drawing.Point(1060, 8)
        Me.btnPage4.Name = "btnPage4"
        Me.btnPage4.Size = New System.Drawing.Size(35, 32)
        Me.btnPage4.TabIndex = 5
        Me.btnPage4.Text = "4"
        Me.btnPage4.UseVisualStyleBackColor = False
        '
        'btnPage3
        '
        Me.btnPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPage3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPage3.Location = New System.Drawing.Point(1020, 8)
        Me.btnPage3.Name = "btnPage3"
        Me.btnPage3.Size = New System.Drawing.Size(35, 32)
        Me.btnPage3.TabIndex = 4
        Me.btnPage3.Text = "3"
        Me.btnPage3.UseVisualStyleBackColor = False
        '
        'btnPage2
        '
        Me.btnPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPage2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPage2.Location = New System.Drawing.Point(980, 8)
        Me.btnPage2.Name = "btnPage2"
        Me.btnPage2.Size = New System.Drawing.Size(35, 32)
        Me.btnPage2.TabIndex = 3
        Me.btnPage2.Text = "2"
        Me.btnPage2.UseVisualStyleBackColor = False
        '
        'btnPage1
        '
        Me.btnPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPage1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnPage1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPage1.Location = New System.Drawing.Point(940, 8)
        Me.btnPage1.Name = "btnPage1"
        Me.btnPage1.Size = New System.Drawing.Size(35, 32)
        Me.btnPage1.TabIndex = 2
        Me.btnPage1.Text = "1"
        Me.btnPage1.UseVisualStyleBackColor = False
        '
        'btnPrevPage
        '
        Me.btnPrevPage.BackColor = System.Drawing.Color.White
        Me.btnPrevPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrevPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.btnPrevPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevPage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.btnPrevPage.Location = New System.Drawing.Point(900, 8)
        Me.btnPrevPage.Name = "btnPrevPage"
        Me.btnPrevPage.Size = New System.Drawing.Size(35, 32)
        Me.btnPrevPage.TabIndex = 1
        Me.btnPrevPage.Text = "◀"
        Me.btnPrevPage.UseVisualStyleBackColor = False
        '
        'lblPaginationInfo
        '
        Me.lblPaginationInfo.AutoSize = True
        Me.lblPaginationInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblPaginationInfo.Location = New System.Drawing.Point(15, 15)
        Me.lblPaginationInfo.Name = "lblPaginationInfo"
        Me.lblPaginationInfo.Size = New System.Drawing.Size(244, 20)
        Me.lblPaginationInfo.TabIndex = 0
        Me.lblPaginationInfo.Text = "Menampilkan 1-10 dari 12,458 data"
        '
        'dgvAuditLog
        '
        Me.dgvAuditLog.AllowUserToAddRows = False
        Me.dgvAuditLog.AllowUserToDeleteRows = False
        Me.dgvAuditLog.AllowUserToResizeColumns = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgvAuditLog.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvAuditLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAuditLog.BackgroundColor = System.Drawing.Color.White
        Me.dgvAuditLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvAuditLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(26, Byte), Integer))
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvAuditLog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvAuditLog.ColumnHeadersHeight = 40
        Me.dgvAuditLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNo, Me.colWaktu, Me.colUser, Me.colAktivitas, Me.colModule, Me.colDeskripsi, Me.colIPAddress, Me.colAksi})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAuditLog.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvAuditLog.EnableHeadersVisualStyles = False
        Me.dgvAuditLog.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvAuditLog.Location = New System.Drawing.Point(0, 45)
        Me.dgvAuditLog.MultiSelect = False
        Me.dgvAuditLog.Name = "dgvAuditLog"
        Me.dgvAuditLog.ReadOnly = True
        Me.dgvAuditLog.RowHeadersVisible = False
        Me.dgvAuditLog.RowHeadersWidth = 51
        Me.dgvAuditLog.RowTemplate.Height = 45
        Me.dgvAuditLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAuditLog.Size = New System.Drawing.Size(1193, 260)
        Me.dgvAuditLog.TabIndex = 1
        '
        'colNo
        '
        Me.colNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colNo.HeaderText = "NO"
        Me.colNo.MinimumWidth = 50
        Me.colNo.Name = "colNo"
        Me.colNo.ReadOnly = True
        Me.colNo.Width = 50
        '
        'colWaktu
        '
        Me.colWaktu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colWaktu.DataPropertyName = "CreatedAt"
        Me.colWaktu.HeaderText = "WAKTU"
        Me.colWaktu.MinimumWidth = 130
        Me.colWaktu.Name = "colWaktu"
        Me.colWaktu.ReadOnly = True
        Me.colWaktu.Width = 130
        '
        'colUser
        '
        Me.colUser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colUser.DataPropertyName = "UserName"
        Me.colUser.HeaderText = "USER"
        Me.colUser.MinimumWidth = 150
        Me.colUser.Name = "colUser"
        Me.colUser.ReadOnly = True
        Me.colUser.Width = 150
        '
        'colAktivitas
        '
        Me.colAktivitas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colAktivitas.DataPropertyName = "Action"
        Me.colAktivitas.HeaderText = "AKTIVITAS"
        Me.colAktivitas.MinimumWidth = 100
        Me.colAktivitas.Name = "colAktivitas"
        Me.colAktivitas.ReadOnly = True
        Me.colAktivitas.Width = 125
        '
        'colModule
        '
        Me.colModule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colModule.DataPropertyName = "TableName"
        Me.colModule.HeaderText = "MODULE"
        Me.colModule.MinimumWidth = 100
        Me.colModule.Name = "colModule"
        Me.colModule.ReadOnly = True
        Me.colModule.Width = 125
        '
        'colDeskripsi
        '
        Me.colDeskripsi.DataPropertyName = "Keterangan"
        Me.colDeskripsi.HeaderText = "DESKRIPSI"
        Me.colDeskripsi.MinimumWidth = 300
        Me.colDeskripsi.Name = "colDeskripsi"
        Me.colDeskripsi.ReadOnly = True
        '
        'colIPAddress
        '
        Me.colIPAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colIPAddress.DataPropertyName = "IPAddress"
        Me.colIPAddress.HeaderText = "IP ADDRESS"
        Me.colIPAddress.MinimumWidth = 120
        Me.colIPAddress.Name = "colIPAddress"
        Me.colIPAddress.ReadOnly = True
        Me.colIPAddress.Width = 120
        '
        'colAksi
        '
        Me.colAksi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colAksi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.colAksi.HeaderText = "AKSI"
        Me.colAksi.MinimumWidth = 80
        Me.colAksi.Name = "colAksi"
        Me.colAksi.ReadOnly = True
        Me.colAksi.Text = "Detail"
        Me.colAksi.UseColumnTextForButtonValue = True
        Me.colAksi.Width = 80
        '
        'pnlTableHeader
        '
        Me.pnlTableHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.pnlTableHeader.Controls.Add(Me.btnExportPDF)
        Me.pnlTableHeader.Controls.Add(Me.lblRecordCount)
        Me.pnlTableHeader.Controls.Add(Me.btnExportExcel)
        Me.pnlTableHeader.Controls.Add(Me.btnRefresh)
        Me.pnlTableHeader.Controls.Add(Me.lblTableTitle)
        Me.pnlTableHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlTableHeader.Name = "pnlTableHeader"
        Me.pnlTableHeader.Size = New System.Drawing.Size(1193, 45)
        Me.pnlTableHeader.TabIndex = 0
        '
        'btnExportPDF
        '
        Me.btnExportPDF.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnExportPDF.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExportPDF.FlatAppearance.BorderSize = 0
        Me.btnExportPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(112, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportPDF.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnExportPDF.ForeColor = System.Drawing.Color.White
        Me.btnExportPDF.Location = New System.Drawing.Point(1062, 7)
        Me.btnExportPDF.Name = "btnExportPDF"
        Me.btnExportPDF.Size = New System.Drawing.Size(107, 32)
        Me.btnExportPDF.TabIndex = 3
        Me.btnExportPDF.Text = " PDF"
        Me.btnExportPDF.UseVisualStyleBackColor = False
        '
        'lblRecordCount
        '
        Me.lblRecordCount.AutoSize = True
        Me.lblRecordCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblRecordCount.Location = New System.Drawing.Point(197, 14)
        Me.lblRecordCount.Name = "lblRecordCount"
        Me.lblRecordCount.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.lblRecordCount.Size = New System.Drawing.Size(220, 26)
        Me.lblRecordCount.TabIndex = 1
        Me.lblRecordCount.Text = "Menampilkan 1-10 dari 12,458"
        '
        'lblTableTitle
        '
        Me.lblTableTitle.AutoSize = True
        Me.lblTableTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTableTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTableTitle.ForeColor = System.Drawing.Color.White
        Me.lblTableTitle.Location = New System.Drawing.Point(15, 12)
        Me.lblTableTitle.Name = "lblTableTitle"
        Me.lblTableTitle.Size = New System.Drawing.Size(157, 23)
        Me.lblTableTitle.TabIndex = 0
        Me.lblTableTitle.Text = "  Daftar Audit Log"
        '
        'FormAuditLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1232, 703)
        Me.Controls.Add(Me.pnlTableContainer)
        Me.Controls.Add(Me.pnlFilter)
        Me.Controls.Add(Me.pnlStatsContainer)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormAuditLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Audit Log - Riwayat Aktivitas"
        Me.pnlStatsContainer.ResumeLayout(False)
        Me.pnlCardTotal.ResumeLayout(False)
        Me.pnlCardTotal.PerformLayout()
        Me.pnlCardTotalIcon.ResumeLayout(False)
        Me.pnlCardLogin.ResumeLayout(False)
        Me.pnlCardLogin.PerformLayout()
        Me.pnlCardLoginIcon.ResumeLayout(False)
        Me.pnlCardChanges.ResumeLayout(False)
        Me.pnlCardChanges.PerformLayout()
        Me.pnlCardChangesIcon.ResumeLayout(False)
        Me.pnlCardErrors.ResumeLayout(False)
        Me.pnlCardErrors.PerformLayout()
        Me.pnlCardErrorsIcon.ResumeLayout(False)
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        Me.pnlHeaderIcon.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlTableContainer.ResumeLayout(False)
        Me.pnlPagination.ResumeLayout(False)
        Me.pnlPagination.PerformLayout()
        CType(Me.dgvAuditLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTableHeader.ResumeLayout(False)
        Me.pnlTableHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlStatsContainer As Panel
    Friend WithEvents pnlCardTotal As Panel
    Friend WithEvents lblCardTotalValue As Label
    Friend WithEvents lblCardTotalTitle As Label
    Friend WithEvents pnlCardLogin As Panel
    Friend WithEvents lblCardLoginValue As Label
    Friend WithEvents lblCardLoginTitle As Label
    Friend WithEvents pnlCardChanges As Panel
    Friend WithEvents lblCardChangesValue As Label
    Friend WithEvents lblCardChangesTitle As Label
    Friend WithEvents pnlCardErrors As Panel
    Friend WithEvents lblCardErrorsValue As Label
    Friend WithEvents lblCardErrorsTitle As Label
    Friend WithEvents pnlFilter As Panel
    Friend WithEvents lblFilterTitle As Label
    Friend WithEvents lblTanggalMulai As Label
    Friend WithEvents dtpTanggalMulai As DateTimePicker
    Friend WithEvents lblTanggalAkhir As Label
    Friend WithEvents dtpTanggalAkhir As DateTimePicker
    Friend WithEvents lblJenisAktivitas As Label
    Friend WithEvents lblPencarian As Label
    Friend WithEvents txtPencarian As TextBox
    Friend WithEvents btnTerapkanFilter As Button
    Friend WithEvents btnResetFilter As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnExportExcel As Button
    Friend WithEvents lblHeaderSubtitle As Label
    Friend WithEvents pnlHeaderIcon As Panel
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblHeaderIcon As Label
    Friend WithEvents lblHeaderTitle As Label
    Friend WithEvents btnExportSemua As Button
    Friend WithEvents btnHapusLogLama As Button
    Friend WithEvents pnlCardTotalBorder As Panel
    Friend WithEvents pnlCardTotalIcon As Panel
    Friend WithEvents lblCardTotalIcon As Label
    Friend WithEvents lblCardTotalChange As Label
    Friend WithEvents pnlCardLoginIcon As Panel
    Friend WithEvents pnlCardLoginBorder As Panel
    Friend WithEvents lblCardLoginChange As Label
    Friend WithEvents pnlCardChangesIcon As Panel
    Friend WithEvents lblCardChangesIcon As Label
    Friend WithEvents pnlCardChangesBorder As Panel
    Friend WithEvents lblCardChangesChange As Label
    Friend WithEvents pnlCardErrorsIcon As Panel
    Friend WithEvents pnlCardErrorsBorder As Panel
    Friend WithEvents lblCardErrorsChange As Label
    Friend WithEvents cmbJenisAktivitas As ComboBox
    Friend WithEvents cmbFilterUser As ComboBox
    Friend WithEvents lblFilterUser As Label
    Friend WithEvents pnlTableContainer As Panel
    Friend WithEvents pnlTableHeader As Panel
    Friend WithEvents lblRecordCount As Label
    Friend WithEvents lblTableTitle As Label
    Friend WithEvents dgvAuditLog As DataGridView
    Friend WithEvents btnExportPDF As Button
    Friend WithEvents pnlPagination As Panel
    Friend WithEvents lblPaginationInfo As Label
    Friend WithEvents colNo As DataGridViewTextBoxColumn
    Friend WithEvents colWaktu As DataGridViewTextBoxColumn
    Friend WithEvents colUser As DataGridViewTextBoxColumn
    Friend WithEvents colAktivitas As DataGridViewTextBoxColumn
    Friend WithEvents colModule As DataGridViewTextBoxColumn
    Friend WithEvents colDeskripsi As DataGridViewTextBoxColumn
    Friend WithEvents colIPAddress As DataGridViewTextBoxColumn
    Friend WithEvents colAksi As DataGridViewButtonColumn
    Friend WithEvents btnPage2 As Button
    Friend WithEvents btnPage1 As Button
    Friend WithEvents btnPrevPage As Button
    Friend WithEvents btnPage5 As Button
    Friend WithEvents btnPage4 As Button
    Friend WithEvents btnPage3 As Button
    Friend WithEvents btnNextPage As Button
    Friend WithEvents lblCardLoginIcon As Label
    Friend WithEvents lblCardErrorsIcon As Label
End Class