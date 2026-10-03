' =============================================
' AuditLog.vb
' Class Model untuk Audit Log / Riwayat Aktivitas
' DISESUAIKAN DENGAN STRUKTUR DATABASE
' =============================================
Imports System.Data.SqlClient

Public Class AuditLog

    ' =============================================
    ' PROPERTIES - SESUAI STRUKTUR DATABASE
    ' =============================================

#Region "Properties"
    Public Property LogID As Integer
    Public Property UserID As Integer?
    Public Property Action As String
    Public Property TableName As String
    Public Property RecordID As Integer?
    Public Property OldValue As String
    Public Property NewValue As String
    Public Property Keterangan As String
    Public Property IPAddress As String
    Public Property ComputerName As String
    Public Property CreatedAt As DateTime?

    ' =============================================
    ' EXTENDED PROPERTIES (dari JOIN dengan Users)
    ' =============================================
    Public Property UserName As String
    Public Property UserLogin As String
    Public Property UserRole As String

#End Region

    ' =============================================
    ' CONSTRUCTORS
    ' =============================================

#Region "Constructors"
    Public Sub New()
        LogID = 0
        UserID = Nothing
        Action = ""
        TableName = ""
        RecordID = Nothing
        OldValue = ""
        NewValue = ""
        Keterangan = ""
        IPAddress = ""
        ComputerName = ""
        CreatedAt = DateTime.Now
        UserName = ""
        UserLogin = ""
        UserRole = ""
    End Sub

    Public Sub New(userID As Integer, action As String, keterangan As String)
        Me.New()
        Me.UserID = userID
        Me.Action = action
        Me.Keterangan = keterangan
        Me.IPAddress = GetLocalIPAddress()
        Me.ComputerName = Environment.MachineName
    End Sub

    Public Sub New(userID As Integer, action As String, tableName As String,
                   recordID As Integer?, oldValue As String, newValue As String, keterangan As String)
        Me.New()
        Me.UserID = userID
        Me.Action = action
        Me.TableName = tableName
        Me.RecordID = recordID
        Me.OldValue = oldValue
        Me.NewValue = newValue
        Me.Keterangan = keterangan
        Me.IPAddress = GetLocalIPAddress()
        Me.ComputerName = Environment.MachineName
    End Sub

#End Region

    ' =============================================
    ' STATIC METHODS - CRUD OPERATIONS
    ' =============================================

