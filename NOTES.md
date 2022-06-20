## Show output during test

```dotnet test ./LikeComparison.Tests -l "console;verbosity=detailed" /p:CollectCoverage=true /p:CoverletOutputFormat=lcov```

## Filter smoke tests
```dotnet test ./LikeComparison.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=lcov --filter "(FullyQualifiedName~StringTests | FullyQualifiedName~AssertTests)"```