dotnet build
Start-Process -FilePath 'dotnet' -ArgumentList 'run --project .\Source\Server\ --no-build --launch-profile "BlazorHosted.Server Alice"'
Start-Process -FilePath 'dotnet' -ArgumentList 'run --project .\Source\Server\ --no-build --launch-profile "BlazorHosted.Server Faber"'