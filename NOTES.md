## Switch dotnet cli to a non-system language

```
$Env:DOTNET_CLI_UI_LANGUAGE="en"
```

## Show output during test with .NET 6

```
dotnet test LikeComparison.Tests --framework net6.0 -e CollectCoverage=true -e CoverletOutputFormat=lcov --logger "console;verbosity=detailed"
```

## Run unit tests with .NET 8 and coverage

```
dotnet test LikeComparison.Tests --framework net8.0 -e CollectCoverage=true -e CoverletOutputFormat=lcov
```