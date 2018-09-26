Param(
    [string]$server,
    [string]$username,
    [string]$password,
    [string]$sharedFolder
)

$securePw = ConvertTo-SecureString $password -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential($username, $securePw)

$remoteCommand = {
    param($sFolder)
    FolderPermissions $sFolder -e
}

Invoke-Command -ComputerName $server -ScriptBlock $remoteCommand -ArgumentList $sharedFolder -Credential $cred
