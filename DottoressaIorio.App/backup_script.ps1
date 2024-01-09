$sourcePath = "C:\Program Files\DottoressaIorio\app.db"
$backupPath = "C:\Program Files\DottoressaIorio\_db_backup\"

Copy-Item $sourcePath "$backupPath\app_backup_$(Get-Date -Format 'yyyyMMddHHmmss').db"
