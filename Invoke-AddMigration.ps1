param(
	[Parameter(Mandatory=$true)]
	[string] $Name
)

Add-Migration  -Name $Name -ProjectName HiringManager.EntityFramework.Migrations -StartUpProjectName HiringManager.Web -ConnectionStringName HiringManager.EntityFramework.HiringManagerDbContext
