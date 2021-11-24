### [run-benchmarkdotnet-in-a-docker-container](https://wojciechnagorski.com/2019/12/how-to-run-benchmarkdotnet-in-a-docker-container/)

```sh
dotnet build -c Release && cmd.exe /c "wt.exe dotnet bin/Release/net5.0/Benchmarks.dll"
```