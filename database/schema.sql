-- Palm Oil Weighbridge Management System
-- Reference schema for a clean SQL Server database.
-- The application also performs lightweight compatibility migrations at runtime.

IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users (
        UserID INT NOT NULL CONSTRAINT PK_Users PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) NOT NULL CONSTRAINT UQ_Users_Username UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        NamaLengkap NVARCHAR(100) NULL,
        Role NVARCHAR(20) NOT NULL CONSTRAINT DF_Users_Role DEFAULT N'Operator',
        AllowedTransType NVARCHAR(50) NOT NULL CONSTRAINT DF_Users_AllowedTransType DEFAULT N'SEMUA',
        IsActive BIT NOT NULL CONSTRAINT DF_Users_IsActive DEFAULT 1,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Users_CreatedAt DEFAULT GETDATE(),
        CreatedBy INT NULL,
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Users_UpdatedAt DEFAULT GETDATE(),
        UpdatedBy INT NULL
    );
END;

IF OBJECT_ID(N'dbo.Customers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Customers (
        CustomerID INT NOT NULL CONSTRAINT PK_Customers PRIMARY KEY IDENTITY(1,1),
        KodeCustomer NVARCHAR(20) NULL CONSTRAINT UQ_Customers_KodeCustomer UNIQUE,
        NamaCustomer NVARCHAR(100) NOT NULL,
        Alamat NVARCHAR(255) NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Customers_IsActive DEFAULT 1,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Customers_CreatedAt DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Customers_UpdatedAt DEFAULT GETDATE()
    );
END;

IF OBJECT_ID(N'dbo.Transporters', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Transporters (
        TransporterID INT NOT NULL CONSTRAINT PK_Transporters PRIMARY KEY IDENTITY(1,1),
        KodeTransporter NVARCHAR(20) NULL CONSTRAINT UQ_Transporters_KodeTransporter UNIQUE,
        NamaTransporter NVARCHAR(100) NOT NULL,
        Alamat NVARCHAR(255) NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Transporters_IsActive DEFAULT 1,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Transporters_CreatedAt DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Transporters_UpdatedAt DEFAULT GETDATE()
    );
END;

IF OBJECT_ID(N'dbo.Products', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products (
        ProductID INT NOT NULL CONSTRAINT PK_Products PRIMARY KEY IDENTITY(1,1),
        KodeProduk NVARCHAR(20) NULL CONSTRAINT UQ_Products_KodeProduk UNIQUE,
        NamaProduk NVARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Products_IsActive DEFAULT 1,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Products_CreatedAt DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Products_UpdatedAt DEFAULT GETDATE()
    );
END;

IF OBJECT_ID(N'dbo.Timbangan', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Timbangan (
        TimbangID INT NOT NULL CONSTRAINT PK_Timbangan PRIMARY KEY IDENTITY(1,1),
        NoTiket NVARCHAR(30) NOT NULL CONSTRAINT UQ_Timbangan_NoTiket UNIQUE,
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
        TransType NVARCHAR(10) NOT NULL CONSTRAINT DF_Timbangan_TransType DEFAULT N'JUAL',
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
        Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Timbangan_Status DEFAULT N'MASUK',
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
END;

IF OBJECT_ID(N'dbo.AuditLog', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.AuditLog (
        LogID INT NOT NULL CONSTRAINT PK_AuditLog PRIMARY KEY IDENTITY(1,1),
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
END;

IF OBJECT_ID(N'dbo.Settings', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Settings (
        SettingID INT NOT NULL CONSTRAINT PK_Settings PRIMARY KEY IDENTITY(1,1),
        SettingKey NVARCHAR(100) NOT NULL CONSTRAINT UQ_Settings_SettingKey UNIQUE,
        SettingValue NVARCHAR(MAX) NULL,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Settings_CreatedAt DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Settings_UpdatedAt DEFAULT GETDATE()
    );
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Products)
BEGIN
    INSERT INTO dbo.Products (KodeProduk, NamaProduk, IsActive) VALUES
        (N'CPO', N'Crude Palm Oil (CPO)', 1),
        (N'TBS', N'Tandan Buah Segar (TBS)', 1),
        (N'NTTN', N'Nutten', 1),
        (N'CNGK', N'Cangkang', 1),
        (N'JJK', N'Janjangan Kosong', 1),
        (N'FBR', N'Fiber (Serabut)', 1);
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Timbangan')
      AND name = N'UQ_Timbangan_NoDO'
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_Timbangan_NoDO
        ON dbo.Timbangan(NoDO)
        WHERE NoDO IS NOT NULL AND NoDO <> N'';
END;
