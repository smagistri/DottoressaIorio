@echo off
set "destinationFolder=C:\Program Files\DottoressaIorio\"
set "service=DottoressaIorio.App"

nssm stop %service%
nssm remove %service% confirm
rmdir /s /q "%destinationFolder%"