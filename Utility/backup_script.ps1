$sourcePath = "C:\Program Files\DottoressaIorio\app.db"
$serviceName = "DottoressaIorio.App"
$onedrivePath = [System.IO.Path]::Combine($env:USERPROFILE, "OneDrive")
$vaultPath = [System.IO.Path]::Combine($onedrivePath, "Personal Vault")
$backupPath = [System.IO.Path]::Combine($vaultPath, $serviceName)

# Check if the backup folder exists, create it if not
if (-not (Test-Path $backupPath -PathType Container)) {
    New-Item -Path $backupPath -ItemType Directory
}

# Stop the service using NSSM
.\nssm stop $serviceName

# Copy the item with a timestamped backup file name
Copy-Item $sourcePath ([System.IO.Path]::Combine($backupPath, "app_backup_$(Get-Date -Format 'yyyyMMddHHmmss').db"))

# Start the installed service using NSSM
.\nssm start $serviceName
