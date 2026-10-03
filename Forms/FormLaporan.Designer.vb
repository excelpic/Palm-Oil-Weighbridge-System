<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLaporan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PanelFilter = New System.Windows.Forms.Panel()
        Me.btnTutup = New System.Windows.Forms.Button()
        Me.lblPeriode = New System.Windows.Forms.Label()
        Me.btnExportPDF = New System.Windows.Forms.Button()
        Me.btnTampilkan = New System.Windows.Forms.Button()
        Me.dtpSampai = New System.Windows.Forms.DateTimePicker()
        Me.lblSampai = New System.Windows.Forms.Label()
        Me.dtpDari = New System.Windows.Forms.DateTimePicker()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.PanelFilter.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelFilter
        '
        Me.PanelFilter.Controls.Add(Me.btnTutup)
        Me.PanelFilter.Controls.Add(Me.lblPeriode)
        Me.PanelFilter.Controls.Add(Me.btnExportPDF)
        Me.PanelFilter.Controls.Add(Me.btnTampilkan)
        Me.PanelFilter.Controls.Add(Me.dtpSampai)
        Me.PanelFilter.Controls.Add(Me.lblSampai)
        Me.PanelFilter.Controls.Add(Me.dtpDari)
        Me.PanelFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFilter.Location = New System.Drawing.Point(0, 0)
        Me.PanelFilter.Name = "PanelFilter"
        Me.PanelFilter.Size = New System.Drawing.Size(1282, 80)
        Me.PanelFilter.TabIndex = 1
        '
        'btnTutup
        '
        Me.btnTutup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTutup.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnTutup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTutup.FlatAppearance.BorderSize = 0
        Me.btnTutup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTutup.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTutup.ForeColor = System.Drawing.Color.White
        Me.btnTutup.Location = New System.Drawing.Point(1150, 22)
        Me.btnTutup.Name = "btnTutup"
        Me.btnTutup.Size = New System.Drawing.Size(110, 36)
        Me.btnTutup.TabIndex = 5
        Me.btnTutup.Text = "🚪 TUTUP"
        Me.btnTutup.UseVisualStyleBackColor = False
        '
        'lblPeriode
        '
        Me.lblPeriode.AutoSize = True
        Me.lblPeriode.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriode.ForeColor = System.Drawing.Color.Black
        Me.lblPeriode.Location = New System.Drawing.Point(12, 30)
        Me.lblPeriode.Name = "lblPeriode"
        Me.lblPeriode.Size = New System.Drawing.Size(76, 23)
        Me.lblPeriode.TabIndex = 2
        Me.lblPeriode.Text = "Periode :"
        '
        'btnExportPDF
        '
        Me.btnExportPDF.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExportPDF.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExportPDF.FlatAppearance.BorderSize = 0
        Me.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportPDF.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportPDF.ForeColor = System.Drawing.Color.White
        Me.btnExportPDF.Location = New System.Drawing.Point(624, 21)
        Me.btnExportPDF.Name = "btnExportPDF"
        Me.btnExportPDF.Size = New System.Drawing.Size(130, 36)
        Me.btnExportPDF.TabIndex = 4
        Me.btnExportPDF.Text = "📄 EXPORT PDF"
        Me.btnExportPDF.UseVisualStyleBackColor = False
        '
        'btnTampilkan
        '
        Me.btnTampilkan.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.btnTampilkan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTampilkan.FlatAppearance.BorderSize = 0
        Me.btnTampilkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTampilkan.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTampilkan.ForeColor = System.Drawing.Color.White
        Me.btnTampilkan.Location = New System.Drawing.Point(488, 21)
        Me.btnTampilkan.Name = "btnTampilkan"
        Me.btnTampilkan.Size = New System.Drawing.Size(130, 36)
        Me.btnTampilkan.TabIndex = 3
        Me.btnTampilkan.Text = "TAMPILKAN"
        Me.btnTampilkan.UseVisualStyleBackColor = False
        '
        'dtpSampai
        '
        Me.dtpSampai.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSampai.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSampai.Location = New System.Drawing.Point(299, 27)
        Me.dtpSampai.Name = "dtpSampai"
        Me.dtpSampai.Size = New System.Drawing.Size(156, 30)
        Me.dtpSampai.TabIndex = 3
        '
        'lblSampai
        '
        Me.lblSampai.AutoSize = True
        Me.lblSampai.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSampai.Location = New System.Drawing.Point(250, 30)
        Me.lblSampai.Name = "lblSampai"
        Me.lblSampai.Size = New System.Drawing.Size(43, 23)
        Me.lblSampai.TabIndex = 2
        Me.lblSampai.Text = "s/d :"
        '
        'dtpDari
        '
        Me.dtpDari.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDari.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDari.Location = New System.Drawing.Point(94, 28)
        Me.dtpDari.Name = "dtpDari"
        Me.dtpDari.Size = New System.Drawing.Size(150, 30)
        Me.dtpDari.TabIndex = 1
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 80)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(1282, 673)
        Me.ReportViewer1.TabIndex = 3
        '
        'FormLaporan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1282, 753)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.PanelFilter)
        Me.Name = "FormLaporan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Laporan Rekapitulasi - Sistem Timbangan PKS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelFilter.ResumeLayout(False)
        Me.PanelFilter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelFilter As Panel
    Friend WithEvents lblPeriode As Label
    Friend WithEvents dtpDari As DateTimePicker
    Friend WithEvents dtpSampai As DateTimePicker
    Friend WithEvents lblSampai As Label
    Friend WithEvents btnTampilkan As Button
    Friend WithEvents btnExportPDF As Button
    Friend WithEvents btnTutup As Button
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
