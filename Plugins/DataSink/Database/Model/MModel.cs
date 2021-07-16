using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Reflection;
using Pomelo.EntityFrameworkCore.MySql;

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
        /// Entity for measurement Locations
        /// </summary>
        public DbSet<MLocation> DBMeasurementLocations { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="options"></param>
        public MModel(DbContextOptions<MModel> options) : base(options)
        {
        }

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
                var connectionString = @"server={Options.Server};port={Options.Port};database={Options.Database};user={Options.UserId};password={Options.Password}";
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
                optionsBuilder.UseMySql(connectionString, serverVersion, options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
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
            modelBuilder.Entity<MType>().ToTable("MType");
            modelBuilder.Entity<MType>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Entity for measurement units
            modelBuilder.Entity<MUnit>().ToTable("MUnit");
            modelBuilder.Entity<MUnit>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Entity for measurement locations
            modelBuilder.Entity<MLocation>().ToTable("MLocation");
            modelBuilder.Entity<MLocation>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Entity for measurement values
            modelBuilder.Entity<MValue>().ToTable("MValue");
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

            // Foreign key from measurement value to measurement unit
            modelBuilder.Entity<MLocation>()
                    .HasMany(c => c.Values)
                    .WithOne(e => e.Location)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.SetNull);

            // Create the model
            base.OnModelCreating(modelBuilder);
        }
    }
}
