$sourcePath = "C:\Program Files\DottoressaIorio\app.db"
$backupPath = "C:\Program Files\DottoressaIorio\_db_backup\"
$serviceName = "DottoressaIorio.App"

# Check if the backup folder exists, create it if not
if (-not (Test-Path $backupPath -PathType Container)) {
    New-Item -Path $backupPath -ItemType Directory
}

# Stop the service using NSSM
.\nssm stop $serviceName

# Copy the item with a timestamped backup file name
Copy-Item $sourcePath "$backupPath\app_backup_$(Get-Date -Format 'yyyyMMddHHmmss').db"

# Start the installed service using NSSM
.\nssm start $serviceName