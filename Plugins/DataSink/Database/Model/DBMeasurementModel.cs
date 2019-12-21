using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Database model for weatherstation data
    /// </summary>
    public class DBMeasurementModel : DbContext
    {
        /// <summary>
        /// Entity for measurement types
        /// </summary>
        public DbSet<DBMeasurementType> DBMeasurementTypes { get; set; }

        /// <summary>
        /// Entity for measurement units
        /// </summary>
        public DbSet<DBMeasurementUnit> DBMeasurementUnits { get; set; }

        /// <summary>
        /// Entity for measurement values
        /// </summary>
        public DbSet<DBMeasurementValue> DBMeasurementValues { get; set; }

        /// <summary>
        /// Overwritten method, select correct database type and corresponding options
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (DatabaseConfig.Instance.DatabaseType)
            {
                case DatabaseConfig.SupportedDatabaseTypes.DBSQLite:
                    try
                    {
                        DatabaseConfig.ParamSQLite Options = JsonConvert.DeserializeObject<DatabaseConfig.ParamSQLite>(DatabaseConfig.Instance.DatabaseParameters);
                        optionsBuilder.UseSqlite($"Filename={Options.Filename}", options =>
                        {
                            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        });
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine($"{nameof(OnConfiguring)}: Invalid config object [{DatabaseConfig.Instance.DatabaseParameters}] for SQLite recieved - exception [{e.Message}].");
                    }
                    break;
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// To create the database model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity for measurement types
            modelBuilder.Entity<DBMeasurementType>().ToTable("MeasurementType", "Weatherdata");
            modelBuilder.Entity<DBMeasurementType>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Entity for measurement units
            modelBuilder.Entity<DBMeasurementUnit>().ToTable("MeasurementUnit", "Weatherdata");
            modelBuilder.Entity<DBMeasurementUnit>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Entity for measurement values
            modelBuilder.Entity<DBMeasurementValue>().ToTable("MeasurementValue", "Weatherdata");
            modelBuilder.Entity<DBMeasurementValue>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.RecordTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Foreign key from measurement value to measurement type
            modelBuilder.Entity<DBMeasurementType>()
                    .HasMany(c => c.MeasurementValues)
                    .WithOne(e => e.MeasurementType)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.SetNull);

            // Foreign key from measurement value to measurement unit
            modelBuilder.Entity<DBMeasurementUnit>()
                    .HasMany(c => c.MeasurementValues)
                    .WithOne(e => e.MeasurementUnit)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.SetNull);

            // Create the model
            base.OnModelCreating(modelBuilder);
        }
    }
}
