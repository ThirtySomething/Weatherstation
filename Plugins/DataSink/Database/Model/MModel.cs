using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Database model for weatherstation data
    /// </summary>
    public class MModel : DbContext
    {
        /// <summary>
        /// Entity for measurement types
        /// </summary>
        public DbSet<MType> DBMeasurementTypes { get; set; }

        /// <summary>
        /// Entity for measurement units
        /// </summary>
        public DbSet<MUnit> DBMeasurementUnits { get; set; }

        /// <summary>
        /// Entity for measurement values
        /// </summary>
        public DbSet<MValue> DBMeasurementValues { get; set; }

        /// <summary>
        /// Select correct database type and corresponding options
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (DatabaseConfig.Instance.DatabaseType)
            {
                case DatabaseConfig.SupportedDatabaseTypes.DBSQLite:
                    ConnectToSQLite(optionsBuilder);
                    break;
                case DatabaseConfig.SupportedDatabaseTypes.MariaDB:
                    ConnectToMariaDB(optionsBuilder);
                    break;
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Handle connection to MariaDB database
        /// </summary>
        /// <param name="optionsBuilder">Connection options object</param>
        private void ConnectToMariaDB(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                DatabaseConfig.ParamMariaDB Options = JsonConvert.DeserializeObject<DatabaseConfig.ParamMariaDB>(DatabaseConfig.Instance.DatabaseOptions);
                optionsBuilder.UseMySql($"Server={Options.Server};User Id={Options.UserId};Password={Options.Password};Database={Options.Database}", options =>
               {
                   options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                       .ServerVersion(new Version(10, 4, 11), ServerType.MariaDb)
                       .CharSetBehavior(CharSetBehavior.AppendToAllColumns);
                    //.AnsiCharSet(CharSet.Latin1)
                    //.UnicodeCharSet(CharSet.Utf8mb4);
                });
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(ConnectToMariaDB)}: Invalid config object [{DatabaseConfig.Instance.DatabaseOptions}] for MariaDB recieved - exception [{e.Message}].");
            }
        }

        /// <summary>
        /// Handle connection to SQLite database
        /// </summary>
        /// <param name="optionsBuilder">Connection options object</param>
        private void ConnectToSQLite(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                DatabaseConfig.ParamSQLite Options = JsonConvert.DeserializeObject<DatabaseConfig.ParamSQLite>(DatabaseConfig.Instance.DatabaseOptions);
                optionsBuilder.UseSqlite($"Filename={Options.Filename}", options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(ConnectToSQLite)}: Invalid config object [{DatabaseConfig.Instance.DatabaseOptions}] for SQLite recieved - exception [{e.Message}].");
            }
        }

        /// <summary>
        /// To create the database model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity for measurement types
            modelBuilder.Entity<MType>().ToTable("MType", null);
            modelBuilder.Entity<MType>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Entity for measurement units
            modelBuilder.Entity<MUnit>().ToTable("MUnit", null);
            modelBuilder.Entity<MUnit>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Entity for measurement values
            modelBuilder.Entity<MValue>().ToTable("MValue", null);
            modelBuilder.Entity<MValue>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.RecordTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Foreign key from measurement value to measurement type
            modelBuilder.Entity<MType>()
                    .HasMany(c => c.Values)
                    .WithOne(e => e.Type)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.SetNull);

            // Foreign key from measurement value to measurement unit
            modelBuilder.Entity<MUnit>()
                    .HasMany(c => c.Values)
                    .WithOne(e => e.Unit)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.SetNull);

            // Create the model
            base.OnModelCreating(modelBuilder);
        }
    }
}