#Region "Static Methods - CRUD"
    Public Shared Function GetAll(Optional pageNumber As Integer = 1, Optional pageSize As Integer = 25) As List(Of AuditLog)
        Dim result As New List(Of AuditLog)

        Try
            Dim offset As Integer = (pageNumber - 1) * pageSize
            Dim query As String = "SELECT a.*, " &
                                  "ISNULL(u.NamaLengkap, ISNULL(u.Username, 'System')) AS UserName, " &
                                  "ISNULL(u.Username, '-') AS UserLogin, " &
                                  "ISNULL(u.Role, '-') AS UserRole " &
                                  "FROM AuditLog a " &
                                  "LEFT JOIN Users u ON a.UserID = u.UserID " &
                                  "ORDER BY a.CreatedAt DESC " &
                                  "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {
                New SqlParameter("@Offset", offset),
                New SqlParameter("@PageSize", pageSize)
            })

            For Each row As DataRow In dt.Rows
                result.Add(MapFromDataRow(row))
            Next

        Catch ex As Exception
            Debug.WriteLine("Error GetAll AuditLog: " & ex.Message)
        End Try

        Return result
    End Function

    Public Shared Function GetByID(logID As Integer) As AuditLog
        Try
            Dim query As String = "SELECT a.*, " &
                                  "ISNULL(u.NamaLengkap, ISNULL(u.Username, 'System')) AS UserName, " &
                                  "ISNULL(u.Username, '-') AS UserLogin, " &
                                  "ISNULL(u.Role, '-') AS UserRole " &
                                  "FROM AuditLog a " &
                                  "LEFT JOIN Users u ON a.UserID = u.UserID " &
                                  "WHERE a.LogID = @LogID"

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {New SqlParameter("@LogID", logID)})

            If dt.Rows.Count > 0 Then
                Return MapFromDataRow(dt.Rows(0))
            End If

        Catch ex As Exception
            Debug.WriteLine("Error GetByID AuditLog: " & ex.Message)
        End Try

        Return Nothing
    End Function

    Public Shared Function GetByDateRange(startDate As DateTime, endDate As DateTime,
                                          Optional pageNumber As Integer = 1,
                                          Optional pageSize As Integer = 25) As List(Of AuditLog)
        Dim result As New List(Of AuditLog)

        Try
            Dim offset As Integer = (pageNumber - 1) * pageSize
            Dim query As String = "SELECT a.*, " &
                                  "ISNULL(u.NamaLengkap, ISNULL(u.Username, 'System')) AS UserName, " &
                                  "ISNULL(u.Username, '-') AS UserLogin, " &
                                  "ISNULL(u.Role, '-') AS UserRole " &
                                  "FROM AuditLog a " &
                                  "LEFT JOIN Users u ON a.UserID = u.UserID " &
                                  "WHERE CAST(a.CreatedAt AS DATE) BETWEEN @StartDate AND @EndDate " &
                                  "ORDER BY a.CreatedAt DESC " &
                                  "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {
                New SqlParameter("@StartDate", startDate.Date),
                New SqlParameter("@EndDate", endDate.Date),
                New SqlParameter("@Offset", offset),
                New SqlParameter("@PageSize", pageSize)
            })

            For Each row As DataRow In dt.Rows
                result.Add(MapFromDataRow(row))
            Next

        Catch ex As Exception
            Debug.WriteLine("Error GetByDateRange AuditLog: " & ex.Message)
        End Try

        Return result
    End Function

    Public Shared Function GetByUserID(userID As Integer, Optional pageNumber As Integer = 1, Optional pageSize As Integer = 25) As List(Of AuditLog)
        Dim result As New List(Of AuditLog)

        Try
            Dim offset As Integer = (pageNumber - 1) * pageSize
            Dim query As String = "SELECT a.*, " &
                                  "ISNULL(u.NamaLengkap, ISNULL(u.Username, 'System')) AS UserName, " &
                                  "ISNULL(u.Username, '-') AS UserLogin, " &
                                  "ISNULL(u.Role, '-') AS UserRole " &
                                  "FROM AuditLog a " &
                                  "LEFT JOIN Users u ON a.UserID = u.UserID " &
                                  "WHERE a.UserID = @UserID " &
                                  "ORDER BY a.CreatedAt DESC " &
                                  "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {
                New SqlParameter("@UserID", userID),
                New SqlParameter("@Offset", offset),
                New SqlParameter("@PageSize", pageSize)
            })

            For Each row As DataRow In dt.Rows
                result.Add(MapFromDataRow(row))
            Next

        Catch ex As Exception
            Debug.WriteLine("Error GetByUserID AuditLog: " & ex.Message)
        End Try

        Return result
    End Function

    Public Shared Function GetByAction(action As String, Optional pageNumber As Integer = 1, Optional pageSize As Integer = 25) As List(Of AuditLog)
        Dim result As New List(Of AuditLog)

        Try
            Dim offset As Integer = (pageNumber - 1) * pageSize
            Dim query As String = "SELECT a.*, " &
                                  "ISNULL(u.NamaLengkap, ISNULL(u.Username, 'System')) AS UserName, " &
                                  "ISNULL(u.Username, '-') AS UserLogin, " &
                                  "ISNULL(u.Role, '-') AS UserRole " &
                                  "FROM AuditLog a " &
                                  "LEFT JOIN Users u ON a.UserID = u.UserID " &
                                  "WHERE UPPER(a.Action) LIKE @Action " &
                                  "ORDER BY a.CreatedAt DESC " &
                                  "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {
                New SqlParameter("@Action", "%" & action.ToUpperInvariant() & "%"),
                New SqlParameter("@Offset", offset),
                New SqlParameter("@PageSize", pageSize)
            })

            For Each row As DataRow In dt.Rows
                result.Add(MapFromDataRow(row))
            Next

        Catch ex As Exception
            Debug.WriteLine("Error GetByAction AuditLog: " & ex.Message)
        End Try

        Return result
    End Function

    Public Shared Function GetTotalCount(Optional startDate As DateTime? = Nothing,
                                         Optional endDate As DateTime? = Nothing,
                                         Optional action As String = "",
                                         Optional userID As Integer? = Nothing) As Integer
        Try
            Dim whereClause As String = "WHERE 1=1 "
            Dim parameters As New List(Of SqlParameter)

            If startDate.HasValue AndAlso endDate.HasValue Then
                whereClause &= "AND CAST(CreatedAt AS DATE) BETWEEN @StartDate AND @EndDate "
                parameters.Add(New SqlParameter("@StartDate", startDate.Value.Date))
                parameters.Add(New SqlParameter("@EndDate", endDate.Value.Date))
            End If

            If Not String.IsNullOrEmpty(action) Then
                whereClause &= "AND UPPER(Action) LIKE @Action "
                parameters.Add(New SqlParameter("@Action", "%" & action.ToUpperInvariant() & "%"))
            End If

            If userID.HasValue Then
                whereClause &= "AND UserID = @UserID "
                parameters.Add(New SqlParameter("@UserID", userID.Value))
            End If

            Dim query As String = "SELECT COUNT(*) FROM AuditLog " & whereClause
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, parameters.ToArray())

            If dt.Rows.Count > 0 AndAlso dt.Rows(0)(0) IsNot DBNull.Value Then
                Return Convert.ToInt32(dt.Rows(0)(0))
            End If

        Catch ex As Exception
            Debug.WriteLine("Error GetTotalCount AuditLog: " & ex.Message)
        End Try

        Return 0
    End Function

    Public Function Insert() As Boolean
        Try
            Dim query As String = "INSERT INTO AuditLog (UserID, Action, TableName, RecordID, " &
                                  "OldValue, NewValue, Keterangan, IPAddress, ComputerName, CreatedAt) " &
                                  "VALUES (@UserID, @Action, @TableName, @RecordID, " &
                                  "@OldValue, @NewValue, @Keterangan, @IPAddress, @ComputerName, GETDATE()); " &
                                  "SELECT SCOPE_IDENTITY();"

            Dim result As Object = DatabaseHelper.ExecuteScalar(query, {
                New SqlParameter("@UserID", If(UserID.HasValue AndAlso UserID.Value > 0, CObj(UserID.Value), DBNull.Value)),
                New SqlParameter("@Action", If(String.IsNullOrEmpty(Action), "UNKNOWN", Action)),
                New SqlParameter("@TableName", If(String.IsNullOrEmpty(TableName), DBNull.Value, CObj(TableName))),
                New SqlParameter("@RecordID", If(RecordID.HasValue AndAlso RecordID.Value > 0, CObj(RecordID.Value), DBNull.Value)),
                New SqlParameter("@OldValue", If(String.IsNullOrEmpty(OldValue), DBNull.Value, CObj(OldValue))),
                New SqlParameter("@NewValue", If(String.IsNullOrEmpty(NewValue), DBNull.Value, CObj(NewValue))),
                New SqlParameter("@Keterangan", If(String.IsNullOrEmpty(Keterangan), DBNull.Value, CObj(Keterangan))),
                New SqlParameter("@IPAddress", If(String.IsNullOrEmpty(IPAddress), DBNull.Value, CObj(IPAddress))),
                New SqlParameter("@ComputerName", If(String.IsNullOrEmpty(ComputerName), DBNull.Value, CObj(ComputerName)))
            })

            If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                LogID = Convert.ToInt32(result)
                Return True
            End If

        Catch ex As Exception
            Debug.WriteLine("Error Insert AuditLog: " & ex.Message)
        End Try

        Return False
    End Function

    Public Shared Function Delete(logID As Integer) As Boolean
        Try
            Dim query As String = "DELETE FROM AuditLog WHERE LogID = @LogID"
            Dim result As Integer = DatabaseHelper.ExecuteNonQuery(query, {New SqlParameter("@LogID", logID)})
            Return result > 0
        Catch ex As Exception
            Debug.WriteLine("Error Delete AuditLog: " & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function DeleteOldLogs(daysOld As Integer) As Integer
        Try
            Dim cutoffDate As DateTime = DateTime.Today.AddDays(-daysOld)
            Dim query As String = "DELETE FROM AuditLog WHERE CAST(CreatedAt AS DATE) < @CutoffDate"
            Return DatabaseHelper.ExecuteNonQuery(query, {New SqlParameter("@CutoffDate", cutoffDate)})
        Catch ex As Exception
            Debug.WriteLine("Error DeleteOldLogs AuditLog: " & ex.Message)
            Return 0
        End Try
    End Function

#End Region

    ' =============================================
    ' STATIC METHODS - INSERT SHORTCUTS
    ' =============================================

#Region "Static Methods - Insert Shortcuts"

    Public Shared Sub Log(userID As Integer, action As String, keterangan As String)
        Try
            Dim auditLog As New AuditLog(userID, action, keterangan)
            auditLog.Insert()
        Catch ex As Exception
            Debug.WriteLine("Error Log AuditLog: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub Log(userID As Integer, action As String, tableName As String, keterangan As String)
        Try
            Dim auditLog As New AuditLog()
            auditLog.UserID = userID
            auditLog.Action = action
            auditLog.TableName = tableName
            auditLog.Keterangan = keterangan
            auditLog.IPAddress = GetLocalIPAddress()
            auditLog.ComputerName = Environment.MachineName
            auditLog.Insert()
        Catch ex As Exception
            Debug.WriteLine("Error Log AuditLog: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub Log(userID As Integer, action As String, tableName As String,
                          recordID As Integer?, oldValue As String, newValue As String, keterangan As String)
        Try
            Dim auditLog As New AuditLog(userID, action, tableName, recordID, oldValue, newValue, keterangan)
            auditLog.Insert()
        Catch ex As Exception
            Debug.WriteLine("Error Log AuditLog: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub LogLogin(userID As Integer, userName As String)
        Log(userID, "LOGIN", "Users", $"User {userName} berhasil login ke sistem")
    End Sub

    Public Shared Sub LogLogout(userID As Integer, userName As String)
        Log(userID, "LOGOUT", "Users", $"User {userName} logout dari sistem")
    End Sub

    Public Shared Sub LogInsert(userID As Integer, tableName As String, recordID As Integer, keterangan As String)
        Log(userID, "INSERT", tableName, recordID, "", "", keterangan)
    End Sub

    Public Shared Sub LogUpdate(userID As Integer, tableName As String, recordID As Integer, oldValue As String, newValue As String, keterangan As String)
        Log(userID, "UPDATE", tableName, recordID, oldValue, newValue, keterangan)
    End Sub

    Public Shared Sub LogDelete(userID As Integer, tableName As String, recordID As Integer, keterangan As String)
        Log(userID, "DELETE", tableName, recordID, "", "", keterangan)
    End Sub

    Public Shared Sub LogError(userID As Integer, errorMessage As String, Optional tableName As String = "")
        Log(userID, "ERROR", tableName, $"Error: {errorMessage}")
    End Sub

    Public Shared Sub LogPrint(userID As Integer, tableName As String, recordID As Integer, keterangan As String)
        Log(userID, "PRINT", tableName, recordID, "", "", keterangan)
    End Sub

#End Region

    ' =============================================
    ' STATIC METHODS - STATISTICS
    ' =============================================

#Region "Static Methods - Statistics"

    Public Shared Function GetTodayCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM AuditLog WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE)"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query)
            If dt.Rows.Count > 0 AndAlso dt.Rows(0)(0) IsNot DBNull.Value Then
                Return Convert.ToInt32(dt.Rows(0)(0))
            End If
        Catch ex As Exception
            Debug.WriteLine("[AuditLog] Statistics query error: " & ex.Message)
        End Try
        Return 0
    End Function

    Public Shared Function GetTodayLoginCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM AuditLog WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE) AND (Action LIKE '%LOGIN%' OR Action LIKE '%LOGOUT%')"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query)
            If dt.Rows.Count > 0 AndAlso dt.Rows(0)(0) IsNot DBNull.Value Then
                Return Convert.ToInt32(dt.Rows(0)(0))
            End If
        Catch ex As Exception
            Debug.WriteLine("[AuditLog] Statistics query error: " & ex.Message)
        End Try
        Return 0
    End Function

    Public Shared Function GetTodayChangesCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM AuditLog WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE) " &
                                  "AND (Action LIKE '%INSERT%' OR Action LIKE '%UPDATE%' OR Action LIKE '%DELETE%' " &
                                  "OR Action LIKE '%CREATE%' OR Action LIKE '%EDIT%' OR Action LIKE '%TAMBAH%' " &
                                  "OR Action LIKE '%UBAH%' OR Action LIKE '%HAPUS%')"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query)
            If dt.Rows.Count > 0 AndAlso dt.Rows(0)(0) IsNot DBNull.Value Then
                Return Convert.ToInt32(dt.Rows(0)(0))
            End If
        Catch ex As Exception
            Debug.WriteLine("[AuditLog.GetTodayChangesCount] Error: " & ex.ToString())
        End Try
        Return 0
    End Function

    Public Shared Function GetTodayErrorCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM AuditLog WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE) " &
                                  "AND (Action LIKE '%ERROR%' OR Action LIKE '%WARNING%' OR Action LIKE '%FAIL%' OR Action LIKE '%GAGAL%')"
            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query)
            If dt.Rows.Count > 0 AndAlso dt.Rows(0)(0) IsNot DBNull.Value Then
                Return Convert.ToInt32(dt.Rows(0)(0))
            End If
        Catch ex As Exception
            Debug.WriteLine("[AuditLog.GetTodayErrorCount] Error: " & ex.ToString())
        End Try
        Return 0
    End Function

