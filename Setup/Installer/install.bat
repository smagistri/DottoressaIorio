@echo off
set "sourceFile=Service.7z"
set "destinationFolder=C:\Program Files\DottoressaIorio\"
set "service=DottoressaIorio.App"
set "app=Service\DottoressaIorio.App.exe"

nssm stop %service%
nssm remove %service% confirm

if not exist "%destinationFolder%" mkdir "%destinationFolder%"
7z x %sourceFile% -o"%destinationFolder%" *.* -r -y

nssm install %service% "%destinationFolder%\%app%"
nssm start %service%

start http://localhost:5000