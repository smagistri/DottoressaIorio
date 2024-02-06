$sourcePath = "C:\Program Files\DottoressaIorio\app.db"
$serviceName = "DottoressaIorio.App"
$onedrivePath = [System.IO.Path]::Combine($env:USERPROFILE, "OneDrive")
$vaultPath = [System.IO.Path]::Combine($onedrivePath, "Personal Vault")
$backupPath = [System.IO.Path]::Combine($vaultPath, $serviceName)

$backupPath2 = "C:\Program Files\DottoressaIorio\_db_backup\"

# Check if the backup folder exists, create it if not
if (-not (Test-Path $backupPath -PathType Container)) {
    New-Item -Path $backupPath -ItemType Directory
}

# Stop the service using NSSM
.\nssm stop $serviceName

# Copy the item with a timestamped backup file name
Copy-Item $sourcePath ([System.IO.Path]::Combine($backupPath, "app_backup_$(Get-Date -Format 'yyyyMMddHHmmss').db"))

# Copy the item with a timestamped backup file name 2
Copy-Item $sourcePath "$backupPath\app_backup_$(Get-Date -Format 'yyyyMMddHHmmss').db"


# Start the installed service using NSSM
.\nssm start $serviceName
