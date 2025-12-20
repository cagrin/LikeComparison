[![NuGet](https://img.shields.io/nuget/v/LikeComparison)](https://www.nuget.org/packages/LikeComparison)
[![Nuget](https://img.shields.io/nuget/dt/LikeComparison)](https://www.nuget.org/stats/packages/LikeComparison?groupby=Version)
[![Coverage Status](https://img.shields.io/coveralls/github/cagrin/LikeComparison)](https://coveralls.io/github/cagrin/LikeComparison)

# LikeComparison
**LikeComparison** is a C# library that enables string pattern matching using SQL-style `LIKE` expressions. It mimics the behavior of the `LIKE` operator found in SQL dialects, making it useful for many .NET use cases.

## Key Features

- **Supports multiple SQL syntaxes**:
  - [**Visual Basic**](https://docs.microsoft.com/en-us/office/vba/language/reference/user-interface-help/like-operator): `*`, `?`, `[ ]`, `^`, `#`
  - [**Transact-SQL**](https://docs.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql): `%`, `_`, `[ ]`, `!`, with optional `ESCAPE`
  - [**PostgreSQL**](https://www.postgresql.org/docs/current/functions-matching.html): `LIKE` and `ILIKE` (case-insensitive), using `%`, `_`

- **Extension Methods**:
  - `Like()` and `ILike()` for string comparisons
  - MSTest: `Assert.That.IsLike()` and `IsILike()`
  - Shouldly: `ShouldBeLike()` and `ShouldBeILike()`

- **Cross-platform compatibility**:
  - .NET 8.0 and 10.0 (recommended)
  - .NET Standard 2.0 (experimental support with C# 8)

## Using ```Like``` method on strings from [LikeComparison](https://www.nuget.org/packages/LikeComparison) package

You can simply use extension method on `String` class:
```cs
using LikeComparison.VisualBasic;
```
```cs
string? matchExpression = "abcdef";

bool isLike = matchExpression.Like(pattern: "a*");
```
or

```cs
using LikeComparison.TransactSql;
```
```cs
string? matchExpression = "abcdef";

// common use
bool isLike = matchExpression.Like(pattern: "a%");

// or with escape character
bool isLike = matchExpression.Like(pattern: "/a%", escapeCharacter: "/");
```
or

```cs
using LikeComparison.PostgreSql;
```
```cs
string? matchExpression = "abcdef";

// case-insensitive
bool isLike = matchExpression.ILike(pattern: "A%");

// or case-sensitive
bool isLike = matchExpression.Like(pattern: "a%");
```

## Using ```IsLike``` method on MSTest assertions from [LikeComparison.MSTest](https://www.nuget.org/packages/LikeComparison.MSTest) package


You can simply use extension method on `Assert` class:
```cs
using LikeComparison.VisualBasic;
```
```cs
string? matchExpression = "abcdef";

Assert.That.IsLike(matchExpression, pattern: "a*");
```
or

```cs
using LikeComparison.TransactSql;
```
```cs
string? matchExpression = "abcdef";

// common use
Assert.That.IsLike(matchExpression, pattern: "a%");

// or with escape character
Assert.That.IsLike(matchExpression, pattern: "/a%", escape: "/");
```
or

```cs
using LikeComparison.PostgreSql;
```
```cs
string? matchExpression = "abcdef";

// case-insensitive
Assert.That.IsILike(matchExpression, pattern: "A%");

// or case-sensitive
Assert.That.IsLike(matchExpression, pattern: "a%");
```

## Using ```ShouldBeLike``` method on Shouldly assertions from [LikeComparison.Shouldly](https://www.nuget.org/packages/LikeComparison.Shouldly) package


You can simply use extension method on `String` class:
```cs
using LikeComparison.VisualBasic;
```
```cs
string? matchExpression = "abcdef";

matchExpression.ShouldBeLike(pattern: "a*");
```
or

```cs
using LikeComparison.TransactSql;
```
```cs
string? matchExpression = "abcdef";

// common use
matchExpression.ShouldBeLike(pattern: "a%");

// or with escape character
matchExpression.ShouldBeLike(pattern: "/a%", escape: "/");
```
or

```cs
using LikeComparison.PostgreSql;
```
```cs
string? matchExpression = "abcdef";

// case-insensitive
matchExpression.ShouldBeILike(pattern: "A%");

// or case-sensitive
matchExpression.ShouldBeLike(pattern: "a%");
```
