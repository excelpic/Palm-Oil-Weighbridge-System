' =============================================
' DatabaseSetup.vb - Auto Setup Database untuk Komputer Baru
' Lokasi: Classes/DatabaseSetup.vb
' =============================================
Imports System.Data.SqlClient
Imports System.IO

Public Class DatabaseSetup

    ' =============================================
    ' SCRIPT SQL UNTUK MEMBUAT TABEL-TABEL
    ' Sesuaikan dengan struktur database kamu!
    ' =============================================
    Private Shared ReadOnly CREATE_TABLES_SCRIPT As String = "
-- ============================================================================
-- Core schema for the Palm Oil Weighbridge Management System
-- ============================================================================

-- Users
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        NamaLengkap NVARCHAR(100) NULL,
        Role NVARCHAR(20) NOT NULL CONSTRAINT DF_Users_Role DEFAULT 'Operator',
        AllowedTransType NVARCHAR(50) NOT NULL CONSTRAINT DF_Users_AllowedTransType DEFAULT 'SEMUA',
        IsActive BIT NOT NULL CONSTRAINT DF_Users_IsActive DEFAULT 1,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Users_CreatedAt DEFAULT GETDATE(),
        CreatedBy INT NULL,
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Users_UpdatedAt DEFAULT GETDATE(),
        UpdatedBy INT NULL
    );
END

-- Customers (master data)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE Customers (
        CustomerID INT PRIMARY KEY IDENTITY(1,1),
        KodeCustomer NVARCHAR(20) NULL UNIQUE,
        NamaCustomer NVARCHAR(100) NOT NULL,
        Alamat NVARCHAR(255) NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Customers_IsActive DEFAULT 1,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Customers_CreatedAt DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Customers_UpdatedAt DEFAULT GETDATE()
    );
END

-- Transporters (master data)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Transporters')
BEGIN
    CREATE TABLE Transporters (
        TransporterID INT PRIMARY KEY IDENTITY(1,1),
        KodeTransporter NVARCHAR(20) NULL UNIQUE,
        NamaTransporter NVARCHAR(100) NOT NULL,
        Alamat NVARCHAR(255) NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Transporters_IsActive DEFAULT 1,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Transporters_CreatedAt DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Transporters_UpdatedAt DEFAULT GETDATE()
    );
END

-- Products (master data)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        ProductID INT PRIMARY KEY IDENTITY(1,1),
        KodeProduk NVARCHAR(20) NULL UNIQUE,
        NamaProduk NVARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Products_IsActive DEFAULT 1,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Products_CreatedAt DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Products_UpdatedAt DEFAULT GETDATE()
    );
END

