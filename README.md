[![NuGet](https://img.shields.io/nuget/v/LikeComparison)](https://www.nuget.org/packages/LikeComparison)
![Nuget](https://img.shields.io/nuget/dt/LikeComparison)

# LikeComparison
LikeComparison is a library that allows you to compare a string expression to a pattern in an "SQL LIKE" expression.

It supports many LIKE operator syntax:
- Visual Basic,
- Transact-SQL,
- PostgreSQL.

Currently there are only ***case-insensitive*** comparison supported.

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

bool isMatched = matchExpression.Like(pattern);
```
or

```cs
using LikeComparison.PostgreSql;
```
```cs
string matchExpression = "abcdef";
string pattern = "a%";

bool isMatched = matchExpression.ILike(pattern);
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