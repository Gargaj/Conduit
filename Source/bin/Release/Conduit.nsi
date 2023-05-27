
Name "Conduit"
OutFile "conduit_setup.exe"

!define CLIENT_NAME "Conduit"

RequestExecutionLevel admin

InstallDir "$PROGRAMFILES\Conduit"

LoadLanguageFile "${NSISDIR}\Contrib\Language files\English.nlf"

VIProductVersion "1.0.0.0"
VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "Conduit installer"
VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" ""
VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" ""
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "1.0"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "Installer application for Conduit"

ReserveFile "${NSISDIR}\Plugins\x86-ansi\InstallOptions.dll"

Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

Section "install"
  SetOutPath $INSTDIR
  File "Conduit.exe"
  File "Newtonsoft.Json.dll"
  File "SevenZipExtractor.dll"
  CreateDirectory $INSTDIR\x64
  CreateDirectory $INSTDIR\x86
  File /oname=x64\7z.dll "x64\7z.dll"
  File /oname=x86\7z.dll "x86\7z.dll"
  WriteUninstaller $INSTDIR\Uninstall.exe
  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${CLIENT_NAME}" "DisplayName" "${CLIENT_NAME}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${CLIENT_NAME}" "UninstallString" "$INSTDIR\Uninstall.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${CLIENT_NAME}" "InstallLocation" "$INSTDIR"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${CLIENT_NAME}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${CLIENT_NAME}" "NoRepair" 1

  Exec '"$INSTDIR\Conduit.exe" -registerScheme'
  
SectionEnd

Section "Uninstall"
  SetOutPath $PROGRAMFILES32
  Delete "$INSTDIR\Conduit.exe"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\SevenZipExtractor.dll"
  Delete "$INSTDIR\Uninstall.exe"
  Delete "$INSTDIR\x64\7z.dll"
  Delete "$INSTDIR\x86\7z.dll"
  RMDir $INSTDIR
  RMDir /r "$SMPROGRAMS\${CLIENT_NAME}"
  Delete "$DESKTOP\${CLIENT_NAME}.lnk"
  Delete "$QUICKLAUNCH\${CLIENT_NAME}.lnk"
  
  DeleteRegKey HKCR "Conduit"
 
SectionEnd

