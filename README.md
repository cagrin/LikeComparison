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

## Using ```Like``` method on strings

You can simply use extension method on `String` class:
```cs
using LikeComparison.VisualBasic;
```
```cs
string matchExpression = "abcdef";
string pattern = "a*";

bool isMatched = matchExpression.Like(pattern);
```
or

```cs
using LikeComparison.TransactSql;
```
```cs
string matchExpression = "abcdef";
string pattern = "a%";

// common use
bool isMatched = matchExpression.Like(pattern);

// or with escape character
bool isMatched = matchExpression.Like(pattern, escapeCharacter: "/");
```
or

```cs
using LikeComparison.PostgreSql;
```
```cs
string matchExpression = "abcdef";
string pattern = "a%";

// case-insensitive
bool isMatched = matchExpression.ILike(pattern);

// or case-sensitive
bool isMatched = matchExpression.Like(pattern);
```

## Using ```IsLike``` method on MSTest assertions

You can simply use extension method on `Assert` class:
```cs
using LikeComparison.VisualBasic;
```
```cs
string matchExpression = "abcdef";
string pattern = "a*";

Assert.That.IsLike(matchExpression, pattern);
```
or

```cs
using LikeComparison.TransactSql;
```
```cs
string matchExpression = "abcdef";
string pattern = "a%";

// common use
Assert.That.IsLike(matchExpression, pattern);

// or with escape character
Assert.That.IsLike(matchExpression, pattern, escape: "/");
```
or

```cs
using LikeComparison.PostgreSql;
```
```cs
string matchExpression = "abcdef";
string pattern = "a%";

// case-insensitive
Assert.That.IsILike(matchExpression, pattern);

// or case-sensitive
Assert.That.IsLike(matchExpression, pattern);
```

## Using ```ShouldBeLike``` method on Shouldly assertions

You can simply use extension method on `String` class:
```cs
using LikeComparison.VisualBasic;
```
```cs
string matchExpression = "abcdef";
string pattern = "a*";

matchExpression.ShouldBeLike(pattern);
```
or

```cs
using LikeComparison.TransactSql;
```
```cs
string matchExpression = "abcdef";
string pattern = "a%";

// common use
matchExpression.ShouldBeLike(pattern);

// or with escape character
matchExpression.ShouldBeLike(pattern, escape: "/");
```
or

```cs
using LikeComparison.PostgreSql;
```
```cs
string matchExpression = "abcdef";
string pattern = "a%";

// case-insensitive
matchExpression.ShouldBeILike(pattern);

// or case-sensitive
matchExpression.ShouldBeLike(pattern);
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
### .NET 7.0
- Preview.
- Additional target for LikeComparison.Tests.
### .NET 6.0
- Recommended.
- Default target for LikeComparison.Tests and code coverage.
### .NET Core 3.1
- Supported.
### .NET Framework 4.8
- Experimental supported with C# 8.