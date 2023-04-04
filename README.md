[![NuGet](https://img.shields.io/nuget/v/LikeComparison)](https://www.nuget.org/packages/LikeComparison)
[![Nuget](https://img.shields.io/nuget/dt/LikeComparison)](https://www.nuget.org/stats/packages/LikeComparison?groupby=Version)
[![Coverage Status](https://img.shields.io/coveralls/github/cagrin/LikeComparison)](https://coveralls.io/github/cagrin/LikeComparison)

# LikeComparison
LikeComparison is a library that allows you to compare a string expression to a pattern in an "SQL LIKE" expression.

It supports many LIKE operator syntax:
- Visual Basic,
- Transact-SQL (with ESCAPE),
- PostgreSQL (both LIKE and ILIKE).

Main use:
- ```Like``` method on strings
- ```IsLike``` method on MSTest assertions
- ```ShouldBeLike``` method on Shouldly assertions

## Using ```Like``` method on strings from [LikeComparison](https://www.nuget.org/packages/LikeComparison) package

You can simply use extension method on `String` class:
```cs
using LikeComparison.VisualBasic;
```
```cs
string? matchExpression = "abcdef";

_ = matchExpression.Like(pattern: "a*");
```
or

```cs
using LikeComparison.TransactSql;
```
```cs
string? matchExpression = "abcdef";

// common use
_ = matchExpression.Like(pattern: "a%");

// or with escape character
_ = matchExpression.Like(pattern: "/a%", escapeCharacter: "/");
```
or

```cs
using LikeComparison.PostgreSql;
```
```cs
string? matchExpression = "abcdef";

// case-insensitive
_ = matchExpression.ILike(pattern: "A%");

// or case-sensitive
_ = matchExpression.Like(pattern: "a%");
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

## Supported syntax
### Visual Basic

https://docs.microsoft.com/en-us/office/vba/language/reference/user-interface-help/like-operator

```* ? [ ] ^ #```
###  Transact-SQL

https://docs.microsoft.com/en-us/sql/t-sql/language-elements/like-transact-sql

```% _ [ ] !```
###  PostgreSQL

https://www.postgresql.org/docs/current/functions-matching.html

```% _```

## Supported targets
### .NET Framework 4.8
- Experimental supported with C# 8.
### .NET 6.0
- Recommended.
- Default target for code coverage.
### .NET 7.0
- Supported.
### .NET 8.0
- Supported.