$sourcePath = "C:\Program Files\DottoressaIorio\app.db"
$backupPath = "C:\Program Files\DottoressaIorio\_db_backup\"

# Check if the backup folder exists, create it if not
if (-not (Test-Path $backupPath -PathType Container)) {
    New-Item -Path $backupPath -ItemType Directory
}

# Copy the item with a timestamped backup file name
Copy-Item $sourcePath "$backupPath\app_backup_$(Get-Date -Format 'yyyyMMddHHmmss').db"
