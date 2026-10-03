' =============================================
' FormEditTimbangan.vb
' Form untuk Edit Data Timbangan
' HANYA MANAGER & DIREKTUR YANG BISA AKSES
' DENGAN FIELD NO. SEGEL
' =============================================
Imports System.Data.SqlClient

Public Class FormEditTimbangan

    Private _timbangID As Integer
    Private _originalData As Dictionary(Of String, String)
    Private _status As String
    Private _noTiket As String

    ' =============================================
    ' CONSTRUCTOR
    ' =============================================
    Public Sub New(timbangID As Integer)
        InitializeComponent()
        _timbangID = timbangID
        _originalData = New Dictionary(Of String, String)
    End Sub

    ' =============================================
    ' FORM LOAD
    ' =============================================
    Private Sub FormEditTimbangan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' CEK HAK AKSES
            If Not CanEditTimbangan() Then
                MessageBox.Show("⛔ Akses Ditolak!" & vbCrLf & vbCrLf &
                               "Hanya Manager dan Direktur yang dapat mengedit Data Timbangan." & vbCrLf & vbCrLf &
                               "Hubungi atasan Anda jika ada data yang perlu dikoreksi.",
                               "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Me.Close()
                Return
            End If

            Me.Text = "✏️ Edit Data Timbangan"
            lblEditInfo.Text = $"👤 Diedit oleh: {UserSession.NamaLengkap} ({UserSession.RoleName}) | 📅 {DateTime.Now:dd/MM/yyyy HH:mm}"

            LoadComboBoxData()
            LoadTimbangan()
            SetupEventHandlers()

            ' Audit Log: Form dibuka
            DatabaseHelper.InsertAuditLog(UserSession.UserID, "Edit Timbangan", _timbangID,
                "Timbangan", _timbangID, Nothing, Nothing,
                $"User {UserSession.NamaLengkap} membuka Form Edit Timbangan: {_timbangID}")

        Catch ex As Exception
            Debug.WriteLine("[FormEditTimbangan] Error: " & ex.ToString())
            MessageBox.Show("Terjadi kesalahan saat memproses data timbangan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    ' =============================================
    ' CEK HAK AKSES
    ' =============================================
    Private Function CanEditTimbangan() As Boolean
        Return UserSession.IsManager() OrElse
               UserSession.IsDirektur() OrElse
               UserSession.IsProgrammer()
    End Function

    ' =============================================
    ' SETUP EVENT HANDLERS
    ' =============================================
    Private Sub SetupEventHandlers()
        ' Auto hitung saat berat atau potongan berubah
        AddHandler txtBeratBruto.TextChanged, AddressOf HitungNetto
        AddHandler txtBeratTara.TextChanged, AddressOf HitungNetto
        AddHandler txtPotonganPersen.TextChanged, AddressOf HitungNetto
        AddHandler txtPotonganKg.TextChanged, AddressOf HitungNetto
    End Sub

    ' =============================================
    ' LOAD COMBOBOX
    ' =============================================
    Private Sub LoadComboBoxData()
        Try
            ' Transporter
            Dim dtTransporter As DataTable = DatabaseHelper.ExecuteQuery(
                "SELECT TransporterID, NamaTransporter FROM Transporters WHERE IsActive = 1 ORDER BY NamaTransporter")
            cmbTransporter.DataSource = dtTransporter
            cmbTransporter.DisplayMember = "NamaTransporter"
            cmbTransporter.ValueMember = "TransporterID"
            cmbTransporter.SelectedIndex = -1

            ' Customer
            Dim dtCustomer As DataTable = DatabaseHelper.ExecuteQuery(
                "SELECT CustomerID, NamaCustomer FROM Customers WHERE IsActive = 1 ORDER BY NamaCustomer")
            cmbCustomer.DataSource = dtCustomer
            cmbCustomer.DisplayMember = "NamaCustomer"
            cmbCustomer.ValueMember = "CustomerID"
            cmbCustomer.SelectedIndex = -1

            ' Product
            Dim dtProduct As DataTable = DatabaseHelper.ExecuteQuery(
                "SELECT ProductID, NamaProduk FROM Products WHERE IsActive = 1 ORDER BY NamaProduk")
            cmbProduct.DataSource = dtProduct
            cmbProduct.DisplayMember = "NamaProduk"
            cmbProduct.ValueMember = "ProductID"
            cmbProduct.SelectedIndex = -1

        Catch ex As Exception
            Debug.WriteLine("[FormEditTimbangan.LoadMaster] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat data master.")
        End Try
    End Sub

    ' =============================================
    ' LOAD DATA TIMBANGAN
    ' =============================================
    Private Sub LoadTimbangan()
        Try
            Dim query As String = "SELECT t.*, u.NamaLengkap AS NamaOperator " &
                                 "FROM Timbangan t " &
                                 "LEFT JOIN Users u ON t.KraniMasuk = u.UserID " &
                                 "WHERE t.TimbangID = @ID"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {New SqlParameter("@ID", _timbangID)})

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Data tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            Dim row As DataRow = dt.Rows(0)
            SaveOriginalData(row)

            ' === INFO TIKET (READ ONLY) ===
            _noTiket = row("NoTiket").ToString()
            txtNoTiket.Text = _noTiket
            _status = row("Status").ToString()
            lblStatus.Text = _status

            ' Set warna status
            Select Case _status.ToUpperInvariant()
                Case "SELESAI" : lblStatus.ForeColor = Color.Green
                Case "MASUK" : lblStatus.ForeColor = Color.Blue
                Case "BATAL" : lblStatus.ForeColor = Color.Red
            End Select

            txtTglInput.Text = If(IsDBNull(row("CreatedAt")), "-", CDate(row("CreatedAt")).ToString("dd/MM/yyyy HH:mm:ss"))
            txtOperator.Text = If(IsDBNull(row("NamaOperator")), "-", row("NamaOperator").ToString())

            ' === DATA KENDARAAN ===
            txtNoPolisi.Text = row("NoPolisi").ToString()
            txtNamaSupir.Text = If(IsDBNull(row("NamaSupir")), "", row("NamaSupir").ToString())
            txtNoDO.Text = If(IsDBNull(row("NoDO")), "", row("NoDO").ToString())

            ' === NO KONTRAK (BARU) ===
            If txtNoKontrak IsNot Nothing Then
                txtNoKontrak.Text = If(IsDBNull(row("NoKontrak")), "", row("NoKontrak").ToString())
            End If

            ' === NO SEGEL (BARU) ===
            If txtSegelAtas IsNot Nothing Then
                txtSegelAtas.Text = If(IsDBNull(row("SegelAtas")), "", row("SegelAtas").ToString())
            End If
            If txtSegelBawah IsNot Nothing Then
                txtSegelBawah.Text = If(IsDBNull(row("SegelBawah")), "", row("SegelBawah").ToString())
            End If

            ' ComboBox
            If Not IsDBNull(row("TransporterNama")) Then cmbTransporter.Text = row("TransporterNama").ToString()
            If Not IsDBNull(row("CustomerNama")) Then cmbCustomer.Text = row("CustomerNama").ToString()
            If Not IsDBNull(row("ProductNama")) Then cmbProduct.Text = row("ProductNama").ToString()

            ' === DATA BERAT ===
            txtBeratBruto.Text = If(IsDBNull(row("BeratMasuk")), "0", CDec(row("BeratMasuk")).ToString("N0"))
            txtBeratTara.Text = If(IsDBNull(row("BeratKeluar")), "0", CDec(row("BeratKeluar")).ToString("N0"))
            txtBeratNetto.Text = If(IsDBNull(row("BeratNetto")), "0", CDec(row("BeratNetto")).ToString("N0"))

            lblWaktuBruto.Text = If(IsDBNull(row("TanggalMasuk")), "-", CDate(row("TanggalMasuk")).ToString("dd/MM HH:mm"))
            lblWaktuTara.Text = If(IsDBNull(row("TanggalKeluar")), "-", CDate(row("TanggalKeluar")).ToString("dd/MM HH:mm"))

            ' === POTONGAN ===
            txtPotonganPersen.Text = If(IsDBNull(row("PotonganPersen")), "0", CDec(row("PotonganPersen")).ToString())
            txtPotonganKg.Text = If(IsDBNull(row("PotonganCong")), "0", CDec(row("PotonganCong")).ToString())
            txtTotalPotongan.Text = If(IsDBNull(row("TotalPotongan")), "0", CDec(row("TotalPotongan")).ToString("N0"))
            txtBeratBersih.Text = If(IsDBNull(row("BeratBersih")), "0", CDec(row("BeratBersih")).ToString("N0"))

            ' === FFA ===
            chkIncludeFFA.Checked = If(IsDBNull(row("IncludeFFA")), False, CBool(row("IncludeFFA")))
            If chkIncludeFFA.Checked Then
                txtFFA.Text = If(IsDBNull(row("FFA")), "0", CDec(row("FFA")).ToString("N2"))
                txtMoisture.Text = If(IsDBNull(row("Moisture")), "0", CDec(row("Moisture")).ToString("N2"))
                txtDirt.Text = If(IsDBNull(row("Dirt")), "0", CDec(row("Dirt")).ToString("N2"))
                If txtSuhuMinyak IsNot Nothing Then
                    txtSuhuMinyak.Text = If(IsDBNull(row("SuhuMinyak")), "0", CDec(row("SuhuMinyak")).ToString("N1"))
                End If
            End If

            ' === KETERANGAN ===
            txtKeterangan.Text = If(IsDBNull(row("Keterangan")), "", row("Keterangan").ToString())

            ' Setup FFA controls
            SetFFAControlsEnabled(chkIncludeFFA.Checked)

        Catch ex As Exception
            Debug.WriteLine("[FormEditTimbangan.LoadData] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat data timbangan.")
        End Try
    End Sub

    ' =============================================
    ' SIMPAN DATA ORIGINAL (UNTUK TRACKING PERUBAHAN)
    ' =============================================
    Private Sub SaveOriginalData(row As DataRow)
        _originalData.Clear()
        _originalData("NoPolisi") = row("NoPolisi").ToString()
        _originalData("NamaSupir") = If(IsDBNull(row("NamaSupir")), "", row("NamaSupir").ToString())
        _originalData("TransporterNama") = If(IsDBNull(row("TransporterNama")), "", row("TransporterNama").ToString())
        _originalData("CustomerNama") = If(IsDBNull(row("CustomerNama")), "", row("CustomerNama").ToString())
        _originalData("ProductNama") = If(IsDBNull(row("ProductNama")), "", row("ProductNama").ToString())
        _originalData("NoDO") = If(IsDBNull(row("NoDO")), "", row("NoDO").ToString())
        _originalData("NoKontrak") = If(IsDBNull(row("NoKontrak")), "", row("NoKontrak").ToString())
        ' === NO SEGEL (BARU) ===
        _originalData("SegelAtas") = If(IsDBNull(row("SegelAtas")), "", row("SegelAtas").ToString())
        _originalData("SegelBawah") = If(IsDBNull(row("SegelBawah")), "", row("SegelBawah").ToString())

        _originalData("BeratMasuk") = If(IsDBNull(row("BeratMasuk")), "0", CDec(row("BeratMasuk")).ToString("N0"))
        _originalData("BeratKeluar") = If(IsDBNull(row("BeratKeluar")), "0", CDec(row("BeratKeluar")).ToString("N0"))
        _originalData("BeratNetto") = If(IsDBNull(row("BeratNetto")), "0", CDec(row("BeratNetto")).ToString("N0"))
        _originalData("PotonganPersen") = If(IsDBNull(row("PotonganPersen")), "0", row("PotonganPersen").ToString())
        _originalData("PotonganCong") = If(IsDBNull(row("PotonganCong")), "0", row("PotonganCong").ToString())
        _originalData("TotalPotongan") = If(IsDBNull(row("TotalPotongan")), "0", CDec(row("TotalPotongan")).ToString("N0"))
        _originalData("BeratBersih") = If(IsDBNull(row("BeratBersih")), "0", CDec(row("BeratBersih")).ToString("N0"))
        _originalData("IncludeFFA") = If(IsDBNull(row("IncludeFFA")), "False", row("IncludeFFA").ToString())
        _originalData("FFA") = If(IsDBNull(row("FFA")), "0", row("FFA").ToString())
        _originalData("Moisture") = If(IsDBNull(row("Moisture")), "0", row("Moisture").ToString())
        _originalData("Dirt") = If(IsDBNull(row("Dirt")), "0", row("Dirt").ToString())
        _originalData("SuhuMinyak") = If(IsDBNull(row("SuhuMinyak")), "0", row("SuhuMinyak").ToString())
        _originalData("Keterangan") = If(IsDBNull(row("Keterangan")), "", row("Keterangan").ToString())
    End Sub

    ' =============================================
    ' HITUNG NETTO OTOMATIS
    ' =============================================
    Private Sub HitungNetto(sender As Object, e As EventArgs)
        Try
            Dim bruto As Decimal = 0
            Dim tara As Decimal = 0
            Dim potonganPersen As Decimal = 0
            Dim potonganKg As Decimal = 0

            Decimal.TryParse(txtBeratBruto.Text.Replace(".", "").Replace(",", ""), bruto)
            Decimal.TryParse(txtBeratTara.Text.Replace(".", "").Replace(",", ""), tara)
            Decimal.TryParse(txtPotonganPersen.Text, potonganPersen)
            Decimal.TryParse(txtPotonganKg.Text, potonganKg)

            ' Hitung Netto
            Dim netto As Decimal = Math.Abs(bruto - tara)
            txtBeratNetto.Text = netto.ToString("N0")

            ' Hitung Potongan
            Dim totalPotongan As Decimal = Math.Round((netto * potonganPersen / 100) + potonganKg, 0)
            txtTotalPotongan.Text = totalPotongan.ToString("N0")

            ' Hitung Berat Bersih
            Dim beratBersih As Decimal = netto - totalPotongan
            txtBeratBersih.Text = beratBersih.ToString("N0")

        Catch ex As Exception
            Debug.WriteLine("[FormEditTimbangan] Recalculation error: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' CHECKBOX FFA
    ' =============================================
    Private Sub chkIncludeFFA_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeFFA.CheckedChanged
        SetFFAControlsEnabled(chkIncludeFFA.Checked)
    End Sub

    Private Sub SetFFAControlsEnabled(enabled As Boolean)
        txtFFA.Enabled = enabled
        txtMoisture.Enabled = enabled
        txtDirt.Enabled = enabled
        If txtSuhuMinyak IsNot Nothing Then txtSuhuMinyak.Enabled = enabled

        Dim bgColor As Color = If(enabled, Color.White, Color.LightGray)
        txtFFA.BackColor = bgColor
        txtMoisture.BackColor = bgColor
        txtDirt.BackColor = bgColor
        If txtSuhuMinyak IsNot Nothing Then txtSuhuMinyak.BackColor = bgColor

        If Not enabled Then
            txtFFA.Text = "0"
            txtMoisture.Text = "0"
            txtDirt.Text = "0"
            If txtSuhuMinyak IsNot Nothing Then txtSuhuMinyak.Text = "0"
        End If
    End Sub

    ' =============================================
    ' TOMBOL SIMPAN
    ' =============================================
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            ' Validasi alasan edit
            If String.IsNullOrWhiteSpace(txtAlasanEdit.Text) Then
                MessageBox.Show("⚠️ ALASAN EDIT (wajib di isi!)", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtAlasanEdit.Focus()
                Return
            End If

            If txtAlasanEdit.Text.Trim().Length < 10 Then
                MessageBox.Show("Alasan edit minimal 10 karakter!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtAlasanEdit.Focus()
                Return
            End If

            ' Validasi No. Polisi
            If String.IsNullOrWhiteSpace(txtNoPolisi.Text) Then
                MessageBox.Show("No. Polisi harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNoPolisi.Focus()
                Return
            End If

            ' Deteksi perubahan
            Dim perubahan As String = DeteksiPerubahan()
            If String.IsNullOrEmpty(perubahan) Then
                MessageBox.Show("Tidak ada perubahan data!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Konfirmasi
            If MessageBox.Show("Simpan perubahan data?" & vbCrLf & vbCrLf &
                              "📝 PERUBAHAN :" & vbCrLf & perubahan & vbCrLf &
                              "📋 ALASAN : " & txtAlasanEdit.Text & vbCrLf & vbCrLf &
                              "⚠️ Akan tercatat di Audit Log",
                              "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            ' Simpan
            SimpanPerubahan(perubahan)

        Catch ex As Exception
            Debug.WriteLine("[FormEditTimbangan] Error: " & ex.ToString())
            MessageBox.Show("Operasi timbangan gagal.")
        End Try
    End Sub

    ' =============================================
    ' DETEKSI PERUBAHAN
    ' =============================================
    Private Function DeteksiPerubahan() As String
        Dim sb As New System.Text.StringBuilder()
        Dim ada As Boolean = False

        If _originalData("NoPolisi") <> txtNoPolisi.Text.Trim().ToUpperInvariant() Then
            sb.AppendLine($"• No.Polisi: {_originalData("NoPolisi")} → {txtNoPolisi.Text.ToUpperInvariant()}")
            ada = True
        End If
        If _originalData("NamaSupir") <> txtNamaSupir.Text.Trim() Then
            sb.AppendLine($"• Supir: {_originalData("NamaSupir")} → {txtNamaSupir.Text}")
            ada = True
        End If
        If _originalData("TransporterNama") <> cmbTransporter.Text.Trim() Then
            sb.AppendLine($"• Transporter: {_originalData("TransporterNama")} → {cmbTransporter.Text}")
            ada = True
        End If
        If _originalData("CustomerNama") <> cmbCustomer.Text.Trim() Then
            sb.AppendLine($"• Customer: {_originalData("CustomerNama")} → {cmbCustomer.Text}")
            ada = True
        End If
        If _originalData("ProductNama") <> cmbProduct.Text.Trim() Then
            sb.AppendLine($"• Product: {_originalData("ProductNama")} → {cmbProduct.Text}")
            ada = True
        End If
        If _originalData("NoDO") <> txtNoDO.Text.Trim() Then
            sb.AppendLine($"• NoDO: {_originalData("NoDO")} → {txtNoDO.Text}")
            ada = True
        End If
        If txtNoKontrak IsNot Nothing Then
            Dim noKontrakBaru As String = txtNoKontrak.Text.Trim()
            If _originalData("NoKontrak") <> noKontrakBaru Then
                sb.AppendLine($"• No.Kontrak: {_originalData("NoKontrak")} → {noKontrakBaru}")
                ada = True
            End If
        End If

        ' === CEK PERUBAHAN NO SEGEL ===
        If txtSegelAtas IsNot Nothing Then
            Dim segelAtasBaru As String = txtSegelAtas.Text.Trim()
            If _originalData("SegelAtas") <> segelAtasBaru Then
                sb.AppendLine($"• Segel Atas: {_originalData("SegelAtas")} → {segelAtasBaru}")
                ada = True
            End If
        End If

        If txtSegelBawah IsNot Nothing Then
            Dim segelBawahBaru As String = txtSegelBawah.Text.Trim()
            If _originalData("SegelBawah") <> segelBawahBaru Then
                sb.AppendLine($"• Segel Bawah: {_originalData("SegelBawah")} → {segelBawahBaru}")
                ada = True
            End If
        End If

        If _originalData("BeratMasuk") <> txtBeratBruto.Text.Replace(".", "").Replace(",", "") Then
            sb.AppendLine($"• BeratBruto: {_originalData("BeratMasuk")} → {txtBeratBruto.Text}")
            ada = True
        End If
        If _originalData("BeratKeluar") <> txtBeratTara.Text.Replace(".", "").Replace(",", "") Then
            sb.AppendLine($"• BeratTara: {_originalData("BeratKeluar")} → {txtBeratTara.Text}")
            ada = True
        End If
        If _originalData("PotonganPersen") <> txtPotonganPersen.Text Then
            sb.AppendLine($"• Potongan%: {_originalData("PotonganPersen")} → {txtPotonganPersen.Text}")
            ada = True
        End If
        If _originalData("PotonganCong") <> txtPotonganKg.Text Then
            sb.AppendLine($"• PotonganKG: {_originalData("PotonganCong")} → {txtPotonganKg.Text}")
            ada = True
        End If
        If _originalData("Keterangan") <> txtKeterangan.Text.Trim() Then
            sb.AppendLine($"• Keterangan: diubah")
            ada = True
        End If

        ' Cek FFA
        If chkIncludeFFA.Checked Then
            If _originalData("FFA") <> txtFFA.Text.Trim() Then
                sb.AppendLine($"• FFA: {_originalData("FFA")} → {txtFFA.Text}")
                ada = True
            End If
            If _originalData("Moisture") <> txtMoisture.Text.Trim() Then
                sb.AppendLine($"• Moisture: {_originalData("Moisture")} → {txtMoisture.Text}")
                ada = True
            End If
            If _originalData("Dirt") <> txtDirt.Text.Trim() Then
                sb.AppendLine($"• Dirt: {_originalData("Dirt")} → {txtDirt.Text}")
                ada = True
            End If
            If txtSuhuMinyak IsNot Nothing Then
                If _originalData("SuhuMinyak") <> txtSuhuMinyak.Text.Trim() Then
                    sb.AppendLine($"• Suhu Minyak: {_originalData("SuhuMinyak")} → {txtSuhuMinyak.Text}")
                    ada = True
                End If
            End If
        End If

        Return If(ada, sb.ToString(), "")
    End Function

    ' =============================================
    ' SIMPAN PERUBAHAN
    ' =============================================
    Private Sub SimpanPerubahan(perubahan As String)
        Try
            Dim beratBruto, beratTara, beratNetto As Decimal
            Dim potonganPersen, potonganKg, totalPotongan, beratBersih As Decimal

            Decimal.TryParse(txtBeratBruto.Text.Replace(".", "").Replace(",", ""), beratBruto)
            Decimal.TryParse(txtBeratTara.Text.Replace(".", "").Replace(",", ""), beratTara)
            Decimal.TryParse(txtBeratNetto.Text.Replace(".", "").Replace(",", ""), beratNetto)
            Decimal.TryParse(txtPotonganPersen.Text, potonganPersen)
            Decimal.TryParse(txtPotonganKg.Text, potonganKg)
            Decimal.TryParse(txtTotalPotongan.Text.Replace(".", "").Replace(",", ""), totalPotongan)
            Decimal.TryParse(txtBeratBersih.Text.Replace(".", "").Replace(",", ""), beratBersih)

            ' FFA
            Dim ffa, moisture, dirt, suhuMinyak As Decimal
            If chkIncludeFFA.Checked Then
                Decimal.TryParse(txtFFA.Text, ffa)
                Decimal.TryParse(txtMoisture.Text, moisture)
                Decimal.TryParse(txtDirt.Text, dirt)
                If txtSuhuMinyak IsNot Nothing Then
                    Decimal.TryParse(txtSuhuMinyak.Text, suhuMinyak)
                End If
            End If

            Dim noKontrak As String = ""
            If txtNoKontrak IsNot Nothing Then noKontrak = txtNoKontrak.Text.Trim()
            ' Ambil nilai Segel
            Dim segelAtas As String = ""
            Dim segelBawah As String = ""
            If txtSegelAtas IsNot Nothing Then segelAtas = txtSegelAtas.Text.Trim()
            If txtSegelBawah IsNot Nothing Then segelBawah = txtSegelBawah.Text.Trim()

            Dim query As String = "UPDATE Timbangan SET " &
                                "NoPolisi=@NoPolisi, NamaSupir=@NamaSupir, " &
                                "TransporterID=@TransporterID, TransporterNama=@TransporterNama, " &
                                "CustomerID=@CustomerID, CustomerNama=@CustomerNama, " &
                                "ProductID=@ProductID, ProductNama=@ProductNama, " &
                                "NoDO=@NoDO, NoKontrak=@NoKontrak, " &
                                "SegelAtas=@SegelAtas, SegelBawah=@SegelBawah, " &
                                "BeratMasuk=@BeratMasuk, BeratKeluar=@BeratKeluar, " &
                                "BeratNetto=@BeratNetto, PotonganPersen=@PotonganPersen, " &
                                "PotonganCong=@PotonganKg, TotalPotongan=@TotalPotongan, " &
                                "BeratBersih=@BeratBersih, " &
                                "IncludeFFA=@IncludeFFA, FFA=@FFA, " &
                                "Moisture=@Moisture, " &
                                "Dirt=@Dirt, " &
                                "SuhuMinyak=@SuhuMinyak, " &
                                "Keterangan=@Keterangan, " &
                                "UpdatedAt=GETDATE(), UpdatedBy=@UpdatedBy WHERE TimbangID=@ID"

            Dim transporterID As Object = If(cmbTransporter.SelectedValue IsNot Nothing, cmbTransporter.SelectedValue, DBNull.Value)
            Dim customerID As Object = If(cmbCustomer.SelectedValue IsNot Nothing, cmbCustomer.SelectedValue, DBNull.Value)
            Dim productID As Object = If(cmbProduct.SelectedValue IsNot Nothing, cmbProduct.SelectedValue, DBNull.Value)

            Dim params As SqlParameter() = {
                New SqlParameter("@NoPolisi", txtNoPolisi.Text.Trim().ToUpperInvariant()),
                New SqlParameter("@NamaSupir", txtNamaSupir.Text.Trim()),
                New SqlParameter("@TransporterID", transporterID),
                New SqlParameter("@TransporterNama", cmbTransporter.Text.Trim()),
                New SqlParameter("@CustomerID", customerID),
                New SqlParameter("@CustomerNama", cmbCustomer.Text.Trim()),
                New SqlParameter("@ProductID", productID),
                New SqlParameter("@ProductNama", cmbProduct.Text.Trim()),
                New SqlParameter("@NoDO", txtNoDO.Text.Trim()),
                New SqlParameter("@NoKontrak", noKontrak),
                New SqlParameter("@SegelAtas", segelAtas),
                New SqlParameter("@SegelBawah", segelBawah),
                New SqlParameter("@BeratMasuk", beratBruto),
                New SqlParameter("@BeratKeluar", If(beratTara > 0, beratTara, DBNull.Value)),
                New SqlParameter("@BeratNetto", If(beratNetto > 0, beratNetto, DBNull.Value)),
                New SqlParameter("@PotonganPersen", potonganPersen),
                New SqlParameter("@PotonganKg", potonganKg),
                New SqlParameter("@TotalPotongan", If(totalPotongan > 0, totalPotongan, DBNull.Value)),
                New SqlParameter("@BeratBersih", If(beratBersih > 0, beratBersih, DBNull.Value)),
                New SqlParameter("@IncludeFFA", chkIncludeFFA.Checked),
                New SqlParameter("@FFA", ffa),
                New SqlParameter("@Moisture", moisture),
                New SqlParameter("@Dirt", dirt),
                New SqlParameter("@SuhuMinyak", suhuMinyak),
                New SqlParameter("@Keterangan", txtKeterangan.Text.Trim()),
                New SqlParameter("@UpdatedBy", UserSession.UserID),
                New SqlParameter("@ID", _timbangID)
            }

            Dim rows As Integer = DatabaseHelper.ExecuteNonQuery(query, params)

            If rows > 0 Then
                ' AUDIT LOG
                Dim dataBefore As String = GenerateDataBefore()
                Dim dataAfter As String = GenerateDataAfter()
                Dim keterangan As String = $"EDIT TIMBANGAN | No Tiket : {_noTiket} | " &
                                          $"Oleh : {UserSession.NamaLengkap} ({UserSession.RoleName}) | " &
                                          $"Alasan Edit : {txtAlasanEdit.Text.Trim()}"

                DatabaseHelper.InsertAuditLog(UserSession.UserID, "Edit Timbangan", _timbangID,
                    "Timbangan", _timbangID, dataBefore, dataAfter, keterangan)

                MessageBox.Show("✅ Data berhasil diupdate!" & vbCrLf & "Tercatat di Audit Log.",
                               "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("Gagal Update!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Debug.WriteLine("[FormEditTimbangan] Error: " & ex.ToString())
            MessageBox.Show("Operasi timbangan gagal.")
        End Try
    End Sub

    Private Function GenerateDataBefore() As String
        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("No Polisi: " & _originalData("NoPolisi"))
        sb.AppendLine("Nama Supir: " & _originalData("NamaSupir"))
        sb.AppendLine("Transporter: " & _originalData("TransporterNama"))
        sb.AppendLine("Customer: " & _originalData("CustomerNama"))
        sb.AppendLine("Product: " & _originalData("ProductNama"))
        sb.AppendLine("No DO: " & _originalData("NoDO"))
        sb.AppendLine("No Kontrak: " & _originalData("NoKontrak"))
        sb.AppendLine("Segel Atas: " & _originalData("SegelAtas"))
        sb.AppendLine("Segel Bawah: " & _originalData("SegelBawah"))
        sb.AppendLine("Berat Bruto: " & _originalData("BeratMasuk"))
        sb.AppendLine("Berat Tara: " & _originalData("BeratKeluar"))
        sb.AppendLine("Berat Netto: " & _originalData("BeratNetto"))
        sb.AppendLine("Potongan Persen: " & _originalData("PotonganPersen"))
        sb.AppendLine("Potongan Kg: " & _originalData("PotonganCong"))
        sb.AppendLine("Total Potongan: " & _originalData("TotalPotongan"))
        sb.AppendLine("Berat Bersih: " & _originalData("BeratBersih"))
        sb.AppendLine("Include FFA: " & _originalData("IncludeFFA"))
        sb.AppendLine("FFA: " & _originalData("FFA"))
        sb.AppendLine("Moisture: " & _originalData("Moisture"))
        sb.AppendLine("Dirt: " & _originalData("Dirt"))
        sb.AppendLine("Suhu Minyak: " & _originalData("SuhuMinyak"))
        sb.AppendLine("Keterangan: " & _originalData("Keterangan"))
        Return sb.ToString()
    End Function

    Private Function GenerateDataAfter() As String
        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("No Polisi: " & txtNoPolisi.Text.Trim().ToUpperInvariant())
        sb.AppendLine("Nama Supir: " & txtNamaSupir.Text.Trim())
        sb.AppendLine("Transporter: " & cmbTransporter.Text.Trim())
        sb.AppendLine("Customer: " & cmbCustomer.Text.Trim())
        sb.AppendLine("Product: " & cmbProduct.Text.Trim())
        sb.AppendLine("No DO: " & txtNoDO.Text.Trim())
        sb.AppendLine("No Kontrak: " & If(txtNoKontrak IsNot Nothing, txtNoKontrak.Text.Trim(), ""))
        sb.AppendLine("Segel Atas: " & If(txtSegelAtas IsNot Nothing, txtSegelAtas.Text.Trim(), ""))
        sb.AppendLine("Segel Bawah: " & If(txtSegelBawah IsNot Nothing, txtSegelBawah.Text.Trim(), ""))
        sb.AppendLine("Berat Bruto: " & txtBeratBruto.Text.Replace(".", "").Replace(",", ""))
        sb.AppendLine("Berat Tara: " & txtBeratTara.Text.Replace(".", "").Replace(",", ""))
        sb.AppendLine("Berat Netto: " & txtBeratNetto.Text.Replace(".", "").Replace(",", ""))
        sb.AppendLine("Potongan Persen: " & txtPotonganPersen.Text)
        sb.AppendLine("Potongan Kg: " & txtPotonganKg.Text)
        sb.AppendLine("Total Potongan: " & txtTotalPotongan.Text.Replace(".", "").Replace(",", ""))
        sb.AppendLine("Berat Bersih: " & txtBeratBersih.Text.Replace(".", "").Replace(",", ""))
        sb.AppendLine("Include FFA: " & chkIncludeFFA.Checked.ToString())
        sb.AppendLine("FFA: " & txtFFA.Text.Trim())
        sb.AppendLine("Moisture: " & txtMoisture.Text.Trim())
        sb.AppendLine("Dirt: " & txtDirt.Text.Trim())
        sb.AppendLine("Suhu Minyak: " & If(txtSuhuMinyak IsNot Nothing, txtSuhuMinyak.Text.Trim(), "0"))
        sb.AppendLine("Keterangan: " & txtKeterangan.Text.Trim())
        Return sb.ToString()
    End Function

    ' =============================================
    ' TOMBOL CETAK
    ' =============================================
    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Try
            If _status.ToUpperInvariant() = "SELESAI" Then
                PrintHelper.CetakDariDatabase(_timbangID, True)
            Else
                MessageBox.Show("Hanya status SELESAI yang bisa dicetak!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormEditTimbangan.Print] Error: " & ex.ToString())
            MessageBox.Show("Gagal mencetak dokumen.")
        End Try
    End Sub

    ' =============================================
    ' TOMBOL BATAL
    ' =============================================
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        If MessageBox.Show("Batalkan perubahan?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub
End Class
