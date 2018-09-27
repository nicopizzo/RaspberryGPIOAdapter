Param(
    [string]$server,
    [string]$username,
    [string]$password,
    [string]$processName
)

$securePw = ConvertTo-SecureString $password -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential($username, $securePw)

$remoteCommand = {
    Param($process)
	Get-Process | Where-Object { $_.Name -eq $process } | Select-Object -First 1 | Stop-Process -Force
}

Invoke-Command -ComputerName $server -ScriptBlock $remoteCommand -ArgumentList $processName -Credential $cred