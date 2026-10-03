' =============================================
' DatabaseMigration.vb
' Otomatis update struktur database saat aplikasi start
' TIDAK pakai sp_rename - aman dari duplicate error
' =============================================
Imports System.Data.SqlClient

Public Class DatabaseMigration

    Public Shared Sub RunMigrations()
        Try
            Debug.WriteLine("[Migration] Mulai...")

            EnsureUsersColumns()
            EnsureTimbanganColumns()
            EnsureProductsTable()
            EnsureCustomersTable()
            EnsureTransportersTable()
            EnsureKodeProdukColumn()
            InsertDefaultProducts()
            EnsureUniqueIndexNoDO()

            Debug.WriteLine("[Migration] Selesai.")
        Catch ex As Exception
            Debug.WriteLine("[Migration] Error utama: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' HELPER: CEK TABEL ADA
    ' =============================================
    Private Shared Function TableExists(tableName As String) As Boolean
        Try
            Dim result As Object = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @T",
                {New SqlParameter("@T", tableName)})
            Return result IsNot Nothing AndAlso CInt(result) > 0
        Catch ex As Exception
            Debug.WriteLine("[Migration] TableExists error: " & ex.Message)
            Return False
        End Try
    End Function

    ' =============================================
    ' HELPER: CEK KOLOM ADA
    ' =============================================
    Private Shared Function ColumnExists(tableName As String, columnName As String) As Boolean
        Try
            Dim result As Object = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @T AND COLUMN_NAME = @C",
                {New SqlParameter("@T", tableName),
                 New SqlParameter("@C", columnName)})
            Return result IsNot Nothing AndAlso CInt(result) > 0
        Catch ex As Exception
            Debug.WriteLine("[Migration] ColumnExists error: " & ex.Message)
            Return False
        End Try
    End Function

    ' =============================================
    ' COMPATIBILITY: USERS
    ' =============================================
    Private Shared Sub EnsureUsersColumns()
        Try
            If Not TableExists("Users") Then Return

            If Not ColumnExists("Users", "CreatedBy") Then
                DatabaseHelper.ExecuteNonQuery("ALTER TABLE Users ADD CreatedBy INT NULL")
            End If
            If Not ColumnExists("Users", "UpdatedBy") Then
                DatabaseHelper.ExecuteNonQuery("ALTER TABLE Users ADD UpdatedBy INT NULL")
            End If
        Catch ex As Exception
            Debug.WriteLine("[Migration] EnsureUsersColumns error: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' COMPATIBILITY: TIMBANGAN
    ' =============================================
    Private Shared Sub EnsureTimbanganColumns()
        Try
            If Not TableExists("Timbangan") Then Return

            Dim nullableColumns As String() = {
                "NoSIM NVARCHAR(50) NULL",
                "TransporterNama NVARCHAR(100) NULL",
                "CustomerNama NVARCHAR(100) NULL",
                "Alamat NVARCHAR(255) NULL",
                "ProductNama NVARCHAR(100) NULL",
                "TransType NVARCHAR(10) NULL",
                "NoKontrak NVARCHAR(50) NULL",
                "SegelAtas NVARCHAR(50) NULL",
                "SegelBawah NVARCHAR(50) NULL",
                "PotonganPersen DECIMAL(8,3) NULL",
                "PotonganCong DECIMAL(18,2) NULL",
                "TotalPotongan DECIMAL(18,2) NULL",
                "KraniMasuk INT NULL",
                "KraniKeluar INT NULL",
                "PetugasMasukID INT NULL",
                "PetugasKeluarID INT NULL",
                "IncludeFFA BIT NULL",
                "FFA DECIMAL(10,3) NULL",
                "Moisture DECIMAL(10,3) NULL",
                "Dirt DECIMAL(10,3) NULL",
                "SuhuMinyak DECIMAL(10,2) NULL",
                "IncludeKeterangan BIT NULL",
                "CreatedBy INT NULL",
                "UpdatedBy INT NULL",
                "JumlahCetak INT NULL"
            }

            For Each definition As String In nullableColumns
                Dim columnName As String = definition.Substring(0, definition.IndexOf(" "c))
                If Not ColumnExists("Timbangan", columnName) Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Timbangan ADD " & definition)
                End If
            Next

            If ColumnExists("Timbangan", "TransType") Then
                DatabaseHelper.ExecuteNonQuery("UPDATE Timbangan SET TransType = 'JUAL' WHERE TransType IS NULL OR TransType = ''")
            End If
            If ColumnExists("Timbangan", "IncludeFFA") Then
                DatabaseHelper.ExecuteNonQuery("UPDATE Timbangan SET IncludeFFA = 0 WHERE IncludeFFA IS NULL")
            End If
            If ColumnExists("Timbangan", "IncludeKeterangan") Then
                DatabaseHelper.ExecuteNonQuery("UPDATE Timbangan SET IncludeKeterangan = 0 WHERE IncludeKeterangan IS NULL")
            End If
            If ColumnExists("Timbangan", "JumlahCetak") Then
                DatabaseHelper.ExecuteNonQuery("UPDATE Timbangan SET JumlahCetak = 0 WHERE JumlahCetak IS NULL")
            End If
        Catch ex As Exception
            Debug.WriteLine("[Migration] EnsureTimbanganColumns error: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' MIGRATION 1: PRODUCTS
    ' =============================================
    Private Shared Sub EnsureProductsTable()
        Try
            ' Jika Products sudah ada, pastikan kolom lengkap saja
            If TableExists("Products") Then
                If Not ColumnExists("Products", "KodeProduk") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Products ADD KodeProduk NVARCHAR(20) NULL")
                End If
                If Not ColumnExists("Products", "IsActive") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Products ADD IsActive BIT DEFAULT 1")
                End If
                If Not ColumnExists("Products", "CreatedAt") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Products ADD CreatedAt DATETIME NULL")
                End If
                If Not ColumnExists("Products", "UpdatedAt") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Products ADD UpdatedAt DATETIME NULL")
                End If
                Debug.WriteLine("[Migration] Products OK")
                Return
            End If

            ' Products belum ada - buat baru
            DatabaseHelper.ExecuteNonQuery(
                "CREATE TABLE Products (" &
                "ProductID INT PRIMARY KEY IDENTITY(1,1), " &
                "KodeProduk NVARCHAR(20) NULL, " &
                "NamaProduk NVARCHAR(100) NOT NULL, " &
                "IsActive BIT DEFAULT 1, " &
                "CreatedAt DATETIME DEFAULT GETDATE(), " &
                "UpdatedAt DATETIME DEFAULT GETDATE())")
            Debug.WriteLine("[Migration] Products dibuat baru")

            ' Jika tabel Produk (lama) ada, copy datanya
            If TableExists("Produk") Then
                Try
                    Dim hasKode As Boolean = ColumnExists("Produk", "KodeProduk")
                    If hasKode Then
                        DatabaseHelper.ExecuteNonQuery(
                            "INSERT INTO Products (KodeProduk, NamaProduk, IsActive) " &
                            "SELECT KodeProduk, NamaProduk, ISNULL(IsActive, 1) FROM Produk")
                    Else
                        DatabaseHelper.ExecuteNonQuery(
                            "INSERT INTO Products (NamaProduk, IsActive) " &
                            "SELECT NamaProduk, ISNULL(IsActive, 1) FROM Produk")
                    End If
                    Debug.WriteLine("[Migration] Data Produk di-copy ke Products")
                Catch exCopy As Exception
                    Debug.WriteLine("[Migration] Copy Produk error: " & exCopy.Message)
                End Try
            End If

        Catch ex As Exception
            Debug.WriteLine("[Migration] EnsureProductsTable error: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' MIGRATION 2: CUSTOMERS
    ' =============================================
    Private Shared Sub EnsureCustomersTable()
        Try
            ' Jika Customers sudah ada, pastikan kolom lengkap saja
            If TableExists("Customers") Then
                If Not ColumnExists("Customers", "Alamat") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Customers ADD Alamat NVARCHAR(255) NULL")
                End If
                If Not ColumnExists("Customers", "IsActive") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Customers ADD IsActive BIT DEFAULT 1")
                End If
                If Not ColumnExists("Customers", "CreatedAt") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Customers ADD CreatedAt DATETIME NULL")
                End If
                If Not ColumnExists("Customers", "UpdatedAt") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Customers ADD UpdatedAt DATETIME NULL")
                End If
                Debug.WriteLine("[Migration] Customers OK")
                Return
            End If

            ' Customers belum ada - buat baru
            DatabaseHelper.ExecuteNonQuery(
                "CREATE TABLE Customers (" &
                "CustomerID INT PRIMARY KEY IDENTITY(1,1), " &
                "KodeCustomer NVARCHAR(20) NULL, " &
                "NamaCustomer NVARCHAR(100) NOT NULL, " &
                "Alamat NVARCHAR(255) NULL, " &
                "IsActive BIT DEFAULT 1, " &
                "CreatedAt DATETIME DEFAULT GETDATE(), " &
                "UpdatedAt DATETIME DEFAULT GETDATE())")
            Debug.WriteLine("[Migration] Customers dibuat baru")

            ' Jika tabel Supplier (lama) ada, copy datanya
            If TableExists("Supplier") Then
                Try
                    DatabaseHelper.ExecuteNonQuery(
                        "INSERT INTO Customers (KodeCustomer, NamaCustomer, Alamat, IsActive) " &
                        "SELECT KodeSupplier, NamaSupplier, Alamat, ISNULL(IsActive, 1) FROM Supplier")
                    Debug.WriteLine("[Migration] Data Supplier di-copy ke Customers")
                Catch exCopy As Exception
                    Debug.WriteLine("[Migration] Copy Supplier error: " & exCopy.Message)
                End Try
            End If

        Catch ex As Exception
            Debug.WriteLine("[Migration] EnsureCustomersTable error: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' MIGRATION 3: TRANSPORTERS
    ' =============================================
    Private Shared Sub EnsureTransportersTable()
        Try
            ' Jika Transporters sudah ada, pastikan kolom lengkap saja
            If TableExists("Transporters") Then
                If Not ColumnExists("Transporters", "Alamat") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Transporters ADD Alamat NVARCHAR(255) NULL")
                End If
                If Not ColumnExists("Transporters", "IsActive") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Transporters ADD IsActive BIT DEFAULT 1")
                End If
                If Not ColumnExists("Transporters", "CreatedAt") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Transporters ADD CreatedAt DATETIME NULL")
                End If
                If Not ColumnExists("Transporters", "UpdatedAt") Then
                    DatabaseHelper.ExecuteNonQuery("ALTER TABLE Transporters ADD UpdatedAt DATETIME NULL")
                End If
                Debug.WriteLine("[Migration] Transporters OK")
                Return
            End If

            ' Transporters belum ada - buat baru
            DatabaseHelper.ExecuteNonQuery(
                "CREATE TABLE Transporters (" &
                "TransporterID INT PRIMARY KEY IDENTITY(1,1), " &
                "KodeTransporter NVARCHAR(20) NULL, " &
                "NamaTransporter NVARCHAR(100) NOT NULL, " &
                "Alamat NVARCHAR(255) NULL, " &
                "IsActive BIT DEFAULT 1, " &
                "CreatedAt DATETIME DEFAULT GETDATE(), " &
                "UpdatedAt DATETIME DEFAULT GETDATE())")
            Debug.WriteLine("[Migration] Transporters dibuat baru")

            ' Jika tabel Transporter (singular/lama) ada, copy datanya
            If TableExists("Transporter") Then
                Try
                    Dim hasAlamat As Boolean = ColumnExists("Transporter", "Alamat")
                    If hasAlamat Then
                        DatabaseHelper.ExecuteNonQuery(
                            "INSERT INTO Transporters (KodeTransporter, NamaTransporter, Alamat, IsActive) " &
                            "SELECT KodeTransporter, NamaTransporter, Alamat, ISNULL(IsActive, 1) FROM Transporter")
                    Else
                        DatabaseHelper.ExecuteNonQuery(
                            "INSERT INTO Transporters (KodeTransporter, NamaTransporter, IsActive) " &
                            "SELECT KodeTransporter, NamaTransporter, ISNULL(IsActive, 1) FROM Transporter")
                    End If
                    Debug.WriteLine("[Migration] Data Transporter di-copy ke Transporters")
                Catch exCopy As Exception
                    Debug.WriteLine("[Migration] Copy Transporter error: " & exCopy.Message)
                End Try
            End If

        Catch ex As Exception
            Debug.WriteLine("[Migration] EnsureTransportersTable error: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' MIGRATION 4: KOLOM KodeProduk di Products
    ' =============================================
    Private Shared Sub EnsureKodeProdukColumn()
        Try
            If TableExists("Products") AndAlso Not ColumnExists("Products", "KodeProduk") Then
                DatabaseHelper.ExecuteNonQuery("ALTER TABLE Products ADD KodeProduk NVARCHAR(20) NULL")
                Debug.WriteLine("[Migration] Kolom KodeProduk ditambahkan")
            End If
        Catch ex As Exception
            Debug.WriteLine("[Migration] EnsureKodeProdukColumn error: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' MIGRATION 5: INSERT PRODUK DEFAULT
    ' =============================================
    Private Shared Sub InsertDefaultProducts()
        Try
            If Not TableExists("Products") Then Return

            ' Update produk lama yang belum punya kode
            Try
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE Products SET KodeProduk = 'CPO' WHERE NamaProduk LIKE '%CPO%' AND (KodeProduk IS NULL OR KodeProduk = '')")
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE Products SET KodeProduk = 'TBS' WHERE NamaProduk LIKE '%TBS%' AND (KodeProduk IS NULL OR KodeProduk = '')")
            Catch ex As Exception
                Debug.WriteLine("[Migration] Failed to normalize legacy product codes: " & ex.Message)
            End Try

            ' Daftar produk default
            Dim kodeList() As String = {"CPO", "TBS", "NTTN", "CNGK", "JJK", "FBR"}
            Dim namaList() As String = {"Crude Palm Oil (CPO)", "Tandan Buah Segar (TBS)", "Nutten", "Cangkang", "Janjangan Kosong", "Fiber (Serabut)"}

            For i As Integer = 0 To kodeList.Length - 1
                Try
                    Dim count As Object = DatabaseHelper.ExecuteScalar(
                        "SELECT COUNT(*) FROM Products WHERE KodeProduk = @K",
                        {New SqlParameter("@K", kodeList(i))})

                    If count Is Nothing OrElse CInt(count) = 0 Then
                        DatabaseHelper.ExecuteNonQuery(
                            "INSERT INTO Products (NamaProduk, KodeProduk, IsActive) VALUES (@N, @K, 1)",
                            {New SqlParameter("@N", namaList(i)),
                             New SqlParameter("@K", kodeList(i))})
                        Debug.WriteLine("[Migration] Produk: " & kodeList(i))
                    End If
                Catch ex As Exception
                    Debug.WriteLine("[Migration] Failed to add default product " & kodeList(i) & ": " & ex.Message)
                End Try
            Next

        Catch ex As Exception
            Debug.WriteLine("[Migration] InsertDefaultProducts error: " & ex.Message)
        End Try
    End Sub

    ' =============================================
    ' MIGRATION 6: UNIQUE INDEX NoDO
    ' =============================================
    Private Shared Sub EnsureUniqueIndexNoDO()
        Try
            If Not TableExists("Timbangan") Then Return

            Dim result As Object = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM sys.indexes WHERE object_id = OBJECT_ID('Timbangan') AND name = 'UQ_Timbangan_NoDO'")

            If result Is Nothing OrElse CInt(result) = 0 Then
                DatabaseHelper.ExecuteNonQuery(
                    "CREATE UNIQUE NONCLUSTERED INDEX UQ_Timbangan_NoDO ON Timbangan(NoDO) WHERE NoDO IS NOT NULL AND NoDO <> ''")
                Debug.WriteLine("[Migration] Index NoDO dibuat")
            End If
        Catch ex As Exception
            Debug.WriteLine("[Migration] EnsureUniqueIndexNoDO error: " & ex.Message)
        End Try
    End Sub

End Class