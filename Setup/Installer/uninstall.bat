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

rem Remove the installation directory and its contents
rmdir /s /q "%destinationFolder%"

rem Pause to keep the console window open for viewing any output
pause
