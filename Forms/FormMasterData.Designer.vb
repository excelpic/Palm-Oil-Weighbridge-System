<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMasterData
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabCustomer = New System.Windows.Forms.TabPage()
        Me.PanelCustomerFooter = New System.Windows.Forms.Panel()
        Me.lblPageInfoCustomer = New System.Windows.Forms.Label()
        Me.btnNextCustomer = New System.Windows.Forms.Button()
        Me.btnPrevCustomer = New System.Windows.Forms.Button()
        Me.btnHapusCustomer = New System.Windows.Forms.Button()
        Me.btnEditCustomer = New System.Windows.Forms.Button()
        Me.dgvCustomer = New System.Windows.Forms.DataGridView()
        Me.PanelCustomerHeader = New System.Windows.Forms.Panel()
        Me.btnRefreshCustomer = New System.Windows.Forms.Button()
        Me.btnTambahCustomer = New System.Windows.Forms.Button()
        Me.btnCariCustomer = New System.Windows.Forms.Button()
        Me.txtCariCustomer = New System.Windows.Forms.TextBox()
        Me.lblCustomerTitle = New System.Windows.Forms.Label()
        Me.tabTransporter = New System.Windows.Forms.TabPage()
        Me.PanelTransporterFooter = New System.Windows.Forms.Panel()
        Me.lblPageInfoTransporter = New System.Windows.Forms.Label()
        Me.btnNextTransporter = New System.Windows.Forms.Button()
        Me.btnPrevTransporter = New System.Windows.Forms.Button()
        Me.btnHapusTransporter = New System.Windows.Forms.Button()
        Me.btnEditTransporter = New System.Windows.Forms.Button()
        Me.dgvTransporter = New System.Windows.Forms.DataGridView()
        Me.PanelTransporterHeader = New System.Windows.Forms.Panel()
        Me.btnRefreshTransporter = New System.Windows.Forms.Button()
        Me.btnTambahTransporter = New System.Windows.Forms.Button()
        Me.btnCariTransporter = New System.Windows.Forms.Button()
        Me.txtCariTransporter = New System.Windows.Forms.TextBox()
        Me.lblTransporterTitle = New System.Windows.Forms.Label()
        Me.tabProduct = New System.Windows.Forms.TabPage()
        Me.PanelProductFooter = New System.Windows.Forms.Panel()
        Me.btnHapusProduct = New System.Windows.Forms.Button()
        Me.btnEditProduct = New System.Windows.Forms.Button()
        Me.dgvProduct = New System.Windows.Forms.DataGridView()
        Me.PanelProductHeader = New System.Windows.Forms.Panel()
        Me.btnRefreshProduct = New System.Windows.Forms.Button()
        Me.btnTambahProduct = New System.Windows.Forms.Button()
        Me.btnCariProduct = New System.Windows.Forms.Button()
        Me.txtCariProduct = New System.Windows.Forms.TextBox()
        Me.lblProductTitle = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.tabCustomer.SuspendLayout()
        Me.PanelCustomerFooter.SuspendLayout()
        CType(Me.dgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelCustomerHeader.SuspendLayout()
        Me.tabTransporter.SuspendLayout()
        Me.PanelTransporterFooter.SuspendLayout()
        CType(Me.dgvTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTransporterHeader.SuspendLayout()
        Me.tabProduct.SuspendLayout()
        Me.PanelProductFooter.SuspendLayout()
        CType(Me.dgvProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelProductHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabCustomer)
        Me.TabControl1.Controls.Add(Me.tabTransporter)
        Me.TabControl1.Controls.Add(Me.tabProduct)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1082, 653)
        Me.TabControl1.TabIndex = 0
        '
        'tabCustomer
        '
        Me.tabCustomer.Controls.Add(Me.PanelCustomerFooter)
        Me.tabCustomer.Controls.Add(Me.dgvCustomer)
        Me.tabCustomer.Controls.Add(Me.PanelCustomerHeader)
        Me.tabCustomer.Location = New System.Drawing.Point(4, 32)
        Me.tabCustomer.Name = "tabCustomer"
        Me.tabCustomer.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCustomer.Size = New System.Drawing.Size(1074, 617)
        Me.tabCustomer.TabIndex = 0
        Me.tabCustomer.Text = "📋 CUSTOMER / SUPPLIER"
        Me.tabCustomer.UseVisualStyleBackColor = True
        '
        'PanelCustomerFooter
        '
        Me.PanelCustomerFooter.BackColor = System.Drawing.Color.Olive
        Me.PanelCustomerFooter.Controls.Add(Me.lblPageInfoCustomer)
        Me.PanelCustomerFooter.Controls.Add(Me.btnNextCustomer)
        Me.PanelCustomerFooter.Controls.Add(Me.btnPrevCustomer)
        Me.PanelCustomerFooter.Controls.Add(Me.btnHapusCustomer)
        Me.PanelCustomerFooter.Controls.Add(Me.btnEditCustomer)
        Me.PanelCustomerFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelCustomerFooter.Location = New System.Drawing.Point(3, 544)
        Me.PanelCustomerFooter.Name = "PanelCustomerFooter"
        Me.PanelCustomerFooter.Size = New System.Drawing.Size(1068, 70)
        Me.PanelCustomerFooter.TabIndex = 2
        '
        'lblPageInfoCustomer
        '
        Me.lblPageInfoCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPageInfoCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPageInfoCustomer.ForeColor = System.Drawing.Color.White
        Me.lblPageInfoCustomer.Location = New System.Drawing.Point(700, 20)
        Me.lblPageInfoCustomer.Name = "lblPageInfoCustomer"
        Me.lblPageInfoCustomer.Size = New System.Drawing.Size(150, 25)
        Me.lblPageInfoCustomer.TabIndex = 4
        Me.lblPageInfoCustomer.Text = "Page 1 of 1"
        Me.lblPageInfoCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNextCustomer
        '
        Me.btnNextCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextCustomer.BackColor = System.Drawing.Color.White
        Me.btnNextCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNextCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNextCustomer.ForeColor = System.Drawing.Color.Black
        Me.btnNextCustomer.Location = New System.Drawing.Point(950, 15)
        Me.btnNextCustomer.Name = "btnNextCustomer"
        Me.btnNextCustomer.Size = New System.Drawing.Size(100, 33)
        Me.btnNextCustomer.TabIndex = 3
        Me.btnNextCustomer.Text = "NEXT ▶"
        Me.btnNextCustomer.UseVisualStyleBackColor = False
        '
        'btnPrevCustomer
        '
        Me.btnPrevCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevCustomer.BackColor = System.Drawing.Color.White
        Me.btnPrevCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrevCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrevCustomer.ForeColor = System.Drawing.Color.Black
        Me.btnPrevCustomer.Location = New System.Drawing.Point(856, 15)
        Me.btnPrevCustomer.Name = "btnPrevCustomer"
        Me.btnPrevCustomer.Size = New System.Drawing.Size(88, 33)
        Me.btnPrevCustomer.TabIndex = 2
        Me.btnPrevCustomer.Text = "◀ PREV"
        Me.btnPrevCustomer.UseVisualStyleBackColor = False
        '
        'btnHapusCustomer
        '
        Me.btnHapusCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnHapusCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHapusCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapusCustomer.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHapusCustomer.ForeColor = System.Drawing.Color.White
        Me.btnHapusCustomer.Location = New System.Drawing.Point(150, 15)
        Me.btnHapusCustomer.Name = "btnHapusCustomer"
        Me.btnHapusCustomer.Size = New System.Drawing.Size(116, 33)
        Me.btnHapusCustomer.TabIndex = 1
        Me.btnHapusCustomer.Text = "🗑 HAPUS"
        Me.btnHapusCustomer.UseVisualStyleBackColor = False
        '
        'btnEditCustomer
        '
        Me.btnEditCustomer.BackColor = System.Drawing.Color.FloralWhite
        Me.btnEditCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEditCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditCustomer.Location = New System.Drawing.Point(20, 15)
        Me.btnEditCustomer.Name = "btnEditCustomer"
        Me.btnEditCustomer.Size = New System.Drawing.Size(116, 33)
        Me.btnEditCustomer.TabIndex = 0
        Me.btnEditCustomer.Text = "✏ EDIT"
        Me.btnEditCustomer.UseVisualStyleBackColor = False
        '
        'dgvCustomer
        '
        Me.dgvCustomer.AllowUserToAddRows = False
        Me.dgvCustomer.AllowUserToDeleteRows = False
        Me.dgvCustomer.AllowUserToOrderColumns = True
        Me.dgvCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCustomer.BackgroundColor = System.Drawing.Color.White
        Me.dgvCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCustomer.Location = New System.Drawing.Point(3, 73)
        Me.dgvCustomer.Name = "dgvCustomer"
        Me.dgvCustomer.RowHeadersWidth = 51
        Me.dgvCustomer.RowTemplate.Height = 30
        Me.dgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCustomer.Size = New System.Drawing.Size(1068, 541)
        Me.dgvCustomer.TabIndex = 1
        '
        'PanelCustomerHeader
        '
        Me.PanelCustomerHeader.BackColor = System.Drawing.Color.DarkGray
        Me.PanelCustomerHeader.Controls.Add(Me.btnRefreshCustomer)
        Me.PanelCustomerHeader.Controls.Add(Me.btnTambahCustomer)
        Me.PanelCustomerHeader.Controls.Add(Me.btnCariCustomer)
        Me.PanelCustomerHeader.Controls.Add(Me.txtCariCustomer)
        Me.PanelCustomerHeader.Controls.Add(Me.lblCustomerTitle)
        Me.PanelCustomerHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelCustomerHeader.Location = New System.Drawing.Point(3, 3)
        Me.PanelCustomerHeader.Name = "PanelCustomerHeader"
        Me.PanelCustomerHeader.Size = New System.Drawing.Size(1068, 70)
        Me.PanelCustomerHeader.TabIndex = 0
        '
        'btnRefreshCustomer
        '
        Me.btnRefreshCustomer.BackColor = System.Drawing.Color.LimeGreen
        Me.btnRefreshCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshCustomer.ForeColor = System.Drawing.Color.Black
        Me.btnRefreshCustomer.Location = New System.Drawing.Point(506, 37)
        Me.btnRefreshCustomer.Name = "btnRefreshCustomer"
        Me.btnRefreshCustomer.Size = New System.Drawing.Size(112, 30)
        Me.btnRefreshCustomer.TabIndex = 4
        Me.btnRefreshCustomer.Text = "🔄 REFRESH"
        Me.btnRefreshCustomer.UseVisualStyleBackColor = False
        '
        'btnTambahCustomer
        '
        Me.btnTambahCustomer.BackColor = System.Drawing.Color.White
        Me.btnTambahCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTambahCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTambahCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTambahCustomer.Location = New System.Drawing.Point(388, 37)
        Me.btnTambahCustomer.Name = "btnTambahCustomer"
        Me.btnTambahCustomer.Size = New System.Drawing.Size(112, 30)
        Me.btnTambahCustomer.TabIndex = 3
        Me.btnTambahCustomer.Text = "➕ TAMBAH BARU"
        Me.btnTambahCustomer.UseVisualStyleBackColor = False
        '
        'btnCariCustomer
        '
        Me.btnCariCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCariCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCariCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCariCustomer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCariCustomer.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCariCustomer.Location = New System.Drawing.Point(286, 37)
        Me.btnCariCustomer.Name = "btnCariCustomer"
        Me.btnCariCustomer.Size = New System.Drawing.Size(96, 30)
        Me.btnCariCustomer.TabIndex = 2
        Me.btnCariCustomer.Text = "🔍 CARI"
        Me.btnCariCustomer.UseVisualStyleBackColor = False
        '
        'txtCariCustomer
        '
        Me.txtCariCustomer.Location = New System.Drawing.Point(20, 37)
        Me.txtCariCustomer.Name = "txtCariCustomer"
        Me.txtCariCustomer.Size = New System.Drawing.Size(250, 30)
        Me.txtCariCustomer.TabIndex = 1
        '
        'lblCustomerTitle
        '
        Me.lblCustomerTitle.AutoSize = True
        Me.lblCustomerTitle.Font = New System.Drawing.Font("Segoe UI", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerTitle.ForeColor = System.Drawing.Color.Black
        Me.lblCustomerTitle.Location = New System.Drawing.Point(15, 0)
        Me.lblCustomerTitle.Name = "lblCustomerTitle"
        Me.lblCustomerTitle.Size = New System.Drawing.Size(315, 25)
        Me.lblCustomerTitle.TabIndex = 0
        Me.lblCustomerTitle.Text = "📋 DAFTAR CUSTOMER / SUPPLIER"
        '
        'tabTransporter
        '
        Me.tabTransporter.Controls.Add(Me.PanelTransporterFooter)
        Me.tabTransporter.Controls.Add(Me.dgvTransporter)
        Me.tabTransporter.Controls.Add(Me.PanelTransporterHeader)
        Me.tabTransporter.Location = New System.Drawing.Point(4, 32)
        Me.tabTransporter.Name = "tabTransporter"
        Me.tabTransporter.Padding = New System.Windows.Forms.Padding(3)
        Me.tabTransporter.Size = New System.Drawing.Size(1074, 617)
        Me.tabTransporter.TabIndex = 1
        Me.tabTransporter.Text = "🚛 TRANSPORTER"
        Me.tabTransporter.UseVisualStyleBackColor = True
        '
        'PanelTransporterFooter
        '
        Me.PanelTransporterFooter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelTransporterFooter.BackColor = System.Drawing.Color.Olive
        Me.PanelTransporterFooter.Controls.Add(Me.lblPageInfoTransporter)
        Me.PanelTransporterFooter.Controls.Add(Me.btnNextTransporter)
        Me.PanelTransporterFooter.Controls.Add(Me.btnPrevTransporter)
        Me.PanelTransporterFooter.Controls.Add(Me.btnHapusTransporter)
        Me.PanelTransporterFooter.Controls.Add(Me.btnEditTransporter)
        Me.PanelTransporterFooter.Location = New System.Drawing.Point(3, 544)
        Me.PanelTransporterFooter.Name = "PanelTransporterFooter"
        Me.PanelTransporterFooter.Size = New System.Drawing.Size(1068, 70)
        Me.PanelTransporterFooter.TabIndex = 6
        '
        'lblPageInfoTransporter
        '
        Me.lblPageInfoTransporter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPageInfoTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPageInfoTransporter.ForeColor = System.Drawing.Color.White
        Me.lblPageInfoTransporter.Location = New System.Drawing.Point(700, 20)
        Me.lblPageInfoTransporter.Name = "lblPageInfoTransporter"
        Me.lblPageInfoTransporter.Size = New System.Drawing.Size(150, 25)
        Me.lblPageInfoTransporter.TabIndex = 4
        Me.lblPageInfoTransporter.Text = "Page 1 of 1"
        Me.lblPageInfoTransporter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNextTransporter
        '
        Me.btnNextTransporter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextTransporter.BackColor = System.Drawing.Color.White
        Me.btnNextTransporter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNextTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNextTransporter.ForeColor = System.Drawing.Color.Black
        Me.btnNextTransporter.Location = New System.Drawing.Point(950, 15)
        Me.btnNextTransporter.Name = "btnNextTransporter"
        Me.btnNextTransporter.Size = New System.Drawing.Size(100, 33)
        Me.btnNextTransporter.TabIndex = 3
        Me.btnNextTransporter.Text = "NEXT ▶"
        Me.btnNextTransporter.UseVisualStyleBackColor = False
        '
        'btnPrevTransporter
        '
        Me.btnPrevTransporter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevTransporter.BackColor = System.Drawing.Color.White
        Me.btnPrevTransporter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrevTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrevTransporter.ForeColor = System.Drawing.Color.Black
        Me.btnPrevTransporter.Location = New System.Drawing.Point(856, 15)
        Me.btnPrevTransporter.Name = "btnPrevTransporter"
        Me.btnPrevTransporter.Size = New System.Drawing.Size(88, 33)
        Me.btnPrevTransporter.TabIndex = 2
        Me.btnPrevTransporter.Text = "◀ PREV"
        Me.btnPrevTransporter.UseVisualStyleBackColor = False
        '
        'btnHapusTransporter
        '
        Me.btnHapusTransporter.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnHapusTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapusTransporter.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHapusTransporter.ForeColor = System.Drawing.Color.White
        Me.btnHapusTransporter.Location = New System.Drawing.Point(150, 15)
        Me.btnHapusTransporter.Name = "btnHapusTransporter"
        Me.btnHapusTransporter.Size = New System.Drawing.Size(116, 33)
        Me.btnHapusTransporter.TabIndex = 1
        Me.btnHapusTransporter.Text = "🗑 HAPUS"
        Me.btnHapusTransporter.UseVisualStyleBackColor = False
        '
        'btnEditTransporter
        '
        Me.btnEditTransporter.BackColor = System.Drawing.Color.White
        Me.btnEditTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditTransporter.ForeColor = System.Drawing.Color.Black
        Me.btnEditTransporter.Location = New System.Drawing.Point(20, 15)
        Me.btnEditTransporter.Name = "btnEditTransporter"
        Me.btnEditTransporter.Size = New System.Drawing.Size(116, 33)
        Me.btnEditTransporter.TabIndex = 0
        Me.btnEditTransporter.Text = "✏ EDIT"
        Me.btnEditTransporter.UseVisualStyleBackColor = False
        '
        'dgvTransporter
        '
        Me.dgvTransporter.AllowUserToAddRows = False
        Me.dgvTransporter.AllowUserToDeleteRows = False
        Me.dgvTransporter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTransporter.BackgroundColor = System.Drawing.Color.White
        Me.dgvTransporter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTransporter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransporter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTransporter.Location = New System.Drawing.Point(3, 73)
        Me.dgvTransporter.Name = "dgvTransporter"
        Me.dgvTransporter.RowHeadersWidth = 51
        Me.dgvTransporter.RowTemplate.Height = 24
        Me.dgvTransporter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTransporter.Size = New System.Drawing.Size(1068, 541)
        Me.dgvTransporter.TabIndex = 5
        '
        'PanelTransporterHeader
        '
        Me.PanelTransporterHeader.BackColor = System.Drawing.Color.DarkGray
        Me.PanelTransporterHeader.Controls.Add(Me.btnRefreshTransporter)
        Me.PanelTransporterHeader.Controls.Add(Me.btnTambahTransporter)
        Me.PanelTransporterHeader.Controls.Add(Me.btnCariTransporter)
        Me.PanelTransporterHeader.Controls.Add(Me.txtCariTransporter)
        Me.PanelTransporterHeader.Controls.Add(Me.lblTransporterTitle)
        Me.PanelTransporterHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTransporterHeader.Location = New System.Drawing.Point(3, 3)
        Me.PanelTransporterHeader.Name = "PanelTransporterHeader"
        Me.PanelTransporterHeader.Size = New System.Drawing.Size(1068, 70)
        Me.PanelTransporterHeader.TabIndex = 0
        '
        'btnRefreshTransporter
        '
        Me.btnRefreshTransporter.BackColor = System.Drawing.Color.LimeGreen
        Me.btnRefreshTransporter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshTransporter.ForeColor = System.Drawing.Color.Black
        Me.btnRefreshTransporter.Location = New System.Drawing.Point(506, 37)
        Me.btnRefreshTransporter.Name = "btnRefreshTransporter"
        Me.btnRefreshTransporter.Size = New System.Drawing.Size(112, 30)
        Me.btnRefreshTransporter.TabIndex = 4
        Me.btnRefreshTransporter.Text = "🔄 REFRESH"
        Me.btnRefreshTransporter.UseVisualStyleBackColor = False
        '
        'btnTambahTransporter
        '
        Me.btnTambahTransporter.BackColor = System.Drawing.Color.White
        Me.btnTambahTransporter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTambahTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTambahTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTambahTransporter.ForeColor = System.Drawing.Color.Black
        Me.btnTambahTransporter.Location = New System.Drawing.Point(388, 37)
        Me.btnTambahTransporter.Name = "btnTambahTransporter"
        Me.btnTambahTransporter.Size = New System.Drawing.Size(112, 30)
        Me.btnTambahTransporter.TabIndex = 3
        Me.btnTambahTransporter.Text = "➕ TAMBAH"
        Me.btnTambahTransporter.UseVisualStyleBackColor = False
        '
        'btnCariTransporter
        '
        Me.btnCariTransporter.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCariTransporter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCariTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCariTransporter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCariTransporter.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCariTransporter.Location = New System.Drawing.Point(286, 37)
        Me.btnCariTransporter.Name = "btnCariTransporter"
        Me.btnCariTransporter.Size = New System.Drawing.Size(96, 30)
        Me.btnCariTransporter.TabIndex = 2
        Me.btnCariTransporter.Text = "" & Global.Microsoft.VisualBasic.ChrW(9) & "🔍 CARI"
        Me.btnCariTransporter.UseVisualStyleBackColor = False
        '
        'txtCariTransporter
        '
        Me.txtCariTransporter.Location = New System.Drawing.Point(20, 37)
        Me.txtCariTransporter.Name = "txtCariTransporter"
        Me.txtCariTransporter.Size = New System.Drawing.Size(250, 30)
        Me.txtCariTransporter.TabIndex = 1
        '
        'lblTransporterTitle
        '
        Me.lblTransporterTitle.AutoSize = True
        Me.lblTransporterTitle.Font = New System.Drawing.Font("Segoe UI", 10.8!, System.Drawing.FontStyle.Bold)
        Me.lblTransporterTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTransporterTitle.Location = New System.Drawing.Point(15, 0)
        Me.lblTransporterTitle.Name = "lblTransporterTitle"
        Me.lblTransporterTitle.Size = New System.Drawing.Size(248, 25)
        Me.lblTransporterTitle.TabIndex = 0
        Me.lblTransporterTitle.Text = "🚛 DAFTAR TRANSPORTER"
        '
        'tabProduct
        '
        Me.tabProduct.Controls.Add(Me.PanelProductFooter)
        Me.tabProduct.Controls.Add(Me.dgvProduct)
        Me.tabProduct.Controls.Add(Me.PanelProductHeader)
        Me.tabProduct.Location = New System.Drawing.Point(4, 32)
        Me.tabProduct.Name = "tabProduct"
        Me.tabProduct.Padding = New System.Windows.Forms.Padding(3)
        Me.tabProduct.Size = New System.Drawing.Size(1074, 617)
        Me.tabProduct.TabIndex = 2
        Me.tabProduct.Text = "📦 PRODUCT"
        Me.tabProduct.UseVisualStyleBackColor = True
        '
        'PanelProductFooter
        '
        Me.PanelProductFooter.BackColor = System.Drawing.Color.Olive
        Me.PanelProductFooter.Controls.Add(Me.btnHapusProduct)
        Me.PanelProductFooter.Controls.Add(Me.btnEditProduct)
        Me.PanelProductFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelProductFooter.Location = New System.Drawing.Point(3, 544)
        Me.PanelProductFooter.Name = "PanelProductFooter"
        Me.PanelProductFooter.Size = New System.Drawing.Size(1068, 70)
        Me.PanelProductFooter.TabIndex = 2
        '
        'btnHapusProduct
        '
        Me.btnHapusProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnHapusProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapusProduct.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHapusProduct.ForeColor = System.Drawing.Color.White
        Me.btnHapusProduct.Location = New System.Drawing.Point(150, 15)
        Me.btnHapusProduct.Name = "btnHapusProduct"
        Me.btnHapusProduct.Size = New System.Drawing.Size(116, 33)
        Me.btnHapusProduct.TabIndex = 1
        Me.btnHapusProduct.Text = "🗑 HAPUS"
        Me.btnHapusProduct.UseVisualStyleBackColor = False
        '
        'btnEditProduct
        '
        Me.btnEditProduct.BackColor = System.Drawing.Color.White
        Me.btnEditProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditProduct.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditProduct.ForeColor = System.Drawing.Color.Black
        Me.btnEditProduct.Location = New System.Drawing.Point(20, 15)
        Me.btnEditProduct.Name = "btnEditProduct"
        Me.btnEditProduct.Size = New System.Drawing.Size(116, 33)
        Me.btnEditProduct.TabIndex = 0
        Me.btnEditProduct.Text = "✏ EDIT"
        Me.btnEditProduct.UseVisualStyleBackColor = False
        '
        'dgvProduct
        '
        Me.dgvProduct.AllowUserToAddRows = False
        Me.dgvProduct.AllowUserToDeleteRows = False
        Me.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvProduct.BackgroundColor = System.Drawing.Color.White
        Me.dgvProduct.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProduct.Location = New System.Drawing.Point(3, 73)
        Me.dgvProduct.Name = "dgvProduct"
        Me.dgvProduct.RowHeadersWidth = 51
        Me.dgvProduct.RowTemplate.Height = 24
        Me.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProduct.Size = New System.Drawing.Size(1068, 541)
        Me.dgvProduct.TabIndex = 1
        '
        'PanelProductHeader
        '
        Me.PanelProductHeader.BackColor = System.Drawing.Color.DarkGray
        Me.PanelProductHeader.Controls.Add(Me.btnRefreshProduct)
        Me.PanelProductHeader.Controls.Add(Me.btnTambahProduct)
        Me.PanelProductHeader.Controls.Add(Me.btnCariProduct)
        Me.PanelProductHeader.Controls.Add(Me.txtCariProduct)
        Me.PanelProductHeader.Controls.Add(Me.lblProductTitle)
        Me.PanelProductHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelProductHeader.Location = New System.Drawing.Point(3, 3)
        Me.PanelProductHeader.Name = "PanelProductHeader"
        Me.PanelProductHeader.Size = New System.Drawing.Size(1068, 70)
        Me.PanelProductHeader.TabIndex = 0
        '
        'btnRefreshProduct
        '
        Me.btnRefreshProduct.BackColor = System.Drawing.Color.LimeGreen
        Me.btnRefreshProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshProduct.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshProduct.ForeColor = System.Drawing.Color.Black
        Me.btnRefreshProduct.Location = New System.Drawing.Point(506, 37)
        Me.btnRefreshProduct.Name = "btnRefreshProduct"
        Me.btnRefreshProduct.Size = New System.Drawing.Size(112, 30)
        Me.btnRefreshProduct.TabIndex = 4
        Me.btnRefreshProduct.Text = "🔄 REFRESH"
        Me.btnRefreshProduct.UseVisualStyleBackColor = False
        '
        'btnTambahProduct
        '
        Me.btnTambahProduct.BackColor = System.Drawing.Color.White
        Me.btnTambahProduct.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTambahProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTambahProduct.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTambahProduct.ForeColor = System.Drawing.Color.Black
        Me.btnTambahProduct.Location = New System.Drawing.Point(388, 37)
        Me.btnTambahProduct.Name = "btnTambahProduct"
        Me.btnTambahProduct.Size = New System.Drawing.Size(112, 30)
        Me.btnTambahProduct.TabIndex = 3
        Me.btnTambahProduct.Text = "➕ TAMBAH"
        Me.btnTambahProduct.UseVisualStyleBackColor = False
        '
        'btnCariProduct
        '
        Me.btnCariProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCariProduct.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCariProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCariProduct.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCariProduct.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCariProduct.Location = New System.Drawing.Point(286, 37)
        Me.btnCariProduct.Name = "btnCariProduct"
        Me.btnCariProduct.Size = New System.Drawing.Size(96, 30)
        Me.btnCariProduct.TabIndex = 2
        Me.btnCariProduct.Text = "🔍 CARI"
        Me.btnCariProduct.UseVisualStyleBackColor = False
        '
        'txtCariProduct
        '
        Me.txtCariProduct.Location = New System.Drawing.Point(20, 37)
        Me.txtCariProduct.Name = "txtCariProduct"
        Me.txtCariProduct.Size = New System.Drawing.Size(250, 30)
        Me.txtCariProduct.TabIndex = 1
        '
        'lblProductTitle
        '
        Me.lblProductTitle.AutoSize = True
        Me.lblProductTitle.Font = New System.Drawing.Font("Segoe UI", 10.8!, System.Drawing.FontStyle.Bold)
        Me.lblProductTitle.ForeColor = System.Drawing.Color.Black
        Me.lblProductTitle.Location = New System.Drawing.Point(15, 0)
        Me.lblProductTitle.Name = "lblProductTitle"
        Me.lblProductTitle.Size = New System.Drawing.Size(203, 25)
        Me.lblProductTitle.TabIndex = 0
        Me.lblProductTitle.Text = "📦 DAFTAR PRODUCT"
        '
        'FormMasterData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Sienna
        Me.ClientSize = New System.Drawing.Size(1082, 653)
        Me.Controls.Add(Me.TabControl1)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "FormMasterData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Data"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.tabCustomer.ResumeLayout(False)
        Me.PanelCustomerFooter.ResumeLayout(False)
        CType(Me.dgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelCustomerHeader.ResumeLayout(False)
        Me.PanelCustomerHeader.PerformLayout()
        Me.tabTransporter.ResumeLayout(False)
        Me.PanelTransporterFooter.ResumeLayout(False)
        CType(Me.dgvTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTransporterHeader.ResumeLayout(False)
        Me.PanelTransporterHeader.PerformLayout()
        Me.tabProduct.ResumeLayout(False)
        Me.PanelProductFooter.ResumeLayout(False)
        CType(Me.dgvProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelProductHeader.ResumeLayout(False)
        Me.PanelProductHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tabCustomer As TabPage
    Friend WithEvents tabTransporter As TabPage
    Friend WithEvents tabProduct As TabPage
    Friend WithEvents PanelCustomerHeader As Panel
    Friend WithEvents txtCariCustomer As TextBox
    Friend WithEvents lblCustomerTitle As Label
    Friend WithEvents btnCariCustomer As Button
    Friend WithEvents btnRefreshCustomer As Button
    Friend WithEvents btnTambahCustomer As Button
    Friend WithEvents dgvCustomer As DataGridView
    Friend WithEvents PanelCustomerFooter As Panel
    Friend WithEvents btnEditCustomer As Button
    Friend WithEvents btnHapusCustomer As Button
    Friend WithEvents btnPrevCustomer As Button
    Friend WithEvents btnNextCustomer As Button
    Friend WithEvents lblPageInfoCustomer As Label
    Friend WithEvents PanelTransporterHeader As Panel
    Friend WithEvents lblTransporterTitle As Label
    Friend WithEvents btnCariTransporter As Button
    Friend WithEvents txtCariTransporter As TextBox
    Friend WithEvents btnRefreshTransporter As Button
    Friend WithEvents btnTambahTransporter As Button
    Friend WithEvents dgvTransporter As DataGridView
    Friend WithEvents PanelTransporterFooter As Panel
    Friend WithEvents btnHapusTransporter As Button
    Friend WithEvents btnEditTransporter As Button
    Friend WithEvents btnPrevTransporter As Button
    Friend WithEvents btnNextTransporter As Button
    Friend WithEvents lblPageInfoTransporter As Label
    Friend WithEvents PanelProductHeader As Panel
    Friend WithEvents lblProductTitle As Label
    Friend WithEvents txtCariProduct As TextBox
    Friend WithEvents btnCariProduct As Button
    Friend WithEvents btnTambahProduct As Button
    Friend WithEvents btnRefreshProduct As Button
    Friend WithEvents PanelProductFooter As Panel
    Friend WithEvents dgvProduct As DataGridView
    Friend WithEvents btnHapusProduct As Button
    Friend WithEvents btnEditProduct As Button
End Class