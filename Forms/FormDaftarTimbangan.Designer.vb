<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormDaftarTimbangan
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
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.txtCariNoPolisi = New System.Windows.Forms.TextBox()
        Me.lblNoPolisi = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.dtpTanggalSampai = New System.Windows.Forms.DateTimePicker()
        Me.lblSampai = New System.Windows.Forms.Label()
        Me.dtpTanggalDari = New System.Windows.Forms.DateTimePicker()
        Me.lblTanggalDari = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.dgvTimbangan = New System.Windows.Forms.DataGridView()
        Me.PanelPagination = New System.Windows.Forms.Panel()   ' ← BARU
        Me.btnFirstPage = New System.Windows.Forms.Button()     ' ← BARU
        Me.btnPrevPage = New System.Windows.Forms.Button()      ' ← BARU
        Me.lblPageInfo = New System.Windows.Forms.Label()       ' ← BARU
        Me.btnNextPage = New System.Windows.Forms.Button()      ' ← BARU
        Me.btnLastPage = New System.Windows.Forms.Button()      ' ← BARU
        Me.lblPageSize = New System.Windows.Forms.Label()       ' ← BARU
        Me.cmbPageSize = New System.Windows.Forms.ComboBox()    ' ← BARU
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.btnTutup = New System.Windows.Forms.Button()
        Me.btnHapus = New System.Windows.Forms.Button()
        Me.btnCetakUlang = New System.Windows.Forms.Button()
        Me.btnDetail = New System.Windows.Forms.Button()
        Me.PanelHeader.SuspendLayout()
        CType(Me.dgvTimbangan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPagination.SuspendLayout()
        Me.PanelFooter.SuspendLayout()
        Me.SuspendLayout()

        ' =============================================
        ' PanelHeader
        ' =============================================
        Me.PanelHeader.BackColor = System.Drawing.Color.White
        Me.PanelHeader.Controls.Add(Me.lblInfo)
        Me.PanelHeader.Controls.Add(Me.btnExportExcel)
        Me.PanelHeader.Controls.Add(Me.btnRefresh)
        Me.PanelHeader.Controls.Add(Me.btnFilter)
        Me.PanelHeader.Controls.Add(Me.txtCariNoPolisi)
        Me.PanelHeader.Controls.Add(Me.lblNoPolisi)
        Me.PanelHeader.Controls.Add(Me.cmbStatus)
        Me.PanelHeader.Controls.Add(Me.lblStatus)
        Me.PanelHeader.Controls.Add(Me.dtpTanggalSampai)
        Me.PanelHeader.Controls.Add(Me.lblSampai)
        Me.PanelHeader.Controls.Add(Me.dtpTanggalDari)
        Me.PanelHeader.Controls.Add(Me.lblTanggalDari)
        Me.PanelHeader.Controls.Add(Me.lblTitle)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(1442, 172)
        Me.PanelHeader.TabIndex = 0

        ' lblInfo
        Me.lblInfo.AutoSize = True
        Me.lblInfo.BackColor = System.Drawing.Color.MistyRose
        Me.lblInfo.Location = New System.Drawing.Point(24, 134)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(135, 23)
        Me.lblInfo.TabIndex = 12
        Me.lblInfo.Text = "Total: 0 transaksi"

        ' btnExportExcel
        Me.btnExportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportExcel.BackColor = System.Drawing.Color.LimeGreen
        Me.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportExcel.Location = New System.Drawing.Point(1271, 73)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(120, 37)
        Me.btnExportExcel.TabIndex = 11
        Me.btnExportExcel.Text = "📊 EXPORT"
        Me.btnExportExcel.UseVisualStyleBackColor = False

        ' btnRefresh
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(1128, 110)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(113, 35)
        Me.btnRefresh.TabIndex = 10
        Me.btnRefresh.Text = "🔄 REFRESH"
        Me.btnRefresh.UseVisualStyleBackColor = False

        ' btnFilter
        Me.btnFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFilter.BackColor = System.Drawing.Color.SeaShell
        Me.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFilter.Location = New System.Drawing.Point(1128, 42)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(113, 34)
        Me.btnFilter.TabIndex = 9
        Me.btnFilter.Text = "🔍 FILTER"
        Me.btnFilter.UseVisualStyleBackColor = False

        ' txtCariNoPolisi
        Me.txtCariNoPolisi.Location = New System.Drawing.Point(851, 80)
        Me.txtCariNoPolisi.Name = "txtCariNoPolisi"
        Me.txtCariNoPolisi.Size = New System.Drawing.Size(135, 30)
        Me.txtCariNoPolisi.TabIndex = 8

        ' lblNoPolisi
        Me.lblNoPolisi.AutoSize = True
        Me.lblNoPolisi.Location = New System.Drawing.Point(756, 83)
        Me.lblNoPolisi.Name = "lblNoPolisi"
        Me.lblNoPolisi.Size = New System.Drawing.Size(89, 23)
        Me.lblNoPolisi.TabIndex = 7
        Me.lblNoPolisi.Text = "No. Polisi :"

        ' cmbStatus
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(605, 80)
        Me.cmbStatus.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(134, 31)
        Me.cmbStatus.TabIndex = 6

        ' lblStatus
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStatus.Location = New System.Drawing.Point(534, 83)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(65, 23)
        Me.lblStatus.TabIndex = 5
        Me.lblStatus.Text = "Status :"

        ' dtpTanggalSampai
        Me.dtpTanggalSampai.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTanggalSampai.Location = New System.Drawing.Point(332, 80)
        Me.dtpTanggalSampai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpTanggalSampai.Name = "dtpTanggalSampai"
        Me.dtpTanggalSampai.Size = New System.Drawing.Size(167, 30)
        Me.dtpTanggalSampai.TabIndex = 4

        ' lblSampai
        Me.lblSampai.AutoSize = True
        Me.lblSampai.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSampai.Location = New System.Drawing.Point(292, 82)
        Me.lblSampai.Name = "lblSampai"
        Me.lblSampai.Size = New System.Drawing.Size(34, 23)
        Me.lblSampai.TabIndex = 3
        Me.lblSampai.Text = "s/d"

        ' dtpTanggalDari
        Me.dtpTanggalDari.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTanggalDari.Location = New System.Drawing.Point(117, 80)
        Me.dtpTanggalDari.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpTanggalDari.Name = "dtpTanggalDari"
        Me.dtpTanggalDari.Size = New System.Drawing.Size(168, 30)
        Me.dtpTanggalDari.TabIndex = 2

        ' lblTanggalDari
        Me.lblTanggalDari.AutoSize = True
        Me.lblTanggalDari.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblTanggalDari.Location = New System.Drawing.Point(24, 83)
        Me.lblTanggalDari.Name = "lblTanggalDari"
        Me.lblTanggalDari.Size = New System.Drawing.Size(78, 23)
        Me.lblTanggalDari.TabIndex = 1
        Me.lblTanggalDari.Text = "Tanggal :"

        ' lblTitle
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblTitle.Location = New System.Drawing.Point(22, 22)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(253, 28)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "📋 DAFTAR TIMBANGAN"

        ' =============================================
        ' dgvTimbangan
        ' =============================================
        Me.dgvTimbangan.AllowUserToAddRows = False
        Me.dgvTimbangan.AllowUserToDeleteRows = False
        Me.dgvTimbangan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTimbangan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTimbangan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTimbangan.Location = New System.Drawing.Point(0, 172)
        Me.dgvTimbangan.Name = "dgvTimbangan"
        Me.dgvTimbangan.ReadOnly = True
        Me.dgvTimbangan.RowHeadersWidth = 51
        Me.dgvTimbangan.RowTemplate.Height = 24
        Me.dgvTimbangan.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.dgvTimbangan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTimbangan.Size = New System.Drawing.Size(1442, 769)
        Me.dgvTimbangan.TabIndex = 1

        ' =============================================
        ' PanelPagination ← BARU
        ' =============================================
        Me.PanelPagination.BackColor = System.Drawing.Color.FromArgb(240, 240, 240)
        Me.PanelPagination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelPagination.Controls.Add(Me.btnFirstPage)
        Me.PanelPagination.Controls.Add(Me.btnPrevPage)
        Me.PanelPagination.Controls.Add(Me.lblPageInfo)
        Me.PanelPagination.Controls.Add(Me.btnNextPage)
        Me.PanelPagination.Controls.Add(Me.btnLastPage)
        Me.PanelPagination.Controls.Add(Me.lblPageSize)
        Me.PanelPagination.Controls.Add(Me.cmbPageSize)
        Me.PanelPagination.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelPagination.Location = New System.Drawing.Point(0, 941)
        Me.PanelPagination.Name = "PanelPagination"
        Me.PanelPagination.Size = New System.Drawing.Size(1442, 40)
        Me.PanelPagination.TabIndex = 3

        ' btnFirstPage
        Me.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirstPage.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnFirstPage.Location = New System.Drawing.Point(8, 6)
        Me.btnFirstPage.Name = "btnFirstPage"
        Me.btnFirstPage.Size = New System.Drawing.Size(40, 26)
        Me.btnFirstPage.TabIndex = 0
        Me.btnFirstPage.Text = "⏮"
        Me.btnFirstPage.UseVisualStyleBackColor = True
        Me.btnFirstPage.FlatAppearance.BorderColor = System.Drawing.Color.Silver

        ' btnPrevPage
        Me.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevPage.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnPrevPage.Location = New System.Drawing.Point(54, 6)
        Me.btnPrevPage.Name = "btnPrevPage"
        Me.btnPrevPage.Size = New System.Drawing.Size(40, 26)
        Me.btnPrevPage.TabIndex = 1
        Me.btnPrevPage.Text = "◀"
        Me.btnPrevPage.UseVisualStyleBackColor = True
        Me.btnPrevPage.FlatAppearance.BorderColor = System.Drawing.Color.Silver

        ' lblPageInfo
        Me.lblPageInfo.AutoSize = False
        Me.lblPageInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPageInfo.Location = New System.Drawing.Point(100, 6)
        Me.lblPageInfo.Name = "lblPageInfo"
        Me.lblPageInfo.Size = New System.Drawing.Size(500, 26)
        Me.lblPageInfo.TabIndex = 2
        Me.lblPageInfo.Text = "Halaman 1 dari 1 | Menampilkan 0 dari 0 transaksi"
        Me.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPageInfo.BackColor = System.Drawing.Color.White

        ' btnNextPage
        Me.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextPage.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnNextPage.Location = New System.Drawing.Point(606, 6)
        Me.btnNextPage.Name = "btnNextPage"
        Me.btnNextPage.Size = New System.Drawing.Size(40, 26)
        Me.btnNextPage.TabIndex = 3
        Me.btnNextPage.Text = "▶"
        Me.btnNextPage.UseVisualStyleBackColor = True
        Me.btnNextPage.FlatAppearance.BorderColor = System.Drawing.Color.Silver

        ' btnLastPage
        Me.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLastPage.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnLastPage.Location = New System.Drawing.Point(652, 6)
        Me.btnLastPage.Name = "btnLastPage"
        Me.btnLastPage.Size = New System.Drawing.Size(40, 26)
        Me.btnLastPage.TabIndex = 4
        Me.btnLastPage.Text = "⏭"
        Me.btnLastPage.UseVisualStyleBackColor = True
        Me.btnLastPage.FlatAppearance.BorderColor = System.Drawing.Color.Silver

        ' lblPageSize
        Me.lblPageSize.AutoSize = True
        Me.lblPageSize.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPageSize.Location = New System.Drawing.Point(710, 10)
        Me.lblPageSize.Name = "lblPageSize"
        Me.lblPageSize.Size = New System.Drawing.Size(70, 20)
        Me.lblPageSize.TabIndex = 5
        Me.lblPageSize.Text = "Baris/hal :"

        ' cmbPageSize
        Me.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPageSize.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbPageSize.FormattingEnabled = True
        Me.cmbPageSize.Items.AddRange(New Object() {"10", "20", "50", "100"})
        Me.cmbPageSize.Location = New System.Drawing.Point(786, 7)
        Me.cmbPageSize.Name = "cmbPageSize"
        Me.cmbPageSize.Size = New System.Drawing.Size(65, 25)
        Me.cmbPageSize.TabIndex = 6
        Me.cmbPageSize.SelectedIndex = 1  ' default: 20

        ' =============================================
        ' PanelFooter
        ' =============================================
        Me.PanelFooter.Controls.Add(Me.BtnEdit)
        Me.PanelFooter.Controls.Add(Me.btnTutup)
        Me.PanelFooter.Controls.Add(Me.btnHapus)
        Me.PanelFooter.Controls.Add(Me.btnCetakUlang)
        Me.PanelFooter.Controls.Add(Me.btnDetail)
        Me.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelFooter.Location = New System.Drawing.Point(0, 981)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Size = New System.Drawing.Size(1442, 70)
        Me.PanelFooter.TabIndex = 2

        ' BtnEdit
        Me.BtnEdit.BackColor = System.Drawing.Color.Navy
        Me.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEdit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.ForeColor = System.Drawing.Color.White
        Me.BtnEdit.Location = New System.Drawing.Point(151, 16)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(113, 42)
        Me.BtnEdit.TabIndex = 13
        Me.BtnEdit.Text = "✏️ EDIT"
        Me.BtnEdit.UseVisualStyleBackColor = False

        ' btnTutup
        Me.btnTutup.BackColor = System.Drawing.Color.Red
        Me.btnTutup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTutup.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTutup.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.btnTutup.Location = New System.Drawing.Point(1150, 15)
        Me.btnTutup.Name = "btnTutup"
        Me.btnTutup.Size = New System.Drawing.Size(109, 40)
        Me.btnTutup.TabIndex = 3
        Me.btnTutup.Text = "🚪 TUTUP"
        Me.btnTutup.UseVisualStyleBackColor = False

        ' btnHapus
        Me.btnHapus.BackColor = System.Drawing.Color.Thistle
        Me.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHapus.ForeColor = System.Drawing.Color.Maroon
        Me.btnHapus.Location = New System.Drawing.Point(462, 18)
        Me.btnHapus.Name = "btnHapus"
        Me.btnHapus.Size = New System.Drawing.Size(109, 40)
        Me.btnHapus.TabIndex = 2
        Me.btnHapus.Text = "🗑 HAPUS"
        Me.btnHapus.UseVisualStyleBackColor = False

        ' btnCetakUlang
        Me.btnCetakUlang.BackColor = System.Drawing.Color.GhostWhite
        Me.btnCetakUlang.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCetakUlang.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCetakUlang.Location = New System.Drawing.Point(316, 16)
        Me.btnCetakUlang.Name = "btnCetakUlang"
        Me.btnCetakUlang.Size = New System.Drawing.Size(140, 43)
        Me.btnCetakUlang.TabIndex = 1
        Me.btnCetakUlang.Text = "🖨 CETAK"
        Me.btnCetakUlang.UseVisualStyleBackColor = False

        ' btnDetail
        Me.btnDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDetail.Location = New System.Drawing.Point(20, 15)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(116, 43)
        Me.btnDetail.TabIndex = 0
        Me.btnDetail.Text = "👁 DETAIL"
        Me.btnDetail.UseVisualStyleBackColor = False

        ' =============================================
        ' FormDaftarTimbangan
        ' =============================================
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(1442, 1051)
        Me.Controls.Add(Me.dgvTimbangan)
        Me.Controls.Add(Me.PanelPagination)     ' ← BARU (urutan penting!)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelHeader)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(1235, 914)
        Me.Name = "FormDaftarTimbangan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormDaftarTimbangan"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.dgvTimbangan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPagination.ResumeLayout(False)
        Me.PanelPagination.PerformLayout()
        Me.PanelFooter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelHeader As Panel
    Friend WithEvents dtpTanggalDari As DateTimePicker
    Friend WithEvents lblTanggalDari As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents dtpTanggalSampai As DateTimePicker
    Friend WithEvents lblSampai As Label
    Friend WithEvents lblNoPolisi As Label
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnFilter As Button
    Friend WithEvents txtCariNoPolisi As TextBox
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblInfo As Label
    Friend WithEvents btnExportExcel As Button
    Friend WithEvents dgvTimbangan As DataGridView
    Friend WithEvents PanelPagination As Panel      ' ← BARU
    Friend WithEvents btnFirstPage As Button        ' ← BARU
    Friend WithEvents btnPrevPage As Button         ' ← BARU
    Friend WithEvents lblPageInfo As Label          ' ← BARU
    Friend WithEvents btnNextPage As Button         ' ← BARU
    Friend WithEvents btnLastPage As Button         ' ← BARU
    Friend WithEvents lblPageSize As Label          ' ← BARU
    Friend WithEvents cmbPageSize As ComboBox       ' ← BARU
    Friend WithEvents PanelFooter As Panel
    Friend WithEvents btnDetail As Button
    Friend WithEvents btnHapus As Button
    Friend WithEvents btnCetakUlang As Button
    Friend WithEvents btnTutup As Button
    Friend WithEvents BtnEdit As Button

End Class