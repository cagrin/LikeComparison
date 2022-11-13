## Show output during test with .NET 6

```dotnet test ./LikeComparison.Tests --framework net6.0 -l "console;verbosity=detailed" -e CollectCoverage=true -e CoverletOutputFormat=lcov```

## Filter smoke tests with .NET 6

```dotnet test ./LikeComparison.Tests --framework net6.0 -e CollectCoverage=true -e CoverletOutputFormat=lcov --filter "(ClassName!~LikeComparison.DatabaseTests)"```