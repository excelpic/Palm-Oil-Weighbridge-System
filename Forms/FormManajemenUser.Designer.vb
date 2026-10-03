<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormManajemenUser
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
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnTambah = New System.Windows.Forms.Button()
        Me.btnCari = New System.Windows.Forms.Button()
        Me.txtCari = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.dgvUser = New System.Windows.Forms.DataGridView()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.btnTutup = New System.Windows.Forms.Button()
        Me.btnHapus = New System.Windows.Forms.Button()
        Me.btnResetPassword = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.PanelHeader.SuspendLayout()
        CType(Me.dgvUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.Color.DarkGray
        Me.PanelHeader.Controls.Add(Me.btnRefresh)
        Me.PanelHeader.Controls.Add(Me.btnTambah)
        Me.PanelHeader.Controls.Add(Me.btnCari)
        Me.PanelHeader.Controls.Add(Me.txtCari)
        Me.PanelHeader.Controls.Add(Me.lblTitle)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(882, 70)
        Me.PanelHeader.TabIndex = 0
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.LimeGreen
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.Black
        Me.btnRefresh.Location = New System.Drawing.Point(468, 37)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(112, 30)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "🔄 REFRESH"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnTambah
        '
        Me.btnTambah.BackColor = System.Drawing.Color.White
        Me.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTambah.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTambah.Location = New System.Drawing.Point(350, 37)
        Me.btnTambah.Name = "btnTambah"
        Me.btnTambah.Size = New System.Drawing.Size(112, 30)
        Me.btnTambah.TabIndex = 3
        Me.btnTambah.Text = "➕ TAMBAH"
        Me.btnTambah.UseVisualStyleBackColor = False
        '
        'btnCari
        '
        Me.btnCari.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCari.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCari.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCari.Location = New System.Drawing.Point(264, 37)
        Me.btnCari.Name = "btnCari"
        Me.btnCari.Size = New System.Drawing.Size(80, 30)
        Me.btnCari.TabIndex = 2
        Me.btnCari.Text = "🔍 CARI"
        Me.btnCari.UseVisualStyleBackColor = False
        '
        'txtCari
        '
        Me.txtCari.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCari.Location = New System.Drawing.Point(8, 37)
        Me.txtCari.Name = "txtCari"
        Me.txtCari.Size = New System.Drawing.Size(250, 30)
        Me.txtCari.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(231, 28)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "👥 MANAJEMEN USER"
        '
        'dgvUser
        '
        Me.dgvUser.AllowUserToAddRows = False
        Me.dgvUser.AllowUserToDeleteRows = False
        Me.dgvUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvUser.BackgroundColor = System.Drawing.Color.White
        Me.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUser.ColumnHeadersHeight = 35
        Me.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUser.Location = New System.Drawing.Point(0, 70)
        Me.dgvUser.Name = "dgvUser"
        Me.dgvUser.RowHeadersWidth = 51
        Me.dgvUser.RowTemplate.Height = 30
        Me.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUser.Size = New System.Drawing.Size(882, 483)
        Me.dgvUser.TabIndex = 1
        '
        'PanelFooter
        '
        Me.PanelFooter.BackColor = System.Drawing.Color.White
        Me.PanelFooter.Controls.Add(Me.btnTutup)
        Me.PanelFooter.Controls.Add(Me.btnHapus)
        Me.PanelFooter.Controls.Add(Me.btnResetPassword)
        Me.PanelFooter.Controls.Add(Me.btnEdit)
        Me.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelFooter.Location = New System.Drawing.Point(0, 483)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Size = New System.Drawing.Size(882, 70)
        Me.PanelFooter.TabIndex = 2
        '
        'btnTutup
        '
        Me.btnTutup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTutup.BackColor = System.Drawing.Color.DarkGray
        Me.btnTutup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTutup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTutup.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnTutup.ForeColor = System.Drawing.Color.Black
        Me.btnTutup.Location = New System.Drawing.Point(449, 15)
        Me.btnTutup.Name = "btnTutup"
        Me.btnTutup.Size = New System.Drawing.Size(116, 33)
        Me.btnTutup.TabIndex = 3
        Me.btnTutup.Text = "🚪 TUTUP"
        Me.btnTutup.UseVisualStyleBackColor = False
        '
        'btnHapus
        '
        Me.btnHapus.BackColor = System.Drawing.Color.Red
        Me.btnHapus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnHapus.Location = New System.Drawing.Point(327, 15)
        Me.btnHapus.Name = "btnHapus"
        Me.btnHapus.Size = New System.Drawing.Size(116, 33)
        Me.btnHapus.TabIndex = 2
        Me.btnHapus.Text = "🗑️ HAPUS"
        Me.btnHapus.UseVisualStyleBackColor = False
        '
        'btnResetPassword
        '
        Me.btnResetPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnResetPassword.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnResetPassword.ForeColor = System.Drawing.Color.Black
        Me.btnResetPassword.Location = New System.Drawing.Point(142, 15)
        Me.btnResetPassword.Name = "btnResetPassword"
        Me.btnResetPassword.Size = New System.Drawing.Size(179, 33)
        Me.btnResetPassword.TabIndex = 1
        Me.btnResetPassword.Text = "🔑 RESET PASSWORD"
        Me.btnResetPassword.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnEdit.ForeColor = System.Drawing.Color.White
        Me.btnEdit.Location = New System.Drawing.Point(20, 15)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(116, 33)
        Me.btnEdit.TabIndex = 0
        Me.btnEdit.Text = "✏️ EDIT"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'FormManajemenUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(882, 553)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.dgvUser)
        Me.Controls.Add(Me.PanelHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormManajemenUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manajemen User - Sistem Timbangan PKS"
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.dgvUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFooter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents btnCari As Button
    Friend WithEvents txtCari As TextBox
    Friend WithEvents btnTambah As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvUser As DataGridView
    Friend WithEvents PanelFooter As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnHapus As Button
    Friend WithEvents btnResetPassword As Button
    Friend WithEvents btnTutup As Button
End Class
