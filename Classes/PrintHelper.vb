' ============================================= 
' PrintHelper.vb 
' LAYOUT STYLE WILMAR - FINAL CLEAN V2
' Kertas: Continuous Form 9.5" x 5.5"
' MODIFIKASI:
' - Tanggal cetak di atas No. Tiket
' - No. SIM di bawah Transporter
' - G. Note dihilangkan
' - Trans.Type ditambahkan di bawah Commodity
' ============================================= 
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class PrintHelper

    Public Class DataCetak
        Public Property TimbangID As Integer = 0
        Public Property NoTiket As String = ""
        Public Property NoDO As String = ""
        Public Property NoKontrak As String = ""
        Public Property NoPolisi As String = ""
        Public Property NamaSupir As String = ""
        Public Property NoSIM As String = ""
        Public Property Transporter As String = ""
        Public Property Customer As String = ""
        Public Property Product As String = ""
        Public Property Alamat As String = ""
        Public Property TransType As String = "JUAL"
        Public Property Supplier As String = ""
        Public Property Tujuan As String = ""
        Public Property BeratBruto As Decimal = 0
        Public Property BeratTara As Decimal = 0
        Public Property BeratNetto As Decimal = 0
        Public Property TotalPotongan As Decimal = 0
        Public Property BeratBersih As Decimal = 0
        Public Property PotonganPersen As Decimal = 0
        Public Property PotonganKg As Decimal = 0
        Public Property WaktuMasuk As DateTime = DateTime.Now
        Public Property WaktuKeluar As DateTime = DateTime.Now
        Public Property WaktuBruto As String = ""
        Public Property WaktuTara As String = ""
        Public Property TanggalCetak As DateTime = DateTime.Now
        Public Property IncludeFFA As Boolean = False
        Public Property FFA As Decimal = 0
        Public Property Moisture As Decimal = 0
        Public Property Dirt As Decimal = 0
        Public Property SuhuMinyak As Decimal = 0
        Public Property IncludeKeterangan As Boolean = False
        Public Property Keterangan As String = ""
        Public Property SegelAtas As String = ""
        Public Property SegelBawah As String = ""
        Public Property NamaPetugas As String = ""
        Public Property NamaManager As String = ""
        Public Property IsDuplikat As Boolean = False
    End Class

    Private Shared _dataCetak As DataCetak

