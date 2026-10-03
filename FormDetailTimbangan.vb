' =============================================
' FormDetailTimbangan.vb
' Form Detail Timbangan - Modern Professional UI
' =============================================
Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class FormDetailTimbangan
    Inherits System.Windows.Forms.Form

    Private _timbangID As Integer
    Private _row As DataRow

    ' =============================================
    ' CONSTRUCTOR
    ' =============================================
    Public Sub New(timbangID As Integer, row As DataRow)
        _timbangID = timbangID
        _row = row
        InitializeComponent()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or
                    ControlStyles.AllPaintingInWmPaint Or
                    ControlStyles.UserPaint, True)
    End Sub

    ' =============================================
    ' WARNA TEMA
    ' =============================================
    Private ReadOnly clrNavy As Color = Color.FromArgb(25, 42, 86)
    Private ReadOnly clrBlue As Color = Color.FromArgb(0, 122, 204)
    Private ReadOnly clrGreen As Color = Color.FromArgb(39, 174, 96)
    Private ReadOnly clrOrange As Color = Color.FromArgb(230, 126, 34)
    Private ReadOnly clrRed As Color = Color.FromArgb(192, 57, 43)
    Private ReadOnly clrPurple As Color = Color.FromArgb(142, 68, 173)
    Private ReadOnly clrBg As Color = Color.FromArgb(245, 247, 250)
    Private ReadOnly clrCard As Color = Color.White
    Private ReadOnly clrBorder As Color = Color.FromArgb(220, 225, 235)
    Private ReadOnly clrTextDark As Color = Color.FromArgb(30, 30, 30)
    Private ReadOnly clrTextGray As Color = Color.FromArgb(120, 120, 130)

    ' =============================================
    ' FORM LOAD
    ' =============================================
    Private Sub FormDetailTimbangan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.BackColor = clrBg
            BuildUI()
        Catch ex As Exception
            Debug.WriteLine("[FormDetailTimbangan.Load] Error: " & ex.ToString())
            MessageBox.Show("Gagal memuat detail timbangan.")
        End Try
    End Sub

    ' =============================================
    ' BUILD SELURUH UI SECARA DINAMIS
    ' =============================================
    Private Sub BuildUI()
        Me.Controls.Clear()
        Me.Size = New Size(780, 820)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        Dim noTiket As String = SafeStr("NoTiket")
        Dim transType As String = SafeStr("TransType", "JUAL")
        Dim status As String = SafeStr("Status", "MASUK")
        Dim noPolisi As String = SafeStr("NoPolisi")
        Dim namaSupir As String = SafeStr("NamaSupir")
        Dim noDO As String = SafeStr("NoDO")
        Dim noKontrak As String = SafeStr("NoKontrak")
        Dim customerNama As String = SafeStr("CustomerNama")
        Dim productNama As String = SafeStr("ProductNama")
        Dim transporterNama As String = SafeStr("TransporterNama")
        Dim keterangan As String = SafeStr("Keterangan", "")
        Dim jumlahCetak As String = SafeStr("JumlahCetak", "0")
        Dim tglMasuk As String = SafeDate("TanggalMasuk", "dd/MM/yyyy HH:mm")
        Dim tglKeluar As String = SafeDate("TanggalKeluar", "dd/MM/yyyy HH:mm")
        Dim beratMasuk As Decimal = SafeDec("BeratMasuk")
        Dim beratKeluar As Decimal = SafeDec("BeratKeluar")
        Dim beratNetto As Decimal = SafeDec("BeratNetto")
        Dim potonganPersen As Decimal = SafeDec("PotonganPersen")
        Dim potonganKg As Decimal = SafeDec("PotonganCong")
        Dim totalPotongan As Decimal = SafeDec("TotalPotongan")
        Dim beratBersih As Decimal = SafeDec("BeratBersih")
        Dim includeFFA As Boolean = SafeBool("IncludeFFA")
        Dim ffa As Decimal = SafeDec("FFA")
        Dim moisture As Decimal = SafeDec("Moisture")
        Dim dirt As Decimal = SafeDec("Dirt")
        Dim suhuMinyak As Decimal = SafeDec("SuhuMinyak")

        Dim statusColor As Color = If(status = "SELESAI", clrGreen,
                                   If(status = "MASUK", clrOrange, clrRed))
        Dim transColor As Color = If(transType = "JUAL", clrGreen, clrBlue)

        Me.Text = "Detail Timbangan — " & noTiket

        ' ══════════════════════════════
        ' HEADER
        ' ══════════════════════════════
        Dim pnlHeader As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 90,
            .BackColor = clrNavy
        }
        AddHandler pnlHeader.Paint, AddressOf PaintHeaderGradient
        Me.Controls.Add(pnlHeader)

        Dim lblJudul As New Label() With {
            .Text = "DETAIL TIMBANGAN",
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(20, 14)
        }
        pnlHeader.Controls.Add(lblJudul)

        Dim lblNoTiketHeader As New Label() With {
            .Text = noTiket,
            .Font = New Font("Consolas", 11, FontStyle.Bold),
            .ForeColor = Color.FromArgb(160, 200, 255),
            .AutoSize = True,
            .Location = New Point(22, 52)
        }
        pnlHeader.Controls.Add(lblNoTiketHeader)

        Dim lblStatusBadge As New Label() With {
            .Text = "  " & status & "  ",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = statusColor,
            .AutoSize = True,
            .Location = New Point(630, 18),
            .Padding = New Padding(6, 3, 6, 3)
        }
        pnlHeader.Controls.Add(lblStatusBadge)

        Dim lblTransBadge As New Label() With {
            .Text = "  " & transType & "  ",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = transColor,
            .AutoSize = True,
            .Location = New Point(630, 52),
            .Padding = New Padding(6, 3, 6, 3)
        }
        pnlHeader.Controls.Add(lblTransBadge)

        ' ══════════════════════════════
        ' BOTTOM BAR
        ' ══════════════════════════════
        Dim pnlBottom As New Panel() With {
            .Dock = DockStyle.Bottom,
            .Height = 56,
            .BackColor = Color.FromArgb(235, 238, 245)
        }
        Me.Controls.Add(pnlBottom)

        Dim btnTutup As New Button() With {
            .Text = "  Tutup",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = clrNavy,
            .FlatStyle = FlatStyle.Flat,
            .Size = New Size(120, 36),
            .Location = New Point(Me.ClientSize.Width - 140, 10),
            .Cursor = Cursors.Hand
        }
        btnTutup.FlatAppearance.BorderSize = 0
        AddHandler btnTutup.Click, Sub(s, ev) Me.Close()
        pnlBottom.Controls.Add(btnTutup)

        If status.ToUpperInvariant() = "SELESAI" AndAlso UserSession.CanReprintStruk() Then
            Dim btnCetak As New Button() With {
                .Text = "  🖨  Cetak Ulang",
                .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .ForeColor = Color.White,
                .BackColor = clrBlue,
                .FlatStyle = FlatStyle.Flat,
                .Size = New Size(155, 36),
                .Location = New Point(Me.ClientSize.Width - 305, 10),
                .Cursor = Cursors.Hand
            }
            btnCetak.FlatAppearance.BorderSize = 0
            AddHandler btnCetak.Click, Sub(s, ev)
                                           PrintHelper.CetakDariDatabase(_timbangID, isDuplikat:=True)
                                       End Sub
            pnlBottom.Controls.Add(btnCetak)
        End If

        ' ══════════════════════════════
        ' SCROLL PANEL
        ' ══════════════════════════════
        Dim pnlScroll As New Panel() With {
            .AutoScroll = True,
            .Dock = DockStyle.Fill,
            .BackColor = clrBg,
            .Padding = New Padding(14, 10, 14, 10)
        }
        Me.Controls.Add(pnlScroll)
        pnlScroll.BringToFront()

        Dim cardW As Integer = 740
        Dim y As Integer = 8

        ' ══════════════════════════════════════════════════
        ' CARD 1 — DATA KENDARAAN
        ' ══════════════════════════════════════════════════
        Dim card1H As Integer = 212
        Dim card1 As Panel = CreateCard(pnlScroll, y, cardW, card1H)
        y += card1H + 8

        AddCardTitle(card1, "🚛  DATA KENDARAAN", clrNavy)

        Dim lbX As Integer = 14
        Dim lvX As Integer = 110
        Dim rbX As Integer = 390
        Dim rvX As Integer = 490
        Dim lvW As Integer = 270
        Dim rvW As Integer = 240
        Dim rH As Integer = 28
        Dim cy As Integer = 40

        AddFieldPair(card1, cy, lbX, lvX, lvW, "No. Polisi", noPolisi,
                     rbX, rvX, rvW, "Nama Supir", namaSupir,
                     clrNavy, FontStyle.Bold)
        cy += rH

        AddFieldPair(card1, cy, lbX, lvX, lvW, "No. DO", noDO,
                     rbX, rvX, rvW, "No. Kontrak", noKontrak)
        cy += rH

        AddFieldPair(card1, cy, lbX, lvX, lvW, "Customer", customerNama,
                     rbX, rvX, rvW, "Transporter", transporterNama)
        cy += rH

        AddFieldPair(card1, cy, lbX, lvX, lvW, "Product", productNama,
                     rbX, rvX, rvW, "", "")
        cy += rH

        Dim lineDiv As New Panel() With {
            .Location = New Point(14, cy + 4),
            .Size = New Size(cardW - 28, 1),
            .BackColor = Color.FromArgb(230, 233, 242)
        }
        card1.Controls.Add(lineDiv)
        cy += 12

        AddFieldPair(card1, cy, lbX, lvX, lvW, "Tgl. Masuk", tglMasuk,
                     rbX, rvX, rvW, "Tgl. Keluar", tglKeluar,
                     clrBlue, FontStyle.Regular, clrBlue)
        cy += rH

        ' ══════════════════════════════════════════════════
        ' CARD 2 — DATA BERAT (FIXED - RAPI SEPERTI REFERENSI)
        ' ══════════════════════════════════════════════════
        Dim card2H As Integer = 220   ' <-- dinaikkan agar baris ke-3 kanan tidak terpotong
        Dim card2 As Panel = CreateCard(pnlScroll, y, cardW, card2H)
        y += card2H + 8

        AddCardTitle(card2, "⚖  DATA BERAT", clrRed)

        Dim vDiv As New Panel() With {
    .Location = New Point(370, 32),
    .Size = New Size(1, card2H - 40),
    .BackColor = Color.FromArgb(220, 225, 235)
}
        card2.Controls.Add(vDiv)

        ' === KOLOM KIRI: Bruto, Tara, Netto ===
        Dim bcy As Integer = 40
        AddWeightRow(card2, bcy, 18, "BRUTO (Masuk)",
             beratMasuk.ToString("N0") & " KG", clrRed)
        bcy += 60

        AddWeightRow(card2, bcy, 18, "TARA (Keluar)",
             beratKeluar.ToString("N0") & " KG", clrBlue)
        bcy += 60

        AddWeightRow(card2, bcy, 18, "NETTO",
             beratNetto.ToString("N0") & " KG", clrTextDark)

        ' === KOLOM KANAN: Potongan ===
        ' pcy start 40, step 60 → baris ke-3 ada di Y=160
        ' value ada di Y+22 = 182, tinggi 28 → butuh sampai Y=210
        ' maka card2H minimal 220
        Dim pcy As Integer = 40
        Dim pLbX As Integer = 386
        Dim pLvX As Integer = 500
        Dim pLvW As Integer = 220

        AddFieldRow2(card2, pcy, pLbX, pLvX, pLvW,
             "Potongan (%)", potonganPersen.ToString("N2") & " %", clrOrange, FontStyle.Bold)
        pcy += 60

        AddFieldRow2(card2, pcy, pLbX, pLvX, pLvW,
             "Potongan (Kg)", potonganKg.ToString("N0") & " KG", clrOrange, FontStyle.Bold)
        pcy += 60

        AddFieldRow2(card2, pcy, pLbX, pLvX, pLvW,
             "Total Potongan", totalPotongan.ToString("N0") & " KG",
             clrOrange, FontStyle.Bold)

        ' ══════════════════════════════════════════════════
        ' CARD 3 — BERAT BERSIH (NETTO PAID)
        ' ══════════════════════════════════════════════════
        Dim card3H As Integer = 80
        Dim card3 As Panel = CreateCard(pnlScroll, y, cardW, card3H, clrNavy)
        y += card3H + 8

        Dim lblBBTitle As New Label() With {
            .Text = "BERAT BERSIH (NETTO PAID)",
            .Font = New Font("Segoe UI", 9.5, FontStyle.Bold),
            .ForeColor = Color.FromArgb(160, 200, 255),
            .AutoSize = True,
            .Location = New Point(18, 8)
        }
        card3.Controls.Add(lblBBTitle)

        Dim lblBBNum As New Label() With {
            .Text = beratBersih.ToString("N0"),
            .Font = New Font("Segoe UI", 26, FontStyle.Bold),
            .ForeColor = Color.FromArgb(0, 230, 118),
            .AutoSize = True,
            .Location = New Point(18, 26)
        }
        card3.Controls.Add(lblBBNum)

        Dim bbNumText As String = beratBersih.ToString("N0")
        Dim bbNumFont As New Font("Segoe UI", 26, FontStyle.Bold)
        Dim bbNumSize As Size = TextRenderer.MeasureText(bbNumText, bbNumFont)
        Dim numWidth As Integer = bbNumSize.Width

        Dim lblBBUnit As New Label() With {
            .Text = "KG",
            .Font = New Font("Segoe UI", 15, FontStyle.Bold),
            .ForeColor = Color.FromArgb(0, 200, 100),
            .AutoSize = True,
            .Location = New Point(18 + numWidth + 4, 38)
        }
        card3.Controls.Add(lblBBUnit)

        Dim lblCetak As New Label() With {
            .Text = "🖨  Dicetak: " & jumlahCetak & "x",
            .Font = New Font("Segoe UI", 9),
            .ForeColor = Color.FromArgb(170, 210, 255),
            .AutoSize = True,
            .Location = New Point(cardW - 130, 28)
        }
        card3.Controls.Add(lblCetak)

        ' ══════════════════════════════════════════════════
        ' CARD 4 — ANALISA LAB
        ' ══════════════════════════════════════════════════
        If includeFFA Then
            Dim card4H As Integer = 102
            Dim card4 As Panel = CreateCard(pnlScroll, y, cardW, card4H)
            y += card4H + 8

            AddCardTitle(card4, "🔬  ANALISA LAB", clrPurple)

            Dim cellW As Integer = 174
            Dim cellGap As Integer = 6
            Dim cellStartX As Integer = 14
            Dim cellY As Integer = 40

            AddAnalysisCell(card4, cellStartX, cellY, cellW, "FFA (Asam)",
                            ffa.ToString("N2") & " %", clrPurple)
            AddAnalysisCell(card4, cellStartX + (cellW + cellGap), cellY, cellW,
                            "Moisture (Air)", moisture.ToString("N2") & " %", clrBlue)
            AddAnalysisCell(card4, cellStartX + (cellW + cellGap) * 2, cellY, cellW,
                            "Dirt (Kotoran)", dirt.ToString("N2") & " %", clrOrange)
            AddAnalysisCell(card4, cellStartX + (cellW + cellGap) * 3, cellY, cellW,
                            "Suhu Minyak", suhuMinyak.ToString("N1") & " °C", clrRed)
        End If

        ' ══════════════════════════════════════════════════
        ' CARD 5 — KETERANGAN
        ' ══════════════════════════════════════════════════
        If Not String.IsNullOrWhiteSpace(keterangan) AndAlso keterangan <> "-" Then
            Dim card5H As Integer = 68
            Dim card5 As Panel = CreateCard(pnlScroll, y, cardW, card5H)
            y += card5H + 8

            AddCardTitle(card5, "📝  KETERANGAN", clrGreen)

            Dim lblKet As New Label() With {
                .Text = keterangan,
                .Font = New Font("Segoe UI", 9.5),
                .ForeColor = clrTextDark,
                .Location = New Point(16, 38),
                .Size = New Size(cardW - 30, 22),
                .AutoEllipsis = True
            }
            card5.Controls.Add(lblKet)
        End If

        ' ══════════════════════════════════════════════════
        ' INFO BAR
        ' ══════════════════════════════════════════════════
        If Not UserSession.CanEditTimbangan() Then
            Dim infoBar As New Panel() With {
                .Location = New Point(0, y),
                .Size = New Size(cardW, 38),
                .BackColor = Color.FromArgb(255, 249, 230)
            }
            AddHandler infoBar.Paint, Sub(s, e)
                                          Using pen As New Pen(Color.FromArgb(255, 224, 130), 1)
                                              e.Graphics.DrawRectangle(pen, 0, 0,
                                                  infoBar.Width - 1, infoBar.Height - 1)
                                          End Using
                                      End Sub
            pnlScroll.Controls.Add(infoBar)

            Dim lblInfo As New Label() With {
                .Text = "ℹ  Untuk mengedit data ini, hubungi Manager / Direktur.",
                .Font = New Font("Segoe UI", 9),
                .ForeColor = Color.FromArgb(160, 100, 0),
                .Location = New Point(12, 10),
                .AutoSize = True
            }
            infoBar.Controls.Add(lblInfo)
            y += 46
        End If

    End Sub

    ' ════════════════════════════════════════════════════
    ' HELPER: GRADIENT HEADER
    ' ════════════════════════════════════════════════════
    Private Sub PaintHeaderGradient(sender As Object, e As PaintEventArgs)
        Dim pnl As Panel = DirectCast(sender, Panel)
        Using br As New Drawing2D.LinearGradientBrush(
            pnl.ClientRectangle,
            Color.FromArgb(25, 42, 86),
            Color.FromArgb(41, 65, 122),
            Drawing2D.LinearGradientMode.Horizontal)
            e.Graphics.FillRectangle(br, pnl.ClientRectangle)
        End Using
    End Sub

    ' ════════════════════════════════════════════════════
    ' HELPER: BUAT CARD
    ' ════════════════════════════════════════════════════
    Private Function CreateCard(parent As Control, y As Integer,
                                 w As Integer, h As Integer,
                                 Optional bgColor As Color = Nothing) As Panel
        If bgColor = Nothing OrElse bgColor = Color.Empty Then
            bgColor = Color.White
        End If

        Dim shadow As New Panel() With {
            .Location = New Point(3, y + 3),
            .Size = New Size(w, h),
            .BackColor = Color.FromArgb(18, 0, 0, 0)
        }
        parent.Controls.Add(shadow)

        Dim card As New Panel() With {
            .Location = New Point(0, y),
            .Size = New Size(w, h),
            .BackColor = bgColor
        }
        AddHandler card.Paint, Sub(s, e)
                                   Using pen As New Pen(Color.FromArgb(220, 225, 235), 1)
                                       e.Graphics.DrawRectangle(pen, 0, 0,
                                           card.Width - 1, card.Height - 1)
                                   End Using
                               End Sub
        parent.Controls.Add(card)
        card.BringToFront()
        Return card
    End Function

    ' ════════════════════════════════════════════════════
    ' HELPER: JUDUL CARD
    ' ════════════════════════════════════════════════════
    Private Sub AddCardTitle(card As Panel, title As String, color As Color)
        Dim bar As New Panel() With {
            .Location = New Point(0, 0),
            .Size = New Size(4, card.Height),
            .BackColor = color
        }
        card.Controls.Add(bar)

        Dim lbl As New Label() With {
            .Text = title,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = color,
            .AutoSize = True,
            .Location = New Point(14, 10)
        }
        card.Controls.Add(lbl)

        Dim line As New Panel() With {
            .Location = New Point(14, 30),
            .Size = New Size(card.Width - 18, 1),
            .BackColor = Color.FromArgb(230, 233, 242)
        }
        card.Controls.Add(line)
    End Sub

    ' ════════════════════════════════════════════════════
    ' HELPER: FIELD PAIR
    ' ════════════════════════════════════════════════════
    Private Sub AddFieldPair(card As Panel, y As Integer,
                              lbX As Integer, lvX As Integer, lvW As Integer,
                              lLabel As String, lValue As String,
                              rbX As Integer, rvX As Integer, rvW As Integer,
                              rLabel As String, rValue As String,
                              Optional lValueColor As Color = Nothing,
                              Optional lValueStyle As FontStyle = FontStyle.Regular,
                              Optional rValueColor As Color = Nothing)

        If lValueColor = Nothing OrElse lValueColor = Color.Empty Then
            lValueColor = clrTextDark
        End If
        If rValueColor = Nothing OrElse rValueColor = Color.Empty Then
            rValueColor = clrTextDark
        End If
        If String.IsNullOrEmpty(lValue) OrElse lValue = "-" Then lValue = "—"
        If String.IsNullOrEmpty(rValue) OrElse rValue = "-" Then rValue = "—"

        card.Controls.Add(New Label() With {
            .Text = lLabel,
            .Font = New Font("Segoe UI", 8.5),
            .ForeColor = clrTextGray,
            .Location = New Point(lbX, y),
            .Size = New Size(lvX - lbX - 2, 22)
        })
        card.Controls.Add(New Label() With {
            .Text = lValue,
            .Font = New Font("Segoe UI", 9.5, lValueStyle),
            .ForeColor = lValueColor,
            .Location = New Point(lvX, y),
            .Size = New Size(lvW, 22),
            .AutoEllipsis = True
        })

        If Not String.IsNullOrEmpty(rLabel) Then
            card.Controls.Add(New Label() With {
                .Text = rLabel,
                .Font = New Font("Segoe UI", 8.5),
                .ForeColor = clrTextGray,
                .Location = New Point(rbX, y),
                .Size = New Size(rvX - rbX - 2, 22)
            })
            card.Controls.Add(New Label() With {
                .Text = rValue,
                .Font = New Font("Segoe UI", 9.5, FontStyle.Regular),
                .ForeColor = rValueColor,
                .Location = New Point(rvX, y),
                .Size = New Size(rvW, 22),
                .AutoEllipsis = True
            })
        End If
    End Sub

    ' ════════════════════════════════════════════════════
    ' HELPER: WEIGHT ROW (FIXED - SPACING LEBIH BAIK)
    ' ════════════════════════════════════════════════════
    Private Sub AddWeightRow(card As Panel, y As Integer, x As Integer,
                              label As String, value As String, color As Color)
        card.Controls.Add(New Label() With {
            .Text = label,
            .Font = New Font("Segoe UI", 8.5),
            .ForeColor = clrTextGray,
            .Location = New Point(x, y),
            .AutoSize = True
        })
        card.Controls.Add(New Label() With {
            .Text = value,
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = color,
            .Location = New Point(x, y + 22),
            .AutoSize = True
        })
    End Sub

    ' ════════════════════════════════════════════════════
    ' HELPER: FIELD ROW 2 (FIXED - SEJAJAR DENGAN KIRI)
    ' ════════════════════════════════════════════════════
    Private Sub AddFieldRow2(card As Panel, y As Integer,
                              labelX As Integer, valueX As Integer, valueW As Integer,
                              labelText As String, valueText As String,
                              valueColor As Color,
                              Optional valueStyle As FontStyle = FontStyle.Regular)
        card.Controls.Add(New Label() With {
            .Text = labelText,
            .Font = New Font("Segoe UI", 8.5),
            .ForeColor = clrTextGray,
            .Location = New Point(labelX, y),
            .Size = New Size(valueX - labelX - 4, 22)
        })
        card.Controls.Add(New Label() With {
            .Text = valueText,
            .Font = New Font("Segoe UI", 14, valueStyle),
            .ForeColor = valueColor,
            .Location = New Point(valueX, y + 20),
            .Size = New Size(valueW, 28),
            .AutoEllipsis = True
        })
    End Sub

    ' ════════════════════════════════════════════════════
    ' HELPER: ANALYSIS CELL
    ' ════════════════════════════════════════════════════
    Private Sub AddAnalysisCell(card As Panel, x As Integer, y As Integer,
                                 cellW As Integer,
                                 label As String, value As String, color As Color)
        Dim cell As New Panel() With {
            .Location = New Point(x, y),
            .Size = New Size(cellW, 52),
            .BackColor = Color.FromArgb(248, 249, 252)
        }

        AddHandler cell.Paint, Sub(s, e)
                                   Using pen As New Pen(color, 1.5F)
                                       e.Graphics.DrawRectangle(pen, 0, 0,
                                           cell.Width - 1, cell.Height - 1)
                                   End Using
                                   Using br As New SolidBrush(color)
                                       e.Graphics.FillRectangle(br,
                                           New Rectangle(0, 0, cell.Width, 3))
                                   End Using
                               End Sub
        card.Controls.Add(cell)

        cell.Controls.Add(New Label() With {
            .Text = label,
            .Font = New Font("Segoe UI", 7.5),
            .ForeColor = Color.FromArgb(130, 130, 140),
            .Location = New Point(7, 7),
            .AutoSize = True
        })
        cell.Controls.Add(New Label() With {
            .Text = value,
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .ForeColor = color,
            .Location = New Point(6, 22),
            .AutoSize = True
        })
    End Sub

    ' ════════════════════════════════════════════════════
    ' HELPER: SAFE DATA ACCESS
    ' ════════════════════════════════════════════════════
    Private Function SafeStr(col As String, Optional def As String = "-") As String
        Try
            If _row.Table.Columns.Contains(col) AndAlso
               Not IsDBNull(_row(col)) Then
                Dim v As String = _row(col).ToString().Trim()
                Return If(String.IsNullOrEmpty(v), def, v)
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormDetailTimbangan.SafeStr] Error: " & ex.ToString())
        End Try
        Return def
    End Function

    Private Function SafeDec(col As String) As Decimal
        Try
            If _row.Table.Columns.Contains(col) AndAlso
               Not IsDBNull(_row(col)) Then
                Return CDec(_row(col))
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormDetailTimbangan.SafeDec] Error: " & ex.ToString())
        End Try
        Return 0D
    End Function

    Private Function SafeBool(col As String) As Boolean
        Try
            If _row.Table.Columns.Contains(col) AndAlso
               Not IsDBNull(_row(col)) Then
                Return CBool(_row(col))
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormDetailTimbangan.SafeBool] Error: " & ex.ToString())
        End Try
        Return False
    End Function

    Private Function SafeDate(col As String, fmt As String) As String
        Try
            If _row.Table.Columns.Contains(col) AndAlso
               Not IsDBNull(_row(col)) Then
                Return CDate(_row(col)).ToString(fmt)
            End If
        Catch ex As Exception
            Debug.WriteLine("[FormDetailTimbangan.SafeDate] Error: " & ex.ToString())
        End Try
        Return "—"
    End Function

    ' ════════════════════════════════════════════════════
    ' REQUIRED: InitializeComponent
    ' ════════════════════════════════════════════════════
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.AutoScaleDimensions = New SizeF(96, 96)
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.ResumeLayout(False)
    End Sub

End Class