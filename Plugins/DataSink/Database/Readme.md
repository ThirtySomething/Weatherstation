# Database

The Database plugin writes the measurement values to a database supported by the [Entity Framework Core][EFC].

Return to [main](./../Readme.md).

## DatabaseConfig.config

For the Database plugin the following settings are available:

* `DatabaseType` - The kind of database to write to:
  * SQLite
  * MariaDB
  * Default is `SQLite`.
* `DatabaseOptions` - The corresponding database parameters as JSON string.
  * For SQLite there is only one option
    * `Filename` - to set the name of the database.
    * The option string looks like `{Filename: "weatherdata.db"}`.
  * For MariaDB there are several options:
    * `Server` - The server name or IP where your MariaDB is running.
    * `UserId` - The user for the database connection.
    * `Password` -  The password for the database connection.
    * `Database` - The database used to write to.
    * `Port` - The port of the database instance.
    * The option string looks like `{Server: "localhost", UserId: "weather", Password: "sunshine", Database: "weatherdata", Port: 3306}`.
    * You have to set all database parameters!

For more details about the supported database types and parameters see file [DatabaseConfig.cs](./DatabaseConfig.cs).

In case you're in trouble creating the database schema using a bare database and this plugin, you may try one of these SQL scripts to create the schema manually:
* [weatherdata.mariadb.sql](./weatherdata.mariadb.sql) - for MariaDB
* [weatherdata.sqlite.sql](./weatherdata.sqlite.sql) - for SQLite

[EFC]: https://docs.microsoft.com/en-us/ef/core/
