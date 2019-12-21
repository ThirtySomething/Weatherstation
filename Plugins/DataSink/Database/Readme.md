# Database

The Database plugin writes the measurement values to a database supported by the [Entity Framework Core][EFC].

Return to [main](./../Readme.md).

## DatabaseConfig.config

For the Database plugin the following settings are available:

* `DatabaseType` - The kind of database to write to. Default is 1 for SQLite.
* `DatabaseParameters` - The corresponding database parameters as JSON string.

For more details about the supported database types and parameters see file `DatabaseConfig.cs`.

[EFC]: https://docs.microsoft.com/en-us/ef/core/