#Region "THERMAL PRINTER - TIKET MASUK"

    ' =============================================
    ' KONSTANTA THERMAL PRINTER
    ' =============================================
    Private Shared THERMAL_WIDTH_58MM As Integer = 300
    Private Shared THERMAL_WIDTH_80MM As Integer = 450
    Private Shared THERMAL_MARGIN As Integer = 5
    Private Shared THERMAL_LINE_HEIGHT As Integer = 18
    Private Shared THERMAL_LINE_HEIGHT_SMALL As Integer = 16

    ' =============================================
    ' CETAK TIKET MASUK - THERMAL PRINTER
    ' =============================================
    Public Shared Sub CetakTiketMasukThermal(data As DataCetak, Optional forcePreview As Boolean? = Nothing)
        _dataCetak = data
        _dataCetak.TanggalCetak = DateTime.Now
        ExecutePrintThermal(AddressOf PrintTiketMasukThermalHandler, forcePreview)
    End Sub

    Public Shared Sub PreviewTiketMasukThermal(data As DataCetak)
        _dataCetak = data
        _dataCetak.TanggalCetak = DateTime.Now
        ExecutePrintThermal(AddressOf PrintTiketMasukThermalHandler, forcePreview:=True)
    End Sub

    ' =============================================
    ' EXECUTE PRINT THERMAL
    ' =============================================
    Private Shared Sub ExecutePrintThermal(handler As PrintPageEventHandler, Optional forcePreview As Boolean? = Nothing)
        Dim pd As PrintDocument = Nothing
        Try
            pd = New PrintDocument()

            ' Ambil nama printer
            Dim printerName As String = GetSetting("PrinterNameTkt", "")
            If String.IsNullOrEmpty(printerName) Then printerName = GetSetting("PrinterThermal", "")
            If String.IsNullOrEmpty(printerName) Then printerName = GetSetting("PrinterName", "")
            If Not String.IsNullOrEmpty(printerName) Then pd.PrinterSettings.PrinterName = printerName

            ' Ambil ukuran kertas
            Dim paperWidth As Integer = If(GetSetting("PaperSizeTkt", "58mm").Contains("80"), 80, 58)
            Dim paperWidthPixel As Integer = If(paperWidth = 80, THERMAL_WIDTH_80MM, THERMAL_WIDTH_58MM)

            ' PERBAIKAN PENTING: Panjang kertas ditambah jadi 900 agar tidak kepotong
            Dim paperHeight As Integer = 700

            pd.DefaultPageSettings.PaperSize = New PaperSize("Thermal", paperWidthPixel, paperHeight)
            pd.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
            pd.OriginAtMargins = False

            AddHandler pd.PrintPage, handler

            Dim showPreview As Boolean = If(forcePreview.HasValue, forcePreview.Value, Not GetSettingBool("CetakOtomatisThermal", False))

            If showPreview Then
                Using preview As New PrintPreviewDialog()
                    preview.Document = pd
                    preview.WindowState = FormWindowState.Maximized
                    preview.ShowDialog()
                End Using
            Else
                pd.Print()
            End If

        Catch ex As Exception
            Debug.WriteLine("[PrintHelper.ExecutePrintThermal] Error: " & ex.ToString())
            MessageBox.Show("Gagal mencetak tiket thermal. Periksa printer dan pengaturannya.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If pd IsNot Nothing Then
                RemoveHandler pd.PrintPage, handler
                pd.Dispose()
            End If
        End Try
    End Sub

    ' =============================================
    ' HANDLER CETAK TIKET MASUK THERMAL
    ' =============================================
    Private Shared Sub PrintTiketMasukThermalHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics

        Dim paperWidth As Integer = GetSettingInt("ThermalPaperWidth", 58)
        Dim maxWidth As Integer = If(paperWidth = 80, THERMAL_WIDTH_80MM, THERMAL_WIDTH_58MM)
        Dim x As Integer = THERMAL_MARGIN

        ' Ukuran Font
        Dim fCompany As New Font("Courier New", If(paperWidth = 80, 11, 10), FontStyle.Bold)
        Dim fAddress As New Font("Courier New", If(paperWidth = 80, 9, 8), FontStyle.Regular)
        Dim fTitle As New Font("Courier New", If(paperWidth = 80, 10, 9), FontStyle.Regular)
        Dim fLabel As New Font("Courier New", If(paperWidth = 80, 9, 8), FontStyle.Regular)
        Dim fValue As New Font("Courier New", If(paperWidth = 80, 9, 8), FontStyle.Regular)
        Dim fPesan As New Font("Courier New", If(paperWidth = 80, 9, 8), FontStyle.Regular)
        Dim fSmall As New Font("Courier New", If(paperWidth = 80, 8, 7), FontStyle.Regular)

        Dim y As Integer = THERMAL_MARGIN + 5
        Dim lineH As Integer = If(paperWidth = 80, THERMAL_LINE_HEIGHT, THERMAL_LINE_HEIGHT_SMALL)

        Try
            ' --- HEADER --- 
            Dim namaPerusahaan As String = GetSetting("NamaPerusahaan", "Example Palm Oil Mill")
            PrintCenterText(g, namaPerusahaan, fCompany, y, maxWidth)
            y += lineH + 2

            Dim alamat1 As String = GetSetting("AlamatPerusahaan", "Example Palm Oil Mill Address")
            PrintCenterText(g, alamat1, fAddress, y, maxWidth)
            y += lineH

            Dim alamat2 As String = GetSetting("KotaPerusahaan", "Example Region") & " - " & GetSetting("KodePos", "00000")
            PrintCenterText(g, alamat2, fAddress, y, maxWidth)
            y += lineH

            ' ---> TAMBAHAN: TELEPON PERUSAHAAN <---
            Dim telepon As String = GetSetting("TeleponPerusahaan", "")
            If Not String.IsNullOrEmpty(telepon) Then
                PrintCenterText(g, "Telp: " & telepon, fAddress, y, maxWidth)
                y += lineH
            End If
            ' ---> AKHIR TAMBAHAN <---

            y += 5
            PrintDashedLineStr(g, fAddress, y, maxWidth)
            y += lineH

            PrintCenterText(g, "TIKET MASUK", fTitle, y, maxWidth)
            y += lineH
            PrintCenterText(g, _dataCetak.Product, fTitle, y, maxWidth)
            y += lineH + 5

            PrintDashedLineStr(g, fAddress, y, maxWidth)
            y += lineH + 5

            ' --- INFO TIKET & KENDARAAN ---
            Dim lblW As Integer = If(paperWidth = 80, 100, 90)
            Dim titikDuaX As Integer = x + lblW
            Dim valueX As Integer = titikDuaX + 10

            DrawThermalRow(g, fLabel, fValue, "No. Tiket", _dataCetak.NoTiket, x, y, titikDuaX, valueX)
            y += lineH
            DrawThermalRow(g, fLabel, fValue, "No. DO", _dataCetak.NoDO, x, y, titikDuaX, valueX)
            y += lineH
            DrawThermalRow(g, fLabel, fValue, "Tgl", _dataCetak.WaktuMasuk.ToString("dd/MM/yyyy"), x, y, titikDuaX, valueX)
            y += lineH
            DrawThermalRow(g, fLabel, fValue, "Jam", _dataCetak.WaktuMasuk.ToString("HH:mm") & " WIB", x, y, titikDuaX, valueX)
            y += lineH
            DrawThermalRow(g, fLabel, fValue, "No. Pol", _dataCetak.NoPolisi, x, y, titikDuaX, valueX)
            y += lineH
            DrawThermalRow(g, fLabel, fValue, "Supir", _dataCetak.NamaSupir, x, y, titikDuaX, valueX)
            y += lineH
            DrawThermalRow(g, fLabel, fValue, "Customer", _dataCetak.Customer, x, y, titikDuaX, valueX)
            y += lineH

            ' --- DATA BERAT ---
            DrawThermalRow(g, fLabel, fValue, "BRUTTO", _dataCetak.BeratBruto.ToString("#,##0") & " KG", x, y, titikDuaX, valueX)
            y += lineH
            DrawThermalRow(g, fLabel, fValue, "TARA", If(_dataCetak.BeratTara = 0, "-", _dataCetak.BeratTara.ToString("#,##0") & " KG"), x, y, titikDuaX, valueX)
            y += lineH
            DrawThermalRow(g, fLabel, fValue, "NETTO", If(_dataCetak.BeratNetto = 0, "-", _dataCetak.BeratNetto.ToString("#,##0") & " KG"), x, y, titikDuaX, valueX)
            y += lineH + 5

            PrintDashedLineStr(g, fAddress, y, maxWidth)
            y += lineH + 5

            ' --- PESAN ---
            PrintCenterText(g, "SIMPAN UNTUK", fPesan, y, maxWidth)
            y += lineH
            PrintCenterText(g, "TIMBANG KELUAR", fPesan, y, maxWidth)
            y += lineH + 30

            ' --- TANDA TANGAN ---
            g.DrawString("Petugas Timbang", fLabel, Brushes.Black, x, y)

            ' =========================================================
            ' TRIK PRINTER THERMAL: Memaksa kertas keluar lebih panjang
            ' =========================================================
            y += 100 ' <--- Ubah angka ini jika ingin kertas lebih panjang lagi (misal 100 atau 120)

            ' Kita buat font yang sangat kecil (ukuran 1 piksel)
            Using fTitikSakti As New Font("Courier New", 1, FontStyle.Regular)
                ' Mencetak titik yang nyaris tidak terlihat di ujung kertas
                g.DrawString(".", fTitikSakti, Brushes.Black, x, y)
            End Using

        Finally
            fCompany.Dispose()
            fAddress.Dispose()
            fTitle.Dispose()
            fLabel.Dispose()
            fValue.Dispose()
            fPesan.Dispose()
            fSmall.Dispose()
        End Try

        e.HasMorePages = False
    End Sub


    ' =============================================
    ' HELPER METHODS - THERMAL PRINTER
    ' =============================================

    ''' <summary>
    ''' Mencetak baris Data (Key : Value) dengan presisi posisi
    ''' </summary>
    Private Shared Sub DrawThermalRow(g As Graphics, fLabel As Font, fValue As Font, label As String, value As String, startX As Integer, y As Integer, titikDuaX As Integer, valueX As Integer)
        g.DrawString(label, fLabel, Brushes.Black, startX, y)
        g.DrawString(":", fLabel, Brushes.Black, titikDuaX, y)
        Dim displayValue As String = If(String.IsNullOrEmpty(value), "-", value)
        g.DrawString(displayValue, fValue, Brushes.Black, valueX, y)
    End Sub

    Private Shared Sub PrintCenterText(g As Graphics, text As String, font As Font, y As Integer, paperWidth As Integer)
        If String.IsNullOrEmpty(text) Then Return
        Dim textSize As SizeF = g.MeasureString(text, font)
        Dim x As Single = (paperWidth - textSize.Width) / 2
        If x < 0 Then x = 0
        g.DrawString(text, font, Brushes.Black, x, y)
    End Sub

    Private Shared Sub PrintDashedLineStr(g As Graphics, font As Font, y As Integer, paperWidth As Integer)
        Dim dashStr As String = "--------------------------------------------------------"
        While g.MeasureString(dashStr, font).Width > paperWidth AndAlso dashStr.Length > 10
            dashStr = dashStr.Substring(0, dashStr.Length - 1)
        End While
        g.DrawString(dashStr, font, Brushes.Black, 0, y)
    End Sub

    Private Shared Function GetSettingInt(key As String, defaultVal As Integer) As Integer
        Try
            Dim str As String = GetSetting(key, defaultVal.ToString())
            Return Integer.Parse(str)
        Catch ex As Exception
            Debug.WriteLine("[PrintHelper.GetSettingInt] Error: " & ex.Message)
            Return defaultVal
        End Try
    End Function

#End Region

#Region "CETAK TIKET MASUK - AUTO DETECT PRINTER"

    ''' <summary>
    ''' Cetak tiket masuk dengan auto-detect jenis printer
    ''' Jika ThermalPrinterName di-set, gunakan thermal
    ''' Jika tidak, gunakan printer biasa (continuous form)
    ''' </summary>
    Public Shared Sub CetakTiketMasukAuto(data As DataCetak, Optional forcePreview As Boolean? = Nothing)
        Dim thermalPrinter As String = GetSetting("PrinterNameTkt", "")

        If Not String.IsNullOrEmpty(thermalPrinter) Then
            ' Gunakan thermal printer
            CetakTiketMasukThermal(data, forcePreview)
        Else
            ' Gunakan printer continuous form
            CetakTiketMasuk(data, forcePreview)
        End If
    End Sub

#End Region

#Region "PREVIEW CONTOH TIKET MASUK THERMAL"

    Public Shared Sub PreviewTiketMasukThermalContoh()
        Dim data As New DataCetak()
        data.NoTiket = "TBS1-260127001"
        data.NoDO = "001"
        data.NoKontrak = ""
        data.Customer = "Example Customer"
        data.Alamat = "-"
        data.Product = "TANDAN BUAH SEGAR (TBS)"
        data.TransType = "BELI"
        data.NoPolisi = "TEST-VEHICLE-01"
        data.NamaSupir = "Demo Driver"
        data.NoSIM = ""
        data.Transporter = ""
        data.BeratBruto = 12500
        data.BeratTara = 0
        data.BeratNetto = 0
        data.TotalPotongan = 0
        data.BeratBersih = 0
        data.TanggalCetak = New DateTime(2026, 1, 27, 12, 45, 0)
        data.WaktuMasuk = DateTime.Now
        data.NamaPetugas = "Admin"
        data.IsDuplikat = False

        PreviewTiketMasukThermal(data)
    End Sub

#End Region

    ' =============================================
    ' KONSTANTA LAYOUT
    ' =============================================
    Private Const ML As Integer = 20
    Private Const MR As Integer = 820
    Private Const MT As Integer = 30
    Private Const MB As Integer = 30
    Private Const COL_DIV As Integer = 460
    Private Const COL_R As Integer = 480

    ' LINE SPACING
    Private Const LINE_SPACING_115 As Integer = 14  ' 1.15 untuk header
    Private Const LINE_SPACING_150 As Integer = 18  ' 1.5 untuk body

    ' ============================================= 
    ' PUBLIC METHODS
    ' ============================================= 
    Public Shared Sub CetakStrukSelesai(data As DataCetak, Optional forcePreview As Boolean? = Nothing)
        _dataCetak = data
        _dataCetak.TanggalCetak = DateTime.Now  ' Set tanggal cetak
        ExecutePrint(AddressOf PrintStrukWilmarStyle, forcePreview)
    End Sub

    Public Shared Sub PreviewStrukSelesai(data As DataCetak)
        _dataCetak = data
        _dataCetak.TanggalCetak = DateTime.Now
        ExecutePrint(AddressOf PrintStrukWilmarStyle, forcePreview:=True)
    End Sub

    Public Shared Sub CetakTiketMasuk(data As DataCetak, Optional forcePreview As Boolean? = Nothing)
        _dataCetak = data
        _dataCetak.TanggalCetak = DateTime.Now
        ExecutePrint(AddressOf PrintTiketMasukHandler, forcePreview)
    End Sub

    Public Shared Sub PreviewStrukContoh()
        Dim data As New DataCetak()
        data.NoTiket = "TBS1-26000015"
        data.NoDO = "001"
        data.NoKontrak = "kk-123"
        data.Customer = "Demo Customer"
        data.Alamat = "-"
        data.Product = "TANDAN BUAH SEGAR"
        data.TransType = "JUAL"                    ' << Trans.Type
        data.NoPolisi = "TEST-VEHICLE-02"
        data.NamaSupir = "Demo Driver"
        data.NoSIM = "TEST-SIM-0001"            ' << No. SIM
        data.Transporter = "Example Transporter"
        data.BeratBruto = 12330
        data.BeratTara = 4560
        data.BeratNetto = 7770
        data.TotalPotongan = 205
        data.BeratBersih = 7565
        data.WaktuMasuk = New DateTime(2026, 1, 19, 21, 45, 0)
        data.WaktuKeluar = New DateTime(2026, 1, 22, 7, 5, 0)
        data.TanggalCetak = DateTime.Now          ' << Tanggal Cetak
        data.IncludeFFA = True
        data.FFA = 4D
        data.Moisture = 0.5D
        data.Dirt = 0.4D
        data.SegelAtas = "DEMO-A-001"
        data.SegelBawah = "DEMO-B-001"
        data.NamaPetugas = "Demo Operator"
        data.NamaManager = "Manager"
        data.IsDuplikat = True
        CetakStrukSelesai(data, forcePreview:=True)
    End Sub

    Public Shared Sub CetakDariDatabase(timbangID As Integer,
                                        Optional isDuplikat As Boolean = True,
                                        Optional forcePreview As Boolean? = Nothing)
        Try
            Dim query As String = "SELECT * FROM Timbangan WHERE TimbangID = @ID"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {New SqlParameter("@ID", timbangID)})

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim row As DataRow = dt.Rows(0)
            Dim data As New DataCetak()

            data.TimbangID = timbangID
            data.NoTiket = SafeStr(row, "NoTiket")
            data.NoDO = SafeStr(row, "NoDO")
            data.NoKontrak = SafeStr(row, "NoKontrak")
            If String.IsNullOrEmpty(data.NoKontrak) Then data.NoKontrak = GetSetting("NO_KONTRAK", "")
            data.NoPolisi = SafeStr(row, "NoPolisi")
            data.NamaSupir = SafeStr(row, "NamaSupir")
            data.NoSIM = SafeStr(row, "NoSIM")         ' << Ambil No. SIM dari database
            data.Transporter = SafeStr(row, "TransporterNama")
            data.Customer = SafeStr(row, "CustomerNama")
            data.Product = SafeStr(row, "ProductNama")
            data.TransType = SafeStr(row, "TransType") ' << Ambil Trans.Type dari database
            If String.IsNullOrEmpty(data.TransType) Then data.TransType = "JUAL"
            data.BeratBruto = SafeDec(row, "BeratMasuk")
            data.BeratTara = SafeDec(row, "BeratKeluar")
            data.BeratNetto = SafeDec(row, "BeratNetto")
            data.TotalPotongan = SafeDec(row, "TotalPotongan")
            data.BeratBersih = SafeDec(row, "BeratBersih")
            data.PotonganPersen = SafeDec(row, "PotonganPersen")
            data.PotonganKg = SafeDec(row, "PotonganCong")

            If Not IsDBNull(row("TanggalMasuk")) Then
                data.WaktuMasuk = CDate(row("TanggalMasuk"))
                data.WaktuBruto = data.WaktuMasuk.ToString("dd/MM HH:mm")
            End If
            If Not IsDBNull(row("TanggalKeluar")) Then
                data.WaktuKeluar = CDate(row("TanggalKeluar"))
                data.WaktuTara = data.WaktuKeluar.ToString("dd/MM HH:mm")
            End If

            data.TanggalCetak = DateTime.Now  ' << Set tanggal cetak saat ini

            data.IncludeFFA = SafeBool(row, "IncludeFFA")
            If data.IncludeFFA Then
                data.FFA = SafeDec(row, "FFA")
                data.Moisture = SafeDec(row, "Moisture")
                data.Dirt = SafeDec(row, "Dirt")
                data.SuhuMinyak = SafeDec(row, "SuhuMinyak")
            End If

            data.IncludeKeterangan = SafeBool(row, "IncludeKeterangan")
            data.Keterangan = SafeStr(row, "Keterangan")

            Try
                data.SegelAtas = SafeStr(row, "SegelAtas")
                data.SegelBawah = SafeStr(row, "SegelBawah")
            Catch ex As Exception
                Debug.WriteLine("[PrintHelper] Best-effort operation failed: " & ex.Message)
            End Try

            data.NamaPetugas = UserSession.NamaLengkap
            data.NamaManager = UserSession.NamaLengkap
            data.IsDuplikat = isDuplikat

            Dim status As String = SafeStr(row, "Status")
            If status.ToUpperInvariant() = "SELESAI" Then
                CetakStrukSelesai(data, forcePreview)
            Else
                CetakTiketMasuk(data, forcePreview)
            End If

            If isDuplikat Then UpdateJumlahCetak(timbangID)

        Catch ex As Exception
            Debug.WriteLine("[PrintHelper.CetakDariDatabase] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat data transaksi untuk pencetakan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Sub ExecutePrint(handler As PrintPageEventHandler, Optional forcePreview As Boolean? = Nothing)
        Dim pd As PrintDocument = Nothing
        Try
            pd = New PrintDocument()

            Dim printerName As String = GetSetting("PrinterName", "")
            If String.IsNullOrEmpty(printerName) Then printerName = GetSetting("NamaPrinter", "")
            If Not String.IsNullOrEmpty(printerName) Then pd.PrinterSettings.PrinterName = printerName

            pd.DefaultPageSettings.PaperSize = New PaperSize("Continuous95x55", 950, 550)
            pd.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
            pd.OriginAtMargins = False

            AddHandler pd.PrintPage, handler

            Dim showPreview As Boolean = If(forcePreview.HasValue, forcePreview.Value, Not GetSettingBool("CetakOtomatis", False))

            If showPreview Then
                Using preview As New PrintPreviewDialog()
                    preview.Document = pd
                    preview.WindowState = FormWindowState.Maximized
                    preview.ShowDialog()
                End Using
            Else
                pd.Print()
            End If

        Catch ex As Exception
            Debug.WriteLine("[PrintHelper.ExecutePrint] Error: " & ex.ToString())
            MessageBox.Show("Gagal mencetak dokumen. Periksa printer dan pengaturannya.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If pd IsNot Nothing Then
                RemoveHandler pd.PrintPage, handler
                pd.Dispose()
            End If
        End Try
    End Sub

    ' ============================================= 
    ' STRUK STYLE WILMAR - V2 MODIFIED
    ' ============================================= 
    Private Shared Sub PrintStrukWilmarStyle(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics

        Using fHeader As New Font("Courier New", 14, FontStyle.Bold),
              fTiketLabel As New Font("Consolas", 10, FontStyle.Bold),
              fTiketNo As New Font("Consolas", 10, FontStyle.Bold),
              fTitle As New Font("Consolas", 9, FontStyle.Bold),
              fSemiBold As New Font("Consolas", 9, FontStyle.Bold),
              fLabel As New Font("Consolas", 9, FontStyle.Regular),
              fValue As New Font("Consolas", 9, FontStyle.Regular),
              fBerat As New Font("Consolas", 10, FontStyle.Bold),
              fNetto As New Font("Consolas", 11, FontStyle.Bold),
              fSmall As New Font("Consolas", 8, FontStyle.Regular),
              fDate As New Font("Consolas", 8, FontStyle.Regular),
              penNormal As New Pen(Color.Black, 1),
              penTebal As New Pen(Color.Black, 1.5F)

            Dim y As Integer = MT

            ' ══════════════════════════════════════════════════════════
            ' KONSTANTA POSISI KOLOM KIRI
            ' ══════════════════════════════════════════════════════════
            Dim leftColonX As Integer = ML + 100
            Dim leftValueX As Integer = leftColonX + 10

            ' ══════════════════════════════════════════════════════════
            ' KONSTANTA POSISI KOLOM KANAN
            ' ══════════════════════════════════════════════════════════
            Dim rightColonX As Integer = COL_R + 100
            Dim rightValueX As Integer = rightColonX + 10

            ' ══════════════════════════════════════════════════════════
            ' HEADER (LINE SPACING 1.15)
            ' ══════════════════════════════════════════════════════════
            Dim tiketStartX As Integer = MR - 200

            ' >>> PERINTAH BARU: CEK DAN CETAK GAMBAR LOGO <<<
            Dim textX As Integer = ML
            Dim logoPath As String = GetSetting("LogoPath", "")

            If Not String.IsNullOrEmpty(logoPath) AndAlso IO.File.Exists(logoPath) Then
                Try
                    Using img As Image = Image.FromFile(logoPath)
                        Dim logoSize As Integer = 45 ' Ukuran logo diset 45x45 piksel agar pas dengan 3 baris teks
                        g.DrawImage(img, New Rectangle(ML, y, logoSize, logoSize))

                        ' Geser teks info perusahaan ke kanan sedikit agar berdampingan rapi dengan logo
                        textX = ML + logoSize + 10
                    End Using
                Catch ex As Exception
                    ' Logo loading is optional; keep printing available if the image is invalid.
                    Debug.WriteLine("[PrintHelper.ExecutePrint] Logo load error: " & ex.Message)
                End Try
            End If
            ' >>> AKHIR PERINTAH GAMBAR LOGO <<<

            ' *** BARIS 0: TANGGAL CETAK (di atas No. Tiket) ***
            Dim tanggalCetakText As String = "Date : " & _dataCetak.TanggalCetak.ToString("dd/MM/yyyy HH:mm")
            g.DrawString(tanggalCetakText, fDate, Brushes.Black, tiketStartX, y)
            y += LINE_SPACING_115

            ' Baris 1: Nama Perusahaan + Label No. Tiket
            ' Menggunakan variabel textX agar teks geser jika ada logo
            g.DrawString(GetSetting("NamaPerusahaan", "Example Palm Oil Mill"), fHeader, Brushes.Black, textX, y)
            g.DrawString("No. Tiket :", fTiketLabel, Brushes.Black, tiketStartX, y + 2)
            y += LINE_SPACING_115 + 4

            ' Baris 2: Alamat + No Tiket + DUPLIKAT
            g.DrawString(GetSetting("AlamatPerusahaan", "Example Palm Oil Mill Address"), fSmall, Brushes.Black, textX, y)
            g.DrawString(_dataCetak.NoTiket, fTiketNo, Brushes.Black, tiketStartX, y)

            If _dataCetak.IsDuplikat Then
                Dim dupText As String = "*** DUPLIKAT ***"
                Dim dupSize As SizeF = g.MeasureString(dupText, fSmall)
                Dim centerX As Single = ML + 180 + ((tiketStartX - (ML + 180) - dupSize.Width) / 2)
                g.DrawString(dupText, fSmall, Brushes.Black, centerX, y)
            End If
            y += LINE_SPACING_115

            ' Baris 3: Kota + Kode Pos
            g.DrawString(GetSetting("KotaPerusahaan", "Example Region") & ", " & GetSetting("KodePos", "00000"), fSmall, Brushes.Black, textX, y)
            y += LINE_SPACING_115

            ' ---> TAMBAHAN: TELEPON PERUSAHAAN <---
            Dim telepon As String = GetSetting("TeleponPerusahaan", "")
            If Not String.IsNullOrEmpty(telepon) Then
                g.DrawString("Telp: " & telepon, fSmall, Brushes.Black, textX, y)
                y += LINE_SPACING_115
            End If
            ' ---> AKHIR TAMBAHAN <---

            g.DrawLine(penTebal, ML, y, MR, y)
            y += 5

            Dim bodyStartY As Integer = y

            ' ══════════════════════════════════════════════════════════
            ' KOLOM KIRI: INFO DOKUMEN (LINE SPACING 1.5)
            ' G. Note DIHILANGKAN, Trans.Type DITAMBAHKAN
            ' ══════════════════════════════════════════════════════════
            Dim leftY As Integer = y

            g.DrawString("DO No.", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            g.DrawString(Nz(_dataCetak.NoDO), fLabel, Brushes.Black, leftValueX, leftY)
            leftY += LINE_SPACING_150

            g.DrawString("Contract", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            g.DrawString(Nz(_dataCetak.NoKontrak), fLabel, Brushes.Black, leftValueX, leftY)
            leftY += LINE_SPACING_150

            g.DrawString("Relation", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            g.DrawString(Cut(_dataCetak.Customer, 30), fLabel, Brushes.Black, leftValueX, leftY)
            leftY += LINE_SPACING_150

            g.DrawString("Address", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            g.DrawString(Nz(_dataCetak.Alamat), fLabel, Brushes.Black, leftValueX, leftY)
            leftY += LINE_SPACING_150

            ' *** G. Note DIHILANGKAN ***

            g.DrawString("Commodity", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            g.DrawString(Cut(_dataCetak.Product, 20), fLabel, Brushes.Black, leftValueX, leftY)
            leftY += LINE_SPACING_150

            ' *** Trans.Type DITAMBAHKAN di bawah Commodity ***
            g.DrawString("Trans.Type", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            g.DrawString(Nz(_dataCetak.TransType), fLabel, Brushes.Black, leftValueX, leftY)
            leftY += LINE_SPACING_150 + LINE_SPACING_150  ' +1 ENTER

            ' Description TANPA titik dua
            g.DrawString("Description", fLabel, Brushes.Black, ML, leftY)
            leftY += LINE_SPACING_150

            g.DrawString("IN", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            g.DrawString("(" & _dataCetak.WaktuMasuk.ToString("HH:mm") & ")  " & _dataCetak.WaktuMasuk.ToString("dd/MM/yyyy"), fLabel, Brushes.Black, leftValueX, leftY)
            leftY += LINE_SPACING_150

            g.DrawString("OUT", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            g.DrawString("(" & _dataCetak.WaktuKeluar.ToString("HH:mm") & ")  " & _dataCetak.WaktuKeluar.ToString("dd/MM/yyyy"), fLabel, Brushes.Black, leftValueX, leftY)
            leftY += LINE_SPACING_150 + 2

            ' ══════════════════════════════════════════════════════════
            ' DATA BERAT (LINE SPACING 1.5)
            ' ══════════════════════════════════════════════════════════
            Dim beratValueX As Integer = leftValueX
            Dim valueRightX As Integer = beratValueX + 95

            Dim garisMulaiX As Integer = leftValueX + 5
            Dim garisAkhirX As Integer = valueRightX

            ' GROSS
            g.DrawString("GROSS", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            DrawRightAligned(g, fBerat, _dataCetak.BeratBruto.ToString("#,##0") & " KG", valueRightX, leftY)
            leftY += LINE_SPACING_150

            ' TARE
            g.DrawString("TARE", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            DrawRightAligned(g, fBerat, _dataCetak.BeratTara.ToString("#,##0") & " KG", valueRightX, leftY)
            leftY += (LINE_SPACING_150 \ 2) + 10

            ' GARIS 1
            g.DrawLine(penNormal, garisMulaiX, leftY, garisAkhirX, leftY)
            leftY += (LINE_SPACING_150 \ 2) + 2

            ' RECEIVED
            g.DrawString("RECEIVED", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            DrawRightAligned(g, fBerat, _dataCetak.BeratNetto.ToString("#,##0") & " KG", valueRightX, leftY)
            leftY += LINE_SPACING_150

            ' DEDUCTION
            g.DrawString("DEDUCTION", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            DrawRightAligned(g, fBerat, _dataCetak.TotalPotongan.ToString("#,##0") & " KG", valueRightX, leftY)
            leftY += (LINE_SPACING_150 \ 2) + 10

            ' GARIS 2
            g.DrawLine(penNormal, garisMulaiX, leftY, garisAkhirX, leftY)
            leftY += (LINE_SPACING_150 \ 2) + 2

            ' NETTO
            g.DrawString("NETTO", fLabel, Brushes.Black, ML, leftY)
            g.DrawString(":", fLabel, Brushes.Black, leftColonX, leftY)
            DrawRightAligned(g, fNetto, _dataCetak.BeratBersih.ToString("#,##0") & " KG", valueRightX, leftY)

            ' ══════════════════════════════════════════════════════════
            ' KOLOM KANAN: KENDARAAN (LINE SPACING 1.5)
            ' No. SIM DITAMBAHKAN di bawah Transporter
            ' ══════════════════════════════════════════════════════════
            Dim rightY As Integer = bodyStartY

            g.DrawString("Truck No.", fLabel, Brushes.Black, COL_R, rightY)
            g.DrawString(":", fLabel, Brushes.Black, rightColonX, rightY)
            g.DrawString(_dataCetak.NoPolisi, fLabel, Brushes.Black, rightValueX, rightY)
            rightY += LINE_SPACING_150

            g.DrawString("Driver Name", fLabel, Brushes.Black, COL_R, rightY)
            g.DrawString(":", fLabel, Brushes.Black, rightColonX, rightY)
            g.DrawString(Cut(_dataCetak.NamaSupir, 25), fLabel, Brushes.Black, rightValueX, rightY)
            rightY += LINE_SPACING_150

            g.DrawString("Transporter", fLabel, Brushes.Black, COL_R, rightY)
            g.DrawString(":", fLabel, Brushes.Black, rightColonX, rightY)
            g.DrawString(Cut(_dataCetak.Transporter, 35), fLabel, Brushes.Black, rightValueX, rightY)
            rightY += LINE_SPACING_150

            ' *** No. SIM DITAMBAHKAN di bawah Transporter ***
            g.DrawString("No. SIM", fLabel, Brushes.Black, COL_R, rightY)
            g.DrawString(":", fLabel, Brushes.Black, rightColonX, rightY)
            g.DrawString(Nz(_dataCetak.NoSIM), fLabel, Brushes.Black, rightValueX, rightY)
            rightY += LINE_SPACING_150 + 6

            ' ══════════════════════════════════════════════════════════
            ' TABEL ANALISA (LINE SPACING 1.5)
            ' ══════════════════════════════════════════════════════════
            Dim tableWidth As Integer = MR - COL_R
            Dim tableColonX As Integer = COL_R + 100

            If _dataCetak.IncludeFFA Then
                Dim ffaTableX As Integer = COL_R
                Dim ffaTableY As Integer = rightY
                Dim ffaRowH As Integer = LINE_SPACING_150
                Dim ffaHeaderH As Integer = 18
                Dim ffaTotalH As Integer = ffaHeaderH + (ffaRowH * 5) + 6

                g.DrawRectangle(penNormal, ffaTableX, ffaTableY, tableWidth, ffaTotalH)
                g.DrawLine(penNormal, ffaTableX, ffaTableY + ffaHeaderH, ffaTableX + tableWidth, ffaTableY + ffaHeaderH)

                Dim analisaTitle As String = "Analisa"
                Dim analisaSize As SizeF = g.MeasureString(analisaTitle, fSemiBold)
                g.DrawString(analisaTitle, fSemiBold, Brushes.Black, ffaTableX + (tableWidth - analisaSize.Width) / 2, ffaTableY + 2)

                Dim ffaContentY As Integer = ffaTableY + ffaHeaderH + 3

                g.DrawString("FFA", fLabel, Brushes.Black, ffaTableX + 5, ffaContentY)
                g.DrawString(":", fLabel, Brushes.Black, tableColonX, ffaContentY)
                g.DrawString(_dataCetak.FFA.ToString("N2") & " %", fLabel, Brushes.Black, tableColonX + 8, ffaContentY)
                ffaContentY += ffaRowH

                g.DrawString("Moisture", fLabel, Brushes.Black, ffaTableX + 5, ffaContentY)
                g.DrawString(":", fLabel, Brushes.Black, tableColonX, ffaContentY)
                g.DrawString(_dataCetak.Moisture.ToString("N2") & " %", fLabel, Brushes.Black, tableColonX + 8, ffaContentY)
                ffaContentY += ffaRowH

                ' DIRT
                g.DrawString("Dirt", fLabel, Brushes.Black, ffaTableX + 5, ffaContentY)
                g.DrawString(":", fLabel, Brushes.Black, tableColonX, ffaContentY)
                g.DrawString(_dataCetak.Dirt.ToString("N2") & " %", fLabel, Brushes.Black, tableColonX + 8, ffaContentY)
                ffaContentY += ffaRowH + 4      ' tambah 4px jarak

                ' SUHU MINYAK
                g.DrawString("Suhu Minyak", fLabel, Brushes.Black, ffaTableX + 5, ffaContentY)
                g.DrawString(":", fLabel, Brushes.Black, tableColonX, ffaContentY)
                g.DrawString(_dataCetak.SuhuMinyak.ToString("N1") & " °C", fLabel, Brushes.Black, tableColonX + 8, ffaContentY)
                ffaContentY += ffaRowH

                rightY = ffaTableY + ffaTotalH + 10

                rightY = ffaTableY + ffaTotalH + 10
            End If

            ' ══════════════════════════════════════════════════════════
            ' TABEL NOMOR SEGEL (LINE SPACING 1.5)
            ' ══════════════════════════════════════════════════════════
            Dim segelTableX As Integer = COL_R
            Dim segelTableY As Integer = rightY
            Dim segelRowH As Integer = LINE_SPACING_150
            Dim segelHeaderH As Integer = 18
            Dim segelTotalH As Integer = segelHeaderH + (segelRowH * 2) + 6

            g.DrawRectangle(penNormal, segelTableX, segelTableY, tableWidth, segelTotalH)
            g.DrawLine(penNormal, segelTableX, segelTableY + segelHeaderH, segelTableX + tableWidth, segelTableY + segelHeaderH)

            Dim segelTitle As String = "Nomor Segel"
            Dim segelSize As SizeF = g.MeasureString(segelTitle, fSemiBold)
            g.DrawString(segelTitle, fSemiBold, Brushes.Black, segelTableX + (tableWidth - segelSize.Width) / 2, segelTableY + 2)

            Dim segelContentY As Integer = segelTableY + segelHeaderH + 3

            g.DrawString("Segel Atas", fLabel, Brushes.Black, segelTableX + 5, segelContentY)
            g.DrawString(":", fLabel, Brushes.Black, tableColonX, segelContentY)
            g.DrawString(Nz(_dataCetak.SegelAtas), fLabel, Brushes.Black, tableColonX + 12, segelContentY)
            segelContentY += segelRowH

            g.DrawString("Segel Bawah", fLabel, Brushes.Black, segelTableX + 5, segelContentY)
            g.DrawString(":", fLabel, Brushes.Black, tableColonX, segelContentY)
            g.DrawString(Nz(_dataCetak.SegelBawah), fLabel, Brushes.Black, tableColonX + 12, segelContentY)

            rightY = segelTableY + segelTotalH

            Dim bodyEndY As Integer = Math.Max(leftY + 20, rightY) + 8

            g.DrawLine(penNormal, COL_DIV, bodyStartY, COL_DIV, bodyEndY)
            g.DrawLine(penTebal, ML, bodyEndY, MR, bodyEndY)

            ' ══════════════════════════════════════════════════════════
            ' TANDA TANGAN - 2 SPASI SETELAH LABEL
            ' ══════════════════════════════════════════════════════════
            Dim paperHeight As Integer = 550
            Dim ttdY As Integer = bodyEndY + 4
            Dim ttdH As Integer = paperHeight - ttdY - MB - 5
            If ttdH < 55 Then ttdH = 55
            If ttdH > 75 Then ttdH = 75

            Dim colW As Integer = (MR - ML) \ 4
            Dim labels() As String = {"Approved by", "Lab / QC", "Petugas", "Supir"}
            Dim subLabels() As String = {"Manager", "", "", ""}

            ' Garis vertikal pemisah
            For i As Integer = 1 To 3
                Dim boxX As Integer = ML + (i * colW)
                g.DrawLine(penNormal, boxX, ttdY, boxX, ttdY + ttdH)
            Next

            ' Garis horizontal bawah
            g.DrawLine(penNormal, ML, ttdY + ttdH, MR, ttdY + ttdH)

            ' Label di atas
            For i As Integer = 0 To 3
                Dim tx As Integer = ML + (i * colW)
                Dim lblSize As SizeF = g.MeasureString(labels(i), fSmall)
                g.DrawString(labels(i), fSmall, Brushes.Black, tx + (colW - lblSize.Width) / 2, ttdY + 3)

                If Not String.IsNullOrEmpty(subLabels(i)) Then
                    Dim subSize As SizeF = g.MeasureString(subLabels(i), fSmall)
                    g.DrawString(subLabels(i), fSmall, Brushes.Black, tx + (colW - subSize.Width) / 2, ttdY + 14)
                End If
            Next

            ' Nama di bawah
            Dim names() As String = {
                "(" & Cut(_dataCetak.NamaManager, 11) & ")",
                "(Analyst/KTU)",
                "(" & Cut(_dataCetak.NamaPetugas, 11) & ")",
                "(" & Cut(_dataCetak.NamaSupir, 11) & ")"
            }

            For i As Integer = 0 To 3
                Dim tx As Integer = ML + (i * colW)
                Dim nameSize As SizeF = g.MeasureString(names(i), fSmall)
                g.DrawString(names(i), fSmall, Brushes.Black, tx + (colW - nameSize.Width) / 2, ttdY + ttdH - 12)
            Next

        End Using

        e.HasMorePages = False
    End Sub

    ' ============================================= 
    ' TIKET MASUK - V2 MODIFIED
    ' ============================================= 
    Private Shared Sub PrintTiketMasukHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics

        Using fHeader As New Font("Courier New", 14, FontStyle.Bold),
              fTiketLabel As New Font("Consolas", 10, FontStyle.Bold),
              fTiketNo As New Font("Consolas", 10, FontStyle.Bold),
              fTitle As New Font("Consolas", 9, FontStyle.Bold),
              fLabel As New Font("Consolas", 9, FontStyle.Regular),
              fValue As New Font("Consolas", 9, FontStyle.Bold),
              fBerat As New Font("Consolas", 14, FontStyle.Bold),
              fSmall As New Font("Consolas", 8, FontStyle.Regular),
              fDate As New Font("Consolas", 8, FontStyle.Regular),
              penNormal As New Pen(Color.Black, 1),
              penTebal As New Pen(Color.Black, 1.5F)

            Dim y As Integer = MT
            Dim lblW As Integer = 80
            Dim tiketStartX As Integer = MR - 170

            ' *** TANGGAL CETAK di atas No. Tiket ***
            Dim tanggalCetakText As String = "Date : " & _dataCetak.TanggalCetak.ToString("dd/MM/yyyy HH:mm")
            g.DrawString(tanggalCetakText, fDate, Brushes.Black, tiketStartX, y)
            y += LINE_SPACING_115

            ' Header dengan line spacing 1.15
            g.DrawString(GetSetting("NamaPerusahaan", "Example Palm Oil Mill"), fHeader, Brushes.Black, ML, y)
            g.DrawString("No. Tiket :", fTiketLabel, Brushes.Black, tiketStartX, y + 2)
            y += LINE_SPACING_115 + 4

            g.DrawString(GetSetting("AlamatPerusahaan", ""), fSmall, Brushes.Black, ML, y)
            g.DrawString(_dataCetak.NoTiket, fTiketNo, Brushes.Black, tiketStartX, y)
            y += LINE_SPACING_115

            g.DrawLine(penTebal, ML, y, MR, y)
            y += 6

            g.DrawString("TIKET MASUK", fTitle, Brushes.Black, ML, y)
            y += LINE_SPACING_150

            g.DrawLine(penNormal, ML, y, MR, y)
            y += 5

            DrawFieldRegular(g, fLabel, "DO No.", _dataCetak.NoDO, ML, y, lblW)
            y += LINE_SPACING_150

            DrawField(g, fLabel, fTitle, "Truck No.", _dataCetak.NoPolisi, ML, y, lblW)
            y += LINE_SPACING_150

            DrawFieldRegular(g, fLabel, "Driver", Cut(_dataCetak.NamaSupir, 30), ML, y, lblW)
            y += LINE_SPACING_150

            ' *** No. SIM ditambahkan ***
            DrawFieldRegular(g, fLabel, "No. SIM", Nz(_dataCetak.NoSIM), ML, y, lblW)
            y += LINE_SPACING_150

            DrawFieldRegular(g, fLabel, "Commodity", Cut(_dataCetak.Product, 30), ML, y, lblW)
            y += LINE_SPACING_150

            ' *** Trans.Type ditambahkan ***
            DrawFieldRegular(g, fLabel, "Trans.Type", Nz(_dataCetak.TransType), ML, y, lblW)
            y += LINE_SPACING_150

            DrawFieldRegular(g, fLabel, "Customer", Cut(_dataCetak.Customer, 30), ML, y, lblW)
            y += LINE_SPACING_150 + 2

            g.DrawLine(penNormal, ML, y, MR, y)
            y += 5

            DrawFieldRegular(g, fLabel, "IN", "(" & DateTime.Now.ToString("HH:mm") & ")  " & DateTime.Now.ToString("dd/MM/yyyy"), ML, y, lblW)
            y += LINE_SPACING_150 + 2

            g.DrawString("GROSS", fTitle, Brushes.Black, ML, y)
            g.DrawString(":", fTitle, Brushes.Black, ML + 70, y)
            g.DrawString(_dataCetak.BeratBruto.ToString("#,##0") & " KG", fBerat, Brushes.Black, ML + 85, y)
            y += 28

            g.DrawLine(penTebal, ML, y, MR, y)
            y += 10

            g.DrawString("Petugas: " & _dataCetak.NamaPetugas, fSmall, Brushes.Gray, ML, y)
            y += LINE_SPACING_150

            Dim footerText As String = "*** Simpan tiket ini untuk timbang keluar ***"
            Dim footerSize As SizeF = g.MeasureString(footerText, fTitle)
            g.DrawString(footerText, fTitle, Brushes.Black, ML + ((MR - ML) - footerSize.Width) / 2, y)

        End Using

        e.HasMorePages = False
    End Sub

    ' ============================================= 
    ' HELPER METHODS
    ' ============================================= 
    Private Shared Sub DrawRightAligned(g As Graphics, f As Font, text As String, rightX As Integer, y As Integer)
        Dim textSize As SizeF = g.MeasureString(text, f)
        g.DrawString(text, f, Brushes.Black, rightX - textSize.Width, y)
    End Sub

    Private Shared Sub DrawField(g As Graphics, fLabel As Font, fValue As Font,
                                 label As String, value As String,
                                 x As Integer, y As Integer, lblWidth As Integer)
        g.DrawString(label, fLabel, Brushes.Black, x, y)
        g.DrawString(":", fLabel, Brushes.Black, x + lblWidth - 5, y)
        g.DrawString(value, fValue, Brushes.Black, x + lblWidth + 3, y)
    End Sub

    Private Shared Sub DrawFieldRegular(g As Graphics, fRegular As Font,
                                        label As String, value As String,
                                        x As Integer, y As Integer, lblWidth As Integer)
        g.DrawString(label, fRegular, Brushes.Black, x, y)
        g.DrawString(":", fRegular, Brushes.Black, x + lblWidth - 5, y)
        g.DrawString(value, fRegular, Brushes.Black, x + lblWidth + 3, y)
    End Sub

    Private Shared Function Nz(text As String) As String
        Return If(String.IsNullOrEmpty(text), "-", text)
    End Function

    Private Shared Function Cut(text As String, maxLen As Integer) As String
        If String.IsNullOrEmpty(text) Then Return "-"
        If text.Length <= maxLen Then Return text
        Return text.Substring(0, maxLen - 2) & ".."
    End Function

    Private Shared Function SafeStr(row As DataRow, col As String) As String
        Try
            If row.Table.Columns.Contains(col) AndAlso Not IsDBNull(row(col)) Then Return row(col).ToString()
        Catch ex As Exception
            Debug.WriteLine("[PrintHelper] Best-effort operation failed: " & ex.Message)
        End Try
        Return ""
    End Function

    Private Shared Function SafeDec(row As DataRow, col As String) As Decimal
        Try
            If row.Table.Columns.Contains(col) AndAlso Not IsDBNull(row(col)) Then Return CDec(row(col))
        Catch ex As Exception
            Debug.WriteLine("[PrintHelper] Best-effort operation failed: " & ex.Message)
        End Try
        Return 0D
    End Function

    Private Shared Function SafeBool(row As DataRow, col As String) As Boolean
        Try
            If row.Table.Columns.Contains(col) AndAlso Not IsDBNull(row(col)) Then Return CBool(row(col))
        Catch ex As Exception
            Debug.WriteLine("[PrintHelper] Best-effort operation failed: " & ex.Message)
        End Try
        Return False
    End Function

    Private Shared Function GetSetting(key As String, defaultVal As String) As String
        Try
            Dim result As Object = DatabaseHelper.ExecuteScalar(
                "SELECT SettingValue FROM Settings WHERE SettingKey = @Key",
                {New SqlParameter("@Key", key)})
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then Return result.ToString()
        Catch ex As Exception
            Debug.WriteLine("[PrintHelper] Best-effort operation failed: " & ex.Message)
        End Try
        Return defaultVal
    End Function

    Private Shared Function GetSettingBool(key As String, defaultVal As Boolean) As Boolean
        Dim str As String = GetSetting(key, If(defaultVal, "1", "0"))
        Return str = "1" OrElse String.Equals(str, "true", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Sub UpdateJumlahCetak(timbangID As Integer)
        Try
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Timbangan SET JumlahCetak = ISNULL(JumlahCetak, 0) + 1 WHERE TimbangID = @ID",
                {New SqlParameter("@ID", timbangID)})
        Catch ex As Exception
            Debug.WriteLine("[PrintHelper] Best-effort operation failed: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub TestPreview()
        PreviewStrukContoh()
    End Sub

    Public Shared Sub Cleanup()
        _dataCetak = Nothing
    End Sub

    Private Shared Sub DrawTextNoWrap(g As Graphics, f As Font, text As String, x As Integer, y As Integer, maxWidth As Integer, Optional bold As Boolean = False)
        Using sf As New StringFormat(StringFormatFlags.NoWrap)
            sf.Trimming = StringTrimming.EllipsisCharacter
            g.DrawString(text, f, Brushes.Black, New RectangleF(x, y, maxWidth, f.GetHeight(g) + 6), sf)
        End Using
    End Sub

End Class