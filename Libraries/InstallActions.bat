@echo off

rem Retrieve the installation directory (TARGETDIR)
set INSTALLDIR=%1

rem Use INSTALLDIR in your commands
nssm install DottoressaIorio.App "%INSTALLDIR%\DottoressaIorio.App.exe"
nssm start DottoressaIorio.App