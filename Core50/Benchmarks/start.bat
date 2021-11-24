@REM start /d "Receive_Topic" dotnet run --no-build "#"
@REM start /d "Receive_Topic" dotnet run --no-build "kern.*"

rem wt -d "Producer_Topic" dotnet run --no-build    
rem wt -d "Consumer_Topic" dotnet run --no-build Tesla ^
rem ; split-pane -H -d "Consumer_Topic" dotnet run --no-build Bmw ^
rem ; split-pane -V -d "Consumer_Topic" dotnet run --no-build Mercedes 

rem dotnet build -c Release
rem dotnet bin/Release/net5.0/Benchmarks.dll

dotnet build -c Release && cmd.exe /c "wt.exe dotnet bin/Release/net5.0/Benchmarks.dll"