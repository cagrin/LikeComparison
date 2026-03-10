## Run unit tests with .NET 10 with code coverage

```
dotnet test LikeComparison.Tests               --framework net10.0 -e CollectCoverage=true -e CoverletOutputFormat=lcov --logger "console;verbosity=detailed"
dotnet test LikeComparison.Tests.WithDatabases --framework net10.0 -e CollectCoverage=true -e CoverletOutputFormat=lcov --logger "console;verbosity=detailed"
```