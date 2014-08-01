Import-Module WebAdministration

$currentPath = Split-Path $script:MyInvocation.MyCommand.Path
$variables = Join-Path $currentPath "\Variables.ps1"
. $variables
. $iisModule
. $sqlModule
. $functionsModule
. $cleanup

write-output "------- Installing Sitefinity --------"

ProjectCleanup

write-output "Setting up Application pool..."

New-WebAppPool $appPollName -Force

Set-ItemProperty IIS:\AppPools\$appPollName managedRuntimeVersion v4.0 -Force

#Setting application pool identity to NetworkService
Set-ItemProperty IIS:\AppPools\$appPollName processmodel.identityType -Value 2 

write-output "Deploy SitefinityWebApp to test execution machine $machineName"

write-output "Unzipping Database..."

UnZipFile $databaseLocation "$databaseName.zip"

write-output "Sitefinity successfully deployed."

function InstallSitefinity()
{
    RestoreDatabaseWithMove $databaseServer $databaseName $databaseName $databaseLocation
    
	$siteId = GetNextWebsiteId
	write-output "Registering $siteName website with id $siteId in IIS."
	New-WebSite -Id $siteId -Name $siteName -Port $websitePort -HostHeader localhost -PhysicalPath $projectWebSiteLocation -ApplicationPool $appPollName -Force
	Start-WebSite -Name $siteName

	write-output "Setting up Sitefinity..."

	$installed = $false
	$count = 0
	while(!$installed){
		if($count -eq 100)
		{
			$errorMsg = "Unable to install host Sitefinity in IIS"
			Throw New-Object System.Exception($errorMsg)
		}
		try{    
			$response = GetRequest $websiteUrl
			if($response.StatusCode -eq "OK"){
				$installed = $true;
				$response
				write-output "----- Sitefinity successfully installed ------"
			}
		}catch [Exception]{
			Restart-WebAppPool $appPollName -ErrorAction Continue
			$count = $count + 1
			write-output "--> $count try"
			write-output "$_.Exception.Message"
			$installed = $false
			
		}
	}
}

function BuildSolution($slnFile)
{
    $BuildArgs = @{
         FilePath = $msBuildExe64
         ArgumentList = $slnFile, "/t:Build"
         RedirectStandardOutput = $true
         Wait = $true
     }
     
    Start-Process @BuildArgs
}
