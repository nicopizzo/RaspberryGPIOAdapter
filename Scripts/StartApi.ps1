Param(
    [string]$server,
    [string]$username,
    [string]$password,
    [string]$appExe
)

$securePw = ConvertTo-SecureString $password -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential($username, $securePw)

$remoteCommand = {
    Param($exe)
	$exe
}

Invoke-Command -ComputerName $server -ScriptBlock $remoteCommand -ArgumentList $appExe -Credential $cred