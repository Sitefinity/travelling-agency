Import-Module WebAdministration

Function CleanDirectory($dir){    
    Cmd /C "rmdir /S /Q $dir 2>NUL"
}

function CleanWebsiteDirectory($dir, $retryCount, $appPollName)
{
    write-output "Cleaning website directory"
    
    for ($i=1; $i -le $retryCount; $i++)
    {
        if(Test-Path $dir)
        {            
		    Restart-WebAppPool $appPollName
			Start-Sleep -s 3
		    CleanDirectory $dir
            if($i -eq $retryCount)
            {
                $errorMsg = "Unable to clean "+ $dir +" directory..."
                Throw New-Object System.Exception($errorMsg)
            }
            write-output "Cleaning $dir... [ Retry$i ]"           
        } else {
            write-output "$dir cleaned successfully."
            break
        }
    }
}

function UnZipFile($location, $filename)
{
	$shell_app = new-object -com shell.application
	$zip_file = $shell_app.namespace($location + "\$filename")
	$destination = $shell_app.namespace($location)
	$destination.Copyhere($zip_file.items(), 0x14)
}

function EnsureSitefinityIsRunning([String]$url="http://localhost", [String]$successOuput="SUCCESS", [Int32]$totalWaitMinutes=10)
{
       $elapsed = [System.Diagnostics.Stopwatch]::StartNew()

       $statusUrl = ($url + "/appstatus")
       Write-Host "Attempt to start Sitefinity up: ${statusUrl}"

       try 
       { 
         $response = Invoke-WebRequest $statusUrl -TimeoutSec 300 -UseBasicParsing

         if($response.StatusCode -eq 200)
         {
              Write-Host "Sitefinity is starting ... ${statusUrl}"
         }  

         while($response.StatusCode -eq 200)
         {    
              Write-Host "Checking Sitefinity status: ${statusUrl}"
              $response = Invoke-WebRequest $statusUrl -TimeoutSec 300 -UseBasicParsing

              if($elapsed.Elapsed.TotalMinutes > $totalWaitMinutes)
              {
                Write-Host "Sitefinity did NOT start in the specified maximum time"
                break
              }

              Start-Sleep -s 1
         }
       } 
       catch 
       {
         if($_.Exception.Response.StatusCode.Value__ -eq 404)
         {
              $response = Invoke-WebRequest $url -TimeoutSec 120 -UseBasicParsing

              if($response.StatusCode -eq 200)
              {
                $elapsed.Stop();
                $elapsedTime = $elapsed.Elapsed.Seconds
                Write-Host "Sitefinity has started after ${elapsedTime} second(s) - ${successOuput}"
                return $response
              }
              else
              {
                Write-Host "Sitefinity failed to start"
              }
         }
         else
         {
           $statusCode = $_.Exception.Response.StatusCode.Value__
              Write-Host "Sitefinity failed to start - StatusCode: ${statusCode}"
       
              Write-Host $_|format-list -force
              Write-Host $_.Exception|format-list -force
         }

         return $_.Exception.Response
	 }
}
