[![NuGet](https://img.shields.io/nuget/v/LikeComparison)](https://www.nuget.org/packages/LikeComparison)
[![Nuget](https://img.shields.io/nuget/dt/LikeComparison)](https://www.nuget.org/stats/packages/LikeComparison?groupby=Version)
[![Coverage Status](https://img.shields.io/coveralls/github/cagrin/LikeComparison)](https://coveralls.io/github/cagrin/LikeComparison)

# LikeComparison
LikeComparison is a library that allows you to compare a string expression to a pattern in an "SQL LIKE" expression.

It supports many LIKE operator syntax:
- Visual Basic,
- Transact-SQL,
- PostgreSQL.


## Using Like method

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
### .NET 6.0
- Recommended.
- Default target for LikeComparison.Tests.
### .NET 5.0
- Supported.
### .NET Core 3.1
- Supported.
### .NET Framework 4.8
- Experimental supported with C# 8.