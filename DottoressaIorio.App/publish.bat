@echo off
setlocal

rem Define the destination folder for publishing
set "destinationFolder=..\Setup\Service"

rem Publish the .NET Core application to the destination folder
dotnet publish -c Release -o %destinationFolder%

rem Copy utility files to the destination folder
copy ..\Utility\*.* %destinationFolder%

:: Create a directory named "db_backup" inside the destinationFolder
mkdir %destinationFolder%\_db_backup

rem Navigate to the Installer folder
cd ..\Setup

rem Delete the existing Service.7z file
del "Service.7z"

rem Create a new compressed archive (zip) of the Service folder
7z a -tzip "Service.7z" ".\Service\*"

rem Remove the publish directory and its contents
rmdir /s /q ".\Service"
