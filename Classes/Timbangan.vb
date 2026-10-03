' =============================================
' Timbangan.vb
' Class untuk mengelola data timbangan
' =============================================
Imports System.Data.SqlClient

Public Class Timbangan

    ' =============================================
    ' PROPERTIES
    ' =============================================
    Public Property TimbangID As Integer

    <Obsolete("Use TimbangID instead.")>
    Public Property TimbanganID As Integer
        Get
            Return TimbangID
        End Get
        Set(value As Integer)
            TimbangID = value
        End Set
    End Property
    Public Property NoTiket As String
    Public Property NoPolisi As String
    Public Property NamaSupir As String

    ' Customer
    Public Property CustomerID As Integer?
    Public Property CustomerNama As String

    ' Transporter
    Public Property TransporterID As Integer?
    Public Property TransporterNama As String

    ' Product
    Public Property ProductID As Integer?
    Public Property ProductNama As String

    ' Kontrak / DO
    Public Property NoKontrak As String
    Public Property NoDO As String

    ' Berat
    Public Property BeratMasuk As Decimal
    Public Property BeratKeluar As Decimal
    Public Property BeratNetto As Decimal
    Public Property BeratBersih As Decimal

    ' Potongan
    Public Property Potongan As Decimal
    Public Property TotalPotongan As Decimal

    ' Waktu
    Public Property TanggalMasuk As DateTime?
    Public Property TanggalKeluar As DateTime?

    ' Status: MASUK, KELUAR, SELESAI, BATAL
    Public Property Status As String

    ' Petugas
    Public Property PetugasMasukID As Integer?
    Public Property PetugasKeluarID As Integer?

    ' Keterangan
    Public Property Keterangan As String

    ' =============================================
    ' CONSTRUCTOR
    ' =============================================
    Public Sub New()
        ' Default values
        Me.Status = "MASUK"
        Me.BeratMasuk = 0
        Me.BeratKeluar = 0
        Me.BeratNetto = 0
        Me.Potongan = 0
        Me.TotalPotongan = 0
        Me.BeratBersih = 0
    End Sub

    ' =============================================
    ' GENERATE NOMOR TIKET OTOMATIS
    ' =============================================
    Public Shared Function GenerateNoTiket() As String
        Try
            ' Ambil prefix dari Settings
            Dim prefix As String = GetSetting("TIKET_PREFIX", "CPO")
            Dim jumlahDigit As Integer = CInt(GetSetting("JumlahDigit", "6"))
            Dim resetHarian As Boolean = (GetSetting("ResetHarian", "1") = "1")

            ' Hitung nomor urut
            Dim nomorUrut As Integer = 1
            Dim query As String

            If resetHarian Then
                ' Reset setiap hari
                query = "SELECT COUNT(*) + 1 FROM Timbangan WHERE CAST(TanggalMasuk AS DATE) = CAST(GETDATE() AS DATE)"
            Else
                ' Tidak reset
                query = "SELECT COUNT(*) + 1 FROM Timbangan"
            End If

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query)
            If dt.Rows.Count > 0 Then
                nomorUrut = CInt(dt.Rows(0)(0))
            End If

            ' Format: PREFIX-YYMMDD-XXXXXX
            Dim tanggal As String = DateTime.Now.ToString("yyMMdd")
            Dim nomor As String = nomorUrut.ToString().PadLeft(jumlahDigit, "0"c)

            Return prefix & "-" & tanggal & "-" & nomor

        Catch ex As Exception
            ' Fallback jika error
            Return "TKT-" & DateTime.Now.ToString("yyMMddHHmmss")
        End Try
    End Function

    ' =============================================
    ' AMBIL SETTING DARI DATABASE
    ' =============================================
    Private Shared Function GetSetting(key As String, defaultValue As String) As String
        Try
            Dim query As String = "SELECT SettingValue FROM Settings WHERE SettingKey = @Key"
            Dim params As SqlParameter() = {New SqlParameter("@Key", key)}
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, params)

            If dt.Rows.Count > 0 AndAlso dt.Rows(0)(0) IsNot DBNull.Value Then
                Return dt.Rows(0)(0).ToString()
            End If

            Return defaultValue
        Catch ex As Exception
            Debug.WriteLine("[Timbangan.GetSetting] Error: " & ex.Message)
            Return defaultValue
        End Try
    End Function

    ' =============================================
    ' SIMPAN DATA TIMBANGAN MASUK (BRUTO)
    ' =============================================
    Public Function SimpanMasuk() As Boolean
        Try
            ' Generate nomor tiket
            Me.NoTiket = GenerateNoTiket()
            Me.TanggalMasuk = DateTime.Now
            Me.Status = "MASUK"
            Me.PetugasMasukID = UserSession.UserID

            Dim query As String = "INSERT INTO Timbangan " &
                "(NoTiket, NoPolisi, NamaSupir, CustomerID, CustomerNama, TransporterID, TransporterNama, " &
                "ProductID, ProductNama, NoKontrak, NoDO, BeratMasuk, TanggalMasuk, Status, PetugasMasukID, Keterangan) " &
                "VALUES " &
                "(@NoTiket, @NoPolisi, @NamaSupir, @CustomerID, @CustomerNama, @TransporterID, @TransporterNama, " &
                "@ProductID, @ProductNama, @NoKontrak, @NoDO, @BeratMasuk, @TanggalMasuk, @Status, @PetugasMasukID, @Keterangan); " &
                "SELECT SCOPE_IDENTITY();"

            Dim params As SqlParameter() = {
                New SqlParameter("@NoTiket", Me.NoTiket),
                New SqlParameter("@NoPolisi", If(Me.NoPolisi, "")),
                New SqlParameter("@NamaSupir", If(Me.NamaSupir, "")),
                New SqlParameter("@CustomerID", If(Me.CustomerID, DBNull.Value)),
                New SqlParameter("@CustomerNama", If(Me.CustomerNama, "")),
                New SqlParameter("@TransporterID", If(Me.TransporterID, DBNull.Value)),
                New SqlParameter("@TransporterNama", If(Me.TransporterNama, "")),
                New SqlParameter("@ProductID", If(Me.ProductID, DBNull.Value)),
                New SqlParameter("@ProductNama", If(Me.ProductNama, "")),
                New SqlParameter("@NoKontrak", If(Me.NoKontrak, "")),
                New SqlParameter("@NoDO", If(Me.NoDO, "")),
                New SqlParameter("@BeratMasuk", Me.BeratMasuk),
                New SqlParameter("@TanggalMasuk", Me.TanggalMasuk),
                New SqlParameter("@Status", Me.Status),
                New SqlParameter("@PetugasMasukID", If(Me.PetugasMasukID, DBNull.Value)),
                New SqlParameter("@Keterangan", If(Me.Keterangan, ""))
            }

            Dim result As Object = DatabaseHelper.ExecuteScalar(query, params)
            If result IsNot Nothing Then
                Me.TimbangID = CInt(result)
                Return True
            End If

            Return False

        Catch ex As Exception
            Debug.WriteLine("[Timbangan.SimpanMasuk] Error: " & ex.ToString())
            Throw New Exception("Gagal menyimpan data masuk.", ex)
        End Try
    End Function

    ' =============================================
    ' SIMPAN DATA TIMBANGAN KELUAR (TARRA) & HITUNG NETTO
    ' =============================================
    Public Function SimpanKeluar() As Boolean
        Try
            Me.TanggalKeluar = DateTime.Now
            Me.Status = "SELESAI"
            Me.PetugasKeluarID = UserSession.UserID

            ' Hitung Netto = Bruto - Tarra
            Me.BeratNetto = Math.Abs(Me.BeratMasuk - Me.BeratKeluar)

            ' Hitung Berat Bersih = Netto - Potongan
            Me.BeratBersih = Me.BeratNetto - Me.TotalPotongan

            Dim query As String = "UPDATE Timbangan SET " &
                "BeratKeluar = @BeratKeluar, " &
                "BeratNetto = @BeratNetto, " &
                "Potongan = @Potongan, " &
                "TotalPotongan = @TotalPotongan, " &
                "BeratBersih = @BeratBersih, " &
                "TanggalKeluar = @TanggalKeluar, " &
                "Status = @Status, " &
                "PetugasKeluarID = @PetugasKeluarID " &
                "WHERE TimbangID = @TimbangID"

            Dim params As SqlParameter() = {
                New SqlParameter("@BeratKeluar", Me.BeratKeluar),
                New SqlParameter("@BeratNetto", Me.BeratNetto),
                New SqlParameter("@Potongan", Me.Potongan),
                New SqlParameter("@TotalPotongan", Me.TotalPotongan),
                New SqlParameter("@BeratBersih", Me.BeratBersih),
                New SqlParameter("@TanggalKeluar", Me.TanggalKeluar),
                New SqlParameter("@Status", Me.Status),
                New SqlParameter("@PetugasKeluarID", If(Me.PetugasKeluarID, DBNull.Value)),
                New SqlParameter("@TimbangID", Me.TimbangID)
            }

            Dim rowsAffected As Integer = DatabaseHelper.ExecuteNonQuery(query, params)
            Return rowsAffected > 0

        Catch ex As Exception
            Debug.WriteLine("[Timbangan.SimpanKeluar] Error: " & ex.ToString())
            Throw New Exception("Gagal menyimpan data keluar.", ex)
        End Try
    End Function

    ' =============================================
    ' AMBIL DATA TIMBANGAN BERDASARKAN ID
    ' =============================================
    Public Shared Function GetByID(id As Integer) As Timbangan
        Try
            Dim query As String = "SELECT * FROM Timbangan WHERE TimbangID = @ID"
            Dim params As SqlParameter() = {New SqlParameter("@ID", id)}
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, params)

            If dt.Rows.Count > 0 Then
                Return MapFromDataRow(dt.Rows(0))
            End If

            Return Nothing

        Catch ex As Exception
            Debug.WriteLine("[Timbangan] Data retrieval error: " & ex.ToString())
            Throw New Exception("Gagal mengambil data timbangan.", ex)
        End Try
    End Function

    ' =============================================
    ' AMBIL DATA TIMBANGAN BERDASARKAN NO TIKET
    ' =============================================
    Public Shared Function GetByNoTiket(noTiket As String) As Timbangan
        Try
            Dim query As String = "SELECT * FROM Timbangan WHERE NoTiket = @NoTiket"
            Dim params As SqlParameter() = {New SqlParameter("@NoTiket", noTiket)}
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, params)

            If dt.Rows.Count > 0 Then
                Return MapFromDataRow(dt.Rows(0))
            End If

            Return Nothing

        Catch ex As Exception
            Debug.WriteLine("[Timbangan.GetByNoTiket] Error: " & ex.ToString())
            Throw New Exception("Gagal mengambil data timbangan.", ex)
        End Try
    End Function

    ' =============================================
    ' AMBIL DATA TIMBANGAN BERDASARKAN NO POLISI (STATUS = MASUK)
    ' =============================================
    Public Shared Function GetByNoPolisiMasuk(noPolisi As String) As Timbangan
        Try
            Dim query As String = "SELECT * FROM Timbangan WHERE NoPolisi = @NoPolisi AND Status = 'MASUK' ORDER BY TanggalMasuk DESC"
            Dim params As SqlParameter() = {New SqlParameter("@NoPolisi", noPolisi)}
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, params)

            If dt.Rows.Count > 0 Then
                Return MapFromDataRow(dt.Rows(0))
            End If

            Return Nothing

        Catch ex As Exception
            Debug.WriteLine("[Timbangan.GetByNoPolisiMasuk] Error: " & ex.ToString())
            Throw New Exception("Gagal mengambil data timbangan.", ex)
        End Try
    End Function

    ' =============================================
    ' AMBIL SEMUA DATA TIMBANGAN HARI INI
    ' =============================================
    Public Shared Function GetTodayTransactions() As DataTable
        Try
            Dim query As String = "SELECT * FROM Timbangan WHERE CAST(TanggalMasuk AS DATE) = CAST(GETDATE() AS DATE) ORDER BY TanggalMasuk DESC"
            Return DatabaseHelper.ExecuteQuery(query)
        Catch ex As Exception
            Debug.WriteLine("[Timbangan.GetTodayTransactions] Error: " & ex.ToString())
            Throw New Exception("Gagal mengambil transaksi hari ini.", ex)
        End Try
    End Function

    ' =============================================
    ' AMBIL DAFTAR KENDARAAN YANG BELUM KELUAR
    ' =============================================
    Public Shared Function GetKendaraanBelumKeluar() As DataTable
        Try
            Dim query As String = "SELECT TimbangID, NoTiket, NoPolisi, NamaSupir, CustomerNama, ProductNama, " &
                                 "BeratMasuk, TanggalMasuk FROM Timbangan " &
                                 "WHERE Status = 'MASUK' ORDER BY TanggalMasuk ASC"
            Return DatabaseHelper.ExecuteQuery(query)
        Catch ex As Exception
            Debug.WriteLine("[Timbangan.GetKendaraanBelumKeluar] Error: " & ex.ToString())
            Throw New Exception("Gagal mengambil antrian kendaraan.", ex)
        End Try
    End Function

    ' =============================================
    ' BATALKAN TRANSAKSI
    ' =============================================
    Public Function Batalkan(alasan As String) As Boolean
        Try
            Dim query As String = "UPDATE Timbangan SET Status = 'BATAL', Keterangan = @Keterangan WHERE TimbangID = @ID"
            Dim params As SqlParameter() = {
                New SqlParameter("@Keterangan", "DIBATALKAN: " & alasan),
                New SqlParameter("@ID", Me.TimbangID)
            }

            Dim rowsAffected As Integer = DatabaseHelper.ExecuteNonQuery(query, params)
            Return rowsAffected > 0

        Catch ex As Exception
            Debug.WriteLine("[Timbangan.Batalkan] Error: " & ex.ToString())
            Throw New Exception("Gagal membatalkan transaksi.", ex)
        End Try
    End Function

    ' =============================================
    ' MAPPING DARI DATAROW KE OBJECT
    ' =============================================
    Private Shared Function MapFromDataRow(row As DataRow) As Timbangan
        Dim t As New Timbangan()

        t.TimbangID = CInt(row("TimbangID"))
        t.NoTiket = row("NoTiket").ToString()
        t.NoPolisi = row("NoPolisi").ToString()
        t.NamaSupir = If(row("NamaSupir") IsNot DBNull.Value, row("NamaSupir").ToString(), "")

        t.CustomerID = If(row("CustomerID") IsNot DBNull.Value, CInt(row("CustomerID")), Nothing)
        t.CustomerNama = If(row("CustomerNama") IsNot DBNull.Value, row("CustomerNama").ToString(), "")

        t.TransporterID = If(row("TransporterID") IsNot DBNull.Value, CInt(row("TransporterID")), Nothing)
        t.TransporterNama = If(row("TransporterNama") IsNot DBNull.Value, row("TransporterNama").ToString(), "")

        t.ProductID = If(row("ProductID") IsNot DBNull.Value, CInt(row("ProductID")), Nothing)
        t.ProductNama = If(row("ProductNama") IsNot DBNull.Value, row("ProductNama").ToString(), "")

        t.NoKontrak = If(row("NoKontrak") IsNot DBNull.Value, row("NoKontrak").ToString(), "")
        t.NoDO = If(row("NoDO") IsNot DBNull.Value, row("NoDO").ToString(), "")

        t.BeratMasuk = If(row("BeratMasuk") IsNot DBNull.Value, CDec(row("BeratMasuk")), 0)
        t.BeratKeluar = If(row("BeratKeluar") IsNot DBNull.Value, CDec(row("BeratKeluar")), 0)
        t.BeratNetto = If(row("BeratNetto") IsNot DBNull.Value, CDec(row("BeratNetto")), 0)
        t.Potongan = If(row("Potongan") IsNot DBNull.Value, CDec(row("Potongan")), 0)
        t.TotalPotongan = If(row("TotalPotongan") IsNot DBNull.Value, CDec(row("TotalPotongan")), 0)
        t.BeratBersih = If(row("BeratBersih") IsNot DBNull.Value, CDec(row("BeratBersih")), 0)

        t.TanggalMasuk = If(row("TanggalMasuk") IsNot DBNull.Value, CDate(row("TanggalMasuk")), Nothing)
        t.TanggalKeluar = If(row("TanggalKeluar") IsNot DBNull.Value, CDate(row("TanggalKeluar")), Nothing)

        t.Status = If(row("Status") IsNot DBNull.Value, row("Status").ToString(), "")
        t.Keterangan = If(row("Keterangan") IsNot DBNull.Value, row("Keterangan").ToString(), "")

        Return t
    End Function

End Class

