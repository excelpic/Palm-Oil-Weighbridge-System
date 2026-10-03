<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormLogin
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlSideColor = New System.Windows.Forms.Panel()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.btnSetting = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.linePass = New System.Windows.Forms.Panel()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lineUser = New System.Windows.Forms.Panel()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblAppSub = New System.Windows.Forms.Label()
        Me.lblAppTitle = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnlSideColor)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Controls.Add(Me.btnExit)
        Me.pnlMain.Controls.Add(Me.btnLogin)
        Me.pnlMain.Controls.Add(Me.linePass)
        Me.pnlMain.Controls.Add(Me.txtPassword)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.lineUser)
        Me.pnlMain.Controls.Add(Me.txtUsername)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.lblAppSub)
        Me.pnlMain.Controls.Add(Me.lblAppTitle)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(500, 450)
        Me.pnlMain.TabIndex = 0
        '
        'pnlSideColor
        '
        Me.pnlSideColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(83, Byte), Integer))
        Me.pnlSideColor.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSideColor.Location = New System.Drawing.Point(0, 0)
        Me.pnlSideColor.Name = "pnlSideColor"
        Me.pnlSideColor.Size = New System.Drawing.Size(5, 410)
        Me.pnlSideColor.TabIndex = 10
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.btnSetting)
        Me.pnlFooter.Controls.Add(Me.lblStatus)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 410)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(500, 40)
        Me.pnlFooter.TabIndex = 11
        '
        'btnSetting
        '
        Me.btnSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSetting.FlatAppearance.BorderSize = 0
        Me.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSetting.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetting.ForeColor = System.Drawing.Color.DimGray
        Me.btnSetting.Location = New System.Drawing.Point(448, 0)
        Me.btnSetting.Name = "btnSetting"
        Me.btnSetting.Size = New System.Drawing.Size(52, 40)
        Me.btnSetting.TabIndex = 1
        Me.btnSetting.Text = "⚙"
        Me.btnSetting.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Gray
        Me.lblStatus.Location = New System.Drawing.Point(12, 11)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(232, 18)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Status: Memeriksa koneksi..."
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Crimson
        Me.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Crimson
        Me.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Crimson
        Me.btnExit.Location = New System.Drawing.Point(219, 310)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(150, 45)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "KELUAR"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(83, Byte), Integer))
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.ForeColor = System.Drawing.Color.Black
        Me.btnLogin.Location = New System.Drawing.Point(49, 310)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(150, 45)
        Me.btnLogin.TabIndex = 8
        Me.btnLogin.Text = "LOGIN"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'linePass
        '
        Me.linePass.BackColor = System.Drawing.Color.DimGray
        Me.linePass.Location = New System.Drawing.Point(49, 275)
        Me.linePass.Name = "linePass"
        Me.linePass.Size = New System.Drawing.Size(350, 2)
        Me.linePass.TabIndex = 7
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPassword.ForeColor = System.Drawing.Color.White
        Me.txtPassword.Location = New System.Drawing.Point(49, 245)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txtPassword.Size = New System.Drawing.Size(350, 23)
        Me.txtPassword.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(83, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(45, 220)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "PASSWORD"
        '
        'lineUser
        '
        Me.lineUser.BackColor = System.Drawing.Color.DimGray
        Me.lineUser.Location = New System.Drawing.Point(49, 195)
        Me.lineUser.Name = "lineUser"
        Me.lineUser.Size = New System.Drawing.Size(350, 2)
        Me.lineUser.TabIndex = 4
        '
        'txtUsername
        '
        Me.txtUsername.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtUsername.ForeColor = System.Drawing.Color.White
        Me.txtUsername.Location = New System.Drawing.Point(49, 165)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(350, 23)
        Me.txtUsername.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(83, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(45, 140)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "USERNAME"
        '
        'lblAppSub
        '
        Me.lblAppSub.AutoSize = True
        Me.lblAppSub.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppSub.ForeColor = System.Drawing.Color.DarkGray
        Me.lblAppSub.Location = New System.Drawing.Point(45, 85)
        Me.lblAppSub.Name = "lblAppSub"
        Me.lblAppSub.Size = New System.Drawing.Size(185, 23)
        Me.lblAppSub.TabIndex = 1
        Me.lblAppSub.Text = "Secure Access Terminal"
        '
        'lblAppTitle
        '
        Me.lblAppTitle.AutoSize = True
        Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 19.0!, System.Drawing.FontStyle.Bold)
        Me.lblAppTitle.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblAppTitle.Location = New System.Drawing.Point(40, 30)
        Me.lblAppTitle.Name = "lblAppTitle"
        Me.lblAppTitle.Size = New System.Drawing.Size(410, 45)
        Me.lblAppTitle.TabIndex = 0
        Me.lblAppTitle.Text = "SISTEM TIMBANGAN PKS"
        '
        'FormLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(500, 450)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FormLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlFooter.ResumeLayout(False)
        Me.pnlFooter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents lblAppTitle As Label
    Friend WithEvents lblAppSub As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents lineUser As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents linePass As Panel
    Friend WithEvents btnLogin As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnSetting As Button
    Friend WithEvents pnlSideColor As Panel
End Class