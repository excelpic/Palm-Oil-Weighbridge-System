Imports System
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Text

Public NotInheritable Class PasswordHasher
    Private Sub New()
    End Sub

    ' PBKDF2-HMAC-SHA256 with a unique random salt per password.
    ' The iteration count is stored with the hash so it can be upgraded later.
    Private Const Scheme As String = "PBKDF2-SHA256"
    Private Const CurrentIterations As Integer = 600000
    Private Const SaltSize As Integer = 16
    Private Const HashSize As Integer = 32
    Private Const MaximumPasswordLength As Integer = 128

    Public Shared Function HashPassword(password As String) As String
        If password Is Nothing Then Throw New ArgumentNullException(NameOf(password))
        If password.Length = 0 OrElse password.Length > MaximumPasswordLength Then
            Throw New ArgumentException("Password length is outside the permitted range.", NameOf(password))
        End If

        Dim salt(SaltSize - 1) As Byte
        Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
            rng.GetBytes(salt)
        End Using

        Dim derivedKey As Byte()
        Using pbkdf2 As New Rfc2898DeriveBytes(password, salt, CurrentIterations, HashAlgorithmName.SHA256)
            derivedKey = pbkdf2.GetBytes(HashSize)
        End Using

        Return String.Join("$", Scheme,
                           CurrentIterations.ToString(CultureInfo.InvariantCulture),
                           Convert.ToBase64String(salt),
                           Convert.ToBase64String(derivedKey))
    End Function

    Public Shared Function VerifyPassword(storedHash As String,
                                          password As String,
                                          ByRef needsRehash As Boolean) As Boolean
        needsRehash = False
        If String.IsNullOrEmpty(storedHash) OrElse password Is Nothing Then Return False
        If password.Length > MaximumPasswordLength Then Return False

        If storedHash.StartsWith(Scheme & "$", StringComparison.Ordinal) Then
            Dim parts As String() = storedHash.Split("$"c)
            If parts.Length <> 4 Then Return False

            Dim iterations As Integer
            If Not Integer.TryParse(parts(1), NumberStyles.None, CultureInfo.InvariantCulture, iterations) Then Return False
            If iterations <= 0 OrElse iterations > 5000000 Then Return False

            Dim salt As Byte()
            Dim expectedHash As Byte()
            Try
                salt = Convert.FromBase64String(parts(2))
                expectedHash = Convert.FromBase64String(parts(3))
            Catch ex As FormatException
                Return False
            End Try

            If salt.Length <> SaltSize OrElse expectedHash.Length <> HashSize Then Return False

            Dim actualHash As Byte()
            Using pbkdf2 As New Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256)
                actualHash = pbkdf2.GetBytes(HashSize)
            End Using

            needsRehash = iterations < CurrentIterations
            Return FixedTimeEquals(actualHash, expectedHash)
        End If

        ' Backward compatibility for legacy unsalted SHA-256 hashes.
        ' A successful legacy login is upgraded immediately to PBKDF2 by the caller.
        If IsLegacySha256(storedHash) Then
            Dim legacyHash As Byte() = ComputeSha256(password)
            Dim expectedLegacyHash As Byte() = HexToBytes(storedHash)
            Return expectedLegacyHash IsNot Nothing AndAlso FixedTimeEquals(legacyHash, expectedLegacyHash)
        End If

        Return False
    End Function

    Public Shared Function IsLegacySha256(storedHash As String) As Boolean
        If String.IsNullOrEmpty(storedHash) OrElse storedHash.Length <> 64 Then Return False
        For Each ch As Char In storedHash
            If Not ((ch >= "0"c AndAlso ch <= "9"c) OrElse
                    (ch >= "a"c AndAlso ch <= "f"c) OrElse
                    (ch >= "A"c AndAlso ch <= "F"c)) Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Shared Function ValidatePassword(password As String, ByRef validationMessage As String) As Boolean
        validationMessage = String.Empty
        If password Is Nothing OrElse password.Length = 0 Then
            validationMessage = "Password cannot be empty."
            Return False
        End If
        If password.Length > MaximumPasswordLength Then
            validationMessage = $"Password must not exceed {MaximumPasswordLength} characters."
            Return False
        End If
        If password.Length < 10 Then
            validationMessage = "Password must contain at least 10 characters."
            Return False
        End If
        If Not System.Linq.Enumerable.Any(password, Function(c) Char.IsUpper(c)) OrElse
           Not System.Linq.Enumerable.Any(password, Function(c) Char.IsLower(c)) OrElse
           Not System.Linq.Enumerable.Any(password, Function(c) Char.IsDigit(c)) Then
            validationMessage = "Password must contain uppercase, lowercase, and numeric characters."
            Return False
        End If
        Return True
    End Function

    Private Shared Function ComputeSha256(value As String) As Byte()
        Using sha256 As SHA256 = SHA256.Create()
            Return sha256.ComputeHash(Encoding.UTF8.GetBytes(value))
        End Using
    End Function

    Private Shared Function HexToBytes(hex As String) As Byte()
        If Not IsLegacySha256(hex) Then Return Nothing
        Dim bytes(31) As Byte
        For i As Integer = 0 To bytes.Length - 1
            bytes(i) = Byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)
        Next
        Return bytes
    End Function

    Private Shared Function FixedTimeEquals(left As Byte(), right As Byte()) As Boolean
        If left Is Nothing OrElse right Is Nothing Then Return False
        Dim diff As Integer = left.Length Xor right.Length
        Dim length As Integer = Math.Min(left.Length, right.Length)
        For i As Integer = 0 To length - 1
            diff = diff Or (left(i) Xor right(i))
        Next
        Return diff = 0
    End Function
End Class
