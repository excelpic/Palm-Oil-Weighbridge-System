' =============================================
' UserSession.vb
' Class untuk menyimpan data user yang login
' DITAMBAH: AllowedTransType untuk filter data
' =============================================

Public Class UserSession

    ' =============================================
    ' DATA USER YANG SEDANG LOGIN
    ' =============================================
    Public Shared Property UserID As Integer = 0
    Public Shared Property Username As String = ""
    Public Shared Property NamaLengkap As String = ""
    Public Shared Property Role As String = ""
    Public Shared Property RoleName As String = ""
    Public Shared Property Email As String = ""
    Public Shared Property LastLogin As DateTime = DateTime.MinValue
    Public Shared Property AllowedTransType As String = "SEMUA"

    ' =============================================
    ' ALIAS UNTUK KOMPATIBILITAS DENGAN KODE LAMA
    ' =============================================
    Public Shared Property CurrentUserID As Integer
        Get
            Return UserID
        End Get
        Set(value As Integer)
            UserID = value
        End Set
    End Property

    Public Shared Property CurrentUsername As String
        Get
            Return Username
        End Get
        Set(value As String)
            Username = value
        End Set
    End Property

    Public Shared Property CurrentUserRole As String
        Get
            Return Role
        End Get
        Set(value As String)
            Role = value
        End Set
    End Property

    Public Shared Property CurrentUserFullName As String
        Get
            Return NamaLengkap
        End Get
        Set(value As String)
            NamaLengkap = value
        End Set
    End Property

    ' =============================================
    ' SET SESSION
    ' =============================================
    Public Shared Sub SetSession(userID As Integer, username As String, role As String, fullName As String)
        UserSession.UserID = userID
        UserSession.Username = username
        UserSession.Role = role
        UserSession.RoleName = role
        UserSession.NamaLengkap = fullName
        UserSession.Email = ""
        UserSession.AllowedTransType = "SEMUA"
        UserSession.LastLogin = DateTime.Now
    End Sub

    Public Shared Sub SetSession(userID As Integer, username As String, role As String, fullName As String, email As String)
        UserSession.UserID = userID
        UserSession.Username = username
        UserSession.Role = role
        UserSession.RoleName = role
        UserSession.NamaLengkap = fullName
        UserSession.Email = email
        UserSession.AllowedTransType = "SEMUA"
        UserSession.LastLogin = DateTime.Now
    End Sub

    ' =============================================
    ' CLEAR SESSION / LOGOUT
    ' =============================================
    Public Shared Sub ClearSession()
        UserID = 0
        Username = ""
        NamaLengkap = ""
        Role = ""
        RoleName = ""
        Email = ""
        LastLogin = DateTime.MinValue
        AllowedTransType = "SEMUA"
    End Sub

    Public Shared Sub Logout()
        ClearSession()
    End Sub

    ' =============================================
    ' CEK STATUS LOGIN
    ' =============================================
    Public Shared Function IsLoggedIn() As Boolean
        Return UserID > 0 AndAlso Not String.IsNullOrEmpty(Username)
    End Function

    ' =============================================
    ' CEK ROLE
    ' =============================================
    Public Shared Function IsProgrammer() As Boolean
        Return String.Equals(Role, "programmer", StringComparison.OrdinalIgnoreCase)
    End Function

    Public Shared Function IsDirektur() As Boolean
        Return String.Equals(Role, "direktur", StringComparison.OrdinalIgnoreCase)
    End Function

    Public Shared Function IsManager() As Boolean
        Return String.Equals(Role, "manager", StringComparison.OrdinalIgnoreCase)
    End Function

    Public Shared Function IsKrani() As Boolean
        Return String.Equals(Role, "krani", StringComparison.OrdinalIgnoreCase)
    End Function

    Public Shared Function IsAdmin() As Boolean
        Return String.Equals(Role, "admin", StringComparison.OrdinalIgnoreCase) OrElse IsProgrammer()
    End Function

    Public Shared Function IsSupervisor() As Boolean
        Return String.Equals(Role, "supervisor", StringComparison.OrdinalIgnoreCase)
    End Function

    ' =============================================
    ' HAK AKSES - UMUM
    ' =============================================
    Public Shared Function CanManageUser(targetRole As String) As Boolean
        Select Case If(Role, String.Empty).Trim().ToLowerInvariant()
            Case "programmer"
                Return True
            Case "direktur"
                Return Not String.Equals(targetRole, "programmer", StringComparison.OrdinalIgnoreCase)
            Case "manager"
                Return String.Equals(targetRole, "krani", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(targetRole, "operator", StringComparison.OrdinalIgnoreCase)
            Case Else
                Return False
        End Select
    End Function

    Public Shared Function CanEditSelf() As Boolean
        Return IsLoggedIn()
    End Function

    ' =============================================
    ' HAK AKSES - TIMBANGAN
    ' =============================================
    Public Shared Function CanInputTimbangan() As Boolean
        Return IsProgrammer() OrElse IsDirektur() OrElse IsManager() OrElse IsKrani()
    End Function

    Public Shared Function CanEditTimbangan() As Boolean
        Return IsProgrammer() OrElse IsDirektur() OrElse IsManager()
    End Function

    Public Shared Function CanDeleteTimbangan() As Boolean
        Return IsProgrammer() OrElse IsDirektur()
    End Function

    Public Shared Function CanPrintStruk() As Boolean
        Return IsLoggedIn()
    End Function

    Public Shared Function CanReprintStruk() As Boolean
        Return IsProgrammer() OrElse IsDirektur() OrElse IsManager() OrElse IsKrani()
    End Function

    ' =============================================
    ' HAK AKSES - FFA & KETERANGAN
    ' =============================================
    Public Shared Function CanEditFFA() As Boolean
        Return CanInputTimbangan()
    End Function

    Public Shared Function CanEditKeterangan() As Boolean
        Return CanInputTimbangan()
    End Function

    ' =============================================
    ' HAK AKSES - SETTINGS & SISTEM
    ' =============================================
    Public Shared Function CanAccessSettings() As Boolean
        Return IsProgrammer() OrElse IsDirektur() OrElse IsManager()
    End Function

    Public Shared Function CanEditPerusahaan() As Boolean
        Return IsProgrammer() OrElse IsDirektur()
    End Function

    Public Shared Function CanEditIndikator() As Boolean
        Return IsProgrammer()
    End Function

    ' =============================================
    ' HAK AKSES - LAPORAN & AUDIT
    ' =============================================
    Public Shared Function CanViewAuditLog() As Boolean
        Return IsProgrammer() OrElse IsDirektur()
    End Function

    Public Shared Function CanExportReport() As Boolean
        Return IsProgrammer() OrElse IsDirektur() OrElse IsManager()
    End Function

    Public Shared Function CanViewReport() As Boolean
        Return IsLoggedIn()
    End Function

    ' =============================================
    ' HAK AKSES - MASTER DATA
    ' =============================================
    Public Shared Function CanManageMasterData() As Boolean
        Return IsProgrammer() OrElse IsDirektur() OrElse IsManager()
    End Function

    Public Shared Function CanAddMasterData() As Boolean
        Return CanManageMasterData()
    End Function

    Public Shared Function CanEditMasterData() As Boolean
        Return CanManageMasterData()
    End Function

    Public Shared Function CanDeleteMasterData() As Boolean
        Return IsProgrammer() OrElse IsDirektur()
    End Function

    ' =============================================
    ' HAK AKSES - USER MANAGEMENT
    ' =============================================
    Public Shared Function CanAccessUserManagement() As Boolean
        Return IsLoggedIn()
    End Function

    Public Shared Function CanAddUser() As Boolean
        Return IsProgrammer() OrElse IsDirektur() OrElse IsManager()
    End Function

    Public Shared Function CanResetPassword() As Boolean
        Return IsProgrammer() OrElse IsDirektur()
    End Function

    ' =============================================
    ' HAK AKSES - FILTER TRANS TYPE (BARU)
    ' =============================================

    ''' <summary>
    ''' Cek apakah user bisa melihat TransType tertentu
    ''' </summary>
    Public Shared Function CanViewTransType(transType As String) As Boolean
        If String.IsNullOrEmpty(AllowedTransType) OrElse
           AllowedTransType.Equals("SEMUA", StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If
        Return String.Equals(AllowedTransType, transType, StringComparison.OrdinalIgnoreCase)
    End Function

    ''' <summary>
    ''' Dapatkan filter SQL untuk TransType
    ''' </summary>
    Public Shared Function GetTransTypeFilter(tableAlias As String) As String
        Dim normalizedType As String = If(AllowedTransType, "SEMUA").Trim().ToUpperInvariant()
        If normalizedType = "SEMUA" Then Return String.Empty

        ' Allowed transaction types are intentionally restricted to values exposed by the UI.
        If normalizedType <> "JUAL" AndAlso normalizedType <> "BELI" Then
            Debug.WriteLine("[UserSession] Invalid AllowedTransType detected; failing closed.")
            Return " AND 1 = 0 "
        End If

        Dim safeAlias As String = If(tableAlias, String.Empty).Trim()
        If safeAlias <> String.Empty AndAlso Not System.Text.RegularExpressions.Regex.IsMatch(safeAlias, "^[A-Za-z_][A-Za-z0-9_]*$") Then
            Throw New ArgumentException("Invalid SQL table alias.", NameOf(tableAlias))
        End If

        If safeAlias = String.Empty Then
            Return $" AND TransType = '{normalizedType}' "
        End If

        Return $" AND {safeAlias}.TransType = '{normalizedType}' "
    End Function

    ''' <summary>
    ''' Cek apakah user melihat semua TransType
    ''' </summary>
    Public Shared Function CanViewAllTransType() As Boolean
        Return String.IsNullOrEmpty(AllowedTransType) OrElse
               AllowedTransType.Equals("SEMUA", StringComparison.OrdinalIgnoreCase)
    End Function

    ' =============================================
    ' HELPER - GET INFO STRING
    ' =============================================
    Public Shared Function GetUserInfo() As String
        If Not IsLoggedIn() Then Return "Tidak ada user login"
        Return NamaLengkap & " (" & Role & ")"
    End Function

    Public Shared Function GetLoginInfo() As String
        If Not IsLoggedIn() Then Return "Belum Login"
        Return "👤 " & NamaLengkap & " | 🔑 " & Role & " | 🕐 " & LastLogin.ToString("HH:mm")
    End Function

End Class