-- Weighbridge transactions
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Timbangan')
BEGIN
    CREATE TABLE Timbangan (
        TimbangID INT PRIMARY KEY IDENTITY(1,1),
        NoTiket NVARCHAR(30) NOT NULL UNIQUE,
        NoPolisi NVARCHAR(20) NOT NULL,
        NamaSupir NVARCHAR(100) NULL,
        NoSIM NVARCHAR(50) NULL,
        TransporterID INT NULL,
        TransporterNama NVARCHAR(100) NULL,
        CustomerID INT NULL,
        CustomerNama NVARCHAR(100) NULL,
        Alamat NVARCHAR(255) NULL,
        ProductID INT NULL,
        ProductNama NVARCHAR(100) NULL,
        TransType NVARCHAR(10) NOT NULL CONSTRAINT DF_Timbangan_TransType DEFAULT 'JUAL',
        NoDO NVARCHAR(50) NULL,
        NoKontrak NVARCHAR(50) NULL,
        SegelAtas NVARCHAR(50) NULL,
        SegelBawah NVARCHAR(50) NULL,
        BeratMasuk DECIMAL(18,2) NOT NULL CONSTRAINT DF_Timbangan_BeratMasuk DEFAULT 0,
        BeratKeluar DECIMAL(18,2) NULL,
        BeratNetto DECIMAL(18,2) NULL,
        Potongan DECIMAL(18,2) NOT NULL CONSTRAINT DF_Timbangan_Potongan DEFAULT 0,
        PotonganPersen DECIMAL(8,3) NOT NULL CONSTRAINT DF_Timbangan_PotonganPersen DEFAULT 0,
        PotonganCong DECIMAL(18,2) NOT NULL CONSTRAINT DF_Timbangan_PotonganCong DEFAULT 0,
        TotalPotongan DECIMAL(18,2) NOT NULL CONSTRAINT DF_Timbangan_TotalPotongan DEFAULT 0,
        BeratBersih DECIMAL(18,2) NOT NULL CONSTRAINT DF_Timbangan_BeratBersih DEFAULT 0,
        IncludeFFA BIT NOT NULL CONSTRAINT DF_Timbangan_IncludeFFA DEFAULT 0,
        FFA DECIMAL(10,3) NOT NULL CONSTRAINT DF_Timbangan_FFA DEFAULT 0,
        Moisture DECIMAL(10,3) NOT NULL CONSTRAINT DF_Timbangan_Moisture DEFAULT 0,
        Dirt DECIMAL(10,3) NOT NULL CONSTRAINT DF_Timbangan_Dirt DEFAULT 0,
        SuhuMinyak DECIMAL(10,2) NOT NULL CONSTRAINT DF_Timbangan_SuhuMinyak DEFAULT 0,
        IncludeKeterangan BIT NOT NULL CONSTRAINT DF_Timbangan_IncludeKeterangan DEFAULT 0,
        Keterangan NVARCHAR(255) NULL,
        Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Timbangan_Status DEFAULT 'MASUK',
        KraniMasuk INT NULL,
        KraniKeluar INT NULL,
        PetugasMasukID INT NULL,
        PetugasKeluarID INT NULL,
        TanggalMasuk DATETIME NOT NULL CONSTRAINT DF_Timbangan_TanggalMasuk DEFAULT GETDATE(),
        TanggalKeluar DATETIME NULL,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Timbangan_CreatedAt DEFAULT GETDATE(),
        CreatedBy INT NULL,
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Timbangan_UpdatedAt DEFAULT GETDATE(),
        UpdatedBy INT NULL,
        JumlahCetak INT NOT NULL CONSTRAINT DF_Timbangan_JumlahCetak DEFAULT 0
    );
END

-- Audit trail
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'AuditLog')
BEGIN
    CREATE TABLE AuditLog (
        LogID INT PRIMARY KEY IDENTITY(1,1),
        UserID INT NULL,
        Action NVARCHAR(50) NULL,
        TableName NVARCHAR(50) NULL,
        RecordID INT NULL,
        OldValue NVARCHAR(MAX) NULL,
        NewValue NVARCHAR(MAX) NULL,
        Keterangan NVARCHAR(255) NULL,
        IPAddress NVARCHAR(50) NULL,
        ComputerName NVARCHAR(100) NULL,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_AuditLog_CreatedAt DEFAULT GETDATE()
    );
END

-- Application settings
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Settings')
BEGIN
    CREATE TABLE Settings (
        SettingID INT PRIMARY KEY IDENTITY(1,1),
        SettingKey NVARCHAR(100) NOT NULL UNIQUE,
        SettingValue NVARCHAR(MAX) NULL,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Settings_CreatedAt DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Settings_UpdatedAt DEFAULT GETDATE()
    );
END

-- Default master data for a clean installation.
IF NOT EXISTS (SELECT 1 FROM Products)
BEGIN
    INSERT INTO Products (KodeProduk, NamaProduk, IsActive) VALUES
        ('CPO', 'Crude Palm Oil (CPO)', 1),
        ('TBS', 'Tandan Buah Segar (TBS)', 1),
        ('NTTN', 'Nutten', 1),
        ('CNGK', 'Cangkang', 1),
        ('JJK', 'Janjangan Kosong', 1),
        ('FBR', 'Fiber (Serabut)', 1);
END

-- Unique NoDO only when a value is actually supplied.
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('Timbangan') AND name = 'UQ_Timbangan_NoDO')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_Timbangan_NoDO
        ON Timbangan(NoDO)
        WHERE NoDO IS NOT NULL AND NoDO <> '';
END
"


    ' =============================================
    ' SCRIPT STORED PROCEDURE
    ' =============================================
    Private Shared ReadOnly CREATE_SP_SCRIPT As String = "
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GenerateNoTiket')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_GenerateNoTiket
        @Prefix NVARCHAR(10),
        @NoTiket NVARCHAR(20) OUTPUT
    AS
    BEGIN
        DECLARE @Counter INT;
        DECLARE @Today DATE = CAST(GETDATE() AS DATE);
        DECLARE @DatePart NVARCHAR(6) = FORMAT(GETDATE(), ''yyMMdd'');
        
        SELECT @Counter = COUNT(*) + 1 
        FROM Timbangan 
        WHERE CAST(TanggalMasuk AS DATE) = @Today;
        
        SET @NoTiket = @Prefix + ''-'' + @DatePart + RIGHT(''000'' + CAST(@Counter AS NVARCHAR), 3);
    END
    ');
    PRINT 'Stored Procedure sp_GenerateNoTiket dibuat';
