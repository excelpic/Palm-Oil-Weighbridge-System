' ============================================= 
' FormAuditLog.vb - DIPERBAIKI
' Form Audit Log - FILTER & PAGINATION FIXED
' ============================================= 
Imports System.Data.SqlClient

Public Class FormAuditLog

    ' Variabel untuk pagination
    Private CurrentPage As Integer = 1
    Private PageSize As Integer = 50
    Private TotalRecords As Integer = 0
    Private TotalPages As Integer = 0

    ' ============================================= 
    ' FORM LOAD
    ' ============================================= 
    Private Sub FormAuditLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Set default tanggal (30 hari terakhir)
            dtpTanggalMulai.Value = DateTime.Today.AddDays(-30)
            dtpTanggalAkhir.Value = DateTime.Today

            ' Load ComboBox filter
            LoadFilterOptions()

            ' Load statistik cards
            LoadStatistikCards()

            ' Load data audit log
            CurrentPage = 1
            LoadAuditLogData()
            btnHapusLogLama.Visible = False
            btnHapusLogLama.Enabled = False

            ' Audit log untuk form ini
            Try
                DatabaseHelper.InsertAuditLog(
                    UserSession.UserID,
                    "OPEN_FORM",
                    Nothing,
                    "Audit Log",
                    Nothing,
                    Nothing,
                    Nothing,
                    $"User membuka Audit Log - Role: {UserSession.Role}"
                )
            Catch exAudit As Exception
                Debug.WriteLine("[FormAuditLog.Load] Audit log write failed: " & exAudit.ToString())
            End Try

        Catch ex As Exception
            Debug.WriteLine("[FormAuditLog.Load] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat Audit Log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================= 
    ' LOAD FILTER OPTIONS
    ' ============================================= 

    ' ============================================= 
    ' FORMAT ACTION NAME - Ubah dari UPPERCASE_UNDERSCORE ke Title Case
    ' ============================================= 
    Private Function FormatActionName(action As String) As String
        If String.IsNullOrEmpty(action) Then Return "-"

        ' Mapping untuk action yang sering digunakan
        Dim actionMappings As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
        {"LOGIN", "Login"},
        {"LOGOUT", "Logout"},
        {"OPEN_FORM", "Buka Form"},
        {"CLOSE_FORM", "Tutup Form"},
        {"VIEW_FORM", "Lihat Form"},
        {"OPEN_EDIT_FORM", "Buka Form Edit"},
        {"INSERT_TIMBANGAN_MASUK", "Timbang Masuk"},
        {"UPDATE_TIMBANGAN_SELESAI", "Timbang Keluar"},
        {"EDIT_TIMBANGAN", "Edit Timbangan"},
        {"DELETE_TIMBANGAN", "Hapus Timbangan"},
        {"VIEW_TIMBANGAN", "Lihat Timbangan"},
        {"VIEW_DETAIL_TIMBANGAN", "Lihat Detail Timbangan"},
        {"UPDATE_TIMBANGAN_SELE", "Update Timbangan"},
        {"PRINT_TIKET_MASUK", "Cetak Tiket Masuk"},
        {"PRINT_STRUK_SELESAI", "Cetak Struk"},
        {"PRINT_DUPLICATE", "Cetak Duplikat"},
        {"INSERT_USER", "Tambah User"},
        {"UPDATE_USER", "Edit User"},
        {"DELETE_USER", "Hapus User"},
        {"INSERT_CUSTOMER", "Tambah Customer"},
        {"UPDATE_CUSTOMER", "Edit Customer"},
        {"DELETE_CUSTOMER", "Hapus Customer"},
        {"INSERT_TRANSPORTER", "Tambah Transporter"},
        {"UPDATE_TRANSPORTER", "Edit Transporter"},
        {"DELETE_TRANSPORTER", "Hapus Transporter"},
        {"INSERT_PRODUCT", "Tambah Product"},
        {"UPDATE_PRODUCT", "Edit Product"},
        {"DELETE_PRODUCT", "Hapus Product"},
        {"UPDATE_SETTINGS", "Update Pengaturan"},
        {"SAVE_DB_CONFIG", "Simpan Konfigurasi DB"},
        {"TEST_CONNECTION_FAILED", "Test Koneksi Gagal"},
        {"TEST_DB_CONNECTION_SU", "Test Koneksi DB"},
        {"TEST_MANUAL", "Test Manual"},
        {"EXPORT_DATA", "Export Data"},
        {"EXPORT_PDF", "Export PDF"},
        {"FILTER_DATA", "Filter Data"},
        {"VIEW_REPORT", "Lihat Laporan"},
        {"TOGGLE_STATUS_TRANSPO", "Ubah Status Transporter"},
        {"RESET_PASSWORD", "Reset Password"}
    }

        ' Cek apakah ada di mapping
        If actionMappings.ContainsKey(action.Trim()) Then
            Return actionMappings(action.Trim())
        End If

        ' Jika tidak ada di mapping, format otomatis
        ' Ganti underscore dengan spasi, lalu Title Case
        Dim formatted As String = action.Replace("_", " ").ToLowerInvariant()

        ' Title Case - huruf pertama setiap kata kapital
        Dim words As String() = formatted.Split(" "c)
        For i As Integer = 0 To words.Length - 1
            If words(i).Length > 0 Then
                words(i) = Char.ToUpper(words(i)(0)) & words(i).Substring(1)
            End If
        Next

        Return String.Join(" ", words)
    End Function

    Private Sub LoadFilterOptions()
        Try
            ' Filter Jenis Aktivitas
            cmbJenisAktivitas.Items.Clear()
            cmbJenisAktivitas.Items.Add("-- Semua Aktivitas --")

            Try
                Dim queryActions As String = "SELECT DISTINCT a.Action FROM AuditLog a " &
                                 "LEFT JOIN Users u ON a.UserID = u.UserID " &
                                 "WHERE a.Action IS NOT NULL AND LTRIM(RTRIM(a.Action)) <> '' " &
                                 "ORDER BY a.Action"
                Dim dtActions As DataTable = DatabaseHelper.ExecuteQuery(queryActions)

                ' Gunakan dictionary untuk menyimpan mapping original -> formatted
                ' Dan hindari duplikat setelah formatting
                Dim addedActions As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

                For Each row As DataRow In dtActions.Rows
                    Dim actionOriginal As String = row("Action").ToString().Trim()
                    Dim actionFormatted As String = FormatActionName(actionOriginal)

                    If Not String.IsNullOrEmpty(actionFormatted) AndAlso Not addedActions.Contains(actionFormatted) Then
                        ' Simpan dengan format: "Formatted|Original" untuk bisa digunakan filter
                        cmbJenisAktivitas.Items.Add(actionFormatted)
                        addedActions.Add(actionFormatted)
                    End If
                Next
            Catch ex As Exception
                Debug.WriteLine($"[LoadFilterOptions] Error: {ex.Message}")
            End Try

            cmbJenisAktivitas.SelectedIndex = 0

            ' Filter User - tetap sama
            cmbFilterUser.Items.Clear()
            cmbFilterUser.Items.Add("-- Semua User --")

            Try
                Dim queryUsers As String = "SELECT DISTINCT u.UserID, u.NamaLengkap, u.Username, u.Role " &
                               "FROM AuditLog a " &
                               "INNER JOIN Users u ON a.UserID = u.UserID " &
                               "WHERE u.NamaLengkap IS NOT NULL " &
                               "ORDER BY u.NamaLengkap"
                Dim dtUsers As DataTable = DatabaseHelper.ExecuteQuery(queryUsers)

                For Each row As DataRow In dtUsers.Rows
                    Dim displayText As String = $"{row("NamaLengkap")} ({row("Username")} - {row("Role")})"
                    cmbFilterUser.Items.Add(displayText)
                Next
            Catch ex As Exception
                Debug.WriteLine($"[LoadFilterOptions] Error: {ex.Message}")
            End Try

            cmbFilterUser.SelectedIndex = 0

        Catch ex As Exception
            Debug.WriteLine($"[LoadFilterOptions] Error: {ex.Message}")
        End Try
    End Sub


    ' ============================================= 
    ' LOAD STATISTIK CARDS - BERDASARKAN FILTER TANGGAL
    ' ============================================= 
    Private Sub LoadStatistikCards()
        Try
            Dim filterStart As DateTime = dtpTanggalMulai.Value.Date
            Dim filterEnd As DateTime = dtpTanggalAkhir.Value.Date.AddDays(1)
            Dim prevStart As DateTime = filterStart.AddDays(-1)
            Dim prevEnd As DateTime = filterStart

            ' TOTAL LOG DALAM RANGE
            Dim qTotal As String = "SELECT COUNT(*) FROM AuditLog a LEFT JOIN Users u ON a.UserID = u.UserID WHERE a.CreatedAt >= @Start AND a.CreatedAt < @End "
            Dim totalInRange As Object = DatabaseHelper.ExecuteScalar(qTotal, {
                New SqlParameter("@Start", filterStart),
                New SqlParameter("@End", filterEnd)
            })
            Dim intTotalInRange As Integer = If(totalInRange IsNot Nothing AndAlso Not IsDBNull(totalInRange), CInt(totalInRange), 0)

            Dim totalPrev As Object = DatabaseHelper.ExecuteScalar(qTotal, {
                New SqlParameter("@Start", prevStart),
                New SqlParameter("@End", prevEnd)
            })
            Dim intTotalPrev As Integer = If(totalPrev IsNot Nothing AndAlso Not IsDBNull(totalPrev), CInt(totalPrev), 0)

            lblCardTotalValue.Text = intTotalInRange.ToString("N0")
            UpdateChangeLabel(lblCardTotalChange, intTotalInRange, intTotalPrev)

            ' LOGIN/LOGOUT
            Dim qLogin As String = "SELECT COUNT(*) FROM AuditLog a LEFT JOIN Users u ON a.UserID = u.UserID WHERE a.CreatedAt >= @Start AND a.CreatedAt < @End AND (a.Action LIKE '%LOGIN%' OR a.Action LIKE '%LOGOUT%') "

            Dim loginInRange As Object = DatabaseHelper.ExecuteScalar(qLogin, {
                New SqlParameter("@Start", filterStart),
                New SqlParameter("@End", filterEnd)
            })
            Dim intLoginInRange As Integer = If(loginInRange IsNot Nothing AndAlso Not IsDBNull(loginInRange), CInt(loginInRange), 0)

            Dim loginPrev As Object = DatabaseHelper.ExecuteScalar(qLogin, {
                New SqlParameter("@Start", prevStart),
                New SqlParameter("@End", prevEnd)
            })
            Dim intLoginPrev As Integer = If(loginPrev IsNot Nothing AndAlso Not IsDBNull(loginPrev), CInt(loginPrev), 0)

            lblCardLoginValue.Text = intLoginInRange.ToString("N0")
            UpdateChangeLabel(lblCardLoginChange, intLoginInRange, intLoginPrev)

            ' PERUBAHAN DATA
            Dim qChanges As String = "SELECT COUNT(*) FROM AuditLog a LEFT JOIN Users u ON a.UserID = u.UserID WHERE a.CreatedAt >= @Start AND a.CreatedAt < @End AND (a.Action LIKE '%INSERT%' OR a.Action LIKE '%UPDATE%' OR a.Action LIKE '%DELETE%' OR a.Action LIKE '%SAVE%' OR a.Action LIKE '%SIMPAN%' OR a.Action LIKE '%TAMBAH%' OR a.Action LIKE '%EDIT%' OR a.Action LIKE '%HAPUS%' OR a.Action LIKE '%RESET%') "

            Dim changesInRange As Object = DatabaseHelper.ExecuteScalar(qChanges, {
                New SqlParameter("@Start", filterStart),
                New SqlParameter("@End", filterEnd)
            })
            Dim intChangesInRange As Integer = If(changesInRange IsNot Nothing AndAlso Not IsDBNull(changesInRange), CInt(changesInRange), 0)

            Dim changesPrev As Object = DatabaseHelper.ExecuteScalar(qChanges, {
                New SqlParameter("@Start", prevStart),
                New SqlParameter("@End", prevEnd)
            })
            Dim intChangesPrev As Integer = If(changesPrev IsNot Nothing AndAlso Not IsDBNull(changesPrev), CInt(changesPrev), 0)

            lblCardChangesValue.Text = intChangesInRange.ToString("N0")
            UpdateChangeLabel(lblCardChangesChange, intChangesInRange, intChangesPrev)

            ' ERROR/WARNING
            Dim qErrors As String = "SELECT COUNT(*) FROM AuditLog a LEFT JOIN Users u ON a.UserID = u.UserID WHERE a.CreatedAt >= @Start AND a.CreatedAt < @End AND (a.Action LIKE '%ERROR%' OR a.Action LIKE '%FAIL%' OR a.Action LIKE '%WARNING%' OR a.Action LIKE '%GAGAL%') "

            Dim errorsInRange As Object = DatabaseHelper.ExecuteScalar(qErrors, {
                New SqlParameter("@Start", filterStart),
                New SqlParameter("@End", filterEnd)
            })
            Dim intErrorsInRange As Integer = If(errorsInRange IsNot Nothing AndAlso Not IsDBNull(errorsInRange), CInt(errorsInRange), 0)

            Dim errorsPrev As Object = DatabaseHelper.ExecuteScalar(qErrors, {
                New SqlParameter("@Start", prevStart),
                New SqlParameter("@End", prevEnd)
            })
            Dim intErrorsPrev As Integer = If(errorsPrev IsNot Nothing AndAlso Not IsDBNull(errorsPrev), CInt(errorsPrev), 0)

            lblCardErrorsValue.Text = intErrorsInRange.ToString("N0")
            UpdateChangeLabel(lblCardErrorsChange, intErrorsInRange, intErrorsPrev, True)

        Catch ex As Exception
            Debug.WriteLine($"[LoadStatistikCards] Error: {ex.Message}")
            lblCardTotalValue.Text = "0"
            lblCardLoginValue.Text = "0"
            lblCardChangesValue.Text = "0"
            lblCardErrorsValue.Text = "0"
        End Try
    End Sub

    ' ============================================= 
    ' UPDATE CHANGE LABEL
    ' ============================================= 
    Private Sub UpdateChangeLabel(lbl As Label, valueNow As Integer, valuePrev As Integer, Optional invertColor As Boolean = False)
        Try
            If valuePrev = 0 Then
                If valueNow > 0 Then
                    lbl.Text = "↑ Data baru"
                    lbl.ForeColor = If(invertColor, Color.FromArgb(231, 76, 60), Color.FromArgb(39, 174, 96))
                Else
                    lbl.Text = "- Tidak ada data"
                    lbl.ForeColor = Color.Gray
                End If
                Return
            End If

            Dim change As Double = ((valueNow - valuePrev) / valuePrev) * 100

            If change > 0 Then
                lbl.Text = "↑ " & Math.Abs(change).ToString("F0") & "% dari kemarin"
                lbl.ForeColor = If(invertColor, Color.FromArgb(231, 76, 60), Color.FromArgb(39, 174, 96))
            ElseIf change < 0 Then
                lbl.Text = "↓ " & Math.Abs(change).ToString("F0") & "% dari kemarin"
                lbl.ForeColor = If(invertColor, Color.FromArgb(39, 174, 96), Color.FromArgb(231, 76, 60))
            Else
                lbl.Text = "- Sama dengan kemarin"
                lbl.ForeColor = Color.Gray
            End If

        Catch ex As Exception
            lbl.Text = "-"
            lbl.ForeColor = Color.Gray
        End Try
    End Sub

    ' ============================================= 
    ' LOAD DATA AUDIT LOG - DIPERBAIKI DENGAN OFFSET-FETCH
    ' ============================================= 
    Private Sub LoadAuditLogData()
        Try
            Debug.WriteLine("========================================")
            Debug.WriteLine("=== LoadAuditLogData START ===")

            ' Build WHERE clause
            Dim whereClause As String = "WHERE 1=1 "
            Dim params As New List(Of SqlParameter)
            whereClause &= ""

            ' ==========================================
            ' FILTER TANGGAL - DIPERBAIKI
            ' ==========================================
            Dim filterTanggalMulai As DateTime = dtpTanggalMulai.Value.Date
            Dim filterTanggalAkhir As DateTime = dtpTanggalAkhir.Value.Date.AddDays(1) ' Include seluruh hari terakhir

            whereClause &= "AND a.CreatedAt >= @TanggalMulai AND a.CreatedAt < @TanggalAkhir "
            params.Add(New SqlParameter("@TanggalMulai", filterTanggalMulai))
            params.Add(New SqlParameter("@TanggalAkhir", filterTanggalAkhir))

            Debug.WriteLine($"Filter Tanggal: {filterTanggalMulai:yyyy-MM-dd HH:mm:ss} sampai {filterTanggalAkhir:yyyy-MM-dd HH:mm:ss}")

            ' Filter Jenis Aktivitas
            If cmbJenisAktivitas.SelectedIndex > 0 Then
                Dim selectedAction As String = cmbJenisAktivitas.SelectedItem.ToString()

                ' Cari action original dari database yang cocok dengan formatted name
                Dim queryFindAction As String = "SELECT DISTINCT Action FROM AuditLog WHERE Action IS NOT NULL"
                Dim dtAllActions As DataTable = DatabaseHelper.ExecuteQuery(queryFindAction)

                Dim matchingActions As New List(Of String)
                For Each actionRow As DataRow In dtAllActions.Rows
                    Dim actionOriginal As String = actionRow("Action").ToString().Trim()
                    If FormatActionName(actionOriginal).Equals(selectedAction, StringComparison.OrdinalIgnoreCase) Then
                        matchingActions.Add(actionOriginal)
                    End If
                Next

                If matchingActions.Count > 0 Then
                    ' Buat OR condition untuk semua matching actions
                    Dim orConditions As New List(Of String)
                    For i As Integer = 0 To matchingActions.Count - 1
                        orConditions.Add($"a.Action = @Action{i}")
                        params.Add(New SqlParameter($"@Action{i}", matchingActions(i)))
                    Next
                    whereClause &= $"AND ({String.Join(" OR ", orConditions)}) "
                End If

                Debug.WriteLine($"Filter Action: {selectedAction} -> {String.Join(", ", matchingActions)}")
            End If

            ' Filter User
            If cmbFilterUser.SelectedIndex > 0 Then
                Dim selectedUserText As String = cmbFilterUser.SelectedItem.ToString()
                Dim filterUser As String = selectedUserText.Split("("c)(0).Trim()
                whereClause &= "AND u.NamaLengkap = @NamaLengkap "
                params.Add(New SqlParameter("@NamaLengkap", filterUser))
                Debug.WriteLine($"Filter User: {filterUser}")
            End If

            ' Filter Pencarian
            If Not String.IsNullOrWhiteSpace(txtPencarian.Text) AndAlso
               txtPencarian.Text <> "Cari deskripsi, IP, module..." Then
                whereClause &= "AND (a.Keterangan LIKE @Search OR a.TableName LIKE @Search OR a.Action LIKE @Search OR a.IPAddress LIKE @Search) "
                params.Add(New SqlParameter("@Search", "%" & txtPencarian.Text.Trim() & "%"))
                Debug.WriteLine($"Filter Search: {txtPencarian.Text.Trim()}")
            End If

            ' ==========================================
            ' QUERY COUNT - Untuk Pagination
            ' ==========================================
            Dim countQuery As String = "SELECT COUNT(*) FROM AuditLog a " &
                                       "LEFT JOIN Users u ON a.UserID = u.UserID " &
                                       whereClause

            Debug.WriteLine($"Count Query: {countQuery}")

            ' Clone parameters untuk count query
            Dim countParams As New List(Of SqlParameter)
            For Each p In params
                countParams.Add(New SqlParameter(p.ParameterName, p.Value))
            Next

            Dim resultCount As Object = DatabaseHelper.ExecuteScalar(countQuery, countParams.ToArray())
            TotalRecords = If(resultCount IsNot Nothing AndAlso Not IsDBNull(resultCount), CInt(resultCount), 0)
            TotalPages = If(TotalRecords > 0, CInt(Math.Ceiling(TotalRecords / PageSize)), 1)

            If CurrentPage > TotalPages Then CurrentPage = 1
            If CurrentPage < 1 Then CurrentPage = 1

            Debug.WriteLine($"Total Records: {TotalRecords}, Total Pages: {TotalPages}, Current Page: {CurrentPage}")

            ' Update label record count
            Dim startRecord As Integer = If(TotalRecords > 0, ((CurrentPage - 1) * PageSize) + 1, 0)
            Dim endRecord As Integer = Math.Min(CurrentPage * PageSize, TotalRecords)

            If lblRecordCount IsNot Nothing Then
                lblRecordCount.Text = $"Menampilkan {startRecord}-{endRecord} dari {TotalRecords:N0} data"
            End If

            If lblPaginationInfo IsNot Nothing Then
                lblPaginationInfo.Text = $"Menampilkan {startRecord}-{endRecord} dari {TotalRecords:N0} data"
            End If

            ' ==========================================
            ' QUERY DATA - DENGAN OFFSET FETCH
            ' ==========================================
            Dim offset As Integer = (CurrentPage - 1) * PageSize

            Dim dataQuery As String = "SELECT " &
                                      "a.LogID, " &
                                      "a.CreatedAt, " &
                                      "ISNULL(u.NamaLengkap, 'System') AS NamaLengkap, " &
                                      "ISNULL(u.Username, 'system') AS Username, " &
                                      "ISNULL(u.Role, 'SYSTEM') AS UserRole, " &
                                      "ISNULL(a.Action, '-') AS Action, " &
                                      "ISNULL(a.TableName, '-') AS TableName, " &
                                      "ISNULL(a.Keterangan, '-') AS Keterangan, " &
                                      "ISNULL(a.IPAddress, '-') AS IPAddress " &
                                      "FROM AuditLog a " &
                                      "LEFT JOIN Users u ON a.UserID = u.UserID " &
                                      whereClause &
                                      "ORDER BY a.CreatedAt DESC " &
                                      "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"

            Debug.WriteLine($"Data Query: {dataQuery}")

            ' Clone parameters untuk data query dan tambahkan offset/pagesize
            Dim dataParams As New List(Of SqlParameter)
            For Each p In params
                dataParams.Add(New SqlParameter(p.ParameterName, p.Value))
            Next
            dataParams.Add(New SqlParameter("@Offset", offset))
            dataParams.Add(New SqlParameter("@PageSize", PageSize))

            Dim dtData As DataTable = DatabaseHelper.ExecuteQuery(dataQuery, dataParams.ToArray())

            Debug.WriteLine($"Rows returned from query: {dtData.Rows.Count}")

            ' ==========================================
            ' POPULATE DATAGRIDVIEW
            ' ==========================================
            dgvAuditLog.Rows.Clear()

            If dtData.Rows.Count = 0 Then
                Debug.WriteLine("WARNING: No data returned from query!")
                If lblRecordCount IsNot Nothing Then
                    lblRecordCount.Text = $"Tidak ada data untuk periode {filterTanggalMulai:dd/MM/yyyy} - {dtpTanggalAkhir.Value:dd/MM/yyyy}"
                End If
                UpdatePaginationButtons()
                Return
            End If

            Dim rowNum As Integer = startRecord

            For Each row As DataRow In dtData.Rows
                Try
                    Dim waktu As DateTime = Convert.ToDateTime(row("CreatedAt"))
                    Dim waktuStr As String = waktu.ToString("dd/MM/yyyy") & vbCrLf & waktu.ToString("HH:mm:ss")

                    Dim namaLengkap As String = row("NamaLengkap").ToString()
                    Dim username As String = row("Username").ToString()
                    Dim userRole As String = row("UserRole").ToString()
                    Dim userStr As String = namaLengkap & vbCrLf & "(" & username & " - " & userRole & ")"

                    Dim action As String = row("Action").ToString()
                    Dim aktivitasShort As String = GetActivityShortName(action)

                    Dim moduleName As String = row("TableName").ToString()

                    Dim keterangan As String = row("Keterangan").ToString()
                    If keterangan.Length > 100 Then
                        keterangan = keterangan.Substring(0, 97) & "..."
                    End If

                    Dim ipAddress As String = row("IPAddress").ToString()

                    Dim rowIndex As Integer = dgvAuditLog.Rows.Add(
                        rowNum.ToString(),
                        waktuStr,
                        userStr,
                        aktivitasShort,
                        moduleName,
                        keterangan,
                        ipAddress,
                        "Detail"
                    )

                    dgvAuditLog.Rows(rowIndex).Tag = row("LogID").ToString()
                    rowNum += 1

                Catch exRow As Exception
                    Debug.WriteLine($"ERROR processing row: {exRow.Message}")
                End Try
            Next

            Debug.WriteLine($"Total rows in DataGridView: {dgvAuditLog.Rows.Count}")
            Debug.WriteLine("=== LoadAuditLogData END ===")

            UpdatePaginationButtons()

        Catch ex As Exception
            Debug.WriteLine("[FormAuditLog.LoadAuditLogData] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat data Audit Log.",
                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"FATAL ERROR: {ex.Message}")
        End Try
    End Sub

    ' ============================================= 
    ' GET ACTIVITY SHORT NAME - DIPERLUAS
    ' ============================================= 
    Private Function GetActivityShortName(action As String) As String
        If String.IsNullOrEmpty(action) Then Return "-"

        action = action.ToUpperInvariant()

        ' LOGIN/LOGOUT
        If action = "LOGIN" Then Return "Login"
        If action = "LOGOUT" Then Return "Logout"
        If action.Contains("LOGIN_FAILED") Then Return "Login Gagal"

        ' FORM
        If action = "OPEN_FORM" OrElse action = "VIEW_FORM" Then Return "Buka Form"
        If action = "CLOSE_FORM" Then Return "Tutup Form"

        ' TIMBANGAN
        If action.Contains("INSERT_TIMBANGAN_MASUK") OrElse action.Contains("TIMBANG_MASUK") Then Return "Timbang Masuk"
        If action.Contains("UPDATE_TIMBANGAN_SELESAI") OrElse action.Contains("TIMBANG_KELUAR") Then Return "Timbang Keluar"
        If action.Contains("EDIT_TIMBANGAN") Then Return "Edit Timbangan"
        If action.Contains("DELETE_TIMBANGAN") Then Return "Hapus Timbangan"
        If action.Contains("VIEW_TIMBANGAN") OrElse action.Contains("VIEW_DETAIL_TIMBANGAN") Then Return "Lihat Timbangan"

        ' PRINT
        If action.Contains("PRINT_TIKET_MASUK") Then Return "Cetak Tiket Masuk"
        If action.Contains("PRINT_STRUK_SELESAI") Then Return "Cetak Struk"
        If action.Contains("PRINT_DUPLICATE") OrElse action.Contains("PRINT_DUPLIKAT") Then Return "Cetak Ulang"
        If action.Contains("PRINT") OrElse action.Contains("CETAK") Then Return "Cetak"

        ' MASTER DATA
        If action.Contains("INSERT_CUSTOMER") OrElse action.Contains("TAMBAH_CUSTOMER") Then Return "Tambah Customer"
        If action.Contains("UPDATE_CUSTOMER") OrElse action.Contains("EDIT_CUSTOMER") Then Return "Edit Customer"
        If action.Contains("DELETE_CUSTOMER") OrElse action.Contains("HAPUS_CUSTOMER") Then Return "Hapus Customer"

        If action.Contains("INSERT_TRANSPORTER") OrElse action.Contains("TAMBAH_TRANSPORTER") Then Return "Tambah Transporter"
        If action.Contains("UPDATE_TRANSPORTER") OrElse action.Contains("EDIT_TRANSPORTER") Then Return "Edit Transporter"
        If action.Contains("DELETE_TRANSPORTER") OrElse action.Contains("HAPUS_TRANSPORTER") Then Return "Hapus Transporter"

        If action.Contains("INSERT_PRODUCT") OrElse action.Contains("TAMBAH_PRODUCT") Then Return "Tambah Product"
        If action.Contains("UPDATE_PRODUCT") OrElse action.Contains("EDIT_PRODUCT") Then Return "Edit Product"
        If action.Contains("DELETE_PRODUCT") OrElse action.Contains("HAPUS_PRODUCT") Then Return "Hapus Product"

        ' USER MANAGEMENT
        If action.Contains("INSERT_USER") OrElse action.Contains("TAMBAH_USER") Then Return "Tambah User"
        If action.Contains("UPDATE_USER") OrElse action.Contains("EDIT_USER") Then Return "Edit User"
        If action.Contains("DELETE_USER") OrElse action.Contains("HAPUS_USER") Then Return "Hapus User"
        If action.Contains("RESET_PASSWORD") Then Return "Reset Password"
        If action.Contains("CHANGE_PASSWORD") OrElse action.Contains("UBAH_PASSWORD") Then Return "Ubah Password"
        If action.Contains("TOGGLE_STATUS") Then Return "Ubah Status User"

        ' SETTINGS
        If action.Contains("Update Pengaturan") OrElse action.Contains("SAVE_SETTINGS") Then Return "Simpan Pengaturan"
        If action.Contains("RESET_SETTINGS") Then Return "Reset Pengaturan"
        If action.Contains("RESET_COUNTER") Then Return "Reset Counter"
        If action.Contains("TEST_CONNECTION") OrElse action.Contains("TEST_KONEKSI") Then Return "Test Koneksi"

        ' LAPORAN
        If action.Contains("EXPORT_EXCEL") OrElse action.Contains("EXPORT_CSV") Then Return "Export Excel"
        If action.Contains("EXPORT_PDF") Then Return "Export PDF"
        If action.Contains("VIEW_REPORT") OrElse action.Contains("VIEW_LAPORAN") Then Return "Lihat Laporan"
        If action.Contains("FILTER_DATA") Then Return "Filter Data"

        ' GENERIC CRUD
        If action.Contains("INSERT") OrElse action.Contains("CREATE") OrElse action.Contains("ADD") OrElse action.Contains("TAMBAH") Then Return "Tambah"
        If action.Contains("UPDATE") OrElse action.Contains("EDIT") OrElse action.Contains("MODIFY") OrElse action.Contains("UBAH") Then Return "Edit Timbangan"
        If action.Contains("DELETE") OrElse action.Contains("REMOVE") OrElse action.Contains("HAPUS") Then Return "Hapus"
        If action.Contains("VIEW") OrElse action.Contains("READ") OrElse action.Contains("LIHAT") Then Return "Lihat"

        ' ERROR
        If action.Contains("ERROR") OrElse action.Contains("FAIL") OrElse action.Contains("GAGAL") Then Return "Error"

        ' TEST
        If action.Contains("TEST") Then Return "Test"

        Return action
    End Function

    ' ============================================= 
    ' UPDATE PAGINATION BUTTONS
    ' ============================================= 
    Private Sub UpdatePaginationButtons()
        Try
            btnPrevPage.Enabled = (CurrentPage > 1)
            btnNextPage.Enabled = (CurrentPage < TotalPages)

            Dim startPage As Integer = Math.Max(1, CurrentPage - 2)
            Dim endPage As Integer = Math.Min(TotalPages, startPage + 4)

            If endPage - startPage < 4 Then
                startPage = Math.Max(1, endPage - 4)
            End If

            Dim pageButtons As Button() = {btnPage1, btnPage2, btnPage3, btnPage4, btnPage5}

            For i As Integer = 0 To 4
                Dim pageNum As Integer = startPage + i
                If pageNum <= TotalPages Then
                    pageButtons(i).Text = pageNum.ToString()
                    pageButtons(i).Visible = True

                    If pageNum = CurrentPage Then
                        pageButtons(i).BackColor = Color.FromArgb(41, 128, 185)
                        pageButtons(i).ForeColor = Color.White
                        pageButtons(i).Font = New Font(pageButtons(i).Font, FontStyle.Bold)
                    Else
                        pageButtons(i).BackColor = Color.White
                        pageButtons(i).ForeColor = Color.FromArgb(102, 102, 102)
                        pageButtons(i).Font = New Font(pageButtons(i).Font, FontStyle.Regular)
                    End If
                Else
                    pageButtons(i).Visible = False
                End If
            Next

        Catch ex As Exception
            Debug.WriteLine($"[UpdatePaginationButtons] Error: {ex.Message}")
        End Try
    End Sub

    ' ============================================= 
    ' PAGINATION CLICK EVENTS
    ' ============================================= 
    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        If CurrentPage > 1 Then
            CurrentPage -= 1
            LoadAuditLogData()
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        If CurrentPage < TotalPages Then
            CurrentPage += 1
            LoadAuditLogData()
        End If
    End Sub

    Private Sub btnPage1_Click(sender As Object, e As EventArgs) Handles btnPage1.Click
        CurrentPage = CInt(btnPage1.Text)
        LoadAuditLogData()
    End Sub

    Private Sub btnPage2_Click(sender As Object, e As EventArgs) Handles btnPage2.Click
        CurrentPage = CInt(btnPage2.Text)
        LoadAuditLogData()
    End Sub

    Private Sub btnPage3_Click(sender As Object, e As EventArgs) Handles btnPage3.Click
        CurrentPage = CInt(btnPage3.Text)
        LoadAuditLogData()
    End Sub

    Private Sub btnPage4_Click(sender As Object, e As EventArgs) Handles btnPage4.Click
        CurrentPage = CInt(btnPage4.Text)
        LoadAuditLogData()
    End Sub

    Private Sub btnPage5_Click(sender As Object, e As EventArgs) Handles btnPage5.Click
        CurrentPage = CInt(btnPage5.Text)
        LoadAuditLogData()
    End Sub

    ' ============================================= 
    ' FILTER - TERAPKAN FILTER
    ' ============================================= 
    Private Sub btnTerapkanFilter_Click(sender As Object, e As EventArgs) Handles btnTerapkanFilter.Click
        ' Validasi tanggal
        If dtpTanggalMulai.Value.Date > dtpTanggalAkhir.Value.Date Then
            MessageBox.Show("Tanggal Mulai tidak boleh lebih besar dari Tanggal Akhir!", "Validasi",
                           MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        CurrentPage = 1
        LoadAuditLogData()
        LoadStatistikCards()
    End Sub

    ' ============================================= 
    ' FILTER - RESET FILTER
    ' ============================================= 
    Private Sub btnResetFilter_Click(sender As Object, e As EventArgs) Handles btnResetFilter.Click
        dtpTanggalMulai.Value = DateTime.Today.AddDays(-30)
        dtpTanggalAkhir.Value = DateTime.Today
        cmbJenisAktivitas.SelectedIndex = 0
        cmbFilterUser.SelectedIndex = 0
        txtPencarian.Text = "Cari deskripsi, IP, module..."

        CurrentPage = 1
        LoadAuditLogData()
        LoadStatistikCards()
    End Sub

    ' ============================================= 
    ' REFRESH DATA
    ' ============================================= 
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadStatistikCards()
        LoadAuditLogData()
        LoadFilterOptions()
        MessageBox.Show("✅ Data berhasil diperbarui!", "Refresh",
                       MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ============================================= 
    ' TOMBOL: EXPORT EXCEL (AUDIT LOG)
    ' ============================================= 
    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Try
            If dgvAuditLog.Rows.Count = 0 Then
                MessageBox.Show("Tidak ada data untuk diekspor!", "Info",
                           MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv"
                sfd.FileName = $"AuditLog_{DateTime.Now:yyyyMMdd_HHmmss}"
                sfd.Title = "Export Audit Log"

                If sfd.ShowDialog() = DialogResult.OK Then
                    Me.Cursor = Cursors.WaitCursor

                    Try
                        If sfd.FileName.EndsWith(".xlsx") Then
                            ' Export menggunakan Excel Interop (jika tersedia)
                            ExportAuditLogToExcel(sfd.FileName)
                        Else
                            ' Export ke CSV
                            ExportAuditLogToCSV(sfd.FileName)
                        End If

                        Me.Cursor = Cursors.Default

                        ' Audit Log
                        DatabaseHelper.InsertAuditLog(
                        UserSession.UserID,
                        "EXPORT_AUDIT_LOG",
                        Nothing,
                        "AuditLog",
                        Nothing,
                        Nothing,
                        Nothing,
                        $"Export Audit Log - File: {IO.Path.GetFileName(sfd.FileName)}, Total: {dgvAuditLog.Rows.Count} record"
                    )

                        MessageBox.Show("✅ Export berhasil!", "Sukses",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If MessageBox.Show("Buka file sekarang?", "Konfirmasi",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Try
                                Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                            Catch
                                Process.Start(sfd.FileName)
                            End Try
                        End If

                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        Debug.WriteLine("[FormAuditLog.Export] Error: " & ex.ToString())
                        MessageBox.Show("Gagal mengekspor data audit log.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Debug.WriteLine("[FormAuditLog.ExportPdf] Error: " & ex.ToString())
            MessageBox.Show("Gagal mengekspor data audit log ke PDF.", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================= 
    ' EXPORT AUDIT LOG TO EXCEL (XLSX)
    ' Menggunakan Excel Interop - Lebih bagus formatnya
    ' ============================================= 
    Private Sub ExportAuditLogToExcel(filePath As String)
        Dim excelApp As Object = Nothing
        Dim workbook As Object = Nothing
        Dim worksheet As Object = Nothing

        Try
            ' Coba buat Excel Application
            Try
                excelApp = CreateObject("Excel.Application")
            Catch
                ' Jika Excel tidak terinstall, fallback ke CSV
                MessageBox.Show("Microsoft Excel tidak terdeteksi di komputer ini." & vbCrLf &
                           "Export akan menggunakan format CSV sebagai gantinya.",
                           "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ExportAuditLogToCSV(filePath.Replace(".xlsx", ".csv"))
                Return
            End Try

            excelApp.Visible = False
            excelApp.DisplayAlerts = False

            workbook = excelApp.Workbooks.Add()
            worksheet = workbook.Sheets(1)
            worksheet.Name = "Audit Log"

            ' ============================================= 
            ' HEADER - INFO EXPORT
            ' ============================================= 
            worksheet.Cells(1, 1).Value = "LAPORAN AUDIT LOG"
            worksheet.Cells(1, 1).Font.Bold = True
            worksheet.Cells(1, 1).Font.Size = 16
            worksheet.Range("A1:G1").Merge()
            worksheet.Range("A1").Interior.Color = RGB(41, 128, 185)
            worksheet.Range("A1").Font.Color = RGB(255, 255, 255)
            worksheet.Range("A1").HorizontalAlignment = -4108 ' xlCenter
            worksheet.Rows(1).RowHeight = 30

            ' Info export
            worksheet.Cells(2, 1).Value = "Periode"
            worksheet.Cells(2, 2).Value = $"{dtpTanggalMulai.Value:dd/MM/yyyy} - {dtpTanggalAkhir.Value:dd/MM/yyyy}"
            worksheet.Range("B2:C2").Merge()

            worksheet.Cells(3, 1).Value = "Diekspor oleh"
            worksheet.Cells(3, 2).Value = UserSession.NamaLengkap
            worksheet.Range("B3:C3").Merge()

            worksheet.Cells(4, 1).Value = "Tanggal Export"
            worksheet.Cells(4, 2).Value = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss")
            worksheet.Range("B4:C4").Merge()

            worksheet.Cells(5, 1).Value = "Total Record"
            worksheet.Cells(5, 2).Value = dgvAuditLog.Rows.Count.ToString("N0")
            worksheet.Range("B5:C5").Merge()

            ' Style info rows
            For i As Integer = 2 To 5
                worksheet.Cells(i, 1).Font.Bold = True
                worksheet.Cells(i, 1).Interior.Color = RGB(240, 240, 240)
                worksheet.Cells(i, 1).HorizontalAlignment = -4131 ' xlLeft
            Next

            ' ============================================= 
            ' TABLE HEADER
            ' ============================================= 
            Dim headerRow As Integer = 7
            Dim headers() As String = {"No", "Waktu", "User", "Aktivitas", "Module", "Deskripsi", "IP Address"}

            For col As Integer = 1 To headers.Length
                worksheet.Cells(headerRow, col).Value = headers(col - 1)
                worksheet.Cells(headerRow, col).Font.Bold = True
                worksheet.Cells(headerRow, col).Interior.Color = RGB(52, 73, 94)
                worksheet.Cells(headerRow, col).Font.Color = RGB(255, 255, 255)
                worksheet.Cells(headerRow, col).HorizontalAlignment = -4108 ' xlCenter
                worksheet.Cells(headerRow, col).VerticalAlignment = -4108
            Next
            worksheet.Rows(headerRow).RowHeight = 25

            ' ============================================= 
            ' TABLE DATA
            ' ============================================= 
            Dim dataRow As Integer = headerRow + 1

            For Each dgvRow As DataGridViewRow In dgvAuditLog.Rows
                If dgvRow.IsNewRow Then Continue For

                ' No
                worksheet.Cells(dataRow, 1).Value = dgvRow.Cells("colNo").Value?.ToString()
                worksheet.Cells(dataRow, 1).HorizontalAlignment = -4108 ' xlCenter

                ' Waktu (replace vbCrLf dengan space)
                Dim waktu As String = dgvRow.Cells("colWaktu").Value?.ToString().Replace(vbCrLf, " ")
                worksheet.Cells(dataRow, 2).Value = waktu
                worksheet.Cells(dataRow, 2).HorizontalAlignment = -4108

                ' User (replace vbCrLf dengan space)
                Dim user As String = dgvRow.Cells("colUser").Value?.ToString().Replace(vbCrLf, " ")
                worksheet.Cells(dataRow, 3).Value = user

                ' Aktivitas
                worksheet.Cells(dataRow, 4).Value = dgvRow.Cells("colAktivitas").Value?.ToString()
                worksheet.Cells(dataRow, 4).HorizontalAlignment = -4108

                ' Module
                worksheet.Cells(dataRow, 5).Value = dgvRow.Cells("colModule").Value?.ToString()
                worksheet.Cells(dataRow, 5).HorizontalAlignment = -4108

                ' Deskripsi
                worksheet.Cells(dataRow, 6).Value = dgvRow.Cells("colDeskripsi").Value?.ToString()

                ' IP Address
                worksheet.Cells(dataRow, 7).Value = dgvRow.Cells("colIPAddress").Value?.ToString()
                worksheet.Cells(dataRow, 7).HorizontalAlignment = -4108

                ' Row height
                worksheet.Rows(dataRow).RowHeight = 20

                dataRow += 1
            Next

            ' ============================================= 
            ' FORMATTING
            ' ============================================= 
            ' Auto fit columns
            worksheet.Columns("A:G").AutoFit()

            ' Set minimum width
            If worksheet.Columns("A").ColumnWidth < 8 Then worksheet.Columns("A").ColumnWidth = 8
            If worksheet.Columns("B").ColumnWidth < 18 Then worksheet.Columns("B").ColumnWidth = 18
            If worksheet.Columns("C").ColumnWidth < 25 Then worksheet.Columns("C").ColumnWidth = 25
            If worksheet.Columns("D").ColumnWidth < 15 Then worksheet.Columns("D").ColumnWidth = 15
            If worksheet.Columns("E").ColumnWidth < 15 Then worksheet.Columns("E").ColumnWidth = 15
            If worksheet.Columns("F").ColumnWidth < 50 Then worksheet.Columns("F").ColumnWidth = 50
            If worksheet.Columns("G").ColumnWidth < 15 Then worksheet.Columns("G").ColumnWidth = 15

            ' Border untuk tabel
            Dim tableRange = worksheet.Range(worksheet.Cells(headerRow, 1), worksheet.Cells(dataRow - 1, 7))
            tableRange.Borders.LineStyle = 1 ' xlContinuous
            tableRange.Borders.Weight = 2 ' xlThin

            ' Freeze panes (freeze header)
            worksheet.Rows(headerRow + 1).Select()
            excelApp.ActiveWindow.FreezePanes = True

            ' ============================================= 
            ' SAVE
            ' ============================================= 
            workbook.SaveAs(filePath, 51) ' 51 = xlOpenXMLWorkbook (.xlsx)

        Catch ex As Exception
            Debug.WriteLine("[FormAuditLog.ExportExcel] Error: " & ex.ToString())
            Throw New Exception("Gagal mengekspor data ke Excel.", ex)
        Finally
            ' Cleanup
            If workbook IsNot Nothing Then
                workbook.Close(False)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook)
            End If
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    ' ============================================= 
    ' EXPORT AUDIT LOG TO CSV
    ' Fallback jika Excel tidak tersedia
    ' ============================================= 
    Private Sub ExportAuditLogToCSV(filePath As String)
        Try
            Using writer As New IO.StreamWriter(filePath, False, System.Text.Encoding.UTF8)
                ' BOM untuk UTF-8
                writer.Write(Chr(&HEF) & Chr(&HBB) & Chr(&HBF))

                ' Header Info
                writer.WriteLine("LAPORAN AUDIT LOG")
                writer.WriteLine($"Periode;{dtpTanggalMulai.Value:dd/MM/yyyy} - {dtpTanggalAkhir.Value:dd/MM/yyyy}")
                writer.WriteLine($"Diekspor oleh;{UserSession.NamaLengkap}")
                writer.WriteLine($"Tanggal Export;{DateTime.Now:dd MMMM yyyy HH:mm:ss}")
                writer.WriteLine($"Total Record;{dgvAuditLog.Rows.Count:N0}")
                writer.WriteLine()

                ' Header tabel
                writer.WriteLine("No;Waktu;User;Aktivitas;Module;Deskripsi;IP Address")

                ' Data
                For Each row As DataGridViewRow In dgvAuditLog.Rows
                    If row.IsNewRow Then Continue For

                    Dim no As String = row.Cells("colNo").Value?.ToString()
                    Dim waktu As String = row.Cells("colWaktu").Value?.ToString().Replace(vbCrLf, " ").Replace(";", ",")
                    Dim user As String = row.Cells("colUser").Value?.ToString().Replace(vbCrLf, " - ").Replace(";", ",")
                    Dim aktivitas As String = row.Cells("colAktivitas").Value?.ToString().Replace(";", ",")
                    Dim moduleName As String = row.Cells("colModule").Value?.ToString().Replace(";", ",")
                    Dim deskripsi As String = row.Cells("colDeskripsi").Value?.ToString().Replace(";", ",").Replace(vbCrLf, " ")
                    Dim ipAddress As String = row.Cells("colIPAddress").Value?.ToString().Replace(";", ",")

                    writer.WriteLine($"{no};{waktu};{user};{aktivitas};{moduleName};{deskripsi};{ipAddress}")
                Next
            End Using

        Catch ex As Exception
            Debug.WriteLine("[FormAuditLog.ExportCsv] Error: " & ex.ToString())
            Throw New Exception("Gagal mengekspor data ke CSV.", ex)
        End Try
    End Sub




    ' ============================================= 
    ' CELL FORMATTING - WARNA BADGE
    ' ============================================= 
    Private Sub dgvAuditLog_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvAuditLog.CellFormatting
        Try
            If e.RowIndex < 0 Then Return
            If dgvAuditLog.Columns.Count <= e.ColumnIndex Then Return
            If dgvAuditLog.Columns(e.ColumnIndex).Name <> "colAktivitas" Then Return
            If e.Value Is Nothing Then Return

            Dim aktivitas As String = e.Value.ToString().ToLowerInvariant()

            Select Case True
                Case aktivitas.Contains("login") AndAlso Not aktivitas.Contains("gagal")
                    e.CellStyle.BackColor = Color.FromArgb(232, 244, 253)
                    e.CellStyle.ForeColor = Color.FromArgb(41, 128, 185)

                Case aktivitas.Contains("logout")
                    e.CellStyle.BackColor = Color.FromArgb(245, 238, 248)
                    e.CellStyle.ForeColor = Color.FromArgb(142, 68, 173)

                Case aktivitas.Contains("tambah") OrElse aktivitas.Contains("masuk") OrElse aktivitas.Contains("simpan")
                    e.CellStyle.BackColor = Color.FromArgb(232, 248, 245)
                    e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96)

                Case aktivitas.Contains("ubah") OrElse aktivitas.Contains("edit") OrElse aktivitas.Contains("keluar")
                    e.CellStyle.BackColor = Color.FromArgb(254, 249, 231)
                    e.CellStyle.ForeColor = Color.FromArgb(212, 168, 37)

                Case aktivitas.Contains("hapus")
                    e.CellStyle.BackColor = Color.FromArgb(253, 237, 236)
                    e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60)

                Case aktivitas.Contains("lihat") OrElse aktivitas.Contains("buka") OrElse aktivitas.Contains("tutup")
                    e.CellStyle.BackColor = Color.FromArgb(234, 236, 238)
                    e.CellStyle.ForeColor = Color.FromArgb(86, 101, 115)

                Case aktivitas.Contains("cetak") OrElse aktivitas.Contains("export")
                    e.CellStyle.BackColor = Color.FromArgb(232, 246, 243)
                    e.CellStyle.ForeColor = Color.FromArgb(22, 160, 133)

                Case aktivitas.Contains("error") OrElse aktivitas.Contains("gagal")
                    e.CellStyle.BackColor = Color.FromArgb(249, 235, 234)
                    e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43)

                Case aktivitas.Contains("test") OrElse aktivitas.Contains("reset")
                    e.CellStyle.BackColor = Color.FromArgb(255, 250, 205)
                    e.CellStyle.ForeColor = Color.FromArgb(255, 140, 0)
            End Select

            e.CellStyle.Font = New Font("Segoe UI", 8, FontStyle.Bold)
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Catch ex As Exception
            Debug.WriteLine($"[CellFormatting] Error: {ex.Message}")
        End Try
    End Sub

    ' ============================================= 
    ' CELL CLICK - TOMBOL DETAIL
    ' ============================================= 
    Private Sub dgvAuditLog_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAuditLog.CellClick
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                If dgvAuditLog.Columns(e.ColumnIndex).Name = "colAksi" Then
                    Dim logID As String = dgvAuditLog.Rows(e.RowIndex).Tag?.ToString()
                    If Not String.IsNullOrEmpty(logID) Then
                        ShowDetailLog(logID)
                    End If
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine($"[CellClick] Error: {ex.Message}")
        End Try
    End Sub

    ' ============================================= 
    ' SHOW DETAIL LOG - FORMAT TABEL PERBANDINGAN
    ' ============================================= 
    ' ============================================= 
    ' SHOW DETAIL LOG - FORMAT TABEL PERBANDINGAN
    ' HANYA TAMPILKAN FIELD YANG BERUBAH
    ' ============================================= 
    Private Sub ShowDetailLog(logID As String)
        Try
            Dim query As String = "SELECT a.*, u.NamaLengkap, u.Username, u.Role " &
                          "FROM AuditLog a " &
                          "LEFT JOIN Users u ON a.UserID = u.UserID " &
                          "WHERE a.LogID = @LogID"

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {New SqlParameter("@LogID", CInt(logID))})

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                ' ============================================= 
                ' BUAT FORM DETAIL DENGAN TABEL
                ' ============================================= 
                Dim frmDetail As New Form()
                frmDetail.Text = "Detail Audit Log"
                frmDetail.Size = New Size(950, 700)
                frmDetail.MinimumSize = New Size(800, 500)
                frmDetail.StartPosition = FormStartPosition.CenterParent
                frmDetail.Font = New Font("Segoe UI", 9)
                frmDetail.BackColor = Color.FromArgb(248, 249, 250)

                ' ============================================= 
                ' HEADER PANEL
                ' ============================================= 
                Dim pnlHeader As New Panel()
                pnlHeader.Dock = DockStyle.Top
                pnlHeader.Height = 90
                pnlHeader.BackColor = Color.FromArgb(41, 128, 185)
                pnlHeader.Padding = New Padding(20, 10, 20, 10)

                Dim lblTitle As New Label()
                lblTitle.Text = "📋 DETAIL AUDIT LOG"
                lblTitle.Font = New Font("Segoe UI", 14, FontStyle.Bold)
                lblTitle.ForeColor = Color.White
                lblTitle.AutoSize = True
                lblTitle.Location = New Point(20, 12)

                Dim lblLogID As New Label()
                lblLogID.Text = $"#LOG-{CInt(logID):D10}"
                lblLogID.Font = New Font("Segoe UI", 10)
                lblLogID.ForeColor = Color.FromArgb(200, 230, 255)
                lblLogID.AutoSize = True
                lblLogID.Location = New Point(20, 42)

                Dim lblInfo As New Label()
                Dim waktu As DateTime = Convert.ToDateTime(row("CreatedAt"))
                Dim userName As String = If(IsDBNull(row("NamaLengkap")), "System", row("NamaLengkap").ToString())
                Dim action As String = If(IsDBNull(row("Action")), "-", row("Action").ToString())
                lblInfo.Text = $"Waktu: {waktu:dd MMM yyyy, HH:mm:ss} | User: {userName} | Aktivitas : {action}"
                lblInfo.Font = New Font("Segoe UI", 9)
                lblInfo.ForeColor = Color.FromArgb(200, 230, 255)
                lblInfo.AutoSize = True
                lblInfo.Location = New Point(20, 65)

                pnlHeader.Controls.AddRange({lblTitle, lblLogID, lblInfo})

                ' ============================================= 
                ' KETERANGAN PANEL
                ' ============================================= 
                Dim pnlKeterangan As New Panel()
                pnlKeterangan.Dock = DockStyle.Top
                pnlKeterangan.Height = 60
                pnlKeterangan.BackColor = Color.FromArgb(255, 249, 230)
                pnlKeterangan.Padding = New Padding(20, 10, 20, 10)

                Dim lblKetTitle As New Label()
                lblKetTitle.Text = "📝 Keterangan:"
                lblKetTitle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                lblKetTitle.ForeColor = Color.FromArgb(90, 74, 26)
                lblKetTitle.AutoSize = True
                lblKetTitle.Location = New Point(20, 8)

                Dim lblKetValue As New Label()
                lblKetValue.Text = If(IsDBNull(row("Keterangan")), "-", row("Keterangan").ToString())
                lblKetValue.Font = New Font("Segoe UI", 9)
                lblKetValue.ForeColor = Color.FromArgb(60, 50, 20)
                lblKetValue.AutoSize = False
                lblKetValue.Location = New Point(20, 30)
                lblKetValue.Size = New Size(880, 25)

                pnlKeterangan.Controls.AddRange({lblKetTitle, lblKetValue})

                ' ============================================= 
                ' DATAGRIDVIEW UNTUK TABEL PERBANDINGAN
                ' ============================================= 
                Dim dgvDetail As New DataGridView()
                dgvDetail.Dock = DockStyle.Fill
                dgvDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                dgvDetail.AllowUserToAddRows = False
                dgvDetail.AllowUserToDeleteRows = False
                dgvDetail.ReadOnly = True
                dgvDetail.RowHeadersVisible = False
                dgvDetail.BackgroundColor = Color.White
                dgvDetail.BorderStyle = BorderStyle.None
                dgvDetail.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                dgvDetail.GridColor = Color.FromArgb(230, 230, 230)
                dgvDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                dgvDetail.RowTemplate.Height = 35
                dgvDetail.EnableHeadersVisualStyles = False

                ' Header Style
                dgvDetail.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94)
                dgvDetail.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
                dgvDetail.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                dgvDetail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvDetail.ColumnHeadersHeight = 40

                ' Columns
                dgvDetail.Columns.Add("colNamaKolom", "Jenis Perubahan")
                dgvDetail.Columns.Add("colSebelum", "Data Sebelum")
                dgvDetail.Columns.Add("colSesudah", "Data Sesudah")
                dgvDetail.Columns.Add("colStatus", "Status Perubahan")

                dgvDetail.Columns("colNamaKolom").FillWeight = 20
                dgvDetail.Columns("colSebelum").FillWeight = 30
                dgvDetail.Columns("colSesudah").FillWeight = 30
                dgvDetail.Columns("colStatus").FillWeight = 20

                dgvDetail.Columns("colNamaKolom").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                dgvDetail.Columns("colSebelum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvDetail.Columns("colSesudah").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvDetail.Columns("colStatus").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' Parse Old dan New Value
                Dim oldValue As String = If(IsDBNull(row("OldValue")), "", row("OldValue").ToString())
                Dim newValue As String = If(IsDBNull(row("NewValue")), "", row("NewValue").ToString())

                Dim oldDict As Dictionary(Of String, String) = ParseValueToDictionary(oldValue)
                Dim newDict As Dictionary(Of String, String) = ParseValueToDictionary(newValue)

                ' Gabungkan semua keys dengan NORMALISASI
                Dim normalizedOld As New Dictionary(Of String, String)
                Dim normalizedNew As New Dictionary(Of String, String)

                For Each kvp In oldDict
                    Dim normalKey As String = NormalizeKey(kvp.Key)
                    If Not normalizedOld.ContainsKey(normalKey) Then
                        normalizedOld.Add(normalKey, kvp.Value)
                    End If
                Next

                For Each kvp In newDict
                    Dim normalKey As String = NormalizeKey(kvp.Key)
                    If Not normalizedNew.ContainsKey(normalKey) Then
                        normalizedNew.Add(normalKey, kvp.Value)
                    End If
                Next

                ' Gabungkan semua keys
                Dim allKeys As New HashSet(Of String)
                For Each key In normalizedOld.Keys
                    allKeys.Add(key)
                Next
                For Each key In normalizedNew.Keys
                    allKeys.Add(key)
                Next

                ' Mapping nama kolom ke label yang lebih friendly
                Dim columnLabels As New Dictionary(Of String, String) From {
                {"nopolisi", "No Polisi"},
                {"namasupir", "Nama Supir"},
                {"transporter", "Transporter"},
                {"customer", "Customer"},
                {"product", "Product"},
                {"nodo", "No DO"},
                {"nokontrak", "No Kontrak"},
                {"segelatas", "Segel Atas"},
                {"segelbawah", "Segel Bawah"},
                {"beratbruto", "Berat Bruto (Kg)"},
                {"beratmasuk", "Berat Bruto (Kg)"},
                {"berattara", "Berat Tara (Kg)"},
                {"beratkeluar", "Berat Tara (Kg)"},
                {"beratnetto", "Berat Netto (Kg)"},
                {"potonganpersen", "Potongan (%)"},
                {"potongankg", "Potongan Cong (Kg)"},
                {"potongancong", "Potongan Cong (Kg)"},
                {"totalpotongan", "Total Potongan (Kg)"},
                {"beratbersih", "Berat Bersih (Kg)"},
                {"keterangan", "Keterangan"},
                {"includeffa", "Include FFA"},
                {"ffa", "FFA (%)"},
                {"moisture", "Moisture (%)"},
                {"dirt", "Dirt (%)"}
            }

                ' Urutan kolom yang diinginkan
                Dim orderedKeys As New List(Of String) From {
                "nopolisi", "namasupir", "transporter", "customer", "product",
                "nodo", "nokontrak", "segelatas", "segelbawah",
                "beratbruto", "beratmasuk", "berattara", "beratkeluar",
                "beratnetto", "potonganpersen", "potongankg", "potongancong",
                "totalpotongan", "beratbersih",
                "includeffa", "ffa", "moisture", "dirt", "keterangan"
            }

                ' Tambahkan keys yang ada di data tapi tidak di ordered list
                For Each key In allKeys
                    If Not orderedKeys.Contains(key) Then
                        orderedKeys.Add(key)
                    End If
                Next

                ' Counter untuk statistik
                Dim totalBerubah As Integer = 0
                Dim totalBaru As Integer = 0

                ' Isi DataGridView - HANYA YANG BERUBAH
                For Each key In orderedKeys
                    If Not allKeys.Contains(key) Then Continue For

                    Dim oldVal As String = If(normalizedOld.ContainsKey(key), normalizedOld(key), "")
                    Dim newVal As String = If(normalizedNew.ContainsKey(key), normalizedNew(key), "")

                    ' Skip jika keduanya kosong
                    If String.IsNullOrEmpty(oldVal) AndAlso String.IsNullOrEmpty(newVal) Then Continue For

                    ' Normalize values untuk perbandingan
                    Dim oldValNorm As String = NormalizeValue(oldVal)
                    Dim newValNorm As String = NormalizeValue(newVal)

                    ' SKIP jika nilainya SAMA (status Tetap)
                    If oldValNorm = newValNorm Then Continue For

                    ' Label nama kolom
                    Dim label As String = If(columnLabels.ContainsKey(key), columnLabels(key), key)

                    ' Tentukan status perubahan
                    Dim status As String = ""
                    Dim statusColor As Color = Color.Gray
                    Dim rowBgColor As Color = Color.White

                    If String.IsNullOrEmpty(oldValNorm) AndAlso Not String.IsNullOrEmpty(newValNorm) Then
                        ' Data baru ditambahkan
                        status = "Baru di Tambahkan"
                        statusColor = Color.FromArgb(41, 128, 185)
                        rowBgColor = Color.FromArgb(232, 248, 245)
                        oldVal = "-"
                        totalBaru += 1
                    ElseIf Not String.IsNullOrEmpty(oldValNorm) AndAlso String.IsNullOrEmpty(newValNorm) Then
                        ' Data dihapus
                        status = "di Hapus"
                        statusColor = Color.FromArgb(231, 76, 60)
                        rowBgColor = Color.FromArgb(253, 237, 236)
                        newVal = "di Hapus"
                        totalBerubah += 1
                    Else
                        ' Data berubah
                        status = "di Ubah"
                        statusColor = Color.FromArgb(39, 174, 96)
                        rowBgColor = Color.FromArgb(255, 254, 245)
                        totalBerubah += 1
                    End If

                    Dim rowIndex As Integer = dgvDetail.Rows.Add(label, oldVal, newVal, status)
                    dgvDetail.Rows(rowIndex).Cells("colStatus").Style.ForeColor = statusColor
                    dgvDetail.Rows(rowIndex).Cells("colStatus").Style.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    dgvDetail.Rows(rowIndex).DefaultCellStyle.BackColor = rowBgColor
                Next

                ' Jika tidak ada data perbandingan yang berubah
                If dgvDetail.Rows.Count = 0 Then
                    dgvDetail.Rows.Add("(Tidak ada perubahan data)", "-", "-", "-")
                End If

                ' ============================================= 
                ' FOOTER PANEL
                ' ============================================= 
                Dim pnlFooter As New Panel()
                pnlFooter.Dock = DockStyle.Bottom
                pnlFooter.Height = 60
                pnlFooter.BackColor = Color.FromArgb(248, 249, 250)
                pnlFooter.Padding = New Padding(20, 12, 20, 12)

                Dim lblStats As New Label()
                lblStats.Text = $"📊 Total : {dgvDetail.Rows.Count} field di Ubah | ✏️ di ubah : {totalBerubah} | 🆕 Baru : {totalBaru}"
                lblStats.Font = New Font("Segoe UI", 9)
                lblStats.ForeColor = Color.FromArgb(100, 100, 100)
                lblStats.AutoSize = True
                lblStats.Location = New Point(20, 18)

                ' Tombol Export Excel
                Dim btnExport As New Button()
                btnExport.Text = "📥 Export Excel"
                btnExport.Size = New Size(120, 36)
                btnExport.FlatStyle = FlatStyle.Flat
                btnExport.BackColor = Color.FromArgb(39, 174, 96)
                btnExport.ForeColor = Color.White
                btnExport.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                btnExport.Cursor = Cursors.Hand
                btnExport.Anchor = AnchorStyles.Right
                btnExport.Location = New Point(frmDetail.ClientSize.Width - 270, 12)
                AddHandler btnExport.Click, Sub(s, ev)
                                                ExportDetailToExcel(dgvDetail, logID, row)
                                            End Sub

                ' Tombol Tutup
                Dim btnTutup As New Button()
                btnTutup.Text = "✕ Tutup"
                btnTutup.Size = New Size(100, 36)
                btnTutup.FlatStyle = FlatStyle.Flat
                btnTutup.BackColor = Color.FromArgb(41, 128, 185)
                btnTutup.ForeColor = Color.White
                btnTutup.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                btnTutup.Cursor = Cursors.Hand
                btnTutup.Anchor = AnchorStyles.Right
                btnTutup.Location = New Point(frmDetail.ClientSize.Width - 140, 12)
                AddHandler btnTutup.Click, Sub(s, ev) frmDetail.Close()

                ' Handle resize
                AddHandler frmDetail.Resize, Sub(s, ev)
                                                 btnExport.Location = New Point(frmDetail.ClientSize.Width - 270, 12)
                                                 btnTutup.Location = New Point(frmDetail.ClientSize.Width - 140, 12)
                                             End Sub

                pnlFooter.Controls.AddRange({lblStats, btnExport, btnTutup})

                ' ============================================= 
                ' TAMBAHKAN KE FORM
                ' ============================================= 
                frmDetail.Controls.Add(dgvDetail)
                frmDetail.Controls.Add(pnlKeterangan)
                frmDetail.Controls.Add(pnlFooter)
                frmDetail.Controls.Add(pnlHeader)

                frmDetail.ShowDialog(Me)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormAuditLog.ShowDetail] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat detail Audit Log.", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================= 
    ' NORMALIZE KEY - Untuk mencocokkan key yang berbeda format
    ' ============================================= 
    Private Function NormalizeKey(key As String) As String
        If String.IsNullOrEmpty(key) Then Return ""

        ' Hapus spasi, underscore, dan lowercase semua
        Dim normalized As String = key.ToLowerInvariant().Replace(" ", "").Replace("_", "")

        ' Mapping alias ke key standar
        Select Case normalized
            Case "transporternama", "transporter"
                Return "transporter"
            Case "customernama", "customer"
                Return "customer"
            Case "productnama", "product"
                Return "product"
            Case "beratmasuk", "beratbruto"
                Return "beratbruto"
            Case "beratkeluar", "berattara"
                Return "berattara"
            Case "potongancong", "potongankg"
                Return "potongankg"
            Case Else
                Return normalized
        End Select
    End Function

    ' ============================================= 
    ' NORMALIZE VALUE - Untuk perbandingan nilai
    ' ============================================= 
    Private Function NormalizeValue(value As String) As String
        If String.IsNullOrEmpty(value) Then Return ""

        ' Trim dan lowercase
        Dim normalized As String = value.Trim().ToLowerInvariant()

        ' Hapus separator ribuan untuk angka
        normalized = normalized.Replace(".", "").Replace(",", "")

        ' Treat "0", "", "-" sebagai kosong
        If normalized = "0" OrElse normalized = "-" OrElse normalized = "false" Then
            Return ""
        End If

        Return normalized
    End Function


    ' ============================================= 
    ' PARSE VALUE STRING TO DICTIONARY
    ' ============================================= 
    Private Function ParseValueToDictionary(value As String) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)

        If String.IsNullOrEmpty(value) Then Return result

        Try
            ' Split by newline
            Dim lines As String() = value.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)

            For Each line In lines
                ' Skip header lines
                If line.StartsWith("===") OrElse line.StartsWith("---") Then Continue For

                ' Parse "Key: Value" format
                Dim colonIndex As Integer = line.IndexOf(":")
                If colonIndex > 0 Then
                    Dim key As String = line.Substring(0, colonIndex).Trim()
                    Dim val As String = line.Substring(colonIndex + 1).Trim()

                    ' Clean up key (remove spaces)
                    key = key.Replace(" ", "")

                    If Not result.ContainsKey(key) Then
                        result.Add(key, val)
                    End If
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine($"[ParseValueToDictionary] Error: {ex.Message}")
        End Try

        Return result
    End Function

    ' ============================================= 
    ' EXPORT DETAIL TO EXCEL (CSV) - DIPERBAIKI
    ' ============================================= 
    Private Sub ExportDetailToExcel(dgv As DataGridView, logID As String, row As DataRow)
        Try
            Using sfd As New SaveFileDialog()
                sfd.Title = "Export Detail Audit Log"
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv"
                sfd.FileName = $"DetailAuditLog_{logID}_{DateTime.Now:yyyyMMdd_HHmmss}"

                If sfd.ShowDialog() = DialogResult.OK Then
                    Dim filePath As String = sfd.FileName

                    If filePath.EndsWith(".xlsx") Then
                        ' Export menggunakan Excel Interop
                        ExportToExcelInterop(dgv, logID, row, filePath)
                    Else
                        ' Export ke CSV dengan format yang lebih baik
                        ExportToCSVFormatted(dgv, logID, row, filePath)
                    End If

                    MessageBox.Show($"✅ Export berhasil!{vbCrLf}File: {filePath}",
                          "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If MessageBox.Show("Buka file sekarang?", "Konfirmasi",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Try
                            Process.Start(New ProcessStartInfo(filePath) With {.UseShellExecute = True})
                        Catch
                            Process.Start(filePath)
                        End Try
                    End If
                End If
            End Using
        Catch ex As Exception
            Debug.WriteLine("[FormAuditLog.Export] Error: " & ex.ToString())
            MessageBox.Show("Gagal mengekspor data audit log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================= 
    ' EXPORT TO EXCEL MENGGUNAKAN INTEROP
    ' ============================================= 
    Private Sub ExportToExcelInterop(dgv As DataGridView, logID As String, row As DataRow, filePath As String)
        Dim excelApp As Object = Nothing
        Dim workbook As Object = Nothing
        Dim worksheet As Object = Nothing

        Try
            ' Buat instance Excel
            excelApp = CreateObject("Excel.Application")
            excelApp.Visible = False
            excelApp.DisplayAlerts = False

            workbook = excelApp.Workbooks.Add()
            worksheet = workbook.Sheets(1)
            worksheet.Name = "Detail Audit Log"

            ' ============================================= 
            ' HEADER INFO
            ' ============================================= 
            ' Title
            worksheet.Cells(1, 1).Value = "DETAIL AUDIT LOG"
            worksheet.Cells(1, 1).Font.Bold = True
            worksheet.Cells(1, 1).Font.Size = 14
            worksheet.Range("A1:D1").Merge()
            worksheet.Range("A1:D1").Interior.Color = RGB(41, 128, 185)
            worksheet.Range("A1:D1").Font.Color = RGB(255, 255, 255)
            worksheet.Range("A1:D1").HorizontalAlignment = -4108 ' xlCenter

            ' Info rows
            worksheet.Cells(2, 1).Value = "Log ID"
            worksheet.Cells(2, 2).Value = $"#LOG-{CInt(logID):D10}"
            worksheet.Range("B2:D2").Merge()

            worksheet.Cells(3, 1).Value = "Waktu"
            worksheet.Cells(3, 2).Value = CDate(row("CreatedAt")).ToString("dd MMMM yyyy HH:mm:ss")
            worksheet.Range("B3:D3").Merge()

            worksheet.Cells(4, 1).Value = "User"
            worksheet.Cells(4, 2).Value = If(IsDBNull(row("NamaLengkap")), "System", row("NamaLengkap").ToString())
            worksheet.Range("B4:D4").Merge()

            worksheet.Cells(5, 1).Value = "Action"
            worksheet.Cells(5, 2).Value = If(IsDBNull(row("Action")), "-", row("Action").ToString())
            worksheet.Range("B5:D5").Merge()

            worksheet.Cells(6, 1).Value = "Keterangan"
            worksheet.Cells(6, 2).Value = If(IsDBNull(row("Keterangan")), "-", row("Keterangan").ToString())
            worksheet.Range("B6:D6").Merge()

            ' Style info rows
            For i As Integer = 2 To 6
                worksheet.Cells(i, 1).Font.Bold = True
                worksheet.Cells(i, 1).Interior.Color = RGB(240, 240, 240)
            Next

            ' ============================================= 
            ' TABLE HEADER
            ' ============================================= 
            Dim headerRow As Integer = 8
            Dim headers() As String = {"Jenis Data", "Data Sebelum (Before)", "Data Sesudah (After)", "Status Perubahan"}

            For col As Integer = 1 To 4
                worksheet.Cells(headerRow, col).Value = headers(col - 1)
                worksheet.Cells(headerRow, col).Font.Bold = True
                worksheet.Cells(headerRow, col).Interior.Color = RGB(52, 73, 94)
                worksheet.Cells(headerRow, col).Font.Color = RGB(255, 255, 255)
                worksheet.Cells(headerRow, col).HorizontalAlignment = -4108 ' xlCenter
                worksheet.Cells(headerRow, col).VerticalAlignment = -4108 ' xlCenter
            Next

            ' ============================================= 
            ' TABLE DATA
            ' ============================================= 
            Dim dataRow As Integer = headerRow + 1

            For Each dgvRow As DataGridViewRow In dgv.Rows
                Dim namaKolom As String = dgvRow.Cells("colNamaKolom").Value?.ToString()
                Dim sebelum As String = dgvRow.Cells("colSebelum").Value?.ToString()
                Dim sesudah As String = dgvRow.Cells("colSesudah").Value?.ToString()
                Dim status As String = dgvRow.Cells("colStatus").Value?.ToString()

                ' Hapus emoji dari status untuk Excel
                Dim statusClean As String = status
                If status.Contains("di Ubah") Then statusClean = "di Ubah"
                If status.Contains("Baru") Then statusClean = "Baru"
                If status.Contains("di Hapus") Then statusClean = "di Hapus"

                worksheet.Cells(dataRow, 1).Value = namaKolom
                worksheet.Cells(dataRow, 2).Value = sebelum
                worksheet.Cells(dataRow, 3).Value = sesudah
                worksheet.Cells(dataRow, 4).Value = statusClean

                ' Alignment: Kolom 1 (Jenis Data) = Left, sisanya Center
                worksheet.Cells(dataRow, 1).HorizontalAlignment = -4131 ' xlLeft
                worksheet.Cells(dataRow, 2).HorizontalAlignment = -4108 ' xlCenter
                worksheet.Cells(dataRow, 3).HorizontalAlignment = -4108 ' xlCenter
                worksheet.Cells(dataRow, 4).HorizontalAlignment = -4108 ' xlCenter

                ' Vertical alignment semua tengah
                For col As Integer = 1 To 4
                    worksheet.Cells(dataRow, col).VerticalAlignment = -4108 ' xlCenter
                Next

                ' Warna berdasarkan status
                Select Case True
                    Case status.Contains("di Ubah")
                        worksheet.Range(worksheet.Cells(dataRow, 1), worksheet.Cells(dataRow, 4)).Interior.Color = RGB(255, 254, 245)
                        worksheet.Cells(dataRow, 4).Font.Color = RGB(39, 174, 96)
                    Case status.Contains("Baru")
                        worksheet.Range(worksheet.Cells(dataRow, 1), worksheet.Cells(dataRow, 4)).Interior.Color = RGB(232, 248, 245)
                        worksheet.Cells(dataRow, 4).Font.Color = RGB(41, 128, 185)
                    Case status.Contains("di Hapus")
                        worksheet.Range(worksheet.Cells(dataRow, 1), worksheet.Cells(dataRow, 4)).Interior.Color = RGB(253, 237, 236)
                        worksheet.Cells(dataRow, 4).Font.Color = RGB(231, 76, 60)
                End Select

                worksheet.Cells(dataRow, 4).Font.Bold = True
                dataRow += 1
            Next

            ' ============================================= 
            ' FORMATTING
            ' ============================================= 
            ' Auto fit columns
            worksheet.Columns("A:D").AutoFit()

            ' Set minimum width
            If worksheet.Columns("A").ColumnWidth < 15 Then worksheet.Columns("A").ColumnWidth = 15
            If worksheet.Columns("B").ColumnWidth < 25 Then worksheet.Columns("B").ColumnWidth = 25
            If worksheet.Columns("C").ColumnWidth < 25 Then worksheet.Columns("C").ColumnWidth = 25
            If worksheet.Columns("D").ColumnWidth < 18 Then worksheet.Columns("D").ColumnWidth = 18

            ' Border untuk tabel
            Dim tableRange = worksheet.Range(worksheet.Cells(headerRow, 1), worksheet.Cells(dataRow - 1, 4))
            tableRange.Borders.LineStyle = 1 ' xlContinuous
            tableRange.Borders.Weight = 2 ' xlThin

            ' Row height
            For i As Integer = headerRow To dataRow - 1
                worksheet.Rows(i).RowHeight = 25
            Next

            ' ============================================= 
            ' SAVE
            ' ============================================= 
            workbook.SaveAs(filePath, 51) ' 51 = xlOpenXMLWorkbook (.xlsx)

        Catch ex As Exception
            Debug.WriteLine("[FormAuditLog.ExportExcel] Error: " & ex.ToString())
            Throw New Exception("Gagal mengekspor data ke Excel.", ex)
        Finally
            ' Cleanup
            If workbook IsNot Nothing Then
                workbook.Close(False)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook)
            End If
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    ' ============================================= 
    ' EXPORT TO CSV DENGAN FORMAT LEBIH BAIK
    ' ============================================= 
    Private Sub ExportToCSVFormatted(dgv As DataGridView, logID As String, row As DataRow, filePath As String)
        Using writer As New System.IO.StreamWriter(filePath, False, System.Text.Encoding.UTF8)
            ' BOM untuk UTF-8
            writer.Write(Chr(&HEF) & Chr(&HBB) & Chr(&HBF))

            ' Header Info dengan separator yang jelas
            writer.WriteLine("DETAIL AUDIT LOG")
            writer.WriteLine($"Log ID;#LOG-{CInt(logID):D10}")
            writer.WriteLine($"Waktu;{CDate(row("CreatedAt")):dd MMMM yyyy HH:mm:ss}")
            writer.WriteLine($"User;{If(IsDBNull(row("NamaLengkap")), "System", row("NamaLengkap"))}")
            writer.WriteLine($"Action;{If(IsDBNull(row("Action")), "-", row("Action"))}")
            writer.WriteLine($"Keterangan;{If(IsDBNull(row("Keterangan")), "-", row("Keterangan").ToString().Replace(";", ","))}")
            writer.WriteLine()

            ' Header Tabel - menggunakan semicolon sebagai separator (lebih baik untuk Excel regional Indonesia)
            writer.WriteLine("Jenis Data;Data Sebelum (Before);Data Sesudah (After);Status Perubahan")

            ' Data Tabel
            For Each dgvRow As DataGridViewRow In dgv.Rows
                Dim namaKolom As String = CleanCSVValue(dgvRow.Cells("colNamaKolom").Value?.ToString())
                Dim sebelum As String = CleanCSVValue(dgvRow.Cells("colSebelum").Value?.ToString())
                Dim sesudah As String = CleanCSVValue(dgvRow.Cells("colSesudah").Value?.ToString())
                Dim status As String = dgvRow.Cells("colStatus").Value?.ToString()

                ' Hapus emoji
                Dim statusClean As String = status
                If status.Contains("di Ubah") Then statusClean = "di Ubah"
                If status.Contains("Baru") Then statusClean = "Baru"
                If status.Contains("di Hapus") Then statusClean = "di Hapus"

                writer.WriteLine($"{namaKolom};{sebelum};{sesudah};{statusClean}")
            Next
        End Using
    End Sub

    ' ============================================= 
    ' CLEAN CSV VALUE
    ' ============================================= 
    Private Function CleanCSVValue(value As String) As String
        If String.IsNullOrEmpty(value) Then Return ""

        ' Ganti semicolon dengan comma (karena kita pakai semicolon sebagai separator)
        value = value.Replace(";", ",")

        ' Jika ada newline atau quote, wrap dengan quotes
        If value.Contains(vbCrLf) OrElse value.Contains(vbLf) OrElse value.Contains("""") Then
            value = """" & value.Replace("""", """""") & """"
        End If

        Return value
    End Function

    ' ============================================= 
    ' TOMBOL: EXPORT PDF (AUDIT LOG)
    ' ============================================= 
    Private Sub btnExportPDF_Click(sender As Object, e As EventArgs) Handles btnExportPDF.Click
        Try
            If dgvAuditLog.Rows.Count = 0 Then
                MessageBox.Show("Tidak ada data untuk diekspor!", "Info",
                           MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "PDF Files (*.pdf)|*.pdf"
                sfd.FileName = $"AuditLog_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                sfd.Title = "Export Audit Log ke PDF"

                If sfd.ShowDialog() = DialogResult.OK Then
                    Me.Cursor = Cursors.WaitCursor

                    Try
                        ' Export via Excel -> PDF (karena lebih mudah formatnya)
                        ExportAuditLogToPDF(sfd.FileName)

                        Me.Cursor = Cursors.Default

                        ' Audit Log
                        DatabaseHelper.InsertAuditLog(
                        UserSession.UserID,
                        "EXPORT_PDF_AUDIT_LOG",
                        Nothing,
                        "AuditLog",
                        Nothing,
                        Nothing,
                        Nothing,
                        $"Export Audit Log ke PDF - File: {IO.Path.GetFileName(sfd.FileName)}, Total: {dgvAuditLog.Rows.Count} record"
                    )

                        MessageBox.Show("✅ Export PDF berhasil!", "Sukses",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If MessageBox.Show("Buka file sekarang?", "Konfirmasi",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Try
                                Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                            Catch
                                Process.Start(sfd.FileName)
                            End Try
                        End If

                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        Debug.WriteLine("[FormAuditLog.ExportPdf] Error: " & ex.ToString())
                        MessageBox.Show("Gagal mengekspor data audit log ke PDF.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Debug.WriteLine("[FormAuditLog.ExportPdf] Error: " & ex.ToString())
            MessageBox.Show("Gagal mengekspor data audit log ke PDF.", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================= 
    ' EXPORT AUDIT LOG TO PDF
    ' Via Excel Interop (Excel -> Save as PDF)
    ' ============================================= 
    Private Sub ExportAuditLogToPDF(filePath As String)
        Dim excelApp As Object = Nothing
        Dim workbook As Object = Nothing
        Dim worksheet As Object = Nothing
        Dim tempXlsxPath As String = IO.Path.Combine(IO.Path.GetTempPath(), $"AuditLog_Temp_{DateTime.Now:yyyyMMddHHmmss}.xlsx")

        Try
            ' Coba buat Excel Application
            Try
                excelApp = CreateObject("Excel.Application")
            Catch
                Throw New Exception("Microsoft Excel tidak terdeteksi di komputer ini." & vbCrLf &
                               "Export PDF memerlukan Microsoft Excel untuk format yang baik.")
            End Try

            excelApp.Visible = False
            excelApp.DisplayAlerts = False

            ' Buat workbook dulu (sama seperti export Excel)
            workbook = excelApp.Workbooks.Add()
            worksheet = workbook.Sheets(1)
            worksheet.Name = "Audit Log"

            ' Copy kode dari ExportAuditLogToExcel (header, data, format)
            ' Atau panggil fungsi helper yang sama
            PopulateAuditLogWorksheet(worksheet)

            ' ============================================= 
            ' SAVE AS PDF
            ' ============================================= 
            ' 0 = xlTypePDF
            worksheet.PageSetup.Orientation = 2 ' 2 = xlLandscape (landscape)
            worksheet.PageSetup.Zoom = False
            worksheet.PageSetup.FitToPagesWide = 1
            worksheet.PageSetup.FitToPagesTall = False

            ' Export to PDF
            worksheet.ExportAsFixedFormat(0, filePath) ' 0 = xlTypePDF

        Catch ex As Exception
            Debug.WriteLine("[FormAuditLog.ExportPdf] Error: " & ex.ToString())
            Throw New Exception("Gagal mengekspor data ke PDF.", ex)
        Finally
            ' Cleanup
            If workbook IsNot Nothing Then
                workbook.Close(False)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook)
            End If
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
            End If

            ' Hapus temp file jika ada
            If IO.File.Exists(tempXlsxPath) Then
                Try
                    IO.File.Delete(tempXlsxPath)
                Catch exDelete As Exception
                    Debug.WriteLine("[FormAuditLog.ExportCleanup] Failed to delete temporary file: " & exDelete.ToString())
                End Try
            End If

            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    ' ============================================= 
    ' HELPER: POPULATE WORKSHEET
    ' Digunakan oleh export Excel dan PDF
    ' ============================================= 
    Private Sub PopulateAuditLogWorksheet(worksheet As Object)
        ' Header
        worksheet.Cells(1, 1).Value = "LAPORAN AUDIT LOG"
        worksheet.Cells(1, 1).Font.Bold = True
        worksheet.Cells(1, 1).Font.Size = 16
        worksheet.Range("A1:G1").Merge()
        worksheet.Range("A1").Interior.Color = RGB(41, 128, 185)
        worksheet.Range("A1").Font.Color = RGB(255, 255, 255)
        worksheet.Range("A1").HorizontalAlignment = -4108
        worksheet.Rows(1).RowHeight = 30

        ' Info
        worksheet.Cells(2, 1).Value = "Periode"
        worksheet.Cells(2, 2).Value = $"{dtpTanggalMulai.Value:dd/MM/yyyy} - {dtpTanggalAkhir.Value:dd/MM/yyyy}"
        worksheet.Range("B2:C2").Merge()
        worksheet.Cells(3, 1).Value = "Diekspor oleh"
        worksheet.Cells(3, 2).Value = UserSession.NamaLengkap
        worksheet.Range("B3:C3").Merge()
        worksheet.Cells(4, 1).Value = "Tanggal Export"
        worksheet.Cells(4, 2).Value = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss")
        worksheet.Range("B4:C4").Merge()
        worksheet.Cells(5, 1).Value = "Total Record"
        worksheet.Cells(5, 2).Value = dgvAuditLog.Rows.Count.ToString("N0")
        worksheet.Range("B5:C5").Merge()

        For i As Integer = 2 To 5
            worksheet.Cells(i, 1).Font.Bold = True
            worksheet.Cells(i, 1).Interior.Color = RGB(240, 240, 240)
        Next

        ' Table header
        Dim headerRow As Integer = 7
        Dim headers() As String = {"No", "Waktu", "User", "Aktivitas", "Module", "Deskripsi", "IP Address"}
        For col As Integer = 1 To headers.Length
            worksheet.Cells(headerRow, col).Value = headers(col - 1)
            worksheet.Cells(headerRow, col).Font.Bold = True
            worksheet.Cells(headerRow, col).Interior.Color = RGB(52, 73, 94)
            worksheet.Cells(headerRow, col).Font.Color = RGB(255, 255, 255)
            worksheet.Cells(headerRow, col).HorizontalAlignment = -4108
        Next
        worksheet.Rows(headerRow).RowHeight = 25

        ' Data
        Dim dataRow As Integer = headerRow + 1
        For Each dgvRow As DataGridViewRow In dgvAuditLog.Rows
            If dgvRow.IsNewRow Then Continue For

            worksheet.Cells(dataRow, 1).Value = dgvRow.Cells("colNo").Value?.ToString()
            worksheet.Cells(dataRow, 1).HorizontalAlignment = -4108
            worksheet.Cells(dataRow, 2).Value = dgvRow.Cells("colWaktu").Value?.ToString().Replace(vbCrLf, " ")
            worksheet.Cells(dataRow, 2).HorizontalAlignment = -4108
            worksheet.Cells(dataRow, 3).Value = dgvRow.Cells("colUser").Value?.ToString().Replace(vbCrLf, " ")
            worksheet.Cells(dataRow, 4).Value = dgvRow.Cells("colAktivitas").Value?.ToString()
            worksheet.Cells(dataRow, 4).HorizontalAlignment = -4108
            worksheet.Cells(dataRow, 5).Value = dgvRow.Cells("colModule").Value?.ToString()
            worksheet.Cells(dataRow, 5).HorizontalAlignment = -4108
            worksheet.Cells(dataRow, 6).Value = dgvRow.Cells("colDeskripsi").Value?.ToString()
            worksheet.Cells(dataRow, 7).Value = dgvRow.Cells("colIPAddress").Value?.ToString()
            worksheet.Cells(dataRow, 7).HorizontalAlignment = -4108
            worksheet.Rows(dataRow).RowHeight = 20
            dataRow += 1
        Next

        ' Format
        worksheet.Columns("A:G").AutoFit()
        If worksheet.Columns("A").ColumnWidth < 8 Then worksheet.Columns("A").ColumnWidth = 8
        If worksheet.Columns("F").ColumnWidth < 50 Then worksheet.Columns("F").ColumnWidth = 50

        Dim tableRange = worksheet.Range(worksheet.Cells(headerRow, 1), worksheet.Cells(dataRow - 1, 7))
        tableRange.Borders.LineStyle = 1
        tableRange.Borders.Weight = 2
    End Sub


    Private Sub btnExportSemua_Click(sender As Object, e As EventArgs) Handles btnExportSemua.Click
        btnExportExcel.PerformClick()
    End Sub

    ' ============================================= 
    ' HAPUS LOG LAMA
    ' ============================================= 
    Private Sub btnHapusLogLama_Click(sender As Object, e As EventArgs) Handles btnHapusLogLama.Click
        MessageBox.Show(
            "Penghapusan Audit Log dinonaktifkan untuk menjaga integritas riwayat aktivitas.",
            "Audit Log",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)
    End Sub

    ' ============================================= 
    ' FORM CLOSING
    ' ============================================= 
    Private Sub FormAuditLog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            DatabaseHelper.InsertAuditLog(
                UserSession.UserID,
                "CLOSE_FORM",
                "Audit Log",
                Nothing,
                Nothing,
                Nothing,
                "User menutup Audit Log"
            )
        Catch ex As Exception
            Debug.WriteLine($"[FormClosing] Error: {ex.Message}")
        End Try
    End Sub

End Class
