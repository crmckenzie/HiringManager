param(
	[string] $databaseName = "HiringManagerDb",
	[string] $buildType="Debug",
	[string] $targetMigration = "",
	[string] $entityFrameworkVersion = "6.1.0"
)

$workingDirectory = "HiringManager.EntityFramework.Migrations\bin\$buildType"
$migrateExe = "packages\EntityFramework.$entityFrameworkVersion\tools\migrate.exe"

write-host "copying $migrateExe to $workingDirectory"

copy $migrateExe $workingDirectory 
$migrateCommand = "$workingDirectory\migrate.exe"

$connectionString="Data Source=.;Initial Catalog=$databaseName;Integrated Security=True"
$assembly = "HiringManager.EntityFramework.Migrations.dll"

$cmd = "$migrateCommand ""$assembly"" /startupDirectory:""$workingDirectory"" /connectionstring:""$connectionString"" /connectionProviderName:""System.Data.SqlClient"""
if ([String]::IsNullOrEmpty($targetMigration ) -eq $false){
	$cmd = $cmd + " /targetMigration:""$targetMigration"""
}
$cmd 

invoke-expression $cmd | Write-Host