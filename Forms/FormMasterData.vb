' =============================================
' FormMasterData.vb
' Master Data Management: Customer, Transporter, Product
' =============================================
Imports System.Data.SqlClient

Public Class FormMasterData

    ' Jenis data yang sedang aktif (untuk reuse kode)
    Private currentDataType As String = "Customer"

    ' ★ PAGINATION - Variabel Customer
    Private customerCurrentPage As Integer = 1
    Private customerTotalPages As Integer = 1
    Private Const CUSTOMER_PAGE_SIZE As Integer = 25

    ' ★ PAGINATION - Variabel Transporter
    Private transporterCurrentPage As Integer = 1
    Private transporterTotalPages As Integer = 1
    Private Const TRANSPORTER_PAGE_SIZE As Integer = 25

    ' ★ PAGINATION - Simpan semua data agar bisa dipaging & difilter
    Private dtAllCustomer As DataTable = Nothing
    Private dtAllTransporter As DataTable = Nothing

    ' ★ PAGINATION - Variabel (bukan konstanta lagi, supaya bisa dinamis)
    Private customerPageSize As Integer = 25
    Private transporterPageSize As Integer = 25

    ' Tambahkan Sub ini
    Private Sub CalculatePageSize()
        Dim headerHeight As Integer = dgvCustomer.ColumnHeadersHeight
        Dim rowHeight As Integer = dgvCustomer.RowTemplate.Height

        If rowHeight > 0 Then
            customerPageSize = Math.Max(1, ((dgvCustomer.Height - headerHeight) \ rowHeight) - 2)  ' ★ kurangi 1
        End If

        Dim headerHeightT As Integer = dgvTransporter.ColumnHeadersHeight
        Dim rowHeightT As Integer = dgvTransporter.RowTemplate.Height

        If rowHeightT > 0 Then
            transporterPageSize = Math.Max(1, ((dgvTransporter.Height - headerHeightT) \ rowHeightT) - 3)  ' ★ kurangi 1
        End If
    End Sub

    ' =============================================
    ' FORM LOAD
    ' =============================================
    Private Sub FormMasterData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CalculatePageSize()
        LoadData("Customer")
        LoadData("Transporter")
        LoadData("Product")

        ' ===== AUDIT LOG =====
        Try
            DatabaseHelper.InsertAuditLog(
            UserSession.UserID,
            "OPEN_FORM",
            Nothing,
            "Master Data",
            Nothing,
            Nothing,
            Nothing,
            "User membuka Master Data"
        )
        Catch ex As Exception
            Debug.WriteLine($"[AuditLog] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' LOAD DATA KE DATAGRIDVIEW
    ' =============================================
    Private Sub LoadData(dataType As String)
        Try
            Dim query As String = ""
            Select Case dataType.ToLowerInvariant()
                Case "customer"
                    query = "SELECT CustomerID, KodeCustomer, NamaCustomer, Alamat, IsActive FROM Customers ORDER BY NamaCustomer"
                    dtAllCustomer = DatabaseHelper.ExecuteQuery(query)       ' ★ PAGINATION
                    customerCurrentPage = 1                                   ' ★ PAGINATION
                    ApplyCustomerPagination()                                 ' ★ PAGINATION

                Case "transporter"
                    query = "SELECT TransporterID, KodeTransporter, NamaTransporter, Alamat, IsActive FROM Transporters ORDER BY NamaTransporter"
                    dtAllTransporter = DatabaseHelper.ExecuteQuery(query)     ' ★ PAGINATION
                    transporterCurrentPage = 1                                ' ★ PAGINATION
                    ApplyTransporterPagination()                              ' ★ PAGINATION

                Case "product"
                    query = "SELECT ProductID, KodeProduk, NamaProduk, IsActive FROM Products ORDER BY NamaProduk"
                    dgvProduct.DataSource = DatabaseHelper.ExecuteQuery(query)
            End Select
        Catch ex As Exception
            Debug.WriteLine("[FormMasterData.LoadData] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat data master. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' ★ PAGINATION - CUSTOMER
    ' =============================================
    Private Sub ApplyCustomerPagination()
        If dtAllCustomer Is Nothing OrElse dtAllCustomer.Rows.Count = 0 Then
            dgvCustomer.DataSource = dtAllCustomer
            customerTotalPages = 1
            customerCurrentPage = 1
            UpdateCustomerPageInfo()
            Return
        End If

        Dim totalRows As Integer = dtAllCustomer.Rows.Count
        customerTotalPages = CInt(Math.Ceiling(totalRows / customerPageSize))  ' ★ pakai variabel

        If customerCurrentPage > customerTotalPages Then customerCurrentPage = customerTotalPages
        If customerCurrentPage < 1 Then customerCurrentPage = 1

        Dim pagedData As DataTable = dtAllCustomer.Clone()
        Dim startIndex As Integer = (customerCurrentPage - 1) * customerPageSize  ' ★
        Dim endIndex As Integer = Math.Min(startIndex + customerPageSize - 1, totalRows - 1)  ' ★

        For i As Integer = startIndex To endIndex
            pagedData.ImportRow(dtAllCustomer.Rows(i))
        Next

        dgvCustomer.DataSource = pagedData
        UpdateCustomerPageInfo()
    End Sub

    Private Sub UpdateCustomerPageInfo()
        lblPageInfoCustomer.Text = $"Page {customerCurrentPage} of {customerTotalPages}"
        btnPrevCustomer.Enabled = (customerCurrentPage > 1)
        btnNextCustomer.Enabled = (customerCurrentPage < customerTotalPages)
    End Sub

    Private Sub btnPrevCustomer_Click(sender As Object, e As EventArgs) Handles btnPrevCustomer.Click
        If customerCurrentPage > 1 Then
            customerCurrentPage -= 1
            ApplyCustomerPagination()
        End If
    End Sub

    Private Sub btnNextCustomer_Click(sender As Object, e As EventArgs) Handles btnNextCustomer.Click
        If customerCurrentPage < customerTotalPages Then
            customerCurrentPage += 1
            ApplyCustomerPagination()
        End If
    End Sub

    ' =============================================
    ' ★ PAGINATION - TRANSPORTER
    ' =============================================
    Private Sub ApplyTransporterPagination()
        If dtAllTransporter Is Nothing OrElse dtAllTransporter.Rows.Count = 0 Then
            dgvTransporter.DataSource = dtAllTransporter
            transporterTotalPages = 1
            transporterCurrentPage = 1
            UpdateTransporterPageInfo()
            Return
        End If

        Dim totalRows As Integer = dtAllTransporter.Rows.Count
        transporterTotalPages = CInt(Math.Ceiling(totalRows / transporterPageSize))  ' ★

        If transporterCurrentPage > transporterTotalPages Then transporterCurrentPage = transporterTotalPages
        If transporterCurrentPage < 1 Then transporterCurrentPage = 1

        Dim pagedData As DataTable = dtAllTransporter.Clone()
        Dim startIndex As Integer = (transporterCurrentPage - 1) * transporterPageSize  ' ★
        Dim endIndex As Integer = Math.Min(startIndex + transporterPageSize - 1, totalRows - 1)  ' ★

        For i As Integer = startIndex To endIndex
            pagedData.ImportRow(dtAllTransporter.Rows(i))
        Next

        dgvTransporter.DataSource = pagedData
        UpdateTransporterPageInfo()
    End Sub

    Private Sub UpdateTransporterPageInfo()
        lblPageInfoTransporter.Text = $"Page {transporterCurrentPage} of {transporterTotalPages}"
        btnPrevTransporter.Enabled = (transporterCurrentPage > 1)
        btnNextTransporter.Enabled = (transporterCurrentPage < transporterTotalPages)
    End Sub

    Private Sub btnPrevTransporter_Click(sender As Object, e As EventArgs) Handles btnPrevTransporter.Click
        If transporterCurrentPage > 1 Then
            transporterCurrentPage -= 1
            ApplyTransporterPagination()
        End If
    End Sub

    Private Sub btnNextTransporter_Click(sender As Object, e As EventArgs) Handles btnNextTransporter.Click
        If transporterCurrentPage < transporterTotalPages Then
            transporterCurrentPage += 1
            ApplyTransporterPagination()
        End If
    End Sub

    ' =============================================
    ' REFRESH BUTTONS
    ' =============================================
    Private Sub btnRefreshCustomer_Click(sender As Object, e As EventArgs) Handles btnRefreshCustomer.Click
        LoadData("Customer")
    End Sub

    Private Sub btnRefreshTransporter_Click(sender As Object, e As EventArgs) Handles btnRefreshTransporter.Click
        LoadData("Transporter")
    End Sub

    Private Sub btnRefreshProduct_Click(sender As Object, e As EventArgs) Handles btnRefreshProduct.Click
        LoadData("Product")
    End Sub

    ' =============================================
    ' CARI BUTTONS
    ' =============================================
    Private Sub btnCariCustomer_Click(sender As Object, e As EventArgs) Handles btnCariCustomer.Click
        FilterData("Customer", txtCariCustomer.Text)
    End Sub

    Private Sub btnCariTransporter_Click(sender As Object, e As EventArgs) Handles btnCariTransporter.Click
        FilterData("Transporter", txtCariTransporter.Text)
    End Sub

    Private Sub btnCariProduct_Click(sender As Object, e As EventArgs) Handles btnCariProduct.Click
        FilterData("Product", txtCariProduct.Text)
    End Sub

    ' =============================================
    ' FILTER DATA BERDASARKAN PENCARIAN
    ' ★ PAGINATION - Diubah agar filter bekerja dari data lengkap lalu di-paging ulang
    ' =============================================
    Private Sub FilterData(dataType As String, keyword As String)
        Try
            Select Case dataType.ToLowerInvariant()
                Case "customer"
                    If dtAllCustomer Is Nothing Then Return
                    If Not String.IsNullOrEmpty(keyword) Then
                        ' Filter dari data lengkap, simpan hasil filter sebagai dtAllCustomer sementara
                        Dim dtOriginal As DataTable = dtAllCustomer.Copy()
                        dtOriginal.DefaultView.RowFilter = String.Format("NamaCustomer LIKE '%{0}%'", EscapeRowFilterValue(keyword))
                        dtAllCustomer = dtOriginal.DefaultView.ToTable()
                    Else
                        ' Jika keyword kosong, reload dari database
                        LoadData("Customer")
                        Return
                    End If
                    customerCurrentPage = 1
                    ApplyCustomerPagination()

                Case "transporter"
                    If dtAllTransporter Is Nothing Then Return
                    If Not String.IsNullOrEmpty(keyword) Then
                        Dim dtOriginal As DataTable = dtAllTransporter.Copy()
                        dtOriginal.DefaultView.RowFilter = String.Format("NamaTransporter LIKE '%{0}%'", EscapeRowFilterValue(keyword))
                        dtAllTransporter = dtOriginal.DefaultView.ToTable()
                    Else
                        LoadData("Transporter")
                        Return
                    End If
                    transporterCurrentPage = 1
                    ApplyTransporterPagination()

                Case "product"
                    ' Product tetap tanpa pagination (seperti aslinya)
                    Dim dt As DataTable = CType(dgvProduct.DataSource, DataTable)
                    If dt Is Nothing Then Return
                    dt = dt.Copy()
                    If Not String.IsNullOrEmpty(keyword) Then
                        dt.DefaultView.RowFilter = String.Format("NamaProduk LIKE '%{0}%'", EscapeRowFilterValue(keyword))
                    Else
                        dt.DefaultView.RowFilter = ""
                    End If
                    dgvProduct.DataSource = dt.DefaultView.ToTable()
            End Select
        Catch ex As Exception
            Debug.WriteLine("[FormMasterData.FilterData] Error: " & ex.ToString())
            MessageBox.Show("Gagal memfilter data. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Function EscapeRowFilterValue(value As String) As String
        If value Is Nothing Then Return String.Empty

        ' Escape DataColumn expression wildcards and string delimiters so a search term
        ' is treated strictly as text rather than as a RowFilter expression.
        Return value.Replace("]", "[]]") _
                   .Replace("[", "[[]") _
                   .Replace("%", "[%]") _
                   .Replace("*", "[*]") _
                   .Replace("'", "''")
    End Function

    ' =============================================
    ' TAMBAH BUTTONS
    ' =============================================
    Private Sub btnTambahCustomer_Click(sender As Object, e As EventArgs) Handles btnTambahCustomer.Click
        currentDataType = "Customer"
        ShowEditForm(0, "", "", "")
    End Sub

    Private Sub btnTambahTransporter_Click(sender As Object, e As EventArgs) Handles btnTambahTransporter.Click
        currentDataType = "Transporter"
        ShowEditForm(0, "", "", "")
    End Sub

    Private Sub btnTambahProduct_Click(sender As Object, e As EventArgs) Handles btnTambahProduct.Click
        currentDataType = "Product"
        ShowEditForm(0, "", "", "")
    End Sub

    ' =============================================
    ' EDIT BUTTONS
    ' =============================================
    Private Sub btnEditCustomer_Click(sender As Object, e As EventArgs) Handles btnEditCustomer.Click
        EditSelected("Customer")
    End Sub

    Private Sub btnEditTransporter_Click(sender As Object, e As EventArgs) Handles btnEditTransporter.Click
        EditSelected("Transporter")
    End Sub

    Private Sub btnEditProduct_Click(sender As Object, e As EventArgs) Handles btnEditProduct.Click
        EditSelected("Product")
    End Sub

    ' =============================================
    ' HAPUS BUTTONS
    ' =============================================
    Private Sub btnHapusCustomer_Click(sender As Object, e As EventArgs) Handles btnHapusCustomer.Click
        DeleteSelected("Customer")
    End Sub

    Private Sub btnHapusTransporter_Click(sender As Object, e As EventArgs) Handles btnHapusTransporter.Click
        DeleteSelected("Transporter")
    End Sub

    Private Sub btnHapusProduct_Click(sender As Object, e As EventArgs) Handles btnHapusProduct.Click
        DeleteSelected("Product")
    End Sub

    ' =============================================
    ' TOMBOL TUTUP
    ' =============================================
    Private Sub btnTutup_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    ' =============================================
    ' FUNGSI BERSAMA
    ' =============================================
    Private Sub EditSelected(dataType As String)
        Dim dgv As DataGridView = Nothing
        Select Case dataType.ToLowerInvariant()
            Case "customer" : dgv = dgvCustomer
            Case "transporter" : dgv = dgvTransporter
            Case "product" : dgv = dgvProduct
        End Select

        If dgv.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data yang ingin diedit.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row = dgv.SelectedRows(0)
        Dim id = CInt(row.Cells(0).Value)
        Dim kode = row.Cells(1).Value.ToString()
        Dim nama = row.Cells(2).Value.ToString()
        Dim alamat = If(dataType <> "Product", row.Cells(3).Value.ToString(), "")

        currentDataType = dataType
        ShowEditForm(id, kode, nama, alamat)
    End Sub

    Private Sub DeleteSelected(dataType As String)
        Dim dgv As DataGridView = Nothing
        Select Case dataType.ToLowerInvariant()
            Case "customer" : dgv = dgvCustomer
            Case "transporter" : dgv = dgvTransporter
            Case "product" : dgv = dgvProduct
        End Select

        If dgv.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data yang ingin dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If MessageBox.Show($"Yakin hapus {dataType.ToLowerInvariant()} ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        ' === AMBIL DATA SEBELUM DIHAPUS ===
        Dim id = CInt(dgv.SelectedRows(0).Cells(0).Value)
        Dim kode As String = dgv.SelectedRows(0).Cells(1).Value?.ToString()
        Dim nama As String = dgv.SelectedRows(0).Cells(2).Value?.ToString()

        Dim query As String = Nothing
        Select Case dataType.ToLowerInvariant()
            Case "customer"
                query = "DELETE FROM Customers WHERE CustomerID = @ID"
            Case "transporter"
                query = "DELETE FROM Transporters WHERE TransporterID = @ID"
            Case "product"
                query = "DELETE FROM Products WHERE ProductID = @ID"
            Case Else
                MessageBox.Show("Jenis data tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
        End Select

        Dim result = DatabaseHelper.ExecuteNonQuery(query, {New SqlParameter("@ID", id)})

        If result > 0 Then
            ' ===== AUDIT LOG =====
            Try
                DatabaseHelper.InsertAuditLog(
                UserSession.UserID,
                $"DELETE_{dataType.ToUpperInvariant()}",
                Nothing,
                $"{dataType}s",
                id,
                $"Kode : {kode}, Nama : {nama}",
                Nothing,
                $"Hapus {dataType} - ID : {id}, Kode: {kode}, Nama : {nama}"
            )
            Catch exAudit As Exception
                Debug.WriteLine($"[AuditLog] Error : {exAudit.Message}")
            End Try

            MessageBox.Show($"{dataType} berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData(dataType)
        Else
            MessageBox.Show("Gagal menghapus data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' =============================================
    ' TAMPILKAN FORM EDIT/ADD - DIPERBAIKI UNTUK PRODUCTS
    ' =============================================
    Private Sub ShowEditForm(id As Integer, kode As String, nama As String, alamat As String)
        Dim frm As New Form()
        frm.Text = If(id = 0, $"Tambah {currentDataType}", $"Edit {currentDataType}")
        frm.Size = New Size(400, 250)
        frm.StartPosition = FormStartPosition.CenterParent
        frm.FormBorderStyle = FormBorderStyle.FixedDialog
        frm.MaximizeBox = False

        ' Label & TextBox
        Dim lblKode As New Label() With {.Text = "Kode:", .Location = New Point(20, 20), .AutoSize = True}
        Dim txtKode As New TextBox() With {.Text = kode, .Location = New Point(120, 20), .Width = 200}

        Dim lblNama As New Label() With {.Text = "Nama:", .Location = New Point(20, 60), .AutoSize = True}
        Dim txtNama As New TextBox() With {.Text = nama, .Location = New Point(120, 60), .Width = 200}

        Dim lblAlamat As New Label() With {.Text = "Alamat:", .Location = New Point(20, 100), .AutoSize = True,
        .Visible = (currentDataType <> "Product")}
        Dim txtAlamat As New TextBox() With {.Text = alamat, .Location = New Point(120, 100), .Width = 200,
        .Visible = (currentDataType <> "Product")}

        ' Tombol Simpan
        Dim btnSave As New Button() With {.Text = "Simpan", .Location = New Point(120, 150), .Width = 100}

        AddHandler btnSave.Click, Sub()
                                      If String.IsNullOrWhiteSpace(txtNama.Text) Then
                                          MessageBox.Show("Nama tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                          Return
                                      End If

                                      Dim query As String = ""
                                      Dim params As SqlParameter()
                                      Dim actionType As String
                                      Dim keterangan As String
                                      Dim recordID As Integer = id

                                      If id = 0 Then
                                          ' === INSERT ===
                                          Select Case currentDataType.ToLowerInvariant()
                                              Case "product"
                                                  query = "INSERT INTO Products (KodeProduk, NamaProduk, IsActive) VALUES (@Kode, @Nama, 1); SELECT SCOPE_IDENTITY();"
                                                  params = {
                                                      New SqlParameter("@Kode", txtKode.Text.Trim()),
                                                      New SqlParameter("@Nama", txtNama.Text.Trim())
                                                  }
                                              Case "customer"
                                                  query = "INSERT INTO Customers (KodeCustomer, NamaCustomer, Alamat, IsActive) VALUES (@Kode, @Nama, @Alamat, 1); SELECT SCOPE_IDENTITY();"
                                                  params = {
                                                      New SqlParameter("@Kode", txtKode.Text.Trim()),
                                                      New SqlParameter("@Nama", txtNama.Text.Trim()),
                                                      New SqlParameter("@Alamat", txtAlamat.Text.Trim())
                                                  }
                                              Case "transporter"
                                                  query = "INSERT INTO Transporters (KodeTransporter, NamaTransporter, Alamat, IsActive) VALUES (@Kode, @Nama, @Alamat, 1); SELECT SCOPE_IDENTITY();"
                                                  params = {
                                                      New SqlParameter("@Kode", txtKode.Text.Trim()),
                                                      New SqlParameter("@Nama", txtNama.Text.Trim()),
                                                      New SqlParameter("@Alamat", txtAlamat.Text.Trim())
                                                  }
                                              Case Else
                                                  MessageBox.Show("Jenis data tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                  Return
                                          End Select
                                          actionType = $"INSERT_{currentDataType.ToUpperInvariant()}"
                                          keterangan = $"Tambah {currentDataType} baru - Kode: {txtKode.Text}, Nama: {txtNama.Text}"
                                      Else
                                          ' === UPDATE ===
                                          Select Case currentDataType.ToLowerInvariant()
                                              Case "product"
                                                  query = "UPDATE Products SET KodeProduk = @Kode, NamaProduk = @Nama WHERE ProductID = @ID"
                                                  params = {
                                                      New SqlParameter("@Kode", txtKode.Text.Trim()),
                                                      New SqlParameter("@Nama", txtNama.Text.Trim()),
                                                      New SqlParameter("@ID", id)
                                                  }
                                              Case "customer"
                                                  query = "UPDATE Customers SET KodeCustomer = @Kode, NamaCustomer = @Nama, Alamat = @Alamat WHERE CustomerID = @ID"
                                                  params = {
                                                      New SqlParameter("@Kode", txtKode.Text.Trim()),
                                                      New SqlParameter("@Nama", txtNama.Text.Trim()),
                                                      New SqlParameter("@Alamat", txtAlamat.Text.Trim()),
                                                      New SqlParameter("@ID", id)
                                                  }
                                              Case "transporter"
                                                  query = "UPDATE Transporters SET KodeTransporter = @Kode, NamaTransporter = @Nama, Alamat = @Alamat WHERE TransporterID = @ID"
                                                  params = {
                                                      New SqlParameter("@Kode", txtKode.Text.Trim()),
                                                      New SqlParameter("@Nama", txtNama.Text.Trim()),
                                                      New SqlParameter("@Alamat", txtAlamat.Text.Trim()),
                                                      New SqlParameter("@ID", id)
                                                  }
                                              Case Else
                                                  MessageBox.Show("Jenis data tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                  Return
                                          End Select
                                          actionType = $"UPDATE_{currentDataType.ToUpperInvariant()}"
                                          keterangan = $"Edit {currentDataType} - ID: {id}, Kode: {txtKode.Text}, Nama: {txtNama.Text}"
                                      End If

                                      ' === EXECUTE QUERY ===
                                      Dim result As Object = Nothing

                                      If id = 0 Then
                                          result = DatabaseHelper.ExecuteScalar(query, params)
                                      Else
                                          Dim rowsAffected As Integer = DatabaseHelper.ExecuteNonQuery(query, params)
                                          result = If(rowsAffected > 0, CObj(rowsAffected), Nothing)
                                      End If

                                      If result IsNot Nothing Then
                                          If id = 0 AndAlso result IsNot Nothing Then
                                              Integer.TryParse(result.ToString(), recordID)
                                          End If

                                          ' ===== AUDIT LOG =====
                                          Try
                                              DatabaseHelper.InsertAuditLog(
                                              UserSession.UserID,
                                              actionType,
                                              Nothing,
                                              $"{currentDataType}s",
                                              recordID,
                                              If(id = 0, Nothing, $"Kode: {kode}, Nama: {nama}"),
                                              $"Kode: {txtKode.Text}, Nama: {txtNama.Text}",
                                              keterangan
                                          )
                                          Catch exAudit As Exception
                                              Debug.WriteLine($"[AuditLog] Error: {exAudit.Message}")
                                          End Try

                                          MessageBox.Show("Data berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                          LoadData(currentDataType)
                                          frm.Close()
                                      Else
                                          MessageBox.Show("Gagal menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                      End If
                                  End Sub

        frm.Controls.AddRange({lblKode, txtKode, lblNama, txtNama, lblAlamat, txtAlamat, btnSave})
        frm.ShowDialog()
    End Sub

    ' --- TEKAN ENTER DI TEXTBOX PENCARIAN AKAN MEMICU TOMBOL CARI ---

    Private Sub txtCariCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariCustomer.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            btnCariCustomer.PerformClick()
            e.Handled = True
        End If
    End Sub

    Private Sub txtCariTransporter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariTransporter.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            btnCariTransporter.PerformClick()
            e.Handled = True
        End If
    End Sub

    Private Sub txtCariProduct_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariProduct.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            btnCariProduct.PerformClick()
            e.Handled = True
        End If
    End Sub

    ' =============================================
    ' EVENT HANDLER: KLIK CHECKBOX ISACTIVE
    ' =============================================

    Private Sub dgvCustomer_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCustomer.CellContentClick
        HandleIsActiveToggle("Customer", dgvCustomer, e)
    End Sub

    Private Sub dgvTransporter_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTransporter.CellContentClick
        HandleIsActiveToggle("Transporter", dgvTransporter, e)
    End Sub

    Private Sub dgvProduct_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProduct.CellContentClick
        HandleIsActiveToggle("Product", dgvProduct, e)
    End Sub

    ' =============================================
    ' FUNGSI BERSAMA: TOGGLE ISACTIVE
    ' =============================================
    Private Sub HandleIsActiveToggle(dataType As String, dgv As DataGridView, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        If dgv.Columns(e.ColumnIndex).Name <> "IsActive" Then Return

        Dim currentValue As Boolean = CBool(dgv.Rows(e.RowIndex).Cells("IsActive").Value)
        Dim newValue As Boolean = Not currentValue
        Dim id As Integer = CInt(dgv.Rows(e.RowIndex).Cells(0).Value)
        Dim nama As String = dgv.Rows(e.RowIndex).Cells(2).Value?.ToString()

        Dim query As String = ""
        Select Case dataType.ToLowerInvariant()
            Case "customer"
                query = "UPDATE Customers SET IsActive = @IsActive WHERE CustomerID = @ID"
            Case "transporter"
                query = "UPDATE Transporters SET IsActive = @IsActive WHERE TransporterID = @ID"
            Case "product"
                query = "UPDATE Products SET IsActive = @IsActive WHERE ProductID = @ID"
        End Select

        Try
            Dim params As SqlParameter() = {
                New SqlParameter("@IsActive", newValue),
                New SqlParameter("@ID", id)
            }

            Dim result As Integer = DatabaseHelper.ExecuteNonQuery(query, params)

            If result > 0 Then
                dgv.Rows(e.RowIndex).Cells("IsActive").Value = newValue

                ' ===== AUDIT LOG =====
                Try
                    DatabaseHelper.InsertAuditLog(
                        UserSession.UserID,
                        $"TOGGLE_STATUS_{dataType.ToUpperInvariant()}",
                        Nothing,
                        $"{dataType}s",
                        id,
                        $"Is Active: {currentValue}",
                        $"Is Active: {newValue}",
                        $"Ubah status {dataType} '{nama}' menjadi {If(newValue, "Aktif", "Nonaktif")}"
                    )
                Catch exAudit As Exception
                    Debug.WriteLine($"[AuditLog] Error : {exAudit.Message}")
                End Try
            Else
                MessageBox.Show("Gagal mengubah status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormMasterData] Error: " & ex.ToString())
            MessageBox.Show("Operasi data master gagal. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================= 
    ' FORM CLOSING - AUDIT LOG
    ' ============================================= 
    Private Sub FormMasterData_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            DatabaseHelper.InsertAuditLog(
            UserSession.UserID,
            "CLOSE_FORM",
            Nothing,
            "Master Data",
            Nothing,
            Nothing,
            Nothing,
            "User menutup Master Data"
        )
        Catch ex As Exception
            Debug.WriteLine($"[AuditLog] Error: {ex.Message}")
        End Try
    End Sub

End Class