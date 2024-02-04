@echo off
setlocal

rem Define the destination folder for publishing
set "destinationFolder=C:\Users\stefa\repo\DottoressaIorio\Setup\Service"

rem Publish the .NET Core application to the destination folder
dotnet publish -c Release -o %destinationFolder%

rem Copy utility files (libwkhtmltox.dll and nssm.exe) to the destination folder
copy .\_utility\libwkhtmltox.dll %destinationFolder%
copy .\_utility\nssm.exe %destinationFolder%

rem Navigate to the Installer folder
cd ..\Setup\Installer

rem Delete the existing Service.7z file
del "Service.7z"

rem Create a new compressed archive (zip) of the Service folder
7z a -tzip "Service.7z" "..\Service\*"
