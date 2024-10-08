## Switch dotnet cli to a non-system language

```
$Env:DOTNET_CLI_UI_LANGUAGE="en"
```

## Run unit tests with .NET 8 with code coverage

```
dotnet test LikeComparison.Tests                --framework net8.0 -e CollectCoverage=true -e CoverletOutputFormat=lcov --logger "console;verbosity=detailed"
dotnet test LikeComparison.Tests.WithConnection --framework net8.0 -e CollectCoverage=true -e CoverletOutputFormat=lcov --logger "console;verbosity=detailed"
```