END

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GenerateNoTiketByProduct')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_GenerateNoTiketByProduct
        @KodeProduk NVARCHAR(20),
        @NoTiket NVARCHAR(30) OUTPUT
    AS
    BEGIN
        DECLARE @Counter INT;
        DECLARE @Today DATE = CAST(GETDATE() AS DATE);
        DECLARE @DatePart NVARCHAR(8) = FORMAT(GETDATE(), ''yy-MM-dd'');
        
        SELECT @Counter = COUNT(*) + 1 
        FROM Timbangan 
        WHERE CAST(TanggalMasuk AS DATE) = @Today
        AND NoTiket LIKE @KodeProduk + ''-%'';
        
        SET @NoTiket = @KodeProduk + ''-'' + @DatePart + ''-'' + RIGHT(''000'' + CAST(@Counter AS NVARCHAR), 3);
    END
    ');
    PRINT 'Stored Procedure sp_GenerateNoTiketByProduct dibuat';
END

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_CheckNoDOExists')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_CheckNoDOExists
        @NoDO NVARCHAR(50),
        @ExcludeTimbangID INT = 0,
        @Exists BIT OUTPUT
    AS
    BEGIN
        IF EXISTS (SELECT 1 FROM Timbangan WHERE NoDO = @NoDO AND TimbangID <> @ExcludeTimbangID AND NoDO IS NOT NULL AND NoDO <> '''')
            SET @Exists = 1
        ELSE
            SET @Exists = 0
    END
    ');
    PRINT 'Stored Procedure sp_CheckNoDOExists dibuat';
END
"

    ' =============================================
    ' FUNGSI UTAMA: SETUP DATABASE LENGKAP
    ' =============================================
    Public Shared Function SetupDatabase(serverName As String, dbName As String,
                                         useWindowsAuth As Boolean,
                                         sqlUsername As String, sqlPassword As String,
                                         ByRef errorMessage As String,
                                         Optional showProgress As Boolean = True) As Boolean
        Try
            ' Step 1: Buat database jika belum ada
            If showProgress Then Debug.WriteLine("[DatabaseSetup] Step 1: Cek/Buat database...")

            If Not DatabaseHelper.EnsureDatabaseExists(serverName, dbName, useWindowsAuth,
                                                       sqlUsername, sqlPassword, errorMessage) Then
                Return False
            End If

            ' Step 2: Buat tabel-tabel
            If showProgress Then Debug.WriteLine("[DatabaseSetup] Step 2: Membuat tabel...")

            If Not CreateTables(serverName, dbName, useWindowsAuth,
                               sqlUsername, sqlPassword, errorMessage) Then
                Return False
            End If

            ' Step 3: Buat stored procedures
            If showProgress Then Debug.WriteLine("[DatabaseSetup] Step 3: Membuat stored procedures...")

            If Not CreateStoredProcedures(serverName, dbName, useWindowsAuth,
                                          sqlUsername, sqlPassword, errorMessage) Then
                ' Stored procedures are optional; keep the core database usable if one cannot be created.
                Debug.WriteLine("[DatabaseSetup] Warning SP: " & errorMessage)
            End If

            ' Apply lightweight compatibility migrations after initial creation or upgrade.
            DatabaseMigration.RunMigrations()

            errorMessage = "Database berhasil di-setup!"
            Debug.WriteLine("[DatabaseSetup] ✅ Setup selesai!")
            Return True

        Catch ex As Exception
            errorMessage = "Database setup could not be completed. Check the database connection and permissions."
            Debug.WriteLine("[DatabaseSetup.SetupDatabase] Error: " & ex.ToString())
            Return False
        End Try
    End Function

    ' =============================================
    ' BUAT TABEL-TABEL
    ' =============================================
    Private Shared Function CreateTables(serverName As String, dbName As String,
                                         useWindowsAuth As Boolean,
                                         sqlUsername As String, sqlPassword As String,
                                         ByRef errorMessage As String) As Boolean
        Try
            Dim connStr As String = DatabaseHelper.BuildConnectionString(
                serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword, 30)

            Using conn As New SqlConnection(connStr)
                conn.Open()

                Using cmd As New SqlCommand(CREATE_TABLES_SCRIPT, conn)
                    cmd.CommandTimeout = 120
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            errorMessage = ""
            Return True

        Catch ex As Exception
            errorMessage = "Database tables could not be created. Check the database connection and permissions."
            Debug.WriteLine("[DatabaseSetup.CreateTables] Error: " & ex.ToString())
            Return False
        End Try
    End Function

    ' =============================================
    ' BUAT STORED PROCEDURES
    ' =============================================
    Private Shared Function CreateStoredProcedures(serverName As String, dbName As String,
                                                   useWindowsAuth As Boolean,
                                                   sqlUsername As String, sqlPassword As String,
                                                   ByRef errorMessage As String) As Boolean
        Try
            Dim connStr As String = DatabaseHelper.BuildConnectionString(
                serverName, dbName, useWindowsAuth, sqlUsername, sqlPassword, 30)

            Using conn As New SqlConnection(connStr)
                conn.Open()

                Using cmd As New SqlCommand(CREATE_SP_SCRIPT, conn)
                    cmd.CommandTimeout = 60
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            errorMessage = ""
            Return True

        Catch ex As Exception
            errorMessage = "Database stored procedures could not be created. Check the database connection and permissions."
            Debug.WriteLine("[DatabaseSetup.CreateStoredProcedures] Error: " & ex.ToString())
            Return False
        End Try
    End Function

    ' =============================================
    ' CEK APAKAH DATABASE SUDAH DI-SETUP
    ' =============================================
    Public Shared Function IsDatabaseSetup() As Boolean
        Try
            ' A database is considered initialized only when all core tables exist.
            Dim query As String =
                "SELECT COUNT(*) FROM sys.tables WHERE name IN ('Users', 'Customers', 'Transporters', 'Products', 'Timbangan', 'AuditLog', 'Settings')"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0)(0)) = 7
            End If

            Return False
        Catch ex As Exception
            Debug.WriteLine("[DatabaseSetup.IsDatabaseSetup] Error: " & ex.ToString())
            Return False
        End Try
    End Function

    ' =============================================
    ' CEK APAKAH ADA USER DI DATABASE
    ' =============================================
    Public Shared Function HasUsers() As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM Users"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0)(0)) > 0
            End If

            Return False
        Catch ex As Exception
            Debug.WriteLine("[DatabaseSetup.HasUsers] Error: " & ex.ToString())
            Return False
        End Try
    End Function

    ' =============================================
    ' GET DATABASE INFO
    ' =============================================
    Public Shared Function GetDatabaseInfo() As Dictionary(Of String, String)
        Dim info As New Dictionary(Of String, String)

        Try
            ' Cek jumlah tabel
            Dim dtTables = DatabaseHelper.ExecuteQuery("SELECT COUNT(*) FROM sys.tables")
            If dtTables IsNot Nothing AndAlso dtTables.Rows.Count > 0 Then
                info("TotalTables") = dtTables.Rows(0)(0).ToString()
            End If

            ' Cek jumlah users
            Try
                Dim dtUsers = DatabaseHelper.ExecuteQuery("SELECT COUNT(*) FROM Users")
                If dtUsers IsNot Nothing AndAlso dtUsers.Rows.Count > 0 Then
                    info("TotalUsers") = dtUsers.Rows(0)(0).ToString()
                End If
            Catch ex As Exception
                Debug.WriteLine("[DatabaseSetup.GetDatabaseInfo] Users count error: " & ex.ToString())
                info("TotalUsers") = "0"
            End Try

            ' Cek jumlah transaksi
            Try
                Dim dtTrx = DatabaseHelper.ExecuteQuery("SELECT COUNT(*) FROM Timbangan")
                If dtTrx IsNot Nothing AndAlso dtTrx.Rows.Count > 0 Then
                    info("TotalTransaksi") = dtTrx.Rows(0)(0).ToString()
                End If
            Catch ex As Exception
                Debug.WriteLine("[DatabaseSetup.GetDatabaseInfo] Transaction count error: " & ex.ToString())
                info("TotalTransaksi") = "0"
            End Try

        Catch ex As Exception
            Debug.WriteLine("[DatabaseSetup.GetDatabaseInfo] Error: " & ex.ToString())
            info("Error") = "Database information could not be loaded."
        End Try

        Return info
    End Function

End Class
