' =============================================
' SerialPortHelper.vb
' VERSI FINAL + SCALE TYPE SUPPORT
' Support: GSC (SGW-3015S, SGW-3015, GST-9600)
'          SONIC (SPS-2000, SPS-3000, T7E)
'          YAOHUA (A9, A28E, A12E)
' Fix: Parser per tipe, ProcessBuffer, DataReceived
' =============================================
Imports System.IO.Ports
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Globalization

Public Class SerialPortHelper

    ' =============================================
    ' SCALE BRAND SUPPORT
    ' =============================================
    Public Enum ScaleBrand
        GSC
        Sonic
        Yaohua
    End Enum

    ' =============================================
    ' SCALE TYPE SUPPORT (TIPE INDIKATOR)
    ' =============================================
    Public Enum ScaleType
        AutoDetect      ' Coba semua parser
        ' === GSC ===
        GSC_SGW3015S    ' Format: ST,GS,+ 12345 kg (continuous)
        GSC_SGW3015     ' Format: = 12345 atau +12345
        GSC_GST9600     ' Format: +12345.0 kg
        GSC_GST9800     ' Format: mirip GST9600
        GSC_SGW3000     ' Format: mirip SGW3015
        ' === SONIC ===
        Sonic_SPS2000   ' Format: WT: 12345 KG
        Sonic_SPS3000   ' Format: ST  +12345 KG
        Sonic_T7E       ' Format: bervariasi
        Sonic_SPS1000   ' Format: mirip SPS2000
        ' === YAOHUA ===
        Yaohua_A9       ' Format: wn12345
        Yaohua_A28E     ' Format: ST,GS,+ 12345.0 kg (mirip GSC SGW-3015S!)
        Yaohua_A12E     ' Format: mirip A28E
        Yaohua_A7       ' Format: mirip A9
        Yaohua_T7       ' Format: bervariasi
    End Enum

    Private Shared _scaleBrand As ScaleBrand = ScaleBrand.GSC
    Private Shared _scaleType As ScaleType = ScaleType.AutoDetect

    Public Shared ReadOnly Property CurrentScaleBrand As ScaleBrand
        Get
            Return _scaleBrand
        End Get
    End Property

    Public Shared ReadOnly Property CurrentScaleType As ScaleType
        Get
            Return _scaleType
        End Get
    End Property

    ' =============================================
    ' REGEX PATTERNS - SONIC
    ' =============================================
    Private Shared ReadOnly SonicPatterns As String() = {
        "WT\s*[:=]?\s*(?<num>[-\+]?\d+(?:[.,]\d+)?)\s*KG",
        "\bS[T]?[, ]*\s*(?<num>[-\+]?\d+(?:[.,]\d+)?)\s*KG\b",
        "\bGROSS\s*[:=]?\s*(?<num>[-\+]?\d+(?:[.,]\d+)?)\s*KG\b",
        "\bNETT?O?\s*[:=]?\s*(?<num>[-\+]?\d+(?:[.,]\d+)?)\s*KG\b",
        "\b(?<num>[-\+]?\d{1,3}(?:[.,]\d{3})+(?:[.,]\d+)?)\s*KG\b",
        "\b(?<num>[-\+]?\d+(?:[.,]\d+)?)\s*(?:KG|KGS)\b",
        "\b(?<num>[-\+]?\d+(?:[.,]\d+)?)\b"
    }

    ' =============================================
    ' REGEX PATTERNS - YAOHUA
    ' =============================================
    Private Shared ReadOnly YaohuaPatterns As String() = {
        "ST,GS,\s*[+\-]\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:kg|KG|t|T)?",
        "ST,NT,\s*[+\-]\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:kg|KG|t|T)?",
        "US,GS,\s*[+\-]\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:kg|KG|t|T)?",
        "OL,GS,\s*[+\-]\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:kg|KG|t|T)?",
        "[+\-]\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:kg|KG)"
    }

    ' =============================================
    ' VARIABEL PRIVATE
    ' =============================================
    Private Shared WithEvents _serialPort As SerialPort
    Private Shared _isConnected As Boolean = False
    Private Shared _dataBuffer As String = ""
    Private Shared _currentBaudRate As Integer = 9600
    Private Shared ReadOnly PARSE_ERROR As Decimal = Decimal.MinValue

    ' Lock object untuk thread safety
    Private Shared ReadOnly _lockObject As New Object()

    ' Timestamp terakhir data valid diterima (untuk monitoring)
    Private Shared _lastValidDataTime As DateTime = DateTime.MinValue

    ' =============================================
    ' EVENTS
    ' =============================================
    Public Shared Event WeightReceived(ByVal weight As Decimal, ByVal rawData As String)
    Public Shared Event ConnectionStatusChanged(ByVal isConnected As Boolean, ByVal message As String)
    Public Shared Event ErrorOccurred(ByVal errorMessage As String)

    ' =============================================
    ' PROPERTIES
    ' =============================================
    Public Shared ReadOnly Property IsConnected As Boolean
        Get
            Return _isConnected AndAlso _serialPort IsNot Nothing AndAlso _serialPort.IsOpen
        End Get
    End Property

    Public Shared ReadOnly Property CurrentPortName As String
        Get
            If _serialPort IsNot Nothing Then Return _serialPort.PortName
            Return ""
        End Get
    End Property

    Public Shared ReadOnly Property CurrentBaudRate As Integer
        Get
            Return _currentBaudRate
        End Get
    End Property

    Public Shared ReadOnly Property LastValidDataTime As DateTime
        Get
            Return _lastValidDataTime
        End Get
    End Property

    ' =============================================
    ' GET AVAILABLE PORTS
    ' =============================================
    Public Shared Function GetAvailablePorts() As String()
        Try
            Return SerialPort.GetPortNames()
        Catch ex As Exception
            Return New String() {}
        End Try
    End Function

    ' =============================================
    ' CALCULATE TIMEOUT BASED ON BAUD RATE
    ' =============================================
    Private Shared Function CalculateReadTimeout(baudRate As Integer) As Integer
        Select Case baudRate
            Case Is <= 2400
                Return 5000
            Case Is <= 4800
                Return 3000
            Case Is <= 9600
                Return 2000
            Case Is <= 19200
                Return 1500
            Case Else
                Return 1000
        End Select
    End Function

    ' =============================================
    ' CALCULATE READ DELAY BASED ON BAUD RATE
    ' =============================================
    Private Shared Function CalculateReadDelay(baudRate As Integer) As Integer
        Select Case baudRate
            Case Is <= 2400
                Return 800
            Case Is <= 4800
                Return 500
            Case Is <= 9600
                Return 300
            Case Is <= 19200
                Return 200
            Case Else
                Return 100
        End Select
    End Function

    ' =============================================
    ' CALCULATE RETRY COUNT BASED ON BAUD RATE
    ' =============================================
    Private Shared Function CalculateRetryCount(baudRate As Integer) As Integer
        Select Case baudRate
            Case Is <= 2400
                Return 5
            Case Is <= 4800
                Return 4
            Case Is <= 9600
                Return 3
            Case Else
                Return 2
        End Select
    End Function

    ' =============================================
    ' MAP STRING KE SCALE TYPE
    ' Dari setting database ke enum
    ' =============================================
    Private Shared Function MapScaleType(brand As String, typeStr As String) As ScaleType
        If String.IsNullOrWhiteSpace(typeStr) Then Return ScaleType.AutoDetect

        ' Gabungkan brand + type dan bersihkan
        Dim combined As String = (brand & "_" & typeStr).ToUpperInvariant()
        combined = combined.Replace("-", "").Replace(" ", "").Replace(".", "")

        Debug.WriteLine($"[MapScaleType] Combined key: [{combined}]")

        Select Case True
            ' === GSC ===
            Case combined.Contains("SGW3015S")
                Return ScaleType.GSC_SGW3015S
            Case combined.Contains("SGW3015") AndAlso Not combined.Contains("SGW3015S")
                Return ScaleType.GSC_SGW3015
            Case combined.Contains("GST9600") OrElse combined.Contains("9600") AndAlso combined.Contains("GSC")
                Return ScaleType.GSC_GST9600
            Case combined.Contains("GST9800")
                Return ScaleType.GSC_GST9800
            Case combined.Contains("SGW3000")
                Return ScaleType.GSC_SGW3000

            ' === SONIC ===
            Case combined.Contains("SPS2000")
                Return ScaleType.Sonic_SPS2000
            Case combined.Contains("SPS3000")
                Return ScaleType.Sonic_SPS3000
            Case combined.Contains("SPS1000")
                Return ScaleType.Sonic_SPS1000
            Case combined.Contains("T7E") AndAlso combined.Contains("SONIC")
                Return ScaleType.Sonic_T7E

            ' === YAOHUA ===
            Case combined.Contains("A9") AndAlso combined.Contains("YAOHUA")
                Return ScaleType.Yaohua_A9
            Case combined.Contains("A28E") OrElse combined.Contains("A28")
                Return ScaleType.Yaohua_A28E
            Case combined.Contains("A12E") OrElse combined.Contains("A12")
                Return ScaleType.Yaohua_A12E
            Case combined.Contains("A7") AndAlso combined.Contains("YAOHUA") AndAlso Not combined.Contains("T7")
                Return ScaleType.Yaohua_A7
            Case combined.Contains("T7") AndAlso combined.Contains("YAOHUA")
                Return ScaleType.Yaohua_T7

            Case Else
                Debug.WriteLine($"[MapScaleType] No match found, using AutoDetect")
                Return ScaleType.AutoDetect
        End Select
    End Function

    ' =============================================
    ' CONNECT TO SERIAL PORT
    ' =============================================
    Public Shared Function Connect(Optional portName As String = "",
                                   Optional baudRate As Integer = 9600,
                                   Optional dataBits As Integer = 8,
                                   Optional stopBits As StopBits = StopBits.One,
                                   Optional parity As Parity = Parity.None) As Boolean
        Try
            SyncLock _lockObject
                ' === CLEANUP PORT LAMA ===
                Try
                    If _serialPort IsNot Nothing Then
                        RemoveHandler _serialPort.DataReceived, AddressOf DataReceivedHandler
                        RemoveHandler _serialPort.ErrorReceived, AddressOf ErrorReceivedHandler
                        If _serialPort.IsOpen Then
                            _serialPort.DiscardInBuffer()
                            _serialPort.DiscardOutBuffer()
                            _serialPort.Close()
                        End If
                        _serialPort.Dispose()
                        _serialPort = Nothing
                    End If
                Catch exClose As Exception
                    Debug.WriteLine($"[SerialPortHelper] Cleanup error (ignored): {exClose.Message}")
                End Try

                ' === AMBIL SETTING DARI DB JIKA TIDAK DIBERIKAN ===
                If String.IsNullOrEmpty(portName) Then
                    portName = GetSettingFromDB("COM_PORT", "COM3")
                    baudRate = CInt(GetSettingFromDB("BAUD_RATE", "9600"))
                End If

                ' === BACA MEREK INDIKATOR DARI DB ===
                Dim brandSetting As String = GetSettingFromDB("SCALE_BRAND", "GSC").Trim().ToUpperInvariant()
                Select Case brandSetting
                    Case "SONIC"
                        _scaleBrand = ScaleBrand.Sonic
                    Case "YAOHUA"
                        _scaleBrand = ScaleBrand.Yaohua
                    Case Else
                        _scaleBrand = ScaleBrand.GSC
                End Select

                ' === BACA TIPE INDIKATOR DARI DB ===
                Dim typeSetting As String = GetSettingFromDB("SCALE_TYPE", "").Trim()
                _scaleType = MapScaleType(brandSetting, typeSetting)

                Debug.WriteLine($"[SerialPortHelper] ========================================")
                Debug.WriteLine($"[SerialPortHelper] ScaleBrand aktif : {_scaleBrand}")
                Debug.WriteLine($"[SerialPortHelper] ScaleType aktif  : {_scaleType}")
                Debug.WriteLine($"[SerialPortHelper] DB SCALE_BRAND   : {brandSetting}")
                Debug.WriteLine($"[SerialPortHelper] DB SCALE_TYPE    : {typeSetting}")
                Debug.WriteLine($"[SerialPortHelper] Port             : {portName}")
                Debug.WriteLine($"[SerialPortHelper] BaudRate          : {baudRate}")
                Debug.WriteLine($"[SerialPortHelper] ========================================")

                ' Simpan baud rate
                _currentBaudRate = baudRate

                ' Cek apakah port tersedia
                Dim available = GetAvailablePorts()
                If Not available.Contains(portName) Then
                    Debug.WriteLine($"[SerialPortHelper] Port {portName} TIDAK DITEMUKAN! Available: {String.Join(", ", available)}")
                    RaiseEvent ConnectionStatusChanged(False, "Port " & portName & " tidak ditemukan!")
                    Return False
                End If

                ' Hitung timeout
                Dim readTimeout As Integer = CalculateReadTimeout(baudRate)
                Dim writeTimeout As Integer = Math.Max(readTimeout \ 2, 1000)

                ' === BUAT DAN KONFIGURASI SERIAL PORT ===
                _serialPort = New SerialPort(portName, baudRate, parity, dataBits, stopBits)
                _serialPort.Handshake = Handshake.None
                _serialPort.RtsEnable = True
                _serialPort.DtrEnable = True
                _serialPort.ReadTimeout = readTimeout
                _serialPort.WriteTimeout = writeTimeout
                _serialPort.Encoding = System.Text.Encoding.ASCII
                _serialPort.NewLine = vbCrLf
                _serialPort.ReadBufferSize = 4096
                _serialPort.WriteBufferSize = 2048
                _serialPort.ReceivedBytesThreshold = 1

                ' === PASANG EVENT HANDLER ===
                AddHandler _serialPort.DataReceived, AddressOf DataReceivedHandler
                AddHandler _serialPort.ErrorReceived, AddressOf ErrorReceivedHandler

                ' === BUKA PORT ===
                _serialPort.Open()

                ' Tunggu stabilisasi
                Dim stabilizeDelay As Integer = If(baudRate <= 4800, 500, 200)
                Threading.Thread.Sleep(stabilizeDelay)

                ' Clear buffer awal (buang data sampah)
                If _serialPort.BytesToRead > 0 Then
                    Dim junkData As String = _serialPort.ReadExisting()
                    Debug.WriteLine($"[SerialPortHelper] Cleared initial buffer: [{junkData.Trim()}] ({junkData.Length} chars)")
                End If

                _isConnected = True
                _dataBuffer = ""

                Dim timeoutInfo As String = $"(Timeout: {readTimeout}ms)"
                Dim typeInfo As String = If(_scaleType = ScaleType.AutoDetect, "Auto", _scaleType.ToString())
                RaiseEvent ConnectionStatusChanged(True, $"Terhubung ke {portName} @ {baudRate} baud {timeoutInfo} [{_scaleBrand} - {typeInfo}]")

                Debug.WriteLine($"[SerialPortHelper] ✓ CONNECTED to {portName} @ {baudRate} baud, Brand={_scaleBrand}, Type={_scaleType}")

                Return True
            End SyncLock

        Catch ex As UnauthorizedAccessException
            _isConnected = False
            RaiseEvent ErrorOccurred("Port " & portName & " sedang digunakan oleh aplikasi lain")
            RaiseEvent ConnectionStatusChanged(False, "Port sedang digunakan")
            Return False
        Catch ex As System.IO.IOException
            _isConnected = False
            RaiseEvent ErrorOccurred("Port " & portName & " tidak ditemukan")
            RaiseEvent ConnectionStatusChanged(False, "Port tidak ditemukan")
            Return False
        Catch ex As Exception
            _isConnected = False
            Debug.WriteLine("[SerialPortHelper.Connect] Error: " & ex.ToString())
            RaiseEvent ErrorOccurred("Gagal terhubung ke indikator timbangan.")
            RaiseEvent ConnectionStatusChanged(False, "Gagal koneksi")
            Return False
        End Try
    End Function

    ' =============================================
    ' DISCONNECT FROM SERIAL PORT
    ' =============================================
    Public Shared Sub Disconnect()
        Try
            SyncLock _lockObject
                If _serialPort IsNot Nothing Then
                    RemoveHandler _serialPort.DataReceived, AddressOf DataReceivedHandler
                    RemoveHandler _serialPort.ErrorReceived, AddressOf ErrorReceivedHandler

                    If _serialPort.IsOpen Then
                        _serialPort.DiscardInBuffer()
                        _serialPort.DiscardOutBuffer()
                        _serialPort.Close()
                    End If
                    _serialPort.Dispose()
                    _serialPort = Nothing
                End If
                _isConnected = False
                _dataBuffer = ""
                RaiseEvent ConnectionStatusChanged(False, "Terputus")
                Debug.WriteLine("[SerialPortHelper] ✓ Disconnected")
            End SyncLock
        Catch ex As Exception
            Debug.WriteLine("[SerialPortHelper.Disconnect] Error: " & ex.ToString())
            RaiseEvent ErrorOccurred("Gagal memutuskan koneksi indikator timbangan.")
        End Try
    End Sub

    ' =============================================
    ' DATA RECEIVED HANDLER (ASYNC)
    ' =============================================
    Private Shared Sub DataReceivedHandler(sender As Object, e As SerialDataReceivedEventArgs)
        Try
            SyncLock _lockObject
                If _serialPort Is Nothing OrElse Not _serialPort.IsOpen Then Return

                ' Delay berdasarkan baud rate
                Dim bufferDelay As Integer
                Select Case _currentBaudRate
                    Case Is <= 2400
                        bufferDelay = 100
                    Case Is <= 4800
                        bufferDelay = 50
                    Case Is <= 9600
                        bufferDelay = 20
                    Case Else
                        bufferDelay = 10
                End Select
                Threading.Thread.Sleep(bufferDelay)

                Dim bytesAvailable As Integer = _serialPort.BytesToRead

                If bytesAvailable > 0 Then
                    Dim data As String = _serialPort.ReadExisting()

                    ' === DETAILED DEBUG ===
                    Dim debugData As String = data.Replace(vbCr, "\r").Replace(vbLf, "\n")
                    Debug.WriteLine($"[DataReceived] Bytes={bytesAvailable}, Brand={_scaleBrand}, Type={_scaleType}, Data=[{debugData}]")

                    ' Log HEX untuk troubleshooting
                    Try
                        Dim hexBytes As String = BitConverter.ToString(System.Text.Encoding.ASCII.GetBytes(data))
                        Debug.WriteLine($"[DataReceived] HEX: {hexBytes}")
                    Catch exHex As Exception
                        Debug.WriteLine("[DataReceived] HEX logging error: " & exHex.Message)
                    End Try

                    If Not String.IsNullOrEmpty(data) Then
                        _dataBuffer &= data
                        ProcessBuffer()
                    End If
                End If
            End SyncLock
        Catch ex As Exception
            Debug.WriteLine("[DataReceived] ERROR: " & ex.ToString())
            RaiseEvent ErrorOccurred("Gagal membaca data dari indikator timbangan.")
        End Try
    End Sub

    ' =============================================
    ' ERROR RECEIVED HANDLER
    ' =============================================
    Private Shared Sub ErrorReceivedHandler(sender As Object, e As SerialErrorReceivedEventArgs)
        Debug.WriteLine($"[SerialPortHelper] Serial Error Event: {e.EventType}")
        RaiseEvent ErrorOccurred("Serial Error: " & e.EventType.ToString())
    End Sub

    ' =============================================
    ' PROCESS BUFFER
    ' Support: CR, LF, STX/ETX, dan fallback
    ' =============================================
    Private Shared Sub ProcessBuffer()
        Try
            Dim hasNewline As Boolean = _dataBuffer.Contains(vbCr) OrElse _dataBuffer.Contains(vbLf)
            Dim hasSTXETX As Boolean = _dataBuffer.Contains(Chr(2)) OrElse _dataBuffer.Contains(Chr(3))

            If hasNewline OrElse hasSTXETX Then
                Dim delimiters As Char() = {CChar(vbCr), CChar(vbLf), Chr(2), Chr(3)}
                Dim lines() As String = _dataBuffer.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)

                For Each line In lines
                    line = line.Trim()
                    If String.IsNullOrWhiteSpace(line) OrElse line.Length < 2 Then Continue For

                    Dim weight As Decimal = ParseWeight(line)

                    If weight <> PARSE_ERROR Then
                        Debug.WriteLine($"[ProcessBuffer] ✓ PARSED: [{line}] -> {weight:N1} KG")
                        _lastValidDataTime = DateTime.Now
                        RaiseEvent WeightReceived(weight, line)
                    Else
                        Debug.WriteLine($"[ProcessBuffer] ✗ FAILED: [{line}] -> PARSE_ERROR")
                    End If
                Next

                ' Simpan sisa buffer
                Dim lastDelimIdx As Integer = -1
                For Each d In delimiters
                    Dim idx As Integer = _dataBuffer.LastIndexOf(d)
                    If idx > lastDelimIdx Then lastDelimIdx = idx
                Next

                If lastDelimIdx >= 0 AndAlso lastDelimIdx < _dataBuffer.Length - 1 Then
                    _dataBuffer = _dataBuffer.Substring(lastDelimIdx + 1)
                Else
                    _dataBuffer = ""
                End If

            ElseIf _dataBuffer.Length > 30 Then
                ' === FALLBACK: Tidak ada terminator ===
                Debug.WriteLine($"[ProcessBuffer] FALLBACK MODE - Buffer({_dataBuffer.Length}): [{_dataBuffer.Trim()}]")

                Try
                    Dim hexStr As String = BitConverter.ToString(System.Text.Encoding.ASCII.GetBytes(_dataBuffer))
                    Debug.WriteLine($"[ProcessBuffer] FALLBACK HEX: {hexStr}")
                Catch exHex As Exception
                    Debug.WriteLine("[ProcessBuffer] HEX logging error: " & exHex.Message)
                End Try

                Dim weight As Decimal = ParseWeight(_dataBuffer)
                If weight <> PARSE_ERROR Then
                    Debug.WriteLine($"[ProcessBuffer] FALLBACK ✓ PARSED: {weight:N1} KG")
                    _lastValidDataTime = DateTime.Now
                    RaiseEvent WeightReceived(weight, _dataBuffer.Trim())
                Else
                    Debug.WriteLine($"[ProcessBuffer] FALLBACK ✗ PARSE_ERROR")
                End If

                _dataBuffer = ""
            End If

            ' Batasi ukuran buffer
            If _dataBuffer.Length > 1024 Then
                Debug.WriteLine($"[ProcessBuffer] Buffer overflow protection - trimming from {_dataBuffer.Length}")
                _dataBuffer = _dataBuffer.Substring(_dataBuffer.Length - 256)
            End If

        Catch ex As Exception
            Debug.WriteLine("[ProcessBuffer] ERROR: " & ex.ToString())
            RaiseEvent ErrorOccurred("Gagal memproses data dari indikator timbangan.")
            _dataBuffer = ""
        End Try
    End Sub

    ' =============================================
    ' PARSE WEIGHT - ROUTER UTAMA
    ' Mengarahkan ke parser sesuai ScaleType
    ' Jika AutoDetect, fallback ke brand
    ' =============================================
    Private Shared Function ParseWeight(rawData As String) As Decimal
        Try
            If String.IsNullOrWhiteSpace(rawData) Then Return PARSE_ERROR

            ' Bersihkan karakter non-printable
            Dim sanitized As String = Regex.Replace(rawData, "[^\x20-\x7E]", "").Trim()
            If String.IsNullOrEmpty(sanitized) Then Return PARSE_ERROR

            Debug.WriteLine($"[ParseWeight] Brand={_scaleBrand}, Type={_scaleType}, Input=[{sanitized}]")

            ' === ROUTING BERDASARKAN SCALE TYPE (PRIORITAS UTAMA) ===
            Select Case _scaleType

                ' ========== GSC TYPES ==========
                Case ScaleType.GSC_SGW3015S
                    Return ParseGscSGW3015S(sanitized)

                Case ScaleType.GSC_SGW3015, ScaleType.GSC_SGW3000
                    Return ParseGscSimple(sanitized)

                Case ScaleType.GSC_GST9600, ScaleType.GSC_GST9800
                    Return ParseGscSimple(sanitized)

                ' ========== SONIC TYPES ==========
                Case ScaleType.Sonic_SPS2000, ScaleType.Sonic_SPS3000,
                     ScaleType.Sonic_SPS1000, ScaleType.Sonic_T7E
                    Return ParseSonicWeight(sanitized)

                ' ========== YAOHUA TYPES ==========
                Case ScaleType.Yaohua_A28E, ScaleType.Yaohua_A12E
                    Return ParseYaohuaA28E(sanitized)

                Case ScaleType.Yaohua_A9, ScaleType.Yaohua_A7
                    Return ParseYaohuaA9(sanitized)

                Case ScaleType.Yaohua_T7
                    Return ParseYaohuaWeight(sanitized)

                ' ========== AUTO DETECT ==========
                Case ScaleType.AutoDetect
                    Return ParseAutoDetect(sanitized)

                    ' ========== FALLBACK KE BRAND ==========
                Case Else
                    Select Case _scaleBrand
                        Case ScaleBrand.Sonic
                            Return ParseSonicWeight(sanitized)
                        Case ScaleBrand.Yaohua
                            Return ParseYaohuaWeight(sanitized)
                        Case Else
                            Return ParseGscWeight(sanitized)
                    End Select
            End Select

        Catch ex As Exception
            Debug.WriteLine($"[ParseWeight] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' PARSER: GSC SGW-3015S (SPESIFIK)
    ' Format: ST,GS,+  12345 kg (continuous mode)
    ' =============================================
    Private Shared Function ParseGscSGW3015S(cleanData As String) As Decimal
        Try
            Dim working As String = cleanData.ToUpperInvariant().Trim()

            Debug.WriteLine($"[ParseGscSGW3015S] Input: [{working}]")

            ' Cek overload/underload
            If working.StartsWith("OL") Then
                RaiseEvent ErrorOccurred("Timbangan OVERLOAD!")
                Return PARSE_ERROR
            End If
            If working.StartsWith("UL") Then
                RaiseEvent ErrorOccurred("Timbangan UNDERLOAD!")
                Return PARSE_ERROR
            End If

            ' =============================================
            ' FORMAT UTAMA SGW-3015S:
            ' "ST,GS,+  12345 kg"   → Stable, Gross
            ' "ST,NT,+   2345 kg"   → Stable, Net
            ' "US,GS,+  12345 kg"   → Unstable, Gross
            ' "ST,GS,+  12345.0 kg" → Dengan desimal
            ' =============================================
            Dim pattern As String =
                "^(?:ST|US|OL|UL)\s*,\s*(?:GS|NT|GT)\s*,\s*(?<sign>[+\-])?\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:KG|KGS|G|T)?\s*$"

            Dim m As Match = Regex.Match(working, pattern, RegexOptions.IgnoreCase)
            If m.Success Then
                Dim sign As String = m.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(m.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint,
                                    CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    Debug.WriteLine($"[ParseGscSGW3015S] ✓ Main pattern: {value}")
                    Return value
                End If
            End If

            ' Jika format utama gagal, coba parser GSC umum sebagai fallback
            Debug.WriteLine($"[ParseGscSGW3015S] Main pattern gagal, coba GSC umum...")
            Return ParseGscWeight(cleanData)

        Catch ex As Exception
            Debug.WriteLine($"[ParseGscSGW3015S] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' PARSER: GSC Simple (SGW-3015, GST-9600, dll)
    ' Format: = 12345 atau +12345 kg atau 12345
    ' =============================================
    Private Shared Function ParseGscSimple(cleanData As String) As Decimal
        Try
            Dim working As String = cleanData.ToUpperInvariant().Trim()

            Debug.WriteLine($"[ParseGscSimple] Input: [{working}]")

            ' Cek overload/underload
            If working.StartsWith("OL") Then
                RaiseEvent ErrorOccurred("Timbangan OVERLOAD!")
                Return PARSE_ERROR
            End If
            If working.StartsWith("UL") Then
                RaiseEvent ErrorOccurred("Timbangan UNDERLOAD!")
                Return PARSE_ERROR
            End If

            ' Hapus prefix "="
            working = Regex.Replace(working, "^=\s*", "")
            ' Hapus prefix status
            working = Regex.Replace(working, "^(ST|GS|NT|US)\s*,?\s*(GS|NT)?\s*,?\s*", "", RegexOptions.IgnoreCase)
            ' Hapus satuan
            working = Regex.Replace(working, "\s*(KG|KGS|G|T)\s*$", "", RegexOptions.IgnoreCase)
            working = working.Trim()

            Dim m As Match = Regex.Match(working, "^(?<sign>[+\-])?\s*(?<num>\d+(?:[.,]\d+)?)")
            If m.Success Then
                Dim sign As String = m.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(m.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint,
                                    CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    Debug.WriteLine($"[ParseGscSimple] ✓ {value}")
                    Return value
                End If
            End If

            Debug.WriteLine($"[ParseGscSimple] ✗ Gagal parse [{cleanData}]")
            Return PARSE_ERROR

        Catch ex As Exception
            Debug.WriteLine($"[ParseGscSimple] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' PARSER: GSC UMUM (FALLBACK UNTUK SEMUA GSC)
    ' Support berbagai format GSC yang tidak diketahui tipenya
    ' =============================================
    Private Shared Function ParseGscWeight(cleanData As String) As Decimal
        Try
            Dim working As String = cleanData.ToUpperInvariant().Trim()

            Debug.WriteLine($"[ParseGscWeight] Input: [{working}]")

            ' === CEK OVERLOAD / UNDERLOAD ===
            If working.StartsWith("OL") Then
                RaiseEvent ErrorOccurred("Timbangan OVERLOAD!")
                Return PARSE_ERROR
            End If
            If working.StartsWith("UL") Then
                RaiseEvent ErrorOccurred("Timbangan UNDERLOAD!")
                Return PARSE_ERROR
            End If

            ' =============================================
            ' PATTERN 1: Format lengkap ST,GS,+ 12345 kg
            ' =============================================
            Dim fullPattern As String =
                "^(?:ST|US|OL|UL)\s*,\s*(?:GS|NT|GT)\s*,\s*(?<sign>[+\-])?\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:KG|KGS|G|T)?\s*$"

            Dim fullMatch As Match = Regex.Match(working, fullPattern, RegexOptions.IgnoreCase)
            If fullMatch.Success Then
                Dim sign As String = fullMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(fullMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    Debug.WriteLine($"[ParseGscWeight] Pattern1 ✓: {value}")
                    Return value
                End If
            End If

            ' =============================================
            ' PATTERN 2: Format tanpa koma: ST GS +12345 kg
            ' =============================================
            Dim noCommaPattern As String =
                "^(?:ST|US|GS|NT)\s+(?:GS|NT)?\s*(?<sign>[+\-])?\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:KG|KGS|G|T)?\s*$"

            Dim noCommaMatch As Match = Regex.Match(working, noCommaPattern, RegexOptions.IgnoreCase)
            If noCommaMatch.Success Then
                Dim sign As String = noCommaMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(noCommaMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    Debug.WriteLine($"[ParseGscWeight] Pattern2 ✓: {value}")
                    Return value
                End If
            End If

            ' =============================================
            ' PATTERN 3: Format "= 12345"
            ' =============================================
            Dim equalPattern As String =
                "^=\s*(?<sign>[+\-])?\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:KG|KGS|G|T)?\s*$"

            Dim equalMatch As Match = Regex.Match(working, equalPattern, RegexOptions.IgnoreCase)
            If equalMatch.Success Then
                Dim sign As String = equalMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(equalMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    Debug.WriteLine($"[ParseGscWeight] Pattern3 (=) ✓: {value}")
                    Return value
                End If
            End If

            ' =============================================
            ' PATTERN 4: Strip semua prefix, cari angka
            ' =============================================
            Dim stripped As String = working
            stripped = Regex.Replace(stripped, "^(ST|GS|NT|US|OL|UL)\s*[,]?\s*(GS|NT|GT)?\s*[,]?\s*", "", RegexOptions.IgnoreCase)
            stripped = Regex.Replace(stripped, "^=\s*", "")
            stripped = Regex.Replace(stripped, "\s*(KG|KGS|G|GRAM|LB|OZ|T)\s*$", "", RegexOptions.IgnoreCase)
            stripped = stripped.Trim()

            Debug.WriteLine($"[ParseGscWeight] After strip: [{stripped}]")

            Dim fallbackMatch As Match = Regex.Match(stripped, "^(?<sign>[+\-])?\s*(?<num>\d+[.,]?\d*)")
            If fallbackMatch.Success Then
                Dim sign As String = fallbackMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(fallbackMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    Debug.WriteLine($"[ParseGscWeight] Pattern4 (fallback) ✓: {value}")
                    Return value
                End If
            End If

            ' =============================================
            ' PATTERN 5: LAST RESORT - Cari angka di mana saja (min 2 digit)
            ' =============================================
            Dim lastResortMatch As Match = Regex.Match(working, "(?<sign>[+\-])?\s*(?<num>\d{2,}(?:[.,]\d+)?)")
            If lastResortMatch.Success Then
                Dim sign As String = lastResortMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(lastResortMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    Debug.WriteLine($"[ParseGscWeight] Pattern5 (last resort) ✓: {value}")
                    Return value
                End If
            End If

            Debug.WriteLine($"[ParseGscWeight] ✗ SEMUA PATTERN GAGAL untuk [{working}]")
            Return PARSE_ERROR

        Catch ex As Exception
            Debug.WriteLine($"[ParseGscWeight] Exception: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' PARSER: SONIC (SEMUA TIPE)
    ' =============================================
    Private Shared Function ParseSonicWeight(raw As String) As Decimal
        Try
            Dim normalized As String = Regex.Replace(
                raw.Replace(ControlChars.Tab, " "), "\s+", " ").Trim()

            Debug.WriteLine($"[ParseSonicWeight] Input: [{normalized}]")

            ' Deteksi satuan gram
            Dim isGram As Boolean =
                normalized.IndexOf("GRAM", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                normalized.EndsWith(" G", StringComparison.OrdinalIgnoreCase)

            For Each pattern As String In SonicPatterns
                Dim match As Match = Regex.Match(normalized, pattern, RegexOptions.IgnoreCase)
                If match.Success Then
                    Dim numericPart As String = match.Groups("num").Value
                    Dim standardized As String = StandardizeNumeric(numericPart)

                    Dim value As Decimal
                    If Decimal.TryParse(standardized,
                                        NumberStyles.AllowDecimalPoint Or NumberStyles.AllowLeadingSign,
                                        CultureInfo.InvariantCulture, value) Then

                        If normalized.Contains("-") AndAlso value > 0D AndAlso
                           (standardized.Length = 0 OrElse standardized(0) <> "-"c) Then
                            value = -value
                        End If

                        If isGram Then
                            value = Math.Round(value / 1000D, 3)
                        End If

                        Debug.WriteLine($"[ParseSonicWeight] ✓ PARSED: [{raw.Trim()}] -> {value}")
                        Return value
                    End If
                End If
            Next

            Debug.WriteLine($"[ParseSonicWeight] ✗ GAGAL parse [{normalized}]")
            Return PARSE_ERROR

        Catch ex As Exception
            Debug.WriteLine($"[ParseSonicWeight] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' PARSER: YAOHUA A28E / A12E (SPESIFIK)
    ' Format: ST,GS,  +  1234.5 kg
    ' CATATAN: Format ini MIRIP dengan GSC SGW-3015S!
    ' =============================================
    Private Shared Function ParseYaohuaA28E(raw As String) As Decimal
        Try
            Dim cleaned As String = Regex.Replace(raw, "[^\x20-\x7E]", "").Trim()
            If String.IsNullOrWhiteSpace(cleaned) Then Return PARSE_ERROR

            Dim normalized As String = Regex.Replace(cleaned, "\s+", " ").Trim()

            Debug.WriteLine($"[ParseYaohuaA28E] Input: [{normalized}]")

            ' === CEK OVERLOAD ===
            If normalized.StartsWith("OL,", StringComparison.OrdinalIgnoreCase) Then
                RaiseEvent ErrorOccurred("Timbangan OVERLOAD! Kurangi beban.")
                Return PARSE_ERROR
            End If

            ' === CEK UNDERLOAD ===
            If normalized.StartsWith("UL,", StringComparison.OrdinalIgnoreCase) Then
                RaiseEvent ErrorOccurred("Timbangan UNDERLOAD!")
                Return PARSE_ERROR
            End If

            ' === FORMAT UTAMA A28E: ST,GS,+ 1234.5 kg ===
            Dim mainPattern As String =
                "^(?:ST|US|OL|UL),(?:GS|NT),\s*(?<sign>[+\-])\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:kg|KG|t|T)?\s*$"

            Dim mainMatch As Match = Regex.Match(normalized, mainPattern, RegexOptions.IgnoreCase)

            If mainMatch.Success Then
                Dim sign As String = mainMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(mainMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value

                    ' Toleransi zero drift
                    If value < -5D Then
                        Debug.WriteLine($"[ParseYaohuaA28E] Nilai negatif besar ({value}), abaikan")
                        Return PARSE_ERROR
                    End If
                    If value < 0D Then value = 0D

                    Debug.WriteLine($"[ParseYaohuaA28E] ✓ Parsed: {value} kg")
                    Return value
                End If
            End If

            ' Fallback ke parser Yaohua umum
            Debug.WriteLine($"[ParseYaohuaA28E] Main pattern gagal, coba Yaohua umum...")
            Return ParseYaohuaWeight(raw)

        Catch ex As Exception
            Debug.WriteLine($"[ParseYaohuaA28E] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' PARSER: YAOHUA A9 / A7 (SPESIFIK)
    ' Format: wn12345 atau wn 12345 atau wn+12345
    ' =============================================
    Private Shared Function ParseYaohuaA9(raw As String) As Decimal
        Try
            Dim working As String = raw.Trim()

            Debug.WriteLine($"[ParseYaohuaA9] Input: [{working}]")

            ' Format A9: "wn12345" atau "wn  12345" atau "wn+12345"
            Dim pattern As String = "(?:wn|WN)\s*(?<sign>[+\-])?\s*(?<num>\d+(?:[.,]\d+)?)"
            Dim m As Match = Regex.Match(working, pattern, RegexOptions.IgnoreCase)

            If m.Success Then
                Dim sign As String = m.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(m.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint,
                                    CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    Debug.WriteLine($"[ParseYaohuaA9] ✓ {value}")
                    Return value
                End If
            End If

            ' Fallback ke parser Yaohua umum
            Debug.WriteLine($"[ParseYaohuaA9] WN pattern gagal, coba Yaohua umum...")
            Return ParseYaohuaWeight(raw)

        Catch ex As Exception
            Debug.WriteLine($"[ParseYaohuaA9] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' PARSER: YAOHUA UMUM (FALLBACK)
    ' Support berbagai format Yaohua yang tidak diketahui tipenya
    ' =============================================
    Private Shared Function ParseYaohuaWeight(raw As String) As Decimal
        Try
            Dim cleaned As String = Regex.Replace(raw, "[^\x20-\x7E]", "").Trim()
            If String.IsNullOrWhiteSpace(cleaned) Then
                Debug.WriteLine("[ParseYaohuaWeight] Data kosong")
                Return PARSE_ERROR
            End If

            Dim normalized As String = Regex.Replace(cleaned, "\s+", " ").Trim()

            Debug.WriteLine($"[ParseYaohuaWeight] Input: [{normalized}]")

            ' === CEK OVERLOAD ===
            If normalized.StartsWith("OL,", StringComparison.OrdinalIgnoreCase) Then
                RaiseEvent ErrorOccurred("Timbangan OVERLOAD! Kurangi beban.")
                Return PARSE_ERROR
            End If

            ' === CEK UNDERLOAD ===
            If normalized.StartsWith("UL,", StringComparison.OrdinalIgnoreCase) Then
                RaiseEvent ErrorOccurred("Timbangan UNDERLOAD!")
                Return PARSE_ERROR
            End If

            ' === FORMAT: ST,GS / ST,NT / US,GS ===
            Dim mainPattern As String =
                "^(?:ST|US|OL|UL),(?:GS|NT),\s*(?<sign>[+\-])\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:kg|KG|t|T)?\s*$"

            Dim mainMatch As Match = Regex.Match(normalized, mainPattern, RegexOptions.IgnoreCase)

            If mainMatch.Success Then
                Dim sign As String = mainMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(mainMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    If value < -5D Then Return PARSE_ERROR
                    If value < 0D Then value = 0D

                    Debug.WriteLine($"[ParseYaohuaWeight] ✓ Parsed: {value} kg")
                    Return value
                End If
            End If

            ' === FORMAT WN (A9 style) ===
            Dim wnPattern As String = "(?:wn|WN)\s*(?<sign>[+\-])?\s*(?<num>\d+(?:[.,]\d+)?)"
            Dim wnMatch As Match = Regex.Match(normalized, wnPattern, RegexOptions.IgnoreCase)

            If wnMatch.Success Then
                Dim sign As String = wnMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(wnMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    If value < 0D Then value = 0D
                    Debug.WriteLine($"[ParseYaohuaWeight] ✓ Parsed (WN): {value} kg")
                    Return value
                End If
            End If

            ' === FALLBACK: cari angka + satuan ===
            Dim fallbackPattern As String =
                "(?<sign>[+\-])?\s*(?<num>\d+(?:[.,]\d+)?)\s*(?:kg|KG|t|T)"

            Dim fallbackMatch As Match = Regex.Match(normalized, fallbackPattern, RegexOptions.IgnoreCase)

            If fallbackMatch.Success Then
                Dim sign As String = fallbackMatch.Groups("sign").Value
                Dim numStr As String = StandardizeNumeric(fallbackMatch.Groups("num").Value)

                Dim value As Decimal
                If Decimal.TryParse(numStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, value) Then
                    If sign = "-" Then value = -value
                    If value < 0D Then value = 0D
                    Debug.WriteLine($"[ParseYaohuaWeight] ✓ Parsed (fallback): {value} kg")
                    Return value
                End If
            End If

            Debug.WriteLine($"[ParseYaohuaWeight] ✗ Tidak bisa parse [{normalized}]")
            Return PARSE_ERROR

        Catch ex As Exception
            Debug.WriteLine($"[ParseYaohuaWeight] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' AUTO DETECT - Coba semua parser
    ' Dipakai jika SCALE_TYPE = AutoDetect atau kosong
    ' =============================================
    Private Shared Function ParseAutoDetect(cleanData As String) As Decimal
        Try
            Debug.WriteLine($"[ParseAutoDetect] Trying all parsers for: [{cleanData}]")

            Dim result As Decimal

            ' Prioritaskan berdasarkan brand yang dipilih
            Select Case _scaleBrand
                Case ScaleBrand.GSC
                    ' 1. Coba GSC SGW-3015S (format paling spesifik)
                    result = ParseGscSGW3015S(cleanData)
                    If result <> PARSE_ERROR Then
                        Debug.WriteLine($"[ParseAutoDetect] ✓ GSC SGW-3015S: {result}")
                        Return result
                    End If

                    ' 2. Coba GSC Simple
                    result = ParseGscSimple(cleanData)
                    If result <> PARSE_ERROR Then
                        Debug.WriteLine($"[ParseAutoDetect] ✓ GSC Simple: {result}")
                        Return result
                    End If

                    ' 3. Coba GSC General
                    result = ParseGscWeight(cleanData)
                    If result <> PARSE_ERROR Then
                        Debug.WriteLine($"[ParseAutoDetect] ✓ GSC General: {result}")
                        Return result
                    End If

                Case ScaleBrand.Sonic
                    ' 1. Coba Sonic
                    result = ParseSonicWeight(cleanData)
                    If result <> PARSE_ERROR Then
                        Debug.WriteLine($"[ParseAutoDetect] ✓ Sonic: {result}")
                        Return result
                    End If

                Case ScaleBrand.Yaohua
                    ' 1. Coba Yaohua A28E
                    result = ParseYaohuaA28E(cleanData)
                    If result <> PARSE_ERROR Then
                        Debug.WriteLine($"[ParseAutoDetect] ✓ Yaohua A28E: {result}")
                        Return result
                    End If

                    ' 2. Coba Yaohua A9
                    result = ParseYaohuaA9(cleanData)
                    If result <> PARSE_ERROR Then
                        Debug.WriteLine($"[ParseAutoDetect] ✓ Yaohua A9: {result}")
                        Return result
                    End If

                    ' 3. Coba Yaohua umum
                    result = ParseYaohuaWeight(cleanData)
                    If result <> PARSE_ERROR Then
                        Debug.WriteLine($"[ParseAutoDetect] ✓ Yaohua General: {result}")
                        Return result
                    End If
            End Select

            ' === CROSS-BRAND FALLBACK ===
            ' Jika brand-specific gagal, coba parser lain

            ' Coba GSC SGW-3015S (format paling umum)
            result = ParseGscSGW3015S(cleanData)
            If result <> PARSE_ERROR Then
                Debug.WriteLine($"[ParseAutoDetect] ✓ Cross-brand GSC SGW-3015S: {result}")
                Return result
            End If

            ' Coba Yaohua A28E (format mirip GSC)
            result = ParseYaohuaA28E(cleanData)
            If result <> PARSE_ERROR Then
                Debug.WriteLine($"[ParseAutoDetect] ✓ Cross-brand Yaohua A28E: {result}")
                Return result
            End If

            ' Coba Sonic
            result = ParseSonicWeight(cleanData)
            If result <> PARSE_ERROR Then
                Debug.WriteLine($"[ParseAutoDetect] ✓ Cross-brand Sonic: {result}")
                Return result
            End If

            ' Coba Yaohua A9
            result = ParseYaohuaA9(cleanData)
            If result <> PARSE_ERROR Then
                Debug.WriteLine($"[ParseAutoDetect] ✓ Cross-brand Yaohua A9: {result}")
                Return result
            End If

            ' Coba GSC Simple
            result = ParseGscSimple(cleanData)
            If result <> PARSE_ERROR Then
                Debug.WriteLine($"[ParseAutoDetect] ✓ Cross-brand GSC Simple: {result}")
                Return result
            End If

            ' Last resort - GSC umum
            result = ParseGscWeight(cleanData)
            If result <> PARSE_ERROR Then
                Debug.WriteLine($"[ParseAutoDetect] ✓ Cross-brand GSC General: {result}")
                Return result
            End If

            Debug.WriteLine($"[ParseAutoDetect] ✗ SEMUA PARSER GAGAL [{cleanData}]")
            Return PARSE_ERROR

        Catch ex As Exception
            Debug.WriteLine($"[ParseAutoDetect] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' NORMALISASI FORMAT ANGKA (RIBUAN/DESIMAL)
    ' =============================================
    Private Shared Function StandardizeNumeric(input As String) As String
        Try
            Dim value As String = input.Trim().Replace(" ", "")
            If String.IsNullOrEmpty(value) Then Return "0"

            Dim commaIdx As Integer = value.LastIndexOf(","c)
            Dim dotIdx As Integer = value.LastIndexOf("."c)

            If commaIdx >= 0 AndAlso dotIdx >= 0 Then
                If dotIdx > commaIdx Then
                    value = value.Replace(",", "")
                Else
                    value = value.Replace(".", "").Replace(",", ".")
                End If
            ElseIf commaIdx >= 0 Then
                value = value.Replace(",", ".")
            End If

            Return value

        Catch ex As Exception
            Debug.WriteLine("[StandardizeNumeric] Error: " & ex.Message)
            Return input.Trim()
        End Try
    End Function

    ' =============================================
    ' READ WEIGHT ONCE - DENGAN RETRY
    ' =============================================
    Public Shared Function ReadWeightOnce() As Decimal
        Try
            SyncLock _lockObject
                If _serialPort Is Nothing OrElse Not _serialPort.IsOpen Then
                    Debug.WriteLine("[ReadWeightOnce] Port not open")
                    Return PARSE_ERROR
                End If

                Dim readDelay As Integer = CalculateReadDelay(_currentBaudRate)
                Dim retryCount As Integer = CalculateRetryCount(_currentBaudRate)
                Dim waitBetweenRetry As Integer = If(_currentBaudRate <= 4800, 300, 150)

                Debug.WriteLine($"[ReadWeightOnce] BaudRate={_currentBaudRate}, Delay={readDelay}ms, Retry={retryCount}x")

                _serialPort.DiscardInBuffer()

                For attempt As Integer = 1 To retryCount
                    Try
                        Threading.Thread.Sleep(readDelay)

                        If _serialPort.BytesToRead > 0 Then
                            Threading.Thread.Sleep(If(_currentBaudRate <= 4800, 200, 100))

                            Dim data As String = _serialPort.ReadExisting()

                            Debug.WriteLine($"[ReadWeightOnce] Attempt {attempt}: Data=[{data.Trim()}]")

                            If Not String.IsNullOrWhiteSpace(data) Then
                                Dim lines() As String = data.Split({CChar(vbCr), CChar(vbLf), Chr(2), Chr(3)}, StringSplitOptions.RemoveEmptyEntries)

                                For Each line In lines
                                    line = line.Trim()
                                    If line.Length < 2 Then Continue For

                                    Dim weight As Decimal = ParseWeight(line)
                                    If weight <> PARSE_ERROR Then
                                        Debug.WriteLine($"[ReadWeightOnce] ✓ Attempt {attempt}: Weight={weight}")
                                        Return weight
                                    End If
                                Next
                            End If
                        Else
                            Debug.WriteLine($"[ReadWeightOnce] Attempt {attempt}: No data in buffer")
                        End If

                        If attempt < retryCount Then
                            Threading.Thread.Sleep(waitBetweenRetry)
                        End If

                    Catch exRead As TimeoutException
                        Debug.WriteLine($"[ReadWeightOnce] Attempt {attempt}: Timeout")
                        If attempt < retryCount Then
                            Threading.Thread.Sleep(waitBetweenRetry)
                        End If
                    Catch exRead As Exception
                        Debug.WriteLine($"[ReadWeightOnce] Attempt {attempt}: Error - {exRead.Message}")
                    End Try
                Next

                Debug.WriteLine("[ReadWeightOnce] ✗ All attempts failed")
                Return PARSE_ERROR
            End SyncLock

        Catch ex As Exception
            Debug.WriteLine("[ReadWeightOnce] Exception: " & ex.ToString())
            RaiseEvent ErrorOccurred("Gagal membaca berat dari indikator timbangan.")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' READ WEIGHT ONCE LEGACY (BACKWARD COMPATIBILITY)
    ' =============================================
    Public Shared Function ReadWeightOnceLegacy(timeoutMs As Integer) As Decimal
        Try
            SyncLock _lockObject
                If _serialPort Is Nothing OrElse Not _serialPort.IsOpen Then
                    Return PARSE_ERROR
                End If

                Dim data As String = ""
                Dim startTime As DateTime = DateTime.Now

                _serialPort.DiscardInBuffer()

                While (DateTime.Now - startTime).TotalMilliseconds < timeoutMs
                    Try
                        If _serialPort.BytesToRead > 0 Then
                            data = _serialPort.ReadLine()
                            If Not String.IsNullOrWhiteSpace(data) Then
                                Exit While
                            End If
                        End If
                        Thread.Sleep(50)
                    Catch ex As TimeoutException
                        Exit While
                    Catch ex As Exception
                        Exit While
                    End Try
                End While

                If String.IsNullOrWhiteSpace(data) Then
                    Return PARSE_ERROR
                End If

                Return ParseWeight(data)
            End SyncLock

        Catch ex As Exception
            Debug.WriteLine($"[ReadWeightOnceLegacy] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' READ WEIGHT WITH CUSTOM TIMEOUT
    ' =============================================
    Public Shared Function ReadWeightWithTimeout(timeoutMs As Integer) As Decimal
        Try
            SyncLock _lockObject
                If _serialPort Is Nothing OrElse Not _serialPort.IsOpen Then Return PARSE_ERROR

                Dim originalTimeout As Integer = _serialPort.ReadTimeout

                Try
                    _serialPort.ReadTimeout = timeoutMs
                    _serialPort.DiscardInBuffer()

                    Dim waitTime As Integer = Math.Min(timeoutMs \ 2, 1000)
                    Threading.Thread.Sleep(waitTime)

                    If _serialPort.BytesToRead > 0 Then
                        Threading.Thread.Sleep(200)
                        Dim data As String = _serialPort.ReadExisting()

                        If Not String.IsNullOrWhiteSpace(data) Then
                            Dim lines() As String = data.Split({CChar(vbCr), CChar(vbLf), Chr(2), Chr(3)}, StringSplitOptions.RemoveEmptyEntries)
                            For Each line In lines
                                line = line.Trim()
                                If line.Length < 2 Then Continue For
                                Dim weight As Decimal = ParseWeight(line)
                                If weight <> PARSE_ERROR Then
                                    Return weight
                                End If
                            Next
                        End If
                    End If

                    Return PARSE_ERROR

                Finally
                    _serialPort.ReadTimeout = originalTimeout
                End Try
            End SyncLock

        Catch ex As Exception
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' FORCE READ - Untuk indikator lambat
    ' =============================================
    Public Shared Function ForceRead(Optional maxWaitMs As Integer = 10000) As Decimal
        Try
            SyncLock _lockObject
                If _serialPort Is Nothing OrElse Not _serialPort.IsOpen Then Return PARSE_ERROR

                Debug.WriteLine($"[ForceRead] MaxWait={maxWaitMs}ms, Brand={_scaleBrand}, Type={_scaleType}")

                _serialPort.DiscardInBuffer()

                Dim startTime As DateTime = DateTime.Now
                Dim checkInterval As Integer = 200

                While (DateTime.Now - startTime).TotalMilliseconds < maxWaitMs
                    Threading.Thread.Sleep(checkInterval)

                    If _serialPort.BytesToRead > 0 Then
                        Threading.Thread.Sleep(If(_currentBaudRate <= 4800, 500, 200))

                        Dim data As String = _serialPort.ReadExisting()

                        If Not String.IsNullOrWhiteSpace(data) Then
                            Debug.WriteLine($"[ForceRead] Data received: [{data.Trim()}]")

                            Dim lines() As String = data.Split({CChar(vbCr), CChar(vbLf), Chr(2), Chr(3)}, StringSplitOptions.RemoveEmptyEntries)
                            For Each line In lines
                                line = line.Trim()
                                If line.Length < 2 Then Continue For
                                Dim weight As Decimal = ParseWeight(line)
                                If weight <> PARSE_ERROR Then
                                    Debug.WriteLine($"[ForceRead] ✓ Success: Weight={weight}")
                                    Return weight
                                End If
                            Next
                        End If
                    End If
                End While

                Debug.WriteLine("[ForceRead] ✗ Timeout - no valid data")
                Return PARSE_ERROR
            End SyncLock

        Catch ex As Exception
            Debug.WriteLine($"[ForceRead] Error: {ex.Message}")
            Return PARSE_ERROR
        End Try
    End Function

    ' =============================================
    ' SEND COMMAND TO SCALE
    ' =============================================
    Public Shared Function SendCommand(command As String) As Boolean
        Try
            SyncLock _lockObject
                If _serialPort Is Nothing OrElse Not _serialPort.IsOpen Then
                    RaiseEvent ErrorOccurred("Port tidak terbuka!")
                    Return False
                End If
                _serialPort.WriteLine(command)
                Return True
            End SyncLock
        Catch ex As Exception
            Debug.WriteLine("[SerialPortHelper.SendCommand] Error: " & ex.ToString())
            RaiseEvent ErrorOccurred("Gagal mengirim perintah ke indikator timbangan.")
            Return False
        End Try
    End Function

    ' =============================================
    ' TEST CONNECTION
    ' =============================================
    Public Shared Function TestConnection(portName As String, Optional baudRate As Integer = 9600) As Boolean
        Try
            Dim testTimeout As Integer = CalculateReadTimeout(baudRate) + 1000
            Dim testDelay As Integer = CalculateReadDelay(baudRate) + 200

            Using testPort As New SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
                testPort.ReadTimeout = testTimeout
                testPort.RtsEnable = True
                testPort.DtrEnable = True
                testPort.Open()

                Debug.WriteLine($"[TestConnection] Port={portName}, BaudRate={baudRate}, Timeout={testTimeout}ms")

                Threading.Thread.Sleep(500)
                Threading.Thread.Sleep(testDelay)

                If testPort.BytesToRead > 0 Then
                    Threading.Thread.Sleep(200)
                    Dim data As String = testPort.ReadExisting()
                    testPort.Close()

                    Dim hasValidData As Boolean = Not String.IsNullOrEmpty(data)
                    Debug.WriteLine($"[TestConnection] HasData={hasValidData}, Data=[{data.Trim()}]")

                    Return hasValidData
                End If

                testPort.Close()
            End Using

            Debug.WriteLine("[TestConnection] No data received")
            Return False

        Catch ex As Exception
            Debug.WriteLine($"[TestConnection] Error: {ex.Message}")
            Return False
        End Try
    End Function

    ' =============================================
    ' GET SETTING FROM DATABASE
    ' =============================================
    Private Shared Function GetSettingFromDB(key As String, defaultValue As String) As String
        Try
            Dim query As String = "SELECT SettingValue FROM Settings WHERE SettingKey = @Key"
            Dim params As System.Data.SqlClient.SqlParameter() = {New System.Data.SqlClient.SqlParameter("@Key", key)}
            Dim result As Object = DatabaseHelper.ExecuteScalar(query, params)
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then Return result.ToString()
            Return defaultValue
        Catch ex As Exception
            Debug.WriteLine("[GetSettingFromDB] Error: " & ex.Message)
            Return defaultValue
        End Try
    End Function

    ' =============================================
    ' SAVE SETTING TO DATABASE
    ' =============================================
    Public Shared Sub SaveSettingToDB(key As String, value As String)
        Try
            Dim query As String = "UPDATE Settings SET SettingValue = @Value, UpdatedAt = GETDATE() WHERE SettingKey = @Key"
            Dim params As System.Data.SqlClient.SqlParameter() = {
                New System.Data.SqlClient.SqlParameter("@Value", value),
                New System.Data.SqlClient.SqlParameter("@Key", key)
            }
            DatabaseHelper.ExecuteNonQuery(query, params)
        Catch ex As Exception
            Debug.WriteLine("[SerialPortHelper.SaveSettingToDB] Error: " & ex.ToString())
            RaiseEvent ErrorOccurred("Gagal menyimpan pengaturan indikator timbangan.")
        End Try
    End Sub

    ' =============================================
    ' IS PARSE ERROR
    ' =============================================
    Public Shared Function IsParseError(value As Decimal) As Boolean
        Return value = PARSE_ERROR
    End Function

    ' =============================================
    ' GET DIAGNOSTIC INFO (untuk debugging)
    ' =============================================
    Public Shared Function GetDiagnosticInfo() As String
        Dim info As New System.Text.StringBuilder()

        info.AppendLine("╔══════════════════════════════════════╗")
        info.AppendLine("║    SERIAL PORT DIAGNOSTIC INFO       ║")
        info.AppendLine("╚══════════════════════════════════════╝")
        info.AppendLine()
        info.AppendLine($"ScaleBrand       : {_scaleBrand}")
        info.AppendLine($"ScaleType        : {_scaleType}")
        info.AppendLine($"DB SCALE_BRAND   : {GetSettingFromDB("SCALE_BRAND", "(not set)")}")
        info.AppendLine($"DB SCALE_TYPE    : {GetSettingFromDB("SCALE_TYPE", "(not set)")}")
        info.AppendLine($"IsConnected      : {IsConnected}")
        info.AppendLine($"Port             : {CurrentPortName}")
        info.AppendLine($"BaudRate         : {_currentBaudRate}")
        info.AppendLine($"ReadTimeout      : {CalculateReadTimeout(_currentBaudRate)}ms")
        info.AppendLine($"ReadDelay        : {CalculateReadDelay(_currentBaudRate)}ms")
        info.AppendLine($"RetryCount       : {CalculateRetryCount(_currentBaudRate)}")

        If _serialPort IsNot Nothing AndAlso _serialPort.IsOpen Then
            Try
                info.AppendLine($"BytesToRead      : {_serialPort.BytesToRead}")
                info.AppendLine($"ReadBufferSize   : {_serialPort.ReadBufferSize}")
                info.AppendLine($"RtsEnable        : {_serialPort.RtsEnable}")
                info.AppendLine($"DtrEnable        : {_serialPort.DtrEnable}")
                info.AppendLine($"Handshake        : {_serialPort.Handshake}")
                info.AppendLine($"Parity           : {_serialPort.Parity}")
                info.AppendLine($"DataBits         : {_serialPort.DataBits}")
                info.AppendLine($"StopBits         : {_serialPort.StopBits}")
            Catch ex As Exception
                info.AppendLine($"(Error reading port info: {ex.Message})")
            End Try
        Else
            info.AppendLine("Port             : NOT OPEN")
        End If

        info.AppendLine($"DataBuffer Len   : {_dataBuffer.Length}")
        info.AppendLine($"Last Valid Data  : {If(_lastValidDataTime = DateTime.MinValue, "NEVER", _lastValidDataTime.ToString("HH:mm:ss.fff"))}")

        info.AppendLine()
        info.AppendLine("--- Database Settings ---")
        info.AppendLine($"COM_PORT         : {GetSettingFromDB("COM_PORT", "(not set)")}")
        info.AppendLine($"BAUD_RATE        : {GetSettingFromDB("BAUD_RATE", "(not set)")}")
        info.AppendLine($"SCALE_BRAND      : {GetSettingFromDB("SCALE_BRAND", "(not set)")}")
        info.AppendLine($"SCALE_TYPE       : {GetSettingFromDB("SCALE_TYPE", "(not set)")}")

        info.AppendLine()
        info.AppendLine("--- Parser Routing ---")
        info.AppendLine($"Jika ScaleType = {_scaleType}:")
        Select Case _scaleType
            Case ScaleType.GSC_SGW3015S
                info.AppendLine("  → ParseGscSGW3015S (lalu fallback ParseGscWeight)")
            Case ScaleType.GSC_SGW3015, ScaleType.GSC_SGW3000
                info.AppendLine("  → ParseGscSimple")
            Case ScaleType.GSC_GST9600, ScaleType.GSC_GST9800
                info.AppendLine("  → ParseGscSimple")
            Case ScaleType.Sonic_SPS2000, ScaleType.Sonic_SPS3000, ScaleType.Sonic_SPS1000, ScaleType.Sonic_T7E
                info.AppendLine("  → ParseSonicWeight")
            Case ScaleType.Yaohua_A28E, ScaleType.Yaohua_A12E
                info.AppendLine("  → ParseYaohuaA28E (lalu fallback ParseYaohuaWeight)")
            Case ScaleType.Yaohua_A9, ScaleType.Yaohua_A7
                info.AppendLine("  → ParseYaohuaA9 (lalu fallback ParseYaohuaWeight)")
            Case ScaleType.Yaohua_T7
                info.AppendLine("  → ParseYaohuaWeight")
            Case ScaleType.AutoDetect
                info.AppendLine("  → ParseAutoDetect (coba semua parser berurutan)")
            Case Else
                info.AppendLine("  → Fallback berdasarkan brand: " & _scaleBrand.ToString())
        End Select

        Return info.ToString()
    End Function

    ' =============================================
    ' READ RAW DATA - UNTUK DEBUG
    ' =============================================
    Public Shared Function ReadRawData(timeoutMs As Integer) As String
        Try
            SyncLock _lockObject
                If _serialPort Is Nothing OrElse Not _serialPort.IsOpen Then
                    Return "ERROR: Port tidak terbuka!" & vbCrLf &
                           $"_serialPort Is Nothing: {_serialPort Is Nothing}" & vbCrLf &
                           $"_isConnected: {_isConnected}"
                End If

                Dim info As New System.Text.StringBuilder()
                info.AppendLine($"Port: {_serialPort.PortName}")
                info.AppendLine($"BaudRate: {_serialPort.BaudRate}")
                info.AppendLine($"DataBits: {_serialPort.DataBits}")
                info.AppendLine($"Parity: {_serialPort.Parity}")
                info.AppendLine($"StopBits: {_serialPort.StopBits}")
                info.AppendLine($"RtsEnable: {_serialPort.RtsEnable}")
                info.AppendLine($"DtrEnable: {_serialPort.DtrEnable}")
                info.AppendLine($"IsOpen: {_serialPort.IsOpen}")
                info.AppendLine()

                _serialPort.DiscardInBuffer()
                info.AppendLine("Buffer cleared. Menunggu data 5 detik...")
                info.AppendLine()

                Dim allData As New System.Text.StringBuilder()
                Dim startTime As DateTime = DateTime.Now
                Dim chunkCount As Integer = 0

                While (DateTime.Now - startTime).TotalMilliseconds < timeoutMs
                    Threading.Thread.Sleep(100)

                    If _serialPort.BytesToRead > 0 Then
                        Threading.Thread.Sleep(50)
                        Dim chunk As String = _serialPort.ReadExisting()
                        chunkCount += 1
                        allData.Append(chunk)

                        Dim clean As String = chunk.Replace(vbCr, "\r").Replace(vbLf, "\n")
                        info.AppendLine($"Chunk #{chunkCount} ({chunk.Length} chars): [{clean}]")

                        Try
                            info.AppendLine($"  HEX: {BitConverter.ToString(System.Text.Encoding.ASCII.GetBytes(chunk))}")
                        Catch exHex As Exception
                            Debug.WriteLine("[Diagnostic] HEX logging error: " & exHex.Message)
                        End Try
                    End If
                End While

                info.AppendLine()
                info.AppendLine($"Total chunks: {chunkCount}")
                info.AppendLine($"Total chars: {allData.Length}")

                If allData.Length = 0 Then
                    info.AppendLine()
                    info.AppendLine("*** TIDAK ADA DATA MASUK! ***")
                    info.AppendLine()
                    info.AppendLine("PENYEBAB PALING UMUM:")
                    info.AppendLine("1. Indikator belum set CONTINUOUS OUTPUT")
                    info.AppendLine("2. Baud Rate tidak cocok")
                    info.AppendLine("3. Kabel TX/RX tertukar")
                    info.AppendLine("4. COM Port salah")
                Else
                    info.AppendLine()
                    info.AppendLine("=== RAW DATA ===")
                    info.AppendLine($"[{allData.ToString().Replace(vbCr, "\r").Replace(vbLf, "\n")}]")
                End If

                Return info.ToString()
            End SyncLock
        Catch ex As Exception
            Debug.WriteLine("[SerialPortHelper.Diagnostic] Exception: " & ex.ToString())
            Return "EXCEPTION: Diagnostic operation failed."
        End Try
    End Function
End Class