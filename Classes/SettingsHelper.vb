' =============================================
' SettingsHelper.vb - Helper untuk membaca Settings dari Database
' Lokasi: Classes/SettingsHelper.vb
' =============================================
Imports System.Data.SqlClient

Public Class SettingsHelper

    ' =============================================
    ' CACHE SETTINGS
    ' =============================================
    Private Shared _settingsCache As Dictionary(Of String, String) = Nothing
    Private Shared _cacheExpiry As DateTime = DateTime.MinValue
    Private Const CACHE_DURATION_MINUTES As Integer = 5

    ' =============================================
    ' FUNGSI UTAMA: AMBIL SETTING DARI DATABASE
    ' =============================================

    ''' <summary>
    ''' Ambil nilai setting dari database berdasarkan key
    ''' </summary>
    ''' <param name="key">Nama setting (SettingKey)</param>
    ''' <param name="defaultValue">Nilai default jika tidak ditemukan</param>
    ''' <returns>Nilai setting atau default</returns>
    Public Shared Function GetSetting(key As String, Optional defaultValue As String = "") As String
        Try
            ' Jika cache kosong atau expired, load semua settings
            If _settingsCache Is Nothing OrElse DateTime.Now >= _cacheExpiry Then
                LoadAllSettings()
            End If

            ' Ambil dari cache
            If _settingsCache IsNot Nothing AndAlso _settingsCache.ContainsKey(key) Then
                Return _settingsCache(key)
            End If

            Return defaultValue

        Catch ex As Exception
            Debug.WriteLine($"[SettingsHelper] Error getting '{key}': {ex.Message}")
            Return defaultValue
        End Try
    End Function

    ''' <summary>
    ''' Load semua settings dari database ke cache
    ''' </summary>
    Public Shared Sub LoadAllSettings()
        Try
            _settingsCache = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

            Dim query As String = "SELECT SettingKey, SettingValue FROM Settings"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim key As String = ""
                    Dim value As String = ""

                    If row("SettingKey") IsNot Nothing AndAlso Not IsDBNull(row("SettingKey")) Then
                        key = row("SettingKey").ToString().Trim()
                    End If

                    If row("SettingValue") IsNot Nothing AndAlso Not IsDBNull(row("SettingValue")) Then
                        value = row("SettingValue").ToString()
                    End If

                    If Not String.IsNullOrEmpty(key) Then
                        _settingsCache(key) = value
                    End If
                Next

                Debug.WriteLine($"[SettingsHelper] ✓ Loaded {_settingsCache.Count} settings from database")
            Else
                Debug.WriteLine("[SettingsHelper] ⚠ No settings found in database")
            End If

            _cacheExpiry = DateTime.Now.AddMinutes(CACHE_DURATION_MINUTES)

        Catch ex As Exception
            Debug.WriteLine($"[SettingsHelper] ✗ Error loading settings: {ex.Message}")
            _settingsCache = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        End Try
    End Sub

    ''' <summary>
    ''' Clear cache - WAJIB dipanggil setelah simpan di FormPengaturan!
    ''' </summary>
    Public Shared Sub ClearCache()
        _settingsCache = Nothing
        _cacheExpiry = DateTime.MinValue
        Debug.WriteLine("[SettingsHelper] ✓ Cache cleared")
    End Sub

    ''' <summary>
    ''' Refresh cache - clear lalu load ulang dari database
    ''' </summary>
    Public Shared Sub RefreshCache()
        ClearCache()
        LoadAllSettings()
        Debug.WriteLine("[SettingsHelper] ✓ Cache refreshed")
    End Sub

    ' =============================================
    ' PROPERTY: DATA PERUSAHAAN
    ' =============================================

    ''' <summary>
    ''' Nama Perusahaan dari Settings
    ''' </summary>
    Public Shared ReadOnly Property NamaPerusahaan As String
        Get
            Dim value As String = GetSetting("NamaPerusahaan", "")
            If String.IsNullOrEmpty(value) Then
                value = "Example Palm Oil Mill"
            End If
            Debug.WriteLine($"[SettingsHelper] NamaPerusahaan = '{value}'")
            Return value
        End Get
    End Property

    ''' <summary>
    ''' Alamat Perusahaan dari Settings
    ''' </summary>
    Public Shared ReadOnly Property AlamatPerusahaan As String
        Get
            Dim value As String = GetSetting("AlamatPerusahaan", "")
            Debug.WriteLine($"[SettingsHelper] AlamatPerusahaan = '{value}'")
            Return value
        End Get
    End Property

    ''' <summary>
    ''' Kota Perusahaan dari Settings
    ''' </summary>
    Public Shared ReadOnly Property KotaPerusahaan As String
        Get
            Return GetSetting("KotaPerusahaan", "")
        End Get
    End Property

    ''' <summary>
    ''' Telepon Perusahaan dari Settings
    ''' </summary>
    Public Shared ReadOnly Property TeleponPerusahaan As String
        Get
            Return GetSetting("TeleponPerusahaan", "")
        End Get
    End Property

    ''' <summary>
    ''' Fax Perusahaan dari Settings
    ''' </summary>
    Public Shared ReadOnly Property FaxPerusahaan As String
        Get
            Return GetSetting("FaxPerusahaan", "")
        End Get
    End Property

    ''' <summary>
    ''' Email Perusahaan dari Settings
    ''' </summary>
    Public Shared ReadOnly Property EmailPerusahaan As String
        Get
            Return GetSetting("EmailPerusahaan", "")
        End Get
    End Property

    ''' <summary>
    ''' Path Logo Perusahaan dari Settings
    ''' </summary>
    Public Shared ReadOnly Property LogoPath As String
        Get
            Return GetSetting("LogoPath", "")
        End Get
    End Property

    ' =============================================
    ' PROPERTY: PENGATURAN TIKET
    ' =============================================

    ''' <summary>
    ''' Prefix Nomor Tiket (contoh: CPO, TBS)
    ''' </summary>
    Public Shared ReadOnly Property TiketPrefix As String
        Get
            Dim value As String = GetSetting("TIKET_PREFIX", "")
            If String.IsNullOrEmpty(value) Then
                value = GetSetting("Prefix", "TKT")
            End If
            Return value
        End Get
    End Property

    ''' <summary>
    ''' Jumlah digit nomor urut tiket
    ''' </summary>
    Public Shared ReadOnly Property JumlahDigit As Integer
        Get
            Dim value As String = GetSetting("JumlahDigit", "6")
            Dim result As Integer = 6
            Integer.TryParse(value, result)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' Reset harian nomor tiket
    ''' </summary>
    Public Shared ReadOnly Property ResetHarian As Boolean
        Get
            Dim value As String = GetSetting("ResetHarian", "1")
            Return value = "1" OrElse String.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ' =============================================
    ' PROPERTY: PENGATURAN PRINTER
    ' =============================================

    ''' <summary>
    ''' Nama Printer yang dipilih
    ''' </summary>
    Public Shared ReadOnly Property NamaPrinter As String
        Get
            Dim value As String = GetSetting("PrinterName", "")
            If String.IsNullOrEmpty(value) Then
                value = GetSetting("NamaPrinter", "")
            End If
            Return value
        End Get
    End Property

    ''' <summary>
    ''' Ukuran Kertas (A4, A5, dll)
    ''' </summary>
    Public Shared ReadOnly Property UkuranKertas As String
        Get
            Dim value As String = GetSetting("PaperSize", "")
            If String.IsNullOrEmpty(value) Then
                value = GetSetting("UkuranKertas", "A5")
            End If
            Return value
        End Get
    End Property

    ''' <summary>
    ''' Jumlah copy saat cetak
    ''' </summary>
    Public Shared ReadOnly Property JumlahCopy As Integer
        Get
            Dim value As String = GetSetting("JumlahCopy", "1")
            Dim result As Integer = 1
            Integer.TryParse(value, result)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' Auto print setelah timbang
    ''' </summary>
    Public Shared ReadOnly Property AutoPrint As Boolean
        Get
            Dim value As String = GetSetting("AutoPrint", "")
            If String.IsNullOrEmpty(value) Then
                value = GetSetting("CetakOtomatis", "0")
            End If
            Return value = "1" OrElse String.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ' =============================================
    ' PROPERTY: PENGATURAN TIMBANGAN
    ' =============================================

    ''' <summary>
    ''' COM Port timbangan
    ''' </summary>
    Public Shared ReadOnly Property ComPort As String
        Get
            Dim value As String = GetSetting("COM_PORT", "")
            If String.IsNullOrEmpty(value) Then
                value = GetSetting("ComPort", "COM3")
            End If
            Return value
        End Get
    End Property

    ''' <summary>
    ''' Baud Rate timbangan
    ''' </summary>
    Public Shared ReadOnly Property BaudRate As Integer
        Get
            Dim value As String = GetSetting("BAUD_RATE", "")
            If String.IsNullOrEmpty(value) Then
                value = GetSetting("BaudRate", "9600")
            End If
            Dim result As Integer = 9600
            Integer.TryParse(value, result)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' Protocol timbangan
    ''' </summary>
    Public Shared ReadOnly Property Protocol As String
        Get
            Return GetSetting("Protocol", "STANDARD")
        End Get
    End Property

    ' =============================================
    ' PROPERTY: PENGATURAN UMUM
    ' =============================================

    ''' <summary>
    ''' Potongan default (kg)
    ''' </summary>
    Public Shared ReadOnly Property PotonganDefault As Decimal
        Get
            Dim value As String = GetSetting("PotonganDefault", "0")
            Dim result As Decimal = 0
            Decimal.TryParse(value, result)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' Wajib pilih supplier
    ''' </summary>
    Public Shared ReadOnly Property WajibSupplier As Boolean
        Get
            Dim value As String = GetSetting("WajibSupplier", "1")
            Return value = "1" OrElse String.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ''' <summary>
    ''' Wajib pilih produk
    ''' </summary>
    Public Shared ReadOnly Property WajibProduk As Boolean
        Get
            Dim value As String = GetSetting("WajibProduk", "1")
            Return value = "1" OrElse String.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ''' <summary>
    ''' Konfirmasi sebelum hapus
    ''' </summary>
    Public Shared ReadOnly Property KonfirmasiHapus As Boolean
        Get
            Dim value As String = GetSetting("KonfirmasiHapus", "1")
            Return value = "1" OrElse String.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

End Class