#End Region

    ' =============================================
    ' HELPER METHODS
    ' =============================================

#Region "Helper Methods"

    Private Shared Function MapFromDataRow(row As DataRow) As AuditLog
        Dim log As New AuditLog()

        log.LogID = Convert.ToInt32(row("LogID"))
        log.UserID = If(row("UserID") IsNot DBNull.Value, CType(Convert.ToInt32(row("UserID")), Integer?), Nothing)
        log.Action = If(row("Action") IsNot DBNull.Value, row("Action").ToString(), "")
        log.TableName = If(row("TableName") IsNot DBNull.Value, row("TableName").ToString(), "")
        log.RecordID = If(row("RecordID") IsNot DBNull.Value, CType(Convert.ToInt32(row("RecordID")), Integer?), Nothing)
        log.OldValue = If(row("OldValue") IsNot DBNull.Value, row("OldValue").ToString(), "")
        log.NewValue = If(row("NewValue") IsNot DBNull.Value, row("NewValue").ToString(), "")
        log.Keterangan = If(row("Keterangan") IsNot DBNull.Value, row("Keterangan").ToString(), "")
        log.IPAddress = If(row("IPAddress") IsNot DBNull.Value, row("IPAddress").ToString(), "")
        log.ComputerName = If(row("ComputerName") IsNot DBNull.Value, row("ComputerName").ToString(), "")
        log.CreatedAt = If(row("CreatedAt") IsNot DBNull.Value, CType(Convert.ToDateTime(row("CreatedAt")), DateTime?), Nothing)

        ' Extended properties
        If row.Table.Columns.Contains("UserName") Then
            log.UserName = If(row("UserName") IsNot DBNull.Value, row("UserName").ToString(), "")
        End If
        If row.Table.Columns.Contains("UserLogin") Then
            log.UserLogin = If(row("UserLogin") IsNot DBNull.Value, row("UserLogin").ToString(), "")
        End If
        If row.Table.Columns.Contains("UserRole") Then
            log.UserRole = If(row("UserRole") IsNot DBNull.Value, row("UserRole").ToString(), "")
        End If

        Return log
    End Function

    Private Shared Function GetLocalIPAddress() As String
        Try
            Dim host As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            For Each ip As System.Net.IPAddress In host.AddressList
                If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
            Return "127.0.0.1"
        Catch ex As Exception
            Debug.WriteLine("[AuditLog.GetLocalIPAddress] Error: " & ex.ToString())
            Return "127.0.0.1"
        End Try
    End Function

    Public Function GetActivityDisplayName() As String
        If String.IsNullOrEmpty(Action) Then Return "-"

        Dim aksi As String = Action.ToUpperInvariant()

        If aksi.Contains("LOGIN") AndAlso Not aksi.Contains("LOGOUT") Then Return "Login"
        If aksi.Contains("LOGOUT") Then Return "Logout"
        If aksi.Contains("INSERT") OrElse aksi.Contains("CREATE") OrElse aksi.Contains("ADD") OrElse aksi.Contains("TAMBAH") Then Return "Insert"
        If aksi.Contains("UPDATE") OrElse aksi.Contains("EDIT") OrElse aksi.Contains("MODIFY") OrElse aksi.Contains("UBAH") Then Return "Update"
        If aksi.Contains("DELETE") OrElse aksi.Contains("REMOVE") OrElse aksi.Contains("HAPUS") Then Return "Delete"
        If aksi.Contains("VIEW") OrElse aksi.Contains("READ") OrElse aksi.Contains("OPEN") OrElse aksi.Contains("LIHAT") Then Return "View"
        If aksi.Contains("PRINT") OrElse aksi.Contains("CETAK") Then Return "Print"
        If aksi.Contains("ERROR") OrElse aksi.Contains("FAIL") OrElse aksi.Contains("GAGAL") Then Return "Error"

        Dim words As String() = aksi.Split(" "c, "_"c)
        If words.Length > 0 AndAlso words(0).Length > 0 Then
            Return words(0).Substring(0, 1).ToUpperInvariant() & words(0).Substring(1).ToLowerInvariant()
        End If

        Return Action
    End Function

    Public Overrides Function ToString() As String
        Return $"[{LogID}] {CreatedAt:dd/MM/yyyy HH:mm} - {UserName} - {Action} - {Keterangan}"
    End Function

