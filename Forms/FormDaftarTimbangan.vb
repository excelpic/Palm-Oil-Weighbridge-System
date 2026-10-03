' =============================================
' FormDaftarTimbangan.vb
' Form untuk menampilkan daftar semua timbangan
' DITAMBAH: Filter berdasarkan AllowedTransType
' DITAMBAH: Pagination
' =============================================

Imports System.Data.SqlClient
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

Public Class FormDaftarTimbangan

    Private _selectedTimbangID As Integer = 0

    ' =============================================
    ' PAGINATION VARIABLES
    ' =============================================
    Private _currentPage As Integer = 1
    Private _pageSize As Integer = 20
    Private _totalRows As Integer = 0
    Private _totalPages As Integer = 1
    Private _fullDataTable As DataTable

    ' =============================================
    ' FORM LOAD
    ' =============================================
    Private Sub FormDaftarTimbangan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Update title dengan info TransType
            Dim transTypeInfo As String = If(UserSession.CanViewAllTransType(), "Semua Data", UserSession.AllowedTransType)
            Me.Text = "Daftar Timbangan - " & UserSession.NamaLengkap & " (" & UserSession.Role & ") - [" & transTypeInfo & "]"

            SetupComboBoxStatus()

            dtpTanggalDari.Value = DateTime.Today.AddDays(-30)
            dtpTanggalSampai.Value = DateTime.Today

            SetupDataGridView()
            SetupButtonsByRole()
            SetupPageSizeCombo()     ' ← BARU
            LoadData()

            Debug.WriteLine("========================================")
            Debug.WriteLine($"[FormDaftarTimbangan] User: {UserSession.NamaLengkap}")
            Debug.WriteLine($"[FormDaftarTimbangan] Role: {UserSession.Role}")
            Debug.WriteLine($"[FormDaftarTimbangan] AllowedTransType: {UserSession.AllowedTransType}")
            Debug.WriteLine($"[FormDaftarTimbangan] CanEditTimbangan: {UserSession.CanEditTimbangan()}")
            Debug.WriteLine($"[FormDaftarTimbangan] CanDeleteTimbangan: {UserSession.CanDeleteTimbangan()}")
            Debug.WriteLine("========================================")

            DatabaseHelper.InsertAuditLog(UserSession.UserID, "VIEW_FORM", Nothing,
                "Daftar Timbangan", Nothing, Nothing, Nothing,
                $"User {UserSession.NamaLengkap} ({UserSession.Role}) membuka form Daftar Timbangan - Filter: {transTypeInfo}")

        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.Load] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat form daftar timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' SETUP COMBOBOX STATUS
    ' =============================================
    Private Sub SetupComboBoxStatus()
        Dim dt As New DataTable()
        dt.Columns.Add("Value", GetType(String))
        dt.Columns.Add("Text", GetType(String))

        dt.Rows.Add("SEMUA", "-- Semua Status --")
        dt.Rows.Add("MASUK", "Masuk (Belum Keluar)")
        dt.Rows.Add("SELESAI", "Selesai")
        dt.Rows.Add("BATAL", "Batal")

        cmbStatus.DataSource = dt
        cmbStatus.DisplayMember = "Text"
        cmbStatus.ValueMember = "Value"
        cmbStatus.SelectedIndex = 0
    End Sub

    ' =============================================
    ' SETUP COMBOBOX PAGE SIZE  ← BARU
    ' =============================================
    Private Sub SetupPageSizeCombo()
        RemoveHandler cmbPageSize.SelectedIndexChanged, AddressOf cmbPageSize_SelectedIndexChanged

        cmbPageSize.Items.Clear()
        cmbPageSize.Items.AddRange(New Object() {"Auto", "10", "20", "50", "100"})
        cmbPageSize.SelectedIndex = 0  ' Default: Auto
        _pageSize = CalculatePageSize()

        AddHandler cmbPageSize.SelectedIndexChanged, AddressOf cmbPageSize_SelectedIndexChanged
    End Sub

    ' =============================================
    ' SETUP DATAGRIDVIEW COLUMNS
    ' =============================================
    Private Sub SetupDataGridView()
        dgvTimbangan.Columns.Clear()
        dgvTimbangan.AutoGenerateColumns = False

        ' Kolom TimbangID (hidden)
        Dim colID As New DataGridViewTextBoxColumn()
        colID.Name = "TimbangID"
        colID.DataPropertyName = "TimbangID"
        colID.Visible = False
        dgvTimbangan.Columns.Add(colID)

        ' Kolom No
        Dim colNo As New DataGridViewTextBoxColumn()
        colNo.HeaderText = "No"
        colNo.Name = "No"
        colNo.Width = 50
        colNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvTimbangan.Columns.Add(colNo)

        ' Kolom No Tiket
        Dim colNoTiket As New DataGridViewTextBoxColumn()
        colNoTiket.HeaderText = "No.Tiket"
        colNoTiket.Name = "NoTiket"
        colNoTiket.DataPropertyName = "NoTiket"
        colNoTiket.Width = 180
        dgvTimbangan.Columns.Add(colNoTiket)

        ' Kolom TransType
        Dim colTransType As New DataGridViewTextBoxColumn()
        colTransType.HeaderText = "Tipe"
        colTransType.Name = "TransType"
        colTransType.DataPropertyName = "TransType"
        colTransType.Width = 60
        colTransType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvTimbangan.Columns.Add(colTransType)

        ' Kolom Tanggal
        Dim colTanggal As New DataGridViewTextBoxColumn()
        colTanggal.HeaderText = "TanggalMasuk"
        colTanggal.Name = "TanggalMasuk"
        colTanggal.DataPropertyName = "TanggalMasuk"
        colTanggal.Width = 150
        colTanggal.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
        dgvTimbangan.Columns.Add(colTanggal)

        ' Kolom No Polisi
        Dim colPolisi As New DataGridViewTextBoxColumn()
        colPolisi.HeaderText = "No. Polisi"
        colPolisi.Name = "NoPolisi"
        colPolisi.DataPropertyName = "NoPolisi"
        colPolisi.Width = 120
        dgvTimbangan.Columns.Add(colPolisi)

        ' Kolom Supir
        Dim colSupir As New DataGridViewTextBoxColumn()
        colSupir.HeaderText = "Nama Supir"
        colSupir.Name = "NamaSupir"
        colSupir.DataPropertyName = "NamaSupir"
        colSupir.Width = 150
        dgvTimbangan.Columns.Add(colSupir)

        ' Kolom Customer
        Dim colCustomer As New DataGridViewTextBoxColumn()
        colCustomer.HeaderText = "Customer"
        colCustomer.Name = "CustomerDisplay"
        colCustomer.DataPropertyName = "CustomerDisplay"
        colCustomer.Width = 200
        dgvTimbangan.Columns.Add(colCustomer)

        ' Kolom Product
        Dim colProduct As New DataGridViewTextBoxColumn()
        colProduct.HeaderText = "Product"
        colProduct.Name = "ProductDisplay"
        colProduct.DataPropertyName = "ProductDisplay"
        colProduct.Width = 150
        dgvTimbangan.Columns.Add(colProduct)

        ' Kolom Berat Masuk
        Dim colBeratMasuk As New DataGridViewTextBoxColumn()
        colBeratMasuk.HeaderText = "Berat Masuk"
        colBeratMasuk.Name = "BeratMasuk"
        colBeratMasuk.DataPropertyName = "BeratMasuk"
        colBeratMasuk.Width = 100
        colBeratMasuk.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        colBeratMasuk.DefaultCellStyle.Format = "N0"
        dgvTimbangan.Columns.Add(colBeratMasuk)

        ' Kolom Berat Keluar
        Dim colBeratKeluar As New DataGridViewTextBoxColumn()
        colBeratKeluar.HeaderText = "Berat Keluar"
        colBeratKeluar.Name = "BeratKeluar"
        colBeratKeluar.DataPropertyName = "BeratKeluar"
        colBeratKeluar.Width = 100
        colBeratKeluar.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        colBeratKeluar.DefaultCellStyle.Format = "N0"
        dgvTimbangan.Columns.Add(colBeratKeluar)

        ' Kolom Berat Netto
        Dim colNetto As New DataGridViewTextBoxColumn()
        colNetto.HeaderText = "Netto"
        colNetto.Name = "BeratNetto"
        colNetto.DataPropertyName = "BeratNetto"
        colNetto.Width = 100
        colNetto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        colNetto.DefaultCellStyle.Format = "N0"
        colNetto.DefaultCellStyle.Font = New Font(dgvTimbangan.Font, FontStyle.Bold)
        dgvTimbangan.Columns.Add(colNetto)

        ' Kolom Berat Bersih
        Dim colBersih As New DataGridViewTextBoxColumn()
        colBersih.HeaderText = "Berat Bersih"
        colBersih.Name = "BeratBersih"
        colBersih.DataPropertyName = "BeratBersih"
        colBersih.Width = 100
        colBersih.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        colBersih.DefaultCellStyle.Format = "N0"
        colBersih.DefaultCellStyle.Font = New Font(dgvTimbangan.Font, FontStyle.Bold)
        colBersih.DefaultCellStyle.ForeColor = Color.Green
        dgvTimbangan.Columns.Add(colBersih)

        ' Kolom Status
        Dim colStatus As New DataGridViewTextBoxColumn()
        colStatus.HeaderText = "Status"
        colStatus.Name = "Status"
        colStatus.DataPropertyName = "Status"
        colStatus.Width = 80
        colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvTimbangan.Columns.Add(colStatus)

        ' Style header
        dgvTimbangan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204)
        dgvTimbangan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvTimbangan.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgvTimbangan.EnableHeadersVisualStyles = False
        dgvTimbangan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgvTimbangan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204)
        dgvTimbangan.DefaultCellStyle.SelectionForeColor = Color.White
    End Sub

    ' =============================================
    ' SETUP TOMBOL BERDASARKAN ROLE
    ' =============================================
    Private Sub SetupButtonsByRole()
        Try
            If BtnEdit IsNot Nothing Then
                BtnEdit.Visible = UserSession.CanEditTimbangan()
                BtnEdit.Enabled = UserSession.CanEditTimbangan()
                Debug.WriteLine($"[SetupButtons] btnEdit.Visible = {BtnEdit.Visible}")
            End If

            If btnHapus IsNot Nothing Then
                btnHapus.Visible = UserSession.CanDeleteTimbangan()
                btnHapus.Enabled = UserSession.CanDeleteTimbangan()
                Debug.WriteLine($"[SetupButtons] btnHapus.Visible = {btnHapus.Visible}")
            End If

            If btnCetakUlang IsNot Nothing Then
                btnCetakUlang.Visible = UserSession.CanReprintStruk()
                btnCetakUlang.Enabled = UserSession.CanReprintStruk()
            End If

            If btnExportExcel IsNot Nothing Then
                btnExportExcel.Visible = UserSession.CanExportReport()
                btnExportExcel.Enabled = UserSession.CanExportReport()
            End If

        Catch ex As Exception
            Debug.WriteLine($"[SetupButtons] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' LOAD DATA - AMBIL SEMUA DATA LALU TAMPILKAN
    ' PER HALAMAN (PAGINATION)
    ' =============================================
    Private Sub LoadData()
        Try
            If cmbPageSize.SelectedItem?.ToString() = "Auto" Then
                _pageSize = CalculatePageSize()
            End If
            Dim query As String = "SELECT " &
                                  "T.TimbangID, T.NoTiket, T.TanggalMasuk, T.NoPolisi, T.NamaSupir, " &
                                  "ISNULL(T.CustomerNama, C.NamaCustomer) AS CustomerDisplay, " &
                                  "ISNULL(T.ProductNama, P.NamaProduk) AS ProductDisplay, " &
                                  "T.BeratMasuk, T.BeratKeluar, T.BeratNetto, T.BeratBersih, T.Status, " &
                                  "T.TransType " &
                                  "FROM Timbangan T " &
                                  "LEFT JOIN Customers C ON T.CustomerID = C.CustomerID " &
                                  "LEFT JOIN Products P ON T.ProductID = P.ProductID " &
                                  "WHERE CAST(T.TanggalMasuk AS DATE) >= @TanggalDari " &
                                  "AND CAST(T.TanggalMasuk AS DATE) <= @TanggalSampai "

            Dim params As New List(Of SqlParameter)
            params.Add(New SqlParameter("@TanggalDari", dtpTanggalDari.Value.Date))
            params.Add(New SqlParameter("@TanggalSampai", dtpTanggalSampai.Value.Date))

            ' =============================================
            ' FILTER TRANS TYPE BERDASARKAN USER SESSION
            ' =============================================
            query &= UserSession.GetTransTypeFilter("T")

            ' Filter Status
            If cmbStatus.SelectedValue IsNot Nothing AndAlso cmbStatus.SelectedValue.ToString() <> "SEMUA" Then
                query &= "AND T.Status = @Status "
                params.Add(New SqlParameter("@Status", cmbStatus.SelectedValue.ToString()))
            End If

            ' Filter No Polisi
            If Not String.IsNullOrWhiteSpace(txtCariNoPolisi.Text) Then
                query &= "AND T.NoPolisi LIKE @NoPolisi "
                params.Add(New SqlParameter("@NoPolisi", "%" & txtCariNoPolisi.Text.Trim() & "%"))
            End If

            query &= "ORDER BY T.TanggalMasuk DESC"

            ' Simpan semua data ke _fullDataTable
            _fullDataTable = DatabaseHelper.ExecuteQuery(query, params.ToArray())

            ' Hitung pagination & reset ke halaman 1
            _totalRows = _fullDataTable.Rows.Count
            _totalPages = If(_totalRows = 0, 1, CInt(Math.Ceiling(_totalRows / _pageSize)))
            _currentPage = 1

            ' Tampilkan halaman pertama
            DisplayCurrentPage()

            ' UpdateInfoTotal tetap pakai _fullDataTable agar total akurat
            UpdateInfoTotal(_fullDataTable)

        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.LoadData] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat data timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TAMPILKAN DATA SESUAI HALAMAN SAAT INI  ← BARU
    ' =============================================
    Private Sub DisplayCurrentPage()
        Try
            If _fullDataTable Is Nothing Then Return

            Dim startIndex As Integer = (_currentPage - 1) * _pageSize
            Dim endIndex As Integer = Math.Min(startIndex + _pageSize, _totalRows) - 1

            ' Buat DataTable subset dengan Clone (salin struktur kolom)
            Dim pageTable As DataTable = _fullDataTable.Clone()
            For i As Integer = startIndex To endIndex
                pageTable.ImportRow(_fullDataTable.Rows(i))
            Next

            dgvTimbangan.DataSource = pageTable

            ' Nomor urut lanjut dari halaman sebelumnya
            For i As Integer = 0 To dgvTimbangan.Rows.Count - 1
                dgvTimbangan.Rows(i).Cells("No").Value = (startIndex + i + 1).ToString()
            Next

            ApplyStatusColorCoding()
            ApplyTransTypeColorCoding()
            UpdatePaginationControls()

        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.Pagination] Error: " & ex.ToString())
            MessageBox.Show("Gagal menampilkan halaman transaksi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' UPDATE LABEL & TOMBOL PAGINATION  ← BARU
    ' =============================================
    Private Sub UpdatePaginationControls()
        Try
            Dim rowsOnPage As Integer = dgvTimbangan.Rows.Count
            Dim startRow As Integer = If(_totalRows = 0, 0, (_currentPage - 1) * _pageSize + 1)
            Dim endRow As Integer = If(_totalRows = 0, 0, (_currentPage - 1) * _pageSize + rowsOnPage)

            lblPageInfo.Text = $"Halaman {_currentPage} / {_totalPages}   |   " &
                               $"Baris {startRow} - {endRow} dari {_totalRows} transaksi"

            btnFirstPage.Enabled = (_currentPage > 1)
            btnPrevPage.Enabled = (_currentPage > 1)
            btnNextPage.Enabled = (_currentPage < _totalPages)
            btnLastPage.Enabled = (_currentPage < _totalPages)
        Catch ex As Exception
            Debug.WriteLine($"[UpdatePaginationControls] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' APPLY STATUS COLOR CODING
    ' =============================================
    Private Sub ApplyStatusColorCoding()
        Try
            For Each row As DataGridViewRow In dgvTimbangan.Rows
                If row.Cells("Status").Value IsNot Nothing AndAlso row.Cells("Status").Value IsNot DBNull.Value Then
                    Dim status As String = row.Cells("Status").Value.ToString().ToUpperInvariant()
                    Select Case status
                        Case "MASUK"
                            row.Cells("Status").Style.BackColor = Color.Orange
                            row.Cells("Status").Style.ForeColor = Color.White
                        Case "SELESAI"
                            row.Cells("Status").Style.BackColor = Color.Green
                            row.Cells("Status").Style.ForeColor = Color.White
                        Case "BATAL"
                            row.Cells("Status").Style.BackColor = Color.Red
                            row.Cells("Status").Style.ForeColor = Color.White
                        Case Else
                            row.Cells("Status").Style.BackColor = Color.Gray
                            row.Cells("Status").Style.ForeColor = Color.White
                    End Select
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.ApplyStatusColorCoding] Error: " & ex.ToString())
        End Try
    End Sub

    ' =============================================
    ' APPLY TRANS TYPE COLOR CODING
    ' =============================================
    Private Sub ApplyTransTypeColorCoding()
        Try
            For Each row As DataGridViewRow In dgvTimbangan.Rows
                If row.Cells("TransType").Value IsNot Nothing AndAlso row.Cells("TransType").Value IsNot DBNull.Value Then
                    Dim transType As String = row.Cells("TransType").Value.ToString().ToUpperInvariant()
                    Select Case transType
                        Case "BELI"
                            row.Cells("TransType").Style.BackColor = Color.DodgerBlue
                            row.Cells("TransType").Style.ForeColor = Color.White
                        Case "JUAL"
                            row.Cells("TransType").Style.BackColor = Color.MediumSeaGreen
                            row.Cells("TransType").Style.ForeColor = Color.White
                        Case Else
                            row.Cells("TransType").Style.BackColor = Color.Gray
                            row.Cells("TransType").Style.ForeColor = Color.White
                    End Select
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.UpdateInfoTotal] Error: " & ex.ToString())
        End Try
    End Sub

    ' =============================================
    ' UPDATE INFO TOTAL
    ' =============================================
    Private Sub UpdateInfoTotal(dt As DataTable)
        Try
            Dim totalTransaksi As Integer = dt.Rows.Count
            Dim totalNetto As Decimal = 0
            Dim totalBersih As Decimal = 0

            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("BeratNetto")) Then totalNetto += CDec(row("BeratNetto"))
                If Not IsDBNull(row("BeratBersih")) Then totalBersih += CDec(row("BeratBersih"))
            Next

            Dim filterInfo As String = If(UserSession.CanViewAllTransType(), "", $" | Filter: {UserSession.AllowedTransType}")
            lblInfo.Text = $"Total: {totalTransaksi} transaksi | Total Netto: {totalNetto:N0} KG | Total Bersih: {totalBersih:N0} KG{filterInfo}"
        Catch ex As Exception
            lblInfo.Text = "Total: 0 transaksi"
        End Try
    End Sub

    ' =============================================
    ' TOMBOL: FILTER
    ' =============================================
    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        LoadData()
        DatabaseHelper.InsertAuditLog(UserSession.UserID, "FILTER_DATA", Nothing, "Timbangan",
            Nothing, Nothing, Nothing,
            $"Filter data - Tanggal : {dtpTanggalDari.Value:dd/MM/yyyy} s/d {dtpTanggalSampai.Value:dd/MM/yyyy}, TransType: {UserSession.AllowedTransType}")
    End Sub

    ' =============================================
    ' TOMBOL: REFRESH
    ' =============================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        dtpTanggalDari.Value = DateTime.Today.AddDays(-30)
        dtpTanggalSampai.Value = DateTime.Today
        cmbStatus.SelectedIndex = 0
        txtCariNoPolisi.Clear()
        LoadData()
    End Sub

    ' =============================================
    ' TOMBOL NAVIGASI PAGINATION  ← BARU
    ' =============================================
    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        If _currentPage <> 1 Then
            _currentPage = 1
            DisplayCurrentPage()
        End If
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            DisplayCurrentPage()
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        If _currentPage < _totalPages Then
            _currentPage += 1
            DisplayCurrentPage()
        End If
    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        If _currentPage <> _totalPages Then
            _currentPage = _totalPages
            DisplayCurrentPage()
        End If
    End Sub

    ' =============================================
    ' COMBOBOX PAGE SIZE CHANGED  ← BARU
    ' =============================================
    Private Sub cmbPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPageSize.SelectedIndexChanged
        Try
            If cmbPageSize.SelectedItem?.ToString() = "Auto" Then
                ' Mode auto: hitung dari tinggi DGV
                _pageSize = CalculatePageSize()
            Else
                Dim newSize As Integer = 0
                If Integer.TryParse(cmbPageSize.SelectedItem?.ToString(), newSize) AndAlso newSize > 0 Then
                    _pageSize = newSize
                End If
            End If

            If _totalRows > 0 Then
                _totalPages = CInt(Math.Ceiling(_totalRows / _pageSize))
                If _currentPage > _totalPages Then _currentPage = _totalPages
                DisplayCurrentPage()
            End If
        Catch ex As Exception
            Debug.WriteLine($"[cmbPageSize] Error: {ex.Message}")
        End Try
    End Sub

    ' ═════════════════════════════════════════════════════════════
    ' TOMBOL: EDIT
    ' ═════════════════════════════════════════════════════════════
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Try
            If Not UserSession.CanEditTimbangan() Then
                MessageBox.Show("⛔ AKSES DITOLAK!" & vbCrLf & vbCrLf &
                               "Hanya Manager, Direktur, dan Programmer yang dapat mengedit data." & vbCrLf & vbCrLf &
                               "Role Anda: " & UserSession.Role,
                               "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If dgvTimbangan.SelectedRows.Count = 0 Then
                MessageBox.Show("Pilih data yang ingin diedit!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim selectedRow As DataGridViewRow = dgvTimbangan.SelectedRows(0)
            Dim timbangID As Integer = CInt(selectedRow.Cells("TimbangID").Value)
            Dim noTiket As String = selectedRow.Cells("NoTiket").Value?.ToString()

            If MessageBox.Show($"Edit data tiket {noTiket}?" & vbCrLf & vbCrLf &
                              "Semua perubahan akan tercatat di Audit Log.",
                              "Konfirmasi Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            Dim frmEdit As New FormEditTimbangan(timbangID)
            Dim result As DialogResult = frmEdit.ShowDialog()

            If result = DialogResult.OK Then
                LoadData()
                MessageBox.Show("✅ Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan] Error: " & ex.ToString())
            MessageBox.Show("Operasi daftar timbangan gagal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═════════════════════════════════════════════════════════════
    ' DOUBLE CLICK DATAGRIDVIEW
    ' ═════════════════════════════════════════════════════════════
    Private Sub dgvTimbangan_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTimbangan.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Return

            If UserSession.CanEditTimbangan() Then
                BtnEdit.PerformClick()
            Else
                ShowDetail()
            End If

        Catch ex As Exception
            Debug.WriteLine($"[DoubleClick] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' TOMBOL: DETAIL
    ' =============================================
    Private Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click
        ShowDetail()
    End Sub

    ' =============================================
    ' SHOW DETAIL
    ' =============================================
    Private Sub ShowDetail()
        Try
            If dgvTimbangan.SelectedRows.Count = 0 Then
                MessageBox.Show("Pilih data yang ingin dilihat detailnya!",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim selectedRow As DataGridViewRow = dgvTimbangan.SelectedRows(0)
            Dim timbangID As Integer = CInt(selectedRow.Cells("TimbangID").Value)
            Dim noTiket As String = selectedRow.Cells("NoTiket").Value?.ToString()
            Dim noPolisi As String = selectedRow.Cells("NoPolisi").Value?.ToString()

            Dim query As String = "SELECT * FROM Timbangan WHERE TimbangID = @ID"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query,
                {New SqlParameter("@ID", timbangID)})

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data tidak ditemukan!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Using frmDetail As New FormDetailTimbangan(timbangID, dt.Rows(0))
                frmDetail.ShowDialog(Me)
            End Using

            DatabaseHelper.InsertAuditLog(UserSession.UserID,
                "VIEW_DETAIL_TIMBANGAN", timbangID, "Timbangan",
                timbangID, Nothing, Nothing,
                $"Lihat detail - No Tiket: {noTiket}, No Pol: {noPolisi}")

        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.ShowDetail] Error: " & ex.ToString())
            MessageBox.Show("Gagal membuka detail timbangan.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═════════════════════════════════════════════════════════════
    ' TOMBOL: CETAK ULANG
    ' ═════════════════════════════════════════════════════════════
    Private Sub btnCetakUlang_Click(sender As Object, e As EventArgs) Handles btnCetakUlang.Click
        Try
            If dgvTimbangan.SelectedRows.Count = 0 Then
                MessageBox.Show("Pilih data yang ingin dicetak ulang!", "Info",
                               MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim selectedRow As DataGridViewRow = dgvTimbangan.SelectedRows(0)
            Dim timbangID As Integer = CInt(selectedRow.Cells("TimbangID").Value)
            Dim noTiket As String = selectedRow.Cells("NoTiket").Value?.ToString()
            Dim status As String = selectedRow.Cells("Status").Value?.ToString()

            If status.ToUpperInvariant() <> "SELESAI" Then
                MessageBox.Show("⚠️ Hanya transaksi dengan status SELESAI yang bisa dicetak ulang!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If MessageBox.Show($"Cetak ulang struk untuk tiket {noTiket}?" & vbCrLf & vbCrLf &
                              "Struk akan ditandai dengan 'DUPLIKAT'.",
                              "Konfirmasi Cetak Ulang", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            PrintHelper.CetakDariDatabase(timbangID, isDuplikat:=True)

            DatabaseHelper.InsertAuditLog(UserSession.UserID, "PRINT_DUPLICATE", timbangID,
                "Timbangan", timbangID, Nothing, Nothing,
                $"Cetak ulang (duplikat) - No Tiket : {noTiket}")

            LoadData()

        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.Reprint] Error: " & ex.ToString())
            MessageBox.Show("Gagal mencetak ulang tiket.", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═════════════════════════════════════════════════════════════
    ' TOMBOL: HAPUS
    ' ═════════════════════════════════════════════════════════════
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Try
            If Not UserSession.CanDeleteTimbangan() Then
                MessageBox.Show("⛔ AKSES DITOLAK!" & vbCrLf & vbCrLf &
                               "Hanya Programmer dan Direktur yang bisa menghapus data!" & vbCrLf & vbCrLf &
                               "Role Anda: " & UserSession.Role,
                               "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If dgvTimbangan.SelectedRows.Count = 0 Then
                MessageBox.Show("Pilih data yang ingin dihapus!", "Info",
                               MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim selectedRow As DataGridViewRow = dgvTimbangan.SelectedRows(0)
            Dim timbangID As Integer = CInt(selectedRow.Cells("TimbangID").Value)
            Dim noTiket As String = selectedRow.Cells("NoTiket").Value?.ToString()
            Dim noPolisi As String = selectedRow.Cells("NoPolisi").Value?.ToString()

            Dim queryDetail As String = "SELECT * FROM Timbangan WHERE TimbangID = @ID"
            Dim dtDetail As DataTable = DatabaseHelper.ExecuteQuery(queryDetail, {New SqlParameter("@ID", timbangID)})
            Dim dataSebelumHapus As String = ""

            If dtDetail.Rows.Count > 0 Then
                Dim row As DataRow = dtDetail.Rows(0)
                dataSebelumHapus = $"NoTiket: {row("NoTiket")}, NoPolisi: {row("NoPolisi")}, " &
                                   $"BeratMasuk: {If(IsDBNull(row("BeratMasuk")), 0, CDec(row("BeratMasuk"))):N0}, " &
                                   $"BeratBersih: {If(IsDBNull(row("BeratBersih")), 0, CDec(row("BeratBersih"))):N0}, " &
                                   $"Status: {row("Status")}"
            End If

            If MessageBox.Show($"⚠️ PERHATIAN!" & vbCrLf & vbCrLf &
                              $"Yakin ingin MENGHAPUS data timbangan:" & vbCrLf &
                              $"No. Tiket: {noTiket}" & vbCrLf &
                              $"No. Polisi: {noPolisi}" & vbCrLf & vbCrLf &
                              "Data yang dihapus TIDAK BISA dikembalikan!",
                              "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                Return
            End If

            Dim query As String = "DELETE FROM Timbangan WHERE TimbangID = @ID"
            Dim result As Integer = DatabaseHelper.ExecuteNonQuery(query, {New SqlParameter("@ID", timbangID)})

            If result > 0 Then
                DatabaseHelper.InsertAuditLog(UserSession.UserID, "Hapus Data Timbangan", timbangID,
                    "Timbangan", timbangID, dataSebelumHapus, Nothing,
                    $"Hapus data - No Tiket : {noTiket}, No Pol : {noPolisi}")

                MessageBox.Show("✅ Data berhasil dihapus!", "Sukses",
                               MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData()
            Else
                MessageBox.Show("Gagal menghapus data!", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.Delete] Error: " & ex.ToString())
            MessageBox.Show("Gagal menghapus transaksi.", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL: EXPORT EXCEL (XLSX - EPPlus 8)
    ' =============================================
    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Try
            If _fullDataTable Is Nothing OrElse _fullDataTable.Rows.Count = 0 Then
                MessageBox.Show("Tidak ada data untuk diekspor!", "Info",
                           MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx"
            saveDialog.FileName = "Laporan_Timbangan_" & Now.ToString("yyyyMMdd_HHmmss") & ".xlsx"

            If saveDialog.ShowDialog() <> DialogResult.OK Then Return

            Me.Cursor = Cursors.WaitCursor

            ' ── Fix EPPlus 8: gunakan License bukan LicenseContext ──
            ExcelPackage.License.SetNonCommercialPersonal("SistemTimbanganPKS")

            Using package As New ExcelPackage()
                Dim ws As ExcelWorksheet = package.Workbook.Worksheets.Add("Laporan Timbangan")

                ' ── Kolom yang visible dan punya DataPropertyName ──
                Dim visibleColumns As List(Of DataGridViewColumn) =
                dgvTimbangan.Columns.Cast(Of DataGridViewColumn) _
                .Where(Function(c) c.Visible AndAlso Not String.IsNullOrWhiteSpace(c.DataPropertyName)) _
                .ToList()

                ' ─────────────────────────────────────────
                ' 1) BARIS JUDUL LAPORAN
                ' ─────────────────────────────────────────
                Dim totalCols As Integer = visibleColumns.Count
                ws.Cells(1, 1, 1, totalCols).Merge = True
                ws.Cells(1, 1).Value = "LAPORAN TIMBANGAN"
                ws.Cells(1, 1).Style.Font.Bold = True
                ws.Cells(1, 1).Style.Font.Size = 14
                ws.Cells(1, 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                ws.Cells(1, 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(1, 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(33, 113, 181))
                ws.Cells(1, 1).Style.Font.Color.SetColor(Color.White)

                ' Sub judul periode
                ws.Cells(2, 1, 2, totalCols).Merge = True
                ws.Cells(2, 1).Value = $"Periode: {dtpTanggalDari.Value:dd/MM/yyyy} s/d {dtpTanggalSampai.Value:dd/MM/yyyy}   |   " &
                                   $"Status: {cmbStatus.Text}   |   " &
                                   $"TransType: {If(UserSession.CanViewAllTransType(), "Semua", UserSession.AllowedTransType)}"
                ws.Cells(2, 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                ws.Cells(2, 1).Style.Font.Italic = True
                ws.Cells(2, 1).Style.Font.Color.SetColor(Color.FromArgb(80, 80, 80))

                ' ─────────────────────────────────────────
                ' 2) HEADER KOLOM (baris ke-4)
                ' ─────────────────────────────────────────
                Dim headerRow As Integer = 4
                For colIndex As Integer = 0 To totalCols - 1
                    Dim cell As ExcelRange = ws.Cells(headerRow, colIndex + 1)
                    cell.Value = visibleColumns(colIndex).HeaderText
                    cell.Style.Font.Bold = True
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid
                    cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(33, 113, 181))
                    cell.Style.Font.Color.SetColor(Color.White)
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.White)
                Next

                ' ─────────────────────────────────────────
                ' 3) DATA ROWS
                ' ─────────────────────────────────────────
                For rowIndex As Integer = 0 To _fullDataTable.Rows.Count - 1
                    Dim dataRow As DataRow = _fullDataTable.Rows(rowIndex)
                    Dim excelRow As Integer = headerRow + 1 + rowIndex

                    ' Alternating row color
                    If (rowIndex Mod 2) = 0 Then
                        ws.Cells(excelRow, 1, excelRow, totalCols).Style.Fill.PatternType = ExcelFillStyle.Solid
                        ws.Cells(excelRow, 1, excelRow, totalCols).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 248, 255))
                    End If

                    For colIndex As Integer = 0 To totalCols - 1
                        Dim propName As String = visibleColumns(colIndex).DataPropertyName
                        Dim cell As ExcelRange = ws.Cells(excelRow, colIndex + 1)

                        ' Isi nilai
                        If Not IsDBNull(dataRow(propName)) Then
                            cell.Value = dataRow(propName)
                        End If

                        ' Format khusus per kolom
                        Dim colName As String = visibleColumns(colIndex).Name
                        Select Case colName
                            Case "TanggalMasuk"
                                cell.Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                            Case "BeratMasuk", "BeratKeluar", "BeratNetto", "BeratBersih"
                                cell.Style.Numberformat.Format = "#,##0"
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
                            Case "Status"
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                cell.Style.Font.Bold = True
                                ' Warna manual per status
                                If Not IsDBNull(dataRow(propName)) Then
                                    Select Case dataRow(propName).ToString().ToUpperInvariant()
                                        Case "SELESAI"
                                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid
                                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(76, 175, 80))
                                            cell.Style.Font.Color.SetColor(Color.White)
                                        Case "MASUK"
                                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid
                                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 152, 0))
                                            cell.Style.Font.Color.SetColor(Color.White)
                                        Case "BATAL"
                                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid
                                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(244, 67, 54))
                                            cell.Style.Font.Color.SetColor(Color.White)
                                    End Select
                                End If
                            Case "TransType"
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                                cell.Style.Font.Bold = True
                                If Not IsDBNull(dataRow(propName)) Then
                                    Select Case dataRow(propName).ToString().ToUpperInvariant()
                                        Case "BELI"
                                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid
                                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(30, 144, 255))
                                            cell.Style.Font.Color.SetColor(Color.White)
                                        Case "JUAL"
                                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid
                                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(60, 179, 113))
                                            cell.Style.Font.Color.SetColor(Color.White)
                                    End Select
                                End If
                            Case Else
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left
                        End Select
                    Next
                Next

                ' ─────────────────────────────────────────
                ' 4) BARIS TOTAL
                ' ─────────────────────────────────────────
                Dim lastDataRow As Integer = headerRow + _fullDataTable.Rows.Count
                Dim totalRow As Integer = lastDataRow + 1

                ws.Cells(totalRow, 1).Value = "TOTAL"
                ws.Cells(totalRow, 1).Style.Font.Bold = True

                Dim nettoIdx As Integer = visibleColumns.FindIndex(Function(c) c.Name = "BeratNetto")
                Dim bersihIdx As Integer = visibleColumns.FindIndex(Function(c) c.Name = "BeratBersih")

                If nettoIdx >= 0 Then
                    Dim c As ExcelRange = ws.Cells(totalRow, nettoIdx + 1)
                    c.Formula = $"SUM({ws.Cells(headerRow + 1, nettoIdx + 1).Address}:{ws.Cells(lastDataRow, nettoIdx + 1).Address})"
                    c.Style.Numberformat.Format = "#,##0"
                    c.Style.Font.Bold = True
                    c.Style.Fill.PatternType = ExcelFillStyle.Solid
                    c.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 227, 252))
                End If

                If bersihIdx >= 0 Then
                    Dim c As ExcelRange = ws.Cells(totalRow, bersihIdx + 1)
                    c.Formula = $"SUM({ws.Cells(headerRow + 1, bersihIdx + 1).Address}:{ws.Cells(lastDataRow, bersihIdx + 1).Address})"
                    c.Style.Numberformat.Format = "#,##0"
                    c.Style.Font.Bold = True
                    c.Style.Fill.PatternType = ExcelFillStyle.Solid
                    c.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 252, 219))
                End If

                ws.Cells(totalRow, 1, totalRow, totalCols).Style.Border.Top.Style = ExcelBorderStyle.Medium

                ' ─────────────────────────────────────────
                ' 5) BORDER SEMUA DATA
                ' ─────────────────────────────────────────
                Using rng As ExcelRange = ws.Cells(headerRow, 1, totalRow, totalCols)
                    rng.Style.Border.Top.Style = ExcelBorderStyle.Hair
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Hair
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Hair
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Hair
                End Using

                ' ─────────────────────────────────────────
                ' 6) FREEZE HEADER & AUTOFIT
                ' ─────────────────────────────────────────
                ws.View.FreezePanes(headerRow + 1, 1)
                ws.Cells(headerRow, 1, totalRow, totalCols).AutoFitColumns()

                ' ─────────────────────────────────────────
                ' 7) PRINT SETTINGS
                ' ─────────────────────────────────────────
                ws.PrinterSettings.Orientation = eOrientation.Landscape
                ws.PrinterSettings.FitToPage = True
                ws.PrinterSettings.FitToWidth = 1
                ws.PrinterSettings.FitToHeight = 0
                ws.PrinterSettings.RepeatRows = New ExcelAddress("4:4")

                package.SaveAs(New IO.FileInfo(saveDialog.FileName))
            End Using

            Me.Cursor = Cursors.Default

            DatabaseHelper.InsertAuditLog(UserSession.UserID, "Export Data", Nothing, "Timbangan",
            Nothing, Nothing, Nothing,
            $"Export ke XLSX - {saveDialog.FileName}, {_fullDataTable.Rows.Count} record")

            MessageBox.Show($"✅ Laporan Excel berhasil dibuat!{vbCrLf}Total: {_fullDataTable.Rows.Count} transaksi",
                       "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If MessageBox.Show("Buka file sekarang?", "Konfirmasi",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Process.Start(New ProcessStartInfo(saveDialog.FileName) With {.UseShellExecute = True})
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Debug.WriteLine("[FormDaftarTimbangan.Export] Error: " & ex.ToString())
            MessageBox.Show("Gagal mengekspor data timbangan.", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL: TUTUP
    ' =============================================
    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Close()
    End Sub

    ' =============================================
    ' DATAGRIDVIEW SELECTION CHANGED
    ' =============================================
    Private Sub dgvTimbangan_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTimbangan.SelectionChanged
        If dgvTimbangan.SelectedRows.Count > 0 Then
            _selectedTimbangID = CInt(dgvTimbangan.SelectedRows(0).Cells("TimbangID").Value)
        End If
    End Sub

    ' =============================================
    ' FORM CLOSING
    ' =============================================
    Private Sub FormDaftarTimbangan_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            DatabaseHelper.InsertAuditLog(UserSession.UserID, "CLOSE_FORM", Nothing,
                "Daftar Timbangan", Nothing, Nothing, Nothing,
                "User menutup Daftar Timbangan")
        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.FormClosing] Audit error: " & ex.ToString())
        End Try
    End Sub

    ' =============================================
    ' HITUNG PAGE SIZE OTOMATIS BERDASARKAN TINGGI DGV
    ' ← BARU
    ' =============================================
    Private Function CalculatePageSize() As Integer
        Try
            Dim headerHeight As Integer = dgvTimbangan.ColumnHeadersHeight
            Dim rowHeight As Integer = dgvTimbangan.RowTemplate.Height
            Dim availableHeight As Integer = dgvTimbangan.ClientSize.Height - headerHeight - 2

            Dim calculated As Integer = Math.Floor(availableHeight / rowHeight)
            Return If(calculated < 5, 5, calculated) ' Minimal 5 baris
        Catch ex As Exception
            Debug.WriteLine("[FormDaftarTimbangan.CalculatePageSize] Error: " & ex.ToString())
            Return 20 ' Fallback default
        End Try
    End Function

    ' =============================================
    ' RECALCULATE SAAT FORM RESIZE  ← BARU
    ' =============================================
    Private Sub RecalculateOnResize()
        Try
            If _fullDataTable Is Nothing Then Return

            Dim newPageSize As Integer = CalculatePageSize()
            If newPageSize = _pageSize Then Return ' Tidak ada perubahan

            _pageSize = newPageSize

            ' Sync cmbPageSize jika nilainya ada di list
            RemoveHandler cmbPageSize.SelectedIndexChanged, AddressOf cmbPageSize_SelectedIndexChanged
            Dim idx As Integer = cmbPageSize.Items.IndexOf(_pageSize.ToString())
            If idx >= 0 Then
                cmbPageSize.SelectedIndex = idx
            Else
                ' Nilai dinamis tidak ada di list, tampilkan saja tanpa select
                cmbPageSize.Text = _pageSize.ToString()
            End If
            AddHandler cmbPageSize.SelectedIndexChanged, AddressOf cmbPageSize_SelectedIndexChanged

            _totalPages = If(_totalRows = 0, 1, CInt(Math.Ceiling(_totalRows / _pageSize)))
            If _currentPage > _totalPages Then _currentPage = _totalPages
            DisplayCurrentPage()

        Catch ex As Exception
            Debug.WriteLine($"[RecalculateOnResize] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' FORM RESIZE - RECALCULATE PAGE SIZE  ← BARU
    ' =============================================
    Private Sub FormDaftarTimbangan_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ' Hanya recalculate jika mode Auto dipilih
        If cmbPageSize.SelectedItem?.ToString() = "Auto" Then
            RecalculateOnResize()
        End If
    End Sub

    Private Sub dgvTimbangan_Resize(sender As Object, e As EventArgs) Handles dgvTimbangan.Resize
        ' Hanya recalculate jika mode Auto dipilih
        If cmbPageSize.SelectedItem?.ToString() = "Auto" Then
            RecalculateOnResize()
        End If
    End Sub

End Class