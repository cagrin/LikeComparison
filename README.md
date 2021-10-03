# LikeComparison
LikeComparison is a library that allows you to compare a string expression to a pattern in an "SQL LIKE" expression.

It supports many LIKE operator syntax:
- Visual Basic,
- Transact-SQL,
- PostgreSQL.

## Using Like method

You can simply use extension method on `String` class:

```cs
string matchExpression = "abcdef";
string pattern = "[az]_%[^qz]ef";

bool isMatched = matchExpression.Like(pattern, new LikeOptions());
```

`LikeOptions` currently available are:
```cs
enum PatternStyle
{
    VisualBasic = 0,
    TransactSql = 1
}
```
```cs
StringComparison.OrdinalIgnoreCase
```