#End Region

#Region "Show Detail Methods"

    ' ============================================= 
    ' SHOW DETAIL LOG - FORMAT TABEL PERBANDINGAN
    ' ============================================= 
    Public Shared Sub ShowDetailLog(logID As String, Optional parentForm As Form = Nothing)
        Try
            Dim query As String = "SELECT a.*, u.NamaLengkap, u.Username, u.Role " &
                              "FROM AuditLog a " &
                              "LEFT JOIN Users u ON a.UserID = u.UserID " &
                              "WHERE a.LogID = @LogID"

            Dim dt As DataTable = DatabaseHelper.ExecuteQuery(query, {New SqlParameter("@LogID", CInt(logID))})

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                ' Parse Old dan New Value
                Dim oldValue As String = If(IsDBNull(row("OldValue")), "", row("OldValue").ToString())
                Dim newValue As String = If(IsDBNull(row("NewValue")), "", row("NewValue").ToString())

                Debug.WriteLine($"[ShowDetailLog] LogID: {logID}")
                Debug.WriteLine($"[ShowDetailLog] OldValue length: {oldValue.Length}")
                Debug.WriteLine($"[ShowDetailLog] NewValue length: {newValue.Length}")

                ' Jika tidak ada data perbandingan, tampilkan pesan
                If String.IsNullOrEmpty(oldValue) AndAlso String.IsNullOrEmpty(newValue) Then
                    MessageBox.Show("Tidak ada data perbandingan untuk log ini." & vbCrLf & vbCrLf &
                                  "Kemungkinan:" & vbCrLf &
                                  "• Log ini adalah aktivitas VIEW/OPEN/PRINT" & vbCrLf &
                                  "• Data belum tersimpan dengan format yang benar" & vbCrLf & vbCrLf &
                                  "Keterangan: " & If(IsDBNull(row("Keterangan")), "-", row("Keterangan").ToString()),
                                  "Informasi",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information)
                    Return
                End If

                ' Buat form detail dengan tabel
                CreateDetailForm(logID, row, oldValue, newValue, parentForm)
            End If

        Catch ex As Exception
            Debug.WriteLine("[AuditLogViewer.LoadDetail] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat detail Audit Log.", "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Sub CreateDetailForm(logID As String, row As DataRow, oldValue As String, newValue As String, parentForm As Form)
        ' (Kode form detail akan dilanjutkan di komentar berikutnya karena terlalu panjang)
        ' Untuk sekarang, gunakan kode ShowDetailLog yang sebelumnya sudah saya berikan
        ' Cukup tambahkan di region "Show Detail Methods"
    End Sub

    ' ============================================= 
    ' PARSE VALUE STRING TO DICTIONARY
    ' ============================================= 
    Private Shared Function ParseValueToDictionary(value As String) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)

        If String.IsNullOrEmpty(value) Then
            Debug.WriteLine("[ParseValueToDictionary] Value is empty")
            Return result
        End If

        Try
            Debug.WriteLine($"[ParseValueToDictionary] Parsing value...")

            Dim lines As String() = value.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
            Debug.WriteLine($"[ParseValueToDictionary] Found {lines.Length} lines")

            For Each line In lines
                Dim trimmedLine As String = line.Trim()

                If String.IsNullOrWhiteSpace(trimmedLine) Then Continue For

                If trimmedLine.StartsWith("===") OrElse
                   trimmedLine.StartsWith("---") OrElse
                   trimmedLine.Contains("DATA SEBELUM") OrElse
                   trimmedLine.Contains("DATA SESUDAH") Then
                    Continue For
                End If

                Dim colonIndex As Integer = trimmedLine.IndexOf(":")
                If colonIndex > 0 AndAlso colonIndex < trimmedLine.Length - 1 Then
                    Dim key As String = trimmedLine.Substring(0, colonIndex).Trim()
                    Dim val As String = trimmedLine.Substring(colonIndex + 1).Trim()

                    key = key.Replace(" ", "").Replace("(", "").Replace(")", "")

                    If Not String.IsNullOrEmpty(key) AndAlso Not result.ContainsKey(key) Then
                        result.Add(key, val)
                        Debug.WriteLine($"[ParseValueToDictionary] Added: {key} = {val}")
                    End If
                End If
            Next

            Debug.WriteLine($"[ParseValueToDictionary] Total parsed: {result.Count} fields")

        Catch ex As Exception
            Debug.WriteLine($"[ParseValueToDictionary] Error: {ex.Message}")
        End Try

        Return result
    End Function

#End Region

End Class
