-- Stored procedures used by the Palm Oil Weighbridge Management System.

CREATE OR ALTER PROCEDURE dbo.sp_GenerateNoTiket
    @Prefix NVARCHAR(10),
    @NoTiket NVARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Counter INT;
    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @DatePart NVARCHAR(6) = FORMAT(GETDATE(), 'yyMMdd');

    SELECT @Counter = COUNT(*) + 1
    FROM dbo.Timbangan
    WHERE CAST(TanggalMasuk AS DATE) = @Today;

    SET @NoTiket = @Prefix + N'-' + @DatePart + RIGHT(N'000' + CAST(@Counter AS NVARCHAR(10)), 3);
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_GenerateNoTiketByProduct
    @KodeProduk NVARCHAR(20),
    @NoTiket NVARCHAR(30) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Counter INT;
    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @DatePart NVARCHAR(8) = FORMAT(GETDATE(), 'yy-MM-dd');

    SELECT @Counter = COUNT(*) + 1
    FROM dbo.Timbangan
    WHERE CAST(TanggalMasuk AS DATE) = @Today
      AND NoTiket LIKE @KodeProduk + N'-%';

    SET @NoTiket = @KodeProduk + N'-' + @DatePart + N'-' + RIGHT(N'000' + CAST(@Counter AS NVARCHAR(10)), 3);
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckNoDOExists
    @NoDO NVARCHAR(50),
    @ExcludeTimbangID INT = 0,
    @Exists BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM dbo.Timbangan
        WHERE NoDO = @NoDO
          AND TimbangID <> @ExcludeTimbangID
          AND NoDO IS NOT NULL
          AND NoDO <> N''
    )
        SET @Exists = 1;
    ELSE
        SET @Exists = 0;
END;
GO
