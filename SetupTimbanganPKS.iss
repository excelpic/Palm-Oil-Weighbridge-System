; =============================================
; Inno Setup Script untuk Sistem Timbangan PKS
; =============================================

#define MyAppName "Sistem Timbangan PKS"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Nama Perusahaan Anda"
#define MyAppExeName "SistemTimbanganPKS.exe"

[Setup]
AppId={{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputDir=Output
OutputBaseFilename=Setup_TimbanganPKS_v{#MyAppVersion}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Buat shortcut di Desktop"; GroupDescription: "Shortcut:"; Flags: unchecked

[Files]
; File EXE utama
Source: "bin\Release\SistemTimbanganPKS.exe"; DestDir: "{app}"; Flags: ignoreversion

; Semua file DLL
Source: "bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

; File konfigurasi
Source: "bin\Release\*.config"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

; File XML (jika ada)
Source: "bin\Release\*.xml"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

; Folder Reports (RDLC)
Source: "bin\Release\Reports\*"; DestDir: "{app}\Reports"; Flags: ignoreversion recursesubdirs createallsubdirs skipifsourcedoesntexist

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Uninstall {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Jalankan Sistem Timbangan PKS"; Flags: nowait postinstall skipifsilent