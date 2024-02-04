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

setlocal 

rem Change to the directory where the script is located
cd /d "%~dp0"

rem Define source file, destination folder, and service-related variables
set "sourceFile=Service.7z"
set "destinationFolder=C:\Program Files\DottoressaIorio\"
set "service=DottoressaIorio.App"
set "app=DottoressaIorio.App.exe"

rem Create the destination folder if it does not exist
if not exist "%destinationFolder%" mkdir "%destinationFolder%"

rem Extract files from the 7z archive to the destination folder
7z x %sourceFile% -o"%destinationFolder%" *.* -r -y

rem Check if .NET Core runtime is installed using winget
winget list -e | find "Microsoft.NET.Runtime.6" > nul

if %errorlevel% neq 0 (
    rem .NET Core runtime is not installed, install it
    echo Installing .NET 6 Runtime using winget...
    winget install Microsoft.DotNet.Runtime.6
    winget install Microsoft.DotNet.AspNetCore.6
)

rem Navigate to the destination folder
cd "%destinationFolder%"

rem Install the service using NSSM
nssm install %service% "%destinationFolder%\%app%"

rem Start the installed service using NSSM
nssm start %service%

rem Open the default browser to the application's URL
start http://localhost:5000