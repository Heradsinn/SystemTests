$MasResponse = (Invoke-WebRequest -URI $env:masUrl -Method 'GET').StatusCode
Write-Host "Response from '$env:masUrl': $MasResponse"

$SmResponse = (Invoke-WebRequest -URI $env:smUrl -Method 'GET').StatusCode
Write-Host "Response from '$env:smUrl': $SmResponse"

$TransactionApiResponse = (Invoke-WebRequest -URI $env:transactionApiUrl -Method 'GET').StatusCode
Write-Host "Response from '$env:transactionApiUrl': $TransactionApiResponse"

$IntegrationApiResponse = (Invoke-WebRequest -URI $env:integrationApiUrl -Method 'GET').StatusCode
Write-Host "Response from '$env:integrationApiUrl': $IntegrationApiResponse"

if ($TransactionApiResponse -ne 200 -or $MasResponse -ne 200 -or $SmResponse -ne 200 -or $IntegrationApiResponse -ne 200) {
	Write-Host  "##vso[task.LogIssue type=error;]One or more azure- apps returned an unexpected response code."
    exit 1
}
else {
	Write-Host "##vso[task.setvariable variable=testTasksShouldRun;]true"
	exit 0 
}