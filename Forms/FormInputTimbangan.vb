' =============================================
' FormInputTimbangan.vb
' Form Input Timbangan - VERSI DIPERBAIKI
' Support GSC SGW-3015S - Data Continue
' TERINTEGRASI DENGAN PrintHelper
' =============================================
Imports System.Data.SqlClient

Public Class FormInputTimbangan
    ' Variabel untuk menyimpan state
    Private _currentTimbangID As Integer = 0
    Private _isEditMode As Boolean = False
    Private _beratDariIndikator As Decimal = 0

    ' Variabel untuk menyimpan berat (dari database)
    Private _beratTersimpan As Decimal = 0
    Private _waktuTersimpan As DateTime = DateTime.MinValue

    ' === DELEGATE UNTUK CROSS-THREAD ===
    Private Delegate Sub UpdateWeightDelegate(weight As Decimal, rawData As String)
    Private Delegate Sub UpdateStatusDelegate(isConnected As Boolean, message As String)
    Private Delegate Sub UpdateErrorDelegate(errorMessage As String)

    ' =============================================
    ' FORM LOAD
    ' =============================================
    Private Sub FormInputTimbangan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "Input Timbangan - " & UserSession.NamaLengkap

            Me.KeyPreview = True

            AdjustForScreenSize()

            ' DEBUG: Cek role
            Debug.WriteLine($"[FormLoad] UserID: {UserSession.UserID}")
            Debug.WriteLine($"[FormLoad] Role: {UserSession.Role}")
            Debug.WriteLine($"[FormLoad] CanEditFFA: {UserSession.CanEditFFA()}")

            LoadComboBoxData()
            LoadAntrian()

            ' === PENTING: Subscribe ke event SerialPortHelper ===
            AddHandler SerialPortHelper.WeightReceived, AddressOf OnWeightReceived
            AddHandler SerialPortHelper.ConnectionStatusChanged, AddressOf OnConnectionStatusChanged
            AddHandler SerialPortHelper.ErrorOccurred, AddressOf OnSerialError

            lblBeratRealtime.Text = "0"
            lblBeratRealtime.ForeColor = Color.Lime

            ConnectToScale()
            AddHandler TimerRealtime.Tick, AddressOf TimerRealtime_Tick
            TimerRealtime.Start()
            SetNewMode()

            ' Audit Log
            DatabaseHelper.InsertAuditLog(UserSession.UserID, "VIEW_FORM", Nothing, "Input Timbangan",
                Nothing, Nothing, Nothing, "User membuka Input Timbangan")
            chkIncludeFFA.Enabled = True
            chkIncludeKeterangan.Enabled = True

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.Load] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat form timbangan. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub TimerRealtime_Tick(sender As Object, e As EventArgs)
        Try
            ' Update label unit setiap detik
            ' (opsional - karena sudah ada event WeightReceived)
            If Not SerialPortHelper.IsConnected Then
                UpdateConnectionStatus(False, "Tidak terhubung")
            End If
        Catch ex As Exception
            Debug.WriteLine($"[TimerRealtime] {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' PENYESUAIAN UKURAN FORM UNTUK BERBAGAI LAYAR
    ' ← TAMBAHKAN SUB BARU INI DI SINI
    ' =============================================
    Private Sub AdjustForScreenSize()
        Try
            ' Ambil ukuran area kerja layar (tidak termasuk taskbar)
            Dim screenArea As Rectangle = Screen.FromControl(Me).WorkingArea

            Debug.WriteLine($"[AdjustForScreen] Screen: {screenArea.Width}x{screenArea.Height}")
            Debug.WriteLine($"[AdjustForScreen] Form  : {Me.Width}x{Me.Height}")

            ' Jika lebar atau tinggi form melebihi layar
            If Me.Width > screenArea.Width OrElse Me.Height > screenArea.Height Then

                ' Aktifkan scroll agar konten tetap bisa dijangkau
                Me.AutoScroll = True

                ' Paksa form mulai dari pojok kiri atas
                Me.Location = New Point(screenArea.Left, screenArea.Top)

                ' Resize form agar tidak melebihi ukuran layar
                ' (konten yang tidak muat bisa discroll)
                Dim newWidth As Integer = Math.Min(Me.Width, screenArea.Width)
                Dim newHeight As Integer = Math.Min(Me.Height, screenArea.Height)
                Me.Size = New Size(newWidth, newHeight)

                Debug.WriteLine($"[AdjustForScreen] Resized to: {newWidth}x{newHeight} - AutoScroll ON")
            Else
                ' Form muat di layar - tampilkan di tengah
                Me.Location = New Point(
                    screenArea.Left + (screenArea.Width - Me.Width) \ 2,
                    screenArea.Top + (screenArea.Height - Me.Height) \ 2
                )
                Debug.WriteLine($"[AdjustForScreen] Centered - no resize needed")
            End If

        Catch ex As Exception
            Debug.WriteLine($"[AdjustForScreenSize] Error: {ex.Message}")
            ' Jika error, biarkan default - tidak akan crash
        End Try
    End Sub

    ' =============================================
    ' KONEKSI KE TIMBANGAN
    ' =============================================
    Private Sub ConnectToScale()
        Try
            Dim comPort As String = GetSetting("COM_PORT", "COM3")
            Dim baudRate As Integer = CInt(GetSetting("BAUD_RATE", "9600"))

            ' Update label port
            If lblComPort IsNot Nothing Then
                lblComPort.Text = "Port : " & comPort
            End If

            ' Baca merek dari DB untuk ditampilkan di header
            Dim brandName As String = GetSetting("SCALE_BRAND", "GSC").ToUpperInvariant()

            ' Update status awal
            UpdateConnectionStatus(False, "Menghubungkan ke " & comPort & " [" & brandName & "]...")
            Application.DoEvents()

            ' Connect — SerialPortHelper akan baca SCALE_BRAND sendiri dari DB di dalamnya
            If SerialPortHelper.Connect(comPort, baudRate) Then
                UpdateConnectionStatus(True, "Terhubung")

                ' Tampilkan brand aktif di label COM Port
                lblComPort.Text = "Port : " & comPort & "  |  Brand : " & brandName
            Else
                UpdateConnectionStatus(False, "Gagal koneksi ke " & comPort)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.ConnectToScale] Error: " & ex.ToString())
            UpdateConnectionStatus(False, "Gagal menghubungkan ke indikator timbangan")
        End Try
    End Sub

    Private Sub UpdateConnectionStatus(isConnected As Boolean, message As String)
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New UpdateStatusDelegate(AddressOf UpdateConnectionStatus), isConnected, message)
                Return
            End If

            ' Update label status (sesuaikan dengan nama label di form Anda)
            ' Dari gambar: sepertinya ada label "Status : Belum Terhubung"
            If lblStatusTimbangan IsNot Nothing Then
                If isConnected Then
                    lblStatusTimbangan.Text = "Status : Terhubung"
                    lblStatusTimbangan.ForeColor = Color.Green
                Else
                    lblStatusTimbangan.Text = "Status : " & message
                    lblStatusTimbangan.ForeColor = Color.Red
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine($"[UpdateConnectionStatus] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' EVENT HANDLER SERIAL PORT - DATA WEIGHT RECEIVED
    ' INI YANG PENTING UNTUK DISPLAY REALTIME
    ' =============================================
    Private Sub OnWeightReceived(weight As Decimal, rawData As String)
        Try
            ' Debug log
            Debug.WriteLine($"[OnWeightReceived] Weight: {weight}, Raw: [{rawData}]")

            ' Cross-thread invocation
            If Me.InvokeRequired Then
                Me.Invoke(New UpdateWeightDelegate(AddressOf OnWeightReceived), weight, rawData)
                Return
            End If

            ' Simpan nilai berat
            _beratDariIndikator = weight

            ' === UPDATE LABEL DISPLAY REALTIME ===
            ' lblBeratRealtime adalah label besar di panel hitam
            lblBeratRealtime.Text = weight.ToString("N0")

            ' Ubah warna berdasarkan nilai
            If weight < 0 Then
                lblBeratRealtime.ForeColor = Color.Red
            ElseIf weight = 0 Then
                lblBeratRealtime.ForeColor = Color.FromArgb(0, 255, 0) ' Hijau terang
            Else
                lblBeratRealtime.ForeColor = Color.FromArgb(0, 255, 0) ' Hijau terang
            End If

        Catch ex As Exception
            Debug.WriteLine($"[OnWeightReceived] Error: {ex.Message}")
        End Try
    End Sub

    Private Sub OnConnectionStatusChanged(isConnected As Boolean, message As String)
        Try
            Debug.WriteLine($"[OnConnectionStatusChanged] Connected: {isConnected}, Message: {message}")
            UpdateConnectionStatus(isConnected, message)
        Catch ex As Exception
            Debug.WriteLine($"[OnConnectionStatusChanged] Error: {ex.Message}")
        End Try
    End Sub

    Private Sub OnSerialError(errorMessage As String)
        Try
            Debug.WriteLine($"[OnSerialError] {errorMessage}")

            If Me.InvokeRequired Then
                Me.Invoke(New UpdateErrorDelegate(AddressOf OnSerialError), errorMessage)
                Return
            End If

            If lblStatusTimbangan IsNot Nothing Then
                lblStatusTimbangan.Text = "Status : Error - " & errorMessage
                lblStatusTimbangan.ForeColor = Color.Red
            End If

        Catch ex As Exception
            Debug.WriteLine($"[OnSerialError Handler] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' LOAD DATA COMBOBOX
    ' =============================================
    Private Sub LoadComboBoxData()
        Try
            ' === TRANSPORTER ===
            Dim dtTransporter As DataTable = DatabaseHelper.ExecuteQuery(
            "SELECT TransporterID, NamaTransporter FROM Transporters WHERE IsActive = 1 ORDER BY NamaTransporter")
            cmbTransporter.DataSource = dtTransporter
            cmbTransporter.DisplayMember = "NamaTransporter"
            cmbTransporter.ValueMember = "TransporterID"
            cmbTransporter.SelectedIndex = -1

            ' === CUSTOMER ===
            Dim dtCustomer As DataTable = DatabaseHelper.ExecuteQuery(
            "SELECT CustomerID, NamaCustomer FROM Customers WHERE IsActive = 1 ORDER BY NamaCustomer")
            cmbCustomer.DataSource = dtCustomer
            cmbCustomer.DisplayMember = "NamaCustomer"
            cmbCustomer.ValueMember = "CustomerID"
            cmbCustomer.SelectedIndex = -1

            ' === PRODUCT ===
            Dim dtProduct As DataTable = DatabaseHelper.ExecuteQuery(
            "SELECT ProductID, NamaProduk FROM Products WHERE IsActive = 1 ORDER BY NamaProduk")
            cmbProduct.DataSource = dtProduct
            cmbProduct.DisplayMember = "NamaProduk"
            cmbProduct.ValueMember = "ProductID"
            cmbProduct.SelectedIndex = -1

            ' === TRANS TYPE (BARU) ===
            If cmbTransType IsNot Nothing Then
                cmbTransType.Items.Clear()
                cmbTransType.Items.Add("JUAL")
                cmbTransType.Items.Add("BELI")
                cmbTransType.SelectedIndex = 0 ' Default: JUAL
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.LoadComboBoxData] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat data master. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' EVENT: PRODUCT BERUBAH - AUTO GENERATE NO TIKET
    ' =============================================
    Private Sub cmbProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProduct.SelectedIndexChanged
        Try
            ' Hanya generate jika mode INPUT BARU (bukan edit)
            If _currentTimbangID > 0 Then Return
            If cmbProduct.SelectedIndex < 0 Then Return

            ' Ambil ProductID yang dipilih
            Dim productID As Integer = 0
            If cmbProduct.SelectedValue IsNot Nothing AndAlso IsNumeric(cmbProduct.SelectedValue) Then
                productID = CInt(cmbProduct.SelectedValue)
            End If

            If productID <= 0 Then Return

            ' Ambil Kode Produk dari database
            Dim kodeProduk As String = DatabaseHelper.GetKodeProdukByID(productID)

            If String.IsNullOrWhiteSpace(kodeProduk) Then
                ' Fallback: coba ambil dari nama produk
                kodeProduk = DatabaseHelper.GetKodeProdukByName(cmbProduct.Text)
            End If

            If String.IsNullOrWhiteSpace(kodeProduk) Then
                Debug.WriteLine($"[cmbProduct_SelectedIndexChanged] Kode produk tidak ditemukan untuk ProductID: {productID}")
                Return
            End If

            ' Generate No Tiket baru berdasarkan kode produk
            Dim noTiketBaru As String = DatabaseHelper.GenerateNoTiketByProduct(kodeProduk)

            If Not String.IsNullOrWhiteSpace(noTiketBaru) Then
                txtNoTiket.Text = noTiketBaru
                Debug.WriteLine($"[cmbProduct_SelectedIndexChanged] Generated NoTiket: {noTiketBaru}")
            End If

        Catch ex As Exception
            Debug.WriteLine($"[cmbProduct_SelectedIndexChanged] Error: {ex.Message}")
        End Try
    End Sub


    ' =============================================
    ' LOAD DAFTAR ANTRIAN - DENGAN FILTER TRANS TYPE
    ' =============================================
    Private Sub LoadAntrian()
        Try
            Dim query As String = "SELECT TimbangID, " &
            "NoTiket + ' - ' + NoPolisi + ' (' + ISNULL(NamaSupir, '-') + ') - ' + " &
            "FORMAT(BeratMasuk, 'N0') + ' KG' AS DisplayText " &
            "FROM Timbangan WHERE Status IN ('PROSES', 'MASUK') "

            ' =============================================
            ' FILTER TRANS TYPE BERDASARKAN USER SESSION
            ' =============================================
            query &= UserSession.GetTransTypeFilter("")

            query &= "ORDER BY CreatedAt DESC"

            Dim dtAntrian As DataTable = DatabaseHelper.ExecuteQuery(query)

            Dim drEmpty As DataRow = dtAntrian.NewRow()
            drEmpty("TimbangID") = 0
            drEmpty("DisplayText") = "-- Input Baru --"
            dtAntrian.Rows.InsertAt(drEmpty, 0)

            cmbAntrian.DataSource = dtAntrian
            cmbAntrian.DisplayMember = "DisplayText"
            cmbAntrian.ValueMember = "TimbangID"
            cmbAntrian.SelectedIndex = 0
        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.LoadAntrian] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat antrian timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' =============================================
    ' EVENT: PILIH ANTRIAN - Load data tiket yang dipilih
    ' =============================================
    Private Sub cmbAntrian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAntrian.SelectedIndexChanged
        Try
            If cmbAntrian.SelectedValue Is Nothing Then Return
            If Not IsNumeric(cmbAntrian.SelectedValue) Then Return

            Dim selectedID As Integer = CInt(cmbAntrian.SelectedValue)

            If selectedID = 0 Then
                SetNewMode()
            Else
                LoadTimbangan(selectedID)
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.LoadAntrianSelection] Error: " & ex.ToString())
        End Try
    End Sub

    ' =============================================
    ' LOAD DATA TIMBANGAN YANG SUDAH ADA
    ' =============================================
    Private Sub LoadTimbangan(timbangID As Integer)
        Try
            Dim query As String = "SELECT * FROM Timbangan WHERE TimbangID = @ID"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {New SqlParameter("@ID", timbangID)})

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim row As DataRow = dt.Rows(0)

            _currentTimbangID = timbangID
            _isEditMode = True

            ' === Load data dasar ke form ===
            txtNoTiket.Text = row("NoTiket").ToString()
            txtNoPolisi.Text = row("NoPolisi").ToString()
            txtNamaSupir.Text = If(IsDBNull(row("NamaSupir")), "", row("NamaSupir").ToString())
            txtNoDO.Text = If(IsDBNull(row("NoDO")), "", row("NoDO").ToString())

            ' === FIELD BARU ===
            If txtNoKontrak IsNot Nothing Then
                txtNoKontrak.Text = If(IsDBNull(row("NoKontrak")), "", row("NoKontrak").ToString())
            End If

            If txtNoSIM IsNot Nothing AndAlso dt.Columns.Contains("NoSIM") Then
                txtNoSIM.Text = If(IsDBNull(row("NoSIM")), "", row("NoSIM").ToString())
            End If

            If txtAlamat IsNot Nothing AndAlso dt.Columns.Contains("Alamat") Then
                txtAlamat.Text = If(IsDBNull(row("Alamat")), "", row("Alamat").ToString())
            End If

            If txtSegelAtas IsNot Nothing AndAlso dt.Columns.Contains("SegelAtas") Then
                txtSegelAtas.Text = If(IsDBNull(row("SegelAtas")), "", row("SegelAtas").ToString())
            End If

            If txtSegelBawah IsNot Nothing AndAlso dt.Columns.Contains("SegelBawah") Then
                txtSegelBawah.Text = If(IsDBNull(row("SegelBawah")), "", row("SegelBawah").ToString())
            End If

            ' === Load ComboBox TRANSPORTER ===
            If Not IsDBNull(row("TransporterNama")) AndAlso row("TransporterNama").ToString() <> "" Then
                cmbTransporter.Text = row("TransporterNama").ToString()
            ElseIf Not IsDBNull(row("TransporterID")) Then
                cmbTransporter.SelectedValue = row("TransporterID")
            Else
                cmbTransporter.SelectedIndex = -1
            End If

            ' === Load ComboBox CUSTOMER ===
            If Not IsDBNull(row("CustomerNama")) AndAlso row("CustomerNama").ToString() <> "" Then
                cmbCustomer.Text = row("CustomerNama").ToString()
            ElseIf Not IsDBNull(row("CustomerID")) Then
                cmbCustomer.SelectedValue = row("CustomerID")
            Else
                cmbCustomer.SelectedIndex = -1
            End If

            ' === Load ComboBox PRODUCT ===
            If Not IsDBNull(row("ProductNama")) AndAlso row("ProductNama").ToString() <> "" Then
                cmbProduct.Text = row("ProductNama").ToString()
            ElseIf Not IsDBNull(row("ProductID")) Then
                cmbProduct.SelectedValue = row("ProductID")
            Else
                cmbProduct.SelectedIndex = -1
            End If

            ' === Load ComboBox TRANS TYPE (BARU) ===
            If cmbTransType IsNot Nothing Then
                If dt.Columns.Contains("TransType") AndAlso Not IsDBNull(row("TransType")) Then
                    Dim transTypeValue As String = row("TransType").ToString()
                    Dim index As Integer = cmbTransType.Items.IndexOf(transTypeValue)
                    If index >= 0 Then
                        cmbTransType.SelectedIndex = index
                    Else
                        cmbTransType.SelectedIndex = 0 ' Default JUAL
                    End If
                Else
                    cmbTransType.SelectedIndex = 0 ' Default JUAL
                End If
            End If

            ' === Load berat yang sudah tersimpan (Timbangan Pertama) ===
            _beratTersimpan = 0
            _waktuTersimpan = DateTime.MinValue

            If Not IsDBNull(row("BeratMasuk")) Then
                _beratTersimpan = CDec(row("BeratMasuk"))
            End If

            If Not IsDBNull(row("TanggalMasuk")) Then
                _waktuTersimpan = CDate(row("TanggalMasuk"))
            End If

            ' Update tampilan berat
            lblBeratMasuk.Text = _beratTersimpan.ToString("N0") & " KG"
            lblWaktuMasuk.Text = If(_waktuTersimpan <> DateTime.MinValue, _waktuTersimpan.ToString("dd/MM/yyyy HH:mm:ss"), "-")

            ' Reset berat keluar (akan diisi saat timbang kedua)
            lblBeratKeluar.Text = "0 KG"
            lblWaktuKeluar.Text = "-"
            lblBeratNetto.Text = "0 KG"
            lblBeratBersih.Text = "0"

            ' Load Potongan
            txtPotonganPersen.Text = If(IsDBNull(row("PotonganPersen")), "0", CDec(row("PotonganPersen")).ToString())
            txtPotonganKg.Text = If(IsDBNull(row("PotonganCong")), "0", CDec(row("PotonganCong")).ToString())
            txtKeterangan.Text = If(IsDBNull(row("Keterangan")), "", row("Keterangan").ToString())

            ' Kunci data kendaraan (tidak boleh diubah saat timbang kedua)
            LockDataKendaraan(False)

            ' Audit Log
            DatabaseHelper.InsertAuditLog(UserSession.UserID, "VIEW_TIMBANGAN", timbangID, "Timbangan",
            timbangID, Nothing, Nothing, $"User membuka tiket: {txtNoTiket.Text}")

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.LoadTimbangan] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat transaksi timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' SET MODE INPUT BARU
    ' =============================================
    Private Sub SetNewMode()
        _currentTimbangID = 0
        _isEditMode = False
        _beratTersimpan = 0
        _waktuTersimpan = DateTime.MinValue

        ClearForm()
        ' === NO TIKET - Akan di-generate saat Product dipilih ===
        txtNoTiket.Text = "" ' Kosongkan dulu, akan terisi otomatis saat pilih product
        txtNoTiket.ReadOnly = True
        txtNoTiket.BackColor = Color.FromArgb(240, 240, 240)

        ' === NO DO - INPUT MANUAL DENGAN VALIDASI UNIK ===



        If txtNoKontrak IsNot Nothing Then
            txtNoKontrak.Text = GetSetting("NO_KONTRAK", "")
        End If

        ' === SET DEFAULT TRANS TYPE ===
        If cmbTransType IsNot Nothing AndAlso cmbTransType.Items.Count > 0 Then
            cmbTransType.SelectedIndex = 0 ' Default: JUAL
        End If

        ' Reset label berat
        lblBeratMasuk.Text = "0 KG"
        lblBeratKeluar.Text = "0 KG"
        lblBeratNetto.Text = "0 KG"
        lblTotalPotongan.Text = "0 KG"
        lblBeratBersih.Text = "0"
        lblWaktuMasuk.Text = "-"
        lblWaktuKeluar.Text = "-"

        ' Checkbox
        chkIncludeFFA.Enabled = True
        chkIncludeKeterangan.Enabled = True

        ' Field FFA disabled sampai checkbox dicentang
        txtFFA.Enabled = False
        txtMoisture.Enabled = False
        txtDirt.Enabled = False
        If txtSuhuMinyak IsNot Nothing Then txtSuhuMinyak.Enabled = False

        ' Field Keterangan disabled sampai checkbox dicentang
        txtKeterangan.Enabled = False
    End Sub

    ' =============================================
    ' CLEAR FORM
    ' =============================================
    Private Sub ClearForm()
        ' =============================================
        ' CLEAR NO DO PERTAMA (sebelum field lain)
        ' =============================================
        txtNoDO.Text = ""
        txtNoDO.BackColor = Color.White

        txtNoTiket.Clear()
        txtNoPolisi.Clear()
        txtNamaSupir.Clear()

        txtPotonganPersen.Text = "0"
        txtPotonganKg.Text = "0"
        txtKeterangan.Clear()
        txtFFA.Clear()
        txtMoisture.Clear()
        txtDirt.Clear()

        cmbTransporter.SelectedIndex = -1
        cmbCustomer.SelectedIndex = -1
        cmbProduct.SelectedIndex = -1

        chkIncludeFFA.Checked = False
        chkIncludeKeterangan.Checked = False

        ' === FIELD BARU ===
        If txtNoKontrak IsNot Nothing Then txtNoKontrak.Clear()
        If txtSegelAtas IsNot Nothing Then txtSegelAtas.Clear()
        If txtSegelBawah IsNot Nothing Then txtSegelBawah.Clear()
        If txtNoSIM IsNot Nothing Then txtNoSIM.Clear()
        If txtAlamat IsNot Nothing Then txtAlamat.Clear()
        If txtSuhuMinyak IsNot Nothing Then txtSuhuMinyak.Clear()
        If cmbTransType IsNot Nothing Then cmbTransType.SelectedIndex = 0 ' Default JUAL
    End Sub


    ' =============================================
    ' GENERATE NO TIKET BARU (BERDASARKAN PRODUCT)
    ' Dipanggil saat form load dengan product kosong
    ' No Tiket akan di-generate ulang saat product dipilih
    ' =============================================
    Private Sub GenerateNoTiket()
        Try
            ' Cek apakah product sudah dipilih
            If cmbProduct.SelectedIndex >= 0 AndAlso cmbProduct.SelectedValue IsNot Nothing Then
                Dim productID As Integer = 0
                If IsNumeric(cmbProduct.SelectedValue) Then
                    productID = CInt(cmbProduct.SelectedValue)
                End If

                If productID > 0 Then
                    Dim kodeProduk As String = DatabaseHelper.GetKodeProdukByID(productID)
                    If Not String.IsNullOrWhiteSpace(kodeProduk) Then
                        txtNoTiket.Text = DatabaseHelper.GenerateNoTiketByProduct(kodeProduk)
                        Return
                    End If
                End If
            End If

            ' Jika product belum dipilih, kosongkan No Tiket
            ' No Tiket akan otomatis terisi saat user memilih product
            txtNoTiket.Text = ""

        Catch ex As Exception
            txtNoTiket.Text = ""
            Debug.WriteLine($"[GenerateNoTiket] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' AMBIL BERAT DARI INDIKATOR
    ' =============================================
    Private Function AmbilBeratDariIndikator() As Decimal
        If _beratDariIndikator <> 0 Then Return _beratDariIndikator
        Dim berat As Decimal = SerialPortHelper.ReadWeightOnce()
        If Not SerialPortHelper.IsParseError(berat) Then Return berat
        Return 0
    End Function

    ' =============================================
    ' TOMBOL: AMBIL BERAT (DENGAN VALIDASI)
    ' =============================================
    Private Sub btnAmbilBerat_Click(sender As Object, e As EventArgs) Handles btnAmbilBerat.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            btnAmbilBerat.Enabled = False
            Application.DoEvents()

            Dim berat As Decimal = 0

            ' 1. Coba ambil dari variabel realtime dulu
            berat = _beratDariIndikator

            ' 2. Jika masih 0, coba baca manual (dengan timeout)
            If berat = 0 Then
                Try
                    berat = SerialPortHelper.ReadWeightOnce()

                    ' Cek apakah parse error
                    If SerialPortHelper.IsParseError(berat) Then
                        berat = 0
                    End If
                Catch exSerial As Exception
                    Debug.WriteLine($"[AmbilBerat] Serial Error: {exSerial.Message}")
                    berat = 0

                    lblStatusTimbangan.Text = "⚖ Berat = 0 atau tidak terbaca"
                    lblStatusTimbangan.ForeColor = Color.Red

                    MessageBox.Show("⚠️ Tidak ada data berat dari timbangan!" & vbCrLf & vbCrLf &
                        "Kemungkinan penyebab:" & vbCrLf &
                        "• Indikator tidak terhubung" & vbCrLf &
                        "• Tidak ada beban di timbangan" & vbCrLf &
                        "• Berat = 0 (timbangan kosong)" & vbCrLf &
                        "• Setting COM Port/Baud Rate tidak sesuai" & vbCrLf & vbCrLf &
                        "Pastikan ada beban di timbangan dan berat > 0.",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End Try
            End If

            ' =============================================
            ' VALIDASI: BERAT HARUS LEBIH DARI 0
            ' =============================================
            If berat <= 0 Then
                Me.Cursor = Cursors.Default
                btnAmbilBerat.Enabled = True

                MessageBox.Show(
                "⚠️ BERAT TIMBANGAN HARUS LEBIH DARI 0 KG!" & vbCrLf & vbCrLf &
                "Pastikan:" & vbCrLf &
                "✓ Kendaraan sudah berada di atas timbangan" & vbCrLf &
                "✓ Indikator timbangan menyala dan terhubung" & vbCrLf &
                "✓ Setting COM Port sudah benar" & vbCrLf &
                "✓ Berat stabil (tidak berkedip)" & vbCrLf & vbCrLf &
                "Berat saat ini: " & _beratDariIndikator.ToString("N0") & " KG",
                "Berat Tidak Valid",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
                Return
            End If

            ' =============================================
            ' PROSES BERAT YANG VALID
            ' =============================================

            ' Jika ini timbangan pertama (input baru)
            If _currentTimbangID = 0 Then
                lblBeratMasuk.Text = berat.ToString("N0") & " KG"
                lblWaktuMasuk.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")

                MessageBox.Show(
                "✅ Berat berhasil diambil!" & vbCrLf & vbCrLf &
                "Berat Masuk: " & berat.ToString("N0") & " KG" & vbCrLf &
                "Waktu: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                "Sukses",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
            Else
                ' Timbangan kedua - tampilkan preview netto
                Dim beratBesar As Decimal = Math.Max(_beratTersimpan, berat)
                Dim beratKecil As Decimal = Math.Min(_beratTersimpan, berat)
                Dim netto As Decimal = beratBesar - beratKecil

                ' Validasi netto tidak boleh 0 atau negatif
                If netto <= 0 Then
                    Me.Cursor = Cursors.Default
                    btnAmbilBerat.Enabled = True

                    MessageBox.Show(
                    "⚠️ BERAT NETTO TIDAK VALID!" & vbCrLf & vbCrLf &
                    "Berat Pertama: " & _beratTersimpan.ToString("N0") & " KG" & vbCrLf &
                    "Berat Kedua: " & berat.ToString("N0") & " KG" & vbCrLf &
                    "Selisih: " & netto.ToString("N0") & " KG" & vbCrLf & vbCrLf &
                    "Pastikan kendaraan dalam kondisi berbeda (isi/kosong)",
                    "Berat Tidak Valid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
                    Return
                End If

                lblBeratKeluar.Text = berat.ToString("N0") & " KG"
                lblWaktuKeluar.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")

                lblBeratMasuk.Text = beratBesar.ToString("N0") & " KG (BRUTO)"
                lblBeratKeluar.Text = beratKecil.ToString("N0") & " KG (TARA)"
                lblBeratNetto.Text = netto.ToString("N0") & " KG"

                HitungNetto(netto)

                MessageBox.Show(
                "✅ Berat kedua berhasil diambil!" & vbCrLf & vbCrLf &
                "BRUTO: " & beratBesar.ToString("N0") & " KG" & vbCrLf &
                "TARA: " & beratKecil.ToString("N0") & " KG" & vbCrLf &
                "NETTO: " & netto.ToString("N0") & " KG",
                "Sukses",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.btnAmbilBerat] Error: " & ex.ToString())
            MessageBox.Show("Gagal mengambil data berat dari indikator timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' PENTING: Selalu kembalikan cursor dan enable tombol
            Me.Cursor = Cursors.Default
            btnAmbilBerat.Enabled = True
        End Try
    End Sub

    ' =============================================
    ' HITUNG NETTO DAN BERAT BERSIH
    ' =============================================
    Private Sub HitungNetto(netto As Decimal)
        Try
            Dim potonganPersen As Decimal = 0
            Dim potonganKg As Decimal = 0
            Decimal.TryParse(txtPotonganPersen.Text, potonganPersen)
            Decimal.TryParse(txtPotonganKg.Text, potonganKg)

            Dim totalPotongan As Decimal = Math.Round((netto * potonganPersen / 100) + potonganKg, 0)
            Dim beratBersih As Decimal = netto - totalPotongan

            lblTotalPotongan.Text = totalPotongan.ToString("N0") & " KG"
            lblBeratBersih.Text = beratBersih.ToString("N0")
        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.HitungNetto] Error: " & ex.Message)
        End Try
    End Sub

    Private Sub txtPotonganPersen_TextChanged(sender As Object, e As EventArgs) Handles txtPotonganPersen.TextChanged
        If _currentTimbangID > 0 AndAlso _beratTersimpan > 0 Then
            Dim beratKedua As Decimal = GetBeratFromLabel(lblBeratKeluar.Text)
            If beratKedua > 0 Then
                Dim netto As Decimal = Math.Abs(_beratTersimpan - beratKedua)
                HitungNetto(netto)
            End If
        End If
    End Sub

    Private Sub txtPotonganKg_TextChanged(sender As Object, e As EventArgs) Handles txtPotonganKg.TextChanged
        If _currentTimbangID > 0 AndAlso _beratTersimpan > 0 Then
            Dim beratKedua As Decimal = GetBeratFromLabel(lblBeratKeluar.Text)
            If beratKedua > 0 Then
                Dim netto As Decimal = Math.Abs(_beratTersimpan - beratKedua)
                HitungNetto(netto)
            End If
        End If
    End Sub

    Private Function GetBeratFromLabel(text As String) As Decimal
        Dim cleanText As String = text.Replace("KG", "").Replace("(BRUTO)", "").Replace("(TARA)", "").Replace(".", "").Replace(",", "").Trim()
        Dim result As Decimal = 0
        Decimal.TryParse(cleanText, result)
        Return result
    End Function

    ' =============================================
    ' VALIDASI INPUT DASAR
    ' =============================================
    Private Function ValidasiInputDasar() As Boolean
        ' === VALIDASI NO POLISI ===
        If String.IsNullOrWhiteSpace(txtNoPolisi.Text) Then
            MessageBox.Show("No. Polisi harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNoPolisi.Focus()
            Return False
        End If

        ' === VALIDASI CUSTOMER ===
        If String.IsNullOrWhiteSpace(cmbCustomer.Text) Then
            MessageBox.Show("Customer harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbCustomer.Focus()
            Return False
        End If

        ' === VALIDASI PRODUCT ===
        If String.IsNullOrWhiteSpace(cmbProduct.Text) Then
            MessageBox.Show("Product harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbProduct.Focus()
            Return False
        End If

        ' === VALIDASI NO TIKET (HARUS SUDAH TERGENERATE) ===
        If String.IsNullOrWhiteSpace(txtNoTiket.Text) Then
            MessageBox.Show("No. Tiket belum ter-generate!" & vbCrLf &
                       "Silakan pilih Product terlebih dahulu.",
                       "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbProduct.Focus()
            Return False
        End If

        ' === VALIDASI NO DO (WAJIB DAN UNIK) ===
        If String.IsNullOrWhiteSpace(txtNoDO.Text) Then
            MessageBox.Show("No. DO harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNoDO.Focus()
            Return False
        End If

        ' Cek duplikat No. DO
        Dim errorMsg As String = DatabaseHelper.ValidateNoDO(txtNoDO.Text.Trim(), _currentTimbangID)
        If Not String.IsNullOrEmpty(errorMsg) Then
            MessageBox.Show(errorMsg, "Validasi No. DO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNoDO.Focus()
            txtNoDO.SelectAll()
            Return False
        End If

        Return True
    End Function

    ' =============================================
    ' TOMBOL: SIMPAN (SATU TOMBOL UNTUK SEMUA)
    ' Otomatis deteksi: Timbangan Pertama atau Kedua
    ' =============================================
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            ' Ambil berat dari label (yang sudah diambil sebelumnya)
            Dim beratSekarang As Decimal = GetBeratFromLabel(lblBeratMasuk.Text)

            ' Jika belum ambil berat
            If beratSekarang <= 0 AndAlso _currentTimbangID = 0 Then
                MessageBox.Show("Silakan klik 'Ambil Berat' terlebih dahulu!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' =============================================
            ' VALIDASI BERAT TIDAK BOLEH 0
            ' =============================================
            If beratSekarang <= 0 AndAlso _currentTimbangID = 0 Then
                MessageBox.Show(
                "⚠️ BERAT TIDAK BOLEH 0!" & vbCrLf & vbCrLf &
                "Silakan klik tombol 'Ambil Berat' terlebih dahulu " &
                "untuk mengambil data berat dari timbangan.",
                "Validasi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
                btnAmbilBerat.Focus()
                Return
            End If

            ' =============================================
            ' CASE 1: INPUT BARU (Timbangan Pertama)
            ' =============================================
            If _currentTimbangID = 0 Then
                If Not ValidasiInputDasar() Then Return

                ' Konfirmasi
                If MessageBox.Show($"Simpan data timbangan pertama?" & vbCrLf & vbCrLf &
                    $"No. Polisi: {txtNoPolisi.Text}" & vbCrLf &
                    $"Customer: {cmbCustomer.Text}" & vbCrLf &
                    $"Product: {cmbProduct.Text}" & vbCrLf &
                    $"Berat: {beratSekarang:N0} KG" & vbCrLf & vbCrLf &
                    "Kendaraan akan masuk antrian untuk timbang kedua.",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Return
                End If

                SimpanTimbanganPertama(beratSekarang)

                ' =============================================
                ' CASE 2: TIMBANGAN KEDUA (Selesaikan Transaksi)
                ' =============================================
            Else
                Dim beratKedua As Decimal = GetBeratFromLabel(lblBeratKeluar.Text)

                If beratKedua <= 0 Then
                    MessageBox.Show("Silakan klik 'Ambil Berat' untuk mengambil berat kedua!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Tentukan Bruto dan Tara otomatis
                Dim bruto As Decimal = Math.Max(_beratTersimpan, beratKedua)
                Dim tara As Decimal = Math.Min(_beratTersimpan, beratKedua)
                Dim netto As Decimal = bruto - tara

                ' Hitung potongan
                Dim potonganPersen As Decimal = 0
                Dim potonganKg As Decimal = 0
                Decimal.TryParse(txtPotonganPersen.Text, potonganPersen)
                Decimal.TryParse(txtPotonganKg.Text, potonganKg)
                Dim totalPotongan As Decimal = Math.Round((netto * potonganPersen / 100) + potonganKg, 0)
                Dim beratBersih As Decimal = netto - totalPotongan

                ' Konfirmasi
                If MessageBox.Show($"Selesaikan transaksi timbangan?" & vbCrLf & vbCrLf &
                    $"No. Tiket: {txtNoTiket.Text}" & vbCrLf &
                    $"No. Polisi: {txtNoPolisi.Text}" & vbCrLf &
                    $"══════════════════════" & vbCrLf &
                    $"BRUTO      : {bruto:N0} KG" & vbCrLf &
                    $"TARA       : {tara:N0} KG" & vbCrLf &
                    $"NETTO      : {netto:N0} KG" & vbCrLf &
                    $"Potongan   : {totalPotongan:N0} KG" & vbCrLf &
                    $"══════════════════════" & vbCrLf &
                    $"BERAT BERSIH: {beratBersih:N0} KG",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Return
                End If

                SimpanTimbanganKedua(bruto, tara, netto, totalPotongan, beratBersih)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.Save] Error: " & ex.ToString())
            MessageBox.Show("Gagal menyimpan transaksi timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' SIMPAN TIMBANGAN PERTAMA
    ' =============================================
    Private Sub SimpanTimbanganPertama(berat As Decimal)
        Try
            Dim transporterID As Object = GetComboBoxID(cmbTransporter)
            Dim customerID As Object = GetComboBoxID(cmbCustomer)
            Dim productID As Object = GetComboBoxID(cmbProduct)

            Dim query As String = "INSERT INTO Timbangan (" &
        "NoTiket, NoPolisi, NamaSupir, NoSIM, " &
        "TransporterID, TransporterNama, " &
        "CustomerID, CustomerNama, " &
        "Alamat, " &
        "ProductID, ProductNama, TransType, " &
        "NoDO, NoKontrak, SegelAtas, SegelBawah, " &
        "BeratMasuk, TanggalMasuk, Status, KraniMasuk, CreatedAt, CreatedBy) " &
        "VALUES (" &
        "@NoTiket, @NoPolisi, @NamaSupir, @NoSIM, " &
        "@TransporterID, @TransporterNama, " &
        "@CustomerID, @CustomerNama, " &
        "@Alamat, " &
        "@ProductID, @ProductNama, @TransType, " &
        "@NoDO, @NoKontrak, @SegelAtas, @SegelBawah, " &
        "@BeratMasuk, @TanggalMasuk, 'MASUK', @KraniID, GETDATE(), @CreatedBy); " &
        "SELECT SCOPE_IDENTITY();"

            ' Ambil NoKontrak dari textbox atau Settings
            Dim noKontrak As String = ""
            If txtNoKontrak IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtNoKontrak.Text) Then
                noKontrak = txtNoKontrak.Text.Trim()
            Else
                noKontrak = GetSetting("NO_KONTRAK", "")
            End If

            ' Ambil nilai field dengan null checking
            Dim noSIM As String = If(txtNoSIM IsNot Nothing, txtNoSIM.Text.Trim(), "")
            Dim alamat As String = If(txtAlamat IsNot Nothing, txtAlamat.Text.Trim(), "")
            Dim transType As String = If(cmbTransType IsNot Nothing AndAlso cmbTransType.SelectedItem IsNot Nothing,
                                 cmbTransType.SelectedItem.ToString(), "JUAL")
            Dim segelAtas As String = If(txtSegelAtas IsNot Nothing, txtSegelAtas.Text.Trim(), "")
            Dim segelBawah As String = If(txtSegelBawah IsNot Nothing, txtSegelBawah.Text.Trim(), "")

            Dim params As SqlParameter() = {
            New SqlParameter("@NoTiket", txtNoTiket.Text),
            New SqlParameter("@NoPolisi", txtNoPolisi.Text.Trim().ToUpperInvariant()),
            New SqlParameter("@NamaSupir", txtNamaSupir.Text.Trim()),
            New SqlParameter("@NoSIM", noSIM),
            New SqlParameter("@TransporterID", transporterID),
            New SqlParameter("@TransporterNama", cmbTransporter.Text.Trim()),
            New SqlParameter("@CustomerID", customerID),
            New SqlParameter("@CustomerNama", cmbCustomer.Text.Trim()),
            New SqlParameter("@Alamat", alamat),
            New SqlParameter("@ProductID", productID),
            New SqlParameter("@ProductNama", cmbProduct.Text.Trim()),
            New SqlParameter("@TransType", transType),
            New SqlParameter("@NoDO", txtNoDO.Text.Trim()),
            New SqlParameter("@NoKontrak", noKontrak),
            New SqlParameter("@SegelAtas", segelAtas),
            New SqlParameter("@SegelBawah", segelBawah),
            New SqlParameter("@BeratMasuk", berat),
            New SqlParameter("@TanggalMasuk", DateTime.Now),
            New SqlParameter("@KraniID", UserSession.UserID),
            New SqlParameter("@CreatedBy", UserSession.UserID)
        }

            Dim result As Object = DatabaseHelper.ExecuteScalar(query, params)

            If result IsNot Nothing Then
                _currentTimbangID = CInt(result)

                DatabaseHelper.InsertAuditLog(UserSession.UserID, "INSERT_TIMBANGAN_MASUK", _currentTimbangID,
            "Timbangan", _currentTimbangID, Nothing, Nothing,
            $"Timbangan masuk - No Tiket: {txtNoTiket.Text}, No Pol: {txtNoPolisi.Text}, Berat: {berat:N0} KG")

                MessageBox.Show($"✅ Data timbangan berhasil disimpan!" & vbCrLf & vbCrLf &
            $"No. Tiket: {txtNoTiket.Text}" & vbCrLf &
            $"Berat: {berat:N0} KG" & vbCrLf & vbCrLf &
            "Kendaraan sudah masuk antrian.",
            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If MessageBox.Show("Cetak Tiket Masuk?", "Cetak", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    CetakTiketMasuk()
                End If

                LoadAntrian()
                SetNewMode()
            Else
                MessageBox.Show("Gagal menyimpan data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.Save] Error: " & ex.ToString())
            MessageBox.Show("Gagal menyimpan transaksi timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' SIMPAN TIMBANGAN KEDUA (SELESAI)
    ' =============================================
    Private Sub SimpanTimbanganKedua(bruto As Decimal, tara As Decimal, netto As Decimal, totalPotongan As Decimal, beratBersih As Decimal)
        Try
            Dim potonganPersen As Decimal = 0
            Dim potonganKg As Decimal = 0
            Decimal.TryParse(txtPotonganPersen.Text, potonganPersen)
            Decimal.TryParse(txtPotonganKg.Text, potonganKg)

            ' Ambil data FFA jika ada
            Dim ffa As Decimal = 0
            Dim moisture As Decimal = 0
            Dim dirt As Decimal = 0
            Dim suhuMinyak As Decimal = 0

            If chkIncludeFFA.Checked Then
                Decimal.TryParse(txtFFA.Text, ffa)
                Decimal.TryParse(txtMoisture.Text, moisture)
                Decimal.TryParse(txtDirt.Text, dirt)
                If txtSuhuMinyak IsNot Nothing Then
                    Decimal.TryParse(txtSuhuMinyak.Text, suhuMinyak)
                End If
            End If

            Dim query As String = "UPDATE Timbangan SET " &
            "BeratMasuk = @Bruto, BeratKeluar = @Tara, TanggalKeluar = @TanggalKeluar, " &
            "BeratNetto = @Netto, PotonganPersen = @PotonganPersen, PotonganCong = @PotonganKg, " &
            "TotalPotongan = @TotalPotongan, BeratBersih = @BeratBersih, " &
            "IncludeFFA = @IncludeFFA, FFA = @FFA, Moisture = @Moisture, Dirt = @Dirt, " &
            "SuhuMinyak = @SuhuMinyak, " &
            "IncludeKeterangan = @IncludeKeterangan, Keterangan = @Keterangan, " &
            "Status = 'SELESAI', KraniKeluar = @KraniID, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy " &
            "WHERE TimbangID = @ID"

            Dim params As SqlParameter() = {
            New SqlParameter("@Bruto", bruto),
            New SqlParameter("@Tara", tara),
            New SqlParameter("@TanggalKeluar", DateTime.Now),
            New SqlParameter("@Netto", netto),
            New SqlParameter("@PotonganPersen", potonganPersen),
            New SqlParameter("@PotonganKg", potonganKg),
            New SqlParameter("@TotalPotongan", totalPotongan),
            New SqlParameter("@BeratBersih", beratBersih),
            New SqlParameter("@IncludeFFA", chkIncludeFFA.Checked),
            New SqlParameter("@FFA", ffa),
            New SqlParameter("@Moisture", moisture),
            New SqlParameter("@Dirt", dirt),
            New SqlParameter("@SuhuMinyak", suhuMinyak),
            New SqlParameter("@IncludeKeterangan", chkIncludeKeterangan.Checked),
            New SqlParameter("@Keterangan", txtKeterangan.Text.Trim()),
            New SqlParameter("@KraniID", UserSession.UserID),
            New SqlParameter("@UpdatedBy", UserSession.UserID),
            New SqlParameter("@ID", _currentTimbangID)
        }

            Dim rowsAffected As Integer = DatabaseHelper.ExecuteNonQuery(query, params)

            If rowsAffected > 0 Then
                ' Audit Log
                DatabaseHelper.InsertAuditLog(UserSession.UserID, "UPDATE_TIMBANGAN_SELESAI", _currentTimbangID,
                "Timbangan", _currentTimbangID, Nothing, Nothing,
                $"Timbangan selesai - No Tiket: {txtNoTiket.Text}, Bruto: {bruto:N0}, Tara: {tara:N0}, Netto: {netto:N0}, Bersih: {beratBersih:N0}")

                MessageBox.Show($"✅ Transaksi timbangan Selesai!" & vbCrLf & vbCrLf &
                $"BERAT BERSIH: {beratBersih:N0} KG",
                "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' ═══════════════════════════════════════════════
                ' CETAK STRUK SELESAI - LEWAT PrintHelper
                ' ═══════════════════════════════════════════════
                If MessageBox.Show("Cetak Struk Timbangan?", "Cetak", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    CetakStrukSelesai(bruto, tara, netto, totalPotongan, beratBersih)
                End If

                LoadAntrian()
                SetNewMode()
            Else
                MessageBox.Show("Gagal update data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.Save] Error: " & ex.ToString())
            MessageBox.Show("Gagal menyimpan transaksi timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═════════════════════════════════════════════════════════════
    ' CETAK TIKET MASUK - TERINTEGRASI DENGAN PrintHelper
    ' ═════════════════════════════════════════════════════════════
    Private Sub CetakTiketMasuk()
        Try
            Dim data As New PrintHelper.DataCetak()

            ' === DATA IDENTITAS ===
            data.TimbangID = _currentTimbangID
            data.NoTiket = txtNoTiket.Text
            data.NoDO = txtNoDO.Text

            ' === NO KONTRAK ===
            If txtNoKontrak IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtNoKontrak.Text) Then
                data.NoKontrak = txtNoKontrak.Text.Trim()
            Else
                data.NoKontrak = GetSetting("NO_KONTRAK", "")
            End If

            ' === DATA KENDARAAN ===
            data.NoPolisi = txtNoPolisi.Text
            data.NamaSupir = txtNamaSupir.Text
            data.Transporter = cmbTransporter.Text
            data.Customer = cmbCustomer.Text
            data.Product = cmbProduct.Text

            ' === FIELD BARU ===
            If txtAlamat IsNot Nothing Then data.Alamat = txtAlamat.Text.Trim()
            If cmbTransType IsNot Nothing AndAlso cmbTransType.SelectedItem IsNot Nothing Then
                data.TransType = cmbTransType.SelectedItem.ToString()
            Else
                data.TransType = "JUAL"
            End If
            If txtNoSIM IsNot Nothing Then data.NoSIM = txtNoSIM.Text.Trim()

            ' === DATA BERAT ===
            data.BeratBruto = GetBeratFromLabel(lblBeratMasuk.Text)
            data.WaktuBruto = lblWaktuMasuk.Text

            ' === DATA SEGEL ===
            If txtSegelAtas IsNot Nothing Then data.SegelAtas = txtSegelAtas.Text.Trim()
            If txtSegelBawah IsNot Nothing Then data.SegelBawah = txtSegelBawah.Text.Trim()

            ' === INFO TAMBAHAN ===
            data.NamaPetugas = UserSession.NamaLengkap
            data.IsDuplikat = False

            ' Panggil PrintHelper
            PrintHelper.CetakTiketMasukAuto(data)

            ' Audit Log
            DatabaseHelper.InsertAuditLog(UserSession.UserID, "PRINT_TIKET_MASUK", _currentTimbangID,
            "Timbangan", _currentTimbangID, Nothing, Nothing,
            $"Cetak tiket masuk - No Tiket: {txtNoTiket.Text}")

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.CetakTiketMasuk] Error: " & ex.ToString())
            MessageBox.Show("Gagal mencetak tiket masuk. Periksa printer dan pengaturannya.", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═════════════════════════════════════════════════════════════
    ' CETAK STRUK SELESAI - TERINTEGRASI DENGAN PrintHelper
    ' ═════════════════════════════════════════════════════════════
    Private Sub CetakStrukSelesai(bruto As Decimal, tara As Decimal, netto As Decimal,
                              totalPotongan As Decimal, beratBersih As Decimal)
        Try
            Dim data As New PrintHelper.DataCetak()

            ' === DATA IDENTITAS ===
            data.TimbangID = _currentTimbangID
            data.NoTiket = txtNoTiket.Text
            data.NoDO = txtNoDO.Text

            ' === NO KONTRAK ===
            If txtNoKontrak IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtNoKontrak.Text) Then
                data.NoKontrak = txtNoKontrak.Text.Trim()
            Else
                data.NoKontrak = GetSetting("NO_KONTRAK", "")
            End If

            ' === DATA KENDARAAN ===
            data.NoPolisi = txtNoPolisi.Text
            data.NamaSupir = txtNamaSupir.Text
            data.Transporter = cmbTransporter.Text
            data.Customer = cmbCustomer.Text
            data.Product = cmbProduct.Text

            ' === FIELD BARU ===
            If txtAlamat IsNot Nothing Then data.Alamat = txtAlamat.Text.Trim()
            If cmbTransType IsNot Nothing AndAlso cmbTransType.SelectedItem IsNot Nothing Then
                data.TransType = cmbTransType.SelectedItem.ToString()
            Else
                data.TransType = "JUAL"
            End If
            If txtNoSIM IsNot Nothing Then data.NoSIM = txtNoSIM.Text.Trim()

            ' === DATA BERAT ===
            data.BeratBruto = bruto
            data.BeratTara = tara
            data.BeratNetto = netto
            data.TotalPotongan = totalPotongan
            data.BeratBersih = beratBersih

            ' === DATA POTONGAN ===
            Decimal.TryParse(txtPotonganPersen.Text, data.PotonganPersen)
            Decimal.TryParse(txtPotonganKg.Text, data.PotonganKg)

            ' === WAKTU TIMBANG ===
            data.WaktuBruto = lblWaktuMasuk.Text
            data.WaktuTara = lblWaktuKeluar.Text

            ' === DATA FFA (ANALISA LAB) ===
            data.IncludeFFA = chkIncludeFFA.Checked
            If data.IncludeFFA Then
                Decimal.TryParse(txtFFA.Text, data.FFA)
                Decimal.TryParse(txtMoisture.Text, data.Moisture)
                Decimal.TryParse(txtDirt.Text, data.Dirt)
                If txtSuhuMinyak IsNot Nothing Then
                    Decimal.TryParse(txtSuhuMinyak.Text, data.SuhuMinyak)
                End If
            End If

            ' === DATA KETERANGAN ===
            data.IncludeKeterangan = chkIncludeKeterangan.Checked
            data.Keterangan = txtKeterangan.Text.Trim()

            ' === DATA SEGEL ===
            If txtSegelAtas IsNot Nothing Then
                data.SegelAtas = txtSegelAtas.Text.Trim()
            End If
            If txtSegelBawah IsNot Nothing Then
                data.SegelBawah = txtSegelBawah.Text.Trim()
            End If

            ' === INFO TAMBAHAN ===
            data.NamaPetugas = UserSession.NamaLengkap
            data.IsDuplikat = False

            ' ═══════════════════════════════════════════════
            ' PANGGIL PrintHelper
            ' ═══════════════════════════════════════════════
            PrintHelper.CetakStrukSelesai(data)

            ' Audit Log
            DatabaseHelper.InsertAuditLog(UserSession.UserID, "PRINT_STRUK_SELESAI", _currentTimbangID,
            "Timbangan", _currentTimbangID, Nothing, Nothing,
            $"Cetak struk selesai - No Tiket: {txtNoTiket.Text}, Berat Bersih: {beratBersih:N0} KG")

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.CetakStrukSelesai] Error: " & ex.ToString())
            MessageBox.Show("Gagal mencetak struk selesai. Periksa printer dan pengaturannya.", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' TOMBOL: CETAK (Manual)
    ' =============================================
    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Try
            If _currentTimbangID <= 0 Then
                MessageBox.Show("Tidak ada data untuk dicetak!", "Peringatan",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ═══════════════════════════════════════════════
            ' CETAK LEWAT PrintHelper.CetakDariDatabase
            ' Otomatis menentukan jenis cetak berdasarkan status
            ' ═══════════════════════════════════════════════
            PrintHelper.CetakDariDatabase(_currentTimbangID, isDuplikat:=False)

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.btnCetak] Error: " & ex.ToString())
            MessageBox.Show("Gagal mencetak dokumen. Periksa printer dan pengaturannya.", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================
    ' CETAK ULANG (DUPLIKAT)
    ' =============================================
    Public Sub CetakUlangStruk(timbangID As Integer)
        PrintHelper.CetakDariDatabase(timbangID, isDuplikat:=True)
    End Sub

    ' Event Handler untuk Checkbox Include FFA/Analisa Lab
    Private Sub chkIncludeFFA_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeFFA.CheckedChanged
        Dim isChecked As Boolean = chkIncludeFFA.Checked

        ' Enable/disable kontrol analisa
        lblFFA.Enabled = isChecked
        txtFFA.Enabled = isChecked
        lblMoisture.Enabled = isChecked
        txtMoisture.Enabled = isChecked
        lblDirt.Enabled = isChecked
        txtDirt.Enabled = isChecked
        txtSuhuMinyak.Enabled = isChecked

        ' Ubah warna teks label menjadi hitam saat enabled, abu-abu saat disabled
        If isChecked Then
            lblFFA.ForeColor = Color.Black
            lblMoisture.ForeColor = Color.Black
            lblDirt.ForeColor = Color.Black

            ' Ubah background textbox untuk indikasi editable
            txtFFA.BackColor = Color.White
            txtMoisture.BackColor = Color.White
            txtDirt.BackColor = Color.White
            txtSuhuMinyak.BackColor = Color.White
        Else
            lblFFA.ForeColor = SystemColors.GrayText
            lblMoisture.ForeColor = SystemColors.GrayText
            lblDirt.ForeColor = SystemColors.GrayText

            ' Reset background
            txtFFA.BackColor = Color.WhiteSmoke
            txtMoisture.BackColor = Color.WhiteSmoke
            txtDirt.BackColor = Color.WhiteSmoke
            txtSuhuMinyak.BackColor = Color.WhiteSmoke
        End If
    End Sub

    ' Event Handler untuk Checkbox Include Keterangan
    Private Sub chkIncludeKeterangan_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeKeterangan.CheckedChanged
        Dim isChecked As Boolean = chkIncludeKeterangan.Checked

        lblKeterangan.Enabled = isChecked
        txtKeterangan.Enabled = isChecked

        ' Ubah warna teks menjadi hitam saat enabled
        If isChecked Then
            lblKeterangan.ForeColor = Color.Black
            txtKeterangan.BackColor = Color.White
        Else
            lblKeterangan.ForeColor = SystemColors.GrayText
            txtKeterangan.BackColor = Color.WhiteSmoke
        End If
    End Sub

    ' =============================================
    ' TOMBOL: BATAL / RESET
    ' =============================================
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        If MessageBox.Show("Reset form dan mulai input baru?", "Konfirmasi",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            ' =============================================
            ' CLEAR txtNoDO DULU SEBELUM SetNewMode
            ' Ini mencegah validasi saat Leave event
            ' =============================================
            txtNoDO.Text = ""
            txtNoDO.BackColor = Color.White

            SetNewMode()
        End If
    End Sub

    ' =============================================
    ' TOMBOL: REFRESH KONEKSI
    ' =============================================
    Private Sub btnRefreshConnection_Click(sender As Object, e As EventArgs) Handles btnRefreshConnection.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            btnRefreshConnection.Enabled = False
            btnRefreshConnection.Text = "⟳  Connecting..."
            Application.DoEvents()

            ' Disconnect dulu
            SerialPortHelper.Disconnect()
            System.Threading.Thread.Sleep(500)

            ' Connect ulang — brand terbaru otomatis dibaca dari DB
            ConnectToScale()

        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.RefreshConnection] Error: " & ex.ToString())
            UpdateConnectionStatus(False, "Gagal memperbarui koneksi")
            MessageBox.Show("Gagal memperbarui koneksi ke indikator timbangan.", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            btnRefreshConnection.Enabled = True
            btnRefreshConnection.Text = "⟳  Refresh"
        End Try
    End Sub

    ' =============================================
    ' TOMBOL: TUTUP
    ' =============================================
    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        ' Clear No DO dulu untuk mencegah validasi
        txtNoDO.Text = ""
        Me.Close()
    End Sub


    ' =============================================
    ' LOCK/UNLOCK DATA KENDARAAN
    ' =============================================
    Private Sub LockDataKendaraan(isLocked As Boolean)
        txtNoTiket.Enabled = Not isLocked
        txtNoPolisi.Enabled = Not isLocked
        txtNamaSupir.Enabled = Not isLocked
        cmbTransporter.Enabled = Not isLocked
        cmbCustomer.Enabled = Not isLocked
        cmbProduct.Enabled = Not isLocked
        txtNoDO.Enabled = Not isLocked
        txtNoDO.ReadOnly = isLocked

        ' === FIELD BARU ===
        If txtNoSIM IsNot Nothing Then txtNoSIM.Enabled = Not isLocked
        If txtAlamat IsNot Nothing Then txtAlamat.Enabled = Not isLocked
        If cmbTransType IsNot Nothing Then cmbTransType.Enabled = Not isLocked
        If txtSegelAtas IsNot Nothing Then txtSegelAtas.Enabled = Not isLocked
        If txtSegelBawah IsNot Nothing Then txtSegelBawah.Enabled = Not isLocked
        If txtNoKontrak IsNot Nothing Then txtNoKontrak.Enabled = Not isLocked

        ' Ubah warna background
        Dim bgColor As Color = If(isLocked, Color.FromArgb(240, 240, 240), Color.White)
        Dim bgColorReadOnly As Color = If(isLocked, Color.FromArgb(230, 230, 230), Color.White)

        txtNoTiket.BackColor = bgColorReadOnly
        txtNoPolisi.BackColor = bgColor
        txtNamaSupir.BackColor = bgColor
        txtNoDO.BackColor = Color.White

        ' === FIELD BARU - Warna ===
        If txtNoSIM IsNot Nothing Then txtNoSIM.BackColor = bgColor
        If txtAlamat IsNot Nothing Then txtAlamat.BackColor = bgColor
        If txtNoKontrak IsNot Nothing Then txtNoKontrak.BackColor = bgColor
        If txtSegelAtas IsNot Nothing Then txtSegelAtas.BackColor = bgColor
        If txtSegelBawah IsNot Nothing Then txtSegelBawah.BackColor = bgColor
    End Sub

    Private Function GetComboBoxID(cmb As ComboBox) As Object
        Try
            If cmb.SelectedIndex >= 0 AndAlso cmb.SelectedValue IsNot Nothing Then
                Return cmb.SelectedValue
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.GetComboBoxID] Error: " & ex.ToString())
        End Try
        Return DBNull.Value
    End Function

    Private Function GetSetting(key As String, defaultValue As String) As String
        Try
            Dim result As Object = DatabaseHelper.ExecuteScalar(
                "SELECT SettingValue FROM Settings WHERE SettingKey = @Key",
                {New SqlParameter("@Key", key)})
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then Return result.ToString()
        Catch ex As Exception
            Debug.WriteLine("[FormInputTimbangan.GetSetting] Error: " & ex.ToString())
        End Try
        Return defaultValue
    End Function

    ' =============================================
    ' FORM CLOSING
    ' =============================================
    Private Sub FormInputTimbangan_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' === 1. Lepas event handler (resource cleanup) ===
        Try
            RemoveHandler SerialPortHelper.WeightReceived, AddressOf OnWeightReceived
            RemoveHandler SerialPortHelper.ConnectionStatusChanged, AddressOf OnConnectionStatusChanged
            RemoveHandler SerialPortHelper.ErrorOccurred, AddressOf OnSerialError
        Catch ex As Exception
            Debug.WriteLine($"[FormClosing][RemoveHandler] {ex.Message}")
        End Try

        ' === 2. Audit log ===
        Try
            DatabaseHelper.InsertAuditLog(
                UserSession.UserID,
                "CLOSE_FORM", Nothing, "Input Timbangan",
                Nothing, Nothing, Nothing,
                $"User {UserSession.NamaLengkap} menutup Input Timbangan")
        Catch ex As Exception
            Debug.WriteLine($"[FormClosing][AuditLog] {ex.Message}")
        End Try
    End Sub
    ' =============================================
    ' FITUR NAVIGASI ENTER (Pindah Fokus & Klik Simpan)
    ' =============================================
    Private Sub FormInputTimbangan_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Cek jika tombol yang ditekan adalah ENTER
        If e.KeyCode = Keys.Enter Then

            ' 1. Jika fokus sedang berada di tombol SIMPAN, lakukan KLIK 
            If Me.ActiveControl Is btnSimpan Then
                btnSimpan.PerformClick()
                e.Handled = True
                e.SuppressKeyPress = True
                Return
            End If

            ' 2. Pengecualian: Jika sedang mengetik di Keterangan (dan multiline), biarkan Enter membuat baris baru
            ' (Hapus blok If ini jika ingin txtKeterangan juga langsung pindah saat di-Enter)
            If TypeOf Me.ActiveControl Is TextBox Then
                Dim txt As TextBox = DirectCast(Me.ActiveControl, TextBox)
                If txt.Multiline AndAlso txt.Name = "txtKeterangan" Then Return
            End If

            ' 3. Pindah ke control berikutnya (Sama seperti tekan TAB)
            e.Handled = True
            e.SuppressKeyPress = True ' Mencegah bunyi 'Ding'
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

        End If

        ' Shortcut: Ctrl+Shift+D = DEBUG
        If e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.D Then
            e.Handled = True
            RunDebugSerial()
        End If
    End Sub

    Private Sub lblMoisture_Click(sender As Object, e As EventArgs) Handles lblMoisture.Click

    End Sub

    Private Sub lblDirt_Click(sender As Object, e As EventArgs) Handles lblDirt.Click

    End Sub

    Private Sub PanelKanan_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    ' =============================================
    ' EVENT: VALIDASI NO DO SAAT LEAVE (KEHILANGAN FOKUS)
    ' =============================================
    Private Sub txtNoDO_Leave(sender As Object, e As EventArgs) Handles txtNoDO.Leave
        Try
            ' =============================================
            ' SKIP VALIDASI JIKA KLIK TOMBOL TERTENTU
            ' =============================================
            Dim activeControl As Control = Me.ActiveControl

            ' Cek apakah focus pindah ke tombol yang tidak perlu validasi
            If activeControl IsNot Nothing Then
                ' Skip jika klik tombol Batal, Tutup, Refresh, atau ComboBox Antrian
                If activeControl Is btnBatal OrElse
               activeControl Is btnTutup OrElse
               activeControl Is btnRefreshConnection OrElse
               activeControl Is cmbAntrian Then
                    Return ' Langsung keluar, tidak validasi
                End If

                ' Skip jika nama control mengandung kata-kata tertentu
                Dim controlName As String = activeControl.Name.ToLowerInvariant()
                If controlName.Contains("batal") OrElse
               controlName.Contains("tutup") OrElse
               controlName.Contains("close") OrElse
               controlName.Contains("cancel") OrElse
               controlName.Contains("refresh") OrElse
               controlName.Contains("antrian") Then
                    Return ' Langsung keluar, tidak validasi
                End If
            End If

            ' =============================================
            ' LAKUKAN VALIDASI HANYA JIKA NO DO TIDAK KOSONG
            ' =============================================
            If Not String.IsNullOrWhiteSpace(txtNoDO.Text) Then
                ValidateNoDOInput()
            End If

        Catch ex As Exception
            Debug.WriteLine($"[txtNoDO_Leave] Error: {ex.Message}")
        End Try
    End Sub

    ' =============================================
    ' EVENT: VALIDASI NO DO SAAT TEXT BERUBAH (OPSIONAL - REALTIME)
    ' =============================================
    Private Sub txtNoDO_TextChanged(sender As Object, e As EventArgs) Handles txtNoDO.TextChanged
        ' Reset warna saat mengetik
        txtNoDO.BackColor = Color.White
    End Sub

    ' =============================================
    ' FUNGSI VALIDASI NO DO INPUT
    ' Hanya tampilkan pesan jika memang ada duplikat dan bukan saat clear form
    ' =============================================
    Private Function ValidateNoDOInput(Optional showMessage As Boolean = True) As Boolean
        Try
            Dim noDO As String = txtNoDO.Text.Trim()

            ' Jika kosong, anggap valid (akan divalidasi saat simpan)
            If String.IsNullOrWhiteSpace(noDO) Then
                txtNoDO.BackColor = Color.White
                Return True
            End If

            ' Cek duplikat
            Dim excludeID As Integer = If(_currentTimbangID > 0, _currentTimbangID, 0)

            If DatabaseHelper.IsNoDOExists(noDO, excludeID) Then
                ' Duplikat ditemukan
                txtNoDO.BackColor = Color.FromArgb(255, 200, 200) ' Merah muda

                ' Hanya tampilkan pesan jika parameter showMessage = True
                If showMessage Then
                    MessageBox.Show(
                        $"⚠️ No. DO '{noDO}' sudah digunakan!" & vbCrLf & vbCrLf &
                        "Silakan gunakan nomor DO yang lain.",
                        "No. DO Duplikat",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)

                    txtNoDO.Focus()
                    txtNoDO.SelectAll()
                End If

                Return False
            Else
                ' Valid
                txtNoDO.BackColor = Color.FromArgb(200, 255, 200) ' Hijau muda
                Return True
            End If

        Catch ex As Exception
            Debug.WriteLine($"[ValidateNoDOInput] Error: {ex.Message}")
            Return True ' Biarkan lanjut jika error
        End Try
    End Function

    Private Sub RunDebugSerial()
        ' ... (isi kode debug yang sama, 
        '      tapi hapus baris "Handles btnDebug.Click")

        Me.Cursor = Cursors.WaitCursor
        Dim info As New System.Text.StringBuilder()

        Try
            info.AppendLine("=== DIAGNOSTIC ===")
            info.AppendLine(SerialPortHelper.GetDiagnosticInfo())
            info.AppendLine()
            info.AppendLine("=== FORM STATE ===")
            info.AppendLine($"_beratDariIndikator: {_beratDariIndikator}")
            info.AppendLine($"lblBeratRealtime: [{lblBeratRealtime.Text}]")
            info.AppendLine($"lblStatus: [{lblStatusTimbangan.Text}]")
            info.AppendLine($"IsConnected: {SerialPortHelper.IsConnected}")
            info.AppendLine()
            info.AppendLine("=== DB SETTINGS ===")
            info.AppendLine($"COM_PORT: {GetSetting("COM_PORT", "?")}")
            info.AppendLine($"BAUD_RATE: {GetSetting("BAUD_RATE", "?")}")
            info.AppendLine($"SCALE_BRAND: {GetSetting("SCALE_BRAND", "?")}")
            info.AppendLine($"SCALE_TYPE: {GetSetting("SCALE_TYPE", "?")}")
            info.AppendLine()
            info.AppendLine("=== RAW DATA (5 detik) ===")
            info.AppendLine(SerialPortHelper.ReadRawData(5000))
        Catch ex As Exception
            info.AppendLine($"ERROR: {ex.Message}")
        End Try

        Me.Cursor = Cursors.Default

        Dim f As New Form()
        f.Text = "DEBUG SERIAL"
        f.Size = New Size(700, 500)
        f.StartPosition = FormStartPosition.CenterScreen

        Dim t As New TextBox()
        t.Multiline = True
        t.ScrollBars = ScrollBars.Both
        t.Dock = DockStyle.Fill
        t.Font = New Font("Consolas", 9)
        t.Text = info.ToString()
        t.ReadOnly = True
        t.BackColor = Color.Black
        t.ForeColor = Color.Lime

        Dim b As New Button()
        b.Text = "COPY"
        b.Dock = DockStyle.Bottom
        b.Height = 35
        AddHandler b.Click, Sub()
                                Clipboard.SetText(info.ToString())
                                MessageBox.Show("Copied!")
                            End Sub
        f.Controls.Add(t)
        f.Controls.Add(b)
        f.ShowDialog()
    End Sub

    Private Sub lblBeratRealtime_Click(sender As Object, e As EventArgs) Handles lblBeratRealtime.Click

    End Sub
End Class
