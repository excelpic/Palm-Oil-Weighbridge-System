' =============================================
' FormPengaturan.vb - VERSI FINAL
' Sesuai dengan Designer yang ada
' =============================================
Imports System.Data.SqlClient
Imports System.IO.Ports
Imports System.IO
Imports System.Drawing.Printing

Public Class FormPengaturan

    ' =============================================
    ' VARIABEL CLASS
    ' =============================================
    Private ErrorLog As New List(Of String)
    Private Const LOG_FILE As String = "FormPengaturan_ErrorLog.txt"

    ' =============================================
    ' ENUM UNTUK ERROR CODES
    ' =============================================
    Public Enum ErrorCode
        SUCCESS = 0
        DB_CONNECTION_FAILED = 1001
        DB_QUERY_FAILED = 1002
        DB_SAVE_FAILED = 1003
        SERIAL_PORT_NOT_FOUND = 2001
        SERIAL_PORT_OPEN_FAILED = 2002
        SERIAL_PORT_TIMEOUT = 2003
        PRINTER_NOT_FOUND = 3001
        PRINTER_OFFLINE = 3002
        PRINT_FAILED = 3003
        FILE_NOT_FOUND = 4001
        FILE_ACCESS_DENIED = 4002
        INVALID_PARAMETER = 5001
        CONTROL_NOT_INITIALIZED = 5002
        UNKNOWN_ERROR = 9999
    End Enum

    ' =============================================
    ' CLASS UNTUK HASIL OPERASI
    ' =============================================
    Public Class OperationResult
        Public Property Success As Boolean
        Public Property ErrorCode As ErrorCode
        Public Property ErrorMessage As String
        Public Property Details As String
        Public Property Timestamp As DateTime = DateTime.Now

        Public Shared Function OK() As OperationResult
            Return New OperationResult With {
                .Success = True,
                .ErrorCode = FormPengaturan.ErrorCode.SUCCESS,
                .ErrorMessage = ""
            }
        End Function

        Public Shared Function Fail(code As ErrorCode, message As String, Optional details As String = "") As OperationResult
            Return New OperationResult With {
                .Success = False,
                .ErrorCode = code,
                .ErrorMessage = message,
                .Details = details
            }
        End Function

        Public Overrides Function ToString() As String
            If Success Then
                Return "✅ Operasi berhasil"
            Else
                Dim msg As String = "❌ Error [" & ErrorCode.ToString() & "]: " & ErrorMessage
                If Not String.IsNullOrEmpty(Details) Then
                    msg &= vbCrLf & "Detail: " & Details
                End If
                Return msg
            End If
        End Function
    End Class

    ' =============================================
    ' FORM LOAD
    ' =============================================
    Private Sub FormPengaturan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim loadErrors As New List(Of String)
        Dim result As OperationResult

        Try
            LogInfo("FormPengaturan_Load dimulai")

            AdjustForScreenSize()

            ' 1. Isi pilihan ComboBox
            result = LoadComboBoxOptions()
            If Not result.Success Then loadErrors.Add(result.ToString())

            ' 2. Load pengaturan dari database
            result = LoadSettings()
            If Not result.Success Then loadErrors.Add(result.ToString())

            ' 3. Load pengaturan database
            LoadDatabaseSettings()

            ApplyPermissionLocks()

            ' Tampilkan ringkasan jika ada error
            If loadErrors.Count > 0 Then
                ShowErrorSummary("Form Load", loadErrors)
            End If

            LogInfo("FormPengaturan_Load selesai dengan " & loadErrors.Count.ToString() & " error")

            ' Audit Log
            Try
                DatabaseHelper.InsertAuditLog(
                    UserSession.UserID,
                    "OPEN_FORM",
                    Nothing,
                    "Pengaturan",
                    Nothing,
                    Nothing,
                    Nothing,
                    "User membuka Pengaturan"
                )
            Catch exAudit As Exception
                Debug.WriteLine($"[AuditLog] Error : {exAudit.Message}")
            End Try

        Catch ex As Exception
            Dim criticalError = HandleException("FormPengaturan_Load", ex)
            MessageBox.Show(criticalError.ToString(), "Critical Error - Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' PENYESUAIAN UKURAN FORM UNTUK BERBAGAI LAYAR
    ' =============================================
    Private Sub AdjustForScreenSize()
        Try
            Dim screenArea As Rectangle = Screen.FromControl(Me).WorkingArea

            Debug.WriteLine($"[AdjustForScreen-Pengaturan] Screen: {screenArea.Width}x{screenArea.Height}")
            Debug.WriteLine($"[AdjustForScreen-Pengaturan] Form  : {Me.Width}x{Me.Height}")

            If Me.Width > screenArea.Width OrElse Me.Height > screenArea.Height Then
                Me.AutoScroll = True
                Me.Location = New Point(screenArea.Left, screenArea.Top)
                Dim newWidth As Integer = Math.Min(Me.Width, screenArea.Width)
                Dim newHeight As Integer = Math.Min(Me.Height, screenArea.Height)
                Me.Size = New Size(newWidth, newHeight)
                Debug.WriteLine($"[AdjustForScreen-Pengaturan] Resized to: {newWidth}x{newHeight}")
            Else
                Me.Location = New Point(
                screenArea.Left + (screenArea.Width - Me.Width) \ 2,
                screenArea.Top + (screenArea.Height - Me.Height) \ 2
            )
            End If

        Catch ex As Exception
            Debug.WriteLine($"[AdjustForScreenSize-Pengaturan] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' ISI PILIHAN COMBOBOX
    ' =============================================
    Private Function LoadComboBoxOptions() As OperationResult
        Dim errors As New List(Of String)

        Try
            ' ============================================
            ' TAB TIMBANGAN - COM PORT
            ' ============================================
            Try
                cmbComPort.Items.Clear()
                Dim availablePorts As String() = SerialPort.GetPortNames()

                If availablePorts IsNot Nothing AndAlso availablePorts.Length > 0 Then
                    Array.Sort(availablePorts)
                    cmbComPort.Items.AddRange(availablePorts)
                    LogInfo("Ditemukan " & availablePorts.Length.ToString() & " COM Port: " & String.Join(", ", availablePorts))
                Else
                    cmbComPort.Items.AddRange(New String() {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8"})
                    LogWarning("Tidak ada COM Port terdeteksi, menggunakan default")
                End If
            Catch ex As Exception
                cmbComPort.Items.AddRange(New String() {"COM1", "COM2", "COM3", "COM4", "COM5"})
                LogWarning("Failed to load COM port list: " & ex.ToString())
                errors.Add("COM Port: Tidak dapat memuat daftar port")
            End Try

            ' Baud Rate - tambahkan item karena di designer sudah ada beberapa
            Try
                If cmbBaudRate.Items.Count = 0 Then
                    cmbBaudRate.Items.AddRange(New String() {"1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200"})
                End If
            Catch ex As Exception
                LogWarning("Failed to load baud rate options: " & ex.ToString())
                errors.Add("Baud Rate: Tidak dapat memuat opsi")
            End Try

            ' Data Bits - di designer sudah ada "7", "8"
            Try
                If cmbDataBits.Items.Count = 0 Then
                    cmbDataBits.Items.AddRange(New String() {"5", "6", "7", "8"})
                End If
            Catch ex As Exception
                LogWarning("Failed to load data bits options: " & ex.ToString())
                errors.Add("Data Bits: Tidak dapat memuat opsi")
            End Try

            ' Parity - di designer sudah ada
            Try
                If cmbParity.Items.Count = 0 Then
                    cmbParity.Items.AddRange(New String() {"None", "Odd", "Even", "Mark", "Space"})
                End If
            Catch ex As Exception
                LogWarning("Failed to load parity options: " & ex.ToString())
                errors.Add("Parity: Tidak dapat memuat opsi")
            End Try

            ' Stop Bits - di designer sudah ada
            Try
                If cmbStopBits.Items.Count = 0 Then
                    cmbStopBits.Items.AddRange(New String() {"1", "1.5", "2"})
                End If
            Catch ex As Exception
                LogWarning("Failed to load stop bits options: " & ex.ToString())
                errors.Add("Stop Bits: Tidak dapat memuat opsi")
            End Try

            ' Protocol - di designer sudah ada
            Try
                If cmbProtocol.Items.Count = 0 Then
                    cmbProtocol.Items.AddRange(New String() {"STANDARD", "A9", "YH-T7", "AND", "OHAUS", "JADEVER", "Continuous", "Command"})
                End If
            Catch ex As Exception
                LogWarning("Failed to load protocol options: " & ex.ToString())
                errors.Add("Protocol: Tidak dapat memuat opsi")
            End Try

            ' Merek Indikator Timbangan
            Try
                If cmbMerekIndikator.Items.Count = 0 Then
                    cmbMerekIndikator.Items.AddRange(New String() {"GSC", "SONIC", "YAOHUA"})
                End If
                If cmbMerekIndikator.SelectedIndex < 0 Then
                    cmbMerekIndikator.SelectedIndex = 0
                End If
            Catch ex As Exception
                LogWarning("Failed to load indicator brand options: " & ex.ToString())
                errors.Add("Merek Indikator: Tidak dapat memuat opsi")
            End Try

            ' ============================================
            ' TAB PRINTER STRUK (TIKET KELUAR)
            ' ============================================
            Try
                cmbPaperSize.Items.Clear()
                ' Ukuran Standar
                cmbPaperSize.Items.Add("A4 (210 x 297 mm)")
                cmbPaperSize.Items.Add("A5 (148 x 210 mm)")
                cmbPaperSize.Items.Add("A6 (105 x 148 mm)")
                cmbPaperSize.Items.Add("Letter (8.5 x 11 inch)")
                cmbPaperSize.Items.Add("Legal (8.5 x 14 inch)")
                ' Continuous Form
                cmbPaperSize.Items.Add("Continuous 9.5 x 5.5 inch")
                cmbPaperSize.Items.Add("Continuous 9.5 x 11 inch")
                cmbPaperSize.Items.Add("Continuous 10 x 6 inch")
                cmbPaperSize.Items.Add("Continuous 10 x 12 inch")
                ' Dot Matrix
                cmbPaperSize.Items.Add("Half Letter (5.5 x 8.5 inch)")
                cmbPaperSize.Items.Add("Faktur Timbangan (A5)")
                ' Custom
                cmbPaperSize.Items.Add("Custom Size...")
                LogInfo("Loaded " & cmbPaperSize.Items.Count.ToString() & " paper size options untuk printer struk")
            Catch ex As Exception
                LogWarning("Failed to load receipt paper sizes: " & ex.ToString())
                errors.Add("Paper Size Struk: Tidak dapat memuat opsi")
            End Try

            ' ============================================
            ' TAB PRINTER TIKET MASUK (THERMAL)
            ' ============================================
            Try
                cmbPaperSizeTkt.Items.Clear()
                ' Ukuran thermal printer yang umum digunakan
                cmbPaperSizeTkt.Items.Add("58mm")
                cmbPaperSizeTkt.Items.Add("80mm")
                LogInfo("Loaded " & cmbPaperSizeTkt.Items.Count.ToString() & " paper size options untuk printer tiket")
            Catch ex As Exception
                LogWarning("Failed to load ticket paper sizes: " & ex.ToString())
                errors.Add("Paper Size Tiket: Tidak dapat memuat opsi")
            End Try

            ' Load Printer untuk Struk (Tiket Keluar)
            Dim printerResult = LoadInstalledPrinters(cmbPrinter)
            If Not printerResult.Success Then
                errors.Add("Printer Struk: " & printerResult.ErrorMessage)
            End If

            ' Load Printer untuk Tiket Masuk (Thermal)
            Dim printerTktResult = LoadInstalledPrinters(CmbPrinterTkt)
            If Not printerTktResult.Success Then
                errors.Add("Printer Tiket: " & printerTktResult.ErrorMessage)
            End If

            ' Return result
            If errors.Count > 0 Then
                Return OperationResult.Fail(
                    ErrorCode.INVALID_PARAMETER,
                    errors.Count.ToString() & " error saat load ComboBox",
                    String.Join("; ", errors)
                )
            End If

            Return OperationResult.OK()

        Catch ex As Exception
            Return HandleException("LoadComboBoxOptions", ex)
        End Try
    End Function
    Private Sub cmbMerekIndikator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMerekIndikator.SelectedIndexChanged
        Try
            If cmbTipeIndikator Is Nothing Then Return

            cmbTipeIndikator.Items.Clear()

            Dim merek As String = If(cmbMerekIndikator.SelectedItem IsNot Nothing,
                                 cmbMerekIndikator.SelectedItem.ToString().ToUpperInvariant(), "")

            Select Case merek
                Case "GSC"
                    cmbTipeIndikator.Items.AddRange(New String() {
                    "SGW-3015S",
                    "SGW-3015",
                    "GST-9600",
                    "GST-9800",
                    "SGW-3000"
                })

                Case "SONIC"
                    cmbTipeIndikator.Items.AddRange(New String() {
                    "A28E",
                    "SPS-2000",
                    "SPS-3000",
                    "SPS-1000",
                    "T7E"
                })

                Case "YAOHUA"
                    cmbTipeIndikator.Items.AddRange(New String() {
                    "XK3190-A9",
                    "XK3190-A12E",
                    "XK3190-A28E",
                    "XK3190-A7",
                    "T7"
                })
            End Select

            If cmbTipeIndikator.Items.Count > 0 Then
                ' Coba load dari database dulu
                Dim savedType As String = GetSavedSetting("SCALE_TYPE", "")
                If Not String.IsNullOrEmpty(savedType) AndAlso cmbTipeIndikator.Items.Contains(savedType) Then
                    cmbTipeIndikator.SelectedItem = savedType
                Else
                    cmbTipeIndikator.SelectedIndex = 0
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine($"[cmbMerekIndikator_SelectedIndexChanged] Error: {ex.Message}")
        End Try
    End Sub
    ' =============================================
    ' LOAD PRINTER YANG TERINSTALL (REUSABLE)
    ' =============================================
    Private Function LoadInstalledPrinters(targetComboBox As ComboBox) As OperationResult
        Try
            targetComboBox.BeginUpdate()
            targetComboBox.DataSource = Nothing
            targetComboBox.Items.Clear()

            Dim found As New List(Of String)
            Dim skippedPrinters As New List(Of String)

            For Each printerName As String In PrinterSettings.InstalledPrinters
                Try
                    If Not String.IsNullOrWhiteSpace(printerName) Then
                        found.Add(printerName)
                    End If
                Catch
                    skippedPrinters.Add(printerName)
                End Try
            Next

            If skippedPrinters.Count > 0 Then
                LogWarning("Skipped " & skippedPrinters.Count.ToString() & " printers: " & String.Join(", ", skippedPrinters))
            End If

            found.Sort()

            If found.Count = 0 Then
                targetComboBox.Items.Add("(Tidak ada printer terdeteksi)")
                targetComboBox.SelectedIndex = 0
                LogWarning("Tidak ada printer yang terdeteksi")
                Return OperationResult.Fail(ErrorCode.PRINTER_NOT_FOUND, "Tidak ada printer terdeteksi di sistem")
            Else
                targetComboBox.Items.AddRange(found.ToArray())

                Try
                    Dim defaultPrinter As String = New PrinterSettings().PrinterName
                    If targetComboBox.Items.Contains(defaultPrinter) Then
                        targetComboBox.SelectedItem = defaultPrinter
                    Else
                        targetComboBox.SelectedIndex = 0
                    End If
                Catch
                    targetComboBox.SelectedIndex = 0
                End Try

                LogInfo("Ditemukan " & found.Count.ToString() & " printer untuk " & targetComboBox.Name)
            End If

            Return OperationResult.OK()

        Catch ex As UnauthorizedAccessException
            LogError("LoadPrinters", ex)
            Return OperationResult.Fail(ErrorCode.FILE_ACCESS_DENIED, "Akses ditolak untuk membaca daftar printer")
        Catch ex As Exception
            Return HandleException("LoadInstalledPrinters", ex)
        Finally
            targetComboBox.EndUpdate()
        End Try
    End Function

    ' =============================================
    ' LOAD PENGATURAN DARI DATABASE
    ' =============================================
    Private Function LoadSettings() As OperationResult
        Dim loadedCount As Integer = 0
        Dim errorCount As Integer = 0
        Dim errors As New List(Of String)

        Try
            If Not IsDatabaseConnected() Then
                Return OperationResult.Fail(ErrorCode.DB_CONNECTION_FAILED, "Tidak dapat terhubung ke database")
            End If

            Dim query As String = "SELECT SettingKey, SettingValue FROM Settings"
            Dim dt As DataTable = Nothing

            Try
                dt = DatabaseHelper.ExecuteQuery(query)
            Catch ex As SqlException
                LogError("LoadSettings", ex)
            Return OperationResult.Fail(ErrorCode.DB_QUERY_FAILED, "Gagal mengambil pengaturan aplikasi")
            End Try

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                LogWarning("Tidak ada pengaturan di database, menggunakan default")
                Return OperationResult.OK()
            End If

            For Each row As DataRow In dt.Rows
                Try
                    Dim key As String = SafeGetString(row, "SettingKey")
                    Dim value As String = SafeGetString(row, "SettingValue")

                    If String.IsNullOrEmpty(key) Then Continue For

                    Dim result = ApplySetting(key, value)
                    If result.Success Then
                        loadedCount += 1
                    Else
                        errorCount += 1
                        errors.Add(key & ": " & result.ErrorMessage)
                    End If

                Catch ex As Exception
                    errorCount += 1
                    LogWarning("Failed to apply setting row: " & ex.ToString())
                    errors.Add("Beberapa pengaturan tidak dapat diterapkan")
                End Try
            Next

            LogInfo("Loaded " & loadedCount.ToString() & " settings, " & errorCount.ToString() & " errors")

            If errorCount > 0 Then
                Return OperationResult.Fail(
                    ErrorCode.DB_QUERY_FAILED,
                    "Berhasil load " & loadedCount.ToString() & " setting, " & errorCount.ToString() & " gagal",
                    String.Join("; ", errors)
                )
            End If

            Return OperationResult.OK()

        Catch ex As Exception
            Return HandleException("LoadSettings", ex)
        End Try
    End Function

    ' =============================================
    ' APPLY SETTING KE KONTROL
    ' =============================================
    Private Function ApplySetting(key As String, value As String) As OperationResult
        Try
            Select Case key
                ' === TAB PERUSAHAAN ===
                Case "NamaPerusahaan"
                    txtNamaPerusahaan.Text = value
                Case "AlamatPerusahaan"
                    txtAlamatPerusahaan.Text = value
                Case "KotaPerusahaan"
                    txtKotaPerusahaan.Text = value
                Case "TeleponPerusahaan"
                    txtTeleponPerusahaan.Text = value
                Case "KodePos"
                    txtFaxPerusahaan.Text = value
                Case "EmailPerusahaan"
                    txtEmailPerusahaan.Text = value
                Case "LogoPath"
                    txtLogoPath.Text = value
                    If Not String.IsNullOrEmpty(value) AndAlso File.Exists(value) Then
                        Try
                            Using fs As New FileStream(value, FileMode.Open, FileAccess.Read)
                                picLogo.Image = Image.FromStream(fs)
                            End Using
                        Catch ex As Exception
                            LogWarning("Gagal load logo: " & ex.Message)
                        End Try
                    End If

                ' === TAB TIMBANGAN ===
                Case "COM_PORT"
                    SafeSelectComboItem(cmbComPort, value)
                Case "BAUD_RATE"
                    SafeSelectComboItem(cmbBaudRate, value)
                Case "DATA_BITS"
                    SafeSelectComboItem(cmbDataBits, value)
                Case "PARITY"
                    SafeSelectComboItem(cmbParity, value)
                Case "STOP_BITS"
                    SafeSelectComboItem(cmbStopBits, value)
                Case "PROTOCOL"
                    SafeSelectComboItem(cmbProtocol, value)
                Case "KapasitasTimbangan"
                    txtKapasitas.Text = value
                Case "DivisiTimbangan"
                    txtDivisi.Text = value
                Case "SCALE_BRAND"
                    SafeSelectComboItem(cmbMerekIndikator, value)
                Case "SCALE_TYPE"
                    SafeSelectComboItem(cmbTipeIndikator, value)

                ' === TAB PRINTER STRUK (TIKET KELUAR) ===
                Case "PrinterName"
                    SafeSelectComboItem(cmbPrinter, value)
                Case "PaperSize"
                    SafeSelectComboItem(cmbPaperSize, value)
                Case "JumlahCopy"
                    If IsNumeric(value) Then
                        Dim numValue As Decimal = CDec(value)
                        If numValue >= nudCopies.Minimum AndAlso numValue <= nudCopies.Maximum Then
                            nudCopies.Value = numValue
                        End If
                    End If
                Case "AutoPrint"

                ' === TAB PRINTER TIKET MASUK (THERMAL) ===
                Case "PrinterNameTkt"
                    SafeSelectComboItem(CmbPrinterTkt, value)
                Case "PaperSizeTkt"
                    SafeSelectComboItem(cmbPaperSizeTkt, value)
                Case "JumlahCopyTkt"
                    If IsNumeric(value) Then
                        Dim numValue As Decimal = CDec(value)
                        If numValue >= NumericUpDown1.Minimum AndAlso numValue <= NumericUpDown1.Maximum Then
                            NumericUpDown1.Value = numValue
                        End If
                    End If

                ' === TAB UMUM ===
                Case "PotonganDefault"
                    If IsNumeric(value) Then
                        Dim numValue As Decimal = CDec(value)
                        If numValue >= nudPotonganDefault.Minimum AndAlso numValue <= nudPotonganDefault.Maximum Then
                            nudPotonganDefault.Value = numValue
                        End If
                    End If
                Case "WajibSupplier"
                    chkWajibSupplier.Checked = (value = "1" OrElse value.ToLowerInvariant() = "true")
                Case "WajibProduk"
                    chkWajibProduk.Checked = (value = "1" OrElse value.ToLowerInvariant() = "true")
                Case "WajibTransporter"
                    chkWajibTransporter.Checked = (value = "1" OrElse value.ToLowerInvariant() = "true")
                Case "KonfirmasiHapus"
                    chkKonfirmasiHapus.Checked = (value = "1" OrElse value.ToLowerInvariant() = "true")
                Case "BackupOtomatis"
                    chkBackupOtomatis.Checked = (value = "1" OrElse value.ToLowerInvariant() = "true")

                Case Else
                    LogWarning("Unknown setting key: " & key)
            End Select

            Return OperationResult.OK()

        Catch ex As Exception
            LogError("ApplySetting:" & key, ex)
            Return OperationResult.Fail(ErrorCode.INVALID_PARAMETER, "Gagal menerapkan pengaturan")
        End Try
    End Function

    ' =============================================
    ' HELPER FUNCTIONS
    ' =============================================
    Private Sub SafeSelectComboItem(cmb As ComboBox, value As String)
        If cmb Is Nothing OrElse String.IsNullOrEmpty(value) Then Return

        Try
            ' Coba exact match dulu
            If cmb.Items.Contains(value) Then
                cmb.SelectedItem = value
                Return
            End If

            ' Coba case-insensitive match
            For i As Integer = 0 To cmb.Items.Count - 1
                If cmb.Items(i).ToString().Equals(value, StringComparison.OrdinalIgnoreCase) Then
                    cmb.SelectedIndex = i
                    Return
                End If
            Next

            ' Coba partial match (untuk items yang ada teks tambahan)
            For i As Integer = 0 To cmb.Items.Count - 1
                If cmb.Items(i).ToString().StartsWith(value, StringComparison.OrdinalIgnoreCase) Then
                    cmb.SelectedIndex = i
                    Return
                End If
            Next
        Catch ex As Exception
            LogWarning("SafeSelectComboItem error: " & ex.Message)
        End Try
    End Sub

    Private Function SafeGetString(row As DataRow, columnName As String) As String
        Try
            If row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) Then Return ""
            Dim value As Object = row(columnName)
            If value Is Nothing OrElse IsDBNull(value) Then Return ""
            Return value.ToString()
        Catch
            Return ""
        End Try
    End Function

    Private Function SafeGetComboValue(cmb As ComboBox, defaultValue As String) As String
        Try
            If cmb Is Nothing OrElse cmb.SelectedItem Is Nothing Then Return defaultValue
            Dim value As String = cmb.SelectedItem.ToString()
            Return If(String.IsNullOrEmpty(value), defaultValue, value)
        Catch
            Return defaultValue
        End Try
    End Function

    Private Function IsDatabaseConnected() As Boolean
        Try
            Dim dt = DatabaseHelper.ExecuteQuery("SELECT 1")
            Return dt IsNot Nothing
        Catch
            Return False
        End Try
    End Function

    Private Function GetTextBoxValue(txt As TextBox) As String
        Try
            If txt Is Nothing Then Return ""
            If String.IsNullOrEmpty(txt.Text) Then Return ""
            Return txt.Text.Trim()
        Catch
            Return ""
        End Try
    End Function

    Private Function GetNumericUpDownValue(nud As NumericUpDown) As String
        Try
            If nud Is Nothing Then Return "0"
            Return nud.Value.ToString()
        Catch
            Return "0"
        End Try
    End Function

    Private Function GetCheckBoxValue(chk As CheckBox) As String
        Try
            If chk Is Nothing Then Return "0"
            Return If(chk.Checked, "1", "0")
        Catch
            Return "0"
        End Try
    End Function

    ' =============================================
    ' TAB TIMBANGAN - TEST KONEKSI
    ' =============================================
    Private Sub btnTestKoneksi_Click(sender As Object, e As EventArgs) Handles btnTestKoneksi.Click
        Dim result = TestSerialConnection()

        ' Audit Log
        Try
            DatabaseHelper.InsertAuditLog(
                UserSession.UserID,
                If(result.Success, "Test Koneksi Berhasil", "Test Koneksi Gagal"),
                Nothing,
                "Settings",
                Nothing,
                Nothing,
                Nothing,
                $"Test koneksi timbangan - Port : {cmbComPort.SelectedItem}, Result : {If(result.Success, "Berhasil", "Gagal")}"
            )
        Catch exAudit As Exception
            Debug.WriteLine($"[AuditLog] Error : {exAudit.Message}")
        End Try

        If result.Success Then
            MessageBox.Show("✓ Koneksi berhasil!" & vbCrLf & vbCrLf & result.Details,
                       "Test Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(result.ToString(), "Test Koneksi Gagal",
                       MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function TestSerialConnection() As OperationResult
        ' Validasi input
        If cmbComPort.SelectedItem Is Nothing OrElse String.IsNullOrEmpty(cmbComPort.Text) Then
            UpdateConnectionStatus(False, "Pilih COM Port!")
            Return OperationResult.Fail(ErrorCode.INVALID_PARAMETER, "COM Port belum dipilih")
        End If

        Dim portName As String = If(cmbComPort.SelectedItem IsNot Nothing, cmbComPort.SelectedItem.ToString(), cmbComPort.Text)
        Dim baudRate As Integer = CInt(SafeGetComboValue(cmbBaudRate, "9600"))
        Dim dataBits As Integer = CInt(SafeGetComboValue(cmbDataBits, "8"))
        Dim parityStr As String = SafeGetComboValue(cmbParity, "None")
        Dim stopBitsStr As String = SafeGetComboValue(cmbStopBits, "1")

        Try
            Using serialPort As New SerialPort()
                serialPort.PortName = portName
                serialPort.BaudRate = baudRate
                serialPort.DataBits = dataBits

                ' Set Parity
                Select Case parityStr
                    Case "None" : serialPort.Parity = Parity.None
                    Case "Odd" : serialPort.Parity = Parity.Odd
                    Case "Even" : serialPort.Parity = Parity.Even
                    Case "Mark" : serialPort.Parity = Parity.Mark
                    Case "Space" : serialPort.Parity = Parity.Space
                    Case Else : serialPort.Parity = Parity.None
                End Select

                ' Set Stop Bits
                Select Case stopBitsStr
                    Case "1" : serialPort.StopBits = StopBits.One
                    Case "1.5" : serialPort.StopBits = StopBits.OnePointFive
                    Case "2" : serialPort.StopBits = StopBits.Two
                    Case Else : serialPort.StopBits = StopBits.One
                End Select

                serialPort.ReadTimeout = 3000
                serialPort.WriteTimeout = 3000

                ' Coba buka port
                serialPort.Open()

                If serialPort.IsOpen Then
                    UpdateConnectionStatus(True, "Terhubung ke " & portName)
                    serialPort.Close()

                    Return New OperationResult With {
                        .Success = True,
                        .ErrorCode = ErrorCode.SUCCESS,
                        .ErrorMessage = "",
                        .Details = "Port: " & portName & vbCrLf &
                                   "Baud Rate: " & baudRate.ToString() & vbCrLf &
                                   "Data Bits: " & dataBits.ToString() & vbCrLf &
                                   "Parity: " & parityStr & vbCrLf &
                                   "Stop Bits: " & stopBitsStr
                    }
                Else
                    UpdateConnectionStatus(False, "Port tidak dapat dibuka")
                    Return OperationResult.Fail(ErrorCode.SERIAL_PORT_OPEN_FAILED, "Port tidak dapat dibuka")
                End If
            End Using

        Catch ex As UnauthorizedAccessException
            UpdateConnectionStatus(False, "Akses ditolak - port mungkin sedang digunakan")
            Return OperationResult.Fail(ErrorCode.FILE_ACCESS_DENIED,
                                       "Akses ke " & portName & " ditolak",
                                       "Port mungkin sedang digunakan aplikasi lain")

        Catch ex As IOException
            UpdateConnectionStatus(False, "Port tidak ditemukan")
            LogError("TestSerialConnection:PortNotFound", ex)
            Return OperationResult.Fail(ErrorCode.SERIAL_PORT_NOT_FOUND,
                                       "Port " & portName & " tidak ditemukan")

        Catch ex As TimeoutException
            UpdateConnectionStatus(False, "Timeout")
            LogError("TestSerialConnection:Timeout", ex)
            Return OperationResult.Fail(ErrorCode.SERIAL_PORT_TIMEOUT,
                                       "Timeout saat membuka " & portName)

        Catch ex As Exception
            LogError("TestSerialConnection", ex)
            UpdateConnectionStatus(False, "Terjadi kesalahan saat menguji port")
            Return HandleException("TestSerialConnection", ex)
        End Try
    End Function

    Private Sub UpdateConnectionStatus(success As Boolean, message As String)
        If lblStatusKoneksi Is Nothing Then Return

        If success Then
            lblStatusKoneksi.Text = "● Status: ✓ " & message
            lblStatusKoneksi.ForeColor = Color.Green
        Else
            lblStatusKoneksi.Text = "● Status: ✗ " & message
            lblStatusKoneksi.ForeColor = Color.Red
        End If
    End Sub

    ' =============================================
    ' TOMBOL SIMPAN
    ' =============================================
    Private Sub ApplyPermissionLocks()
        Dim canCompany As Boolean = UserSession.CanEditPerusahaan()
        Dim canIndicator As Boolean = UserSession.CanEditIndikator()
        Dim canDbConn As Boolean = UserSession.CanEditDatabaseConnection()

        For Each c As Control In New Control() {txtNamaPerusahaan, txtAlamatPerusahaan, txtKotaPerusahaan, txtTeleponPerusahaan,
                                  txtFaxPerusahaan, txtEmailPerusahaan, txtLogoPath, btnBrowseLogo, btnHapusLogo}
            c.Enabled = canCompany
        Next

        For Each c As Control In New Control() {cmbComPort, cmbBaudRate, cmbDataBits, cmbParity, cmbStopBits, cmbProtocol,
                                  txtKapasitas, txtDivisi, cmbMerekIndikator, cmbTipeIndikator}
            c.Enabled = canIndicator
        Next

        For Each c As Control In New Control() {txtServerName, txtDatabaseName, rbWindowsAuth, rbSQLAuth,
                                  txtSQLUsername, txtSQLPassword, btnTestDB, btnSimpanDB}
            c.Enabled = canDbConn
        Next

        btnResetDefault.Enabled = canCompany AndAlso canIndicator
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Not UserSession.Authorize(UserSession.CanAccessSettings(), "simpan pengaturan sistem") Then Return

        If MessageBox.Show("Simpan semua pengaturan?", "Konfirmasi",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Dim saveErrors As New List(Of String)
        Dim savedCount As Integer = 0

        Try
            btnSimpan.Enabled = False
            btnSimpan.Text = "⏳ Menyimpan..."
            Application.DoEvents()

            ' Simpan ke My.Settings (LOCAL)
            Try
                If UserSession.CanEditIndikator() Then SaveLocalSettings()
            Catch ex As Exception
                LogWarning("Failed to save local settings: " & ex.ToString())
                saveErrors.Add("Local Settings: Gagal menyimpan pengaturan")
            End Try

            ' Simpan ke Database
            Dim dbResults = SaveDatabaseSettings()
            savedCount = dbResults.Item1
            saveErrors.AddRange(dbResults.Item2)
            SettingsHelper.ClearCache()

            ' Audit log
            Try
                DatabaseHelper.InsertAuditLog(UserSession.UserID, "UPDATE_SETTINGS", Nothing, "Settings", Nothing,
                                             "", "Pengaturan Sistem di Ubah", "Update Pengaturan Sistem")
            Catch ex As Exception
                LogWarning("Failed to write audit log during save: " & ex.ToString())
                saveErrors.Add("Audit Log: Gagal mencatat aktivitas")
            End Try

            ' Tampilkan hasil
            If saveErrors.Count = 0 Then
                MessageBox.Show("✅ Semua " & savedCount.ToString() & " pengaturan berhasil disimpan!",
                               "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim summaryList As New List(Of String)
                summaryList.Add("✓ Berhasil: " & savedCount.ToString() & " setting")
                summaryList.Add("✗ Gagal: " & saveErrors.Count.ToString() & " setting")
                ShowErrorSummary("Simpan Pengaturan", summaryList, saveErrors)
            End If

        Catch ex As Exception
            Dim result = HandleException("btnSimpan_Click", ex)
            MessageBox.Show(result.ToString(), "Error Simpan", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            btnSimpan.Enabled = True
            btnSimpan.Text = "💾 Simpan"
        End Try
    End Sub

    Private Sub SaveLocalSettings()
        My.Settings.LastComPort = SafeGetComboValue(cmbComPort, "")
        My.Settings.LastBaudRate = SafeGetComboValue(cmbBaudRate, "9600")
        My.Settings.LastDataBits = SafeGetComboValue(cmbDataBits, "8")
        My.Settings.LastParity = SafeGetComboValue(cmbParity, "None")
        My.Settings.LastStopBits = SafeGetComboValue(cmbStopBits, "1")
        My.Settings.LastProtocol = SafeGetComboValue(cmbProtocol, "Standard")
        My.Settings.LastKapasitas = GetTextBoxValue(txtKapasitas)
        My.Settings.LastDivisi = GetTextBoxValue(txtDivisi)
        My.Settings.Save()
    End Sub

    ' =============================================
    ' SIMPAN SETTINGS KE DATABASE
    ' =============================================
    Private Function SaveDatabaseSettings() As Tuple(Of Integer, List(Of String))
        Dim savedCount As Integer = 0
        Dim errors As New List(Of String)

        Dim settings As New Dictionary(Of String, String)

        ' === TAB PERUSAHAAN ===
        If UserSession.CanEditPerusahaan() Then
            settings.Add("NamaPerusahaan", GetTextBoxValue(txtNamaPerusahaan))
            settings.Add("AlamatPerusahaan", GetTextBoxValue(txtAlamatPerusahaan))
            settings.Add("KotaPerusahaan", GetTextBoxValue(txtKotaPerusahaan))
            settings.Add("TeleponPerusahaan", GetTextBoxValue(txtTeleponPerusahaan))
            settings.Add("KodePos", GetTextBoxValue(txtFaxPerusahaan))
            settings.Add("EmailPerusahaan", GetTextBoxValue(txtEmailPerusahaan))
            settings.Add("LogoPath", GetTextBoxValue(txtLogoPath))
        End If

        ' === TAB TIMBANGAN ===
        If UserSession.CanEditIndikator() Then
            settings.Add("COM_PORT", SafeGetComboValue(cmbComPort, "COM3"))
            settings.Add("BAUD_RATE", SafeGetComboValue(cmbBaudRate, "9600"))
            settings.Add("DATA_BITS", SafeGetComboValue(cmbDataBits, "8"))
            settings.Add("PARITY", SafeGetComboValue(cmbParity, "None"))
            settings.Add("STOP_BITS", SafeGetComboValue(cmbStopBits, "1"))
            settings.Add("PROTOCOL", SafeGetComboValue(cmbProtocol, "STANDARD"))
            settings.Add("KapasitasTimbangan", GetTextBoxValue(txtKapasitas))
            settings.Add("DivisiTimbangan", GetTextBoxValue(txtDivisi))
            settings.Add("SCALE_BRAND", SafeGetComboValue(cmbMerekIndikator, "GSC"))
            settings.Add("SCALE_TYPE", SafeGetComboValue(cmbTipeIndikator, ""))
        End If

        ' === TAB PRINTER STRUK (TIKET KELUAR) ===
        settings.Add("PrinterName", SafeGetComboValue(cmbPrinter, ""))
        settings.Add("PaperSize", SafeGetComboValue(cmbPaperSize, "A5"))
        settings.Add("JumlahCopy", GetNumericUpDownValue(nudCopies))

        ' === TAB PRINTER TIKET MASUK (THERMAL) ===
        settings.Add("PrinterNameTkt", SafeGetComboValue(CmbPrinterTkt, ""))
        settings.Add("PaperSizeTkt", SafeGetComboValue(cmbPaperSizeTkt, "58mm"))
        settings.Add("JumlahCopyTkt", GetNumericUpDownValue(NumericUpDown1))

        ' === TAB UMUM ===
        settings.Add("PotonganDefault", GetNumericUpDownValue(nudPotonganDefault))
        settings.Add("WajibSupplier", GetCheckBoxValue(chkWajibSupplier))
        settings.Add("WajibProduk", GetCheckBoxValue(chkWajibProduk))
        settings.Add("WajibTransporter", GetCheckBoxValue(chkWajibTransporter))
        settings.Add("KonfirmasiHapus", GetCheckBoxValue(chkKonfirmasiHapus))
        settings.Add("BackupOtomatis", GetCheckBoxValue(chkBackupOtomatis))

        ' Simpan setiap setting ke database
        For Each kvp In settings
            Try
                ExecuteSave(kvp.Key, kvp.Value)
                savedCount += 1
            Catch ex As Exception
                LogWarning("Failed to save setting " & kvp.Key & ": " & ex.ToString())
                errors.Add(kvp.Key & ": Gagal disimpan")
            End Try
        Next

        Return Tuple.Create(savedCount, errors)
    End Function

    ' =============================================
    ' FUNGSI SIMPAN SETTING KE DATABASE
    ' =============================================
    Private Sub ExecuteSave(key As String, value As String)
        If String.IsNullOrEmpty(key) Then
            Throw New ArgumentException("Setting key tidak boleh kosong")
        End If

        Try
            Dim checkQuery As String = "SELECT COUNT(*) FROM Settings WHERE SettingKey = @Key"
            Dim checkParams As SqlParameter() = {New SqlParameter("@Key", key)}
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(checkQuery, checkParams)

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Throw New Exception("Gagal mengecek existing setting")
            End If

            Dim exists As Integer = CInt(dt.Rows(0)(0))

            If exists > 0 Then
                Dim updateQuery As String = "UPDATE Settings SET SettingValue = @Value, UpdatedAt = GETDATE() WHERE SettingKey = @Key"
                DatabaseHelper.ExecuteNonQuery(updateQuery, {
                    New SqlParameter("@Value", If(value, "")),
                    New SqlParameter("@Key", key)
                })
            Else
                Dim insertQuery As String = "INSERT INTO Settings (SettingKey, SettingValue, CreatedAt, UpdatedAt) VALUES (@Key, @Value, GETDATE(), GETDATE())"
                DatabaseHelper.ExecuteNonQuery(insertQuery, {
                    New SqlParameter("@Key", key),
                    New SqlParameter("@Value", If(value, ""))
                })
            End If

        Catch ex As SqlException
            Debug.WriteLine("[FormPengaturan.SaveSetting] Database error: " & ex.ToString())
            Throw New Exception("Gagal menyimpan pengaturan database.", ex)
        Catch ex As Exception
            Debug.WriteLine("[FormPengaturan.SaveSetting] Error: " & ex.ToString())
            Throw New Exception("Gagal menyimpan pengaturan.", ex)
        End Try
    End Sub
    Private Function GetSavedSetting(key As String, defaultValue As String) As String
        Try
            Dim result As Object = DatabaseHelper.ExecuteScalar(
            "SELECT SettingValue FROM Settings WHERE SettingKey = @Key",
            {New System.Data.SqlClient.SqlParameter("@Key", key)})
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then Return result.ToString()
        Catch ex As Exception
            Debug.WriteLine("[FormPengaturan.GetSetting] Error: " & ex.ToString())
        End Try
        Return defaultValue
    End Function
    ' =============================================
    ' ERROR HANDLING & LOGGING
    ' =============================================
    Private Function HandleException(methodName As String, ex As Exception) As OperationResult
        Dim errCode As ErrorCode = ErrorCode.UNKNOWN_ERROR

        If TypeOf ex Is SqlException Then
            errCode = ErrorCode.DB_QUERY_FAILED
        ElseIf TypeOf ex Is UnauthorizedAccessException Then
            errCode = ErrorCode.FILE_ACCESS_DENIED
        ElseIf TypeOf ex Is FileNotFoundException Then
            errCode = ErrorCode.FILE_NOT_FOUND
        ElseIf TypeOf ex Is IOException AndAlso ex.Message.IndexOf("COM", StringComparison.OrdinalIgnoreCase) >= 0 Then
            errCode = ErrorCode.SERIAL_PORT_NOT_FOUND
        ElseIf TypeOf ex Is TimeoutException Then
            errCode = ErrorCode.SERIAL_PORT_TIMEOUT
        ElseIf TypeOf ex Is ArgumentException Then
            errCode = ErrorCode.INVALID_PARAMETER
        End If

        LogError(methodName, ex)

        ' Keep provider/path/stack details out of user-facing operation results.
        ' Full technical details remain available through the local diagnostic log.
        Return OperationResult.Fail(
            errCode,
            GetSafeErrorMessage(errCode),
            "Operation: " & methodName)
    End Function

    Private Function GetSafeErrorMessage(code As ErrorCode) As String
        Select Case code
            Case ErrorCode.DB_CONNECTION_FAILED, ErrorCode.DB_QUERY_FAILED, ErrorCode.DB_SAVE_FAILED
                Return "Operasi database gagal. Periksa koneksi dan pengaturan database."
            Case ErrorCode.SERIAL_PORT_NOT_FOUND, ErrorCode.SERIAL_PORT_OPEN_FAILED
                Return "Port komunikasi timbangan tidak tersedia atau tidak dapat dibuka."
            Case ErrorCode.SERIAL_PORT_TIMEOUT
                Return "Komunikasi dengan indikator timbangan melebihi batas waktu."
            Case ErrorCode.PRINTER_NOT_FOUND, ErrorCode.PRINTER_OFFLINE, ErrorCode.PRINT_FAILED
                Return "Operasi printer gagal. Periksa printer dan pengaturannya."
            Case ErrorCode.FILE_NOT_FOUND
                Return "File yang diperlukan tidak ditemukan."
            Case ErrorCode.FILE_ACCESS_DENIED
                Return "Akses ke file ditolak oleh sistem operasi."
            Case ErrorCode.INVALID_PARAMETER
                Return "Parameter yang diberikan tidak valid."
            Case ErrorCode.CONTROL_NOT_INITIALIZED
                Return "Komponen aplikasi belum siap digunakan."
            Case Else
                Return "Operasi gagal. Silakan coba lagi."
        End Select
    End Function

    Private Sub LogError(source As String, ex As Exception)
        Dim logEntry As String = "[" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "] ERROR in " & source & ": " & ex.Message
        ErrorLog.Add(logEntry)

        Try
            File.AppendAllText(LOG_FILE, logEntry & vbCrLf)
        Catch ex As Exception
            Debug.WriteLine("[FormPengaturan.LogError] File log write failed: " & ex.ToString())
        End Try

        Debug.WriteLine(logEntry)
    End Sub

    Private Sub LogWarning(message As String)
        Dim logEntry As String = "[" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "] WARNING: " & message
        ErrorLog.Add(logEntry)

        Try
            File.AppendAllText(LOG_FILE, logEntry & vbCrLf)
        Catch ex As Exception
            Debug.WriteLine("[FormPengaturan.LogWarning] File log write failed: " & ex.ToString())
        End Try

        Debug.WriteLine(logEntry)
    End Sub

    Private Sub LogInfo(message As String)
        Dim logEntry As String = "[" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "] INFO: " & message

        Try
            File.AppendAllText(LOG_FILE, logEntry & vbCrLf)
        Catch ex As Exception
            Debug.WriteLine("[FormPengaturan.LogInfo] File log write failed: " & ex.ToString())
        End Try

        Debug.WriteLine(logEntry)
    End Sub

    Private Sub ShowErrorSummary(title As String, errors As List(Of String), Optional details As List(Of String) = Nothing)
        Dim message As New System.Text.StringBuilder()
        message.AppendLine("⚠️ " & title & " selesai dengan beberapa masalah:")
        message.AppendLine()

        Dim showCount As Integer = Math.Min(errors.Count, 5)
        For i As Integer = 0 To showCount - 1
            message.AppendLine("• " & errors(i))
        Next

        If errors.Count > 5 Then
            message.AppendLine("• ... dan " & (errors.Count - 5).ToString() & " error lainnya")
        End If

        If details IsNot Nothing AndAlso details.Count > 0 Then
            message.AppendLine()
            message.AppendLine("Detail:")
            Dim detailCount As Integer = Math.Min(details.Count, 3)
            For i As Integer = 0 To detailCount - 1
                message.AppendLine("  - " & details(i))
            Next
        End If

        message.AppendLine()
        message.AppendLine("Log disimpan di: " & LOG_FILE)

        MessageBox.Show(message.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' =============================================
    ' TAB PERUSAHAAN - BROWSE LOGO
    ' =============================================
    Private Sub btnBrowseLogo_Click(sender As Object, e As EventArgs) Handles btnBrowseLogo.Click
        If Not UserSession.Authorize(UserSession.CanEditPerusahaan(), "ubah logo perusahaan") Then Return

        Try
            Using ofd As New OpenFileDialog()
                ofd.Title = "Pilih Logo Perusahaan"
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"

                If ofd.ShowDialog() = DialogResult.OK Then
                    If Not File.Exists(ofd.FileName) Then
                        MessageBox.Show("File tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    Dim fileInfo As New FileInfo(ofd.FileName)
                    If fileInfo.Length > 5 * 1024 * 1024 Then
                        MessageBox.Show("Ukuran file terlalu besar (max 5MB)!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    txtLogoPath.Text = ofd.FileName

                    If picLogo.Image IsNot Nothing Then
                        picLogo.Image.Dispose()
                        picLogo.Image = Nothing
                    End If

                    Using fs As New FileStream(ofd.FileName, FileMode.Open, FileAccess.Read)
                        picLogo.Image = Image.FromStream(fs)
                    End Using
                End If
            End Using

        Catch ex As Exception
            Dim result = HandleException("btnBrowseLogo_Click", ex)
            MessageBox.Show(result.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnHapusLogo_Click(sender As Object, e As EventArgs) Handles btnHapusLogo.Click
        If Not UserSession.Authorize(UserSession.CanEditPerusahaan(), "hapus logo perusahaan") Then Return

        If MessageBox.Show("Hapus logo perusahaan?", "Konfirmasi",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            txtLogoPath.Clear()
            If picLogo.Image IsNot Nothing Then
                picLogo.Image.Dispose()
                picLogo.Image = Nothing
            End If
        End If
    End Sub

    ' =============================================
    ' RESET DEFAULT
    ' =============================================
    Private Sub btnResetDefault_Click(sender As Object, e As EventArgs) Handles btnResetDefault.Click
        If Not UserSession.Authorize(UserSession.CanEditPerusahaan() AndAlso UserSession.CanEditIndikator(),
                                     "reset pengaturan ke default") Then Return

        If MessageBox.Show("Reset SEMUA pengaturan ke default?", "Konfirmasi Reset",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
            Return
        End If

        Try
            ' Reset Tab Perusahaan
            txtNamaPerusahaan.Text = "Example Palm Oil Mill"
            txtAlamatPerusahaan.Text = "Alamat Perusahaan"
            txtKotaPerusahaan.Text = "Kabupaten"
            txtTeleponPerusahaan.Text = ""
            txtFaxPerusahaan.Text = ""
            txtEmailPerusahaan.Text = ""
            txtLogoPath.Clear()
            If picLogo.Image IsNot Nothing Then
                picLogo.Image.Dispose()
                picLogo.Image = Nothing
            End If

            ' Reset Tab Timbangan
            SafeSelectComboItem(cmbComPort, "COM3")
            SafeSelectComboItem(cmbBaudRate, "9600")
            SafeSelectComboItem(cmbDataBits, "8")
            SafeSelectComboItem(cmbParity, "None")
            SafeSelectComboItem(cmbStopBits, "1")
            SafeSelectComboItem(cmbProtocol, "STANDARD")
            SafeSelectComboItem(cmbMerekIndikator, "GSC")
            txtKapasitas.Text = "60000"
            txtDivisi.Text = "10"

            ' Reset Tab Printer Struk
            SafeSelectComboItem(cmbPaperSize, "A5 (148 x 210 mm)")
            nudCopies.Value = 1

            ' Reset Tab Printer Tiket Masuk (Thermal)
            SafeSelectComboItem(cmbPaperSizeTkt, "58mm")
            NumericUpDown1.Value = 1

            ' Reset Tab Umum
            nudPotonganDefault.Value = 0
            chkWajibSupplier.Checked = True
            chkWajibProduk.Checked = True
            chkWajibTransporter.Checked = True
            chkKonfirmasiHapus.Checked = True
            chkBackupOtomatis.Checked = True

            ' Reset My.Settings
            My.Settings.LastComPort = ""
            My.Settings.LastBaudRate = "9600"
            My.Settings.LastDataBits = "8"
            My.Settings.LastParity = "None"
            My.Settings.LastStopBits = "1"
            My.Settings.LastProtocol = "STANDARD"
            My.Settings.LastKapasitas = ""
            My.Settings.LastDivisi = ""
            My.Settings.Save()

            MessageBox.Show("Pengaturan direset ke default. Klik SIMPAN untuk menyimpan ke database.",
                           "Reset Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Dim result = HandleException("btnResetDefault_Click", ex)
            MessageBox.Show(result.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL BATAL & TUTUP
    ' =============================================
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        If MessageBox.Show("Batalkan perubahan?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Close()
    End Sub

    ' =============================================
    ' FORM CLOSING - CLEANUP
    ' =============================================
    Private Sub FormPengaturan_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If picLogo.Image IsNot Nothing Then
                picLogo.Image.Dispose()
            End If

            Try
                DatabaseHelper.InsertAuditLog(
                    UserSession.UserID,
                    "CLOSE_FORM",
                    Nothing,
                    "Pengaturan",
                    Nothing,
                    Nothing,
                    Nothing,
                    "User menutup Pengaturan"
                )
            Catch exAudit As Exception
                Debug.WriteLine($"[AuditLog] Error: {exAudit.Message}")
            End Try

            If ErrorLog.Count > 0 Then
                LogInfo("Form closed with " & ErrorLog.Count.ToString() & " logged errors")
            End If
        Catch ex As Exception
            Debug.WriteLine($"[FormClosing] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' REGION: TAB DATABASE
    ' =============================================
#Region "Tab Database"

    Private Sub LoadDatabaseSettings()
        Try
            Dim serverName As String = ""
            Dim dbName As String = ""
            Dim useWindowsAuth As Boolean = True
            Dim sqlUsername As String = ""
            Dim sqlPassword As String = ""

            DatabaseHelper.GetSavedConnectionSettings(
                serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword)

            txtServerName.Text = serverName
            txtDatabaseName.Text = dbName

            If useWindowsAuth Then
                rbWindowsAuth.Checked = True
            Else
                rbSQLAuth.Checked = True
            End If

            txtSQLUsername.Text = sqlUsername
            txtSQLPassword.Text = sqlPassword

            UpdateSQLAuthFields()
            UpdateCurrentConnectionInfo(serverName, dbName, useWindowsAuth, sqlUsername)

            LogInfo("Database settings loaded - Server: " & serverName & ", DB: " & dbName)

        Catch ex As Exception
            LogError("LoadDatabaseSettings", ex)
        End Try
    End Sub

    Private Sub UpdateCurrentConnectionInfo(server As String, db As String, winAuth As Boolean, username As String)
        Try
            lblCurrentServer.Text = "Server         : " & server
            lblCurrentDB.Text = "Database    : " & db

            If winAuth Then
                lblCurrentAuth.Text = "Autentikasi : Windows Authentication"
            Else
                lblCurrentAuth.Text = "Autentikasi : SQL Server (" & username & ")"
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormPengaturan.UpdateCurrentAuthInfo] Error: " & ex.ToString())
        End Try
    End Sub

    Private Sub UpdateSQLAuthFields()
        Try
            Dim useSQLAuth As Boolean = rbSQLAuth.Checked

            lblUsername.Enabled = useSQLAuth
            txtSQLUsername.Enabled = useSQLAuth
            txtSQLUsername.BackColor = If(useSQLAuth, Color.White, Color.FromArgb(240, 240, 240))

            lblPassword.Enabled = useSQLAuth
            txtSQLPassword.Enabled = useSQLAuth
            txtSQLPassword.BackColor = If(useSQLAuth, Color.White, Color.FromArgb(240, 240, 240))
        Catch ex As Exception
            LogWarning("UpdateSQLAuthFields error: " & ex.Message)
        End Try
    End Sub

    Private Sub rbWindowsAuth_CheckedChanged(sender As Object, e As EventArgs) Handles rbWindowsAuth.CheckedChanged
        UpdateSQLAuthFields()
    End Sub

    Private Sub rbSQLAuth_CheckedChanged(sender As Object, e As EventArgs) Handles rbSQLAuth.CheckedChanged
        UpdateSQLAuthFields()
    End Sub

    Private Sub btnTestDB_Click(sender As Object, e As EventArgs) Handles btnTestDB.Click
        If Not UserSession.Authorize(UserSession.CanEditDatabaseConnection(), "uji koneksi database") Then Return

        ' Validasi input
        If String.IsNullOrWhiteSpace(txtServerName.Text) Then
            MessageBox.Show("Server Name tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtServerName.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtDatabaseName.Text) Then
            MessageBox.Show("Database Name tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDatabaseName.Focus()
            Return
        End If

        If rbSQLAuth.Checked AndAlso String.IsNullOrWhiteSpace(txtSQLUsername.Text) Then
            MessageBox.Show("Username tidak boleh kosong untuk SQL Authentication!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSQLUsername.Focus()
            Return
        End If

        btnTestDB.Enabled = False
        btnTestDB.Text = "⏳ Testing..."
        lblStatusDB.Text = "Status: Sedang menguji koneksi..."
        lblStatusDB.ForeColor = Color.FromArgb(230, 126, 34)
        pnlStatusDB.BackColor = Color.FromArgb(255, 248, 225)
        Application.DoEvents()

        Try
            Dim serverName As String = txtServerName.Text.Trim()
            Dim dbName As String = txtDatabaseName.Text.Trim()
            Dim useWindowsAuth As Boolean = rbWindowsAuth.Checked
            Dim sqlUsername As String = txtSQLUsername.Text.Trim()
            Dim sqlPassword As String = txtSQLPassword.Text

            Dim errorMessage As String = ""
            Dim connectionFailure As DatabaseHelper.DatabaseConnectionFailureType = DatabaseHelper.DatabaseConnectionFailureType.Unknown

            Dim success As Boolean = DatabaseHelper.TestConnectionCustom(
                serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword, errorMessage, connectionFailure)

            If success Then
                lblStatusDB.Text = "Status: ✅ Koneksi berhasil!"
                lblStatusDB.ForeColor = Color.FromArgb(39, 174, 96)
                pnlStatusDB.BackColor = Color.FromArgb(232, 245, 233)

                MessageBox.Show("✅ Koneksi ke database berhasil!" & vbCrLf & vbCrLf &
                               "Server: " & serverName & vbCrLf &
                               "Database: " & dbName & vbCrLf &
                               "Autentikasi: " & If(useWindowsAuth, "Windows", "SQL Server"),
                               "Test Koneksi Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Try
                    DatabaseHelper.InsertAuditLog(UserSession.UserID, "TEST_DB_CONNECTION_SUCCESS",
                        Nothing, "Settings", Nothing, Nothing, Nothing,
                        "Test koneksi DB berhasil! - Server : " & serverName & ", DB : " & dbName)
                Catch exAudit As Exception
                    Debug.WriteLine("[FormPengaturan.TestDatabaseConnection] Audit log write failed: " & exAudit.ToString())
                End Try
            Else
                lblStatusDB.Text = "Status: ❌ Koneksi gagal!"
                lblStatusDB.ForeColor = Color.FromArgb(192, 57, 43)
                pnlStatusDB.BackColor = Color.FromArgb(255, 235, 238)

                If connectionFailure = DatabaseHelper.DatabaseConnectionFailureType.DatabaseNotFound Then
                    Dim createDB = MessageBox.Show(
                        "Database '" & dbName & "' tidak ditemukan di server." & vbCrLf & vbCrLf &
                        "Apakah Anda ingin membuat database baru?",
                        "Database Tidak Ditemukan", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If createDB = DialogResult.Yes Then
                        CreateNewDatabase(serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword)
                    End If
                Else
                    MessageBox.Show("❌ Koneksi gagal!" & vbCrLf & vbCrLf &
                                   errorMessage & vbCrLf & vbCrLf &
                                   "Tips:" & vbCrLf &
                                   "• Pastikan SQL Server sudah berjalan" & vbCrLf &
                                   "• Periksa nama server (contoh: .\SQLEXPRESS)" & vbCrLf &
                                   "• Periksa firewall jika koneksi remote",
                                   "Test Koneksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Catch ex As Exception
            lblStatusDB.Text = "Status: ❌ Terjadi kesalahan saat menguji koneksi"
            lblStatusDB.ForeColor = Color.FromArgb(192, 57, 43)
            pnlStatusDB.BackColor = Color.FromArgb(255, 235, 238)
            MessageBox.Show("Gagal menguji koneksi database. Periksa pengaturan server dan autentikasi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            btnTestDB.Enabled = True
            btnTestDB.Text = "🔌 Test Koneksi"
        End Try
    End Sub

    Private Sub CreateNewDatabase(serverName As String, dbName As String, useWindowsAuth As Boolean,
                                  sqlUsername As String, sqlPassword As String)
        Try
            btnTestDB.Enabled = False
            btnTestDB.Text = "⏳ Membuat DB..."
            lblStatusDB.Text = "Status: Membuat database baru..."
            Application.DoEvents()

            Dim errorMessage As String = ""
            Dim success As Boolean = DatabaseHelper.EnsureDatabaseExists(
                serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword, errorMessage)

            If success Then
                lblStatusDB.Text = "Status: ✅ Database berhasil dibuat!"
                lblStatusDB.ForeColor = Color.FromArgb(39, 174, 96)
                pnlStatusDB.BackColor = Color.FromArgb(232, 245, 233)

                MessageBox.Show("✅ Database '" & dbName & "' berhasil dibuat!" & vbCrLf & vbCrLf &
                               "PENTING: Anda perlu menjalankan script SQL untuk membuat tabel-tabel yang diperlukan.",
                               "Database Dibuat", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                lblStatusDB.Text = "Status: ❌ Gagal membuat database"
                lblStatusDB.ForeColor = Color.FromArgb(192, 57, 43)

                MessageBox.Show("Gagal membuat database:" & vbCrLf & errorMessage,
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal membuat database. Periksa hak akses dan pengaturan koneksi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            btnTestDB.Enabled = True
            btnTestDB.Text = "🔌 Test Koneksi"
        End Try
    End Sub

    Private Sub btnSimpanDB_Click(sender As Object, e As EventArgs) Handles btnSimpanDB.Click
        If Not UserSession.Authorize(UserSession.CanEditDatabaseConnection(), "ubah koneksi database") Then Return

        ' Validasi
        If String.IsNullOrWhiteSpace(txtServerName.Text) Then
            MessageBox.Show("Server Name tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtDatabaseName.Text) Then
            MessageBox.Show("Database Name tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirm = MessageBox.Show(
            "Simpan pengaturan koneksi database?" & vbCrLf & vbCrLf &
            "Server: " & txtServerName.Text & vbCrLf &
            "Database: " & txtDatabaseName.Text & vbCrLf &
            "Autentikasi: " & If(rbWindowsAuth.Checked, "Windows", "SQL Server") & vbCrLf & vbCrLf &
            "Aplikasi akan menggunakan pengaturan ini untuk koneksi database.",
            "Konfirmasi Simpan", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirm = DialogResult.No Then Return

        Try
            btnSimpanDB.Enabled = False
            btnSimpanDB.Text = "⏳ Menyimpan..."
            Application.DoEvents()

            DatabaseHelper.SaveConnectionConfig(
                txtServerName.Text.Trim(),
                txtDatabaseName.Text.Trim(),
                rbWindowsAuth.Checked,
                txtSQLUsername.Text.Trim(),
                txtSQLPassword.Text
            )

            UpdateCurrentConnectionInfo(
                txtServerName.Text.Trim(),
                txtDatabaseName.Text.Trim(),
                rbWindowsAuth.Checked,
                txtSQLUsername.Text.Trim()
            )

            SettingsHelper.ClearCache()

            lblStatusDB.Text = "Status: ✅ Pengaturan tersimpan!"
            lblStatusDB.ForeColor = Color.FromArgb(39, 174, 96)
            pnlStatusDB.BackColor = Color.FromArgb(232, 245, 233)

            MessageBox.Show("✅ Pengaturan koneksi database berhasil disimpan!" & vbCrLf & vbCrLf &
                           "Pengaturan akan langsung digunakan untuk koneksi berikutnya.",
                           "Simpan Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Try
                DatabaseHelper.InsertAuditLog(UserSession.UserID, "SAVE_DB_CONFIG",
                    Nothing, "Settings", Nothing, Nothing, Nothing,
                    "Simpan config DB - Server : " & txtServerName.Text & ", DB : " & txtDatabaseName.Text)
            Catch exAudit As Exception
                Debug.WriteLine("[FormPengaturan.SaveDatabaseConfig] Audit log write failed: " & exAudit.ToString())
            End Try

        Catch ex As Exception
            lblStatusDB.Text = "Status: ❌ Gagal menyimpan"
            lblStatusDB.ForeColor = Color.FromArgb(192, 57, 43)

            MessageBox.Show("Gagal menyimpan pengaturan. Periksa nilai yang dimasukkan dan koneksi database.",
                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            btnSimpanDB.Enabled = True
            btnSimpanDB.Text = "💾 Simpan && Terapkan"
        End Try
    End Sub


#End Region

End Class