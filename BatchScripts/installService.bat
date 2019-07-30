@ECHO OFF

set CURRENT_DIR=%~dp0
set CURRENT_DIR=%CURRENT_DIR:\BatchScripts=%
echo Current Running Directory %CURRENT_DIR% 

REM The following directory is for .NET 4.0
set DOTNETFX4=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX4%

set INSTALLFILE=%CURRENT_DIR%UpdateMyEFPassword.exe

echo Install Path : %INSTALLFILE%
echo Installing MyService...
echo ---------------------------------------------------
InstallUtil -i %INSTALLFILE%
echo ---------------------------------------------------
echo Done.
