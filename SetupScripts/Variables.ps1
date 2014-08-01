$machineName = gc env:computername
$currentPath = Split-Path $script:MyInvocation.MyCommand.Path

$doc = New-Object System.Xml.XmlDocument
$doc.Load($currentPath + "\Variables.xml") 

#Modules
$iisModule = Join-Path $currentPath "\IIS.ps1"
$sqlModule = Join-Path $currentPath "\SQL.ps1"
$functionsModule = Join-Path $currentPath "\Functions.ps1"
$sitefinitySetup = Join-Path $currentPath "\SitefinitySetup.ps1"
$cleanup = Join-Path $currentPath "\Cleanup.ps1"
 
#Website setup properties

$siteName = $doc.SelectSingleNode("//variables/siteName").InnerText
$websiteUrl = $doc.SelectSingleNode("//variables/websiteUrl").InnerText
$websitePort = $doc.SelectSingleNode("//variables/websitePort").InnerText
$projectWebSiteLocation =  $doc.SelectSingleNode("//variables/projectWebSiteLocation").InnerText
$databaseLocation = $doc.SelectSingleNode("//variables/databaseLocation").InnerText
$databaseName = $doc.SelectSingleNode("//variables/databaseName").InnerText

$databaseServer = $doc.SelectSingleNode("//variables/databaseServer").InnerText
$appPollName = $siteName

$aspNetTempFolder = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Temporary ASP.NET Files\root"
$sqlCmdExe = "C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE" 
$msBuildExe64 = "C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
$msBuildExe32 = "C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"