Param(
    [string]$applicationDir,
    [string]$deployDir
)

cd $applicationDir

dotnet publish -r win10-arm

Get-ChildItem -Path $deployDir -Include *.* -File -Recurse | foreach { $_.Delete()}

xcopy.exe /y ".\bin\Debug\netcoreapp2.1\win10-arm\publish" $deployDir
