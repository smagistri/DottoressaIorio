@echo off 
:: Check if the script is run as administrator
>nul 2>&1 "%SYSTEMROOT%\System32\cacls.exe" "%SYSTEMROOT%\System32\config\system" && (
    goto :runCommand
) || (
    echo Please run this script as an administrator.
    echo Exiting...    
    pause
    exit /b
)

:runCommand

rem Define the installation directory and service name
set "destinationFolder=C:\Program Files\DottoressaIorio\"
set "service=DottoressaIorio.App"

cd %destinationFolder%

rem Stop the service using NSSM
nssm stop %service%

rem Remove the service using NSSM (confirming the removal)
nssm remove %service% confirm

rem Remove all files and subdirectories in the installation directory except db_backup
for /D %%i in (*) do (
    if /I "%%i" neq "_db_backup" (
        rmdir /s /q "%%i"
    )
)

rem Remove all files in the installation directory except db_backup
for %%i in (*) do (
    if /I "%%i" neq "_db_backup" (
        del /q "%%i"
    )
)

cd /d "%~dp0"

pause