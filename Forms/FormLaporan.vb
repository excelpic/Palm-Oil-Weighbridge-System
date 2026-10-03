' =============================================
' FormLaporan.vb
' DITAMBAH: Filter berdasarkan AllowedTransType
' =============================================

Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class FormLaporan

    Private Sub FormLaporan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Update title dengan info TransType
        Dim transTypeInfo As String = If(UserSession.CanViewAllTransType(), "Semua Data", UserSession.AllowedTransType)
        Me.Text = "Laporan Rekapitulasi Timbangan PKS - [" & transTypeInfo & "]"

        dtpDari.Value = Date.Today
        dtpSampai.Value = Date.Today

        SettingsHelper.RefreshCache()

        Try
            DatabaseHelper.InsertAuditLog(
                UserSession.UserID,
                "OPEN_FORM",
                Nothing,
                "FormLaporan",
                Nothing,
                Nothing,
                Nothing,
                $"User membuka Form Laporan - Filter: {transTypeInfo}"
            )
        Catch ex As Exception
            Debug.WriteLine($"[AuditLog] Error: {ex.Message}")
        End Try
    End Sub

    Private Sub btnTampilkan_Click(sender As Object, e As EventArgs) Handles btnTampilkan.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            If dtpDari.Value.Date > dtpSampai.Value.Date Then
                MessageBox.Show("Tanggal Dari tidak boleh lebih besar dari Tanggal Sampai.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim rdlcPath As String = IO.Path.Combine(Application.StartupPath, "Reports", "RptLaporanHarian.rdlc")
            If Not IO.File.Exists(rdlcPath) Then
                MessageBox.Show("File RDLC tidak ditemukan:" & vbCrLf & rdlcPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' =============================================
            ' QUERY DENGAN FILTER TRANS TYPE
            ' =============================================
            Dim query As String = "SELECT " &
                "T.NoTiket AS NoTicket, " &
                "T.TanggalMasuk AS DateTimeIn, " &
                "T.TanggalKeluar AS DateTimeOut, " &
                "T.TransType AS TrxType, " &
                "T.CustomerNama AS Customer, " &
                "T.TransporterNama AS Transporter, " &
                "T.ProductNama AS Products, " &
                "T.NoPolisi AS Vehicles, " &
                "T.BeratMasuk AS Gross, " &
                "T.BeratKeluar AS Tare, " &
                "T.BeratNetto AS Netto, " &
                "T.TotalPotongan AS Adjust, " &
                "T.BeratBersih AS AdjustedNetto, " &
                "T.NoDO AS Contracts, " &
                "0 AS NoOf, " &
                "T.NoDO AS DONO, " &
                "T.CustomerNama AS Supplier, " &
                "T.Alamat AS Address, " &
                "T.TransType AS TransType, " &
                "T.NamaSupir AS DriverName, " &
                "T.NoSIM AS NoSIM, " &
                "T.NoKontrak AS NoKontrak, " &
                "ISNULL(T.SuhuMinyak, 0) AS SuhuMinyak, " &
                "ISNULL(T.FFA, 0) AS FFA, " &
                "ISNULL(T.Moisture, 0) AS Moist, " &
                "ISNULL(T.Dirt, 0) AS Dirt, " &
                "T.SegelAtas AS SegelAtas, " &
                "T.SegelBawah AS SegelBawah " &
                "FROM Timbangan T " &
                "WHERE CAST(T.TanggalMasuk AS DATE) BETWEEN @tgl1 AND @tgl2 " &
                "AND T.TanggalKeluar IS NOT NULL "

            ' =============================================
            ' FILTER TRANS TYPE BERDASARKAN USER SESSION
            ' =============================================
            query &= UserSession.GetTransTypeFilter("T")

            query &= "ORDER BY T.TanggalMasuk ASC"

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {
                New SqlParameter("@tgl1", dtpDari.Value.Date),
                New SqlParameter("@tgl2", dtpSampai.Value.Date)
            })

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Tidak ada data untuk periode yang dipilih.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ReportViewer1.Reset()
            ReportViewer1.ProcessingMode = ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = rdlcPath
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsRekapTimbangan", dt))

            Dim parameters As New List(Of ReportParameter) From {
                New ReportParameter("TanggalDari", dtpDari.Value.ToString("dd/MM/yyyy")),
                New ReportParameter("TanggalSampai", dtpSampai.Value.ToString("dd/MM/yyyy")),
                New ReportParameter("PrintedBy", If(UserSession.NamaLengkap, "Admin")),
                New ReportParameter("NamaPerusahaan", SettingsHelper.NamaPerusahaan),
                New ReportParameter("AlamatPerusahaan", SettingsHelper.AlamatPerusahaan)
            }

            ReportViewer1.LocalReport.SetParameters(parameters)
            ReportViewer1.RefreshReport()

            Try
                DatabaseHelper.InsertAuditLog(UserSession.UserID, "VIEW_REPORT", Nothing, "Timbangan", Nothing, Nothing, Nothing,
                    $"Lihat laporan - Periode: {dtpDari.Value:dd/MM/yyyy} s/d {dtpSampai.Value:dd/MM/yyyy}, Total: {dt.Rows.Count} record, TransType: {UserSession.AllowedTransType}")
            Catch exAudit As Exception
                Debug.WriteLine("[FormLaporan] Audit log error: " & exAudit.Message)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "ERROR DETAIL", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnExportPDF_Click(sender As Object, e As EventArgs) Handles btnExportPDF.Click
        Try
            If ReportViewer1.LocalReport.DataSources.Count = 0 Then
                MessageBox.Show("Silakan klik TAMPILKAN terlebih dahulu.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using saveDialog As New SaveFileDialog()
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
                saveDialog.FileName = "Laporan_Timbangan_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"

                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Me.Cursor = Cursors.WaitCursor

                    Dim warnings As Warning() = Nothing
                    Dim streamIds As String() = Nothing
                    Dim mimeType As String = Nothing
                    Dim encoding As String = Nothing
                    Dim extension As String = Nothing

                    Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamIds, warnings)

                    System.IO.File.WriteAllBytes(saveDialog.FileName, bytes)
                    Me.Cursor = Cursors.Default

                    Try
                        DatabaseHelper.InsertAuditLog(UserSession.UserID, "EXPORT_PDF", Nothing, "Timbangan", Nothing, Nothing, Nothing,
                            $"Export PDF: {saveDialog.FileName}, TransType: {UserSession.AllowedTransType}")
                    Catch exAudit As Exception
                        Debug.WriteLine("[FormLaporan] PDF audit log error: " & exAudit.Message)
                    End Try

                    If MessageBox.Show("PDF berhasil disimpan! Buka file?", "Export Berhasil", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                        Process.Start(saveDialog.FileName)
                    End If
                End If
            End Using
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Debug.WriteLine("[FormLaporan.ExportPdf] Error: " & ex.ToString())
            MessageBox.Show("Gagal mengekspor laporan ke PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Close()
    End Sub

    Private Sub FormLaporan_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            DatabaseHelper.InsertAuditLog(UserSession.UserID, "CLOSE_FORM", Nothing, "FormLaporan", Nothing, Nothing, Nothing, "User menutup Form Laporan")
        Catch exAudit As Exception
            Debug.WriteLine("[FormLaporan] Close audit log error: " & exAudit.Message)
        End Try
    End Sub
End Class
